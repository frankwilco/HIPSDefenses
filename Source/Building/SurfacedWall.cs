using System.Collections.Generic;
using Verse;

namespace SubWall
{
    public class SurfacedWall : SubmersibleWall
    {
        public static readonly ThingDef Def = ThingDef.Named("SubmersibleWall");

        public override void Tick()
        {
            DoTick(SubmersedWall.Def);
            base.Tick();
        }

        public override IEnumerable<Gizmo> GetGizmos()
        {
            return GetGizmosWithAction("Sub");
        }
    }
}