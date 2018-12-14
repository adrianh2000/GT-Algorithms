using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace GraphTheoryEditor
{
    public partial class Form1 : Form
    {
        int iMousePosX, iMousePosY, iVertexSelectedIndex;

        Graph gCurrentGraph;
        String sCurrentAction;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
            gCurrentGraph = new Graph();
            sCurrentAction = "SELECTING";
            iVertexSelectedIndex = -1;            
        }

        private void pictureBoxMain_Click(object sender, EventArgs e)
        {
            int iCurrentVertexSelectedIndex;

            switch(sCurrentAction) 
            {
                case "SELECTING":
                    iCurrentVertexSelectedIndex = gCurrentGraph.iFindVertexIndex(iMousePosX, iMousePosY);

                    if (iCurrentVertexSelectedIndex > -1)
                    {
                        iVertexSelectedIndex = iCurrentVertexSelectedIndex;
                        sCurrentAction = "VERTEXMOVE";
                    }
                    break;
                case "VERTEXMOVE":
                    iVertexSelectedIndex = -1;
                    sCurrentAction = "SELECTING";
                    break;
                case "VERTEXCREATE":
                    AddVertexToPictureBox();
                    break;
                case "EDGECREATE":
                    iCurrentVertexSelectedIndex = gCurrentGraph.iFindVertexIndex(iMousePosX, iMousePosY);

                    if (iCurrentVertexSelectedIndex > -1)
                    {
                        if (iVertexSelectedIndex == -1)
                            iVertexSelectedIndex = iCurrentVertexSelectedIndex;
                        else
                        {
                            //Add Edge
                            gCurrentGraph.AddEdge(iVertexSelectedIndex, iCurrentVertexSelectedIndex);

                            if (iVertexSelectedIndex > iCurrentVertexSelectedIndex)
                            {
                                int iTemp = iVertexSelectedIndex;
                                iVertexSelectedIndex = iCurrentVertexSelectedIndex;
                                iCurrentVertexSelectedIndex = iTemp;
                            }

                            if (!gCurrentGraph.eAdjMatrix[iVertexSelectedIndex, iCurrentVertexSelectedIndex].ExistsEdge())
                            {
                                iVertexSelectedIndex += 0;
                            }

                            iVertexSelectedIndex = -1;
                        }
                    }

                    break;
            }
        }

        private void pictureBoxMain_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {            
            Graphics g = e.Graphics;
            Refresh(g);
        }

        private void pictureBoxMain_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            labelMouseCoordinates.Text = "(X, Y) = (" + e.X.ToString() + ", " + e.Y.ToString()+")";
            iMousePosX = e.X;
            iMousePosY = e.Y;
            pictureBoxMain.Refresh();
        }

        private void buttonDrawVertex_Click(object sender, EventArgs e)
        {
            sCurrentAction = "VERTEXCREATE";
            //AddVertexToPictureBox();
        }

        private void AddVertexToPictureBox()
        {
            Vertex vNewVertex = new Vertex(iMousePosX, iMousePosY, 30, gCurrentGraph.lVertexList.Count);            
            gCurrentGraph.lVertexList.Add(vNewVertex);
            //iVertexSelectedIndex = gCurrentGraph.lVertexList.Count - 1;
        }

        private void Refresh(Graphics g)
        {
            switch (sCurrentAction)
            {
                case "SELECTING":
                    //Pen myPen = new Pen(System.Drawing.Color.Red, 5);
                    
                    //g.DrawEllipse(myPen, iMousePosX-15, iMousePosY-15, 30, 30);
                    break;
                case "VERTEXMOVE":
                    int iRadius = gCurrentGraph.lVertexList[iVertexSelectedIndex].GetRadius();

                    gCurrentGraph.lVertexList[iVertexSelectedIndex].SetXY(iMousePosX, iMousePosY);    
                    break;
                case "EDGECREATE":
                    if (iVertexSelectedIndex > -1)
                    {
                        int iPosX = Convert.ToInt32(gCurrentGraph.lVertexList[iVertexSelectedIndex].GetX());
                        int iPosY = Convert.ToInt32(gCurrentGraph.lVertexList[iVertexSelectedIndex].GetY());

                        Pen myPen = new Pen(System.Drawing.Color.Green, 5);

                        g.DrawLine(myPen, iPosX, iPosY, iMousePosX, iMousePosY);
                    }

                    break;
            }

            gCurrentGraph.DrawGraph(g);
        }

        private void buttonDrawEdge_Click(object sender, EventArgs e)
        {
            sCurrentAction = "EDGECREATE";
            iVertexSelectedIndex = -1;
        }

        private void buttonSelection_Click(object sender, EventArgs e)
        {
            sCurrentAction = "SELECTING";
            iVertexSelectedIndex = -1;
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void showDistancesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form fDisplayForm = new Form();
            TextBox tbDisplayText = new TextBox();
            int iRow, iCol, iNumvertices = gCurrentGraph.lVertexList.Count, iCellWidth = 10;
            String sText = "", sDistance, sSpaces = GetSpaces(iCellWidth);
            Algorithm algDistances = new Algorithm();
            Button buttonOk = new Button(), buttonCancel = new Button();
            Font fTextFont = new Font("Courier", 10);

            fDisplayForm.Text = "Distances";
            fDisplayForm.Width = this.Width;
            fDisplayForm.Height = this.Height;
            fDisplayForm.Left = this.Left + this.Width;
            fDisplayForm.Top = this.Top;
            fDisplayForm.AcceptButton = buttonOk;
            fDisplayForm.CancelButton = buttonCancel;

            tbDisplayText.Multiline = true;
            tbDisplayText.Width = fDisplayForm.Width - fDisplayForm.Width/20;
            tbDisplayText.Height = fDisplayForm.Height - fDisplayForm.Height / 20;
            tbDisplayText.Left = 0;
            tbDisplayText.Top = 0;
            tbDisplayText.Font = fTextFont;
            tbDisplayText.ScrollBars = ScrollBars.Both;
            algDistances.ComputeAllDistances(gCurrentGraph);

            sText += sSpaces + "  ";
            for (iCol = 0; iCol < iNumvertices; iCol++)
                sText += String.Format("{0,"+iCellWidth.ToString()+"}",iCol.ToString()) + " ";

            sText += Environment.NewLine;

            for (iRow = 0; iRow < iNumvertices; iRow++)
            {
                sText += String.Format("{0,"+iCellWidth.ToString()+"} [ ",iRow.ToString());
                for (iCol = 0; iCol < iNumvertices; iCol++)
                {
                    if (algDistances.aAllDistances[iRow, iCol] == Double.MaxValue)
                        sDistance = String.Format("{0," + iCellWidth.ToString() + "}", "∞");
                    else
                        sDistance = String.Format("{0," + iCellWidth.ToString() + "}",algDistances.aAllDistances[iRow, iCol].ToString());

                    sText += sDistance + " ";
                }
                sText += "]"+Environment.NewLine;
            }

            tbDisplayText.Text = sText;
            fDisplayForm.Controls.Add(tbDisplayText);
            DialogResult dialogResult = fDisplayForm.ShowDialog();                
        }

        private String GetSpaces(int iNumSpaces)
        {
            int i;
            String sText = "";

            for (i = 0; i < iNumSpaces; i++)
                sText += " ";

            return sText;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String sFilename;
            FileStream fs;
            StreamWriter sw;

            try
            {                
                SaveFileDialog dialogSave = new SaveFileDialog();
                dialogSave.InitialDirectory = Directory.GetCurrentDirectory();
                //dialogSave.Filter = "Graph Files (*.gph; *.txt)|All Files (*.*)";

                if (dialogSave.ShowDialog() == DialogResult.OK)
                {
                    int iRow, iCol, iVertex, iNumVertices = gCurrentGraph.lVertexList.Count;

                    sFilename = dialogSave.FileName;
                    fs = new FileStream(sFilename, FileMode.Create, FileAccess.Write);
                    sw = new StreamWriter(fs);

                    sw.WriteLine(iNumVertices.ToString());

                    for (iVertex = 0; iVertex < iNumVertices; iVertex++)
                        sw.WriteLine(gCurrentGraph.lVertexList[iVertex].getLabel() + "," + gCurrentGraph.lVertexList[iVertex].GetX().ToString() + "," + gCurrentGraph.lVertexList[iVertex].GetY().ToString() +","+ gCurrentGraph.lVertexList[iVertex].GetRadius().ToString());

                    //Save Edges
                    for (iRow = 0; iRow < iNumVertices; iRow++)
                        for (iCol = iRow + 1; iCol < iNumVertices; iCol++)
                            if (gCurrentGraph.eAdjMatrix[iRow, iCol].ExistsEdge())
                                sw.WriteLine(iRow.ToString()+","+iCol.ToString());

                    sw.Close();
                }
            }
            catch (Exception)
            {
                throw new ApplicationException("Failed saving graph");
            }


        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            String sFilename, sPath = Directory.GetCurrentDirectory();
            DirectoryInfo diCurDir = new DirectoryInfo(sPath);
            DirectoryInfo twoLevelsUp = diCurDir.Parent.Parent;
            sPath = twoLevelsUp.FullName;
            FileStream fs;
            StreamReader sr;
            
            try
            {
                OpenFileDialog dialogOpen = new OpenFileDialog();

                //dialogOpen.InitialDirectory = Directory.GetCurrentDirectory();
                dialogOpen.InitialDirectory = sPath + @"\UserGraphs";

                //dialogOpen.Filter = "Graph Files (*.gph; *.txt)|All Files (*.*)";

                if (dialogOpen.ShowDialog() == DialogResult.OK)
                {
                    int iVertex, iNumVertices;
                    String sCurrentLine;
                    String[] aCurrentLine;
                    Vertex vNewVertex;

                    gCurrentGraph.Initialize();

                    sFilename = dialogOpen.FileName;
                    fs = new FileStream(sFilename, FileMode.Open, FileAccess.Read);
                    sr = new StreamReader(fs);

                    //Read Number of vertices
                    sCurrentLine = sr.ReadLine();
                    iNumVertices = Convert.ToInt32(sCurrentLine);

                    for (iVertex = 0; iVertex < iNumVertices; iVertex++)
                    {                        
                        sCurrentLine = sr.ReadLine();
                        aCurrentLine = sCurrentLine.Split(',');

                        vNewVertex = new Vertex(Convert.ToDouble(aCurrentLine[1]), Convert.ToDouble(aCurrentLine[2]), Convert.ToDouble(aCurrentLine[3]), Convert.ToInt16(aCurrentLine[0]));
                        gCurrentGraph.lVertexList.Add(vNewVertex);
                    }

                    //Read Edges
                    while (!sr.EndOfStream)
                    {
                        sCurrentLine = sr.ReadLine();
                        aCurrentLine = sCurrentLine.Split(',');
                        gCurrentGraph.AddEdge(Convert.ToInt32(aCurrentLine[0]),Convert.ToInt32(aCurrentLine[1]));                        
                    }

                    sr.Close();
                }
            }
            catch (Exception)
            {
                throw new ApplicationException("Failed opening graph");
            }
        }

        private void algorithmToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void buttonCreateCircle_Click(object sender, EventArgs e)
        {
            int i, n;
            Double dTheta, dCenterX = pictureBoxMain.Width / 2, dCenterY = pictureBoxMain.Height / 2, dRadius = pictureBoxMain.Height / 4;
            String sText;
            Double dPosX, dPosY;

            sText = Microsoft.VisualBasic.Interaction.InputBox("How many vertices?", "Create circular graph", "8");

            try
            {
                n = Convert.ToInt32(sText);
                Vertex vNewVertex;

                gCurrentGraph.Initialize();

                for (i = 0; i < n; i++) 
                {
                    dTheta = i * ((2 * Math.PI)/n);
                    dPosX = dCenterX + dRadius * Math.Cos(dTheta);
                    dPosY = dCenterY + dRadius * Math.Sin(dTheta);

                    vNewVertex = new Vertex(dPosX, dPosY, 30, i);
                    gCurrentGraph.lVertexList.Add(vNewVertex);
                }
            }
            catch (Exception)
            {
                throw new ApplicationException("Invalid number of vertices");
            }            
        }

        private void depthFirstSearchDFSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Algorithm alDFS = new Algorithm();

            alDFS.depthFirstSearch(gCurrentGraph);

            Refresh();
        }

        private void saveGraphScreenshotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Save Graph image to file
            String sDirectoryRoot = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString()).FullName;
            String sNewDirectory = @"n = " + gCurrentGraph.lVertexList.Count.ToString() + ", " + DateTime.Now.ToShortDateString().Replace('/', '-') + ", " + DateTime.Now.ToShortTimeString().Replace(':', '_');
            String sPath = sDirectoryRoot + @"\Output\";

            var bitmap = new Bitmap(pictureBoxMain.Width, pictureBoxMain.Height);
            pictureBoxMain.DrawToBitmap(bitmap, pictureBoxMain.ClientRectangle);
            bitmap.Save(sPath + "GraphTest.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            //pictureBoxMain.Image.Save(sPath + "Graph.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
        }
               

        private void findGracefulLabelingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            String sDirectoryRoot = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString()).FullName;
            String sToday = DateTime.Now.Year.ToString("D4") + "-" + DateTime.Now.Month.ToString("D2") + "-" + DateTime.Now.Day.ToString("D2") + ", " + DateTime.Now.ToShortTimeString().Replace(':', '_'); ; //  DateTime.Now.ToShortDateString().Replace('/', '-') + ", " + DateTime.Now.ToShortTimeString().Replace(':', '_');
            String sNewDirectory = sToday + @", n = " + gCurrentGraph.lVertexList.Count.ToString();
            String sPath = sDirectoryRoot + @"\Output\" + sNewDirectory + @"\";

            if (Directory.Exists(sPath)) { 
                MessageBox.Show("Directory: " + sPath + " already exists. Select another folder and try again");
                return;
            }
            else
            {
                //Create new folder                
                System.IO.FileInfo file = new System.IO.FileInfo(sPath);
                file.Directory.Create(); // If the directory already exists, this method does nothing.
            }

            //Save Graph image to file
            var bitmap = new Bitmap(pictureBoxMain.Width, pictureBoxMain.Height);
            pictureBoxMain.DrawToBitmap(bitmap, pictureBoxMain.ClientRectangle);
            bitmap.Save(sPath + "Graph.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);


            String sFilename="OutputGraceful n="+gCurrentGraph.lVertexList.Count.ToString()+".txt";
            GracefulAlgorithm aGL = new GracefulAlgorithm(gCurrentGraph);
            List<int[]> lGracefulLabelings;

            aGL.execute(sPath);
            lGracefulLabelings = aGL.getAllGracefulLabels();

            //Save graceful labelings to a file
            FileStream fs = new FileStream(sPath +sFilename, FileMode.Create, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);

            sw.WriteLine("Labelings for a tree n = " + gCurrentGraph.lVertexList.Count.ToString());
            sw.WriteLine("Number of graceful labelings found: " + lGracefulLabelings.Count);
            sw.WriteLine("------------| Vertex labelings by vertex order |--------------------------------");
            foreach (int[] aiLabeling in lGracefulLabelings)
            { 
                for(int i = 0; i < aiLabeling.Length; i++)                
                    sw.Write("V["+i+"]="+aiLabeling[i]+", ");                

                sw.WriteLine();
            }

            sw.WriteLine("------------| Vertex labelings in DFS order |--------------------------------");
            foreach (int[] aiLabeling in lGracefulLabelings)
            {
                for (int i = 0; i < aiLabeling.Length; i++)
                    sw.Write(aiLabeling[i] + ", ");

                sw.WriteLine();
            }

            sw.Close();

            //----- create file to be imported into the python program that displays all labels graphically
            sFilename = "graphics labels.txt";
            fs = new FileStream(sPath + sFilename, FileMode.Create, FileAccess.Write);
            sw = new StreamWriter(fs);
            sw.WriteLine("vertices. Vertex order,x,y");
            for(int iCtr = 0; iCtr < gCurrentGraph.lVertexList.Count; iCtr++)
            {
                //String sOrder = gCurrentGraph.lVertexList[iCtr].getOrder().ToString();
                double x = gCurrentGraph.lVertexList[iCtr].GetX(), y = gCurrentGraph.lVertexList[iCtr].GetY();
                
                //convert coordinates to homogenous coordinates (0, 1)
                x /= pictureBoxMain.Width;
                y /= pictureBoxMain.Height;

                sw.WriteLine(iCtr + "," + x.ToString("#.####") + "," + y.ToString("#.####"));
            }

            sw.WriteLine("edges. Ei,Ej");
            int n = gCurrentGraph.lVertexList.Count;
            for (int iRow = 0; iRow < n; iRow++)
                for (int iCol = iRow + 1; iCol < n; iCol++)
                    if (gCurrentGraph.eAdjMatrix[iRow, iCol].ExistsEdge())
                        sw.WriteLine(iRow + "," + iCol);

            sw.WriteLine("graceful labels. l0,l1,...,ln");            
            foreach (int[] aiLabeling in lGracefulLabelings)
            {
                for (int i = 0; i < aiLabeling.Length; i++)
                    sw.Write(aiLabeling[i] + ",");

                sw.WriteLine();
            }

            sw.WriteLine("end");
            sw.Close();

            playSound();
            MessageBox.Show("Output file created , you can find the file at " + sPath + sFilename);            
        }

        public void playSound()
        {
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(@"I:\Research\Graph Theory\Graceful\Find all graceful labels\GraphTheoryEditorWithFindAllGracefulLabelings\GraphTheoryEditor\sounds\blast.wav");
            player.Play();
        }
    }
}
