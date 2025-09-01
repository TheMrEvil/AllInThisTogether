using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x0200007D RID: 125
public class EntityHealth : MonoBehaviour
{
	// Token: 0x1700006B RID: 107
	// (get) Token: 0x0600052F RID: 1327 RVA: 0x000265CA File Offset: 0x000247CA
	public int DefaultMaxHealth
	{
		get
		{
			return this.BaseMaxHealth;
		}
	}

	// Token: 0x1700006C RID: 108
	// (get) Token: 0x06000530 RID: 1328 RVA: 0x000265D4 File Offset: 0x000247D4
	public int MaxHealth
	{
		get
		{
			if (this.cachedMaxHP == 0 && Application.isPlaying && this.control != null)
			{
				float num = 1f;
				AIControl aicontrol = this.control as AIControl;
				if (aicontrol != null && AIManager.instance != null && !(aicontrol is AITrivialControl) && !TutorialManager.InTutorial)
				{
					num = AIManager.instance.HealthAndShieldMult(aicontrol);
				}
				this.cachedMaxHP = (int)this.control.GetPassiveMod(Passive.EntityValue.Health, (float)this.BaseMaxHealth * num);
				this.cachedMaxHP = Mathf.Max(1, this.cachedMaxHP);
			}
			return this.cachedMaxHP;
		}
	}

	// Token: 0x1700006D RID: 109
	// (get) Token: 0x06000531 RID: 1329 RVA: 0x00026678 File Offset: 0x00024878
	public int MaxShield
	{
		get
		{
			if (this.cachedMaxShield == -1 && this.control != null)
			{
				float num = 1f;
				AIControl aicontrol = this.control as AIControl;
				if (aicontrol != null && AIManager.instance != null && !(aicontrol is AITrivialControl))
				{
					num = AIManager.instance.HealthAndShieldMult(aicontrol);
				}
				this.cachedMaxShield = (int)this.control.GetPassiveMod(Passive.EntityValue.Shield, (float)this.BaseMaxShield * num);
				this.cachedMaxShield = Mathf.Max(0, this.cachedMaxShield);
			}
			return this.cachedMaxShield;
		}
	}

	// Token: 0x1700006E RID: 110
	// (get) Token: 0x06000532 RID: 1330 RVA: 0x00026706 File Offset: 0x00024906
	public float TimeSinceLastDamage
	{
		get
		{
			return Time.realtimeSinceStartup - this.lastDamagedTime;
		}
	}

	// Token: 0x1700006F RID: 111
	// (get) Token: 0x06000533 RID: 1331 RVA: 0x00026714 File Offset: 0x00024914
	internal bool isLocalControl
	{
		get
		{
			return this.control == null || this.control.IsMine;
		}
	}

	// Token: 0x17000070 RID: 112
	// (get) Token: 0x06000534 RID: 1332 RVA: 0x00026731 File Offset: 0x00024931
	public float CurrentHealthProportion
	{
		get
		{
			return (float)this.health / (float)this.MaxHealth;
		}
	}

	// Token: 0x17000071 RID: 113
	// (get) Token: 0x06000535 RID: 1333 RVA: 0x00026742 File Offset: 0x00024942
	public float CurrentShieldProportion
	{
		get
		{
			if (this.MaxShield <= 0)
			{
				return 0f;
			}
			return this.shield / (float)this.MaxShield;
		}
	}

	// Token: 0x17000072 RID: 114
	// (get) Token: 0x06000536 RID: 1334 RVA: 0x00026761 File Offset: 0x00024961
	public float OvershieldValue
	{
		get
		{
			if (this.shield <= 0f || this.shield < (float)this.MaxShield)
			{
				return 0f;
			}
			return this.shield - (float)Mathf.Max(this.MaxShield, 0);
		}
	}

	// Token: 0x17000073 RID: 115
	// (get) Token: 0x06000537 RID: 1335 RVA: 0x00026799 File Offset: 0x00024999
	public bool ShieldCharging
	{
		get
		{
			return this.shield < (float)this.MaxShield && this.shieldDelay <= 0f;
		}
	}

