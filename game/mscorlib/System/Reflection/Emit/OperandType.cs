using System;

namespace System.Reflection.Emit
{
	/// <summary>Describes the operand type of Microsoft intermediate language (MSIL) instruction.</summary>
	// Token: 0x02000908 RID: 2312
	public enum OperandType
	{
		/// <summary>The operand is a 32-bit integer branch target.</summary>
		// Token: 0x0400308B RID: 12427
		InlineBrTarget,
		/// <summary>The operand is a 32-bit metadata token.</summary>
		// Token: 0x0400308C RID: 12428
		InlineField,
		/// <summary>The operand is a 32-bit integer.</summary>
		// Token: 0x0400308D RID: 12429
		InlineI,
		/// <summary>The operand is a 64-bit integer.</summary>
		// Token: 0x0400308E RID: 12430
		InlineI8,
		/// <summary>The operand is a 32-bit metadata token.</summary>
		// Token: 0x0400308F RID: 12431
		InlineMethod,
		/// <summary>No operand.</summary>
		// Token: 0x04003090 RID: 12432
		InlineNone,
		/// <summary>The operand is reserved and should not be used.</summary>
		// Token: 0x04003091 RID: 12433
		[Obsolete("This API has been deprecated. http://go.microsoft.com/fwlink/?linkid=14202")]
		InlinePhi,
		/// <summary>The operand is a 64-bit IEEE floating point number.</summary>
		// Token: 0x04003092 RID: 12434
		InlineR,
		/// <summary>The operand is a 32-bit metadata signature token.</summary>
		// Token: 0x04003093 RID: 12435
		InlineSig = 9,
		/// <summary>The operand is a 32-bit metadata string token.</summary>
		// Token: 0x04003094 RID: 12436
		InlineString,
		/// <summary>The operand is the 32-bit integer argument to a switch instruction.</summary>
		// Token: 0x04003095 RID: 12437
		InlineSwitch,
		/// <summary>The operand is a <see langword="FieldRef" />, <see langword="MethodRef" />, or <see langword="TypeRef" /> token.</summary>
		// Token: 0x04003096 RID: 12438
		InlineTok,
		/// <summary>The operand is a 32-bit metadata token.</summary>
		// Token: 0x04003097 RID: 12439
		InlineType,
		/// <summary>The operand is 16-bit integer containing the ordinal of a local variable or an argument.</summary>
		// Token: 0x04003098 RID: 12440
		InlineVar,
		/// <summary>The operand is an 8-bit integer branch target.</summary>
		// Token: 0x04003099 RID: 12441
		ShortInlineBrTarget,
		/// <summary>The operand is an 8-bit integer.</summary>
		// Token: 0x0400309A RID: 12442
		ShortInlineI,
		/// <summary>The operand is a 32-bit IEEE floating point number.</summary>
		// Token: 0x0400309B RID: 12443
		ShortInlineR,
		/// <summary>The operand is an 8-bit integer containing the ordinal of a local variable or an argumenta.</summary>
		// Token: 0x0400309C RID: 12444
		ShortInlineVar
	}
}
