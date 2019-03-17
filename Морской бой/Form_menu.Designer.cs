namespace Морской_бой
{
    partial class Form_menu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_menu));
            this.but_help = new System.Windows.Forms.Button();
            this.but_exit = new System.Windows.Forms.Button();
            this.but_start_game = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // but_help
            // 
            this.but_help.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.but_help.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.but_help.Font = new System.Drawing.Font("Arial Narrow", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.but_help.Location = new System.Drawing.Point(594, 542);
            this.but_help.Name = "but_help";
            this.but_help.Size = new System.Drawing.Size(160, 35);
            this.but_help.TabIndex = 2;
            this.but_help.Text = "Правила игры";
            this.but_help.UseVisualStyleBackColor = false;
            this.but_help.Click += new System.EventHandler(this.but_help_Click);
            // 
            // but_exit
            // 
            this.but_exit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.but_exit.BackColor = System.Drawing.Color.Red;
            this.but_exit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.but_exit.Font = new System.Drawing.Font("Arial Narrow", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.but_exit.Location = new System.Drawing.Point(12, 545);
            this.but_exit.Name = "but_exit";
            this.but_exit.Size = new System.Drawing.Size(150, 35);
            this.but_exit.TabIndex = 5;
            this.but_exit.Text = "Выйти из игры";
            this.but_exit.UseVisualStyleBackColor = false;
            this.but_exit.Click += new System.EventHandler(this.but_exit_Click);
            // 
            // but_start_game
            // 
            this.but_start_game.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.but_start_game.BackColor = System.Drawing.Color.Lime;
            this.but_start_game.Font = new System.Drawing.Font("Arial Narrow", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.but_start_game.Location = new System.Drawing.Point(594, 12);
            this.but_start_game.Name = "but_start_game";
            this.but_start_game.Size = new System.Drawing.Size(160, 70);
            this.but_start_game.TabIndex = 1;
            this.but_start_game.Text = "Начать игру";
            this.but_start_game.UseVisualStyleBackColor = false;
            this.but_start_game.Click += new System.EventHandler(this.but_start_game_Click);
            // 
            // Form_menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(766, 589);
            this.Controls.Add(this.but_exit);
            this.Controls.Add(this.but_help);
            this.Controls.Add(this.but_start_game);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form_menu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Морской бой";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button but_help;
        private System.Windows.Forms.Button but_exit;
        private System.Windows.Forms.Button but_start_game;
    }
}

