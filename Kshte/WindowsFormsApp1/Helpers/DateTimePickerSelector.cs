using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1.Helpers
{
    public class DateTimePickerSelector : DateTimeSelector
    {
        private readonly DateTimePicker fromDate;
        private readonly DateTimePicker fromTime;
        private readonly DateTimePicker toDate;
        private readonly DateTimePicker toTime;

        public DateTimePickerSelector(DateTimePicker fromDate, DateTimePicker fromTime, DateTimePicker toDate, DateTimePicker toTime)
        {
            this.fromDate = fromDate;
            this.fromTime = fromTime;
            this.toDate = toDate;
            this.toTime = toTime;

            RefreshBoundaries(DateTimePicker.MinimumDateTime, DateTimePicker.MaximumDateTime);
        }

        public override FromToDateTime GetSelectedBounds()
        {
            FromToDateTime fromToDateTime = new FromToDateTime();

            fromToDateTime.From = fromDate.Value.Date + fromTime.Value.TimeOfDay;
            fromToDateTime.To = toDate.Value.Date + toTime.Value.TimeOfDay;

            return fromToDateTime;
        }

        protected override void UpdateControlsFromBoundaries()
        {
            //We have to set the MinDate and MaxDate to their default values first, because otherwise we could get OutOfRange exceptions when we are setting
            //them, as a DateTimePicker checks whether Min < Max on every individual assignment.

            fromDate.MinDate = DateTimePicker.MinimumDateTime;
            fromTime.MinDate = DateTimePicker.MinimumDateTime;
            fromDate.MaxDate = DateTimePicker.MaximumDateTime;
            fromTime.MaxDate = DateTimePicker.MaximumDateTime;

            toDate.MinDate = DateTimePicker.MinimumDateTime;
            toTime.MinDate = DateTimePicker.MinimumDateTime;
            toDate.MaxDate = DateTimePicker.MaximumDateTime;
            toTime.MaxDate = DateTimePicker.MaximumDateTime;

            fromDate.MinDate = FromBoundary;
            fromTime.MinDate = FromBoundary;
            fromDate.MaxDate = ToBoundary;
            fromTime.MaxDate = ToBoundary;

            toDate.MinDate = FromBoundary;
            toTime.MinDate = FromBoundary;
            toDate.MaxDate = ToBoundary;
            toTime.MaxDate = ToBoundary;

            fromDate.Value = FromBoundary;
            fromTime.Value = FromBoundary;

            toDate.Value = ToBoundary;
            toTime.Value = ToBoundary;
        }
    }
}
