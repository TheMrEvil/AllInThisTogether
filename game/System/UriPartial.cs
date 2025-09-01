using System;

namespace System
{
	/// <summary>Defines the parts of a URI for the <see cref="M:System.Uri.GetLeftPart(System.UriPartial)" /> method.</summary>
	// Token: 0x02000155 RID: 341
	public enum UriPartial
	{
		/// <summary>The scheme segment of the URI.</summary>
		// Token: 0x04000609 RID: 1545
		Scheme,
		/// <summary>The scheme and authority segments of the URI.</summary>
		// Token: 0x0400060A RID: 1546
		Authority,
		/// <summary>The scheme, authority, and path segments of the URI.</summary>
		// Token: 0x0400060B RID: 1547
		Path,
		/// <summary>The scheme, authority, path, and query segments of the URI.</summary>
		// Token: 0x0400060C RID: 1548
		Query
	}
}
