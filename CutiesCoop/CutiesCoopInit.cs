using Harmony;
using Keplerth;
using System.Collections.Generic;
using System.Reflection;

namespace CutiesCoop
{
    [StaticConstructorOnStartup]
    public static class CutiesCoopInit
    {
        public const int DROP_RATE = 2;
        private static readonly List<ItemType> bannedItemTypes = new List<ItemType> { ItemType.Equip, ItemType.Weapon, ItemType.Clue, ItemType.SkillItem, ItemType.RandomItemBag/*, ItemType.Placement*/ };
        static CutiesCoopInit()
        {
            HarmonyInstance.Create("com.akreao.cutiescoopmod").PatchAll(Assembly.GetExecutingAssembly());
        }

        [HarmonyPatch(typeof(DropItems), nameof(DropItems.DropItem))]
        public class DropRatePatch
        {
            public static bool Prefix(ref ItemData item)
            {
                if (item.count > 0 && IsDropRateAffected(item.type))
                {
                    item.count *= DROP_RATE;
                }

                return true;
            }

            private static bool IsDropRateAffected(ItemType type)
            {
                return !bannedItemTypes.Contains(type);
            }
        }
    }
}
