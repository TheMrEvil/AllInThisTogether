using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200015F RID: 351
public class DailyQuestUIBox : MonoBehaviour
{
	// Token: 0x1700012B RID: 299
	// (get) Token: 0x06000F54 RID: 3924 RVA: 0x00061437 File Offset: 0x0005F637
	public string QuestID
	{
		get
		{
			MetaDB.DailyQuest dailyQuest = this.questRef;
			return ((dailyQuest != null) ? dailyQuest.ID : null) ?? "INVALID";
		}
	}

	// Token: 0x06000F55 RID: 3925 RVA: 0x00061454 File Offset: 0x0005F654
	public void Setup(MetaDB.DailyQuest quest)
	{
		this.questRef = quest;
		if (this.questRef == null)
		{
			return;
		}
		this.progressRef = Progression.GetQuestProgress(this.questRef.ID);
		this.Title.text = quest.Title;
		this.Detail.text = TextParser.AugmentDetail(quest.Description, null, null, false);
		this.QuestType.text = quest.Timing.ToString();
		this.CollectPrompt.SetActive(this.progressRef.IsComplete && !this.progressRef.IsCollected);
		this.UncollectedDisplay.SetActive(this.progressRef.IsComplete && !this.progressRef.IsCollected);
		this.RerollButton.SetActive(!this.progressRef.IsComplete);
		this.RerollInvisButton.SetActive(!this.progressRef.IsComplete);
		this.RerollCostBox.SetActive(!this.progressRef.IsComplete);
		this.RerollCostText.text = ((quest.Timing == MetaDB.DailyQuest.Timescale.Daily) ? 25 : 100).ToString();
		this.RerollKeybindDisplay.SetActive(InputManager.IsUsingController);
		this.SetupProgressBar();
		this.SetupRewardInfo();
		base.StartCoroutine("FadeInSequence");
	}

	// Token: 0x06000F56 RID: 3926 RVA: 0x000615B4 File Offset: 0x0005F7B4
	private void SetupProgressBar()
	{
		int num = this.questRef.StatTargetValue;
		int num2;
		if (this.questRef.UsesStat)
		{
			if (this.progressRef.IsComplete)
			{
				num2 = num;
			}
			else
			{
				num2 = Mathf.Min((int)GameStats.GetEphemeralStat(this.questRef.StatID), num);
			}
		}
		else
		{
			num = 1;
			num2 = (this.progressRef.IsComplete ? 1 : 0);
		}
		this.ProgressFill.fillAmount = (float)num2 / (float)num;
		this.ProgressText.text = string.Format("{0:N0} <color=#4d431f><font=\"Alegreya_Black\">/ {1:N0}</font></color>", num2, num);
		this.ProgressCompleteDisplay.SetActive(this.progressRef.IsComplete);
	}

	// Token: 0x06000F57 RID: 3927 RVA: 0x00061664 File Offset: 0x0005F864
	private void SetupRewardInfo()
	{
		if (this.questRef.Reward == MetaDB.DailyQuest.RewardType.Quillmarks)
		{
			this.RewardIcon.sprite = this.Quillmark;
		}
		else
		{
			this.RewardIcon.sprite = this.Gilding;
		}
		int rewardValue = MetaDB.GetRewardValue(this.questRef.RewardValue);
		this.RewardValue.text = rewardValue.ToString();
	}

	// Token: 0x06000F58 RID: 3928 RVA: 0x000616C5 File Offset: 0x0005F8C5
	private IEnumerator FadeInSequence()
	{
		this.inIntro = true;
		base.transform.localScale = Vector3.one * Mathf.Epsilon;
		yield return new WaitForSeconds(UnityEngine.Random.Range(0f, 0.33f));
		AudioManager.PlayInterfaceSFX(this.SpawnSFX, 1f, 0f);
		this.BaseGroup.UpdateOpacity(true, 4f, false);
		float t = 0f;
		while (t < 1f)
		{
			t += Time.deltaTime * 1f;
			base.transform.localScale = Vector3.one * this.ScaleInCurve.Evaluate(t);
			yield return true;
		}
		base.transform.localScale = Vector3.one;
		this.BaseGroup.alpha = 1f;
		this.inIntro = false;
		yield break;
	}

	// Token: 0x06000F59 RID: 3929 RVA: 0x000616D4 File Offset: 0x0005F8D4
	public void TestAnimFlow()
	{
		base.StopAllCoroutines();
		base.StartCoroutine("TestAnim");
	}

