using System;

namespace System.Runtime.CompilerServices
{
	/// <summary>Specifies the preferred default binding for a dependent assembly.</summary>
	// Token: 0x02000829 RID: 2089
	[Serializable]
	public enum LoadHint
	{
		/// <summary>No preference specified.</summary>
		// Token: 0x04002D73 RID: 11635
		Default,
		/// <summary>The dependency is always loaded.</summary>
		// Token: 0x04002D74 RID: 11636
		Always,
		/// <summary>The dependency is sometimes loaded.</summary>
		// Token: 0x04002D75 RID: 11637
		Sometimes
	}
}
