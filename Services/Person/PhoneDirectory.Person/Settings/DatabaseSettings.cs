namespace PhoneDirectory.Person.Settings
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string PersonCollectionName { get; set; }
        public string ContactInfoCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
