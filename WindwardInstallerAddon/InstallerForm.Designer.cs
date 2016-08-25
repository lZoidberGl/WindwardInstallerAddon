namespace WindwardInstallerAddon
{
    partial class InstallerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InstallerForm));
            this.InstallButton = new System.Windows.Forms.Button();
            this.GamePath = new System.Windows.Forms.TextBox();
            this.ProgressBar = new System.Windows.Forms.ProgressBar();
            this.Status = new System.Windows.Forms.Label();
            this.BrowseGame = new System.Windows.Forms.Button();
            this.UpdateButton = new System.Windows.Forms.Button();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.StatusPanel = new System.Windows.Forms.Panel();
            this.UpdateProg = new System.Windows.Forms.Button();
            this.NewerVers = new System.Windows.Forms.Label();
            this.SavePath = new System.Windows.Forms.Button();
            this.Tips = new System.Windows.Forms.ToolTip(this.components);
            this.Unzipper = new System.ComponentModel.BackgroundWorker();
            this.button1 = new System.Windows.Forms.Button();
            this.DevButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // InstallButton
            // 
            this.InstallButton.Location = new System.Drawing.Point(11, 8);
            this.InstallButton.Name = "InstallButton";
            this.InstallButton.Size = new System.Drawing.Size(100, 23);
            this.InstallButton.TabIndex = 0;
            this.InstallButton.Text = "Установить";
            this.InstallButton.UseVisualStyleBackColor = true;
            this.InstallButton.Click += new System.EventHandler(this.InstallButton_Click);
            // 
            // GamePath
            // 
            this.GamePath.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GamePath.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.GamePath.Location = new System.Drawing.Point(40, 37);
            this.GamePath.Name = "GamePath";
            this.GamePath.Size = new System.Drawing.Size(249, 21);
            this.GamePath.TabIndex = 1;
            this.GamePath.TextChanged += new System.EventHandler(this.GamePath_TextChanged);
            // 
            // ProgressBar
            // 
            this.ProgressBar.Location = new System.Drawing.Point(11, 65);
            this.ProgressBar.Name = "ProgressBar";
            this.ProgressBar.Size = new System.Drawing.Size(311, 23);
            this.ProgressBar.TabIndex = 2;
            // 
            // Status
            // 
            this.Status.BackColor = System.Drawing.Color.Transparent;
            this.Status.Font = new System.Drawing.Font("Segoe Script", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Status.ForeColor = System.Drawing.Color.Snow;
            this.Status.Location = new System.Drawing.Point(12, 93);
            this.Status.Name = "Status";
            this.Status.Size = new System.Drawing.Size(310, 23);
            this.Status.TabIndex = 3;
            this.Status.Text = "0MB / 0MB    0%\r\n";
            this.Status.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BrowseGame
            // 
            this.BrowseGame.Location = new System.Drawing.Point(295, 36);
            this.BrowseGame.Name = "BrowseGame";
            this.BrowseGame.Size = new System.Drawing.Size(28, 23);
            this.BrowseGame.TabIndex = 0;
            this.BrowseGame.Text = "...";
            this.BrowseGame.UseVisualStyleBackColor = true;
            this.BrowseGame.Click += new System.EventHandler(this.BrowseGame_Click);
            // 
            // UpdateButton
            // 
            this.UpdateButton.Location = new System.Drawing.Point(117, 8);
            this.UpdateButton.Name = "UpdateButton";
            this.UpdateButton.Size = new System.Drawing.Size(100, 23);
            this.UpdateButton.TabIndex = 0;
            this.UpdateButton.Text = "Обновить";
            this.UpdateButton.UseVisualStyleBackColor = true;
            this.UpdateButton.Click += new System.EventHandler(this.UpdateButton_Click);
            // 
            // DeleteButton
            // 
            this.DeleteButton.Location = new System.Drawing.Point(223, 8);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(100, 23);
            this.DeleteButton.TabIndex = 0;
            this.DeleteButton.Text = "Удалить";
            this.DeleteButton.UseVisualStyleBackColor = true;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // StatusPanel
            // 
            this.StatusPanel.Location = new System.Drawing.Point(39, 36);
            this.StatusPanel.Name = "StatusPanel";
            this.StatusPanel.Size = new System.Drawing.Size(251, 23);
            this.StatusPanel.TabIndex = 4;
            // 
            // UpdateProg
            // 
            this.UpdateProg.Location = new System.Drawing.Point(11, 119);
            this.UpdateProg.Name = "UpdateProg";
            this.UpdateProg.Size = new System.Drawing.Size(311, 23);
            this.UpdateProg.TabIndex = 5;
            this.UpdateProg.Text = "Обновить программу";
            this.UpdateProg.UseVisualStyleBackColor = true;
            this.UpdateProg.Click += new System.EventHandler(this.UpdateProg_Click);
            // 
            // NewerVers
            // 
            this.NewerVers.AutoSize = true;
            this.NewerVers.Location = new System.Drawing.Point(1, 2);
            this.NewerVers.Name = "NewerVers";
            this.NewerVers.Size = new System.Drawing.Size(0, 13);
            this.NewerVers.TabIndex = 6;
            this.NewerVers.Visible = false;
            // 
            // SavePath
            // 
            this.SavePath.BackColor = System.Drawing.Color.Transparent;
            this.SavePath.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("SavePath.BackgroundImage")));
            this.SavePath.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.SavePath.FlatAppearance.BorderSize = 0;
            this.SavePath.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SavePath.Location = new System.Drawing.Point(11, 37);
            this.SavePath.Name = "SavePath";
            this.SavePath.Size = new System.Drawing.Size(20, 20);
            this.SavePath.TabIndex = 7;
            this.Tips.SetToolTip(this.SavePath, "Сохранить путь для след. запуска");
            this.SavePath.UseVisualStyleBackColor = false;
            this.SavePath.Click += new System.EventHandler(this.SavePath_Click);
            // 
            // Unzipper
            // 
            this.Unzipper.DoWork += new System.ComponentModel.DoWorkEventHandler(this.Unzipper_DoWork);
            this.Unzipper.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.Unzipper_RunWorkerCompleted);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(324, 127);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(19, 20);
            this.button1.TabIndex = 8;
            this.button1.Text = "?";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // DevButton
            // 
            this.DevButton.Location = new System.Drawing.Point(0, 135);
            this.DevButton.Name = "DevButton";
            this.DevButton.Size = new System.Drawing.Size(10, 10);
            this.DevButton.TabIndex = 9;
            this.DevButton.Text = "DevButton";
            this.DevButton.UseVisualStyleBackColor = true;
            this.DevButton.Visible = false;
            this.DevButton.Click += new System.EventHandler(this.DevButton_Click);
            // 
            // InstallerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(335, 145);
            this.Controls.Add(this.DevButton);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.SavePath);
            this.Controls.Add(this.NewerVers);
            this.Controls.Add(this.UpdateProg);
            this.Controls.Add(this.Status);
            this.Controls.Add(this.ProgressBar);
            this.Controls.Add(this.GamePath);
            this.Controls.Add(this.BrowseGame);
            this.Controls.Add(this.DeleteButton);
            this.Controls.Add(this.UpdateButton);
            this.Controls.Add(this.InstallButton);
            this.Controls.Add(this.StatusPanel);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "InstallerForm";
            this.Text = "Установщик Windward";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.InstallerForm_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button InstallButton;
        private System.Windows.Forms.TextBox GamePath;
        private System.Windows.Forms.ProgressBar ProgressBar;
        private System.Windows.Forms.Label Status;
        private System.Windows.Forms.Button BrowseGame;
        private System.Windows.Forms.Button UpdateButton;
        private System.Windows.Forms.Button DeleteButton;
        private System.Windows.Forms.Panel StatusPanel;
        private System.Windows.Forms.Button UpdateProg;
        private System.Windows.Forms.Label NewerVers;
        private System.Windows.Forms.Button SavePath;
        private System.Windows.Forms.ToolTip Tips;
        private System.ComponentModel.BackgroundWorker Unzipper;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button DevButton;
    }
}

