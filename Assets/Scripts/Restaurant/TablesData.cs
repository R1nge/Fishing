using System;

namespace Restaurant
{
    public static class TablesData
    {
        public static int FreeAmount = 10;
        public static int Amount = 10;
        public static event Action<int, int> OnValueChanged;

        public static void Update() => OnValueChanged?.Invoke(FreeAmount, Amount);
    }
}