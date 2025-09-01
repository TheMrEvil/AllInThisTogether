using System;
using System.IO;
using System.Runtime.Serialization;
using System.Security;

namespace System.Xml
{
	// Token: 0x02000050 RID: 80
	internal class XmlBinaryWriter : XmlBaseWriter, IXmlBinaryWriterInitializer
	{
		// Token: 0x06000361 RID: 865 RVA: 0x00010D68 File Offset: 0x0000EF68
		public void SetOutput(Stream stream, IXmlDictionary dictionary, XmlBinaryWriterSession session, bool ownsStream)
		{
			if (stream == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("stream"));
			}
			if (this.writer == null)
			{
				this.writer = new XmlBinaryNodeWriter();
			}
			this.writer.SetOutput(stream, dictionary, session, ownsStream);
			base.SetOutput(this.writer);
		}

		// Token: 0x06000362 RID: 866 RVA: 0x0000FA6F File Offset: 0x0000DC6F
		protected override XmlSigningNodeWriter CreateSigningNodeWriter()
		{
			return new XmlSigningNodeWriter(false);
		}

		// Token: 0x06000363 RID: 867 RVA: 0x00010DB8 File Offset: 0x0000EFB8
		protected override void WriteTextNode(XmlDictionaryReader reader, bool attribute)
		{
			Type valueType = reader.ValueType;
			if (valueType == typeof(string))
			{
				XmlDictionaryString value;
				if (reader.TryGetValueAsDictionaryString(out value))
				{
					this.WriteString(value);
				}
				else if (reader.CanReadValueChunk)
				{
					if (this.chars == null)
					{
						this.chars = new char[256];
					}
					int count;
					while ((count = reader.ReadValueChunk(this.chars, 0, this.chars.Length)) > 0)
					{
						this.WriteChars(this.chars, 0, count);
					}
				}
				else
				{
					this.WriteString(reader.Value);
				}
				if (!attribute)
				{
					reader.Read();
					return;
				}
			}
			else if (valueType == typeof(byte[]))
			{
				if (reader.CanReadBinaryContent)
				{
					if (this.bytes == null)
					{
						this.bytes = new byte[384];
					}
					int count2;
					while ((count2 = reader.ReadValueAsBase64(this.bytes, 0, this.bytes.Length)) > 0)
					{
						this.WriteBase64(this.bytes, 0, count2);
					}
				}
				else
				{
					this.WriteString(reader.Value);
				}
				if (!attribute)
				{
					reader.Read();
					return;
				}
			}
			else
			{
				if (valueType == typeof(int))
				{
					this.WriteValue(reader.ReadContentAsInt());
					return;
				}
				if (valueType == typeof(long))
				{
					this.WriteValue(reader.ReadContentAsLong());
					return;
				}
				if (valueType == typeof(bool))
				{
					this.WriteValue(reader.ReadContentAsBoolean());
					return;
				}
				if (valueType == typeof(double))
				{
					this.WriteValue(reader.ReadContentAsDouble());
					return;
				}
				if (valueType == typeof(DateTime))
				{
					this.WriteValue(reader.ReadContentAsDateTime());
					return;
				}
				if (valueType == typeof(float))
				{
					this.WriteValue(reader.ReadContentAsFloat());
					return;
				}
				if (valueType == typeof(decimal))
				{
					this.WriteValue(reader.ReadContentAsDecimal());
					return;
				}
				if (valueType == typeof(UniqueId))
				{
					this.WriteValue(reader.ReadContentAsUniqueId());
					return;
				}
				if (valueType == typeof(Guid))
				{
					this.WriteValue(reader.ReadContentAsGuid());
					return;
				}
				if (valueType == typeof(TimeSpan))
				{
					this.WriteValue(reader.ReadContentAsTimeSpan());
					return;
				}
				this.WriteValue(reader.ReadContentAsObject());
			}
		}

		// Token: 0x06000364 RID: 868 RVA: 0x00011011 File Offset: 0x0000F211
		private void WriteStartArray(string prefix, string localName, string namespaceUri, int count)
		{
			base.StartArray(count);
			this.writer.WriteArrayNode();
			this.WriteStartElement(prefix, localName, namespaceUri);
			this.WriteEndElement();
		}

