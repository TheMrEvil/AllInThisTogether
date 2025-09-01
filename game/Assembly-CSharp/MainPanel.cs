using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x020001E7 RID: 487
public class MainPanel : MonoBehaviourPunCallbacks
{
	// Token: 0x17000164 RID: 356
	// (get) Token: 0x06001472 RID: 5234 RVA: 0x0007FBFB File Offset: 0x0007DDFB
	public bool InMP
	{
		get
		{
			return this.inMPGroup;
		}
	}

	// Token: 0x06001473 RID: 5235 RVA: 0x0007FC04 File Offset: 0x0007DE04
	private void Awake()
	{
		MainPanel.instance = this;
		this.VersionText.text = "v " + Application.version;
		this.panel = base.GetComponent<UIPanel>();
		UIPanel uipanel = this.panel;
		uipanel.OnEnteredPanel = (Action)Delegate.Combine(uipanel.OnEnteredPanel, new Action(this.OnPanelEntered));
		UIPanel uipanel2 = this.panel;
		uipanel2.OnControllerStarted = (Action)Delegate.Combine(uipanel2.OnControllerStarted, new Action(this.OnControllerSelect));
		this.DemoDisplay.SetActive(false);
		this.SingleplayerButton.onClick.AddListener(new UnityAction(this.OfflineGame));
		this.QuickmatchButton.onClick.AddListener(new UnityAction(this.CreateOrJoinRandom));
		this.CreateButton.onClick.AddListener(new UnityAction(this.CreateGameButton));
		this.JoinButton.onClick.AddListener(new UnityAction(this.JoinGameButton));
		this.MPBackButton.onClick.AddListener(new UnityAction(this.MPClose));
		this.MPButton.onClick.AddListener(new UnityAction(this.MPOpenAction));
		this.JoinButton.interactable = false;
	}

	// Token: 0x06001474 RID: 5236 RVA: 0x0007FD4C File Offset: 0x0007DF4C
	private void Start()
	{
		this.SettingsButton.onClick.AddListener(new UnityAction(SettingsPanel.instance.GoToSettings));
		this.FTUXSettingsButton.onClick.AddListener(new UnityAction(SettingsPanel.instance.GoToSettings));
		this.VolumeSlider.InitializeSlider();
		this.RegionSelector.Setup(null);
		SelectorSetting regionSelector = this.RegionSelector;
		regionSelector.OnChangeSystemSetting = (Action<SystemSetting, int>)Delegate.Combine(regionSelector.OnChangeSystemSetting, new Action<SystemSetting, int>(this.OnMPRegionChanged));
	}

	// Token: 0x06001475 RID: 5237 RVA: 0x0007FDD8 File Offset: 0x0007DFD8
	public void OnPanelEntered()
	{
		this.CoreBox.SetActive(Settings.DoneFTUX);
		this.FTBox.SetActive(!Settings.DoneFTUX);
		InfoDisplay.Reset();
		if (PhotonNetwork.InLobby)
		{
			this.MPOpenAction();
		}
		else if (this.inMPGroup)
		{
			this.MPClose();
		}
		this.OnControllerSelect();
		if (this.FTBox.activeSelf)
		{
			this.VolumeSlider.UpdateStateExternal();
		}
		if (Progression.BadLoad)
		{
			if (Settings.HasBackupAvailable())
			{
				ConfirmationPrompt.OpenPrompt(this.CorruptSavePrompt.Replace("%date%", Settings.GetLatestBackupTime().ToString("yyyy-MM-dd HH:mm")), "Yes", "Quit", new Action<bool>(this.TryLoadBackupFiles), 0f);
				ConfirmationPrompt.SetTitle("Corrupted Data");
				return;
			}
			this.BadLoad.SetActive(true);
		}
	}

	// Token: 0x06001476 RID: 5238 RVA: 0x0007FEAF File Offset: 0x0007E0AF
	private void TryLoadBackupFiles(bool shouldLoad)
	{
		if (!shouldLoad)
		{
			Application.Quit();
			return;
		}
		GameStats.LoadFromBackup();
	}