	// Token: 0x06000F5A RID: 3930 RVA: 0x000616E8 File Offset: 0x0005F8E8
	private IEnumerator TestAnim()
	{
		base.transform.localScale = Vector3.one;
		this.BaseGroup.alpha = 1f;
		this.WhiteFlash.alpha = 0f;
		yield return new WaitForSeconds(0.5f);
		float t = 0f;
		this.WhiteFlash.alpha = 1f;
		while (t < 1f)
		{
			t += Time.deltaTime * 2f;
			this.WhiteFlash.UpdateOpacity(false, 4f, true);
			if (t > 0.5f)
			{
				this.BaseGroup.UpdateOpacity(false, 3f, false);
			}
			base.transform.localScale = Vector3.one * this.TestCurve.Evaluate(t);
			yield return true;
		}
		base.transform.localScale = Vector3.zero;
		yield break;
	}

	// Token: 0x06000F5B RID: 3931 RVA: 0x000616F7 File Offset: 0x0005F8F7
	public void UpdateTimer(string timerText)
	{
		if (this.progressRef.IsComplete)
		{
			return;
		}
		this.QuestType.text = timerText;
	}

	// Token: 0x06000F5C RID: 3932 RVA: 0x00061714 File Offset: 0x0005F914
	public void TryCollect()
	{
		if (!this.progressRef.IsComplete || this.progressRef.IsCollected || this.isDestroying || this.inIntro)
		{
			return;
		}
		Progression.CollectQuest(this.progressRef.ID);
		DailyQuestPanel.instance.QuestCompleted(this.questRef);
		this.CollectPrompt.SetActive(false);
		this.UncollectedDisplay.SetActive(false);
		this.DestroySequence();
	}

	// Token: 0x06000F5D RID: 3933 RVA: 0x0006178C File Offset: 0x0005F98C
	public void TryReroll()
	{
		if (this.isDestroying || this.progressRef.IsCollected || this.progressRef.IsComplete || this.inIntro)
		{
			return;
		}
		int num = (this.questRef.Timing == MetaDB.DailyQuest.Timescale.Daily) ? 25 : 100;
		if (Currency.LoadoutCoin < num)
		{
			return;
		}
		Currency.SpendLoadoutCoin(num, true);
		this.RerollButton.SetActive(false);
		this.RerollCostBox.SetActive(false);
		this.RerollInvisButton.SetActive(false);
		Progression.RemoveQuest(this.questRef.ID);
		LibraryInfoWidget.QuillmarksSpent(num, base.transform);
		this.DestroySequence();
	}

	// Token: 0x06000F5E RID: 3934 RVA: 0x0006182E File Offset: 0x0005FA2E
	public void DestroySequence()
	{
		if (this.isDestroying)
		{
			return;
		}
		this.isDestroying = true;
		this.Button.interactable = false;
		DailyQuestPanel.instance.RemoveBox(this);
		DailyQuestPanel.instance.RefreshDelayed();
		base.StartCoroutine("DestroyTimed");
	}

	// Token: 0x06000F5F RID: 3935 RVA: 0x0006186D File Offset: 0x0005FA6D
	private IEnumerator DestroyTimed()
	{
		float t = 0f;
		this.WhiteFlash.alpha = 1f;
		while (t < 1f)
		{
			t += Time.deltaTime * 2f;
			this.WhiteFlash.UpdateOpacity(false, 4f, true);
			if (t > 0.5f)
			{
				this.BaseGroup.UpdateOpacity(false, 3f, false);
			}
			base.transform.localScale = Vector3.one * this.PopOutCurve.Evaluate(t);
			yield return true;
		}
		base.transform.localScale = Vector3.zero;
		UnityEngine.Object.Destroy(base.gameObject);
		yield break;
	}

	// Token: 0x06000F60 RID: 3936 RVA: 0x0006187C File Offset: 0x0005FA7C
	public DailyQuestUIBox()
	{
	}

	// Token: 0x04000D2D RID: 3373
	public Button Button;

	// Token: 0x04000D2E RID: 3374
	public TextMeshProUGUI Title;

	// Token: 0x04000D2F RID: 3375
	public TextMeshProUGUI Detail;

	// Token: 0x04000D30 RID: 3376
	public TextMeshProUGUI QuestType;

	// Token: 0x04000D31 RID: 3377
	public Image ProgressFill;

	// Token: 0x04000D32 RID: 3378
	public TextMeshProUGUI ProgressText;

