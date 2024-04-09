namespace FinalAPI.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public int AvailableItems { get; set; }


        public Product(int id, string name, string description, float price, int availableItems)
        {
            this.Id = id;
            this.Name = name;
            this.Description = description;
            this.Price = price;
            this.AvailableItems = availableItems;
        }
    }
}
