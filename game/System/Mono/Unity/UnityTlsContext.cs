using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Mono.Net.Security;
using Mono.Security.Cryptography;
using Mono.Security.Interface;
using Mono.Util;

namespace Mono.Unity
{
	// Token: 0x02000077 RID: 119
	internal class UnityTlsContext : MobileTlsContext
	{
		// Token: 0x06000198 RID: 408 RVA: 0x00003F50 File Offset: 0x00002150
		public unsafe UnityTlsContext(MobileAuthenticatedStream parent, MonoSslAuthenticationOptions options) : base(parent, options)
		{
			this.handle = GCHandle.Alloc(this);
			UnityTls.unitytls_errorstate errorState = UnityTls.NativeInterface.unitytls_errorstate_create();
			UnityTls.unitytls_tlsctx_protocolrange supportedProtocols = new UnityTls.unitytls_tlsctx_protocolrange
			{
				min = UnityTlsConversions.GetMinProtocol(options.EnabledSslProtocols),
				max = UnityTlsConversions.GetMaxProtocol(options.EnabledSslProtocols)
			};
			this.readCallback = new UnityTls.unitytls_tlsctx_read_callback(UnityTlsContext.ReadCallback);
			this.writeCallback = new UnityTls.unitytls_tlsctx_write_callback(UnityTlsContext.WriteCallback);
			UnityTls.unitytls_tlsctx_callbacks callbacks = new UnityTls.unitytls_tlsctx_callbacks
			{
				write = this.writeCallback,
				read = this.readCallback,
				data = (void*)((IntPtr)this.handle)
			};
			if (options.ServerMode)
			{
				UnityTls.unitytls_x509list* list;
				UnityTls.unitytls_key* key;
				UnityTlsContext.ExtractNativeKeyAndChainFromManagedCertificate(options.ServerCertificate, &errorState, out list, out key);
				try
				{
					UnityTls.unitytls_x509list_ref unitytls_x509list_ref = UnityTls.NativeInterface.unitytls_x509list_get_ref(list, &errorState);
					UnityTls.unitytls_key_ref unitytls_key_ref = UnityTls.NativeInterface.unitytls_key_get_ref(key, &errorState);
					Mono.Unity.Debug.CheckAndThrow(errorState, "Failed to parse server key/certificate", AlertDescription.InternalError);
					this.tlsContext = UnityTls.NativeInterface.unitytls_tlsctx_create_server(supportedProtocols, callbacks, unitytls_x509list_ref.handle, unitytls_key_ref.handle, &errorState);
					if (base.AskForClientCertificate)
					{
						UnityTls.unitytls_x509list* list2 = null;
						try
						{
							list2 = UnityTls.NativeInterface.unitytls_x509list_create(&errorState);
							UnityTls.unitytls_x509list_ref clientAuthCAList = UnityTls.NativeInterface.unitytls_x509list_get_ref(list2, &errorState);
							UnityTls.NativeInterface.unitytls_tlsctx_server_require_client_authentication(this.tlsContext, clientAuthCAList, &errorState);
						}
						finally
						{
							UnityTls.NativeInterface.unitytls_x509list_free(list2);
						}
					}
					goto IL_26F;
				}
				finally
				{
					UnityTls.NativeInterface.unitytls_x509list_free(list);
					UnityTls.NativeInterface.unitytls_key_free(key);
				}
			}
			byte[] bytes = Encoding.UTF8.GetBytes(options.TargetHost);
			byte[] array;
			byte* cn;
			if ((array = bytes) == null || array.Length == 0)
			{
				cn = null;
			}
			else
			{
				cn = &array[0];
			}
			this.tlsContext = UnityTls.NativeInterface.unitytls_tlsctx_create_client(supportedProtocols, callbacks, cn, (IntPtr)bytes.Length, &errorState);
			array = null;
			this.certificateCallback = new UnityTls.unitytls_tlsctx_certificate_callback(UnityTlsContext.CertificateCallback);
			UnityTls.NativeInterface.unitytls_tlsctx_set_certificate_callback(this.tlsContext, this.certificateCallback, (void*)((IntPtr)this.handle), &errorState);
			IL_26F:
			this.verifyCallback = new UnityTls.unitytls_tlsctx_x509verify_callback(UnityTlsContext.VerifyCallback);
			UnityTls.NativeInterface.unitytls_tlsctx_set_x509verify_callback(this.tlsContext, this.verifyCallback, (void*)((IntPtr)this.handle), &errorState);
			Mono.Unity.Debug.CheckAndThrow(errorState, "Failed to create UnityTls context", AlertDescription.InternalError);
			this.hasContext = true;
		}

