using System;

namespace System.Xml
{
	/// <summary>Specifies whether to remove duplicate namespace declarations in the <see cref="T:System.Xml.XmlWriter" />. </summary>
	// Token: 0x02000045 RID: 69
	[Flags]
	public enum NamespaceHandling
	{
		/// <summary>Specifies that duplicate namespace declarations will not be removed.</summary>
		// Token: 0x0400060D RID: 1549
		Default = 0,
		/// <summary>Specifies that duplicate namespace declarations will be removed. For the duplicate namespace to be removed, the prefix and the namespace must match.</summary>
		// Token: 0x0400060E RID: 1550
		OmitDuplicates = 1
	}
}
