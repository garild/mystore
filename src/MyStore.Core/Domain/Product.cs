namespace MyStore.Core.Domain
{
    public class Product
    {
        public AggregateId Id { get; set; }
        public string Name { get; set; }
        public AggregateId CategoryId { get; set; }
        public decimal Price { get; set; }

        private Product()
        {
        }
        
        public Product(AggregateId id, string name, AggregateId categoryId, decimal price)
        {
            Id = id;
            Name = name;
            CategoryId = categoryId;
            Price = price;
        }
    }
}