using System;
using System.Net;
using System.Runtime.CompilerServices;

namespace Mono.Http
{
	// Token: 0x020000CB RID: 203
	internal class NtlmClient : IAuthenticationModule
	{
		// Token: 0x060003FD RID: 1021 RVA: 0x0000C99C File Offset: 0x0000AB9C
		public Authorization Authenticate(string challenge, WebRequest webRequest, ICredentials credentials)
		{
			if (credentials == null || challenge == null)
			{
				return null;
			}
			string text = challenge.Trim();
			int num = text.ToLower().IndexOf("ntlm");
			if (num == -1)
			{
				return null;
			}
			num = text.IndexOfAny(new char[]
			{
				' ',
				'\t'
			});
			if (num != -1)
			{
				text = text.Substring(num).Trim();
			}
			else
			{
				text = null;
			}
			HttpWebRequest httpWebRequest = webRequest as HttpWebRequest;
			if (httpWebRequest == null)
			{
				return null;
			}
			ConditionalWeakTable<HttpWebRequest, NtlmSession> obj = NtlmClient.cache;
			Authorization result;
			lock (obj)
			{
				result = NtlmClient.cache.GetValue(httpWebRequest, (HttpWebRequest x) => new NtlmSession()).Authenticate(text, webRequest, credentials);
			}
			return result;
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x00002F6A File Offset: 0x0000116A
		public Authorization PreAuthenticate(WebRequest webRequest, ICredentials credentials)
		{
			return null;
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060003FF RID: 1023 RVA: 0x0000CA6C File Offset: 0x0000AC6C
		public string AuthenticationType
		{
			get
			{
				return "NTLM";
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000400 RID: 1024 RVA: 0x00003062 File Offset: 0x00001262
		public bool CanPreAuthenticate
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x0000219B File Offset: 0x0000039B
		public NtlmClient()
		{
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x0000CA73 File Offset: 0x0000AC73
		// Note: this type is marked as 'beforefieldinit'.
		static NtlmClient()
		{
		}

		// Token: 0x04000391 RID: 913
		private static readonly ConditionalWeakTable<HttpWebRequest, NtlmSession> cache = new ConditionalWeakTable<HttpWebRequest, NtlmSession>();

		// Token: 0x020000CC RID: 204
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000403 RID: 1027 RVA: 0x0000CA7F File Offset: 0x0000AC7F
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000404 RID: 1028 RVA: 0x0000219B File Offset: 0x0000039B
			public <>c()
			{
			}

			// Token: 0x06000405 RID: 1029 RVA: 0x0000CA8B File Offset: 0x0000AC8B
			internal NtlmSession <Authenticate>b__1_0(HttpWebRequest x)
			{
				return new NtlmSession();
			}

			// Token: 0x04000392 RID: 914
			public static readonly NtlmClient.<>c <>9 = new NtlmClient.<>c();

			// Token: 0x04000393 RID: 915
			public static ConditionalWeakTable<HttpWebRequest, NtlmSession>.CreateValueCallback <>9__1_0;
		}
	}
}
