using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000107 RID: 263
	[NativeHeader("Runtime/Export/CrashReport/CrashReport.bindings.h")]
	public sealed class CrashReport
	{
		// Token: 0x06000621 RID: 1569 RVA: 0x000084CC File Offset: 0x000066CC
		private static int Compare(CrashReport c1, CrashReport c2)
		{
			long ticks = c1.time.Ticks;
			long ticks2 = c2.time.Ticks;
			bool flag = ticks > ticks2;
			int result;
			if (flag)
			{
				result = 1;
			}
			else
			{
				bool flag2 = ticks < ticks2;
				if (flag2)
				{
					result = -1;
				}
				else
				{
					result = 0;
				}
			}
			return result;
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x0000851C File Offset: 0x0000671C
		private static void PopulateReports()
		{
			object obj = CrashReport.reportsLock;
			lock (obj)
			{
				bool flag = CrashReport.internalReports != null;
				if (!flag)
				{
					string[] reports = CrashReport.GetReports();
					CrashReport.internalReports = new List<CrashReport>(reports.Length);
					foreach (string text in reports)
					{
						double value;
						string reportData = CrashReport.GetReportData(text, out value);
						DateTime dateTime = new DateTime(1970, 1, 1).AddSeconds(value);
						CrashReport.internalReports.Add(new CrashReport(text, dateTime, reportData));
					}
					CrashReport.internalReports.Sort(new Comparison<CrashReport>(CrashReport.Compare));
				}
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000623 RID: 1571 RVA: 0x000085E8 File Offset: 0x000067E8
		public static CrashReport[] reports
		{
			get
			{
				CrashReport.PopulateReports();
				object obj = CrashReport.reportsLock;
				CrashReport[] result;
				lock (obj)
				{
					result = CrashReport.internalReports.ToArray();
				}
				return result;
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000624 RID: 1572 RVA: 0x00008630 File Offset: 0x00006830
		public static CrashReport lastReport
		{
			get
			{
				CrashReport.PopulateReports();
				object obj = CrashReport.reportsLock;
				lock (obj)
				{
					bool flag = CrashReport.internalReports.Count > 0;
					if (flag)
					{
						return CrashReport.internalReports[CrashReport.internalReports.Count - 1];
					}
				}
				return null;
			}
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x000086A0 File Offset: 0x000068A0
		public static void RemoveAll()
		{
			foreach (CrashReport crashReport in CrashReport.reports)
			{
				crashReport.Remove();
			}
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x000086CE File Offset: 0x000068CE
		private CrashReport(string id, DateTime time, string text)
		{
			this.id = id;
			this.time = time;
			this.text = text;
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x000086F0 File Offset: 0x000068F0
		public void Remove()
		{
			bool flag = CrashReport.RemoveReport(this.id);
			if (flag)
			{
				object obj = CrashReport.reportsLock;
				lock (obj)
				{
					CrashReport.internalReports.Remove(this);
				}
			}
		}

		// Token: 0x06000628 RID: 1576
		[FreeFunction(Name = "CrashReport_Bindings::GetReports", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string[] GetReports();

		// Token: 0x06000629 RID: 1577
		[FreeFunction(Name = "CrashReport_Bindings::GetReportData", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string GetReportData(string id, out double secondsSinceUnixEpoch);

		// Token: 0x0600062A RID: 1578
		[FreeFunction(Name = "CrashReport_Bindings::RemoveReport", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool RemoveReport(string id);

		// Token: 0x0600062B RID: 1579 RVA: 0x00008744 File Offset: 0x00006944
		// Note: this type is marked as 'beforefieldinit'.
		static CrashReport()
		{
		}

		// Token: 0x04000379 RID: 889
		private static List<CrashReport> internalReports;

		// Token: 0x0400037A RID: 890
		private static object reportsLock = new object();

		// Token: 0x0400037B RID: 891
		private readonly string id;

		// Token: 0x0400037C RID: 892
		public readonly DateTime time;

		// Token: 0x0400037D RID: 893
		public readonly string text;
	}
}
