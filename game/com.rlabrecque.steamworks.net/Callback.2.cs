using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;

namespace Steamworks
{
	// Token: 0x02000180 RID: 384
	public sealed class Callback<T> : Callback, IDisposable
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060008AB RID: 2219 RVA: 0x0000C9F0 File Offset: 0x0000ABF0
		// (remove) Token: 0x060008AC RID: 2220 RVA: 0x0000CA28 File Offset: 0x0000AC28
		private event Callback<T>.DispatchDelegate m_Func
		{
			[CompilerGenerated]
			add
			{
				Callback<T>.DispatchDelegate dispatchDelegate = this.m_Func;
				Callback<T>.DispatchDelegate dispatchDelegate2;
				do
				{
					dispatchDelegate2 = dispatchDelegate;
					Callback<T>.DispatchDelegate value2 = (Callback<T>.DispatchDelegate)Delegate.Combine(dispatchDelegate2, value);
					dispatchDelegate = Interlocked.CompareExchange<Callback<T>.DispatchDelegate>(ref this.m_Func, value2, dispatchDelegate2);
				}
				while (dispatchDelegate != dispatchDelegate2);
			}
			[CompilerGenerated]
			remove
			{
				Callback<T>.DispatchDelegate dispatchDelegate = this.m_Func;
				Callback<T>.DispatchDelegate dispatchDelegate2;
				do
				{
					dispatchDelegate2 = dispatchDelegate;
					Callback<T>.DispatchDelegate value2 = (Callback<T>.DispatchDelegate)Delegate.Remove(dispatchDelegate2, value);
					dispatchDelegate = Interlocked.CompareExchange<Callback<T>.DispatchDelegate>(ref this.m_Func, value2, dispatchDelegate2);
				}
				while (dispatchDelegate != dispatchDelegate2);
			}
		}

		// Token: 0x060008AD RID: 2221 RVA: 0x0000CA5D File Offset: 0x0000AC5D
		public static Callback<T> Create(Callback<T>.DispatchDelegate func)
		{
			return new Callback<T>(func, false);
		}

		// Token: 0x060008AE RID: 2222 RVA: 0x0000CA66 File Offset: 0x0000AC66
		public static Callback<T> CreateGameServer(Callback<T>.DispatchDelegate func)
		{
			return new Callback<T>(func, true);
		}

		// Token: 0x060008AF RID: 2223 RVA: 0x0000CA6F File Offset: 0x0000AC6F
		public Callback(Callback<T>.DispatchDelegate func, bool bGameServer = false)
		{
			this.m_bGameServer = bGameServer;
			this.Register(func);
		}

		// Token: 0x060008B0 RID: 2224 RVA: 0x0000CA88 File Offset: 0x0000AC88
		~Callback()
		{
			this.Dispose();
		}

		// Token: 0x060008B1 RID: 2225 RVA: 0x0000CAB4 File Offset: 0x0000ACB4
		public void Dispose()
		{
			if (this.m_bDisposed)
			{
				return;
			}
			GC.SuppressFinalize(this);
			if (this.m_bIsRegistered)
			{
				this.Unregister();
			}
			this.m_bDisposed = true;
		}

		// Token: 0x060008B2 RID: 2226 RVA: 0x0000CADA File Offset: 0x0000ACDA
		public void Register(Callback<T>.DispatchDelegate func)
		{
			if (func == null)
			{
				throw new Exception("Callback function must not be null.");
			}
			if (this.m_bIsRegistered)
			{
				this.Unregister();
			}
			this.m_Func = func;
			CallbackDispatcher.Register(this);
			this.m_bIsRegistered = true;
		}

		// Token: 0x060008B3 RID: 2227 RVA: 0x0000CB0C File Offset: 0x0000AD0C
		public void Unregister()
		{
			CallbackDispatcher.Unregister(this);
			this.m_bIsRegistered = false;
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060008B4 RID: 2228 RVA: 0x0000CB1B File Offset: 0x0000AD1B
		public override bool IsGameServer
		{
			get
			{
				return this.m_bGameServer;
			}
		}

		// Token: 0x060008B5 RID: 2229 RVA: 0x0000CB23 File Offset: 0x0000AD23
		internal override Type GetCallbackType()
		{
			return typeof(T);
		}

		// Token: 0x060008B6 RID: 2230 RVA: 0x0000CB30 File Offset: 0x0000AD30
		internal override void OnRunCallback(IntPtr pvParam)
		{
			try
			{
				this.m_Func((T)((object)Marshal.PtrToStructure(pvParam, typeof(T))));
			}
			catch (Exception e)
			{
				CallbackDispatcher.ExceptionHandler(e);
			}
		}

		// Token: 0x060008B7 RID: 2231 RVA: 0x0000CB78 File Offset: 0x0000AD78
		internal override void SetUnregistered()
		{
			this.m_bIsRegistered = false;
		}

		// Token: 0x04000A37 RID: 2615
		[CompilerGenerated]
		private Callback<T>.DispatchDelegate m_Func;

		// Token: 0x04000A38 RID: 2616
		private bool m_bGameServer;

		// Token: 0x04000A39 RID: 2617
		private bool m_bIsRegistered;

		// Token: 0x04000A3A RID: 2618
		private bool m_bDisposed;

		// Token: 0x020001CE RID: 462
		// (Invoke) Token: 0x06000B81 RID: 2945
		public delegate void DispatchDelegate(T param);
	}
}
