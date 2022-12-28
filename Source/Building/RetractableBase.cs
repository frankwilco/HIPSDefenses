using System.Collections.Generic;
using System.Linq;
using RimWorld;
using SubWall.Settings;
using UnityEngine;
using Verse;

namespace SubWall
{
    //Industrial Tech
    public class RetractableBase : Building
    {
        public bool actionWaiting;
        public int powerAction = 150;
        public CompPowerTrader powerComp;
        public int progressTick;
        public int ticksToAction = 360;
        public bool IsPowered => powerComp.PowerOn;

        public UtilityConsole MannedConsole => PowerComp?.PowerNet?.powerComps?.Select(x => x.parent)
            .OfType<UtilityConsole>().FirstOrDefault(x => x.Manned);

        public override void SpawnSetup(Map map, bool respawningAfterLoad)
        {
            base.SpawnSetup(map, respawningAfterLoad);
            powerComp = GetComp<CompPowerTrader>();
            ticksToAction = SubWall_Mod.Settings.ticksToAction;
            powerAction = SubWall_Mod.Settings.powerAction;
        }

        public void DoProgress(int progress)
        {
            MoteMaker.ThrowText(this.TrueCenter(), Map, (progress + 60).TicksToSeconds().ToString(), 1f);
        }

        protected void ReplaceWith(ThingDef def)
        {
            Building building = ThingMaker.MakeThing(def, Stuff) as Building;
            building.SetFaction(Faction);
            building.ChangePaint(PaintColorDef);
            building.HitPoints = HitPoints;
            GenSpawn.Spawn(building, Position, Map, Rotation);
        }

        protected void DoTick(ThingDef def)
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
                ReplaceWith(def);
            }
        }

        protected Gizmo GetWallGizmo(string action)
        {
            var commandAction = new Command_Action
            {
                defaultLabel = $"SubWall_{action}Action".Translate(),
                defaultDesc = $"SubWall_{action}ActionDesc".Translate(),
                icon = ContentFinder<Texture2D>.Get($"UI/Designators/{action}", false),
                action = delegate
                {
                    actionWaiting = true;
                    powerComp.PowerOutput = -powerAction;
                },
                disabled = MannedConsole == null || !IsPowered || actionWaiting
            };

            if (commandAction.disabled)
            {
                if (MannedConsole == null)
                {
                    commandAction.disabledReason = "SubWall_MannedError".Translate();
                }
                if (!IsPowered)
                {
                    commandAction.disabledReason = "SubWall_PowerError".Translate();
                }
                if (actionWaiting)
                {
                    commandAction.disabledReason = "SubWall_PendError".Translate();
                }
            }

            return commandAction;
        }

        protected IEnumerable<Gizmo> GetGizmosWithAction(string action)
        {
            foreach (Gizmo gizmo in base.GetGizmos())
            {
                yield return gizmo;
            }
            yield return GetWallGizmo(action);
        }
#if DEBUG
        public override string GetInspectString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            string baseString = base.GetInspectString();
            if (!baseString.NullOrEmpty())
            {
                stringBuilder.Append(baseString);
                stringBuilder.AppendLine();
            }
            stringBuilder.Append("ticksToAction: " + ticksToAction.ToString());
            stringBuilder.AppendLine();
            stringBuilder.Append("powerAction: " + powerAction.ToString());
            stringBuilder.AppendLine();
            //stringBuilder.Append("PowerOutput: " + powerComp.PowerOutput);
            //stringBuilder.AppendLine();
            //stringBuilder.Append("basePowerConsumption: " + powerComp.PowerOutput);
            //stringBuilder.AppendLine();
            stringBuilder.Append("powerComp: " + powerComp.PowerOn);

            return stringBuilder.ToString().TrimEndNewlines();
        }
#endif
    }

    //Medieval Tech
}

//*/