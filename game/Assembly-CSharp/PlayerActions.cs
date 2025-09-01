using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using QFSW.QC;
using UnityEngine;

// Token: 0x02000087 RID: 135
public class PlayerActions : MonoBehaviour
{
	// Token: 0x060005B4 RID: 1460 RVA: 0x00029DB0 File Offset: 0x00027FB0
	private void Awake()
	{
		this.control = base.GetComponent<PlayerControl>();
		this.Input = base.GetComponent<PlayerInput>();
		this.display = base.GetComponent<PlayerDisplay>();
		this.sync = base.GetComponent<PlayerNetwork>();
		this.mana = base.GetComponent<PlayerMana>();
		this.control.Abilities.Clear();
		this.control.AddAbility(this.primary);
		this.control.AddAbility(this.secondary);
		this.control.AddAbility(this.utility);
		this.control.AddAbility(this.movement);
		this.control.AddAbility(this.ghost);
		this.control.ResetAbilities();
		if (this.control.IsMine)
		{
			this.abilityCastFailed = (Action<PlayerAbilityType, CastFailedReason, int>)Delegate.Combine(this.abilityCastFailed, new Action<PlayerAbilityType, CastFailedReason, int>(Crosshair.instance.CastFailed));
			this.abilityCastFailed = (Action<PlayerAbilityType, CastFailedReason, int>)Delegate.Combine(this.abilityCastFailed, new Action<PlayerAbilityType, CastFailedReason, int>(ManaDisplay.instance.CastFailed));
			this.abilityCastSucceeded = (Action<PlayerAbilityType, Dictionary<MagicColor, int>>)Delegate.Combine(this.abilityCastSucceeded, new Action<PlayerAbilityType, Dictionary<MagicColor, int>>(Crosshair.instance.AbilityManaUsed));
		}
	}

	// Token: 0x060005B5 RID: 1461 RVA: 0x00029EF0 File Offset: 0x000280F0
	private void Start()
	{
		if (this.control.IsMine)
		{
			Progression.Loadout loadout = Settings.GetLoadout();
			this.ApplyLoadout(loadout, false);
			this.cachedAbilities[PlayerAbilityType.Primary] = loadout.Generator;
			this.cachedAbilities[PlayerAbilityType.Secondary] = loadout.Spender;
			this.cachedAbilities[PlayerAbilityType.Movement] = loadout.Movement;
		}
	}

	// Token: 0x060005B6 RID: 1462 RVA: 0x00029F50 File Offset: 0x00028150
	public void ApplyLoadout(Progression.Loadout loadout, bool force = false)
	{
		if (loadout.Core == null)
		{
			this.SetCore(this.core);
			return;
		}
		if (UnlockManager.IsCoreUnlocked(loadout.Core))
		{
			this.SetCore(loadout.Core);
		}
		if (UnlockManager.IsAbilityUnlocked(loadout.Generator))
		{
			this.LoadAbility(loadout.Generator.Root.PlrAbilityType, loadout.Generator.Root.guid, false);
		}
		if (UnlockManager.IsAbilityUnlocked(loadout.Spender))
		{
			this.LoadAbility(loadout.Spender.Root.PlrAbilityType, loadout.Spender.Root.guid, false);
		}
		if (UnlockManager.IsAbilityUnlocked(loadout.Movement))
		{
			this.LoadAbility(loadout.Movement.Root.PlrAbilityType, loadout.Movement.Root.guid, false);
		}
	}

