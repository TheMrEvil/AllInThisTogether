using System;

namespace System
{
	/// <summary>A customizable parser based on the news scheme using the Network News Transfer Protocol (NNTP).</summary>
	// Token: 0x02000161 RID: 353
	public class NewsStyleUriParser : UriParser
	{
		/// <summary>Create a customizable parser based on the news scheme using the Network News Transfer Protocol (NNTP).</summary>
		// Token: 0x0600098E RID: 2446 RVA: 0x0002A2E3 File Offset: 0x000284E3
		public NewsStyleUriParser() : base(UriParser.NewsUri.Flags)
		{
		}
	}
}
