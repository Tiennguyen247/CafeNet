using System;

namespace CyberCafeManager
{
    public static class SimulatedClock
    {
        private static DateTime _base = DateTime.Now;
        private static TimeSpan _offset = TimeSpan.Zero;

        public static DateTime Now => DateTime.Now + _offset;

        public static void AdvanceDay() => _offset += TimeSpan.FromDays(1);

        public static void Reset()
        {
            _base = DateTime.Now;
            _offset = TimeSpan.Zero;
        }

        public static bool IsSimulating => _offset != TimeSpan.Zero;

        public static string DisplayDate =>
            Now.ToString("dddd, dd/MM/yyyy")
            + (IsSimulating ? "  ⏱ [Mô phỏng]" : "");
    }
}