using System;

namespace System.Net
{
	/// <summary>Defines the HTTP version numbers that are supported by the <see cref="T:System.Net.HttpWebRequest" /> and <see cref="T:System.Net.HttpWebResponse" /> classes.</summary>
	// Token: 0x0200057B RID: 1403
	public class HttpVersion
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.HttpVersion" /> class.</summary>
		// Token: 0x06002D53 RID: 11603 RVA: 0x0000219B File Offset: 0x0000039B
		public HttpVersion()
		{
		}

		// Token: 0x06002D54 RID: 11604 RVA: 0x0009B3AD File Offset: 0x000995AD
		// Note: this type is marked as 'beforefieldinit'.
		static HttpVersion()
		{
		}

		// Token: 0x040018E0 RID: 6368
		public static readonly Version Unknown = new Version(0, 0);

		/// <summary>Defines a <see cref="T:System.Version" /> instance for HTTP 1.0.</summary>
		// Token: 0x040018E1 RID: 6369
		public static readonly Version Version10 = new Version(1, 0);

		/// <summary>Defines a <see cref="T:System.Version" /> instance for HTTP 1.1.</summary>
		// Token: 0x040018E2 RID: 6370
		public static readonly Version Version11 = new Version(1, 1);

		// Token: 0x040018E3 RID: 6371
		public static readonly Version Version20 = new Version(2, 0);
	}
}
