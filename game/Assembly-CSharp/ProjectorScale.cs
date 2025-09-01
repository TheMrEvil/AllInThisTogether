using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x0200025A RID: 602
[RequireComponent(typeof(Projector))]
public class ProjectorScale : MonoBehaviour
{
	// Token: 0x06001839 RID: 6201 RVA: 0x00097B28 File Offset: 0x00095D28
	private void OnEnable()
	{
		this.EnsureProjector();
		List<ProjectorScale> list = new List<ProjectorScale>();
		if (base.transform.parent != null)
		{
			foreach (ProjectorScale projectorScale in base.transform.parent.GetComponentsInChildren<ProjectorScale>())
			{
				if (projectorScale.SortOrder < this.SortOrder)
				{
					return;
				}
				list.Add(projectorScale);
			}
		}
		list.Sort((ProjectorScale x, ProjectorScale y) => x.SortOrder.CompareTo(y.SortOrder));
		foreach (ProjectorScale projectorScale2 in list)
		{
			projectorScale2.GetComponent<Projector>().enabled = false;
			projectorScale2.GetComponent<Projector>().enabled = true;
		}
	}

	// Token: 0x0600183A RID: 6202 RVA: 0x00097C04 File Offset: 0x00095E04
	public void Validate()
	{
		if (Application.isPlaying)
		{
			return;
		}
		this.EnsureProjector();
		this.projector.orthographic = true;
		this.projector.ignoreLayers = ~(1 << LayerMask.NameToLayer("StaticLevel"));
		this.SetupMaterial();
	}

	// Token: 0x0600183B RID: 6203 RVA: 0x00097C41 File Offset: 0x00095E41
	private void Start()
	{
		if (this.AutoSetup)
		{
			this.Setup(null);
		}
		if (this.AutoFadeOut)
		{
			base.Invoke("Hide", this.FadeOutDelay);
		}
	}

	// Token: 0x0600183C RID: 6204 RVA: 0x00097C6C File Offset: 0x00095E6C
	private void SetupMaterial()
	{
		this.EnsureProjector();
		if (this.ProjectorMat == null || this.auraControl != null)
		{
			return;
		}
		this.projMat = new Material(this.ProjectorMat);
		this.projMat.CopyPropertiesFromMaterial(this.ProjectorMat);
		this.projector.material = this.projMat;
		this.UpdateMaterialProps();
	}

	// Token: 0x0600183D RID: 6205 RVA: 0x00097CD5 File Offset: 0x00095ED5
	private void EnsureProjector()
	{
		if (this.projector != null)
		{
			return;
		}
		this.auraControl = base.GetComponent<AuraController>();
		this.projector = base.GetComponent<Projector>();
	}

	// Token: 0x0600183E RID: 6206 RVA: 0x00097CFE File Offset: 0x00095EFE
	public void Setup(ActionEffect e)
	{
		this.effect = e;
		this.Show();
	}

	// Token: 0x0600183F RID: 6207 RVA: 0x00097D10 File Offset: 0x00095F10
	public void Show()
	{
		this.SetupMaterial();
		if (this.projMat == null)
		{
			return;
		}
		if (!base.gameObject.activeInHierarchy)
		{
			return;
		}
		this.projector.enabled = true;
		if (this.auraControl == null)
		{
			this.projMat.SetFloat(ProjectorScale.DissolveAmount, 1f);
		}
		else
		{
			this.auraControl.Opacity = 0f;
		}
		base.StopAllCoroutines();
		base.StartCoroutine("FadeIn");
	}

	// Token: 0x06001840 RID: 6208 RVA: 0x00097D93 File Offset: 0x00095F93
	public void Hide()
	{
		if (!this.projector.enabled || this.projMat == null || !base.gameObject.activeInHierarchy)
		{
			return;
		}
		base.StopAllCoroutines();
		base.StartCoroutine("FadeOut");
	}

	// Token: 0x06001841 RID: 6209 RVA: 0x00097DD0 File Offset: 0x00095FD0
	private void UpdateMaterialProps()
	{
		if (this.auraControl != null)
		{
			return;
		}
		if (this.projMat != null)
		{
			this.projMat.SetColor("_TintColor", this.TintColor);
			this.projMat.SetFloat("_SoftMult", this.ClipMultiplier);
		}
	}

