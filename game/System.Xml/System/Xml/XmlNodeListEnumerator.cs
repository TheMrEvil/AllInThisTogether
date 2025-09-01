using System;
using System.Collections;

namespace System.Xml
{
	// Token: 0x020001B3 RID: 435
	internal class XmlNodeListEnumerator : IEnumerator
	{
		// Token: 0x06000FCF RID: 4047 RVA: 0x00066586 File Offset: 0x00064786
		public XmlNodeListEnumerator(XPathNodeList list)
		{
			this.list = list;
			this.index = -1;
			this.valid = false;
		}

		// Token: 0x06000FD0 RID: 4048 RVA: 0x000665A3 File Offset: 0x000647A3
		public void Reset()
		{
			this.index = -1;
		}

		// Token: 0x06000FD1 RID: 4049 RVA: 0x000665AC File Offset: 0x000647AC
		public bool MoveNext()
		{
			this.index++;
			if (this.list.ReadUntil(this.index + 1) - 1 < this.index)
			{
				return false;
			}
			this.valid = (this.list[this.index] != null);
			return this.valid;
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000FD2 RID: 4050 RVA: 0x00066606 File Offset: 0x00064806
		public object Current
		{
			get
			{
				if (this.valid)
				{
					return this.list[this.index];
				}
				return null;
			}
		}

		// Token: 0x04001037 RID: 4151
		private XPathNodeList list;

		// Token: 0x04001038 RID: 4152
		private int index;

		// Token: 0x04001039 RID: 4153
		private bool valid;
	}
}
