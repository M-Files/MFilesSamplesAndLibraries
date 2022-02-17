using MFiles.VAF.Configuration;
using MFiles.VAF.Configuration.JsonAdaptor;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace WithVAFExtensionsLibrary
{
    [DataContract]
    public class Configuration
        : MFiles.VAF.Extensions.Configuration.ConfigurationBase
    {
    }
}