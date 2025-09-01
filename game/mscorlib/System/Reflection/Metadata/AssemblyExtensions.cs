using System;

namespace System.Reflection.Metadata
{
	// Token: 0x02000906 RID: 2310
	public static class AssemblyExtensions
	{
		// Token: 0x06004E4E RID: 20046 RVA: 0x000479F8 File Offset: 0x00045BF8
		[CLSCompliant(false)]
		public unsafe static bool TryGetRawMetadata(this Assembly assembly, out byte* blob, out int length)
		{
			throw new NotImplementedException();
		}
	}
}
