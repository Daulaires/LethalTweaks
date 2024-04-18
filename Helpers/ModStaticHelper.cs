using BepInEx.Bootstrap;
using BepInEx.Logging;

namespace LC_Tweaks.Helpers
{
    public class ModStaticHelper
    {
        // Mod Information
        public const string modName = "LC Tweaks";
        public const string modGUID = "LC.Tweaks";
        public const string modVersion = "1.0.2";

        public static bool IsThisModInstalled(string mod)
        {
            if (Chainloader.PluginInfos.TryGetValue(mod, out BepInEx.PluginInfo modInfo))
            {
                TweakBase.LC_Tweaks.Log($"Mod ${mod} is loaded alongside {modInfo}");
                return true;
            }
            return false;
        }
    }
}