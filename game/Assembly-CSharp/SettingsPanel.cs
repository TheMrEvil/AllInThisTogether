using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using MagicaCloth2;
using PI.NGSS;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020001EF RID: 495
public class SettingsPanel : MonoBehaviour
{
	// Token: 0x1700016A RID: 362
	// (get) Token: 0x06001512 RID: 5394 RVA: 0x00084128 File Offset: 0x00082328
	private SettingsPanel.SettingTab CurrentTab
	{
		get
		{
			foreach (SettingsPanel.SettingTab settingTab in this.Tabs)
			{
				if (settingTab.Setting == this.currentSetting)
				{
					return settingTab;
				}
			}
			return null;
		}
	}

	// Token: 0x06001513 RID: 5395 RVA: 0x0008418C File Offset: 0x0008238C
	private void Awake()
	{
		SettingsPanel.instance = this;
		foreach (SettingsPanel.SettingTab settingTab in this.Tabs)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.TabRef, this.TabRef.transform.parent);
			gameObject.SetActive(true);
			SettingsTab component = gameObject.GetComponent<SettingsTab>();
			component.Setup(settingTab);
			settingTab.tabRef = component;
		}
		UIPanel component2 = base.GetComponent<UIPanel>();
		component2.OnEnteredPanel = (Action)Delegate.Combine(component2.OnEnteredPanel, new Action(delegate()
		{
			this.SetTab(SettingsPanel.TabType.General, false);
		}));
		component2.OnLeftPanel = (Action)Delegate.Combine(component2.OnLeftPanel, new Action(InputManager.SaveBindings));
		component2.OnNextTab = (Action)Delegate.Combine(component2.OnNextTab, new Action(this.NextTab));
		component2.OnPrevTab = (Action)Delegate.Combine(component2.OnPrevTab, new Action(this.PrevTab));
		component2.OnPageNext = (Action)Delegate.Combine(component2.OnPageNext, new Action(this.NextPage));
		component2.OnPagePrev = (Action)Delegate.Combine(component2.OnPagePrev, new Action(this.PrevPage));
		this.ApplyButton.GetComponent<Button>().onClick.AddListener(new UnityAction(this.AcceptChanges));
		ResolutionSelector resolution = this.Resolution;
		resolution.OnScreenSettingsChanged = (Action)Delegate.Combine(resolution.OnScreenSettingsChanged, new Action(this.UpdateResolutionSettings));
		this.BuildData.text = PickupManager.instance.DB.BuildData;
	}

	// Token: 0x06001514 RID: 5396 RVA: 0x00084348 File Offset: 0x00082548
	private IEnumerator Start()
	{
		yield return true;
		using (List<SettingsPanel.SettingTab>.Enumerator enumerator = this.Tabs.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				SettingsPanel.SettingTab settingTab = enumerator.Current;
				foreach (SettingsPanel.SettingOption settingOption in settingTab.Options)
				{
					if (settingOption.interactType == SettingsPanel.InteractType.Slider)
					{
						this.SliderValueChanged(settingOption.settingID, Settings.GetFloat(settingOption.settingID, (float)settingOption.GetDefaultValue()));
					}
					else if (settingOption.interactType == SettingsPanel.InteractType.Selector)
					{
						this.SelectorValueChanged(settingOption.settingID, Settings.GetInt(settingOption.settingID, (int)settingOption.GetDefaultValue()));
					}
					else if (settingOption.interactType == SettingsPanel.InteractType.Toggle)
					{
						this.ToggleValueChanged(settingOption.settingID, Settings.GetBool(settingOption.settingID, (bool)settingOption.GetDefaultValue()));
					}
				}
			}
			goto IL_15A;
		}
		IL_13E:
		yield return true;
		IL_15A:
		if (this.Resolution.options.Count != 0)
		{
			this.UpdateResolutionSettings();
			this.Resolution.ApplySetting();
			yield break;
		}
		goto IL_13E;
	}

	// Token: 0x06001515 RID: 5397 RVA: 0x00084358 File Offset: 0x00082558
	private void UpdateResolutionSettings()
	{
		foreach (SettingsPanel.SettingTab settingTab in this.Tabs)
		{
			foreach (SettingsPanel.SettingOption settingOption in settingTab.Options)
			{
				if (settingOption.settingID == SystemSetting.Resolution)
				{
					settingOption.options = new List<string>();
					foreach (Vector2 vector in this.Resolution.options)
					{
						List<string> options = settingOption.options;
						float num = vector.x;
						string str = num.ToString();
						string str2 = "x";
						num = vector.y;
						options.Add(str + str2 + num.ToString());
					}
				}
			}
		}
		if (PanelManager.CurPanel == PanelType.Settings && this.currentSetting == SettingsPanel.TabType.Graphics)
		{
			this.SetTab(SettingsPanel.TabType.Graphics, true);
		}
	}

	// Token: 0x06001516 RID: 5398 RVA: 0x0008448C File Offset: 0x0008268C
	public void GoToSettings()
	{
		if (PanelManager.curSelect.panelType == PanelType.Settings)
		{
			return;
		}
		PanelManager.instance.PushPanel(PanelType.Settings);
	}

	// Token: 0x06001517 RID: 5399 RVA: 0x000844A7 File Offset: 0x000826A7
	public void AcceptChanges()
	{
		if (this.Resolution != null)
		{
			this.Resolution.ApplySetting();
		}
	}

	// Token: 0x06001518 RID: 5400 RVA: 0x000844C4 File Offset: 0x000826C4
	public void ResetSettings()
	{
		if (this.CurrentTab.Setting == SettingsPanel.TabType.Bindings)
		{
			InputManager.ResetBindings();
		}
		foreach (SettingElement settingElement in this.CurSettingObjects)
		{
			settingElement.Reset();
		}
	}

	// Token: 0x06001519 RID: 5401 RVA: 0x00084528 File Offset: 0x00082728
	private void Update()
	{
		if (PanelManager.CurPanel != PanelType.Settings)
		{
			return;
		}
		if (this.Scroller != null && InputManager.IsUsingController)
		{
			this.Scroller.TickUpdate();
		}
	}

	// Token: 0x0600151A RID: 5402 RVA: 0x00084554 File Offset: 0x00082754
	public void NextTab()
	{
		UnityEngine.Debug.Log("Next Tab - Settings");
		int num = this.CurrentTabIndex();
		num++;
		if (num >= this.Tabs.Count)
		{
			num = 0;
		}
		this.SetTab(this.Tabs[num].Setting, false);
	}

	// Token: 0x0600151B RID: 5403 RVA: 0x000845A0 File Offset: 0x000827A0
	public void PrevTab()
	{
		int num = this.CurrentTabIndex();
		num--;
		if (num < 0)
		{
			num = this.Tabs.Count - 1;
		}
		this.SetTab(this.Tabs[num].Setting, false);
	}

	// Token: 0x0600151C RID: 5404 RVA: 0x000845E4 File Offset: 0x000827E4
	private int CurrentTabIndex()
	{
		for (int i = 0; i < this.Tabs.Count; i++)
		{
			if (this.Tabs[i].Setting == this.currentSetting)
			{
				return i;
			}
		}
		return 0;
	}

	// Token: 0x0600151D RID: 5405 RVA: 0x00084624 File Offset: 0x00082824
	public void SetTab(SettingsPanel.TabType t, bool force = false)
	{
		if (t == this.currentSetting && !force)
		{
			return;
		}
		this.DetailInfo.text = "";
		this.DetailImage.gameObject.SetActive(false);
		this.CreditsHolder.SetActive(t == SettingsPanel.TabType.Credits);
		this.OptionsHolder.SetActive(t != SettingsPanel.TabType.Credits);
		this.ClearSettingObjects();
		foreach (SettingsPanel.SettingTab settingTab in this.Tabs)
		{
			settingTab.tabRef.UpdateSelected(t);
			if (settingTab.Setting == t)
			{
				this.CreateSettingObjects(settingTab);
			}
		}
		this.currentSetting = t;
	}

	// Token: 0x0600151E RID: 5406 RVA: 0x000846EC File Offset: 0x000828EC
	private void ClearSettingObjects()
	{
		for (int i = 0; i < this.CurSettingObjects.Count; i++)
		{
			UnityEngine.Object.Destroy(this.CurSettingObjects[i].gameObject);
		}
		this.CurSettingObjects.Clear();
	}

	// Token: 0x0600151F RID: 5407 RVA: 0x00084730 File Offset: 0x00082930
	private void CreateSettingObjects(SettingsPanel.SettingTab tab)
	{
		foreach (SettingsPanel.SettingOption settingOption in tab.Options)
		{
			if ((!InputManager.IsUsingController || settingOption.Shown != SettingsPanel.ShowSetting.Keyboard) && (InputManager.IsUsingController || settingOption.Shown != SettingsPanel.ShowSetting.Controller))
			{
				GameObject gameObject = this.SliderRef;
				if (settingOption.interactType == SettingsPanel.InteractType.Selector)
				{
					gameObject = this.SelectorRef;
				}
				else if (settingOption.interactType == SettingsPanel.InteractType.Toggle)
				{
					gameObject = this.ToggleRef;
				}
				else if (settingOption.interactType == SettingsPanel.InteractType.Keybinder)
				{
					gameObject = this.KeybindRef;
				}
				else if (settingOption.interactType == SettingsPanel.InteractType.Header)
				{
					gameObject = this.HeaderRef;
				}
				GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(gameObject, gameObject.transform.parent);
				gameObject2.SetActive(true);
				SettingElement component = gameObject2.GetComponent<SettingElement>();
				component.Setup(settingOption);
				this.CurSettingObjects.Add(component);
			}
		}
		UISelector.SetupVerticalListNav<SettingElement>(this.CurSettingObjects, null, null, false);
		this.ApplyButton.SetActive(tab.UseApplyButton);
		UISelector.SelectFirstInList<SettingElement>(this.CurSettingObjects);
	}

	// Token: 0x06001520 RID: 5408 RVA: 0x00084850 File Offset: 0x00082A50
	public void SettingSelected(SettingsPanel.SettingOption setting)
	{
		if (this.DetailImage == null || setting == null || this.DetailInfo == null)
		{
			return;
		}
		this.DetailImage.gameObject.SetActive(setting.imageDisplay != null);
		this.DetailImage.sprite = setting.imageDisplay;
		this.DetailTitle.text = setting.Label;
		this.DetailInfo.text = setting.detailText;
	}

	// Token: 0x06001521 RID: 5409 RVA: 0x000848CC File Offset: 0x00082ACC
	public void LeaveSettings()
	{
		PanelManager.instance.PopPanel();
	}

	// Token: 0x06001522 RID: 5410 RVA: 0x000848D8 File Offset: 0x00082AD8
	public void SliderValueChanged(SystemSetting ID, float value)
	{
		if (ID == SystemSetting.MasterVolume || ID - SystemSetting.MusicVolume <= 3)
		{
			AudioManager.instance.ChangeVolumeSetting(ID, value);
		}
	}

	// Token: 0x06001523 RID: 5411 RVA: 0x000848F0 File Offset: 0x00082AF0
	public void SelectorValueChanged(SystemSetting ID, int value)
	{
		if (ID <= SystemSetting.TerrainDetails)
		{
			switch (ID)
			{
			case SystemSetting.Resolution:
				this.Resolution.SetResolutionID(value);
				return;
			case SystemSetting.Vsync:
			case SystemSetting.MasterVolume:
				break;
			case SystemSetting.ShadowQuality:
			{
				QualitySettings.shadows = ((value == 0) ? ShadowQuality.Disable : ShadowQuality.All);
				ShadowResolution[] array = new ShadowResolution[4];
				RuntimeHelpers.InitializeArray(array, fieldof(<PrivateImplementationDetails>.BAED642339816AFFB3FE8719792D0E4CE82F12DB72B7373D244EAA65445800FE).FieldHandle);
				QualitySettings.shadowResolution = array[value];
				NGSS_Directional ngss_Directional = UnityEngine.Object.FindObjectOfType<NGSS_Directional>();
				if (ngss_Directional != null)
				{
					ngss_Directional.NGSS_SHADOWS_RESOLUTION = (new NGSS_Directional.ShadowMapResolution[]
					{
						(NGSS_Directional.ShadowMapResolution)0,
						NGSS_Directional.ShadowMapResolution.VeryLow,
						NGSS_Directional.ShadowMapResolution.Med,
						NGSS_Directional.ShadowMapResolution.High,
						NGSS_Directional.ShadowMapResolution.Ultra
					})[value];
				}
				QualitySettings.shadowCascades = ((value >= 3) ? 4 : 2);
				return;
			}
			case SystemSetting.FullscreenMode:
				this.Resolution.SetFullscreenMode(value);
				return;
			case SystemSetting.TextureQuality:
				QualitySettings.masterTextureLimit = 2 - value;
				return;
			default:
				if (ID != SystemSetting.TerrainDetails)
				{
					return;
				}
				Shader.SetGlobalInt("_QualitySetting", value);
				return;
			}
		}
		else
		{
			if (ID == SystemSetting.ServerRegion)
			{
				NetworkManager.instance.SetRegion(value);
				return;
			}
			if (ID == SystemSetting.VFXDensity_Player)
			{
				ActionEnabler.PlayerQuality = (ActionEnabler.EffectQuality)value;
				return;
			}
			if (ID != SystemSetting.VFXDensity_Enemy)
			{
				return;
			}
			ActionEnabler.EnemyQuality = (ActionEnabler.EffectQuality)value;
		}
	}

	// Token: 0x06001524 RID: 5412 RVA: 0x000849E4 File Offset: 0x00082BE4
	public void ToggleValueChanged(SystemSetting ID, bool value)
	{
		if (ID > SystemSetting.SoundInBackground)
		{
			if (ID != SystemSetting.HDROutput)
			{
				if (ID == SystemSetting.ClothPhysics)
				{
					MagicaManager.IsClothEnabled = value;
					return;
				}
				if (ID != SystemSetting.Photosensitivity)
				{
					return;
				}
				Shader.SetGlobalFloat(SettingsPanel.Photosens, (float)(value ? 1 : 0));
			}
			else if (HDROutputSettings.main.available)
			{
				HDROutputSettings.main.automaticHDRTonemapping = value;
				HDROutputSettings.main.RequestHDRModeChange(value);
				return;
			}
			return;
		}
		if (ID != SystemSetting.Vsync)
		{
			return;
		}
		QualitySettings.vSyncCount = (value ? 1 : 0);
	}

	// Token: 0x06001525 RID: 5413 RVA: 0x00084A59 File Offset: 0x00082C59
	private void NextPage()
	{
		if (this.currentSetting == SettingsPanel.TabType.Credits)
		{
			this.Credits.NextPage();
		}
	}

	// Token: 0x06001526 RID: 5414 RVA: 0x00084A70 File Offset: 0x00082C70
	private void PrevPage()
	{
		if (this.currentSetting == SettingsPanel.TabType.Credits)
		{
			this.Credits.PrevPage();
		}
	}

	// Token: 0x06001527 RID: 5415 RVA: 0x00084A87 File Offset: 0x00082C87
	public SettingsPanel()
	{
	}

	// Token: 0x06001528 RID: 5416 RVA: 0x00084A9A File Offset: 0x00082C9A
	// Note: this type is marked as 'beforefieldinit'.
	static SettingsPanel()
	{
	}

	// Token: 0x06001529 RID: 5417 RVA: 0x00084AAB File Offset: 0x00082CAB
	[CompilerGenerated]
	private void <Awake>b__24_0()
	{
		this.SetTab(SettingsPanel.TabType.General, false);
	}

	// Token: 0x04001484 RID: 5252
	public static SettingsPanel instance;

	// Token: 0x04001485 RID: 5253
	[Header("Tabs")]
	public GameObject TabRef;

	// Token: 0x04001486 RID: 5254
	public List<SettingsPanel.SettingTab> Tabs;

	// Token: 0x04001487 RID: 5255
	[Header("Main Content")]
	public GameObject OptionsHolder;

	// Token: 0x04001488 RID: 5256
	public TextMeshProUGUI BuildData;

	// Token: 0x04001489 RID: 5257
	public AutoScrollRect Scroller;

	// Token: 0x0400148A RID: 5258
	public GameObject SliderRef;

	// Token: 0x0400148B RID: 5259
	public GameObject SelectorRef;

	// Token: 0x0400148C RID: 5260
	public GameObject ToggleRef;

	// Token: 0x0400148D RID: 5261
	public GameObject KeybindRef;

	// Token: 0x0400148E RID: 5262
	public GameObject HeaderRef;

	// Token: 0x0400148F RID: 5263
	public Image DetailImage;

	// Token: 0x04001490 RID: 5264
	public TextMeshProUGUI DetailTitle;

	// Token: 0x04001491 RID: 5265
	public TextMeshProUGUI DetailInfo;

	// Token: 0x04001492 RID: 5266
	public GameObject ApplyButton;

	// Token: 0x04001493 RID: 5267
	[Header("Special Components")]
	public ResolutionSelector Resolution;

	// Token: 0x04001494 RID: 5268
	[Header("Credits")]
	public GameObject CreditsHolder;

	// Token: 0x04001495 RID: 5269
	public SettingsCredits Credits;

	// Token: 0x04001496 RID: 5270
	public SettingsPanel.TabType currentSetting;

	// Token: 0x04001497 RID: 5271
	public GameObject RayBlocker;

	// Token: 0x04001498 RID: 5272
	private List<SettingElement> CurSettingObjects = new List<SettingElement>();

	// Token: 0x04001499 RID: 5273
	private static readonly int Photosens = Shader.PropertyToID("_PHOTOSENS");

	// Token: 0x020005BB RID: 1467
	[Serializable]
	public class SettingTab
	{
		// Token: 0x060025FA RID: 9722 RVA: 0x000D29BD File Offset: 0x000D0BBD
		public SettingTab()
		{
		}

		// Token: 0x0400285C RID: 10332
		public SettingsPanel.TabType Setting;

		// Token: 0x0400285D RID: 10333
		public string Title;

		// Token: 0x0400285E RID: 10334
		[TextArea(2, 5)]
		public string detailText;

		// Token: 0x0400285F RID: 10335
		public List<SettingsPanel.SettingOption> Options;

		// Token: 0x04002860 RID: 10336
		public bool UseApplyButton;

		// Token: 0x04002861 RID: 10337
		[NonSerialized]
		public SettingsTab tabRef;
	}

	// Token: 0x020005BC RID: 1468
	public enum TabType
	{
		// Token: 0x04002863 RID: 10339
		_,
		// Token: 0x04002864 RID: 10340
		Graphics,
		// Token: 0x04002865 RID: 10341
		Gameplay,
		// Token: 0x04002866 RID: 10342
		Audio,
		// Token: 0x04002867 RID: 10343
		General,
		// Token: 0x04002868 RID: 10344
		Color,
		// Token: 0x04002869 RID: 10345
		Game,
		// Token: 0x0400286A RID: 10346
		Room,
		// Token: 0x0400286B RID: 10347
		Input,
		// Token: 0x0400286C RID: 10348
		Credits,
		// Token: 0x0400286D RID: 10349
		Bindings
	}

	// Token: 0x020005BD RID: 1469
	[Serializable]
	public class SettingOption
	{
		// Token: 0x060025FB RID: 9723 RVA: 0x000D29C8 File Offset: 0x000D0BC8
		public object GetDefaultValue()
		{
			bool flag = this.SteamdeckOverride && PlatformSetup.IsSteamDeck;
			object result;
			switch (this.interactType)
			{
			case SettingsPanel.InteractType.Slider:
				result = (flag ? this.steamdeckNumber : this.defaultNumber);
				break;
			case SettingsPanel.InteractType.Toggle:
				result = (flag ? this.steamdeckBool : this.defaultBool);
				break;
			case SettingsPanel.InteractType.Selector:
				result = (flag ? this.steamdeckSelection : this.defaultSelection);
				break;
			case SettingsPanel.InteractType.Keybinder:
				result = this.action;
				break;
			default:
				result = 0;
				break;
			}
			return result;
		}

		// Token: 0x060025FC RID: 9724 RVA: 0x000D2A66 File Offset: 0x000D0C66
		public SettingOption()
		{
		}

		// Token: 0x0400286E RID: 10350
		public string Label = "";

		// Token: 0x0400286F RID: 10351
		public SystemSetting settingID;

		// Token: 0x04002870 RID: 10352
		public SettingsPanel.ShowSetting Shown;

		// Token: 0x04002871 RID: 10353
		public Sprite imageDisplay;

		// Token: 0x04002872 RID: 10354
		[TextArea(2, 5)]
		public string detailText;

		// Token: 0x04002873 RID: 10355
		[Header("Interaction")]
		public SettingsPanel.InteractType interactType = SettingsPanel.InteractType.Selector;

		// Token: 0x04002874 RID: 10356
		public float defaultNumber = 0.5f;

		// Token: 0x04002875 RID: 10357
		public bool WholeNumbers;

		// Token: 0x04002876 RID: 10358
		public float MinVal;

		// Token: 0x04002877 RID: 10359
		public float MaxVal = 1f;

		// Token: 0x04002878 RID: 10360
		public bool defaultBool;

		// Token: 0x04002879 RID: 10361
		public List<string> options;

		// Token: 0x0400287A RID: 10362
		public int defaultSelection;

		// Token: 0x0400287B RID: 10363
		public InputActions.InputAction action;

		// Token: 0x0400287C RID: 10364
		public bool SteamdeckOverride;

		// Token: 0x0400287D RID: 10365
		public float steamdeckNumber;

		// Token: 0x0400287E RID: 10366
		public bool steamdeckBool;

		// Token: 0x0400287F RID: 10367
		public int steamdeckSelection;
	}

	// Token: 0x020005BE RID: 1470
	public enum InteractType
	{
		// Token: 0x04002881 RID: 10369
		Slider,
		// Token: 0x04002882 RID: 10370
		Toggle,
		// Token: 0x04002883 RID: 10371
		Selector,
		// Token: 0x04002884 RID: 10372
		Keybinder,
		// Token: 0x04002885 RID: 10373
		Header
	}

	// Token: 0x020005BF RID: 1471
	public enum ShowSetting
	{
		// Token: 0x04002887 RID: 10375
		Always,
		// Token: 0x04002888 RID: 10376
		Controller,
		// Token: 0x04002889 RID: 10377
		Keyboard
	}

	// Token: 0x020005C0 RID: 1472
	[CompilerGenerated]
	private sealed class <Start>d__25 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x060025FD RID: 9725 RVA: 0x000D2A96 File Offset: 0x000D0C96
		[DebuggerHidden]
		public <Start>d__25(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x060025FE RID: 9726 RVA: 0x000D2AA5 File Offset: 0x000D0CA5
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x060025FF RID: 9727 RVA: 0x000D2AA8 File Offset: 0x000D0CA8
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			SettingsPanel settingsPanel = this;
			switch (num)
			{
			case 0:
				this.<>1__state = -1;
				this.<>2__current = true;
				this.<>1__state = 1;
				return true;
			case 1:
				this.<>1__state = -1;
				using (List<SettingsPanel.SettingTab>.Enumerator enumerator = settingsPanel.Tabs.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						SettingsPanel.SettingTab settingTab = enumerator.Current;
						foreach (SettingsPanel.SettingOption settingOption in settingTab.Options)
						{
							if (settingOption.interactType == SettingsPanel.InteractType.Slider)
							{
								settingsPanel.SliderValueChanged(settingOption.settingID, Settings.GetFloat(settingOption.settingID, (float)settingOption.GetDefaultValue()));
							}
							else if (settingOption.interactType == SettingsPanel.InteractType.Selector)
							{
								settingsPanel.SelectorValueChanged(settingOption.settingID, Settings.GetInt(settingOption.settingID, (int)settingOption.GetDefaultValue()));
							}
							else if (settingOption.interactType == SettingsPanel.InteractType.Toggle)
							{
								settingsPanel.ToggleValueChanged(settingOption.settingID, Settings.GetBool(settingOption.settingID, (bool)settingOption.GetDefaultValue()));
							}
						}
					}
					goto IL_15A;
				}
				break;
			case 2:
				this.<>1__state = -1;
				goto IL_15A;
			default:
				return false;
			}
			IL_13E:
			this.<>2__current = true;
			this.<>1__state = 2;
			return true;
			IL_15A:
			if (settingsPanel.Resolution.options.Count != 0)
			{
				settingsPanel.UpdateResolutionSettings();
				settingsPanel.Resolution.ApplySetting();
				return false;
			}
			goto IL_13E;
		}

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06002600 RID: 9728 RVA: 0x000D2C50 File Offset: 0x000D0E50
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x06002601 RID: 9729 RVA: 0x000D2C58 File Offset: 0x000D0E58
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06002602 RID: 9730 RVA: 0x000D2C5F File Offset: 0x000D0E5F
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x0400288A RID: 10378
		private int <>1__state;

		// Token: 0x0400288B RID: 10379
		private object <>2__current;

		// Token: 0x0400288C RID: 10380
		public SettingsPanel <>4__this;
	}
}
