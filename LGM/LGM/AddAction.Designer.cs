namespace LGM
{
    partial class AddAction
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
            this.btnOK = new System.Windows.Forms.Button();
            this.argumentboxes = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.appliestogbox = new System.Windows.Forms.GroupBox();
            this.appliestobox = new System.Windows.Forms.ComboBox();
            this.panel1.SuspendLayout();
            this.appliestogbox.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(12, 8);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(96, 33);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // argumentboxes
            // 
            this.argumentboxes.Location = new System.Drawing.Point(12, 102);
            this.argumentboxes.Name = "argumentboxes";
            this.argumentboxes.Size = new System.Drawing.Size(385, 292);
            this.argumentboxes.TabIndex = 1;
            this.argumentboxes.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 400);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(409, 48);
            this.panel1.TabIndex = 2;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(301, 8);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(96, 33);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // appliestogbox
            // 
            this.appliestogbox.Controls.Add(this.appliestobox);
            this.appliestogbox.Location = new System.Drawing.Point(109, 12);
            this.appliestogbox.Name = "appliestogbox";
            this.appliestogbox.Size = new System.Drawing.Size(288, 92);
            this.appliestogbox.TabIndex = 3;
            this.appliestogbox.TabStop = false;
            this.appliestogbox.Text = "Applies To";
            // 
            // appliestobox
            // 
            this.appliestobox.FormattingEnabled = true;
            this.appliestobox.Location = new System.Drawing.Point(20, 35);
            this.appliestobox.Name = "appliestobox";
            this.appliestobox.Size = new System.Drawing.Size(179, 28);
            this.appliestobox.TabIndex = 4;
            // 
            // AddAction
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(409, 448);
            this.Controls.Add(this.appliestogbox);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.argumentboxes);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddAction";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add Action";
            this.Load += new System.EventHandler(this.AddAction_Load);
            this.panel1.ResumeLayout(false);
            this.appliestogbox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.GroupBox argumentboxes;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox appliestogbox;
        private System.Windows.Forms.ComboBox appliestobox;
    }
}