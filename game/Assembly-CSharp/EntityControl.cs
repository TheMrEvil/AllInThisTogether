using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using Photon.Pun;
using UnityEngine;

// Token: 0x02000078 RID: 120
public class EntityControl : MonoBehaviour
{
	// Token: 0x17000058 RID: 88
	// (get) Token: 0x0600048E RID: 1166 RVA: 0x00022731 File Offset: 0x00020931
	public bool IsMine
	{
		get
		{
			return this.net == null || this.view == null || this.view.IsMine || !PhotonNetwork.InRoom;
		}
	}

	// Token: 0x17000059 RID: 89
	// (get) Token: 0x0600048F RID: 1167 RVA: 0x00022766 File Offset: 0x00020966
	public bool IsDead
	{
		get
		{
			return this.health.isDead;
		}
	}

	// Token: 0x1700005A RID: 90
	// (get) Token: 0x06000490 RID: 1168 RVA: 0x00022773 File Offset: 0x00020973
	public PhotonView view
	{
		get
		{
			return this.net.view;
		}
	}

	// Token: 0x1700005B RID: 91
	// (get) Token: 0x06000491 RID: 1169 RVA: 0x00022780 File Offset: 0x00020980
	public int ViewID
	{
		get
		{
			return this.net.view.ViewID;
		}
	}

	// Token: 0x1700005C RID: 92
	// (get) Token: 0x06000492 RID: 1170 RVA: 0x00022792 File Offset: 0x00020992
	// (set) Token: 0x06000493 RID: 1171 RVA: 0x0002279A File Offset: 0x0002099A
	public EntityControl currentTarget
	{
		[CompilerGenerated]
		get
		{
			return this.<currentTarget>k__BackingField;
		}
		[CompilerGenerated]
		internal set
		{
			this.<currentTarget>k__BackingField = value;
		}
	}

	// Token: 0x1700005D RID: 93
	// (get) Token: 0x06000494 RID: 1172 RVA: 0x000227A3 File Offset: 0x000209A3
	// (set) Token: 0x06000495 RID: 1173 RVA: 0x000227AB File Offset: 0x000209AB
	public EntityControl allyTarget
	{
		[CompilerGenerated]
		get
		{
			return this.<allyTarget>k__BackingField;
		}
		[CompilerGenerated]
		internal set
		{
			this.<allyTarget>k__BackingField = value;
		}
	}

	// Token: 0x1700005E RID: 94
	// (get) Token: 0x06000496 RID: 1174 RVA: 0x000227B4 File Offset: 0x000209B4
	public EffectProperties SelfProps
	{
		get
		{
			return this.internalSelfProps;
		}
	}

	// Token: 0x06000497 RID: 1175 RVA: 0x000227BC File Offset: 0x000209BC
	public virtual void Awake()
	{
		this.net = base.GetComponent<EntityNetworked>();
		this.display = base.GetComponent<EntityDisplay>();
		this.movement = base.GetComponent<EntityMovement>();
		this.health = base.GetComponent<EntityHealth>();
		this.audio = base.GetComponent<EntityAudio>();
		if (this.health != null)
		{
			EntityHealth entityHealth = this.health;
			entityHealth.OnDie = (Action<DamageInfo>)Delegate.Combine(entityHealth.OnDie, new Action<DamageInfo>(this.OnDie));
			EntityHealth entityHealth2 = this.health;
			entityHealth2.OnRevive = (Action<int>)Delegate.Combine(entityHealth2.OnRevive, new Action<int>(this.OnRevive));
			EntityHealth entityHealth3 = this.health;
			entityHealth3.OnShieldsDepleted = (Action<DamageInfo>)Delegate.Combine(entityHealth3.OnShieldsDepleted, new Action<DamageInfo>(this.OnShieldDepleted));
			EntityHealth entityHealth4 = this.health;
			entityHealth4.OnDamageTaken = (Action<DamageInfo>)Delegate.Combine(entityHealth4.OnDamageTaken, new Action<DamageInfo>(this.OnDamageTaken));
		}
		if (EntityControl.AllEntities == null)
		{
			EntityControl.AllEntities = new List<EntityControl>();
		}
		EntityControl.AllEntities.Add(this);
		if (this.TeamID == 2)
		{
			AIManager.Enemies.Add(this);
		}
	}

	// Token: 0x06000498 RID: 1176 RVA: 0x000228E6 File Offset: 0x00020AE6
	private IEnumerator Start()
	{
		this.Setup();
		this.internalSelfProps = new EffectProperties(this);
		yield return true;
		this.Initialized = true;
		this.OnSpawned();
		yield break;
	}

	// Token: 0x06000499 RID: 1177 RVA: 0x000228F5 File Offset: 0x00020AF5
	internal virtual void OnSpawned()
	{
		this.TriggerSnippets(EventTrigger.Spawned, new EffectProperties(this), 1f);
	}

	// Token: 0x0600049A RID: 1178 RVA: 0x0002290C File Offset: 0x00020B0C
	public virtual void Setup()
	{
		if (this.didSetup)
		{
			return;
		}
		this.net.Setup();
		this.display.Setup();
		this.movement.Setup();
		this.health.Setup();
		this.audio.Setup();
		this.RegisterSnippets();
		this.InitialTimers();
		this.didSetup = true;
		EntityControl.ResortAllEntities();
	}

	// Token: 0x0600049B RID: 1179 RVA: 0x00022974 File Offset: 0x00020B74
	public virtual void Update()
	{
		this.UpdateStatuses();
		if (RaidManager.IsInRaid)
		{
			AIControl aicontrol = this as AIControl;
			if (aicontrol != null && aicontrol.TeamID == 2 && !RaidManager.IsEncounterStarted)
			{
				return;
			}
		}
		foreach (Ability ability in this.Abilities)
		{
			this.TryUpdateAbility(ability, ability.properties);
			if (ability.SnippetCD > 0f)
			{
				ability.SnippetCD -= Time.deltaTime;
			}
		}
		this.internalSelfProps = new EffectProperties(this);
		this.UpdateTimers();
		if (this.DebugLines)
		{
			this.DebugDrawLines();
		}
	}

	// Token: 0x0600049C RID: 1180 RVA: 0x00022A38 File Offset: 0x00020C38
	internal void UpdateTimers()
	{
		foreach (int num in this.AbilityCasts.Keys.ToList<int>())
		{
			Dictionary<int, float> abilityCasts = this.AbilityCasts;
			int key = num;
			abilityCasts[key] -= GameplayManager.deltaTime;
			if (this.AbilityCasts[num] <= 0f)
			{
				this.AbilityCasts.Remove(num);
			}
		}
		foreach (TimeSince timeSince in this.TimerKeys)
		{
			Dictionary<TimeSince, float> timers = this.Timers;
			TimeSince key2 = timeSince;
			timers[key2] += GameplayManager.deltaTime;
		}
		this.TimerEvents();
	}

	// Token: 0x0600049D RID: 1181 RVA: 0x00022B30 File Offset: 0x00020D30
	internal void UpdateStatuses()
	{
		bool flag = false;
		try
		{
			for (int i = this.Statuses.Count - 1; i >= 0; i--)
			{
				int stacks = this.Statuses[i].Stacks;
				this.Statuses[i].UpdateTick(this);
				if (i >= 0 && i < this.Statuses.Count && this.Statuses[i].Duration <= 0f && this.Statuses[i].expires && (this.Statuses[i].Stacks <= 0 || this.Statuses[i].rootNode.TimeBehaviour == StatusRootNode.TimeoutBehaviour.Expire))
				{
					this.Statuses[i].OnExpire(this, false, stacks);
					this.Statuses.RemoveAt(i);
					flag = true;
				}
			}
		}
		catch
		{
		}
		if (flag)
		{
			this.ClearModCache();
		}
	}

	// Token: 0x0600049E RID: 1182 RVA: 0x00022C30 File Offset: 0x00020E30
	internal virtual void DebugDrawLines()
	{
		if (this.currentTarget != null)
		{
			UnityEngine.Debug.DrawLine(this.display.CenterOfMass.position, this.currentTarget.display.CenterOfMass.position, Color.red);
		}
	}

	// Token: 0x0600049F RID: 1183 RVA: 0x00022C70 File Offset: 0x00020E70
	private void LateUpdate()
	{
		if (this.ToApplyStatuses.Count > 0)
		{
			foreach (ValueTuple<int, int, float, int, int> valueTuple in this.ToApplyStatuses)
			{
				this.AddStatusEffect(valueTuple.Item1, valueTuple.Item2, valueTuple.Item3, valueTuple.Item4, false, valueTuple.Item5);
			}
			this.ToApplyStatuses.Clear();
		}
		if (this.ToRemoveStatuses.Count > 0)
		{
			for (int i = 0; i < this.ToRemoveStatuses.Count; i++)
			{
				ValueTuple<int, int, int> valueTuple2 = this.ToRemoveStatuses[i];
				this.RemoveStatusEffect(valueTuple2.Item1, valueTuple2.Item2, valueTuple2.Item3, false, false);
			}
			this.ToRemoveStatuses.Clear();
		}
	}

	// Token: 0x060004A0 RID: 1184 RVA: 0x00022D54 File Offset: 0x00020F54
	public virtual void ChangeTeam(int newTeam)
	{
		if (this.TeamID == newTeam)
		{
			return;
		}
		this.TeamID = newTeam;
		if (this.TeamID == 2)
		{
			AIManager.Enemies.Add(this);
			return;
		}
		AIManager.Enemies.Remove(this);
	}

