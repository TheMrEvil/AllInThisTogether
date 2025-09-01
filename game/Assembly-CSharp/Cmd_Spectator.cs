using System;
using AtmosphericHeightFog;
using QFSW.QC;
using UnityEngine;

// Token: 0x0200003B RID: 59
[CommandPrefix("spectate.")]
public static class Cmd_Spectator
{
	// Token: 0x060001DF RID: 479 RVA: 0x00011493 File Offset: 0x0000F693
	[Command("toggle", Platform.AllPlatforms, MonoTargetType.Single)]
	[CommandDescription("Toggle Spectator Mode")]
	private static string ToggleSpectator()
	{
		if (PlayerControl.myInstance == null)
		{
			return "Player does not Exist!";
		}
		PlayerControl.myInstance.actions.ToggleSpectator();
		return "Spectator Mode: " + PlayerControl.myInstance.IsSpectator.ToString();
	}

	// Token: 0x060001E0 RID: 480 RVA: 0x000114D0 File Offset: 0x0000F6D0
	[Command("fog", Platform.AllPlatforms, MonoTargetType.Single)]
	[CommandDescription("Toggle Fog Visibility")]
	private static string ToggleFog()
	{
		HeightFogGlobal heightFogGlobal = UnityEngine.Object.FindObjectOfType<HeightFogGlobal>();
		if (heightFogGlobal == null)
		{
			Debug.LogError("Could not find Fog object");
		}
		heightFogGlobal.enabled = !heightFogGlobal.enabled;
		return "Fog Toggled: " + heightFogGlobal.enabled.ToString();
	}

	// Token: 0x060001E1 RID: 481 RVA: 0x00011520 File Offset: 0x0000F720
	[Command("horizontal-lock", Platform.AllPlatforms, MonoTargetType.Single)]
	[CommandDescription("Toggle Spectator Horizontal Movement")]
	private static string ToggleHorizontalLock()
	{
		if (PlayerControl.myInstance == null)
		{
			return "Player does not Exist!";
		}
		if (!PlayerControl.myInstance.IsSpectator)
		{
			return "Not in Spectator Mode";
		}
		return "Horizontal Lock: " + PlayerControl.myInstance.Display.SpectatorCam.ToggleHorizontalLock().ToString();
	}

	// Token: 0x060001E2 RID: 482 RVA: 0x00011578 File Offset: 0x0000F778
	[Command("fov", Platform.AllPlatforms, MonoTargetType.Single)]
	[CommandDescription("Set Spectator camera FOV")]
	private static string SetFoV(int fov = 61)
	{
		if (PlayerControl.myInstance == null)
		{
			return "Player does not Exist!";
		}
		if (!PlayerControl.myInstance.IsSpectator)
		{
			return "Not in Spectator Mode";
		}
		fov = Mathf.Clamp(fov, 10, 180);
		if (CameraFOV.instance == null)
		{
			return "No Camera to modify FOV";
		}
		CameraFOV.instance.SpectatorFOV = (float)fov;
		return "Spectator FOV set to " + fov.ToString();
	}

	// Token: 0x060001E3 RID: 483 RVA: 0x000115EC File Offset: 0x0000F7EC
	[Command("rot-speed", Platform.AllPlatforms, MonoTargetType.Single)]
	[CommandDescription("Set Spectator Rotation Speed")]
	private static string RotSpeed(int speed = 70)
	{
		if (PlayerControl.myInstance == null)
		{
			return "Player does not Exist!";
		}
		if (!PlayerControl.myInstance.IsSpectator)
		{
			return "Not in Spectator Mode";
		}
		speed = Mathf.Clamp(speed, 0, 250);
		PlayerControl.myInstance.Display.SpectatorCam.TurnSpeed = (float)speed;
		return "Spectator Rotation Speed: " + speed.ToString();
	}

	// Token: 0x060001E4 RID: 484 RVA: 0x00011654 File Offset: 0x0000F854
	[Command("fog-intensity", Platform.AllPlatforms, MonoTargetType.Single)]
	[CommandDescription("Set Game Fog Intensity Multiplier (0-1)")]
	private static string FogIntensity(float value = 1f)
	{
		HeightFogGlobal heightFogGlobal = UnityEngine.Object.FindObjectOfType<HeightFogGlobal>();
		if (heightFogGlobal == null)
		{
			Debug.LogError("Could not find Fog object");
		}
		heightFogGlobal.SetFogIntensity(value);
		return "Set Fog Intensity Multiplier: " + heightFogGlobal.IntensityMultiplier.ToString();
	}

	// Token: 0x060001E5 RID: 485 RVA: 0x00011698 File Offset: 0x0000F898
	[Command("vert-speed", Platform.AllPlatforms, MonoTargetType.Single)]
	[CommandDescription("Set Spectator Vertical Movement Speed")]
	private static string VertSpeed(int speed = 17)
	{
		if (PlayerControl.myInstance == null)
		{
			return "Player does not Exist!";
		}
		if (!PlayerControl.myInstance.IsSpectator)
		{
			return "Not in Spectator Mode";
		}
		speed = Mathf.Clamp(speed, 0, 100);
		PlayerControl.myInstance.Display.SpectatorCam.VerticalSpeed = (float)speed;
		return "Spectator Vertical Speed: " + speed.ToString();
	}

	// Token: 0x060001E6 RID: 486 RVA: 0x000116FC File Offset: 0x0000F8FC
	[Command("visible", Platform.AllPlatforms, MonoTargetType.Single)]
	[CommandDescription("Toggle Spectator Camera Visibility")]
	private static string SetVisible()
	{
		if (PlayerControl.myInstance == null)
		{
			return "Player does not Exist!";
		}
		if (!PlayerControl.myInstance.IsSpectator)
		{
			return "Not in Spectator Mode";
		}
		PlayerControl.myInstance.Display.ShowSpectatorCam = !PlayerControl.myInstance.Display.ShowSpectatorCam;
		return "Spectator Camera Visible: " + PlayerControl.myInstance.Display.ShowSpectatorCam.ToString();
	}

	// Token: 0x060001E7 RID: 487 RVA: 0x0001176D File Offset: 0x0000F96D
	[Command("map-decay", Platform.AllPlatforms, MonoTargetType.Single)]
	[CommandDescription("Set level of map decay")]
	private static string MapDecay(float progress = 0.5f)
	{
		TomeTerrain.ApplyLayer(progress);
		return "Map decay level set to " + progress.ToString();
	}

	// Token: 0x040001E5 RID: 485
	private const string NOT_SPECTATOR = "Not in Spectator Mode";

	// Token: 0x040001E6 RID: 486
	private const string NO_PLAYER = "Player does not Exist!";
}
