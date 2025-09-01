using System;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

namespace System.Net
{
	// Token: 0x02000667 RID: 1639
	internal class ServerCertValidationCallback
	{
		// Token: 0x060033BE RID: 13246 RVA: 0x000B4408 File Offset: 0x000B2608
		internal ServerCertValidationCallback(RemoteCertificateValidationCallback validationCallback)
		{
			this.m_ValidationCallback = validationCallback;
			this.m_Context = ExecutionContext.Capture();
		}

		// Token: 0x17000A72 RID: 2674
		// (get) Token: 0x060033BF RID: 13247 RVA: 0x000B4422 File Offset: 0x000B2622
		internal RemoteCertificateValidationCallback ValidationCallback
		{
			get
			{
				return this.m_ValidationCallback;
			}
		}

		// Token: 0x060033C0 RID: 13248 RVA: 0x000B442C File Offset: 0x000B262C
		internal void Callback(object state)
		{
			ServerCertValidationCallback.CallbackContext callbackContext = (ServerCertValidationCallback.CallbackContext)state;
			callbackContext.result = this.m_ValidationCallback(callbackContext.request, callbackContext.certificate, callbackContext.chain, callbackContext.sslPolicyErrors);
		}

		// Token: 0x060033C1 RID: 13249 RVA: 0x000B446C File Offset: 0x000B266C
		internal bool Invoke(object request, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{
			if (this.m_Context == null)
			{
				return this.m_ValidationCallback(request, certificate, chain, sslPolicyErrors);
			}
			ExecutionContext executionContext = this.m_Context.CreateCopy();
			ServerCertValidationCallback.CallbackContext callbackContext = new ServerCertValidationCallback.CallbackContext(request, certificate, chain, sslPolicyErrors);
			ExecutionContext.Run(executionContext, new ContextCallback(this.Callback), callbackContext);
			return callbackContext.result;
		}

		// Token: 0x04001E53 RID: 7763
		private readonly RemoteCertificateValidationCallback m_ValidationCallback;

		// Token: 0x04001E54 RID: 7764
		private readonly ExecutionContext m_Context;

		// Token: 0x02000668 RID: 1640
		private class CallbackContext
		{
			// Token: 0x060033C2 RID: 13250 RVA: 0x000B44C0 File Offset: 0x000B26C0
			internal CallbackContext(object request, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
			{
				this.request = request;
				this.certificate = certificate;
				this.chain = chain;
				this.sslPolicyErrors = sslPolicyErrors;
			}

			// Token: 0x04001E55 RID: 7765
			internal readonly object request;

			// Token: 0x04001E56 RID: 7766
			internal readonly X509Certificate certificate;

			// Token: 0x04001E57 RID: 7767
			internal readonly X509Chain chain;

			// Token: 0x04001E58 RID: 7768
			internal readonly SslPolicyErrors sslPolicyErrors;

			// Token: 0x04001E59 RID: 7769
			internal bool result;
		}
	}
}
