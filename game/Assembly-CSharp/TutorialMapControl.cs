using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Photon.Pun;
using UnityEngine;

// Token: 0x020000DD RID: 221
public class TutorialMapControl : MonoBehaviour
{
	// Token: 0x060009E5 RID: 2533 RVA: 0x00041560 File Offset: 0x0003F760
	private void Awake()
	{
		if (!TutorialManager.InTutorial)
		{
			TutorialManager.InTutorial = true;
		}
		TutorialManager.OnStepChanged = (Action<TutorialStep>)Delegate.Combine(TutorialManager.OnStepChanged, new Action<TutorialStep>(this.TutorialStepChanged));
		GameplayManager.OnGameStateChanged = (Action<GameState, GameState>)Delegate.Combine(GameplayManager.OnGameStateChanged, new Action<GameState, GameState>(this.AllGameEvents));
	}

	// Token: 0x060009E6 RID: 2534 RVA: 0x000415BA File Offset: 0x0003F7BA
	private IEnumerator Start()
	{
		while (PlayerControl.myInstance == null)
		{
			yield return true;
		}
		yield return true;
		if (GameplayManager.instance.GameGraph != this.TutorialGenre)
		{
			GameplayManager.instance.TrySetGenreMaster(this.TutorialGenre.Root.guid);
		}
		PlayerControl.myInstance.Display.CamController.RotateTowardPosition(new Vector3(0f, 5f, 0f), 9551000f);
		PlayerControl.myInstance.AddAugment(this.TutorialAugment, 1);
		PlayerControl.myInstance.actions.SetCore(this.TutorialCore);
		PlayerControl.myInstance.actions.LoadAbility(PlayerAbilityType.Primary, this.Primary.ID, false);
		PlayerControl.myInstance.actions.LoadAbility(PlayerAbilityType.Secondary, this.Secondary.ID, false);
		PlayerControl.myInstance.actions.LoadAbility(PlayerAbilityType.Movement, this.Movement.ID, false);
		LorePage page_Starting = this.Page_Starting;
		page_Starting.OnUse = (Action)Delegate.Combine(page_Starting.OnUse, new Action(delegate()
		{
			TutorialManager.instance.ChangeStep(TutorialStep.PlayerControl);
		}));
		LorePage page_Torn = this.Page_Torn;
		page_Torn.OnUse = (Action)Delegate.Combine(page_Torn.OnUse, new Action(delegate()
		{
			TutorialManager.instance.ChangeStep(TutorialStep.CombatA);
		}));
		Tutorial_CoreSelect coreSelector = this.CoreSelector;
		coreSelector.OnSelectedCore = (Action)Delegate.Combine(coreSelector.OnSelectedCore, new Action(delegate()
		{
			TutorialManager.instance.ChangeStep(TutorialStep.CoreObjective);
		}));
		yield break;
	}

	// Token: 0x060009E7 RID: 2535 RVA: 0x000415CC File Offset: 0x0003F7CC
	private void OnDestroy()
	{
		TutorialManager.OnStepChanged = (Action<TutorialStep>)Delegate.Remove(TutorialManager.OnStepChanged, new Action<TutorialStep>(this.TutorialStepChanged));
		GameplayManager.OnGameStateChanged = (Action<GameState, GameState>)Delegate.Remove(GameplayManager.OnGameStateChanged, new Action<GameState, GameState>(this.TopGameEvents));
		GameplayManager.OnGameStateChanged = (Action<GameState, GameState>)Delegate.Remove(GameplayManager.OnGameStateChanged, new Action<GameState, GameState>(this.AllGameEvents));
		PlayerControl myInstance = PlayerControl.myInstance;
		myInstance.OnAugmentsChanged = (Action)Delegate.Remove(myInstance.OnAugmentsChanged, new Action(this.OnPlayerAugmentChosen));
		TutorialManager.ResetTutorial();
		if (PhotonNetwork.InRoom && PhotonNetwork.IsMasterClient)
		{
			PhotonNetwork.CurrentRoom.MaxPlayers = 4;
		}
	}

