using System;
using System.Net.Security;
using System.Runtime.InteropServices;

namespace System.Net
{
	// Token: 0x02000551 RID: 1361
	internal interface SSPIInterface
	{
		// Token: 0x170008FD RID: 2301
		// (get) Token: 0x06002C3A RID: 11322
		// (set) Token: 0x06002C3B RID: 11323
		SecurityPackageInfoClass[] SecurityPackages { get; set; }

		// Token: 0x06002C3C RID: 11324
		int EnumerateSecurityPackages(out int pkgnum, out SafeFreeContextBuffer pkgArray);

		// Token: 0x06002C3D RID: 11325
		int AcquireCredentialsHandle(string moduleName, Interop.SspiCli.CredentialUse usage, ref Interop.SspiCli.SEC_WINNT_AUTH_IDENTITY_W authdata, out SafeFreeCredentials outCredential);

		// Token: 0x06002C3E RID: 11326
		int AcquireCredentialsHandle(string moduleName, Interop.SspiCli.CredentialUse usage, ref SafeSspiAuthDataHandle authdata, out SafeFreeCredentials outCredential);

		// Token: 0x06002C3F RID: 11327
		int AcquireDefaultCredential(string moduleName, Interop.SspiCli.CredentialUse usage, out SafeFreeCredentials outCredential);

		// Token: 0x06002C40 RID: 11328
		int AcquireCredentialsHandle(string moduleName, Interop.SspiCli.CredentialUse usage, ref Interop.SspiCli.SCHANNEL_CRED authdata, out SafeFreeCredentials outCredential);

		// Token: 0x06002C41 RID: 11329
		int AcceptSecurityContext(ref SafeFreeCredentials credential, ref SafeDeleteContext context, SecurityBuffer inputBuffer, Interop.SspiCli.ContextFlags inFlags, Interop.SspiCli.Endianness endianness, SecurityBuffer outputBuffer, ref Interop.SspiCli.ContextFlags outFlags);

		// Token: 0x06002C42 RID: 11330
		int AcceptSecurityContext(SafeFreeCredentials credential, ref SafeDeleteContext context, SecurityBuffer[] inputBuffers, Interop.SspiCli.ContextFlags inFlags, Interop.SspiCli.Endianness endianness, SecurityBuffer outputBuffer, ref Interop.SspiCli.ContextFlags outFlags);

		// Token: 0x06002C43 RID: 11331
		int InitializeSecurityContext(ref SafeFreeCredentials credential, ref SafeDeleteContext context, string targetName, Interop.SspiCli.ContextFlags inFlags, Interop.SspiCli.Endianness endianness, SecurityBuffer inputBuffer, SecurityBuffer outputBuffer, ref Interop.SspiCli.ContextFlags outFlags);

		// Token: 0x06002C44 RID: 11332
		int InitializeSecurityContext(SafeFreeCredentials credential, ref SafeDeleteContext context, string targetName, Interop.SspiCli.ContextFlags inFlags, Interop.SspiCli.Endianness endianness, SecurityBuffer[] inputBuffers, SecurityBuffer outputBuffer, ref Interop.SspiCli.ContextFlags outFlags);

		// Token: 0x06002C45 RID: 11333
		int EncryptMessage(SafeDeleteContext context, ref Interop.SspiCli.SecBufferDesc inputOutput, uint sequenceNumber);

		// Token: 0x06002C46 RID: 11334
		int DecryptMessage(SafeDeleteContext context, ref Interop.SspiCli.SecBufferDesc inputOutput, uint sequenceNumber);

		// Token: 0x06002C47 RID: 11335
		int MakeSignature(SafeDeleteContext context, ref Interop.SspiCli.SecBufferDesc inputOutput, uint sequenceNumber);

		// Token: 0x06002C48 RID: 11336
		int VerifySignature(SafeDeleteContext context, ref Interop.SspiCli.SecBufferDesc inputOutput, uint sequenceNumber);

		// Token: 0x06002C49 RID: 11337
		int QueryContextChannelBinding(SafeDeleteContext phContext, Interop.SspiCli.ContextAttribute attribute, out SafeFreeContextBufferChannelBinding refHandle);

		// Token: 0x06002C4A RID: 11338
		int QueryContextAttributes(SafeDeleteContext phContext, Interop.SspiCli.ContextAttribute attribute, byte[] buffer, Type handleType, out SafeHandle refHandle);

		// Token: 0x06002C4B RID: 11339
		int SetContextAttributes(SafeDeleteContext phContext, Interop.SspiCli.ContextAttribute attribute, byte[] buffer);

		// Token: 0x06002C4C RID: 11340
		int QuerySecurityContextToken(SafeDeleteContext phContext, out SecurityContextTokenHandle phToken);

		// Token: 0x06002C4D RID: 11341
		int CompleteAuthToken(ref SafeDeleteContext refContext, SecurityBuffer[] inputBuffers);

		// Token: 0x06002C4E RID: 11342
		int ApplyControlToken(ref SafeDeleteContext refContext, SecurityBuffer[] inputBuffers);
	}
}
