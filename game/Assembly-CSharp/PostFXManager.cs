using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using SCPE;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

// Token: 0x02000258 RID: 600
public class PostFXManager : MonoBehaviour
{
	// Token: 0x06001817 RID: 6167 RVA: 0x00096FB4 File Offset: 0x000951B4
	private void Awake()
	{
		if (PostFXManager.instance != null)
		{
			UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
		PostFXManager.instance = this;
		this.selfVolume = base.GetComponent<PostProcessVolume>();
		MapManager.SceneChanged = (Action)Delegate.Combine(MapManager.SceneChanged, new Action(this.LoadSceneVolume));
		MapManager.SceneChanged = (Action)Delegate.Combine(MapManager.SceneChanged, new Action(this.ClearTempEffects));
		Settings.OnSystemSettingsChanged = (Action<SystemSetting>)Delegate.Combine(Settings.OnSystemSettingsChanged, new Action<SystemSetting>(this.OnSettingsChanged));
		this.LoadSceneVolume();
		this.BlurVolume.gameObject.SetActive(false);
		this.ResetSketchValues();
	}

	// Token: 0x06001818 RID: 6168 RVA: 0x00097069 File Offset: 0x00095269
	private void Start()
	{
		PanelManager panelManager = PanelManager.instance;
		panelManager.OnPanelChanged = (Action<PanelType, PanelType>)Delegate.Combine(panelManager.OnPanelChanged, new Action<PanelType, PanelType>(this.OnPanelChanged));
	}

	// Token: 0x06001819 RID: 6169 RVA: 0x00097094 File Offset: 0x00095294
	public void CreateTempFX(PostProcessNode node, EffectProperties props)
	{
		if (this.nodeProfiles.ContainsKey(node.guid))
		{
			this.nodeProfiles[node.guid].UpdateValues(node, props);
			return;
		}
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.TempfxRef, base.transform);
		gameObject.SetActive(true);
		TempFXProfile component = gameObject.GetComponent<TempFXProfile>();
		component.Setup(node, props);
		this.nodeProfiles.Add(node.guid, component);
	}

	// Token: 0x0600181A RID: 6170 RVA: 0x00097108 File Offset: 0x00095308
	public void ReleaseTempFX(PostProcessNode node)
	{
		if (this.nodeProfiles.ContainsKey(node.guid))
		{
			TempFXProfile tempFXProfile = this.nodeProfiles[node.guid];
			if (tempFXProfile != null)
			{
				tempFXProfile.Release();
			}
			this.nodeProfiles.Remove(node.guid);
		}
	}

	// Token: 0x0600181B RID: 6171 RVA: 0x00097158 File Offset: 0x00095358
	public void ReleaseTempFX(TempFXProfile profile)
	{
		string text = null;
		foreach (KeyValuePair<string, TempFXProfile> keyValuePair in this.nodeProfiles)
		{
			if (keyValuePair.Value == profile)
			{
				text = keyValuePair.Key;
			}
		}
		if (text != null)
		{
			this.nodeProfiles.Remove(text);
		}
	}

	// Token: 0x0600181C RID: 6172 RVA: 0x000971D0 File Offset: 0x000953D0
	public void ClearTempEffects()
	{
		foreach (KeyValuePair<string, TempFXProfile> keyValuePair in this.nodeProfiles)
		{
			keyValuePair.Value.Release();
		}
		this.nodeProfiles.Clear();
	}

	// Token: 0x0600181D RID: 6173 RVA: 0x00097234 File Offset: 0x00095434
	private void LoadSceneVolume()
	{
		if (Scene_Settings.instance == null)
		{
			return;
		}
		this.selfVolume.profile = Scene_Settings.instance.ScenePostFX;
		this.UpdatePostSettings();
		if (MapManager.InLobbyScene)
		{
			this.ResetSketchValues();
		}
	}

	// Token: 0x0600181E RID: 6174 RVA: 0x0009726C File Offset: 0x0009546C
	private void Update()
	{
		bool flag = PlayerControl.myInstance != null && PlayerControl.myInstance.IsDead;
		this.DeathVolume.gameObject.SetActive(flag || this.DeathVolume.weight > 0f);
		this.DeathVolume.UpdateOpacity(flag, 1f, 1f);
		bool flag2 = PlayerControl.myInstance != null && PlayerControl.myInstance.Health.LowHealthDanger;
		this.LowHealthVolume.gameObject.SetActive(flag2 || this.LowHealthVolume.weight > 0f);
		this.LowHealthVolume.UpdateOpacity(flag2, 4f, this.dangerIntensityLimit);
		this.HurtVolume.UpdateOpacity(false, 1.5f, 1f);
		if (this.HurtVolume.weight <= 0f && this.HurtVolume.gameObject.activeSelf)
		{
			this.HurtVolume.gameObject.SetActive(false);
		}
		this.UpdateGreyscale();
		if (PlayerControl.myInstance == null || PlayerControl.myInstance.health.shieldDelay < 3.75f || PlayerControl.myInstance.IsDead)
		{
			this.ShieldBreakVolume.UpdateOpacity(false, 1f, 1f);
			if (this.ShieldBreakVolume.weight <= 0f && this.ShieldBreakVolume.gameObject.activeSelf)
			{
				this.ShieldBreakVolume.gameObject.SetActive(false);
			}
		}
	}

