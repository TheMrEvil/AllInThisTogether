using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using Photon.Pun;
using UnityEngine;
using UnityEngine.AI;

// Token: 0x02000068 RID: 104
public class AIManager : MonoBehaviourPunCallbacks, IPunObservable
{
	// Token: 0x17000045 RID: 69
	// (get) Token: 0x060003A3 RID: 931 RVA: 0x0001E124 File Offset: 0x0001C324
	public float PointsAlive
	{
		get
		{
			return this.AliveAIPoints(2, false);
		}
	}

	// Token: 0x17000046 RID: 70
	// (get) Token: 0x060003A4 RID: 932 RVA: 0x0001E130 File Offset: 0x0001C330
	private float GroupValue
	{
		get
		{
			float num;
			if (WaveManager.WaveConfig == null || WaveManager.WaveConfig.Spawn == null)
			{
				num = 8f;
			}
			else
			{
				num = (float)WaveManager.WaveConfig.Spawn.GroupPoints * this.Waves.GetPlayerValues(PlayerControl.PlayerCount).GroupSizeMult;
			}
			float num2 = (float)WaveManager.WaveConfig.ProgressRequired();
			float num3 = this.PointsSpawned - this.CurGroupVal;
			if (num2 - num3 - num < num / 1.5f)
			{
				num += Mathf.Max(0f, num2 - num3 - num);
			}
			return num;
		}
	}

	// Token: 0x17000047 RID: 71
	// (get) Token: 0x060003A5 RID: 933 RVA: 0x0001E1C8 File Offset: 0x0001C3C8
	private float NextGroupThresh
	{
		get
		{
			if (WaveManager.WaveConfig == null || WaveManager.WaveConfig.Spawn == null)
			{
				return 3f;
			}
			return (float)WaveManager.WaveConfig.Spawn.NextGrpAt * this.Waves.GetPlayerValues(PlayerControl.PlayerCount).NextGrpMult;
		}
	}

	// Token: 0x17000048 RID: 72
	// (get) Token: 0x060003A6 RID: 934 RVA: 0x0001E220 File Offset: 0x0001C420
	public static int AliveEnemies
	{
		get
		{
			if (AIManager.Enemies == null || AIManager.Enemies.Count == 0)
			{
				return 0;
			}
			int num = 0;
			foreach (EntityControl entityControl in AIManager.Enemies)
			{
				if (!entityControl.IsDead && entityControl.TeamID == 2)
				{
					num++;
				}
			}
			return num;
		}
	}

	// Token: 0x17000049 RID: 73
	// (get) Token: 0x060003A7 RID: 935 RVA: 0x0001E298 File Offset: 0x0001C498
	public static int RemainingForGenre
	{
		get
		{
			if (AIManager.Enemies == null || AIManager.Enemies.Count == 0)
			{
				return 0;
			}
			int num = 0;
			foreach (EntityControl entityControl in AIManager.Enemies)
			{
				AIControl aicontrol = (AIControl)entityControl;
				if (!aicontrol.IsDead && aicontrol.TeamID == 2 && aicontrol.Level != EnemyLevel.Minion)
				{
					num++;
				}
			}
			return num;
		}
	}

	// Token: 0x060003A8 RID: 936 RVA: 0x0001E320 File Offset: 0x0001C520
	private void Awake()
	{
		AIManager.instance = this;
		AIManager.GlobalEnemyMods = new Augments();
		AIManager.AugmentIDs = new List<string>();
		AIManager.Enemies = new List<EntityControl>();
		AIManager.tagsCache = new HashSet<EnemyModTag>();
		this.view = base.GetComponent<PhotonView>();
		MapManager.SceneChanged = (Action)Delegate.Combine(MapManager.SceneChanged, new Action(this.OnSceneChanged));
		int bindingLevel = 0;
		this.SetLayout(this.DB.GetRandomLayout(bindingLevel, null, -1));
		SpawnZone.CurZones = new List<SpawnZone>();
		this.Reset();
	}

	// Token: 0x060003A9 RID: 937 RVA: 0x0001E3B0 File Offset: 0x0001C5B0
	private void Update()
	{
		GenreWaveNode waveConfig = WaveManager.WaveConfig;
		if (waveConfig != null && waveConfig.chapterType == GenreWaveNode.ChapterType.PointTotal)
		{
			WaveManager.instance.GoalProgress = AIManager.instance.PointsSpawned;
			WaveManager.instance.GoalCompletion = WaveManager.instance.GoalProgress - this.AliveAIPoints(2, false);
		}
		this.ResetCache();
		AIManager.tagsCache.Clear();
		this.UpdateAIBrains();
		if (!PhotonNetwork.InRoom || !PhotonNetwork.IsMasterClient)
		{
			return;
		}
		if (!RaidManager.IsInRaid)
		{
			GenreWaveNode waveConfig2 = WaveManager.WaveConfig;
			if ((waveConfig2 != null && waveConfig2.chapterType == GenreWaveNode.ChapterType.Boss) || AIManager.GetBoss() != null)
			{
				this.UpdateBossSpawning();
				return;
			}
			if (this.InWave && GameplayManager.IsInGame)
			{
				this.SpawnUpdate();
			}
		}
	}

	// Token: 0x060003AA RID: 938 RVA: 0x0001E470 File Offset: 0x0001C670
	private void UpdateAIBrains()
	{
		if (AIControl.AllAI == null)
		{
			AIControl.AllAI = new List<AIControl>();
		}
		int num = 1;
		int num2 = 0;
		AIControl.AllAI.Sort((AIControl x, AIControl y) => x.LastBrainTime.CompareTo(y.LastBrainTime));
		foreach (AIControl aicontrol in AIControl.AllAI)
		{
			if (!aicontrol.IsDead && !aicontrol.NoLogic)
			{
				try
				{
					if (aicontrol.TryTickBrain())
					{
						num2++;
						if (num2 >= num)
						{
							break;
						}
					}
				}
				catch (Exception)
				{
				}
			}
		}
	}

	// Token: 0x060003AB RID: 939 RVA: 0x0001E530 File Offset: 0x0001C730
	private void OnSceneChanged()
	{
	}

	// Token: 0x060003AC RID: 940 RVA: 0x0001E534 File Offset: 0x0001C734
	public void Reset()
	{
		SpawnZone.RequiredSubGroup = -1;
		this.InWave = false;
		this.CurStatus = AIManager.WaveStatus.Prewave;
		this.WaveSetIndex = 0;
		this.SpawnGroupIndex = 0;
		this.spawnCD = 0f;
		AIManager.GlobalEnemyMods = new Augments();
		AIManager.AugmentIDs.Clear();
		foreach (EntityControl entityControl in AIManager.Enemies)
		{
			if (entityControl.IsMine)
			{
				entityControl.net.Destroy();
			}
		}
		AIManager.Enemies.Clear();
		this.SpawnedElites.Clear();
	}

	// Token: 0x060003AD RID: 941 RVA: 0x0001E5E8 File Offset: 0x0001C7E8
	private void SpawnUpdate()
	{
		this.TrySpawnNextElite();
		GenreWaveNode waveConfig = WaveManager.WaveConfig;
		if (waveConfig != null && waveConfig.chapterType == GenreWaveNode.ChapterType.Boss)
		{
			return;
		}
		switch (this.CurStatus)
		{
		case AIManager.WaveStatus.Prewave:
			this.UpdatePreWave();
			return;
		case AIManager.WaveStatus.PreparingZone:
			this.zoneSpawnDelay -= GameplayManager.deltaTime;
			if (this.zoneSpawnDelay <= 0f)
			{
				this.CurStatus = AIManager.WaveStatus.SpawningGroup;
				return;
			}
			break;
		case AIManager.WaveStatus.SpawningGroup:
			this.UpdateSpawning();
			return;
		case AIManager.WaveStatus.WaitingForGroup:
			this.UpdateWaiting();
			break;
		default:
			return;
		}
	}

