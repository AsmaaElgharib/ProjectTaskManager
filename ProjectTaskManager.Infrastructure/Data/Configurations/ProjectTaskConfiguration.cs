using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectTaskManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTaskManager.Infrastructure.Data.Configurations
{
    public class ProjectTaskConfiguration : IEntityTypeConfiguration<ProjectTask>
    {
        public void Configure(EntityTypeBuilder<ProjectTask> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Title).IsRequired().HasMaxLength(300);
            builder.Property(t => t.Description).HasMaxLength(2000);
            builder.Property(t => t.Status).IsRequired();
            builder.Property(t => t.Priority).IsRequired();
            builder.Property(t => t.CreatedAt).IsRequired();
        }
    }
}
