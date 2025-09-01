using System;
using System.IO;

namespace System.Security.Cryptography.Xml
{
	/// <summary>Defines methods that decrypt an XrML <see langword="&lt;encryptedGrant&gt;" /> element.</summary>
	// Token: 0x0200003F RID: 63
	public interface IRelDecryptor
	{
		/// <summary>Decrypts an XrML <see langword="&lt;encryptedGrant&gt;" /> element that is contained within a <see cref="T:System.IO.Stream" /> object.</summary>
		/// <param name="encryptionMethod">An <see cref="T:System.Security.Cryptography.Xml.EncryptionMethod" /> object that encapsulates the algorithm used for XML encryption.</param>
		/// <param name="keyInfo">A <see cref="T:System.Security.Cryptography.Xml.KeyInfo" /> object that contains an asymmetric key to use for decryption.</param>
		/// <param name="toDecrypt">A stream object that contains an <see langword="&lt;encryptedGrant&gt;" /> element to decrypt.</param>
		/// <returns>A <see cref="T:System.IO.Stream" /> object that contains a decrypted <see langword="&lt;encryptedGrant&gt;" /> element.</returns>
		// Token: 0x060001A1 RID: 417
		Stream Decrypt(EncryptionMethod encryptionMethod, KeyInfo keyInfo, Stream toDecrypt);
	}
}
