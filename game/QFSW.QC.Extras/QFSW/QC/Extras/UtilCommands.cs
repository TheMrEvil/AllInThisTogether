using System;
using QFSW.QC.Pooling;

namespace QFSW.QC.Extras
{
	// Token: 0x02000012 RID: 18
	public static class UtilCommands
	{
		// Token: 0x06000050 RID: 80 RVA: 0x00003723 File Offset: 0x00001923
		// Note: this type is marked as 'beforefieldinit'.
		static UtilCommands()
		{
		}

		// Token: 0x04000013 RID: 19
		private static readonly ConcurrentStringBuilderPool _builderPool = new ConcurrentStringBuilderPool();
	}
}
