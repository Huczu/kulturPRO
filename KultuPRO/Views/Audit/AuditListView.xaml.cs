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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TrackerEnabledDbContext.Common.Models;

namespace KulturPRO.Views.Audit
{
    /// <summary>
    /// Interaction logic for AuditListView.xaml
    /// </summary>
    public partial class AuditListView : UserControl
    {
        public AuditListView()
        {
            InitializeComponent();
        }

        private void Row_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridRow row = ItemsControl.ContainerFromElement((DataGrid)sender, e.OriginalSource as DependencyObject) as DataGridRow;

            if (row != null)
            {
                var item = row.Item as AuditLog;

                AuditDetails auditDetails = new AuditDetails(item.AuditLogId);
                auditDetails.Show();
            }
        }
    }
}
