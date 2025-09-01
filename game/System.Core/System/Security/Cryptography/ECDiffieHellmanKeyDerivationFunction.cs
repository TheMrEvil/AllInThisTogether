using System;

namespace System.Security.Cryptography
{
	/// <summary>Specifies the key derivation function that the <see cref="T:System.Security.Cryptography.ECDiffieHellmanCng" /> class will use to convert secret agreements into key material.</summary>
	// Token: 0x0200036E RID: 878
	public enum ECDiffieHellmanKeyDerivationFunction
	{
		/// <summary>A hash algorithm is used to generate key material. The <see cref="P:System.Security.Cryptography.ECDiffieHellmanCng.HashAlgorithm" /> property specifies the name of the algorithm to use. If the algorithm name is not specified, <see cref="T:System.Security.Cryptography.SHA256" /> is used as the default algorithm. </summary>
		// Token: 0x04000CDC RID: 3292
		Hash,
		/// <summary>A Hash-based Message Authentication Code (HMAC) algorithm is used to generate key material. The <see cref="P:System.Security.Cryptography.ECDiffieHellmanCng.HmacKey" /> property specifies the key to use. Either this property must be set or the <see cref="P:System.Security.Cryptography.ECDiffieHellmanCng.UseSecretAgreementAsHmacKey" /> property must be set to <see langword="true" />; otherwise, a <see cref="T:System.Security.Cryptography.CryptographicException" /> is thrown when you use <see cref="F:System.Security.Cryptography.ECDiffieHellmanKeyDerivationFunction.Hmac" />. If both properties are set, the secret agreement is used as the HMAC key. </summary>
		// Token: 0x04000CDD RID: 3293
		Hmac,
		/// <summary>The Transport Layer Security (TLS) protocol is used to generate key material. The <see cref="P:System.Security.Cryptography.ECDiffieHellmanCng.Seed" /> and <see cref="P:System.Security.Cryptography.ECDiffieHellmanCng.Label" /> properties must be set; otherwise, a <see cref="T:System.Security.Cryptography.CryptographicException" /> is thrown when you use <see cref="F:System.Security.Cryptography.ECDiffieHellmanKeyDerivationFunction.Tls" />. </summary>
		// Token: 0x04000CDE RID: 3294
		Tls
	}
}
