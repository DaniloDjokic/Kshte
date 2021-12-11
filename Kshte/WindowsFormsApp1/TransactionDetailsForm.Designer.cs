namespace WindowsFormsApp1
{
    partial class TransactionDetailsForm
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
            this.detailsGridView = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.detailsGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // detailsGridView
            // 
            this.detailsGridView.AllowUserToAddRows = false;
            this.detailsGridView.AllowUserToDeleteRows = false;
            this.detailsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.detailsGridView.Location = new System.Drawing.Point(12, 12);
            this.detailsGridView.Name = "detailsGridView";
            this.detailsGridView.RowHeadersWidth = 51;
            this.detailsGridView.RowTemplate.Height = 24;
            this.detailsGridView.Size = new System.Drawing.Size(951, 308);
            this.detailsGridView.TabIndex = 0;
            // 
            // TransactionDetailsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(975, 332);
            this.Controls.Add(this.detailsGridView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "TransactionDetailsForm";
            this.Text = "TransactionDetailsForm";
            ((System.ComponentModel.ISupportInitialize)(this.detailsGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView detailsGridView;
    }
}