	// Token: 0x060009E8 RID: 2536 RVA: 0x00041680 File Offset: 0x0003F880
	private void TutorialStepChanged(TutorialStep step)
	{
		if (step <= TutorialStep.Spender_StoneStairs)
		{
			if (step <= TutorialStep.Generator)
			{
				if (step == TutorialStep.PlayerControl)
				{
					AudioManager.PlaySFX2D(this.NextStepSFX, 0.6f, 0.1f);
					this.JumpIndicator.gameObject.SetActive(true);
					this.BottomAreaFX.Stop();
					this.BottomAreaBlocker.gameObject.SetActive(false);
					return;
				}
				if (step != TutorialStep.Generator)
				{
					return;
				}
				this.JumpIndicator.Deactivate();
				EntityHealth health = AIManager.SpawnAIExplicit(AIData.AIDetails.GetResourcePath(this.CrystalRaving), this.Cry_GenSpawn_A.position, this.Cry_GenSpawn_A.forward).health;
				health.OnDie = (Action<DamageInfo>)Delegate.Combine(health.OnDie, new Action<DamageInfo>(delegate(DamageInfo <p0>)
				{
					TutorialManager.instance.ChangeStep(TutorialStep.Generator_Mana);
				}));
				return;
			}
			else
			{
				if (step == TutorialStep.Generator_Mana)
				{
					PlayerControl.myInstance.Mana.Drain();
					this.DoubleGeneratorEnemy();
					return;
				}
				if (step == TutorialStep.Spender)
				{
					EntityControl entityControl = AIManager.SpawnAIExplicit(AIData.AIDetails.GetResourcePath(this.CrystalTangent), this.Cry_SpenderSpawn.position, this.Cry_GenSpawn_A.forward);
					entityControl.AddAugment(this.PrimaryImmune, 1);
					EntityHealth health2 = entityControl.health;
					health2.OnDie = (Action<DamageInfo>)Delegate.Combine(health2.OnDie, new Action<DamageInfo>(delegate(DamageInfo <p0>)
					{
						TutorialManager.instance.ChangeStep(TutorialStep.Spender_StoneStairs);
					}));
					return;
				}
				if (step != TutorialStep.Spender_StoneStairs)
				{
					return;
				}
				this.SpawnSpenderStairs();
				AudioManager.PlaySFX2D(this.NextStepSFX, 0.6f, 0.1f);
				return;
			}
		}
		else if (step <= TutorialStep.CombatA_Completed)
		{
			if (step == TutorialStep.MoveAbility)
			{
				AIManager.instance.SetLayout(this.TutorialLayout.Layout);
				this.MainArenaBlock.SetActive(true);
				this.FountainVFX.SetActive(false);
				this.SpawnMover();
				return;
			}
			if (step == TutorialStep.CombatA)
			{
				this.StartCombatTop();
				AudioManager.PlaySFX2D(this.NextStepSFX, 0.6f, 0.1f);
				return;
			}
			if (step != TutorialStep.CombatA_Completed)
			{
				return;
			}
			AudioManager.PlaySFX2D(this.NextStepSFX, 0.6f, 0.1f);
			return;
		}
		else
		{
			if (step != TutorialStep.PlayerChoice)
			{
				switch (step)
				{
				case TutorialStep.PostCore:
					this.CoreSelector.gameObject.SetActive(true);
					this.SpawnDownRamp();
					AudioManager.PlaySFX2D(this.CoreAppearSFX, 0.6f, 0.1f);
					return;
				case TutorialStep.CoreObjective:
					base.Invoke("StartCoreObjective", 0.5f);
					return;
				case TutorialStep.EnemyChoice_Ready:
					this.Enemy_Indicator.gameObject.SetActive(true);
					this.SpawnDownRamp();
					AudioManager.PlaySFX2D(this.NextStepSFX, 0.6f, 0.1f);
					return;
				case (TutorialStep)88:
				case (TutorialStep)89:
					break;
				case TutorialStep.EnemyChoice:
					AudioManager.PlaySFX2D(this.NextStepSFX, 0.6f, 0.1f);
					this.Enemy_Indicator.Deactivate();
					this.DoEnemyChoice();
					return;
				default:
					if (step != TutorialStep.Completed)
					{
						return;
					}
					this.TutorialCompleted();
					break;
				}
				return;
			}
			this.AwardPlayer();
			return;
		}
	}

