using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Helpers
{
    public struct FromToDateTime
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }

        public bool IsWithinBounds(DateTime dateTime)
        {
            if (dateTime >= From && dateTime <= To)
                return true;
            else
                return false;
        }
    }

    public abstract class DateTimeSelector
    {
        public DateTime FromBoundary { get; private set; }
        public DateTime ToBoundary { get; private set; }

        public void RefreshBoundaries(DateTime fromDateTime, DateTime toDateTime)
        {
            if (fromDateTime > toDateTime)
            {
                throw new ArgumentOutOfRangeException("From DateTime has to represent a time before the To DateTime.");
            }

            FromBoundary = fromDateTime;
            ToBoundary = toDateTime;

            UpdateControlsFromBoundaries();
        }

        protected abstract void UpdateControlsFromBoundaries();

        public abstract FromToDateTime GetSelectedBounds();
    }
}