		// Token: 0x06000365 RID: 869 RVA: 0x00011035 File Offset: 0x0000F235
		private void WriteStartArray(string prefix, XmlDictionaryString localName, XmlDictionaryString namespaceUri, int count)
		{
			base.StartArray(count);
			this.writer.WriteArrayNode();
			this.WriteStartElement(prefix, localName, namespaceUri);
			this.WriteEndElement();
		}

		// Token: 0x06000366 RID: 870 RVA: 0x00011059 File Offset: 0x0000F259
		private void WriteEndArray()
		{
			base.EndArray();
		}

		// Token: 0x06000367 RID: 871 RVA: 0x00011061 File Offset: 0x0000F261
		[SecurityCritical]
		private unsafe void UnsafeWriteArray(string prefix, string localName, string namespaceUri, XmlBinaryNodeType nodeType, int count, byte* array, byte* arrayMax)
		{
			this.WriteStartArray(prefix, localName, namespaceUri, count);
			this.writer.UnsafeWriteArray(nodeType, count, array, arrayMax);
			this.WriteEndArray();
		}

		// Token: 0x06000368 RID: 872 RVA: 0x00011087 File Offset: 0x0000F287
		[SecurityCritical]
		private unsafe void UnsafeWriteArray(string prefix, XmlDictionaryString localName, XmlDictionaryString namespaceUri, XmlBinaryNodeType nodeType, int count, byte* array, byte* arrayMax)
		{
			this.WriteStartArray(prefix, localName, namespaceUri, count);
			this.writer.UnsafeWriteArray(nodeType, count, array, arrayMax);
			this.WriteEndArray();
		}

		// Token: 0x06000369 RID: 873 RVA: 0x000110B0 File Offset: 0x0000F2B0
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

		// Token: 0x0600036A RID: 874 RVA: 0x00011180 File Offset: 0x0000F380
		[SecuritySafeCritical]
		public unsafe override void WriteArray(string prefix, string localName, string namespaceUri, bool[] array, int offset, int count)
		{
			if (base.Signing)
			{
				base.WriteArray(prefix, localName, namespaceUri, array, offset, count);
				return;
			}
			this.CheckArray(array, offset, count);
			if (count > 0)
			{
				fixed (bool* ptr = &array[offset])
				{
					bool* ptr2 = ptr;
					this.UnsafeWriteArray(prefix, localName, namespaceUri, XmlBinaryNodeType.BoolTextWithEndElement, count, (byte*)ptr2, (byte*)(ptr2 + count));
				}
			}
		}

		// Token: 0x0600036B RID: 875 RVA: 0x000111DC File Offset: 0x0000F3DC
		[SecuritySafeCritical]
		public unsafe override void WriteArray(string prefix, XmlDictionaryString localName, XmlDictionaryString namespaceUri, bool[] array, int offset, int count)
		{
			if (base.Signing)
			{
				base.WriteArray(prefix, localName, namespaceUri, array, offset, count);
				return;
			}
			this.CheckArray(array, offset, count);
			if (count > 0)
			{
				fixed (bool* ptr = &array[offset])
				{
					bool* ptr2 = ptr;
					this.UnsafeWriteArray(prefix, localName, namespaceUri, XmlBinaryNodeType.BoolTextWithEndElement, count, (byte*)ptr2, (byte*)(ptr2 + count));
				}
			}
		}

		// Token: 0x0600036C RID: 876 RVA: 0x00011238 File Offset: 0x0000F438
		[SecuritySafeCritical]
		public unsafe override void WriteArray(string prefix, string localName, string namespaceUri, short[] array, int offset, int count)
		{
			if (base.Signing)
			{
				base.WriteArray(prefix, localName, namespaceUri, array, offset, count);
				return;
			}
			this.CheckArray(array, offset, count);
			if (count > 0)
			{
				fixed (short* ptr = &array[offset])
				{
					short* ptr2 = ptr;
					this.UnsafeWriteArray(prefix, localName, namespaceUri, XmlBinaryNodeType.Int16TextWithEndElement, count, (byte*)ptr2, (byte*)(ptr2 + count));
				}
			}
		}

