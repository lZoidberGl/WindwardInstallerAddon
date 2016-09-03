namespace WindwardInstallerAddon
{
    partial class GameHasher
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
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.BrowseGame = new System.Windows.Forms.Button();
            this.HashFiles = new System.Windows.Forms.Button();
            this.SaveIt = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(0, 118);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(377, 290);
            this.listBox1.TabIndex = 0;
            // 
            // BrowseGame
            // 
            this.BrowseGame.Dock = System.Windows.Forms.DockStyle.Top;
            this.BrowseGame.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BrowseGame.Location = new System.Drawing.Point(0, 0);
            this.BrowseGame.Name = "BrowseGame";
            this.BrowseGame.Size = new System.Drawing.Size(377, 34);
            this.BrowseGame.TabIndex = 1;
            this.BrowseGame.Text = "Указать папку с игрой";
            this.BrowseGame.UseVisualStyleBackColor = true;
            this.BrowseGame.Click += new System.EventHandler(this.BrowseGame_Click);
            // 
            // HashFiles
            // 
            this.HashFiles.Dock = System.Windows.Forms.DockStyle.Top;
            this.HashFiles.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.HashFiles.Location = new System.Drawing.Point(0, 34);
            this.HashFiles.Name = "HashFiles";
            this.HashFiles.Size = new System.Drawing.Size(377, 30);
            this.HashFiles.TabIndex = 2;
            this.HashFiles.Text = "Хешировать";
            this.HashFiles.UseVisualStyleBackColor = true;
            this.HashFiles.Click += new System.EventHandler(this.HashFiles_Click);
            // 
            // SaveIt
            // 
            this.SaveIt.Dock = System.Windows.Forms.DockStyle.Top;
            this.SaveIt.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SaveIt.Location = new System.Drawing.Point(0, 64);
            this.SaveIt.Name = "SaveIt";
            this.SaveIt.Size = new System.Drawing.Size(377, 30);
            this.SaveIt.TabIndex = 4;
            this.SaveIt.Text = "Сохранить";
            this.SaveIt.UseVisualStyleBackColor = true;
            this.SaveIt.Click += new System.EventHandler(this.SaveIt_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.progressBar1.Location = new System.Drawing.Point(0, 94);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(377, 23);
            this.progressBar1.TabIndex = 5;
            // 
            // GameHasher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(377, 408);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.SaveIt);
            this.Controls.Add(this.HashFiles);
            this.Controls.Add(this.BrowseGame);
            this.Controls.Add(this.listBox1);
            this.Name = "GameHasher";
            this.Text = "GameHasher";
            this.Load += new System.EventHandler(this.GameHasher_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button BrowseGame;
        private System.Windows.Forms.Button HashFiles;
        private System.Windows.Forms.Button SaveIt;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}