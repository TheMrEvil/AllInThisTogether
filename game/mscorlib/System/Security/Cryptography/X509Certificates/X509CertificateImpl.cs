using System;
using Microsoft.Win32.SafeHandles;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020004DA RID: 1242
	internal abstract class X509CertificateImpl : IDisposable
	{
		// Token: 0x1700068B RID: 1675
		// (get) Token: 0x060031B1 RID: 12721
		public abstract bool IsValid { get; }

		// Token: 0x1700068C RID: 1676
		// (get) Token: 0x060031B2 RID: 12722
		public abstract IntPtr Handle { get; }

		// Token: 0x060031B3 RID: 12723
		public abstract IntPtr GetNativeAppleCertificate();

		// Token: 0x060031B4 RID: 12724 RVA: 0x000B778A File Offset: 0x000B598A
		protected void ThrowIfContextInvalid()
		{
			if (!this.IsValid)
			{
				throw X509Helper.GetInvalidContextException();
			}
		}

		// Token: 0x060031B5 RID: 12725
		public abstract X509CertificateImpl Clone();

		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x060031B6 RID: 12726
		public abstract string Issuer { get; }

		// Token: 0x1700068E RID: 1678
		// (get) Token: 0x060031B7 RID: 12727
		public abstract string Subject { get; }

		// Token: 0x1700068F RID: 1679
		// (get) Token: 0x060031B8 RID: 12728
		public abstract string LegacyIssuer { get; }

		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x060031B9 RID: 12729
		public abstract string LegacySubject { get; }

		// Token: 0x17000691 RID: 1681
		// (get) Token: 0x060031BA RID: 12730
		public abstract byte[] RawData { get; }

		// Token: 0x17000692 RID: 1682
		// (get) Token: 0x060031BB RID: 12731
		public abstract DateTime NotAfter { get; }

		// Token: 0x17000693 RID: 1683
		// (get) Token: 0x060031BC RID: 12732
		public abstract DateTime NotBefore { get; }

		// Token: 0x17000694 RID: 1684
		// (get) Token: 0x060031BD RID: 12733
		public abstract byte[] Thumbprint { get; }

		// Token: 0x060031BE RID: 12734 RVA: 0x000B779C File Offset: 0x000B599C
		public sealed override int GetHashCode()
		{
			if (!this.IsValid)
			{
				return 0;
			}
			byte[] thumbprint = this.Thumbprint;
			int num = 0;
			int num2 = 0;
			while (num2 < thumbprint.Length && num2 < 4)
			{
				num = (num << 8 | (int)thumbprint[num2]);
				num2++;
			}
			return num;
		}

		// Token: 0x060031BF RID: 12735
		public abstract bool Equals(X509CertificateImpl other, out bool result);

		// Token: 0x17000695 RID: 1685
		// (get) Token: 0x060031C0 RID: 12736
		public abstract string KeyAlgorithm { get; }

		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x060031C1 RID: 12737
		public abstract byte[] KeyAlgorithmParameters { get; }

		// Token: 0x17000697 RID: 1687
		// (get) Token: 0x060031C2 RID: 12738
		public abstract byte[] PublicKeyValue { get; }

		// Token: 0x17000698 RID: 1688
		// (get) Token: 0x060031C3 RID: 12739
		public abstract byte[] SerialNumber { get; }

		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x060031C4 RID: 12740
		public abstract bool HasPrivateKey { get; }

		// Token: 0x060031C5 RID: 12741
		public abstract RSA GetRSAPrivateKey();

		// Token: 0x060031C6 RID: 12742
		public abstract DSA GetDSAPrivateKey();

		// Token: 0x060031C7 RID: 12743
		public abstract byte[] Export(X509ContentType contentType, SafePasswordHandle password);

		// Token: 0x060031C8 RID: 12744
		public abstract X509CertificateImpl CopyWithPrivateKey(RSA privateKey);

		// Token: 0x060031C9 RID: 12745
		public abstract X509Certificate CreateCertificate();

		// Token: 0x060031CA RID: 12746 RVA: 0x000B77D8 File Offset: 0x000B59D8
		public sealed override bool Equals(object obj)
		{
			X509CertificateImpl x509CertificateImpl = obj as X509CertificateImpl;
			if (x509CertificateImpl == null)
			{
				return false;
			}
			if (!this.IsValid || !x509CertificateImpl.IsValid)
			{
				return false;
			}
			if (!this.Issuer.Equals(x509CertificateImpl.Issuer))
			{
				return false;
			}
			byte[] serialNumber = this.SerialNumber;
			byte[] serialNumber2 = x509CertificateImpl.SerialNumber;
			if (serialNumber.Length != serialNumber2.Length)
			{
				return false;
			}
			for (int i = 0; i < serialNumber.Length; i++)
			{
				if (serialNumber[i] != serialNumber2[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060031CB RID: 12747 RVA: 0x000B7849 File Offset: 0x000B5A49
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060031CC RID: 12748 RVA: 0x00004BF9 File Offset: 0x00002DF9
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x060031CD RID: 12749 RVA: 0x000B7858 File Offset: 0x000B5A58
		~X509CertificateImpl()
		{
			this.Dispose(false);
		}

		// Token: 0x060031CE RID: 12750 RVA: 0x0000259F File Offset: 0x0000079F
		protected X509CertificateImpl()
		{
		}
	}
}
