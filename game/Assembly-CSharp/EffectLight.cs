using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x0200024B RID: 587
[RequireComponent(typeof(Light))]
public class EffectLight : MonoBehaviour
{
	// Token: 0x060017D1 RID: 6097 RVA: 0x00095320 File Offset: 0x00093520
	private void OnValidate()
	{
		if (Application.isPlaying)
		{
			return;
		}
		if (this.l == null || this.l.Length == 0 || this.l.Contains(null))
		{
			this.l = base.GetComponents<Light>();
		}
		if (this.f == null || this.f.Length == 0 || this.f.Contains(null))
		{
			this.f = base.GetComponents<LensFlare>();
		}
	}

	// Token: 0x060017D2 RID: 6098 RVA: 0x0009538C File Offset: 0x0009358C
	private void Awake()
	{
		if (this.l == null && this.f == null)
		{
			return;
		}
		float val = this.InCurve.Evaluate(0f);
		if (this.l != null)
		{
			Light[] array = this.l;
			for (int i = 0; i < array.Length; i++)
			{
				EffectLight.Comp comp = new EffectLight.Comp(array[i]);
				this.parts.Add(comp);
				comp.UpdateBrightness(val);
			}
		}
		if (this.f != null)
		{
			LensFlare[] array2 = this.f;
			for (int i = 0; i < array2.Length; i++)
			{
				EffectLight.Comp comp2 = new EffectLight.Comp(array2[i]);
				this.parts.Add(comp2);
				comp2.UpdateBrightness(val);
			}
		}
		if (this.RevealOnStart)
		{
			this.Activate();
		}
	}

	// Token: 0x060017D3 RID: 6099 RVA: 0x00095441 File Offset: 0x00093641
	private void OnEnable()
	{
		if (this.RevealOnStart)
		{
			this.Activate();
		}
	}

	// Token: 0x060017D4 RID: 6100 RVA: 0x00095451 File Offset: 0x00093651
	public void Activate()
	{
		if (this.isRevealed)
		{
			return;
		}
		this.isRevealed = true;
		base.StopAllCoroutines();
		base.StartCoroutine("FadeIn");
	}

	// Token: 0x060017D5 RID: 6101 RVA: 0x00095475 File Offset: 0x00093675
	public void Deactivate()
	{
		if (!this.isRevealed || !base.gameObject.activeSelf)
		{
			return;
		}
		base.StopAllCoroutines();
		base.StartCoroutine("FadeOut");
	}

	// Token: 0x060017D6 RID: 6102 RVA: 0x0009549F File Offset: 0x0009369F
	private IEnumerator FadeIn()
	{
		float t = 0f;
		while (t < 1f)
		{
			t += Time.deltaTime / this.InTime;
			foreach (EffectLight.Comp comp in this.parts)
			{
				comp.UpdateBrightness(this.InCurve.Evaluate(t));
			}
			yield return true;
		}
		foreach (EffectLight.Comp comp2 in this.parts)
		{
			comp2.UpdateBrightness(1f);
		}
		if (this.AutoFade)
		{
			yield return this.FadeOut();
		}
		yield break;
	}

	// Token: 0x060017D7 RID: 6103 RVA: 0x000954AE File Offset: 0x000936AE
	private IEnumerator FadeOut()
	{
		float t = 0f;
		while (t <= 1f)
		{
			t += Time.deltaTime / this.OutTime;
			foreach (EffectLight.Comp comp in this.parts)
			{
				comp.UpdateBrightness(this.OutCurve.Evaluate(t));
			}
			yield return true;
		}
		foreach (EffectLight.Comp comp2 in this.parts)
		{
			comp2.UpdateBrightness(0f);
		}
		this.isRevealed = false;
		yield break;
	}

	// Token: 0x060017D8 RID: 6104 RVA: 0x000954C0 File Offset: 0x000936C0
	private void OnDisable()
	{
		this.isRevealed = false;
		foreach (EffectLight.Comp comp in this.parts)
		{
			comp.UpdateBrightness(0f);
		}
	}

	// Token: 0x060017D9 RID: 6105 RVA: 0x0009551C File Offset: 0x0009371C
	public EffectLight()
	{
	}

	// Token: 0x040017A3 RID: 6051
	public bool RevealOnStart = true;

	// Token: 0x040017A4 RID: 6052
	public float InTime = 0.1f;

	// Token: 0x040017A5 RID: 6053
	public AnimationCurve InCurve = new AnimationCurve(new Keyframe[]
	{
		new Keyframe(0f, 0f),
		new Keyframe(1f, 1f)
	});

	// Token: 0x040017A6 RID: 6054
	public bool AutoFade;

	// Token: 0x040017A7 RID: 6055
	public float OutTime = 0.3f;

	// Token: 0x040017A8 RID: 6056
	public AnimationCurve OutCurve = new AnimationCurve(new Keyframe[]
	{
		new Keyframe(0f, 1f),
		new Keyframe(1f, 0f)
	});

