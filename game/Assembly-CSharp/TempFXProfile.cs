using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

// Token: 0x0200025D RID: 605
public class TempFXProfile : MonoBehaviour
{
	// Token: 0x06001853 RID: 6227 RVA: 0x00098364 File Offset: 0x00096564
	public void Setup(PostProcessNode node, EffectProperties props)
	{
		this.Volume.weight = 0f;
		this.Volume.profile = node.Profile;
		this.settings = node;
		this.fastIn = node.FastIn;
		if (node.Lifetime > 0f)
		{
			base.StartCoroutine("LifetimeRoutine");
			return;
		}
		this.UpdateValues(node, props);
	}

	// Token: 0x06001854 RID: 6228 RVA: 0x000983C7 File Offset: 0x000965C7
	public void UpdateValues(PostProcessNode node, EffectProperties props)
	{
		this.variableWeight = true;
		this.wantWeight = node.GetDesiredWeight(props);
	}

	// Token: 0x06001855 RID: 6229 RVA: 0x000983E0 File Offset: 0x000965E0
	private void Update()
	{
		if (!this.variableWeight)
		{
			return;
		}
		float num = this.fastIn ? 6f : 2f;
		this.Volume.weight = Mathf.Lerp(this.Volume.weight, this.wantWeight, Time.deltaTime * num);
		if (this.wantRelease)
		{
			if (this.wantWeight > 0f)
			{
				this.wantWeight -= Time.deltaTime * num;
				return;
			}
			if (this.Volume.weight <= 0.05f)
			{
				this.ClearProfile();
			}
		}
	}

	// Token: 0x06001856 RID: 6230 RVA: 0x00098475 File Offset: 0x00096675
	public void Release()
	{
		this.wantRelease = true;
	}

	// Token: 0x06001857 RID: 6231 RVA: 0x0009847E File Offset: 0x0009667E
	private IEnumerator LifetimeRoutine()
	{
		float t = 0f;
		while (t < 1f)
		{
			t += Time.deltaTime / Mathf.Max(0.01f, this.settings.Lifetime);
			this.Volume.weight = this.settings.WeightCurve.Evaluate(t);
			yield return true;
		}
		this.ClearProfile();
		yield break;
	}

	// Token: 0x06001858 RID: 6232 RVA: 0x0009848D File Offset: 0x0009668D
	private void ClearProfile()
	{
		PostFXManager.instance.ReleaseTempFX(this);
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06001859 RID: 6233 RVA: 0x000984A5 File Offset: 0x000966A5
	public TempFXProfile()
	{
	}

	// Token: 0x04001829 RID: 6185
	public PostProcessVolume Volume;

	// Token: 0x0400182A RID: 6186
	private PostProcessNode settings;

	// Token: 0x0400182B RID: 6187
	public bool variableWeight;

	// Token: 0x0400182C RID: 6188
	public float wantWeight;

	// Token: 0x0400182D RID: 6189
	public bool wantRelease;

	// Token: 0x0400182E RID: 6190
	private bool fastIn;

	// Token: 0x02000627 RID: 1575
	[CompilerGenerated]
	private sealed class <LifetimeRoutine>d__10 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x06002785 RID: 10117 RVA: 0x000D645F File Offset: 0x000D465F
		[DebuggerHidden]
		public <LifetimeRoutine>d__10(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x06002786 RID: 10118 RVA: 0x000D646E File Offset: 0x000D466E
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06002787 RID: 10119 RVA: 0x000D6470 File Offset: 0x000D4670
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			TempFXProfile tempFXProfile = this;
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
				tempFXProfile.ClearProfile();
				return false;
			}
			t += Time.deltaTime / Mathf.Max(0.01f, tempFXProfile.settings.Lifetime);
			tempFXProfile.Volume.weight = tempFXProfile.settings.WeightCurve.Evaluate(t);
			this.<>2__current = true;
			this.<>1__state = 1;
			return true;
		}

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06002788 RID: 10120 RVA: 0x000D6521 File Offset: 0x000D4721
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x06002789 RID: 10121 RVA: 0x000D6529 File Offset: 0x000D4729
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x0600278A RID: 10122 RVA: 0x000D6530 File Offset: 0x000D4730
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x04002A0A RID: 10762
		private int <>1__state;

		// Token: 0x04002A0B RID: 10763
		private object <>2__current;

		// Token: 0x04002A0C RID: 10764
		public TempFXProfile <>4__this;

		// Token: 0x04002A0D RID: 10765
		private float <t>5__2;
	}
}
