﻿using ReportIt.Models;
using Microsoft.EntityFrameworkCore;

namespace ReportIt.Data
{
    public class AuditLogRepository : IAuditLogRepository
    {
        private readonly ReportItContext _context;

        public AuditLogRepository(ReportItContext context)
        {
            _context = context;
        }

        List<AuditLog> IAuditLogRepository.GetAll()
        {
            return _context.AuditLog.ToList();
        }

        AuditLog? IAuditLogRepository.GetById(int id)
        {
            var AuditLog = _context.AuditLog
                .FirstOrDefault(m => m.Id == id);
            return AuditLog;
        }

        AuditLog IAuditLogRepository.Create(AuditLog auditLog)
        {
            _context.Add(auditLog);
            _context.SaveChanges();
            return auditLog;
        }

        AuditLog IAuditLogRepository.Update(AuditLog auditLog)
        {
            _context.Update(auditLog);
            _context.SaveChanges();
            return auditLog;
        }

        AuditLog? IAuditLogRepository.DeleteById(int id)
        {
            var auditLog = _context.AuditLog
                .FirstOrDefault(m => m.Id == id);
            if (auditLog == null)
            {
                return null;
            }
            _context.AuditLog.Remove(auditLog);
            _context.SaveChanges();
            return auditLog;
        }
    }
}
