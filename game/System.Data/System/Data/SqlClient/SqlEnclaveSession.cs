using System;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Data.SqlClient
{
	/// <summary>Encapsulates the state of a secure session between SqlClient and an enclave inside SQL Server, which can be used for computations on encrypted columns protected with Always Encrypted.</summary>
	// Token: 0x020003F5 RID: 1013
	public class SqlEnclaveSession
	{
		/// <summary>Instantiates a new instance of the <see cref="T:System.Data.SqlClient.SqlEnclaveSession" /> class.</summary>
		/// <param name="sessionKey">The symmetric key used to encrypt all the information sent using the session.</param>
		/// <param name="sessionId">The session ID.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="sessionKey" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="sessionKey" /> has zero length.</exception>
		// Token: 0x06002FAD RID: 12205 RVA: 0x000108A6 File Offset: 0x0000EAA6
		public SqlEnclaveSession(byte[] sessionKey, long sessionId)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the session ID.</summary>
		/// <returns>The session ID.</returns>
		// Token: 0x170007EA RID: 2026
		// (get) Token: 0x06002FAE RID: 12206 RVA: 0x000CBB74 File Offset: 0x000C9D74
		public long SessionId
		{
			[CompilerGenerated]
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return 0L;
			}
		}

		/// <summary>Gets the symmetric key that SqlClient uses to encrypt all the information it sends to the enclave using the session.</summary>
		/// <returns>The symmetric key.</returns>
		// Token: 0x06002FAF RID: 12207 RVA: 0x00060C51 File Offset: 0x0005EE51
		public byte[] GetSessionKey()
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
