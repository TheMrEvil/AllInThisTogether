using System;

namespace System.Xml.Serialization
{
	// Token: 0x020002CB RID: 715
	internal enum XmlAttributeFlags
	{
		// Token: 0x040019CC RID: 6604
		Enum = 1,
		// Token: 0x040019CD RID: 6605
		Array,
		// Token: 0x040019CE RID: 6606
		Text = 4,
		// Token: 0x040019CF RID: 6607
		ArrayItems = 8,
		// Token: 0x040019D0 RID: 6608
		Elements = 16,
		// Token: 0x040019D1 RID: 6609
		Attribute = 32,
		// Token: 0x040019D2 RID: 6610
		Root = 64,
		// Token: 0x040019D3 RID: 6611
		Type = 128,
		// Token: 0x040019D4 RID: 6612
		AnyElements = 256,
		// Token: 0x040019D5 RID: 6613
		AnyAttribute = 512,
		// Token: 0x040019D6 RID: 6614
		ChoiceIdentifier = 1024,
		// Token: 0x040019D7 RID: 6615
		XmlnsDeclarations = 2048
	}
}