	// Token: 0x0600181F RID: 6175 RVA: 0x000973FC File Offset: 0x000955FC
	public void ShieldBreak()
	{
		this.ShieldBreakVolume.gameObject.SetActive(true);
		this.ShieldBreakVolume.weight = 1f;
	}

	// Token: 0x06001820 RID: 6176 RVA: 0x00097420 File Offset: 0x00095620
	public void SetBWSketch(float backingIntensity)
	{
		EdgeDetection setting = PostFXManager.instance.SketchVolume.profile.GetSetting<EdgeDetection>();
		Overlay setting2 = PostFXManager.instance.SketchVolume.profile.GetSetting<Overlay>();
		setting.backgroundIntensity.value = backingIntensity;
		setting2.overlayTex.value = this.SketchOverlayWhite;
	}

	// Token: 0x06001821 RID: 6177 RVA: 0x00097474 File Offset: 0x00095674
	public void ResetSketchValues()
	{
		EdgeDetection setting = PostFXManager.instance.SketchVolume.profile.GetSetting<EdgeDetection>();
		Overlay setting2 = PostFXManager.instance.SketchVolume.profile.GetSetting<Overlay>();
		setting.backgroundIntensity.value = 1f;
		setting2.overlayTex.value = this.SketchOverlayParchment;
	}

	// Token: 0x06001822 RID: 6178 RVA: 0x000974CA File Offset: 0x000956CA
	public void ActivateSketch(bool useSFX = true, bool save = true)
	{
		if (this.IsInSketch)
		{
			return;
		}
		if (useSFX)
		{
			PageFlip.instance.PlaySketchIntro();
		}
		this.IsInSketch = true;
		base.StartCoroutine("FadeInSketch", save);
	}

	// Token: 0x06001823 RID: 6179 RVA: 0x000974FB File Offset: 0x000956FB
	public void ReleaseSketch()
	{
		if (!this.IsInSketch)
		{
			return;
		}
		base.StartCoroutine("FadeOutSketch");
	}

	// Token: 0x06001824 RID: 6180 RVA: 0x00097512 File Offset: 0x00095712
	public void CompleteSketch()
	{
		PageFlip.instance.Curl();
		this.ClearTempEffects();
	}

	// Token: 0x06001825 RID: 6181 RVA: 0x00097524 File Offset: 0x00095724
	private IEnumerator FadeInSketch(bool save)
	{
		float t = 0f;
		while (t < 1f)
		{
			yield return true;
			t += Time.deltaTime / this.SketchTransitionDuration;
			PostFXManager.UpdateSketch(t);
			if (PlayerControl.myInstance == null)
			{
				base.StartCoroutine("FadeOutSketch");
				yield break;
			}
		}
		if (save)
		{
			PageFlip.instance.Save();
		}
		yield break;
	}

	// Token: 0x06001826 RID: 6182 RVA: 0x0009753A File Offset: 0x0009573A
	private IEnumerator FadeOutSketch()
	{
		float t = 0f;
		while (t < 1f)
		{
			yield return true;
			t += Time.deltaTime / (this.SketchTransitionDuration / 1.5f);
			PostFXManager.UpdateSketch(1f - t);
		}
		Action onMapChangeFinished = MapManager.OnMapChangeFinished;
		if (onMapChangeFinished != null)
		{
			onMapChangeFinished();
		}
		PostFXManager.instance.SketchVolume.gameObject.SetActive(false);
		this.IsInSketch = false;
		yield break;
	}

	// Token: 0x06001827 RID: 6183 RVA: 0x00097549 File Offset: 0x00095749
	public void DoPulse()
	{
		base.StopAllCoroutines();
		base.StartCoroutine("SketchPulse");
	}

	// Token: 0x06001828 RID: 6184 RVA: 0x0009755D File Offset: 0x0009575D
	private IEnumerator SketchPulse()
	{
		Shader.SetGlobalVector("_FountainLoc", PlayerControl.myInstance.display.CenterOfMass.position);
		this.SketchVolume.gameObject.SetActive(true);
		this.SketchVolume.weight = 1f;
		EdgeDetection edge = PostFXManager.instance.SketchVolume.profile.GetSetting<EdgeDetection>();
		Overlay overlay = PostFXManager.instance.SketchVolume.profile.GetSetting<Overlay>();
		edge.edgeDistMin.value = 0f;
		overlay.distanceMin.value = 0f;
		edge.edgeDistance.value = 0f;
		overlay.distance.value = 0f;
		float t = 0f;
		while (t < 1f)
		{
			yield return true;
			t += Time.deltaTime / (this.PulseDuration / 1.5f);
			float num = PostFXManager.instance.PulseCurve.Evaluate(t) * 245f;
			num -= this.PulseWidth;
			num += this.PulseMinDist;
			edge.edgeDistMin.value = Mathf.Max(this.PulseMinDist, num);
			overlay.distanceMin.value = Mathf.Max(this.PulseMinDist - 5f, num - 5f);
			edge.edgeDistance.value = Mathf.Max(this.PulseMinDist, num + this.PulseWidth);
			overlay.distance.value = Mathf.Max(this.PulseMinDist - 5f, num + this.PulseWidth + 5f);
		}
		t = 0f;
		while (t < 1f)
		{
			yield return true;
			t += Time.deltaTime;
			this.SketchVolume.weight = 1f - t;
		}
		this.SketchVolume.gameObject.SetActive(false);
		Shader.SetGlobalVector("_FountainLoc", Fountain.instance.transform.position);
		yield break;
	}

