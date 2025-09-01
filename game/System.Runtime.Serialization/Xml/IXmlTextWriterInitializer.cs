using System;
using System.IO;
using System.Text;

namespace System.Xml
{
	/// <summary>Specifies implementation requirements for XML text writers that derive from this interface.</summary>
	// Token: 0x0200009C RID: 156
	public interface IXmlTextWriterInitializer
	{
		/// <summary>Specifies initialization requirements for XML text writers that implement this method.</summary>
		/// <param name="stream">The stream to write to.</param>
		/// <param name="encoding">The character encoding of the stream.</param>
		/// <param name="ownsStream">
		///   <see langword="true" /> to indicate the stream is closed by the writer when done; otherwise, <see langword="false" />.</param>
		// Token: 0x0600084C RID: 2124
		void SetOutput(Stream stream, Encoding encoding, bool ownsStream);
	}
}
