using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;

namespace Steamworks
{
	// Token: 0x02000182 RID: 386
	public sealed class CallResult<T> : CallResult, IDisposable
	{
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x060008BC RID: 2236 RVA: 0x0000CB8C File Offset: 0x0000AD8C
		// (remove) Token: 0x060008BD RID: 2237 RVA: 0x0000CBC4 File Offset: 0x0000ADC4
		private event CallResult<T>.APIDispatchDelegate m_Func
		{
			[CompilerGenerated]
			add
			{
				CallResult<T>.APIDispatchDelegate apidispatchDelegate = this.m_Func;
				CallResult<T>.APIDispatchDelegate apidispatchDelegate2;
				do
				{
					apidispatchDelegate2 = apidispatchDelegate;
					CallResult<T>.APIDispatchDelegate value2 = (CallResult<T>.APIDispatchDelegate)Delegate.Combine(apidispatchDelegate2, value);
					apidispatchDelegate = Interlocked.CompareExchange<CallResult<T>.APIDispatchDelegate>(ref this.m_Func, value2, apidispatchDelegate2);
				}
				while (apidispatchDelegate != apidispatchDelegate2);
			}
			[CompilerGenerated]
			remove
			{
				CallResult<T>.APIDispatchDelegate apidispatchDelegate = this.m_Func;
				CallResult<T>.APIDispatchDelegate apidispatchDelegate2;
				do
				{
					apidispatchDelegate2 = apidispatchDelegate;
					CallResult<T>.APIDispatchDelegate value2 = (CallResult<T>.APIDispatchDelegate)Delegate.Remove(apidispatchDelegate2, value);
					apidispatchDelegate = Interlocked.CompareExchange<CallResult<T>.APIDispatchDelegate>(ref this.m_Func, value2, apidispatchDelegate2);
				}
				while (apidispatchDelegate != apidispatchDelegate2);
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060008BE RID: 2238 RVA: 0x0000CBF9 File Offset: 0x0000ADF9
		public SteamAPICall_t Handle
		{
			get
			{
				return this.m_hAPICall;
			}
		}

		// Token: 0x060008BF RID: 2239 RVA: 0x0000CC01 File Offset: 0x0000AE01
		public static CallResult<T> Create(CallResult<T>.APIDispatchDelegate func = null)
		{
			return new CallResult<T>(func);
		}

		// Token: 0x060008C0 RID: 2240 RVA: 0x0000CC09 File Offset: 0x0000AE09
		public CallResult(CallResult<T>.APIDispatchDelegate func = null)
		{
			this.m_Func = func;
		}

		// Token: 0x060008C1 RID: 2241 RVA: 0x0000CC24 File Offset: 0x0000AE24
		~CallResult()
		{
			this.Dispose();
		}

		// Token: 0x060008C2 RID: 2242 RVA: 0x0000CC50 File Offset: 0x0000AE50
		public void Dispose()
		{
			if (this.m_bDisposed)
			{
				return;
			}
			GC.SuppressFinalize(this);
			this.Cancel();
			this.m_bDisposed = true;
		}

		// Token: 0x060008C3 RID: 2243 RVA: 0x0000CC70 File Offset: 0x0000AE70
		public void Set(SteamAPICall_t hAPICall, CallResult<T>.APIDispatchDelegate func = null)
		{
			if (func != null)
			{
				this.m_Func = func;
			}
			if (this.m_Func == null)
			{
				throw new Exception("CallResult function was null, you must either set it in the CallResult Constructor or via Set()");
			}
			if (this.m_hAPICall != SteamAPICall_t.Invalid)
			{
				CallbackDispatcher.Unregister(this.m_hAPICall, this);
			}
			this.m_hAPICall = hAPICall;
			if (hAPICall != SteamAPICall_t.Invalid)
			{
				CallbackDispatcher.Register(hAPICall, this);
			}
		}

		// Token: 0x060008C4 RID: 2244 RVA: 0x0000CCD3 File Offset: 0x0000AED3
		public bool IsActive()
		{
			return this.m_hAPICall != SteamAPICall_t.Invalid;
		}

		// Token: 0x060008C5 RID: 2245 RVA: 0x0000CCE5 File Offset: 0x0000AEE5
		public void Cancel()
		{
			if (this.IsActive())
			{
				CallbackDispatcher.Unregister(this.m_hAPICall, this);
			}
		}

		// Token: 0x060008C6 RID: 2246 RVA: 0x0000CCFB File Offset: 0x0000AEFB
		internal override Type GetCallbackType()
		{
			return typeof(T);
		}

		// Token: 0x060008C7 RID: 2247 RVA: 0x0000CD08 File Offset: 0x0000AF08
		internal override void OnRunCallResult(IntPtr pvParam, bool bFailed, ulong hSteamAPICall_)
		{
			if ((SteamAPICall_t)hSteamAPICall_ == this.m_hAPICall)
			{
				try
				{
					this.m_Func((T)((object)Marshal.PtrToStructure(pvParam, typeof(T))), bFailed);
				}
				catch (Exception e)
				{
					CallbackDispatcher.ExceptionHandler(e);
				}
			}
		}

		// Token: 0x060008C8 RID: 2248 RVA: 0x0000CD64 File Offset: 0x0000AF64
		internal override void SetUnregistered()
		{
			this.m_hAPICall = SteamAPICall_t.Invalid;
		}

		// Token: 0x04000A3B RID: 2619
		[CompilerGenerated]
		private CallResult<T>.APIDispatchDelegate m_Func;

		// Token: 0x04000A3C RID: 2620
		private SteamAPICall_t m_hAPICall = SteamAPICall_t.Invalid;

		// Token: 0x04000A3D RID: 2621
		private bool m_bDisposed;

		// Token: 0x020001CF RID: 463
		// (Invoke) Token: 0x06000B85 RID: 2949
		public delegate void APIDispatchDelegate(T param, bool bIOFailure);
	}
}
