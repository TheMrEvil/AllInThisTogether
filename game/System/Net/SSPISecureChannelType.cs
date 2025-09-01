using System;
using System.Net.Security;
using System.Runtime.InteropServices;

namespace System.Net
{
	// Token: 0x02000552 RID: 1362
	internal class SSPISecureChannelType : SSPIInterface
	{
		// Token: 0x170008FE RID: 2302
		// (get) Token: 0x06002C4F RID: 11343 RVA: 0x00096760 File Offset: 0x00094960
		// (set) Token: 0x06002C50 RID: 11344 RVA: 0x00096769 File Offset: 0x00094969
		public SecurityPackageInfoClass[] SecurityPackages
		{
			get
			{
				return SSPISecureChannelType.s_securityPackages;
			}
			set
			{
				SSPISecureChannelType.s_securityPackages = value;
			}
		}

		// Token: 0x06002C51 RID: 11345 RVA: 0x0009645B File Offset: 0x0009465B
		public int EnumerateSecurityPackages(out int pkgnum, out SafeFreeContextBuffer pkgArray)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Info(this, null, "EnumerateSecurityPackages");
			}
			return SafeFreeContextBuffer.EnumeratePackages(out pkgnum, out pkgArray);
		}

		// Token: 0x06002C52 RID: 11346 RVA: 0x00096477 File Offset: 0x00094677
		public int AcquireCredentialsHandle(string moduleName, Interop.SspiCli.CredentialUse usage, ref Interop.SspiCli.SEC_WINNT_AUTH_IDENTITY_W authdata, out SafeFreeCredentials outCredential)
		{
			return SafeFreeCredentials.AcquireCredentialsHandle(moduleName, usage, ref authdata, out outCredential);
		}

		// Token: 0x06002C53 RID: 11347 RVA: 0x00096483 File Offset: 0x00094683
		public int AcquireCredentialsHandle(string moduleName, Interop.SspiCli.CredentialUse usage, ref SafeSspiAuthDataHandle authdata, out SafeFreeCredentials outCredential)
		{
			return SafeFreeCredentials.AcquireCredentialsHandle(moduleName, usage, ref authdata, out outCredential);
		}

		// Token: 0x06002C54 RID: 11348 RVA: 0x0009648F File Offset: 0x0009468F
		public int AcquireDefaultCredential(string moduleName, Interop.SspiCli.CredentialUse usage, out SafeFreeCredentials outCredential)
		{
			return SafeFreeCredentials.AcquireDefaultCredential(moduleName, usage, out outCredential);
		}

		// Token: 0x06002C55 RID: 11349 RVA: 0x00096499 File Offset: 0x00094699
		public int AcquireCredentialsHandle(string moduleName, Interop.SspiCli.CredentialUse usage, ref Interop.SspiCli.SCHANNEL_CRED authdata, out SafeFreeCredentials outCredential)
		{
			return SafeFreeCredentials.AcquireCredentialsHandle(moduleName, usage, ref authdata, out outCredential);
		}

		// Token: 0x06002C56 RID: 11350 RVA: 0x000964A5 File Offset: 0x000946A5
		public int AcceptSecurityContext(ref SafeFreeCredentials credential, ref SafeDeleteContext context, SecurityBuffer inputBuffer, Interop.SspiCli.ContextFlags inFlags, Interop.SspiCli.Endianness endianness, SecurityBuffer outputBuffer, ref Interop.SspiCli.ContextFlags outFlags)
		{
			return SafeDeleteContext.AcceptSecurityContext(ref credential, ref context, inFlags, endianness, inputBuffer, null, outputBuffer, ref outFlags);
		}

		// Token: 0x06002C57 RID: 11351 RVA: 0x000964B8 File Offset: 0x000946B8
		public int AcceptSecurityContext(SafeFreeCredentials credential, ref SafeDeleteContext context, SecurityBuffer[] inputBuffers, Interop.SspiCli.ContextFlags inFlags, Interop.SspiCli.Endianness endianness, SecurityBuffer outputBuffer, ref Interop.SspiCli.ContextFlags outFlags)
		{
			return SafeDeleteContext.AcceptSecurityContext(ref credential, ref context, inFlags, endianness, null, inputBuffers, outputBuffer, ref outFlags);
		}

		// Token: 0x06002C58 RID: 11352 RVA: 0x00096774 File Offset: 0x00094974
		public int InitializeSecurityContext(ref SafeFreeCredentials credential, ref SafeDeleteContext context, string targetName, Interop.SspiCli.ContextFlags inFlags, Interop.SspiCli.Endianness endianness, SecurityBuffer inputBuffer, SecurityBuffer outputBuffer, ref Interop.SspiCli.ContextFlags outFlags)
		{
			return SafeDeleteContext.InitializeSecurityContext(ref credential, ref context, targetName, inFlags, endianness, inputBuffer, null, outputBuffer, ref outFlags);
		}

		// Token: 0x06002C59 RID: 11353 RVA: 0x00096794 File Offset: 0x00094994
		public int InitializeSecurityContext(SafeFreeCredentials credential, ref SafeDeleteContext context, string targetName, Interop.SspiCli.ContextFlags inFlags, Interop.SspiCli.Endianness endianness, SecurityBuffer[] inputBuffers, SecurityBuffer outputBuffer, ref Interop.SspiCli.ContextFlags outFlags)
		{
			return SafeDeleteContext.InitializeSecurityContext(ref credential, ref context, targetName, inFlags, endianness, null, inputBuffers, outputBuffer, ref outFlags);
		}

		// Token: 0x06002C5A RID: 11354 RVA: 0x000967B8 File Offset: 0x000949B8
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

		// Token: 0x06002C5B RID: 11355 RVA: 0x000967F8 File Offset: 0x000949F8
		public int DecryptMessage(SafeDeleteContext context, ref Interop.SspiCli.SecBufferDesc inputOutput, uint sequenceNumber)
		{
			int result;
			try
			{
				bool flag = false;
				context.DangerousAddRef(ref flag);
				result = Interop.SspiCli.DecryptMessage(ref context._handle, ref inputOutput, sequenceNumber, null);
			}
			finally
			{
				context.DangerousRelease();
			}
			return result;
		}

		// Token: 0x06002C5C RID: 11356 RVA: 0x000966FC File Offset: 0x000948FC
		public int MakeSignature(SafeDeleteContext context, ref Interop.SspiCli.SecBufferDesc inputOutput, uint sequenceNumber)
		{
			throw NotImplemented.ByDesignWithMessage("This method is not implemented by this class.");
		}

		// Token: 0x06002C5D RID: 11357 RVA: 0x000966FC File Offset: 0x000948FC
		public int VerifySignature(SafeDeleteContext context, ref Interop.SspiCli.SecBufferDesc inputOutput, uint sequenceNumber)
		{
			throw NotImplemented.ByDesignWithMessage("This method is not implemented by this class.");
		}

		// Token: 0x06002C5E RID: 11358 RVA: 0x0009683C File Offset: 0x00094A3C
		public unsafe int QueryContextChannelBinding(SafeDeleteContext phContext, Interop.SspiCli.ContextAttribute attribute, out SafeFreeContextBufferChannelBinding refHandle)
		{
			refHandle = SafeFreeContextBufferChannelBinding.CreateEmptyHandle();
			SecPkgContext_Bindings secPkgContext_Bindings = default(SecPkgContext_Bindings);
			return SafeFreeContextBufferChannelBinding.QueryContextChannelBinding(phContext, attribute, &secPkgContext_Bindings, refHandle);
		}

		// Token: 0x06002C5F RID: 11359 RVA: 0x00096864 File Offset: 0x00094A64
		public unsafe int QueryContextAttributes(SafeDeleteContext phContext, Interop.SspiCli.ContextAttribute attribute, byte[] buffer, Type handleType, out SafeHandle refHandle)
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
			return SafeFreeContextBuffer.QueryContextAttributes(phContext, attribute, buffer2, refHandle);
		}

		// Token: 0x06002C60 RID: 11360 RVA: 0x000968F8 File Offset: 0x00094AF8
		public int SetContextAttributes(SafeDeleteContext phContext, Interop.SspiCli.ContextAttribute attribute, byte[] buffer)
		{
			return SafeFreeContextBuffer.SetContextAttributes(phContext, attribute, buffer);
		}

		// Token: 0x06002C61 RID: 11361 RVA: 0x000044FA File Offset: 0x000026FA
		public int QuerySecurityContextToken(SafeDeleteContext phContext, out SecurityContextTokenHandle phToken)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002C62 RID: 11362 RVA: 0x000044FA File Offset: 0x000026FA
		public int CompleteAuthToken(ref SafeDeleteContext refContext, SecurityBuffer[] inputBuffers)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002C63 RID: 11363 RVA: 0x00096902 File Offset: 0x00094B02
		public int ApplyControlToken(ref SafeDeleteContext refContext, SecurityBuffer[] inputBuffers)
		{
			return SafeDeleteContext.ApplyControlToken(ref refContext, inputBuffers);
		}

		// Token: 0x06002C64 RID: 11364 RVA: 0x0000219B File Offset: 0x0000039B
		public SSPISecureChannelType()
		{
		}

		// Token: 0x040017C9 RID: 6089
		private static volatile SecurityPackageInfoClass[] s_securityPackages;
	}
}
