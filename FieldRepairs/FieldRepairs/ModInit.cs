using HBS.Logging;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Reflection;

namespace FieldRepairs
{

    public static class Mod
    {
        
        public const string HarmonyPackage = "us.frostraptor.FieldRepairs";
        public const string LogName = "field_repairs";
        public const string LogLabel = "FLDREPAIR";

        public static ILog Log;
        public static string ModDir;
        public static ModConfig Config;

        public static readonly Random Random = new Random();

        public static void Init(string modDirectory, string settingsJSON)
        {
            ModDir = modDirectory;
            Log = Logger.GetLogger("FieldRepairs");


            Exception settingsE = null;
            try
            {
                Mod.Config = JsonConvert.DeserializeObject<ModConfig>(settingsJSON);
            }
            catch (Exception e)
            {
                settingsE = e;
                Mod.Config = new ModConfig();
            }

            Assembly asm = Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(asm.Location);
            Log.LogDebug($"Assembly version: {fvi.FileVersion}");

            // Initialize the mod settings
            Mod.Config.Init();

            Log.LogDebug($"ModDir is:{modDirectory}");
            Log.LogDebug($"mod.json settings are:({settingsJSON})");
            Mod.Config.LogConfig();

            if (settingsE != null)
            {
                Log.LogDebug($"ERROR reading settings file! Error was: {settingsE}");
            }
            else
            {
                Log.LogDebug($"INFO: No errors reading settings file.");
            }

            // Initialize modules
            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), HarmonyPackage);

        }

    }
}