	// Token: 0x060005B7 RID: 1463 RVA: 0x0002A030 File Offset: 0x00028230
	private void Update()
	{
		if (!this.Input.isLocalControl)
		{
			return;
		}
		if (MapManager.InLobbyScene && PlayerNook.IsInEditMode)
		{
			this.TryReleaseAbility(this.primary, true);
			this.TryReleaseAbility(this.secondary, true);
			this.TryReleaseAbility(this.utility, true);
			this.TryReleaseAbility(this.movement, true);
			PingUISelector.Deactivate();
			EmoteSelector.Deactivate();
			foreach (Ability ability in this.toCancel)
			{
				this.TryReleaseAbility(ability.rootNode, false);
			}
			this.toCancel.Clear();
			this.HandleEditingControls();
			return;
		}
		new EffectProperties().SourceControl = base.GetComponent<EntityControl>();
		if (!QuantumConsole.Instance.IsActive && UnityEngine.Input.GetKeyDown(KeyCode.O) && this.control.IsDeveloper && PanelManager.CurPanel == PanelType.GameInvisible)
		{
			this.ToggleSpectator();
		}
		if (PanelManager.CurPanel == PanelType.GameInvisible)
		{
			if (this.Input.pingPressed)
			{
				this.pingTimer += Time.deltaTime;
				if (this.pingTimer > 0.11f)
				{
					PingUISelector.Activate();
				}
			}
			if (this.Input.pingReleased)
			{
				this.pingTimer = 0f;
				PlayerDB.PingType ping = PingUISelector.IsActive ? PingUISelector.GetSelectedType() : PlayerDB.PingType.Generic;
				PlayerPing pingController = this.PingController;
				if (pingController != null)
				{
					pingController.TryPing(ping);
				}
				PingUISelector.Deactivate();
			}
			if (this.Input.emotePressed)
			{
				EmoteSelector.Activate();
			}
			if (this.Input.emoteReleased)
			{
				string selectedEmote = EmoteSelector.GetSelectedEmote();
				if (!string.IsNullOrEmpty(selectedEmote))
				{
					PlayerControl.myInstance.Net.Emote(selectedEmote);
				}
				EmoteSelector.Deactivate();
			}
		}
		if (!this.control.IsDead)
		{
			if (this.Input.firePressed && this.primary != null && !this.GetAbility(PlayerAbilityType.Secondary).IsActive(true) && !LibraryRaces.IsPlayerRacing)
			{
				this.TryActivateAbility(this.primary);
			}
			else if (this.primary != null)
			{
				this.TryReleaseAbility(this.primary, true);
			}
			if (this.Input.secondaryPressed && this.secondary != null && !this.GetAbility(PlayerAbilityType.Primary).IsActive(true) && !LibraryRaces.IsPlayerRacing)
			{
				this.TryActivateAbility(this.secondary);
			}
			else if (this.secondary != null)
			{
				this.TryReleaseAbility(this.secondary, true);
			}
			if (this.Input.utilityPressed && this.utility != null && !LibraryRaces.IsPlayerRacing)
			{
				this.TryActivateAbility(this.utility);
			}
			else if (this.utility != null)
			{
				this.TryReleaseAbility(this.utility, true);
			}
			if (this.Input.boostPressed && this.movement != null)
			{
				this.TryActivateAbility(this.movement);
			}
			else if (this.movement != null)
			{
				this.TryReleaseAbility(this.movement, true);
			}
			foreach (Ability ability2 in this.toCancel)
			{
				this.TryReleaseAbility(ability2.rootNode, false);
			}
			this.toCancel.Clear();
			if (!this.control.CanUseAbilities())
			{
				this.TryReleaseAbility(this.primary, true);
				this.TryReleaseAbility(this.secondary, true);
				this.TryReleaseAbility(this.utility, true);
				this.TryReleaseAbility(this.movement, true);
			}
			return;
		}
		if (!this.control.GhostPlayerDisplay.IsActive)
		{
			return;
		}
		if (this.Input.firePressed && this.ghost != null)
		{
			this.TryActivateAbility(this.ghost);
			return;
		}
		if (this.ghost != null)
		{
			this.TryReleaseAbility(this.ghost, true);
		}
	}

	// Token: 0x060005B8 RID: 1464 RVA: 0x0002A490 File Offset: 0x00028690
	private void HandleEditingControls()
	{
		if (PlayerNook.MyNook == null)
		{
			return;
		}
		if (this.Input.actions.SecondaryUse.WasPressed)
		{
			if (PlayerNook.MyNook.HeldItem != null)
			{
				PlayerNook.MyNook.SecondaryActionTaken();
			}
			else
			{
				NookPanel.Toggle();
			}
		}
		if (this.Input.actions.PrimaryUse.WasPressed)
		{
			PlayerNook.MyNook.PrimaryActionTaken();
		}
		if (this.Input.actions.Ping.WasPressed)
		{
			PlayerNook.MyNook.SecondaryActionTaken();
		}
		if (InputManager.IsUsingController)
		{
			if (this.Input.actions.MovementAbility.WasPressed)
			{
				PlayerNook.MyNook.SnapMode = !PlayerNook.MyNook.SnapMode;
				return;
			}
		}
		else
		{
			PlayerNook.MyNook.SnapMode = this.Input.boostPressed;
		}
	}