	// Token: 0x06001477 RID: 5239 RVA: 0x0007FEC0 File Offset: 0x0007E0C0
	private void OnControllerSelect()
	{
		if (!InputManager.IsUsingController)
		{
			return;
		}
		if (this.FTBox.activeSelf)
		{
			UISelector.SelectSelectable(this.StartFTUXButton);
			return;
		}
		if (this.inMPGroup)
		{
			UnityEngine.Debug.Log("Selecting MP Controller Item");
			UISelector.SelectSelectable(this.QuickmatchButton);
			return;
		}
		UISelector.SelectSelectable(this.SingleplayerButton);
	}

	// Token: 0x06001478 RID: 5240 RVA: 0x0007FF18 File Offset: 0x0007E118
	private void Update()
	{
		if (PanelManager.CurPanel != PanelType.Main)
		{
			return;
		}
		if (this.wantDetail && this.DetailGroup.alpha < 1f)
		{
			this.DetailGroup.alpha += Time.deltaTime * 2f;
		}
		else if (!this.wantDetail && this.DetailGroup.alpha > 0f)
		{
			this.DetailGroup.alpha -= Time.deltaTime;
		}
		this.MPGroup.UpdateOpacity(this.inMPGroup, 3f, false);
		this.MainGroup.UpdateOpacity(!this.inMPGroup, 3f, false);
		string text = NetworkManager.RegionCodeName(PhotonNetwork.CloudRegion);
		if (!string.IsNullOrEmpty(PhotonNetwork.CloudRegion) && this.RegionText.text != text)
		{
			this.RegionText.text = text;
		}
		if (EventSystem.current.currentSelectedGameObject == this.gameInput.gameObject && Input.GetKeyDown(KeyCode.Return))
		{
			this.JoinGameButton();
		}
		if (this.inMPGroup && UISelector.Actions.UIBack.WasPressed && this.MPBackButton.interactable)
		{
			this.MPClose();
		}
	}

	// Token: 0x06001479 RID: 5241 RVA: 0x00080058 File Offset: 0x0007E258
	private void SetDetailText(string dtext, float hideTime = 0f)
	{
		if (this.DetailText.text != dtext)
		{
			this.DetailText.text = dtext;
		}
		this.wantDetail = true;
		if (this.hideRoutine != null)
		{
			base.StopCoroutine(this.hideRoutine);
		}
		if (hideTime > 0f)
		{
			this.hideRoutine = base.StartCoroutine(this.HideDelay(hideTime));
		}
	}

	// Token: 0x0600147A RID: 5242 RVA: 0x000800BA File Offset: 0x0007E2BA
	private IEnumerator HideDelay(float delayTime)
	{
		yield return new WaitForSeconds(delayTime);
		this.HideDetail();
		yield break;
	}

	// Token: 0x0600147B RID: 5243 RVA: 0x000800D0 File Offset: 0x0007E2D0
	private void HideDetail()
	{
		this.wantDetail = false;
	}

	// Token: 0x0600147C RID: 5244 RVA: 0x000800D9 File Offset: 0x0007E2D9
	public void OfflineGame()
	{
		if (PhotonNetwork.IsConnected || PhotonNetwork.IsConnectedAndReady)
		{
			NetworkManager.instance.GoOffline();
		}
		PhotonNetwork.OfflineMode = true;
		this.CreateOrJoinRandom();
	}

	// Token: 0x0600147D RID: 5245 RVA: 0x000800FF File Offset: 0x0007E2FF
	public void OpenRoadmap()
	{
		PanelManager.instance.PushPanel(PanelType.Roadmap);
	}

	// Token: 0x0600147E RID: 5246 RVA: 0x0008010D File Offset: 0x0007E30D
	public void Tutorial()
	{
		if (!TutorialManager.InTutorial)
		{
			base.StartCoroutine("EnterTutorial");
		}
	}

