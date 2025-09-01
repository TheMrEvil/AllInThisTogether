using System;

namespace System.Data.Common
{
	/// <summary>Specifies what types of Transact-SQL join statements are supported by the data source.</summary>
	// Token: 0x020003C0 RID: 960
	[Flags]
	public enum SupportedJoinOperators
	{
		/// <summary>The data source does not support join queries.</summary>
		// Token: 0x04001BE3 RID: 7139
		None = 0,
		/// <summary>The data source supports inner joins.</summary>
		// Token: 0x04001BE4 RID: 7140
		Inner = 1,
		/// <summary>The data source supports left outer joins.</summary>
		// Token: 0x04001BE5 RID: 7141
		LeftOuter = 2,
		/// <summary>The data source supports right outer joins.</summary>
		// Token: 0x04001BE6 RID: 7142
		RightOuter = 4,
		/// <summary>The data source supports full outer joins.</summary>
		// Token: 0x04001BE7 RID: 7143
		FullOuter = 8
	}
}
