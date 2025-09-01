using System;

namespace System.Xml.Serialization
{
	/// <summary>Establishes a <see cref="P:System.Xml.Serialization.IXmlTextParser.Normalized" /> property for use by the .NET Framework infrastructure.</summary>
	// Token: 0x0200027C RID: 636
	public interface IXmlTextParser
	{
		/// <summary>Gets or sets whether white space and attribute values are normalized.</summary>
		/// <returns>
		///     <see langword="true" /> if white space attributes values are normalized; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x06001810 RID: 6160
		// (set) Token: 0x06001811 RID: 6161
		bool Normalized { get; set; }

		/// <summary>Gets or sets how white space is handled when parsing XML.</summary>
		/// <returns>A member of the <see cref="T:System.Xml.WhitespaceHandling" /> enumeration that describes how whites pace is handled when parsing XML.</returns>
		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x06001812 RID: 6162
		// (set) Token: 0x06001813 RID: 6163
		WhitespaceHandling WhitespaceHandling { get; set; }
	}
}
