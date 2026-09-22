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

        /// <summary>
        /// Initialises the object to the specified coordinates and adds an input pin.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public OutputLamp(int x, int y) : base(x, y)
        {
            // Add an input pin to the output lamp
            pins.Add(new Pin(this, true, 20));
            MoveTo(x, y);
        }

        /// <summary>
        /// Draws the output lamp on the given graphics context.
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
            if (inputStatus)
            {
                paper.DrawImage(Properties.Resources.OutputIcon, Left, Top);
            }
            else
            {
                paper.DrawImage(Properties.Resources.OutputIconOff, Left, Top);
            }

            if (selected)
            {
                paper.DrawImage(Properties.Resources.OutputIconRed, Left, Top);
            }
        }

        /// <summary>
        /// Clones the input source by creating a new instance at the same coordinates.
        /// </summary>
        public override Gate Clone()
        {
            OutputLamp newOutputLamp = new OutputLamp(Left, Top);
            return newOutputLamp;
        }

        /// <summary>
        /// Evaluates the output lamp's status based on the input gate's evaluation.
        /// </summary>
        /// <returns></returns>
        public override bool Evaluate()
        {
            Gate gateA = pins[0].InputWire.FromPin.Owner;
            inputStatus = gateA.Evaluate(); // Set output status based on the input gate's evaluation
            return inputStatus;
        }

        /// <summary>
        /// Moves the output lamp and its pin to the specified coordinates.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public override void MoveTo(int x, int y)
        {
            base.MoveTo(x, y);
            // Move the pin too
            pins[0].X = x - WIDTH;
            pins[0].Y = y + HEIGHT / 2;
        }
    }
}
