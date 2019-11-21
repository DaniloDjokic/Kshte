using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.DBTools;

namespace WindowsFormsApp1.Models
{
    public class Category : IEquatable<Category>
    {
        #region DB Properties
        public int ID { get; internal set; }
        public string Name { get; internal set; }
        #endregion

        public Category() { }

        public bool Equals(Category other)
        {
            if (other.ID == ID)
            {
                return true;
            }

            return false;
        }
    }
}
