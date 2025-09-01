using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	/// <summary>Wraps an XSD <see langword="hexBinary" /> type.</summary>
	// Token: 0x020005E9 RID: 1513
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapHexBinary : ISoapXsd
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapHexBinary" /> class.</summary>
		// Token: 0x06003966 RID: 14694 RVA: 0x000CB994 File Offset: 0x000C9B94
		public SoapHexBinary()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapHexBinary" /> class.</summary>
		/// <param name="value">A <see cref="T:System.Byte" /> array that contains a hexadecimal number.</param>
		// Token: 0x06003967 RID: 14695 RVA: 0x000CB9A7 File Offset: 0x000C9BA7
		public SoapHexBinary(byte[] value)
		{
			this._value = value;
		}

		/// <summary>Gets or sets the hexadecimal representation of a number.</summary>
		/// <returns>A <see cref="T:System.Byte" /> array containing the hexadecimal representation of a number.</returns>
		// Token: 0x17000845 RID: 2117
		// (get) Token: 0x06003968 RID: 14696 RVA: 0x000CB9C1 File Offset: 0x000C9BC1
		// (set) Token: 0x06003969 RID: 14697 RVA: 0x000CB9C9 File Offset: 0x000C9BC9
		public byte[] Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
			}
		}

		/// <summary>Gets the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> indicating the XSD of the current SOAP type.</returns>
		// Token: 0x17000846 RID: 2118
		// (get) Token: 0x0600396A RID: 14698 RVA: 0x000CB9D2 File Offset: 0x000C9BD2
		public static string XsdType
		{
			get
			{
				return "hexBinary";
			}
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x0600396B RID: 14699 RVA: 0x000CB9D9 File Offset: 0x000C9BD9
		public string GetXsdType()
		{
			return SoapHexBinary.XsdType;
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> into a <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapHexBinary" /> object.</summary>
		/// <param name="value">The <see langword="String" /> to convert.</param>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapHexBinary" /> object that is obtained from <paramref name="value" />.</returns>
		// Token: 0x0600396C RID: 14700 RVA: 0x000CB9E0 File Offset: 0x000C9BE0
		public static SoapHexBinary Parse(string value)
		{
			return new SoapHexBinary(SoapHexBinary.FromBinHexString(value));
		}

		// Token: 0x0600396D RID: 14701 RVA: 0x000CB9F0 File Offset: 0x000C9BF0
		internal static byte[] FromBinHexString(string value)
		{
			char[] array = value.ToCharArray();
			byte[] array2 = new byte[array.Length / 2 + array.Length % 2];
			int num = array.Length;
			if (num % 2 != 0)
			{
				throw SoapHexBinary.CreateInvalidValueException(value);
			}
			int num2 = 0;
			for (int i = 0; i < num - 1; i += 2)
			{
				array2[num2] = SoapHexBinary.FromHex(array[i], value);
				byte[] array3 = array2;
				int num3 = num2;
				array3[num3] = (byte)(array3[num3] << 4);
				byte[] array4 = array2;
				int num4 = num2;
				array4[num4] += SoapHexBinary.FromHex(array[i + 1], value);
				num2++;
			}
			return array2;
		}

		// Token: 0x0600396E RID: 14702 RVA: 0x000CBA70 File Offset: 0x000C9C70
		private static byte FromHex(char hexDigit, string value)
		{
			byte result;
			try
			{
				result = byte.Parse(hexDigit.ToString(), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
			}
			catch (FormatException)
			{
				throw SoapHexBinary.CreateInvalidValueException(value);
			}
			return result;
		}

		// Token: 0x0600396F RID: 14703 RVA: 0x000CBAB0 File Offset: 0x000C9CB0
		private static Exception CreateInvalidValueException(string value)
		{
			return new RemotingException(string.Format(CultureInfo.InvariantCulture, "Invalid value '{0}' for xsd:{1}.", value, SoapHexBinary.XsdType));
		}

		/// <summary>Returns <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapHexBinary.Value" /> as a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that is obtained from <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapHexBinary.Value" />.</returns>
		// Token: 0x06003970 RID: 14704 RVA: 0x000CBACC File Offset: 0x000C9CCC
		public override string ToString()
		{
			this.sb.Length = 0;
			foreach (byte b in this._value)
			{
				this.sb.Append(b.ToString("X2"));
			}
			return this.sb.ToString();
		}

		// Token: 0x04002629 RID: 9769
		private byte[] _value;

		// Token: 0x0400262A RID: 9770
		private StringBuilder sb = new StringBuilder();
	}
}
