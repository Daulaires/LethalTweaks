using BepInEx;
using BepInEx.Logging;
using Crawler.Patches;
using HarmonyLib;
using LC_Tweaks.Helpers;
using NetworkingPatch.Network;
using Player.Patches;
using UnityEngine;

using System;
using System.Collections.Generic;
using System.Reflection;
using Lethal_Company_Tweaks.Patches;
using LC_Tweaks.ShipDoor.Patches;

namespace TweakBase
{

    [BepInPlugin(ModStaticHelper.modGUID, ModStaticHelper.modName, ModStaticHelper.modVersion)]
    public class LC_Tweaks : BaseUnityPlugin
    {
        // Stuff for Modding
        private readonly Harmony harmony = new Harmony(ModStaticHelper.modGUID);
        public static LC_Tweaks instance;
        internal static ManualLogSource mls;
        // What the game will use to load the Patches.
        private void Awake()
        {
            if (instance == null) { instance = this; CreateCustomLogger(); }
            var types = Assembly.GetExecutingAssembly().GetTypes();
            foreach (var type in types)
            {
                var methods = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
                foreach (var method in methods)
                {
                    var attributes = method.GetCustomAttributes(typeof(RuntimeInitializeOnLoadMethodAttribute), false);
                    if (attributes.Length > 0)
                    {
                        method.Invoke(null, null);
                    }
                }
            }
            // Define a list of types that contain your patches
            List<Type> patchTypes = new List<Type>
            {
                typeof(LC_Tweaks),
                typeof(Networking),
                typeof(ShovelPatch),
                typeof(playerPatches),
                typeof(ShipDoorPatch),
                typeof(crawlerAIPatches),
                typeof(RoundManagerPatch)
            };
            // Loop through the list and apply PatchAll for each type
            foreach (Type patchType in patchTypes)
            {
                harmony.PatchAll(patchType);
                Log($"{patchType.BaseType} was patched");
            }
            // Let the person know it was loaded.
            Log($"Plugin {ModStaticHelper.modName} is loaded!");
        }

        // Func that will allow me to user the Logging Funcs
        private void CreateCustomLogger()
        {
            try { mls = BepInEx.Logging.Logger.CreateLogSource(string.Format("{0}", Info.Metadata.Name)); }
            catch { mls = Logger; }
        }

        // Overrides
        public override int GetHashCode() => base.GetHashCode();
        public override bool Equals(object other) =>base.Equals(other);
        public override string ToString() => base.ToString();
        // Logging Funcs
        public static void Log(string message) => mls.LogInfo(message);
        public static void LogFloat(float message) => mls.LogInfo(message);
        public static void LogError(string message) => mls.LogError(message);
        public static void LogWarning(string message) => mls.LogWarning(message);
    }
}

