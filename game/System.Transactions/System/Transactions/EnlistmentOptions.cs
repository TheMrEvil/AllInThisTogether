using System;

namespace System.Transactions
{
	/// <summary>Determines whether the object should be enlisted during the prepare phase.</summary>
	// Token: 0x02000011 RID: 17
	[Flags]
	public enum EnlistmentOptions
	{
		/// <summary>The object does not require enlistment during the initial phase of the commitment process.</summary>
		// Token: 0x04000035 RID: 53
		None = 0,
		/// <summary>The object must enlist during the initial phase of the commitment process.</summary>
		// Token: 0x04000036 RID: 54
		EnlistDuringPrepareRequired = 1
	}
}
