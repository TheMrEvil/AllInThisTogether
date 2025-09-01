using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Steamworks.Data;

namespace Steamworks
{
	// Token: 0x02000004 RID: 4
	internal struct CallResult<T> : INotifyCompletion where T : struct, ICallbackData
	{
		// Token: 0x06000003 RID: 3 RVA: 0x00002064 File Offset: 0x00000264
		public CallResult(SteamAPICall_t call, bool server)
		{
			this.call = call;
			this.server = server;
			this.utils = ((server ? SteamSharedClass<SteamUtils>.InterfaceServer : SteamSharedClass<SteamUtils>.InterfaceClient) as ISteamUtils);
			bool flag = this.utils == null;
			if (flag)
			{
				this.utils = (SteamSharedClass<SteamUtils>.Interface as ISteamUtils);
			}
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020B7 File Offset: 0x000002B7
		public void OnCompleted(Action continuation)
		{
			Dispatch.OnCallComplete<T>(this.call, continuation, this.server);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020D0 File Offset: 0x000002D0
		public T? GetResult()
		{
			bool flag = false;
			bool flag2 = !this.utils.IsAPICallCompleted(this.call, ref flag) || flag;
			T? result;
			if (flag2)
			{
				result = null;
			}
			else
			{
				T t = default(T);
				int dataSize = t.DataSize;
				IntPtr intPtr = Marshal.AllocHGlobal(dataSize);
				try
				{
					bool flag3 = !this.utils.GetAPICallResult(this.call, intPtr, dataSize, (int)t.CallbackType, ref flag) || flag;
					if (flag3)
					{
						Action<CallbackType, string, bool> onDebugCallback = Dispatch.OnDebugCallback;
						if (onDebugCallback != null)
						{
							onDebugCallback(t.CallbackType, "!GetAPICallResult or failed", this.server);
						}
						result = null;
					}
					else
					{
						Action<CallbackType, string, bool> onDebugCallback2 = Dispatch.OnDebugCallback;
						if (onDebugCallback2 != null)
						{
							onDebugCallback2(t.CallbackType, Dispatch.CallbackToString(t.CallbackType, intPtr, dataSize), this.server);
						}
						result = new T?((T)((object)Marshal.PtrToStructure(intPtr, typeof(T))));
					}
				}
				finally
				{
					Marshal.FreeHGlobal(intPtr);
				}
			}
			return result;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000006 RID: 6 RVA: 0x00002204 File Offset: 0x00000404
		public bool IsCompleted
		{
			get
			{
				bool flag = false;
				return this.utils.IsAPICallCompleted(this.call, ref flag) || flag;
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002238 File Offset: 0x00000438
		internal CallResult<T> GetAwaiter()
		{
			return this;
		}

		// Token: 0x04000001 RID: 1
		private SteamAPICall_t call;

		// Token: 0x04000002 RID: 2
		private ISteamUtils utils;

		// Token: 0x04000003 RID: 3
		private bool server;
	}
}
