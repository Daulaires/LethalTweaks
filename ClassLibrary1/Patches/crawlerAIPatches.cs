using HarmonyLib;

namespace Crawler.Patches
{
    [HarmonyPatch(typeof(CrawlerAI))]
    internal class crawlerAIPatches : CrawlerAI
    {
        private new const float maxSearchAndRoamRadius = 25.0f;
        public static float MaxSearchAndRoamRadius => maxSearchAndRoamRadius;
        [HarmonyPatch(nameof(Update))]
        [HarmonyPostfix]
        static void OnPatch(ref float ___maxSearchAndRoamRadius)
        {
            ___maxSearchAndRoamRadius = MaxSearchAndRoamRadius;
        }
    }
}
