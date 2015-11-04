using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Services;
using Database.Models;
using KulturPRO.Models;
using TrackerEnabledDbContext.Common.Models;
using AutoMapper;

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

        private List<AuditDetails> _auditDetails;
        public List<AuditDetails> AuditDetails
        {
            get
            {
                return _auditDetails;
            }
        }

        public AuditDetailsViewModel(long _id)
        {
            _auditService = new AuditService();

            _auditLog = _auditService.GetAuditLogForId(_id);
            _auditDetails = Mapper.Map<List<AuditDetails>>(_auditLog.LogDetails);
        }
    }
}
