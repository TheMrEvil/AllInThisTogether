using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Profiling;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000325 RID: 805
	internal class TextureBlitter : IDisposable
	{
		// Token: 0x17000653 RID: 1619
		// (get) Token: 0x06001A47 RID: 6727 RVA: 0x00071E45 File Offset: 0x00070045
		// (set) Token: 0x06001A48 RID: 6728 RVA: 0x00071E4D File Offset: 0x0007004D
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

		// Token: 0x06001A49 RID: 6729 RVA: 0x00071E56 File Offset: 0x00070056
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001A4A RID: 6730 RVA: 0x00071E68 File Offset: 0x00070068
		protected virtual void Dispose(bool disposing)
		{
			bool disposed = this.disposed;
			if (!disposed)
			{
				if (disposing)
				{
					UIRUtility.Destroy(this.m_BlitMaterial);
					this.m_BlitMaterial = null;
				}
				this.disposed = true;
			}
		}

		// Token: 0x06001A4B RID: 6731 RVA: 0x00071EA8 File Offset: 0x000700A8
		static TextureBlitter()
		{
			TextureBlitter.k_TextureIds = new int[8];
			for (int i = 0; i < 8; i++)
			{
				TextureBlitter.k_TextureIds[i] = Shader.PropertyToID("_MainTex" + i.ToString());
			}
		}

		// Token: 0x06001A4C RID: 6732 RVA: 0x00071EFD File Offset: 0x000700FD
		public TextureBlitter(int capacity = 512)
		{
			this.m_PendingBlits = new List<TextureBlitter.BlitInfo>(capacity);
		}

		// Token: 0x06001A4D RID: 6733 RVA: 0x00071F20 File Offset: 0x00070120
		public void QueueBlit(Texture src, RectInt srcRect, Vector2Int dstPos, bool addBorder, Color tint)
		{
			bool disposed = this.disposed;
			if (disposed)
			{
				DisposeHelper.NotifyDisposedUsed(this);
			}
			else
			{
				this.m_PendingBlits.Add(new TextureBlitter.BlitInfo
				{
					src = src,
					srcRect = srcRect,
					dstPos = dstPos,
					border = (addBorder ? 1 : 0),
					tint = tint
				});
			}
		}

		// Token: 0x06001A4E RID: 6734 RVA: 0x00071F88 File Offset: 0x00070188
		public void BlitOneNow(RenderTexture dst, Texture src, RectInt srcRect, Vector2Int dstPos, bool addBorder, Color tint)
		{
			bool disposed = this.disposed;
			if (disposed)
			{
				DisposeHelper.NotifyDisposedUsed(this);
			}
			else
			{
				this.m_SingleBlit[0] = new TextureBlitter.BlitInfo
				{
					src = src,
					srcRect = srcRect,
					dstPos = dstPos,
					border = (addBorder ? 1 : 0),
					tint = tint
				};
				this.BeginBlit(dst);
				this.DoBlit(this.m_SingleBlit, 0);
				this.EndBlit();
			}
		}

		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x06001A4F RID: 6735 RVA: 0x0007200D File Offset: 0x0007020D
		public int queueLength
		{
			get
			{
				return this.m_PendingBlits.Count;
			}
		}

		// Token: 0x06001A50 RID: 6736 RVA: 0x0007201C File Offset: 0x0007021C
		public void Commit(RenderTexture dst)
		{
			bool disposed = this.disposed;
			if (disposed)
			{
				DisposeHelper.NotifyDisposedUsed(this);
			}
			else
			{
				bool flag = this.m_PendingBlits.Count == 0;
				if (!flag)
				{
					TextureBlitter.s_CommitSampler.Begin();
					this.BeginBlit(dst);
					for (int i = 0; i < this.m_PendingBlits.Count; i += 8)
					{
						this.DoBlit(this.m_PendingBlits, i);
					}
					this.EndBlit();
					TextureBlitter.s_CommitSampler.End();
					this.m_PendingBlits.Clear();
				}
			}
		}

		// Token: 0x06001A51 RID: 6737 RVA: 0x000720AB File Offset: 0x000702AB
		public void Reset()
		{
			this.m_PendingBlits.Clear();
		}

		// Token: 0x06001A52 RID: 6738 RVA: 0x000720BC File Offset: 0x000702BC
		private void BeginBlit(RenderTexture dst)
		{
			bool flag = this.m_BlitMaterial == null;
			if (flag)
			{
				Shader shader = Shader.Find(Shaders.k_AtlasBlit);
				this.m_BlitMaterial = new Material(shader);
				this.m_BlitMaterial.hideFlags |= HideFlags.DontSaveInEditor;
			}
			bool flag2 = this.m_Properties == null;
			if (flag2)
			{
				this.m_Properties = new MaterialPropertyBlock();
			}
			this.m_Viewport = Utility.GetActiveViewport();
			this.m_PrevRT = RenderTexture.active;
			GL.LoadPixelMatrix(0f, (float)dst.width, 0f, (float)dst.height);
			Graphics.SetRenderTarget(dst);
			this.m_BlitMaterial.SetPass(0);
		}

		// Token: 0x06001A53 RID: 6739 RVA: 0x00072168 File Offset: 0x00070368
		private void DoBlit(IList<TextureBlitter.BlitInfo> blitInfos, int startIndex)
		{
			int num = Mathf.Min(blitInfos.Count - startIndex, 8);
			int num2 = startIndex + num;
			int i = startIndex;
			int num3 = 0;
			while (i < num2)
			{
				Texture src = blitInfos[i].src;
				bool flag = src != null;
				if (flag)
				{
					this.m_Properties.SetTexture(TextureBlitter.k_TextureIds[num3], src);
				}
				i++;
				num3++;
			}
			Utility.SetPropertyBlock(this.m_Properties);
			GL.Begin(7);
			int j = startIndex;
			int num4 = 0;
			while (j < num2)
			{
				TextureBlitter.BlitInfo blitInfo = blitInfos[j];
				float num5 = 1f / (float)blitInfo.src.width;
				float num6 = 1f / (float)blitInfo.src.height;
				float x = (float)(blitInfo.dstPos.x - blitInfo.border);
				float y = (float)(blitInfo.dstPos.y - blitInfo.border);
				float x2 = (float)(blitInfo.dstPos.x + blitInfo.srcRect.width + blitInfo.border);
				float y2 = (float)(blitInfo.dstPos.y + blitInfo.srcRect.height + blitInfo.border);
				float x3 = (float)(blitInfo.srcRect.x - blitInfo.border) * num5;
				float y3 = (float)(blitInfo.srcRect.y - blitInfo.border) * num6;
				float x4 = (float)(blitInfo.srcRect.xMax + blitInfo.border) * num5;
				float y4 = (float)(blitInfo.srcRect.yMax + blitInfo.border) * num6;
				GL.Color(blitInfo.tint);
				GL.TexCoord3(x3, y3, (float)num4);
				GL.Vertex3(x, y, 0f);
				GL.Color(blitInfo.tint);
				GL.TexCoord3(x3, y4, (float)num4);
				GL.Vertex3(x, y2, 0f);
				GL.Color(blitInfo.tint);
				GL.TexCoord3(x4, y4, (float)num4);
				GL.Vertex3(x2, y2, 0f);
				GL.Color(blitInfo.tint);
				GL.TexCoord3(x4, y3, (float)num4);
				GL.Vertex3(x2, y, 0f);
				j++;
				num4++;
			}
			GL.End();
		}

		// Token: 0x06001A54 RID: 6740 RVA: 0x000723CC File Offset: 0x000705CC
		private void EndBlit()
		{
			Graphics.SetRenderTarget(this.m_PrevRT);
			GL.Viewport(new Rect((float)this.m_Viewport.x, (float)this.m_Viewport.y, (float)this.m_Viewport.width, (float)this.m_Viewport.height));
		}

		// Token: 0x04000C00 RID: 3072
		private const int k_TextureSlotCount = 8;

		// Token: 0x04000C01 RID: 3073
		private static readonly int[] k_TextureIds;

		// Token: 0x04000C02 RID: 3074
		private static ProfilerMarker s_CommitSampler = new ProfilerMarker("UIR.TextureBlitter.Commit");

		// Token: 0x04000C03 RID: 3075
		private TextureBlitter.BlitInfo[] m_SingleBlit = new TextureBlitter.BlitInfo[1];

		// Token: 0x04000C04 RID: 3076
		private Material m_BlitMaterial;

		// Token: 0x04000C05 RID: 3077
		private MaterialPropertyBlock m_Properties;

		// Token: 0x04000C06 RID: 3078
		private RectInt m_Viewport;

		// Token: 0x04000C07 RID: 3079
		private RenderTexture m_PrevRT;

		// Token: 0x04000C08 RID: 3080
		private List<TextureBlitter.BlitInfo> m_PendingBlits;

		// Token: 0x04000C09 RID: 3081
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private bool <disposed>k__BackingField;

		// Token: 0x02000326 RID: 806
		private struct BlitInfo
		{
			// Token: 0x04000C0A RID: 3082
			public Texture src;

			// Token: 0x04000C0B RID: 3083
			public RectInt srcRect;

			// Token: 0x04000C0C RID: 3084
			public Vector2Int dstPos;

			// Token: 0x04000C0D RID: 3085
			public int border;

			// Token: 0x04000C0E RID: 3086
			public Color tint;
		}
	}
}
