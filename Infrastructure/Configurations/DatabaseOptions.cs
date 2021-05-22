namespace Infrastructure.Configurations
{
    public class DatabaseOptions
    {
        public const string Key = "Database";

        public string Host { get; set; }

        public string Database { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string ConnectionString()
        {
            return $"Host={Host};Database={Database};Username={Username};Password={Password}";
        }
    }
}