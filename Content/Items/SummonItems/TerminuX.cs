using CalamityMod.Events;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.SummonItems;
using CalBossRushRework.Core.CrossCompatibility.Calamity;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalBossRushRework.Content.Items.SummonItems;

[JITWhenModsEnabled(CalamityCompatibility.ModName)]
[ExtendsFromMod(CalamityCompatibility.ModName)]
public class TerminuX : ModItem
{
    public override void SetStaticDefaults() => Item.ResearchUnlockCount = 1;

    public override void SetDefaults()
    {
        Item.width = 60;
        Item.height = 80;
        Item.useAnimation = 40;
        Item.useStyle = ItemUseStyleID.HoldUp;
        Item.useTime = 40;
        Item.useStyle = ItemHoldStyleID.HoldUp;
        Item.autoReuse = true;
        Item.noUseGraphic = true;
        Item.consumable = false;
        Item.noMelee = true;
        Item.rare = ItemRarityID.Blue;
        Item.UseSound = null;
    }

    public override bool? UseItem(Player player)
    {
        if (BossRushEvent.BossRushActive)
        {
            SoundEngine.PlaySound(BossRushEvent.TerminusDeactivationSound, Main.LocalPlayer.Center);
            BossRushEvent.End();
        }
        else
        {
            BossRushEvent.SyncStartTimer(120);
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC n = Main.npc[i];
                if (!n.active)
                {
                    continue;
                }

                bool shouldDespawn = n.boss || n.type == NPCID.EaterofWorldsHead || n.type == NPCID.EaterofWorldsBody || n.type == NPCID.EaterofWorldsTail;
                if (shouldDespawn)
                {
                    n.active = false;
                    n.netUpdate = true;
                }
            }

            SoundEngine.PlaySound(BossRushEvent.TerminusActivationSound, Main.LocalPlayer.Center);
            BossRushEvent.BossRushStage = 0;
            BossRushEvent.BossRushActive = true;
        }
        return true;
    }

    public override void AddRecipes()
    {
        var CalamityMod = ModLoader.TryGetMod("CalamityMod", out Mod calamity);

        if (CalamityMod)
        {
            try
            {
                Recipe.Create(Type, 1)
                    .AddIngredient(calamity, "Terminus", 1)
                    .Register();
            }
            catch
            {
                return;
            }
        }
    }

    public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
    {
        itemGroup = ContentSamples.CreativeHelper.ItemGroup.EventItem;
    }
}