using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Steamworks.Data;

namespace Steamworks
{
	// Token: 0x0200009C RID: 156
	public class SteamNetworkingUtils : SteamSharedClass<SteamNetworkingUtils>
	{
		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000818 RID: 2072 RVA: 0x0000D6F1 File Offset: 0x0000B8F1
		internal static ISteamNetworkingUtils Internal
		{
			get
			{
				return SteamSharedClass<SteamNetworkingUtils>.Interface as ISteamNetworkingUtils;
			}
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x0000D6FD File Offset: 0x0000B8FD
		internal override void InitializeInterface(bool server)
		{
			this.SetInterface(server, new ISteamNetworkingUtils(server));
			SteamNetworkingUtils.InstallCallbacks(server);
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x0000D715 File Offset: 0x0000B915
		private static void InstallCallbacks(bool server)
		{
			Dispatch.Install<SteamRelayNetworkStatus_t>(delegate(SteamRelayNetworkStatus_t x)
			{
				SteamNetworkingUtils.Status = x.Avail;
			}, server);
		}

		// Token: 0x1400001B RID: 27
		// (add) Token: 0x0600081B RID: 2075 RVA: 0x0000D740 File Offset: 0x0000B940
		// (remove) Token: 0x0600081C RID: 2076 RVA: 0x0000D774 File Offset: 0x0000B974
		public static event Action<NetDebugOutput, string> OnDebugOutput
		{
			[CompilerGenerated]
			add
			{
				Action<NetDebugOutput, string> action = SteamNetworkingUtils.OnDebugOutput;
				Action<NetDebugOutput, string> action2;
				do
				{
					action2 = action;
					Action<NetDebugOutput, string> value2 = (Action<NetDebugOutput, string>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<NetDebugOutput, string>>(ref SteamNetworkingUtils.OnDebugOutput, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<NetDebugOutput, string> action = SteamNetworkingUtils.OnDebugOutput;
				Action<NetDebugOutput, string> action2;
				do
				{
					action2 = action;
					Action<NetDebugOutput, string> value2 = (Action<NetDebugOutput, string>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<NetDebugOutput, string>>(ref SteamNetworkingUtils.OnDebugOutput, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600081D RID: 2077 RVA: 0x0000D7A7 File Offset: 0x0000B9A7
		// (set) Token: 0x0600081E RID: 2078 RVA: 0x0000D7AE File Offset: 0x0000B9AE
		public static SteamNetworkingAvailability Status
		{
			[CompilerGenerated]
			get
			{
				return SteamNetworkingUtils.<Status>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				SteamNetworkingUtils.<Status>k__BackingField = value;
			}
		}

		// Token: 0x0600081F RID: 2079 RVA: 0x0000D7B6 File Offset: 0x0000B9B6
		public static void InitRelayNetworkAccess()
		{
			SteamNetworkingUtils.Internal.InitRelayNetworkAccess();
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000820 RID: 2080 RVA: 0x0000D7C4 File Offset: 0x0000B9C4
		public static NetPingLocation? LocalPingLocation
		{
			get
			{
				NetPingLocation value = default(NetPingLocation);
				float localPingLocation = SteamNetworkingUtils.Internal.GetLocalPingLocation(ref value);
				bool flag = localPingLocation < 0f;
				NetPingLocation? result;
				if (flag)
				{
					result = null;
				}
				else
				{
					result = new NetPingLocation?(value);
				}
				return result;
			}
		}

		// Token: 0x06000821 RID: 2081 RVA: 0x0000D80C File Offset: 0x0000BA0C
		public static int EstimatePingTo(NetPingLocation target)
		{
			return SteamNetworkingUtils.Internal.EstimatePingTimeFromLocalHost(ref target);
		}

		// Token: 0x06000822 RID: 2082 RVA: 0x0000D82C File Offset: 0x0000BA2C
		public static async Task WaitForPingDataAsync(float maxAgeInSeconds = 300f)
		{
			bool flag = SteamNetworkingUtils.Internal.CheckPingDataUpToDate(maxAgeInSeconds);
			if (!flag)
			{
				SteamRelayNetworkStatus_t status = default(SteamRelayNetworkStatus_t);
				while (SteamNetworkingUtils.Internal.GetRelayNetworkStatus(ref status) != SteamNetworkingAvailability.Current)
				{
					await Task.Delay(10);
				}
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000823 RID: 2083 RVA: 0x0000D873 File Offset: 0x0000BA73
		public static long LocalTimestamp
		{
			get
			{
				return SteamNetworkingUtils.Internal.GetLocalTimestamp();
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000824 RID: 2084 RVA: 0x0000D87F File Offset: 0x0000BA7F
		// (set) Token: 0x06000825 RID: 2085 RVA: 0x0000D887 File Offset: 0x0000BA87
		public static float FakeSendPacketLoss
		{
			get
			{
				return SteamNetworkingUtils.GetConfigFloat(NetConfig.FakePacketLoss_Send);
			}
			set
			{
				SteamNetworkingUtils.SetConfigFloat(NetConfig.FakePacketLoss_Send, value);
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000826 RID: 2086 RVA: 0x0000D891 File Offset: 0x0000BA91
		// (set) Token: 0x06000827 RID: 2087 RVA: 0x0000D899 File Offset: 0x0000BA99
		public static float FakeRecvPacketLoss
		{
			get
			{
				return SteamNetworkingUtils.GetConfigFloat(NetConfig.FakePacketLoss_Recv);
			}
			set
			{
				SteamNetworkingUtils.SetConfigFloat(NetConfig.FakePacketLoss_Recv, value);
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000828 RID: 2088 RVA: 0x0000D8A3 File Offset: 0x0000BAA3
		// (set) Token: 0x06000829 RID: 2089 RVA: 0x0000D8AB File Offset: 0x0000BAAB
		public static float FakeSendPacketLag
		{
			get
			{
				return SteamNetworkingUtils.GetConfigFloat(NetConfig.FakePacketLag_Send);
			}
			set
			{
				SteamNetworkingUtils.SetConfigFloat(NetConfig.FakePacketLag_Send, value);
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600082A RID: 2090 RVA: 0x0000D8B5 File Offset: 0x0000BAB5
		// (set) Token: 0x0600082B RID: 2091 RVA: 0x0000D8BD File Offset: 0x0000BABD
		public static float FakeRecvPacketLag
		{
			get
			{
				return SteamNetworkingUtils.GetConfigFloat(NetConfig.FakePacketLag_Recv);
			}
			set
			{
				SteamNetworkingUtils.SetConfigFloat(NetConfig.FakePacketLag_Recv, value);
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600082C RID: 2092 RVA: 0x0000D8C7 File Offset: 0x0000BAC7
		// (set) Token: 0x0600082D RID: 2093 RVA: 0x0000D8D0 File Offset: 0x0000BAD0
		public static int ConnectionTimeout
		{
			get
			{
				return SteamNetworkingUtils.GetConfigInt(NetConfig.TimeoutInitial);
			}
			set
			{
				SteamNetworkingUtils.SetConfigInt(NetConfig.TimeoutInitial, value);
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600082E RID: 2094 RVA: 0x0000D8DB File Offset: 0x0000BADB
		// (set) Token: 0x0600082F RID: 2095 RVA: 0x0000D8E4 File Offset: 0x0000BAE4
		public static int Timeout
		{
			get
			{
				return SteamNetworkingUtils.GetConfigInt(NetConfig.TimeoutConnected);
			}
			set
			{
				SteamNetworkingUtils.SetConfigInt(NetConfig.TimeoutConnected, value);
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000830 RID: 2096 RVA: 0x0000D8EF File Offset: 0x0000BAEF
		// (set) Token: 0x06000831 RID: 2097 RVA: 0x0000D8F8 File Offset: 0x0000BAF8
		public static int SendBufferSize
		{
			get
			{
				return SteamNetworkingUtils.GetConfigInt(NetConfig.SendBufferSize);
			}
			set
			{
				SteamNetworkingUtils.SetConfigInt(NetConfig.SendBufferSize, value);
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000832 RID: 2098 RVA: 0x0000D903 File Offset: 0x0000BB03
		// (set) Token: 0x06000833 RID: 2099 RVA: 0x0000D90A File Offset: 0x0000BB0A
		public static NetDebugOutput DebugLevel
		{
			get
			{
				return SteamNetworkingUtils._debugLevel;
			}
			set
			{
				SteamNetworkingUtils._debugLevel = value;
				SteamNetworkingUtils._debugFunc = new NetDebugFunc(SteamNetworkingUtils.OnDebugMessage);
				SteamNetworkingUtils.Internal.SetDebugOutputFunction(value, SteamNetworkingUtils._debugFunc);
			}
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x0000D938 File Offset: 0x0000BB38
		[MonoPInvokeCallback]
		private static void OnDebugMessage(NetDebugOutput nType, IntPtr str)
		{
			SteamNetworkingUtils.debugMessages.Enqueue(new SteamNetworkingUtils.DebugMessage
			{
				Type = nType,
				Msg = Helpers.MemoryToString(str)
			});
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x0000D970 File Offset: 0x0000BB70
		internal static void OutputDebugMessages()
		{
			bool isEmpty = SteamNetworkingUtils.debugMessages.IsEmpty;
			if (!isEmpty)
			{
				for (;;)
				{
					SteamNetworkingUtils.DebugMessage debugMessage;
					bool flag = SteamNetworkingUtils.debugMessages.TryDequeue(out debugMessage);
					if (!flag)
					{
						break;
					}
					Action<NetDebugOutput, string> onDebugOutput = SteamNetworkingUtils.OnDebugOutput;
					if (onDebugOutput != null)
					{
						onDebugOutput(debugMessage.Type, debugMessage.Msg);
					}
				}
			}
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x0000D9C0 File Offset: 0x0000BBC0
		internal unsafe static bool SetConfigInt(NetConfig type, int value)
		{
			int* value2 = &value;
			return SteamNetworkingUtils.Internal.SetConfigValue(type, NetConfigScope.Global, IntPtr.Zero, NetConfigType.Int32, (IntPtr)((void*)value2));
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x0000D9F0 File Offset: 0x0000BBF0
		internal unsafe static int GetConfigInt(NetConfig type)
		{
			int num = 0;
			NetConfigType netConfigType = NetConfigType.Int32;
			int* value = &num;
			UIntPtr uintPtr = new UIntPtr(4U);
			NetConfigResult configValue = SteamNetworkingUtils.Internal.GetConfigValue(type, NetConfigScope.Global, IntPtr.Zero, ref netConfigType, (IntPtr)((void*)value), ref uintPtr);
			bool flag = configValue != NetConfigResult.OK;
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				result = num;
			}
			return result;
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x0000DA48 File Offset: 0x0000BC48
		internal unsafe static bool SetConfigFloat(NetConfig type, float value)
		{
			float* value2 = &value;
			return SteamNetworkingUtils.Internal.SetConfigValue(type, NetConfigScope.Global, IntPtr.Zero, NetConfigType.Float, (IntPtr)((void*)value2));
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x0000DA78 File Offset: 0x0000BC78
		internal unsafe static float GetConfigFloat(NetConfig type)
		{
			float num = 0f;
			NetConfigType netConfigType = NetConfigType.Float;
			float* value = &num;
			UIntPtr uintPtr = new UIntPtr(4U);
			NetConfigResult configValue = SteamNetworkingUtils.Internal.GetConfigValue(type, NetConfigScope.Global, IntPtr.Zero, ref netConfigType, (IntPtr)((void*)value), ref uintPtr);
			bool flag = configValue != NetConfigResult.OK;
			float result;
			if (flag)
			{
				result = 0f;
			}
			else
			{
				result = num;
			}
			return result;
		}

		// Token: 0x0600083A RID: 2106 RVA: 0x0000DAD8 File Offset: 0x0000BCD8
		internal unsafe static bool SetConfigString(NetConfig type, string value)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(value);
			byte[] array;
			byte* value2;
			if ((array = bytes) == null || array.Length == 0)
			{
				value2 = null;
			}
			else
			{
				value2 = &array[0];
			}
			return SteamNetworkingUtils.Internal.SetConfigValue(type, NetConfigScope.Global, IntPtr.Zero, NetConfigType.String, (IntPtr)((void*)value2));
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x0000DB27 File Offset: 0x0000BD27
		public SteamNetworkingUtils()
		{
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x0000DB30 File Offset: 0x0000BD30
		// Note: this type is marked as 'beforefieldinit'.
		static SteamNetworkingUtils()
		{
		}

		// Token: 0x04000709 RID: 1801
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action<NetDebugOutput, string> OnDebugOutput;

		// Token: 0x0400070A RID: 1802
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static SteamNetworkingAvailability <Status>k__BackingField;

		// Token: 0x0400070B RID: 1803
		private static NetDebugOutput _debugLevel;

		// Token: 0x0400070C RID: 1804
		private static NetDebugFunc _debugFunc;

		// Token: 0x0400070D RID: 1805
		private static ConcurrentQueue<SteamNetworkingUtils.DebugMessage> debugMessages = new ConcurrentQueue<SteamNetworkingUtils.DebugMessage>();

		// Token: 0x02000236 RID: 566
		private struct DebugMessage
		{
			// Token: 0x04000D3C RID: 3388
			public NetDebugOutput Type;

			// Token: 0x04000D3D RID: 3389
			public string Msg;
		}

		// Token: 0x02000237 RID: 567
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x0600112D RID: 4397 RVA: 0x0001E30B File Offset: 0x0001C50B
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x0600112E RID: 4398 RVA: 0x0001E317 File Offset: 0x0001C517
			public <>c()
			{
			}

			// Token: 0x0600112F RID: 4399 RVA: 0x0001E320 File Offset: 0x0001C520
			internal void <InstallCallbacks>b__3_0(SteamRelayNetworkStatus_t x)
			{
				SteamNetworkingUtils.Status = x.Avail;
			}

			// Token: 0x04000D3E RID: 3390
			public static readonly SteamNetworkingUtils.<>c <>9 = new SteamNetworkingUtils.<>c();

			// Token: 0x04000D3F RID: 3391
			public static Action<SteamRelayNetworkStatus_t> <>9__3_0;
		}

		// Token: 0x02000238 RID: 568
		[CompilerGenerated]
		private sealed class <WaitForPingDataAsync>d__15 : IAsyncStateMachine
		{
			// Token: 0x06001130 RID: 4400 RVA: 0x0001E32F File Offset: 0x0001C52F
			public <WaitForPingDataAsync>d__15()
			{
			}

			// Token: 0x06001131 RID: 4401 RVA: 0x0001E338 File Offset: 0x0001C538
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				try
				{
					TaskAwaiter taskAwaiter;
					if (num != 0)
					{
						bool flag = SteamNetworkingUtils.Internal.CheckPingDataUpToDate(maxAgeInSeconds);
						if (flag)
						{
							goto IL_CF;
						}
						status = default(SteamRelayNetworkStatus_t);
						goto IL_96;
					}
					else
					{
						TaskAwaiter taskAwaiter2;
						taskAwaiter = taskAwaiter2;
						taskAwaiter2 = default(TaskAwaiter);
						num2 = -1;
					}
					IL_8D:
					taskAwaiter.GetResult();
					IL_96:
					if (SteamNetworkingUtils.Internal.GetRelayNetworkStatus(ref status) != SteamNetworkingAvailability.Current)
					{
						taskAwaiter = Task.Delay(10).GetAwaiter();
						if (!taskAwaiter.IsCompleted)
						{
							num2 = 0;
							TaskAwaiter taskAwaiter2 = taskAwaiter;
							SteamNetworkingUtils.<WaitForPingDataAsync>d__15 <WaitForPingDataAsync>d__ = this;
							this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, SteamNetworkingUtils.<WaitForPingDataAsync>d__15>(ref taskAwaiter, ref <WaitForPingDataAsync>d__);
							return;
						}
						goto IL_8D;
					}
				}
				catch (Exception exception)
				{
					num2 = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_CF:
				num2 = -2;
				this.<>t__builder.SetResult();
			}

			// Token: 0x06001132 RID: 4402 RVA: 0x0001E438 File Offset: 0x0001C638
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			// Token: 0x04000D40 RID: 3392
			public int <>1__state;

			// Token: 0x04000D41 RID: 3393
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000D42 RID: 3394
			public float maxAgeInSeconds;

			// Token: 0x04000D43 RID: 3395
			private SteamRelayNetworkStatus_t <status>5__1;

			// Token: 0x04000D44 RID: 3396
			private TaskAwaiter <>u__1;
		}
	}
}
