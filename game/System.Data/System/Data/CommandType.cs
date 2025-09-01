using System;

namespace System.Data
{
	/// <summary>Specifies how a command string is interpreted.</summary>
	// Token: 0x020000A5 RID: 165
	public enum CommandType
	{
		/// <summary>An SQL text command. (Default.)</summary>
		// Token: 0x0400076A RID: 1898
		Text = 1,
		/// <summary>The name of a stored procedure.</summary>
		// Token: 0x0400076B RID: 1899
		StoredProcedure = 4,
		/// <summary>The name of a table.</summary>
		// Token: 0x0400076C RID: 1900
		TableDirect = 512
	}
}
