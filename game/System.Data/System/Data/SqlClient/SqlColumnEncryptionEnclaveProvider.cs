using System;
using System.Security.Cryptography;
using Unity;

namespace System.Data.SqlClient
{
	/// <summary>The base class that defines the interface for enclave providers for Always Encrypted.</summary>
	// Token: 0x020003F4 RID: 1012
	public abstract class SqlColumnEncryptionEnclaveProvider
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlClient.SqlColumnEncryptionEnclaveProvider" /> class</summary>
		// Token: 0x06002FA8 RID: 12200 RVA: 0x000108A6 File Offset: 0x0000EAA6
		protected SqlColumnEncryptionEnclaveProvider()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>When overridden in a derived class, performs enclave attestation, generates a symmetric key for the session, creates a an enclave session and stores the session information in the cache.</summary>
		/// <param name="enclaveAttestationInfo">The information the provider uses to attest the enclave and generate a symmetric key for the session. The format of this information is specific to the enclave attestation protocol.</param>
		/// <param name="clientDiffieHellmanKey">A Diffie-Hellman algorithm object that encapsulates a client-side key pair.</param>
		/// <param name="attestationUrl">The endpoint of an attestation service for attesting the enclave.</param>
		/// <param name="servername">The name of the SQL Server instance containing the enclave.</param>
		/// <param name="sqlEnclaveSession">The requested enclave session or <see langword="null" /> if the provider doesn't implement session caching.</param>
		/// <param name="counter">A counter that the enclave provider is expected to increment each time SqlClient retrieves the session from the cache. The purpose of this field is to prevent replay attacks.</param>
		// Token: 0x06002FA9 RID: 12201
		public abstract void CreateEnclaveSession(byte[] enclaveAttestationInfo, ECDiffieHellmanCng clientDiffieHellmanKey, string attestationUrl, string servername, out SqlEnclaveSession sqlEnclaveSession, out long counter);

		/// <summary>Gets the information that SqlClient subsequently uses to initiate the process of attesting the enclave and to establish a secure session with the enclave.</summary>
		/// <returns>The information SqlClient subsequently uses to initiate the process of attesting the enclave and to establish a secure session with the enclave.</returns>
		// Token: 0x06002FAA RID: 12202
		public abstract SqlEnclaveAttestationParameters GetAttestationParameters();

		/// <summary>When overridden in a derived class, looks up an existing enclave session information in the enclave session cache. If the enclave provider doesn't implement enclave session caching, this method is expected to return <see langword="null" /> in the <paramref name="sqlEnclaveSession" /> parameter.</summary>
		/// <param name="serverName">The name of the SQL Server instance containing the enclave.</param>
		/// <param name="attestationUrl">The endpoint of an attestation service, SqlClient contacts to attest the enclave.</param>
		/// <param name="sqlEnclaveSession">When this method returns, the requested enclave session or <see langword="null" /> if the provider doesn't implement session caching. This parameter is treated as uninitialized.</param>
		/// <param name="counter">A counter that the enclave provider is expected to increment each time SqlClient retrieves the session from the cache. The purpose of this field is to prevent replay attacks.</param>
		// Token: 0x06002FAB RID: 12203
		public abstract void GetEnclaveSession(string serverName, string attestationUrl, out SqlEnclaveSession sqlEnclaveSession, out long counter);

		/// <summary>When overridden in a derived class, looks up and evicts an enclave session from the enclave session cache, if the provider implements session caching.</summary>
		/// <param name="serverName">The name of the SQL Server instance containing the enclave.</param>
		/// <param name="enclaveAttestationUrl">The endpoint of an attestation service, SqlClient contacts to attest the enclave.</param>
		/// <param name="enclaveSession">The session to be invalidated.</param>
		// Token: 0x06002FAC RID: 12204
		public abstract void InvalidateEnclaveSession(string serverName, string enclaveAttestationUrl, SqlEnclaveSession enclaveSession);
	}
}
