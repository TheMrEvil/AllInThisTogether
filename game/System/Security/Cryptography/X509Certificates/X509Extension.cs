using System;
using System.Text;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Represents an X509 extension.</summary>
	// Token: 0x020002E2 RID: 738
	public class X509Extension : AsnEncodedData
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Extension" /> class.</summary>
		// Token: 0x06001776 RID: 6006 RVA: 0x0005D14E File Offset: 0x0005B34E
		protected X509Extension()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Extension" /> class.</summary>
		/// <param name="encodedExtension">The encoded data to be used to create the extension.</param>
		/// <param name="critical">
		///   <see langword="true" /> if the extension is critical; otherwise <see langword="false" />.</param>
		// Token: 0x06001777 RID: 6007 RVA: 0x0005D156 File Offset: 0x0005B356
		public X509Extension(AsnEncodedData encodedExtension, bool critical)
		{
			if (encodedExtension.Oid == null)
			{
				throw new ArgumentNullException("encodedExtension.Oid");
			}
			base.Oid = encodedExtension.Oid;
			base.RawData = encodedExtension.RawData;
			this._critical = critical;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Extension" /> class.</summary>
		/// <param name="oid">The object identifier used to identify the extension.</param>
		/// <param name="rawData">The encoded data used to create the extension.</param>
		/// <param name="critical">
		///   <see langword="true" /> if the extension is critical; otherwise <see langword="false" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="oid" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="oid" /> is an empty string ("").</exception>
		// Token: 0x06001778 RID: 6008 RVA: 0x0005D190 File Offset: 0x0005B390
		public X509Extension(Oid oid, byte[] rawData, bool critical)
		{
			if (oid == null)
			{
				throw new ArgumentNullException("oid");
			}
			base.Oid = oid;
			base.RawData = rawData;
			this._critical = critical;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Extension" /> class.</summary>
		/// <param name="oid">A string representing the object identifier.</param>
		/// <param name="rawData">The encoded data used to create the extension.</param>
		/// <param name="critical">
		///   <see langword="true" /> if the extension is critical; otherwise <see langword="false" />.</param>
		// Token: 0x06001779 RID: 6009 RVA: 0x0005D1BB File Offset: 0x0005B3BB
		public X509Extension(string oid, byte[] rawData, bool critical) : base(oid, rawData)
		{
			this._critical = critical;
		}

		/// <summary>Gets a Boolean value indicating whether the extension is critical.</summary>
		/// <returns>
		///   <see langword="true" /> if the extension is critical; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x0600177A RID: 6010 RVA: 0x0005D1CC File Offset: 0x0005B3CC
		// (set) Token: 0x0600177B RID: 6011 RVA: 0x0005D1D4 File Offset: 0x0005B3D4
		public bool Critical
		{
			get
			{
				return this._critical;
			}
			set
			{
				this._critical = value;
			}
		}

		/// <summary>Copies the extension properties of the specified <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object.</summary>
		/// <param name="asnEncodedData">The <see cref="T:System.Security.Cryptography.AsnEncodedData" /> to be copied.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asnEncodedData" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asnEncodedData" /> does not have a valid X.509 extension.</exception>
		// Token: 0x0600177C RID: 6012 RVA: 0x0005D1E0 File Offset: 0x0005B3E0
		public override void CopyFrom(AsnEncodedData asnEncodedData)
		{
			if (asnEncodedData == null)
			{
				throw new ArgumentNullException("encodedData");
			}
			X509Extension x509Extension = asnEncodedData as X509Extension;
			if (x509Extension == null)
			{
				throw new ArgumentException(Locale.GetText("Expected a X509Extension instance."));
			}
			base.CopyFrom(asnEncodedData);
			this._critical = x509Extension.Critical;
		}

		// Token: 0x0600177D RID: 6013 RVA: 0x0005D228 File Offset: 0x0005B428
		internal string FormatUnkownData(byte[] data)
		{
			if (data == null || data.Length == 0)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < data.Length; i++)
			{
				stringBuilder.Append(data[i].ToString("X2"));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000D28 RID: 3368
		private bool _critical;
	}
}
