using System;
using System.Runtime.InteropServices;

namespace System.Xml.Schema
{
	// Token: 0x02000580 RID: 1408
	[StructLayout(LayoutKind.Explicit)]
	internal struct StateUnion
	{
		// Token: 0x04002A09 RID: 10761
		[FieldOffset(0)]
		public int State;

		// Token: 0x04002A0A RID: 10762
		[FieldOffset(0)]
		public int AllElementsRequired;

		// Token: 0x04002A0B RID: 10763
		[FieldOffset(0)]
		public int CurPosIndex;

		// Token: 0x04002A0C RID: 10764
		[FieldOffset(0)]
		public int NumberOfRunningPos;
	}
}
