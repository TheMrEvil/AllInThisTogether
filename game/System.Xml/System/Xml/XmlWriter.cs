using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using System.Xml.XPath;

namespace System.Xml
{
	/// <summary>Represents a writer that provides a fast, non-cached, forward-only way to generate streams or files that contain XML data.</summary>
	// Token: 0x02000179 RID: 377
	public abstract class XmlWriter : IDisposable
	{
		/// <summary>Gets the <see cref="T:System.Xml.XmlWriterSettings" /> object used to create this <see cref="T:System.Xml.XmlWriter" /> instance.</summary>
		/// <returns>The <see cref="T:System.Xml.XmlWriterSettings" /> object used to create this writer instance. If this writer was not created using the <see cref="Overload:System.Xml.XmlWriter.Create" /> method, this property returns <see langword="null" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000D47 RID: 3399 RVA: 0x0001DA42 File Offset: 0x0001BC42
		public virtual XmlWriterSettings Settings
		{
			get
			{
				return null;
			}
		}

		/// <summary>When overridden in a derived class, writes the XML declaration with the version "1.0".</summary>
		/// <exception cref="T:System.InvalidOperationException">This is not the first write method called after the constructor.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000D48 RID: 3400
		public abstract void WriteStartDocument();

		/// <summary>When overridden in a derived class, writes the XML declaration with the version "1.0" and the standalone attribute.</summary>
		/// <param name="standalone">If <see langword="true" />, it writes "standalone=yes"; if <see langword="false" />, it writes "standalone=no".</param>
		/// <exception cref="T:System.InvalidOperationException">This is not the first write method called after the constructor. </exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000D49 RID: 3401
		public abstract void WriteStartDocument(bool standalone);

		/// <summary>When overridden in a derived class, closes any open elements or attributes and puts the writer back in the Start state.</summary>
		/// <exception cref="T:System.ArgumentException">The XML document is invalid.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000D4A RID: 3402
		public abstract void WriteEndDocument();

		/// <summary>When overridden in a derived class, writes the DOCTYPE declaration with the specified name and optional attributes.</summary>
		/// <param name="name">The name of the DOCTYPE. This must be non-empty.</param>
		/// <param name="pubid">If non-null it also writes PUBLIC "pubid" "sysid" where <paramref name="pubid" /> and <paramref name="sysid" /> are replaced with the value of the given arguments.</param>
		/// <param name="sysid">If <paramref name="pubid" /> is <see langword="null" /> and <paramref name="sysid" /> is non-null it writes SYSTEM "sysid" where <paramref name="sysid" /> is replaced with the value of this argument.</param>
		/// <param name="subset">If non-null it writes [subset] where subset is replaced with the value of this argument.</param>
		/// <exception cref="T:System.InvalidOperationException">This method was called outside the prolog (after the root element). </exception>
		/// <exception cref="T:System.ArgumentException">The value for <paramref name="name" /> would result in invalid XML.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000D4B RID: 3403
		public abstract void WriteDocType(string name, string pubid, string sysid, string subset);

		/// <summary>When overridden in a derived class, writes the specified start tag and associates it with the given namespace.</summary>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="ns">The namespace URI to associate with the element. If this namespace is already in scope and has an associated prefix, the writer automatically writes that prefix also.</param>
		/// <exception cref="T:System.InvalidOperationException">The writer is closed.</exception>
		/// <exception cref="T:System.Text.EncoderFallbackException">There is a character in the buffer that is a valid XML character but is not valid for the output encoding. For example, if the output encoding is ASCII, you should only use characters from the range of 0 to 127 for element and attribute names. The invalid character might be in the argument of this method or in an argument of previous methods that were writing to the buffer. Such characters are escaped by character entity references when possible (for example, in text nodes or attribute values). However, the character entity reference is not allowed in element and attribute names, comments, processing instructions, or CDATA sections.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000D4C RID: 3404 RVA: 0x00057E6E File Offset: 0x0005606E
		public void WriteStartElement(string localName, string ns)
		{
			this.WriteStartElement(null, localName, ns);
		}

		/// <summary>When overridden in a derived class, writes the specified start tag and associates it with the given namespace and prefix.</summary>
		/// <param name="prefix">The namespace prefix of the element.</param>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="ns">The namespace URI to associate with the element.</param>
		/// <exception cref="T:System.InvalidOperationException">The writer is closed.</exception>
		/// <exception cref="T:System.Text.EncoderFallbackException">There is a character in the buffer that is a valid XML character but is not valid for the output encoding. For example, if the output encoding is ASCII, you should only use characters from the range of 0 to 127 for element and attribute names. The invalid character might be in the argument of this method or in an argument of previous methods that were writing to the buffer. Such characters are escaped by character entity references when possible (for example, in text nodes or attribute values). However, the character entity reference is not allowed in element and attribute names, comments, processing instructions, or CDATA sections.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000D4D RID: 3405
		public abstract void WriteStartElement(string prefix, string localName, string ns);

		/// <summary>When overridden in a derived class, writes out a start tag with the specified local name.</summary>
		/// <param name="localName">The local name of the element.</param>
		/// <exception cref="T:System.InvalidOperationException">The writer is closed.</exception>
		/// <exception cref="T:System.Text.EncoderFallbackException">There is a character in the buffer that is a valid XML character but is not valid for the output encoding. For example, if the output encoding is ASCII, you should only use characters from the range of 0 to 127 for element and attribute names. The invalid character might be in the argument of this method or in an argument of previous methods that were writing to the buffer. Such characters are escaped by character entity references when possible (for example, in text nodes or attribute values). However, the character entity reference is not allowed in element and attribute names, comments, processing instructions, or CDATA sections. </exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000D4E RID: 3406 RVA: 0x00057E79 File Offset: 0x00056079
		public void WriteStartElement(string localName)
		{
			this.WriteStartElement(null, localName, null);
		}

		/// <summary>When overridden in a derived class, closes one element and pops the corresponding namespace scope.</summary>
		/// <exception cref="T:System.InvalidOperationException">This results in an invalid XML document.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000D4F RID: 3407
		public abstract void WriteEndElement();

		/// <summary>When overridden in a derived class, closes one element and pops the corresponding namespace scope.</summary>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000D50 RID: 3408
		public abstract void WriteFullEndElement();

		/// <summary>When overridden in a derived class, writes an attribute with the specified local name, namespace URI, and value.</summary>
		/// <param name="localName">The local name of the attribute.</param>
		/// <param name="ns">The namespace URI to associate with the attribute.</param>
		/// <param name="value">The value of the attribute.</param>
		/// <exception cref="T:System.InvalidOperationException">The state of writer is not <see langword="WriteState.Element" /> or writer is closed. </exception>
		/// <exception cref="T:System.ArgumentException">The <see langword="xml:space" /> or <see langword="xml:lang" /> attribute value is invalid. </exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000D51 RID: 3409 RVA: 0x00057E84 File Offset: 0x00056084
		public void WriteAttributeString(string localName, string ns, string value)
		{
			this.WriteStartAttribute(null, localName, ns);
			this.WriteString(value);
			this.WriteEndAttribute();
		}

		/// <summary>When overridden in a derived class, writes out the attribute with the specified local name and value.</summary>
		/// <param name="localName">The local name of the attribute.</param>
		/// <param name="value">The value of the attribute.</param>
		/// <exception cref="T:System.InvalidOperationException">The state of writer is not <see langword="WriteState.Element" /> or writer is closed. </exception>
		/// <exception cref="T:System.ArgumentException">The <see langword="xml:space" /> or <see langword="xml:lang" /> attribute value is invalid. </exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000D52 RID: 3410 RVA: 0x00057E9C File Offset: 0x0005609C
		public void WriteAttributeString(string localName, string value)
		{
			this.WriteStartAttribute(null, localName, null);
			this.WriteString(value);
			this.WriteEndAttribute();
		}

		/// <summary>When overridden in a derived class, writes out the attribute with the specified prefix, local name, namespace URI, and value.</summary>
		/// <param name="prefix">The namespace prefix of the attribute.</param>
		/// <param name="localName">The local name of the attribute.</param>
		/// <param name="ns">The namespace URI of the attribute.</param>
		/// <param name="value">The value of the attribute.</param>
		/// <exception cref="T:System.InvalidOperationException">The state of writer is not <see langword="WriteState.Element" /> or writer is closed. </exception>
		/// <exception cref="T:System.ArgumentException">The <see langword="xml:space" /> or <see langword="xml:lang" /> attribute value is invalid. </exception>
		/// <exception cref="T:System.Xml.XmlException">The <paramref name="localName" /> or <paramref name="ns" /> is <see langword="null" />. </exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000D53 RID: 3411 RVA: 0x00057EB4 File Offset: 0x000560B4
		public void WriteAttributeString(string prefix, string localName, string ns, string value)
		{
			this.WriteStartAttribute(prefix, localName, ns);
			this.WriteString(value);
			this.WriteEndAttribute();
		}

		/// <summary>Writes the start of an attribute with the specified local name and namespace URI.</summary>
		/// <param name="localName">The local name of the attribute.</param>
		/// <param name="ns">The namespace URI of the attribute.</param>
		/// <exception cref="T:System.Text.EncoderFallbackException">There is a character in the buffer that is a valid XML character but is not valid for the output encoding. For example, if the output encoding is ASCII, you should only use characters from the range of 0 to 127 for element and attribute names. The invalid character might be in the argument of this method or in an argument of previous methods that were writing to the buffer. Such characters are escaped by character entity references when possible (for example, in text nodes or attribute values). However, the character entity reference is not allowed in element and attribute names, comments, processing instructions, or CDATA sections.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000D54 RID: 3412 RVA: 0x00057ECD File Offset: 0x000560CD
		public void WriteStartAttribute(string localName, string ns)
		{
			this.WriteStartAttribute(null, localName, ns);
		}

		/// <summary>When overridden in a derived class, writes the start of an attribute with the specified prefix, local name, and namespace URI.</summary>
		/// <param name="prefix">The namespace prefix of the attribute.</param>
		/// <param name="localName">The local name of the attribute.</param>
		/// <param name="ns">The namespace URI for the attribute.</param>
		/// <exception cref="T:System.Text.EncoderFallbackException">There is a character in the buffer that is a valid XML character but is not valid for the output encoding. For example, if the output encoding is ASCII, you should only use characters from the range of 0 to 127 for element and attribute names. The invalid character might be in the argument of this method or in an argument of previous methods that were writing to the buffer. Such characters are escaped by character entity references when possible (for example, in text nodes or attribute values). However, the character entity reference is not allowed in element and attribute names, comments, processing instructions, or CDATA sections. </exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000D55 RID: 3413
		public abstract void WriteStartAttribute(string prefix, string localName, string ns);

		/// <summary>Writes the start of an attribute with the specified local name.</summary>
		/// <param name="localName">The local name of the attribute.</param>
		/// <exception cref="T:System.InvalidOperationException">The writer is closed.</exception>
		/// <exception cref="T:System.Text.EncoderFallbackException">There is a character in the buffer that is a valid XML character but is not valid for the output encoding. For example, if the output encoding is ASCII, you should only use characters from the range of 0 to 127 for element and attribute names. The invalid character might be in the argument of this method or in an argument of previous methods that were writing to the buffer. Such characters are escaped by character entity references when possible (for example, in text nodes or attribute values). However, the character entity reference is not allowed in element and attribute names, comments, processing instructions, or CDATA sections.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000D56 RID: 3414 RVA: 0x00057ED8 File Offset: 0x000560D8
		public void WriteStartAttribute(string localName)
		{
			this.WriteStartAttribute(null, localName, null);
		}

		/// <summary>When overridden in a derived class, closes the previous <see cref="M:System.Xml.XmlWriter.WriteStartAttribute(System.String,System.String)" /> call.</summary>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000D57 RID: 3415
		public abstract void WriteEndAttribute();

		/// <summary>When overridden in a derived class, writes out a &lt;![CDATA[...]]&gt; block containing the specified text.</summary>
		/// <param name="text">The text to place inside the CDATA block.</param>
		/// <exception cref="T:System.ArgumentException">The text would result in a non-well formed XML document.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000D58 RID: 3416
		public abstract void WriteCData(string text);

		/// <summary>When overridden in a derived class, writes out a comment &lt;!--...--&gt; containing the specified text.</summary>
		/// <param name="text">Text to place inside the comment.</param>
		/// <exception cref="T:System.ArgumentException">The text would result in a non-well-formed XML document.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000D59 RID: 3417
		public abstract void WriteComment(string text);

		/// <summary>When overridden in a derived class, writes out a processing instruction with a space between the name and text as follows: &lt;?name text?&gt;.</summary>
		/// <param name="name">The name of the processing instruction.</param>
		/// <param name="text">The text to include in the processing instruction.</param>
		/// <exception cref="T:System.ArgumentException">The text would result in a non-well formed XML document.
		///         <paramref name="name" /> is either <see langword="null" /> or <see langword="String.Empty" />.This method is being used to create an XML declaration after <see cref="M:System.Xml.XmlWriter.WriteStartDocument" /> has already been called. </exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000D5A RID: 3418
		public abstract void WriteProcessingInstruction(string name, string text);

		/// <summary>When overridden in a derived class, writes out an entity reference as <see langword="&amp;name;" />.</summary>
		/// <param name="name">The name of the entity reference.</param>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="name" /> is either <see langword="null" /> or <see langword="String.Empty" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000D5B RID: 3419
		public abstract void WriteEntityRef(string name);

		/// <summary>When overridden in a derived class, forces the generation of a character entity for the specified Unicode character value.</summary>
		/// <param name="ch">The Unicode character for which to generate a character entity.</param>
		/// <exception cref="T:System.ArgumentException">The character is in the surrogate pair character range, <see langword="0xd800" /> - <see langword="0xdfff" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000D5C RID: 3420
		public abstract void WriteCharEntity(char ch);

		/// <summary>When overridden in a derived class, writes out the given white space.</summary>
		/// <param name="ws">The string of white space characters.</param>
		/// <exception cref="T:System.ArgumentException">The string contains non-white space characters.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000D5D RID: 3421
		public abstract void WriteWhitespace(string ws);

		/// <summary>When overridden in a derived class, writes the given text content.</summary>
		/// <param name="text">The text to write.</param>
		/// <exception cref="T:System.ArgumentException">The text string contains an invalid surrogate pair.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000D5E RID: 3422
		public abstract void WriteString(string text);

