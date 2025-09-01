using System;
using System.Collections;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x02000024 RID: 36
	internal class CanonicalXmlNodeList : XmlNodeList, IList, ICollection, IEnumerable
	{
		// Token: 0x060000AB RID: 171 RVA: 0x00004278 File Offset: 0x00002478
		internal CanonicalXmlNodeList()
		{
			this._nodeArray = new ArrayList();
		}

		// Token: 0x060000AC RID: 172 RVA: 0x0000428B File Offset: 0x0000248B
		public override XmlNode Item(int index)
		{
			return (XmlNode)this._nodeArray[index];
		}

		// Token: 0x060000AD RID: 173 RVA: 0x0000429E File Offset: 0x0000249E
		public override IEnumerator GetEnumerator()
		{
			return this._nodeArray.GetEnumerator();
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000AE RID: 174 RVA: 0x000042AB File Offset: 0x000024AB
		public override int Count
		{
			get
			{
				return this._nodeArray.Count;
			}
		}

		// Token: 0x060000AF RID: 175 RVA: 0x000042B8 File Offset: 0x000024B8
		public int Add(object value)
		{
			if (!(value is XmlNode))
			{
				throw new ArgumentException("Type of input object is invalid.", "node");
			}
			return this._nodeArray.Add(value);
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x000042DE File Offset: 0x000024DE
		public void Clear()
		{
			this._nodeArray.Clear();
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x000042EB File Offset: 0x000024EB
		public bool Contains(object value)
		{
			return this._nodeArray.Contains(value);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x000042F9 File Offset: 0x000024F9
		public int IndexOf(object value)
		{
			return this._nodeArray.IndexOf(value);
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00004307 File Offset: 0x00002507
		public void Insert(int index, object value)
		{
			if (!(value is XmlNode))
			{
				throw new ArgumentException("Type of input object is invalid.", "value");
			}
			this._nodeArray.Insert(index, value);
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x0000432E File Offset: 0x0000252E
		public void Remove(object value)
		{
			this._nodeArray.Remove(value);
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x0000433C File Offset: 0x0000253C
		public void RemoveAt(int index)
		{
			this._nodeArray.RemoveAt(index);
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x0000434A File Offset: 0x0000254A
		public bool IsFixedSize
		{
			get
			{
				return this._nodeArray.IsFixedSize;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x00004357 File Offset: 0x00002557
		public bool IsReadOnly
		{
			get
			{
				return this._nodeArray.IsReadOnly;
			}
		}

		// Token: 0x1700001F RID: 31
		object IList.this[int index]
		{
			get
			{
				return this._nodeArray[index];
			}
			set
			{
				if (!(value is XmlNode))
				{
					throw new ArgumentException("Type of input object is invalid.", "value");
				}
				this._nodeArray[index] = value;
			}
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00004399 File Offset: 0x00002599
		public void CopyTo(Array array, int index)
		{
			this._nodeArray.CopyTo(array, index);
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000BB RID: 187 RVA: 0x000043A8 File Offset: 0x000025A8
		public object SyncRoot
		{
			get
			{
				return this._nodeArray.SyncRoot;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000BC RID: 188 RVA: 0x000043B5 File Offset: 0x000025B5
		public bool IsSynchronized
		{
			get
			{
				return this._nodeArray.IsSynchronized;
			}
		}

		// Token: 0x0400014C RID: 332
		private ArrayList _nodeArray;
	}
}