	// Token: 0x06001829 RID: 6185 RVA: 0x0009756C File Offset: 0x0009576C
	public static void UpdateSketch(float val)
	{
		if (PostFXManager.instance == null)
		{
			return;
		}
		PostFXManager.instance.SketchVolume.gameObject.SetActive(true);
		PostFXManager.instance.SketchVolume.weight = (float)((val > 0f) ? 1 : 0);
		float num = PostFXManager.instance.SketchFlow.Evaluate(val);
		EdgeDetection setting = PostFXManager.instance.SketchVolume.profile.GetSetting<EdgeDetection>();
		Overlay setting2 = PostFXManager.instance.SketchVolume.profile.GetSetting<Overlay>();
		setting.edgeDistMin.value = 0f;
		setting2.distanceMin.value = 0f;
		setting.edgeDistance.value = num * 250f;
		setting2.distance.value = num * 250f + 10f;
	}

	// Token: 0x0600182A RID: 6186 RVA: 0x0009763C File Offset: 0x0009583C
	private void UpdateGreyscale()
	{
		bool flag = (GameplayManager.instance != null && GameplayManager.instance.CurrentState == GameState.Hub_Bindings) || UITransitionHelper.InWavePrelim() || Splashscreen.WantGreyscale;
		this.DesaturateVolume.gameObject.SetActive(flag || this.DesaturateVolume.weight > 0f);
		float speed = 0.3f;
		if (MapManager.InLobbyScene)
		{
			speed = 3f;
		}
		this.DesaturateVolume.UpdateOpacity(flag, speed, 1f);
	}

	// Token: 0x0600182B RID: 6187 RVA: 0x000976C1 File Offset: 0x000958C1
	private void TestGreyscalePulse()
	{
		this.GreyscalePulse(Vector3.zero);
	}

	// Token: 0x0600182C RID: 6188 RVA: 0x000976CE File Offset: 0x000958CE
	public void GreyscalePulse(Vector3 pos)
	{
		if (GreyscaleAreas.Zones.Count > 0)
		{
			return;
		}
		base.StartCoroutine("PulseSequence", pos);
	}

	// Token: 0x0600182D RID: 6189 RVA: 0x000976F0 File Offset: 0x000958F0
	private IEnumerator PulseSequence(Vector3 pos)
	{
		GreyscaleAreas.GSZone zone = GreyscaleAreas.AddZone(pos, 0f);
		float t = 0f;
		while (t < 1f)
		{
			yield return true;
			t += Time.deltaTime / (this.GreyPulseDuration / 1.5f);
			float num = this.GreyPulseCurve.Evaluate(t) * 250f;
			float r = num;
			float innerRadius = Mathf.Max(0f, num - this.GreyPulseWidth);
			zone.Update(pos, r);
			GreyscaleAreas.UpdateZones(innerRadius);
		}
		GreyscaleAreas.RemoveZone(zone);
		GreyscaleAreas.UpdateZones(0f);
		yield break;
	}

	// Token: 0x0600182E RID: 6190 RVA: 0x00097708 File Offset: 0x00095908
	public static void DamgageFX(float damageAmount)
	{
		if (PostFXManager.instance != null)
		{
			PostFXManager.instance.HurtVolume.gameObject.SetActive(true);
			PostFXManager.instance.HurtVolume.weight = (0.5f + 0.5f * Mathf.Lerp(0f, 1f, damageAmount / 15f)) * PostFXManager.instance.dangerIntensityLimit;
		}
	}

	// Token: 0x0600182F RID: 6191 RVA: 0x00097774 File Offset: 0x00095974
	private void OnSettingsChanged(SystemSetting setting)
	{
		switch (setting)
		{
		case SystemSetting.Post_Sunshafts:
			this.UpdatePostSettings();
			return;
		case SystemSetting.Post_Vignette:
			this.UpdatePostSettings();
			return;
		case SystemSetting.Post_EdgeDetection:
			this.UpdatePostSettings();
			return;
		case SystemSetting.Post_Posterize:
			this.UpdatePostSettings();
			return;
		case SystemSetting.Post_AntiAlias:
			break;
		case SystemSetting.Post_Bloom:
			this.UpdatePostSettings();
			return;
		default:
			if (setting != SystemSetting.DangerIntensity)
			{
				return;
			}
			this.dangerIntensityLimit = Settings.GetFloat(SystemSetting.DangerIntensity, 0f);
			break;
		}
	}

