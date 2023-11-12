using Microsoft.EntityFrameworkCore;

namespace Lab3
{
    public class ModelDB:DbContext
    {
        public ModelDB(DbContextOptions options) : base(options)
        {
           Database.EnsureCreated();
        }

        public DbSet<Rate> Rate { get; set; }
        public DbSet<Cassette> Cassette { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<Rate>().HasData(
                new Rate { Id = 1, Type = "Sony", Genre = "drama" },
                new Rate { Id = 2, Type = "JVC", Genre = "horror" },
                new Rate { Id = 3, Type = "Philips", Genre = "action" },
                new Rate { Id = 4, Type = "JVC", Genre = "horror" },
                new Rate { Id = 5, Type = "Sony", Genre = "documentary" },
                new Rate { Id = 6, Type = "JVC", Genre = "action" },
                new Rate { Id = 7, Type = "Philips", Genre = "тhriller" }
                );
            modelBuilder.Entity<Cassette>().HasData( //Sony  JVC  Philips
    new Cassette{ Id=1, Type = "Sony" , Genre = "Action" ,Film = "Film1", DateCassette = new DateTime(2023, 1, 5),
        FIO = "John Doe" ,Term = 40, TotalPrice = 1500
    },
    new Cassette{ Id=2, Type = "JVC" , Genre = "Comedy" ,Film = "Film2", DateCassette = new DateTime(2023, 2, 12),
        FIO = "Jane Doe" ,Term = 10, TotalPrice = 1800
    },
    new Cassette{ Id=3, Type = "Philips" , Genre = "Drama" ,Film = "Film3", DateCassette = new DateTime(2023, 3, 20),
        FIO = "Alice Smith" ,Term = 12, TotalPrice = 1200
    },
    new Cassette{ Id=4, Type = "Sony" , Genre = "Sci-Fi" ,Film = "Film4", DateCassette = new DateTime(2023, 4, 7),
        FIO = "Bob Johnson" ,Term = 24, TotalPrice = 2200
    },
    new Cassette{ Id=5, Type = "JVC" , Genre = "Horror" ,Film = "Film5", DateCassette = new DateTime(2023, 5, 15),
        FIO = "Charlie Brown" ,Term = 24, TotalPrice = 1800
    },
    new Cassette{ Id=6, Type = "Philips" , Genre = "Romance" ,Film = "Film6", DateCassette = new DateTime(2023, 6, 23),
        FIO = "John Doe" ,Term = 4, TotalPrice = 1950
    },
    new Cassette{ Id=7, Type = "Sony" , Genre = "Adventure" ,Film = "Film7", DateCassette = new DateTime(2023, 7, 1),
        FIO = "Jane Doe" ,Term = 54, TotalPrice = 1500
    },
    new Cassette{ Id=8, Type = "JVC" , Genre = "Thriller" ,Film = "Film8", DateCassette = new DateTime(2023, 8, 8),
        FIO = "Alice Smith" ,Term = 15, TotalPrice = 1800
    },
    new Cassette{ Id=9, Type = "Philips" , Genre = "Mystery" ,Film = "Film9", DateCassette = new DateTime(2023, 9, 16),
        FIO = "Bob Johnson" ,Term = 65, TotalPrice = 1200
    },
    new Cassette{ Id=10, Type = "Sony" , Genre = "Fantasy" ,Film = "Film10", DateCassette = new DateTime(2023, 10, 24),
        FIO = "Charlie Brown" ,Term = 22, TotalPrice = 2200
    },
    new Cassette{ Id=11, Type = "JVC" , Genre = "Action" ,Film = "Film1", DateCassette = new DateTime(2023, 11, 2),
        FIO = "John Doe" ,Term = 31, TotalPrice = 1800
    },
    new Cassette{ Id=12, Type = "Philips" , Genre = "Comedy" ,Film = "Film2", DateCassette = new DateTime(2023, 12, 10),
        FIO = "Jane Doe" ,Term = 11, TotalPrice = 1950
    },
    new Cassette{ Id=13, Type = "Sony" , Genre = "Drama" ,Film = "Film3", DateCassette = new DateTime(2023, 1, 18),
        FIO = "Alice Smith" ,Term = 10, TotalPrice = 1500
    },
    new Cassette{ Id=14, Type = "JVC" , Genre = "Sci-Fi" ,Film = "Film4", DateCassette = new DateTime(2023, 2, 26),
        FIO = "Bob Johnson" ,Term = 5, TotalPrice = 1800
    },
    new Cassette{ Id=15, Type = "Philips" , Genre = "Horror" ,Film = "Film5", DateCassette = new DateTime(2023, 3, 6),
        FIO = "Charlie Brown" ,Term = 7, TotalPrice = 1200
    }

                );
            modelBuilder.Entity<User>().HasData(
                new User{Id=1,Email="BimBam@gmail.com", Password="bimbam111" },
                new User{Id=2,Email="BamBim@gmail.com", Password="111bambim" }
                );
        }
    }
}
