namespace Морской_бой
{
    partial class Form_game
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_game));
            this.but_reset_locships = new System.Windows.Forms.Button();
            this.but_Four_desk = new System.Windows.Forms.Button();
            this.but_Three_desk = new System.Windows.Forms.Button();
            this.but_Two_desk = new System.Windows.Forms.Button();
            this.but_One_desk = new System.Windows.Forms.Button();
            this.but_Start = new System.Windows.Forms.Button();
            this.but_end_game = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // but_reset_locships
            // 
            this.but_reset_locships.Enabled = false;
            this.but_reset_locships.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.but_reset_locships.Location = new System.Drawing.Point(50, 300);
            this.but_reset_locships.Name = "but_reset_locships";
            this.but_reset_locships.Size = new System.Drawing.Size(209, 50);
            this.but_reset_locships.TabIndex = 0;
            this.but_reset_locships.Text = "Сборсить расположение кораблей";
            this.but_reset_locships.UseVisualStyleBackColor = true;
            this.but_reset_locships.Click += new System.EventHandler(this.but_reset_locships_Click);
            // 
            // but_Four_desk
            // 
            this.but_Four_desk.Enabled = false;
            this.but_Four_desk.Location = new System.Drawing.Point(300, 50);
            this.but_Four_desk.Name = "but_Four_desk";
            this.but_Four_desk.Size = new System.Drawing.Size(130, 50);
            this.but_Four_desk.TabIndex = 1;
            this.but_Four_desk.Text = "Поставить\r\n4-х палубный\r\nкорабль";
            this.but_Four_desk.UseVisualStyleBackColor = true;
            this.but_Four_desk.Click += new System.EventHandler(this.but_Four_desk_Click);
            // 
            // but_Three_desk
            // 
            this.but_Three_desk.Enabled = false;
            this.but_Three_desk.Location = new System.Drawing.Point(300, 106);
            this.but_Three_desk.Name = "but_Three_desk";
            this.but_Three_desk.Size = new System.Drawing.Size(130, 50);
            this.but_Three_desk.TabIndex = 2;
            this.but_Three_desk.Text = "Поставить\r\n3-х палубный\r\nкорабль";
            this.but_Three_desk.UseVisualStyleBackColor = true;
            this.but_Three_desk.Click += new System.EventHandler(this.but_Three_desk_Click);
            // 
            // but_Two_desk
            // 
            this.but_Two_desk.Enabled = false;
            this.but_Two_desk.Location = new System.Drawing.Point(300, 162);
            this.but_Two_desk.Name = "but_Two_desk";
            this.but_Two_desk.Size = new System.Drawing.Size(130, 50);
            this.but_Two_desk.TabIndex = 3;
            this.but_Two_desk.Text = "Поставить\r\n2-х палубный\r\nкорабль";
            this.but_Two_desk.UseVisualStyleBackColor = true;
            this.but_Two_desk.Click += new System.EventHandler(this.but_Two_desk_Click);
            // 
            // but_One_desk
            // 
            this.but_One_desk.Enabled = false;
            this.but_One_desk.Location = new System.Drawing.Point(300, 218);
            this.but_One_desk.Name = "but_One_desk";
            this.but_One_desk.Size = new System.Drawing.Size(130, 50);
            this.but_One_desk.TabIndex = 4;
            this.but_One_desk.Text = "Поставить\r\nоднопалубный\r\nкорабль";
            this.but_One_desk.UseVisualStyleBackColor = true;
            this.but_One_desk.Click += new System.EventHandler(this.but_One_desk_Click);
            // 
            // but_Start
            // 
            this.but_Start.Enabled = false;
            this.but_Start.Location = new System.Drawing.Point(300, 300);
            this.but_Start.Name = "but_Start";
            this.but_Start.Size = new System.Drawing.Size(130, 50);
            this.but_Start.TabIndex = 5;
            this.but_Start.Text = "Закончить расстановку кораблей и начать игру";
            this.but_Start.UseVisualStyleBackColor = true;
            this.but_Start.Click += new System.EventHandler(this.but_Start_Click);
            // 
            // but_end_game
            // 
            this.but_end_game.BackColor = System.Drawing.Color.Red;
            this.but_end_game.Font = new System.Drawing.Font("Arial Narrow", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.but_end_game.Location = new System.Drawing.Point(50, 545);
            this.but_end_game.Name = "but_end_game";
            this.but_end_game.Size = new System.Drawing.Size(150, 35);
            this.but_end_game.TabIndex = 6;
            this.but_end_game.Text = "Выйти из игры";
            this.but_end_game.UseVisualStyleBackColor = false;
            this.but_end_game.Click += new System.EventHandler(this.but_end_game_Click);
            // 
            // Form_game
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Морской_бой.Properties.Resources.GameScreen;
            this.ClientSize = new System.Drawing.Size(766, 589);
            this.Controls.Add(this.but_end_game);
            this.Controls.Add(this.but_Start);
            this.Controls.Add(this.but_One_desk);
            this.Controls.Add(this.but_Two_desk);
            this.Controls.Add(this.but_Three_desk);
            this.Controls.Add(this.but_Four_desk);
            this.Controls.Add(this.but_reset_locships);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form_game";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Морской бой";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button but_reset_locships;
        private System.Windows.Forms.Button but_Four_desk;
        private System.Windows.Forms.Button but_Three_desk;
        private System.Windows.Forms.Button but_Two_desk;
        private System.Windows.Forms.Button but_One_desk;
        private System.Windows.Forms.Button but_Start;
        private System.Windows.Forms.Button but_end_game;
    }
}