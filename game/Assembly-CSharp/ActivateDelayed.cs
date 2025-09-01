using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x02000219 RID: 537
public class ActivateDelayed : MonoBehaviour
{
	// Token: 0x060016A9 RID: 5801 RVA: 0x0008F571 File Offset: 0x0008D771
	public void Trigger()
	{
		base.StopAllCoroutines();
		base.StartCoroutine("ActiveDelay");
	}

	// Token: 0x060016AA RID: 5802 RVA: 0x0008F585 File Offset: 0x0008D785
	public void CancelActivation()
	{
		base.StopAllCoroutines();
	}

	// Token: 0x060016AB RID: 5803 RVA: 0x0008F58D File Offset: 0x0008D78D
	private IEnumerator ActiveDelay()
	{
		yield return new WaitForSeconds(this.Delay);
		this.ToActivate.SetActive(!this.Deactivate);
		yield break;
	}

	// Token: 0x060016AC RID: 5804 RVA: 0x0008F59C File Offset: 0x0008D79C
	public ActivateDelayed()
	{
	}

	// Token: 0x04001639 RID: 5689
	public float Delay;

	// Token: 0x0400163A RID: 5690
	public bool Deactivate;

	// Token: 0x0400163B RID: 5691
	public GameObject ToActivate;

	// Token: 0x020005FA RID: 1530
	[CompilerGenerated]
	private sealed class <ActiveDelay>d__5 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x060026AC RID: 9900 RVA: 0x000D3F57 File Offset: 0x000D2157
		[DebuggerHidden]
		public <ActiveDelay>d__5(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x060026AD RID: 9901 RVA: 0x000D3F66 File Offset: 0x000D2166
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x060026AE RID: 9902 RVA: 0x000D3F68 File Offset: 0x000D2168
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			ActivateDelayed activateDelayed = this;
			if (num == 0)
			{
				this.<>1__state = -1;
				this.<>2__current = new WaitForSeconds(activateDelayed.Delay);
				this.<>1__state = 1;
				return true;
			}
			if (num != 1)
			{
				return false;
			}
			this.<>1__state = -1;
			activateDelayed.ToActivate.SetActive(!activateDelayed.Deactivate);
			return false;
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x060026AF RID: 9903 RVA: 0x000D3FC9 File Offset: 0x000D21C9
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x060026B0 RID: 9904 RVA: 0x000D3FD1 File Offset: 0x000D21D1
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x060026B1 RID: 9905 RVA: 0x000D3FD8 File Offset: 0x000D21D8
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x0400295B RID: 10587
		private int <>1__state;

		// Token: 0x0400295C RID: 10588
		private object <>2__current;

		// Token: 0x0400295D RID: 10589
		public ActivateDelayed <>4__this;
	}
}
