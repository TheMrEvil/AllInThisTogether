using System;

namespace System.Xml.Linq
{
	/// <summary>Specifies whether to omit duplicate namespaces when loading an <see cref="T:System.Xml.Linq.XDocument" /> with an <see cref="T:System.Xml.XmlReader" />.</summary>
	// Token: 0x0200004C RID: 76
	[Flags]
	public enum ReaderOptions
	{
		/// <summary>No reader options specified.</summary>
		// Token: 0x04000180 RID: 384
		None = 0,
		/// <summary>Omit duplicate namespaces when loading the <see cref="T:System.Xml.Linq.XDocument" />.</summary>
		// Token: 0x04000181 RID: 385
		OmitDuplicateNamespaces = 1
	}
}