	// Token: 0x17000074 RID: 116
	// (get) Token: 0x06000538 RID: 1336 RVA: 0x000267BC File Offset: 0x000249BC
	public bool HasOvershield
	{
		get
		{
			return this.shield > (float)this.MaxShield;
		}
	}

	// Token: 0x06000539 RID: 1337 RVA: 0x000267CD File Offset: 0x000249CD
	public virtual void Awake()
	{
		this.control = base.GetComponent<EntityControl>();
		this.health = this.BaseMaxHealth;
	}

	// Token: 0x0600053A RID: 1338 RVA: 0x000267E8 File Offset: 0x000249E8
	public virtual void Setup()
	{
		this.Reset();
		this.OnDamageTaken = (Action<DamageInfo>)Delegate.Combine(this.OnDamageTaken, new Action<DamageInfo>(this.DamageTriggerSnippet));
		this.OnShieldsDepleted = (Action<DamageInfo>)Delegate.Combine(this.OnShieldsDepleted, new Action<DamageInfo>(this.ShieldBreakSnippet));
		this.OnRevive = (Action<int>)Delegate.Combine(this.OnRevive, new Action<int>(this.OnReviveSnippet));
	}

	// Token: 0x0600053B RID: 1339 RVA: 0x00026861 File Offset: 0x00024A61
	public virtual void Reset()
	{
		this.health = this.MaxHealth;
		this.shield = (float)this.MaxShield;
	}

	// Token: 0x0600053C RID: 1340 RVA: 0x0002687C File Offset: 0x00024A7C
	public void Transform(EntityHealth newHealth)
	{
		this.BaseMaxHealth = newHealth.BaseMaxHealth;
		this.BaseMaxShield = newHealth.BaseMaxShield;
		this.Immortal = newHealth.Immortal;
	}

	// Token: 0x0600053D RID: 1341 RVA: 0x000268A4 File Offset: 0x00024AA4
	public void DebugDamage()
	{
		DamageInfo dmg = new DamageInfo(25f, DNumType.Default, this.control.ViewID, 0f, null);
		this.ApplyDamageImmediate(dmg);
	}

	// Token: 0x0600053E RID: 1342 RVA: 0x000268D5 File Offset: 0x00024AD5
	public void DebugKill()
	{
		if (!this.control.IsMine)
		{
			this.control.net.Kill(null);
			return;
		}
		this.Die(null);
	}

	// Token: 0x0600053F RID: 1343 RVA: 0x000268FD File Offset: 0x00024AFD
	public void DebugHeal()
	{
		this.Heal(new DamageInfo(25));
	}

	// Token: 0x06000540 RID: 1344 RVA: 0x0002690C File Offset: 0x00024B0C
	public void DebugAddShield()
	{
		this.ModifyShield(25, null);
	}

	// Token: 0x06000541 RID: 1345 RVA: 0x00026917 File Offset: 0x00024B17
	public void DebugRevive()
	{
		this.Revive(1f);
	}

	// Token: 0x06000542 RID: 1346 RVA: 0x00026924 File Offset: 0x00024B24
	public void Heal(DamageInfo heal)
	{
		if (this.isDead)
		{
			return;
		}
		int num = this.health;
		if (heal.Amount < 1f && heal.Amount > 0f)
		{
			this.healLeftover += heal.Amount;
			if (this.healLeftover >= 1f)
			{
				this.healLeftover -= 1f;
				heal.Amount += 1f;
			}
		}
		this.health += (int)heal.Amount;
		this.health = Mathf.Clamp(this.health, 0, this.MaxHealth);
		int obj = this.health - num;
		if (this.health > 0)
		{
			Action<int> onHealed = this.OnHealed;
			if (onHealed != null)
			{
				onHealed(obj);
			}
		}
		this.control.TriggerSnippets(EventTrigger.HealRecieved, heal.GenerateEffectProperties(), 1f);
	}

	// Token: 0x06000543 RID: 1347 RVA: 0x00026A07 File Offset: 0x00024C07
	public void FillShield()
	{
		if (this.shield >= (float)this.MaxShield)
		{
			return;
		}
		this.shield = (float)this.MaxShield;
	}

