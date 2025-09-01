using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;

// Token: 0x02000245 RID: 581
public class UnityMainThreadDispatcher : MonoBehaviour
{
	// Token: 0x060017A7 RID: 6055 RVA: 0x00094A40 File Offset: 0x00092C40
	public void Update()
	{
		Queue<Action> executionQueue = UnityMainThreadDispatcher._executionQueue;
		lock (executionQueue)
		{
			while (UnityMainThreadDispatcher._executionQueue.Count > 0)
			{
				UnityMainThreadDispatcher._executionQueue.Dequeue()();
			}
		}
	}

	// Token: 0x060017A8 RID: 6056 RVA: 0x00094A98 File Offset: 0x00092C98
	public void Enqueue(IEnumerator action)
	{
		Queue<Action> executionQueue = UnityMainThreadDispatcher._executionQueue;
		lock (executionQueue)
		{
			UnityMainThreadDispatcher._executionQueue.Enqueue(delegate
			{
				this.StartCoroutine(action);
			});
		}
	}

	// Token: 0x060017A9 RID: 6057 RVA: 0x00094AFC File Offset: 0x00092CFC
	public void Enqueue(Action action)
	{
		this.Enqueue(this.ActionWrapper(action));
	}

	// Token: 0x060017AA RID: 6058 RVA: 0x00094B0B File Offset: 0x00092D0B
	private IEnumerator ActionWrapper(Action a)
	{
		a();
		yield return null;
		yield break;
	}

	// Token: 0x060017AB RID: 6059 RVA: 0x00094B1A File Offset: 0x00092D1A
	public static bool Exists()
	{
		return UnityMainThreadDispatcher._instance != null;
	}

	// Token: 0x060017AC RID: 6060 RVA: 0x00094B27 File Offset: 0x00092D27
	public static UnityMainThreadDispatcher Instance()
	{
		if (!UnityMainThreadDispatcher.Exists())
		{
			throw new Exception("UnityMainThreadDispatcher could not find the UnityMainThreadDispatcher object. Please ensure you have added the MainThreadExecutor Prefab to your scene.");
		}
		return UnityMainThreadDispatcher._instance;
	}

	// Token: 0x060017AD RID: 6061 RVA: 0x00094B40 File Offset: 0x00092D40
	private void Awake()
	{
		this.MainThread = Thread.CurrentThread;
		if (UnityMainThreadDispatcher._instance == null)
		{
			UnityMainThreadDispatcher._instance = this;
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		}
	}

	// Token: 0x060017AE RID: 6062 RVA: 0x00094B6B File Offset: 0x00092D6B
	private void OnDestroy()
	{
		UnityMainThreadDispatcher._instance = null;
	}

	// Token: 0x060017AF RID: 6063 RVA: 0x00094B73 File Offset: 0x00092D73
	public UnityMainThreadDispatcher()
	{
	}

	// Token: 0x060017B0 RID: 6064 RVA: 0x00094B7B File Offset: 0x00092D7B
	// Note: this type is marked as 'beforefieldinit'.
	static UnityMainThreadDispatcher()
	{
	}

	// Token: 0x04001782 RID: 6018
	private static readonly Queue<Action> _executionQueue = new Queue<Action>();

	// Token: 0x04001783 RID: 6019
	private static UnityMainThreadDispatcher _instance = null;

	// Token: 0x04001784 RID: 6020
	private Thread MainThread;

	// Token: 0x02000608 RID: 1544
	[CompilerGenerated]
	private sealed class <>c__DisplayClass2_0
	{
		// Token: 0x060026EE RID: 9966 RVA: 0x000D480E File Offset: 0x000D2A0E
		public <>c__DisplayClass2_0()
		{
		}

		// Token: 0x060026EF RID: 9967 RVA: 0x000D4816 File Offset: 0x000D2A16
		internal void <Enqueue>b__0()
		{
			this.<>4__this.StartCoroutine(this.action);
		}

		// Token: 0x04002990 RID: 10640
		public UnityMainThreadDispatcher <>4__this;

		// Token: 0x04002991 RID: 10641
		public IEnumerator action;
	}

	// Token: 0x02000609 RID: 1545
	[CompilerGenerated]
	private sealed class <ActionWrapper>d__4 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x060026F0 RID: 9968 RVA: 0x000D482A File Offset: 0x000D2A2A
		[DebuggerHidden]
		public <ActionWrapper>d__4(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x060026F1 RID: 9969 RVA: 0x000D4839 File Offset: 0x000D2A39
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x060026F2 RID: 9970 RVA: 0x000D483C File Offset: 0x000D2A3C
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			if (num == 0)
			{
				this.<>1__state = -1;
				a();
				this.<>2__current = null;
				this.<>1__state = 1;
				return true;
			}
			if (num != 1)
			{
				return false;
			}
			this.<>1__state = -1;
			return false;
		}

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x060026F3 RID: 9971 RVA: 0x000D4883 File Offset: 0x000D2A83
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x060026F4 RID: 9972 RVA: 0x000D488B File Offset: 0x000D2A8B
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x060026F5 RID: 9973 RVA: 0x000D4892 File Offset: 0x000D2A92
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x04002992 RID: 10642
		private int <>1__state;

		// Token: 0x04002993 RID: 10643
		private object <>2__current;

		// Token: 0x04002994 RID: 10644
		public Action a;
	}
}
