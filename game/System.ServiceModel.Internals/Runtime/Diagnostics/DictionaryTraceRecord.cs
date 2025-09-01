using System;
using System.Collections;
using System.Xml;

namespace System.Runtime.Diagnostics
{
	// Token: 0x02000042 RID: 66
	internal class DictionaryTraceRecord : TraceRecord
	{
		// Token: 0x06000266 RID: 614 RVA: 0x0000A296 File Offset: 0x00008496
		internal DictionaryTraceRecord(IDictionary dictionary)
		{
			this.dictionary = dictionary;
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000267 RID: 615 RVA: 0x0000A2A5 File Offset: 0x000084A5
		internal override string EventId
		{
			get
			{
				return "http://schemas.microsoft.com/2006/08/ServiceModel/DictionaryTraceRecord";
			}
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0000A2AC File Offset: 0x000084AC
		internal override void WriteTo(XmlWriter xml)
		{
			if (this.dictionary != null)
			{
				foreach (object obj in this.dictionary.Keys)
				{
					object obj2 = this.dictionary[obj];
					xml.WriteElementString(obj.ToString(), (obj2 == null) ? string.Empty : obj2.ToString());
				}
			}
		}

		// Token: 0x04000157 RID: 343
		private IDictionary dictionary;
	}
}
