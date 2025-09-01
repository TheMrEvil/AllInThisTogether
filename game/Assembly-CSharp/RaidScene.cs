using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x0200012C RID: 300
public class RaidScene : MonoBehaviour
{
	// Token: 0x06000E04 RID: 3588 RVA: 0x000598E8 File Offset: 0x00057AE8
	private void Awake()
	{
		RaidScene.instance = this;
		if (!RaidManager.IsInRaid)
		{
			RaidManager.IsInRaid = true;
		}
		if (this.StartTrigger != null)
		{
			this.StartTrigger.EnterEvents.AddListener(new UnityAction(this.StartEncounter));
		}
		RaidManager.OnEncounterStarted = (Action)Delegate.Combine(RaidManager.OnEncounterStarted, new Action(this.OnEncounterStarted));
		RaidManager.OnEncounterReset = (Action)Delegate.Combine(RaidManager.OnEncounterReset, new Action(this.OnEncounterReset));
		RaidManager.OnEncounterEnded = (Action)Delegate.Combine(RaidManager.OnEncounterEnded, new Action(this.OnEncounterEnded));
		if (this.RaidCodex != null)
		{
			this.RaidCodex.SetActive(false);
		}
	}

	// Token: 0x06000E05 RID: 3589 RVA: 0x000599AC File Offset: 0x00057BAC
	private void Start()
	{
		if (this.StartTrigger != null)
		{
			this.StartTrigger.Deactivate();
		}
	}

	// Token: 0x06000E06 RID: 3590 RVA: 0x000599C8 File Offset: 0x00057BC8
	public static bool CanTriggerEvent(string id)
	{
		if (RaidScene.instance == null)
		{
			return false;
		}
		RaidScene.ArenaEvent @event = RaidScene.instance.GetEvent(id);
		return @event != null && (!@event.SingleUse || !RaidScene.instance.TriggeredEvents.Contains(id));
	}

	// Token: 0x06000E07 RID: 3591 RVA: 0x00059A12 File Offset: 0x00057C12
	public static void TriggerSceneEvent(string id)
	{
		if (!RaidScene.CanTriggerEvent(id))
		{
			return;
		}
		RaidScene.instance.GetEvent(id).Event.Invoke();
	}

	// Token: 0x06000E08 RID: 3592 RVA: 0x00059A34 File Offset: 0x00057C34
	public void TryForcePlayerInside()
	{
		if (this.InsideRaidTrigger == null)
		{
			return;
		}
		PlayerControl myInstance = PlayerControl.myInstance;
		if (myInstance == null)
		{
			return;
		}
		Vector3 position = myInstance.movement.GetPosition();
		Vector3 position2 = this.InsideRaidTrigger.transform.position;
		Vector3 vector = position2 + (position - position2).normalized * this.InsideRaidTrigger.radius * 0.75f;
		vector = AIManager.NearestNavPoint(vector, -1f);
		Debug.DrawLine(vector, vector + Vector3.up * 3f, Color.red, 2.5f);
		if (Vector3.Distance(position2, position) > this.InsideRaidTrigger.radius)
		{
			myInstance.movement.SetPositionImmediate(vector, myInstance.movement.GetForward(), true);
		}
		foreach (EntityControl entityControl in EntityControl.AllEntities)
		{
			AIControl aicontrol = entityControl as AIControl;
			if (aicontrol != null && !aicontrol.IsDead && aicontrol.PetOwnerID == myInstance.ViewID)
			{
				Vector3 point = GoalManager.FixPointOnNav(vector + UnityEngine.Random.insideUnitSphere * 6f);
				aicontrol.Movement.SetPositionImmediate(point, aicontrol.Movement.GetForward(), true);
			}
		}
		UnityMainThreadDispatcher.Instance().Invoke(new Action(this.PetSafetyCheck), 1.5f);
	}

	// Token: 0x06000E09 RID: 3593 RVA: 0x00059BC0 File Offset: 0x00057DC0
	private void PetSafetyCheck()
	{
		PlayerControl myInstance = PlayerControl.myInstance;
		Vector3 position = PlayerControl.myInstance.Movement.GetPosition();
		Vector3 position2 = this.InsideRaidTrigger.transform.position;
		foreach (EntityControl entityControl in EntityControl.AllEntities)
		{
			AIControl aicontrol = entityControl as AIControl;
			if (aicontrol != null && !aicontrol.IsDead && aicontrol.PetOwnerID == myInstance.ViewID && Vector3.Distance(position2, aicontrol.Movement.GetPosition()) >= this.InsideRaidTrigger.radius)
			{
				Vector3 point = GoalManager.FixPointOnNav(position + UnityEngine.Random.insideUnitSphere * 6f);
				aicontrol.Movement.SetPositionImmediate(point, aicontrol.Movement.GetForward(), true);
			}
		}
	}

