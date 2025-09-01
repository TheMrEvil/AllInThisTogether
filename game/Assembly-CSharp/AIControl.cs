using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Agoston_R.Aim_Assist_Pro.Scripts.AimAssistCode.Target;
using Photon.Pun;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

// Token: 0x02000061 RID: 97
public class AIControl : EntityControl
{
	// Token: 0x1700003A RID: 58
	// (get) Token: 0x060002FE RID: 766 RVA: 0x000192C7 File Offset: 0x000174C7
	public AIMovement Movement
	{
		get
		{
			return this.movement as AIMovement;
		}
	}

	// Token: 0x1700003B RID: 59
	// (get) Token: 0x060002FF RID: 767 RVA: 0x000192D4 File Offset: 0x000174D4
	public AINetworked Net
	{
		get
		{
			AINetworkedLight ainetworkedLight = this.net as AINetworkedLight;
			if (ainetworkedLight != null)
			{
				return ainetworkedLight;
			}
			return this.net as AINetworked;
		}
	}

	// Token: 0x1700003C RID: 60
	// (get) Token: 0x06000300 RID: 768 RVA: 0x000192FD File Offset: 0x000174FD
	public AIAudio Audio
	{
		get
		{
			if (!(this.audio == null))
			{
				return this.audio as AIAudio;
			}
			return null;
		}
	}

	// Token: 0x1700003D RID: 61
	// (get) Token: 0x06000301 RID: 769 RVA: 0x0001931A File Offset: 0x0001751A
	public AIDisplay Display
	{
		get
		{
			if (!(this.display == null))
			{
				return this.display as AIDisplay;
			}
			return null;
		}
	}

	// Token: 0x1700003E RID: 62
	// (get) Token: 0x06000302 RID: 770 RVA: 0x00019337 File Offset: 0x00017537
	public string AIName
	{
		get
		{
			return base.gameObject.name.Replace("(Clone)", "");
		}
	}

