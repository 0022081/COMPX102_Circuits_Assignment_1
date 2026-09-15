using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Circuits
{
    public class InputSource : Gate
    {
        protected bool outputStatus = false;

        protected const int WIDTH = 20;
        protected const int HEIGHT = 20;

        /// <summary>
        /// Initialises the object to the specified coordinates and adds an output pin.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public InputSource(int x, int y) : base(x, y)
        {
            // Add an output pin to the input source
            pins.Add(new Pin(this, false, 20));
            MoveTo(x, y);
        }

        /// <summary>
        /// Draws the input source on the given graphics paper.
        /// </summary>
        /// <param name="paper"></param>
        public override void Draw(Graphics paper)
        {
            // Draw the pin for the input source
            foreach (Pin p in pins)
            {
                p.Draw(paper);
            }

            // Toggle the output status when the input source is clicked
            if (selected)
            {
                if (outputStatus)
                {
                    outputStatus = false;
                }
                else if (outputStatus == false)
                {
                    outputStatus = true;
                }
            }

            // Draw the main part of the input source
            paper.DrawImage(Properties.Resources.InputIcon, Left, Top);
            // Draw the output status of the input source
            if (outputStatus)
            {
                paper.FillRectangle(Brushes.GreenYellow, Left, Top, WIDTH, HEIGHT);
            }
            else
            {
                paper.FillRectangle(Brushes.Black, Left, Top, WIDTH, HEIGHT);
                //Draw black filled rectangle

            }
        }

        /// <summary>
        /// Clones the input source by creating a new instance at the same coordinates.
        /// </summary>
        public override Gate Clone()
        {
            InputSource newInputSource = new InputSource(Left, Top);
            return newInputSource;
        }

        /// <summary>
        /// Evaluates the input source and returns its output status.
        /// </summary>
        /// <returns></returns>
        public override bool Evaluate()
        {
            Console.WriteLine("Input Source Evaluated: " + outputStatus);
            return (outputStatus);
            
        }

        /// <summary>
        /// Moves the input source and its pin to the specified coordinates.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public override void MoveTo(int x, int y)
        {
            base.MoveTo(x, y);
            // Move the pin too
            pins[0].X = x + WIDTH + GAP;
            pins[0].Y = y + HEIGHT / 2;
        }
    }
}
