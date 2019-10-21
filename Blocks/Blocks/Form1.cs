using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Media;
using System.Drawing.Drawing2D;



namespace Blocks
{
    public partial class AvailableBlocks : Form
    {
        public AvailableBlocks()
        {
            InitializeComponent();

            PanelDiagram.MouseMove += new MouseEventHandler(this.PanelDiagram_MouseMove);

            PanelDiagram.MouseWheel += new MouseEventHandler(PanelDiagram_MouseWheel);

            this.KeyDown += new KeyEventHandler(PanelDiagram_KeyDown);

            this.KeyUp += new KeyEventHandler(PanelDiagram_KeyUp);

            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);


            ProgramBlocks = new CommonBlock[MaxBlocks];
            ProgramConnections = new Connection[MaxConnections];

        }



        public static CommonBlock[] ProgramBlocks = new CommonBlock[MaxBlocks];
        public static Connection[] ProgramConnections = new Connection[MaxConnections];

        /// Graphics
        Pen pen = new Pen(Color.Black);
        Brush brush = new SolidBrush(Color.Black);
        Font font = new Font("Calibri", 8);


        /// Booleans
        public static bool ctrlKeyDown;
        public static bool zKeyDown;
        public static bool mouseClick;
        public static bool mouseDown;
        public static bool mouseUp;
        public static bool CanResize;

        /// Floats
        public static float CurrentZoom = 1.0f;
        public static float ZoomFactor = 1.0f;


        /// Integers
        public static int MaxBlocks = 100;
        public static int MaxConnections = 400;
        public static int NumberOfBlocks = 0;
        public static int NumberOfConnections = 0;
        public static int Zoom = 2;
        public static int PanelSize = 5000;
        public static int Panel_X = 0;
        public static int Panel_Y = 0;
        public static int Diagram_X {get; set;}
        public static int Diagram_Y {get; set;}
        public static int MaxStateHold = 30;
        public static int CurrentState = 0; 

        
        /// Floats
        public static float PanelZoom;

        /// Arrays
        public static float[] ZoomScale = { 0.5f, 0.75f, 1.0f, 1.25f, 1.5f, 2.0f };

        /// Longs


        /// Objects
        object[,] BlocksStates = new object[MaxBlocks, MaxStateHold];


        /// ============================================================ \\\
        
        public void PanelDiagram_Paint(object sender, PaintEventArgs e)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            PanelZoom = ZoomScale[Zoom];

            //e.Graphics.SmoothingMode = SmoothingMode.HighSpeed;

            Graphics graphics = e.Graphics;

            graphics.Clear(Color.White);

            Matrix matrix = new Matrix();
            e.Graphics.ScaleTransform(PanelZoom, PanelZoom, MatrixOrder.Append);

            DrawBlocks(e.Graphics);
            DrawConnections(e.Graphics);
            sw.Stop();

