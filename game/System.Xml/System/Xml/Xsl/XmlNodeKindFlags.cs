using System;

namespace System.Xml.Xsl
{
	// Token: 0x02000335 RID: 821
	[Flags]
	internal enum XmlNodeKindFlags
	{
		// Token: 0x04001BB9 RID: 7097
		None = 0,
		// Token: 0x04001BBA RID: 7098
		Document = 1,
		// Token: 0x04001BBB RID: 7099
		Element = 2,
		// Token: 0x04001BBC RID: 7100
		Attribute = 4,
		// Token: 0x04001BBD RID: 7101
		Text = 8,
		// Token: 0x04001BBE RID: 7102
		Comment = 16,
		// Token: 0x04001BBF RID: 7103
		PI = 32,
		// Token: 0x04001BC0 RID: 7104
		Namespace = 64,
		// Token: 0x04001BC1 RID: 7105
		Content = 58,
		// Token: 0x04001BC2 RID: 7106
		Any = 127
	}
}
