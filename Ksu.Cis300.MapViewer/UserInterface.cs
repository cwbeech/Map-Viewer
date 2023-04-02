/* UserInterface.cs
 * Author: Calvin Beechner
 */
using KansasStateUniversity.TreeViewer2;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ksu.Cis300.MapViewer
{
    /// <summary>
    /// UserInterface class. Used for displaying the user interface to the user.
    /// </summary>
    public partial class UserInterface : Form
    {
        /// <summary>
        /// An int representing the maximum zoom level of any node in the quad tree.
        /// </summary>
        private int _maxZoomLevel;

        /// <summary>
        /// UserInterface constructor.
        /// </summary>
        public UserInterface()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Updates the "Enabled" property of the "zoom in"/"zoom out" buttons. 1/1 additional methods required.
        /// </summary>
        /// <param name="maxZoomLevel">An int representing the maximum zoom level.</param>
        private void UpdateZoomButtons()
        {
            if (map1.ZoomLevel != _maxZoomLevel)
            {
                uxZoomInToolStripMenuItem.Enabled = true;
            }
            else
            {
                uxZoomInToolStripMenuItem.Enabled = false;
            }
            if (map1.ZoomLevel != 1)
            {
                uxZoomOutToolStripMenuItem.Enabled = true;
            }
            else
            {
                uxZoomOutToolStripMenuItem.Enabled = false;
            }
        }

        /// <summary>
        /// Event handler for the "Open File" button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uxOpenFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (uxOpenFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    map1.QuadTreeLocal = QuadTree.Read(uxOpenFileDialog1.FileName, out int maxZoomLevel);
                    uxFlowLayoutPanel1.AutoScrollPosition = new Point(0, 0);
                    _maxZoomLevel = maxZoomLevel;
                    UpdateZoomButtons();

                    /* Used for testing
                    TreeForm treeForm = new TreeForm(map1.QuadTreeLocal, 100);
                    treeForm.Show();
                    */
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Event handler for the "Zoom in" button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uxZoomInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Point upperLeft = uxFlowLayoutPanel1.AutoScrollPosition;
            upperLeft.X = -1 * upperLeft.X;
            upperLeft.Y = -1 * upperLeft.Y;
            Size visiblePortion = uxFlowLayoutPanel1.ClientSize;
            map1.ZoomLevel = map1.ZoomLevel + 1; //zoom in

            Point oldCenter = new Point(upperLeft.X + visiblePortion.Width / 2, upperLeft.Y + visiblePortion.Height / 2);
            uxFlowLayoutPanel1.AutoScrollPosition = new Point(upperLeft.X * 2 + visiblePortion.Width / 2, upperLeft.Y * 2 + visiblePortion.Height / 2); //zoom in
            UpdateZoomButtons();
        }

        /// <summary>
        /// Event handler for the "Zoom out" button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uxZoomOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Point upperLeft = uxFlowLayoutPanel1.AutoScrollPosition;
            upperLeft.X = -1 * upperLeft.X;
            upperLeft.Y = -1 * upperLeft.Y;
            Size visiblePortion = uxFlowLayoutPanel1.ClientSize;
            map1.ZoomLevel = map1.ZoomLevel - 1; //zoom out

            Point oldCenter = new Point(upperLeft.X + visiblePortion.Width / 2, upperLeft.Y + visiblePortion.Height / 2);
            uxFlowLayoutPanel1.AutoScrollPosition = new Point(upperLeft.X / 2 - visiblePortion.Width / 4, upperLeft.Y / 2 - visiblePortion.Height / 4); //zoom in
            UpdateZoomButtons();
        }
    }
}
