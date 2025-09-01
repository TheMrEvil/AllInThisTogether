using System;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace System.Data.SqlClient.SNI
{
	// Token: 0x0200028E RID: 654
	internal class SNICommon
	{
		// Token: 0x06001E37 RID: 7735 RVA: 0x0008F2E0 File Offset: 0x0008D4E0
		internal static bool ValidateSslServerCertificate(string targetServerName, object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors policyErrors)
		{
			if (policyErrors == SslPolicyErrors.None)
			{
				return true;
			}
			if ((policyErrors & SslPolicyErrors.RemoteCertificateNameMismatch) == SslPolicyErrors.None)
			{
				return false;
			}
			string text = cert.Subject.Substring(cert.Subject.IndexOf('=') + 1);
			if (targetServerName.Length > text.Length)
			{
				return false;
			}
			if (targetServerName.Length == text.Length)
			{
				if (!targetServerName.Equals(text, StringComparison.OrdinalIgnoreCase))
				{
					return false;
				}
			}
			else
			{
				if (string.Compare(targetServerName, 0, text, 0, targetServerName.Length, StringComparison.OrdinalIgnoreCase) != 0)
				{
					return false;
				}
				if (text[targetServerName.Length] != '.')
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001E38 RID: 7736 RVA: 0x0008F367 File Offset: 0x0008D567
		internal static uint ReportSNIError(SNIProviders provider, uint nativeError, uint sniError, string errorMessage)
		{
			return SNICommon.ReportSNIError(new SNIError(provider, nativeError, sniError, errorMessage));
		}

		// Token: 0x06001E39 RID: 7737 RVA: 0x0008F377 File Offset: 0x0008D577
		internal static uint ReportSNIError(SNIProviders provider, uint sniError, Exception sniException)
		{
			return SNICommon.ReportSNIError(new SNIError(provider, sniError, sniException));
		}

		// Token: 0x06001E3A RID: 7738 RVA: 0x0008F386 File Offset: 0x0008D586
		internal static uint ReportSNIError(SNIError error)
		{
			SNILoadHandle.SingletonInstance.LastError = error;
			return 1U;
		}

		// Token: 0x06001E3B RID: 7739 RVA: 0x00003D93 File Offset: 0x00001F93
		public SNICommon()
		{
		}

		// Token: 0x040014E5 RID: 5349
		internal const int ConnTerminatedError = 2;

		// Token: 0x040014E6 RID: 5350
		internal const int InvalidParameterError = 5;

		// Token: 0x040014E7 RID: 5351
		internal const int ProtocolNotSupportedError = 8;

		// Token: 0x040014E8 RID: 5352
		internal const int ConnTimeoutError = 11;

		// Token: 0x040014E9 RID: 5353
		internal const int ConnNotUsableError = 19;

		// Token: 0x040014EA RID: 5354
		internal const int InvalidConnStringError = 25;

		// Token: 0x040014EB RID: 5355
		internal const int HandshakeFailureError = 31;

		// Token: 0x040014EC RID: 5356
		internal const int InternalExceptionError = 35;

		// Token: 0x040014ED RID: 5357
		internal const int ConnOpenFailedError = 40;

		// Token: 0x040014EE RID: 5358
		internal const int ErrorSpnLookup = 44;

		// Token: 0x040014EF RID: 5359
		internal const int LocalDBErrorCode = 50;

		// Token: 0x040014F0 RID: 5360
		internal const int MultiSubnetFailoverWithMoreThan64IPs = 47;

		// Token: 0x040014F1 RID: 5361
		internal const int MultiSubnetFailoverWithInstanceSpecified = 48;

		// Token: 0x040014F2 RID: 5362
		internal const int MultiSubnetFailoverWithNonTcpProtocol = 49;

		// Token: 0x040014F3 RID: 5363
		internal const int MaxErrorValue = 50157;

		// Token: 0x040014F4 RID: 5364
		internal const int LocalDBNoInstanceName = 51;

		// Token: 0x040014F5 RID: 5365
		internal const int LocalDBNoInstallation = 52;

		// Token: 0x040014F6 RID: 5366
		internal const int LocalDBInvalidConfig = 53;

		// Token: 0x040014F7 RID: 5367
		internal const int LocalDBNoSqlUserInstanceDllPath = 54;

		// Token: 0x040014F8 RID: 5368
		internal const int LocalDBInvalidSqlUserInstanceDllPath = 55;

		// Token: 0x040014F9 RID: 5369
		internal const int LocalDBFailedToLoadDll = 56;

		// Token: 0x040014FA RID: 5370
		internal const int LocalDBBadRuntime = 57;
	}
}