	// Token: 0x0600147F RID: 5247 RVA: 0x00080122 File Offset: 0x0007E322
	private IEnumerator EnterTutorial()
	{
		TutorialManager.instance.StartTutorial();
		if (this.TutorialRoomMode == 0)
		{
			if (PhotonNetwork.IsConnected)
			{
				PhotonNetwork.Disconnect();
			}
			while (PhotonNetwork.IsConnected)
			{
				yield return true;
			}
			PhotonNetwork.OfflineMode = true;
			this.OfflineGame();
		}
		else
		{
			PhotonNetwork.OfflineMode = false;
			if (!PhotonNetwork.IsConnected)
			{
				NetworkManager.instance.GoOnline();
			}
			while (!PhotonNetwork.IsConnected || (PhotonNetwork.NetworkClientState != ClientState.JoinedLobby && PhotonNetwork.NetworkClientState != ClientState.ConnectedToMasterServer))
			{
				yield return true;
			}
			this.CreateGameButton();
			AudioManager.PlayInterfaceSFX(this.StartSFX, 1f, 0f);
		}
		yield break;
	}

	// Token: 0x06001480 RID: 5248 RVA: 0x00080131 File Offset: 0x0007E331
	public void FTUXRoomMode(int mode)
	{
		this.TutorialRoomMode = mode;
	}

	// Token: 0x06001481 RID: 5249 RVA: 0x0008013C File Offset: 0x0007E33C
	public void CreateGameButton()
	{
		this.ButtonInteractivity(false);
		ConfirmationPrompt.OpenPrompt(this.CreateGamePrompt, "Public", "Cancel", new Action<bool>(this.CreateRoom), 0f);
		ConfirmationPrompt.UseTertiary("Private", new Action(this.CreatePrivateRoom), true);
	}

	// Token: 0x06001482 RID: 5250 RVA: 0x0008018D File Offset: 0x0007E38D
	private void CreateRoom(bool openRoom)
	{
		if (openRoom)
		{
			this.CreatePublicRoom();
			return;
		}
		TutorialManager.InTutorial = false;
		this.ButtonInteractivity(true);
	}

	// Token: 0x06001483 RID: 5251 RVA: 0x000801A8 File Offset: 0x0007E3A8
	private void CreatePublicRoom()
	{
		RoomOptions roomOptions = new RoomOptions();
		roomOptions.MaxPlayers = (TutorialManager.InTutorial ? 1 : 4);
		roomOptions.IsVisible = true;
		NetworkManager.instance.CreateNewGame(roomOptions);
		AudioManager.PlayInterfaceSFX(this.StartSFX, 1f, 0f);
	}

	// Token: 0x06001484 RID: 5252 RVA: 0x000801F4 File Offset: 0x0007E3F4
	private void CreatePrivateRoom()
	{
		RoomOptions roomOptions = new RoomOptions();
		roomOptions.MaxPlayers = (TutorialManager.InTutorial ? 1 : 4);
		roomOptions.IsVisible = false;
		NetworkManager.instance.CreateNewGame(roomOptions);
	}

	// Token: 0x06001485 RID: 5253 RVA: 0x0008022C File Offset: 0x0007E42C
	private void CreateOrJoinRandom()
	{
		this.ButtonInteractivity(false);
		this.SetDetailText("Joining Random Room...", 0f);
		NetworkManager.instance.WantOpenRoom = true;
		NetworkManager.instance.CreateOrJoinRandom();
		AudioManager.PlayInterfaceSFX(this.StartSFX, 1f, 0f);
	}

	// Token: 0x06001486 RID: 5254 RVA: 0x0008027A File Offset: 0x0007E47A
	public void VerifyInput(string codeInput)
	{
		codeInput = codeInput.ToUpper();
		this.gameInput.SetTextWithoutNotify(codeInput);
		this.JoinButton.interactable = NetworkManager.IsValidCode(codeInput);
	}

	// Token: 0x06001487 RID: 5255 RVA: 0x000802A1 File Offset: 0x0007E4A1
	public void CodeEditEnded()
	{
		if (Input.GetKeyDown(KeyCode.Return))
		{
			this.JoinGameButton();
		}
	}

	// Token: 0x06001488 RID: 5256 RVA: 0x000802B4 File Offset: 0x0007E4B4
	public void JoinGameButton()
	{
		string text = this.gameInput.text;
		if (!NetworkManager.IsValidCode(text))
		{
			this.SetDetailText("Invalid Code!", 1.5f);
			return;
		}
		this.ButtonInteractivity(false);
		this.SetDetailText("Joining Room...", 0f);
		NetworkManager.instance.TryJoinRoom(text.ToUpper());
	}

