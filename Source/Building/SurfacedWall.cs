using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace SubWall
{
    public class SurfacedWall : SubmersibleWall
    {
        public static readonly ThingDef Def = ThingDef.Named("SubmersibleWall");

        public override void Tick()
        {
            if (actionWaiting)
            {
                progressTick += 1;
                if (progressTick % 60 == 0)
                {
                    DoProgress(ticksToAction - progressTick);
                }
            }

            if (progressTick >= ticksToAction)
            {
                progressTick = 0;
                actionWaiting = false;
                ReplaceWith(SubmersedWall.Def);
            }

            base.Tick();
        }

        public override IEnumerable<Gizmo> GetGizmos()
        {
            foreach (var gizmo in base.GetGizmos())
            {
                yield return gizmo;
            }

            var Sub = new Command_Action
            {
                defaultLabel = "SubWall_SubAction".Translate(),
                defaultDesc = "SubWall_SubActionDesc".Translate(),
                icon = ContentFinder<Texture2D>.Get("UI/Designators/Sub", false),
                action = PendAction
            };
            if (MannedConsole != null && IsPowered && !actionWaiting)
            {
                Sub.disabled = false;
            }
            else
            {
                Sub.disabled = true;
                if (MannedConsole == null)
                {
                    Sub.disabledReason = "SubWall_MannedError".Translate();
                }

                if (!IsPowered)
                {
                    Sub.disabledReason = "SubWall_PowerError".Translate();
                }

                if (actionWaiting)
                {
                    Sub.disabledReason = "SubWall_PendError".Translate();
                }
            }

            yield return Sub;
        }
    }
}