	// Token: 0x060009E9 RID: 2537 RVA: 0x00041960 File Offset: 0x0003FB60
	private void DoubleGeneratorEnemy()
	{
		EntityControl entityControl = AIManager.SpawnAIExplicit(AIData.AIDetails.GetResourcePath(this.CrystalTangent), this.Cry_GenSpawn_B.position, this.Cry_GenSpawn_B.forward);
		EntityControl entityControl2 = AIManager.SpawnAIExplicit(AIData.AIDetails.GetResourcePath(this.CrystalSplice), this.Cry_GenSpawn_C.position, this.Cry_GenSpawn_C.forward);
		int mKill = 0;
		EntityHealth health = entityControl.health;
		health.OnDie = (Action<DamageInfo>)Delegate.Combine(health.OnDie, new Action<DamageInfo>(delegate(DamageInfo <p0>)
		{
			int mKill = mKill;
			mKill++;
			if (mKill >= 2)
			{
				TutorialManager.instance.ChangeStep(TutorialStep.Spender);
			}
		}));
		EntityHealth health2 = entityControl2.health;
		health2.OnDie = (Action<DamageInfo>)Delegate.Combine(health2.OnDie, new Action<DamageInfo>(delegate(DamageInfo <p0>)
		{
			int mKill = mKill;
			mKill++;
			if (mKill >= 2)
			{
				TutorialManager.instance.ChangeStep(TutorialStep.Spender);
			}
		}));
	}

