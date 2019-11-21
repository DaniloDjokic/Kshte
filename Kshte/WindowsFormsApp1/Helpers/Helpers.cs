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
    }
}
