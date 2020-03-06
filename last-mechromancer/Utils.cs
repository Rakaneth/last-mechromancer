using System;
using YamlDotNet.Core;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using System.IO;

namespace last_mechromancer {
    public static class Utils {
        public static T Clamp<T>(T val, T low, T high) where T : IComparable<T>, IEquatable<T>  {
            if (val.CompareTo(low) < 0)
                return low;
            else if (val.CompareTo(high) > 0)
                return high;
            else
                return val;
        }

        public static bool Between<T>(T val, T low, T high) where T : IComparable<T>, IEquatable<T> {
            return Clamp(val, low, high).Equals(val);
        }

        public static string Decorate(string text, string fg="default", string bg="default") {
            return $"[c:r f:{fg}][c:r b:{bg}]{text}[c:undo][c:undo]";
        }

        public static TOutput ParseYaml<TOutput>(string filename) {
            using (var input = File.OpenText(filename))
            {
                var deser = new DeserializerBuilder()
                    .WithNamingConvention(CamelCaseNamingConvention.Instance)
                    .Build();
                return deser.Deserialize<TOutput>(new MergingParser(new Parser(input)));
            }
        }

        public static void LogError(string topic, string err) {
            System.Console.Error.WriteLine($"[{topic}]: {err}");
        }

        public static void LogInfo(string topic, string txt) {
            System.Console.WriteLine($"[{topic}]: {txt}");
        }
    }
}