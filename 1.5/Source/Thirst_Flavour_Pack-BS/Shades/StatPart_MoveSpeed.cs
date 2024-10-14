using Verse;

namespace Thirst_Flavour_Pack.BS.Shades;

public class StatPart_MoveSpeed: StatPart_Multiplier
{
    public override string ExplanationString => "MSS_Thirst_MoveSpeed";

    public override float GetMultiplier(Thing t)
    {
        if(t is not Pawn pawn) return 1f;

        return !pawn.health.hediffSet.TryGetHediff(out Hediff_MergedShade hediff) ? 1f : 1/hediff.MergedMoveSpeedInverseMultiplier;
    }
}
