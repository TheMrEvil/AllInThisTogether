using System;
using System.Collections.ObjectModel;

namespace System.Xml.Xsl.Xslt
{
	// Token: 0x020003D8 RID: 984
	internal class DecimalFormats : KeyedCollection<XmlQualifiedName, DecimalFormatDecl>
	{
		// Token: 0x06002760 RID: 10080 RVA: 0x000EABF5 File Offset: 0x000E8DF5
		protected override XmlQualifiedName GetKeyForItem(DecimalFormatDecl format)
		{
			return format.Name;
		}

		// Token: 0x06002761 RID: 10081 RVA: 0x000EABFD File Offset: 0x000E8DFD
		public DecimalFormats()
		{
		}
	}
}
