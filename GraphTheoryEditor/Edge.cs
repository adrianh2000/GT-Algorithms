using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace GraphTheoryEditor
{
    public class Edge
    {        
        int iVertex0, iVertex1;
        int iThickness, iOrder;
        String sLabel;
        Double dWeight;        
        System.Drawing.Color cEdgeColor;
        System.Drawing.Color cEdgeLabelColor;
        Boolean bVisited;
        public void setVisited(Boolean _bVisited) { bVisited = _bVisited; }
        public Boolean getVisited() { return bVisited; }

        public Edge(int iV0, int iV1)
        {
            iVertex0 = iV0;
            iVertex1 = iV1;
            iThickness = 2;
            cEdgeColor = System.Drawing.Color.Blue;
            cEdgeLabelColor = System.Drawing.Color.Black;
            dWeight = 1;
            sLabel = "";
            iOrder = -1;
        }

        public Double GetWeight()
        {
            return dWeight;
        }

        public void SetWeight(Double dW)
        {
            dWeight = dW;
        }

        public void SetVertex0(int iV0)
        {
            iVertex0 = iV0;
        }

        public void SetVertex1(int iV1)
        {
            iVertex1 = iV1;
        }

        public void SetVerticesV0V1(int iV0, int iV1)
        {
            iVertex0 = iV0;
            iVertex1 = iV1;
        }

        public int GetVertex0()
        {
            return iVertex0;
        }

        public int GetVertex1()
        {
            return iVertex1;
        }

        public System.Drawing.Color getColor() { return cEdgeColor; }
        public void setColor(System.Drawing.Color newColor) { cEdgeColor = newColor; }

        public System.Drawing.Color getLabelColor() { return cEdgeLabelColor; }
        public void setLabelColor(System.Drawing.Color newColor) { cEdgeLabelColor = newColor; }

        public void setOrder(int newOrder) { iOrder = newOrder; }
        public int getOrder() { return iOrder; }

        public void setLabel(String sNewLabel) { sLabel = sNewLabel; }
        public String getLabel() { return sLabel; }

        public Boolean isLabeled()
        {
            Boolean bResult = sLabel != "";
            return bResult;
        }
        public void DrawEdge(Graphics g, int iPosX0, int iPosY0, int iPosX1, int iPosY1)
        {            
            Pen myPen = new Pen(cEdgeColor, iThickness);
                        
            g.DrawLine(myPen, iPosX0, iPosY0, iPosX1, iPosY1);
            drawEdgeLabel(g, iPosX0, iPosY0, iPosX1, iPosY1);
        }

        public void drawEdgeLabel(Graphics g, int iPosX0, int iPosY0, int iPosX1, int iPosY1)
        {
            //Pen myPen = new Pen(cVertexColor, 5);
            int dPosX = (iPosX0 + iPosX1) / 2;
            int dPosY = (iPosY0 + iPosY1) / 2;
            Brush brStringBrush = new SolidBrush(cEdgeLabelColor);
            Font drawFont = new Font("Arial", 16);
            StringFormat drawFormat = new StringFormat();
            drawFormat.FormatFlags = StringFormatFlags.DisplayFormatControl;

 
            g.DrawString(sLabel.ToString(), drawFont, brStringBrush, (float)(dPosX), (float)(dPosY), drawFormat);
        }

        public void RemoveEdge()
        {
            iVertex0 = -1;
            iVertex1 = -1;
        }
        
        public Boolean ExistsEdge()
        {
            return (iVertex0 != -1) && (iVertex1 != -1);
        }
    }
}