	// Token: 0x06001830 RID: 6192 RVA: 0x000977E0 File Offset: 0x000959E0
	private void UpdatePostSettings()
	{
		PostFXSetting @int = (PostFXSetting)Settings.GetInt(SystemSetting.Post_EdgeDetection, 2);
		EdgeDetection setting = this.selfVolume.profile.GetSetting<EdgeDetection>();
		if (setting != null)
		{
			if (@int == PostFXSetting.Off)
			{
				setting.enabled.value = false;
			}
			else
			{
				setting.enabled.value = true;
				setting.edgeSize.value = 1;
			}
		}
		@int = (PostFXSetting)Settings.GetInt(SystemSetting.Post_Sunshafts, 2);
		Sunshafts setting2 = this.selfVolume.profile.GetSetting<Sunshafts>();
		if (setting2 != null)
		{
			if (@int == PostFXSetting.Off)
			{
				setting2.enabled.value = false;
			}
			else
			{
				setting2.enabled.value = true;
				Sunshafts.SunShaftsResolutionParameter resolution = setting2.resolution;
				SunshaftsBase.SunShaftsResolution value;
				switch (@int)
				{
				case PostFXSetting.Low:
					value = SunshaftsBase.SunShaftsResolution.Low;
					break;
				case PostFXSetting.Medium:
					value = SunshaftsBase.SunShaftsResolution.Normal;
					break;
				case PostFXSetting.High:
					value = SunshaftsBase.SunShaftsResolution.High;
					break;
				case PostFXSetting.Max:
					value = SunshaftsBase.SunShaftsResolution.High;
					break;
				default:
					value = SunshaftsBase.SunShaftsResolution.Low;
					break;
				}
				resolution.value = value;
				setting2.highQuality.value = (@int == PostFXSetting.Max);
			}
		}
		@int = (PostFXSetting)Settings.GetInt(SystemSetting.Post_Posterize, 1);
		Posterize setting3 = this.selfVolume.profile.GetSetting<Posterize>();
		if (setting3 != null)
		{
			if (@int == PostFXSetting.Off)
			{
				setting3.enabled.value = false;
			}
			else
			{
				setting3.enabled.value = true;
			}
		}
		@int = (PostFXSetting)Settings.GetInt(SystemSetting.Post_Bloom, 2);
		Bloom setting4 = this.BloomVolume.profile.GetSetting<Bloom>();
		if (setting4 != null)
		{
			if (@int == PostFXSetting.Off)
			{
				setting4.enabled.value = false;
				return;
			}
			setting4.enabled.value = true;
			setting4.fastMode.value = (@int == PostFXSetting.Low);
		}
	}

	// Token: 0x06001831 RID: 6193 RVA: 0x00097960 File Offset: 0x00095B60
	private void OnPanelChanged(PanelType from, PanelType to)
	{
		PanelManager.instance.GetPanel(from);
		UIPanel panel = PanelManager.instance.GetPanel(to);
		if (from == PanelType.GameInvisible && this.ShouldBlur(panel))
		{
			this.EnterBlur();
			return;
		}
		this.ExitBlur();
	}

	// Token: 0x06001832 RID: 6194 RVA: 0x0009799F File Offset: 0x00095B9F
	private bool ShouldBlur(UIPanel panel)
	{
		return !(panel == null) && (panel.panelType == PanelType.BossReward || (panel.gameplayInteractable && (!panel.NoBook || panel.UseBlur) && panel.panelType != PanelType.Genre_Selection));
	}

	// Token: 0x06001833 RID: 6195 RVA: 0x000979DF File Offset: 0x00095BDF
	private void EnterBlur()
	{
		if (this.isBlurred)
		{
			return;
		}
		this.isBlurred = true;
		if (this.blurRoutine != null)
		{
			base.StopCoroutine(this.blurRoutine);
		}
		this.blurRoutine = base.StartCoroutine("BlurSeq");
	}

	// Token: 0x06001834 RID: 6196 RVA: 0x00097A16 File Offset: 0x00095C16
	public void ExitBlur()
	{
		if (!this.isBlurred)
		{
			return;
		}
		this.isBlurred = false;
		if (this.blurRoutine != null)
		{
			base.StopCoroutine(this.blurRoutine);
		}
		this.blurRoutine = base.StartCoroutine("UnblurSeq");
	}

