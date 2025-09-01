using System;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
using System.Text;

namespace System.Xml
{
	/// <summary>An <see langword="abstract" /> class that the Windows Communication Foundation (WCF) derives from <see cref="T:System.Xml.XmlReader" /> to do serialization and deserialization.</summary>
	// Token: 0x02000060 RID: 96
	public abstract class XmlDictionaryReader : XmlReader
	{
		/// <summary>Creates an instance of <see cref="T:System.Xml.XmlDictionaryReader" /> from an existing <see cref="T:System.Xml.XmlReader" />.</summary>
		/// <param name="reader">An instance of <see cref="T:System.Xml.XmlReader" />.</param>
		/// <returns>An instance of <see cref="T:System.Xml.XmlDictionaryReader" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="reader" /> is <see langword="null" />.</exception>
		// Token: 0x06000476 RID: 1142 RVA: 0x00016CA4 File Offset: 0x00014EA4
		public static XmlDictionaryReader CreateDictionaryReader(XmlReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			XmlDictionaryReader xmlDictionaryReader = reader as XmlDictionaryReader;
			if (xmlDictionaryReader == null)
			{
				xmlDictionaryReader = new XmlDictionaryReader.XmlWrappedReader(reader, null);
			}
			return xmlDictionaryReader;
		}

		/// <summary>Creates an instance of <see cref="T:System.Xml.XmlDictionaryReader" /> that can read .NET Binary XML Format.</summary>
		/// <param name="buffer">The buffer from which to read.</param>
		/// <param name="quotas">The quotas that apply to this operation.</param>
		/// <returns>An instance of <see cref="T:System.Xml.XmlDictionaryReader" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.</exception>
		// Token: 0x06000477 RID: 1143 RVA: 0x00016CD2 File Offset: 0x00014ED2
		public static XmlDictionaryReader CreateBinaryReader(byte[] buffer, XmlDictionaryReaderQuotas quotas)
		{
			if (buffer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("buffer");
			}
			return XmlDictionaryReader.CreateBinaryReader(buffer, 0, buffer.Length, quotas);
		}

		/// <summary>Creates an instance of <see cref="T:System.Xml.XmlDictionaryReader" /> that can read .NET Binary XML Format.</summary>
		/// <param name="buffer">The buffer from which to read.</param>
		/// <param name="offset">The starting position from which to read in <paramref name="buffer" />.</param>
		/// <param name="count">The number of bytes that can be read from <paramref name="buffer" />.</param>
		/// <param name="quotas">The quotas that apply to this operation.</param>
		/// <returns>An instance of <see cref="T:System.Xml.XmlDictionaryReader" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> is less than zero or greater than the buffer length minus the offset.
		/// -or-
		/// <paramref name="offset" /> is less than zero or greater than the buffer length.</exception>
		// Token: 0x06000478 RID: 1144 RVA: 0x00016CED File Offset: 0x00014EED
		public static XmlDictionaryReader CreateBinaryReader(byte[] buffer, int offset, int count, XmlDictionaryReaderQuotas quotas)
		{
			return XmlDictionaryReader.CreateBinaryReader(buffer, offset, count, null, quotas);
		}

		/// <summary>Creates an instance of <see cref="T:System.Xml.XmlDictionaryReader" /> that can read .NET Binary XML Format.</summary>
		/// <param name="buffer">The buffer from which to read.</param>
		/// <param name="offset">The starting position from which to read in <paramref name="buffer" />.</param>
		/// <param name="count">The number of bytes that can be read from <paramref name="buffer" />.</param>
		/// <param name="dictionary">
		///   <see cref="T:System.Xml.XmlDictionary" /> to use.</param>
		/// <param name="quotas">The quotas that apply to this operation.</param>
		/// <returns>An instance of <see cref="T:System.Xml.XmlDictionaryReader" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.
		/// -or-
		/// <paramref name="offset" /> is less than zero or greater than the buffer length.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> is less than zero or greater than the buffer length minus the offset.</exception>
		// Token: 0x06000479 RID: 1145 RVA: 0x00016CF9 File Offset: 0x00014EF9
		public static XmlDictionaryReader CreateBinaryReader(byte[] buffer, int offset, int count, IXmlDictionary dictionary, XmlDictionaryReaderQuotas quotas)
		{
			return XmlDictionaryReader.CreateBinaryReader(buffer, offset, count, dictionary, quotas, null);
		}

		/// <summary>Creates an instance of <see cref="T:System.Xml.XmlDictionaryReader" /> that can read .NET Binary XML Format.</summary>
		/// <param name="buffer">The buffer from which to read.</param>
		/// <param name="offset">The starting position from which to read in <paramref name="buffer" />.</param>
		/// <param name="count">The number of bytes that can be read from <paramref name="buffer" />.</param>
		/// <param name="dictionary">The <see cref="T:System.Xml.XmlDictionary" /> to use.</param>
		/// <param name="quotas">The <see cref="T:System.Xml.XmlDictionaryReaderQuotas" /> to apply.</param>
		/// <param name="session">The <see cref="T:System.Xml.XmlBinaryReaderSession" /> to use.</param>
		/// <returns>An instance of <see cref="T:System.Xml.XmlDictionaryReader" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> is less than zero or greater than the buffer length minus the offset.
		/// -or-
		/// <paramref name="offset" /> is less than zero or greater than the buffer length.</exception>
		// Token: 0x0600047A RID: 1146 RVA: 0x00016D07 File Offset: 0x00014F07
		public static XmlDictionaryReader CreateBinaryReader(byte[] buffer, int offset, int count, IXmlDictionary dictionary, XmlDictionaryReaderQuotas quotas, XmlBinaryReaderSession session)
		{
			return XmlDictionaryReader.CreateBinaryReader(buffer, offset, count, dictionary, quotas, session, null);
		}

		/// <summary>Creates an instance of <see cref="T:System.Xml.XmlDictionaryReader" /> that can read .NET Binary XML Format.</summary>
		/// <param name="buffer">The buffer from which to read.</param>
		/// <param name="offset">The starting position from which to read in <paramref name="buffer" />.</param>
		/// <param name="count">The number of bytes that can be read from <paramref name="buffer" />.</param>
		/// <param name="dictionary">The <see cref="T:System.Xml.XmlDictionary" /> to use.</param>
		/// <param name="quotas">The <see cref="T:System.Xml.XmlDictionaryReaderQuotas" /> to apply.</param>
		/// <param name="session">The <see cref="T:System.Xml.XmlBinaryReaderSession" /> to use.</param>
		/// <param name="onClose">Delegate to be called when the reader is closed.</param>
		/// <returns>An instance of <see cref="T:System.Xml.XmlDictionaryReader" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> is less than zero or greater than the buffer length minus the offset.
		/// -or-
		/// <paramref name="offset" /> is less than zero or greater than the buffer length.</exception>
		// Token: 0x0600047B RID: 1147 RVA: 0x00016D18 File Offset: 0x00014F18
		public static XmlDictionaryReader CreateBinaryReader(byte[] buffer, int offset, int count, IXmlDictionary dictionary, XmlDictionaryReaderQuotas quotas, XmlBinaryReaderSession session, OnXmlDictionaryReaderClose onClose)
		{
			XmlBinaryReader xmlBinaryReader = new XmlBinaryReader();
			xmlBinaryReader.SetInput(buffer, offset, count, dictionary, quotas, session, onClose);
			return xmlBinaryReader;
		}

		/// <summary>Creates an instance of <see cref="T:System.Xml.XmlDictionaryReader" /> that can read .NET Binary XML Format.</summary>
		/// <param name="stream">The stream from which to read.</param>
		/// <param name="quotas">The quotas that apply to this operation.</param>
		/// <returns>An instance of <see cref="T:System.Xml.XmlDictionaryReader" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> is <see langword="null" />.</exception>
		// Token: 0x0600047C RID: 1148 RVA: 0x00016D3A File Offset: 0x00014F3A
		public static XmlDictionaryReader CreateBinaryReader(Stream stream, XmlDictionaryReaderQuotas quotas)
		{
			return XmlDictionaryReader.CreateBinaryReader(stream, null, quotas);
		}

		/// <summary>Creates an instance of <see cref="T:System.Xml.XmlDictionaryReader" /> that can read .NET Binary XML Format.</summary>
		/// <param name="stream">The stream from which to read.</param>
		/// <param name="dictionary">
		///   <see cref="T:System.Xml.XmlDictionary" /> to use.</param>
		/// <param name="quotas">The quotas that apply to this operation.</param>
		/// <returns>An instance of <see cref="T:System.Xml.XmlDictionaryReader" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> or <paramref name="quotas" /> is <see langword="null" />.</exception>
		// Token: 0x0600047D RID: 1149 RVA: 0x00016D44 File Offset: 0x00014F44
		public static XmlDictionaryReader CreateBinaryReader(Stream stream, IXmlDictionary dictionary, XmlDictionaryReaderQuotas quotas)
		{
			return XmlDictionaryReader.CreateBinaryReader(stream, dictionary, quotas, null);
		}

		/// <summary>Creates an instance of <see cref="T:System.Xml.XmlDictionaryReader" /> that can read .NET Binary XML Format.</summary>
		/// <param name="stream">The stream from which to read.</param>
		/// <param name="dictionary">
		///   <see cref="T:System.Xml.XmlDictionary" /> to use.</param>
		/// <param name="quotas">The quotas that apply to this operation.</param>
		/// <param name="session">
		///   <see cref="T:System.Xml.XmlBinaryReaderSession" /> to use.</param>
		/// <returns>An instance of <see cref="T:System.Xml.XmlDictionaryReader" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> is <see langword="null" />.</exception>
		// Token: 0x0600047E RID: 1150 RVA: 0x00016D4F File Offset: 0x00014F4F
		public static XmlDictionaryReader CreateBinaryReader(Stream stream, IXmlDictionary dictionary, XmlDictionaryReaderQuotas quotas, XmlBinaryReaderSession session)
		{
			return XmlDictionaryReader.CreateBinaryReader(stream, dictionary, quotas, session, null);
		}

		/// <summary>Creates an instance of <see cref="T:System.Xml.XmlDictionaryReader" /> that can read .NET Binary XML Format.</summary>
		/// <param name="stream">The stream from which to read.</param>
		/// <param name="dictionary">
		///   <see cref="T:System.Xml.XmlDictionary" /> to use.</param>
		/// <param name="quotas">
		///   <see cref="T:System.Xml.XmlDictionaryReaderQuotas" /> to apply.</param>
		/// <param name="session">
		///   <see cref="T:System.Xml.XmlBinaryReaderSession" /> to use.</param>
		/// <param name="onClose">Delegate to be called when the reader is closed.</param>
		/// <returns>An instance of <see cref="T:System.Xml.XmlDictionaryReader" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> is <see langword="null" />.</exception>
		// Token: 0x0600047F RID: 1151 RVA: 0x00016D5B File Offset: 0x00014F5B
		public static XmlDictionaryReader CreateBinaryReader(Stream stream, IXmlDictionary dictionary, XmlDictionaryReaderQuotas quotas, XmlBinaryReaderSession session, OnXmlDictionaryReaderClose onClose)
		{
			XmlBinaryReader xmlBinaryReader = new XmlBinaryReader();
			xmlBinaryReader.SetInput(stream, dictionary, quotas, session, onClose);
			return xmlBinaryReader;
		}

		/// <summary>Creates an instance of <see cref="T:System.Xml.XmlDictionaryReader" />.</summary>
		/// <param name="buffer">The buffer from which to read.</param>
		/// <param name="quotas">The quotas applied to the reader.</param>
		/// <returns>An instance of <see cref="T:System.Xml.XmlDictionaryReader" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.</exception>
		// Token: 0x06000480 RID: 1152 RVA: 0x00016D6E File Offset: 0x00014F6E
		public static XmlDictionaryReader CreateTextReader(byte[] buffer, XmlDictionaryReaderQuotas quotas)
		{
			if (buffer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("buffer");
			}
			return XmlDictionaryReader.CreateTextReader(buffer, 0, buffer.Length, quotas);
		}

		/// <summary>Creates an instance of <see cref="T:System.Xml.XmlDictionaryReader" />.</summary>
		/// <param name="buffer">The buffer from which to read.</param>
		/// <param name="offset">The starting position from which to read in <paramref name="buffer" />.</param>
		/// <param name="count">The number of bytes that can be read from <paramref name="buffer" />.</param>
		/// <param name="quotas">The quotas applied to the reader.</param>
		/// <returns>An instance of <see cref="T:System.Xml.XmlDictionaryReader" />.</returns>
		// Token: 0x06000481 RID: 1153 RVA: 0x00016D89 File Offset: 0x00014F89
		public static XmlDictionaryReader CreateTextReader(byte[] buffer, int offset, int count, XmlDictionaryReaderQuotas quotas)
		{
			return XmlDictionaryReader.CreateTextReader(buffer, offset, count, null, quotas, null);
		}

		/// <summary>Creates an instance of <see cref="T:System.Xml.XmlDictionaryReader" />.</summary>
		/// <param name="buffer">The buffer from which to read.</param>
		/// <param name="offset">The starting position from which to read in <paramref name="buffer" />.</param>
		/// <param name="count">The number of bytes that can be read from <paramref name="buffer" />.</param>
		/// <param name="encoding">The <see cref="T:System.Text.Encoding" /> object that specifies the encoding properties to apply.</param>
		/// <param name="quotas">The <see cref="T:System.Xml.XmlDictionaryReaderQuotas" /> to apply.</param>
		/// <param name="onClose">The delegate to be called when the reader is closed.</param>
		/// <returns>An instance of <see cref="T:System.Xml.XmlDictionaryReader" />.</returns>
		// Token: 0x06000482 RID: 1154 RVA: 0x00016D96 File Offset: 0x00014F96
		public static XmlDictionaryReader CreateTextReader(byte[] buffer, int offset, int count, Encoding encoding, XmlDictionaryReaderQuotas quotas, OnXmlDictionaryReaderClose onClose)
		{
			XmlUTF8TextReader xmlUTF8TextReader = new XmlUTF8TextReader();
			xmlUTF8TextReader.SetInput(buffer, offset, count, encoding, quotas, onClose);
			return xmlUTF8TextReader;
		}

		/// <summary>Creates an instance of <see cref="T:System.Xml.XmlDictionaryReader" />.</summary>
		/// <param name="stream">The stream from which to read.</param>
		/// <param name="quotas">The quotas applied to the reader.</param>
		/// <returns>An instance of <see cref="T:System.Xml.XmlDictionaryReader" />.</returns>
		// Token: 0x06000483 RID: 1155 RVA: 0x00016DAB File Offset: 0x00014FAB
		public static XmlDictionaryReader CreateTextReader(Stream stream, XmlDictionaryReaderQuotas quotas)
		{
			return XmlDictionaryReader.CreateTextReader(stream, null, quotas, null);
		}

