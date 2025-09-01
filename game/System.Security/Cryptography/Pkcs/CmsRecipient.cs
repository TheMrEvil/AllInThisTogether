using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace System.Security.Cryptography.Pkcs
{
	/// <summary>The <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipient" /> class defines the recipient of a CMS/PKCS #7 message.</summary>
	// Token: 0x02000069 RID: 105
	public sealed class CmsRecipient
	{
		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.CmsRecipient.#ctor(System.Security.Cryptography.X509Certificates.X509Certificate2)" /> constructor constructs an instance of the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipient" /> class by using the specified recipient certificate.</summary>
		/// <param name="certificate">An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object that represents the recipient certificate.</param>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
		// Token: 0x0600037E RID: 894 RVA: 0x00010E58 File Offset: 0x0000F058
		public CmsRecipient(X509Certificate2 certificate) : this(SubjectIdentifierType.IssuerAndSerialNumber, certificate)
		{
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.CmsRecipient.#ctor(System.Security.Cryptography.Pkcs.SubjectIdentifierType,System.Security.Cryptography.X509Certificates.X509Certificate2)" /> constructor constructs an instance of the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipient" /> class by using the specified recipient identifier type and recipient certificate.</summary>
		/// <param name="recipientIdentifierType">A member of the <see cref="T:System.Security.Cryptography.Pkcs.SubjectIdentifierType" /> enumeration that specifies the type of the identifier of the recipient.</param>
		/// <param name="certificate">An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object that represents the recipient certificate.</param>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
		// Token: 0x0600037F RID: 895 RVA: 0x00010E64 File Offset: 0x0000F064
		public CmsRecipient(SubjectIdentifierType recipientIdentifierType, X509Certificate2 certificate)
		{
			if (certificate == null)
			{
				throw new ArgumentNullException("certificate");
			}
			switch (recipientIdentifierType)
			{
			case SubjectIdentifierType.Unknown:
				recipientIdentifierType = SubjectIdentifierType.IssuerAndSerialNumber;
				break;
			case SubjectIdentifierType.IssuerAndSerialNumber:
			case SubjectIdentifierType.SubjectKeyIdentifier:
				break;
			default:
				throw new CryptographicException(SR.Format("The subject identifier type {0} is not valid.", recipientIdentifierType));
			}
			this.RecipientIdentifierType = recipientIdentifierType;
			this.Certificate = certificate;
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.CmsRecipient.RecipientIdentifierType" /> property retrieves the type of the identifier of the recipient.</summary>
		/// <returns>A member of the <see cref="T:System.Security.Cryptography.Pkcs.SubjectIdentifierType" /> enumeration that specifies the type of the identifier of the recipient.</returns>
		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000380 RID: 896 RVA: 0x00010EC2 File Offset: 0x0000F0C2
		public SubjectIdentifierType RecipientIdentifierType
		{
			[CompilerGenerated]
			get
			{
				return this.<RecipientIdentifierType>k__BackingField;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.CmsRecipient.Certificate" /> property retrieves the certificate associated with the recipient.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object that holds the certificate associated with the recipient.</returns>
		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000381 RID: 897 RVA: 0x00010ECA File Offset: 0x0000F0CA
		public X509Certificate2 Certificate
		{
			[CompilerGenerated]
			get
			{
				return this.<Certificate>k__BackingField;
			}
		}

		// Token: 0x04000264 RID: 612
		[CompilerGenerated]
		private readonly SubjectIdentifierType <RecipientIdentifierType>k__BackingField;

		// Token: 0x04000265 RID: 613
		[CompilerGenerated]
		private readonly X509Certificate2 <Certificate>k__BackingField;
	}
}
