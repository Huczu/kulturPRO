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
using System.Windows.Input;
using Database.Services;
using TrackerEnabledDbContext.Common.Models;

namespace KulturPRO.ViewModels
{
    public class AuditListViewModel : INotifyPropertyChanged
    {
        private readonly AuditService _auditService;

        private List<AuditLog> _auditLogs;

        private int _pageSize;
        public int PageSize 
        {
            get { return _pageSize; }
            set
            {
                _pageSize = value;
                UpdateAuditTable();
            }
        }

        public int PageIndex { get; set; }

        public int Count { get; set; }

        public List<AuditLog> AuditLogs
        {
            get { return _auditLogs; }
            set
            {
                _auditLogs = value;
                OnPropertyChanged("AuditLogs");
            }
        }

        public AuditListViewModel()
        {
            //init service
            _auditService = new AuditService();

            PageIndex = 1;
            PageSize = 50;
            UpdateAuditTable();

            ChangedIndexCommand = new RelayCommand(r => UpdateAuditTable());
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public ICommand ChangedIndexCommand { get; set; }

        public async void UpdateAuditTable()
        {
            Count = await _auditService.GetTotalLogCount();
            AuditLogs = new List<AuditLog>(await _auditService.GetLogsPagination(PageSize, PageIndex));
        }
    }
}
