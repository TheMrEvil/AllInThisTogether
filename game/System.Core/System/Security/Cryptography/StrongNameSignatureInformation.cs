using System;
using System.Security.Permissions;
using Unity;

namespace System.Security.Cryptography
{
	/// <summary>Holds the strong name signature information for a manifest.</summary>
	// Token: 0x02000374 RID: 884
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class StrongNameSignatureInformation
	{
		// Token: 0x06001AE7 RID: 6887 RVA: 0x0000235B File Offset: 0x0000055B
		internal StrongNameSignatureInformation()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the hash algorithm that is used to calculate the strong name signature.</summary>
		/// <returns>The name of the hash algorithm that is used to calculate the strong name signature.</returns>
		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x06001AE8 RID: 6888 RVA: 0x0005A05A File Offset: 0x0005825A
		public string HashAlgorithm
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the HRESULT value of the result code.</summary>
		/// <returns>The HRESULT value of the result code.</returns>
		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x06001AE9 RID: 6889 RVA: 0x0005A1B4 File Offset: 0x000583B4
		public int HResult
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Gets a value indicating whether the strong name signature is valid.</summary>
		/// <returns>
		///     <see langword="true" /> if the strong name signature is valid; otherwise, <see langword="false" />.</returns>
		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x06001AEA RID: 6890 RVA: 0x0005A1D0 File Offset: 0x000583D0
		public bool IsValid
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		/// <summary>Gets the public key that is used to verify the signature.</summary>
		/// <returns>The public key that is used to verify the signature. </returns>
		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x06001AEB RID: 6891 RVA: 0x0005A05A File Offset: 0x0005825A
		public AsymmetricAlgorithm PublicKey
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the results of verifying the strong name signature.</summary>
		/// <returns>The result codes for signature verification.</returns>
		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x06001AEC RID: 6892 RVA: 0x0005A1EC File Offset: 0x000583EC
		public SignatureVerificationResult VerificationResult
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return SignatureVerificationResult.Valid;
			}
		}
	}
}
