using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace ConsoleApp.Experiment.Auxiliary
{
    public class ResearchGraph
    {
        private const int X = 8600;
        public int GetX { get => X; }
        private const int Y = 5000;
        private Pen ffPen = new Pen(Brushes.Red);
        private Pen dPen = new Pen(Brushes.Blue);
        private Pen scalePen = new Pen(Brushes.Gray);
        private Font font = new Font("Arial", 90);
        private Font scaleFont = new Font("Arial", 60);
        private const int stableY = Y - 400;
        private const int stableX = 300;
        public int GetStableX { get => stableX; }
        private Bitmap bmp = new Bitmap(X, Y);
        private Graphics g;
        public ResearchGraph()
        {
            ffPen.Width = 6.0F;
            dPen.Width = 6.0F;
            scalePen.Width = 3.0F;
            g = Graphics.FromImage(bmp);
            g.Clear(Color.White);
        }
        public void DrawGraphLine(bool algorithm, int startX, int startY, int size, double msec)
        {
            if(algorithm)
                g.DrawLine(ffPen, startX, stableY - startY, startX + ((X - 420) / size), stableY - Convert.ToInt32(msec * 100));
            else
                g.DrawLine(dPen, startX, stableY - startY, startX + ((X - 420) / size), stableY - Convert.ToInt32(msec * 100));
        }
        
        public void DrawXNumbers(int i, int startX)
        {
            g.DrawString((i).ToString(), scaleFont, Brushes.Gray, startX, stableY);
        }
        public void DrawYNumbers(List<double> FordFulkersonCount, List<double> DinicsCount)
        {
            var maxFF = FordFulkersonCount.Average();
            var maxD = DinicsCount.Average();
            var maxHeight = maxFF >= maxD ? maxFF : maxD;

            var partHeight = Convert.ToInt32((maxHeight * 3) / 5) * 100;
            var tmpY = stableY;
            for (var i = 1; i <= 5; i++) //numbers on Y 
            {
                g.DrawString(((i * partHeight) / 100).ToString(), scaleFont, Brushes.Gray, 150, tmpY - partHeight);
                tmpY -= partHeight;
            }
        }
        public void DrawYNumbers(List<Average> time)
        {
            var maxFF = 0.0;
            foreach (var x in time)
            {
                if (x.Time[0] > maxFF)
                    maxFF = x.Time[0];
            }
            var maxD = 0.0;
            foreach (var x in time)
            {
                if (x.Time[1] > maxD)
                    maxD = x.Time[1];
            }
            var maxHeight = maxFF >= maxD ? maxFF : maxD;
            var partHeight = Convert.ToInt32(maxHeight / 5) * 10;
            var tmpY = stableY;
            for (var i = 1; i <= 5; i++) //numbers on Y 
            {
                g.DrawString(((i * partHeight) / 10).ToString(), scaleFont, Brushes.Gray, 150, tmpY - partHeight);
                tmpY -= partHeight;
            }
        }
        public void DrawAxes(string x, string y)
        {
            g.DrawLine(scalePen, stableX, stableY, X - 120, stableY);
            g.DrawLine(scalePen, stableX, stableY, stableX, 200);

            var format = new StringFormat
            {
                FormatFlags = StringFormatFlags.DirectionVertical
            };
            g.DrawString(x, scaleFont, Brushes.Gray, X - 550, stableY + 90);
            g.DrawString(y, scaleFont, Brushes.Gray, 150, 300, format);
        }
        public void DrawLegend(int size, IOConsole console)
        {
            g.DrawRectangle(ffPen, 50, Y - 160, 90, 90);
            g.DrawString($"Ford-Fulkerson ", font, Brushes.Black, 150, Y - 180);
            g.DrawRectangle(dPen, 1150, Y - 160, 90, 90);
            g.DrawString($"Dinics\t Matrix size = {console.N}\t Research size = {size}",
                font, Brushes.Black, 1250, Y - 180);
        }
        public void DrawLegend(int startsize, int finishsize, int step, int researchsize)
        {
            g.DrawRectangle(ffPen, 50, Y - 160, 90, 90);
            g.DrawString($"Ford-Fulkerson ", font, Brushes.Black, 150, Y - 180);
            g.DrawRectangle(dPen, 1150, Y - 160, 90, 90);
            g.DrawString($"Dinics\t Start size = {startsize}\t Final size = {finishsize}" +
                $"\t Step = {step}\t Researches = {researchsize}",
                font, Brushes.Black, 1250, Y - 180);
        }
        public void SaveJPG(int num)
        {
            bmp.Save($"Research_{num}_{DateTime.Now.Day}{DateTime.Now.Month}{DateTime.Now.Year}_{DateTime.Now.Hour}{DateTime.Now.Minute}{DateTime.Now.Second}.jpg");
        }
    }
}
