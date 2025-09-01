using System;
using System.Threading;
using Internal.Cryptography;
using Unity;

namespace System.Security.Cryptography.Pkcs
{
	/// <summary>The <see cref="T:System.Security.Cryptography.Pkcs.KeyAgreeRecipientInfo" /> class defines key agreement recipient information. Key agreement algorithms typically use the Diffie-Hellman key agreement algorithm, in which the two parties that establish a shared cryptographic key both take part in its generation and, by definition, agree on that key. This is in contrast to key transport algorithms, in which one party generates the key unilaterally and sends, or transports it, to the other party.</summary>
	// Token: 0x02000077 RID: 119
	public sealed class KeyAgreeRecipientInfo : RecipientInfo
	{
		// Token: 0x060003E9 RID: 1001 RVA: 0x000126C0 File Offset: 0x000108C0
		internal KeyAgreeRecipientInfo(KeyAgreeRecipientInfoPal pal) : base(RecipientInfoType.KeyAgreement, pal)
		{
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.KeyAgreeRecipientInfo.Version" /> property retrieves the version of the key agreement recipient. This is automatically set for  objects in this class, and the value  implies that the recipient is taking part in a key agreement algorithm.</summary>
		/// <returns>The version of the <see cref="T:System.Security.Cryptography.Pkcs.KeyAgreeRecipientInfo" /> object.</returns>
		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060003EA RID: 1002 RVA: 0x000126CA File Offset: 0x000108CA
		public override int Version
		{
			get
			{
				return this.Pal.Version;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.KeyAgreeRecipientInfo.RecipientIdentifier" /> property retrieves the identifier of the recipient.</summary>
		/// <returns>The identifier of the recipient.</returns>
		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060003EB RID: 1003 RVA: 0x000126D8 File Offset: 0x000108D8
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

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.KeyAgreeRecipientInfo.KeyEncryptionAlgorithm" /> property retrieves the algorithm used to perform the key agreement.</summary>
		/// <returns>The value of the algorithm used to perform the key agreement.</returns>
		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060003EC RID: 1004 RVA: 0x00012708 File Offset: 0x00010908
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

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.KeyAgreeRecipientInfo.EncryptedKey" /> property retrieves the encrypted recipient keying material.</summary>
		/// <returns>An array of byte values that contain the encrypted recipient keying material.</returns>
		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060003ED RID: 1005 RVA: 0x00012738 File Offset: 0x00010938
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

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.KeyAgreeRecipientInfo.OriginatorIdentifierOrKey" /> property retrieves information about the originator of the key agreement for key agreement algorithms that warrant it.</summary>
		/// <returns>An object that contains information about the originator of the key agreement.</returns>
		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060003EE RID: 1006 RVA: 0x00012768 File Offset: 0x00010968
		public SubjectIdentifierOrKey OriginatorIdentifierOrKey
		{
			get
			{
				SubjectIdentifierOrKey result;
				if ((result = this._lazyOriginatorIdentifierKey) == null)
				{
					result = (this._lazyOriginatorIdentifierKey = this.Pal.OriginatorIdentifierOrKey);
				}
				return result;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.KeyAgreeRecipientInfo.Date" /> property retrieves the date and time of the start of the key agreement protocol by the originator.</summary>
		/// <returns>The date and time of the start of the key agreement protocol by the originator.</returns>
		/// <exception cref="T:System.InvalidOperationException">The recipient identifier type is not a subject key identifier.</exception>
		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060003EF RID: 1007 RVA: 0x00012797 File Offset: 0x00010997
		public DateTime Date
		{
			get
			{
				if (this._lazyDate == null)
				{
					this._lazyDate = new DateTime?(this.Pal.Date);
					Interlocked.MemoryBarrier();
				}
				return this._lazyDate.Value;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.KeyAgreeRecipientInfo.OtherKeyAttribute" /> property retrieves attributes of the keying material.</summary>
		/// <returns>The attributes of the keying material.</returns>
		/// <exception cref="T:System.InvalidOperationException">The recipient identifier type is not a subject key identifier.</exception>
		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060003F0 RID: 1008 RVA: 0x000127CC File Offset: 0x000109CC
		public CryptographicAttributeObject OtherKeyAttribute
		{
			get
			{
				CryptographicAttributeObject result;
				if ((result = this._lazyOtherKeyAttribute) == null)
				{
					result = (this._lazyOtherKeyAttribute = this.Pal.OtherKeyAttribute);
				}
				return result;
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060003F1 RID: 1009 RVA: 0x000127FB File Offset: 0x000109FB
		private new KeyAgreeRecipientInfoPal Pal
		{
			get
			{
				return (KeyAgreeRecipientInfoPal)base.Pal;
			}
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x000029A4 File Offset: 0x00000BA4
		internal KeyAgreeRecipientInfo()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000289 RID: 649
		private volatile SubjectIdentifier _lazyRecipientIdentifier;

		// Token: 0x0400028A RID: 650
		private volatile AlgorithmIdentifier _lazyKeyEncryptionAlgorithm;

		// Token: 0x0400028B RID: 651
		private volatile byte[] _lazyEncryptedKey;

		// Token: 0x0400028C RID: 652
		private volatile SubjectIdentifierOrKey _lazyOriginatorIdentifierKey;

		// Token: 0x0400028D RID: 653
		private DateTime? _lazyDate;

		// Token: 0x0400028E RID: 654
		private volatile CryptographicAttributeObject _lazyOtherKeyAttribute;
	}
}
