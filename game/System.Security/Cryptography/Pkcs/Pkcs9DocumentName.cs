using System;
using Internal.Cryptography;

namespace System.Security.Cryptography.Pkcs
{
	/// <summary>The <see cref="T:System.Security.Cryptography.Pkcs.Pkcs9DocumentName" /> class defines the name of a CMS/PKCS #7 message.</summary>
	// Token: 0x0200007C RID: 124
	public sealed class Pkcs9DocumentName : Pkcs9AttributeObject
	{
		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.Pkcs9DocumentName.#ctor" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.Pkcs9DocumentName" /> class.</summary>
		// Token: 0x0600040C RID: 1036 RVA: 0x00012AA9 File Offset: 0x00010CA9
		public Pkcs9DocumentName() : base(new Oid("1.3.6.1.4.1.311.88.2.1"))
		{
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.Pkcs9DocumentName.#ctor(System.String)" /> constructor creates an instance of the  <see cref="T:System.Security.Cryptography.Pkcs.Pkcs9DocumentName" /> class by using the specified name for the CMS/PKCS #7 message.</summary>
		/// <param name="documentName">A  <see cref="T:System.String" /> object that specifies the name for the CMS/PKCS #7 message.</param>
		// Token: 0x0600040D RID: 1037 RVA: 0x00012ABB File Offset: 0x00010CBB
		public Pkcs9DocumentName(string documentName) : base("1.3.6.1.4.1.311.88.2.1", Pkcs9DocumentName.Encode(documentName))
		{
			this._lazyDocumentName = documentName;
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.Pkcs9DocumentName.#ctor(System.Byte[])" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.Pkcs9DocumentName" /> class by using the specified array of byte values as the encoded name of the content of a CMS/PKCS #7 message.</summary>
		/// <param name="encodedDocumentName">An array of byte values that specifies the encoded name of the CMS/PKCS #7 message.</param>
		// Token: 0x0600040E RID: 1038 RVA: 0x00012AD7 File Offset: 0x00010CD7
		public Pkcs9DocumentName(byte[] encodedDocumentName) : base("1.3.6.1.4.1.311.88.2.1", encodedDocumentName)
		{
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.Pkcs9DocumentName.DocumentName" /> property retrieves the document name.</summary>
		/// <returns>A <see cref="T:System.String" /> object that contains the document name.</returns>
		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x0600040F RID: 1039 RVA: 0x00012AE8 File Offset: 0x00010CE8
		public string DocumentName
		{
			get
			{
				string result;
				if ((result = this._lazyDocumentName) == null)
				{
					result = (this._lazyDocumentName = Pkcs9DocumentName.Decode(base.RawData));
				}
				return result;
			}
		}

		/// <summary>Copies information from an <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object.</summary>
		/// <param name="asnEncodedData">The <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object from which to copy information.</param>
		// Token: 0x06000410 RID: 1040 RVA: 0x00012B17 File Offset: 0x00010D17
		public override void CopyFrom(AsnEncodedData asnEncodedData)
		{
			base.CopyFrom(asnEncodedData);
			this._lazyDocumentName = null;
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x00012A65 File Offset: 0x00010C65
		private static string Decode(byte[] rawData)
		{
			if (rawData == null)
			{
				return null;
			}
			return PkcsPal.Instance.DecodeOctetString(rawData).OctetStringToUnicode();
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x00012B2C File Offset: 0x00010D2C
		private static byte[] Encode(string documentName)
		{
			if (documentName == null)
			{
				throw new ArgumentNullException("documentName");
			}
			byte[] octets = documentName.UnicodeToOctetString();
			return PkcsPal.Instance.EncodeOctetString(octets);
		}

		// Token: 0x04000294 RID: 660
		private volatile string _lazyDocumentName;
	}
}
