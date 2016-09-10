namespace Gold.VisioAddIn
{
    partial class ResultForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing )
        {
            if ( disposing && (components != null) )
            {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ResultForm));
            this.btnOk = new System.Windows.Forms.Button();
            this.dataGrid = new System.Windows.Forms.DataGridView();
            this.ShapeId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.labelOk = new System.Windows.Forms.Label();
            this.labelNotOk = new System.Windows.Forms.Label();
            this.btnMore = new System.Windows.Forms.Button();
            this.panelInfo = new System.Windows.Forms.Panel();
            this.labelInfo = new System.Windows.Forms.Label();
            this.labelWarning = new System.Windows.Forms.Label();
            this.labelError = new System.Windows.Forms.Label();
            this.panelWarn = new System.Windows.Forms.Panel();
            this.panelError = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOk.Location = new System.Drawing.Point(275, 35);
            this.btnOk.Margin = new System.Windows.Forms.Padding(2);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(90, 30);
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // dataGrid
            // 
            this.dataGrid.AllowUserToAddRows = false;
            this.dataGrid.AllowUserToDeleteRows = false;
            this.dataGrid.AllowUserToResizeColumns = false;
            this.dataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ShapeId,
            this.Code,
            this.Description});
            this.dataGrid.Location = new System.Drawing.Point(9, 76);
            this.dataGrid.Margin = new System.Windows.Forms.Padding(2);
            this.dataGrid.Name = "dataGrid";
            this.dataGrid.RowHeadersVisible = false;
            this.dataGrid.RowTemplate.Height = 24;
            this.dataGrid.Size = new System.Drawing.Size(452, 237);
            this.dataGrid.TabIndex = 1;
            // 
            // ShapeId
            // 
            this.ShapeId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ShapeId.DataPropertyName = "ShapeId";
            this.ShapeId.HeaderText = "Shape";
            this.ShapeId.MinimumWidth = 150;
            this.ShapeId.Name = "ShapeId";
            this.ShapeId.ReadOnly = true;
            this.ShapeId.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ShapeId.Width = 150;
            // 
            // Code
            // 
            this.Code.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Code.DataPropertyName = "Code";
            this.Code.HeaderText = "Code";
            this.Code.MinimumWidth = 60;
            this.Code.Name = "Code";
            this.Code.ReadOnly = true;
            this.Code.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Code.Width = 60;
            // 
            // Description
            // 
            this.Description.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Description.DataPropertyName = "Description";
            this.Description.HeaderText = "Description";
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            this.Description.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // labelOk
            // 
            this.labelOk.AutoSize = true;
            this.labelOk.Location = new System.Drawing.Point(10, 11);
            this.labelOk.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelOk.Name = "labelOk";
            this.labelOk.Size = new System.Drawing.Size(164, 13);
            this.labelOk.TabIndex = 2;
            this.labelOk.Text = "Command executed successfully.";
            // 
            // labelNotOk
            // 
            this.labelNotOk.AutoSize = true;
            this.labelNotOk.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelNotOk.Location = new System.Drawing.Point(10, 11);
            this.labelNotOk.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelNotOk.Name = "labelNotOk";
            this.labelNotOk.Size = new System.Drawing.Size(401, 16);
            this.labelNotOk.TabIndex = 2;
            this.labelNotOk.Text = "Command did not complete successfully, please review list of errors.";
            // 
            // btnMore
            // 
            this.btnMore.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMore.Location = new System.Drawing.Point(369, 35);
            this.btnMore.Margin = new System.Windows.Forms.Padding(2);
            this.btnMore.Name = "btnMore";
            this.btnMore.Size = new System.Drawing.Size(90, 30);
            this.btnMore.TabIndex = 3;
            this.btnMore.Text = "Details";
            this.btnMore.UseVisualStyleBackColor = true;
            this.btnMore.Click += new System.EventHandler(this.btnMore_Click);
            // 
            // panelInfo
            // 
            this.panelInfo.BackgroundImage = global::Gold.VisioAddIn.Properties.Resources.IconInformation;
            this.panelInfo.Location = new System.Drawing.Point(15, 44);
            this.panelInfo.Margin = new System.Windows.Forms.Padding(2);
            this.panelInfo.Name = "panelInfo";
            this.panelInfo.Size = new System.Drawing.Size(12, 13);
            this.panelInfo.TabIndex = 4;
            // 
            // labelInfo
            // 
            this.labelInfo.AutoSize = true;
            this.labelInfo.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelInfo.Location = new System.Drawing.Point(28, 44);
            this.labelInfo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(39, 16);
            this.labelInfo.TabIndex = 5;
            this.labelInfo.Text = "Info: ";
            // 
            // labelWarning
            // 
            this.labelWarning.AutoSize = true;
            this.labelWarning.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelWarning.Location = new System.Drawing.Point(100, 44);
            this.labelWarning.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelWarning.Name = "labelWarning";
            this.labelWarning.Size = new System.Drawing.Size(48, 16);
            this.labelWarning.TabIndex = 6;
            this.labelWarning.Text = "Warn: ";
            // 
            // labelError
            // 
            this.labelError.AutoSize = true;
            this.labelError.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelError.Location = new System.Drawing.Point(179, 44);
            this.labelError.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelError.Name = "labelError";
            this.labelError.Size = new System.Drawing.Size(52, 16);
            this.labelError.TabIndex = 7;
            this.labelError.Text = "Errors: ";
            // 
            // panelWarn
            // 
            this.panelWarn.BackgroundImage = global::Gold.VisioAddIn.Properties.Resources.IconWarn;
            this.panelWarn.Location = new System.Drawing.Point(87, 44);
            this.panelWarn.Margin = new System.Windows.Forms.Padding(2);
            this.panelWarn.Name = "panelWarn";
            this.panelWarn.Size = new System.Drawing.Size(12, 13);
            this.panelWarn.TabIndex = 5;
            // 
            // panelError
            // 
            this.panelError.BackgroundImage = global::Gold.VisioAddIn.Properties.Resources.IconError;
            this.panelError.Location = new System.Drawing.Point(166, 44);
            this.panelError.Margin = new System.Windows.Forms.Padding(2);
            this.panelError.Name = "panelError";
            this.panelError.Size = new System.Drawing.Size(12, 13);
            this.panelError.TabIndex = 5;
            // 
            // ResultForm
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnOk;
            this.ClientSize = new System.Drawing.Size(470, 325);
            this.Controls.Add(this.panelError);
            this.Controls.Add(this.panelWarn);
            this.Controls.Add(this.labelError);
            this.Controls.Add(this.labelWarning);
            this.Controls.Add(this.labelInfo);
            this.Controls.Add(this.panelInfo);
            this.Controls.Add(this.btnMore);
            this.Controls.Add(this.labelNotOk);
            this.Controls.Add(this.labelOk);
            this.Controls.Add(this.dataGrid);
            this.Controls.Add(this.btnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ResultForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Result";
            this.Shown += new System.EventHandler(this.ResultForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.DataGridView dataGrid;
        private System.Windows.Forms.Label labelOk;
        private System.Windows.Forms.Label labelNotOk;
        private System.Windows.Forms.Button btnMore;
        private System.Windows.Forms.Panel panelInfo;
        private System.Windows.Forms.Label labelInfo;
        private System.Windows.Forms.Label labelWarning;
        private System.Windows.Forms.Label labelError;
        private System.Windows.Forms.Panel panelWarn;
        private System.Windows.Forms.Panel panelError;
        private System.Windows.Forms.DataGridViewTextBoxColumn ShapeId;
        private System.Windows.Forms.DataGridViewTextBoxColumn Code;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
    }
}