		/// <summary>Creates an instance of <see cref="T:System.Xml.XmlDictionaryReader" />.</summary>
		/// <param name="stream">The stream from which to read.</param>
		/// <param name="encoding">The <see cref="T:System.Text.Encoding" /> object that specifies the encoding properties to apply.</param>
		/// <param name="quotas">The <see cref="T:System.Xml.XmlDictionaryReaderQuotas" /> to apply.</param>
		/// <param name="onClose">The delegate to be called when the reader is closed.</param>
		/// <returns>An instance of <see cref="T:System.Xml.XmlDictionaryReader" />.</returns>
		// Token: 0x06000484 RID: 1156 RVA: 0x00016DB6 File Offset: 0x00014FB6
		public static XmlDictionaryReader CreateTextReader(Stream stream, Encoding encoding, XmlDictionaryReaderQuotas quotas, OnXmlDictionaryReaderClose onClose)
		{
			XmlUTF8TextReader xmlUTF8TextReader = new XmlUTF8TextReader();
			xmlUTF8TextReader.SetInput(stream, encoding, quotas, onClose);
			return xmlUTF8TextReader;
		}

		/// <summary>Creates an instance of <see cref="T:System.Xml.XmlDictionaryReader" /> that reads XML in the MTOM format.</summary>
		/// <param name="stream">The stream from which to read.</param>
		/// <param name="encoding">The possible character encoding of the stream.</param>
		/// <param name="quotas">The quotas to apply to this reader.</param>
		/// <returns>An instance of <see cref="T:System.Xml.XmlDictionaryReader" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="encoding" /> is <see langword="null" />.</exception>
		// Token: 0x06000485 RID: 1157 RVA: 0x00016DC7 File Offset: 0x00014FC7
		public static XmlDictionaryReader CreateMtomReader(Stream stream, Encoding encoding, XmlDictionaryReaderQuotas quotas)
		{
			if (encoding == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("encoding");
			}
			return XmlDictionaryReader.CreateMtomReader(stream, new Encoding[]
			{
				encoding
			}, quotas);
		}

		/// <summary>Creates an instance of <see cref="T:System.Xml.XmlDictionaryReader" /> that reads XML in the MTOM format.</summary>
		/// <param name="stream">The stream from which to read.</param>
		/// <param name="encodings">The possible character encodings of the stream.</param>
		/// <param name="quotas">The quotas to apply to this reader.</param>
		/// <returns>An instance of <see cref="T:System.Xml.XmlDictionaryReader" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="encoding" /> is <see langword="null" />.</exception>
		// Token: 0x06000486 RID: 1158 RVA: 0x00016DE8 File Offset: 0x00014FE8
		public static XmlDictionaryReader CreateMtomReader(Stream stream, Encoding[] encodings, XmlDictionaryReaderQuotas quotas)
		{
			return XmlDictionaryReader.CreateMtomReader(stream, encodings, null, quotas);
		}

		/// <summary>Creates an instance of <see cref="T:System.Xml.XmlDictionaryReader" /> that reads XML in the MTOM format.</summary>
		/// <param name="stream">The stream from which to read.</param>
		/// <param name="encodings">The possible character encodings of the stream.</param>
		/// <param name="contentType">The Content-Type MIME type of the message.</param>
		/// <param name="quotas">The quotas to apply to this reader.</param>
		/// <returns>An instance of <see cref="T:System.Xml.XmlDictionaryReader" />.</returns>
		// Token: 0x06000487 RID: 1159 RVA: 0x00016DF3 File Offset: 0x00014FF3
		public static XmlDictionaryReader CreateMtomReader(Stream stream, Encoding[] encodings, string contentType, XmlDictionaryReaderQuotas quotas)
		{
			return XmlDictionaryReader.CreateMtomReader(stream, encodings, contentType, quotas, int.MaxValue, null);
		}

		/// <summary>Creates an instance of <see cref="T:System.Xml.XmlDictionaryReader" /> that reads XML in the MTOM format.</summary>
		/// <param name="stream">The stream from which to read.</param>
		/// <param name="encodings">The possible character encodings of the stream.</param>
		/// <param name="contentType">The Content-Type MIME type of the message.</param>
		/// <param name="quotas">The MIME type of the message.</param>
		/// <param name="maxBufferSize">The <see cref="T:System.Xml.XmlDictionaryReaderQuotas" /> to apply to the reader.</param>
		/// <param name="onClose">The delegate to be called when the reader is closed.</param>
		/// <returns>An instance of <see cref="T:System.Xml.XmlDictionaryReader" />.</returns>
		// Token: 0x06000488 RID: 1160 RVA: 0x00016E04 File Offset: 0x00015004
		public static XmlDictionaryReader CreateMtomReader(Stream stream, Encoding[] encodings, string contentType, XmlDictionaryReaderQuotas quotas, int maxBufferSize, OnXmlDictionaryReaderClose onClose)
		{
			XmlMtomReader xmlMtomReader = new XmlMtomReader();
			xmlMtomReader.SetInput(stream, encodings, contentType, quotas, maxBufferSize, onClose);
			return xmlMtomReader;
		}

		/// <summary>Creates an instance of <see cref="T:System.Xml.XmlDictionaryReader" /> that reads XML in the MTOM format.</summary>
		/// <param name="buffer">The buffer from which to read.</param>
		/// <param name="offset">The starting position from which to read in <paramref name="buffer" />.</param>
		/// <param name="count">The number of bytes that can be read from <paramref name="buffer" />.</param>
		/// <param name="encoding">The possible character encoding of the input.</param>
		/// <param name="quotas">The quotas to apply to this reader.</param>
		/// <returns>An instance of <see cref="T:System.Xml.XmlDictionaryReader" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="encoding" /> is <see langword="null" />.</exception>
		// Token: 0x06000489 RID: 1161 RVA: 0x00016E19 File Offset: 0x00015019
		public static XmlDictionaryReader CreateMtomReader(byte[] buffer, int offset, int count, Encoding encoding, XmlDictionaryReaderQuotas quotas)
		{
			if (encoding == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("encoding");
			}
			return XmlDictionaryReader.CreateMtomReader(buffer, offset, count, new Encoding[]
			{
				encoding
			}, quotas);
		}

		/// <summary>Creates an instance of <see cref="T:System.Xml.XmlDictionaryReader" /> that reads XML in the MTOM format.</summary>
		/// <param name="buffer">The buffer from which to read.</param>
		/// <param name="offset">The starting position from which to read in <paramref name="buffer" />.</param>
		/// <param name="count">The number of bytes that can be read from <paramref name="buffer" />.</param>
		/// <param name="encodings">The possible character encodings of the input.</param>
		/// <param name="quotas">The quotas to apply to this reader.</param>
		/// <returns>An instance of <see cref="T:System.Xml.XmlDictionaryReader" />.</returns>
		// Token: 0x0600048A RID: 1162 RVA: 0x00016E3D File Offset: 0x0001503D
		public static XmlDictionaryReader CreateMtomReader(byte[] buffer, int offset, int count, Encoding[] encodings, XmlDictionaryReaderQuotas quotas)
		{
			return XmlDictionaryReader.CreateMtomReader(buffer, offset, count, encodings, null, quotas);
		}

		/// <summary>Creates an instance of <see cref="T:System.Xml.XmlDictionaryReader" /> that reads XML in the MTOM format.</summary>
		/// <param name="buffer">The buffer from which to read.</param>
		/// <param name="offset">The starting position from which to read in <paramref name="buffer" />.</param>
		/// <param name="count">The number of bytes that can be read from <paramref name="buffer" />.</param>
		/// <param name="encodings">The possible character encodings of the input.</param>
		/// <param name="contentType">The Content-Type MIME type of the message.</param>
		/// <param name="quotas">The quotas to apply to this reader.</param>
		/// <returns>An instance of <see cref="T:System.Xml.XmlDictionaryReader" />.</returns>
		// Token: 0x0600048B RID: 1163 RVA: 0x00016E4B File Offset: 0x0001504B
		public static XmlDictionaryReader CreateMtomReader(byte[] buffer, int offset, int count, Encoding[] encodings, string contentType, XmlDictionaryReaderQuotas quotas)
		{
			return XmlDictionaryReader.CreateMtomReader(buffer, offset, count, encodings, contentType, quotas, int.MaxValue, null);
		}

		/// <summary>Creates an instance of <see cref="T:System.Xml.XmlDictionaryReader" /> that reads XML in the MTOM format.</summary>
		/// <param name="buffer">The buffer from which to read.</param>
		/// <param name="offset">The starting position from which to read in <paramref name="buffer" />.</param>
		/// <param name="count">The number of bytes that can be read from <paramref name="buffer" />.</param>
		/// <param name="encodings">The possible character encodings of the input.</param>
		/// <param name="contentType">The Content-Type MIME type of the message.</param>
		/// <param name="quotas">The <see cref="T:System.Xml.XmlDictionaryReaderQuotas" /> to apply to the reader.</param>
		/// <param name="maxBufferSize">The maximum allowed size of the buffer.</param>
		/// <param name="onClose">The delegate to be called when the reader is closed.</param>
		/// <returns>An instance of <see cref="T:System.Xml.XmlDictionaryReader" />.</returns>
		// Token: 0x0600048C RID: 1164 RVA: 0x00016E60 File Offset: 0x00015060
		public static XmlDictionaryReader CreateMtomReader(byte[] buffer, int offset, int count, Encoding[] encodings, string contentType, XmlDictionaryReaderQuotas quotas, int maxBufferSize, OnXmlDictionaryReaderClose onClose)
		{
			XmlMtomReader xmlMtomReader = new XmlMtomReader();
			xmlMtomReader.SetInput(buffer, offset, count, encodings, contentType, quotas, maxBufferSize, onClose);
			return xmlMtomReader;
		}

		/// <summary>This property always returns <see langword="false" />. Its derived classes can override to return <see langword="true" /> if they support canonicalization.</summary>
		/// <returns>Returns <see langword="false" />.</returns>
		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600048D RID: 1165 RVA: 0x00003127 File Offset: 0x00001327
		public virtual bool CanCanonicalize
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets the quota values that apply to the current instance of this class.</summary>
		/// <returns>The <see cref="T:System.Xml.XmlDictionaryReaderQuotas" /> that applies to the current instance of this class.</returns>
		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600048E RID: 1166 RVA: 0x00016E84 File Offset: 0x00015084
		public virtual XmlDictionaryReaderQuotas Quotas
		{
			get
			{
				return XmlDictionaryReaderQuotas.Max;
			}
		}

		/// <summary>This method is not yet implemented.</summary>
		/// <param name="stream">The stream to read from.</param>
		/// <param name="includeComments">Determines whether comments are included.</param>
		/// <param name="inclusivePrefixes">The prefixes to be included.</param>
		/// <exception cref="T:System.NotSupportedException">Always.</exception>
		// Token: 0x0600048F RID: 1167 RVA: 0x00003141 File Offset: 0x00001341
		public virtual void StartCanonicalization(Stream stream, bool includeComments, string[] inclusivePrefixes)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
		}

