using System;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering.PostProcessing
{
	// Token: 0x02000031 RID: 49
	[Preserve]
	[Serializable]
	internal sealed class MultiScaleVO : IAmbientOcclusionMethod
	{
		// Token: 0x06000090 RID: 144 RVA: 0x000078A4 File Offset: 0x00005AA4
		public MultiScaleVO(AmbientOcclusion settings)
		{
			this.m_Settings = settings;
			this.m_R8Format = RenderTextureFormat.R8;
			this.m_R16Format = RenderTextureFormat.RHalf;
			if (!SystemInfo.IsFormatSupported(GraphicsFormatUtility.GetGraphicsFormat(this.m_R8Format, false), FormatUsage.LoadStore) && SystemInfo.IsFormatSupported(GraphicsFormatUtility.GetGraphicsFormat(RenderTextureFormat.ARGB32, false), FormatUsage.LoadStore))
			{
				this.m_R8Format = RenderTextureFormat.ARGB32;
				this.float4Texture = true;
			}
			if (!SystemInfo.IsFormatSupported(GraphicsFormatUtility.GetGraphicsFormat(this.m_R16Format, false), FormatUsage.LoadStore) && SystemInfo.IsFormatSupported(GraphicsFormatUtility.GetGraphicsFormat(RenderTextureFormat.RFloat, false), FormatUsage.LoadStore))
			{
				this.m_R16Format = RenderTextureFormat.RFloat;
			}
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00007A4C File Offset: 0x00005C4C
		public DepthTextureMode GetCameraFlags()
		{
			return DepthTextureMode.Depth;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00007A4F File Offset: 0x00005C4F
		public void SetResources(PostProcessResources resources)
		{
			this.m_Resources = resources;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00007A58 File Offset: 0x00005C58
		private void Alloc(CommandBuffer cmd, int id, MultiScaleVO.MipLevel size, RenderTextureFormat format, bool uav, bool dynamicScale)
		{
			cmd.GetTemporaryRT(id, new RenderTextureDescriptor
			{
				width = this.m_Widths[(int)size],
				height = this.m_Heights[(int)size],
				colorFormat = format,
				depthBufferBits = 0,
				volumeDepth = 1,
				autoGenerateMips = false,
				msaaSamples = 1,
				mipCount = 1,
				useDynamicScale = dynamicScale,
				enableRandomWrite = uav,
				dimension = TextureDimension.Tex2D,
				sRGB = false
			}, FilterMode.Point);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00007AEC File Offset: 0x00005CEC
		private void AllocArray(CommandBuffer cmd, int id, MultiScaleVO.MipLevel size, RenderTextureFormat format, bool uav, bool dynamicScale)
		{
			cmd.GetTemporaryRT(id, new RenderTextureDescriptor
			{
				width = this.m_Widths[(int)size],
				height = this.m_Heights[(int)size],
				colorFormat = format,
				depthBufferBits = 0,
				volumeDepth = 16,
				autoGenerateMips = false,
				msaaSamples = 1,
				mipCount = 1,
				useDynamicScale = dynamicScale,
				enableRandomWrite = uav,
				dimension = TextureDimension.Tex2DArray,
				sRGB = false
			}, FilterMode.Point);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00007B7E File Offset: 0x00005D7E
		private void Release(CommandBuffer cmd, int id)
		{
			cmd.ReleaseTemporaryRT(id);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00007B88 File Offset: 0x00005D88
		private Vector4 CalculateZBufferParams(Camera camera)
		{
			float num = camera.farClipPlane / camera.nearClipPlane;
			if (SystemInfo.usesReversedZBuffer)
			{
				return new Vector4(num - 1f, 1f, 0f, 0f);
			}
			return new Vector4(1f - num, num, 0f, 0f);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00007BE0 File Offset: 0x00005DE0
		private float CalculateTanHalfFovHeight(Camera camera)
		{
			return 1f / camera.projectionMatrix[0, 0];
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00007C03 File Offset: 0x00005E03
		private Vector2 GetSize(MultiScaleVO.MipLevel mip)
		{
			return new Vector2((float)this.m_ScaledWidths[(int)mip], (float)this.m_ScaledHeights[(int)mip]);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00007C1C File Offset: 0x00005E1C
		private Vector3 GetSizeArray(MultiScaleVO.MipLevel mip)
		{
			return new Vector3((float)this.m_ScaledWidths[(int)mip], (float)this.m_ScaledHeights[(int)mip], 16f);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00007C3C File Offset: 0x00005E3C
		public void GenerateAOMap(CommandBuffer cmd, Camera camera, RenderTargetIdentifier destination, RenderTargetIdentifier? depthMap, bool invert, bool isMSAA)
		{
			this.m_Widths[0] = (this.m_ScaledWidths[0] = camera.pixelWidth * (RuntimeUtilities.isSinglePassStereoEnabled ? 2 : 1));
			this.m_Heights[0] = (this.m_ScaledHeights[0] = camera.pixelHeight);
			this.m_ScaledWidths[0] = camera.scaledPixelWidth * (RuntimeUtilities.isSinglePassStereoEnabled ? 2 : 1);
			this.m_ScaledHeights[0] = camera.scaledPixelHeight;
			float widthScaleFactor = ScalableBufferManager.widthScaleFactor;
			float heightScaleFactor = ScalableBufferManager.heightScaleFactor;
			for (int i = 1; i < 7; i++)
			{
				int num = 1 << i;
				this.m_Widths[i] = (this.m_Widths[0] + (num - 1)) / num;
				this.m_Heights[i] = (this.m_Heights[0] + (num - 1)) / num;
				this.m_ScaledWidths[i] = Mathf.CeilToInt((float)this.m_Widths[i] * widthScaleFactor);
				this.m_ScaledHeights[i] = Mathf.CeilToInt((float)this.m_Heights[i] * heightScaleFactor);
			}
			this.PushAllocCommands(cmd, isMSAA, camera);
			this.PushDownsampleCommands(cmd, camera, depthMap, isMSAA);
			float tanHalfFovH = this.CalculateTanHalfFovHeight(camera);
			this.PushRenderCommands(cmd, ShaderIDs.TiledDepth1, ShaderIDs.Occlusion1, this.GetSizeArray(MultiScaleVO.MipLevel.L3), tanHalfFovH, isMSAA);
			this.PushRenderCommands(cmd, ShaderIDs.TiledDepth2, ShaderIDs.Occlusion2, this.GetSizeArray(MultiScaleVO.MipLevel.L4), tanHalfFovH, isMSAA);
			this.PushRenderCommands(cmd, ShaderIDs.TiledDepth3, ShaderIDs.Occlusion3, this.GetSizeArray(MultiScaleVO.MipLevel.L5), tanHalfFovH, isMSAA);
			this.PushRenderCommands(cmd, ShaderIDs.TiledDepth4, ShaderIDs.Occlusion4, this.GetSizeArray(MultiScaleVO.MipLevel.L6), tanHalfFovH, isMSAA);
			this.PushUpsampleCommands(cmd, ShaderIDs.LowDepth4, ShaderIDs.Occlusion4, ShaderIDs.LowDepth3, new int?(ShaderIDs.Occlusion3), ShaderIDs.Combined3, this.GetSize(MultiScaleVO.MipLevel.L4), this.GetSize(MultiScaleVO.MipLevel.L3), isMSAA, false);
			this.PushUpsampleCommands(cmd, ShaderIDs.LowDepth3, ShaderIDs.Combined3, ShaderIDs.LowDepth2, new int?(ShaderIDs.Occlusion2), ShaderIDs.Combined2, this.GetSize(MultiScaleVO.MipLevel.L3), this.GetSize(MultiScaleVO.MipLevel.L2), isMSAA, false);
			this.PushUpsampleCommands(cmd, ShaderIDs.LowDepth2, ShaderIDs.Combined2, ShaderIDs.LowDepth1, new int?(ShaderIDs.Occlusion1), ShaderIDs.Combined1, this.GetSize(MultiScaleVO.MipLevel.L2), this.GetSize(MultiScaleVO.MipLevel.L1), isMSAA, false);
			this.PushUpsampleCommands(cmd, ShaderIDs.LowDepth1, ShaderIDs.Combined1, ShaderIDs.LinearDepth, null, destination, this.GetSize(MultiScaleVO.MipLevel.L1), this.GetSize(MultiScaleVO.MipLevel.Original), isMSAA, invert);
			this.PushReleaseCommands(cmd);
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00007EC4 File Offset: 0x000060C4
		private void PushAllocCommands(CommandBuffer cmd, bool isMSAA, Camera camera)
		{
			bool dynamicScale = RuntimeUtilities.IsDynamicResolutionEnabled(camera);
			if (isMSAA)
			{
				this.Alloc(cmd, ShaderIDs.LinearDepth, MultiScaleVO.MipLevel.Original, RenderTextureFormat.RGHalf, true, dynamicScale);
				this.Alloc(cmd, ShaderIDs.LowDepth1, MultiScaleVO.MipLevel.L1, RenderTextureFormat.RGFloat, true, dynamicScale);
				this.Alloc(cmd, ShaderIDs.LowDepth2, MultiScaleVO.MipLevel.L2, RenderTextureFormat.RGFloat, true, dynamicScale);
				this.Alloc(cmd, ShaderIDs.LowDepth3, MultiScaleVO.MipLevel.L3, RenderTextureFormat.RGFloat, true, dynamicScale);
				this.Alloc(cmd, ShaderIDs.LowDepth4, MultiScaleVO.MipLevel.L4, RenderTextureFormat.RGFloat, true, dynamicScale);
				this.AllocArray(cmd, ShaderIDs.TiledDepth1, MultiScaleVO.MipLevel.L3, RenderTextureFormat.RGHalf, true, dynamicScale);
				this.AllocArray(cmd, ShaderIDs.TiledDepth2, MultiScaleVO.MipLevel.L4, RenderTextureFormat.RGHalf, true, dynamicScale);
				this.AllocArray(cmd, ShaderIDs.TiledDepth3, MultiScaleVO.MipLevel.L5, RenderTextureFormat.RGHalf, true, dynamicScale);
				this.AllocArray(cmd, ShaderIDs.TiledDepth4, MultiScaleVO.MipLevel.L6, RenderTextureFormat.RGHalf, true, dynamicScale);
				this.Alloc(cmd, ShaderIDs.Occlusion1, MultiScaleVO.MipLevel.L1, RenderTextureFormat.RG16, true, dynamicScale);
				this.Alloc(cmd, ShaderIDs.Occlusion2, MultiScaleVO.MipLevel.L2, RenderTextureFormat.RG16, true, dynamicScale);
				this.Alloc(cmd, ShaderIDs.Occlusion3, MultiScaleVO.MipLevel.L3, RenderTextureFormat.RG16, true, dynamicScale);
				this.Alloc(cmd, ShaderIDs.Occlusion4, MultiScaleVO.MipLevel.L4, RenderTextureFormat.RG16, true, dynamicScale);
				this.Alloc(cmd, ShaderIDs.Combined1, MultiScaleVO.MipLevel.L1, RenderTextureFormat.RG16, true, dynamicScale);
				this.Alloc(cmd, ShaderIDs.Combined2, MultiScaleVO.MipLevel.L2, RenderTextureFormat.RG16, true, dynamicScale);
				this.Alloc(cmd, ShaderIDs.Combined3, MultiScaleVO.MipLevel.L3, RenderTextureFormat.RG16, true, dynamicScale);
				return;
			}
			this.Alloc(cmd, ShaderIDs.LinearDepth, MultiScaleVO.MipLevel.Original, this.m_R16Format, true, dynamicScale);
			this.Alloc(cmd, ShaderIDs.LowDepth1, MultiScaleVO.MipLevel.L1, RenderTextureFormat.RFloat, true, dynamicScale);
			this.Alloc(cmd, ShaderIDs.LowDepth2, MultiScaleVO.MipLevel.L2, RenderTextureFormat.RFloat, true, dynamicScale);
			this.Alloc(cmd, ShaderIDs.LowDepth3, MultiScaleVO.MipLevel.L3, RenderTextureFormat.RFloat, true, dynamicScale);
			this.Alloc(cmd, ShaderIDs.LowDepth4, MultiScaleVO.MipLevel.L4, RenderTextureFormat.RFloat, true, dynamicScale);
			this.AllocArray(cmd, ShaderIDs.TiledDepth1, MultiScaleVO.MipLevel.L3, this.m_R16Format, true, dynamicScale);
			this.AllocArray(cmd, ShaderIDs.TiledDepth2, MultiScaleVO.MipLevel.L4, this.m_R16Format, true, dynamicScale);
			this.AllocArray(cmd, ShaderIDs.TiledDepth3, MultiScaleVO.MipLevel.L5, this.m_R16Format, true, dynamicScale);
			this.AllocArray(cmd, ShaderIDs.TiledDepth4, MultiScaleVO.MipLevel.L6, this.m_R16Format, true, dynamicScale);
			this.Alloc(cmd, ShaderIDs.Occlusion1, MultiScaleVO.MipLevel.L1, this.m_R8Format, true, dynamicScale);
			this.Alloc(cmd, ShaderIDs.Occlusion2, MultiScaleVO.MipLevel.L2, this.m_R8Format, true, dynamicScale);
			this.Alloc(cmd, ShaderIDs.Occlusion3, MultiScaleVO.MipLevel.L3, this.m_R8Format, true, dynamicScale);
			this.Alloc(cmd, ShaderIDs.Occlusion4, MultiScaleVO.MipLevel.L4, this.m_R8Format, true, dynamicScale);
			this.Alloc(cmd, ShaderIDs.Combined1, MultiScaleVO.MipLevel.L1, this.m_R8Format, true, dynamicScale);
			this.Alloc(cmd, ShaderIDs.Combined2, MultiScaleVO.MipLevel.L2, this.m_R8Format, true, dynamicScale);
			this.Alloc(cmd, ShaderIDs.Combined3, MultiScaleVO.MipLevel.L3, this.m_R8Format, true, dynamicScale);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00008130 File Offset: 0x00006330
		private void PushDownsampleCommands(CommandBuffer cmd, Camera camera, RenderTargetIdentifier? depthMap, bool isMSAA)
		{
			bool flag = false;
			RenderTargetIdentifier renderTargetIdentifier;
			if (depthMap != null)
			{
				renderTargetIdentifier = depthMap.Value;
			}
			else if (!RuntimeUtilities.IsResolvedDepthAvailable(camera))
			{
				this.Alloc(cmd, ShaderIDs.DepthCopy, MultiScaleVO.MipLevel.Original, RenderTextureFormat.RFloat, false, RuntimeUtilities.IsDynamicResolutionEnabled(camera));
				renderTargetIdentifier = new RenderTargetIdentifier(ShaderIDs.DepthCopy);
				cmd.BlitFullscreenTriangle(BuiltinRenderTextureType.None, renderTargetIdentifier, this.m_PropertySheet, 0, false, null, false);
				flag = true;
			}
			else
			{
				renderTargetIdentifier = BuiltinRenderTextureType.ResolvedDepth;
			}
			ComputeShader computeShader = this.m_Resources.computeShaders.multiScaleAODownsample1;
			int kernelIndex = computeShader.FindKernel(isMSAA ? "MultiScaleVODownsample1_MSAA" : "MultiScaleVODownsample1");
			cmd.SetComputeTextureParam(computeShader, kernelIndex, "LinearZ", ShaderIDs.LinearDepth);
			cmd.SetComputeTextureParam(computeShader, kernelIndex, "DS2x", ShaderIDs.LowDepth1);
			cmd.SetComputeTextureParam(computeShader, kernelIndex, "DS2xAtlas", ShaderIDs.TiledDepth1);
			cmd.SetComputeVectorParam(computeShader, "ZBufferParams", this.CalculateZBufferParams(camera));
			cmd.SetComputeTextureParam(computeShader, kernelIndex, "Depth", renderTargetIdentifier);
			cmd.SetComputeTextureParam(computeShader, kernelIndex, "DS4x", ShaderIDs.LowDepth2);
			cmd.SetComputeTextureParam(computeShader, kernelIndex, "DS4xAtlas", ShaderIDs.TiledDepth2);
			cmd.DispatchCompute(computeShader, kernelIndex, this.m_ScaledWidths[4], this.m_ScaledHeights[4], 1);
			if (flag)
			{
				this.Release(cmd, ShaderIDs.DepthCopy);
			}
			computeShader = this.m_Resources.computeShaders.multiScaleAODownsample2;
			kernelIndex = (isMSAA ? computeShader.FindKernel("MultiScaleVODownsample2_MSAA") : computeShader.FindKernel("MultiScaleVODownsample2"));
			cmd.SetComputeTextureParam(computeShader, kernelIndex, "DS4x", ShaderIDs.LowDepth2);
			cmd.SetComputeTextureParam(computeShader, kernelIndex, "DS8x", ShaderIDs.LowDepth3);
			cmd.SetComputeTextureParam(computeShader, kernelIndex, "DS16x", ShaderIDs.LowDepth4);
			cmd.SetComputeTextureParam(computeShader, kernelIndex, "DS8xAtlas", ShaderIDs.TiledDepth3);
			cmd.SetComputeTextureParam(computeShader, kernelIndex, "DS16xAtlas", ShaderIDs.TiledDepth4);
			cmd.DispatchCompute(computeShader, kernelIndex, this.m_ScaledWidths[6], this.m_ScaledHeights[6], 1);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00008348 File Offset: 0x00006548
		private void PushRenderCommands(CommandBuffer cmd, int source, int destination, Vector3 sourceSize, float tanHalfFovH, bool isMSAA)
		{
			float num = 2f * tanHalfFovH * 10f / sourceSize.x;
			if (RuntimeUtilities.isSinglePassStereoEnabled)
			{
				num *= 2f;
			}
			float num2 = 1f / num;
			for (int i = 0; i < 12; i++)
			{
				this.m_InvThicknessTable[i] = num2 / this.m_SampleThickness[i];
			}
			this.m_SampleWeightTable[0] = 4f * this.m_SampleThickness[0];
			this.m_SampleWeightTable[1] = 4f * this.m_SampleThickness[1];
			this.m_SampleWeightTable[2] = 4f * this.m_SampleThickness[2];
			this.m_SampleWeightTable[3] = 4f * this.m_SampleThickness[3];
			this.m_SampleWeightTable[4] = 4f * this.m_SampleThickness[4];
			this.m_SampleWeightTable[5] = 8f * this.m_SampleThickness[5];
			this.m_SampleWeightTable[6] = 8f * this.m_SampleThickness[6];
			this.m_SampleWeightTable[7] = 8f * this.m_SampleThickness[7];
			this.m_SampleWeightTable[8] = 4f * this.m_SampleThickness[8];
			this.m_SampleWeightTable[9] = 8f * this.m_SampleThickness[9];
			this.m_SampleWeightTable[10] = 8f * this.m_SampleThickness[10];
			this.m_SampleWeightTable[11] = 4f * this.m_SampleThickness[11];
			this.m_SampleWeightTable[0] = 0f;
			this.m_SampleWeightTable[2] = 0f;
			this.m_SampleWeightTable[5] = 0f;
			this.m_SampleWeightTable[7] = 0f;
			this.m_SampleWeightTable[9] = 0f;
			float num3 = 0f;
			foreach (float num4 in this.m_SampleWeightTable)
			{
				num3 += num4;
			}
			for (int k = 0; k < this.m_SampleWeightTable.Length; k++)
			{
				this.m_SampleWeightTable[k] /= num3;
			}
			ComputeShader multiScaleAORender = this.m_Resources.computeShaders.multiScaleAORender;
			string text = isMSAA ? "MultiScaleVORender_MSAA_interleaved" : "MultiScaleVORender_interleaved";
			if (this.float4Texture)
			{
				text += "_Float4";
			}
			int kernelIndex = multiScaleAORender.FindKernel(text);
			cmd.SetComputeFloatParams(multiScaleAORender, "gInvThicknessTable", this.m_InvThicknessTable);
			cmd.SetComputeFloatParams(multiScaleAORender, "gSampleWeightTable", this.m_SampleWeightTable);
			cmd.SetComputeVectorParam(multiScaleAORender, "gInvSliceDimension", new Vector2(1f / sourceSize.x, 1f / sourceSize.y));
			cmd.SetComputeVectorParam(multiScaleAORender, "AdditionalParams", new Vector3(-1f / this.m_Settings.thicknessModifier.value, this.m_Settings.intensity.value, this.m_Settings.zBias.value));
			cmd.SetComputeTextureParam(multiScaleAORender, kernelIndex, "DepthTex", source);
			cmd.SetComputeTextureParam(multiScaleAORender, kernelIndex, "Occlusion", destination);
			uint num5;
			uint num6;
			uint num7;
			multiScaleAORender.GetKernelThreadGroupSizes(kernelIndex, out num5, out num6, out num7);
			cmd.DispatchCompute(multiScaleAORender, kernelIndex, ((int)sourceSize.x + (int)num5 - 1) / (int)num5, ((int)sourceSize.y + (int)num6 - 1) / (int)num6, ((int)sourceSize.z + (int)num7 - 1) / (int)num7);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x000086A0 File Offset: 0x000068A0
		private void PushUpsampleCommands(CommandBuffer cmd, int lowResDepth, int interleavedAO, int highResDepth, int? highResAO, RenderTargetIdentifier dest, Vector3 lowResDepthSize, Vector2 highResDepthSize, bool isMSAA, bool invert = false)
		{
			ComputeShader multiScaleAOUpsample = this.m_Resources.computeShaders.multiScaleAOUpsample;
			int kernelIndex;
			if (!isMSAA)
			{
				string text = (highResAO == null) ? (invert ? "MultiScaleVOUpSample_invert" : "MultiScaleVOUpSample") : "MultiScaleVOUpSample_blendout";
				if (this.float4Texture)
				{
					text += "_Float4";
				}
				kernelIndex = multiScaleAOUpsample.FindKernel(text);
			}
			else
			{
				string text2 = (highResAO == null) ? (invert ? "MultiScaleVOUpSample_MSAA_invert" : "MultiScaleVOUpSample_MSAA") : "MultiScaleVOUpSample_MSAA_blendout";
				if (this.float4Texture)
				{
					text2 += "_Float4";
				}
				kernelIndex = multiScaleAOUpsample.FindKernel(text2);
			}
			float num = 1920f / lowResDepthSize.x;
			float num2 = 1f - Mathf.Pow(10f, this.m_Settings.blurTolerance.value) * num;
			num2 *= num2;
			float num3 = Mathf.Pow(10f, this.m_Settings.upsampleTolerance.value);
			float x = 1f / (Mathf.Pow(10f, this.m_Settings.noiseFilterTolerance.value) + num3);
			cmd.SetComputeVectorParam(multiScaleAOUpsample, "InvLowResolution", new Vector2(1f / lowResDepthSize.x, 1f / lowResDepthSize.y));
			cmd.SetComputeVectorParam(multiScaleAOUpsample, "InvHighResolution", new Vector2(1f / highResDepthSize.x, 1f / highResDepthSize.y));
			cmd.SetComputeVectorParam(multiScaleAOUpsample, "AdditionalParams", new Vector4(x, num, num2, num3));
			cmd.SetComputeTextureParam(multiScaleAOUpsample, kernelIndex, "LoResDB", lowResDepth);
			cmd.SetComputeTextureParam(multiScaleAOUpsample, kernelIndex, "HiResDB", highResDepth);
			cmd.SetComputeTextureParam(multiScaleAOUpsample, kernelIndex, "LoResAO1", interleavedAO);
			if (highResAO != null)
			{
				cmd.SetComputeTextureParam(multiScaleAOUpsample, kernelIndex, "HiResAO", highResAO.Value);
			}
			cmd.SetComputeTextureParam(multiScaleAOUpsample, kernelIndex, "AoResult", dest);
			int threadGroupsX = ((int)highResDepthSize.x + 17) / 16;
			int threadGroupsY = ((int)highResDepthSize.y + 17) / 16;
			cmd.DispatchCompute(multiScaleAOUpsample, kernelIndex, threadGroupsX, threadGroupsY, 1);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x000088D0 File Offset: 0x00006AD0
		private void PushReleaseCommands(CommandBuffer cmd)
		{
			this.Release(cmd, ShaderIDs.LinearDepth);
			this.Release(cmd, ShaderIDs.LowDepth1);
			this.Release(cmd, ShaderIDs.LowDepth2);
			this.Release(cmd, ShaderIDs.LowDepth3);
			this.Release(cmd, ShaderIDs.LowDepth4);
			this.Release(cmd, ShaderIDs.TiledDepth1);
			this.Release(cmd, ShaderIDs.TiledDepth2);
			this.Release(cmd, ShaderIDs.TiledDepth3);
			this.Release(cmd, ShaderIDs.TiledDepth4);
			this.Release(cmd, ShaderIDs.Occlusion1);
			this.Release(cmd, ShaderIDs.Occlusion2);
			this.Release(cmd, ShaderIDs.Occlusion3);
			this.Release(cmd, ShaderIDs.Occlusion4);
			this.Release(cmd, ShaderIDs.Combined1);
			this.Release(cmd, ShaderIDs.Combined2);
			this.Release(cmd, ShaderIDs.Combined3);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x000089A0 File Offset: 0x00006BA0
		private void PreparePropertySheet(PostProcessRenderContext context)
		{
			PropertySheet propertySheet = context.propertySheets.Get(this.m_Resources.shaders.multiScaleAO);
			propertySheet.ClearKeywords();
			propertySheet.properties.SetVector(ShaderIDs.AOColor, Color.white - this.m_Settings.color.value);
			this.m_PropertySheet = propertySheet;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00008A08 File Offset: 0x00006C08
		private void CheckAOTexture(PostProcessRenderContext context)
		{
			bool flag = this.m_AmbientOnlyAO == null || !this.m_AmbientOnlyAO.IsCreated() || this.m_AmbientOnlyAO.width != context.width || this.m_AmbientOnlyAO.height != context.height;
			bool flag2 = RuntimeUtilities.IsDynamicResolutionEnabled(context.camera);
			if (flag || this.m_AmbientOnlyAO.useDynamicScale != flag2)
			{
				RuntimeUtilities.Destroy(this.m_AmbientOnlyAO);
				this.m_AmbientOnlyAO = new RenderTexture(context.width, context.height, 0, this.m_R8Format, RenderTextureReadWrite.Linear)
				{
					hideFlags = HideFlags.DontSave,
					filterMode = FilterMode.Point,
					enableRandomWrite = true,
					useDynamicScale = flag2
				};
				this.m_AmbientOnlyAO.Create();
			}
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00008AD3 File Offset: 0x00006CD3
		private void PushDebug(PostProcessRenderContext context)
		{
			if (context.IsDebugOverlayEnabled(DebugOverlay.AmbientOcclusion))
			{
				context.PushDebugOverlay(context.command, this.m_AmbientOnlyAO, this.m_PropertySheet, 3);
			}
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00008AFC File Offset: 0x00006CFC
		public void RenderAfterOpaque(PostProcessRenderContext context)
		{
			CommandBuffer command = context.command;
			command.BeginSample("Ambient Occlusion");
			this.SetResources(context.resources);
			this.PreparePropertySheet(context);
			this.CheckAOTexture(context);
			if (context.camera.actualRenderingPath == RenderingPath.Forward && RenderSettings.fog)
			{
				this.m_PropertySheet.EnableKeyword("APPLY_FORWARD_FOG");
				this.m_PropertySheet.properties.SetVector(ShaderIDs.FogParams, new Vector3(RenderSettings.fogDensity, RenderSettings.fogStartDistance, RenderSettings.fogEndDistance));
			}
			this.GenerateAOMap(command, context.camera, this.m_AmbientOnlyAO, null, false, false);
			this.PushDebug(context);
			command.SetGlobalTexture(ShaderIDs.MSVOcclusionTexture, this.m_AmbientOnlyAO);
			command.BlitFullscreenTriangle(BuiltinRenderTextureType.None, BuiltinRenderTextureType.CameraTarget, this.m_PropertySheet, 2, RenderBufferLoadAction.Load, null, false);
			command.EndSample("Ambient Occlusion");
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00008BF8 File Offset: 0x00006DF8
		public void RenderAmbientOnly(PostProcessRenderContext context)
		{
			CommandBuffer command = context.command;
			command.BeginSample("Ambient Occlusion Render");
			this.SetResources(context.resources);
			this.PreparePropertySheet(context);
			this.CheckAOTexture(context);
			this.GenerateAOMap(command, context.camera, this.m_AmbientOnlyAO, null, false, false);
			this.PushDebug(context);
			command.EndSample("Ambient Occlusion Render");
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00008C68 File Offset: 0x00006E68
		public void CompositeAmbientOnly(PostProcessRenderContext context)
		{
			CommandBuffer command = context.command;
			command.BeginSample("Ambient Occlusion Composite");
			command.SetGlobalTexture(ShaderIDs.MSVOcclusionTexture, this.m_AmbientOnlyAO);
			command.BlitFullscreenTriangle(BuiltinRenderTextureType.None, this.m_MRT, BuiltinRenderTextureType.CameraTarget, this.m_PropertySheet, 1, false, null);
			command.EndSample("Ambient Occlusion Composite");
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00008CCF File Offset: 0x00006ECF
		public void Release()
		{
			RuntimeUtilities.Destroy(this.m_AmbientOnlyAO);
			this.m_AmbientOnlyAO = null;
		}

		// Token: 0x040000FF RID: 255
		private readonly float[] m_SampleThickness = new float[]
		{
			Mathf.Sqrt(0.96f),
			Mathf.Sqrt(0.84f),
			Mathf.Sqrt(0.64f),
			Mathf.Sqrt(0.35999995f),
			Mathf.Sqrt(0.91999996f),
			Mathf.Sqrt(0.79999995f),
			Mathf.Sqrt(0.59999996f),
			Mathf.Sqrt(0.31999993f),
			Mathf.Sqrt(0.67999995f),
			Mathf.Sqrt(0.47999996f),
			Mathf.Sqrt(0.19999993f),
			Mathf.Sqrt(0.27999997f)
		};

		// Token: 0x04000100 RID: 256
		private readonly float[] m_InvThicknessTable = new float[12];

		// Token: 0x04000101 RID: 257
		private readonly float[] m_SampleWeightTable = new float[12];

		// Token: 0x04000102 RID: 258
		private readonly int[] m_Widths = new int[7];

		// Token: 0x04000103 RID: 259
		private readonly int[] m_Heights = new int[7];

		// Token: 0x04000104 RID: 260
		private readonly int[] m_ScaledWidths = new int[7];

		// Token: 0x04000105 RID: 261
		private readonly int[] m_ScaledHeights = new int[7];

		// Token: 0x04000106 RID: 262
		private AmbientOcclusion m_Settings;

		// Token: 0x04000107 RID: 263
		private PropertySheet m_PropertySheet;

		// Token: 0x04000108 RID: 264
		private PostProcessResources m_Resources;

		// Token: 0x04000109 RID: 265
		private RenderTexture m_AmbientOnlyAO;

		// Token: 0x0400010A RID: 266
		private RenderTextureFormat m_R8Format;

		// Token: 0x0400010B RID: 267
		private RenderTextureFormat m_R16Format;

		// Token: 0x0400010C RID: 268
		private bool float4Texture;

		// Token: 0x0400010D RID: 269
		private readonly RenderTargetIdentifier[] m_MRT = new RenderTargetIdentifier[]
		{
			BuiltinRenderTextureType.GBuffer0,
			BuiltinRenderTextureType.CameraTarget
		};

		// Token: 0x0200007B RID: 123
		internal enum MipLevel
		{
			// Token: 0x04000301 RID: 769
			Original,
			// Token: 0x04000302 RID: 770
			L1,
			// Token: 0x04000303 RID: 771
			L2,
			// Token: 0x04000304 RID: 772
			L3,
			// Token: 0x04000305 RID: 773
			L4,
			// Token: 0x04000306 RID: 774
			L5,
			// Token: 0x04000307 RID: 775
			L6
		}

		// Token: 0x0200007C RID: 124
		private enum Pass
		{
			// Token: 0x04000309 RID: 777
			DepthCopy,
			// Token: 0x0400030A RID: 778
			CompositionDeferred,
			// Token: 0x0400030B RID: 779
			CompositionForward,
			// Token: 0x0400030C RID: 780
			DebugOverlay
		}
	}
}
