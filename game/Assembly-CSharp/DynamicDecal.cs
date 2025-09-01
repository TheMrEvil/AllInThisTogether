using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x0200024A RID: 586
[ExecuteInEditMode]
public class DynamicDecal : MonoBehaviour
{
	// Token: 0x060017C1 RID: 6081 RVA: 0x00094F8D File Offset: 0x0009318D
	private void Awake()
	{
		this.Validate();
		if (Application.isPlaying)
		{
			this.baseScale = base.transform.localScale;
		}
	}

	// Token: 0x060017C2 RID: 6082 RVA: 0x00094FAD File Offset: 0x000931AD
	private void OnEnable()
	{
		this.lifetimeLocal = 0f;
		base.StartCoroutine("DoStartup");
	}

	// Token: 0x060017C3 RID: 6083 RVA: 0x00094FC6 File Offset: 0x000931C6
	private IEnumerator DoStartup()
	{
		yield return new WaitForEndOfFrame();
		this.OnStart();
		yield break;
	}

	// Token: 0x060017C4 RID: 6084 RVA: 0x00094FD8 File Offset: 0x000931D8
	private void OnStart()
	{
		if (this.baseScale == Vector3.zero)
		{
			this.baseScale = base.transform.localScale;
		}
		if (this.AutoSetup)
		{
			this.Setup(null);
		}
		if (this.AutoFadeOut)
		{
			base.Invoke("Hide", this.FadeOutDelay);
		}
	}

	// Token: 0x060017C5 RID: 6085 RVA: 0x00095030 File Offset: 0x00093230
	public void Setup(ActionEffect e)
	{
		this.effect = e;
		this.Validate();
		this.Show();
	}

	// Token: 0x060017C6 RID: 6086 RVA: 0x00095048 File Offset: 0x00093248
	public void Show()
	{
		if (this.mr == null || !base.gameObject.activeInHierarchy)
		{
			return;
		}
		this.pb.SetFloat(DynamicDecal.DissolveAmountID, 1f);
		this.mr.SetPropertyBlock(this.pb);
		if (this.FadeRoutine != null)
		{
			base.StopCoroutine(this.FadeRoutine);
		}
		this.FadeRoutine = base.StartCoroutine("FadeIn");
	}

	// Token: 0x060017C7 RID: 6087 RVA: 0x000950BC File Offset: 0x000932BC
	public void Hide()
	{
		if (this.mr == null || !base.gameObject.activeInHierarchy)
		{
			return;
		}
		if (this.FadeRoutine != null)
		{
			base.StopCoroutine(this.FadeRoutine);
		}
		base.StartCoroutine("FadeOut");
	}

	// Token: 0x060017C8 RID: 6088 RVA: 0x000950FA File Offset: 0x000932FA
	private IEnumerator FadeIn()
	{
		float t = 0f;
		while (t < 1f)
		{
			t += Time.deltaTime / this.FadeInTime;
			this.pb.SetFloat(DynamicDecal.DissolveAmountID, 1f - t);
			this.mr.SetPropertyBlock(this.pb);
			yield return true;
		}
		this.pb.SetFloat(DynamicDecal.DissolveAmountID, 0f);
		this.mr.SetPropertyBlock(this.pb);
		this.FadeRoutine = null;
		yield break;
	}

	// Token: 0x060017C9 RID: 6089 RVA: 0x00095109 File Offset: 0x00093309
	private IEnumerator FadeOut()
	{
		float t = this.pb.GetFloat(DynamicDecal.DissolveAmountID);
		float ft = this.FadeInTime;
		if (this.SeparateFadeOutTime)
		{
			ft = this.FadeOutTime;
		}
		while (t < 1f)
		{
			t += Time.deltaTime / ft;
			this.pb.SetFloat(DynamicDecal.DissolveAmountID, t);
			this.mr.SetPropertyBlock(this.pb);
			yield return true;
		}
		this.pb.SetFloat(DynamicDecal.DissolveAmountID, 1f);
		this.mr.SetPropertyBlock(this.pb);
		this.FadeRoutine = null;
		yield break;
	}

	// Token: 0x060017CA RID: 6090 RVA: 0x00095118 File Offset: 0x00093318
	private void Update()
	{
		if (this.mr == null || !Application.isPlaying || this.overrideScaling)
		{
			return;
		}
		this.lifetimeLocal += GameplayManager.deltaTime;
		Vector3 vector = this.baseScale;
		if (this.effect != null)
		{
			vector *= this.ScaleTimeCurve.Evaluate(this.effect.timeAlive);
		}
		else
		{
			vector *= this.ScaleTimeCurve.Evaluate(this.lifetimeLocal);
		}
		if (this.ScaleXOnly)
		{
			vector.z = this.baseScale.z;
		}
		vector.y = this.baseScale.y;
		base.transform.localScale = vector;
	}

