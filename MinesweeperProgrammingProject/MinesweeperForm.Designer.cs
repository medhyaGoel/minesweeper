
namespace MinesweeperProgrammingProject
{
    partial class MinesweeperForm
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
            this.exit_button = new System.Windows.Forms.Button();
            this.reset_button = new System.Windows.Forms.Button();
            this.status_label = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // exit_button
            // 
            this.exit_button.Location = new System.Drawing.Point(216, 392);
            this.exit_button.Name = "exit_button";
            this.exit_button.Size = new System.Drawing.Size(75, 23);
            this.exit_button.TabIndex = 0;
            this.exit_button.Text = "Exit";
            this.exit_button.UseVisualStyleBackColor = true;
            this.exit_button.Click += new System.EventHandler(this.exit_button_Click);
            // 
            // reset_button
            // 
            this.reset_button.Location = new System.Drawing.Point(310, 392);
            this.reset_button.Name = "reset_button";
            this.reset_button.Size = new System.Drawing.Size(75, 23);
            this.reset_button.TabIndex = 1;
            this.reset_button.Text = "Play again";
            this.reset_button.UseVisualStyleBackColor = true;
            this.reset_button.Click += new System.EventHandler(this.reset_button_Click);
            // 
            // status_label
            // 
            this.status_label.AutoSize = true;
            this.status_label.Location = new System.Drawing.Point(26, 397);
            this.status_label.Name = "status_label";
            this.status_label.Size = new System.Drawing.Size(72, 13);
            this.status_label.TabIndex = 2;
            this.status_label.Text = "Game status: ";
            // 
            // MinesweeperForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(676, 444);
            this.Controls.Add(this.status_label);
            this.Controls.Add(this.reset_button);
            this.Controls.Add(this.exit_button);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MinesweeperForm";
            this.Text = "Minesweeper";
            this.Load += new System.EventHandler(this.MinesweeperForm_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MinesweeperForm_MouseDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button exit_button;
        private System.Windows.Forms.Button reset_button;
        private System.Windows.Forms.Label status_label;
    }
}

