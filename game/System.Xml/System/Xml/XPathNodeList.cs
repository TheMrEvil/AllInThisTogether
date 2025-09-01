using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.XPath;

namespace System.Xml
{
	// Token: 0x020001B2 RID: 434
	internal class XPathNodeList : XmlNodeList
	{
		// Token: 0x06000FC8 RID: 4040 RVA: 0x00066484 File Offset: 0x00064684
		public XPathNodeList(XPathNodeIterator nodeIterator)
		{
			this.nodeIterator = nodeIterator;
			this.list = new List<XmlNode>();
			this.done = false;
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000FC9 RID: 4041 RVA: 0x000664A5 File Offset: 0x000646A5
		public override int Count
		{
			get
			{
				if (!this.done)
				{
					this.ReadUntil(int.MaxValue);
				}
				return this.list.Count;
			}
		}

		// Token: 0x06000FCA RID: 4042 RVA: 0x000664C6 File Offset: 0x000646C6
		private XmlNode GetNode(XPathNavigator n)
		{
			return ((IHasXmlNode)n).GetNode();
		}

		// Token: 0x06000FCB RID: 4043 RVA: 0x000664D4 File Offset: 0x000646D4
		internal int ReadUntil(int index)
		{
			int num = this.list.Count;
			while (!this.done && num <= index)
			{
				if (!this.nodeIterator.MoveNext())
				{
					this.done = true;
					break;
				}
				XmlNode node = this.GetNode(this.nodeIterator.Current);
				if (node != null)
				{
					this.list.Add(node);
					num++;
				}
			}
			return num;
		}

		// Token: 0x06000FCC RID: 4044 RVA: 0x00066539 File Offset: 0x00064739
		public override XmlNode Item(int index)
		{
			if (this.list.Count <= index)
			{
				this.ReadUntil(index);
			}
			if (index < 0 || this.list.Count <= index)
			{
				return null;
			}
			return this.list[index];
		}

		// Token: 0x06000FCD RID: 4045 RVA: 0x00066571 File Offset: 0x00064771
		public override IEnumerator GetEnumerator()
		{
			return new XmlNodeListEnumerator(this);
		}

		// Token: 0x06000FCE RID: 4046 RVA: 0x00066579 File Offset: 0x00064779
		// Note: this type is marked as 'beforefieldinit'.
		static XPathNodeList()
		{
		}

		// Token: 0x04001033 RID: 4147
		private List<XmlNode> list;

		// Token: 0x04001034 RID: 4148
		private XPathNodeIterator nodeIterator;

		// Token: 0x04001035 RID: 4149
		private bool done;

		// Token: 0x04001036 RID: 4150
		private static readonly object[] nullparams = new object[0];
	}
}
