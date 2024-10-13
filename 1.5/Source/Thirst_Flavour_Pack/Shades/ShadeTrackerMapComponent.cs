using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;
using Verse.AI;

namespace Thirst_Flavour_Pack.Shades;

public class ShadeTrackerMapComponent(Map map) : MapComponent(map)
{
    public int shadesOnMapSince = -1;
    public int mergeTick = 600;
    public static int CheckTick = 300;

    public Dictionary<Pawn, OverriddenShadeStats> resizedShades = new Dictionary<Pawn, OverriddenShadeStats>();

    public class OverriddenShadeStats: IExposable
    {
        public float BodySize = 1f;

        public void ExposeData()
        {
            Scribe_Values.Look(ref BodySize, "BodySize");
        }
    }

    public override void ExposeData()
    {
        base.ExposeData();

        Scribe_Collections.Look(ref resizedShades, "resizedShades", LookMode.Reference, LookMode.Value);
    }
}
