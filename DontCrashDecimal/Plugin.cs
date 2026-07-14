using ResoniteModLoader;
using Elements.Core;
using HarmonyLib;

namespace DontCrashDecimal;

public class DontCrashDecimal : ResoniteMod
{
    public override string Name => "DontCrashDecimal";
    public override string Author => "Gyztor Mizirath";
    public override string Version => "1.0.0";
    public override string Link => "https://github.com/Gyztor/DontCrashDecimal/";

    public override void OnEngineInit()
    {
        try
        {
            Harmony harmony = new Harmony("xyz.gyztormizirath.DontCrashDecimal");
            harmony.PatchAll();
            Debug("Dont Crash Decimal has Loaded!");
        } 
        catch (System.Exception ex)
        {
            Error($"Dont Crash Decimal failed to patch: {ex}");
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