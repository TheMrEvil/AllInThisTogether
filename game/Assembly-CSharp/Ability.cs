using System;
using UnityEngine;

// Token: 0x02000079 RID: 121
[Serializable]
public class Ability
{
	// Token: 0x1700005F RID: 95
	// (get) Token: 0x060004EF RID: 1263 RVA: 0x00024B0D File Offset: 0x00022D0D
	public string GUID
	{
		get
		{
			AbilityTree abilityTree = this.AbilityTree;
			if (abilityTree == null)
			{
				return null;
			}
			return abilityTree.RootNode.guid;
		}
	}

	// Token: 0x17000060 RID: 96
	// (get) Token: 0x060004F0 RID: 1264 RVA: 0x00024B25 File Offset: 0x00022D25
	public AbilityRootNode rootNode
	{
		get
		{
			AbilityTree abilityTree = this.AbilityTree;
			return ((abilityTree != null) ? abilityTree.RootNode : null) as AbilityRootNode;
		}
	}

	// Token: 0x17000061 RID: 97
	// (get) Token: 0x060004F1 RID: 1265 RVA: 0x00024B3E File Offset: 0x00022D3E
	public global::Pose location
	{
		get
		{
			return this.rootNode.AtPoint(this.properties);
		}
	}

	// Token: 0x17000062 RID: 98
	// (get) Token: 0x060004F2 RID: 1266 RVA: 0x00024B51 File Offset: 0x00022D51
	public AbilityRootNode.UsageProps props
	{
		get
		{
			if (this.AbilityTree == null)
			{
				return null;
			}
			AbilityRootNode abilityRootNode = this.AbilityTree.RootNode as AbilityRootNode;
			if (abilityRootNode == null)
			{
				return null;
			}
			return abilityRootNode.Usage;
		}
	}

	// Token: 0x17000063 RID: 99
	// (get) Token: 0x060004F3 RID: 1267 RVA: 0x00024B7E File Offset: 0x00022D7E
	public PlayerAbilityType PlayerAbilityType
	{
		get
		{
			AbilityRootNode rootNode = this.rootNode;
			if (rootNode == null)
			{
				return PlayerAbilityType.None;
			}
			return rootNode.PlrAbilityType;
		}
	}

	// Token: 0x17000064 RID: 100
	// (get) Token: 0x060004F4 RID: 1268 RVA: 0x00024B91 File Offset: 0x00022D91
	public bool IsOnCooldown
	{
		get
		{
			return this.props.Cooldown > 0f && this.currentCD > 0f && this.ChargesAvailable < 1;
		}
	}

	// Token: 0x17000065 RID: 101
	// (get) Token: 0x060004F5 RID: 1269 RVA: 0x00024BBD File Offset: 0x00022DBD
	public float ManaCost
	{
		get
		{
			AbilityRootNode.UsageProps props = this.props;
			if (props == null)
			{
				return 0f;
			}
			return props.AbilityMetadata.ManaCost;
		}
	}

	// Token: 0x17000066 RID: 102
	// (get) Token: 0x060004F6 RID: 1270 RVA: 0x00024BD9 File Offset: 0x00022DD9
	public int ChargeCount
	{
		get
		{
			AbilityRootNode.UsageProps props = this.props;
			if (props == null)
			{
				return 1;
			}
			return props.Charges;
		}
	}

	// Token: 0x17000067 RID: 103
	// (get) Token: 0x060004F7 RID: 1271 RVA: 0x00024BEC File Offset: 0x00022DEC
	public bool isReleasing
	{
		get
		{
			return this.current != null && this.current.isReleasing && this.current.CurrentState != AbilityState.Finished;
		}
	}

	// Token: 0x060004F8 RID: 1272 RVA: 0x00024C1C File Offset: 0x00022E1C
	public Ability(AbilityTree tree)
	{
		this.AbilityTree = tree;
		this.ResetCooldownToDefault();
	}

