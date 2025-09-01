using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.UIElements.UIR;

namespace UnityEngine.UIElements
{
	// Token: 0x0200025A RID: 602
	internal class DynamicAtlasPage : IDisposable
	{
		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06001256 RID: 4694 RVA: 0x00048683 File Offset: 0x00046883
		// (set) Token: 0x06001257 RID: 4695 RVA: 0x0004868B File Offset: 0x0004688B
		public TextureId textureId
		{
			[CompilerGenerated]
			get
			{
				return this.<textureId>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<textureId>k__BackingField = value;
			}
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x06001258 RID: 4696 RVA: 0x00048694 File Offset: 0x00046894
		// (set) Token: 0x06001259 RID: 4697 RVA: 0x0004869C File Offset: 0x0004689C
		public RenderTexture atlas
		{
			[CompilerGenerated]
			get
			{
				return this.<atlas>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<atlas>k__BackingField = value;
			}
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x0600125A RID: 4698 RVA: 0x000486A5 File Offset: 0x000468A5
		public RenderTextureFormat format
		{
			[CompilerGenerated]
			get
			{
				return this.<format>k__BackingField;
			}
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x0600125B RID: 4699 RVA: 0x000486AD File Offset: 0x000468AD
		public FilterMode filterMode
		{
			[CompilerGenerated]
			get
			{
				return this.<filterMode>k__BackingField;
			}
		}

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x0600125C RID: 4700 RVA: 0x000486B5 File Offset: 0x000468B5
		public Vector2Int minSize
		{
			[CompilerGenerated]
			get
			{
				return this.<minSize>k__BackingField;
			}
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x0600125D RID: 4701 RVA: 0x000486BD File Offset: 0x000468BD
		public Vector2Int maxSize
		{
			[CompilerGenerated]
			get
			{
				return this.<maxSize>k__BackingField;
			}
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x0600125E RID: 4702 RVA: 0x000486C5 File Offset: 0x000468C5
		public Vector2Int currentSize
		{
			get
			{
				return this.m_CurrentSize;
			}
		}

		// Token: 0x0600125F RID: 4703 RVA: 0x000486D0 File Offset: 0x000468D0
		public DynamicAtlasPage(RenderTextureFormat format, FilterMode filterMode, Vector2Int minSize, Vector2Int maxSize)
		{
			this.textureId = TextureRegistry.instance.AllocAndAcquireDynamic();
			this.format = format;
			this.filterMode = filterMode;
			this.minSize = minSize;
			this.maxSize = maxSize;
			this.m_Allocator = new Allocator2D(minSize, maxSize, this.m_2Padding);
			this.m_Blitter = new TextureBlitter(64);
		}

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x06001260 RID: 4704 RVA: 0x00048742 File Offset: 0x00046942
		// (set) Token: 0x06001261 RID: 4705 RVA: 0x0004874A File Offset: 0x0004694A
		private protected bool disposed
		{
			[CompilerGenerated]
			protected get
			{
				return this.<disposed>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<disposed>k__BackingField = value;
			}
		}

		// Token: 0x06001262 RID: 4706 RVA: 0x00048753 File Offset: 0x00046953
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001263 RID: 4707 RVA: 0x00048768 File Offset: 0x00046968
		protected virtual void Dispose(bool disposing)
		{
			bool disposed = this.disposed;
			if (!disposed)
			{
				if (disposing)
				{
					bool flag = this.atlas != null;
					if (flag)
					{
						UIRUtility.Destroy(this.atlas);
						this.atlas = null;
					}
					bool flag2 = this.m_Allocator != null;
					if (flag2)
					{
						this.m_Allocator = null;
					}
					bool flag3 = this.m_Blitter != null;
					if (flag3)
					{
						this.m_Blitter.Dispose();
						this.m_Blitter = null;
					}
					bool flag4 = this.textureId != TextureId.invalid;
					if (flag4)
					{
						TextureRegistry.instance.Release(this.textureId);
						this.textureId = TextureId.invalid;
					}
				}
				this.disposed = true;
			}
		}

		// Token: 0x06001264 RID: 4708 RVA: 0x00048830 File Offset: 0x00046A30
		public bool TryAdd(Texture2D image, out Allocator2D.Alloc2D alloc, out RectInt rect)
		{
			bool disposed = this.disposed;
			bool result;
			if (disposed)
			{
				DisposeHelper.NotifyDisposedUsed(this);
				alloc = default(Allocator2D.Alloc2D);
				rect = default(RectInt);
				result = false;
			}
			else
			{
				bool flag = !this.m_Allocator.TryAllocate(image.width + this.m_2Padding, image.height + this.m_2Padding, out alloc);
				if (flag)
				{
					rect = default(RectInt);
					result = false;
				}
				else
				{
					this.m_CurrentSize.x = Mathf.Max(this.m_CurrentSize.x, UIRUtility.GetNextPow2(alloc.rect.xMax));
					this.m_CurrentSize.y = Mathf.Max(this.m_CurrentSize.y, UIRUtility.GetNextPow2(alloc.rect.yMax));
					rect = new RectInt(alloc.rect.xMin + this.m_1Padding, alloc.rect.yMin + this.m_1Padding, image.width, image.height);
					this.Update(image, rect);
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06001265 RID: 4709 RVA: 0x00048948 File Offset: 0x00046B48
		public void Update(Texture2D image, RectInt rect)
		{
			bool disposed = this.disposed;
			if (disposed)
			{
				DisposeHelper.NotifyDisposedUsed(this);
			}
			else
			{
				Debug.Assert(image != null && rect.width > 0 && rect.height > 0);
				this.m_Blitter.QueueBlit(image, new RectInt(0, 0, image.width, image.height), new Vector2Int(rect.x, rect.y), true, Color.white);
			}
		}

		// Token: 0x06001266 RID: 4710 RVA: 0x000489CC File Offset: 0x00046BCC
		public void Remove(Allocator2D.Alloc2D alloc)
		{
			bool disposed = this.disposed;
			if (disposed)
			{
				DisposeHelper.NotifyDisposedUsed(this);
			}
			else
			{
				Debug.Assert(alloc.rect.width > 0 && alloc.rect.height > 0);
				this.m_Allocator.Free(alloc);
			}
		}

		// Token: 0x06001267 RID: 4711 RVA: 0x00048A24 File Offset: 0x00046C24
		public void Commit()
		{
			bool disposed = this.disposed;
			if (disposed)
			{
				DisposeHelper.NotifyDisposedUsed(this);
			}
			else
			{
				this.UpdateAtlasTexture();
				this.m_Blitter.Commit(this.atlas);
			}
		}

		// Token: 0x06001268 RID: 4712 RVA: 0x00048A60 File Offset: 0x00046C60
		private void UpdateAtlasTexture()
		{
			bool flag = this.atlas == null;
			if (flag)
			{
				this.atlas = this.CreateAtlasTexture();
			}
			else
			{
				bool flag2 = this.atlas.width != this.m_CurrentSize.x || this.atlas.height != this.m_CurrentSize.y;
				if (flag2)
				{
					RenderTexture renderTexture = this.CreateAtlasTexture();
					bool flag3 = renderTexture == null;
					if (flag3)
					{
						Debug.LogErrorFormat("Failed to allocate a render texture for the dynamic atlas. Current Size = {0}x{1}. Requested Size = {2}x{3}.", new object[]
						{
							this.atlas.width,
							this.atlas.height,
							this.m_CurrentSize.x,
							this.m_CurrentSize.y
						});
					}
					else
					{
						this.m_Blitter.BlitOneNow(renderTexture, this.atlas, new RectInt(0, 0, this.atlas.width, this.atlas.height), new Vector2Int(0, 0), false, Color.white);
					}
					UIRUtility.Destroy(this.atlas);
					this.atlas = renderTexture;
				}
			}
		}

		// Token: 0x06001269 RID: 4713 RVA: 0x00048B94 File Offset: 0x00046D94
		private RenderTexture CreateAtlasTexture()
		{
			bool flag = this.m_CurrentSize.x == 0 || this.m_CurrentSize.y == 0;
			RenderTexture result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = new RenderTexture(this.m_CurrentSize.x, this.m_CurrentSize.y, 0, this.format)
				{
					hideFlags = HideFlags.HideAndDontSave,
					name = "UIR Dynamic Atlas Page " + DynamicAtlasPage.s_TextureCounter++.ToString(),
					filterMode = this.filterMode
				};
			}
			return result;
		}

		// Token: 0x04000844 RID: 2116
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private TextureId <textureId>k__BackingField;

		// Token: 0x04000845 RID: 2117
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private RenderTexture <atlas>k__BackingField;

		// Token: 0x04000846 RID: 2118
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly RenderTextureFormat <format>k__BackingField;

		// Token: 0x04000847 RID: 2119
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly FilterMode <filterMode>k__BackingField;

		// Token: 0x04000848 RID: 2120
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly Vector2Int <minSize>k__BackingField;

		// Token: 0x04000849 RID: 2121
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly Vector2Int <maxSize>k__BackingField;

		// Token: 0x0400084A RID: 2122
		private readonly int m_1Padding = 1;

		// Token: 0x0400084B RID: 2123
		private readonly int m_2Padding = 2;

		// Token: 0x0400084C RID: 2124
		private Allocator2D m_Allocator;

		// Token: 0x0400084D RID: 2125
		private TextureBlitter m_Blitter;

		// Token: 0x0400084E RID: 2126
		private Vector2Int m_CurrentSize;

		// Token: 0x0400084F RID: 2127
		private static int s_TextureCounter;

		// Token: 0x04000850 RID: 2128
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private bool <disposed>k__BackingField;
	}
}
