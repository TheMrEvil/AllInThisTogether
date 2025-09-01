using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000650 RID: 1616
	internal sealed class SortKey
	{
		// Token: 0x0600418A RID: 16778 RVA: 0x00167B5B File Offset: 0x00165D5B
		public SortKey(int numKeys, int originalPosition, XPathNavigator node)
		{
			this._numKeys = numKeys;
			this._keys = new object[numKeys];
			this._originalPosition = originalPosition;
			this._node = node;
		}

		// Token: 0x17000C7B RID: 3195
		public object this[int index]
		{
			get
			{
				return this._keys[index];
			}
			set
			{
				this._keys[index] = value;
			}
		}

		// Token: 0x17000C7C RID: 3196
		// (get) Token: 0x0600418D RID: 16781 RVA: 0x00167B99 File Offset: 0x00165D99
		public int NumKeys
		{
			get
			{
				return this._numKeys;
			}
		}

		// Token: 0x17000C7D RID: 3197
		// (get) Token: 0x0600418E RID: 16782 RVA: 0x00167BA1 File Offset: 0x00165DA1
		public int OriginalPosition
		{
			get
			{
				return this._originalPosition;
			}
		}

		// Token: 0x17000C7E RID: 3198
		// (get) Token: 0x0600418F RID: 16783 RVA: 0x00167BA9 File Offset: 0x00165DA9
		public XPathNavigator Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x04002E8D RID: 11917
		private int _numKeys;

		// Token: 0x04002E8E RID: 11918
		private object[] _keys;

		// Token: 0x04002E8F RID: 11919
		private int _originalPosition;

		// Token: 0x04002E90 RID: 11920
		private XPathNavigator _node;
	}
}
