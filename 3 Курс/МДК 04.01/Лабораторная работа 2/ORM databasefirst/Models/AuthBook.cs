using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM_databasefirst.Models
{

    public partial class AuthBook
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public int CountPage { get; set; }

        public double? Price { get; set; }

        public virtual ICollection<Auth> Auths { get; set; } = new List<Auth>();
    }

}
