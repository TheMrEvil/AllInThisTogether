using System;
using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace System.ComponentModel.Composition.Diagnostics
{
	// Token: 0x020000FE RID: 254
	internal sealed class DebuggerTraceWriter : TraceWriter
	{
		// Token: 0x1700018D RID: 397
		// (get) Token: 0x060006B1 RID: 1713 RVA: 0x0000A969 File Offset: 0x00008B69
		public override bool CanWriteInformation
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x060006B2 RID: 1714 RVA: 0x00014C37 File Offset: 0x00012E37
		public override bool CanWriteWarning
		{
			get
			{
				return Debugger.IsLogging();
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x060006B3 RID: 1715 RVA: 0x00014C37 File Offset: 0x00012E37
		public override bool CanWriteError
		{
			get
			{
				return Debugger.IsLogging();
			}
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x00014C3E File Offset: 0x00012E3E
		public override void WriteInformation(CompositionTraceId traceId, string format, params object[] arguments)
		{
			DebuggerTraceWriter.WriteEvent(DebuggerTraceWriter.TraceEventType.Information, traceId, format, arguments);
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x00014C49 File Offset: 0x00012E49
		public override void WriteWarning(CompositionTraceId traceId, string format, params object[] arguments)
		{
			DebuggerTraceWriter.WriteEvent(DebuggerTraceWriter.TraceEventType.Warning, traceId, format, arguments);
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x00014C54 File Offset: 0x00012E54
		public override void WriteError(CompositionTraceId traceId, string format, params object[] arguments)
		{
			DebuggerTraceWriter.WriteEvent(DebuggerTraceWriter.TraceEventType.Error, traceId, format, arguments);
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x00014C60 File Offset: 0x00012E60
		private static void WriteEvent(DebuggerTraceWriter.TraceEventType eventType, CompositionTraceId traceId, string format, params object[] arguments)
		{
			if (!Debugger.IsLogging())
			{
				return;
			}
			string message = DebuggerTraceWriter.CreateLogMessage(eventType, traceId, format, arguments);
			Debugger.Log(0, null, message);
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x00014C88 File Offset: 0x00012E88
		internal static string CreateLogMessage(DebuggerTraceWriter.TraceEventType eventType, CompositionTraceId traceId, string format, params object[] arguments)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "{0} {1}: {2} : ", DebuggerTraceWriter.SourceName, eventType.ToString(), (int)traceId);
			if (arguments == null)
			{
				stringBuilder.Append(format);
			}
			else
			{
				stringBuilder.AppendFormat(CultureInfo.InvariantCulture, format, arguments);
			}
			stringBuilder.AppendLine();
			return stringBuilder.ToString();
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x00014CEC File Offset: 0x00012EEC
		public DebuggerTraceWriter()
		{
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x00014CF4 File Offset: 0x00012EF4
		// Note: this type is marked as 'beforefieldinit'.
		static DebuggerTraceWriter()
		{
		}

		// Token: 0x040002E0 RID: 736
		private static readonly string SourceName = "System.ComponentModel.Composition";

		// Token: 0x020000FF RID: 255
		internal enum TraceEventType
		{
			// Token: 0x040002E2 RID: 738
			Error = 2,
			// Token: 0x040002E3 RID: 739
			Warning = 4,
			// Token: 0x040002E4 RID: 740
			Information = 8
		}
	}
}
