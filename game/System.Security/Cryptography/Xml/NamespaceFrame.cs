using System;
using System.Collections;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x02000049 RID: 73
	internal class NamespaceFrame
	{
		// Token: 0x060001E8 RID: 488 RVA: 0x00008DAF File Offset: 0x00006FAF
		internal NamespaceFrame()
		{
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00008DCD File Offset: 0x00006FCD
		internal void AddRendered(XmlAttribute attr)
		{
			this._rendered.Add(Utils.GetNamespacePrefix(attr), attr);
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00008DE1 File Offset: 0x00006FE1
		internal XmlAttribute GetRendered(string nsPrefix)
		{
			return (XmlAttribute)this._rendered[nsPrefix];
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00008DF4 File Offset: 0x00006FF4
		internal void AddUnrendered(XmlAttribute attr)
		{
			this._unrendered.Add(Utils.GetNamespacePrefix(attr), attr);
		}

		// Token: 0x060001EC RID: 492 RVA: 0x00008E08 File Offset: 0x00007008
		internal XmlAttribute GetUnrendered(string nsPrefix)
		{
			return (XmlAttribute)this._unrendered[nsPrefix];
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00008E1B File Offset: 0x0000701B
		internal Hashtable GetUnrendered()
		{
			return this._unrendered;
		}

		// Token: 0x040001B0 RID: 432
		private Hashtable _rendered = new Hashtable();

		// Token: 0x040001B1 RID: 433
		private Hashtable _unrendered = new Hashtable();
	}
}