	// Token: 0x06001489 RID: 5257 RVA: 0x00080310 File Offset: 0x0007E510
	public void JoinFailed(short returnCode)
	{
		if (returnCode == 32758)
		{
			this.SetDetailText("Room Not Found", 1.5f);
		}
		else if (returnCode == 32764)
		{
			this.SetDetailText("Invalid: Raid in Progress", 1.5f);
		}
		else
		{
			this.SetDetailText("Joining Failed :(", 1.5f);
		}
		this.ButtonInteractivity(true);
	}

	// Token: 0x0600148A RID: 5258 RVA: 0x00080368 File Offset: 0x0007E568
	public void FailedToJoinRandom()
	{
		this.SetDetailText("Creating New Room...", 1.5f);
	}

	// Token: 0x0600148B RID: 5259 RVA: 0x0008037A File Offset: 0x0007E57A
	public override void OnLeftRoom()
	{
		this.ButtonInteractivity(true);
		this.gameInput.text = "";
		this.OnControllerSelect();
		this.CheckWasAFKicked();
	}

	// Token: 0x0600148C RID: 5260 RVA: 0x0008039F File Offset: 0x0007E59F
	private void MPOpenAction()
	{
		this.MPOpen("");
	}

	// Token: 0x0600148D RID: 5261 RVA: 0x000803AC File Offset: 0x0007E5AC
	public void MPOpen(string autoCode = "")
	{
		this.inMPGroup = true;
		this.gameInput.interactable = false;
		this.JoinButton.interactable = false;
		this.CreateButton.interactable = false;
		this.QuickmatchButton.interactable = false;
		this.RegionSelector.interactable = false;
		if (autoCode.Length > 0)
		{
			this.gameInput.SetTextWithoutNotify(autoCode);
		}
		base.StopAllCoroutines();
		base.StartCoroutine(this.ConnectCheck());
		NetworkManager.instance.GoOnline();
	}

	// Token: 0x0600148E RID: 5262 RVA: 0x0008042E File Offset: 0x0007E62E
	private void MPClose()
	{
		this.inMPGroup = false;
		base.StopAllCoroutines();
		NetworkManager.instance.GoOffline();
		this.OnControllerSelect();
	}

	// Token: 0x0600148F RID: 5263 RVA: 0x0008044D File Offset: 0x0007E64D
	public void FixConnectionAndJoin(string autoCode)
	{
		this.MPOpen(autoCode);
		UnityEngine.Debug.Log("Fixing Connection -> Joining lobby");
		PhotonNetwork.JoinLobby();
	}

	// Token: 0x06001490 RID: 5264 RVA: 0x00080466 File Offset: 0x0007E666
	private IEnumerator ConnectCheck()
	{
		while (!PhotonNetwork.InLobby)
		{
			yield return true;
			this.SetDetailText(PhotonNetwork.NetworkClientState.ToString(), 0f);
		}
		yield return true;
		this.SetDetailText("Connected!", 0f);
		this.gameInput.interactable = true;
		this.CreateButton.interactable = true;
		this.QuickmatchButton.interactable = true;
		this.RegionSelector.interactable = true;
		this.VerifyInput(this.gameInput.text);
		this.OnControllerSelect();
		this.HideDetail();
		yield break;
	}

	// Token: 0x06001491 RID: 5265 RVA: 0x00080475 File Offset: 0x0007E675
	private void OnMPRegionChanged(SystemSetting systemSettingID, int curID)
	{
		if (this.ReconnectRoutine != null)
		{
			base.StopCoroutine(this.ReconnectRoutine);
		}
		this.ReconnectRoutine = base.StartCoroutine("ChangeRegionSequence");
	}

	// Token: 0x06001492 RID: 5266 RVA: 0x0008049C File Offset: 0x0007E69C
	private IEnumerator ChangeRegionSequence()
	{
		yield return new WaitForSeconds(1f);
		this.gameInput.interactable = false;
		this.JoinButton.interactable = false;
		this.CreateButton.interactable = false;
		this.QuickmatchButton.interactable = false;
		this.RegionSelector.interactable = false;
		NetworkManager.instance.Reconnect();
		yield return true;
		yield return this.ConnectCheck();
		yield break;
	}

