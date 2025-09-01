using System;
using System.IO;

namespace System.Xml
{
	/// <summary>Specifies implementation requirements for XML binary writers that derive from this interface.</summary>
	// Token: 0x0200004D RID: 77
	public interface IXmlBinaryWriterInitializer
	{
		/// <summary>Specifies initialization requirements for XML binary writers that implement this method.</summary>
		/// <param name="stream">The stream to write to.</param>
		/// <param name="dictionary">The <see cref="T:System.Xml.XmlDictionary" /> to use.</param>
		/// <param name="session">The <see cref="T:System.Xml.XmlBinaryWriterSession" /> to use.</param>
		/// <param name="ownsStream">
		///   <see langword="true" /> to indicate the stream is closed by the writer when done; otherwise, <see langword="false" />.</param>
		// Token: 0x0600031B RID: 795
		void SetOutput(Stream stream, IXmlDictionary dictionary, XmlBinaryWriterSession session, bool ownsStream);
	}
}
