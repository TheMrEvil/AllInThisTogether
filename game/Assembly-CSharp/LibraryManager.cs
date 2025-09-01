using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using AtmosphericHeightFog;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

// Token: 0x02000111 RID: 273
public class LibraryManager : MonoBehaviour
{
	// Token: 0x06000CD0 RID: 3280 RVA: 0x00051FE4 File Offset: 0x000501E4
	private void Awake()
	{
		LibraryManager.DoneLoading = false;
		bool flag = false;
		if (LibraryManager.DidLoad || flag)
		{
			this.LoadingCamera.transform.SetPositionAndRotation(this.CamLoc_Main.position, this.CamLoc_Main.rotation);
			LibraryManager.DoneLoading = true;
			LibraryManager.DidLoad = true;
		}
		else
		{
			base.StartCoroutine("LoadFadeIn");
		}
		GameplayManager.OnGameStateChanged = (Action<GameState, GameState>)Delegate.Combine(GameplayManager.OnGameStateChanged, new Action<GameState, GameState>(this.OnGameStateChanged));
		NetworkManager.JoinedRoom = (Action)Delegate.Combine(NetworkManager.JoinedRoom, new Action(this.JoinedRoom));
		NetworkManager.LeftRoom = (Action)Delegate.Combine(NetworkManager.LeftRoom, new Action(this.LeftRoom));
		LibraryManager.instance = this;
		this.LibraryAugments = new Augments();
	}

	// Token: 0x06000CD1 RID: 3281 RVA: 0x000520B3 File Offset: 0x000502B3
	private IEnumerator LoadFadeIn()
	{
		this.LoadingCamera.GetComponent<Animator>().Play("Library_Start");
		this.LoadingCamera.transform.SetPositionAndRotation(this.CamLoc_Loading.position, this.CamLoc_Loading.rotation);
		while (!LibraryManager.DoneLoading)
		{
			yield return true;
		}
		AudioManager.PlayInterfaceSFX(this.LoadMoveSFX, 1f, 0f);
		LibraryManager.DidLoad = true;
		this.LoadingCamera.GetComponent<Animator>().Play("Move");
		yield return new WaitForSeconds(this.LoadMoveTime - 0.15f);
		PanelManager.instance.GoToPanel(PanelType.Main);
		yield break;
	}

	// Token: 0x06000CD2 RID: 3282 RVA: 0x000520C2 File Offset: 0x000502C2
	public void LoadTransition()
	{
		LibraryManager.DoneLoading = true;
	}

	// Token: 0x06000CD3 RID: 3283 RVA: 0x000520CA File Offset: 0x000502CA
	private IEnumerator Start()
	{
		this.RegenerateBooks();
		yield return true;
		yield return true;
		this.Volume_Light.SetActive(false);
		yield return true;
		this.Volume_Light.SetActive(true);
		if (PlatformSetup.IsSteamDeck)
		{
			this.Volume_Light.SetActive(false);
		}
		if (Settings.HasCompletedLibraryTutorial(LibraryTutorial.Bindings) && !Settings.HasCompletedUITutorial(UITutorial.Tutorial.Bindings) && !Settings.GetBool(SystemSetting.Photosensitivity, false))
		{
			Fountain.instance.WaveTornado_Base.Play();
			Fountain.instance.WaveTornado_Extra.Play();
		}
		while (GameplayManager.CurState != GameState.Hub)
		{
			yield return true;
		}
		if (TutorialManager.InTutorial)
		{
			this.LibraryEntered();
		}
		else
		{
			yield return true;
			yield return true;
			this.LibraryEntered();
		}
		yield break;
	}

	// Token: 0x06000CD4 RID: 3284 RVA: 0x000520D9 File Offset: 0x000502D9
	private void JoinedRoom()
	{
		base.StopAllCoroutines();
		base.StartCoroutine("EnterLibraryNet");
	}

	// Token: 0x06000CD5 RID: 3285 RVA: 0x000520ED File Offset: 0x000502ED
	private IEnumerator EnterLibraryNet()
	{
		while (GoalManager.instance == null)
		{
			yield return true;
		}
		this.LibraryEntered();
		yield break;
	}

	// Token: 0x06000CD6 RID: 3286 RVA: 0x000520FC File Offset: 0x000502FC
	private void LeftRoom()
	{
		this.didSetup = false;
		Library_RaidControl.instance.Reset();
		LibraryManager.WantAntechamberSpawn = false;
		LibraryManager.OnExitLibrary();
	}

