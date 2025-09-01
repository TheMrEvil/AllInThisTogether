using System;
using System.Collections;
using System.Collections.Specialized;
using System.Xml.Schema;

namespace System.Xml.Serialization
{
	// Token: 0x0200027E RID: 638
	internal class SchemaObjectCache
	{
		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x0600181C RID: 6172 RVA: 0x0008D8F8 File Offset: 0x0008BAF8
		private Hashtable Graph
		{
			get
			{
				if (this.graph == null)
				{
					this.graph = new Hashtable();
				}
				return this.graph;
			}
		}

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x0600181D RID: 6173 RVA: 0x0008D913 File Offset: 0x0008BB13
		private Hashtable Hash
		{
			get
			{
				if (this.hash == null)
				{
					this.hash = new Hashtable();
				}
				return this.hash;
			}
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x0600181E RID: 6174 RVA: 0x0008D92E File Offset: 0x0008BB2E
		private Hashtable ObjectCache
		{
			get
			{
				if (this.objectCache == null)
				{
					this.objectCache = new Hashtable();
				}
				return this.objectCache;
			}
		}

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x0600181F RID: 6175 RVA: 0x0008D949 File Offset: 0x0008BB49
		internal StringCollection Warnings
		{
			get
			{
				if (this.warnings == null)
				{
					this.warnings = new StringCollection();
				}
				return this.warnings;
			}
		}

		// Token: 0x06001820 RID: 6176 RVA: 0x0008D964 File Offset: 0x0008BB64
		internal XmlSchemaObject AddItem(XmlSchemaObject item, XmlQualifiedName qname, XmlSchemas schemas)
		{
			if (item == null)
			{
				return null;
			}
			if (qname == null || qname.IsEmpty)
			{
				return null;
			}
			string key = item.GetType().Name + ":" + qname.ToString();
			ArrayList arrayList = (ArrayList)this.ObjectCache[key];
			if (arrayList == null)
			{
				arrayList = new ArrayList();
				this.ObjectCache[key] = arrayList;
			}
			for (int i = 0; i < arrayList.Count; i++)
			{
				XmlSchemaObject xmlSchemaObject = (XmlSchemaObject)arrayList[i];
				if (xmlSchemaObject == item)
				{
					return xmlSchemaObject;
				}
				if (this.Match(xmlSchemaObject, item, true))
				{
					return xmlSchemaObject;
				}
				this.Warnings.Add(Res.GetString("Warning: Cannot share {0} named '{1}' from '{2}' namespace. Several mismatched schema declarations were found.", new object[]
				{
					item.GetType().Name,
					qname.Name,
					qname.Namespace
				}));
				this.Warnings.Add("DEBUG:Cached item key:\r\n" + (string)this.looks[xmlSchemaObject] + "\r\nnew item key:\r\n" + (string)this.looks[item]);
			}
			arrayList.Add(item);
			return item;
		}

		// Token: 0x06001821 RID: 6177 RVA: 0x0008DA88 File Offset: 0x0008BC88
		internal bool Match(XmlSchemaObject o1, XmlSchemaObject o2, bool shareTypes)
		{
			if (o1 == o2)
			{
				return true;
			}
			if (o1.GetType() != o2.GetType())
			{
				return false;
			}
			if (this.Hash[o1] == null)
			{
				this.Hash[o1] = this.GetHash(o1);
			}
			int num = (int)this.Hash[o1];
			int num2 = this.GetHash(o2);
			return num == num2 && (!shareTypes || this.CompositeHash(o1, num) == this.CompositeHash(o2, num2));
		}

		// Token: 0x06001822 RID: 6178 RVA: 0x0008DB10 File Offset: 0x0008BD10
		private ArrayList GetDependencies(XmlSchemaObject o, ArrayList deps, Hashtable refs)
		{
			if (refs[o] == null)
			{
				refs[o] = o;
				deps.Add(o);
				ArrayList arrayList = this.Graph[o] as ArrayList;
				if (arrayList != null)
				{
					for (int i = 0; i < arrayList.Count; i++)
					{
						this.GetDependencies((XmlSchemaObject)arrayList[i], deps, refs);
					}
				}
			}
			return deps;
		}

		// Token: 0x06001823 RID: 6179 RVA: 0x0008DB74 File Offset: 0x0008BD74
		private int CompositeHash(XmlSchemaObject o, int hash)
		{
			ArrayList dependencies = this.GetDependencies(o, new ArrayList(), new Hashtable());
			double num = 0.0;
			for (int i = 0; i < dependencies.Count; i++)
			{
				object obj = this.Hash[dependencies[i]];
				if (obj is int)
				{
					num += (double)((int)obj / dependencies.Count);
				}
			}
			return (int)num;
		}

		// Token: 0x06001824 RID: 6180 RVA: 0x0008DBDC File Offset: 0x0008BDDC
		internal void GenerateSchemaGraph(XmlSchemas schemas)
		{
			ArrayList items = new SchemaGraph(this.Graph, schemas).GetItems();
			for (int i = 0; i < items.Count; i++)
			{
				this.GetHash((XmlSchemaObject)items[i]);
			}
		}

		// Token: 0x06001825 RID: 6181 RVA: 0x0008DC20 File Offset: 0x0008BE20
		private int GetHash(XmlSchemaObject o)
		{
			object obj = this.Hash[o];
			if (obj != null && !(obj is XmlSchemaObject))
			{
				return (int)obj;
			}
			string text = this.ToString(o, new SchemaObjectWriter());
			this.looks[o] = text;
			int hashCode = text.GetHashCode();
			this.Hash[o] = hashCode;
			return hashCode;
		}

		// Token: 0x06001826 RID: 6182 RVA: 0x0008DC80 File Offset: 0x0008BE80
		private string ToString(XmlSchemaObject o, SchemaObjectWriter writer)
		{
			return writer.WriteXmlSchemaObject(o);
		}

		// Token: 0x06001827 RID: 6183 RVA: 0x0008DC89 File Offset: 0x0008BE89
		public SchemaObjectCache()
		{
		}

		// Token: 0x040018A6 RID: 6310
		private Hashtable graph;

		// Token: 0x040018A7 RID: 6311
		private Hashtable hash;

		// Token: 0x040018A8 RID: 6312
		private Hashtable objectCache;

		// Token: 0x040018A9 RID: 6313
		private StringCollection warnings;

		// Token: 0x040018AA RID: 6314
		internal Hashtable looks = new Hashtable();
	}
}
