using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace GDIProject
{
    public class GDIManager
    {
        private Pen normalPen = new Pen(Color.Black);
        public Pen NormalPen
        {
            get { return guidePen; }
            set { guidePen = value; }
        }

        private Pen guidePen = new Pen(Color.Blue, 2);
        public Pen GuidePen
        {
            get
            {
                guidePen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                return guidePen;
            }

            set { guidePen = value; }
        }

        public bool ShiftKeyDown { get; set; }

        public Rectangle GetGuideRect(Rectangle figure)
        {
            Rectangle guide_figure = new Rectangle(figure.X - halfPoint, figure.Y - 6, figure.Width + pointSize, figure.Height + pointSize);

            return guide_figure;
        }

        int pointSize = 12;
        int halfPoint = 6;
        List<Rectangle> ResizePointList = new List<Rectangle>();
        public List<Rectangle> GetSizeRect(Rectangle item)
        {            
            int halfWidth = item.Width / 2;
            int halfHeight = item.Height / 2;

            ResizePointList.Clear();
            // 각 모서리
            ResizePointList.Add(new Rectangle(item.X - halfPoint, item.Y - halfPoint, pointSize, pointSize)); // 상좌
            ResizePointList.Add(new Rectangle(item.X + item.Width - halfPoint, item.Y - halfPoint, pointSize, pointSize)); // 상우
            ResizePointList.Add(new Rectangle(item.X - halfPoint, item.Y + item.Height - halfPoint, pointSize, pointSize)); // 하좌
            ResizePointList.Add(new Rectangle(item.X + item.Width - halfPoint, item.Y + item.Height - halfPoint, pointSize, pointSize)); // 하우

            // 상하좌우
            ResizePointList.Add(new Rectangle(item.X + halfWidth - halfPoint, item.Y - halfPoint, pointSize, pointSize)); // 상
            ResizePointList.Add(new Rectangle(item.X + halfWidth - halfPoint, item.Y + item.Height - halfPoint, pointSize, pointSize)); // 하
            ResizePointList.Add(new Rectangle(item.X - halfPoint, item.Y + halfHeight - halfPoint, pointSize, pointSize)); // 좌
            ResizePointList.Add(new Rectangle(item.X + item.Width - halfPoint, item.Y + halfHeight - halfPoint, pointSize, pointSize)); // 우

            return ResizePointList;
        }

        /// <summary>
        /// Revise select Position when figures clicked
        /// </summary>
        /// <param name="startPoint"></param>
        /// <param name="selectPoint"></param>
        /// <returns></returns>
        private Point RevisePosition(Point startPoint, Point selectPoint)
        {
            Point results = new Point();
            int x_pos = selectPoint.X - startPoint.X;
            int y_pos = selectPoint.Y - startPoint.Y;

            results.X = selectPoint.X + x_pos;
            results.Y = selectPoint.Y + y_pos;

            return results;
        }

        /// <summary>
        /// Check mouse Point, Return true when mouse point is in the figure
        /// </summary>
        /// <param name="mouse"></param>
        /// <param name="rectangle"></param>
        /// <returns></returns>
        public bool isInside(Point mouse, Rectangle rectangle)
        {
            bool isInside = false;

            Rectangle _rect = rectangle;

            if (mouse.X > _rect.X && mouse.X < _rect.X + _rect.Width && mouse.Y > _rect.Y && mouse.Y < _rect.Y + _rect.Width)
            {
                isInside = true;
            }

            return isInside;
        }

        /// <summary>
        /// 마우스 포인터가 생성된 FigureInfo 객체 내에 있는지 확인하여 해당 객체를 반환
        /// </summary>
        /// <param name="mouse"></param>
        /// <param name="rectangle"></param>
        /// <returns></returns>
        public FigureInfo isInside(Point mouse)
        {   
            foreach (FigureInfo figureItem in FigureList)
            {
                if (mouse.X > figureItem.Rectangle.X && mouse.X < figureItem.Rectangle.X + figureItem.Rectangle.Width && mouse.Y > figureItem.Rectangle.Y && mouse.Y < figureItem.Rectangle.Y + figureItem.Rectangle.Width)
                {
                    SelectedFigure = figureItem;
                }
            }

            return SelectedFigure;
        }
        
        private List<FigureInfo> figureList = new List<FigureInfo>();
        public List<FigureInfo> FigureList
        {
            get { return figureList; }
            set {
                figureList = value;
                if (figureList != null && figureList.Count > 0)
                {
                    SelectedFigure = figureList[0];
                }
            }
        }

        List<FigureInfo> selectedFigureList = new List<FigureInfo>();
        public List<FigureInfo> SelectedFigureList
        {
            get { return selectedFigureList; }
            set {selectedFigureList = value;}
        }

        /// <summary>
        /// get & set Current Figure
        /// </summary>
        public FigureInfo SelectedFigure { get; set; }

        public Cursor getMouseCursor(Point point)
        {
            Cursor cursor = Cursors.Default;

            try
            {                
                if (ResizePointList.Count == 0) return cursor;
               
                if (isInside(point, ResizePointList[0]))
                {
                    SizeDirection = SIZE_DIRECTION.TopLeft;
                    cursor = Cursors.SizeNWSE;
                }
                else if (isInside(point, ResizePointList[1]))
                {
                    SizeDirection = SIZE_DIRECTION.TopRight;
                    cursor = Cursors.SizeNESW;
                }
                else if (isInside(point, ResizePointList[2]))
                {
                    SizeDirection = SIZE_DIRECTION.BottomLeft;
                    cursor = Cursors.SizeNESW;
                }
                else if (isInside(point, ResizePointList[3]))
                {
                    SizeDirection = SIZE_DIRECTION.BottomRight;
                    cursor = Cursors.SizeNWSE;
                }
                else if (isInside(point, ResizePointList[4]))
                {
                    SizeDirection = SIZE_DIRECTION.Top;
                    cursor = Cursors.SizeNS;
                }
                else if (isInside(point, ResizePointList[5]))
                {
                    SizeDirection = SIZE_DIRECTION.Bottom;
                    cursor = Cursors.SizeNS;
                }
                else if (isInside(point, ResizePointList[6]))
                {
                    SizeDirection = SIZE_DIRECTION.Left;
                    cursor = Cursors.SizeWE;
                }
                else if (isInside(point, ResizePointList[7]))
                {
                    SizeDirection = SIZE_DIRECTION.Right;
                    cursor = Cursors.SizeWE;
                }
                else if (SelectedFigure != null && isInside(point, SelectedFigure.Rectangle))
                {
                    cursor = Cursors.SizeAll;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + ex.StackTrace);
            }

            return cursor;
        }

        public void GetDirection(Point mouse, Point rect)
        {            
            switch (SizeDirection)
            {
                case SIZE_DIRECTION.TopLeft :
                    if (mouse.X > rect.X + SelectedFigure.Rectangle.Width && mouse.Y > rect.Y + SelectedFigure.Rectangle.Height)
                    {
                        SizeDirection = SIZE_DIRECTION.BottomRight;
                    }
                    else if(mouse.Y > rect.Y + SelectedFigure.Rectangle.Height) 
                    {
                        SizeDirection = SIZE_DIRECTION.BottomLeft;
                    }
                    else if(mouse.X > rect.X + SelectedFigure.Rectangle.Width)
                    {
                        SizeDirection = SIZE_DIRECTION.TopRight;
                    }
                    break;
                case SIZE_DIRECTION.TopRight :
                    if (mouse.X < rect.X && mouse.Y > rect.Y)
                    {
                        SizeDirection = SIZE_DIRECTION.BottomLeft;
                    }
                    else if (mouse.Y > rect.Y + SelectedFigure.Rectangle.Height) 
                    {
                        SizeDirection = SIZE_DIRECTION.BottomRight;
                    }
                    else if (mouse.X < rect.X)
                    {
                        SizeDirection = SIZE_DIRECTION.TopLeft;
                    }
                    break;
                case SIZE_DIRECTION.BottomLeft :
                    if (mouse.X > rect.X && mouse.Y < rect.Y)
                    {
                        SizeDirection = SIZE_DIRECTION.TopRight;
                    }
                    else if (mouse.Y < rect.Y)
                    {
                        SizeDirection = SIZE_DIRECTION.TopLeft;
                    }
                    else if (mouse.X > rect.X + SelectedFigure.Rectangle.Width)
                    {
                        SizeDirection = SIZE_DIRECTION.BottomRight;
                    }
                    break;
                case SIZE_DIRECTION.BottomRight:
                    if (mouse.X < rect.X && mouse.Y < rect.Y)
                    {
                        SizeDirection = SIZE_DIRECTION.TopLeft;
                    }
                    else if (mouse.Y < rect.Y)
                    {
                        SizeDirection = SIZE_DIRECTION.TopRight;
                    }
                    else if (mouse.X < rect.X)
                    {
                        SizeDirection = SIZE_DIRECTION.BottomLeft;
                    }
                    break;
                case SIZE_DIRECTION.Top :
                    if (mouse.Y > rect.Y + SelectedFigure.Rectangle.Height)
                    {
                        SizeDirection = SIZE_DIRECTION.Bottom;
                    }
                    break;
                case SIZE_DIRECTION.Bottom :
                    if (mouse.Y < rect.Y)
                    {
                        SizeDirection = SIZE_DIRECTION.Top;
                    }
                    break;
                case SIZE_DIRECTION.Left :
                    if (mouse.X > rect.X + SelectedFigure.Rectangle.Width)
                    {
                        SizeDirection = SIZE_DIRECTION.Right;
                    }
                    break;
                case SIZE_DIRECTION.Right:
                    if (mouse.X < rect.X)
                    {
                        SizeDirection = SIZE_DIRECTION.Left;
                    }
                    break;
            }         
        }

        Point StartPoint;
        Point CurrentPoint;
        public Rectangle ResizeRectangle(MouseEventArgs e)
        {
            GetDirection(e.Location, SelectedFigure.Rectangle.Location);

            switch (SizeDirection)
            {
                case SIZE_DIRECTION.TopLeft:
                    StartPoint = new Point(SelectedFigure.Rectangle.X + SelectedFigure.Rectangle.Width, SelectedFigure.Rectangle.Y + SelectedFigure.Rectangle.Height);
                    break;
                case SIZE_DIRECTION.TopRight:
                    StartPoint = new Point(SelectedFigure.Rectangle.X, SelectedFigure.Rectangle.Y + SelectedFigure.Rectangle.Height);                    
                    break;
                case SIZE_DIRECTION.BottomLeft:
                    StartPoint = new Point(SelectedFigure.Rectangle.X + SelectedFigure.Rectangle.Width, SelectedFigure.Rectangle.Y);                    
                    break;                
                case SIZE_DIRECTION.Top :
                    StartPoint = new Point(SelectedFigure.Rectangle.X, SelectedFigure.Rectangle.Y + SelectedFigure.Rectangle.Height);
                    break;
                case SIZE_DIRECTION.Left :
                    StartPoint = new Point(SelectedFigure.Rectangle.X + SelectedFigure.Rectangle.Width, SelectedFigure.Rectangle.Y);
                    break;                
                case SIZE_DIRECTION.Bottom:
                case SIZE_DIRECTION.Right:
                case SIZE_DIRECTION.BottomRight:
                    StartPoint = SelectedFigure.Rectangle.Location;
                    break;
            }

            switch (SizeDirection)
            {
                case SIZE_DIRECTION.TopLeft :
                case SIZE_DIRECTION.TopRight:
                case SIZE_DIRECTION.BottomLeft:
                case SIZE_DIRECTION.BottomRight:
                    CurrentPoint = e.Location;
                    break;
                case SIZE_DIRECTION.Top:
                case SIZE_DIRECTION.Bottom:
                    CurrentPoint = new Point(StartPoint.X + SelectedFigure.Rectangle.Width,  e.Location.Y);
                    break;
                case SIZE_DIRECTION.Left:
                case SIZE_DIRECTION.Right:
                    CurrentPoint = new Point(e.Location.X, StartPoint.Y + SelectedFigure.Rectangle.Height);
                    break;
            }
                    
            Rectangle rect = getRectangle();
            //Console.WriteLine("start X : {0}, start Y : {1}, current X : {2}, current Y : {3}, ", StartPoint.X, StartPoint.Y, CurrentPoint.X, CurrentPoint.Y);
            //Console.WriteLine("X : {0}, Y : {1}, W : {2}, H : {3}, ", rect.X, rect.Y, rect.Width, rect.Height);

            return getRectangle();
        }
        
        private Rectangle getRectangle()
        {
            return new Rectangle(
                Math.Min(StartPoint.X, CurrentPoint.X),
                Math.Min(StartPoint.Y, CurrentPoint.Y),
                Math.Abs(StartPoint.X - CurrentPoint.X),
                Math.Abs(StartPoint.Y - CurrentPoint.Y));            
        }

        public Point RevisePoint { get; set; }
        public Rectangle RevisePosition(MouseEventArgs e)
        {
            Rectangle rect = SelectedFigure.Rectangle;

            rect.X = e.X - RevisePoint.X;
            rect.Y = e.Y - RevisePoint.Y;

            return rect;
        }

        public SIZE_DIRECTION SizeDirection
        {
            get;
            set;
        }

        public Pen SettingPen(Color color, int Width = 1)
        {
            Pen pen = new Pen(Color.Black);
            if (!color.IsEmpty)
            {
                pen.Color = color;
            }
            
            pen.Width = Width;

            return pen;
        }
    }

    /// <summary>
    /// Form3, 4에서 사용 삭제 예정
    /// </summary>
    public enum MouseModifyMode
    {
        Default = 0, SelectMode = 1, DrawMode = 2, ResizeMode = 3
    }

    public enum SIZE_DIRECTION
        {
            TopLeft = 0,
            TopRight = 1,
            BottomLeft = 2,
            BottomRight = 3,
            Top = 4,
            Right = 5,
            Left = 6,
            Bottom = 7
        }

    public class FigureInfo
    {
        /// <summary>
        /// Rectangle & Ellipse
        /// </summary>
        public Rectangle Rectangle { get; set; }

        public Image Image { get; set; }

        /// <summary>
        /// 0 : Rectrangle, 1: Ellipse, 2 : Image
        /// </summary>
        public int FigureType { get; set; }

        public string id { get; set; }

        public Font Font { get; set; }

        public Color BorderColor { get; set; }

        public Color BackgroundColor { get; set; }

        public List<string> menuList { get; set; }

        public bool isEditable { get; set; }        
    }
}
