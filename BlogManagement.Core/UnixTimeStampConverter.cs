using System;
using BlogManagement.Core.Extend;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BlogManagement.Core
{
    public class UnixTimeStampConverter : DateTimeConverterBase
    {
        internal static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        /// 读
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="objectType"></param>
        /// <param name="existingValue"></param>
        /// <param name="serializer"></param>
        /// <returns></returns>
        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            if (reader.Value.IsNullOrEmpty())
            {
                return UnixEpoch;
            }
            else
            {
                if (reader.TokenType == JsonToken.Date)
                {
                    return reader.Value;
                }
                else if (reader.TokenType == JsonToken.String)
                {
                    return DateTime.Parse(reader.Value.ToString());
                }
                else
                {

                    return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(Convert.ToInt64(reader.Value)).ToLocalTime();
                }
            }
        }
        /// <summary>
        /// 写
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="serializer"></param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            long seconds;
            if (value is DateTime dateTime)
            {
                seconds = (long)(dateTime.ToUniversalTime() - UnixEpoch).TotalMilliseconds;
            }
            else
            {
                throw new JsonSerializationException("Expected date object value.");
            }
            if (seconds < 0)
            {
                //throw new JsonSerializationException("Cannot convert date value that is before Unix epoch of 00:00:00 UTC on 1 January 1970.");
            }
            writer.WriteValue(seconds);
        }
    }

    public class UnixDateTimeConverter : DateTimeConverterBase
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType != JsonToken.Integer)
            {
                throw new Exception(String.Format("日期格式错误,got {0}.", reader.TokenType));
            }
            var ticks = (long)reader.Value;
            var date = new DateTime(1970, 1, 1);
            date = date.AddSeconds(ticks);
            return date;
        }
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            long ticks;
            if (value is DateTime)
            {
                var epoc = new DateTime(1970, 1, 1);
                var delta = ((DateTime)value) - epoc;
                if (delta.TotalSeconds < 0)
                {
                    throw new ArgumentOutOfRangeException("时间格式错误.1");
                }
                ticks = (long)delta.TotalSeconds;
            }
            else
            {
                throw new Exception("时间格式错误.2");
            }
            writer.WriteValue(ticks);
        }
    }
}
