using System;
using System.IO;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Mono.Net.Security;
using Mono.Security.Interface;
using Mono.Util;

namespace Mono.Unity
{
	// Token: 0x02000079 RID: 121
	internal class UnityTlsProvider : MobileTlsProvider
	{
		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060001BB RID: 443 RVA: 0x00004F9E File Offset: 0x0000319E
		public override string Name
		{
			get
			{
				return "unitytls";
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060001BC RID: 444 RVA: 0x00004FA5 File Offset: 0x000031A5
		public override Guid ID
		{
			get
			{
				return Mono.Net.Security.MonoTlsProviderFactory.UnityTlsId;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060001BD RID: 445 RVA: 0x0000390E File Offset: 0x00001B0E
		public override bool SupportsSslStream
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060001BE RID: 446 RVA: 0x0000390E File Offset: 0x00001B0E
		public override bool SupportsMonoExtensions
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060001BF RID: 447 RVA: 0x0000390E File Offset: 0x00001B0E
		public override bool SupportsConnectionInfo
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x0000390E File Offset: 0x00001B0E
		internal override bool SupportsCleanShutdown
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x00004FAC File Offset: 0x000031AC
		public override SslProtocols SupportedProtocols
		{
			get
			{
				return SslProtocols.Tls | SslProtocols.Tls11 | SslProtocols.Tls12;
			}
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00004FB3 File Offset: 0x000031B3
		internal override MobileAuthenticatedStream CreateSslStream(SslStream sslStream, Stream innerStream, bool leaveInnerStreamOpen, MonoTlsSettings settings)
		{
			return new UnityTlsStream(innerStream, leaveInnerStreamOpen, sslStream, settings, this);
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00004FC0 File Offset: 0x000031C0
		[MonoPInvokeCallback(typeof(UnityTls.unitytls_x509verify_callback))]
		private unsafe static UnityTls.unitytls_x509verify_result x509verify_callback(void* userData, UnityTls.unitytls_x509_ref cert, UnityTls.unitytls_x509verify_result result, UnityTls.unitytls_errorstate* errorState)
		{
			if (userData != null)
			{
				UnityTls.NativeInterface.unitytls_x509list_append((UnityTls.unitytls_x509list*)userData, cert, errorState);
			}
			return result;
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00004FDC File Offset: 0x000031DC
		internal unsafe override bool ValidateCertificate(ChainValidationHelper validator, string targetHost, bool serverMode, X509CertificateCollection certificates, bool wantsChain, ref X509Chain chain, ref SslPolicyErrors errors, ref int status11)
		{
			UnityTls.unitytls_errorstate unitytls_errorstate = UnityTls.NativeInterface.unitytls_errorstate_create();
			X509ChainImplUnityTls x509ChainImplUnityTls = chain.Impl as X509ChainImplUnityTls;
			if (x509ChainImplUnityTls == null)
			{
				if (certificates == null || certificates.Count == 0)
				{
					errors |= SslPolicyErrors.RemoteCertificateNotAvailable;
					return false;
				}
			}
			else if (UnityTls.NativeInterface.unitytls_x509list_get_x509(x509ChainImplUnityTls.NativeCertificateChain, (IntPtr)0, &unitytls_errorstate).handle == UnityTls.NativeInterface.UNITYTLS_INVALID_HANDLE)
			{
				errors |= SslPolicyErrors.RemoteCertificateNotAvailable;
				return false;
			}
			if (!string.IsNullOrEmpty(targetHost))
			{
				int num = targetHost.IndexOf(':');
				if (num > 0)
				{
					targetHost = targetHost.Substring(0, num);
				}
			}
			else if (targetHost == null)
			{
				targetHost = "";
			}
			UnityTls.unitytls_x509verify_result unitytls_x509verify_result = (UnityTls.unitytls_x509verify_result)2147483648U;
			UnityTls.unitytls_x509list* ptr = null;
			UnityTls.unitytls_x509list* ptr2 = UnityTls.NativeInterface.unitytls_x509list_create(&unitytls_errorstate);
			try
			{
				UnityTls.unitytls_x509list_ref chain2;
				if (x509ChainImplUnityTls == null)
				{
					ptr = UnityTls.NativeInterface.unitytls_x509list_create(&unitytls_errorstate);
					CertHelper.AddCertificatesToNativeChain(ptr, certificates, &unitytls_errorstate);
					chain2 = UnityTls.NativeInterface.unitytls_x509list_get_ref(ptr, &unitytls_errorstate);
				}
				else
				{
					chain2 = x509ChainImplUnityTls.NativeCertificateChain;
				}
				byte[] bytes = Encoding.UTF8.GetBytes(targetHost);
				if (validator.Settings.TrustAnchors != null)
				{
					UnityTls.unitytls_x509list* ptr3 = null;
					try
					{
						ptr3 = UnityTls.NativeInterface.unitytls_x509list_create(&unitytls_errorstate);
						CertHelper.AddCertificatesToNativeChain(ptr3, validator.Settings.TrustAnchors, &unitytls_errorstate);
						UnityTls.unitytls_x509list_ref trustCA = UnityTls.NativeInterface.unitytls_x509list_get_ref(ptr3, &unitytls_errorstate);
						try
						{
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
							unitytls_x509verify_result = UnityTls.NativeInterface.unitytls_x509verify_explicit_ca(chain2, trustCA, cn, (IntPtr)bytes.Length, new UnityTls.unitytls_x509verify_callback(UnityTlsProvider.x509verify_callback), (void*)ptr2, &unitytls_errorstate);
						}
						finally
						{
							byte[] array = null;
						}
						goto IL_217;
					}
					finally
					{
						UnityTls.NativeInterface.unitytls_x509list_free(ptr3);
					}
				}
				try
				{
					byte[] array;
					byte* cn2;
					if ((array = bytes) == null || array.Length == 0)
					{
						cn2 = null;
					}
					else
					{
						cn2 = &array[0];
					}
					unitytls_x509verify_result = UnityTls.NativeInterface.unitytls_x509verify_default_ca(chain2, cn2, (IntPtr)bytes.Length, new UnityTls.unitytls_x509verify_callback(UnityTlsProvider.x509verify_callback), (void*)ptr2, &unitytls_errorstate);
				}
				finally
				{
					byte[] array = null;
				}
				IL_217:;
			}
			catch
			{
				UnityTls.NativeInterface.unitytls_x509list_free(ptr2);
				throw;
			}
			finally
			{
				UnityTls.NativeInterface.unitytls_x509list_free(ptr);
			}
			X509Chain x509Chain = chain;
			if (x509Chain != null)
			{
				x509Chain.Dispose();
			}
			X509ChainImplUnityTls x509ChainImplUnityTls2 = new X509ChainImplUnityTls(ptr2, &unitytls_errorstate, true);
			chain = new X509Chain(x509ChainImplUnityTls2);
			errors = UnityTlsConversions.VerifyResultToPolicyErrror(unitytls_x509verify_result);
			x509ChainImplUnityTls2.AddStatus(UnityTlsConversions.VerifyResultToChainStatus(unitytls_x509verify_result));
			return unitytls_x509verify_result == UnityTls.unitytls_x509verify_result.UNITYTLS_X509VERIFY_SUCCESS && unitytls_errorstate.code == UnityTls.unitytls_error_code.UNITYTLS_SUCCESS;
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x000052EC File Offset: 0x000034EC
		public UnityTlsProvider()
		{
		}
	}
}
