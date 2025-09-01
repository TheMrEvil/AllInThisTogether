using System;
using System.Globalization;
using System.Resources;
using System.Runtime.Diagnostics;
using System.Security;

namespace System.Runtime.Serialization.Diagnostics.Application
{
	// Token: 0x0200019E RID: 414
	internal class TD
	{
		// Token: 0x060014E9 RID: 5353 RVA: 0x0000222F File Offset: 0x0000042F
		private TD()
		{
		}

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x060014EA RID: 5354 RVA: 0x000534A9 File Offset: 0x000516A9
		private static ResourceManager ResourceManager
		{
			get
			{
				if (TD.resourceManager == null)
				{
					TD.resourceManager = new ResourceManager("System.Runtime.Serialization.Diagnostics.Application.TD", typeof(TD).Assembly);
				}
				return TD.resourceManager;
			}
		}

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x060014EB RID: 5355 RVA: 0x000534D5 File Offset: 0x000516D5
		// (set) Token: 0x060014EC RID: 5356 RVA: 0x000534DC File Offset: 0x000516DC
		internal static CultureInfo Culture
		{
			get
			{
				return TD.resourceCulture;
			}
			set
			{
				TD.resourceCulture = value;
			}
		}

		// Token: 0x060014ED RID: 5357 RVA: 0x000534E4 File Offset: 0x000516E4
		internal static bool ReaderQuotaExceededIsEnabled()
		{
			return FxTrace.ShouldTraceError && TD.IsEtwEventEnabled(0);
		}

