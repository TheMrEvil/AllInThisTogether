using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.Rendering.PostProcessing
{
	// Token: 0x02000059 RID: 89
	public abstract class PostProcessEffectRenderer<T> : PostProcessEffectRenderer where T : PostProcessEffectSettings
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000144 RID: 324 RVA: 0x0000BF75 File Offset: 0x0000A175
		// (set) Token: 0x06000145 RID: 325 RVA: 0x0000BF7D File Offset: 0x0000A17D
		public T settings
		{
			[CompilerGenerated]
			get
			{
				return this.<settings>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<settings>k__BackingField = value;
			}
		}

		// Token: 0x06000146 RID: 326 RVA: 0x0000BF86 File Offset: 0x0000A186
		internal override void SetSettings(PostProcessEffectSettings settings)
		{
			this.settings = (T)((object)settings);
		}

		// Token: 0x06000147 RID: 327 RVA: 0x0000BF94 File Offset: 0x0000A194
		protected PostProcessEffectRenderer()
		{
		}

		// Token: 0x04000195 RID: 405
		[CompilerGenerated]
		private T <settings>k__BackingField;
	}
}