	// Token: 0x06001835 RID: 6197 RVA: 0x00097A4D File Offset: 0x00095C4D
	private IEnumerator BlurSeq()
	{
		DepthOfField blur = this.BlurVolume.profile.GetSetting<DepthOfField>();
		Kuwahara kwh = this.BlurVolume.profile.GetSetting<Kuwahara>();
		this.BlurVolume.gameObject.SetActive(true);
		this.BlurVolume.weight = 1f;
		if (PanelManager.CurPanel == PanelType.Cosmetics)
		{
			kwh.startFadeDistance.value = 2.5f;
			kwh.endFadeDistance.value = 3.5f;
			blur.focusDistance.value = 2f;
		}
		else
		{
			blur.focusDistance.value = PanelManager.curSelect.BookOffset.z;
			kwh.startFadeDistance.value = PanelManager.curSelect.BookOffset.z - 0.05f;
			kwh.endFadeDistance.value = PanelManager.curSelect.BookOffset.z + 0.05f;
		}
		int wantBlur = 30;
		int wantK = 8;
		float i = (float)kwh.radius;
		float f = blur.focalLength;
		while (f < (float)wantBlur - 0.05f)
		{
			f = Mathf.Lerp(f, (float)wantBlur, Time.deltaTime * 8f);
			i = Mathf.Lerp(i, (float)wantK, Time.deltaTime * 8f);
			blur.focalLength.value = f;
			kwh.radius.value = (int)i;
			yield return true;
		}
		blur.focalLength.value = (float)wantBlur;
		kwh.radius.value = wantK;
		yield break;
	}

	// Token: 0x06001836 RID: 6198 RVA: 0x00097A5C File Offset: 0x00095C5C
	private IEnumerator UnblurSeq()
	{
		DepthOfField blur = this.BlurVolume.profile.GetSetting<DepthOfField>();
		Kuwahara kwh = this.BlurVolume.profile.GetSetting<Kuwahara>();
		float i = (float)kwh.radius;
		float f = blur.focalLength;
		while (i > 0.05f)
		{
			f = Mathf.Lerp(f, 1f, Time.deltaTime * 8f);
			i = Mathf.Lerp(i, 0f, Time.deltaTime * 8f);
			blur.focalLength.value = f;
			kwh.radius.value = (int)i;
			yield return true;
		}
		this.BlurVolume.weight = 0f;
		this.BlurVolume.gameObject.SetActive(false);
		yield break;
	}

	// Token: 0x06001837 RID: 6199 RVA: 0x00097A6C File Offset: 0x00095C6C
	private void OnDestroy()
	{
		MapManager.SceneChanged = (Action)Delegate.Remove(MapManager.SceneChanged, new Action(this.LoadSceneVolume));
		MapManager.SceneChanged = (Action)Delegate.Remove(MapManager.SceneChanged, new Action(this.ClearTempEffects));
	}

	// Token: 0x06001838 RID: 6200 RVA: 0x00097ABC File Offset: 0x00095CBC
	public PostFXManager()
	{
	}

	// Token: 0x040017E8 RID: 6120
	public static PostFXManager instance;

	// Token: 0x040017E9 RID: 6121
	private PostProcessVolume selfVolume;

	// Token: 0x040017EA RID: 6122
	public PostProcessVolume LowHealthVolume;

	// Token: 0x040017EB RID: 6123
	public PostProcessVolume DeathVolume;

	// Token: 0x040017EC RID: 6124
	public PostProcessVolume HurtVolume;

	// Token: 0x040017ED RID: 6125
	public PostProcessVolume DesaturateVolume;

	// Token: 0x040017EE RID: 6126
	public PostProcessVolume ShieldBreakVolume;

	// Token: 0x040017EF RID: 6127
	public PostProcessVolume BlurVolume;

	// Token: 0x040017F0 RID: 6128
	public PostProcessVolume BloomVolume;

	// Token: 0x040017F1 RID: 6129
	public Texture2D SketchOverlayParchment;

	// Token: 0x040017F2 RID: 6130
	public Texture2D SketchOverlayWhite;

	// Token: 0x040017F3 RID: 6131
	public PostProcessVolume SketchVolume;

	// Token: 0x040017F4 RID: 6132
	public float SketchTransitionDuration = 3f;

	// Token: 0x040017F5 RID: 6133
	public AnimationCurve SketchFlow;

	// Token: 0x040017F6 RID: 6134
	public float PulseDuration = 1f;

	// Token: 0x040017F7 RID: 6135
	public AnimationCurve PulseCurve;

	// Token: 0x040017F8 RID: 6136
	public float PulseWidth = 20f;

	// Token: 0x040017F9 RID: 6137
	public float PulseMinDist = 3f;

	// Token: 0x040017FA RID: 6138
	public float GreyPulseDuration = 2f;

	// Token: 0x040017FB RID: 6139
	public AnimationCurve GreyPulseCurve;

	// Token: 0x040017FC RID: 6140
	public float GreyPulseWidth = 7.5f;

	// Token: 0x040017FD RID: 6141
	private bool IsInSketch;

	// Token: 0x040017FE RID: 6142
	private float dangerIntensityLimit = 1f;

	// Token: 0x040017FF RID: 6143
	public Material desaturateFog;

	// Token: 0x04001800 RID: 6144
	public GameObject TempfxRef;

	// Token: 0x04001801 RID: 6145
	private Dictionary<string, TempFXProfile> nodeProfiles = new Dictionary<string, TempFXProfile>();

	// Token: 0x04001802 RID: 6146
	private Coroutine blurRoutine;

