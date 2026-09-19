namespace Circuits
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonAnd = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonNot = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonOr = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonPad = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonInput = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonOutput = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonEvaluate = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonClone = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonCompount = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonEndCompount = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonAnd,
            this.toolStripButtonNot,
            this.toolStripButtonOr,
            this.toolStripButtonPad,
            this.toolStripButtonInput,
            this.toolStripButtonOutput,
            this.toolStripButtonEvaluate,
            this.toolStripButtonClone,
            this.toolStripButtonCompount,
            this.toolStripButtonEndCompount});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.toolStrip1.Size = new System.Drawing.Size(1512, 38);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonAnd
            // 
            this.toolStripButtonAnd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonAnd.Image = global::Circuits.Properties.Resources.AndIcon;
            this.toolStripButtonAnd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAnd.Name = "toolStripButtonAnd";
            this.toolStripButtonAnd.Size = new System.Drawing.Size(34, 33);
            this.toolStripButtonAnd.Text = "AND Gate";
            this.toolStripButtonAnd.Click += new System.EventHandler(this.toolStripButtonAnd_Click);
            // 
            // toolStripButtonNot
            // 
            this.toolStripButtonNot.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonNot.Image = global::Circuits.Properties.Resources.NotGate;
            this.toolStripButtonNot.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonNot.Name = "toolStripButtonNot";
            this.toolStripButtonNot.Size = new System.Drawing.Size(34, 33);
            this.toolStripButtonNot.Text = "NOT Gate";
            this.toolStripButtonNot.Click += new System.EventHandler(this.toolStripButtonNot_Click);
            // 
            // toolStripButtonOr
            // 
            this.toolStripButtonOr.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonOr.Image = global::Circuits.Properties.Resources.OrGate;
            this.toolStripButtonOr.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonOr.Name = "toolStripButtonOr";
            this.toolStripButtonOr.Size = new System.Drawing.Size(34, 33);
            this.toolStripButtonOr.Text = "OR Gate";
            this.toolStripButtonOr.Click += new System.EventHandler(this.toolStripButtonOr_Click);
            // 
            // toolStripButtonPad
            // 
            this.toolStripButtonPad.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonPad.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonPad.Image")));
            this.toolStripButtonPad.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPad.Name = "toolStripButtonPad";
            this.toolStripButtonPad.Size = new System.Drawing.Size(34, 33);
            this.toolStripButtonPad.Text = "Pad Gate";
            this.toolStripButtonPad.Click += new System.EventHandler(this.toolStripButtonPad_Click);
            // 
            // toolStripButtonInput
            // 
            this.toolStripButtonInput.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonInput.Image = global::Circuits.Properties.Resources.InputIcon;
            this.toolStripButtonInput.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonInput.Name = "toolStripButtonInput";
            this.toolStripButtonInput.Size = new System.Drawing.Size(34, 33);
            this.toolStripButtonInput.Text = "Input Gate";
            this.toolStripButtonInput.Click += new System.EventHandler(this.toolStripButtonInput_Click);
            // 
            // toolStripButtonOutput
            // 
            this.toolStripButtonOutput.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonOutput.Image = global::Circuits.Properties.Resources.OutputIcon;
            this.toolStripButtonOutput.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonOutput.Name = "toolStripButtonOutput";
            this.toolStripButtonOutput.Size = new System.Drawing.Size(34, 33);
            this.toolStripButtonOutput.Text = "Output Gate";
            this.toolStripButtonOutput.Click += new System.EventHandler(this.toolStripButtonOutput_Click);
            // 
            // toolStripButtonEvaluate
            // 
            this.toolStripButtonEvaluate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonEvaluate.Image = global::Circuits.Properties.Resources.EvaluateIcon;
            this.toolStripButtonEvaluate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonEvaluate.Name = "toolStripButtonEvaluate";
            this.toolStripButtonEvaluate.Size = new System.Drawing.Size(34, 33);
            this.toolStripButtonEvaluate.Text = "Evaluate";
            this.toolStripButtonEvaluate.Click += new System.EventHandler(this.toolStripButtonEvaluate_Click);
            // 
            // toolStripButtonClone
            // 
            this.toolStripButtonClone.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonClone.Image = global::Circuits.Properties.Resources.CopyIcon;
            this.toolStripButtonClone.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonClone.Name = "toolStripButtonClone";
            this.toolStripButtonClone.Size = new System.Drawing.Size(34, 33);
            this.toolStripButtonClone.Text = "Clone";
            this.toolStripButtonClone.Click += new System.EventHandler(this.toolStripButtonClone_Click);
            // 
            // toolStripButtonCompount
            // 
            this.toolStripButtonCompount.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonCompount.Image = global::Circuits.Properties.Resources.StartCompoundIcon;
            this.toolStripButtonCompount.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonCompount.Name = "toolStripButtonCompount";
            this.toolStripButtonCompount.Size = new System.Drawing.Size(34, 33);
            this.toolStripButtonCompount.Text = "Start Group";
            this.toolStripButtonCompount.Click += new System.EventHandler(this.toolStripButtonCompount_Click);
            // 
            // toolStripButtonEndCompount
            // 
            this.toolStripButtonEndCompount.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonEndCompount.Image = global::Circuits.Properties.Resources.EndCompoundIcon;
            this.toolStripButtonEndCompount.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonEndCompount.Name = "toolStripButtonEndCompount";
            this.toolStripButtonEndCompount.Size = new System.Drawing.Size(34, 33);
            this.toolStripButtonEndCompount.Text = "End Group";
            this.toolStripButtonEndCompount.Click += new System.EventHandler(this.toolStripButtonEndCompount_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(1512, 1122);
            this.Controls.Add(this.toolStrip1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Circuits 2023";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButtonAnd;
        private System.Windows.Forms.ToolStripButton toolStripButtonNot;
        private System.Windows.Forms.ToolStripButton toolStripButtonOr;
        private System.Windows.Forms.ToolStripButton toolStripButtonPad;
        private System.Windows.Forms.ToolStripButton toolStripButtonInput;
        private System.Windows.Forms.ToolStripButton toolStripButtonOutput;
        private System.Windows.Forms.ToolStripButton toolStripButtonEvaluate;
        private System.Windows.Forms.ToolStripButton toolStripButtonClone;
        private System.Windows.Forms.ToolStripButton toolStripButtonCompount;
        private System.Windows.Forms.ToolStripButton toolStripButtonEndCompount;
    }
}

