using System;
using System.ComponentModel;
using System.Globalization;
using System.Net.Security;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Net
{
	// Token: 0x02000553 RID: 1363
	internal static class SSPIWrapper
	{
		// Token: 0x06002C65 RID: 11365 RVA: 0x0009690C File Offset: 0x00094B0C
		internal static SecurityPackageInfoClass[] EnumerateSecurityPackages(SSPIInterface secModule)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Enter(null, null, "EnumerateSecurityPackages");
			}
			if (secModule.SecurityPackages == null)
			{
				lock (secModule)
				{
					if (secModule.SecurityPackages == null)
					{
						int num = 0;
						SafeFreeContextBuffer safeFreeContextBuffer = null;
						try
						{
							int num2 = secModule.EnumerateSecurityPackages(out num, out safeFreeContextBuffer);
							if (NetEventSource.IsEnabled)
							{
								NetEventSource.Info(null, FormattableStringFactory.Create("arrayBase: {0}", new object[]
								{
									safeFreeContextBuffer
								}), "EnumerateSecurityPackages");
							}
							if (num2 != 0)
							{
								throw new Win32Exception(num2);
							}
							SecurityPackageInfoClass[] array = new SecurityPackageInfoClass[num];
							for (int i = 0; i < num; i++)
							{
								array[i] = new SecurityPackageInfoClass(safeFreeContextBuffer, i);
								if (NetEventSource.IsEnabled)
								{
									NetEventSource.Log.EnumerateSecurityPackages(array[i].Name);
								}
							}
							secModule.SecurityPackages = array;
						}
						finally
						{
							if (safeFreeContextBuffer != null)
							{
								safeFreeContextBuffer.Dispose();
							}
						}
					}
				}
			}
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Exit(null, null, "EnumerateSecurityPackages");
			}
			return secModule.SecurityPackages;
		}

		// Token: 0x06002C66 RID: 11366 RVA: 0x00096A28 File Offset: 0x00094C28
		internal static SecurityPackageInfoClass GetVerifyPackageInfo(SSPIInterface secModule, string packageName)
		{
			return SSPIWrapper.GetVerifyPackageInfo(secModule, packageName, false);
		}

		// Token: 0x06002C67 RID: 11367 RVA: 0x00096A34 File Offset: 0x00094C34
		internal static SecurityPackageInfoClass GetVerifyPackageInfo(SSPIInterface secModule, string packageName, bool throwIfMissing)
		{
			SecurityPackageInfoClass[] array = SSPIWrapper.EnumerateSecurityPackages(secModule);
			if (array != null)
			{
				for (int i = 0; i < array.Length; i++)
				{
					if (string.Compare(array[i].Name, packageName, StringComparison.OrdinalIgnoreCase) == 0)
					{
						return array[i];
					}
				}
			}
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Log.SspiPackageNotFound(packageName);
			}
			if (throwIfMissing)
			{
				throw new NotSupportedException("The requested security package is not supported.");
			}
			return null;
		}

		// Token: 0x06002C68 RID: 11368 RVA: 0x00096A90 File Offset: 0x00094C90
		public static SafeFreeCredentials AcquireDefaultCredential(SSPIInterface secModule, string package, Interop.SspiCli.CredentialUse intent)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Enter(null, package, "AcquireDefaultCredential");
				NetEventSource.Log.AcquireDefaultCredential(package, intent);
			}
			SafeFreeCredentials result = null;
			int num = secModule.AcquireDefaultCredential(package, intent, out result);
			if (num != 0)
			{
				if (NetEventSource.IsEnabled)
				{
					NetEventSource.Error(null, SR.Format("{0} failed with error {1}.", "AcquireDefaultCredential", string.Format("0x{0:X}", num)), "AcquireDefaultCredential");
				}
				throw new Win32Exception(num);
			}
			return result;
		}

		// Token: 0x06002C69 RID: 11369 RVA: 0x00096B08 File Offset: 0x00094D08
		public static SafeFreeCredentials AcquireCredentialsHandle(SSPIInterface secModule, string package, Interop.SspiCli.CredentialUse intent, ref Interop.SspiCli.SEC_WINNT_AUTH_IDENTITY_W authdata)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Enter(null, package, "AcquireCredentialsHandle");
				NetEventSource.Log.AcquireCredentialsHandle(package, intent, authdata);
			}
			SafeFreeCredentials result = null;
			int num = secModule.AcquireCredentialsHandle(package, intent, ref authdata, out result);
			if (num != 0)
			{
				if (NetEventSource.IsEnabled)
				{
					NetEventSource.Error(null, SR.Format("{0} failed with error {1}.", "AcquireCredentialsHandle", string.Format("0x{0:X}", num)), "AcquireCredentialsHandle");
				}
				throw new Win32Exception(num);
			}
			return result;
		}

		// Token: 0x06002C6A RID: 11370 RVA: 0x00096B8C File Offset: 0x00094D8C
		public static SafeFreeCredentials AcquireCredentialsHandle(SSPIInterface secModule, string package, Interop.SspiCli.CredentialUse intent, ref SafeSspiAuthDataHandle authdata)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Log.AcquireCredentialsHandle(package, intent, authdata);
			}
			SafeFreeCredentials result = null;
			int num = secModule.AcquireCredentialsHandle(package, intent, ref authdata, out result);
			if (num != 0)
			{
				if (NetEventSource.IsEnabled)
				{
					NetEventSource.Error(null, SR.Format("{0} failed with error {1}.", "AcquireCredentialsHandle", string.Format("0x{0:X}", num)), "AcquireCredentialsHandle");
				}
				throw new Win32Exception(num);
			}
			return result;
		}

		// Token: 0x06002C6B RID: 11371 RVA: 0x00096BF8 File Offset: 0x00094DF8
		public static SafeFreeCredentials AcquireCredentialsHandle(SSPIInterface secModule, string package, Interop.SspiCli.CredentialUse intent, Interop.SspiCli.SCHANNEL_CRED scc)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Enter(null, package, "AcquireCredentialsHandle");
				NetEventSource.Log.AcquireCredentialsHandle(package, intent, scc);
			}
			SafeFreeCredentials safeFreeCredentials = null;
			int num = secModule.AcquireCredentialsHandle(package, intent, ref scc, out safeFreeCredentials);
			if (num != 0)
			{
				if (NetEventSource.IsEnabled)
				{
					NetEventSource.Error(null, SR.Format("{0} failed with error {1}.", "AcquireCredentialsHandle", string.Format("0x{0:X}", num)), "AcquireCredentialsHandle");
				}
				throw new Win32Exception(num);
			}
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Exit(null, safeFreeCredentials, "AcquireCredentialsHandle");
			}
			return safeFreeCredentials;
		}

		// Token: 0x06002C6C RID: 11372 RVA: 0x00096C88 File Offset: 0x00094E88
		internal static int InitializeSecurityContext(SSPIInterface secModule, ref SafeFreeCredentials credential, ref SafeDeleteContext context, string targetName, Interop.SspiCli.ContextFlags inFlags, Interop.SspiCli.Endianness datarep, SecurityBuffer inputBuffer, SecurityBuffer outputBuffer, ref Interop.SspiCli.ContextFlags outFlags)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Log.InitializeSecurityContext(credential, context, targetName, inFlags);
			}
			int num = secModule.InitializeSecurityContext(ref credential, ref context, targetName, inFlags, datarep, inputBuffer, outputBuffer, ref outFlags);
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Log.SecurityContextInputBuffer("InitializeSecurityContext", (inputBuffer != null) ? inputBuffer.size : 0, outputBuffer.size, (Interop.SECURITY_STATUS)num);
			}
			return num;
		}

		// Token: 0x06002C6D RID: 11373 RVA: 0x00096CF0 File Offset: 0x00094EF0
		internal static int InitializeSecurityContext(SSPIInterface secModule, SafeFreeCredentials credential, ref SafeDeleteContext context, string targetName, Interop.SspiCli.ContextFlags inFlags, Interop.SspiCli.Endianness datarep, SecurityBuffer[] inputBuffers, SecurityBuffer outputBuffer, ref Interop.SspiCli.ContextFlags outFlags)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Log.InitializeSecurityContext(credential, context, targetName, inFlags);
			}
			int num = secModule.InitializeSecurityContext(credential, ref context, targetName, inFlags, datarep, inputBuffers, outputBuffer, ref outFlags);
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Log.SecurityContextInputBuffers("InitializeSecurityContext", (inputBuffers != null) ? inputBuffers.Length : 0, outputBuffer.size, (Interop.SECURITY_STATUS)num);
			}
			return num;
		}

		// Token: 0x06002C6E RID: 11374 RVA: 0x00096D54 File Offset: 0x00094F54
		internal static int AcceptSecurityContext(SSPIInterface secModule, ref SafeFreeCredentials credential, ref SafeDeleteContext context, Interop.SspiCli.ContextFlags inFlags, Interop.SspiCli.Endianness datarep, SecurityBuffer inputBuffer, SecurityBuffer outputBuffer, ref Interop.SspiCli.ContextFlags outFlags)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Log.AcceptSecurityContext(credential, context, inFlags);
			}
			int num = secModule.AcceptSecurityContext(ref credential, ref context, inputBuffer, inFlags, datarep, outputBuffer, ref outFlags);
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Log.SecurityContextInputBuffer("AcceptSecurityContext", (inputBuffer != null) ? inputBuffer.size : 0, outputBuffer.size, (Interop.SECURITY_STATUS)num);
			}
			return num;
		}

		// Token: 0x06002C6F RID: 11375 RVA: 0x00096DB8 File Offset: 0x00094FB8
		internal static int AcceptSecurityContext(SSPIInterface secModule, SafeFreeCredentials credential, ref SafeDeleteContext context, Interop.SspiCli.ContextFlags inFlags, Interop.SspiCli.Endianness datarep, SecurityBuffer[] inputBuffers, SecurityBuffer outputBuffer, ref Interop.SspiCli.ContextFlags outFlags)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Log.AcceptSecurityContext(credential, context, inFlags);
			}
			int num = secModule.AcceptSecurityContext(credential, ref context, inputBuffers, inFlags, datarep, outputBuffer, ref outFlags);
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Log.SecurityContextInputBuffers("AcceptSecurityContext", (inputBuffers != null) ? inputBuffers.Length : 0, outputBuffer.size, (Interop.SECURITY_STATUS)num);
			}
			return num;
		}

		// Token: 0x06002C70 RID: 11376 RVA: 0x00096E18 File Offset: 0x00095018
		internal static int CompleteAuthToken(SSPIInterface secModule, ref SafeDeleteContext context, SecurityBuffer[] inputBuffers)
		{
			int num = secModule.CompleteAuthToken(ref context, inputBuffers);
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Log.OperationReturnedSomething("CompleteAuthToken", (Interop.SECURITY_STATUS)num);
			}
			return num;
		}

		// Token: 0x06002C71 RID: 11377 RVA: 0x00096E48 File Offset: 0x00095048
		internal static int ApplyControlToken(SSPIInterface secModule, ref SafeDeleteContext context, SecurityBuffer[] inputBuffers)
		{
			int num = secModule.ApplyControlToken(ref context, inputBuffers);
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Log.OperationReturnedSomething("ApplyControlToken", (Interop.SECURITY_STATUS)num);
			}
			return num;
		}

		// Token: 0x06002C72 RID: 11378 RVA: 0x00096E76 File Offset: 0x00095076
		public static int QuerySecurityContextToken(SSPIInterface secModule, SafeDeleteContext context, out SecurityContextTokenHandle token)
		{
			return secModule.QuerySecurityContextToken(context, out token);
		}

		// Token: 0x06002C73 RID: 11379 RVA: 0x00096E80 File Offset: 0x00095080
		public static int EncryptMessage(SSPIInterface secModule, SafeDeleteContext context, SecurityBuffer[] input, uint sequenceNumber)
		{
			return SSPIWrapper.EncryptDecryptHelper(SSPIWrapper.OP.Encrypt, secModule, context, input, sequenceNumber);
		}

		// Token: 0x06002C74 RID: 11380 RVA: 0x00096E8C File Offset: 0x0009508C
		public static int DecryptMessage(SSPIInterface secModule, SafeDeleteContext context, SecurityBuffer[] input, uint sequenceNumber)
		{
			return SSPIWrapper.EncryptDecryptHelper(SSPIWrapper.OP.Decrypt, secModule, context, input, sequenceNumber);
		}

		// Token: 0x06002C75 RID: 11381 RVA: 0x00096E98 File Offset: 0x00095098
		internal static int MakeSignature(SSPIInterface secModule, SafeDeleteContext context, SecurityBuffer[] input, uint sequenceNumber)
		{
			return SSPIWrapper.EncryptDecryptHelper(SSPIWrapper.OP.MakeSignature, secModule, context, input, sequenceNumber);
		}

		// Token: 0x06002C76 RID: 11382 RVA: 0x00096EA4 File Offset: 0x000950A4
		public static int VerifySignature(SSPIInterface secModule, SafeDeleteContext context, SecurityBuffer[] input, uint sequenceNumber)
		{
			return SSPIWrapper.EncryptDecryptHelper(SSPIWrapper.OP.VerifySignature, secModule, context, input, sequenceNumber);
		}

		// Token: 0x06002C77 RID: 11383 RVA: 0x00096EB0 File Offset: 0x000950B0
		private unsafe static int EncryptDecryptHelper(SSPIWrapper.OP op, SSPIInterface secModule, SafeDeleteContext context, SecurityBuffer[] input, uint sequenceNumber)
		{
			Interop.SspiCli.SecBufferDesc secBufferDesc = new Interop.SspiCli.SecBufferDesc(input.Length);
			Interop.SspiCli.SecBuffer[] array = new Interop.SspiCli.SecBuffer[input.Length];
			Interop.SspiCli.SecBuffer[] array2;
			Interop.SspiCli.SecBuffer* pBuffers;
			if ((array2 = array) == null || array2.Length == 0)
			{
				pBuffers = null;
			}
			else
			{
				pBuffers = &array2[0];
			}
			secBufferDesc.pBuffers = (void*)pBuffers;
			GCHandle[] array3 = new GCHandle[input.Length];
			byte[][] array4 = new byte[input.Length][];
			int result;
			try
			{
				for (int i = 0; i < input.Length; i++)
				{
					SecurityBuffer securityBuffer = input[i];
					array[i].cbBuffer = securityBuffer.size;
					array[i].BufferType = securityBuffer.type;
					if (securityBuffer.token == null || securityBuffer.token.Length == 0)
					{
						array[i].pvBuffer = IntPtr.Zero;
					}
					else
					{
						array3[i] = GCHandle.Alloc(securityBuffer.token, GCHandleType.Pinned);
						array[i].pvBuffer = Marshal.UnsafeAddrOfPinnedArrayElement<byte>(securityBuffer.token, securityBuffer.offset);
						array4[i] = securityBuffer.token;
					}
				}
				int num;
				switch (op)
				{
				case SSPIWrapper.OP.Encrypt:
					num = secModule.EncryptMessage(context, ref secBufferDesc, sequenceNumber);
					break;
				case SSPIWrapper.OP.Decrypt:
					num = secModule.DecryptMessage(context, ref secBufferDesc, sequenceNumber);
					break;
				case SSPIWrapper.OP.MakeSignature:
					num = secModule.MakeSignature(context, ref secBufferDesc, sequenceNumber);
					break;
				case SSPIWrapper.OP.VerifySignature:
					num = secModule.VerifySignature(context, ref secBufferDesc, sequenceNumber);
					break;
				default:
					NetEventSource.Fail(null, FormattableStringFactory.Create("Unknown OP: {0}", new object[]
					{
						op
					}), "EncryptDecryptHelper");
					throw NotImplemented.ByDesignWithMessage("This method is not implemented by this class.");
				}
				for (int j = 0; j < input.Length; j++)
				{
					SecurityBuffer securityBuffer2 = input[j];
					securityBuffer2.size = array[j].cbBuffer;
					securityBuffer2.type = array[j].BufferType;
					checked
					{
						if (securityBuffer2.size == 0)
						{
							securityBuffer2.offset = 0;
							securityBuffer2.token = null;
						}
						else
						{
							int k;
							for (k = 0; k < input.Length; k++)
							{
								if (array4[k] != null)
								{
									byte* ptr = (byte*)((void*)Marshal.UnsafeAddrOfPinnedArrayElement<byte>(array4[k], 0));
									if ((void*)array[j].pvBuffer >= (void*)ptr && (byte*)((void*)array[j].pvBuffer) + securityBuffer2.size == ptr + array4[k].Length)
									{
										securityBuffer2.offset = (int)(unchecked((long)((byte*)((void*)array[j].pvBuffer) - (byte*)ptr)));
										securityBuffer2.token = array4[k];
										break;
									}
								}
							}
							if (k >= input.Length)
							{
								NetEventSource.Fail(null, "Output buffer out of range.", "EncryptDecryptHelper");
								securityBuffer2.size = 0;
								securityBuffer2.offset = 0;
								securityBuffer2.token = null;
							}
						}
						if (securityBuffer2.offset < 0 || securityBuffer2.offset > ((securityBuffer2.token == null) ? 0 : securityBuffer2.token.Length))
						{
							NetEventSource.Fail(null, FormattableStringFactory.Create("'offset' out of range.  [{0}]", new object[]
							{
								securityBuffer2.offset
							}), "EncryptDecryptHelper");
						}
					}
					if (securityBuffer2.size < 0 || securityBuffer2.size > ((securityBuffer2.token == null) ? 0 : (securityBuffer2.token.Length - securityBuffer2.offset)))
					{
						NetEventSource.Fail(null, FormattableStringFactory.Create("'size' out of range.  [{0}]", new object[]
						{
							securityBuffer2.size
						}), "EncryptDecryptHelper");
					}
				}
				if (NetEventSource.IsEnabled && num != 0)
				{
					if (num == 590625)
					{
						NetEventSource.Error(null, SR.Format("{0} returned {1}.", op, "SEC_I_RENEGOTIATE"), "EncryptDecryptHelper");
					}
					else
					{
						NetEventSource.Error(null, SR.Format("{0} failed with error {1}.", op, string.Format("0x{0:X}", 0)), "EncryptDecryptHelper");
					}
				}
				result = num;
			}
			finally
			{
				for (int l = 0; l < array3.Length; l++)
				{
					if (array3[l].IsAllocated)
					{
						array3[l].Free();
					}
				}
			}
			return result;
		}

		// Token: 0x06002C78 RID: 11384 RVA: 0x000972CC File Offset: 0x000954CC
		public static SafeFreeContextBufferChannelBinding QueryContextChannelBinding(SSPIInterface secModule, SafeDeleteContext securityContext, Interop.SspiCli.ContextAttribute contextAttribute)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Enter(null, contextAttribute, "QueryContextChannelBinding");
			}
			SafeFreeContextBufferChannelBinding safeFreeContextBufferChannelBinding;
			int num = secModule.QueryContextChannelBinding(securityContext, contextAttribute, out safeFreeContextBufferChannelBinding);
			if (num != 0)
			{
				if (NetEventSource.IsEnabled)
				{
					NetEventSource.Exit(null, FormattableStringFactory.Create("ERROR = {0}", new object[]
					{
						SSPIWrapper.ErrorDescription(num)
					}), "QueryContextChannelBinding");
				}
				return null;
			}
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Exit(null, safeFreeContextBufferChannelBinding, "QueryContextChannelBinding");
			}
			return safeFreeContextBufferChannelBinding;
		}

		// Token: 0x06002C79 RID: 11385 RVA: 0x00097340 File Offset: 0x00095540
		public static object QueryContextAttributes(SSPIInterface secModule, SafeDeleteContext securityContext, Interop.SspiCli.ContextAttribute contextAttribute)
		{
			int num;
			return SSPIWrapper.QueryContextAttributes(secModule, securityContext, contextAttribute, out num);
		}

		// Token: 0x06002C7A RID: 11386 RVA: 0x00097358 File Offset: 0x00095558
		public unsafe static object QueryContextAttributes(SSPIInterface secModule, SafeDeleteContext securityContext, Interop.SspiCli.ContextAttribute contextAttribute, out int errorCode)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Enter(null, contextAttribute, "QueryContextAttributes");
			}
			int num = IntPtr.Size;
			Type handleType = null;
			if (contextAttribute <= Interop.SspiCli.ContextAttribute.SECPKG_ATTR_CLIENT_SPECIFIED_TARGET)
			{
				if (contextAttribute <= Interop.SspiCli.ContextAttribute.SECPKG_ATTR_PACKAGE_INFO)
				{
					switch (contextAttribute)
					{
					case Interop.SspiCli.ContextAttribute.SECPKG_ATTR_SIZES:
						num = SecPkgContext_Sizes.SizeOf;
						goto IL_136;
					case Interop.SspiCli.ContextAttribute.SECPKG_ATTR_NAMES:
						handleType = typeof(SafeFreeContextBuffer);
						goto IL_136;
					case Interop.SspiCli.ContextAttribute.SECPKG_ATTR_LIFESPAN:
					case Interop.SspiCli.ContextAttribute.SECPKG_ATTR_DCE_INFO:
						break;
					case Interop.SspiCli.ContextAttribute.SECPKG_ATTR_STREAM_SIZES:
						num = SecPkgContext_StreamSizes.SizeOf;
						goto IL_136;
					default:
						if (contextAttribute == Interop.SspiCli.ContextAttribute.SECPKG_ATTR_PACKAGE_INFO)
						{
							handleType = typeof(SafeFreeContextBuffer);
							goto IL_136;
						}
						break;
					}
				}
				else
				{
					if (contextAttribute == Interop.SspiCli.ContextAttribute.SECPKG_ATTR_NEGOTIATION_INFO)
					{
						handleType = typeof(SafeFreeContextBuffer);
						num = sizeof(SecPkgContext_NegotiationInfoW);
						goto IL_136;
					}
					if (contextAttribute == Interop.SspiCli.ContextAttribute.SECPKG_ATTR_CLIENT_SPECIFIED_TARGET)
					{
						handleType = typeof(SafeFreeContextBuffer);
						goto IL_136;
					}
				}
			}
			else if (contextAttribute <= Interop.SspiCli.ContextAttribute.SECPKG_ATTR_REMOTE_CERT_CONTEXT)
			{
				if (contextAttribute == Interop.SspiCli.ContextAttribute.SECPKG_ATTR_APPLICATION_PROTOCOL)
				{
					num = Marshal.SizeOf<Interop.SecPkgContext_ApplicationProtocol>();
					goto IL_136;
				}
				if (contextAttribute == Interop.SspiCli.ContextAttribute.SECPKG_ATTR_REMOTE_CERT_CONTEXT)
				{
					handleType = typeof(SafeFreeCertContext);
					goto IL_136;
				}
			}
			else
			{
				if (contextAttribute == Interop.SspiCli.ContextAttribute.SECPKG_ATTR_LOCAL_CERT_CONTEXT)
				{
					handleType = typeof(SafeFreeCertContext);
					goto IL_136;
				}
				if (contextAttribute == Interop.SspiCli.ContextAttribute.SECPKG_ATTR_ISSUER_LIST_EX)
				{
					num = Marshal.SizeOf<Interop.SspiCli.SecPkgContext_IssuerListInfoEx>();
					handleType = typeof(SafeFreeContextBuffer);
					goto IL_136;
				}
				if (contextAttribute == Interop.SspiCli.ContextAttribute.SECPKG_ATTR_CONNECTION_INFO)
				{
					num = Marshal.SizeOf<SecPkgContext_ConnectionInfo>();
					goto IL_136;
				}
			}
			throw new ArgumentException(SR.Format("The specified value is not valid in the '{0}' enumeration.", "contextAttribute"), "contextAttribute");
			IL_136:
			SafeHandle safeHandle = null;
			object obj = null;
			try
			{
				byte[] array = new byte[num];
				errorCode = secModule.QueryContextAttributes(securityContext, contextAttribute, array, handleType, out safeHandle);
				if (errorCode != 0)
				{
					if (NetEventSource.IsEnabled)
					{
						NetEventSource.Exit(null, FormattableStringFactory.Create("ERROR = {0}", new object[]
						{
							SSPIWrapper.ErrorDescription(errorCode)
						}), "QueryContextAttributes");
					}
					return null;
				}
				if (contextAttribute <= Interop.SspiCli.ContextAttribute.SECPKG_ATTR_CLIENT_SPECIFIED_TARGET)
				{
					if (contextAttribute <= Interop.SspiCli.ContextAttribute.SECPKG_ATTR_PACKAGE_INFO)
					{
						switch (contextAttribute)
						{
						case Interop.SspiCli.ContextAttribute.SECPKG_ATTR_SIZES:
							obj = new SecPkgContext_Sizes(array);
							break;
						case Interop.SspiCli.ContextAttribute.SECPKG_ATTR_NAMES:
							obj = Marshal.PtrToStringUni(safeHandle.DangerousGetHandle());
							break;
						case Interop.SspiCli.ContextAttribute.SECPKG_ATTR_LIFESPAN:
						case Interop.SspiCli.ContextAttribute.SECPKG_ATTR_DCE_INFO:
							break;
						case Interop.SspiCli.ContextAttribute.SECPKG_ATTR_STREAM_SIZES:
							obj = new SecPkgContext_StreamSizes(array);
							break;
						default:
							if (contextAttribute == Interop.SspiCli.ContextAttribute.SECPKG_ATTR_PACKAGE_INFO)
							{
								obj = new SecurityPackageInfoClass(safeHandle, 0);
							}
							break;
						}
					}
					else
					{
						if (contextAttribute != Interop.SspiCli.ContextAttribute.SECPKG_ATTR_NEGOTIATION_INFO)
						{
							if (contextAttribute != Interop.SspiCli.ContextAttribute.SECPKG_ATTR_CLIENT_SPECIFIED_TARGET)
							{
								goto IL_2B6;
							}
						}
						else
						{
							try
							{
								fixed (byte* ptr = &array[0])
								{
									void* ptr2 = (void*)ptr;
									obj = new NegotiationInfoClass(safeHandle, (int)((SecPkgContext_NegotiationInfoW*)ptr2)->NegotiationState);
									goto IL_2C2;
								}
							}
							finally
							{
								byte* ptr = null;
							}
						}
						obj = Marshal.PtrToStringUni(safeHandle.DangerousGetHandle());
					}
				}
				else if (contextAttribute <= Interop.SspiCli.ContextAttribute.SECPKG_ATTR_LOCAL_CERT_CONTEXT)
				{
					if (contextAttribute != Interop.SspiCli.ContextAttribute.SECPKG_ATTR_APPLICATION_PROTOCOL)
					{
						if (contextAttribute - Interop.SspiCli.ContextAttribute.SECPKG_ATTR_REMOTE_CERT_CONTEXT <= 1)
						{
							obj = safeHandle;
							safeHandle = null;
						}
					}
					else
					{
						try
						{
							byte[] array2;
							void* value;
							if ((array2 = array) == null || array2.Length == 0)
							{
								value = null;
							}
							else
							{
								value = (void*)(&array2[0]);
							}
							obj = Marshal.PtrToStructure<Interop.SecPkgContext_ApplicationProtocol>(new IntPtr(value));
						}
						finally
						{
							byte[] array2 = null;
						}
					}
				}
				else if (contextAttribute != Interop.SspiCli.ContextAttribute.SECPKG_ATTR_ISSUER_LIST_EX)
				{
					if (contextAttribute == Interop.SspiCli.ContextAttribute.SECPKG_ATTR_CONNECTION_INFO)
					{
						obj = new SecPkgContext_ConnectionInfo(array);
					}
				}
				else
				{
					obj = new Interop.SspiCli.SecPkgContext_IssuerListInfoEx(safeHandle, array);
					safeHandle = null;
				}
				IL_2B6:;
			}
			finally
			{
				if (safeHandle != null)
				{
					safeHandle.Dispose();
				}
			}
			IL_2C2:
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Exit(null, obj, "QueryContextAttributes");
			}
			return obj;
		}

		// Token: 0x06002C7B RID: 11387 RVA: 0x0009768C File Offset: 0x0009588C
		public static string ErrorDescription(int errorCode)
		{
			if (errorCode == -1)
			{
				return "An exception when invoking Win32 API";
			}
			Interop.SECURITY_STATUS security_STATUS = (Interop.SECURITY_STATUS)errorCode;
			if (security_STATUS <= Interop.SECURITY_STATUS.MessageAltered)
			{
				switch (security_STATUS)
				{
				case Interop.SECURITY_STATUS.InvalidHandle:
					return "Invalid handle";
				case Interop.SECURITY_STATUS.Unsupported:
				case Interop.SECURITY_STATUS.InternalError:
					break;
				case Interop.SECURITY_STATUS.TargetUnknown:
					return "Target unknown";
				case Interop.SECURITY_STATUS.PackageNotFound:
					return "Package not found";
				default:
					if (security_STATUS == Interop.SECURITY_STATUS.InvalidToken)
					{
						return "Invalid token";
					}
					if (security_STATUS == Interop.SECURITY_STATUS.MessageAltered)
					{
						return "Message altered";
					}
					break;
				}
			}
			else
			{
				if (security_STATUS == Interop.SECURITY_STATUS.IncompleteMessage)
				{
					return "Message incomplete";
				}
				switch (security_STATUS)
				{
				case Interop.SECURITY_STATUS.BufferNotEnough:
					return "Buffer not enough";
				case Interop.SECURITY_STATUS.WrongPrincipal:
					return "Wrong principal";
				case (Interop.SECURITY_STATUS)(-2146893021):
				case Interop.SECURITY_STATUS.TimeSkew:
					break;
				case Interop.SECURITY_STATUS.UntrustedRoot:
					return "Untrusted root";
				default:
					if (security_STATUS == Interop.SECURITY_STATUS.ContinueNeeded)
					{
						return "Continue needed";
					}
					break;
				}
			}
			return "0x" + errorCode.ToString("x", NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x02000554 RID: 1364
		private enum OP
		{
			// Token: 0x040017CB RID: 6091
			Encrypt = 1,
			// Token: 0x040017CC RID: 6092
			Decrypt,
			// Token: 0x040017CD RID: 6093
			MakeSignature,
			// Token: 0x040017CE RID: 6094
			VerifySignature
		}
	}
}
