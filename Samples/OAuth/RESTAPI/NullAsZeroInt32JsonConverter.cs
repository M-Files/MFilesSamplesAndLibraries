using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MFaaP.MFWSClient
{
	/// <summary>
	/// Reads a JSON null as zero for an <see cref="int"/>, e.g. the <see cref="UploadInfo.UploadID"/> of a stateless upload,
	/// which MFWS returns as null.
	/// </summary>
	public class NullAsZeroInt32JsonConverter
		: JsonConverter<int>
	{
		/// <inheritdoc />
		public override int Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			if (reader.TokenType == JsonTokenType.Null)
				return 0;
			if (reader.TokenType == JsonTokenType.String)
				return int.Parse(reader.GetString(), System.Globalization.CultureInfo.InvariantCulture);
			return reader.GetInt32();
		}

		/// <inheritdoc />
		public override void Write(Utf8JsonWriter writer, int value, JsonSerializerOptions options)
		{
			writer.WriteNumberValue(value);
		}
	}
}
