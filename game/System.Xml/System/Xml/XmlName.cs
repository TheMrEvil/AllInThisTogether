using System;
using System.Xml.Schema;

namespace System.Xml
{
	// Token: 0x020001CB RID: 459
	internal class XmlName : IXmlSchemaInfo
	{
		// Token: 0x060011A9 RID: 4521 RVA: 0x0006C5E0 File Offset: 0x0006A7E0
		public static XmlName Create(string prefix, string localName, string ns, int hashCode, XmlDocument ownerDoc, XmlName next, IXmlSchemaInfo schemaInfo)
		{
			if (schemaInfo == null)
			{
				return new XmlName(prefix, localName, ns, hashCode, ownerDoc, next);
			}
			return new XmlNameEx(prefix, localName, ns, hashCode, ownerDoc, next, schemaInfo);
		}

		// Token: 0x060011AA RID: 4522 RVA: 0x0006C603 File Offset: 0x0006A803
		internal XmlName(string prefix, string localName, string ns, int hashCode, XmlDocument ownerDoc, XmlName next)
		{
			this.prefix = prefix;
			this.localName = localName;
			this.ns = ns;
			this.name = null;
			this.hashCode = hashCode;
			this.ownerDoc = ownerDoc;
			this.next = next;
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x060011AB RID: 4523 RVA: 0x0006C63F File Offset: 0x0006A83F
		public string LocalName
		{
			get
			{
				return this.localName;
			}
		}

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x060011AC RID: 4524 RVA: 0x0006C647 File Offset: 0x0006A847
		public string NamespaceURI
		{
			get
			{
				return this.ns;
			}
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x060011AD RID: 4525 RVA: 0x0006C64F File Offset: 0x0006A84F
		public string Prefix
		{
			get
			{
				return this.prefix;
			}
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x060011AE RID: 4526 RVA: 0x0006C657 File Offset: 0x0006A857
		public int HashCode
		{
			get
			{
				return this.hashCode;
			}
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x060011AF RID: 4527 RVA: 0x0006C65F File Offset: 0x0006A85F
		public XmlDocument OwnerDocument
		{
			get
			{
				return this.ownerDoc;
			}
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x060011B0 RID: 4528 RVA: 0x0006C668 File Offset: 0x0006A868
		public string Name
		{
			get
			{
				if (this.name == null)
				{
					if (this.prefix.Length > 0)
					{
						if (this.localName.Length > 0)
						{
							string array = this.prefix + ":" + this.localName;
							XmlNameTable nameTable = this.ownerDoc.NameTable;
							lock (nameTable)
							{
								if (this.name == null)
								{
									this.name = this.ownerDoc.NameTable.Add(array);
								}
								goto IL_99;
							}
						}
						this.name = this.prefix;
					}
					else
					{
						this.name = this.localName;
					}
				}
				IL_99:
				return this.name;
			}
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x060011B1 RID: 4529 RVA: 0x0000D1C5 File Offset: 0x0000B3C5
		public virtual XmlSchemaValidity Validity
		{
			get
			{
				return XmlSchemaValidity.NotKnown;
			}
		}

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x060011B2 RID: 4530 RVA: 0x0000D1C5 File Offset: 0x0000B3C5
		public virtual bool IsDefault
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x060011B3 RID: 4531 RVA: 0x0000D1C5 File Offset: 0x0000B3C5
		public virtual bool IsNil
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x060011B4 RID: 4532 RVA: 0x0001DA42 File Offset: 0x0001BC42
		public virtual XmlSchemaSimpleType MemberType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x060011B5 RID: 4533 RVA: 0x0001DA42 File Offset: 0x0001BC42
		public virtual XmlSchemaType SchemaType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x060011B6 RID: 4534 RVA: 0x0001DA42 File Offset: 0x0001BC42
		public virtual XmlSchemaElement SchemaElement
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x060011B7 RID: 4535 RVA: 0x0001DA42 File Offset: 0x0001BC42
		public virtual XmlSchemaAttribute SchemaAttribute
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060011B8 RID: 4536 RVA: 0x0006C724 File Offset: 0x0006A924
		public virtual bool Equals(IXmlSchemaInfo schemaInfo)
		{
			return schemaInfo == null;
		}

		// Token: 0x060011B9 RID: 4537 RVA: 0x0006C72C File Offset: 0x0006A92C
		public static int GetHashCode(string name)
		{
			int num = 0;
			if (name != null)
			{
				for (int i = name.Length - 1; i >= 0; i--)
				{
					char c = name[i];
					if (c == ':')
					{
						break;
					}
					num += (num << 7 ^ (int)c);
				}
				num -= num >> 17;
				num -= num >> 11;
				num -= num >> 5;
			}
			return num;
		}

		// Token: 0x040010A4 RID: 4260
		private string prefix;

		// Token: 0x040010A5 RID: 4261
		private string localName;

		// Token: 0x040010A6 RID: 4262
		private string ns;

		// Token: 0x040010A7 RID: 4263
		private string name;

		// Token: 0x040010A8 RID: 4264
		private int hashCode;

		// Token: 0x040010A9 RID: 4265
		internal XmlDocument ownerDoc;

		// Token: 0x040010AA RID: 4266
		internal XmlName next;
	}
}
