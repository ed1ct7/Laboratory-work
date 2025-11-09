using ORM_Individual.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM_Individual.ViewModels.TableViewModels
{
    public class Component_VM : Base_VM
    {
        public int Id { get; set; }

        public int? TypeId { get; set; }

        public string? Brand { get; set; }

        public string? ManufacturerCompany { get; set; }

        public string? ManufacturerCountry { get; set; }

        public string? ReleaseDate { get; set; }

        public string? Specifications { get; set; }

        public int? Warranty { get; set; }

        public string? Description { get; set; }

        public decimal? Price { get; set; }

        public virtual ICollection<Order> OrderComponent1s { get; set; } = new List<Order>();

        public virtual ICollection<Order> OrderComponent2s { get; set; } = new List<Order>();

        public virtual ICollection<Order> OrderComponent3s { get; set; } = new List<Order>();

        public virtual ComponentType? Type { get; set; }
    }
}
