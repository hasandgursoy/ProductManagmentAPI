using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayer.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository.Configurations
{
    public class CategoryConfigurations : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(x => x.Id); // primary key olduğunu söylüyoruz.
            builder.Property(x => x.Id).UseIdentityColumn(); // birer birer artsın diyoruz. 
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.ToTable("Categories");

        }   
    }
}