	// Token: 0x06000303 RID: 771 RVA: 0x00019354 File Offset: 0x00017554
	public override void Setup()
	{
		base.Setup();
		if (!this.NoLogic)
		{
			this.InitializeRaycastArrays();
		}
		if (this.ActImmediately)
		{
			this.logicT = 0f;
		}
		else
		{
			this.logicT = UnityEngine.Random.Range(0.8f, 1f);
		}
		foreach (string item in this.StartingTags)
		{
			if (!this.Tags.Contains(item))
			{
				this.Tags.Add(item);
			}
		}
		if (this.behaviourTree != null)
		{
			this.ChangeBrain(this.behaviourTree);
		}
		if (this.TeamID == 2)
		{
			if (!this.NoAugments)
			{
				this.AddEnemyAugments();
			}
			GameRecord.EnemySeen(this.AIName);
			if (this.Level == EnemyLevel.Boss)
			{
				AIManager.instance.OnBossSpawned(this);
				GenreWaveNode waveConfig = WaveManager.WaveConfig;
				this.shouldDropBossPage = (!RaidManager.IsInRaid && waveConfig != null && GameplayManager.instance.GameGraph.Root.Waves.IndexOf(waveConfig) != GameplayManager.instance.GameGraph.Root.Waves.Count - 1);
			}
			GameRecord.AIEvent(GameRecord.EventType.Enemy_Spawned, this, "");
		}
		EntityHealth health = this.health;
		health.OnDamageTaken = (Action<DamageInfo>)Delegate.Combine(health.OnDamageTaken, new Action<DamageInfo>(this.OnDamaged));
		EntityHealth health2 = this.health;
		health2.OnDie = (Action<DamageInfo>)Delegate.Combine(health2.OnDie, new Action<DamageInfo>(this.OnDeath));
		EntityHealth health3 = this.health;
		health3.OnRevive = (Action<int>)Delegate.Combine(health3.OnRevive, new Action<int>(delegate(int <p0>)
		{
			if (this.DeathRoutine != null)
			{
				base.StopCoroutine(this.DeathRoutine);
			}
		}));
		if (this.Affectable && this.TeamID == 2 && this.Display.UseDropSpawn && (!RaidManager.IsInRaid || this.Level != EnemyLevel.Boss))
		{
			AIManager instance = AIManager.instance;
			if (instance != null)
			{
				instance.DoSpawnFX(this, this.Movement.GetPosition());
			}
			this.Affectable = false;
			this.Display.SetInvisible();
			float num = 1f;
			if (this.Level == EnemyLevel.Boss)
			{
				num = 1.5f;
			}
			this.logicT = UnityEngine.Random.Range(num - 0.1f, num + 0.1f);
			base.Invoke("ReEnable", num);
			base.Invoke("DoAISpawnEvents", num);
			Collider[] componentsInChildren = base.GetComponentsInChildren<Collider>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].gameObject.AddComponent<AimAssistTarget>();
			}
		}
		else
		{
			base.Invoke("DoAISpawnEvents", 0.05f);
		}
		if (AIControl.AllAI == null)
		{
			AIControl.AllAI = new List<AIControl>();
		}
		AIControl.AllAI.Add(this);
		this.aiEffects = base.gameObject.GetAllComponents<EffectBase>();
		EnemyLevel level = this.Level;
		if (base.IsDead)
		{
			UnityEngine.Debug.Log("AI Spawned as Dead --", base.gameObject);
			this.OnDeath(null);
		}
	}

	// Token: 0x06000304 RID: 772 RVA: 0x0001966C File Offset: 0x0001786C
	internal override void OnSpawned()
	{
	}

	// Token: 0x06000305 RID: 773 RVA: 0x0001966E File Offset: 0x0001786E
	public void SetPetOwner(int UnitID)
	{
		this.PetOwnerID = UnitID;
		this.SetAllyTargetByViewID(UnitID);
	}

	// Token: 0x06000306 RID: 774 RVA: 0x0001967E File Offset: 0x0001787E
	internal void DoAISpawnEvents()
	{
		this.hasSpawned = true;
		this.TriggerSnippets(EventTrigger.Spawned, new EffectProperties(this), 1f);
		this.TryTickBrain();
	}

	// Token: 0x06000307 RID: 775 RVA: 0x000196A0 File Offset: 0x000178A0
	private void ReEnable()
	{
		this.Affectable = true;
		this.Display.SetVisible();
	}

	// Token: 0x06000308 RID: 776 RVA: 0x000196B4 File Offset: 0x000178B4
	private void AddEnemyAugments()
	{
		foreach (KeyValuePair<AugmentRootNode, int> keyValuePair in AIManager.GlobalEnemyMods.trees)
		{
			AugmentRootNode augmentRootNode;
			int num;
			keyValuePair.Deconstruct(out augmentRootNode, out num);
			AugmentRootNode augmentRootNode2 = augmentRootNode;
			int count = num;
			if (augmentRootNode2.ApplyTo.HasFlag(this.Level) && (augmentRootNode2.ApplyType == EnemyType.Any || this.EnemyIsType(augmentRootNode2.ApplyType)))
			{
				this.AddAugment(augmentRootNode2, count);
			}
		}
		foreach (KeyValuePair<AugmentRootNode, int> keyValuePair in InkManager.PurchasedMods.trees)
		{
			AugmentRootNode augmentRootNode;
			int num;
			keyValuePair.Deconstruct(out augmentRootNode, out num);
			AugmentRootNode augmentRootNode3 = augmentRootNode;
			int count2 = num;
			if (augmentRootNode3.ApplyToEnemies && augmentRootNode3.ApplyTo.HasFlag(this.Level) && (augmentRootNode3.ApplyType == EnemyType.Any || this.EnemyIsType(augmentRootNode3.ApplyType)))
			{
				this.AddAugment(augmentRootNode3, count2);
			}
		}
		if (GameplayManager.instance != null)
		{
			foreach (KeyValuePair<AugmentRootNode, int> keyValuePair in GameplayManager.instance.GenreBindings.trees)
			{
				AugmentRootNode augmentRootNode;
				int num;
				keyValuePair.Deconstruct(out augmentRootNode, out num);
				AugmentRootNode augmentRootNode4 = augmentRootNode;
				int count3 = num;
				if (augmentRootNode4.ApplyToEnemies && augmentRootNode4.ApplyTo.HasFlag(this.Level) && (augmentRootNode4.ApplyType == EnemyType.Any || this.EnemyIsType(augmentRootNode4.ApplyType)))
				{
					this.AddAugment(augmentRootNode4, count3);
				}
			}
		}
		if (GameplayManager.instance != null)
		{
			foreach (KeyValuePair<AugmentRootNode, int> keyValuePair in GameplayManager.instance.GetGameAugments(ModType.Enemy).trees)
			{
				AugmentRootNode augmentRootNode;
				int num;
				keyValuePair.Deconstruct(out augmentRootNode, out num);
				AugmentRootNode augmentRootNode5 = augmentRootNode;
				int count4 = num;
				if (augmentRootNode5.ApplyTo.HasFlag(this.Level) && (augmentRootNode5.ApplyType == EnemyType.Any || this.EnemyIsType(augmentRootNode5.ApplyType)))
				{
					this.AddAugment(augmentRootNode5, count4);
				}
			}
		}
		if (RaidManager.IsInRaid)
		{
			RaidDB.Encounter encounter = RaidDB.GetEncounter(RaidManager.instance.CurrentRaid, RaidManager.instance.CurrentEncounter);
			foreach (AugmentTree augmentTree in ((RaidManager.instance.Difficulty == RaidDB.Difficulty.Normal) ? encounter.NormalEnemyAugments : encounter.HardEnemyAugments))
			{
				AugmentRootNode root = augmentTree.Root;
				if (root.ApplyTo.HasFlag(this.Level) && (root.ApplyType == EnemyType.Any || this.EnemyIsType(root.ApplyType)))
				{
					this.AddAugment(root, 1);
				}
			}
		}
	}

	// Token: 0x06000309 RID: 777 RVA: 0x00019A00 File Offset: 0x00017C00
	public override void Update()
	{
		if (base.IsDead)
		{
			return;
		}
		this.CheckShouldCleanup();
		this.UpdateEvents();
		if (this.logicT > 0f)
		{
			this.logicT -= Time.deltaTime;
		}
		if (Mathf.Abs(base.transform.position.y) > 256f || float.IsNaN(base.transform.position.y))
		{
			try
			{
				base.transform.position = Vector3.zero;
			}
			catch (Exception)
			{
			}
			if (Mathf.Abs(base.transform.position.y) > 256f || float.IsNaN(base.transform.position.y))
			{
				this.health.DebugKill();
			}
		}
		base.Update();
	}

	// Token: 0x0600030A RID: 778 RVA: 0x00019AE0 File Offset: 0x00017CE0
	public void DebugButton()
	{
	}

	// Token: 0x0600030B RID: 779 RVA: 0x00019AE4 File Offset: 0x00017CE4
	public bool TryTickBrain()
	{
		if (this.runtimeTree == null || PausePanel.IsGamePaused || this.NoLogic)
		{
			return false;
		}
		if (RaidManager.IsInRaid && !RaidManager.IsEncounterStarted && this.TeamID != 1)
		{
			return false;
		}
		if (PhotonNetwork.InRoom && !this.net.view.IsMine)
		{
			return false;
		}
		if (this.logicT > 0f)
		{
			return false;
		}
		try
		{
			this.UpdateVisibleEntities();
			this.runtimeTree.DoUpdate(this);
		}
		catch (Exception ex)
		{
			if (!this.loggedLogicFailure)
			{
				string str = "AI Logic Failure: ";
				string ainame = this.AIName;
				string str2 = " - ";
				Exception ex2 = ex;
				UnityEngine.Debug.LogError(str + ainame + str2 + ((ex2 != null) ? ex2.ToString() : null));
				this.loggedLogicFailure = true;
			}
		}
		this.PostBrainLogic();
		return true;
	}

	// Token: 0x0600030C RID: 780 RVA: 0x00019BBC File Offset: 0x00017DBC
	private void TryBrainEvents(EventTrigger trigger)
	{
		if (this.runtimeTree == null)
		{
			return;
		}
		if (PhotonNetwork.InRoom && !this.net.view.IsMine)
		{
			return;
		}
		if (this.runtimeTree.TriggerEvents(trigger, this))
		{
			this.PostBrainLogic();
		}
	}

	// Token: 0x0600030D RID: 781 RVA: 0x00019BFC File Offset: 0x00017DFC
	private void PostBrainLogic()
	{
		this.currentAIState = this.runtimeTree.CurrentState;
		this.logicT = AIRootNode.AITickRate;
		this.LastBrainTime = Time.realtimeSinceStartup;
		this.LogicIndex++;
		if (base.currentTarget != null && base.currentTarget is PlayerControl && this.net.view.IsMine && base.currentTarget.view.OwnerActorNr != this.net.view.OwnerActorNr)
		{
			this.net.view.TransferOwnership(base.currentTarget.view.Owner);
		}
	}

	// Token: 0x0600030E RID: 782 RVA: 0x00019CB0 File Offset: 0x00017EB0
	public void ChangeBrain(AITree newBrain)
	{
		if (newBrain == null)
		{
			return;
		}
		AITree aitree = newBrain.Clone() as AITree;
		aitree.Init();
		this.behaviourTree = aitree;
		this.runtimeTree = (aitree.RootNode as AIRootNode);
		this.runtimeTree.ClearState(null);
		List<AbilityTree> list = this.runtimeTree.CollectActions(null, null);
		this.Abilities.Clear();
		foreach (AbilityTree tree in list)
		{
			base.AddAbility(tree);
		}
	}

	// Token: 0x0600030F RID: 783 RVA: 0x00019D58 File Offset: 0x00017F58
	private void CheckShouldCleanup()
	{
		if (this.AllowedOutsideWave)
		{
			return;
		}
		if (GameplayManager.CurState == GameState.InWave || GameplayManager.CurState == GameState.EXPLICIT_CMD)
		{
			return;
		}
		if (GoalManager.InVignette)
		{
			return;
		}
		if (RaidManager.IsInRaid)
		{
			return;
		}
		if (base.IsDead || !this.hasSpawned)
		{
			return;
		}
		this.health.DebugKill();
	}

	// Token: 0x06000310 RID: 784 RVA: 0x00019DAC File Offset: 0x00017FAC
	public void ActivateAbilityLocal(string GUID, Vector3 targetPt, int seed)
	{
		EffectProperties effectProperties = new EffectProperties(this);
		effectProperties.OverrideSeed(seed, 0);
		effectProperties.SeekTarget = ((base.currentTarget == null) ? null : base.currentTarget.gameObject);
		this.TryActivateAbility(GUID, effectProperties);
	}

	// Token: 0x06000311 RID: 785 RVA: 0x00019DF4 File Offset: 0x00017FF4
	public override bool TryActivateAbility(string GUID, EffectProperties props)
	{
		if (!base.TryActivateAbility(GUID, props))
		{
			return false;
		}
		Ability ability = base.GetAbility(GUID);
		ability.TriggerSnippets(EventTrigger.AbilityUsed, this, new EffectProperties(this)
		{
			AbilityType = ability.PlayerAbilityType
		});
		GameRecord.AIEvent(GameRecord.EventType.Enemy_AbilityUsed, this, ability.rootNode.Usage.AIAbilityID);
		return true;
	}

	// Token: 0x06000312 RID: 786 RVA: 0x00019E4B File Offset: 0x0001804B
	public override bool CanUseAbilities()
	{
		return this.Level.HasFlag(EnemyLevel.Boss) || (this.movement.CanInterruptMovement() && base.CanUseAbilities());
	}

	// Token: 0x06000313 RID: 787 RVA: 0x00019E80 File Offset: 0x00018080
	public void ReleaseAbility(string GUID)
	{
		Ability ability = base.GetAbility(GUID);
		this.TryReleaseAbility(GUID, (ability != null) ? ability.properties : null);
	}

	// Token: 0x06000314 RID: 788 RVA: 0x00019EAC File Offset: 0x000180AC
	public void ResetAllCooldowns()
	{
		foreach (Ability ability in this.Abilities)
		{
			ability.ResetCooldownToDefault();
		}
	}

	// Token: 0x06000315 RID: 789 RVA: 0x00019EFC File Offset: 0x000180FC
	public bool EnemyIsType(EnemyType eType)
	{
		if (this.Level.AnyFlagsMatch(EnemyLevel.Boss))
		{
			return this.EnemyType.AnyFlagsMatch(eType) || GameplayManager.HasGameOverride("Boss_WildType");
		}
		if (this.Level.AnyFlagsMatch(EnemyLevel.Elite))
		{
			return this.EnemyType.AnyFlagsMatch(eType) || GameplayManager.HasGameOverride("Elite_WildType");
		}
		return this.EnemyType.AnyFlagsMatch(eType) || GameplayManager.HasGameOverride("Normal_WildType");
	}

	// Token: 0x06000316 RID: 790 RVA: 0x00019F76 File Offset: 0x00018176
	public void AddTag(string tag)
	{
		if (!this.Tags.Contains(tag))
		{
			this.Tags.Add(tag);
		}
		this.Net.SendTags();
	}

	// Token: 0x06000317 RID: 791 RVA: 0x00019F9D File Offset: 0x0001819D
	public void RemoveTag(string tag)
	{
		this.Tags.Remove(tag);
		this.Net.SendTags();
	}

	// Token: 0x06000318 RID: 792 RVA: 0x00019FB7 File Offset: 0x000181B7
	public bool HasTag(string tag)
	{
		return this.Tags.Contains(tag);
	}

	// Token: 0x06000319 RID: 793 RVA: 0x00019FC8 File Offset: 0x000181C8
	public void SetTags(string tagList)
	{
		this.Tags.Clear();
		if (tagList.Length == 0)
		{
			return;
		}
		foreach (string item in tagList.Split('|', StringSplitOptions.None))
		{
			this.Tags.Add(item);
		}
	}

	// Token: 0x0600031A RID: 794 RVA: 0x0001A014 File Offset: 0x00018214
	public string AllTags()
	{
		string text = "";
		foreach (string text2 in this.Tags)
		{
			text = text + text2.ToString() + "|";
		}
		if (text.Length == 0)
		{
			return text;
		}
		return text.Substring(0, text.Length - 1);
	}

	// Token: 0x0600031B RID: 795 RVA: 0x0001A094 File Offset: 0x00018294
	private void UpdateEvents()
	{
		if (base.currentTarget != null)
		{
			base.UpdateEventTime(TimeSince.HadTarget);
		}
		bool flag = false;
		bool flag2 = false;
		bool flag3 = false;
		foreach (EntityControl entityControl in this.InLOS)
		{
			flag |= (entityControl.TeamID != this.TeamID);
			flag2 |= (entityControl == base.currentTarget);
			flag3 |= (entityControl.TeamID == this.TeamID);
		}
		if (flag)
		{
			base.UpdateEventTime(TimeSince.SeenEnemy);
		}
		if (flag2)
		{
			base.UpdateEventTime(TimeSince.SeenTarget);
		}
		if (flag3)
		{
			base.UpdateEventTime(TimeSince.SeenAlly);
		}
	}

	// Token: 0x0600031C RID: 796 RVA: 0x0001A154 File Offset: 0x00018354
	public void SetTarget(EntityControl control)
	{
		bool flag = base.currentTarget != control;
		base.currentTarget = control;
		base.UpdateEventTime(TimeSince.ChangedTarget);
		if (flag)
		{
			this.TriggerSnippets(EventTrigger.Target_Changed, new EffectProperties(this), 1f);
		}
		if (base.currentTarget == null)
		{
			return;
		}
		this.AddTag("HadTarget");
	}

	// Token: 0x0600031D RID: 797 RVA: 0x0001A1AC File Offset: 0x000183AC
	public void SetTargetByViewID(int id)
	{
		if (id < 0)
		{
			bool flag = base.currentTarget != null;
			if (base.currentTarget != null)
			{
				base.currentTarget = null;
			}
			if (flag)
			{
				this.TriggerSnippets(EventTrigger.Target_Changed, new EffectProperties(this), 1f);
			}
			return;
		}
		EntityControl entity = EntityControl.GetEntity(id);
		if (base.currentTarget == entity)
		{
			return;
		}
		this.SetTarget(entity);
	}

	// Token: 0x0600031E RID: 798 RVA: 0x0001A211 File Offset: 0x00018411
	public void SetAllyTarget(EntityControl control)
	{
		base.allyTarget = control;
		base.UpdateEventTime(TimeSince.ChangedAllyTarget);
		if (base.allyTarget == null)
		{
			return;
		}
		this.AddTag("HadAllyTarget");
	}

	// Token: 0x0600031F RID: 799 RVA: 0x0001A23C File Offset: 0x0001843C
	public void SetAllyTargetByViewID(int id)
	{
		if (id < 0)
		{
			if (base.allyTarget != null)
			{
				base.allyTarget = null;
			}
			return;
		}
		EntityControl entity = EntityControl.GetEntity(id);
		if (base.allyTarget == entity)
		{
			return;
		}
		this.SetAllyTarget(entity);
	}

	// Token: 0x06000320 RID: 800 RVA: 0x0001A280 File Offset: 0x00018480
	public bool CanSeeEntity(EntityControl entity)
	{
		if (this.InLOS.Contains(entity))
		{
			return true;
		}
		if (!(this.movement is AIGroundMovement))
		{
			return false;
		}
		NavVisionPoint occupiedPoint = AIVisionGraph.instance.GetOccupiedPoint(this);
		return occupiedPoint != null && occupiedPoint.InView.ContainsKey(entity);
	}

	// Token: 0x06000321 RID: 801 RVA: 0x0001A2CC File Offset: 0x000184CC
	private void InitializeRaycastArrays()
	{
		if (this.commands.IsCreated)
		{
			this.commands.Dispose();
		}
		if (this.results.IsCreated)
		{
			this.results.Dispose();
		}
		if (this.validIndices.IsCreated)
		{
			this.validIndices.Dispose();
		}
		this.commands = new NativeArray<RaycastCommand>(63, Allocator.Persistent, NativeArrayOptions.ClearMemory);
		this.results = new NativeArray<RaycastHit>(63, Allocator.Persistent, NativeArrayOptions.ClearMemory);
		this.validIndices = new NativeList<int>(Allocator.Persistent);
		this.currentCapacity = 63;
		if (FlyingNavmesh.instance != null)
		{
			this.mask = FlyingNavmesh.instance.checkMask;
			return;
		}
		this.mask = LayerMask.GetMask(new string[]
		{
			"StaticLevel"
		});
	}

	// Token: 0x06000322 RID: 802 RVA: 0x0001A394 File Offset: 0x00018594
	private void UpdateVisibleEntities()
	{
		Vector2 vector = new Vector2(0f, this.VisionRange);
		this.validIndices.Clear();
		int num = 0;
		Vector3 position = base.transform.position;
		if (this.display != null && this.display.EyelineLocation != null)
		{
			position = this.display.EyelineLocation.position;
		}
		for (int i = 0; i < EntityControl.AllEntities.Count; i++)
		{
			EntityControl entityControl = EntityControl.AllEntities[i];
			if (!(entityControl == null) && !(entityControl == this) && entityControl.Targetable)
			{
				Vector3 position2 = entityControl.transform.position;
				if (entityControl.display != null && entityControl.display.CenterOfMass != null)
				{
					position2 = entityControl.display.CenterOfMass.position;
				}
				Vector3 vector2 = position2 - position;
				if (vector2.magnitude >= vector.x && vector2.magnitude <= vector.y)
				{
					if (num >= this.currentCapacity)
					{
						this.ResizeRaycastArrays(this.currentCapacity * 2);
					}
					this.commands[num] = new RaycastCommand(position, vector2.normalized, vector2.magnitude * 0.98f, this.mask, 1);
					this.validIndices.Add(i);
					num++;
				}
			}
		}
		if (num < 40 && this.currentCapacity > 63)
		{
			this.ResizeRaycastArrays(63);
		}
		RaycastCommand.ScheduleBatch(this.commands.GetSubArray(0, num), this.results.GetSubArray(0, num), 1, default(JobHandle)).Complete();
		this.InLOS.Clear();
		for (int j = 0; j < num; j++)
		{
			if (this.results[j].collider == null)
			{
				int index = this.validIndices[j];
				this.InLOS.Add(EntityControl.AllEntities[index]);
			}
		}
	}

	// Token: 0x06000323 RID: 803 RVA: 0x0001A5C0 File Offset: 0x000187C0
	private void ResizeRaycastArrays(int newCapacity)
	{
		NativeArray<RaycastCommand> dst = new NativeArray<RaycastCommand>(newCapacity, Allocator.Persistent, NativeArrayOptions.ClearMemory);
		NativeArray<RaycastHit> dst2 = new NativeArray<RaycastHit>(newCapacity, Allocator.Persistent, NativeArrayOptions.ClearMemory);
		NativeArray<RaycastCommand>.Copy(this.commands, dst, Mathf.Min(this.commands.Length, newCapacity));
		NativeArray<RaycastHit>.Copy(this.results, dst2, Mathf.Min(this.results.Length, newCapacity));
		this.commands.Dispose();
		this.results.Dispose();
		this.commands = dst;
		this.results = dst2;
		this.currentCapacity = newCapacity;
	}

	// Token: 0x06000324 RID: 804 RVA: 0x0001A648 File Offset: 0x00018848
	private void DisposeRaycastArrays()
	{
		if (this.commands.IsCreated)
		{
			this.commands.Dispose();
		}
		if (this.results.IsCreated)
		{
			this.results.Dispose();
		}
		if (this.validIndices.IsCreated)
		{
			this.validIndices.Dispose();
		}
	}

	// Token: 0x06000325 RID: 805 RVA: 0x0001A6A0 File Offset: 0x000188A0
	public void TransformInto(GameObject newEntity)
	{
		AIControl component = newEntity.GetComponent<AIControl>();
		AIDisplay component2 = newEntity.GetComponent<AIDisplay>();
		EntityHealth component3 = newEntity.GetComponent<EntityHealth>();
		AIMovement component4 = newEntity.GetComponent<AIMovement>();
		AIAudio component5 = newEntity.GetComponent<AIAudio>();
		if (component == null || component2 == null || component3 == null || component4 == null || component5 == null)
		{
			UnityEngine.Debug.LogError("Entity is missing a required component");
			return;
		}
		this.DisplayName = component.DisplayName;
		this.PointValue = component.PointValue;
		this.StatID = component.StatID;
		this.EnemyType = component.EnemyType;
		this.Level = component.Level;
		if (this.behaviourTree != component.behaviourTree)
		{
			this.ChangeBrain(component.behaviourTree);
		}
		this.health.Transform(component3);
		this.movement = this.Movement.TransformInto(component4);
		this.Audio.Transform(component5);
		this.Display.TransformInto(component2);
		base.view.RefreshRpcMonoBehaviourCache();
		base.StartCoroutine(this.UpdateRPCMethodsDelayed());
	}

	// Token: 0x06000326 RID: 806 RVA: 0x0001A7B4 File Offset: 0x000189B4
	private IEnumerator UpdateRPCMethodsDelayed()
	{
		yield return true;
		base.view.RefreshRpcMonoBehaviourCache();
		yield break;
	}

	// Token: 0x06000327 RID: 807 RVA: 0x0001A7C4 File Offset: 0x000189C4
	public override bool CanTriggerSnippets(EventTrigger trigger, bool includeStatuses = true, float chanceMult = 1f)
	{
		return base.CanTriggerSnippets(trigger, includeStatuses, chanceMult) || (!(this.runtimeTree == null) && (!PhotonNetwork.InRoom || this.net.view.IsMine) && this.runtimeTree.CanTrigger(trigger));
	}

	// Token: 0x06000328 RID: 808 RVA: 0x0001A813 File Offset: 0x00018A13
	public override void TriggerSnippets(EventTrigger trigger, EffectProperties props = null, float chanceMult = 1f)
	{
		base.TriggerSnippets(trigger, props, chanceMult);
		this.TryBrainEvents(trigger);
	}

	// Token: 0x06000329 RID: 809 RVA: 0x0001A825 File Offset: 0x00018A25
	public void TryTriggerEffect(EffectBase effect)
	{
		if (!this.aiEffects.Contains(effect))
		{
			return;
		}
		this.Net.TriggerEffect(this.aiEffects.IndexOf(effect));
	}

	// Token: 0x0600032A RID: 810 RVA: 0x0001A850 File Offset: 0x00018A50
	public void TriggerAIEffect(int index, int playerID)
	{
		if (index < 0 || index >= this.aiEffects.Count)
		{
			return;
		}
		AIDiageticInteraction aidiageticInteraction = this.aiEffects[index] as AIDiageticInteraction;
		if (aidiageticInteraction != null)
		{
			aidiageticInteraction.ActivateFromNetwork(playerID);
		}
	}

	// Token: 0x0600032B RID: 811 RVA: 0x0001A88C File Offset: 0x00018A8C
	public override EffectProperties DamageDone(DamageInfo info)
	{
		EffectProperties result = base.DamageDone(info);
		if (this.PetOwnerID != -1)
		{
			EntityControl entity = EntityControl.GetEntity(this.PetOwnerID);
			if (entity != null && entity == PlayerControl.myInstance)
			{
				HitMarker.Show(info.DamageType, (int)info.TotalAmount, info.AtPoint, info.Depth);
				GameStats.PlayerDamageDone(info);
				DamageInfo damageInfo = info.Copy();
				damageInfo.SnippetChance = 0f;
				damageInfo.AbilityType = PlayerAbilityType.None;
				PlayerControl.myInstance.DamageDone(damageInfo);
			}
		}
		return result;
	}

	// Token: 0x0600032C RID: 812 RVA: 0x0001A914 File Offset: 0x00018B14
	internal void OnDamaged(DamageInfo dmg)
	{
		base.UpdateEventTime(TimeSince.RecievedDamage);
		this.LastDamageTakenPoint = dmg.AtPoint;
		this.LastDamageTaken = dmg.Amount;
	}

	// Token: 0x0600032D RID: 813 RVA: 0x0001A938 File Offset: 0x00018B38
	private void OnDeath(DamageInfo dmg)
	{
		if (this.DeathRoutine != null)
		{
			base.StopCoroutine(this.DeathRoutine);
		}
		this.DeathRoutine = base.StartCoroutine("DeathSequence");
		AIManager.AIDied(this);
		GameRecord.AIEvent(GameRecord.EventType.Enemy_Died, this, "");
		if (this.TeamID == 2)
		{
			PickupManager.instance.TrySpawnPickups(this);
			if (PlayerControl.myInstance != null && this.localPlayerDamaged)
			{
				PlayerControl.myInstance.DamagedEntityKilled(this);
			}
			if (this.Level.HasFlag(EnemyLevel.Elite))
			{
				GoalManager instance = GoalManager.instance;
				if (instance != null)
				{
					instance.EliteReward(this.movement.GetPosition());
				}
				GameplayManager.instance.TriggerWorldAugments(EventTrigger.EliteDied);
				return;
			}
			if (this.Level.HasFlag(EnemyLevel.Boss))
			{
				if (RaidManager.IsInRaid)
				{
					RaidManager.instance.BossDied(this);
					return;
				}
				if (this.shouldDropBossPage && AIManager.GetAliveBoss() == null)
				{
					GoalManager instance2 = GoalManager.instance;
					if (instance2 == null)
					{
						return;
					}
					instance2.TryMidgameBossReward(this.movement.GetPosition());
				}
			}
		}
	}

	// Token: 0x0600032E RID: 814 RVA: 0x0001AA4F File Offset: 0x00018C4F
	private IEnumerator DeathSequence()
	{
		float t = 0f;
		while (t < 1.5f)
		{
			t += Time.deltaTime;
			yield return true;
		}
		if (!base.IsDead)
		{
			base.StopCoroutine(this.DeathRoutine);
		}
		this.display.Dissolve();
		t = 0f;
		while (t < 1.5f)
		{
			t += Time.deltaTime;
			yield return true;
		}
		this.net.Destroy();
		yield break;
	}

	// Token: 0x0600032F RID: 815 RVA: 0x0001AA5E File Offset: 0x00018C5E
	internal override void OnDestroy()
	{
		base.OnDestroy();
		this.DisposeRaycastArrays();
		AIControl.AllAI.Remove(this);
	}

	// Token: 0x06000330 RID: 816 RVA: 0x0001AA78 File Offset: 0x00018C78
	public void OpenBrainGraph()
	{
	}

	// Token: 0x06000331 RID: 817 RVA: 0x0001AA7C File Offset: 0x00018C7C
	public AIControl()
	{
	}

	// Token: 0x06000332 RID: 818 RVA: 0x0001AAE7 File Offset: 0x00018CE7
	[CompilerGenerated]
	private void <Setup>b__35_0(int <p0>)
	{
		if (this.DeathRoutine != null)
		{
			base.StopCoroutine(this.DeathRoutine);
		}
	}

	// Token: 0x040002F8 RID: 760
	public float VisionRange = 999f;

	// Token: 0x040002F9 RID: 761
	public AITree behaviourTree;

	// Token: 0x040002FA RID: 762
	public bool ActImmediately;

	// Token: 0x040002FB RID: 763
	public bool NoLogic;

	// Token: 0x040002FC RID: 764
	internal AIRootNode runtimeTree;

	// Token: 0x040002FD RID: 765
	public EnemyLevel Level = EnemyLevel.Default;

	// Token: 0x040002FE RID: 766
	public EnemyType EnemyType;

	// Token: 0x040002FF RID: 767
	public bool AllowedOutsideWave;

	// Token: 0x04000300 RID: 768
	public string DisplayName;

	// Token: 0x04000301 RID: 769
	public string StatID;

	// Token: 0x04000302 RID: 770
	public bool NoAugments;

	// Token: 0x04000303 RID: 771
	[Tooltip("Used for calculating genre spawn budgeting")]
	public float PointValue = 1f;

	// Token: 0x04000304 RID: 772
	public List<string> StartingTags = new List<string>();

	// Token: 0x04000305 RID: 773
	private List<EntityControl> InLOS = new List<EntityControl>();

	// Token: 0x04000306 RID: 774
	internal List<string> Tags = new List<string>();

	// Token: 0x04000307 RID: 775
	public AILogicState currentAIState;

	// Token: 0x04000308 RID: 776
	private int LogicIndex;

	// Token: 0x04000309 RID: 777
	private float logicT;

	// Token: 0x0400030A RID: 778
	[NonSerialized]
	public float LastBrainTime;

	// Token: 0x0400030B RID: 779
	[NonSerialized]
	public bool localPlayerDamaged;

	// Token: 0x0400030C RID: 780
	public int PetOwnerID = -1;

	// Token: 0x0400030D RID: 781
	public static List<AIControl> AllAI;

	// Token: 0x0400030E RID: 782
	private bool hasSpawned;

	// Token: 0x0400030F RID: 783
	private bool shouldDropBossPage;

	// Token: 0x04000310 RID: 784
	private List<EffectBase> aiEffects = new List<EffectBase>();

	// Token: 0x04000311 RID: 785
	private bool loggedLogicFailure;

	// Token: 0x04000312 RID: 786
	private const string BOSS_WILD = "Boss_WildType";

	// Token: 0x04000313 RID: 787
	private const string ELITE_WILD = "Elite_WildType";

	// Token: 0x04000314 RID: 788
	private const string NORMAL_WILD = "Normal_WildType";

	// Token: 0x04000315 RID: 789
	private NativeArray<RaycastCommand> commands;

	// Token: 0x04000316 RID: 790
	private NativeArray<RaycastHit> results;

	// Token: 0x04000317 RID: 791
	private NativeList<int> validIndices;

	// Token: 0x04000318 RID: 792
	private const int initialCapacity = 63;

	// Token: 0x04000319 RID: 793
	private const int shrinkThreshold = 40;

	// Token: 0x0400031A RID: 794
	private const int growMultiplier = 2;

	// Token: 0x0400031B RID: 795
	private int currentCapacity = 63;

	// Token: 0x0400031C RID: 796
	private LayerMask mask;

	// Token: 0x0400031D RID: 797
	private Coroutine DeathRoutine;

	// Token: 0x0400031E RID: 798
	public static AIControl.OpenGraphWindow OnOpenGraphWindow;

	// Token: 0x0200047B RID: 1147
	// (Invoke) Token: 0x06002198 RID: 8600
	public delegate void OpenGraphWindow(AITree tree);

	// Token: 0x0200047C RID: 1148
	[Serializable]
	public class AbilityInfo
	{
		// Token: 0x0600219B RID: 8603 RVA: 0x000C34E0 File Offset: 0x000C16E0
		public AbilityInfo(Ability a)
		{
			this.ability = a;
			this.AbilityName = a.rootNode.Name;
			this.CurrentCD = a.currentCD;
			this.MaxCD = a.rootNode.Usage.Cooldown;
		}

		// Token: 0x040022D1 RID: 8913
		[NonSerialized]
		public Ability ability;

		// Token: 0x040022D2 RID: 8914
		public string AbilityName;

		// Token: 0x040022D3 RID: 8915
		[HideInInspector]
		public float MaxCD;

		// Token: 0x040022D4 RID: 8916
		public float CurrentCD;
	}

	// Token: 0x0200047D RID: 1149
	[CompilerGenerated]
	private sealed class <UpdateRPCMethodsDelayed>d__82 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x0600219C RID: 8604 RVA: 0x000C352D File Offset: 0x000C172D
		[DebuggerHidden]
		public <UpdateRPCMethodsDelayed>d__82(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x0600219D RID: 8605 RVA: 0x000C353C File Offset: 0x000C173C
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x0600219E RID: 8606 RVA: 0x000C3540 File Offset: 0x000C1740
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			AIControl aicontrol = this;
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
			aicontrol.view.RefreshRpcMonoBehaviourCache();
			return false;
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x0600219F RID: 8607 RVA: 0x000C3593 File Offset: 0x000C1793
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x060021A0 RID: 8608 RVA: 0x000C359B File Offset: 0x000C179B
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x060021A1 RID: 8609 RVA: 0x000C35A2 File Offset: 0x000C17A2
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x040022D5 RID: 8917
		private int <>1__state;

		// Token: 0x040022D6 RID: 8918
		private object <>2__current;

		// Token: 0x040022D7 RID: 8919
		public AIControl <>4__this;
	}

	// Token: 0x0200047E RID: 1150
	[CompilerGenerated]
	private sealed class <DeathSequence>d__91 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x060021A2 RID: 8610 RVA: 0x000C35AA File Offset: 0x000C17AA
		[DebuggerHidden]
		public <DeathSequence>d__91(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x060021A3 RID: 8611 RVA: 0x000C35B9 File Offset: 0x000C17B9
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x060021A4 RID: 8612 RVA: 0x000C35BC File Offset: 0x000C17BC
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			AIControl aicontrol = this;
			switch (num)
			{
			case 0:
				this.<>1__state = -1;
				t = 0f;
				break;
			case 1:
				this.<>1__state = -1;
				break;
			case 2:
				this.<>1__state = -1;
				goto IL_CB;
			default:
				return false;
			}
			if (t < 1.5f)
			{
				t += Time.deltaTime;
				this.<>2__current = true;
				this.<>1__state = 1;
				return true;
			}
			if (!aicontrol.IsDead)
			{
				aicontrol.StopCoroutine(aicontrol.DeathRoutine);
			}
			aicontrol.display.Dissolve();
			t = 0f;
			IL_CB:
			if (t >= 1.5f)
			{
				aicontrol.net.Destroy();
				return false;
			}
			t += Time.deltaTime;
			this.<>2__current = true;
			this.<>1__state = 2;
			return true;
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x060021A5 RID: 8613 RVA: 0x000C36AD File Offset: 0x000C18AD
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x060021A6 RID: 8614 RVA: 0x000C36B5 File Offset: 0x000C18B5
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x060021A7 RID: 8615 RVA: 0x000C36BC File Offset: 0x000C18BC
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x040022D8 RID: 8920
		private int <>1__state;

		// Token: 0x040022D9 RID: 8921
		private object <>2__current;

		// Token: 0x040022DA RID: 8922
		public AIControl <>4__this;

		// Token: 0x040022DB RID: 8923
		private float <t>5__2;
	}
}
