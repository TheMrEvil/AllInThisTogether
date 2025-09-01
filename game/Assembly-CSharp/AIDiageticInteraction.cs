using System;
using System.Collections.Generic;
using MiniTools.BetterGizmos;
using UnityEngine;

// Token: 0x02000062 RID: 98
public class AIDiageticInteraction : DiageticOption, EffectBase
{
	// Token: 0x1700003F RID: 63
	// (get) Token: 0x06000333 RID: 819 RVA: 0x0001AAFD File Offset: 0x00018CFD
	private bool HasCooldown
	{
		get
		{
			return this.Interactivity == AIDiageticInteraction.RepeatType.OwnerCooldown || this.Interactivity == AIDiageticInteraction.RepeatType.PerPlayerCooldown || this.Interactivity == AIDiageticInteraction.RepeatType.GlobalCooldown;
		}
	}

	// Token: 0x06000334 RID: 820 RVA: 0x0001AB1C File Offset: 0x00018D1C
	protected override void Awake()
	{
		if (this.ai == null)
		{
			this.ai = base.GetComponentInParent<AIControl>();
		}
		base.Awake();
	}

	// Token: 0x06000335 RID: 821 RVA: 0x0001AB40 File Offset: 0x00018D40
	public void SetupInfo(EffectProperties props)
	{
		if (!(props.SourceControl == null))
		{
			AIControl aicontrol = props.SourceControl as AIControl;
			if (aicontrol != null)
			{
				this.ai = aicontrol;
				return;
			}
		}
	}

