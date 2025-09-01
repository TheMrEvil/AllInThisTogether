using System;

namespace System
{
	/// <summary>A customizable parser based on the File Transfer Protocol (FTP) scheme.</summary>
	// Token: 0x0200015F RID: 351
	public class FtpStyleUriParser : UriParser
	{
		/// <summary>Creates a customizable parser based on the File Transfer Protocol (FTP) scheme.</summary>
		// Token: 0x0600098C RID: 2444 RVA: 0x0002A2BF File Offset: 0x000284BF
		public FtpStyleUriParser() : base(UriParser.FtpUri.Flags)
		{
		}
	}
}
