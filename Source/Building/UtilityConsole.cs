using RimWorld;
using Verse;

namespace FrankWilco.RimWorld
{
    public class UtilityConsole : Building
    {
        public CompMannable mannableComp;
        public bool Manned => mannableComp.MannedNow;

        public override void SpawnSetup(Map map, bool respawningAfterLoad)
        {
            base.SpawnSetup(map, respawningAfterLoad);
            mannableComp = GetComp<CompMannable>();
        }
    }
}