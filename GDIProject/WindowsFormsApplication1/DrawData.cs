using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
    public class DrawData
    {
        private System.Drawing.Point startPoint;
        private System.Drawing.Point nowPoint;
        private System.Drawing.Pen myPen;
        private int drawMode;
        private System.Drawing.Color color;
        private bool fillFlag;

        public DrawData(System.Drawing.Point startPoint, System.Drawing.Point nowPoint, System.Drawing.Pen myPen, int drawMode)
        {
            // TODO: Complete member initialization
            this.startPoint = startPoint;
            this.nowPoint = nowPoint;
            this.myPen = myPen;
            this.drawMode = drawMode;
        }

        public DrawData(System.Drawing.Point startPoint, System.Drawing.Point nowPoint, System.Drawing.Pen myPen, System.Drawing.Color color, bool fillFlag, int drawMode)
        {
            // TODO: Complete member initialization
            this.startPoint = startPoint;
            this.nowPoint = nowPoint;
            this.myPen = myPen;
            this.color = color;
            this.fillFlag = fillFlag;
            this.drawMode = drawMode;
        }

        internal void drawData(System.Drawing.Graphics graphics)
        {
            throw new NotImplementedException();
        }
    }
}
