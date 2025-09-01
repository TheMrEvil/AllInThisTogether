using System;
using System.Collections;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	// Token: 0x020005E2 RID: 1506
	internal class XmlSchemaSubstitutionGroup : XmlSchemaObject
	{
		// Token: 0x17000BCD RID: 3021
		// (get) Token: 0x06003C4E RID: 15438 RVA: 0x001511E4 File Offset: 0x0014F3E4
		[XmlIgnore]
		internal ArrayList Members
		{
			get
			{
				return this.membersList;
			}
		}

		// Token: 0x17000BCE RID: 3022
		// (get) Token: 0x06003C4F RID: 15439 RVA: 0x001511EC File Offset: 0x0014F3EC
		// (set) Token: 0x06003C50 RID: 15440 RVA: 0x001511F4 File Offset: 0x0014F3F4
		[XmlIgnore]
		internal XmlQualifiedName Examplar
		{
			get
			{
				return this.examplar;
			}
			set
			{
				this.examplar = value;
			}
		}

		// Token: 0x06003C51 RID: 15441 RVA: 0x001511FD File Offset: 0x0014F3FD
		public XmlSchemaSubstitutionGroup()
		{
		}

		// Token: 0x04002BCF RID: 11215
		private ArrayList membersList = new ArrayList();

		// Token: 0x04002BD0 RID: 11216
		private XmlQualifiedName examplar = XmlQualifiedName.Empty;
	}
}