	// Token: 0x060005B9 RID: 1465 RVA: 0x0002A572 File Offset: 0x00028772
	public void ToggleSpectator()
	{
		this.control.Net.SetSpectator(!this.control.IsSpectator);
	}

	// Token: 0x060005BA RID: 1466 RVA: 0x0002A594 File Offset: 0x00028794
	private void TryActivateAbility(AbilityRootNode a)
	{
		CastFailedReason castFailedReason = CastFailedReason.None;
		if (a == null)
		{
			return;
		}
		if (!this.control.CanUseAbilities())
		{
			castFailedReason = CastFailedReason.Prevented;
		}
		Ability ability = this.control.GetAbility(a.guid);
		if (castFailedReason == CastFailedReason.None && ability == null)
		{
			castFailedReason = CastFailedReason.Invalid;
		}
		if (castFailedReason == CastFailedReason.None)
		{
			castFailedReason = ability.CanUse(this.control);
		}
		PlayerAbilityType abilityType = this.GetAbilityType(a);
		float num = 0f;
		if (castFailedReason != CastFailedReason.None)
		{
			Action<PlayerAbilityType, CastFailedReason, int> action = this.abilityCastFailed;
			if (action == null)
			{
				return;
			}
			action(abilityType, castFailedReason, (int)num);
			return;
		}
		else
		{
			EffectProperties effectProperties = new EffectProperties(this.control);
			if (ability.properties == null)
			{
				ability.properties = effectProperties;
			}
			effectProperties.StartLoc = (effectProperties.OutLoc = ability.location.Copy());
			effectProperties.AbilityType = abilityType;
			effectProperties.SourceLocation = effectProperties.StartLoc.GetTransform(effectProperties);
			if (castFailedReason == CastFailedReason.None && !this.control.IsDead)
			{
				num = this.control.ModifyManaCost(effectProperties, ability.ManaCost);
				if (ability.ManaCost > 0f && num > -10f)
				{
					num = Mathf.Max(num, 1f);
				}
				if (!this.mana.CanUseMana(num))
				{
					castFailedReason = CastFailedReason.Mana;
				}
			}
			if (castFailedReason != CastFailedReason.None)
			{
				Action<PlayerAbilityType, CastFailedReason, int> action2 = this.abilityCastFailed;
				if (action2 == null)
				{
					return;
				}
				action2(abilityType, castFailedReason, (int)num);
				return;
			}
			else
			{
				Dictionary<MagicColor, int> dictionary = new Dictionary<MagicColor, int>();
				if (num > 0f)
				{
					dictionary = this.mana.ConsumeMana(num, true);
					foreach (KeyValuePair<MagicColor, int> keyValuePair in dictionary)
					{
						effectProperties.AddMana(keyValuePair.Key, keyValuePair.Value);
					}
				}
				if (!this.control.TryActivateAbility(a.RootNode.guid, effectProperties))
				{
					return;
				}
				Action<PlayerAbilityType, Dictionary<MagicColor, int>> action3 = this.abilityCastSucceeded;
				if (action3 != null)
				{
					action3(abilityType, dictionary);
				}
				ValueTuple<Vector3, Vector3> originVectors = effectProperties.GetOriginVectors();
				Action<int, Vector3, Vector3, EffectProperties> action4 = this.abilityActivated;
				if (action4 != null)
				{
					action4((int)abilityType, originVectors.Item1, originVectors.Item2, effectProperties);
				}
				GameRecord.PlayerAbilityEvent(this.control, abilityType);
				if (!a.IsChanneledAbility())
				{
					this.control.PlayerAbilityReleaseSnippits((int)abilityType, originVectors.Item1, originVectors.Item2, effectProperties);
				}
				return;
			}
		}
	}