	// Token: 0x060009EA RID: 2538 RVA: 0x00041A14 File Offset: 0x0003FC14
	private void SpawnSpenderStairs()
	{
		this.SpenderStairs.SetActive(true);
		this.TopAreaFX.Play();
		ActionMaterial[] componentsInChildren = this.SpenderStairs.GetComponentsInChildren<ActionMaterial>();
		ActionMaterial[] array = componentsInChildren;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Enter();
		}
		base.StartCoroutine(this.StairDissolveIn(componentsInChildren));
	}

	// Token: 0x060009EB RID: 2539 RVA: 0x00041A6C File Offset: 0x0003FC6C
	private void SpawnDownRamp()
	{
		this.RampDown.SetActive(true);
		ActionMaterial[] componentsInChildren = this.RampDown.GetComponentsInChildren<ActionMaterial>();
		ActionMaterial[] array = componentsInChildren;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Enter();
		}
		base.StartCoroutine(this.StairDissolveIn(componentsInChildren));
	}

	// Token: 0x060009EC RID: 2540 RVA: 0x00041AB7 File Offset: 0x0003FCB7
	private IEnumerator StairDissolveIn(ActionMaterial[] mats)
	{
		yield return new WaitForSeconds(0.5f);
		float t = 0f;
		while (t < 2.5f)
		{
			for (int i = 0; i < mats.Length; i++)
			{
				mats[i].TickUpdate();
			}
			t += Time.deltaTime;
			yield return true;
		}
		yield break;
	}

	// Token: 0x060009ED RID: 2541 RVA: 0x00041AC6 File Offset: 0x0003FCC6
	private void SpawnMover()
	{
		this.Page_Torn.gameObject.SetActive(true);
	}

	// Token: 0x060009EE RID: 2542 RVA: 0x00041AD9 File Offset: 0x0003FCD9
	private void StartCombatTop()
	{
		this.TopArenaBlock.SetActive(true);
		SpawnZone.RequiredSubGroup = 1;
		GameplayManager.OnGameStateChanged = (Action<GameState, GameState>)Delegate.Combine(GameplayManager.OnGameStateChanged, new Action<GameState, GameState>(this.TopGameEvents));
		WaveManager.instance.NextWave();
	}

	// Token: 0x060009EF RID: 2543 RVA: 0x00041B17 File Offset: 0x0003FD17
	private void AllGameEvents(GameState from, GameState to)
	{
		if (to == GameState.PostGame)
		{
			TutorialManager.instance.ChangeStep(TutorialStep.Completed);
		}
	}

	// Token: 0x060009F0 RID: 2544 RVA: 0x00041B2C File Offset: 0x0003FD2C
	public void TutorialCompleted()
	{
		UnityEngine.Debug.Log("Tutorial Completed");
		Fountain.instance.ChapterCompletePulse();
		Settings.DoneTutorial();
		ParseManager.DoneTutorial();
		Progression.DoTutorialReward();
		BossRewardTrigger bossRewardTrigger = Progression.CreateQuillmarkReward(new Vector3(-32f, 2.6f, -34f), 100);
		bossRewardTrigger.OnInteract = (Action)Delegate.Combine(bossRewardTrigger.OnInteract, new Action(delegate()
		{
			TutorialManager.IsReturning = true;
		}));
	}

	// Token: 0x060009F1 RID: 2545 RVA: 0x00041BAB File Offset: 0x0003FDAB
	private void TopGameEvents(GameState from, GameState to)
	{
		if (to == GameState.Reward_Start)
		{
			this.AugmentIndicator.gameObject.SetActive(true);
			TutorialManager.instance.ChangeStep(TutorialStep.CombatA_Completed);
			this.FountainTriggerFX.SetActive(true);
			this.FountainProj.Show();
		}
	}

	// Token: 0x060009F2 RID: 2546 RVA: 0x00041BE8 File Offset: 0x0003FDE8
	private void AwardPlayer()
	{
		this.AugmentIndicator.Deactivate();
		AugmentsPanel.AwardUpgradeChoice();
		this.FountainTriggerFX.GetComponentInChildren<ParticleSystem>().Stop();
		this.FountainProj.Hide();
		PlayerControl myInstance = PlayerControl.myInstance;
		myInstance.OnAugmentsChanged = (Action)Delegate.Combine(myInstance.OnAugmentsChanged, new Action(this.OnPlayerAugmentChosen));
		GameplayManager.OnGameStateChanged = (Action<GameState, GameState>)Delegate.Remove(GameplayManager.OnGameStateChanged, new Action<GameState, GameState>(this.TopGameEvents));
	}

	// Token: 0x060009F3 RID: 2547 RVA: 0x00041C68 File Offset: 0x0003FE68
	private void OnPlayerAugmentChosen()
	{
		PlayerControl myInstance = PlayerControl.myInstance;
		myInstance.OnAugmentsChanged = (Action)Delegate.Remove(myInstance.OnAugmentsChanged, new Action(this.OnPlayerAugmentChosen));
		TutorialManager.instance.ChangeStep(TutorialStep.PostCore);
		SpawnZone.RequiredSubGroup = 2;
		this.TopAreaBlocker.SetActive(false);
		this.TopAreaFX.Stop();
		GameplayManager.InvokeDelayed(2f, delegate()
		{
			AugmentsPanel.TryClose();
		});
	}

	// Token: 0x060009F4 RID: 2548 RVA: 0x00041CED File Offset: 0x0003FEED
	private void DoEnemyChoice()
	{
		GameplayManager.instance.UpdateGameState(GameState.Reward_PreEnemy);
		GameplayManager.InvokeDelayed(3f, delegate()
		{
			GameplayManager.instance.UpdateGameState(GameState.Reward_Enemy);
			VoteManager.PrepareVote(ModType.Enemy);
		});
	}

	// Token: 0x060009F5 RID: 2549 RVA: 0x00041D24 File Offset: 0x0003FF24
	private void StartCoreObjective()
	{
		if (PlayerControl.myInstance.actions.core == this.BlueCore)
		{
			GoalManager.instance.SetBonusObjective(this.BlueObjective, true);
			this.BlueUI.SetActive(true);
		}
		else
		{
			if (!(PlayerControl.myInstance.actions.core == this.YellowCore))
			{
				TutorialManager.instance.ChangeStep(TutorialStep.EnemyChoice);
				return;
			}
			GoalManager.instance.SetBonusObjective(this.YellowObjective, true);
			this.YellowUI.SetActive(true);
		}
		PlayerControl.myInstance.actions.SetCooldown(PlayerAbilityType.Utility, 4.5f, null);
		GoalManager.instance.BeginBonusObjective();
	}

	// Token: 0x060009F6 RID: 2550 RVA: 0x00041DD4 File Offset: 0x0003FFD4
	public TutorialMapControl()
	{
	}

	// Token: 0x0400085C RID: 2140
	public GameObject CrystalSplice;

	// Token: 0x0400085D RID: 2141
	public GameObject CrystalTangent;

	// Token: 0x0400085E RID: 2142
	public GameObject CrystalRaving;

	// Token: 0x0400085F RID: 2143
	public GameObject CrystalMoving;

	// Token: 0x04000860 RID: 2144
	public AugmentTree PrimaryImmune;

	// Token: 0x04000861 RID: 2145
	public Transform Cry_GenSpawn_A;

	// Token: 0x04000862 RID: 2146
	public Transform Cry_GenSpawn_B;

	// Token: 0x04000863 RID: 2147
	public Transform Cry_GenSpawn_C;

	// Token: 0x04000864 RID: 2148
	public Transform Cry_SpenderSpawn;

	// Token: 0x04000865 RID: 2149
	public Transform Cry_MovingSpawn;

	// Token: 0x04000866 RID: 2150
	public GenreTree TutorialGenre;

	// Token: 0x04000867 RID: 2151
	public AugmentTree TutorialCore;

	// Token: 0x04000868 RID: 2152
	public AugmentTree TutorialAugment;

	// Token: 0x04000869 RID: 2153
	public AbilityTree Primary;

	// Token: 0x0400086A RID: 2154
	public AbilityTree Secondary;

	// Token: 0x0400086B RID: 2155
	public AbilityTree Movement;

	// Token: 0x0400086C RID: 2156
	public AILayoutRef TutorialLayout;

	// Token: 0x0400086D RID: 2157
	public LorePage Page_Starting;

	// Token: 0x0400086E RID: 2158
	public LorePage Page_Torn;

	// Token: 0x0400086F RID: 2159
	public LorePage Page_Enemy;

	// Token: 0x04000870 RID: 2160
	public EntityIndicator Enemy_Indicator;

	// Token: 0x04000871 RID: 2161
	public AudioClip NextStepSFX;

	// Token: 0x04000872 RID: 2162
	public GameObject SpenderStairs;

	// Token: 0x04000873 RID: 2163
	public GameObject RampDown;

	// Token: 0x04000874 RID: 2164
	public GameObject MainArenaBlock;

	// Token: 0x04000875 RID: 2165
	public GameObject TopArenaBlock;

	// Token: 0x04000876 RID: 2166
	public EntityIndicator JumpIndicator;

	// Token: 0x04000877 RID: 2167
	public EntityIndicator AugmentIndicator;

	// Token: 0x04000878 RID: 2168
	public GameObject CoreAbilityText;

	// Token: 0x04000879 RID: 2169
	public ParticleSystem BottomAreaFX;

	// Token: 0x0400087A RID: 2170
	public GameObject BottomAreaBlocker;

	// Token: 0x0400087B RID: 2171
	public GameObject FountainTriggerFX;

	// Token: 0x0400087C RID: 2172
	public ProjectorScale FountainProj;

	// Token: 0x0400087D RID: 2173
	public GameObject FountainVFX;

	// Token: 0x0400087E RID: 2174
	public ParticleSystem TopAreaFX;

	// Token: 0x0400087F RID: 2175
	public GameObject TopAreaBlocker;

	// Token: 0x04000880 RID: 2176
	public Tutorial_CoreSelect CoreSelector;

	// Token: 0x04000881 RID: 2177
	public AudioClip CoreAppearSFX;

	// Token: 0x04000882 RID: 2178
	public AugmentTree BlueCore;

	// Token: 0x04000883 RID: 2179
	public GameObject BlueUI;

	// Token: 0x04000884 RID: 2180
	public AugmentTree BlueObjective;

	// Token: 0x04000885 RID: 2181
	public AugmentTree YellowCore;

	// Token: 0x04000886 RID: 2182
	public GameObject YellowUI;

	// Token: 0x04000887 RID: 2183
	public AugmentTree YellowObjective;

	// Token: 0x020004D0 RID: 1232
	[CompilerGenerated]
	[Serializable]
	private sealed class <>c
	{
		// Token: 0x060022E3 RID: 8931 RVA: 0x000C7F84 File Offset: 0x000C6184
		// Note: this type is marked as 'beforefieldinit'.
		static <>c()
		{
		}

		// Token: 0x060022E4 RID: 8932 RVA: 0x000C7F90 File Offset: 0x000C6190
		public <>c()
		{
		}

		// Token: 0x060022E5 RID: 8933 RVA: 0x000C7F98 File Offset: 0x000C6198
		internal void <Start>b__45_0()
		{
			TutorialManager.instance.ChangeStep(TutorialStep.PlayerControl);
		}

		// Token: 0x060022E6 RID: 8934 RVA: 0x000C7FA6 File Offset: 0x000C61A6
		internal void <Start>b__45_1()
		{
			TutorialManager.instance.ChangeStep(TutorialStep.CombatA);
		}

		// Token: 0x060022E7 RID: 8935 RVA: 0x000C7FB4 File Offset: 0x000C61B4
		internal void <Start>b__45_2()
		{
			TutorialManager.instance.ChangeStep(TutorialStep.CoreObjective);
		}

		// Token: 0x060022E8 RID: 8936 RVA: 0x000C7FC2 File Offset: 0x000C61C2
		internal void <TutorialStepChanged>b__47_0(DamageInfo <p0>)
		{
			TutorialManager.instance.ChangeStep(TutorialStep.Generator_Mana);
		}

		// Token: 0x060022E9 RID: 8937 RVA: 0x000C7FD0 File Offset: 0x000C61D0
		internal void <TutorialStepChanged>b__47_1(DamageInfo <p0>)
		{
			TutorialManager.instance.ChangeStep(TutorialStep.Spender_StoneStairs);
		}

		// Token: 0x060022EA RID: 8938 RVA: 0x000C7FDE File Offset: 0x000C61DE
		internal void <TutorialCompleted>b__55_0()
		{
			TutorialManager.IsReturning = true;
		}

		// Token: 0x060022EB RID: 8939 RVA: 0x000C7FE6 File Offset: 0x000C61E6
		internal void <OnPlayerAugmentChosen>b__58_0()
		{
			AugmentsPanel.TryClose();
		}

		// Token: 0x060022EC RID: 8940 RVA: 0x000C7FED File Offset: 0x000C61ED
		internal void <DoEnemyChoice>b__59_0()
		{
			GameplayManager.instance.UpdateGameState(GameState.Reward_Enemy);
			VoteManager.PrepareVote(ModType.Enemy);
		}

		// Token: 0x0400246B RID: 9323
		public static readonly TutorialMapControl.<>c <>9 = new TutorialMapControl.<>c();

		// Token: 0x0400246C RID: 9324
		public static Action <>9__45_0;

		// Token: 0x0400246D RID: 9325
		public static Action <>9__45_1;

		// Token: 0x0400246E RID: 9326
		public static Action <>9__45_2;

		// Token: 0x0400246F RID: 9327
		public static Action<DamageInfo> <>9__47_0;

		// Token: 0x04002470 RID: 9328
		public static Action<DamageInfo> <>9__47_1;

		// Token: 0x04002471 RID: 9329
		public static Action <>9__55_0;

		// Token: 0x04002472 RID: 9330
		public static Action <>9__58_0;

		// Token: 0x04002473 RID: 9331
		public static Action <>9__59_0;
	}

	// Token: 0x020004D1 RID: 1233
	[CompilerGenerated]
	private sealed class <Start>d__45 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x060022ED RID: 8941 RVA: 0x000C8001 File Offset: 0x000C6201
		[DebuggerHidden]
		public <Start>d__45(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x060022EE RID: 8942 RVA: 0x000C8010 File Offset: 0x000C6210
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x060022EF RID: 8943 RVA: 0x000C8014 File Offset: 0x000C6214
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			TutorialMapControl tutorialMapControl = this;
			switch (num)
			{
			case 0:
				this.<>1__state = -1;
				break;
			case 1:
				this.<>1__state = -1;
				break;
			case 2:
			{
				this.<>1__state = -1;
				if (GameplayManager.instance.GameGraph != tutorialMapControl.TutorialGenre)
				{
					GameplayManager.instance.TrySetGenreMaster(tutorialMapControl.TutorialGenre.Root.guid);
				}
				PlayerControl.myInstance.Display.CamController.RotateTowardPosition(new Vector3(0f, 5f, 0f), 9551000f);
				PlayerControl.myInstance.AddAugment(tutorialMapControl.TutorialAugment, 1);
				PlayerControl.myInstance.actions.SetCore(tutorialMapControl.TutorialCore);
				PlayerControl.myInstance.actions.LoadAbility(PlayerAbilityType.Primary, tutorialMapControl.Primary.ID, false);
				PlayerControl.myInstance.actions.LoadAbility(PlayerAbilityType.Secondary, tutorialMapControl.Secondary.ID, false);
				PlayerControl.myInstance.actions.LoadAbility(PlayerAbilityType.Movement, tutorialMapControl.Movement.ID, false);
				LorePage page_Starting = tutorialMapControl.Page_Starting;
				page_Starting.OnUse = (Action)Delegate.Combine(page_Starting.OnUse, new Action(TutorialMapControl.<>c.<>9.<Start>b__45_0));
				LorePage page_Torn = tutorialMapControl.Page_Torn;
				page_Torn.OnUse = (Action)Delegate.Combine(page_Torn.OnUse, new Action(TutorialMapControl.<>c.<>9.<Start>b__45_1));
				Tutorial_CoreSelect coreSelector = tutorialMapControl.CoreSelector;
				coreSelector.OnSelectedCore = (Action)Delegate.Combine(coreSelector.OnSelectedCore, new Action(TutorialMapControl.<>c.<>9.<Start>b__45_2));
				return false;
			}
			default:
				return false;
			}
			if (!(PlayerControl.myInstance == null))
			{
				this.<>2__current = true;
				this.<>1__state = 2;
				return true;
			}
			this.<>2__current = true;
			this.<>1__state = 1;
			return true;
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x060022F0 RID: 8944 RVA: 0x000C8218 File Offset: 0x000C6418
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x060022F1 RID: 8945 RVA: 0x000C8220 File Offset: 0x000C6420
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x060022F2 RID: 8946 RVA: 0x000C8227 File Offset: 0x000C6427
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x04002474 RID: 9332
		private int <>1__state;

		// Token: 0x04002475 RID: 9333
		private object <>2__current;

		// Token: 0x04002476 RID: 9334
		public TutorialMapControl <>4__this;
	}

	// Token: 0x020004D2 RID: 1234
	[CompilerGenerated]
	private sealed class <>c__DisplayClass48_0
	{
		// Token: 0x060022F3 RID: 8947 RVA: 0x000C822F File Offset: 0x000C642F
		public <>c__DisplayClass48_0()
		{
		}

		// Token: 0x060022F4 RID: 8948 RVA: 0x000C8238 File Offset: 0x000C6438
		internal void <DoubleGeneratorEnemy>b__0(DamageInfo <p0>)
		{
			int num = this.mKill;
			this.mKill = num + 1;
			if (this.mKill >= 2)
			{
				TutorialManager.instance.ChangeStep(TutorialStep.Spender);
			}
		}

		// Token: 0x060022F5 RID: 8949 RVA: 0x000C826C File Offset: 0x000C646C
		internal void <DoubleGeneratorEnemy>b__1(DamageInfo <p0>)
		{
			int num = this.mKill;
			this.mKill = num + 1;
			if (this.mKill >= 2)
			{
				TutorialManager.instance.ChangeStep(TutorialStep.Spender);
			}
		}

		// Token: 0x04002477 RID: 9335
		public int mKill;
	}

	// Token: 0x020004D3 RID: 1235
	[CompilerGenerated]
	private sealed class <StairDissolveIn>d__51 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x060022F6 RID: 8950 RVA: 0x000C829E File Offset: 0x000C649E
		[DebuggerHidden]
		public <StairDissolveIn>d__51(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x060022F7 RID: 8951 RVA: 0x000C82AD File Offset: 0x000C64AD
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x060022F8 RID: 8952 RVA: 0x000C82B0 File Offset: 0x000C64B0
		bool IEnumerator.MoveNext()
		{
			switch (this.<>1__state)
			{
			case 0:
				this.<>1__state = -1;
				this.<>2__current = new WaitForSeconds(0.5f);
				this.<>1__state = 1;
				return true;
			case 1:
				this.<>1__state = -1;
				t = 0f;
				break;
			case 2:
				this.<>1__state = -1;
				break;
			default:
				return false;
			}
			if (t >= 2.5f)
			{
				return false;
			}
			ActionMaterial[] array = mats;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].TickUpdate();
			}
			t += Time.deltaTime;
			this.<>2__current = true;
			this.<>1__state = 2;
			return true;
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x060022F9 RID: 8953 RVA: 0x000C8365 File Offset: 0x000C6565
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x060022FA RID: 8954 RVA: 0x000C836D File Offset: 0x000C656D
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x060022FB RID: 8955 RVA: 0x000C8374 File Offset: 0x000C6574
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x04002478 RID: 9336
		private int <>1__state;

		// Token: 0x04002479 RID: 9337
		private object <>2__current;

		// Token: 0x0400247A RID: 9338
		public ActionMaterial[] mats;

		// Token: 0x0400247B RID: 9339
		private float <t>5__2;
	}
}
