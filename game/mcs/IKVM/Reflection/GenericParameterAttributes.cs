using System;

namespace IKVM.Reflection
{
	// Token: 0x02000017 RID: 23
	[Flags]
	public enum GenericParameterAttributes
	{
		// Token: 0x04000072 RID: 114
		None = 0,
		// Token: 0x04000073 RID: 115
		Covariant = 1,
		// Token: 0x04000074 RID: 116
		Contravariant = 2,
		// Token: 0x04000075 RID: 117
		VarianceMask = 3,
		// Token: 0x04000076 RID: 118
		ReferenceTypeConstraint = 4,
		// Token: 0x04000077 RID: 119
		NotNullableValueTypeConstraint = 8,
		// Token: 0x04000078 RID: 120
		DefaultConstructorConstraint = 16,
		// Token: 0x04000079 RID: 121
		SpecialConstraintMask = 28
	}
}