	// Token: 0x060003AE RID: 942 RVA: 0x0001E66B File Offset: 0x0001C86B
	private void UpdatePreWave()
	{
		this.PrepareNextZone();
	}

	// Token: 0x060003AF RID: 943 RVA: 0x0001E673 File Offset: 0x0001C873
	private void UpdateBossSpawning()
	{
		this.TrySpawnNextElite();
		this.spawnCD -= GameplayManager.deltaTime;
		if (this.spawnCD < 0f)
		{
			this.spawnCD = this.SpawnBossAllies();
		}
	}

	// Token: 0x060003B0 RID: 944 RVA: 0x0001E6A8 File Offset: 0x0001C8A8
	private void UpdateSpawning()
	{
		this.spawnCD -= GameplayManager.deltaTime;
		if (this.spawnCD < 0f)
		{
			this.spawnCD = UnityEngine.Random.Range(0.5f, 1f);
			this.SpawnChapterEnemies();
		}
		if (this.CurGroupVal >= this.GroupValue || WaveManager.WaveConfig.IsFinished())
		{
			this.CurStatus = AIManager.WaveStatus.WaitingForGroup;
			this.view.RPC("SpawnGroupCompletedNetwork", RpcTarget.All, Array.Empty<object>());
			if (this.WaveSetIndex == 0)
			{
				GoalManager.instance.BeginBonusObjective();
			}
		}
	}

	// Token: 0x060003B1 RID: 945 RVA: 0x0001E739 File Offset: 0x0001C939
	private void UpdateWaiting()
	{
		if (this.AliveAIPoints(2, false) > this.NextGroupThresh || WaveManager.WaveConfig.IsFinished())
		{
			return;
		}
		this.CurGroupVal = 0f;
		this.WaveSetIndex++;
		this.PrepareNextZone();
	}

	// Token: 0x060003B2 RID: 946 RVA: 0x0001E778 File Offset: 0x0001C978
	private void PrepareNextZone()
	{
		if (this.PointsSpawned >= (float)WaveManager.WaveConfig.ProgressRequired())
		{
			return;
		}
		this.CurStatus = AIManager.WaveStatus.PreparingZone;
		SpawnZone.NextZone();
		this.zoneSpawnDelay = 3f;
		int count = SpawnZone.CurZones.Count;
		Vector3 vector = (count > 0) ? SpawnZone.CurZones[0].transform.position : Vector3.zero;
		Vector3 vector2 = (count > 1) ? SpawnZone.CurZones[1].transform.position : Vector3.zero;
		Vector3 vector3 = (count > 2) ? SpawnZone.CurZones[2].transform.position : Vector3.zero;
		Vector3 vector4 = (count > 3) ? SpawnZone.CurZones[3].transform.position : Vector3.zero;
		this.view.RPC("SpawnZoneChosen", RpcTarget.All, new object[]
		{
			count,
			vector,
			vector2,
			vector3,
			vector4
		});
	}

	// Token: 0x060003B3 RID: 947 RVA: 0x0001E885 File Offset: 0x0001CA85
	private void ResetCache()
	{
		this.cachedAlive = -1f;
	}

	// Token: 0x060003B4 RID: 948 RVA: 0x0001E894 File Offset: 0x0001CA94
	public void GetRandomLayout(int seed = -1)
	{
		if (!PhotonNetwork.IsMasterClient)
		{
			return;
		}
		if ((GameStats.GetGlobalStat(GameStats.Stat.TomesWon, 0) < 1 || UnlockManager.GenresUnlocked() <= 0) && !TutorialManager.InTutorial && LibraryManager.instance != null)
		{
			this.SetLayout(LibraryManager.instance.BaseLayout.Layout.ToString());
		}
		else if (GameplayManager.IsChallengeActive && GameplayManager.Challenge.OverrideEnemySet)
		{
			this.SetLayout(GameplayManager.Challenge.AILayout.ToString());
		}
		else
		{
			this.SetLayout(this.DB.GetRandomLayout(BindingsPanel.instance.CurrentBindingLevel, null, seed));
		}
		this.SyncLayout();
	}

	// Token: 0x060003B5 RID: 949 RVA: 0x0001E938 File Offset: 0x0001CB38
	public void OverrideLayout(AILayout layout)
	{
		if (!PhotonNetwork.IsMasterClient)
		{
			return;
		}
		this.SetLayout(layout.ToString());
		this.SyncLayout();
	}

	// Token: 0x060003B6 RID: 950 RVA: 0x0001E954 File Offset: 0x0001CB54
	public void SyncLayout()
	{
		if (!PhotonNetwork.IsMasterClient)
		{
			return;
		}
		if (this.Layout != null)
		{
			this.view.RPC("SetLayoutNetwork", RpcTarget.All, new object[]
			{
				this.LayoutString
			});
		}
	}

	// Token: 0x060003B7 RID: 951 RVA: 0x0001E988 File Offset: 0x0001CB88
	public void ActivateWave(GenreWaveNode WaveNode)
	{
		this.InWave = true;
		this.WaveConfig = WaveNode;
		this.WaveSetIndex = 0;
		this.SpawnGroupIndex = 0;
		this.CurGroupVal = 0f;
		this.PointsSpawned = 0f;
		this.InGoalKilled = 0;
		this.SpawnedElites.Clear();
		this.CurStatus = AIManager.WaveStatus.Prewave;
	}

	// Token: 0x060003B8 RID: 952 RVA: 0x0001E9E0 File Offset: 0x0001CBE0
	public void NextGoalStarted()
	{
		this.PointsSpawned = 0f;
		this.InGoalKilled = 0;
	}

	// Token: 0x060003B9 RID: 953 RVA: 0x0001E9F4 File Offset: 0x0001CBF4
	public void EndWave()
	{
		this.InWave = false;
		this.SpawnedElites.Clear();
	}

	// Token: 0x060003BA RID: 954 RVA: 0x0001EA08 File Offset: 0x0001CC08
	private void TrySpawnNextElite()
	{
		if (WaveManager.WaveConfig == null)
		{
			WaveManager.instance.SetWaveConfig(0);
		}
		if (WaveManager.WaveConfig.Spawn == null || GameplayManager.instance.CurrentState != GameState.InWave || WaveManager.WaveConfig.ShoudlEndWave() || WaveManager.WaveConfig.Event != GenreWaveNode.EventType.Elite)
		{
			return;
		}
		float num = this.PointsSpawned / (float)WaveManager.WaveConfig.ProgressRequired();
		foreach (GenreSpawnNode.EliteSpawn eliteSpawn in WaveManager.WaveConfig.Elites)
		{
			if (!this.SpawnedElites.Contains(eliteSpawn) && eliteSpawn.At <= num)
			{
				this.SpawnedElites.Add(eliteSpawn);
				int number = eliteSpawn.Index + WaveManager.instance.AppendixLevel;
				AIManager.SpawnAIWorld(AIData.AIDetails.GetResourcePath(this.Layout.GetElite(number)), EnemyLevel.Elite, true);
				this.view.RPC("EliteSpawnedNetwork", RpcTarget.All, Array.Empty<object>());
				break;
			}
		}
	}

	// Token: 0x060003BB RID: 955 RVA: 0x0001EB24 File Offset: 0x0001CD24
	private bool SpawnChapterEnemies()
	{
		GenreSpawnNode spawn = WaveManager.WaveConfig.Spawn;
		if (spawn == null || GameplayManager.instance.CurrentState != GameState.InWave || WaveManager.WaveConfig.IsFinished() || this.CurGroupVal >= this.GroupValue)
		{
			return false;
		}
		float num = this.GroupValue - this.CurGroupVal;
		List<AIData.AIDetails> group = spawn.GetGroup(this.SpawnGroupIndex, num, this.Layout);
		if (group != null && group.Count > 0)
		{
			return this.SpawnGroup(group);
		}
		this.spawnCD = UnityEngine.Random.Range(0.1f, 0.5f);
		return this.SpawnFillEnemy(spawn, num);
	}

