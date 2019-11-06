namespace WindowsFormsApp1
{
    partial class TableForm
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
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.allArticlesListView = new System.Windows.Forms.ListView();
            this.hranaBtn = new System.Windows.Forms.Button();
            this.miscBtn = new System.Windows.Forms.Button();
            this.zestinaBtn = new System.Windows.Forms.Button();
            this.topliNapiciBtn = new System.Windows.Forms.Button();
            this.pivoBtn = new System.Windows.Forms.Button();
            this.sokoviBtn = new System.Windows.Forms.Button();
            this.activeArticlesListView = new System.Windows.Forms.ListView();
            this.payAllBtn = new System.Windows.Forms.Button();
            this.deleteOrderBtn = new System.Windows.Forms.Button();
            this.backBtn = new System.Windows.Forms.Button();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.allArticlesListView);
            this.groupBox4.Controls.Add(this.hranaBtn);
            this.groupBox4.Controls.Add(this.miscBtn);
            this.groupBox4.Controls.Add(this.zestinaBtn);
            this.groupBox4.Controls.Add(this.topliNapiciBtn);
            this.groupBox4.Controls.Add(this.pivoBtn);
            this.groupBox4.Controls.Add(this.sokoviBtn);
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(16, 80);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox4.Size = new System.Drawing.Size(553, 299);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Categories";
            // 
            // allArticlesListView
            // 
            this.allArticlesListView.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.allArticlesListView.Location = new System.Drawing.Point(-3, 0);
            this.allArticlesListView.Margin = new System.Windows.Forms.Padding(4);
            this.allArticlesListView.Name = "allArticlesListView";
            this.allArticlesListView.Size = new System.Drawing.Size(579, 375);
            this.allArticlesListView.TabIndex = 6;
            this.allArticlesListView.UseCompatibleStateImageBehavior = false;
            // 
            // hranaBtn
            // 
            this.hranaBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hranaBtn.Location = new System.Drawing.Point(441, 212);
            this.hranaBtn.Margin = new System.Windows.Forms.Padding(4);
            this.hranaBtn.Name = "hranaBtn";
            this.hranaBtn.Size = new System.Drawing.Size(104, 80);
            this.hranaBtn.TabIndex = 5;
            this.hranaBtn.Text = "Hrana";
            this.hranaBtn.UseVisualStyleBackColor = true;
            this.hranaBtn.Click += new System.EventHandler(this.hranaBtn_Click);
            // 
            // miscBtn
            // 
            this.miscBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.miscBtn.Location = new System.Drawing.Point(217, 212);
            this.miscBtn.Margin = new System.Windows.Forms.Padding(4);
            this.miscBtn.Name = "miscBtn";
            this.miscBtn.Size = new System.Drawing.Size(104, 80);
            this.miscBtn.TabIndex = 4;
            this.miscBtn.Text = "Misc";
            this.miscBtn.UseVisualStyleBackColor = true;
            this.miscBtn.Click += new System.EventHandler(this.miscBtn_Click);
            // 
            // zestinaBtn
            // 
            this.zestinaBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.zestinaBtn.Location = new System.Drawing.Point(8, 212);
            this.zestinaBtn.Margin = new System.Windows.Forms.Padding(4);
            this.zestinaBtn.Name = "zestinaBtn";
            this.zestinaBtn.Size = new System.Drawing.Size(104, 80);
            this.zestinaBtn.TabIndex = 3;
            this.zestinaBtn.Text = "Zestina";
            this.zestinaBtn.UseVisualStyleBackColor = true;
            this.zestinaBtn.Click += new System.EventHandler(this.zestinaBtn_Click);
            // 
            // topliNapiciBtn
            // 
            this.topliNapiciBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.topliNapiciBtn.Location = new System.Drawing.Point(441, 37);
            this.topliNapiciBtn.Margin = new System.Windows.Forms.Padding(4);
            this.topliNapiciBtn.Name = "topliNapiciBtn";
            this.topliNapiciBtn.Size = new System.Drawing.Size(104, 80);
            this.topliNapiciBtn.TabIndex = 2;
            this.topliNapiciBtn.Text = "Topli napici";
            this.topliNapiciBtn.UseVisualStyleBackColor = true;
            this.topliNapiciBtn.Click += new System.EventHandler(this.topliNapiciBtn_Click);
            // 
            // pivoBtn
            // 
            this.pivoBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pivoBtn.Location = new System.Drawing.Point(217, 37);
            this.pivoBtn.Margin = new System.Windows.Forms.Padding(4);
            this.pivoBtn.Name = "pivoBtn";
            this.pivoBtn.Size = new System.Drawing.Size(104, 80);
            this.pivoBtn.TabIndex = 1;
            this.pivoBtn.Text = "Pivo";
            this.pivoBtn.UseVisualStyleBackColor = true;
            this.pivoBtn.Click += new System.EventHandler(this.pivoBtn_Click);
            // 
            // sokoviBtn
            // 
            this.sokoviBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sokoviBtn.Location = new System.Drawing.Point(8, 37);
            this.sokoviBtn.Margin = new System.Windows.Forms.Padding(4);
            this.sokoviBtn.Name = "sokoviBtn";
            this.sokoviBtn.Size = new System.Drawing.Size(104, 80);
            this.sokoviBtn.TabIndex = 0;
            this.sokoviBtn.Text = "Sokovi";
            this.sokoviBtn.UseVisualStyleBackColor = true;
            this.sokoviBtn.Click += new System.EventHandler(this.sokoviBtn_Click);
            // 
            // activeArticlesListView
            // 
            this.activeArticlesListView.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.activeArticlesListView.Location = new System.Drawing.Point(612, 81);
            this.activeArticlesListView.Margin = new System.Windows.Forms.Padding(4);
            this.activeArticlesListView.Name = "activeArticlesListView";
            this.activeArticlesListView.Size = new System.Drawing.Size(433, 297);
            this.activeArticlesListView.TabIndex = 2;
            this.activeArticlesListView.UseCompatibleStateImageBehavior = false;
            // 
            // payAllBtn
            // 
            this.payAllBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.payAllBtn.Location = new System.Drawing.Point(612, 386);
            this.payAllBtn.Margin = new System.Windows.Forms.Padding(4);
            this.payAllBtn.Name = "payAllBtn";
            this.payAllBtn.Size = new System.Drawing.Size(161, 69);
            this.payAllBtn.TabIndex = 3;
            this.payAllBtn.Text = "Pay All";
            this.payAllBtn.UseVisualStyleBackColor = true;
            // 
            // deleteOrderBtn
            // 
            this.deleteOrderBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deleteOrderBtn.Location = new System.Drawing.Point(885, 386);
            this.deleteOrderBtn.Margin = new System.Windows.Forms.Padding(4);
            this.deleteOrderBtn.Name = "deleteOrderBtn";
            this.deleteOrderBtn.Size = new System.Drawing.Size(161, 69);
            this.deleteOrderBtn.TabIndex = 4;
            this.deleteOrderBtn.Text = "Delete Order";
            this.deleteOrderBtn.UseVisualStyleBackColor = true;
            // 
            // backBtn
            // 
            this.backBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.backBtn.Location = new System.Drawing.Point(471, 34);
            this.backBtn.Margin = new System.Windows.Forms.Padding(4);
            this.backBtn.Name = "backBtn";
            this.backBtn.Size = new System.Drawing.Size(99, 38);
            this.backBtn.TabIndex = 5;
            this.backBtn.Text = "Back";
            this.backBtn.UseVisualStyleBackColor = true;
            this.backBtn.Click += new System.EventHandler(this.backBtn_Click);
            // 
            // TableForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.ClientSize = new System.Drawing.Size(1067, 470);
            this.Controls.Add(this.backBtn);
            this.Controls.Add(this.deleteOrderBtn);
            this.Controls.Add(this.payAllBtn);
            this.Controls.Add(this.activeArticlesListView);
            this.Controls.Add(this.groupBox4);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "TableForm";
            this.Text = "TableForm";
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button hranaBtn;
        private System.Windows.Forms.Button miscBtn;
        private System.Windows.Forms.Button zestinaBtn;
        private System.Windows.Forms.Button topliNapiciBtn;
        private System.Windows.Forms.Button pivoBtn;
        private System.Windows.Forms.Button sokoviBtn;
        private System.Windows.Forms.ListView activeArticlesListView;
        private System.Windows.Forms.Button payAllBtn;
        private System.Windows.Forms.Button deleteOrderBtn;
        private System.Windows.Forms.Button backBtn;
        private System.Windows.Forms.ListView allArticlesListView;
    }
}