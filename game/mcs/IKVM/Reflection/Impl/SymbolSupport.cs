using System;
using IKVM.Reflection.Emit;

namespace IKVM.Reflection.Impl
{
	// Token: 0x0200007B RID: 123
	internal static class SymbolSupport
	{
		// Token: 0x06000698 RID: 1688 RVA: 0x00013CBB File Offset: 0x00011EBB
		internal static ISymbolWriterImpl CreateSymbolWriterFor(ModuleBuilder moduleBuilder)
		{
			throw new NotSupportedException("IKVM.Reflection compiled with NO_SYMBOL_WRITER does not support writing debugging symbols.");
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x00013CC7 File Offset: 0x00011EC7
		internal static byte[] GetDebugInfo(ISymbolWriterImpl writer, ref IMAGE_DEBUG_DIRECTORY idd)
		{
			return writer.GetDebugInfo(ref idd);
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x00013CD0 File Offset: 0x00011ED0
		internal static void RemapToken(ISymbolWriterImpl writer, int oldToken, int newToken)
		{
			writer.RemapToken(oldToken, newToken);
		}
	}
}
