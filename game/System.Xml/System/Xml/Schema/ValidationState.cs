using System;
using System.Collections.Generic;

namespace System.Xml.Schema
{
	// Token: 0x02000581 RID: 1409
	internal sealed class ValidationState
	{
		// Token: 0x060038B0 RID: 14512 RVA: 0x001472F0 File Offset: 0x001454F0
		public ValidationState()
		{
		}

		// Token: 0x04002A0D RID: 10765
		public bool IsNill;

		// Token: 0x04002A0E RID: 10766
		public bool IsDefault;

		// Token: 0x04002A0F RID: 10767
		public bool NeedValidateChildren;

		// Token: 0x04002A10 RID: 10768
		public bool CheckRequiredAttribute;

		// Token: 0x04002A11 RID: 10769
		public bool ValidationSkipped;

		// Token: 0x04002A12 RID: 10770
		public int Depth;

		// Token: 0x04002A13 RID: 10771
		public XmlSchemaContentProcessing ProcessContents;

		// Token: 0x04002A14 RID: 10772
		public XmlSchemaValidity Validity;

		// Token: 0x04002A15 RID: 10773
		public SchemaElementDecl ElementDecl;

		// Token: 0x04002A16 RID: 10774
		public SchemaElementDecl ElementDeclBeforeXsi;

		// Token: 0x04002A17 RID: 10775
		public string LocalName;

		// Token: 0x04002A18 RID: 10776
		public string Namespace;

		// Token: 0x04002A19 RID: 10777
		public ConstraintStruct[] Constr;

		// Token: 0x04002A1A RID: 10778
		public StateUnion CurrentState;

		// Token: 0x04002A1B RID: 10779
		public bool HasMatched;

		// Token: 0x04002A1C RID: 10780
		public BitSet[] CurPos = new BitSet[2];

		// Token: 0x04002A1D RID: 10781
		public BitSet AllElementsSet;

		// Token: 0x04002A1E RID: 10782
		public List<RangePositionInfo> RunningPositions;

		// Token: 0x04002A1F RID: 10783
		public bool TooComplex;
	}
}
