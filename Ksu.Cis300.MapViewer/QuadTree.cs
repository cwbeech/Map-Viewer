/* QuadTree.cs
 * Author: Calvin Beechner
 */
using Ksu.Cis300.ImmutableBinaryTrees;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.AxHost;

namespace Ksu.Cis300.MapViewer
{
    /// <summary>
    /// QuadTree Class. Used for building and drawing quad trees implemented using binary trees.
    /// </summary>
    public static class QuadTree
    {
        /// <summary>
        /// A const int giving the threshold for increasing the zoom level.
        /// </summary>
        private const int _zoomThreshold = 100;

        /// <summary>
        /// A const int giving the field index of the width of the map bounds.
        /// </summary>
        private const int _width = 0;

        /// <summary>
        /// A const int giving the field index of the length of the map bounds.
        /// </summary>
        private const int _length = 1;

        /// <summary>
        /// A const int giving the field index of the x-coordinate of the first point listed on an input lineafter the first line.
        /// </summary>
        private const int _startX = 0;

        /// <summary>
        /// A const int giving the field index of the y-coordinate of the first point listed on an input line after the first line.
        /// </summary>
        private const int _startY = 1;

        /// <summary>
        /// A const int giving the field index of the x-coordinate of the second point listed on an input line after the first line.
        /// </summary>
        private const int _endX = 2;

        /// <summary>
        /// A const int giving the field index of the y-coordinate of the second point listed on an input line after the first line.
        /// </summary>
        private const int _endY = 3;

        /// <summary>
        /// A const int giving the field index of the color on an input line after the first line.
        /// </summary>
        private const int _color = 4;

        /// <summary>
        /// A const int giving the field index of the line segment width on an input line after the first line.
        /// </summary>
        private const int _lineWidth = 5;

        /// <summary>
        /// A const int giving the zoom level on an input line after the first line.
        /// </summary>
        private const int _zoomLevel = 6;

        /// <summary>
        /// A Dictionary<(int, float), Pen> to contain the Pens used for drawing line segments.
        /// </summary>
        private static Dictionary<(int, float), Pen> _pens = new Dictionary<(int, float), Pen>();

        /// <summary>
        /// Builds a tree;
        /// </summary>
        /// <param name="lineSegments">A list<LineSegment> containing the line segments to be included in the tree.</param>
        /// <param name="mapBounds">A RectangleF giving the map bounds for the root node.</param>
        /// <param name="zoomLevel">An int giving the zoom level for the root node.</param>
        /// <param name="isQuadTreeNode">A bool indicating whether the root is a quad tree node.</param>
        /// <param name="maxZoomLevel">An out int giving the maximum zoom level in the tree.</param>
        /// <returns>Returns a BinaryTreeNode<MapData> giving the root of a tree containing the information given in the parameters.</returns>
        private static BinaryTreeNode<MapData> BuildTree(List<LineSegment> lineSegments, RectangleF mapBounds, int zoomLevel, bool isQuadTreeNode, out int maxZoomLevel)
        {
            if (lineSegments.Count == 0) //does this properly check if empty?
            {
                maxZoomLevel = 0;
                return null;
            }
            MapData rootInfo = new MapData(mapBounds, zoomLevel);
            float splitValue;
            RectangleF leftChild;
            RectangleF rightChild;
            int childrenZoomLevel = zoomLevel;
            if (isQuadTreeNode)
            {
                splitValue = mapBounds.Left + mapBounds.Width / 2;
                leftChild = new RectangleF(mapBounds.X, mapBounds.Y, mapBounds.Width/2, mapBounds.Height);
                rightChild = new RectangleF(splitValue, mapBounds.Y, mapBounds.Width/2, mapBounds.Height);
            }
            else
            {
                splitValue = mapBounds.Top + mapBounds.Height / 2;
                leftChild = new RectangleF(mapBounds.X, mapBounds.Y, mapBounds.Width, mapBounds.Height/2);
                rightChild = new RectangleF(mapBounds.X, splitValue, mapBounds.Width, mapBounds.Height/2);
                if (mapBounds.Height*mapBounds.Width < _zoomThreshold)
                {
                    childrenZoomLevel = zoomLevel + 1; //rewrites in case it is special case.
                }
            }
            List<LineSegment> leftList = new List<LineSegment>();
            List<LineSegment> rightList = new List<LineSegment>();

            foreach (LineSegment segment in lineSegments)
            {
                if (segment.MinZoom <= zoomLevel)
                {
                    rootInfo.Lines.Add(segment);
                }
                else if (segment.End.X <= splitValue)
                {
                    leftList.Add(segment.Reflect());
                }
                else if (segment.Start.X >= splitValue)
                {
                    rightList.Add(segment.Reflect());
                }
                else
                {
                    LineSegment newLeft = new LineSegment();
                    LineSegment newRight = new LineSegment();
                    segment.Split(splitValue, out newLeft, out newRight);
                    leftList.Add(newLeft.Reflect());
                    rightList.Add(newRight.Reflect());
                }
            }
            maxZoomLevel = Math.Max(zoomLevel, childrenZoomLevel);
            BinaryTreeNode<MapData> leftTreeChild = BuildTree(leftList, mapBounds, zoomLevel, isQuadTreeNode, out maxZoomLevel); //these are probably wrong
            BinaryTreeNode<MapData> rightTreeChild = BuildTree(rightList, mapBounds, zoomLevel, isQuadTreeNode, out maxZoomLevel);
            BinaryTreeNode<MapData> root = new BinaryTreeNode<MapData>(new MapData(mapBounds, zoomLevel), leftTreeChild, rightTreeChild);
            return root;
        }