	// Token: 0x060005BB RID: 1467 RVA: 0x0002A7CC File Offset: 0x000289CC
	public void TryReleaseAbility(AbilityRootNode a, bool fromInput = true)
	{
		if (a == null)
		{
			return;
		}
		Ability ability = this.control.GetAbility(a.guid);
		if (ability == null)
		{
			return;
		}
		if (fromInput)
		{
			ability.AutoFiring = false;
		}
		if (ability.current == null)
		{
			return;
		}
		if (!ability.IsActive(false))
		{
			return;
		}
		if (fromInput && ability.props.MinCancelTime > ability.Lifetime && ability.props.MinCancelTime > 0f)
		{
			return;
		}
		if (ability.isReleasing)
		{
			return;
		}
		EffectProperties effectProperties = (ability.properties == null) ? new EffectProperties() : ability.properties.Copy(false);
		effectProperties.StartLoc = (effectProperties.OutLoc = ability.location.Copy());
		effectProperties.SourceControl = this.control;
		effectProperties.SeekTarget = ((this.control.currentTarget == null) ? null : this.control.currentTarget.gameObject);
		effectProperties.AllyTarget = ((this.control.allyTarget == null) ? null : this.control.allyTarget.gameObject);
		effectProperties.SourceLocation = ability.location.GetTransform(effectProperties);
		effectProperties.AbilityType = ability.PlayerAbilityType;
		if (!this.control.TryReleaseAbility(a.RootNode.guid, effectProperties))
		{
			return;
		}
		PlayerAbilityType abilityType = this.GetAbilityType(a);
		if (this.abilityReleased != null)
		{
			ValueTuple<Vector3, Vector3> originVectors = effectProperties.GetOriginVectors();
			this.abilityReleased((int)abilityType, originVectors.Item1, originVectors.Item2, effectProperties);
		}
	}

	// Token: 0x060005BC RID: 1468 RVA: 0x0002A954 File Offset: 0x00028B54
	public void RefreshMana(PlayerAbilityType abilityType, Dictionary<MagicColor, int> newMana)
	{
		Ability ability = this.GetAbility(abilityType);
		if (ability == null || ability.props == null)
		{
			return;
		}
		ability.properties.ClearManaData();
		foreach (KeyValuePair<MagicColor, int> keyValuePair in newMana)
		{
			ability.properties.AddMana(keyValuePair.Key, keyValuePair.Value);
		}
	}

	// Token: 0x060005BD RID: 1469 RVA: 0x0002A9D4 File Offset: 0x00028BD4
	private PlayerAbilityType GetAbilityType(AbilityRootNode tree)
	{
		if (tree == null)
		{
			return PlayerAbilityType.Any;
		}
		if (this.primary != null && tree.guid == this.primary.RootNode.guid)
		{
			return PlayerAbilityType.Primary;
		}
		if (this.secondary != null && tree.guid == this.secondary.RootNode.guid)
		{
			return PlayerAbilityType.Secondary;
		}
		if (this.utility != null && tree.guid == this.utility.RootNode.guid)
		{
			return PlayerAbilityType.Utility;
		}
		if (this.movement != null && tree.guid == this.movement.RootNode.guid)
		{
			return PlayerAbilityType.Movement;
		}
		if (this.ghost != null && tree.guid == this.ghost.RootNode.guid)
		{
			return PlayerAbilityType.Ghost;
		}
		return PlayerAbilityType.Any;
	}

	// Token: 0x060005BE RID: 1470 RVA: 0x0002AAD0 File Offset: 0x00028CD0
	public void ForceCancel(AbilityRootNode root)
	{
		if (!this.control.IsMine)
		{
			return;
		}
		Ability ability = this.GetAbility(root);
		if (ability != null && !this.toCancel.Contains(ability))
		{
			this.toCancel.Add(ability);
		}
	}

	// Token: 0x060005BF RID: 1471 RVA: 0x0002AB10 File Offset: 0x00028D10
	public void ResetCooldown(PlayerAbilityType abilityType, bool useFrameDelay)
	{
		Ability ability = this.GetAbility(abilityType);
		if (ability == null)
		{
			return;
		}
		if (useFrameDelay)
		{
			AbilityTree abilityTree = ability.AbilityTree;
			base.StartCoroutine(this.ResetCooldownFrame(((abilityTree != null) ? abilityTree.RootNode : null) as AbilityRootNode));
			return;
		}
		EntityControl entityControl = this.control;
		AbilityTree abilityTree2 = ability.AbilityTree;
		entityControl.ResetCooldown(((abilityTree2 != null) ? abilityTree2.RootNode : null) as AbilityRootNode);
	}

