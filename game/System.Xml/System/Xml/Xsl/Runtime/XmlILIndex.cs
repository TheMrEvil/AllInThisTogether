using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.XPath;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x0200046D RID: 1133
	[EditorBrowsable(EditorBrowsableState.Never)]
	public sealed class XmlILIndex
	{
		// Token: 0x06002BEC RID: 11244 RVA: 0x00105C5E File Offset: 0x00103E5E
		internal XmlILIndex()
		{
			this.table = new Dictionary<string, XmlQueryNodeSequence>();
		}

		// Token: 0x06002BED RID: 11245 RVA: 0x00105C74 File Offset: 0x00103E74
		public void Add(string key, XPathNavigator navigator)
		{
			XmlQueryNodeSequence xmlQueryNodeSequence;
			if (!this.table.TryGetValue(key, out xmlQueryNodeSequence))
			{
				xmlQueryNodeSequence = new XmlQueryNodeSequence();
				xmlQueryNodeSequence.AddClone(navigator);
				this.table.Add(key, xmlQueryNodeSequence);
				return;
			}
			if (!navigator.IsSamePosition(xmlQueryNodeSequence[xmlQueryNodeSequence.Count - 1]))
			{
				xmlQueryNodeSequence.AddClone(navigator);
			}
		}

		// Token: 0x06002BEE RID: 11246 RVA: 0x00105CCC File Offset: 0x00103ECC
		public XmlQueryNodeSequence Lookup(string key)
		{
			XmlQueryNodeSequence result;
			if (!this.table.TryGetValue(key, out result))
			{
				result = new XmlQueryNodeSequence();
			}
			return result;
		}

		// Token: 0x040022C8 RID: 8904
		private Dictionary<string, XmlQueryNodeSequence> table;
	}
}
