using System;

namespace System.Runtime.CompilerServices
{
	/// <summary>Represents the values of run-time variables.</summary>
	// Token: 0x020002E4 RID: 740
	public interface IRuntimeVariables
	{
		/// <summary>Gets a count of the run-time variables.</summary>
		/// <returns>The number of run-time variables.</returns>
		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06001673 RID: 5747
		int Count { get; }

		/// <summary>Gets the value of the run-time variable at the specified index.</summary>
		/// <param name="index">The zero-based index of the run-time variable whose value is to be returned.</param>
		/// <returns>The value of the run-time variable.</returns>
		// Token: 0x170003D8 RID: 984
		object this[int index]
		{
			get;
			set;
		}
	}
}
