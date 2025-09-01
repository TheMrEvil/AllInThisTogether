using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Fluxy;
using UnityEngine;

// Token: 0x02000009 RID: 9
[RequireComponent(typeof(FluxyTarget))]
public class FluXYMeshSplat : MonoBehaviour
{
	// Token: 0x06000015 RID: 21 RVA: 0x00002455 File Offset: 0x00000655
	private void OnEnable()
	{
		base.StopAllCoroutines();
		base.StartCoroutine("Start");
	}

	// Token: 0x06000016 RID: 22 RVA: 0x00002469 File Offset: 0x00000669
	private IEnumerator Start()
	{
		this.target = base.GetComponent<FluxyTarget>();
		float t = 0f;
		Color c = this.target.color;
		while (t < 1f)
		{
			t += Time.deltaTime * 8f;
			c.a = 1f - t;
			this.target.color = c;
			yield return true;
		}
		c.a = 0f;
		this.target.color = c;
		yield break;
	}

	// Token: 0x06000017 RID: 23 RVA: 0x00002478 File Offset: 0x00000678
	public FluXYMeshSplat()
	{
	}

	// Token: 0x04000016 RID: 22
	public Camera cam;

	// Token: 0x04000017 RID: 23
	private FluxyTarget target;

	// Token: 0x02000021 RID: 33
	[CompilerGenerated]
	private sealed class <Start>d__3 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x060000A2 RID: 162 RVA: 0x00006D66 File Offset: 0x00004F66
		[DebuggerHidden]
		public <Start>d__3(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00006D75 File Offset: 0x00004F75
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00006D78 File Offset: 0x00004F78
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			FluXYMeshSplat fluXYMeshSplat = this;
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
				fluXYMeshSplat.target = fluXYMeshSplat.GetComponent<FluxyTarget>();
				t = 0f;
				c = fluXYMeshSplat.target.color;
			}
			if (t >= 1f)
			{
				c.a = 0f;
				fluXYMeshSplat.target.color = c;
				return false;
			}
			t += Time.deltaTime * 8f;
			c.a = 1f - t;
			fluXYMeshSplat.target.color = c;
			this.<>2__current = true;
			this.<>1__state = 1;
			return true;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x00006E5B File Offset: 0x0000505B
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00006E63 File Offset: 0x00005063
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x00006E6A File Offset: 0x0000506A
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x040000D3 RID: 211
		private int <>1__state;

		// Token: 0x040000D4 RID: 212
		private object <>2__current;

		// Token: 0x040000D5 RID: 213
		public FluXYMeshSplat <>4__this;

		// Token: 0x040000D6 RID: 214
		private float <t>5__2;

		// Token: 0x040000D7 RID: 215
		private Color <c>5__3;
	}
}
