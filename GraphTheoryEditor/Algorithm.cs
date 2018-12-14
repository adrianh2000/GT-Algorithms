using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace GraphTheoryEditor
{
    class Algorithm
    {
        public Double[,] aAllDistances;

        public Algorithm()
        {
            
        }

        public void ComputeAllDistances(Graph gGraph)
        {
            int i, j, k, iOrder = gGraph.lVertexList.Count;
            
            Double[,] D1;

            //initialize
            D1 = new Double[iOrder, iOrder];            

            for (i = 0; i < iOrder; i++)            
                for (j = i + 1; j < iOrder; j++)
                {
                    if (gGraph.eAdjMatrix[i,j].ExistsEdge())
                    {
                        D1[i, j] = gGraph.eAdjMatrix[i, j].GetWeight();
                        D1[j, i] = gGraph.eAdjMatrix[i, j].GetWeight();
                    }
                    else
                    {
                        D1[i,j] = Double.MaxValue;
                    }
                } 

            //Compute Distances
            for (k = 0; k < iOrder; k++)
                for (i = 0; i < iOrder; i++)
                    for (j = 0; j < iOrder; j++)
                        if ((i != j) && ((D1[i, k] * D1[k, j]) != 0))
                            if (((D1[i, k] + D1[k, j]) < D1[i, j]) || (D1[i, j] == 0))
                                D1[i, j] = D1[i, k] + D1[k, j];

            aAllDistances = new Double[iOrder, iOrder];           
            aAllDistances = D1;            
        }

        private Double GetMax(Double dA, Double dB)
        {
            if (dA > dB)
                return dA;
            else
                return dB;
        }

        public void depthFirstSearch(Graph gGraph, int iVertex = 0) {
            gGraph.clearAllEdgeLabels("-1");
            gGraph.clearAllEdgeOrder(-1);
            gGraph.clearAllVertexLabels(-1);
            gGraph.clearAllVertexOrder(-1);
            gGraph.clearAllVertexVisited();

            int iVisitedCtr = DFS(gGraph, iVertex, 0);
        }

        private int DFS(Graph gGraph, int iVertex, int iVisitedCtr)
        {
            List<int> lAdjNonVisitedVertices;

            gGraph.lVertexList[iVertex].setOrder(iVisitedCtr);
            gGraph.lVertexList[iVertex].setLabel(iVisitedCtr);
            gGraph.lVertexList[iVertex].setVisited(true);
            gGraph.lVertexList[iVertex].setVertexColor(System.Drawing.Color.Lime);

            lAdjNonVisitedVertices = findNotVisitedAdjacentVertices(gGraph, iVertex);

            if (lAdjNonVisitedVertices.Count < 1)
                return iVisitedCtr;

            foreach (int iVertexNext in lAdjNonVisitedVertices)
            {
                //Make sure first vertex index is lower than the second
                iVisitedCtr++;
                if (iVertexNext > iVertex)
                {
                    gGraph.eAdjMatrix[iVertex, iVertexNext].setVisited(true);
                    gGraph.eAdjMatrix[iVertex, iVertexNext].setOrder(iVisitedCtr);
                    gGraph.eAdjMatrix[iVertex, iVertexNext].setLabel(iVisitedCtr.ToString());
                    gGraph.eAdjMatrix[iVertex, iVertexNext].setColor(System.Drawing.Color.Lime);
                }
                else
                {
                    gGraph.eAdjMatrix[iVertexNext, iVertex].setVisited(true);
                    gGraph.eAdjMatrix[iVertexNext, iVertex].setOrder(iVisitedCtr);
                    gGraph.eAdjMatrix[iVertexNext, iVertex].setLabel(iVisitedCtr.ToString());
                    gGraph.eAdjMatrix[iVertexNext, iVertex].setColor(System.Drawing.Color.Lime);
                }

                iVisitedCtr = DFS(gGraph, iVertexNext, iVisitedCtr);
            }

            return iVisitedCtr;
        }

        //Returns a list with the index of each adjacent vertex of iVertex
        public static List<int> findAdjacentVertices(Graph gGraph, int iVertex)
        {
            List<int> lVertices = new List<int>();

            for(int i = 0; i < gGraph.lVertexList.Count; i++)
                if (gGraph.eAdjMatrix[i, iVertex].ExistsEdge())                
                    lVertices.Add(i);                

            return lVertices;
        }

        //Returns a list with the index of each non visited adjacent vertex of iVertex
        public static List<int> findNotVisitedAdjacentVertices(Graph gGraph, int iVertex)
        {
            List<int> lVertices = new List<int>();

            for (int i = iVertex + 1; i < gGraph.lVertexList.Count; i++)
                if (gGraph.eAdjMatrix[iVertex, i].ExistsEdge())
                    if (!gGraph.lVertexList[i].getVisited())                
                        lVertices.Add(i);                

            return lVertices;
        }


    }
}
