namespace Api_ComminityCares.Models
{
    public class BookStoreDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string BooksCollectionName { get; set; } = null!;
        public string DonorCollectionName { get; set; } = null!;
        public string PersonCollectionName { get; set; } = null!;
        public string UserCollectionName { get; set; } = null!;

    }
}
