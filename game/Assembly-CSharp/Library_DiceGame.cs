using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

// Token: 0x0200011A RID: 282
public class Library_DiceGame : MonoBehaviour
{
	// Token: 0x17000117 RID: 279
	// (get) Token: 0x06000D45 RID: 3397 RVA: 0x00054F88 File Offset: 0x00053188
	private static bool CanAfford
	{
		get
		{
			return Currency.Gildings >= 10;
		}
	}

	// Token: 0x17000118 RID: 280
	// (get) Token: 0x06000D46 RID: 3398 RVA: 0x00054F98 File Offset: 0x00053198
	public static bool IsInAreaOfInfluence
	{
		get
		{
			return !(Library_DiceGame.instance == null) && !(PlayerControl.myInstance == null) && Library_DiceGame.instance.gameObject.activeSelf && !(Library_DiceGame.instance.AreaOfInfluence == null) && Vector3.Distance(Library_DiceGame.instance.AreaOfInfluence.position, PlayerControl.myInstance.Display.CenterOfMass.position) < Library_DiceGame.instance.AOIRadius;
		}
	}

	// Token: 0x06000D47 RID: 3399 RVA: 0x0005501A File Offset: 0x0005321A
	private void Awake()
	{
		Library_DiceGame.instance = this;
		LibraryManager libraryManager = LibraryManager.instance;
		libraryManager.OnLibraryEntered = (Action)Delegate.Combine(libraryManager.OnLibraryEntered, new Action(this.ReturnToWaiting));
	}

	// Token: 0x06000D48 RID: 3400 RVA: 0x00055048 File Offset: 0x00053248
	private void OnEnable()
	{
		this.ReturnToWaiting();
		base.InvokeRepeating("CheckCurrencyChanged", 2.5f, 1.5f);
	}

	// Token: 0x06000D49 RID: 3401 RVA: 0x00055065 File Offset: 0x00053265
	private void Start()
	{
		this.ReturnToWaiting();
	}

	// Token: 0x06000D4A RID: 3402 RVA: 0x0005506D File Offset: 0x0005326D
	private void Update()
	{
		this.CostGroup.UpdateOpacity(this.CurrentState == Library_DiceGame.DiceState.Waiting, 3f, true);
	}

	// Token: 0x06000D4B RID: 3403 RVA: 0x00055089 File Offset: 0x00053289
	public void TryRoll()
	{
		MapManager.instance.TryRollDice();
	}

	// Token: 0x06000D4C RID: 3404 RVA: 0x00055095 File Offset: 0x00053295
	public static bool CanRoll()
	{
		return Library_DiceGame.instance != null && Library_DiceGame.instance.CurrentState != Library_DiceGame.DiceState.Rolling;
	}

	// Token: 0x06000D4D RID: 3405 RVA: 0x000550B8 File Offset: 0x000532B8
	public void Roll(int playerID, int seed)
	{
		if (this.CurrentState == Library_DiceGame.DiceState.Rolling)
		{
			return;
		}
		if (PlayerControl.myInstance.view.OwnerActorNr == playerID)
		{
			Currency.TrySpend(10);
		}
		this.RollPrompt.Deactivate();
		base.StopAllCoroutines();
		base.StartCoroutine(this.RollSequence(seed, playerID));
	}

