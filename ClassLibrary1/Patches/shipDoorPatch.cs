using HarmonyLib;
using UnityEngine;

namespace LC_Tweaks.ShipDoor.Patches
{
    [HarmonyPatch(typeof(HangarShipDoor))]
    internal class ShipDoorPatch : HangarShipDoor
    {
        private static new float doorPowerDuration = 55f;
        private static new float doorPower = 1f;
        public static new bool overheated = false;

        public static float DoorPowerDuration { get => doorPowerDuration; set => doorPowerDuration = value; }
        public static float DoorPower { get => doorPower; set => doorPower = value; }

        [HarmonyPatch("Update")]
        [HarmonyPostfix]
        public static void LongerDoorPower()
        {
            if (overheated)
            {
                doorPower = Mathf.Clamp(DoorPower * 2f + (Time.deltaTime / (DoorPowerDuration * 0.11f / (0.5f * 2f))), 0f, 1f);
            } else if (!overheated)
            {
                doorPower = Mathf.Clamp(DoorPower * 2f + (Time.deltaTime / (DoorPowerDuration * 0.11f / (0.5f * 2f))), 0f, 1f);
            }
        }
    }
}
