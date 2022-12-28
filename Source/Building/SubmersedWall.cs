using System.Collections.Generic;
using Verse;

namespace SubWall
{
    public class SubmersedWall : SubmersibleWall
    {
        public static readonly ThingDef Def = ThingDef.Named("SubmersedWall");

        public override void Tick()
        {
            DoTick(SurfacedWall.Def);
            base.Tick();
        }

        public override IEnumerable<Gizmo> GetGizmos()
        {
            return GetGizmosWithAction("Rise");
        }
    }
}