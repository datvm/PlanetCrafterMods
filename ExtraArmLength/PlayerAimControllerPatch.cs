using HarmonyLib;
using SpaceCraft;
using UnityEngine;

namespace ExtraArmLength
{

    partial class Plugin
    {

        [HarmonyPatch(typeof(PlayerAimController), "SetLayerMaskAndDistance")]
        public class PlayerAimControllerPatch
        {

            static void Postfix(ref float ____distanceHitLimit)
            {
                ____distanceHitLimit = distance.Value;
            }

        }

    }

}
