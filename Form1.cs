using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace asgn5v1
{
    /// <summary>
    /// Summary description for Transformer.
    /// </summary>

    public class Transformer : System.Windows.Forms.Form
    {
        private System.ComponentModel.IContainer components;

        // extra data about the shape
        double centerX     = 0;
        double centerY     = 0;
        double centerZ     = 0;
        double shapeWidth  = 0;
        double shapeHeight = 0;
        double shapeDepth  = 0;

        // thread used to continuously rotate the shape
        bool continuouslyRotateX;
        bool continuouslyRotateY;
        bool continuouslyRotateZ;

        // basic data for Transformer
        int numpts = 0;
        int numlines = 0;
        bool gooddata = false;
        double[,] vertices;
        double[,] scrnpts;
        double[,] ctrans = new double[4,4];  //your main transformation matrix
        private System.Windows.Forms.ImageList tbimages;
        private System.Windows.Forms.ToolBar toolBar1;
        private System.Windows.Forms.ToolBarButton transleftbtn;
        private System.Windows.Forms.ToolBarButton transrightbtn;
        private System.Windows.Forms.ToolBarButton transupbtn;
        private System.Windows.Forms.ToolBarButton transdownbtn;
        private System.Windows.Forms.ToolBarButton toolBarButton1;
        private System.Windows.Forms.ToolBarButton scaleupbtn;
        private System.Windows.Forms.ToolBarButton scaledownbtn;
        private System.Windows.Forms.ToolBarButton toolBarButton2;
        private System.Windows.Forms.ToolBarButton rotxby1btn;
        private System.Windows.Forms.ToolBarButton rotyby1btn;
        private System.Windows.Forms.ToolBarButton rotzby1btn;
        private System.Windows.Forms.ToolBarButton toolBarButton3;
        private System.Windows.Forms.ToolBarButton rotxbtn;
        private System.Windows.Forms.ToolBarButton rotybtn;
        private System.Windows.Forms.ToolBarButton rotzbtn;
        private System.Windows.Forms.ToolBarButton toolBarButton4;
        private System.Windows.Forms.ToolBarButton shearrightbtn;
        private System.Windows.Forms.ToolBarButton shearleftbtn;
        private System.Windows.Forms.ToolBarButton toolBarButton5;
        private System.Windows.Forms.ToolBarButton resetbtn;
        private System.Windows.Forms.ToolBarButton exitbtn;
        int[,] lines;

        public Transformer()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            Text = "COMP 4560:  Assignment 5 (200830) Eric Tsang, A00841554, 4O";
            ResizeRedraw = true;
            BackColor = Color.Black;
            MenuItem miNewDat = new MenuItem("New &Data...",
                new EventHandler(MenuNewDataOnClick));
            MenuItem miExit = new MenuItem("E&xit",
                new EventHandler(MenuFileExitOnClick));
            MenuItem miDash = new MenuItem("-");
            MenuItem miFile = new MenuItem("&File",
                new MenuItem[] {miNewDat, miDash, miExit});
            MenuItem miAbout = new MenuItem("&About",
                new EventHandler(MenuAboutOnClick));
            Menu = new MainMenu(new MenuItem[] {miFile, miAbout});


        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose( bool disposing )
        {
            if( disposing )
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Transformer));
            this.tbimages = new System.Windows.Forms.ImageList(this.components);
            this.toolBar1 = new System.Windows.Forms.ToolBar();
            this.transleftbtn = new System.Windows.Forms.ToolBarButton();
            this.transrightbtn = new System.Windows.Forms.ToolBarButton();
            this.transupbtn = new System.Windows.Forms.ToolBarButton();
            this.transdownbtn = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton1 = new System.Windows.Forms.ToolBarButton();
            this.scaleupbtn = new System.Windows.Forms.ToolBarButton();
            this.scaledownbtn = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton2 = new System.Windows.Forms.ToolBarButton();
            this.rotxby1btn = new System.Windows.Forms.ToolBarButton();
            this.rotyby1btn = new System.Windows.Forms.ToolBarButton();
            this.rotzby1btn = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton3 = new System.Windows.Forms.ToolBarButton();
            this.rotxbtn = new System.Windows.Forms.ToolBarButton();
            this.rotybtn = new System.Windows.Forms.ToolBarButton();
            this.rotzbtn = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton4 = new System.Windows.Forms.ToolBarButton();
            this.shearrightbtn = new System.Windows.Forms.ToolBarButton();
            this.shearleftbtn = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton5 = new System.Windows.Forms.ToolBarButton();
            this.resetbtn = new System.Windows.Forms.ToolBarButton();
            this.exitbtn = new System.Windows.Forms.ToolBarButton();
            this.SuspendLayout();
            //
            // tbimages
            //
            this.tbimages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("tbimages.ImageStream")));
            this.tbimages.TransparentColor = System.Drawing.Color.Transparent;
            this.tbimages.Images.SetKeyName(0, "");
            this.tbimages.Images.SetKeyName(1, "");
            this.tbimages.Images.SetKeyName(2, "");
            this.tbimages.Images.SetKeyName(3, "");
            this.tbimages.Images.SetKeyName(4, "");
            this.tbimages.Images.SetKeyName(5, "");
            this.tbimages.Images.SetKeyName(6, "");
            this.tbimages.Images.SetKeyName(7, "");
            this.tbimages.Images.SetKeyName(8, "");
            this.tbimages.Images.SetKeyName(9, "");
            this.tbimages.Images.SetKeyName(10, "");
            this.tbimages.Images.SetKeyName(11, "");
            this.tbimages.Images.SetKeyName(12, "");
            this.tbimages.Images.SetKeyName(13, "");
            this.tbimages.Images.SetKeyName(14, "");
            this.tbimages.Images.SetKeyName(15, "");
            //
            // toolBar1
            //
            this.toolBar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.transleftbtn,
            this.transrightbtn,
            this.transupbtn,
            this.transdownbtn,
            this.toolBarButton1,
            this.scaleupbtn,
            this.scaledownbtn,
            this.toolBarButton2,
            this.rotxby1btn,
            this.rotyby1btn,
            this.rotzby1btn,
            this.toolBarButton3,
            this.rotxbtn,
            this.rotybtn,
            this.rotzbtn,
            this.toolBarButton4,
            this.shearrightbtn,
            this.shearleftbtn,
            this.toolBarButton5,
            this.resetbtn,
            this.exitbtn});
            this.toolBar1.Dock = System.Windows.Forms.DockStyle.Right;
            this.toolBar1.DropDownArrows = true;
            this.toolBar1.ImageList = this.tbimages;
            this.toolBar1.Location = new System.Drawing.Point(484, 0);
            this.toolBar1.Name = "toolBar1";
            this.toolBar1.ShowToolTips = true;
            this.toolBar1.Size = new System.Drawing.Size(24, 306);
            this.toolBar1.TabIndex = 0;
            this.toolBar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar1_ButtonClick);
            //
            // transleftbtn
            //
            this.transleftbtn.ImageIndex = 1;
            this.transleftbtn.Name = "transleftbtn";
            this.transleftbtn.ToolTipText = "translate left";
            //
            // transrightbtn
            //
            this.transrightbtn.ImageIndex = 0;
            this.transrightbtn.Name = "transrightbtn";
            this.transrightbtn.ToolTipText = "translate right";
            //
            // transupbtn
            //
            this.transupbtn.ImageIndex = 2;
            this.transupbtn.Name = "transupbtn";
            this.transupbtn.ToolTipText = "translate up";
            //
            // transdownbtn
            //
            this.transdownbtn.ImageIndex = 3;
            this.transdownbtn.Name = "transdownbtn";
            this.transdownbtn.ToolTipText = "translate down";
            //
            // toolBarButton1
            //
            this.toolBarButton1.Name = "toolBarButton1";
            this.toolBarButton1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            //
            // scaleupbtn
            //
            this.scaleupbtn.ImageIndex = 4;
            this.scaleupbtn.Name = "scaleupbtn";
            this.scaleupbtn.ToolTipText = "scale up";
            //
            // scaledownbtn
            //
            this.scaledownbtn.ImageIndex = 5;
            this.scaledownbtn.Name = "scaledownbtn";
            this.scaledownbtn.ToolTipText = "scale down";
            //
            // toolBarButton2
            //
            this.toolBarButton2.Name = "toolBarButton2";
            this.toolBarButton2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            //
            // rotxby1btn
            //
            this.rotxby1btn.ImageIndex = 6;
            this.rotxby1btn.Name = "rotxby1btn";
            this.rotxby1btn.ToolTipText = "rotate about x by 1";
            //
            // rotyby1btn
            //
            this.rotyby1btn.ImageIndex = 7;
            this.rotyby1btn.Name = "rotyby1btn";
            this.rotyby1btn.ToolTipText = "rotate about y by 1";
            //
            // rotzby1btn
            //
            this.rotzby1btn.ImageIndex = 8;
            this.rotzby1btn.Name = "rotzby1btn";
            this.rotzby1btn.ToolTipText = "rotate about z by 1";
            //
            // toolBarButton3
            //
            this.toolBarButton3.Name = "toolBarButton3";
            this.toolBarButton3.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            //
            // rotxbtn
            //
            this.rotxbtn.ImageIndex = 9;
            this.rotxbtn.Name = "rotxbtn";
            this.rotxbtn.ToolTipText = "rotate about x continuously";
            //
            // rotybtn
            //
            this.rotybtn.ImageIndex = 10;
            this.rotybtn.Name = "rotybtn";
            this.rotybtn.ToolTipText = "rotate about y continuously";
            //
            // rotzbtn
            //
            this.rotzbtn.ImageIndex = 11;
            this.rotzbtn.Name = "rotzbtn";
            this.rotzbtn.ToolTipText = "rotate about z continuously";
            //
            // toolBarButton4
            //
            this.toolBarButton4.Name = "toolBarButton4";
            this.toolBarButton4.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            //
            // shearrightbtn
            //
            this.shearrightbtn.ImageIndex = 12;
            this.shearrightbtn.Name = "shearrightbtn";
            this.shearrightbtn.ToolTipText = "shear right";
            //
            // shearleftbtn
            //
            this.shearleftbtn.ImageIndex = 13;
            this.shearleftbtn.Name = "shearleftbtn";
            this.shearleftbtn.ToolTipText = "shear left";
            //
            // toolBarButton5
            //
            this.toolBarButton5.Name = "toolBarButton5";
            this.toolBarButton5.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            //
            // resetbtn
            //
            this.resetbtn.ImageIndex = 14;
            this.resetbtn.Name = "resetbtn";
            this.resetbtn.ToolTipText = "restore the initial image";
            //
            // exitbtn
            //
            this.exitbtn.ImageIndex = 15;
            this.exitbtn.Name = "exitbtn";
            this.exitbtn.ToolTipText = "exit the program";
            //
            // Transformer
            //
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(508, 306);
            this.Controls.Add(this.toolBar1);
            this.Name = "Transformer";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Transformer_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.Run(new Transformer());
        }

        protected override void OnPaint(PaintEventArgs pea)
        {
            Graphics grfx = pea.Graphics;
            Pen pen = new Pen(Color.White, 3);
            double temp;
            int k;

            if (gooddata)
            {
                //create the screen coordinates:
                // scrnpts = vertices*ctrans

                for (int i = 0; i < numpts; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        temp = 0.0d;
                        for (k = 0; k < 4; k++)
                            temp += vertices[i, k] * ctrans[k, j];
                        scrnpts[i, j] = temp;
                    }
                }

                //now draw the lines

                for (int i = 0; i < numlines; i++)
                {
                    grfx.DrawLine(pen, (int)scrnpts[lines[i, 0], 0], (int)scrnpts[lines[i, 0], 1],
                        (int)scrnpts[lines[i, 1], 0], (int)scrnpts[lines[i, 1], 1]);
                }


            } // end of gooddata block
        } // end of OnPaint

        void MenuNewDataOnClick(object obj, EventArgs ea)
        {
            //MessageBox.Show("New Data item clicked.");
            gooddata = GetNewData();
            RestoreInitialImage();
        }

        void MenuFileExitOnClick(object obj, EventArgs ea)
        {
            Close();
        }

        void MenuAboutOnClick(object obj, EventArgs ea)
        {
            AboutDialogBox dlg = new AboutDialogBox();
            dlg.ShowDialog();
        }

        void RestoreInitialImage()
        {
            // reset ctrans
            ctrans = new double[4,4];
            setIdentity(ctrans,4,4);

            // move the shape to the right place
            addScale(new double[] {
                this.Height/2/shapeHeight,
                -this.Height/2/shapeHeight,
                this.Height/2/shapeHeight
                });
            rmTranslation(getCenterCoords());
            addTranslation(new double[] {this.Width/2,this.Height/2,0});

            // force repaint
            Invalidate();
        } // end of RestoreInitialImage

        bool GetNewData()
        {
            string strinputfile,text;
            ArrayList coorddata = new ArrayList();
            ArrayList linesdata = new ArrayList();
            OpenFileDialog opendlg = new OpenFileDialog();
            opendlg.Title = "Choose File with Coordinates of Vertices";
            if (opendlg.ShowDialog() == DialogResult.OK)
            {
                strinputfile=opendlg.FileName;
                FileInfo coordfile = new FileInfo(strinputfile);
                StreamReader reader = coordfile.OpenText();
                do
                {
                    text = reader.ReadLine();
                    if (text != null) coorddata.Add(text);
                } while (text != null);
                reader.Close();
                DecodeCoords(coorddata);
            }
            else
            {
                MessageBox.Show("***Failed to Open Coordinates File***");
                return false;
            }

            opendlg.Title = "Choose File with Data Specifying Lines";
            if (opendlg.ShowDialog() == DialogResult.OK)
            {
                strinputfile=opendlg.FileName;
                FileInfo linesfile = new FileInfo(strinputfile);
                StreamReader reader = linesfile.OpenText();
                do
                {
                    text = reader.ReadLine();
                    if (text != null) linesdata.Add(text);
                } while (text != null);
                reader.Close();
                DecodeLines(linesdata);
            }
            else
            {
                MessageBox.Show("***Failed to Open Line Data File***");
                return false;
            }
            scrnpts = new double[numpts,4];
            setIdentity(ctrans,4,4);  //initialize transformation matrix to identity
            return true;
        } // end of GetNewData

        void DecodeCoords(ArrayList coorddata)
        {
            Wrapper<double> minx = null;
            Wrapper<double> maxx = null;
            Wrapper<double> miny = null;
            Wrapper<double> maxy = null;
            Wrapper<double> minz = null;
            Wrapper<double> maxz = null;

            //this may allocate slightly more rows that necessary
            vertices = new double[coorddata.Count,4];
            numpts = 0;
            string [] text = null;
            for (int i = 0; i < coorddata.Count; i++)
            {
                // parse the coordinates out of the file
                text = coorddata[i].ToString().Split(' ',',');
                vertices[numpts,0]=double.Parse(text[0]);
                if (vertices[numpts,0] < 0.0d) break;
                vertices[numpts,1]=double.Parse(text[1]);
                vertices[numpts,2]=double.Parse(text[2]);
                vertices[numpts,3] = 1.0d;

                // update the min and max values
                if(minx == null)
                    minx = new Wrapper<double>(vertices[numpts,0]);
                else
                    minx.Value = Math.Min(minx.Value, vertices[numpts, 0]);
                if(maxx == null)
                    maxx = new Wrapper<double>(vertices[numpts,0]);
                else
                    maxx.Value = Math.Max(maxx.Value, vertices[numpts,0]);
                if(miny == null)
                    miny = new Wrapper<double>(vertices[numpts,1]);
                else
                    miny.Value = Math.Min(miny.Value, vertices[numpts,1]);
                if(maxy == null)
                    maxy = new Wrapper<double>(vertices[numpts,1]);
                else
                    maxy.Value = Math.Max(maxy.Value, vertices[numpts,1]);
                if(minz == null)
                    minz = new Wrapper<double>(vertices[numpts,1]);
                else
                    minz.Value = Math.Min(minz.Value, vertices[numpts,1]);
                if(maxz == null)
                    maxz = new Wrapper<double>(vertices[numpts,1]);
                else
                    maxz.Value = Math.Max(maxz.Value, vertices[numpts,1]);

                numpts++;
            }

            // initialize shape data
            centerX     = (maxx.Value+minx.Value)/2;
            centerY     = (maxy.Value+miny.Value)/2;
            centerZ     = (maxz.Value+minz.Value)/2;
            shapeWidth  = maxx.Value-minx.Value;
            shapeHeight = maxy.Value-miny.Value;
            shapeDepth  = maxz.Value-minz.Value;

        }// end of DecodeCoords

        void DecodeLines(ArrayList linesdata)
        {
            //this may allocate slightly more rows that necessary
            lines = new int[linesdata.Count,2];
            numlines = 0;
            string [] text = null;
            for (int i = 0; i < linesdata.Count; i++)
            {
                text = linesdata[i].ToString().Split(' ',',');
                lines[numlines,0]=int.Parse(text[0]);
                if (lines[numlines,0] < 0) break;
                lines[numlines,1]=int.Parse(text[1]);
                numlines++;
            }
        } // end of DecodeLines

        void setIdentity(double[,] A,int nrow,int ncol)
        {
            for (int i = 0; i < nrow;i++)
            {
                for (int j = 0; j < ncol; j++) A[i,j] = 0.0d;
                A[i,i] = 1.0d;
            }
        }// end of setIdentity

        private void Transformer_Load(object sender, System.EventArgs e)
        {
            continuouslyRotate();
        }

        ///////////////////////////////
        // basic transform functions //
        ///////////////////////////////

        private void addNetTransform(double[,] matrix)
        {
            double[,] temp = new double[4,4];

            // multiple the matrices
            for(int i = 0; i < 4; i++)
            {
                for(int j = 0; j < 4; j++)
                {
                    for(int k = 0; k < 4; k++)
                    {
                        temp[i,j] += ctrans[i,k]*matrix[k,j];
                    }
                }
            }

            // assign ctrans to the new matrix
            ctrans = temp;
        }

        /////////////////////////////////////
        // convenience transform functions //
        /////////////////////////////////////

        private double[] getCenterCoords()
        {
            double[] center = new double[3];

            getTranslation(center);

            center[0] += centerX*ctrans[0,0]+
                         centerY*ctrans[1,0]+
                         centerZ*ctrans[2,0];
            center[1] += centerX*ctrans[0,1]+
                         centerY*ctrans[1,1]+
                         centerZ*ctrans[2,1];
            center[2] += centerX*ctrans[0,2]+
                         centerY*ctrans[1,2]+
                         centerZ*ctrans[2,2];

            return center;
        }

        // translate functions

        private void addTranslation(double[] xyz)
        {
            double[,] temp = new double[4,4];
            setIdentity(temp,4,4);
            temp[3,0] = xyz[0];
            temp[3,1] = xyz[1];
            temp[3,2] = xyz[2];
            addNetTransform(temp);
        }

        private void rmTranslation(double[] xyz)
        {
            double[,] temp = new double[4,4];
            setIdentity(temp,4,4);
            temp[3,0] = -xyz[0];
            temp[3,1] = -xyz[1];
            temp[3,2] = -xyz[2];
            addNetTransform(temp);
        }

        private void getTranslation(double[] xyz)
        {
            xyz[0] = ctrans[3,0];
            xyz[1] = ctrans[3,1];
            xyz[2] = ctrans[3,2];
        }

        private void setTranslation(double[] xyz)
        {
            ctrans[3,0] = xyz[0];
            ctrans[3,1] = xyz[1];
            ctrans[3,2] = xyz[2];
        }

        // scale functions

        private void addScale(double[] xyz)
        {
            double[,] temp = new double[4,4];
            setIdentity(temp,4,4);
            temp[0,0] = xyz[0];
            temp[1,1] = xyz[1];
            temp[2,2] = xyz[2];
            temp[3,3] = 1;
            addNetTransform(temp);
        }

        private void setScale(double[] xyz)
        {
            ctrans[0,0] = xyz[0];
            ctrans[1,1] = xyz[1];
            ctrans[2,2] = xyz[2];
        }

        // rotate functions

        /**
         * rotates around the x axis in a clockwise direction, from perspective
         *   of positive x axis to the origin.
         *
         * @param radians radians to rotate around x axis as described above.
         */
        private void rotateX(double radians)
        {
            double[,] temp = new double[4,4];
            setIdentity(temp,4,4);
            temp[1,1] = Math.Cos(radians);
            temp[1,2] = -Math.Sin(radians);
            temp[2,1] = Math.Sin(radians);
            temp[2,2] = Math.Cos(radians);
            addNetTransform(temp);
        }

        /**
         * rotates around the y axis in a clockwise direction, from perspective
         *   of positive y axis to the origin.
         *
         * @param radians radians to rotate around y axis as described above.
         */
        private void rotateY(double radians)
        {
            double[,] temp = new double[4,4];
            setIdentity(temp,4,4);
            temp[0,0] = Math.Cos(radians);
            temp[0,2] = -Math.Sin(radians);
            temp[2,0] = Math.Sin(radians);
            temp[2,2] = Math.Cos(radians);
            addNetTransform(temp);
        }

        /**
         * rotates around the z axis in a clockwise direction, from perspective
         *   of positive z axis to the origin.
         *
         * @param radians radians to rotate around z axis as described above.
         */
        private void rotateZ(double radians)
        {
            double[,] temp = new double[4,4];
            setIdentity(temp,4,4);
            temp[0,0] = Math.Cos(radians);
            temp[0,1] = Math.Sin(radians);
            temp[1,0] = -Math.Sin(radians);
            temp[1,1] = Math.Cos(radians);
            addNetTransform(temp);
        }

        // shear functions

        private void shearX(double scale)
        {
            double[,] temp = new double[4,4];
            setIdentity(temp,4,4);
            temp[1,0] = scale;
            addNetTransform(temp);
        }

        /////////////////////////////////////////
        // public rotate functions for threads //
        /////////////////////////////////////////

        private async void continuouslyRotate()
        {
            while (true)
            {
                double[] translationParams = getCenterCoords();

                rmTranslation(translationParams);
                if (continuouslyRotateX)
                    rotateX(0.05);
                if (continuouslyRotateY)
                    rotateY(0.05);
                if (continuouslyRotateZ)
                    rotateZ(0.05);
                addTranslation(translationParams);
                Refresh();
                await Task.Delay(50);
            }
        }

        ///////////////////
        // event handler //
        ///////////////////

        private void toolBar1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
        {

            if (e.Button == transleftbtn)
            {
                addTranslation(new double[] {-50, 0, 0});
                Refresh();
            }
            if (e.Button == transrightbtn)
            {
                addTranslation(new double[] {50, 0, 0});
                Refresh();
            }
            if (e.Button == transupbtn)
            {
                addTranslation(new double[] {0, -25, 0});
                Refresh();
            }

            if(e.Button == transdownbtn)
            {
                addTranslation(new double[] {0, 25, 0});
                Refresh();
            }
            if (e.Button == scaleupbtn)
            {
                double[] translationParams = getCenterCoords();

                rmTranslation(translationParams);
                addScale(new double[] {1.1,1.1,1.1});
                addTranslation(translationParams);

                Refresh();
            }
            if (e.Button == scaledownbtn)
            {
                double[] translationParams = getCenterCoords();

                rmTranslation(translationParams);
                addScale(new double[] {0.9,0.9,0.9});
                addTranslation(translationParams);

                Refresh();
            }
            if (e.Button == rotxby1btn)
            {
                continuouslyRotateX = false;
                continuouslyRotateY = false;
                continuouslyRotateZ = false;

                double[] translationParams = getCenterCoords();

                rmTranslation(translationParams);
                rotateX(0.05);
                addTranslation(translationParams);

                Refresh();
            }
            if (e.Button == rotyby1btn)
            {
                continuouslyRotateX = false;
                continuouslyRotateY = false;
                continuouslyRotateZ = false;

                double[] translationParams = getCenterCoords();

                rmTranslation(translationParams);
                rotateY(0.05);
                addTranslation(translationParams);

                Refresh();
            }
            if (e.Button == rotzby1btn)
            {
                continuouslyRotateX = false;
                continuouslyRotateY = false;
                continuouslyRotateZ = false;

                double[] translationParams = getCenterCoords();

                rmTranslation(translationParams);
                rotateZ(0.05);
                addTranslation(translationParams);

                Refresh();
            }

            if (e.Button == rotxbtn)
            {
                continuouslyRotateX = true;
            }
            if (e.Button == rotybtn)
            {
                continuouslyRotateY = true;
            }

            if (e.Button == rotzbtn)
            {
                continuouslyRotateZ = true;
            }

            if(e.Button == shearleftbtn)
            {
                double[] center = getCenterCoords();
                double[] translationParams = new double[3];

                translationParams[1] = -centerX*ctrans[0,1]
                                       -centerY*ctrans[1,1]
                                       -centerZ*ctrans[2,1];

                rmTranslation(center);
                rmTranslation(translationParams);
                shearX(0.1);
                addTranslation(translationParams);
                addTranslation(center);

                Refresh();
            }

            if (e.Button == shearrightbtn)
            {
                double[] center = getCenterCoords();
                double[] translationParams = new double[3];

                translationParams[1] = -centerX*ctrans[0,1]+
                                       -centerY*ctrans[1,1]+
                                       -centerZ*ctrans[2,1];

                rmTranslation(center);
                rmTranslation(translationParams);
                shearX(-0.1);
                addTranslation(translationParams);
                addTranslation(center);

                Refresh();
            }

            if (e.Button == resetbtn)
            {
                RestoreInitialImage();
            }

            if(e.Button == exitbtn)
            {
                Close();
            }

        }


    }


}
