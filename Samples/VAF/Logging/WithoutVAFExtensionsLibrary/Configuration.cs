using MFiles.VAF.Configuration;
using MFiles.VAF.Configuration.JsonAdaptor;
using MFiles.VaultApplications.Logging.NLog;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace WithoutVAFExtensionsLibrary
{
    [DataContract]
    public class Configuration
    {
        [DataMember]
        public NLogLoggingConfiguration LoggingConfiguration { get; set; } = new NLogLoggingConfiguration();
    }
}