	// Token: 0x040017A9 RID: 6057
	[SerializeField]
	private Light[] l = Array.Empty<Light>();

	// Token: 0x040017AA RID: 6058
	[SerializeField]
	private LensFlare[] f = Array.Empty<LensFlare>();

	// Token: 0x040017AB RID: 6059
	private List<EffectLight.Comp> parts = new List<EffectLight.Comp>();

	// Token: 0x040017AC RID: 6060
	private bool isRevealed;

	// Token: 0x0200060E RID: 1550
	private class Comp
	{
		// Token: 0x0600270E RID: 9998 RVA: 0x000D4C5C File Offset: 0x000D2E5C
		public Comp(Light l)
		{
			this.light = l;
			this.baseIntensity = l.intensity;
		}

		// Token: 0x0600270F RID: 9999 RVA: 0x000D4C77 File Offset: 0x000D2E77
		public Comp(LensFlare f)
		{
			this.flare = f;
			this.baseIntensity = f.brightness;
		}

		// Token: 0x06002710 RID: 10000 RVA: 0x000D4C94 File Offset: 0x000D2E94
		public void UpdateBrightness(float val)
		{
			if (this.light != null)
			{
				this.light.intensity = this.baseIntensity * val;
			}
			if (this.flare != null)
			{
				this.flare.brightness = this.baseIntensity * val;
			}
		}

		// Token: 0x040029A5 RID: 10661
		private Light light;

		// Token: 0x040029A6 RID: 10662
		private LensFlare flare;

		// Token: 0x040029A7 RID: 10663
		public float baseIntensity;
	}

	// Token: 0x0200060F RID: 1551
	[CompilerGenerated]
	private sealed class <FadeIn>d__15 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x06002711 RID: 10001 RVA: 0x000D4CE3 File Offset: 0x000D2EE3
		[DebuggerHidden]
		public <FadeIn>d__15(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x06002712 RID: 10002 RVA: 0x000D4CF2 File Offset: 0x000D2EF2
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06002713 RID: 10003 RVA: 0x000D4CF4 File Offset: 0x000D2EF4
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			EffectLight effectLight = this;
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
				return false;
			default:
				return false;
			}
			if (t < 1f)
			{
				t += Time.deltaTime / effectLight.InTime;
				foreach (EffectLight.Comp comp in effectLight.parts)
				{
					comp.UpdateBrightness(effectLight.InCurve.Evaluate(t));
				}
				this.<>2__current = true;
				this.<>1__state = 1;
				return true;
			}
			foreach (EffectLight.Comp comp2 in effectLight.parts)
			{
				comp2.UpdateBrightness(1f);
			}
			if (effectLight.AutoFade)
			{
				this.<>2__current = effectLight.FadeOut();
				this.<>1__state = 2;
				return true;
			}
			return false;
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06002714 RID: 10004 RVA: 0x000D4E3C File Offset: 0x000D303C
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x06002715 RID: 10005 RVA: 0x000D4E44 File Offset: 0x000D3044
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06002716 RID: 10006 RVA: 0x000D4E4B File Offset: 0x000D304B
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x040029A8 RID: 10664
		private int <>1__state;

		// Token: 0x040029A9 RID: 10665
		private object <>2__current;

		// Token: 0x040029AA RID: 10666
		public EffectLight <>4__this;

		// Token: 0x040029AB RID: 10667
		private float <t>5__2;
	}

	// Token: 0x02000610 RID: 1552
	[CompilerGenerated]
	private sealed class <FadeOut>d__16 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x06002717 RID: 10007 RVA: 0x000D4E53 File Offset: 0x000D3053
		[DebuggerHidden]
		public <FadeOut>d__16(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x06002718 RID: 10008 RVA: 0x000D4E62 File Offset: 0x000D3062
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06002719 RID: 10009 RVA: 0x000D4E64 File Offset: 0x000D3064
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			EffectLight effectLight = this;
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
			if (t > 1f)
			{
				foreach (EffectLight.Comp comp in effectLight.parts)
				{
					comp.UpdateBrightness(0f);
				}
				effectLight.isRevealed = false;
				return false;
			}
			t += Time.deltaTime / effectLight.OutTime;
			foreach (EffectLight.Comp comp2 in effectLight.parts)
			{
				comp2.UpdateBrightness(effectLight.OutCurve.Evaluate(t));
			}
			this.<>2__current = true;
			this.<>1__state = 1;
			return true;
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x0600271A RID: 10010 RVA: 0x000D4F84 File Offset: 0x000D3184
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x0600271B RID: 10011 RVA: 0x000D4F8C File Offset: 0x000D318C
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x0600271C RID: 10012 RVA: 0x000D4F93 File Offset: 0x000D3193
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x040029AC RID: 10668
		private int <>1__state;

		// Token: 0x040029AD RID: 10669
		private object <>2__current;

		// Token: 0x040029AE RID: 10670
		public EffectLight <>4__this;

		// Token: 0x040029AF RID: 10671
		private float <t>5__2;
	}
}
