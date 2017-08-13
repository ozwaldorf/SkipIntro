using System;
using UnityEngine;
using Harmony;

namespace SkipIntro {
    internal class SkipIntro {
        public static void OnLoad() {
            MoviePlayer.m_HasIntroPlayedForMainMenu = true;
        }
    }
}

[HarmonyPatch(typeof(Panel_MainMenu))]
[HarmonyPatch("UpdateFading")]
public class FadePatch {
    public static void Postfix(Panel_MainMenu __instance) {
        __instance.m_InitialScreenFadeInDuration = 0f;
    }
}

[HarmonyPatch(typeof(ConsoleManager))]
[HarmonyPatch("RegisterCommands")]
class AddConsoleCommands {
    static void Postfix() {
        uConsole.RegisterCommand("gun", new uConsole.DebugCommand(SpawnGun));
        uConsole.RegisterCommand("plane", new uConsole.DebugCommand(SpawnPlane));
    }
    private static void SpawnGun() {
        GameObject ar15 = UnityEngine.Object.Instantiate<GameObject>(Resources.Load("GEAR_Rifle") as GameObject);
        GunItem ar15Gun = ar15.GetComponent<GunItem>();
        GearItem ar15Gear = ar15.GetComponent<GearItem>();
        ar15Gun.m_ClipSize = 5;
        ar15Gun.m_FiringRateSeconds = 0;
        ar15Gun.m_FireDelayOnAim = 0;
        ar15Gun.m_FireDelayAfterReload = 0;
        ar15Gear.name = "ar15";
        Transform player = GameManager.GetVpFPSCamera().transform;
        ar15.transform.position = player.position;
    }

    private static void SpawnPlane() {
        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Plane); 
        Transform player = GameManager.GetVpFPSCamera().transform;
        go.transform.position = player.position;
    }
}

[HarmonyPatch(typeof(PlayerAnimation))]
[HarmonyPatch("Trigger_Generic_Fire")]
class ShootPatch {
    static bool Prefix() {
        GearItem itemInHands = GameManager.GetPlayerManagerComponent().m_ItemInHands;
        if (itemInHands.name == "ar15") {
            return false;
        } else {
            return true;
        }
    }
}

/*
[HarmonyPatch(typeof(vp_Bullet))]
[HarmonyPatch("Start")]
class BulletPatch {
    static void Prefix() {
        GameManager.GetCameraEffects().ElectrocutionPulse(1f);
    }
}
*/