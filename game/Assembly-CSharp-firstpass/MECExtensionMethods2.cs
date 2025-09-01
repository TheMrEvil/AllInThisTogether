using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using MEC;
using UnityEngine;

// Token: 0x02000005 RID: 5
public static class MECExtensionMethods2
{
	// Token: 0x06000015 RID: 21 RVA: 0x00002947 File Offset: 0x00000B47
	public static IEnumerator<float> Delay(this IEnumerator<float> coroutine, float timeToDelay)
	{
		yield return Timing.WaitForSeconds(timeToDelay);
		while (coroutine.MoveNext())
		{
			float num = coroutine.Current;
			yield return num;
		}
		yield break;
	}

	// Token: 0x06000016 RID: 22 RVA: 0x0000295D File Offset: 0x00000B5D
	public static IEnumerator<float> Delay(this IEnumerator<float> coroutine, Func<bool> condition)
	{
		while (!condition())
		{
			yield return 0f;
		}
		while (coroutine.MoveNext())
		{
			float num = coroutine.Current;
			yield return num;
		}
		yield break;
	}

	// Token: 0x06000017 RID: 23 RVA: 0x00002973 File Offset: 0x00000B73
	public static IEnumerator<float> Delay<T>(this IEnumerator<float> coroutine, T data, Func<T, bool> condition)
	{
		while (!condition(data))
		{
			yield return 0f;
		}
		while (coroutine.MoveNext())
		{
			float num = coroutine.Current;
			yield return num;
		}
		yield break;
	}

	// Token: 0x06000018 RID: 24 RVA: 0x00002990 File Offset: 0x00000B90
	public static IEnumerator<float> DelayFrames(this IEnumerator<float> coroutine, int framesToDelay)
	{
		for (;;)
		{
			int num = framesToDelay;
			framesToDelay = num - 1;
			if (num <= 0)
			{
				break;
			}
			yield return 0f;
		}
		while (coroutine.MoveNext())
		{
			float num2 = coroutine.Current;
			yield return num2;
		}
		yield break;
	}

	// Token: 0x06000019 RID: 25 RVA: 0x000029A6 File Offset: 0x00000BA6
	public static IEnumerator<float> CancelWith(this IEnumerator<float> coroutine, GameObject gameObject)
	{
		while (Timing.MainThread != Thread.CurrentThread || (gameObject && gameObject.activeInHierarchy && coroutine.MoveNext()))
		{
			yield return coroutine.Current;
		}
		yield break;
	}

	// Token: 0x0600001A RID: 26 RVA: 0x000029BC File Offset: 0x00000BBC
	public static IEnumerator<float> CancelWith(this IEnumerator<float> coroutine, GameObject gameObject1, GameObject gameObject2)
	{
		while (Timing.MainThread != Thread.CurrentThread || (gameObject1 && gameObject1.activeInHierarchy && gameObject2 && gameObject2.activeInHierarchy && coroutine.MoveNext()))
		{
			yield return coroutine.Current;
		}
		yield break;
	}

	// Token: 0x0600001B RID: 27 RVA: 0x000029D9 File Offset: 0x00000BD9
	public static IEnumerator<float> CancelWith<T>(this IEnumerator<float> coroutine, T script) where T : MonoBehaviour
	{
		GameObject myGO = script.gameObject;
		while (Timing.MainThread != Thread.CurrentThread || (myGO && myGO.activeInHierarchy && script != null && coroutine.MoveNext()))
		{
			yield return coroutine.Current;
		}
		yield break;
	}

	// Token: 0x0600001C RID: 28 RVA: 0x000029EF File Offset: 0x00000BEF
	public static IEnumerator<float> CancelWith(this IEnumerator<float> coroutine, Func<bool> condition)
	{
		if (condition == null)
		{
			yield break;
		}
		while (Timing.MainThread != Thread.CurrentThread || (condition() && coroutine.MoveNext()))
		{
			yield return coroutine.Current;
		}
		yield break;
	}

	// Token: 0x0600001D RID: 29 RVA: 0x00002A05 File Offset: 0x00000C05
	public static IEnumerator<float> PauseWith(this IEnumerator<float> coroutine, GameObject gameObject)
	{
		while (Timing.MainThread == Thread.CurrentThread && gameObject)
		{
			if (gameObject.activeInHierarchy)
			{
				if (!coroutine.MoveNext())
				{
					yield break;
				}
				yield return coroutine.Current;
			}
			else
			{
				yield return float.NegativeInfinity;
			}
		}
		yield break;
	}

	// Token: 0x0600001E RID: 30 RVA: 0x00002A1B File Offset: 0x00000C1B
	public static IEnumerator<float> PauseWith(this IEnumerator<float> coroutine, GameObject gameObject1, GameObject gameObject2)
	{
		while (Timing.MainThread == Thread.CurrentThread && gameObject1 && gameObject2)
		{
			if (gameObject1.activeInHierarchy && gameObject2.activeInHierarchy)
			{
				if (!coroutine.MoveNext())
				{
					yield break;
				}
				yield return coroutine.Current;
			}
			else
			{
				yield return float.NegativeInfinity;
			}
		}
		yield break;
	}

	// Token: 0x0600001F RID: 31 RVA: 0x00002A38 File Offset: 0x00000C38
	public static IEnumerator<float> PauseWith<T>(this IEnumerator<float> coroutine, T script) where T : MonoBehaviour
	{
		GameObject myGO = script.gameObject;
		while (Timing.MainThread == Thread.CurrentThread && myGO && myGO.GetComponent<T>() != null)
		{
			if (myGO.activeInHierarchy && script.enabled)
			{
				if (!coroutine.MoveNext())
				{
					yield break;
				}
				yield return coroutine.Current;
			}
			else
			{
				yield return float.NegativeInfinity;
			}
		}
		yield break;
	}

