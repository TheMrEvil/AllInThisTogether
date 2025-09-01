using System;
using Internal.Cryptography;
using Unity;

namespace System.Security.Cryptography.Pkcs
{
	/// <summary>The <see cref="T:System.Security.Cryptography.Pkcs.KeyTransRecipientInfo" /> class defines key transport recipient information.        Key transport algorithms typically use the RSA algorithm, in which  an originator establishes a shared cryptographic key with a recipient by generating that key and  then transporting it to the recipient. This is in contrast to key agreement algorithms, in which the two parties that will be using a cryptographic key both take part in its generation, thereby mutually agreeing to that key.</summary>
	// Token: 0x02000078 RID: 120
	public sealed class KeyTransRecipientInfo : RecipientInfo
	{
		// Token: 0x060003F3 RID: 1011 RVA: 0x00012808 File Offset: 0x00010A08
		internal KeyTransRecipientInfo(KeyTransRecipientInfoPal pal) : base(RecipientInfoType.KeyTransport, pal)
		{
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.KeyTransRecipientInfo.Version" /> property retrieves the version of the key transport recipient. The version of the key transport recipient is automatically set for  objects in this class, and the value  implies that the recipient is taking part in a key transport algorithm.</summary>
		/// <returns>An int value that represents the version of the key transport <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfo" /> object.</returns>
		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060003F4 RID: 1012 RVA: 0x00012812 File Offset: 0x00010A12
		public override int Version
		{
			get
			{
				return this.Pal.Version;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.KeyTransRecipientInfo.RecipientIdentifier" /> property retrieves the subject identifier associated with the encrypted content.</summary>
		/// <returns>A   <see cref="T:System.Security.Cryptography.Pkcs.SubjectIdentifier" /> object that  stores the identifier of the recipient taking part in the key transport.</returns>
		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060003F5 RID: 1013 RVA: 0x00012820 File Offset: 0x00010A20
		public override SubjectIdentifier RecipientIdentifier
		{
			get
			{
				SubjectIdentifier result;
				if ((result = this._lazyRecipientIdentifier) == null)
				{
					result = (this._lazyRecipientIdentifier = this.Pal.RecipientIdentifier);
				}
				return result;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.KeyTransRecipientInfo.KeyEncryptionAlgorithm" /> property retrieves the key encryption algorithm used to encrypt the content encryption key.</summary>
		/// <returns>An  <see cref="T:System.Security.Cryptography.Pkcs.AlgorithmIdentifier" /> object that stores the key encryption algorithm identifier.</returns>
		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060003F6 RID: 1014 RVA: 0x00012850 File Offset: 0x00010A50
		public override AlgorithmIdentifier KeyEncryptionAlgorithm
		{
			get
			{
				AlgorithmIdentifier result;
				if ((result = this._lazyKeyEncryptionAlgorithm) == null)
				{
					result = (this._lazyKeyEncryptionAlgorithm = this.Pal.KeyEncryptionAlgorithm);
				}
				return result;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.KeyTransRecipientInfo.EncryptedKey" /> property retrieves the encrypted key for this key transport recipient.</summary>
		/// <returns>An array of byte values that represents the encrypted key.</returns>
		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060003F7 RID: 1015 RVA: 0x00012880 File Offset: 0x00010A80
		public override byte[] EncryptedKey
		{
			get
			{
				byte[] result;
				if ((result = this._lazyEncryptedKey) == null)
				{
					result = (this._lazyEncryptedKey = this.Pal.EncryptedKey);
				}
				return result;
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060003F8 RID: 1016 RVA: 0x000128AF File Offset: 0x00010AAF
		private new KeyTransRecipientInfoPal Pal
		{
			get
			{
				return (KeyTransRecipientInfoPal)base.Pal;
			}
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x000029A4 File Offset: 0x00000BA4
		internal KeyTransRecipientInfo()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x0400028F RID: 655
		private volatile SubjectIdentifier _lazyRecipientIdentifier;

		// Token: 0x04000290 RID: 656
		private volatile AlgorithmIdentifier _lazyKeyEncryptionAlgorithm;

		// Token: 0x04000291 RID: 657
		private volatile byte[] _lazyEncryptedKey;
	}
}
