using System;
using System.IO;
using System.Net.Security;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Threading;
using Microsoft.Win32.SafeHandles;

// Token: 0x02000002 RID: 2
internal static class Interop
{
	// Token: 0x02000003 RID: 3
	internal static class Crypt32
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		internal static Interop.Crypt32.CRYPT_OID_INFO FindOidInfo(Interop.Crypt32.CryptOidInfoKeyType keyType, string key, OidGroup group, bool fallBackToAllGroups)
		{
			IntPtr intPtr = IntPtr.Zero;
			Interop.Crypt32.CRYPT_OID_INFO result;
			try
			{
				if (keyType == Interop.Crypt32.CryptOidInfoKeyType.CRYPT_OID_INFO_OID_KEY)
				{
					intPtr = Marshal.StringToCoTaskMemAnsi(key);
				}
				else
				{
					if (keyType != Interop.Crypt32.CryptOidInfoKeyType.CRYPT_OID_INFO_NAME_KEY)
					{
						throw new NotSupportedException();
					}
					intPtr = Marshal.StringToCoTaskMemUni(key);
				}
				if (!Interop.Crypt32.OidGroupWillNotUseActiveDirectory(group))
				{
					OidGroup group2 = group | (OidGroup)(-2147483648);
					IntPtr intPtr2 = Interop.Crypt32.CryptFindOIDInfo(keyType, intPtr, group2);
					if (intPtr2 != IntPtr.Zero)
					{
						return Marshal.PtrToStructure<Interop.Crypt32.CRYPT_OID_INFO>(intPtr2);
					}
				}
				IntPtr intPtr3 = Interop.Crypt32.CryptFindOIDInfo(keyType, intPtr, group);
				if (intPtr3 != IntPtr.Zero)
				{
					result = Marshal.PtrToStructure<Interop.Crypt32.CRYPT_OID_INFO>(intPtr3);
				}
				else
				{
					if (fallBackToAllGroups && group != OidGroup.All)
					{
						IntPtr intPtr4 = Interop.Crypt32.CryptFindOIDInfo(keyType, intPtr, OidGroup.All);
						if (intPtr4 != IntPtr.Zero)
						{
							return Marshal.PtrToStructure<Interop.Crypt32.CRYPT_OID_INFO>(intPtr4);
						}
					}
					result = new Interop.Crypt32.CRYPT_OID_INFO
					{
						AlgId = -1
					};
				}
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeCoTaskMem(intPtr);
				}
			}
			return result;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002138 File Offset: 0x00000338
		private static bool OidGroupWillNotUseActiveDirectory(OidGroup group)
		{
			return group == OidGroup.HashAlgorithm || group == OidGroup.EncryptionAlgorithm || group == OidGroup.PublicKeyAlgorithm || group == OidGroup.SignatureAlgorithm || group == OidGroup.Attribute || group == OidGroup.ExtensionOrAttribute || group == OidGroup.KeyDerivationFunction;
		}

		// Token: 0x06000003 RID: 3
		[DllImport("crypt32.dll", CharSet = CharSet.Unicode)]
		private static extern IntPtr CryptFindOIDInfo(Interop.Crypt32.CryptOidInfoKeyType dwKeyType, IntPtr pvKey, OidGroup group);

