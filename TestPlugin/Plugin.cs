using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using SpaceCraft;
using System.Reflection;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

namespace TestPlugin
{
    [BepInPlugin("com.lukevo.test", "Test plugin", "1.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        static Plugin instance;

        static ConfigEntry<bool> isEnabled;

        public static ManualLogSource Log => instance.Logger;

        private void Awake()
        {
            instance = this;

            isEnabled = Config.Bind("General", "Enabled", true, "Enable when the game starts");

            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), null);
        }

        [HarmonyPatch(typeof(PlayerMovable), nameof(PlayerMovable.UpdatePlayerMovement))]
        static class PlayerMovable_UpdatePlayerMovement_Patch
        {
            static float? state;

            static void Prefix(float ___jumpActionValue, int ___jumpStatusInAir, float ___jetpackFactor, CharacterController ___m_Controller)
            {
                state = null;

                if (___jumpActionValue > 0f && ___jetpackFactor > 0f && ___jumpStatusInAir == 2)
                {
                    state = ___m_Controller.transform.position.y;
                    Log.LogInfo("Logging state Y: " + state.Value);

                }
            }

            static void Postfix(float ___jumpActionValue, int ___jumpStatusInAir, float ___jetpackFactor, CharacterController ___m_Controller)
            {
                if (state.HasValue && ___jumpActionValue > 0f && ___jetpackFactor > 0f && ___jumpStatusInAir == 2)
                {
                    var delta = ___m_Controller.transform.position.y - state.Value;
                    Log.LogInfo("Delta: " + delta);

                    ___m_Controller.Move(Vector3.up * -delta);
                }
            }

        }

    }
}
