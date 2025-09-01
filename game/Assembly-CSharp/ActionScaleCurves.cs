using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x0200001A RID: 26
public class ActionScaleCurves : MonoBehaviour
{
	// Token: 0x0600008C RID: 140 RVA: 0x00006CA0 File Offset: 0x00004EA0
	private void OnEnable()
	{
		if (this.startScale == Vector3.zero)
		{
			this.startScale = base.transform.localScale;
		}
		base.StartCoroutine("DoScaleIn");
	}

	// Token: 0x0600008D RID: 141 RVA: 0x00006CD1 File Offset: 0x00004ED1
	private IEnumerator DoScaleIn()
	{
		float t = 0f;
		while (t < 1f)
		{
			t += Time.deltaTime / this.ScaleInTime;
			base.transform.localScale = this.startScale * this.ScaleIn.Evaluate(t);
			yield return null;
		}
		base.transform.localScale = this.startScale;
		yield break;
	}

	// Token: 0x0600008E RID: 142 RVA: 0x00006CE0 File Offset: 0x00004EE0
	public void DoScaleOut()
	{
		base.StartCoroutine("ScaleOutRoutine");
	}

	// Token: 0x0600008F RID: 143 RVA: 0x00006CEE File Offset: 0x00004EEE
	private IEnumerator ScaleOutRoutine()
	{
		float t = 0f;
		while (t < 1f)
		{
			t += Time.deltaTime / this.ScaleOutTime;
			base.transform.localScale = this.startScale * this.ScaleOut.Evaluate(t);
			yield return null;
		}
		base.transform.localScale = Vector3.zero;
		yield break;
	}

	// Token: 0x06000090 RID: 144 RVA: 0x00006CFD File Offset: 0x00004EFD
	private void OnDisable()
	{
		base.StopAllCoroutines();
	}

	// Token: 0x06000091 RID: 145 RVA: 0x00006D05 File Offset: 0x00004F05
	public ActionScaleCurves()
	{
	}

	// Token: 0x04000086 RID: 134
	public float ScaleInTime = 1f;

	// Token: 0x04000087 RID: 135
	public AnimationCurve ScaleIn;

	// Token: 0x04000088 RID: 136
	public float ScaleOutTime = 1f;

	// Token: 0x04000089 RID: 137
	public AnimationCurve ScaleOut;

	// Token: 0x0400008A RID: 138
	private Vector3 startScale = Vector3.zero;

	// Token: 0x020003E9 RID: 1001
	[CompilerGenerated]
	private sealed class <DoScaleIn>d__6 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x0600205A RID: 8282 RVA: 0x000C0058 File Offset: 0x000BE258
		[DebuggerHidden]
		public <DoScaleIn>d__6(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x0600205B RID: 8283 RVA: 0x000C0067 File Offset: 0x000BE267
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x0600205C RID: 8284 RVA: 0x000C006C File Offset: 0x000BE26C
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			ActionScaleCurves actionScaleCurves = this;
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
				actionScaleCurves.transform.localScale = actionScaleCurves.startScale;
				return false;
			}
			t += Time.deltaTime / actionScaleCurves.ScaleInTime;
			actionScaleCurves.transform.localScale = actionScaleCurves.startScale * actionScaleCurves.ScaleIn.Evaluate(t);
			this.<>2__current = null;
			this.<>1__state = 1;
			return true;
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x0600205D RID: 8285 RVA: 0x000C011A File Offset: 0x000BE31A
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x0600205E RID: 8286 RVA: 0x000C0122 File Offset: 0x000BE322
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x0600205F RID: 8287 RVA: 0x000C0129 File Offset: 0x000BE329
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x040020CE RID: 8398
		private int <>1__state;

		// Token: 0x040020CF RID: 8399
		private object <>2__current;

		// Token: 0x040020D0 RID: 8400
		public ActionScaleCurves <>4__this;

		// Token: 0x040020D1 RID: 8401
		private float <t>5__2;
	}

	// Token: 0x020003EA RID: 1002
	[CompilerGenerated]
	private sealed class <ScaleOutRoutine>d__8 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x06002060 RID: 8288 RVA: 0x000C0131 File Offset: 0x000BE331
		[DebuggerHidden]
		public <ScaleOutRoutine>d__8(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x06002061 RID: 8289 RVA: 0x000C0140 File Offset: 0x000BE340
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06002062 RID: 8290 RVA: 0x000C0144 File Offset: 0x000BE344
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			ActionScaleCurves actionScaleCurves = this;
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
				actionScaleCurves.transform.localScale = Vector3.zero;
				return false;
			}
			t += Time.deltaTime / actionScaleCurves.ScaleOutTime;
			actionScaleCurves.transform.localScale = actionScaleCurves.startScale * actionScaleCurves.ScaleOut.Evaluate(t);
			this.<>2__current = null;
			this.<>1__state = 1;
			return true;
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06002063 RID: 8291 RVA: 0x000C01F1 File Offset: 0x000BE3F1
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x06002064 RID: 8292 RVA: 0x000C01F9 File Offset: 0x000BE3F9
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06002065 RID: 8293 RVA: 0x000C0200 File Offset: 0x000BE400
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x040020D2 RID: 8402
		private int <>1__state;

		// Token: 0x040020D3 RID: 8403
		private object <>2__current;

		// Token: 0x040020D4 RID: 8404
		public ActionScaleCurves <>4__this;

		// Token: 0x040020D5 RID: 8405
		private float <t>5__2;
	}
}