		// Token: 0x06000199 RID: 409 RVA: 0x0000423C File Offset: 0x0000243C
		private unsafe static void ExtractNativeKeyAndChainFromManagedCertificate(X509Certificate cert, UnityTls.unitytls_errorstate* errorState, out UnityTls.unitytls_x509list* nativeCertChain, out UnityTls.unitytls_key* nativeKey)
		{
			if (cert == null)
			{
				throw new ArgumentNullException("cert");
			}
			X509Certificate2 x509Certificate = cert as X509Certificate2;
			if (x509Certificate == null || x509Certificate.PrivateKey == null)
			{
				throw new ArgumentException("Certificate does not have a private key", "cert");
			}
			nativeCertChain = (IntPtr)((UIntPtr)0);
			nativeKey = (IntPtr)((UIntPtr)0);
			try
			{
				nativeCertChain = UnityTls.NativeInterface.unitytls_x509list_create(errorState);
				CertHelper.AddCertificateToNativeChain(nativeCertChain, cert, errorState);
				byte[] array = PKCS8.PrivateKeyInfo.Encode(x509Certificate.PrivateKey);
				try
				{
					byte[] array2;
					byte* buffer;
					if ((array2 = array) == null || array2.Length == 0)
					{
						buffer = null;
					}
					else
					{
						buffer = &array2[0];
					}
					nativeKey = UnityTls.NativeInterface.unitytls_key_parse_der(buffer, (IntPtr)array.Length, null, (IntPtr)0, errorState);
				}
				finally
				{
					byte[] array2 = null;
				}
			}
			catch
			{
				UnityTls.NativeInterface.unitytls_x509list_free(nativeCertChain);
				UnityTls.NativeInterface.unitytls_key_free(nativeKey);
				throw;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600019A RID: 410 RVA: 0x0000432C File Offset: 0x0000252C
		public override bool HasContext
		{
			get
			{
				return this.hasContext;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600019B RID: 411 RVA: 0x00004334 File Offset: 0x00002534
		public override bool IsAuthenticated
		{
			get
			{
				return this.isAuthenticated;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600019C RID: 412 RVA: 0x0000433C File Offset: 0x0000253C
		public override MonoTlsConnectionInfo ConnectionInfo
		{
			get
			{
				return this.connectioninfo;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600019D RID: 413 RVA: 0x00004344 File Offset: 0x00002544
		internal override bool IsRemoteCertificateAvailable
		{
			get
			{
				return this.remoteCertificate != null;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600019E RID: 414 RVA: 0x0000434F File Offset: 0x0000254F
		internal override X509Certificate LocalClientCertificate
		{
			get
			{
				return this.localClientCertificate;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600019F RID: 415 RVA: 0x00004357 File Offset: 0x00002557
		public override X509Certificate2 RemoteCertificate
		{
			get
			{
				return this.remoteCertificate;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x0000435F File Offset: 0x0000255F
		public override TlsProtocols NegotiatedProtocol
		{
			get
			{
				return this.ConnectionInfo.ProtocolVersion;
			}
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00003917 File Offset: 0x00001B17
		public override void Flush()
		{
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x0000436C File Offset: 0x0000256C
		[return: TupleElementNames(new string[]
		{
			"ret",
			"wantMore"
		})]
		public unsafe override ValueTuple<int, bool> Read(byte[] buffer, int offset, int count)
		{
			this.lastException = null;
			UnityTls.unitytls_errorstate unitytls_errorstate = UnityTls.NativeInterface.unitytls_errorstate_create();
			int num;
			fixed (byte[] array = buffer)
			{
				byte* ptr;
				if (buffer == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				num = (int)UnityTls.NativeInterface.unitytls_tlsctx_read(this.tlsContext, ptr + offset, (IntPtr)count, &unitytls_errorstate);
			}
			if (this.lastException != null)
			{
				throw this.lastException;
			}
			UnityTls.unitytls_error_code code = unitytls_errorstate.code;
			if (code == UnityTls.unitytls_error_code.UNITYTLS_SUCCESS)
			{
				return new ValueTuple<int, bool>(num, num < count);
			}
			if (code == UnityTls.unitytls_error_code.UNITYTLS_STREAM_CLOSED)
			{
				return new ValueTuple<int, bool>(0, false);
			}
			if (code != UnityTls.unitytls_error_code.UNITYTLS_USER_WOULD_BLOCK)
			{
				if (!this.closedGraceful)
				{
					Mono.Unity.Debug.CheckAndThrow(unitytls_errorstate, "Failed to read data to TLS context", AlertDescription.InternalError);
				}
				return new ValueTuple<int, bool>(0, false);
			}
			return new ValueTuple<int, bool>(num, true);
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00004438 File Offset: 0x00002638
		[return: TupleElementNames(new string[]
		{
			"ret",
			"wantMore"
		})]
		public unsafe override ValueTuple<int, bool> Write(byte[] buffer, int offset, int count)
		{
			this.lastException = null;
			UnityTls.unitytls_errorstate unitytls_errorstate = UnityTls.NativeInterface.unitytls_errorstate_create();
			int num;
			fixed (byte[] array = buffer)
			{
				byte* ptr;
				if (buffer == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				num = (int)UnityTls.NativeInterface.unitytls_tlsctx_write(this.tlsContext, ptr + offset, (IntPtr)count, &unitytls_errorstate);
			}
			if (this.lastException != null)
			{
				throw this.lastException;
			}
			UnityTls.unitytls_error_code code = unitytls_errorstate.code;
			if (code == UnityTls.unitytls_error_code.UNITYTLS_SUCCESS)
			{
				return new ValueTuple<int, bool>(num, num < count);
			}
			if (code == UnityTls.unitytls_error_code.UNITYTLS_STREAM_CLOSED)
			{
				return new ValueTuple<int, bool>(0, false);
			}
			if (code != UnityTls.unitytls_error_code.UNITYTLS_USER_WOULD_BLOCK)
			{
				Mono.Unity.Debug.CheckAndThrow(unitytls_errorstate, "Failed to write data to TLS context", AlertDescription.InternalError);
				return new ValueTuple<int, bool>(0, false);
			}
			return new ValueTuple<int, bool>(num, true);
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x00003062 File Offset: 0x00001262
		public override bool CanRenegotiate
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x000044FA File Offset: 0x000026FA
		public override void Renegotiate()
		{
			throw new NotSupportedException();
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00003062 File Offset: 0x00001262
		public override bool PendingRenegotiation()
		{
			return false;
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00004504 File Offset: 0x00002704
		public unsafe override void Shutdown()
		{
			if (base.Settings != null && base.Settings.SendCloseNotify)
			{
				UnityTls.unitytls_errorstate unitytls_errorstate = UnityTls.NativeInterface.unitytls_errorstate_create();
				UnityTls.NativeInterface.unitytls_tlsctx_notify_close(this.tlsContext, &unitytls_errorstate);
			}
			UnityTls.NativeInterface.unitytls_x509list_free(this.requestedClientCertChain);
			UnityTls.NativeInterface.unitytls_key_free(this.requestedClientKey);
			UnityTls.NativeInterface.unitytls_tlsctx_free(this.tlsContext);
			this.tlsContext = null;
			this.hasContext = false;
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x0000459C File Offset: 0x0000279C
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					this.Shutdown();
					this.localClientCertificate = null;
					this.remoteCertificate = null;
					if (this.localClientCertificate != null)
					{
						this.localClientCertificate.Dispose();
						this.localClientCertificate = null;
					}
					if (this.remoteCertificate != null)
					{
						this.remoteCertificate.Dispose();
						this.remoteCertificate = null;
					}
					this.connectioninfo = null;
					this.isAuthenticated = false;
					this.hasContext = false;
				}
				this.handle.Free();
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00004630 File Offset: 0x00002830
		public unsafe override void StartHandshake()
		{
			if (base.Settings != null && base.Settings.EnabledCiphers != null)
			{
				UnityTls.unitytls_ciphersuite[] array = new UnityTls.unitytls_ciphersuite[base.Settings.EnabledCiphers.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = (UnityTls.unitytls_ciphersuite)base.Settings.EnabledCiphers[i];
				}
				UnityTls.unitytls_errorstate errorState = UnityTls.NativeInterface.unitytls_errorstate_create();
				UnityTls.unitytls_ciphersuite[] array2;
				UnityTls.unitytls_ciphersuite* supportedCiphersuites;
				if ((array2 = array) == null || array2.Length == 0)
				{
					supportedCiphersuites = null;
				}
				else
				{
					supportedCiphersuites = &array2[0];
				}
				UnityTls.NativeInterface.unitytls_tlsctx_set_supported_ciphersuites(this.tlsContext, supportedCiphersuites, (IntPtr)array.Length, &errorState);
				array2 = null;
				Mono.Unity.Debug.CheckAndThrow(errorState, "Failed to set list of supported ciphers", AlertDescription.HandshakeFailure);
			}
		}

		// Token: 0x060001AA RID: 426 RVA: 0x000046E8 File Offset: 0x000028E8
		public unsafe override bool ProcessHandshake()
		{
			this.lastException = null;
			UnityTls.unitytls_errorstate unitytls_errorstate = UnityTls.NativeInterface.unitytls_errorstate_create();
			UnityTls.unitytls_x509verify_result unitytls_x509verify_result = UnityTls.NativeInterface.unitytls_tlsctx_process_handshake(this.tlsContext, &unitytls_errorstate);
			if (unitytls_errorstate.code == UnityTls.unitytls_error_code.UNITYTLS_USER_WOULD_BLOCK)
			{
				return false;
			}
			if (this.lastException != null)
			{
				throw this.lastException;
			}
			if (base.IsServer && unitytls_x509verify_result == (UnityTls.unitytls_x509verify_result)2147483648U)
			{
				Mono.Unity.Debug.CheckAndThrow(unitytls_errorstate, "Handshake failed", AlertDescription.HandshakeFailure);
				if (!base.ValidateCertificate(null, null))
				{
					throw new TlsException(AlertDescription.HandshakeFailure, "Verification failure during handshake");
				}
			}
			else
			{
				Mono.Unity.Debug.CheckAndThrow(unitytls_errorstate, unitytls_x509verify_result, "Handshake failed", AlertDescription.HandshakeFailure);
			}
			return true;
		}

		// Token: 0x060001AB RID: 427 RVA: 0x00004788 File Offset: 0x00002988
		public unsafe override void FinishHandshake()
		{
			UnityTls.unitytls_errorstate unitytls_errorstate = UnityTls.NativeInterface.unitytls_errorstate_create();
			UnityTls.unitytls_ciphersuite unitytls_ciphersuite = UnityTls.NativeInterface.unitytls_tlsctx_get_ciphersuite(this.tlsContext, &unitytls_errorstate);
			UnityTls.unitytls_protocol protocol = UnityTls.NativeInterface.unitytls_tlsctx_get_protocol(this.tlsContext, &unitytls_errorstate);
			this.connectioninfo = new MonoTlsConnectionInfo
			{
				CipherSuiteCode = (CipherSuiteCode)unitytls_ciphersuite,
				ProtocolVersion = UnityTlsConversions.ConvertProtocolVersion(protocol),
				PeerDomainName = base.ServerName
			};
			this.isAuthenticated = true;
		}

		// Token: 0x060001AC RID: 428 RVA: 0x0000480C File Offset: 0x00002A0C
		[MonoPInvokeCallback(typeof(UnityTls.unitytls_tlsctx_write_callback))]
		private unsafe static IntPtr WriteCallback(void* userData, byte* data, IntPtr bufferLen, UnityTls.unitytls_errorstate* errorState)
		{
			return ((UnityTlsContext)((GCHandle)((IntPtr)userData)).Target).WriteCallback(data, bufferLen, errorState);
		}

		// Token: 0x060001AD RID: 429 RVA: 0x0000483C File Offset: 0x00002A3C
		private unsafe IntPtr WriteCallback(byte* data, IntPtr bufferLen, UnityTls.unitytls_errorstate* errorState)
		{
			IntPtr result;
			try
			{
				if (this.writeBuffer == null || this.writeBuffer.Length < (int)bufferLen)
				{
					this.writeBuffer = new byte[(int)bufferLen];
				}
				Marshal.Copy((IntPtr)((void*)data), this.writeBuffer, 0, (int)bufferLen);
				if (!base.Parent.InternalWrite(this.writeBuffer, 0, (int)bufferLen))
				{
					UnityTls.NativeInterface.unitytls_errorstate_raise_error(errorState, UnityTls.unitytls_error_code.UNITYTLS_USER_WRITE_FAILED);
					result = (IntPtr)0;
				}
				else
				{
					result = bufferLen;
				}
			}
			catch (Exception ex)
			{
				UnityTls.NativeInterface.unitytls_errorstate_raise_error(errorState, UnityTls.unitytls_error_code.UNITYTLS_USER_UNKNOWN_ERROR);
				if (this.lastException == null)
				{
					this.lastException = ex;
				}
				result = (IntPtr)0;
			}
			return result;
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00004908 File Offset: 0x00002B08
		[MonoPInvokeCallback(typeof(UnityTls.unitytls_tlsctx_read_callback))]
		private unsafe static IntPtr ReadCallback(void* userData, byte* buffer, IntPtr bufferLen, UnityTls.unitytls_errorstate* errorState)
		{
			return ((UnityTlsContext)((GCHandle)((IntPtr)userData)).Target).ReadCallback(buffer, bufferLen, errorState);
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00004938 File Offset: 0x00002B38
		private unsafe IntPtr ReadCallback(byte* buffer, IntPtr bufferLen, UnityTls.unitytls_errorstate* errorState)
		{
			IntPtr result;
			try
			{
				if (this.readBuffer == null || this.readBuffer.Length < (int)bufferLen)
				{
					this.readBuffer = new byte[(int)bufferLen];
				}
				bool flag;
				int num = base.Parent.InternalRead(this.readBuffer, 0, (int)bufferLen, out flag);
				if (num < 0)
				{
					UnityTls.NativeInterface.unitytls_errorstate_raise_error(errorState, UnityTls.unitytls_error_code.UNITYTLS_USER_READ_FAILED);
				}
				else if (num > 0)
				{
					Marshal.Copy(this.readBuffer, 0, (IntPtr)((void*)buffer), (int)bufferLen);
				}
				else if (flag)
				{
					UnityTls.NativeInterface.unitytls_errorstate_raise_error(errorState, UnityTls.unitytls_error_code.UNITYTLS_USER_WOULD_BLOCK);
				}
				else
				{
					this.closedGraceful = true;
					UnityTls.NativeInterface.unitytls_errorstate_raise_error(errorState, UnityTls.unitytls_error_code.UNITYTLS_USER_READ_FAILED);
				}
				result = (IntPtr)num;
			}
			catch (Exception ex)
			{
				UnityTls.NativeInterface.unitytls_errorstate_raise_error(errorState, UnityTls.unitytls_error_code.UNITYTLS_USER_UNKNOWN_ERROR);
				if (this.lastException == null)
				{
					this.lastException = ex;
				}
				result = (IntPtr)0;
			}
			return result;
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x00004A40 File Offset: 0x00002C40
		[MonoPInvokeCallback(typeof(UnityTls.unitytls_tlsctx_x509verify_callback))]
		private unsafe static UnityTls.unitytls_x509verify_result VerifyCallback(void* userData, UnityTls.unitytls_x509list_ref chain, UnityTls.unitytls_errorstate* errorState)
		{
			return ((UnityTlsContext)((GCHandle)((IntPtr)userData)).Target).VerifyCallback(chain, errorState);
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00004A6C File Offset: 0x00002C6C
		private unsafe UnityTls.unitytls_x509verify_result VerifyCallback(UnityTls.unitytls_x509list_ref chain, UnityTls.unitytls_errorstate* errorState)
		{
			UnityTls.unitytls_x509verify_result result;
			try
			{
				using (X509ChainImplUnityTls x509ChainImplUnityTls = new X509ChainImplUnityTls(chain, false))
				{
					using (X509Chain x509Chain = new X509Chain(x509ChainImplUnityTls))
					{
						this.remoteCertificate = x509Chain.ChainElements[0].Certificate;
						if (base.ValidateCertificate(this.remoteCertificate, x509Chain))
						{
							result = UnityTls.unitytls_x509verify_result.UNITYTLS_X509VERIFY_SUCCESS;
						}
						else
						{
							result = UnityTls.unitytls_x509verify_result.UNITYTLS_X509VERIFY_FLAG_NOT_TRUSTED;
						}
					}
				}
			}
			catch (Exception ex)
			{
				if (this.lastException == null)
				{
					this.lastException = ex;
				}
				result = (UnityTls.unitytls_x509verify_result)4294967295U;
			}
			return result;
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00004B08 File Offset: 0x00002D08
		[MonoPInvokeCallback(typeof(UnityTls.unitytls_tlsctx_certificate_callback))]
		private unsafe static void CertificateCallback(void* userData, UnityTls.unitytls_tlsctx* ctx, byte* cn, IntPtr cnLen, UnityTls.unitytls_x509name* caList, IntPtr caListLen, UnityTls.unitytls_x509list_ref* chain, UnityTls.unitytls_key_ref* key, UnityTls.unitytls_errorstate* errorState)
		{
			((UnityTlsContext)((GCHandle)((IntPtr)userData)).Target).CertificateCallback(ctx, cn, cnLen, caList, caListLen, chain, key, errorState);
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00004B40 File Offset: 0x00002D40
		private unsafe void CertificateCallback(UnityTls.unitytls_tlsctx* ctx, byte* cn, IntPtr cnLen, UnityTls.unitytls_x509name* caList, IntPtr caListLen, UnityTls.unitytls_x509list_ref* chain, UnityTls.unitytls_key_ref* key, UnityTls.unitytls_errorstate* errorState)
		{
			try
			{
				if (this.remoteCertificate == null)
				{
					throw new TlsException(AlertDescription.InternalError, "Cannot request client certificate before receiving one from the server.");
				}
				this.localClientCertificate = base.SelectClientCertificate(null);
				if (this.localClientCertificate == null)
				{
					*chain = new UnityTls.unitytls_x509list_ref
					{
						handle = UnityTls.NativeInterface.UNITYTLS_INVALID_HANDLE
					};
					*key = new UnityTls.unitytls_key_ref
					{
						handle = UnityTls.NativeInterface.UNITYTLS_INVALID_HANDLE
					};
				}
				else
				{
					UnityTls.NativeInterface.unitytls_x509list_free(this.requestedClientCertChain);
					UnityTls.NativeInterface.unitytls_key_free(this.requestedClientKey);
					UnityTlsContext.ExtractNativeKeyAndChainFromManagedCertificate(this.localClientCertificate, errorState, out this.requestedClientCertChain, out this.requestedClientKey);
					*chain = UnityTls.NativeInterface.unitytls_x509list_get_ref(this.requestedClientCertChain, errorState);
					*key = UnityTls.NativeInterface.unitytls_key_get_ref(this.requestedClientKey, errorState);
				}
				Mono.Unity.Debug.CheckAndThrow(*errorState, "Failed to retrieve certificates on request.", AlertDescription.HandshakeFailure);
			}
			catch (Exception ex)
			{
				UnityTls.NativeInterface.unitytls_errorstate_raise_error(errorState, UnityTls.unitytls_error_code.UNITYTLS_USER_UNKNOWN_ERROR);
				if (this.lastException == null)
				{
					this.lastException = ex;
				}
			}
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00004C94 File Offset: 0x00002E94
		[MonoPInvokeCallback(typeof(UnityTls.unitytls_tlsctx_trace_callback))]
		private unsafe static void TraceCallback(void* userData, UnityTls.unitytls_tlsctx* ctx, byte* traceMessage, IntPtr traceMessageLen)
		{
			Console.Write(Encoding.UTF8.GetString(traceMessage, (int)traceMessageLen));
		}

		// Token: 0x040001B3 RID: 435
		private const bool ActivateTracing = false;

		// Token: 0x040001B4 RID: 436
		private unsafe UnityTls.unitytls_tlsctx* tlsContext = null;

		// Token: 0x040001B5 RID: 437
		private unsafe UnityTls.unitytls_x509list* requestedClientCertChain = null;

		// Token: 0x040001B6 RID: 438
		private unsafe UnityTls.unitytls_key* requestedClientKey = null;

		// Token: 0x040001B7 RID: 439
		private UnityTls.unitytls_tlsctx_read_callback readCallback;

		// Token: 0x040001B8 RID: 440
		private UnityTls.unitytls_tlsctx_write_callback writeCallback;

		// Token: 0x040001B9 RID: 441
		private UnityTls.unitytls_tlsctx_trace_callback traceCallback;

		// Token: 0x040001BA RID: 442
		private UnityTls.unitytls_tlsctx_certificate_callback certificateCallback;

		// Token: 0x040001BB RID: 443
		private UnityTls.unitytls_tlsctx_x509verify_callback verifyCallback;

		// Token: 0x040001BC RID: 444
		private X509Certificate localClientCertificate;

		// Token: 0x040001BD RID: 445
		private X509Certificate2 remoteCertificate;

		// Token: 0x040001BE RID: 446
		private MonoTlsConnectionInfo connectioninfo;

		// Token: 0x040001BF RID: 447
		private bool isAuthenticated;

		// Token: 0x040001C0 RID: 448
		private bool hasContext;

		// Token: 0x040001C1 RID: 449
		private bool closedGraceful;

		// Token: 0x040001C2 RID: 450
		private byte[] writeBuffer;

		// Token: 0x040001C3 RID: 451
		private byte[] readBuffer;

		// Token: 0x040001C4 RID: 452
		private GCHandle handle;

		// Token: 0x040001C5 RID: 453
		private Exception lastException;
	}
}
