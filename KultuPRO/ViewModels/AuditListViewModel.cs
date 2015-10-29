using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;
using Database.Services;
using TrackerEnabledDbContext.Common.Models;

namespace KulturPRO.ViewModels
{
    public class AuditListViewModel : INotifyPropertyChanged
    {
        private readonly AuditService _auditService;

        private List<AuditLog> _auditLogs;

        private int _limit;
        public int Limit
        {
            get {return _limit;}
            set
            {
                _limit = value;
                OnPropertyChanged("Limit");
            }
        }

        private int _offset;
        public int Offset
        {
            get { return _offset; }
            set
            {
                _offset = value;
                OnPropertyChanged("Offset");
            }
        }

        public List<AuditLog> AuditLogs
        {
            get { return _auditLogs; }
        }

        public AuditListViewModel()
        {
            //init service
            _auditService = new AuditService();

            //init limit and offset
            Offset = 0;
            Limit = 50;

            _auditLogs = new List<AuditLog>(_auditService.GetLogsPagination(_limit, _offset));
            OnPropertyChanged("AuditLogs");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
