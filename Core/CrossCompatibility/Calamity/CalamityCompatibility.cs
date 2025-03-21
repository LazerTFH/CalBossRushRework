using CalamityMod;
using CalamityMod.CalPlayer;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalBossRushRework.Core.CrossCompatibility.Calamity;

public class CalamityCompatibility : ModSystem
{
    // Internal name of the Calamity Mod
    public const string ModName = "CalamityMod";

    // Calamity's mod instance
    public static Mod Calamity
    {
        get;
        private set;
    }

    // Whether the Calamity Mod is evabled
    public static bool Enabled => ModLoader.HasMod(ModName);

    public override void OnModLoad()
    {
        if (ModLoader.TryGetMod(ModName, out Mod cal))
        Calamity = cal;
    }
}