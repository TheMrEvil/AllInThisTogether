using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020000E5 RID: 229
public class VignetteControl : MonoBehaviour
{
	// Token: 0x170000DC RID: 220
	// (get) Token: 0x06000A29 RID: 2601 RVA: 0x000426CA File Offset: 0x000408CA
	private bool IsInVignette
	{
		get
		{
			if (RaidManager.IsInRaid && RaidManager.PreparedEncounter != null)
			{
				return RaidManager.PreparedEncounter.Type == RaidDB.EncounterType.Vignette;
			}
			return GameplayManager.CurState == GameState.Vignette_Inside;
		}
	}

	// Token: 0x06000A2A RID: 2602 RVA: 0x000426F1 File Offset: 0x000408F1
	private void Awake()
	{
		VignetteControl.instance = this;
	}

	// Token: 0x06000A2B RID: 2603 RVA: 0x000426FC File Offset: 0x000408FC
	private void Update()
	{
		this.autoExitTimer -= Time.deltaTime;
		if (!this.ReadyToLeave && this.autoExitTimer < 0f)
		{
			this.ActivateExit();
		}
		if (this.ReadyToLeave && this.IsInVignette)
		{
			this.UpdateExitZones();
		}
	}

	// Token: 0x06000A2C RID: 2604 RVA: 0x0004274C File Offset: 0x0004094C
	public void StartVignette()
	{
		base.StartCoroutine("BeginVignetteSequence");
	}

	// Token: 0x06000A2D RID: 2605 RVA: 0x0004275A File Offset: 0x0004095A
	private IEnumerator BeginVignetteSequence()
	{
		while (GoalManager.instance == null || StateManager.instance == null)
		{
			yield return true;
		}
		VignetteInfoDisplay vignetteInfoDisplay = VignetteInfoDisplay.instance;
		if (vignetteInfoDisplay != null)
		{
			vignetteInfoDisplay.Setup(this.Title, this.Detail);
		}
		if (!string.IsNullOrEmpty(this.StartingAction))
		{
			StateManager.VignetteAction(this.StartingAction);
		}
		if (this.CanLeaveFromStart)
		{
			yield return new WaitForSeconds(5f + this.ExtraLeaveDelay);
			this.ActivateExit();
		}
		yield break;
	}

	// Token: 0x06000A2E RID: 2606 RVA: 0x0004276C File Offset: 0x0004096C
	public void OnActionTaken(string actionID, int sourceID, int seed)
	{
		VignetteControl.VignetteAction action = this.GetAction(actionID);
		if (action == null)
		{
			return;
		}
		if (!this.CanActivate(action, sourceID))
		{
			return;
		}
		this.AddActivation(actionID, sourceID);
		EntityControl entity = EntityControl.GetEntity(sourceID);
		if (action.Action != null)
		{
			this.TryTriggerAction(action, entity, seed);
		}
		if (action.NewInstruction)
		{
			this.TryUpdateText(action, sourceID);
		}
		UnityEvent onAction = action.OnAction;
		if (onAction != null)
		{
			onAction.Invoke();
		}
		if (entity == PlayerControl.myInstance)
		{
			UnityEvent onActionSrc = action.OnActionSrc;
			if (onActionSrc == null)
			{
				return;
			}
			onActionSrc.Invoke();
		}
	}

	// Token: 0x06000A2F RID: 2607 RVA: 0x000427F4 File Offset: 0x000409F4
	private void TryTriggerAction(VignetteControl.VignetteAction vAct, EntityControl src, int seed)
	{
		if (vAct.ActionForSourceOnly && src != PlayerControl.myInstance)
		{
			return;
		}
		EffectProperties effectProperties;
		if (src != null)
		{
			effectProperties = new EffectProperties(src);
		}
		else
		{
			effectProperties = new EffectProperties();
			effectProperties.IsWorld = true;
		}
		effectProperties.SourceType = ActionSource.Vignette;
		effectProperties.OverrideSeed(seed, 0);
		EffectProperties effectProperties2 = effectProperties;
		PlayerControl myInstance = PlayerControl.myInstance;
		effectProperties2.AllyTarget = ((myInstance != null) ? myInstance.gameObject : null);
		effectProperties.SetExtra(EProp.DynamicInput, (float)vAct.InputNum);
		if (vAct.StartLocation != null)
		{
			effectProperties.StartLoc = new global::Pose(vAct.StartLocation);
			effectProperties.OutLoc = new global::Pose(vAct.StartLocation);
		}
		vAct.Action.Root.Apply(effectProperties);
	}

