using System;
using System.Collections.Specialized;
using System.Security.Principal;

namespace WebSocketSharp.Net
{
	// Token: 0x02000030 RID: 48
	public class HttpDigestIdentity : GenericIdentity
	{
		// Token: 0x06000389 RID: 905 RVA: 0x000166B4 File Offset: 0x000148B4
		internal HttpDigestIdentity(NameValueCollection parameters) : base(parameters["username"], "Digest")
		{
			this._parameters = parameters;
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x0600038A RID: 906 RVA: 0x000166D8 File Offset: 0x000148D8
		public string Algorithm
		{
			get
			{
				return this._parameters["algorithm"];
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x0600038B RID: 907 RVA: 0x000166FC File Offset: 0x000148FC
		public string Cnonce
		{
			get
			{
				return this._parameters["cnonce"];
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x0600038C RID: 908 RVA: 0x00016720 File Offset: 0x00014920
		public string Nc
		{
			get
			{
				return this._parameters["nc"];
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x0600038D RID: 909 RVA: 0x00016744 File Offset: 0x00014944
		public string Nonce
		{
			get
			{
				return this._parameters["nonce"];
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x0600038E RID: 910 RVA: 0x00016768 File Offset: 0x00014968
		public string Opaque
		{
			get
			{
				return this._parameters["opaque"];
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x0600038F RID: 911 RVA: 0x0001678C File Offset: 0x0001498C
		public string Qop
		{
			get
			{
				return this._parameters["qop"];
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000390 RID: 912 RVA: 0x000167B0 File Offset: 0x000149B0
		public string Realm
		{
			get
			{
				return this._parameters["realm"];
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000391 RID: 913 RVA: 0x000167D4 File Offset: 0x000149D4
		public string Response
		{
			get
			{
				return this._parameters["response"];
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000392 RID: 914 RVA: 0x000167F8 File Offset: 0x000149F8
		public string Uri
		{
			get
			{
				return this._parameters["uri"];
			}
		}

		// Token: 0x06000393 RID: 915 RVA: 0x0001681C File Offset: 0x00014A1C
		internal bool IsValid(string password, string realm, string method, string entity)
		{
			NameValueCollection nameValueCollection = new NameValueCollection(this._parameters);
			nameValueCollection["password"] = password;
			nameValueCollection["realm"] = realm;
			nameValueCollection["method"] = method;
			nameValueCollection["entity"] = entity;
			string b = AuthenticationResponse.CreateRequestDigest(nameValueCollection);
			return this._parameters["response"] == b;
		}

		// Token: 0x04000178 RID: 376
		private NameValueCollection _parameters;
	}
}
