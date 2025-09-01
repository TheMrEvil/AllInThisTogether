using System;
using cakeslice;
using FidelityFX;
using HorizonBasedAmbientOcclusion;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

// Token: 0x0200008B RID: 139
public class PlayerCamera : MonoBehaviour
{
	// Token: 0x060005E0 RID: 1504 RVA: 0x0002B737 File Offset: 0x00029937
	private void Awake()
	{
		if (TutorialManager.InTutorial && LibraryManager.instance != null)
		{
			this.Cam.enabled = false;
		}
	}

	// Token: 0x060005E1 RID: 1505 RVA: 0x0002B75C File Offset: 0x0002995C
	private void Start()
	{
		if (base.GetComponentInParent<PlayerControl>() == PlayerControl.myInstance)
		{
			PlayerCamera.myInstance = this;
		}
		this.SetAOQuality();
		this.SetAntiAliasing();
		this.SetDistance();
		Settings.OnSystemSettingsChanged = (Action<SystemSetting>)Delegate.Combine(Settings.OnSystemSettingsChanged, new Action<SystemSetting>(this.OnSettingsChanged));
		MapManager.SceneChanged = (Action)Delegate.Combine(MapManager.SceneChanged, new Action(this.SceneChanged));
	}

	// Token: 0x060005E2 RID: 1506 RVA: 0x0002B7D3 File Offset: 0x000299D3
	private void OnSettingsChanged(SystemSetting setting)
	{
		if (setting == SystemSetting.Post_AmbientOcclusion)
		{
			this.SetAOQuality();
		}
		if (setting == SystemSetting.Post_AntiAlias || setting == SystemSetting.Amd_FSR)
		{
			this.SetAntiAliasing();
		}
		if (setting == SystemSetting.CameraDistance)
		{
			this.SetDistance();
		}
	}

	// Token: 0x060005E3 RID: 1507 RVA: 0x0002B7FC File Offset: 0x000299FC
	private void SetAntiAliasing()
	{
		int @int = Settings.GetInt(SystemSetting.Post_AntiAlias, 1);
		int int2 = Settings.GetInt(SystemSetting.Amd_FSR, 1);
		if (int2 != 0)
		{
			CanvasController.instance.UICamera.rect = new Rect(0f, 0f, 1.001f, 1f);
			this.PostLayer.antialiasingMode = PostProcessLayer.Antialiasing.FSR3;
			FSR3 fsr = this.PostLayer.fsr3;
			Fsr3.QualityMode qualityMode;
			switch (int2)
			{
			case 1:
				qualityMode = Fsr3.QualityMode.Quality;
				break;
			case 2:
				qualityMode = Fsr3.QualityMode.Balanced;
				break;
			case 3:
				qualityMode = Fsr3.QualityMode.Performance;
				break;
			case 4:
				qualityMode = Fsr3.QualityMode.UltraPerformance;
				break;
			default:
				qualityMode = Fsr3.QualityMode.NativeAA;
				break;
			}
			fsr.qualityMode = qualityMode;
			return;
		}
		CanvasController.instance.UICamera.rect = new Rect(0f, 0f, 1f, 1f);
		switch (@int)
		{
		case 0:
			this.PostLayer.antialiasingMode = PostProcessLayer.Antialiasing.None;
			return;
		case 1:
			this.PostLayer.antialiasingMode = PostProcessLayer.Antialiasing.FastApproximateAntialiasing;
			this.PostLayer.fastApproximateAntialiasing.fastMode = true;
			return;
		case 2:
			this.PostLayer.antialiasingMode = PostProcessLayer.Antialiasing.FastApproximateAntialiasing;
			this.PostLayer.fastApproximateAntialiasing.fastMode = false;
			return;
		case 3:
			this.PostLayer.antialiasingMode = PostProcessLayer.Antialiasing.SubpixelMorphologicalAntialiasing;
			this.PostLayer.subpixelMorphologicalAntialiasing.quality = SubpixelMorphologicalAntialiasing.Quality.Low;
			return;
		case 4:
			this.PostLayer.antialiasingMode = PostProcessLayer.Antialiasing.SubpixelMorphologicalAntialiasing;
			this.PostLayer.subpixelMorphologicalAntialiasing.quality = SubpixelMorphologicalAntialiasing.Quality.High;
			return;
		case 5:
			this.PostLayer.antialiasingMode = PostProcessLayer.Antialiasing.TemporalAntialiasing;
			return;
		default:
			return;
		}
	}

	// Token: 0x060005E4 RID: 1508 RVA: 0x0002B970 File Offset: 0x00029B70
	private void SetAOQuality()
	{
		PostFXSetting @int = (PostFXSetting)Settings.GetInt(SystemSetting.Post_AmbientOcclusion, 2);
		if (@int == PostFXSetting.Off)
		{
			this.AORef.enabled = false;
			return;
		}
		this.AORef.enabled = true;
		HBAO aoref = this.AORef;
		HBAO.Preset preset;
		switch (@int)
		{
		case PostFXSetting.Low:
			preset = HBAO.Preset.FastestPerformance;
			break;
		case PostFXSetting.Medium:
			preset = HBAO.Preset.FastPerformance;
			break;
		case PostFXSetting.High:
			preset = HBAO.Preset.Normal;
			break;
		case PostFXSetting.Max:
			preset = HBAO.Preset.HighQuality;
			break;
		default:
			preset = HBAO.Preset.FastestPerformance;
			break;
		}
		aoref.ApplyPreset(preset);
	}

	// Token: 0x060005E5 RID: 1509 RVA: 0x0002B9DC File Offset: 0x00029BDC
	private void SceneChanged()
	{
		Camera worldUICam = this.WorldUICam;
		float depth = worldUICam.depth;
		worldUICam.depth = depth + 1f;
		if (this.WorldUICam.depth > 10f)
		{
			this.WorldUICam.depth = 2f;
		}
	}

	// Token: 0x060005E6 RID: 1510 RVA: 0x0002BA24 File Offset: 0x00029C24
	private void SetDistance()
	{
		float @float = Settings.GetFloat(SystemSetting.CameraDistance, 5f);
		float x = Mathf.Max(0.75f, 0.15f * @float);
		float y = Mathf.Max(0.8f, 0.16f * @float);
		float z = -@float;
		this.CameraOffset.localPosition = new Vector3(x, y, z);
	}

	// Token: 0x060005E7 RID: 1511 RVA: 0x0002BA78 File Offset: 0x00029C78
	private void OnDestroy()
	{
		Settings.OnSystemSettingsChanged = (Action<SystemSetting>)Delegate.Remove(Settings.OnSystemSettingsChanged, new Action<SystemSetting>(this.OnSettingsChanged));
		MapManager.SceneChanged = (Action)Delegate.Remove(MapManager.SceneChanged, new Action(this.SceneChanged));
	}

	// Token: 0x060005E8 RID: 1512 RVA: 0x0002BAC5 File Offset: 0x00029CC5
	public PlayerCamera()
	{
	}

	// Token: 0x040004EB RID: 1259
	public static PlayerCamera myInstance;

	// Token: 0x040004EC RID: 1260
	public Camera WorldUICam;

	// Token: 0x040004ED RID: 1261
	public Camera Cam;

	// Token: 0x040004EE RID: 1262
	public HBAO AORef;

	// Token: 0x040004EF RID: 1263
	public OutlineEffect Outline;

	// Token: 0x040004F0 RID: 1264
	public PostProcessLayer PostLayer;

	// Token: 0x040004F1 RID: 1265
	public Transform CameraOffset;
}
