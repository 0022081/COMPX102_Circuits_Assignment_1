using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Circuits
{
    /// <summary>
    /// The main GUI for the COMPX102 digital circuits editor.
    /// This has a toolbar, containing buttons called buttonAnd, buttonOr, etc.
    /// The contents of the circuit are drawn directly onto the form.
    /// </summary>
    /// 

    //1.	Is it a better idea to fully document the Gate class or the AndGate subclass? Can you inherit comments? 
    //      - It is better to fully document the Gate class as this is the super class and thus any of the methods, properties and instance variables used
    //        in the AndGate subclass will automatically inherit the documentation. If the AndGate subclass was fully documented instead then the Gate super class
    //        would not be documented and any of the other subclasses of the Gate class would not either. 
    //      - Yes you can inherit comments in the way that documentation of summary's will be inherited in the variables, methods and properties from a super class
    //        to a subclass
 
    //2.	What is the advantage of making a method abstract in the superclass rather than just writing a virtual method with no code in the body of the method? Is there any disadvantage to an abstract method? 
    //      - Making a method in the superclass abstract over virtual enforces that each subclass has to have a method that is different from the superclass. If the class is 
    //        is only virtual then the subclass does not have to inherit the method at all and can also just inherit the exact superclass method. 
    //      - Having an abstract method in a superclass does make it harder for usage of inherited class in other less related areas of code and subclasses. It also means that adding or chaning any abstract methods
    //        in the superclass after the creation of subclasses breaks the compilation of all the subclasses as these are strictly tied to the superclass. 
 
    //3.	If a class has an abstract method in it, does the class have to be abstract? 
    //      - Yes if the class has an abstract method in it the class has to be abstract. 
 
    //4.	What would happen in your program if one of the gates added to your Compound Gate is another Compound Gate? Is your design robust enough to cope with this situation?
    //      - My program uses a recursive seaching method for pins and selection for compound gates, thus if compound gates are inside other compound gates it will seach for a default gate class like AND, NOT, OR
    //        then it will perform its selected or if mouse is on pin methods.

    public partial class Form1 : Form
    {
        /// <summary>
        /// The (x,y) mouse position of the last MouseDown event.
        /// </summary>
        protected int startX, startY;

        /// <summary>
        /// If this is non-null, we are inserting a wire by
        /// dragging the mouse from startPin to some output Pin.
        /// </summary>
        protected Pin startPin = null;

        /// <summary>
        /// The (x,y) position of the current gate, just before we started dragging it.
        /// </summary>
        protected int currentX, currentY;

        /// <summary>
        /// The set of gates in the circuit
        /// </summary>
        protected List<Gate> gatesList = new List<Gate>();

        /// <summary>
        /// Holds any selected gates being added to new compound that need to removed from gatesList
        /// </summary>
        protected List<Gate> removeGatesList = new List<Gate>();

        /// <summary>
        /// The set of connector wires in the circuit
        /// </summary>
        protected List<Wire> wiresList = new List<Wire>();

        /// <summary>
        /// List of the wires inside new compound gate to move
        /// </summary>
        protected List<Wire> wiresToMove = new List<Wire>();

        /// <summary>
        /// The currently selected gate, or null if no gate is selected.
        /// </summary>
        protected Gate current = null;

        /// <summary>
        /// The new gate that is about to be inserted into the circuit
        /// </summary>
        protected Gate newGate = null;

        /// <summary>
        /// The new compound gate that is about to be inserted into the circuit
        /// </summary>
        protected Compound newCompound = null;

        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;
        }

        /// <summary>
        /// Finds the pin that is close to (x,y), or returns
        /// null if there are no pins close to the position.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>The pin that has been selected</returns>
        public Pin findPin(int x, int y)
        {
            // Search gates and their child gates recursively for a pin at (x,y)
            foreach (Gate g in gatesList)
            {
                Pin found = findPinInGate(g, x, y);
                if (found != null)
                    return found;
            }
            return null;
        }

        /// <summary>
        /// Recursively search the provided gate (and any child gates if it's a Compound)
        /// for a pin close to (x,y).
        /// </summary>
        private Pin findPinInGate(Gate g, int x, int y)
        {
            // Check pins directly on this gate first
            foreach (Pin p in g.Pins)
            {
                if (p.isMouseOn(x, y))
                    return p;
            }

            // If this gate is a compound, recurse into its children
            Compound comp = g as Compound;
            if (comp != null)
            {
                foreach (Gate child in comp.CompGatesList)
                {
                    Pin found = findPinInGate(child, x, y);
                    if (found != null)
                        return found;
                }
            }

            return null;
        }

        /// <summary>
        /// Handles all events when the mouse is moving.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (startPin != null)
            {
                //Console.WriteLine("wire from " + startPin + " to " + e.X + "," + e.Y);
                currentX = e.X;
                currentY = e.Y;
                this.Invalidate();  // this will draw the line
            }
            else if (startX >= 0 && startY >= 0 && current != null)
            {
                //Console.WriteLine("mouse move to " + e.X + "," + e.Y);
                current.MoveTo(currentX + (e.X - startX), currentY + (e.Y - startY));
                this.Invalidate();
            }
            else if (newGate != null)
            {
                currentX = e.X;
                currentY = e.Y;
                this.Invalidate();
            }
        }

        /// <summary>
        /// Handles all events when the mouse button is released.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (startPin != null)
            {
                // see if we can insert a wire
                Pin endPin = findPin(e.X, e.Y);
                if (endPin != null)
                {
                    //Console.WriteLine("Trying to connect " + startPin + " to " + endPin);
                    Pin input, output;
                    if (startPin.IsOutput)
                    {
                        input = endPin;
                        output = startPin;
                    }
                    else
                    {
                        input = startPin;
                        output = endPin;
                    }
                    if (input.IsInput && output.IsOutput)
                    {
                        if (input.InputWire == null)
                        {
                            Wire newWire = new Wire(output, input);
                            input.InputWire = newWire;
                            wiresList.Add(newWire);
                        }
                        else
                        {
                            MessageBox.Show("That input is already used.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Error: you must connect an output pin to an input pin.");
                    }
                }
                startPin = null;
                this.Invalidate();
            }
            // We have finished moving/dragging
            startX = -1;
            startY = -1;
            currentX = 0;
            currentY = 0;
        }

        /// <summary>
        /// This will create a new And gate.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonAnd_Click(object sender, EventArgs e)
        {
            newGate = new AndGate(0, 0);
        }

        /// <summary>
        /// This will create a new Or gate.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonNot_Click(object sender, EventArgs e)
        {
            newGate = new NotGate(0, 0);
        }

        /// <summary>
        /// This will create a new Or gate.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonOr_Click(object sender, EventArgs e)
        {
            newGate = new OrGate(0, 0);
        }

        /// <summary>
        /// This will creat a new Pad gate.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonPad_Click(object sender, EventArgs e)
        {
            newGate = new PadGate(0, 0);
        }

        /// <summary>
        /// This will create a new Input source.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonInput_Click(object sender, EventArgs e)
        {
            newGate = new InputSource(0, 0);
        }

        /// <summary>
        /// This will create a new Output lamp.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonOutput_Click(object sender, EventArgs e)
        {
            newGate = new OutputLamp(0, 0);
        }

        /// <summary>
        /// This will evaluate all of the output lamps in the circuit.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonEvaluate_Click(object sender, EventArgs e)
        {
            foreach (Gate g in gatesList)
            {
                if (g is OutputLamp)
                {
                    OutputLamp lamp = (OutputLamp)g;
                    lamp.Evaluate();
                    //Console.WriteLine("Output lamp at " + lamp.Left + "," + lamp.Top + " is " + (lamp.Evaluate() ? "ON" : "OFF"));
                    this.Invalidate();   // Redraw the form
                }
            }
            
        }

        /// <summary>
        /// This will clone all of the selected gates in the circuit.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonClone_Click(object sender, EventArgs e)
        {
            // If gate selected create new clone
            if (current != null)
            {
                if (current.Selected)
                {
                    Gate newGate = current.Clone();   // Clone the selected gate
                    newGate.MoveTo(current.Left + 10, current.Top + 10);    // Move the new gate slightly to the right and down
                    gatesList.Add(newGate);     // Add the new gate to the list of gates
                }
            }
            this.Invalidate();   // Redraw the form

        }

        /// <summary>
        /// This will create a new Compound gate.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonCompound_Click(object sender, EventArgs e)
        {
            newCompound = new Compound(this.Width, this.Height); // Create a new compound gate
        }

        /// <summary>
        /// This will add the selected gate to the compound gate.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonEndCompount_Click(object sender, EventArgs e)
        {
            newGate = newCompound; // Add the selected compound gate to its own gate
            foreach(Gate g in removeGatesList)
            {
                gatesList.Remove(g);
            }
            newCompound = null;    // Clear the new compound gate variable
        }

        /// <summary>
        /// Redraws all the graphics for the current circuit.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Paint(object sender, PaintEventArgs e)
        {

            Color backColor = ColorTranslator.FromHtml("#696969");
            // Clear the background
            e.Graphics.Clear(backColor);

            //Draw all of the gates
            foreach (Gate g in gatesList)
            {
                g.Draw(e.Graphics);
            }
            //Draw all of the wires
            foreach (Wire w in wiresList)
            {
                w.Draw(e.Graphics);
            }

            if (startPin != null)
            {
                e.Graphics.DrawLine(Pens.White,
                    startPin.X, startPin.Y,
                    currentX, currentY);
            }
            if (newGate != null)
            {
                // show the gate that we are dragging into the circuit
                newGate.MoveTo(currentX, currentY);
                newGate.Draw(e.Graphics);
            }
        }

        /// <summary>
        /// Handles events while the mouse button is pressed down.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (current == null)
            {
                // try to start adding a wire
                startPin = findPin(e.X, e.Y);
            }
            else if (current.IsMouseOn(e.X, e.Y))
            {
                // start dragging the current object around
                startX = e.X;
                startY = e.Y;
                currentX = current.Left;
                currentY = current.Top;
            }
        }

        /// <summary>
        /// Handles all events when a mouse is clicked in the form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            //Check if a gate is currently selected
            if (current != null)
            {
                //Unselect the selected gate
                current.Selected = false;
                current = null;
                this.Invalidate();
            }
            // See if we are inserting a new gate
            if (newGate != null)
            {
                newGate.MoveTo(e.X, e.Y);
                gatesList.Add(newGate);
                newGate = null;
                this.Invalidate();
            }
            else
            {
                //Console.WriteLine(gatesList.Count);
                // search for the first gate under the mouse position
                foreach (Gate g in gatesList)
                {
                    if (g.IsMouseOn(e.X, e.Y))
                    {
                        g.Selected = true;
                        current = g;

                        // If a compound gate is being created, add the selected gate to the compound gate
                        if (newCompound != null)
                        {
                            //Add the selected gate to the compound gate
                            newCompound.AddGate(current);
                            // Move any wires that now lie entirely inside the compound
                            wiresToMove = wiresList.Where(w => newCompound.CompGatesList.Contains(w.FromPin.Owner) && newCompound.CompGatesList.Contains(w.ToPin.Owner)).ToList();
                            foreach (Wire w in wiresToMove)
                            {
                                wiresList.Remove(w);
                                newCompound.CompWiresList.Add(w);
                            }
                            //Remove the selected gate from the gates list
                            removeGatesList.Add(current);
                            current = null;
                        }

                        // Redraw the form
                        this.Invalidate();
                        break;
                    }
                }
            }
        }
    }
}
