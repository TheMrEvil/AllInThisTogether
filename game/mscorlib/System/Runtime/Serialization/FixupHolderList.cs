using System;

namespace System.Runtime.Serialization
{
	// Token: 0x02000664 RID: 1636
	[Serializable]
	internal class FixupHolderList
	{
		// Token: 0x06003D3A RID: 15674 RVA: 0x000D42C3 File Offset: 0x000D24C3
		internal FixupHolderList() : this(2)
		{
		}

		// Token: 0x06003D3B RID: 15675 RVA: 0x000D42CC File Offset: 0x000D24CC
		internal FixupHolderList(int startingSize)
		{
			this.m_count = 0;
			this.m_values = new FixupHolder[startingSize];
		}

		// Token: 0x06003D3C RID: 15676 RVA: 0x000D42E8 File Offset: 0x000D24E8
		internal virtual void Add(long id, object fixupInfo)
		{
			if (this.m_count == this.m_values.Length)
			{
				this.EnlargeArray();
			}
			this.m_values[this.m_count].m_id = id;
			FixupHolder[] values = this.m_values;
			int count = this.m_count;
			this.m_count = count + 1;
			values[count].m_fixupInfo = fixupInfo;
		}

		// Token: 0x06003D3D RID: 15677 RVA: 0x000D433C File Offset: 0x000D253C
		internal virtual void Add(FixupHolder fixup)
		{
			if (this.m_count == this.m_values.Length)
			{
				this.EnlargeArray();
			}
			FixupHolder[] values = this.m_values;
			int count = this.m_count;
			this.m_count = count + 1;
			values[count] = fixup;
		}

		// Token: 0x06003D3E RID: 15678 RVA: 0x000D4378 File Offset: 0x000D2578
		private void EnlargeArray()
		{
			int num = this.m_values.Length * 2;
			if (num < 0)
			{
				if (num == 2147483647)
				{
					throw new SerializationException(Environment.GetResourceString("The internal array cannot expand to greater than Int32.MaxValue elements."));
				}
				num = int.MaxValue;
			}
			FixupHolder[] array = new FixupHolder[num];
			Array.Copy(this.m_values, array, this.m_count);
			this.m_values = array;
		}

		// Token: 0x04002769 RID: 10089
		internal const int InitialSize = 2;

		// Token: 0x0400276A RID: 10090
		internal FixupHolder[] m_values;

		// Token: 0x0400276B RID: 10091
		internal int m_count;
	}
}
