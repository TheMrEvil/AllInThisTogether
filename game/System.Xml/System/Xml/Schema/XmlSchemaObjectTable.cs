using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Xml.Schema
{
	/// <summary>Provides the collections for contained elements in the <see cref="T:System.Xml.Schema.XmlSchema" /> class (for example, Attributes, AttributeGroups, Elements, and so on).</summary>
	// Token: 0x020005CD RID: 1485
	public class XmlSchemaObjectTable
	{
		// Token: 0x06003BA6 RID: 15270 RVA: 0x0014E645 File Offset: 0x0014C845
		internal XmlSchemaObjectTable()
		{
		}

		// Token: 0x06003BA7 RID: 15271 RVA: 0x0014E663 File Offset: 0x0014C863
		internal void Add(XmlQualifiedName name, XmlSchemaObject value)
		{
			this.table.Add(name, value);
			this.entries.Add(new XmlSchemaObjectTable.XmlSchemaObjectEntry(name, value));
		}

		// Token: 0x06003BA8 RID: 15272 RVA: 0x0014E684 File Offset: 0x0014C884
		internal void Insert(XmlQualifiedName name, XmlSchemaObject value)
		{
			XmlSchemaObject xso = null;
			if (this.table.TryGetValue(name, out xso))
			{
				this.table[name] = value;
				int index = this.FindIndexByValue(xso);
				this.entries[index] = new XmlSchemaObjectTable.XmlSchemaObjectEntry(name, value);
				return;
			}
			this.Add(name, value);
		}

		// Token: 0x06003BA9 RID: 15273 RVA: 0x0014E6D4 File Offset: 0x0014C8D4
		internal void Replace(XmlQualifiedName name, XmlSchemaObject value)
		{
			XmlSchemaObject xso;
			if (this.table.TryGetValue(name, out xso))
			{
				this.table[name] = value;
				int index = this.FindIndexByValue(xso);
				this.entries[index] = new XmlSchemaObjectTable.XmlSchemaObjectEntry(name, value);
			}
		}

		// Token: 0x06003BAA RID: 15274 RVA: 0x0014E719 File Offset: 0x0014C919
		internal void Clear()
		{
			this.table.Clear();
			this.entries.Clear();
		}

		// Token: 0x06003BAB RID: 15275 RVA: 0x0014E734 File Offset: 0x0014C934
		internal void Remove(XmlQualifiedName name)
		{
			XmlSchemaObject xso;
			if (this.table.TryGetValue(name, out xso))
			{
				this.table.Remove(name);
				int index = this.FindIndexByValue(xso);
				this.entries.RemoveAt(index);
			}
		}

		// Token: 0x06003BAC RID: 15276 RVA: 0x0014E774 File Offset: 0x0014C974
		private int FindIndexByValue(XmlSchemaObject xso)
		{
			for (int i = 0; i < this.entries.Count; i++)
			{
				if (this.entries[i].xso == xso)
				{
					return i;
				}
			}
			return -1;
		}

		/// <summary>Gets the number of items contained in the <see cref="T:System.Xml.Schema.XmlSchemaObjectTable" />.</summary>
		/// <returns>The number of items contained in the <see cref="T:System.Xml.Schema.XmlSchemaObjectTable" />.</returns>
		// Token: 0x17000B8D RID: 2957
		// (get) Token: 0x06003BAD RID: 15277 RVA: 0x0014E7AE File Offset: 0x0014C9AE
		public int Count
		{
			get
			{
				return this.table.Count;
			}
		}

		/// <summary>Determines if the qualified name specified exists in the collection.</summary>
		/// <param name="name">The <see cref="T:System.Xml.XmlQualifiedName" />.</param>
		/// <returns>
		///     <see langword="true" /> if the qualified name specified exists in the collection; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003BAE RID: 15278 RVA: 0x0014E7BB File Offset: 0x0014C9BB
		public bool Contains(XmlQualifiedName name)
		{
			return this.table.ContainsKey(name);
		}

		/// <summary>Returns the element in the <see cref="T:System.Xml.Schema.XmlSchemaObjectTable" /> specified by qualified name.</summary>
		/// <param name="name">The <see cref="T:System.Xml.XmlQualifiedName" /> of the element to return.</param>
		/// <returns>The <see cref="T:System.Xml.Schema.XmlSchemaObject" /> of the element in the <see cref="T:System.Xml.Schema.XmlSchemaObjectTable" /> specified by qualified name.</returns>
		// Token: 0x17000B8E RID: 2958
		public XmlSchemaObject this[XmlQualifiedName name]
		{
			get
			{
				XmlSchemaObject result;
				if (this.table.TryGetValue(name, out result))
				{
					return result;
				}
				return null;
			}
		}

		/// <summary>Returns a collection of all the named elements in the <see cref="T:System.Xml.Schema.XmlSchemaObjectTable" />.</summary>
		/// <returns>A collection of all the named elements in the <see cref="T:System.Xml.Schema.XmlSchemaObjectTable" />.</returns>
		// Token: 0x17000B8F RID: 2959
		// (get) Token: 0x06003BB0 RID: 15280 RVA: 0x0014E7EC File Offset: 0x0014C9EC
		public ICollection Names
		{
			get
			{
				return new XmlSchemaObjectTable.NamesCollection(this.entries, this.table.Count);
			}
		}

		/// <summary>Returns a collection of all the values for all the elements in the <see cref="T:System.Xml.Schema.XmlSchemaObjectTable" />.</summary>
		/// <returns>A collection of all the values for all the elements in the <see cref="T:System.Xml.Schema.XmlSchemaObjectTable" />.</returns>
		// Token: 0x17000B90 RID: 2960
		// (get) Token: 0x06003BB1 RID: 15281 RVA: 0x0014E804 File Offset: 0x0014CA04
		public ICollection Values
		{
			get
			{
				return new XmlSchemaObjectTable.ValuesCollection(this.entries, this.table.Count);
			}
		}

		/// <summary>Returns an enumerator that can iterate through the <see cref="T:System.Xml.Schema.XmlSchemaObjectTable" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionaryEnumerator" /> that can iterate through <see cref="T:System.Xml.Schema.XmlSchemaObjectTable" />.</returns>
		// Token: 0x06003BB2 RID: 15282 RVA: 0x0014E81C File Offset: 0x0014CA1C
		public IDictionaryEnumerator GetEnumerator()
		{
			return new XmlSchemaObjectTable.XSODictionaryEnumerator(this.entries, this.table.Count, XmlSchemaObjectTable.EnumeratorType.DictionaryEntry);
		}

		// Token: 0x04002B89 RID: 11145
		private Dictionary<XmlQualifiedName, XmlSchemaObject> table = new Dictionary<XmlQualifiedName, XmlSchemaObject>();

		// Token: 0x04002B8A RID: 11146
		private List<XmlSchemaObjectTable.XmlSchemaObjectEntry> entries = new List<XmlSchemaObjectTable.XmlSchemaObjectEntry>();

		// Token: 0x020005CE RID: 1486
		internal enum EnumeratorType
		{
			// Token: 0x04002B8C RID: 11148
			Keys,
			// Token: 0x04002B8D RID: 11149
			Values,
			// Token: 0x04002B8E RID: 11150
			DictionaryEntry
		}

		// Token: 0x020005CF RID: 1487
		internal struct XmlSchemaObjectEntry
		{
			// Token: 0x06003BB3 RID: 15283 RVA: 0x0014E835 File Offset: 0x0014CA35
			public XmlSchemaObjectEntry(XmlQualifiedName name, XmlSchemaObject value)
			{
				this.qname = name;
				this.xso = value;
			}

			// Token: 0x06003BB4 RID: 15284 RVA: 0x0014E845 File Offset: 0x0014CA45
			public XmlSchemaObject IsMatch(string localName, string ns)
			{
				if (localName == this.qname.Name && ns == this.qname.Namespace)
				{
					return this.xso;
				}
				return null;
			}

			// Token: 0x06003BB5 RID: 15285 RVA: 0x0014E875 File Offset: 0x0014CA75
			public void Reset()
			{
				this.qname = null;
				this.xso = null;
			}

			// Token: 0x04002B8F RID: 11151
			internal XmlQualifiedName qname;

			// Token: 0x04002B90 RID: 11152
			internal XmlSchemaObject xso;
		}

		// Token: 0x020005D0 RID: 1488
		internal class NamesCollection : ICollection, IEnumerable
		{
			// Token: 0x06003BB6 RID: 15286 RVA: 0x0014E885 File Offset: 0x0014CA85
			internal NamesCollection(List<XmlSchemaObjectTable.XmlSchemaObjectEntry> entries, int size)
			{
				this.entries = entries;
				this.size = size;
			}

			// Token: 0x17000B91 RID: 2961
			// (get) Token: 0x06003BB7 RID: 15287 RVA: 0x0014E89B File Offset: 0x0014CA9B
			public int Count
			{
				get
				{
					return this.size;
				}
			}

			// Token: 0x17000B92 RID: 2962
			// (get) Token: 0x06003BB8 RID: 15288 RVA: 0x0014E8A3 File Offset: 0x0014CAA3
			public object SyncRoot
			{
				get
				{
					return ((ICollection)this.entries).SyncRoot;
				}
			}

			// Token: 0x17000B93 RID: 2963
			// (get) Token: 0x06003BB9 RID: 15289 RVA: 0x0014E8B0 File Offset: 0x0014CAB0
			public bool IsSynchronized
			{
				get
				{
					return ((ICollection)this.entries).IsSynchronized;
				}
			}

			// Token: 0x06003BBA RID: 15290 RVA: 0x0014E8C0 File Offset: 0x0014CAC0
			public void CopyTo(Array array, int arrayIndex)
			{
				if (array == null)
				{
					throw new ArgumentNullException("array");
				}
				if (arrayIndex < 0)
				{
					throw new ArgumentOutOfRangeException("arrayIndex");
				}
				for (int i = 0; i < this.size; i++)
				{
					array.SetValue(this.entries[i].qname, arrayIndex++);
				}
			}

			// Token: 0x06003BBB RID: 15291 RVA: 0x0014E918 File Offset: 0x0014CB18
			public IEnumerator GetEnumerator()
			{
				return new XmlSchemaObjectTable.XSOEnumerator(this.entries, this.size, XmlSchemaObjectTable.EnumeratorType.Keys);
			}

			// Token: 0x04002B91 RID: 11153
			private List<XmlSchemaObjectTable.XmlSchemaObjectEntry> entries;

			// Token: 0x04002B92 RID: 11154
			private int size;
		}

		// Token: 0x020005D1 RID: 1489
		internal class ValuesCollection : ICollection, IEnumerable
		{
			// Token: 0x06003BBC RID: 15292 RVA: 0x0014E92C File Offset: 0x0014CB2C
			internal ValuesCollection(List<XmlSchemaObjectTable.XmlSchemaObjectEntry> entries, int size)
			{
				this.entries = entries;
				this.size = size;
			}

			// Token: 0x17000B94 RID: 2964
			// (get) Token: 0x06003BBD RID: 15293 RVA: 0x0014E942 File Offset: 0x0014CB42
			public int Count
			{
				get
				{
					return this.size;
				}
			}

			// Token: 0x17000B95 RID: 2965
			// (get) Token: 0x06003BBE RID: 15294 RVA: 0x0014E94A File Offset: 0x0014CB4A
			public object SyncRoot
			{
				get
				{
					return ((ICollection)this.entries).SyncRoot;
				}
			}

			// Token: 0x17000B96 RID: 2966
			// (get) Token: 0x06003BBF RID: 15295 RVA: 0x0014E957 File Offset: 0x0014CB57
			public bool IsSynchronized
			{
				get
				{
					return ((ICollection)this.entries).IsSynchronized;
				}
			}

			// Token: 0x06003BC0 RID: 15296 RVA: 0x0014E964 File Offset: 0x0014CB64
			public void CopyTo(Array array, int arrayIndex)
			{
				if (array == null)
				{
					throw new ArgumentNullException("array");
				}
				if (arrayIndex < 0)
				{
					throw new ArgumentOutOfRangeException("arrayIndex");
				}
				for (int i = 0; i < this.size; i++)
				{
					array.SetValue(this.entries[i].xso, arrayIndex++);
				}
			}

			// Token: 0x06003BC1 RID: 15297 RVA: 0x0014E9BC File Offset: 0x0014CBBC
			public IEnumerator GetEnumerator()
			{
				return new XmlSchemaObjectTable.XSOEnumerator(this.entries, this.size, XmlSchemaObjectTable.EnumeratorType.Values);
			}

			// Token: 0x04002B93 RID: 11155
			private List<XmlSchemaObjectTable.XmlSchemaObjectEntry> entries;

			// Token: 0x04002B94 RID: 11156
			private int size;
		}

		// Token: 0x020005D2 RID: 1490
		internal class XSOEnumerator : IEnumerator
		{
			// Token: 0x06003BC2 RID: 15298 RVA: 0x0014E9D0 File Offset: 0x0014CBD0
			internal XSOEnumerator(List<XmlSchemaObjectTable.XmlSchemaObjectEntry> entries, int size, XmlSchemaObjectTable.EnumeratorType enumType)
			{
				this.entries = entries;
				this.size = size;
				this.enumType = enumType;
				this.currentIndex = -1;
			}

			// Token: 0x17000B97 RID: 2967
			// (get) Token: 0x06003BC3 RID: 15299 RVA: 0x0014E9F4 File Offset: 0x0014CBF4
			public object Current
			{
				get
				{
					if (this.currentIndex == -1)
					{
						throw new InvalidOperationException(Res.GetString("Enumeration has not started. Call MoveNext.", new object[]
						{
							string.Empty
						}));
					}
					if (this.currentIndex >= this.size)
					{
						throw new InvalidOperationException(Res.GetString("Enumeration has already finished.", new object[]
						{
							string.Empty
						}));
					}
					switch (this.enumType)
					{
					case XmlSchemaObjectTable.EnumeratorType.Keys:
						return this.currentKey;
					case XmlSchemaObjectTable.EnumeratorType.Values:
						return this.currentValue;
					case XmlSchemaObjectTable.EnumeratorType.DictionaryEntry:
						return new DictionaryEntry(this.currentKey, this.currentValue);
					default:
						return null;
					}
				}
			}

			// Token: 0x06003BC4 RID: 15300 RVA: 0x0014EA98 File Offset: 0x0014CC98
			public bool MoveNext()
			{
				if (this.currentIndex >= this.size - 1)
				{
					this.currentValue = null;
					this.currentKey = null;
					return false;
				}
				this.currentIndex++;
				this.currentValue = this.entries[this.currentIndex].xso;
				this.currentKey = this.entries[this.currentIndex].qname;
				return true;
			}

			// Token: 0x06003BC5 RID: 15301 RVA: 0x0014EB0C File Offset: 0x0014CD0C
			public void Reset()
			{
				this.currentIndex = -1;
				this.currentValue = null;
				this.currentKey = null;
			}

			// Token: 0x04002B95 RID: 11157
			private List<XmlSchemaObjectTable.XmlSchemaObjectEntry> entries;

			// Token: 0x04002B96 RID: 11158
			private XmlSchemaObjectTable.EnumeratorType enumType;

			// Token: 0x04002B97 RID: 11159
			protected int currentIndex;

			// Token: 0x04002B98 RID: 11160
			protected int size;

			// Token: 0x04002B99 RID: 11161
			protected XmlQualifiedName currentKey;

			// Token: 0x04002B9A RID: 11162
			protected XmlSchemaObject currentValue;
		}

		// Token: 0x020005D3 RID: 1491
		internal class XSODictionaryEnumerator : XmlSchemaObjectTable.XSOEnumerator, IDictionaryEnumerator, IEnumerator
		{
			// Token: 0x06003BC6 RID: 15302 RVA: 0x0014EB23 File Offset: 0x0014CD23
			internal XSODictionaryEnumerator(List<XmlSchemaObjectTable.XmlSchemaObjectEntry> entries, int size, XmlSchemaObjectTable.EnumeratorType enumType) : base(entries, size, enumType)
			{
			}

			// Token: 0x17000B98 RID: 2968
			// (get) Token: 0x06003BC7 RID: 15303 RVA: 0x0014EB30 File Offset: 0x0014CD30
			public DictionaryEntry Entry
			{
				get
				{
					if (this.currentIndex == -1)
					{
						throw new InvalidOperationException(Res.GetString("Enumeration has not started. Call MoveNext.", new object[]
						{
							string.Empty
						}));
					}
					if (this.currentIndex >= this.size)
					{
						throw new InvalidOperationException(Res.GetString("Enumeration has already finished.", new object[]
						{
							string.Empty
						}));
					}
					return new DictionaryEntry(this.currentKey, this.currentValue);
				}
			}

			// Token: 0x17000B99 RID: 2969
			// (get) Token: 0x06003BC8 RID: 15304 RVA: 0x0014EBA4 File Offset: 0x0014CDA4
			public object Key
			{
				get
				{
					if (this.currentIndex == -1)
					{
						throw new InvalidOperationException(Res.GetString("Enumeration has not started. Call MoveNext.", new object[]
						{
							string.Empty
						}));
					}
					if (this.currentIndex >= this.size)
					{
						throw new InvalidOperationException(Res.GetString("Enumeration has already finished.", new object[]
						{
							string.Empty
						}));
					}
					return this.currentKey;
				}
			}

			// Token: 0x17000B9A RID: 2970
			// (get) Token: 0x06003BC9 RID: 15305 RVA: 0x0014EC0C File Offset: 0x0014CE0C
			public object Value
			{
				get
				{
					if (this.currentIndex == -1)
					{
						throw new InvalidOperationException(Res.GetString("Enumeration has not started. Call MoveNext.", new object[]
						{
							string.Empty
						}));
					}
					if (this.currentIndex >= this.size)
					{
						throw new InvalidOperationException(Res.GetString("Enumeration has already finished.", new object[]
						{
							string.Empty
						}));
					}
					return this.currentValue;
				}
			}
		}
	}
}
