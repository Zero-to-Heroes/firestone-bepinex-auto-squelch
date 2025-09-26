using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System;

namespace FirestoneAutoSquelch;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class Plugin : BaseUnityPlugin
{
    internal static new ManualLogSource Logger;
        
    private void Awake()
    {
        // Plugin startup logic
        Logger = base.Logger;  
        Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");

        var harmony = new Harmony("com.firestoneapp.FirestoneAutoSquelch");
        harmony.PatchAll();
        //Harmony.CreateAndPatchAll(typeof(SquelchPatcher));
        Logger.LogInfo($"Patched harmony");
    }
}

[HarmonyPatch(typeof(EnemyEmoteHandler), "IsSquelched", new Type[] { typeof(int) })]
public static class SquelchPatcher
{
    [HarmonyPrefix]
    public static bool Prefix(ref bool __result)
    {
        Plugin.Logger.LogInfo($"Patching IsSquelched: {__result}");
        __result = true;
        Plugin.Logger.LogInfo($"Returning instead: {__result}");
        return false;
    }
}