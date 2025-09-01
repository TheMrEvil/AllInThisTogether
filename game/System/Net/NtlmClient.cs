using System;
using Mono.Http;

namespace System.Net
{
	// Token: 0x020006AA RID: 1706
	internal class NtlmClient : IAuthenticationModule
	{
		// Token: 0x06003684 RID: 13956 RVA: 0x000BF7C6 File Offset: 0x000BD9C6
		public NtlmClient()
		{
			this.authObject = new NtlmClient();
		}

		// Token: 0x06003685 RID: 13957 RVA: 0x000BF7D9 File Offset: 0x000BD9D9
		public Authorization Authenticate(string challenge, WebRequest webRequest, ICredentials credentials)
		{
			if (this.authObject == null)
			{
				return null;
			}
			return this.authObject.Authenticate(challenge, webRequest, credentials);
		}

		// Token: 0x06003686 RID: 13958 RVA: 0x00002F6A File Offset: 0x0000116A
		public Authorization PreAuthenticate(WebRequest webRequest, ICredentials credentials)
		{
			return null;
		}

		// Token: 0x17000B47 RID: 2887
		// (get) Token: 0x06003687 RID: 13959 RVA: 0x0000CA6C File Offset: 0x0000AC6C
		public string AuthenticationType
		{
			get
			{
				return "NTLM";
			}
		}

		// Token: 0x17000B48 RID: 2888
		// (get) Token: 0x06003688 RID: 13960 RVA: 0x00003062 File Offset: 0x00001262
		public bool CanPreAuthenticate
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04001FD6 RID: 8150
		private IAuthenticationModule authObject;
	}
}