	// Token: 0x06000D4E RID: 3406 RVA: 0x00055109 File Offset: 0x00053309
	private IEnumerator RollSequence(int seed, int playerID)
	{
		AudioManager.PlayClipAtPoint(this.RollStartSFX, this.Dice[2].transform.position, 1f, 1f, 1f, 10f, 250f);
		this.CurrentState = Library_DiceGame.DiceState.Rolling;
		System.Random rng = new System.Random(seed);
		foreach (Library_Dice library_Dice in this.Dice)
		{
			library_Dice.StartRolling();
		}
		int firstResult = rng.Next(1, 7);
		int secondResult = rng.Next(1, 7);
		int thirdResult = rng.Next(1, 7);
		Library_DiceGame.Result res = Library_DiceGame.Result.Single;
		yield return new WaitForSeconds(this.RollDuration);
		this.Dice[0].Land(firstResult, res);
		AudioManager.PlayClipAtPoint(this.DiceStartSFX.GetRandomClip(-1), this.Dice[0].transform.position, 1f, 1f, 1f, 10f, 250f);
		yield return new WaitForSeconds(this.DiceDelay);
		if (firstResult == secondResult)
		{
			res = Library_DiceGame.Result.Double;
		}
		this.Dice[1].Land(secondResult, res);
		AudioManager.PlayClipAtPoint(this.DiceStartSFX.GetRandomClip(-1), this.Dice[1].transform.position, 1f, 1f, 1f, 10f, 250f);
		yield return new WaitForSeconds(this.DiceDelay);
		Library_DiceGame.Result result = Library_DiceGame.Result.Single;
		if (firstResult == thirdResult || secondResult == thirdResult)
		{
			result = Library_DiceGame.Result.Double;
			res = Library_DiceGame.Result.Double;
		}
		if (firstResult == secondResult && secondResult == thirdResult)
		{
			result = Library_DiceGame.Result.Triple;
			res = Library_DiceGame.Result.Triple;
		}
		this.Dice[2].Land(thirdResult, result);
		AudioManager.PlayClipAtPoint(this.DiceStartSFX.GetRandomClip(-1), this.Dice[2].transform.position, 1f, 1f, 1f, 10f, 250f);
		this.Reward(playerID, firstResult, res, rng.Next());
		this.CurrentState = Library_DiceGame.DiceState.Landed;
		yield return new WaitForSeconds(this.PostRollWait);
		this.ReturnToWaiting();
		yield break;
	}

	// Token: 0x06000D4F RID: 3407 RVA: 0x00055128 File Offset: 0x00053328
	public void Reward(int playerID, int num, Library_DiceGame.Result result, int seed)
	{
		if (result == Library_DiceGame.Result.Single)
		{
			return;
		}
		EntityControl player = PlayerControl.GetPlayer(playerID);
		EffectProperties effectProperties;
		if (player != null)
		{
			effectProperties = new EffectProperties(player);
		}
		else
		{
			effectProperties = new EffectProperties();
			effectProperties.IsWorld = true;
		}
		effectProperties.SourceType = ActionSource.Vignette;
		effectProperties.OverrideSeed(seed, 0);
		EffectProperties effectProperties2 = effectProperties;
		PlayerControl myInstance = PlayerControl.myInstance;
		effectProperties2.AllyTarget = ((myInstance != null) ? myInstance.gameObject : null);
		effectProperties.SetExtra(EProp.DynamicInput, (float)this.GetRewardValue(num, result));
		if (this.RewardSpawn != null)
		{
			effectProperties.StartLoc = new global::Pose(this.RewardSpawn);
			effectProperties.OutLoc = new global::Pose(this.RewardSpawn);
		}
		this.RewardTree.Root.Apply(effectProperties);
	}

	// Token: 0x06000D50 RID: 3408 RVA: 0x000551D8 File Offset: 0x000533D8
	private int GetRewardValue(int v1, Library_DiceGame.Result res)
	{
		int result;
		if (res != Library_DiceGame.Result.Double)
		{
			if (res != Library_DiceGame.Result.Triple)
			{
				result = 0;
			}
			else
			{
				result = this.tripleRewards[v1];
			}
		}
		else
		{
			result = 20;
		}
		return result;
	}

	// Token: 0x06000D51 RID: 3409 RVA: 0x00055204 File Offset: 0x00053404
	private void SimulateDiceGame()
	{
		double num = 0.0;
		int num2 = 110000;
		System.Random random = new System.Random();
		for (int i = 0; i < num2; i++)
		{
			int num3 = random.Next(1, 7);
			int num4 = random.Next(1, 7);
			int num5 = random.Next(1, 7);
			double num6 = 0.0;
			if (num3 == num4 && num4 == num5)
			{
				num6 = (double)this.tripleRewards[num3];
			}
			else if (num3 == num4 || num4 == num5 || num3 == num5)
			{
				num6 = (double)this.pairReward;
			}
			num += num6 - 10.0;
		}
		double num7 = num / (double)num2;
		UnityEngine.Debug.Log(string.Format("After {0} simulations:", num2));
		UnityEngine.Debug.Log(string.Format("  Total net: {0:F2}", num));
		UnityEngine.Debug.Log(string.Format("  Average net per game: {0:F4}", num7));
	}

	// Token: 0x06000D52 RID: 3410 RVA: 0x000552E8 File Offset: 0x000534E8
	private void ResetActivation()
	{
		if (Library_DiceGame.CanAfford)
		{
			this.RollPrompt.Activate();
		}
		else
		{
			this.RollPrompt.Deactivate();
		}
		this.CostText.color = (Library_DiceGame.CanAfford ? this.BaseCostColor : this.UnavailableCostColor);
	}

