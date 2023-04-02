/* MapData.cs
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
    /// MapData structure. Describes the portion of th emap at a particular zoom level.
    /// </summary>
    public struct MapData
    {
        /// <summary>
        /// Gets a RectangleF giving the bounds of the portion of the map described by the MapData.
        /// </summary>
        public RectangleF Bounds { get; }

        /// <summary>
        /// Gets an int giving the zoom level for this MapData.
        /// </summary>
        public int Zoom { get; }

        /// <summary>
        /// Gets a List<LineSegment> containing the line segments to be drawn within the bounds and the zoom level of this MapData.
        /// </summary>
        public List<LineSegment> Lines { get; }

        /// <summary>
        /// MapData constructor. Used to create a new MapData.
        /// </summary>
        /// <param name="bounds"></param>
        /// <param name="zoom"></param>
        public MapData(RectangleF bounds, int zoom)
        {
            Bounds = bounds;
            Zoom = zoom;
            Lines = new List<LineSegment>();
        }

        /// <summary>
        /// ToString Override. Used for testing.
        /// </summary>
        /// <returns>Returns a string representing the MapData.</returns>
        public override string ToString()
        {
            //return (Zoom + "; {X = " + Bounds.X + ",Y=" + Bounds.Y + ",Width=" + Bounds.Width + ",Height=" + Bounds.Height + "}");
            string result = "";
            foreach (LineSegment ls in Lines)
            {
                result += ("[" + ls.Start + "," + ls.End + "]");

            }
            return result;
        }
    }
}
