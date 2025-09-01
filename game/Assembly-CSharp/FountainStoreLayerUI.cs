using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001AB RID: 427
public class FountainStoreLayerUI : MonoBehaviour
{
	// Token: 0x060011B2 RID: 4530 RVA: 0x0006DE4E File Offset: 0x0006C04E
	public void Setup(InkRow row)
	{
		this.Row = row;
		this.hasUnlocked = this.Row.IsUnlocked;
		base.StopAllCoroutines();
		this.UpdateDisplay(true);
	}

	// Token: 0x060011B3 RID: 4531 RVA: 0x0006DE78 File Offset: 0x0006C078
	public void UpdateDisplay(bool fromSetup = false)
	{
		bool isUnlocked = this.Row.IsUnlocked;
		if (fromSetup)
		{
			this.LockDisplay.SetActive(!isUnlocked);
		}
		else if (isUnlocked && !this.hasUnlocked)
		{
			this.Unlock();
		}
		if (!isUnlocked)
		{
			this.CountText.text = this.Row.RemainingForUnlock.ToString();
			base.StartCoroutine("DelayedRebuild");
		}
		this.LockDisplay.transform.SetAsLastSibling();
		LayoutRebuilder.ForceRebuildLayoutImmediate(base.GetComponent<RectTransform>());
	}

	// Token: 0x060011B4 RID: 4532 RVA: 0x0006DEFE File Offset: 0x0006C0FE
	private IEnumerator DelayedRebuild()
	{
		yield return true;
		LayoutRebuilder.ForceRebuildLayoutImmediate(this.BannerRect);
		yield break;
	}

	// Token: 0x060011B5 RID: 4533 RVA: 0x0006DF10 File Offset: 0x0006C110
	public void UpdateAllItems()
	{
		foreach (FountainStoreItemUI fountainStoreItemUI in this.bars)
		{
			fountainStoreItemUI.UpdateDisplay(false);
		}
	}

	// Token: 0x060011B6 RID: 4534 RVA: 0x0006DF64 File Offset: 0x0006C164
	public void UpdateItem(InkTalent option)
	{
		foreach (FountainStoreItemUI fountainStoreItemUI in this.bars)
		{
			if (fountainStoreItemUI.Option == option)
			{
				fountainStoreItemUI.UpdateDisplay(false);
			}
		}
	}

	// Token: 0x060011B7 RID: 4535 RVA: 0x0006DFC0 File Offset: 0x0006C1C0
	public void CreateItems()
	{
		Transform transform = base.transform;
		foreach (InkTalent option in this.Row.Options)
		{
			this.CreateNewOption(option, transform);
		}
		this.UpdateDisplay(false);
		UISelector.SetupHorizontalListNav<FountainStoreItemUI>(this.bars, null, null, false);
	}

