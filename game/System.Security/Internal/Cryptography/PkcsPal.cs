using System;
using System.Security.Cryptography;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.X509Certificates;
using Internal.Cryptography.Pal.AnyOS;

namespace Internal.Cryptography
{
	// Token: 0x02000111 RID: 273
	internal abstract class PkcsPal
	{
		// Token: 0x060006E8 RID: 1768 RVA: 0x00002145 File Offset: 0x00000345
		protected PkcsPal()
		{
		}

		// Token: 0x060006E9 RID: 1769
		public abstract byte[] Encrypt(CmsRecipientCollection recipients, ContentInfo contentInfo, AlgorithmIdentifier contentEncryptionAlgorithm, X509Certificate2Collection originatorCerts, CryptographicAttributeObjectCollection unprotectedAttributes);

		// Token: 0x060006EA RID: 1770
		public abstract DecryptorPal Decode(byte[] encodedMessage, out int version, out ContentInfo contentInfo, out AlgorithmIdentifier contentEncryptionAlgorithm, out X509Certificate2Collection originatorCerts, out CryptographicAttributeObjectCollection unprotectedAttributes);

		// Token: 0x060006EB RID: 1771
		public abstract byte[] EncodeOctetString(byte[] octets);

		// Token: 0x060006EC RID: 1772
		public abstract byte[] DecodeOctetString(byte[] encodedOctets);

		// Token: 0x060006ED RID: 1773
		public abstract byte[] EncodeUtcTime(DateTime utcTime);

		// Token: 0x060006EE RID: 1774
		public abstract DateTime DecodeUtcTime(byte[] encodedUtcTime);

		// Token: 0x060006EF RID: 1775
		public abstract string DecodeOid(byte[] encodedOid);

		// Token: 0x060006F0 RID: 1776
		public abstract Oid GetEncodedMessageType(byte[] encodedMessage);

		// Token: 0x060006F1 RID: 1777
		public abstract void AddCertsFromStoreForDecryption(X509Certificate2Collection certs);

		// Token: 0x060006F2 RID: 1778
		public abstract Exception CreateRecipientsNotFoundException();

		// Token: 0x060006F3 RID: 1779
		public abstract Exception CreateRecipientInfosAfterEncryptException();

		// Token: 0x060006F4 RID: 1780
		public abstract Exception CreateDecryptAfterEncryptException();

		// Token: 0x060006F5 RID: 1781
		public abstract Exception CreateDecryptTwiceException();

		// Token: 0x060006F6 RID: 1782
		public abstract byte[] GetSubjectKeyIdentifier(X509Certificate2 certificate);

		// Token: 0x060006F7 RID: 1783
		public abstract T GetPrivateKeyForSigning<T>(X509Certificate2 certificate, bool silent) where T : AsymmetricAlgorithm;

		// Token: 0x060006F8 RID: 1784
		public abstract T GetPrivateKeyForDecryption<T>(X509Certificate2 certificate, bool silent) where T : AsymmetricAlgorithm;

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060006F9 RID: 1785 RVA: 0x0001C941 File Offset: 0x0001AB41
		public static PkcsPal Instance
		{
			get
			{
				return PkcsPal.s_instance;
			}
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x0001C948 File Offset: 0x0001AB48
		// Note: this type is marked as 'beforefieldinit'.
		static PkcsPal()
		{
		}

		// Token: 0x04000466 RID: 1126
		private static readonly PkcsPal s_instance = new ManagedPkcsPal();
	}
}
