using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x0200050E RID: 1294
	internal class RestrictionFacets
	{
		// Token: 0x06003487 RID: 13447 RVA: 0x0000216B File Offset: 0x0000036B
		public RestrictionFacets()
		{
		}

		// Token: 0x0400271A RID: 10010
		internal int Length;

		// Token: 0x0400271B RID: 10011
		internal int MinLength;

		// Token: 0x0400271C RID: 10012
		internal int MaxLength;

		// Token: 0x0400271D RID: 10013
		internal ArrayList Patterns;

		// Token: 0x0400271E RID: 10014
		internal ArrayList Enumeration;

		// Token: 0x0400271F RID: 10015
		internal XmlSchemaWhiteSpace WhiteSpace;

		// Token: 0x04002720 RID: 10016
		internal object MaxInclusive;

		// Token: 0x04002721 RID: 10017
		internal object MaxExclusive;

		// Token: 0x04002722 RID: 10018
		internal object MinInclusive;

		// Token: 0x04002723 RID: 10019
		internal object MinExclusive;

		// Token: 0x04002724 RID: 10020
		internal int TotalDigits;

		// Token: 0x04002725 RID: 10021
		internal int FractionDigits;

		// Token: 0x04002726 RID: 10022
		internal RestrictionFlags Flags;

		// Token: 0x04002727 RID: 10023
		internal RestrictionFlags FixedFlags;
	}
}
