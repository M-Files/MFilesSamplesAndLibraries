using MFiles.VAF.Configuration;

using MultiLingualConfiguration.Properties;

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MultiLingualConfiguration
{
    /// <summary>
    /// Sample configuration entry for demonstrating multi-lingual settings
    /// </summary>
    [DataContract]
    public class Configuration
    {
        [DataMember]
        [JsonConfEditor(
            Label = ResourceMarker.Suffix + nameof(Resources.Label_Configuration_Enabled),
            DefaultValue = true,
            HelpTextResourceId = nameof(Resources.HelpText_Configuration_Enabled))]
        public bool Enabled { get; set; } = true;

        [DataMember]
        [JsonConfEditor(
            Label = ResourceMarker.Suffix + nameof(Resources.Label_Selections),
            HelpTextResourceId = nameof(Resources.HelpText_Selections),
            ChildName = ResourceMarker.Suffix + nameof(Resources.ChildName_Selection),
            NameMember = nameof(SelectionOption.SelectedOption))]
        public List<SelectionOption> Selections { get; set; } = new List<SelectionOption>();
    }

    /// <summary>
    /// Sub class for testing name member and child name
    /// </summary>
    [DataContract]
    public class SelectionOption
    {
        /// <summary>
        /// Enum Selection
        /// </summary>
        [DataMember]
        [JsonConfEditor(
            Label = ResourceMarker.Suffix + nameof(Resources.Label_SelectedOption),
            HelpTextResourceId = nameof(Resources.HelpText_SelectedOption))]
        public SelectionOptions SelectedOption { get; set; }
    }

    /// <summary>
    /// Some enum entries just for demonstrating
    /// </summary>
    public enum SelectionOptions
    {
        [JsonConfEditor(
            Label = ResourceMarker.Suffix + nameof(Resources.Label_SelectionOptions_First),
            HelpTextResourceId = nameof(Resources.HelpText_SelectionOptions_First))]
        First,

        [JsonConfEditor(
            Label = ResourceMarker.Suffix + nameof(Resources.Label_SelectionOptions_Second),
            HelpTextResourceId = nameof(Resources.HelpText_SelectionOptions_Second))]
        Second,

        [JsonConfEditor(
            Label = ResourceMarker.Suffix + nameof(Resources.Label_SelectionOptions_Third),
            HelpTextResourceId = nameof(Resources.HelpText_SelectionOptions_Third))]
        Third
    }
}