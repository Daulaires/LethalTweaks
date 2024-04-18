using GameNetcodeStuff;
using HarmonyLib;
using UnityEngine;

namespace Player.Patches
{

    [HarmonyPatch(typeof(PlayerControllerB))]
    internal class playerPatches : PlayerControllerB
    {

        [Header("INPUT / MOVEMENT")]
        public new const float movementSpeed = 0.5f;
        private new const float sprintTime = 25f;
        private const float sprintMultiplier = 1.4f;
        private const float num2 = 0.9f;
        public new const float carryWeight = 1.0f;

        public static float SprintTime => sprintTime;
        public static float SprintMultiplier => sprintMultiplier;
        public static float MovementSpeed => movementSpeed;
        public static float Num2 => num2;

        [Header("DEATH")]
        public static new int health = 0;
        public static new float healthRegenerateTimer = Time.deltaTime;
        public static new bool criticallyInjured = false;
        public static new bool hasBeenCriticallyInjured = false;
        private static readonly float limpMultiplier = 0.2f;
        public static new CauseOfDeath causeOfDeath;
        public static new bool isPlayerDead = false;

        [HarmonyPatch("Update")]
        [HarmonyPostfix]
        private static void SlightlyBetterSprint(ref float ___sprintMultiplier, ref float ___sprintTime, ref bool ___isSprinting, ref bool ___isWalking, ref float ___sprintMeter)
        {
        
            if (___isSprinting)
            {
                ___sprintTime = SprintTime;
                ___sprintMultiplier = Mathf.Lerp(SprintMultiplier, 2.25f, Time.deltaTime * 1f);
                ___sprintMeter = Mathf.Clamp(___sprintMeter - Time.deltaTime / SprintTime * carryWeight * Num2, 0f, 1f);
            } else if (___isWalking){
                float num3 = 1.4f;
                float SprintMultiplier = 1.0f;
                ___sprintMultiplier = Mathf.Lerp(SprintMultiplier, 1f, 10f * Time.deltaTime);
                ___sprintMeter = Mathf.Clamp(___sprintMeter + Time.deltaTime / ___sprintTime * num3, 0f, 1f);
            } else if (!___isWalking) 
            {
                float num3 = 1.8f;
                float SprintMultiplier = 1.0f;
                ___sprintMultiplier = Mathf.Lerp(SprintMultiplier, 1f, 10f * Time.deltaTime);
                ___sprintMeter = Mathf.Clamp(___sprintMeter + Time.deltaTime / ___sprintTime * num3, 0f, 1f);
            }
        
        }

    }
}
