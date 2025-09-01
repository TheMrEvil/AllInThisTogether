using System;
using System.Net.Security;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Net
{
	// Token: 0x02000550 RID: 1360
	internal class SSPIAuthType : SSPIInterface
	{
		// Token: 0x170008FC RID: 2300
		// (get) Token: 0x06002C23 RID: 11299 RVA: 0x00096448 File Offset: 0x00094648
		// (set) Token: 0x06002C24 RID: 11300 RVA: 0x00096451 File Offset: 0x00094651
		public SecurityPackageInfoClass[] SecurityPackages
		{
			get
			{
				return SSPIAuthType.s_securityPackages;
			}
			set
			{
				SSPIAuthType.s_securityPackages = value;
			}
		}

		// Token: 0x06002C25 RID: 11301 RVA: 0x0009645B File Offset: 0x0009465B
		public int EnumerateSecurityPackages(out int pkgnum, out SafeFreeContextBuffer pkgArray)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Info(this, null, "EnumerateSecurityPackages");
			}
			return SafeFreeContextBuffer.EnumeratePackages(out pkgnum, out pkgArray);
		}

		// Token: 0x06002C26 RID: 11302 RVA: 0x00096477 File Offset: 0x00094677
		public int AcquireCredentialsHandle(string moduleName, Interop.SspiCli.CredentialUse usage, ref Interop.SspiCli.SEC_WINNT_AUTH_IDENTITY_W authdata, out SafeFreeCredentials outCredential)
		{
			return SafeFreeCredentials.AcquireCredentialsHandle(moduleName, usage, ref authdata, out outCredential);
		}

		// Token: 0x06002C27 RID: 11303 RVA: 0x00096483 File Offset: 0x00094683
		public int AcquireCredentialsHandle(string moduleName, Interop.SspiCli.CredentialUse usage, ref SafeSspiAuthDataHandle authdata, out SafeFreeCredentials outCredential)
		{
			return SafeFreeCredentials.AcquireCredentialsHandle(moduleName, usage, ref authdata, out outCredential);
		}

		// Token: 0x06002C28 RID: 11304 RVA: 0x0009648F File Offset: 0x0009468F
		public int AcquireDefaultCredential(string moduleName, Interop.SspiCli.CredentialUse usage, out SafeFreeCredentials outCredential)
		{
			return SafeFreeCredentials.AcquireDefaultCredential(moduleName, usage, out outCredential);
		}

		// Token: 0x06002C29 RID: 11305 RVA: 0x00096499 File Offset: 0x00094699
		public int AcquireCredentialsHandle(string moduleName, Interop.SspiCli.CredentialUse usage, ref Interop.SspiCli.SCHANNEL_CRED authdata, out SafeFreeCredentials outCredential)
		{
			return SafeFreeCredentials.AcquireCredentialsHandle(moduleName, usage, ref authdata, out outCredential);
		}

		// Token: 0x06002C2A RID: 11306 RVA: 0x000964A5 File Offset: 0x000946A5
		public int AcceptSecurityContext(ref SafeFreeCredentials credential, ref SafeDeleteContext context, SecurityBuffer inputBuffer, Interop.SspiCli.ContextFlags inFlags, Interop.SspiCli.Endianness endianness, SecurityBuffer outputBuffer, ref Interop.SspiCli.ContextFlags outFlags)
		{
			return SafeDeleteContext.AcceptSecurityContext(ref credential, ref context, inFlags, endianness, inputBuffer, null, outputBuffer, ref outFlags);
		}

		// Token: 0x06002C2B RID: 11307 RVA: 0x000964B8 File Offset: 0x000946B8
		public int AcceptSecurityContext(SafeFreeCredentials credential, ref SafeDeleteContext context, SecurityBuffer[] inputBuffers, Interop.SspiCli.ContextFlags inFlags, Interop.SspiCli.Endianness endianness, SecurityBuffer outputBuffer, ref Interop.SspiCli.ContextFlags outFlags)
		{
			return SafeDeleteContext.AcceptSecurityContext(ref credential, ref context, inFlags, endianness, null, inputBuffers, outputBuffer, ref outFlags);
		}

		// Token: 0x06002C2C RID: 11308 RVA: 0x000964CC File Offset: 0x000946CC
		public int InitializeSecurityContext(ref SafeFreeCredentials credential, ref SafeDeleteContext context, string targetName, Interop.SspiCli.ContextFlags inFlags, Interop.SspiCli.Endianness endianness, SecurityBuffer inputBuffer, SecurityBuffer outputBuffer, ref Interop.SspiCli.ContextFlags outFlags)
		{
			return SafeDeleteContext.InitializeSecurityContext(ref credential, ref context, targetName, inFlags, endianness, inputBuffer, null, outputBuffer, ref outFlags);
		}

		// Token: 0x06002C2D RID: 11309 RVA: 0x000964EC File Offset: 0x000946EC
		public int InitializeSecurityContext(SafeFreeCredentials credential, ref SafeDeleteContext context, string targetName, Interop.SspiCli.ContextFlags inFlags, Interop.SspiCli.Endianness endianness, SecurityBuffer[] inputBuffers, SecurityBuffer outputBuffer, ref Interop.SspiCli.ContextFlags outFlags)
		{
			return SafeDeleteContext.InitializeSecurityContext(ref credential, ref context, targetName, inFlags, endianness, null, inputBuffers, outputBuffer, ref outFlags);
		}

		// Token: 0x06002C2E RID: 11310 RVA: 0x00096510 File Offset: 0x00094710
		public int EncryptMessage(SafeDeleteContext context, ref Interop.SspiCli.SecBufferDesc inputOutput, uint sequenceNumber)
		{
			int result;
			try
			{
				bool flag = false;
				context.DangerousAddRef(ref flag);
				result = Interop.SspiCli.EncryptMessage(ref context._handle, 0U, ref inputOutput, sequenceNumber);
			}
			finally
			{
				context.DangerousRelease();
			}
			return result;
		}

		// Token: 0x06002C2F RID: 11311 RVA: 0x00096550 File Offset: 0x00094750
		public unsafe int DecryptMessage(SafeDeleteContext context, ref Interop.SspiCli.SecBufferDesc inputOutput, uint sequenceNumber)
		{
			int num = -2146893055;
			uint num2 = 0U;
			try
			{
				bool flag = false;
				context.DangerousAddRef(ref flag);
				num = Interop.SspiCli.DecryptMessage(ref context._handle, ref inputOutput, sequenceNumber, &num2);
			}
			finally
			{
				context.DangerousRelease();
			}
			if (num == 0 && num2 == 2147483649U)
			{
				NetEventSource.Fail(this, FormattableStringFactory.Create("Expected qop = 0, returned value = {0}", new object[]
				{
					num2
				}), "DecryptMessage");
				throw new InvalidOperationException("Protocol error: A received message contains a valid signature but it was not encrypted as required by the effective Protection Level.");
			}
			return num;
		}

		// Token: 0x06002C30 RID: 11312 RVA: 0x000965D4 File Offset: 0x000947D4
		public int MakeSignature(SafeDeleteContext context, ref Interop.SspiCli.SecBufferDesc inputOutput, uint sequenceNumber)
		{
			int result;
			try
			{
				bool flag = false;
				context.DangerousAddRef(ref flag);
				result = Interop.SspiCli.EncryptMessage(ref context._handle, 2147483649U, ref inputOutput, sequenceNumber);
			}
			finally
			{
				context.DangerousRelease();
			}
			return result;
		}

		// Token: 0x06002C31 RID: 11313 RVA: 0x00096618 File Offset: 0x00094818
		public unsafe int VerifySignature(SafeDeleteContext context, ref Interop.SspiCli.SecBufferDesc inputOutput, uint sequenceNumber)
		{
			int result;
			try
			{
				bool flag = false;
				uint num = 0U;
				context.DangerousAddRef(ref flag);
				result = Interop.SspiCli.DecryptMessage(ref context._handle, ref inputOutput, sequenceNumber, &num);
			}
			finally
			{
				context.DangerousRelease();
			}
			return result;
		}

		// Token: 0x06002C32 RID: 11314 RVA: 0x0009665C File Offset: 0x0009485C
		public int QueryContextChannelBinding(SafeDeleteContext context, Interop.SspiCli.ContextAttribute attribute, out SafeFreeContextBufferChannelBinding binding)
		{
			binding = null;
			throw new NotSupportedException();
		}

		// Token: 0x06002C33 RID: 11315 RVA: 0x00096668 File Offset: 0x00094868
		public unsafe int QueryContextAttributes(SafeDeleteContext context, Interop.SspiCli.ContextAttribute attribute, byte[] buffer, Type handleType, out SafeHandle refHandle)
		{
			refHandle = null;
			if (handleType != null)
			{
				if (handleType == typeof(SafeFreeContextBuffer))
				{
					refHandle = SafeFreeContextBuffer.CreateEmptyHandle();
				}
				else
				{
					if (!(handleType == typeof(SafeFreeCertContext)))
					{
						throw new ArgumentException(SR.Format("'{0}' is not a supported handle type.", handleType.FullName), "handleType");
					}
					refHandle = new SafeFreeCertContext();
				}
			}
			byte* buffer2;
			if (buffer == null || buffer.Length == 0)
			{
				buffer2 = null;
			}
			else
			{
				buffer2 = &buffer[0];
			}
			return SafeFreeContextBuffer.QueryContextAttributes(context, attribute, buffer2, refHandle);
		}

		// Token: 0x06002C34 RID: 11316 RVA: 0x000966FC File Offset: 0x000948FC
		public int SetContextAttributes(SafeDeleteContext context, Interop.SspiCli.ContextAttribute attribute, byte[] buffer)
		{
			throw NotImplemented.ByDesignWithMessage("This method is not implemented by this class.");
		}

		// Token: 0x06002C35 RID: 11317 RVA: 0x00096708 File Offset: 0x00094908
		public int QuerySecurityContextToken(SafeDeleteContext phContext, out SecurityContextTokenHandle phToken)
		{
			return SSPIAuthType.GetSecurityContextToken(phContext, out phToken);
		}

		// Token: 0x06002C36 RID: 11318 RVA: 0x00096711 File Offset: 0x00094911
		public int CompleteAuthToken(ref SafeDeleteContext refContext, SecurityBuffer[] inputBuffers)
		{
			return SafeDeleteContext.CompleteAuthToken(ref refContext, inputBuffers);
		}

		// Token: 0x06002C37 RID: 11319 RVA: 0x0009671C File Offset: 0x0009491C
		private static int GetSecurityContextToken(SafeDeleteContext phContext, out SecurityContextTokenHandle safeHandle)
		{
			safeHandle = null;
			int result;
			try
			{
				bool flag = false;
				phContext.DangerousAddRef(ref flag);
				result = Interop.SspiCli.QuerySecurityContextToken(ref phContext._handle, out safeHandle);
			}
			finally
			{
				phContext.DangerousRelease();
			}
			return result;
		}

		// Token: 0x06002C38 RID: 11320 RVA: 0x000044FA File Offset: 0x000026FA
		public int ApplyControlToken(ref SafeDeleteContext refContext, SecurityBuffer[] inputBuffers)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002C39 RID: 11321 RVA: 0x0000219B File Offset: 0x0000039B
		public SSPIAuthType()
		{
		}

		// Token: 0x040017C8 RID: 6088
		private static volatile SecurityPackageInfoClass[] s_securityPackages;
	}
}
