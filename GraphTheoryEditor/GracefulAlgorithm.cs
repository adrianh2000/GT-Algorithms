using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GraphTheoryEditor
{
    class GracefulAlgorithm
    {
        public struct edgeLocation
        {
            public int row, col;
        }

        edgeLocation[] lEdgeDFSOrder;
        Graph gGraph;
        List<int[]> lGracefulVertexLabels;
        List<String> lSubLabels;
        int iSubLabelsFileNumber, iSubLabelsCtr;

        public GracefulAlgorithm(Graph _gGraph)
        {
            gGraph = _gGraph;
            lEdgeDFSOrder = new edgeLocation[gGraph.lVertexList.Count-1];
            setAllValues(ref lEdgeDFSOrder, -1);
            lGracefulVertexLabels = new List<int[]>();
            lSubLabels = new List<String>();
            iSubLabelsFileNumber = 0;
            iSubLabelsCtr = 0;
        }

        //set all values of myArray to iValue
        private void setAllValues(ref edgeLocation[] myArray, int iValue)
        {
            for (int i = 0; i < myArray.Length; i++)
            {
                myArray[i].row = iValue;
                myArray[i].col = iValue;
            }
        }

        //Traverse the graph in DFS fashion starting at iVertex
        //return a list for which each element
        //       the index is the edge position in DFS traverse
        //       the value is the edge position in the Adjacency Matrix
        public int DFSEdgesOrderToList(int iVertex, int iVisitedCtr, Boolean bSetLabels = false)
        {
            List<int> lAdjNonVisitedVertices;

            gGraph.lVertexList[iVertex].setOrder(iVisitedCtr);
            gGraph.lVertexList[iVertex].setVisited(true);
            if (bSetLabels)
            {
                gGraph.lVertexList[iVertex].setLabel(iVisitedCtr);
                gGraph.lVertexList[iVertex].setVertexColor(System.Drawing.Color.Lime);
            }

            lAdjNonVisitedVertices = Algorithm.findNotVisitedAdjacentVertices(gGraph, iVertex);

            if (lAdjNonVisitedVertices.Count < 1)
                return iVisitedCtr;

            foreach (int iVertexNext in lAdjNonVisitedVertices)
            {
                //Make sure first vertex index is lower than the second                
                if (iVertexNext > iVertex)
                {
                    gGraph.eAdjMatrix[iVertex, iVertexNext].setVisited(true);
                    gGraph.eAdjMatrix[iVertex, iVertexNext].setOrder(iVisitedCtr);
                    if (bSetLabels)
                    {
                        gGraph.eAdjMatrix[iVertex, iVertexNext].setLabel(iVisitedCtr.ToString());
                        gGraph.eAdjMatrix[iVertex, iVertexNext].setColor(System.Drawing.Color.Lime);
                    }

                    lEdgeDFSOrder[iVisitedCtr].row = iVertex;
                    lEdgeDFSOrder[iVisitedCtr].col = iVertexNext;
                }
                else
                {
                    gGraph.eAdjMatrix[iVertexNext, iVertex].setVisited(true);
                    gGraph.eAdjMatrix[iVertexNext, iVertex].setOrder(iVisitedCtr);

                    if (bSetLabels)
                    {
                        gGraph.eAdjMatrix[iVertexNext, iVertex].setLabel(iVisitedCtr.ToString());
                        gGraph.eAdjMatrix[iVertexNext, iVertex].setColor(System.Drawing.Color.Lime);
                    }

                    lEdgeDFSOrder[iVisitedCtr].row = iVertexNext;
                    lEdgeDFSOrder[iVisitedCtr].col = iVertex;
                }

                iVisitedCtr = DFSEdgesOrderToList(iVertexNext, iVisitedCtr + 1);
            }

            return iVisitedCtr;
        }

        //Find next unlabeled edge
        public edgeLocation findNextUnlabeledEdge(int iStartingOrderIndex)
        {
            edgeLocation elResult = new edgeLocation();

            elResult.row = -1;
            elResult.col = -1;

            for (int i = iStartingOrderIndex + 1; i < lEdgeDFSOrder.Length; i++)
                if (lEdgeDFSOrder[i].row > -1 && lEdgeDFSOrder[i].col > -1)
                {
                    //Edge found
                    elResult = lEdgeDFSOrder[i];
                    break;
                }

            return elResult;
        }

        //Find next unlabeled edge  of particular graph g
        //if bCheckAlsoFromBeginning == true, then it will also check from the beginning if next edge was not found after iStartingOrderIndex
        public int findNextUnlabeledEdgeIndex(Graph g, int iStartingOrderIndex, Boolean bCheckAlsoFromBeginning = true)
        {
            int i, iNextEdgeIndex = -1, iRow, iCol;

            for (i = iStartingOrderIndex + 1; i < lEdgeDFSOrder.Length; i++)
            {
                iRow = lEdgeDFSOrder[i].row;
                iCol = lEdgeDFSOrder[i].col;
                if (iRow > -1 && iCol > -1) //Make sure edge exists
                    if (!g.eAdjMatrix[iRow, iCol].isLabeled())
                    {
                        iNextEdgeIndex = i;
                        break;
                    }
            }

            //if next unalabeled edge was not found, keep searching from the beginning
            if ((iNextEdgeIndex == -1) && (bCheckAlsoFromBeginning))
                for (i = 0; i < iStartingOrderIndex; i++)
                {
                    iRow = lEdgeDFSOrder[i].row;
                    iCol = lEdgeDFSOrder[i].col;
                    if (iRow > -1 && iCol > -1) //Make sure edge exists
                        if (!g.eAdjMatrix[iRow, iCol].isLabeled())
                        {
                            iNextEdgeIndex = i;
                            break;
                        }
                }

            ////if next unabeled edge was not found, see if it is the current edge
            //if(iNextEdgeIndex == -1)
            //{
            //    i = iStartingOrderIndex;
            //    iRow = lEdgeDFSOrder[i].row;
            //    iCol = lEdgeDFSOrder[i].col;
            //    if (iRow > -1 && iCol > -1) //Make sure edge exists
            //        if (!g.eAdjMatrix[iRow, iCol].isLabeled())                    
            //            iNextEdgeIndex = i;
                    
            //}

            return iNextEdgeIndex;
        }

        public void execute(String sPath)
        {
            //Graph gCloneGraph = (Graph)ObjectExtensions.Copy(gGraph);
            int n = gGraph.getNumVertices();
            gGraph.clearAllLabelsAndOrder();

            //Give each vertex's a DFS order starting from vertex 0 (it could be any vertex)
            DFSEdgesOrderToList(0, 0);
            
            //Initialize the graceful list
            lGracefulVertexLabels = new List<int[]>();

            //Loop through all edges in the DFS order
            for(int iEdgeCtr = 0; iEdgeCtr < lEdgeDFSOrder.Length; iEdgeCtr++)
                findAllGracefulLabelings(gGraph, new GracefulMatrix(n), n - 1, iEdgeCtr, sPath);

            //Save last generated sublabels
            saveAllSubLabelingsToFile(sPath, @"Sublabelings N=" + gGraph.getNumVertices().ToString() + "-" + iSubLabelsFileNumber.ToString() + ".txt");
            iSubLabelsFileNumber++;
            lSubLabels.Clear();
        }

        //This method returns the list of graceful labelings of vertices from the last known calculation
        public List<int[]> getAllGracefulLabels()
        {
            return lGracefulVertexLabels;
        }

        //It returns true if any of the absolute value of the differences between the vertex iVertexIndex
        //and its neighbors (except iV0, which is the vertex with the other new label) is greater than or equal to iLevel
        private Boolean isVertexLabelBlocking(Graph g, int iVertexIndex, int iV0, int iLabel, int iLevel)
        {
            int iCol;

            for(iCol = 0; iCol < iVertexIndex; iCol++)                
                if(iCol !=iV0 && g.eAdjMatrix[iVertexIndex, iCol].ExistsEdge())
                    if(g.lVertexList[iCol].isLabeled())
                    {
                        int iLabelCol = g.lVertexList[iCol].getLabel();

                        if (Math.Abs(iLabel - iLabelCol) >= iLevel)
                            return true;
                    }

            for (iCol = iVertexIndex + 1; iCol < g.lVertexList.Count; iCol++)
                if (iCol != iV0 && g.eAdjMatrix[iVertexIndex, iCol].ExistsEdge())
                    if (g.lVertexList[iCol].isLabeled())
                    {
                        int iLabelCol = g.lVertexList[iCol].getLabel();

                        if (Math.Abs(iLabel - iLabelCol) >= iLevel)
                            return true;
                    }

            return false;
        }

        private String getCurrentLabeling(Graph g, int iLevel)
        {
            String sResult = iLevel.ToString() + " | ";

            for (int i = 0; i < g.getNumVertices(); i++)
            {
                if(g.lVertexList[i].isLabeled())
                    sResult += g.lVertexList[i].getLabel().ToString() + " ";
                else
                    sResult += "_ ";
            }

            return "[" + iSubLabelsCtr.ToString("D8") + "] "  + sResult;
        }

        private void saveAllSubLabelingsToFile(String sPath, String sFilename)
        {
            FileStream fs;
            StreamWriter sw;

            fs = new FileStream(sPath + sFilename, FileMode.Create, FileAccess.Write);
            sw = new StreamWriter(fs);

            for (int i = 0; i < lSubLabels.Count; i++)
                sw.WriteLine(lSubLabels[i]);
            sw.Close();            
        }

        private void findAllGracefulLabelings(Graph gTargetGraph, GracefulMatrix gracefulMatrix, int iLevel, int iEdgeOrderIndex, String sPath)
        {
            int iRow = 0, iCol;
            String sCurLabeling = getCurrentLabeling(gTargetGraph, iLevel);

            if (iLevel < 2)
            {
                //debug
                //if (gTargetGraph.lVertexList[1].getLabel() == 6 && gTargetGraph.lVertexList[4].getLabel() == 7)
                //    iRow += 0;

                //Potential Graceful labeling found
                int iUnlabeledEdgeIndex = findNextUnlabeledEdgeIndex(gTargetGraph, 0);

                //if no unlabeled edge was found, use first edge index               
                if(iUnlabeledEdgeIndex < 0)
                    iUnlabeledEdgeIndex = 0;

                iRow = lEdgeDFSOrder[iUnlabeledEdgeIndex].row;
                iCol = lEdgeDFSOrder[iUnlabeledEdgeIndex].col;

                //if one of the adjacent vertices to the unlabeled edge is not labeled but the other one is,
                //see if it is possible to label the unlabeled vertex so that it generates edge label 1
                if(gTargetGraph.lVertexList[iRow].isLabeled() ^ gTargetGraph.lVertexList[iCol].isLabeled())
                {
                    List<GracefulAlgorithm.edgeLocation> allLabelPairsLevel1 = gracefulMatrix.findAvailableLabelPairs(iLevel, gTargetGraph, iRow, iCol);
                    
                    //If a labeling was found, label the vertex to generate level 1
                    if(allLabelPairsLevel1.Count>0)
                    {
                        //There should only be at most 1 vertex label pair
                        GracefulAlgorithm.edgeLocation labelPair = allLabelPairsLevel1[0];

                        //Set edge label
                        gTargetGraph.eAdjMatrix[iRow, iCol].setLabel(iLevel.ToString());

                        //Set vertices labels
                        gTargetGraph.lVertexList[iRow].setLabel(labelPair.row);
                        gTargetGraph.lVertexList[iCol].setLabel(labelPair.col);
                    }
                }

                if(gTargetGraph.lVertexList[iRow].isLabeled() && gTargetGraph.lVertexList[iCol].isLabeled())
                {
                    int iRowLabel = gTargetGraph.lVertexList[iRow].getLabel();
                    int iColLabel = gTargetGraph.lVertexList[iCol].getLabel();

                    if (Math.Abs(iRowLabel - iColLabel) == 1)
                    {
                        //Graceful labeling found!!!!
                        int[] aGracefulVertexLabels = new int[gTargetGraph.getNumVertices()];

                        for (int i = 0; i < aGracefulVertexLabels.Length; i++)
                        {
                            aGracefulVertexLabels[i] = gTargetGraph.lVertexList[i].getLabel();
                        }

                        //Add labeling to list
                        lGracefulVertexLabels.Add(aGracefulVertexLabels);

                        //Add labeling to sublabeling list (for easy debugging)
                        sCurLabeling = getCurrentLabeling(gTargetGraph, iLevel);

                        lSubLabels.Add(sCurLabeling + " *Graceful*");
                        return;
                    }
                }

                //Graceful labeling not found. Kill branch
                return;
            }
                        
            iRow = lEdgeDFSOrder[iEdgeOrderIndex].row;
            iCol = lEdgeDFSOrder[iEdgeOrderIndex].col;

            //-----------------------------------------------------------------------------------------------
            //- Find pairs that match the current labeled vertex                                            -
            //- if pairs are found, create execution tree for each pair                                     -
            //-----------------------------------------------------------------------------------------------
            List<GracefulAlgorithm.edgeLocation> allLabelPairs = gracefulMatrix.findAvailableLabelPairs(iLevel, gTargetGraph, iRow, iCol);
   
            //If no pairs were found, then execution branch dies since it is not longer possible to find a graceful labeling for the branch
            //This may not be needed
            if (allLabelPairs.Count < 1)
                return;

            foreach (GracefulAlgorithm.edgeLocation labelPair in allLabelPairs) {
                 //Clone graph and Graceful Matrix by Deep Copy
                Graph g = (Graph)ObjectExtensions.Copy(gTargetGraph);
                GracefulMatrix gm = (GracefulMatrix)ObjectExtensions.Copy(gracefulMatrix);
                Boolean bWasRowLabeled = g.lVertexList[iRow].isLabeled(), bWasColLabeled = g.lVertexList[iCol].isLabeled();

                //Set edge label
                g.eAdjMatrix[iRow, iCol].setLabel(iLevel.ToString());

                //Set vertices labels
                g.lVertexList[iRow].setLabel(labelPair.row);
                g.lVertexList[iCol].setLabel(labelPair.col);

                //--------------- write execution tree to disk into files --------------
                //sCurLabeling = getCurrentLabeling(g, iLevel);
                //lSubLabels.Add(sCurLabeling);
                //iSubLabelsCtr++;

                ////if (iSubLabelsCtr == 294)
                ////    iSubLabelsCtr += 0;

                //if (lSubLabels.Count >= 500)
                //{                    
                //    saveAllSubLabelingsToFile(sPath, @"Sublabelings N=" + g.getNumVertices().ToString() + "-" + iSubLabelsFileNumber.ToString() + ".txt");
                //    iSubLabelsFileNumber++;
                //    lSubLabels.Clear();
                //}

                //--------------------------------------------------

                //Find out if any of those new adjacencies includes an edge label >= level. If so, kill branch
                if (!bWasRowLabeled && isVertexLabelBlocking(g, iRow, iCol, labelPair.row, iLevel))
                    continue;

                if (!bWasColLabeled && isVertexLabelBlocking(g, iCol, iRow, labelPair.col, iLevel))
                    continue;
                
                //Lock pair
                gm.lockPair(labelPair.row, labelPair.col);

                //Lock entire label for iRow if no more unlabeled adjacent vertices exist
                if (g.findAdjacentUnlabeledVertices(iRow).Count < 1)
                    gm.lockVertexLabel(labelPair.row);



                //Lock neighbors of iRow except iCol that have already been labeled and have no more unlabeled adjacent vertices
                List<int> lAdjVerticesRow = g.findAdjacentVertices(iRow);
                foreach (int iVertexAdjIndex in lAdjVerticesRow)
                    if (iVertexAdjIndex != iCol && g.lVertexList[iVertexAdjIndex].isLabeled())
                        if (g.findAdjacentUnlabeledVertices(iVertexAdjIndex).Count < 1)
                            gm.lockVertexLabel(g.lVertexList[iVertexAdjIndex].getLabel());

                //Lock entire label for iCol if no more unlabeled adjacent vertices exist
                if (g.findAdjacentUnlabeledVertices(iCol).Count < 1)
                    gm.lockVertexLabel(labelPair.col);

                //Lock neighbors of iCol except iRow that have already been labeled and have no more unlabeled adjacent vertices
                List<int> lAdjVerticesCol = g.findAdjacentVertices(iCol);
                foreach (int iVertexAdjIndex in lAdjVerticesCol)
                    if (iVertexAdjIndex != iRow && g.lVertexList[iVertexAdjIndex].isLabeled())
                        if (g.findAdjacentUnlabeledVertices(iVertexAdjIndex).Count < 1)
                            gm.lockVertexLabel(g.lVertexList[iVertexAdjIndex].getLabel());

                //Traverse all unlabeled edges in DFS order and recursively call the findAllGracefulLabelings method for each edge
                for (int iEdgeCtr = 0; iEdgeCtr < lEdgeDFSOrder.Length; iEdgeCtr++)
                {
                    int iRowEdge = lEdgeDFSOrder[iEdgeCtr].row;
                    int iColEdge = lEdgeDFSOrder[iEdgeCtr].col;

                    if(!g.eAdjMatrix[iRowEdge, iColEdge].isLabeled())
                        findAllGracefulLabelings(g, gm, iLevel - 1, iEdgeCtr, sPath);
                }

            }




        }
    }
}
