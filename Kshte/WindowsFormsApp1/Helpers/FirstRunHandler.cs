using System;
using Kshte.DBTools;
using Kshte.Properties;
using Dapper;

namespace Kshte.Helpers
{
    public abstract class FirstRunHandler
    {

        private bool isFirstRun;
        public bool IsFirstRun
        {
            get => isFirstRun;
            protected set => isFirstRun = value;
        } 

        protected FirstRunHandler()
        {
            CheckIsFirstRun();
        }

        public bool CheckIsFirstRun()
        {
            AssignIsFirstRun(out isFirstRun);
            return IsFirstRun;
        }

        protected abstract void AssignIsFirstRun(out bool isFirstRun);

        protected abstract void ActionOnFirstRun();

        protected virtual void ActionOnOtherRuns() { }

        protected virtual void ResetFirstRunFlag() { }

        public void HandleRun()
        {
            if (CheckIsFirstRun())
            {
                ActionOnFirstRun();
                ResetFirstRunFlag();
            }
            else
                ActionOnOtherRuns();
        }
    }
}