	// Token: 0x06000336 RID: 822 RVA: 0x0001AB72 File Offset: 0x00018D72
	private void Start()
	{
		if (this.ai == null)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x06000337 RID: 823 RVA: 0x0001AB90 File Offset: 0x00018D90
	private void Update()
	{
		if (PlayerControl.myInstance == null)
		{
			return;
		}
		if (this.IsAvailable && !this.CanActivate(PlayerControl.myInstance.ViewID))
		{
			this.Deactivate();
		}
		else if (!this.IsAvailable && this.CanActivate(PlayerControl.myInstance.ViewID))
		{
			this.Activate();
		}
		if (this.HasCooldown)
		{
			if (this.globalCooldown > 0f)
			{
				this.globalCooldown -= Time.deltaTime;
			}
			foreach (int num in this.PlayerCooldowns.GetKeys<int, float>())
			{
				Dictionary<int, float> playerCooldowns = this.PlayerCooldowns;
				int key = num;
				playerCooldowns[key] -= Time.deltaTime;
				if (this.PlayerCooldowns[num] <= 0f)
				{
					this.PlayerCooldowns.Remove(num);
				}
			}
		}
		if (this.icd > 0f)
		{
			this.icd -= Time.deltaTime;
		}
	}

	// Token: 0x06000338 RID: 824 RVA: 0x0001ACB8 File Offset: 0x00018EB8
	public override void Select()
	{
		if (!this.CanActivate(PlayerControl.myInstance.ViewID))
		{
			return;
		}
		this.ai.TryTriggerEffect(this);
		this.Deactivate();
	}

	// Token: 0x06000339 RID: 825 RVA: 0x0001ACE0 File Offset: 0x00018EE0
	public void ActivateFromNetwork(int playerID)
	{
		if (this.icd > 0f)
		{
			return;
		}
		this.RunAction(playerID);
		this.icd = this.InternalCooldown;
		if (this.HasCooldown)
		{
			if (this.Interactivity == AIDiageticInteraction.RepeatType.GlobalCooldown)
			{
				this.globalCooldown = this.Cooldown;
			}
			this.PlayerCooldowns[playerID] = this.Cooldown;
		}
		switch (this.Interactivity)
		{
		case AIDiageticInteraction.RepeatType.OwnerOnce:
		case AIDiageticInteraction.RepeatType.GlobalOnce:
			this.hasUsedGlobal = true;
			return;
		case AIDiageticInteraction.RepeatType.OwnerCooldown:
		case AIDiageticInteraction.RepeatType.GlobalCooldown:
			this.globalCooldown = this.Cooldown;
			return;
		case AIDiageticInteraction.RepeatType.PerPlayerOnce:
		case AIDiageticInteraction.RepeatType.PerPlayerCooldown:
			this.PlayerCooldowns[playerID] = this.Cooldown;
			return;
		case AIDiageticInteraction.RepeatType.PlayerTagged:
			break;
		case AIDiageticInteraction.RepeatType.PlayerStatus:
		{
			PlayerControl playerFromViewID = PlayerControl.GetPlayerFromViewID(playerID);
			if (playerFromViewID == PlayerControl.myInstance)
			{
				playerFromViewID.Net.RemoveStatusNetwork(this.RequiredStatus.HashCode, -1, 0, false, true);
			}
			break;
		}
		default:
			return;
		}
	}

	// Token: 0x0600033A RID: 826 RVA: 0x0001ADC4 File Offset: 0x00018FC4
	private void RunAction(int playerID)
	{
		AIDiageticInteraction.InteractType act = this.Act;
		if (act != AIDiageticInteraction.InteractType.Action)
		{
			if (act != AIDiageticInteraction.InteractType.Tag)
			{
				return;
			}
			this.ai.AddTag(this.Tag);
		}
		else
		{
			EffectProperties effectProperties = new EffectProperties(this.ai);
			PlayerControl playerFromViewID = PlayerControl.GetPlayerFromViewID(playerID);
			if (playerFromViewID != null)
			{
				effectProperties.Affected = playerFromViewID.gameObject;
			}
			if (this.ActionTrigger != null && this.ai.view.IsMine)
			{
				Debug.Log("Running AI Action");
				this.ai.net.ExecuteActionTree(this.ActionTrigger.RootNode.guid, effectProperties);
				return;
			}
		}
	}

	// Token: 0x0600033B RID: 827 RVA: 0x0001AE68 File Offset: 0x00019068
	private bool CanActivate(int playerID)
	{
		if (this.ai.IsDead)
		{
			return false;
		}
		PlayerControl playerFromViewID = PlayerControl.GetPlayerFromViewID(playerID);
		if (this.DisallowedStatus != null && playerFromViewID.HasStatusEffectGUID(this.DisallowedStatus.ID))
		{
			return false;
		}
		switch (this.Interactivity)
		{
		case AIDiageticInteraction.RepeatType.OwnerOnce:
			return !this.hasUsedGlobal && this.ai.PetOwnerID == playerID;
		case AIDiageticInteraction.RepeatType.OwnerCooldown:
			return this.globalCooldown <= 0f && this.ai.PetOwnerID == playerID;
		case AIDiageticInteraction.RepeatType.GlobalOnce:
			return !this.hasUsedGlobal;
		case AIDiageticInteraction.RepeatType.GlobalCooldown:
			return this.globalCooldown <= 0f;
		case AIDiageticInteraction.RepeatType.PerPlayerOnce:
			return !this.PlayerCooldowns.ContainsKey(playerID);
		case AIDiageticInteraction.RepeatType.PerPlayerCooldown:
			return !this.PlayerCooldowns.ContainsKey(playerID) || this.PlayerCooldowns[playerID] <= 0f;
		case AIDiageticInteraction.RepeatType.PlayerStatus:
			return playerFromViewID.HasStatusEffectGUID(this.RequiredStatus.ID);
		}
		return false;
	}

	// Token: 0x0600033C RID: 828 RVA: 0x0001AF92 File Offset: 0x00019192
	private void OnDrawGizmos()
	{
		BetterGizmos.DrawSphere(new Color(0.8f, 0.7f, 0.5f, 0.2f), base.transform.position, this.InteractDistance);
	}

	// Token: 0x0600033D RID: 829 RVA: 0x0001AFC3 File Offset: 0x000191C3
	public AIDiageticInteraction()
	{
	}

	// Token: 0x0400031F RID: 799
	private AIControl ai;

	// Token: 0x04000320 RID: 800
	public string Label;

	// Token: 0x04000321 RID: 801
	public AIDiageticInteraction.RepeatType Interactivity;

	// Token: 0x04000322 RID: 802
	public StatusTree RequiredStatus;

	// Token: 0x04000323 RID: 803
	public StatusTree DisallowedStatus;

	// Token: 0x04000324 RID: 804
	public bool OwnerOnly = true;

	// Token: 0x04000325 RID: 805
	public float Cooldown = 4f;

	// Token: 0x04000326 RID: 806
	public float InternalCooldown;

	// Token: 0x04000327 RID: 807
	private float icd;

	// Token: 0x04000328 RID: 808
	public AIDiageticInteraction.InteractType Act;

	// Token: 0x04000329 RID: 809
	public ActionTree ActionTrigger;

	// Token: 0x0400032A RID: 810
	public string Tag;

	// Token: 0x0400032B RID: 811
	private bool hasUsedGlobal;

	// Token: 0x0400032C RID: 812
	private float globalCooldown;

	// Token: 0x0400032D RID: 813
	private Dictionary<int, float> PlayerCooldowns = new Dictionary<int, float>();

	// Token: 0x0200047F RID: 1151
	public enum InteractType
	{
		// Token: 0x040022DD RID: 8925
		Action,
		// Token: 0x040022DE RID: 8926
		Tag
	}

	// Token: 0x02000480 RID: 1152
	public enum RepeatType
	{
		// Token: 0x040022E0 RID: 8928
		OwnerOnce,
		// Token: 0x040022E1 RID: 8929
		OwnerCooldown,
		// Token: 0x040022E2 RID: 8930
		GlobalOnce,
		// Token: 0x040022E3 RID: 8931
		GlobalCooldown,
		// Token: 0x040022E4 RID: 8932
		PerPlayerOnce,
		// Token: 0x040022E5 RID: 8933
		PerPlayerCooldown,
		// Token: 0x040022E6 RID: 8934
		PlayerTagged,
		// Token: 0x040022E7 RID: 8935
		PlayerStatus
	}
}
