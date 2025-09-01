using System;

namespace System
{
	/// <summary>A customizable parser based on the Gopher scheme.</summary>
	// Token: 0x02000162 RID: 354
	public class GopherStyleUriParser : UriParser
	{
		/// <summary>Creates a customizable parser based on the Gopher scheme.</summary>
		// Token: 0x0600098F RID: 2447 RVA: 0x0002A2F5 File Offset: 0x000284F5
		public GopherStyleUriParser() : base(UriParser.GopherUri.Flags)
		{
		}
	}
}
