using System;

namespace System
{
	/// <summary>Specifies options for a <see cref="T:System.UriParser" />.</summary>
	// Token: 0x0200014B RID: 331
	[Flags]
	public enum GenericUriParserOptions
	{
		/// <summary>The parser:
		///
		/// Requires an authority.
		/// Converts back slashes into forward slashes.
		/// Unescapes path dots, forward slashes, and back slashes.
		/// Removes trailing dots, empty segments, and dots-only segments.</summary>
		// Token: 0x04000581 RID: 1409
		Default = 0,
		/// <summary>The parser allows a registry-based authority.</summary>
		// Token: 0x04000582 RID: 1410
		GenericAuthority = 1,
		/// <summary>The parser allows a URI with no authority.</summary>
		// Token: 0x04000583 RID: 1411
		AllowEmptyAuthority = 2,
		/// <summary>The scheme does not define a user information part.</summary>
		// Token: 0x04000584 RID: 1412
		NoUserInfo = 4,
		/// <summary>The scheme does not define a port.</summary>
		// Token: 0x04000585 RID: 1413
		NoPort = 8,
		/// <summary>The scheme does not define a query part.</summary>
		// Token: 0x04000586 RID: 1414
		NoQuery = 16,
		/// <summary>The scheme does not define a fragment part.</summary>
		// Token: 0x04000587 RID: 1415
		NoFragment = 32,
		/// <summary>The parser does not convert back slashes into forward slashes.</summary>
		// Token: 0x04000588 RID: 1416
		DontConvertPathBackslashes = 64,
		/// <summary>The parser does not canonicalize the URI.</summary>
		// Token: 0x04000589 RID: 1417
		DontCompressPath = 128,
		/// <summary>The parser does not unescape path dots, forward slashes, or back slashes.</summary>
		// Token: 0x0400058A RID: 1418
		DontUnescapePathDotsAndSlashes = 256,
		/// <summary>The parser supports Internationalized Domain Name (IDN) parsing (IDN) of host names. Whether IDN is used is dictated by configuration values.</summary>
		// Token: 0x0400058B RID: 1419
		Idn = 512,
		/// <summary>The parser supports the parsing rules specified in RFC 3987 for International Resource Identifiers (IRI). Whether IRI is used is dictated by configuration values.</summary>
		// Token: 0x0400058C RID: 1420
		IriParsing = 1024
	}
}