	// Token: 0x06000CD7 RID: 3287 RVA: 0x0005211C File Offset: 0x0005031C
	private void LibraryEntered()
	{
		if (this.didSetup)
		{
			return;
		}
		if (TutorialManager.InTutorial)
		{
			UnityEngine.Debug.Log("Starting Library Tutorial Move");
			this.LoadingCamera.gameObject.SetActive(true);
			this.LoadingCamera.depth = 1f;
			base.StartCoroutine("TutorialRoutine");
		}
		else
		{
			GameStats.SaveIfNeeded();
			this.SpawnTargetDummies();
			base.Invoke("RewardDelayed", 1f);
			base.Invoke("LoadGlobalStats", 0.25f);
			Library_LorePages.instance.OnEnteredLibrary();
			string key = "public";
			Room currentRoom = PhotonNetwork.CurrentRoom;
			StateManager.SetRoomOpen(StateManager.GetBool(key, currentRoom == null || currentRoom.IsVisible));
			GoalManager.instance.SyncLibraryObjects();
			InkManager.instance.PauseQuoteID = LoreDB.SelectLibraryQuote();
		}
		PlayerChoicePanel.ChoiceTotal = 0;
		PlatformSetup.SetSteamRoomCode();
		Action onLibraryEntered = this.OnLibraryEntered;
		if (onLibraryEntered != null)
		{
			onLibraryEntered();
		}
		this.didSetup = true;
		LibraryManager.WantAntechamberSpawn = false;
	}

	// Token: 0x06000CD8 RID: 3288 RVA: 0x00052209 File Offset: 0x00050409
	private void LoadGlobalStats()
	{
		CodexStatsPanel.instance.RunHistory.GlobalStatsDisplay.RefreshTomeStats();
	}

	// Token: 0x06000CD9 RID: 3289 RVA: 0x0005221F File Offset: 0x0005041F
	private void RewardDelayed()
	{
		Progression.CreateLossQuillReward();
		LibraryInfoWidget libraryInfoWidget = LibraryInfoWidget.instance;
		if (libraryInfoWidget == null)
		{
			return;
		}
		libraryInfoWidget.LoadRewardToasts();
	}

	// Token: 0x06000CDA RID: 3290 RVA: 0x00052235 File Offset: 0x00050435
	public static void FinishedTutorial(LibraryTutorial tutorial)
	{
		if (LibraryManager.InLibraryTutorial && LibraryManager.CurrentStep == tutorial)
		{
			LibraryManager.InLibraryTutorial = false;
		}
		Settings.LibraryTutorialCompleted(tutorial);
	}

	// Token: 0x06000CDB RID: 3291 RVA: 0x00052252 File Offset: 0x00050452
	private IEnumerator TutorialRoutine()
	{
		this.LoadingCamera.GetComponent<Animator>().enabled = false;
		yield return true;
		TutorialManager.instance.LibraryStart();
		PlayerControl.MyCamera.enabled = false;
		PlayerControl.myInstance.Audio.Listener.enabled = false;
		yield return true;
		AudioManager.PlayInterfaceSFX(this.FTUXStartSFX, 1f, 0f);
		float t = 0f;
		Transform camT = this.LoadingCamera.transform;
		Vector3 startPos = camT.position;
		Quaternion startRot = camT.rotation;
		while (t < 1f)
		{
			t += Time.deltaTime / this.CamMoveTime;
			Vector3 position = Vector3.Lerp(startPos, this.CameraTargetPos.position, t);
			Quaternion rotation = Quaternion.Lerp(startRot, this.CameraTargetPos.rotation, t);
			camT.SetPositionAndRotation(position, rotation);
			yield return true;
		}
		yield break;
	}

	// Token: 0x06000CDC RID: 3292 RVA: 0x00052264 File Offset: 0x00050464
	private void Update()
	{
		float b = 280f;
		if (PlayerControl.myInstance == null)
		{
			b = 400f;
			this.VolumeFog.mainCamera = this.LoadingCamera;
		}
		this.VolumeFog.fogDistanceEnd = Mathf.Lerp(this.VolumeFog.fogDistanceEnd, b, Time.deltaTime * 1f);
	}

