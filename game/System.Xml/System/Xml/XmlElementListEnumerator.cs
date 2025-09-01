using System;
using System.Collections;

namespace System.Xml
{
	// Token: 0x020001C2 RID: 450
	internal class XmlElementListEnumerator : IEnumerator
	{
		// Token: 0x06001150 RID: 4432 RVA: 0x0006A822 File Offset: 0x00068A22
		public XmlElementListEnumerator(XmlElementList list)
		{
			this.list = list;
			this.curElem = null;
			this.changeCount = list.ChangeCount;
		}

		// Token: 0x06001151 RID: 4433 RVA: 0x0006A844 File Offset: 0x00068A44
		public bool MoveNext()
		{
			if (this.list.ChangeCount != this.changeCount)
			{
				throw new InvalidOperationException(Res.GetString("The element list has changed. The enumeration operation failed to continue."));
			}
			this.curElem = this.list.GetNextNode(this.curElem);
			return this.curElem != null;
		}

		// Token: 0x06001152 RID: 4434 RVA: 0x0006A894 File Offset: 0x00068A94
		public void Reset()
		{
			this.curElem = null;
			this.changeCount = this.list.ChangeCount;
		}

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06001153 RID: 4435 RVA: 0x0006A8AE File Offset: 0x00068AAE
		public object Current
		{
			get
			{
				return this.curElem;
			}
		}

		// Token: 0x0400108B RID: 4235
		private XmlElementList list;

		// Token: 0x0400108C RID: 4236
		private XmlNode curElem;

		// Token: 0x0400108D RID: 4237
		private int changeCount;
	}
}
