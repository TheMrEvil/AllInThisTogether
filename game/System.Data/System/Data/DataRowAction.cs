using System;

namespace System.Data
{
	/// <summary>Describes an action performed on a <see cref="T:System.Data.DataRow" />.</summary>
	// Token: 0x020000BF RID: 191
	[Flags]
	public enum DataRowAction
	{
		/// <summary>The row has not changed.</summary>
		// Token: 0x040007DB RID: 2011
		Nothing = 0,
		/// <summary>The row was deleted from the table.</summary>
		// Token: 0x040007DC RID: 2012
		Delete = 1,
		/// <summary>The row has changed.</summary>
		// Token: 0x040007DD RID: 2013
		Change = 2,
		/// <summary>The most recent change to the row has been rolled back.</summary>
		// Token: 0x040007DE RID: 2014
		Rollback = 4,
		/// <summary>The changes to the row have been committed.</summary>
		// Token: 0x040007DF RID: 2015
		Commit = 8,
		/// <summary>The row has been added to the table.</summary>
		// Token: 0x040007E0 RID: 2016
		Add = 16,
		/// <summary>The original version of the row has been changed.</summary>
		// Token: 0x040007E1 RID: 2017
		ChangeOriginal = 32,
		/// <summary>The original and the current versions of the row have been changed.</summary>
		// Token: 0x040007E2 RID: 2018
		ChangeCurrentAndOriginal = 64
	}
}
