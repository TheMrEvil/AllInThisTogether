using System;

namespace UnityEngine
{
	// Token: 0x02000035 RID: 53
	internal sealed class GUIAspectSizer : GUILayoutEntry
	{
		// Token: 0x060003ED RID: 1005 RVA: 0x0000D000 File Offset: 0x0000B200
		public GUIAspectSizer(float aspect, GUILayoutOption[] options) : base(0f, 0f, 0f, 0f, GUIStyle.none)
		{
			this.aspect = aspect;
			this.ApplyOptions(options);
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x0000D034 File Offset: 0x0000B234
		public override void CalcHeight()
		{
			this.minHeight = (this.maxHeight = this.rect.width / this.aspect);
		}

		// Token: 0x040000FA RID: 250
		private float aspect;
	}
}