		// Token: 0x060014EE RID: 5358 RVA: 0x000534F8 File Offset: 0x000516F8
		internal static void ReaderQuotaExceeded(string param0)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(0))
			{
				TD.WriteEtwEvent(0, null, param0, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x060014EF RID: 5359 RVA: 0x0005352B File Offset: 0x0005172B
		internal static bool DCSerializeWithSurrogateStartIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(1);
		}

		// Token: 0x060014F0 RID: 5360 RVA: 0x0005353C File Offset: 0x0005173C
		internal static void DCSerializeWithSurrogateStart(string SurrogateType)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(1))
			{
				TD.WriteEtwEvent(1, null, SurrogateType, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x060014F1 RID: 5361 RVA: 0x0005356F File Offset: 0x0005176F
		internal static bool DCSerializeWithSurrogateStopIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(2);
		}

		// Token: 0x060014F2 RID: 5362 RVA: 0x00053580 File Offset: 0x00051780
		internal static void DCSerializeWithSurrogateStop()
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(2))
			{
				TD.WriteEtwEvent(2, null, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x060014F3 RID: 5363 RVA: 0x000535B2 File Offset: 0x000517B2
		internal static bool DCDeserializeWithSurrogateStartIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(3);
		}

		// Token: 0x060014F4 RID: 5364 RVA: 0x000535C4 File Offset: 0x000517C4
		internal static void DCDeserializeWithSurrogateStart(string SurrogateType)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(3))
			{
				TD.WriteEtwEvent(3, null, SurrogateType, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x060014F5 RID: 5365 RVA: 0x000535F7 File Offset: 0x000517F7
		internal static bool DCDeserializeWithSurrogateStopIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(4);
		}

		// Token: 0x060014F6 RID: 5366 RVA: 0x00053608 File Offset: 0x00051808
		internal static void DCDeserializeWithSurrogateStop()
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(4))
			{
				TD.WriteEtwEvent(4, null, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x060014F7 RID: 5367 RVA: 0x0005363A File Offset: 0x0005183A
		internal static bool ImportKnownTypesStartIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(5);
		}

		// Token: 0x060014F8 RID: 5368 RVA: 0x0005364C File Offset: 0x0005184C
		internal static void ImportKnownTypesStart()
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(5))
			{
				TD.WriteEtwEvent(5, null, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x060014F9 RID: 5369 RVA: 0x0005367E File Offset: 0x0005187E
		internal static bool ImportKnownTypesStopIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(6);
		}

		// Token: 0x060014FA RID: 5370 RVA: 0x00053690 File Offset: 0x00051890
		internal static void ImportKnownTypesStop()
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(6))
			{
				TD.WriteEtwEvent(6, null, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x060014FB RID: 5371 RVA: 0x000536C2 File Offset: 0x000518C2
		internal static bool DCResolverResolveIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(7);
		}

		// Token: 0x060014FC RID: 5372 RVA: 0x000536D4 File Offset: 0x000518D4
		internal static void DCResolverResolve(string TypeName)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(7))
			{
				TD.WriteEtwEvent(7, null, TypeName, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x060014FD RID: 5373 RVA: 0x00053707 File Offset: 0x00051907
		internal static bool DCGenWriterStartIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(8);
		}

		// Token: 0x060014FE RID: 5374 RVA: 0x00053718 File Offset: 0x00051918
		internal static void DCGenWriterStart(string Kind, string TypeName)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(8))
			{
				TD.WriteEtwEvent(8, null, Kind, TypeName, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x060014FF RID: 5375 RVA: 0x0005374C File Offset: 0x0005194C
		internal static bool DCGenWriterStopIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(9);
		}

		// Token: 0x06001500 RID: 5376 RVA: 0x00053760 File Offset: 0x00051960
		internal static void DCGenWriterStop()
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(9))
			{
				TD.WriteEtwEvent(9, null, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06001501 RID: 5377 RVA: 0x00053794 File Offset: 0x00051994
		internal static bool DCGenReaderStartIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(10);
		}

		// Token: 0x06001502 RID: 5378 RVA: 0x000537A8 File Offset: 0x000519A8
		internal static void DCGenReaderStart(string Kind, string TypeName)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(10))
			{
				TD.WriteEtwEvent(10, null, Kind, TypeName, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06001503 RID: 5379 RVA: 0x000537DE File Offset: 0x000519DE
		internal static bool DCGenReaderStopIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(11);
		}

		// Token: 0x06001504 RID: 5380 RVA: 0x000537F0 File Offset: 0x000519F0
		internal static void DCGenReaderStop()
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(11))
			{
				TD.WriteEtwEvent(11, null, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06001505 RID: 5381 RVA: 0x00053824 File Offset: 0x00051A24
		internal static bool DCJsonGenReaderStartIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(12);
		}

		// Token: 0x06001506 RID: 5382 RVA: 0x00053838 File Offset: 0x00051A38
		internal static void DCJsonGenReaderStart(string Kind, string TypeName)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(12))
			{
				TD.WriteEtwEvent(12, null, Kind, TypeName, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06001507 RID: 5383 RVA: 0x0005386E File Offset: 0x00051A6E
		internal static bool DCJsonGenReaderStopIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(13);
		}

		// Token: 0x06001508 RID: 5384 RVA: 0x00053880 File Offset: 0x00051A80
		internal static void DCJsonGenReaderStop()
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(13))
			{
				TD.WriteEtwEvent(13, null, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06001509 RID: 5385 RVA: 0x000538B4 File Offset: 0x00051AB4
		internal static bool DCJsonGenWriterStartIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(14);
		}

		// Token: 0x0600150A RID: 5386 RVA: 0x000538C8 File Offset: 0x00051AC8
		internal static void DCJsonGenWriterStart(string Kind, string TypeName)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(14))
			{
				TD.WriteEtwEvent(14, null, Kind, TypeName, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x0600150B RID: 5387 RVA: 0x000538FE File Offset: 0x00051AFE
		internal static bool DCJsonGenWriterStopIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(15);
		}

		// Token: 0x0600150C RID: 5388 RVA: 0x00053910 File Offset: 0x00051B10
		internal static void DCJsonGenWriterStop()
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(15))
			{
				TD.WriteEtwEvent(15, null, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x0600150D RID: 5389 RVA: 0x00053944 File Offset: 0x00051B44
		internal static bool GenXmlSerializableStartIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(16);
		}

		// Token: 0x0600150E RID: 5390 RVA: 0x00053958 File Offset: 0x00051B58
		internal static void GenXmlSerializableStart(string DCType)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(16))
			{
				TD.WriteEtwEvent(16, null, DCType, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x0600150F RID: 5391 RVA: 0x0005398D File Offset: 0x00051B8D
		internal static bool GenXmlSerializableStopIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(17);
		}

		// Token: 0x06001510 RID: 5392 RVA: 0x000539A0 File Offset: 0x00051BA0
		internal static void GenXmlSerializableStop()
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(17))
			{
				TD.WriteEtwEvent(17, null, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06001511 RID: 5393 RVA: 0x000539D4 File Offset: 0x00051BD4
		[SecuritySafeCritical]
		private static void CreateEventDescriptors()
		{
			EventDescriptor[] ed = new EventDescriptor[]
			{
				new EventDescriptor(1420, 0, 18, 2, 0, 2560, 2305843009217888256L),
				new EventDescriptor(5001, 0, 19, 5, 1, 2592, 1152921504606846978L),
				new EventDescriptor(5002, 0, 19, 5, 2, 2592, 1152921504606846978L),
				new EventDescriptor(5003, 0, 19, 5, 1, 2591, 1152921504606846978L),
				new EventDescriptor(5004, 0, 19, 5, 2, 2591, 1152921504606846978L),
				new EventDescriptor(5005, 0, 19, 5, 1, 2547, 1152921504606846978L),
				new EventDescriptor(5006, 0, 19, 5, 2, 2547, 1152921504606846978L),
				new EventDescriptor(5007, 0, 19, 5, 1, 2528, 1152921504606846978L),
				new EventDescriptor(5008, 0, 19, 5, 1, 2544, 1152921504606846978L),
				new EventDescriptor(5009, 0, 19, 5, 2, 2544, 1152921504606846978L),
				new EventDescriptor(5010, 0, 19, 5, 1, 2543, 1152921504606846978L),
				new EventDescriptor(5011, 0, 19, 5, 2, 2543, 1152921504606846978L),
				new EventDescriptor(5012, 0, 19, 5, 1, 2543, 1152921504606846978L),
				new EventDescriptor(5013, 0, 19, 5, 2, 2543, 1152921504606846978L),
				new EventDescriptor(5014, 0, 19, 5, 1, 2544, 1152921504606846978L),
				new EventDescriptor(5015, 0, 19, 5, 2, 2544, 1152921504606846978L),
				new EventDescriptor(5016, 0, 19, 5, 1, 2545, 1152921504606846978L),
				new EventDescriptor(5017, 0, 19, 5, 2, 2545, 1152921504606846978L)
			};
			ushort[] events = new ushort[0];
			FxTrace.UpdateEventDefinitions(ed, events);
			TD.eventDescriptors = ed;
		}

		// Token: 0x06001512 RID: 5394 RVA: 0x00053C8C File Offset: 0x00051E8C
		private static void EnsureEventDescriptors()
		{
			if (TD.eventDescriptorsCreated)
			{
				return;
			}
			lock (TD.syncLock)
			{
				if (!TD.eventDescriptorsCreated)
				{
					TD.CreateEventDescriptors();
					TD.eventDescriptorsCreated = true;
				}
			}
		}

		// Token: 0x06001513 RID: 5395 RVA: 0x00053CE4 File Offset: 0x00051EE4
		private static bool IsEtwEventEnabled(int eventIndex)
		{
			if (FxTrace.Trace.IsEtwProviderEnabled)
			{
				TD.EnsureEventDescriptors();
				return FxTrace.IsEventEnabled(eventIndex);
			}
			return false;
		}

		// Token: 0x06001514 RID: 5396 RVA: 0x00053CFF File Offset: 0x00051EFF
		[SecuritySafeCritical]
		private static bool WriteEtwEvent(int eventIndex, EventTraceActivity eventParam0, string eventParam1, string eventParam2)
		{
			TD.EnsureEventDescriptors();
			return FxTrace.Trace.EtwProvider.WriteEvent(ref TD.eventDescriptors[eventIndex], eventParam0, eventParam1, eventParam2);
		}

		// Token: 0x06001515 RID: 5397 RVA: 0x00053D23 File Offset: 0x00051F23
		[SecuritySafeCritical]
		private static bool WriteEtwEvent(int eventIndex, EventTraceActivity eventParam0, string eventParam1)
		{
			TD.EnsureEventDescriptors();
			return FxTrace.Trace.EtwProvider.WriteEvent(ref TD.eventDescriptors[eventIndex], eventParam0, eventParam1);
		}

		// Token: 0x06001516 RID: 5398 RVA: 0x00053D46 File Offset: 0x00051F46
		[SecuritySafeCritical]
		private static bool WriteEtwEvent(int eventIndex, EventTraceActivity eventParam0, string eventParam1, string eventParam2, string eventParam3)
		{
			TD.EnsureEventDescriptors();
			return FxTrace.Trace.EtwProvider.WriteEvent(ref TD.eventDescriptors[eventIndex], eventParam0, eventParam1, eventParam2, eventParam3);
		}

		// Token: 0x06001517 RID: 5399 RVA: 0x00053D6C File Offset: 0x00051F6C
		// Note: this type is marked as 'beforefieldinit'.
		static TD()
		{
		}

		// Token: 0x04000A75 RID: 2677
		private static ResourceManager resourceManager;

		// Token: 0x04000A76 RID: 2678
		private static CultureInfo resourceCulture;

		// Token: 0x04000A77 RID: 2679
		[SecurityCritical]
		private static EventDescriptor[] eventDescriptors;

		// Token: 0x04000A78 RID: 2680
		private static object syncLock = new object();

		// Token: 0x04000A79 RID: 2681
		private static volatile bool eventDescriptorsCreated;
	}
}
