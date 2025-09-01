using System;
using System.Collections.Generic;
using UnityEngine;

namespace Fluxy
{
	// Token: 0x0200000F RID: 15
	[CreateAssetMenu(fileName = "FluxyStorage", menuName = "Data/Fluxy Storage", order = 334)]
	public class FluxyStorage : ScriptableObject
	{
		// Token: 0x0600005F RID: 95 RVA: 0x00004ED4 File Offset: 0x000030D4
		public int RequestFramebuffer(int desiredResolution, int stateSupersampling)
		{
			FluxyStorage.Framebuffer framebuffer = new FluxyStorage.Framebuffer(desiredResolution, stateSupersampling);
			int num = 0;
			while (num < this.framebuffers.Count && this.framebuffers[num] != null)
			{
				num++;
			}
			if (num == this.framebuffers.Count)
			{
				this.framebuffers.Add(framebuffer);
			}
			else
			{
				this.framebuffers[num] = framebuffer;
			}
			this.ResizeStorage();
			return num;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00004F40 File Offset: 0x00003140
		public void DisposeFramebuffer(int framebufferID, bool expand = true)
		{
			if (framebufferID >= 0 && framebufferID < this.framebuffers.Count)
			{
				FluxyStorage.Framebuffer framebuffer = this.framebuffers[framebufferID];
				if (framebuffer != null)
				{
					RenderTexture.ReleaseTemporary(framebuffer.velocityA);
					RenderTexture.ReleaseTemporary(framebuffer.velocityB);
					RenderTexture.ReleaseTemporary(framebuffer.stateA);
					RenderTexture.ReleaseTemporary(framebuffer.stateB);
					this.framebuffers[framebufferID] = null;
					if (expand)
					{
						this.ResizeStorage();
					}
				}
			}
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00004FB1 File Offset: 0x000031B1
		private int PrevPowerTwo(int x)
		{
			if (x == 0)
			{
				return 0;
			}
			x |= x >> 1;
			x |= x >> 2;
			x |= x >> 4;
			x |= x >> 8;
			x |= x >> 16;
			return x - (x >> 1);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00004FE4 File Offset: 0x000031E4
		public void ResizeStorage()
		{
			int bytesPerPixel = this.GetBytesPerPixel(this.densityPrecision);
			int bytesPerPixel2 = this.GetBytesPerPixel(this.velocityPrecision);
			float num = (float)(this.memoryBudget * 1048576) / (float)(2 * (bytesPerPixel + bytesPerPixel2));
			float num2 = 0f;
			for (int i = 0; i < this.framebuffers.Count; i++)
			{
				if (this.framebuffers[i] != null)
				{
					num2 += (float)this.framebuffers[i].desiredResolution;
				}
			}
			for (int j = 0; j < this.framebuffers.Count; j++)
			{
				if (this.framebuffers[j] != null)
				{
					float num3 = (float)this.framebuffers[j].desiredResolution / num2;
					int b = Mathf.FloorToInt(Mathf.Sqrt(num * num3));
					int x = Mathf.Min(this.framebuffers[j].desiredResolution, b);
					int resolution = Mathf.Max(32, this.PrevPowerTwo(x));
					this.ReallocateFramebuffer(j, resolution);
				}
			}
		}

		// Token: 0x06000063 RID: 99 RVA: 0x000050EE File Offset: 0x000032EE
		public FluxyStorage.Framebuffer GetFramebuffer(int framebufferID)
		{
			if (framebufferID >= 0 && framebufferID < this.framebuffers.Count)
			{
				return this.framebuffers[framebufferID];
			}
			return null;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00005110 File Offset: 0x00003310
		private RenderTextureFormat GetRenderTextureFormat(FluxyStorage.FluidTexturePrecision precision)
		{
			switch (precision)
			{
			case FluxyStorage.FluidTexturePrecision.Float:
				if (SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.ARGBFloat))
				{
					return RenderTextureFormat.ARGBFloat;
				}
				if (SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.ARGBHalf))
				{
					return RenderTextureFormat.ARGBHalf;
				}
				return RenderTextureFormat.ARGB32;
			case FluxyStorage.FluidTexturePrecision.Half:
				if (SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.ARGBHalf))
				{
					return RenderTextureFormat.ARGBHalf;
				}
				return RenderTextureFormat.ARGB32;
			}
			return RenderTextureFormat.ARGB32;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x0000514B File Offset: 0x0000334B
		private int GetBytesPerPixel(FluxyStorage.FluidTexturePrecision precision)
		{
			switch (precision)
			{
			case FluxyStorage.FluidTexturePrecision.Float:
				if (SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.ARGBFloat))
				{
					return 16;
				}
				if (SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.ARGBHalf))
				{
					return 8;
				}
				return 4;
			case FluxyStorage.FluidTexturePrecision.Half:
				if (SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.ARGBHalf))
				{
					return 8;
				}
				return 4;
			}
			return 4;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00005188 File Offset: 0x00003388
		private void ReallocateFramebuffer(int id, int resolution)
		{
			FluxyStorage.Framebuffer framebuffer = this.framebuffers[id];
			if (framebuffer.stateA != null && framebuffer.stateA.width == resolution * framebuffer.stateSupersampling)
			{
				return;
			}
			RenderTextureFormat renderTextureFormat = this.GetRenderTextureFormat(this.densityPrecision);
			RenderTextureFormat renderTextureFormat2 = this.GetRenderTextureFormat(this.velocityPrecision);
			RenderTexture temporary = RenderTexture.GetTemporary(resolution, resolution, 0, renderTextureFormat2, RenderTextureReadWrite.Linear);
			RenderTexture temporary2 = RenderTexture.GetTemporary(resolution, resolution, 0, renderTextureFormat2, RenderTextureReadWrite.Linear);
			RenderTexture temporary3 = RenderTexture.GetTemporary(resolution * framebuffer.stateSupersampling, resolution * framebuffer.stateSupersampling, 0, renderTextureFormat, RenderTextureReadWrite.Linear);
			RenderTexture temporary4 = RenderTexture.GetTemporary(resolution * framebuffer.stateSupersampling, resolution * framebuffer.stateSupersampling, 0, renderTextureFormat, RenderTextureReadWrite.Linear);
			RenderTexture temporary5 = RenderTexture.GetTemporary(resolution, resolution, 0, RenderTextureFormat.RHalf, RenderTextureReadWrite.Linear);
			temporary.filterMode = FilterMode.Point;
			temporary2.filterMode = FilterMode.Point;
			temporary3.filterMode = FilterMode.Point;
			temporary4.filterMode = FilterMode.Point;
			temporary5.filterMode = FilterMode.Point;
			if (framebuffer.velocityA != null)
			{
				Graphics.Blit(framebuffer.velocityA, temporary);
				Graphics.Blit(framebuffer.stateA, temporary3);
				Graphics.Blit(framebuffer.tileID, temporary5);
			}
			else
			{
				RenderTexture active = RenderTexture.active;
				RenderTexture.active = temporary;
				GL.Clear(false, true, Color.clear);
				RenderTexture.active = temporary3;
				GL.Clear(false, true, Color.clear);
				RenderTexture.active = temporary5;
				GL.Clear(false, true, Color.clear);
				RenderTexture.active = active;
			}
			RenderTexture.ReleaseTemporary(framebuffer.velocityA);
			RenderTexture.ReleaseTemporary(framebuffer.velocityB);
			RenderTexture.ReleaseTemporary(framebuffer.stateA);
			RenderTexture.ReleaseTemporary(framebuffer.stateB);
			RenderTexture.ReleaseTemporary(framebuffer.tileID);
			framebuffer.velocityA = temporary;
			framebuffer.velocityB = temporary2;
			framebuffer.stateA = temporary3;
			framebuffer.stateB = temporary4;
			framebuffer.tileID = temporary5;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00005335 File Offset: 0x00003535
		public FluxyStorage()
		{
		}

		// Token: 0x0400006F RID: 111
		public const int minFramebufferSize = 32;

		// Token: 0x04000070 RID: 112
		public const int bytesPerMbyte = 1048576;

		// Token: 0x04000071 RID: 113
		[Tooltip("Memory budget, expressed in megabytes. The combined memory used by all solvers sharing this asset will not be larger than this value. Note that supersampling is not taken into account.")]
		public int memoryBudget = 32;

		// Token: 0x04000072 RID: 114
		[Tooltip("Precision of the density textures.")]
		public FluxyStorage.FluidTexturePrecision densityPrecision = FluxyStorage.FluidTexturePrecision.Half;

		// Token: 0x04000073 RID: 115
		[Tooltip("Precision of the velocity textures.")]
		public FluxyStorage.FluidTexturePrecision velocityPrecision = FluxyStorage.FluidTexturePrecision.Half;

		// Token: 0x04000074 RID: 116
		public List<FluxyStorage.Framebuffer> framebuffers = new List<FluxyStorage.Framebuffer>();

		// Token: 0x0200002B RID: 43
		public class Framebuffer
		{
			// Token: 0x060000B3 RID: 179 RVA: 0x00006ED8 File Offset: 0x000050D8
			public Framebuffer(int desiredResolution, int stateSupersampling = 1)
			{
				this.desiredResolution = desiredResolution;
				this.stateSupersampling = Mathf.Max(1, stateSupersampling);
			}

			// Token: 0x040000EE RID: 238
			public RenderTexture velocityA;

			// Token: 0x040000EF RID: 239
			public RenderTexture velocityB;

			// Token: 0x040000F0 RID: 240
			public RenderTexture stateA;

			// Token: 0x040000F1 RID: 241
			public RenderTexture stateB;

			// Token: 0x040000F2 RID: 242
			public RenderTexture tileID;

			// Token: 0x040000F3 RID: 243
			public int desiredResolution = 256;

			// Token: 0x040000F4 RID: 244
			public int stateSupersampling = 1;
		}

		// Token: 0x0200002C RID: 44
		public enum FluidTexturePrecision
		{
			// Token: 0x040000F6 RID: 246
			Float,
			// Token: 0x040000F7 RID: 247
			Half,
			// Token: 0x040000F8 RID: 248
			Fixed
		}
	}
}