	// Token: 0x060005C0 RID: 1472 RVA: 0x0002AB73 File Offset: 0x00028D73
	private IEnumerator ResetCooldownFrame(AbilityRootNode ability)
	{
		yield return true;
		this.control.ResetCooldown(ability);
		yield break;
	}

	// Token: 0x060005C1 RID: 1473 RVA: 0x0002AB8C File Offset: 0x00028D8C
	public void ModifyCooldown(PlayerAbilityType abilityType, float addedValue)
	{
		Ability ability = this.GetAbility(abilityType);
		if (ability == null)
		{
			return;
		}
		EntityControl entityControl = this.control;
		AbilityTree abilityTree = ability.AbilityTree;
		entityControl.ModifyCooldown(((abilityTree != null) ? abilityTree.RootNode : null) as AbilityRootNode, addedValue);
	}

	// Token: 0x060005C2 RID: 1474 RVA: 0x0002ABC8 File Offset: 0x00028DC8
	public void SetCooldown(PlayerAbilityType abilityType, float addedValue, EffectProperties props = null)
	{
		Ability ability = this.GetAbility(abilityType);
		if (ability == null)
		{
			return;
		}
		EntityControl entityControl = this.control;
		AbilityTree abilityTree = ability.AbilityTree;
		entityControl.SetCooldown(((abilityTree != null) ? abilityTree.RootNode : null) as AbilityRootNode, addedValue, props);
	}

	// Token: 0x060005C3 RID: 1475 RVA: 0x0002AC05 File Offset: 0x00028E05
	private Ability GetAbility(AbilityRootNode root)
	{
		if (root == null)
		{
			return null;
		}
		return this.control.GetAbility(root.guid);
	}

	// Token: 0x060005C4 RID: 1476 RVA: 0x0002AC24 File Offset: 0x00028E24
	public bool IsOnCooldown(PlayerAbilityType abilityType)
	{
		Ability ability = this.GetAbility(abilityType);
		return ability != null && ability.IsOnCooldown;
	}

	// Token: 0x060005C5 RID: 1477 RVA: 0x0002AC44 File Offset: 0x00028E44
	private AbilityTree GetCurrentTree(PlayerAbilityType abilityType)
	{
		if (abilityType == PlayerAbilityType.Primary && this.primary != null)
		{
			return this.primary;
		}
		if (abilityType == PlayerAbilityType.Secondary && this.secondary != null)
		{
			return this.secondary;
		}
		if (abilityType == PlayerAbilityType.Utility && this.utility != null)
		{
			return this.utility;
		}
		if (abilityType == PlayerAbilityType.Movement && this.movement != null)
		{
			return this.movement;
		}
		if (abilityType == PlayerAbilityType.Ghost && this.ghost != null)
		{
			return this.ghost;
		}
		return null;
	}

	// Token: 0x060005C6 RID: 1478 RVA: 0x0002ACD0 File Offset: 0x00028ED0
	public Ability GetAbility(PlayerAbilityType abilityType)
	{
		AbilityTree currentTree = this.GetCurrentTree(abilityType);
		if (currentTree != null)
		{
			return this.control.GetAbility(currentTree.RootNode.guid);
		}
		return null;
	}

	// Token: 0x060005C7 RID: 1479 RVA: 0x0002AD08 File Offset: 0x00028F08
	public void ResetTempAbility(PlayerAbilityType abilityType)
	{
		AbilityTree abilityTree;
		this.cachedAbilities.TryGetValue(abilityType, out abilityTree);
		UnityEngine.Debug.Log(string.Format("Trying to reset temp {0} spell -> {1} | {2}", abilityType, ((abilityTree != null) ? abilityTree.Root.Name : null) ?? "NONE", abilityTree != this.GetAbility(abilityType).AbilityTree));
		AbilityTree abilityTree2;
		if (this.cachedAbilities.TryGetValue(abilityType, out abilityTree2) && abilityTree2 != this.GetAbility(abilityType).AbilityTree)
		{
			this.LoadAbility(abilityType, abilityTree2.RootNode.guid, false);
		}
	}

