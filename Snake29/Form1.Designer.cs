
namespace Snake29
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.upper_Panel = new System.Windows.Forms.Panel();
            this.Ex_but = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SGC = new Tao.Platform.Windows.SimpleOpenGlControl();
            this.t1 = new System.Windows.Forms.Timer(this.components);
            this.rightMenuPanel = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.cy = new System.Windows.Forms.Label();
            this.cx = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Stop_but = new System.Windows.Forms.Button();
            this.Start_but = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.upper_Panel.SuspendLayout();
            this.rightMenuPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // upper_Panel
            // 
            this.upper_Panel.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.upper_Panel.Controls.Add(this.Ex_but);
            this.upper_Panel.Controls.Add(this.label1);
            this.upper_Panel.Dock = System.Windows.Forms.DockStyle.Top;
            this.upper_Panel.Location = new System.Drawing.Point(0, 0);
            this.upper_Panel.Name = "upper_Panel";
            this.upper_Panel.Size = new System.Drawing.Size(1280, 45);
            this.upper_Panel.TabIndex = 0;
            this.upper_Panel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.upper_Panel_MouseDown);
            this.upper_Panel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.upper_Panel_MouseMove);
            // 
            // Ex_but
            // 
            this.Ex_but.AutoSize = true;
            this.Ex_but.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Ex_but.ForeColor = System.Drawing.Color.Black;
            this.Ex_but.Location = new System.Drawing.Point(1256, 1);
            this.Ex_but.Name = "Ex_but";
            this.Ex_but.Size = new System.Drawing.Size(21, 20);
            this.Ex_but.TabIndex = 1;
            this.Ex_but.Text = "X";
            this.Ex_but.Click += new System.EventHandler(this.Ex_but_Click);
            this.Ex_but.MouseLeave += new System.EventHandler(this.Ex_but_MouseLeave);
            this.Ex_but.MouseHover += new System.EventHandler(this.Ex_but_MouseHover);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe Script", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(511, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(203, 44);
            this.label1.TabIndex = 0;
            this.label1.Text = "SNAKE - 29";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SGC
            // 
            this.SGC.AccumBits = ((byte)(0));
            this.SGC.AutoCheckErrors = false;
            this.SGC.AutoFinish = false;
            this.SGC.AutoMakeCurrent = true;
            this.SGC.AutoSwapBuffers = true;
            this.SGC.BackColor = System.Drawing.Color.Black;
            this.SGC.ColorBits = ((byte)(32));
            this.SGC.DepthBits = ((byte)(16));
            this.SGC.Location = new System.Drawing.Point(0, 42);
            this.SGC.Name = "SGC";
            this.SGC.Size = new System.Drawing.Size(983, 983);
            this.SGC.StencilBits = ((byte)(0));
            this.SGC.TabIndex = 1;
            this.SGC.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SGC_KeyDown);
            // 
            // t1
            // 
            this.t1.Interval = 11;
            this.t1.Tick += new System.EventHandler(this.t1_Tick);
            // 
            // rightMenuPanel
            // 
            this.rightMenuPanel.Controls.Add(this.label4);
            this.rightMenuPanel.Controls.Add(this.label3);
            this.rightMenuPanel.Controls.Add(this.textBox1);
            this.rightMenuPanel.Controls.Add(this.cy);
            this.rightMenuPanel.Controls.Add(this.cx);
            this.rightMenuPanel.Controls.Add(this.label2);
            this.rightMenuPanel.Controls.Add(this.panel1);
            this.rightMenuPanel.Location = new System.Drawing.Point(981, 42);
            this.rightMenuPanel.Name = "rightMenuPanel";
            this.rightMenuPanel.Size = new System.Drawing.Size(299, 983);
            this.rightMenuPanel.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(175, 854);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "num_of_fruits:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(251, 851);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(36, 20);
            this.textBox1.TabIndex = 4;
            // 
            // cy
            // 
            this.cy.AutoSize = true;
            this.cy.Location = new System.Drawing.Point(4, 21);
            this.cy.Name = "cy";
            this.cy.Size = new System.Drawing.Size(33, 13);
            this.cy.TabIndex = 3;
            this.cy.Text = "c.y = ";
            // 
            // cx
            // 
            this.cx.AutoSize = true;
            this.cx.Location = new System.Drawing.Point(4, 4);
            this.cx.Name = "cx";
            this.cx.Size = new System.Drawing.Size(33, 13);
            this.cx.TabIndex = 2;
            this.cx.Text = "c.x = ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 854);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "t1 disabled";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.Stop_but);
            this.panel1.Controls.Add(this.Start_but);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 870);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(299, 113);
            this.panel1.TabIndex = 0;
            // 
            // Stop_but
            // 
            this.Stop_but.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Stop_but.Enabled = false;
            this.Stop_but.Font = new System.Drawing.Font("Segoe Script", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Stop_but.Location = new System.Drawing.Point(75, 30);
            this.Stop_but.Name = "Stop_but";
            this.Stop_but.Size = new System.Drawing.Size(160, 50);
            this.Stop_but.TabIndex = 1;
            this.Stop_but.Text = "STOP";
            this.Stop_but.UseVisualStyleBackColor = true;
            this.Stop_but.Visible = false;
            this.Stop_but.Click += new System.EventHandler(this.Stop_but_Click);
            // 
            // Start_but
            // 
            this.Start_but.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Start_but.Font = new System.Drawing.Font("Segoe Script", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Start_but.Location = new System.Drawing.Point(75, 30);
            this.Start_but.Name = "Start_but";
            this.Start_but.Size = new System.Drawing.Size(160, 50);
            this.Start_but.TabIndex = 0;
            this.Start_but.Text = "START";
            this.Start_but.UseVisualStyleBackColor = true;
            this.Start_but.Click += new System.EventHandler(this.Start_but_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 38);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(22, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "pp:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1280, 1024);
            this.Controls.Add(this.rightMenuPanel);
            this.Controls.Add(this.SGC);
            this.Controls.Add(this.upper_Panel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.upper_Panel.ResumeLayout(false);
            this.upper_Panel.PerformLayout();
            this.rightMenuPanel.ResumeLayout(false);
            this.rightMenuPanel.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel upper_Panel;
        private System.Windows.Forms.Label Ex_but;
        private System.Windows.Forms.Label label1;
        private Tao.Platform.Windows.SimpleOpenGlControl SGC;
        private System.Windows.Forms.Timer t1;
        private System.Windows.Forms.Panel rightMenuPanel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button Stop_but;
        private System.Windows.Forms.Button Start_but;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label cy;
        private System.Windows.Forms.Label cx;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label4;
    }
}