		// Token: 0x0600036D RID: 877 RVA: 0x00011298 File Offset: 0x0000F498
		[SecuritySafeCritical]
		public unsafe override void WriteArray(string prefix, XmlDictionaryString localName, XmlDictionaryString namespaceUri, short[] array, int offset, int count)
		{
			if (base.Signing)
			{
				base.WriteArray(prefix, localName, namespaceUri, array, offset, count);
				return;
			}
			this.CheckArray(array, offset, count);
			if (count > 0)
			{
				fixed (short* ptr = &array[offset])
				{
					short* ptr2 = ptr;
					this.UnsafeWriteArray(prefix, localName, namespaceUri, XmlBinaryNodeType.Int16TextWithEndElement, count, (byte*)ptr2, (byte*)(ptr2 + count));
				}
			}
		}

		// Token: 0x0600036E RID: 878 RVA: 0x000112F8 File Offset: 0x0000F4F8
		[SecuritySafeCritical]
		public unsafe override void WriteArray(string prefix, string localName, string namespaceUri, int[] array, int offset, int count)
		{
			if (base.Signing)
			{
				base.WriteArray(prefix, localName, namespaceUri, array, offset, count);
				return;
			}
			this.CheckArray(array, offset, count);
			if (count > 0)
			{
				fixed (int* ptr = &array[offset])
				{
					int* ptr2 = ptr;
					this.UnsafeWriteArray(prefix, localName, namespaceUri, XmlBinaryNodeType.Int32TextWithEndElement, count, (byte*)ptr2, (byte*)(ptr2 + count));
				}
			}
		}

		// Token: 0x0600036F RID: 879 RVA: 0x00011358 File Offset: 0x0000F558
		[SecuritySafeCritical]
		public unsafe override void WriteArray(string prefix, XmlDictionaryString localName, XmlDictionaryString namespaceUri, int[] array, int offset, int count)
		{
			if (base.Signing)
			{
				base.WriteArray(prefix, localName, namespaceUri, array, offset, count);
				return;
			}
			this.CheckArray(array, offset, count);
			if (count > 0)
			{
				fixed (int* ptr = &array[offset])
				{
					int* ptr2 = ptr;
					this.UnsafeWriteArray(prefix, localName, namespaceUri, XmlBinaryNodeType.Int32TextWithEndElement, count, (byte*)ptr2, (byte*)(ptr2 + count));
				}
			}
		}

		// Token: 0x06000370 RID: 880 RVA: 0x000113B8 File Offset: 0x0000F5B8
		[SecuritySafeCritical]
		public unsafe override void WriteArray(string prefix, string localName, string namespaceUri, long[] array, int offset, int count)
		{
			if (base.Signing)
			{
				base.WriteArray(prefix, localName, namespaceUri, array, offset, count);
				return;
			}
			this.CheckArray(array, offset, count);
			if (count > 0)
			{
				fixed (long* ptr = &array[offset])
				{
					long* ptr2 = ptr;
					this.UnsafeWriteArray(prefix, localName, namespaceUri, XmlBinaryNodeType.Int64TextWithEndElement, count, (byte*)ptr2, (byte*)(ptr2 + count));
				}
			}
		}

		// Token: 0x06000371 RID: 881 RVA: 0x00011418 File Offset: 0x0000F618
		[SecuritySafeCritical]
		public unsafe override void WriteArray(string prefix, XmlDictionaryString localName, XmlDictionaryString namespaceUri, long[] array, int offset, int count)
		{
			if (base.Signing)
			{
				base.WriteArray(prefix, localName, namespaceUri, array, offset, count);
				return;
			}
			this.CheckArray(array, offset, count);
			if (count > 0)
			{
				fixed (long* ptr = &array[offset])
				{
					long* ptr2 = ptr;
					this.UnsafeWriteArray(prefix, localName, namespaceUri, XmlBinaryNodeType.Int64TextWithEndElement, count, (byte*)ptr2, (byte*)(ptr2 + count));
				}
			}
		}

		// Token: 0x06000372 RID: 882 RVA: 0x00011478 File Offset: 0x0000F678
		[SecuritySafeCritical]
		public unsafe override void WriteArray(string prefix, string localName, string namespaceUri, float[] array, int offset, int count)
		{
			if (base.Signing)
			{
				base.WriteArray(prefix, localName, namespaceUri, array, offset, count);
				return;
			}
			this.CheckArray(array, offset, count);
			if (count > 0)
			{
				fixed (float* ptr = &array[offset])
				{
					float* ptr2 = ptr;
					this.UnsafeWriteArray(prefix, localName, namespaceUri, XmlBinaryNodeType.FloatTextWithEndElement, count, (byte*)ptr2, (byte*)(ptr2 + count));
				}
			}
		}

