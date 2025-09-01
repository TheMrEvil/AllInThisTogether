using System;
using System.Collections;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Mono.Security.X509;

namespace Mono.Security.Authenticode
{
	// Token: 0x02000069 RID: 105
	public class SoftwarePublisherCertificate
	{
		// Token: 0x06000426 RID: 1062 RVA: 0x00016B64 File Offset: 0x00014D64
		public SoftwarePublisherCertificate()
		{
			this.pkcs7 = new PKCS7.SignedData();
			this.pkcs7.ContentInfo.ContentType = "1.2.840.113549.1.7.1";
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x00016B8C File Offset: 0x00014D8C
		public SoftwarePublisherCertificate(byte[] data) : this()
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			PKCS7.ContentInfo contentInfo = new PKCS7.ContentInfo(data);
			if (contentInfo.ContentType != "1.2.840.113549.1.7.2")
			{
				throw new ArgumentException(Locale.GetText("Unsupported ContentType"));
			}
			this.pkcs7 = new PKCS7.SignedData(contentInfo.Content);
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000428 RID: 1064 RVA: 0x00016BE7 File Offset: 0x00014DE7
		public X509CertificateCollection Certificates
		{
			get
			{
				return this.pkcs7.Certificates;
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000429 RID: 1065 RVA: 0x00016BF4 File Offset: 0x00014DF4
		public ArrayList Crls
		{
			get
			{
				return this.pkcs7.Crls;
			}
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x00016C01 File Offset: 0x00014E01
		public byte[] GetBytes()
		{
			PKCS7.ContentInfo contentInfo = new PKCS7.ContentInfo("1.2.840.113549.1.7.2");
			contentInfo.Content.Add(this.pkcs7.ASN1);
			return contentInfo.GetBytes();
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x00016C2C File Offset: 0x00014E2C
		public static SoftwarePublisherCertificate CreateFromFile(string filename)
		{
			if (filename == null)
			{
				throw new ArgumentNullException("filename");
			}
			byte[] array = null;
			using (FileStream fileStream = File.Open(filename, FileMode.Open, FileAccess.Read, FileShare.Read))
			{
				array = new byte[fileStream.Length];
				fileStream.Read(array, 0, array.Length);
				fileStream.Close();
			}
			if (array.Length < 2)
			{
				return null;
			}
			if (array[0] != 48)
			{
				try
				{
					array = SoftwarePublisherCertificate.PEM(array);
				}
				catch (Exception inner)
				{
					throw new CryptographicException("Invalid encoding", inner);
				}
			}
			return new SoftwarePublisherCertificate(array);
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x00016CC8 File Offset: 0x00014EC8
		private static byte[] PEM(byte[] data)
		{
			string text = (data[1] == 0) ? Encoding.Unicode.GetString(data) : Encoding.ASCII.GetString(data);
			int num = text.IndexOf("-----BEGIN PKCS7-----") + "-----BEGIN PKCS7-----".Length;
			int num2 = text.IndexOf("-----END PKCS7-----", num);
			return Convert.FromBase64String((num == -1 || num2 == -1) ? text : text.Substring(num, num2 - num));
		}

		// Token: 0x04000333 RID: 819
		private PKCS7.SignedData pkcs7;

		// Token: 0x04000334 RID: 820
		private const string header = "-----BEGIN PKCS7-----";

		// Token: 0x04000335 RID: 821
		private const string footer = "-----END PKCS7-----";
	}
}
