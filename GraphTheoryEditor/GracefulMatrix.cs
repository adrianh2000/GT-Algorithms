using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GraphTheoryEditor
{
    class GracefulMatrix
    {
        //This matrix contains the vertex labels necessary to obtain a specific edge label.
        //The row and column represent the vertex labels.
        //Given Vr = Vertex Row and Vc = Vertex Column
        //gracefulmatrix[Vr, Vc] = ABS(Vr - Vc) or -1 if Vr or Vc are unavailable
        int[,] gracefulMatrix;

        public GracefulMatrix(int n)
        {
            gracefulMatrix = new int[n, n];

            init();
        }

        public void init()
        { 
            for (int r = 0; r <= gracefulMatrix.GetUpperBound(0); r++)
                for (int c = 0; c <= gracefulMatrix.GetUpperBound(1); c++)
                    gracefulMatrix[r, c] = Math.Abs(r - c);            
        }

        public void printMatrixToFile()
        {
            String sFilename = @"I:\Research\Graph Theory\Graceful\Find all graceful labels for any tree algorithm\GraphTheoryEditorWithFindAllGracefulLabelings\GraphTheoryEditor\Output\output.txt";
            FileStream fs;
            StreamWriter sw;

            try
            {
                    
                fs = new FileStream(sFilename, FileMode.Create, FileAccess.Write);
                sw = new StreamWriter(fs);

                
                for (int r = 0; r < gracefulMatrix.GetUpperBound(0); r++)
                {
                    for (int c = 0; c < gracefulMatrix.GetUpperBound(1); c++)
                        sw.Write(gracefulMatrix[r, c] + " ");

                    sw.WriteLine();
                }


                sw.Close();
                
            }
            catch (Exception)
            {
                throw new ApplicationException("Failed saving graph");
            }
        }

        //This methods makes a vertex label unavailable by blanking out all the rows and columns using that label
        //It is mostly used when a label is placed on a vertex for which has all its adjacent vertices have already been labeled
        public void lockVertexLabel(int iVertexLabel)
        {
            int i;

            for (i = 0; i <= gracefulMatrix.GetUpperBound(0); i++)
            {
                gracefulMatrix[iVertexLabel, i] = -1;
                gracefulMatrix[i, iVertexLabel] = -1;
            }
        }

        public void lockPair(int iRow, int iCol)
        {
            gracefulMatrix[iRow, iCol] = -1;
        }

        //Returns a list with all pairs of available vertex labels that generate the edge label iEdgeLabel
        public List<GracefulAlgorithm.edgeLocation> findAvailableLabelPairs(int iEdgeLabel, Graph gGraph)
        {
            List<GracefulAlgorithm.edgeLocation> liAllPairs = new List<GracefulAlgorithm.edgeLocation>();
            
            int i, n = gracefulMatrix.GetUpperBound(0), iRowLabel, iColLabel;

            //Top triangle
            for (i = 0; i <= n - iEdgeLabel; i++)
                if (gracefulMatrix[i, i + iEdgeLabel] > -1)
                {
                    iRowLabel = i;
                    iColLabel = i + iEdgeLabel;
                    if(!gGraph.isAnyVertexLabelOfPairUsed(iRowLabel, iColLabel))  //Make sure none of the vertex labels in the pair have been used
                    {
                        GracefulAlgorithm.edgeLocation vertexLabelPair;
                        vertexLabelPair.row = iRowLabel;
                        vertexLabelPair.col = iColLabel;
                        liAllPairs.Add(vertexLabelPair);
                    }
                }

            //Bottom triangle
            for (i = 0; i <= n - iEdgeLabel; i++)
                if (gracefulMatrix[i + iEdgeLabel, i] > -1)
                {
                    iRowLabel = i + iEdgeLabel;
                    iColLabel = i;
                    if (!gGraph.isAnyVertexLabelOfPairUsed(iRowLabel, iColLabel))  //Make sure none of the vertex labels in the pair have been used
                    {
                        GracefulAlgorithm.edgeLocation vertexLabelPair;
                        vertexLabelPair.row = iRowLabel;
                        vertexLabelPair.col = iColLabel;
                        liAllPairs.Add(vertexLabelPair);
                    }
                }

            return liAllPairs;
        }

        //to label an edge, we need to find out if its vertices have already been labeled 
        //it returns:
        //          -1 if the edge cannot be labeled with iLabel
        //           0 if the edge can be labeled with iLabel
        //          >0 if the edge can be labeled with iLabel but one of the vertices has
        //             already been used. Then the return value is the label needed for the unlabeled vertex (may be more than 1).
        public int canLabelEdge(Graph gGraph, int row, int col, int iEdgeLabel)
        {
            List<GracefulAlgorithm.edgeLocation> lAllVertexLabelPairs = findAvailableLabelPairs(iEdgeLabel, gGraph);
            
            return -1;
        }

        //Returns a list with all pairs of available vertex labels that generate the edge label iEdgeLabel
        //Considering that one of the vertices may have already been labeled
        public List<GracefulAlgorithm.edgeLocation> findAvailableLabelPairs(int iEdgeLabel, Graph gGraph, int iVertexRow, int iVertexCol)
        {
            List<GracefulAlgorithm.edgeLocation> liAllPairs = new List<GracefulAlgorithm.edgeLocation>();
            int iVertexLabelUsedRow = -1, iVertexLabelUsedCol = -1;

            if (gGraph.lVertexList[iVertexRow].isLabeled())
            {                
                if (gGraph.lVertexList[iVertexCol].isLabeled())
                    return liAllPairs; //if both vertices are labeled, then the edge cannot be labeled and thus, we return an empty list
                else
                    iVertexLabelUsedRow = (gGraph.lVertexList[iVertexRow].getLabel());
            }
            else
                if (gGraph.lVertexList[iVertexCol].isLabeled())
                    iVertexLabelUsedCol = (gGraph.lVertexList[iVertexCol].getLabel());
                else
                    return findAvailableLabelPairs(iEdgeLabel, gGraph); //if iVertex1 and iVertex2 are unlabeled, then return all the pairs

            //If we reach this line is because either iVertexRow or iVertexCol are labeled, but NOT both.
            //Up to 2 pair of vertex labels can be available.

            //When the Vertex Row is already labeled
            if(iVertexLabelUsedRow > -1)
            {
                int iNextVertexLabel = iVertexLabelUsedRow - iEdgeLabel;

                //Verify that next vertex label is available
                if(iNextVertexLabel >-1 && iNextVertexLabel < gGraph.lVertexList.Count)
                    if (gracefulMatrix[iVertexLabelUsedRow, iNextVertexLabel] > -1)
                    {
                        GracefulAlgorithm.edgeLocation vertexLabelPair;
                        vertexLabelPair.row = iVertexLabelUsedRow;
                        vertexLabelPair.col = iNextVertexLabel;
                        liAllPairs.Add(vertexLabelPair);
                    }

                iNextVertexLabel = iVertexLabelUsedRow + iEdgeLabel;

                //Verify that next vertex label is available
                if (iNextVertexLabel > -1 && iNextVertexLabel < gGraph.lVertexList.Count)
                    if (gracefulMatrix[iVertexLabelUsedRow, iNextVertexLabel] > -1)
                    {
                        GracefulAlgorithm.edgeLocation vertexLabelPair;
                        vertexLabelPair.row = iVertexLabelUsedRow;
                        vertexLabelPair.col = iNextVertexLabel;
                        liAllPairs.Add(vertexLabelPair);
                    }
            }
            else
            {
                //when the Vertex Col is already labeled
                int iNextVertexLabel = iVertexLabelUsedCol - iEdgeLabel;

                //Verify that next vertex label is available
                if (iNextVertexLabel > -1 && iNextVertexLabel < gGraph.lVertexList.Count)
                    if (gracefulMatrix[iNextVertexLabel, iVertexLabelUsedCol] > -1)
                    {
                        GracefulAlgorithm.edgeLocation vertexLabelPair;
                        vertexLabelPair.row = iNextVertexLabel;
                        vertexLabelPair.col = iVertexLabelUsedCol;
                        liAllPairs.Add(vertexLabelPair);
                    }

                iNextVertexLabel = iVertexLabelUsedCol + iEdgeLabel;

                //Verify that next vertex label is available
                if (iNextVertexLabel > -1 && iNextVertexLabel < gGraph.lVertexList.Count)
                    if (gracefulMatrix[iNextVertexLabel, iVertexLabelUsedCol] > -1)
                    {
                        GracefulAlgorithm.edgeLocation vertexLabelPair;
                        vertexLabelPair.row = iNextVertexLabel;
                        vertexLabelPair.col = iVertexLabelUsedCol;
                        liAllPairs.Add(vertexLabelPair);
                    }
            }

            return liAllPairs;
        }

    }
}
