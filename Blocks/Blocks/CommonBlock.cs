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
    public class CommonBlock
    {

        Pen pen = new Pen(Color.Black);
        Font font = new Font("Calibri", 8);
        Brush brush = new SolidBrush(Color.Black);
        Graphics graphics;
        Form origem;

        public int CtrlzCount = -1;

        public int BlockWidth = 100;
        public int BlockHeight = 50;


        public string Name { get; set; }
        public string Input { get; set; }
        public List<string> Outputs = new List<string>() ;
        public string Command;

        public bool ANATEM;
        public bool ORGANON;
        public bool Selected  = false;
        public bool CanSelect = false;
        public bool CanResize = false;
        public bool CanMove   = false;


        public int X { get; set; }
        public int Y { get; set; }
        public int NumberOfConnections;
        public int CollisionBox;
        public int NumberOfParams = 11;

        public int pos_X;
        public int pos_Y;

        

        public CommonBlock(Form PanelDiagram, int init_X, int init_Y)
        {
            origem = PanelDiagram;
            this.X = init_X;
            this.Y = init_Y;
            Name = "LFPP";
            Input = "NULL";            //null;
            Command = "LFPP";
            ANATEM = true;
            ORGANON = false;
            //NumberOfConnections = 1;
        }

        public void Draw(Graphics graph)
        {

            graphics = graph;
            Rectangle rect = new Rectangle(this.X, this.Y, BlockWidth, BlockHeight);
            Rectangle inSlot = new Rectangle(this.X - 6 - 1, this.Y + BlockHeight / 2 - 3, 6, 6);
            Rectangle outSlot = new Rectangle(this.X + BlockWidth, this.Y + BlockHeight / 2 - 3, 6, 6);

            Point[] outTriangle = new Point[] { new Point(this.X + BlockWidth, (this.Y + BlockHeight / 2) - 3), new Point(this.X + BlockWidth + 6, (this.Y + BlockHeight / 2)), new Point(this.X + BlockWidth, (this.Y + BlockHeight / 2) + 3) };
            graphics.DrawPolygon(pen, outTriangle );
            graphics.FillPolygon(brush, outTriangle);

            Point[] inTriangle = new Point[] { new Point(this.X - 6, (this.Y + BlockHeight / 2) - 3), new Point(this.X, (this.Y + BlockHeight / 2)), new Point(this.X - 6, (this.Y + BlockHeight / 2) + 3) };
            

            if (this.Selected)
            {
                pen = new Pen(Color.Blue);
                brush = new SolidBrush(Color.Blue);
                int mini_rect_width = 6;
                int mini_rect_height = 6;
                Size mini_rect_size = new Size(mini_rect_width, mini_rect_height);
                Point Point1 = new Point(this.X - mini_rect_width / 2, this.Y - mini_rect_height / 2);                                       // upper left point
                Point Point2 = new Point(this.X - mini_rect_width / 2, this.Y + BlockHeight - mini_rect_height / 2);                         // lower left point
                Point Point3 = new Point(this.X + BlockWidth - mini_rect_width / 2, this.Y + BlockHeight - mini_rect_height / 2);            // lower right point
                Point Point4 = new Point(this.X + BlockWidth - mini_rect_width / 2, this.Y - mini_rect_height / 2);                          // upper right point
                Rectangle mini_rect1 = new Rectangle(Point1, mini_rect_size);
                Rectangle mini_rect2 = new Rectangle(Point2, mini_rect_size);
                Rectangle mini_rect3 = new Rectangle(Point3, mini_rect_size);
                Rectangle mini_rect4 = new Rectangle(Point4, mini_rect_size);

                graphics.FillRectangle(brush, mini_rect1);
                graphics.FillRectangle(brush, mini_rect2);
                graphics.FillRectangle(brush, mini_rect3);
                graphics.FillRectangle(brush, mini_rect4);
            }
            else
            {
                pen = new Pen(Color.Black);
                brush = new SolidBrush(Color.Black);
            }



            var SelectedLabel = string.Format("Selected = {0} ", Selected.ToString());
            var InputName = string.Format("Input = {0}", this.Input);

            if (this.Input == null) { graphics.DrawRectangle(pen, inSlot); } else { graphics.DrawPolygon(pen, inTriangle); graphics.FillPolygon(brush, inTriangle); }
            graphics.DrawRectangle(pen, rect);
            graphics.DrawString(SelectedLabel, font, brush, this.X + 5, this.Y + 20);
            graphics.DrawString(InputName, font, brush, this.X, this.Y + 50);

            
        }



        public void BlockResize(int Diagram_X, int Diagram_Y)
        {
            var mx = Diagram_X;
            var my = Diagram_Y;

            int limit_X = this.X + BlockWidth;
            int limit_Y = this.Y + BlockHeight;

            var min_width = 50;
            var min_height = 50;


            if (mx < this.X + BlockWidth / 2) // mouse is on the left of the block 
            {
                if (my < this.Y + BlockHeight / 2) // mouse is on top left of block 
                {
                    if (BlockWidth > min_width || mx < this.X) 
                    {
                        this.X = mx;
                        BlockWidth = limit_X - this.X;
                    }
                    if (BlockHeight > min_height || my < this.Y)
                    {
                        this.Y = my;
                        BlockHeight = limit_Y - this.Y; 
                    }
                    
                }
                else                               // mouse is on botton left of block
                {
                    if (BlockWidth > min_width|| mx < this.X)
                    {
                        this.X = mx;
                        BlockWidth = limit_X - this.X;
                    }
                    if (BlockHeight > min_height || my > this.Y + BlockHeight)
                    {
                        BlockHeight += my - limit_Y;
                    }
                }
            } 
            else if (mx > this.X + BlockWidth / 2)
            {
                if (my < this.Y + BlockHeight / 2) // mouse is on top right of block 
                {
                    if (BlockWidth > min_width || mx > this.X + BlockWidth)
                    {
                        BlockWidth += mx - limit_X;
                    }
                    if (BlockHeight > min_height || my < this.Y)
                    {
                        this.Y = my;
                        BlockHeight = limit_Y - this.Y;
                    }
                }
                else
                {
                    if (BlockWidth > min_width || mx > this.X + BlockWidth)
                    {
                        BlockWidth += mx - limit_X;
                    }
                    if (BlockHeight > min_height || my > this.Y + BlockHeight)
                    {
                        BlockHeight += my - limit_Y;
                    }
                }
            }
            if (BlockWidth < min_width) { BlockWidth = min_width; }
            if (BlockHeight < min_height) { BlockHeight = min_height; }
        }

        public bool MiniRectCollisionCheck(int mx, int my)
        {
            pen = new Pen(Color.Blue);
            brush = new SolidBrush(Color.Blue);
            int mini_rect_width = 6;
            int mini_rect_height = 6;
            Size mini_rect_size = new Size(mini_rect_width, mini_rect_height);
            Point Point1 = new Point(this.X - mini_rect_width / 2, this.Y - mini_rect_height / 2);                                       // upper left point
            Point Point2 = new Point(this.X - mini_rect_width / 2, this.Y + BlockHeight - mini_rect_height / 2);                         // lower left point
            Point Point3 = new Point(this.X + BlockWidth - mini_rect_width / 2, this.Y + BlockHeight - mini_rect_height / 2);            // lower right point
            Point Point4 = new Point(this.X + BlockWidth - mini_rect_width / 2, this.Y - mini_rect_height / 2);                          // upper right point
            Rectangle mini_rect1 = new Rectangle(Point1, mini_rect_size);
            Rectangle mini_rect2 = new Rectangle(Point2, mini_rect_size);
            Rectangle mini_rect3 = new Rectangle(Point3, mini_rect_size);
            Rectangle mini_rect4 = new Rectangle(Point4, mini_rect_size);

            if( mini_rect1.Contains(new Point(mx, my)))
            {
                return true;
            }
            else if (mini_rect2.Contains(new Point(mx, my)))
            {
                return true;
            }
            else if (mini_rect3.Contains(new Point(mx, my)))
            {
                return true;
            }
            else if (mini_rect4.Contains(new Point(mx, my)))
            {
                return true;
            }
            else return false;
        }

        public void BlockMove(MouseEventArgs e)
        {
                this.X = e.X - pos_X;
                this.Y = e.Y - pos_Y;
        }

        public void IsInBlock()
        {
            int limit_X = this.X + BlockWidth;
            int limit_Y = this.Y + BlockHeight;

            if (Selected) // a little increase in hitbox size to compensate the mini boxes on borders
            {
                if (AvailableBlocks.Diagram_X >= this.X-3 && AvailableBlocks.Diagram_X <= limit_X+3 && AvailableBlocks.Diagram_Y >= this.Y-3 && AvailableBlocks.Diagram_Y <= limit_Y+3)
                {
                    pos_X = AvailableBlocks.Diagram_X - this.X;
                    pos_Y = AvailableBlocks.Diagram_Y - this.Y;
                    this.CanSelect = true;
                }
                else
                {
                    this.CanSelect = false;
                }
            }
            else
            {
                if (AvailableBlocks.Diagram_X >= this.X && AvailableBlocks.Diagram_X <= limit_X && AvailableBlocks.Diagram_Y >= this.Y && AvailableBlocks.Diagram_Y <= limit_Y)
                {
                    pos_X = AvailableBlocks.Diagram_X - this.X;
                    pos_Y = AvailableBlocks.Diagram_Y - this.Y;
                    this.CanSelect = true;
                }
                else
                {
                    this.CanSelect = false;
                }
            }


        }

        public void BlockUndoState(object ThisState)
        {
            object StateHolderrr = ThisState;           
            var StateHolderr = StateHolderrr as object[];

            this.X = (int)(StateHolderr[0]);
            this.Y = (int)(StateHolderr[1]);
            this.Name = StateHolderr[2].ToString();
            this.Input = StateHolderr[3].ToString();
            this.Outputs = StateHolderr[4] as List<string>;
            this.ANATEM = (bool)StateHolderr[5];
            this.ORGANON = (bool)StateHolderr[6];
            this.Command = StateHolderr[7].ToString();
            this.NumberOfConnections = (int)StateHolderr[8];
            this.BlockWidth = (int)StateHolderr[9];
            this.BlockHeight = (int)StateHolderr[10];   
        }

        public object[] BlockHoldState()
        {
            this.CtrlzCount++;
            object[] StateHolder = new object[NumberOfParams];

            StateHolder[0] = this.X;
            StateHolder[1] = this.Y;
            StateHolder[2] = this.Name;
            StateHolder[3] = this.Input;
            StateHolder[4] = this.Outputs;
            StateHolder[5] = this.ANATEM;
            StateHolder[6] = this.ORGANON;
            StateHolder[7] = this.Command;
            StateHolder[8] = this.NumberOfConnections;
            StateHolder[9] = this.BlockWidth;
            StateHolder[10] = this.BlockHeight;

            return StateHolder;
        }

        public int CheckCompleteConn(int x, int y, bool mouseDown)
        {
            Rectangle inSlot = new Rectangle(this.X - 6 - 1, this.Y + BlockHeight / 2 - 3, 6, 6);
            Rectangle outSlot = new Rectangle(this.X + BlockWidth, this.Y + BlockHeight / 2 - 3, 6, 6);

            if (inSlot.Contains(new Point(x, y)))
            {
                return 1;
            }
            if (outSlot.Contains(new Point(x, y)))
            {
                return 2;
            }
            return 0;
        }

        public void CheckNewConnection(int x, int y, bool mouseDown)
        {
            Rectangle inSlot = new Rectangle(this.X - 6 - 1, this.Y + BlockHeight / 2 - 3, 6, 6);
            Rectangle outSlot = new Rectangle(this.X + BlockWidth, this.Y + BlockHeight / 2 - 3, 6, 6);
 
             if (inSlot.Contains(new Point(x, y)))
             {
                 AvailableBlocks.ProgramConnections[AvailableBlocks.NumberOfConnections] = new Connection(AvailableBlocks.Diagram_X, AvailableBlocks.Diagram_Y, this, 1);
                 AvailableBlocks.NumberOfConnections++;
                 NumberOfConnections++;
             }
             if (outSlot.Contains(new Point(x, y)))
             {
                 AvailableBlocks.ProgramConnections[AvailableBlocks.NumberOfConnections] = new Connection(AvailableBlocks.Diagram_X, AvailableBlocks.Diagram_Y, this, 2);
                 AvailableBlocks.NumberOfConnections++;
                 //NumberOfConnections++;
             }

            
            
        }


    }
}