	// Token: 0x04001803 RID: 6147
	private bool isBlurred;

	// Token: 0x0200061C RID: 1564
	[CompilerGenerated]
	private sealed class <FadeInSketch>d__40 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x06002746 RID: 10054 RVA: 0x000D54B6 File Offset: 0x000D36B6
		[DebuggerHidden]
		public <FadeInSketch>d__40(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x06002747 RID: 10055 RVA: 0x000D54C5 File Offset: 0x000D36C5
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06002748 RID: 10056 RVA: 0x000D54C8 File Offset: 0x000D36C8
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			PostFXManager postFXManager = this;
			if (num != 0)
			{
				if (num != 1)
				{
					return false;
				}
				this.<>1__state = -1;
				t += Time.deltaTime / postFXManager.SketchTransitionDuration;
				PostFXManager.UpdateSketch(t);
				if (PlayerControl.myInstance == null)
				{
					postFXManager.StartCoroutine("FadeOutSketch");
					return false;
				}
			}
			else
			{
				this.<>1__state = -1;
				t = 0f;
			}
			if (t >= 1f)
			{
				if (save)
				{
					PageFlip.instance.Save();
				}
				return false;
			}
			this.<>2__current = true;
			this.<>1__state = 1;
			return true;
		}

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06002749 RID: 10057 RVA: 0x000D557B File Offset: 0x000D377B
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x0600274A RID: 10058 RVA: 0x000D5583 File Offset: 0x000D3783
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x0600274B RID: 10059 RVA: 0x000D558A File Offset: 0x000D378A
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x040029D5 RID: 10709
		private int <>1__state;

		// Token: 0x040029D6 RID: 10710
		private object <>2__current;

		// Token: 0x040029D7 RID: 10711
		public PostFXManager <>4__this;

		// Token: 0x040029D8 RID: 10712
		public bool save;

