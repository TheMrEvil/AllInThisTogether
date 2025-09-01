using System;
using System.IO;
using System.Text;

namespace System.Xml
{
	/// <summary>When implemented by an MTOM writer, this interface ensures initialization for an MTOM writer.</summary>
	// Token: 0x02000082 RID: 130
	public interface IXmlMtomWriterInitializer
	{
		/// <summary>When implemented by an MTOM writer, initializes an MTOM writer.</summary>
		/// <param name="stream">The stream to write to.</param>
		/// <param name="encoding">The character encoding of the stream.</param>
		/// <param name="maxSizeInBytes">The maximum number of bytes that are buffered in the writer.</param>
		/// <param name="startInfo">An attribute in the ContentType SOAP header, set to "Application/soap+xml".</param>
		/// <param name="boundary">The MIME boundary string.</param>
		/// <param name="startUri">The URI for MIME section.</param>
		/// <param name="writeMessageHeaders">If <see langword="true" />, write message headers.</param>
		/// <param name="ownsStream">
		///   <see langword="true" /> to indicate the stream is closed by the writer when done; otherwise, <see langword="false" />.</param>
		// Token: 0x060006E9 RID: 1769
		void SetOutput(Stream stream, Encoding encoding, int maxSizeInBytes, string startInfo, string boundary, string startUri, bool writeMessageHeaders, bool ownsStream);
	}
}
