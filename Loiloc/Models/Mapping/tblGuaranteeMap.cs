using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Loiloc.Models.Mapping
{
    public class tblGuaranteeMap : EntityTypeConfiguration<tblGuarantee>
    {
        public tblGuaranteeMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.Name)
                .HasMaxLength(200);

            this.Property(t => t.Description)
                .HasMaxLength(300);

            this.Property(t => t.Title)
                .HasMaxLength(200);

            this.Property(t => t.Keyword)
                .HasMaxLength(500);

            this.Property(t => t.Address)
                .HasMaxLength(200);

            this.Property(t => t.Mobile)
                .HasMaxLength(100);

            this.Property(t => t.Email)
                .HasMaxLength(200);

            this.Property(t => t.TimeWork)
                .HasMaxLength(100);

            this.Property(t => t.Tag)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("tblGuarantee");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.idManu).HasColumnName("idManu");
            this.Property(t => t.idDistrict).HasColumnName("idDistrict");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.Keyword).HasColumnName("Keyword");
            this.Property(t => t.Content).HasColumnName("Content");
            this.Property(t => t.Address).HasColumnName("Address");
            this.Property(t => t.Mobile).HasColumnName("Mobile");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.TimeWork).HasColumnName("TimeWork");
            this.Property(t => t.Tag).HasColumnName("Tag");
            this.Property(t => t.Active).HasColumnName("Active");
            this.Property(t => t.Ord).HasColumnName("Ord");
            this.Property(t => t.DateCreate).HasColumnName("DateCreate");
            this.Property(t => t.idUser).HasColumnName("idUser");
        }
    }
}
