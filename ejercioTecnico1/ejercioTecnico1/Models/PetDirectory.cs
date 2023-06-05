using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ejercioTecnico1.Models

{


    public partial class PetDirectory
    {
        [JsonProperty("count")]
        public long Count { get; set; }

        [JsonProperty("entries")]
        public Entry[] Entries { get; set; }
    }

    public partial class Entry
    {
        [JsonProperty("API")]
        public string Api { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("Auth")]
        public string Auth { get; set; }

        [JsonProperty("HTTPS")]
        public bool Https { get; set; }

        [JsonProperty("Cors")]
        public Cors Cors { get; set; }

        [JsonProperty("Link")]
        public Uri Link { get; set; }

        [JsonProperty("Category")]
        public string Category { get; set; }
    }

    public enum Auth { ApiKey, Empty, OAuth, UserAgent, XMashapeKey };

    public enum Cors { No, Unknown, Unkown, Yes };

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                AuthConverter.Singleton,
                CorsConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class AuthConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Auth) || t == typeof(Auth?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "":
                    return Auth.Empty;
                case "OAuth":
                    return Auth.OAuth;
                case "User-Agent":
                    return Auth.UserAgent;
                case "X-Mashape-Key":
                    return Auth.XMashapeKey;
                case "apiKey":
                    return Auth.ApiKey;
            }
            throw new Exception("Cannot unmarshal type Auth");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Auth)untypedValue;
            switch (value)
            {
                case Auth.Empty:
                    serializer.Serialize(writer, "");
                    return;
                case Auth.OAuth:
                    serializer.Serialize(writer, "OAuth");
                    return;
                case Auth.UserAgent:
                    serializer.Serialize(writer, "User-Agent");
                    return;
                case Auth.XMashapeKey:
                    serializer.Serialize(writer, "X-Mashape-Key");
                    return;
                case Auth.ApiKey:
                    serializer.Serialize(writer, "apiKey");
                    return;
            }
            throw new Exception("Cannot marshal type Auth");
        }

        public static readonly AuthConverter Singleton = new AuthConverter();
    }

    internal class CorsConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Cors) || t == typeof(Cors?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "no":
                    return Cors.No;
                case "unknown":
                    return Cors.Unknown;
                case "unkown":
                    return Cors.Unkown;
                case "yes":
                    return Cors.Yes;
            }
            throw new Exception("Cannot unmarshal type Cors");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Cors)untypedValue;
            switch (value)
            {
                case Cors.No:
                    serializer.Serialize(writer, "no");
                    return;
                case Cors.Unknown:
                    serializer.Serialize(writer, "unknown");
                    return;
                case Cors.Unkown:
                    serializer.Serialize(writer, "unkown");
                    return;
                case Cors.Yes:
                    serializer.Serialize(writer, "yes");
                    return;
            }
            throw new Exception("Cannot marshal type Cors");
        }

        public static readonly CorsConverter Singleton = new CorsConverter();
    }
}