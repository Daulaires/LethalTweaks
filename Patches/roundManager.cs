using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DunGen;
using GameNetcodeStuff;
using HarmonyLib;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

namespace Lethal_Company_Tweaks.Patches
{
    [HarmonyPatch(typeof(RoundManager))]
    internal class RoundManagerPatch : RoundManager
    {
        [Header("Gameplay events")]
        public new static List<int> enemySpawnTimes = new List<int>();
        public new static bool isSpawningEnemies = false;
        public new static bool begunSpawningEnemies = false;

        [Header("Global Game Variables / Balancing")]
        public new static float scrapValueMultiplier = 1f;
        public new static float scrapAmountMultiplier = 1f;
        public new static float mapSizeMultiplier = 1f;

        [Header("Enemies")]
        public new static EnemyVent[] allEnemyVents = null;
        public new static List<Anomaly> SpawnedAnomalies = new List<Anomaly>();
        public new static List<EnemyAI> SpawnedEnemies = new List<EnemyAI>();
        public static List<int> SpawnProbabilities = new List<int>();

        public new static int hourTimeBetweenEnemySpawnBatches = 2;
        public new static int numberOfEnemiesInScene = 0;
        public new static int minEnemiesToSpawn = 0;
        public new static int minOutsideEnemiesToSpawn = 0;

        [Header("Hazards")]
        public new static SpawnableMapObject[] spawnableMapObjects = null;
        public new static GameObject mapPropsContainer = null;
        public new static Transform[] shipSpawnPathPoints = null;
        public new static GameObject[] spawnDenialPoints = null;
        public new static string[] possibleCodesForBigDoors = null;
        public new static GameObject quicksandPrefab = null;
        public new static GameObject keyPrefab = null;

        static bool GamePlayEvents()
        {
            if (begunSpawningEnemies)
            {
                return begunSpawningEnemies;
            }
            else
            {
                return begunSpawningEnemies;
            }
        }

        static void DespawnEnemies()
        {
            EnemyAINestSpawnObject[] array2 = UnityEngine.Object.FindObjectsByType<EnemyAINestSpawnObject>(FindObjectsSortMode.None);
            for (int j = 0; j < array2.Length; j++)
            {
                NetworkObject component = array2[j].gameObject.GetComponent<NetworkObject>();
                if (component != null && component.IsSpawned)
                    component.Despawn();
                else
                    UnityEngine.Object.Destroy(array2[j].gameObject);
            }
        }

        [HarmonyPatch("Update")]
        [HarmonyPostfix]
        static void Main()
        {
            
        }
    }
}
