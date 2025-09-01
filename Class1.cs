using System;
using MelonLoader;
using HarmonyLib;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine;

namespace AllInThisTogether
{
    public class Class1 : MelonMod
    {
        private static readonly int UNLIMITED_PLAYERS = 250; // Photon's practical limit
        
        public override void OnInitializeMelon()
        {
            LoggerInstance.Msg("All In This Together mod loaded!");
            LoggerInstance.Msg("Player cap upped to 250");
        }
        
        // Patch the NetworkManager.CreateNewGame method - this is the main patch
        [HarmonyPatch(typeof(NetworkManager), "CreateNewGame")]
        private static class CreateNewGamePatch
        {
            private static void Prefix(ref RoomOptions options)
            {
                if (options != null)
                {
                    var originalMaxPlayers = options.MaxPlayers;
                    options.MaxPlayers = UNLIMITED_PLAYERS;
                    MelonLogger.Msg($"Player cap modified: {originalMaxPlayers} -> {options.MaxPlayers}");
                }
            }
        }
        
        // Patch room joining logic for logging - fix parameter name
        [HarmonyPatch(typeof(NetworkManager), "TryJoinRoom")]
        private static class TryJoinRoomPatch
        {
            private static void Prefix(string code)
            {
                MelonLogger.Msg($"Attempting to join room: {code} (unlimited players mod active)");
            }
        }
        
        // Replace CreatePublicRoom entirely to avoid IL transpiler issues
        [HarmonyPatch(typeof(MainPanel), "CreatePublicRoom")]
        private static class CreatePublicRoomPatch
        {
            private static bool Prefix(MainPanel __instance)
            {
                // Recreate the method with our unlimited players
                RoomOptions roomOptions = new RoomOptions();
                roomOptions.MaxPlayers = UNLIMITED_PLAYERS; // Instead of checking tutorial mode
                roomOptions.IsVisible = true;
                NetworkManager.instance.CreateNewGame(roomOptions);
                
                // Try to play the start sound effect
                try
                {
                    var audioManagerType = typeof(AudioManager);
                    var playMethod = audioManagerType.GetMethod("PlayInterfaceSFX", new Type[] { typeof(AudioClip), typeof(float), typeof(float) });
                    
                    // Get the StartSFX field from MainPanel
                    var startSFXField = typeof(MainPanel).GetField("StartSFX", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                    if (startSFXField != null && playMethod != null)
                    {
                        var startSFX = startSFXField.GetValue(__instance);
                        if (startSFX != null)
                        {
                            playMethod.Invoke(null, new object[] { startSFX, 1f, 0f });
                        }
                    }
                }
                catch (Exception ex)
                {
                    MelonLogger.Warning($"Could not play start SFX: {ex.Message}");
                }
                
                MelonLogger.Msg("Created public room with unlimited players!");
                return false; // Skip original method
            }
        }
        
        // Replace CreatePrivateRoom entirely to avoid IL transpiler issues
        [HarmonyPatch(typeof(MainPanel), "CreatePrivateRoom")]
        private static class CreatePrivateRoomPatch
        {
            private static bool Prefix(MainPanel __instance)
            {
                // Recreate the method with our unlimited players
                RoomOptions roomOptions = new RoomOptions();
                roomOptions.MaxPlayers = UNLIMITED_PLAYERS; // Instead of checking tutorial mode
                roomOptions.IsVisible = false;
                NetworkManager.instance.CreateNewGame(roomOptions);
                
                MelonLogger.Msg("Created private room with unlimited players!");
                return false; // Skip original method
            }
        }
        
        // Log when CreateOrJoinRandom is called
        [HarmonyPatch(typeof(MainPanel), "CreateOrJoinRandom")]
        private static class CreateOrJoinRandomPatch
        {
            private static void Postfix()
            {
                MelonLogger.Msg("CreateOrJoinRandom called - unlimited players mod active");
            }
        }
    }
}