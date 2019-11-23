namespace UrbanNoise.Importer.Components.Shared.Settings
{
    public class AppSettings
    {
        public CosmosDb CosmosDb { get; set; }
        public BcnConnectaApi BcnConnectaApi { get; set; }
    }

    public class CosmosDb
    {
        public string ConnectionString { get; set; }
        public string Collection { get; set; }
        public string Database { get; set; }
    }

    public class BcnConnectaApi
    {
        public string BaseUri { get; set; }
        public string SensorType { get; set; }
    }
}