	// Token: 0x04000D33 RID: 3379
	public GameObject ProgressCompleteDisplay;

	// Token: 0x04000D34 RID: 3380
	public GameObject RerollButton;

	// Token: 0x04000D35 RID: 3381
	public GameObject RerollInvisButton;

	// Token: 0x04000D36 RID: 3382
	public GameObject RerollKeybindDisplay;

	// Token: 0x04000D37 RID: 3383
	public GameObject RerollCostBox;

	// Token: 0x04000D38 RID: 3384
	public TextMeshProUGUI RerollCostText;

	// Token: 0x04000D39 RID: 3385
	public Image RewardIcon;

	// Token: 0x04000D3A RID: 3386
	public Sprite Quillmark;

	// Token: 0x04000D3B RID: 3387
	public Sprite Gilding;

	// Token: 0x04000D3C RID: 3388
	public TextMeshProUGUI RewardValue;

	// Token: 0x04000D3D RID: 3389
	public GameObject CollectPrompt;

	// Token: 0x04000D3E RID: 3390
	public GameObject UncollectedDisplay;

	// Token: 0x04000D3F RID: 3391
	public AnimationCurve ScaleInCurve;

	// Token: 0x04000D40 RID: 3392
	public AudioClip SpawnSFX;

	// Token: 0x04000D41 RID: 3393
	public AnimationCurve TestCurve;

	// Token: 0x04000D42 RID: 3394
	public AnimationCurve PopOutCurve;

	// Token: 0x04000D43 RID: 3395
	public CanvasGroup BaseGroup;

	// Token: 0x04000D44 RID: 3396
	public CanvasGroup WhiteFlash;

	// Token: 0x04000D45 RID: 3397
	private MetaDB.DailyQuest questRef;

	// Token: 0x04000D46 RID: 3398
	private MetaDB.QuestProgress progressRef;

	// Token: 0x04000D47 RID: 3399
	private bool isDestroying;

	// Token: 0x04000D48 RID: 3400
	private bool inIntro;

	// Token: 0x02000550 RID: 1360
	[CompilerGenerated]
	private sealed class <FadeInSequence>d__33 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x06002464 RID: 9316 RVA: 0x000CDF7D File Offset: 0x000CC17D
		[DebuggerHidden]
		public <FadeInSequence>d__33(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x06002465 RID: 9317 RVA: 0x000CDF8C File Offset: 0x000CC18C
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06002466 RID: 9318 RVA: 0x000CDF90 File Offset: 0x000CC190
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			DailyQuestUIBox dailyQuestUIBox = this;
			switch (num)
			{
			case 0:
				this.<>1__state = -1;
				dailyQuestUIBox.inIntro = true;
				dailyQuestUIBox.transform.localScale = Vector3.one * Mathf.Epsilon;
				this.<>2__current = new WaitForSeconds(UnityEngine.Random.Range(0f, 0.33f));
				this.<>1__state = 1;
				return true;
			case 1:
				this.<>1__state = -1;
				AudioManager.PlayInterfaceSFX(dailyQuestUIBox.SpawnSFX, 1f, 0f);
				dailyQuestUIBox.BaseGroup.UpdateOpacity(true, 4f, false);
				t = 0f;
				break;
			case 2:
				this.<>1__state = -1;
				break;
			default:
				return false;
			}
			if (t >= 1f)
			{
				dailyQuestUIBox.transform.localScale = Vector3.one;
				dailyQuestUIBox.BaseGroup.alpha = 1f;
				dailyQuestUIBox.inIntro = false;
				return false;
			}
			t += Time.deltaTime * 1f;
			dailyQuestUIBox.transform.localScale = Vector3.one * dailyQuestUIBox.ScaleInCurve.Evaluate(t);
			this.<>2__current = true;
			this.<>1__state = 2;
			return true;
		}

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06002467 RID: 9319 RVA: 0x000CE0D4 File Offset: 0x000CC2D4
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x06002468 RID: 9320 RVA: 0x000CE0DC File Offset: 0x000CC2DC
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06002469 RID: 9321 RVA: 0x000CE0E3 File Offset: 0x000CC2E3
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x040026B1 RID: 9905
		private int <>1__state;

		// Token: 0x040026B2 RID: 9906
		private object <>2__current;

		// Token: 0x040026B3 RID: 9907
		public DailyQuestUIBox <>4__this;

