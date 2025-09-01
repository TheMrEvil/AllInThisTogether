using System;

namespace MagicaCloth2
{
	// Token: 0x0200005C RID: 92
	[Serializable]
	public class GizmoSerializeData
	{
		// Token: 0x06000123 RID: 291 RVA: 0x0000D7B1 File Offset: 0x0000B9B1
		public GizmoSerializeData()
		{
			this.clothDebugSettings.enable = true;
			this.clothDebugSettings.shape = true;
		}

		// Token: 0x06000124 RID: 292 RVA: 0x0000D7DC File Offset: 0x0000B9DC
		public bool IsAlways()
		{
			return this.always;
		}

		// Token: 0x04000226 RID: 550
		public bool always;

		// Token: 0x04000227 RID: 551
		public ClothDebugSettings clothDebugSettings = new ClothDebugSettings();
	}
}
