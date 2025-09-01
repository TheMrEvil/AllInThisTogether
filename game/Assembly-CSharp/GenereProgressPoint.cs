using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000154 RID: 340
public class GenereProgressPoint : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	// Token: 0x06000F10 RID: 3856 RVA: 0x0005FA60 File Offset: 0x0005DC60
	public void Setup(GenreWaveNode wave, bool showFountainChoice, float wantPoint, int waveID)
	{
		this.cgroup = base.GetComponent<CanvasGroup>();
		this.WaveIndex = waveID;
		this.ProgressPoint = wantPoint;
		this.CurrentPoint = this.ProgressPoint;
		this.parent = base.transform.parent.GetComponent<RectTransform>();
		this.UpdateRelativeLocation();
		this.Augment = null;
	}

	// Token: 0x06000F11 RID: 3857 RVA: 0x0005FAB7 File Offset: 0x0005DCB7
	private void Update()
	{
		if (!GenreProgressBar.ShouldShow)
		{
			return;
		}
		this.UpdateRelativeLocation();
	}

	// Token: 0x06000F12 RID: 3858 RVA: 0x0005FAC7 File Offset: 0x0005DCC7
	public void MoveIn()
	{
		base.StopAllCoroutines();
		base.StartCoroutine("MoveInSequence");
	}

	// Token: 0x06000F13 RID: 3859 RVA: 0x0005FADB File Offset: 0x0005DCDB
	public void MoveOut(int CenterWave)
	{
		base.StopAllCoroutines();
		base.StartCoroutine(this.MoveOutSequence(CenterWave));
	}

	// Token: 0x06000F14 RID: 3860 RVA: 0x0005FAF1 File Offset: 0x0005DCF1
	private IEnumerator MoveInSequence()
	{
		while (Mathf.Abs(this.CurrentPoint - this.ProgressPoint) > 0f)
		{
			this.CurrentPoint = Mathf.MoveTowards(this.CurrentPoint, this.ProgressPoint, Mathf.Max(Mathf.Abs(this.CurrentPoint - this.ProgressPoint) * Time.deltaTime * 2f, 0.05f * Time.deltaTime));
			this.cgroup.alpha = Mathf.MoveTowards(this.cgroup.alpha, 1f, Time.deltaTime * 3f);
			yield return true;
		}
		this.CurrentPoint = this.ProgressPoint;
		this.cgroup.alpha = 1f;
		yield return true;
		yield break;
	}

	// Token: 0x06000F15 RID: 3861 RVA: 0x0005FB00 File Offset: 0x0005DD00
	private IEnumerator MoveOutSequence(int waveNum)
	{
		float desired = (float)((waveNum >= this.WaveIndex) ? 0 : 1);
		bool wantVisible = this.WaveIndex == waveNum || this.WaveIndex == waveNum + 1;
		while (Mathf.Abs(this.CurrentPoint - desired) > 0f)
		{
			this.CurrentPoint = Mathf.MoveTowards(this.CurrentPoint, desired, Mathf.Max(Mathf.Abs(this.CurrentPoint - desired) * Time.deltaTime * 3f, 0.05f * Time.deltaTime));
			this.cgroup.alpha = Mathf.MoveTowards(this.cgroup.alpha, (float)(wantVisible ? 1 : 0), Time.deltaTime * 3f);
			yield return true;
		}
		this.CurrentPoint = desired;
		this.cgroup.alpha = (float)(wantVisible ? 1 : 0);
		yield return true;
		yield break;
	}

	// Token: 0x06000F16 RID: 3862 RVA: 0x0005FB18 File Offset: 0x0005DD18
	private void UpdateRelativeLocation()
	{
		float num = (this.parent.rect.width - 0f) * this.CurrentPoint + 0f;
		if (this.rect.anchoredPosition.x != num)
		{
			this.rect.anchoredPosition = new Vector2(num, 0f);
		}
	}

	// Token: 0x06000F17 RID: 3863 RVA: 0x0005FB75 File Offset: 0x0005DD75
	public void OnPointerEnter(PointerEventData p)
	{
		if (this.Augment == null)
		{
			return;
		}
		Tooltip.Show(this.TooltipAnchor.position, TextAnchor.UpperCenter, this.Augment, 1, null);
	}

	// Token: 0x06000F18 RID: 3864 RVA: 0x0005FB9F File Offset: 0x0005DD9F
	public void OnPointerExit(PointerEventData p)
	{
		Tooltip.Release();
	}

	// Token: 0x06000F19 RID: 3865 RVA: 0x0005FBA8 File Offset: 0x0005DDA8
	public void SetChoice(AugmentTree augment)
	{
		this.ChoiceIcon.gameObject.SetActive(true);
		this.detailImage.gameObject.SetActive(false);
		this.ChoiceIcon.sprite = augment.Root.Icon;
		this.Augment = augment;
	}

	// Token: 0x06000F1A RID: 3866 RVA: 0x0005FBF4 File Offset: 0x0005DDF4
	public GenereProgressPoint()
	{
	}

	// Token: 0x04000CC5 RID: 3269
	private const float OFFSET = 0f;

	// Token: 0x04000CC6 RID: 3270
	[Header("Config")]
	public RectTransform rect;

	// Token: 0x04000CC7 RID: 3271
	public Image img;

	// Token: 0x04000CC8 RID: 3272
	public Image ChoiceIcon;

	// Token: 0x04000CC9 RID: 3273
	public Image detailImage;

	// Token: 0x04000CCA RID: 3274
	private CanvasGroup cgroup;

	// Token: 0x04000CCB RID: 3275
	[Header("Options")]
	public Sprite NewActSprite;

	// Token: 0x04000CCC RID: 3276
	public Vector2 NewActSize;

	// Token: 0x04000CCD RID: 3277
	public Sprite fountainPower;

	// Token: 0x04000CCE RID: 3278
	public ParticleSystem PulseFX;

	// Token: 0x04000CCF RID: 3279
	private RectTransform parent;

	// Token: 0x04000CD0 RID: 3280
	public Transform TooltipAnchor;

	// Token: 0x04000CD1 RID: 3281
	public int WaveIndex;

	// Token: 0x04000CD2 RID: 3282
	private float CurrentPoint;

	// Token: 0x04000CD3 RID: 3283
	private float ProgressPoint;

	// Token: 0x04000CD4 RID: 3284
	private AugmentTree Augment;

	// Token: 0x0200054B RID: 1355
	[CompilerGenerated]
	private sealed class <MoveInSequence>d__20 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x06002451 RID: 9297 RVA: 0x000CDC1A File Offset: 0x000CBE1A
		[DebuggerHidden]
		public <MoveInSequence>d__20(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x06002452 RID: 9298 RVA: 0x000CDC29 File Offset: 0x000CBE29
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06002453 RID: 9299 RVA: 0x000CDC2C File Offset: 0x000CBE2C
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			GenereProgressPoint genereProgressPoint = this;
			switch (num)
			{
			case 0:
				this.<>1__state = -1;
				break;
			case 1:
				this.<>1__state = -1;
				break;
			case 2:
				this.<>1__state = -1;
				return false;
			default:
				return false;
			}
			if (Mathf.Abs(genereProgressPoint.CurrentPoint - genereProgressPoint.ProgressPoint) <= 0f)
			{
				genereProgressPoint.CurrentPoint = genereProgressPoint.ProgressPoint;
				genereProgressPoint.cgroup.alpha = 1f;
				this.<>2__current = true;
				this.<>1__state = 2;
				return true;
			}
			genereProgressPoint.CurrentPoint = Mathf.MoveTowards(genereProgressPoint.CurrentPoint, genereProgressPoint.ProgressPoint, Mathf.Max(Mathf.Abs(genereProgressPoint.CurrentPoint - genereProgressPoint.ProgressPoint) * Time.deltaTime * 2f, 0.05f * Time.deltaTime));
			genereProgressPoint.cgroup.alpha = Mathf.MoveTowards(genereProgressPoint.cgroup.alpha, 1f, Time.deltaTime * 3f);
			this.<>2__current = true;
			this.<>1__state = 1;
			return true;
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06002454 RID: 9300 RVA: 0x000CDD48 File Offset: 0x000CBF48
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x06002455 RID: 9301 RVA: 0x000CDD50 File Offset: 0x000CBF50
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06002456 RID: 9302 RVA: 0x000CDD57 File Offset: 0x000CBF57
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x040026A0 RID: 9888
		private int <>1__state;

		// Token: 0x040026A1 RID: 9889
		private object <>2__current;

		// Token: 0x040026A2 RID: 9890
		public GenereProgressPoint <>4__this;
	}

	// Token: 0x0200054C RID: 1356
	[CompilerGenerated]
	private sealed class <MoveOutSequence>d__21 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x06002457 RID: 9303 RVA: 0x000CDD5F File Offset: 0x000CBF5F
		[DebuggerHidden]
		public <MoveOutSequence>d__21(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x06002458 RID: 9304 RVA: 0x000CDD6E File Offset: 0x000CBF6E
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06002459 RID: 9305 RVA: 0x000CDD70 File Offset: 0x000CBF70
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			GenereProgressPoint genereProgressPoint = this;
			switch (num)
			{
			case 0:
				this.<>1__state = -1;
				desired = (float)((waveNum >= genereProgressPoint.WaveIndex) ? 0 : 1);
				wantVisible = (genereProgressPoint.WaveIndex == waveNum || genereProgressPoint.WaveIndex == waveNum + 1);
				break;
			case 1:
				this.<>1__state = -1;
				break;
			case 2:
				this.<>1__state = -1;
				return false;
			default:
				return false;
			}
			if (Mathf.Abs(genereProgressPoint.CurrentPoint - desired) <= 0f)
			{
				genereProgressPoint.CurrentPoint = desired;
				genereProgressPoint.cgroup.alpha = (float)(wantVisible ? 1 : 0);
				this.<>2__current = true;
				this.<>1__state = 2;
				return true;
			}
			genereProgressPoint.CurrentPoint = Mathf.MoveTowards(genereProgressPoint.CurrentPoint, desired, Mathf.Max(Mathf.Abs(genereProgressPoint.CurrentPoint - desired) * Time.deltaTime * 3f, 0.05f * Time.deltaTime));
			genereProgressPoint.cgroup.alpha = Mathf.MoveTowards(genereProgressPoint.cgroup.alpha, (float)(wantVisible ? 1 : 0), Time.deltaTime * 3f);
			this.<>2__current = true;
			this.<>1__state = 1;
			return true;
		}

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x0600245A RID: 9306 RVA: 0x000CDEDC File Offset: 0x000CC0DC
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x0600245B RID: 9307 RVA: 0x000CDEE4 File Offset: 0x000CC0E4
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x0600245C RID: 9308 RVA: 0x000CDEEB File Offset: 0x000CC0EB
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x040026A3 RID: 9891
		private int <>1__state;

		// Token: 0x040026A4 RID: 9892
		private object <>2__current;

		// Token: 0x040026A5 RID: 9893
		public int waveNum;

		// Token: 0x040026A6 RID: 9894
		public GenereProgressPoint <>4__this;

		// Token: 0x040026A7 RID: 9895
		private float <desired>5__2;

		// Token: 0x040026A8 RID: 9896
		private bool <wantVisible>5__3;
	}
}