	// Token: 0x06000544 RID: 1348 RVA: 0x00026A28 File Offset: 0x00024C28
	public void BreakShield(EffectProperties props)
	{
		if (this.shield <= 0f)
		{
			return;
		}
		DamageInfo obj = new DamageInfo((float)((int)this.shield), DNumType.Default, this.control.ViewID, 1f, props);
		this.shield = 0f;
		this.shieldDelay = this.control.GetPassiveMod(Passive.EntityValue.ShieldDelay, 4f);
		Action<DamageInfo> onShieldsDepleted = this.OnShieldsDepleted;
		if (onShieldsDepleted == null)
		{
			return;
		}
		onShieldsDepleted(obj);
	}

	// Token: 0x06000545 RID: 1349 RVA: 0x00026A98 File Offset: 0x00024C98
	public void ModifyShield(int delta, EffectProperties props)
	{
		if (delta > 0)
		{
			this.shield += (float)delta;
			return;
		}
		DamageInfo damageInfo = new DamageInfo(Mathf.Min(this.shield, (float)(-(float)delta)), DNumType.Default, this.control.ViewID, 1f, props);
		this.TryDamage(damageInfo);
	}

	// Token: 0x06000546 RID: 1350 RVA: 0x00026AE8 File Offset: 0x00024CE8
	public void TryDamage(DamageInfo damageInfo)
	{
		if (this.control is PlayerControl && damageInfo.SourceID != this.control.ViewID && PlayerControl.GetPlayerFromViewID(damageInfo.SourceID) != null)
		{
			return;
		}
		if (damageInfo.SourceID == PlayerControl.MyViewID)
		{
			AIControl aicontrol = this.control as AIControl;
			if (aicontrol != null)
			{
				aicontrol.localPlayerDamaged = true;
			}
		}
		if (this.isLocalControl)
		{
			this.ApplyDamageImmediate(damageInfo);
			return;
		}
		this.control.net.PropagateDamage(damageInfo);
		this.control.display.LocalDamageDone(damageInfo);
	}

	// Token: 0x06000547 RID: 1351 RVA: 0x00026B80 File Offset: 0x00024D80
	public virtual void ApplyDamageImmediate(DamageInfo dmg)
	{
		this.ApplyDamageLeftover(ref dmg);
		this.ApplyDamageShield(ref dmg);
		int num = (int)dmg.Amount;
		this.health -= num;
		this.health = Mathf.Clamp(this.health, 0, this.MaxHealth);
		this.lastDamagedTime = Time.realtimeSinceStartup;
		Action<DamageInfo> onDamageTaken = this.OnDamageTaken;
		if (onDamageTaken != null)
		{
			onDamageTaken(dmg);
		}
		if (this.health <= 0 && this.isLocalControl)
		{
			if (this.Immortal)
			{
				this.health = 1;
				return;
			}
			this.DieFromDamage(dmg);
		}
	}

	// Token: 0x06000548 RID: 1352 RVA: 0x00026C10 File Offset: 0x00024E10
	internal void ApplyDamageLeftover(ref DamageInfo dmg)
	{
		if (dmg.Amount < 1f && dmg.Amount > 0f)
		{
			this.dmgLeftover += dmg.Amount;
			if (this.dmgLeftover >= 1f)
			{
				this.dmgLeftover -= 1f;
				dmg.Amount += 1f;
			}
		}
	}

	// Token: 0x06000549 RID: 1353 RVA: 0x00026C80 File Offset: 0x00024E80
	internal void ApplyDamageShield(ref DamageInfo dmg)
	{
		if (dmg.Amount >= 1f)
		{
			this.shieldDelay = this.control.GetPassiveMod(Passive.EntityValue.ShieldDelay, 4f);
		}
		if (this.shield <= 0f)
		{
			return;
		}
		DamageInfo damageInfo = dmg.Copy();
		float num = Mathf.Min(dmg.Amount, this.shield);
		damageInfo.Amount = (float)((int)num);
		this.shield -= num;
		dmg.Amount -= (float)Mathf.FloorToInt(num);
		dmg.ShieldAmount = damageInfo.Amount;
		if (this.shield <= 0f && (this.MaxShield > 0 || num >= 20f))
		{
			Action<DamageInfo> onShieldsDepleted = this.OnShieldsDepleted;
			if (onShieldsDepleted == null)
			{
				return;
			}
			onShieldsDepleted(damageInfo);
		}
	}

