using System;
using System.Collections;

namespace System.Net
{
	// Token: 0x02000676 RID: 1654
	internal class DigestClient : IAuthenticationModule
	{
		// Token: 0x17000A87 RID: 2695
		// (get) Token: 0x0600340E RID: 13326 RVA: 0x000B5960 File Offset: 0x000B3B60
		private static Hashtable Cache
		{
			get
			{
				object syncRoot = DigestClient.cache.SyncRoot;
				lock (syncRoot)
				{
					DigestClient.CheckExpired(DigestClient.cache.Count);
				}
				return DigestClient.cache;
			}
		}

		// Token: 0x0600340F RID: 13327 RVA: 0x000B59B4 File Offset: 0x000B3BB4
		private static void CheckExpired(int count)
		{
			if (count < 10)
			{
				return;
			}
			DateTime t = DateTime.MaxValue;
			DateTime utcNow = DateTime.UtcNow;
			ArrayList arrayList = null;
			foreach (object obj in DigestClient.cache.Keys)
			{
				int num = (int)obj;
				DigestSession digestSession = (DigestSession)DigestClient.cache[num];
				if (digestSession.LastUse < t && (digestSession.LastUse - utcNow).Ticks > 6000000000L)
				{
					t = digestSession.LastUse;
					if (arrayList == null)
					{
						arrayList = new ArrayList();
					}
					arrayList.Add(num);
				}
			}
			if (arrayList != null)
			{
				foreach (object obj2 in arrayList)
				{
					int num2 = (int)obj2;
					DigestClient.cache.Remove(num2);
				}
			}
		}

		// Token: 0x06003410 RID: 13328 RVA: 0x000B5AE0 File Offset: 0x000B3CE0
		public Authorization Authenticate(string challenge, WebRequest webRequest, ICredentials credentials)
		{
			if (credentials == null || challenge == null)
			{
				return null;
			}
			if (challenge.Trim().ToLower().IndexOf("digest") == -1)
			{
				return null;
			}
			HttpWebRequest httpWebRequest = webRequest as HttpWebRequest;
			if (httpWebRequest == null)
			{
				return null;
			}
			DigestSession digestSession = new DigestSession();
			if (!digestSession.Parse(challenge))
			{
				return null;
			}
			int num = httpWebRequest.Address.GetHashCode() ^ credentials.GetHashCode() ^ digestSession.Nonce.GetHashCode();
			DigestSession digestSession2 = (DigestSession)DigestClient.Cache[num];
			bool flag = digestSession2 == null;
			if (flag)
			{
				digestSession2 = digestSession;
			}
			else if (!digestSession2.Parse(challenge))
			{
				return null;
			}
			if (flag)
			{
				DigestClient.Cache.Add(num, digestSession2);
			}
			return digestSession2.Authenticate(webRequest, credentials);
		}

		// Token: 0x06003411 RID: 13329 RVA: 0x000B5B98 File Offset: 0x000B3D98
		public Authorization PreAuthenticate(WebRequest webRequest, ICredentials credentials)
		{
			HttpWebRequest httpWebRequest = webRequest as HttpWebRequest;
			if (httpWebRequest == null)
			{
				return null;
			}
			if (credentials == null)
			{
				return null;
			}
			int num = httpWebRequest.Address.GetHashCode() ^ credentials.GetHashCode();
			DigestSession digestSession = (DigestSession)DigestClient.Cache[num];
			if (digestSession == null)
			{
				return null;
			}
			return digestSession.Authenticate(webRequest, credentials);
		}

		// Token: 0x17000A88 RID: 2696
		// (get) Token: 0x06003412 RID: 13330 RVA: 0x000B5BEC File Offset: 0x000B3DEC
		public string AuthenticationType
		{
			get
			{
				return "Digest";
			}
		}

		// Token: 0x17000A89 RID: 2697
		// (get) Token: 0x06003413 RID: 13331 RVA: 0x0000390E File Offset: 0x00001B0E
		public bool CanPreAuthenticate
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003414 RID: 13332 RVA: 0x0000219B File Offset: 0x0000039B
		public DigestClient()
		{
		}

		// Token: 0x06003415 RID: 13333 RVA: 0x000B5BF3 File Offset: 0x000B3DF3
		// Note: this type is marked as 'beforefieldinit'.
		static DigestClient()
		{
		}

		// Token: 0x04001E82 RID: 7810
		private static readonly Hashtable cache = Hashtable.Synchronized(new Hashtable());
	}
}
