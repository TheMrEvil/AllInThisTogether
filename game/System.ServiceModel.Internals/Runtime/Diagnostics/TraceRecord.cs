using System;
using System.Xml;

namespace System.Runtime.Diagnostics
{
	// Token: 0x0200004F RID: 79
	[Serializable]
	internal class TraceRecord
	{
		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060002E1 RID: 737 RVA: 0x0000FAF8 File Offset: 0x0000DCF8
		internal virtual string EventId
		{
			get
			{
				return this.BuildEventId("Empty");
			}
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0000FB05 File Offset: 0x0000DD05
		internal virtual void WriteTo(XmlWriter writer)
		{
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x0000FB07 File Offset: 0x0000DD07
		protected string BuildEventId(string eventId)
		{
			return "http://schemas.microsoft.com/2006/08/ServiceModel/" + eventId + "TraceRecord";
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x0000FB19 File Offset: 0x0000DD19
		protected string XmlEncode(string text)
		{
			return DiagnosticTraceBase.XmlEncode(text);
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0000FB21 File Offset: 0x0000DD21
		public TraceRecord()
		{
		}

		// Token: 0x040001E7 RID: 487
		protected const string EventIdBase = "http://schemas.microsoft.com/2006/08/ServiceModel/";

		// Token: 0x040001E8 RID: 488
		protected const string NamespaceSuffix = "TraceRecord";
	}
}
