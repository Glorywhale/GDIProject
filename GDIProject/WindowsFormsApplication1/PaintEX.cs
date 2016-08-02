
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace WindowsFormsApplication1
{
    public partial class PaintEX : Form
    {

        int drawMode, CurveCnt = 0; // 그리기 모드와 곡선의 커브 개수 저장
        Point startPoint, nowPoint; // 시작 좌표와 현재 좌표
        Pen myPen;                  // 선을 그릴 때 사용
        Brush myBrush;              // 원과 사각형 그릴 때 사용
        ArrayList saveData;         // 커브가 생길 때 마다 저장, 취소 전용
        ArrayList saveCurve;        // 커브 좌표가 누적되어 저장, 그리기 전용
        ArrayList saveLine, saveCircle, saveRectangle;
        // 선, 원, 사각형의 정보 저장, 그리기 전용
        bool fillFlag;              // 채우기 여부 선택

        int delCnt = 0;              // 커브 개수를 저장할 배열의 위치 값
        int[] delArr = new int[100]; // 커브 개수를 저장
        int delVal = 0;              // 커브의 총 개수를 저장

        public PaintEX()
        {
            InitializeComponent();

            drawMode = 1;                 // 초기 선 그리기 모드로 시작
            fillFlag = false;             // 선이기 때문에 채우기 모드는 false
            myPen = new Pen(Color.Black); // 선과 곡선의 초기 색상은 검정색

            saveData = new ArrayList();
            saveLine = new ArrayList();
            saveCircle = new ArrayList();
            saveRectangle = new ArrayList();
            saveCurve = new ArrayList();   // ArrayList들의 객체 생성
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Invalidate();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);

            // 그림이 잔상을 남기지 않도록 설정
            // UserPaint가 True 값을 가질 때만 그리도록 설정

            ResizeRedraw = true; // 크기 조정 시 다시 그림

        }

        private void 선ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            drawMode = 1; // 그리기 모드 1
            Invalidate();
            Update();
        }

        private void 원ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            drawMode = 2; // 그리기 모드 2
            Invalidate();
            Update();
        }

        private void 사각형ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            drawMode = 3; // 그리기 모드 3
            Invalidate();
            Update();
        }

        private void 곡선ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            drawMode = 4; // 그리기 모드 4
            Invalidate();
            Update();
        }

        private void 화면삭제ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveData.Clear();
            saveLine.Clear();
            saveCircle.Clear();
            saveRectangle.Clear();
            saveCurve.Clear();    // 모든 ArrayList의 값을 비워줌

            fillFlag = false;
            Invalidate();
            Update();
        }

        private void 종료ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close(); // 현재의 창을 닫음
        }

        private void 펜색ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            colorPen.ShowDialog();        // 다이얼로그 띄움
            myPen.Color = colorPen.Color; // 선택된 색상 가져옴
        }

        private void 색선택ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fillFlag = true;          // 채우기 모드 true
            colorBrush.ShowDialog();  // 다이얼로그 띄움
            myBrush = new SolidBrush(colorBrush.Color); // 선택된 색상 가져옴
        }

        private void 널브러시ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fillFlag = false; // 색상이 채워지지 않는 도형을 그림
        }

        private void 굵은선ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            myPen.Width = 5;
        }

        private void 보통선ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            myPen.Width = 3;
        }

        private void 가는선ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            myPen.Width = 1;
        }

        // 그리기 모드에 맞는 번호가 선택될 경우에 따른 취소 여부 결정
        private void 선ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (saveLine.Count > 0) // 선이 있을 경우 하나 제거
            {
                saveLine.RemoveAt(saveLine.Count - 1);
                Invalidate();
                Update();
            }
        }

        private void 원ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (saveCircle.Count > 0) // 원을 하나 제거
            {
                saveCircle.RemoveAt(saveCircle.Count - 1);
                Invalidate();
                Update();
            }
        }

        private void 사각형ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (saveRectangle.Count > 0) // 사각형을 하나 제거
            {
                saveRectangle.RemoveAt(saveRectangle.Count - 1);
                Invalidate();
                Update();
            }
        }

        private void 곡선ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (delCnt > 0) // 곡선을 하나 제거
            {
                for (int i = 0; i < delArr[delCnt - 1]; i++)
                { // 값 저장과 동시에 delCnt를 증가 시키기 때문에 감소한 위치에서 연산
                    saveData.RemoveAt(saveData.Count - 1); // 곡선 제거
                }
                Invalidate();
                Update();
                delCnt--; // 배열의 크기도 하나 감소
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            Cursor = Cursors.Cross;         // 마우스 클릭 시 커서 변화
            nowPoint = new Point(e.X, e.Y); // 현재 포인터 추출
            startPoint = nowPoint;          // 시작 포인터로 설정
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            nowPoint = new Point(e.X, e.Y); // 현재 포인터 추출
            Graphics g = CreateGraphics();  // 그리기 객체 생성
            Invalidate();
            Update();
            Rectangle rect;

            switch (drawMode)
            {
                case 1:
                    g.DrawLine(myPen, startPoint, nowPoint);
                    break; // 현재 펜 설정과 좌표를 DrawData 클래스로 전달

                case 2:
                    rect = new Rectangle(startPoint.X, startPoint.Y,
                        nowPoint.X - startPoint.X, nowPoint.Y - startPoint.Y);

                    if (fillFlag) // FillFlag 모드에 따라 펜이나 브러쉬로 그림
                        g.FillEllipse(myBrush, rect);
                    else
                        g.DrawEllipse(myPen, rect);
                    break;

                case 3:
                    rect = new Rectangle(startPoint.X, startPoint.Y,
                        nowPoint.X - startPoint.X, nowPoint.Y - startPoint.Y);

                    if (fillFlag) // FillFlag 모드에 따라 펜이나 브러쉬로 그림
                        g.FillRectangle(myBrush, rect);
                    else
                        g.DrawRectangle(myPen, rect);
                    break;

                case 4:
                    g.DrawLine(myPen, startPoint, nowPoint);
                    DrawData inputData = new DrawData(startPoint, nowPoint, myPen, drawMode);
                    saveData.Add(inputData);
                    CurveCnt++;
                    startPoint = nowPoint;
                    delVal = Convert.ToInt32(saveData.Count.ToString());
                    // 현재 작업한 커브 개수를 저장(누적)

                    break;
                // DrawData 객체를 생성해 ArrayList에 담고 커브의 숫자를 계속 해서 증가
                // 현재 좌표를 추출해 시작 좌표로 설정
            }

            g.Dispose();

        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            int c = delVal; // 연산을 위해 누적된 커브 개수를 임시 변수에 담음

            if (delCnt == 0)
            {
                delArr[delCnt] = delVal; // 첫 번째 값은 그대로 저장
            }

            else if (delCnt > 0)
            {
                foreach (int d in delArr)
                {
                    c -= d;
                    // 총 커브 횟수에서 이미 저장된 커브 개수들을 빼면
                    // 최근에 작업했던 커브 횟수를 알 수 있음
                }

                delArr[delCnt] = c; // 연산된 값을 배열에 저장
            }

            delCnt++; // 배열의 위치를 하나 증가
            c = 0;    // 임시 변수를 초기화

            if (e.Button != MouseButtons.Left)
                return;

            DrawData inputData; // 객체 생성

            switch (drawMode)
            {
                // 각각의 번호에 맞는 객체를 생성해 각각의 ArrayList에 추가함
                case 1:
                    inputData = new DrawData(startPoint, nowPoint, myPen, drawMode);
                    saveLine.Add(inputData);
                    break;

                case 2:
                    inputData = new DrawData(startPoint, nowPoint, myPen,
                        colorBrush.Color, fillFlag, drawMode);
                    saveCircle.Add(inputData);
                    break;

                case 3:
                    inputData = new DrawData(startPoint, nowPoint, myPen,
                        colorBrush.Color, fillFlag, drawMode);
                    saveRectangle.Add(inputData);
                    break;

                case 4:
                    inputData = new DrawData(startPoint, nowPoint, myPen,
                        colorBrush.Color, fillFlag, drawMode);
                    saveCurve.Add(inputData);
                    break;
            }
            Cursor = Cursors.Arrow; // 커서는 화살표로
        }

        protected override void OnPaint(PaintEventArgs e)
        {   // OnPaint 재정의

            Graphics g = e.Graphics; // 그리기 객체 생성

            // 4개의 ArrayList에 따로 저장된 선과 도형들을 모두 그려줌
            foreach (DrawData outData in saveData)
            {
                outData.drawData(e.Graphics);
            }

            foreach (DrawData outData in saveLine)
            {
                outData.drawData(e.Graphics);
            }

            foreach (DrawData outData in saveCircle)
            {
                outData.drawData(e.Graphics);
            }

            foreach (DrawData outData in saveRectangle)
            {
                outData.drawData(e.Graphics);
            }
        }
    }
}