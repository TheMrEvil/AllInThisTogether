using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000002 RID: 2
	[NativeHeader("UnityAnalyticsScriptingClasses.h")]
	[NativeHeader("Modules/UnityAnalytics/RemoteSettings/RemoteSettings.h")]
	public static class RemoteSettings
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		// (remove) Token: 0x06000002 RID: 2 RVA: 0x00002084 File Offset: 0x00000284
		public static event RemoteSettings.UpdatedEventHandler Updated
		{
			[CompilerGenerated]
			add
			{
				RemoteSettings.UpdatedEventHandler updatedEventHandler = RemoteSettings.Updated;
				RemoteSettings.UpdatedEventHandler updatedEventHandler2;
				do
				{
					updatedEventHandler2 = updatedEventHandler;
					RemoteSettings.UpdatedEventHandler value2 = (RemoteSettings.UpdatedEventHandler)Delegate.Combine(updatedEventHandler2, value);
					updatedEventHandler = Interlocked.CompareExchange<RemoteSettings.UpdatedEventHandler>(ref RemoteSettings.Updated, value2, updatedEventHandler2);
				}
				while (updatedEventHandler != updatedEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				RemoteSettings.UpdatedEventHandler updatedEventHandler = RemoteSettings.Updated;
				RemoteSettings.UpdatedEventHandler updatedEventHandler2;
				do
				{
					updatedEventHandler2 = updatedEventHandler;
					RemoteSettings.UpdatedEventHandler value2 = (RemoteSettings.UpdatedEventHandler)Delegate.Remove(updatedEventHandler2, value);
					updatedEventHandler = Interlocked.CompareExchange<RemoteSettings.UpdatedEventHandler>(ref RemoteSettings.Updated, value2, updatedEventHandler2);
				}
				while (updatedEventHandler != updatedEventHandler2);
			}
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000003 RID: 3 RVA: 0x000020B8 File Offset: 0x000002B8
		// (remove) Token: 0x06000004 RID: 4 RVA: 0x000020EC File Offset: 0x000002EC
		public static event Action BeforeFetchFromServer
		{
			[CompilerGenerated]
			add
			{
				Action action = RemoteSettings.BeforeFetchFromServer;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref RemoteSettings.BeforeFetchFromServer, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = RemoteSettings.BeforeFetchFromServer;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref RemoteSettings.BeforeFetchFromServer, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000005 RID: 5 RVA: 0x00002120 File Offset: 0x00000320
		// (remove) Token: 0x06000006 RID: 6 RVA: 0x00002154 File Offset: 0x00000354
		public static event Action<bool, bool, int> Completed
		{
			[CompilerGenerated]
			add
			{
				Action<bool, bool, int> action = RemoteSettings.Completed;
				Action<bool, bool, int> action2;
				do
				{
					action2 = action;
					Action<bool, bool, int> value2 = (Action<bool, bool, int>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<bool, bool, int>>(ref RemoteSettings.Completed, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<bool, bool, int> action = RemoteSettings.Completed;
				Action<bool, bool, int> action2;
				do
				{
					action2 = action;
					Action<bool, bool, int> value2 = (Action<bool, bool, int>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<bool, bool, int>>(ref RemoteSettings.Completed, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002188 File Offset: 0x00000388
		[RequiredByNativeCode]
		internal static void RemoteSettingsUpdated(bool wasLastUpdatedFromServer)
		{
			RemoteSettings.UpdatedEventHandler updated = RemoteSettings.Updated;
			bool flag = updated != null;
			if (flag)
			{
				updated();
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000021AC File Offset: 0x000003AC
		[RequiredByNativeCode]
		internal static void RemoteSettingsBeforeFetchFromServer()
		{
			Action beforeFetchFromServer = RemoteSettings.BeforeFetchFromServer;
			bool flag = beforeFetchFromServer != null;
			if (flag)
			{
				beforeFetchFromServer();
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000021D0 File Offset: 0x000003D0
		[RequiredByNativeCode]
		internal static void RemoteSettingsUpdateCompleted(bool wasLastUpdatedFromServer, bool settingsChanged, int response)
		{
			Action<bool, bool, int> completed = RemoteSettings.Completed;
			bool flag = completed != null;
			if (flag)
			{
				completed(wasLastUpdatedFromServer, settingsChanged, response);
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021F6 File Offset: 0x000003F6
		[Obsolete("Calling CallOnUpdate() is not necessary any more and should be removed. Use RemoteSettingsUpdated instead", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static void CallOnUpdate()
		{
			throw new NotSupportedException("Calling CallOnUpdate() is not necessary any more and should be removed.");
		}

		// Token: 0x0600000B RID: 11
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ForceUpdate();

		// Token: 0x0600000C RID: 12
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool WasLastUpdatedFromServer();

		// Token: 0x0600000D RID: 13 RVA: 0x00002204 File Offset: 0x00000404
		[ExcludeFromDocs]
		public static int GetInt(string key)
		{
			return RemoteSettings.GetInt(key, 0);
		}

		// Token: 0x0600000E RID: 14
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetInt(string key, [UnityEngine.Internal.DefaultValue("0")] int defaultValue);

		// Token: 0x0600000F RID: 15 RVA: 0x00002220 File Offset: 0x00000420
		[ExcludeFromDocs]
		public static long GetLong(string key)
		{
			return RemoteSettings.GetLong(key, 0L);
		}

		// Token: 0x06000010 RID: 16
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern long GetLong(string key, [UnityEngine.Internal.DefaultValue("0")] long defaultValue);

		// Token: 0x06000011 RID: 17 RVA: 0x0000223C File Offset: 0x0000043C
		[ExcludeFromDocs]
		public static float GetFloat(string key)
		{
			return RemoteSettings.GetFloat(key, 0f);
		}

		// Token: 0x06000012 RID: 18
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float GetFloat(string key, [UnityEngine.Internal.DefaultValue("0.0F")] float defaultValue);

		// Token: 0x06000013 RID: 19 RVA: 0x0000225C File Offset: 0x0000045C
		[ExcludeFromDocs]
		public static string GetString(string key)
		{
			return RemoteSettings.GetString(key, "");
		}

		// Token: 0x06000014 RID: 20
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string GetString(string key, [UnityEngine.Internal.DefaultValue("\"\"")] string defaultValue);

		// Token: 0x06000015 RID: 21 RVA: 0x0000227C File Offset: 0x0000047C
		[ExcludeFromDocs]
		public static bool GetBool(string key)
		{
			return RemoteSettings.GetBool(key, false);
		}

		// Token: 0x06000016 RID: 22
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool GetBool(string key, [UnityEngine.Internal.DefaultValue("false")] bool defaultValue);

		// Token: 0x06000017 RID: 23
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool HasKey(string key);

		// Token: 0x06000018 RID: 24
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetCount();

		// Token: 0x06000019 RID: 25
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string[] GetKeys();

		// Token: 0x0600001A RID: 26 RVA: 0x00002298 File Offset: 0x00000498
		public static T GetObject<T>(string key = "")
		{
			return (T)((object)RemoteSettings.GetObject(typeof(T), key));
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000022C0 File Offset: 0x000004C0
		public static object GetObject(Type type, string key = "")
		{
			bool flag = type == null;
			if (flag)
			{
				throw new ArgumentNullException("type");
			}
			bool flag2 = type.IsAbstract || type.IsSubclassOf(typeof(Object));
			if (flag2)
			{
				throw new ArgumentException("Cannot deserialize to new instances of type '" + type.Name + ".'");
			}
			return RemoteSettings.GetAsScriptingObject(type, null, key);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002328 File Offset: 0x00000528
		public static object GetObject(string key, object defaultValue)
		{
			bool flag = defaultValue == null;
			if (flag)
			{
				throw new ArgumentNullException("defaultValue");
			}
			Type type = defaultValue.GetType();
			bool flag2 = type.IsAbstract || type.IsSubclassOf(typeof(Object));
			if (flag2)
			{
				throw new ArgumentException("Cannot deserialize to new instances of type '" + type.Name + ".'");
			}
			return RemoteSettings.GetAsScriptingObject(type, defaultValue, key);
		}

		// Token: 0x0600001D RID: 29
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern object GetAsScriptingObject(Type t, object defaultValue, string key);

		// Token: 0x0600001E RID: 30 RVA: 0x00002398 File Offset: 0x00000598
		public static IDictionary<string, object> GetDictionary(string key = "")
		{
			RemoteSettings.UseSafeLock();
			IDictionary<string, object> dictionary = RemoteConfigSettingsHelper.GetDictionary(RemoteSettings.GetSafeTopMap(), key);
			RemoteSettings.ReleaseSafeLock();
			return dictionary;
		}

		// Token: 0x0600001F RID: 31
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void UseSafeLock();

		// Token: 0x06000020 RID: 32
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ReleaseSafeLock();

		// Token: 0x06000021 RID: 33
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr GetSafeTopMap();

		// Token: 0x04000001 RID: 1
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private static RemoteSettings.UpdatedEventHandler Updated;

		// Token: 0x04000002 RID: 2
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action BeforeFetchFromServer;

		// Token: 0x04000003 RID: 3
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private static Action<bool, bool, int> Completed;

		// Token: 0x02000003 RID: 3
		// (Invoke) Token: 0x06000023 RID: 35
		public delegate void UpdatedEventHandler();
	}
}
