using System;
using System.IO;

namespace System.Xml
{
	/// <summary>Contains properties and methods that when implemented by a <see cref="T:System.Xml.XmlDictionaryWriter" />, allows processing of XML fragments.</summary>
	// Token: 0x0200001C RID: 28
	public interface IFragmentCapableXmlDictionaryWriter
	{
		/// <summary>Gets a value that indicates whether this <see cref="T:System.Xml.XmlDictionaryWriter" /> can process XML fragments.</summary>
		/// <returns>
		///   <see langword="true" /> if this <see cref="T:System.Xml.XmlDictionaryWriter" /> can process XML fragments; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600008B RID: 139
		bool CanFragment { get; }

		/// <summary>Starts the processing of an XML fragment.</summary>
		/// <param name="stream">The stream to write to.</param>
		/// <param name="generateSelfContainedTextFragment">If <see langword="true" />, any namespaces declared outside the fragment is declared again if used inside of it; if <see langword="false" /> the namespaces are not declared again.</param>
		// Token: 0x0600008C RID: 140
		void StartFragment(Stream stream, bool generateSelfContainedTextFragment);

		/// <summary>Ends the processing of an XML fragment.</summary>
		// Token: 0x0600008D RID: 141
		void EndFragment();

		/// <summary>Writes an XML fragment to the underlying stream of the writer.</summary>
		/// <param name="buffer">The buffer to write to.</param>
		/// <param name="offset">The starting position from which to write in <paramref name="buffer" />.</param>
		/// <param name="count">The number of bytes to be written to the <paramref name="buffer" />.</param>
		// Token: 0x0600008E RID: 142
		void WriteFragment(byte[] buffer, int offset, int count);
	}
}
