using System.Linq;
using RimWorld;
using Verse;
using Verse.AI;

namespace Thirst_Flavour_Pack.BS.Shades;

public class JobGiver_ShamblerMerge: ThinkNode_JobGiver
{
    public float ChanceToMerge => Thirst_Flavour_PackMod.settings.ChanceToMerge;
    protected LocomotionUrgency locomotionUrgency = LocomotionUrgency.Jog;

    public override ThinkNode DeepCopy(bool resolve = true)
    {
        JobGiver_ShamblerMerge jobGiver = (JobGiver_ShamblerMerge) base.DeepCopy(resolve);
        jobGiver.locomotionUrgency = locomotionUrgency;
        return jobGiver;
    }

    public Pawn RandomShadeOnPawnsMap(Pawn pawn)
    {
        return pawn.Map.mapPawns.AllPawns.Where(p=>p!=pawn && p.kindDef == PawnKindDefOf.ShamblerSwarmer).RandomElementWithFallback();
    }


    protected override Job TryGiveJob(Pawn pawn)
    {
        if (!Rand.Chance(ChanceToMerge))
        {
            return null;
        }

        bool alreadyMerging = pawn.CurJob != null && pawn.CurJob.def == Thirst_Flavour_PackDefOf.MSS_Thirst_Merge_Shades;

        if (alreadyMerging)
        {
            return null;
        }


        Pawn target = RandomShadeOnPawnsMap(pawn);

        if (target == null)
        {
            return null;;
        }

        pawn.mindState.nextMoveOrderIsWait = false;

        Job job = JobMaker.MakeJob(Thirst_Flavour_PackDefOf.MSS_Thirst_Merge_Shades, pawn, target);

        job.reportStringOverride = "MSS_Thirst_Shade_Merging".Translate(pawn, target);

        return job;
    }
}