	// Token: 0x060003BC RID: 956 RVA: 0x0001EBC4 File Offset: 0x0001CDC4
	private float SpawnBossAllies()
	{
		if (WaveManager.WaveConfig == null)
		{
			WaveManager.instance.SetWaveConfig(0);
		}
		GenreSpawnNode spawn = WaveManager.WaveConfig.Spawn;
		if (spawn == null || WaveManager.WaveConfig.IsFinished())
		{
			return 0f;
		}
		AIControl boss = AIManager.GetBoss();
		if (boss == null || boss.IsDead)
		{
			return 5f;
		}
		if (this.PointsAlive > (float)spawn.NextGrpAt * this.Waves.GetPlayerValues(PlayerControl.PlayerCount).NextGrpMult)
		{
			return 1f;
		}
		float num = this.Waves.GetPlayerValues(PlayerControl.PlayerCount).BossGroupTimerMult * (1f + 0.025f * (float)GameplayManager.BindingLevel);
		List<AIData.AIDetails> group = spawn.GetGroup(this.SpawnGroupIndex, (float)spawn.GroupPoints * this.Waves.GetPlayerValues(PlayerControl.PlayerCount).GroupSizeMult, this.Layout);
		if (group != null && group.Count > 0 && this.SpawnGroup(group))
		{
			return UnityEngine.Random.Range(12f, 20f) * num;
		}
		this.SpawnFillEnemy(spawn, 6f);
		return UnityEngine.Random.Range(1.5f, 2.25f) * num;
	}

	// Token: 0x060003BD RID: 957 RVA: 0x0001ECF8 File Offset: 0x0001CEF8
	private bool SpawnGroup(List<AIData.AIDetails> group)
	{
		AIData.AIDetails aidetails = group[0];
		SpawnPoint enemySpawnPoint = AIManager.GetEnemySpawnPoint(aidetails.AIType, aidetails.Reference.GetComponent<AIControl>().Level, true);
		base.StartCoroutine(this.GroupSpawnSequence(group, enemySpawnPoint));
		this.SpawnGroupIndex++;
		return true;
	}

	// Token: 0x060003BE RID: 958 RVA: 0x0001ED48 File Offset: 0x0001CF48
	private IEnumerator GroupSpawnSequence(List<AIData.AIDetails> toSpawn, SpawnPoint point)
	{
		Vector3 pos = point.transform.position;
		Vector3 forward = point.transform.forward;
		float range = 5f;
		foreach (AIData.AIDetails aidetails in toSpawn)
		{
			Vector3 b = new Vector3(UnityEngine.Random.Range(range, -range), 0f, UnityEngine.Random.Range(range, -range));
			AIManager.SpawnAIExplicit(aidetails.ResourcePath, pos + b, forward);
			this.PointsSpawned += aidetails.PointValue;
			this.CurGroupVal += aidetails.PointValue;
			yield return new WaitForSeconds(UnityEngine.Random.Range(0.15f, 0.3f));
		}
		List<AIData.AIDetails>.Enumerator enumerator = default(List<AIData.AIDetails>.Enumerator);
		yield break;
		yield break;
	}

	// Token: 0x060003BF RID: 959 RVA: 0x0001ED68 File Offset: 0x0001CF68
	private bool SpawnFillEnemy(GenreSpawnNode spawnConfig, float budget)
	{
		List<AILayout.GenreEnemy> enemies = spawnConfig.FillEnemies.Enemies;
		if (enemies.Count == 0)
		{
			return false;
		}
		List<AIData.AIDetails> options = this.DB.GetDetails(enemies, this.Layout);
		if (GameplayManager.HasGameOverride("No_Fodder"))
		{
			options = this.DB.GetNormalEnemies(this.Layout);
			budget *= 4f;
		}
		AIData.AIDetails aidetails = this.DB.ChooseEnemy(options, budget, true);
		if (aidetails == null)
		{
			return false;
		}
		this.PointsSpawned += aidetails.PointValue;
		this.CurGroupVal += aidetails.PointValue;
		AIManager.SpawnEnemy(aidetails, true);
		return true;
	}

	// Token: 0x060003C0 RID: 960 RVA: 0x0001EE08 File Offset: 0x0001D008
	public static EntityControl SpawnEnemy(AIData.AIDetails Enemy, bool useSpawnZone)
	{
		SpawnPoint enemySpawnPoint = AIManager.GetEnemySpawnPoint(Enemy.AIType, Enemy.Reference.GetComponent<AIControl>().Level, useSpawnZone);
		if (enemySpawnPoint == null)
		{
			UnityEngine.Debug.LogError("No valid spawn point found for " + Enemy.ResourcePath);
			return null;
		}
		enemySpawnPoint.Spawn();
		return AIManager.SpawnAIExplicit(Enemy.ResourcePath, enemySpawnPoint.transform.position, enemySpawnPoint.transform.forward);
	}

	// Token: 0x060003C1 RID: 961 RVA: 0x0001EE7C File Offset: 0x0001D07C
	public static EntityControl SpawnAIWorld(string ID, EnemyLevel level, bool useSpawnZone)
	{
		SpawnPoint enemySpawnPoint = AIManager.GetEnemySpawnPoint(SpawnType.AI_Ground, level, useSpawnZone);
		if (enemySpawnPoint == null)
		{
			UnityEngine.Debug.LogError("No valid spawn point found for " + ID);
			return null;
		}
		enemySpawnPoint.Spawn();
		return AIManager.SpawnAIExplicit(ID, enemySpawnPoint.transform.position, enemySpawnPoint.transform.forward);
	}

	// Token: 0x060003C2 RID: 962 RVA: 0x0001EED0 File Offset: 0x0001D0D0
	public static EntityControl SpawnAIExplicit(string ID, Vector3 position, Vector3 forward)
	{
		GameObject gameObject = null;
		try
		{
			if (PhotonNetwork.IsMasterClient)
			{
				gameObject = PhotonNetwork.InstantiateRoomObject("AI_Entities/" + ID, position, Quaternion.identity, 0, null);
			}
			else
			{
				gameObject = PhotonNetwork.Instantiate("AI_Entities/" + ID, position, Quaternion.identity, 0, null);
			}
		}
		catch (Exception ex)
		{
			string str = "Failed to Spawn AI: ";
			string str2 = " - ";
			Exception ex2 = ex;
			UnityEngine.Debug.LogError(str + ID + str2 + ((ex2 != null) ? ex2.ToString() : null));
		}
		if (gameObject == null)
		{
			return null;
		}
		EntityControl component = gameObject.GetComponent<EntityControl>();
		component.movement.SetForward(forward, true);
		return component;
	}

	// Token: 0x060003C3 RID: 963 RVA: 0x0001EF70 File Offset: 0x0001D170
	private static SpawnPoint GetEnemySpawnPoint(SpawnType sType, EnemyLevel level, bool useSpawnZone)
	{
		List<SpawnPoint> list = new List<SpawnPoint>();
		if (!useSpawnZone)
		{
			list = SpawnPoint.GetValidSpawns(sType, level);
			if (list.Count == 0)
			{
				list = SpawnPoint.GetAllSpawns(sType, EnemyLevel.None);
			}
		}
		else
		{
			if (!SpawnZone.HasSpawnZone)
			{
				SpawnZone.NextZone();
			}
			list = SpawnZone.GetCurrentValidSpawns(sType, level);
		}
		List<SpawnPoint> list2 = new List<SpawnPoint>();
		foreach (SpawnPoint spawnPoint in list)
		{
			if (Time.realtimeSinceStartup - spawnPoint.LastSpawnedTime < 0.25f)
			{
				list2.Add(spawnPoint);
			}
		}
		if (list2.Count < list.Count)
		{
			list.Remove(list2);
		}
		if (list.Count == 0)
		{
			return null;
		}
		return list[UnityEngine.Random.Range(0, list.Count)];
	}

