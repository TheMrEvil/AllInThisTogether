using System;

namespace UnityEngine.Rendering.PostProcessing
{
	// Token: 0x02000058 RID: 88
	public abstract class PostProcessEffectRenderer
	{
		// Token: 0x0600013D RID: 317 RVA: 0x0000BF50 File Offset: 0x0000A150
		public virtual void Init()
		{
		}

		// Token: 0x0600013E RID: 318 RVA: 0x0000BF52 File Offset: 0x0000A152
		public virtual DepthTextureMode GetCameraFlags()
		{
			return DepthTextureMode.None;
		}

		// Token: 0x0600013F RID: 319 RVA: 0x0000BF55 File Offset: 0x0000A155
		public virtual void ResetHistory()
		{
			this.m_ResetHistory = true;
		}

		// Token: 0x06000140 RID: 320 RVA: 0x0000BF5E File Offset: 0x0000A15E
		public virtual void Release()
		{
			this.ResetHistory();
		}

		// Token: 0x06000141 RID: 321
		public abstract void Render(PostProcessRenderContext context);

		// Token: 0x06000142 RID: 322
		internal abstract void SetSettings(PostProcessEffectSettings settings);

		// Token: 0x06000143 RID: 323 RVA: 0x0000BF66 File Offset: 0x0000A166
		protected PostProcessEffectRenderer()
		{
		}

		// Token: 0x04000194 RID: 404
		protected bool m_ResetHistory = true;
	}
}
