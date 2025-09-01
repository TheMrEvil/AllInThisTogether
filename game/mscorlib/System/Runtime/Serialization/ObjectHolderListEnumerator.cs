using System;

namespace System.Runtime.Serialization
{
	// Token: 0x02000667 RID: 1639
	internal class ObjectHolderListEnumerator
	{
		// Token: 0x06003D4F RID: 15695 RVA: 0x000D461E File Offset: 0x000D281E
		internal ObjectHolderListEnumerator(ObjectHolderList list, bool isFixupEnumerator)
		{
			this.m_list = list;
			this.m_startingVersion = this.m_list.Version;
			this.m_currPos = -1;
			this.m_isFixupEnumerator = isFixupEnumerator;
		}

		// Token: 0x06003D50 RID: 15696 RVA: 0x000D464C File Offset: 0x000D284C
		internal bool MoveNext()
		{
			if (this.m_isFixupEnumerator)
			{
				int num;
				do
				{
					num = this.m_currPos + 1;
					this.m_currPos = num;
				}
				while (num < this.m_list.Count && this.m_list.m_values[this.m_currPos].CompletelyFixed);
				return this.m_currPos != this.m_list.Count;
			}
			this.m_currPos++;
			return this.m_currPos != this.m_list.Count;
		}

		// Token: 0x17000969 RID: 2409
		// (get) Token: 0x06003D51 RID: 15697 RVA: 0x000D46D3 File Offset: 0x000D28D3
		internal ObjectHolder Current
		{
			get
			{
				return this.m_list.m_values[this.m_currPos];
			}
		}

		// Token: 0x04002774 RID: 10100
		private bool m_isFixupEnumerator;

		// Token: 0x04002775 RID: 10101
		private ObjectHolderList m_list;

		// Token: 0x04002776 RID: 10102
		private int m_startingVersion;

		// Token: 0x04002777 RID: 10103
		private int m_currPos;
	}
}
