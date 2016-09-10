using System;
using System.ComponentModel;
using System.Windows.Forms;
using WinForm = System.Windows.Forms.Form;
using Gold.Runtime;

namespace Gold.VisioAddIn
{
    public partial class ProgressForm : WinForm
    {
        public ProgressForm()
        {
            InitializeComponent();
        }


        public ModelCommand Command
        {
            get;
            set;
        }


        public ModelExportSettings Settings
        {
            get;
            set;
        }


        /// <summary>
        /// As soon as the form is shown, immediately begin the background
        /// worker.
        /// </summary>
        private void ProgressForm_Shown( object sender, EventArgs e )
        {
            this.backgroundWorker.RunWorkerAsync();
        }

        /// <summary>
        /// Background worker: this code, and all of the callbacks, are
        /// running in a different thread than the UI thread: as such, they
        /// *must* not interact at all with any of the UI elements. All
        /// interaction must occur via .ReportProgress.
        /// </summary>
        private void backgroundWorker_DoWork( object sender, DoWorkEventArgs e )
        {
            ModelExporter export = new ModelExporter();
            export.Settings = this.Settings;
            export.PageStart += new EventHandler<PageEventArgs>( export_PageStart );
            export.PageEnd += new EventHandler<PageEventArgs>( export_PageEnd );
            export.StepStart += new EventHandler<PageStepEventArgs>( export_StepStart );
            export.StepEnd += new EventHandler<PageStepEventArgs>( export_StepEnd );

            e.Result = export.Execute( this.Command );
        }


        void export_PageStart( object sender, PageEventArgs e )
        {
            int pc = percentageTask( false, e.PageIndex, e.PageCount );
            this.backgroundWorker.ReportProgress( pc, "major:start" );
        }


        void export_PageEnd( object sender, PageEventArgs e )
        {
            int pc = percentageTask( true, e.PageIndex, e.PageCount );
            this.backgroundWorker.ReportProgress( pc, "major:end" );
        }


        void export_StepStart( object sender, PageStepEventArgs e )
        {
            int pc = percentageTask( false, e.StepIndex, e.StepCount );
            this.backgroundWorker.ReportProgress( pc, "minor:start" );
        }


        void export_StepEnd( object sender, PageStepEventArgs e )
        {
            int pc = percentageTask( true, e.StepIndex, e.StepCount );
            this.backgroundWorker.ReportProgress( pc, "minor:end" );
        }


        private static int percentageTask( bool isEnd, int numerator, int denominator )
        {
            int num = numerator * 2;
            int den = denominator * 2;

            if ( isEnd == false )
                num = num - 1;

            return (int) (num * 100.0 / den);
        }

        /// <summary>
        /// Callback to UI thread whenever the (background) worker performs a unit of
        /// work. This updates the progress bars.
        /// </summary>
        private void backgroundWorker_ProgressChanged( object sender, ProgressChangedEventArgs e )
        {
            string desc = (string) e.UserState;
  
            if ( desc.StartsWith( "major", StringComparison.OrdinalIgnoreCase ) == true )
            {
                this.progressBarMajor.Value = e.ProgressPercentage;

                if ( desc == "major:start" )
                    this.progressBarMinor.Value = 0;
            }
            else
            {
                this.progressBarMinor.Value = e.ProgressPercentage;
            }
        }

        /// <summary>
        /// Callback to UI thread when the (background) worker completes: it automatically
        /// closes the form, so that it doesn't annoy the user anymore.
        /// </summary>
        private void backgroundWorker_RunWorkerCompleted( object sender, RunWorkerCompletedEventArgs e )
        {
            this.CommandResult = (ModelCommandResult) e.Result;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }


        /// <summary>
        /// Gets the execution result of the command issued over the model(s).
        /// </summary>
        public ModelCommandResult CommandResult
        {
            get;
            private set;
        }
    }
}

/* eof */