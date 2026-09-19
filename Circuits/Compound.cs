using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Circuits
{
    public class Compound : Gate
    {
        /// <summary>
        /// A list of gates that make up the compound gate.
        /// </summary>
        List<Gate> comp_Gates = new List<Gate>();

        /// <summary>
        /// Initialises the object to the specified coordinates and adds an input pin and an output pin.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Compound(int x, int y) : base(x, y)
        {
            // Move the compound gate to the specified coordinates
            MoveTo(x, y);
        }

        /// <summary>
        /// Adds a gate to the compound gate.
        /// </summary>
        /// <param name="g"></param>
        public void AddGate(Gate g)
        {
            comp_Gates.Add(g);
        }

        /// <summary>
        /// Draws the compound gate by drawing each of its constituent gates.
        /// </summary>
        /// <param name="paper"></param>
        public override void Draw(Graphics paper)
        {
            // Draw the gates that make up the compound gate
            foreach (Gate g in comp_Gates)
            {
                g.Draw(paper);
            }
        }

        /// <summary>
        /// Creates a new instance of the compound gate with the same properties and constituent gates as the original.
        /// </summary>
        /// <returns></returns>
        public override Gate Clone()
        {
            // Need to clone wires as well

            Compound newCompound = new Compound(Left, Top);
            foreach (Gate g in comp_Gates)
            {
                newCompound.AddGate(g.Clone());
            }
            return newCompound;
        }

        /// <summary>
        /// Moves the compound gate to the specified coordinates by calculating the offset for each gate in the compound gate and moving them accordingly.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public override void MoveTo(int x, int y)
        {
            // Calculate the offset for each gate in the compound gate
            int offsetX = x - Left;
            int offsetY = y - Top;
            // Move each gate in the compound gate by the offset
            foreach (Gate g in comp_Gates)
            {
                g.MoveTo(g.Left + offsetX, g.Top + offsetY);
            }
            // Update the position of the compound gate
            base.MoveTo(x, y);
        }

        /// <summary>
        /// Evaluates the output of the compound gate based on the outputs of its constituent gates. If any of the constituent gates evaluate to false, the compound gate evaluates to false. Otherwise, it evaluates to true.
        /// </summary>
        /// <returns></returns>
        public override bool Evaluate()
        {
            // Evaluate the output of the compound gate based on the outputs of its constituent gates
            foreach (Gate g in comp_Gates)
            {
                if (!g.Evaluate())
                {
                    return false;
                }
            }
            return true;
        }

    }
}
