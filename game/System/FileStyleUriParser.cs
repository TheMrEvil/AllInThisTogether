using System;

namespace System
{
	/// <summary>A customizable parser based on the File scheme.</summary>
	// Token: 0x02000160 RID: 352
	public class FileStyleUriParser : UriParser
	{
		/// <summary>Creates a customizable parser based on the File scheme.</summary>
		// Token: 0x0600098D RID: 2445 RVA: 0x0002A2D1 File Offset: 0x000284D1
		public FileStyleUriParser() : base(UriParser.FileUri.Flags)
		{
		}
	}
}