	// Token: 0x060005C8 RID: 1480 RVA: 0x0002ADA4 File Offset: 0x00028FA4
	public void LoadAbility(PlayerAbilityType abilityType, string guid, bool isTemporary = false)
	{
		AbilityTree currentTree = this.GetCurrentTree(abilityType);
		if (currentTree != null)
		{
			if (currentTree.RootNode.guid == guid)
			{
				return;
			}
			this.TryReleaseAbility(currentTree.Root, false);
			this.control.RemoveAbility(currentTree);
		}
		AbilityTree ability = GraphDB.GetAbility(guid);
		Ability ability2 = this.control.AddAbility(ability);
		if (!isTemporary)
		{
			this.cachedAbilities[abilityType] = ability;
		}
		if (abilityType == PlayerAbilityType.Primary)
		{
			this.primary = ability;
		}
		if (abilityType == PlayerAbilityType.Secondary)
		{
			this.secondary = ability;
		}
		if (abilityType == PlayerAbilityType.Utility)
		{
			this.utility = ability;
			ability2.SetFullCooldown(this.control, new EffectProperties(this.control));
		}
		if (abilityType == PlayerAbilityType.Movement)
		{
			this.movement = ability;
		}
		if (abilityType == PlayerAbilityType.Ghost)
		{
			this.ghost = ability;
		}
		if (this.control.IsMine && LibraryManager.instance != null)
		{
			Action onPlayerLoadoutChanged = LibraryManager.instance.OnPlayerLoadoutChanged;
			if (onPlayerLoadoutChanged != null)
			{
				onPlayerLoadoutChanged();
			}
		}
		this.control.Net.SendAbilities();
	}

	// Token: 0x060005C9 RID: 1481 RVA: 0x0002AEA0 File Offset: 0x000290A0
	public void TryResetEphemerals(PlayerAbilityInfo.ResetType reason)
	{
		List<AbilityTree> list = new List<AbilityTree>();
		foreach (Ability ability in this.control.Abilities)
		{
			AbilityRootNode.UsageProps usage = ability.rootNode.Usage;
			if (usage.IsPlayerAbility && usage.AbilityMetadata.Ephemeral && usage.AbilityMetadata.ResetOn == reason)
			{
				list.Add(usage.AbilityMetadata.OriginalAbility);
			}
		}
		foreach (AbilityTree abilityTree in list)
		{
			this.LoadAbility(abilityTree.Root.PlrAbilityType, abilityTree.Root.guid, false);
		}
	}

	// Token: 0x060005CA RID: 1482 RVA: 0x0002AF8C File Offset: 0x0002918C
	public void SetCore(AugmentTree coreTree)
	{
		if (this.core != coreTree)
		{
			this.core.Root.TryTrigger(this.control, EventTrigger.ThisRemoved, new EffectProperties(this.control), 1f);
		}
		this.core = coreTree;
		PlayerDB.CoreDisplay coreDisplay = PlayerDB.GetCore(this.core);
		this.LoadAbility(PlayerAbilityType.Utility, coreDisplay.Ability.Root.guid, false);
		this.control.Display.UpdateCoreDisplay();
		Action<MagicColor> action = this.coreChanged;
		if (action != null)
		{
			action(this.core.Root.magicColor);
		}
		this.core.Root.TryTrigger(this.control, EventTrigger.ThisChosen, new EffectProperties(this.control), 1f);
		this.control.UpdateTalents();
		this.control.Mana.ResetMana();
		if (this.control.IsMine)
		{
			Shader.SetGlobalColor("_EnemyHitColor", coreDisplay.EnemyHitColor);
			if (LibraryManager.instance != null)
			{
				Action onPlayerLoadoutChanged = LibraryManager.instance.OnPlayerLoadoutChanged;
				if (onPlayerLoadoutChanged == null)
				{
					return;
				}
				onPlayerLoadoutChanged();
			}
		}
	}

	// Token: 0x060005CB RID: 1483 RVA: 0x0002B0AC File Offset: 0x000292AC
	public void LoadCore(string guid)
	{
		this.core = GraphDB.GetAugment(guid);
		this.control.Display.UpdateCoreDisplay();
		Action<MagicColor> action = this.coreChanged;
		if (action == null)
		{
			return;
		}
		action(this.core.Root.magicColor);
	}

