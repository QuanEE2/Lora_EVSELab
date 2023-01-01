namespace lora1
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
            this.components = new System.ComponentModel.Container();
            this.Port = new System.Windows.Forms.Label();
            this.cbox = new System.Windows.Forms.ComboBox();
            this.btnOpen = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.txtRecieve = new System.Windows.Forms.TextBox();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.zedGraphControl1 = new ZedGraph.ZedGraphControl();
            this.checkBox = new System.Windows.Forms.CheckedListBox();
            this.typeGraph = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // Port
            // 
            this.Port.AutoSize = true;
            this.Port.Location = new System.Drawing.Point(71, 22);
            this.Port.Name = "Port";
            this.Port.Size = new System.Drawing.Size(31, 16);
            this.Port.TabIndex = 0;
            this.Port.Text = "Port";
            this.Port.Click += new System.EventHandler(this.label1_Click);
            // 
            // cbox
            // 
            this.cbox.FormattingEnabled = true;
            this.cbox.Location = new System.Drawing.Point(121, 19);
            this.cbox.Name = "cbox";
            this.cbox.Size = new System.Drawing.Size(121, 24);
            this.cbox.TabIndex = 1;
            this.cbox.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(258, 19);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(75, 23);
            this.btnOpen.TabIndex = 2;
            this.btnOpen.Text = "Open";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(339, 19);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // txtRecieve
            // 
            this.txtRecieve.Location = new System.Drawing.Point(79, 59);
            this.txtRecieve.Multiline = true;
            this.txtRecieve.Name = "txtRecieve";
            this.txtRecieve.Size = new System.Drawing.Size(537, 24);
            this.txtRecieve.TabIndex = 4;
            this.txtRecieve.TextChanged += new System.EventHandler(this.txtRecieve_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 16);
            this.label1.TabIndex = 9;
            this.label1.Text = "Recieve:";
            this.label1.Click += new System.EventHandler(this.label1_Click_2);
            // 
            // zedGraphControl1
            // 
            this.zedGraphControl1.Location = new System.Drawing.Point(659, 43);
            this.zedGraphControl1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.zedGraphControl1.Name = "zedGraphControl1";
            this.zedGraphControl1.ScrollGrace = 0D;
            this.zedGraphControl1.ScrollMaxX = 0D;
            this.zedGraphControl1.ScrollMaxY = 0D;
            this.zedGraphControl1.ScrollMaxY2 = 0D;
            this.zedGraphControl1.ScrollMinX = 0D;
            this.zedGraphControl1.ScrollMinY = 0D;
            this.zedGraphControl1.ScrollMinY2 = 0D;
            this.zedGraphControl1.Size = new System.Drawing.Size(789, 499);
            this.zedGraphControl1.TabIndex = 12;
            this.zedGraphControl1.UseExtendedPrintDialog = true;
            this.zedGraphControl1.Load += new System.EventHandler(this.zedGraphControl1_Load);
            // 
            // checkBox
            // 
            this.checkBox.FormattingEnabled = true;
            this.checkBox.Items.AddRange(new object[] {
            "Node 1",
            "Node 2"});
            this.checkBox.Location = new System.Drawing.Point(79, 113);
            this.checkBox.Name = "checkBox";
            this.checkBox.Size = new System.Drawing.Size(219, 429);
            this.checkBox.TabIndex = 13;
            this.checkBox.SelectedIndexChanged += new System.EventHandler(this.checkBox_SelectedIndexChanged);
            // 
            // typeGraph
            // 
            this.typeGraph.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.typeGraph.Items.AddRange(new object[] {
            "Irradiance",
            "Short Current",
            "Temperature"});
            this.typeGraph.Location = new System.Drawing.Point(659, 12);
            this.typeGraph.Name = "typeGraph";
            this.typeGraph.Size = new System.Drawing.Size(121, 24);
            this.typeGraph.TabIndex = 14;
            this.typeGraph.Tag = "Irradiance";
            this.typeGraph.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged_1);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1468, 569);
            this.Controls.Add(this.typeGraph);
            this.Controls.Add(this.checkBox);
            this.Controls.Add(this.zedGraphControl1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtRecieve);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.cbox);
            this.Controls.Add(this.Port);
            this.Name = "Form1";
            this.Text = "Lora";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label Port;
        private System.Windows.Forms.ComboBox cbox;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TextBox txtRecieve;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.Label label1;
        private ZedGraph.ZedGraphControl zedGraphControl1;
        private System.Windows.Forms.CheckedListBox checkBox;
        public System.Windows.Forms.ComboBox typeGraph;
    }
}