	// Token: 0x0600054A RID: 1354 RVA: 0x00026D44 File Offset: 0x00024F44
	internal void DieFromDamage(DamageInfo dmg)
	{
		this.health = 0;
		EntityControl entity = EntityControl.GetEntity(dmg.SourceID);
		if (entity != null && !(entity is PlayerControl))
		{
			entity.OnKilledEntity(this.control);
		}
		this.Die(dmg);
	}

	// Token: 0x0600054B RID: 1355 RVA: 0x00026D88 File Offset: 0x00024F88
	internal virtual void Update()
	{
		if (this.isLocalControl)
		{
			if (this.health <= 0 && !this.isDead)
			{
				this.Die(null);
			}
			else if (this.health > 0 && !this.isDead)
			{
				this.Revive(1f);
			}
		}
		if (this.isDead)
		{
			return;
		}
		if (this.health > this.MaxHealth)
		{
			this.health = this.MaxHealth;
		}
		this.UpdateShield();
		if (this.isLocalControl && this.MaxHealth > this.lastMax && this.lastMax > 0)
		{
			int num = this.MaxHealth - this.lastMax;
			this.health += num;
		}
		this.lastMax = this.MaxHealth;
		this.ResetCached();
	}

	// Token: 0x0600054C RID: 1356 RVA: 0x00026E4C File Offset: 0x0002504C
	private void UpdateShield()
	{
		if (this.shield > (float)this.MaxShield + this.control.GetPassiveMod(Passive.EntityValue.ShieldOverchargeMax, 0f))
		{
			float num = this.shield - (float)this.MaxShield;
			if (num < (float)(100 + this.MaxShield))
			{
				this.shield -= 3f * Mathf.Sqrt(Mathf.Max(1f, num)) * GameplayManager.deltaTime;
			}
			else
			{
				this.shield -= Mathf.Max(1f, num) * GameplayManager.deltaTime;
			}
		}
		if (this.shieldDelay > 0f)
		{
			this.shieldDelay -= GameplayManager.deltaTime;
			if (this.shieldDelay <= 0f)
			{
				this.ResetShieldCD(true);
				return;
			}
		}
		else if (this.shield < (float)this.MaxShield)
		{
			this.shield = Mathf.MoveTowards(this.shield, (float)this.MaxShield, GameplayManager.deltaTime * (float)this.MaxShield * 0.25f);
			if (this.shield >= (float)this.MaxShield)
			{
				Action onShieldRecharged = this.OnShieldRecharged;
				if (onShieldRecharged == null)
				{
					return;
				}
				onShieldRecharged();
			}
		}
	}

	// Token: 0x0600054D RID: 1357 RVA: 0x00026F6C File Offset: 0x0002516C
	public void ResetShieldCD(bool forceEffect = false)
	{
		if (this.shieldDelay > 0f || forceEffect)
		{
			Action onShieldChargeStart = this.OnShieldChargeStart;
			if (onShieldChargeStart != null)
			{
				onShieldChargeStart();
			}
		}
		this.shieldDelay = 0f;
	}

	// Token: 0x0600054E RID: 1358 RVA: 0x00026F9B File Offset: 0x0002519B
	private void ResetCached()
	{
		this.cachedMaxHP = 0;
		this.cachedMaxShield = -1;
	}

	// Token: 0x0600054F RID: 1359 RVA: 0x00026FAB File Offset: 0x000251AB
	public void Die(DamageInfo dmg)
	{
		if (this.isDead)
		{
			return;
		}
		this.isDead = true;
		if (this.OnDie != null)
		{
			this.OnDie(dmg);
		}
	}

