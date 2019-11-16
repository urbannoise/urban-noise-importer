using System;
using System.Collections.Generic;
using System.Text;

namespace UrbanNoise.Importer.Components.Shared.Settings
{
    public class AppSettings
    {
        public Database Database { get; set; }
        public BcnConnectaApi BcnConnectaApi { get; set; }
    }

    public class Database
    {
        public string ConnectionString { get; set; }
        public string Collection { get; set; }
    }

    public class BcnConnectaApi
    {
        public string BaseUri { get; set; }
    }
}