	// Token: 0x060005CC RID: 1484 RVA: 0x0002B0EA File Offset: 0x000292EA
	private void OnDestroy()
	{
		if (this.abilityCastFailed != null)
		{
			this.abilityCastFailed = (Action<PlayerAbilityType, CastFailedReason, int>)Delegate.Remove(this.abilityCastFailed, new Action<PlayerAbilityType, CastFailedReason, int>(Crosshair.instance.CastFailed));
		}
	}

	// Token: 0x060005CD RID: 1485 RVA: 0x0002B11A File Offset: 0x0002931A
	public PlayerActions()
	{
	}

	// Token: 0x040004B4 RID: 1204
	private PlayerControl control;

	// Token: 0x040004B5 RID: 1205
	[NonSerialized]
	public PlayerInput Input;

	// Token: 0x040004B6 RID: 1206
	private PlayerDisplay display;

	// Token: 0x040004B7 RID: 1207
	private PlayerNetwork sync;

	// Token: 0x040004B8 RID: 1208
	private PlayerMana mana;

	// Token: 0x040004B9 RID: 1209
	public Vector2 CurInput;

	// Token: 0x040004BA RID: 1210
	public AugmentTree core;

	// Token: 0x040004BB RID: 1211
	public AbilityTree primary;

	// Token: 0x040004BC RID: 1212
	public AbilityTree secondary;

	// Token: 0x040004BD RID: 1213
	public AbilityTree utility;

	// Token: 0x040004BE RID: 1214
	public AbilityTree movement;

	// Token: 0x040004BF RID: 1215
	public AbilityTree ghost;

	// Token: 0x040004C0 RID: 1216
	private Dictionary<PlayerAbilityType, AbilityTree> cachedAbilities = new Dictionary<PlayerAbilityType, AbilityTree>();

	// Token: 0x040004C1 RID: 1217
	public PlayerPing PingController;

	// Token: 0x040004C2 RID: 1218
	private float pingTimer;

	// Token: 0x040004C3 RID: 1219
	public Action<int, Vector3, Vector3, EffectProperties> abilityActivated;

	// Token: 0x040004C4 RID: 1220
	public Action<int, Vector3, Vector3, EffectProperties> abilityReleased;

	// Token: 0x040004C5 RID: 1221
	public Action<PlayerAbilityType, CastFailedReason, int> abilityCastFailed;

	// Token: 0x040004C6 RID: 1222
	public Action<PlayerAbilityType, Dictionary<MagicColor, int>> abilityCastSucceeded;

	// Token: 0x040004C7 RID: 1223
	public Action<MagicColor> coreChanged;

	// Token: 0x040004C8 RID: 1224
	private List<Ability> toCancel = new List<Ability>();

	// Token: 0x0200049F RID: 1183
	[CompilerGenerated]
	private sealed class <ResetCooldownFrame>d__33 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x06002226 RID: 8742 RVA: 0x000C5B28 File Offset: 0x000C3D28
		[DebuggerHidden]
		public <ResetCooldownFrame>d__33(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x06002227 RID: 8743 RVA: 0x000C5B37 File Offset: 0x000C3D37
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06002228 RID: 8744 RVA: 0x000C5B3C File Offset: 0x000C3D3C
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			PlayerActions playerActions = this;
			if (num == 0)
			{
				this.<>1__state = -1;
				this.<>2__current = true;
				this.<>1__state = 1;
				return true;
			}
			if (num != 1)
			{
				return false;
			}
			this.<>1__state = -1;
			playerActions.control.ResetCooldown(ability);
			return false;
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06002229 RID: 8745 RVA: 0x000C5B95 File Offset: 0x000C3D95
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x0600222A RID: 8746 RVA: 0x000C5B9D File Offset: 0x000C3D9D
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x0600222B RID: 8747 RVA: 0x000C5BA4 File Offset: 0x000C3DA4
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x040023B5 RID: 9141
		private int <>1__state;

		// Token: 0x040023B6 RID: 9142
		private object <>2__current;

		// Token: 0x040023B7 RID: 9143
		public PlayerActions <>4__this;

		// Token: 0x040023B8 RID: 9144
		public AbilityRootNode ability;
	}
}