	// Token: 0x06000E0A RID: 3594 RVA: 0x00059CB0 File Offset: 0x00057EB0
	public void UnstuckPlayer()
	{
		PlayerControl myInstance = PlayerControl.myInstance;
		if (myInstance == null || this.InsideRaidTrigger == null)
		{
			return;
		}
		Vector3 point = AIManager.NearestNavPoint(UnityEngine.Random.insideUnitSphere * this.InsideRaidTrigger.radius * 0.75f, -1f);
		myInstance.movement.SetPositionImmediate(point, myInstance.movement.GetForward(), true);
	}

	// Token: 0x06000E0B RID: 3595 RVA: 0x00059D20 File Offset: 0x00057F20
	private RaidScene.ArenaEvent GetEvent(string id)
	{
		foreach (RaidScene.ArenaEvent arenaEvent in this.EncounterEvents)
		{
			if (arenaEvent.ID == id)
			{
				return arenaEvent;
			}
		}
		return null;
	}

	// Token: 0x06000E0C RID: 3596 RVA: 0x00059D84 File Offset: 0x00057F84
	public void LoadEncounter(string id)
	{
		RaidManager.instance.LoadEncounter(id);
		RaidManager.instance.PreEncounter();
		this.TriggeredEvents.Clear();
	}

	// Token: 0x06000E0D RID: 3597 RVA: 0x00059DA6 File Offset: 0x00057FA6
	public void StartEncounter()
	{
		RaidManager.instance.StartEncounter();
	}

	// Token: 0x06000E0E RID: 3598 RVA: 0x00059DB2 File Offset: 0x00057FB2
	private void OnEncounterStarted()
	{
		this.EncounterStarted.Invoke();
	}

	// Token: 0x06000E0F RID: 3599 RVA: 0x00059DBF File Offset: 0x00057FBF
	private void OnEncounterReset()
	{
		this.StartTrigger.Reset();
		this.EncounterReset.Invoke();
		this.TriggeredEvents.Clear();
	}

	// Token: 0x06000E10 RID: 3600 RVA: 0x00059DE2 File Offset: 0x00057FE2
	private void OnEncounterEnded()
	{
		this.EncounterEnded.Invoke();
	}

	// Token: 0x06000E11 RID: 3601 RVA: 0x00059DF0 File Offset: 0x00057FF0
	private void OnDestroy()
	{
		RaidManager.OnEncounterStarted = (Action)Delegate.Remove(RaidManager.OnEncounterStarted, new Action(this.OnEncounterStarted));
		RaidManager.OnEncounterReset = (Action)Delegate.Remove(RaidManager.OnEncounterReset, new Action(this.OnEncounterReset));
		RaidManager.OnEncounterReset = (Action)Delegate.Remove(RaidManager.OnEncounterReset, new Action(this.OnEncounterEnded));
	}

	// Token: 0x06000E12 RID: 3602 RVA: 0x00059E5D File Offset: 0x0005805D
	public RaidScene()
	{
	}

	// Token: 0x04000B7A RID: 2938
	public static RaidScene instance;

	// Token: 0x04000B7B RID: 2939
	public bool IsFirstEncounter;

	// Token: 0x04000B7C RID: 2940
	public bool AutoStartEncounter;

	// Token: 0x04000B7D RID: 2941
	public bool DebugHardMode;

	// Token: 0x04000B7E RID: 2942
	public string DebugEncounterID;

	// Token: 0x04000B7F RID: 2943
	public GameObject RaidCodex;

	// Token: 0x04000B80 RID: 2944
	public PlayerTrigger StartTrigger;

	// Token: 0x04000B81 RID: 2945
	public SphereCollider InsideRaidTrigger;

	// Token: 0x04000B82 RID: 2946
	public Transform BossSpawn;

	// Token: 0x04000B83 RID: 2947
	public Transform BossCameraPoint;

	// Token: 0x04000B84 RID: 2948
	public Transform BossNamePoint;

	// Token: 0x04000B85 RID: 2949
	public Transform EndOverridePoint;

	// Token: 0x04000B86 RID: 2950
	public UnityEvent OnAllFirstReward;

	// Token: 0x04000B87 RID: 2951
	public UnityEvent EncounterStartSequence;

	// Token: 0x04000B88 RID: 2952
	public UnityEvent EncounterStarted;

	// Token: 0x04000B89 RID: 2953
	public UnityEvent EncounterReset;

	// Token: 0x04000B8A RID: 2954
	public UnityEvent EncounterEnded;

	// Token: 0x04000B8B RID: 2955
	public List<RaidScene.ArenaEvent> EncounterEvents = new List<RaidScene.ArenaEvent>();

	// Token: 0x04000B8C RID: 2956
	private HashSet<string> TriggeredEvents = new HashSet<string>();

	// Token: 0x02000536 RID: 1334
	[Serializable]
	public class ArenaEvent
	{
		// Token: 0x06002418 RID: 9240 RVA: 0x000CCF94 File Offset: 0x000CB194
		public ArenaEvent()
		{
		}

		// Token: 0x0400264D RID: 9805
		public string ID;

		// Token: 0x0400264E RID: 9806
		public bool SingleUse = true;

		// Token: 0x0400264F RID: 9807
		public UnityEvent Event;
	}
}
