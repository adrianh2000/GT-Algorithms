using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;

namespace GraphTheoryEditor
{
    class Graph 
    {
        const int iAdjMatrixNumVerticesStep = 20;
        
        int iVertexRadius;
        public Edge[,] eAdjMatrix;
        public List<Vertex> lVertexList = new List<Vertex>();        
       
        public Graph()
        {
            Initialize();
        }

        public void Initialize()
        {
            int iCol, iRow;

            iVertexRadius = 30;
            eAdjMatrix = new Edge[iAdjMatrixNumVerticesStep, iAdjMatrixNumVerticesStep];
            lVertexList.Clear();

            for (iRow = 0; iRow < iAdjMatrixNumVerticesStep; iRow++)
                for (iCol = 0; iCol < iAdjMatrixNumVerticesStep; iCol++)
                    eAdjMatrix[iRow, iCol] = new Edge(-1, -1);   
        }

        public void clearAllVertexLabels(int iNewLabel)
        {
            foreach (Vertex v in lVertexList)
            {
                v.setLabel(iNewLabel);
            }
        }

        public void clearAllVertexVisited()
        {
            foreach (Vertex v in lVertexList)
            {
                v.setVisited(false);
            }
        }

        public void clearAllVertexOrder(int iNewOrder)
        {
            foreach (Vertex v in lVertexList)
            {
                v.setOrder(iNewOrder);
            }
        }

        public void clearAllEdgeOrder(int iNewOrder)
        {
            for (int iRow = 0; iRow < iAdjMatrixNumVerticesStep; iRow++)
                for (int iCol = 0; iCol < iAdjMatrixNumVerticesStep; iCol++)
                    eAdjMatrix[iRow, iCol].setOrder(iNewOrder);   
        }

        public void clearAllEdgeLabels(String sNewLabel)
        {
            for (int iRow = 0; iRow < iAdjMatrixNumVerticesStep; iRow++)
                for (int iCol = 0; iCol < iAdjMatrixNumVerticesStep; iCol++)
                    eAdjMatrix[iRow, iCol].setLabel(sNewLabel);
        }


        public void ClearAllLabelsAndOrder()
        {
            clearAllVertexLabels(-1);
            clearAllVertexVisited();
            clearAllVertexOrder(-1);
            clearAllEdgeOrder(-1);
            clearAllEdgeLabels("");
        }

        public void SetVertexRadius(int iVR)
        {
            int i;
            iVertexRadius = iVR;

            for(i=0;i<lVertexList.Count;i++)
                lVertexList[i].SetRadius(iVertexRadius);                
        }

        public void AddEdge(int iVertexIndex0, int iVertexIndex1)
        {            
            //Make sure we only deal with the upper triangle of the adjacency matrix
            if(iVertexIndex0>iVertexIndex1)
                Swap(ref iVertexIndex0, ref iVertexIndex1);

            //Make sure the adjancy matrix is large enough
            if (iVertexIndex1 >= eAdjMatrix.GetUpperBound(0))
            {
                //Enlarge Adjacency matrix
                int iRow,iCol, iOldSize = eAdjMatrix.GetUpperBound(0);
                int iNewSize = iVertexIndex1 + iAdjMatrixNumVerticesStep;
                Edge[,] eTempAdj = new Edge[iNewSize, iNewSize];
                Edge eCurEdge;

                //Copy old data
                for(iRow=0; iRow < iOldSize; iRow++)
                    for (iCol = 0; iCol < iOldSize; iCol++)
                    {
                        eCurEdge = new Edge(eAdjMatrix[iRow, iCol].GetVertex0(), eAdjMatrix[iRow, iCol].GetVertex1());
                        eCurEdge.SetWeight(eAdjMatrix[iRow, iCol].GetWeight());

                        eTempAdj[iRow, iCol] = eCurEdge;
                    }

                eAdjMatrix = new Edge[iNewSize, iNewSize];
                for (iRow = 0; iRow < iOldSize; iRow++)
                    for (iCol = 0; iCol < iOldSize; iCol++)
                    {
                        eCurEdge = new Edge(eTempAdj[iRow, iCol].GetVertex0(), eTempAdj[iRow, iCol].GetVertex1());
                        eCurEdge.SetWeight(eTempAdj[iRow, iCol].GetWeight());

                        eAdjMatrix[iRow, iCol] = eCurEdge;
                    }

                //Add new data
                for (iRow = iOldSize; iRow < iNewSize; iRow++)
                    for (iCol = iOldSize; iCol < iNewSize; iCol++)
                        eAdjMatrix[iRow, iCol] = new Edge(-1, -1);   
                
            }

            eAdjMatrix[iVertexIndex0, iVertexIndex1].SetVerticesV0V1(iVertexIndex0,iVertexIndex1);
        }
           