		// Token: 0x06000004 RID: 4
		[DllImport("crypt32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool CertFreeCertificateContext(IntPtr pCertContext);

		// Token: 0x06000005 RID: 5
		[DllImport("crypt32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool CertVerifyCertificateChainPolicy(IntPtr pszPolicyOID, SafeX509ChainHandle pChainContext, [In] ref Interop.Crypt32.CERT_CHAIN_POLICY_PARA pPolicyPara, [In] [Out] ref Interop.Crypt32.CERT_CHAIN_POLICY_STATUS pPolicyStatus);

		// Token: 0x02000004 RID: 4
		internal struct CRYPT_OID_INFO
		{
			// Token: 0x17000001 RID: 1
			// (get) Token: 0x06000006 RID: 6 RVA: 0x00002159 File Offset: 0x00000359
			public string OID
			{
				get
				{
					return Marshal.PtrToStringAnsi(this.pszOID);
				}
			}

			// Token: 0x17000002 RID: 2
			// (get) Token: 0x06000007 RID: 7 RVA: 0x00002166 File Offset: 0x00000366
			public string Name
			{
				get
				{
					return Marshal.PtrToStringUni(this.pwszName);
				}
			}

			// Token: 0x04000001 RID: 1
			public int cbSize;

			// Token: 0x04000002 RID: 2
			public IntPtr pszOID;

			// Token: 0x04000003 RID: 3
			public IntPtr pwszName;

			// Token: 0x04000004 RID: 4
			public OidGroup dwGroupId;

			// Token: 0x04000005 RID: 5
			public int AlgId;

			// Token: 0x04000006 RID: 6
			public int cbData;

			// Token: 0x04000007 RID: 7
			public IntPtr pbData;
		}

		// Token: 0x02000005 RID: 5
		internal enum CryptOidInfoKeyType
		{
			// Token: 0x04000009 RID: 9
			CRYPT_OID_INFO_OID_KEY = 1,
			// Token: 0x0400000A RID: 10
			CRYPT_OID_INFO_NAME_KEY,
			// Token: 0x0400000B RID: 11
			CRYPT_OID_INFO_ALGID_KEY,
			// Token: 0x0400000C RID: 12
			CRYPT_OID_INFO_SIGN_KEY,
			// Token: 0x0400000D RID: 13
			CRYPT_OID_INFO_CNG_ALGID_KEY,
			// Token: 0x0400000E RID: 14
			CRYPT_OID_INFO_CNG_SIGN_KEY
		}

		// Token: 0x02000006 RID: 6
		internal static class AuthType
		{
			// Token: 0x0400000F RID: 15
			internal const uint AUTHTYPE_CLIENT = 1U;

			// Token: 0x04000010 RID: 16
			internal const uint AUTHTYPE_SERVER = 2U;
		}

		// Token: 0x02000007 RID: 7
		internal static class CertChainPolicyIgnoreFlags
		{
			// Token: 0x04000011 RID: 17
			internal const uint CERT_CHAIN_POLICY_IGNORE_NOT_TIME_VALID_FLAG = 1U;

			// Token: 0x04000012 RID: 18
			internal const uint CERT_CHAIN_POLICY_IGNORE_CTL_NOT_TIME_VALID_FLAG = 2U;

			// Token: 0x04000013 RID: 19
			internal const uint CERT_CHAIN_POLICY_IGNORE_NOT_TIME_NESTED_FLAG = 4U;

			// Token: 0x04000014 RID: 20
			internal const uint CERT_CHAIN_POLICY_IGNORE_INVALID_BASIC_CONSTRAINTS_FLAG = 8U;

			// Token: 0x04000015 RID: 21
			internal const uint CERT_CHAIN_POLICY_ALLOW_UNKNOWN_CA_FLAG = 16U;

			// Token: 0x04000016 RID: 22
			internal const uint CERT_CHAIN_POLICY_IGNORE_WRONG_USAGE_FLAG = 32U;

			// Token: 0x04000017 RID: 23
			internal const uint CERT_CHAIN_POLICY_IGNORE_INVALID_NAME_FLAG = 64U;

			// Token: 0x04000018 RID: 24
			internal const uint CERT_CHAIN_POLICY_IGNORE_INVALID_POLICY_FLAG = 128U;

			// Token: 0x04000019 RID: 25
			internal const uint CERT_CHAIN_POLICY_IGNORE_END_REV_UNKNOWN_FLAG = 256U;

			// Token: 0x0400001A RID: 26
			internal const uint CERT_CHAIN_POLICY_IGNORE_CTL_SIGNER_REV_UNKNOWN_FLAG = 512U;

			// Token: 0x0400001B RID: 27
			internal const uint CERT_CHAIN_POLICY_IGNORE_CA_REV_UNKNOWN_FLAG = 1024U;

			// Token: 0x0400001C RID: 28
			internal const uint CERT_CHAIN_POLICY_IGNORE_ROOT_REV_UNKNOWN_FLAG = 2048U;

			// Token: 0x0400001D RID: 29
			internal const uint CERT_CHAIN_POLICY_IGNORE_ALL = 4095U;
		}

		// Token: 0x02000008 RID: 8
		internal static class CertChainPolicy
		{
			// Token: 0x0400001E RID: 30
			internal const int CERT_CHAIN_POLICY_BASE = 1;

			// Token: 0x0400001F RID: 31
			internal const int CERT_CHAIN_POLICY_AUTHENTICODE = 2;

			// Token: 0x04000020 RID: 32
			internal const int CERT_CHAIN_POLICY_AUTHENTICODE_TS = 3;

			// Token: 0x04000021 RID: 33
			internal const int CERT_CHAIN_POLICY_SSL = 4;

			// Token: 0x04000022 RID: 34
			internal const int CERT_CHAIN_POLICY_BASIC_CONSTRAINTS = 5;

			// Token: 0x04000023 RID: 35
			internal const int CERT_CHAIN_POLICY_NT_AUTH = 6;

			// Token: 0x04000024 RID: 36
			internal const int CERT_CHAIN_POLICY_MICROSOFT_ROOT = 7;

			// Token: 0x04000025 RID: 37
			internal const int CERT_CHAIN_POLICY_EV = 8;
		}

		// Token: 0x02000009 RID: 9
		internal static class CertChainPolicyErrors
		{
			// Token: 0x04000026 RID: 38
			internal const uint TRUST_E_CERT_SIGNATURE = 2148098052U;

			// Token: 0x04000027 RID: 39
			internal const uint CRYPT_E_REVOKED = 2148081680U;

			// Token: 0x04000028 RID: 40
			internal const uint CERT_E_UNTRUSTEDROOT = 2148204809U;

			// Token: 0x04000029 RID: 41
			internal const uint CERT_E_UNTRUSTEDTESTROOT = 2148204813U;

			// Token: 0x0400002A RID: 42
			internal const uint CERT_E_CHAINING = 2148204810U;

			// Token: 0x0400002B RID: 43
			internal const uint CERT_E_WRONG_USAGE = 2148204816U;

			// Token: 0x0400002C RID: 44
			internal const uint CERT_E_EXPIRE = 2148204801U;

			// Token: 0x0400002D RID: 45
			internal const uint CERT_E_INVALID_NAME = 2148204820U;

			// Token: 0x0400002E RID: 46
			internal const uint CERT_E_INVALID_POLICY = 2148204819U;

			// Token: 0x0400002F RID: 47
			internal const uint TRUST_E_BASIC_CONSTRAINTS = 2148098073U;

			// Token: 0x04000030 RID: 48
			internal const uint CERT_E_CRITICAL = 2148204805U;

			// Token: 0x04000031 RID: 49
			internal const uint CERT_E_VALIDITYPERIODNESTING = 2148204802U;

			// Token: 0x04000032 RID: 50
			internal const uint CRYPT_E_NO_REVOCATION_CHECK = 2148081682U;

			// Token: 0x04000033 RID: 51
			internal const uint CRYPT_E_REVOCATION_OFFLINE = 2148081683U;

			// Token: 0x04000034 RID: 52
			internal const uint CERT_E_PURPOSE = 2148204806U;

			// Token: 0x04000035 RID: 53
			internal const uint CERT_E_REVOKED = 2148204812U;

			// Token: 0x04000036 RID: 54
			internal const uint CERT_E_REVOCATION_FAILURE = 2148204814U;

			// Token: 0x04000037 RID: 55
			internal const uint CERT_E_CN_NO_MATCH = 2148204815U;

			// Token: 0x04000038 RID: 56
			internal const uint CERT_E_ROLE = 2148204803U;
		}

		// Token: 0x0200000A RID: 10
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CERT_CONTEXT
		{
			// Token: 0x04000039 RID: 57
			internal uint dwCertEncodingType;

			// Token: 0x0400003A RID: 58
			internal IntPtr pbCertEncoded;

			// Token: 0x0400003B RID: 59
			internal uint cbCertEncoded;

			// Token: 0x0400003C RID: 60
			internal IntPtr pCertInfo;

			// Token: 0x0400003D RID: 61
			internal IntPtr hCertStore;
		}

		// Token: 0x0200000B RID: 11
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct SSL_EXTRA_CERT_CHAIN_POLICY_PARA
		{
			// Token: 0x0400003E RID: 62
			internal uint cbSize;

			// Token: 0x0400003F RID: 63
			internal uint dwAuthType;

			// Token: 0x04000040 RID: 64
			internal uint fdwChecks;

			// Token: 0x04000041 RID: 65
			internal unsafe char* pwszServerName;
		}

		// Token: 0x0200000C RID: 12
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CERT_CHAIN_POLICY_PARA
		{
			// Token: 0x04000042 RID: 66
			public uint cbSize;

			// Token: 0x04000043 RID: 67
			public uint dwFlags;

			// Token: 0x04000044 RID: 68
			public unsafe Interop.Crypt32.SSL_EXTRA_CERT_CHAIN_POLICY_PARA* pvExtraPolicyPara;
		}

		// Token: 0x0200000D RID: 13
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CERT_CHAIN_POLICY_STATUS
		{
			// Token: 0x04000045 RID: 69
			public uint cbSize;

			// Token: 0x04000046 RID: 70
			public uint dwError;

			// Token: 0x04000047 RID: 71
			public int lChainIndex;

			// Token: 0x04000048 RID: 72
			public int lElementIndex;

			// Token: 0x04000049 RID: 73
			public unsafe void* pvExtraPolicyStatus;
		}
	}

	// Token: 0x0200000E RID: 14
	internal enum BOOL
	{
		// Token: 0x0400004B RID: 75
		FALSE,
		// Token: 0x0400004C RID: 76
		TRUE
	}

	// Token: 0x0200000F RID: 15
	internal static class Libraries
	{
		// Token: 0x0400004D RID: 77
		internal const string Advapi32 = "advapi32.dll";

		// Token: 0x0400004E RID: 78
		internal const string BCrypt = "BCrypt.dll";

		// Token: 0x0400004F RID: 79
		internal const string CoreComm_L1_1_1 = "api-ms-win-core-comm-l1-1-1.dll";

		// Token: 0x04000050 RID: 80
		internal const string Crypt32 = "crypt32.dll";

		// Token: 0x04000051 RID: 81
		internal const string Error_L1 = "api-ms-win-core-winrt-error-l1-1-0.dll";

		// Token: 0x04000052 RID: 82
		internal const string HttpApi = "httpapi.dll";

		// Token: 0x04000053 RID: 83
		internal const string IpHlpApi = "iphlpapi.dll";

		// Token: 0x04000054 RID: 84
		internal const string Kernel32 = "kernel32.dll";

		// Token: 0x04000055 RID: 85
		internal const string Memory_L1_3 = "api-ms-win-core-memory-l1-1-3.dll";

		// Token: 0x04000056 RID: 86
		internal const string Mswsock = "mswsock.dll";

		// Token: 0x04000057 RID: 87
		internal const string NCrypt = "ncrypt.dll";

		// Token: 0x04000058 RID: 88
		internal const string NtDll = "ntdll.dll";

		// Token: 0x04000059 RID: 89
		internal const string Odbc32 = "odbc32.dll";

		// Token: 0x0400005A RID: 90
		internal const string OleAut32 = "oleaut32.dll";

		// Token: 0x0400005B RID: 91
		internal const string PerfCounter = "perfcounter.dll";

		// Token: 0x0400005C RID: 92
		internal const string RoBuffer = "api-ms-win-core-winrt-robuffer-l1-1-0.dll";

		// Token: 0x0400005D RID: 93
		internal const string Secur32 = "secur32.dll";

		// Token: 0x0400005E RID: 94
		internal const string Shell32 = "shell32.dll";

		// Token: 0x0400005F RID: 95
		internal const string SspiCli = "sspicli.dll";

		// Token: 0x04000060 RID: 96
		internal const string User32 = "user32.dll";

		// Token: 0x04000061 RID: 97
		internal const string Version = "version.dll";

		// Token: 0x04000062 RID: 98
		internal const string WebSocket = "websocket.dll";

		// Token: 0x04000063 RID: 99
		internal const string WinHttp = "winhttp.dll";

		// Token: 0x04000064 RID: 100
		internal const string Ws2_32 = "ws2_32.dll";

		// Token: 0x04000065 RID: 101
		internal const string Wtsapi32 = "wtsapi32.dll";

		// Token: 0x04000066 RID: 102
		internal const string CompressionNative = "clrcompression.dll";
	}

	// Token: 0x02000010 RID: 16
	internal enum SECURITY_STATUS
	{
		// Token: 0x04000068 RID: 104
		OK,
		// Token: 0x04000069 RID: 105
		ContinueNeeded = 590610,
		// Token: 0x0400006A RID: 106
		CompleteNeeded,
		// Token: 0x0400006B RID: 107
		CompAndContinue,
		// Token: 0x0400006C RID: 108
		ContextExpired = 590615,
		// Token: 0x0400006D RID: 109
		CredentialsNeeded = 590624,
		// Token: 0x0400006E RID: 110
		Renegotiate,
		// Token: 0x0400006F RID: 111
		OutOfMemory = -2146893056,
		// Token: 0x04000070 RID: 112
		InvalidHandle,
		// Token: 0x04000071 RID: 113
		Unsupported,
		// Token: 0x04000072 RID: 114
		TargetUnknown,
		// Token: 0x04000073 RID: 115
		InternalError,
		// Token: 0x04000074 RID: 116
		PackageNotFound,
		// Token: 0x04000075 RID: 117
		NotOwner,
		// Token: 0x04000076 RID: 118
		CannotInstall,
		// Token: 0x04000077 RID: 119
		InvalidToken,
		// Token: 0x04000078 RID: 120
		CannotPack,
		// Token: 0x04000079 RID: 121
		QopNotSupported,
		// Token: 0x0400007A RID: 122
		NoImpersonation,
		// Token: 0x0400007B RID: 123
		LogonDenied,
		// Token: 0x0400007C RID: 124
		UnknownCredentials,
		// Token: 0x0400007D RID: 125
		NoCredentials,
		// Token: 0x0400007E RID: 126
		MessageAltered,
		// Token: 0x0400007F RID: 127
		OutOfSequence,
		// Token: 0x04000080 RID: 128
		NoAuthenticatingAuthority,
		// Token: 0x04000081 RID: 129
		IncompleteMessage = -2146893032,
		// Token: 0x04000082 RID: 130
		IncompleteCredentials = -2146893024,
		// Token: 0x04000083 RID: 131
		BufferNotEnough,
		// Token: 0x04000084 RID: 132
		WrongPrincipal,
		// Token: 0x04000085 RID: 133
		TimeSkew = -2146893020,
		// Token: 0x04000086 RID: 134
		UntrustedRoot,
		// Token: 0x04000087 RID: 135
		IllegalMessage,
		// Token: 0x04000088 RID: 136
		CertUnknown,
		// Token: 0x04000089 RID: 137
		CertExpired,
		// Token: 0x0400008A RID: 138
		AlgorithmMismatch = -2146893007,
		// Token: 0x0400008B RID: 139
		SecurityQosFailed,
		// Token: 0x0400008C RID: 140
		SmartcardLogonRequired = -2146892994,
		// Token: 0x0400008D RID: 141
		UnsupportedPreauth = -2146892989,
		// Token: 0x0400008E RID: 142
		BadBinding = -2146892986,
		// Token: 0x0400008F RID: 143
		DowngradeDetected = -2146892976,
		// Token: 0x04000090 RID: 144
		ApplicationProtocolMismatch = -2146892953
	}

	// Token: 0x02000011 RID: 17
	internal enum ApplicationProtocolNegotiationStatus
	{
		// Token: 0x04000092 RID: 146
		None,
		// Token: 0x04000093 RID: 147
		Success,
		// Token: 0x04000094 RID: 148
		SelectedClientOnly
	}

	// Token: 0x02000012 RID: 18
	internal enum ApplicationProtocolNegotiationExt
	{
		// Token: 0x04000096 RID: 150
		None,
		// Token: 0x04000097 RID: 151
		NPN,
		// Token: 0x04000098 RID: 152
		ALPN
	}

	// Token: 0x02000013 RID: 19
	[StructLayout(LayoutKind.Sequential)]
	internal class SecPkgContext_ApplicationProtocol
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002174 File Offset: 0x00000374
		public byte[] Protocol
		{
			get
			{
				return new Span<byte>(this.ProtocolId, 0, (int)this.ProtocolIdSize).ToArray();
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000219B File Offset: 0x0000039B
		public SecPkgContext_ApplicationProtocol()
		{
		}

		// Token: 0x04000099 RID: 153
		private const int MaxProtocolIdSize = 255;

		// Token: 0x0400009A RID: 154
		public Interop.ApplicationProtocolNegotiationStatus ProtoNegoStatus;

		// Token: 0x0400009B RID: 155
		public Interop.ApplicationProtocolNegotiationExt ProtoNegoExt;

		// Token: 0x0400009C RID: 156
		public byte ProtocolIdSize;

		// Token: 0x0400009D RID: 157
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 255)]
		public byte[] ProtocolId;
	}

	// Token: 0x02000014 RID: 20
	internal class Kernel32
	{
		// Token: 0x0600000A RID: 10
		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool CloseHandle(IntPtr handle);

		// Token: 0x0600000B RID: 11
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, EntryPoint = "CreateFileW", ExactSpelling = true, SetLastError = true)]
		private unsafe static extern IntPtr CreateFilePrivate(string lpFileName, int dwDesiredAccess, FileShare dwShareMode, Interop.Kernel32.SECURITY_ATTRIBUTES* securityAttrs, FileMode dwCreationDisposition, int dwFlagsAndAttributes, IntPtr hTemplateFile);

		// Token: 0x0600000C RID: 12 RVA: 0x000021A4 File Offset: 0x000003A4
		internal unsafe static SafeFileHandle CreateFile(string lpFileName, int dwDesiredAccess, FileShare dwShareMode, ref Interop.Kernel32.SECURITY_ATTRIBUTES securityAttrs, FileMode dwCreationDisposition, int dwFlagsAndAttributes, IntPtr hTemplateFile)
		{
			lpFileName = PathInternal.EnsureExtendedPrefixIfNeeded(lpFileName);
			fixed (Interop.Kernel32.SECURITY_ATTRIBUTES* ptr = &securityAttrs)
			{
				Interop.Kernel32.SECURITY_ATTRIBUTES* securityAttrs2 = ptr;
				IntPtr intPtr = Interop.Kernel32.CreateFilePrivate(lpFileName, dwDesiredAccess, dwShareMode, securityAttrs2, dwCreationDisposition, dwFlagsAndAttributes, hTemplateFile);
				SafeFileHandle result;
				try
				{
					result = new SafeFileHandle(intPtr, true);
				}
				catch
				{
					Interop.Kernel32.CloseHandle(intPtr);
					throw;
				}
				return result;
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000021F4 File Offset: 0x000003F4
		internal static SafeFileHandle CreateFile(string lpFileName, int dwDesiredAccess, FileShare dwShareMode, FileMode dwCreationDisposition, int dwFlagsAndAttributes)
		{
			IntPtr intPtr = Interop.Kernel32.CreateFile_IntPtr(lpFileName, dwDesiredAccess, dwShareMode, dwCreationDisposition, dwFlagsAndAttributes);
			SafeFileHandle result;
			try
			{
				result = new SafeFileHandle(intPtr, true);
			}
			catch
			{
				Interop.Kernel32.CloseHandle(intPtr);
				throw;
			}
			return result;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002234 File Offset: 0x00000434
		internal static IntPtr CreateFile_IntPtr(string lpFileName, int dwDesiredAccess, FileShare dwShareMode, FileMode dwCreationDisposition, int dwFlagsAndAttributes)
		{
			lpFileName = PathInternal.EnsureExtendedPrefixIfNeeded(lpFileName);
			return Interop.Kernel32.CreateFilePrivate(lpFileName, dwDesiredAccess, dwShareMode, null, dwCreationDisposition, dwFlagsAndAttributes, IntPtr.Zero);
		}

		// Token: 0x0600000F RID: 15
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal unsafe static extern bool ReadDirectoryChangesW(SafeFileHandle hDirectory, byte[] lpBuffer, uint nBufferLength, [MarshalAs(UnmanagedType.Bool)] bool bWatchSubtree, int dwNotifyFilter, out int lpBytesReturned, NativeOverlapped* lpOverlapped, IntPtr lpCompletionRoutine);

		// Token: 0x06000010 RID: 16 RVA: 0x0000219B File Offset: 0x0000039B
		public Kernel32()
		{
		}

		// Token: 0x0400009E RID: 158
		internal const uint SEM_FAILCRITICALERRORS = 1U;

		// Token: 0x02000015 RID: 21
		internal class IOReparseOptions
		{
			// Token: 0x06000011 RID: 17 RVA: 0x0000219B File Offset: 0x0000039B
			public IOReparseOptions()
			{
			}

			// Token: 0x0400009F RID: 159
			internal const uint IO_REPARSE_TAG_FILE_PLACEHOLDER = 2147483669U;

			// Token: 0x040000A0 RID: 160
			internal const uint IO_REPARSE_TAG_MOUNT_POINT = 2684354563U;
		}

		// Token: 0x02000016 RID: 22
		internal class FileOperations
		{
			// Token: 0x06000012 RID: 18 RVA: 0x0000219B File Offset: 0x0000039B
			public FileOperations()
			{
			}

			// Token: 0x040000A1 RID: 161
			internal const int OPEN_EXISTING = 3;

			// Token: 0x040000A2 RID: 162
			internal const int COPY_FILE_FAIL_IF_EXISTS = 1;

			// Token: 0x040000A3 RID: 163
			internal const int FILE_ACTION_ADDED = 1;

			// Token: 0x040000A4 RID: 164
			internal const int FILE_ACTION_REMOVED = 2;

			// Token: 0x040000A5 RID: 165
			internal const int FILE_ACTION_MODIFIED = 3;

			// Token: 0x040000A6 RID: 166
			internal const int FILE_ACTION_RENAMED_OLD_NAME = 4;

			// Token: 0x040000A7 RID: 167
			internal const int FILE_ACTION_RENAMED_NEW_NAME = 5;

			// Token: 0x040000A8 RID: 168
			internal const int FILE_FLAG_BACKUP_SEMANTICS = 33554432;

			// Token: 0x040000A9 RID: 169
			internal const int FILE_FLAG_FIRST_PIPE_INSTANCE = 524288;

			// Token: 0x040000AA RID: 170
			internal const int FILE_FLAG_OVERLAPPED = 1073741824;

			// Token: 0x040000AB RID: 171
			internal const int FILE_LIST_DIRECTORY = 1;
		}

		// Token: 0x02000017 RID: 23
		internal struct SECURITY_ATTRIBUTES
		{
			// Token: 0x040000AC RID: 172
			internal uint nLength;

			// Token: 0x040000AD RID: 173
			internal IntPtr lpSecurityDescriptor;

			// Token: 0x040000AE RID: 174
			internal Interop.BOOL bInheritHandle;
		}
	}

	// Token: 0x02000018 RID: 24
	internal static class SspiCli
	{
		// Token: 0x06000013 RID: 19
		[DllImport("sspicli.dll", ExactSpelling = true, SetLastError = true)]
		internal static extern int EncryptMessage(ref Interop.SspiCli.CredHandle contextHandle, [In] uint qualityOfProtection, [In] [Out] ref Interop.SspiCli.SecBufferDesc inputOutput, [In] uint sequenceNumber);

		// Token: 0x06000014 RID: 20
		[DllImport("sspicli.dll", ExactSpelling = true, SetLastError = true)]
		internal unsafe static extern int DecryptMessage([In] ref Interop.SspiCli.CredHandle contextHandle, [In] [Out] ref Interop.SspiCli.SecBufferDesc inputOutput, [In] uint sequenceNumber, uint* qualityOfProtection);

		// Token: 0x06000015 RID: 21
		[DllImport("sspicli.dll", ExactSpelling = true, SetLastError = true)]
		internal static extern int QuerySecurityContextToken(ref Interop.SspiCli.CredHandle phContext, out SecurityContextTokenHandle handle);

		// Token: 0x06000016 RID: 22
		[DllImport("sspicli.dll", ExactSpelling = true, SetLastError = true)]
		internal static extern int FreeContextBuffer([In] IntPtr contextBuffer);

		// Token: 0x06000017 RID: 23
		[DllImport("sspicli.dll", ExactSpelling = true, SetLastError = true)]
		internal static extern int FreeCredentialsHandle(ref Interop.SspiCli.CredHandle handlePtr);

		// Token: 0x06000018 RID: 24
		[DllImport("sspicli.dll", ExactSpelling = true, SetLastError = true)]
		internal static extern int DeleteSecurityContext(ref Interop.SspiCli.CredHandle handlePtr);

		// Token: 0x06000019 RID: 25
		[DllImport("sspicli.dll", ExactSpelling = true, SetLastError = true)]
		internal unsafe static extern int AcceptSecurityContext(ref Interop.SspiCli.CredHandle credentialHandle, [In] void* inContextPtr, [In] Interop.SspiCli.SecBufferDesc* inputBuffer, [In] Interop.SspiCli.ContextFlags inFlags, [In] Interop.SspiCli.Endianness endianness, ref Interop.SspiCli.CredHandle outContextPtr, [In] [Out] ref Interop.SspiCli.SecBufferDesc outputBuffer, [In] [Out] ref Interop.SspiCli.ContextFlags attributes, out long timeStamp);

		// Token: 0x0600001A RID: 26
		[DllImport("sspicli.dll", ExactSpelling = true, SetLastError = true)]
		internal unsafe static extern int QueryContextAttributesW(ref Interop.SspiCli.CredHandle contextHandle, [In] Interop.SspiCli.ContextAttribute attribute, [In] void* buffer);

		// Token: 0x0600001B RID: 27
		[DllImport("sspicli.dll", ExactSpelling = true, SetLastError = true)]
		internal static extern int SetContextAttributesW(ref Interop.SspiCli.CredHandle contextHandle, [In] Interop.SspiCli.ContextAttribute attribute, [In] byte[] buffer, [In] int bufferSize);

		// Token: 0x0600001C RID: 28
		[DllImport("sspicli.dll", ExactSpelling = true, SetLastError = true)]
		internal static extern int EnumerateSecurityPackagesW(out int pkgnum, out SafeFreeContextBuffer_SECURITY handle);

		// Token: 0x0600001D RID: 29
		[DllImport("sspicli.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
		internal unsafe static extern int AcquireCredentialsHandleW([In] string principal, [In] string moduleName, [In] int usage, [In] void* logonID, [In] ref Interop.SspiCli.SEC_WINNT_AUTH_IDENTITY_W authdata, [In] void* keyCallback, [In] void* keyArgument, ref Interop.SspiCli.CredHandle handlePtr, out long timeStamp);

		// Token: 0x0600001E RID: 30
		[DllImport("sspicli.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
		internal unsafe static extern int AcquireCredentialsHandleW([In] string principal, [In] string moduleName, [In] int usage, [In] void* logonID, [In] IntPtr zero, [In] void* keyCallback, [In] void* keyArgument, ref Interop.SspiCli.CredHandle handlePtr, out long timeStamp);

		// Token: 0x0600001F RID: 31
		[DllImport("sspicli.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
		internal unsafe static extern int AcquireCredentialsHandleW([In] string principal, [In] string moduleName, [In] int usage, [In] void* logonID, [In] SafeSspiAuthDataHandle authdata, [In] void* keyCallback, [In] void* keyArgument, ref Interop.SspiCli.CredHandle handlePtr, out long timeStamp);

		// Token: 0x06000020 RID: 32
		[DllImport("sspicli.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
		internal unsafe static extern int AcquireCredentialsHandleW([In] string principal, [In] string moduleName, [In] int usage, [In] void* logonID, [In] ref Interop.SspiCli.SCHANNEL_CRED authData, [In] void* keyCallback, [In] void* keyArgument, ref Interop.SspiCli.CredHandle handlePtr, out long timeStamp);

		// Token: 0x06000021 RID: 33
		[DllImport("sspicli.dll", ExactSpelling = true, SetLastError = true)]
		internal unsafe static extern int InitializeSecurityContextW(ref Interop.SspiCli.CredHandle credentialHandle, [In] void* inContextPtr, [In] byte* targetName, [In] Interop.SspiCli.ContextFlags inFlags, [In] int reservedI, [In] Interop.SspiCli.Endianness endianness, [In] Interop.SspiCli.SecBufferDesc* inputBuffer, [In] int reservedII, ref Interop.SspiCli.CredHandle outContextPtr, [In] [Out] ref Interop.SspiCli.SecBufferDesc outputBuffer, [In] [Out] ref Interop.SspiCli.ContextFlags attributes, out long timeStamp);

		// Token: 0x06000022 RID: 34
		[DllImport("sspicli.dll", ExactSpelling = true, SetLastError = true)]
		internal unsafe static extern int CompleteAuthToken([In] void* inContextPtr, [In] [Out] ref Interop.SspiCli.SecBufferDesc inputBuffers);

		// Token: 0x06000023 RID: 35
		[DllImport("sspicli.dll", ExactSpelling = true, SetLastError = true)]
		internal unsafe static extern int ApplyControlToken([In] void* inContextPtr, [In] [Out] ref Interop.SspiCli.SecBufferDesc inputBuffers);

		// Token: 0x06000024 RID: 36
		[DllImport("sspicli.dll", ExactSpelling = true, SetLastError = true)]
		internal static extern Interop.SECURITY_STATUS SspiFreeAuthIdentity([In] IntPtr authData);

		// Token: 0x06000025 RID: 37
		[DllImport("sspicli.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
		internal static extern Interop.SECURITY_STATUS SspiEncodeStringsAsAuthIdentity([In] string userName, [In] string domainName, [In] string password, out SafeSspiAuthDataHandle authData);

		// Token: 0x040000AF RID: 175
		internal const uint SECQOP_WRAP_NO_ENCRYPT = 2147483649U;

		// Token: 0x040000B0 RID: 176
		internal const int SEC_I_RENEGOTIATE = 590625;

		// Token: 0x040000B1 RID: 177
		internal const int SECPKG_NEGOTIATION_COMPLETE = 0;

		// Token: 0x040000B2 RID: 178
		internal const int SECPKG_NEGOTIATION_OPTIMISTIC = 1;

		// Token: 0x02000019 RID: 25
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		internal struct CredHandle
		{
			// Token: 0x17000004 RID: 4
			// (get) Token: 0x06000026 RID: 38 RVA: 0x00002250 File Offset: 0x00000450
			public bool IsZero
			{
				get
				{
					return this.dwLower == IntPtr.Zero && this.dwUpper == IntPtr.Zero;
				}
			}

			// Token: 0x06000027 RID: 39 RVA: 0x00002276 File Offset: 0x00000476
			internal void SetToInvalid()
			{
				this.dwLower = IntPtr.Zero;
				this.dwUpper = IntPtr.Zero;
			}

			// Token: 0x06000028 RID: 40 RVA: 0x0000228E File Offset: 0x0000048E
			public override string ToString()
			{
				return this.dwLower.ToString("x") + ":" + this.dwUpper.ToString("x");
			}

			// Token: 0x040000B3 RID: 179
			private IntPtr dwLower;

			// Token: 0x040000B4 RID: 180
			private IntPtr dwUpper;
		}

		// Token: 0x0200001A RID: 26
		internal enum ContextAttribute
		{
			// Token: 0x040000B6 RID: 182
			SECPKG_ATTR_SIZES,
			// Token: 0x040000B7 RID: 183
			SECPKG_ATTR_NAMES,
			// Token: 0x040000B8 RID: 184
			SECPKG_ATTR_LIFESPAN,
			// Token: 0x040000B9 RID: 185
			SECPKG_ATTR_DCE_INFO,
			// Token: 0x040000BA RID: 186
			SECPKG_ATTR_STREAM_SIZES,
			// Token: 0x040000BB RID: 187
			SECPKG_ATTR_AUTHORITY = 6,
			// Token: 0x040000BC RID: 188
			SECPKG_ATTR_PACKAGE_INFO = 10,
			// Token: 0x040000BD RID: 189
			SECPKG_ATTR_NEGOTIATION_INFO = 12,
			// Token: 0x040000BE RID: 190
			SECPKG_ATTR_UNIQUE_BINDINGS = 25,
			// Token: 0x040000BF RID: 191
			SECPKG_ATTR_ENDPOINT_BINDINGS,
			// Token: 0x040000C0 RID: 192
			SECPKG_ATTR_CLIENT_SPECIFIED_TARGET,
			// Token: 0x040000C1 RID: 193
			SECPKG_ATTR_APPLICATION_PROTOCOL = 35,
			// Token: 0x040000C2 RID: 194
			SECPKG_ATTR_REMOTE_CERT_CONTEXT = 83,
			// Token: 0x040000C3 RID: 195
			SECPKG_ATTR_LOCAL_CERT_CONTEXT,
			// Token: 0x040000C4 RID: 196
			SECPKG_ATTR_ROOT_STORE,
			// Token: 0x040000C5 RID: 197
			SECPKG_ATTR_ISSUER_LIST_EX = 89,
			// Token: 0x040000C6 RID: 198
			SECPKG_ATTR_CONNECTION_INFO,
			// Token: 0x040000C7 RID: 199
			SECPKG_ATTR_UI_INFO = 104
		}

		// Token: 0x0200001B RID: 27
		[Flags]
		internal enum ContextFlags
		{
			// Token: 0x040000C9 RID: 201
			Zero = 0,
			// Token: 0x040000CA RID: 202
			Delegate = 1,
			// Token: 0x040000CB RID: 203
			MutualAuth = 2,
			// Token: 0x040000CC RID: 204
			ReplayDetect = 4,
			// Token: 0x040000CD RID: 205
			SequenceDetect = 8,
			// Token: 0x040000CE RID: 206
			Confidentiality = 16,
			// Token: 0x040000CF RID: 207
			UseSessionKey = 32,
			// Token: 0x040000D0 RID: 208
			AllocateMemory = 256,
			// Token: 0x040000D1 RID: 209
			Connection = 2048,
			// Token: 0x040000D2 RID: 210
			InitExtendedError = 16384,
			// Token: 0x040000D3 RID: 211
			AcceptExtendedError = 32768,
			// Token: 0x040000D4 RID: 212
			InitStream = 32768,
			// Token: 0x040000D5 RID: 213
			AcceptStream = 65536,
			// Token: 0x040000D6 RID: 214
			InitIntegrity = 65536,
			// Token: 0x040000D7 RID: 215
			AcceptIntegrity = 131072,
			// Token: 0x040000D8 RID: 216
			InitManualCredValidation = 524288,
			// Token: 0x040000D9 RID: 217
			InitUseSuppliedCreds = 128,
			// Token: 0x040000DA RID: 218
			InitIdentify = 131072,
			// Token: 0x040000DB RID: 219
			AcceptIdentify = 524288,
			// Token: 0x040000DC RID: 220
			ProxyBindings = 67108864,
			// Token: 0x040000DD RID: 221
			AllowMissingBindings = 268435456,
			// Token: 0x040000DE RID: 222
			UnverifiedTargetName = 536870912
		}

		// Token: 0x0200001C RID: 28
		internal enum Endianness
		{
			// Token: 0x040000E0 RID: 224
			SECURITY_NETWORK_DREP,
			// Token: 0x040000E1 RID: 225
			SECURITY_NATIVE_DREP = 16
		}

		// Token: 0x0200001D RID: 29
		internal enum CredentialUse
		{
			// Token: 0x040000E3 RID: 227
			SECPKG_CRED_INBOUND = 1,
			// Token: 0x040000E4 RID: 228
			SECPKG_CRED_OUTBOUND,
			// Token: 0x040000E5 RID: 229
			SECPKG_CRED_BOTH
		}

		// Token: 0x0200001E RID: 30
		internal struct CERT_CHAIN_ELEMENT
		{
			// Token: 0x040000E6 RID: 230
			public uint cbSize;

			// Token: 0x040000E7 RID: 231
			public IntPtr pCertContext;
		}

		// Token: 0x0200001F RID: 31
		internal struct SecPkgContext_IssuerListInfoEx
		{
			// Token: 0x06000029 RID: 41 RVA: 0x000022BC File Offset: 0x000004BC
			public unsafe SecPkgContext_IssuerListInfoEx(SafeHandle handle, byte[] nativeBuffer)
			{
				this.aIssuers = handle;
				fixed (byte[] array = nativeBuffer)
				{
					byte* ptr;
					if (nativeBuffer == null || array.Length == 0)
					{
						ptr = null;
					}
					else
					{
						ptr = &array[0];
					}
					this.cIssuers = *(uint*)(ptr + IntPtr.Size);
				}
			}

			// Token: 0x040000E8 RID: 232
			public SafeHandle aIssuers;

			// Token: 0x040000E9 RID: 233
			public uint cIssuers;
		}

		// Token: 0x02000020 RID: 32
		internal struct SCHANNEL_CRED
		{
			// Token: 0x040000EA RID: 234
			public const int CurrentVersion = 4;

			// Token: 0x040000EB RID: 235
			public int dwVersion;

			// Token: 0x040000EC RID: 236
			public int cCreds;

			// Token: 0x040000ED RID: 237
			public IntPtr paCred;

			// Token: 0x040000EE RID: 238
			public IntPtr hRootStore;

			// Token: 0x040000EF RID: 239
			public int cMappers;

			// Token: 0x040000F0 RID: 240
			public IntPtr aphMappers;

			// Token: 0x040000F1 RID: 241
			public int cSupportedAlgs;

			// Token: 0x040000F2 RID: 242
			public IntPtr palgSupportedAlgs;

			// Token: 0x040000F3 RID: 243
			public int grbitEnabledProtocols;

			// Token: 0x040000F4 RID: 244
			public int dwMinimumCipherStrength;

			// Token: 0x040000F5 RID: 245
			public int dwMaximumCipherStrength;

			// Token: 0x040000F6 RID: 246
			public int dwSessionLifespan;

			// Token: 0x040000F7 RID: 247
			public Interop.SspiCli.SCHANNEL_CRED.Flags dwFlags;

			// Token: 0x040000F8 RID: 248
			public int reserved;

			// Token: 0x02000021 RID: 33
			[Flags]
			public enum Flags
			{
				// Token: 0x040000FA RID: 250
				Zero = 0,
				// Token: 0x040000FB RID: 251
				SCH_CRED_NO_SYSTEM_MAPPER = 2,
				// Token: 0x040000FC RID: 252
				SCH_CRED_NO_SERVERNAME_CHECK = 4,
				// Token: 0x040000FD RID: 253
				SCH_CRED_MANUAL_CRED_VALIDATION = 8,
				// Token: 0x040000FE RID: 254
				SCH_CRED_NO_DEFAULT_CREDS = 16,
				// Token: 0x040000FF RID: 255
				SCH_CRED_AUTO_CRED_VALIDATION = 32,
				// Token: 0x04000100 RID: 256
				SCH_SEND_AUX_RECORD = 2097152,
				// Token: 0x04000101 RID: 257
				SCH_USE_STRONG_CRYPTO = 4194304
			}
		}

		// Token: 0x02000022 RID: 34
		internal struct SecBuffer
		{
			// Token: 0x0600002A RID: 42 RVA: 0x000022F8 File Offset: 0x000004F8
			// Note: this type is marked as 'beforefieldinit'.
			static SecBuffer()
			{
			}

			// Token: 0x04000102 RID: 258
			public int cbBuffer;

			// Token: 0x04000103 RID: 259
			public SecurityBufferType BufferType;

			// Token: 0x04000104 RID: 260
			public IntPtr pvBuffer;

			// Token: 0x04000105 RID: 261
			public static readonly int Size = sizeof(Interop.SspiCli.SecBuffer);
		}

		// Token: 0x02000023 RID: 35
		internal struct SecBufferDesc
		{
			// Token: 0x0600002B RID: 43 RVA: 0x00002305 File Offset: 0x00000505
			public SecBufferDesc(int count)
			{
				this.ulVersion = 0;
				this.cBuffers = count;
				this.pBuffers = null;
			}

			// Token: 0x04000106 RID: 262
			public readonly int ulVersion;

			// Token: 0x04000107 RID: 263
			public readonly int cBuffers;

			// Token: 0x04000108 RID: 264
			public unsafe void* pBuffers;
		}

		// Token: 0x02000024 RID: 36
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct SEC_WINNT_AUTH_IDENTITY_W
		{
			// Token: 0x04000109 RID: 265
			internal string User;

			// Token: 0x0400010A RID: 266
			internal int UserLength;

			// Token: 0x0400010B RID: 267
			internal string Domain;

			// Token: 0x0400010C RID: 268
			internal int DomainLength;

			// Token: 0x0400010D RID: 269
			internal string Password;

			// Token: 0x0400010E RID: 270
			internal int PasswordLength;

			// Token: 0x0400010F RID: 271
			internal int Flags;
		}
	}
}
