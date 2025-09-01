using System;
using System.Data.Common;
using System.Runtime.InteropServices;

namespace System.Data.SqlClient
{
	// Token: 0x02000179 RID: 377
	internal static class SNINativeMethodWrapper
	{
		// Token: 0x1700035F RID: 863
		// (get) Token: 0x060013EF RID: 5103 RVA: 0x0005B19C File Offset: 0x0005939C
		internal static int SniMaxComposedSpnLength
		{
			get
			{
				if (SNINativeMethodWrapper.s_sniMaxComposedSpnLength == -1)
				{
					SNINativeMethodWrapper.s_sniMaxComposedSpnLength = checked((int)SNINativeMethodWrapper.GetSniMaxComposedSpnLength());
				}
				return SNINativeMethodWrapper.s_sniMaxComposedSpnLength;
			}
		}

		// Token: 0x060013F0 RID: 5104
		[DllImport("sni.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNIAddProviderWrapper")]
		internal static extern uint SNIAddProvider(SNIHandle pConn, SNINativeMethodWrapper.ProviderEnum ProvNum, [In] ref uint pInfo);

		// Token: 0x060013F1 RID: 5105
		[DllImport("sni.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNICheckConnectionWrapper")]
		internal static extern uint SNICheckConnection([In] SNIHandle pConn);

		// Token: 0x060013F2 RID: 5106
		[DllImport("sni.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNICloseWrapper")]
		internal static extern uint SNIClose(IntPtr pConn);

