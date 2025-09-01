using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020000B7 RID: 183
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public class VolumeComponentMenu : Attribute
	{
		// Token: 0x0600061D RID: 1565 RVA: 0x0001C8DC File Offset: 0x0001AADC
		public VolumeComponentMenu(string menu)
		{
			this.menu = menu;
		}

		// Token: 0x04000399 RID: 921
		public readonly string menu;
	}
}
