using System;
using System.ComponentModel;
using System.Diagnostics;

namespace System.Runtime.CompilerServices
{
	/// <summary>Represents the runtime state of a dynamically generated method.</summary>
	// Token: 0x020002E2 RID: 738
	[EditorBrowsable(EditorBrowsableState.Never)]
	[DebuggerStepThrough]
	public sealed class Closure
	{
		/// <summary>Creates an object to hold state of a dynamically generated method.</summary>
		/// <param name="constants">The constant values that are used by the method.</param>
		/// <param name="locals">The hoisted local variables from the parent context.</param>
		// Token: 0x0600166D RID: 5741 RVA: 0x0004BD57 File Offset: 0x00049F57
		public Closure(object[] constants, object[] locals)
		{
			this.Constants = constants;
			this.Locals = locals;
		}

		/// <summary>Represents the non-trivial constants and locally executable expressions that are referenced by a dynamically generated method.</summary>
		// Token: 0x04000B54 RID: 2900
		public readonly object[] Constants;

		/// <summary>Represents the hoisted local variables from the parent context.</summary>
		// Token: 0x04000B55 RID: 2901
		public readonly object[] Locals;
	}
}