		/// <summary>When overridden in a derived class, generates and writes the surrogate character entity for the surrogate character pair.</summary>
		/// <param name="lowChar">The low surrogate. This must be a value between 0xDC00 and 0xDFFF.</param>
		/// <param name="highChar">The high surrogate. This must be a value between 0xD800 and 0xDBFF.</param>
		/// <exception cref="T:System.ArgumentException">An invalid surrogate character pair was passed.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000D5F RID: 3423
		public abstract void WriteSurrogateCharEntity(char lowChar, char highChar);

		/// <summary>When overridden in a derived class, writes text one buffer at a time.</summary>
		/// <param name="buffer">Character array containing the text to write.</param>
		/// <param name="index">The position in the buffer indicating the start of the text to write.</param>
		/// <param name="count">The number of characters to write.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="buffer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///         <paramref name="index" /> or <paramref name="count" /> is less than zero.-or-The buffer length minus <paramref name="index" /> is less than <paramref name="count" />; the call results in surrogate pair characters being split or an invalid surrogate pair being written.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="buffer" /> parameter value is not valid.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000D60 RID: 3424
		public abstract void WriteChars(char[] buffer, int index, int count);

		/// <summary>When overridden in a derived class, writes raw markup manually from a character buffer.</summary>
		/// <param name="buffer">Character array containing the text to write.</param>
		/// <param name="index">The position within the buffer indicating the start of the text to write.</param>
		/// <param name="count">The number of characters to write.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="buffer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///         <paramref name="index" /> or <paramref name="count" /> is less than zero. -or-The buffer length minus <paramref name="index" /> is less than <paramref name="count" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000D61 RID: 3425
		public abstract void WriteRaw(char[] buffer, int index, int count);

		/// <summary>When overridden in a derived class, writes raw markup manually from a string.</summary>
		/// <param name="data">String containing the text to write.</param>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="data" /> is either <see langword="null" /> or <see langword="String.Empty" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000D62 RID: 3426
		public abstract void WriteRaw(string data);

		/// <summary>When overridden in a derived class, encodes the specified binary bytes as Base64 and writes out the resulting text.</summary>
		/// <param name="buffer">Byte array to encode.</param>
		/// <param name="index">The position in the buffer indicating the start of the bytes to write.</param>
		/// <param name="count">The number of bytes to write.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="buffer" /> is <see langword="null" />. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///         <paramref name="index" /> or <paramref name="count" /> is less than zero. -or-The buffer length minus <paramref name="index" /> is less than <paramref name="count" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000D63 RID: 3427
		public abstract void WriteBase64(byte[] buffer, int index, int count);

		/// <summary>When overridden in a derived class, encodes the specified binary bytes as <see langword="BinHex" /> and writes out the resulting text.</summary>
		/// <param name="buffer">Byte array to encode.</param>
		/// <param name="index">The position in the buffer indicating the start of the bytes to write.</param>
		/// <param name="count">The number of bytes to write.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="buffer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The writer is closed or in error state.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///         <paramref name="index" /> or <paramref name="count" /> is less than zero. -or-The buffer length minus <paramref name="index" /> is less than <paramref name="count" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000D64 RID: 3428 RVA: 0x00057EE3 File Offset: 0x000560E3
		public virtual void WriteBinHex(byte[] buffer, int index, int count)
		{
			BinHexEncoder.Encode(buffer, index, count, this);
		}

		/// <summary>When overridden in a derived class, gets the state of the writer.</summary>
		/// <returns>One of the <see cref="T:System.Xml.WriteState" /> values.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000D65 RID: 3429
		public abstract WriteState WriteState { get; }

		/// <summary>When overridden in a derived class, closes this stream and the underlying stream.</summary>
		/// <exception cref="T:System.InvalidOperationException">A call is made to write more output after <see langword="Close" /> has been called or the result of this call is an invalid XML document.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000D66 RID: 3430 RVA: 0x0000B528 File Offset: 0x00009728
		public virtual void Close()
		{
		}

		/// <summary>When overridden in a derived class, flushes whatever is in the buffer to the underlying streams and also flushes the underlying stream.</summary>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000D67 RID: 3431
		public abstract void Flush();

		/// <summary>When overridden in a derived class, returns the closest prefix defined in the current namespace scope for the namespace URI.</summary>
		/// <param name="ns">The namespace URI whose prefix you want to find.</param>
		/// <returns>The matching prefix or <see langword="null" /> if no matching namespace URI is found in the current scope.</returns>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="ns" /> is either <see langword="null" /> or <see langword="String.Empty" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000D68 RID: 3432
		public abstract string LookupPrefix(string ns);

		/// <summary>When overridden in a derived class, gets an <see cref="T:System.Xml.XmlSpace" /> representing the current <see langword="xml:space" /> scope.</summary>
		/// <returns>An <see langword="XmlSpace" /> representing the current <see langword="xml:space" /> scope.Value Meaning 
		///             <see langword="None" />
		///           This is the default if no <see langword="xml:space" /> scope exists.
		///             <see langword="Default" />
		///           The current scope is <see langword="xml:space" />="default".
		///             <see langword="Preserve" />
		///           The current scope is <see langword="xml:space" />="preserve".</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06000D69 RID: 3433 RVA: 0x0001222F File Offset: 0x0001042F
		public virtual XmlSpace XmlSpace
		{
			get
			{
				return XmlSpace.Default;
			}
		}

		/// <summary>When overridden in a derived class, gets the current <see langword="xml:lang" /> scope.</summary>
		/// <returns>The current <see langword="xml:lang" /> scope.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06000D6A RID: 3434 RVA: 0x0001E51E File Offset: 0x0001C71E
		public virtual string XmlLang
		{
			get
			{
				return string.Empty;
			}
		}

		/// <summary>When overridden in a derived class, writes out the specified name, ensuring it is a valid NmToken according to the W3C XML 1.0 recommendation (http://www.w3.org/TR/1998/REC-xml-19980210#NT-Name).</summary>
		/// <param name="name">The name to write.</param>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="name" /> is not a valid NmToken; or <paramref name="name" /> is either <see langword="null" /> or <see langword="String.Empty" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000D6B RID: 3435 RVA: 0x00057EEE File Offset: 0x000560EE
		public virtual void WriteNmToken(string name)
		{
			if (name == null || name.Length == 0)
			{
				throw new ArgumentException(Res.GetString("The empty string '' is not a valid name."));
			}
			this.WriteString(XmlConvert.VerifyNMTOKEN(name, ExceptionType.ArgumentException));
		}

		/// <summary>When overridden in a derived class, writes out the specified name, ensuring it is a valid name according to the W3C XML 1.0 recommendation (http://www.w3.org/TR/1998/REC-xml-19980210#NT-Name).</summary>
		/// <param name="name">The name to write.</param>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="name" /> is not a valid XML name; or <paramref name="name" /> is either <see langword="null" /> or <see langword="String.Empty" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000D6C RID: 3436 RVA: 0x00057F18 File Offset: 0x00056118
		public virtual void WriteName(string name)
		{
			this.WriteString(XmlConvert.VerifyQName(name, ExceptionType.ArgumentException));
		}

		/// <summary>When overridden in a derived class, writes out the namespace-qualified name. This method looks up the prefix that is in scope for the given namespace.</summary>
		/// <param name="localName">The local name to write.</param>
		/// <param name="ns">The namespace URI for the name.</param>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="localName" /> is either <see langword="null" /> or <see langword="String.Empty" />.
		///         <paramref name="localName" /> is not a valid name. </exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000D6D RID: 3437 RVA: 0x00057F28 File Offset: 0x00056128
		public virtual void WriteQualifiedName(string localName, string ns)
		{
			if (ns != null && ns.Length > 0)
			{
				string text = this.LookupPrefix(ns);
				if (text == null)
				{
					throw new ArgumentException(Res.GetString("The '{0}' namespace is not defined.", new object[]
					{
						ns
					}));
				}
				this.WriteString(text);
				this.WriteString(":");
			}
			this.WriteString(localName);
		}

		/// <summary>Writes the object value.</summary>
		/// <param name="value">The object value to write.
		///       Note   With the release of the .NET Framework 3.5, this method accepts <see cref="T:System.DateTimeOffset" /> as a parameter.</param>
		/// <exception cref="T:System.ArgumentException">An invalid value was specified.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The writer is closed or in error state.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000D6E RID: 3438 RVA: 0x00057F7F File Offset: 0x0005617F
		public virtual void WriteValue(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this.WriteString(XmlUntypedConverter.Untyped.ToString(value, null));
		}

		/// <summary>Writes a <see cref="T:System.String" /> value.</summary>
		/// <param name="value">The <see cref="T:System.String" /> value to write.</param>
		/// <exception cref="T:System.ArgumentException">An invalid value was specified.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000D6F RID: 3439 RVA: 0x00057FA1 File Offset: 0x000561A1
		public virtual void WriteValue(string value)
		{
			if (value == null)
			{
				return;
			}
			this.WriteString(value);
		}

		/// <summary>Writes a <see cref="T:System.Boolean" /> value.</summary>
		/// <param name="value">The <see cref="T:System.Boolean" /> value to write.</param>
		/// <exception cref="T:System.ArgumentException">An invalid value was specified.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000D70 RID: 3440 RVA: 0x00057FAE File Offset: 0x000561AE
		public virtual void WriteValue(bool value)
		{
			this.WriteString(XmlConvert.ToString(value));
		}

		/// <summary>Writes a <see cref="T:System.DateTime" /> value.</summary>
		/// <param name="value">The <see cref="T:System.DateTime" /> value to write.</param>
		/// <exception cref="T:System.ArgumentException">An invalid value was specified.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000D71 RID: 3441 RVA: 0x00057FBC File Offset: 0x000561BC
		public virtual void WriteValue(DateTime value)
		{
			this.WriteString(XmlConvert.ToString(value, XmlDateTimeSerializationMode.RoundtripKind));
		}

		/// <summary>Writes a <see cref="T:System.DateTimeOffset" /> value.</summary>
		/// <param name="value">The <see cref="T:System.DateTimeOffset" /> value to write.</param>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000D72 RID: 3442 RVA: 0x00057FCB File Offset: 0x000561CB
		public virtual void WriteValue(DateTimeOffset value)
		{
			if (value.Offset != TimeSpan.Zero)
			{
				this.WriteValue(value.LocalDateTime);
				return;
			}
			this.WriteValue(value.UtcDateTime);
		}

		/// <summary>Writes a <see cref="T:System.Double" /> value.</summary>
		/// <param name="value">The <see cref="T:System.Double" /> value to write.</param>
		/// <exception cref="T:System.ArgumentException">An invalid value was specified.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000D73 RID: 3443 RVA: 0x00057FFB File Offset: 0x000561FB
		public virtual void WriteValue(double value)
		{
			this.WriteString(XmlConvert.ToString(value));
		}

		/// <summary>Writes a single-precision floating-point number.</summary>
		/// <param name="value">The single-precision floating-point number to write.</param>
		/// <exception cref="T:System.ArgumentException">An invalid value was specified.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000D74 RID: 3444 RVA: 0x00058009 File Offset: 0x00056209
		public virtual void WriteValue(float value)
		{
			this.WriteString(XmlConvert.ToString(value));
		}

		/// <summary>Writes a <see cref="T:System.Decimal" /> value.</summary>
		/// <param name="value">The <see cref="T:System.Decimal" /> value to write.</param>
		/// <exception cref="T:System.ArgumentException">An invalid value was specified.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000D75 RID: 3445 RVA: 0x00058017 File Offset: 0x00056217
		public virtual void WriteValue(decimal value)
		{
			this.WriteString(XmlConvert.ToString(value));
		}

		/// <summary>Writes a <see cref="T:System.Int32" /> value.</summary>
		/// <param name="value">The <see cref="T:System.Int32" /> value to write.</param>
		/// <exception cref="T:System.ArgumentException">An invalid value was specified.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000D76 RID: 3446 RVA: 0x00058025 File Offset: 0x00056225
		public virtual void WriteValue(int value)
		{
			this.WriteString(XmlConvert.ToString(value));
		}

		/// <summary>Writes a <see cref="T:System.Int64" /> value.</summary>
		/// <param name="value">The <see cref="T:System.Int64" /> value to write.</param>
		/// <exception cref="T:System.ArgumentException">An invalid value was specified.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000D77 RID: 3447 RVA: 0x00058033 File Offset: 0x00056233
		public virtual void WriteValue(long value)
		{
			this.WriteString(XmlConvert.ToString(value));
		}

		/// <summary>When overridden in a derived class, writes out all the attributes found at the current position in the <see cref="T:System.Xml.XmlReader" />.</summary>
		/// <param name="reader">The <see langword="XmlReader" /> from which to copy the attributes.</param>
		/// <param name="defattr">
		///       <see langword="true" /> to copy the default attributes from the <see langword="XmlReader" />; otherwise, <see langword="false" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="reader" /> is <see langword="null" />. </exception>
		/// <exception cref="T:System.Xml.XmlException">The reader is not positioned on an <see langword="element" />, <see langword="attribute" /> or <see langword="XmlDeclaration" /> node. </exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000D78 RID: 3448 RVA: 0x00058044 File Offset: 0x00056244
		public virtual void WriteAttributes(XmlReader reader, bool defattr)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			if (reader.NodeType == XmlNodeType.Element || reader.NodeType == XmlNodeType.XmlDeclaration)
			{
				if (reader.MoveToFirstAttribute())
				{
					this.WriteAttributes(reader, defattr);
					reader.MoveToElement();
					return;
				}
			}
			else
			{
				if (reader.NodeType != XmlNodeType.Attribute)
				{
					throw new XmlException("The current position on the Reader is neither an element nor an attribute.", string.Empty);
				}
				do
				{
					if (defattr || !reader.IsDefaultInternal)
					{
						this.WriteStartAttribute(reader.Prefix, reader.LocalName, reader.NamespaceURI);
						while (reader.ReadAttributeValue())
						{
							if (reader.NodeType == XmlNodeType.EntityReference)
							{
								this.WriteEntityRef(reader.Name);
							}
							else
							{
								this.WriteString(reader.Value);
							}
						}
						this.WriteEndAttribute();
					}
				}
				while (reader.MoveToNextAttribute());
			}
		}

