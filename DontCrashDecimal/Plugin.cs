using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using BepInEx.NET.Common;
using BepInExResoniteShim;
//using BepisResoniteWrapper;
using Elements.Core;
using FrooxEngine;
using FrooxEngine.UIX;
using System;
using System.Collections.Generic;
using System.Linq;
using FrooxEngine.UIX;
using HarmonyLib;
using System.Reflection;
using System.Reflection.Emit;

namespace DontCrashDecimal;

[ResonitePlugin(PluginMetadata.GUID, PluginMetadata.NAME, PluginMetadata.VERSION, PluginMetadata.AUTHORS, PluginMetadata.REPOSITORY_URL)]
[BepInDependency(BepInExResoniteShim.PluginMetadata.GUID)]
public class DontCrashDecimal : BasePlugin
{
    internal new static ManualLogSource Log = null!;

    public override void Load()
    {
        Log = base.Log;

        try
        {

            HarmonyInstance.PatchAll();
            Log.LogInfo("Dont Crash Decimal has Loaded!");
        } 
        catch (System.Exception ex)
        {
            Log.LogError($"Dont Crash Decimal failed to patch: {ex}");
        }

    }

    [HarmonyPatchCategory(nameof(ValueModDecimal))]
    [HarmonyPatch(typeof(Coder<decimal>), nameof(Coder<Decimal>.Mod))]
    static class ValueModDecimal
    {
        [HarmonyPatch]
        private static bool Prefix(decimal b) => b != 0;
    }
}