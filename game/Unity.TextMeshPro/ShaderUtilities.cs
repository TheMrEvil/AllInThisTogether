using System;
using System.Linq;
using UnityEngine;

namespace TMPro
{
	// Token: 0x02000050 RID: 80
	public static class ShaderUtilities
	{
		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000392 RID: 914 RVA: 0x00024E4B File Offset: 0x0002304B
		internal static Shader ShaderRef_MobileSDF
		{
			get
			{
				if (ShaderUtilities.k_ShaderRef_MobileSDF == null)
				{
					ShaderUtilities.k_ShaderRef_MobileSDF = Shader.Find("TextMeshPro/Mobile/Distance Field");
				}
				return ShaderUtilities.k_ShaderRef_MobileSDF;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000393 RID: 915 RVA: 0x00024E6E File Offset: 0x0002306E
		internal static Shader ShaderRef_MobileBitmap
		{
			get
			{
				if (ShaderUtilities.k_ShaderRef_MobileBitmap == null)
				{
					ShaderUtilities.k_ShaderRef_MobileBitmap = Shader.Find("TextMeshPro/Mobile/Bitmap");
				}
				return ShaderUtilities.k_ShaderRef_MobileBitmap;
			}
		}

		// Token: 0x06000394 RID: 916 RVA: 0x00024E94 File Offset: 0x00023094
		static ShaderUtilities()
		{
			ShaderUtilities.GetShaderPropertyIDs();
		}

		// Token: 0x06000395 RID: 917 RVA: 0x00024F1C File Offset: 0x0002311C
		public static void GetShaderPropertyIDs()
		{
			if (!ShaderUtilities.isInitialized)
			{
				ShaderUtilities.isInitialized = true;
				ShaderUtilities.ID_MainTex = Shader.PropertyToID("_MainTex");
				ShaderUtilities.ID_FaceTex = Shader.PropertyToID("_FaceTex");
				ShaderUtilities.ID_FaceColor = Shader.PropertyToID("_FaceColor");
				ShaderUtilities.ID_FaceDilate = Shader.PropertyToID("_FaceDilate");
				ShaderUtilities.ID_Shininess = Shader.PropertyToID("_FaceShininess");
				ShaderUtilities.ID_UnderlayColor = Shader.PropertyToID("_UnderlayColor");
				ShaderUtilities.ID_UnderlayOffsetX = Shader.PropertyToID("_UnderlayOffsetX");
				ShaderUtilities.ID_UnderlayOffsetY = Shader.PropertyToID("_UnderlayOffsetY");
				ShaderUtilities.ID_UnderlayDilate = Shader.PropertyToID("_UnderlayDilate");
				ShaderUtilities.ID_UnderlaySoftness = Shader.PropertyToID("_UnderlaySoftness");
				ShaderUtilities.ID_UnderlayOffset = Shader.PropertyToID("_UnderlayOffset");
				ShaderUtilities.ID_UnderlayIsoPerimeter = Shader.PropertyToID("_UnderlayIsoPerimeter");
				ShaderUtilities.ID_WeightNormal = Shader.PropertyToID("_WeightNormal");
				ShaderUtilities.ID_WeightBold = Shader.PropertyToID("_WeightBold");
				ShaderUtilities.ID_OutlineTex = Shader.PropertyToID("_OutlineTex");
				ShaderUtilities.ID_OutlineWidth = Shader.PropertyToID("_OutlineWidth");
				ShaderUtilities.ID_OutlineSoftness = Shader.PropertyToID("_OutlineSoftness");
				ShaderUtilities.ID_OutlineColor = Shader.PropertyToID("_OutlineColor");
				ShaderUtilities.ID_Outline2Color = Shader.PropertyToID("_Outline2Color");
				ShaderUtilities.ID_Outline2Width = Shader.PropertyToID("_Outline2Width");
				ShaderUtilities.ID_Padding = Shader.PropertyToID("_Padding");
				ShaderUtilities.ID_GradientScale = Shader.PropertyToID("_GradientScale");
				ShaderUtilities.ID_ScaleX = Shader.PropertyToID("_ScaleX");
				ShaderUtilities.ID_ScaleY = Shader.PropertyToID("_ScaleY");
				ShaderUtilities.ID_PerspectiveFilter = Shader.PropertyToID("_PerspectiveFilter");
				ShaderUtilities.ID_Sharpness = Shader.PropertyToID("_Sharpness");
				ShaderUtilities.ID_TextureWidth = Shader.PropertyToID("_TextureWidth");
				ShaderUtilities.ID_TextureHeight = Shader.PropertyToID("_TextureHeight");
				ShaderUtilities.ID_BevelAmount = Shader.PropertyToID("_Bevel");
				ShaderUtilities.ID_LightAngle = Shader.PropertyToID("_LightAngle");
				ShaderUtilities.ID_EnvMap = Shader.PropertyToID("_Cube");
				ShaderUtilities.ID_EnvMatrix = Shader.PropertyToID("_EnvMatrix");
				ShaderUtilities.ID_EnvMatrixRotation = Shader.PropertyToID("_EnvMatrixRotation");
				ShaderUtilities.ID_GlowColor = Shader.PropertyToID("_GlowColor");
				ShaderUtilities.ID_GlowOffset = Shader.PropertyToID("_GlowOffset");
				ShaderUtilities.ID_GlowPower = Shader.PropertyToID("_GlowPower");
				ShaderUtilities.ID_GlowOuter = Shader.PropertyToID("_GlowOuter");
				ShaderUtilities.ID_GlowInner = Shader.PropertyToID("_GlowInner");
				ShaderUtilities.ID_MaskCoord = Shader.PropertyToID("_MaskCoord");
				ShaderUtilities.ID_ClipRect = Shader.PropertyToID("_ClipRect");
				ShaderUtilities.ID_UseClipRect = Shader.PropertyToID("_UseClipRect");
				ShaderUtilities.ID_MaskSoftnessX = Shader.PropertyToID("_MaskSoftnessX");
				ShaderUtilities.ID_MaskSoftnessY = Shader.PropertyToID("_MaskSoftnessY");
				ShaderUtilities.ID_VertexOffsetX = Shader.PropertyToID("_VertexOffsetX");
				ShaderUtilities.ID_VertexOffsetY = Shader.PropertyToID("_VertexOffsetY");
				ShaderUtilities.ID_StencilID = Shader.PropertyToID("_Stencil");
				ShaderUtilities.ID_StencilOp = Shader.PropertyToID("_StencilOp");
				ShaderUtilities.ID_StencilComp = Shader.PropertyToID("_StencilComp");
				ShaderUtilities.ID_StencilReadMask = Shader.PropertyToID("_StencilReadMask");
				ShaderUtilities.ID_StencilWriteMask = Shader.PropertyToID("_StencilWriteMask");
				ShaderUtilities.ID_ShaderFlags = Shader.PropertyToID("_ShaderFlags");
				ShaderUtilities.ID_ScaleRatio_A = Shader.PropertyToID("_ScaleRatioA");
				ShaderUtilities.ID_ScaleRatio_B = Shader.PropertyToID("_ScaleRatioB");
				ShaderUtilities.ID_ScaleRatio_C = Shader.PropertyToID("_ScaleRatioC");
				if (ShaderUtilities.k_ShaderRef_MobileSDF == null)
				{
					ShaderUtilities.k_ShaderRef_MobileSDF = Shader.Find("TextMeshPro/Mobile/Distance Field");
				}
				if (ShaderUtilities.k_ShaderRef_MobileBitmap == null)
				{
					ShaderUtilities.k_ShaderRef_MobileBitmap = Shader.Find("TextMeshPro/Mobile/Bitmap");
				}
			}
		}

		// Token: 0x06000396 RID: 918 RVA: 0x0002529C File Offset: 0x0002349C
		public static void UpdateShaderRatios(Material mat)
		{
			bool flag = !mat.shaderKeywords.Contains(ShaderUtilities.Keyword_Ratios);
			if (!mat.HasProperty(ShaderUtilities.ID_GradientScale) || !mat.HasProperty(ShaderUtilities.ID_FaceDilate))
			{
				return;
			}
			float @float = mat.GetFloat(ShaderUtilities.ID_GradientScale);
			float float2 = mat.GetFloat(ShaderUtilities.ID_FaceDilate);
			float float3 = mat.GetFloat(ShaderUtilities.ID_OutlineWidth);
			float float4 = mat.GetFloat(ShaderUtilities.ID_OutlineSoftness);
			float num = Mathf.Max(mat.GetFloat(ShaderUtilities.ID_WeightNormal), mat.GetFloat(ShaderUtilities.ID_WeightBold)) / 4f;
			float num2 = Mathf.Max(1f, num + float2 + float3 + float4);
			float value = flag ? ((@float - ShaderUtilities.m_clamp) / (@float * num2)) : 1f;
			mat.SetFloat(ShaderUtilities.ID_ScaleRatio_A, value);
			if (mat.HasProperty(ShaderUtilities.ID_GlowOffset))
			{
				float float5 = mat.GetFloat(ShaderUtilities.ID_GlowOffset);
				float float6 = mat.GetFloat(ShaderUtilities.ID_GlowOuter);
				float num3 = (num + float2) * (@float - ShaderUtilities.m_clamp);
				num2 = Mathf.Max(1f, float5 + float6);
				float value2 = flag ? (Mathf.Max(0f, @float - ShaderUtilities.m_clamp - num3) / (@float * num2)) : 1f;
				mat.SetFloat(ShaderUtilities.ID_ScaleRatio_B, value2);
			}
			if (mat.HasProperty(ShaderUtilities.ID_UnderlayOffsetX))
			{
				float float7 = mat.GetFloat(ShaderUtilities.ID_UnderlayOffsetX);
				float float8 = mat.GetFloat(ShaderUtilities.ID_UnderlayOffsetY);
				float float9 = mat.GetFloat(ShaderUtilities.ID_UnderlayDilate);
				float float10 = mat.GetFloat(ShaderUtilities.ID_UnderlaySoftness);
				float num4 = (num + float2) * (@float - ShaderUtilities.m_clamp);
				num2 = Mathf.Max(1f, Mathf.Max(Mathf.Abs(float7), Mathf.Abs(float8)) + float9 + float10);
				float value3 = flag ? (Mathf.Max(0f, @float - ShaderUtilities.m_clamp - num4) / (@float * num2)) : 1f;
				mat.SetFloat(ShaderUtilities.ID_ScaleRatio_C, value3);
			}
		}

		// Token: 0x06000397 RID: 919 RVA: 0x000254A2 File Offset: 0x000236A2
		public static Vector4 GetFontExtent(Material material)
		{
			return Vector4.zero;
		}

		// Token: 0x06000398 RID: 920 RVA: 0x000254AC File Offset: 0x000236AC
		public static bool IsMaskingEnabled(Material material)
		{
			return !(material == null) && material.HasProperty(ShaderUtilities.ID_ClipRect) && (material.shaderKeywords.Contains(ShaderUtilities.Keyword_MASK_SOFT) || material.shaderKeywords.Contains(ShaderUtilities.Keyword_MASK_HARD) || material.shaderKeywords.Contains(ShaderUtilities.Keyword_MASK_TEX));
		}

		// Token: 0x06000399 RID: 921 RVA: 0x0002550C File Offset: 0x0002370C
		public static float GetPadding(Material material, bool enableExtraPadding, bool isBold)
		{
			if (!ShaderUtilities.isInitialized)
			{
				ShaderUtilities.GetShaderPropertyIDs();
			}
			if (material == null)
			{
				return 0f;
			}
			int num = enableExtraPadding ? 4 : 0;
			if (!material.HasProperty(ShaderUtilities.ID_GradientScale))
			{
				if (material.HasProperty(ShaderUtilities.ID_Padding))
				{
					num += (int)material.GetFloat(ShaderUtilities.ID_Padding);
				}
				return (float)num + 1f;
			}
			Vector4 vector = Vector4.zero;
			Vector4 zero = Vector4.zero;
			float num2 = 0f;
			float num3 = 0f;
			float num4 = 0f;
			float num5 = 0f;
			float num6 = 0f;
			float num7 = 0f;
			float num8 = 0f;
			float num9 = 0f;
			ShaderUtilities.UpdateShaderRatios(material);
			string[] shaderKeywords = material.shaderKeywords;
			if (material.HasProperty(ShaderUtilities.ID_ScaleRatio_A))
			{
				num5 = material.GetFloat(ShaderUtilities.ID_ScaleRatio_A);
			}
			if (material.HasProperty(ShaderUtilities.ID_FaceDilate))
			{
				num2 = material.GetFloat(ShaderUtilities.ID_FaceDilate) * num5;
			}
			if (material.HasProperty(ShaderUtilities.ID_OutlineSoftness))
			{
				num3 = material.GetFloat(ShaderUtilities.ID_OutlineSoftness) * num5;
			}
			if (material.HasProperty(ShaderUtilities.ID_OutlineWidth))
			{
				num4 = material.GetFloat(ShaderUtilities.ID_OutlineWidth) * num5;
			}
			float num10 = num4 + num3 + num2;
			if (material.HasProperty(ShaderUtilities.ID_GlowOffset) && shaderKeywords.Contains(ShaderUtilities.Keyword_Glow))
			{
				if (material.HasProperty(ShaderUtilities.ID_ScaleRatio_B))
				{
					num6 = material.GetFloat(ShaderUtilities.ID_ScaleRatio_B);
				}
				num8 = material.GetFloat(ShaderUtilities.ID_GlowOffset) * num6;
				num9 = material.GetFloat(ShaderUtilities.ID_GlowOuter) * num6;
			}
			num10 = Mathf.Max(num10, num2 + num8 + num9);
			if (material.HasProperty(ShaderUtilities.ID_UnderlaySoftness) && shaderKeywords.Contains(ShaderUtilities.Keyword_Underlay))
			{
				if (material.HasProperty(ShaderUtilities.ID_ScaleRatio_C))
				{
					num7 = material.GetFloat(ShaderUtilities.ID_ScaleRatio_C);
				}
				float num11 = material.GetFloat(ShaderUtilities.ID_UnderlayOffsetX) * num7;
				float num12 = material.GetFloat(ShaderUtilities.ID_UnderlayOffsetY) * num7;
				float num13 = material.GetFloat(ShaderUtilities.ID_UnderlayDilate) * num7;
				float num14 = material.GetFloat(ShaderUtilities.ID_UnderlaySoftness) * num7;
				vector.x = Mathf.Max(vector.x, num2 + num13 + num14 - num11);
				vector.y = Mathf.Max(vector.y, num2 + num13 + num14 - num12);
				vector.z = Mathf.Max(vector.z, num2 + num13 + num14 + num11);
				vector.w = Mathf.Max(vector.w, num2 + num13 + num14 + num12);
			}
			vector.x = Mathf.Max(vector.x, num10);
			vector.y = Mathf.Max(vector.y, num10);
			vector.z = Mathf.Max(vector.z, num10);
			vector.w = Mathf.Max(vector.w, num10);
			vector.x += (float)num;
			vector.y += (float)num;
			vector.z += (float)num;
			vector.w += (float)num;
			vector.x = Mathf.Min(vector.x, 1f);
			vector.y = Mathf.Min(vector.y, 1f);
			vector.z = Mathf.Min(vector.z, 1f);
			vector.w = Mathf.Min(vector.w, 1f);
			zero.x = ((zero.x < vector.x) ? vector.x : zero.x);
			zero.y = ((zero.y < vector.y) ? vector.y : zero.y);
			zero.z = ((zero.z < vector.z) ? vector.z : zero.z);
			zero.w = ((zero.w < vector.w) ? vector.w : zero.w);
			float @float = material.GetFloat(ShaderUtilities.ID_GradientScale);
			vector *= @float;
			num10 = Mathf.Max(vector.x, vector.y);
			num10 = Mathf.Max(vector.z, num10);
			num10 = Mathf.Max(vector.w, num10);
			return num10 + 1.25f;
		}

		// Token: 0x0600039A RID: 922 RVA: 0x0002594C File Offset: 0x00023B4C
		public static float GetPadding(Material[] materials, bool enableExtraPadding, bool isBold)
		{
			if (!ShaderUtilities.isInitialized)
			{
				ShaderUtilities.GetShaderPropertyIDs();
			}
			if (materials == null)
			{
				return 0f;
			}
			int num = enableExtraPadding ? 4 : 0;
			if (materials[0].HasProperty(ShaderUtilities.ID_Padding))
			{
				return (float)num + materials[0].GetFloat(ShaderUtilities.ID_Padding);
			}
			Vector4 vector = Vector4.zero;
			Vector4 zero = Vector4.zero;
			float num2 = 0f;
			float num3 = 0f;
			float num4 = 0f;
			float num5 = 0f;
			float num6 = 0f;
			float num7 = 0f;
			float num8 = 0f;
			float num9 = 0f;
			float num10;
			for (int i = 0; i < materials.Length; i++)
			{
				ShaderUtilities.UpdateShaderRatios(materials[i]);
				string[] shaderKeywords = materials[i].shaderKeywords;
				if (materials[i].HasProperty(ShaderUtilities.ID_ScaleRatio_A))
				{
					num5 = materials[i].GetFloat(ShaderUtilities.ID_ScaleRatio_A);
				}
				if (materials[i].HasProperty(ShaderUtilities.ID_FaceDilate))
				{
					num2 = materials[i].GetFloat(ShaderUtilities.ID_FaceDilate) * num5;
				}
				if (materials[i].HasProperty(ShaderUtilities.ID_OutlineSoftness))
				{
					num3 = materials[i].GetFloat(ShaderUtilities.ID_OutlineSoftness) * num5;
				}
				if (materials[i].HasProperty(ShaderUtilities.ID_OutlineWidth))
				{
					num4 = materials[i].GetFloat(ShaderUtilities.ID_OutlineWidth) * num5;
				}
				num10 = num4 + num3 + num2;
				if (materials[i].HasProperty(ShaderUtilities.ID_GlowOffset) && shaderKeywords.Contains(ShaderUtilities.Keyword_Glow))
				{
					if (materials[i].HasProperty(ShaderUtilities.ID_ScaleRatio_B))
					{
						num6 = materials[i].GetFloat(ShaderUtilities.ID_ScaleRatio_B);
					}
					num8 = materials[i].GetFloat(ShaderUtilities.ID_GlowOffset) * num6;
					num9 = materials[i].GetFloat(ShaderUtilities.ID_GlowOuter) * num6;
				}
				num10 = Mathf.Max(num10, num2 + num8 + num9);
				if (materials[i].HasProperty(ShaderUtilities.ID_UnderlaySoftness) && shaderKeywords.Contains(ShaderUtilities.Keyword_Underlay))
				{
					if (materials[i].HasProperty(ShaderUtilities.ID_ScaleRatio_C))
					{
						num7 = materials[i].GetFloat(ShaderUtilities.ID_ScaleRatio_C);
					}
					float num11 = materials[i].GetFloat(ShaderUtilities.ID_UnderlayOffsetX) * num7;
					float num12 = materials[i].GetFloat(ShaderUtilities.ID_UnderlayOffsetY) * num7;
					float num13 = materials[i].GetFloat(ShaderUtilities.ID_UnderlayDilate) * num7;
					float num14 = materials[i].GetFloat(ShaderUtilities.ID_UnderlaySoftness) * num7;
					vector.x = Mathf.Max(vector.x, num2 + num13 + num14 - num11);
					vector.y = Mathf.Max(vector.y, num2 + num13 + num14 - num12);
					vector.z = Mathf.Max(vector.z, num2 + num13 + num14 + num11);
					vector.w = Mathf.Max(vector.w, num2 + num13 + num14 + num12);
				}
				vector.x = Mathf.Max(vector.x, num10);
				vector.y = Mathf.Max(vector.y, num10);
				vector.z = Mathf.Max(vector.z, num10);
				vector.w = Mathf.Max(vector.w, num10);
				vector.x += (float)num;
				vector.y += (float)num;
				vector.z += (float)num;
				vector.w += (float)num;
				vector.x = Mathf.Min(vector.x, 1f);
				vector.y = Mathf.Min(vector.y, 1f);
				vector.z = Mathf.Min(vector.z, 1f);
				vector.w = Mathf.Min(vector.w, 1f);
				zero.x = ((zero.x < vector.x) ? vector.x : zero.x);
				zero.y = ((zero.y < vector.y) ? vector.y : zero.y);
				zero.z = ((zero.z < vector.z) ? vector.z : zero.z);
				zero.w = ((zero.w < vector.w) ? vector.w : zero.w);
			}
			float @float = materials[0].GetFloat(ShaderUtilities.ID_GradientScale);
			vector *= @float;
			num10 = Mathf.Max(vector.x, vector.y);
			num10 = Mathf.Max(vector.z, num10);
			num10 = Mathf.Max(vector.w, num10);
			return num10 + 0.25f;
		}

		// Token: 0x0400034F RID: 847
		public static int ID_MainTex;

		// Token: 0x04000350 RID: 848
		public static int ID_FaceTex;

		// Token: 0x04000351 RID: 849
		public static int ID_FaceColor;

		// Token: 0x04000352 RID: 850
		public static int ID_FaceDilate;

		// Token: 0x04000353 RID: 851
		public static int ID_Shininess;

		// Token: 0x04000354 RID: 852
		public static int ID_UnderlayColor;

		// Token: 0x04000355 RID: 853
		public static int ID_UnderlayOffsetX;

		// Token: 0x04000356 RID: 854
		public static int ID_UnderlayOffsetY;

		// Token: 0x04000357 RID: 855
		public static int ID_UnderlayDilate;

		// Token: 0x04000358 RID: 856
		public static int ID_UnderlaySoftness;

		// Token: 0x04000359 RID: 857
		public static int ID_UnderlayOffset;

		// Token: 0x0400035A RID: 858
		public static int ID_UnderlayIsoPerimeter;

		// Token: 0x0400035B RID: 859
		public static int ID_WeightNormal;

		// Token: 0x0400035C RID: 860
		public static int ID_WeightBold;

		// Token: 0x0400035D RID: 861
		public static int ID_OutlineTex;

		// Token: 0x0400035E RID: 862
		public static int ID_OutlineWidth;

		// Token: 0x0400035F RID: 863
		public static int ID_OutlineSoftness;

		// Token: 0x04000360 RID: 864
		public static int ID_OutlineColor;

		// Token: 0x04000361 RID: 865
		public static int ID_Outline2Color;

		// Token: 0x04000362 RID: 866
		public static int ID_Outline2Width;

		// Token: 0x04000363 RID: 867
		public static int ID_Padding;

		// Token: 0x04000364 RID: 868
		public static int ID_GradientScale;

		// Token: 0x04000365 RID: 869
		public static int ID_ScaleX;

		// Token: 0x04000366 RID: 870
		public static int ID_ScaleY;

		// Token: 0x04000367 RID: 871
		public static int ID_PerspectiveFilter;

		// Token: 0x04000368 RID: 872
		public static int ID_Sharpness;

		// Token: 0x04000369 RID: 873
		public static int ID_TextureWidth;

		// Token: 0x0400036A RID: 874
		public static int ID_TextureHeight;

		// Token: 0x0400036B RID: 875
		public static int ID_BevelAmount;

		// Token: 0x0400036C RID: 876
		public static int ID_GlowColor;

		// Token: 0x0400036D RID: 877
		public static int ID_GlowOffset;

		// Token: 0x0400036E RID: 878
		public static int ID_GlowPower;

		// Token: 0x0400036F RID: 879
		public static int ID_GlowOuter;

		// Token: 0x04000370 RID: 880
		public static int ID_GlowInner;

		// Token: 0x04000371 RID: 881
		public static int ID_LightAngle;

		// Token: 0x04000372 RID: 882
		public static int ID_EnvMap;

		// Token: 0x04000373 RID: 883
		public static int ID_EnvMatrix;

		// Token: 0x04000374 RID: 884
		public static int ID_EnvMatrixRotation;

		// Token: 0x04000375 RID: 885
		public static int ID_MaskCoord;

		// Token: 0x04000376 RID: 886
		public static int ID_ClipRect;

		// Token: 0x04000377 RID: 887
		public static int ID_MaskSoftnessX;

		// Token: 0x04000378 RID: 888
		public static int ID_MaskSoftnessY;

		// Token: 0x04000379 RID: 889
		public static int ID_VertexOffsetX;

		// Token: 0x0400037A RID: 890
		public static int ID_VertexOffsetY;

		// Token: 0x0400037B RID: 891
		public static int ID_UseClipRect;

		// Token: 0x0400037C RID: 892
		public static int ID_StencilID;

		// Token: 0x0400037D RID: 893
		public static int ID_StencilOp;

		// Token: 0x0400037E RID: 894
		public static int ID_StencilComp;

		// Token: 0x0400037F RID: 895
		public static int ID_StencilReadMask;

		// Token: 0x04000380 RID: 896
		public static int ID_StencilWriteMask;

		// Token: 0x04000381 RID: 897
		public static int ID_ShaderFlags;

		// Token: 0x04000382 RID: 898
		public static int ID_ScaleRatio_A;

		// Token: 0x04000383 RID: 899
		public static int ID_ScaleRatio_B;

		// Token: 0x04000384 RID: 900
		public static int ID_ScaleRatio_C;

		// Token: 0x04000385 RID: 901
		public static string Keyword_Bevel = "BEVEL_ON";

		// Token: 0x04000386 RID: 902
		public static string Keyword_Glow = "GLOW_ON";

		// Token: 0x04000387 RID: 903
		public static string Keyword_Underlay = "UNDERLAY_ON";

		// Token: 0x04000388 RID: 904
		public static string Keyword_Ratios = "RATIOS_OFF";

		// Token: 0x04000389 RID: 905
		public static string Keyword_MASK_SOFT = "MASK_SOFT";

		// Token: 0x0400038A RID: 906
		public static string Keyword_MASK_HARD = "MASK_HARD";

		// Token: 0x0400038B RID: 907
		public static string Keyword_MASK_TEX = "MASK_TEX";

		// Token: 0x0400038C RID: 908
		public static string Keyword_Outline = "OUTLINE_ON";

		// Token: 0x0400038D RID: 909
		public static string ShaderTag_ZTestMode = "unity_GUIZTestMode";

		// Token: 0x0400038E RID: 910
		public static string ShaderTag_CullMode = "_CullMode";

		// Token: 0x0400038F RID: 911
		private static float m_clamp = 1f;

		// Token: 0x04000390 RID: 912
		public static bool isInitialized = false;

		// Token: 0x04000391 RID: 913
		private static Shader k_ShaderRef_MobileSDF;

		// Token: 0x04000392 RID: 914
		private static Shader k_ShaderRef_MobileBitmap;
	}
}
