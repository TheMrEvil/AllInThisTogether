using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x0200024F RID: 591
public class GreyPulseVFX : MonoBehaviour
{
	// Token: 0x060017E5 RID: 6117 RVA: 0x0009592A File Offset: 0x00093B2A
	private void OnEnable()
	{
		base.StartCoroutine(this.PulseDelayed());
	}

	// Token: 0x060017E6 RID: 6118 RVA: 0x00095939 File Offset: 0x00093B39
	private IEnumerator PulseDelayed()
	{
		yield return true;
		PostFXManager.instance.GreyscalePulse(base.transform.position);
		UnityEngine.Debug.DrawRay(base.transform.position, Vector3.up * 10f, Color.white, 3f);
		yield break;
	}

	// Token: 0x060017E7 RID: 6119 RVA: 0x00095948 File Offset: 0x00093B48
	public GreyPulseVFX()
	{
	}

	// Token: 0x02000612 RID: 1554
	[CompilerGenerated]
	private sealed class <PulseDelayed>d__1 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x0600271F RID: 10015 RVA: 0x000D4FFB File Offset: 0x000D31FB
		[DebuggerHidden]
		public <PulseDelayed>d__1(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x06002720 RID: 10016 RVA: 0x000D500A File Offset: 0x000D320A
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06002721 RID: 10017 RVA: 0x000D500C File Offset: 0x000D320C
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			GreyPulseVFX greyPulseVFX = this;
			if (num == 0)
			{
				this.<>1__state = -1;
				this.<>2__current = true;
				this.<>1__state = 1;
				return true;
			}
			if (num != 1)
			{
				return false;
			}
			this.<>1__state = -1;
			PostFXManager.instance.GreyscalePulse(greyPulseVFX.transform.position);
			UnityEngine.Debug.DrawRay(greyPulseVFX.transform.position, Vector3.up * 10f, Color.white, 3f);
			return false;
		}

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06002722 RID: 10018 RVA: 0x000D5092 File Offset: 0x000D3292
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x06002723 RID: 10019 RVA: 0x000D509A File Offset: 0x000D329A
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06002724 RID: 10020 RVA: 0x000D50A1 File Offset: 0x000D32A1
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x040029B3 RID: 10675
		private int <>1__state;

		// Token: 0x040029B4 RID: 10676
		private object <>2__current;

		// Token: 0x040029B5 RID: 10677
		public GreyPulseVFX <>4__this;
	}
}
