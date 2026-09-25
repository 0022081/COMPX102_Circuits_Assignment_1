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
        /// <summary>
        /// Set the output status of the input source. True for high, false for low.
        /// </summary>
        protected bool outputStatus = false;

        /// <summary>
        /// The width of the input source.
        /// </summary>
        protected const int WIDTH = 20;

        /// <summary>
        /// The height of the input source.
        /// </summary>
        protected const int HEIGHT = 20;

        /// <summary>
        /// The width of the icon within the input source.
        /// </summary>
        protected const int ICON_WIDTH = 8;

        /// <summary>
        /// The height of the icon within the input source.
        /// </summary>
        protected const int ICON_HEIGHT = 8;

        /// <summary>
        /// The offset for the icon within the input source.
        /// </summary>
        protected const int ICON_OFFSET = 6;

        /// <summary>
        /// The gap between the input source and its output pin.
        /// </summary>
        protected const int GAP = 12;

        /// <summary>
        /// Sets the output status of the gate
        /// </summary>
        public bool OutputStatus
        {
            get { return outputStatus; }
            set
            {
                if (outputStatus)
                {
                    outputStatus = false;

                }
                else
                {
                    outputStatus = true;
                }
            }
        }

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

            // Draw the output status of the input source
            if (outputStatus)
            {
                // Draw the main part of the input source
                paper.DrawImage(Properties.Resources.InputIcon, Left, Top);
                // Draw green filled rectangle
                paper.FillRectangle(Brushes.GreenYellow, Left + (ICON_OFFSET / 2), Top + ICON_OFFSET, ICON_WIDTH, ICON_HEIGHT);
            }
            else
            {
                // Draw the main part of the input source
                paper.DrawImage(Properties.Resources.InputIcon, Left, Top);
                // Draw black filled rectangle
                paper.FillRectangle(Brushes.Black, Left + (ICON_OFFSET / 2), Top + ICON_OFFSET, ICON_WIDTH, ICON_HEIGHT);
            }
        }

        /// <summary>
        /// Clones the input source by creating a new instance at the same coordinates.
        /// </summary>
        public override Gate Clone()
        {
            //Console.WriteLine("Cloned input gate");
            InputSource newInputSource = new InputSource(Left, Top);
            return newInputSource;
        }

        /// <summary>
        /// Evaluates the input source and returns its output status.
        /// </summary>
        /// <returns></returns>
        public override bool Evaluate()
        {
            //Console.WriteLine("Input Source Evaluated: " + outputStatus);
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
