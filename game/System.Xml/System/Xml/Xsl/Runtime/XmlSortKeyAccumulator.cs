using System;
using System.ComponentModel;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x0200048E RID: 1166
	[EditorBrowsable(EditorBrowsableState.Never)]
	public struct XmlSortKeyAccumulator
	{
		// Token: 0x06002D72 RID: 11634 RVA: 0x00109C75 File Offset: 0x00107E75
		public void Create()
		{
			if (this.keys == null)
			{
				this.keys = new XmlSortKey[64];
			}
			this.pos = 0;
			this.keys[0] = null;
		}

		// Token: 0x06002D73 RID: 11635 RVA: 0x00109C9C File Offset: 0x00107E9C
		public void AddStringSortKey(XmlCollation collation, string value)
		{
			this.AppendSortKey(collation.CreateSortKey(value));
		}

		// Token: 0x06002D74 RID: 11636 RVA: 0x00109CAB File Offset: 0x00107EAB
		public void AddDecimalSortKey(XmlCollation collation, decimal value)
		{
			this.AppendSortKey(new XmlDecimalSortKey(value, collation));
		}

		// Token: 0x06002D75 RID: 11637 RVA: 0x00109CBA File Offset: 0x00107EBA
		public void AddIntegerSortKey(XmlCollation collation, long value)
		{
			this.AppendSortKey(new XmlIntegerSortKey(value, collation));
		}

		// Token: 0x06002D76 RID: 11638 RVA: 0x00109CC9 File Offset: 0x00107EC9
		public void AddIntSortKey(XmlCollation collation, int value)
		{
			this.AppendSortKey(new XmlIntSortKey(value, collation));
		}

		// Token: 0x06002D77 RID: 11639 RVA: 0x00109CD8 File Offset: 0x00107ED8
		public void AddDoubleSortKey(XmlCollation collation, double value)
		{
			this.AppendSortKey(new XmlDoubleSortKey(value, collation));
		}

		// Token: 0x06002D78 RID: 11640 RVA: 0x00109CE7 File Offset: 0x00107EE7
		public void AddDateTimeSortKey(XmlCollation collation, DateTime value)
		{
			this.AppendSortKey(new XmlDateTimeSortKey(value, collation));
		}

		// Token: 0x06002D79 RID: 11641 RVA: 0x00109CF6 File Offset: 0x00107EF6
		public void AddEmptySortKey(XmlCollation collation)
		{
			this.AppendSortKey(new XmlEmptySortKey(collation));
		}

		// Token: 0x06002D7A RID: 11642 RVA: 0x00109D04 File Offset: 0x00107F04
		public void FinishSortKeys()
		{
			this.pos++;
			if (this.pos >= this.keys.Length)
			{
				XmlSortKey[] destinationArray = new XmlSortKey[this.pos * 2];
				Array.Copy(this.keys, 0, destinationArray, 0, this.keys.Length);
				this.keys = destinationArray;
			}
			this.keys[this.pos] = null;
		}

		// Token: 0x06002D7B RID: 11643 RVA: 0x00109D68 File Offset: 0x00107F68
		private void AppendSortKey(XmlSortKey key)
		{
			key.Priority = this.pos;
			if (this.keys[this.pos] == null)
			{
				this.keys[this.pos] = key;
				return;
			}
			this.keys[this.pos].AddSortKey(key);
		}

		// Token: 0x1700087E RID: 2174
		// (get) Token: 0x06002D7C RID: 11644 RVA: 0x00109DA8 File Offset: 0x00107FA8
		public Array Keys
		{
			get
			{
				return this.keys;
			}
		}

		// Token: 0x04002331 RID: 9009
		private XmlSortKey[] keys;

		// Token: 0x04002332 RID: 9010
		private int pos;

		// Token: 0x04002333 RID: 9011
		private const int DefaultSortKeyCount = 64;
	}
}
