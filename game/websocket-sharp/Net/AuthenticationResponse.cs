using System;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;

namespace WebSocketSharp.Net
{
	// Token: 0x0200003A RID: 58
	internal class AuthenticationResponse : AuthenticationBase
	{
		// Token: 0x060003C5 RID: 965 RVA: 0x00017264 File Offset: 0x00015464
		private AuthenticationResponse(AuthenticationSchemes scheme, NameValueCollection parameters) : base(scheme, parameters)
		{
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x00017555 File Offset: 0x00015755
		internal AuthenticationResponse(NetworkCredential credentials) : this(AuthenticationSchemes.Basic, new NameValueCollection(), credentials, 0U)
		{
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x00017567 File Offset: 0x00015767
		internal AuthenticationResponse(AuthenticationChallenge challenge, NetworkCredential credentials, uint nonceCount) : this(challenge.Scheme, challenge.Parameters, credentials, nonceCount)
		{
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x00017580 File Offset: 0x00015780
		internal AuthenticationResponse(AuthenticationSchemes scheme, NameValueCollection parameters, NetworkCredential credentials, uint nonceCount) : base(scheme, parameters)
		{
			this.Parameters["username"] = credentials.Username;
			this.Parameters["password"] = credentials.Password;
			this.Parameters["uri"] = credentials.Domain;
			this._nonceCount = nonceCount;
			bool flag = scheme == AuthenticationSchemes.Digest;
			if (flag)
			{
				this.initAsDigest();
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060003C9 RID: 969 RVA: 0x000175F4 File Offset: 0x000157F4
		internal uint NonceCount
		{
			get
			{
				return (this._nonceCount < uint.MaxValue) ? this._nonceCount : 0U;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060003CA RID: 970 RVA: 0x00017618 File Offset: 0x00015818
		public string Cnonce
		{
			get
			{
				return this.Parameters["cnonce"];
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060003CB RID: 971 RVA: 0x0001763C File Offset: 0x0001583C
		public string Nc
		{
			get
			{
				return this.Parameters["nc"];
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060003CC RID: 972 RVA: 0x00017660 File Offset: 0x00015860
		public string Password
		{
			get
			{
				return this.Parameters["password"];
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060003CD RID: 973 RVA: 0x00017684 File Offset: 0x00015884
		public string Response
		{
			get
			{
				return this.Parameters["response"];
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060003CE RID: 974 RVA: 0x000176A8 File Offset: 0x000158A8
		public string Uri
		{
			get
			{
				return this.Parameters["uri"];
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060003CF RID: 975 RVA: 0x000176CC File Offset: 0x000158CC
		public string UserName
		{
			get
			{
				return this.Parameters["username"];
			}
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x000176F0 File Offset: 0x000158F0
		private static string createA1(string username, string password, string realm)
		{
			return string.Format("{0}:{1}:{2}", username, realm, password);
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x00017710 File Offset: 0x00015910
		private static string createA1(string username, string password, string realm, string nonce, string cnonce)
		{
			return string.Format("{0}:{1}:{2}", AuthenticationResponse.hash(AuthenticationResponse.createA1(username, password, realm)), nonce, cnonce);
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x0001773C File Offset: 0x0001593C
		private static string createA2(string method, string uri)
		{
			return string.Format("{0}:{1}", method, uri);
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x0001775C File Offset: 0x0001595C
		private static string createA2(string method, string uri, string entity)
		{
			return string.Format("{0}:{1}:{2}", method, uri, AuthenticationResponse.hash(entity));
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x00017780 File Offset: 0x00015980
		private static string hash(string value)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(value);
			MD5 md = MD5.Create();
			byte[] array = md.ComputeHash(bytes);
			StringBuilder stringBuilder = new StringBuilder(64);
			foreach (byte b in array)
			{
				stringBuilder.Append(b.ToString("x2"));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x000177F0 File Offset: 0x000159F0
		private void initAsDigest()
		{
			string text = this.Parameters["qop"];
			bool flag = text != null;
			if (flag)
			{
				bool flag2 = text.Split(new char[]
				{
					','
				}).Contains((string qop) => qop.Trim().ToLower() == "auth");
				if (flag2)
				{
					this.Parameters["qop"] = "auth";
					this.Parameters["cnonce"] = AuthenticationBase.CreateNonceValue();
					NameValueCollection parameters = this.Parameters;
					string name = "nc";
					string format = "{0:x8}";
					uint num = this._nonceCount + 1U;
					this._nonceCount = num;
					parameters[name] = string.Format(format, num);
				}
				else
				{
					this.Parameters["qop"] = null;
				}
			}
			this.Parameters["method"] = "GET";
			this.Parameters["response"] = AuthenticationResponse.CreateRequestDigest(this.Parameters);
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x000178FC File Offset: 0x00015AFC
		internal static string CreateRequestDigest(NameValueCollection parameters)
		{
			string username = parameters["username"];
			string password = parameters["password"];
			string realm = parameters["realm"];
			string text = parameters["nonce"];
			string uri = parameters["uri"];
			string text2 = parameters["algorithm"];
			string text3 = parameters["qop"];
			string text4 = parameters["cnonce"];
			string text5 = parameters["nc"];
			string method = parameters["method"];
			string value = (text2 != null && text2.ToLower() == "md5-sess") ? AuthenticationResponse.createA1(username, password, realm, text, text4) : AuthenticationResponse.createA1(username, password, realm);
			string value2 = (text3 != null && text3.ToLower() == "auth-int") ? AuthenticationResponse.createA2(method, uri, parameters["entity"]) : AuthenticationResponse.createA2(method, uri);
			string arg = AuthenticationResponse.hash(value);
			string arg2 = (text3 != null) ? string.Format("{0}:{1}:{2}:{3}:{4}", new object[]
			{
				text,
				text5,
				text4,
				text3,
				AuthenticationResponse.hash(value2)
			}) : string.Format("{0}:{1}", text, AuthenticationResponse.hash(value2));
			return AuthenticationResponse.hash(string.Format("{0}:{1}", arg, arg2));
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x00017A58 File Offset: 0x00015C58
		internal static AuthenticationResponse Parse(string value)
		{
			try
			{
				string[] array = value.Split(new char[]
				{
					' '
				}, 2);
				bool flag = array.Length != 2;
				if (flag)
				{
					return null;
				}
				string a = array[0].ToLower();
				return (a == "basic") ? new AuthenticationResponse(AuthenticationSchemes.Basic, AuthenticationResponse.ParseBasicCredentials(array[1])) : ((a == "digest") ? new AuthenticationResponse(AuthenticationSchemes.Digest, AuthenticationBase.ParseParameters(array[1])) : null);
			}
			catch
			{
			}
			return null;
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x00017AEC File Offset: 0x00015CEC
		internal static NameValueCollection ParseBasicCredentials(string value)
		{
			string @string = Encoding.Default.GetString(Convert.FromBase64String(value));
			int num = @string.IndexOf(':');
			string text = @string.Substring(0, num);
			string value2 = (num < @string.Length - 1) ? @string.Substring(num + 1) : string.Empty;
			num = text.IndexOf('\\');
			bool flag = num > -1;
			if (flag)
			{
				text = text.Substring(num + 1);
			}
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["username"] = text;
			nameValueCollection["password"] = value2;
			return nameValueCollection;
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x00017B84 File Offset: 0x00015D84
		internal override string ToBasicString()
		{
			string s = string.Format("{0}:{1}", this.Parameters["username"], this.Parameters["password"]);
			string str = Convert.ToBase64String(Encoding.UTF8.GetBytes(s));
			return "Basic " + str;
		}

		// Token: 0x060003DA RID: 986 RVA: 0x00017BE0 File Offset: 0x00015DE0
		internal override string ToDigestString()
		{
			StringBuilder stringBuilder = new StringBuilder(256);
			stringBuilder.AppendFormat("Digest username=\"{0}\", realm=\"{1}\", nonce=\"{2}\", uri=\"{3}\", response=\"{4}\"", new object[]
			{
				this.Parameters["username"],
				this.Parameters["realm"],
				this.Parameters["nonce"],
				this.Parameters["uri"],
				this.Parameters["response"]
			});
			string text = this.Parameters["opaque"];
			bool flag = text != null;
			if (flag)
			{
				stringBuilder.AppendFormat(", opaque=\"{0}\"", text);
			}
			string text2 = this.Parameters["algorithm"];
			bool flag2 = text2 != null;
			if (flag2)
			{
				stringBuilder.AppendFormat(", algorithm={0}", text2);
			}
			string text3 = this.Parameters["qop"];
			bool flag3 = text3 != null;
			if (flag3)
			{
				stringBuilder.AppendFormat(", qop={0}, cnonce=\"{1}\", nc={2}", text3, this.Parameters["cnonce"], this.Parameters["nc"]);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060003DB RID: 987 RVA: 0x00017D10 File Offset: 0x00015F10
		public IIdentity ToIdentity()
		{
			AuthenticationSchemes scheme = base.Scheme;
			IIdentity result;
			if (scheme != AuthenticationSchemes.Basic)
			{
				IIdentity identity = (scheme == AuthenticationSchemes.Digest) ? new HttpDigestIdentity(this.Parameters) : null;
				result = identity;
			}
			else
			{
				IIdentity identity = new HttpBasicIdentity(this.Parameters["username"], this.Parameters["password"]);
				result = identity;
			}
			return result;
		}

		// Token: 0x04000197 RID: 407
		private uint _nonceCount;

		// Token: 0x0200006F RID: 111
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060005C0 RID: 1472 RVA: 0x0001EECD File Offset: 0x0001D0CD
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060005C1 RID: 1473 RVA: 0x0001DA3E File Offset: 0x0001BC3E
			public <>c()
			{
			}

			// Token: 0x060005C2 RID: 1474 RVA: 0x0001EED9 File Offset: 0x0001D0D9
			internal bool <initAsDigest>b__24_0(string qop)
			{
				return qop.Trim().ToLower() == "auth";
			}

			// Token: 0x040002DD RID: 733
			public static readonly AuthenticationResponse.<>c <>9 = new AuthenticationResponse.<>c();

			// Token: 0x040002DE RID: 734
			public static Func<string, bool> <>9__24_0;
		}
	}
}
