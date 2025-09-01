using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml;

namespace System.Runtime.Serialization.Json
{
	/// <summary>Specifies the interface for initializing a JavaScript Object Notation (JSON) reader when reusing them to read from a particular stream or buffer.</summary>
	// Token: 0x02000167 RID: 359
	[TypeForwardedFrom("System.ServiceModel.Web, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")]
	public interface IXmlJsonReaderInitializer
	{
		/// <summary>Reinitializes a JavaScript Object Notation (JSON) enabled reader to a specified buffer that contains JSON-encoded data.</summary>
		/// <param name="buffer">The input <see cref="T:System.Byte" /> buffer array from which to read.</param>
		/// <param name="offset">The starting position from which to read in <paramref name="buffer" />.</param>
		/// <param name="count">The number of bytes that can be read from <paramref name="buffer" />.</param>
		/// <param name="encoding">The <see cref="T:System.Text.Encoding" /> used by the reader.</param>
		/// <param name="quotas">The <see cref="T:System.Xml.XmlDictionaryReaderQuotas" /> to apply.</param>
		/// <param name="onClose">The <see cref="T:System.Xml.OnXmlDictionaryReaderClose" /> delegate to call when the reader is closed.</param>
		// Token: 0x060012FF RID: 4863
		void SetInput(byte[] buffer, int offset, int count, Encoding encoding, XmlDictionaryReaderQuotas quotas, OnXmlDictionaryReaderClose onClose);

		/// <summary>Reinitializes a JavaScript Object Notation (JSON) enabled reader to a specified stream that contains JSON-encoded data.</summary>
		/// <param name="stream">The input <see cref="T:System.IO.Stream" /> from which to read.</param>
		/// <param name="encoding">The <see cref="T:System.Text.Encoding" /> used by the reader.</param>
		/// <param name="quotas">
		///   <see cref="T:System.Xml.XmlDictionaryReaderQuotas" /> to apply.</param>
		/// <param name="onClose">Delegate to call when the reader is closed.</param>
		// Token: 0x06001300 RID: 4864
		void SetInput(Stream stream, Encoding encoding, XmlDictionaryReaderQuotas quotas, OnXmlDictionaryReaderClose onClose);
	}
}
