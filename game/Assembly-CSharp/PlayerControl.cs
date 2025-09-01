using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using EZCameraShake;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

// Token: 0x0200008C RID: 140
public class PlayerControl : EntityControl
{
	// Token: 0x1700007E RID: 126
	// (get) Token: 0x060005E9 RID: 1513 RVA: 0x0002BACD File Offset: 0x00029CCD
	public static Camera MyCamera
	{
		get
		{
			PlayerCamera playerCamera = PlayerCamera.myInstance;
			if (playerCamera == null)
			{
				return null;
			}
			return playerCamera.Cam;
		}
	}

	// Token: 0x1700007F RID: 127
	// (get) Token: 0x060005EA RID: 1514 RVA: 0x0002BADF File Offset: 0x00029CDF
	public PlayerMovement Movement
	{
		get
		{
			return this.movement as PlayerMovement;
		}
	}

	// Token: 0x17000080 RID: 128
	// (get) Token: 0x060005EB RID: 1515 RVA: 0x0002BAEC File Offset: 0x00029CEC
	public PlayerNetwork Net
	{
		get
		{
			return this.net as PlayerNetwork;
		}
	}

	// Token: 0x17000081 RID: 129
	// (get) Token: 0x060005EC RID: 1516 RVA: 0x0002BAF9 File Offset: 0x00029CF9
	public PlayerDisplay Display
	{
		get
		{
			return this.display as PlayerDisplay;
		}
	}

	// Token: 0x17000082 RID: 130
	// (get) Token: 0x060005ED RID: 1517 RVA: 0x0002BB06 File Offset: 0x00029D06
	public PlayerAudio Audio
	{
		get
		{
			return this.audio as PlayerAudio;
		}
	}

	// Token: 0x17000083 RID: 131
	// (get) Token: 0x060005EE RID: 1518 RVA: 0x0002BB13 File Offset: 0x00029D13
	public PlayerHealth Health
	{
		get
		{
			return this.health as PlayerHealth;
		}
	}

	// Token: 0x17000084 RID: 132
	// (get) Token: 0x060005EF RID: 1519 RVA: 0x0002BB20 File Offset: 0x00029D20
	// (set) Token: 0x060005F0 RID: 1520 RVA: 0x0002BB3D File Offset: 0x00029D3D
	private EntityControl playerCurrentTarget
	{
		get
		{
			if (this.ctarg != null)
			{
				return this.ctarg;
			}
			return this.lastTarg;
		}
		set
		{
			this.ctarg = value;
			if (this.ctarg != null || !base.IsMine)
			{
				this.lastTarg = this.ctarg;
				this.lastTargetSetTime = Time.realtimeSinceStartup;
			}
		}
	}

	// Token: 0x17000085 RID: 133
	// (get) Token: 0x060005F1 RID: 1521 RVA: 0x0002BB73 File Offset: 0x00029D73
	// (set) Token: 0x060005F2 RID: 1522 RVA: 0x0002BB90 File Offset: 0x00029D90
	private EntityControl playerAllyTarget
	{
		get
		{
			if (!(this.allytarg != null))
			{
				return this.lastAllyTarg;
			}
			return this.allytarg;
		}
		set
		{
			this.allytarg = value;
			if (this.allytarg == null && base.IsMine)
			{
				return;
			}
			this.lastAllyTarg = this.allytarg;
			this.lastAllySetTime = Time.realtimeSinceStartup;
		}
	}

	// Token: 0x17000086 RID: 134
	// (get) Token: 0x060005F3 RID: 1523 RVA: 0x0002BBC7 File Offset: 0x00029DC7
	public EntityControl RealTarget
	{
		get
		{
			return this.ctarg;
		}
	}

	// Token: 0x17000087 RID: 135
	// (get) Token: 0x060005F4 RID: 1524 RVA: 0x0002BBCF File Offset: 0x00029DCF
	public Augments CurQuests
	{
		get
		{
			return Progression.CurrentQuestAugments();
		}
	}

	// Token: 0x17000088 RID: 136
	// (get) Token: 0x060005F5 RID: 1525 RVA: 0x0002BBD8 File Offset: 0x00029DD8
	public MagicColor SignatureColor
	{
		get
		{
			PlayerActions playerActions = this.actions;
			MagicColor? magicColor;
			if (playerActions == null)
			{
				magicColor = null;
			}
			else
			{
				AugmentTree core = playerActions.core;
				magicColor = ((core != null) ? new MagicColor?(core.Root.magicColor) : null);
			}
			MagicColor? magicColor2 = magicColor;
			if (magicColor2 == null)
			{
				return MagicColor.Neutral;
			}
			return magicColor2.GetValueOrDefault();
		}
	}

