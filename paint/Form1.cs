using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Collections;

namespace paint
{

	public enum drawMode
	{
		line,
		circle,
		ractangle,
	}

	public partial class Form1 : Form
	{
		
		Point startPoint, nowPoint; // 선택한 좌표와 현재 좌표
		Pen myPen;                  // 도형
		drawMode drawmode = drawMode.line;

		ArrayList saveLine, saveCircle, saveRect;   //그린 도형 정보 저장용

		public Form1()
		{
			InitializeComponent();                
			myPen = new Pen(Color.Black);

			saveLine = new ArrayList();
			saveCircle = new ArrayList();
			saveRect = new ArrayList();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			Invalidate();
			this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			this.SetStyle(ControlStyles.UserPaint, true);

			ResizeRedraw = true;
		}

		#region 그리기모드

		private void button_line_Click(object sender, EventArgs e)
		{
			drawmode = drawMode.line;
			Invalidate();
			Update();
		}

		private void button_circle_Click(object sender, EventArgs e)
		{
			drawmode = drawMode.circle;
			Invalidate();
			Update();
		}

		private void ractangle_Click(object sender, EventArgs e)
		{
			drawmode = drawMode.ractangle;
			Invalidate();
			Update();
		}

		#endregion

		private void Panel1_MouseDown(object sender, MouseEventArgs e)
		{
			nowPoint = new Point(e.X, e.Y);
			startPoint = nowPoint;
			mouseClickPosition.Text = "[" + startPoint.X + "," + startPoint.Y + "]";
		}

		private void Panel1_MouseMove(object sender, MouseEventArgs e)
		{
			nowPoint = new Point(e.X, e.Y);
			mousePosition.Text = "[" + nowPoint.X + "," + nowPoint.Y + "]";

			Graphics g = CreateGraphics();  // 그리기 객체 생성
			Invalidate();
			Update();
			Rectangle rect;

			switch (drawmode)
			{
				case drawMode.line:
					g.DrawLine(myPen, startPoint, nowPoint);
					break;

				case drawMode.circle:
					rect = new Rectangle(startPoint.X, startPoint.Y, nowPoint.X - startPoint.X, nowPoint.Y - startPoint.Y);
					g.DrawEllipse(myPen, rect);
					break;

				case drawMode.ractangle:
					rect = new Rectangle(startPoint.X, startPoint.Y, nowPoint.X - startPoint.X, nowPoint.Y - startPoint.Y);
					g.DrawRectangle(myPen,rect);
					break;
			}
			g.Dispose();
		}

		private void Panel1_MouseUp(object sender, MouseEventArgs e)
		{
			//
			DrawData inputData;

			switch (drawmode)
			{
				case drawMode.line:
					inputData = new DrawData(startPoint, nowPoint, myPen, drawmode);
					saveLine.Add(inputData);
					break;

				case drawMode.circle:
					inputData = new DrawData(startPoint, nowPoint, myPen, drawmode);
					saveCircle.Add(inputData);
					break;

				case drawMode.ractangle:
					inputData = new DrawData(startPoint, nowPoint, myPen, drawmode);
					saveRect.Add(inputData);
					break;

			}
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			//base.OnPaint(e);

			Graphics g = e.Graphics;

			foreach (DrawData outData in saveLine)
			{
				outData.drawData(e.Graphics);
			}

			foreach (DrawData outData in saveCircle)
			{
				outData.drawData(e.Graphics);
			}

			foreach (DrawData outData in saveRect)
			{
				outData.drawData(e.Graphics);
			}
		}
	}

	class DrawData
	{
		Point StartPoint, EndPoint;

		Color pen_color;
		drawMode DrawMode;

		public DrawData(Point x, Point y, Pen p, drawMode drawmode)
		{
			StartPoint = x;
			EndPoint = y;
			pen_color = p.Color;
			DrawMode = drawmode;
		}

		public void drawData(Graphics g)
		{
			Rectangle rect;
			Pen p = new Pen(pen_color);

			switch (DrawMode)
			{
				case drawMode.line:
					g.DrawLine(p, StartPoint, EndPoint);
					break;

				case drawMode.circle:
					rect = new Rectangle(StartPoint.X, StartPoint.Y, EndPoint.X - StartPoint.X, EndPoint.Y - StartPoint.Y);
					g.DrawEllipse(p, rect);
					break;

				case drawMode.ractangle:
					rect = new Rectangle(StartPoint.X, StartPoint.Y, EndPoint.X - StartPoint.X, EndPoint.Y - StartPoint.Y);
					g.DrawRectangle(p, rect);
					break;
			}
		}
	}

}
