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
        protected List<Gate> compGatesList = new List<Gate>();
        /// <summary>
        /// A list of wires that connect gates inside this compound.
        /// These are wires whose FromPin.Owner and ToPin.Owner are both
        /// inside CompGatesList.
        /// </summary>
        protected List<Wire> compWiresList = new List<Wire>();

        /// <summary>
        /// Gets and sets the left coordinate of the compound gate.
        /// </summary>
        public override int Left
        {
            get { return left; }
            set { left = value; }
        }

        /// <summary>
        /// Gets and sets the top coordinate of the compound gate.
        /// </summary>
        public override int Top
        {
            get { return top; }
            set { top = value; }
        }

        /// <summary>
        /// Gets and sets the selected property for clicking on all of the gates in the compound list
        /// </summary>
        public override bool Selected
        {
            get { return selected; }
            set
            {
                if (selected != value)
                {
                    selected = value;
                    foreach (Gate g in CompGatesList)
                    {
                        g.Selected = value;
                    }
                }
            }
        }

        /// <summary>
        /// Gets and sets the list of gates that make up the compound gate.
        /// </summary>
        public List<Gate> CompGatesList
        {
            get { return compGatesList; }
            set { compGatesList = value; }
        }

        /// <summary>
        /// Gets/sets the list of wires that belong to this compound gate.
        /// </summary>
        public List<Wire> CompWiresList
        {
            get { return compWiresList; }
            set { compWiresList = value; }
        }

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
            // Add the gate to the list of gates that make up the compound gate
            CompGatesList.Add(g);

            // Update the position of the compound gate to encompass the new gate
            if (g.Left < Left)
            {
                Left = g.Left;
            }
            
            if(g.Top < Top)
            {
                Top = g.Top;
            }

        }

        /// <summary>
        /// Draws the compound gate by drawing each of its constituent gates.
        /// </summary>
        /// <param name="paper"></param>
        public override void Draw(Graphics paper)
        {
            // Draw the gates that make up the compound gate
            foreach (Gate g in CompGatesList)
            {
                g.Draw(paper);
            }

            // Draw wires that belong to this compound (internal connections)
            foreach (Wire w in CompWiresList)
            {
                w.Draw(paper);
            }
        }

        /// <summary>
        /// Override mouse hit testing so clicks on any child gate select the compound gate
        /// </summary>
        public override bool IsMouseOn(int x, int y)
        {
            // First check if the point is inside any child gate
            foreach (Gate g in CompGatesList)
            {
                if (g.Left <= x && x < g.Left + WIDTH && g.Top <= y && y < g.Top + HEIGHT)
                {
                    if (Selected)
                        Selected = false;
                    else
                        Selected = true;
                    return true;
                }
            }

            // Default back to the base implementation if no child gate was hit
            return base.IsMouseOn(x, y);
        }

        /// <summary>
        /// Creates a new instance of the compound gate with the same properties and constituent gates as the original.
        /// </summary>
        /// <returns></returns>
        public override Gate Clone()
        {
            // Create new compound gate at the 
            Compound newCompound = new Compound(Left, Top);

            // Mapping from original gate/pin to cloned gate/pin
            Dictionary<Gate, Gate> gateMap = new Dictionary<Gate, Gate>();
            Dictionary<Pin, Pin> pinMap = new Dictionary<Pin, Pin>();

            // First clone gates and build pin mapping (assumes Clone creates pins in same order)
            foreach (Gate g in CompGatesList)
            {
                Gate cloned = g.Clone();
                newCompound.AddGate(cloned);

                //##
                gateMap[g] = cloned;
                // map pins by index
                for (int i = 0; i < g.Pins.Count && i < cloned.Pins.Count; i++)
                {
                    pinMap[g.Pins[i]] = cloned.Pins[i];
                }
            }

            // Now clone internal wires (wires between gates inside this compound)
            foreach (Wire w in CompWiresList)
            {
                Pin origFrom = w.FromPin;
                Pin origTo = w.ToPin;
                if (pinMap.ContainsKey(origFrom) && pinMap.ContainsKey(origTo))
                {
                    Pin clonedFrom = pinMap[origFrom];
                    Pin clonedTo = pinMap[origTo];
                    Wire clonedWire = new Wire(clonedFrom, clonedTo);
                    // set the input pin's InputWire so evaluation and drawing work
                    clonedTo.InputWire = clonedWire;
                    newCompound.CompWiresList.Add(clonedWire);
                }
            }
            //##
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
            // Update the position of the compound gate
            base.MoveTo(x, y);
            // Move each gate in the compound gate by the offset
            foreach (Gate g in CompGatesList)
            {
                g.MoveTo(g.Left + offsetX, g.Top + offsetY);
            }

        }

        /// <summary>
        /// Evaluates the output of the compound gate based on the outputs of its constituent gates. If any of the constituent gates evaluate to false, the compound gate evaluates to false. Otherwise, it evaluates to true.
        /// </summary>
        /// <returns></returns>
        public override bool Evaluate()
        {
            // Evaluate the output of the compound gate based on the outputs of its constituent gates
            foreach (Gate g in CompGatesList)
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
