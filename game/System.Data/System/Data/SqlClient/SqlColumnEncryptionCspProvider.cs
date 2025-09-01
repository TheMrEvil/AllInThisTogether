using System;
using Unity;

namespace System.Data.SqlClient
{
	/// <summary>The CMK Store provider implementation for using  Microsoft CAPI based Cryptographic Service Providers (CSP) with Always Encrypted.</summary>
	// Token: 0x020003F3 RID: 1011
	public class SqlColumnEncryptionCspProvider : SqlColumnEncryptionKeyStoreProvider
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlClient.SqlColumnEncryptionCspProvider" /> class.</summary>
		// Token: 0x06002FA3 RID: 12195 RVA: 0x000108A6 File Offset: 0x0000EAA6
		public SqlColumnEncryptionCspProvider()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Decrypts the given encrypted value using an asymmetric key specified by the key path and algorithm. The key path will be in the format of [ProviderName]/KeyIdentifier and should be an asymmetric key stored in the specified CSP provider. The valid algorithm used to encrypt/decrypt the CEK is 'RSA_OAEP'.</summary>
		/// <param name="masterKeyPath">The master key path.</param>
		/// <param name="encryptionAlgorithm">The encryption algorithm.</param>
		/// <param name="encryptedColumnEncryptionKey">The encrypted column encryption key.</param>
		/// <returns>The decrypted column encryption key.</returns>
		// Token: 0x06002FA4 RID: 12196 RVA: 0x00060C51 File Offset: 0x0005EE51
		public override byte[] DecryptColumnEncryptionKey(string masterKeyPath, string encryptionAlgorithm, byte[] encryptedColumnEncryptionKey)
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Encrypts the given plain text column encryption key using an asymmetric key specified by the key path and the specified algorithm. The key path will be in the format of [ProviderName]/KeyIdentifier and should be an asymmetric key stored in the specified CSP provider. The valid algorithm used to encrypt/decrypt the CEK is 'RSA_OAEP'.</summary>
		/// <param name="masterKeyPath">The master key path.</param>
		/// <param name="encryptionAlgorithm">The encryption algorithm.</param>
		/// <param name="columnEncryptionKey">The encrypted column encryption key.</param>
		/// <returns>The encrypted column encryption key.</returns>
		// Token: 0x06002FA5 RID: 12197 RVA: 0x00060C51 File Offset: 0x0005EE51
		public override byte[] EncryptColumnEncryptionKey(string masterKeyPath, string encryptionAlgorithm, byte[] columnEncryptionKey)
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Throws a <see cref="T:System.NotSupportedException" /> exception in all cases.</summary>
		/// <param name="masterKeyPath">The column master key path. The path format is specific to a key store provider.</param>
		/// <param name="allowEnclaveComputations">
		///   <see langword="true" /> to indicate that the column master key supports enclave computations; otherwise, <see langword="false" />.</param>
		/// <returns>The signature of the column master key metadata.</returns>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x06002FA6 RID: 12198 RVA: 0x00060C51 File Offset: 0x0005EE51
		public override byte[] SignColumnMasterKeyMetadata(string masterKeyPath, bool allowEnclaveComputations)
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>This function must be implemented by the corresponding Key Store providers. This function should use an asymmetric key identified by a key path and sign the masterkey metadata consisting of (masterKeyPath, allowEnclaveComputations, providerName).</summary>
		/// <param name="masterKeyPath">The complete path of an asymmetric key. The path format is specific to a key store provider.</param>
		/// <param name="allowEnclaveComputations">A boolean that indicates if this key can be sent to the trusted enclave.</param>
		/// <param name="signature">Master key metadata signature.</param>
		/// <returns>A Boolean that indicates if the master key metadata can be verified based on the provided signature.</returns>
		// Token: 0x06002FA7 RID: 12199 RVA: 0x000CBB58 File Offset: 0x000C9D58
		public override bool VerifyColumnMasterKeyMetadata(string masterKeyPath, bool allowEnclaveComputations, byte[] signature)
		{
			ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>A constant string for the provider name 'MSSQL_CSP_PROVIDER'.</summary>
		// Token: 0x04001CBD RID: 7357
		public const string ProviderName = "MSSQL_CSP_PROVIDER";
	}
}
