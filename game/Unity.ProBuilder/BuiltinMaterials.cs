using System;
using System.Reflection;
using UnityEngine.Rendering;

namespace UnityEngine.ProBuilder
{
	// Token: 0x0200000A RID: 10
	public static class BuiltinMaterials
	{
		// Token: 0x06000060 RID: 96 RVA: 0x00003BF8 File Offset: 0x00001DF8
		private static void Init()
		{
			if (BuiltinMaterials.s_IsInitialized)
			{
				return;
			}
			BuiltinMaterials.s_IsInitialized = true;
			Shader shader = Shader.Find("Hidden/ProBuilder/LineBillboard");
			BuiltinMaterials.s_GeometryShadersSupported = (shader != null && shader.isSupported);
			BuiltinMaterials.s_DefaultMaterial = BuiltinMaterials.GetDefaultMaterial();
			BuiltinMaterials.s_SelectionPickerShader = Shader.Find("Hidden/ProBuilder/SelectionPicker");
			if ((BuiltinMaterials.s_FacePickerMaterial = Resources.Load<Material>(BuiltinMaterials.k_FacePickerMaterial)) == null)
			{
				Log.Error("FacePicker material not loaded... please re-install ProBuilder to fix this error.");
				BuiltinMaterials.s_FacePickerMaterial = new Material(Shader.Find(BuiltinMaterials.k_FacePickerShader));
			}
			if ((BuiltinMaterials.s_VertexPickerMaterial = Resources.Load<Material>(BuiltinMaterials.k_VertexPickerMaterial)) == null)
			{
				Log.Error("VertexPicker material not loaded... please re-install ProBuilder to fix this error.");
				BuiltinMaterials.s_VertexPickerMaterial = new Material(Shader.Find(BuiltinMaterials.k_VertexPickerShader));
			}
			if ((BuiltinMaterials.s_EdgePickerMaterial = Resources.Load<Material>(BuiltinMaterials.k_EdgePickerMaterial)) == null)
			{
				Log.Error("EdgePicker material not loaded... please re-install ProBuilder to fix this error.");
				BuiltinMaterials.s_EdgePickerMaterial = new Material(Shader.Find(BuiltinMaterials.k_EdgePickerShader));
			}
			BuiltinMaterials.s_UnlitVertexColorMaterial = (Material)Resources.Load("Materials/UnlitVertexColor", typeof(Material));
			BuiltinMaterials.s_ShapePreviewMaterial = new Material(BuiltinMaterials.s_DefaultMaterial.shader);
			BuiltinMaterials.s_ShapePreviewMaterial.hideFlags = HideFlags.HideAndDontSave;
			if (BuiltinMaterials.s_ShapePreviewMaterial.HasProperty("_MainTex"))
			{
				BuiltinMaterials.s_ShapePreviewMaterial.mainTexture = (Texture2D)Resources.Load("Textures/GridBox_Default");
			}
			if (BuiltinMaterials.s_ShapePreviewMaterial.HasProperty("_Color"))
			{
				BuiltinMaterials.s_ShapePreviewMaterial.SetColor("_Color", BuiltinMaterials.previewColor);
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00003D7D File Offset: 0x00001F7D
		public static bool geometryShadersSupported
		{
			get
			{
				BuiltinMaterials.Init();
				return BuiltinMaterials.s_GeometryShadersSupported;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00003D89 File Offset: 0x00001F89
		public static Material defaultMaterial
		{
			get
			{
				BuiltinMaterials.Init();
				return BuiltinMaterials.s_DefaultMaterial;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00003D95 File Offset: 0x00001F95
		internal static Shader selectionPickerShader
		{
			get
			{
				BuiltinMaterials.Init();
				return BuiltinMaterials.s_SelectionPickerShader;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00003DA1 File Offset: 0x00001FA1
		internal static Material facePickerMaterial
		{
			get
			{
				BuiltinMaterials.Init();
				return BuiltinMaterials.s_FacePickerMaterial;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00003DAD File Offset: 0x00001FAD
		internal static Material vertexPickerMaterial
		{
			get
			{
				BuiltinMaterials.Init();
				return BuiltinMaterials.s_VertexPickerMaterial;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00003DB9 File Offset: 0x00001FB9
		internal static Material edgePickerMaterial
		{
			get
			{
				BuiltinMaterials.Init();
				return BuiltinMaterials.s_EdgePickerMaterial;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00003DC5 File Offset: 0x00001FC5
		internal static Material triggerMaterial
		{
			get
			{
				BuiltinMaterials.Init();
				return (Material)Resources.Load("Materials/Trigger", typeof(Material));
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00003DE5 File Offset: 0x00001FE5
		internal static Material colliderMaterial
		{
			get
			{
				BuiltinMaterials.Init();
				return (Material)Resources.Load("Materials/Collider", typeof(Material));
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00003E05 File Offset: 0x00002005
		[Obsolete("NoDraw is no longer supported.")]
		internal static Material noDrawMaterial
		{
			get
			{
				BuiltinMaterials.Init();
				return (Material)Resources.Load("Materials/NoDraw", typeof(Material));
			}
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003E28 File Offset: 0x00002028
		internal static Material GetLegacyDiffuse()
		{
			BuiltinMaterials.Init();
			if (BuiltinMaterials.s_UnityDefaultDiffuse == null)
			{
				MethodInfo method = typeof(Material).GetMethod("GetDefaultMaterial", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				if (method != null)
				{
					BuiltinMaterials.s_UnityDefaultDiffuse = (method.Invoke(null, null) as Material);
				}
				if (BuiltinMaterials.s_UnityDefaultDiffuse == null)
				{
					GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
					BuiltinMaterials.s_UnityDefaultDiffuse = gameObject.GetComponent<MeshRenderer>().sharedMaterial;
					Object.DestroyImmediate(gameObject);
				}
			}
			return BuiltinMaterials.s_UnityDefaultDiffuse;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003EA8 File Offset: 0x000020A8
		internal static Material GetDefaultMaterial()
		{
			Material material = null;
			if (GraphicsSettings.renderPipelineAsset != null)
			{
				material = GraphicsSettings.renderPipelineAsset.defaultMaterial;
			}
			if (material == null)
			{
				material = (Material)Resources.Load("Materials/ProBuilderDefault", typeof(Material));
				if (material == null || !material.shader.isSupported)
				{
					material = BuiltinMaterials.GetLegacyDiffuse();
				}
			}
			return material;
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600006C RID: 108 RVA: 0x00003F0F File Offset: 0x0000210F
		internal static Material unlitVertexColor
		{
			get
			{
				BuiltinMaterials.Init();
				return BuiltinMaterials.s_UnlitVertexColorMaterial;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00003F1B File Offset: 0x0000211B
		internal static Material ShapePreviewMaterial
		{
			get
			{
				BuiltinMaterials.Init();
				return BuiltinMaterials.s_ShapePreviewMaterial;
			}
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003F28 File Offset: 0x00002128
		// Note: this type is marked as 'beforefieldinit'.
		static BuiltinMaterials()
		{
		}

		// Token: 0x04000022 RID: 34
		private static bool s_IsInitialized;

		// Token: 0x04000023 RID: 35
		public const string faceShader = "Hidden/ProBuilder/FaceHighlight";

		// Token: 0x04000024 RID: 36
		public const string lineShader = "Hidden/ProBuilder/LineBillboard";

		// Token: 0x04000025 RID: 37
		public const string lineShaderMetal = "Hidden/ProBuilder/LineBillboardMetal";

		// Token: 0x04000026 RID: 38
		public const string pointShader = "Hidden/ProBuilder/PointBillboard";

		// Token: 0x04000027 RID: 39
		public const string wireShader = "Hidden/ProBuilder/FaceHighlight";

		// Token: 0x04000028 RID: 40
		public const string dotShader = "Hidden/ProBuilder/VertexShader";

		// Token: 0x04000029 RID: 41
		internal static readonly Color previewColor = new Color(0.5f, 0.9f, 1f, 0.56f);

		// Token: 0x0400002A RID: 42
		private static Shader s_SelectionPickerShader;

		// Token: 0x0400002B RID: 43
		private static bool s_GeometryShadersSupported;

		// Token: 0x0400002C RID: 44
		private static Material s_DefaultMaterial;

		// Token: 0x0400002D RID: 45
		private static Material s_FacePickerMaterial;

		// Token: 0x0400002E RID: 46
		private static Material s_VertexPickerMaterial;

		// Token: 0x0400002F RID: 47
		private static Material s_EdgePickerMaterial;

		// Token: 0x04000030 RID: 48
		private static Material s_UnityDefaultDiffuse;

		// Token: 0x04000031 RID: 49
		private static Material s_UnlitVertexColorMaterial;

		// Token: 0x04000032 RID: 50
		private static Material s_ShapePreviewMaterial;

		// Token: 0x04000033 RID: 51
		private static string k_EdgePickerMaterial = "Materials/EdgePicker";

		// Token: 0x04000034 RID: 52
		private static string k_FacePickerMaterial = "Materials/FacePicker";

		// Token: 0x04000035 RID: 53
		private static string k_VertexPickerMaterial = "Materials/VertexPicker";

		// Token: 0x04000036 RID: 54
		private static string k_EdgePickerShader = "Hidden/ProBuilder/EdgePicker";

		// Token: 0x04000037 RID: 55
		private static string k_FacePickerShader = "Hidden/ProBuilder/FacePicker";

		// Token: 0x04000038 RID: 56
		private static string k_VertexPickerShader = "Hidden/ProBuilder/VertexPicker";
	}
}
