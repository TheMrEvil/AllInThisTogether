using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Steamworks
{
	// Token: 0x0200017E RID: 382
	public static class CallbackDispatcher
	{
		// Token: 0x0600089B RID: 2203 RVA: 0x0000C277 File Offset: 0x0000A477
		public static void ExceptionHandler(Exception e)
		{
			Debug.LogException(e);
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600089C RID: 2204 RVA: 0x0000C27F File Offset: 0x0000A47F
		public static bool IsInitialized
		{
			get
			{
				return CallbackDispatcher.m_initCount > 0;
			}
		}

		// Token: 0x0600089D RID: 2205 RVA: 0x0000C28C File Offset: 0x0000A48C
		internal static void Initialize()
		{
			object sync = CallbackDispatcher.m_sync;
			lock (sync)
			{
				if (CallbackDispatcher.m_initCount == 0)
				{
					NativeMethods.SteamAPI_ManualDispatch_Init();
					CallbackDispatcher.m_pCallbackMsg = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(CallbackMsg_t)));
				}
				CallbackDispatcher.m_initCount++;
			}
		}

		// Token: 0x0600089E RID: 2206 RVA: 0x0000C2F8 File Offset: 0x0000A4F8
		internal static void Shutdown()
		{
			object sync = CallbackDispatcher.m_sync;
			lock (sync)
			{
				CallbackDispatcher.m_initCount--;
				if (CallbackDispatcher.m_initCount == 0)
				{
					CallbackDispatcher.UnregisterAll();
					Marshal.FreeHGlobal(CallbackDispatcher.m_pCallbackMsg);
					CallbackDispatcher.m_pCallbackMsg = IntPtr.Zero;
				}
			}
		}

		// Token: 0x0600089F RID: 2207 RVA: 0x0000C360 File Offset: 0x0000A560
		internal static void Register(Callback cb)
		{
			int callbackIdentity = CallbackIdentities.GetCallbackIdentity(cb.GetCallbackType());
			Dictionary<int, List<Callback>> dictionary = cb.IsGameServer ? CallbackDispatcher.m_registeredGameServerCallbacks : CallbackDispatcher.m_registeredCallbacks;
			object sync = CallbackDispatcher.m_sync;
			lock (sync)
			{
				List<Callback> list;
				if (!dictionary.TryGetValue(callbackIdentity, out list))
				{
					list = new List<Callback>();
					dictionary.Add(callbackIdentity, list);
				}
				list.Add(cb);
			}
		}

		// Token: 0x060008A0 RID: 2208 RVA: 0x0000C3E0 File Offset: 0x0000A5E0
		internal static void Register(SteamAPICall_t asyncCall, CallResult cr)
		{
			object sync = CallbackDispatcher.m_sync;
			lock (sync)
			{
				List<CallResult> list;
				if (!CallbackDispatcher.m_registeredCallResults.TryGetValue((ulong)asyncCall, out list))
				{
					list = new List<CallResult>();
					CallbackDispatcher.m_registeredCallResults.Add((ulong)asyncCall, list);
				}
				list.Add(cr);
			}
		}

		// Token: 0x060008A1 RID: 2209 RVA: 0x0000C44C File Offset: 0x0000A64C
		internal static void Unregister(Callback cb)
		{
			int callbackIdentity = CallbackIdentities.GetCallbackIdentity(cb.GetCallbackType());
			Dictionary<int, List<Callback>> dictionary = cb.IsGameServer ? CallbackDispatcher.m_registeredGameServerCallbacks : CallbackDispatcher.m_registeredCallbacks;
			object sync = CallbackDispatcher.m_sync;
			lock (sync)
			{
				List<Callback> list;
				if (dictionary.TryGetValue(callbackIdentity, out list))
				{
					list.Remove(cb);
					if (list.Count == 0)
					{
						dictionary.Remove(callbackIdentity);
					}
				}
			}
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x0000C4CC File Offset: 0x0000A6CC
		internal static void Unregister(SteamAPICall_t asyncCall, CallResult cr)
		{
			object sync = CallbackDispatcher.m_sync;
			lock (sync)
			{
				List<CallResult> list;
				if (CallbackDispatcher.m_registeredCallResults.TryGetValue((ulong)asyncCall, out list))
				{
					list.Remove(cr);
					if (list.Count == 0)
					{
						CallbackDispatcher.m_registeredCallResults.Remove((ulong)asyncCall);
					}
				}
			}
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x0000C53C File Offset: 0x0000A73C
		private static void UnregisterAll()
		{
			List<Callback> list = new List<Callback>();
			List<CallResult> list2 = new List<CallResult>();
			object sync = CallbackDispatcher.m_sync;
			lock (sync)
			{
				foreach (KeyValuePair<int, List<Callback>> keyValuePair in CallbackDispatcher.m_registeredCallbacks)
				{
					list.AddRange(keyValuePair.Value);
				}
				CallbackDispatcher.m_registeredCallbacks.Clear();
				foreach (KeyValuePair<int, List<Callback>> keyValuePair2 in CallbackDispatcher.m_registeredGameServerCallbacks)
				{
					list.AddRange(keyValuePair2.Value);
				}
				CallbackDispatcher.m_registeredGameServerCallbacks.Clear();
				foreach (KeyValuePair<ulong, List<CallResult>> keyValuePair3 in CallbackDispatcher.m_registeredCallResults)
				{
					list2.AddRange(keyValuePair3.Value);
				}
				CallbackDispatcher.m_registeredCallResults.Clear();
				foreach (Callback callback in list)
				{
					callback.SetUnregistered();
				}
				foreach (CallResult callResult in list2)
				{
					callResult.SetUnregistered();
				}
			}
		}

		// Token: 0x060008A4 RID: 2212 RVA: 0x0000C738 File Offset: 0x0000A938
		internal static void RunFrame(bool isGameServer)
		{
			if (!CallbackDispatcher.IsInitialized)
			{
				throw new InvalidOperationException("Callback dispatcher is not initialized.");
			}
			HSteamPipe hSteamPipe = (HSteamPipe)(isGameServer ? NativeMethods.SteamGameServer_GetHSteamPipe() : NativeMethods.SteamAPI_GetHSteamPipe());
			NativeMethods.SteamAPI_ManualDispatch_RunFrame(hSteamPipe);
			Dictionary<int, List<Callback>> dictionary = isGameServer ? CallbackDispatcher.m_registeredGameServerCallbacks : CallbackDispatcher.m_registeredCallbacks;
			while (NativeMethods.SteamAPI_ManualDispatch_GetNextCallback(hSteamPipe, CallbackDispatcher.m_pCallbackMsg))
			{
				CallbackMsg_t callbackMsg_t = (CallbackMsg_t)Marshal.PtrToStructure(CallbackDispatcher.m_pCallbackMsg, typeof(CallbackMsg_t));
				try
				{
					List<Callback> collection;
					if (callbackMsg_t.m_iCallback == 703)
					{
						SteamAPICallCompleted_t steamAPICallCompleted_t = (SteamAPICallCompleted_t)Marshal.PtrToStructure(callbackMsg_t.m_pubParam, typeof(SteamAPICallCompleted_t));
						IntPtr intPtr = Marshal.AllocHGlobal((int)steamAPICallCompleted_t.m_cubParam);
						bool bFailed;
						if (NativeMethods.SteamAPI_ManualDispatch_GetAPICallResult(hSteamPipe, steamAPICallCompleted_t.m_hAsyncCall, intPtr, (int)steamAPICallCompleted_t.m_cubParam, steamAPICallCompleted_t.m_iCallback, out bFailed))
						{
							object sync = CallbackDispatcher.m_sync;
							lock (sync)
							{
								List<CallResult> list;
								if (CallbackDispatcher.m_registeredCallResults.TryGetValue((ulong)steamAPICallCompleted_t.m_hAsyncCall, out list))
								{
									CallbackDispatcher.m_registeredCallResults.Remove((ulong)steamAPICallCompleted_t.m_hAsyncCall);
									foreach (CallResult callResult in list)
									{
										callResult.OnRunCallResult(intPtr, bFailed, (ulong)steamAPICallCompleted_t.m_hAsyncCall);
										callResult.SetUnregistered();
									}
								}
							}
						}
						Marshal.FreeHGlobal(intPtr);
					}
					else if (dictionary.TryGetValue(callbackMsg_t.m_iCallback, out collection))
					{
						object sync = CallbackDispatcher.m_sync;
						List<Callback> list2;
						lock (sync)
						{
							list2 = new List<Callback>(collection);
						}
						foreach (Callback callback in list2)
						{
							callback.OnRunCallback(callbackMsg_t.m_pubParam);
						}
					}
				}
				catch (Exception e)
				{
					CallbackDispatcher.ExceptionHandler(e);
				}
				finally
				{
					NativeMethods.SteamAPI_ManualDispatch_FreeLastCallback(hSteamPipe);
				}
			}
		}

		// Token: 0x060008A5 RID: 2213 RVA: 0x0000C9BC File Offset: 0x0000ABBC
		// Note: this type is marked as 'beforefieldinit'.
		static CallbackDispatcher()
		{
		}

		// Token: 0x04000A31 RID: 2609
		private static Dictionary<int, List<Callback>> m_registeredCallbacks = new Dictionary<int, List<Callback>>();

		// Token: 0x04000A32 RID: 2610
		private static Dictionary<int, List<Callback>> m_registeredGameServerCallbacks = new Dictionary<int, List<Callback>>();

		// Token: 0x04000A33 RID: 2611
		private static Dictionary<ulong, List<CallResult>> m_registeredCallResults = new Dictionary<ulong, List<CallResult>>();

		// Token: 0x04000A34 RID: 2612
		private static object m_sync = new object();

		// Token: 0x04000A35 RID: 2613
		private static IntPtr m_pCallbackMsg;

		// Token: 0x04000A36 RID: 2614
		private static int m_initCount;
	}
}
