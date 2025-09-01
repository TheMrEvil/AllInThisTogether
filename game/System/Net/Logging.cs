using System;
using System.Diagnostics;

namespace System.Net
{
	// Token: 0x02000666 RID: 1638
	internal static class Logging
	{
		// Token: 0x17000A6E RID: 2670
		// (get) Token: 0x060033AD RID: 13229 RVA: 0x00003062 File Offset: 0x00001262
		internal static bool On
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000A6F RID: 2671
		// (get) Token: 0x060033AE RID: 13230 RVA: 0x00002F6A File Offset: 0x0000116A
		internal static TraceSource Web
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000A70 RID: 2672
		// (get) Token: 0x060033AF RID: 13231 RVA: 0x00002F6A File Offset: 0x0000116A
		internal static TraceSource HttpListener
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000A71 RID: 2673
		// (get) Token: 0x060033B0 RID: 13232 RVA: 0x00002F6A File Offset: 0x0000116A
		internal static TraceSource Sockets
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060033B1 RID: 13233 RVA: 0x00003917 File Offset: 0x00001B17
		[Conditional("TRACE")]
		internal static void Enter(TraceSource traceSource, object obj, string method, object paramObject)
		{
		}

		// Token: 0x060033B2 RID: 13234 RVA: 0x00003917 File Offset: 0x00001B17
		[Conditional("TRACE")]
		internal static void Enter(TraceSource traceSource, string msg)
		{
		}

		// Token: 0x060033B3 RID: 13235 RVA: 0x00003917 File Offset: 0x00001B17
		[Conditional("TRACE")]
		internal static void Enter(TraceSource traceSource, string msg, string parameters)
		{
		}

		// Token: 0x060033B4 RID: 13236 RVA: 0x00003917 File Offset: 0x00001B17
		[Conditional("TRACE")]
		internal static void Exception(TraceSource traceSource, object obj, string method, Exception e)
		{
		}

		// Token: 0x060033B5 RID: 13237 RVA: 0x00003917 File Offset: 0x00001B17
		[Conditional("TRACE")]
		internal static void Exit(TraceSource traceSource, object obj, string method, object retObject)
		{
		}

		// Token: 0x060033B6 RID: 13238 RVA: 0x00003917 File Offset: 0x00001B17
		[Conditional("TRACE")]
		internal static void Exit(TraceSource traceSource, string msg)
		{
		}

		// Token: 0x060033B7 RID: 13239 RVA: 0x00003917 File Offset: 0x00001B17
		[Conditional("TRACE")]
		internal static void Exit(TraceSource traceSource, string msg, string parameters)
		{
		}

		// Token: 0x060033B8 RID: 13240 RVA: 0x00003917 File Offset: 0x00001B17
		[Conditional("TRACE")]
		internal static void PrintInfo(TraceSource traceSource, object obj, string method, string msg)
		{
		}

		// Token: 0x060033B9 RID: 13241 RVA: 0x00003917 File Offset: 0x00001B17
		[Conditional("TRACE")]
		internal static void PrintInfo(TraceSource traceSource, object obj, string msg)
		{
		}

		// Token: 0x060033BA RID: 13242 RVA: 0x00003917 File Offset: 0x00001B17
		[Conditional("TRACE")]
		internal static void PrintInfo(TraceSource traceSource, string msg)
		{
		}

		// Token: 0x060033BB RID: 13243 RVA: 0x00003917 File Offset: 0x00001B17
		[Conditional("TRACE")]
		internal static void PrintWarning(TraceSource traceSource, object obj, string method, string msg)
		{
		}

		// Token: 0x060033BC RID: 13244 RVA: 0x00003917 File Offset: 0x00001B17
		[Conditional("TRACE")]
		internal static void PrintWarning(TraceSource traceSource, string msg)
		{
		}

		// Token: 0x060033BD RID: 13245 RVA: 0x00003917 File Offset: 0x00001B17
		[Conditional("TRACE")]
		internal static void PrintError(TraceSource traceSource, string msg)
		{
		}
	}
}