	// Token: 0x06001842 RID: 6210 RVA: 0x00097E26 File Offset: 0x00096026
	private IEnumerator FadeIn()
	{
		float t = 0f;
		while (t < 1f)
		{
			t += Time.deltaTime / this.FadeInTime;
			if (this.auraControl == null)
			{
				this.projMat.SetFloat(ProjectorScale.DissolveAmount, 1f - t);
			}
			else
			{
				this.auraControl.Opacity = t;
			}
			yield return true;
		}
		if (this.auraControl == null)
		{
			this.projMat.SetFloat(ProjectorScale.DissolveAmount, 0f);
		}
		else
		{
			this.auraControl.Opacity = 1f;
		}
		yield break;
	}

	// Token: 0x06001843 RID: 6211 RVA: 0x00097E35 File Offset: 0x00096035
	private IEnumerator FadeOut()
	{
		float t = this.projMat.GetFloat(ProjectorScale.DissolveAmount);
		while (t < 1f)
		{
			t += Time.deltaTime / this.FadeInTime;
			if (this.auraControl == null)
			{
				this.projMat.SetFloat(ProjectorScale.DissolveAmount, t);
			}
			else
			{
				this.auraControl.Opacity = 1f - t;
			}
			yield return true;
		}
		if (this.auraControl == null)
		{
			this.projMat.SetFloat(ProjectorScale.DissolveAmount, 1f);
		}
		else
		{
			this.auraControl.Opacity = 0f;
		}
		this.projector.enabled = true;
		yield break;
	}

	// Token: 0x06001844 RID: 6212 RVA: 0x00097E44 File Offset: 0x00096044
	private void Update()
	{
		this.EnsureProjector();
		if (!this.projector.orthographic)
		{
			this.projector.orthographic = true;
		}
		if (this.auraControl != null)
		{
			return;
		}
		float num = this.scaleMultiplier;
		if (this.effect != null)
		{
			num *= this.ScaleTimeCurve.Evaluate(this.effect.timeAlive);
		}
		float x = base.transform.lossyScale.x;
		this.projector.orthographicSize = x * num;
		this.projector.aspectRatio = this.AspectRatio;
		float num2 = 4f;
		if (base.transform.root != base.transform && !this.ignoreOffsetMult)
		{
			num2 = base.transform.root.InverseTransformPoint(base.transform.position).z * base.transform.root.lossyScale.y;
		}
		else if (this.ignoreOffsetMult)
		{
			num2 *= this.HeightMultiplier;
		}
		this.projector.farClipPlane = 2f * num2;
	}

	// Token: 0x06001845 RID: 6213 RVA: 0x00097F60 File Offset: 0x00096160
	public ProjectorScale()
	{
	}

	// Token: 0x06001846 RID: 6214 RVA: 0x00097FFD File Offset: 0x000961FD
	// Note: this type is marked as 'beforefieldinit'.
	static ProjectorScale()
	{
	}

	// Token: 0x0400180A RID: 6154
	private Projector projector;

	// Token: 0x0400180B RID: 6155
	private ActionEffect effect;

	// Token: 0x0400180C RID: 6156
	private AuraController auraControl;

	// Token: 0x0400180D RID: 6157
	public int SortOrder;

	// Token: 0x0400180E RID: 6158
	public float scaleMultiplier = 1f;

	// Token: 0x0400180F RID: 6159
	[Tooltip("Changes Square nature projector")]
	public float AspectRatio = 1f;

	// Token: 0x04001810 RID: 6160
	public AnimationCurve ScaleTimeCurve = new AnimationCurve(new Keyframe[]
	{
		new Keyframe(0f, 1f),
		new Keyframe(1f, 1f)
	});

	// Token: 0x04001811 RID: 6161
	public float FadeInTime = 0.5f;

	// Token: 0x04001812 RID: 6162
	public bool AutoSetup;

	// Token: 0x04001813 RID: 6163
	public bool AutoFadeOut;

	// Token: 0x04001814 RID: 6164
	public float FadeOutDelay = 2f;

	// Token: 0x04001815 RID: 6165
	private Material projMat;

	// Token: 0x04001816 RID: 6166
	[ColorUsage(true, true)]
	public Color TintColor = Color.white;

	// Token: 0x04001817 RID: 6167
	[Range(0f, 1f)]
	public float ClipMultiplier = 1f;

	// Token: 0x04001818 RID: 6168
	public bool ignoreOffsetMult;

	// Token: 0x04001819 RID: 6169
	public float HeightMultiplier = 1f;

	// Token: 0x0400181A RID: 6170
	public Material ProjectorMat;

	// Token: 0x0400181B RID: 6171
	private static readonly int DissolveAmount = Shader.PropertyToID("_DissolveAmount");