		// Token: 0x06000373 RID: 883 RVA: 0x000114D8 File Offset: 0x0000F6D8
		[SecuritySafeCritical]
		public unsafe override void WriteArray(string prefix, XmlDictionaryString localName, XmlDictionaryString namespaceUri, float[] array, int offset, int count)
		{
			if (base.Signing)
			{
				base.WriteArray(prefix, localName, namespaceUri, array, offset, count);
				return;
			}
			this.CheckArray(array, offset, count);
			if (count > 0)
			{
				fixed (float* ptr = &array[offset])
				{
					float* ptr2 = ptr;
					this.UnsafeWriteArray(prefix, localName, namespaceUri, XmlBinaryNodeType.FloatTextWithEndElement, count, (byte*)ptr2, (byte*)(ptr2 + count));
				}
			}
		}

		// Token: 0x06000374 RID: 884 RVA: 0x00011538 File Offset: 0x0000F738
		[SecuritySafeCritical]
		public unsafe override void WriteArray(string prefix, string localName, string namespaceUri, double[] array, int offset, int count)
		{
			if (base.Signing)
			{
				base.WriteArray(prefix, localName, namespaceUri, array, offset, count);
				return;
			}
			this.CheckArray(array, offset, count);
			if (count > 0)
			{
				fixed (double* ptr = &array[offset])
				{
					double* ptr2 = ptr;
					this.UnsafeWriteArray(prefix, localName, namespaceUri, XmlBinaryNodeType.DoubleTextWithEndElement, count, (byte*)ptr2, (byte*)(ptr2 + count));
				}
			}
		}

		// Token: 0x06000375 RID: 885 RVA: 0x00011598 File Offset: 0x0000F798
		[SecuritySafeCritical]
		public unsafe override void WriteArray(string prefix, XmlDictionaryString localName, XmlDictionaryString namespaceUri, double[] array, int offset, int count)
		{
			if (base.Signing)
			{
				base.WriteArray(prefix, localName, namespaceUri, array, offset, count);
				return;
			}
			this.CheckArray(array, offset, count);
			if (count > 0)
			{
				fixed (double* ptr = &array[offset])
				{
					double* ptr2 = ptr;
					this.UnsafeWriteArray(prefix, localName, namespaceUri, XmlBinaryNodeType.DoubleTextWithEndElement, count, (byte*)ptr2, (byte*)(ptr2 + count));
				}
			}
		}

		// Token: 0x06000376 RID: 886 RVA: 0x000115F8 File Offset: 0x0000F7F8
		[SecuritySafeCritical]
		public unsafe override void WriteArray(string prefix, string localName, string namespaceUri, decimal[] array, int offset, int count)
		{
			if (base.Signing)
			{
				base.WriteArray(prefix, localName, namespaceUri, array, offset, count);
				return;
			}
			this.CheckArray(array, offset, count);
			if (count > 0)
			{
				fixed (decimal* ptr = &array[offset])
				{
					decimal* ptr2 = ptr;
					this.UnsafeWriteArray(prefix, localName, namespaceUri, XmlBinaryNodeType.DecimalTextWithEndElement, count, (byte*)ptr2, (byte*)(ptr2 + (IntPtr)count * 16 / (IntPtr)sizeof(decimal)));
				}
			}
		}

		// Token: 0x06000377 RID: 887 RVA: 0x00011658 File Offset: 0x0000F858
		[SecuritySafeCritical]
		public unsafe override void WriteArray(string prefix, XmlDictionaryString localName, XmlDictionaryString namespaceUri, decimal[] array, int offset, int count)
		{
			if (base.Signing)
			{
				base.WriteArray(prefix, localName, namespaceUri, array, offset, count);
				return;
			}
			this.CheckArray(array, offset, count);
			if (count > 0)
			{
				fixed (decimal* ptr = &array[offset])
				{
					decimal* ptr2 = ptr;
					this.UnsafeWriteArray(prefix, localName, namespaceUri, XmlBinaryNodeType.DecimalTextWithEndElement, count, (byte*)ptr2, (byte*)(ptr2 + (IntPtr)count * 16 / (IntPtr)sizeof(decimal)));
				}
			}
		}

