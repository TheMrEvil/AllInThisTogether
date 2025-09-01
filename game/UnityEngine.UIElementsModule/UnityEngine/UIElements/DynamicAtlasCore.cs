using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Profiling;
using UnityEngine.UIElements.UIR;

namespace UnityEngine.UIElements
{
	// Token: 0x02000259 RID: 601
	internal class DynamicAtlasCore : IDisposable
	{
		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x06001243 RID: 4675 RVA: 0x00047FAD File Offset: 0x000461AD
		public int maxImageSize
		{
			[CompilerGenerated]
			get
			{
				return this.<maxImageSize>k__BackingField;
			}
		}

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06001244 RID: 4676 RVA: 0x00047FB5 File Offset: 0x000461B5
		public RenderTextureFormat format
		{
			[CompilerGenerated]
			get
			{
				return this.<format>k__BackingField;
			}
		}

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x06001245 RID: 4677 RVA: 0x00047FBD File Offset: 0x000461BD
		// (set) Token: 0x06001246 RID: 4678 RVA: 0x00047FC5 File Offset: 0x000461C5
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

		// Token: 0x06001247 RID: 4679 RVA: 0x00047FD0 File Offset: 0x000461D0
		public DynamicAtlasCore(RenderTextureFormat format = RenderTextureFormat.ARGB32, FilterMode filterMode = FilterMode.Bilinear, int maxImageSize = 64, int initialSize = 64, int maxAtlasSize = 4096)
		{
			Debug.Assert(filterMode == FilterMode.Bilinear || filterMode == FilterMode.Point);
			Debug.Assert(maxAtlasSize <= SystemInfo.maxRenderTextureSize);
			Debug.Assert(initialSize <= maxAtlasSize);
			Debug.Assert(Mathf.IsPowerOfTwo(maxImageSize));
			Debug.Assert(Mathf.IsPowerOfTwo(initialSize));
			Debug.Assert(Mathf.IsPowerOfTwo(maxAtlasSize));
			this.m_MaxAtlasSize = maxAtlasSize;
			this.format = format;
			this.maxImageSize = maxImageSize;
			this.m_FilterMode = filterMode;
			this.m_UVs = new Dictionary<Texture2D, RectInt>(64);
			this.m_Blitter = new TextureBlitter(64);
			this.m_InitialSize = initialSize;
			this.m_2SidePadding = ((filterMode == FilterMode.Point) ? 0 : 2);
			this.m_1SidePadding = ((filterMode == FilterMode.Point) ? 0 : 1);
			this.m_Allocator = new UIRAtlasAllocator(this.m_InitialSize, this.m_MaxAtlasSize, this.m_1SidePadding);
			this.m_ColorSpace = QualitySettings.activeColorSpace;
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x06001248 RID: 4680 RVA: 0x000480BE File Offset: 0x000462BE
		// (set) Token: 0x06001249 RID: 4681 RVA: 0x000480C6 File Offset: 0x000462C6
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

		// Token: 0x0600124A RID: 4682 RVA: 0x000480CF File Offset: 0x000462CF
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600124B RID: 4683 RVA: 0x000480E4 File Offset: 0x000462E4
		protected virtual void Dispose(bool disposing)
		{
			bool disposed = this.disposed;
			if (!disposed)
			{
				if (disposing)
				{
					UIRUtility.Destroy(this.atlas);
					this.atlas = null;
					bool flag = this.m_Allocator != null;
					if (flag)
					{
						this.m_Allocator.Dispose();
						this.m_Allocator = null;
					}
					bool flag2 = this.m_Blitter != null;
					if (flag2)
					{
						this.m_Blitter.Dispose();
						this.m_Blitter = null;
					}
				}
				this.disposed = true;
			}
		}

		// Token: 0x0600124C RID: 4684 RVA: 0x00048167 File Offset: 0x00046367
		private static void LogDisposeError()
		{
			Debug.LogError("An attempt to use a disposed atlas manager has been detected.");
		}

		// Token: 0x0600124D RID: 4685 RVA: 0x00048178 File Offset: 0x00046378
		public bool IsReleased()
		{
			return this.atlas != null && !this.atlas.IsCreated();
		}

		// Token: 0x0600124E RID: 4686 RVA: 0x000481AC File Offset: 0x000463AC
		public bool TryGetRect(Texture2D image, out RectInt uvs, Func<Texture2D, bool> filter = null)
		{
			uvs = default(RectInt);
			bool disposed = this.disposed;
			bool result;
			if (disposed)
			{
				DynamicAtlasCore.LogDisposeError();
				result = false;
			}
			else
			{
				bool flag = image == null;
				if (flag)
				{
					result = false;
				}
				else
				{
					bool flag2 = this.m_UVs.TryGetValue(image, out uvs);
					if (flag2)
					{
						result = true;
					}
					else
					{
						bool flag3 = filter != null && !filter(image);
						if (flag3)
						{
							result = false;
						}
						else
						{
							bool flag4 = !this.AllocateRect(image.width, image.height, out uvs);
							if (flag4)
							{
								result = false;
							}
							else
							{
								this.m_UVs[image] = uvs;
								this.m_Blitter.QueueBlit(image, new RectInt(0, 0, image.width, image.height), new Vector2Int(uvs.x, uvs.y), true, Color.white);
								result = true;
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x0600124F RID: 4687 RVA: 0x0004828C File Offset: 0x0004648C
		public void UpdateTexture(Texture2D image)
		{
			bool disposed = this.disposed;
			if (disposed)
			{
				DynamicAtlasCore.LogDisposeError();
			}
			else
			{
				RectInt rectInt;
				bool flag = !this.m_UVs.TryGetValue(image, out rectInt);
				if (!flag)
				{
					this.m_Blitter.QueueBlit(image, new RectInt(0, 0, image.width, image.height), new Vector2Int(rectInt.x, rectInt.y), true, Color.white);
				}
			}
		}

		// Token: 0x06001250 RID: 4688 RVA: 0x00048300 File Offset: 0x00046500
		public bool AllocateRect(int width, int height, out RectInt uvs)
		{
			bool flag = !this.m_Allocator.TryAllocate(width + this.m_2SidePadding, height + this.m_2SidePadding, out uvs);
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				uvs = new RectInt(uvs.x + this.m_1SidePadding, uvs.y + this.m_1SidePadding, width, height);
				result = true;
			}
			return result;
		}

		// Token: 0x06001251 RID: 4689 RVA: 0x00048361 File Offset: 0x00046561
		public void EnqueueBlit(Texture image, RectInt srcRect, int x, int y, bool addBorder, Color tint)
		{
			this.m_Blitter.QueueBlit(image, srcRect, new Vector2Int(x, y), addBorder, tint);
		}

		// Token: 0x06001252 RID: 4690 RVA: 0x00048380 File Offset: 0x00046580
		public void Commit()
		{
			bool disposed = this.disposed;
			if (disposed)
			{
				DynamicAtlasCore.LogDisposeError();
			}
			else
			{
				this.UpdateAtlasTexture();
				bool forceReblitAll = this.m_ForceReblitAll;
				if (forceReblitAll)
				{
					this.m_ForceReblitAll = false;
					this.m_Blitter.Reset();
					foreach (KeyValuePair<Texture2D, RectInt> keyValuePair in this.m_UVs)
					{
						this.m_Blitter.QueueBlit(keyValuePair.Key, new RectInt(0, 0, keyValuePair.Key.width, keyValuePair.Key.height), new Vector2Int(keyValuePair.Value.x, keyValuePair.Value.y), true, Color.white);
					}
				}
				this.m_Blitter.Commit(this.atlas);
			}
		}

		// Token: 0x06001253 RID: 4691 RVA: 0x00048480 File Offset: 0x00046680
		private void UpdateAtlasTexture()
		{
			bool flag = this.atlas == null;
			if (flag)
			{
				bool flag2 = this.m_UVs.Count > this.m_Blitter.queueLength;
				if (flag2)
				{
					this.m_ForceReblitAll = true;
				}
				this.atlas = this.CreateAtlasTexture();
			}
			else
			{
				bool flag3 = this.atlas.width != this.m_Allocator.physicalWidth || this.atlas.height != this.m_Allocator.physicalHeight;
				if (flag3)
				{
					RenderTexture renderTexture = this.CreateAtlasTexture();
					bool flag4 = renderTexture == null;
					if (flag4)
					{
						Debug.LogErrorFormat("Failed to allocate a render texture for the dynamic atlas. Current Size = {0}x{1}. Requested Size = {2}x{3}.", new object[]
						{
							this.atlas.width,
							this.atlas.height,
							this.m_Allocator.physicalWidth,
							this.m_Allocator.physicalHeight
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

		// Token: 0x06001254 RID: 4692 RVA: 0x000485DC File Offset: 0x000467DC
		private RenderTexture CreateAtlasTexture()
		{
			bool flag = this.m_Allocator.physicalWidth == 0 || this.m_Allocator.physicalHeight == 0;
			RenderTexture result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = new RenderTexture(this.m_Allocator.physicalWidth, this.m_Allocator.physicalHeight, 0, this.format)
				{
					hideFlags = HideFlags.HideAndDontSave,
					name = "UIR Dynamic Atlas " + DynamicAtlasCore.s_TextureCounter++.ToString(),
					filterMode = this.m_FilterMode
				};
			}
			return result;
		}

		// Token: 0x06001255 RID: 4693 RVA: 0x00048672 File Offset: 0x00046872
		// Note: this type is marked as 'beforefieldinit'.
		static DynamicAtlasCore()
		{
		}

		// Token: 0x04000834 RID: 2100
		private int m_InitialSize;

		// Token: 0x04000835 RID: 2101
		private UIRAtlasAllocator m_Allocator;

		// Token: 0x04000836 RID: 2102
		private Dictionary<Texture2D, RectInt> m_UVs;

		// Token: 0x04000837 RID: 2103
		private bool m_ForceReblitAll;

		// Token: 0x04000838 RID: 2104
		private FilterMode m_FilterMode;

		// Token: 0x04000839 RID: 2105
		private ColorSpace m_ColorSpace;

		// Token: 0x0400083A RID: 2106
		private TextureBlitter m_Blitter;

		// Token: 0x0400083B RID: 2107
		private int m_2SidePadding;

		// Token: 0x0400083C RID: 2108
		private int m_1SidePadding;

		// Token: 0x0400083D RID: 2109
		private int m_MaxAtlasSize;

		// Token: 0x0400083E RID: 2110
		private static ProfilerMarker s_MarkerReset = new ProfilerMarker("UIR.AtlasManager.Reset");

		// Token: 0x0400083F RID: 2111
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly int <maxImageSize>k__BackingField;

		// Token: 0x04000840 RID: 2112
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private readonly RenderTextureFormat <format>k__BackingField;

		// Token: 0x04000841 RID: 2113
		private static int s_TextureCounter;

		// Token: 0x04000842 RID: 2114
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private RenderTexture <atlas>k__BackingField;

		// Token: 0x04000843 RID: 2115
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool <disposed>k__BackingField;
	}
}
