
namespace BenzeneSoft.QBuild.Functions
{
    /// <summary>
    /// Implementations of methods in this class are not meaningful.
    /// These are only to be used as SQL function aliases
    /// </summary>
    public static class SqlFunctions
    {
        // Aggregate
        public static T Avg<T>(T val) => val;
        public static T Max<T>(T val) => val;
        public static T Min<T>(T val) => val;
        public static T Count<T>(T val) => val;
        public static T Sum<T>(T val) => val;

        // Numaric
        public static T Sqrt<T>(T val) => val;
        public static T Abs<T>(T val) => val;
        public static T Mod<T>(T val) => val;
        public static T Ceil<T>(T val) => val;
        public static T Floor<T>(T val) => val;
    }
}