		// Token: 0x06000378 RID: 888 RVA: 0x000116B8 File Offset: 0x0000F8B8
		public override void WriteArray(string prefix, string localName, string namespaceUri, DateTime[] array, int offset, int count)
		{
			if (base.Signing)
			{
				base.WriteArray(prefix, localName, namespaceUri, array, offset, count);
				return;
			}
			this.CheckArray(array, offset, count);
			if (count > 0)
			{
				this.WriteStartArray(prefix, localName, namespaceUri, count);
				this.writer.WriteDateTimeArray(array, offset, count);
				this.WriteEndArray();
			}
		}

		// Token: 0x06000379 RID: 889 RVA: 0x00011710 File Offset: 0x0000F910
		public override void WriteArray(string prefix, XmlDictionaryString localName, XmlDictionaryString namespaceUri, DateTime[] array, int offset, int count)
		{
			if (base.Signing)
			{
				base.WriteArray(prefix, localName, namespaceUri, array, offset, count);
				return;
			}
			this.CheckArray(array, offset, count);
			if (count > 0)
			{
				this.WriteStartArray(prefix, localName, namespaceUri, count);
				this.writer.WriteDateTimeArray(array, offset, count);
				this.WriteEndArray();
			}
		}

		// Token: 0x0600037A RID: 890 RVA: 0x00011768 File Offset: 0x0000F968
		public override void WriteArray(string prefix, string localName, string namespaceUri, Guid[] array, int offset, int count)
		{
			if (base.Signing)
			{
				base.WriteArray(prefix, localName, namespaceUri, array, offset, count);
				return;
			}
			this.CheckArray(array, offset, count);
			if (count > 0)
			{
				this.WriteStartArray(prefix, localName, namespaceUri, count);
				this.writer.WriteGuidArray(array, offset, count);
				this.WriteEndArray();
			}
		}

		// Token: 0x0600037B RID: 891 RVA: 0x000117C0 File Offset: 0x0000F9C0
		public override void WriteArray(string prefix, XmlDictionaryString localName, XmlDictionaryString namespaceUri, Guid[] array, int offset, int count)
		{
			if (base.Signing)
			{
				base.WriteArray(prefix, localName, namespaceUri, array, offset, count);
				return;
			}
			this.CheckArray(array, offset, count);
			if (count > 0)
			{
				this.WriteStartArray(prefix, localName, namespaceUri, count);
				this.writer.WriteGuidArray(array, offset, count);
				this.WriteEndArray();
			}
		}

		// Token: 0x0600037C RID: 892 RVA: 0x00011818 File Offset: 0x0000FA18
		public override void WriteArray(string prefix, string localName, string namespaceUri, TimeSpan[] array, int offset, int count)
		{
			if (base.Signing)
			{
				base.WriteArray(prefix, localName, namespaceUri, array, offset, count);
				return;
			}
			this.CheckArray(array, offset, count);
			if (count > 0)
			{
				this.WriteStartArray(prefix, localName, namespaceUri, count);
				this.writer.WriteTimeSpanArray(array, offset, count);
				this.WriteEndArray();
			}
		}

		// Token: 0x0600037D RID: 893 RVA: 0x00011870 File Offset: 0x0000FA70
		public override void WriteArray(string prefix, XmlDictionaryString localName, XmlDictionaryString namespaceUri, TimeSpan[] array, int offset, int count)
		{
			if (base.Signing)
			{
				base.WriteArray(prefix, localName, namespaceUri, array, offset, count);
				return;
			}
			this.CheckArray(array, offset, count);
			if (count > 0)
			{
				this.WriteStartArray(prefix, localName, namespaceUri, count);
				this.writer.WriteTimeSpanArray(array, offset, count);
				this.WriteEndArray();
			}
		}

		// Token: 0x0600037E RID: 894 RVA: 0x000118C8 File Offset: 0x0000FAC8
		public XmlBinaryWriter()
		{
		}

		// Token: 0x04000220 RID: 544
		private XmlBinaryNodeWriter writer;

		// Token: 0x04000221 RID: 545
		private char[] chars;

		// Token: 0x04000222 RID: 546
		private byte[] bytes;
	}
}
