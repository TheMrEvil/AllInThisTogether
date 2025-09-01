using System;

namespace Mono.Globalization.Unicode
{
	// Token: 0x0200006F RID: 111
	internal class Level2Map
	{
		// Token: 0x0600018C RID: 396 RVA: 0x00005D7D File Offset: 0x00003F7D
		public Level2Map(byte source, byte replace)
		{
			this.Source = source;
			this.Replace = replace;
		}

		// Token: 0x04000E3A RID: 3642
		public byte Source;

		// Token: 0x04000E3B RID: 3643
		public byte Replace;
	}
}
