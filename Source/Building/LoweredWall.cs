using System.Collections.Generic;
using Verse;

namespace SubWall
{
    public class LoweredWall : RetractableBase
    {
        public static readonly ThingDef Def = ThingDef.Named("LoweredWall");

        public override void Tick()
        {
            DoTick(RetractableWall.Def);
            base.Tick();
        }

        public override IEnumerable<Gizmo> GetGizmos()
        {
            return GetGizmosWithAction("Raise");
        }
    }
}