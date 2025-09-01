using System;
using System.Collections;
using System.IO;

namespace System.Diagnostics
{
	// Token: 0x0200022F RID: 559
	internal static class TraceInternal
	{
		// Token: 0x170002BD RID: 701
		// (get) Token: 0x0600106D RID: 4205 RVA: 0x000474BC File Offset: 0x000456BC
		public static TraceListenerCollection Listeners
		{
			get
			{
				TraceInternal.InitializeSettings();
				if (TraceInternal.listeners == null)
				{
					object obj = TraceInternal.critSec;
					lock (obj)
					{
						if (TraceInternal.listeners == null)
						{
							SystemDiagnosticsSection systemDiagnosticsSection = DiagnosticsConfiguration.SystemDiagnosticsSection;
							if (systemDiagnosticsSection != null)
							{
								TraceInternal.listeners = systemDiagnosticsSection.Trace.Listeners.GetRuntimeObject();
							}
							else
							{
								TraceInternal.listeners = new TraceListenerCollection();
								TraceListener traceListener = new DefaultTraceListener();
								traceListener.IndentLevel = TraceInternal.indentLevel;
								traceListener.IndentSize = TraceInternal.indentSize;
								TraceInternal.listeners.Add(traceListener);
							}
						}
					}
				}
				return TraceInternal.listeners;
			}
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x0600106E RID: 4206 RVA: 0x00047570 File Offset: 0x00045770
		internal static string AppName
		{
			get
			{
				if (TraceInternal.appName == null)
				{
					string[] commandLineArgs = Environment.GetCommandLineArgs();
					if (commandLineArgs.Length != 0)
					{
						TraceInternal.appName = Path.GetFileName(commandLineArgs[0]);
					}
				}
				return TraceInternal.appName;
			}
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x0600106F RID: 4207 RVA: 0x000475A6 File Offset: 0x000457A6
		// (set) Token: 0x06001070 RID: 4208 RVA: 0x000475B4 File Offset: 0x000457B4
		public static bool AutoFlush
		{
			get
			{
				TraceInternal.InitializeSettings();
				return TraceInternal.autoFlush;
			}
			set
			{
				TraceInternal.InitializeSettings();
				TraceInternal.autoFlush = value;
			}
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06001071 RID: 4209 RVA: 0x000475C3 File Offset: 0x000457C3
		// (set) Token: 0x06001072 RID: 4210 RVA: 0x000475D1 File Offset: 0x000457D1
		public static bool UseGlobalLock
		{
			get
			{
				TraceInternal.InitializeSettings();
				return TraceInternal.useGlobalLock;
			}
			set
			{
				TraceInternal.InitializeSettings();
				TraceInternal.useGlobalLock = value;
			}
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06001073 RID: 4211 RVA: 0x000475E0 File Offset: 0x000457E0
		// (set) Token: 0x06001074 RID: 4212 RVA: 0x000475E8 File Offset: 0x000457E8
		public static int IndentLevel
		{
			get
			{
				return TraceInternal.indentLevel;
			}
			set
			{
				object obj = TraceInternal.critSec;
				lock (obj)
				{
					if (value < 0)
					{
						value = 0;
					}
					TraceInternal.indentLevel = value;
					if (TraceInternal.listeners != null)
					{
						foreach (object obj2 in TraceInternal.Listeners)
						{
							((TraceListener)obj2).IndentLevel = TraceInternal.indentLevel;
						}
					}
				}
			}
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06001075 RID: 4213 RVA: 0x00047680 File Offset: 0x00045880
		// (set) Token: 0x06001076 RID: 4214 RVA: 0x0004768E File Offset: 0x0004588E
		public static int IndentSize
		{
			get
			{
				TraceInternal.InitializeSettings();
				return TraceInternal.indentSize;
			}
			set
			{
				TraceInternal.InitializeSettings();
				TraceInternal.SetIndentSize(value);
			}
		}

		// Token: 0x06001077 RID: 4215 RVA: 0x0004769C File Offset: 0x0004589C
		private static void SetIndentSize(int value)
		{
			object obj = TraceInternal.critSec;
			lock (obj)
			{
				if (value < 0)
				{
					value = 0;
				}
				TraceInternal.indentSize = value;
				if (TraceInternal.listeners != null)
				{
					foreach (object obj2 in TraceInternal.Listeners)
					{
						((TraceListener)obj2).IndentSize = TraceInternal.indentSize;
					}
				}
			}
		}

		// Token: 0x06001078 RID: 4216 RVA: 0x00047738 File Offset: 0x00045938
		public static void Indent()
		{
			object obj = TraceInternal.critSec;
			lock (obj)
			{
				TraceInternal.InitializeSettings();
				if (TraceInternal.indentLevel < 2147483647)
				{
					TraceInternal.indentLevel++;
				}
				foreach (object obj2 in TraceInternal.Listeners)
				{
					((TraceListener)obj2).IndentLevel = TraceInternal.indentLevel;
				}
			}
		}

		// Token: 0x06001079 RID: 4217 RVA: 0x000477D8 File Offset: 0x000459D8
		public static void Unindent()
		{
			object obj = TraceInternal.critSec;
			lock (obj)
			{
				TraceInternal.InitializeSettings();
				if (TraceInternal.indentLevel > 0)
				{
					TraceInternal.indentLevel--;
				}
				foreach (object obj2 in TraceInternal.Listeners)
				{
					((TraceListener)obj2).IndentLevel = TraceInternal.indentLevel;
				}
			}
		}

		// Token: 0x0600107A RID: 4218 RVA: 0x00047874 File Offset: 0x00045A74
		public static void Flush()
		{
			if (TraceInternal.listeners != null)
			{
				if (TraceInternal.UseGlobalLock)
				{
					object obj = TraceInternal.critSec;
					lock (obj)
					{
						using (IEnumerator enumerator = TraceInternal.Listeners.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								object obj2 = enumerator.Current;
								((TraceListener)obj2).Flush();
							}
							return;
						}
					}
				}
				foreach (object obj3 in TraceInternal.Listeners)
				{
					TraceListener traceListener = (TraceListener)obj3;
					if (!traceListener.IsThreadSafe)
					{
						TraceListener obj4 = traceListener;
						lock (obj4)
						{
							traceListener.Flush();
							continue;
						}
					}
					traceListener.Flush();
				}
			}
		}

		// Token: 0x0600107B RID: 4219 RVA: 0x00047988 File Offset: 0x00045B88
		public static void Close()
		{
			if (TraceInternal.listeners != null)
			{
				object obj = TraceInternal.critSec;
				lock (obj)
				{
					foreach (object obj2 in TraceInternal.Listeners)
					{
						((TraceListener)obj2).Close();
					}
				}
			}
		}

		// Token: 0x0600107C RID: 4220 RVA: 0x00047A0C File Offset: 0x00045C0C
		public static void Assert(bool condition)
		{
			if (condition)
			{
				return;
			}
			TraceInternal.Fail(string.Empty);
		}

		// Token: 0x0600107D RID: 4221 RVA: 0x00047A1C File Offset: 0x00045C1C
		public static void Assert(bool condition, string message)
		{
			if (condition)
			{
				return;
			}
			TraceInternal.Fail(message);
		}

		// Token: 0x0600107E RID: 4222 RVA: 0x00047A28 File Offset: 0x00045C28
		public static void Assert(bool condition, string message, string detailMessage)
		{
			if (condition)
			{
				return;
			}
			TraceInternal.Fail(message, detailMessage);
		}

		// Token: 0x0600107F RID: 4223 RVA: 0x00047A38 File Offset: 0x00045C38
		public static void Fail(string message)
		{
			if (TraceInternal.UseGlobalLock)
			{
				object obj = TraceInternal.critSec;
				lock (obj)
				{
					using (IEnumerator enumerator = TraceInternal.Listeners.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							object obj2 = enumerator.Current;
							TraceListener traceListener = (TraceListener)obj2;
							traceListener.Fail(message);
							if (TraceInternal.AutoFlush)
							{
								traceListener.Flush();
							}
						}
						return;
					}
				}
			}
			foreach (object obj3 in TraceInternal.Listeners)
			{
				TraceListener traceListener2 = (TraceListener)obj3;
				if (!traceListener2.IsThreadSafe)
				{
					TraceListener obj4 = traceListener2;
					lock (obj4)
					{
						traceListener2.Fail(message);
						if (TraceInternal.AutoFlush)
						{
							traceListener2.Flush();
						}
						continue;
					}
				}
				traceListener2.Fail(message);
				if (TraceInternal.AutoFlush)
				{
					traceListener2.Flush();
				}
			}
		}

		// Token: 0x06001080 RID: 4224 RVA: 0x00047B74 File Offset: 0x00045D74
		public static void Fail(string message, string detailMessage)
		{
			if (TraceInternal.UseGlobalLock)
			{
				object obj = TraceInternal.critSec;
				lock (obj)
				{
					using (IEnumerator enumerator = TraceInternal.Listeners.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							object obj2 = enumerator.Current;
							TraceListener traceListener = (TraceListener)obj2;
							traceListener.Fail(message, detailMessage);
							if (TraceInternal.AutoFlush)
							{
								traceListener.Flush();
							}
						}
						return;
					}
				}
			}
			foreach (object obj3 in TraceInternal.Listeners)
			{
				TraceListener traceListener2 = (TraceListener)obj3;
				if (!traceListener2.IsThreadSafe)
				{
					TraceListener obj4 = traceListener2;
					lock (obj4)
					{
						traceListener2.Fail(message, detailMessage);
						if (TraceInternal.AutoFlush)
						{
							traceListener2.Flush();
						}
						continue;
					}
				}
				traceListener2.Fail(message, detailMessage);
				if (TraceInternal.AutoFlush)
				{
					traceListener2.Flush();
				}
			}
		}

		// Token: 0x06001081 RID: 4225 RVA: 0x00047CB0 File Offset: 0x00045EB0
		private static void InitializeSettings()
		{
			if (!TraceInternal.settingsInitialized || (TraceInternal.defaultInitialized && DiagnosticsConfiguration.IsInitialized()))
			{
				object obj = TraceInternal.critSec;
				lock (obj)
				{
					if (!TraceInternal.settingsInitialized || (TraceInternal.defaultInitialized && DiagnosticsConfiguration.IsInitialized()))
					{
						TraceInternal.defaultInitialized = DiagnosticsConfiguration.IsInitializing();
						TraceInternal.SetIndentSize(DiagnosticsConfiguration.IndentSize);
						TraceInternal.autoFlush = DiagnosticsConfiguration.AutoFlush;
						TraceInternal.useGlobalLock = DiagnosticsConfiguration.UseGlobalLock;
						TraceInternal.settingsInitialized = true;
					}
				}
			}
		}

		// Token: 0x06001082 RID: 4226 RVA: 0x00047D54 File Offset: 0x00045F54
		internal static void Refresh()
		{
			object obj = TraceInternal.critSec;
			lock (obj)
			{
				TraceInternal.settingsInitialized = false;
				TraceInternal.listeners = null;
			}
			TraceInternal.InitializeSettings();
		}

		// Token: 0x06001083 RID: 4227 RVA: 0x00047DA4 File Offset: 0x00045FA4
		public static void TraceEvent(TraceEventType eventType, int id, string format, params object[] args)
		{
			TraceEventCache eventCache = new TraceEventCache();
			if (TraceInternal.UseGlobalLock)
			{
				object obj = TraceInternal.critSec;
				lock (obj)
				{
					if (args == null)
					{
						using (IEnumerator enumerator = TraceInternal.Listeners.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								object obj2 = enumerator.Current;
								TraceListener traceListener = (TraceListener)obj2;
								traceListener.TraceEvent(eventCache, TraceInternal.AppName, eventType, id, format);
								if (TraceInternal.AutoFlush)
								{
									traceListener.Flush();
								}
							}
							return;
						}
					}
					using (IEnumerator enumerator = TraceInternal.Listeners.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							object obj3 = enumerator.Current;
							TraceListener traceListener2 = (TraceListener)obj3;
							traceListener2.TraceEvent(eventCache, TraceInternal.AppName, eventType, id, format, args);
							if (TraceInternal.AutoFlush)
							{
								traceListener2.Flush();
							}
						}
						return;
					}
				}
			}
			if (args == null)
			{
				using (IEnumerator enumerator = TraceInternal.Listeners.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj4 = enumerator.Current;
						TraceListener traceListener3 = (TraceListener)obj4;
						if (!traceListener3.IsThreadSafe)
						{
							TraceListener obj5 = traceListener3;
							lock (obj5)
							{
								traceListener3.TraceEvent(eventCache, TraceInternal.AppName, eventType, id, format);
								if (TraceInternal.AutoFlush)
								{
									traceListener3.Flush();
								}
								continue;
							}
						}
						traceListener3.TraceEvent(eventCache, TraceInternal.AppName, eventType, id, format);
						if (TraceInternal.AutoFlush)
						{
							traceListener3.Flush();
						}
					}
					return;
				}
			}
			foreach (object obj6 in TraceInternal.Listeners)
			{
				TraceListener traceListener4 = (TraceListener)obj6;
				if (!traceListener4.IsThreadSafe)
				{
					TraceListener obj5 = traceListener4;
					lock (obj5)
					{
						traceListener4.TraceEvent(eventCache, TraceInternal.AppName, eventType, id, format, args);
						if (TraceInternal.AutoFlush)
						{
							traceListener4.Flush();
						}
						continue;
					}
				}
				traceListener4.TraceEvent(eventCache, TraceInternal.AppName, eventType, id, format, args);
				if (TraceInternal.AutoFlush)
				{
					traceListener4.Flush();
				}
			}
		}

