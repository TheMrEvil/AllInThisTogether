using System;
using System.Diagnostics;
using UnityEngine.Rendering;

namespace UnityEngine.Experimental.Rendering.RenderGraphModule
{
	// Token: 0x02000039 RID: 57
	[DebuggerDisplay("Texture ({handle.index})")]
	public struct TextureHandle
	{
		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000227 RID: 551 RVA: 0x0000BD2D File Offset: 0x00009F2D
		public static TextureHandle nullHandle
		{
			get
			{
				return TextureHandle.s_NullHandle;
			}
		}

		// Token: 0x06000228 RID: 552 RVA: 0x0000BD34 File Offset: 0x00009F34
		internal TextureHandle(int handle, bool shared = false)
		{
			this.handle = new ResourceHandle(handle, RenderGraphResourceType.Texture, shared);
			this.fallBackResource = TextureHandle.s_NullHandle.handle;
		}

		// Token: 0x06000229 RID: 553 RVA: 0x0000BD54 File Offset: 0x00009F54
		public static implicit operator RenderTargetIdentifier(TextureHandle texture)
		{
			if (!texture.IsValid())
			{
				return default(RenderTargetIdentifier);
			}
			return RenderGraphResourceRegistry.current.GetTexture(texture);
		}

		// Token: 0x0600022A RID: 554 RVA: 0x0000BD85 File Offset: 0x00009F85
		public static implicit operator Texture(TextureHandle texture)
		{
			return texture.IsValid() ? RenderGraphResourceRegistry.current.GetTexture(texture) : null;
		}

		// Token: 0x0600022B RID: 555 RVA: 0x0000BDA4 File Offset: 0x00009FA4
		public static implicit operator RenderTexture(TextureHandle texture)
		{
			return texture.IsValid() ? RenderGraphResourceRegistry.current.GetTexture(texture) : null;
		}

		// Token: 0x0600022C RID: 556 RVA: 0x0000BDC3 File Offset: 0x00009FC3
		public static implicit operator RTHandle(TextureHandle texture)
		{
			if (!texture.IsValid())
			{
				return null;
			}
			return RenderGraphResourceRegistry.current.GetTexture(texture);
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0000BDDC File Offset: 0x00009FDC
		public bool IsValid()
		{
			return this.handle.IsValid();
		}

		// Token: 0x0600022E RID: 558 RVA: 0x0000BDE9 File Offset: 0x00009FE9
		public void SetFallBackResource(TextureHandle texture)
		{
			this.fallBackResource = texture.handle;
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0000BDF7 File Offset: 0x00009FF7
		// Note: this type is marked as 'beforefieldinit'.
		static TextureHandle()
		{
		}

		// Token: 0x0400015E RID: 350
		private static TextureHandle s_NullHandle;

		// Token: 0x0400015F RID: 351
		internal ResourceHandle handle;

		// Token: 0x04000160 RID: 352
		internal ResourceHandle fallBackResource;
	}
}
