using System;
using System.Collections;
using System.IO;
using System.Runtime.Serialization;
using System.Text;

namespace System.Xml
{
	// Token: 0x02000056 RID: 86
	internal sealed class XmlCanonicalWriter
	{
		// Token: 0x060003EF RID: 1007 RVA: 0x0000222F File Offset: 0x0000042F
		public XmlCanonicalWriter()
		{
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x0001392C File Offset: 0x00011B2C
		public void SetOutput(Stream stream, bool includeComments, string[] inclusivePrefixes)
		{
			if (stream == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("stream");
			}
			if (this.writer == null)
			{
				this.writer = new XmlUTF8NodeWriter(XmlCanonicalWriter.isEscapedAttributeChar, XmlCanonicalWriter.isEscapedElementChar);
			}
			this.writer.SetOutput(stream, false, null);
			if (this.elementStream == null)
			{
				this.elementStream = new MemoryStream();
			}
			if (this.elementWriter == null)
			{
				this.elementWriter = new XmlUTF8NodeWriter(XmlCanonicalWriter.isEscapedAttributeChar, XmlCanonicalWriter.isEscapedElementChar);
			}
			this.elementWriter.SetOutput(this.elementStream, false, null);
			if (this.xmlnsAttributes == null)
			{
				this.xmlnsAttributeCount = 0;
				this.xmlnsOffset = 0;
				this.WriteXmlnsAttribute("xml", "http://www.w3.org/XML/1998/namespace");
				this.WriteXmlnsAttribute("xmlns", "http://www.w3.org/2000/xmlns/");
				this.WriteXmlnsAttribute(string.Empty, string.Empty);
				this.xmlnsStartOffset = this.xmlnsOffset;
				for (int i = 0; i < 3; i++)
				{
					this.xmlnsAttributes[i].referred = true;
				}
			}
			else
			{
				this.xmlnsAttributeCount = 3;
				this.xmlnsOffset = this.xmlnsStartOffset;
			}
			this.depth = 0;
			this.inStartElement = false;
			this.includeComments = includeComments;
			this.inclusivePrefixes = null;
			if (inclusivePrefixes != null)
			{
				this.inclusivePrefixes = new string[inclusivePrefixes.Length];
				for (int j = 0; j < inclusivePrefixes.Length; j++)
				{
					if (inclusivePrefixes[j] == null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(System.Runtime.Serialization.SR.GetString("The inclusive namespace prefix collection cannot contain null as one of the items."));
					}
					this.inclusivePrefixes[j] = inclusivePrefixes[j];
				}
			}
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x00013A95 File Offset: 0x00011C95
		public void Flush()
		{
			this.ThrowIfClosed();
			this.writer.Flush();
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x00013AA8 File Offset: 0x00011CA8
		public void Close()
		{
			if (this.writer != null)
			{
				this.writer.Close();
			}
			if (this.elementWriter != null)
			{
				this.elementWriter.Close();
			}
			if (this.elementStream != null && this.elementStream.Length > 512L)
			{
				this.elementStream = null;
			}
			this.elementBuffer = null;
			if (this.scopes != null && this.scopes.Length > 16)
			{
				this.scopes = null;
			}
			if (this.attributes != null && this.attributes.Length > 16)
			{
				this.attributes = null;
			}
			if (this.xmlnsBuffer != null && this.xmlnsBuffer.Length > 1024)
			{
				this.xmlnsAttributes = null;
				this.xmlnsBuffer = null;
			}
			this.inclusivePrefixes = null;
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x0000A8EE File Offset: 0x00008AEE
		public void WriteDeclaration()
		{
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x00013B66 File Offset: 0x00011D66
		public void WriteComment(string value)
		{
			if (value == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
			}
			this.ThrowIfClosed();
			if (this.includeComments)
			{
				this.writer.WriteComment(value);
			}
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x00013B90 File Offset: 0x00011D90
		private void StartElement()
		{
			if (this.scopes == null)
			{
				this.scopes = new XmlCanonicalWriter.Scope[4];
			}
			else if (this.depth == this.scopes.Length)
			{
				XmlCanonicalWriter.Scope[] destinationArray = new XmlCanonicalWriter.Scope[this.depth * 2];
				Array.Copy(this.scopes, destinationArray, this.depth);
				this.scopes = destinationArray;
			}
			this.scopes[this.depth].xmlnsAttributeCount = this.xmlnsAttributeCount;
			this.scopes[this.depth].xmlnsOffset = this.xmlnsOffset;
			this.depth++;
			this.inStartElement = true;
			this.attributeCount = 0;
			this.elementStream.Position = 0L;
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x00013C4C File Offset: 0x00011E4C
		private void EndElement()
		{
			this.depth--;
			this.xmlnsAttributeCount = this.scopes[this.depth].xmlnsAttributeCount;
			this.xmlnsOffset = this.scopes[this.depth].xmlnsOffset;
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x00013CA0 File Offset: 0x00011EA0
		public void WriteStartElement(string prefix, string localName)
		{
			if (prefix == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("prefix");
			}
			if (localName == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("localName");
			}
			this.ThrowIfClosed();
			bool flag = this.depth == 0;
			this.StartElement();
			this.element.prefixOffset = this.elementWriter.Position + 1;
			this.element.prefixLength = Encoding.UTF8.GetByteCount(prefix);
			this.element.localNameOffset = this.element.prefixOffset + this.element.prefixLength + ((this.element.prefixLength != 0) ? 1 : 0);
			this.element.localNameLength = Encoding.UTF8.GetByteCount(localName);
			this.elementWriter.WriteStartElement(prefix, localName);
			if (flag && this.inclusivePrefixes != null)
			{
				for (int i = 0; i < this.scopes[0].xmlnsAttributeCount; i++)
				{
					if (this.IsInclusivePrefix(ref this.xmlnsAttributes[i]))
					{
						XmlCanonicalWriter.XmlnsAttribute xmlnsAttribute = this.xmlnsAttributes[i];
						this.AddXmlnsAttribute(ref xmlnsAttribute);
					}
				}
			}
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x00013DB4 File Offset: 0x00011FB4
		public void WriteStartElement(byte[] prefixBuffer, int prefixOffset, int prefixLength, byte[] localNameBuffer, int localNameOffset, int localNameLength)
		{
			if (prefixBuffer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("prefixBuffer"));
			}
			if (prefixOffset < 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("prefixOffset", System.Runtime.Serialization.SR.GetString("The value of this argument must be non-negative.")));
			}
			if (prefixOffset > prefixBuffer.Length)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("prefixOffset", System.Runtime.Serialization.SR.GetString("The specified offset exceeds the buffer size ({0} bytes).", new object[]
				{
					prefixBuffer.Length
				})));
			}
			if (prefixLength < 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("prefixLength", System.Runtime.Serialization.SR.GetString("The value of this argument must be non-negative.")));
			}
			if (prefixLength > prefixBuffer.Length - prefixOffset)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("prefixLength", System.Runtime.Serialization.SR.GetString("The specified size exceeds the remaining buffer space ({0} bytes).", new object[]
				{
					prefixBuffer.Length - prefixOffset
				})));
			}
			if (localNameBuffer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("localNameBuffer"));
			}
			if (localNameOffset < 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("localNameOffset", System.Runtime.Serialization.SR.GetString("The value of this argument must be non-negative.")));
			}
			if (localNameOffset > localNameBuffer.Length)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("localNameOffset", System.Runtime.Serialization.SR.GetString("The specified offset exceeds the buffer size ({0} bytes).", new object[]
				{
					localNameBuffer.Length
				})));
			}
			if (localNameLength < 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("localNameLength", System.Runtime.Serialization.SR.GetString("The value of this argument must be non-negative.")));
			}
			if (localNameLength > localNameBuffer.Length - localNameOffset)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("localNameLength", System.Runtime.Serialization.SR.GetString("The specified size exceeds the remaining buffer space ({0} bytes).", new object[]
				{
					localNameBuffer.Length - localNameOffset
				})));
			}
			this.ThrowIfClosed();
			bool flag = this.depth == 0;
			this.StartElement();
			this.element.prefixOffset = this.elementWriter.Position + 1;
			this.element.prefixLength = prefixLength;
			this.element.localNameOffset = this.element.prefixOffset + prefixLength + ((prefixLength != 0) ? 1 : 0);
			this.element.localNameLength = localNameLength;
			this.elementWriter.WriteStartElement(prefixBuffer, prefixOffset, prefixLength, localNameBuffer, localNameOffset, localNameLength);
			if (flag && this.inclusivePrefixes != null)
			{
				for (int i = 0; i < this.scopes[0].xmlnsAttributeCount; i++)
				{
					if (this.IsInclusivePrefix(ref this.xmlnsAttributes[i]))
					{
						XmlCanonicalWriter.XmlnsAttribute xmlnsAttribute = this.xmlnsAttributes[i];
						this.AddXmlnsAttribute(ref xmlnsAttribute);
					}
				}
			}
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x00014000 File Offset: 0x00012200
		private bool IsInclusivePrefix(ref XmlCanonicalWriter.XmlnsAttribute xmlnsAttribute)
		{
			for (int i = 0; i < this.inclusivePrefixes.Length; i++)
			{
				if (this.inclusivePrefixes[i].Length == xmlnsAttribute.prefixLength && string.Compare(Encoding.UTF8.GetString(this.xmlnsBuffer, xmlnsAttribute.prefixOffset, xmlnsAttribute.prefixLength), this.inclusivePrefixes[i], StringComparison.Ordinal) == 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x00014064 File Offset: 0x00012264
		public void WriteEndStartElement(bool isEmpty)
		{
			this.ThrowIfClosed();
			this.elementWriter.Flush();
			this.elementBuffer = this.elementStream.GetBuffer();
			this.inStartElement = false;
			this.ResolvePrefixes();
			this.writer.WriteStartElement(this.elementBuffer, this.element.prefixOffset, this.element.prefixLength, this.elementBuffer, this.element.localNameOffset, this.element.localNameLength);
			for (int i = this.scopes[this.depth - 1].xmlnsAttributeCount; i < this.xmlnsAttributeCount; i++)
			{
				int j = i - 1;
				bool flag = false;
				while (j >= 0)
				{
					if (this.Equals(this.xmlnsBuffer, this.xmlnsAttributes[i].prefixOffset, this.xmlnsAttributes[i].prefixLength, this.xmlnsBuffer, this.xmlnsAttributes[j].prefixOffset, this.xmlnsAttributes[j].prefixLength))
					{
						if (!this.Equals(this.xmlnsBuffer, this.xmlnsAttributes[i].nsOffset, this.xmlnsAttributes[i].nsLength, this.xmlnsBuffer, this.xmlnsAttributes[j].nsOffset, this.xmlnsAttributes[j].nsLength))
						{
							break;
						}
						if (this.xmlnsAttributes[j].referred)
						{
							flag = true;
							break;
						}
					}
					j--;
				}
				if (!flag)
				{
					this.WriteXmlnsAttribute(ref this.xmlnsAttributes[i]);
				}
			}
			if (this.attributeCount > 0)
			{
				if (this.attributeCount > 1)
				{
					this.SortAttributes();
				}
				for (int k = 0; k < this.attributeCount; k++)
				{
					this.writer.WriteText(this.elementBuffer, this.attributes[k].offset, this.attributes[k].length);
				}
			}
			this.writer.WriteEndStartElement(false);
			if (isEmpty)
			{
				this.writer.WriteEndElement(this.elementBuffer, this.element.prefixOffset, this.element.prefixLength, this.elementBuffer, this.element.localNameOffset, this.element.localNameLength);
				this.EndElement();
			}
			this.elementBuffer = null;
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x000142BE File Offset: 0x000124BE
		public void WriteEndElement(string prefix, string localName)
		{
			if (prefix == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("prefix");
			}
			if (localName == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("localName");
			}
			this.ThrowIfClosed();
			this.writer.WriteEndElement(prefix, localName);
			this.EndElement();
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x000142F8 File Offset: 0x000124F8
		private void EnsureXmlnsBuffer(int byteCount)
		{
			if (this.xmlnsBuffer == null)
			{
				this.xmlnsBuffer = new byte[Math.Max(byteCount, 128)];
				return;
			}
			if (this.xmlnsOffset + byteCount > this.xmlnsBuffer.Length)
			{
				byte[] dst = new byte[Math.Max(this.xmlnsOffset + byteCount, this.xmlnsBuffer.Length * 2)];
				Buffer.BlockCopy(this.xmlnsBuffer, 0, dst, 0, this.xmlnsOffset);
				this.xmlnsBuffer = dst;
			}
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x00014370 File Offset: 0x00012570
		public void WriteXmlnsAttribute(string prefix, string ns)
		{
			if (prefix == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("prefix");
			}
			if (ns == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("ns");
			}
			this.ThrowIfClosed();
			if (prefix.Length > 2147483647 - ns.Length)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("ns", System.Runtime.Serialization.SR.GetString("The combined length of the prefix and namespace must not be greater than {0}.", new object[]
				{
					715827882
				})));
			}
			int num = prefix.Length + ns.Length;
			if (num > 715827882)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("ns", System.Runtime.Serialization.SR.GetString("The combined length of the prefix and namespace must not be greater than {0}.", new object[]
				{
					715827882
				})));
			}
			this.EnsureXmlnsBuffer(num * 3);
			XmlCanonicalWriter.XmlnsAttribute xmlnsAttribute;
			xmlnsAttribute.prefixOffset = this.xmlnsOffset;
			xmlnsAttribute.prefixLength = Encoding.UTF8.GetBytes(prefix, 0, prefix.Length, this.xmlnsBuffer, this.xmlnsOffset);
			this.xmlnsOffset += xmlnsAttribute.prefixLength;
			xmlnsAttribute.nsOffset = this.xmlnsOffset;
			xmlnsAttribute.nsLength = Encoding.UTF8.GetBytes(ns, 0, ns.Length, this.xmlnsBuffer, this.xmlnsOffset);
			this.xmlnsOffset += xmlnsAttribute.nsLength;
			xmlnsAttribute.referred = false;
			this.AddXmlnsAttribute(ref xmlnsAttribute);
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x000144C8 File Offset: 0x000126C8
		public void WriteXmlnsAttribute(byte[] prefixBuffer, int prefixOffset, int prefixLength, byte[] nsBuffer, int nsOffset, int nsLength)
		{
			if (prefixBuffer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("prefixBuffer"));
			}
			if (prefixOffset < 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("prefixOffset", System.Runtime.Serialization.SR.GetString("The value of this argument must be non-negative.")));
			}
			if (prefixOffset > prefixBuffer.Length)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("prefixOffset", System.Runtime.Serialization.SR.GetString("The specified offset exceeds the buffer size ({0} bytes).", new object[]
				{
					prefixBuffer.Length
				})));
			}
			if (prefixLength < 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("prefixLength", System.Runtime.Serialization.SR.GetString("The value of this argument must be non-negative.")));
			}
			if (prefixLength > prefixBuffer.Length - prefixOffset)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("prefixLength", System.Runtime.Serialization.SR.GetString("The specified size exceeds the remaining buffer space ({0} bytes).", new object[]
				{
					prefixBuffer.Length - prefixOffset
				})));
			}
			if (nsBuffer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("nsBuffer"));
			}
			if (nsOffset < 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("nsOffset", System.Runtime.Serialization.SR.GetString("The value of this argument must be non-negative.")));
			}
			if (nsOffset > nsBuffer.Length)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("nsOffset", System.Runtime.Serialization.SR.GetString("The specified offset exceeds the buffer size ({0} bytes).", new object[]
				{
					nsBuffer.Length
				})));
			}
			if (nsLength < 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("nsLength", System.Runtime.Serialization.SR.GetString("The value of this argument must be non-negative.")));
			}
			if (nsLength > nsBuffer.Length - nsOffset)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("nsLength", System.Runtime.Serialization.SR.GetString("The specified size exceeds the remaining buffer space ({0} bytes).", new object[]
				{
					nsBuffer.Length - nsOffset
				})));
			}
			this.ThrowIfClosed();
			if (prefixLength > 2147483647 - nsLength)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("nsLength", System.Runtime.Serialization.SR.GetString("The combined length of the prefix and namespace must not be greater than {0}.", new object[]
				{
					int.MaxValue
				})));
			}
			this.EnsureXmlnsBuffer(prefixLength + nsLength);
			XmlCanonicalWriter.XmlnsAttribute xmlnsAttribute;
			xmlnsAttribute.prefixOffset = this.xmlnsOffset;
			xmlnsAttribute.prefixLength = prefixLength;
			Buffer.BlockCopy(prefixBuffer, prefixOffset, this.xmlnsBuffer, this.xmlnsOffset, prefixLength);
			this.xmlnsOffset += prefixLength;
			xmlnsAttribute.nsOffset = this.xmlnsOffset;
			xmlnsAttribute.nsLength = nsLength;
			Buffer.BlockCopy(nsBuffer, nsOffset, this.xmlnsBuffer, this.xmlnsOffset, nsLength);
			this.xmlnsOffset += nsLength;
			xmlnsAttribute.referred = false;
			this.AddXmlnsAttribute(ref xmlnsAttribute);
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x00014718 File Offset: 0x00012918
		public void WriteStartAttribute(string prefix, string localName)
		{
			if (prefix == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("prefix");
			}
			if (localName == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("localName");
			}
			this.ThrowIfClosed();
			this.attribute.offset = this.elementWriter.Position;
			this.attribute.length = 0;
			this.attribute.prefixOffset = this.attribute.offset + 1;
			this.attribute.prefixLength = Encoding.UTF8.GetByteCount(prefix);
			this.attribute.localNameOffset = this.attribute.prefixOffset + this.attribute.prefixLength + ((this.attribute.prefixLength != 0) ? 1 : 0);
			this.attribute.localNameLength = Encoding.UTF8.GetByteCount(localName);
			this.attribute.nsOffset = 0;
			this.attribute.nsLength = 0;
			this.elementWriter.WriteStartAttribute(prefix, localName);
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x00014808 File Offset: 0x00012A08
		public void WriteStartAttribute(byte[] prefixBuffer, int prefixOffset, int prefixLength, byte[] localNameBuffer, int localNameOffset, int localNameLength)
		{
			if (prefixBuffer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("prefixBuffer"));
			}
			if (prefixOffset < 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("prefixOffset", System.Runtime.Serialization.SR.GetString("The value of this argument must be non-negative.")));
			}
			if (prefixOffset > prefixBuffer.Length)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("prefixOffset", System.Runtime.Serialization.SR.GetString("The specified offset exceeds the buffer size ({0} bytes).", new object[]
				{
					prefixBuffer.Length
				})));
			}
			if (prefixLength < 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("prefixLength", System.Runtime.Serialization.SR.GetString("The value of this argument must be non-negative.")));
			}
			if (prefixLength > prefixBuffer.Length - prefixOffset)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("prefixLength", System.Runtime.Serialization.SR.GetString("The specified size exceeds the remaining buffer space ({0} bytes).", new object[]
				{
					prefixBuffer.Length - prefixOffset
				})));
			}
			if (localNameBuffer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("localNameBuffer"));
			}
			if (localNameOffset < 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("localNameOffset", System.Runtime.Serialization.SR.GetString("The value of this argument must be non-negative.")));
			}
			if (localNameOffset > localNameBuffer.Length)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("localNameOffset", System.Runtime.Serialization.SR.GetString("The specified offset exceeds the buffer size ({0} bytes).", new object[]
				{
					localNameBuffer.Length
				})));
			}
			if (localNameLength < 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("localNameLength", System.Runtime.Serialization.SR.GetString("The value of this argument must be non-negative.")));
			}
			if (localNameLength > localNameBuffer.Length - localNameOffset)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("localNameLength", System.Runtime.Serialization.SR.GetString("The specified size exceeds the remaining buffer space ({0} bytes).", new object[]
				{
					localNameBuffer.Length - localNameOffset
				})));
			}
			this.ThrowIfClosed();
			this.attribute.offset = this.elementWriter.Position;
			this.attribute.length = 0;
			this.attribute.prefixOffset = this.attribute.offset + 1;
			this.attribute.prefixLength = prefixLength;
			this.attribute.localNameOffset = this.attribute.prefixOffset + prefixLength + ((prefixLength != 0) ? 1 : 0);
			this.attribute.localNameLength = localNameLength;
			this.attribute.nsOffset = 0;
			this.attribute.nsLength = 0;
			this.elementWriter.WriteStartAttribute(prefixBuffer, prefixOffset, prefixLength, localNameBuffer, localNameOffset, localNameLength);
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x00014A30 File Offset: 0x00012C30
		public void WriteEndAttribute()
		{
			this.ThrowIfClosed();
			this.elementWriter.WriteEndAttribute();
			this.attribute.length = this.elementWriter.Position - this.attribute.offset;
			this.AddAttribute(ref this.attribute);
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x00014A7C File Offset: 0x00012C7C
		public void WriteCharEntity(int ch)
		{
			this.ThrowIfClosed();
			if (ch <= 65535)
			{
				char[] chars = new char[]
				{
					(char)ch
				};
				this.WriteEscapedText(chars, 0, 1);
				return;
			}
			this.WriteText(ch);
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x00014AB4 File Offset: 0x00012CB4
		public void WriteEscapedText(string value)
		{
			if (value == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
			}
			this.ThrowIfClosed();
			if (this.depth > 0)
			{
				if (this.inStartElement)
				{
					this.elementWriter.WriteEscapedText(value);
					return;
				}
				this.writer.WriteEscapedText(value);
			}
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x00014AF4 File Offset: 0x00012CF4
		public void WriteEscapedText(byte[] chars, int offset, int count)
		{
			if (chars == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("chars"));
			}
			if (offset < 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("offset", System.Runtime.Serialization.SR.GetString("The value of this argument must be non-negative.")));
			}
			if (offset > chars.Length)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("offset", System.Runtime.Serialization.SR.GetString("The specified offset exceeds the buffer size ({0} bytes).", new object[]
				{
					chars.Length
				})));
			}
			if (count < 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("count", System.Runtime.Serialization.SR.GetString("The value of this argument must be non-negative.")));
			}
			if (count > chars.Length - offset)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("count", System.Runtime.Serialization.SR.GetString("The specified size exceeds the remaining buffer space ({0} bytes).", new object[]
				{
					chars.Length - offset
				})));
			}
			this.ThrowIfClosed();
			if (this.depth > 0)
			{
				if (this.inStartElement)
				{
					this.elementWriter.WriteEscapedText(chars, offset, count);
					return;
				}
				this.writer.WriteEscapedText(chars, offset, count);
			}
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x00014BEA File Offset: 0x00012DEA
		public void WriteEscapedText(char[] chars, int offset, int count)
		{
			this.ThrowIfClosed();
			if (this.depth > 0)
			{
				if (this.inStartElement)
				{
					this.elementWriter.WriteEscapedText(chars, offset, count);
					return;
				}
				this.writer.WriteEscapedText(chars, offset, count);
			}
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x00014C20 File Offset: 0x00012E20
		public void WriteText(int ch)
		{
			this.ThrowIfClosed();
			if (this.inStartElement)
			{
				this.elementWriter.WriteText(ch);
				return;
			}
			this.writer.WriteText(ch);
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x00014C4C File Offset: 0x00012E4C
		public void WriteText(byte[] chars, int offset, int count)
		{
			this.ThrowIfClosed();
			if (chars == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("chars"));
			}
			if (offset < 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("offset", System.Runtime.Serialization.SR.GetString("The value of this argument must be non-negative.")));
			}
			if (offset > chars.Length)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("offset", System.Runtime.Serialization.SR.GetString("The specified offset exceeds the buffer size ({0} bytes).", new object[]
				{
					chars.Length
				})));
			}
			if (count < 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("count", System.Runtime.Serialization.SR.GetString("The value of this argument must be non-negative.")));
			}
			if (count > chars.Length - offset)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("count", System.Runtime.Serialization.SR.GetString("The specified size exceeds the remaining buffer space ({0} bytes).", new object[]
				{
					chars.Length - offset
				})));
			}
			if (this.inStartElement)
			{
				this.elementWriter.WriteText(chars, offset, count);
				return;
			}
			this.writer.WriteText(chars, offset, count);
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x00014D39 File Offset: 0x00012F39
		public void WriteText(string value)
		{
			if (value == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("value"));
			}
			if (value.Length > 0)
			{
				if (this.inStartElement)
				{
					this.elementWriter.WriteText(value);
					return;
				}
				this.writer.WriteText(value);
			}
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x00014D78 File Offset: 0x00012F78
		public void WriteText(char[] chars, int offset, int count)
		{
			this.ThrowIfClosed();
			if (chars == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("chars"));
			}
			if (offset < 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("offset", System.Runtime.Serialization.SR.GetString("The value of this argument must be non-negative.")));
			}
			if (offset > chars.Length)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("offset", System.Runtime.Serialization.SR.GetString("The specified offset exceeds the buffer size ({0} bytes).", new object[]
				{
					chars.Length
				})));
			}
			if (count < 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("count", System.Runtime.Serialization.SR.GetString("The value of this argument must be non-negative.")));
			}
			if (count > chars.Length - offset)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("count", System.Runtime.Serialization.SR.GetString("The specified size exceeds the remaining buffer space ({0} bytes).", new object[]
				{
					chars.Length - offset
				})));
			}
			if (this.inStartElement)
			{
				this.elementWriter.WriteText(chars, offset, count);
				return;
			}
			this.writer.WriteText(chars, offset, count);
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x00014E65 File Offset: 0x00013065
		private void ThrowIfClosed()
		{
			if (this.writer == null)
			{
				this.ThrowClosed();
			}
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x00014E75 File Offset: 0x00013075
		private void ThrowClosed()
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ObjectDisposedException(base.GetType().ToString()));
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x00014E8C File Offset: 0x0001308C
		private void WriteXmlnsAttribute(ref XmlCanonicalWriter.XmlnsAttribute xmlnsAttribute)
		{
			if (xmlnsAttribute.referred)
			{
				this.writer.WriteXmlnsAttribute(this.xmlnsBuffer, xmlnsAttribute.prefixOffset, xmlnsAttribute.prefixLength, this.xmlnsBuffer, xmlnsAttribute.nsOffset, xmlnsAttribute.nsLength);
			}
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x00014EC8 File Offset: 0x000130C8
		private void SortAttributes()
		{
			if (this.attributeCount < 16)
			{
				for (int i = 0; i < this.attributeCount - 1; i++)
				{
					int num = i;
					for (int j = i + 1; j < this.attributeCount; j++)
					{
						if (this.Compare(ref this.attributes[j], ref this.attributes[num]) < 0)
						{
							num = j;
						}
					}
					if (num != i)
					{
						XmlCanonicalWriter.Attribute attribute = this.attributes[i];
						this.attributes[i] = this.attributes[num];
						this.attributes[num] = attribute;
					}
				}
				return;
			}
			new XmlCanonicalWriter.AttributeSorter(this).Sort();
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x00014F70 File Offset: 0x00013170
		private void AddAttribute(ref XmlCanonicalWriter.Attribute attribute)
		{
			if (this.attributes == null)
			{
				this.attributes = new XmlCanonicalWriter.Attribute[4];
			}
			else if (this.attributeCount == this.attributes.Length)
			{
				XmlCanonicalWriter.Attribute[] destinationArray = new XmlCanonicalWriter.Attribute[this.attributeCount * 2];
				Array.Copy(this.attributes, destinationArray, this.attributeCount);
				this.attributes = destinationArray;
			}
			this.attributes[this.attributeCount] = attribute;
			this.attributeCount++;
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x00014FF0 File Offset: 0x000131F0
		private void AddXmlnsAttribute(ref XmlCanonicalWriter.XmlnsAttribute xmlnsAttribute)
		{
			if (this.xmlnsAttributes == null)
			{
				this.xmlnsAttributes = new XmlCanonicalWriter.XmlnsAttribute[4];
			}
			else if (this.xmlnsAttributes.Length == this.xmlnsAttributeCount)
			{
				XmlCanonicalWriter.XmlnsAttribute[] destinationArray = new XmlCanonicalWriter.XmlnsAttribute[this.xmlnsAttributeCount * 2];
				Array.Copy(this.xmlnsAttributes, destinationArray, this.xmlnsAttributeCount);
				this.xmlnsAttributes = destinationArray;
			}
			if (this.depth > 0 && this.inclusivePrefixes != null && this.IsInclusivePrefix(ref xmlnsAttribute))
			{
				xmlnsAttribute.referred = true;
			}
			if (this.depth == 0)
			{
				XmlCanonicalWriter.XmlnsAttribute[] array = this.xmlnsAttributes;
				int num = this.xmlnsAttributeCount;
				this.xmlnsAttributeCount = num + 1;
				array[num] = xmlnsAttribute;
				return;
			}
			int i = this.scopes[this.depth - 1].xmlnsAttributeCount;
			bool flag = true;
			while (i < this.xmlnsAttributeCount)
			{
				int num2 = this.Compare(ref xmlnsAttribute, ref this.xmlnsAttributes[i]);
				if (num2 > 0)
				{
					i++;
				}
				else
				{
					if (num2 == 0)
					{
						this.xmlnsAttributes[i] = xmlnsAttribute;
						flag = false;
						break;
					}
					break;
				}
			}
			if (flag)
			{
				Array.Copy(this.xmlnsAttributes, i, this.xmlnsAttributes, i + 1, this.xmlnsAttributeCount - i);
				this.xmlnsAttributes[i] = xmlnsAttribute;
				this.xmlnsAttributeCount++;
			}
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x00015138 File Offset: 0x00013338
		private void ResolvePrefix(int prefixOffset, int prefixLength, out int nsOffset, out int nsLength)
		{
			int num = this.scopes[this.depth - 1].xmlnsAttributeCount;
			int num2 = this.xmlnsAttributeCount - 1;
			while (!this.Equals(this.elementBuffer, prefixOffset, prefixLength, this.xmlnsBuffer, this.xmlnsAttributes[num2].prefixOffset, this.xmlnsAttributes[num2].prefixLength))
			{
				num2--;
			}
			nsOffset = this.xmlnsAttributes[num2].nsOffset;
			nsLength = this.xmlnsAttributes[num2].nsLength;
			if (num2 < num)
			{
				if (!this.xmlnsAttributes[num2].referred)
				{
					XmlCanonicalWriter.XmlnsAttribute xmlnsAttribute = this.xmlnsAttributes[num2];
					xmlnsAttribute.referred = true;
					this.AddXmlnsAttribute(ref xmlnsAttribute);
					return;
				}
			}
			else
			{
				this.xmlnsAttributes[num2].referred = true;
			}
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x00015213 File Offset: 0x00013413
		private void ResolvePrefix(ref XmlCanonicalWriter.Attribute attribute)
		{
			if (attribute.prefixLength != 0)
			{
				this.ResolvePrefix(attribute.prefixOffset, attribute.prefixLength, out attribute.nsOffset, out attribute.nsLength);
			}
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x0001523C File Offset: 0x0001343C
		private void ResolvePrefixes()
		{
			int num;
			int num2;
			this.ResolvePrefix(this.element.prefixOffset, this.element.prefixLength, out num, out num2);
			for (int i = 0; i < this.attributeCount; i++)
			{
				this.ResolvePrefix(ref this.attributes[i]);
			}
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x0001528C File Offset: 0x0001348C
		private int Compare(ref XmlCanonicalWriter.XmlnsAttribute xmlnsAttribute1, ref XmlCanonicalWriter.XmlnsAttribute xmlnsAttribute2)
		{
			return this.Compare(this.xmlnsBuffer, xmlnsAttribute1.prefixOffset, xmlnsAttribute1.prefixLength, xmlnsAttribute2.prefixOffset, xmlnsAttribute2.prefixLength);
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x000152B4 File Offset: 0x000134B4
		private int Compare(ref XmlCanonicalWriter.Attribute attribute1, ref XmlCanonicalWriter.Attribute attribute2)
		{
			int num = this.Compare(this.xmlnsBuffer, attribute1.nsOffset, attribute1.nsLength, attribute2.nsOffset, attribute2.nsLength);
			if (num == 0)
			{
				num = this.Compare(this.elementBuffer, attribute1.localNameOffset, attribute1.localNameLength, attribute2.localNameOffset, attribute2.localNameLength);
			}
			return num;
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x0001530F File Offset: 0x0001350F
		private int Compare(byte[] buffer, int offset1, int length1, int offset2, int length2)
		{
			if (offset1 == offset2)
			{
				return length1 - length2;
			}
			return this.Compare(buffer, offset1, length1, buffer, offset2, length2);
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x0001532C File Offset: 0x0001352C
		private int Compare(byte[] buffer1, int offset1, int length1, byte[] buffer2, int offset2, int length2)
		{
			int num = Math.Min(length1, length2);
			int num2 = 0;
			int num3 = 0;
			while (num3 < num && num2 == 0)
			{
				num2 = (int)(buffer1[offset1 + num3] - buffer2[offset2 + num3]);
				num3++;
			}
			if (num2 == 0)
			{
				num2 = length1 - length2;
			}
			return num2;
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x0001536C File Offset: 0x0001356C
		private bool Equals(byte[] buffer1, int offset1, int length1, byte[] buffer2, int offset2, int length2)
		{
			if (length1 != length2)
			{
				return false;
			}
			for (int i = 0; i < length1; i++)
			{
				if (buffer1[offset1 + i] != buffer2[offset2 + i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x0001539D File Offset: 0x0001359D
		// Note: this type is marked as 'beforefieldinit'.
		static XmlCanonicalWriter()
		{
		}

		// Token: 0x0400023F RID: 575
		private XmlUTF8NodeWriter writer;

		// Token: 0x04000240 RID: 576
		private MemoryStream elementStream;

		// Token: 0x04000241 RID: 577
		private byte[] elementBuffer;

		// Token: 0x04000242 RID: 578
		private XmlUTF8NodeWriter elementWriter;

		// Token: 0x04000243 RID: 579
		private bool inStartElement;

		// Token: 0x04000244 RID: 580
		private int depth;

		// Token: 0x04000245 RID: 581
		private XmlCanonicalWriter.Scope[] scopes;

		// Token: 0x04000246 RID: 582
		private int xmlnsAttributeCount;

		// Token: 0x04000247 RID: 583
		private XmlCanonicalWriter.XmlnsAttribute[] xmlnsAttributes;

		// Token: 0x04000248 RID: 584
		private int attributeCount;

		// Token: 0x04000249 RID: 585
		private XmlCanonicalWriter.Attribute[] attributes;

		// Token: 0x0400024A RID: 586
		private XmlCanonicalWriter.Attribute attribute;

		// Token: 0x0400024B RID: 587
		private XmlCanonicalWriter.Element element;

		// Token: 0x0400024C RID: 588
		private byte[] xmlnsBuffer;

		// Token: 0x0400024D RID: 589
		private int xmlnsOffset;

		// Token: 0x0400024E RID: 590
		private const int maxBytesPerChar = 3;

		// Token: 0x0400024F RID: 591
		private int xmlnsStartOffset;

		// Token: 0x04000250 RID: 592
		private bool includeComments;

		// Token: 0x04000251 RID: 593
		private string[] inclusivePrefixes;

		// Token: 0x04000252 RID: 594
		private const string xmlnsNamespace = "http://www.w3.org/2000/xmlns/";

		// Token: 0x04000253 RID: 595
		private static readonly bool[] isEscapedAttributeChar = new bool[]
		{
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			false,
			false,
			true,
			false,
			false,
			false,
			true,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			true,
			false,
			false,
			false
		};

		// Token: 0x04000254 RID: 596
		private static readonly bool[] isEscapedElementChar = new bool[]
		{
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			false,
			false,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			false,
			false,
			false,
			false,
			false,
			false,
			true,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			true,
			false,
			true,
			false
		};

		// Token: 0x02000057 RID: 87
		private class AttributeSorter : IComparer
		{
			// Token: 0x06000419 RID: 1049 RVA: 0x000153CD File Offset: 0x000135CD
			public AttributeSorter(XmlCanonicalWriter writer)
			{
				this.writer = writer;
			}

			// Token: 0x0600041A RID: 1050 RVA: 0x000153DC File Offset: 0x000135DC
			public void Sort()
			{
				object[] array = new object[this.writer.attributeCount];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = i;
				}
				Array.Sort(array, this);
				XmlCanonicalWriter.Attribute[] array2 = new XmlCanonicalWriter.Attribute[this.writer.attributes.Length];
				for (int j = 0; j < array.Length; j++)
				{
					array2[j] = this.writer.attributes[(int)array[j]];
				}
				this.writer.attributes = array2;
			}

			// Token: 0x0600041B RID: 1051 RVA: 0x00015464 File Offset: 0x00013664
			public int Compare(object obj1, object obj2)
			{
				int num = (int)obj1;
				int num2 = (int)obj2;
				return this.writer.Compare(ref this.writer.attributes[num], ref this.writer.attributes[num2]);
			}

			// Token: 0x04000255 RID: 597
			private XmlCanonicalWriter writer;
		}

		// Token: 0x02000058 RID: 88
		private struct Scope
		{
			// Token: 0x04000256 RID: 598
			public int xmlnsAttributeCount;

			// Token: 0x04000257 RID: 599
			public int xmlnsOffset;
		}

		// Token: 0x02000059 RID: 89
		private struct Element
		{
			// Token: 0x04000258 RID: 600
			public int prefixOffset;

			// Token: 0x04000259 RID: 601
			public int prefixLength;

			// Token: 0x0400025A RID: 602
			public int localNameOffset;

			// Token: 0x0400025B RID: 603
			public int localNameLength;
		}

		// Token: 0x0200005A RID: 90
		private struct Attribute
		{
			// Token: 0x0400025C RID: 604
			public int prefixOffset;

			// Token: 0x0400025D RID: 605
			public int prefixLength;

			// Token: 0x0400025E RID: 606
			public int localNameOffset;

			// Token: 0x0400025F RID: 607
			public int localNameLength;

			// Token: 0x04000260 RID: 608
			public int nsOffset;

			// Token: 0x04000261 RID: 609
			public int nsLength;

			// Token: 0x04000262 RID: 610
			public int offset;

			// Token: 0x04000263 RID: 611
			public int length;
		}

		// Token: 0x0200005B RID: 91
		private struct XmlnsAttribute
		{
			// Token: 0x04000264 RID: 612
			public int prefixOffset;

			// Token: 0x04000265 RID: 613
			public int prefixLength;

			// Token: 0x04000266 RID: 614
			public int nsOffset;

			// Token: 0x04000267 RID: 615
			public int nsLength;

			// Token: 0x04000268 RID: 616
			public bool referred;
		}
	}
}
