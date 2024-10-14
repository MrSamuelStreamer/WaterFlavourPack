using System;
using System.Collections.Generic;
using BigAndSmall;
using RimWorld;
using Verse;
using Verse.AI;

namespace Thirst_Flavour_Pack.BS.Shades;

public class JobDriver_MergeShades : JobDriver
{
    public void MergeShades()
    {
        Hediff_MergedShade hediffA = (Hediff_MergedShade)pawn.health.GetOrAddHediff(Thirst_Flavour_Pack_BS_DefOf.MSS_Thirst_MergedShade);

        if (!TargetPawnB.health.hediffSet.TryGetHediff(Thirst_Flavour_Pack_BS_DefOf.MSS_Thirst_MergedShade, out Hediff hediffB))
        {
            hediffA.MergedBodySizeMultiplier += TargetPawnB.GetStatValue(BSDefs.SM_BodySizeMultiplier);
            hediffA.MergedMoveSpeedMultiplier += TargetPawnB.GetStatValue(StatDefOf.MoveSpeed);
            hediffA.MergedMeleeCooldownFactorMultiplier += TargetPawnB.GetStatValue(StatDefOf.MeleeCooldownFactor);
            hediffA.MergedMeleeDamageFactorFactorMultiplier += TargetPawnB.GetStatValue(StatDefOf.MeleeDamageFactor);
            hediffA.Severity += 1;
        }
        else
        {
            hediffA.MergedBodySizeMultiplier += ((Hediff_MergedShade) hediffB).MergedBodySizeMultiplier;
            hediffA.MergedMoveSpeedMultiplier += ((Hediff_MergedShade) hediffB).MergedMoveSpeedMultiplier;
            hediffA.MergedMeleeCooldownFactorMultiplier += ((Hediff_MergedShade) hediffB).MergedMeleeCooldownFactorMultiplier;
            hediffA.MergedMeleeDamageFactorFactorMultiplier += ((Hediff_MergedShade) hediffB).MergedMeleeDamageFactorFactorMultiplier;
            hediffA.Severity += hediffB.Severity;
        }

        string nameA = TargetPawnA.NameFullColored;

        string nameB = TargetPawnB.NameFullColored;
        TargetPawnB.Destroy(DestroyMode.Vanish);

        if (hediffA.Severity > Thirst_Flavour_PackMod.settings.ShadeMergeHediffSeverityToTransform)
        {
            PawnGenerationRequest request = new PawnGenerationRequest(
                kind: Thirst_Flavour_Pack_BS_DefOf.MSS_Thirst_ShamblerGorebeast,
                forceGenerateNewPawn: true,
                allowDowned: false,
                allowDead: false,
                faction: pawn.Faction,
                tile: pawn.Map.Tile,
                fixedGender: pawn.gender,
                fixedBirthName: pawn.Name.ToStringFull,
                fixedBiologicalAge: pawn.ageTracker.AgeBiologicalYearsFloat,
                fixedChronologicalAge: pawn.ageTracker.AgeChronologicalYearsFloat);
            request.ForcedMutant = pawn.mutant.Def;

            Pawn newPawn = PawnGenerator.GeneratePawn(request);
            SpawnRequest req = new SpawnRequest([newPawn], [pawn.Position],1, 1) { spawnSound = SoundDefOf.FleshmassBirth };
            pawn.Map.deferredSpawner.AddRequest(req);
            pawn.Destroy(DestroyMode.Vanish);

            Messages.Message("MSS_Thirst_Shade_Merge_Gorebeast".Translate(nameB, nameA), newPawn, MessageTypeDefOf.ThreatBig, true);
        }
        else
        {
            Messages.Message("MSS_Thirst_Shade_Merge".Translate(nameB, nameA), TargetPawnA, MessageTypeDefOf.ThreatSmall, true);

            HumanoidPawnScaler.GetCache(pawn, forceRefresh: true, scheduleForce: 10);
        }

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
