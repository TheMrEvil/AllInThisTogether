using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using Unity;

namespace System.Data.SqlClient
{
	/// <summary>Encapsulates the information SqlClient sends to SQL Server to initiate the process of attesting and creating a secure session with the enclave, SQL Server uses for computations on columns protected using Always Encrypted.</summary>
	// Token: 0x020003F6 RID: 1014
	public class SqlEnclaveAttestationParameters
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlClient.SqlEnclaveAttestationParameters" /> class.</summary>
		/// <param name="protocol">The enclave attestation protocol.</param>
		/// <param name="input">The input of the enclave attestation protocol.</param>
		/// <param name="clientDiffieHellmanKey">A Diffie-Hellman algorithm that encapsulates a client-side key pair.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="clientDiffieHellmanKey" /> is <see langword="null" />.</exception>
		// Token: 0x06002FB0 RID: 12208 RVA: 0x000108A6 File Offset: 0x0000EAA6
		public SqlEnclaveAttestationParameters(int protocol, byte[] input, ECDiffieHellmanCng clientDiffieHellmanKey)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets a Diffie-Hellman algorithm that encapsulates a key pair that SqlClient uses to establish a secure session with the enclave.</summary>
		/// <returns>The Diffie-Hellman algorithm.</returns>
		// Token: 0x170007EB RID: 2027
		// (get) Token: 0x06002FB1 RID: 12209 RVA: 0x00060C51 File Offset: 0x0005EE51
		public ECDiffieHellmanCng ClientDiffieHellmanKey
		{
			[CompilerGenerated]
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the enclave attestation protocol identifier.</summary>
		/// <returns>The enclave attestation protocol identifier.</returns>
		// Token: 0x170007EC RID: 2028
		// (get) Token: 0x06002FB2 RID: 12210 RVA: 0x000CBB90 File Offset: 0x000C9D90
		public int Protocol
		{
			[CompilerGenerated]
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Gets the information used to initiate the process of attesting the enclave. The format and the content of this information is specific to the attestation protocol.</summary>
		/// <returns>The information required by SQL Server to execute attestation protocol identified by EnclaveAttestationProtocols.</returns>
		// Token: 0x06002FB3 RID: 12211 RVA: 0x00060C51 File Offset: 0x0005EE51
		public byte[] GetInput()
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
