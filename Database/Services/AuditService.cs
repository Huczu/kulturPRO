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
        public ICollection<AuditLog> GetLogsPagination(int limit, int offset)
        {
            using (var context = new DatabaseContext())
            {
                var logs = context.AuditLog.OrderBy(a => a.EventDateUTC).Skip(offset).Take(limit).ToList();

                return logs;
            }
        }
    }
}