	// Token: 0x06001493 RID: 5267 RVA: 0x000804AC File Offset: 0x0007E6AC
	private void CheckWasAFKicked()
	{
		if (StateManager.DidGetAFKKicked)
		{
			string text = "You were removed from the lobby for inactivity.";
			if (StateManager.DoesHaveKickRewards)
			{
				text += "\n\nPartial game rewards are waiting in the Library.";
			}
			ErrorPrompt.OpenPrompt(text, null);
			StateManager.DidGetAFKKicked = false;
			StateManager.DoesHaveKickRewards = false;
			return;
		}
		if (StateManager.DidGetServerKicked)
		{
			string text2 = "You were removed from the lobby by the Host.";
			if (StateManager.DoesHaveKickRewards)
			{
				text2 += "\n\nPartial game rewards are waiting in the Library.";
			}
			ErrorPrompt.OpenPrompt(text2, null);
			StateManager.DidGetServerKicked = false;
			StateManager.DoesHaveKickRewards = false;
		}
	}

	// Token: 0x06001494 RID: 5268 RVA: 0x00080520 File Offset: 0x0007E720
	private void ButtonInteractivity(bool val)
	{
		this.JoinButton.interactable = val;
		this.MPButton.interactable = val;
		this.CreateButton.interactable = val;
		this.QuickmatchButton.interactable = val;
		this.MPBackButton.interactable = val;
		this.RegionSelector.interactable = val;
	}

	// Token: 0x06001495 RID: 5269 RVA: 0x00080575 File Offset: 0x0007E775
	public void DoQuit()
	{
		GameStats.TryTakeSnapshot();
		UnlockManager.TryTakeSnapshot();
		Application.Quit();
	}

	// Token: 0x06001496 RID: 5270 RVA: 0x00080586 File Offset: 0x0007E786
	public MainPanel()
	{
	}

	// Token: 0x040013B2 RID: 5042
	public static MainPanel instance;

	// Token: 0x040013B3 RID: 5043
	public TextMeshProUGUI VersionText;

	// Token: 0x040013B4 RID: 5044
	private UIPanel panel;

	// Token: 0x040013B5 RID: 5045
	public GameObject BadLoad;

	// Token: 0x040013B6 RID: 5046
	public AudioClip StartSFX;

	// Token: 0x040013B7 RID: 5047
	public GameObject CoreBox;

	// Token: 0x040013B8 RID: 5048
	public GameObject DemoDisplay;

	// Token: 0x040013B9 RID: 5049
	public CanvasGroup MainGroup;

	// Token: 0x040013BA RID: 5050
	public Button SingleplayerButton;

	// Token: 0x040013BB RID: 5051
	public Button MPButton;

	// Token: 0x040013BC RID: 5052
	public Button SettingsButton;

	// Token: 0x040013BD RID: 5053
	[TextArea(4, 4)]
	public string CorruptSavePrompt;

	// Token: 0x040013BE RID: 5054
	[Header("Multiplayer")]
	public CanvasGroup MPGroup;

	// Token: 0x040013BF RID: 5055
	public TMP_InputField gameInput;

	// Token: 0x040013C0 RID: 5056
	public Button QuickmatchButton;

	// Token: 0x040013C1 RID: 5057
	public Button CreateButton;

	// Token: 0x040013C2 RID: 5058
	public Button JoinButton;

	// Token: 0x040013C3 RID: 5059
	public SelectorSetting RegionSelector;

	// Token: 0x040013C4 RID: 5060
	public Button MPBackButton;

	// Token: 0x040013C5 RID: 5061
	public TextMeshProUGUI RegionText;

	// Token: 0x040013C6 RID: 5062
	[TextArea]
	public string CreateGamePrompt;

	// Token: 0x040013C7 RID: 5063
	private bool inMPGroup;

	// Token: 0x040013C8 RID: 5064
	private Coroutine ReconnectRoutine;

	// Token: 0x040013C9 RID: 5065
	private int TutorialRoomMode;

	// Token: 0x040013CA RID: 5066
	public GameObject FTBox;

	// Token: 0x040013CB RID: 5067
	public Button StartFTUXButton;

