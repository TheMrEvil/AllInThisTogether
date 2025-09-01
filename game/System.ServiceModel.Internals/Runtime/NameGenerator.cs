using System;
using System.Globalization;
using System.Threading;

namespace System.Runtime
{
	// Token: 0x02000025 RID: 37
	internal class NameGenerator
	{
		// Token: 0x06000115 RID: 277 RVA: 0x00005604 File Offset: 0x00003804
		private NameGenerator()
		{
			this.prefix = "_" + Guid.NewGuid().ToString().Replace('-', '_') + "_";
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00005648 File Offset: 0x00003848
		public static string Next()
		{
			long num = Interlocked.Increment(ref NameGenerator.nameGenerator.id);
			return NameGenerator.nameGenerator.prefix + num.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00005680 File Offset: 0x00003880
		// Note: this type is marked as 'beforefieldinit'.
		static NameGenerator()
		{
		}

		// Token: 0x040000C1 RID: 193
		private static NameGenerator nameGenerator = new NameGenerator();

		// Token: 0x040000C2 RID: 194
		private long id;

		// Token: 0x040000C3 RID: 195
		private string prefix;
	}
}
