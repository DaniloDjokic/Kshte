using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public static class Helpers
    {
        public static List<Control> GetControlsRecursive(Control parentControl)
        {
            List<Control> controls = new List<Control>();
            foreach(Control control in parentControl.Controls)
            {
                controls.Add(control);
                if(control.Controls.Count != 0)
                {
                    controls.AddRange(GetControlsRecursive(control));
                }
            }

            return controls;
        }

        public static string GetFullMessage(this Exception e)
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append(e.Message);

            Exception inner = e.InnerException;
            while (inner != null)
            {
                stringBuilder.Append(inner.Message);
                inner = inner.InnerException;
            }

            return stringBuilder.ToString();
        }

        public static bool IsNumericType(this Type t)
        {
            switch (Type.GetTypeCode(t))
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    return true;
                default:
                    return false;
            }
        }
    }
}
