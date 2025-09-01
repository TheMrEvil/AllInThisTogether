using System;
using System.Xml;

namespace System.Runtime.Diagnostics
{
	// Token: 0x0200004E RID: 78
	internal class StringTraceRecord : TraceRecord
	{
		// Token: 0x060002DE RID: 734 RVA: 0x0000FAC1 File Offset: 0x0000DCC1
		internal StringTraceRecord(string elementName, string content)
		{
			this.elementName = elementName;
			this.content = content;
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060002DF RID: 735 RVA: 0x0000FAD7 File Offset: 0x0000DCD7
		internal override string EventId
		{
			get
			{
				return base.BuildEventId("String");
			}
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x0000FAE4 File Offset: 0x0000DCE4
		internal override void WriteTo(XmlWriter writer)
		{
			writer.WriteElementString(this.elementName, this.content);
		}

		// Token: 0x040001E5 RID: 485
		private string elementName;

		// Token: 0x040001E6 RID: 486
		private string content;
	}
}