		// Token: 0x040029D9 RID: 10713
		private float <t>5__2;
	}

	// Token: 0x0200061D RID: 1565
	[CompilerGenerated]
	private sealed class <FadeOutSketch>d__41 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x0600274C RID: 10060 RVA: 0x000D5592 File Offset: 0x000D3792
		[DebuggerHidden]
		public <FadeOutSketch>d__41(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x0600274D RID: 10061 RVA: 0x000D55A1 File Offset: 0x000D37A1
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x0600274E RID: 10062 RVA: 0x000D55A4 File Offset: 0x000D37A4
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			PostFXManager postFXManager = this;
			if (num != 0)
			{
				if (num != 1)
				{
					return false;
				}
				this.<>1__state = -1;
				t += Time.deltaTime / (postFXManager.SketchTransitionDuration / 1.5f);
				PostFXManager.UpdateSketch(1f - t);
			}
			else
			{
				this.<>1__state = -1;
				t = 0f;
			}
			if (t >= 1f)
			{
				Action onMapChangeFinished = MapManager.OnMapChangeFinished;
				if (onMapChangeFinished != null)
				{
					onMapChangeFinished();
				}
				PostFXManager.instance.SketchVolume.gameObject.SetActive(false);
				postFXManager.IsInSketch = false;
				return false;
			}
			this.<>2__current = true;
			this.<>1__state = 1;
			return true;
		}

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x0600274F RID: 10063 RVA: 0x000D5662 File Offset: 0x000D3862
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x06002750 RID: 10064 RVA: 0x000D566A File Offset: 0x000D386A
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06002751 RID: 10065 RVA: 0x000D5671 File Offset: 0x000D3871
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x040029DA RID: 10714
		private int <>1__state;

		// Token: 0x040029DB RID: 10715
		private object <>2__current;

		// Token: 0x040029DC RID: 10716
		public PostFXManager <>4__this;

		// Token: 0x040029DD RID: 10717
		private float <t>5__2;
	}

	// Token: 0x0200061E RID: 1566
	[CompilerGenerated]
	private sealed class <SketchPulse>d__43 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x06002752 RID: 10066 RVA: 0x000D5679 File Offset: 0x000D3879
		[DebuggerHidden]
		public <SketchPulse>d__43(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x06002753 RID: 10067 RVA: 0x000D5688 File Offset: 0x000D3888
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06002754 RID: 10068 RVA: 0x000D568C File Offset: 0x000D388C
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			PostFXManager postFXManager = this;
			switch (num)
			{
			case 0:
				this.<>1__state = -1;
				Shader.SetGlobalVector("_FountainLoc", PlayerControl.myInstance.display.CenterOfMass.position);
				postFXManager.SketchVolume.gameObject.SetActive(true);
				postFXManager.SketchVolume.weight = 1f;
				edge = PostFXManager.instance.SketchVolume.profile.GetSetting<EdgeDetection>();
				overlay = PostFXManager.instance.SketchVolume.profile.GetSetting<Overlay>();
				edge.edgeDistMin.value = 0f;
				overlay.distanceMin.value = 0f;
				edge.edgeDistance.value = 0f;
				overlay.distance.value = 0f;
				t = 0f;
				break;
			case 1:
			{
				this.<>1__state = -1;
				t += Time.deltaTime / (postFXManager.PulseDuration / 1.5f);
				float num2 = PostFXManager.instance.PulseCurve.Evaluate(t) * 245f;
				num2 -= postFXManager.PulseWidth;
				num2 += postFXManager.PulseMinDist;
				edge.edgeDistMin.value = Mathf.Max(postFXManager.PulseMinDist, num2);
				overlay.distanceMin.value = Mathf.Max(postFXManager.PulseMinDist - 5f, num2 - 5f);
				edge.edgeDistance.value = Mathf.Max(postFXManager.PulseMinDist, num2 + postFXManager.PulseWidth);
				overlay.distance.value = Mathf.Max(postFXManager.PulseMinDist - 5f, num2 + postFXManager.PulseWidth + 5f);
				break;
			}
			case 2:
				this.<>1__state = -1;
				t += Time.deltaTime;
				postFXManager.SketchVolume.weight = 1f - t;
				goto IL_266;
			default:
				return false;
			}
			if (t < 1f)
			{
				this.<>2__current = true;
				this.<>1__state = 1;
				return true;
			}
			t = 0f;
			IL_266:
			if (t >= 1f)
			{
				postFXManager.SketchVolume.gameObject.SetActive(false);
				Shader.SetGlobalVector("_FountainLoc", Fountain.instance.transform.position);
				return false;
			}
			this.<>2__current = true;
			this.<>1__state = 2;
			return true;
		}

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06002755 RID: 10069 RVA: 0x000D593C File Offset: 0x000D3B3C
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x06002756 RID: 10070 RVA: 0x000D5944 File Offset: 0x000D3B44
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06002757 RID: 10071 RVA: 0x000D594B File Offset: 0x000D3B4B
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x040029DE RID: 10718
		private int <>1__state;

		// Token: 0x040029DF RID: 10719
		private object <>2__current;

		// Token: 0x040029E0 RID: 10720
		public PostFXManager <>4__this;

		// Token: 0x040029E1 RID: 10721
		private EdgeDetection <edge>5__2;

		// Token: 0x040029E2 RID: 10722
		private Overlay <overlay>5__3;

		// Token: 0x040029E3 RID: 10723
		private float <t>5__4;
	}

	// Token: 0x0200061F RID: 1567
	[CompilerGenerated]
	private sealed class <PulseSequence>d__48 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x06002758 RID: 10072 RVA: 0x000D5953 File Offset: 0x000D3B53
		[DebuggerHidden]
		public <PulseSequence>d__48(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x06002759 RID: 10073 RVA: 0x000D5962 File Offset: 0x000D3B62
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x0600275A RID: 10074 RVA: 0x000D5964 File Offset: 0x000D3B64
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			PostFXManager postFXManager = this;
			if (num != 0)
			{
				if (num != 1)
				{
					return false;
				}
				this.<>1__state = -1;
				t += Time.deltaTime / (postFXManager.GreyPulseDuration / 1.5f);
				float num2 = postFXManager.GreyPulseCurve.Evaluate(t) * 250f;
				float r = num2;
				float innerRadius = Mathf.Max(0f, num2 - postFXManager.GreyPulseWidth);
				zone.Update(pos, r);
				GreyscaleAreas.UpdateZones(innerRadius);
			}
			else
			{
				this.<>1__state = -1;
				zone = GreyscaleAreas.AddZone(pos, 0f);
				t = 0f;
			}
			if (t >= 1f)
			{
				GreyscaleAreas.RemoveZone(zone);
				GreyscaleAreas.UpdateZones(0f);
				return false;
			}
			this.<>2__current = true;
			this.<>1__state = 1;
			return true;
		}

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x0600275B RID: 10075 RVA: 0x000D5A56 File Offset: 0x000D3C56
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x0600275C RID: 10076 RVA: 0x000D5A5E File Offset: 0x000D3C5E
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x0600275D RID: 10077 RVA: 0x000D5A65 File Offset: 0x000D3C65
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x040029E4 RID: 10724
		private int <>1__state;

		// Token: 0x040029E5 RID: 10725
		private object <>2__current;

		// Token: 0x040029E6 RID: 10726
		public Vector3 pos;

		// Token: 0x040029E7 RID: 10727
		public PostFXManager <>4__this;

		// Token: 0x040029E8 RID: 10728
		private GreyscaleAreas.GSZone <zone>5__2;

		// Token: 0x040029E9 RID: 10729
		private float <t>5__3;
	}

	// Token: 0x02000620 RID: 1568
	[CompilerGenerated]
	private sealed class <BlurSeq>d__58 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x0600275E RID: 10078 RVA: 0x000D5A6D File Offset: 0x000D3C6D
		[DebuggerHidden]
		public <BlurSeq>d__58(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x0600275F RID: 10079 RVA: 0x000D5A7C File Offset: 0x000D3C7C
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06002760 RID: 10080 RVA: 0x000D5A80 File Offset: 0x000D3C80
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			PostFXManager postFXManager = this;
			if (num != 0)
			{
				if (num != 1)
				{
					return false;
				}
				this.<>1__state = -1;
			}
			else
			{
				this.<>1__state = -1;
				blur = postFXManager.BlurVolume.profile.GetSetting<DepthOfField>();
				kwh = postFXManager.BlurVolume.profile.GetSetting<Kuwahara>();
				postFXManager.BlurVolume.gameObject.SetActive(true);
				postFXManager.BlurVolume.weight = 1f;
				if (PanelManager.CurPanel == PanelType.Cosmetics)
				{
					kwh.startFadeDistance.value = 2.5f;
					kwh.endFadeDistance.value = 3.5f;
					blur.focusDistance.value = 2f;
				}
				else
				{
					blur.focusDistance.value = PanelManager.curSelect.BookOffset.z;
					kwh.startFadeDistance.value = PanelManager.curSelect.BookOffset.z - 0.05f;
					kwh.endFadeDistance.value = PanelManager.curSelect.BookOffset.z + 0.05f;
				}
				wantBlur = 30;
				wantK = 8;
				i = (float)kwh.radius;
				f = blur.focalLength;
			}
			if (f >= (float)wantBlur - 0.05f)
			{
				blur.focalLength.value = (float)wantBlur;
				kwh.radius.value = wantK;
				return false;
			}
			f = Mathf.Lerp(f, (float)wantBlur, Time.deltaTime * 8f);
			i = Mathf.Lerp(i, (float)wantK, Time.deltaTime * 8f);
			blur.focalLength.value = f;
			kwh.radius.value = (int)i;
			this.<>2__current = true;
			this.<>1__state = 1;
			return true;
		}

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x06002761 RID: 10081 RVA: 0x000D5CC4 File Offset: 0x000D3EC4
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x06002762 RID: 10082 RVA: 0x000D5CCC File Offset: 0x000D3ECC
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06002763 RID: 10083 RVA: 0x000D5CD3 File Offset: 0x000D3ED3
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x040029EA RID: 10730
		private int <>1__state;

		// Token: 0x040029EB RID: 10731
		private object <>2__current;

		// Token: 0x040029EC RID: 10732
		public PostFXManager <>4__this;

		// Token: 0x040029ED RID: 10733
		private DepthOfField <blur>5__2;

		// Token: 0x040029EE RID: 10734
		private Kuwahara <kwh>5__3;

		// Token: 0x040029EF RID: 10735
		private int <wantBlur>5__4;

		// Token: 0x040029F0 RID: 10736
		private int <wantK>5__5;

		// Token: 0x040029F1 RID: 10737
		private float <k>5__6;

		// Token: 0x040029F2 RID: 10738
		private float <f>5__7;
	}

	// Token: 0x02000621 RID: 1569
	[CompilerGenerated]
	private sealed class <UnblurSeq>d__59 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x06002764 RID: 10084 RVA: 0x000D5CDB File Offset: 0x000D3EDB
		[DebuggerHidden]
		public <UnblurSeq>d__59(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x06002765 RID: 10085 RVA: 0x000D5CEA File Offset: 0x000D3EEA
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06002766 RID: 10086 RVA: 0x000D5CEC File Offset: 0x000D3EEC
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			PostFXManager postFXManager = this;
			if (num != 0)
			{
				if (num != 1)
				{
					return false;
				}
				this.<>1__state = -1;
			}
			else
			{
				this.<>1__state = -1;
				blur = postFXManager.BlurVolume.profile.GetSetting<DepthOfField>();
				kwh = postFXManager.BlurVolume.profile.GetSetting<Kuwahara>();
				i = (float)kwh.radius;
				f = blur.focalLength;
			}
			if (i <= 0.05f)
			{
				postFXManager.BlurVolume.weight = 0f;
				postFXManager.BlurVolume.gameObject.SetActive(false);
				return false;
			}
			f = Mathf.Lerp(f, 1f, Time.deltaTime * 8f);
			i = Mathf.Lerp(i, 0f, Time.deltaTime * 8f);
			blur.focalLength.value = f;
			kwh.radius.value = (int)i;
			this.<>2__current = true;
			this.<>1__state = 1;
			return true;
		}

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x06002767 RID: 10087 RVA: 0x000D5E35 File Offset: 0x000D4035
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x06002768 RID: 10088 RVA: 0x000D5E3D File Offset: 0x000D403D
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06002769 RID: 10089 RVA: 0x000D5E44 File Offset: 0x000D4044
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x040029F3 RID: 10739
		private int <>1__state;

		// Token: 0x040029F4 RID: 10740
		private object <>2__current;

		// Token: 0x040029F5 RID: 10741
		public PostFXManager <>4__this;

		// Token: 0x040029F6 RID: 10742
		private DepthOfField <blur>5__2;

		// Token: 0x040029F7 RID: 10743
		private Kuwahara <kwh>5__3;

		// Token: 0x040029F8 RID: 10744
		private float <k>5__4;

		// Token: 0x040029F9 RID: 10745
		private float <f>5__5;
	}
}