	// Token: 0x06000A30 RID: 2608 RVA: 0x000428AC File Offset: 0x00040AAC
	private void TryUpdateText(VignetteControl.VignetteAction vAct, int sourceID)
	{
		VignetteControl.RepeatType repeat = vAct.Repeat;
		if (repeat <= VignetteControl.RepeatType.Once)
		{
			VignetteInfoDisplay.UpdateDetail(vAct.InstructionText);
			return;
		}
		if (repeat - VignetteControl.RepeatType.OncePerPlayer > 2)
		{
			return;
		}
		if (!this.CanActivate(vAct, sourceID))
		{
			VignetteInfoDisplay.UpdateDetail(vAct.InstructionText);
		}
	}

	// Token: 0x06000A31 RID: 2609 RVA: 0x000428EC File Offset: 0x00040AEC
	private void AddActivation(string ID, int source)
	{
		if (!this.ActionActivations.ContainsKey(ID))
		{
			this.ActionActivations.Add(ID, new Dictionary<int, int>());
		}
		if (!this.ActionActivations[ID].ContainsKey(source))
		{
			this.ActionActivations[ID].Add(source, 0);
		}
		Dictionary<int, int> dictionary = this.ActionActivations[ID];
		int num = dictionary[source];
		dictionary[source] = num + 1;
	}

	// Token: 0x06000A32 RID: 2610 RVA: 0x00042960 File Offset: 0x00040B60
	public bool CanActivate(string ID, int sourceID)
	{
		VignetteControl.VignetteAction action = this.GetAction(ID);
		return action != null && this.CanActivate(action, sourceID);
	}

	// Token: 0x06000A33 RID: 2611 RVA: 0x00042984 File Offset: 0x00040B84
	private bool CanActivate(VignetteControl.VignetteAction act, int sourceID)
	{
		switch (act.Repeat)
		{
		case VignetteControl.RepeatType.Many:
			return true;
		case VignetteControl.RepeatType.Once:
			return !this.ActionActivations.ContainsKey(act.ID);
		case VignetteControl.RepeatType.OncePerPlayer:
			return !this.ActionActivations.ContainsKey(act.ID) || !this.ActionActivations[act.ID].ContainsKey(sourceID);
		case VignetteControl.RepeatType.Count:
		{
			if (!this.ActionActivations.ContainsKey(act.ID))
			{
				return true;
			}
			int num = 0;
			foreach (KeyValuePair<int, int> keyValuePair in this.ActionActivations[act.ID])
			{
				num += keyValuePair.Value;
			}
			return num < act.Count;
		}
		case VignetteControl.RepeatType.CountPerPlayer:
			return !this.ActionActivations.ContainsKey(act.ID) || !this.ActionActivations[act.ID].ContainsKey(sourceID) || this.ActionActivations[act.ID][sourceID] < act.Count;
		default:
			return true;
		}
	}

	// Token: 0x06000A34 RID: 2612 RVA: 0x00042AC4 File Offset: 0x00040CC4
	private VignetteControl.VignetteAction GetAction(string ID)
	{
		foreach (VignetteControl.VignetteAction vignetteAction in this.Actions)
		{
			if (vignetteAction.ID == ID)
			{
				return vignetteAction;
			}
		}
		return null;
	}

	// Token: 0x06000A35 RID: 2613 RVA: 0x00042B28 File Offset: 0x00040D28
	public bool HasDoneAction(string ID)
	{
		return this.ActionActivations.ContainsKey(ID);
	}

	// Token: 0x06000A36 RID: 2614 RVA: 0x00042B38 File Offset: 0x00040D38
	public void ActivateExit()
	{
		if (this.ReadyToLeave)
		{
			return;
		}
		UnityEngine.Debug.Log("Activating Vignette Exit");
		this.ReadyToLeave = true;
		Room currentRoom = PhotonNetwork.CurrentRoom;
		int num = (currentRoom != null) ? currentRoom.PlayerCount : 1;
		for (int i = 0; i < num; i++)
		{
			this.CreateZone(this.LeaveZoneLocations[i].position);
		}
		Fountain.instance.ExitIndicator.SetActive(true);
		AudioManager.PlayClipAtPoint(Fountain.instance.ExitReadySFX, Fountain.instance.transform.position, 1f, 1f, 1f, 10f, 250f);
		StateManager.VignetteAction("EXIT_READY");
	}

