using System;

namespace System.Data
{
	/// <summary>Specifies how a <see cref="T:System.Data.DataColumn" /> is mapped.</summary>
	// Token: 0x0200010D RID: 269
	public enum MappingType
	{
		/// <summary>The column is mapped to an XML element.</summary>
		// Token: 0x0400097C RID: 2428
		Element = 1,
		/// <summary>The column is mapped to an XML attribute.</summary>
		// Token: 0x0400097D RID: 2429
		Attribute,
		/// <summary>The column is mapped to an <see cref="T:System.Xml.XmlText" /> node.</summary>
		// Token: 0x0400097E RID: 2430
		SimpleContent,
		/// <summary>The column is mapped to an internal structure.</summary>
		// Token: 0x0400097F RID: 2431
		Hidden
	}
}