	// Token: 0x060005F6 RID: 1526 RVA: 0x0002BC30 File Offset: 0x00029E30
	public override void Awake()
	{
		base.Awake();
		this.Input = base.GetComponent<PlayerInput>();
		this.Mana = base.GetComponent<PlayerMana>();
		this.actions = base.GetComponentInChildren<PlayerActions>();
		if (PlayerControl.AllPlayers == null)
		{
			PlayerControl.AllPlayers = new List<PlayerControl>();
		}
		PlayerControl.AllPlayers.Add(this);
		PlayerControl.AllPlayers.Sort((PlayerControl x, PlayerControl y) => x.GetComponent<PhotonView>().ViewID.CompareTo(y.GetComponent<PhotonView>().ViewID));
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	// Token: 0x060005F7 RID: 1527 RVA: 0x0002BCB8 File Offset: 0x00029EB8
	public override void Setup()
	{
		if (base.IsMine)
		{
			PlayerControl.myInstance = this;
		}
		base.Setup();
		this.Mana.Setup();
		this.SetUsername();
		this.SetupRandom(UnityEngine.Random.Range(0, int.MaxValue), 0);
		if (base.IsMine)
		{
			this.Input.isLocalControl = true;
			this.Movement.cameraRoot.SetActive(true);
			PlayerActions playerActions = this.actions;
			playerActions.abilityActivated = (Action<int, Vector3, Vector3, EffectProperties>)Delegate.Combine(playerActions.abilityActivated, new Action<int, Vector3, Vector3, EffectProperties>(this.Net.AbilityActivated));
			PlayerActions playerActions2 = this.actions;
			playerActions2.abilityActivated = (Action<int, Vector3, Vector3, EffectProperties>)Delegate.Combine(playerActions2.abilityActivated, new Action<int, Vector3, Vector3, EffectProperties>(this.PlayerAbilityUseSnippits));
			PlayerActions playerActions3 = this.actions;
			playerActions3.abilityReleased = (Action<int, Vector3, Vector3, EffectProperties>)Delegate.Combine(playerActions3.abilityReleased, new Action<int, Vector3, Vector3, EffectProperties>(this.Net.AbilityReleased));
			PlayerActions playerActions4 = this.actions;
			playerActions4.abilityReleased = (Action<int, Vector3, Vector3, EffectProperties>)Delegate.Combine(playerActions4.abilityReleased, new Action<int, Vector3, Vector3, EffectProperties>(this.PlayerAbilityReleaseSnippits));
			this.UpdateTalents();
			this.ApplyLocalHealthEvents();
			this.Input.TryLibraryIn();
			Action localPlayerSpawned = PlayerControl.LocalPlayerSpawned;
			if (localPlayerSpawned != null)
			{
				localPlayerSpawned();
			}
			this.Net.SyncUserID();
			this.Net.SendExtraData(null);
			this.TriggerSnippets(EventTrigger.Player_MetaSpecialEvent, null, 1f);
			PlayerControl.ToggleMultiplayerCollision(true);
		}
		else
		{
			this.Input.isLocalControl = false;
		}
		EntityHealth health = this.health;
		health.OnDamageTaken = (Action<DamageInfo>)Delegate.Combine(health.OnDamageTaken, new Action<DamageInfo>(delegate(DamageInfo info)
		{
			GameRecord.PlayerDamageTaken(this, info);
		}));
		EntityHealth health2 = this.health;
		health2.OnShieldsDepleted = (Action<DamageInfo>)Delegate.Combine(health2.OnShieldsDepleted, new Action<DamageInfo>(delegate(DamageInfo info)
		{
			GameRecord.PlayerBarrierBroken(this, info);
		}));
		GameplayManager.OnGameStateChanged = (Action<GameState, GameState>)Delegate.Combine(GameplayManager.OnGameStateChanged, new Action<GameState, GameState>(this.GameStateChanged));
		MapManager.OnMapChangeFinished = (Action)Delegate.Combine(MapManager.OnMapChangeFinished, new Action(this.OnMapChangeFinished));
		GameRecord.RecordEvent(GameRecord.EventType.Player_Connected, this, this.movement.GetPosition(), null);
	}

	// Token: 0x060005F8 RID: 1528 RVA: 0x0002BECC File Offset: 0x0002A0CC
	private void SetUsername()
	{
		Player owner = base.view.Owner;
		string text = (owner != null) ? owner.NickName : null;
		Regex regex = new Regex("<[^>]*>");
		if (text != null && regex.IsMatch(text))
		{
			text = regex.Replace(text, string.Empty);
		}
		if (!string.IsNullOrEmpty(text))
		{
			text = string.Concat<char>(from c in text.Normalize(NormalizationForm.FormD)
			where CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark
			select c);
		}
		this.Username = text;
		base.gameObject.name = "Player_" + this.Username;
		MultiplayerUI instance = MultiplayerUI.instance;
		if (instance == null)
		{
			return;
		}
		instance.OnPlayerJoined(this);
	}

	// Token: 0x060005F9 RID: 1529 RVA: 0x0002BF80 File Offset: 0x0002A180
	public string GetUsernameText()
	{
		string text = this.Username;
		if (!string.IsNullOrEmpty(this.UserID) && ParseManager.IsDeveloper(this.UserID))
		{
			text = "<color=#FFBA44>[Dev]</color> " + text;
		}
		if (this.PrestigeLevel > 0)
		{
			text = string.Format("<sprite name=Prestige_{0}>", this.PrestigeLevel - 1) + text;
		}
		return text;
	}

	// Token: 0x060005FA RID: 1530 RVA: 0x0002BFE2 File Offset: 0x0002A1E2
	public void SetUserID(string userID)
	{
		if (!string.IsNullOrEmpty(this.UserID))
		{
			return;
		}
		this.UserID = userID;
		this.IsDeveloper = (!string.IsNullOrEmpty(this.UserID) && ParseManager.IsDeveloper(this.UserID));
	}

	// Token: 0x060005FB RID: 1531 RVA: 0x0002C01C File Offset: 0x0002A21C
	private void ApplyLocalHealthEvents()
	{
		EntityHealth health = this.health;
		health.OnDamageTaken = (Action<DamageInfo>)Delegate.Combine(health.OnDamageTaken, new Action<DamageInfo>(delegate(DamageInfo dmg)
		{
			CameraShaker instance = CameraShaker.Instance;
			if (instance != null)
			{
				instance.ShakeOnce(Mathf.Lerp(0f, 1f, dmg.TotalAmount / 10f), 8f, 0f, 0.5f);
			}
			PostFXManager.DamgageFX(dmg.TotalAmount);
		}));
		EntityHealth health2 = this.health;
		health2.OnHealed = (Action<int>)Delegate.Combine(health2.OnHealed, new Action<int>(delegate(int healed)
		{
			this.Display.HealPulse(healed);
		}));
		EntityHealth health3 = this.health;
		health3.OnDamageTaken = (Action<DamageInfo>)Delegate.Combine(health3.OnDamageTaken, new Action<DamageInfo>(DamageIndicator.DamageTaken));
		EntityHealth health4 = this.health;
		health4.OnDamageTaken = (Action<DamageInfo>)Delegate.Combine(health4.OnDamageTaken, new Action<DamageInfo>(AudioManager.OnPlayerDamage));
		EntityHealth health5 = this.health;
		health5.OnHealed = (Action<int>)Delegate.Combine(health5.OnHealed, new Action<int>(GameHUD.OnHealed));
		EntityHealth health6 = this.health;
		health6.OnDamageTaken = (Action<DamageInfo>)Delegate.Combine(health6.OnDamageTaken, new Action<DamageInfo>(GameHUD.OnDamageTaken));
	}

	// Token: 0x060005FC RID: 1532 RVA: 0x0002C128 File Offset: 0x0002A328
	public void SetupRandom(int seed, int count = 0)
	{
		this.PageSeed = seed;
		this.PageRand = new System.Random(seed);
		for (int i = 0; i < count; i++)
		{
			this.GetRandom(0, 0);
		}
	}

	// Token: 0x060005FD RID: 1533 RVA: 0x0002C160 File Offset: 0x0002A360
	public int GetRandom(int min, int max)
	{
		this.PageSeedCount++;
		int num = this.PageRand.Next(min, max);
		this.pageRolls.Add(new Vector2((float)this.PageSeedCount, (float)num));
		return num;
	}

	// Token: 0x060005FE RID: 1534 RVA: 0x0002C1A4 File Offset: 0x0002A3A4
	public void ActivateAbilityLocal(int actionType, Vector3 origin, Vector3 direction, EffectProperties props)
	{
		props.StartLoc = (props.OutLoc = global::Pose.WorldPoint(origin, direction));
		Ability ability = this.actions.GetAbility((PlayerAbilityType)actionType);
		if (ability != null)
		{
			this.TryActivateAbility(ability.GUID, props);
		}
	}

	// Token: 0x060005FF RID: 1535 RVA: 0x0002C1EC File Offset: 0x0002A3EC
	public void ReleaseAbilityLocal(int actionType, Vector3 origin, Vector3 direction, EffectProperties props)
	{
		Ability ability = this.actions.GetAbility((PlayerAbilityType)actionType);
		if (ability == null)
		{
			return;
		}
		props.StartLoc = (props.OutLoc = global::Pose.WorldPoint(origin, direction));
		this.TryReleaseAbility(ability.GUID, props);
	}

	// Token: 0x06000600 RID: 1536 RVA: 0x0002C234 File Offset: 0x0002A434
	private void PlayerAbilityUseSnippits(int actionTypeID, Vector3 loc, Vector3 dir, EffectProperties props)
	{
		Ability ability = this.actions.GetAbility((PlayerAbilityType)actionTypeID);
		float? num;
		if (ability == null)
		{
			num = null;
		}
		else
		{
			AbilityTree abilityTree = ability.AbilityTree;
			num = ((abilityTree != null) ? new float?(abilityTree.Root.Usage.PageProcChance) : null);
		}
		float num2 = num ?? 1f;
		if (num2 == 0f || !this.CanTriggerSnippets(EventTrigger.AbilityUsed, true, 1f))
		{
			return;
		}
		if (num2 < 1f && props.RandomFloat(0f, 100f) > num2 * 100f)
		{
			return;
		}
		EffectProperties effectProperties = props.Copy(false);
		effectProperties.Affected = base.gameObject;
		effectProperties.AbilityType = (PlayerAbilityType)actionTypeID;
		this.actions.GetAbility((PlayerAbilityType)actionTypeID).TriggerSnippets(EventTrigger.AbilityUsed, this, effectProperties);
	}

	// Token: 0x06000601 RID: 1537 RVA: 0x0002C310 File Offset: 0x0002A510
	public void PlayerAbilityReleaseSnippits(int actionTypeID, Vector3 loc, Vector3 dir, EffectProperties props)
	{
		Ability ability = this.actions.GetAbility((PlayerAbilityType)actionTypeID);
		float? num;
		if (ability == null)
		{
			num = null;
		}
		else
		{
			AbilityTree abilityTree = ability.AbilityTree;
			num = ((abilityTree != null) ? new float?(abilityTree.Root.Usage.PageProcChance) : null);
		}
		float num2 = num ?? 1f;
		if (num2 == 0f || !this.CanTriggerSnippets(EventTrigger.AbilityReleased, true, 1f))
		{
			return;
		}
		if (num2 < 1f && props.RandomFloat(0f, 100f) > num2 * 100f)
		{
			return;
		}
		EffectProperties effectProperties = props.Copy(false);
		effectProperties.Affected = base.gameObject;
		effectProperties.AbilityType = (PlayerAbilityType)actionTypeID;
		this.TriggerSnippets(EventTrigger.AbilityReleased, effectProperties, 1f);
	}

	// Token: 0x06000602 RID: 1538 RVA: 0x0002C3E4 File Offset: 0x0002A5E4
	public override void Update()
	{
		this.UpdateTargeting();
		this.UpdateAllyTargeting();
		base.Update();
		this.changedThisFrame = false;
		if (!base.IsMine)
		{
			this.AimPointSmooth = Vector3.Lerp(this.AimPointSmooth, this.CameraAimPoint, Time.deltaTime * 6f);
			return;
		}
		this.AimPointSmooth = this.CameraAimPoint;
		this.CurMenu = PanelManager.CurPanel;
		base.currentTarget = this.playerCurrentTarget;
		base.allyTarget = this.playerAllyTarget;
		this.FixPositionIfInvalid();
	}

	// Token: 0x06000603 RID: 1539 RVA: 0x0002C46C File Offset: 0x0002A66C
	private void UpdateTargeting()
	{
		if (!base.IsMine || this.IsSpectator)
		{
			return;
		}
		if (PlayerControl.MyCamera == null)
		{
			this.playerCurrentTarget = null;
		}
		EntityControl entityControl = null;
		float num = float.MaxValue;
		RaycastHit raycastHit;
		if (Physics.Raycast(PlayerControl.MyCamera.transform.position, PlayerControl.MyCamera.transform.forward, out raycastHit, 1000f, GameplayManager.instance.EnvironmentMask + GameplayManager.instance.EntityMask))
		{
			num = raycastHit.distance + 5f;
		}
		foreach (EntityControl entityControl2 in EntityControl.AllEntities)
		{
			if (!(entityControl2 is PlayerControl) && !entityControl2.IsDead && entityControl2.TeamID != this.TeamID && entityControl2.Targetable)
			{
				Vector3 vector = entityControl2.display.CenterOfMass.position - PlayerControl.MyCamera.transform.position;
				if (Vector3.Angle(vector.normalized, PlayerControl.MyCamera.transform.forward) <= 5f)
				{
					float magnitude = vector.magnitude;
					if (magnitude < num + entityControl2.display.VFXScaleFactor)
					{
						num = magnitude;
						entityControl = entityControl2;
					}
				}
			}
		}
		if (entityControl != null || AIManager.AliveEnemies == 0)
		{
			this.lastTargetTime = Time.realtimeSinceStartup;
		}
		bool flag = this.CanShowHiglights();
		if (this.lastTarg != null && (this.lastTarg != entityControl || !flag))
		{
			this.lastTarg.display.ReleaseHilight();
		}
		this.playerCurrentTarget = entityControl;
		if (flag && entityControl != null)
		{
			entityControl.display.ApplyHilight();
		}
		if (this.ctarg == null && Time.realtimeSinceStartup - this.lastTargetSetTime > 0.5f)
		{
			this.lastTarg = null;
		}
		this.HelpShowEnemies();
	}

	// Token: 0x06000604 RID: 1540 RVA: 0x0002C678 File Offset: 0x0002A878
	public void ForceClearTarget()
	{
		if (base.currentTarget != null)
		{
			return;
		}
		Debug.Log("Force Clearing target");
		base.currentTarget.display.ReleaseHilight();
		EnemyUIDisplayManager.instance.TryReleaseDisplay(base.currentTarget);
		base.currentTarget = null;
	}

	// Token: 0x06000605 RID: 1541 RVA: 0x0002C6C8 File Offset: 0x0002A8C8
	private void HelpShowEnemies()
	{
		bool flag = this.CanShowHiglights();
		if (this.isHelping)
		{
			if (Time.realtimeSinceStartup - this.lastTargetTime >= 5f && PanelManager.CurPanel == PanelType.GameInvisible)
			{
				return;
			}
			this.isHelping = false;
			foreach (EntityControl entityControl in EntityControl.AllEntities)
			{
				if (!(entityControl is PlayerControl) && !entityControl.IsDead && entityControl.TeamID != this.TeamID && entityControl.Targetable)
				{
					entityControl.display.ReleaseHilight();
				}
			}
			return;
		}
		else
		{
			if (Time.realtimeSinceStartup - this.lastTargetTime < 5f || !flag)
			{
				return;
			}
			this.isHelping = true;
			foreach (EntityControl entityControl2 in EntityControl.AllEntities)
			{
				if (!(entityControl2 is PlayerControl) && !entityControl2.IsDead && entityControl2.TeamID != this.TeamID && entityControl2.Targetable && GameHUD.Mode != GameHUD.HUDMode.Off)
				{
					entityControl2.display.ApplyHilight();
				}
			}
			return;
		}
	}

	// Token: 0x06000606 RID: 1542 RVA: 0x0002C80C File Offset: 0x0002AA0C
	private bool CanShowHiglights()
	{
		return PanelManager.CurPanel == PanelType.GameInvisible && !this.IsSpectator && GameHUD.Mode != GameHUD.HUDMode.Off && (!RaidManager.IsInRaid || RaidManager.IsEncounterStarted);
	}

	// Token: 0x06000607 RID: 1543 RVA: 0x0002C840 File Offset: 0x0002AA40
	public static bool CanSeeEntity(EntityControl e)
	{
		if (PlayerControl.myInstance == null)
		{
			return false;
		}
		if (!Logic_EntityIs.InFrontOf(e, PlayerControl.myInstance))
		{
			return false;
		}
		Vector3 position = PlayerControl.MyCamera.transform.position;
		Vector3 position2 = e.display.CenterOfMass.position;
		Vector3 normalized = (position2 - position).normalized;
		float maxDistance = Vector3.Distance(position, position2) - 2f;
		Debug.DrawLine(position, position2, Color.gray);
		RaycastHit raycastHit;
		return !Physics.Raycast(position, normalized, out raycastHit, maxDistance, GameplayManager.instance.EnvironmentMask + GameplayManager.instance.EntityMask);
	}

	// Token: 0x06000608 RID: 1544 RVA: 0x0002C8E5 File Offset: 0x0002AAE5
	private void UpdateAllyTargeting()
	{
	}

	// Token: 0x06000609 RID: 1545 RVA: 0x0002C8E8 File Offset: 0x0002AAE8
	public override Augments AllAugments(bool includeStatuses = true, Augments cache = null)
	{
		if (this.modCache != null && includeStatuses)
		{
			return this.modCache;
		}
		Augments augments = base.AllAugments(includeStatuses, cache);
		augments.Add(this.BasePlayerAugment, 1);
		if (this.actions.core != null)
		{
			augments.Add(this.actions.core, 1);
		}
		if ((!GameplayManager.IsChallengeActive || !GameplayManager.IsInGame) && !LibraryRaces.IsPlayerRacing)
		{
			augments.Add(this.Talents);
		}
		if (MapManager.InLobbyScene && LibraryManager.instance != null && !LibraryRaces.IsPlayerRacing)
		{
			augments.Add(LibraryManager.instance.LibraryAugments);
		}
		augments.Add(this.Mana.ManaBaseAugments);
		if (GameplayManager.instance != null)
		{
			augments.Add(GameplayManager.instance.GetGameAugments(ModType.Player));
		}
		if (GameplayManager.IsInGame)
		{
			foreach (KeyValuePair<AugmentRootNode, int> keyValuePair in InkManager.PurchasedMods.trees)
			{
				AugmentRootNode augmentRootNode;
				int num;
				keyValuePair.Deconstruct(out augmentRootNode, out num);
				AugmentRootNode augmentRootNode2 = augmentRootNode;
				int count = num;
				if (augmentRootNode2.ApplyToPlayers)
				{
					augments.Add(augmentRootNode2, count);
				}
			}
			if (GameplayManager.instance != null && GameplayManager.instance.GenreBindings != null)
			{
				foreach (KeyValuePair<AugmentRootNode, int> keyValuePair in GameplayManager.instance.GenreBindings.trees)
				{
					AugmentRootNode augmentRootNode;
					int num;
					keyValuePair.Deconstruct(out augmentRootNode, out num);
					AugmentRootNode augmentRootNode3 = augmentRootNode;
					int count2 = num;
					if (augmentRootNode3.ApplyToPlayers)
					{
						augments.Add(augmentRootNode3, count2);
					}
				}
			}
			GameplayManager instance = GameplayManager.instance;
			if (((instance != null) ? instance.PlayerTeamMods : null) != null)
			{
				augments.Add(GameplayManager.instance.PlayerTeamMods);
			}
		}
		return augments;
	}

	// Token: 0x0600060A RID: 1546 RVA: 0x0002CAE0 File Offset: 0x0002ACE0
	public override bool HasPassiveMod(Passive p, bool checkStatuses = true)
	{
		if (base.HasPassiveMod(p, checkStatuses))
		{
			return true;
		}
		if (this.BasePlayerAugment.Root.HasPassive(p))
		{
			return true;
		}
		if (this.actions.core != null && this.actions.core.Root.HasPassive(p))
		{
			return true;
		}
		if ((!GameplayManager.IsChallengeActive || !GameplayManager.IsInGame) && !LibraryRaces.IsPlayerRacing && this.Talents.HasPassive(p))
		{
			return true;
		}
		if (MapManager.InLobbyScene && LibraryManager.instance != null && !LibraryRaces.IsPlayerRacing && LibraryManager.instance.LibraryAugments.HasPassive(p))
		{
			return true;
		}
		if (GameplayManager.instance != null && GameplayManager.instance.GetGameAugments(ModType.Player).HasPassive(p))
		{
			return true;
		}
		if (this.Mana.ManaBaseAugments.HasPassive(p))
		{
			return true;
		}
		if (GameplayManager.IsInGame)
		{
			foreach (KeyValuePair<AugmentRootNode, int> keyValuePair in InkManager.PurchasedMods.trees)
			{
				AugmentRootNode augmentRootNode;
				int num;
				keyValuePair.Deconstruct(out augmentRootNode, out num);
				if (augmentRootNode.HasPassive(p))
				{
					return true;
				}
			}
			if (GameplayManager.instance != null && GameplayManager.instance.GenreBindings != null && GameplayManager.instance.GenreBindings.HasPassive(p))
			{
				return true;
			}
			GameplayManager instance = GameplayManager.instance;
			if (((instance != null) ? instance.PlayerTeamMods : null) != null && GameplayManager.instance.PlayerTeamMods.HasPassive(p))
			{
				return true;
			}
			return false;
		}
		return false;
	}

	// Token: 0x0600060B RID: 1547 RVA: 0x0002CC84 File Offset: 0x0002AE84
	public override Augments GetPassiveAugments(Passive p, bool canUseCache = false, Augments cache = null)
	{
		Augments passiveAugments = base.GetPassiveAugments(p, false, cache);
		passiveAugments.Add(this.BasePlayerAugment, 1);
		if (this.actions.core != null)
		{
			passiveAugments.Add(this.actions.core, 1);
		}
		if ((!GameplayManager.IsChallengeActive || !GameplayManager.IsInGame) && !LibraryRaces.IsPlayerRacing)
		{
			this.Talents.GetPassiveAugments(p, ref passiveAugments);
		}
		if (MapManager.InLobbyScene && LibraryManager.instance != null && !LibraryRaces.IsPlayerRacing)
		{
			LibraryManager.instance.LibraryAugments.GetPassiveAugments(p, ref passiveAugments);
		}
		passiveAugments.Add(this.Mana.ManaBaseAugments);
		if (GameplayManager.instance != null)
		{
			GameplayManager.instance.GetGameAugments(ModType.Player).GetPassiveAugments(p, ref passiveAugments);
		}
		if (GameplayManager.IsInGame)
		{
			foreach (KeyValuePair<AugmentRootNode, int> keyValuePair in InkManager.PurchasedMods.trees)
			{
				AugmentRootNode augmentRootNode;
				int num;
				keyValuePair.Deconstruct(out augmentRootNode, out num);
				AugmentRootNode augmentRootNode2 = augmentRootNode;
				int count = num;
				if (augmentRootNode2.ApplyToPlayers && augmentRootNode2.HasPassive(p))
				{
					passiveAugments.Add(augmentRootNode2, count);
				}
			}
			if (GameplayManager.instance != null && GameplayManager.instance.GenreBindings != null)
			{
				foreach (KeyValuePair<AugmentRootNode, int> keyValuePair in GameplayManager.instance.GenreBindings.trees)
				{
					AugmentRootNode augmentRootNode;
					int num;
					keyValuePair.Deconstruct(out augmentRootNode, out num);
					AugmentRootNode augmentRootNode3 = augmentRootNode;
					int count2 = num;
					if (augmentRootNode3.ApplyToPlayers && augmentRootNode3.HasPassive(p))
					{
						passiveAugments.Add(augmentRootNode3, count2);
					}
				}
			}
			GameplayManager instance = GameplayManager.instance;
			if (((instance != null) ? instance.PlayerTeamMods : null) != null)
			{
				GameplayManager.instance.PlayerTeamMods.GetPassiveAugments(p, ref passiveAugments);
			}
		}
		return passiveAugments;
	}

	// Token: 0x0600060C RID: 1548 RVA: 0x0002CE84 File Offset: 0x0002B084
	public override bool CanTriggerSnippets(EventTrigger trigger, bool includeStatuses = true, float chanceMult = 1f)
	{
		if (base.CanTriggerSnippets(trigger, includeStatuses, chanceMult))
		{
			return true;
		}
		if (this.BasePlayerAugment.Root.SnippetMatches.Contains(trigger))
		{
			return true;
		}
		if (this.actions.core != null && this.actions.core.Root.SnippetMatches.Contains(trigger))
		{
			return true;
		}
		if ((!GameplayManager.IsChallengeActive || !GameplayManager.IsInGame) && !LibraryRaces.IsPlayerRacing && this.Talents.HasSnippet(trigger))
		{
			return true;
		}
		if (MapManager.InLobbyScene && LibraryManager.instance != null && !LibraryRaces.IsPlayerRacing && LibraryManager.instance.LibraryAugments.HasSnippet(trigger))
		{
			return true;
		}
		if (GameplayManager.instance != null && GameplayManager.instance.GetGameAugments(ModType.Player).HasSnippet(trigger))
		{
			return true;
		}
		if (this.Mana.ManaBaseAugments.HasSnippet(trigger))
		{
			return true;
		}
		if (AchievementManager.HasAchievementWithTrigger(trigger) || this.CurQuests.HasSnippet(trigger))
		{
			return true;
		}
		if (GameplayManager.IsInGame)
		{
			foreach (KeyValuePair<AugmentRootNode, int> keyValuePair in InkManager.PurchasedMods.trees)
			{
				AugmentRootNode augmentRootNode;
				int num;
				keyValuePair.Deconstruct(out augmentRootNode, out num);
				if (augmentRootNode.SnippetMatches.Contains(trigger))
				{
					return true;
				}
			}
			if (GameplayManager.instance != null && GameplayManager.instance.GenreBindings != null && GameplayManager.instance.GenreBindings.HasSnippet(trigger))
			{
				return true;
			}
			GameplayManager instance = GameplayManager.instance;
			if (((instance != null) ? instance.PlayerTeamMods : null) != null && GameplayManager.instance.PlayerTeamMods.HasSnippet(trigger))
			{
				return true;
			}
			return false;
		}
		return false;
	}

	// Token: 0x0600060D RID: 1549 RVA: 0x0002D050 File Offset: 0x0002B250
	public override Augments GetSnippetAugments(EventTrigger t, bool includeStatuses = true, Augments cache = null)
	{
		Augments snippetAugments = base.GetSnippetAugments(t, includeStatuses, cache);
		snippetAugments.Add(this.BasePlayerAugment, 1);
		if (this.actions.core != null)
		{
			snippetAugments.Add(this.actions.core, 1);
		}
		if ((!GameplayManager.IsChallengeActive || !GameplayManager.IsInGame) && !LibraryRaces.IsPlayerRacing)
		{
			snippetAugments.Add(this.Talents.GetSnippetAugments(t));
		}
		if (MapManager.InLobbyScene && LibraryManager.instance != null && !LibraryRaces.IsPlayerRacing)
		{
			snippetAugments.Add(LibraryManager.instance.LibraryAugments.GetSnippetAugments(t));
		}
		snippetAugments.Add(this.Mana.ManaBaseAugments);
		if (GameplayManager.instance != null)
		{
			snippetAugments.Add(GameplayManager.instance.GetGameAugments(ModType.Player).GetSnippetAugments(t));
		}
		if (GameplayManager.IsInGame)
		{
			foreach (KeyValuePair<AugmentRootNode, int> keyValuePair in InkManager.PurchasedMods.trees)
			{
				AugmentRootNode augmentRootNode;
				int num;
				keyValuePair.Deconstruct(out augmentRootNode, out num);
				AugmentRootNode augmentRootNode2 = augmentRootNode;
				int count = num;
				if (augmentRootNode2.ApplyToPlayers && augmentRootNode2.SnippetMatches.Contains(t))
				{
					snippetAugments.Add(augmentRootNode2, count);
				}
			}
			if (GameplayManager.instance != null && GameplayManager.instance.GenreBindings != null)
			{
				foreach (KeyValuePair<AugmentRootNode, int> keyValuePair in GameplayManager.instance.GenreBindings.trees)
				{
					AugmentRootNode augmentRootNode;
					int num;
					keyValuePair.Deconstruct(out augmentRootNode, out num);
					AugmentRootNode augmentRootNode3 = augmentRootNode;
					int count2 = num;
					if (augmentRootNode3.ApplyToPlayers && augmentRootNode3.SnippetMatches.Contains(t))
					{
						snippetAugments.Add(augmentRootNode3, count2);
					}
				}
			}
			GameplayManager instance = GameplayManager.instance;
			if (((instance != null) ? instance.PlayerTeamMods : null) != null)
			{
				snippetAugments.Add(GameplayManager.instance.PlayerTeamMods.GetSnippetAugments(t));
			}
		}
		return snippetAugments;
	}

	// Token: 0x0600060E RID: 1550 RVA: 0x0002D26C File Offset: 0x0002B46C
	public void AddAugmentExternal(AugmentTree mod)
	{
		if (base.view.IsMine)
		{
			this.Net.AugmentAddedExternal(mod);
		}
		this.AddAugment(mod, 1);
	}

	// Token: 0x0600060F RID: 1551 RVA: 0x0002D28F File Offset: 0x0002B48F
	public override void RemoveAugment(AugmentTree mod, int count = 1)
	{
		base.RemoveAugment(mod, count);
		if (base.IsMine)
		{
			this.Net.SendAugments(null);
		}
		this.AugmentsChanged();
	}

	// Token: 0x06000610 RID: 1552 RVA: 0x0002D2B3 File Offset: 0x0002B4B3
	public override void AddAugment(AugmentTree mod, int count = 1)
	{
		base.AddAugment(mod, count);
		mod.Root.WarmPoolObjects();
		if (base.IsMine)
		{
			this.Net.SendAugments(null);
			Progression.UnseenAugments.Remove(mod.ID);
		}
		this.AugmentsChanged();
	}

	// Token: 0x06000611 RID: 1553 RVA: 0x0002D2F4 File Offset: 0x0002B4F4
	public void AugmentsChanged()
	{
		if (this.changedThisFrame)
		{
			return;
		}
		this.changedThisFrame = true;
		if (this.AugmentAddedFX != null)
		{
			this.AugmentAddedFX.Play();
		}
		this.Audio.AugmentAdded();
		Action onAugmentsChanged = this.OnAugmentsChanged;
		if (onAugmentsChanged != null)
		{
			onAugmentsChanged();
		}
		AugmentsPanel.instance.GameAugmentsChanged();
	}

	// Token: 0x06000612 RID: 1554 RVA: 0x0002D350 File Offset: 0x0002B550
	public void UpdateTalents()
	{
		if (!base.IsMine)
		{
			return;
		}
		this.InkLevel = Progression.InkLevel;
		this.PrestigeLevel = Progression.PrestigeCount;
		this.Talents = PlayerDB.GetCurrentTalents();
		base.ClearModCache();
		this.Net.SendAugments(null);
	}

	// Token: 0x06000613 RID: 1555 RVA: 0x0002D390 File Offset: 0x0002B590
	internal override void CollectModTags()
	{
		this.actions.primary.Root.AddTags(this.tagsCache);
		this.actions.secondary.Root.AddTags(this.tagsCache);
		this.actions.movement.Root.AddTags(this.tagsCache);
		this.actions.utility.Root.AddTags(this.tagsCache);
		this.actions.ghost.Root.AddTags(this.tagsCache);
		base.CollectModTags();
	}

	// Token: 0x06000614 RID: 1556 RVA: 0x0002D42C File Offset: 0x0002B62C
	public void ResetPosition(bool withPageFlip = false)
	{
		if (withPageFlip)
		{
			PageFlip.instance.DoFlipInstant();
		}
		if (!MapManager.InLobbyScene)
		{
			Transform spawnPoint = GameplayManager.instance.GetSpawnPoint();
			this.Movement.SetPositionWithCamera(spawnPoint.position, spawnPoint.forward, true, true);
			return;
		}
		if (LibraryManager.WantAntechamberSpawn)
		{
			Library_RaidControl.instance.SpawnInAntechamber();
		}
		else
		{
			Transform spawnPoint2 = GameplayManager.instance.GetSpawnPoint();
			this.Movement.SetPositionWithCamera(spawnPoint2.position, spawnPoint2.forward, true, true);
		}
		PlayerControl.ToggleMultiplayerCollision(true);
	}

	// Token: 0x06000615 RID: 1557 RVA: 0x0002D4B0 File Offset: 0x0002B6B0
	public void Reset(bool resetMods)
	{
		if (!base.IsMine)
		{
			return;
		}
		this.ResetPosition(false);
		if (base.IsDead)
		{
			this.health.Revive(1f);
		}
		this.health.Reset();
		this.Mana.ResetMana();
		base.ResetAbilities();
		base.RemoveAllStatuses(false);
		base.view.RPC("ResetNetwork", RpcTarget.All, Array.Empty<object>());
		if (GameplayManager.IsChallengeActive)
		{
			this.actions.ApplyLoadout(Settings.GetLoadout(), false);
		}
		if (!resetMods)
		{
			return;
		}
		this.Augment = new Augments();
		base.ClearModCache();
		this.UpdateTalents();
		this.Net.SendAugments(null);
		this.PStats = new PlayerGameStats();
	}

	// Token: 0x06000616 RID: 1558 RVA: 0x0002D568 File Offset: 0x0002B768
	[PunRPC]
	private void ResetNetwork()
	{
		this.Display.ResetDisplay();
	}

	// Token: 0x06000617 RID: 1559 RVA: 0x0002D575 File Offset: 0x0002B775
	public override bool CanUseAbilities()
	{
		return !this.IsSpectator && base.CanUseAbilities();
	}

	// Token: 0x06000618 RID: 1560 RVA: 0x0002D588 File Offset: 0x0002B788
	public void SetSpectatorLocal(bool spectatorVal)
	{
		this.IsSpectator = spectatorVal;
		this.Targetable = !spectatorVal;
		this.Affectable = !spectatorVal;
		this.Display.ToggleSpectator(spectatorVal);
		foreach (PlayerControl playerControl in PlayerControl.AllPlayers)
		{
			if (!playerControl.IsMine)
			{
				playerControl.Display.ToggleOutline();
			}
		}
		if (!base.IsMine)
		{
			this.Display.ToggleOutline();
		}
	}

	// Token: 0x06000619 RID: 1561 RVA: 0x0002D620 File Offset: 0x0002B820
	private void FixPositionIfInvalid()
	{
		float num = 0f;
		if (Scene_Settings.instance == null)
		{
			num = Scene_Settings.instance.MapOrigin.y;
		}
		if (this.movement.GetPosition().y < num - 10f)
		{
			this.ResetPosition(true);
		}
	}

	// Token: 0x0600061A RID: 1562 RVA: 0x0002D670 File Offset: 0x0002B870
	public void TryRespawn()
	{
		if (!base.IsDead)
		{
			return;
		}
		this.Respawn(this.Health.Ghost.transform.position, 0.33f);
	}

	// Token: 0x0600061B RID: 1563 RVA: 0x0002D69C File Offset: 0x0002B89C
	public void Respawn(Vector3 spawnPoint, float lifeAmount = 1f)
	{
		this.Movement.SetPositionImmediate(spawnPoint, Vector3.zero, true);
		this.health.Revive(lifeAmount);
		this.health.FillShield();
		if (base.IsMine)
		{
			this.PStats.IncreaseCounts(PlayerStat.Rebirths, 1);
		}
	}

	// Token: 0x0600061C RID: 1564 RVA: 0x0002D6E8 File Offset: 0x0002B8E8
	public void Respawn(Transform spawnPoint, float lifeAmount = 1f)
	{
		this.Movement.SetPositionWithExplicitCamera(spawnPoint.position, spawnPoint.forward, spawnPoint.forward);
		this.health.Revive(lifeAmount);
		this.health.FillShield();
		if (base.IsMine)
		{
			this.PStats.IncreaseCounts(PlayerStat.Rebirths, 1);
		}
	}

	// Token: 0x0600061D RID: 1565 RVA: 0x0002D740 File Offset: 0x0002B940
	public override EffectProperties DamageDone(DamageInfo info)
	{
		EffectProperties result = base.DamageDone(info);
		if (base.IsMine && GameplayManager.IsInGame)
		{
			this.PStats.DamageDone(info);
			GameStats.PlayerDamageDone(info);
		}
		this.Health.DamageDone(info);
		if (this == PlayerControl.myInstance)
		{
			CombatTextController.instance.AddLocalDamageEvent(info);
		}
		return result;
	}

	// Token: 0x0600061E RID: 1566 RVA: 0x0002D799 File Offset: 0x0002B999
	public override void HealingDone(DamageInfo info)
	{
		base.HealingDone(info);
		if (base.IsMine)
		{
			this.PStats.HealingDone(info);
		}
	}

	// Token: 0x0600061F RID: 1567 RVA: 0x0002D7B8 File Offset: 0x0002B9B8
	public void DamagedEntityKilled(EntityControl control)
	{
		if (!base.IsMine)
		{
			return;
		}
		this.OnKilledEntity(control);
		if (base.IsMine)
		{
			this.PStats.IncreaseCounts(PlayerStat.Kills, 1);
			AIControl aicontrol = control as AIControl;
			if (aicontrol != null && !string.IsNullOrEmpty(aicontrol.StatID))
			{
				GameStats.IncrementEnemyStat(aicontrol.StatID, false);
			}
		}
	}

	// Token: 0x06000620 RID: 1568 RVA: 0x0002D80E File Offset: 0x0002BA0E
	public override void OnKilledEntity(EntityControl control)
	{
		if (base.IsMine)
		{
			base.Invoke("KilledEnemyDelayed", 0.05f);
		}
		base.OnKilledEntity(control);
	}

	// Token: 0x06000621 RID: 1569 RVA: 0x0002D830 File Offset: 0x0002BA30
	private void KilledEnemyDelayed()
	{
		if (Time.realtimeSinceStartup - this.lastKillTime < 0.1f)
		{
			return;
		}
		this.lastKillTime = Time.realtimeSinceStartup;
		AudioManager.PlaySFX2D(AudioManager.instance.KilledEnemy.GetRandomClip(-1), 1f, 0.1f);
		HitMarker.KillMarker();
	}

	// Token: 0x06000622 RID: 1570 RVA: 0x0002D880 File Offset: 0x0002BA80
	private void GameStateChanged(GameState from, GameState to)
	{
		if (to == GameState.Reward_Start)
		{
			base.RemoveNegativeStatuses(true);
			if (base.IsDead)
			{
				this.TryRespawn();
			}
			this.actions.TryResetEphemerals(PlayerAbilityInfo.ResetType.WaveComplete);
			return;
		}
		if (from == GameState.Hub_Traveling)
		{
			this.TriggerSnippets(EventTrigger.Game_Started, null, 1f);
			return;
		}
		if (to == GameState.Hub)
		{
			this.actions.TryResetEphemerals(PlayerAbilityInfo.ResetType.ReturnToLobby);
		}
	}

	// Token: 0x06000623 RID: 1571 RVA: 0x0002D8D6 File Offset: 0x0002BAD6
	private void OnMapChangeFinished()
	{
		this.TriggerSnippets(EventTrigger.MapChanged, null, 1f);
		this.Net.SendExtraData(null);
		this.actions.TryResetEphemerals(PlayerAbilityInfo.ResetType.MapChange);
		this.Movement.MapChanged();
	}

	// Token: 0x06000624 RID: 1572 RVA: 0x0002D909 File Offset: 0x0002BB09
	public override void TriggerSnippets(EventTrigger trigger, EffectProperties props = null, float chanceMult = 1f)
	{
		if (props == null)
		{
			props = new EffectProperties(this);
		}
		base.TriggerSnippets(trigger, props, chanceMult);
		if (this == PlayerControl.myInstance)
		{
			AchievementManager.UnlockEvent(trigger, props);
			if (!MapManager.InLobbyScene)
			{
				this.CurQuests.ApplySnippets(this, trigger, props, chanceMult);
			}
		}
	}

	// Token: 0x06000625 RID: 1573 RVA: 0x0002D94C File Offset: 0x0002BB4C
	internal override void OnDestroy()
	{
		base.OnDestroy();
		GameplayManager.OnGameStateChanged = (Action<GameState, GameState>)Delegate.Remove(GameplayManager.OnGameStateChanged, new Action<GameState, GameState>(this.GameStateChanged));
		MapManager.OnMapChangeFinished = (Action)Delegate.Remove(MapManager.OnMapChangeFinished, new Action(this.OnMapChangeFinished));
		PlayerControl.AllPlayers.Remove(this);
		PlayerControl.AllPlayers.Sort((PlayerControl x, PlayerControl y) => x.ViewID.CompareTo(y.ViewID));
		GameRecord.RecordEvent(GameRecord.EventType.Player_Disconnected, this, this.movement.GetPosition(), null);
		MultiplayerUI instance = MultiplayerUI.instance;
		if (instance == null)
		{
			return;
		}
		instance.OnPlayerLeft(this);
	}

	// Token: 0x17000089 RID: 137
	// (get) Token: 0x06000626 RID: 1574 RVA: 0x0002D9F7 File Offset: 0x0002BBF7
	public static int PlayerCount
	{
		get
		{
			List<PlayerControl> allPlayers = PlayerControl.AllPlayers;
			if (allPlayers == null)
			{
				return 0;
			}
			return allPlayers.Count;
		}
	}

	// Token: 0x1700008A RID: 138
	// (get) Token: 0x06000627 RID: 1575 RVA: 0x0002DA09 File Offset: 0x0002BC09
	public static int MyViewID
	{
		get
		{
			if (PlayerControl.myInstance != null)
			{
				return PlayerControl.myInstance.ViewID;
			}
			return -1;
		}
	}

	// Token: 0x1700008B RID: 139
	// (get) Token: 0x06000628 RID: 1576 RVA: 0x0002DA24 File Offset: 0x0002BC24
	public static int DeadPlayersCount
	{
		get
		{
			int num = 0;
			using (List<PlayerControl>.Enumerator enumerator = PlayerControl.AllPlayers.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.IsDead)
					{
						num++;
					}
				}
			}
			return num;
		}
	}

	// Token: 0x06000629 RID: 1577 RVA: 0x0002DA7C File Offset: 0x0002BC7C
	public static PlayerControl GetPlayer(int playerID)
	{
		foreach (PlayerControl playerControl in PlayerControl.AllPlayers)
		{
			if (playerControl.net.view.Owner.ActorNumber == playerID)
			{
				return playerControl;
			}
		}
		return null;
	}

	// Token: 0x0600062A RID: 1578 RVA: 0x0002DAE8 File Offset: 0x0002BCE8
	public static PlayerControl GetPlayerFromViewID(int viewID)
	{
		foreach (PlayerControl playerControl in PlayerControl.AllPlayers)
		{
			if (playerControl.ViewID == viewID)
			{
				return playerControl;
			}
		}
		return null;
	}

	// Token: 0x0600062B RID: 1579 RVA: 0x0002DB44 File Offset: 0x0002BD44
	public static int GetPlayerIndex(PlayerControl player)
	{
		return PlayerControl.AllPlayers.IndexOf(player);
	}

	// Token: 0x0600062C RID: 1580 RVA: 0x0002DB54 File Offset: 0x0002BD54
	public static PlayerControl ClosestPlayer(Vector3 point, float minDist = 3.4028235E+38f)
	{
		float num = minDist;
		PlayerControl result = null;
		foreach (PlayerControl playerControl in PlayerControl.AllPlayers)
		{
			float num2 = Vector3.Distance(playerControl.movement.GetPosition(), point);
			if (num2 < num)
			{
				num = num2;
				result = playerControl;
			}
		}
		return result;
	}

	// Token: 0x0600062D RID: 1581 RVA: 0x0002DBC4 File Offset: 0x0002BDC4
	public static void ToggleMultiplayerCollision(bool enabled)
	{
		int layer = 8;
		int layer2 = 11;
		Physics.IgnoreLayerCollision(layer, layer2, !enabled);
		if (PlayerControl.myInstance != null)
		{
			PlayerControl.myInstance.Movement.mover.RecalculateSensorLayerMask();
		}
	}

	// Token: 0x0600062E RID: 1582 RVA: 0x0002DC00 File Offset: 0x0002BE00
	public PlayerControl()
	{
	}

	// Token: 0x0600062F RID: 1583 RVA: 0x0002DC51 File Offset: 0x0002BE51
	[CompilerGenerated]
	private void <Setup>b__56_0(DamageInfo info)
	{
		GameRecord.PlayerDamageTaken(this, info);
	}

	// Token: 0x06000630 RID: 1584 RVA: 0x0002DC5A File Offset: 0x0002BE5A
	[CompilerGenerated]
	private void <Setup>b__56_1(DamageInfo info)
	{
		GameRecord.PlayerBarrierBroken(this, info);
	}

	// Token: 0x06000631 RID: 1585 RVA: 0x0002DC63 File Offset: 0x0002BE63
	[CompilerGenerated]
	private void <ApplyLocalHealthEvents>b__60_1(int healed)
	{
		this.Display.HealPulse(healed);
	}

	// Token: 0x040004F2 RID: 1266
	public static PlayerControl myInstance;

	// Token: 0x040004F3 RID: 1267
	[NonSerialized]
	public PlayerInput Input;

	// Token: 0x040004F4 RID: 1268
	[NonSerialized]
	public PlayerMana Mana;

	// Token: 0x040004F5 RID: 1269
	public ParticleSystem AugmentAddedFX;

	// Token: 0x040004F6 RID: 1270
	public AugmentTree BasePlayerAugment;

	// Token: 0x040004F7 RID: 1271
	[Header("Ghost System")]
	public GhostPlayerDisplay GhostPlayerDisplay;

	// Token: 0x040004F8 RID: 1272
	[NonSerialized]
	public Vector3 AimPoint;

	// Token: 0x040004F9 RID: 1273
	[NonSerialized]
	public Vector3 CameraAimPoint;

	// Token: 0x040004FA RID: 1274
	[NonSerialized]
	public Vector3 AimPointSmooth;

	// Token: 0x040004FB RID: 1275
	private EntityControl lastTarg;

	// Token: 0x040004FC RID: 1276
	private EntityControl ctarg;

	// Token: 0x040004FD RID: 1277
	private float lastTargetSetTime;

	// Token: 0x040004FE RID: 1278
	private EntityControl lastAllyTarg;

	// Token: 0x040004FF RID: 1279
	private EntityControl allytarg;

	// Token: 0x04000500 RID: 1280
	private float lastAllySetTime;

	// Token: 0x04000501 RID: 1281
	[NonSerialized]
	public string Username;

	// Token: 0x04000502 RID: 1282
	[NonSerialized]
	public string UserID = "";

	// Token: 0x04000503 RID: 1283
	[NonSerialized]
	public PlayerActions actions;

	// Token: 0x04000504 RID: 1284
	[NonSerialized]
	public PanelType CurMenu;

	// Token: 0x04000505 RID: 1285
	[NonSerialized]
	public bool IsDeveloper;

	// Token: 0x04000506 RID: 1286
	public bool IsSpectator;

	// Token: 0x04000507 RID: 1287
	public PlayerGameStats PStats = new PlayerGameStats();

	// Token: 0x04000508 RID: 1288
	public Augments Talents = new Augments();

	// Token: 0x04000509 RID: 1289
	[NonSerialized]
	public int InkLevel = 1;

	// Token: 0x0400050A RID: 1290
	[NonSerialized]
	public int PrestigeLevel;

	// Token: 0x0400050B RID: 1291
	[NonSerialized]
	public int PageSeed;

	// Token: 0x0400050C RID: 1292
	[NonSerialized]
	public int PageSeedCount;

	// Token: 0x0400050D RID: 1293
	[NonSerialized]
	public List<Vector2> pageRolls = new List<Vector2>();

	// Token: 0x0400050E RID: 1294
	private System.Random PageRand = new System.Random();

	// Token: 0x0400050F RID: 1295
	public static Action LocalPlayerSpawned;

	// Token: 0x04000510 RID: 1296
	public Action OnAugmentsChanged;

	// Token: 0x04000511 RID: 1297
	private float lastTargetTime;

	// Token: 0x04000512 RID: 1298
	private bool isHelping;

	// Token: 0x04000513 RID: 1299
	private const float Help_Time = 5f;

	// Token: 0x04000514 RID: 1300
	private bool changedThisFrame;

	// Token: 0x04000515 RID: 1301
	private float lastKillTime;

	// Token: 0x04000516 RID: 1302
	public static List<PlayerControl> AllPlayers;

	// Token: 0x020004A0 RID: 1184
	[CompilerGenerated]
	[Serializable]
	private sealed class <>c
	{
		// Token: 0x0600222C RID: 8748 RVA: 0x000C5BAC File Offset: 0x000C3DAC
		// Note: this type is marked as 'beforefieldinit'.
		static <>c()
		{
		}

		// Token: 0x0600222D RID: 8749 RVA: 0x000C5BB8 File Offset: 0x000C3DB8
		public <>c()
		{
		}

		// Token: 0x0600222E RID: 8750 RVA: 0x000C5BC0 File Offset: 0x000C3DC0
		internal int <Awake>b__55_0(PlayerControl x, PlayerControl y)
		{
			return x.GetComponent<PhotonView>().ViewID.CompareTo(y.GetComponent<PhotonView>().ViewID);
		}

		// Token: 0x0600222F RID: 8751 RVA: 0x000C5BEB File Offset: 0x000C3DEB
		internal bool <SetUsername>b__57_0(char c)
		{
			return CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark;
		}

		// Token: 0x06002230 RID: 8752 RVA: 0x000C5BFC File Offset: 0x000C3DFC
		internal void <ApplyLocalHealthEvents>b__60_0(DamageInfo dmg)
		{
			CameraShaker instance = CameraShaker.Instance;
			if (instance != null)
			{
				instance.ShakeOnce(Mathf.Lerp(0f, 1f, dmg.TotalAmount / 10f), 8f, 0f, 0.5f);
			}
			PostFXManager.DamgageFX(dmg.TotalAmount);
		}

		// Token: 0x06002231 RID: 8753 RVA: 0x000C5C50 File Offset: 0x000C3E50
		internal int <OnDestroy>b__107_0(PlayerControl x, PlayerControl y)
		{
			return x.ViewID.CompareTo(y.ViewID);
		}

		// Token: 0x040023B9 RID: 9145
		public static readonly PlayerControl.<>c <>9 = new PlayerControl.<>c();

		// Token: 0x040023BA RID: 9146
		public static Comparison<PlayerControl> <>9__55_0;

		// Token: 0x040023BB RID: 9147
		public static Func<char, bool> <>9__57_0;

		// Token: 0x040023BC RID: 9148
		public static Action<DamageInfo> <>9__60_0;

		// Token: 0x040023BD RID: 9149
		public static Comparison<PlayerControl> <>9__107_0;
	}
}
