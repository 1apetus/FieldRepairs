﻿using HBS.Logging;
using System;
using System.Collections.Generic;

namespace FieldRepairs
{

    public static class ModStats
    {
        public const string TestStat = "IRFR_TestStat";
        public const string AmmoBoxCurrentAmmo = "CurrentAmmo";
    }

    public class ModConfig
    {

        public class SkirmishConfig
        {
            /* A tag to apply to enemy units during skirmish matches. Can be one of the vanilla tags for now:
             * spawn_poorly_maintained_25
             * spawn_poorly_maintained_50
             * spawn_poorly_maintained_75
            */
            public string Tag = "spawn_poorly_maintained_25";
        }
        public SkirmishConfig Skirmish = new SkirmishConfig();

        public class UnitRollCfg
        {
            public int PM25_MinRolls = 1;
            public int PM25_MaxRolls = 3;

            public int PM50_MinRolls = 2;
            public int PM50_MaxRolls = 4;

            public int PM75_MinRolls = 3;
            public int PM75_MaxRolls = 6;
        }
        public class DamageRollCfg
        {
            public UnitRollCfg MechRolls = new UnitRollCfg()
            {
                PM25_MinRolls = 0,
                PM25_MaxRolls = 2,
                PM50_MinRolls = 0,
                PM50_MaxRolls = 4,
                PM75_MinRolls = 1,
                PM75_MaxRolls = 5
            };

            public UnitRollCfg VehicleRolls = new UnitRollCfg()
            {
                PM25_MinRolls = 0,
                PM25_MaxRolls = 2,
                PM50_MinRolls = 0,
                PM50_MaxRolls = 3,
                PM75_MinRolls = 1,
                PM75_MaxRolls = 4

            };

            public UnitRollCfg TurretRolls = new UnitRollCfg()
            {
                PM25_MinRolls = 0,
                PM25_MaxRolls = 2,
                PM50_MinRolls = 0,
                PM50_MaxRolls = 3,
                PM75_MinRolls = 1,
                PM75_MaxRolls = 4
            };
        }
        public DamageRollCfg DamageRollsConfig = new DamageRollCfg();

        public class ThemeConfig
        {
            public const int MaxWeightItems = 10;

            public string Label;
            public override string ToString() { return Label; }

            public string[] MechWeights;
            public DamageType[] MechTable = new DamageType[MaxWeightItems];

            public string[] VehicleWeights;
            public DamageType[] VehicleTable = new DamageType[MaxWeightItems];

            public string[] TurretWeights;
            public DamageType[] TurretTable = new DamageType[MaxWeightItems];
        }

        public List<ThemeConfig> Themes = new List<ThemeConfig> { };

        public class CCCategories
        {
            public List<string> Blacklisted = new List<string> { "Armor", "Structure", "CASE", "PositiveQuirk", "Cockpit" };
        }
        public CCCategories CustomComponentCategories = new CCCategories();


        public class HitPenalties
        {
            public float MinArmorLoss = 0.2f;
            public float MaxArmorLoss = 0.5f;
            public float MinStructureLoss = 0.1f;
            public float MaxStructureLoss = 0.3f;

            public int MinSkillPenalty = 0;
            public int MaxSkillPenalty = 2;

            public float MinAmmoRemaining = 0.05f;
            public float MaxAmmoRemaining = 0.25f;
        }
        public HitPenalties PerHitPenalties = new HitPenalties();

        //disable injuries for TrooperSquads
        public bool DisableInjuriesForTroopers = true;
        // If true, many logs will be printed
        public bool Debug = false;
        // If true, all logs will be printed
        public bool Trace = false;

        // Localizations
        public const string LT_TT_DAMAGE_ARMOR = "ARMOR_DAMAGE";
        public const string LT_TT_DAMAGE_STRUCTURE = "STRUCT_DAMAGE";
        public const string LT_TT_DAMAGE_COMP = "COMP_DAMAGE";
        public const string LT_TT_DAMAGE_PILOT = "PILOT_DAMAGE";
        public const string LT_TT_DAMAGE_SKILL = "SKILL_DAMAGE";

