using Microsoft.EntityFrameworkCore;
using BelediyeTicketAPI.Models;

namespace BelediyeTicketAPI.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // ==========================
        // DEPARTMENTS
        // ==========================
        modelBuilder.Entity<Department>().HasData(

            new Department
            {
                Id = 1,
                Name = "Bilgi İşlem"
            },

            new Department
            {
                Id = 2,
                Name = "Fen İşleri"
            },

            new Department
            {
                Id = 3,
                Name = "Mali Hizmetler"
            },

            new Department
            {
                Id = 4,
                Name = "İnsan Kaynakları"
            },

            new Department
            {
                Id = 5,
                Name = "Destek Hizmetleri"
            }

        );

        // ==========================
        // USERS
        // ==========================
        modelBuilder.Entity<User>().HasData(

            new User
            {
                Id = 1,
                FirstName = "Ahmet",
                LastName = "Yılmaz",
                Email = "ahmet@belediye.com",
                PasswordHash = "123456",
                Role = "Admin",
                DepartmentId = 1
            },

            new User
            {
                Id = 2,
                FirstName = "Ayşe",
                LastName = "Demir",
                Email = "ayse@belediye.com",
                PasswordHash = "123456",
                Role = "Personel",
                DepartmentId = 3
            },

            new User
            {
                Id = 3,
                FirstName = "Mehmet",
                LastName = "Kaya",
                Email = "mehmet@belediye.com",
                PasswordHash = "123456",
                Role = "Personel",
                DepartmentId = 5
            }

        );

        // ==========================
        // CATEGORIES
        // ==========================
        modelBuilder.Entity<Category>().HasData(

            new Category
            {
                Id = 1,
                Name = "Bilgi İşlem"
            },

            new Category
            {
                Id = 2,
                Name = "Fen İşleri"
            },

            new Category
            {
                Id = 3,
                Name = "Mali Hizmetler"
            },

            new Category
            {
                Id = 4,
                Name = "İnsan Kaynakları"
            },

            new Category
            {
                Id = 5,
                Name = "Destek Hizmetleri"
            },

            new Category
            {
                Id = 6,
                Name = "Park ve Bahçeler"
            },

            new Category
            {
                Id = 7,
                Name = "Temizlik İşleri"
            }

        );
    // ==========================
    // TICKETS
    // ==========================
        modelBuilder.Entity<Ticket>().HasData(

             new Ticket
    {
        Id = 1,
        Title = "Yazıcı çalışmıyor",
        Description = "HP LaserJet sürekli kağıt sıkıştırıyor.",
        Status = "Beklemede",
        CreatedDate = new DateTime(2026, 7, 20, 0, 0, 0, DateTimeKind.Utc),
        CategoryId = 1,
        UserId = 1
    },

    new Ticket
    {
        Id = 2,
        Title = "İnternet bağlantısı yok",
        Description = "Muhasebe biriminde internet kesildi.",
        Status = "İşlemde",
        CreatedDate = new DateTime(2026, 7, 21, 0, 0, 0, DateTimeKind.Utc),
        CategoryId = 3,
        UserId = 2
    },

    new Ticket
    {
        Id = 3,
        Title = "Klima çalışmıyor",
        Description = "Başkanlık katındaki klima arızalı.",
        Status = "Tamamlandı",
        CreatedDate = new DateTime(2026, 7, 22, 0, 0, 0, DateTimeKind.Utc),
        CategoryId = 5,
        UserId = 3
    }

     );
    }
}