	// Token: 0x06000020 RID: 32 RVA: 0x00002A4E File Offset: 0x00000C4E
	public static IEnumerator<float> PauseWith(this IEnumerator<float> coroutine, Func<bool> condition)
	{
		if (condition == null)
		{
			yield break;
		}
		while (Timing.MainThread != Thread.CurrentThread || (condition() && coroutine.MoveNext()))
		{
			yield return coroutine.Current;
		}
		yield break;
	}

	// Token: 0x06000021 RID: 33 RVA: 0x00002A64 File Offset: 0x00000C64
	public static IEnumerator<float> KillWith(this IEnumerator<float> coroutine, CoroutineHandle otherCoroutine)
	{
		while (otherCoroutine.IsRunning && coroutine.MoveNext())
		{
			yield return coroutine.Current;
		}
		yield break;
	}

	// Token: 0x06000022 RID: 34 RVA: 0x00002A7A File Offset: 0x00000C7A
	public static IEnumerator<float> Append(this IEnumerator<float> coroutine, IEnumerator<float> nextCoroutine)
	{
		while (coroutine.MoveNext())
		{
			float num = coroutine.Current;
			yield return num;
		}
		if (nextCoroutine == null)
		{
			yield break;
		}
		while (nextCoroutine.MoveNext())
		{
			float num2 = nextCoroutine.Current;
			yield return num2;
		}
		yield break;
	}

	// Token: 0x06000023 RID: 35 RVA: 0x00002A90 File Offset: 0x00000C90
	public static IEnumerator<float> Append(this IEnumerator<float> coroutine, Action onDone)
	{
		while (coroutine.MoveNext())
		{
			float num = coroutine.Current;
			yield return num;
		}
		if (onDone != null)
		{
			onDone();
		}
		yield break;
	}

	// Token: 0x06000024 RID: 36 RVA: 0x00002AA6 File Offset: 0x00000CA6
	public static IEnumerator<float> Prepend(this IEnumerator<float> coroutine, IEnumerator<float> lastCoroutine)
	{
		if (lastCoroutine != null)
		{
			while (lastCoroutine.MoveNext())
			{
				float num = lastCoroutine.Current;
				yield return num;
			}
		}
		while (coroutine.MoveNext())
		{
			float num2 = coroutine.Current;
			yield return num2;
		}
		yield break;
	}

	// Token: 0x06000025 RID: 37 RVA: 0x00002ABC File Offset: 0x00000CBC
	public static IEnumerator<float> Prepend(this IEnumerator<float> coroutine, Action onStart)
	{
		if (onStart != null)
		{
			onStart();
		}
		while (coroutine.MoveNext())
		{
			float num = coroutine.Current;
			yield return num;
		}
		yield break;
	}

	// Token: 0x06000026 RID: 38 RVA: 0x00002AD2 File Offset: 0x00000CD2
	public static IEnumerator<float> Superimpose(this IEnumerator<float> coroutineA, IEnumerator<float> coroutineB)
	{
		return coroutineA.Superimpose(coroutineB, Timing.Instance);
	}

	// Token: 0x06000027 RID: 39 RVA: 0x00002AE0 File Offset: 0x00000CE0
	public static IEnumerator<float> Superimpose(this IEnumerator<float> coroutineA, IEnumerator<float> coroutineB, Timing instance)
	{
		while (coroutineA != null || coroutineB != null)
		{
			if (coroutineA != null && instance.localTime >= coroutineA.Current && !coroutineA.MoveNext())
			{
				coroutineA = null;
			}
			if (coroutineB != null && instance.localTime >= coroutineB.Current && !coroutineB.MoveNext())
			{
				coroutineB = null;
			}
			if ((coroutineA != null && float.IsNaN(coroutineA.Current)) || (coroutineB != null && float.IsNaN(coroutineB.Current)))
			{
				yield return float.NaN;
			}
			else if (coroutineA != null && coroutineB != null)
			{
				yield return (coroutineA.Current < coroutineB.Current) ? coroutineA.Current : coroutineB.Current;
			}
			else if (coroutineA == null && coroutineB != null)
			{
				yield return coroutineB.Current;
			}
			else if (coroutineA != null)
			{
				yield return coroutineA.Current;
			}
		}
		yield break;
	}

	// Token: 0x06000028 RID: 40 RVA: 0x00002AFD File Offset: 0x00000CFD
	public static IEnumerator<float> Hijack(this IEnumerator<float> coroutine, Func<float, float> newReturn)
	{
		if (newReturn == null)
		{
			yield break;
		}
		while (coroutine.MoveNext())
		{
			float arg = coroutine.Current;
			yield return newReturn(arg);
		}
		yield break;
	}

	// Token: 0x06000029 RID: 41 RVA: 0x00002B13 File Offset: 0x00000D13
	public static IEnumerator<float> RerouteExceptions(this IEnumerator<float> coroutine, Action<Exception> exceptionHandler)
	{
		for (;;)
		{
			try
			{
				if (!coroutine.MoveNext())
				{
					yield break;
				}
			}
			catch (Exception obj)
			{
				if (exceptionHandler != null)
				{
					exceptionHandler(obj);
				}
				yield break;
			}
			yield return coroutine.Current;
		}
		yield break;
	}

