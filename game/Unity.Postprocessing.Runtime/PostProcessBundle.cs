using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.Rendering.PostProcessing
{
	// Token: 0x02000053 RID: 83
	public sealed class PostProcessBundle
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000118 RID: 280 RVA: 0x0000B50E File Offset: 0x0000970E
		// (set) Token: 0x06000119 RID: 281 RVA: 0x0000B516 File Offset: 0x00009716
		public PostProcessAttribute attribute
		{
			[CompilerGenerated]
			get
			{
				return this.<attribute>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<attribute>k__BackingField = value;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600011A RID: 282 RVA: 0x0000B51F File Offset: 0x0000971F
		// (set) Token: 0x0600011B RID: 283 RVA: 0x0000B527 File Offset: 0x00009727
		public PostProcessEffectSettings settings
		{
			[CompilerGenerated]
			get
			{
				return this.<settings>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<settings>k__BackingField = value;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600011C RID: 284 RVA: 0x0000B530 File Offset: 0x00009730
		internal PostProcessEffectRenderer renderer
		{
			get
			{
				if (this.m_Renderer == null)
				{
					Type renderer = this.attribute.renderer;
					this.m_Renderer = (PostProcessEffectRenderer)Activator.CreateInstance(renderer);
					this.m_Renderer.SetSettings(this.settings);
					this.m_Renderer.Init();
				}
				return this.m_Renderer;
			}
		}

		// Token: 0x0600011D RID: 285 RVA: 0x0000B584 File Offset: 0x00009784
		internal PostProcessBundle(PostProcessEffectSettings settings)
		{
			this.settings = settings;
			this.attribute = settings.GetType().GetAttribute<PostProcessAttribute>();
		}

		// Token: 0x0600011E RID: 286 RVA: 0x0000B5A4 File Offset: 0x000097A4
		internal void Release()
		{
			if (this.m_Renderer != null)
			{
				this.m_Renderer.Release();
			}
			RuntimeUtilities.Destroy(this.settings);
		}

		// Token: 0x0600011F RID: 287 RVA: 0x0000B5C4 File Offset: 0x000097C4
		internal void ResetHistory()
		{
			if (this.m_Renderer != null)
			{
				this.m_Renderer.ResetHistory();
			}
		}

		// Token: 0x06000120 RID: 288 RVA: 0x0000B5D9 File Offset: 0x000097D9
		internal T CastSettings<T>() where T : PostProcessEffectSettings
		{
			return (T)((object)this.settings);
		}

		// Token: 0x06000121 RID: 289 RVA: 0x0000B5E6 File Offset: 0x000097E6
		internal T CastRenderer<T>() where T : PostProcessEffectRenderer
		{
			return (T)((object)this.renderer);
		}

		// Token: 0x0400016D RID: 365
		[CompilerGenerated]
		private PostProcessAttribute <attribute>k__BackingField;

		// Token: 0x0400016E RID: 366
		[CompilerGenerated]
		private PostProcessEffectSettings <settings>k__BackingField;

		// Token: 0x0400016F RID: 367
		private PostProcessEffectRenderer m_Renderer;
	}
}
