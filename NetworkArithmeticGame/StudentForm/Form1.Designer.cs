namespace StudentForm
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
            this.btnSubmit = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtAnswer = new System.Windows.Forms.TextBox();
            this.lbFirstNumber = new System.Windows.Forms.Label();
            this.lstQuestion = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lbNotConnected = new System.Windows.Forms.Label();
            this.lbConnected = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnSubmit
            // 
            this.btnSubmit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnSubmit.FlatAppearance.BorderSize = 0;
            this.btnSubmit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSubmit.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSubmit.ForeColor = System.Drawing.Color.White;
            this.btnSubmit.Location = new System.Drawing.Point(253, 246);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(131, 38);
            this.btnSubmit.TabIndex = 24;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = false;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(77, 158);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 21);
            this.label1.TabIndex = 22;
            this.label1.Text = "Your Answer:";
            // 
            // txtAnswer
            // 
            this.txtAnswer.BackColor = System.Drawing.Color.White;
            this.txtAnswer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAnswer.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAnswer.Location = new System.Drawing.Point(206, 153);
            this.txtAnswer.Multiline = true;
            this.txtAnswer.Name = "txtAnswer";
            this.txtAnswer.Size = new System.Drawing.Size(178, 35);
            this.txtAnswer.TabIndex = 23;
            // 
            // lbFirstNumber
            // 
            this.lbFirstNumber.AutoSize = true;
            this.lbFirstNumber.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbFirstNumber.ForeColor = System.Drawing.Color.Black;
            this.lbFirstNumber.Location = new System.Drawing.Point(77, 96);
            this.lbFirstNumber.Name = "lbFirstNumber";
            this.lbFirstNumber.Size = new System.Drawing.Size(84, 21);
            this.lbFirstNumber.TabIndex = 20;
            this.lbFirstNumber.Text = "Question:";
            // 
            // lstQuestion
            // 
            this.lstQuestion.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lstQuestion.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstQuestion.Location = new System.Drawing.Point(205, 91);
            this.lstQuestion.Name = "lstQuestion";
            this.lstQuestion.Size = new System.Drawing.Size(178, 36);
            this.lstQuestion.TabIndex = 25;
            this.lstQuestion.UseCompatibleStateImageBehavior = false;
            this.lstQuestion.View = System.Windows.Forms.View.SmallIcon;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "mainColumn";
            this.columnHeader1.Width = 166;
            // 
            // lbNotConnected
            // 
            this.lbNotConnected.AutoSize = true;
            this.lbNotConnected.ForeColor = System.Drawing.Color.Silver;
            this.lbNotConnected.Location = new System.Drawing.Point(405, 23);
            this.lbNotConnected.Name = "lbNotConnected";
            this.lbNotConnected.Size = new System.Drawing.Size(79, 13);
            this.lbNotConnected.TabIndex = 44;
            this.lbNotConnected.Text = "Not Connected";
            // 
            // lbConnected
            // 
            this.lbConnected.AutoSize = true;
            this.lbConnected.ForeColor = System.Drawing.Color.Silver;
            this.lbConnected.Location = new System.Drawing.Point(425, 9);
            this.lbConnected.Name = "lbConnected";
            this.lbConnected.Size = new System.Drawing.Size(59, 13);
            this.lbConnected.TabIndex = 43;
            this.lbConnected.Text = "Connected";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(490, 373);
            this.Controls.Add(this.lbNotConnected);
            this.Controls.Add(this.lbConnected);
            this.Controls.Add(this.lstQuestion);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtAnswer);
            this.Controls.Add(this.lbFirstNumber);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtAnswer;
        private System.Windows.Forms.Label lbFirstNumber;
        private System.Windows.Forms.ListView lstQuestion;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Label lbNotConnected;
        private System.Windows.Forms.Label lbConnected;
    }
}

