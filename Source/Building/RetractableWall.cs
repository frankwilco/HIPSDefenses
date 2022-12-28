using System.Collections.Generic;
using Verse;

namespace SubWall
{
    public class RetractableWall : RetractableBase
    {
        public static readonly ThingDef Def = ThingDef.Named("RetractableWall");

        public override void Tick()
        {
            DoTick(LoweredWall.Def);
            base.Tick();
        }

        public override IEnumerable<Gizmo> GetGizmos()
        {
            return GetGizmosWithAction("Lower");
        }
    }
}