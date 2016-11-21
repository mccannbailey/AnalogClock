//By Bailey
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clock
{
    public partial class Form1 : Form
    {
        Brush blackBrush, whiteBrush;
        Font clockFont;
        Pen blackPen, whitePen, redPen;

        int width = 400, height = 400, secHand = 125, minuteHand = 100, hourHand = 50, startUp = 0;
        int[] sechandCoord, hourhandCoord, minutehandCoord;
        int cx, cy;
        int hour, minute, second;
        string hourDisplay, minDisplay, secDisplay;
        bool hasStarted = false;

        public Form1()
        {
            InitializeComponent();
            cx = width / 2;
            cy = height / 2;

            blackBrush = new SolidBrush(Color.Black); whiteBrush = new SolidBrush(Color.White);
            blackPen = new Pen(Color.Black); whitePen = new Pen(Color.White); redPen = new Pen(Color.Red);
            clockFont = new Font("Courier New", 18, FontStyle.Bold);
        }

        private void clockTimer_Tick(object sender, EventArgs e)
        {
            second = DateTime.Now.Second;
            minute = DateTime.Now.Minute;
            hour = DateTime.Now.Hour;


            sechandCoord = secCoord(second, secHand);
            hourhandCoord = hourCoord(hour, hourHand);
            minutehandCoord = secCoord(minute, minuteHand);
            clockTimer.Enabled = true;
            hasStarted = true;

            minDisplay = Convert.ToString(minute); 
            secDisplay = Convert.ToString(second);

            if (hour <= 12)
            {
                hourDisplay = Convert.ToString(hour);
            }
            else
            {
                hourDisplay = Convert.ToString(hour - 12);
            }
            this.Refresh();
        }
        //array for second and min hand
        private int[] secCoord(int val, int secLength)
        {
            int[] coord = new int[2];
            val *= 6; //move 6 degrees
            if (val >= 0 && val <= 180)
            {
                coord[0] = cx + (int)(secLength * Math.Sin(Math.PI * val / 180));
                coord[1] = cy - (int)(secLength * Math.Cos(Math.PI * val / 180));
            }
            else
            {
                coord[0] = cx + (int)(secLength * Math.Sin(Math.PI * val / 180));
                coord[1] = cy - (int)(secLength * Math.Cos(Math.PI * val / 180));
            }
            return coord;
        }

        //array for hour hand
        private int[] hourCoord(int val, int hourLength)
        {
            int[] coord = new int[2];
            val *= 30; //move 30 degrees
            if (val >= 0 && val <= 180)
            {
                coord[0] = cx + (int)(hourLength * Math.Sin(Math.PI * val / 180));
                coord[1] = cy - (int)(hourLength * Math.Cos(Math.PI * val / 180));
            }
            else
            {
                coord[0] = cx + (int)(hourLength * Math.Sin(Math.PI * val / 180));
                coord[1] = cy - (int)(hourLength * Math.Cos(Math.PI * val / 180));
            }
            return coord;
        }

        //drawings
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (hasStarted)
            {
                #region Draw Clock
                e.Graphics.Clear(Color.Gray);
                e.Graphics.FillEllipse(whiteBrush, 40, 50, 300, 300);
                e.Graphics.DrawString("12", clockFont, blackBrush, 175, 65);
                e.Graphics.DrawString("6", clockFont, blackBrush, 175, 315);
                e.Graphics.DrawString("3", clockFont, blackBrush, 300, 190);
                e.Graphics.DrawString("9", clockFont, blackBrush, 65, 190);
                #endregion

                e.Graphics.DrawLine(new Pen(Color.Red, 3f), new Point(190, 200), new Point(sechandCoord[0], sechandCoord[1]));
                e.Graphics.DrawLine(new Pen(Color.Black, 4f), new Point(190, 200), new Point(hourhandCoord[0], hourhandCoord[1]));
                e.Graphics.DrawLine(new Pen(Color.Black, 3f), new Point(190, 200), new Point(minutehandCoord[0], minutehandCoord[1]));
                timeLabel.Text = hourDisplay + ":" + minDisplay + ":" + secDisplay;
            }
        }
    }
}