	// Token: 0x040013CC RID: 5068
	public TextMeshProUGUI DetailText;

	// Token: 0x040013CD RID: 5069
	public CanvasGroup DetailGroup;

	// Token: 0x040013CE RID: 5070
	public Button FTUXSettingsButton;

	// Token: 0x040013CF RID: 5071
	public SliderSetting VolumeSlider;

	// Token: 0x040013D0 RID: 5072
	private bool wantDetail;

	// Token: 0x040013D1 RID: 5073
	private Coroutine hideRoutine;

	// Token: 0x020005B2 RID: 1458
	[CompilerGenerated]
	private sealed class <HideDelay>d__41 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x060025C8 RID: 9672 RVA: 0x000D22F9 File Offset: 0x000D04F9
		[DebuggerHidden]
		public <HideDelay>d__41(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x060025C9 RID: 9673 RVA: 0x000D2308 File Offset: 0x000D0508
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x060025CA RID: 9674 RVA: 0x000D230C File Offset: 0x000D050C
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			MainPanel mainPanel = this;
			if (num == 0)
			{
				this.<>1__state = -1;
				this.<>2__current = new WaitForSeconds(delayTime);
				this.<>1__state = 1;
				return true;
			}
			if (num != 1)
			{
				return false;
			}
			this.<>1__state = -1;
			mainPanel.HideDetail();
			return false;
		}

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x060025CB RID: 9675 RVA: 0x000D235F File Offset: 0x000D055F
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x060025CC RID: 9676 RVA: 0x000D2367 File Offset: 0x000D0567
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x060025CD RID: 9677 RVA: 0x000D236E File Offset: 0x000D056E
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x04002840 RID: 10304
		private int <>1__state;

		// Token: 0x04002841 RID: 10305
		private object <>2__current;

		// Token: 0x04002842 RID: 10306
		public float delayTime;

