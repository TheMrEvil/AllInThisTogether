using System;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Mono.Security;
using Mono.Security.Cryptography;

namespace System.Security.Cryptography
{
	/// <summary>Represents Abstract Syntax Notation One (ASN.1)-encoded data.</summary>
	// Token: 0x020002BA RID: 698
	public class AsnEncodedData
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.AsnEncodedData" /> class.</summary>
		// Token: 0x060015E0 RID: 5600 RVA: 0x0000219B File Offset: 0x0000039B
		protected AsnEncodedData()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.AsnEncodedData" /> class using a byte array.</summary>
		/// <param name="oid">A string that represents <see cref="T:System.Security.Cryptography.Oid" /> information.</param>
		/// <param name="rawData">A byte array that contains Abstract Syntax Notation One (ASN.1)-encoded data.</param>
		// Token: 0x060015E1 RID: 5601 RVA: 0x00057950 File Offset: 0x00055B50
		public AsnEncodedData(string oid, byte[] rawData)
		{
			this._oid = new Oid(oid);
			this.RawData = rawData;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.AsnEncodedData" /> class using an <see cref="T:System.Security.Cryptography.Oid" /> object and a byte array.</summary>
		/// <param name="oid">An <see cref="T:System.Security.Cryptography.Oid" /> object.</param>
		/// <param name="rawData">A byte array that contains Abstract Syntax Notation One (ASN.1)-encoded data.</param>
		// Token: 0x060015E2 RID: 5602 RVA: 0x0005796B File Offset: 0x00055B6B
		public AsnEncodedData(Oid oid, byte[] rawData)
		{
			this.Oid = oid;
			this.RawData = rawData;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.AsnEncodedData" /> class using an instance of the <see cref="T:System.Security.Cryptography.AsnEncodedData" /> class.</summary>
		/// <param name="asnEncodedData">An instance of the <see cref="T:System.Security.Cryptography.AsnEncodedData" /> class.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asnEncodedData" /> is <see langword="null" />.</exception>
		// Token: 0x060015E3 RID: 5603 RVA: 0x00057981 File Offset: 0x00055B81
		public AsnEncodedData(AsnEncodedData asnEncodedData)
		{
			if (asnEncodedData == null)
			{
				throw new ArgumentNullException("asnEncodedData");
			}
			if (asnEncodedData._oid != null)
			{
				this.Oid = new Oid(asnEncodedData._oid);
			}
			this.RawData = asnEncodedData._raw;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.AsnEncodedData" /> class using a byte array.</summary>
		/// <param name="rawData">A byte array that contains Abstract Syntax Notation One (ASN.1)-encoded data.</param>
		// Token: 0x060015E4 RID: 5604 RVA: 0x000579BC File Offset: 0x00055BBC
		public AsnEncodedData(byte[] rawData)
		{
			this.RawData = rawData;
		}

		/// <summary>Gets or sets the <see cref="T:System.Security.Cryptography.Oid" /> value for an <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.Oid" /> object.</returns>
		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x060015E5 RID: 5605 RVA: 0x000579CB File Offset: 0x00055BCB
		// (set) Token: 0x060015E6 RID: 5606 RVA: 0x000579D3 File Offset: 0x00055BD3
		public Oid Oid
		{
			get
			{
				return this._oid;
			}
			set
			{
				if (value == null)
				{
					this._oid = null;
					return;
				}
				this._oid = new Oid(value);
			}
		}

		/// <summary>Gets or sets the Abstract Syntax Notation One (ASN.1)-encoded data represented in a byte array.</summary>
		/// <returns>A byte array that represents the Abstract Syntax Notation One (ASN.1)-encoded data.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value is <see langword="null" />.</exception>
		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x060015E7 RID: 5607 RVA: 0x000579EC File Offset: 0x00055BEC
		// (set) Token: 0x060015E8 RID: 5608 RVA: 0x000579F4 File Offset: 0x00055BF4
		public byte[] RawData
		{
			get
			{
				return this._raw;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("RawData");
				}
				this._raw = (byte[])value.Clone();
			}
		}

		/// <summary>Copies information from an <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object.</summary>
		/// <param name="asnEncodedData">The <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object to base the new object on.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asnEncodedData" /> is <see langword="null" />.</exception>
		// Token: 0x060015E9 RID: 5609 RVA: 0x00057A15 File Offset: 0x00055C15
		public virtual void CopyFrom(AsnEncodedData asnEncodedData)
		{
			if (asnEncodedData == null)
			{
				throw new ArgumentNullException("asnEncodedData");
			}
			if (asnEncodedData._oid == null)
			{
				this.Oid = null;
			}
			else
			{
				this.Oid = new Oid(asnEncodedData._oid);
			}
			this.RawData = asnEncodedData._raw;
		}

		/// <summary>Returns a formatted version of the Abstract Syntax Notation One (ASN.1)-encoded data as a string.</summary>
		/// <param name="multiLine">
		///   <see langword="true" /> if the return string should contain carriage returns; otherwise, <see langword="false" />.</param>
		/// <returns>A formatted string that represents the Abstract Syntax Notation One (ASN.1)-encoded data.</returns>
		// Token: 0x060015EA RID: 5610 RVA: 0x00057A53 File Offset: 0x00055C53
		public virtual string Format(bool multiLine)
		{
			if (this._raw == null)
			{
				return string.Empty;
			}
			if (this._oid == null)
			{
				return this.Default(multiLine);
			}
			return this.ToString(multiLine);
		}

		// Token: 0x060015EB RID: 5611 RVA: 0x00057A7C File Offset: 0x00055C7C
		internal virtual string ToString(bool multiLine)
		{
			string value = this._oid.Value;
			if (value == "2.5.29.19")
			{
				return this.BasicConstraintsExtension(multiLine);
			}
			if (value == "2.5.29.37")
			{
				return this.EnhancedKeyUsageExtension(multiLine);
			}
			if (value == "2.5.29.15")
			{
				return this.KeyUsageExtension(multiLine);
			}
			if (value == "2.5.29.14")
			{
				return this.SubjectKeyIdentifierExtension(multiLine);
			}
			if (value == "2.5.29.17")
			{
				return this.SubjectAltName(multiLine);
			}
			if (!(value == "2.16.840.1.113730.1.1"))
			{
				return this.Default(multiLine);
			}
			return this.NetscapeCertType(multiLine);
		}

		// Token: 0x060015EC RID: 5612 RVA: 0x00057B1C File Offset: 0x00055D1C
		internal string Default(bool multiLine)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < this._raw.Length; i++)
			{
				stringBuilder.Append(this._raw[i].ToString("x2"));
				if (i != this._raw.Length - 1)
				{
					stringBuilder.Append(" ");
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060015ED RID: 5613 RVA: 0x00057B80 File Offset: 0x00055D80
		internal string BasicConstraintsExtension(bool multiLine)
		{
			string result;
			try
			{
				result = new X509BasicConstraintsExtension(this, false).ToString(multiLine);
			}
			catch
			{
				result = string.Empty;
			}
			return result;
		}

		// Token: 0x060015EE RID: 5614 RVA: 0x00057BB8 File Offset: 0x00055DB8
		internal string EnhancedKeyUsageExtension(bool multiLine)
		{
			string result;
			try
			{
				result = new X509EnhancedKeyUsageExtension(this, false).ToString(multiLine);
			}
			catch
			{
				result = string.Empty;
			}
			return result;
		}

		// Token: 0x060015EF RID: 5615 RVA: 0x00057BF0 File Offset: 0x00055DF0
		internal string KeyUsageExtension(bool multiLine)
		{
			string result;
			try
			{
				result = new X509KeyUsageExtension(this, false).ToString(multiLine);
			}
			catch
			{
				result = string.Empty;
			}
			return result;
		}

		// Token: 0x060015F0 RID: 5616 RVA: 0x00057C28 File Offset: 0x00055E28
		internal string SubjectKeyIdentifierExtension(bool multiLine)
		{
			string result;
			try
			{
				result = new X509SubjectKeyIdentifierExtension(this, false).ToString(multiLine);
			}
			catch
			{
				result = string.Empty;
			}
			return result;
		}

		// Token: 0x060015F1 RID: 5617 RVA: 0x00057C60 File Offset: 0x00055E60
		internal string SubjectAltName(bool multiLine)
		{
			if (this._raw.Length < 5)
			{
				return "Information Not Available";
			}
			string result;
			try
			{
				ASN1 asn = new ASN1(this._raw);
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < asn.Count; i++)
				{
					ASN1 asn2 = asn[i];
					byte tag = asn2.Tag;
					string value;
					string value2;
					if (tag != 129)
					{
						if (tag != 130)
						{
							value = string.Format("Unknown ({0})=", asn2.Tag);
							value2 = CryptoConvert.ToHex(asn2.Value);
						}
						else
						{
							value = "DNS Name=";
							value2 = Encoding.ASCII.GetString(asn2.Value);
						}
					}
					else
					{
						value = "RFC822 Name=";
						value2 = Encoding.ASCII.GetString(asn2.Value);
					}
					stringBuilder.Append(value);
					stringBuilder.Append(value2);
					if (multiLine)
					{
						stringBuilder.Append(Environment.NewLine);
					}
					else if (i < asn.Count - 1)
					{
						stringBuilder.Append(", ");
					}
				}
				result = stringBuilder.ToString();
			}
			catch
			{
				result = string.Empty;
			}
			return result;
		}

		// Token: 0x060015F2 RID: 5618 RVA: 0x00057D8C File Offset: 0x00055F8C
		internal string NetscapeCertType(bool multiLine)
		{
			if (this._raw.Length < 4 || this._raw[0] != 3 || this._raw[1] != 2)
			{
				return "Information Not Available";
			}
			int num = this._raw[3] >> (int)this._raw[2] << (int)this._raw[2];
			StringBuilder stringBuilder = new StringBuilder();
			if ((num & 128) == 128)
			{
				stringBuilder.Append("SSL Client Authentication");
			}
			if ((num & 64) == 64)
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append("SSL Server Authentication");
			}
			if ((num & 32) == 32)
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append("SMIME");
			}
			if ((num & 16) == 16)
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append("Signature");
			}
			if ((num & 8) == 8)
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append("Unknown cert type");
			}
			if ((num & 4) == 4)
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append("SSL CA");
			}
			if ((num & 2) == 2)
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append("SMIME CA");
			}
			if ((num & 1) == 1)
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append("Signature CA");
			}
			stringBuilder.AppendFormat(" ({0})", num.ToString("x2"));
			return stringBuilder.ToString();
		}

		// Token: 0x04000C4B RID: 3147
		internal Oid _oid;

		// Token: 0x04000C4C RID: 3148
		internal byte[] _raw;
	}
}
