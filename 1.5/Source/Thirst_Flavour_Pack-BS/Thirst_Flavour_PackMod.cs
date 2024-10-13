using HarmonyLib;
using Verse;

namespace Thirst_Flavour_Pack.BS;

public class Thirst_Flavour_Pack_BS_Mod : Mod
{
    public Thirst_Flavour_Pack_BS_Mod(ModContentPack content) : base(content)
    {
#if DEBUG
        ModLog.Log("Thirst_Flavour_Pack_BS_Mod");
        Harmony.DEBUG = true;
#endif
        Harmony harmony = new Harmony("Feldoh.rimworld.Thirst_Flavour_Pack.BS.main");
        harmony.PatchAll();
    }
}
