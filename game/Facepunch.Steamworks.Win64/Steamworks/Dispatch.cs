using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Steamworks.Data;

namespace Steamworks
{
	// Token: 0x02000007 RID: 7
	public static class Dispatch
	{
		// Token: 0x0600000D RID: 13
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void SteamAPI_ManualDispatch_Init();

		// Token: 0x0600000E RID: 14
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void SteamAPI_ManualDispatch_RunFrame(HSteamPipe pipe);

		// Token: 0x0600000F RID: 15
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		internal static extern bool SteamAPI_ManualDispatch_GetNextCallback(HSteamPipe pipe, [In] [Out] ref Dispatch.CallbackMsg_t msg);

		// Token: 0x06000010 RID: 16
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		internal static extern bool SteamAPI_ManualDispatch_FreeLastCallback(HSteamPipe pipe);

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000011 RID: 17 RVA: 0x000022A4 File Offset: 0x000004A4
		// (set) Token: 0x06000012 RID: 18 RVA: 0x000022AB File Offset: 0x000004AB
		internal static HSteamPipe ClientPipe
		{
			[CompilerGenerated]
			get
			{
				return Dispatch.<ClientPipe>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				Dispatch.<ClientPipe>k__BackingField = value;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000013 RID: 19 RVA: 0x000022B3 File Offset: 0x000004B3
		// (set) Token: 0x06000014 RID: 20 RVA: 0x000022BA File Offset: 0x000004BA
		internal static HSteamPipe ServerPipe
		{
			[CompilerGenerated]
			get
			{
				return Dispatch.<ServerPipe>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				Dispatch.<ServerPipe>k__BackingField = value;
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000022C2 File Offset: 0x000004C2
		internal static void Init()
		{
			Dispatch.SteamAPI_ManualDispatch_Init();
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000022CC File Offset: 0x000004CC
		internal static void Frame(HSteamPipe pipe)
		{
			bool flag = Dispatch.runningFrame;
			if (!flag)
			{
				try
				{
					Dispatch.runningFrame = true;
					Dispatch.SteamAPI_ManualDispatch_RunFrame(pipe);
					SteamNetworkingUtils.OutputDebugMessages();
					Dispatch.CallbackMsg_t msg = default(Dispatch.CallbackMsg_t);
					while (Dispatch.SteamAPI_ManualDispatch_GetNextCallback(pipe, ref msg))
					{
						try
						{
							Dispatch.ProcessCallback(msg, pipe == Dispatch.ServerPipe);
						}
						finally
						{
							Dispatch.SteamAPI_ManualDispatch_FreeLastCallback(pipe);
						}
					}
				}
				catch (Exception obj)
				{
					Action<Exception> onException = Dispatch.OnException;
					if (onException != null)
					{
						onException(obj);
					}
				}
				finally
				{
					Dispatch.runningFrame = false;
				}
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002380 File Offset: 0x00000580
		private static void ProcessCallback(Dispatch.CallbackMsg_t msg, bool isServer)
		{
			Action<CallbackType, string, bool> onDebugCallback = Dispatch.OnDebugCallback;
			if (onDebugCallback != null)
			{
				onDebugCallback(msg.Type, Dispatch.CallbackToString(msg.Type, msg.Data, msg.DataSize), isServer);
			}
			bool flag = msg.Type == CallbackType.SteamAPICallCompleted;
			if (flag)
			{
				Dispatch.ProcessResult(msg);
			}
			else
			{
				List<Dispatch.Callback> list;
				bool flag2 = Dispatch.Callbacks.TryGetValue(msg.Type, out list);
				if (flag2)
				{
					Dispatch.actionsToCall.Clear();
					foreach (Dispatch.Callback callback in list)
					{
						bool flag3 = callback.server != isServer;
						if (!flag3)
						{
							Dispatch.actionsToCall.Add(callback.action);
						}
					}
					foreach (Action<IntPtr> action in Dispatch.actionsToCall)
					{
						action(msg.Data);
					}
					Dispatch.actionsToCall.Clear();
				}
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000024C0 File Offset: 0x000006C0
		internal static string CallbackToString(CallbackType type, IntPtr data, int expectedsize)
		{
			Type type2;
			bool flag = !CallbackTypeFactory.All.TryGetValue(type, out type2);
			string result;
			if (flag)
			{
				result = string.Format("[{0} not in sdk]", type);
			}
			else
			{
				object obj = data.ToType(type2);
				bool flag2 = obj == null;
				if (flag2)
				{
					result = "[null]";
				}
				else
				{
					string text = "";
					FieldInfo[] fields = type2.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
					bool flag3 = fields.Length == 0;
					if (flag3)
					{
						result = "[no fields]";
					}
					else
					{
						int num = fields.Max((FieldInfo x) => x.Name.Length) + 1;
						bool flag4 = num < 10;
						if (flag4)
						{
							num = 10;
						}
						foreach (FieldInfo fieldInfo in fields)
						{
							int num2 = num - fieldInfo.Name.Length;
							bool flag5 = num2 < 0;
							if (flag5)
							{
								num2 = 0;
							}
							text += string.Format("{0}{1}: {2}\n", new string(' ', num2), fieldInfo.Name, fieldInfo.GetValue(obj));
						}
						result = text.Trim(new char[]
						{
							'\n'
						});
					}
				}
			}
			return result;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000025F8 File Offset: 0x000007F8
		private static void ProcessResult(Dispatch.CallbackMsg_t msg)
		{
			SteamAPICallCompleted_t steamAPICallCompleted_t = msg.Data.ToType<SteamAPICallCompleted_t>();
			Dispatch.ResultCallback resultCallback;
			bool flag = !Dispatch.ResultCallbacks.TryGetValue(steamAPICallCompleted_t.AsyncCall, out resultCallback);
			if (flag)
			{
				Action<CallbackType, string, bool> onDebugCallback = Dispatch.OnDebugCallback;
				if (onDebugCallback != null)
				{
					onDebugCallback((CallbackType)steamAPICallCompleted_t.Callback, "[no callback waiting/required]", false);
				}
			}
			else
			{
				Dispatch.ResultCallbacks.Remove(steamAPICallCompleted_t.AsyncCall);
				resultCallback.continuation();
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002668 File Offset: 0x00000868
		internal static async void LoopClientAsync()
		{
			while (Dispatch.ClientPipe != 0)
			{
				Dispatch.Frame(Dispatch.ClientPipe);
				await Task.Delay(16);
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000026A0 File Offset: 0x000008A0
		internal static async void LoopServerAsync()
		{
			while (Dispatch.ServerPipe != 0)
			{
				Dispatch.Frame(Dispatch.ServerPipe);
				await Task.Delay(32);
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000026D8 File Offset: 0x000008D8
		internal static void OnCallComplete<T>(SteamAPICall_t call, Action continuation, bool server) where T : struct, ICallbackData
		{
			Dispatch.ResultCallbacks[call.Value] = new Dispatch.ResultCallback
			{
				continuation = continuation,
				server = server
			};
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002710 File Offset: 0x00000910
		internal static void Install<T>(Action<T> p, bool server = false) where T : ICallbackData
		{
			T t = default(T);
			CallbackType callbackType = t.CallbackType;
			List<Dispatch.Callback> list;
			bool flag = !Dispatch.Callbacks.TryGetValue(callbackType, out list);
			if (flag)
			{
				list = new List<Dispatch.Callback>();
				Dispatch.Callbacks[callbackType] = list;
			}
			list.Add(new Dispatch.Callback
			{
				action = delegate(IntPtr x)
				{
					p(x.ToType<T>());
				},
				server = server
			});
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002798 File Offset: 0x00000998
		internal static void ShutdownServer()
		{
			Dispatch.ServerPipe = 0;
			foreach (KeyValuePair<CallbackType, List<Dispatch.Callback>> keyValuePair in Dispatch.Callbacks)
			{
				Dispatch.Callbacks[keyValuePair.Key].RemoveAll((Dispatch.Callback x) => x.server);
			}
			Dispatch.ResultCallbacks = (from x in Dispatch.ResultCallbacks
			where !x.Value.server
			select x).ToDictionary((KeyValuePair<ulong, Dispatch.ResultCallback> x) => x.Key, (KeyValuePair<ulong, Dispatch.ResultCallback> x) => x.Value);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x0000289C File Offset: 0x00000A9C
		internal static void ShutdownClient()
		{
			Dispatch.ClientPipe = 0;
			foreach (KeyValuePair<CallbackType, List<Dispatch.Callback>> keyValuePair in Dispatch.Callbacks)
			{
				Dispatch.Callbacks[keyValuePair.Key].RemoveAll((Dispatch.Callback x) => !x.server);
			}
			Dispatch.ResultCallbacks = (from x in Dispatch.ResultCallbacks
			where x.Value.server
			select x).ToDictionary((KeyValuePair<ulong, Dispatch.ResultCallback> x) => x.Key, (KeyValuePair<ulong, Dispatch.ResultCallback> x) => x.Value);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000029A0 File Offset: 0x00000BA0
		// Note: this type is marked as 'beforefieldinit'.
		static Dispatch()
		{
		}

		// Token: 0x04000006 RID: 6
		public static Action<CallbackType, string, bool> OnDebugCallback;

		// Token: 0x04000007 RID: 7
		public static Action<Exception> OnException;

		// Token: 0x04000008 RID: 8
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static HSteamPipe <ClientPipe>k__BackingField;

		// Token: 0x04000009 RID: 9
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static HSteamPipe <ServerPipe>k__BackingField;

		// Token: 0x0400000A RID: 10
		private static bool runningFrame = false;

		// Token: 0x0400000B RID: 11
		private static List<Action<IntPtr>> actionsToCall = new List<Action<IntPtr>>();

		// Token: 0x0400000C RID: 12
		private static Dictionary<ulong, Dispatch.ResultCallback> ResultCallbacks = new Dictionary<ulong, Dispatch.ResultCallback>();

		// Token: 0x0400000D RID: 13
		private static Dictionary<CallbackType, List<Dispatch.Callback>> Callbacks = new Dictionary<CallbackType, List<Dispatch.Callback>>();

		// Token: 0x02000208 RID: 520
		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		internal struct CallbackMsg_t
		{
			// Token: 0x04000C3A RID: 3130
			public HSteamUser m_hSteamUser;

			// Token: 0x04000C3B RID: 3131
			public CallbackType Type;

			// Token: 0x04000C3C RID: 3132
			public IntPtr Data;

			// Token: 0x04000C3D RID: 3133
			public int DataSize;
		}

		// Token: 0x02000209 RID: 521
		private struct ResultCallback
		{
			// Token: 0x04000C3E RID: 3134
			public Action continuation;

			// Token: 0x04000C3F RID: 3135
			public bool server;
		}

		// Token: 0x0200020A RID: 522
		private struct Callback
		{
			// Token: 0x04000C40 RID: 3136
			public Action<IntPtr> action;

			// Token: 0x04000C41 RID: 3137
			public bool server;
		}

		// Token: 0x0200020B RID: 523
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06001068 RID: 4200 RVA: 0x0001B32E File Offset: 0x0001952E
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06001069 RID: 4201 RVA: 0x0001B33A File Offset: 0x0001953A
			public <>c()
			{
			}

			// Token: 0x0600106A RID: 4202 RVA: 0x0001B343 File Offset: 0x00019543
			internal int <CallbackToString>b__20_0(FieldInfo x)
			{
				return x.Name.Length;
			}

			// Token: 0x0600106B RID: 4203 RVA: 0x0001B350 File Offset: 0x00019550
			internal bool <ShutdownServer>b__30_3(Dispatch.Callback x)
			{
				return x.server;
			}

			// Token: 0x0600106C RID: 4204 RVA: 0x0001B358 File Offset: 0x00019558
			internal bool <ShutdownServer>b__30_0(KeyValuePair<ulong, Dispatch.ResultCallback> x)
			{
				return !x.Value.server;
			}

			// Token: 0x0600106D RID: 4205 RVA: 0x0001B369 File Offset: 0x00019569
			internal ulong <ShutdownServer>b__30_1(KeyValuePair<ulong, Dispatch.ResultCallback> x)
			{
				return x.Key;
			}

			// Token: 0x0600106E RID: 4206 RVA: 0x0001B372 File Offset: 0x00019572
			internal Dispatch.ResultCallback <ShutdownServer>b__30_2(KeyValuePair<ulong, Dispatch.ResultCallback> x)
			{
				return x.Value;
			}

			// Token: 0x0600106F RID: 4207 RVA: 0x0001B37B File Offset: 0x0001957B
			internal bool <ShutdownClient>b__31_3(Dispatch.Callback x)
			{
				return !x.server;
			}

			// Token: 0x06001070 RID: 4208 RVA: 0x0001B386 File Offset: 0x00019586
			internal bool <ShutdownClient>b__31_0(KeyValuePair<ulong, Dispatch.ResultCallback> x)
			{
				return x.Value.server;
			}

			// Token: 0x06001071 RID: 4209 RVA: 0x0001B394 File Offset: 0x00019594
			internal ulong <ShutdownClient>b__31_1(KeyValuePair<ulong, Dispatch.ResultCallback> x)
			{
				return x.Key;
			}

			// Token: 0x06001072 RID: 4210 RVA: 0x0001B39D File Offset: 0x0001959D
			internal Dispatch.ResultCallback <ShutdownClient>b__31_2(KeyValuePair<ulong, Dispatch.ResultCallback> x)
			{
				return x.Value;
			}

			// Token: 0x04000C42 RID: 3138
			public static readonly Dispatch.<>c <>9 = new Dispatch.<>c();

			// Token: 0x04000C43 RID: 3139
			public static Func<FieldInfo, int> <>9__20_0;

			// Token: 0x04000C44 RID: 3140
			public static Predicate<Dispatch.Callback> <>9__30_3;

			// Token: 0x04000C45 RID: 3141
			public static Func<KeyValuePair<ulong, Dispatch.ResultCallback>, bool> <>9__30_0;

			// Token: 0x04000C46 RID: 3142
			public static Func<KeyValuePair<ulong, Dispatch.ResultCallback>, ulong> <>9__30_1;

			// Token: 0x04000C47 RID: 3143
			public static Func<KeyValuePair<ulong, Dispatch.ResultCallback>, Dispatch.ResultCallback> <>9__30_2;

			// Token: 0x04000C48 RID: 3144
			public static Predicate<Dispatch.Callback> <>9__31_3;

			// Token: 0x04000C49 RID: 3145
			public static Func<KeyValuePair<ulong, Dispatch.ResultCallback>, bool> <>9__31_0;

			// Token: 0x04000C4A RID: 3146
			public static Func<KeyValuePair<ulong, Dispatch.ResultCallback>, ulong> <>9__31_1;

			// Token: 0x04000C4B RID: 3147
			public static Func<KeyValuePair<ulong, Dispatch.ResultCallback>, Dispatch.ResultCallback> <>9__31_2;
		}

		// Token: 0x0200020C RID: 524
		[CompilerGenerated]
		private sealed class <LoopClientAsync>d__22 : IAsyncStateMachine
		{
			// Token: 0x06001073 RID: 4211 RVA: 0x0001B3A6 File Offset: 0x000195A6
			public <LoopClientAsync>d__22()
			{
			}

			// Token: 0x06001074 RID: 4212 RVA: 0x0001B3B0 File Offset: 0x000195B0
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				try
				{
					if (num != 0)
					{
						goto IL_7C;
					}
					TaskAwaiter taskAwaiter2;
					TaskAwaiter taskAwaiter = taskAwaiter2;
					taskAwaiter2 = default(TaskAwaiter);
					num2 = -1;
					IL_73:
					taskAwaiter.GetResult();
					IL_7C:
					if (Dispatch.ClientPipe != 0)
					{
						Dispatch.Frame(Dispatch.ClientPipe);
						taskAwaiter = Task.Delay(16).GetAwaiter();
						if (!taskAwaiter.IsCompleted)
						{
							num2 = 0;
							taskAwaiter2 = taskAwaiter;
							Dispatch.<LoopClientAsync>d__22 <LoopClientAsync>d__ = this;
							this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, Dispatch.<LoopClientAsync>d__22>(ref taskAwaiter, ref <LoopClientAsync>d__);
							return;
						}
						goto IL_73;
					}
				}
				catch (Exception exception)
				{
					num2 = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				num2 = -2;
				this.<>t__builder.SetResult();
			}

			// Token: 0x06001075 RID: 4213 RVA: 0x0001B490 File Offset: 0x00019690
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			// Token: 0x04000C4C RID: 3148
			public int <>1__state;

			// Token: 0x04000C4D RID: 3149
			public AsyncVoidMethodBuilder <>t__builder;

			// Token: 0x04000C4E RID: 3150
			private TaskAwaiter <>u__1;
		}

		// Token: 0x0200020D RID: 525
		[CompilerGenerated]
		private sealed class <LoopServerAsync>d__23 : IAsyncStateMachine
		{
			// Token: 0x06001076 RID: 4214 RVA: 0x0001B492 File Offset: 0x00019692
			public <LoopServerAsync>d__23()
			{
			}

			// Token: 0x06001077 RID: 4215 RVA: 0x0001B49C File Offset: 0x0001969C
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				try
				{
					if (num != 0)
					{
						goto IL_7C;
					}
					TaskAwaiter taskAwaiter2;
					TaskAwaiter taskAwaiter = taskAwaiter2;
					taskAwaiter2 = default(TaskAwaiter);
					num2 = -1;
					IL_73:
					taskAwaiter.GetResult();
					IL_7C:
					if (Dispatch.ServerPipe != 0)
					{
						Dispatch.Frame(Dispatch.ServerPipe);
						taskAwaiter = Task.Delay(32).GetAwaiter();
						if (!taskAwaiter.IsCompleted)
						{
							num2 = 0;
							taskAwaiter2 = taskAwaiter;
							Dispatch.<LoopServerAsync>d__23 <LoopServerAsync>d__ = this;
							this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, Dispatch.<LoopServerAsync>d__23>(ref taskAwaiter, ref <LoopServerAsync>d__);
							return;
						}
						goto IL_73;
					}
				}
				catch (Exception exception)
				{
					num2 = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				num2 = -2;
				this.<>t__builder.SetResult();
			}

			// Token: 0x06001078 RID: 4216 RVA: 0x0001B57C File Offset: 0x0001977C
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			// Token: 0x04000C4F RID: 3151
			public int <>1__state;

			// Token: 0x04000C50 RID: 3152
			public AsyncVoidMethodBuilder <>t__builder;

			// Token: 0x04000C51 RID: 3153
			private TaskAwaiter <>u__1;
		}

		// Token: 0x0200020E RID: 526
		[CompilerGenerated]
		private sealed class <>c__DisplayClass29_0<T> where T : ICallbackData
		{
			// Token: 0x06001079 RID: 4217 RVA: 0x0001B57E File Offset: 0x0001977E
			public <>c__DisplayClass29_0()
			{
			}

			// Token: 0x0600107A RID: 4218 RVA: 0x0001B587 File Offset: 0x00019787
			internal void <Install>b__0(IntPtr x)
			{
				this.p(x.ToType<T>());
			}

			// Token: 0x04000C52 RID: 3154
			public Action<T> p;
		}
	}
}