	// Token: 0x06000A37 RID: 2615 RVA: 0x00042BE6 File Offset: 0x00040DE6
	private void UpdateExitZones()
	{
		if (this.AllZonesReady() && (PlayerControl.PlayerCount == 1 || PhotonNetwork.IsMasterClient))
		{
			if (RaidManager.IsInRaid)
			{
				RaidManager.instance.VignetteComplete();
				return;
			}
			GameplayManager.instance.UpdateGameState(GameState.Vignette_Completed);
		}
	}

	// Token: 0x06000A38 RID: 2616 RVA: 0x00042C20 File Offset: 0x00040E20
	public static void ClearZones()
	{
		if (VignetteControl.instance == null)
		{
			return;
		}
		foreach (LibraryMPStartZone libraryMPStartZone in VignetteControl.instance.CurZones)
		{
			libraryMPStartZone.Deactivate();
		}
		VignetteControl.instance.CurZones.Clear();
		AudioManager.PlaySFX2D(AudioManager.instance.VignetteCompletedSFX, 1f, 0.1f);
	}

	// Token: 0x06000A39 RID: 2617 RVA: 0x00042CAC File Offset: 0x00040EAC
	private void CreateZone(Vector3 pt)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(GoalManager.instance.ExitZonePrefab);
		gameObject.transform.position = pt;
		gameObject.SetActive(true);
		LibraryMPStartZone component = gameObject.GetComponent<LibraryMPStartZone>();
		this.CurZones.Add(component);
	}

	// Token: 0x06000A3A RID: 2618 RVA: 0x00042CF0 File Offset: 0x00040EF0
	public bool AllZonesReady()
	{
		if (this.CurZones.Count <= 0)
		{
			return false;
		}
		int num = 0;
		foreach (LibraryMPStartZone libraryMPStartZone in this.CurZones)
		{
			if (libraryMPStartZone.IsActive && libraryMPStartZone.IsPlayerInside)
			{
				num++;
			}
		}
		return num == this.CurZones.Count || num >= PlayerControl.PlayerCount;
	}

	// Token: 0x06000A3B RID: 2619 RVA: 0x00042D7C File Offset: 0x00040F7C
	public void EndCleanup()
	{
		foreach (StatusTree statusTree in this.ToRemoveAtEnd)
		{
			PlayerControl.myInstance.RemoveStatusEffect(statusTree.HashCode, -1, 0, false, true);
		}
	}

	// Token: 0x06000A3C RID: 2620 RVA: 0x00042DDC File Offset: 0x00040FDC
	public void DebugStart()
	{
		this.debugReadyToStart = true;
	}

	// Token: 0x06000A3D RID: 2621 RVA: 0x00042DE5 File Offset: 0x00040FE5
	private void OnDrawGizmos()
	{
	}

	// Token: 0x06000A3E RID: 2622 RVA: 0x00042DE7 File Offset: 0x00040FE7
	public VignetteControl()
	{
	}

	// Token: 0x040008A9 RID: 2217
	public static VignetteControl instance;

	// Token: 0x040008AA RID: 2218
	public string Title;

	// Token: 0x040008AB RID: 2219
	public string Detail;

	// Token: 0x040008AC RID: 2220
	public bool CanLeaveFromStart;

	// Token: 0x040008AD RID: 2221
	public float ExtraLeaveDelay;

	// Token: 0x040008AE RID: 2222
	public List<Transform> LeaveZoneLocations;

	// Token: 0x040008AF RID: 2223
	private List<LibraryMPStartZone> CurZones = new List<LibraryMPStartZone>();

	// Token: 0x040008B0 RID: 2224
	private bool ReadyToLeave;

	// Token: 0x040008B1 RID: 2225
	public string StartingAction;

	// Token: 0x040008B2 RID: 2226
	public List<VignetteControl.VignetteAction> Actions = new List<VignetteControl.VignetteAction>();

	// Token: 0x040008B3 RID: 2227
	public List<StatusTree> ToRemoveAtEnd = new List<StatusTree>();

	// Token: 0x040008B4 RID: 2228
	private Dictionary<string, Dictionary<int, int>> ActionActivations = new Dictionary<string, Dictionary<int, int>>();

	// Token: 0x040008B5 RID: 2229
	private float autoExitTimer = 60f;

	// Token: 0x040008B6 RID: 2230
	private const string EXIT_READY_ACTION = "EXIT_READY";

	// Token: 0x040008B7 RID: 2231
	private const string SAFETY_OPEN_EXIT = "SAFETY_EXIT";

	// Token: 0x040008B8 RID: 2232
	public bool DebugEditorWait;

	// Token: 0x040008B9 RID: 2233
	private bool debugReadyToStart;

	// Token: 0x020004D8 RID: 1240
	[Serializable]
	public class VignetteAction
	{
		// Token: 0x0600230A RID: 8970 RVA: 0x000C84C1 File Offset: 0x000C66C1
		public VignetteAction()
		{
		}

		// Token: 0x04002486 RID: 9350
		public string ID;

		// Token: 0x04002487 RID: 9351
		public VignetteControl.RepeatType Repeat;

		// Token: 0x04002488 RID: 9352
		public int Count;

		// Token: 0x04002489 RID: 9353
		public ActionTree Action;

		// Token: 0x0400248A RID: 9354
		public Transform StartLocation;

		// Token: 0x0400248B RID: 9355
		public bool ActionForSourceOnly;

		// Token: 0x0400248C RID: 9356
		public int InputNum;

		// Token: 0x0400248D RID: 9357
		public bool NewInstruction;

		// Token: 0x0400248E RID: 9358
		public string InstructionText;

		// Token: 0x0400248F RID: 9359
		public UnityEvent OnAction;

		// Token: 0x04002490 RID: 9360
		public UnityEvent OnActionSrc;
	}

	// Token: 0x020004D9 RID: 1241
	public enum RepeatType
	{
		// Token: 0x04002492 RID: 9362
		Many,
		// Token: 0x04002493 RID: 9363
		Once,
		// Token: 0x04002494 RID: 9364
		OncePerPlayer,
		// Token: 0x04002495 RID: 9365
		Count,
		// Token: 0x04002496 RID: 9366
		CountPerPlayer
	}

	// Token: 0x020004DA RID: 1242
	[CompilerGenerated]
	private sealed class <BeginVignetteSequence>d__20 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x0600230B RID: 8971 RVA: 0x000C84C9 File Offset: 0x000C66C9
		[DebuggerHidden]
		public <BeginVignetteSequence>d__20(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x0600230C RID: 8972 RVA: 0x000C84D8 File Offset: 0x000C66D8
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x0600230D RID: 8973 RVA: 0x000C84DC File Offset: 0x000C66DC
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			VignetteControl vignetteControl = this;
			switch (num)
			{
			case 0:
				this.<>1__state = -1;
				break;
			case 1:
				this.<>1__state = -1;
				break;
			case 2:
				this.<>1__state = -1;
				vignetteControl.ActivateExit();
				return false;
			default:
				return false;
			}
			if (GoalManager.instance == null || StateManager.instance == null)
			{
				this.<>2__current = true;
				this.<>1__state = 1;
				return true;
			}
			VignetteInfoDisplay instance = VignetteInfoDisplay.instance;
			if (instance != null)
			{
				instance.Setup(vignetteControl.Title, vignetteControl.Detail);
			}
			if (!string.IsNullOrEmpty(vignetteControl.StartingAction))
			{
				StateManager.VignetteAction(vignetteControl.StartingAction);
			}
			if (vignetteControl.CanLeaveFromStart)
			{
				this.<>2__current = new WaitForSeconds(5f + vignetteControl.ExtraLeaveDelay);
				this.<>1__state = 2;
				return true;
			}
			return false;
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x0600230E RID: 8974 RVA: 0x000C85B4 File Offset: 0x000C67B4
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x0600230F RID: 8975 RVA: 0x000C85BC File Offset: 0x000C67BC
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06002310 RID: 8976 RVA: 0x000C85C3 File Offset: 0x000C67C3
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x04002497 RID: 9367
		private int <>1__state;

		// Token: 0x04002498 RID: 9368
		private object <>2__current;

		// Token: 0x04002499 RID: 9369
		public VignetteControl <>4__this;
	}
}
