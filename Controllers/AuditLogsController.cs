﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReportIt.Data;
using ReportIt.Models;

namespace ReportIt.Controllers
{
    public class AuditLogsController : Controller
    {
        private readonly IAuditLogRepository _auditLogRepository;

        public AuditLogsController(IAuditLogRepository auditLogRepository)
        {
            _auditLogRepository = auditLogRepository;
        }

        // display all audit log entries
        public IActionResult Index()
        {
            return View(_auditLogRepository.GetAll());
        }

        // retrieve individual log details
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var auditLog = _auditLogRepository.GetById(id.Value);
            if (auditLog == null)
            {
                return NotFound();
            }

            return View(auditLog);
        }
        public IActionResult Visualization()
        {
            var auditLogs = _auditLogRepository.GetAll();
            return View(auditLogs);
        }

    }
}
