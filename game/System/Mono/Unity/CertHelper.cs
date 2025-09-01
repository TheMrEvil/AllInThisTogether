using System;
using System.Security.Cryptography.X509Certificates;

namespace Mono.Unity
{
	// Token: 0x0200003E RID: 62
	internal static class CertHelper
	{
		// Token: 0x060000FC RID: 252 RVA: 0x00003DA8 File Offset: 0x00001FA8
		public unsafe static void AddCertificatesToNativeChain(UnityTls.unitytls_x509list* nativeCertificateChain, X509CertificateCollection certificates, UnityTls.unitytls_errorstate* errorState)
		{
			foreach (X509Certificate certificate in certificates)
			{
				CertHelper.AddCertificateToNativeChain(nativeCertificateChain, certificate, errorState);
			}
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00003DF8 File Offset: 0x00001FF8
		public unsafe static void AddCertificateToNativeChain(UnityTls.unitytls_x509list* nativeCertificateChain, X509Certificate certificate, UnityTls.unitytls_errorstate* errorState)
		{
			byte[] rawCertData = certificate.GetRawCertData();
			byte[] array;
			byte* buffer;
			if ((array = rawCertData) == null || array.Length == 0)
			{
				buffer = null;
			}
			else
			{
				buffer = &array[0];
			}
			UnityTls.NativeInterface.unitytls_x509list_append_der(nativeCertificateChain, buffer, (IntPtr)rawCertData.Length, errorState);
			array = null;
			X509Certificate2Impl x509Certificate2Impl = certificate.Impl as X509Certificate2Impl;
			if (x509Certificate2Impl != null)
			{
				X509CertificateImplCollection intermediateCertificates = x509Certificate2Impl.IntermediateCertificates;
				if (intermediateCertificates != null && intermediateCertificates.Count > 0)
				{
					for (int i = 0; i < intermediateCertificates.Count; i++)
					{
						CertHelper.AddCertificateToNativeChain(nativeCertificateChain, new X509Certificate(intermediateCertificates[i]), errorState);
					}
				}
			}
		}
	}
}
