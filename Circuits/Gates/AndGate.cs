using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Circuits
{
    /// <summary>
    /// This class implements an AND gate with two inputs
    /// and one output.
    /// </summary>
    public class AndGate : Gate
    {
        /// <summary>
        /// Initialises the Gate.
        /// </summary>
        /// <param name="x">The x position of the gate</param>
        /// <param name="y">The y position of the gate</param>
        public AndGate(int x, int y) : base(x, y)
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
        /// Draws the gate in the normal colour or in the selected colour.
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

            if (selected)
            {
                paper.DrawImage(Properties.Resources.AndGateRed, Left, Top);
            }
            else
            {
                paper.DrawImage(Properties.Resources.AndGate, Left, Top);
            }
            //Note: You can also use the images that have been imported into the project if you wish,
            //      using the code below.  You will need to space the pins out a bit more in the constructor.
            //      There are provided images for the other gates and selected versions of the gates as well.
            //paper.DrawImage(Properties.Resources.AndGate, Left, Top);


        }

        /// <summary>
        /// Clones the gate to create a new instance of the gate at the same position.
        /// </summary>
        public override Gate Clone()
        {
            AndGate newGate = new AndGate(Left, Top);
            return newGate;
        }

        /// <summary>
        /// Evaluates the AND gate based on the values of the input pins.
        /// </summary>
        /// <returns></returns>
        public override bool Evaluate()
        {
            try
            {
                // Check if the input pins are connected to wires
                if (pins[0].InputWire == null || pins[1].InputWire == null)
                {
                    throw new Exception("Input pins are not connected to any wires.");
                }
                // Evaluate the AND gate based on the values of the input pins
                else
                {
                    Gate gateA = pins[0].InputWire.FromPin.Owner;
                    Gate gateB = pins[1].InputWire.FromPin.Owner;
                    return gateA.Evaluate() && gateB.Evaluate();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error evaluating AND gate: {ex.Message}");
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
            pins[0].Y = y + GAP;
            pins[1].X = x - GAP;
            pins[1].Y = y + HEIGHT - GAP;
            pins[2].X = x + WIDTH + GAP;
            pins[2].Y = y + HEIGHT / 2;
        }
    }
}
