using HarmonyLib;

namespace Lethal_Company_Tweaks.Patches
{
    
    [HarmonyPatch(typeof(Shovel))]
    internal class ShovelPatch : GrabbableObject
    {
        private static int shovelHitForce;

        public static int ShovelHitForce { get => shovelHitForce; set => shovelHitForce = value; }

        [HarmonyPatch("HitShovel")]
        [HarmonyPostfix]
        public static void OnInteract()
        {
            
            // now we assign the new value to the shovelHitForce
            shovelHitForce = 2;

        }
    }
}
