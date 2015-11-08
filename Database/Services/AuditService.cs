using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Models;
using TrackerEnabledDbContext.Common.Models;

namespace Database.Services
{
    public class AuditService
    {
        public ICollection<AuditLog> GetLogsPagination(int size, int index)
        {
            using (var context = new DatabaseContext())
            {
                var logs = context.AuditLog.OrderBy(a => a.EventDateUTC).Skip(size*(index - 1)).Take(size).ToList();

                return logs;
            }
        }

        public AuditLog GetAuditLogForId(long id)
        {
            using (var context = new DatabaseContext())
            {
                var auditLog = context.AuditLog.Include(a => a.LogDetails).FirstOrDefault(a => a.AuditLogId.Equals(id));

                return auditLog;
            }
        }

        public int GetTotalLogCount()
        {
            using (var context = new DatabaseContext())
            {
                return context.AuditLog.Count();
            }
        }
    }
}
