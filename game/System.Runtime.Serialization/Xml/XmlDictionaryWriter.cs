using System;
using System.Globalization;
using System.IO;
using System.Runtime;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace System.Xml
{
	/// <summary>Represents an abstract class that Windows Communication Foundation (WCF) derives from <see cref="T:System.Xml.XmlWriter" /> to do serialization and deserialization.</summary>
	// Token: 0x02000066 RID: 102
	public abstract class XmlDictionaryWriter : XmlWriter
	{
		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000557 RID: 1367 RVA: 0x00003127 File Offset: 0x00001327
		internal virtual bool FastAsync
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x00018AFE File Offset: 0x00016CFE
		internal virtual AsyncCompletionResult WriteBase64Async(AsyncEventArgs<XmlWriteBase64AsyncArguments> state)
		{
			throw FxTrace.Exception.AsError(new NotSupportedException());
		}

		/// <summary>Asynchronously encodes the specified binary bytes as Base64 and writes out the resulting text.</summary>
		/// <param name="buffer">Byte array to encode.</param>
		/// <param name="index">The position in the buffer indicating the start of the bytes to write.</param>
		/// <param name="count">The number of bytes to write.</param>
		/// <returns>The task that represents the asynchronous <see langword="WriteBase64" /> operation.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlDictionaryWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message "An asynchronous operation is already in progress."
		/// -or-
		/// An <see cref="T:System.Xml.XmlDictionaryWriter" /> asynchronous method was called without setting the <see cref="P:System.Xml.XmlWriterSettings.Async" /> flag to <see langword="true" />. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message "Set XmlWriterSettings.Async to true if you want to use Async Methods."</exception>
		// Token: 0x06000559 RID: 1369 RVA: 0x00018B0F File Offset: 0x00016D0F
		public override Task WriteBase64Async(byte[] buffer, int index, int count)
		{
			return Task.Factory.FromAsync<byte[], int, int>(new Func<byte[], int, int, AsyncCallback, object, IAsyncResult>(this.BeginWriteBase64), new Action<IAsyncResult>(this.EndWriteBase64), buffer, index, count, null);
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x00018B39 File Offset: 0x00016D39
		internal virtual IAsyncResult BeginWriteBase64(byte[] buffer, int index, int count, AsyncCallback callback, object state)
		{
			return new XmlDictionaryWriter.WriteBase64AsyncResult(buffer, index, count, this, callback, state);
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x00018B48 File Offset: 0x00016D48
		internal virtual void EndWriteBase64(IAsyncResult result)
		{
			ScheduleActionItemAsyncResult.End(result);
		}

		/// <summary>Creates an instance of <see cref="T:System.Xml.XmlDictionaryWriter" /> that writes WCF binary XML format.</summary>
		/// <param name="stream">The stream to write to.</param>
		/// <returns>An instance of <see cref="T:System.Xml.XmlDictionaryWriter" />.</returns>
		// Token: 0x0600055C RID: 1372 RVA: 0x00018B50 File Offset: 0x00016D50
		public static XmlDictionaryWriter CreateBinaryWriter(Stream stream)
		{
			return XmlDictionaryWriter.CreateBinaryWriter(stream, null);
		}

		/// <summary>Creates an instance of <see cref="T:System.Xml.XmlDictionaryWriter" /> that writes WCF binary XML format.</summary>
		/// <param name="stream">The stream to write to.</param>
		/// <param name="dictionary">The <see cref="T:System.Xml.XmlDictionary" /> to use as the shared dictionary.</param>
		/// <returns>An instance of <see cref="T:System.Xml.XmlDictionaryWriter" />.</returns>
		// Token: 0x0600055D RID: 1373 RVA: 0x00018B59 File Offset: 0x00016D59
		public static XmlDictionaryWriter CreateBinaryWriter(Stream stream, IXmlDictionary dictionary)
		{
			return XmlDictionaryWriter.CreateBinaryWriter(stream, dictionary, null);
		}

		/// <summary>Creates an instance of <see cref="T:System.Xml.XmlDictionaryWriter" /> that writes WCF binary XML format.</summary>
		/// <param name="stream">The stream to write to.</param>
		/// <param name="dictionary">The <see cref="T:System.Xml.XmlDictionary" /> to use as the shared dictionary.</param>
		/// <param name="session">The <see cref="T:System.Xml.XmlBinaryWriterSession" /> to use.</param>
		/// <returns>An instance of <see cref="T:System.Xml.XmlDictionaryWriter" />.</returns>
		// Token: 0x0600055E RID: 1374 RVA: 0x00018B63 File Offset: 0x00016D63
		public static XmlDictionaryWriter CreateBinaryWriter(Stream stream, IXmlDictionary dictionary, XmlBinaryWriterSession session)
		{
			return XmlDictionaryWriter.CreateBinaryWriter(stream, dictionary, session, true);
		}

		/// <summary>Creates an instance of <see cref="T:System.Xml.XmlDictionaryWriter" /> that writes WCF binary XML format.</summary>
		/// <param name="stream">The stream from which to read.</param>
		/// <param name="dictionary">The <see cref="T:System.Xml.XmlDictionary" /> to use as the shared dictionary.</param>
		/// <param name="session">The <see cref="T:System.Xml.XmlBinaryWriterSession" /> to use.</param>
		/// <param name="ownsStream">
		///   <see langword="true" /> to indicate that the stream is closed by the writer when done; otherwise <see langword="false" />.</param>
		/// <returns>An instance of <see cref="T:System.Xml.XmlDictionaryWriter" />.</returns>
		// Token: 0x0600055F RID: 1375 RVA: 0x00018B6E File Offset: 0x00016D6E
		public static XmlDictionaryWriter CreateBinaryWriter(Stream stream, IXmlDictionary dictionary, XmlBinaryWriterSession session, bool ownsStream)
		{
			XmlBinaryWriter xmlBinaryWriter = new XmlBinaryWriter();
			xmlBinaryWriter.SetOutput(stream, dictionary, session, ownsStream);
			return xmlBinaryWriter;
		}

		/// <summary>Creates an instance of <see cref="T:System.Xml.XmlDictionaryWriter" /> that writes text XML.</summary>
		/// <param name="stream">The stream to write to.</param>
		/// <returns>An instance of <see cref="T:System.Xml.XmlDictionaryWriter" />.</returns>
		// Token: 0x06000560 RID: 1376 RVA: 0x00018B7F File Offset: 0x00016D7F
		public static XmlDictionaryWriter CreateTextWriter(Stream stream)
		{
			return XmlDictionaryWriter.CreateTextWriter(stream, Encoding.UTF8, true);
		}

		/// <summary>Creates an instance of <see cref="T:System.Xml.XmlDictionaryWriter" /> that writes text XML.</summary>
		/// <param name="stream">The stream to write to.</param>
		/// <param name="encoding">The character encoding of the output.</param>
		/// <returns>An instance of <see cref="T:System.Xml.XmlDictionaryWriter" />.</returns>
		// Token: 0x06000561 RID: 1377 RVA: 0x00018B8D File Offset: 0x00016D8D
		public static XmlDictionaryWriter CreateTextWriter(Stream stream, Encoding encoding)
		{
			return XmlDictionaryWriter.CreateTextWriter(stream, encoding, true);
		}

		/// <summary>Creates an instance of <see cref="T:System.Xml.XmlDictionaryWriter" /> that writes text XML.</summary>
		/// <param name="stream">The stream to write to.</param>
		/// <param name="encoding">The character encoding of the stream.</param>
		/// <param name="ownsStream">
		///   <see langword="true" /> to indicate that the stream is closed by the writer when done; otherwise <see langword="false" />.</param>
		/// <returns>An instance of <see cref="T:System.Xml.XmlDictionaryWriter" />.</returns>
		// Token: 0x06000562 RID: 1378 RVA: 0x00018B97 File Offset: 0x00016D97
		public static XmlDictionaryWriter CreateTextWriter(Stream stream, Encoding encoding, bool ownsStream)
		{
			XmlUTF8TextWriter xmlUTF8TextWriter = new XmlUTF8TextWriter();
			xmlUTF8TextWriter.SetOutput(stream, encoding, ownsStream);
			return xmlUTF8TextWriter;
		}

		/// <summary>Creates an instance of <see cref="T:System.Xml.XmlDictionaryWriter" /> that writes XML in the MTOM format.</summary>
		/// <param name="stream">The stream to write to.</param>
		/// <param name="encoding">The character encoding of the stream.</param>
		/// <param name="maxSizeInBytes">The maximum number of bytes that are buffered in the writer.</param>
		/// <param name="startInfo">An attribute in the ContentType SOAP header.</param>
		/// <returns>An instance of <see cref="T:System.Xml.XmlDictionaryWriter" />.</returns>
		// Token: 0x06000563 RID: 1379 RVA: 0x00018BA7 File Offset: 0x00016DA7
		public static XmlDictionaryWriter CreateMtomWriter(Stream stream, Encoding encoding, int maxSizeInBytes, string startInfo)
		{
			return XmlDictionaryWriter.CreateMtomWriter(stream, encoding, maxSizeInBytes, startInfo, null, null, true, true);
		}

		/// <summary>Creates an instance of <see cref="T:System.Xml.XmlDictionaryWriter" /> that writes XML in the MTOM format.</summary>
		/// <param name="stream">The stream to write to.</param>
		/// <param name="encoding">The character encoding of the stream.</param>
		/// <param name="maxSizeInBytes">The maximum number of bytes that are buffered in the writer.</param>
		/// <param name="startInfo">The content-type of the MIME part that contains the Infoset.</param>
		/// <param name="boundary">The MIME boundary in the message.</param>
		/// <param name="startUri">The content-id URI of the MIME part that contains the Infoset.</param>
		/// <param name="writeMessageHeaders">
		///   <see langword="true" /> to write message headers.</param>
		/// <param name="ownsStream">
		///   <see langword="true" /> to indicate that the stream is closed by the writer when done; otherwise <see langword="false" />.</param>
		/// <returns>An instance of <see cref="T:System.Xml.XmlDictionaryWriter" />.</returns>
		// Token: 0x06000564 RID: 1380 RVA: 0x00018BB8 File Offset: 0x00016DB8
		public static XmlDictionaryWriter CreateMtomWriter(Stream stream, Encoding encoding, int maxSizeInBytes, string startInfo, string boundary, string startUri, bool writeMessageHeaders, bool ownsStream)
		{
			XmlMtomWriter xmlMtomWriter = new XmlMtomWriter();
			xmlMtomWriter.SetOutput(stream, encoding, maxSizeInBytes, startInfo, boundary, startUri, writeMessageHeaders, ownsStream);
			return xmlMtomWriter;
		}

		/// <summary>Creates an instance of <see cref="T:System.Xml.XmlDictionaryWriter" /> from an existing <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="writer">An instance of <see cref="T:System.Xml.XmlWriter" />.</param>
		/// <returns>An instance of <see cref="T:System.Xml.XmlDictionaryWriter" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="writer" /> is <see langword="null" />.</exception>
		// Token: 0x06000565 RID: 1381 RVA: 0x00018BDC File Offset: 0x00016DDC
		public static XmlDictionaryWriter CreateDictionaryWriter(XmlWriter writer)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			XmlDictionaryWriter xmlDictionaryWriter = writer as XmlDictionaryWriter;
			if (xmlDictionaryWriter == null)
			{
				xmlDictionaryWriter = new XmlDictionaryWriter.XmlWrappedWriter(writer);
			}
			return xmlDictionaryWriter;
		}

		/// <summary>Writes the specified start tag and associates it with the given namespace.</summary>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <exception cref="T:System.InvalidOperationException">The writer is closed.</exception>
		// Token: 0x06000566 RID: 1382 RVA: 0x00018C09 File Offset: 0x00016E09
		public void WriteStartElement(XmlDictionaryString localName, XmlDictionaryString namespaceUri)
		{
			this.WriteStartElement(null, localName, namespaceUri);
		}

		/// <summary>Writes the specified start tag and associates it with the given namespace and prefix.</summary>
		/// <param name="prefix">The prefix of the element.</param>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <exception cref="T:System.InvalidOperationException">The writer is closed.</exception>
		// Token: 0x06000567 RID: 1383 RVA: 0x00018C14 File Offset: 0x00016E14
		public virtual void WriteStartElement(string prefix, XmlDictionaryString localName, XmlDictionaryString namespaceUri)
		{
			this.WriteStartElement(prefix, XmlDictionaryString.GetString(localName), XmlDictionaryString.GetString(namespaceUri));
		}

		/// <summary>Writes the start of an attribute with the specified local name, and namespace URI.</summary>
		/// <param name="localName">The local name of the attribute.</param>
		/// <param name="namespaceUri">The namespace URI of the attribute.</param>
		// Token: 0x06000568 RID: 1384 RVA: 0x00018C29 File Offset: 0x00016E29
		public void WriteStartAttribute(XmlDictionaryString localName, XmlDictionaryString namespaceUri)
		{
			this.WriteStartAttribute(null, localName, namespaceUri);
		}

		/// <summary>Writes the start of an attribute with the specified prefix, local name, and namespace URI.</summary>
		/// <param name="prefix">The namespace prefix.</param>
		/// <param name="localName">The local name of the attribute.</param>
		/// <param name="namespaceUri">The namespace URI of the attribute.</param>
		// Token: 0x06000569 RID: 1385 RVA: 0x00018C34 File Offset: 0x00016E34
		public virtual void WriteStartAttribute(string prefix, XmlDictionaryString localName, XmlDictionaryString namespaceUri)
		{
			this.WriteStartAttribute(prefix, XmlDictionaryString.GetString(localName), XmlDictionaryString.GetString(namespaceUri));
		}

		/// <summary>Writes an attribute qualified name and value.</summary>
		/// <param name="localName">The local name of the attribute.</param>
		/// <param name="namespaceUri">The namespace URI of the attribute.</param>
		/// <param name="value">The attribute.</param>
		// Token: 0x0600056A RID: 1386 RVA: 0x00018C49 File Offset: 0x00016E49
		public void WriteAttributeString(XmlDictionaryString localName, XmlDictionaryString namespaceUri, string value)
		{
			this.WriteAttributeString(null, localName, namespaceUri, value);
		}

		/// <summary>Writes a namespace declaration attribute.</summary>
		/// <param name="prefix">The prefix that is bound to the given namespace.</param>
		/// <param name="namespaceUri">The namespace to which the prefix is bound.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="namespaceUri" /> is <see langword="null" />.</exception>
		// Token: 0x0600056B RID: 1387 RVA: 0x00018C58 File Offset: 0x00016E58
		public virtual void WriteXmlnsAttribute(string prefix, string namespaceUri)
		{
			if (namespaceUri == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("namespaceUri");
			}
			if (prefix == null)
			{
				if (this.LookupPrefix(namespaceUri) != null)
				{
					return;
				}
				prefix = ((namespaceUri.Length == 0) ? string.Empty : ("d" + namespaceUri.Length.ToString(NumberFormatInfo.InvariantInfo)));
			}
			base.WriteAttributeString("xmlns", prefix, null, namespaceUri);
		}

		/// <summary>Writes a namespace declaration attribute.</summary>
		/// <param name="prefix">The prefix that is bound to the given namespace.</param>
		/// <param name="namespaceUri">The namespace to which the prefix is bound.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="namespaceUri" /> is <see langword="null" />.</exception>
		// Token: 0x0600056C RID: 1388 RVA: 0x00018CBC File Offset: 0x00016EBC
		public virtual void WriteXmlnsAttribute(string prefix, XmlDictionaryString namespaceUri)
		{
			this.WriteXmlnsAttribute(prefix, XmlDictionaryString.GetString(namespaceUri));
		}

		/// <summary>Writes a standard XML attribute in the current node.</summary>
		/// <param name="localName">The local name of the attribute.</param>
		/// <param name="value">The value of the attribute.</param>
		// Token: 0x0600056D RID: 1389 RVA: 0x00018CCB File Offset: 0x00016ECB
		public virtual void WriteXmlAttribute(string localName, string value)
		{
			base.WriteAttributeString("xml", localName, null, value);
		}

		/// <summary>Writes an XML attribute in the current node.</summary>
		/// <param name="localName">The local name of the attribute.</param>
		/// <param name="value">The value of the attribute.</param>
		// Token: 0x0600056E RID: 1390 RVA: 0x00018CDB File Offset: 0x00016EDB
		public virtual void WriteXmlAttribute(XmlDictionaryString localName, XmlDictionaryString value)
		{
			this.WriteXmlAttribute(XmlDictionaryString.GetString(localName), XmlDictionaryString.GetString(value));
		}

		/// <summary>Writes an attribute qualified name and value.</summary>
		/// <param name="prefix">The prefix of the attribute.</param>
		/// <param name="localName">The local name of the attribute.</param>
		/// <param name="namespaceUri">The namespace URI of the attribute.</param>
		/// <param name="value">The attribute.</param>
		// Token: 0x0600056F RID: 1391 RVA: 0x00018CEF File Offset: 0x00016EEF
		public void WriteAttributeString(string prefix, XmlDictionaryString localName, XmlDictionaryString namespaceUri, string value)
		{
			this.WriteStartAttribute(prefix, localName, namespaceUri);
			this.WriteString(value);
			this.WriteEndAttribute();
		}

		/// <summary>Writes an element with a text content.</summary>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <param name="value">The element content.</param>
		// Token: 0x06000570 RID: 1392 RVA: 0x00018D08 File Offset: 0x00016F08
		public void WriteElementString(XmlDictionaryString localName, XmlDictionaryString namespaceUri, string value)
		{
			this.WriteElementString(null, localName, namespaceUri, value);
		}

		/// <summary>Writes an element with a text content.</summary>
		/// <param name="prefix">The prefix of the element.</param>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <param name="value">The element content.</param>
		// Token: 0x06000571 RID: 1393 RVA: 0x00018D14 File Offset: 0x00016F14
		public void WriteElementString(string prefix, XmlDictionaryString localName, XmlDictionaryString namespaceUri, string value)
		{
			this.WriteStartElement(prefix, localName, namespaceUri);
			this.WriteString(value);
			this.WriteEndElement();
		}

		/// <summary>Writes the given text content.</summary>
		/// <param name="value">The text to write.</param>
		// Token: 0x06000572 RID: 1394 RVA: 0x00018D2D File Offset: 0x00016F2D
		public virtual void WriteString(XmlDictionaryString value)
		{
			this.WriteString(XmlDictionaryString.GetString(value));
		}

		/// <summary>Writes out the namespace-qualified name. This method looks up the prefix that is in scope for the given namespace.</summary>
		/// <param name="localName">The local name of the qualified name.</param>
		/// <param name="namespaceUri">The namespace URI of the qualified name.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="localName" /> is <see langword="null" />.</exception>
		// Token: 0x06000573 RID: 1395 RVA: 0x00018D3B File Offset: 0x00016F3B
		public virtual void WriteQualifiedName(XmlDictionaryString localName, XmlDictionaryString namespaceUri)
		{
			if (localName == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("localName"));
			}
			if (namespaceUri == null)
			{
				namespaceUri = XmlDictionaryString.Empty;
			}
			this.WriteQualifiedName(localName.Value, namespaceUri.Value);
		}

		/// <summary>Writes a <see cref="T:System.Xml.XmlDictionaryString" /> value.</summary>
		/// <param name="value">The <see cref="T:System.Xml.XmlDictionaryString" /> value.</param>
		// Token: 0x06000574 RID: 1396 RVA: 0x00018D6C File Offset: 0x00016F6C
		public virtual void WriteValue(XmlDictionaryString value)
		{
			this.WriteValue(XmlDictionaryString.GetString(value));
		}

		/// <summary>Writes a value from an <see cref="T:System.Xml.IStreamProvider" />.</summary>
		/// <param name="value">The <see cref="T:System.Xml.IStreamProvider" /> value to write.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Xml.XmlException">
		///   <paramref name="value" /> returns a <see langword="null" /> stream object.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlDictionaryWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message "An asynchronous operation is already in progress."</exception>
		// Token: 0x06000575 RID: 1397 RVA: 0x00018D7C File Offset: 0x00016F7C
		public virtual void WriteValue(IStreamProvider value)
		{
			if (value == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("value"));
			}
			Stream stream = value.GetStream();
			if (stream == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(System.Runtime.Serialization.SR.GetString("Stream returned by IStreamProvider cannot be null.")));
			}
			int num = 256;
			byte[] buffer = new byte[num];
			for (;;)
			{
				int num2 = stream.Read(buffer, 0, num);
				if (num2 <= 0)
				{
					break;
				}
				this.WriteBase64(buffer, 0, num2);
				if (num < 65536 && num2 == num)
				{
					num *= 16;
					buffer = new byte[num];
				}
			}
			value.ReleaseStream(stream);
		}

		/// <summary>Asynchronously writes a value from an <see cref="T:System.Xml.IStreamProvider" />.</summary>
		/// <param name="value">The <see cref="T:System.Xml.IStreamProvider" /> value to write.</param>
		/// <returns>The task that represents the asynchronous <see langword="WriteValue" /> operation.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlDictionaryWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message "An asynchronous operation is already in progress."
		/// -or-
		/// An <see cref="T:System.Xml.XmlDictionaryWriter" /> asynchronous method was called without setting the <see cref="P:System.Xml.XmlWriterSettings.Async" /> flag to <see langword="true" />. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message "Set XmlWriterSettings.Async to true if you want to use Async Methods."</exception>
		// Token: 0x06000576 RID: 1398 RVA: 0x00018E02 File Offset: 0x00017002
		public virtual Task WriteValueAsync(IStreamProvider value)
		{
			return Task.Factory.FromAsync<IStreamProvider>(new Func<IStreamProvider, AsyncCallback, object, IAsyncResult>(this.BeginWriteValue), new Action<IAsyncResult>(this.EndWriteValue), value, null);
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x00018E2A File Offset: 0x0001702A
		internal virtual IAsyncResult BeginWriteValue(IStreamProvider value, AsyncCallback callback, object state)
		{
			if (value == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("value"));
			}
			if (this.FastAsync)
			{
				return new XmlDictionaryWriter.WriteValueFastAsyncResult(this, value, callback, state);
			}
			return new XmlDictionaryWriter.WriteValueAsyncResult(this, value, callback, state);
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x00018E5A File Offset: 0x0001705A
		internal virtual void EndWriteValue(IAsyncResult result)
		{
			if (this.FastAsync)
			{
				XmlDictionaryWriter.WriteValueFastAsyncResult.End(result);
				return;
			}
			XmlDictionaryWriter.WriteValueAsyncResult.End(result);
		}

		/// <summary>Writes a Unique Id value.</summary>
		/// <param name="value">The Unique Id value to write.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x06000579 RID: 1401 RVA: 0x00018E71 File Offset: 0x00017071
		public virtual void WriteValue(UniqueId value)
		{
			if (value == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
			}
			this.WriteString(value.ToString());
		}

		/// <summary>Writes a <see cref="T:System.Guid" /> value.</summary>
		/// <param name="value">The <see cref="T:System.Guid" /> value to write.</param>
		// Token: 0x0600057A RID: 1402 RVA: 0x00018E93 File Offset: 0x00017093
		public virtual void WriteValue(Guid value)
		{
			this.WriteString(value.ToString());
		}

		/// <summary>Writes a <see cref="T:System.TimeSpan" /> value.</summary>
		/// <param name="value">The <see cref="T:System.TimeSpan" /> value to write.</param>
		// Token: 0x0600057B RID: 1403 RVA: 0x00018EA8 File Offset: 0x000170A8
		public virtual void WriteValue(TimeSpan value)
		{
			this.WriteString(XmlConvert.ToString(value));
		}

		/// <summary>This property always returns <see langword="false" />. Its derived classes can override to return <see langword="true" /> if they support canonicalization.</summary>
		/// <returns>
		///   <see langword="false" /> in all cases.</returns>
		// Token: 0x1700009E RID: 158
		// (get) Token: 0x0600057C RID: 1404 RVA: 0x00003127 File Offset: 0x00001327
		public virtual bool CanCanonicalize
		{
			get
			{
				return false;
			}
		}

		/// <summary>When implemented by a derived class, it starts the canonicalization.</summary>
		/// <param name="stream">The stream to write to.</param>
		/// <param name="includeComments">
		///   <see langword="true" /> to include comments; otherwise, <see langword="false" />.</param>
		/// <param name="inclusivePrefixes">The prefixes to be included.</param>
		/// <exception cref="T:System.NotSupportedException">Method is not implemented yet.</exception>
		// Token: 0x0600057D RID: 1405 RVA: 0x00003141 File Offset: 0x00001341
		public virtual void StartCanonicalization(Stream stream, bool includeComments, string[] inclusivePrefixes)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
		}

		/// <summary>When implemented by a derived class, it stops the canonicalization started by the matching <see cref="M:System.Xml.XmlDictionaryWriter.StartCanonicalization(System.IO.Stream,System.Boolean,System.String[])" /> call.</summary>
		/// <exception cref="T:System.NotSupportedException">Method is not implemented yet.</exception>
		// Token: 0x0600057E RID: 1406 RVA: 0x00003141 File Offset: 0x00001341
		public virtual void EndCanonicalization()
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x00018EB8 File Offset: 0x000170B8
		private void WriteElementNode(XmlDictionaryReader reader, bool defattr)
		{
			XmlDictionaryString localName;
			XmlDictionaryString namespaceUri;
			if (reader.TryGetLocalNameAsDictionaryString(out localName) && reader.TryGetNamespaceUriAsDictionaryString(out namespaceUri))
			{
				this.WriteStartElement(reader.Prefix, localName, namespaceUri);
			}
			else
			{
				this.WriteStartElement(reader.Prefix, reader.LocalName, reader.NamespaceURI);
			}
			if ((defattr || (!reader.IsDefault && (reader.SchemaInfo == null || !reader.SchemaInfo.IsDefault))) && reader.MoveToFirstAttribute())
			{
				do
				{
					if (reader.TryGetLocalNameAsDictionaryString(out localName) && reader.TryGetNamespaceUriAsDictionaryString(out namespaceUri))
					{
						this.WriteStartAttribute(reader.Prefix, localName, namespaceUri);
					}
					else
					{
						this.WriteStartAttribute(reader.Prefix, reader.LocalName, reader.NamespaceURI);
					}
					while (reader.ReadAttributeValue())
					{
						if (reader.NodeType == XmlNodeType.EntityReference)
						{
							this.WriteEntityRef(reader.Name);
						}
						else
						{
							this.WriteTextNode(reader, true);
						}
					}
					this.WriteEndAttribute();
				}
				while (reader.MoveToNextAttribute());
				reader.MoveToElement();
			}
			if (reader.IsEmptyElement)
			{
				this.WriteEndElement();
			}
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x00018FB8 File Offset: 0x000171B8
		private void WriteArrayNode(XmlDictionaryReader reader, string prefix, string localName, string namespaceUri, Type type)
		{
			if (type == typeof(bool))
			{
				BooleanArrayHelperWithString.Instance.WriteArray(this, prefix, localName, namespaceUri, reader);
				return;
			}
			if (type == typeof(short))
			{
				Int16ArrayHelperWithString.Instance.WriteArray(this, prefix, localName, namespaceUri, reader);
				return;
			}
			if (type == typeof(int))
			{
				Int32ArrayHelperWithString.Instance.WriteArray(this, prefix, localName, namespaceUri, reader);
				return;
			}
			if (type == typeof(long))
			{
				Int64ArrayHelperWithString.Instance.WriteArray(this, prefix, localName, namespaceUri, reader);
				return;
			}
			if (type == typeof(float))
			{
				SingleArrayHelperWithString.Instance.WriteArray(this, prefix, localName, namespaceUri, reader);
				return;
			}
			if (type == typeof(double))
			{
				DoubleArrayHelperWithString.Instance.WriteArray(this, prefix, localName, namespaceUri, reader);
				return;
			}
			if (type == typeof(decimal))
			{
				DecimalArrayHelperWithString.Instance.WriteArray(this, prefix, localName, namespaceUri, reader);
				return;
			}
			if (type == typeof(DateTime))
			{
				DateTimeArrayHelperWithString.Instance.WriteArray(this, prefix, localName, namespaceUri, reader);
				return;
			}
			if (type == typeof(Guid))
			{
				GuidArrayHelperWithString.Instance.WriteArray(this, prefix, localName, namespaceUri, reader);
				return;
			}
			if (type == typeof(TimeSpan))
			{
				TimeSpanArrayHelperWithString.Instance.WriteArray(this, prefix, localName, namespaceUri, reader);
				return;
			}
			this.WriteElementNode(reader, false);
			reader.Read();
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x0001913C File Offset: 0x0001733C
		private void WriteArrayNode(XmlDictionaryReader reader, string prefix, XmlDictionaryString localName, XmlDictionaryString namespaceUri, Type type)
		{
			if (type == typeof(bool))
			{
				BooleanArrayHelperWithDictionaryString.Instance.WriteArray(this, prefix, localName, namespaceUri, reader);
				return;
			}
			if (type == typeof(short))
			{
				Int16ArrayHelperWithDictionaryString.Instance.WriteArray(this, prefix, localName, namespaceUri, reader);
				return;
			}
			if (type == typeof(int))
			{
				Int32ArrayHelperWithDictionaryString.Instance.WriteArray(this, prefix, localName, namespaceUri, reader);
				return;
			}
			if (type == typeof(long))
			{
				Int64ArrayHelperWithDictionaryString.Instance.WriteArray(this, prefix, localName, namespaceUri, reader);
				return;
			}
			if (type == typeof(float))
			{
				SingleArrayHelperWithDictionaryString.Instance.WriteArray(this, prefix, localName, namespaceUri, reader);
				return;
			}
			if (type == typeof(double))
			{
				DoubleArrayHelperWithDictionaryString.Instance.WriteArray(this, prefix, localName, namespaceUri, reader);
				return;
			}
			if (type == typeof(decimal))
			{
				DecimalArrayHelperWithDictionaryString.Instance.WriteArray(this, prefix, localName, namespaceUri, reader);
				return;
			}
			if (type == typeof(DateTime))
			{
				DateTimeArrayHelperWithDictionaryString.Instance.WriteArray(this, prefix, localName, namespaceUri, reader);
				return;
			}
			if (type == typeof(Guid))
			{
				GuidArrayHelperWithDictionaryString.Instance.WriteArray(this, prefix, localName, namespaceUri, reader);
				return;
			}
			if (type == typeof(TimeSpan))
			{
				TimeSpanArrayHelperWithDictionaryString.Instance.WriteArray(this, prefix, localName, namespaceUri, reader);
				return;
			}
			this.WriteElementNode(reader, false);
			reader.Read();
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x000192C0 File Offset: 0x000174C0
		private void WriteArrayNode(XmlDictionaryReader reader, Type type)
		{
			XmlDictionaryString localName;
			XmlDictionaryString namespaceUri;
			if (reader.TryGetLocalNameAsDictionaryString(out localName) && reader.TryGetNamespaceUriAsDictionaryString(out namespaceUri))
			{
				this.WriteArrayNode(reader, reader.Prefix, localName, namespaceUri, type);
				return;
			}
			this.WriteArrayNode(reader, reader.Prefix, reader.LocalName, reader.NamespaceURI, type);
		}

		/// <summary>Writes the text node that an <see cref="T:System.Xml.XmlDictionaryReader" /> is currently positioned on.</summary>
		/// <param name="reader">The <see cref="T:System.Xml.XmlDictionaryReader" /> to get the text value from.</param>
		/// <param name="isAttribute">
		///   <see langword="true" /> to indicate that the reader is positioned on an attribute value or element content; otherwise, <see langword="false" />.</param>
		// Token: 0x06000583 RID: 1411 RVA: 0x0001930C File Offset: 0x0001750C
		protected virtual void WriteTextNode(XmlDictionaryReader reader, bool isAttribute)
		{
			XmlDictionaryString value;
			if (reader.TryGetValueAsDictionaryString(out value))
			{
				this.WriteString(value);
			}
			else
			{
				this.WriteString(reader.Value);
			}
			if (!isAttribute)
			{
				reader.Read();
			}
		}

		/// <summary>Writes the current XML node from an <see cref="T:System.Xml.XmlReader" />.</summary>
		/// <param name="reader">The <see cref="T:System.Xml.XmlReader" />.</param>
		/// <param name="defattr">
		///   <see langword="true" /> to copy the default attributes from the <see cref="T:System.Xml.XmlReader" />; otherwise, <see langword="false" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="reader" /> is <see langword="null" />.</exception>
		// Token: 0x06000584 RID: 1412 RVA: 0x00019344 File Offset: 0x00017544
		public override void WriteNode(XmlReader reader, bool defattr)
		{
			XmlDictionaryReader xmlDictionaryReader = reader as XmlDictionaryReader;
			if (xmlDictionaryReader != null)
			{
				this.WriteNode(xmlDictionaryReader, defattr);
				return;
			}
			base.WriteNode(reader, defattr);
		}

		/// <summary>Writes the current XML node from an <see cref="T:System.Xml.XmlDictionaryReader" />.</summary>
		/// <param name="reader">The <see cref="T:System.Xml.XmlDictionaryReader" />.</param>
		/// <param name="defattr">
		///   <see langword="true" /> to copy the default attributes from the <see langword="XmlReader" />; otherwise, <see langword="false" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="reader" /> is <see langword="null" />.</exception>
		// Token: 0x06000585 RID: 1413 RVA: 0x0001936C File Offset: 0x0001756C
		public virtual void WriteNode(XmlDictionaryReader reader, bool defattr)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("reader"));
			}
			int num = (reader.NodeType == XmlNodeType.None) ? -1 : reader.Depth;
			do
			{
				XmlNodeType nodeType = reader.NodeType;
				if (nodeType == XmlNodeType.Text || nodeType == XmlNodeType.Whitespace || nodeType == XmlNodeType.SignificantWhitespace)
				{
					this.WriteTextNode(reader, false);
				}
				else
				{
					Type type;
					if (reader.Depth <= num || !reader.IsStartArray(out type))
					{
						switch (nodeType)
						{
						case XmlNodeType.Element:
							this.WriteElementNode(reader, defattr);
							break;
						case XmlNodeType.Attribute:
						case XmlNodeType.Text:
						case XmlNodeType.Entity:
						case XmlNodeType.Document:
							break;
						case XmlNodeType.CDATA:
							this.WriteCData(reader.Value);
							break;
						case XmlNodeType.EntityReference:
							this.WriteEntityRef(reader.Name);
							break;
						case XmlNodeType.ProcessingInstruction:
							goto IL_C9;
						case XmlNodeType.Comment:
							this.WriteComment(reader.Value);
							break;
						case XmlNodeType.DocumentType:
							this.WriteDocType(reader.Name, reader.GetAttribute("PUBLIC"), reader.GetAttribute("SYSTEM"), reader.Value);
							break;
						default:
							if (nodeType != XmlNodeType.EndElement)
							{
								if (nodeType == XmlNodeType.XmlDeclaration)
								{
									goto IL_C9;
								}
							}
							else
							{
								this.WriteFullEndElement();
							}
							break;
						}
						IL_11B:
						if (reader.Read())
						{
							goto IL_123;
						}
						break;
						IL_C9:
						this.WriteProcessingInstruction(reader.Name, reader.Value);
						goto IL_11B;
					}
					this.WriteArrayNode(reader, type);
				}
				IL_123:;
			}
			while (num < reader.Depth || (num == reader.Depth && reader.NodeType == XmlNodeType.EndElement));
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x000194C0 File Offset: 0x000176C0
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

		/// <summary>Writes nodes from a <see cref="T:System.Boolean" /> array.</summary>
		/// <param name="prefix">The namespace prefix.</param>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <param name="array">The array that contains the data.</param>
		/// <param name="offset">The starting index in the array.</param>
		/// <param name="count">The number of values to write from the array.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is &lt; 0 or &gt; <paramref name="array" /> length.
		/// -or-
		/// <paramref name="count" /> is &lt; 0 or &gt; <paramref name="array" /> length minus <paramref name="offset" />.</exception>
		// Token: 0x06000587 RID: 1415 RVA: 0x00019590 File Offset: 0x00017790
		public virtual void WriteArray(string prefix, string localName, string namespaceUri, bool[] array, int offset, int count)
		{
			this.CheckArray(array, offset, count);
			for (int i = 0; i < count; i++)
			{
				this.WriteStartElement(prefix, localName, namespaceUri);
				this.WriteValue(array[offset + i]);
				this.WriteEndElement();
			}
		}

		/// <summary>Writes nodes from a <see cref="T:System.Boolean" /> array.</summary>
		/// <param name="prefix">The namespace prefix.</param>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <param name="array">The array that contains the nodes.</param>
		/// <param name="offset">The starting index in the array.</param>
		/// <param name="count">The number of nodes to get from the array.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is &lt; 0 or &gt; <paramref name="array" /> length.
		/// -or-
		/// <paramref name="count" /> is &lt; 0 or &gt; <paramref name="array" /> length minus <paramref name="offset" />.</exception>
		// Token: 0x06000588 RID: 1416 RVA: 0x000195D2 File Offset: 0x000177D2
		public virtual void WriteArray(string prefix, XmlDictionaryString localName, XmlDictionaryString namespaceUri, bool[] array, int offset, int count)
		{
			this.WriteArray(prefix, XmlDictionaryString.GetString(localName), XmlDictionaryString.GetString(namespaceUri), array, offset, count);
		}

		/// <summary>Writes nodes from a <see cref="T:System.Int16" /> array.</summary>
		/// <param name="prefix">The namespace prefix.</param>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <param name="array">The array that contains the nodes.</param>
		/// <param name="offset">The starting index in the array.</param>
		/// <param name="count">The number of nodes to get from the array.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is &lt; 0 or &gt; <paramref name="array" /> length.
		/// -or-
		/// <paramref name="count" /> is &lt; 0 or &gt; <paramref name="array" /> length minus <paramref name="offset" />.</exception>
		// Token: 0x06000589 RID: 1417 RVA: 0x000195F0 File Offset: 0x000177F0
		public virtual void WriteArray(string prefix, string localName, string namespaceUri, short[] array, int offset, int count)
		{
			this.CheckArray(array, offset, count);
			for (int i = 0; i < count; i++)
			{
				this.WriteStartElement(prefix, localName, namespaceUri);
				this.WriteValue((int)array[offset + i]);
				this.WriteEndElement();
			}
		}

		/// <summary>Writes nodes from a <see cref="T:System.Int16" /> array.</summary>
		/// <param name="prefix">The namespace prefix.</param>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <param name="array">The array that contains the nodes.</param>
		/// <param name="offset">The starting index in the array.</param>
		/// <param name="count">The number of nodes to get from the array.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is &lt; 0 or &gt; <paramref name="array" /> length.
		/// -or-
		/// <paramref name="count" /> is &lt; 0 or &gt; <paramref name="array" /> length minus <paramref name="offset" />.</exception>
		// Token: 0x0600058A RID: 1418 RVA: 0x00019632 File Offset: 0x00017832
		public virtual void WriteArray(string prefix, XmlDictionaryString localName, XmlDictionaryString namespaceUri, short[] array, int offset, int count)
		{
			this.WriteArray(prefix, XmlDictionaryString.GetString(localName), XmlDictionaryString.GetString(namespaceUri), array, offset, count);
		}

		/// <summary>Writes nodes from a <see cref="T:System.Int32" /> array.</summary>
		/// <param name="prefix">The namespace prefix.</param>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <param name="array">The array that contains the nodes.</param>
		/// <param name="offset">The starting index in the array.</param>
		/// <param name="count">The number of nodes to get from the array.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is &lt; 0 or &gt; <paramref name="array" /> length.
		/// -or-
		/// <paramref name="count" /> is &lt; 0 or &gt; <paramref name="array" /> length minus <paramref name="offset" />.</exception>
		// Token: 0x0600058B RID: 1419 RVA: 0x00019650 File Offset: 0x00017850
		public virtual void WriteArray(string prefix, string localName, string namespaceUri, int[] array, int offset, int count)
		{
			this.CheckArray(array, offset, count);
			for (int i = 0; i < count; i++)
			{
				this.WriteStartElement(prefix, localName, namespaceUri);
				this.WriteValue(array[offset + i]);
				this.WriteEndElement();
			}
		}

		/// <summary>Writes nodes from a <see cref="T:System.Int32" /> array.</summary>
		/// <param name="prefix">The namespace prefix.</param>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <param name="array">The array that contains the nodes.</param>
		/// <param name="offset">The starting index in the array.</param>
		/// <param name="count">The number of nodes to get from the array.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is &lt; 0 or &gt; <paramref name="array" /> length.
		/// -or-
		/// <paramref name="count" /> is &lt; 0 or &gt; <paramref name="array" /> length minus <paramref name="offset" />.</exception>
		// Token: 0x0600058C RID: 1420 RVA: 0x00019692 File Offset: 0x00017892
		public virtual void WriteArray(string prefix, XmlDictionaryString localName, XmlDictionaryString namespaceUri, int[] array, int offset, int count)
		{
			this.WriteArray(prefix, XmlDictionaryString.GetString(localName), XmlDictionaryString.GetString(namespaceUri), array, offset, count);
		}

		/// <summary>Writes nodes from a <see cref="T:System.Int64" /> array.</summary>
		/// <param name="prefix">The namespace prefix.</param>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <param name="array">The array that contains the nodes.</param>
		/// <param name="offset">The starting index in the array.</param>
		/// <param name="count">The number of nodes to get from the array.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is &lt; 0 or &gt; <paramref name="array" /> length.
		/// -or-
		/// <paramref name="count" /> is &lt; 0 or &gt; <paramref name="array" /> length minus <paramref name="offset" />.</exception>
		// Token: 0x0600058D RID: 1421 RVA: 0x000196B0 File Offset: 0x000178B0
		public virtual void WriteArray(string prefix, string localName, string namespaceUri, long[] array, int offset, int count)
		{
			this.CheckArray(array, offset, count);
			for (int i = 0; i < count; i++)
			{
				this.WriteStartElement(prefix, localName, namespaceUri);
				this.WriteValue(array[offset + i]);
				this.WriteEndElement();
			}
		}

		/// <summary>Writes nodes from a <see cref="T:System.Int64" /> array.</summary>
		/// <param name="prefix">The namespace prefix.</param>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <param name="array">The array that contains the nodes.</param>
		/// <param name="offset">The starting index in the array.</param>
		/// <param name="count">The number of nodes to get from the array.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is &lt; 0 or &gt; <paramref name="array" /> length.
		/// -or-
		/// <paramref name="count" /> is &lt; 0 or &gt; <paramref name="array" /> length minus <paramref name="offset" />.</exception>
		// Token: 0x0600058E RID: 1422 RVA: 0x000196F2 File Offset: 0x000178F2
		public virtual void WriteArray(string prefix, XmlDictionaryString localName, XmlDictionaryString namespaceUri, long[] array, int offset, int count)
		{
			this.WriteArray(prefix, XmlDictionaryString.GetString(localName), XmlDictionaryString.GetString(namespaceUri), array, offset, count);
		}

		/// <summary>Writes nodes from a <see cref="T:System.Single" /> array.</summary>
		/// <param name="prefix">The namespace prefix.</param>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <param name="array">The array that contains the nodes.</param>
		/// <param name="offset">The starting index in the array.</param>
		/// <param name="count">The number of nodes to get from the array.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is &lt; 0 or &gt; <paramref name="array" /> length.
		/// -or-
		/// <paramref name="count" /> is &lt; 0 or &gt; <paramref name="array" /> length minus <paramref name="offset" />.</exception>
		// Token: 0x0600058F RID: 1423 RVA: 0x00019710 File Offset: 0x00017910
		public virtual void WriteArray(string prefix, string localName, string namespaceUri, float[] array, int offset, int count)
		{
			this.CheckArray(array, offset, count);
			for (int i = 0; i < count; i++)
			{
				this.WriteStartElement(prefix, localName, namespaceUri);
				this.WriteValue(array[offset + i]);
				this.WriteEndElement();
			}
		}

		/// <summary>Writes nodes from a <see cref="T:System.Single" /> array.</summary>
		/// <param name="prefix">The namespace prefix.</param>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <param name="array">The array that contains the nodes.</param>
		/// <param name="offset">The starting index in the array.</param>
		/// <param name="count">The number of nodes to get from the array.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is &lt; 0 or &gt; <paramref name="array" /> length.
		/// -or-
		/// <paramref name="count" /> is &lt; 0 or &gt; <paramref name="array" /> length minus <paramref name="offset" />.</exception>
		// Token: 0x06000590 RID: 1424 RVA: 0x00019752 File Offset: 0x00017952
		public virtual void WriteArray(string prefix, XmlDictionaryString localName, XmlDictionaryString namespaceUri, float[] array, int offset, int count)
		{
			this.WriteArray(prefix, XmlDictionaryString.GetString(localName), XmlDictionaryString.GetString(namespaceUri), array, offset, count);
		}

		/// <summary>Writes nodes from a <see cref="T:System.Double" /> array.</summary>
		/// <param name="prefix">The namespace prefix.</param>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <param name="array">The array that contains the nodes.</param>
		/// <param name="offset">The starting index in the array.</param>
		/// <param name="count">The number of nodes to get from the array.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is &lt; 0 or &gt; <paramref name="array" /> length.
		/// -or-
		/// <paramref name="count" /> is &lt; 0 or &gt; <paramref name="array" /> length minus <paramref name="offset" />.</exception>
		// Token: 0x06000591 RID: 1425 RVA: 0x00019770 File Offset: 0x00017970
		public virtual void WriteArray(string prefix, string localName, string namespaceUri, double[] array, int offset, int count)
		{
			this.CheckArray(array, offset, count);
			for (int i = 0; i < count; i++)
			{
				this.WriteStartElement(prefix, localName, namespaceUri);
				this.WriteValue(array[offset + i]);
				this.WriteEndElement();
			}
		}

		/// <summary>Writes nodes from a <see cref="T:System.Double" /> array.</summary>
		/// <param name="prefix">The namespace prefix.</param>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <param name="array">The array that contains the nodes.</param>
		/// <param name="offset">The starting index in the array.</param>
		/// <param name="count">The number of nodes to get from the array.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is &lt; 0 or &gt; <paramref name="array" /> length.
		/// -or-
		/// <paramref name="count" /> is &lt; 0 or &gt; <paramref name="array" /> length minus <paramref name="offset" />.</exception>
		// Token: 0x06000592 RID: 1426 RVA: 0x000197B2 File Offset: 0x000179B2
		public virtual void WriteArray(string prefix, XmlDictionaryString localName, XmlDictionaryString namespaceUri, double[] array, int offset, int count)
		{
			this.WriteArray(prefix, XmlDictionaryString.GetString(localName), XmlDictionaryString.GetString(namespaceUri), array, offset, count);
		}

		/// <summary>Writes nodes from a <see cref="T:System.Decimal" /> array.</summary>
		/// <param name="prefix">The namespace prefix.</param>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <param name="array">The array that contains the nodes.</param>
		/// <param name="offset">The starting index in the array.</param>
		/// <param name="count">The number of nodes to get from the array.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is &lt; 0 or &gt; <paramref name="array" /> length.
		/// -or-
		/// <paramref name="count" /> is &lt; 0 or &gt; <paramref name="array" /> length minus <paramref name="offset" />.</exception>
		// Token: 0x06000593 RID: 1427 RVA: 0x000197D0 File Offset: 0x000179D0
		public virtual void WriteArray(string prefix, string localName, string namespaceUri, decimal[] array, int offset, int count)
		{
			this.CheckArray(array, offset, count);
			for (int i = 0; i < count; i++)
			{
				this.WriteStartElement(prefix, localName, namespaceUri);
				this.WriteValue(array[offset + i]);
				this.WriteEndElement();
			}
		}

		/// <summary>Writes nodes from a <see cref="T:System.Decimal" /> array.</summary>
		/// <param name="prefix">The namespace prefix.</param>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <param name="array">The array that contains the nodes.</param>
		/// <param name="offset">The starting index in the array.</param>
		/// <param name="count">The number of nodes to get from the array.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is &lt; 0 or &gt; <paramref name="array" /> length.
		/// -or-
		/// <paramref name="count" /> is &lt; 0 or &gt; <paramref name="array" /> length minus <paramref name="offset" />.</exception>
		// Token: 0x06000594 RID: 1428 RVA: 0x00019816 File Offset: 0x00017A16
		public virtual void WriteArray(string prefix, XmlDictionaryString localName, XmlDictionaryString namespaceUri, decimal[] array, int offset, int count)
		{
			this.WriteArray(prefix, XmlDictionaryString.GetString(localName), XmlDictionaryString.GetString(namespaceUri), array, offset, count);
		}

		/// <summary>Writes nodes from a <see cref="T:System.DateTime" /> array.</summary>
		/// <param name="prefix">The namespace prefix.</param>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <param name="array">The array that contains the nodes.</param>
		/// <param name="offset">The starting index in the array.</param>
		/// <param name="count">The number of nodes to get from the array.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is &lt; 0 or &gt; <paramref name="array" /> length.
		/// -or-
		/// <paramref name="count" /> is &lt; 0 or &gt; <paramref name="array" /> length minus <paramref name="offset" />.</exception>
		// Token: 0x06000595 RID: 1429 RVA: 0x00019834 File Offset: 0x00017A34
		public virtual void WriteArray(string prefix, string localName, string namespaceUri, DateTime[] array, int offset, int count)
		{
			this.CheckArray(array, offset, count);
			for (int i = 0; i < count; i++)
			{
				this.WriteStartElement(prefix, localName, namespaceUri);
				this.WriteValue(array[offset + i]);
				this.WriteEndElement();
			}
		}

		/// <summary>Writes nodes from a <see cref="T:System.DateTime" /> array.</summary>
		/// <param name="prefix">The namespace prefix.</param>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <param name="array">The array that contains the nodes.</param>
		/// <param name="offset">The starting index in the array.</param>
		/// <param name="count">The number of nodes to get from the array.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is &lt; 0 or &gt; <paramref name="array" /> length.
		/// -or-
		/// <paramref name="count" /> is &lt; 0 or &gt; <paramref name="array" /> length minus <paramref name="offset" />.</exception>
		// Token: 0x06000596 RID: 1430 RVA: 0x0001987A File Offset: 0x00017A7A
		public virtual void WriteArray(string prefix, XmlDictionaryString localName, XmlDictionaryString namespaceUri, DateTime[] array, int offset, int count)
		{
			this.WriteArray(prefix, XmlDictionaryString.GetString(localName), XmlDictionaryString.GetString(namespaceUri), array, offset, count);
		}

		/// <summary>Writes nodes from a <see cref="T:System.Guid" /> array.</summary>
		/// <param name="prefix">The namespace prefix.</param>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <param name="array">The array that contains the nodes.</param>
		/// <param name="offset">The starting index in the array.</param>
		/// <param name="count">The number of nodes to get from the array.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is &lt; 0 or &gt; <paramref name="array" /> length.
		/// -or-
		/// <paramref name="count" /> is &lt; 0 or &gt; <paramref name="array" /> length minus <paramref name="offset" />.</exception>
		// Token: 0x06000597 RID: 1431 RVA: 0x00019898 File Offset: 0x00017A98
		public virtual void WriteArray(string prefix, string localName, string namespaceUri, Guid[] array, int offset, int count)
		{
			this.CheckArray(array, offset, count);
			for (int i = 0; i < count; i++)
			{
				this.WriteStartElement(prefix, localName, namespaceUri);
				this.WriteValue(array[offset + i]);
				this.WriteEndElement();
			}
		}

		/// <summary>Writes nodes from a <see cref="T:System.Guid" /> array.</summary>
		/// <param name="prefix">The namespace prefix.</param>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <param name="array">The array that contains the nodes.</param>
		/// <param name="offset">The starting index in the array.</param>
		/// <param name="count">The number of nodes to get from the array.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is &lt; 0 or &gt; <paramref name="array" /> length.
		/// -or-
		/// <paramref name="count" /> is &lt; 0 or &gt; <paramref name="array" /> length minus <paramref name="offset" />.</exception>
		// Token: 0x06000598 RID: 1432 RVA: 0x000198DE File Offset: 0x00017ADE
		public virtual void WriteArray(string prefix, XmlDictionaryString localName, XmlDictionaryString namespaceUri, Guid[] array, int offset, int count)
		{
			this.WriteArray(prefix, XmlDictionaryString.GetString(localName), XmlDictionaryString.GetString(namespaceUri), array, offset, count);
		}

		/// <summary>Writes nodes from a <see cref="T:System.TimeSpan" /> array.</summary>
		/// <param name="prefix">The namespace prefix.</param>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <param name="array">The array that contains the nodes.</param>
		/// <param name="offset">The starting index in the array.</param>
		/// <param name="count">The number of nodes to get from the array.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is &lt; 0 or &gt; <paramref name="array" /> length.
		/// -or-
		/// <paramref name="count" /> is &lt; 0 or &gt; <paramref name="array" /> length minus <paramref name="offset" />.</exception>
		// Token: 0x06000599 RID: 1433 RVA: 0x000198FC File Offset: 0x00017AFC
		public virtual void WriteArray(string prefix, string localName, string namespaceUri, TimeSpan[] array, int offset, int count)
		{
			this.CheckArray(array, offset, count);
			for (int i = 0; i < count; i++)
			{
				this.WriteStartElement(prefix, localName, namespaceUri);
				this.WriteValue(array[offset + i]);
				this.WriteEndElement();
			}
		}

		/// <summary>Writes nodes from a <see cref="T:System.TimeSpan" /> array.</summary>
		/// <param name="prefix">The namespace prefix.</param>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <param name="array">The array that contains the nodes.</param>
		/// <param name="offset">The starting index in the array.</param>
		/// <param name="count">The number of nodes to get from the array.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is &lt; 0 or &gt; <paramref name="array" /> length.
		/// -or-
		/// <paramref name="count" /> is &lt; 0 or &gt; <paramref name="array" /> length minus <paramref name="offset" />.</exception>
		// Token: 0x0600059A RID: 1434 RVA: 0x00019942 File Offset: 0x00017B42
		public virtual void WriteArray(string prefix, XmlDictionaryString localName, XmlDictionaryString namespaceUri, TimeSpan[] array, int offset, int count)
		{
			this.WriteArray(prefix, XmlDictionaryString.GetString(localName), XmlDictionaryString.GetString(namespaceUri), array, offset, count);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlDictionaryWriter" /> class.</summary>
		// Token: 0x0600059B RID: 1435 RVA: 0x0001995D File Offset: 0x00017B5D
		protected XmlDictionaryWriter()
		{
		}

		// Token: 0x02000067 RID: 103
		private class WriteValueFastAsyncResult : AsyncResult
		{
			// Token: 0x0600059C RID: 1436 RVA: 0x00019968 File Offset: 0x00017B68
			public WriteValueFastAsyncResult(XmlDictionaryWriter writer, IStreamProvider value, AsyncCallback callback, object state) : base(callback, state)
			{
				this.streamProvider = value;
				this.writer = writer;
				this.stream = value.GetStream();
				if (this.stream == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(System.Runtime.Serialization.SR.GetString("Stream returned by IStreamProvider cannot be null.")));
				}
				this.blockSize = 256;
				this.bytesRead = 0;
				this.block = new byte[this.blockSize];
				this.nextOperation = XmlDictionaryWriter.WriteValueFastAsyncResult.Operation.Read;
				this.ContinueWork(true, null);
			}

			// Token: 0x0600059D RID: 1437 RVA: 0x000199E7 File Offset: 0x00017BE7
			private void CompleteAndReleaseStream(bool completedSynchronously, Exception completionException = null)
			{
				if (completionException == null)
				{
					this.streamProvider.ReleaseStream(this.stream);
					this.stream = null;
				}
				base.Complete(completedSynchronously, completionException);
			}

			// Token: 0x0600059E RID: 1438 RVA: 0x00019A0C File Offset: 0x00017C0C
			private void ContinueWork(bool completedSynchronously, Exception completionException = null)
			{
				try
				{
					for (;;)
					{
						if (this.nextOperation == XmlDictionaryWriter.WriteValueFastAsyncResult.Operation.Read)
						{
							if (this.ReadAsync() != AsyncCompletionResult.Completed)
							{
								break;
							}
						}
						else if (this.nextOperation == XmlDictionaryWriter.WriteValueFastAsyncResult.Operation.Write)
						{
							if (this.WriteAsync() != AsyncCompletionResult.Completed)
							{
								break;
							}
						}
						else if (this.nextOperation == XmlDictionaryWriter.WriteValueFastAsyncResult.Operation.Complete)
						{
							goto Block_6;
						}
					}
					return;
					Block_6:;
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					if (completedSynchronously)
					{
						throw;
					}
					if (completionException == null)
					{
						completionException = ex;
					}
				}
				if (!this.completed)
				{
					this.completed = true;
					this.CompleteAndReleaseStream(completedSynchronously, completionException);
				}
			}

			// Token: 0x0600059F RID: 1439 RVA: 0x00019A8C File Offset: 0x00017C8C
			private AsyncCompletionResult ReadAsync()
			{
				IAsyncResult asyncResult = this.stream.BeginRead(this.block, 0, this.blockSize, XmlDictionaryWriter.WriteValueFastAsyncResult.onReadComplete, this);
				if (asyncResult.CompletedSynchronously)
				{
					this.HandleReadComplete(asyncResult);
					return AsyncCompletionResult.Completed;
				}
				return AsyncCompletionResult.Queued;
			}

			// Token: 0x060005A0 RID: 1440 RVA: 0x00019ACA File Offset: 0x00017CCA
			private void HandleReadComplete(IAsyncResult result)
			{
				this.bytesRead = this.stream.EndRead(result);
				if (this.bytesRead > 0)
				{
					this.nextOperation = XmlDictionaryWriter.WriteValueFastAsyncResult.Operation.Write;
					return;
				}
				this.nextOperation = XmlDictionaryWriter.WriteValueFastAsyncResult.Operation.Complete;
			}

			// Token: 0x060005A1 RID: 1441 RVA: 0x00019AF8 File Offset: 0x00017CF8
			private static void OnReadComplete(IAsyncResult result)
			{
				if (result.CompletedSynchronously)
				{
					return;
				}
				Exception completionException = null;
				XmlDictionaryWriter.WriteValueFastAsyncResult writeValueFastAsyncResult = (XmlDictionaryWriter.WriteValueFastAsyncResult)result.AsyncState;
				bool flag = false;
				try
				{
					writeValueFastAsyncResult.HandleReadComplete(result);
					flag = true;
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					completionException = ex;
				}
				if (!flag)
				{
					writeValueFastAsyncResult.nextOperation = XmlDictionaryWriter.WriteValueFastAsyncResult.Operation.Complete;
				}
				writeValueFastAsyncResult.ContinueWork(false, completionException);
			}

			// Token: 0x060005A2 RID: 1442 RVA: 0x00019B58 File Offset: 0x00017D58
			private AsyncCompletionResult WriteAsync()
			{
				if (this.writerAsyncState == null)
				{
					this.writerAsyncArgs = new XmlWriteBase64AsyncArguments();
					this.writerAsyncState = new AsyncEventArgs<XmlWriteBase64AsyncArguments>();
				}
				if (XmlDictionaryWriter.WriteValueFastAsyncResult.onWriteComplete == null)
				{
					XmlDictionaryWriter.WriteValueFastAsyncResult.onWriteComplete = new AsyncEventArgsCallback(XmlDictionaryWriter.WriteValueFastAsyncResult.OnWriteComplete);
				}
				this.writerAsyncArgs.Buffer = this.block;
				this.writerAsyncArgs.Offset = 0;
				this.writerAsyncArgs.Count = this.bytesRead;
				this.writerAsyncState.Set(XmlDictionaryWriter.WriteValueFastAsyncResult.onWriteComplete, this.writerAsyncArgs, this);
				if (this.writer.WriteBase64Async(this.writerAsyncState) == AsyncCompletionResult.Completed)
				{
					this.HandleWriteComplete();
					this.writerAsyncState.Complete(true);
					return AsyncCompletionResult.Completed;
				}
				return AsyncCompletionResult.Queued;
			}

			// Token: 0x060005A3 RID: 1443 RVA: 0x00019C0C File Offset: 0x00017E0C
			private void HandleWriteComplete()
			{
				this.nextOperation = XmlDictionaryWriter.WriteValueFastAsyncResult.Operation.Read;
				if (this.blockSize < 65536 && this.bytesRead == this.blockSize)
				{
					this.blockSize *= 16;
					this.block = new byte[this.blockSize];
				}
			}

			// Token: 0x060005A4 RID: 1444 RVA: 0x00019C5C File Offset: 0x00017E5C
			private static void OnWriteComplete(IAsyncEventArgs asyncState)
			{
				XmlDictionaryWriter.WriteValueFastAsyncResult writeValueFastAsyncResult = (XmlDictionaryWriter.WriteValueFastAsyncResult)asyncState.AsyncState;
				Exception completionException = null;
				bool flag = false;
				try
				{
					if (asyncState.Exception != null)
					{
						completionException = asyncState.Exception;
					}
					else
					{
						writeValueFastAsyncResult.HandleWriteComplete();
						flag = true;
					}
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					completionException = ex;
				}
				if (!flag)
				{
					writeValueFastAsyncResult.nextOperation = XmlDictionaryWriter.WriteValueFastAsyncResult.Operation.Complete;
				}
				writeValueFastAsyncResult.ContinueWork(false, completionException);
			}

			// Token: 0x060005A5 RID: 1445 RVA: 0x00019CC4 File Offset: 0x00017EC4
			internal static void End(IAsyncResult result)
			{
				AsyncResult.End<XmlDictionaryWriter.WriteValueFastAsyncResult>(result);
			}

			// Token: 0x060005A6 RID: 1446 RVA: 0x00019CCD File Offset: 0x00017ECD
			// Note: this type is marked as 'beforefieldinit'.
			static WriteValueFastAsyncResult()
			{
			}

			// Token: 0x04000299 RID: 665
			private bool completed;

			// Token: 0x0400029A RID: 666
			private int blockSize;

			// Token: 0x0400029B RID: 667
			private byte[] block;

			// Token: 0x0400029C RID: 668
			private int bytesRead;

			// Token: 0x0400029D RID: 669
			private Stream stream;

			// Token: 0x0400029E RID: 670
			private XmlDictionaryWriter.WriteValueFastAsyncResult.Operation nextOperation;

			// Token: 0x0400029F RID: 671
			private IStreamProvider streamProvider;

			// Token: 0x040002A0 RID: 672
			private XmlDictionaryWriter writer;

			// Token: 0x040002A1 RID: 673
			private AsyncEventArgs<XmlWriteBase64AsyncArguments> writerAsyncState;

			// Token: 0x040002A2 RID: 674
			private XmlWriteBase64AsyncArguments writerAsyncArgs;

			// Token: 0x040002A3 RID: 675
			private static AsyncCallback onReadComplete = Fx.ThunkCallback(new AsyncCallback(XmlDictionaryWriter.WriteValueFastAsyncResult.OnReadComplete));

			// Token: 0x040002A4 RID: 676
			private static AsyncEventArgsCallback onWriteComplete;

			// Token: 0x02000068 RID: 104
			private enum Operation
			{
				// Token: 0x040002A6 RID: 678
				Read,
				// Token: 0x040002A7 RID: 679
				Write,
				// Token: 0x040002A8 RID: 680
				Complete
			}
		}

		// Token: 0x02000069 RID: 105
		private class WriteValueAsyncResult : AsyncResult
		{
			// Token: 0x060005A7 RID: 1447 RVA: 0x00019CE8 File Offset: 0x00017EE8
			public WriteValueAsyncResult(XmlDictionaryWriter writer, IStreamProvider value, AsyncCallback callback, object state) : base(callback, state)
			{
				this.streamProvider = value;
				this.writer = writer;
				this.writeBlockHandler = ((this.writer.Settings != null && this.writer.Settings.Async) ? XmlDictionaryWriter.WriteValueAsyncResult.handleWriteBlockAsync : XmlDictionaryWriter.WriteValueAsyncResult.handleWriteBlock);
				this.stream = value.GetStream();
				if (this.stream == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(System.Runtime.Serialization.SR.GetString("Stream returned by IStreamProvider cannot be null.")));
				}
				this.blockSize = 256;
				this.bytesRead = 0;
				this.block = new byte[this.blockSize];
				if (this.ContinueWork(null))
				{
					this.CompleteAndReleaseStream(true, null);
				}
			}

			// Token: 0x060005A8 RID: 1448 RVA: 0x00019D9A File Offset: 0x00017F9A
			private void AdjustBlockSize()
			{
				if (this.blockSize < 65536 && this.bytesRead == this.blockSize)
				{
					this.blockSize *= 16;
					this.block = new byte[this.blockSize];
				}
			}

			// Token: 0x060005A9 RID: 1449 RVA: 0x00019DD7 File Offset: 0x00017FD7
			private void CompleteAndReleaseStream(bool completedSynchronously, Exception completionException)
			{
				if (completionException == null)
				{
					this.streamProvider.ReleaseStream(this.stream);
					this.stream = null;
				}
				base.Complete(completedSynchronously, completionException);
			}

			// Token: 0x060005AA RID: 1450 RVA: 0x00019DFC File Offset: 0x00017FFC
			private bool ContinueWork(IAsyncResult result)
			{
				for (;;)
				{
					if (this.operation == XmlDictionaryWriter.WriteValueAsyncResult.Operation.Read)
					{
						if (!this.HandleReadBlock(result))
						{
							return false;
						}
						if (this.bytesRead <= 0)
						{
							break;
						}
						this.operation = XmlDictionaryWriter.WriteValueAsyncResult.Operation.Write;
					}
					else
					{
						if (!this.writeBlockHandler(result, this))
						{
							return false;
						}
						this.AdjustBlockSize();
						this.operation = XmlDictionaryWriter.WriteValueAsyncResult.Operation.Read;
					}
					result = null;
				}
				return true;
			}

			// Token: 0x060005AB RID: 1451 RVA: 0x00019E54 File Offset: 0x00018054
			private bool HandleReadBlock(IAsyncResult result)
			{
				if (result == null)
				{
					result = this.stream.BeginRead(this.block, 0, this.blockSize, XmlDictionaryWriter.WriteValueAsyncResult.onContinueWork, this);
					if (!result.CompletedSynchronously)
					{
						return false;
					}
				}
				this.bytesRead = this.stream.EndRead(result);
				return true;
			}

			// Token: 0x060005AC RID: 1452 RVA: 0x00019EA1 File Offset: 0x000180A1
			private static bool HandleWriteBlock(IAsyncResult result, XmlDictionaryWriter.WriteValueAsyncResult thisPtr)
			{
				if (result == null)
				{
					result = thisPtr.writer.BeginWriteBase64(thisPtr.block, 0, thisPtr.bytesRead, XmlDictionaryWriter.WriteValueAsyncResult.onContinueWork, thisPtr);
					if (!result.CompletedSynchronously)
					{
						return false;
					}
				}
				thisPtr.writer.EndWriteBase64(result);
				return true;
			}

			// Token: 0x060005AD RID: 1453 RVA: 0x00019EE0 File Offset: 0x000180E0
			private static bool HandleWriteBlockAsync(IAsyncResult result, XmlDictionaryWriter.WriteValueAsyncResult thisPtr)
			{
				Task task = (Task)result;
				if (task == null)
				{
					task = thisPtr.writer.WriteBase64Async(thisPtr.block, 0, thisPtr.bytesRead);
					task.AsAsyncResult(XmlDictionaryWriter.WriteValueAsyncResult.onContinueWork, thisPtr);
					return false;
				}
				task.GetAwaiter().GetResult();
				return true;
			}

			// Token: 0x060005AE RID: 1454 RVA: 0x00019F30 File Offset: 0x00018130
			private static void OnContinueWork(IAsyncResult result)
			{
				if (result.CompletedSynchronously && !(result is Task))
				{
					return;
				}
				Exception completionException = null;
				XmlDictionaryWriter.WriteValueAsyncResult writeValueAsyncResult = (XmlDictionaryWriter.WriteValueAsyncResult)result.AsyncState;
				bool flag = false;
				try
				{
					flag = writeValueAsyncResult.ContinueWork(result);
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					flag = true;
					completionException = ex;
				}
				if (flag)
				{
					writeValueAsyncResult.CompleteAndReleaseStream(false, completionException);
				}
			}

			// Token: 0x060005AF RID: 1455 RVA: 0x00019F94 File Offset: 0x00018194
			public static void End(IAsyncResult result)
			{
				AsyncResult.End<XmlDictionaryWriter.WriteValueAsyncResult>(result);
			}

			// Token: 0x060005B0 RID: 1456 RVA: 0x00019F9D File Offset: 0x0001819D
			// Note: this type is marked as 'beforefieldinit'.
			static WriteValueAsyncResult()
			{
			}

			// Token: 0x040002A9 RID: 681
			private int blockSize;

			// Token: 0x040002AA RID: 682
			private byte[] block;

			// Token: 0x040002AB RID: 683
			private int bytesRead;

			// Token: 0x040002AC RID: 684
			private Stream stream;

			// Token: 0x040002AD RID: 685
			private XmlDictionaryWriter.WriteValueAsyncResult.Operation operation;

			// Token: 0x040002AE RID: 686
			private IStreamProvider streamProvider;

			// Token: 0x040002AF RID: 687
			private XmlDictionaryWriter writer;

			// Token: 0x040002B0 RID: 688
			private Func<IAsyncResult, XmlDictionaryWriter.WriteValueAsyncResult, bool> writeBlockHandler;

			// Token: 0x040002B1 RID: 689
			private static Func<IAsyncResult, XmlDictionaryWriter.WriteValueAsyncResult, bool> handleWriteBlock = new Func<IAsyncResult, XmlDictionaryWriter.WriteValueAsyncResult, bool>(XmlDictionaryWriter.WriteValueAsyncResult.HandleWriteBlock);

			// Token: 0x040002B2 RID: 690
			private static Func<IAsyncResult, XmlDictionaryWriter.WriteValueAsyncResult, bool> handleWriteBlockAsync = new Func<IAsyncResult, XmlDictionaryWriter.WriteValueAsyncResult, bool>(XmlDictionaryWriter.WriteValueAsyncResult.HandleWriteBlockAsync);

			// Token: 0x040002B3 RID: 691
			private static AsyncCallback onContinueWork = Fx.ThunkCallback(new AsyncCallback(XmlDictionaryWriter.WriteValueAsyncResult.OnContinueWork));

			// Token: 0x0200006A RID: 106
			private enum Operation
			{
				// Token: 0x040002B5 RID: 693
				Read,
				// Token: 0x040002B6 RID: 694
				Write
			}
		}

		// Token: 0x0200006B RID: 107
		private class WriteBase64AsyncResult : ScheduleActionItemAsyncResult
		{
			// Token: 0x060005B1 RID: 1457 RVA: 0x00019FD7 File Offset: 0x000181D7
			public WriteBase64AsyncResult(byte[] buffer, int index, int count, XmlDictionaryWriter writer, AsyncCallback callback, object state) : base(callback, state)
			{
				this.buffer = buffer;
				this.index = index;
				this.count = count;
				this.writer = writer;
				base.Schedule();
			}

			// Token: 0x060005B2 RID: 1458 RVA: 0x0001A006 File Offset: 0x00018206
			protected override void OnDoWork()
			{
				this.writer.WriteBase64(this.buffer, this.index, this.count);
			}

			// Token: 0x040002B7 RID: 695
			private byte[] buffer;

			// Token: 0x040002B8 RID: 696
			private int index;

			// Token: 0x040002B9 RID: 697
			private int count;

			// Token: 0x040002BA RID: 698
			private XmlDictionaryWriter writer;
		}

		// Token: 0x0200006C RID: 108
		private class XmlWrappedWriter : XmlDictionaryWriter
		{
			// Token: 0x060005B3 RID: 1459 RVA: 0x0001A025 File Offset: 0x00018225
			public XmlWrappedWriter(XmlWriter writer)
			{
				this.writer = writer;
				this.depth = 0;
			}

			// Token: 0x060005B4 RID: 1460 RVA: 0x0001A03B File Offset: 0x0001823B
			public override void Close()
			{
				this.writer.Close();
			}

			// Token: 0x060005B5 RID: 1461 RVA: 0x0001A048 File Offset: 0x00018248
			public override void Flush()
			{
				this.writer.Flush();
			}

			// Token: 0x060005B6 RID: 1462 RVA: 0x0001A055 File Offset: 0x00018255
			public override string LookupPrefix(string namespaceUri)
			{
				return this.writer.LookupPrefix(namespaceUri);
			}

			// Token: 0x060005B7 RID: 1463 RVA: 0x0001A063 File Offset: 0x00018263
			public override void WriteAttributes(XmlReader reader, bool defattr)
			{
				this.writer.WriteAttributes(reader, defattr);
			}

			// Token: 0x060005B8 RID: 1464 RVA: 0x0001A072 File Offset: 0x00018272
			public override void WriteBase64(byte[] buffer, int index, int count)
			{
				this.writer.WriteBase64(buffer, index, count);
			}

			// Token: 0x060005B9 RID: 1465 RVA: 0x0001A082 File Offset: 0x00018282
			public override void WriteBinHex(byte[] buffer, int index, int count)
			{
				this.writer.WriteBinHex(buffer, index, count);
			}

			// Token: 0x060005BA RID: 1466 RVA: 0x0001A092 File Offset: 0x00018292
			public override void WriteCData(string text)
			{
				this.writer.WriteCData(text);
			}

			// Token: 0x060005BB RID: 1467 RVA: 0x0001A0A0 File Offset: 0x000182A0
			public override void WriteCharEntity(char ch)
			{
				this.writer.WriteCharEntity(ch);
			}

			// Token: 0x060005BC RID: 1468 RVA: 0x0001A0AE File Offset: 0x000182AE
			public override void WriteChars(char[] buffer, int index, int count)
			{
				this.writer.WriteChars(buffer, index, count);
			}

			// Token: 0x060005BD RID: 1469 RVA: 0x0001A0BE File Offset: 0x000182BE
			public override void WriteComment(string text)
			{
				this.writer.WriteComment(text);
			}

			// Token: 0x060005BE RID: 1470 RVA: 0x0001A0CC File Offset: 0x000182CC
			public override void WriteDocType(string name, string pubid, string sysid, string subset)
			{
				this.writer.WriteDocType(name, pubid, sysid, subset);
			}

			// Token: 0x060005BF RID: 1471 RVA: 0x0001A0DE File Offset: 0x000182DE
			public override void WriteEndAttribute()
			{
				this.writer.WriteEndAttribute();
			}

			// Token: 0x060005C0 RID: 1472 RVA: 0x0001A0EB File Offset: 0x000182EB
			public override void WriteEndDocument()
			{
				this.writer.WriteEndDocument();
			}

			// Token: 0x060005C1 RID: 1473 RVA: 0x0001A0F8 File Offset: 0x000182F8
			public override void WriteEndElement()
			{
				this.writer.WriteEndElement();
				this.depth--;
			}

			// Token: 0x060005C2 RID: 1474 RVA: 0x0001A113 File Offset: 0x00018313
			public override void WriteEntityRef(string name)
			{
				this.writer.WriteEntityRef(name);
			}

			// Token: 0x060005C3 RID: 1475 RVA: 0x0001A121 File Offset: 0x00018321
			public override void WriteFullEndElement()
			{
				this.writer.WriteFullEndElement();
			}

			// Token: 0x060005C4 RID: 1476 RVA: 0x0001A12E File Offset: 0x0001832E
			public override void WriteName(string name)
			{
				this.writer.WriteName(name);
			}

			// Token: 0x060005C5 RID: 1477 RVA: 0x0001A13C File Offset: 0x0001833C
			public override void WriteNmToken(string name)
			{
				this.writer.WriteNmToken(name);
			}

			// Token: 0x060005C6 RID: 1478 RVA: 0x0001A14A File Offset: 0x0001834A
			public override void WriteNode(XmlReader reader, bool defattr)
			{
				this.writer.WriteNode(reader, defattr);
			}

			// Token: 0x060005C7 RID: 1479 RVA: 0x0001A159 File Offset: 0x00018359
			public override void WriteProcessingInstruction(string name, string text)
			{
				this.writer.WriteProcessingInstruction(name, text);
			}

			// Token: 0x060005C8 RID: 1480 RVA: 0x0001A168 File Offset: 0x00018368
			public override void WriteQualifiedName(string localName, string namespaceUri)
			{
				this.writer.WriteQualifiedName(localName, namespaceUri);
			}

			// Token: 0x060005C9 RID: 1481 RVA: 0x0001A177 File Offset: 0x00018377
			public override void WriteRaw(char[] buffer, int index, int count)
			{
				this.writer.WriteRaw(buffer, index, count);
			}

			// Token: 0x060005CA RID: 1482 RVA: 0x0001A187 File Offset: 0x00018387
			public override void WriteRaw(string data)
			{
				this.writer.WriteRaw(data);
			}

			// Token: 0x060005CB RID: 1483 RVA: 0x0001A195 File Offset: 0x00018395
			public override void WriteStartAttribute(string prefix, string localName, string namespaceUri)
			{
				this.writer.WriteStartAttribute(prefix, localName, namespaceUri);
				this.prefix++;
			}

			// Token: 0x060005CC RID: 1484 RVA: 0x0001A1B3 File Offset: 0x000183B3
			public override void WriteStartDocument()
			{
				this.writer.WriteStartDocument();
			}

			// Token: 0x060005CD RID: 1485 RVA: 0x0001A1C0 File Offset: 0x000183C0
			public override void WriteStartDocument(bool standalone)
			{
				this.writer.WriteStartDocument(standalone);
			}

			// Token: 0x060005CE RID: 1486 RVA: 0x0001A1CE File Offset: 0x000183CE
			public override void WriteStartElement(string prefix, string localName, string namespaceUri)
			{
				this.writer.WriteStartElement(prefix, localName, namespaceUri);
				this.depth++;
				this.prefix = 1;
			}

			// Token: 0x1700009F RID: 159
			// (get) Token: 0x060005CF RID: 1487 RVA: 0x0001A1F3 File Offset: 0x000183F3
			public override WriteState WriteState
			{
				get
				{
					return this.writer.WriteState;
				}
			}

			// Token: 0x060005D0 RID: 1488 RVA: 0x0001A200 File Offset: 0x00018400
			public override void WriteString(string text)
			{
				this.writer.WriteString(text);
			}

			// Token: 0x060005D1 RID: 1489 RVA: 0x0001A20E File Offset: 0x0001840E
			public override void WriteSurrogateCharEntity(char lowChar, char highChar)
			{
				this.writer.WriteSurrogateCharEntity(lowChar, highChar);
			}

			// Token: 0x060005D2 RID: 1490 RVA: 0x0001A21D File Offset: 0x0001841D
			public override void WriteWhitespace(string whitespace)
			{
				this.writer.WriteWhitespace(whitespace);
			}

			// Token: 0x060005D3 RID: 1491 RVA: 0x0001A22B File Offset: 0x0001842B
			public override void WriteValue(object value)
			{
				this.writer.WriteValue(value);
			}

			// Token: 0x060005D4 RID: 1492 RVA: 0x0001A239 File Offset: 0x00018439
			public override void WriteValue(string value)
			{
				this.writer.WriteValue(value);
			}

			// Token: 0x060005D5 RID: 1493 RVA: 0x0001A247 File Offset: 0x00018447
			public override void WriteValue(bool value)
			{
				this.writer.WriteValue(value);
			}

			// Token: 0x060005D6 RID: 1494 RVA: 0x0001A255 File Offset: 0x00018455
			public override void WriteValue(DateTime value)
			{
				this.writer.WriteValue(value);
			}

			// Token: 0x060005D7 RID: 1495 RVA: 0x0001A263 File Offset: 0x00018463
			public override void WriteValue(double value)
			{
				this.writer.WriteValue(value);
			}

			// Token: 0x060005D8 RID: 1496 RVA: 0x0001A271 File Offset: 0x00018471
			public override void WriteValue(int value)
			{
				this.writer.WriteValue(value);
			}

			// Token: 0x060005D9 RID: 1497 RVA: 0x0001A27F File Offset: 0x0001847F
			public override void WriteValue(long value)
			{
				this.writer.WriteValue(value);
			}

			// Token: 0x060005DA RID: 1498 RVA: 0x0001A290 File Offset: 0x00018490
			public override void WriteXmlnsAttribute(string prefix, string namespaceUri)
			{
				if (namespaceUri == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("namespaceUri");
				}
				if (prefix == null)
				{
					if (this.LookupPrefix(namespaceUri) != null)
					{
						return;
					}
					if (namespaceUri.Length == 0)
					{
						prefix = string.Empty;
					}
					else
					{
						string str = this.depth.ToString(NumberFormatInfo.InvariantInfo);
						string str2 = this.prefix.ToString(NumberFormatInfo.InvariantInfo);
						prefix = "d" + str + "p" + str2;
					}
				}
				base.WriteAttributeString("xmlns", prefix, null, namespaceUri);
			}

			// Token: 0x170000A0 RID: 160
			// (get) Token: 0x060005DB RID: 1499 RVA: 0x0001A30C File Offset: 0x0001850C
			public override string XmlLang
			{
				get
				{
					return this.writer.XmlLang;
				}
			}

			// Token: 0x170000A1 RID: 161
			// (get) Token: 0x060005DC RID: 1500 RVA: 0x0001A319 File Offset: 0x00018519
			public override XmlSpace XmlSpace
			{
				get
				{
					return this.writer.XmlSpace;
				}
			}

			// Token: 0x040002BB RID: 699
			private XmlWriter writer;

			// Token: 0x040002BC RID: 700
			private int depth;

			// Token: 0x040002BD RID: 701
			private int prefix;
		}
	}
}
