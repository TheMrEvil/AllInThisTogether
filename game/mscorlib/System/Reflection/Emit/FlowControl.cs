using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	/// <summary>Describes how an instruction alters the flow of control.</summary>
	// Token: 0x02000927 RID: 2343
	[ComVisible(true)]
	[Serializable]
	public enum FlowControl
	{
		/// <summary>Branch instruction.</summary>
		// Token: 0x0400316E RID: 12654
		Branch,
		/// <summary>Break instruction.</summary>
		// Token: 0x0400316F RID: 12655
		Break,
		/// <summary>Call instruction.</summary>
		// Token: 0x04003170 RID: 12656
		Call,
		/// <summary>Conditional branch instruction.</summary>
		// Token: 0x04003171 RID: 12657
		Cond_Branch,
		/// <summary>Provides information about a subsequent instruction. For example, the <see langword="Unaligned" /> instruction of <see langword="Reflection.Emit.Opcodes" /> has <see langword="FlowControl.Meta" /> and specifies that the subsequent pointer instruction might be unaligned.</summary>
		// Token: 0x04003172 RID: 12658
		Meta,
		/// <summary>Normal flow of control.</summary>
		// Token: 0x04003173 RID: 12659
		Next,
		/// <summary>This enumerator value is reserved and should not be used.</summary>
		// Token: 0x04003174 RID: 12660
		[Obsolete("This API has been deprecated.")]
		Phi,
		/// <summary>Return instruction.</summary>
		// Token: 0x04003175 RID: 12661
		Return,
		/// <summary>Exception throw instruction.</summary>
		// Token: 0x04003176 RID: 12662
		Throw
	}
}
