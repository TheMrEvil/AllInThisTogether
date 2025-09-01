using System;
using System.Runtime.CompilerServices;
using Internal.Cryptography;

namespace System.Security.Cryptography.Pkcs
{
	/// <summary>The <see cref="T:System.Security.Cryptography.Pkcs.ContentInfo" /> class represents the CMS/PKCS #7 ContentInfo data structure as defined in the CMS/PKCS #7 standards document. This data structure is the basis for all CMS/PKCS #7 messages.</summary>
	// Token: 0x02000074 RID: 116
	public sealed class ContentInfo
	{
		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.ContentInfo.#ctor(System.Byte[])" /> constructor  creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.ContentInfo" /> class by using an array of byte values as the data and a default <paramref name="object identifier" /> (OID) that represents the content type.</summary>
		/// <param name="content">An array of byte values that represents the data from which to create the <see cref="T:System.Security.Cryptography.Pkcs.ContentInfo" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">A null reference  was passed to a method that does not accept it as a valid argument.</exception>
		// Token: 0x060003CA RID: 970 RVA: 0x00012223 File Offset: 0x00010423
		public ContentInfo(byte[] content) : this(Oid.FromOidValue("1.2.840.113549.1.7.1", OidGroup.ExtensionOrAttribute), content)
		{
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.ContentInfo.#ctor(System.Security.Cryptography.Oid,System.Byte[])" /> constructor  creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.ContentInfo" /> class by using the specified content type and an array of byte values as the data.</summary>
		/// <param name="contentType">An <see cref="T:System.Security.Cryptography.Oid" /> object that contains an object identifier (OID) that specifies the content type of the content. This can be data, digestedData, encryptedData, envelopedData, hashedData, signedAndEnvelopedData, or signedData.  For more information, see  Remarks.</param>
		/// <param name="content">An array of byte values that represents the data from which to create the <see cref="T:System.Security.Cryptography.Pkcs.ContentInfo" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">A null reference  was passed to a method that does not accept it as a valid argument.</exception>
		// Token: 0x060003CB RID: 971 RVA: 0x00012237 File Offset: 0x00010437
		public ContentInfo(Oid contentType, byte[] content)
		{
			if (contentType == null)
			{
				throw new ArgumentNullException("contentType");
			}
			if (content == null)
			{
				throw new ArgumentNullException("content");
			}
			this.ContentType = contentType;
			this.Content = content;
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.ContentInfo.ContentType" /> property  retrieves the <see cref="T:System.Security.Cryptography.Oid" /> object that contains the <paramref name="object identifier" /> (OID)  of the content type of the inner content of the CMS/PKCS #7 message.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.Oid" /> object that contains the OID value that represents the content type.</returns>
		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060003CC RID: 972 RVA: 0x00012269 File Offset: 0x00010469
		public Oid ContentType
		{
			[CompilerGenerated]
			get
			{
				return this.<ContentType>k__BackingField;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.ContentInfo.Content" /> property  retrieves the content of the CMS/PKCS #7 message.</summary>
		/// <returns>An array of byte values that represents the content data.</returns>
		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060003CD RID: 973 RVA: 0x00012271 File Offset: 0x00010471
		public byte[] Content
		{
			[CompilerGenerated]
			get
			{
				return this.<Content>k__BackingField;
			}
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.ContentInfo.GetContentType(System.Byte[])" /> static method  retrieves the outer content type of the encoded <see cref="T:System.Security.Cryptography.Pkcs.ContentInfo" /> message represented by an array of byte values.</summary>
		/// <param name="encodedMessage">An array of byte values that represents the encoded <see cref="T:System.Security.Cryptography.Pkcs.ContentInfo" /> message from which to retrieve the outer content type.</param>
		/// <returns>If the method succeeds, the method returns an <see cref="T:System.Security.Cryptography.Oid" /> object that contains the outer content type of the specified encoded <see cref="T:System.Security.Cryptography.Pkcs.ContentInfo" /> message.  
		///  If the method fails, it throws an exception.</returns>
		/// <exception cref="T:System.ArgumentNullException">A null reference  was passed to a method that does not accept it as a valid argument.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error occurred during a cryptographic operation.</exception>
		// Token: 0x060003CE RID: 974 RVA: 0x00012279 File Offset: 0x00010479
		public static Oid GetContentType(byte[] encodedMessage)
		{
			if (encodedMessage == null)
			{
				throw new ArgumentNullException("encodedMessage");
			}
			return PkcsPal.Instance.GetEncodedMessageType(encodedMessage);
		}

		// Token: 0x0400027A RID: 634
		[CompilerGenerated]
		private readonly Oid <ContentType>k__BackingField;

		// Token: 0x0400027B RID: 635
		[CompilerGenerated]
		private readonly byte[] <Content>k__BackingField;
	}
}
