using ORM_Individual.Models.Entities;
using System.Data;

namespace ORM_Individual.Models.Repositories
{
    public class ServiceRepository : BaseRepository<Service>
    {
        public override Service CreateInstanceFromDataRow(DataRow row)
        {
            var service = new Service();

            if (!row.IsNull("id") && row["id"] != DBNull.Value) { service.Id = Convert.ToInt32(row["id"]); }
            else { service.Id = 0; }

            service.Name = row.IsNull("name") ? "" : row["name"].ToString();
            service.Description = row.IsNull("description") ? "" : row["description"].ToString();

            if (!row.IsNull("price") && row["price"] != DBNull.Value)
            {
                if (decimal.TryParse(row["price"].ToString(), out decimal priceValue)) { service.Price = priceValue; }
                else { service.Price = 0; }
            }
            else { service.Price = 0; }

            Console.WriteLine($"Created Service: ID={service.Id}, Name='{service.Name}', Price={service.Price}");

            return service;
        }
    }
}
    