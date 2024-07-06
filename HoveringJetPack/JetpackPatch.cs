using HarmonyLib;
using SpaceCraft;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;

namespace HoveringJetPack
{

    partial class Plugin
    {

        [HarmonyPatch(typeof(PlayerMovable), nameof(PlayerMovable.UpdatePlayerMovement))]
        static class PlayerMovable_UpdatePlayerMovement_Patch
        {
            //public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
            //{
            //    if (!smoothEnabled.Value) { return instructions; }

            //    var codes = new List<CodeInstruction>(instructions);

            //    // Look for the -5 operand
            //    for (int i = 0; i < codes.Count; i++)
            //    {
            //        var ins = codes[i];
            //        if (ins.opcode == OpCodes.Ldc_R4 && (float)ins.operand == -5f)
            //        {
            //            // Replace the next 4 op, replacing the next one with 0
            //            codes[i + 1] = new CodeInstruction(OpCodes.Ldc_R4, 0f);
            //            codes.RemoveRange(i + 2, 3);

            //            break;
            //        }
            //    }

            //    return codes;
            //}

            static void Prefix(float ___jumpActionValue, int ___jumpStatusInAir, float ___jetpackFactor, CharacterController ___m_Controller, ref float ___tooFarFromGroundCorrection, out float? __state)
            {
                __state = null;

                if (hoveringEnabled && ___jumpActionValue > 0f && ___jetpackFactor > 0f && ___jumpStatusInAir == 2)
                {
                    __state = ___m_Controller.transform.position.y;
                }

                if (smoothEnabled.Value)
                {
                    ___tooFarFromGroundCorrection = 0f;
                }
            }

            static void Postfix(float ___jumpActionValue, int ___jumpStatusInAir, float ___jetpackFactor, CharacterController ___m_Controller, float? __state)
            {
                if (__state.HasValue && ___jumpActionValue > 0f && ___jetpackFactor > 0f && ___jumpStatusInAir == 2)
                {
                    var delta = ___m_Controller.transform.position.y - __state.Value;                    
                    ___m_Controller.Move(Vector3.up * -delta);
                }
            }

        }

    }

}
