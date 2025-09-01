using System;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x020006C2 RID: 1730
	[Serializable]
	internal enum SoapAttributeType
	{
		// Token: 0x040029E9 RID: 10729
		None,
		// Token: 0x040029EA RID: 10730
		SchemaType,
		// Token: 0x040029EB RID: 10731
		Embedded,
		// Token: 0x040029EC RID: 10732
		XmlElement = 4,
		// Token: 0x040029ED RID: 10733
		XmlAttribute = 8
	}
}
