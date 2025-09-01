using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.Win32.SafeHandles;
using Mono.Security;
using Mono.Security.Authenticode;

namespace Mono
{
	// Token: 0x02000034 RID: 52
	internal abstract class X509PalImpl
	{
		// Token: 0x060000A8 RID: 168
		public abstract X509CertificateImpl Import(byte[] data);

		// Token: 0x060000A9 RID: 169
		public abstract X509Certificate2Impl Import(byte[] data, SafePasswordHandle password, X509KeyStorageFlags keyStorageFlags);

		// Token: 0x060000AA RID: 170
		public abstract X509Certificate2Impl Import(X509Certificate cert);

		// Token: 0x060000AB RID: 171 RVA: 0x00002F78 File Offset: 0x00001178
		private static byte[] PEM(string type, byte[] data)
		{
			string @string = Encoding.ASCII.GetString(data);
			string text = string.Format("-----BEGIN {0}-----", type);
			string value = string.Format("-----END {0}-----", type);
			int num = @string.IndexOf(text) + text.Length;
			int num2 = @string.IndexOf(value, num);
			return Convert.FromBase64String(@string.Substring(num, num2 - num));
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00002FD0 File Offset: 0x000011D0
		protected static byte[] ConvertData(byte[] data)
		{
			if (data == null || data.Length == 0)
			{
				return data;
			}
			if (data[0] != 48)
			{
				try
				{
					return X509PalImpl.PEM("CERTIFICATE", data);
				}
				catch
				{
				}
				return data;
			}
			return data;
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00003014 File Offset: 0x00001214
		internal X509Certificate2Impl ImportFallback(byte[] data)
		{
			data = X509PalImpl.ConvertData(data);
			X509Certificate2Impl result;
			using (SafePasswordHandle safePasswordHandle = new SafePasswordHandle(null))
			{
				result = new X509Certificate2ImplMono(data, safePasswordHandle, X509KeyStorageFlags.DefaultKeySet);
			}
			return result;
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00003058 File Offset: 0x00001258
		internal X509Certificate2Impl ImportFallback(byte[] data, SafePasswordHandle password, X509KeyStorageFlags keyStorageFlags)
		{
			return new X509Certificate2ImplMono(data, password, keyStorageFlags);
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000AF RID: 175 RVA: 0x00003062 File Offset: 0x00001262
		public bool SupportsLegacyBasicConstraintsExtension
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00003068 File Offset: 0x00001268
		public X509ContentType GetCertContentType(byte[] rawData)
		{
			if (rawData == null || rawData.Length == 0)
			{
				throw new ArgumentException("rawData");
			}
			if (rawData[0] == 48)
			{
				try
				{
					ASN1 asn = new ASN1(rawData);
					if (asn.Count == 3 && asn[0].Tag == 48 && asn[1].Tag == 48 && asn[2].Tag == 3)
					{
						return X509ContentType.Cert;
					}
					if (asn.Count == 3 && asn[0].Tag == 2 && asn[1].Tag == 48 && asn[2].Tag == 48)
					{
						return X509ContentType.Pfx;
					}
					if (asn.Count > 0 && asn[0].Tag == 6 && asn[0].CompareValue(X509PalImpl.signedData))
					{
						return X509ContentType.Pkcs7;
					}
					return X509ContentType.Unknown;
				}
				catch (Exception)
				{
					return X509ContentType.Unknown;
				}
			}
			if (Encoding.ASCII.GetString(rawData).IndexOf("-----BEGIN CERTIFICATE-----") >= 0)
			{
				return X509ContentType.Cert;
			}
			X509ContentType result;
			try
			{
				new AuthenticodeDeformatter(rawData);
				result = X509ContentType.Authenticode;
			}
			catch
			{
				result = X509ContentType.Unknown;
			}
			return result;
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00003194 File Offset: 0x00001394
		public X509ContentType GetCertContentType(string fileName)
		{
			if (fileName == null)
			{
				throw new ArgumentNullException("fileName");
			}
			if (fileName.Length == 0)
			{
				throw new ArgumentException("fileName");
			}
			byte[] rawData = File.ReadAllBytes(fileName);
			return this.GetCertContentType(rawData);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x0000219B File Offset: 0x0000039B
		protected X509PalImpl()
		{
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x000031D0 File Offset: 0x000013D0
		// Note: this type is marked as 'beforefieldinit'.
		static X509PalImpl()
		{
		}

		// Token: 0x04000124 RID: 292
		private static byte[] signedData = new byte[]
		{
			42,
			134,
			72,
			134,
			247,
			13,
			1,
			7,
			2
		};
	}
}