	// Token: 0x060003C4 RID: 964 RVA: 0x0001F040 File Offset: 0x0001D240
	public void DoSpawnFX(AIControl control, Vector3 pos)
	{
		if (control.Level == EnemyLevel.Boss)
		{
			UnityEngine.Object.Instantiate<GameObject>(this.BossSpawnFX, pos + Vector3.up * 0.1f, this.SpawnFX.transform.rotation);
			AudioManager.PlayClipAtPoint(this.BossSpawnSFX, pos, 1f, 1f, 0.9f, 25f, 250f);
			UnityMainThreadDispatcher.Instance().Invoke(delegate
			{
				PostFXManager.instance.GreyscalePulse(pos);
			}, 1.25f);
			return;
		}
		ActionPool.SpawnObject(this.SpawnFX, pos + Vector3.up * 0.1f, this.SpawnFX.transform.rotation);
		AudioClip randomClip = this.SpawnSFX.GetRandomClip(-1);
		AudioManager.PlayClipAtPoint(randomClip, pos, 1f, UnityEngine.Random.Range(0.95f, 1.05f), 1f, 15f, 150f);
		AudioManager.ClipPlayed(randomClip, 0.15f);
	}

	// Token: 0x060003C5 RID: 965 RVA: 0x0001F15D File Offset: 0x0001D35D
	public void OnBossSpawned(AIControl boss)
	{
		this.spawnCD = 6f;
		Progression.SawBoss(boss.DisplayName);
	}

	// Token: 0x060003C6 RID: 966 RVA: 0x0001F175 File Offset: 0x0001D375
	public void ResetAugments()
	{
		if (!PhotonNetwork.IsMasterClient)
		{
			return;
		}
		AIManager.GlobalEnemyMods = new Augments();
		AIManager.AugmentIDs.Clear();
		GameplayManager.instance.SyncMods();
	}

	// Token: 0x060003C7 RID: 967 RVA: 0x0001F19D File Offset: 0x0001D39D
	public void AddAugment(AugmentTree tree)
	{
		if (tree == null)
		{
			return;
		}
		AIManager.GlobalEnemyMods.Add(tree, 1);
		GameplayManager.instance.SyncMods();
		AugmentsPanel.instance.GameAugmentsChanged();
	}

	// Token: 0x060003C8 RID: 968 RVA: 0x0001F1D0 File Offset: 0x0001D3D0
	public void AddAugments(List<AugmentTree> augments, bool addToIDs = false)
	{
		if (augments == null || augments.Count == 0)
		{
			return;
		}
		foreach (AugmentTree t in augments)
		{
			AIManager.GlobalEnemyMods.Add(t, 1);
		}
		if (addToIDs)
		{
			foreach (AugmentTree augmentTree in augments)
			{
				AIManager.AugmentIDs.Add(augmentTree.ID);
			}
		}
		GameplayManager.instance.SyncMods();
		AugmentsPanel.instance.GameAugmentsChanged();
	}

	// Token: 0x060003C9 RID: 969 RVA: 0x0001F294 File Offset: 0x0001D494
	public static void SyncBossMods()
	{
		foreach (EntityControl entityControl in AIManager.Enemies)
		{
			AIControl aicontrol = (AIControl)entityControl;
			if (aicontrol.Level.HasFlag(EnemyLevel.Boss) || aicontrol.Level.HasFlag(EnemyLevel.Elite))
			{
				aicontrol.Net.SendAugments(null);
			}
		}
	}

	// Token: 0x060003CA RID: 970 RVA: 0x0001F324 File Offset: 0x0001D524
	public void TriggerAugmentsOnAllEnemies(EventTrigger Trigger)
	{
		foreach (EntityControl entityControl in AIManager.Enemies.Clone<EntityControl>())
		{
			AIControl aicontrol = (AIControl)entityControl;
			if (!(aicontrol == null) && !aicontrol.IsDead && aicontrol.TeamID == 2 && aicontrol.IsMine && aicontrol.CanTriggerSnippets(Trigger, true, 1f))
			{
				aicontrol.TriggerSnippets(Trigger, null, 1f);
			}
		}
	}

	// Token: 0x060003CB RID: 971 RVA: 0x0001F3B8 File Offset: 0x0001D5B8
	public void TriggerAugments(EventTrigger Trigger)
	{
		if (!PhotonNetwork.IsMasterClient)
		{
			return;
		}
		int num = UnityEngine.Random.Range(0, 9999999);
		this.view.RPC("TriggerAIAugmentsNetwork", RpcTarget.All, new object[]
		{
			(int)Trigger,
			num
		});
	}

	// Token: 0x060003CC RID: 972 RVA: 0x0001F402 File Offset: 0x0001D602
	private IEnumerator AugmentTriggerOverTime(EventTrigger trigger, int seed)
	{
		new System.Random(seed);
		List<ModSnippetNode> snippets = new List<ModSnippetNode>();
		foreach (KeyValuePair<AugmentRootNode, int> keyValuePair in AIManager.GlobalEnemyMods.trees)
		{
			AugmentRootNode augmentRootNode;
			int num;
			keyValuePair.Deconstruct(out augmentRootNode, out num);
			AugmentRootNode augment = augmentRootNode;
			int count = num;
			if (augment.ApplyToWorld)
			{
				snippets.Clear();
				augment.CollectSnippets(ref snippets, trigger);
				if (snippets.Count != 0)
				{
					for (int i = 0; i < count; i = num + 1)
					{
						yield return new WaitForSeconds(0.5f);
						Vector3 position = Fountain.instance.transform.position;
						EffectProperties effectProperties = new EffectProperties();
						effectProperties.StartLoc = (effectProperties.OutLoc = global::Pose.WorldPoint(position, Vector3.up));
						effectProperties.SourceType = ActionSource.Genre;
						effectProperties.AbilityType = PlayerAbilityType.None;
						effectProperties.IsWorld = true;
						foreach (ModSnippetNode modSnippetNode in snippets)
						{
							modSnippetNode.TryTriggerFromProps(effectProperties, augment);
						}
						num = i;
					}
					augment = null;
				}
			}
		}
		Dictionary<AugmentRootNode, int>.Enumerator enumerator = default(Dictionary<AugmentRootNode, int>.Enumerator);
		foreach (KeyValuePair<AugmentRootNode, int> keyValuePair in GameplayManager.instance.GenreBindings.trees)
		{
			AugmentRootNode augmentRootNode;
			int num;
			keyValuePair.Deconstruct(out augmentRootNode, out num);
			AugmentRootNode augment = augmentRootNode;
			int count = num;
			if (!augment.ApplyToPlayers && !augment.ApplyToEnemies)
			{
				snippets.Clear();
				augment.CollectSnippets(ref snippets, trigger);
				if (snippets.Count != 0)
				{
					for (int i = 0; i < count; i = num + 1)
					{
						yield return new WaitForSeconds(0.5f);
						Vector3 position2 = Fountain.instance.transform.position;
						EffectProperties effectProperties2 = new EffectProperties();
						effectProperties2.StartLoc = global::Pose.WorldPoint(position2, Vector3.up);
						effectProperties2.SourceType = ActionSource.Genre;
						effectProperties2.AbilityType = PlayerAbilityType.None;
						effectProperties2.IsWorld = true;
						foreach (ModSnippetNode modSnippetNode2 in snippets)
						{
							modSnippetNode2.TryTriggerFromProps(effectProperties2, augment);
						}
						num = i;
					}
					augment = null;
				}
			}
		}
		enumerator = default(Dictionary<AugmentRootNode, int>.Enumerator);
		yield break;
		yield break;
	}

