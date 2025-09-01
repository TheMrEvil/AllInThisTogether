using System;

namespace System.Net
{
	// Token: 0x0200066A RID: 1642
	internal class BasicClient : IAuthenticationModule
	{
		// Token: 0x060033D4 RID: 13268 RVA: 0x000B492E File Offset: 0x000B2B2E
		public Authorization Authenticate(string challenge, WebRequest webRequest, ICredentials credentials)
		{
			if (credentials == null || challenge == null)
			{
				return null;
			}
			if (challenge.Trim().ToLower().IndexOf("basic", StringComparison.Ordinal) == -1)
			{
				return null;
			}
			return BasicClient.InternalAuthenticate(webRequest, credentials);
		}

		// Token: 0x060033D5 RID: 13269 RVA: 0x000B495C File Offset: 0x000B2B5C
		private static byte[] GetBytes(string str)
		{
			int i = str.Length;
			byte[] array = new byte[i];
			for (i--; i >= 0; i--)
			{
				array[i] = (byte)str[i];
			}
			return array;
		}

		// Token: 0x060033D6 RID: 13270 RVA: 0x000B4994 File Offset: 0x000B2B94
		private static Authorization InternalAuthenticate(WebRequest webRequest, ICredentials credentials)
		{
			HttpWebRequest httpWebRequest = webRequest as HttpWebRequest;
			if (httpWebRequest == null || credentials == null)
			{
				return null;
			}
			NetworkCredential credential = credentials.GetCredential(httpWebRequest.AuthUri, "basic");
			if (credential == null)
			{
				return null;
			}
			string userName = credential.UserName;
			if (userName == null || userName == "")
			{
				return null;
			}
			string password = credential.Password;
			string domain = credential.Domain;
			byte[] bytes;
			if (domain == null || domain == "" || domain.Trim() == "")
			{
				bytes = BasicClient.GetBytes(userName + ":" + password);
			}
			else
			{
				bytes = BasicClient.GetBytes(string.Concat(new string[]
				{
					domain,
					"\\",
					userName,
					":",
					password
				}));
			}
			return new Authorization("Basic " + Convert.ToBase64String(bytes));
		}

		// Token: 0x060033D7 RID: 13271 RVA: 0x000B4A6E File Offset: 0x000B2C6E
		public Authorization PreAuthenticate(WebRequest webRequest, ICredentials credentials)
		{
			return BasicClient.InternalAuthenticate(webRequest, credentials);
		}

		// Token: 0x17000A77 RID: 2679
		// (get) Token: 0x060033D8 RID: 13272 RVA: 0x000B4A77 File Offset: 0x000B2C77
		public string AuthenticationType
		{
			get
			{
				return "Basic";
			}
		}

		// Token: 0x17000A78 RID: 2680
		// (get) Token: 0x060033D9 RID: 13273 RVA: 0x0000390E File Offset: 0x00001B0E
		public bool CanPreAuthenticate
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060033DA RID: 13274 RVA: 0x0000219B File Offset: 0x0000039B
		public BasicClient()
		{
		}
	}
}