	// Token: 0x060004F9 RID: 1273 RVA: 0x00024C44 File Offset: 0x00022E44
	public void ResetCooldown()
	{
		if (this.ChargeCount > 1 && this.ChargesAvailable < this.ChargeCount)
		{
			this.ChargesAvailable++;
			if (this.ChargesAvailable >= this.ChargeCount)
			{
				this.ChargeUseCD = 0f;
				this.currentCD = 0f;
			}
			return;
		}
		this.currentCD = 0f;
		this.ChargeUseCD = 0f;
	}

	// Token: 0x060004FA RID: 1274 RVA: 0x00024CB4 File Offset: 0x00022EB4
	public void ResetCooldownToDefault()
	{
		if (this.rootNode.Usage.StartOnCD)
		{
			float num = this.rootNode.Usage.Cooldown * this.rootNode.Usage.StartMult;
			if (this.rootNode.Usage.RandomStartCD > 0f)
			{
				num -= UnityEngine.Random.Range(0f, num) * this.rootNode.Usage.RandomStartCD;
			}
			this.currentCD = num;
			this.ChargesAvailable = 0;
			return;
		}
		this.currentCD = 0f;
		this.ChargesAvailable = this.props.Charges;
	}

	// Token: 0x060004FB RID: 1275 RVA: 0x00024D58 File Offset: 0x00022F58
	public void TickUpdate(EntityControl control)
	{
		float deltaTime = GameplayManager.deltaTime;
		this.ModifyCooldown(control, -deltaTime);
		if (this.ChargeUseCD > 0f)
		{
			this.ChargeUseCD -= deltaTime;
		}
	}

	// Token: 0x060004FC RID: 1276 RVA: 0x00024D8F File Offset: 0x00022F8F
	public void ModifyCooldown(EntityControl control, float delta)
	{
		this.SetCooldown(control, this.currentCD + delta);
	}

	// Token: 0x060004FD RID: 1277 RVA: 0x00024DA0 File Offset: 0x00022FA0
	public void SetCooldown(EntityControl control, float val)
	{
		this.currentCD = Mathf.Max(val, 0f);
		if (this.currentCD <= 0f && this.ChargeCount > 1 && this.ChargesAvailable < this.ChargeCount)
		{
			this.ChargesAvailable++;
			if (this.ChargesAvailable < this.ChargeCount)
			{
				this.SetFullCooldown(control, this.properties);
			}
		}
	}

	// Token: 0x060004FE RID: 1278 RVA: 0x00024E0C File Offset: 0x0002300C
	public bool IsActive(bool ignorePassive = false)
	{
		return !(this.current == null) && this.current.AbilityType != AbilityType.Instant && (!ignorePassive || this.current.AbilityType != AbilityType.Channel_Passive) && (!this.current.isReleasing || this.current.CurrentState != AbilityState.Finished);
	}

	// Token: 0x060004FF RID: 1279 RVA: 0x00024E69 File Offset: 0x00023069
	public bool IsRunning()
	{
		return this.current != null;
	}

	// Token: 0x06000500 RID: 1280 RVA: 0x00024E78 File Offset: 0x00023078
	public virtual CastFailedReason CanUse(EntityControl control)
	{
		if (this.AbilityTree == null)
		{
			return CastFailedReason.Invalid;
		}
		if (this.AutoFiring && !this.props.CanAutoInstant)
		{
			return CastFailedReason.SilentFail;
		}
		if (this.props.RequiresTarget && control.currentTarget == null)
		{
			return CastFailedReason.NoTarget;
		}
		if (this.props.Cooldown > 0f && this.currentCD > 0f && (this.props.Charges <= 1 || this.ChargesAvailable <= 0))
		{
			return CastFailedReason.Cooldown;
		}
		if (this.ChargeUseCD > 0f)
		{
			return CastFailedReason.Cooldown;
		}
		if (this.current != null && this.current.AbilityType != AbilityType.Instant && this.current.CurrentState != AbilityState.Finished)
		{
			return CastFailedReason.SilentFail;
		}
		return CastFailedReason.None;
	}