		/// <summary>This method is not yet implemented.</summary>
		/// <exception cref="T:System.NotSupportedException">Always.</exception>
		// Token: 0x06000490 RID: 1168 RVA: 0x00003141 File Offset: 0x00001341
		public virtual void EndCanonicalization()
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
		}

		/// <summary>Tests whether the current content node is a start element or an empty element.</summary>
		// Token: 0x06000491 RID: 1169 RVA: 0x00016E8B File Offset: 0x0001508B
		public virtual void MoveToStartElement()
		{
			if (!this.IsStartElement())
			{
				XmlExceptionHelper.ThrowStartElementExpected(this);
			}
		}

		/// <summary>Tests whether the current content node is a start element or an empty element and if the <see cref="P:System.Xml.XmlReader.Name" /> property of the element matches the given argument.</summary>
		/// <param name="name">The <see cref="P:System.Xml.XmlReader.Name" /> property of the element.</param>
		// Token: 0x06000492 RID: 1170 RVA: 0x00016E9B File Offset: 0x0001509B
		public virtual void MoveToStartElement(string name)
		{
			if (!this.IsStartElement(name))
			{
				XmlExceptionHelper.ThrowStartElementExpected(this, name);
			}
		}

		/// <summary>Tests whether the current content node is a start element or an empty element and if the <see cref="P:System.Xml.XmlReader.LocalName" /> and <see cref="P:System.Xml.XmlReader.NamespaceURI" /> properties of the element matches the given arguments.</summary>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		// Token: 0x06000493 RID: 1171 RVA: 0x00016EAD File Offset: 0x000150AD
		public virtual void MoveToStartElement(string localName, string namespaceUri)
		{
			if (!this.IsStartElement(localName, namespaceUri))
			{
				XmlExceptionHelper.ThrowStartElementExpected(this, localName, namespaceUri);
			}
		}

		/// <summary>Tests whether the current content node is a start element or an empty element and if the <see cref="P:System.Xml.XmlReader.LocalName" /> and <see cref="P:System.Xml.XmlReader.NamespaceURI" /> properties of the element matches the given argument.</summary>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		// Token: 0x06000494 RID: 1172 RVA: 0x00016EC1 File Offset: 0x000150C1
		public virtual void MoveToStartElement(XmlDictionaryString localName, XmlDictionaryString namespaceUri)
		{
			if (!this.IsStartElement(localName, namespaceUri))
			{
				XmlExceptionHelper.ThrowStartElementExpected(this, localName, namespaceUri);
			}
		}

		/// <summary>Checks whether the parameter, <paramref name="localName" />, is the local name of the current node.</summary>
		/// <param name="localName">The local name of the current node.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="localName" /> matches local name of the current node; otherwise <see langword="false" />.</returns>
		// Token: 0x06000495 RID: 1173 RVA: 0x00016ED5 File Offset: 0x000150D5
		public virtual bool IsLocalName(string localName)
		{
			return this.LocalName == localName;
		}

		/// <summary>Checks whether the parameter, <paramref name="localName" />, is the local name of the current node.</summary>
		/// <param name="localName">An <see cref="T:System.Xml.XmlDictionaryString" /> that represents the local name of the current node.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="localName" /> matches local name of the current node; otherwise <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="localName" /> is <see langword="null" />.</exception>
		// Token: 0x06000496 RID: 1174 RVA: 0x00016EE3 File Offset: 0x000150E3
		public virtual bool IsLocalName(XmlDictionaryString localName)
		{
			if (localName == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("localName");
			}
			return this.IsLocalName(localName.Value);
		}

		/// <summary>Checks whether the parameter, <paramref name="namespaceUri" />, is the namespace of the current node.</summary>
		/// <param name="namespaceUri">The namespace of current node.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="namespaceUri" /> matches namespace of the current node; otherwise <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="namespaceUri" /> is <see langword="null" />.</exception>
		// Token: 0x06000497 RID: 1175 RVA: 0x00016EFF File Offset: 0x000150FF
		public virtual bool IsNamespaceUri(string namespaceUri)
		{
			if (namespaceUri == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("namespaceUri");
			}
			return this.NamespaceURI == namespaceUri;
		}

		/// <summary>Checks whether the parameter, <paramref name="namespaceUri" />, is the namespace of the current node.</summary>
		/// <param name="namespaceUri">Namespace of current node.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="namespaceUri" /> matches namespace of the current node; otherwise <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="namespaceUri" /> is <see langword="null" />.</exception>
		// Token: 0x06000498 RID: 1176 RVA: 0x00016F1B File Offset: 0x0001511B
		public virtual bool IsNamespaceUri(XmlDictionaryString namespaceUri)
		{
			if (namespaceUri == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("namespaceUri");
			}
			return this.IsNamespaceUri(namespaceUri.Value);
		}

		/// <summary>Checks whether the current node is an element and advances the reader to the next node.</summary>
		/// <exception cref="T:System.Xml.XmlException">
		///   <see cref="M:System.Xml.XmlDictionaryReader.IsStartElement(System.Xml.XmlDictionaryString,System.Xml.XmlDictionaryString)" /> returns <see langword="false" />.</exception>
		// Token: 0x06000499 RID: 1177 RVA: 0x00016F37 File Offset: 0x00015137
		public virtual void ReadFullStartElement()
		{
			this.MoveToStartElement();
			if (this.IsEmptyElement)
			{
				XmlExceptionHelper.ThrowFullStartElementExpected(this);
			}
			this.Read();
		}

		/// <summary>Checks whether the current node is an element with the given <paramref name="name" /> and advances the reader to the next node.</summary>
		/// <param name="name">The qualified name of the element.</param>
		/// <exception cref="T:System.Xml.XmlException">
		///   <see cref="M:System.Xml.XmlDictionaryReader.IsStartElement(System.Xml.XmlDictionaryString,System.Xml.XmlDictionaryString)" /> returns <see langword="false" />.</exception>
		// Token: 0x0600049A RID: 1178 RVA: 0x00016F54 File Offset: 0x00015154
		public virtual void ReadFullStartElement(string name)
		{
			this.MoveToStartElement(name);
			if (this.IsEmptyElement)
			{
				XmlExceptionHelper.ThrowFullStartElementExpected(this, name);
			}
			this.Read();
		}

		/// <summary>Checks whether the current node is an element with the given <paramref name="localName" /> and <paramref name="namespaceUri" /> and advances the reader to the next node.</summary>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <exception cref="T:System.Xml.XmlException">
		///   <see cref="M:System.Xml.XmlDictionaryReader.IsStartElement(System.Xml.XmlDictionaryString,System.Xml.XmlDictionaryString)" /> returns <see langword="false" />.</exception>
		// Token: 0x0600049B RID: 1179 RVA: 0x00016F73 File Offset: 0x00015173
		public virtual void ReadFullStartElement(string localName, string namespaceUri)
		{
			this.MoveToStartElement(localName, namespaceUri);
			if (this.IsEmptyElement)
			{
				XmlExceptionHelper.ThrowFullStartElementExpected(this, localName, namespaceUri);
			}
			this.Read();
		}

		/// <summary>Checks whether the current node is an element with the given <paramref name="localName" /> and <paramref name="namespaceUri" /> and advances the reader to the next node.</summary>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <exception cref="T:System.Xml.XmlException">
		///   <see cref="M:System.Xml.XmlDictionaryReader.IsStartElement(System.Xml.XmlDictionaryString,System.Xml.XmlDictionaryString)" /> returns <see langword="false" />.</exception>
		// Token: 0x0600049C RID: 1180 RVA: 0x00016F94 File Offset: 0x00015194
		public virtual void ReadFullStartElement(XmlDictionaryString localName, XmlDictionaryString namespaceUri)
		{
			this.MoveToStartElement(localName, namespaceUri);
			if (this.IsEmptyElement)
			{
				XmlExceptionHelper.ThrowFullStartElementExpected(this, localName, namespaceUri);
			}
			this.Read();
		}

		/// <summary>Checks whether the current node is an element with the given <paramref name="localName" /> and <paramref name="namespaceUri" /> and advances the reader to the next node.</summary>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		// Token: 0x0600049D RID: 1181 RVA: 0x00016FB5 File Offset: 0x000151B5
		public virtual void ReadStartElement(XmlDictionaryString localName, XmlDictionaryString namespaceUri)
		{
			this.MoveToStartElement(localName, namespaceUri);
			this.Read();
		}

		/// <summary>Tests whether the first tag is a start tag or empty element tag and if the local name and namespace URI match those of the current node.</summary>
		/// <param name="localName">An <see cref="T:System.Xml.XmlDictionaryString" /> that represents the local name of the attribute.</param>
		/// <param name="namespaceUri">An <see cref="T:System.Xml.XmlDictionaryString" /> that represents the namespace of the attribute.</param>
		/// <returns>
		///   <see langword="true" /> if the first tag in the array is a start tag or empty element tag and matches <paramref name="localName" /> and <paramref name="namespaceUri" />; otherwise <see langword="false" />.</returns>
		// Token: 0x0600049E RID: 1182 RVA: 0x00016FC6 File Offset: 0x000151C6
		public virtual bool IsStartElement(XmlDictionaryString localName, XmlDictionaryString namespaceUri)
		{
			return this.IsStartElement(XmlDictionaryString.GetString(localName), XmlDictionaryString.GetString(namespaceUri));
		}

		/// <summary>Gets the index of the local name of the current node within an array of names.</summary>
		/// <param name="localNames">The string array of local names to be searched.</param>
		/// <param name="namespaceUri">The namespace of current node.</param>
		/// <returns>The index of the local name of the current node within an array of names.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="localNames" /> or any of the names in the array is <see langword="null" />.
		/// -or-
		/// <paramref name="namespaceUri" /> is <see langword="null" />.</exception>
		// Token: 0x0600049F RID: 1183 RVA: 0x00016FDC File Offset: 0x000151DC
		public virtual int IndexOfLocalName(string[] localNames, string namespaceUri)
		{
			if (localNames == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("localNames");
			}
			if (namespaceUri == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("namespaceUri");
			}
			if (this.NamespaceURI == namespaceUri)
			{
				string localName = this.LocalName;
				for (int i = 0; i < localNames.Length; i++)
				{
					string text = localNames[i];
					if (text == null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull(string.Format(CultureInfo.InvariantCulture, "localNames[{0}]", i));
					}
					if (localName == text)
					{
						return i;
					}
				}
			}
			return -1;
		}

		/// <summary>Gets the index of the local name of the current node within an array of names.</summary>
		/// <param name="localNames">The <see cref="T:System.Xml.XmlDictionaryString" /> array of local names to be searched.</param>
		/// <param name="namespaceUri">The namespace of current node.</param>
		/// <returns>The index of the local name of the current node within an array of names.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="localNames" /> or any of the names in the array is <see langword="null" />.
		/// -or-
		/// <paramref name="namespaceUri" /> is <see langword="null" />.</exception>
		// Token: 0x060004A0 RID: 1184 RVA: 0x00017058 File Offset: 0x00015258
		public virtual int IndexOfLocalName(XmlDictionaryString[] localNames, XmlDictionaryString namespaceUri)
		{
			if (localNames == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("localNames");
			}
			if (namespaceUri == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("namespaceUri");
			}
			if (this.NamespaceURI == namespaceUri.Value)
			{
				string localName = this.LocalName;
				for (int i = 0; i < localNames.Length; i++)
				{
					XmlDictionaryString xmlDictionaryString = localNames[i];
					if (xmlDictionaryString == null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull(string.Format(CultureInfo.InvariantCulture, "localNames[{0}]", i));
					}
					if (localName == xmlDictionaryString.Value)
					{
						return i;
					}
				}
			}
			return -1;
		}

		/// <summary>When overridden in a derived class, gets the value of an attribute.</summary>
		/// <param name="localName">An <see cref="T:System.Xml.XmlDictionaryString" /> that represents the local name of the attribute.</param>
		/// <param name="namespaceUri">An <see cref="T:System.Xml.XmlDictionaryString" /> that represents the namespace of the attribute.</param>
		/// <returns>The value of the attribute.</returns>
		// Token: 0x060004A1 RID: 1185 RVA: 0x000170DC File Offset: 0x000152DC
		public virtual string GetAttribute(XmlDictionaryString localName, XmlDictionaryString namespaceUri)
		{
			return this.GetAttribute(XmlDictionaryString.GetString(localName), XmlDictionaryString.GetString(namespaceUri));
		}

		/// <summary>Not implemented in this class (it always returns <see langword="false" />). May be overridden in derived classes.</summary>
		/// <param name="length">Returns 0, unless overridden in a derived class.</param>
		/// <returns>
		///   <see langword="false" />, unless overridden in a derived class.</returns>
		// Token: 0x060004A2 RID: 1186 RVA: 0x000170F0 File Offset: 0x000152F0
		public virtual bool TryGetBase64ContentLength(out int length)
		{
			length = 0;
			return false;
		}

		/// <summary>Not implemented.</summary>
		/// <param name="buffer">The buffer from which to read.</param>
		/// <param name="offset">The starting position from which to read in <paramref name="buffer" />.</param>
		/// <param name="count">The number of bytes that can be read from <paramref name="buffer" />.</param>
		/// <returns>Not implemented.</returns>
		/// <exception cref="T:System.NotSupportedException">Always.</exception>
		// Token: 0x060004A3 RID: 1187 RVA: 0x00003141 File Offset: 0x00001341
		public virtual int ReadValueAsBase64(byte[] buffer, int offset, int count)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
		}

		/// <summary>Reads the content and returns the Base64 decoded binary bytes.</summary>
		/// <returns>A byte array that contains the Base64 decoded binary bytes.</returns>
		/// <exception cref="T:System.Xml.XmlException">The array size is greater than the MaxArrayLength quota for this reader.</exception>
		// Token: 0x060004A4 RID: 1188 RVA: 0x000170F6 File Offset: 0x000152F6
		public virtual byte[] ReadContentAsBase64()
		{
			return this.ReadContentAsBase64(this.Quotas.MaxArrayLength, 65535);
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x00017110 File Offset: 0x00015310
		internal byte[] ReadContentAsBase64(int maxByteArrayContentLength, int maxInitialCount)
		{
			int num;
			if (this.TryGetBase64ContentLength(out num))
			{
				if (num > maxByteArrayContentLength)
				{
					XmlExceptionHelper.ThrowMaxArrayLengthExceeded(this, maxByteArrayContentLength);
				}
				if (num <= maxInitialCount)
				{
					byte[] array = new byte[num];
					int num2;
					for (int i = 0; i < num; i += num2)
					{
						num2 = this.ReadContentAsBase64(array, i, num - i);
						if (num2 == 0)
						{
							XmlExceptionHelper.ThrowBase64DataExpected(this);
						}
					}
					return array;
				}
			}
			return this.ReadContentAsBytes(true, maxByteArrayContentLength);
		}

		/// <summary>Converts a node's content to a string.</summary>
		/// <returns>The node content in a string representation.</returns>
		// Token: 0x060004A6 RID: 1190 RVA: 0x00017168 File Offset: 0x00015368
		public override string ReadContentAsString()
		{
			return this.ReadContentAsString(this.Quotas.MaxStringContentLength);
		}

		/// <summary>Converts a node's content to a string.</summary>
		/// <param name="maxStringContentLength">The maximum string length.</param>
		/// <returns>Node content in string representation.</returns>
		// Token: 0x060004A7 RID: 1191 RVA: 0x0001717C File Offset: 0x0001537C
		protected string ReadContentAsString(int maxStringContentLength)
		{
			StringBuilder stringBuilder = null;
			string text = string.Empty;
			bool flag = false;
			for (;;)
			{
				switch (this.NodeType)
				{
				case XmlNodeType.Element:
				case XmlNodeType.Entity:
				case XmlNodeType.Document:
				case XmlNodeType.DocumentType:
				case XmlNodeType.DocumentFragment:
				case XmlNodeType.Notation:
				case XmlNodeType.EndElement:
					goto IL_B4;
				case XmlNodeType.Attribute:
					text = this.Value;
					break;
				case XmlNodeType.Text:
				case XmlNodeType.CDATA:
				case XmlNodeType.Whitespace:
				case XmlNodeType.SignificantWhitespace:
				{
					string value = this.Value;
					if (text.Length == 0)
					{
						text = value;
					}
					else
					{
						if (stringBuilder == null)
						{
							stringBuilder = new StringBuilder(text);
						}
						if (stringBuilder.Length > maxStringContentLength - value.Length)
						{
							XmlExceptionHelper.ThrowMaxStringContentLengthExceeded(this, maxStringContentLength);
						}
						stringBuilder.Append(value);
					}
					break;
				}
				case XmlNodeType.EntityReference:
					if (!this.CanResolveEntity)
					{
						goto IL_B4;
					}
					this.ResolveEntity();
					break;
				case XmlNodeType.ProcessingInstruction:
				case XmlNodeType.Comment:
				case XmlNodeType.EndEntity:
					break;
				default:
					goto IL_B4;
				}
				IL_B6:
				if (flag)
				{
					break;
				}
				if (this.AttributeCount != 0)
				{
					this.ReadAttributeValue();
					continue;
				}
				this.Read();
				continue;
				IL_B4:
				flag = true;
				goto IL_B6;
			}
			if (stringBuilder != null)
			{
				text = stringBuilder.ToString();
			}
			if (text.Length > maxStringContentLength)
			{
				XmlExceptionHelper.ThrowMaxStringContentLengthExceeded(this, maxStringContentLength);
			}
			return text;
		}

		/// <summary>Reads the contents of the current node into a string.</summary>
		/// <returns>A string that contains the contents of the current node.</returns>
		/// <exception cref="T:System.InvalidOperationException">Unable to read the contents of the current node.</exception>
		/// <exception cref="T:System.Xml.XmlException">Maximum allowed string length exceeded.</exception>
		// Token: 0x060004A8 RID: 1192 RVA: 0x0001727D File Offset: 0x0001547D
		public override string ReadString()
		{
			return this.ReadString(this.Quotas.MaxStringContentLength);
		}

		/// <summary>Reads the contents of the current node into a string with a given maximum length.</summary>
		/// <param name="maxStringContentLength">Maximum allowed string length.</param>
		/// <returns>A string that contains the contents of the current node.</returns>
		/// <exception cref="T:System.InvalidOperationException">Unable to read the contents of the current node.</exception>
		/// <exception cref="T:System.Xml.XmlException">Maximum allowed string length exceeded.</exception>
		// Token: 0x060004A9 RID: 1193 RVA: 0x00017290 File Offset: 0x00015490
		protected string ReadString(int maxStringContentLength)
		{
			if (this.ReadState != ReadState.Interactive)
			{
				return string.Empty;
			}
			if (this.NodeType != XmlNodeType.Element)
			{
				this.MoveToElement();
			}
			if (this.NodeType == XmlNodeType.Element)
			{
				if (this.IsEmptyElement)
				{
					return string.Empty;
				}
				if (!this.Read())
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(System.Runtime.Serialization.SR.GetString("The reader cannot be advanced.")));
				}
				if (this.NodeType == XmlNodeType.EndElement)
				{
					return string.Empty;
				}
			}
			StringBuilder stringBuilder = null;
			string text = string.Empty;
			while (this.IsTextNode(this.NodeType))
			{
				string value = this.Value;
				if (text.Length == 0)
				{
					text = value;
				}
				else
				{
					if (stringBuilder == null)
					{
						stringBuilder = new StringBuilder(text);
					}
					if (stringBuilder.Length > maxStringContentLength - value.Length)
					{
						XmlExceptionHelper.ThrowMaxStringContentLengthExceeded(this, maxStringContentLength);
					}
					stringBuilder.Append(value);
				}
				if (!this.Read())
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(System.Runtime.Serialization.SR.GetString("The reader cannot be advanced.")));
				}
			}
			if (stringBuilder != null)
			{
				text = stringBuilder.ToString();
			}
			if (text.Length > maxStringContentLength)
			{
				XmlExceptionHelper.ThrowMaxStringContentLengthExceeded(this, maxStringContentLength);
			}
			return text;
		}

		/// <summary>Reads the content and returns the <see langword="BinHex" /> decoded binary bytes.</summary>
		/// <returns>A byte array that contains the <see langword="BinHex" /> decoded binary bytes.</returns>
		/// <exception cref="T:System.Xml.XmlException">The array size is greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x060004AA RID: 1194 RVA: 0x0001738C File Offset: 0x0001558C
		public virtual byte[] ReadContentAsBinHex()
		{
			return this.ReadContentAsBinHex(this.Quotas.MaxArrayLength);
		}

		/// <summary>Reads the content and returns the <see langword="BinHex" /> decoded binary bytes.</summary>
		/// <param name="maxByteArrayContentLength">The maximum array length.</param>
		/// <returns>A byte array that contains the <see langword="BinHex" /> decoded binary bytes.</returns>
		/// <exception cref="T:System.Xml.XmlException">The array size is greater than <paramref name="maxByteArrayContentLength" />.</exception>
		// Token: 0x060004AB RID: 1195 RVA: 0x0001739F File Offset: 0x0001559F
		protected byte[] ReadContentAsBinHex(int maxByteArrayContentLength)
		{
			return this.ReadContentAsBytes(false, maxByteArrayContentLength);
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x000173AC File Offset: 0x000155AC
		private byte[] ReadContentAsBytes(bool base64, int maxByteArrayContentLength)
		{
			byte[][] array = new byte[32][];
			int num = 384;
			int num2 = 0;
			int num3 = 0;
			byte[] array2;
			for (;;)
			{
				array2 = new byte[num];
				array[num2++] = array2;
				int i;
				int num4;
				for (i = 0; i < array2.Length; i += num4)
				{
					if (base64)
					{
						num4 = this.ReadContentAsBase64(array2, i, array2.Length - i);
					}
					else
					{
						num4 = this.ReadContentAsBinHex(array2, i, array2.Length - i);
					}
					if (num4 == 0)
					{
						break;
					}
				}
				if (num3 > maxByteArrayContentLength - i)
				{
					XmlExceptionHelper.ThrowMaxArrayLengthExceeded(this, maxByteArrayContentLength);
				}
				num3 += i;
				if (i < array2.Length)
				{
					break;
				}
				num *= 2;
			}
			array2 = new byte[num3];
			int num5 = 0;
			for (int j = 0; j < num2 - 1; j++)
			{
				Buffer.BlockCopy(array[j], 0, array2, num5, array[j].Length);
				num5 += array[j].Length;
			}
			Buffer.BlockCopy(array[num2 - 1], 0, array2, num5, num3 - num5);
			return array2;
		}

		/// <summary>Tests whether the current node is a text node.</summary>
		/// <param name="nodeType">Type of the node being tested.</param>
		/// <returns>
		///   <see langword="true" /> if the node type is <see cref="F:System.Xml.XmlNodeType.Text" />, <see cref="F:System.Xml.XmlNodeType.Whitespace" />, <see cref="F:System.Xml.XmlNodeType.SignificantWhitespace" />, <see cref="F:System.Xml.XmlNodeType.CDATA" />, or <see cref="F:System.Xml.XmlNodeType.Attribute" />; otherwise <see langword="false" />.</returns>
		// Token: 0x060004AD RID: 1197 RVA: 0x0001748B File Offset: 0x0001568B
		protected bool IsTextNode(XmlNodeType nodeType)
		{
			return nodeType == XmlNodeType.Text || nodeType == XmlNodeType.Whitespace || nodeType == XmlNodeType.SignificantWhitespace || nodeType == XmlNodeType.CDATA || nodeType == XmlNodeType.Attribute;
		}

		/// <summary>Reads the content into a <see langword="char" /> array.</summary>
		/// <param name="chars">The array into which the characters are put.</param>
		/// <param name="offset">The starting index in the array.</param>
		/// <param name="count">The number of characters to put in the array.</param>
		/// <returns>Number of characters read.</returns>
		// Token: 0x060004AE RID: 1198 RVA: 0x000174A8 File Offset: 0x000156A8
		public virtual int ReadContentAsChars(char[] chars, int offset, int count)
		{
			int num = 0;
			for (;;)
			{
				XmlNodeType nodeType = this.NodeType;
				if (nodeType == XmlNodeType.Element || nodeType == XmlNodeType.EndElement)
				{
					break;
				}
				if (this.IsTextNode(nodeType))
				{
					num = this.ReadValueChunk(chars, offset, count);
					if (num > 0 || nodeType == XmlNodeType.Attribute)
					{
						break;
					}
					if (!this.Read())
					{
						break;
					}
				}
				else if (!this.Read())
				{
					break;
				}
			}
			return num;
		}

		/// <summary>Converts a node's content to a specified type.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> of the value to be returned.</param>
		/// <param name="namespaceResolver">An <see cref="T:System.Xml.IXmlNamespaceResolver" /> object that is used to resolve any namespace prefixes related to type conversion. For example, this can be used when converting an <see cref="T:System.Xml.XmlQualifiedName" /> object to an <c>xs:string</c>. This value can be a null reference.</param>
		/// <returns>The concatenated text content or attribute value converted to the requested type.</returns>
		// Token: 0x060004AF RID: 1199 RVA: 0x000174F8 File Offset: 0x000156F8
		public override object ReadContentAs(Type type, IXmlNamespaceResolver namespaceResolver)
		{
			if (type == typeof(Guid[]))
			{
				string[] array = (string[])this.ReadContentAs(typeof(string[]), namespaceResolver);
				Guid[] array2 = new Guid[array.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array2[i] = XmlConverter.ToGuid(array[i]);
				}
				return array2;
			}
			if (type == typeof(UniqueId[]))
			{
				string[] array3 = (string[])this.ReadContentAs(typeof(string[]), namespaceResolver);
				UniqueId[] array4 = new UniqueId[array3.Length];
				for (int j = 0; j < array3.Length; j++)
				{
					array4[j] = XmlConverter.ToUniqueId(array3[j]);
				}
				return array4;
			}
			return base.ReadContentAs(type, namespaceResolver);
		}

		/// <summary>Converts a node's content to a string.</summary>
		/// <param name="strings">The array of strings to match content against.</param>
		/// <param name="index">The index of the entry in <paramref name="strings" /> that matches the content.</param>
		/// <returns>The node content in a string representation.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="strings" /> is <see langword="null" />.
		/// -or-
		/// An entry in <paramref name="strings" /> is <see langword="null" />.</exception>
		// Token: 0x060004B0 RID: 1200 RVA: 0x000175B4 File Offset: 0x000157B4
		public virtual string ReadContentAsString(string[] strings, out int index)
		{
			if (strings == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("strings");
			}
			string text = this.ReadContentAsString();
			index = -1;
			for (int i = 0; i < strings.Length; i++)
			{
				string text2 = strings[i];
				if (text2 == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull(string.Format(CultureInfo.InvariantCulture, "strings[{0}]", i));
				}
				if (text2 == text)
				{
					index = i;
					return text2;
				}
			}
			return text;
		}

		/// <summary>Converts a node's content to a string.</summary>
		/// <param name="strings">The array of <see cref="T:System.Xml.XmlDictionaryString" /> objects to match content against.</param>
		/// <param name="index">The index of the entry in <paramref name="strings" /> that matches the content.</param>
		/// <returns>The node content in a string representation.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="strings" /> is <see langword="null" />.
		/// -or-
		/// An entry in <paramref name="strings" /> is <see langword="null" />.</exception>
		// Token: 0x060004B1 RID: 1201 RVA: 0x00017618 File Offset: 0x00015818
		public virtual string ReadContentAsString(XmlDictionaryString[] strings, out int index)
		{
			if (strings == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("strings");
			}
			string text = this.ReadContentAsString();
			index = -1;
			for (int i = 0; i < strings.Length; i++)
			{
				XmlDictionaryString xmlDictionaryString = strings[i];
				if (xmlDictionaryString == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull(string.Format(CultureInfo.InvariantCulture, "strings[{0}]", i));
				}
				if (xmlDictionaryString.Value == text)
				{
					index = i;
					return xmlDictionaryString.Value;
				}
			}
			return text;
		}

		/// <summary>Converts a node's content to <see langword="decimal" />.</summary>
		/// <returns>The <see langword="decimal" /> representation of node's content.</returns>
		// Token: 0x060004B2 RID: 1202 RVA: 0x00017686 File Offset: 0x00015886
		public override decimal ReadContentAsDecimal()
		{
			return XmlConverter.ToDecimal(this.ReadContentAsString());
		}

		/// <summary>Converts a node's content to <see langword="float" />.</summary>
		/// <returns>The <see langword="float" /> representation of node's content.</returns>
		// Token: 0x060004B3 RID: 1203 RVA: 0x00017693 File Offset: 0x00015893
		public override float ReadContentAsFloat()
		{
			return XmlConverter.ToSingle(this.ReadContentAsString());
		}

		/// <summary>Converts a node's content to a unique identifier.</summary>
		/// <returns>The node's content represented as a unique identifier.</returns>
		// Token: 0x060004B4 RID: 1204 RVA: 0x000176A0 File Offset: 0x000158A0
		public virtual UniqueId ReadContentAsUniqueId()
		{
			return XmlConverter.ToUniqueId(this.ReadContentAsString());
		}

		/// <summary>Converts a node's content to <see langword="guid" />.</summary>
		/// <returns>The <see langword="guid" /> representation of node's content.</returns>
		// Token: 0x060004B5 RID: 1205 RVA: 0x000176AD File Offset: 0x000158AD
		public virtual Guid ReadContentAsGuid()
		{
			return XmlConverter.ToGuid(this.ReadContentAsString());
		}

		/// <summary>Converts a node's content to <see cref="T:System.TimeSpan" />.</summary>
		/// <returns>
		///   <see cref="T:System.TimeSpan" /> representation of node's content.</returns>
		// Token: 0x060004B6 RID: 1206 RVA: 0x000176BA File Offset: 0x000158BA
		public virtual TimeSpan ReadContentAsTimeSpan()
		{
			return XmlConverter.ToTimeSpan(this.ReadContentAsString());
		}

		/// <summary>Converts a node's content to a qualified name representation.</summary>
		/// <param name="localName">The <see cref="P:System.Xml.XmlReader.LocalName" /> part of the qualified name (<see langword="out" /> parameter).</param>
		/// <param name="namespaceUri">The <see cref="P:System.Xml.XmlReader.NamespaceURI" /> part of the qualified name (<see langword="out" /> parameter).</param>
		// Token: 0x060004B7 RID: 1207 RVA: 0x000176C8 File Offset: 0x000158C8
		public virtual void ReadContentAsQualifiedName(out string localName, out string namespaceUri)
		{
			string prefix;
			XmlConverter.ToQualifiedName(this.ReadContentAsString(), out prefix, out localName);
			namespaceUri = this.LookupNamespace(prefix);
			if (namespaceUri == null)
			{
				XmlExceptionHelper.ThrowUndefinedPrefix(this, prefix);
			}
		}

		/// <summary>Converts an element's content to a <see cref="T:System.String" />.</summary>
		/// <returns>The node's content represented as a <see cref="T:System.String" />.</returns>
		// Token: 0x060004B8 RID: 1208 RVA: 0x000176F8 File Offset: 0x000158F8
		public override string ReadElementContentAsString()
		{
			string result;
			if (this.IsStartElement() && this.IsEmptyElement)
			{
				this.Read();
				result = string.Empty;
			}
			else
			{
				this.ReadStartElement();
				result = this.ReadContentAsString();
				this.ReadEndElement();
			}
			return result;
		}

		/// <summary>Converts an element's content to a <see cref="T:System.Boolean" />.</summary>
		/// <returns>The node's content represented as a <see cref="T:System.Boolean" />.</returns>
		// Token: 0x060004B9 RID: 1209 RVA: 0x0001773C File Offset: 0x0001593C
		public override bool ReadElementContentAsBoolean()
		{
			bool result;
			if (this.IsStartElement() && this.IsEmptyElement)
			{
				this.Read();
				result = XmlConverter.ToBoolean(string.Empty);
			}
			else
			{
				this.ReadStartElement();
				result = this.ReadContentAsBoolean();
				this.ReadEndElement();
			}
			return result;
		}

		/// <summary>Converts an element's content to an integer (<see cref="T:System.Int32" />).</summary>
		/// <returns>The node's content represented as an integer (<see cref="T:System.Int32" />).</returns>
		// Token: 0x060004BA RID: 1210 RVA: 0x00017784 File Offset: 0x00015984
		public override int ReadElementContentAsInt()
		{
			int result;
			if (this.IsStartElement() && this.IsEmptyElement)
			{
				this.Read();
				result = XmlConverter.ToInt32(string.Empty);
			}
			else
			{
				this.ReadStartElement();
				result = this.ReadContentAsInt();
				this.ReadEndElement();
			}
			return result;
		}

		/// <summary>Converts an element's content to a long integer (<see cref="T:System.Int64" />).</summary>
		/// <returns>The node's content represented as a long integer (<see cref="T:System.Int64" />).</returns>
		// Token: 0x060004BB RID: 1211 RVA: 0x000177CC File Offset: 0x000159CC
		public override long ReadElementContentAsLong()
		{
			long result;
			if (this.IsStartElement() && this.IsEmptyElement)
			{
				this.Read();
				result = XmlConverter.ToInt64(string.Empty);
			}
			else
			{
				this.ReadStartElement();
				result = this.ReadContentAsLong();
				this.ReadEndElement();
			}
			return result;
		}

		/// <summary>Converts an element's content to a floating point number (<see cref="T:System.Single" />).</summary>
		/// <returns>The node's content represented as a floating point number (<see cref="T:System.Single" />).</returns>
		// Token: 0x060004BC RID: 1212 RVA: 0x00017814 File Offset: 0x00015A14
		public override float ReadElementContentAsFloat()
		{
			float result;
			if (this.IsStartElement() && this.IsEmptyElement)
			{
				this.Read();
				result = XmlConverter.ToSingle(string.Empty);
			}
			else
			{
				this.ReadStartElement();
				result = this.ReadContentAsFloat();
				this.ReadEndElement();
			}
			return result;
		}

		/// <summary>Converts an element's content to a <see cref="T:System.Double" />.</summary>
		/// <returns>The node's content represented as a <see cref="T:System.Double" />.</returns>
		// Token: 0x060004BD RID: 1213 RVA: 0x0001785C File Offset: 0x00015A5C
		public override double ReadElementContentAsDouble()
		{
			double result;
			if (this.IsStartElement() && this.IsEmptyElement)
			{
				this.Read();
				result = XmlConverter.ToDouble(string.Empty);
			}
			else
			{
				this.ReadStartElement();
				result = this.ReadContentAsDouble();
				this.ReadEndElement();
			}
			return result;
		}

		/// <summary>Converts an element's content to a <see cref="T:System.Decimal" />.</summary>
		/// <returns>The node's content represented as a <see cref="T:System.Decimal" />.</returns>
		// Token: 0x060004BE RID: 1214 RVA: 0x000178A4 File Offset: 0x00015AA4
		public override decimal ReadElementContentAsDecimal()
		{
			decimal result;
			if (this.IsStartElement() && this.IsEmptyElement)
			{
				this.Read();
				result = XmlConverter.ToDecimal(string.Empty);
			}
			else
			{
				this.ReadStartElement();
				result = this.ReadContentAsDecimal();
				this.ReadEndElement();
			}
			return result;
		}

		/// <summary>Converts an element's content to a <see cref="T:System.DateTime" />.</summary>
		/// <returns>The node's content represented as a <see cref="T:System.DateTime" />.</returns>
		/// <exception cref="T:System.ArgumentException">The element is not in valid format.</exception>
		/// <exception cref="T:System.FormatException">The element is not in valid format.</exception>
		// Token: 0x060004BF RID: 1215 RVA: 0x000178EC File Offset: 0x00015AEC
		public override DateTime ReadElementContentAsDateTime()
		{
			if (this.IsStartElement() && this.IsEmptyElement)
			{
				this.Read();
				try
				{
					return DateTime.Parse(string.Empty, NumberFormatInfo.InvariantInfo);
				}
				catch (ArgumentException exception)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlExceptionHelper.CreateConversionException(string.Empty, "DateTime", exception));
				}
				catch (FormatException exception2)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlExceptionHelper.CreateConversionException(string.Empty, "DateTime", exception2));
				}
			}
			this.ReadStartElement();
			DateTime result = this.ReadContentAsDateTime();
			this.ReadEndElement();
			return result;
		}

		/// <summary>Converts an element's content to a unique identifier.</summary>
		/// <returns>The node's content represented as a unique identifier.</returns>
		/// <exception cref="T:System.ArgumentException">The element is not in valid format.</exception>
		/// <exception cref="T:System.FormatException">The element is not in valid format.</exception>
		// Token: 0x060004C0 RID: 1216 RVA: 0x00017984 File Offset: 0x00015B84
		public virtual UniqueId ReadElementContentAsUniqueId()
		{
			if (this.IsStartElement() && this.IsEmptyElement)
			{
				this.Read();
				try
				{
					return new UniqueId(string.Empty);
				}
				catch (ArgumentException exception)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlExceptionHelper.CreateConversionException(string.Empty, "UniqueId", exception));
				}
				catch (FormatException exception2)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlExceptionHelper.CreateConversionException(string.Empty, "UniqueId", exception2));
				}
			}
			this.ReadStartElement();
			UniqueId result = this.ReadContentAsUniqueId();
			this.ReadEndElement();
			return result;
		}

		/// <summary>Converts an element's content to a <see cref="T:System.Guid" />.</summary>
		/// <returns>The node's content represented as a <see cref="T:System.Guid" />.</returns>
		/// <exception cref="T:System.ArgumentException">The element is not in valid format.</exception>
		/// <exception cref="T:System.FormatException">The element is not in valid format.</exception>
		// Token: 0x060004C1 RID: 1217 RVA: 0x00017A18 File Offset: 0x00015C18
		public virtual Guid ReadElementContentAsGuid()
		{
			if (this.IsStartElement() && this.IsEmptyElement)
			{
				this.Read();
				try
				{
					return Guid.Empty;
				}
				catch (ArgumentException exception)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlExceptionHelper.CreateConversionException(string.Empty, "Guid", exception));
				}
				catch (FormatException exception2)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlExceptionHelper.CreateConversionException(string.Empty, "Guid", exception2));
				}
				catch (OverflowException exception3)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlExceptionHelper.CreateConversionException(string.Empty, "Guid", exception3));
				}
			}
			this.ReadStartElement();
			Guid result = this.ReadContentAsGuid();
			this.ReadEndElement();
			return result;
		}

		/// <summary>Converts an element's content to a <see cref="T:System.TimeSpan" />.</summary>
		/// <returns>The node's content represented as a <see cref="T:System.TimeSpan" />.</returns>
		// Token: 0x060004C2 RID: 1218 RVA: 0x00017AC8 File Offset: 0x00015CC8
		public virtual TimeSpan ReadElementContentAsTimeSpan()
		{
			TimeSpan result;
			if (this.IsStartElement() && this.IsEmptyElement)
			{
				this.Read();
				result = XmlConverter.ToTimeSpan(string.Empty);
			}
			else
			{
				this.ReadStartElement();
				result = this.ReadContentAsTimeSpan();
				this.ReadEndElement();
			}
			return result;
		}

		/// <summary>Converts a node's content to a array of Base64 bytes.</summary>
		/// <returns>The node's content represented as an array of Base64 bytes.</returns>
		// Token: 0x060004C3 RID: 1219 RVA: 0x00017B10 File Offset: 0x00015D10
		public virtual byte[] ReadElementContentAsBase64()
		{
			byte[] result;
			if (this.IsStartElement() && this.IsEmptyElement)
			{
				this.Read();
				result = new byte[0];
			}
			else
			{
				this.ReadStartElement();
				result = this.ReadContentAsBase64();
				this.ReadEndElement();
			}
			return result;
		}

		/// <summary>Converts a node's content to an array of <see langword="BinHex" /> bytes.</summary>
		/// <returns>The node's content represented as an array of <see langword="BinHex" /> bytes.</returns>
		// Token: 0x060004C4 RID: 1220 RVA: 0x00017B54 File Offset: 0x00015D54
		public virtual byte[] ReadElementContentAsBinHex()
		{
			byte[] result;
			if (this.IsStartElement() && this.IsEmptyElement)
			{
				this.Read();
				result = new byte[0];
			}
			else
			{
				this.ReadStartElement();
				result = this.ReadContentAsBinHex();
				this.ReadEndElement();
			}
			return result;
		}

		/// <summary>Gets non-atomized names.</summary>
		/// <param name="localName">The local name.</param>
		/// <param name="namespaceUri">The namespace for the local <paramref name="localName" />.</param>
		// Token: 0x060004C5 RID: 1221 RVA: 0x00017B98 File Offset: 0x00015D98
		public virtual void GetNonAtomizedNames(out string localName, out string namespaceUri)
		{
			localName = this.LocalName;
			namespaceUri = this.NamespaceURI;
		}

		/// <summary>Not implemented in this class (it always returns <see langword="false" />). May be overridden in derived classes.</summary>
		/// <param name="localName">Returns <see langword="null" />, unless overridden in a derived class. .</param>
		/// <returns>
		///   <see langword="false" />, unless overridden in a derived class.</returns>
		// Token: 0x060004C6 RID: 1222 RVA: 0x00017BAA File Offset: 0x00015DAA
		public virtual bool TryGetLocalNameAsDictionaryString(out XmlDictionaryString localName)
		{
			localName = null;
			return false;
		}

		/// <summary>Not implemented in this class (it always returns <see langword="false" />). May be overridden in derived classes.</summary>
		/// <param name="namespaceUri">Returns <see langword="null" />, unless overridden in a derived class.</param>
		/// <returns>
		///   <see langword="false" />, unless overridden in a derived class.</returns>
		// Token: 0x060004C7 RID: 1223 RVA: 0x00017BAA File Offset: 0x00015DAA
		public virtual bool TryGetNamespaceUriAsDictionaryString(out XmlDictionaryString namespaceUri)
		{
			namespaceUri = null;
			return false;
		}

		/// <summary>Not implemented in this class (it always returns <see langword="false" />). May be overridden in derived classes.</summary>
		/// <param name="value">Returns <see langword="null" />, unless overridden in a derived class.</param>
		/// <returns>
		///   <see langword="false" />, unless overridden in a derived class.</returns>
		// Token: 0x060004C8 RID: 1224 RVA: 0x00017BAA File Offset: 0x00015DAA
		public virtual bool TryGetValueAsDictionaryString(out XmlDictionaryString value)
		{
			value = null;
			return false;
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x00017BB0 File Offset: 0x00015DB0
		private void CheckArray(Array array, int offset, int count)
		{
			if (array == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("array"));
			}
			if (offset < 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("offset", System.Runtime.Serialization.SR.GetString("The value of this argument must be non-negative.")));
			}
			if (offset > array.Length)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("offset", System.Runtime.Serialization.SR.GetString("The specified offset exceeds the buffer size ({0} bytes).", new object[]
				{
					array.Length
				})));
			}
			if (count < 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("count", System.Runtime.Serialization.SR.GetString("The value of this argument must be non-negative.")));
			}
			if (count > array.Length - offset)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("count", System.Runtime.Serialization.SR.GetString("The specified size exceeds the remaining buffer space ({0} bytes).", new object[]
				{
					array.Length - offset
				})));
			}
		}

		/// <summary>Checks whether the reader is positioned at the start of an array. This class returns <see langword="false" />, but derived classes that have the concept of arrays might return <see langword="true" />.</summary>
		/// <param name="type">Type of the node, if a valid node; otherwise <see langword="null" />.</param>
		/// <returns>
		///   <see langword="true" /> if the reader is positioned at the start of an array node; otherwise <see langword="false" />.</returns>
		// Token: 0x060004CA RID: 1226 RVA: 0x00017BAA File Offset: 0x00015DAA
		public virtual bool IsStartArray(out Type type)
		{
			type = null;
			return false;
		}

		/// <summary>Not implemented in this class (it always returns <see langword="false" />). May be overridden in derived classes.</summary>
		/// <param name="count">Returns 0, unless overridden in a derived class.</param>
		/// <returns>
		///   <see langword="false" />, unless overridden in a derived class.</returns>
		// Token: 0x060004CB RID: 1227 RVA: 0x000170F0 File Offset: 0x000152F0
		public virtual bool TryGetArrayLength(out int count)
		{
			count = 0;
			return false;
		}

		/// <summary>Reads repeated occurrences of <see cref="T:System.Boolean" /> nodes into a typed array.</summary>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <returns>A <see cref="T:System.Boolean" /> array of the <see cref="T:System.Boolean" /> nodes.</returns>
		// Token: 0x060004CC RID: 1228 RVA: 0x00017C7E File Offset: 0x00015E7E
		public virtual bool[] ReadBooleanArray(string localName, string namespaceUri)
		{
			return BooleanArrayHelperWithString.Instance.ReadArray(this, localName, namespaceUri, this.Quotas.MaxArrayLength);
		}

		/// <summary>Reads repeated occurrences of <see cref="T:System.Boolean" /> nodes into a typed array.</summary>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <returns>A <see cref="T:System.Boolean" /> array of the <see cref="T:System.Boolean" /> nodes.</returns>
		// Token: 0x060004CD RID: 1229 RVA: 0x00017C98 File Offset: 0x00015E98
		public virtual bool[] ReadBooleanArray(XmlDictionaryString localName, XmlDictionaryString namespaceUri)
		{
			return BooleanArrayHelperWithDictionaryString.Instance.ReadArray(this, localName, namespaceUri, this.Quotas.MaxArrayLength);
		}

		/// <summary>Reads repeated occurrences of <see cref="T:System.Boolean" /> nodes into a typed array.</summary>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The local name of the element.</param>
		/// <param name="array">The array into which the nodes are put.</param>
		/// <param name="offset">The starting index in the array.</param>
		/// <param name="count">The number of nodes to put in the array.</param>
		/// <returns>The number of nodes put in the array.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is &lt; 0 or &gt; <paramref name="array" /> length.
		/// -or-
		/// <paramref name="count" /> is &lt; 0 or &gt; <paramref name="array" /> length minus <paramref name="offset" />.</exception>
		// Token: 0x060004CE RID: 1230 RVA: 0x00017CB4 File Offset: 0x00015EB4
		public virtual int ReadArray(string localName, string namespaceUri, bool[] array, int offset, int count)
		{
			this.CheckArray(array, offset, count);
			int num = 0;
			while (num < count && this.IsStartElement(localName, namespaceUri))
			{
				array[offset + num] = this.ReadElementContentAsBoolean();
				num++;
			}
			return num;
		}

		/// <summary>Reads repeated occurrences of <see cref="T:System.Boolean" /> nodes into a typed array.</summary>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <param name="array">The array into which the nodes are put.</param>
		/// <param name="offset">The starting index in the array.</param>
		/// <param name="count">The number of nodes to put in the array.</param>
		/// <returns>The number of nodes put in the array.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is &lt; 0 or &gt; <paramref name="array" /> length.
		/// -or-
		/// <paramref name="count" /> is &lt; 0 or &gt; <paramref name="array" /> length minus <paramref name="offset" />.</exception>
		// Token: 0x060004CF RID: 1231 RVA: 0x00017CF0 File Offset: 0x00015EF0
		public virtual int ReadArray(XmlDictionaryString localName, XmlDictionaryString namespaceUri, bool[] array, int offset, int count)
		{
			return this.ReadArray(XmlDictionaryString.GetString(localName), XmlDictionaryString.GetString(namespaceUri), array, offset, count);
		}

		/// <summary>Reads the contents of a series of nodes with the given <paramref name="localName" /> and <paramref name="namespaceUri" /> into an array of <see langword="short" /> integers (<see cref="T:System.Int16" />).</summary>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <returns>An array of <see langword="short" /> integers (<see cref="T:System.Int16" />).</returns>
		// Token: 0x060004D0 RID: 1232 RVA: 0x00017D09 File Offset: 0x00015F09
		public virtual short[] ReadInt16Array(string localName, string namespaceUri)
		{
			return Int16ArrayHelperWithString.Instance.ReadArray(this, localName, namespaceUri, this.Quotas.MaxArrayLength);
		}

		/// <summary>Reads the contents of a series of nodes with the given <paramref name="localName" /> and <paramref name="namespaceUri" /> into an array of <see langword="short" /> integers (<see cref="T:System.Int16" />).</summary>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <returns>An array of <see langword="short" /> integers (<see cref="T:System.Int16" />).</returns>
		// Token: 0x060004D1 RID: 1233 RVA: 0x00017D23 File Offset: 0x00015F23
		public virtual short[] ReadInt16Array(XmlDictionaryString localName, XmlDictionaryString namespaceUri)
		{
			return Int16ArrayHelperWithDictionaryString.Instance.ReadArray(this, localName, namespaceUri, this.Quotas.MaxArrayLength);
		}

		/// <summary>Reads repeated occurrences of <see langword="short" /> integers into a typed array.</summary>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <param name="array">The array into which the integers are put.</param>
		/// <param name="offset">The starting index in the array.</param>
		/// <param name="count">The number of integers to put in the array.</param>
		/// <returns>The number of integers put in the array.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is &lt; 0 or &gt; <paramref name="array" /> length.
		/// -or-
		/// <paramref name="count" /> is &lt; 0 or &gt; <paramref name="array" /> length minus <paramref name="offset" />.</exception>
		// Token: 0x060004D2 RID: 1234 RVA: 0x00017D40 File Offset: 0x00015F40
		public virtual int ReadArray(string localName, string namespaceUri, short[] array, int offset, int count)
		{
			this.CheckArray(array, offset, count);
			int num = 0;
			while (num < count && this.IsStartElement(localName, namespaceUri))
			{
				int num2 = this.ReadElementContentAsInt();
				if (num2 < -32768 || num2 > 32767)
				{
					XmlExceptionHelper.ThrowConversionOverflow(this, num2.ToString(NumberFormatInfo.CurrentInfo), "Int16");
				}
				array[offset + num] = (short)num2;
				num++;
			}
			return num;
		}

		/// <summary>Reads repeated occurrences of <see langword="short" /> integers into a typed array.</summary>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <param name="array">The array into which the integers are put.</param>
		/// <param name="offset">The starting index in the array.</param>
		/// <param name="count">The number of integers to put in the array.</param>
		/// <returns>The number of integers put in the array.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is &lt; 0 or &gt; <paramref name="array" /> length.
		/// -or-
		/// <paramref name="count" /> is &lt; 0 or &gt; <paramref name="array" /> length minus <paramref name="offset" />.</exception>
		// Token: 0x060004D3 RID: 1235 RVA: 0x00017DA6 File Offset: 0x00015FA6
		public virtual int ReadArray(XmlDictionaryString localName, XmlDictionaryString namespaceUri, short[] array, int offset, int count)
		{
			return this.ReadArray(XmlDictionaryString.GetString(localName), XmlDictionaryString.GetString(namespaceUri), array, offset, count);
		}

		/// <summary>Reads the contents of a series of nodes with the given <paramref name="localName" /> and <paramref name="namespaceUri" /> into an array of integers (<see cref="T:System.Int32" />).</summary>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <returns>An array of integers (<see cref="T:System.Int32" />).</returns>
		// Token: 0x060004D4 RID: 1236 RVA: 0x00017DBF File Offset: 0x00015FBF
		public virtual int[] ReadInt32Array(string localName, string namespaceUri)
		{
			return Int32ArrayHelperWithString.Instance.ReadArray(this, localName, namespaceUri, this.Quotas.MaxArrayLength);
		}

		/// <summary>Reads the contents of a series of nodes with the given <paramref name="localName" /> and <paramref name="namespaceUri" /> into an array of integers (<see cref="T:System.Int32" />).</summary>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <returns>An array of integers (<see cref="T:System.Int32" />).</returns>
		// Token: 0x060004D5 RID: 1237 RVA: 0x00017DD9 File Offset: 0x00015FD9
		public virtual int[] ReadInt32Array(XmlDictionaryString localName, XmlDictionaryString namespaceUri)
		{
			return Int32ArrayHelperWithDictionaryString.Instance.ReadArray(this, localName, namespaceUri, this.Quotas.MaxArrayLength);
		}

		/// <summary>Reads repeated occurrences of integers into a typed array.</summary>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <param name="array">The array into which the integers are put.</param>
		/// <param name="offset">The starting index in the array.</param>
		/// <param name="count">The number of integers to put in the array.</param>
		/// <returns>The number of integers put in the array.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is &lt; 0 or &gt; <paramref name="array" /> length.
		/// -or-
		/// <paramref name="count" /> is &lt; 0 or &gt; <paramref name="array" /> length minus <paramref name="offset" />.</exception>
		// Token: 0x060004D6 RID: 1238 RVA: 0x00017DF4 File Offset: 0x00015FF4
		public virtual int ReadArray(string localName, string namespaceUri, int[] array, int offset, int count)
		{
			this.CheckArray(array, offset, count);
			int num = 0;
			while (num < count && this.IsStartElement(localName, namespaceUri))
			{
				array[offset + num] = this.ReadElementContentAsInt();
				num++;
			}
			return num;
		}

		/// <summary>Reads repeated occurrences of integers into a typed array.</summary>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <param name="array">The array into which the integers are put.</param>
		/// <param name="offset">The starting index in the array.</param>
		/// <param name="count">The number of integers to put in the array.</param>
		/// <returns>The number of integers put in the array.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is &lt; 0 or &gt; <paramref name="array" /> length.
		/// -or-
		/// <paramref name="count" /> is &lt; 0 or &gt; <paramref name="array" /> length minus <paramref name="offset" />.</exception>
		// Token: 0x060004D7 RID: 1239 RVA: 0x00017E30 File Offset: 0x00016030
		public virtual int ReadArray(XmlDictionaryString localName, XmlDictionaryString namespaceUri, int[] array, int offset, int count)
		{
			return this.ReadArray(XmlDictionaryString.GetString(localName), XmlDictionaryString.GetString(namespaceUri), array, offset, count);
		}

		/// <summary>Reads the contents of a series of nodes with the given <paramref name="localName" /> and <paramref name="namespaceUri" /> into an array of <see langword="long" /> integers (<see cref="T:System.Int64" />).</summary>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <returns>An array of <see langword="long" /> integers (<see cref="T:System.Int64" />).</returns>
		// Token: 0x060004D8 RID: 1240 RVA: 0x00017E49 File Offset: 0x00016049
		public virtual long[] ReadInt64Array(string localName, string namespaceUri)
		{
			return Int64ArrayHelperWithString.Instance.ReadArray(this, localName, namespaceUri, this.Quotas.MaxArrayLength);
		}

		/// <summary>Reads the contents of a series of nodes with the given <paramref name="localName" /> and <paramref name="namespaceUri" /> into an array of <see langword="long" /> integers (<see cref="T:System.Int64" />).</summary>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <returns>An array of <see langword="long" /> integers (<see cref="T:System.Int64" />).</returns>
		// Token: 0x060004D9 RID: 1241 RVA: 0x00017E63 File Offset: 0x00016063
		public virtual long[] ReadInt64Array(XmlDictionaryString localName, XmlDictionaryString namespaceUri)
		{
			return Int64ArrayHelperWithDictionaryString.Instance.ReadArray(this, localName, namespaceUri, this.Quotas.MaxArrayLength);
		}

		/// <summary>Reads repeated occurrences of <see langword="long" /> integers into a typed array.</summary>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <param name="array">The array into which the integers are put.</param>
		/// <param name="offset">The starting index in the array.</param>
		/// <param name="count">The number of integers to put in the array.</param>
		/// <returns>The number of integers put in the array.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is &lt; 0 or &gt; <paramref name="array" /> length.
		/// -or-
		/// <paramref name="count" /> is &lt; 0 or &gt; <paramref name="array" /> length minus <paramref name="offset" />.</exception>
		// Token: 0x060004DA RID: 1242 RVA: 0x00017E80 File Offset: 0x00016080
		public virtual int ReadArray(string localName, string namespaceUri, long[] array, int offset, int count)
		{
			this.CheckArray(array, offset, count);
			int num = 0;
			while (num < count && this.IsStartElement(localName, namespaceUri))
			{
				array[offset + num] = this.ReadElementContentAsLong();
				num++;
			}
			return num;
		}

		/// <summary>Reads repeated occurrences of <see langword="long" /> integers into a typed array.</summary>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <param name="array">The array into which the integers are put.</param>
		/// <param name="offset">The starting index in the array.</param>
		/// <param name="count">The number of integers to put in the array.</param>
		/// <returns>The number of integers put in the array.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is &lt; 0 or &gt; <paramref name="array" /> length.
		/// -or-
		/// <paramref name="count" /> is &lt; 0 or &gt; <paramref name="array" /> length minus <paramref name="offset" />.</exception>
		// Token: 0x060004DB RID: 1243 RVA: 0x00017EBC File Offset: 0x000160BC
		public virtual int ReadArray(XmlDictionaryString localName, XmlDictionaryString namespaceUri, long[] array, int offset, int count)
		{
			return this.ReadArray(XmlDictionaryString.GetString(localName), XmlDictionaryString.GetString(namespaceUri), array, offset, count);
		}

		/// <summary>Reads the contents of a series of nodes with the given <paramref name="localName" /> and <paramref name="namespaceUri" /> into an array of <see langword="float" /> numbers (<see cref="T:System.Single" />).</summary>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <returns>An array of <see langword="float" /> numbers (<see cref="T:System.Single" />).</returns>
		// Token: 0x060004DC RID: 1244 RVA: 0x00017ED5 File Offset: 0x000160D5
		public virtual float[] ReadSingleArray(string localName, string namespaceUri)
		{
			return SingleArrayHelperWithString.Instance.ReadArray(this, localName, namespaceUri, this.Quotas.MaxArrayLength);
		}

		/// <summary>Reads the contents of a series of nodes with the given <paramref name="localName" /> and <paramref name="namespaceUri" /> into an array of <see langword="float" /> numbers (<see cref="T:System.Single" />).</summary>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <returns>An array of <see langword="float" /> numbers (<see cref="T:System.Single" />).</returns>
		// Token: 0x060004DD RID: 1245 RVA: 0x00017EEF File Offset: 0x000160EF
		public virtual float[] ReadSingleArray(XmlDictionaryString localName, XmlDictionaryString namespaceUri)
		{
			return SingleArrayHelperWithDictionaryString.Instance.ReadArray(this, localName, namespaceUri, this.Quotas.MaxArrayLength);
		}

		/// <summary>Reads repeated occurrences of <see langword="float" /> numbers into a typed array.</summary>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <param name="array">The array into which the float numbers are put.</param>
		/// <param name="offset">The starting index in the array.</param>
		/// <param name="count">The number of float numbers to put in the array.</param>
		/// <returns>The umber of float numbers put in the array.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is &lt; 0 or &gt; <paramref name="array" /> length.
		/// -or-
		/// <paramref name="count" /> is &lt; 0 or &gt; <paramref name="array" /> length minus <paramref name="offset" />.</exception>
		// Token: 0x060004DE RID: 1246 RVA: 0x00017F0C File Offset: 0x0001610C
		public virtual int ReadArray(string localName, string namespaceUri, float[] array, int offset, int count)
		{
			this.CheckArray(array, offset, count);
			int num = 0;
			while (num < count && this.IsStartElement(localName, namespaceUri))
			{
				array[offset + num] = this.ReadElementContentAsFloat();
				num++;
			}
			return num;
		}

		/// <summary>Reads repeated occurrences of <see langword="float" /> numbers into a typed array.</summary>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <param name="array">The array into which the float numbers are put.</param>
		/// <param name="offset">The starting index in the array.</param>
		/// <param name="count">The number of float numbers to put in the array.</param>
		/// <returns>The number of float numbers put in the array.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is &lt; 0 or &gt; <paramref name="array" /> length.
		/// -or-
		/// <paramref name="count" /> is &lt; 0 or &gt; <paramref name="array" /> length minus <paramref name="offset" />.</exception>
		// Token: 0x060004DF RID: 1247 RVA: 0x00017F48 File Offset: 0x00016148
		public virtual int ReadArray(XmlDictionaryString localName, XmlDictionaryString namespaceUri, float[] array, int offset, int count)
		{
			return this.ReadArray(XmlDictionaryString.GetString(localName), XmlDictionaryString.GetString(namespaceUri), array, offset, count);
		}

		/// <summary>Converts a node's content to a <see cref="T:System.Double" /> array.</summary>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <returns>The node's content represented as a <see cref="T:System.Double" /> array.</returns>
		// Token: 0x060004E0 RID: 1248 RVA: 0x00017F61 File Offset: 0x00016161
		public virtual double[] ReadDoubleArray(string localName, string namespaceUri)
		{
			return DoubleArrayHelperWithString.Instance.ReadArray(this, localName, namespaceUri, this.Quotas.MaxArrayLength);
		}

		/// <summary>Converts a node's content to a <see cref="T:System.Double" /> array.</summary>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <returns>The node's content represented as a <see cref="T:System.Double" /> array.</returns>
		// Token: 0x060004E1 RID: 1249 RVA: 0x00017F7B File Offset: 0x0001617B
		public virtual double[] ReadDoubleArray(XmlDictionaryString localName, XmlDictionaryString namespaceUri)
		{
			return DoubleArrayHelperWithDictionaryString.Instance.ReadArray(this, localName, namespaceUri, this.Quotas.MaxArrayLength);
		}

		/// <summary>Reads repeated occurrences of <see cref="T:System.Double" /> nodes into a typed array.</summary>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <param name="array">The array into which the nodes are put.</param>
		/// <param name="offset">The starting index in the array.</param>
		/// <param name="count">The number of nodes to put in the array.</param>
		/// <returns>The number of nodes put in the array.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is &lt; 0 or &gt; <paramref name="array" /> length.
		/// -or-
		/// <paramref name="count" /> is &lt; 0 or &gt; <paramref name="array" /> length minus <paramref name="offset" />.</exception>
		// Token: 0x060004E2 RID: 1250 RVA: 0x00017F98 File Offset: 0x00016198
		public virtual int ReadArray(string localName, string namespaceUri, double[] array, int offset, int count)
		{
			this.CheckArray(array, offset, count);
			int num = 0;
			while (num < count && this.IsStartElement(localName, namespaceUri))
			{
				array[offset + num] = this.ReadElementContentAsDouble();
				num++;
			}
			return num;
		}

		/// <summary>Reads repeated occurrences of <see cref="T:System.Double" /> nodes type into a typed array.</summary>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <param name="array">The array into which the nodes are put.</param>
		/// <param name="offset">The starting index in the array.</param>
		/// <param name="count">The number of nodes to put in the array.</param>
		/// <returns>The number of nodes put in the array.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is &lt; 0 or &gt; <paramref name="array" /> length.
		/// -or-
		/// <paramref name="count" /> is &lt; 0 or &gt; <paramref name="array" /> length minus <paramref name="offset" />.</exception>
		// Token: 0x060004E3 RID: 1251 RVA: 0x00017FD4 File Offset: 0x000161D4
		public virtual int ReadArray(XmlDictionaryString localName, XmlDictionaryString namespaceUri, double[] array, int offset, int count)
		{
			return this.ReadArray(XmlDictionaryString.GetString(localName), XmlDictionaryString.GetString(namespaceUri), array, offset, count);
		}

		/// <summary>Converts a node's content to a <see cref="T:System.Decimal" /> array.</summary>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <returns>The node's content represented as a <see cref="T:System.Decimal" /> array.</returns>
		// Token: 0x060004E4 RID: 1252 RVA: 0x00017FED File Offset: 0x000161ED
		public virtual decimal[] ReadDecimalArray(string localName, string namespaceUri)
		{
			return DecimalArrayHelperWithString.Instance.ReadArray(this, localName, namespaceUri, this.Quotas.MaxArrayLength);
		}

		/// <summary>Converts a node's content to a <see cref="T:System.Decimal" /> array.</summary>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <returns>The node's content represented as a <see cref="T:System.Decimal" /> array.</returns>
		// Token: 0x060004E5 RID: 1253 RVA: 0x00018007 File Offset: 0x00016207
		public virtual decimal[] ReadDecimalArray(XmlDictionaryString localName, XmlDictionaryString namespaceUri)
		{
			return DecimalArrayHelperWithDictionaryString.Instance.ReadArray(this, localName, namespaceUri, this.Quotas.MaxArrayLength);
		}

		/// <summary>Reads repeated occurrences of <see cref="T:System.Decimal" /> nodes into a typed array.</summary>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <param name="array">The array into which the nodes are put.</param>
		/// <param name="offset">The starting index in the array.</param>
		/// <param name="count">The number of nodes to put in the array.</param>
		/// <returns>The number of nodes put in the array.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is &lt; 0 or &gt; <paramref name="array" /> length.
		/// -or-
		/// <paramref name="count" /> is &lt; 0 or &gt; <paramref name="array" /> length minus <paramref name="offset" />.</exception>
		// Token: 0x060004E6 RID: 1254 RVA: 0x00018024 File Offset: 0x00016224
		public virtual int ReadArray(string localName, string namespaceUri, decimal[] array, int offset, int count)
		{
			this.CheckArray(array, offset, count);
			int num = 0;
			while (num < count && this.IsStartElement(localName, namespaceUri))
			{
				array[offset + num] = this.ReadElementContentAsDecimal();
				num++;
			}
			return num;
		}

		/// <summary>Reads repeated occurrences of <see cref="T:System.Decimal" /> nodes into a typed array.</summary>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <param name="array">The array into which the nodes are put.</param>
		/// <param name="offset">The starting index in the array.</param>
		/// <param name="count">The number of nodes to put in the array.</param>
		/// <returns>The number of nodes put in the array.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is &lt; 0 or &gt; <paramref name="array" /> length.
		/// -or-
		/// <paramref name="count" /> is &lt; 0 or &gt; <paramref name="array" /> length minus <paramref name="offset" />.</exception>
		// Token: 0x060004E7 RID: 1255 RVA: 0x00018064 File Offset: 0x00016264
		public virtual int ReadArray(XmlDictionaryString localName, XmlDictionaryString namespaceUri, decimal[] array, int offset, int count)
		{
			return this.ReadArray(XmlDictionaryString.GetString(localName), XmlDictionaryString.GetString(namespaceUri), array, offset, count);
		}

		/// <summary>Converts a node's content to a <see cref="T:System.DateTime" /> array.</summary>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <returns>The node's content represented as a <see cref="T:System.DateTime" /> array.</returns>
		// Token: 0x060004E8 RID: 1256 RVA: 0x0001807D File Offset: 0x0001627D
		public virtual DateTime[] ReadDateTimeArray(string localName, string namespaceUri)
		{
			return DateTimeArrayHelperWithString.Instance.ReadArray(this, localName, namespaceUri, this.Quotas.MaxArrayLength);
		}

		/// <summary>Converts a node's content to a <see cref="T:System.DateTime" /> array.</summary>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <returns>The node's content represented as a <see cref="T:System.DateTime" /> array.</returns>
		// Token: 0x060004E9 RID: 1257 RVA: 0x00018097 File Offset: 0x00016297
		public virtual DateTime[] ReadDateTimeArray(XmlDictionaryString localName, XmlDictionaryString namespaceUri)
		{
			return DateTimeArrayHelperWithDictionaryString.Instance.ReadArray(this, localName, namespaceUri, this.Quotas.MaxArrayLength);
		}

		/// <summary>Reads repeated occurrences of <see cref="T:System.DateTime" /> nodes into a typed array.</summary>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <param name="array">The array into which the nodes are put.</param>
		/// <param name="offset">The starting index in the array.</param>
		/// <param name="count">The number of nodes to put in the array.</param>
		/// <returns>The number of nodes put in the array.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is &lt; 0 or &gt; <paramref name="array" /> length.
		/// -or-
		/// <paramref name="count" /> is &lt; 0 or &gt; <paramref name="array" /> length minus <paramref name="offset" />.</exception>
		// Token: 0x060004EA RID: 1258 RVA: 0x000180B4 File Offset: 0x000162B4
		public virtual int ReadArray(string localName, string namespaceUri, DateTime[] array, int offset, int count)
		{
			this.CheckArray(array, offset, count);
			int num = 0;
			while (num < count && this.IsStartElement(localName, namespaceUri))
			{
				array[offset + num] = this.ReadElementContentAsDateTime();
				num++;
			}
			return num;
		}

		/// <summary>Reads repeated occurrences of <see cref="T:System.DateTime" /> nodes into a typed array.</summary>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <param name="array">The array into which the nodes are put.</param>
		/// <param name="offset">The starting index in the array.</param>
		/// <param name="count">The number of nodes to put in the array.</param>
		/// <returns>The number of nodes put in the array.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is &lt; 0 or &gt; <paramref name="array" /> length.
		/// -or-
		/// <paramref name="count" /> is &lt; 0 or &gt; <paramref name="array" /> length minus <paramref name="offset" />.</exception>
		// Token: 0x060004EB RID: 1259 RVA: 0x000180F4 File Offset: 0x000162F4
		public virtual int ReadArray(XmlDictionaryString localName, XmlDictionaryString namespaceUri, DateTime[] array, int offset, int count)
		{
			return this.ReadArray(XmlDictionaryString.GetString(localName), XmlDictionaryString.GetString(namespaceUri), array, offset, count);
		}

		/// <summary>Reads the contents of a series of nodes with the given <paramref name="localName" /> and <paramref name="namespaceUri" /> into an array of <see cref="T:System.Guid" />.</summary>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <returns>An array of <see cref="T:System.Guid" />.</returns>
		// Token: 0x060004EC RID: 1260 RVA: 0x0001810D File Offset: 0x0001630D
		public virtual Guid[] ReadGuidArray(string localName, string namespaceUri)
		{
			return GuidArrayHelperWithString.Instance.ReadArray(this, localName, namespaceUri, this.Quotas.MaxArrayLength);
		}

		/// <summary>Reads the contents of a series of nodes with the given <paramref name="localName" /> and <paramref name="namespaceUri" /> into an array of <see cref="T:System.Guid" />.</summary>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <returns>An array of <see cref="T:System.Guid" />.</returns>
		// Token: 0x060004ED RID: 1261 RVA: 0x00018127 File Offset: 0x00016327
		public virtual Guid[] ReadGuidArray(XmlDictionaryString localName, XmlDictionaryString namespaceUri)
		{
			return GuidArrayHelperWithDictionaryString.Instance.ReadArray(this, localName, namespaceUri, this.Quotas.MaxArrayLength);
		}

		/// <summary>Reads repeated occurrences of <see cref="T:System.Guid" /> nodes into a typed array.</summary>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <param name="array">The array into which the nodes are put.</param>
		/// <param name="offset">The starting index in the array.</param>
		/// <param name="count">The number of nodes to put in the array.</param>
		/// <returns>The number of nodes put in the array.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is &lt; 0 or &gt; <paramref name="array" /> length.
		/// -or-
		/// <paramref name="count" /> is &lt; 0 or &gt; <paramref name="array" /> length minus <paramref name="offset" />.</exception>
		// Token: 0x060004EE RID: 1262 RVA: 0x00018144 File Offset: 0x00016344
		public virtual int ReadArray(string localName, string namespaceUri, Guid[] array, int offset, int count)
		{
			this.CheckArray(array, offset, count);
			int num = 0;
			while (num < count && this.IsStartElement(localName, namespaceUri))
			{
				array[offset + num] = this.ReadElementContentAsGuid();
				num++;
			}
			return num;
		}

		/// <summary>Reads repeated occurrences of <see cref="T:System.Guid" /> nodes into a typed array.</summary>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <param name="array">The array into which the nodes are put.</param>
		/// <param name="offset">The starting index in the array.</param>
		/// <param name="count">The number of nodes to put in the array.</param>
		/// <returns>The number of nodes put in the array.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is &lt; 0 or &gt; <paramref name="array" /> length.
		/// -or-
		/// <paramref name="count" /> is &lt; 0 or &gt; <paramref name="array" /> length minus <paramref name="offset" />.</exception>
		// Token: 0x060004EF RID: 1263 RVA: 0x00018184 File Offset: 0x00016384
		public virtual int ReadArray(XmlDictionaryString localName, XmlDictionaryString namespaceUri, Guid[] array, int offset, int count)
		{
			return this.ReadArray(XmlDictionaryString.GetString(localName), XmlDictionaryString.GetString(namespaceUri), array, offset, count);
		}

		/// <summary>Reads the contents of a series of nodes with the given <paramref name="localName" /> and <paramref name="namespaceUri" /> into a <see cref="T:System.TimeSpan" /> array.</summary>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <returns>A <see cref="T:System.TimeSpan" /> array.</returns>
		// Token: 0x060004F0 RID: 1264 RVA: 0x0001819D File Offset: 0x0001639D
		public virtual TimeSpan[] ReadTimeSpanArray(string localName, string namespaceUri)
		{
			return TimeSpanArrayHelperWithString.Instance.ReadArray(this, localName, namespaceUri, this.Quotas.MaxArrayLength);
		}

		/// <summary>Reads the contents of a series of nodes with the given <paramref name="localName" /> and <paramref name="namespaceUri" /> into a <see cref="T:System.TimeSpan" /> array.</summary>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <returns>A <see cref="T:System.TimeSpan" /> array.</returns>
		// Token: 0x060004F1 RID: 1265 RVA: 0x000181B7 File Offset: 0x000163B7
		public virtual TimeSpan[] ReadTimeSpanArray(XmlDictionaryString localName, XmlDictionaryString namespaceUri)
		{
			return TimeSpanArrayHelperWithDictionaryString.Instance.ReadArray(this, localName, namespaceUri, this.Quotas.MaxArrayLength);
		}

		/// <summary>Reads repeated occurrences of <see cref="T:System.TimeSpan" /> nodes into a typed array.</summary>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <param name="array">The array into which the nodes are put.</param>
		/// <param name="offset">The starting index in the array.</param>
		/// <param name="count">The number of nodes to put in the array.</param>
		/// <returns>The number of nodes put in the array.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is &lt; 0 or &gt; <paramref name="array" /> length.
		/// -or-
		/// <paramref name="count" /> is &lt; 0 or &gt; <paramref name="array" /> length minus <paramref name="offset" />.</exception>
		// Token: 0x060004F2 RID: 1266 RVA: 0x000181D4 File Offset: 0x000163D4
		public virtual int ReadArray(string localName, string namespaceUri, TimeSpan[] array, int offset, int count)
		{
			this.CheckArray(array, offset, count);
			int num = 0;
			while (num < count && this.IsStartElement(localName, namespaceUri))
			{
				array[offset + num] = this.ReadElementContentAsTimeSpan();
				num++;
			}
			return num;
		}

		/// <summary>Reads repeated occurrences of <see cref="T:System.TimeSpan" /> nodes into a typed array.</summary>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <param name="array">The array into which the nodes are put.</param>
		/// <param name="offset">The starting index in the array.</param>
		/// <param name="count">The number of nodes to put in the array.</param>
		/// <returns>The number of nodes put in the array.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is &lt; 0 or &gt; <paramref name="array" /> length.
		/// -or-
		/// <paramref name="count" /> is &lt; 0 or &gt; <paramref name="array" /> length minus <paramref name="offset" />.</exception>
		// Token: 0x060004F3 RID: 1267 RVA: 0x00018214 File Offset: 0x00016414
		public virtual int ReadArray(XmlDictionaryString localName, XmlDictionaryString namespaceUri, TimeSpan[] array, int offset, int count)
		{
			return this.ReadArray(XmlDictionaryString.GetString(localName), XmlDictionaryString.GetString(namespaceUri), array, offset, count);
		}

		/// <summary>Creates an instance of this class.  Invoked only by its derived classes.</summary>
		// Token: 0x060004F4 RID: 1268 RVA: 0x0001822D File Offset: 0x0001642D
		protected XmlDictionaryReader()
		{
		}

		// Token: 0x0400027A RID: 634
		internal const int MaxInitialArrayLength = 65535;

		// Token: 0x02000061 RID: 97
		private class XmlWrappedReader : XmlDictionaryReader, IXmlLineInfo
		{
			// Token: 0x060004F5 RID: 1269 RVA: 0x00018235 File Offset: 0x00016435
			public XmlWrappedReader(XmlReader reader, XmlNamespaceManager nsMgr)
			{
				this.reader = reader;
				this.nsMgr = nsMgr;
			}

			// Token: 0x17000077 RID: 119
			// (get) Token: 0x060004F6 RID: 1270 RVA: 0x0001824B File Offset: 0x0001644B
			public override int AttributeCount
			{
				get
				{
					return this.reader.AttributeCount;
				}
			}

			// Token: 0x17000078 RID: 120
			// (get) Token: 0x060004F7 RID: 1271 RVA: 0x00018258 File Offset: 0x00016458
			public override string BaseURI
			{
				get
				{
					return this.reader.BaseURI;
				}
			}

			// Token: 0x17000079 RID: 121
			// (get) Token: 0x060004F8 RID: 1272 RVA: 0x00018265 File Offset: 0x00016465
			public override bool CanReadBinaryContent
			{
				get
				{
					return this.reader.CanReadBinaryContent;
				}
			}

			// Token: 0x1700007A RID: 122
			// (get) Token: 0x060004F9 RID: 1273 RVA: 0x00018272 File Offset: 0x00016472
			public override bool CanReadValueChunk
			{
				get
				{
					return this.reader.CanReadValueChunk;
				}
			}

			// Token: 0x060004FA RID: 1274 RVA: 0x0001827F File Offset: 0x0001647F
			public override void Close()
			{
				this.reader.Close();
				this.nsMgr = null;
			}

			// Token: 0x1700007B RID: 123
			// (get) Token: 0x060004FB RID: 1275 RVA: 0x00018293 File Offset: 0x00016493
			public override int Depth
			{
				get
				{
					return this.reader.Depth;
				}
			}

			// Token: 0x1700007C RID: 124
			// (get) Token: 0x060004FC RID: 1276 RVA: 0x000182A0 File Offset: 0x000164A0
			public override bool EOF
			{
				get
				{
					return this.reader.EOF;
				}
			}

			// Token: 0x060004FD RID: 1277 RVA: 0x000182AD File Offset: 0x000164AD
			public override string GetAttribute(int index)
			{
				return this.reader.GetAttribute(index);
			}

			// Token: 0x060004FE RID: 1278 RVA: 0x000182BB File Offset: 0x000164BB
			public override string GetAttribute(string name)
			{
				return this.reader.GetAttribute(name);
			}

			// Token: 0x060004FF RID: 1279 RVA: 0x000182C9 File Offset: 0x000164C9
			public override string GetAttribute(string name, string namespaceUri)
			{
				return this.reader.GetAttribute(name, namespaceUri);
			}

			// Token: 0x1700007D RID: 125
			// (get) Token: 0x06000500 RID: 1280 RVA: 0x000182D8 File Offset: 0x000164D8
			public override bool HasValue
			{
				get
				{
					return this.reader.HasValue;
				}
			}

			// Token: 0x1700007E RID: 126
			// (get) Token: 0x06000501 RID: 1281 RVA: 0x000182E5 File Offset: 0x000164E5
			public override bool IsDefault
			{
				get
				{
					return this.reader.IsDefault;
				}
			}

			// Token: 0x1700007F RID: 127
			// (get) Token: 0x06000502 RID: 1282 RVA: 0x000182F2 File Offset: 0x000164F2
			public override bool IsEmptyElement
			{
				get
				{
					return this.reader.IsEmptyElement;
				}
			}

			// Token: 0x06000503 RID: 1283 RVA: 0x000182FF File Offset: 0x000164FF
			public override bool IsStartElement(string name)
			{
				return this.reader.IsStartElement(name);
			}

			// Token: 0x06000504 RID: 1284 RVA: 0x0001830D File Offset: 0x0001650D
			public override bool IsStartElement(string localName, string namespaceUri)
			{
				return this.reader.IsStartElement(localName, namespaceUri);
			}

			// Token: 0x17000080 RID: 128
			// (get) Token: 0x06000505 RID: 1285 RVA: 0x0001831C File Offset: 0x0001651C
			public override string LocalName
			{
				get
				{
					return this.reader.LocalName;
				}
			}

			// Token: 0x06000506 RID: 1286 RVA: 0x00018329 File Offset: 0x00016529
			public override string LookupNamespace(string namespaceUri)
			{
				return this.reader.LookupNamespace(namespaceUri);
			}

			// Token: 0x06000507 RID: 1287 RVA: 0x00018337 File Offset: 0x00016537
			public override void MoveToAttribute(int index)
			{
				this.reader.MoveToAttribute(index);
			}

			// Token: 0x06000508 RID: 1288 RVA: 0x00018345 File Offset: 0x00016545
			public override bool MoveToAttribute(string name)
			{
				return this.reader.MoveToAttribute(name);
			}

			// Token: 0x06000509 RID: 1289 RVA: 0x00018353 File Offset: 0x00016553
			public override bool MoveToAttribute(string name, string namespaceUri)
			{
				return this.reader.MoveToAttribute(name, namespaceUri);
			}

			// Token: 0x0600050A RID: 1290 RVA: 0x00018362 File Offset: 0x00016562
			public override bool MoveToElement()
			{
				return this.reader.MoveToElement();
			}

			// Token: 0x0600050B RID: 1291 RVA: 0x0001836F File Offset: 0x0001656F
			public override bool MoveToFirstAttribute()
			{
				return this.reader.MoveToFirstAttribute();
			}

			// Token: 0x0600050C RID: 1292 RVA: 0x0001837C File Offset: 0x0001657C
			public override bool MoveToNextAttribute()
			{
				return this.reader.MoveToNextAttribute();
			}

			// Token: 0x17000081 RID: 129
			// (get) Token: 0x0600050D RID: 1293 RVA: 0x00018389 File Offset: 0x00016589
			public override string Name
			{
				get
				{
					return this.reader.Name;
				}
			}

			// Token: 0x17000082 RID: 130
			// (get) Token: 0x0600050E RID: 1294 RVA: 0x00018396 File Offset: 0x00016596
			public override string NamespaceURI
			{
				get
				{
					return this.reader.NamespaceURI;
				}
			}

			// Token: 0x17000083 RID: 131
			// (get) Token: 0x0600050F RID: 1295 RVA: 0x000183A3 File Offset: 0x000165A3
			public override XmlNameTable NameTable
			{
				get
				{
					return this.reader.NameTable;
				}
			}

			// Token: 0x17000084 RID: 132
			// (get) Token: 0x06000510 RID: 1296 RVA: 0x000183B0 File Offset: 0x000165B0
			public override XmlNodeType NodeType
			{
				get
				{
					return this.reader.NodeType;
				}
			}

			// Token: 0x17000085 RID: 133
			// (get) Token: 0x06000511 RID: 1297 RVA: 0x000183BD File Offset: 0x000165BD
			public override string Prefix
			{
				get
				{
					return this.reader.Prefix;
				}
			}

			// Token: 0x17000086 RID: 134
			// (get) Token: 0x06000512 RID: 1298 RVA: 0x000183CA File Offset: 0x000165CA
			public override char QuoteChar
			{
				get
				{
					return this.reader.QuoteChar;
				}
			}

			// Token: 0x06000513 RID: 1299 RVA: 0x000183D7 File Offset: 0x000165D7
			public override bool Read()
			{
				return this.reader.Read();
			}

			// Token: 0x06000514 RID: 1300 RVA: 0x000183E4 File Offset: 0x000165E4
			public override bool ReadAttributeValue()
			{
				return this.reader.ReadAttributeValue();
			}

			// Token: 0x06000515 RID: 1301 RVA: 0x000183F1 File Offset: 0x000165F1
			public override string ReadElementString(string name)
			{
				return this.reader.ReadElementString(name);
			}

			// Token: 0x06000516 RID: 1302 RVA: 0x000183FF File Offset: 0x000165FF
			public override string ReadElementString(string localName, string namespaceUri)
			{
				return this.reader.ReadElementString(localName, namespaceUri);
			}

			// Token: 0x06000517 RID: 1303 RVA: 0x0001840E File Offset: 0x0001660E
			public override string ReadInnerXml()
			{
				return this.reader.ReadInnerXml();
			}

			// Token: 0x06000518 RID: 1304 RVA: 0x0001841B File Offset: 0x0001661B
			public override string ReadOuterXml()
			{
				return this.reader.ReadOuterXml();
			}

			// Token: 0x06000519 RID: 1305 RVA: 0x00018428 File Offset: 0x00016628
			public override void ReadStartElement(string name)
			{
				this.reader.ReadStartElement(name);
			}

			// Token: 0x0600051A RID: 1306 RVA: 0x00018436 File Offset: 0x00016636
			public override void ReadStartElement(string localName, string namespaceUri)
			{
				this.reader.ReadStartElement(localName, namespaceUri);
			}

			// Token: 0x0600051B RID: 1307 RVA: 0x00018445 File Offset: 0x00016645
			public override void ReadEndElement()
			{
				this.reader.ReadEndElement();
			}

			// Token: 0x0600051C RID: 1308 RVA: 0x00018452 File Offset: 0x00016652
			public override string ReadString()
			{
				return this.reader.ReadString();
			}

			// Token: 0x17000087 RID: 135
			// (get) Token: 0x0600051D RID: 1309 RVA: 0x0001845F File Offset: 0x0001665F
			public override ReadState ReadState
			{
				get
				{
					return this.reader.ReadState;
				}
			}

			// Token: 0x0600051E RID: 1310 RVA: 0x0001846C File Offset: 0x0001666C
			public override void ResolveEntity()
			{
				this.reader.ResolveEntity();
			}

			// Token: 0x17000088 RID: 136
			public override string this[int index]
			{
				get
				{
					return this.reader[index];
				}
			}

			// Token: 0x17000089 RID: 137
			public override string this[string name]
			{
				get
				{
					return this.reader[name];
				}
			}

			// Token: 0x1700008A RID: 138
			public override string this[string name, string namespaceUri]
			{
				get
				{
					return this.reader[name, namespaceUri];
				}
			}

			// Token: 0x1700008B RID: 139
			// (get) Token: 0x06000522 RID: 1314 RVA: 0x000184A4 File Offset: 0x000166A4
			public override string Value
			{
				get
				{
					return this.reader.Value;
				}
			}

			// Token: 0x1700008C RID: 140
			// (get) Token: 0x06000523 RID: 1315 RVA: 0x000184B1 File Offset: 0x000166B1
			public override string XmlLang
			{
				get
				{
					return this.reader.XmlLang;
				}
			}

			// Token: 0x1700008D RID: 141
			// (get) Token: 0x06000524 RID: 1316 RVA: 0x000184BE File Offset: 0x000166BE
			public override XmlSpace XmlSpace
			{
				get
				{
					return this.reader.XmlSpace;
				}
			}

			// Token: 0x06000525 RID: 1317 RVA: 0x000184CB File Offset: 0x000166CB
			public override int ReadElementContentAsBase64(byte[] buffer, int offset, int count)
			{
				return this.reader.ReadElementContentAsBase64(buffer, offset, count);
			}

			// Token: 0x06000526 RID: 1318 RVA: 0x000184DB File Offset: 0x000166DB
			public override int ReadContentAsBase64(byte[] buffer, int offset, int count)
			{
				return this.reader.ReadContentAsBase64(buffer, offset, count);
			}

			// Token: 0x06000527 RID: 1319 RVA: 0x000184EB File Offset: 0x000166EB
			public override int ReadElementContentAsBinHex(byte[] buffer, int offset, int count)
			{
				return this.reader.ReadElementContentAsBinHex(buffer, offset, count);
			}

			// Token: 0x06000528 RID: 1320 RVA: 0x000184FB File Offset: 0x000166FB
			public override int ReadContentAsBinHex(byte[] buffer, int offset, int count)
			{
				return this.reader.ReadContentAsBinHex(buffer, offset, count);
			}

			// Token: 0x06000529 RID: 1321 RVA: 0x0001850B File Offset: 0x0001670B
			public override int ReadValueChunk(char[] chars, int offset, int count)
			{
				return this.reader.ReadValueChunk(chars, offset, count);
			}

			// Token: 0x1700008E RID: 142
			// (get) Token: 0x0600052A RID: 1322 RVA: 0x0001851B File Offset: 0x0001671B
			public override Type ValueType
			{
				get
				{
					return this.reader.ValueType;
				}
			}

			// Token: 0x0600052B RID: 1323 RVA: 0x00018528 File Offset: 0x00016728
			public override bool ReadContentAsBoolean()
			{
				return this.reader.ReadContentAsBoolean();
			}

			// Token: 0x0600052C RID: 1324 RVA: 0x00018535 File Offset: 0x00016735
			public override DateTime ReadContentAsDateTime()
			{
				return this.reader.ReadContentAsDateTime();
			}

			// Token: 0x0600052D RID: 1325 RVA: 0x00018542 File Offset: 0x00016742
			public override decimal ReadContentAsDecimal()
			{
				return (decimal)this.reader.ReadContentAs(typeof(decimal), null);
			}

			// Token: 0x0600052E RID: 1326 RVA: 0x0001855F File Offset: 0x0001675F
			public override double ReadContentAsDouble()
			{
				return this.reader.ReadContentAsDouble();
			}

			// Token: 0x0600052F RID: 1327 RVA: 0x0001856C File Offset: 0x0001676C
			public override int ReadContentAsInt()
			{
				return this.reader.ReadContentAsInt();
			}

			// Token: 0x06000530 RID: 1328 RVA: 0x00018579 File Offset: 0x00016779
			public override long ReadContentAsLong()
			{
				return this.reader.ReadContentAsLong();
			}

			// Token: 0x06000531 RID: 1329 RVA: 0x00018586 File Offset: 0x00016786
			public override float ReadContentAsFloat()
			{
				return this.reader.ReadContentAsFloat();
			}

			// Token: 0x06000532 RID: 1330 RVA: 0x00018593 File Offset: 0x00016793
			public override string ReadContentAsString()
			{
				return this.reader.ReadContentAsString();
			}

			// Token: 0x06000533 RID: 1331 RVA: 0x000185A0 File Offset: 0x000167A0
			public override object ReadContentAs(Type type, IXmlNamespaceResolver namespaceResolver)
			{
				return this.reader.ReadContentAs(type, namespaceResolver);
			}

			// Token: 0x06000534 RID: 1332 RVA: 0x000185B0 File Offset: 0x000167B0
			public bool HasLineInfo()
			{
				IXmlLineInfo xmlLineInfo = this.reader as IXmlLineInfo;
				return xmlLineInfo != null && xmlLineInfo.HasLineInfo();
			}

			// Token: 0x1700008F RID: 143
			// (get) Token: 0x06000535 RID: 1333 RVA: 0x000185D4 File Offset: 0x000167D4
			public int LineNumber
			{
				get
				{
					IXmlLineInfo xmlLineInfo = this.reader as IXmlLineInfo;
					if (xmlLineInfo == null)
					{
						return 1;
					}
					return xmlLineInfo.LineNumber;
				}
			}

			// Token: 0x17000090 RID: 144
			// (get) Token: 0x06000536 RID: 1334 RVA: 0x000185F8 File Offset: 0x000167F8
			public int LinePosition
			{
				get
				{
					IXmlLineInfo xmlLineInfo = this.reader as IXmlLineInfo;
					if (xmlLineInfo == null)
					{
						return 1;
					}
					return xmlLineInfo.LinePosition;
				}
			}

			// Token: 0x0400027B RID: 635
			private XmlReader reader;

			// Token: 0x0400027C RID: 636
			private XmlNamespaceManager nsMgr;
		}
	}
}
