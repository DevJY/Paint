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
		ractangle,
		circle,
	};

	public partial class Form1 : Form
	{
		
		Point startPoint, nowPoint; // 선택한 좌표와 현재 좌표
		Pen myPen;                  // 선
		Brush myBrush;              // 원과 사각형
		drawMode drawmode = drawMode.line;


		public Form1()
		{
			InitializeComponent();                
			myPen = new Pen(Color.Black);
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

		private void square_Click(object sender, EventArgs e)
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
			//Rectangle rect;

			switch (drawmode)
			{
				case drawMode.line:
					g.DrawLine(myPen, startPoint, nowPoint);
					break;

				case drawMode.circle:
					g.DrawLine(myPen, startPoint, nowPoint);
					break;

			}
		}

		private void Panel1_MouseUp(object sender, MouseEventArgs e)
		{

		}






	}
}
