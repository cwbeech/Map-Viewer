/* UserInterface.cs
 * Author: Calvin Beechner
 */
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
        /// Updates the "Enabled" property of the "zoom in"/"zoom out" buttons.
        /// </summary>
        /// <param name="maxZoomLevel">An int representing the maximum zoom level.</param>
        private void UpdateZoomButtons(int maxZoomLevel)
        {
            if (map1.ZoomLevel != maxZoomLevel)
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
            if (uxOpenFileDialog1.ShowDialog() == DialogResult.OK)
            {
                map1.QuadTreeLocal = QuadTree.Read(uxOpenFileDialog1.FileName, out int maxZoomLevel);
                //map1.ZoomLevel = maxZoomLevel;
                uxFlowLayoutPanel1.AutoScrollPosition = new Point(0, 0);
                UpdateZoomButtons(maxZoomLevel);
            }
        }

        /// <summary>
        /// Event handler for the "Zoom in" button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uxZoomInToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