	// Token: 0x06000CDD RID: 3293 RVA: 0x000522C4 File Offset: 0x000504C4
	public void RegenerateBooks()
	{
		Bookshelf[] array = UnityEngine.Object.FindObjectsOfType<Bookshelf>(false);
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Generate(this.Data);
		}
	}

	// Token: 0x06000CDE RID: 3294 RVA: 0x000522F4 File Offset: 0x000504F4
	private void SpawnTargetDummies()
	{
		if (!PhotonNetwork.IsMasterClient || !PhotonNetwork.InRoom)
		{
			return;
		}
		AIManager.SpawnAIExplicit("Unique/TDummy_Elite", this.SpawnPoints[0].position, this.SpawnPoints[0].transform.forward);
		AIManager.SpawnAIExplicit("Splice/TDummy_Splice", this.SpawnPoints[1].position, this.SpawnPoints[1].transform.forward);
		AIManager.SpawnAIExplicit("Raving/TDummy_Raving", this.SpawnPoints[2].position, this.SpawnPoints[2].transform.forward);
	}

	// Token: 0x06000CDF RID: 3295 RVA: 0x0005238E File Offset: 0x0005058E
	private void StartBindings()
	{
		Fountain.instance.WaveTornado_Base.Play();
		base.Invoke("FountainBindingDelayed", 1.5f);
	}

	// Token: 0x06000CE0 RID: 3296 RVA: 0x000523AF File Offset: 0x000505AF
	private void FountainBindingDelayed()
	{
		Fountain.instance.WaveTornado_Extra.Play();
	}

	// Token: 0x06000CE1 RID: 3297 RVA: 0x000523C0 File Offset: 0x000505C0
	private void BindingsCompleted()
	{
		Fountain.instance.WaveTornado_Extra.Stop();
		Fountain.instance.WaveTornado_Base.Stop();
	}

	// Token: 0x06000CE2 RID: 3298 RVA: 0x000523E0 File Offset: 0x000505E0
	public void PlayTransfer()
	{
		AudioManager.PlayLoudClipAtPoint(this.TransferSFX, Fountain.instance.transform.position, 1f, 1f, 0f, 10f, 250f);
		ParticleSystem[] transferFX = this.TransferFX;
		for (int i = 0; i < transferFX.Length; i++)
		{
			transferFX[i].Play();
		}
		PlatformSetup.ClearSteamRoomCode();
		this.ConfirmCore();
		if (PanelManager.CurPanel != PanelType.GameInvisible && PanelManager.CurPanel != PanelType.Settings)
		{
			PanelManager.instance.PopPanel();
		}
	}

	// Token: 0x06000CE3 RID: 3299 RVA: 0x00052464 File Offset: 0x00050664
	private void ConfirmCore()
	{
		if (PlayerControl.myInstance.actions.core.Root.magicColor != MagicColor.Neutral)
		{
			return;
		}
		PlayerControl.myInstance.actions.SetCore(this.CoreFallback);
		Settings.SaveLoadout();
		PlayerControl.myInstance.AugmentsChanged();
	}

	// Token: 0x06000CE4 RID: 3300 RVA: 0x000524B2 File Offset: 0x000506B2
	public void OnGameStateChanged(GameState from, GameState to)
	{
		if (to == GameState.Hub_Preparing)
		{
			this.PlayTransfer();
		}
		if (to == GameState.Hub_Bindings)
		{
			this.StartBindings();
		}
		if (from == GameState.Hub_Bindings)
		{
			this.BindingsCompleted();
		}
	}

	// Token: 0x06000CE5 RID: 3301 RVA: 0x000524D4 File Offset: 0x000506D4
	private static void OnExitLibrary()
	{
		if (PhotonNetwork.InRoom && !PhotonNetwork.IsMasterClient)
		{
			return;
		}
		StateManager.SetValue("LibSecretRoom", false);
	}

	// Token: 0x06000CE6 RID: 3302 RVA: 0x000524F8 File Offset: 0x000506F8
	private void OnDestroy()
	{
		GameplayManager.OnGameStateChanged = (Action<GameState, GameState>)Delegate.Remove(GameplayManager.OnGameStateChanged, new Action<GameState, GameState>(this.OnGameStateChanged));
		NetworkManager.JoinedRoom = (Action)Delegate.Remove(NetworkManager.JoinedRoom, new Action(this.JoinedRoom));
		NetworkManager.LeftRoom = (Action)Delegate.Remove(NetworkManager.LeftRoom, new Action(this.LeftRoom));
		if (Application.isPlaying)
		{
			UnityMainThreadDispatcher.Instance().Invoke(new Action(LibraryManager.OnExitLibrary), 1f);
		}
	}

	// Token: 0x06000CE7 RID: 3303 RVA: 0x00052587 File Offset: 0x00050787
	public LibraryManager()
	{
	}

	// Token: 0x04000A31 RID: 2609
	public static LibraryManager instance;

	// Token: 0x04000A32 RID: 2610
	public bool DebugSkipIntro;

	// Token: 0x04000A33 RID: 2611
	public BookshelfData Data;

	// Token: 0x04000A34 RID: 2612
	public GameObject Volume_Light;

	// Token: 0x04000A35 RID: 2613
	public HeightFogGlobal VolumeFog;

	// Token: 0x04000A36 RID: 2614
	public Transform CamLoc_Loading;

	// Token: 0x04000A37 RID: 2615
	public Transform CamLoc_Main;

	// Token: 0x04000A38 RID: 2616
	public AudioClip LoadMoveSFX;

	// Token: 0x04000A39 RID: 2617
	public AnimationCurve LoadMoveCurve;

	// Token: 0x04000A3A RID: 2618
	public float LoadMoveTime = 3f;

	// Token: 0x04000A3B RID: 2619
	public static bool DidLoad;

	// Token: 0x04000A3C RID: 2620
	public static bool DoneLoading;

	// Token: 0x04000A3D RID: 2621
	public AudioClip TransferSFX;

	// Token: 0x04000A3E RID: 2622
	public ParticleSystem[] TransferFX;

	// Token: 0x04000A3F RID: 2623
	public GenreTree BaseGenere;

	// Token: 0x04000A40 RID: 2624
	public AILayoutRef BaseLayout;

	// Token: 0x04000A41 RID: 2625
	public AudioClip BindingStartSFX;

	// Token: 0x04000A42 RID: 2626
	public AudioClip FTUXStartSFX;

	// Token: 0x04000A43 RID: 2627
	public Camera LoadingCamera;

	// Token: 0x04000A44 RID: 2628
	public float CamMoveTime = 3f;

	// Token: 0x04000A45 RID: 2629
	public AnimationCurve CamMoveCurve;

	// Token: 0x04000A46 RID: 2630
	public Transform CameraTargetPos;

	// Token: 0x04000A47 RID: 2631
	public EntityIndicator BookstandIndicator;

	// Token: 0x04000A48 RID: 2632
	public EntityIndicator AbilityIndicator;

	// Token: 0x04000A49 RID: 2633
	public EntityIndicator TomesIndicator;

	// Token: 0x04000A4A RID: 2634
	public EntityIndicator MetaIndicator;

	// Token: 0x04000A4B RID: 2635
	public GenreTree BindingWinRequirement;

	// Token: 0x04000A4C RID: 2636
	public AugmentTree CoreFallback;

	// Token: 0x04000A4D RID: 2637
	public static bool InLibraryTutorial;

	// Token: 0x04000A4E RID: 2638
	public static bool WantAntechamberSpawn;

	// Token: 0x04000A4F RID: 2639
	public static LibraryTutorial CurrentStep;

	// Token: 0x04000A50 RID: 2640
	public Augments LibraryAugments;

	// Token: 0x04000A51 RID: 2641
	public Action OnPlayerLoadoutChanged;

	// Token: 0x04000A52 RID: 2642
	public Action OnLibraryEntered;

	// Token: 0x04000A53 RID: 2643
	public Transform[] SpawnPoints;

	// Token: 0x04000A54 RID: 2644
	private bool didSetup;

	// Token: 0x02000512 RID: 1298
	[CompilerGenerated]
	private sealed class <LoadFadeIn>d__37 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x060023B1 RID: 9137 RVA: 0x000CB9FE File Offset: 0x000C9BFE
		[DebuggerHidden]
		public <LoadFadeIn>d__37(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x060023B2 RID: 9138 RVA: 0x000CBA0D File Offset: 0x000C9C0D
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x060023B3 RID: 9139 RVA: 0x000CBA10 File Offset: 0x000C9C10
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			LibraryManager libraryManager = this;
			switch (num)
			{
			case 0:
				this.<>1__state = -1;
				libraryManager.LoadingCamera.GetComponent<Animator>().Play("Library_Start");
				libraryManager.LoadingCamera.transform.SetPositionAndRotation(libraryManager.CamLoc_Loading.position, libraryManager.CamLoc_Loading.rotation);
				break;
			case 1:
				this.<>1__state = -1;
				break;
			case 2:
				this.<>1__state = -1;
				PanelManager.instance.GoToPanel(PanelType.Main);
				return false;
			default:
				return false;
			}
			if (LibraryManager.DoneLoading)
			{
				AudioManager.PlayInterfaceSFX(libraryManager.LoadMoveSFX, 1f, 0f);
				LibraryManager.DidLoad = true;
				libraryManager.LoadingCamera.GetComponent<Animator>().Play("Move");
				this.<>2__current = new WaitForSeconds(libraryManager.LoadMoveTime - 0.15f);
				this.<>1__state = 2;
				return true;
			}
			this.<>2__current = true;
			this.<>1__state = 1;
			return true;
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x060023B4 RID: 9140 RVA: 0x000CBB09 File Offset: 0x000C9D09
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x060023B5 RID: 9141 RVA: 0x000CBB11 File Offset: 0x000C9D11
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x060023B6 RID: 9142 RVA: 0x000CBB18 File Offset: 0x000C9D18
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x040025B5 RID: 9653
		private int <>1__state;

		// Token: 0x040025B6 RID: 9654
		private object <>2__current;

		// Token: 0x040025B7 RID: 9655
		public LibraryManager <>4__this;
	}

	// Token: 0x02000513 RID: 1299
	[CompilerGenerated]
	private sealed class <Start>d__39 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x060023B7 RID: 9143 RVA: 0x000CBB20 File Offset: 0x000C9D20
		[DebuggerHidden]
		public <Start>d__39(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x060023B8 RID: 9144 RVA: 0x000CBB2F File Offset: 0x000C9D2F
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x060023B9 RID: 9145 RVA: 0x000CBB34 File Offset: 0x000C9D34
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			LibraryManager libraryManager = this;
			switch (num)
			{
			case 0:
				this.<>1__state = -1;
				libraryManager.RegenerateBooks();
				this.<>2__current = true;
				this.<>1__state = 1;
				return true;
			case 1:
				this.<>1__state = -1;
				this.<>2__current = true;
				this.<>1__state = 2;
				return true;
			case 2:
				this.<>1__state = -1;
				libraryManager.Volume_Light.SetActive(false);
				this.<>2__current = true;
				this.<>1__state = 3;
				return true;
			case 3:
				this.<>1__state = -1;
				libraryManager.Volume_Light.SetActive(true);
				if (PlatformSetup.IsSteamDeck)
				{
					libraryManager.Volume_Light.SetActive(false);
				}
				if (Settings.HasCompletedLibraryTutorial(LibraryTutorial.Bindings) && !Settings.HasCompletedUITutorial(UITutorial.Tutorial.Bindings) && !Settings.GetBool(SystemSetting.Photosensitivity, false))
				{
					Fountain.instance.WaveTornado_Base.Play();
					Fountain.instance.WaveTornado_Extra.Play();
				}
				break;
			case 4:
				this.<>1__state = -1;
				break;
			case 5:
				this.<>1__state = -1;
				this.<>2__current = true;
				this.<>1__state = 6;
				return true;
			case 6:
				this.<>1__state = -1;
				libraryManager.LibraryEntered();
				return false;
			default:
				return false;
			}
			if (GameplayManager.CurState != GameState.Hub)
			{
				this.<>2__current = true;
				this.<>1__state = 4;
				return true;
			}
			if (!TutorialManager.InTutorial)
			{
				this.<>2__current = true;
				this.<>1__state = 5;
				return true;
			}
			libraryManager.LibraryEntered();
			return false;
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x060023BA RID: 9146 RVA: 0x000CBCAB File Offset: 0x000C9EAB
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x060023BB RID: 9147 RVA: 0x000CBCB3 File Offset: 0x000C9EB3
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x060023BC RID: 9148 RVA: 0x000CBCBA File Offset: 0x000C9EBA
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x040025B8 RID: 9656
		private int <>1__state;

		// Token: 0x040025B9 RID: 9657
		private object <>2__current;

		// Token: 0x040025BA RID: 9658
		public LibraryManager <>4__this;
	}

	// Token: 0x02000514 RID: 1300
	[CompilerGenerated]
	private sealed class <EnterLibraryNet>d__41 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x060023BD RID: 9149 RVA: 0x000CBCC2 File Offset: 0x000C9EC2
		[DebuggerHidden]
		public <EnterLibraryNet>d__41(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x060023BE RID: 9150 RVA: 0x000CBCD1 File Offset: 0x000C9ED1
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x060023BF RID: 9151 RVA: 0x000CBCD4 File Offset: 0x000C9ED4
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			LibraryManager libraryManager = this;
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
			}
			if (!(GoalManager.instance == null))
			{
				libraryManager.LibraryEntered();
				return false;
			}
			this.<>2__current = true;
			this.<>1__state = 1;
			return true;
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x060023C0 RID: 9152 RVA: 0x000CBD31 File Offset: 0x000C9F31
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x060023C1 RID: 9153 RVA: 0x000CBD39 File Offset: 0x000C9F39
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x060023C2 RID: 9154 RVA: 0x000CBD40 File Offset: 0x000C9F40
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x040025BB RID: 9659
		private int <>1__state;

		// Token: 0x040025BC RID: 9660
		private object <>2__current;

		// Token: 0x040025BD RID: 9661
		public LibraryManager <>4__this;
	}

	// Token: 0x02000515 RID: 1301
	[CompilerGenerated]
	private sealed class <TutorialRoutine>d__47 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x060023C3 RID: 9155 RVA: 0x000CBD48 File Offset: 0x000C9F48
		[DebuggerHidden]
		public <TutorialRoutine>d__47(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x060023C4 RID: 9156 RVA: 0x000CBD57 File Offset: 0x000C9F57
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x060023C5 RID: 9157 RVA: 0x000CBD5C File Offset: 0x000C9F5C
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			LibraryManager libraryManager = this;
			switch (num)
			{
			case 0:
				this.<>1__state = -1;
				libraryManager.LoadingCamera.GetComponent<Animator>().enabled = false;
				this.<>2__current = true;
				this.<>1__state = 1;
				return true;
			case 1:
				this.<>1__state = -1;
				TutorialManager.instance.LibraryStart();
				PlayerControl.MyCamera.enabled = false;
				PlayerControl.myInstance.Audio.Listener.enabled = false;
				this.<>2__current = true;
				this.<>1__state = 2;
				return true;
			case 2:
				this.<>1__state = -1;
				AudioManager.PlayInterfaceSFX(libraryManager.FTUXStartSFX, 1f, 0f);
				t = 0f;
				camT = libraryManager.LoadingCamera.transform;
				startPos = camT.position;
				startRot = camT.rotation;
				break;
			case 3:
				this.<>1__state = -1;
				break;
			default:
				return false;
			}
			if (t >= 1f)
			{
				return false;
			}
			t += Time.deltaTime / libraryManager.CamMoveTime;
			Vector3 position = Vector3.Lerp(startPos, libraryManager.CameraTargetPos.position, t);
			Quaternion rotation = Quaternion.Lerp(startRot, libraryManager.CameraTargetPos.rotation, t);
			camT.SetPositionAndRotation(position, rotation);
			this.<>2__current = true;
			this.<>1__state = 3;
			return true;
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x060023C6 RID: 9158 RVA: 0x000CBEEB File Offset: 0x000CA0EB
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x060023C7 RID: 9159 RVA: 0x000CBEF3 File Offset: 0x000CA0F3
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x060023C8 RID: 9160 RVA: 0x000CBEFA File Offset: 0x000CA0FA
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x040025BE RID: 9662
		private int <>1__state;

		// Token: 0x040025BF RID: 9663
		private object <>2__current;

		// Token: 0x040025C0 RID: 9664
		public LibraryManager <>4__this;

		// Token: 0x040025C1 RID: 9665
		private float <t>5__2;

		// Token: 0x040025C2 RID: 9666
		private Transform <camT>5__3;

		// Token: 0x040025C3 RID: 9667
		private Vector3 <startPos>5__4;

		// Token: 0x040025C4 RID: 9668
		private Quaternion <startRot>5__5;
	}
}
