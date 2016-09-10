using System;
using WinForm = System.Windows.Forms.Form;

namespace Gold.VisioAddIn
{
    public partial class FindShapeForm : WinForm
    {
        public FindShapeForm()
        {
            InitializeComponent();
        }


        public string ShapeCode
        {
            get
            {
                return textBoxShapeId.Text.Trim();
            }
        }


        private void FindShapeForm_Shown( object sender, EventArgs e )
        {
            textBoxShapeId.Focus();
        }
    }
}

/* eof */