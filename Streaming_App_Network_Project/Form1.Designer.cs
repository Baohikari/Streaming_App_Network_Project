namespace Streaming_App_Network_Project
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.server_request_btn = new System.Windows.Forms.Button();
            this.client_request_btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(279, 85);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(263, 36);
            this.label1.TabIndex = 0;
            this.label1.Text = "STREAMING APP";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(344, 170);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(124, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "Who are you";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // server_request_btn
            // 
            this.server_request_btn.Location = new System.Drawing.Point(262, 252);
            this.server_request_btn.Name = "server_request_btn";
            this.server_request_btn.Size = new System.Drawing.Size(101, 39);
            this.server_request_btn.TabIndex = 2;
            this.server_request_btn.Text = "I am server";
            this.server_request_btn.UseVisualStyleBackColor = true;
            this.server_request_btn.Click += new System.EventHandler(this.server_request_btn_Click);
            // 
            // client_request_btn
            // 
            this.client_request_btn.Location = new System.Drawing.Point(481, 252);
            this.client_request_btn.Name = "client_request_btn";
            this.client_request_btn.Size = new System.Drawing.Size(99, 39);
            this.client_request_btn.TabIndex = 3;
            this.client_request_btn.Text = "I am client";
            this.client_request_btn.UseVisualStyleBackColor = true;
            this.client_request_btn.Click += new System.EventHandler(this.client_request_btn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.client_request_btn);
            this.Controls.Add(this.server_request_btn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button server_request_btn;
        private System.Windows.Forms.Button client_request_btn;
    }
}

