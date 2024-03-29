﻿using System.Windows.Forms;
using WindowsFormsApp1.DBTools;
using WindowsFormsApp1.Properties;
using Dapper;

namespace WindowsFormsApp1.Helpers
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
            if (DBConnector.CheckForDatabase() &&
                QueryUserForceCreation())
            {
                DBConnector.StartAndOpenDB(true);
                DBSeeder.InitializeDatabase(DBConnector.Connection);
                DbReady = true;
            }
            else
            {
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
            DialogResult result = MessageBox.Show("Postojeća baza je detektovana. Ukoliko je postojeća baza nekompatibilna sa aplikacijom, može doći do grešaka. Da li želite da se postojeća baza obriše i kreira nova baza?",
                "Prvo pokretanje aplikacije", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            return result == DialogResult.Yes ?  true : false;
        }

        public bool QueryUserCreation()
        {
            DialogResult result = MessageBox.Show("Nije detektovana postojeća baza. Ova aplikacija se ne može pokrenti bez baze podataka. Da li želite da kreirate novu bazu?",
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