        private void Swap(ref int iA, ref int iB)
        {
            int iTemp = iA;
            iA = iB;
            iB = iTemp;
        }

        public void DrawGraph(Graphics g)
        {
            int iCol, iRow, i, iNumVertices = lVertexList.Count, iPosX0, iPosY0, iPosX1, iPosY1;
           
            //Draw Edges
            for (iRow = 0; iRow < iNumVertices; iRow++)
                for (iCol = iRow + 1; iCol < iNumVertices; iCol++)
                    if (eAdjMatrix[iRow, iCol].ExistsEdge())
                    {
                        iPosX0 = Convert.ToInt32(lVertexList[iRow].GetX());
                        iPosY0 = Convert.ToInt32(lVertexList[iRow].GetY());
                        iPosX1 = Convert.ToInt32(lVertexList[iCol].GetX());
                        iPosY1 = Convert.ToInt32(lVertexList[iCol].GetY());
                        eAdjMatrix[iRow, iCol].DrawEdge(g, iPosX0, iPosY0, iPosX1, iPosY1);
                    }

            //Draw Vertices
            for (i = 0; i < iNumVertices; i++)
                lVertexList[i].DrawVertex(g);
        }

        public int iFindVertexIndex(int iPosX, int iPosY)
        {
            int iVertexIndex = -1, i = -1;

            while ((i < lVertexList.Count-1) && (iVertexIndex == -1))
            {
                i++;

                if (lVertexList[i].MouseInsideVertex(iPosX, iPosY))
                    iVertexIndex = i;
            }

            return iVertexIndex;
        }

        public int getNumVertices() { return lVertexList.Count;  }

        //Returns a list with the index of each adjacent vertex of iVertexIndex
        public List<int> findAdjacentVertices(int iVertexIndex)
        {
            List<int> lResult = new List<int>();

            for (int iCol = iVertexIndex+1; iCol < lVertexList.Count; iCol++)
                if (eAdjMatrix[iVertexIndex, iCol].ExistsEdge())
                    lResult.Add(iCol);

            for (int iRow = 0; iRow < iVertexIndex; iRow++)
                if (eAdjMatrix[iRow, iVertexIndex].ExistsEdge())
                    lResult.Add(iRow);

            return lResult;
        }

        //Returns a list with the index of each unlabeled adjacent vertex of iVertexIndex
        public List<int> findAdjacentUnlabeledVertices(int iVertexIndex)
        {
            List<int> lResult = new List<int>();

            for (int iCol = iVertexIndex + 1; iCol < lVertexList.Count; iCol++)
                if (eAdjMatrix[iVertexIndex, iCol].ExistsEdge() && !lVertexList[iCol].isLabeled())
                    lResult.Add(iCol);

            for (int iRow = 0; iRow < iVertexIndex; iRow++)
                if (eAdjMatrix[iRow, iVertexIndex].ExistsEdge() && !lVertexList[iRow].isLabeled())
                    lResult.Add(iRow);

            return lResult;
        }

        public Boolean isAnyVertexLabelOfPairUsed(int iVL0, int iVL1)
        {
            int i = 0;
            Boolean bLabelUsed = false;

            while (i < lVertexList.Count && !bLabelUsed)
            {
                bLabelUsed = (lVertexList[i].getLabel() == iVL0) || (lVertexList[i].getLabel() == iVL1);
                i++;
            }

            return bLabelUsed;
        }

    }
}
