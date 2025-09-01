using System;

namespace System.Data
{
	/// <summary>Specifies the type of a parameter within a query relative to the <see cref="T:System.Data.DataSet" />.</summary>
	// Token: 0x02000113 RID: 275
	public enum ParameterDirection
	{
		/// <summary>The parameter is an input parameter.</summary>
		// Token: 0x04000992 RID: 2450
		Input = 1,
		/// <summary>The parameter is an output parameter.</summary>
		// Token: 0x04000993 RID: 2451
		Output,
		/// <summary>The parameter is capable of both input and output.</summary>
		// Token: 0x04000994 RID: 2452
		InputOutput,
		/// <summary>The parameter represents a return value from an operation such as a stored procedure, built-in function, or user-defined function.</summary>
		// Token: 0x04000995 RID: 2453
		ReturnValue = 6
	}
}
