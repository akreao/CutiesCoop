using Harmony;
using Keplerth;
using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            Root.Instance.Init();
        }

        [HarmonyPatch(typeof(EscUI), "OkButtonOnClick")]
        public class EscUI_OkButtonOnClick_Patch
        {
            public static void Postfix()
            {
                if (Root.Instance.CutiesGUI != null)
                {
                    Root.Instance.CutiesGUI.Init();
                }
            }
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

    public class Root : MonoSingleton<Root>
    {
        public CutiesGUI CutiesGUI { get; set; }

        public void Init()
        {
            SceneManager.activeSceneChanged += OnSceneChange;
            CutiesGUI = gameObject.AddComponent<CutiesGUI>();
            CutiesGUI.enabled = false;
        }

        private void OnSceneChange(Scene from, Scene to)
        {
            if (to.name == "TestGame" || to.name == "GameScene")
            {
                CutiesGUI.enabled = true;
            }
            else
            {
                CutiesGUI.enabled = false;
            }
        }
    }
}
