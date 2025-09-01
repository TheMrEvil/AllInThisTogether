using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Net.Security
{
	// Token: 0x0200084C RID: 2124
	internal static class NegotiateStreamPal
	{
		// Token: 0x0600438D RID: 17293 RVA: 0x000EBA76 File Offset: 0x000E9C76
		internal static int QueryMaxTokenSize(string package)
		{
			return SSPIWrapper.GetVerifyPackageInfo(GlobalSSPI.SSPIAuth, package, true).MaxToken;
		}

		// Token: 0x0600438E RID: 17294 RVA: 0x000EBA89 File Offset: 0x000E9C89
		internal static SafeFreeCredentials AcquireDefaultCredential(string package, bool isServer)
		{
			return SSPIWrapper.AcquireDefaultCredential(GlobalSSPI.SSPIAuth, package, isServer ? Interop.SspiCli.CredentialUse.SECPKG_CRED_INBOUND : Interop.SspiCli.CredentialUse.SECPKG_CRED_OUTBOUND);
		}

		// Token: 0x0600438F RID: 17295 RVA: 0x000EBAA0 File Offset: 0x000E9CA0
		internal static SafeFreeCredentials AcquireCredentialsHandle(string package, bool isServer, NetworkCredential credential)
		{
			SafeSspiAuthDataHandle safeSspiAuthDataHandle = null;
			SafeFreeCredentials result;
			try
			{
				Interop.SECURITY_STATUS security_STATUS = Interop.SspiCli.SspiEncodeStringsAsAuthIdentity(credential.UserName, credential.Domain, credential.Password, out safeSspiAuthDataHandle);
				if (security_STATUS != Interop.SECURITY_STATUS.OK)
				{
					if (NetEventSource.IsEnabled)
					{
						NetEventSource.Error(null, SR.Format("{0} failed with error {1}.", "SspiEncodeStringsAsAuthIdentity", string.Format("0x{0:X}", (int)security_STATUS)), "AcquireCredentialsHandle");
					}
					throw new Win32Exception((int)security_STATUS);
				}
				result = SSPIWrapper.AcquireCredentialsHandle(GlobalSSPI.SSPIAuth, package, isServer ? Interop.SspiCli.CredentialUse.SECPKG_CRED_INBOUND : Interop.SspiCli.CredentialUse.SECPKG_CRED_OUTBOUND, ref safeSspiAuthDataHandle);
			}
			finally
			{
				if (safeSspiAuthDataHandle != null)
				{
					safeSspiAuthDataHandle.Dispose();
				}
			}
			return result;
		}

		// Token: 0x06004390 RID: 17296 RVA: 0x000EBB38 File Offset: 0x000E9D38
		internal static string QueryContextClientSpecifiedSpn(SafeDeleteContext securityContext)
		{
			return SSPIWrapper.QueryContextAttributes(GlobalSSPI.SSPIAuth, securityContext, Interop.SspiCli.ContextAttribute.SECPKG_ATTR_CLIENT_SPECIFIED_TARGET) as string;
		}

		// Token: 0x06004391 RID: 17297 RVA: 0x000EBB4C File Offset: 0x000E9D4C
		internal static string QueryContextAuthenticationPackage(SafeDeleteContext securityContext)
		{
			NegotiationInfoClass negotiationInfoClass = SSPIWrapper.QueryContextAttributes(GlobalSSPI.SSPIAuth, securityContext, Interop.SspiCli.ContextAttribute.SECPKG_ATTR_NEGOTIATION_INFO) as NegotiationInfoClass;
			if (negotiationInfoClass == null)
			{
				return null;
			}
			return negotiationInfoClass.AuthenticationPackage;
		}

		// Token: 0x06004392 RID: 17298 RVA: 0x000EBB6C File Offset: 0x000E9D6C
		internal static SecurityStatusPal InitializeSecurityContext(SafeFreeCredentials credentialsHandle, ref SafeDeleteContext securityContext, string spn, ContextFlagsPal requestedContextFlags, SecurityBuffer[] inSecurityBufferArray, SecurityBuffer outSecurityBuffer, ref ContextFlagsPal contextFlags)
		{
			Interop.SspiCli.ContextFlags win32Flags = Interop.SspiCli.ContextFlags.Zero;
			Interop.SECURITY_STATUS win32SecurityStatus = (Interop.SECURITY_STATUS)SSPIWrapper.InitializeSecurityContext(GlobalSSPI.SSPIAuth, credentialsHandle, ref securityContext, spn, ContextFlagsAdapterPal.GetInteropFromContextFlagsPal(requestedContextFlags), Interop.SspiCli.Endianness.SECURITY_NETWORK_DREP, inSecurityBufferArray, outSecurityBuffer, ref win32Flags);
			contextFlags = ContextFlagsAdapterPal.GetContextFlagsPalFromInterop(win32Flags);
			return SecurityStatusAdapterPal.GetSecurityStatusPalFromInterop(win32SecurityStatus, false);
		}

		// Token: 0x06004393 RID: 17299 RVA: 0x000EBBA4 File Offset: 0x000E9DA4
		internal static SecurityStatusPal CompleteAuthToken(ref SafeDeleteContext securityContext, SecurityBuffer[] inSecurityBufferArray)
		{
			return SecurityStatusAdapterPal.GetSecurityStatusPalFromInterop((Interop.SECURITY_STATUS)SSPIWrapper.CompleteAuthToken(GlobalSSPI.SSPIAuth, ref securityContext, inSecurityBufferArray), false);
		}

		// Token: 0x06004394 RID: 17300 RVA: 0x000EBBB8 File Offset: 0x000E9DB8
		internal static SecurityStatusPal AcceptSecurityContext(SafeFreeCredentials credentialsHandle, ref SafeDeleteContext securityContext, ContextFlagsPal requestedContextFlags, SecurityBuffer[] inSecurityBufferArray, SecurityBuffer outSecurityBuffer, ref ContextFlagsPal contextFlags)
		{
			Interop.SspiCli.ContextFlags win32Flags = Interop.SspiCli.ContextFlags.Zero;
			Interop.SECURITY_STATUS win32SecurityStatus = (Interop.SECURITY_STATUS)SSPIWrapper.AcceptSecurityContext(GlobalSSPI.SSPIAuth, credentialsHandle, ref securityContext, ContextFlagsAdapterPal.GetInteropFromContextFlagsPal(requestedContextFlags), Interop.SspiCli.Endianness.SECURITY_NETWORK_DREP, inSecurityBufferArray, outSecurityBuffer, ref win32Flags);
			contextFlags = ContextFlagsAdapterPal.GetContextFlagsPalFromInterop(win32Flags);
			return SecurityStatusAdapterPal.GetSecurityStatusPalFromInterop(win32SecurityStatus, false);
		}

		// Token: 0x06004395 RID: 17301 RVA: 0x000EBBEE File Offset: 0x000E9DEE
		internal static Win32Exception CreateExceptionFromError(SecurityStatusPal statusCode)
		{
			return new Win32Exception((int)SecurityStatusAdapterPal.GetInteropFromSecurityStatusPal(statusCode));
		}

		// Token: 0x06004396 RID: 17302 RVA: 0x000EBBFC File Offset: 0x000E9DFC
		internal static int VerifySignature(SafeDeleteContext securityContext, byte[] buffer, int offset, int count)
		{
			if (offset < 0 || offset > ((buffer == null) ? 0 : buffer.Length))
			{
				NetEventSource.Info("Argument 'offset' out of range.", null, "VerifySignature");
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0 || count > ((buffer == null) ? 0 : (buffer.Length - offset)))
			{
				NetEventSource.Info("Argument 'count' out of range.", null, "VerifySignature");
				throw new ArgumentOutOfRangeException("count");
			}
			SecurityBuffer[] array = new SecurityBuffer[]
			{
				new SecurityBuffer(buffer, offset, count, SecurityBufferType.SECBUFFER_STREAM),
				new SecurityBuffer(0, SecurityBufferType.SECBUFFER_DATA)
			};
			int num = SSPIWrapper.VerifySignature(GlobalSSPI.SSPIAuth, securityContext, array, 0U);
			if (num != 0)
			{
				NetEventSource.Info("VerifySignature threw error: " + num.ToString("x", NumberFormatInfo.InvariantInfo), null, "VerifySignature");
				throw new Win32Exception(num);
			}
			if (array[1].type != SecurityBufferType.SECBUFFER_DATA)
			{
				throw new InternalException();
			}
			return array[1].size;
		}

		// Token: 0x06004397 RID: 17303 RVA: 0x000EBCD8 File Offset: 0x000E9ED8
		internal static int MakeSignature(SafeDeleteContext securityContext, byte[] buffer, int offset, int count, ref byte[] output)
		{
			SecPkgContext_Sizes secPkgContext_Sizes = SSPIWrapper.QueryContextAttributes(GlobalSSPI.SSPIAuth, securityContext, Interop.SspiCli.ContextAttribute.SECPKG_ATTR_SIZES) as SecPkgContext_Sizes;
			int num = count + secPkgContext_Sizes.cbMaxSignature;
			if (output == null || output.Length < num)
			{
				output = new byte[num];
			}
			Buffer.BlockCopy(buffer, offset, output, secPkgContext_Sizes.cbMaxSignature, count);
			SecurityBuffer[] array = new SecurityBuffer[]
			{
				new SecurityBuffer(output, 0, secPkgContext_Sizes.cbMaxSignature, SecurityBufferType.SECBUFFER_TOKEN),
				new SecurityBuffer(output, secPkgContext_Sizes.cbMaxSignature, count, SecurityBufferType.SECBUFFER_DATA)
			};
			int num2 = SSPIWrapper.MakeSignature(GlobalSSPI.SSPIAuth, securityContext, array, 0U);
			if (num2 != 0)
			{
				NetEventSource.Info("MakeSignature threw error: " + num2.ToString("x", NumberFormatInfo.InvariantInfo), null, "MakeSignature");
				throw new Win32Exception(num2);
			}
			return array[0].size + array[1].size;
		}
	}
}
