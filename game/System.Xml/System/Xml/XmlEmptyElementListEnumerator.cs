using System;
using System.Collections;

namespace System.Xml
{
	// Token: 0x020001C3 RID: 451
	internal class XmlEmptyElementListEnumerator : IEnumerator
	{
		// Token: 0x06001154 RID: 4436 RVA: 0x0000216B File Offset: 0x0000036B
		public XmlEmptyElementListEnumerator(XmlElementList list)
		{
		}

		// Token: 0x06001155 RID: 4437 RVA: 0x0000D1C5 File Offset: 0x0000B3C5
		public bool MoveNext()
		{
			return false;
		}

		// Token: 0x06001156 RID: 4438 RVA: 0x0000B528 File Offset: 0x00009728
		public void Reset()
		{
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06001157 RID: 4439 RVA: 0x0001DA42 File Offset: 0x0001BC42
		public object Current
		{
			get
			{
				return null;
			}
		}
	}
}
