using System;
using System.Collections.Specialized;
using System.Text;

namespace WebSocketSharp.Net
{
	// Token: 0x0200003B RID: 59
	internal abstract class AuthenticationBase
	{
		// Token: 0x060003DC RID: 988 RVA: 0x00017D6A File Offset: 0x00015F6A
		protected AuthenticationBase(AuthenticationSchemes scheme, NameValueCollection parameters)
		{
			this._scheme = scheme;
			this.Parameters = parameters;
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060003DD RID: 989 RVA: 0x00017D84 File Offset: 0x00015F84
		public string Algorithm
		{
			get
			{
				return this.Parameters["algorithm"];
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060003DE RID: 990 RVA: 0x00017DA8 File Offset: 0x00015FA8
		public string Nonce
		{
			get
			{
				return this.Parameters["nonce"];
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060003DF RID: 991 RVA: 0x00017DCC File Offset: 0x00015FCC
		public string Opaque
		{
			get
			{
				return this.Parameters["opaque"];
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060003E0 RID: 992 RVA: 0x00017DF0 File Offset: 0x00015FF0
		public string Qop
		{
			get
			{
				return this.Parameters["qop"];
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060003E1 RID: 993 RVA: 0x00017E14 File Offset: 0x00016014
		public string Realm
		{
			get
			{
				return this.Parameters["realm"];
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060003E2 RID: 994 RVA: 0x00017E38 File Offset: 0x00016038
		public AuthenticationSchemes Scheme
		{
			get
			{
				return this._scheme;
			}
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x00017E50 File Offset: 0x00016050
		internal static string CreateNonceValue()
		{
			byte[] array = new byte[16];
			Random random = new Random();
			random.NextBytes(array);
			StringBuilder stringBuilder = new StringBuilder(32);
			foreach (byte b in array)
			{
				stringBuilder.Append(b.ToString("x2"));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x00017EB8 File Offset: 0x000160B8
		internal static NameValueCollection ParseParameters(string value)
		{
			NameValueCollection nameValueCollection = new NameValueCollection();
			foreach (string text in value.SplitHeaderValue(new char[]
			{
				','
			}))
			{
				int num = text.IndexOf('=');
				string name = (num > 0) ? text.Substring(0, num).Trim() : null;
				string value2 = (num < 0) ? text.Trim().Trim(new char[]
				{
					'"'
				}) : ((num < text.Length - 1) ? text.Substring(num + 1).Trim().Trim(new char[]
				{
					'"'
				}) : string.Empty);
				nameValueCollection.Add(name, value2);
			}
			return nameValueCollection;
		}

		// Token: 0x060003E5 RID: 997
		internal abstract string ToBasicString();

		// Token: 0x060003E6 RID: 998
		internal abstract string ToDigestString();

		// Token: 0x060003E7 RID: 999 RVA: 0x00017F9C File Offset: 0x0001619C
		public override string ToString()
		{
			return (this._scheme == AuthenticationSchemes.Basic) ? this.ToBasicString() : ((this._scheme == AuthenticationSchemes.Digest) ? this.ToDigestString() : string.Empty);
		}

		// Token: 0x04000198 RID: 408
		private AuthenticationSchemes _scheme;

		// Token: 0x04000199 RID: 409
		internal NameValueCollection Parameters;
	}
}
