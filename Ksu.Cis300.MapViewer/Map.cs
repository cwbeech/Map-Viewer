/* Map.cs
 * Author: Calvin Beechner
 */
using Ksu.Cis300.ImmutableBinaryTrees;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ksu.Cis300.MapViewer
{
    /// <summary>
    /// Map class. Used to create a Map object.
    /// </summary>
    public partial class Map : UserControl
    {
        /// <summary>
        /// A const float giving the scale factor at zoom level 0.
        /// </summary>
        private const float _scaleAtZero = 5;

        /// <summary>
        /// A const int giving the width and height of the Map when the quad tree is null.
        /// </summary>
        private const int _widthAndHeight = 150;

        /// <summary>
        /// A float storing the current scale factor.
        /// </summary>
        private float _currentScaleFactor;

        /// <summary>
        /// An int storing the current zoom level.
        /// </summary>
        private int _zoomLevel;

        /// <summary>
        /// A BinaryTreeNode<MapData> storing the quad tree containing the map data.
        /// </summary>
        private BinaryTreeNode<MapData> _quadTree;

        /// <summary>
        /// Property to get and set the zoom level.
        /// </summary>
        public int ZoomLevel
        {
            get => _zoomLevel;
            set
            {
                _zoomLevel = value;
                _currentScaleFactor = (float)Math.Pow(2, _zoomLevel) * _scaleAtZero;
                if (_quadTree == null)
                {
                    Width = _widthAndHeight;
                    Height = _widthAndHeight;
                }
                else
                {
                    SetWidthAndHeight();
                    Invalidate();
                }
            }

        }

        /// <summary>
        /// Sets Width and Height to their respective values.
        /// </summary>
        public void SetWidthAndHeight()
        {
            Width = (int)(_quadTree.Data.Bounds.Width * _currentScaleFactor) + 1;
            Height = (int)(_quadTree.Data.Bounds.Height * _currentScaleFactor) + 1;
        }

        /// <summary>
        /// Property to get or set the quad tree.
        /// </summary>
        public BinaryTreeNode<MapData> QuadTreeLocal
        {
            get => _quadTree;
            set
            {
                _quadTree = value;
                ZoomLevel = 1;
            }
        }

        /// <summary>
        /// Map constructor.
        /// </summary>
        public Map()
        {
            InitializeComponent();
            ZoomLevel = 1;
        }

        /// <summary>
        /// Override of OnPaint method.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Rectangle rectangle = e.ClipRectangle;
            Graphics graphics = e.Graphics;
            if (graphics != null)
            {
                graphics.Clip = new Region(rectangle);
                QuadTree.Draw(QuadTreeLocal, graphics, ZoomLevel, _currentScaleFactor);
            }
        }
    }
}