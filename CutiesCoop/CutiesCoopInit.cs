using Harmony;
using Keplerth;
using System.Reflection;
using UnityEngine;

namespace CutiesCoop
{
    [StaticConstructorOnStartup]
    public static class CutiesCoopInit
    {
        public static string modID;
        public static CutiesConfig config;

        static CutiesCoopInit()
        {
            modID = "com.akreao.cutiescoopmod";
            config = new CutiesConfig();
            Application.quitting += Application_quitting;

            HarmonyInstance.Create(modID).PatchAll(Assembly.GetExecutingAssembly());
        }

        private static void Application_quitting()
        {
            config.SaveConfig();
        }

        [HarmonyPatch(typeof(DropItems), nameof(DropItems.DropItem))]
        public class DropRatePatch
        {
            public static bool Prefix(ref ItemData item)
            {
                CutiesRates.ApplyDropRate(ref item);

                return true;
            }
        }

    }
}
