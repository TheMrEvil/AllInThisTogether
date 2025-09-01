using System;
using System.Xml.Schema;

namespace System.Xml.XPath
{
	// Token: 0x0200025F RID: 607
	internal class XPathNavigatorReaderWithSI : XPathNavigatorReader, IXmlSchemaInfo
	{
		// Token: 0x060016CA RID: 5834 RVA: 0x000884C9 File Offset: 0x000866C9
		internal XPathNavigatorReaderWithSI(XPathNavigator navToRead, IXmlLineInfo xli, IXmlSchemaInfo xsi) : base(navToRead, xli, xsi)
		{
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x060016CB RID: 5835 RVA: 0x000884D4 File Offset: 0x000866D4
		public virtual XmlSchemaValidity Validity
		{
			get
			{
				if (!base.IsReading)
				{
					return XmlSchemaValidity.NotKnown;
				}
				return this.schemaInfo.Validity;
			}
		}

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x060016CC RID: 5836 RVA: 0x000884EB File Offset: 0x000866EB
		public override bool IsDefault
		{
			get
			{
				return base.IsReading && this.schemaInfo.IsDefault;
			}
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x060016CD RID: 5837 RVA: 0x00088502 File Offset: 0x00086702
		public virtual bool IsNil
		{
			get
			{
				return base.IsReading && this.schemaInfo.IsNil;
			}
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x060016CE RID: 5838 RVA: 0x00088519 File Offset: 0x00086719
		public virtual XmlSchemaSimpleType MemberType
		{
			get
			{
				if (!base.IsReading)
				{
					return null;
				}
				return this.schemaInfo.MemberType;
			}
		}

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x060016CF RID: 5839 RVA: 0x00088530 File Offset: 0x00086730
		public virtual XmlSchemaType SchemaType
		{
			get
			{
				if (!base.IsReading)
				{
					return null;
				}
				return this.schemaInfo.SchemaType;
			}
		}

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x060016D0 RID: 5840 RVA: 0x00088547 File Offset: 0x00086747
		public virtual XmlSchemaElement SchemaElement
		{
			get
			{
				if (!base.IsReading)
				{
					return null;
				}
				return this.schemaInfo.SchemaElement;
			}
		}

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x060016D1 RID: 5841 RVA: 0x0008855E File Offset: 0x0008675E
		public virtual XmlSchemaAttribute SchemaAttribute
		{
			get
			{
				if (!base.IsReading)
				{
					return null;
				}
				return this.schemaInfo.SchemaAttribute;
			}
		}
	}
}