        public const string LT_TT_SKILL_GUNNERY = "SKILL_GUNNERY";
        public const string LT_TT_SKILL_GUTS = "SKILL_GUTS";
        public const string LT_TT_SKILL_PILOTING = "SKILL_PILOTING";
        public const string LT_TT_SKILL_TACTICS = "SKILL_TACTICS";

        public const string LT_TT_PILOT_HEALTH = "PILOT_HEALTH";
        public const string LT_TT_PILOT_BONUS_HEALTH = "PILOT_BONUS_HEALTH";

        public Dictionary<string, string> LocalizedText = new Dictionary<string, string>()
        {
            { LT_TT_DAMAGE_ARMOR, "<color=#FF0000>ARMOR DAMAGE</color>\n" },
            { LT_TT_DAMAGE_STRUCTURE, "<color=#FF0000>STRUCTURE DAMAGE</color>\n" },
            { LT_TT_DAMAGE_COMP, "<color=#FF0000>COMPONENT DAMAGE</color>\n" },
            { LT_TT_DAMAGE_PILOT, "<color=#FF0000>HEALTH DAMAGE</color>\n" },
            { LT_TT_DAMAGE_SKILL, "<color=#FF0000>SKILL PENALTY</color>\n" },

            { LT_TT_SKILL_GUNNERY, " - Gunnery: -{0}\n" },
            { LT_TT_SKILL_GUTS, " - Guts: -{0}\n" },
            { LT_TT_SKILL_PILOTING, " - Piloting: -{0}\n" },
            { LT_TT_SKILL_TACTICS, " - Tactics: -{0}\n" },

            { LT_TT_PILOT_HEALTH, " - Health: -{0}\n" },
            { LT_TT_PILOT_BONUS_HEALTH, " - Bonus Health: -{0}\n" },
        };

