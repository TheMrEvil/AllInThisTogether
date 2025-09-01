using System;
using System.Text;
using Mono.Security;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Defines the collection of object identifiers (OIDs) that indicates the applications that use the key. This class cannot be inherited.</summary>
	// Token: 0x020002E1 RID: 737
	public sealed class X509EnhancedKeyUsageExtension : X509Extension
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509EnhancedKeyUsageExtension" /> class.</summary>
		// Token: 0x0600176E RID: 5998 RVA: 0x0005CD5F File Offset: 0x0005AF5F
		public X509EnhancedKeyUsageExtension()
		{
			this._oid = new Oid("2.5.29.37", "Enhanced Key Usage");
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509EnhancedKeyUsageExtension" /> class using an <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object and a value that identifies whether the extension is critical.</summary>
		/// <param name="encodedEnhancedKeyUsages">The encoded data to use to create the extension.</param>
		/// <param name="critical">
		///   <see langword="true" /> if the extension is critical; otherwise, <see langword="false" />.</param>
		// Token: 0x0600176F RID: 5999 RVA: 0x0005CD7C File Offset: 0x0005AF7C
		public X509EnhancedKeyUsageExtension(AsnEncodedData encodedEnhancedKeyUsages, bool critical)
		{
			this._oid = new Oid("2.5.29.37", "Enhanced Key Usage");
			this._raw = encodedEnhancedKeyUsages.RawData;
			base.Critical = critical;
			this._status = this.Decode(base.RawData);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509EnhancedKeyUsageExtension" /> class using an <see cref="T:System.Security.Cryptography.OidCollection" /> and a value that identifies whether the extension is critical.</summary>
		/// <param name="enhancedKeyUsages">An <see cref="T:System.Security.Cryptography.OidCollection" /> collection.</param>
		/// <param name="critical">
		///   <see langword="true" /> if the extension is critical; otherwise, <see langword="false" />.</param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The specified <see cref="T:System.Security.Cryptography.OidCollection" /> contains one or more corrupt values.</exception>
		// Token: 0x06001770 RID: 6000 RVA: 0x0005CDCC File Offset: 0x0005AFCC
		public X509EnhancedKeyUsageExtension(OidCollection enhancedKeyUsages, bool critical)
		{
			if (enhancedKeyUsages == null)
			{
				throw new ArgumentNullException("enhancedKeyUsages");
			}
			this._oid = new Oid("2.5.29.37", "Enhanced Key Usage");
			base.Critical = critical;
			this._enhKeyUsage = new OidCollection();
			foreach (Oid oid in enhancedKeyUsages)
			{
				this._enhKeyUsage.Add(oid);
			}
			base.RawData = this.Encode();
		}

		/// <summary>Gets the collection of object identifiers (OIDs) that indicate the applications that use the key.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.OidCollection" /> object indicating the applications that use the key.</returns>
		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x06001771 RID: 6001 RVA: 0x0005CE48 File Offset: 0x0005B048
		public OidCollection EnhancedKeyUsages
		{
			get
			{
				AsnDecodeStatus status = this._status;
				if (status == AsnDecodeStatus.Ok || status == AsnDecodeStatus.InformationNotAvailable)
				{
					OidCollection oidCollection = new OidCollection();
					if (this._enhKeyUsage != null)
					{
						foreach (Oid oid in this._enhKeyUsage)
						{
							oidCollection.Add(oid);
						}
					}
					return oidCollection;
				}
				throw new CryptographicException("Badly encoded extension.");
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509EnhancedKeyUsageExtension" /> class using an <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object.</summary>
		/// <param name="asnEncodedData">The encoded data to use to create the extension.</param>
		// Token: 0x06001772 RID: 6002 RVA: 0x0005CEA4 File Offset: 0x0005B0A4
		public override void CopyFrom(AsnEncodedData asnEncodedData)
		{
			if (asnEncodedData == null)
			{
				throw new ArgumentNullException("encodedData");
			}
			X509Extension x509Extension = asnEncodedData as X509Extension;
			if (x509Extension == null)
			{
				throw new ArgumentException(Locale.GetText("Wrong type."), "asnEncodedData");
			}
			if (x509Extension._oid == null)
			{
				this._oid = new Oid("2.5.29.37", "Enhanced Key Usage");
			}
			else
			{
				this._oid = new Oid(x509Extension._oid);
			}
			base.RawData = x509Extension.RawData;
			base.Critical = x509Extension.Critical;
			this._status = this.Decode(base.RawData);
		}

		// Token: 0x06001773 RID: 6003 RVA: 0x0005CF38 File Offset: 0x0005B138
		internal AsnDecodeStatus Decode(byte[] extension)
		{
			if (extension == null || extension.Length == 0)
			{
				return AsnDecodeStatus.BadAsn;
			}
			if (extension[0] != 48)
			{
				return AsnDecodeStatus.BadTag;
			}
			if (this._enhKeyUsage == null)
			{
				this._enhKeyUsage = new OidCollection();
			}
			try
			{
				ASN1 asn = new ASN1(extension);
				if (asn.Tag != 48)
				{
					throw new CryptographicException(Locale.GetText("Invalid ASN.1 Tag"));
				}
				for (int i = 0; i < asn.Count; i++)
				{
					this._enhKeyUsage.Add(new Oid(ASN1Convert.ToOid(asn[i])));
				}
			}
			catch
			{
				return AsnDecodeStatus.BadAsn;
			}
			return AsnDecodeStatus.Ok;
		}

		// Token: 0x06001774 RID: 6004 RVA: 0x0005CFD4 File Offset: 0x0005B1D4
		internal byte[] Encode()
		{
			ASN1 asn = new ASN1(48);
			foreach (Oid oid in this._enhKeyUsage)
			{
				asn.Add(ASN1Convert.FromOid(oid.Value));
			}
			return asn.GetBytes();
		}

		// Token: 0x06001775 RID: 6005 RVA: 0x0005D020 File Offset: 0x0005B220
		internal override string ToString(bool multiLine)
		{
			switch (this._status)
			{
			case AsnDecodeStatus.BadAsn:
				return string.Empty;
			case AsnDecodeStatus.BadTag:
			case AsnDecodeStatus.BadLength:
				return base.FormatUnkownData(this._raw);
			case AsnDecodeStatus.InformationNotAvailable:
				return "Information Not Available";
			default:
			{
				if (this._oid.Value != "2.5.29.37")
				{
					return string.Format("Unknown Key Usage ({0})", this._oid.Value);
				}
				if (this._enhKeyUsage.Count == 0)
				{
					return "Information Not Available";
				}
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < this._enhKeyUsage.Count; i++)
				{
					Oid oid = this._enhKeyUsage[i];
					if (oid.Value == "1.3.6.1.5.5.7.3.1")
					{
						stringBuilder.Append("Server Authentication (");
					}
					else
					{
						stringBuilder.Append("Unknown Key Usage (");
					}
					stringBuilder.Append(oid.Value);
					stringBuilder.Append(")");
					if (multiLine)
					{
						stringBuilder.Append(Environment.NewLine);
					}
					else if (i != this._enhKeyUsage.Count - 1)
					{
						stringBuilder.Append(", ");
					}
				}
				return stringBuilder.ToString();
			}
			}
		}

		// Token: 0x04000D24 RID: 3364
		internal const string oid = "2.5.29.37";

		// Token: 0x04000D25 RID: 3365
		internal const string friendlyName = "Enhanced Key Usage";

		// Token: 0x04000D26 RID: 3366
		private OidCollection _enhKeyUsage;

		// Token: 0x04000D27 RID: 3367
		private AsnDecodeStatus _status;
	}
}
