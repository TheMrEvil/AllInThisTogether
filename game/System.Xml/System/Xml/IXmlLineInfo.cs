using System;

namespace System.Xml
{
	/// <summary>Provides an interface to enable a class to return line and position information.</summary>
	// Token: 0x020001E2 RID: 482
	public interface IXmlLineInfo
	{
		/// <summary>Gets a value indicating whether the class can return line information.</summary>
		/// <returns>
		///     <see langword="true" /> if <see cref="P:System.Xml.IXmlLineInfo.LineNumber" /> and <see cref="P:System.Xml.IXmlLineInfo.LinePosition" /> can be provided; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600131B RID: 4891
		bool HasLineInfo();

		/// <summary>Gets the current line number.</summary>
		/// <returns>The current line number or 0 if no line information is available (for example, <see cref="M:System.Xml.IXmlLineInfo.HasLineInfo" /> returns <see langword="false" />).</returns>
		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x0600131C RID: 4892
		int LineNumber { get; }

		/// <summary>Gets the current line position.</summary>
		/// <returns>The current line position or 0 if no line information is available (for example, <see cref="M:System.Xml.IXmlLineInfo.HasLineInfo" /> returns <see langword="false" />).</returns>
		// Token: 0x170003AA RID: 938
		// (get) Token: 0x0600131D RID: 4893
		int LinePosition { get; }
	}
}
