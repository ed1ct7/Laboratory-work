using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;

namespace SQLiteApp
{
    public class ApplicationContext : DbContext
    {
        private static ApplicationContext _context;
        public DbSet<User> Users { get; set; } = null;
        public static ApplicationContext GetContext()
        {
            if( _context == null )
                _context = new ApplicationContext();
            return _context;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=C:\Secret\Fork\ORM\ORM\test.db");
        }
    }
}
