using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x0200022E RID: 558
public class InvokeDelayed : MonoBehaviour
{
	// Token: 0x0600172E RID: 5934 RVA: 0x00092A05 File Offset: 0x00090C05
	public void Invoke()
	{
		base.CancelInvoke();
		base.StartCoroutine("DoDelay");
	}

	// Token: 0x0600172F RID: 5935 RVA: 0x00092A19 File Offset: 0x00090C19
	public void TryCancel()
	{
		base.StopAllCoroutines();
	}

	// Token: 0x06001730 RID: 5936 RVA: 0x00092A21 File Offset: 0x00090C21
	private IEnumerator DoDelay()
	{
		yield return new WaitForSeconds(this.Delay);
		this.Event.Invoke();
		yield break;
	}

	// Token: 0x06001731 RID: 5937 RVA: 0x00092A30 File Offset: 0x00090C30
	public InvokeDelayed()
	{
	}

	// Token: 0x040016F3 RID: 5875
	public float Delay = 1f;

	// Token: 0x040016F4 RID: 5876
	public UnityEvent Event;

	// Token: 0x020005FF RID: 1535
	[CompilerGenerated]
	private sealed class <DoDelay>d__4 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x060026C0 RID: 9920 RVA: 0x000D411C File Offset: 0x000D231C
		[DebuggerHidden]
		public <DoDelay>d__4(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x060026C1 RID: 9921 RVA: 0x000D412B File Offset: 0x000D232B
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x060026C2 RID: 9922 RVA: 0x000D4130 File Offset: 0x000D2330
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			InvokeDelayed invokeDelayed = this;
			if (num == 0)
			{
				this.<>1__state = -1;
				this.<>2__current = new WaitForSeconds(invokeDelayed.Delay);
				this.<>1__state = 1;
				return true;
			}
			if (num != 1)
			{
				return false;
			}
			this.<>1__state = -1;
			invokeDelayed.Event.Invoke();
			return false;
		}

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x060026C3 RID: 9923 RVA: 0x000D4188 File Offset: 0x000D2388
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x060026C4 RID: 9924 RVA: 0x000D4190 File Offset: 0x000D2390
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x060026C5 RID: 9925 RVA: 0x000D4197 File Offset: 0x000D2397
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x0400296A RID: 10602
		private int <>1__state;

		// Token: 0x0400296B RID: 10603
		private object <>2__current;

		// Token: 0x0400296C RID: 10604
		public InvokeDelayed <>4__this;
	}
}
