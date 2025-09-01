using System;
using System.ComponentModel;
using System.Xml.XPath;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x0200047D RID: 1149
	[EditorBrowsable(EditorBrowsableState.Never)]
	public sealed class XmlQueryItemSequence : XmlQuerySequence<XPathItem>
	{
		// Token: 0x06002CF6 RID: 11510 RVA: 0x0010897D File Offset: 0x00106B7D
		public static XmlQueryItemSequence CreateOrReuse(XmlQueryItemSequence seq)
		{
			if (seq != null)
			{
				seq.Clear();
				return seq;
			}
			return new XmlQueryItemSequence();
		}

		// Token: 0x06002CF7 RID: 11511 RVA: 0x0010898F File Offset: 0x00106B8F
		public static XmlQueryItemSequence CreateOrReuse(XmlQueryItemSequence seq, XPathItem item)
		{
			if (seq != null)
			{
				seq.Clear();
				seq.Add(item);
				return seq;
			}
			return new XmlQueryItemSequence(item);
		}

		// Token: 0x06002CF8 RID: 11512 RVA: 0x001089A9 File Offset: 0x00106BA9
		public XmlQueryItemSequence()
		{
		}

		// Token: 0x06002CF9 RID: 11513 RVA: 0x001089B1 File Offset: 0x00106BB1
		public XmlQueryItemSequence(int capacity) : base(capacity)
		{
		}

		// Token: 0x06002CFA RID: 11514 RVA: 0x001089BA File Offset: 0x00106BBA
		public XmlQueryItemSequence(XPathItem item) : base(1)
		{
			this.AddClone(item);
		}

		// Token: 0x06002CFB RID: 11515 RVA: 0x001089CA File Offset: 0x00106BCA
		public void AddClone(XPathItem item)
		{
			if (item.IsNode)
			{
				base.Add(((XPathNavigator)item).Clone());
				return;
			}
			base.Add(item);
		}

		// Token: 0x06002CFC RID: 11516 RVA: 0x001089ED File Offset: 0x00106BED
		// Note: this type is marked as 'beforefieldinit'.
		static XmlQueryItemSequence()
		{
		}

		// Token: 0x04002311 RID: 8977
		public new static readonly XmlQueryItemSequence Empty = new XmlQueryItemSequence();
	}
}
