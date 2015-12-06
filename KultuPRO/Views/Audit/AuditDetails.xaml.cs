using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using KulturPRO.ViewModels;

namespace KulturPRO.Views.Audit
{
    /// <summary>
    /// Interaction logic for AuditDetails.xaml
    /// </summary>
    public partial class AuditDetails : Window
    {
        public AuditDetails()
        {
            InitializeComponent();
        }

        public AuditDetails(long auditLogId)
        {
            this.DataContext = new AuditDetailsViewModel(auditLogId);

            InitializeComponent();
        }
    }
}
