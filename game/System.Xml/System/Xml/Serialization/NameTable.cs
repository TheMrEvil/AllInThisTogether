using System;
using System.Collections;

namespace System.Xml.Serialization
{
	// Token: 0x020002A1 RID: 673
	internal class NameTable : INameScope
	{
		// Token: 0x06001930 RID: 6448 RVA: 0x000907FC File Offset: 0x0008E9FC
		internal void Add(XmlQualifiedName qname, object value)
		{
			this.Add(qname.Name, qname.Namespace, value);
		}

		// Token: 0x06001931 RID: 6449 RVA: 0x00090814 File Offset: 0x0008EA14
		internal void Add(string name, string ns, object value)
		{
			NameKey key = new NameKey(name, ns);
			this.table.Add(key, value);
		}

		// Token: 0x170004DC RID: 1244
		internal object this[XmlQualifiedName qname]
		{
			get
			{
				return this.table[new NameKey(qname.Name, qname.Namespace)];
			}
			set
			{
				this.table[new NameKey(qname.Name, qname.Namespace)] = value;
			}
		}

		// Token: 0x170004DD RID: 1245
		internal object this[string name, string ns]
		{
			get
			{
				return this.table[new NameKey(name, ns)];
			}
			set
			{
				this.table[new NameKey(name, ns)] = value;
			}
		}

		// Token: 0x170004DE RID: 1246
		object INameScope.this[string name, string ns]
		{
			get
			{
				return this.table[new NameKey(name, ns)];
			}
			set
			{
				this.table[new NameKey(name, ns)] = value;
			}
		}

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x06001938 RID: 6456 RVA: 0x0009089C File Offset: 0x0008EA9C
		internal ICollection Values
		{
			get
			{
				return this.table.Values;
			}
		}

		// Token: 0x06001939 RID: 6457 RVA: 0x000908AC File Offset: 0x0008EAAC
		internal Array ToArray(Type type)
		{
			Array array = Array.CreateInstance(type, this.table.Count);
			this.table.Values.CopyTo(array, 0);
			return array;
		}

		// Token: 0x0600193A RID: 6458 RVA: 0x000908DE File Offset: 0x0008EADE
		public NameTable()
		{
		}

		// Token: 0x0400191C RID: 6428
		private Hashtable table = new Hashtable();
	}
}
