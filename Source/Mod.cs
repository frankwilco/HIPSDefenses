using UnityEngine;
using Verse;

namespace FrankWilco.RimWorld
{
    public class HipsDefensesMod : Mod
    {
        public static HipsDefensesModSettings Settings;

        public HipsDefensesMod(ModContentPack content)
            : base(content)
        {
            Settings = GetSettings<HipsDefensesModSettings>();
        }

        public override string SettingsCategory()
        {
            return "rtd.prefs.title".Translate();
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            Settings.DoSettingsWindowContents(inRect);
        }
    }
}