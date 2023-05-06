using UnityEngine;

namespace TownRally
{
    internal class PlayerPrefsHandler
    {
        private static readonly string PP_PREFIX = "NibbsTown/";
        private static readonly string PP_STARTED_RALLIES = "StartedRallies/";

        internal static bool VarOut_RallyStartedPreviously(string rallyId)
        {
            string key = string.Concat(PP_PREFIX, PP_STARTED_RALLIES, rallyId);
            return PlayerPrefs.HasKey(key);
        }

        internal void Init()
        {

        }
    }
}
