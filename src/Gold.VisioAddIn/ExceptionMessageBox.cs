using System;
using System.Windows.Forms;
using WinForm = System.Windows.Forms.Form;

namespace Gold.VisioAddIn
{
    public partial class ExceptionMessageBox : WinForm
    {
        public ExceptionMessageBox()
        {
            InitializeComponent();
        }


        private void buttonOk_Click( object sender, EventArgs e )
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }


        public static void Show( string message, Exception exception = null )
        {
            #region Validations

            if ( message == null )
                throw new ArgumentNullException( nameof( message ) );

            #endregion


            /*
             * 
             */
            ExceptionMessageBox form = new ExceptionMessageBox();
            form.label.Text = message;
            form.textBox.Text = exception?.ToString() ?? "";

            if ( exception == null )
                form.Height = 147;

            form.ShowDialog();
        }
    }
}
