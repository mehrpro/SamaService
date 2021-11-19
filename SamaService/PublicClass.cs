namespace SamaService
{
    public static class PublicClass
    {
        public static bool Send10000 { get; set; }
        public static bool Send5000 { get; set; }
        public static bool Send1200 { get; set; }
        public static bool Between(this int num, int lower, int upper, bool inclusive = false)
        {
            return inclusive
                ? lower <= num && num <= upper
                : lower < num && num < upper;
        }

        public static int LastCredit { get; set; }
    }
}