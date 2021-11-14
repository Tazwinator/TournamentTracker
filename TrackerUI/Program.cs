using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrackerUI
{
    static class Program
    {

        // TODO - move everything over from the old forms to here
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Initialize the database connections
            TrackerLibrary.GlobalConfig.InitializeConnections(TrackerLibrary.DatabaseType.TextFile); // TrackerLibrary.DatabaseType.Sql or .TextFile

            Application.Run(new CreateTeamForm());
            // Application.Run(new TournamentDashboardForm());
        }
    }
}
