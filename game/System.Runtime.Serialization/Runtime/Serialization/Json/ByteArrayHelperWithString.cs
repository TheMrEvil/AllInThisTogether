using System;
using System.Globalization;
using System.Xml;

namespace System.Runtime.Serialization.Json
{
	// Token: 0x02000164 RID: 356
	internal class ByteArrayHelperWithString : ArrayHelper<string, byte>
	{
		// Token: 0x060012A8 RID: 4776 RVA: 0x00049420 File Offset: 0x00047620
		internal void WriteArray(XmlWriter writer, byte[] array, int offset, int count)
		{
			XmlJsonReader.CheckArray(array, offset, count);
			writer.WriteAttributeString(string.Empty, "type", string.Empty, "array");
			for (int i = 0; i < count; i++)
			{
				writer.WriteStartElement("item", string.Empty);
				writer.WriteAttributeString(string.Empty, "type", string.Empty, "number");
				writer.WriteValue((int)array[offset + i]);
				writer.WriteEndElement();
			}
		}

		// Token: 0x060012A9 RID: 4777 RVA: 0x00049498 File Offset: 0x00047698
		protected override int ReadArray(XmlDictionaryReader reader, string localName, string namespaceUri, byte[] array, int offset, int count)
		{
			XmlJsonReader.CheckArray(array, offset, count);
			int num = 0;
			while (num < count && reader.IsStartElement("item", string.Empty))
			{
				array[offset + num] = this.ToByte(reader.ReadElementContentAsInt());
				num++;
			}
			return num;
		}

		// Token: 0x060012AA RID: 4778 RVA: 0x000494E3 File Offset: 0x000476E3
		protected override void WriteArray(XmlDictionaryWriter writer, string prefix, string localName, string namespaceUri, byte[] array, int offset, int count)
		{
			this.WriteArray(writer, array, offset, count);
		}

		// Token: 0x060012AB RID: 4779 RVA: 0x000494F2 File Offset: 0x000476F2
		private void ThrowConversionException(string value, string type)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("The value '{0}' cannot be parsed as the type '{1}'.", new object[]
			{
				value,
				type
			})));
		}

		// Token: 0x060012AC RID: 4780 RVA: 0x00049516 File Offset: 0x00047716
		private byte ToByte(int value)
		{
			if (value < 0 || value > 255)
			{
				this.ThrowConversionException(value.ToString(NumberFormatInfo.CurrentInfo), "Byte");
			}
			return (byte)value;
		}

		// Token: 0x060012AD RID: 4781 RVA: 0x0004953D File Offset: 0x0004773D
		public ByteArrayHelperWithString()
		{
		}

		// Token: 0x060012AE RID: 4782 RVA: 0x00049545 File Offset: 0x00047745
		// Note: this type is marked as 'beforefieldinit'.
		static ByteArrayHelperWithString()
		{
		}

		// Token: 0x04000970 RID: 2416
		public static readonly ByteArrayHelperWithString Instance = new ByteArrayHelperWithString();
	}
}
