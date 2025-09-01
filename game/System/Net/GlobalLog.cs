using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.ConstrainedExecution;

namespace System.Net
{
	// Token: 0x02000630 RID: 1584
	internal static class GlobalLog
	{
		// Token: 0x06003203 RID: 12803 RVA: 0x000AD639 File Offset: 0x000AB839
		[ReliabilityContract(Consistency.MayCorruptAppDomain, Cer.None)]
		private static BaseLoggingObject LoggingInitialize()
		{
			return new BaseLoggingObject();
		}

		// Token: 0x17000A0D RID: 2573
		// (get) Token: 0x06003204 RID: 12804 RVA: 0x00003062 File Offset: 0x00001262
		internal static ThreadKinds CurrentThreadKind
		{
			get
			{
				return ThreadKinds.Unknown;
			}
		}

		// Token: 0x06003205 RID: 12805 RVA: 0x00003917 File Offset: 0x00001B17
		[ReliabilityContract(Consistency.MayCorruptAppDomain, Cer.None)]
		[Conditional("DEBUG")]
		internal static void SetThreadSource(ThreadKinds source)
		{
		}

		// Token: 0x06003206 RID: 12806 RVA: 0x00003917 File Offset: 0x00001B17
		[Conditional("DEBUG")]
		[ReliabilityContract(Consistency.MayCorruptAppDomain, Cer.None)]
		internal static void ThreadContract(ThreadKinds kind, string errorMsg)
		{
		}

		// Token: 0x06003207 RID: 12807 RVA: 0x000AD640 File Offset: 0x000AB840
		[Conditional("DEBUG")]
		[ReliabilityContract(Consistency.MayCorruptAppDomain, Cer.None)]
		internal static void ThreadContract(ThreadKinds kind, ThreadKinds allowedSources, string errorMsg)
		{
			if ((kind & ThreadKinds.SourceMask) != ThreadKinds.Unknown || (allowedSources & ThreadKinds.SourceMask) != allowedSources)
			{
				throw new InternalException();
			}
			ThreadKinds currentThreadKind = GlobalLog.CurrentThreadKind;
		}

		// Token: 0x06003208 RID: 12808 RVA: 0x00003917 File Offset: 0x00001B17
		[Conditional("TRAVE")]
		public static void AddToArray(string msg)
		{
		}

		// Token: 0x06003209 RID: 12809 RVA: 0x00003917 File Offset: 0x00001B17
		[Conditional("TRAVE")]
		public static void Ignore(object msg)
		{
		}

		// Token: 0x0600320A RID: 12810 RVA: 0x00003917 File Offset: 0x00001B17
		[ReliabilityContract(Consistency.MayCorruptAppDomain, Cer.None)]
		[Conditional("TRAVE")]
		public static void Print(string msg)
		{
		}

		// Token: 0x0600320B RID: 12811 RVA: 0x00003917 File Offset: 0x00001B17
		[Conditional("TRAVE")]
		public static void PrintHex(string msg, object value)
		{
		}

		// Token: 0x0600320C RID: 12812 RVA: 0x00003917 File Offset: 0x00001B17
		[Conditional("TRAVE")]
		public static void Enter(string func)
		{
		}

		// Token: 0x0600320D RID: 12813 RVA: 0x00003917 File Offset: 0x00001B17
		[Conditional("TRAVE")]
		public static void Enter(string func, string parms)
		{
		}

		// Token: 0x0600320E RID: 12814 RVA: 0x000AD664 File Offset: 0x000AB864
		[ReliabilityContract(Consistency.MayCorruptAppDomain, Cer.None)]
		[Conditional("_FORCE_ASSERTS")]
		[Conditional("DEBUG")]
		public static void Assert(bool condition, string messageFormat, params object[] data)
		{
			if (!condition)
			{
				string text = string.Format(CultureInfo.InvariantCulture, messageFormat, data);
				int num = text.IndexOf('|');
				if (num != -1)
				{
					int length = text.Length;
				}
			}
		}

		// Token: 0x0600320F RID: 12815 RVA: 0x00003917 File Offset: 0x00001B17
		[ReliabilityContract(Consistency.MayCorruptAppDomain, Cer.None)]
		[Conditional("DEBUG")]
		[Conditional("_FORCE_ASSERTS")]
		public static void Assert(string message)
		{
		}

		// Token: 0x06003210 RID: 12816 RVA: 0x000AD698 File Offset: 0x000AB898
		[Conditional("_FORCE_ASSERTS")]
		[Conditional("DEBUG")]
		[ReliabilityContract(Consistency.MayCorruptAppDomain, Cer.None)]
		public static void Assert(string message, string detailMessage)
		{
			try
			{
				GlobalLog.Logobject.DumpArray(false);
			}
			finally
			{
				Debugger.Break();
			}
		}

		// Token: 0x06003211 RID: 12817 RVA: 0x00003917 File Offset: 0x00001B17
		[Conditional("TRAVE")]
		public static void LeaveException(string func, Exception exception)
		{
		}

		// Token: 0x06003212 RID: 12818 RVA: 0x00003917 File Offset: 0x00001B17
		[Conditional("TRAVE")]
		public static void Leave(string func)
		{
		}

		// Token: 0x06003213 RID: 12819 RVA: 0x00003917 File Offset: 0x00001B17
		[Conditional("TRAVE")]
		public static void Leave(string func, string result)
		{
		}

		// Token: 0x06003214 RID: 12820 RVA: 0x00003917 File Offset: 0x00001B17
		[Conditional("TRAVE")]
		public static void Leave(string func, int returnval)
		{
		}

		// Token: 0x06003215 RID: 12821 RVA: 0x00003917 File Offset: 0x00001B17
		[Conditional("TRAVE")]
		public static void Leave(string func, bool returnval)
		{
		}

		// Token: 0x06003216 RID: 12822 RVA: 0x00003917 File Offset: 0x00001B17
		[Conditional("TRAVE")]
		public static void DumpArray()
		{
		}

		// Token: 0x06003217 RID: 12823 RVA: 0x00003917 File Offset: 0x00001B17
		[Conditional("TRAVE")]
		public static void Dump(byte[] buffer)
		{
		}

		// Token: 0x06003218 RID: 12824 RVA: 0x00003917 File Offset: 0x00001B17
		[Conditional("TRAVE")]
		public static void Dump(byte[] buffer, int length)
		{
		}

		// Token: 0x06003219 RID: 12825 RVA: 0x00003917 File Offset: 0x00001B17
		[Conditional("TRAVE")]
		public static void Dump(byte[] buffer, int offset, int length)
		{
		}

		// Token: 0x0600321A RID: 12826 RVA: 0x00003917 File Offset: 0x00001B17
		[Conditional("TRAVE")]
		public static void Dump(IntPtr buffer, int offset, int length)
		{
		}

		// Token: 0x0600321B RID: 12827 RVA: 0x000AD6C8 File Offset: 0x000AB8C8
		// Note: this type is marked as 'beforefieldinit'.
		static GlobalLog()
		{
		}

		// Token: 0x04001D4B RID: 7499
		private static BaseLoggingObject Logobject = GlobalLog.LoggingInitialize();
	}
}
