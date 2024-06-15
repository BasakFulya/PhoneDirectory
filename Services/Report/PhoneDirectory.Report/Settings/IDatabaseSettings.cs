namespace PhoneDirectory.Report.Settings
{
    public interface IDatabaseSettings
    {
        public string ReportCollectionName { get; set; }
        public string PersonCollectionName { get; set; }
        public string ContactInfoCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