	// Token: 0x06000D53 RID: 3411 RVA: 0x00055334 File Offset: 0x00053534
	private void ReturnToWaiting()
	{
		this.CurrentState = Library_DiceGame.DiceState.Waiting;
		foreach (Library_Dice library_Dice in this.Dice)
		{
			library_Dice.ReturnToWaiting();
		}
		this.ResetActivation();
		AudioManager.PlayClipAtPoint(this.ReturnToWaitSFX, this.RewardSpawn.position, 1f, 1f, 1f, 10f, 250f);
	}

	// Token: 0x06000D54 RID: 3412 RVA: 0x000553C4 File Offset: 0x000535C4
	private void CheckCurrencyChanged()
	{
		int gildings = Currency.Gildings;
		if (this.currencyCache == gildings)
		{
			return;
		}
		this.currencyCache = gildings;
		if (this.CurrentState == Library_DiceGame.DiceState.Waiting)
		{
			this.ResetActivation();
		}
	}

	// Token: 0x06000D55 RID: 3413 RVA: 0x000553F6 File Offset: 0x000535F6
	private void OnDrawGizmos()
	{
		if (this.AreaOfInfluence == null)
		{
			return;
		}
		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere(this.AreaOfInfluence.position, this.AOIRadius);
	}

	// Token: 0x06000D56 RID: 3414 RVA: 0x00055428 File Offset: 0x00053628
	public Library_DiceGame()
	{
	}

	// Token: 0x04000AD4 RID: 2772
	public static Library_DiceGame instance;

	// Token: 0x04000AD5 RID: 2773
	private int pairReward = 20;

	// Token: 0x04000AD6 RID: 2774
	private int[] tripleRewards = new int[]
	{
		0,
		80,
		90,
		120,
		160,
		200,
		300
	};

	// Token: 0x04000AD7 RID: 2775
	public List<Library_Dice> Dice;

	// Token: 0x04000AD8 RID: 2776
	public AudioClip RollStartSFX;

	// Token: 0x04000AD9 RID: 2777
	public List<AudioClip> DiceStartSFX;

	// Token: 0x04000ADA RID: 2778
	public float RollDuration = 1.5f;

	// Token: 0x04000ADB RID: 2779
	public float DiceDelay = 0.75f;

	// Token: 0x04000ADC RID: 2780
	public float PostRollWait = 2f;

	// Token: 0x04000ADD RID: 2781
	public AudioClip ReturnToWaitSFX;

	// Token: 0x04000ADE RID: 2782
	public SimpleDiagetic RollPrompt;

	// Token: 0x04000ADF RID: 2783
	public CanvasGroup CostGroup;

	// Token: 0x04000AE0 RID: 2784
	public TextMeshProUGUI CostText;

	// Token: 0x04000AE1 RID: 2785
	public Color BaseCostColor;

	// Token: 0x04000AE2 RID: 2786
	public Color UnavailableCostColor;

	// Token: 0x04000AE3 RID: 2787
	public Transform AreaOfInfluence;

	// Token: 0x04000AE4 RID: 2788
	public float AOIRadius = 10f;

	// Token: 0x04000AE5 RID: 2789
	public ActionTree RewardTree;

	// Token: 0x04000AE6 RID: 2790
	public Transform RewardSpawn;

	// Token: 0x04000AE7 RID: 2791
	private int currencyCache;

	// Token: 0x04000AE8 RID: 2792
	public Library_DiceGame.DiceState CurrentState;

	// Token: 0x02000524 RID: 1316
	public enum Result
	{
		// Token: 0x04002601 RID: 9729
		Single,
		// Token: 0x04002602 RID: 9730
		Double,
		// Token: 0x04002603 RID: 9731
		Triple,
		// Token: 0x04002604 RID: 9732
		Bad
	}

	// Token: 0x02000525 RID: 1317
	public enum DiceState
	{
		// Token: 0x04002606 RID: 9734
		Waiting,
		// Token: 0x04002607 RID: 9735
		Rolling,
		// Token: 0x04002608 RID: 9736
		Landed
	}

