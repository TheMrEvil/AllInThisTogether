using System;
using UnityEngine.UIElements.UIR;

namespace UnityEngine.UIElements
{
	// Token: 0x02000007 RID: 7
	internal abstract class AtlasBase
	{
		// Token: 0x0600000E RID: 14 RVA: 0x00002140 File Offset: 0x00000340
		public virtual bool TryGetAtlas(VisualElement ctx, Texture2D src, out TextureId atlas, out RectInt atlasRect)
		{
			atlas = TextureId.invalid;
			atlasRect = default(RectInt);
			return false;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002166 File Offset: 0x00000366
		public virtual void ReturnAtlas(VisualElement ctx, Texture2D src, TextureId atlas)
		{
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002166 File Offset: 0x00000366
		public virtual void Reset()
		{
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002166 File Offset: 0x00000366
		protected virtual void OnAssignedToPanel(IPanel panel)
		{
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002166 File Offset: 0x00000366
		protected virtual void OnRemovedFromPanel(IPanel panel)
		{
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002166 File Offset: 0x00000366
		protected virtual void OnUpdateDynamicTextures(IPanel panel)
		{
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002169 File Offset: 0x00000369
		internal void InvokeAssignedToPanel(IPanel panel)
		{
			this.OnAssignedToPanel(panel);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002174 File Offset: 0x00000374
		internal void InvokeRemovedFromPanel(IPanel panel)
		{
			this.OnRemovedFromPanel(panel);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000217F File Offset: 0x0000037F
		internal void InvokeUpdateDynamicTextures(IPanel panel)
		{
			this.OnUpdateDynamicTextures(panel);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000218C File Offset: 0x0000038C
		protected static void RepaintTexturedElements(IPanel panel)
		{
			Panel panel2 = panel as Panel;
			UIRRepaintUpdater uirrepaintUpdater = ((panel2 != null) ? panel2.GetUpdater(VisualTreeUpdatePhase.Repaint) : null) as UIRRepaintUpdater;
			if (uirrepaintUpdater != null)
			{
				RenderChain renderChain = uirrepaintUpdater.renderChain;
				if (renderChain != null)
				{
					renderChain.RepaintTexturedElements();
				}
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000021CC File Offset: 0x000003CC
		protected TextureId AllocateDynamicTexture()
		{
			return this.textureRegistry.AllocAndAcquireDynamic();
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000021E9 File Offset: 0x000003E9
		protected void FreeDynamicTexture(TextureId id)
		{
			this.textureRegistry.Release(id);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000021F9 File Offset: 0x000003F9
		protected void SetDynamicTexture(TextureId id, Texture texture)
		{
			this.textureRegistry.UpdateDynamic(id, texture);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000220A File Offset: 0x0000040A
		protected AtlasBase()
		{
		}

		// Token: 0x04000003 RID: 3
		internal TextureRegistry textureRegistry = TextureRegistry.instance;
	}
}