	// Token: 0x06000550 RID: 1360 RVA: 0x00026FD1 File Offset: 0x000251D1
	public void ReviveFromEffect(float f)
	{
		base.StartCoroutine(this.ReviveDelayed(f));
	}

	// Token: 0x06000551 RID: 1361 RVA: 0x00026FE1 File Offset: 0x000251E1
	private IEnumerator ReviveDelayed(float prop)
	{
		yield return new WaitForSeconds(0.25f);
		this.Revive(prop);
		yield break;
	}

	// Token: 0x06000552 RID: 1362 RVA: 0x00026FF8 File Offset: 0x000251F8
	public virtual void Revive(float portionHeal = 1f)
	{
		if (!this.isDead)
		{
			return;
		}
		this.isDead = false;
		this.Heal(new DamageInfo(Mathf.CeilToInt((float)this.MaxHealth * portionHeal)));
		if (this.OnRevive != null)
		{
			this.OnRevive(this.health);
		}
	}

	// Token: 0x06000553 RID: 1363 RVA: 0x00027048 File Offset: 0x00025248
	private void DamageTriggerSnippet(DamageInfo dmg)
	{
		bool flag = this.control.CanTriggerSnippets(EventTrigger.DamageTaken, true, dmg.SnippetChance);
		bool flag2 = dmg.DamageType == DNumType.Crit && this.control.CanTriggerSnippets(EventTrigger.CriticalTaken, true, dmg.SnippetChance);
		if (!flag && !flag2)
		{
			return;
		}
		EffectProperties effectProperties = new EffectProperties(this.control);
		effectProperties.StartLoc = (effectProperties.OutLoc = global::Pose.WorldPoint(dmg.AtPoint, (dmg.AtPoint - this.control.display.CenterOfMass.position).normalized));
		effectProperties.SourceControl = this.control;
		effectProperties.Affected = this.control.gameObject;
		EffectProperties effectProperties2 = effectProperties;
		EntityControl entity = EntityControl.GetEntity(dmg.SourceID);
		effectProperties2.SeekTarget = ((entity != null) ? entity.gameObject : null);
		effectProperties.AbilityType = dmg.AbilityType;
		effectProperties.SourceType = dmg.ActionSource;
		effectProperties.EffectSource = dmg.EffectSource;
		effectProperties.SetExtra(EProp.Snip_DamageTaken, dmg.TotalAmount);
		this.control.TriggerSnippets(EventTrigger.DamageTaken, effectProperties, dmg.SnippetChance);
		if (dmg.DamageType == DNumType.Crit)
		{
			this.control.TriggerSnippets(EventTrigger.CriticalTaken, effectProperties.Copy(false), dmg.SnippetChance);
		}
	}

	// Token: 0x06000554 RID: 1364 RVA: 0x00027180 File Offset: 0x00025380
	private void ShieldBreakSnippet(DamageInfo dmg)
	{
		if (!this.control.CanTriggerSnippets(EventTrigger.ShieldBroken, true, dmg.SnippetChance))
		{
			return;
		}
		EffectProperties effectProperties = new EffectProperties(this.control);
		effectProperties.StartLoc = (effectProperties.OutLoc = global::Pose.WorldPoint(dmg.AtPoint, (dmg.AtPoint - this.control.display.CenterOfMass.position).normalized));
		effectProperties.SourceControl = this.control;
		effectProperties.Affected = this.control.gameObject;
		EffectProperties effectProperties2 = effectProperties;
		EntityControl entity = EntityControl.GetEntity(dmg.SourceID);
		effectProperties2.SeekTarget = ((entity != null) ? entity.gameObject : null);
		effectProperties.AbilityType = dmg.AbilityType;
		effectProperties.SourceType = dmg.ActionSource;
		effectProperties.EffectSource = dmg.EffectSource;
		effectProperties.SetExtra(EProp.Snip_DamageTaken, dmg.Amount);
		this.control.TriggerSnippets(EventTrigger.ShieldBroken, effectProperties, 1f);
	}

	// Token: 0x06000555 RID: 1365 RVA: 0x00027274 File Offset: 0x00025474
	private void OnReviveSnippet(int hp)
	{
		this.control.TriggerSnippets(EventTrigger.Revived, null, 1f);
	}

