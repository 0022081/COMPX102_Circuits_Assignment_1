using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Circuits
{
    public class OrGate : Gate
    {
        // Sets the gate offset for pins
        const int GAP_OFFSET = 12;

        /// <summary>
        /// Initialises the object to the specified coordinates and adds two input pins and one output pin.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public OrGate(int x, int y) : base(x, y)
        {
            //Add the two input pins to the gate
            pins.Add(new Pin(this, true, 20));
            pins.Add(new Pin(this, true, 20));
            //Add the output pin to the gate
            pins.Add(new Pin(this, false, 20));
            //move the gate and the pins to the position passed in
            MoveTo(x, y);
        }

        /// <summary>
        /// Draws the gate and its pins to the graphics object passed in.
        /// </summary>
        /// <param name="paper"></param>
        public override void Draw(Graphics paper)
        {
            //Draw the pins for the gate
            foreach (Pin p in pins)
            {
                p.Draw(paper);
            }

            // Draw the or gate based on whether selected or not
            if (Selected)
            {
                paper.DrawImage(Properties.Resources.OrGateRed, Left, Top);
            }
            else
            {
                paper.DrawImage(Properties.Resources.OrGate, Left, Top);
            }
        }

        /// <summary>
        /// Clones the input source by creating a new instance at the same coordinates.
        /// </summary>
        public override Gate Clone()
        {
            OrGate newOrGate = new OrGate(Left, Top);   // create new clone on current gate
            return newOrGate;
        }

        /// <summary>
        /// Evaluates the output of the gate based on the inputs from the connected pins.
        /// </summary>
        /// <returns></returns>
        public override bool Evaluate()
        {
            try
            {
                if (pins[0].InputWire == null || pins[1].InputWire == null) // Throw exception if either input pin is not connected to a wire
                {
                    throw new Exception("Input pin not connected to a wire.");
                }
                else
                {
                    // Get the gates connected to the input pins
                    Gate inputGate1 = pins[0].InputWire.FromPin.Owner;
                    Gate inputGate2 = pins[1].InputWire.FromPin.Owner;
                    // Evaluate the inputs and return the OR result
                    return inputGate1.Evaluate() || inputGate2.Evaluate();
                }

            }
            catch (Exception ex)    // Catch any exceptions that occur during evaluation
            {
                Console.WriteLine($"Error evaluating OrGate: {ex.Message}");
                return false; // Return false if there's an error
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
            pins[0].Y = y + GAP;
            pins[1].X = x - GAP;
            pins[1].Y = y + HEIGHT - GAP;
            pins[2].X = x + WIDTH + GAP + GAP_OFFSET;
            pins[2].Y = y + HEIGHT / 2;
        }
    }
}
