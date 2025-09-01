using System;

namespace System.Reflection.Emit
{
	/// <summary>Describes how values are pushed onto a stack or popped off a stack.</summary>
	// Token: 0x0200090A RID: 2314
	public enum StackBehaviour
	{
		/// <summary>No values are popped off the stack.</summary>
		// Token: 0x040030A8 RID: 12456
		Pop0,
		/// <summary>Pops one value off the stack.</summary>
		// Token: 0x040030A9 RID: 12457
		Pop1,
		/// <summary>Pops 1 value off the stack for the first operand, and 1 value of the stack for the second operand.</summary>
		// Token: 0x040030AA RID: 12458
		Pop1_pop1,
		/// <summary>Pops a 32-bit integer off the stack.</summary>
		// Token: 0x040030AB RID: 12459
		Popi,
		/// <summary>Pops a 32-bit integer off the stack for the first operand, and a value off the stack for the second operand.</summary>
		// Token: 0x040030AC RID: 12460
		Popi_pop1,
		/// <summary>Pops a 32-bit integer off the stack for the first operand, and a 32-bit integer off the stack for the second operand.</summary>
		// Token: 0x040030AD RID: 12461
		Popi_popi,
		/// <summary>Pops a 32-bit integer off the stack for the first operand, and a 64-bit integer off the stack for the second operand.</summary>
		// Token: 0x040030AE RID: 12462
		Popi_popi8,
		/// <summary>Pops a 32-bit integer off the stack for the first operand, a 32-bit integer off the stack for the second operand, and a 32-bit integer off the stack for the third operand.</summary>
		// Token: 0x040030AF RID: 12463
		Popi_popi_popi,
		/// <summary>Pops a 32-bit integer off the stack for the first operand, and a 32-bit floating point number off the stack for the second operand.</summary>
		// Token: 0x040030B0 RID: 12464
		Popi_popr4,
		/// <summary>Pops a 32-bit integer off the stack for the first operand, and a 64-bit floating point number off the stack for the second operand.</summary>
		// Token: 0x040030B1 RID: 12465
		Popi_popr8,
		/// <summary>Pops a reference off the stack.</summary>
		// Token: 0x040030B2 RID: 12466
		Popref,
		/// <summary>Pops a reference off the stack for the first operand, and a value off the stack for the second operand.</summary>
		// Token: 0x040030B3 RID: 12467
		Popref_pop1,
		/// <summary>Pops a reference off the stack for the first operand, and a 32-bit integer off the stack for the second operand.</summary>
		// Token: 0x040030B4 RID: 12468
		Popref_popi,
		/// <summary>Pops a reference off the stack for the first operand, a value off the stack for the second operand, and a value off the stack for the third operand.</summary>
		// Token: 0x040030B5 RID: 12469
		Popref_popi_popi,
		/// <summary>Pops a reference off the stack for the first operand, a value off the stack for the second operand, and a 64-bit integer off the stack for the third operand.</summary>
		// Token: 0x040030B6 RID: 12470
		Popref_popi_popi8,
		/// <summary>Pops a reference off the stack for the first operand, a value off the stack for the second operand, and a 32-bit integer off the stack for the third operand.</summary>
		// Token: 0x040030B7 RID: 12471
		Popref_popi_popr4,
		/// <summary>Pops a reference off the stack for the first operand, a value off the stack for the second operand, and a 64-bit floating point number off the stack for the third operand.</summary>
		// Token: 0x040030B8 RID: 12472
		Popref_popi_popr8,
		/// <summary>Pops a reference off the stack for the first operand, a value off the stack for the second operand, and a reference off the stack for the third operand.</summary>
		// Token: 0x040030B9 RID: 12473
		Popref_popi_popref,
		/// <summary>No values are pushed onto the stack.</summary>
		// Token: 0x040030BA RID: 12474
		Push0,
		/// <summary>Pushes one value onto the stack.</summary>
		// Token: 0x040030BB RID: 12475
		Push1,
		/// <summary>Pushes 1 value onto the stack for the first operand, and 1 value onto the stack for the second operand.</summary>
		// Token: 0x040030BC RID: 12476
		Push1_push1,
		/// <summary>Pushes a 32-bit integer onto the stack.</summary>
		// Token: 0x040030BD RID: 12477
		Pushi,
		/// <summary>Pushes a 64-bit integer onto the stack.</summary>
		// Token: 0x040030BE RID: 12478
		Pushi8,
		/// <summary>Pushes a 32-bit floating point number onto the stack.</summary>
		// Token: 0x040030BF RID: 12479
		Pushr4,
		/// <summary>Pushes a 64-bit floating point number onto the stack.</summary>
		// Token: 0x040030C0 RID: 12480
		Pushr8,
		/// <summary>Pushes a reference onto the stack.</summary>
		// Token: 0x040030C1 RID: 12481
		Pushref,
		/// <summary>Pops a variable off the stack.</summary>
		// Token: 0x040030C2 RID: 12482
		Varpop,
		/// <summary>Pushes a variable onto the stack.</summary>
		// Token: 0x040030C3 RID: 12483
		Varpush,
		/// <summary>Pops a reference off the stack for the first operand, a value off the stack for the second operand, and a 32-bit integer off the stack for the third operand.</summary>
		// Token: 0x040030C4 RID: 12484
		Popref_popi_pop1
	}
}
