/* LineSegment.cs
 * Author: Calvin Beechner
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Ksu.Cis300.MapViewer
{
    /// <summary>
    /// LineSegment Structure. Used to represent an immutable line segment within the map.
    /// </summary>
    public struct LineSegment
    {
        /// <summary>
        /// Gets a PointF giving the starting point of the segment.
        /// </summary>
        public PointF Start { get; }

        /// <summary>
        /// Gets a PointF giving the ending point of the segment.
        /// </summary>
        public PointF End { get; }

        /// <summary>
        /// Gets an int giving the minimum zoom level at which this line implementation will be visible.
        /// </summary>
        public int MinZoom { get; }

        /// <summary>
        /// Pen field used to store the pen used to draw the line.
        /// </summary>
        private Pen _pen;

        /// <summary>
        /// LineSegment constructor. Used for creating a new LineSegment.
        /// </summary>
        /// <param name="point1">A PointF used to be set to Start/End.</param>
        /// <param name="point2">A PointF used to be set to Start/End.</param>
        /// <param name="minZoom">An int used to be set to MinZoom.</param>
        /// <param name="pen">A Pen used to be set to LineDrawer.</param>
        public LineSegment(PointF point1, PointF point2, int minZoom, Pen pen)
        {
            if (point1.X <= point2.X)
            {
                Start = point1;
                End = point2;
            }
            else
            {
                Start = point2;
                End = point1;
            }
            MinZoom = minZoom;
            _pen = pen;
        }

        /// <summary>
        /// Produces the reflection about the main diagonal.
        /// </summary>
        /// <returns>Returns a LineSegment describing the reflection.</returns>
        public LineSegment Reflect()
        {
            return new LineSegment(new PointF(Start.Y, Start.X), new PointF(End.Y, End.X), MinZoom, _pen);
        }

        /// <summary>
        /// Splits the line segment at a given x-coordinate.
        /// </summary>
        /// <param name="x">A float giving the x-coordinate at which to split the line segment.</param>
        /// <param name="left">An out LineSegment through which to return the left portion of the split segment.</param>
        /// <param name="right">An out LineSegment through which to return the right portion of the split segment.</param>
        public void Split(float x, out LineSegment left, out LineSegment right)
        {
            float slope = (End.Y - Start.Y) / (End.X - Start.X);
            float verticalShift = Start.Y - (slope * Start.X); //is this necessary?
            float p = (slope * x) + verticalShift;
            PointF splitPoint = new PointF(x, p);
            left = new LineSegment(Start, splitPoint, MinZoom, _pen);
            right = new LineSegment(splitPoint, End, MinZoom, _pen);
        }

        /// <summary>
        /// Draws the line segment.
        /// </summary>
        /// <param name="g">A Graphics giving the graphics context on which to draw the line segment.</param>
        /// <param name="scale">A float giving the scale factor to use for drawing.</param>
        public void Draw(Graphics g, float scale)
        {
            g.DrawLine(_pen, Start.X * scale, Start.Y * scale, End.X * scale, End.Y * scale);
        }
    }

}
