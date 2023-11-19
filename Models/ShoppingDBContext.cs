using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace _3_Asp.Net_MVC.Models
{
    public class ShoppingDBContext : DbContext
    {
        public ShoppingDBContext()
        {

        }
        public ShoppingDBContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<BillDetail> BillDetails { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<CartDetail> CartDetails { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=LAPTOP-G189FU38\SQLEXPRESS;Initial Catalog=DAThucTap;Integrated Security=True");
        }
        private void SeedData(ModelBuilder modelBuilder)
        {
            // Add your seed data here
            modelBuilder.Entity<Category>().HasData(
                new Category { ID = Guid.Parse("9d47c1cb-c7d9-42c5-8654-4585f2f796e2"), Name = "Áo", Description = "Áo", Status = 1 },
                new Category { ID = Guid.Parse("d9275424-8cf1-4f51-8bc4-fb08112d99cb"), Name = "Quần ", Description = "Quần", Status = 1 }
            );
            modelBuilder.Entity<Role>().HasData(
                new Role { ID = Guid.Parse("7a715fa6-026b-4fb2-9d2b-ad76cbe421b8"), RoleName = "Admin", Description = "Admin", Status = 1 },
                new Role { ID = Guid.Parse("9d47c1cb-c7d9-42c5-8654-4585f2f673e0"), RoleName = "Khách hàng", Description = "Khách mua hàng dùng tài khoản", Status = 1 },
                new Role { ID = Guid.Parse("c2a00f0f-263f-4f15-b0de-3924fd81c451"), RoleName = "Khách vãng lai", Description = "Khách mua hàng không có tài khoản", Status = 1 }
            );
            modelBuilder.Entity<User>().HasData(
                new User { ID = Guid.Parse("5fc96e34-ea45-40b0-b655-f044e773771d"), UserName = "Admin123", Email = "admin123@gmail.com", Password = "Admin123", RoleID = Guid.Parse("7a715fa6-026b-4fb2-9d2b-ad76cbe421b8"), Status = 1 },
                new User { ID = Guid.Parse("d76b97a6-9e9c-460e-8930-69fffea6a1e8"), UserName = "Customer123", Email = "customer123@gmail.com", Password = "Customer123", RoleID = Guid.Parse("9d47c1cb-c7d9-42c5-8654-4585f2f673e0"), Status = 1 },
                new User { ID = Guid.Parse("1b49d058-582a-4f3a-ae38-2f715bf75156"), Email = null ,RoleID = Guid.Parse("c2a00f0f-263f-4f15-b0de-3924fd81c451"), Status = 1 }
            ); ; ;
            modelBuilder.Entity<Product>().HasData(
                new Product { ID = Guid.Parse("fa31a855-ced2-4574-a720-154fe3380f63"), ProductName = "Áo polo", Image = "product-8.jpg", Size = "S", Price = 300000, AvailableQuantity = 45, TypeName = Guid.Parse("9d47c1cb-c7d9-42c5-8654-4585f2f796e2"), Description = "Áo polo lịch sự", Status = 1 },
                 new Product { ID = Guid.Parse("b3c41a23-2b05-4101-81fe-36eeac9a2695"), ProductName = "Áo khoác kaki", Image = "product-2.jpg", Size = "L", Price = 450000, AvailableQuantity = 89, TypeName = Guid.Parse("9d47c1cb-c7d9-42c5-8654-4585f2f796e2"), Description = "Áo kaki ấm có cổ, khóa kéo", Status = 1 },
                 new Product { ID = Guid.Parse("d4648b5a-cce8-465a-a83b-4523e2b1fe86"), ProductName = "Áo phông họa tiết trừu tượng", Image = "product-5.jpg", Size = "S", Price = 250000, AvailableQuantity = 59, TypeName = Guid.Parse("9d47c1cb-c7d9-42c5-8654-4585f2f796e2"), Description = "Áo phông mát mẻ, co dãn 4 chiều", Status = 1 },
                 new Product { ID = Guid.Parse("48990401-d56c-4272-9694-bd672f7484c2"), ProductName = "Áo polo kẻ rộng", Image = "product-15.jpg", Size = "M", Price = 350000, AvailableQuantity = 78, TypeName = Guid.Parse("9d47c1cb-c7d9-42c5-8654-4585f2f796e2"), Description = "Áo polo mát mẻ, co dãn 4 chiều", Status = 1 },
                 new Product { ID = Guid.Parse("d5ac8261-5791-403b-b7ba-27d9ca0d7b04"), ProductName = "Áo khoác kaki họa tiết", Image = "product-12.jpg", Size = "M", Price = 650000, AvailableQuantity = 59, TypeName = Guid.Parse("9d47c1cb-c7d9-42c5-8654-4585f2f796e2"), Description = "Áo kaki ấm có cổ, khóa kéo", Status = 1 },
                 new Product { ID = Guid.Parse("0b4eada7-5c76-4561-a680-f3933f3482d5"), ProductName = "Áo khoác kaki họa tiết 1", Image = "product-4.jpg", Size = "M", Price = 650000, AvailableQuantity = 59, TypeName = Guid.Parse("9d47c1cb-c7d9-42c5-8654-4585f2f796e2"), Description = "Áo kaki ấm có cổ, khóa kéo", Status = 1 }
            );
            modelBuilder.Entity<CartDetail>().HasData(
                new CartDetail { ID = Guid.Parse("5fc96e34-ea45-40b0-b655-f044e773771d"), UserID = Guid.Parse("d76b97a6-9e9c-460e-8930-69fffea6a1e8"), ProductID = Guid.Parse("48990401-d56c-4272-9694-bd672f7484c2"), Quantity = 1 },
                new CartDetail { ID = Guid.Parse("1b76af66-0132-451d-8c2c-284b3ebfafc6"), UserID = Guid.Parse("d76b97a6-9e9c-460e-8930-69fffea6a1e8"), ProductID = Guid.Parse("0b4eada7-5c76-4561-a680-f3933f3482d5"), Quantity = 2 }
            );
            modelBuilder.Entity<Bill>().HasData(
                new Bill { ID = Guid.Parse("0c522152-5a19-42d8-84ce-76514ce25ae8"), UserID = Guid.Parse("d76b97a6-9e9c-460e-8930-69fffea6a1e8"), CreateDate = DateTime.Now, Status = 0, Recipient = "Nguyễn Văn A", Address = "Hà Nội", PhoneNumber = "0943665884" },
                new Bill { ID = Guid.Parse("ada30208-3268-47a7-aa35-d59486ebb6b2"), UserID = Guid.Parse("d76b97a6-9e9c-460e-8930-69fffea6a1e8"), CreateDate = DateTime.Now, Status = 0, Recipient = "Nguyễn Văn A", Address = "Hà Nội", PhoneNumber = "0998568984" },
                new Bill { ID = Guid.Parse("bf80ab36-d28f-472a-98ab-e1b7d2d79d5e"), UserID = Guid.Parse("1b49d058-582a-4f3a-ae38-2f715bf75156"), CreateDate = DateTime.Now, Status = 0, Recipient = "Nguyễn Văn A", Address = "Hà Nội", PhoneNumber = "0345278984" },
                new Bill { ID = Guid.Parse("05be279c-15db-435c-9805-a578222b46c5"), UserID = Guid.Parse("1b49d058-582a-4f3a-ae38-2f715bf75156"), CreateDate = DateTime.Now, Status = 1, Recipient = "Nguyễn Văn A", Address = "Hà Nội", PhoneNumber = "0879875384" }
            );
            modelBuilder.Entity<BillDetail>().HasData(
                new BillDetail { ID = Guid.Parse("64c1a8aa-2339-487f-ac07-d09423781bf6"), BillID = Guid.Parse("0c522152-5a19-42d8-84ce-76514ce25ae8"), ProductID = Guid.Parse("0b4eada7-5c76-4561-a680-f3933f3482d5"), Quantity = 2, Price = 300000 },
                new BillDetail { ID = Guid.Parse("b5e25ec7-d760-46f5-9578-d8322e437f32"), BillID = Guid.Parse("0c522152-5a19-42d8-84ce-76514ce25ae8"), ProductID = Guid.Parse("fa31a855-ced2-4574-a720-154fe3380f63"), Quantity = 1, Price = 450000 },
                new BillDetail { ID = Guid.Parse("4efe695d-a602-4cf7-b50c-4c0da4c84524"), BillID = Guid.Parse("ada30208-3268-47a7-aa35-d59486ebb6b2"), ProductID = Guid.Parse("d4648b5a-cce8-465a-a83b-4523e2b1fe86"), Quantity = 1, Price = 650000 },
                new BillDetail { ID = Guid.Parse("5ef2af34-55d5-4cdf-89d8-283c87df13f2"), BillID = Guid.Parse("bf80ab36-d28f-472a-98ab-e1b7d2d79d5e"), ProductID = Guid.Parse("fa31a855-ced2-4574-a720-154fe3380f63"), Quantity = 2, Price = 350000 },
                new BillDetail { ID = Guid.Parse("7e9d8ae1-321b-414d-a157-c638a061749c"), BillID = Guid.Parse("bf80ab36-d28f-472a-98ab-e1b7d2d79d5e"), ProductID = Guid.Parse("b3c41a23-2b05-4101-81fe-36eeac9a2695"), Quantity = 2, Price = 650000 },
                new BillDetail { ID = Guid.Parse("f85f7fec-df10-4f43-9fec-e9c3726707b0"), BillID = Guid.Parse("05be279c-15db-435c-9805-a578222b46c5"), ProductID = Guid.Parse("d4648b5a-cce8-465a-a83b-4523e2b1fe86"), Quantity = 2, Price = 350000 }
            );
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            SeedData(modelBuilder);
        }

    }
}
