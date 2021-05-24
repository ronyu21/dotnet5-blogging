namespace Infrastructure.Entities
{
    public class Post : BaseEntity
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public long? BlogId { get; set; }

        public Blog Blog { get; set; }
    }
}