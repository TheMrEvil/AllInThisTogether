using System;
using System.Runtime.CompilerServices;
using Internal.Cryptography;
using Unity;

namespace System.Security.Cryptography.Pkcs
{
	/// <summary>The <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfo" /> class represents information about a CMS/PKCS #7 message recipient. The <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfo" /> class is an abstract class inherited by the <see cref="T:System.Security.Cryptography.Pkcs.KeyAgreeRecipientInfo" /> and <see cref="T:System.Security.Cryptography.Pkcs.KeyTransRecipientInfo" /> classes.</summary>
	// Token: 0x02000080 RID: 128
	public abstract class RecipientInfo
	{
		// Token: 0x06000423 RID: 1059 RVA: 0x00012CE8 File Offset: 0x00010EE8
		internal RecipientInfo(RecipientInfoType type, RecipientInfoPal pal)
		{
			this.Type = type;
			this.Pal = pal;
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.RecipientInfo.Type" /> property retrieves the type of the recipient. The type of the recipient determines which of two major protocols is used to establish a key between the originator and the recipient of a CMS/PKCS #7 message.</summary>
		/// <returns>A value of the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoType" /> enumeration that defines the type of the recipient.</returns>
		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000424 RID: 1060 RVA: 0x00012CFE File Offset: 0x00010EFE
		public RecipientInfoType Type
		{
			[CompilerGenerated]
			get
			{
				return this.<Type>k__BackingField;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.RecipientInfo.Version" /> abstract property retrieves the version of the recipient information. Derived classes automatically set this property for their objects, and the value indicates whether it is using PKCS #7 or Cryptographic Message Syntax (CMS) to protect messages. The version also implies whether the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfo" /> object establishes a cryptographic key by a key agreement algorithm or a key transport algorithm.</summary>
		/// <returns>An <see cref="T:System.Int32" /> value that represents the version of the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfo" /> object.</returns>
		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000425 RID: 1061
		public abstract int Version { get; }

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.RecipientInfo.RecipientIdentifier" /> abstract property retrieves the identifier of the recipient.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.Pkcs.SubjectIdentifier" /> object that contains the identifier of the recipient.</returns>
		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000426 RID: 1062
		public abstract SubjectIdentifier RecipientIdentifier { get; }

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.RecipientInfo.KeyEncryptionAlgorithm" /> abstract property retrieves the algorithm used to perform the key establishment.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.Pkcs.AlgorithmIdentifier" /> object that contains the value of the algorithm used to establish the key between the originator and recipient of the CMS/PKCS #7 message.</returns>
		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000427 RID: 1063
		public abstract AlgorithmIdentifier KeyEncryptionAlgorithm { get; }

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.RecipientInfo.EncryptedKey" /> abstract property retrieves the encrypted recipient keying material.</summary>
		/// <returns>An array of byte values that contain the encrypted recipient keying material.</returns>
		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000428 RID: 1064
		public abstract byte[] EncryptedKey { get; }

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000429 RID: 1065 RVA: 0x00012D06 File Offset: 0x00010F06
		internal RecipientInfoPal Pal
		{
			[CompilerGenerated]
			get
			{
				return this.<Pal>k__BackingField;
			}
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x000029A4 File Offset: 0x00000BA4
		internal RecipientInfo()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000299 RID: 665
		[CompilerGenerated]
		private readonly RecipientInfoType <Type>k__BackingField;

		// Token: 0x0400029A RID: 666
		[CompilerGenerated]
		private readonly RecipientInfoPal <Pal>k__BackingField;
	}
}
