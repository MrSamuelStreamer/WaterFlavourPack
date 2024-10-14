using RimWorld;
using UnityEngine;
using Verse;

namespace Thirst_Flavour_Pack.BS.Shades;

public abstract class StatPart_Offset: StatPart
{
    public abstract string ExplanationString { get; }

    public abstract float GetOffset(Thing t);

    public override void TransformValue(StatRequest req, ref float val)
    {
        val += GetOffset(req.Thing);
    }

    public override string ExplanationPart(StatRequest req)
    {
        float offset = GetOffset(req.Thing);

        return Mathf.Approximately(offset, 0f) ? null : ExplanationString.Translate(offset.ToString("0.00"));
    }
}
