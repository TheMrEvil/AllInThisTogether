using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001CC RID: 460
public class VignetteInfoDisplay : MonoBehaviour
{
	// Token: 0x060012CB RID: 4811 RVA: 0x00073E59 File Offset: 0x00072059
	private void Awake()
	{
		VignetteInfoDisplay.instance = this;
		base.gameObject.SetActive(false);
	}

	// Token: 0x060012CC RID: 4812 RVA: 0x00073E70 File Offset: 0x00072070
	public void Setup(string title, string detail)
	{
		this.HeaderTitle.text = (RaidManager.IsInRaid ? "Raid Vignette" : "Tome Vignette");
		this.Title.text = title;
		this.Detail.text = detail;
		Image tomeIcon = this.TomeIcon;
		Sprite sprite;
		if (!RaidManager.IsInRaid)
		{
			GenreRootNode curTomeRoot = GameplayManager.CurTomeRoot;
			sprite = ((curTomeRoot != null) ? curTomeRoot.Icon : null);
		}
		else
		{
			sprite = ((RaidManager.instance.Difficulty == RaidDB.Difficulty.Hard) ? this.RaidIconHard : this.RaidIcon);
		}
		tomeIcon.sprite = sprite;
		this.Fader.alpha = 1f;
		this.didRelease = false;
		base.gameObject.SetActive(true);
		AudioManager.PlayInterfaceSFX(this.IntroSFX, 1f, 0f);
		this.Anim.Play();
		base.StartCoroutine("RebuildTextLayout");
	}

	// Token: 0x060012CD RID: 4813 RVA: 0x00073F43 File Offset: 0x00072143
	private void Update()
	{
		if (!GoalManager.InVignette)
		{
			UnityEngine.Debug.Log("Releasing Vignette UI");
			this.Release();
		}
	}

	// Token: 0x060012CE RID: 4814 RVA: 0x00073F5C File Offset: 0x0007215C
	public static void UpdateDetail(string detail)
	{
		if (VignetteInfoDisplay.instance == null)
		{
			return;
		}
		VignetteInfoDisplay.instance.Detail.text = detail;
		VignetteInfoDisplay.instance.StartCoroutine("RebuildTextLayout");
	}

	// Token: 0x060012CF RID: 4815 RVA: 0x00073F8C File Offset: 0x0007218C
	private IEnumerator RebuildTextLayout()
	{
		yield return true;
		LayoutRebuilder.ForceRebuildLayoutImmediate(this.HorizontalGroup);
		yield break;
	}

	// Token: 0x060012D0 RID: 4816 RVA: 0x00073F9B File Offset: 0x0007219B
	public void Release()
	{
		if (this.didRelease)
		{
			return;
		}
		this.didRelease = true;
		this.Fader.alpha = 1f;
		base.StopAllCoroutines();
		base.StartCoroutine("FadeOut");
	}

	// Token: 0x060012D1 RID: 4817 RVA: 0x00073FCF File Offset: 0x000721CF
	private IEnumerator FadeOut()
	{
		float t = 0f;
		while (t < 1f)
		{
			t += Time.deltaTime;
			this.Fader.UpdateOpacity(false, 4f, true);
			yield return true;
		}
		base.gameObject.SetActive(false);
		yield break;
	}

	// Token: 0x060012D2 RID: 4818 RVA: 0x00073FDE File Offset: 0x000721DE
	public VignetteInfoDisplay()
	{
	}

	// Token: 0x040011ED RID: 4589
	public static VignetteInfoDisplay instance;

	// Token: 0x040011EE RID: 4590
	public Animation Anim;

	// Token: 0x040011EF RID: 4591
	public CanvasGroup Fader;

	// Token: 0x040011F0 RID: 4592
	public TextMeshProUGUI HeaderTitle;

	// Token: 0x040011F1 RID: 4593
	public Image TomeIcon;

	// Token: 0x040011F2 RID: 4594
	public Sprite RaidIcon;

	// Token: 0x040011F3 RID: 4595
	public Sprite RaidIconHard;

	// Token: 0x040011F4 RID: 4596
	public TextMeshProUGUI Title;

	// Token: 0x040011F5 RID: 4597
	public TextMeshProUGUI Detail;

	// Token: 0x040011F6 RID: 4598
	public RectTransform HorizontalGroup;

	// Token: 0x040011F7 RID: 4599
	public AudioClip IntroSFX;

	// Token: 0x040011F8 RID: 4600
	private bool didRelease;

	// Token: 0x02000589 RID: 1417
	[CompilerGenerated]
	private sealed class <RebuildTextLayout>d__16 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x0600254A RID: 9546 RVA: 0x000D11B6 File Offset: 0x000CF3B6
		[DebuggerHidden]
		public <RebuildTextLayout>d__16(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x0600254B RID: 9547 RVA: 0x000D11C5 File Offset: 0x000CF3C5
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x0600254C RID: 9548 RVA: 0x000D11C8 File Offset: 0x000CF3C8
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			VignetteInfoDisplay vignetteInfoDisplay = this;
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
			LayoutRebuilder.ForceRebuildLayoutImmediate(vignetteInfoDisplay.HorizontalGroup);
			return false;
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x0600254D RID: 9549 RVA: 0x000D121B File Offset: 0x000CF41B
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x0600254E RID: 9550 RVA: 0x000D1223 File Offset: 0x000CF423
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x0600254F RID: 9551 RVA: 0x000D122A File Offset: 0x000CF42A
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x04002797 RID: 10135
		private int <>1__state;

		// Token: 0x04002798 RID: 10136
		private object <>2__current;

		// Token: 0x04002799 RID: 10137
		public VignetteInfoDisplay <>4__this;
	}

	// Token: 0x0200058A RID: 1418
	[CompilerGenerated]
	private sealed class <FadeOut>d__18 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x06002550 RID: 9552 RVA: 0x000D1232 File Offset: 0x000CF432
		[DebuggerHidden]
		public <FadeOut>d__18(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x06002551 RID: 9553 RVA: 0x000D1241 File Offset: 0x000CF441
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06002552 RID: 9554 RVA: 0x000D1244 File Offset: 0x000CF444
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			VignetteInfoDisplay vignetteInfoDisplay = this;
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
			}
			if (t >= 1f)
			{
				vignetteInfoDisplay.gameObject.SetActive(false);
				return false;
			}
			t += Time.deltaTime;
			vignetteInfoDisplay.Fader.UpdateOpacity(false, 4f, true);
			this.<>2__current = true;
			this.<>1__state = 1;
			return true;
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06002553 RID: 9555 RVA: 0x000D12D6 File Offset: 0x000CF4D6
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x06002554 RID: 9556 RVA: 0x000D12DE File Offset: 0x000CF4DE
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06002555 RID: 9557 RVA: 0x000D12E5 File Offset: 0x000CF4E5
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x0400279A RID: 10138
		private int <>1__state;

		// Token: 0x0400279B RID: 10139
		private object <>2__current;

		// Token: 0x0400279C RID: 10140
		public VignetteInfoDisplay <>4__this;

		// Token: 0x0400279D RID: 10141
		private float <t>5__2;
	}
}
