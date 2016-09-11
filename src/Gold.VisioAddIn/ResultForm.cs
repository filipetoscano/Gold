using Gold.Runtime;
using System;
using System.Data;
using System.Threading;
using WinForm = System.Windows.Forms.Form;

namespace Gold.VisioAddIn
{
    public partial class ResultForm : WinForm
    {
        public ResultForm()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Gets the execution result of the command issued over the model(s).
        /// </summary>
        public ModelCommandResult CommandResult
        {
            get;
            set;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResultForm_Shown( object sender, EventArgs e )
        {
            /*
             * 
             */
            DataTable dt = new DataTable();
            dt.Locale = Thread.CurrentThread.CurrentUICulture;
            dt.Columns.Add( "ShapeCode", typeof( string ) );
            dt.Columns.Add( "Code", typeof( int ) );
            dt.Columns.Add( "Description", typeof( string ) );

            int countInfo = 0;
            int countWarn = 0;
            int countError = 0;

            foreach ( ModelCommandPageResult pageResult in this.CommandResult.Pages )
            {
                if ( this.CommandResult.Pages.Count > 1 )
                    dt.Rows.Add( null, null, pageResult.Name );

                foreach ( ModelResultItem item in pageResult.Items )
                {
                    if ( item.ItemType == ModelResultItemType.Error )
                        countError++;

                    if ( item.ItemType == ModelResultItemType.Warning )
                        countWarn++;

                    if ( item.ItemType == ModelResultItemType.Information )
                        countInfo++;

                    dt.Rows.Add( item.VisioShapeId, item.Code, item.Description );
                }
            }


            /*
             * 
             */
            dataGrid.DataSource = dt;

            labelInfo.Text = string.Concat( labelInfo.Text, countInfo );
            labelWarning.Text = string.Concat( labelWarning.Text, countWarn );
            labelError.Text = string.Concat( labelError.Text, countError );
            btnMore.Visible = false;

            if ( countError == 0 )
            {
                labelOk.Visible = true;
                labelNotOk.Visible = false;

                if ( countInfo > 0 || countWarn > 0 )
                    btnMore.Visible = true;
                
                this.Height = 125;
            }
            else
            {
                labelOk.Visible = false;
                labelNotOk.Visible = true;
                
                this.Height = 430;
            }
        }


        private void btnMore_Click( object sender, EventArgs e )
        {
            this.Height = 430;
        }
    }
}

/* eof */