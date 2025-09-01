using System;

namespace System.Dynamic
{
	/// <summary>Represents information about a dynamic get member operation that indicates if the get member should invoke properties when they perform the get operation.</summary>
	// Token: 0x0200031C RID: 796
	public interface IInvokeOnGetBinder
	{
		/// <summary>Gets the value indicating if this get member operation should invoke properties when they perform the get operation. The default value when this interface is not present is true.</summary>
		/// <returns>True if this get member operation should invoke properties when they perform the get operation; otherwise false.</returns>
		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x060017F8 RID: 6136
		bool InvokeOnGet { get; }
	}
}
