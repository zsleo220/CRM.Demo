using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Demo.App
{
        public class DatabaseManagement
        {
            private readonly AppDbContext _context;
            public DatabaseManagement(AppDbContext context)
            {
                _context = context;
            }

            async public Task InitDatabase()
            {
                await _context.Database.EnsureCreatedAsync();
            }
        }

}
