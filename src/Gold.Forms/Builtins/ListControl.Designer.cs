﻿namespace Gold.Forms
{
    partial class ListControl
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
            if ( disposing && ( components != null ) )
            {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.label = new System.Windows.Forms.Label();
            this.comboBox = new System.Windows.Forms.ComboBox();
            this.tableLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayout
            // 
            this.tableLayout.ColumnCount = 2;
            this.tableLayout.ColumnStyles.Add( new System.Windows.Forms.ColumnStyle( System.Windows.Forms.SizeType.Absolute, 150F ) );
            this.tableLayout.ColumnStyles.Add( new System.Windows.Forms.ColumnStyle( System.Windows.Forms.SizeType.Percent, 100F ) );
            this.tableLayout.Controls.Add( this.label, 0, 0 );
            this.tableLayout.Controls.Add( this.comboBox, 1, 0 );
            this.tableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayout.Font = new System.Drawing.Font( "Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (byte) ( 238 ) ) );
            this.tableLayout.Location = new System.Drawing.Point( 0, 0 );
            this.tableLayout.Name = "tableLayout";
            this.tableLayout.RowCount = 1;
            this.tableLayout.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 100F ) );
            this.tableLayout.Size = new System.Drawing.Size( 500, 37 );
            this.tableLayout.TabIndex = 0;
            // 
            // label
            // 
            this.label.Anchor = ( (System.Windows.Forms.AnchorStyles) ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.label.AutoSize = true;
            this.label.Font = new System.Drawing.Font( "Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (byte) ( 238 ) ) );
            this.label.Location = new System.Drawing.Point( 98, 0 );
            this.label.Name = "label";
            this.label.Padding = new System.Windows.Forms.Padding( 0, 6, 0, 0 );
            this.label.Size = new System.Drawing.Size( 49, 27 );
            this.label.TabIndex = 0;
            this.label.Text = "Label";
            // 
            // comboBox
            // 
            this.comboBox.BackColor = System.Drawing.SystemColors.Window;
            this.comboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox.FormattingEnabled = true;
            this.comboBox.Location = new System.Drawing.Point( 153, 3 );
            this.comboBox.Name = "comboBox";
            this.comboBox.Size = new System.Drawing.Size( 344, 29 );
            this.comboBox.TabIndex = 1;
            this.comboBox.Validating += new System.ComponentModel.CancelEventHandler( this.comboBox_Validating );
            this.comboBox.SelectedIndexChanged += new System.EventHandler( this.comboBox_SelectedIndexChanged );
            this.comboBox.Validated += new System.EventHandler( this.comboBox_Validated );
            // 
            // ListControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 8F, 16F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add( this.tableLayout );
            this.Name = "ListControl";
            this.Size = new System.Drawing.Size( 500, 37 );
            this.tableLayout.ResumeLayout( false );
            this.tableLayout.PerformLayout();
            this.ResumeLayout( false );

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayout;
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.ComboBox comboBox;
    }
}