	// Token: 0x06000556 RID: 1366 RVA: 0x0002728C File Offset: 0x0002548C
	public void UpdateHealthFromNetwork(int hp, float shield, float shieldDelay, bool dead)
	{
		if (!dead && this.isDead)
		{
			this.Revive(1f);
		}
		this.health = hp;
		this.shield = shield;
		this.shieldDelay = shieldDelay;
		if (dead && !this.isDead)
		{
			this.Die(null);
		}
	}

	// Token: 0x06000557 RID: 1367 RVA: 0x000272D8 File Offset: 0x000254D8
	public EntityHealth()
	{
	}

	// Token: 0x0400041F RID: 1055
	public const float SHIELD_DELAY = 4f;

	// Token: 0x04000420 RID: 1056
	internal EntityControl control;

	// Token: 0x04000421 RID: 1057
	[SerializeField]
	private int BaseMaxHealth = 50;

	// Token: 0x04000422 RID: 1058
	[SerializeField]
	private int BaseMaxShield;

	// Token: 0x04000423 RID: 1059
	public bool Immortal;

	// Token: 0x04000424 RID: 1060
	private int cachedMaxHP;

	// Token: 0x04000425 RID: 1061
	public int health = 1;

	// Token: 0x04000426 RID: 1062
	private float dmgLeftover;

	// Token: 0x04000427 RID: 1063
	private float healLeftover;

	// Token: 0x04000428 RID: 1064
	private int cachedMaxShield = -1;

	// Token: 0x04000429 RID: 1065
	public float shield = 1f;

	// Token: 0x0400042A RID: 1066
	public float shieldDelay;

	// Token: 0x0400042B RID: 1067
	public bool isDead;

	// Token: 0x0400042C RID: 1068
	[NonSerialized]
	internal float lastDamagedTime;

	// Token: 0x0400042D RID: 1069
	public Action<DamageInfo> OnDie;

	// Token: 0x0400042E RID: 1070
	public Action<int> OnRevive;

	// Token: 0x0400042F RID: 1071
	public Action<DamageInfo> OnDamageTaken;

	// Token: 0x04000430 RID: 1072
	public Action<int> OnHealed;

	// Token: 0x04000431 RID: 1073
	public Action<DamageInfo> OnShieldsDepleted;

	// Token: 0x04000432 RID: 1074
	public Action OnShieldChargeStart;

	// Token: 0x04000433 RID: 1075
	public Action OnShieldRecharged;

	// Token: 0x04000434 RID: 1076
	private int lastMax;

	// Token: 0x0200049A RID: 1178
	[CompilerGenerated]
	private sealed class <ReviveDelayed>d__66 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x06002217 RID: 8727 RVA: 0x000C5967 File Offset: 0x000C3B67
		[DebuggerHidden]
		public <ReviveDelayed>d__66(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x06002218 RID: 8728 RVA: 0x000C5976 File Offset: 0x000C3B76
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06002219 RID: 8729 RVA: 0x000C5978 File Offset: 0x000C3B78
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			EntityHealth entityHealth = this;
			if (num == 0)
			{
				this.<>1__state = -1;
				this.<>2__current = new WaitForSeconds(0.25f);
				this.<>1__state = 1;
				return true;
			}
			if (num != 1)
			{
				return false;
			}
			this.<>1__state = -1;
			entityHealth.Revive(prop);
			return false;
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x0600221A RID: 8730 RVA: 0x000C59D0 File Offset: 0x000C3BD0
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x0600221B RID: 8731 RVA: 0x000C59D8 File Offset: 0x000C3BD8
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x0600221C RID: 8732 RVA: 0x000C59DF File Offset: 0x000C3BDF
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x04002362 RID: 9058
		private int <>1__state;

		// Token: 0x04002363 RID: 9059
		private object <>2__current;

		// Token: 0x04002364 RID: 9060
		public EntityHealth <>4__this;

		// Token: 0x04002365 RID: 9061
		public float prop;
	}
}
