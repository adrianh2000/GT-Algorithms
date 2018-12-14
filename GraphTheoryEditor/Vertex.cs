using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace GraphTheoryEditor
{
    class Vertex
    {
        Double dPosX, dPosY, dRadius;
        System.Drawing.Color cVertexColor;
        System.Drawing.Color cLabelColor { get; set; } 
        int iOrder;
        Boolean bVisited;        

        //String sLabel;
        public int iLabel;

        public Vertex(Double dX, Double dY, Double dR, int _iLabel)
        {
            dPosX = dX;
            dPosY = dY;
            if (dR > 0)
                dRadius = dR;
            else
                dR = 30;

            cVertexColor = System.Drawing.Color.Red;
            cLabelColor = System.Drawing.Color.Black;
            bVisited = false;
            iLabel = _iLabel;           
        }

        public void setLabel(int iL)   {  iLabel = iL;  }
        public int getLabel()   {  return iLabel;  }
        public Boolean isLabeled() { return iLabel >= 0; /*negative label indicates emptiness */ }
        public void blankLabel() { iLabel = -1; }

        public void SetRadius(Double dR)
        {
            dRadius = dR;
        }

        public int GetRadius()
        {
            return Convert.ToInt32(dRadius);
        }

        public void SetXY(Double dX, Double dY)
        {
            dPosX = dX;
            dPosY = dY;
        }

        public Double GetX()
        {
            return dPosX;
        }

        public Double GetY()
        {
            return dPosY;
        }

        public void setVertexColor(System.Drawing.Color newColor) { cVertexColor = newColor;  }
        public System.Drawing.Color getVertexColor() { return cVertexColor; }
        public void setOrder(int newOrder) { iOrder = newOrder; }
        public int getOrder() { return iOrder; }
        public void setVisited(Boolean _bVisited) { bVisited = _bVisited; }
        public Boolean getVisited() { return bVisited;  }

        public void DrawVertex(Graphics g)
        {
            //Pen myPen = new Pen(cVertexColor, 5);
            Brush brVertexBrush = new SolidBrush(cVertexColor);
            Brush brStringBrush = new SolidBrush(cLabelColor);
            Font drawFont = new Font("Arial", 16);
            StringFormat drawFormat = new StringFormat();
            drawFormat.FormatFlags = StringFormatFlags.DisplayFormatControl;
            

            //g.DrawEllipse(myPen, Convert.ToInt32(dPosX), Convert.ToInt32(dPosY), Convert.ToInt32(dRadius), Convert.ToInt32(dRadius));
            g.FillEllipse(brVertexBrush, Convert.ToInt32(dPosX - dRadius / 2), Convert.ToInt32(dPosY - dRadius / 2), Convert.ToInt32(dRadius), Convert.ToInt32(dRadius));

            g.DrawString(iLabel.ToString(), drawFont, brStringBrush, (float)(dPosX - dRadius/2), (float)(dPosY - dRadius/2), drawFormat);
        }

        public void DeleteVertex()
        {
            dPosX = -1;
            dPosY = -1;
        }

        public Boolean ExistsVertex()
        {
            return ((dPosY >= 0) && (dPosX >= 0));
        }

        public Boolean MouseInsideVertex(int iMouseX, int iMouseY)
        {
            return ((iMouseX >= dPosX - dRadius) && (iMouseX <= dPosX + dRadius) && (iMouseY >= dPosY - dRadius) && (iMouseY <= dPosY + dRadius));
        }
                
    }
}