		// Token: 0x060013F3 RID: 5107
		[DllImport("sni.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void SNIGetLastError(out SNINativeMethodWrapper.SNI_Error pErrorStruct);

		// Token: 0x060013F4 RID: 5108
		[DllImport("sni.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void SNIPacketRelease(IntPtr pPacket);

		// Token: 0x060013F5 RID: 5109
		[DllImport("sni.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNIPacketResetWrapper")]
		internal static extern void SNIPacketReset([In] SNIHandle pConn, SNINativeMethodWrapper.IOType IOType, SNIPacket pPacket, SNINativeMethodWrapper.ConsumerNumber ConsNum);

		// Token: 0x060013F6 RID: 5110
		[DllImport("sni.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern uint SNIQueryInfo(SNINativeMethodWrapper.QTypes QType, ref uint pbQInfo);

		// Token: 0x060013F7 RID: 5111
		[DllImport("sni.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern uint SNIQueryInfo(SNINativeMethodWrapper.QTypes QType, ref IntPtr pbQInfo);

		// Token: 0x060013F8 RID: 5112
		[DllImport("sni.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNIReadAsyncWrapper")]
		internal static extern uint SNIReadAsync(SNIHandle pConn, ref IntPtr ppNewPacket);

		// Token: 0x060013F9 RID: 5113
		[DllImport("sni.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern uint SNIReadSyncOverAsync(SNIHandle pConn, ref IntPtr ppNewPacket, int timeout);

		// Token: 0x060013FA RID: 5114
		[DllImport("sni.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNIRemoveProviderWrapper")]
		internal static extern uint SNIRemoveProvider(SNIHandle pConn, SNINativeMethodWrapper.ProviderEnum ProvNum);

		// Token: 0x060013FB RID: 5115
		[DllImport("sni.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern uint SNISecInitPackage(ref uint pcbMaxToken);

		// Token: 0x060013FC RID: 5116
		[DllImport("sni.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNISetInfoWrapper")]
		internal static extern uint SNISetInfo(SNIHandle pConn, SNINativeMethodWrapper.QTypes QType, [In] ref uint pbQInfo);

		// Token: 0x060013FD RID: 5117
		[DllImport("sni.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern uint SNITerminate();

		// Token: 0x060013FE RID: 5118
		[DllImport("sni.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNIWaitForSSLHandshakeToCompleteWrapper")]
		internal static extern uint SNIWaitForSSLHandshakeToComplete([In] SNIHandle pConn, int dwMilliseconds);

		// Token: 0x060013FF RID: 5119
		[DllImport("sni.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern uint UnmanagedIsTokenRestricted([In] IntPtr token, [MarshalAs(UnmanagedType.Bool)] out bool isRestricted);

		// Token: 0x06001400 RID: 5120
		[DllImport("sni.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern uint GetSniMaxComposedSpnLength();

		// Token: 0x06001401 RID: 5121
		[DllImport("sni.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern uint SNIGetInfoWrapper([In] SNIHandle pConn, SNINativeMethodWrapper.QTypes QType, out Guid pbQInfo);

		// Token: 0x06001402 RID: 5122
		[DllImport("sni.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern uint SNIInitialize([In] IntPtr pmo);

		// Token: 0x06001403 RID: 5123
		[DllImport("sni.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern uint SNIOpenSyncExWrapper(ref SNINativeMethodWrapper.SNI_CLIENT_CONSUMER_INFO pClientConsumerInfo, out IntPtr ppConn);

		// Token: 0x06001404 RID: 5124
		[DllImport("sni.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern uint SNIOpenWrapper([In] ref SNINativeMethodWrapper.Sni_Consumer_Info pConsumerInfo, [MarshalAs(UnmanagedType.LPStr)] string szConnect, [In] SNIHandle pConn, out IntPtr ppConn, [MarshalAs(UnmanagedType.Bool)] bool fSync);

		// Token: 0x06001405 RID: 5125
		[DllImport("sni.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr SNIPacketAllocateWrapper([In] SafeHandle pConn, SNINativeMethodWrapper.IOType IOType);

		// Token: 0x06001406 RID: 5126
		[DllImport("sni.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern uint SNIPacketGetDataWrapper([In] IntPtr packet, [In] [Out] byte[] readBuffer, uint readBufferLength, out uint dataSize);

		// Token: 0x06001407 RID: 5127
		[DllImport("sni.dll", CallingConvention = CallingConvention.Cdecl)]
		private unsafe static extern void SNIPacketSetData(SNIPacket pPacket, [In] byte* pbBuf, uint cbBuf);

		// Token: 0x06001408 RID: 5128
		[DllImport("sni.dll", CallingConvention = CallingConvention.Cdecl)]
		private unsafe static extern uint SNISecGenClientContextWrapper([In] SNIHandle pConn, [In] [Out] byte[] pIn, uint cbIn, [In] [Out] byte[] pOut, [In] ref uint pcbOut, [MarshalAs(UnmanagedType.Bool)] out bool pfDone, byte* szServerInfo, uint cbServerInfo, [MarshalAs(UnmanagedType.LPWStr)] string pwszUserName, [MarshalAs(UnmanagedType.LPWStr)] string pwszPassword);

		// Token: 0x06001409 RID: 5129
		[DllImport("sni.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern uint SNIWriteAsyncWrapper(SNIHandle pConn, [In] SNIPacket pPacket);

		// Token: 0x0600140A RID: 5130
		[DllImport("sni.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern uint SNIWriteSyncOverAsync(SNIHandle pConn, [In] SNIPacket pPacket);

		// Token: 0x0600140B RID: 5131 RVA: 0x0005B1B6 File Offset: 0x000593B6
		internal static uint SniGetConnectionId(SNIHandle pConn, ref Guid connId)
		{
			return SNINativeMethodWrapper.SNIGetInfoWrapper(pConn, SNINativeMethodWrapper.QTypes.SNI_QUERY_CONN_CONNID, out connId);
		}

		// Token: 0x0600140C RID: 5132 RVA: 0x0005B1C1 File Offset: 0x000593C1
		internal static uint SNIInitialize()
		{
			return SNINativeMethodWrapper.SNIInitialize(IntPtr.Zero);
		}

		// Token: 0x0600140D RID: 5133 RVA: 0x0005B1D0 File Offset: 0x000593D0
		internal static uint SNIOpenMarsSession(SNINativeMethodWrapper.ConsumerInfo consumerInfo, SNIHandle parent, ref IntPtr pConn, bool fSync)
		{
			SNINativeMethodWrapper.Sni_Consumer_Info sni_Consumer_Info = default(SNINativeMethodWrapper.Sni_Consumer_Info);
			SNINativeMethodWrapper.MarshalConsumerInfo(consumerInfo, ref sni_Consumer_Info);
			return SNINativeMethodWrapper.SNIOpenWrapper(ref sni_Consumer_Info, "session:", parent, out pConn, fSync);
		}

		// Token: 0x0600140E RID: 5134 RVA: 0x0005B1FC File Offset: 0x000593FC
		internal unsafe static uint SNIOpenSyncEx(SNINativeMethodWrapper.ConsumerInfo consumerInfo, string constring, ref IntPtr pConn, byte[] spnBuffer, byte[] instanceName, bool fOverrideCache, bool fSync, int timeout, bool fParallel)
		{
			fixed (byte* ptr = &instanceName[0])
			{
				byte* szInstanceName = ptr;
				SNINativeMethodWrapper.SNI_CLIENT_CONSUMER_INFO sni_CLIENT_CONSUMER_INFO = default(SNINativeMethodWrapper.SNI_CLIENT_CONSUMER_INFO);
				SNINativeMethodWrapper.MarshalConsumerInfo(consumerInfo, ref sni_CLIENT_CONSUMER_INFO.ConsumerInfo);
				sni_CLIENT_CONSUMER_INFO.wszConnectionString = constring;
				sni_CLIENT_CONSUMER_INFO.networkLibrary = SNINativeMethodWrapper.PrefixEnum.UNKNOWN_PREFIX;
				sni_CLIENT_CONSUMER_INFO.szInstanceName = szInstanceName;
				sni_CLIENT_CONSUMER_INFO.cchInstanceName = (uint)instanceName.Length;
				sni_CLIENT_CONSUMER_INFO.fOverrideLastConnectCache = fOverrideCache;
				sni_CLIENT_CONSUMER_INFO.fSynchronousConnection = fSync;
				sni_CLIENT_CONSUMER_INFO.timeout = timeout;
				sni_CLIENT_CONSUMER_INFO.fParallel = fParallel;
				sni_CLIENT_CONSUMER_INFO.transparentNetworkResolution = SNINativeMethodWrapper.TransparentNetworkResolutionMode.DisabledMode;
				sni_CLIENT_CONSUMER_INFO.totalTimeout = -1;
				sni_CLIENT_CONSUMER_INFO.isAzureSqlServerEndpoint = ADP.IsAzureSqlServerEndpoint(constring);
				if (spnBuffer != null)
				{
					fixed (byte* ptr2 = &spnBuffer[0])
					{
						byte* szSPN = ptr2;
						sni_CLIENT_CONSUMER_INFO.szSPN = szSPN;
						sni_CLIENT_CONSUMER_INFO.cchSPN = (uint)spnBuffer.Length;
						return SNINativeMethodWrapper.SNIOpenSyncExWrapper(ref sni_CLIENT_CONSUMER_INFO, out pConn);
					}
				}
				return SNINativeMethodWrapper.SNIOpenSyncExWrapper(ref sni_CLIENT_CONSUMER_INFO, out pConn);
			}
		}

		// Token: 0x0600140F RID: 5135 RVA: 0x0005B2C1 File Offset: 0x000594C1
		internal static void SNIPacketAllocate(SafeHandle pConn, SNINativeMethodWrapper.IOType IOType, ref IntPtr pPacket)
		{
			pPacket = SNINativeMethodWrapper.SNIPacketAllocateWrapper(pConn, IOType);
		}

		// Token: 0x06001410 RID: 5136 RVA: 0x0005B2CC File Offset: 0x000594CC
		internal static uint SNIPacketGetData(IntPtr packet, byte[] readBuffer, ref uint dataSize)
		{
			return SNINativeMethodWrapper.SNIPacketGetDataWrapper(packet, readBuffer, (uint)readBuffer.Length, out dataSize);
		}

		// Token: 0x06001411 RID: 5137 RVA: 0x0005B2DC File Offset: 0x000594DC
		internal unsafe static void SNIPacketSetData(SNIPacket packet, byte[] data, int length)
		{
			fixed (byte* ptr = &data[0])
			{
				byte* pbBuf = ptr;
				SNINativeMethodWrapper.SNIPacketSetData(packet, pbBuf, (uint)length);
			}
		}

		// Token: 0x06001412 RID: 5138 RVA: 0x0005B300 File Offset: 0x00059500
		internal unsafe static uint SNISecGenClientContext(SNIHandle pConnectionObject, byte[] inBuff, uint receivedLength, byte[] OutBuff, ref uint sendLength, byte[] serverUserName)
		{
			fixed (byte* ptr = &serverUserName[0])
			{
				byte* szServerInfo = ptr;
				bool flag;
				return SNINativeMethodWrapper.SNISecGenClientContextWrapper(pConnectionObject, inBuff, receivedLength, OutBuff, ref sendLength, out flag, szServerInfo, (uint)serverUserName.Length, null, null);
			}
		}

		// Token: 0x06001413 RID: 5139 RVA: 0x0005B32D File Offset: 0x0005952D
		internal static uint SNIWritePacket(SNIHandle pConn, SNIPacket packet, bool sync)
		{
			if (sync)
			{
				return SNINativeMethodWrapper.SNIWriteSyncOverAsync(pConn, packet);
			}
			return SNINativeMethodWrapper.SNIWriteAsyncWrapper(pConn, packet);
		}

		// Token: 0x06001414 RID: 5140 RVA: 0x0005B344 File Offset: 0x00059544
		private static void MarshalConsumerInfo(SNINativeMethodWrapper.ConsumerInfo consumerInfo, ref SNINativeMethodWrapper.Sni_Consumer_Info native_consumerInfo)
		{
			native_consumerInfo.DefaultUserDataLength = consumerInfo.defaultBufferSize;
			native_consumerInfo.fnReadComp = ((consumerInfo.readDelegate != null) ? Marshal.GetFunctionPointerForDelegate<SNINativeMethodWrapper.SqlAsyncCallbackDelegate>(consumerInfo.readDelegate) : IntPtr.Zero);
			native_consumerInfo.fnWriteComp = ((consumerInfo.writeDelegate != null) ? Marshal.GetFunctionPointerForDelegate<SNINativeMethodWrapper.SqlAsyncCallbackDelegate>(consumerInfo.writeDelegate) : IntPtr.Zero);
			native_consumerInfo.ConsumerKey = consumerInfo.key;
		}

		// Token: 0x06001415 RID: 5141 RVA: 0x0005B3A9 File Offset: 0x000595A9
		// Note: this type is marked as 'beforefieldinit'.
		static SNINativeMethodWrapper()
		{
		}

		// Token: 0x04000C34 RID: 3124
		private const string SNI = "sni.dll";

		// Token: 0x04000C35 RID: 3125
		private static int s_sniMaxComposedSpnLength = -1;

		// Token: 0x04000C36 RID: 3126
		private const int SniOpenTimeOut = -1;

		// Token: 0x0200017A RID: 378
		internal enum SniSpecialErrors : uint
		{
			// Token: 0x04000C38 RID: 3128
			LocalDBErrorCode = 50U,
			// Token: 0x04000C39 RID: 3129
			MultiSubnetFailoverWithMoreThan64IPs = 47U,
			// Token: 0x04000C3A RID: 3130
			MultiSubnetFailoverWithInstanceSpecified,
			// Token: 0x04000C3B RID: 3131
			MultiSubnetFailoverWithNonTcpProtocol,
			// Token: 0x04000C3C RID: 3132
			MaxErrorValue = 50157U
		}

		// Token: 0x0200017B RID: 379
		// (Invoke) Token: 0x06001417 RID: 5143
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate void SqlAsyncCallbackDelegate(IntPtr m_ConsKey, IntPtr pPacket, uint dwError);

		// Token: 0x0200017C RID: 380
		internal struct ConsumerInfo
		{
			// Token: 0x04000C3D RID: 3133
			internal int defaultBufferSize;

			// Token: 0x04000C3E RID: 3134
			internal SNINativeMethodWrapper.SqlAsyncCallbackDelegate readDelegate;

			// Token: 0x04000C3F RID: 3135
			internal SNINativeMethodWrapper.SqlAsyncCallbackDelegate writeDelegate;

			// Token: 0x04000C40 RID: 3136
			internal IntPtr key;
		}

		// Token: 0x0200017D RID: 381
		internal enum ConsumerNumber
		{
			// Token: 0x04000C42 RID: 3138
			SNI_Consumer_SNI,
			// Token: 0x04000C43 RID: 3139
			SNI_Consumer_SSB,
			// Token: 0x04000C44 RID: 3140
			SNI_Consumer_PacketIsReleased,
			// Token: 0x04000C45 RID: 3141
			SNI_Consumer_Invalid
		}

		// Token: 0x0200017E RID: 382
		internal enum IOType
		{
			// Token: 0x04000C47 RID: 3143
			READ,
			// Token: 0x04000C48 RID: 3144
			WRITE
		}

		// Token: 0x0200017F RID: 383
		internal enum PrefixEnum
		{
			// Token: 0x04000C4A RID: 3146
			UNKNOWN_PREFIX,
			// Token: 0x04000C4B RID: 3147
			SM_PREFIX,
			// Token: 0x04000C4C RID: 3148
			TCP_PREFIX,
			// Token: 0x04000C4D RID: 3149
			NP_PREFIX,
			// Token: 0x04000C4E RID: 3150
			VIA_PREFIX,
			// Token: 0x04000C4F RID: 3151
			INVALID_PREFIX
		}

		// Token: 0x02000180 RID: 384
		internal enum ProviderEnum
		{
			// Token: 0x04000C51 RID: 3153
			HTTP_PROV,
			// Token: 0x04000C52 RID: 3154
			NP_PROV,
			// Token: 0x04000C53 RID: 3155
			SESSION_PROV,
			// Token: 0x04000C54 RID: 3156
			SIGN_PROV,
			// Token: 0x04000C55 RID: 3157
			SM_PROV,
			// Token: 0x04000C56 RID: 3158
			SMUX_PROV,
			// Token: 0x04000C57 RID: 3159
			SSL_PROV,
			// Token: 0x04000C58 RID: 3160
			TCP_PROV,
			// Token: 0x04000C59 RID: 3161
			VIA_PROV,
			// Token: 0x04000C5A RID: 3162
			MAX_PROVS,
			// Token: 0x04000C5B RID: 3163
			INVALID_PROV
		}

		// Token: 0x02000181 RID: 385
		internal enum QTypes
		{
			// Token: 0x04000C5D RID: 3165
			SNI_QUERY_CONN_INFO,
			// Token: 0x04000C5E RID: 3166
			SNI_QUERY_CONN_BUFSIZE,
			// Token: 0x04000C5F RID: 3167
			SNI_QUERY_CONN_KEY,
			// Token: 0x04000C60 RID: 3168
			SNI_QUERY_CLIENT_ENCRYPT_POSSIBLE,
			// Token: 0x04000C61 RID: 3169
			SNI_QUERY_SERVER_ENCRYPT_POSSIBLE,
			// Token: 0x04000C62 RID: 3170
			SNI_QUERY_CERTIFICATE,
			// Token: 0x04000C63 RID: 3171
			SNI_QUERY_LOCALDB_HMODULE,
			// Token: 0x04000C64 RID: 3172
			SNI_QUERY_CONN_ENCRYPT,
			// Token: 0x04000C65 RID: 3173
			SNI_QUERY_CONN_PROVIDERNUM,
			// Token: 0x04000C66 RID: 3174
			SNI_QUERY_CONN_CONNID,
			// Token: 0x04000C67 RID: 3175
			SNI_QUERY_CONN_PARENTCONNID,
			// Token: 0x04000C68 RID: 3176
			SNI_QUERY_CONN_SECPKG,
			// Token: 0x04000C69 RID: 3177
			SNI_QUERY_CONN_NETPACKETSIZE,
			// Token: 0x04000C6A RID: 3178
			SNI_QUERY_CONN_NODENUM,
			// Token: 0x04000C6B RID: 3179
			SNI_QUERY_CONN_PACKETSRECD,
			// Token: 0x04000C6C RID: 3180
			SNI_QUERY_CONN_PACKETSSENT,
			// Token: 0x04000C6D RID: 3181
			SNI_QUERY_CONN_PEERADDR,
			// Token: 0x04000C6E RID: 3182
			SNI_QUERY_CONN_PEERPORT,
			// Token: 0x04000C6F RID: 3183
			SNI_QUERY_CONN_LASTREADTIME,
			// Token: 0x04000C70 RID: 3184
			SNI_QUERY_CONN_LASTWRITETIME,
			// Token: 0x04000C71 RID: 3185
			SNI_QUERY_CONN_CONSUMER_ID,
			// Token: 0x04000C72 RID: 3186
			SNI_QUERY_CONN_CONNECTTIME,
			// Token: 0x04000C73 RID: 3187
			SNI_QUERY_CONN_HTTPENDPOINT,
			// Token: 0x04000C74 RID: 3188
			SNI_QUERY_CONN_LOCALADDR,
			// Token: 0x04000C75 RID: 3189
			SNI_QUERY_CONN_LOCALPORT,
			// Token: 0x04000C76 RID: 3190
			SNI_QUERY_CONN_SSLHANDSHAKESTATE,
			// Token: 0x04000C77 RID: 3191
			SNI_QUERY_CONN_SOBUFAUTOTUNING,
			// Token: 0x04000C78 RID: 3192
			SNI_QUERY_CONN_SECPKGNAME,
			// Token: 0x04000C79 RID: 3193
			SNI_QUERY_CONN_SECPKGMUTUALAUTH,
			// Token: 0x04000C7A RID: 3194
			SNI_QUERY_CONN_CONSUMERCONNID,
			// Token: 0x04000C7B RID: 3195
			SNI_QUERY_CONN_SNIUCI,
			// Token: 0x04000C7C RID: 3196
			SNI_QUERY_CONN_SUPPORTS_EXTENDED_PROTECTION,
			// Token: 0x04000C7D RID: 3197
			SNI_QUERY_CONN_CHANNEL_PROVIDES_AUTHENTICATION_CONTEXT,
			// Token: 0x04000C7E RID: 3198
			SNI_QUERY_CONN_PEERID,
			// Token: 0x04000C7F RID: 3199
			SNI_QUERY_CONN_SUPPORTS_SYNC_OVER_ASYNC
		}

		// Token: 0x02000182 RID: 386
		internal enum TransparentNetworkResolutionMode : byte
		{
			// Token: 0x04000C81 RID: 3201
			DisabledMode,
			// Token: 0x04000C82 RID: 3202
			SequentialMode,
			// Token: 0x04000C83 RID: 3203
			ParallelMode
		}

		// Token: 0x02000183 RID: 387
		private struct Sni_Consumer_Info
		{
			// Token: 0x04000C84 RID: 3204
			public int DefaultUserDataLength;

			// Token: 0x04000C85 RID: 3205
			public IntPtr ConsumerKey;

			// Token: 0x04000C86 RID: 3206
			public IntPtr fnReadComp;

			// Token: 0x04000C87 RID: 3207
			public IntPtr fnWriteComp;

			// Token: 0x04000C88 RID: 3208
			public IntPtr fnTrace;

			// Token: 0x04000C89 RID: 3209
			public IntPtr fnAcceptComp;

			// Token: 0x04000C8A RID: 3210
			public uint dwNumProts;

			// Token: 0x04000C8B RID: 3211
			public IntPtr rgListenInfo;

			// Token: 0x04000C8C RID: 3212
			public IntPtr NodeAffinity;
		}

		// Token: 0x02000184 RID: 388
		private struct SNI_CLIENT_CONSUMER_INFO
		{
			// Token: 0x04000C8D RID: 3213
			public SNINativeMethodWrapper.Sni_Consumer_Info ConsumerInfo;

			// Token: 0x04000C8E RID: 3214
			[MarshalAs(UnmanagedType.LPWStr)]
			public string wszConnectionString;

			// Token: 0x04000C8F RID: 3215
			public SNINativeMethodWrapper.PrefixEnum networkLibrary;

			// Token: 0x04000C90 RID: 3216
			public unsafe byte* szSPN;

			// Token: 0x04000C91 RID: 3217
			public uint cchSPN;

			// Token: 0x04000C92 RID: 3218
			public unsafe byte* szInstanceName;

			// Token: 0x04000C93 RID: 3219
			public uint cchInstanceName;

			// Token: 0x04000C94 RID: 3220
			[MarshalAs(UnmanagedType.Bool)]
			public bool fOverrideLastConnectCache;

			// Token: 0x04000C95 RID: 3221
			[MarshalAs(UnmanagedType.Bool)]
			public bool fSynchronousConnection;

			// Token: 0x04000C96 RID: 3222
			public int timeout;

			// Token: 0x04000C97 RID: 3223
			[MarshalAs(UnmanagedType.Bool)]
			public bool fParallel;

			// Token: 0x04000C98 RID: 3224
			public SNINativeMethodWrapper.TransparentNetworkResolutionMode transparentNetworkResolution;

			// Token: 0x04000C99 RID: 3225
			public int totalTimeout;

			// Token: 0x04000C9A RID: 3226
			public bool isAzureSqlServerEndpoint;
		}

		// Token: 0x02000185 RID: 389
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct SNI_Error
		{
			// Token: 0x04000C9B RID: 3227
			internal SNINativeMethodWrapper.ProviderEnum provider;

			// Token: 0x04000C9C RID: 3228
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 261)]
			internal string errorMessage;

			// Token: 0x04000C9D RID: 3229
			internal uint nativeError;

			// Token: 0x04000C9E RID: 3230
			internal uint sniError;

			// Token: 0x04000C9F RID: 3231
			[MarshalAs(UnmanagedType.LPWStr)]
			internal string fileName;

			// Token: 0x04000CA0 RID: 3232
			[MarshalAs(UnmanagedType.LPWStr)]
			internal string function;

			// Token: 0x04000CA1 RID: 3233
			internal uint lineNumber;
		}
	}
}
