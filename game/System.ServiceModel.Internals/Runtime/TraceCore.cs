using System;
using System.Globalization;
using System.Resources;
using System.Runtime.Diagnostics;
using System.Security;

namespace System.Runtime
{
	// Token: 0x0200003A RID: 58
	internal class TraceCore
	{
		// Token: 0x060001BE RID: 446 RVA: 0x00007BFA File Offset: 0x00005DFA
		private TraceCore()
		{
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060001BF RID: 447 RVA: 0x00007C02 File Offset: 0x00005E02
		private static ResourceManager ResourceManager
		{
			get
			{
				if (TraceCore.resourceManager == null)
				{
					TraceCore.resourceManager = new ResourceManager("System.Runtime.TraceCore", typeof(TraceCore).Assembly);
				}
				return TraceCore.resourceManager;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x00007C2E File Offset: 0x00005E2E
		// (set) Token: 0x060001C1 RID: 449 RVA: 0x00007C35 File Offset: 0x00005E35
		internal static CultureInfo Culture
		{
			get
			{
				return TraceCore.resourceCulture;
			}
			set
			{
				TraceCore.resourceCulture = value;
			}
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00007C3D File Offset: 0x00005E3D
		internal static bool AppDomainUnloadIsEnabled(EtwDiagnosticTrace trace)
		{
			return trace.ShouldTrace(TraceEventLevel.Informational) || TraceCore.IsEtwEventEnabled(trace, 0);
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00007C54 File Offset: 0x00005E54
		internal static void AppDomainUnload(EtwDiagnosticTrace trace, string appdomainName, string processName, string processId)
		{
			TracePayload serializedPayload = trace.GetSerializedPayload(null, null, null);
			if (TraceCore.IsEtwEventEnabled(trace, 0))
			{
				TraceCore.WriteEtwEvent(trace, 0, null, appdomainName, processName, processId, serializedPayload.AppDomainFriendlyName);
			}
			if (trace.ShouldTraceToTraceSource(TraceEventLevel.Informational))
			{
				string description = string.Format(TraceCore.Culture, TraceCore.ResourceManager.GetString("AppDomainUnload", TraceCore.Culture), appdomainName, processName, processId);
				TraceCore.WriteTraceSource(trace, 0, description, serializedPayload);
			}
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00007CBB File Offset: 0x00005EBB
		internal static bool HandledExceptionIsEnabled(EtwDiagnosticTrace trace)
		{
			return trace.ShouldTrace(TraceEventLevel.Informational) || TraceCore.IsEtwEventEnabled(trace, 1);
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00007CD0 File Offset: 0x00005ED0
		internal static void HandledException(EtwDiagnosticTrace trace, string param0, Exception exception)
		{
			TracePayload serializedPayload = trace.GetSerializedPayload(null, null, exception);
			if (TraceCore.IsEtwEventEnabled(trace, 1))
			{
				TraceCore.WriteEtwEvent(trace, 1, null, param0, serializedPayload.SerializedException, serializedPayload.AppDomainFriendlyName);
			}
			if (trace.ShouldTraceToTraceSource(TraceEventLevel.Informational))
			{
				string description = string.Format(TraceCore.Culture, TraceCore.ResourceManager.GetString("HandledException", TraceCore.Culture), param0);
				TraceCore.WriteTraceSource(trace, 1, description, serializedPayload);
			}
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00007D3A File Offset: 0x00005F3A
		internal static bool ShipAssertExceptionMessageIsEnabled(EtwDiagnosticTrace trace)
		{
			return trace.ShouldTrace(TraceEventLevel.Error) || TraceCore.IsEtwEventEnabled(trace, 2);
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00007D50 File Offset: 0x00005F50
		internal static void ShipAssertExceptionMessage(EtwDiagnosticTrace trace, string param0)
		{
			TracePayload serializedPayload = trace.GetSerializedPayload(null, null, null);
			if (TraceCore.IsEtwEventEnabled(trace, 2))
			{
				TraceCore.WriteEtwEvent(trace, 2, null, param0, serializedPayload.AppDomainFriendlyName);
			}
			if (trace.ShouldTraceToTraceSource(TraceEventLevel.Error))
			{
				string description = string.Format(TraceCore.Culture, TraceCore.ResourceManager.GetString("ShipAssertExceptionMessage", TraceCore.Culture), param0);
				TraceCore.WriteTraceSource(trace, 2, description, serializedPayload);
			}
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00007DB3 File Offset: 0x00005FB3
		internal static bool ThrowingExceptionIsEnabled(EtwDiagnosticTrace trace)
		{
			return trace.ShouldTrace(TraceEventLevel.Warning) || TraceCore.IsEtwEventEnabled(trace, 3);
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00007DC8 File Offset: 0x00005FC8
		internal static void ThrowingException(EtwDiagnosticTrace trace, string param0, string param1, Exception exception)
		{
			TracePayload serializedPayload = trace.GetSerializedPayload(null, null, exception);
			if (TraceCore.IsEtwEventEnabled(trace, 3))
			{
				TraceCore.WriteEtwEvent(trace, 3, null, param0, param1, serializedPayload.SerializedException, serializedPayload.AppDomainFriendlyName);
			}
			if (trace.ShouldTraceToTraceSource(TraceEventLevel.Warning))
			{
				string description = string.Format(TraceCore.Culture, TraceCore.ResourceManager.GetString("ThrowingException", TraceCore.Culture), param0, param1);
				TraceCore.WriteTraceSource(trace, 3, description, serializedPayload);
			}
		}

		// Token: 0x060001CA RID: 458 RVA: 0x00007E34 File Offset: 0x00006034
		internal static bool UnhandledExceptionIsEnabled(EtwDiagnosticTrace trace)
		{
			return trace.ShouldTrace(TraceEventLevel.Critical) || TraceCore.IsEtwEventEnabled(trace, 4);
		}

		// Token: 0x060001CB RID: 459 RVA: 0x00007E48 File Offset: 0x00006048
		internal static void UnhandledException(EtwDiagnosticTrace trace, string param0, Exception exception)
		{
			TracePayload serializedPayload = trace.GetSerializedPayload(null, null, exception);
			if (TraceCore.IsEtwEventEnabled(trace, 4))
			{
				TraceCore.WriteEtwEvent(trace, 4, null, param0, serializedPayload.SerializedException, serializedPayload.AppDomainFriendlyName);
			}
			if (trace.ShouldTraceToTraceSource(TraceEventLevel.Critical))
			{
				string description = string.Format(TraceCore.Culture, TraceCore.ResourceManager.GetString("UnhandledException", TraceCore.Culture), param0);
				TraceCore.WriteTraceSource(trace, 4, description, serializedPayload);
			}
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00007EB2 File Offset: 0x000060B2
		internal static bool TraceCodeEventLogCriticalIsEnabled(EtwDiagnosticTrace trace)
		{
			return trace.ShouldTrace(TraceEventLevel.Critical) || TraceCore.IsEtwEventEnabled(trace, 5);
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00007EC8 File Offset: 0x000060C8
		internal static void TraceCodeEventLogCritical(EtwDiagnosticTrace trace, TraceRecord traceRecord)
		{
			TracePayload serializedPayload = trace.GetSerializedPayload(null, traceRecord, null);
			if (TraceCore.IsEtwEventEnabled(trace, 5))
			{
				TraceCore.WriteEtwEvent(trace, 5, null, serializedPayload.ExtendedData, serializedPayload.AppDomainFriendlyName);
			}
			if (trace.ShouldTraceToTraceSource(TraceEventLevel.Critical))
			{
				string description = string.Format(TraceCore.Culture, TraceCore.ResourceManager.GetString("TraceCodeEventLogCritical", TraceCore.Culture), Array.Empty<object>());
				TraceCore.WriteTraceSource(trace, 5, description, serializedPayload);
			}
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00007F35 File Offset: 0x00006135
		internal static bool TraceCodeEventLogErrorIsEnabled(EtwDiagnosticTrace trace)
		{
			return trace.ShouldTrace(TraceEventLevel.Error) || TraceCore.IsEtwEventEnabled(trace, 6);
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00007F4C File Offset: 0x0000614C
		internal static void TraceCodeEventLogError(EtwDiagnosticTrace trace, TraceRecord traceRecord)
		{
			TracePayload serializedPayload = trace.GetSerializedPayload(null, traceRecord, null);
			if (TraceCore.IsEtwEventEnabled(trace, 6))
			{
				TraceCore.WriteEtwEvent(trace, 6, null, serializedPayload.ExtendedData, serializedPayload.AppDomainFriendlyName);
			}
			if (trace.ShouldTraceToTraceSource(TraceEventLevel.Error))
			{
				string description = string.Format(TraceCore.Culture, TraceCore.ResourceManager.GetString("TraceCodeEventLogError", TraceCore.Culture), Array.Empty<object>());
				TraceCore.WriteTraceSource(trace, 6, description, serializedPayload);
			}
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00007FB9 File Offset: 0x000061B9
		internal static bool TraceCodeEventLogInfoIsEnabled(EtwDiagnosticTrace trace)
		{
			return trace.ShouldTrace(TraceEventLevel.Informational) || TraceCore.IsEtwEventEnabled(trace, 7);
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00007FD0 File Offset: 0x000061D0
		internal static void TraceCodeEventLogInfo(EtwDiagnosticTrace trace, TraceRecord traceRecord)
		{
			TracePayload serializedPayload = trace.GetSerializedPayload(null, traceRecord, null);
			if (TraceCore.IsEtwEventEnabled(trace, 7))
			{
				TraceCore.WriteEtwEvent(trace, 7, null, serializedPayload.ExtendedData, serializedPayload.AppDomainFriendlyName);
			}
			if (trace.ShouldTraceToTraceSource(TraceEventLevel.Informational))
			{
				string description = string.Format(TraceCore.Culture, TraceCore.ResourceManager.GetString("TraceCodeEventLogInfo", TraceCore.Culture), Array.Empty<object>());
				TraceCore.WriteTraceSource(trace, 7, description, serializedPayload);
			}
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x0000803D File Offset: 0x0000623D
		internal static bool TraceCodeEventLogVerboseIsEnabled(EtwDiagnosticTrace trace)
		{
			return trace.ShouldTrace(TraceEventLevel.Verbose) || TraceCore.IsEtwEventEnabled(trace, 8);
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x00008054 File Offset: 0x00006254
		internal static void TraceCodeEventLogVerbose(EtwDiagnosticTrace trace, TraceRecord traceRecord)
		{
			TracePayload serializedPayload = trace.GetSerializedPayload(null, traceRecord, null);
			if (TraceCore.IsEtwEventEnabled(trace, 8))
			{
				TraceCore.WriteEtwEvent(trace, 8, null, serializedPayload.ExtendedData, serializedPayload.AppDomainFriendlyName);
			}
			if (trace.ShouldTraceToTraceSource(TraceEventLevel.Verbose))
			{
				string description = string.Format(TraceCore.Culture, TraceCore.ResourceManager.GetString("TraceCodeEventLogVerbose", TraceCore.Culture), Array.Empty<object>());
				TraceCore.WriteTraceSource(trace, 8, description, serializedPayload);
			}
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x000080C1 File Offset: 0x000062C1
		internal static bool TraceCodeEventLogWarningIsEnabled(EtwDiagnosticTrace trace)
		{
			return trace.ShouldTrace(TraceEventLevel.Warning) || TraceCore.IsEtwEventEnabled(trace, 9);
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x000080D8 File Offset: 0x000062D8
		internal static void TraceCodeEventLogWarning(EtwDiagnosticTrace trace, TraceRecord traceRecord)
		{
			TracePayload serializedPayload = trace.GetSerializedPayload(null, traceRecord, null);
			if (TraceCore.IsEtwEventEnabled(trace, 9))
			{
				TraceCore.WriteEtwEvent(trace, 9, null, serializedPayload.ExtendedData, serializedPayload.AppDomainFriendlyName);
			}
			if (trace.ShouldTraceToTraceSource(TraceEventLevel.Warning))
			{
				string description = string.Format(TraceCore.Culture, TraceCore.ResourceManager.GetString("TraceCodeEventLogWarning", TraceCore.Culture), Array.Empty<object>());
				TraceCore.WriteTraceSource(trace, 9, description, serializedPayload);
			}
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x00008148 File Offset: 0x00006348
		internal static bool HandledExceptionWarningIsEnabled(EtwDiagnosticTrace trace)
		{
			return trace.ShouldTrace(TraceEventLevel.Warning) || TraceCore.IsEtwEventEnabled(trace, 10);
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x00008160 File Offset: 0x00006360
		internal static void HandledExceptionWarning(EtwDiagnosticTrace trace, string param0, Exception exception)
		{
			TracePayload serializedPayload = trace.GetSerializedPayload(null, null, exception);
			if (TraceCore.IsEtwEventEnabled(trace, 10))
			{
				TraceCore.WriteEtwEvent(trace, 10, null, param0, serializedPayload.SerializedException, serializedPayload.AppDomainFriendlyName);
			}
			if (trace.ShouldTraceToTraceSource(TraceEventLevel.Warning))
			{
				string description = string.Format(TraceCore.Culture, TraceCore.ResourceManager.GetString("HandledExceptionWarning", TraceCore.Culture), param0);
				TraceCore.WriteTraceSource(trace, 10, description, serializedPayload);
			}
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x000081CD File Offset: 0x000063CD
		internal static bool BufferPoolAllocationIsEnabled(EtwDiagnosticTrace trace)
		{
			return TraceCore.IsEtwEventEnabled(trace, 11);
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x000081D8 File Offset: 0x000063D8
		internal static void BufferPoolAllocation(EtwDiagnosticTrace trace, int Size)
		{
			TracePayload serializedPayload = trace.GetSerializedPayload(null, null, null);
			if (TraceCore.IsEtwEventEnabled(trace, 11))
			{
				TraceCore.WriteEtwEvent(trace, 11, null, Size, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x060001DA RID: 474 RVA: 0x0000820B File Offset: 0x0000640B
		internal static bool BufferPoolChangeQuotaIsEnabled(EtwDiagnosticTrace trace)
		{
			return TraceCore.IsEtwEventEnabled(trace, 12);
		}

		// Token: 0x060001DB RID: 475 RVA: 0x00008218 File Offset: 0x00006418
		internal static void BufferPoolChangeQuota(EtwDiagnosticTrace trace, int PoolSize, int Delta)
		{
			TracePayload serializedPayload = trace.GetSerializedPayload(null, null, null);
			if (TraceCore.IsEtwEventEnabled(trace, 12))
			{
				TraceCore.WriteEtwEvent(trace, 12, null, PoolSize, Delta, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x060001DC RID: 476 RVA: 0x0000824C File Offset: 0x0000644C
		internal static bool ActionItemScheduledIsEnabled(EtwDiagnosticTrace trace)
		{
			return TraceCore.IsEtwEventEnabled(trace, 13);
		}

		// Token: 0x060001DD RID: 477 RVA: 0x00008258 File Offset: 0x00006458
		internal static void ActionItemScheduled(EtwDiagnosticTrace trace, EventTraceActivity eventTraceActivity)
		{
			TracePayload serializedPayload = trace.GetSerializedPayload(null, null, null);
			if (TraceCore.IsEtwEventEnabled(trace, 13))
			{
				TraceCore.WriteEtwEvent(trace, 13, eventTraceActivity, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x060001DE RID: 478 RVA: 0x0000828A File Offset: 0x0000648A
		internal static bool ActionItemCallbackInvokedIsEnabled(EtwDiagnosticTrace trace)
		{
			return TraceCore.IsEtwEventEnabled(trace, 14);
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00008294 File Offset: 0x00006494
		internal static void ActionItemCallbackInvoked(EtwDiagnosticTrace trace, EventTraceActivity eventTraceActivity)
		{
			TracePayload serializedPayload = trace.GetSerializedPayload(null, null, null);
			if (TraceCore.IsEtwEventEnabled(trace, 14))
			{
				TraceCore.WriteEtwEvent(trace, 14, eventTraceActivity, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x000082C6 File Offset: 0x000064C6
		internal static bool HandledExceptionErrorIsEnabled(EtwDiagnosticTrace trace)
		{
			return trace.ShouldTrace(TraceEventLevel.Error) || TraceCore.IsEtwEventEnabled(trace, 15);
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x000082DC File Offset: 0x000064DC
		internal static void HandledExceptionError(EtwDiagnosticTrace trace, string param0, Exception exception)
		{
			TracePayload serializedPayload = trace.GetSerializedPayload(null, null, exception);
			if (TraceCore.IsEtwEventEnabled(trace, 15))
			{
				TraceCore.WriteEtwEvent(trace, 15, null, param0, serializedPayload.SerializedException, serializedPayload.AppDomainFriendlyName);
			}
			if (trace.ShouldTraceToTraceSource(TraceEventLevel.Error))
			{
				string description = string.Format(TraceCore.Culture, TraceCore.ResourceManager.GetString("HandledExceptionError", TraceCore.Culture), param0);
				TraceCore.WriteTraceSource(trace, 15, description, serializedPayload);
			}
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00008349 File Offset: 0x00006549
		internal static bool HandledExceptionVerboseIsEnabled(EtwDiagnosticTrace trace)
		{
			return trace.ShouldTrace(TraceEventLevel.Verbose) || TraceCore.IsEtwEventEnabled(trace, 16);
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00008360 File Offset: 0x00006560
		internal static void HandledExceptionVerbose(EtwDiagnosticTrace trace, string param0, Exception exception)
		{
			TracePayload serializedPayload = trace.GetSerializedPayload(null, null, exception);
			if (TraceCore.IsEtwEventEnabled(trace, 16))
			{
				TraceCore.WriteEtwEvent(trace, 16, null, param0, serializedPayload.SerializedException, serializedPayload.AppDomainFriendlyName);
			}
			if (trace.ShouldTraceToTraceSource(TraceEventLevel.Verbose))
			{
				string description = string.Format(TraceCore.Culture, TraceCore.ResourceManager.GetString("HandledExceptionVerbose", TraceCore.Culture), param0);
				TraceCore.WriteTraceSource(trace, 16, description, serializedPayload);
			}
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x000083CD File Offset: 0x000065CD
		internal static bool EtwUnhandledExceptionIsEnabled(EtwDiagnosticTrace trace)
		{
			return TraceCore.IsEtwEventEnabled(trace, 17);
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x000083D8 File Offset: 0x000065D8
		internal static void EtwUnhandledException(EtwDiagnosticTrace trace, string param0, Exception exception)
		{
			TracePayload serializedPayload = trace.GetSerializedPayload(null, null, exception);
			if (TraceCore.IsEtwEventEnabled(trace, 17))
			{
				TraceCore.WriteEtwEvent(trace, 17, null, param0, serializedPayload.SerializedException, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00008412 File Offset: 0x00006612
		internal static bool ThrowingEtwExceptionIsEnabled(EtwDiagnosticTrace trace)
		{
			return TraceCore.IsEtwEventEnabled(trace, 18);
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x0000841C File Offset: 0x0000661C
		internal static void ThrowingEtwException(EtwDiagnosticTrace trace, string param0, string param1, Exception exception)
		{
			TracePayload serializedPayload = trace.GetSerializedPayload(null, null, exception);
			if (TraceCore.IsEtwEventEnabled(trace, 18))
			{
				TraceCore.WriteEtwEvent(trace, 18, null, param0, param1, serializedPayload.SerializedException, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00008457 File Offset: 0x00006657
		internal static bool ThrowingEtwExceptionVerboseIsEnabled(EtwDiagnosticTrace trace)
		{
			return TraceCore.IsEtwEventEnabled(trace, 19);
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00008464 File Offset: 0x00006664
		internal static void ThrowingEtwExceptionVerbose(EtwDiagnosticTrace trace, string param0, string param1, Exception exception)
		{
			TracePayload serializedPayload = trace.GetSerializedPayload(null, null, exception);
			if (TraceCore.IsEtwEventEnabled(trace, 19))
			{
				TraceCore.WriteEtwEvent(trace, 19, null, param0, param1, serializedPayload.SerializedException, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x060001EA RID: 490 RVA: 0x0000849F File Offset: 0x0000669F
		internal static bool ThrowingExceptionVerboseIsEnabled(EtwDiagnosticTrace trace)
		{
			return trace.ShouldTrace(TraceEventLevel.Verbose) || TraceCore.IsEtwEventEnabled(trace, 20);
		}

		// Token: 0x060001EB RID: 491 RVA: 0x000084B4 File Offset: 0x000066B4
		internal static void ThrowingExceptionVerbose(EtwDiagnosticTrace trace, string param0, string param1, Exception exception)
		{
			TracePayload serializedPayload = trace.GetSerializedPayload(null, null, exception);
			if (TraceCore.IsEtwEventEnabled(trace, 20))
			{
				TraceCore.WriteEtwEvent(trace, 20, null, param0, param1, serializedPayload.SerializedException, serializedPayload.AppDomainFriendlyName);
			}
			if (trace.ShouldTraceToTraceSource(TraceEventLevel.Verbose))
			{
				string description = string.Format(TraceCore.Culture, TraceCore.ResourceManager.GetString("ThrowingExceptionVerbose", TraceCore.Culture), param0, param1);
				TraceCore.WriteTraceSource(trace, 20, description, serializedPayload);
			}
		}

		// Token: 0x060001EC RID: 492 RVA: 0x00008524 File Offset: 0x00006724
		[SecuritySafeCritical]
		private static void CreateEventDescriptors()
		{
			TraceCore.eventDescriptors = new EventDescriptor[]
			{
				new EventDescriptor(57393, 0, 19, 4, 0, 0, 1152921504606912512L),
				new EventDescriptor(57394, 0, 18, 4, 0, 0, 2305843009213759488L),
				new EventDescriptor(57395, 0, 18, 2, 0, 0, 2305843009213759488L),
				new EventDescriptor(57396, 0, 18, 3, 0, 0, 2305843009213759488L),
				new EventDescriptor(57397, 0, 17, 1, 0, 0, 4611686018427453440L),
				new EventDescriptor(57399, 0, 19, 1, 0, 0, 1152921504606912512L),
				new EventDescriptor(57400, 0, 19, 2, 0, 0, 1152921504606912512L),
				new EventDescriptor(57401, 0, 19, 4, 0, 0, 1152921504606912512L),
				new EventDescriptor(57402, 0, 19, 5, 0, 0, 1152921504606912512L),
				new EventDescriptor(57403, 0, 19, 3, 0, 0, 1152921504606912512L),
				new EventDescriptor(57404, 0, 18, 3, 0, 0, 2305843009213759488L),
				new EventDescriptor(131, 0, 19, 5, 12, 2509, 1152921504606912512L),
				new EventDescriptor(132, 0, 19, 5, 13, 2509, 1152921504606912512L),
				new EventDescriptor(133, 0, 19, 5, 1, 2593, 1152921504608944128L),
				new EventDescriptor(134, 0, 19, 5, 2, 2593, 1152921504608944128L),
				new EventDescriptor(57405, 0, 17, 2, 0, 0, 4611686018427453440L),
				new EventDescriptor(57406, 0, 18, 5, 0, 0, 2305843009213759488L),
				new EventDescriptor(57408, 0, 17, 1, 0, 0, 4611686018427453440L),
				new EventDescriptor(57410, 0, 18, 3, 0, 0, 2305843009213759488L),
				new EventDescriptor(57409, 0, 18, 5, 0, 0, 2305843009213759488L),
				new EventDescriptor(57407, 0, 18, 5, 0, 0, 2305843009213759488L)
			};
		}

		// Token: 0x060001ED RID: 493 RVA: 0x000087FC File Offset: 0x000069FC
		private static void EnsureEventDescriptors()
		{
			if (TraceCore.eventDescriptorsCreated)
			{
				return;
			}
			lock (TraceCore.syncLock)
			{
				if (!TraceCore.eventDescriptorsCreated)
				{
					TraceCore.CreateEventDescriptors();
					TraceCore.eventDescriptorsCreated = true;
				}
			}
		}

		// Token: 0x060001EE RID: 494 RVA: 0x00008854 File Offset: 0x00006A54
		[SecuritySafeCritical]
		private static bool IsEtwEventEnabled(EtwDiagnosticTrace trace, int eventIndex)
		{
			if (trace.IsEtwProviderEnabled)
			{
				TraceCore.EnsureEventDescriptors();
				return trace.IsEtwEventEnabled(ref TraceCore.eventDescriptors[eventIndex], false);
			}
			return false;
		}

		// Token: 0x060001EF RID: 495 RVA: 0x00008877 File Offset: 0x00006A77
		[SecuritySafeCritical]
		private static bool WriteEtwEvent(EtwDiagnosticTrace trace, int eventIndex, EventTraceActivity eventParam0, string eventParam1, string eventParam2, string eventParam3, string eventParam4)
		{
			TraceCore.EnsureEventDescriptors();
			return trace.EtwProvider.WriteEvent(ref TraceCore.eventDescriptors[eventIndex], eventParam0, eventParam1, eventParam2, eventParam3, eventParam4);
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x0000889C File Offset: 0x00006A9C
		[SecuritySafeCritical]
		private static bool WriteEtwEvent(EtwDiagnosticTrace trace, int eventIndex, EventTraceActivity eventParam0, string eventParam1, string eventParam2, string eventParam3)
		{
			TraceCore.EnsureEventDescriptors();
			return trace.EtwProvider.WriteEvent(ref TraceCore.eventDescriptors[eventIndex], eventParam0, eventParam1, eventParam2, eventParam3);
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x000088BF File Offset: 0x00006ABF
		[SecuritySafeCritical]
		private static bool WriteEtwEvent(EtwDiagnosticTrace trace, int eventIndex, EventTraceActivity eventParam0, string eventParam1, string eventParam2)
		{
			TraceCore.EnsureEventDescriptors();
			return trace.EtwProvider.WriteEvent(ref TraceCore.eventDescriptors[eventIndex], eventParam0, eventParam1, eventParam2);
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x000088E0 File Offset: 0x00006AE0
		[SecuritySafeCritical]
		private static bool WriteEtwEvent(EtwDiagnosticTrace trace, int eventIndex, EventTraceActivity eventParam0, int eventParam1, string eventParam2)
		{
			TraceCore.EnsureEventDescriptors();
			return trace.EtwProvider.WriteEvent(ref TraceCore.eventDescriptors[eventIndex], eventParam0, new object[]
			{
				eventParam1,
				eventParam2
			});
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00008912 File Offset: 0x00006B12
		[SecuritySafeCritical]
		private static bool WriteEtwEvent(EtwDiagnosticTrace trace, int eventIndex, EventTraceActivity eventParam0, int eventParam1, int eventParam2, string eventParam3)
		{
			TraceCore.EnsureEventDescriptors();
			return trace.EtwProvider.WriteEvent(ref TraceCore.eventDescriptors[eventIndex], eventParam0, new object[]
			{
				eventParam1,
				eventParam2,
				eventParam3
			});
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x0000894E File Offset: 0x00006B4E
		[SecuritySafeCritical]
		private static bool WriteEtwEvent(EtwDiagnosticTrace trace, int eventIndex, EventTraceActivity eventParam0, string eventParam1)
		{
			TraceCore.EnsureEventDescriptors();
			return trace.EtwProvider.WriteEvent(ref TraceCore.eventDescriptors[eventIndex], eventParam0, eventParam1);
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x0000896D File Offset: 0x00006B6D
		[SecuritySafeCritical]
		private static void WriteTraceSource(EtwDiagnosticTrace trace, int eventIndex, string description, TracePayload payload)
		{
			TraceCore.EnsureEventDescriptors();
			trace.WriteTraceSource(ref TraceCore.eventDescriptors[eventIndex], description, payload);
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00008987 File Offset: 0x00006B87
		// Note: this type is marked as 'beforefieldinit'.
		static TraceCore()
		{
		}

		// Token: 0x04000114 RID: 276
		private static ResourceManager resourceManager;

		// Token: 0x04000115 RID: 277
		private static CultureInfo resourceCulture;

		// Token: 0x04000116 RID: 278
		[SecurityCritical]
		private static EventDescriptor[] eventDescriptors;

		// Token: 0x04000117 RID: 279
		private static object syncLock = new object();

		// Token: 0x04000118 RID: 280
		private static volatile bool eventDescriptorsCreated;
	}
}
