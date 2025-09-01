using System;

namespace System
{
	/// <summary>A customizable parser based on the HTTP scheme.</summary>
	// Token: 0x0200015E RID: 350
	public class HttpStyleUriParser : UriParser
	{
		/// <summary>Create a customizable parser based on the HTTP scheme.</summary>
		// Token: 0x0600098B RID: 2443 RVA: 0x0002A2AD File Offset: 0x000284AD
		public HttpStyleUriParser() : base(UriParser.HttpUri.Flags)
		{
		}
	}
}
