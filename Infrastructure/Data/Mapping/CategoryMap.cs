using System.Data.Entity.ModelConfiguration;
using Infrastructure.Entity.Models;

namespace Infrastructure.Data.Mapping
{
    public class CategoryMap : EntityTypeConfiguration<Category>
    {
        public CategoryMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Category");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.ControllerName).HasColumnName("ControllerName");
            this.Property(t => t.ActionName).HasColumnName("ActionName");
        }
    }
}
