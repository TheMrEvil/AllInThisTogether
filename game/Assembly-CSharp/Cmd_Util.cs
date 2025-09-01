using System;
using QFSW.QC;

// Token: 0x0200003D RID: 61
public static class Cmd_Util
{
	// Token: 0x060001F6 RID: 502 RVA: 0x00011906 File Offset: 0x0000FB06
	[Command("fps", Platform.AllPlatforms, MonoTargetType.Single)]
	[CommandDescription("Toggle FPS Display")]
	private static string ToggleFPS()
	{
		FPSCounter.ShowFPS = !FPSCounter.ShowFPS;
		return "FPS Counter Display: " + FPSCounter.ShowFPS.ToString();
	}

	// Token: 0x060001F7 RID: 503 RVA: 0x00011929 File Offset: 0x0000FB29
	[Command("hud", Platform.AllPlatforms, MonoTargetType.Single)]
	[CommandDescription("Toggles HUD Display")]
	private static string ToggleHUD()
	{
		GameHUD.instance.CycleHudMode();
		return "HUD Mode: " + GameHUD.Mode.ToString();
	}

	// Token: 0x060001F8 RID: 504 RVA: 0x0001194F File Offset: 0x0000FB4F
	[Command("reset-keybinds", Platform.AllPlatforms, MonoTargetType.Single)]
	[CommandDescription("Toggles HUD Display")]
	private static string ResetKeybinds()
	{
		InputManager.ResetBindings();
		return "Keybindings Reset";
	}

	// Token: 0x060001F9 RID: 505 RVA: 0x0001195C File Offset: 0x0000FB5C
	[Command("launch-options", Platform.AllPlatforms, MonoTargetType.Single)]
	[CommandDescription("Print the Launch Options passed to the game when opening")]
	private static string LaunchOptions()
	{
		string text = "";
		foreach (string str in Environment.GetCommandLineArgs())
		{
			text = text + str + " ";
		}
		return text;
	}
}