	// Token: 0x02000177 RID: 375
	[CompilerGenerated]
	private sealed class <Delay>d__0 : IEnumerator<float>, IEnumerator, IDisposable
	{
		// Token: 0x06000E30 RID: 3632 RVA: 0x0005E4BB File Offset: 0x0005C6BB
		[DebuggerHidden]
		public <Delay>d__0(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x06000E31 RID: 3633 RVA: 0x0005E4CA File Offset: 0x0005C6CA
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06000E32 RID: 3634 RVA: 0x0005E4CC File Offset: 0x0005C6CC
		bool IEnumerator.MoveNext()
		{
			switch (this.<>1__state)
			{
			case 0:
				this.<>1__state = -1;
				this.<>2__current = Timing.WaitForSeconds(timeToDelay);
				this.<>1__state = 1;
				return true;
			case 1:
				this.<>1__state = -1;
				break;
			case 2:
				this.<>1__state = -1;
				break;
			default:
				return false;
			}
			if (!coroutine.MoveNext())
			{
				return false;
			}
			this.<>2__current = coroutine.Current;
			this.<>1__state = 2;
			return true;
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000E33 RID: 3635 RVA: 0x0005E54D File Offset: 0x0005C74D
		float IEnumerator<float>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x06000E34 RID: 3636 RVA: 0x0005E555 File Offset: 0x0005C755
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000E35 RID: 3637 RVA: 0x0005E55C File Offset: 0x0005C75C
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x04000BEE RID: 3054
		private int <>1__state;

		// Token: 0x04000BEF RID: 3055
		private float <>2__current;

		// Token: 0x04000BF0 RID: 3056
		public float timeToDelay;

		// Token: 0x04000BF1 RID: 3057
		public IEnumerator<float> coroutine;
	}

	// Token: 0x02000178 RID: 376
	[CompilerGenerated]
	private sealed class <Delay>d__1 : IEnumerator<float>, IEnumerator, IDisposable
	{
		// Token: 0x06000E36 RID: 3638 RVA: 0x0005E569 File Offset: 0x0005C769
		[DebuggerHidden]
		public <Delay>d__1(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x06000E37 RID: 3639 RVA: 0x0005E578 File Offset: 0x0005C778
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06000E38 RID: 3640 RVA: 0x0005E57C File Offset: 0x0005C77C
		bool IEnumerator.MoveNext()
		{
			switch (this.<>1__state)
			{
			case 0:
				this.<>1__state = -1;
				break;
			case 1:
				this.<>1__state = -1;
				break;
			case 2:
				this.<>1__state = -1;
				goto IL_6F;
			default:
				return false;
			}
			if (!condition())
			{
				this.<>2__current = 0f;
				this.<>1__state = 1;
				return true;
			}
			IL_6F:
			if (!coroutine.MoveNext())
			{
				return false;
			}
			this.<>2__current = coroutine.Current;
			this.<>1__state = 2;
			return true;
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000E39 RID: 3641 RVA: 0x0005E606 File Offset: 0x0005C806
		float IEnumerator<float>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x06000E3A RID: 3642 RVA: 0x0005E60E File Offset: 0x0005C80E
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000E3B RID: 3643 RVA: 0x0005E615 File Offset: 0x0005C815
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x04000BF2 RID: 3058
		private int <>1__state;

		// Token: 0x04000BF3 RID: 3059
		private float <>2__current;

		// Token: 0x04000BF4 RID: 3060
		public Func<bool> condition;

		// Token: 0x04000BF5 RID: 3061
		public IEnumerator<float> coroutine;
	}

	// Token: 0x02000179 RID: 377
	[CompilerGenerated]
	private sealed class <Delay>d__2<T> : IEnumerator<float>, IEnumerator, IDisposable
	{
		// Token: 0x06000E3C RID: 3644 RVA: 0x0005E622 File Offset: 0x0005C822
		[DebuggerHidden]
		public <Delay>d__2(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x06000E3D RID: 3645 RVA: 0x0005E631 File Offset: 0x0005C831
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06000E3E RID: 3646 RVA: 0x0005E634 File Offset: 0x0005C834
		bool IEnumerator.MoveNext()
		{
			switch (this.<>1__state)
			{
			case 0:
				this.<>1__state = -1;
				break;
			case 1:
				this.<>1__state = -1;
				break;
			case 2:
				this.<>1__state = -1;
				goto IL_75;
			default:
				return false;
			}
			if (!condition(data))
			{
				this.<>2__current = 0f;
				this.<>1__state = 1;
				return true;
			}
			IL_75:
			if (!coroutine.MoveNext())
			{
				return false;
			}
			this.<>2__current = coroutine.Current;
			this.<>1__state = 2;
			return true;
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000E3F RID: 3647 RVA: 0x0005E6C4 File Offset: 0x0005C8C4
		float IEnumerator<float>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x06000E40 RID: 3648 RVA: 0x0005E6CC File Offset: 0x0005C8CC
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000E41 RID: 3649 RVA: 0x0005E6D3 File Offset: 0x0005C8D3
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x04000BF6 RID: 3062
		private int <>1__state;

		// Token: 0x04000BF7 RID: 3063
		private float <>2__current;

		// Token: 0x04000BF8 RID: 3064
		public Func<T, bool> condition;

		// Token: 0x04000BF9 RID: 3065
		public T data;

		// Token: 0x04000BFA RID: 3066
		public IEnumerator<float> coroutine;
	}

	// Token: 0x0200017A RID: 378
	[CompilerGenerated]
	private sealed class <DelayFrames>d__3 : IEnumerator<float>, IEnumerator, IDisposable
	{
		// Token: 0x06000E42 RID: 3650 RVA: 0x0005E6E0 File Offset: 0x0005C8E0
		[DebuggerHidden]
		public <DelayFrames>d__3(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x06000E43 RID: 3651 RVA: 0x0005E6EF File Offset: 0x0005C8EF
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06000E44 RID: 3652 RVA: 0x0005E6F4 File Offset: 0x0005C8F4
		bool IEnumerator.MoveNext()
		{
			switch (this.<>1__state)
			{
			case 0:
				this.<>1__state = -1;
				break;
			case 1:
				this.<>1__state = -1;
				break;
			case 2:
				this.<>1__state = -1;
				goto IL_76;
			default:
				return false;
			}
			int num = framesToDelay;
			framesToDelay = num - 1;
			if (num > 0)
			{
				this.<>2__current = 0f;
				this.<>1__state = 1;
				return true;
			}
			IL_76:
			if (!coroutine.MoveNext())
			{
				return false;
			}
			this.<>2__current = coroutine.Current;
			this.<>1__state = 2;
			return true;
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000E45 RID: 3653 RVA: 0x0005E785 File Offset: 0x0005C985
		float IEnumerator<float>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x06000E46 RID: 3654 RVA: 0x0005E78D File Offset: 0x0005C98D
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000E47 RID: 3655 RVA: 0x0005E794 File Offset: 0x0005C994
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x04000BFB RID: 3067
		private int <>1__state;

		// Token: 0x04000BFC RID: 3068
		private float <>2__current;

		// Token: 0x04000BFD RID: 3069
		public int framesToDelay;

		// Token: 0x04000BFE RID: 3070
		public IEnumerator<float> coroutine;
	}

	// Token: 0x0200017B RID: 379
	[CompilerGenerated]
	private sealed class <CancelWith>d__4 : IEnumerator<float>, IEnumerator, IDisposable
	{
		// Token: 0x06000E48 RID: 3656 RVA: 0x0005E7A1 File Offset: 0x0005C9A1
		[DebuggerHidden]
		public <CancelWith>d__4(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x06000E49 RID: 3657 RVA: 0x0005E7B0 File Offset: 0x0005C9B0
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06000E4A RID: 3658 RVA: 0x0005E7B4 File Offset: 0x0005C9B4
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
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
			}
			if (Timing.MainThread == Thread.CurrentThread && (!gameObject || !gameObject.activeInHierarchy || !coroutine.MoveNext()))
			{
				return false;
			}
			this.<>2__current = coroutine.Current;
			this.<>1__state = 1;
			return true;
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000E4B RID: 3659 RVA: 0x0005E82F File Offset: 0x0005CA2F
		float IEnumerator<float>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x06000E4C RID: 3660 RVA: 0x0005E837 File Offset: 0x0005CA37
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000E4D RID: 3661 RVA: 0x0005E83E File Offset: 0x0005CA3E
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x04000BFF RID: 3071
		private int <>1__state;

		// Token: 0x04000C00 RID: 3072
		private float <>2__current;

		// Token: 0x04000C01 RID: 3073
		public IEnumerator<float> coroutine;

		// Token: 0x04000C02 RID: 3074
		public GameObject gameObject;
	}

	// Token: 0x0200017C RID: 380
	[CompilerGenerated]
	private sealed class <CancelWith>d__5 : IEnumerator<float>, IEnumerator, IDisposable
	{
		// Token: 0x06000E4E RID: 3662 RVA: 0x0005E84B File Offset: 0x0005CA4B
		[DebuggerHidden]
		public <CancelWith>d__5(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x06000E4F RID: 3663 RVA: 0x0005E85A File Offset: 0x0005CA5A
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06000E50 RID: 3664 RVA: 0x0005E85C File Offset: 0x0005CA5C
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
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
			}
			if (Timing.MainThread == Thread.CurrentThread && (!gameObject1 || !gameObject1.activeInHierarchy || !gameObject2 || !gameObject2.activeInHierarchy || !coroutine.MoveNext()))
			{
				return false;
			}
			this.<>2__current = coroutine.Current;
			this.<>1__state = 1;
			return true;
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000E51 RID: 3665 RVA: 0x0005E8F1 File Offset: 0x0005CAF1
		float IEnumerator<float>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x06000E52 RID: 3666 RVA: 0x0005E8F9 File Offset: 0x0005CAF9
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000E53 RID: 3667 RVA: 0x0005E900 File Offset: 0x0005CB00
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x04000C03 RID: 3075
		private int <>1__state;

		// Token: 0x04000C04 RID: 3076
		private float <>2__current;

		// Token: 0x04000C05 RID: 3077
		public IEnumerator<float> coroutine;

		// Token: 0x04000C06 RID: 3078
		public GameObject gameObject1;

		// Token: 0x04000C07 RID: 3079
		public GameObject gameObject2;
	}

	// Token: 0x0200017D RID: 381
	[CompilerGenerated]
	private sealed class <CancelWith>d__6<T> : IEnumerator<float>, IEnumerator, IDisposable where T : MonoBehaviour
	{
		// Token: 0x06000E54 RID: 3668 RVA: 0x0005E90D File Offset: 0x0005CB0D
		[DebuggerHidden]
		public <CancelWith>d__6(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x06000E55 RID: 3669 RVA: 0x0005E91C File Offset: 0x0005CB1C
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06000E56 RID: 3670 RVA: 0x0005E920 File Offset: 0x0005CB20
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
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
				myGO = script.gameObject;
			}
			if (Timing.MainThread == Thread.CurrentThread && (!myGO || !myGO.activeInHierarchy || !(script != null) || !coroutine.MoveNext()))
			{
				return false;
			}
			this.<>2__current = coroutine.Current;
			this.<>1__state = 1;
			return true;
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000E57 RID: 3671 RVA: 0x0005E9C4 File Offset: 0x0005CBC4
		float IEnumerator<float>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x06000E58 RID: 3672 RVA: 0x0005E9CC File Offset: 0x0005CBCC
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000E59 RID: 3673 RVA: 0x0005E9D3 File Offset: 0x0005CBD3
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x04000C08 RID: 3080
		private int <>1__state;

		// Token: 0x04000C09 RID: 3081
		private float <>2__current;

		// Token: 0x04000C0A RID: 3082
		public T script;

		// Token: 0x04000C0B RID: 3083
		public IEnumerator<float> coroutine;

		// Token: 0x04000C0C RID: 3084
		private GameObject <myGO>5__2;
	}

	// Token: 0x0200017E RID: 382
	[CompilerGenerated]
	private sealed class <CancelWith>d__7 : IEnumerator<float>, IEnumerator, IDisposable
	{
		// Token: 0x06000E5A RID: 3674 RVA: 0x0005E9E0 File Offset: 0x0005CBE0
		[DebuggerHidden]
		public <CancelWith>d__7(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x06000E5B RID: 3675 RVA: 0x0005E9EF File Offset: 0x0005CBEF
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06000E5C RID: 3676 RVA: 0x0005E9F4 File Offset: 0x0005CBF4
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
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
				if (condition == null)
				{
					return false;
				}
			}
			if (Timing.MainThread == Thread.CurrentThread && (!condition() || !coroutine.MoveNext()))
			{
				return false;
			}
			this.<>2__current = coroutine.Current;
			this.<>1__state = 1;
			return true;
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000E5D RID: 3677 RVA: 0x0005EA6A File Offset: 0x0005CC6A
		float IEnumerator<float>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x06000E5E RID: 3678 RVA: 0x0005EA72 File Offset: 0x0005CC72
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000E5F RID: 3679 RVA: 0x0005EA79 File Offset: 0x0005CC79
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x04000C0D RID: 3085
		private int <>1__state;

		// Token: 0x04000C0E RID: 3086
		private float <>2__current;

		// Token: 0x04000C0F RID: 3087
		public Func<bool> condition;

		// Token: 0x04000C10 RID: 3088
		public IEnumerator<float> coroutine;
	}

	// Token: 0x0200017F RID: 383
	[CompilerGenerated]
	private sealed class <PauseWith>d__8 : IEnumerator<float>, IEnumerator, IDisposable
	{
		// Token: 0x06000E60 RID: 3680 RVA: 0x0005EA86 File Offset: 0x0005CC86
		[DebuggerHidden]
		public <PauseWith>d__8(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x06000E61 RID: 3681 RVA: 0x0005EA95 File Offset: 0x0005CC95
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06000E62 RID: 3682 RVA: 0x0005EA98 File Offset: 0x0005CC98
		bool IEnumerator.MoveNext()
		{
			switch (this.<>1__state)
			{
			case 0:
				this.<>1__state = -1;
				break;
			case 1:
				this.<>1__state = -1;
				break;
			case 2:
				this.<>1__state = -1;
				break;
			default:
				return false;
			}
			if (Timing.MainThread != Thread.CurrentThread || !gameObject)
			{
				return false;
			}
			if (!gameObject.activeInHierarchy)
			{
				this.<>2__current = float.NegativeInfinity;
				this.<>1__state = 2;
				return true;
			}
			if (coroutine.MoveNext())
			{
				this.<>2__current = coroutine.Current;
				this.<>1__state = 1;
				return true;
			}
			return false;
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000E63 RID: 3683 RVA: 0x0005EB3D File Offset: 0x0005CD3D
		float IEnumerator<float>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x06000E64 RID: 3684 RVA: 0x0005EB45 File Offset: 0x0005CD45
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000E65 RID: 3685 RVA: 0x0005EB4C File Offset: 0x0005CD4C
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x04000C11 RID: 3089
		private int <>1__state;

		// Token: 0x04000C12 RID: 3090
		private float <>2__current;

		// Token: 0x04000C13 RID: 3091
		public GameObject gameObject;

		// Token: 0x04000C14 RID: 3092
		public IEnumerator<float> coroutine;
	}

	// Token: 0x02000180 RID: 384
	[CompilerGenerated]
	private sealed class <PauseWith>d__9 : IEnumerator<float>, IEnumerator, IDisposable
	{
		// Token: 0x06000E66 RID: 3686 RVA: 0x0005EB59 File Offset: 0x0005CD59
		[DebuggerHidden]
		public <PauseWith>d__9(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x06000E67 RID: 3687 RVA: 0x0005EB68 File Offset: 0x0005CD68
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06000E68 RID: 3688 RVA: 0x0005EB6C File Offset: 0x0005CD6C
		bool IEnumerator.MoveNext()
		{
			switch (this.<>1__state)
			{
			case 0:
				this.<>1__state = -1;
				break;
			case 1:
				this.<>1__state = -1;
				break;
			case 2:
				this.<>1__state = -1;
				break;
			default:
				return false;
			}
			if (Timing.MainThread != Thread.CurrentThread || !gameObject1 || !gameObject2)
			{
				return false;
			}
			if (!gameObject1.activeInHierarchy || !gameObject2.activeInHierarchy)
			{
				this.<>2__current = float.NegativeInfinity;
				this.<>1__state = 2;
				return true;
			}
			if (coroutine.MoveNext())
			{
				this.<>2__current = coroutine.Current;
				this.<>1__state = 1;
				return true;
			}
			return false;
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000E69 RID: 3689 RVA: 0x0005EC2E File Offset: 0x0005CE2E
		float IEnumerator<float>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x06000E6A RID: 3690 RVA: 0x0005EC36 File Offset: 0x0005CE36
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000E6B RID: 3691 RVA: 0x0005EC3D File Offset: 0x0005CE3D
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x04000C15 RID: 3093
		private int <>1__state;

		// Token: 0x04000C16 RID: 3094
		private float <>2__current;

		// Token: 0x04000C17 RID: 3095
		public GameObject gameObject1;

		// Token: 0x04000C18 RID: 3096
		public GameObject gameObject2;

		// Token: 0x04000C19 RID: 3097
		public IEnumerator<float> coroutine;
	}

	// Token: 0x02000181 RID: 385
	[CompilerGenerated]
	private sealed class <PauseWith>d__10<T> : IEnumerator<float>, IEnumerator, IDisposable where T : MonoBehaviour
	{
		// Token: 0x06000E6C RID: 3692 RVA: 0x0005EC4A File Offset: 0x0005CE4A
		[DebuggerHidden]
		public <PauseWith>d__10(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x06000E6D RID: 3693 RVA: 0x0005EC59 File Offset: 0x0005CE59
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06000E6E RID: 3694 RVA: 0x0005EC5C File Offset: 0x0005CE5C
		bool IEnumerator.MoveNext()
		{
			switch (this.<>1__state)
			{
			case 0:
				this.<>1__state = -1;
				myGO = script.gameObject;
				break;
			case 1:
				this.<>1__state = -1;
				break;
			case 2:
				this.<>1__state = -1;
				break;
			default:
				return false;
			}
			if (Timing.MainThread != Thread.CurrentThread || !myGO || !(myGO.GetComponent<T>() != null))
			{
				return false;
			}
			if (!myGO.activeInHierarchy || !script.enabled)
			{
				this.<>2__current = float.NegativeInfinity;
				this.<>1__state = 2;
				return true;
			}
			if (coroutine.MoveNext())
			{
				this.<>2__current = coroutine.Current;
				this.<>1__state = 1;
				return true;
			}
			return false;
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000E6F RID: 3695 RVA: 0x0005ED44 File Offset: 0x0005CF44
		float IEnumerator<float>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x06000E70 RID: 3696 RVA: 0x0005ED4C File Offset: 0x0005CF4C
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000E71 RID: 3697 RVA: 0x0005ED53 File Offset: 0x0005CF53
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x04000C1A RID: 3098
		private int <>1__state;

		// Token: 0x04000C1B RID: 3099
		private float <>2__current;

		// Token: 0x04000C1C RID: 3100
		public T script;

		// Token: 0x04000C1D RID: 3101
		public IEnumerator<float> coroutine;

		// Token: 0x04000C1E RID: 3102
		private GameObject <myGO>5__2;
	}

	// Token: 0x02000182 RID: 386
	[CompilerGenerated]
	private sealed class <PauseWith>d__11 : IEnumerator<float>, IEnumerator, IDisposable
	{
		// Token: 0x06000E72 RID: 3698 RVA: 0x0005ED60 File Offset: 0x0005CF60
		[DebuggerHidden]
		public <PauseWith>d__11(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x06000E73 RID: 3699 RVA: 0x0005ED6F File Offset: 0x0005CF6F
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06000E74 RID: 3700 RVA: 0x0005ED74 File Offset: 0x0005CF74
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
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
				if (condition == null)
				{
					return false;
				}
			}
			if (Timing.MainThread == Thread.CurrentThread && (!condition() || !coroutine.MoveNext()))
			{
				return false;
			}
			this.<>2__current = coroutine.Current;
			this.<>1__state = 1;
			return true;
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000E75 RID: 3701 RVA: 0x0005EDEA File Offset: 0x0005CFEA
		float IEnumerator<float>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x06000E76 RID: 3702 RVA: 0x0005EDF2 File Offset: 0x0005CFF2
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000E77 RID: 3703 RVA: 0x0005EDF9 File Offset: 0x0005CFF9
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x04000C1F RID: 3103
		private int <>1__state;

		// Token: 0x04000C20 RID: 3104
		private float <>2__current;

		// Token: 0x04000C21 RID: 3105
		public Func<bool> condition;

		// Token: 0x04000C22 RID: 3106
		public IEnumerator<float> coroutine;
	}

	// Token: 0x02000183 RID: 387
	[CompilerGenerated]
	private sealed class <KillWith>d__12 : IEnumerator<float>, IEnumerator, IDisposable
	{
		// Token: 0x06000E78 RID: 3704 RVA: 0x0005EE06 File Offset: 0x0005D006
		[DebuggerHidden]
		public <KillWith>d__12(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x06000E79 RID: 3705 RVA: 0x0005EE15 File Offset: 0x0005D015
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06000E7A RID: 3706 RVA: 0x0005EE18 File Offset: 0x0005D018
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
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
			}
			if (!otherCoroutine.IsRunning || !coroutine.MoveNext())
			{
				return false;
			}
			this.<>2__current = coroutine.Current;
			this.<>1__state = 1;
			return true;
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000E7B RID: 3707 RVA: 0x0005EE7A File Offset: 0x0005D07A
		float IEnumerator<float>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x06000E7C RID: 3708 RVA: 0x0005EE82 File Offset: 0x0005D082
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000E7D RID: 3709 RVA: 0x0005EE89 File Offset: 0x0005D089
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x04000C23 RID: 3107
		private int <>1__state;

		// Token: 0x04000C24 RID: 3108
		private float <>2__current;

		// Token: 0x04000C25 RID: 3109
		public IEnumerator<float> coroutine;

		// Token: 0x04000C26 RID: 3110
		public CoroutineHandle otherCoroutine;
	}

	// Token: 0x02000184 RID: 388
	[CompilerGenerated]
	private sealed class <Append>d__13 : IEnumerator<float>, IEnumerator, IDisposable
	{
		// Token: 0x06000E7E RID: 3710 RVA: 0x0005EE96 File Offset: 0x0005D096
		[DebuggerHidden]
		public <Append>d__13(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x06000E7F RID: 3711 RVA: 0x0005EEA5 File Offset: 0x0005D0A5
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06000E80 RID: 3712 RVA: 0x0005EEA8 File Offset: 0x0005D0A8
		bool IEnumerator.MoveNext()
		{
			switch (this.<>1__state)
			{
			case 0:
				this.<>1__state = -1;
				break;
			case 1:
				this.<>1__state = -1;
				break;
			case 2:
				this.<>1__state = -1;
				goto IL_7D;
			default:
				return false;
			}
			if (coroutine.MoveNext())
			{
				this.<>2__current = coroutine.Current;
				this.<>1__state = 1;
				return true;
			}
			if (nextCoroutine == null)
			{
				return false;
			}
			IL_7D:
			if (!nextCoroutine.MoveNext())
			{
				return false;
			}
			this.<>2__current = nextCoroutine.Current;
			this.<>1__state = 2;
			return true;
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000E81 RID: 3713 RVA: 0x0005EF40 File Offset: 0x0005D140
		float IEnumerator<float>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x06000E82 RID: 3714 RVA: 0x0005EF48 File Offset: 0x0005D148
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000E83 RID: 3715 RVA: 0x0005EF4F File Offset: 0x0005D14F
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x04000C27 RID: 3111
		private int <>1__state;

		// Token: 0x04000C28 RID: 3112
		private float <>2__current;

		// Token: 0x04000C29 RID: 3113
		public IEnumerator<float> coroutine;

		// Token: 0x04000C2A RID: 3114
		public IEnumerator<float> nextCoroutine;
	}

	// Token: 0x02000185 RID: 389
	[CompilerGenerated]
	private sealed class <Append>d__14 : IEnumerator<float>, IEnumerator, IDisposable
	{
		// Token: 0x06000E84 RID: 3716 RVA: 0x0005EF5C File Offset: 0x0005D15C
		[DebuggerHidden]
		public <Append>d__14(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x06000E85 RID: 3717 RVA: 0x0005EF6B File Offset: 0x0005D16B
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06000E86 RID: 3718 RVA: 0x0005EF70 File Offset: 0x0005D170
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
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
			}
			if (!coroutine.MoveNext())
			{
				if (onDone != null)
				{
					onDone();
				}
				return false;
			}
			this.<>2__current = coroutine.Current;
			this.<>1__state = 1;
			return true;
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000E87 RID: 3719 RVA: 0x0005EFD8 File Offset: 0x0005D1D8
		float IEnumerator<float>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x06000E88 RID: 3720 RVA: 0x0005EFE0 File Offset: 0x0005D1E0
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06000E89 RID: 3721 RVA: 0x0005EFE7 File Offset: 0x0005D1E7
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x04000C2B RID: 3115
		private int <>1__state;

		// Token: 0x04000C2C RID: 3116
		private float <>2__current;

		// Token: 0x04000C2D RID: 3117
		public IEnumerator<float> coroutine;

		// Token: 0x04000C2E RID: 3118
		public Action onDone;
	}

	// Token: 0x02000186 RID: 390
	[CompilerGenerated]
	private sealed class <Prepend>d__15 : IEnumerator<float>, IEnumerator, IDisposable
	{
		// Token: 0x06000E8A RID: 3722 RVA: 0x0005EFF4 File Offset: 0x0005D1F4
		[DebuggerHidden]
		public <Prepend>d__15(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x06000E8B RID: 3723 RVA: 0x0005F003 File Offset: 0x0005D203
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06000E8C RID: 3724 RVA: 0x0005F008 File Offset: 0x0005D208
		bool IEnumerator.MoveNext()
		{
			switch (this.<>1__state)
			{
			case 0:
				this.<>1__state = -1;
				if (lastCoroutine == null)
				{
					goto IL_7D;
				}
				break;
			case 1:
				this.<>1__state = -1;
				break;
			case 2:
				this.<>1__state = -1;
				goto IL_7D;
			default:
				return false;
			}
			if (lastCoroutine.MoveNext())
			{
				this.<>2__current = lastCoroutine.Current;
				this.<>1__state = 1;
				return true;
			}
			IL_7D:
			if (!coroutine.MoveNext())
			{
				return false;
			}
			this.<>2__current = coroutine.Current;
			this.<>1__state = 2;
			return true;
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000E8D RID: 3725 RVA: 0x0005F0A0 File Offset: 0x0005D2A0
		float IEnumerator<float>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x06000E8E RID: 3726 RVA: 0x0005F0A8 File Offset: 0x0005D2A8
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000E8F RID: 3727 RVA: 0x0005F0AF File Offset: 0x0005D2AF
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x04000C2F RID: 3119
		private int <>1__state;

		// Token: 0x04000C30 RID: 3120
		private float <>2__current;

		// Token: 0x04000C31 RID: 3121
		public IEnumerator<float> lastCoroutine;

		// Token: 0x04000C32 RID: 3122
		public IEnumerator<float> coroutine;
	}

	// Token: 0x02000187 RID: 391
	[CompilerGenerated]
	private sealed class <Prepend>d__16 : IEnumerator<float>, IEnumerator, IDisposable
	{
		// Token: 0x06000E90 RID: 3728 RVA: 0x0005F0BC File Offset: 0x0005D2BC
		[DebuggerHidden]
		public <Prepend>d__16(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x06000E91 RID: 3729 RVA: 0x0005F0CB File Offset: 0x0005D2CB
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06000E92 RID: 3730 RVA: 0x0005F0D0 File Offset: 0x0005D2D0
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
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
				if (onStart != null)
				{
					onStart();
				}
			}
			if (!coroutine.MoveNext())
			{
				return false;
			}
			this.<>2__current = coroutine.Current;
			this.<>1__state = 1;
			return true;
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000E93 RID: 3731 RVA: 0x0005F138 File Offset: 0x0005D338
		float IEnumerator<float>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x06000E94 RID: 3732 RVA: 0x0005F140 File Offset: 0x0005D340
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000E95 RID: 3733 RVA: 0x0005F147 File Offset: 0x0005D347
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x04000C33 RID: 3123
		private int <>1__state;

		// Token: 0x04000C34 RID: 3124
		private float <>2__current;

		// Token: 0x04000C35 RID: 3125
		public Action onStart;

		// Token: 0x04000C36 RID: 3126
		public IEnumerator<float> coroutine;
	}

	// Token: 0x02000188 RID: 392
	[CompilerGenerated]
	private sealed class <Superimpose>d__18 : IEnumerator<float>, IEnumerator, IDisposable
	{
		// Token: 0x06000E96 RID: 3734 RVA: 0x0005F154 File Offset: 0x0005D354
		[DebuggerHidden]
		public <Superimpose>d__18(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x06000E97 RID: 3735 RVA: 0x0005F163 File Offset: 0x0005D363
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06000E98 RID: 3736 RVA: 0x0005F168 File Offset: 0x0005D368
		bool IEnumerator.MoveNext()
		{
			switch (this.<>1__state)
			{
			case 0:
				this.<>1__state = -1;
				break;
			case 1:
				this.<>1__state = -1;
				break;
			case 2:
				this.<>1__state = -1;
				break;
			case 3:
				this.<>1__state = -1;
				break;
			case 4:
				this.<>1__state = -1;
				break;
			default:
				return false;
			}
			while (coroutineA != null || coroutineB != null)
			{
				if (coroutineA != null && instance.localTime >= coroutineA.Current && !coroutineA.MoveNext())
				{
					coroutineA = null;
				}
				if (coroutineB != null && instance.localTime >= coroutineB.Current && !coroutineB.MoveNext())
				{
					coroutineB = null;
				}
				if ((coroutineA != null && float.IsNaN(coroutineA.Current)) || (coroutineB != null && float.IsNaN(coroutineB.Current)))
				{
					this.<>2__current = float.NaN;
					this.<>1__state = 1;
					return true;
				}
				if (coroutineA != null && coroutineB != null)
				{
					this.<>2__current = ((coroutineA.Current < coroutineB.Current) ? coroutineA.Current : coroutineB.Current);
					this.<>1__state = 2;
					return true;
				}
				if (coroutineA == null && coroutineB != null)
				{
					this.<>2__current = coroutineB.Current;
					this.<>1__state = 3;
					return true;
				}
				if (coroutineA != null)
				{
					this.<>2__current = coroutineA.Current;
					this.<>1__state = 4;
					return true;
				}
			}
			return false;
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000E99 RID: 3737 RVA: 0x0005F32B File Offset: 0x0005D52B
		float IEnumerator<float>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x06000E9A RID: 3738 RVA: 0x0005F333 File Offset: 0x0005D533
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000E9B RID: 3739 RVA: 0x0005F33A File Offset: 0x0005D53A
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x04000C37 RID: 3127
		private int <>1__state;

		// Token: 0x04000C38 RID: 3128
		private float <>2__current;

		// Token: 0x04000C39 RID: 3129
		public IEnumerator<float> coroutineA;

		// Token: 0x04000C3A RID: 3130
		public Timing instance;

		// Token: 0x04000C3B RID: 3131
		public IEnumerator<float> coroutineB;
	}

	// Token: 0x02000189 RID: 393
	[CompilerGenerated]
	private sealed class <Hijack>d__19 : IEnumerator<float>, IEnumerator, IDisposable
	{
		// Token: 0x06000E9C RID: 3740 RVA: 0x0005F347 File Offset: 0x0005D547
		[DebuggerHidden]
		public <Hijack>d__19(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x06000E9D RID: 3741 RVA: 0x0005F356 File Offset: 0x0005D556
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06000E9E RID: 3742 RVA: 0x0005F358 File Offset: 0x0005D558
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
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
				if (newReturn == null)
				{
					return false;
				}
			}
			if (!coroutine.MoveNext())
			{
				return false;
			}
			this.<>2__current = newReturn(coroutine.Current);
			this.<>1__state = 1;
			return true;
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000E9F RID: 3743 RVA: 0x0005F3C0 File Offset: 0x0005D5C0
		float IEnumerator<float>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x06000EA0 RID: 3744 RVA: 0x0005F3C8 File Offset: 0x0005D5C8
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000EA1 RID: 3745 RVA: 0x0005F3CF File Offset: 0x0005D5CF
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x04000C3C RID: 3132
		private int <>1__state;

		// Token: 0x04000C3D RID: 3133
		private float <>2__current;

		// Token: 0x04000C3E RID: 3134
		public Func<float, float> newReturn;

		// Token: 0x04000C3F RID: 3135
		public IEnumerator<float> coroutine;
	}

	// Token: 0x0200018A RID: 394
	[CompilerGenerated]
	private sealed class <RerouteExceptions>d__20 : IEnumerator<float>, IEnumerator, IDisposable
	{
		// Token: 0x06000EA2 RID: 3746 RVA: 0x0005F3DC File Offset: 0x0005D5DC
		[DebuggerHidden]
		public <RerouteExceptions>d__20(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x06000EA3 RID: 3747 RVA: 0x0005F3EB File Offset: 0x0005D5EB
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06000EA4 RID: 3748 RVA: 0x0005F3F0 File Offset: 0x0005D5F0
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
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
			}
			try
			{
				if (!coroutine.MoveNext())
				{
					return false;
				}
			}
			catch (Exception obj)
			{
				if (exceptionHandler != null)
				{
					exceptionHandler(obj);
				}
				return false;
			}
			this.<>2__current = coroutine.Current;
			this.<>1__state = 1;
			return true;
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06000EA5 RID: 3749 RVA: 0x0005F478 File Offset: 0x0005D678
		float IEnumerator<float>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x06000EA6 RID: 3750 RVA: 0x0005F480 File Offset: 0x0005D680
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000EA7 RID: 3751 RVA: 0x0005F487 File Offset: 0x0005D687
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x04000C40 RID: 3136
		private int <>1__state;

		// Token: 0x04000C41 RID: 3137
		private float <>2__current;

		// Token: 0x04000C42 RID: 3138
		public IEnumerator<float> coroutine;

		// Token: 0x04000C43 RID: 3139
		public Action<Exception> exceptionHandler;
	}
}
