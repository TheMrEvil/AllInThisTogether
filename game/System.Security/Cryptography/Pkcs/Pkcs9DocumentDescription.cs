using System;
using Internal.Cryptography;

namespace System.Security.Cryptography.Pkcs
{
	/// <summary>The <see cref="T:System.Security.Cryptography.Pkcs.Pkcs9DocumentDescription" /> class defines the description of the content of a CMS/PKCS #7 message.</summary>
	// Token: 0x0200007B RID: 123
	public sealed class Pkcs9DocumentDescription : Pkcs9AttributeObject
	{
		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.Pkcs9DocumentDescription.#ctor" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.Pkcs9DocumentDescription" /> class.</summary>
		// Token: 0x06000405 RID: 1029 RVA: 0x000129E8 File Offset: 0x00010BE8
		public Pkcs9DocumentDescription() : base(new Oid("1.3.6.1.4.1.311.88.2.2"))
		{
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.Pkcs9DocumentDescription.#ctor(System.String)" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.Pkcs9DocumentDescription" /> class by using the specified description of the content of a CMS/PKCS #7 message.</summary>
		/// <param name="documentDescription">An instance of the <see cref="T:System.String" /> class that specifies the description for the CMS/PKCS #7 message.</param>
		// Token: 0x06000406 RID: 1030 RVA: 0x000129FA File Offset: 0x00010BFA
		public Pkcs9DocumentDescription(string documentDescription) : base("1.3.6.1.4.1.311.88.2.2", Pkcs9DocumentDescription.Encode(documentDescription))
		{
			this._lazyDocumentDescription = documentDescription;
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.Pkcs9DocumentDescription.#ctor(System.Byte[])" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.Pkcs9DocumentDescription" /> class by using the specified array of byte values as the encoded description of the content of a CMS/PKCS #7 message.</summary>
		/// <param name="encodedDocumentDescription">An array of byte values that specifies the encoded description of the CMS/PKCS #7 message.</param>
		// Token: 0x06000407 RID: 1031 RVA: 0x00012A16 File Offset: 0x00010C16
		public Pkcs9DocumentDescription(byte[] encodedDocumentDescription) : base("1.3.6.1.4.1.311.88.2.2", encodedDocumentDescription)
		{
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.Pkcs9DocumentDescription.DocumentDescription" /> property retrieves the document description.</summary>
		/// <returns>A <see cref="T:System.String" /> object that contains the document description.</returns>
		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000408 RID: 1032 RVA: 0x00012A24 File Offset: 0x00010C24
		public string DocumentDescription
		{
			get
			{
				string result;
				if ((result = this._lazyDocumentDescription) == null)
				{
					result = (this._lazyDocumentDescription = Pkcs9DocumentDescription.Decode(base.RawData));
				}
				return result;
			}
		}

		/// <summary>Copies information from an <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object.</summary>
		/// <param name="asnEncodedData">The <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object from which to copy information.</param>
		// Token: 0x06000409 RID: 1033 RVA: 0x00012A53 File Offset: 0x00010C53
		public override void CopyFrom(AsnEncodedData asnEncodedData)
		{
			base.CopyFrom(asnEncodedData);
			this._lazyDocumentDescription = null;
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x00012A65 File Offset: 0x00010C65
		private static string Decode(byte[] rawData)
		{
			if (rawData == null)
			{
				return null;
			}
			return PkcsPal.Instance.DecodeOctetString(rawData).OctetStringToUnicode();
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x00012A7C File Offset: 0x00010C7C
		private static byte[] Encode(string documentDescription)
		{
			if (documentDescription == null)
			{
				throw new ArgumentNullException("documentDescription");
			}
			byte[] octets = documentDescription.UnicodeToOctetString();
			return PkcsPal.Instance.EncodeOctetString(octets);
		}

		// Token: 0x04000293 RID: 659
		private volatile string _lazyDocumentDescription;
	}
}
