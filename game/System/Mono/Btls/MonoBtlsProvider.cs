using System;
using System.IO;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Win32.SafeHandles;
using Mono.Net.Security;
using Mono.Security.Interface;

namespace Mono.Btls
{
	// Token: 0x020000E1 RID: 225
	internal class MonoBtlsProvider : MobileTlsProvider
	{
		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060004AC RID: 1196 RVA: 0x0000E0EF File Offset: 0x0000C2EF
		public override Guid ID
		{
			get
			{
				return Mono.Net.Security.MonoTlsProviderFactory.BtlsId;
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060004AD RID: 1197 RVA: 0x0000E0F6 File Offset: 0x0000C2F6
		public override string Name
		{
			get
			{
				return "btls";
			}
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x0000E0FD File Offset: 0x0000C2FD
		internal MonoBtlsProvider()
		{
			if (!Mono.Net.Security.MonoTlsProviderFactory.IsBtlsSupported())
			{
				throw new NotSupportedException("BTLS is not supported in this runtime.");
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060004AF RID: 1199 RVA: 0x0000390E File Offset: 0x00001B0E
		public override bool SupportsSslStream
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060004B0 RID: 1200 RVA: 0x0000390E File Offset: 0x00001B0E
		public override bool SupportsMonoExtensions
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060004B1 RID: 1201 RVA: 0x0000390E File Offset: 0x00001B0E
		public override bool SupportsConnectionInfo
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060004B2 RID: 1202 RVA: 0x0000390E File Offset: 0x00001B0E
		internal override bool SupportsCleanShutdown
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060004B3 RID: 1203 RVA: 0x00004FAC File Offset: 0x000031AC
		public override SslProtocols SupportedProtocols
		{
			get
			{
				return SslProtocols.Tls | SslProtocols.Tls11 | SslProtocols.Tls12;
			}
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x0000E117 File Offset: 0x0000C317
		internal override MobileAuthenticatedStream CreateSslStream(SslStream sslStream, Stream innerStream, bool leaveInnerStreamOpen, MonoTlsSettings settings)
		{
			return new MonoBtlsStream(innerStream, leaveInnerStreamOpen, sslStream, settings, this);
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060004B5 RID: 1205 RVA: 0x0000390E File Offset: 0x00001B0E
		internal override bool HasNativeCertificates
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x0000E124 File Offset: 0x0000C324
		internal X509Certificate2Impl GetNativeCertificate(byte[] data, string password, X509KeyStorageFlags flags)
		{
			X509Certificate2Impl nativeCertificate;
			using (SafePasswordHandle safePasswordHandle = new SafePasswordHandle(password))
			{
				nativeCertificate = this.GetNativeCertificate(data, safePasswordHandle, flags);
			}
			return nativeCertificate;
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x0000E160 File Offset: 0x0000C360
		internal X509Certificate2Impl GetNativeCertificate(X509Certificate certificate)
		{
			X509CertificateImplBtls x509CertificateImplBtls = certificate.Impl as X509CertificateImplBtls;
			if (x509CertificateImplBtls != null)
			{
				return (X509Certificate2Impl)x509CertificateImplBtls.Clone();
			}
			return new X509CertificateImplBtls(certificate.GetRawCertData(), MonoBtlsX509Format.DER);
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x0000E194 File Offset: 0x0000C394
		internal X509Certificate2Impl GetNativeCertificate(byte[] data, SafePasswordHandle password, X509KeyStorageFlags flags)
		{
			return new X509CertificateImplBtls(data, password, flags);
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x0000E1A0 File Offset: 0x0000C3A0
		internal static MonoBtlsX509VerifyParam GetVerifyParam(MonoTlsSettings settings, string targetHost, bool serverMode)
		{
			MonoBtlsX509VerifyParam monoBtlsX509VerifyParam;
			if (serverMode)
			{
				monoBtlsX509VerifyParam = MonoBtlsX509VerifyParam.GetSslClient();
			}
			else
			{
				monoBtlsX509VerifyParam = MonoBtlsX509VerifyParam.GetSslServer();
			}
			if (targetHost == null && (settings == null || settings.CertificateValidationTime == null))
			{
				return monoBtlsX509VerifyParam;
			}
			MonoBtlsX509VerifyParam result;
			try
			{
				MonoBtlsX509VerifyParam monoBtlsX509VerifyParam2 = monoBtlsX509VerifyParam.Copy();
				if (targetHost != null)
				{
					monoBtlsX509VerifyParam2.SetHost(targetHost);
				}
				if (settings != null && settings.CertificateValidationTime != null)
				{
					monoBtlsX509VerifyParam2.SetTime(settings.CertificateValidationTime.Value);
				}
				result = monoBtlsX509VerifyParam2;
			}
			finally
			{
				monoBtlsX509VerifyParam.Dispose();
			}
			return result;
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x0000E234 File Offset: 0x0000C434
		internal override bool ValidateCertificate(ChainValidationHelper validator, string targetHost, bool serverMode, X509CertificateCollection certificates, bool wantsChain, ref X509Chain chain, ref SslPolicyErrors errors, ref int status11)
		{
			if (Environment.OSVersion.Platform == PlatformID.Unix && Environment.GetEnvironmentVariable("UNITY_THISISABUILDMACHINE") != null)
			{
				return true;
			}
			if (chain != null)
			{
				X509ChainImplBtls x509ChainImplBtls = (X509ChainImplBtls)chain.Impl;
				bool flag = x509ChainImplBtls.StoreCtx.VerifyResult == 1;
				this.CheckValidationResult(validator, targetHost, serverMode, certificates, wantsChain, chain, x509ChainImplBtls.StoreCtx, flag, ref errors, ref status11);
				return flag;
			}
			bool result;
			using (MonoBtlsX509Store monoBtlsX509Store = new MonoBtlsX509Store())
			{
				using (MonoBtlsX509Chain nativeChain = MonoBtlsProvider.GetNativeChain(certificates))
				{
					using (MonoBtlsX509VerifyParam verifyParam = MonoBtlsProvider.GetVerifyParam(validator.Settings, targetHost, serverMode))
					{
						using (MonoBtlsX509StoreCtx monoBtlsX509StoreCtx = new MonoBtlsX509StoreCtx())
						{
							MonoBtlsProvider.SetupCertificateStore(monoBtlsX509Store, validator.Settings, serverMode);
							monoBtlsX509StoreCtx.Initialize(monoBtlsX509Store, nativeChain);
							monoBtlsX509StoreCtx.SetVerifyParam(verifyParam);
							bool flag2 = monoBtlsX509StoreCtx.Verify() == 1;
							if (wantsChain && chain == null)
							{
								chain = MonoBtlsProvider.GetManagedChain(nativeChain);
							}
							this.CheckValidationResult(validator, targetHost, serverMode, certificates, wantsChain, null, monoBtlsX509StoreCtx, flag2, ref errors, ref status11);
							result = flag2;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x0000E37C File Offset: 0x0000C57C
		internal static bool ValidateCertificate(MonoBtlsX509Chain chain, MonoBtlsX509VerifyParam param)
		{
			bool result;
			using (MonoBtlsX509Store monoBtlsX509Store = new MonoBtlsX509Store())
			{
				using (MonoBtlsX509StoreCtx monoBtlsX509StoreCtx = new MonoBtlsX509StoreCtx())
				{
					MonoBtlsProvider.SetupCertificateStore(monoBtlsX509Store, MonoTlsSettings.DefaultSettings, false);
					monoBtlsX509StoreCtx.Initialize(monoBtlsX509Store, chain);
					if (param != null)
					{
						monoBtlsX509StoreCtx.SetVerifyParam(param);
					}
					result = (monoBtlsX509StoreCtx.Verify() == 1);
				}
			}
			return result;
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x0000E3F0 File Offset: 0x0000C5F0
		private void CheckValidationResult(ChainValidationHelper validator, string targetHost, bool serverMode, X509CertificateCollection certificates, bool wantsChain, X509Chain chain, MonoBtlsX509StoreCtx storeCtx, bool success, ref SslPolicyErrors errors, ref int status11)
		{
			status11 = 0;
			if (success)
			{
				return;
			}
			errors = SslPolicyErrors.RemoteCertificateChainErrors;
			if (!wantsChain || storeCtx == null || chain == null)
			{
				status11 = -2146762485;
				return;
			}
			MonoBtlsX509Error error = storeCtx.GetError();
			if (error != MonoBtlsX509Error.OK)
			{
				if (error != MonoBtlsX509Error.CRL_NOT_YET_VALID)
				{
					if (error == MonoBtlsX509Error.HOSTNAME_MISMATCH)
					{
						errors = SslPolicyErrors.RemoteCertificateNameMismatch;
						chain.Impl.AddStatus(X509ChainStatusFlags.UntrustedRoot);
						status11 = -2146762485;
						return;
					}
					chain.Impl.AddStatus(MonoBtlsProvider.MapVerifyErrorToChainStatus(error));
					status11 = -2146762485;
				}
				return;
			}
			errors = SslPolicyErrors.None;
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x0000E470 File Offset: 0x0000C670
		internal static X509ChainStatusFlags MapVerifyErrorToChainStatus(MonoBtlsX509Error code)
		{
			switch (code)
			{
			case MonoBtlsX509Error.OK:
				return X509ChainStatusFlags.NoError;
			case MonoBtlsX509Error.UNABLE_TO_GET_ISSUER_CERT:
			case MonoBtlsX509Error.UNABLE_TO_GET_ISSUER_CERT_LOCALLY:
			case MonoBtlsX509Error.UNABLE_TO_VERIFY_LEAF_SIGNATURE:
				return X509ChainStatusFlags.PartialChain;
			case MonoBtlsX509Error.UNABLE_TO_GET_CRL:
			case MonoBtlsX509Error.UNABLE_TO_DECRYPT_CRL_SIGNATURE:
			case MonoBtlsX509Error.CRL_SIGNATURE_FAILURE:
			case MonoBtlsX509Error.CRL_NOT_YET_VALID:
			case MonoBtlsX509Error.ERROR_IN_CRL_LAST_UPDATE_FIELD:
			case MonoBtlsX509Error.ERROR_IN_CRL_NEXT_UPDATE_FIELD:
			case MonoBtlsX509Error.UNABLE_TO_GET_CRL_ISSUER:
			case MonoBtlsX509Error.KEYUSAGE_NO_CRL_SIGN:
			case MonoBtlsX509Error.UNHANDLED_CRITICAL_CRL_EXTENSION:
				return X509ChainStatusFlags.RevocationStatusUnknown;
			case MonoBtlsX509Error.UNABLE_TO_DECODE_ISSUER_PUBLIC_KEY:
			case MonoBtlsX509Error.CERT_SIGNATURE_FAILURE:
				return X509ChainStatusFlags.NotSignatureValid;
			case MonoBtlsX509Error.CERT_NOT_YET_VALID:
			case MonoBtlsX509Error.CERT_HAS_EXPIRED:
			case MonoBtlsX509Error.ERROR_IN_CERT_NOT_BEFORE_FIELD:
			case MonoBtlsX509Error.ERROR_IN_CERT_NOT_AFTER_FIELD:
				return X509ChainStatusFlags.NotTimeValid;
			case MonoBtlsX509Error.CRL_HAS_EXPIRED:
				return X509ChainStatusFlags.OfflineRevocation;
			case MonoBtlsX509Error.OUT_OF_MEM:
				throw new OutOfMemoryException();
			case MonoBtlsX509Error.DEPTH_ZERO_SELF_SIGNED_CERT:
			case MonoBtlsX509Error.SELF_SIGNED_CERT_IN_CHAIN:
			case MonoBtlsX509Error.CERT_UNTRUSTED:
				return X509ChainStatusFlags.UntrustedRoot;
			case MonoBtlsX509Error.CERT_CHAIN_TOO_LONG:
				throw new CryptographicException();
			case MonoBtlsX509Error.CERT_REVOKED:
				return X509ChainStatusFlags.Revoked;
			case MonoBtlsX509Error.INVALID_CA:
			case MonoBtlsX509Error.PATH_LENGTH_EXCEEDED:
			case MonoBtlsX509Error.KEYUSAGE_NO_CERTSIGN:
			case MonoBtlsX509Error.INVALID_NON_CA:
			case MonoBtlsX509Error.KEYUSAGE_NO_DIGITAL_SIGNATURE:
				return X509ChainStatusFlags.InvalidBasicConstraints;
			case MonoBtlsX509Error.INVALID_PURPOSE:
				return X509ChainStatusFlags.NotValidForUsage;
			case MonoBtlsX509Error.CERT_REJECTED:
				return X509ChainStatusFlags.ExplicitDistrust;
			case MonoBtlsX509Error.UNHANDLED_CRITICAL_EXTENSION:
				return X509ChainStatusFlags.HasNotSupportedCriticalExtension;
			case MonoBtlsX509Error.INVALID_EXTENSION:
				return X509ChainStatusFlags.InvalidExtension;
			case MonoBtlsX509Error.INVALID_POLICY_EXTENSION:
			case MonoBtlsX509Error.NO_EXPLICIT_POLICY:
				return X509ChainStatusFlags.InvalidPolicyConstraints;
			case MonoBtlsX509Error.HOSTNAME_MISMATCH:
				return X509ChainStatusFlags.UntrustedRoot;
			}
			throw new CryptographicException("Unrecognized X509VerifyStatusCode:" + code.ToString());
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x0000E5E8 File Offset: 0x0000C7E8
		internal static void SetupCertificateStore(MonoBtlsX509Store store, MonoTlsSettings settings, bool server)
		{
			if (server || ((settings != null) ? settings.CertificateSearchPaths : null) == null)
			{
				MonoBtlsProvider.AddTrustedRoots(store, settings, server);
				if (!server)
				{
					MonoBtlsProvider.SetupDefaultCertificateStore(store);
				}
				return;
			}
			foreach (string text in settings.CertificateSearchPaths)
			{
				if (!(text == "@default"))
				{
					if (!(text == "@trusted"))
					{
						if (!(text == "@user"))
						{
							if (!(text == "@machine"))
							{
								if (text.StartsWith("@pem:"))
								{
									string text2 = text.Substring(5);
									if (Directory.Exists(text2))
									{
										store.AddDirectoryLookup(text2, MonoBtlsX509FileType.PEM);
									}
								}
								else
								{
									if (!text.StartsWith("@der:"))
									{
										throw new NotSupportedException(string.Format("Invalid item `{0}' in MonoTlsSettings.CertificateSearchPaths.", text));
									}
									string text3 = text.Substring(5);
									if (Directory.Exists(text3))
									{
										store.AddDirectoryLookup(text3, MonoBtlsX509FileType.ASN1);
									}
								}
							}
							else
							{
								MonoBtlsProvider.AddMachineStore(store);
							}
						}
						else
						{
							MonoBtlsProvider.AddUserStore(store);
						}
					}
					else
					{
						MonoBtlsProvider.AddTrustedRoots(store, settings, server);
					}
				}
				else
				{
					MonoBtlsProvider.AddTrustedRoots(store, settings, server);
					MonoBtlsProvider.AddUserStore(store);
					MonoBtlsProvider.AddMachineStore(store);
				}
			}
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x0000E6FF File Offset: 0x0000C8FF
		private static void SetupDefaultCertificateStore(MonoBtlsX509Store store)
		{
			MonoBtlsProvider.AddUserStore(store);
			MonoBtlsProvider.AddMachineStore(store);
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x0000E710 File Offset: 0x0000C910
		private static void AddUserStore(MonoBtlsX509Store store)
		{
			string storePath = MonoBtlsX509StoreManager.GetStorePath(MonoBtlsX509StoreType.UserTrustedRoots);
			if (Directory.Exists(storePath))
			{
				store.AddDirectoryLookup(storePath, MonoBtlsX509FileType.PEM);
			}
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x0000E734 File Offset: 0x0000C934
		private static void AddMachineStore(MonoBtlsX509Store store)
		{
			string storePath = MonoBtlsX509StoreManager.GetStorePath(MonoBtlsX509StoreType.MachineTrustedRoots);
			if (Directory.Exists(storePath))
			{
				store.AddDirectoryLookup(storePath, MonoBtlsX509FileType.PEM);
			}
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x0000E758 File Offset: 0x0000C958
		private static void AddTrustedRoots(MonoBtlsX509Store store, MonoTlsSettings settings, bool server)
		{
			if (((settings != null) ? settings.TrustAnchors : null) == null)
			{
				return;
			}
			MonoBtlsX509TrustKind trust = server ? MonoBtlsX509TrustKind.TRUST_CLIENT : MonoBtlsX509TrustKind.TRUST_SERVER;
			store.AddCollection(settings.TrustAnchors, trust);
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x0000E789 File Offset: 0x0000C989
		public static string GetSystemStoreLocation()
		{
			return MonoBtlsX509StoreManager.GetStorePath(MonoBtlsX509StoreType.MachineTrustedRoots);
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x0000E794 File Offset: 0x0000C994
		public static X509Certificate2 CreateCertificate(byte[] data, MonoBtlsX509Format format)
		{
			X509Certificate2 result;
			using (X509CertificateImplBtls x509CertificateImplBtls = new X509CertificateImplBtls(data, format))
			{
				result = new X509Certificate2(x509CertificateImplBtls);
			}
			return result;
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x0000E7D0 File Offset: 0x0000C9D0
		public static X509Certificate2 CreateCertificate(byte[] data, string password, bool disallowFallback = false)
		{
			X509Certificate2 result;
			using (SafePasswordHandle safePasswordHandle = new SafePasswordHandle(password))
			{
				using (X509CertificateImplBtls x509CertificateImplBtls = new X509CertificateImplBtls(data, safePasswordHandle, X509KeyStorageFlags.DefaultKeySet))
				{
					result = new X509Certificate2(x509CertificateImplBtls);
				}
			}
			return result;
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x0000E828 File Offset: 0x0000CA28
		public static X509Certificate2 CreateCertificate(MonoBtlsX509 x509)
		{
			X509Certificate2 result;
			using (X509CertificateImplBtls x509CertificateImplBtls = new X509CertificateImplBtls(x509))
			{
				result = new X509Certificate2(x509CertificateImplBtls);
			}
			return result;
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x0000E860 File Offset: 0x0000CA60
		public static X509Chain CreateChain()
		{
			X509Chain result;
			using (X509ChainImplBtls x509ChainImplBtls = new X509ChainImplBtls())
			{
				result = new X509Chain(x509ChainImplBtls);
			}
			return result;
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x0000E898 File Offset: 0x0000CA98
		public static X509Chain GetManagedChain(MonoBtlsX509Chain chain)
		{
			return new X509Chain(new X509ChainImplBtls(chain));
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x0000E8A8 File Offset: 0x0000CAA8
		public static MonoBtlsX509 GetBtlsCertificate(X509Certificate certificate)
		{
			X509CertificateImplBtls x509CertificateImplBtls = certificate.Impl as X509CertificateImplBtls;
			if (x509CertificateImplBtls != null)
			{
				return x509CertificateImplBtls.X509.Copy();
			}
			return MonoBtlsX509.LoadFromData(certificate.GetRawCertData(), MonoBtlsX509Format.DER);
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x0000E8DC File Offset: 0x0000CADC
		public static MonoBtlsX509Chain GetNativeChain(X509CertificateCollection certificates)
		{
			MonoBtlsX509Chain monoBtlsX509Chain = new MonoBtlsX509Chain();
			MonoBtlsX509Chain result;
			try
			{
				foreach (X509Certificate certificate in certificates)
				{
					using (MonoBtlsX509 btlsCertificate = MonoBtlsProvider.GetBtlsCertificate(certificate))
					{
						monoBtlsX509Chain.AddCertificate(btlsCertificate);
					}
				}
				result = monoBtlsX509Chain;
			}
			catch
			{
				monoBtlsX509Chain.Dispose();
				throw;
			}
			return result;
		}
	}
}
