using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Interop;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Security.Principal;
using System.Text;

namespace System.Runtime.Diagnostics
{
	// Token: 0x0200004A RID: 74
	internal sealed class EventLogger
	{
		// Token: 0x060002C2 RID: 706 RVA: 0x0000F3C9 File Offset: 0x0000D5C9
		private EventLogger()
		{
			this.isInPartialTrust = this.IsInPartialTrust();
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x0000F3E0 File Offset: 0x0000D5E0
		[Obsolete("For System.Runtime.dll use only. Call FxTrace.EventLog instead")]
		public EventLogger(string eventLogSourceName, DiagnosticTraceBase diagnosticTrace)
		{
			try
			{
				this.diagnosticTrace = diagnosticTrace;
				if (EventLogger.canLogEvent)
				{
					this.SafeSetLogSourceName(eventLogSourceName);
				}
			}
			catch (SecurityException)
			{
				EventLogger.canLogEvent = false;
			}
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0000F424 File Offset: 0x0000D624
		[SecurityCritical]
		public static EventLogger UnsafeCreateEventLogger(string eventLogSourceName, DiagnosticTraceBase diagnosticTrace)
		{
			EventLogger eventLogger = new EventLogger();
			eventLogger.SetLogSourceName(eventLogSourceName, diagnosticTrace);
			return eventLogger;
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0000F434 File Offset: 0x0000D634
		[SecurityCritical]
		public void UnsafeLogEvent(TraceEventType type, ushort eventLogCategory, uint eventId, bool shouldTrace, params string[] values)
		{
			if (EventLogger.logCountForPT < 5)
			{
				try
				{
					int num = 0;
					string[] array = new string[values.Length + 2];
					for (int i = 0; i < values.Length; i++)
					{
						string text = values[i];
						if (!string.IsNullOrEmpty(text))
						{
							text = EventLogger.NormalizeEventLogParameter(text);
						}
						else
						{
							text = string.Empty;
						}
						array[i] = text;
						num += text.Length + 1;
					}
					string text2 = EventLogger.NormalizeEventLogParameter(this.UnsafeGetProcessName());
					array[array.Length - 2] = text2;
					num += text2.Length + 1;
					string text3 = this.UnsafeGetProcessId().ToString(CultureInfo.InvariantCulture);
					array[array.Length - 1] = text3;
					num += text3.Length + 1;
					if (num > 25600)
					{
						int num2 = 25600 / array.Length - 1;
						for (int j = 0; j < array.Length; j++)
						{
							if (array[j].Length > num2)
							{
								array[j] = array[j].Substring(0, num2);
							}
						}
					}
					SecurityIdentifier user = WindowsIdentity.GetCurrent().User;
					byte[] array2 = new byte[user.BinaryLength];
					user.GetBinaryForm(array2, 0);
					IntPtr[] array3 = new IntPtr[array.Length];
					GCHandle stringsRootHandle = default(GCHandle);
					GCHandle[] array4 = null;
					try
					{
						stringsRootHandle = GCHandle.Alloc(array3, GCHandleType.Pinned);
						array4 = new GCHandle[array.Length];
						for (int k = 0; k < array.Length; k++)
						{
							array4[k] = GCHandle.Alloc(array[k], GCHandleType.Pinned);
							array3[k] = array4[k].AddrOfPinnedObject();
						}
						this.UnsafeWriteEventLog(type, eventLogCategory, eventId, array, array2, stringsRootHandle);
					}
					finally
					{
						if (stringsRootHandle.AddrOfPinnedObject() != IntPtr.Zero)
						{
							stringsRootHandle.Free();
						}
						if (array4 != null)
						{
							foreach (GCHandle gchandle in array4)
							{
								gchandle.Free();
							}
						}
					}
					if (shouldTrace && this.diagnosticTrace != null && this.diagnosticTrace.IsEnabled())
					{
						Dictionary<string, string> dictionary = new Dictionary<string, string>(array.Length + 4);
						dictionary["CategoryID.Name"] = "EventLogCategory";
						dictionary["CategoryID.Value"] = eventLogCategory.ToString(CultureInfo.InvariantCulture);
						dictionary["InstanceID.Name"] = "EventId";
						dictionary["InstanceID.Value"] = eventId.ToString(CultureInfo.InvariantCulture);
						for (int m = 0; m < values.Length; m++)
						{
							dictionary.Add("Value" + m.ToString(CultureInfo.InvariantCulture), (values[m] == null) ? string.Empty : DiagnosticTraceBase.XmlEncode(values[m]));
						}
						this.diagnosticTrace.TraceEventLogEvent(type, new DictionaryTraceRecord(dictionary));
					}
				}
				catch (Exception exception)
				{
					if (Fx.IsFatal(exception))
					{
						throw;
					}
				}
				if (this.isInPartialTrust)
				{
					EventLogger.logCountForPT++;
				}
			}
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0000F734 File Offset: 0x0000D934
		public void LogEvent(TraceEventType type, ushort eventLogCategory, uint eventId, bool shouldTrace, params string[] values)
		{
			if (EventLogger.canLogEvent)
			{
				try
				{
					this.SafeLogEvent(type, eventLogCategory, eventId, shouldTrace, values);
				}
				catch (SecurityException exception)
				{
					EventLogger.canLogEvent = false;
					if (shouldTrace)
					{
						Fx.Exception.TraceHandledException(exception, TraceEventType.Information);
					}
				}
			}
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0000F780 File Offset: 0x0000D980
		public void LogEvent(TraceEventType type, ushort eventLogCategory, uint eventId, params string[] values)
		{
			this.LogEvent(type, eventLogCategory, eventId, true, values);
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0000F790 File Offset: 0x0000D990
		private static EventLogEntryType EventLogEntryTypeFromEventType(TraceEventType type)
		{
			EventLogEntryType result = EventLogEntryType.Information;
			if (type - TraceEventType.Critical > 1)
			{
				if (type == TraceEventType.Warning)
				{
					result = EventLogEntryType.Warning;
				}
			}
			else
			{
				result = EventLogEntryType.Error;
			}
			return result;
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0000F7B2 File Offset: 0x0000D9B2
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
		private void SafeLogEvent(TraceEventType type, ushort eventLogCategory, uint eventId, bool shouldTrace, params string[] values)
		{
			this.UnsafeLogEvent(type, eventLogCategory, eventId, shouldTrace, values);
		}

		// Token: 0x060002CA RID: 714 RVA: 0x0000F7C1 File Offset: 0x0000D9C1
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
		private void SafeSetLogSourceName(string eventLogSourceName)
		{
			this.eventLogSourceName = eventLogSourceName;
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0000F7CA File Offset: 0x0000D9CA
		[SecurityCritical]
		private void SetLogSourceName(string eventLogSourceName, DiagnosticTraceBase diagnosticTrace)
		{
			this.eventLogSourceName = eventLogSourceName;
			this.diagnosticTrace = diagnosticTrace;
		}

		// Token: 0x060002CC RID: 716 RVA: 0x0000F7DC File Offset: 0x0000D9DC
		[SecuritySafeCritical]
		private bool IsInPartialTrust()
		{
			bool result = false;
			try
			{
				using (Process currentProcess = Process.GetCurrentProcess())
				{
					result = string.IsNullOrEmpty(currentProcess.ProcessName);
				}
			}
			catch (SecurityException)
			{
				result = true;
			}
			return result;
		}

		// Token: 0x060002CD RID: 717 RVA: 0x0000F830 File Offset: 0x0000DA30
		[SecurityCritical]
		[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
		private void UnsafeWriteEventLog(TraceEventType type, ushort eventLogCategory, uint eventId, string[] logValues, byte[] sidBA, GCHandle stringsRootHandle)
		{
			using (SafeEventLogWriteHandle safeEventLogWriteHandle = SafeEventLogWriteHandle.RegisterEventSource(null, this.eventLogSourceName))
			{
				if (safeEventLogWriteHandle != null)
				{
					HandleRef strings = new HandleRef(safeEventLogWriteHandle, stringsRootHandle.AddrOfPinnedObject());
					UnsafeNativeMethods.ReportEvent(safeEventLogWriteHandle, (ushort)EventLogger.EventLogEntryTypeFromEventType(type), eventLogCategory, eventId, sidBA, (ushort)logValues.Length, 0U, strings, null);
				}
			}
		}

		// Token: 0x060002CE RID: 718 RVA: 0x0000F894 File Offset: 0x0000DA94
		[SecurityCritical]
		[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		private string UnsafeGetProcessName()
		{
			string result = null;
			using (Process currentProcess = Process.GetCurrentProcess())
			{
				result = currentProcess.ProcessName;
			}
			return result;
		}

		// Token: 0x060002CF RID: 719 RVA: 0x0000F8D0 File Offset: 0x0000DAD0
		[SecurityCritical]
		[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		private int UnsafeGetProcessId()
		{
			int result = -1;
			using (Process currentProcess = Process.GetCurrentProcess())
			{
				result = currentProcess.Id;
			}
			return result;
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0000F90C File Offset: 0x0000DB0C
		internal static string NormalizeEventLogParameter(string eventLogParameter)
		{
			if (eventLogParameter.IndexOf('%') < 0)
			{
				return eventLogParameter;
			}
			StringBuilder stringBuilder = null;
			int length = eventLogParameter.Length;
			for (int i = 0; i < length; i++)
			{
				char c = eventLogParameter[i];
				if (c != '%')
				{
					if (stringBuilder != null)
					{
						stringBuilder.Append(c);
					}
				}
				else if (i + 1 >= length)
				{
					if (stringBuilder != null)
					{
						stringBuilder.Append(c);
					}
				}
				else if (eventLogParameter[i + 1] < '0' || eventLogParameter[i + 1] > '9')
				{
					if (stringBuilder != null)
					{
						stringBuilder.Append(c);
					}
				}
				else
				{
					if (stringBuilder == null)
					{
						stringBuilder = new StringBuilder(length + 2);
						for (int j = 0; j < i; j++)
						{
							stringBuilder.Append(eventLogParameter[j]);
						}
					}
					stringBuilder.Append(c);
					stringBuilder.Append(' ');
				}
			}
			if (stringBuilder == null)
			{
				return eventLogParameter;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0000F9DF File Offset: 0x0000DBDF
		// Note: this type is marked as 'beforefieldinit'.
		static EventLogger()
		{
		}

		// Token: 0x040001DC RID: 476
		private const int MaxEventLogsInPT = 5;

		// Token: 0x040001DD RID: 477
		[SecurityCritical]
		private static int logCountForPT;

		// Token: 0x040001DE RID: 478
		private static bool canLogEvent = true;

		// Token: 0x040001DF RID: 479
		private DiagnosticTraceBase diagnosticTrace;

		// Token: 0x040001E0 RID: 480
		[SecurityCritical]
		private string eventLogSourceName;

		// Token: 0x040001E1 RID: 481
		private bool isInPartialTrust;
	}
}
