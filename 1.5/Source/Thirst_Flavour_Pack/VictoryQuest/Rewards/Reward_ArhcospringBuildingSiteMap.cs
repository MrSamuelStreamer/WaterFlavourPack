﻿using System;
using System.Collections.Generic;
using RimWorld;
using UnityEngine;
using Verse;
using Verse.Grammar;

namespace Thirst_Flavour_Pack.VictoryQuest.Rewards;

[StaticConstructorOnStartup]
public class Reward_ArhcospringBuildingSiteMap : Reward
{
    public static Texture2D icon;
    public static Texture2D Icon
    {
        get
        {
            if (icon == null)
                icon = ContentFinder<Texture2D>.Get("UI/Icons/ArchonexusMapPart");
            return icon;
        }
    }

    public override IEnumerable<GenUI.AnonymousStackElement> StackElements
    {
        get
        {
            yield return QuestPartUtility.GetStandardRewardStackElement(
                "MSS_Thirst_Reward_ArchospringSignalLocationLabel".Translate(), Icon, () => $"{GetDescription(default).CapitalizeFirst()}.", null);
        }
    }

    public override void InitFromValue(
        float rewardValue,
        RewardsGeneratorParams parms,
        out float valueActuallyUsed)
    {
        throw new NotImplementedException();
    }

    public override IEnumerable<QuestPart> GenerateQuestParts(
        int index,
        RewardsGeneratorParams parms,
        string customLetterLabel,
        string customLetterText,
        RulePack customLetterLabelRules,
        RulePack customLetterTextRules)
    {
        throw new NotImplementedException();
    }

    public override string GetDescription(RewardsGeneratorParams parms)
    {
        return "MSS_Thirst_Reward_ArchospringSignalLocationDesc".Translate();
    }
}