		// Token: 0x06001084 RID: 4228 RVA: 0x00048028 File Offset: 0x00046228
		public static void Write(string message)
		{
			if (TraceInternal.UseGlobalLock)
			{
				object obj = TraceInternal.critSec;
				lock (obj)
				{
					using (IEnumerator enumerator = TraceInternal.Listeners.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							object obj2 = enumerator.Current;
							TraceListener traceListener = (TraceListener)obj2;
							traceListener.Write(message);
							if (TraceInternal.AutoFlush)
							{
								traceListener.Flush();
							}
						}
						return;
					}
				}
			}
			foreach (object obj3 in TraceInternal.Listeners)
			{
				TraceListener traceListener2 = (TraceListener)obj3;
				if (!traceListener2.IsThreadSafe)
				{
					TraceListener obj4 = traceListener2;
					lock (obj4)
					{
						traceListener2.Write(message);
						if (TraceInternal.AutoFlush)
						{
							traceListener2.Flush();
						}
						continue;
					}
				}
				traceListener2.Write(message);
				if (TraceInternal.AutoFlush)
				{
					traceListener2.Flush();
				}
			}
		}

		// Token: 0x06001085 RID: 4229 RVA: 0x00048164 File Offset: 0x00046364
		public static void Write(object value)
		{
			if (TraceInternal.UseGlobalLock)
			{
				object obj = TraceInternal.critSec;
				lock (obj)
				{
					using (IEnumerator enumerator = TraceInternal.Listeners.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							object obj2 = enumerator.Current;
							TraceListener traceListener = (TraceListener)obj2;
							traceListener.Write(value);
							if (TraceInternal.AutoFlush)
							{
								traceListener.Flush();
							}
						}
						return;
					}
				}
			}
			foreach (object obj3 in TraceInternal.Listeners)
			{
				TraceListener traceListener2 = (TraceListener)obj3;
				if (!traceListener2.IsThreadSafe)
				{
					TraceListener obj4 = traceListener2;
					lock (obj4)
					{
						traceListener2.Write(value);
						if (TraceInternal.AutoFlush)
						{
							traceListener2.Flush();
						}
						continue;
					}
				}
				traceListener2.Write(value);
				if (TraceInternal.AutoFlush)
				{
					traceListener2.Flush();
				}
			}
		}

		// Token: 0x06001086 RID: 4230 RVA: 0x000482A0 File Offset: 0x000464A0
		public static void Write(string message, string category)
		{
			if (TraceInternal.UseGlobalLock)
			{
				object obj = TraceInternal.critSec;
				lock (obj)
				{
					using (IEnumerator enumerator = TraceInternal.Listeners.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							object obj2 = enumerator.Current;
							TraceListener traceListener = (TraceListener)obj2;
							traceListener.Write(message, category);
							if (TraceInternal.AutoFlush)
							{
								traceListener.Flush();
							}
						}
						return;
					}
				}
			}
			foreach (object obj3 in TraceInternal.Listeners)
			{
				TraceListener traceListener2 = (TraceListener)obj3;
				if (!traceListener2.IsThreadSafe)
				{
					TraceListener obj4 = traceListener2;
					lock (obj4)
					{
						traceListener2.Write(message, category);
						if (TraceInternal.AutoFlush)
						{
							traceListener2.Flush();
						}
						continue;
					}
				}
				traceListener2.Write(message, category);
				if (TraceInternal.AutoFlush)
				{
					traceListener2.Flush();
				}
			}
		}

		// Token: 0x06001087 RID: 4231 RVA: 0x000483DC File Offset: 0x000465DC
		public static void Write(object value, string category)
		{
			if (TraceInternal.UseGlobalLock)
			{
				object obj = TraceInternal.critSec;
				lock (obj)
				{
					using (IEnumerator enumerator = TraceInternal.Listeners.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							object obj2 = enumerator.Current;
							TraceListener traceListener = (TraceListener)obj2;
							traceListener.Write(value, category);
							if (TraceInternal.AutoFlush)
							{
								traceListener.Flush();
							}
						}
						return;
					}
				}
			}
			foreach (object obj3 in TraceInternal.Listeners)
			{
				TraceListener traceListener2 = (TraceListener)obj3;
				if (!traceListener2.IsThreadSafe)
				{
					TraceListener obj4 = traceListener2;
					lock (obj4)
					{
						traceListener2.Write(value, category);
						if (TraceInternal.AutoFlush)
						{
							traceListener2.Flush();
						}
						continue;
					}
				}
				traceListener2.Write(value, category);
				if (TraceInternal.AutoFlush)
				{
					traceListener2.Flush();
				}
			}
		}

		// Token: 0x06001088 RID: 4232 RVA: 0x00048518 File Offset: 0x00046718
		public static void WriteLine(string message)
		{
			if (TraceInternal.UseGlobalLock)
			{
				object obj = TraceInternal.critSec;
				lock (obj)
				{
					using (IEnumerator enumerator = TraceInternal.Listeners.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							object obj2 = enumerator.Current;
							TraceListener traceListener = (TraceListener)obj2;
							traceListener.WriteLine(message);
							if (TraceInternal.AutoFlush)
							{
								traceListener.Flush();
							}
						}
						return;
					}
				}
			}
			foreach (object obj3 in TraceInternal.Listeners)
			{
				TraceListener traceListener2 = (TraceListener)obj3;
				if (!traceListener2.IsThreadSafe)
				{
					TraceListener obj4 = traceListener2;
					lock (obj4)
					{
						traceListener2.WriteLine(message);
						if (TraceInternal.AutoFlush)
						{
							traceListener2.Flush();
						}
						continue;
					}
				}
				traceListener2.WriteLine(message);
				if (TraceInternal.AutoFlush)
				{
					traceListener2.Flush();
				}
			}
		}

		// Token: 0x06001089 RID: 4233 RVA: 0x00048654 File Offset: 0x00046854
		public static void WriteLine(object value)
		{
			if (TraceInternal.UseGlobalLock)
			{
				object obj = TraceInternal.critSec;
				lock (obj)
				{
					using (IEnumerator enumerator = TraceInternal.Listeners.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							object obj2 = enumerator.Current;
							TraceListener traceListener = (TraceListener)obj2;
							traceListener.WriteLine(value);
							if (TraceInternal.AutoFlush)
							{
								traceListener.Flush();
							}
						}
						return;
					}
				}
			}
			foreach (object obj3 in TraceInternal.Listeners)
			{
				TraceListener traceListener2 = (TraceListener)obj3;
				if (!traceListener2.IsThreadSafe)
				{
					TraceListener obj4 = traceListener2;
					lock (obj4)
					{
						traceListener2.WriteLine(value);
						if (TraceInternal.AutoFlush)
						{
							traceListener2.Flush();
						}
						continue;
					}
				}
				traceListener2.WriteLine(value);
				if (TraceInternal.AutoFlush)
				{
					traceListener2.Flush();
				}
			}
		}

		// Token: 0x0600108A RID: 4234 RVA: 0x00048790 File Offset: 0x00046990
		public static void WriteLine(string message, string category)
		{
			if (TraceInternal.UseGlobalLock)
			{
				object obj = TraceInternal.critSec;
				lock (obj)
				{
					using (IEnumerator enumerator = TraceInternal.Listeners.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							object obj2 = enumerator.Current;
							TraceListener traceListener = (TraceListener)obj2;
							traceListener.WriteLine(message, category);
							if (TraceInternal.AutoFlush)
							{
								traceListener.Flush();
							}
						}
						return;
					}
				}
			}
			foreach (object obj3 in TraceInternal.Listeners)
			{
				TraceListener traceListener2 = (TraceListener)obj3;
				if (!traceListener2.IsThreadSafe)
				{
					TraceListener obj4 = traceListener2;
					lock (obj4)
					{
						traceListener2.WriteLine(message, category);
						if (TraceInternal.AutoFlush)
						{
							traceListener2.Flush();
						}
						continue;
					}
				}
				traceListener2.WriteLine(message, category);
				if (TraceInternal.AutoFlush)
				{
					traceListener2.Flush();
				}
			}
		}

		// Token: 0x0600108B RID: 4235 RVA: 0x000488CC File Offset: 0x00046ACC
		public static void WriteLine(object value, string category)
		{
			if (TraceInternal.UseGlobalLock)
			{
				object obj = TraceInternal.critSec;
				lock (obj)
				{
					using (IEnumerator enumerator = TraceInternal.Listeners.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							object obj2 = enumerator.Current;
							TraceListener traceListener = (TraceListener)obj2;
							traceListener.WriteLine(value, category);
							if (TraceInternal.AutoFlush)
							{
								traceListener.Flush();
							}
						}
						return;
					}
				}
			}
			foreach (object obj3 in TraceInternal.Listeners)
			{
				TraceListener traceListener2 = (TraceListener)obj3;
				if (!traceListener2.IsThreadSafe)
				{
					TraceListener obj4 = traceListener2;
					lock (obj4)
					{
						traceListener2.WriteLine(value, category);
						if (TraceInternal.AutoFlush)
						{
							traceListener2.Flush();
						}
						continue;
					}
				}
				traceListener2.WriteLine(value, category);
				if (TraceInternal.AutoFlush)
				{
					traceListener2.Flush();
				}
			}
		}

		// Token: 0x0600108C RID: 4236 RVA: 0x00048A08 File Offset: 0x00046C08
		public static void WriteIf(bool condition, string message)
		{
			if (condition)
			{
				TraceInternal.Write(message);
			}
		}

		// Token: 0x0600108D RID: 4237 RVA: 0x00048A13 File Offset: 0x00046C13
		public static void WriteIf(bool condition, object value)
		{
			if (condition)
			{
				TraceInternal.Write(value);
			}
		}

		// Token: 0x0600108E RID: 4238 RVA: 0x00048A1E File Offset: 0x00046C1E
		public static void WriteIf(bool condition, string message, string category)
		{
			if (condition)
			{
				TraceInternal.Write(message, category);
			}
		}

		// Token: 0x0600108F RID: 4239 RVA: 0x00048A2A File Offset: 0x00046C2A
		public static void WriteIf(bool condition, object value, string category)
		{
			if (condition)
			{
				TraceInternal.Write(value, category);
			}
		}

		// Token: 0x06001090 RID: 4240 RVA: 0x00048A36 File Offset: 0x00046C36
		public static void WriteLineIf(bool condition, string message)
		{
			if (condition)
			{
				TraceInternal.WriteLine(message);
			}
		}

		// Token: 0x06001091 RID: 4241 RVA: 0x00048A41 File Offset: 0x00046C41
		public static void WriteLineIf(bool condition, object value)
		{
			if (condition)
			{
				TraceInternal.WriteLine(value);
			}
		}

		// Token: 0x06001092 RID: 4242 RVA: 0x00048A4C File Offset: 0x00046C4C
		public static void WriteLineIf(bool condition, string message, string category)
		{
			if (condition)
			{
				TraceInternal.WriteLine(message, category);
			}
		}

		// Token: 0x06001093 RID: 4243 RVA: 0x00048A58 File Offset: 0x00046C58
		public static void WriteLineIf(bool condition, object value, string category)
		{
			if (condition)
			{
				TraceInternal.WriteLine(value, category);
			}
		}

		// Token: 0x06001094 RID: 4244 RVA: 0x00048A64 File Offset: 0x00046C64
		// Note: this type is marked as 'beforefieldinit'.
		static TraceInternal()
		{
		}

		// Token: 0x040009E4 RID: 2532
		private static volatile string appName = null;

		// Token: 0x040009E5 RID: 2533
		private static volatile TraceListenerCollection listeners;

		// Token: 0x040009E6 RID: 2534
		private static volatile bool autoFlush;

		// Token: 0x040009E7 RID: 2535
		private static volatile bool useGlobalLock;

		// Token: 0x040009E8 RID: 2536
		[ThreadStatic]
		private static int indentLevel;

		// Token: 0x040009E9 RID: 2537
		private static volatile int indentSize;

		// Token: 0x040009EA RID: 2538
		private static volatile bool settingsInitialized;

		// Token: 0x040009EB RID: 2539
		private static volatile bool defaultInitialized;

		// Token: 0x040009EC RID: 2540
		internal static readonly object critSec = new object();
	}
}
