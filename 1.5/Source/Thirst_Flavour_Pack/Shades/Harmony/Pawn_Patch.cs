using System.Collections.Generic;
using HarmonyLib;
using Verse;

namespace Thirst_Flavour_Pack.Shades.Harmony;

[HarmonyPatch(typeof(Pawn))]
public static class Pawn_Patch
{
    [HarmonyPatch(nameof(Pawn.BodySize), MethodType.Getter)]
    [HarmonyBefore(["RedMattis.BetterPrerequisites"])]
    [HarmonyPostfix]
    public static void BodySize(Pawn __instance, ref float __result)
    {
        Dictionary<Pawn, ShadeTrackerMapComponent.OverriddenShadeStats> resizedShades = __instance.Map?.GetComponent<ShadeTrackerMapComponent>()?.resizedShades;
        if (resizedShades != null && resizedShades.TryGetValue(__instance, out ShadeTrackerMapComponent.OverriddenShadeStats overrides))
        {
            __result = overrides.BodySize;
        }
    }
}
