using Microsoft.EntityFrameworkCore;
using NLayer.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository
{
    public class AppDbContext:DbContext
    {

        public AppDbContext(DbContextOptions <AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductFeature> ProductFeatures { get; set; }


        // Aşşağıda yazan kodu açıklamadan önce oluşturduğumuz configurationlar nedir bunları açıklayalım.
        // Configurations dosyasında Entitylerimize Anotasyon olarak verebilceğimiz değerleri tek bir yerden ve daha detaylı yönetmek için oluşturduğumuz yapılardır.
        // Bu oluşturduğumuz configurations yapılarını EntityFrameworkCore'a tanıtmak için yapmamız gereken 
        // Aşşağıdaki gibi model oluşturulurken IEntityTypeConfigurations interfacesine sahip sınıflarımızı tarar ve EFCore ile iletişime geçerek uygular.
        // Assembly = projenin kendisi desek yanlış olmaz gördüğümüz herşey bir assembly tahminim assembly seviyesinde tarama yapıyor. This is my prediction.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.Entity<ProductFeature>().HasData(
            new ProductFeature
            {
                Id = 1,
                Color = "Kırmızı",
                Height = 100,
                Width = 200,
                ProductId = 1,

            },
            new ProductFeature
            {
                Id = 2,
                Color = "Kırmızı",
                Height = 300,
                Width = 400,
                ProductId = 2,

            });


            base.OnModelCreating(modelBuilder);
        }



    }
}
