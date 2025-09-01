using System;
using System.Xml.Schema;

namespace System.Xml
{
	// Token: 0x020001CC RID: 460
	internal sealed class XmlNameEx : XmlName
	{
		// Token: 0x060011BA RID: 4538 RVA: 0x0006C77C File Offset: 0x0006A97C
		internal XmlNameEx(string prefix, string localName, string ns, int hashCode, XmlDocument ownerDoc, XmlName next, IXmlSchemaInfo schemaInfo) : base(prefix, localName, ns, hashCode, ownerDoc, next)
		{
			this.SetValidity(schemaInfo.Validity);
			this.SetIsDefault(schemaInfo.IsDefault);
			this.SetIsNil(schemaInfo.IsNil);
			this.memberType = schemaInfo.MemberType;
			this.schemaType = schemaInfo.SchemaType;
			this.decl = ((schemaInfo.SchemaElement != null) ? schemaInfo.SchemaElement : schemaInfo.SchemaAttribute);
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x060011BB RID: 4539 RVA: 0x0006C7F8 File Offset: 0x0006A9F8
		public override XmlSchemaValidity Validity
		{
			get
			{
				if (!this.ownerDoc.CanReportValidity)
				{
					return XmlSchemaValidity.NotKnown;
				}
				return (XmlSchemaValidity)(this.flags & 3);
			}
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x060011BC RID: 4540 RVA: 0x0006C811 File Offset: 0x0006AA11
		public override bool IsDefault
		{
			get
			{
				return (this.flags & 4) > 0;
			}
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x060011BD RID: 4541 RVA: 0x0006C81E File Offset: 0x0006AA1E
		public override bool IsNil
		{
			get
			{
				return (this.flags & 8) > 0;
			}
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x060011BE RID: 4542 RVA: 0x0006C82B File Offset: 0x0006AA2B
		public override XmlSchemaSimpleType MemberType
		{
			get
			{
				return this.memberType;
			}
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x060011BF RID: 4543 RVA: 0x0006C833 File Offset: 0x0006AA33
		public override XmlSchemaType SchemaType
		{
			get
			{
				return this.schemaType;
			}
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x060011C0 RID: 4544 RVA: 0x0006C83B File Offset: 0x0006AA3B
		public override XmlSchemaElement SchemaElement
		{
			get
			{
				return this.decl as XmlSchemaElement;
			}
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x060011C1 RID: 4545 RVA: 0x0006C848 File Offset: 0x0006AA48
		public override XmlSchemaAttribute SchemaAttribute
		{
			get
			{
				return this.decl as XmlSchemaAttribute;
			}
		}

		// Token: 0x060011C2 RID: 4546 RVA: 0x0006C855 File Offset: 0x0006AA55
		public void SetValidity(XmlSchemaValidity value)
		{
			this.flags = (byte)(((int)this.flags & -4) | (int)((byte)value));
		}

		// Token: 0x060011C3 RID: 4547 RVA: 0x0006C86A File Offset: 0x0006AA6A
		public void SetIsDefault(bool value)
		{
			if (value)
			{
				this.flags |= 4;
				return;
			}
			this.flags = (byte)((int)this.flags & -5);
		}

		// Token: 0x060011C4 RID: 4548 RVA: 0x0006C88F File Offset: 0x0006AA8F
		public void SetIsNil(bool value)
		{
			if (value)
			{
				this.flags |= 8;
				return;
			}
			this.flags = (byte)((int)this.flags & -9);
		}

		// Token: 0x060011C5 RID: 4549 RVA: 0x0006C8B4 File Offset: 0x0006AAB4
		public override bool Equals(IXmlSchemaInfo schemaInfo)
		{
			return schemaInfo != null && schemaInfo.Validity == (XmlSchemaValidity)(this.flags & 3) && schemaInfo.IsDefault == (this.flags & 4) > 0 && schemaInfo.IsNil == (this.flags & 8) > 0 && schemaInfo.MemberType == this.memberType && schemaInfo.SchemaType == this.schemaType && schemaInfo.SchemaElement == this.decl as XmlSchemaElement && schemaInfo.SchemaAttribute == this.decl as XmlSchemaAttribute;
		}

		// Token: 0x040010AB RID: 4267
		private byte flags;

		// Token: 0x040010AC RID: 4268
		private XmlSchemaSimpleType memberType;

		// Token: 0x040010AD RID: 4269
		private XmlSchemaType schemaType;

		// Token: 0x040010AE RID: 4270
		private object decl;

		// Token: 0x040010AF RID: 4271
		private const byte ValidityMask = 3;

		// Token: 0x040010B0 RID: 4272
		private const byte IsDefaultBit = 4;

		// Token: 0x040010B1 RID: 4273
		private const byte IsNilBit = 8;
	}
}