	// Token: 0x060004A1 RID: 1185 RVA: 0x00022D88 File Offset: 0x00020F88
	public float TimeSinceLast(TimeSince Event)
	{
		float num = this.LastEventTime(Event);
		if (num == -1f)
		{
			return -1f;
		}
		return num;
	}

	// Token: 0x060004A2 RID: 1186 RVA: 0x00022DAC File Offset: 0x00020FAC
	public float LastEventTime(TimeSince Event)
	{
		if (this.TimerKeys.Contains(Event))
		{
			return this.Timers[Event];
		}
		return -1f;
	}

	// Token: 0x060004A3 RID: 1187 RVA: 0x00022DD0 File Offset: 0x00020FD0
	internal void TimerEvents()
	{
		if (this.TimeSinceLast(TimeSince.TimerEvent_0_25) < 0.25f)
		{
			return;
		}
		this.UpdateEventTime(TimeSince.TimerEvent_0_25);
		this.TriggerSnippets(EventTrigger.TimePassed_0_25, this.internalSelfProps, 1f);
		if ((double)this.TimeSinceLast(TimeSince.TimerEvent_0_5) < 0.5)
		{
			return;
		}
		this.UpdateEventTime(TimeSince.TimerEvent_0_5);
		this.TriggerSnippets(EventTrigger.TimePassed_0_5, this.internalSelfProps, 1f);
		if (this.TimeSinceLast(TimeSince.TimerEvent_1) < 1f)
		{
			return;
		}
		this.UpdateEventTime(TimeSince.TimerEvent_1);
		this.TriggerSnippets(EventTrigger.TimePassed_1, this.internalSelfProps, 1f);
		if (this.TimeSinceLast(TimeSince.TimerEvent_5) < 5f)
		{
			return;
		}
		this.UpdateEventTime(TimeSince.TimerEvent_5);
		this.TriggerSnippets(EventTrigger.TimePassed_5, this.internalSelfProps, 1f);
		if (this.TimeSinceLast(TimeSince.TimerEvent_10) < 10f)
		{
			return;
		}
		this.UpdateEventTime(TimeSince.TimerEvent_10);
		this.TriggerSnippets(EventTrigger.TimePassed_10, this.internalSelfProps, 1f);
	}

	// Token: 0x060004A4 RID: 1188 RVA: 0x00022EB7 File Offset: 0x000210B7
	internal void InitialTimers()
	{
		this.UpdateEventTime(TimeSince.Spawned);
		this.UpdateEventTime(TimeSince.TimerEvent_0_25);
		this.UpdateEventTime(TimeSince.TimerEvent_0_5);
		this.UpdateEventTime(TimeSince.TimerEvent_1);
		this.UpdateEventTime(TimeSince.TimerEvent_5);
		this.UpdateEventTime(TimeSince.TimerEvent_10);
		this.UpdateEventTime(TimeSince.UsedAbility);
	}

	// Token: 0x060004A5 RID: 1189 RVA: 0x00022EF0 File Offset: 0x000210F0
	public void UpdateEventTime(TimeSince Event)
	{
		if (!this.TimerKeys.Contains(Event))
		{
			this.TimerKeys.Add(Event);
			this.Timers.Add(Event, 0f);
			return;
		}
		this.Timers[Event] = 0f;
	}

	// Token: 0x060004A6 RID: 1190 RVA: 0x00022F30 File Offset: 0x00021130
	public bool CanBeInteractedBy(EntityControl attempter)
	{
		if (this.Affectable)
		{
			return true;
		}
		if (attempter != null)
		{
			AIControl aicontrol = this as AIControl;
			if (aicontrol != null)
			{
				return aicontrol.PetOwnerID == attempter.ViewID;
			}
		}
		return false;
	}

	// Token: 0x060004A7 RID: 1191 RVA: 0x00022F6C File Offset: 0x0002116C
	public static bool CanInteractWith(EffectProperties props, EntityControl entity, EffectInteractsWith mask)
	{
		if (mask == EffectInteractsWith.AllEntities)
		{
			return true;
		}
		if (mask == EffectInteractsWith.Environment)
		{
			return false;
		}
		if (mask == EffectInteractsWith.Self)
		{
			return props.SourceControl == entity;
		}
		if (mask == EffectInteractsWith.Players)
		{
			return entity is PlayerControl;
		}
		if (mask == EffectInteractsWith.Allies)
		{
			return entity.TeamID == props.SourceControl.TeamID;
		}
		if (mask == EffectInteractsWith.LocalPlayer)
		{
			return entity == PlayerControl.myInstance;
		}
		if (mask == EffectInteractsWith.Target)
		{
			return props.SourceControl.currentTarget == entity;
		}
		if (mask == EffectInteractsWith.Affected)
		{
			return props.AffectedControl == entity;
		}
		return entity.TeamID != props.SourceControl.TeamID;
	}

	// Token: 0x060004A8 RID: 1192 RVA: 0x0002300C File Offset: 0x0002120C
	public static bool CanInteractWith(EntityControl entity, int team, EffectInteractsWith mask)
	{
		if (mask == EffectInteractsWith.AllEntities)
		{
			return true;
		}
		if (mask == EffectInteractsWith.Environment)
		{
			return false;
		}
		if (mask == EffectInteractsWith.Self)
		{
			return false;
		}
		if (mask == EffectInteractsWith.Allies)
		{
			return entity.TeamID == team;
		}
		if (mask == EffectInteractsWith.LocalPlayer)
		{
			return entity == PlayerControl.myInstance;
		}
		if (mask == EffectInteractsWith.Players)
		{
			return entity is PlayerControl;
		}
		return entity.TeamID != team;
	}

	// Token: 0x060004A9 RID: 1193 RVA: 0x00023064 File Offset: 0x00021264
	public static bool CanInteractWith(ActionSource source, EntityControl entity, EffectInteractsWith mask)
	{
		if (mask == EffectInteractsWith.AllEntities)
		{
			return true;
		}
		if (mask == EffectInteractsWith.Environment)
		{
			return false;
		}
		if (mask == EffectInteractsWith.Self)
		{
			return source == ActionSource.Fountain;
		}
		if (mask == EffectInteractsWith.Allies)
		{
			return entity.TeamID == ((source == ActionSource.Fountain || source == ActionSource.Genre) ? 1 : 2);
		}
		if (mask == EffectInteractsWith.LocalPlayer)
		{
			return entity == PlayerControl.myInstance;
		}
		if (mask == EffectInteractsWith.Players)
		{
			return entity is PlayerControl;
		}
		return entity.TeamID == ((source == ActionSource.Fountain || source == ActionSource.Genre) ? 2 : 1);
	}

	// Token: 0x060004AA RID: 1194 RVA: 0x000230D4 File Offset: 0x000212D4
	public virtual bool TryActivateAbility(string GUID, EffectProperties props)
	{
		Ability ability = this.GetAbility(GUID);
		if (ability == null)
		{
			return false;
		}
		if (ability.CanUse(this) != CastFailedReason.None && this.view.IsMine)
		{
			return false;
		}
		props.SourceType = ActionSource.Ability;
		props.AbilityType = ability.PlayerAbilityType;
		props.StartLoc = (props.OutLoc = ability.rootNode.AtPoint(props).Copy());
		this.AbilityCasts.TryAdd(props.CastID, 10f);
		ability.Use(this, props);
		if (this is PlayerControl)
		{
			ability.AutoFiring = true;
		}
		ability.current = (ability.rootNode.Clone(null, false) as AbilityRootNode);
		ability.Lifetime = 0f;
		ability.current.Activate(props);
		ability.current.DoUpdate(props);
		this.UpdateEventTime(TimeSince.UsedAbility);
		return true;
	}

	// Token: 0x060004AB RID: 1195 RVA: 0x000231AC File Offset: 0x000213AC
	private bool TryUpdateAbility(Ability ability, EffectProperties props)
	{
		if (this.IsDead && (ability.current == null || !ability.current.isReleasing) && (ability.current == null || ability.current.PlrAbilityType != PlayerAbilityType.Ghost))
		{
			return false;
		}
		if (ability.currentCD > 0f && (ability.current == null || ability.current.isReleasing || ability.current.AbilityType == AbilityType.Instant))
		{
			ability.TickUpdate(this);
		}
		if (ability.current == null || ability.current.CurrentState == AbilityState.Finished)
		{
			return false;
		}
		ability.Lifetime += GameplayManager.deltaTime;
		if (props == null)
		{
			props = new EffectProperties(this);
		}
		EffectProperties effectProperties = props.Copy(false);
		effectProperties.StartLoc = (props.OutLoc = ability.location.Copy());
		effectProperties.SourceType = ActionSource.Ability;
		if (this.currentTarget)
		{
			effectProperties.SeekTarget = this.currentTarget.gameObject;
		}
		if (this.allyTarget)
		{
			effectProperties.AllyTarget = this.allyTarget.gameObject;
		}
		props.AddEntityAugments(this, true);
		effectProperties.SourceLocation = effectProperties.StartLoc.GetLocation().GetTransform(props);
		effectProperties.Lifetime = ability.Lifetime;
		effectProperties.SetExtra(EProp.ChargeTime, ability.Lifetime);
		ability.properties = effectProperties;
		AbilityState abilityState = ability.current.DoUpdate(effectProperties);
		if (abilityState == AbilityState.Finished)
		{
			if (!ability.current.isReleasing)
			{
				if (ability.current.AbilityType == AbilityType.Channel_Active)
				{
					PlayerControl playerControl = this as PlayerControl;
					if (playerControl != null)
					{
						playerControl.actions.TryReleaseAbility(ability.rootNode, false);
					}
				}
				ability.current.Release(effectProperties);
			}
			else
			{
				ability.current = null;
			}
		}
		this.UpdateEventTime(TimeSince.UsedAbility);
		return abilityState == AbilityState.Running;
	}

