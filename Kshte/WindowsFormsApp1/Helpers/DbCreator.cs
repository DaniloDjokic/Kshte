using System.Windows.Forms;
using Kshte.DBTools;
using Kshte.Properties;
using Dapper;

namespace Kshte.Helpers
{
    internal class DbCreator : FirstRunHandler
    {
        public bool DbReady { get; private set; } = false;

        protected override void AssignIsFirstRun(out bool isFirstRun)
        {
            isFirstRun = Settings.Default.FirstRun;
        }

        protected override void ActionOnFirstRun()
        {
            if (!DBConnector.CheckForDatabase())
            {
                if (!QueryUserCreation()) return;

                DBConnector.StartAndOpenDB(true);
                DBSeeder.InitializeDatabase(DBConnector.Connection);
                DbReady = true;
            }
            else
            {
                if (!QueryUserForceCreation())
                {
                    DBConnector.StartAndOpenDB(true);
                    DBSeeder.InitializeDatabase(DBConnector.Connection);
                }
                DbReady = true;
            }
        }

        protected override void ActionOnOtherRuns()
        {
            if (!DBConnector.CheckForDatabase())
            {
                if (!QueryUserCreation()) return;

                DBConnector.StartAndOpenDB(true);
                DBSeeder.InitializeDatabase(DBConnector.Connection);
                DbReady = true;
            }
            else
            {
                DbReady = true;
            }
        }

        public bool QueryUserForceCreation()
        {
            DialogResult result = MessageBox.Show("Postojeća baza je detektovana. Da li želite da se ona obriše i kreira nova baza? \nNAPOMENA: Stara baze je možda lošeg formata.",
                "Prvo pokretanje aplikacije", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            return result == DialogResult.Yes ?  true : false;
        }

        public bool QueryUserCreation()
        {
            DialogResult result = MessageBox.Show("Nije detektovana postojeća baza podataka. Ova aplikacija se ne može pokrenuti bez baze podataka. Da li želite da kreirate novu bazu?",
                "Kreiranje nove baze", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            return result == DialogResult.Yes ? true : false;
        }

        protected override void ResetFirstRunFlag()
        {
            Settings.Default.FirstRun = false;
            Settings.Default.Save();
        }
    }
}