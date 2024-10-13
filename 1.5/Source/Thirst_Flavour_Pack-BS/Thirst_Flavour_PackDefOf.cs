using RimWorld;
using Verse;
// ReSharper disable UnassignedField.Global

namespace Thirst_Flavour_Pack.BS;

[DefOf]
public static class Thirst_Flavour_Pack_BS_DefOf
{
    public static HediffDef MSS_Thirst_MergedShade;

    static Thirst_Flavour_Pack_BS_DefOf() => DefOfHelper.EnsureInitializedInCtor(typeof(Thirst_Flavour_PackDefOf));
}