	// Token: 0x060004AC RID: 1196 RVA: 0x0002337C File Offset: 0x0002157C
	public virtual bool TryReleaseAbility(string GUID, EffectProperties props)
	{
		Ability ability = this.GetAbility(GUID);
		if (ability == null)
		{
			return false;
		}
		if (ability.current == null || !ability.IsActive(false))
		{
			return false;
		}
		props.SourceType = ActionSource.Ability;
		ability.current.Release(props);
		ability.current.DoUpdate(props);
		this.UpdateEventTime(TimeSince.UsedAbility);
		return true;
	}

	// Token: 0x060004AD RID: 1197 RVA: 0x000233D8 File Offset: 0x000215D8
	public void ResetCooldown(AbilityRootNode root)
	{
		if (!this.IsMine)
		{
			return;
		}
		Ability ability = this.GetAbility(root.guid);
		if (ability == null)
		{
			return;
		}
		ability.ResetCooldown();
	}

	// Token: 0x060004AE RID: 1198 RVA: 0x00023405 File Offset: 0x00021605
	public bool TryConsumeFirstHitEvent(EffectProperties props)
	{
		if (!this.AbilityCasts.ContainsKey(props.CastID))
		{
			return false;
		}
		this.AbilityCasts.Remove(props.CastID);
		return true;
	}

	// Token: 0x060004AF RID: 1199 RVA: 0x00023430 File Offset: 0x00021630
	public void ModifyCooldown(AbilityRootNode root, float addedValue)
	{
		if (!this.IsMine)
		{
			return;
		}
		Ability ability = this.GetAbility(root.guid);
		if (ability == null)
		{
			return;
		}
		if (addedValue < 0f && ability.currentCD + addedValue < ability.props.MinCooldown)
		{
			addedValue = -Mathf.Max(0f, Mathf.Min(-addedValue, ability.currentCD - ability.props.MinCooldown));
		}
		ability.ModifyCooldown(this, addedValue);
	}

	// Token: 0x060004B0 RID: 1200 RVA: 0x000234A4 File Offset: 0x000216A4
	public void SetCooldown(AbilityRootNode root, float value, EffectProperties props = null)
	{
		PlayerControl playerControl = this as PlayerControl;
		if (playerControl != null && playerControl != PlayerControl.myInstance)
		{
			return;
		}
		Ability ability = this.GetAbility(root.guid);
		if (ability == null)
		{
			return;
		}
		ability.SetCooldown(this, value);
		ability.UpdateCooldownInfo(this, props);
	}

	// Token: 0x060004B1 RID: 1201 RVA: 0x000234EC File Offset: 0x000216EC
	public Ability GetAbility(string GUID)
	{
		foreach (Ability ability in this.Abilities)
		{
			if (ability.GUID == GUID)
			{
				return ability;
			}
		}
		return null;
	}

	// Token: 0x060004B2 RID: 1202 RVA: 0x00023550 File Offset: 0x00021750
	public bool CanUseAbility(string GUID)
	{
		Ability ability = this.GetAbility(GUID);
		return ability != null && ability.CanUse(this) == CastFailedReason.None;
	}

