using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Kshte.Models;

namespace Kshte
{
    public partial class TransactionDetailsForm : Form
    {
        public TransactionDetailsForm(IEnumerable<TransactionDetailView> details)
        {
            InitializeComponent();

            detailsGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Populate(details);
        }

        private void Populate(IEnumerable<TransactionDetailView> details)
        {
            detailsGridView.DataSource = details.Select(d => new { PaidFor = d.PaidFor, Article = d.Article, EffectivePrice = d.EffectivePrice}).ToList();
        }
    }
}
