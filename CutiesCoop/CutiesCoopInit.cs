using System.Collections.Generic;
using Keplerth;
using Harmony;
using System.Reflection;
using UnityEngine;

namespace CutiesCoop
{
    [StaticConstructorOnStartup]
    public static class CutiesCoopInit
    {
        private const int DROP_RATE = 2;
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
                    item.count *= DROP_RATE;

                return true;
            }

            private static bool IsDropRateAffected(ItemType type)
            {
                if (type != ItemType.Equip &&
                    type != ItemType.Weapon &&
                    type != ItemType.Equip &&
                    type != ItemType.Clue &&
                    type != ItemType.SkillItem &&
                    type != ItemType.RandomItemBag)
                    return false;
                else
                    return true;
            }
        }
    }
}
