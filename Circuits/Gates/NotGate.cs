using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Circuits
{
    public class NotGate : Gate
    {
        /// <summary>
        /// Initialises the object to the specified coordinates and adds two input pins and one output pin.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public NotGate(int x, int y) : base(x, y)
        {
            //Add the input pin to the gate
            pins.Add(new Pin(this, true, 20));
            //Add the output pin to the gate
            pins.Add(new Pin(this, false, 20));
            //move the gate and the pins to the position passed in
            MoveTo(x, y);
        }

        /// <summary>
        /// Draws the NOT gate and its pins on the provided Graphics object.
        /// </summary>
        /// <param name="paper"></param>
        public override void Draw(Graphics paper)
        {
            //Draw the pins for the gate
            foreach (Pin p in pins)
            {
                p.Draw(paper);
            }

            //Now draw the main part of the gate after setting which image to use based on whether the gate is selected or not

            if (Selected)
            {
                paper.DrawImage(Properties.Resources.NotGateRed, Left, Top);
            }
            else
            {
                paper.DrawImage(Properties.Resources.NotGate, Left, Top);
            }
        }

        /// <summary>
        /// Clones the input source by creating a new instance at the same coordinates.
        /// </summary>
        /// <returns></returns>
        public override Gate Clone()
        {
            NotGate newNotGate = new NotGate(Left, Top);
            return newNotGate;
        }

        /// <summary>
        /// Evaluates the output of the NOT gate based on the input pin's value.
        /// </summary>
        /// <returns></returns>
        public override bool Evaluate()
        {
            try
            {
                // Check if the input pin is connected to a wire
                if (pins[0].InputWire == null)
                {
                    throw new Exception("Input pin is not connected to a wire.");
                }
                else // Pins connected so return the NOT of the input gate's evaluation
                {
                    Gate inputGate = pins[0].InputWire.FromPin.Owner;
                    return !inputGate.Evaluate();
                }

            }
            catch (Exception ex)    // Catch any exceptions that occur during evaluation and print an error message
            {
                Console.WriteLine($"Error evaluating NOT gate: {ex.Message}");
                return false;
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
