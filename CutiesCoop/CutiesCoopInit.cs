using Harmony;
using Keplerth;
using System;
using System.Reflection;

namespace CutiesCoop
{
    [StaticConstructorOnStartup]
    public static class CutiesCoopInit
    {
        public const string modID = "com.akreao.cutiescoopmod";
        private const int DROP_RATE = 2; //Need config file and GUI at some point

        private static readonly int[] oreItemIDs = { 3004, 3005, 3006, 3007, 3008, 3009, 3012, 3013, 3014, 3015 };
        private static readonly int[] plantItemIDs = { 9001, 9002, 3221, 3204, 3203, 3201 };
        private static readonly int[] foodItemIDs = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17 };
        private static readonly int[] dropsItemIDs = { 9003, 9006, 9007, 9008, 9009, 9010, 9018, 9027, };
        //static readonly int[] dnaItemIDs = { };
        static CutiesCoopInit()
        {
            HarmonyInstance.Create(modID).PatchAll(Assembly.GetExecutingAssembly());
        }

        [HarmonyPatch(typeof(DropItems), nameof(DropItems.DropItem))]
        public class DropRatePatch
        {
            public static bool Prefix(ref ItemData item)
            {
                if (item.count > 0 && IsDropRateAffected(item))
                {
                    item.count *= DROP_RATE;
                }

                return true;
            }

            private static bool IsDropRateAffected(ItemData item)
            {
                return Array.Exists(oreItemIDs, e => e == item.id) || Array.Exists(plantItemIDs, e => e == item.id) || Array.Exists(foodItemIDs, e => e == item.id) || Array.Exists(dropsItemIDs, e => e == item.id);
            }
        }
    }
}
