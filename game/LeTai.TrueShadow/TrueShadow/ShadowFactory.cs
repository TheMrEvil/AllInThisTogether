using System;
using System.Collections.Generic;
using LeTai.Effects;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

namespace LeTai.TrueShadow
{
	// Token: 0x0200000D RID: 13
	public class ShadowFactory
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00003310 File Offset: 0x00001510
		public static ShadowFactory Instance
		{
			get
			{
				ShadowFactory result;
				if ((result = ShadowFactory.instance) == null)
				{
					result = (ShadowFactory.instance = new ShadowFactory());
				}
				return result;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00003326 File Offset: 0x00001526
		public int CachedCount
		{
			get
			{
				return this.shadowCache.Count;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00003334 File Offset: 0x00001534
		private Material CutoutMaterial
		{
			get
			{
				if (!this.cutoutMaterial)
				{
					return this.cutoutMaterial = new Material(Shader.Find("Hidden/TrueShadow/Cutout"));
				}
				return this.cutoutMaterial;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00003370 File Offset: 0x00001570
		private Material ImprintPostProcessMaterial
		{
			get
			{
				if (!this.imprintPostProcessMaterial)
				{
					return this.imprintPostProcessMaterial = new Material(Shader.Find("Hidden/TrueShadow/ImprintPostProcess"));
				}
				return this.imprintPostProcessMaterial;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000064 RID: 100 RVA: 0x000033AC File Offset: 0x000015AC
		private Material ShadowPostProcessMaterial
		{
			get
			{
				if (!this.shadowPostProcessMaterial)
				{
					return this.shadowPostProcessMaterial = new Material(Shader.Find("Hidden/TrueShadow/PostProcess"));
				}
				return this.shadowPostProcessMaterial;
			}
		}

		// Token: 0x06000065 RID: 101 RVA: 0x000033E8 File Offset: 0x000015E8
		private ShadowFactory()
		{
			this.cmd = new CommandBuffer
			{
				name = "Shadow Commands"
			};
			this.materialProps = new MaterialPropertyBlock();
			this.materialProps.SetVector(ShaderId.CLIP_RECT, new Vector4(float.NegativeInfinity, float.NegativeInfinity, float.PositiveInfinity, float.PositiveInfinity));
			this.materialProps.SetInt(ShaderId.COLOR_MASK, 15);
			ShaderProperties.Init(8);
			this.blurConfig = ScriptableObject.CreateInstance<ScalableBlurConfig>();
			this.blurConfig.hideFlags = HideFlags.HideAndDontSave;
			this.blurProcessor = new ScalableBlur();
			this.blurProcessor.Configure(this.blurConfig);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x0000349C File Offset: 0x0000169C
		internal void Get(ShadowSettingSnapshot snapshot, ref ShadowContainer container)
		{
			if (float.IsNaN(snapshot.dimensions.x) || snapshot.dimensions.x < 1f || float.IsNaN(snapshot.dimensions.y) || snapshot.dimensions.y < 1f)
			{
				this.ReleaseContainer(ref container);
				return;
			}
			float num = snapshot.size * 2f;
			if (snapshot.dimensions.x + num > 4097f || snapshot.dimensions.y + num > 4097f)
			{
				Debug.LogWarning("Requested Shadow is too large");
				return;
			}
			int hashCode = snapshot.GetHashCode();
			ShadowContainer shadowContainer = container;
			if (shadowContainer != null && shadowContainer.requestHash == hashCode)
			{
				return;
			}
			this.ReleaseContainer(ref container);
			ShadowContainer shadowContainer2;
			if (this.shadowCache.TryGetValue(hashCode, out shadowContainer2))
			{
				ShadowContainer shadowContainer3 = shadowContainer2;
				int refCount = shadowContainer3.RefCount;
				shadowContainer3.RefCount = refCount + 1;
				container = shadowContainer2;
				return;
			}
			container = (this.shadowCache[hashCode] = this.GenerateShadow(snapshot));
		}

		// Token: 0x06000067 RID: 103 RVA: 0x0000359C File Offset: 0x0000179C
		internal void ReleaseContainer(ref ShadowContainer container)
		{
			if (container == null)
			{
				return;
			}
			ShadowContainer shadowContainer = container;
			int num = shadowContainer.RefCount - 1;
			shadowContainer.RefCount = num;
			if (num > 0)
			{
				return;
			}
			RenderTexture.ReleaseTemporary(container.Texture);
			this.shadowCache.Remove(container.requestHash);
			container = null;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x000035E8 File Offset: 0x000017E8
		private ShadowContainer GenerateShadow(ShadowSettingSnapshot snapshot)
		{
			this.cmd.Clear();
			this.cmd.BeginSample("TrueShadow:Capture");
			Bounds bounds = snapshot.shadow.SpriteMesh.bounds;
			int num = snapshot.shadow.Inset ? Math.Max(Mathf.CeilToInt(Mathf.Max(Mathf.Abs(snapshot.canvasRelativeOffset.x), Mathf.Abs(snapshot.canvasRelativeOffset.y))), (this.blurConfig.Iteration + 1) * (this.blurConfig.Iteration + 1)) : Mathf.CeilToInt(snapshot.size);
			Vector2Int vector2Int = Vector2Int.CeilToInt(snapshot.dimensions);
			int num2 = vector2Int.x;
			int num3 = vector2Int.y;
			int num4 = num * 2;
			num2 += num4;
			num3 += num4;
			RenderTexture temporary = RenderTexture.GetTemporary(num2, num3, 0, RenderTextureFormat.ARGB32);
			RenderTextureDescriptor descriptor = temporary.descriptor;
			RenderTexture temporary2 = RenderTexture.GetTemporary(descriptor);
			RenderTexture renderTexture = null;
			bool flag = snapshot.shadow.IgnoreCasterColor || snapshot.shadow.Inset;
			if (flag)
			{
				renderTexture = RenderTexture.GetTemporary(descriptor);
			}
			Texture content = snapshot.shadow.Content;
			if (content)
			{
				this.materialProps.SetTexture(ShaderId.MAIN_TEX, content);
				Texture2D texture2D = content as Texture2D;
				if (texture2D != null && texture2D.format == TextureFormat.Alpha8)
				{
					this.materialProps.SetVector(ShaderId.TEXTURE_SAMPLE_ADD, ShadowFactory.ALPHA8_TEXTURE_BIAS);
				}
				else
				{
					this.materialProps.SetVector(ShaderId.TEXTURE_SAMPLE_ADD, Vector4.zero);
				}
			}
			else
			{
				this.materialProps.SetTexture(ShaderId.MAIN_TEX, Texture2D.whiteTexture);
			}
			this.cmd.SetRenderTarget(temporary2);
			this.cmd.ClearRenderTarget(true, true, snapshot.shadow.ClearColor);
			this.cmd.SetViewport(new Rect(new Vector2((float)num, (float)num), vector2Int));
			this.cmd.SetViewProjectionMatrices(Matrix4x4.identity, Matrix4x4.Ortho(bounds.min.x, bounds.max.x, bounds.min.y, bounds.max.y, -1f, 1f));
			this.materialProps.SetVector(ShaderId.SCREEN_PARAMS, new Vector4((float)num2, (float)num3, 1f + 1f / (float)num2, 1f + 1f / (float)num3));
			if (snapshot.shadow.Graphic is TextMeshProUGUI || snapshot.shadow.Graphic is TMP_SubMeshUI)
			{
				Vector3 lossyScale = snapshot.canvas.transform.lossyScale;
				this.materialProps.SetFloat(ShaderId.SCALE_X, 1f / lossyScale.x);
				this.materialProps.SetFloat(ShaderId.SCALE_Y, 1f / lossyScale.y);
			}
			snapshot.shadow.ModifyShadowCastingMesh(snapshot.shadow.SpriteMesh);
			snapshot.shadow.ModifyShadowCastingMaterialProperties(this.materialProps);
			this.cmd.DrawMesh(snapshot.shadow.SpriteMesh, Matrix4x4.identity, snapshot.shadow.GetShadowCastingMaterial(), 0, 0, this.materialProps);
			if (flag)
			{
				this.ImprintPostProcessMaterial.SetKeyword("BLEACH", snapshot.shadow.IgnoreCasterColor);
				this.ImprintPostProcessMaterial.SetKeyword("INSET", snapshot.shadow.Inset);
				this.cmd.Blit(temporary2, renderTexture, this.ImprintPostProcessMaterial);
			}
			this.cmd.EndSample("TrueShadow:Capture");
			bool flag2 = (double)snapshot.shadow.Spread > 0.001;
			this.cmd.BeginSample("TrueShadow:Cast");
			RenderTexture renderTexture2 = flag ? renderTexture : temporary2;
			RenderTexture renderTexture3;
			if (flag2)
			{
				renderTexture3 = RenderTexture.GetTemporary(temporary.descriptor);
			}
			else
			{
				renderTexture3 = temporary;
			}
			if ((double)snapshot.size < 0.01)
			{
				this.cmd.Blit(renderTexture2, renderTexture3);
			}
			else
			{
				this.blurConfig.Strength = snapshot.size;
				this.blurProcessor.Blur(this.cmd, renderTexture2, ShadowFactory.UNIT_RECT, renderTexture3);
			}
			this.cmd.EndSample("TrueShadow:Cast");
			Vector2 v = new Vector2(snapshot.canvasRelativeOffset.x / (float)num2, snapshot.canvasRelativeOffset.y / (float)num3);
			int num5 = snapshot.shadow.Inset ? 1 : 0;
			if (flag2)
			{
				this.cmd.BeginSample("TrueShadow:PostProcess");
				this.ShadowPostProcessMaterial.SetTexture(ShaderId.SHADOW_TEX, renderTexture3);
				this.ShadowPostProcessMaterial.SetVector(ShaderId.OFFSET, v);
				this.ShadowPostProcessMaterial.SetFloat(ShaderId.OVERFLOW_ALPHA, (float)num5);
				this.ShadowPostProcessMaterial.SetFloat(ShaderId.ALPHA_MULTIPLIER, 1f / Mathf.Max(1.5259022E-05f, 1f - snapshot.shadow.Spread));
				this.cmd.SetViewport(ShadowFactory.UNIT_RECT);
				this.cmd.Blit(renderTexture2, temporary, this.ShadowPostProcessMaterial);
				this.cmd.EndSample("TrueShadow:PostProcess");
			}
			else if (snapshot.shadow.Cutout)
			{
				this.cmd.BeginSample("TrueShadow:Cutout");
				this.CutoutMaterial.SetVector(ShaderId.OFFSET, v);
				this.CutoutMaterial.SetFloat(ShaderId.OVERFLOW_ALPHA, (float)num5);
				this.cmd.SetViewport(ShadowFactory.UNIT_RECT);
				this.cmd.Blit(renderTexture2, temporary, this.CutoutMaterial);
				this.cmd.EndSample("TrueShadow:Cutout");
			}
			Graphics.ExecuteCommandBuffer(this.cmd);
			RenderTexture.ReleaseTemporary(temporary2);
			RenderTexture.ReleaseTemporary(renderTexture2);
			if (flag2)
			{
				RenderTexture.ReleaseTemporary(renderTexture3);
			}
			return new ShadowContainer(temporary, snapshot, num, vector2Int);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003BD4 File Offset: 0x00001DD4
		private RenderTexture GenColoredTexture(int hash)
		{
			Texture2D texture2D = new Texture2D(1, 1);
			texture2D.SetPixels32(new Color32[]
			{
				new Color32((byte)(hash >> 8), (byte)(hash >> 16), (byte)(hash >> 24), byte.MaxValue)
			});
			texture2D.Apply();
			RenderTexture temporary = RenderTexture.GetTemporary(1, 1);
			Graphics.Blit(texture2D, temporary);
			return temporary;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003C2A File Offset: 0x00001E2A
		// Note: this type is marked as 'beforefieldinit'.
		static ShadowFactory()
		{
		}

		// Token: 0x0400004A RID: 74
		private static ShadowFactory instance;

		// Token: 0x0400004B RID: 75
		private readonly Dictionary<int, ShadowContainer> shadowCache = new Dictionary<int, ShadowContainer>();

		// Token: 0x0400004C RID: 76
		private readonly CommandBuffer cmd;

		// Token: 0x0400004D RID: 77
		private readonly MaterialPropertyBlock materialProps;

		// Token: 0x0400004E RID: 78
		private readonly ScalableBlur blurProcessor;

		// Token: 0x0400004F RID: 79
		private readonly ScalableBlurConfig blurConfig;

		// Token: 0x04000050 RID: 80
		private Material cutoutMaterial;

		// Token: 0x04000051 RID: 81
		private Material imprintPostProcessMaterial;

		// Token: 0x04000052 RID: 82
		private Material shadowPostProcessMaterial;

		// Token: 0x04000053 RID: 83
		private static readonly Rect UNIT_RECT = new Rect(0f, 0f, 1f, 1f);

		// Token: 0x04000054 RID: 84
		private static readonly Vector4 ALPHA8_TEXTURE_BIAS = new Vector4(1f, 1f, 1f, 0f);
	}
}
