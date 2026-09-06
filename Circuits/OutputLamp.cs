using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Circuits
{
    public class OutputLamp : Gate
    {
        protected bool inputStatus = false;

        protected const int WIDTH = 20;
        protected const int HEIGHT = 20;

        public OutputLamp(int x, int y) : base(x, y)
        {
            // Add an input pin to the output lamp
            pins.Add(new Pin(this, true, 20));
            MoveTo(x, y);
        }

        public override void Draw(Graphics paper)
        {
            // Draw the pin for the input source
            foreach (Pin p in pins)
            {
                p.Draw(paper);
            }

            // Draw the main part of the input source
            paper.DrawImage(Properties.Resources.OutputIcon, Left, Top);
            // Draw the output status of the input source
            if (inputStatus)
            {
                paper.FillRectangle(Brushes.Red, Left, Top, WIDTH, HEIGHT);
            }
            else
            {
                paper.FillRectangle(Brushes.Black, Left, Top, WIDTH, HEIGHT);
            }
        }

        public override void MoveTo(int x, int y)
        {
            base.MoveTo(x, y);
            // Move the pin too
            pins[0].X = x - WIDTH;
            pins[0].Y = y + HEIGHT / 2;
        }
    }
}