	// Token: 0x060003CD RID: 973 RVA: 0x0001F418 File Offset: 0x0001D618
	public static AugmentTree GetEnemyAugment(int index)
	{
		if (index < 0 || index >= AIManager.AugmentIDs.Count)
		{
			return null;
		}
		return GraphDB.GetAugment(AIManager.AugmentIDs[index]);
	}

	// Token: 0x060003CE RID: 974 RVA: 0x0001F440 File Offset: 0x0001D640
	public static List<AugmentRootNode> GetEnemyAugments()
	{
		List<AugmentRootNode> list = new List<AugmentRootNode>();
		if (AIManager.AugmentIDs == null)
		{
			return list;
		}
		foreach (string guid in AIManager.AugmentIDs)
		{
			AugmentTree augment = GraphDB.GetAugment(guid);
			if (augment != null)
			{
				list.Add(augment);
			}
		}
		return list;
	}

	// Token: 0x060003CF RID: 975 RVA: 0x0001F4B8 File Offset: 0x0001D6B8
	public static bool HasEnemyModTag(EnemyModTag tag)
	{
		if (AIManager.tagsCache.Count > 0)
		{
			return AIManager.tagsCache.Contains(tag);
		}
		AIManager.tagsCache.Clear();
		AIManager.CollectModTags();
		return AIManager.tagsCache.Contains(tag);
	}

	// Token: 0x060003D0 RID: 976 RVA: 0x0001F4F0 File Offset: 0x0001D6F0
	private static void CollectModTags()
	{
		foreach (EnemyModTag item in AIManager.GlobalEnemyMods.GetEnemyTags())
		{
			if (!AIManager.tagsCache.Contains(item))
			{
				AIManager.tagsCache.Add(item);
			}
		}
	}

	// Token: 0x060003D1 RID: 977 RVA: 0x0001F55C File Offset: 0x0001D75C
	public static void KillAllEnemies()
	{
		if (AIManager.AliveEnemies <= 0)
		{
			return;
		}
		foreach (EntityControl entityControl in AIManager.Enemies.ToList<EntityControl>())
		{
			if (entityControl != null && !entityControl.IsDead)
			{
				entityControl.health.DebugKill();
			}
		}
	}

	// Token: 0x060003D2 RID: 978 RVA: 0x0001F5D4 File Offset: 0x0001D7D4
	public static void ClearAllEnemies(bool clearOwnedEffects)
	{
		foreach (EntityControl entityControl in AIManager.Enemies.ToList<EntityControl>())
		{
			if (!(entityControl == null))
			{
				AIManager.instance.CancelAI(entityControl as AIControl);
			}
		}
	}

	// Token: 0x060003D3 RID: 979 RVA: 0x0001F640 File Offset: 0x0001D840
	public void TryCancelAI(EffectProperties props, string GraphID)
	{
		bool flag = props.SourceControl != null;
		for (int i = AIManager.Enemies.Count - 1; i >= 0; i--)
		{
			AIControl aicontrol = AIManager.Enemies[i] as AIControl;
			if (aicontrol != null && (!flag || aicontrol.Net.OwnerID == props.SourceControl.ViewID) && !(aicontrol.Net.GraphSource != GraphID))
			{
				this.CancelAI(aicontrol);
			}
		}
	}

	// Token: 0x060003D4 RID: 980 RVA: 0x0001F6BA File Offset: 0x0001D8BA
	public void CancelAI(AIControl ai)
	{
		if (ai == null)
		{
			return;
		}
		UnityEngine.Object.Instantiate<GameObject>(this.AICancelFX, ai.movement.GetPosition(), this.AICancelFX.transform.rotation);
		ai.net.Destroy();
	}

	// Token: 0x060003D5 RID: 981 RVA: 0x0001F6F8 File Offset: 0x0001D8F8
	public static void AIDied(AIControl control)
	{
		if (control.TeamID == 2 && AIManager.instance != null)
		{
			AIManager.instance.InGoalKilled++;
		}
		if (AIManager.IsBoss(control) && AIManager.GetAliveBoss() == null)
		{
			UnityEngine.Object.Instantiate<GameObject>(AIManager.instance.BossDeathFX, control.movement.GetPosition() + Vector3.up * 0.1f, AIManager.instance.BossDeathFX.transform.rotation);
			AudioManager.PlayClipAtPoint(AIManager.instance.BossDeathSFX, control.movement.GetPosition(), 1f, 1f, 0.75f, 25f, 250f);
			GameplayManager.instance.BossDied(control.movement.GetPosition());
		}
	}

	// Token: 0x060003D6 RID: 982 RVA: 0x0001F7D4 File Offset: 0x0001D9D4
	public static void RemoveAllAI()
	{
		for (int i = AIManager.Enemies.Count - 1; i >= 0; i--)
		{
			AIManager.Enemies[i].net.Destroy();
		}
	}

	// Token: 0x060003D7 RID: 983 RVA: 0x0001F810 File Offset: 0x0001DA10
	public static float ModifyBaseDamage(float damage, AIControl source)
	{
		if (AIManager.instance == null)
		{
			return damage;
		}
		EnemyScalingData enemyScaling = AIManager.instance.Waves.EnemyScaling;
		if (!source.Level.AnyFlagsMatch(EnemyLevel.Boss) && !source.Level.AnyFlagsMatch(EnemyLevel.Elite))
		{
			damage *= AIManager.instance.Waves.GetPlayerValues(PlayerControl.PlayerCount).BaseDamageScale;
		}
		damage *= enemyScaling.BindingDamageScaling();
		damage *= enemyScaling.AppendixDamageScale;
		damage *= enemyScaling.GetUnboundScale();
		damage += enemyScaling.AppendixDamageIncrease;
		return damage;
	}

	// Token: 0x060003D8 RID: 984 RVA: 0x0001F8A0 File Offset: 0x0001DAA0
	public static float ModifyBaseCooldown(float cooldown, AIControl source)
	{
		if (AIManager.instance == null)
		{
			return cooldown;
		}
		EnemyScalingData enemyScaling = AIManager.instance.Waves.EnemyScaling;
		cooldown *= enemyScaling.GetCooldownMult();
		return cooldown;
	}

	// Token: 0x060003D9 RID: 985 RVA: 0x0001F8D8 File Offset: 0x0001DAD8
	public static float ModifyBaseSpeed(float speed, AIControl source)
	{
		if (AIManager.instance == null)
		{
			return speed;
		}
		EnemyScalingData enemyScaling = AIManager.instance.Waves.EnemyScaling;
		speed *= enemyScaling.GetSpeedMult();
		return speed;
	}

	// Token: 0x060003DA RID: 986 RVA: 0x0001F910 File Offset: 0x0001DB10
	public float AliveAIPoints(int Team = 2, bool includeElites = false)
	{
		if (this.cachedAlive >= 0f)
		{
			return this.cachedAlive;
		}
		this.cachedAlive = 0f;
		foreach (EntityControl entityControl in AIManager.Enemies)
		{
			AIControl aicontrol = (AIControl)entityControl;
			if (!aicontrol.IsDead && aicontrol.TeamID == Team && (includeElites || !AIManager.IsElite(aicontrol)))
			{
				this.cachedAlive += aicontrol.PointValue;
			}
		}
		return this.cachedAlive;
	}

	// Token: 0x060003DB RID: 987 RVA: 0x0001F9B4 File Offset: 0x0001DBB4
	public float HealthAndShieldMult(AIControl control)
	{
		Wave_PlayerValues playerValues = this.Waves.GetPlayerValues(PlayerControl.PlayerCount);
		float num = this.Waves.EnemyScaling.BaseScaling();
		num += this.Waves.EnemyScaling.BindingHPScaling();
		num *= this.Waves.EnemyScaling.GetUnboundScale();
		if (control.Level.HasFlag(EnemyLevel.Boss))
		{
			if (RaidManager.IsInRaid)
			{
				return playerValues.RaidBossHealthScale * num;
			}
			return playerValues.BossHealthScale * num;
		}
		else
		{
			if (control.Level.HasFlag(EnemyLevel.Elite))
			{
				return playerValues.EliteHealthScale * num;
			}
			return playerValues.BaseHealthScale * num;
		}
	}

