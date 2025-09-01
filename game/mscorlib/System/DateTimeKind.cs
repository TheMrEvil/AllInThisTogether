using System;

namespace System
{
	/// <summary>Specifies whether a <see cref="T:System.DateTime" /> object represents a local time, a Coordinated Universal Time (UTC), or is not specified as either local time or UTC.</summary>
	// Token: 0x0200010D RID: 269
	public enum DateTimeKind
	{
		/// <summary>The time represented is not specified as either local time or Coordinated Universal Time (UTC).</summary>
		// Token: 0x040010C3 RID: 4291
		Unspecified,
		/// <summary>The time represented is UTC.</summary>
		// Token: 0x040010C4 RID: 4292
		Utc,
		/// <summary>The time represented is local time.</summary>
		// Token: 0x040010C5 RID: 4293
		Local
	}
}