	// Token: 0x060004B3 RID: 1203 RVA: 0x00023574 File Offset: 0x00021774
	public bool IsUsingAbility(AbilityTree tree, bool ignorePassives = false)
	{
		foreach (Ability ability in this.Abilities)
		{
			if (ability.rootNode != null && ability.rootNode.guid == tree.RootNode.guid && ability.IsActive(ignorePassives))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060004B4 RID: 1204 RVA: 0x000235FC File Offset: 0x000217FC
	public bool IsCastingAnyAbility()
	{
		using (List<Ability>.Enumerator enumerator = this.Abilities.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.IsActive(true))
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x060004B5 RID: 1205 RVA: 0x00023658 File Offset: 0x00021858
	public bool IsReleasingAbility(AbilityTree ability)
	{
		foreach (Ability ability2 in this.Abilities)
		{
			if (ability2.rootNode != null && ability2.rootNode.guid == ability.RootNode.guid && ability2.isReleasing)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060004B6 RID: 1206 RVA: 0x000236E0 File Offset: 0x000218E0
	public virtual bool CanUseAbilities()
	{
		return !this.HasStatusKeyword(StatusKeyword.Prevent_Abilities);
	}

	// Token: 0x060004B7 RID: 1207 RVA: 0x000236F0 File Offset: 0x000218F0
	public bool HasStatusKeyword(StatusKeyword keyword)
	{
		using (List<EntityControl.AppliedStatus>.Enumerator enumerator = this.Statuses.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.Keywords.Contains(keyword))
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x060004B8 RID: 1208 RVA: 0x00023750 File Offset: 0x00021950
	public void ResetAbilities()
	{
		EffectProperties effectProperties = new EffectProperties(this);
		foreach (Ability ability in this.Abilities)
		{
			this.TryReleaseAbility(ability.GUID, effectProperties.Copy(false));
		}
	}

	// Token: 0x060004B9 RID: 1209 RVA: 0x000237B8 File Offset: 0x000219B8
	public bool IsUsingActiveAbility()
	{
		using (List<Ability>.Enumerator enumerator = this.Abilities.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.IsActive(true))
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x060004BA RID: 1210 RVA: 0x00023814 File Offset: 0x00021A14
	public Ability GetCurrentActiveAbility()
	{
		foreach (Ability ability in this.Abilities)
		{
			if (ability.IsActive(true))
			{
				return ability;
			}
		}
		return null;
	}

	// Token: 0x060004BB RID: 1211 RVA: 0x00023870 File Offset: 0x00021A70
	internal Ability AddAbility(AbilityTree tree)
	{
		if (tree == null)
		{
			return null;
		}
		Ability ability = new Ability(tree);
		this.Abilities.Add(ability);
		return ability;
	}

	// Token: 0x060004BC RID: 1212 RVA: 0x0002389C File Offset: 0x00021A9C
	internal void RemoveAbility(AbilityTree tree)
	{
		if (tree == null)
		{
			return;
		}
		for (int i = this.Abilities.Count - 1; i >= 0; i--)
		{
			if (this.Abilities[i].AbilityTree == tree)
			{
				this.Abilities.RemoveAt(i);
			}
		}
	}

	// Token: 0x060004BD RID: 1213 RVA: 0x000238F0 File Offset: 0x00021AF0
	public bool AddStatusEffect(int StatusID, int sourceID, float duration, int stacks, bool delay, int depth)
	{
		if (delay)
		{
			this.ToApplyStatuses.Add(new ValueTuple<int, int, float, int, int>(StatusID, sourceID, duration, stacks, depth));
			return true;
		}
		foreach (EntityControl.AppliedStatus appliedStatus in this.Statuses)
		{
			if (appliedStatus.rootNode.guid.GetHashCode() == StatusID && (!appliedStatus.rootNode.UniquePerSource || appliedStatus.sourceID == sourceID))
			{
				if (appliedStatus.Duration >= duration - Mathf.Epsilon && (!appliedStatus.CanStack || appliedStatus.Stacks >= appliedStatus.MaxStacks))
				{
					return false;
				}
				if (appliedStatus.Keywords.Count > 0 && this.HasPassiveMod(new EntityPassive(Passive.EntityValue.ImpairDuration), true))
				{
					duration = this.GetPassiveMod(Passive.EntityValue.ImpairDuration, appliedStatus.GetProperties(this), duration);
				}
				appliedStatus.Add(duration, stacks, depth, this);
				this.ClearModCache();
				return true;
			}
		}
		StatusTree statusEffect = GraphDB.GetStatusEffect(StatusID);
		if (statusEffect == null)
		{
			UnityEngine.Debug.LogError("No Status found: " + StatusID.ToString());
			return false;
		}
		EntityControl.AppliedStatus appliedStatus2 = new EntityControl.AppliedStatus();
		StatusRootNode root = statusEffect.Root;
		appliedStatus2.HashCode = root.guid.GetHashCode();
		appliedStatus2.rootNode = root;
		appliedStatus2.StatusName = ((root != null) ? root.EffectName : null);
		appliedStatus2.Depth = depth;
		appliedStatus2.expires = (duration > 0f || root.TimeBehaviour == StatusRootNode.TimeoutBehaviour.DecrementStack);
		appliedStatus2.sourceID = sourceID;
		EntityControl.AppliedStatus appliedStatus3 = appliedStatus2;
		EntityControl source = appliedStatus2.Source;
		appliedStatus3.SetupOverrides(((source != null) ? source.AllAugments(true, null) : null) ?? null);
		appliedStatus2.Add(duration, stacks, depth, this);
		this.Statuses.Add(appliedStatus2);
		this.ClearModCache();
		appliedStatus2.OnApply(this);
		this.ClearModCache();
		return true;
	}

	// Token: 0x060004BE RID: 1214 RVA: 0x00023AF4 File Offset: 0x00021CF4
	public void RemoveStatusEffect(int StatusID, int SourceID, int stacks = 0, bool delay = false, bool byAll = false)
	{
		if (delay)
		{
			this.ToRemoveStatuses.Add(new ValueTuple<int, int, int>(StatusID, SourceID, stacks));
			return;
		}
		for (int i = this.Statuses.Count - 1; i >= 0; i--)
		{
			if (this.Statuses[i].rootNode.guid.GetHashCode() == StatusID && (!this.Statuses[i].rootNode.UniquePerSource || this.Statuses[i].sourceID == SourceID || byAll))
			{
				if (this.Statuses[i].Stacks > stacks && stacks > 0)
				{
					this.Statuses[i].RemoveStacks(stacks, this);
				}
				else
				{
					this.Statuses[i].OnExpire(this, this.IsDead, 0);
					if (this.Statuses.Count > i)
					{
						this.Statuses.RemoveAt(i);
					}
				}
			}
		}
		this.ClearModCache();
	}

	// Token: 0x060004BF RID: 1215 RVA: 0x00023BF0 File Offset: 0x00021DF0
	public bool HasStatusEffectGUID(string StatusID)
	{
		if (StatusID == null)
		{
			return false;
		}
		using (List<EntityControl.AppliedStatus>.Enumerator enumerator = this.Statuses.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.rootNode.guid == StatusID)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x060004C0 RID: 1216 RVA: 0x00023C5C File Offset: 0x00021E5C
	public bool HasStatusEffectHash(int StatusID)
	{
		using (List<EntityControl.AppliedStatus>.Enumerator enumerator = this.Statuses.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.HashCode == StatusID)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x060004C1 RID: 1217 RVA: 0x00023CB8 File Offset: 0x00021EB8
	public EntityControl.AppliedStatus GetStatusInfo(string StatusName)
	{
		StatusName = StatusName.ToLower();
		foreach (EntityControl.AppliedStatus appliedStatus in this.Statuses)
		{
			if (appliedStatus.rootNode.EffectName.ToLower().Equals(StatusName))
			{
				return appliedStatus;
			}
		}
		return null;
	}

	// Token: 0x060004C2 RID: 1218 RVA: 0x00023D2C File Offset: 0x00021F2C
	public EntityControl.AppliedStatus GetStatusInfoByID(string guid, int sourceID = -1)
	{
		foreach (EntityControl.AppliedStatus appliedStatus in this.Statuses)
		{
			if ((sourceID < 0 || appliedStatus.sourceID == sourceID) && appliedStatus.rootNode.guid == guid)
			{
				return appliedStatus;
			}
		}
		return null;
	}

	// Token: 0x060004C3 RID: 1219 RVA: 0x00023DA0 File Offset: 0x00021FA0
	public bool HasStatusFromEntity(string guid, int sourceID)
	{
		foreach (EntityControl.AppliedStatus appliedStatus in this.Statuses)
		{
			if (appliedStatus.rootNode.guid == guid && appliedStatus.sourceID == sourceID)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060004C4 RID: 1220 RVA: 0x00023E10 File Offset: 0x00022010
	public int NumStatusesApplied(string guid)
	{
		int num = 0;
		using (List<EntityControl>.Enumerator enumerator = EntityControl.AllEntities.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.GetStatusInfoByID(guid, this.ViewID) != null)
				{
					num++;
				}
			}
		}
		return num;
	}

	// Token: 0x060004C5 RID: 1221 RVA: 0x00023E70 File Offset: 0x00022070
	public void RemoveNegativeStatuses(bool fromWaveEnd)
	{
		for (int i = this.Statuses.Count - 1; i >= 0; i--)
		{
			if (this.Statuses[i].rootNode.IsNegative && (!fromWaveEnd || !this.Statuses[i].rootNode.PersistThroughWave))
			{
				this.Statuses[i].OnExpire(this, fromWaveEnd, 0);
				this.Statuses.RemoveAt(i);
			}
		}
		this.ClearModCache();
	}

	// Token: 0x060004C6 RID: 1222 RVA: 0x00023EF0 File Offset: 0x000220F0
	public void RemoveAllStatuses(bool fromDeath)
	{
		List<EntityControl.AppliedStatus> list = new List<EntityControl.AppliedStatus>();
		int i = this.Statuses.Count - 1;
		while (i >= 0)
		{
			if (!fromDeath)
			{
				goto IL_43;
			}
			if (!this.Statuses[i].rootNode.PersistThroughDeath)
			{
				list.Add(this.Statuses[i]);
				goto IL_43;
			}
			IL_4F:
			i--;
			continue;
			IL_43:
			this.Statuses.RemoveAt(i);
			goto IL_4F;
		}
		foreach (EntityControl.AppliedStatus appliedStatus in list)
		{
			appliedStatus.OnExpire(this, fromDeath, 0);
		}
		this.ClearModCache();
		if (this.IsMine)
		{
			this.net.ClearStatuses(fromDeath);
		}
	}

	// Token: 0x060004C7 RID: 1223 RVA: 0x00023FB0 File Offset: 0x000221B0
	public virtual Augments AllAugments(bool includeStatuses = true, Augments cache = null)
	{
		if (this.modCache != null && includeStatuses)
		{
			return this.modCache;
		}
		if (cache != null)
		{
			cache.Clear();
		}
		Augments augments = cache ?? new Augments();
		augments.Add(this.Augment);
		if (includeStatuses)
		{
			foreach (EntityControl.AppliedStatus appliedStatus in this.Statuses)
			{
				augments.Add(appliedStatus.GetMods());
			}
		}
		if (includeStatuses)
		{
			this.modCache = augments;
		}
		return augments;
	}

	// Token: 0x060004C8 RID: 1224 RVA: 0x0002404C File Offset: 0x0002224C
	public virtual bool HasPassiveMod(Passive p, bool checkStatuses = true)
	{
		if (this.Augment.HasPassive(p))
		{
			return true;
		}
		if (checkStatuses)
		{
			using (List<EntityControl.AppliedStatus>.Enumerator enumerator = this.Statuses.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.HasPassive(p))
					{
						return true;
					}
				}
			}
			return false;
		}
		return false;
	}

	// Token: 0x060004C9 RID: 1225 RVA: 0x000240BC File Offset: 0x000222BC
	public virtual Augments GetPassiveAugments(Passive p, bool canUseCache = false, Augments cache = null)
	{
		if (cache != null)
		{
			cache.Clear();
		}
		int hashCode = p.GetHashCode();
		Augments result;
		if (canUseCache && this.PassiveCache.TryGetValue(hashCode, out result))
		{
			return result;
		}
		Augments augments = cache ?? new Augments();
		this.Augment.GetPassiveAugments(p, ref augments);
		foreach (EntityControl.AppliedStatus appliedStatus in this.Statuses)
		{
			appliedStatus.rootNode.GetPassiveMods(p, appliedStatus.Stacks, ref augments, appliedStatus.OverrideAugments);
		}
		if (canUseCache)
		{
			this.PassiveCache.Add(hashCode, augments);
		}
		return augments;
	}

	// Token: 0x060004CA RID: 1226 RVA: 0x00024178 File Offset: 0x00022378
	public virtual Augments GetSnippetAugments(EventTrigger t, bool includeStatuses = true, Augments cache = null)
	{
		if (cache != null)
		{
			cache.Clear();
		}
		Augments augments = cache ?? new Augments();
		augments.Add(this.Augment.GetSnippetAugments(t));
		if (includeStatuses)
		{
			foreach (EntityControl.AppliedStatus appliedStatus in this.Statuses)
			{
				augments.Add(appliedStatus.rootNode.GetSnippetMods(t, appliedStatus.Stacks, appliedStatus.OverrideAugments));
			}
		}
		return augments;
	}

	// Token: 0x060004CB RID: 1227 RVA: 0x0002420C File Offset: 0x0002240C
	private void AddDebugAugment()
	{
		AugmentTree augmentByName = GraphDB.GetAugmentByName(this.DebugAugment);
		if (augmentByName == null)
		{
			UnityEngine.Debug.LogError("Could not find Augment with ID: " + this.DebugAugment.ToLower().Replace(" ", "_"));
			return;
		}
		this.Augment.Add(augmentByName, 1);
		this.ClearModCache();
	}

	// Token: 0x060004CC RID: 1228 RVA: 0x00024270 File Offset: 0x00022470
	private void RegisterSnippets()
	{
	}

	// Token: 0x060004CD RID: 1229 RVA: 0x00024272 File Offset: 0x00022472
	public virtual void AddAugment(Augments mod)
	{
		this.Augment.Add(mod);
		this.ClearModCache();
	}

	// Token: 0x060004CE RID: 1230 RVA: 0x00024286 File Offset: 0x00022486
	public virtual void AddAugment(AugmentTree mod, int count = 1)
	{
		if (mod == null)
		{
			return;
		}
		this.AddAugment(mod.Root, count);
		this.ClearModCache();
	}

	// Token: 0x060004CF RID: 1231 RVA: 0x000242A5 File Offset: 0x000224A5
	public virtual void RemoveAugment(AugmentTree mod, int count = 1)
	{
		this.Augment.Remove(mod, count);
		this.ClearModCache();
	}

	// Token: 0x060004D0 RID: 1232 RVA: 0x000242BA File Offset: 0x000224BA
	public virtual void AddAugment(AugmentRootNode mod, int count = 1)
	{
		this.TriggerSnippets(EventTrigger.ModAdded, null, 1f);
		Augments augment = this.Augment;
		if (augment != null)
		{
			augment.Add(mod, count);
		}
		mod.TryTrigger(this, EventTrigger.ThisChosen, new EffectProperties(this), 1f);
		this.ClearModCache();
	}

	// Token: 0x060004D1 RID: 1233 RVA: 0x000242F8 File Offset: 0x000224F8
	public virtual bool CanTriggerSnippets(EventTrigger trigger, bool includeStatuses = true, float chanceMult = 1f)
	{
		if ((!this.view.IsMine && !trigger.IsLocalTrigger()) || chanceMult <= 0f)
		{
			return false;
		}
		if (this.Augment.HasSnippet(trigger))
		{
			return true;
		}
		if (includeStatuses)
		{
			using (List<EntityControl.AppliedStatus>.Enumerator enumerator = this.Statuses.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.HasSnippet(trigger))
					{
						return true;
					}
				}
			}
			return false;
		}
		return false;
	}

	// Token: 0x060004D2 RID: 1234 RVA: 0x00024384 File Offset: 0x00022584
	public virtual void TriggerSnippets(EventTrigger trigger, EffectProperties props = null, float chanceMult = 1f)
	{
		if ((!this.view.IsMine && !trigger.IsLocalTrigger()) || chanceMult <= 0f)
		{
			return;
		}
		this.GetSnippetAugments(trigger, false, null).ApplySnippets(this, trigger, props, chanceMult);
		foreach (EntityControl.AppliedStatus appliedStatus in this.Statuses.Clone<EntityControl.AppliedStatus>())
		{
			if (this.Statuses.Contains(appliedStatus) && appliedStatus.HasSnippet(trigger))
			{
				appliedStatus.ApplySnippets(this, trigger, props, chanceMult);
			}
		}
	}

	// Token: 0x060004D3 RID: 1235 RVA: 0x00024428 File Offset: 0x00022628
	public void RunSnippetLocal(string ActionGUID, EffectProperties props)
	{
		ActionTree action = GraphDB.GetAction(ActionGUID);
		if (action == null)
		{
			UnityEngine.Debug.LogError("Failed to find ActionGraph with GUID: " + ActionGUID);
			return;
		}
		action.Root.Apply(props);
	}

	// Token: 0x060004D4 RID: 1236 RVA: 0x00024462 File Offset: 0x00022662
	internal void ClearModCache()
	{
		this.modCache = null;
		this.PassiveCache.Clear();
		this.tagsCache.Clear();
	}

	// Token: 0x060004D5 RID: 1237 RVA: 0x00024481 File Offset: 0x00022681
	public bool HasModTag(ModTag tag)
	{
		if (this.tagsCache.Count > 0)
		{
			return this.tagsCache.Contains(tag);
		}
		this.tagsCache.Clear();
		this.CollectModTags();
		return this.tagsCache.Contains(tag);
	}

	// Token: 0x060004D6 RID: 1238 RVA: 0x000244BC File Offset: 0x000226BC
	internal virtual void CollectModTags()
	{
		foreach (ModTag item in this.AllAugments(true, null).GetTags())
		{
			this.tagsCache.Add(item);
		}
		foreach (ModTag item2 in InkManager.PurchasedMods.GetTags())
		{
			this.tagsCache.Add(item2);
		}
		foreach (ModTag item3 in InkManager.PurchasedMods.GetTags())
		{
			this.tagsCache.Add(item3);
		}
	}

	// Token: 0x060004D7 RID: 1239 RVA: 0x000245B4 File Offset: 0x000227B4
	public bool HasAugment(string guid, bool includeStatuses = true)
	{
		return this.AllAugments(includeStatuses, null).TreeIDs.Contains(guid);
	}

	// Token: 0x060004D8 RID: 1240 RVA: 0x000245CC File Offset: 0x000227CC
	public bool HasAbility(string guid)
	{
		using (List<Ability>.Enumerator enumerator = this.Abilities.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.AbilityTree.ID == guid)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x060004D9 RID: 1241 RVA: 0x00024630 File Offset: 0x00022830
	public virtual float GetPassiveMod(Passive.EntityValue passive, float startVal)
	{
		return this.GetPassiveMod(passive, startVal);
	}

	// Token: 0x060004DA RID: 1242 RVA: 0x00024640 File Offset: 0x00022840
	public virtual float GetPassiveMod(EntityPassive passive, float startVal)
	{
		if (!this.HasPassiveMod(passive, true))
		{
			return startVal;
		}
		EffectProperties effectProperties = this.SelfProps;
		if (effectProperties == null)
		{
			effectProperties = new EffectProperties(this);
		}
		return this.GetPassiveMod(passive, effectProperties, startVal);
	}

	// Token: 0x060004DB RID: 1243 RVA: 0x00024673 File Offset: 0x00022873
	public virtual float GetPassiveMod(EntityPassive passive, EffectProperties props, float startVal)
	{
		return this.GetPassiveAugments(passive, true, null).GetModifiedValue(this, props, passive, startVal);
	}

	// Token: 0x060004DC RID: 1244 RVA: 0x00024687 File Offset: 0x00022887
	public virtual float ModifyDamageTaken(EffectProperties props, float DamageAmount)
	{
		if (!this.HasPassiveMod(new EntityPassive(Passive.EntityValue.DamageTaken), true))
		{
			return DamageAmount;
		}
		return this.GetPassiveMod(Passive.EntityValue.DamageTaken, props, DamageAmount);
	}

	// Token: 0x060004DD RID: 1245 RVA: 0x000246B0 File Offset: 0x000228B0
	public float ModifyManaCost(EffectProperties props, float Cost)
	{
		AbilityPassive abilityPassive = new ValueTuple<PlayerAbilityType, Passive.AbilityValue>((props != null) ? props.AbilityType : PlayerAbilityType.Any, Passive.AbilityValue.ManaCost);
		if (!this.HasPassiveMod(abilityPassive, true))
		{
			return Cost;
		}
		return this.GetPassiveAugments(abilityPassive, true, null).GetModifiedValue(this, props, abilityPassive, Cost);
	}

	// Token: 0x060004DE RID: 1246 RVA: 0x000246F4 File Offset: 0x000228F4
	public float ModifyCooldownPassive(EffectProperties props, float BaseCD)
	{
		AbilityPassive abilityPassive = new ValueTuple<PlayerAbilityType, Passive.AbilityValue>((props != null) ? props.AbilityType : PlayerAbilityType.Any, Passive.AbilityValue.Cooldown);
		if (!this.HasPassiveMod(abilityPassive, true))
		{
			return BaseCD;
		}
		return this.GetPassiveAugments(abilityPassive, true, null).GetModifiedValue(this, props, abilityPassive, BaseCD);
	}

	// Token: 0x060004DF RID: 1247 RVA: 0x00024737 File Offset: 0x00022937
	public virtual EffectProperties DamageDone(DamageInfo info)
	{
		Action<DamageInfo> onDamageDone = this.OnDamageDone;
		if (onDamageDone != null)
		{
			onDamageDone(info);
		}
		return this.TriggerDamageDoneSnippets(info);
	}

	// Token: 0x060004E0 RID: 1248 RVA: 0x00024754 File Offset: 0x00022954
	public virtual void HealingDone(DamageInfo info)
	{
		if (this.health.isDead || !this.CanTriggerSnippets(EventTrigger.HealProvided, true, info.SnippetChance))
		{
			return;
		}
		EffectProperties props = info.GenerateEffectProperties();
		this.TriggerSnippets(EventTrigger.HealProvided, props, info.SnippetChance);
	}

	// Token: 0x060004E1 RID: 1249 RVA: 0x00024798 File Offset: 0x00022998
	private EffectProperties TriggerDamageDoneSnippets(DamageInfo info)
	{
		if (this.health.isDead)
		{
			return null;
		}
		EffectProperties effectProperties = info.GenerateEffectProperties();
		if (info.DamageType == DNumType.Crit && this.CanTriggerSnippets(EventTrigger.CriticalDone, true, info.SnippetChance))
		{
			this.TriggerSnippets(EventTrigger.CriticalDone, effectProperties.Copy(false), info.SnippetChance);
		}
		if (this.CanTriggerSnippets(EventTrigger.DamageDone, true, info.SnippetChance))
		{
			this.TriggerSnippets(EventTrigger.DamageDone, effectProperties.Copy(false), info.SnippetChance);
		}
		return effectProperties;
	}

	// Token: 0x060004E2 RID: 1250 RVA: 0x00024810 File Offset: 0x00022A10
	public virtual void OnDie(DamageInfo dmg)
	{
		EffectProperties effectProperties = new EffectProperties(this);
		if (dmg != null)
		{
			EffectProperties effectProperties2 = effectProperties;
			EntityControl entity = EntityControl.GetEntity(dmg.SourceID);
			effectProperties2.SeekTarget = ((entity != null) ? entity.gameObject : null);
		}
		effectProperties.Affected = base.gameObject;
		this.TriggerSnippets(EventTrigger.Died, effectProperties, 1f);
		this.ResetAbilities();
		this.RemoveAllStatuses(true);
	}

	// Token: 0x060004E3 RID: 1251 RVA: 0x0002486A File Offset: 0x00022A6A
	public virtual void OnRevive(int health)
	{
		this.ResetAbilities();
	}

	// Token: 0x060004E4 RID: 1252 RVA: 0x00024872 File Offset: 0x00022A72
	public virtual void OnShieldDepleted(DamageInfo dmg)
	{
	}

	// Token: 0x060004E5 RID: 1253 RVA: 0x00024874 File Offset: 0x00022A74
	public virtual void OnDamageTaken(DamageInfo dmg)
	{
	}

	// Token: 0x060004E6 RID: 1254 RVA: 0x00024878 File Offset: 0x00022A78
	public virtual void OnKilledEntity(EntityControl control)
	{
		this.TriggerSnippets(EventTrigger.KilledEntity, new EffectProperties(this)
		{
			Affected = ((control != null) ? control.gameObject : null),
			SeekTarget = ((control != null) ? control.gameObject : null)
		}, 1f);
	}

	// Token: 0x060004E7 RID: 1255 RVA: 0x000248BE File Offset: 0x00022ABE
	internal virtual void OnDestroy()
	{
		EntityControl.AllEntities.Remove(this);
		EntityControl.ResortAllEntities();
		AIManager.Enemies.Remove(this);
	}

	// Token: 0x060004E8 RID: 1256 RVA: 0x000248DD File Offset: 0x00022ADD
	internal bool InPlayMode()
	{
		return Application.isPlaying;
	}

	// Token: 0x060004E9 RID: 1257 RVA: 0x000248E4 File Offset: 0x00022AE4
	private void Destroy()
	{
		this.net.Destroy();
	}

	// Token: 0x060004EA RID: 1258 RVA: 0x000248F4 File Offset: 0x00022AF4
	public static EntityControl GetEntity(int ViewID)
	{
		if (ViewID < 0)
		{
			return null;
		}
		foreach (EntityControl entityControl in EntityControl.AllEntities)
		{
			if (entityControl.net.view.ViewID == ViewID)
			{
				return entityControl;
			}
		}
		return null;
	}

	// Token: 0x060004EB RID: 1259 RVA: 0x00024960 File Offset: 0x00022B60
	public static EntityControl GetNearestEntity(Vector3 position)
	{
		float num = float.MaxValue;
		EntityControl result = null;
		foreach (EntityControl entityControl in EntityControl.AllEntities)
		{
			if (entityControl.Targetable)
			{
				float num2 = Vector3.Distance(entityControl.movement.GetPosition(), position);
				if (num2 < 5f && num2 < num)
				{
					result = entityControl;
					num = num2;
				}
			}
		}
		return result;
	}

	// Token: 0x060004EC RID: 1260 RVA: 0x000249E4 File Offset: 0x00022BE4
	public static void ClearForSceneChange()
	{
		if (!PhotonNetwork.InRoom)
		{
			return;
		}
		for (int i = EntityControl.AllEntities.Count - 1; i >= 0; i--)
		{
			EntityControl entityControl = EntityControl.AllEntities[i];
			if (entityControl is AIControl && entityControl.IsMine)
			{
				PhotonNetwork.Destroy(entityControl.view);
			}
		}
	}

	// Token: 0x060004ED RID: 1261 RVA: 0x00024A37 File Offset: 0x00022C37
	public static void ResortAllEntities()
	{
		if (EntityControl.AllEntities != null)
		{
			EntityControl.AllEntities.Sort(delegate(EntityControl x, EntityControl y)
			{
				int? num;
				if (x == null)
				{
					num = null;
				}
				else
				{
					EntityNetworked entityNetworked = x.net;
					if (entityNetworked == null)
					{
						num = null;
					}
					else
					{
						PhotonView view = entityNetworked.view;
						num = ((view != null) ? new int?(view.ViewID) : null);
					}
				}
				int num2 = num ?? -1;
				int? num3;
				if (y == null)
				{
					num3 = null;
				}
				else
				{
					EntityNetworked entityNetworked2 = y.net;
					if (entityNetworked2 == null)
					{
						num3 = null;
					}
					else
					{
						PhotonView view2 = entityNetworked2.view;
						num3 = ((view2 != null) ? new int?(view2.ViewID) : null);
					}
				}
				return num2.CompareTo(num3 ?? -1);
			});
		}
	}

	// Token: 0x060004EE RID: 1262 RVA: 0x00024A6C File Offset: 0x00022C6C
	public EntityControl()
	{
	}

	// Token: 0x040003CA RID: 970
	public int TeamID = -1;

	// Token: 0x040003CB RID: 971
	public bool Targetable = true;

	// Token: 0x040003CC RID: 972
	public bool Affectable = true;

	// Token: 0x040003CD RID: 973
	[NonSerialized]
	public EntityHealth health;

	// Token: 0x040003CE RID: 974
	[NonSerialized]
	public EntityMovement movement;

	// Token: 0x040003CF RID: 975
	[NonSerialized]
	public EntityNetworked net;

	// Token: 0x040003D0 RID: 976
	[NonSerialized]
	public EntityDisplay display;

	// Token: 0x040003D1 RID: 977
	[NonSerialized]
	public EntityAudio audio;

	// Token: 0x040003D2 RID: 978
	public bool DebugLines;

	// Token: 0x040003D3 RID: 979
	internal bool didSetup;

	// Token: 0x040003D4 RID: 980
	[NonSerialized]
	public bool Initialized;

	// Token: 0x040003D5 RID: 981
	[CompilerGenerated]
	private EntityControl <currentTarget>k__BackingField;

	// Token: 0x040003D6 RID: 982
	[CompilerGenerated]
	private EntityControl <allyTarget>k__BackingField;

	// Token: 0x040003D7 RID: 983
	public Augments Augment = new Augments();

	// Token: 0x040003D8 RID: 984
	[NonSerialized]
	public List<ActionEffect> OwnedEffects = new List<ActionEffect>();

	// Token: 0x040003D9 RID: 985
	[NonSerialized]
	public List<Ability> Abilities = new List<Ability>();

	// Token: 0x040003DA RID: 986
	[NonSerialized]
	public List<EntityControl.AppliedStatus> Statuses = new List<EntityControl.AppliedStatus>();

	// Token: 0x040003DB RID: 987
	[TupleElementNames(new string[]
	{
		"id",
		"source",
		"duration",
		"stacks",
		"depth"
	})]
	private List<ValueTuple<int, int, float, int, int>> ToApplyStatuses = new List<ValueTuple<int, int, float, int, int>>();

	// Token: 0x040003DC RID: 988
	[TupleElementNames(new string[]
	{
		"id",
		"source",
		"stacks"
	})]
	private List<ValueTuple<int, int, int>> ToRemoveStatuses = new List<ValueTuple<int, int, int>>();

	// Token: 0x040003DD RID: 989
	internal Dictionary<int, float> AbilityCasts = new Dictionary<int, float>();

	// Token: 0x040003DE RID: 990
	[NonSerialized]
	public float TimeAlive;

	// Token: 0x040003DF RID: 991
	private HashSet<TimeSince> TimerKeys = new HashSet<TimeSince>();

	// Token: 0x040003E0 RID: 992
	private Dictionary<TimeSince, float> Timers = new Dictionary<TimeSince, float>();

	// Token: 0x040003E1 RID: 993
	public string DebugAugment;

	// Token: 0x040003E2 RID: 994
	internal Vector3 LastDamageTakenPoint;

	// Token: 0x040003E3 RID: 995
	internal float LastDamageTaken;

	// Token: 0x040003E4 RID: 996
	public static List<EntityControl> AllEntities;

	// Token: 0x040003E5 RID: 997
	[NonSerialized]
	private EffectProperties internalSelfProps;

	// Token: 0x040003E6 RID: 998
	internal Dictionary<int, Augments> PassiveCache = new Dictionary<int, Augments>();

	// Token: 0x040003E7 RID: 999
	internal Augments modCache;

	// Token: 0x040003E8 RID: 1000
	internal HashSet<ModTag> tagsCache = new HashSet<ModTag>();

	// Token: 0x040003E9 RID: 1001
	public Action<DamageInfo> OnDamageDone;

	// Token: 0x02000496 RID: 1174
	[Serializable]
	public class AppliedStatus
	{
		// Token: 0x17000265 RID: 613
		// (get) Token: 0x060021FA RID: 8698 RVA: 0x000C4AA1 File Offset: 0x000C2CA1
		public int Stacks
		{
			get
			{
				if (!this.rootNode.UniqueStackDurations)
				{
					return this._simpleStackCount;
				}
				return this.StackValues.Count;
			}
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x060021FB RID: 8699 RVA: 0x000C4AC4 File Offset: 0x000C2CC4
		public float Duration
		{
			get
			{
				float duration = 0f;
				this.StackValues.ForEach(delegate(float v)
				{
					duration = Mathf.Max(v, duration);
				});
				return duration;
			}
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x060021FC RID: 8700 RVA: 0x000C4AFF File Offset: 0x000C2CFF
		// (set) Token: 0x060021FD RID: 8701 RVA: 0x000C4B07 File Offset: 0x000C2D07
		public float baseDuration
		{
			[CompilerGenerated]
			get
			{
				return this.<baseDuration>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<baseDuration>k__BackingField = value;
			}
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x060021FE RID: 8702 RVA: 0x000C4B10 File Offset: 0x000C2D10
		public float CurrentTickTime
		{
			get
			{
				return this.t;
			}
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x060021FF RID: 8703 RVA: 0x000C4B18 File Offset: 0x000C2D18
		public EntityControl Source
		{
			get
			{
				if (this.source == null && this.sourceID >= 0)
				{
					this.source = EntityControl.GetEntity(this.sourceID);
				}
				return this.source;
			}
		}

		// Token: 0x06002200 RID: 8704 RVA: 0x000C4B48 File Offset: 0x000C2D48
		public void OnApply(EntityControl c)
		{
			this.t = this.rootNode.tickRate;
			if (this.rootNode.OnApply.Count > 0)
			{
				this.rootNode.Apply(this.GetProperties(c));
			}
			foreach (StatusOverrideNode statusOverrideNode in this.Overrides)
			{
				foreach (Node node in statusOverrideNode.OnApply)
				{
					((EffectNode)node).Invoke(this.GetProperties(c));
				}
			}
		}

		// Token: 0x06002201 RID: 8705 RVA: 0x000C4C14 File Offset: 0x000C2E14
		public void SetupOverrides(Augments mods)
		{
			this.MaxStacks = this.rootNode.MaxStacks;
			this.CanStack = this.rootNode.CanStack;
			foreach (StatusKeyword item in this.rootNode.Keywords)
			{
				this.Keywords.Add(item);
			}
			if (mods == null)
			{
				return;
			}
			List<ModOverrideNode> list = new List<ModOverrideNode>();
			mods.OverrideNodeEffects(null, this.rootNode, ref list);
			foreach (ModOverrideNode modOverrideNode in list)
			{
				StatusOverrideNode item2 = (StatusOverrideNode)modOverrideNode;
				this.Overrides.Add(item2);
			}
			foreach (StatusOverrideNode statusOverrideNode in this.Overrides)
			{
				if (statusOverrideNode.OverrideStacks)
				{
					this.CanStack = statusOverrideNode.CanStack;
					this.MaxStacks = (statusOverrideNode.AdditiveStacks ? (this.MaxStacks + statusOverrideNode.MaxStacks) : statusOverrideNode.MaxStacks);
				}
				foreach (StatusKeyword item3 in statusOverrideNode.AddKeywords)
				{
					this.Keywords.Add(item3);
				}
				foreach (StatusKeyword item4 in statusOverrideNode.RemoveKeywords)
				{
					this.Keywords.Remove(item4);
				}
				foreach (StatusRootNode.StatusAugment item5 in statusOverrideNode.AddAugments)
				{
					this.OverrideAugments.Add(item5);
				}
			}
		}

		// Token: 0x06002202 RID: 8706 RVA: 0x000C4EA0 File Offset: 0x000C30A0
		public void RemoveStacks(int amount, EntityControl c)
		{
			if (amount <= 0)
			{
				return;
			}
			int num;
			if (this.rootNode.UniqueStackDurations)
			{
				if (this.StackValues.Count <= 0)
				{
					return;
				}
				this.StackValues.Sort((float x, float y) => x.CompareTo(y));
				this.StackValues.Sort((float x, float y) => Mathf.Sign(x).CompareTo(Mathf.Sign(y)));
				num = Mathf.Min(amount, this.StackValues.Count);
				for (int i = 0; i < num; i++)
				{
					this.StackValues.RemoveAt(0);
				}
			}
			else
			{
				if (this._simpleStackCount <= 0)
				{
					return;
				}
				num = Mathf.Min(amount, this._simpleStackCount);
				this._simpleStackCount -= num;
				if (this._simpleStackCount <= 0)
				{
					this.StackValues.Clear();
				}
			}
			this.StackChanged(c, true, num);
		}

		// Token: 0x06002203 RID: 8707 RVA: 0x000C4F98 File Offset: 0x000C3198
		public void Add(float duration, int stacks, int depth, EntityControl c)
		{
			bool flag = this.StackValues.Count == 0;
			int num = this.CanStack ? this.MaxStacks : 1;
			int num2 = Mathf.Min(stacks, num - this.Stacks);
			int num3 = stacks - num2;
			this.baseDuration = Mathf.Max(this.Duration, duration);
			if (!this.rootNode.UniqueStackDurations)
			{
				this._simpleStackCount += num2;
				if (this.StackValues.Count <= 0)
				{
					this.StackValues.Add(duration);
				}
				this.StackValues[0] = Mathf.Max(this.StackValues[0], duration);
			}
			else
			{
				for (int i = 0; i < num2; i++)
				{
					this.StackValues.Add(duration);
				}
				int num4 = 0;
				for (int j = 0; j < this.StackValues.Count; j++)
				{
					if (this.StackValues[j] >= -1f && this.StackValues[j] <= duration)
					{
						this.StackValues[j] = duration;
						num4++;
						if (num4 >= num3)
						{
							break;
						}
					}
				}
			}
			if (c.CanTriggerSnippets(EventTrigger.RecievedStatus, true, 1f))
			{
				c.TriggerSnippets(EventTrigger.RecievedStatus, new EffectProperties(c)
				{
					SourceControl = this.Source,
					Affected = c.gameObject,
					GraphIDRef = this.rootNode.guid
				}, 1f);
			}
			if (!flag && num2 > 0)
			{
				this.StackChanged(c, false, num2);
			}
		}

		// Token: 0x06002204 RID: 8708 RVA: 0x000C5120 File Offset: 0x000C3320
		public void UpdateTick(EntityControl c)
		{
			float deltaTime = GameplayManager.deltaTime;
			if (this.expires && this.rootNode.TimeBehaviour == StatusRootNode.TimeoutBehaviour.Expire)
			{
				bool flag = false;
				for (int i = this.StackValues.Count - 1; i >= 0; i--)
				{
					if (this.StackValues[i] >= -1f)
					{
						List<float> stackValues = this.StackValues;
						int index = i;
						stackValues[index] -= deltaTime;
						if (this.StackValues[i] <= 0f)
						{
							this.StackValues.RemoveAt(i);
							if (this.rootNode.UniqueStackDurations)
							{
								this.deltaChange++;
							}
							else
							{
								this.deltaChange += this._simpleStackCount;
							}
							flag = true;
						}
					}
				}
				if (flag)
				{
					this.StackChanged(c, true, this.deltaChange);
				}
			}
			this.t -= deltaTime;
			this.Lifetime += deltaTime;
			if (this.t <= 0f)
			{
				this.t = this.rootNode.tickRate;
				if (this.rootNode.OnTick.Count > 0)
				{
					this.rootNode.TickEvent(this.GetProperties(c));
					foreach (StatusOverrideNode statusOverrideNode in this.Overrides)
					{
						foreach (Node node in statusOverrideNode.OnTick)
						{
							((EffectNode)node).Invoke(this.GetProperties(c));
						}
					}
				}
				if (this.rootNode.TimeBehaviour == StatusRootNode.TimeoutBehaviour.DecrementStack)
				{
					this.RemoveStacks(1, c);
				}
			}
		}

		// Token: 0x06002205 RID: 8709 RVA: 0x000C5304 File Offset: 0x000C3504
		public void OnExpire(EntityControl c, bool fromDeath, int prevStacks = 0)
		{
			try
			{
				bool flag = c.CanTriggerSnippets(EventTrigger.LostStatus, true, 1f);
				bool flag2 = this.rootNode.OnExpire.Count > 0 || (this.rootNode.OnDiedWith.Count > 0 && fromDeath);
				if (!flag2 && this.Overrides.Count > 0)
				{
					foreach (StatusOverrideNode statusOverrideNode in this.Overrides)
					{
						if (statusOverrideNode.OnDiedWith.Count > 0 || statusOverrideNode.OnExpire.Count > 0)
						{
							flag2 = true;
						}
					}
				}
				if (flag2 || flag)
				{
					EffectProperties properties = this.GetProperties(c);
					if (prevStacks != 0)
					{
						properties.SetExtra(EProp.Stacks, (float)prevStacks);
					}
					properties.GraphIDRef = this.rootNode.guid;
					foreach (StatusOverrideNode statusOverrideNode2 in this.Overrides)
					{
						if (fromDeath)
						{
							using (List<Node>.Enumerator enumerator2 = statusOverrideNode2.OnDiedWith.GetEnumerator())
							{
								while (enumerator2.MoveNext())
								{
									Node node = enumerator2.Current;
									((EffectNode)node).Invoke(properties.Copy(false));
								}
								continue;
							}
						}
						foreach (Node node2 in statusOverrideNode2.OnExpire)
						{
							((EffectNode)node2).Invoke(properties.Copy(false));
						}
					}
					this.rootNode.Expire(flag ? properties.Copy(false) : properties, fromDeath);
					if (flag)
					{
						c.TriggerSnippets(EventTrigger.LostStatus, properties, 1f);
					}
				}
			}
			catch (Exception message)
			{
				UnityEngine.Debug.LogError(message);
			}
		}

		// Token: 0x06002206 RID: 8710 RVA: 0x000C5548 File Offset: 0x000C3748
		public void StackChanged(EntityControl c, bool wasRemoval, int amount)
		{
			if (this.rootNode.OnStackChanged.Count == 0)
			{
				return;
			}
			EffectProperties properties = this.GetProperties(c);
			properties.SetExtra(EProp.Snip_Input, (float)(wasRemoval ? (-(float)amount) : amount));
			foreach (StatusOverrideNode statusOverrideNode in this.Overrides)
			{
				foreach (Node node in statusOverrideNode.OnStackChanged)
				{
					((EffectNode)node).Invoke(properties.Copy(false));
				}
			}
			this.rootNode.StackChanged(properties);
		}

		// Token: 0x06002207 RID: 8711 RVA: 0x000C5618 File Offset: 0x000C3818
		public void ApplySnippets(EntityControl control, EventTrigger trigger, EffectProperties props = null, float chanceMult = 1f)
		{
			if (props == null)
			{
				props = this.GetProperties(control);
			}
			else if (props.SourceControl != EntityControl.GetEntity(this.sourceID))
			{
				props = props.Copy(false);
				props.SourceControl = EntityControl.GetEntity(this.sourceID);
			}
			this.rootNode.GetSnippetMods(trigger, this.Stacks, this.OverrideAugments).ApplySnippets(control, trigger, props, chanceMult);
		}

		// Token: 0x06002208 RID: 8712 RVA: 0x000C5688 File Offset: 0x000C3888
		public Augments GetMods()
		{
			int stacks = this.Stacks;
			if (this.cachedMods != null && this.cacheModStacks == stacks)
			{
				return this.cachedMods;
			}
			this.cacheModStacks = stacks;
			this.cachedMods = this.rootNode.GetModifiers(stacks, this.OverrideAugments);
			return this.cachedMods;
		}

		// Token: 0x06002209 RID: 8713 RVA: 0x000C56D9 File Offset: 0x000C38D9
		public bool HasSnippet(EventTrigger t)
		{
			return this.rootNode.HasSnippet(t, this.OverrideAugments);
		}

		// Token: 0x0600220A RID: 8714 RVA: 0x000C56ED File Offset: 0x000C38ED
		public bool HasPassive(Passive p)
		{
			return this.rootNode.HasPassive(p, this.OverrideAugments);
		}

		// Token: 0x0600220B RID: 8715 RVA: 0x000C5704 File Offset: 0x000C3904
		public EffectProperties GetProperties(EntityControl c)
		{
			EntityControl entity = EntityControl.GetEntity(this.sourceID);
			EffectProperties effectProperties = new EffectProperties(entity);
			if (c != null)
			{
				effectProperties.Affected = c.gameObject;
			}
			else
			{
				effectProperties.Affected = null;
			}
			if (entity == null)
			{
				effectProperties.SourceTeam = -1;
			}
			effectProperties.EffectSource = EffectSource.StatusEffect;
			effectProperties.StartLoc = (effectProperties.OutLoc = global::Pose.FromEntity(c));
			effectProperties.Keyword = this.rootNode.PlayerKeyword;
			effectProperties.Depth = this.Depth;
			effectProperties.CauseName = this.rootNode.EffectName;
			effectProperties.CauseID = this.rootNode.guid;
			effectProperties.SetExtra(EProp.Stacks, (float)this.Stacks);
			effectProperties.Lifetime = this.Lifetime;
			return effectProperties;
		}

		// Token: 0x0600220C RID: 8716 RVA: 0x000C57C6 File Offset: 0x000C39C6
		public AppliedStatus()
		{
		}

		// Token: 0x04002342 RID: 9026
		[HideInInspector]
		[NonSerialized]
		public StatusRootNode rootNode;

		// Token: 0x04002343 RID: 9027
		public int HashCode;

		// Token: 0x04002344 RID: 9028
		public int sourceID;

		// Token: 0x04002345 RID: 9029
		[NonSerialized]
		public bool CanStack;

		// Token: 0x04002346 RID: 9030
		[NonSerialized]
		public int MaxStacks;

		// Token: 0x04002347 RID: 9031
		public HashSet<StatusKeyword> Keywords = new HashSet<StatusKeyword>();

		// Token: 0x04002348 RID: 9032
		public string StatusName;

		// Token: 0x04002349 RID: 9033
		private bool UniqueStackDurations;

		// Token: 0x0400234A RID: 9034
		public int Depth;

		// Token: 0x0400234B RID: 9035
		private List<float> StackValues = new List<float>();

		// Token: 0x0400234C RID: 9036
		private int _simpleStackCount;

		// Token: 0x0400234D RID: 9037
		private Augments cachedMods;

		// Token: 0x0400234E RID: 9038
		private int cacheModStacks;

		// Token: 0x0400234F RID: 9039
		public float Lifetime;

		// Token: 0x04002350 RID: 9040
		[CompilerGenerated]
		private float <baseDuration>k__BackingField;

		// Token: 0x04002351 RID: 9041
		public bool expires;

		// Token: 0x04002352 RID: 9042
		public List<StatusOverrideNode> Overrides = new List<StatusOverrideNode>();

		// Token: 0x04002353 RID: 9043
		public List<StatusRootNode.StatusAugment> OverrideAugments = new List<StatusRootNode.StatusAugment>();

		// Token: 0x04002354 RID: 9044
		private EntityControl source;

		// Token: 0x04002355 RID: 9045
		private float t;

		// Token: 0x04002356 RID: 9046
		private int deltaChange;

		// Token: 0x020006BD RID: 1725
		[CompilerGenerated]
		private sealed class <>c__DisplayClass12_0
		{
			// Token: 0x06002859 RID: 10329 RVA: 0x000D8715 File Offset: 0x000D6915
			public <>c__DisplayClass12_0()
			{
			}

			// Token: 0x0600285A RID: 10330 RVA: 0x000D871D File Offset: 0x000D691D
			internal void <get_Duration>b__0(float v)
			{
				this.duration = Mathf.Max(v, this.duration);
			}

			// Token: 0x04002CD4 RID: 11476
			public float duration;
		}

		// Token: 0x020006BE RID: 1726
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x0600285B RID: 10331 RVA: 0x000D8731 File Offset: 0x000D6931
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x0600285C RID: 10332 RVA: 0x000D873D File Offset: 0x000D693D
			public <>c()
			{
			}

			// Token: 0x0600285D RID: 10333 RVA: 0x000D8745 File Offset: 0x000D6945
			internal int <RemoveStacks>b__32_0(float x, float y)
			{
				return x.CompareTo(y);
			}

			// Token: 0x0600285E RID: 10334 RVA: 0x000D8750 File Offset: 0x000D6950
			internal int <RemoveStacks>b__32_1(float x, float y)
			{
				return Mathf.Sign(x).CompareTo(Mathf.Sign(y));
			}

			// Token: 0x04002CD5 RID: 11477
			public static readonly EntityControl.AppliedStatus.<>c <>9 = new EntityControl.AppliedStatus.<>c();

			// Token: 0x04002CD6 RID: 11478
			public static Comparison<float> <>9__32_0;

			// Token: 0x04002CD7 RID: 11479
			public static Comparison<float> <>9__32_1;
		}
	}

	// Token: 0x02000497 RID: 1175
	[CompilerGenerated]
	private sealed class <Start>d__45 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x0600220D RID: 8717 RVA: 0x000C57FA File Offset: 0x000C39FA
		[DebuggerHidden]
		public <Start>d__45(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x0600220E RID: 8718 RVA: 0x000C5809 File Offset: 0x000C3A09
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x0600220F RID: 8719 RVA: 0x000C580C File Offset: 0x000C3A0C
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			EntityControl entityControl = this;
			if (num == 0)
			{
				this.<>1__state = -1;
				entityControl.Setup();
				entityControl.internalSelfProps = new EffectProperties(entityControl);
				this.<>2__current = true;
				this.<>1__state = 1;
				return true;
			}
			if (num != 1)
			{
				return false;
			}
			this.<>1__state = -1;
			entityControl.Initialized = true;
			entityControl.OnSpawned();
			return false;
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06002210 RID: 8720 RVA: 0x000C5873 File Offset: 0x000C3A73
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x06002211 RID: 8721 RVA: 0x000C587B File Offset: 0x000C3A7B
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06002212 RID: 8722 RVA: 0x000C5882 File Offset: 0x000C3A82
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x04002357 RID: 9047
		private int <>1__state;

		// Token: 0x04002358 RID: 9048
		private object <>2__current;

		// Token: 0x04002359 RID: 9049
		public EntityControl <>4__this;
	}

	// Token: 0x02000498 RID: 1176
	[CompilerGenerated]
	[Serializable]
	private sealed class <>c
	{
		// Token: 0x06002213 RID: 8723 RVA: 0x000C588A File Offset: 0x000C3A8A
		// Note: this type is marked as 'beforefieldinit'.
		static <>c()
		{
		}

		// Token: 0x06002214 RID: 8724 RVA: 0x000C5896 File Offset: 0x000C3A96
		public <>c()
		{
		}

		// Token: 0x06002215 RID: 8725 RVA: 0x000C58A0 File Offset: 0x000C3AA0
		internal int <ResortAllEntities>b__135_0(EntityControl x, EntityControl y)
		{
			int? num;
			if (x == null)
			{
				num = null;
			}
			else
			{
				EntityNetworked net = x.net;
				if (net == null)
				{
					num = null;
				}
				else
				{
					PhotonView view = net.view;
					num = ((view != null) ? new int?(view.ViewID) : null);
				}
			}
			int num2 = num ?? -1;
			int? num3;
			if (y == null)
			{
				num3 = null;
			}
			else
			{
				EntityNetworked net2 = y.net;
				if (net2 == null)
				{
					num3 = null;
				}
				else
				{
					PhotonView view2 = net2.view;
					num3 = ((view2 != null) ? new int?(view2.ViewID) : null);
				}
			}
			return num2.CompareTo(num3 ?? -1);
		}

		// Token: 0x0400235A RID: 9050
		public static readonly EntityControl.<>c <>9 = new EntityControl.<>c();

		// Token: 0x0400235B RID: 9051
		public static Comparison<EntityControl> <>9__135_0;
	}
}
