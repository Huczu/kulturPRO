using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Services;
using TrackerEnabledDbContext.Common.Models;

namespace KulturPRO.ViewModels
{
    public class AuditDetailsViewModel
    {
        private AuditService _auditService;

        private AuditLog _auditLog;
        public AuditLog AuditLog
        {
            get
            {
                return _auditLog;
            }
        }

        public AuditDetailsViewModel(long _id)
        {
            _auditService = new AuditService();

            _auditLog = _auditService.GetAuditLogForId(_id);

            return;
        }
    }
}
