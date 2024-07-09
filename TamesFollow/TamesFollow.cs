using System.Reflection;
using BepInEx;
using HarmonyLib;
using JetBrains.Annotations;

namespace TamesFollow
{
    [BepInPlugin(PluginId, "Tames Follow", "1.0.0")]
    public class TamesFollow : BaseUnityPlugin
    {
        public const string PluginId = "oldmankatan.mods.tamesfollow";
        private static TamesFollow _instance;

        private Harmony _harmony;

        [UsedImplicitly]
        private void Awake()
        {
            _harmony = Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), PluginId);
            _instance = this;
        }

        [UsedImplicitly]
        private void OnDestroy()
        {
            _harmony?.UnpatchSelf();
            _instance = null;
        }
    }

    [HarmonyPatch(typeof(Tameable), "Interact")]
    public class Tameable_Interact_Patch
    {
        private static void Prefix(Tameable __instance) {
            Character m_character = __instance.GetComponent<Character>();
            if (m_character.IsTamed())
            {
                __instance.m_commandable = true;
            }
        }
    }
}
