using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x02000014 RID: 20
public class ActionCollider : MonoBehaviour
{
	// Token: 0x06000059 RID: 89 RVA: 0x000052BC File Offset: 0x000034BC
	private void Awake()
	{
		this.startScale = base.transform.localScale;
		if (this.ActivateOnStart)
		{
			this.Activate();
		}
	}

	// Token: 0x0600005A RID: 90 RVA: 0x000052DD File Offset: 0x000034DD
	public void Activate()
	{
		if (this.activated)
		{
			return;
		}
		if (this.SpawnRoutine != null)
		{
			base.StopCoroutine(this.SpawnRoutine);
		}
		this.SpawnRoutine = base.StartCoroutine("ActivateRoutine");
	}

	// Token: 0x0600005B RID: 91 RVA: 0x0000530D File Offset: 0x0000350D
	public void Deactivate()
	{
		if (this.SpawnRoutine != null)
		{
			base.StopCoroutine(this.SpawnRoutine);
		}
		base.transform.localScale = this.startScale;
		this.activated = false;
	}

	// Token: 0x0600005C RID: 92 RVA: 0x0000533B File Offset: 0x0000353B
	private IEnumerator ActivateRoutine()
	{
		float t = 0f;
		while (t < 1f)
		{
			t += Time.deltaTime / Mathf.Max(this.ScaleTime, 0.01f);
			base.transform.localScale = Vector3.Lerp(this.startScale, Vector3.one, this.scaleCurve.Evaluate(t));
			yield return true;
		}
		base.transform.localScale = Vector3.one;
		yield break;
	}

	// Token: 0x0600005D RID: 93 RVA: 0x0000534C File Offset: 0x0000354C
	public ActionCollider()
	{
	}

	// Token: 0x0400004C RID: 76
	public bool ActivateOnStart = true;

	// Token: 0x0400004D RID: 77
	public AnimationCurve scaleCurve = new AnimationCurve(new Keyframe[]
	{
		new Keyframe(0f, 0f),
		new Keyframe(1f, 1f)
	});

	// Token: 0x0400004E RID: 78
	public float ScaleTime = 0.5f;

	// Token: 0x0400004F RID: 79
	private Vector3 startScale = Vector3.one;

	// Token: 0x04000050 RID: 80
	public bool activated;

	// Token: 0x04000051 RID: 81
	private Coroutine SpawnRoutine;

	// Token: 0x020003DF RID: 991
	[CompilerGenerated]
	private sealed class <ActivateRoutine>d__9 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x06002035 RID: 8245 RVA: 0x000BFB58 File Offset: 0x000BDD58
		[DebuggerHidden]
		public <ActivateRoutine>d__9(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x06002036 RID: 8246 RVA: 0x000BFB67 File Offset: 0x000BDD67
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06002037 RID: 8247 RVA: 0x000BFB6C File Offset: 0x000BDD6C
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			ActionCollider actionCollider = this;
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
				actionCollider.transform.localScale = Vector3.one;
				return false;
			}
			t += Time.deltaTime / Mathf.Max(actionCollider.ScaleTime, 0.01f);
			actionCollider.transform.localScale = Vector3.Lerp(actionCollider.startScale, Vector3.one, actionCollider.scaleCurve.Evaluate(t));
			this.<>2__current = true;
			this.<>1__state = 1;
			return true;
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06002038 RID: 8248 RVA: 0x000BFC2D File Offset: 0x000BDE2D
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x06002039 RID: 8249 RVA: 0x000BFC35 File Offset: 0x000BDE35
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x0600203A RID: 8250 RVA: 0x000BFC3C File Offset: 0x000BDE3C
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x040020AD RID: 8365
		private int <>1__state;

		// Token: 0x040020AE RID: 8366
		private object <>2__current;

		// Token: 0x040020AF RID: 8367
		public ActionCollider <>4__this;

		// Token: 0x040020B0 RID: 8368
		private float <t>5__2;
	}
}
