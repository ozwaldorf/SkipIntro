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