            matrix.Dispose();


            
        }

        public void PanelDiagram_MouseMove(object sender, MouseEventArgs e)
        {
            Diagram_X = e.X;
            Diagram_Y = e.Y;
            
            for (int i = 0; i < NumberOfBlocks; i++)
            {
                if (ProgramBlocks[i].Selected == true && mouseDown)
                {
                    if (ProgramBlocks[i].CanResize == false)
                    {
                        ProgramBlocks[i].BlockMove(e);
                    }
                    else
                    {
                        ProgramBlocks[i].BlockResize(e.X,e.Y);
                    }
                    
                }
               
            }
            ToolBarCoord.Refresh();

            for (int i = 0; i < NumberOfConnections; i++)
            {
                if (ProgramConnections[i].isPartial && mouseDown)
                {
                    ProgramConnections[i].MoveCon(e.X, e.Y, mouseClick);
                    ProgramConnections[i].RefreshPoints();                   
                }
                if (ProgramConnections[i].toBlock != null)
                {
                    ProgramConnections[i].MoveCon(e.X, e.Y, mouseClick);
                    ProgramConnections[i].RefreshPoints();
                }

                

            }
            PanelDiagram.Invalidate();
        }

        public void PanelDiagram_KeyDown(object sender, KeyEventArgs e)
        {
            ctrlKeyDown = e.Control;
            if (e.KeyCode == Keys.Z && e.Control)
            {                
                zKeyDown = true;
                BlocksAllUndoState();
                PanelDiagram.Refresh();
            }            
        }

        public void PanelDiagram_KeyUp(object sender, KeyEventArgs e)
        {
            ctrlKeyDown = e.Control;
        }

        public void PanelDiagram_MouseWheel(object sender, MouseEventArgs e)
        {
            //bool IsGoUp = e.Delta > 0 ? true : false;
            ////ScrollPanel.AutoScroll = false;
            //
            //
            ////Console.WriteLine(e.Delta);
            //if (this.ctrlKeyDown)
            //{
            //    
            //    ScrollTimer++;
            //    if (IsGoUp && ScrollTimer > 50) 
            //    {
            //        //ScrollPanel.AutoScroll = false;
            //        ZoomIn();
            //        ScrollPanel.AutoScrollOffset = new Point(0,0);
            //        ScrollPanel.AutoScrollPosition = new Point(0, 0);
            //        ScrollTimer = 0;
            //        ScrollPanel.AutoScroll = true;
            //        
            //    }
            //    if (!IsGoUp && ScrollTimer > 50)
            //    {
            //        //ScrollPanel.AutoScroll = false;
            //        ZoomOut();
            //        ScrollPanel.AutoScrollOffset = new Point(0, 0);
            //        ScrollPanel.VerticalScroll.Value = 0;
            //        ScrollPanel.HorizontalScroll.Value = 0;
            //        ScrollTimer = 0;
            //
            //        ScrollPanel.AutoScroll = true;
            //        
            //    }
            //}
            //
            ////ScrollPanel.AutoScroll = true;
        }

        private void PanelDiagram_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {


        }

        private void PanelDiagram_Scroll(object sender, ScrollEventArgs e)
        {

        }

        private void PanelDiagram_LocationChanged(object sender, EventArgs e)
        {

        }

        private void PanelDiagram_Click(object sender, EventArgs e)
        {
        }

        private void PanelDiagram_MouseDown(object sender, MouseEventArgs e)
        {
            CheckBlockCollision();
            mouseClick = true;
            mouseDown = true;
            CheckConnCollision();
            CheckBlockResize();           
            PanelDiagram.Refresh();
        }

        private void PanelDiagram_MouseUp(object sender, MouseEventArgs e)
        {
            mouseClick = false;
            mouseDown = false;
            //CheckBlockCollision();
            GetAllStates();
            PanelDiagram.Refresh();
            for (int i = 0; i < NumberOfBlocks; i++)
            {
                ProgramBlocks[i].CanResize = false;
            }
            CheckForCompleteConn();
            CheckForPartialConns();
            
        }

        public void CheckForCompleteConn()
        {
            for (int i = 0; i < NumberOfBlocks; i++)
            {
                var slot = ProgramBlocks[i].CheckCompleteConn(Diagram_X, Diagram_Y, mouseDown);
                {
                    for (int ii = 0; ii < NumberOfConnections; ii++)
                    {
                        if (ProgramConnections[ii].isPartial == true)
                        {
                            if (ProgramConnections[ii].slot != slot && slot != 0)
                            {
                                ProgramConnections[ii].isPartial = false;
                                ProgramConnections[ii].CompleteCon(ProgramBlocks[i]);
                                //NumberOfConnections++;
                            }
                        }
                    }
                }
            }
        }

        public void CheckForPartialConns()
        {
            for (int i = 0; i < NumberOfConnections; i++)
            {
                if (ProgramConnections[i].isPartial)
                {
                    var HOLDER = ProgramConnections.ToList();
                    HOLDER.RemoveAt(i);
                    HOLDER.Add(null);
                    ProgramConnections = HOLDER.ToArray();
                    NumberOfConnections--;
                }
            }
        }

        /// ============================================================== \\\
 

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewProject();
        }

        private void zoomOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ZoomOut();
        }

        private void zoomInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ZoomIn();
        }

        private void Blocks_Load(object sender, EventArgs e)
        {
            typeof(Panel).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance
            | BindingFlags.NonPublic, null, PanelDiagram, new object[] { true });

        }

        private void Coordinates_Paint(object sender, PaintEventArgs e)
        {
            Coordinates.Text = string.Format("Diagram ({0} , {1})", Diagram_X, Diagram_Y);
        }

        private void BlocksNumber_Paint(object sender, PaintEventArgs e)
        {
            BlocksNumber.Text = string.Format("Blocks = {0}", NumberOfBlocks);
        }

        private void ToolBar_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void ScrollPanel_MouseMove(object sender, MouseEventArgs e)
        {
 
        }

        private void blocksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BlocksWindow command = new BlocksWindow();
            command.Show();
        }


        /// <summary>
        /// Passar methodos para baixo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        public void BlocksAllUndoState()
        {
            if (CurrentState > 0) // Program has Started and has at least 1 block
            {
                CurrentState--;
                // First check if there is any block that didnt exist in previous state to delete it.
                for (int i = 0; i < NumberOfBlocks; i++)
                {
                    if (BlocksStates[i, CurrentState] == null)
                    {
                        ProgramBlocks[i] = null;
                        NumberOfBlocks--;
                        PanelDiagram.Invalidate();
                        //CurrentState--;
                    }
                }
                // Then, proceed to previous state
                for (int i = 0; i < NumberOfBlocks; i++)
                {
                    ProgramBlocks[i].BlockUndoState(BlocksStates[i, CurrentState]);
                }
            }
        }

        public void GetAllStates()
        {
            if (CurrentState < MaxStateHold - 1) // If initializing states, it will only add to state holder
            {
                CurrentState++;
                for (int i = 0; i < NumberOfBlocks; i++)
                {
                    ProgramBlocks[i].BlockHoldState();
                    BlocksStates[i, CurrentState] = ProgramBlocks[i].BlockHoldState();
                }
            }
            else //  If it already exceeded maxstates, will substitue all states for one next to make room for current state.
            {
                CurrentState = 9;
                for (int i = 0; i < NumberOfBlocks; i++)
                {
                    for (int ii = 0; ii < MaxStateHold - 1; ii++)
                    {
                        BlocksStates[i, ii] = BlocksStates[i, ii + 1];
                    }
                    ProgramBlocks[i].BlockHoldState();
                    BlocksStates[i, CurrentState] = ProgramBlocks[i].BlockHoldState();
                }
            }
        }

        public void CheckConnCollision()
        {

            for (int i = 0; i < NumberOfConnections; i++)
            {
                if (ProgramConnections[i].IsOnLine(new Point(Diagram_X, Diagram_Y), 8))
                {
                    if (mouseClick)
                    {
                        ProgramConnections[i].Selected = true;
                    }
                }
                else
                {
                    if (mouseClick)
                    {
                        ProgramConnections[i].Selected = false;
                    }
                }
            }
        }

        public void CheckBlockResize()
        {
            for (int i = 0; i < NumberOfBlocks; i++)
            {
                ProgramBlocks[i].IsInBlock();
                if (ProgramBlocks[i].CanSelect == true)  // mouse is over that block
                {
                    if (ProgramBlocks[i].MiniRectCollisionCheck(Diagram_X, Diagram_Y)) // mouse is over the mini rectangle
                    {
                        ProgramBlocks[i].CanResize = true;
                    }
                    else
                    {
                        ProgramBlocks[i].CanResize = false;
                    }
                }
            }
        }

        public void CheckBlockCollision()
        {
            for (int i = 0; i < NumberOfBlocks; i++)
            {
                ProgramBlocks[i].CheckNewConnection(Diagram_X, Diagram_Y, mouseDown);
                ProgramBlocks[i].IsInBlock();
                if (ProgramBlocks[i].CanSelect == true)
                {
                    ProgramBlocks[i].Selected = true;
                }
                else
                {
                    if (ProgramBlocks[i].CanResize == false)
                    {
                        ProgramBlocks[i].Selected = false;
                    }
                }
            }
        }


        public void DrawBlocks(Graphics graphics)
        {
            if (ProgramBlocks[0] != null)
            {
                for (int i = 0; i < NumberOfBlocks; i++)
                {
                    ProgramBlocks[i].Draw(graphics);
                }

            }
        }


        public void DrawConnections(Graphics graphics)
        {
            if (ProgramConnections[0] != null)
            {
                for (int i = 0; i < NumberOfConnections; i++)
                {
                    ProgramConnections[i].Draw(graphics);
                }

            }
        }

        public void TestBlock_Click(object sender, EventArgs e)
        {
            ProgramBlocks[NumberOfBlocks] = new CommonBlock(this, Diagram_X, Diagram_Y);
            //BlocksHoldState(ProgramBlocks[NumberOfBlocks]);
            NumberOfBlocks++;
            GetAllStates();

            ToolBarCoord.Refresh();
            PanelDiagram.Invalidate();
        }

        private void NewProject()
        {
            if (PanelDiagram.Visible == false)
            {
                PanelDiagram.Size = new Size(PanelSize, PanelSize);
                PanelDiagram.Visible = true;
                ScrollPanel.Visible = true;
                //PerformAutoScale();
            }
            else
            {
                ProgramBlocks = new CommonBlock[MaxBlocks];
                NumberOfBlocks = 0;
                PanelDiagram.Invalidate();
                PanelDiagram.Controls.Clear();
                PanelDiagram.Refresh();
            }

        }

        private void ZoomOut()
        {

            if (Zoom > 0)
            {
                Zoom--;

                ZoomFactor = ZoomScale[Zoom] / CurrentZoom;
                CurrentZoom = ZoomScale[Zoom];

                PanelDiagram.Scale(ZoomFactor, ZoomFactor);
                PanelDiagram.Location = new Point(ScrollPanel.AutoScrollPosition.X, ScrollPanel.AutoScrollPosition.Y);
                PanelDiagram.Refresh();
            }

        }

        private void ZoomIn()
        {
            if (Zoom < 5)
            {
                Zoom++;

                ZoomFactor = ZoomScale[Zoom] / CurrentZoom;
                CurrentZoom = ZoomScale[Zoom];

                PanelDiagram.Scale(ZoomFactor, ZoomFactor);
                PanelDiagram.Location = new Point(ScrollPanel.AutoScrollPosition.X, ScrollPanel.AutoScrollPosition.Y);
                PanelDiagram.Refresh();
            }

        }

        public static int PanelToDiagram(int x)
        {
            // Esta e a outra função, DiagramToPanel não estão sendo usadas. Talvez possam ser usadas depois com alguns ajustes.
            var Diag_X = (int)(x / ZoomScale[Zoom]);
            return Diag_X;
        }

        public static int DiagramToPanel(int x)
        {
            var Pan_X = (int)(x * ZoomScale[Zoom]);
            return Pan_X;
        }

        private void ScrollPanel_Paint(object sender, PaintEventArgs e)
        {

        }



    }
}
