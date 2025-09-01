using System;

namespace UnityEngine.Rendering.PostProcessing
{
	// Token: 0x0200000A RID: 10
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public sealed class PostProcessAttribute : Attribute
	{
		// Token: 0x0600000D RID: 13 RVA: 0x000021D3 File Offset: 0x000003D3
		public PostProcessAttribute(Type renderer, PostProcessEvent eventType, string menuItem, bool allowInSceneView = true)
		{
			this.renderer = renderer;
			this.eventType = eventType;
			this.menuItem = menuItem;
			this.allowInSceneView = allowInSceneView;
			this.builtinEffect = false;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000021FF File Offset: 0x000003FF
		internal PostProcessAttribute(Type renderer, string menuItem, bool allowInSceneView = true)
		{
			this.renderer = renderer;
			this.menuItem = menuItem;
			this.allowInSceneView = allowInSceneView;
			this.builtinEffect = true;
		}

		// Token: 0x0400001C RID: 28
		public readonly Type renderer;

		// Token: 0x0400001D RID: 29
		public readonly PostProcessEvent eventType;

		// Token: 0x0400001E RID: 30
		public readonly string menuItem;

		// Token: 0x0400001F RID: 31
		public readonly bool allowInSceneView;

		// Token: 0x04000020 RID: 32
		internal readonly bool builtinEffect;
	}
}
