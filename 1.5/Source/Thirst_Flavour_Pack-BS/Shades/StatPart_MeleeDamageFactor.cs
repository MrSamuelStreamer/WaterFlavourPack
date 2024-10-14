using Verse;

namespace Thirst_Flavour_Pack.BS.Shades;

public class StatPart_MeleeDamageFactor: StatPart_ShadeMergeBase
{
    public override string ExplanationString => "MSS_Thirst_MeleeDamageFactor";

    public override float GetMultiplier(Thing t)
    {
        if(t is not Pawn pawn) return 1f;

        return !pawn.health.hediffSet.TryGetHediff(out Hediff_MergedShade hediff) ? 1f : hediff.MergedMeleeDamageFactorFactorMultiplier;
    }
}
