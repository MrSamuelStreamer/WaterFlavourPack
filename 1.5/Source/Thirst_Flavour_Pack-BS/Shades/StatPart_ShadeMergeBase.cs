using RimWorld;
using UnityEngine;
using Verse;

namespace Thirst_Flavour_Pack.BS.Shades;

public abstract class StatPart_ShadeMergeBase: StatPart
{
    public abstract string ExplanationString { get; }

    public abstract float GetMultiplier(Thing t);

    public override void TransformValue(StatRequest req, ref float val)
    {
        val *= GetMultiplier(req.Thing);
    }

    public override string ExplanationPart(StatRequest req)
    {
        float multiplier = GetMultiplier(req.Thing);

        return Mathf.Approximately(multiplier, 1f) ? null : ExplanationString.Translate(multiplier.ToString("0.00" + "%"));
    }
}