	// Token: 0x060011B8 RID: 4536 RVA: 0x0006E038 File Offset: 0x0006C238
	private void CreateNewOption(InkTalent option, Transform layer)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.BarRef, layer);
		gameObject.SetActive(true);
		FountainStoreItemUI component = gameObject.GetComponent<FountainStoreItemUI>();
		component.Setup(option);
		this.bars.Add(component);
	}

	// Token: 0x060011B9 RID: 4537 RVA: 0x0006E074 File Offset: 0x0006C274
	private void Unlock()
	{
		this.hasUnlocked = true;
		this.Anim.Play("Release");
		this.UnlockFX.Play();
		base.StartCoroutine(this.UnlockRoutine());
		AudioManager.PlayInterfaceSFX(this.UnlockSFX, 1f, 0f);
	}

	// Token: 0x060011BA RID: 4538 RVA: 0x0006E0C5 File Offset: 0x0006C2C5
	private IEnumerator UnlockRoutine()
	{
		float t = 1f;
		while (t > 0f)
		{
			t -= Time.deltaTime;
			foreach (FountainStoreItemUI fountainStoreItemUI in this.bars)
			{
				fountainStoreItemUI.UpdateToUnlocked();
			}
			yield return true;
		}
		yield break;
	}

	// Token: 0x060011BB RID: 4539 RVA: 0x0006E0D4 File Offset: 0x0006C2D4
	public FountainStoreLayerUI()
	{
	}

	// Token: 0x04001063 RID: 4195
	public GameObject LockDisplay;

	// Token: 0x04001064 RID: 4196
	public RectTransform BannerRect;

	// Token: 0x04001065 RID: 4197
	public TextMeshProUGUI CountText;

	// Token: 0x04001066 RID: 4198
	public Animator Anim;

	// Token: 0x04001067 RID: 4199
	public AudioClip UnlockSFX;

	// Token: 0x04001068 RID: 4200
	private bool hasUnlocked;

	// Token: 0x04001069 RID: 4201
	public GameObject BarRef;

	// Token: 0x0400106A RID: 4202
	public ParticleSystem UnlockFX;

	// Token: 0x0400106B RID: 4203
	[NonSerialized]
	public List<FountainStoreItemUI> bars = new List<FountainStoreItemUI>();

	// Token: 0x0400106C RID: 4204
	public InkRow Row;

	// Token: 0x02000572 RID: 1394
	[CompilerGenerated]
	private sealed class <DelayedRebuild>d__12 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x060024EF RID: 9455 RVA: 0x000CFC37 File Offset: 0x000CDE37
		[DebuggerHidden]
		public <DelayedRebuild>d__12(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x060024F0 RID: 9456 RVA: 0x000CFC46 File Offset: 0x000CDE46
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x060024F1 RID: 9457 RVA: 0x000CFC48 File Offset: 0x000CDE48
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			FountainStoreLayerUI fountainStoreLayerUI = this;
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
			LayoutRebuilder.ForceRebuildLayoutImmediate(fountainStoreLayerUI.BannerRect);
			return false;
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x060024F2 RID: 9458 RVA: 0x000CFC9B File Offset: 0x000CDE9B
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x060024F3 RID: 9459 RVA: 0x000CFCA3 File Offset: 0x000CDEA3
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x060024F4 RID: 9460 RVA: 0x000CFCAA File Offset: 0x000CDEAA
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x0400272E RID: 10030
		private int <>1__state;

		// Token: 0x0400272F RID: 10031
		private object <>2__current;

		// Token: 0x04002730 RID: 10032
		public FountainStoreLayerUI <>4__this;
	}

	// Token: 0x02000573 RID: 1395
	[CompilerGenerated]
	private sealed class <UnlockRoutine>d__18 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x060024F5 RID: 9461 RVA: 0x000CFCB2 File Offset: 0x000CDEB2
		[DebuggerHidden]
		public <UnlockRoutine>d__18(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x060024F6 RID: 9462 RVA: 0x000CFCC1 File Offset: 0x000CDEC1
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x060024F7 RID: 9463 RVA: 0x000CFCC4 File Offset: 0x000CDEC4
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			FountainStoreLayerUI fountainStoreLayerUI = this;
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
				t = 1f;
			}
			if (t <= 0f)
			{
				return false;
			}
			t -= Time.deltaTime;
			foreach (FountainStoreItemUI fountainStoreItemUI in fountainStoreLayerUI.bars)
			{
				fountainStoreItemUI.UpdateToUnlocked();
			}
			this.<>2__current = true;
			this.<>1__state = 1;
			return true;
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x060024F8 RID: 9464 RVA: 0x000CFD7C File Offset: 0x000CDF7C
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x060024F9 RID: 9465 RVA: 0x000CFD84 File Offset: 0x000CDF84
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x060024FA RID: 9466 RVA: 0x000CFD8B File Offset: 0x000CDF8B
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x04002731 RID: 10033
		private int <>1__state;

		// Token: 0x04002732 RID: 10034
		private object <>2__current;

		// Token: 0x04002733 RID: 10035
		public FountainStoreLayerUI <>4__this;

		// Token: 0x04002734 RID: 10036
		private float <t>5__2;
	}
}
