using Microsoft.EntityFrameworkCore;
using SoftwareManagement.Application.DTOs.Request.AuditEntry;
using SoftwareManagement.Application.Enums;
using SoftwareManagement.Domain.Entities.AuditLogs;
using SoftwareManagement.Domain.Entities.Base;
using SoftwareManagement.Domain.Entities.Tenants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareManagement.Infrastructure.DbContexts
{
    public class TenantDbContext : DbContext
    {
        public TenantDbContext(DbContextOptions<TenantDbContext> options)
           : base(options)
        {

        }
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<AuditLogs> TenantAuditLogs { get; set; }

        public virtual async Task<int> SaveChangesAsync(string userId = null)
        {
            OnBeforeSaveChanges(userId);
            var result = await base.SaveChangesAsync();
            return result;
        }
        private void OnBeforeSaveChanges(string userId)
        {
            ChangeTracker.DetectChanges();
            var auditEntries = new List<AuditLogEntry>();
            foreach (var entry in ChangeTracker.Entries())
            {

                if (entry.Entity is AuditEntity && (entry.State == EntityState.Added || entry.State == EntityState.Modified))
                {
                    if (entry.State == EntityState.Added)
                    {
                        ((AuditEntity)entry.Entity).CreatedDate = DateTime.UtcNow;
                        ((AuditEntity)entry.Entity).CreatedBy = userId;
                        ((AuditEntity)entry.Entity).IsDeleted = false;
                        ((AuditEntity)entry.Entity).IsActive = true;
                    }
                    if (entry.State == EntityState.Modified)
                    {
                        ((AuditEntity)entry.Entity).UpdatedDate = DateTime.UtcNow;
                        ((AuditEntity)entry.Entity).UpdatedBy = userId;
                    }
                }

                if (entry.Entity is AuditLogs || entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                    continue;
                var auditEntry = new AuditLogEntry(entry);
                auditEntry.TableName = entry.Entity.GetType().Name;
                auditEntry.UserId = userId;
                auditEntries.Add(auditEntry);
                foreach (var property in entry.Properties)
                {
                    string propertyName = property.Metadata.Name;
                    if (property.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValues[propertyName] = property.CurrentValue;
                        continue;
                    }
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            auditEntry.AuditType = AuditType.Create;
                            auditEntry.NewValues[propertyName] = property.CurrentValue;
                            break;
                        case EntityState.Deleted:
                            auditEntry.AuditType = AuditType.Delete;
                            auditEntry.OldValues[propertyName] = property.OriginalValue;
                            break;
                        case EntityState.Modified:
                            if (property.IsModified)
                            {
                                auditEntry.ChangedColumns.Add(propertyName);
                                auditEntry.AuditType = AuditType.Update;
                                auditEntry.OldValues[propertyName] = property.OriginalValue;
                                auditEntry.NewValues[propertyName] = property.CurrentValue;
                            }
                            break;
                    }
                }
            }
            foreach (var auditEntry in auditEntries)
            {
                TenantAuditLogs.Add(auditEntry.ToAudit());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Tenant>();
            modelBuilder.Entity<AuditLogs>();
        }
    }
}


