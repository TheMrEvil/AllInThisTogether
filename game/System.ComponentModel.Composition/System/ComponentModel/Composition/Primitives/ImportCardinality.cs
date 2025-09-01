using System;

namespace System.ComponentModel.Composition.Primitives
{
	/// <summary>Indicates the cardinality of the <see cref="T:System.ComponentModel.Composition.Primitives.Export" /> objects required by an <see cref="T:System.ComponentModel.Composition.Primitives.ImportDefinition" />.</summary>
	// Token: 0x02000098 RID: 152
	public enum ImportCardinality
	{
		/// <summary>Zero or one <see cref="T:System.ComponentModel.Composition.Primitives.Export" /> objects are required by the <see cref="T:System.ComponentModel.Composition.Primitives.ImportDefinition" />.</summary>
		// Token: 0x0400018A RID: 394
		ZeroOrOne,
		/// <summary>Exactly one <see cref="T:System.ComponentModel.Composition.Primitives.Export" /> object is required by the <see cref="T:System.ComponentModel.Composition.Primitives.ImportDefinition" />.</summary>
		// Token: 0x0400018B RID: 395
		ExactlyOne,
		/// <summary>Zero or more <see cref="T:System.ComponentModel.Composition.Primitives.Export" /> objects are required by the <see cref="T:System.ComponentModel.Composition.Primitives.ImportDefinition" />.</summary>
		// Token: 0x0400018C RID: 396
		ZeroOrMore
	}
}