	// Token: 0x02000526 RID: 1318
	[CompilerGenerated]
	private sealed class <RollSequence>d__32 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x060023ED RID: 9197 RVA: 0x000CC518 File Offset: 0x000CA718
		[DebuggerHidden]
		public <RollSequence>d__32(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x060023EE RID: 9198 RVA: 0x000CC527 File Offset: 0x000CA727
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x060023EF RID: 9199 RVA: 0x000CC52C File Offset: 0x000CA72C
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			Library_DiceGame library_DiceGame = this;
			switch (num)
			{
			case 0:
				this.<>1__state = -1;
				AudioManager.PlayClipAtPoint(library_DiceGame.RollStartSFX, library_DiceGame.Dice[2].transform.position, 1f, 1f, 1f, 10f, 250f);
				library_DiceGame.CurrentState = Library_DiceGame.DiceState.Rolling;
				rng = new System.Random(seed);
				foreach (Library_Dice library_Dice in library_DiceGame.Dice)
				{
					library_Dice.StartRolling();
				}
				firstResult = rng.Next(1, 7);
				secondResult = rng.Next(1, 7);
				thirdResult = rng.Next(1, 7);
				res = Library_DiceGame.Result.Single;
				this.<>2__current = new WaitForSeconds(library_DiceGame.RollDuration);
				this.<>1__state = 1;
				return true;
			case 1:
				this.<>1__state = -1;
				library_DiceGame.Dice[0].Land(firstResult, res);
				AudioManager.PlayClipAtPoint(library_DiceGame.DiceStartSFX.GetRandomClip(-1), library_DiceGame.Dice[0].transform.position, 1f, 1f, 1f, 10f, 250f);
				this.<>2__current = new WaitForSeconds(library_DiceGame.DiceDelay);
				this.<>1__state = 2;
				return true;
			case 2:
				this.<>1__state = -1;
				if (firstResult == secondResult)
				{
					res = Library_DiceGame.Result.Double;
				}
				library_DiceGame.Dice[1].Land(secondResult, res);
				AudioManager.PlayClipAtPoint(library_DiceGame.DiceStartSFX.GetRandomClip(-1), library_DiceGame.Dice[1].transform.position, 1f, 1f, 1f, 10f, 250f);
				this.<>2__current = new WaitForSeconds(library_DiceGame.DiceDelay);
				this.<>1__state = 3;
				return true;
			case 3:
			{
				this.<>1__state = -1;
				Library_DiceGame.Result result = Library_DiceGame.Result.Single;
				if (firstResult == thirdResult || secondResult == thirdResult)
				{
					result = Library_DiceGame.Result.Double;
					res = Library_DiceGame.Result.Double;
				}
				if (firstResult == secondResult && secondResult == thirdResult)
				{
					result = Library_DiceGame.Result.Triple;
					res = Library_DiceGame.Result.Triple;
				}
				library_DiceGame.Dice[2].Land(thirdResult, result);
				AudioManager.PlayClipAtPoint(library_DiceGame.DiceStartSFX.GetRandomClip(-1), library_DiceGame.Dice[2].transform.position, 1f, 1f, 1f, 10f, 250f);
				library_DiceGame.Reward(playerID, firstResult, res, rng.Next());
				library_DiceGame.CurrentState = Library_DiceGame.DiceState.Landed;
				this.<>2__current = new WaitForSeconds(library_DiceGame.PostRollWait);
				this.<>1__state = 4;
				return true;
			}
			case 4:
				this.<>1__state = -1;
				library_DiceGame.ReturnToWaiting();
				return false;
			default:
				return false;
			}
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x060023F0 RID: 9200 RVA: 0x000CC86C File Offset: 0x000CAA6C
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x060023F1 RID: 9201 RVA: 0x000CC874 File Offset: 0x000CAA74
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x060023F2 RID: 9202 RVA: 0x000CC87B File Offset: 0x000CAA7B
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x04002609 RID: 9737
		private int <>1__state;

		// Token: 0x0400260A RID: 9738
		private object <>2__current;

		// Token: 0x0400260B RID: 9739
		public Library_DiceGame <>4__this;

		// Token: 0x0400260C RID: 9740
		public int seed;

		// Token: 0x0400260D RID: 9741
		public int playerID;

		// Token: 0x0400260E RID: 9742
		private System.Random <rng>5__2;

		// Token: 0x0400260F RID: 9743
		private int <firstResult>5__3;

		// Token: 0x04002610 RID: 9744
		private int <secondResult>5__4;

		// Token: 0x04002611 RID: 9745
		private int <thirdResult>5__5;

		// Token: 0x04002612 RID: 9746
		private Library_DiceGame.Result <res>5__6;
	}
}
