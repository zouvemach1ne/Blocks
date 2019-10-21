using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Threading;

namespace Blocks
{
    public class Connection
    {
        Pen pen = new Pen(Color.Black);
        Font font = new Font("Calibri", 8);
        Brush brush = new SolidBrush(Color.Black);
        Graphics graphics;
        Point[] LinePoints;
        public CommonBlock fromBlock;
        public CommonBlock toBlock;

        PathData path;

        Point Point1;
        Point Point2;

        Point Point3;
        Point Point4;

        public string fromName;
        public string toName;
        public object from;

        public int slot { get; set; }  // slot = 1: Input , slot = 2: Output
        public int init_x;
        public int init_y;

        public bool isPartial = true;
        public bool Selected = false;

        // THE SELECTION WILL BE CREATED AS A PARTIAL SELECTION WHEN ONE CLICKS AT THE CONNECTION BOX OF A BLOCK AND WILL HAVE AN SLOT REPRESENTING IF IT IS AN OUTPUT OR INPUT OF THIS BLOCK. 
        // THE SELECTION WILL BE COMPLETED IF THE USER STOP HOLDING THE MOUSE ON TOP OF ANOTHER CONNECTION WITH ANOTHER SLOT NUMBER SIGNIFYING THAT THE CONNECTION SHOULD BE CREATED WITH THESE TWO BLOCKS.
        // THE SELECTION WILL AFTER CREATION, RAISE AN METHOD ON FORM1 TO CHECK FOR OTHER CONNECTION POINTS ON FORM1(CHECKFORCOMPLETECON) AFTER IT IS CONNECTED, IT WILL PASS THE NAME OF THE MISSING BLOCK
        // TO THE OTHER BLOCK.


        public Connection(CommonBlock from, CommonBlock to)
        {
             this.fromName = from.Name;
             this.toName = to.Name;
             this.fromBlock = from;

            //Point FromPoint = new Point(from.outSlot)
        }

        public Connection(int x, int y,CommonBlock from, int slot)
        {
            this.fromName = from.Name;
            this.fromBlock = from;
            this.init_x = x;
            this.init_y = y;
            this.slot = slot;

            Point Point1 = new Point (init_x,init_y);
            Point Point2 = new Point (x,y);

            RefreshPoints();

            LinePoints = new Point[] { Point1, Point2 };
        }

        public void Draw(Graphics graph)
        {
            if (this.Selected)
            {
                pen = new Pen(Color.Blue);
                brush = new SolidBrush(Color.Blue);
            }
            else
            {
                pen = new Pen(Color.Black);
                brush = new SolidBrush(Color.Black);
            }

            graphics = graph;
            graphics.DrawLines(pen, this.LinePoints);
        }

        public void MoveCon(int x, int y, bool mouseclick)
        {
            if (this.isPartial)
            {
                Point1 = new Point(this.init_x, this.init_y);
                Point4 = new Point(x, y);

                this.LinePoints = new Point[] { Point1, Point4 };
            }
            else
            {
                if (slot == 1)
                {
                    Point2 = new Point(x, fromBlock.Y);
                    Point3 = new Point(x, toBlock.Y);
                }
                else
                {
                    Point2 = new Point(x, toBlock.Y);
                    Point3 = new Point(x, fromBlock.Y);
                }
                
            }
        }

        public void RefreshPoints()
        {
            if (toBlock != null)
            {
                var mid_x = (fromBlock.X - toBlock.X) / 2;
                if (slot == 1) // it started as an input
                {
                    Point1 = new Point(fromBlock.X, fromBlock.Y + (fromBlock.BlockHeight / 2));                 
                    Point4 = new Point(toBlock.X + toBlock.BlockWidth, toBlock.Y + (toBlock.BlockHeight / 2));
                }
                else
                {
                    Point1 = new Point(toBlock.X, toBlock.Y + (toBlock.BlockHeight / 2));
                    Point4 = new Point(fromBlock.X + fromBlock.BlockWidth, fromBlock.Y + (fromBlock.BlockHeight / 2));
                }
                this.LinePoints = new Point[] { Point1, Point2, Point3, Point4 };                
            }
        }

        public bool IsOnLine(Point p, int width)
        {
            var isOnLine = false;
            using (var path = new GraphicsPath())
            {
                using (var pen = new Pen(Brushes.Black, width))
                {
                    path.AddLine(Point1, Point2);
                    path.AddLine(Point2, Point3);
                    path.AddLine(Point3, Point4);
                    isOnLine = path.IsOutlineVisible(p, pen);
                }
            }
            return isOnLine;
        }

        public void CompleteCon(CommonBlock toBlock)
        {
            this.toBlock = toBlock;

            var dif_y = (fromBlock.Y + fromBlock.BlockHeight / 2) - (toBlock.Y + toBlock.BlockHeight / 2); // if fromBlock is above toBlock, dif_y will be negative
            var Yf = fromBlock.Y + fromBlock.BlockHeight / 2; // if the slot == 1, its coming from a block input to an block output slot
            var Yt = toBlock.Y + toBlock.BlockHeight / 2;

            if (slot == 1) // it started as an output
            {
                var Xf = fromBlock.X;
                var Xt = toBlock.X + toBlock.BlockWidth;
                var dif_x = (Xf - Xt) / 2;

                Point1 = new Point(fromBlock.X, fromBlock.Y + (fromBlock.BlockHeight/2));
                Point2 = new Point(fromBlock.X - dif_x, Yf);
                Point3 = new Point(fromBlock.X - dif_x, Yt);
                Point4 = new Point(toBlock.X + toBlock.BlockWidth, toBlock.Y + (toBlock.BlockHeight / 2));
            }
            else
            {
                var Xf = fromBlock.X + toBlock.BlockWidth;
                var Xt = toBlock.X;
                var dif_x = (Xt - Xf) / 2;

                Point1 = new Point(toBlock.X, toBlock.Y + (toBlock.BlockHeight / 2));
                Point2 = new Point(Xf + dif_x, Yt);
                Point3 = new Point(Xf + dif_x, Yf);
                Point4 = new Point(fromBlock.X + fromBlock.BlockWidth, fromBlock.Y + (fromBlock.BlockHeight / 2));
            }
            
            this.fromBlock.Outputs.Add(toBlock.Name);
            this.toBlock.Input = this.fromBlock.Name;
            LinePoints = new Point[] { Point1, Point2, Point3, Point4 };
        }

    }
}
