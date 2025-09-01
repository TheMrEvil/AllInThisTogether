using System;

namespace System.Xml.Linq
{
	/// <summary>Specifies serialization options.</summary>
	// Token: 0x0200004B RID: 75
	[Flags]
	public enum SaveOptions
	{
		/// <summary>Format (indent) the XML while serializing.</summary>
		// Token: 0x0400017C RID: 380
		None = 0,
		/// <summary>Preserve all insignificant white space while serializing.</summary>
		// Token: 0x0400017D RID: 381
		DisableFormatting = 1,
		/// <summary>Remove the duplicate namespace declarations while serializing.</summary>
		// Token: 0x0400017E RID: 382
		OmitDuplicateNamespaces = 2
	}
}
