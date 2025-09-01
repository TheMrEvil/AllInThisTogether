using System;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Security.Cryptography.Pkcs
{
	/// <summary>The <see cref="T:System.Security.Cryptography.Pkcs.SubjectIdentifierOrKey" /> class defines the type of the identifier of a subject, such as a <see cref="T:System.Security.Cryptography.Pkcs.CmsSigner" /> or a <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipient" />.  The subject can be identified by the certificate issuer and serial number, the hash of the subject key, or the subject key.</summary>
	// Token: 0x0200008A RID: 138
	public sealed class SubjectIdentifierOrKey
	{
		// Token: 0x06000499 RID: 1177 RVA: 0x0001498F File Offset: 0x00012B8F
		internal SubjectIdentifierOrKey(SubjectIdentifierOrKeyType type, object value)
		{
			this.Type = type;
			this.Value = value;
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.SubjectIdentifierOrKey.Type" /> property retrieves the type of subject identifier or key. The subject can be identified by the certificate issuer and serial number, the hash of the subject key, or the subject key.</summary>
		/// <returns>A member of the <see cref="T:System.Security.Cryptography.Pkcs.SubjectIdentifierOrKeyType" /> enumeration that specifies the type of subject identifier.</returns>
		// Token: 0x1700010A RID: 266
		// (get) Token: 0x0600049A RID: 1178 RVA: 0x000149A5 File Offset: 0x00012BA5
		public SubjectIdentifierOrKeyType Type
		{
			[CompilerGenerated]
			get
			{
				return this.<Type>k__BackingField;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.SubjectIdentifierOrKey.Value" /> property retrieves the value of the subject identifier or  key. Use the <see cref="P:System.Security.Cryptography.Pkcs.SubjectIdentifierOrKey.Type" /> property to determine the type of subject identifier or key, and use the <see cref="P:System.Security.Cryptography.Pkcs.SubjectIdentifierOrKey.Value" /> property to retrieve the corresponding value.</summary>
		/// <returns>An <see cref="T:System.Object" /> object that represents the value of the subject identifier or key. This <see cref="T:System.Object" /> can be one of the following objects as determined by the <see cref="P:System.Security.Cryptography.Pkcs.SubjectIdentifierOrKey.Type" /> property.  
		///  <see cref="P:System.Security.Cryptography.Pkcs.SubjectIdentifierOrKey.Type" /> property  
		///
		///   Object  
		///
		///   IssuerAndSerialNumber  
		///
		///  <see cref="T:System.Security.Cryptography.Xml.X509IssuerSerial" /> SubjectKeyIdentifier  
		///
		///  <see cref="T:System.String" /> PublicKeyInfo  
		///
		///  <see cref="T:System.Security.Cryptography.Pkcs.PublicKeyInfo" /></returns>
		// Token: 0x1700010B RID: 267
		// (get) Token: 0x0600049B RID: 1179 RVA: 0x000149AD File Offset: 0x00012BAD
		public object Value
		{
			[CompilerGenerated]
			get
			{
				return this.<Value>k__BackingField;
			}
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x000029A4 File Offset: 0x00000BA4
		internal SubjectIdentifierOrKey()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040002C3 RID: 707
		[CompilerGenerated]
		private readonly SubjectIdentifierOrKeyType <Type>k__BackingField;

		// Token: 0x040002C4 RID: 708
		[CompilerGenerated]
		private readonly object <Value>k__BackingField;
	}
}
