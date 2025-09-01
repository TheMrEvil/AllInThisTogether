using System;
using Mono.Security.Interface;

namespace Mono.Unity
{
	// Token: 0x0200003F RID: 63
	internal static class Debug
	{
		// Token: 0x060000FE RID: 254 RVA: 0x00003E90 File Offset: 0x00002090
		public static void CheckAndThrow(UnityTls.unitytls_errorstate errorState, string context, AlertDescription defaultAlert = AlertDescription.InternalError)
		{
			if (errorState.code == UnityTls.unitytls_error_code.UNITYTLS_SUCCESS)
			{
				return;
			}
			string message = string.Format("{0} - error code: {1}", context, errorState.code);
			throw new TlsException(defaultAlert, message);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00003EC4 File Offset: 0x000020C4
		public static void CheckAndThrow(UnityTls.unitytls_errorstate errorState, UnityTls.unitytls_x509verify_result verifyResult, string context, AlertDescription defaultAlert = AlertDescription.InternalError)
		{
			if (verifyResult == UnityTls.unitytls_x509verify_result.UNITYTLS_X509VERIFY_SUCCESS)
			{
				Debug.CheckAndThrow(errorState, context, defaultAlert);
				return;
			}
			AlertDescription description = UnityTlsConversions.VerifyResultToAlertDescription(verifyResult, defaultAlert);
			string message = string.Format("{0} - error code: {1}, verify result: {2}", context, errorState.code, verifyResult);
			throw new TlsException(description, message);
		}
	}
}