		// Token: 0x040026B4 RID: 9908
		private float <t>5__2;
	}

	// Token: 0x02000551 RID: 1361
	[CompilerGenerated]
	private sealed class <TestAnim>d__35 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x0600246A RID: 9322 RVA: 0x000CE0EB File Offset: 0x000CC2EB
		[DebuggerHidden]
		public <TestAnim>d__35(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x0600246B RID: 9323 RVA: 0x000CE0FA File Offset: 0x000CC2FA
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x0600246C RID: 9324 RVA: 0x000CE0FC File Offset: 0x000CC2FC
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			DailyQuestUIBox dailyQuestUIBox = this;
			switch (num)
			{
			case 0:
				this.<>1__state = -1;
				dailyQuestUIBox.transform.localScale = Vector3.one;
				dailyQuestUIBox.BaseGroup.alpha = 1f;
				dailyQuestUIBox.WhiteFlash.alpha = 0f;
				this.<>2__current = new WaitForSeconds(0.5f);
				this.<>1__state = 1;
				return true;
			case 1:
				this.<>1__state = -1;
				t = 0f;
				dailyQuestUIBox.WhiteFlash.alpha = 1f;
				break;
			case 2:
				this.<>1__state = -1;
				break;
			default:
				return false;
			}
			if (t >= 1f)
			{
				dailyQuestUIBox.transform.localScale = Vector3.zero;
				return false;
			}
			t += Time.deltaTime * 2f;
			dailyQuestUIBox.WhiteFlash.UpdateOpacity(false, 4f, true);
			if (t > 0.5f)
			{
				dailyQuestUIBox.BaseGroup.UpdateOpacity(false, 3f, false);
			}
			dailyQuestUIBox.transform.localScale = Vector3.one * dailyQuestUIBox.TestCurve.Evaluate(t);
			this.<>2__current = true;
			this.<>1__state = 2;
			return true;
		}

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x0600246D RID: 9325 RVA: 0x000CE24E File Offset: 0x000CC44E
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x0600246E RID: 9326 RVA: 0x000CE256 File Offset: 0x000CC456
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x0600246F RID: 9327 RVA: 0x000CE25D File Offset: 0x000CC45D
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x040026B5 RID: 9909
		private int <>1__state;

		// Token: 0x040026B6 RID: 9910
		private object <>2__current;

		// Token: 0x040026B7 RID: 9911
		public DailyQuestUIBox <>4__this;

		// Token: 0x040026B8 RID: 9912
		private float <t>5__2;
	}

	// Token: 0x02000552 RID: 1362
	[CompilerGenerated]
	private sealed class <DestroyTimed>d__40 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x06002470 RID: 9328 RVA: 0x000CE265 File Offset: 0x000CC465
		[DebuggerHidden]
		public <DestroyTimed>d__40(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x06002471 RID: 9329 RVA: 0x000CE274 File Offset: 0x000CC474
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06002472 RID: 9330 RVA: 0x000CE278 File Offset: 0x000CC478
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			DailyQuestUIBox dailyQuestUIBox = this;
			if (num != 0)
			{
				if (num != 1)
				{
					return false;
				}
				this.<>1__state = -1;
			}
			else
			{
				this.<>1__state = -1;
				t = 0f;
				dailyQuestUIBox.WhiteFlash.alpha = 1f;
			}
			if (t >= 1f)
			{
				dailyQuestUIBox.transform.localScale = Vector3.zero;
				UnityEngine.Object.Destroy(dailyQuestUIBox.gameObject);
				return false;
			}
			t += Time.deltaTime * 2f;
			dailyQuestUIBox.WhiteFlash.UpdateOpacity(false, 4f, true);
			if (t > 0.5f)
			{
				dailyQuestUIBox.BaseGroup.UpdateOpacity(false, 3f, false);
			}
			dailyQuestUIBox.transform.localScale = Vector3.one * dailyQuestUIBox.PopOutCurve.Evaluate(t);
			this.<>2__current = true;
			this.<>1__state = 1;
			return true;
		}

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06002473 RID: 9331 RVA: 0x000CE37D File Offset: 0x000CC57D
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x06002474 RID: 9332 RVA: 0x000CE385 File Offset: 0x000CC585
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06002475 RID: 9333 RVA: 0x000CE38C File Offset: 0x000CC58C
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x040026B9 RID: 9913
		private int <>1__state;

		// Token: 0x040026BA RID: 9914
		private object <>2__current;

		// Token: 0x040026BB RID: 9915
		public DailyQuestUIBox <>4__this;

		// Token: 0x040026BC RID: 9916
		private float <t>5__2;
	}
}
