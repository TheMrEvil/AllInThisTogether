using System;
using Unity;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Represents an element of an X.509 chain.</summary>
	// Token: 0x020002DA RID: 730
	public class X509ChainElement
	{
		// Token: 0x06001703 RID: 5891 RVA: 0x0005B3E8 File Offset: 0x000595E8
		internal X509ChainElement(X509Certificate2 certificate)
		{
			this.certificate = certificate;
			this.info = string.Empty;
		}

		/// <summary>Gets the X.509 certificate at a particular chain element.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object.</returns>
		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x06001704 RID: 5892 RVA: 0x0005B402 File Offset: 0x00059602
		public X509Certificate2 Certificate
		{
			get
			{
				return this.certificate;
			}
		}

		/// <summary>Gets the error status of the current X.509 certificate in a chain.</summary>
		/// <returns>An array of <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainStatus" /> objects.</returns>
		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x06001705 RID: 5893 RVA: 0x0005B40A File Offset: 0x0005960A
		public X509ChainStatus[] ChainElementStatus
		{
			get
			{
				return this.status;
			}
		}

		/// <summary>Gets additional error information from an unmanaged certificate chain structure.</summary>
		/// <returns>A string representing the <see langword="pwszExtendedErrorInfo" /> member of the unmanaged <see langword="CERT_CHAIN_ELEMENT" /> structure in the Crypto API.</returns>
		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x06001706 RID: 5894 RVA: 0x0005B412 File Offset: 0x00059612
		public string Information
		{
			get
			{
				return this.info;
			}
		}

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x06001707 RID: 5895 RVA: 0x0005B41A File Offset: 0x0005961A
		// (set) Token: 0x06001708 RID: 5896 RVA: 0x0005B422 File Offset: 0x00059622
		internal X509ChainStatusFlags StatusFlags
		{
			get
			{
				return this.compressed_status_flags;
			}
			set
			{
				this.compressed_status_flags = value;
			}
		}

		// Token: 0x06001709 RID: 5897 RVA: 0x0005B42C File Offset: 0x0005962C
		private int Count(X509ChainStatusFlags flags)
		{
			int num = 0;
			int num2 = 0;
			int num3 = 1;
			while (num2++ < 32)
			{
				if ((flags & (X509ChainStatusFlags)num3) == (X509ChainStatusFlags)num3)
				{
					num++;
				}
				num3 <<= 1;
			}
			return num;
		}

		// Token: 0x0600170A RID: 5898 RVA: 0x0005B45B File Offset: 0x0005965B
		private void Set(X509ChainStatus[] status, ref int position, X509ChainStatusFlags flags, X509ChainStatusFlags mask)
		{
			if ((flags & mask) != X509ChainStatusFlags.NoError)
			{
				status[position].Status = mask;
				status[position].StatusInformation = X509ChainStatus.GetInformation(mask);
				position++;
			}
		}

		// Token: 0x0600170B RID: 5899 RVA: 0x0005B48C File Offset: 0x0005968C
		internal void UncompressFlags()
		{
			if (this.compressed_status_flags == X509ChainStatusFlags.NoError)
			{
				this.status = new X509ChainStatus[0];
				return;
			}
			int num = this.Count(this.compressed_status_flags);
			this.status = new X509ChainStatus[num];
			int num2 = 0;
			this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.UntrustedRoot);
			this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.NotTimeValid);
			this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.NotTimeNested);
			this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.Revoked);
			this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.NotSignatureValid);
			this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.NotValidForUsage);
			this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.RevocationStatusUnknown);
			this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.Cyclic);
			this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.InvalidExtension);
			this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.InvalidPolicyConstraints);
			this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.InvalidBasicConstraints);
			this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.InvalidNameConstraints);
			this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.HasNotSupportedNameConstraint);
			this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.HasNotDefinedNameConstraint);
			this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.HasNotPermittedNameConstraint);
			this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.HasExcludedNameConstraint);
			this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.PartialChain);
			this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.CtlNotTimeValid);
			this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.CtlNotSignatureValid);
			this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.CtlNotValidForUsage);
			this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.OfflineRevocation);
			this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.NoIssuanceChainPolicy);
		}

		// Token: 0x0600170C RID: 5900 RVA: 0x00013BCA File Offset: 0x00011DCA
		internal X509ChainElement()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000D03 RID: 3331
		private X509Certificate2 certificate;

		// Token: 0x04000D04 RID: 3332
		private X509ChainStatus[] status;

		// Token: 0x04000D05 RID: 3333
		private string info;

		// Token: 0x04000D06 RID: 3334
		private X509ChainStatusFlags compressed_status_flags;
	}
}