	// Token: 0x02000622 RID: 1570
	[CompilerGenerated]
	[Serializable]
	private sealed class <>c
	{
		// Token: 0x0600276A RID: 10090 RVA: 0x000D5E4C File Offset: 0x000D404C
		// Note: this type is marked as 'beforefieldinit'.
		static <>c()
		{
		}

		// Token: 0x0600276B RID: 10091 RVA: 0x000D5E58 File Offset: 0x000D4058
		public <>c()
		{
		}

		// Token: 0x0600276C RID: 10092 RVA: 0x000D5E60 File Offset: 0x000D4060
		internal int <OnEnable>b__18_0(ProjectorScale x, ProjectorScale y)
		{
			return x.SortOrder.CompareTo(y.SortOrder);
		}

		// Token: 0x040029FA RID: 10746
		public static readonly ProjectorScale.<>c <>9 = new ProjectorScale.<>c();

		// Token: 0x040029FB RID: 10747
		public static Comparison<ProjectorScale> <>9__18_0;
	}

	// Token: 0x02000623 RID: 1571
	[CompilerGenerated]
	private sealed class <FadeIn>d__27 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x0600276D RID: 10093 RVA: 0x000D5E73 File Offset: 0x000D4073
		[DebuggerHidden]
		public <FadeIn>d__27(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x0600276E RID: 10094 RVA: 0x000D5E82 File Offset: 0x000D4082
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x0600276F RID: 10095 RVA: 0x000D5E84 File Offset: 0x000D4084
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			ProjectorScale projectorScale = this;
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
				if (projectorScale.auraControl == null)
				{
					projectorScale.projMat.SetFloat(ProjectorScale.DissolveAmount, 0f);
				}
				else
				{
					projectorScale.auraControl.Opacity = 1f;
				}
				return false;
			}
			t += Time.deltaTime / projectorScale.FadeInTime;
			if (projectorScale.auraControl == null)
			{
				projectorScale.projMat.SetFloat(ProjectorScale.DissolveAmount, 1f - t);
			}
			else
			{
				projectorScale.auraControl.Opacity = t;
			}
			this.<>2__current = true;
			this.<>1__state = 1;
			return true;
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06002770 RID: 10096 RVA: 0x000D5F74 File Offset: 0x000D4174
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x06002771 RID: 10097 RVA: 0x000D5F7C File Offset: 0x000D417C
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x06002772 RID: 10098 RVA: 0x000D5F83 File Offset: 0x000D4183
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x040029FC RID: 10748
		private int <>1__state;

		// Token: 0x040029FD RID: 10749
		private object <>2__current;

		// Token: 0x040029FE RID: 10750
		public ProjectorScale <>4__this;

		// Token: 0x040029FF RID: 10751
		private float <t>5__2;
	}

	// Token: 0x02000624 RID: 1572
	[CompilerGenerated]
	private sealed class <FadeOut>d__28 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x06002773 RID: 10099 RVA: 0x000D5F8B File Offset: 0x000D418B
		[DebuggerHidden]
		public <FadeOut>d__28(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x06002774 RID: 10100 RVA: 0x000D5F9A File Offset: 0x000D419A
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06002775 RID: 10101 RVA: 0x000D5F9C File Offset: 0x000D419C
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			ProjectorScale projectorScale = this;
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
				t = projectorScale.projMat.GetFloat(ProjectorScale.DissolveAmount);
			}
			if (t >= 1f)
			{
				if (projectorScale.auraControl == null)
				{
					projectorScale.projMat.SetFloat(ProjectorScale.DissolveAmount, 1f);
				}
				else
				{
					projectorScale.auraControl.Opacity = 0f;
				}
				projectorScale.projector.enabled = true;
				return false;
			}
			t += Time.deltaTime / projectorScale.FadeInTime;
			if (projectorScale.auraControl == null)
			{
				projectorScale.projMat.SetFloat(ProjectorScale.DissolveAmount, t);
			}
			else
			{
				projectorScale.auraControl.Opacity = 1f - t;
			}
			this.<>2__current = true;
			this.<>1__state = 1;
			return true;
		}

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x06002776 RID: 10102 RVA: 0x000D60A3 File Offset: 0x000D42A3
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x06002777 RID: 10103 RVA: 0x000D60AB File Offset: 0x000D42AB
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06002778 RID: 10104 RVA: 0x000D60B2 File Offset: 0x000D42B2
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x04002A00 RID: 10752
		private int <>1__state;

		// Token: 0x04002A01 RID: 10753
		private object <>2__current;

		// Token: 0x04002A02 RID: 10754
		public ProjectorScale <>4__this;

		// Token: 0x04002A03 RID: 10755
		private float <t>5__2;
	}
}