        public static BinaryTreeNode<MapData> Read(string fileName, out int maxZoomLevel)
        {
            List<LineSegment> lineSegments;
            RectangleF rectangle;
            using (StreamReader input = new StreamReader(fileName))
            {
                string line = input.ReadLine();
                ReadFirstLine(line, out float boundX, out float boundY);
                if (boundX <= 0 || boundY <= 0)
                {
                    throw new IOException("Line 1 contains a non-positive value.");
                }

                rectangle = new RectangleF(0, 0, boundX, boundY);
                lineSegments = new List<LineSegment>();

                int currentLine = 2;
                while ((line = input.ReadLine()) != null)
                {
                    ReadLine(line, out float startX, out float startY, out float endX, out float endY, out int color, out float lineWidth, out int zoomLevel);
                    //error checking
                    if (startX > boundX || startY > boundY || endX > boundX || endY > boundY)
                    {
                        throw new IOException("Line " + currentLine + " describes a street that is not within the map bounds.");
                    }
                    else if (lineWidth <= 0)
                    {
                        throw new IOException("line " + currentLine + " contains a non-positive line width.");
                    }
                    else if (zoomLevel < 1)
                    {
                        throw new IOException("Line " + currentLine + " contains a non-positive zoom level.");
                    }
                    if (!_pens.TryGetValue((color, lineWidth), out Pen pen))
                    {
                        pen = new Pen(Color.FromArgb(color)); //if pen not found, then a new pen is created.
                    }
                    lineSegments.Add(new LineSegment(new PointF(startX, startY), new PointF(endX, endY), zoomLevel, pen));
                    currentLine++;
                }
            }
            return BuildTree(lineSegments, rectangle, 0, true, out maxZoomLevel); //fix
            
        }
        /// <summary>
        /// Used to read the first line of a file. 1/2 additional private methods.
        /// </summary>
        /// <param name="s">The line being read.</param>
        /// <param name="x">An out float representing the x valaue.</param>
        /// <param name="y">An out float representing the y valaue.</param>
        private static void ReadFirstLine(string s, out float x, out float y)
        {
            string[] split = s.Split(',');
            x = (float)Convert.ToDouble(split[_startX]);
            y = (float)Convert.ToDouble(split[_startY]);
        }

        /// <summary>
        /// Used to real all lines of a file EXCEPT the first. 2/2 additional private methods.
        /// </summary>
        /// <param name="s">A string representing the line being read.</param>
        /// <param name="startX">An out float representing the (floating-point) x-coordinate of one end of the line segment.</param>
        /// <param name="startY">An out float representing the (floating-point) y-coordinate of this end of the line segment.</param>
        /// <param name="endX">An out float representing the (floating-point) x-coordinate of the other end of the line segment.</param>
        /// <param name="endY">An out float representing the (floating-point) y-coordinate of this end of the line segment.</param>
        /// <param name="color">An out int giving the color of the line segment in ARGB format.</param>
        /// <param name="lineWidth">An out float giving the line width in pixels.</param>
        /// <param name="zoomLevel">An out int giving the lowest zoom level at which this segment should be shown.</param>
        private static void ReadLine(string s, out float startX, out float startY, out float endX, out float endY, out int color, out float lineWidth, out int zoomLevel)
        {
            string[] split = s.Split(',');
            startX = (float)Convert.ToDouble(split[_startX]);
            startY = (float)Convert.ToDouble(split[_startY]);
            endX = (float)Convert.ToDouble(split[_endX]);
            endY = (float)Convert.ToDouble(split[_endY]);
            color = Convert.ToInt32(split[_color]);
            lineWidth = (float)Convert.ToDouble(split[_lineWidth]);
            zoomLevel = Convert.ToInt32(split[_zoomLevel]);
        }

        /// <summary>
        /// Method to draw a portion of the data in a quad tree.
        /// </summary>
        /// <param name="quadTree">A BinaryTreeNode<MapData> giving the quad tree.</param>
        /// <param name="graphics">A Graphics giving the graphics context on which to draw.</param>
        /// <param name="zoomLevel">An int giving the zoom level currently being displayed.</param>
        /// <param name="currentScale">A float giving the current scale factor by which the map data must be multiplied to obtain pixel coordinates.</param>
        public static void Draw(BinaryTreeNode<MapData> quadTree, Graphics graphics, int zoomLevel, float currentScale)
        {
            if (quadTree != null)
            {
                float convertedX = quadTree.Data.Bounds.X * currentScale;
                float convertedY = quadTree.Data.Bounds.Y * currentScale;
                float convertedWidth = quadTree.Data.Bounds.Width * currentScale;
                float convertedHeight = quadTree.Data.Bounds.Height * currentScale;
                RectangleF convertedRectangle = new RectangleF(convertedX, convertedY, convertedWidth, convertedHeight);
                if (convertedRectangle.IntersectsWith(graphics.ClipBounds) && quadTree.Data.Zoom <= zoomLevel)
                {
                    foreach(LineSegment ls in quadTree.Data.Lines)
                    {
                        ls.Draw(graphics, currentScale);
                    }
                    Draw(quadTree.LeftChild, graphics, zoomLevel, currentScale);
                    Draw(quadTree.RightChild, graphics, zoomLevel, currentScale);
                }
            }
        }

    }
}
