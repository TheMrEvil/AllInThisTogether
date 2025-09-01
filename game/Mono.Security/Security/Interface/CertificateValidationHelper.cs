using System;
using System.IO;
using Mono.Net.Security;

namespace Mono.Security.Interface
{
	// Token: 0x02000039 RID: 57
	public static class CertificateValidationHelper
	{
		// Token: 0x0600024D RID: 589 RVA: 0x0000EF38 File Offset: 0x0000D138
		static CertificateValidationHelper()
		{
			if (File.Exists("/System/Library/Frameworks/Security.framework/Security"))
			{
				CertificateValidationHelper.noX509Chain = true;
				CertificateValidationHelper.supportsTrustAnchors = true;
				return;
			}
			CertificateValidationHelper.noX509Chain = false;
			CertificateValidationHelper.supportsTrustAnchors = false;
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x0600024E RID: 590 RVA: 0x0000EF5F File Offset: 0x0000D15F
		public static bool SupportsX509Chain
		{
			get
			{
				return !CertificateValidationHelper.noX509Chain;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x0600024F RID: 591 RVA: 0x0000EF69 File Offset: 0x0000D169
		public static bool SupportsTrustAnchors
		{
			get
			{
				return CertificateValidationHelper.supportsTrustAnchors;
			}
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0000EF70 File Offset: 0x0000D170
		public static ICertificateValidator GetValidator(MonoTlsSettings settings)
		{
			return (ICertificateValidator)NoReflectionHelper.GetDefaultValidator(settings);
		}

		// Token: 0x04000147 RID: 327
		private const string SecurityLibrary = "/System/Library/Frameworks/Security.framework/Security";

		// Token: 0x04000148 RID: 328
		private static readonly bool noX509Chain;

		// Token: 0x04000149 RID: 329
		private static readonly bool supportsTrustAnchors;
	}
}
