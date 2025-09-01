using System;

namespace System.Runtime.CompilerServices
{
	/// <summary>Defines a property for accessing the value that an object references.</summary>
	// Token: 0x020002EC RID: 748
	public interface IStrongBox
	{
		/// <summary>Gets or sets the value that an object references.</summary>
		/// <returns>The value that the object references.</returns>
		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x060016B3 RID: 5811
		// (set) Token: 0x060016B4 RID: 5812
		object Value { get; set; }
	}
}
