using System;

namespace IKVM.Reflection.Impl
{
	// Token: 0x0200007A RID: 122
	internal interface ISymbolWriterImpl
	{
		// Token: 0x06000693 RID: 1683
		byte[] GetDebugInfo(ref IMAGE_DEBUG_DIRECTORY idd);

		// Token: 0x06000694 RID: 1684
		void RemapToken(int oldToken, int newToken);

		// Token: 0x06000695 RID: 1685
		void DefineLocalVariable2(string name, FieldAttributes attributes, int signature, int addrKind, int addr1, int addr2, int addr3, int startOffset, int endOffset);

		// Token: 0x06000696 RID: 1686
		void OpenMethod(SymbolToken symbolToken, MethodBase mb);

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06000697 RID: 1687
		bool IsDeterministic { get; }
	}
}
