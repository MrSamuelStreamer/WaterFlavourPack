using Verse;

namespace Thirst_Flavour_Pack.BS.Shades;

public class StatPart_MeleeDamageFactor: StatPart_Offset
{
    public override string ExplanationString => "MSS_Thirst_MeleeDamageFactor";

    public override float GetOffset(Thing t)
    {
        if(t is not Pawn pawn) return 0f;

        return !pawn.health.hediffSet.TryGetHediff(out Hediff_MergedShade hediff) ? 0f : hediff.MergedMeleeDamageFactorOffset;
    }
}
