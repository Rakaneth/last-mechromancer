using System;

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
    }
}