	// Token: 0x060017CB RID: 6091 RVA: 0x000951D9 File Offset: 0x000933D9
	public void SetOverrideScaling(bool overrideScale)
	{
		this.overrideScaling = overrideScale;
		UnityEngine.Debug.Log("Override Scaling = " + this.overrideScaling.ToString());
	}

	// Token: 0x060017CC RID: 6092 RVA: 0x000951FC File Offset: 0x000933FC
	private void Validate()
	{
		if (Application.isPlaying && this.mr != null)
		{
			return;
		}
		this.mr = base.GetComponent<MeshRenderer>();
		this.pb = new MaterialPropertyBlock();
		this.pb.SetColor(DynamicDecal.TintColorID, this.TintColor);
		this.pb.SetFloat(DynamicDecal.DissolveAmountID, 0f);
		this.mr.SetPropertyBlock(this.pb);
	}

	// Token: 0x060017CD RID: 6093 RVA: 0x00095272 File Offset: 0x00093472
	private void OnDrawGizmos()
	{
	}

	// Token: 0x060017CE RID: 6094 RVA: 0x00095274 File Offset: 0x00093474
	public void UpdatePropertyBlock()
	{
		if (Application.isPlaying)
		{
			return;
		}
		this.Validate();
	}

	// Token: 0x060017CF RID: 6095 RVA: 0x00095284 File Offset: 0x00093484
	public DynamicDecal()
	{
	}

	// Token: 0x060017D0 RID: 6096 RVA: 0x00095300 File Offset: 0x00093500
	// Note: this type is marked as 'beforefieldinit'.
	static DynamicDecal()
	{
	}

	// Token: 0x04001790 RID: 6032
	private ActionEffect effect;

	// Token: 0x04001791 RID: 6033
	private MeshRenderer mr;

	// Token: 0x04001792 RID: 6034
	public int SortOrder;

	// Token: 0x04001793 RID: 6035
	public AnimationCurve ScaleTimeCurve = new AnimationCurve(new Keyframe[]
	{
		new Keyframe(0f, 1f),
		new Keyframe(1f, 1f)
	});

	// Token: 0x04001794 RID: 6036
	public bool ScaleXOnly;

	// Token: 0x04001795 RID: 6037
	public float FadeInTime = 0.5f;

	// Token: 0x04001796 RID: 6038
	public bool SeparateFadeOutTime;

	// Token: 0x04001797 RID: 6039
	public float FadeOutTime = 0.5f;

	// Token: 0x04001798 RID: 6040
	public bool AutoSetup;

	// Token: 0x04001799 RID: 6041
	public bool AutoFadeOut;

	// Token: 0x0400179A RID: 6042
	public float FadeOutDelay = 2f;

	// Token: 0x0400179B RID: 6043
	[ColorUsage(true, true)]
	public Color TintColor = Color.white;

	// Token: 0x0400179C RID: 6044
	private static readonly int DissolveAmountID = Shader.PropertyToID("_DissolveAmount");

	// Token: 0x0400179D RID: 6045
	private static readonly int TintColorID = Shader.PropertyToID("_TintColor");

	// Token: 0x0400179E RID: 6046
	private Vector3 baseScale;

	// Token: 0x0400179F RID: 6047
	private MaterialPropertyBlock pb;

	// Token: 0x040017A0 RID: 6048
	private bool overrideScaling;

	// Token: 0x040017A1 RID: 6049
	private float lifetimeLocal;

	// Token: 0x040017A2 RID: 6050
	private Coroutine FadeRoutine;

	// Token: 0x0200060B RID: 1547
	[CompilerGenerated]
	private sealed class <DoStartup>d__21 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x060026FC RID: 9980 RVA: 0x000D49BC File Offset: 0x000D2BBC
		[DebuggerHidden]
		public <DoStartup>d__21(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x060026FD RID: 9981 RVA: 0x000D49CB File Offset: 0x000D2BCB
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x060026FE RID: 9982 RVA: 0x000D49D0 File Offset: 0x000D2BD0
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			DynamicDecal dynamicDecal = this;
			if (num == 0)
			{
				this.<>1__state = -1;
				this.<>2__current = new WaitForEndOfFrame();
				this.<>1__state = 1;
				return true;
			}
			if (num != 1)
			{
				return false;
			}
			this.<>1__state = -1;
			dynamicDecal.OnStart();
			return false;
		}

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x060026FF RID: 9983 RVA: 0x000D4A1D File Offset: 0x000D2C1D
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x06002700 RID: 9984 RVA: 0x000D4A25 File Offset: 0x000D2C25
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06002701 RID: 9985 RVA: 0x000D4A2C File Offset: 0x000D2C2C
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x04002999 RID: 10649
		private int <>1__state;