		// Token: 0x04002843 RID: 10307
		public MainPanel <>4__this;
	}

	// Token: 0x020005B3 RID: 1459
	[CompilerGenerated]
	private sealed class <EnterTutorial>d__46 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x060025CE RID: 9678 RVA: 0x000D2376 File Offset: 0x000D0576
		[DebuggerHidden]
		public <EnterTutorial>d__46(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x060025CF RID: 9679 RVA: 0x000D2385 File Offset: 0x000D0585
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x060025D0 RID: 9680 RVA: 0x000D2388 File Offset: 0x000D0588
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			MainPanel mainPanel = this;
			switch (num)
			{
			case 0:
				this.<>1__state = -1;
				TutorialManager.instance.StartTutorial();
				if (mainPanel.TutorialRoomMode == 0)
				{
					if (PhotonNetwork.IsConnected)
					{
						PhotonNetwork.Disconnect();
					}
				}
				else
				{
					PhotonNetwork.OfflineMode = false;
					if (!PhotonNetwork.IsConnected)
					{
						NetworkManager.instance.GoOnline();
						goto IL_AF;
					}
					goto IL_AF;
				}
				break;
			case 1:
				this.<>1__state = -1;
				break;
			case 2:
				this.<>1__state = -1;
				goto IL_AF;
			default:
				return false;
			}
			if (!PhotonNetwork.IsConnected)
			{
				PhotonNetwork.OfflineMode = true;
				mainPanel.OfflineGame();
				return false;
			}
			this.<>2__current = true;
			this.<>1__state = 1;
			return true;
			IL_AF:
			if (!PhotonNetwork.IsConnected || (PhotonNetwork.NetworkClientState != ClientState.JoinedLobby && PhotonNetwork.NetworkClientState != ClientState.ConnectedToMasterServer))
			{
				this.<>2__current = true;
				this.<>1__state = 2;
				return true;
			}
			mainPanel.CreateGameButton();
			AudioManager.PlayInterfaceSFX(mainPanel.StartSFX, 1f, 0f);
			return false;
		}

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x060025D1 RID: 9681 RVA: 0x000D2478 File Offset: 0x000D0678
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x060025D2 RID: 9682 RVA: 0x000D2480 File Offset: 0x000D0680
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x060025D3 RID: 9683 RVA: 0x000D2487 File Offset: 0x000D0687
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x04002844 RID: 10308
		private int <>1__state;

		// Token: 0x04002845 RID: 10309
		private object <>2__current;

		// Token: 0x04002846 RID: 10310
		public MainPanel <>4__this;
	}

	// Token: 0x020005B4 RID: 1460
	[CompilerGenerated]
	private sealed class <ConnectCheck>d__63 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x060025D4 RID: 9684 RVA: 0x000D248F File Offset: 0x000D068F
		[DebuggerHidden]
		public <ConnectCheck>d__63(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x060025D5 RID: 9685 RVA: 0x000D249E File Offset: 0x000D069E
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x060025D6 RID: 9686 RVA: 0x000D24A0 File Offset: 0x000D06A0
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			MainPanel mainPanel = this;
			switch (num)
			{
			case 0:
				this.<>1__state = -1;
				break;
			case 1:
				this.<>1__state = -1;
				mainPanel.SetDetailText(PhotonNetwork.NetworkClientState.ToString(), 0f);
				break;
			case 2:
				this.<>1__state = -1;
				mainPanel.SetDetailText("Connected!", 0f);
				mainPanel.gameInput.interactable = true;
				mainPanel.CreateButton.interactable = true;
				mainPanel.QuickmatchButton.interactable = true;
				mainPanel.RegionSelector.interactable = true;
				mainPanel.VerifyInput(mainPanel.gameInput.text);
				mainPanel.OnControllerSelect();
				mainPanel.HideDetail();
				return false;
			default:
				return false;
			}
			if (PhotonNetwork.InLobby)
			{
				this.<>2__current = true;
				this.<>1__state = 2;
				return true;
			}
			this.<>2__current = true;
			this.<>1__state = 1;
			return true;
		}

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x060025D7 RID: 9687 RVA: 0x000D2593 File Offset: 0x000D0793
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x060025D8 RID: 9688 RVA: 0x000D259B File Offset: 0x000D079B
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x060025D9 RID: 9689 RVA: 0x000D25A2 File Offset: 0x000D07A2
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x04002847 RID: 10311
		private int <>1__state;

		// Token: 0x04002848 RID: 10312
		private object <>2__current;

		// Token: 0x04002849 RID: 10313
		public MainPanel <>4__this;
	}

	// Token: 0x020005B5 RID: 1461
	[CompilerGenerated]
	private sealed class <ChangeRegionSequence>d__65 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x060025DA RID: 9690 RVA: 0x000D25AA File Offset: 0x000D07AA
		[DebuggerHidden]
		public <ChangeRegionSequence>d__65(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x060025DB RID: 9691 RVA: 0x000D25B9 File Offset: 0x000D07B9
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x060025DC RID: 9692 RVA: 0x000D25BC File Offset: 0x000D07BC
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			MainPanel mainPanel = this;
			switch (num)
			{
			case 0:
				this.<>1__state = -1;
				this.<>2__current = new WaitForSeconds(1f);
				this.<>1__state = 1;
				return true;
			case 1:
				this.<>1__state = -1;
				mainPanel.gameInput.interactable = false;
				mainPanel.JoinButton.interactable = false;
				mainPanel.CreateButton.interactable = false;
				mainPanel.QuickmatchButton.interactable = false;
				mainPanel.RegionSelector.interactable = false;
				NetworkManager.instance.Reconnect();
				this.<>2__current = true;
				this.<>1__state = 2;
				return true;
			case 2:
				this.<>1__state = -1;
				this.<>2__current = mainPanel.ConnectCheck();
				this.<>1__state = 3;
				return true;
			case 3:
				this.<>1__state = -1;
				return false;
			default:
				return false;
			}
		}

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x060025DD RID: 9693 RVA: 0x000D2695 File Offset: 0x000D0895
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x060025DE RID: 9694 RVA: 0x000D269D File Offset: 0x000D089D
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x060025DF RID: 9695 RVA: 0x000D26A4 File Offset: 0x000D08A4
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x0400284A RID: 10314
		private int <>1__state;

		// Token: 0x0400284B RID: 10315
		private object <>2__current;

		// Token: 0x0400284C RID: 10316
		public MainPanel <>4__this;
	}
}
