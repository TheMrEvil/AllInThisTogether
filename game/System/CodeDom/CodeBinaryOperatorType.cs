using System;

namespace System.CodeDom
{
	/// <summary>Defines identifiers for supported binary operators.</summary>
	// Token: 0x020002FA RID: 762
	public enum CodeBinaryOperatorType
	{
		/// <summary>Addition operator.</summary>
		// Token: 0x04000D5A RID: 3418
		Add,
		/// <summary>Subtraction operator.</summary>
		// Token: 0x04000D5B RID: 3419
		Subtract,
		/// <summary>Multiplication operator.</summary>
		// Token: 0x04000D5C RID: 3420
		Multiply,
		/// <summary>Division operator.</summary>
		// Token: 0x04000D5D RID: 3421
		Divide,
		/// <summary>Modulus operator.</summary>
		// Token: 0x04000D5E RID: 3422
		Modulus,
		/// <summary>Assignment operator.</summary>
		// Token: 0x04000D5F RID: 3423
		Assign,
		/// <summary>Identity not equal operator.</summary>
		// Token: 0x04000D60 RID: 3424
		IdentityInequality,
		/// <summary>Identity equal operator.</summary>
		// Token: 0x04000D61 RID: 3425
		IdentityEquality,
		/// <summary>Value equal operator.</summary>
		// Token: 0x04000D62 RID: 3426
		ValueEquality,
		/// <summary>Bitwise or operator.</summary>
		// Token: 0x04000D63 RID: 3427
		BitwiseOr,
		/// <summary>Bitwise and operator.</summary>
		// Token: 0x04000D64 RID: 3428
		BitwiseAnd,
		/// <summary>Boolean or operator. This represents a short circuiting operator. A short circuiting operator will evaluate only as many expressions as necessary before returning a correct value.</summary>
		// Token: 0x04000D65 RID: 3429
		BooleanOr,
		/// <summary>Boolean and operator. This represents a short circuiting operator. A short circuiting operator will evaluate only as many expressions as necessary before returning a correct value.</summary>
		// Token: 0x04000D66 RID: 3430
		BooleanAnd,
		/// <summary>Less than operator.</summary>
		// Token: 0x04000D67 RID: 3431
		LessThan,
		/// <summary>Less than or equal operator.</summary>
		// Token: 0x04000D68 RID: 3432
		LessThanOrEqual,
		/// <summary>Greater than operator.</summary>
		// Token: 0x04000D69 RID: 3433
		GreaterThan,
		/// <summary>Greater than or equal operator.</summary>
		// Token: 0x04000D6A RID: 3434
		GreaterThanOrEqual
	}
}
