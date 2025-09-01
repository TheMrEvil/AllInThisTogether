using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Diagnostics;

namespace System.Runtime.Serialization.Diagnostics
{
	// Token: 0x0200019D RID: 413
	internal static class TraceUtility
	{
		// Token: 0x060014E5 RID: 5349 RVA: 0x00053314 File Offset: 0x00051514
		internal static void Trace(TraceEventType severity, int traceCode, string traceDescription)
		{
			TraceUtility.Trace(severity, traceCode, traceDescription, null);
		}

		// Token: 0x060014E6 RID: 5350 RVA: 0x0005331F File Offset: 0x0005151F
		internal static void Trace(TraceEventType severity, int traceCode, string traceDescription, TraceRecord record)
		{
			TraceUtility.Trace(severity, traceCode, traceDescription, record, null);
		}

		// Token: 0x060014E7 RID: 5351 RVA: 0x0005332C File Offset: 0x0005152C
		internal static void Trace(TraceEventType severity, int traceCode, string traceDescription, TraceRecord record, Exception exception)
		{
			string text = "";
			object[] array = new object[7];
			array[0] = severity;
			array[1] = traceCode;
			array[2] = text;
			array[3] = traceDescription;
			array[4] = record;
			array[5] = exception;
			DiagnosticUtility.DiagnosticTrace.TraceEvent(array);
		}

		// Token: 0x060014E8 RID: 5352 RVA: 0x00053370 File Offset: 0x00051570
		// Note: this type is marked as 'beforefieldinit'.
		static TraceUtility()
		{
		}

		// Token: 0x04000A74 RID: 2676
		private static Dictionary<int, string> traceCodes = new Dictionary<int, string>(18)
		{
			{
				196609,
				"WriteObjectBegin"
			},
			{
				196610,
				"WriteObjectEnd"
			},
			{
				196611,
				"WriteObjectContentBegin"
			},
			{
				196612,
				"WriteObjectContentEnd"
			},
			{
				196613,
				"ReadObjectBegin"
			},
			{
				196614,
				"ReadObjectEnd"
			},
			{
				196615,
				"ElementIgnored"
			},
			{
				196616,
				"XsdExportBegin"
			},
			{
				196617,
				"XsdExportEnd"
			},
			{
				196618,
				"XsdImportBegin"
			},
			{
				196619,
				"XsdImportEnd"
			},
			{
				196620,
				"XsdExportError"
			},
			{
				196621,
				"XsdImportError"
			},
			{
				196622,
				"XsdExportAnnotationFailed"
			},
			{
				196623,
				"XsdImportAnnotationFailed"
			},
			{
				196624,
				"XsdExportDupItems"
			},
			{
				196625,
				"FactoryTypeNotFound"
			},
			{
				196626,
				"ObjectWithLargeDepth"
			}
		};
	}
}
