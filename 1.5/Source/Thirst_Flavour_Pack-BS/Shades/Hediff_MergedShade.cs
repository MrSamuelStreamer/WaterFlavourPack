using Verse;

namespace Thirst_Flavour_Pack.BS.Shades;

public class Hediff_MergedShade: HediffWithComps
{
    public float MergedBodySizeMultiplier = 1f;
    public float MergedMoveSpeedMultiplier = 1f;
    public float MergedMeleeCooldownFactorMultiplier = 1f;
    public float MergedMeleeDamageFactorFactorMultiplier = 1f;


    public override void ExposeData()
    {
        Scribe_Values.Look(ref MergedBodySizeMultiplier, "MergedBodySizeMultiplier", 1f);
        Scribe_Values.Look(ref MergedMoveSpeedMultiplier, "MergedMoveSpeedMultiplier", 1f);
        Scribe_Values.Look(ref MergedMeleeCooldownFactorMultiplier, "MergedMeleeCooldownFactorMultiplier", 1f);
        Scribe_Values.Look(ref MergedMeleeDamageFactorFactorMultiplier, "MergedMeleeDamageFactorFactorMultiplier", 1f);
    }
}
