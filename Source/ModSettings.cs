using UnityEngine;
using Verse;

namespace SubWall.Settings
{
    internal class HipsDefensesModSettings : ModSettings
    {
        public int powerAction = 150;
        public int ticksToAction = 360;
        public bool consoleRequired = true;

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref ticksToAction, "ticksToAction", 360);
            Scribe_Values.Look(ref powerAction, "powerAction", 15);
            Scribe_Values.Look(ref consoleRequired, "utilityConsoleRequired", true);
        }

        public void DoSettingsWindowContents(Rect canvas)
        {
            var listing_Standard = new Listing_Standard();
            listing_Standard.Begin(canvas);
            listing_Standard.Label("rtd.prefs.reload_required".Translate());
            listing_Standard.Label("rtd.prefs.ticks_to_action.label".Translate() + (ticksToAction / 60));
            ticksToAction = (int)(listing_Standard.Slider(ticksToAction / 60, 1, 25) * 60);
            listing_Standard.Label("rtd.prefs.power_consumed.label".Translate() + powerAction);
            powerAction = (int)listing_Standard.Slider(powerAction, 15, 500);

            bool currentValue = consoleRequired;
            bool newValue = consoleRequired;
            listing_Standard.CheckboxLabeled("rtd.prefs.console_required.label".Translate(), ref newValue);
            if (newValue != currentValue)
            {
                consoleRequired = newValue;
            }

            listing_Standard.End();
        }
    }
}