using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	// Token: 0x020005E3 RID: 1507
	internal class XmlSchemaSubstitutionGroupV1Compat : XmlSchemaSubstitutionGroup
	{
		// Token: 0x17000BCF RID: 3023
		// (get) Token: 0x06003C52 RID: 15442 RVA: 0x0015121B File Offset: 0x0014F41B
		[XmlIgnore]
		internal XmlSchemaChoice Choice
		{
			get
			{
				return this.choice;
			}
		}

		// Token: 0x06003C53 RID: 15443 RVA: 0x00151223 File Offset: 0x0014F423
		public XmlSchemaSubstitutionGroupV1Compat()
		{
		}

		// Token: 0x04002BD1 RID: 11217
		private XmlSchemaChoice choice = new XmlSchemaChoice();
	}
}
