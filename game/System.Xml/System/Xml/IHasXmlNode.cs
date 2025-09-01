using System;

namespace System.Xml
{
	/// <summary>Enables a class to return an <see cref="T:System.Xml.XmlNode" /> from the current context or position.</summary>
	// Token: 0x020001E1 RID: 481
	public interface IHasXmlNode
	{
		/// <summary>Returns the <see cref="T:System.Xml.XmlNode" /> for the current position.</summary>
		/// <returns>The <see langword="XmlNode" /> for the current position.</returns>
		// Token: 0x0600131A RID: 4890
		XmlNode GetNode();
	}
}