	// Token: 0x060003DC RID: 988 RVA: 0x0001FA64 File Offset: 0x0001DC64
	public static bool IsElite(AIControl control)
	{
		return !(control is AITrivialControl) && control.Level == EnemyLevel.Elite;
	}

	// Token: 0x060003DD RID: 989 RVA: 0x0001FA79 File Offset: 0x0001DC79
	public static bool IsBoss(AIControl control)
	{
		return !(control is AITrivialControl) && control.Level == EnemyLevel.Boss;
	}

	// Token: 0x060003DE RID: 990 RVA: 0x0001FA8F File Offset: 0x0001DC8F
	public void DoTPFX(AIControl control, Vector3 from, Vector3 to)
	{
		UnityEngine.Object.Instantiate<GameObject>(this.NetTPFromFX, from, this.NetTPFromFX.transform.rotation);
		UnityEngine.Object.Instantiate<GameObject>(this.NetTPToFX, to, this.NetTPToFX.transform.rotation);
	}

	// Token: 0x060003DF RID: 991 RVA: 0x0001FACC File Offset: 0x0001DCCC
	public static AIControl GetBoss()
	{
		if (AIManager.bossCache != null && !AIManager.bossCache.IsDead)
		{
			return AIManager.bossCache;
		}
		foreach (EntityControl entityControl in EntityControl.AllEntities)
		{
			AIControl aicontrol = entityControl as AIControl;
			if (aicontrol != null && AIManager.IsBoss(aicontrol))
			{
				AIManager.bossCache = aicontrol;
				return aicontrol;
			}
		}
		return null;
	}

	// Token: 0x060003E0 RID: 992 RVA: 0x0001FB54 File Offset: 0x0001DD54
	public static AIControl GetAliveBoss()
	{
		if (AIManager.bossCache != null && !AIManager.bossCache.IsDead)
		{
			return AIManager.bossCache;
		}
		foreach (EntityControl entityControl in EntityControl.AllEntities)
		{
			if (!entityControl.IsDead)
			{
				AIControl aicontrol = entityControl as AIControl;
				if (aicontrol != null && AIManager.IsBoss(aicontrol))
				{
					AIManager.bossCache = aicontrol;
					return aicontrol;
				}
			}
		}
		return null;
	}

	// Token: 0x060003E1 RID: 993 RVA: 0x0001FBE8 File Offset: 0x0001DDE8
	public static string GetNextBossName()
	{
		AIControl boss = AIManager.GetBoss();
		if (boss != null)
		{
			return boss.DisplayName;
		}
		GameObject nextBoss = Logic_World.GetNextBoss();
		if (nextBoss == null)
		{
			return "";
		}
		return nextBoss.GetComponent<AIControl>().DisplayName;
	}

	// Token: 0x060003E2 RID: 994 RVA: 0x0001FC2B File Offset: 0x0001DE2B
	[PunRPC]
	private void SpawnGroupCompletedNetwork()
	{
		GameplayManager.instance.TriggerWorldAugments(EventTrigger.Wave_SpawnGroupCompleted);
		SpawnZone.EndCurrentGreyscale();
	}

