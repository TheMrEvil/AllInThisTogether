using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Unity
{
	// Token: 0x02000040 RID: 64
	internal static class UnityTls
	{
		// Token: 0x06000100 RID: 256
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetUnityTlsInterface();

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000101 RID: 257 RVA: 0x00003F07 File Offset: 0x00002107
		public static bool IsSupported
		{
			get
			{
				return UnityTls.NativeInterface != null;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000102 RID: 258 RVA: 0x00003F14 File Offset: 0x00002114
		public static UnityTls.unitytls_interface_struct NativeInterface
		{
			get
			{
				if (UnityTls.marshalledInterface == null)
				{
					IntPtr unityTlsInterface = UnityTls.GetUnityTlsInterface();
					if (unityTlsInterface == IntPtr.Zero)
					{
						return null;
					}
					UnityTls.marshalledInterface = Marshal.PtrToStructure<UnityTls.unitytls_interface_struct>(unityTlsInterface);
				}
				return UnityTls.marshalledInterface;
			}
		}

		// Token: 0x0400015B RID: 347
		private static UnityTls.unitytls_interface_struct marshalledInterface;

		// Token: 0x02000041 RID: 65
		public enum unitytls_error_code : uint
		{
			// Token: 0x0400015D RID: 349
			UNITYTLS_SUCCESS,
			// Token: 0x0400015E RID: 350
			UNITYTLS_INVALID_ARGUMENT,
			// Token: 0x0400015F RID: 351
			UNITYTLS_INVALID_FORMAT,
			// Token: 0x04000160 RID: 352
			UNITYTLS_INVALID_PASSWORD,
			// Token: 0x04000161 RID: 353
			UNITYTLS_INVALID_STATE,
			// Token: 0x04000162 RID: 354
			UNITYTLS_BUFFER_OVERFLOW,
			// Token: 0x04000163 RID: 355
			UNITYTLS_OUT_OF_MEMORY,
			// Token: 0x04000164 RID: 356
			UNITYTLS_INTERNAL_ERROR,
			// Token: 0x04000165 RID: 357
			UNITYTLS_NOT_SUPPORTED,
			// Token: 0x04000166 RID: 358
			UNITYTLS_ENTROPY_SOURCE_FAILED,
			// Token: 0x04000167 RID: 359
			UNITYTLS_STREAM_CLOSED,
			// Token: 0x04000168 RID: 360
			UNITYTLS_USER_CUSTOM_ERROR_START = 1048576U,
			// Token: 0x04000169 RID: 361
			UNITYTLS_USER_WOULD_BLOCK,
			// Token: 0x0400016A RID: 362
			UNITYTLS_USER_READ_FAILED,
			// Token: 0x0400016B RID: 363
			UNITYTLS_USER_WRITE_FAILED,
			// Token: 0x0400016C RID: 364
			UNITYTLS_USER_UNKNOWN_ERROR,
			// Token: 0x0400016D RID: 365
			UNITYTLS_USER_CUSTOM_ERROR_END = 2097152U
		}

		// Token: 0x02000042 RID: 66
		public struct unitytls_errorstate
		{
			// Token: 0x0400016E RID: 366
			private uint magic;

			// Token: 0x0400016F RID: 367
			public UnityTls.unitytls_error_code code;

			// Token: 0x04000170 RID: 368
			private ulong reserved;
		}

		// Token: 0x02000043 RID: 67
		public struct unitytls_key
		{
		}

		// Token: 0x02000044 RID: 68
		public struct unitytls_key_ref
		{
			// Token: 0x04000171 RID: 369
			public ulong handle;
		}

		// Token: 0x02000045 RID: 69
		public struct unitytls_x509
		{
		}

		// Token: 0x02000046 RID: 70
		public struct unitytls_x509_ref
		{
			// Token: 0x04000172 RID: 370
			public ulong handle;
		}

		// Token: 0x02000047 RID: 71
		public struct unitytls_x509list
		{
		}

		// Token: 0x02000048 RID: 72
		public struct unitytls_x509list_ref
		{
			// Token: 0x04000173 RID: 371
			public ulong handle;
		}

		// Token: 0x02000049 RID: 73
		[Flags]
		public enum unitytls_x509verify_result : uint
		{
			// Token: 0x04000175 RID: 373
			UNITYTLS_X509VERIFY_SUCCESS = 0U,
			// Token: 0x04000176 RID: 374
			UNITYTLS_X509VERIFY_NOT_DONE = 2147483648U,
			// Token: 0x04000177 RID: 375
			UNITYTLS_X509VERIFY_FATAL_ERROR = 4294967295U,
			// Token: 0x04000178 RID: 376
			UNITYTLS_X509VERIFY_FLAG_EXPIRED = 1U,
			// Token: 0x04000179 RID: 377
			UNITYTLS_X509VERIFY_FLAG_REVOKED = 2U,
			// Token: 0x0400017A RID: 378
			UNITYTLS_X509VERIFY_FLAG_CN_MISMATCH = 4U,
			// Token: 0x0400017B RID: 379
			UNITYTLS_X509VERIFY_FLAG_NOT_TRUSTED = 8U,
			// Token: 0x0400017C RID: 380
			UNITYTLS_X509VERIFY_FLAG_USER_ERROR1 = 65536U,
			// Token: 0x0400017D RID: 381
			UNITYTLS_X509VERIFY_FLAG_USER_ERROR2 = 131072U,
			// Token: 0x0400017E RID: 382
			UNITYTLS_X509VERIFY_FLAG_USER_ERROR3 = 262144U,
			// Token: 0x0400017F RID: 383
			UNITYTLS_X509VERIFY_FLAG_USER_ERROR4 = 524288U,
			// Token: 0x04000180 RID: 384
			UNITYTLS_X509VERIFY_FLAG_USER_ERROR5 = 1048576U,
			// Token: 0x04000181 RID: 385
			UNITYTLS_X509VERIFY_FLAG_USER_ERROR6 = 2097152U,
			// Token: 0x04000182 RID: 386
			UNITYTLS_X509VERIFY_FLAG_USER_ERROR7 = 4194304U,
			// Token: 0x04000183 RID: 387
			UNITYTLS_X509VERIFY_FLAG_USER_ERROR8 = 8388608U,
			// Token: 0x04000184 RID: 388
			UNITYTLS_X509VERIFY_FLAG_UNKNOWN_ERROR = 134217728U
		}

		// Token: 0x0200004A RID: 74
		// (Invoke) Token: 0x06000104 RID: 260
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public unsafe delegate UnityTls.unitytls_x509verify_result unitytls_x509verify_callback(void* userData, UnityTls.unitytls_x509_ref cert, UnityTls.unitytls_x509verify_result result, UnityTls.unitytls_errorstate* errorState);

		// Token: 0x0200004B RID: 75
		public struct unitytls_tlsctx
		{
		}

		// Token: 0x0200004C RID: 76
		public struct unitytls_tlsctx_ref
		{
			// Token: 0x04000185 RID: 389
			public ulong handle;
		}

		// Token: 0x0200004D RID: 77
		public struct unitytls_x509name
		{
		}

		// Token: 0x0200004E RID: 78
		public enum unitytls_ciphersuite : uint
		{
			// Token: 0x04000187 RID: 391
			UNITYTLS_CIPHERSUITE_INVALID = 16777215U
		}

		// Token: 0x0200004F RID: 79
		public enum unitytls_protocol : uint
		{
			// Token: 0x04000189 RID: 393
			UNITYTLS_PROTOCOL_TLS_1_0,
			// Token: 0x0400018A RID: 394
			UNITYTLS_PROTOCOL_TLS_1_1,
			// Token: 0x0400018B RID: 395
			UNITYTLS_PROTOCOL_TLS_1_2,
			// Token: 0x0400018C RID: 396
			UNITYTLS_PROTOCOL_INVALID
		}

		// Token: 0x02000050 RID: 80
		public struct unitytls_tlsctx_protocolrange
		{
			// Token: 0x0400018D RID: 397
			public UnityTls.unitytls_protocol min;

			// Token: 0x0400018E RID: 398
			public UnityTls.unitytls_protocol max;
		}

		// Token: 0x02000051 RID: 81
		// (Invoke) Token: 0x06000108 RID: 264
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public unsafe delegate IntPtr unitytls_tlsctx_write_callback(void* userData, byte* data, IntPtr bufferLen, UnityTls.unitytls_errorstate* errorState);

		// Token: 0x02000052 RID: 82
		// (Invoke) Token: 0x0600010C RID: 268
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public unsafe delegate IntPtr unitytls_tlsctx_read_callback(void* userData, byte* buffer, IntPtr bufferLen, UnityTls.unitytls_errorstate* errorState);

		// Token: 0x02000053 RID: 83
		// (Invoke) Token: 0x06000110 RID: 272
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public unsafe delegate void unitytls_tlsctx_trace_callback(void* userData, UnityTls.unitytls_tlsctx* ctx, byte* traceMessage, IntPtr traceMessageLen);

		// Token: 0x02000054 RID: 84
		// (Invoke) Token: 0x06000114 RID: 276
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public unsafe delegate void unitytls_tlsctx_certificate_callback(void* userData, UnityTls.unitytls_tlsctx* ctx, byte* cn, IntPtr cnLen, UnityTls.unitytls_x509name* caList, IntPtr caListLen, UnityTls.unitytls_x509list_ref* chain, UnityTls.unitytls_key_ref* key, UnityTls.unitytls_errorstate* errorState);

		// Token: 0x02000055 RID: 85
		// (Invoke) Token: 0x06000118 RID: 280
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public unsafe delegate UnityTls.unitytls_x509verify_result unitytls_tlsctx_x509verify_callback(void* userData, UnityTls.unitytls_x509list_ref chain, UnityTls.unitytls_errorstate* errorState);

		// Token: 0x02000056 RID: 86
		public struct unitytls_tlsctx_callbacks
		{
			// Token: 0x0400018F RID: 399
			public UnityTls.unitytls_tlsctx_read_callback read;

			// Token: 0x04000190 RID: 400
			public UnityTls.unitytls_tlsctx_write_callback write;

			// Token: 0x04000191 RID: 401
			public unsafe void* data;
		}

		// Token: 0x02000057 RID: 87
		[StructLayout(LayoutKind.Sequential)]
		public class unitytls_interface_struct
		{
			// Token: 0x0600011B RID: 283 RVA: 0x0000219B File Offset: 0x0000039B
			public unitytls_interface_struct()
			{
			}

			// Token: 0x04000192 RID: 402
			public readonly ulong UNITYTLS_INVALID_HANDLE;

			// Token: 0x04000193 RID: 403
			public readonly UnityTls.unitytls_tlsctx_protocolrange UNITYTLS_TLSCTX_PROTOCOLRANGE_DEFAULT;

			// Token: 0x04000194 RID: 404
			public UnityTls.unitytls_interface_struct.unitytls_errorstate_create_t unitytls_errorstate_create;

			// Token: 0x04000195 RID: 405
			public UnityTls.unitytls_interface_struct.unitytls_errorstate_raise_error_t unitytls_errorstate_raise_error;

			// Token: 0x04000196 RID: 406
			public UnityTls.unitytls_interface_struct.unitytls_key_get_ref_t unitytls_key_get_ref;

			// Token: 0x04000197 RID: 407
			public UnityTls.unitytls_interface_struct.unitytls_key_parse_der_t unitytls_key_parse_der;

			// Token: 0x04000198 RID: 408
			public UnityTls.unitytls_interface_struct.unitytls_key_parse_pem_t unitytls_key_parse_pem;

			// Token: 0x04000199 RID: 409
			public UnityTls.unitytls_interface_struct.unitytls_key_free_t unitytls_key_free;

			// Token: 0x0400019A RID: 410
			public UnityTls.unitytls_interface_struct.unitytls_x509_export_der_t unitytls_x509_export_der;

			// Token: 0x0400019B RID: 411
			public UnityTls.unitytls_interface_struct.unitytls_x509list_get_ref_t unitytls_x509list_get_ref;

			// Token: 0x0400019C RID: 412
			public UnityTls.unitytls_interface_struct.unitytls_x509list_get_x509_t unitytls_x509list_get_x509;

			// Token: 0x0400019D RID: 413
			public UnityTls.unitytls_interface_struct.unitytls_x509list_create_t unitytls_x509list_create;

			// Token: 0x0400019E RID: 414
			public UnityTls.unitytls_interface_struct.unitytls_x509list_append_t unitytls_x509list_append;

			// Token: 0x0400019F RID: 415
			public UnityTls.unitytls_interface_struct.unitytls_x509list_append_der_t unitytls_x509list_append_der;

			// Token: 0x040001A0 RID: 416
			public UnityTls.unitytls_interface_struct.unitytls_x509list_append_der_t unitytls_x509list_append_pem;

			// Token: 0x040001A1 RID: 417
			public UnityTls.unitytls_interface_struct.unitytls_x509list_free_t unitytls_x509list_free;

			// Token: 0x040001A2 RID: 418
			public UnityTls.unitytls_interface_struct.unitytls_x509verify_default_ca_t unitytls_x509verify_default_ca;

			// Token: 0x040001A3 RID: 419
			public UnityTls.unitytls_interface_struct.unitytls_x509verify_explicit_ca_t unitytls_x509verify_explicit_ca;

			// Token: 0x040001A4 RID: 420
			public UnityTls.unitytls_interface_struct.unitytls_tlsctx_create_server_t unitytls_tlsctx_create_server;

			// Token: 0x040001A5 RID: 421
			public UnityTls.unitytls_interface_struct.unitytls_tlsctx_create_client_t unitytls_tlsctx_create_client;

			// Token: 0x040001A6 RID: 422
			public UnityTls.unitytls_interface_struct.unitytls_tlsctx_server_require_client_authentication_t unitytls_tlsctx_server_require_client_authentication;

			// Token: 0x040001A7 RID: 423
			public UnityTls.unitytls_interface_struct.unitytls_tlsctx_set_certificate_callback_t unitytls_tlsctx_set_certificate_callback;

			// Token: 0x040001A8 RID: 424
			public UnityTls.unitytls_interface_struct.unitytls_tlsctx_set_trace_callback_t unitytls_tlsctx_set_trace_callback;

			// Token: 0x040001A9 RID: 425
			public UnityTls.unitytls_interface_struct.unitytls_tlsctx_set_x509verify_callback_t unitytls_tlsctx_set_x509verify_callback;

			// Token: 0x040001AA RID: 426
			public UnityTls.unitytls_interface_struct.unitytls_tlsctx_set_supported_ciphersuites_t unitytls_tlsctx_set_supported_ciphersuites;

			// Token: 0x040001AB RID: 427
			public UnityTls.unitytls_interface_struct.unitytls_tlsctx_get_ciphersuite_t unitytls_tlsctx_get_ciphersuite;

			// Token: 0x040001AC RID: 428
			public UnityTls.unitytls_interface_struct.unitytls_tlsctx_get_protocol_t unitytls_tlsctx_get_protocol;

			// Token: 0x040001AD RID: 429
			public UnityTls.unitytls_interface_struct.unitytls_tlsctx_process_handshake_t unitytls_tlsctx_process_handshake;

			// Token: 0x040001AE RID: 430
			public UnityTls.unitytls_interface_struct.unitytls_tlsctx_read_t unitytls_tlsctx_read;

			// Token: 0x040001AF RID: 431
			public UnityTls.unitytls_interface_struct.unitytls_tlsctx_write_t unitytls_tlsctx_write;

			// Token: 0x040001B0 RID: 432
			public UnityTls.unitytls_interface_struct.unitytls_tlsctx_notify_close_t unitytls_tlsctx_notify_close;

			// Token: 0x040001B1 RID: 433
			public UnityTls.unitytls_interface_struct.unitytls_tlsctx_free_t unitytls_tlsctx_free;

			// Token: 0x040001B2 RID: 434
			public UnityTls.unitytls_interface_struct.unitytls_random_generate_bytes_t unitytls_random_generate_bytes;

			// Token: 0x02000058 RID: 88
			// (Invoke) Token: 0x0600011D RID: 285
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public delegate UnityTls.unitytls_errorstate unitytls_errorstate_create_t();

			// Token: 0x02000059 RID: 89
			// (Invoke) Token: 0x06000121 RID: 289
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate void unitytls_errorstate_raise_error_t(UnityTls.unitytls_errorstate* errorState, UnityTls.unitytls_error_code errorCode);

			// Token: 0x0200005A RID: 90
			// (Invoke) Token: 0x06000125 RID: 293
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate UnityTls.unitytls_key_ref unitytls_key_get_ref_t(UnityTls.unitytls_key* key, UnityTls.unitytls_errorstate* errorState);

			// Token: 0x0200005B RID: 91
			// (Invoke) Token: 0x06000129 RID: 297
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate UnityTls.unitytls_key* unitytls_key_parse_der_t(byte* buffer, IntPtr bufferLen, byte* password, IntPtr passwordLen, UnityTls.unitytls_errorstate* errorState);

			// Token: 0x0200005C RID: 92
			// (Invoke) Token: 0x0600012D RID: 301
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate UnityTls.unitytls_key* unitytls_key_parse_pem_t(byte* buffer, IntPtr bufferLen, byte* password, IntPtr passwordLen, UnityTls.unitytls_errorstate* errorState);

			// Token: 0x0200005D RID: 93
			// (Invoke) Token: 0x06000131 RID: 305
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate void unitytls_key_free_t(UnityTls.unitytls_key* key);

			// Token: 0x0200005E RID: 94
			// (Invoke) Token: 0x06000135 RID: 309
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate IntPtr unitytls_x509_export_der_t(UnityTls.unitytls_x509_ref cert, byte* buffer, IntPtr bufferLen, UnityTls.unitytls_errorstate* errorState);

			// Token: 0x0200005F RID: 95
			// (Invoke) Token: 0x06000139 RID: 313
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate UnityTls.unitytls_x509list_ref unitytls_x509list_get_ref_t(UnityTls.unitytls_x509list* list, UnityTls.unitytls_errorstate* errorState);

			// Token: 0x02000060 RID: 96
			// (Invoke) Token: 0x0600013D RID: 317
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate UnityTls.unitytls_x509_ref unitytls_x509list_get_x509_t(UnityTls.unitytls_x509list_ref list, IntPtr index, UnityTls.unitytls_errorstate* errorState);

			// Token: 0x02000061 RID: 97
			// (Invoke) Token: 0x06000141 RID: 321
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate UnityTls.unitytls_x509list* unitytls_x509list_create_t(UnityTls.unitytls_errorstate* errorState);

			// Token: 0x02000062 RID: 98
			// (Invoke) Token: 0x06000145 RID: 325
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate void unitytls_x509list_append_t(UnityTls.unitytls_x509list* list, UnityTls.unitytls_x509_ref cert, UnityTls.unitytls_errorstate* errorState);

			// Token: 0x02000063 RID: 99
			// (Invoke) Token: 0x06000149 RID: 329
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate void unitytls_x509list_append_der_t(UnityTls.unitytls_x509list* list, byte* buffer, IntPtr bufferLen, UnityTls.unitytls_errorstate* errorState);

			// Token: 0x02000064 RID: 100
			// (Invoke) Token: 0x0600014D RID: 333
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate void unitytls_x509list_append_pem_t(UnityTls.unitytls_x509list* list, byte* buffer, IntPtr bufferLen, UnityTls.unitytls_errorstate* errorState);

			// Token: 0x02000065 RID: 101
			// (Invoke) Token: 0x06000151 RID: 337
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate void unitytls_x509list_free_t(UnityTls.unitytls_x509list* list);

			// Token: 0x02000066 RID: 102
			// (Invoke) Token: 0x06000155 RID: 341
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate UnityTls.unitytls_x509verify_result unitytls_x509verify_default_ca_t(UnityTls.unitytls_x509list_ref chain, byte* cn, IntPtr cnLen, UnityTls.unitytls_x509verify_callback cb, void* userData, UnityTls.unitytls_errorstate* errorState);

			// Token: 0x02000067 RID: 103
			// (Invoke) Token: 0x06000159 RID: 345
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate UnityTls.unitytls_x509verify_result unitytls_x509verify_explicit_ca_t(UnityTls.unitytls_x509list_ref chain, UnityTls.unitytls_x509list_ref trustCA, byte* cn, IntPtr cnLen, UnityTls.unitytls_x509verify_callback cb, void* userData, UnityTls.unitytls_errorstate* errorState);

			// Token: 0x02000068 RID: 104
			// (Invoke) Token: 0x0600015D RID: 349
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate UnityTls.unitytls_tlsctx* unitytls_tlsctx_create_server_t(UnityTls.unitytls_tlsctx_protocolrange supportedProtocols, UnityTls.unitytls_tlsctx_callbacks callbacks, ulong certChain, ulong leafCertificateKey, UnityTls.unitytls_errorstate* errorState);

			// Token: 0x02000069 RID: 105
			// (Invoke) Token: 0x06000161 RID: 353
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate UnityTls.unitytls_tlsctx* unitytls_tlsctx_create_client_t(UnityTls.unitytls_tlsctx_protocolrange supportedProtocols, UnityTls.unitytls_tlsctx_callbacks callbacks, byte* cn, IntPtr cnLen, UnityTls.unitytls_errorstate* errorState);

			// Token: 0x0200006A RID: 106
			// (Invoke) Token: 0x06000165 RID: 357
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate void unitytls_tlsctx_server_require_client_authentication_t(UnityTls.unitytls_tlsctx* ctx, UnityTls.unitytls_x509list_ref clientAuthCAList, UnityTls.unitytls_errorstate* errorState);

			// Token: 0x0200006B RID: 107
			// (Invoke) Token: 0x06000169 RID: 361
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate void unitytls_tlsctx_set_certificate_callback_t(UnityTls.unitytls_tlsctx* ctx, UnityTls.unitytls_tlsctx_certificate_callback cb, void* userData, UnityTls.unitytls_errorstate* errorState);

			// Token: 0x0200006C RID: 108
			// (Invoke) Token: 0x0600016D RID: 365
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate void unitytls_tlsctx_set_trace_callback_t(UnityTls.unitytls_tlsctx* ctx, UnityTls.unitytls_tlsctx_trace_callback cb, void* userData, UnityTls.unitytls_errorstate* errorState);

			// Token: 0x0200006D RID: 109
			// (Invoke) Token: 0x06000171 RID: 369
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate void unitytls_tlsctx_set_x509verify_callback_t(UnityTls.unitytls_tlsctx* ctx, UnityTls.unitytls_tlsctx_x509verify_callback cb, void* userData, UnityTls.unitytls_errorstate* errorState);

			// Token: 0x0200006E RID: 110
			// (Invoke) Token: 0x06000175 RID: 373
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate void unitytls_tlsctx_set_supported_ciphersuites_t(UnityTls.unitytls_tlsctx* ctx, UnityTls.unitytls_ciphersuite* supportedCiphersuites, IntPtr supportedCiphersuitesLen, UnityTls.unitytls_errorstate* errorState);

			// Token: 0x0200006F RID: 111
			// (Invoke) Token: 0x06000179 RID: 377
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate UnityTls.unitytls_ciphersuite unitytls_tlsctx_get_ciphersuite_t(UnityTls.unitytls_tlsctx* ctx, UnityTls.unitytls_errorstate* errorState);

			// Token: 0x02000070 RID: 112
			// (Invoke) Token: 0x0600017D RID: 381
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate UnityTls.unitytls_protocol unitytls_tlsctx_get_protocol_t(UnityTls.unitytls_tlsctx* ctx, UnityTls.unitytls_errorstate* errorState);

			// Token: 0x02000071 RID: 113
			// (Invoke) Token: 0x06000181 RID: 385
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate UnityTls.unitytls_x509verify_result unitytls_tlsctx_process_handshake_t(UnityTls.unitytls_tlsctx* ctx, UnityTls.unitytls_errorstate* errorState);

			// Token: 0x02000072 RID: 114
			// (Invoke) Token: 0x06000185 RID: 389
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate IntPtr unitytls_tlsctx_read_t(UnityTls.unitytls_tlsctx* ctx, byte* buffer, IntPtr bufferLen, UnityTls.unitytls_errorstate* errorState);

			// Token: 0x02000073 RID: 115
			// (Invoke) Token: 0x06000189 RID: 393
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate IntPtr unitytls_tlsctx_write_t(UnityTls.unitytls_tlsctx* ctx, byte* data, IntPtr bufferLen, UnityTls.unitytls_errorstate* errorState);

			// Token: 0x02000074 RID: 116
			// (Invoke) Token: 0x0600018D RID: 397
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate void unitytls_tlsctx_notify_close_t(UnityTls.unitytls_tlsctx* ctx, UnityTls.unitytls_errorstate* errorState);

			// Token: 0x02000075 RID: 117
			// (Invoke) Token: 0x06000191 RID: 401
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate void unitytls_tlsctx_free_t(UnityTls.unitytls_tlsctx* ctx);

			// Token: 0x02000076 RID: 118
			// (Invoke) Token: 0x06000195 RID: 405
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate void unitytls_random_generate_bytes_t(byte* buffer, IntPtr bufferLen, UnityTls.unitytls_errorstate* errorState);
		}
	}
}