        public void LogConfig()
        {
            Mod.Log.LogDebug("=== MOD CONFIG BEGIN ===");
            Mod.Log.LogDebug($"  DEBUG:{this.Debug} Trace:{this.Trace}");

            Mod.Log.LogDebug(" --- SKIRMISH ---");
            Mod.Log.LogDebug($"  TAG: {this.Skirmish.Tag}");

            Mod.Log.LogDebug(" --- DAMAGE ROLLS ---");
            Mod.Log.LogDebug($"  MECH ROLLS: ");
            Mod.Log.LogDebug($"    poorly_maintained_25 -> min: {this.DamageRollsConfig.MechRolls.PM25_MinRolls} / max: {this.DamageRollsConfig.MechRolls.PM25_MaxRolls}");
            Mod.Log.LogDebug($"    poorly_maintained_50 -> min: {this.DamageRollsConfig.MechRolls.PM50_MinRolls} / max: {this.DamageRollsConfig.MechRolls.PM50_MaxRolls}");
            Mod.Log.LogDebug($"    poorly_maintained_75 -> min: {this.DamageRollsConfig.MechRolls.PM75_MinRolls} / max: {this.DamageRollsConfig.MechRolls.PM75_MaxRolls}");
            Mod.Log.LogDebug($"  VEHICLE ROLLS: ");
            Mod.Log.LogDebug($"    poorly_maintained_25 -> min: {this.DamageRollsConfig.VehicleRolls.PM25_MinRolls} / max: {this.DamageRollsConfig.VehicleRolls.PM25_MaxRolls}");
            Mod.Log.LogDebug($"    poorly_maintained_50 -> min: {this.DamageRollsConfig.VehicleRolls.PM50_MinRolls} / max: {this.DamageRollsConfig.VehicleRolls.PM50_MaxRolls}");
            Mod.Log.LogDebug($"    poorly_maintained_75 -> min: {this.DamageRollsConfig.VehicleRolls.PM75_MinRolls} / max: {this.DamageRollsConfig.VehicleRolls.PM75_MaxRolls}");
            Mod.Log.LogDebug($"  TURRET ROLLS: ");
            Mod.Log.LogDebug($"    poorly_maintained_25 -> min: {this.DamageRollsConfig.TurretRolls.PM25_MinRolls} / max: {this.DamageRollsConfig.TurretRolls.PM25_MaxRolls}");
            Mod.Log.LogDebug($"    poorly_maintained_50 -> min: {this.DamageRollsConfig.TurretRolls.PM50_MinRolls} / max: {this.DamageRollsConfig.TurretRolls.PM50_MaxRolls}");
            Mod.Log.LogDebug($"    poorly_maintained_75 -> min: {this.DamageRollsConfig.TurretRolls.PM75_MinRolls} / max: {this.DamageRollsConfig.TurretRolls.PM75_MaxRolls}");

            Mod.Log.LogDebug(" --- CUSTOM COMPONENTS CATEGORIES ---");
            Mod.Log.LogDebug($"  Blacklisted: {String.Join(", ", this.CustomComponentCategories.Blacklisted)}");

            Mod.Log.LogDebug(" --- PER HIT PENALTIES ---");
            Mod.Log.LogDebug($"  ArmorLoss =>  min: {this.PerHitPenalties.MinArmorLoss} max: {this.PerHitPenalties.MaxArmorLoss}");
            Mod.Log.LogDebug($"  StructureLoss =>  min: {this.PerHitPenalties.MinStructureLoss} max: {this.PerHitPenalties.MaxStructureLoss}");
            Mod.Log.LogDebug($"  SkillPenalty =>  min: {this.PerHitPenalties.MinSkillPenalty} max: {this.PerHitPenalties.MaxSkillPenalty}");

            Mod.Log.LogDebug(" --- THEMES ---");
            foreach (ThemeConfig theme in this.Themes)
            {
                Mod.Log.LogDebug($"  THEME: {theme.Label}");
                Mod.Log.LogDebug($"    MECH WEIGHTS: {String.Join(", ", theme.MechWeights)}");
                Mod.Log.LogDebug($"    VEHICLE WEIGHTS: {String.Join(", ", theme.VehicleWeights)}");
                Mod.Log.LogDebug($"    TURRET WEIGHTS: {String.Join(", ", theme.TurretWeights)}");
                Mod.Log.LogDebug($" ");
            }

            Mod.Log.LogDebug("=== MOD CONFIG END ===");
        }

        public void Init()
        {
            Mod.Log.LogDebug(" == Initializing Configuration");

            foreach (ThemeConfig themeConfig in Themes)
            {
                Mod.Log.LogDebug($" -- {themeConfig.Label} ");
                WeightToDamageType(themeConfig);
            }

            Mod.Log.LogDebug(" == Configuration Initialized");
        }

        // Translate the strings in the config to enum types
        private void WeightToDamageType(ThemeConfig theme)
        {

            for (int i = 0; i < ThemeConfig.MaxWeightItems; i++)
            {
                string mechDamageTypeId = theme.MechWeights[i];
                DamageType mechDamageType = (DamageType)Enum.Parse(typeof(DamageType), mechDamageTypeId);
                theme.MechTable[i] = mechDamageType;

                string vehicleDamageTypeId = theme.VehicleWeights[i];
                DamageType vehicleDamageType = (DamageType)Enum.Parse(typeof(DamageType), vehicleDamageTypeId);
                theme.VehicleTable[i] = vehicleDamageType;

                string turretDamageTypeId = theme.TurretWeights[i];
                DamageType turretDamageType = (DamageType)Enum.Parse(typeof(DamageType), turretDamageTypeId);
                theme.TurretTable[i] = turretDamageType;
            }

        }

    }
}
