using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Circuits
{
    public class PadGate : Gate
    {
        public PadGate(int x, int y) : base(x, y)
        {
            //Add the input pin to the gate
            pins.Add(new Pin(this, true, 20));
            //Add the output pin to the gate
            pins.Add(new Pin(this, false, 20));
            //move the gate and the pins to the position passed in
            MoveTo(x, y);
        }

        public override void Draw(Graphics paper)
        {
            //Draw the pins for the gate
            foreach (Pin p in pins)
            {
                p.Draw(paper);
            }

            //Now draw the main part of the gate after setting which image to use based on whether the gate is selected or not

            if (selected)
            {
                //Draw small rectangle for the PadGate instead of image

                paper.FillRectangle(Brushes.Red, Left, Top, WIDTH, HEIGHT);
            }
            else
            {
                paper.FillRectangle(Brushes.Black, Left, Top, WIDTH, HEIGHT);
            }
        }

        /// <summary>
        /// Moves the gate to the position specified.
        /// </summary>
        /// <param name="x">The x position to move the gate to</param>
        /// <param name="y">The y position to move the gate to</param>
        public override void MoveTo(int x, int y)
        {
            base.MoveTo(x, y);
            // must move the pins too
            pins[0].X = x - GAP;
            pins[0].Y = y + HEIGHT / 2;
            pins[1].X = x + WIDTH + GAP;
            pins[1].Y = y + HEIGHT / 2;
        }
    }
}