	// Token: 0x06000501 RID: 1281 RVA: 0x00024F3C File Offset: 0x0002313C
	public void TriggerSnippets(EventTrigger trigger, EntityControl control, EffectProperties eProps)
	{
		if (this.SnippetCD > 0f)
		{
			return;
		}
		AbilityRootNode.UsageProps props = this.props;
		this.SnippetCD = ((props != null) ? props.SnippetICD : 0f);
		control.TriggerSnippets(trigger, eProps, 1f);
	}

	// Token: 0x06000502 RID: 1282 RVA: 0x00024F78 File Offset: 0x00023178
	public void Use(EntityControl control, EffectProperties effectProps)
	{
		this.properties = effectProps;
		if (this.currentCD < 0.1f)
		{
			this.currentCD = 0.1f;
		}
		if (this.ChargeCount <= 1 || this.ChargesAvailable >= this.ChargeCount)
		{
			this.SetFullCooldown(control, effectProps);
		}
		this.ChargeUseCD = Mathf.Min(this.props.MinCancelTime, this.currentCD);
		this.properties.CauseName = this.rootNode.Name;
		this.properties.CauseID = this.rootNode.guid;
		this.ChargesAvailable = Mathf.Max(0, this.ChargesAvailable - 1);
	}

	// Token: 0x06000503 RID: 1283 RVA: 0x00025020 File Offset: 0x00023220
	public void UpdateCooldownInfo(EntityControl control, EffectProperties effectProps)
	{
		AbilityRootNode.UsageProps props = this.props;
		float num = (props != null) ? props.Cooldown : 0.1f;
		AIControl aicontrol = control as AIControl;
		if (aicontrol != null && aicontrol.TeamID == 2)
		{
			num = AIManager.ModifyBaseCooldown(num, aicontrol);
		}
		num = control.ModifyCooldownPassive(effectProps, num);
		float a = num;
		float a2 = 0.1f;
		AbilityRootNode.UsageProps props2 = this.props;
		num = Mathf.Max(a, Mathf.Max(a2, (props2 != null) ? props2.MinCooldown : 0.1f));
		this.AbilityCooldown = num;
	}

	// Token: 0x06000504 RID: 1284 RVA: 0x00025098 File Offset: 0x00023298
	public void SetFullCooldown(EntityControl control, EffectProperties effectProps)
	{
		AbilityRootNode.UsageProps props = this.props;
		float num = (props != null) ? props.Cooldown : 0.1f;
		AIControl aicontrol = control as AIControl;
		if (aicontrol != null && aicontrol.TeamID == 2)
		{
			num = AIManager.ModifyBaseCooldown(num, aicontrol);
		}
		num = control.ModifyCooldownPassive(effectProps, num);
		float a = num;
		float a2 = 0.1f;
		AbilityRootNode.UsageProps props2 = this.props;
		num = Mathf.Max(a, Mathf.Max(a2, (props2 != null) ? props2.MinCooldown : 0.1f));
		this.AbilityCooldown = num;
		this.currentCD = num;
	}

	// Token: 0x040003EA RID: 1002
	public AbilityTree AbilityTree;

	// Token: 0x040003EB RID: 1003
	[NonSerialized]
	public AbilityRootNode current;

	// Token: 0x040003EC RID: 1004
	[NonSerialized]
	public float currentCD;

	// Token: 0x040003ED RID: 1005
	[NonSerialized]
	public float Lifetime;

	// Token: 0x040003EE RID: 1006
	[NonSerialized]
	public EffectProperties properties;

	// Token: 0x040003EF RID: 1007
	[NonSerialized]
	public bool AutoFiring;

	// Token: 0x040003F0 RID: 1008
	public int AbilityUseNumber;

	// Token: 0x040003F1 RID: 1009
	[NonSerialized]
	public float AbilityCooldown = 0.1f;

	// Token: 0x040003F2 RID: 1010
	[NonSerialized]
	public float SnippetCD;

	// Token: 0x040003F3 RID: 1011
	[NonSerialized]
	public int ChargesAvailable = 1;

	// Token: 0x040003F4 RID: 1012
	private float ChargeUseCD;
}
