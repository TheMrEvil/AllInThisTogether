using System;
using System.Collections.Specialized;

namespace System.Net
{
	// Token: 0x020005F7 RID: 1527
	internal class KnownHttpVerb
	{
		// Token: 0x06003074 RID: 12404 RVA: 0x000A6C50 File Offset: 0x000A4E50
		internal KnownHttpVerb(string name, bool requireContentBody, bool contentBodyNotAllowed, bool connectRequest, bool expectNoContentResponse)
		{
			this.Name = name;
			this.RequireContentBody = requireContentBody;
			this.ContentBodyNotAllowed = contentBodyNotAllowed;
			this.ConnectRequest = connectRequest;
			this.ExpectNoContentResponse = expectNoContentResponse;
		}

		// Token: 0x06003075 RID: 12405 RVA: 0x000A6C80 File Offset: 0x000A4E80
		static KnownHttpVerb()
		{
			KnownHttpVerb.NamedHeaders[KnownHttpVerb.Get.Name] = KnownHttpVerb.Get;
			KnownHttpVerb.NamedHeaders[KnownHttpVerb.Connect.Name] = KnownHttpVerb.Connect;
			KnownHttpVerb.NamedHeaders[KnownHttpVerb.Head.Name] = KnownHttpVerb.Head;
			KnownHttpVerb.NamedHeaders[KnownHttpVerb.Put.Name] = KnownHttpVerb.Put;
			KnownHttpVerb.NamedHeaders[KnownHttpVerb.Post.Name] = KnownHttpVerb.Post;
			KnownHttpVerb.NamedHeaders[KnownHttpVerb.MkCol.Name] = KnownHttpVerb.MkCol;
		}

		// Token: 0x06003076 RID: 12406 RVA: 0x000A6DA4 File Offset: 0x000A4FA4
		public bool Equals(KnownHttpVerb verb)
		{
			return this == verb || string.Compare(this.Name, verb.Name, StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x06003077 RID: 12407 RVA: 0x000A6DC4 File Offset: 0x000A4FC4
		public static KnownHttpVerb Parse(string name)
		{
			KnownHttpVerb knownHttpVerb = KnownHttpVerb.NamedHeaders[name] as KnownHttpVerb;
			if (knownHttpVerb == null)
			{
				knownHttpVerb = new KnownHttpVerb(name, false, false, false, false);
			}
			return knownHttpVerb;
		}

		// Token: 0x04001C07 RID: 7175
		internal string Name;

		// Token: 0x04001C08 RID: 7176
		internal bool RequireContentBody;

		// Token: 0x04001C09 RID: 7177
		internal bool ContentBodyNotAllowed;

		// Token: 0x04001C0A RID: 7178
		internal bool ConnectRequest;

		// Token: 0x04001C0B RID: 7179
		internal bool ExpectNoContentResponse;

		// Token: 0x04001C0C RID: 7180
		private static ListDictionary NamedHeaders = new ListDictionary(CaseInsensitiveAscii.StaticInstance);

		// Token: 0x04001C0D RID: 7181
		internal static KnownHttpVerb Get = new KnownHttpVerb("GET", false, true, false, false);

		// Token: 0x04001C0E RID: 7182
		internal static KnownHttpVerb Connect = new KnownHttpVerb("CONNECT", false, true, true, false);

		// Token: 0x04001C0F RID: 7183
		internal static KnownHttpVerb Head = new KnownHttpVerb("HEAD", false, true, false, true);

		// Token: 0x04001C10 RID: 7184
		internal static KnownHttpVerb Put = new KnownHttpVerb("PUT", true, false, false, false);

		// Token: 0x04001C11 RID: 7185
		internal static KnownHttpVerb Post = new KnownHttpVerb("POST", true, false, false, false);

		// Token: 0x04001C12 RID: 7186
		internal static KnownHttpVerb MkCol = new KnownHttpVerb("MKCOL", false, false, false, false);
	}
}
