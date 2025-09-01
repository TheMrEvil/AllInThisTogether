using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace Unity.TLS.LowLevel
{
	// Token: 0x02000003 RID: 3
	[NativeHeader("External/unitytls/builds/CSharp/BindingsUnity/TLSAgent.gen.bindings.h")]
	internal static class Binding
	{
		// Token: 0x06000001 RID: 1
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern uint unitytls_client_send_data(Binding.unitytls_client* clientInstance, byte* data, UIntPtr dataLen);

		// Token: 0x06000002 RID: 2
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern uint unitytls_client_read_data(Binding.unitytls_client* clientInstance, byte* buffer, UIntPtr bufferLen, UIntPtr* bytesRead);

		// Token: 0x06000003 RID: 3
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void unitytls_client_add_ciphersuite(Binding.unitytls_client* clientInstance, uint suite);

		// Token: 0x06000004 RID: 4
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern uint unitytls_client_get_ciphersuite(Binding.unitytls_client* clientInstance, int ndx);

		// Token: 0x06000005 RID: 5
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern int unitytls_client_get_ciphersuite_cnt(Binding.unitytls_client* clientInstance);

		// Token: 0x06000006 RID: 6
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void unitytls_client_init_config(Binding.unitytls_client_config* config);

		// Token: 0x06000007 RID: 7
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern Binding.unitytls_client* unitytls_client_create(uint role, Binding.unitytls_client_config* config);

		// Token: 0x06000008 RID: 8
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void unitytls_client_destroy(Binding.unitytls_client* clientInstance);

		// Token: 0x06000009 RID: 9
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern int unitytls_client_init(Binding.unitytls_client* clientInstance);

		// Token: 0x0600000A RID: 10
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern uint unitytls_client_handshake(Binding.unitytls_client* clientInstance);

		// Token: 0x0600000B RID: 11
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern uint unitytls_client_set_cookie_info(Binding.unitytls_client* clientInstance, byte* peerIdDataPtr, int peerIdDataLen);

		// Token: 0x0600000C RID: 12
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern uint unitytls_client_get_handshake_state(Binding.unitytls_client* clientInstance);

		// Token: 0x0600000D RID: 13
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern uint unitytls_client_get_errorsState(Binding.unitytls_client* clientInstance, ulong* reserved);

		// Token: 0x0600000E RID: 14
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern uint unitytls_client_get_state(Binding.unitytls_client* clientInstance);

		// Token: 0x0600000F RID: 15
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern uint unitytls_client_get_role(Binding.unitytls_client* clientInstance);

		// Token: 0x04000001 RID: 1
		public const int UNITYTLS_SUCCESS = 0;

		// Token: 0x04000002 RID: 2
		public const int UNITYTLS_INVALID_ARGUMENT = 1;

		// Token: 0x04000003 RID: 3
		public const int UNITYTLS_INVALID_FORMAT = 2;

		// Token: 0x04000004 RID: 4
		public const int UNITYTLS_INVALID_PASSWORD = 3;

		// Token: 0x04000005 RID: 5
		public const int UNITYTLS_INVALID_STATE = 4;

		// Token: 0x04000006 RID: 6
		public const int UNITYTLS_BUFFER_OVERFLOW = 5;

		// Token: 0x04000007 RID: 7
		public const int UNITYTLS_OUT_OF_MEMORY = 6;

		// Token: 0x04000008 RID: 8
		public const int UNITYTLS_INTERNAL_ERROR = 7;

		// Token: 0x04000009 RID: 9
		public const int UNITYTLS_NOT_SUPPORTED = 8;

		// Token: 0x0400000A RID: 10
		public const int UNITYTLS_ENTROPY_SOURCE_FAILED = 9;

		// Token: 0x0400000B RID: 11
		public const int UNITYTLS_STREAM_CLOSED = 10;

		// Token: 0x0400000C RID: 12
		public const int UNITYTLS_DER_PARSE_ERROR = 11;

		// Token: 0x0400000D RID: 13
		public const int UNITYTLS_KEY_PARSE_ERROR = 12;

		// Token: 0x0400000E RID: 14
		public const int UNITYTLS_SSL_ERROR = 13;

		// Token: 0x0400000F RID: 15
		public const int UNITYTLS_USER_CUSTOM_ERROR_START = 1048576;

		// Token: 0x04000010 RID: 16
		public const int UNITYTLS_USER_WOULD_BLOCK = 1048577;

		// Token: 0x04000011 RID: 17
		public const int UNITYTLS_USER_WOULD_BLOCK_READ = 1048578;

		// Token: 0x04000012 RID: 18
		public const int UNITYTLS_USER_WOULD_BLOCK_WRITE = 1048579;

		// Token: 0x04000013 RID: 19
		public const int UNITYTLS_USER_READ_FAILED = 1048580;

		// Token: 0x04000014 RID: 20
		public const int UNITYTLS_USER_WRITE_FAILED = 1048581;

		// Token: 0x04000015 RID: 21
		public const int UNITYTLS_USER_UNKNOWN_ERROR = 1048582;

		// Token: 0x04000016 RID: 22
		public const int UNITYTLS_SSL_NEEDS_VERIFY = 1048583;

		// Token: 0x04000017 RID: 23
		public const int UNITYTLS_HANDSHAKE_STEP = 1048584;

		// Token: 0x04000018 RID: 24
		public const int UNITYTLS_USER_CUSTOM_ERROR_END = 2097152;

		// Token: 0x04000019 RID: 25
		public const int UNITYTLS_LOGLEVEL_MIN = 0;

		// Token: 0x0400001A RID: 26
		public const int UNITYTLS_LOGLEVEL_FATAL = 0;

		// Token: 0x0400001B RID: 27
		public const int UNITYTLS_LOGLEVEL_ERROR = 1;

		// Token: 0x0400001C RID: 28
		public const int UNITYTLS_LOGLEVEL_WARN = 2;

		// Token: 0x0400001D RID: 29
		public const int UNITYTLS_LOGLEVEL_INFO = 3;

		// Token: 0x0400001E RID: 30
		public const int UNITYTLS_LOGLEVEL_DEBUG = 4;

		// Token: 0x0400001F RID: 31
		public const int UNITYTLS_LOGLEVEL_TRACE = 5;

		// Token: 0x04000020 RID: 32
		public const int UNITYTLS_LOGLEVEL_MAX = 5;

		// Token: 0x04000021 RID: 33
		public const int UNITYTLS_SSL_HANDSHAKE_HELLO_REQUEST = 0;

		// Token: 0x04000022 RID: 34
		public const int UNITYTLS_SSL_HANDSHAKE_CLIENT_HELLO = 1;

		// Token: 0x04000023 RID: 35
		public const int UNITYTLS_SSL_HANDSHAKE_SERVER_HELLO = 2;

		// Token: 0x04000024 RID: 36
		public const int UNITYTLS_SSL_HANDSHAKE_SERVER_CERTIFICATE = 3;

		// Token: 0x04000025 RID: 37
		public const int UNITYTLS_SSL_HANDSHAKE_SERVER_KEY_EXCHANGE = 4;

		// Token: 0x04000026 RID: 38
		public const int UNITYTLS_SSL_HANDSHAKE_CERTIFICATE_REQUEST = 5;

		// Token: 0x04000027 RID: 39
		public const int UNITYTLS_SSL_HANDSHAKE_SERVER_HELLO_DONE = 6;

		// Token: 0x04000028 RID: 40
		public const int UNITYTLS_SSL_HANDSHAKE_CLIENT_CERTIFICATE = 7;

		// Token: 0x04000029 RID: 41
		public const int UNITYTLS_SSL_HANDSHAKE_CLIENT_KEY_EXCHANGE = 8;

		// Token: 0x0400002A RID: 42
		public const int UNITYTLS_SSL_HANDSHAKE_CERTIFICATE_VERIFY = 9;

		// Token: 0x0400002B RID: 43
		public const int UNITYTLS_SSL_HANDSHAKE_CLIENT_CHANGE_CIPHER_SPEC = 10;

		// Token: 0x0400002C RID: 44
		public const int UNITYTLS_SSL_HANDSHAKE_CLIENT_FINISHED = 11;

		// Token: 0x0400002D RID: 45
		public const int UNITYTLS_SSL_HANDSHAKE_SERVER_CHANGE_CIPHER_SPEC = 12;

		// Token: 0x0400002E RID: 46
		public const int UNITYTLS_SSL_HANDSHAKE_SERVER_FINISHED = 13;

		// Token: 0x0400002F RID: 47
		public const int UNITYTLS_SSL_HANDSHAKE_FLUSH_BUFFERS = 14;

		// Token: 0x04000030 RID: 48
		public const int UNITYTLS_SSL_HANDSHAKE_WRAPUP = 15;

		// Token: 0x04000031 RID: 49
		public const int UNITYTLS_SSL_HANDSHAKE_OVER = 16;

		// Token: 0x04000032 RID: 50
		public const int UNITYTLS_SSL_HANDSHAKE_SERVER_NEW_SESSION_TICKET = 17;

		// Token: 0x04000033 RID: 51
		public const int UNITYTLS_SSL_HANDSHAKE_HELLO_VERIFY_REQUIRED = 18;

		// Token: 0x04000034 RID: 52
		public const int UNITYTLS_SSL_HANDSHAKE_COUNT = 19;

		// Token: 0x04000035 RID: 53
		public const int UNITYTLS_SSL_HANDSHAKE_BEGIN = 0;

		// Token: 0x04000036 RID: 54
		public const int UNITYTLS_SSL_HANDSHAKE_DONE = 16;

		// Token: 0x04000037 RID: 55
		public const int UNITYTLS_SSL_HANDSHAKE_HANDSHAKE_FLUSH_BUFFERS = 14;

		// Token: 0x04000038 RID: 56
		public const int UNITYTLS_SSL_HANDSHAKE_HANDSHAKE_WRAPUP = 15;

		// Token: 0x04000039 RID: 57
		public const int UNITYTLS_SSL_HANDSHAKE_HANDSHAKE_OVER = 16;

		// Token: 0x0400003A RID: 58
		public const int UnityTLSClientAuth_None = 0;

		// Token: 0x0400003B RID: 59
		public const int UnityTLSClientAuth_Optional = 1;

		// Token: 0x0400003C RID: 60
		public const int UnityTLSClientAuth_Required = 2;

		// Token: 0x0400003D RID: 61
		public const int UnityTLSRole_None = 0;

		// Token: 0x0400003E RID: 62
		public const int UnityTLSRole_Server = 1;

		// Token: 0x0400003F RID: 63
		public const int UnityTLSRole_Client = 2;

		// Token: 0x04000040 RID: 64
		public const int UnityTLSTransportProtocol_Stream = 0;

		// Token: 0x04000041 RID: 65
		public const int UnityTLSTransportProtocol_Datagram = 1;

		// Token: 0x04000042 RID: 66
		public const int UnityTLSClientState_None = 0;

		// Token: 0x04000043 RID: 67
		public const int UnityTLSClientState_Init = 1;

		// Token: 0x04000044 RID: 68
		public const int UnityTLSClientState_Handshake = 2;

		// Token: 0x04000045 RID: 69
		public const int UnityTLSClientState_Messaging = 3;

		// Token: 0x04000046 RID: 70
		public const int UnityTLSClientState_Fail = 64;

		// Token: 0x02000004 RID: 4
		public struct unitytls_errorstate
		{
			// Token: 0x04000047 RID: 71
			public uint magic;

			// Token: 0x04000048 RID: 72
			public uint code;

			// Token: 0x04000049 RID: 73
			public ulong reserved;
		}

		// Token: 0x02000005 RID: 5
		public struct unitytls_dataRef
		{
			// Token: 0x0400004A RID: 74
			public unsafe byte* dataPtr;

			// Token: 0x0400004B RID: 75
			public UIntPtr dataLen;
		}

		// Token: 0x02000006 RID: 6
		// (Invoke) Token: 0x06000011 RID: 17
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public unsafe delegate void unitytls_client_on_data_callback(IntPtr arg0, byte* arg1, UIntPtr arg2, uint arg3);

		// Token: 0x02000007 RID: 7
		// (Invoke) Token: 0x06000015 RID: 21
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public unsafe delegate int unitytls_client_data_send_callback(IntPtr arg0, byte* arg1, UIntPtr arg2, uint arg3);

		// Token: 0x02000008 RID: 8
		// (Invoke) Token: 0x06000019 RID: 25
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public unsafe delegate int unitytls_client_data_receive_callback(IntPtr arg0, byte* arg1, UIntPtr arg2, uint arg3);

		// Token: 0x02000009 RID: 9
		// (Invoke) Token: 0x0600001D RID: 29
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public unsafe delegate int unitytls_client_data_receive_timeout_callback(IntPtr arg0, byte* arg1, UIntPtr arg2, uint arg3, uint arg4);

		// Token: 0x0200000A RID: 10
		// (Invoke) Token: 0x06000021 RID: 33
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public unsafe delegate void unitytls_client_log_callback(int arg0, byte* arg1, UIntPtr arg2, byte* arg3, byte* arg4, UIntPtr arg5);

		// Token: 0x0200000B RID: 11
		public struct unitytls_client_config
		{
			// Token: 0x0400004C RID: 76
			public Binding.unitytls_dataRef caPEM;

			// Token: 0x0400004D RID: 77
			public Binding.unitytls_dataRef serverPEM;

			// Token: 0x0400004E RID: 78
			public Binding.unitytls_dataRef privateKeyPEM;

			// Token: 0x0400004F RID: 79
			public uint clientAuth;

			// Token: 0x04000050 RID: 80
			public uint transportProtocol;

			// Token: 0x04000051 RID: 81
			public Binding.unitytls_dataRef psk;

			// Token: 0x04000052 RID: 82
			public Binding.unitytls_dataRef pskIdentity;

			// Token: 0x04000053 RID: 83
			public IntPtr onDataCB;

			// Token: 0x04000054 RID: 84
			public IntPtr dataSendCB;

			// Token: 0x04000055 RID: 85
			public IntPtr dataReceiveCB;

			// Token: 0x04000056 RID: 86
			public IntPtr dataReceiveTimeoutCB;

			// Token: 0x04000057 RID: 87
			public IntPtr transportUserData;

			// Token: 0x04000058 RID: 88
			public IntPtr applicationUserData;

			// Token: 0x04000059 RID: 89
			public int handshakeReturnsOnStep;

			// Token: 0x0400005A RID: 90
			public int handshakeReturnsIfWouldBlock;

			// Token: 0x0400005B RID: 91
			public uint ssl_read_timeout_ms;

			// Token: 0x0400005C RID: 92
			public unsafe byte* hostname;

			// Token: 0x0400005D RID: 93
			public uint tracelevel;

			// Token: 0x0400005E RID: 94
			public IntPtr logCallback;

			// Token: 0x0400005F RID: 95
			public uint ssl_handshake_timeout_min;

			// Token: 0x04000060 RID: 96
			public uint ssl_handshake_timeout_max;

			// Token: 0x04000061 RID: 97
			public ushort mtu;
		}

		// Token: 0x0200000C RID: 12
		public struct unitytls_client
		{
			// Token: 0x04000062 RID: 98
			public uint role;

			// Token: 0x04000063 RID: 99
			public uint state;

			// Token: 0x04000064 RID: 100
			public uint handshakeState;

			// Token: 0x04000065 RID: 101
			public IntPtr ctx;

			// Token: 0x04000066 RID: 102
			public unsafe Binding.unitytls_client_config* config;

			// Token: 0x04000067 RID: 103
			public IntPtr internalCtx;
		}

		// Token: 0x0200000D RID: 13
		// (Invoke) Token: 0x06000025 RID: 37
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public unsafe delegate int unitytls_tlsctx_handshake_on_blocking_callback(Binding.unitytls_client* arg0, IntPtr arg1, int arg2);
	}
}
