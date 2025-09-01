using System;
using System.Security.Cryptography;
using System.Text;

namespace System.Net
{
	// Token: 0x02000675 RID: 1653
	internal class DigestSession
	{
		// Token: 0x060033FF RID: 13311 RVA: 0x000B5515 File Offset: 0x000B3715
		static DigestSession()
		{
		}

		// Token: 0x06003400 RID: 13312 RVA: 0x000B5521 File Offset: 0x000B3721
		public DigestSession()
		{
			this._nc = 1;
			this.lastUse = DateTime.UtcNow;
		}

		// Token: 0x17000A80 RID: 2688
		// (get) Token: 0x06003401 RID: 13313 RVA: 0x000B553B File Offset: 0x000B373B
		public string Algorithm
		{
			get
			{
				return this.parser.Algorithm;
			}
		}

		// Token: 0x17000A81 RID: 2689
		// (get) Token: 0x06003402 RID: 13314 RVA: 0x000B5548 File Offset: 0x000B3748
		public string Realm
		{
			get
			{
				return this.parser.Realm;
			}
		}

		// Token: 0x17000A82 RID: 2690
		// (get) Token: 0x06003403 RID: 13315 RVA: 0x000B5555 File Offset: 0x000B3755
		public string Nonce
		{
			get
			{
				return this.parser.Nonce;
			}
		}

		// Token: 0x17000A83 RID: 2691
		// (get) Token: 0x06003404 RID: 13316 RVA: 0x000B5562 File Offset: 0x000B3762
		public string Opaque
		{
			get
			{
				return this.parser.Opaque;
			}
		}

		// Token: 0x17000A84 RID: 2692
		// (get) Token: 0x06003405 RID: 13317 RVA: 0x000B556F File Offset: 0x000B376F
		public string QOP
		{
			get
			{
				return this.parser.QOP;
			}
		}

		// Token: 0x17000A85 RID: 2693
		// (get) Token: 0x06003406 RID: 13318 RVA: 0x000B557C File Offset: 0x000B377C
		public string CNonce
		{
			get
			{
				if (this._cnonce == null)
				{
					byte[] array = new byte[15];
					DigestSession.rng.GetBytes(array);
					this._cnonce = Convert.ToBase64String(array);
					Array.Clear(array, 0, array.Length);
				}
				return this._cnonce;
			}
		}

		// Token: 0x06003407 RID: 13319 RVA: 0x000B55C0 File Offset: 0x000B37C0
		public bool Parse(string challenge)
		{
			this.parser = new DigestHeaderParser(challenge);
			if (!this.parser.Parse())
			{
				return false;
			}
			if (this.parser.Algorithm == null || this.parser.Algorithm.ToUpper().StartsWith("MD5"))
			{
				this.hash = MD5.Create();
			}
			return true;
		}

		// Token: 0x06003408 RID: 13320 RVA: 0x000B5620 File Offset: 0x000B3820
		private string HashToHexString(string toBeHashed)
		{
			if (this.hash == null)
			{
				return null;
			}
			this.hash.Initialize();
			byte[] array = this.hash.ComputeHash(Encoding.ASCII.GetBytes(toBeHashed));
			StringBuilder stringBuilder = new StringBuilder();
			foreach (byte b in array)
			{
				stringBuilder.Append(b.ToString("x2"));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06003409 RID: 13321 RVA: 0x000B568C File Offset: 0x000B388C
		private string HA1(string username, string password)
		{
			string toBeHashed = string.Format("{0}:{1}:{2}", username, this.Realm, password);
			if (this.Algorithm != null && this.Algorithm.ToLower() == "md5-sess")
			{
				toBeHashed = string.Format("{0}:{1}:{2}", this.HashToHexString(toBeHashed), this.Nonce, this.CNonce);
			}
			return this.HashToHexString(toBeHashed);
		}

		// Token: 0x0600340A RID: 13322 RVA: 0x000B56F0 File Offset: 0x000B38F0
		private string HA2(HttpWebRequest webRequest)
		{
			string toBeHashed = string.Format("{0}:{1}", webRequest.Method, webRequest.RequestUri.PathAndQuery);
			this.QOP == "auth-int";
			return this.HashToHexString(toBeHashed);
		}

		// Token: 0x0600340B RID: 13323 RVA: 0x000B5734 File Offset: 0x000B3934
		private string Response(string username, string password, HttpWebRequest webRequest)
		{
			string text = string.Format("{0}:{1}:", this.HA1(username, password), this.Nonce);
			if (this.QOP != null)
			{
				text += string.Format("{0}:{1}:{2}:", this._nc.ToString("X8"), this.CNonce, this.QOP);
			}
			text += this.HA2(webRequest);
			return this.HashToHexString(text);
		}

		// Token: 0x0600340C RID: 13324 RVA: 0x000B57A4 File Offset: 0x000B39A4
		public Authorization Authenticate(WebRequest webRequest, ICredentials credentials)
		{
			if (this.parser == null)
			{
				throw new InvalidOperationException();
			}
			HttpWebRequest httpWebRequest = webRequest as HttpWebRequest;
			if (httpWebRequest == null)
			{
				return null;
			}
			this.lastUse = DateTime.UtcNow;
			NetworkCredential credential = credentials.GetCredential(httpWebRequest.RequestUri, "digest");
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
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("Digest username=\"{0}\", ", userName);
			stringBuilder.AppendFormat("realm=\"{0}\", ", this.Realm);
			stringBuilder.AppendFormat("nonce=\"{0}\", ", this.Nonce);
			stringBuilder.AppendFormat("uri=\"{0}\", ", httpWebRequest.Address.PathAndQuery);
			if (this.Algorithm != null)
			{
				stringBuilder.AppendFormat("algorithm=\"{0}\", ", this.Algorithm);
			}
			stringBuilder.AppendFormat("response=\"{0}\", ", this.Response(userName, password, httpWebRequest));
			if (this.QOP != null)
			{
				stringBuilder.AppendFormat("qop=\"{0}\", ", this.QOP);
			}
			lock (this)
			{
				if (this.QOP != null)
				{
					stringBuilder.AppendFormat("nc={0:X8}, ", this._nc);
					this._nc++;
				}
			}
			if (this.CNonce != null)
			{
				stringBuilder.AppendFormat("cnonce=\"{0}\", ", this.CNonce);
			}
			if (this.Opaque != null)
			{
				stringBuilder.AppendFormat("opaque=\"{0}\", ", this.Opaque);
			}
			stringBuilder.Length -= 2;
			return new Authorization(stringBuilder.ToString());
		}

		// Token: 0x17000A86 RID: 2694
		// (get) Token: 0x0600340D RID: 13325 RVA: 0x000B5958 File Offset: 0x000B3B58
		public DateTime LastUse
		{
			get
			{
				return this.lastUse;
			}
		}

		// Token: 0x04001E7C RID: 7804
		private static RandomNumberGenerator rng = RandomNumberGenerator.Create();

		// Token: 0x04001E7D RID: 7805
		private DateTime lastUse;

		// Token: 0x04001E7E RID: 7806
		private int _nc;

		// Token: 0x04001E7F RID: 7807
		private HashAlgorithm hash;

		// Token: 0x04001E80 RID: 7808
		private DigestHeaderParser parser;

		// Token: 0x04001E81 RID: 7809
		private string _cnonce;
	}
}
