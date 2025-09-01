using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering.PostProcessing
{
	// Token: 0x02000070 RID: 112
	internal class TextureLerper
	{
		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000267 RID: 615 RVA: 0x00012A88 File Offset: 0x00010C88
		internal static TextureLerper instance
		{
			get
			{
				if (TextureLerper.m_Instance == null)
				{
					TextureLerper.m_Instance = new TextureLerper();
				}
				return TextureLerper.m_Instance;
			}
		}

		// Token: 0x06000268 RID: 616 RVA: 0x00012AA0 File Offset: 0x00010CA0
		private TextureLerper()
		{
			this.m_Recycled = new List<RenderTexture>();
			this.m_Actives = new List<RenderTexture>();
		}

		// Token: 0x06000269 RID: 617 RVA: 0x00012ABE File Offset: 0x00010CBE
		internal void BeginFrame(PostProcessRenderContext context)
		{
			this.m_Command = context.command;
			this.m_PropertySheets = context.propertySheets;
			this.m_Resources = context.resources;
		}

		// Token: 0x0600026A RID: 618 RVA: 0x00012AE4 File Offset: 0x00010CE4
		internal void EndFrame()
		{
			if (this.m_Recycled.Count > 0)
			{
				foreach (RenderTexture obj in this.m_Recycled)
				{
					RuntimeUtilities.Destroy(obj);
				}
				this.m_Recycled.Clear();
			}
			if (this.m_Actives.Count > 0)
			{
				this.m_Recycled.AddRange(this.m_Actives);
				this.m_Actives.Clear();
			}
		}

		// Token: 0x0600026B RID: 619 RVA: 0x00012B78 File Offset: 0x00010D78
		private RenderTexture Get(RenderTextureFormat format, int w, int h, int d = 1, bool enableRandomWrite = false, bool force3D = false)
		{
			RenderTexture renderTexture = null;
			int count = this.m_Recycled.Count;
			int i;
			for (i = 0; i < count; i++)
			{
				RenderTexture renderTexture2 = this.m_Recycled[i];
				if (renderTexture2.width == w && renderTexture2.height == h && renderTexture2.volumeDepth == d && renderTexture2.format == format && renderTexture2.enableRandomWrite == enableRandomWrite && (!force3D || renderTexture2.dimension == TextureDimension.Tex3D))
				{
					renderTexture = renderTexture2;
					break;
				}
			}
			if (renderTexture == null)
			{
				TextureDimension dimension = (d > 1 || force3D) ? TextureDimension.Tex3D : TextureDimension.Tex2D;
				renderTexture = new RenderTexture(w, h, 0, format)
				{
					dimension = dimension,
					filterMode = FilterMode.Bilinear,
					wrapMode = TextureWrapMode.Clamp,
					anisoLevel = 0,
					volumeDepth = d,
					enableRandomWrite = enableRandomWrite
				};
				renderTexture.Create();
			}
			else
			{
				this.m_Recycled.RemoveAt(i);
			}
			this.m_Actives.Add(renderTexture);
			return renderTexture;
		}

		// Token: 0x0600026C RID: 620 RVA: 0x00012C60 File Offset: 0x00010E60
		internal Texture Lerp(Texture from, Texture to, float t)
		{
			if (from == to)
			{
				return from;
			}
			if (t <= 0f)
			{
				return from;
			}
			if (t >= 1f)
			{
				return to;
			}
			RenderTexture renderTexture;
			if (from is Texture3D || (from is RenderTexture && ((RenderTexture)from).volumeDepth > 1))
			{
				int num = (from is Texture3D) ? ((Texture3D)from).depth : ((RenderTexture)from).volumeDepth;
				int num2 = Mathf.Max(Mathf.Max(from.width, from.height), num);
				renderTexture = this.Get(RenderTextureFormat.ARGBHalf, from.width, from.height, num, true, true);
				ComputeShader texture3dLerp = this.m_Resources.computeShaders.texture3dLerp;
				int kernelIndex = texture3dLerp.FindKernel("KTexture3DLerp");
				this.m_Command.SetComputeVectorParam(texture3dLerp, "_DimensionsAndLerp", new Vector4((float)from.width, (float)from.height, (float)num, t));
				this.m_Command.SetComputeTextureParam(texture3dLerp, kernelIndex, "_Output", renderTexture);
				this.m_Command.SetComputeTextureParam(texture3dLerp, kernelIndex, "_From", from);
				this.m_Command.SetComputeTextureParam(texture3dLerp, kernelIndex, "_To", to);
				uint num3;
				uint num4;
				uint num5;
				texture3dLerp.GetKernelThreadGroupSizes(kernelIndex, out num3, out num4, out num5);
				int num6 = Mathf.CeilToInt((float)num2 / num3);
				int threadGroupsZ = Mathf.CeilToInt((float)num2 / num5);
				this.m_Command.DispatchCompute(texture3dLerp, kernelIndex, num6, num6, threadGroupsZ);
				return renderTexture;
			}
			RenderTextureFormat uncompressedRenderTextureFormat = TextureFormatUtilities.GetUncompressedRenderTextureFormat(to);
			renderTexture = this.Get(uncompressedRenderTextureFormat, to.width, to.height, 1, false, false);
			PropertySheet propertySheet = this.m_PropertySheets.Get(this.m_Resources.shaders.texture2dLerp);
			propertySheet.properties.SetTexture(ShaderIDs.To, to);
			propertySheet.properties.SetFloat(ShaderIDs.Interp, t);
			this.m_Command.BlitFullscreenTriangle(from, renderTexture, propertySheet, 0, false, null, false);
			return renderTexture;
		}

		// Token: 0x0600026D RID: 621 RVA: 0x00012E60 File Offset: 0x00011060
		internal Texture Lerp(Texture from, Color to, float t)
		{
			if ((double)t < 1E-05)
			{
				return from;
			}
			RenderTexture renderTexture;
			if (from is Texture3D || (from is RenderTexture && ((RenderTexture)from).volumeDepth > 1))
			{
				int num = (from is Texture3D) ? ((Texture3D)from).depth : ((RenderTexture)from).volumeDepth;
				float num2 = (float)Mathf.Max(Mathf.Max(from.width, from.height), num);
				renderTexture = this.Get(RenderTextureFormat.ARGBHalf, from.width, from.height, num, true, true);
				ComputeShader texture3dLerp = this.m_Resources.computeShaders.texture3dLerp;
				int kernelIndex = texture3dLerp.FindKernel("KTexture3DLerpToColor");
				this.m_Command.SetComputeVectorParam(texture3dLerp, "_DimensionsAndLerp", new Vector4((float)from.width, (float)from.height, (float)num, t));
				this.m_Command.SetComputeVectorParam(texture3dLerp, "_TargetColor", new Vector4(to.r, to.g, to.b, to.a));
				this.m_Command.SetComputeTextureParam(texture3dLerp, kernelIndex, "_Output", renderTexture);
				this.m_Command.SetComputeTextureParam(texture3dLerp, kernelIndex, "_From", from);
				int num3 = Mathf.CeilToInt(num2 / 4f);
				this.m_Command.DispatchCompute(texture3dLerp, kernelIndex, num3, num3, num3);
				return renderTexture;
			}
			RenderTextureFormat uncompressedRenderTextureFormat = TextureFormatUtilities.GetUncompressedRenderTextureFormat(from);
			renderTexture = this.Get(uncompressedRenderTextureFormat, from.width, from.height, 1, false, false);
			PropertySheet propertySheet = this.m_PropertySheets.Get(this.m_Resources.shaders.texture2dLerp);
			propertySheet.properties.SetVector(ShaderIDs.TargetColor, new Vector4(to.r, to.g, to.b, to.a));
			propertySheet.properties.SetFloat(ShaderIDs.Interp, t);
			this.m_Command.BlitFullscreenTriangle(from, renderTexture, propertySheet, 1, false, null, false);
			return renderTexture;
		}

		// Token: 0x0600026E RID: 622 RVA: 0x00013064 File Offset: 0x00011264
		internal void Clear()
		{
			foreach (RenderTexture obj in this.m_Actives)
			{
				RuntimeUtilities.Destroy(obj);
			}
			foreach (RenderTexture obj2 in this.m_Recycled)
			{
				RuntimeUtilities.Destroy(obj2);
			}
			this.m_Actives.Clear();
			this.m_Recycled.Clear();
		}

		// Token: 0x040002C2 RID: 706
		private static TextureLerper m_Instance;

		// Token: 0x040002C3 RID: 707
		private CommandBuffer m_Command;

		// Token: 0x040002C4 RID: 708
		private PropertySheetFactory m_PropertySheets;

		// Token: 0x040002C5 RID: 709
		private PostProcessResources m_Resources;

		// Token: 0x040002C6 RID: 710
		private List<RenderTexture> m_Recycled;

		// Token: 0x040002C7 RID: 711
		private List<RenderTexture> m_Actives;
	}
}