		/// <summary>When overridden in a derived class, copies everything from the reader to the writer and moves the reader to the start of the next sibling.</summary>
		/// <param name="reader">The <see cref="T:System.Xml.XmlReader" /> to read from.</param>
		/// <param name="defattr">
		///       <see langword="true" /> to copy the default attributes from the <see langword="XmlReader" />; otherwise, <see langword="false" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="reader" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="reader" /> contains invalid characters.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000D79 RID: 3449 RVA: 0x00058104 File Offset: 0x00056304
		public virtual void WriteNode(XmlReader reader, bool defattr)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			bool canReadValueChunk = reader.CanReadValueChunk;
			int num = (reader.NodeType == XmlNodeType.None) ? -1 : reader.Depth;
			do
			{
				switch (reader.NodeType)
				{
				case XmlNodeType.Element:
					this.WriteStartElement(reader.Prefix, reader.LocalName, reader.NamespaceURI);
					this.WriteAttributes(reader, defattr);
					if (reader.IsEmptyElement)
					{
						this.WriteEndElement();
					}
					break;
				case XmlNodeType.Text:
					if (canReadValueChunk)
					{
						if (this.writeNodeBuffer == null)
						{
							this.writeNodeBuffer = new char[1024];
						}
						int count;
						while ((count = reader.ReadValueChunk(this.writeNodeBuffer, 0, 1024)) > 0)
						{
							this.WriteChars(this.writeNodeBuffer, 0, count);
						}
					}
					else
					{
						this.WriteString(reader.Value);
					}
					break;
				case XmlNodeType.CDATA:
					this.WriteCData(reader.Value);
					break;
				case XmlNodeType.EntityReference:
					this.WriteEntityRef(reader.Name);
					break;
				case XmlNodeType.ProcessingInstruction:
				case XmlNodeType.XmlDeclaration:
					this.WriteProcessingInstruction(reader.Name, reader.Value);
					break;
				case XmlNodeType.Comment:
					this.WriteComment(reader.Value);
					break;
				case XmlNodeType.DocumentType:
					this.WriteDocType(reader.Name, reader.GetAttribute("PUBLIC"), reader.GetAttribute("SYSTEM"), reader.Value);
					break;
				case XmlNodeType.Whitespace:
				case XmlNodeType.SignificantWhitespace:
					this.WriteWhitespace(reader.Value);
					break;
				case XmlNodeType.EndElement:
					this.WriteFullEndElement();
					break;
				}
			}
			while (reader.Read() && (num < reader.Depth || (num == reader.Depth && reader.NodeType == XmlNodeType.EndElement)));
		}

		/// <summary>Copies everything from the <see cref="T:System.Xml.XPath.XPathNavigator" /> object to the writer. The position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> remains unchanged.</summary>
		/// <param name="navigator">The <see cref="T:System.Xml.XPath.XPathNavigator" /> to copy from.</param>
		/// <param name="defattr">
		///       <see langword="true" /> to copy the default attributes; otherwise, <see langword="false" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="navigator" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000D7A RID: 3450 RVA: 0x000582C4 File Offset: 0x000564C4
		public virtual void WriteNode(XPathNavigator navigator, bool defattr)
		{
			if (navigator == null)
			{
				throw new ArgumentNullException("navigator");
			}
			int num = 0;
			navigator = navigator.Clone();
			for (;;)
			{
				IL_18:
				bool flag = false;
				switch (navigator.NodeType)
				{
				case XPathNodeType.Root:
					flag = true;
					break;
				case XPathNodeType.Element:
					this.WriteStartElement(navigator.Prefix, navigator.LocalName, navigator.NamespaceURI);
					if (navigator.MoveToFirstAttribute())
					{
						do
						{
							IXmlSchemaInfo schemaInfo = navigator.SchemaInfo;
							if (defattr || schemaInfo == null || !schemaInfo.IsDefault)
							{
								this.WriteStartAttribute(navigator.Prefix, navigator.LocalName, navigator.NamespaceURI);
								this.WriteString(navigator.Value);
								this.WriteEndAttribute();
							}
						}
						while (navigator.MoveToNextAttribute());
						navigator.MoveToParent();
					}
					if (navigator.MoveToFirstNamespace(XPathNamespaceScope.Local))
					{
						this.WriteLocalNamespaces(navigator);
						navigator.MoveToParent();
					}
					flag = true;
					break;
				case XPathNodeType.Text:
					this.WriteString(navigator.Value);
					break;
				case XPathNodeType.SignificantWhitespace:
				case XPathNodeType.Whitespace:
					this.WriteWhitespace(navigator.Value);
					break;
				case XPathNodeType.ProcessingInstruction:
					this.WriteProcessingInstruction(navigator.LocalName, navigator.Value);
					break;
				case XPathNodeType.Comment:
					this.WriteComment(navigator.Value);
					break;
				}
				if (flag)
				{
					if (navigator.MoveToFirstChild())
					{
						num++;
						continue;
					}
					if (navigator.NodeType == XPathNodeType.Element)
					{
						if (navigator.IsEmptyElement)
						{
							this.WriteEndElement();
						}
						else
						{
							this.WriteFullEndElement();
						}
					}
				}
				while (num != 0)
				{
					if (navigator.MoveToNext())
					{
						goto IL_18;
					}
					num--;
					navigator.MoveToParent();
					if (navigator.NodeType == XPathNodeType.Element)
					{
						this.WriteFullEndElement();
					}
				}
				break;
			}
		}

		/// <summary>Writes an element with the specified local name and value.</summary>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="value">The value of the element.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="localName" /> value is <see langword="null" /> or an empty string.-or-The parameter values are not valid.</exception>
		/// <exception cref="T:System.Text.EncoderFallbackException">There is a character in the buffer that is a valid XML character but is not valid for the output encoding. For example, if the output encoding is ASCII, you should only use characters from the range of 0 to 127 for element and attribute names. The invalid character might be in the argument of this method or in an argument of previous methods that were writing to the buffer. Such characters are escaped by character entity references when possible (for example, in text nodes or attribute values). However, the character entity reference is not allowed in element and attribute names, comments, processing instructions, or CDATA sections.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000D7B RID: 3451 RVA: 0x00058447 File Offset: 0x00056647
		public void WriteElementString(string localName, string value)
		{
			this.WriteElementString(localName, null, value);
		}

		/// <summary>Writes an element with the specified local name, namespace URI, and value.</summary>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="ns">The namespace URI to associate with the element.</param>
		/// <param name="value">The value of the element.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="localName" /> value is <see langword="null" /> or an empty string.-or-The parameter values are not valid.</exception>
		/// <exception cref="T:System.Text.EncoderFallbackException">There is a character in the buffer that is a valid XML character but is not valid for the output encoding. For example, if the output encoding is ASCII, you should only use characters from the range of 0 to 127 for element and attribute names. The invalid character might be in the argument of this method or in an argument of previous methods that were writing to the buffer. Such characters are escaped by character entity references when possible (for example, in text nodes or attribute values). However, the character entity reference is not allowed in element and attribute names, comments, processing instructions, or CDATA sections.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000D7C RID: 3452 RVA: 0x00058452 File Offset: 0x00056652
		public void WriteElementString(string localName, string ns, string value)
		{
			this.WriteStartElement(localName, ns);
			if (value != null && value.Length != 0)
			{
				this.WriteString(value);
			}
			this.WriteEndElement();
		}

		/// <summary>Writes an element with the specified prefix, local name, namespace URI, and value.</summary>
		/// <param name="prefix">The prefix of the element.</param>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="ns">The namespace URI of the element.</param>
		/// <param name="value">The value of the element.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="localName" /> value is <see langword="null" /> or an empty string.-or-The parameter values are not valid.</exception>
		/// <exception cref="T:System.Text.EncoderFallbackException">There is a character in the buffer that is a valid XML character but is not valid for the output encoding. For example, if the output encoding is ASCII, you should only use characters from the range of 0 to 127 for element and attribute names. The invalid character might be in the argument of this method or in an argument of previous methods that were writing to the buffer. Such characters are escaped by character entity references when possible (for example, in text nodes or attribute values). However, the character entity reference is not allowed in element and attribute names, comments, processing instructions, or CDATA sections.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000D7D RID: 3453 RVA: 0x00058474 File Offset: 0x00056674
		public void WriteElementString(string prefix, string localName, string ns, string value)
		{
			this.WriteStartElement(prefix, localName, ns);
			if (value != null && value.Length != 0)
			{
				this.WriteString(value);
			}
			this.WriteEndElement();
		}

		/// <summary>Releases all resources used by the current instance of the <see cref="T:System.Xml.XmlWriter" /> class.</summary>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000D7E RID: 3454 RVA: 0x0005849A File Offset: 0x0005669A
		public void Dispose()
		{
			this.Dispose(true);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Xml.XmlWriter" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///       <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000D7F RID: 3455 RVA: 0x000584A3 File Offset: 0x000566A3
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this.WriteState != WriteState.Closed)
			{
				this.Close();
			}
		}

		// Token: 0x06000D80 RID: 3456 RVA: 0x000584B8 File Offset: 0x000566B8
		private void WriteLocalNamespaces(XPathNavigator nsNav)
		{
			string localName = nsNav.LocalName;
			string value = nsNav.Value;
			if (nsNav.MoveToNextNamespace(XPathNamespaceScope.Local))
			{
				this.WriteLocalNamespaces(nsNav);
			}
			if (localName.Length == 0)
			{
				this.WriteAttributeString(string.Empty, "xmlns", "http://www.w3.org/2000/xmlns/", value);
				return;
			}
			this.WriteAttributeString("xmlns", localName, "http://www.w3.org/2000/xmlns/", value);
		}

		/// <summary>Creates a new <see cref="T:System.Xml.XmlWriter" /> instance using the specified filename.</summary>
		/// <param name="outputFileName">The file to which you want to write. The <see cref="T:System.Xml.XmlWriter" /> creates a file at the specified path and writes to it in XML 1.0 text syntax. The <paramref name="outputFileName" /> must be a file system path.</param>
		/// <returns>An <see cref="T:System.Xml.XmlWriter" /> object.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="url" /> value is <see langword="null" />.</exception>
		// Token: 0x06000D81 RID: 3457 RVA: 0x00058514 File Offset: 0x00056714
		public static XmlWriter Create(string outputFileName)
		{
			return XmlWriter.Create(outputFileName, null);
		}

		/// <summary>Creates a new <see cref="T:System.Xml.XmlWriter" /> instance using the filename and <see cref="T:System.Xml.XmlWriterSettings" /> object.</summary>
		/// <param name="outputFileName">The file to which you want to write. The <see cref="T:System.Xml.XmlWriter" /> creates a file at the specified path and writes to it in XML 1.0 text syntax. The <paramref name="outputFileName" /> must be a file system path.</param>
		/// <param name="settings">The <see cref="T:System.Xml.XmlWriterSettings" /> object used to configure the new <see cref="T:System.Xml.XmlWriter" /> instance. If this is <see langword="null" />, a <see cref="T:System.Xml.XmlWriterSettings" /> with default settings is used.If the <see cref="T:System.Xml.XmlWriter" /> is being used with the <see cref="M:System.Xml.Xsl.XslCompiledTransform.Transform(System.String,System.Xml.XmlWriter)" /> method, you should use the <see cref="P:System.Xml.Xsl.XslCompiledTransform.OutputSettings" /> property to obtain an <see cref="T:System.Xml.XmlWriterSettings" /> object with the correct settings. This ensures that the created <see cref="T:System.Xml.XmlWriter" /> object has the correct output settings.</param>
		/// <returns>An <see cref="T:System.Xml.XmlWriter" /> object.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="url" /> value is <see langword="null" />.</exception>
		// Token: 0x06000D82 RID: 3458 RVA: 0x0005851D File Offset: 0x0005671D
		public static XmlWriter Create(string outputFileName, XmlWriterSettings settings)
		{
			if (settings == null)
			{
				settings = new XmlWriterSettings();
			}
			return settings.CreateWriter(outputFileName);
		}

		/// <summary>Creates a new <see cref="T:System.Xml.XmlWriter" /> instance using the specified stream.</summary>
		/// <param name="output">The stream to which you want to write. The <see cref="T:System.Xml.XmlWriter" /> writes XML 1.0 text syntax and appends it to the specified stream.</param>
		/// <returns>An <see cref="T:System.Xml.XmlWriter" /> object.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="stream" /> value is <see langword="null" />.</exception>
		// Token: 0x06000D83 RID: 3459 RVA: 0x00058530 File Offset: 0x00056730
		public static XmlWriter Create(Stream output)
		{
			return XmlWriter.Create(output, null);
		}

		/// <summary>Creates a new <see cref="T:System.Xml.XmlWriter" /> instance using the stream and <see cref="T:System.Xml.XmlWriterSettings" /> object.</summary>
		/// <param name="output">The stream to which you want to write. The <see cref="T:System.Xml.XmlWriter" /> writes XML 1.0 text syntax and appends it to the specified stream.</param>
		/// <param name="settings">The <see cref="T:System.Xml.XmlWriterSettings" /> object used to configure the new <see cref="T:System.Xml.XmlWriter" /> instance. If this is <see langword="null" />, a <see cref="T:System.Xml.XmlWriterSettings" /> with default settings is used.If the <see cref="T:System.Xml.XmlWriter" /> is being used with the <see cref="M:System.Xml.Xsl.XslCompiledTransform.Transform(System.String,System.Xml.XmlWriter)" /> method, you should use the <see cref="P:System.Xml.Xsl.XslCompiledTransform.OutputSettings" /> property to obtain an <see cref="T:System.Xml.XmlWriterSettings" /> object with the correct settings. This ensures that the created <see cref="T:System.Xml.XmlWriter" /> object has the correct output settings.</param>
		/// <returns>An <see cref="T:System.Xml.XmlWriter" /> object.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="stream" /> value is <see langword="null" />.</exception>
		// Token: 0x06000D84 RID: 3460 RVA: 0x00058539 File Offset: 0x00056739
		public static XmlWriter Create(Stream output, XmlWriterSettings settings)
		{
			if (settings == null)
			{
				settings = new XmlWriterSettings();
			}
			return settings.CreateWriter(output);
		}

		/// <summary>Creates a new <see cref="T:System.Xml.XmlWriter" /> instance using the specified <see cref="T:System.IO.TextWriter" />.</summary>
		/// <param name="output">The <see cref="T:System.IO.TextWriter" /> to which you want to write. The <see cref="T:System.Xml.XmlWriter" /> writes XML 1.0 text syntax and appends it to the specified <see cref="T:System.IO.TextWriter" />.</param>
		/// <returns>An <see cref="T:System.Xml.XmlWriter" /> object.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="text" /> value is <see langword="null" />.</exception>
		// Token: 0x06000D85 RID: 3461 RVA: 0x0005854C File Offset: 0x0005674C
		public static XmlWriter Create(TextWriter output)
		{
			return XmlWriter.Create(output, null);
		}

		/// <summary>Creates a new <see cref="T:System.Xml.XmlWriter" /> instance using the <see cref="T:System.IO.TextWriter" /> and <see cref="T:System.Xml.XmlWriterSettings" /> objects.</summary>
		/// <param name="output">The <see cref="T:System.IO.TextWriter" /> to which you want to write. The <see cref="T:System.Xml.XmlWriter" /> writes XML 1.0 text syntax and appends it to the specified <see cref="T:System.IO.TextWriter" />.</param>
		/// <param name="settings">The <see cref="T:System.Xml.XmlWriterSettings" /> object used to configure the new <see cref="T:System.Xml.XmlWriter" /> instance. If this is <see langword="null" />, a <see cref="T:System.Xml.XmlWriterSettings" /> with default settings is used.If the <see cref="T:System.Xml.XmlWriter" /> is being used with the <see cref="M:System.Xml.Xsl.XslCompiledTransform.Transform(System.String,System.Xml.XmlWriter)" /> method, you should use the <see cref="P:System.Xml.Xsl.XslCompiledTransform.OutputSettings" /> property to obtain an <see cref="T:System.Xml.XmlWriterSettings" /> object with the correct settings. This ensures that the created <see cref="T:System.Xml.XmlWriter" /> object has the correct output settings.</param>
		/// <returns>An <see cref="T:System.Xml.XmlWriter" /> object.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="text" /> value is <see langword="null" />.</exception>
		// Token: 0x06000D86 RID: 3462 RVA: 0x00058555 File Offset: 0x00056755
		public static XmlWriter Create(TextWriter output, XmlWriterSettings settings)
		{
			if (settings == null)
			{
				settings = new XmlWriterSettings();
			}
			return settings.CreateWriter(output);
		}

		/// <summary>Creates a new <see cref="T:System.Xml.XmlWriter" /> instance using the specified <see cref="T:System.Text.StringBuilder" />.</summary>
		/// <param name="output">The <see cref="T:System.Text.StringBuilder" /> to which to write to. Content written by the <see cref="T:System.Xml.XmlWriter" /> is appended to the <see cref="T:System.Text.StringBuilder" />.</param>
		/// <returns>An <see cref="T:System.Xml.XmlWriter" /> object.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="builder" /> value is <see langword="null" />.</exception>
		// Token: 0x06000D87 RID: 3463 RVA: 0x00058568 File Offset: 0x00056768
		public static XmlWriter Create(StringBuilder output)
		{
			return XmlWriter.Create(output, null);
		}

		/// <summary>Creates a new <see cref="T:System.Xml.XmlWriter" /> instance using the <see cref="T:System.Text.StringBuilder" /> and <see cref="T:System.Xml.XmlWriterSettings" /> objects.</summary>
		/// <param name="output">The <see cref="T:System.Text.StringBuilder" /> to which to write to. Content written by the <see cref="T:System.Xml.XmlWriter" /> is appended to the <see cref="T:System.Text.StringBuilder" />.</param>
		/// <param name="settings">The <see cref="T:System.Xml.XmlWriterSettings" /> object used to configure the new <see cref="T:System.Xml.XmlWriter" /> instance. If this is <see langword="null" />, a <see cref="T:System.Xml.XmlWriterSettings" /> with default settings is used.If the <see cref="T:System.Xml.XmlWriter" /> is being used with the <see cref="M:System.Xml.Xsl.XslCompiledTransform.Transform(System.String,System.Xml.XmlWriter)" /> method, you should use the <see cref="P:System.Xml.Xsl.XslCompiledTransform.OutputSettings" /> property to obtain an <see cref="T:System.Xml.XmlWriterSettings" /> object with the correct settings. This ensures that the created <see cref="T:System.Xml.XmlWriter" /> object has the correct output settings.</param>
		/// <returns>An <see cref="T:System.Xml.XmlWriter" /> object.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="builder" /> value is <see langword="null" />.</exception>
		// Token: 0x06000D88 RID: 3464 RVA: 0x00058571 File Offset: 0x00056771
		public static XmlWriter Create(StringBuilder output, XmlWriterSettings settings)
		{
			if (settings == null)
			{
				settings = new XmlWriterSettings();
			}
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			return settings.CreateWriter(new StringWriter(output, CultureInfo.InvariantCulture));
		}

		/// <summary>Creates a new <see cref="T:System.Xml.XmlWriter" /> instance using the specified <see cref="T:System.Xml.XmlWriter" /> object.</summary>
		/// <param name="output">The <see cref="T:System.Xml.XmlWriter" /> object that you want to use as the underlying writer.</param>
		/// <returns>An <see cref="T:System.Xml.XmlWriter" /> object that is wrapped around the specified <see cref="T:System.Xml.XmlWriter" /> object.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="writer" /> value is <see langword="null" />.</exception>
		// Token: 0x06000D89 RID: 3465 RVA: 0x0005859C File Offset: 0x0005679C
		public static XmlWriter Create(XmlWriter output)
		{
			return XmlWriter.Create(output, null);
		}

		/// <summary>Creates a new <see cref="T:System.Xml.XmlWriter" /> instance using the specified <see cref="T:System.Xml.XmlWriter" /> and <see cref="T:System.Xml.XmlWriterSettings" /> objects.</summary>
		/// <param name="output">The <see cref="T:System.Xml.XmlWriter" /> object that you want to use as the underlying writer.</param>
		/// <param name="settings">The <see cref="T:System.Xml.XmlWriterSettings" /> object used to configure the new <see cref="T:System.Xml.XmlWriter" /> instance. If this is <see langword="null" />, a <see cref="T:System.Xml.XmlWriterSettings" /> with default settings is used.If the <see cref="T:System.Xml.XmlWriter" /> is being used with the <see cref="M:System.Xml.Xsl.XslCompiledTransform.Transform(System.String,System.Xml.XmlWriter)" /> method, you should use the <see cref="P:System.Xml.Xsl.XslCompiledTransform.OutputSettings" /> property to obtain an <see cref="T:System.Xml.XmlWriterSettings" /> object with the correct settings. This ensures that the created <see cref="T:System.Xml.XmlWriter" /> object has the correct output settings.</param>
		/// <returns>An <see cref="T:System.Xml.XmlWriter" /> object that is wrapped around the specified <see cref="T:System.Xml.XmlWriter" /> object.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="writer" /> value is <see langword="null" />.</exception>
		// Token: 0x06000D8A RID: 3466 RVA: 0x000585A5 File Offset: 0x000567A5
		public static XmlWriter Create(XmlWriter output, XmlWriterSettings settings)
		{
			if (settings == null)
			{
				settings = new XmlWriterSettings();
			}
			return settings.CreateWriter(output);
		}

		/// <summary>Asynchronously writes the XML declaration with the version "1.0".</summary>
		/// <returns>The task that represents the asynchronous <see langword="WriteStartDocument" /> operation.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> asynchronous method was called without setting the <see cref="P:System.Xml.XmlWriterSettings.Async" /> flag to <see langword="true" />. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “Set XmlWriterSettings.Async to true if you want to use Async Methods.”</exception>
		// Token: 0x06000D8B RID: 3467 RVA: 0x0000349C File Offset: 0x0000169C
		public virtual Task WriteStartDocumentAsync()
		{
			throw new NotImplementedException();
		}

		/// <summary>Asynchronously writes the XML declaration with the version "1.0" and the standalone attribute.</summary>
		/// <param name="standalone">If <see langword="true" />, it writes "standalone=yes"; if <see langword="false" />, it writes "standalone=no".</param>
		/// <returns>The task that represents the asynchronous <see langword="WriteStartDocument" /> operation.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> asynchronous method was called without setting the <see cref="P:System.Xml.XmlWriterSettings.Async" /> flag to <see langword="true" />. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “Set XmlWriterSettings.Async to true if you want to use Async Methods.”</exception>
		// Token: 0x06000D8C RID: 3468 RVA: 0x0000349C File Offset: 0x0000169C
		public virtual Task WriteStartDocumentAsync(bool standalone)
		{
			throw new NotImplementedException();
		}

		/// <summary>Asynchronously closes any open elements or attributes and puts the writer back in the Start state.</summary>
		/// <returns>The task that represents the asynchronous <see langword="WriteEndDocument" /> operation.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> asynchronous method was called without setting the <see cref="P:System.Xml.XmlWriterSettings.Async" /> flag to <see langword="true" />. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “Set XmlWriterSettings.Async to true if you want to use Async Methods.”</exception>
		// Token: 0x06000D8D RID: 3469 RVA: 0x0000349C File Offset: 0x0000169C
		public virtual Task WriteEndDocumentAsync()
		{
			throw new NotImplementedException();
		}

		/// <summary>Asynchronously writes the DOCTYPE declaration with the specified name and optional attributes.</summary>
		/// <param name="name">The name of the DOCTYPE. This must be non-empty.</param>
		/// <param name="pubid">If non-null it also writes PUBLIC "pubid" "sysid" where <paramref name="pubid" /> and <paramref name="sysid" /> are replaced with the value of the given arguments.</param>
		/// <param name="sysid">If <paramref name="pubid" /> is <see langword="null" /> and <paramref name="sysid" /> is non-null it writes SYSTEM "sysid" where <paramref name="sysid" /> is replaced with the value of this argument.</param>
		/// <param name="subset">If non-null it writes [subset] where subset is replaced with the value of this argument.</param>
		/// <returns>The task that represents the asynchronous <see langword="WriteDocType" /> operation.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> asynchronous method was called without setting the <see cref="P:System.Xml.XmlWriterSettings.Async" /> flag to <see langword="true" />. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “Set XmlWriterSettings.Async to true if you want to use Async Methods.”</exception>
		// Token: 0x06000D8E RID: 3470 RVA: 0x0000349C File Offset: 0x0000169C
		public virtual Task WriteDocTypeAsync(string name, string pubid, string sysid, string subset)
		{
			throw new NotImplementedException();
		}

		/// <summary>Asynchronously writes the specified start tag and associates it with the given namespace and prefix.</summary>
		/// <param name="prefix">The namespace prefix of the element.</param>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="ns">The namespace URI to associate with the element.</param>
		/// <returns>The task that represents the asynchronous <see langword="WriteStartElement" /> operation.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> asynchronous method was called without setting the <see cref="P:System.Xml.XmlWriterSettings.Async" /> flag to <see langword="true" />. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “Set XmlWriterSettings.Async to true if you want to use Async Methods.”</exception>
		// Token: 0x06000D8F RID: 3471 RVA: 0x0000349C File Offset: 0x0000169C
		public virtual Task WriteStartElementAsync(string prefix, string localName, string ns)
		{
			throw new NotImplementedException();
		}

		/// <summary>Asynchronously closes one element and pops the corresponding namespace scope.</summary>
		/// <returns>The task that represents the asynchronous <see langword="WriteEndElement" /> operation.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> asynchronous method was called without setting the <see cref="P:System.Xml.XmlWriterSettings.Async" /> flag to <see langword="true" />. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “Set XmlWriterSettings.Async to true if you want to use Async Methods.”</exception>
		// Token: 0x06000D90 RID: 3472 RVA: 0x0000349C File Offset: 0x0000169C
		public virtual Task WriteEndElementAsync()
		{
			throw new NotImplementedException();
		}

		/// <summary>Asynchronously closes one element and pops the corresponding namespace scope.</summary>
		/// <returns>The task that represents the asynchronous <see langword="WriteFullEndElement" /> operation.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> asynchronous method was called without setting the <see cref="P:System.Xml.XmlWriterSettings.Async" /> flag to <see langword="true" />. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “Set XmlWriterSettings.Async to true if you want to use Async Methods.”</exception>
		// Token: 0x06000D91 RID: 3473 RVA: 0x0000349C File Offset: 0x0000169C
		public virtual Task WriteFullEndElementAsync()
		{
			throw new NotImplementedException();
		}

		/// <summary>Asynchronously writes out the attribute with the specified prefix, local name, namespace URI, and value.</summary>
		/// <param name="prefix">The namespace prefix of the attribute.</param>
		/// <param name="localName">The local name of the attribute.</param>
		/// <param name="ns">The namespace URI of the attribute.</param>
		/// <param name="value">The value of the attribute.</param>
		/// <returns>The task that represents the asynchronous <see langword="WriteAttributeString" /> operation.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> asynchronous method was called without setting the <see cref="P:System.Xml.XmlWriterSettings.Async" /> flag to <see langword="true" />. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “Set XmlWriterSettings.Async to true if you want to use Async Methods.”</exception>
		// Token: 0x06000D92 RID: 3474 RVA: 0x000585B8 File Offset: 0x000567B8
		public Task WriteAttributeStringAsync(string prefix, string localName, string ns, string value)
		{
			Task task = this.WriteStartAttributeAsync(prefix, localName, ns);
			if (task.IsSuccess())
			{
				return this.WriteStringAsync(value).CallTaskFuncWhenFinish(new Func<Task>(this.WriteEndAttributeAsync));
			}
			return this.WriteAttributeStringAsyncHelper(task, value);
		}

		// Token: 0x06000D93 RID: 3475 RVA: 0x000585FC File Offset: 0x000567FC
		private Task WriteAttributeStringAsyncHelper(Task task, string value)
		{
			XmlWriter.<WriteAttributeStringAsyncHelper>d__82 <WriteAttributeStringAsyncHelper>d__;
			<WriteAttributeStringAsyncHelper>d__.<>4__this = this;
			<WriteAttributeStringAsyncHelper>d__.task = task;
			<WriteAttributeStringAsyncHelper>d__.value = value;
			<WriteAttributeStringAsyncHelper>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteAttributeStringAsyncHelper>d__.<>1__state = -1;
			<WriteAttributeStringAsyncHelper>d__.<>t__builder.Start<XmlWriter.<WriteAttributeStringAsyncHelper>d__82>(ref <WriteAttributeStringAsyncHelper>d__);
			return <WriteAttributeStringAsyncHelper>d__.<>t__builder.Task;
		}

		/// <summary>Asynchronously writes the start of an attribute with the specified prefix, local name, and namespace URI.</summary>
		/// <param name="prefix">The namespace prefix of the attribute.</param>
		/// <param name="localName">The local name of the attribute.</param>
		/// <param name="ns">The namespace URI for the attribute.</param>
		/// <returns>The task that represents the asynchronous <see langword="WriteStartAttribute" /> operation.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> asynchronous method was called without setting the <see cref="P:System.Xml.XmlWriterSettings.Async" /> flag to <see langword="true" />. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “Set XmlWriterSettings.Async to true if you want to use Async Methods.”</exception>
		// Token: 0x06000D94 RID: 3476 RVA: 0x0000349C File Offset: 0x0000169C
		protected internal virtual Task WriteStartAttributeAsync(string prefix, string localName, string ns)
		{
			throw new NotImplementedException();
		}

		/// <summary>Asynchronously closes the previous <see cref="M:System.Xml.XmlWriter.WriteStartAttribute(System.String,System.String)" /> call.</summary>
		/// <returns>The task that represents the asynchronous <see langword="WriteEndAttribute" /> operation.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> asynchronous method was called without setting the <see cref="P:System.Xml.XmlWriterSettings.Async" /> flag to <see langword="true" />. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “Set XmlWriterSettings.Async to true if you want to use Async Methods.”</exception>
		// Token: 0x06000D95 RID: 3477 RVA: 0x0000349C File Offset: 0x0000169C
		protected internal virtual Task WriteEndAttributeAsync()
		{
			throw new NotImplementedException();
		}

		/// <summary>Asynchronously writes out a &lt;![CDATA[...]]&gt; block containing the specified text.</summary>
		/// <param name="text">The text to place inside the CDATA block.</param>
		/// <returns>The task that represents the asynchronous <see langword="WriteCData" /> operation.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> asynchronous method was called without setting the <see cref="P:System.Xml.XmlWriterSettings.Async" /> flag to <see langword="true" />. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “Set XmlWriterSettings.Async to true if you want to use Async Methods.”</exception>
		// Token: 0x06000D96 RID: 3478 RVA: 0x0000349C File Offset: 0x0000169C
		public virtual Task WriteCDataAsync(string text)
		{
			throw new NotImplementedException();
		}

		/// <summary>Asynchronously writes out a comment &lt;!--...--&gt; containing the specified text.</summary>
		/// <param name="text">Text to place inside the comment.</param>
		/// <returns>The task that represents the asynchronous <see langword="WriteComment" /> operation.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> asynchronous method was called without setting the <see cref="P:System.Xml.XmlWriterSettings.Async" /> flag to <see langword="true" />. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “Set XmlWriterSettings.Async to true if you want to use Async Methods.”</exception>
		// Token: 0x06000D97 RID: 3479 RVA: 0x0000349C File Offset: 0x0000169C
		public virtual Task WriteCommentAsync(string text)
		{
			throw new NotImplementedException();
		}

		/// <summary>Asynchronously writes out a processing instruction with a space between the name and text as follows: &lt;?name text?&gt;.</summary>
		/// <param name="name">The name of the processing instruction.</param>
		/// <param name="text">The text to include in the processing instruction.</param>
		/// <returns>The task that represents the asynchronous <see langword="WriteProcessingInstruction" /> operation.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> asynchronous method was called without setting the <see cref="P:System.Xml.XmlWriterSettings.Async" /> flag to <see langword="true" />. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “Set XmlWriterSettings.Async to true if you want to use Async Methods.”</exception>
		// Token: 0x06000D98 RID: 3480 RVA: 0x0000349C File Offset: 0x0000169C
		public virtual Task WriteProcessingInstructionAsync(string name, string text)
		{
			throw new NotImplementedException();
		}

		/// <summary>Asynchronously writes out an entity reference as <see langword="&amp;name;" />.</summary>
		/// <param name="name">The name of the entity reference.</param>
		/// <returns>The task that represents the asynchronous <see langword="WriteEntityRef" /> operation.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> asynchronous method was called without setting the <see cref="P:System.Xml.XmlWriterSettings.Async" /> flag to <see langword="true" />. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “Set XmlWriterSettings.Async to true if you want to use Async Methods.”</exception>
		// Token: 0x06000D99 RID: 3481 RVA: 0x0000349C File Offset: 0x0000169C
		public virtual Task WriteEntityRefAsync(string name)
		{
			throw new NotImplementedException();
		}

		/// <summary>Asynchronously forces the generation of a character entity for the specified Unicode character value.</summary>
		/// <param name="ch">The Unicode character for which to generate a character entity.</param>
		/// <returns>The task that represents the asynchronous <see langword="WriteCharEntity" /> operation.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> asynchronous method was called without setting the <see cref="P:System.Xml.XmlWriterSettings.Async" /> flag to <see langword="true" />. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “Set XmlWriterSettings.Async to true if you want to use Async Methods.”</exception>
		// Token: 0x06000D9A RID: 3482 RVA: 0x0000349C File Offset: 0x0000169C
		public virtual Task WriteCharEntityAsync(char ch)
		{
			throw new NotImplementedException();
		}

		/// <summary>Asynchronously writes out the given white space.</summary>
		/// <param name="ws">The string of white space characters.</param>
		/// <returns>The task that represents the asynchronous <see langword="WriteWhitespace" /> operation.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> asynchronous method was called without setting the <see cref="P:System.Xml.XmlWriterSettings.Async" /> flag to <see langword="true" />. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “Set XmlWriterSettings.Async to true if you want to use Async Methods.”</exception>
		// Token: 0x06000D9B RID: 3483 RVA: 0x0000349C File Offset: 0x0000169C
		public virtual Task WriteWhitespaceAsync(string ws)
		{
			throw new NotImplementedException();
		}

		/// <summary>Asynchronously writes the given text content.</summary>
		/// <param name="text">The text to write.</param>
		/// <returns>The task that represents the asynchronous <see langword="WriteString" /> operation.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> asynchronous method was called without setting the <see cref="P:System.Xml.XmlWriterSettings.Async" /> flag to <see langword="true" />. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “Set XmlWriterSettings.Async to true if you want to use Async Methods.”</exception>
		// Token: 0x06000D9C RID: 3484 RVA: 0x0000349C File Offset: 0x0000169C
		public virtual Task WriteStringAsync(string text)
		{
			throw new NotImplementedException();
		}

		/// <summary>Asynchronously generates and writes the surrogate character entity for the surrogate character pair.</summary>
		/// <param name="lowChar">The low surrogate. This must be a value between 0xDC00 and 0xDFFF.</param>
		/// <param name="highChar">The high surrogate. This must be a value between 0xD800 and 0xDBFF.</param>
		/// <returns>The task that represents the asynchronous <see langword="WriteSurrogateCharEntity" /> operation.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> asynchronous method was called without setting the <see cref="P:System.Xml.XmlWriterSettings.Async" /> flag to <see langword="true" />. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “Set XmlWriterSettings.Async to true if you want to use Async Methods.”</exception>
		// Token: 0x06000D9D RID: 3485 RVA: 0x0000349C File Offset: 0x0000169C
		public virtual Task WriteSurrogateCharEntityAsync(char lowChar, char highChar)
		{
			throw new NotImplementedException();
		}

		/// <summary>Asynchronously writes text one buffer at a time.</summary>
		/// <param name="buffer">Character array containing the text to write.</param>
		/// <param name="index">The position in the buffer indicating the start of the text to write.</param>
		/// <param name="count">The number of characters to write.</param>
		/// <returns>The task that represents the asynchronous <see langword="WriteChars" /> operation.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> asynchronous method was called without setting the <see cref="P:System.Xml.XmlWriterSettings.Async" /> flag to <see langword="true" />. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “Set XmlWriterSettings.Async to true if you want to use Async Methods.”</exception>
		// Token: 0x06000D9E RID: 3486 RVA: 0x0000349C File Offset: 0x0000169C
		public virtual Task WriteCharsAsync(char[] buffer, int index, int count)
		{
			throw new NotImplementedException();
		}

		/// <summary>Asynchronously writes raw markup manually from a character buffer.</summary>
		/// <param name="buffer">Character array containing the text to write.</param>
		/// <param name="index">The position within the buffer indicating the start of the text to write.</param>
		/// <param name="count">The number of characters to write.</param>
		/// <returns>The task that represents the asynchronous <see langword="WriteRaw" /> operation.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> asynchronous method was called without setting the <see cref="P:System.Xml.XmlWriterSettings.Async" /> flag to <see langword="true" />. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “Set XmlWriterSettings.Async to true if you want to use Async Methods.”</exception>
		// Token: 0x06000D9F RID: 3487 RVA: 0x0000349C File Offset: 0x0000169C
		public virtual Task WriteRawAsync(char[] buffer, int index, int count)
		{
			throw new NotImplementedException();
		}

		/// <summary>Asynchronously writes raw markup manually from a string.</summary>
		/// <param name="data">String containing the text to write.</param>
		/// <returns>The task that represents the asynchronous <see langword="WriteRaw" /> operation.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> asynchronous method was called without setting the <see cref="P:System.Xml.XmlWriterSettings.Async" /> flag to <see langword="true" />. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “Set XmlWriterSettings.Async to true if you want to use Async Methods.”</exception>
		// Token: 0x06000DA0 RID: 3488 RVA: 0x0000349C File Offset: 0x0000169C
		public virtual Task WriteRawAsync(string data)
		{
			throw new NotImplementedException();
		}

		/// <summary>Asynchronously encodes the specified binary bytes as Base64 and writes out the resulting text.</summary>
		/// <param name="buffer">Byte array to encode.</param>
		/// <param name="index">The position in the buffer indicating the start of the bytes to write.</param>
		/// <param name="count">The number of bytes to write.</param>
		/// <returns>The task that represents the asynchronous <see langword="WriteBase64" /> operation.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> asynchronous method was called without setting the <see cref="P:System.Xml.XmlWriterSettings.Async" /> flag to <see langword="true" />. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “Set XmlWriterSettings.Async to true if you want to use Async Methods.”</exception>
		// Token: 0x06000DA1 RID: 3489 RVA: 0x0000349C File Offset: 0x0000169C
		public virtual Task WriteBase64Async(byte[] buffer, int index, int count)
		{
			throw new NotImplementedException();
		}

		/// <summary>Asynchronously encodes the specified binary bytes as <see langword="BinHex" /> and writes out the resulting text.</summary>
		/// <param name="buffer">Byte array to encode.</param>
		/// <param name="index">The position in the buffer indicating the start of the bytes to write.</param>
		/// <param name="count">The number of bytes to write.</param>
		/// <returns>The task that represents the asynchronous <see langword="WriteBinHex" /> operation.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> asynchronous method was called without setting the <see cref="P:System.Xml.XmlWriterSettings.Async" /> flag to <see langword="true" />. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “Set XmlWriterSettings.Async to true if you want to use Async Methods.”</exception>
		// Token: 0x06000DA2 RID: 3490 RVA: 0x0005864F File Offset: 0x0005684F
		public virtual Task WriteBinHexAsync(byte[] buffer, int index, int count)
		{
			return BinHexEncoder.EncodeAsync(buffer, index, count, this);
		}

		/// <summary>Asynchronously flushes whatever is in the buffer to the underlying streams and also flushes the underlying stream.</summary>
		/// <returns>The task that represents the asynchronous <see langword="Flush" /> operation.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> asynchronous method was called without setting the <see cref="P:System.Xml.XmlWriterSettings.Async" /> flag to <see langword="true" />. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “Set XmlWriterSettings.Async to true if you want to use Async Methods.”</exception>
		// Token: 0x06000DA3 RID: 3491 RVA: 0x0000349C File Offset: 0x0000169C
		public virtual Task FlushAsync()
		{
			throw new NotImplementedException();
		}

		/// <summary>Asynchronously writes out the specified name, ensuring it is a valid NmToken according to the W3C XML 1.0 recommendation (http://www.w3.org/TR/1998/REC-xml-19980210#NT-Name).</summary>
		/// <param name="name">The name to write.</param>
		/// <returns>The task that represents the asynchronous <see langword="WriteNmToken" /> operation.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> asynchronous method was called without setting the <see cref="P:System.Xml.XmlWriterSettings.Async" /> flag to <see langword="true" />. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “Set XmlWriterSettings.Async to true if you want to use Async Methods.”</exception>
		// Token: 0x06000DA4 RID: 3492 RVA: 0x0005865A File Offset: 0x0005685A
		public virtual Task WriteNmTokenAsync(string name)
		{
			if (name == null || name.Length == 0)
			{
				throw new ArgumentException(Res.GetString("The empty string '' is not a valid name."));
			}
			return this.WriteStringAsync(XmlConvert.VerifyNMTOKEN(name, ExceptionType.ArgumentException));
		}

		/// <summary>Asynchronously writes out the specified name, ensuring it is a valid name according to the W3C XML 1.0 recommendation (http://www.w3.org/TR/1998/REC-xml-19980210#NT-Name).</summary>
		/// <param name="name">The name to write.</param>
		/// <returns>The task that represents the asynchronous <see langword="WriteName" /> operation.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> asynchronous method was called without setting the <see cref="P:System.Xml.XmlWriterSettings.Async" /> flag to <see langword="true" />. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “Set XmlWriterSettings.Async to true if you want to use Async Methods.”</exception>
		// Token: 0x06000DA5 RID: 3493 RVA: 0x00058684 File Offset: 0x00056884
		public virtual Task WriteNameAsync(string name)
		{
			return this.WriteStringAsync(XmlConvert.VerifyQName(name, ExceptionType.ArgumentException));
		}

		/// <summary>Asynchronously writes out the namespace-qualified name. This method looks up the prefix that is in scope for the given namespace.</summary>
		/// <param name="localName">The local name to write.</param>
		/// <param name="ns">The namespace URI for the name.</param>
		/// <returns>The task that represents the asynchronous <see langword="WriteQualifiedName" /> operation.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> asynchronous method was called without setting the <see cref="P:System.Xml.XmlWriterSettings.Async" /> flag to <see langword="true" />. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “Set XmlWriterSettings.Async to true if you want to use Async Methods.”</exception>
		// Token: 0x06000DA6 RID: 3494 RVA: 0x00058694 File Offset: 0x00056894
		public virtual Task WriteQualifiedNameAsync(string localName, string ns)
		{
			XmlWriter.<WriteQualifiedNameAsync>d__101 <WriteQualifiedNameAsync>d__;
			<WriteQualifiedNameAsync>d__.<>4__this = this;
			<WriteQualifiedNameAsync>d__.localName = localName;
			<WriteQualifiedNameAsync>d__.ns = ns;
			<WriteQualifiedNameAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteQualifiedNameAsync>d__.<>1__state = -1;
			<WriteQualifiedNameAsync>d__.<>t__builder.Start<XmlWriter.<WriteQualifiedNameAsync>d__101>(ref <WriteQualifiedNameAsync>d__);
			return <WriteQualifiedNameAsync>d__.<>t__builder.Task;
		}

		/// <summary>Asynchronously writes out all the attributes found at the current position in the <see cref="T:System.Xml.XmlReader" />.</summary>
		/// <param name="reader">The <see langword="XmlReader" /> from which to copy the attributes.</param>
		/// <param name="defattr">
		///       <see langword="true" /> to copy the default attributes from the <see langword="XmlReader" />; otherwise, <see langword="false" />.</param>
		/// <returns>The task that represents the asynchronous <see langword="WriteAttributes" /> operation.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> asynchronous method was called without setting the <see cref="P:System.Xml.XmlWriterSettings.Async" /> flag to <see langword="true" />. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “Set XmlWriterSettings.Async to true if you want to use Async Methods.”</exception>
		// Token: 0x06000DA7 RID: 3495 RVA: 0x000586E8 File Offset: 0x000568E8
		public virtual Task WriteAttributesAsync(XmlReader reader, bool defattr)
		{
			XmlWriter.<WriteAttributesAsync>d__102 <WriteAttributesAsync>d__;
			<WriteAttributesAsync>d__.<>4__this = this;
			<WriteAttributesAsync>d__.reader = reader;
			<WriteAttributesAsync>d__.defattr = defattr;
			<WriteAttributesAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteAttributesAsync>d__.<>1__state = -1;
			<WriteAttributesAsync>d__.<>t__builder.Start<XmlWriter.<WriteAttributesAsync>d__102>(ref <WriteAttributesAsync>d__);
			return <WriteAttributesAsync>d__.<>t__builder.Task;
		}

		/// <summary>Asynchronously copies everything from the reader to the writer and moves the reader to the start of the next sibling.</summary>
		/// <param name="reader">The <see cref="T:System.Xml.XmlReader" /> to read from.</param>
		/// <param name="defattr">
		///       <see langword="true" /> to copy the default attributes from the <see langword="XmlReader" />; otherwise, <see langword="false" />.</param>
		/// <returns>The task that represents the asynchronous <see langword="WriteNode" /> operation.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> asynchronous method was called without setting the <see cref="P:System.Xml.XmlWriterSettings.Async" /> flag to <see langword="true" />. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “Set XmlWriterSettings.Async to true if you want to use Async Methods.”</exception>
		// Token: 0x06000DA8 RID: 3496 RVA: 0x0005873B File Offset: 0x0005693B
		public virtual Task WriteNodeAsync(XmlReader reader, bool defattr)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			if (reader.Settings != null && reader.Settings.Async)
			{
				return this.WriteNodeAsync_CallAsyncReader(reader, defattr);
			}
			return this.WriteNodeAsync_CallSyncReader(reader, defattr);
		}

		// Token: 0x06000DA9 RID: 3497 RVA: 0x00058774 File Offset: 0x00056974
		internal Task WriteNodeAsync_CallSyncReader(XmlReader reader, bool defattr)
		{
			XmlWriter.<WriteNodeAsync_CallSyncReader>d__104 <WriteNodeAsync_CallSyncReader>d__;
			<WriteNodeAsync_CallSyncReader>d__.<>4__this = this;
			<WriteNodeAsync_CallSyncReader>d__.reader = reader;
			<WriteNodeAsync_CallSyncReader>d__.defattr = defattr;
			<WriteNodeAsync_CallSyncReader>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteNodeAsync_CallSyncReader>d__.<>1__state = -1;
			<WriteNodeAsync_CallSyncReader>d__.<>t__builder.Start<XmlWriter.<WriteNodeAsync_CallSyncReader>d__104>(ref <WriteNodeAsync_CallSyncReader>d__);
			return <WriteNodeAsync_CallSyncReader>d__.<>t__builder.Task;
		}

		// Token: 0x06000DAA RID: 3498 RVA: 0x000587C8 File Offset: 0x000569C8
		internal Task WriteNodeAsync_CallAsyncReader(XmlReader reader, bool defattr)
		{
			XmlWriter.<WriteNodeAsync_CallAsyncReader>d__105 <WriteNodeAsync_CallAsyncReader>d__;
			<WriteNodeAsync_CallAsyncReader>d__.<>4__this = this;
			<WriteNodeAsync_CallAsyncReader>d__.reader = reader;
			<WriteNodeAsync_CallAsyncReader>d__.defattr = defattr;
			<WriteNodeAsync_CallAsyncReader>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteNodeAsync_CallAsyncReader>d__.<>1__state = -1;
			<WriteNodeAsync_CallAsyncReader>d__.<>t__builder.Start<XmlWriter.<WriteNodeAsync_CallAsyncReader>d__105>(ref <WriteNodeAsync_CallAsyncReader>d__);
			return <WriteNodeAsync_CallAsyncReader>d__.<>t__builder.Task;
		}

		/// <summary>Asynchronously copies everything from the <see cref="T:System.Xml.XPath.XPathNavigator" /> object to the writer. The position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> remains unchanged.</summary>
		/// <param name="navigator">The <see cref="T:System.Xml.XPath.XPathNavigator" /> to copy from.</param>
		/// <param name="defattr">
		///       <see langword="true" /> to copy the default attributes; otherwise, <see langword="false" />.</param>
		/// <returns>The task that represents the asynchronous <see langword="WriteNode" /> operation.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> asynchronous method was called without setting the <see cref="P:System.Xml.XmlWriterSettings.Async" /> flag to <see langword="true" />. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “Set XmlWriterSettings.Async to true if you want to use Async Methods.”</exception>
		// Token: 0x06000DAB RID: 3499 RVA: 0x0005881C File Offset: 0x00056A1C
		public virtual Task WriteNodeAsync(XPathNavigator navigator, bool defattr)
		{
			XmlWriter.<WriteNodeAsync>d__106 <WriteNodeAsync>d__;
			<WriteNodeAsync>d__.<>4__this = this;
			<WriteNodeAsync>d__.navigator = navigator;
			<WriteNodeAsync>d__.defattr = defattr;
			<WriteNodeAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteNodeAsync>d__.<>1__state = -1;
			<WriteNodeAsync>d__.<>t__builder.Start<XmlWriter.<WriteNodeAsync>d__106>(ref <WriteNodeAsync>d__);
			return <WriteNodeAsync>d__.<>t__builder.Task;
		}

		/// <summary>Asynchronously writes an element with the specified prefix, local name, namespace URI, and value.</summary>
		/// <param name="prefix">The prefix of the element.</param>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="ns">The namespace URI of the element.</param>
		/// <param name="value">The value of the element.</param>
		/// <returns>The task that represents the asynchronous <see langword="WriteElementString" /> operation.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter" /> asynchronous method was called without setting the <see cref="P:System.Xml.XmlWriterSettings.Async" /> flag to <see langword="true" />. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “Set XmlWriterSettings.Async to true if you want to use Async Methods.”</exception>
		// Token: 0x06000DAC RID: 3500 RVA: 0x00058870 File Offset: 0x00056A70
		public Task WriteElementStringAsync(string prefix, string localName, string ns, string value)
		{
			XmlWriter.<WriteElementStringAsync>d__107 <WriteElementStringAsync>d__;
			<WriteElementStringAsync>d__.<>4__this = this;
			<WriteElementStringAsync>d__.prefix = prefix;
			<WriteElementStringAsync>d__.localName = localName;
			<WriteElementStringAsync>d__.ns = ns;
			<WriteElementStringAsync>d__.value = value;
			<WriteElementStringAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteElementStringAsync>d__.<>1__state = -1;
			<WriteElementStringAsync>d__.<>t__builder.Start<XmlWriter.<WriteElementStringAsync>d__107>(ref <WriteElementStringAsync>d__);
			return <WriteElementStringAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000DAD RID: 3501 RVA: 0x000588D4 File Offset: 0x00056AD4
		private Task WriteLocalNamespacesAsync(XPathNavigator nsNav)
		{
			XmlWriter.<WriteLocalNamespacesAsync>d__108 <WriteLocalNamespacesAsync>d__;
			<WriteLocalNamespacesAsync>d__.<>4__this = this;
			<WriteLocalNamespacesAsync>d__.nsNav = nsNav;
			<WriteLocalNamespacesAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteLocalNamespacesAsync>d__.<>1__state = -1;
			<WriteLocalNamespacesAsync>d__.<>t__builder.Start<XmlWriter.<WriteLocalNamespacesAsync>d__108>(ref <WriteLocalNamespacesAsync>d__);
			return <WriteLocalNamespacesAsync>d__.<>t__builder.Task;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlWriter" /> class.</summary>
		// Token: 0x06000DAE RID: 3502 RVA: 0x0000216B File Offset: 0x0000036B
		protected XmlWriter()
		{
		}

		// Token: 0x04000EAC RID: 3756
		private char[] writeNodeBuffer;

		// Token: 0x04000EAD RID: 3757
		private const int WriteNodeBufferSize = 1024;

		// Token: 0x0200017A RID: 378
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteAttributeStringAsyncHelper>d__82 : IAsyncStateMachine
		{
			// Token: 0x06000DAF RID: 3503 RVA: 0x00058920 File Offset: 0x00056B20
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlWriter xmlWriter = this.<>4__this;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					switch (num)
					{
					case 0:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						break;
					case 1:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_E7;
					case 2:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_148;
					default:
						awaiter = this.task.ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWriter.<WriteAttributeStringAsyncHelper>d__82>(ref awaiter, ref this);
							return;
						}
						break;
					}
					awaiter.GetResult();
					awaiter = xmlWriter.WriteStringAsync(this.value).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWriter.<WriteAttributeStringAsyncHelper>d__82>(ref awaiter, ref this);
						return;
					}
					IL_E7:
					awaiter.GetResult();
					awaiter = xmlWriter.WriteEndAttributeAsync().ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 2;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWriter.<WriteAttributeStringAsyncHelper>d__82>(ref awaiter, ref this);
						return;
					}
					IL_148:
					awaiter.GetResult();
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<>t__builder.SetResult();
			}

			// Token: 0x06000DB0 RID: 3504 RVA: 0x00058AC8 File Offset: 0x00056CC8
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000EAE RID: 3758
			public int <>1__state;

			// Token: 0x04000EAF RID: 3759
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000EB0 RID: 3760
			public Task task;

			// Token: 0x04000EB1 RID: 3761
			public XmlWriter <>4__this;

			// Token: 0x04000EB2 RID: 3762
			public string value;

			// Token: 0x04000EB3 RID: 3763
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x0200017B RID: 379
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteQualifiedNameAsync>d__101 : IAsyncStateMachine
		{
			// Token: 0x06000DB1 RID: 3505 RVA: 0x00058AD8 File Offset: 0x00056CD8
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlWriter xmlWriter = this.<>4__this;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					switch (num)
					{
					case 0:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						break;
					case 1:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_134;
					case 2:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_19C;
					default:
					{
						if (this.ns == null || this.ns.Length <= 0)
						{
							goto IL_13B;
						}
						string text = xmlWriter.LookupPrefix(this.ns);
						if (text == null)
						{
							throw new ArgumentException(Res.GetString("The '{0}' namespace is not defined.", new object[]
							{
								this.ns
							}));
						}
						awaiter = xmlWriter.WriteStringAsync(text).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWriter.<WriteQualifiedNameAsync>d__101>(ref awaiter, ref this);
							return;
						}
						break;
					}
					}
					awaiter.GetResult();
					awaiter = xmlWriter.WriteStringAsync(":").ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWriter.<WriteQualifiedNameAsync>d__101>(ref awaiter, ref this);
						return;
					}
					IL_134:
					awaiter.GetResult();
					IL_13B:
					awaiter = xmlWriter.WriteStringAsync(this.localName).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 2;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWriter.<WriteQualifiedNameAsync>d__101>(ref awaiter, ref this);
						return;
					}
					IL_19C:
					awaiter.GetResult();
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<>t__builder.SetResult();
			}

			// Token: 0x06000DB2 RID: 3506 RVA: 0x00058CD4 File Offset: 0x00056ED4
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000EB4 RID: 3764
			public int <>1__state;

			// Token: 0x04000EB5 RID: 3765
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000EB6 RID: 3766
			public string ns;

			// Token: 0x04000EB7 RID: 3767
			public XmlWriter <>4__this;

			// Token: 0x04000EB8 RID: 3768
			public string localName;

			// Token: 0x04000EB9 RID: 3769
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x0200017C RID: 380
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteAttributesAsync>d__102 : IAsyncStateMachine
		{
			// Token: 0x06000DB3 RID: 3507 RVA: 0x00058CE4 File Offset: 0x00056EE4
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlWriter xmlWriter = this.<>4__this;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					switch (num)
					{
					case 0:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						break;
					case 1:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_1A0;
					case 2:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_222;
					case 3:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_293;
					case 4:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_304;
					default:
						if (this.reader == null)
						{
							throw new ArgumentNullException("reader");
						}
						if (this.reader.NodeType == XmlNodeType.Element || this.reader.NodeType == XmlNodeType.XmlDeclaration)
						{
							if (!this.reader.MoveToFirstAttribute())
							{
								goto IL_31B;
							}
							awaiter = xmlWriter.WriteAttributesAsync(this.reader, this.defattr).ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.<>1__state = 0;
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWriter.<WriteAttributesAsync>d__102>(ref awaiter, ref this);
								return;
							}
						}
						else
						{
							if (this.reader.NodeType != XmlNodeType.Attribute)
							{
								throw new XmlException("The current position on the Reader is neither an element nor an attribute.", string.Empty);
							}
							goto IL_10A;
						}
						break;
					}
					awaiter.GetResult();
					this.reader.MoveToElement();
					goto IL_31B;
					IL_10A:
					if (!this.defattr && this.reader.IsDefaultInternal)
					{
						goto IL_30B;
					}
					awaiter = xmlWriter.WriteStartAttributeAsync(this.reader.Prefix, this.reader.LocalName, this.reader.NamespaceURI).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWriter.<WriteAttributesAsync>d__102>(ref awaiter, ref this);
						return;
					}
					IL_1A0:
					awaiter.GetResult();
					goto IL_29A;
					IL_222:
					awaiter.GetResult();
					goto IL_29A;
					IL_293:
					awaiter.GetResult();
					IL_29A:
					if (!this.reader.ReadAttributeValue())
					{
						awaiter = xmlWriter.WriteEndAttributeAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 4;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWriter.<WriteAttributesAsync>d__102>(ref awaiter, ref this);
							return;
						}
					}
					else if (this.reader.NodeType == XmlNodeType.EntityReference)
					{
						awaiter = xmlWriter.WriteEntityRefAsync(this.reader.Name).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 2;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWriter.<WriteAttributesAsync>d__102>(ref awaiter, ref this);
							return;
						}
						goto IL_222;
					}
					else
					{
						awaiter = xmlWriter.WriteStringAsync(this.reader.Value).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 3;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWriter.<WriteAttributesAsync>d__102>(ref awaiter, ref this);
							return;
						}
						goto IL_293;
					}
					IL_304:
					awaiter.GetResult();
					IL_30B:
					if (this.reader.MoveToNextAttribute())
					{
						goto IL_10A;
					}
					IL_31B:;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<>t__builder.SetResult();
			}

			// Token: 0x06000DB4 RID: 3508 RVA: 0x00059058 File Offset: 0x00057258
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000EBA RID: 3770
			public int <>1__state;

			// Token: 0x04000EBB RID: 3771
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000EBC RID: 3772
			public XmlReader reader;

			// Token: 0x04000EBD RID: 3773
			public XmlWriter <>4__this;

			// Token: 0x04000EBE RID: 3774
			public bool defattr;

			// Token: 0x04000EBF RID: 3775
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x0200017D RID: 381
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteNodeAsync_CallSyncReader>d__104 : IAsyncStateMachine
		{
			// Token: 0x06000DB5 RID: 3509 RVA: 0x00059068 File Offset: 0x00057268
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlWriter xmlWriter = this.<>4__this;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					switch (num)
					{
					case 0:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_152;
					case 1:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_1C3;
					case 2:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_238;
					case 3:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_2D3;
					case 4:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_368;
					case 5:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_3DD;
					case 6:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_452;
					case 7:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_4C7;
					case 8:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_547;
					case 9:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_5E8;
					case 10:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_65E;
					case 11:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_6C6;
					default:
						this.<canReadChunk>5__2 = this.reader.CanReadValueChunk;
						this.<d>5__3 = ((this.reader.NodeType == XmlNodeType.None) ? -1 : this.reader.Depth);
						break;
					}
					IL_76:
					switch (this.reader.NodeType)
					{
					case XmlNodeType.Element:
						awaiter = xmlWriter.WriteStartElementAsync(this.reader.Prefix, this.reader.LocalName, this.reader.NamespaceURI).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWriter.<WriteNodeAsync_CallSyncReader>d__104>(ref awaiter, ref this);
							return;
						}
						break;
					case XmlNodeType.Attribute:
					case XmlNodeType.Entity:
					case XmlNodeType.Document:
					case XmlNodeType.DocumentFragment:
					case XmlNodeType.Notation:
					case XmlNodeType.EndEntity:
						goto IL_6CD;
					case XmlNodeType.Text:
						if (this.<canReadChunk>5__2)
						{
							if (xmlWriter.writeNodeBuffer == null)
							{
								xmlWriter.writeNodeBuffer = new char[1024];
								goto IL_2DA;
							}
							goto IL_2DA;
						}
						else
						{
							awaiter = xmlWriter.WriteStringAsync(this.reader.Value).ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.<>1__state = 4;
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWriter.<WriteNodeAsync_CallSyncReader>d__104>(ref awaiter, ref this);
								return;
							}
							goto IL_368;
						}
						break;
					case XmlNodeType.CDATA:
						awaiter = xmlWriter.WriteCDataAsync(this.reader.Value).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 6;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWriter.<WriteNodeAsync_CallSyncReader>d__104>(ref awaiter, ref this);
							return;
						}
						goto IL_452;
					case XmlNodeType.EntityReference:
						awaiter = xmlWriter.WriteEntityRefAsync(this.reader.Name).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 7;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWriter.<WriteNodeAsync_CallSyncReader>d__104>(ref awaiter, ref this);
							return;
						}
						goto IL_4C7;
					case XmlNodeType.ProcessingInstruction:
					case XmlNodeType.XmlDeclaration:
						awaiter = xmlWriter.WriteProcessingInstructionAsync(this.reader.Name, this.reader.Value).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 8;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWriter.<WriteNodeAsync_CallSyncReader>d__104>(ref awaiter, ref this);
							return;
						}
						goto IL_547;
					case XmlNodeType.Comment:
						awaiter = xmlWriter.WriteCommentAsync(this.reader.Value).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 10;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWriter.<WriteNodeAsync_CallSyncReader>d__104>(ref awaiter, ref this);
							return;
						}
						goto IL_65E;
					case XmlNodeType.DocumentType:
						awaiter = xmlWriter.WriteDocTypeAsync(this.reader.Name, this.reader.GetAttribute("PUBLIC"), this.reader.GetAttribute("SYSTEM"), this.reader.Value).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 9;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWriter.<WriteNodeAsync_CallSyncReader>d__104>(ref awaiter, ref this);
							return;
						}
						goto IL_5E8;
					case XmlNodeType.Whitespace:
					case XmlNodeType.SignificantWhitespace:
						awaiter = xmlWriter.WriteWhitespaceAsync(this.reader.Value).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 5;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWriter.<WriteNodeAsync_CallSyncReader>d__104>(ref awaiter, ref this);
							return;
						}
						goto IL_3DD;
					case XmlNodeType.EndElement:
						awaiter = xmlWriter.WriteFullEndElementAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 11;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWriter.<WriteNodeAsync_CallSyncReader>d__104>(ref awaiter, ref this);
							return;
						}
						goto IL_6C6;
					default:
						goto IL_6CD;
					}
					IL_152:
					awaiter.GetResult();
					awaiter = xmlWriter.WriteAttributesAsync(this.reader, this.defattr).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWriter.<WriteNodeAsync_CallSyncReader>d__104>(ref awaiter, ref this);
						return;
					}
					IL_1C3:
					awaiter.GetResult();
					if (!this.reader.IsEmptyElement)
					{
						goto IL_6CD;
					}
					awaiter = xmlWriter.WriteEndElementAsync().ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 2;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWriter.<WriteNodeAsync_CallSyncReader>d__104>(ref awaiter, ref this);
						return;
					}
					IL_238:
					awaiter.GetResult();
					goto IL_6CD;
					IL_2D3:
					awaiter.GetResult();
					IL_2DA:
					int count;
					if ((count = this.reader.ReadValueChunk(xmlWriter.writeNodeBuffer, 0, 1024)) <= 0)
					{
						goto IL_6CD;
					}
					awaiter = xmlWriter.WriteCharsAsync(xmlWriter.writeNodeBuffer, 0, count).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 3;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWriter.<WriteNodeAsync_CallSyncReader>d__104>(ref awaiter, ref this);
						return;
					}
					goto IL_2D3;
					IL_368:
					awaiter.GetResult();
					goto IL_6CD;
					IL_3DD:
					awaiter.GetResult();
					goto IL_6CD;
					IL_452:
					awaiter.GetResult();
					goto IL_6CD;
					IL_4C7:
					awaiter.GetResult();
					goto IL_6CD;
					IL_547:
					awaiter.GetResult();
					goto IL_6CD;
					IL_5E8:
					awaiter.GetResult();
					goto IL_6CD;
					IL_65E:
					awaiter.GetResult();
					goto IL_6CD;
					IL_6C6:
					awaiter.GetResult();
					IL_6CD:
					if (this.reader.Read() && (this.<d>5__3 < this.reader.Depth || (this.<d>5__3 == this.reader.Depth && this.reader.NodeType == XmlNodeType.EndElement)))
					{
						goto IL_76;
					}
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<>t__builder.SetResult();
			}

			// Token: 0x06000DB6 RID: 3510 RVA: 0x000597D4 File Offset: 0x000579D4
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000EC0 RID: 3776
			public int <>1__state;

			// Token: 0x04000EC1 RID: 3777
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000EC2 RID: 3778
			public XmlReader reader;

			// Token: 0x04000EC3 RID: 3779
			public XmlWriter <>4__this;

			// Token: 0x04000EC4 RID: 3780
			public bool defattr;

			// Token: 0x04000EC5 RID: 3781
			private bool <canReadChunk>5__2;

			// Token: 0x04000EC6 RID: 3782
			private int <d>5__3;

			// Token: 0x04000EC7 RID: 3783
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x0200017E RID: 382
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteNodeAsync_CallAsyncReader>d__105 : IAsyncStateMachine
		{
			// Token: 0x06000DB7 RID: 3511 RVA: 0x000597E4 File Offset: 0x000579E4
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlWriter xmlWriter = this.<>4__this;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter awaiter2;
					ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter awaiter3;
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter4;
					switch (num)
					{
					case 0:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_162;
					case 1:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_1D3;
					case 2:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_248;
					case 3:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_2E3;
					case 4:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_35C;
					case 5:
						awaiter3 = this.<>u__3;
						this.<>u__3 = default(ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_3D7;
					case 6:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_440;
					case 7:
						awaiter3 = this.<>u__3;
						this.<>u__3 = default(ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_4B2;
					case 8:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_51B;
					case 9:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_591;
					case 10:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_607;
					case 11:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_688;
					case 12:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_729;
					case 13:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_79F;
					case 14:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_807;
					case 15:
						awaiter4 = this.<>u__4;
						this.<>u__4 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_875;
					default:
						this.<canReadChunk>5__2 = this.reader.CanReadValueChunk;
						this.<d>5__3 = ((this.reader.NodeType == XmlNodeType.None) ? -1 : this.reader.Depth);
						break;
					}
					IL_86:
					switch (this.reader.NodeType)
					{
					case XmlNodeType.Element:
						awaiter = xmlWriter.WriteStartElementAsync(this.reader.Prefix, this.reader.LocalName, this.reader.NamespaceURI).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWriter.<WriteNodeAsync_CallAsyncReader>d__105>(ref awaiter, ref this);
							return;
						}
						break;
					case XmlNodeType.Attribute:
					case XmlNodeType.Entity:
					case XmlNodeType.Document:
					case XmlNodeType.DocumentFragment:
					case XmlNodeType.Notation:
					case XmlNodeType.EndEntity:
						goto IL_80E;
					case XmlNodeType.Text:
						if (this.<canReadChunk>5__2)
						{
							if (xmlWriter.writeNodeBuffer == null)
							{
								xmlWriter.writeNodeBuffer = new char[1024];
								goto IL_2EA;
							}
							goto IL_2EA;
						}
						else
						{
							awaiter3 = this.reader.GetValueAsync().ConfigureAwait(false).GetAwaiter();
							if (!awaiter3.IsCompleted)
							{
								this.<>1__state = 5;
								this.<>u__3 = awaiter3;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter, XmlWriter.<WriteNodeAsync_CallAsyncReader>d__105>(ref awaiter3, ref this);
								return;
							}
							goto IL_3D7;
						}
						break;
					case XmlNodeType.CDATA:
						awaiter = xmlWriter.WriteCDataAsync(this.reader.Value).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 9;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWriter.<WriteNodeAsync_CallAsyncReader>d__105>(ref awaiter, ref this);
							return;
						}
						goto IL_591;
					case XmlNodeType.EntityReference:
						awaiter = xmlWriter.WriteEntityRefAsync(this.reader.Name).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 10;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWriter.<WriteNodeAsync_CallAsyncReader>d__105>(ref awaiter, ref this);
							return;
						}
						goto IL_607;
					case XmlNodeType.ProcessingInstruction:
					case XmlNodeType.XmlDeclaration:
						awaiter = xmlWriter.WriteProcessingInstructionAsync(this.reader.Name, this.reader.Value).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 11;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWriter.<WriteNodeAsync_CallAsyncReader>d__105>(ref awaiter, ref this);
							return;
						}
						goto IL_688;
					case XmlNodeType.Comment:
						awaiter = xmlWriter.WriteCommentAsync(this.reader.Value).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 13;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWriter.<WriteNodeAsync_CallAsyncReader>d__105>(ref awaiter, ref this);
							return;
						}
						goto IL_79F;
					case XmlNodeType.DocumentType:
						awaiter = xmlWriter.WriteDocTypeAsync(this.reader.Name, this.reader.GetAttribute("PUBLIC"), this.reader.GetAttribute("SYSTEM"), this.reader.Value).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 12;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWriter.<WriteNodeAsync_CallAsyncReader>d__105>(ref awaiter, ref this);
							return;
						}
						goto IL_729;
					case XmlNodeType.Whitespace:
					case XmlNodeType.SignificantWhitespace:
						awaiter3 = this.reader.GetValueAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter3.IsCompleted)
						{
							this.<>1__state = 7;
							this.<>u__3 = awaiter3;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter, XmlWriter.<WriteNodeAsync_CallAsyncReader>d__105>(ref awaiter3, ref this);
							return;
						}
						goto IL_4B2;
					case XmlNodeType.EndElement:
						awaiter = xmlWriter.WriteFullEndElementAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 14;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWriter.<WriteNodeAsync_CallAsyncReader>d__105>(ref awaiter, ref this);
							return;
						}
						goto IL_807;
					default:
						goto IL_80E;
					}
					IL_162:
					awaiter.GetResult();
					awaiter = xmlWriter.WriteAttributesAsync(this.reader, this.defattr).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWriter.<WriteNodeAsync_CallAsyncReader>d__105>(ref awaiter, ref this);
						return;
					}
					IL_1D3:
					awaiter.GetResult();
					if (!this.reader.IsEmptyElement)
					{
						goto IL_80E;
					}
					awaiter = xmlWriter.WriteEndElementAsync().ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 2;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWriter.<WriteNodeAsync_CallAsyncReader>d__105>(ref awaiter, ref this);
						return;
					}
					IL_248:
					awaiter.GetResult();
					goto IL_80E;
					IL_2E3:
					awaiter.GetResult();
					IL_2EA:
					awaiter2 = this.reader.ReadValueChunkAsync(xmlWriter.writeNodeBuffer, 0, 1024).ConfigureAwait(false).GetAwaiter();
					if (!awaiter2.IsCompleted)
					{
						this.<>1__state = 4;
						this.<>u__2 = awaiter2;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, XmlWriter.<WriteNodeAsync_CallAsyncReader>d__105>(ref awaiter2, ref this);
						return;
					}
					IL_35C:
					int result;
					if ((result = awaiter2.GetResult()) <= 0)
					{
						goto IL_80E;
					}
					awaiter = xmlWriter.WriteCharsAsync(xmlWriter.writeNodeBuffer, 0, result).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 3;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWriter.<WriteNodeAsync_CallAsyncReader>d__105>(ref awaiter, ref this);
						return;
					}
					goto IL_2E3;
					IL_3D7:
					string result2 = awaiter3.GetResult();
					awaiter = xmlWriter.WriteStringAsync(result2).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 6;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWriter.<WriteNodeAsync_CallAsyncReader>d__105>(ref awaiter, ref this);
						return;
					}
					IL_440:
					awaiter.GetResult();
					goto IL_80E;
					IL_4B2:
					result2 = awaiter3.GetResult();
					awaiter = xmlWriter.WriteWhitespaceAsync(result2).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 8;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWriter.<WriteNodeAsync_CallAsyncReader>d__105>(ref awaiter, ref this);
						return;
					}
					IL_51B:
					awaiter.GetResult();
					goto IL_80E;
					IL_591:
					awaiter.GetResult();
					goto IL_80E;
					IL_607:
					awaiter.GetResult();
					goto IL_80E;
					IL_688:
					awaiter.GetResult();
					goto IL_80E;
					IL_729:
					awaiter.GetResult();
					goto IL_80E;
					IL_79F:
					awaiter.GetResult();
					goto IL_80E;
					IL_807:
					awaiter.GetResult();
					IL_80E:
					awaiter4 = this.reader.ReadAsync().ConfigureAwait(false).GetAwaiter();
					if (!awaiter4.IsCompleted)
					{
						this.<>1__state = 15;
						this.<>u__4 = awaiter4;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, XmlWriter.<WriteNodeAsync_CallAsyncReader>d__105>(ref awaiter4, ref this);
						return;
					}
					IL_875:
					if (awaiter4.GetResult() && (this.<d>5__3 < this.reader.Depth || (this.<d>5__3 == this.reader.Depth && this.reader.NodeType == XmlNodeType.EndElement)))
					{
						goto IL_86;
					}
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<>t__builder.SetResult();
			}

			// Token: 0x06000DB8 RID: 3512 RVA: 0x0005A0F4 File Offset: 0x000582F4
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000EC8 RID: 3784
			public int <>1__state;

			// Token: 0x04000EC9 RID: 3785
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000ECA RID: 3786
			public XmlReader reader;

			// Token: 0x04000ECB RID: 3787
			public XmlWriter <>4__this;

			// Token: 0x04000ECC RID: 3788
			public bool defattr;

			// Token: 0x04000ECD RID: 3789
			private bool <canReadChunk>5__2;

			// Token: 0x04000ECE RID: 3790
			private int <d>5__3;

			// Token: 0x04000ECF RID: 3791
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x04000ED0 RID: 3792
			private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter <>u__2;

			// Token: 0x04000ED1 RID: 3793
			private ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter <>u__3;

			// Token: 0x04000ED2 RID: 3794
			private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter <>u__4;
		}

		// Token: 0x0200017F RID: 383
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteNodeAsync>d__106 : IAsyncStateMachine
		{
			// Token: 0x06000DB9 RID: 3513 RVA: 0x0005A104 File Offset: 0x00058304
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlWriter xmlWriter = this.<>4__this;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					switch (num)
					{
					case 0:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_130;
					case 1:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_1EB;
					case 2:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_25B;
					case 3:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_2C0;
					case 4:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_355;
					case 5:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_3DD;
					case 6:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_452;
					case 7:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_4D3;
					case 8:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_550;
					case 9:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_603;
					case 10:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_66B;
					case 11:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_717;
					default:
						if (this.navigator == null)
						{
							throw new ArgumentNullException("navigator");
						}
						this.<iLevel>5__2 = 0;
						this.navigator = this.navigator.Clone();
						goto IL_6F;
					}
					IL_672:
					while (this.<iLevel>5__2 != 0)
					{
						if (this.navigator.MoveToNext())
						{
							goto IL_6F;
						}
						int num2 = this.<iLevel>5__2;
						this.<iLevel>5__2 = num2 - 1;
						this.navigator.MoveToParent();
						if (this.navigator.NodeType == XPathNodeType.Element)
						{
							awaiter = xmlWriter.WriteFullEndElementAsync().ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.<>1__state = 11;
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWriter.<WriteNodeAsync>d__106>(ref awaiter, ref this);
								return;
							}
							goto IL_717;
						}
					}
					goto IL_73C;
					IL_6F:
					this.<mayHaveChildren>5__3 = false;
					switch (this.navigator.NodeType)
					{
					case XPathNodeType.Root:
						this.<mayHaveChildren>5__3 = true;
						goto IL_557;
					case XPathNodeType.Element:
						awaiter = xmlWriter.WriteStartElementAsync(this.navigator.Prefix, this.navigator.LocalName, this.navigator.NamespaceURI).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWriter.<WriteNodeAsync>d__106>(ref awaiter, ref this);
							return;
						}
						break;
					case XPathNodeType.Attribute:
					case XPathNodeType.Namespace:
						goto IL_557;
					case XPathNodeType.Text:
						awaiter = xmlWriter.WriteStringAsync(this.navigator.Value).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 5;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWriter.<WriteNodeAsync>d__106>(ref awaiter, ref this);
							return;
						}
						goto IL_3DD;
					case XPathNodeType.SignificantWhitespace:
					case XPathNodeType.Whitespace:
						awaiter = xmlWriter.WriteWhitespaceAsync(this.navigator.Value).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 6;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWriter.<WriteNodeAsync>d__106>(ref awaiter, ref this);
							return;
						}
						goto IL_452;
					case XPathNodeType.ProcessingInstruction:
						awaiter = xmlWriter.WriteProcessingInstructionAsync(this.navigator.LocalName, this.navigator.Value).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 8;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWriter.<WriteNodeAsync>d__106>(ref awaiter, ref this);
							return;
						}
						goto IL_550;
					case XPathNodeType.Comment:
						awaiter = xmlWriter.WriteCommentAsync(this.navigator.Value).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 7;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWriter.<WriteNodeAsync>d__106>(ref awaiter, ref this);
							return;
						}
						goto IL_4D3;
					default:
						goto IL_557;
					}
					IL_130:
					awaiter.GetResult();
					if (!this.navigator.MoveToFirstAttribute())
					{
						goto IL_2E3;
					}
					IL_147:
					IXmlSchemaInfo schemaInfo = this.navigator.SchemaInfo;
					if (!this.defattr && schemaInfo != null && schemaInfo.IsDefault)
					{
						goto IL_2C7;
					}
					awaiter = xmlWriter.WriteStartAttributeAsync(this.navigator.Prefix, this.navigator.LocalName, this.navigator.NamespaceURI).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWriter.<WriteNodeAsync>d__106>(ref awaiter, ref this);
						return;
					}
					IL_1EB:
					awaiter.GetResult();
					awaiter = xmlWriter.WriteStringAsync(this.navigator.Value).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 2;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWriter.<WriteNodeAsync>d__106>(ref awaiter, ref this);
						return;
					}
					IL_25B:
					awaiter.GetResult();
					awaiter = xmlWriter.WriteEndAttributeAsync().ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 3;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWriter.<WriteNodeAsync>d__106>(ref awaiter, ref this);
						return;
					}
					IL_2C0:
					awaiter.GetResult();
					IL_2C7:
					if (this.navigator.MoveToNextAttribute())
					{
						goto IL_147;
					}
					this.navigator.MoveToParent();
					IL_2E3:
					if (!this.navigator.MoveToFirstNamespace(XPathNamespaceScope.Local))
					{
						goto IL_368;
					}
					awaiter = xmlWriter.WriteLocalNamespacesAsync(this.navigator).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 4;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWriter.<WriteNodeAsync>d__106>(ref awaiter, ref this);
						return;
					}
					IL_355:
					awaiter.GetResult();
					this.navigator.MoveToParent();
					IL_368:
					this.<mayHaveChildren>5__3 = true;
					goto IL_557;
					IL_3DD:
					awaiter.GetResult();
					goto IL_557;
					IL_452:
					awaiter.GetResult();
					goto IL_557;
					IL_4D3:
					awaiter.GetResult();
					goto IL_557;
					IL_550:
					awaiter.GetResult();
					IL_557:
					if (!this.<mayHaveChildren>5__3)
					{
						goto IL_672;
					}
					if (this.navigator.MoveToFirstChild())
					{
						int num2 = this.<iLevel>5__2;
						this.<iLevel>5__2 = num2 + 1;
						goto IL_6F;
					}
					if (this.navigator.NodeType != XPathNodeType.Element)
					{
						goto IL_672;
					}
					if (this.navigator.IsEmptyElement)
					{
						awaiter = xmlWriter.WriteEndElementAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 9;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWriter.<WriteNodeAsync>d__106>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = xmlWriter.WriteFullEndElementAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 10;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWriter.<WriteNodeAsync>d__106>(ref awaiter, ref this);
							return;
						}
						goto IL_66B;
					}
					IL_603:
					awaiter.GetResult();
					goto IL_672;
					IL_66B:
					awaiter.GetResult();
					goto IL_672;
					IL_717:
					awaiter.GetResult();
					goto IL_672;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_73C:
				this.<>1__state = -2;
				this.<>t__builder.SetResult();
			}

			// Token: 0x06000DBA RID: 3514 RVA: 0x0005A87C File Offset: 0x00058A7C
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000ED3 RID: 3795
			public int <>1__state;

			// Token: 0x04000ED4 RID: 3796
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000ED5 RID: 3797
			public XPathNavigator navigator;

			// Token: 0x04000ED6 RID: 3798
			public XmlWriter <>4__this;

			// Token: 0x04000ED7 RID: 3799
			public bool defattr;

			// Token: 0x04000ED8 RID: 3800
			private int <iLevel>5__2;

			// Token: 0x04000ED9 RID: 3801
			private bool <mayHaveChildren>5__3;

			// Token: 0x04000EDA RID: 3802
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000180 RID: 384
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteElementStringAsync>d__107 : IAsyncStateMachine
		{
			// Token: 0x06000DBB RID: 3515 RVA: 0x0005A88C File Offset: 0x00058A8C
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlWriter xmlWriter = this.<>4__this;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					switch (num)
					{
					case 0:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						break;
					case 1:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_10E;
					case 2:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_16F;
					default:
						awaiter = xmlWriter.WriteStartElementAsync(this.prefix, this.localName, this.ns).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWriter.<WriteElementStringAsync>d__107>(ref awaiter, ref this);
							return;
						}
						break;
					}
					awaiter.GetResult();
					if (this.value == null || this.value.Length == 0)
					{
						goto IL_115;
					}
					awaiter = xmlWriter.WriteStringAsync(this.value).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWriter.<WriteElementStringAsync>d__107>(ref awaiter, ref this);
						return;
					}
					IL_10E:
					awaiter.GetResult();
					IL_115:
					awaiter = xmlWriter.WriteEndElementAsync().ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 2;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWriter.<WriteElementStringAsync>d__107>(ref awaiter, ref this);
						return;
					}
					IL_16F:
					awaiter.GetResult();
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<>t__builder.SetResult();
			}

			// Token: 0x06000DBC RID: 3516 RVA: 0x0005AA5C File Offset: 0x00058C5C
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000EDB RID: 3803
			public int <>1__state;

			// Token: 0x04000EDC RID: 3804
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000EDD RID: 3805
			public XmlWriter <>4__this;

			// Token: 0x04000EDE RID: 3806
			public string prefix;

			// Token: 0x04000EDF RID: 3807
			public string localName;

			// Token: 0x04000EE0 RID: 3808
			public string ns;

			// Token: 0x04000EE1 RID: 3809
			public string value;

			// Token: 0x04000EE2 RID: 3810
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000181 RID: 385
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteLocalNamespacesAsync>d__108 : IAsyncStateMachine
		{
			// Token: 0x06000DBD RID: 3517 RVA: 0x0005AA6C File Offset: 0x00058C6C
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlWriter xmlWriter = this.<>4__this;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					switch (num)
					{
					case 0:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						break;
					case 1:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_139;
					case 2:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_1B2;
					default:
						this.<prefix>5__2 = this.nsNav.LocalName;
						this.<ns>5__3 = this.nsNav.Value;
						if (!this.nsNav.MoveToNextNamespace(XPathNamespaceScope.Local))
						{
							goto IL_BA;
						}
						awaiter = xmlWriter.WriteLocalNamespacesAsync(this.nsNav).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWriter.<WriteLocalNamespacesAsync>d__108>(ref awaiter, ref this);
							return;
						}
						break;
					}
					awaiter.GetResult();
					IL_BA:
					if (this.<prefix>5__2.Length == 0)
					{
						awaiter = xmlWriter.WriteAttributeStringAsync(string.Empty, "xmlns", "http://www.w3.org/2000/xmlns/", this.<ns>5__3).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 1;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWriter.<WriteLocalNamespacesAsync>d__108>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = xmlWriter.WriteAttributeStringAsync("xmlns", this.<prefix>5__2, "http://www.w3.org/2000/xmlns/", this.<ns>5__3).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 2;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlWriter.<WriteLocalNamespacesAsync>d__108>(ref awaiter, ref this);
							return;
						}
						goto IL_1B2;
					}
					IL_139:
					awaiter.GetResult();
					goto IL_1B9;
					IL_1B2:
					awaiter.GetResult();
					IL_1B9:;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<prefix>5__2 = null;
					this.<ns>5__3 = null;
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<prefix>5__2 = null;
				this.<ns>5__3 = null;
				this.<>t__builder.SetResult();
			}

			// Token: 0x06000DBE RID: 3518 RVA: 0x0005AC98 File Offset: 0x00058E98
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000EE3 RID: 3811
			public int <>1__state;

			// Token: 0x04000EE4 RID: 3812
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000EE5 RID: 3813
			public XPathNavigator nsNav;

			// Token: 0x04000EE6 RID: 3814
			public XmlWriter <>4__this;

			// Token: 0x04000EE7 RID: 3815
			private string <prefix>5__2;

			// Token: 0x04000EE8 RID: 3816
			private string <ns>5__3;

			// Token: 0x04000EE9 RID: 3817
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}
	}
}
