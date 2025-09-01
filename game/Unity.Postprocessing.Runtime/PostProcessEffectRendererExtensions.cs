using System;

namespace UnityEngine.Rendering.PostProcessing
{
	// Token: 0x02000068 RID: 104
	internal static class PostProcessEffectRendererExtensions
	{
		// Token: 0x0600020D RID: 525 RVA: 0x00010BF4 File Offset: 0x0000EDF4
		public static Exception RenderOrLog(this PostProcessEffectRenderer self, PostProcessRenderContext context)
		{
			try
			{
				self.Render(context);
			}
			catch (Exception ex)
			{
				Debug.LogException(ex);
				return ex;
			}
			return null;
		}
	}
}