		// Token: 0x0400299A RID: 10650
		private object <>2__current;

		// Token: 0x0400299B RID: 10651
		public DynamicDecal <>4__this;
	}

	// Token: 0x0200060C RID: 1548
	[CompilerGenerated]
	private sealed class <FadeIn>d__26 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x06002702 RID: 9986 RVA: 0x000D4A34 File Offset: 0x000D2C34
		[DebuggerHidden]
		public <FadeIn>d__26(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x06002703 RID: 9987 RVA: 0x000D4A43 File Offset: 0x000D2C43
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06002704 RID: 9988 RVA: 0x000D4A48 File Offset: 0x000D2C48
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			DynamicDecal dynamicDecal = this;
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
				dynamicDecal.pb.SetFloat(DynamicDecal.DissolveAmountID, 0f);
				dynamicDecal.mr.SetPropertyBlock(dynamicDecal.pb);
				dynamicDecal.FadeRoutine = null;
				return false;
			}
			t += Time.deltaTime / dynamicDecal.FadeInTime;
			dynamicDecal.pb.SetFloat(DynamicDecal.DissolveAmountID, 1f - t);
			dynamicDecal.mr.SetPropertyBlock(dynamicDecal.pb);
			this.<>2__current = true;
			this.<>1__state = 1;
			return true;
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06002705 RID: 9989 RVA: 0x000D4B1D File Offset: 0x000D2D1D
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x06002706 RID: 9990 RVA: 0x000D4B25 File Offset: 0x000D2D25
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06002707 RID: 9991 RVA: 0x000D4B2C File Offset: 0x000D2D2C
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x0400299C RID: 10652
		private int <>1__state;

		// Token: 0x0400299D RID: 10653
		private object <>2__current;

		// Token: 0x0400299E RID: 10654
		public DynamicDecal <>4__this;

		// Token: 0x0400299F RID: 10655
		private float <t>5__2;
	}

	// Token: 0x0200060D RID: 1549
	[CompilerGenerated]
	private sealed class <FadeOut>d__27 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x06002708 RID: 9992 RVA: 0x000D4B34 File Offset: 0x000D2D34
		[DebuggerHidden]
		public <FadeOut>d__27(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x06002709 RID: 9993 RVA: 0x000D4B43 File Offset: 0x000D2D43
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x0600270A RID: 9994 RVA: 0x000D4B48 File Offset: 0x000D2D48
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			DynamicDecal dynamicDecal = this;
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
				t = dynamicDecal.pb.GetFloat(DynamicDecal.DissolveAmountID);
				ft = dynamicDecal.FadeInTime;
				if (dynamicDecal.SeparateFadeOutTime)
				{
					ft = dynamicDecal.FadeOutTime;
				}
			}
			if (t >= 1f)
			{
				dynamicDecal.pb.SetFloat(DynamicDecal.DissolveAmountID, 1f);
				dynamicDecal.mr.SetPropertyBlock(dynamicDecal.pb);
				dynamicDecal.FadeRoutine = null;
				return false;
			}
			t += Time.deltaTime / ft;
			dynamicDecal.pb.SetFloat(DynamicDecal.DissolveAmountID, t);
			dynamicDecal.mr.SetPropertyBlock(dynamicDecal.pb);
			this.<>2__current = true;
			this.<>1__state = 1;
			return true;
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x0600270B RID: 9995 RVA: 0x000D4C45 File Offset: 0x000D2E45
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x0600270C RID: 9996 RVA: 0x000D4C4D File Offset: 0x000D2E4D
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x0600270D RID: 9997 RVA: 0x000D4C54 File Offset: 0x000D2E54
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x040029A0 RID: 10656
		private int <>1__state;

		// Token: 0x040029A1 RID: 10657
		private object <>2__current;

		// Token: 0x040029A2 RID: 10658
		public DynamicDecal <>4__this;

		// Token: 0x040029A3 RID: 10659
		private float <t>5__2;

		// Token: 0x040029A4 RID: 10660
		private float <ft>5__3;
	}
}