	// Token: 0x060003E3 RID: 995 RVA: 0x0001FC40 File Offset: 0x0001DE40
	[PunRPC]
	private void SpawnZoneChosen(int zoneCount, Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p4)
	{
		List<Vector3> list = new List<Vector3>();
		if (zoneCount > 0)
		{
			list.Add(p1);
		}
		if (zoneCount > 1)
		{
			list.Add(p2);
		}
		if (zoneCount > 2)
		{
			list.Add(p3);
		}
		if (zoneCount > 3)
		{
			list.Add(p4);
		}
		SpawnZone.SetZoneFromPosition(list);
		if (this.ZoneSpawnVFX != null)
		{
			foreach (SpawnZone spawnZone in SpawnZone.CurZones)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ZoneSpawnVFX);
				gameObject.transform.position = spawnZone.transform.position + Vector3.up * 15f;
				spawnZone.StartGreyscale();
				gameObject.SetActive(true);
			}
		}
		AudioManager.PlayInterfaceSFX(this.ZoneSpawnSFX, 1f, 1f);
	}

	// Token: 0x060003E4 RID: 996 RVA: 0x0001FD2C File Offset: 0x0001DF2C
	[PunRPC]
	private void TriggerAIAugmentsNetwork(int trigger, int randomSeed)
	{
		base.StartCoroutine(this.AugmentTriggerOverTime((EventTrigger)trigger, randomSeed));
	}

	// Token: 0x060003E5 RID: 997 RVA: 0x0001FD4A File Offset: 0x0001DF4A
	[PunRPC]
	public void EliteSpawnedNetwork()
	{
		AudioManager.PlayInterfaceSFX(this.EliteSpawnSFX, 1f, 0f);
		WaveProgressBar.instance.PulseElite();
		GameplayManager.instance.TriggerWorldAugments(EventTrigger.EliteSpawn);
	}

	// Token: 0x060003E6 RID: 998 RVA: 0x0001FD77 File Offset: 0x0001DF77
	[PunRPC]
	public void SetLayoutNetwork(string ID)
	{
		this.SetLayout(ID);
		this.LayoutString = this.Layout.ToString();
	}

	// Token: 0x060003E7 RID: 999 RVA: 0x0001FD91 File Offset: 0x0001DF91
	public void SetLayout(AILayout layout)
	{
		this.Layout = layout;
		this.LayoutString = this.Layout.ToString();
	}

	// Token: 0x060003E8 RID: 1000 RVA: 0x0001FDAB File Offset: 0x0001DFAB
	private void SetLayout(string layoutString)
	{
		this.LayoutString = layoutString;
		this.Layout = this.DB.GetLayout(layoutString);
	}

	// Token: 0x060003E9 RID: 1001 RVA: 0x0001FDC8 File Offset: 0x0001DFC8
	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.IsWriting)
		{
			stream.SendNext(this.PointsSpawned);
			stream.SendNext(this.InGoalKilled);
			stream.SendNext(this.InWave);
			stream.SendNext(this.WaveSetIndex);
			stream.SendNext(this.SpawnGroupIndex);
			stream.SendNext((int)this.CurStatus);
			return;
		}
		this.PointsSpawned = (float)stream.ReceiveNext();
		this.InGoalKilled = (int)stream.ReceiveNext();
		this.InWave = (bool)stream.ReceiveNext();
		this.WaveSetIndex = (int)stream.ReceiveNext();
		this.SpawnGroupIndex = (int)stream.ReceiveNext();
		this.CurStatus = (AIManager.WaveStatus)((int)stream.ReceiveNext());
	}

	// Token: 0x060003EA RID: 1002 RVA: 0x0001FEAC File Offset: 0x0001E0AC
	public static Vector3 NearestNavPoint(Vector3 input, float maxDistance = -1f)
	{
		if (maxDistance <= 0f)
		{
			float num = 2f;
			for (int i = 0; i < 10; i++)
			{
				if (NavMesh.SamplePosition(input, out AIManager.navHit, num, -1))
				{
					return AIManager.navHit.position;
				}
				num *= 2f;
			}
			return input.INVALID();
		}
		if (!NavMesh.SamplePosition(input, out AIManager.navHit, maxDistance, -1))
		{
			return input.INVALID();
		}
		return AIManager.navHit.position;
	}

	// Token: 0x060003EB RID: 1003 RVA: 0x0001FF1D File Offset: 0x0001E11D
	private void OnDestroy()
	{
		MapManager.SceneChanged = (Action)Delegate.Remove(MapManager.SceneChanged, new Action(this.OnSceneChanged));
	}

	// Token: 0x060003EC RID: 1004 RVA: 0x0001FF3F File Offset: 0x0001E13F
	public AIManager()
	{
	}

	// Token: 0x060003ED RID: 1005 RVA: 0x0001FF61 File Offset: 0x0001E161
	// Note: this type is marked as 'beforefieldinit'.
	static AIManager()
	{
	}

	// Token: 0x0400036D RID: 877
	public static AIManager instance;

	// Token: 0x0400036E RID: 878
	public AIData DB;

	// Token: 0x0400036F RID: 879
	public WaveDB Waves;

	// Token: 0x04000370 RID: 880
	public int MaxEnemies = 25;

	// Token: 0x04000371 RID: 881
	public int Income = 5;

	// Token: 0x04000372 RID: 882
	public LayerMask AILayerMask;

	// Token: 0x04000373 RID: 883
	public AugmentTree EliteMod;

	// Token: 0x04000374 RID: 884
	public AugmentTree BossMod;

	// Token: 0x04000375 RID: 885
	public GameObject SpawnFX;

	// Token: 0x04000376 RID: 886
	public List<AudioClip> SpawnSFX;

	// Token: 0x04000377 RID: 887
	public GameObject BossSpawnFX;

	// Token: 0x04000378 RID: 888
	public AudioClip BossSpawnSFX;

	// Token: 0x04000379 RID: 889
	public GameObject BossDeathFX;

	// Token: 0x0400037A RID: 890
	public AudioClip BossDeathSFX;

	// Token: 0x0400037B RID: 891
	public GameObject NetTPFromFX;

	// Token: 0x0400037C RID: 892
	public GameObject NetTPToFX;

	// Token: 0x0400037D RID: 893
	public GameObject AIShieldBreak;

	// Token: 0x0400037E RID: 894
	public List<AudioClip> ShieldBreakSFX;

	// Token: 0x0400037F RID: 895
	public GameObject AICancelFX;

	// Token: 0x04000380 RID: 896
	public AudioClip EliteSpawnSFX;

	// Token: 0x04000381 RID: 897
	public GameObject ZoneSpawnVFX;

	// Token: 0x04000382 RID: 898
	public AudioClip ZoneSpawnSFX;

	// Token: 0x04000383 RID: 899
	private PhotonView view;

	// Token: 0x04000384 RID: 900
	public float CurGroupVal;

	// Token: 0x04000385 RID: 901
	public int InGoalKilled;

	// Token: 0x04000386 RID: 902
	public float PointsSpawned;

	// Token: 0x04000387 RID: 903
	public bool InWave;

	// Token: 0x04000388 RID: 904
	public int WaveSetIndex;

	// Token: 0x04000389 RID: 905
	public int SpawnGroupIndex;

	// Token: 0x0400038A RID: 906
	public float spawnCD;

	// Token: 0x0400038B RID: 907
	public AILayout Layout;

	// Token: 0x0400038C RID: 908
	public string LayoutString;

	// Token: 0x0400038D RID: 909
	public GenreWaveNode WaveConfig;

	// Token: 0x0400038E RID: 910
	public AIManager.WaveStatus CurStatus;

	// Token: 0x0400038F RID: 911
	private float zoneSpawnDelay;

	// Token: 0x04000390 RID: 912
	public bool preparingNextZone;

	// Token: 0x04000391 RID: 913
	private List<GenreSpawnNode.EliteSpawn> SpawnedElites = new List<GenreSpawnNode.EliteSpawn>();

	// Token: 0x04000392 RID: 914
	public static Augments GlobalEnemyMods = new Augments();

	// Token: 0x04000393 RID: 915
	public static List<string> AugmentIDs = new List<string>();

	// Token: 0x04000394 RID: 916
	public static List<EntityControl> Enemies = new List<EntityControl>();

	// Token: 0x04000395 RID: 917
	private static HashSet<EnemyModTag> tagsCache = new HashSet<EnemyModTag>();

	// Token: 0x04000396 RID: 918
	private float cachedAlive;

	// Token: 0x04000397 RID: 919
	private static AIControl bossCache;

	// Token: 0x04000398 RID: 920
	private static NavMeshHit navHit;

	// Token: 0x02000487 RID: 1159
	public enum WaveStatus
	{
		// Token: 0x04002303 RID: 8963
		Prewave,
		// Token: 0x04002304 RID: 8964
		PreparingZone,
		// Token: 0x04002305 RID: 8965
		SpawningGroup,
		// Token: 0x04002306 RID: 8966
		WaitingForGroup
	}

	// Token: 0x02000488 RID: 1160
	[CompilerGenerated]
	[Serializable]
	private sealed class <>c
	{
		// Token: 0x060021C9 RID: 8649 RVA: 0x000C3D7D File Offset: 0x000C1F7D
		// Note: this type is marked as 'beforefieldinit'.
		static <>c()
		{
		}

		// Token: 0x060021CA RID: 8650 RVA: 0x000C3D89 File Offset: 0x000C1F89
		public <>c()
		{
		}

		// Token: 0x060021CB RID: 8651 RVA: 0x000C3D91 File Offset: 0x000C1F91
		internal int <UpdateAIBrains>b__53_0(AIControl x, AIControl y)
		{
			return x.LastBrainTime.CompareTo(y.LastBrainTime);
		}

		// Token: 0x04002307 RID: 8967
		public static readonly AIManager.<>c <>9 = new AIManager.<>c();

		// Token: 0x04002308 RID: 8968
		public static Comparison<AIControl> <>9__53_0;
	}

	// Token: 0x02000489 RID: 1161
	[CompilerGenerated]
	private sealed class <GroupSpawnSequence>d__73 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x060021CC RID: 8652 RVA: 0x000C3DA4 File Offset: 0x000C1FA4
		[DebuggerHidden]
		public <GroupSpawnSequence>d__73(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x060021CD RID: 8653 RVA: 0x000C3DB4 File Offset: 0x000C1FB4
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
			int num = this.<>1__state;
			if (num == -3 || num == 1)
			{
				try
				{
				}
				finally
				{
					this.<>m__Finally1();
				}
			}
		}

		// Token: 0x060021CE RID: 8654 RVA: 0x000C3DEC File Offset: 0x000C1FEC
		bool IEnumerator.MoveNext()
		{
			bool result;
			try
			{
				int num = this.<>1__state;
				AIManager aimanager = this;
				if (num != 0)
				{
					if (num != 1)
					{
						return false;
					}
					this.<>1__state = -3;
				}
				else
				{
					this.<>1__state = -1;
					pos = point.transform.position;
					forward = point.transform.forward;
					range = 5f;
					enumerator = toSpawn.GetEnumerator();
					this.<>1__state = -3;
				}
				if (!enumerator.MoveNext())
				{
					this.<>m__Finally1();
					enumerator = default(List<AIData.AIDetails>.Enumerator);
					result = false;
				}
				else
				{
					AIData.AIDetails aidetails = enumerator.Current;
					Vector3 b = new Vector3(UnityEngine.Random.Range(range, -range), 0f, UnityEngine.Random.Range(range, -range));
					AIManager.SpawnAIExplicit(aidetails.ResourcePath, pos + b, forward);
					aimanager.PointsSpawned += aidetails.PointValue;
					aimanager.CurGroupVal += aidetails.PointValue;
					this.<>2__current = new WaitForSeconds(UnityEngine.Random.Range(0.15f, 0.3f));
					this.<>1__state = 1;
					result = true;
				}
			}
			catch
			{
				this.System.IDisposable.Dispose();
				throw;
			}
			return result;
		}

		// Token: 0x060021CF RID: 8655 RVA: 0x000C3F6C File Offset: 0x000C216C
		private void <>m__Finally1()
		{
			this.<>1__state = -1;
			((IDisposable)enumerator).Dispose();
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x060021D0 RID: 8656 RVA: 0x000C3F86 File Offset: 0x000C2186
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x060021D1 RID: 8657 RVA: 0x000C3F8E File Offset: 0x000C218E
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x060021D2 RID: 8658 RVA: 0x000C3F95 File Offset: 0x000C2195
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x04002309 RID: 8969
		private int <>1__state;

		// Token: 0x0400230A RID: 8970
		private object <>2__current;

		// Token: 0x0400230B RID: 8971
		public SpawnPoint point;

		// Token: 0x0400230C RID: 8972
		public List<AIData.AIDetails> toSpawn;

		// Token: 0x0400230D RID: 8973
		public AIManager <>4__this;

		// Token: 0x0400230E RID: 8974
		private Vector3 <pos>5__2;

		// Token: 0x0400230F RID: 8975
		private Vector3 <forward>5__3;

		// Token: 0x04002310 RID: 8976
		private float <range>5__4;

		// Token: 0x04002311 RID: 8977
		private List<AIData.AIDetails>.Enumerator <>7__wrap4;
	}

	// Token: 0x0200048A RID: 1162
	[CompilerGenerated]
	private sealed class <>c__DisplayClass79_0
	{
		// Token: 0x060021D3 RID: 8659 RVA: 0x000C3F9D File Offset: 0x000C219D
		public <>c__DisplayClass79_0()
		{
		}

		// Token: 0x060021D4 RID: 8660 RVA: 0x000C3FA5 File Offset: 0x000C21A5
		internal void <DoSpawnFX>b__0()
		{
			PostFXManager.instance.GreyscalePulse(this.pos);
		}

		// Token: 0x04002312 RID: 8978
		public Vector3 pos;
	}

	// Token: 0x0200048B RID: 1163
	[CompilerGenerated]
	private sealed class <AugmentTriggerOverTime>d__87 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x060021D5 RID: 8661 RVA: 0x000C3FB7 File Offset: 0x000C21B7
		[DebuggerHidden]
		public <AugmentTriggerOverTime>d__87(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x060021D6 RID: 8662 RVA: 0x000C3FC8 File Offset: 0x000C21C8
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
			switch (this.<>1__state)
			{
			case -4:
			case 2:
				break;
			case -3:
			case 1:
				try
				{
					return;
				}
				finally
				{
					this.<>m__Finally1();
				}
				break;
			case -2:
			case -1:
			case 0:
				return;
			default:
				return;
			}
			try
			{
			}
			finally
			{
				this.<>m__Finally2();
			}
		}

		// Token: 0x060021D7 RID: 8663 RVA: 0x000C4034 File Offset: 0x000C2234
		bool IEnumerator.MoveNext()
		{
			bool result;
			try
			{
				switch (this.<>1__state)
				{
				case 0:
					this.<>1__state = -1;
					new System.Random(seed);
					snippets = new List<ModSnippetNode>();
					enumerator = AIManager.GlobalEnemyMods.trees.GetEnumerator();
					this.<>1__state = -3;
					break;
				case 1:
				{
					this.<>1__state = -3;
					Vector3 position = Fountain.instance.transform.position;
					EffectProperties effectProperties = new EffectProperties();
					effectProperties.StartLoc = (effectProperties.OutLoc = global::Pose.WorldPoint(position, Vector3.up));
					effectProperties.SourceType = ActionSource.Genre;
					effectProperties.AbilityType = PlayerAbilityType.None;
					effectProperties.IsWorld = true;
					foreach (ModSnippetNode modSnippetNode in snippets)
					{
						modSnippetNode.TryTriggerFromProps(effectProperties, augment);
					}
					int num = i;
					i = num + 1;
					goto IL_197;
				}
				case 2:
				{
					this.<>1__state = -4;
					Vector3 position2 = Fountain.instance.transform.position;
					EffectProperties effectProperties2 = new EffectProperties();
					effectProperties2.StartLoc = global::Pose.WorldPoint(position2, Vector3.up);
					effectProperties2.SourceType = ActionSource.Genre;
					effectProperties2.AbilityType = PlayerAbilityType.None;
					effectProperties2.IsWorld = true;
					foreach (ModSnippetNode modSnippetNode2 in snippets)
					{
						modSnippetNode2.TryTriggerFromProps(effectProperties2, augment);
					}
					int num = i;
					i = num + 1;
					goto IL_333;
				}
				default:
					return false;
				}
				IL_1AF:
				while (enumerator.MoveNext())
				{
					KeyValuePair<AugmentRootNode, int> keyValuePair = enumerator.Current;
					int num;
					AugmentRootNode augmentRootNode;
					keyValuePair.Deconstruct(out augmentRootNode, out num);
					augment = augmentRootNode;
					count = num;
					if (augment.ApplyToWorld)
					{
						snippets.Clear();
						augment.CollectSnippets(ref snippets, trigger);
						if (snippets.Count != 0)
						{
							i = 0;
							goto IL_197;
						}
					}
				}
				this.<>m__Finally1();
				enumerator = default(Dictionary<AugmentRootNode, int>.Enumerator);
				enumerator = GameplayManager.instance.GenreBindings.trees.GetEnumerator();
				this.<>1__state = -4;
				IL_34B:
				while (enumerator.MoveNext())
				{
					KeyValuePair<AugmentRootNode, int> keyValuePair = enumerator.Current;
					int num;
					AugmentRootNode augmentRootNode;
					keyValuePair.Deconstruct(out augmentRootNode, out num);
					augment = augmentRootNode;
					count = num;
					if (!augment.ApplyToPlayers && !augment.ApplyToEnemies)
					{
						snippets.Clear();
						augment.CollectSnippets(ref snippets, trigger);
						if (snippets.Count != 0)
						{
							i = 0;
							goto IL_333;
						}
					}
				}
				this.<>m__Finally2();
				enumerator = default(Dictionary<AugmentRootNode, int>.Enumerator);
				return false;
				IL_197:
				if (i >= count)
				{
					augment = null;
					goto IL_1AF;
				}
				this.<>2__current = new WaitForSeconds(0.5f);
				this.<>1__state = 1;
				return true;
				IL_333:
				if (i >= count)
				{
					augment = null;
					goto IL_34B;
				}
				this.<>2__current = new WaitForSeconds(0.5f);
				this.<>1__state = 2;
				result = true;
			}
			catch
			{
				this.System.IDisposable.Dispose();
				throw;
			}
			return result;
		}

		// Token: 0x060021D8 RID: 8664 RVA: 0x000C4408 File Offset: 0x000C2608
		private void <>m__Finally1()
		{
			this.<>1__state = -1;
			((IDisposable)enumerator).Dispose();
		}

		// Token: 0x060021D9 RID: 8665 RVA: 0x000C4422 File Offset: 0x000C2622
		private void <>m__Finally2()
		{
			this.<>1__state = -1;
			((IDisposable)enumerator).Dispose();
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x060021DA RID: 8666 RVA: 0x000C443C File Offset: 0x000C263C
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x060021DB RID: 8667 RVA: 0x000C4444 File Offset: 0x000C2644
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x060021DC RID: 8668 RVA: 0x000C444B File Offset: 0x000C264B
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x04002313 RID: 8979
		private int <>1__state;

		// Token: 0x04002314 RID: 8980
		private object <>2__current;

		// Token: 0x04002315 RID: 8981
		public int seed;

		// Token: 0x04002316 RID: 8982
		public EventTrigger trigger;

		// Token: 0x04002317 RID: 8983
		private List<ModSnippetNode> <snippets>5__2;

		// Token: 0x04002318 RID: 8984
		private Dictionary<AugmentRootNode, int>.Enumerator <>7__wrap2;

		// Token: 0x04002319 RID: 8985
		private AugmentRootNode <augment>5__4;

		// Token: 0x0400231A RID: 8986
		private int <count>5__5;

		// Token: 0x0400231B RID: 8987
		private int <i>5__6;
	}
}
