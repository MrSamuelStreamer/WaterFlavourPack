using System;
using System.Collections.Generic;
using RimWorld;
using Verse;
using Verse.AI;

namespace Thirst_Flavour_Pack.Shades;

public class JobDriver_MergeShades : JobDriver
{
    public Dictionary<Pawn, ShadeTrackerMapComponent.OverriddenShadeStats> ResizedShades => Map.GetComponent<ShadeTrackerMapComponent>()?.resizedShades;
    public void MergeShades()
    {
        if (!ResizedShades.TryGetValue(pawn, out ShadeTrackerMapComponent.OverriddenShadeStats overrides))
        {
            overrides = new ShadeTrackerMapComponent.OverriddenShadeStats();
            ResizedShades[pawn] = overrides;
        }

        overrides.BodySize += TargetPawnB.BodySize;

        if (ResizedShades.ContainsKey(TargetPawnB))
        {
            ResizedShades.Remove(TargetPawnB);
        }

        string nameB = TargetPawnB.NameFullColored;
        TargetPawnB.Destroy(DestroyMode.Vanish);

        Messages.Message("MSS_Thirst_Shade_Merge".Translate(nameB, TargetPawnA.NameFullColored), TargetPawnA, MessageTypeDefOf.ThreatSmall, true);
    }

    public override bool TryMakePreToilReservations(bool errorOnFailed)
    {
        return true;
    }

    protected override IEnumerable<Toil> MakeNewToils()
    {
        // fail if not pawn, and not shambler
        AddEndCondition((Func<JobCondition>) (() => TargetPawnA.Dead || TargetPawnA.kindDef != PawnKindDefOf.ShamblerSwarmer ? JobCondition.Incompletable : JobCondition.Ongoing));
        AddEndCondition((Func<JobCondition>) (() => TargetPawnB.Dead || TargetPawnB.kindDef != PawnKindDefOf.ShamblerSwarmer ? JobCondition.Incompletable : JobCondition.Ongoing));

        this.FailOnDespawnedOrNull(TargetIndex.A);
        this.FailOnDespawnedOrNull(TargetIndex.B);

        yield return Toils_Goto.GotoThing(TargetIndex.B, PathEndMode.Touch, false);
        Toil toil = ToilMaker.MakeToil("MakeNewToils");
        toil.initAction = MergeShades;
        yield return toil;
    }
}
