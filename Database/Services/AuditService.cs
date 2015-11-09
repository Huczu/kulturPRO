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
        public async Task<ICollection<AuditLog>> GetLogsPagination(int size, int index)
        {
            using (var context = new DatabaseContext())
            {
                var logs = await context.AuditLog.OrderBy(a => a.EventDateUTC).Skip(size*(index - 1)).Take(size).ToListAsync();

                return logs;
            }
        }

        public async Task<AuditLog> GetAuditLogForId(long id)
        {
            using (var context = new DatabaseContext())
            {
                var auditLog = await context.AuditLog.Include(a => a.LogDetails).FirstOrDefaultAsync(a => a.AuditLogId.Equals(id));

                return auditLog;
            }
        }

        public async Task<int> GetTotalLogCount()
        {
            using (var context = new DatabaseContext())
            {
                return await context.AuditLog.CountAsync();
            }
        }
    }
}
