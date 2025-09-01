using System;
using System.Text;
using Microsoft.Win32.SafeHandles;
using Mono;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020004DB RID: 1243
	internal static class X509Helper
	{
		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x060031CF RID: 12751 RVA: 0x000B7888 File Offset: 0x000B5A88
		private static ISystemCertificateProvider CertificateProvider
		{
			get
			{
				return DependencyInjector.SystemProvider.CertificateProvider;
			}
		}

		// Token: 0x060031D0 RID: 12752 RVA: 0x000B7894 File Offset: 0x000B5A94
		public static X509CertificateImpl InitFromCertificate(X509Certificate cert)
		{
			return X509Helper.CertificateProvider.Import(cert, CertificateImportFlags.None);
		}

		// Token: 0x060031D1 RID: 12753 RVA: 0x000B78A2 File Offset: 0x000B5AA2
		public static X509CertificateImpl InitFromCertificate(X509CertificateImpl impl)
		{
			if (impl == null)
			{
				return null;
			}
			return impl.Clone();
		}

		// Token: 0x060031D2 RID: 12754 RVA: 0x000B78AF File Offset: 0x000B5AAF
		public static bool IsValid(X509CertificateImpl impl)
		{
			return impl != null && impl.IsValid;
		}

		// Token: 0x060031D3 RID: 12755 RVA: 0x000B78BC File Offset: 0x000B5ABC
		internal static void ThrowIfContextInvalid(X509CertificateImpl impl)
		{
			if (!X509Helper.IsValid(impl))
			{
				throw X509Helper.GetInvalidContextException();
			}
		}

		// Token: 0x060031D4 RID: 12756 RVA: 0x000B78CC File Offset: 0x000B5ACC
		internal static Exception GetInvalidContextException()
		{
			return new CryptographicException(Locale.GetText("Certificate instance is empty."));
		}

		// Token: 0x060031D5 RID: 12757 RVA: 0x000B78DD File Offset: 0x000B5ADD
		public static X509CertificateImpl Import(byte[] rawData)
		{
			return X509Helper.CertificateProvider.Import(rawData, CertificateImportFlags.None);
		}

		// Token: 0x060031D6 RID: 12758 RVA: 0x000B78EB File Offset: 0x000B5AEB
		public static X509CertificateImpl Import(byte[] rawData, SafePasswordHandle password, X509KeyStorageFlags keyStorageFlags)
		{
			return X509Helper.CertificateProvider.Import(rawData, password, keyStorageFlags, CertificateImportFlags.None);
		}

		// Token: 0x060031D7 RID: 12759 RVA: 0x000B78FB File Offset: 0x000B5AFB
		public static byte[] Export(X509CertificateImpl impl, X509ContentType contentType, SafePasswordHandle password)
		{
			X509Helper.ThrowIfContextInvalid(impl);
			return impl.Export(contentType, password);
		}

		// Token: 0x060031D8 RID: 12760 RVA: 0x000B790C File Offset: 0x000B5B0C
		public static bool Equals(X509CertificateImpl first, X509CertificateImpl second)
		{
			if (!X509Helper.IsValid(first) || !X509Helper.IsValid(second))
			{
				return false;
			}
			bool result;
			if (first.Equals(second, out result))
			{
				return result;
			}
			byte[] rawData = first.RawData;
			byte[] rawData2 = second.RawData;
			if (rawData == null)
			{
				return rawData2 == null;
			}
			if (rawData2 == null)
			{
				return false;
			}
			if (rawData.Length != rawData2.Length)
			{
				return false;
			}
			for (int i = 0; i < rawData.Length; i++)
			{
				if (rawData[i] != rawData2[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060031D9 RID: 12761 RVA: 0x000B7978 File Offset: 0x000B5B78
		public static string ToHexString(byte[] data)
		{
			if (data != null)
			{
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < data.Length; i++)
				{
					stringBuilder.Append(data[i].ToString("X2"));
				}
				return stringBuilder.ToString();
			}
			return null;
		}
	}
}
