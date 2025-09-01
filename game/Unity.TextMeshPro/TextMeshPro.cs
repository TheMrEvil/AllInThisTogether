using System;
using System.Runtime.CompilerServices;
using System.Threading;
using Unity.Profiling;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.TextCore;
using UnityEngine.UI;

namespace TMPro
{
	// Token: 0x0200000C RID: 12
	[DisallowMultipleComponent]
	[RequireComponent(typeof(MeshRenderer))]
	[AddComponentMenu("Mesh/TextMeshPro - Text")]
	[ExecuteAlways]
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.textmeshpro@3.0")]
	public class TextMeshPro : TMP_Text, ILayoutElement
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00002E1D File Offset: 0x0000101D
		// (set) Token: 0x06000050 RID: 80 RVA: 0x00002E3A File Offset: 0x0000103A
		public int sortingLayerID
		{
			get
			{
				if (this.renderer == null)
				{
					return 0;
				}
				return this.m_renderer.sortingLayerID;
			}
			set
			{
				if (this.renderer == null)
				{
					return;
				}
				this.m_renderer.sortingLayerID = value;
				this._SortingLayerID = value;
				this.UpdateSubMeshSortingLayerID(value);
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00002E65 File Offset: 0x00001065
		// (set) Token: 0x06000052 RID: 82 RVA: 0x00002E82 File Offset: 0x00001082
		public int sortingOrder
		{
			get
			{
				if (this.renderer == null)
				{
					return 0;
				}
				return this.m_renderer.sortingOrder;
			}
			set
			{
				if (this.renderer == null)
				{
					return;
				}
				this.m_renderer.sortingOrder = value;
				this._SortingOrder = value;
				this.UpdateSubMeshSortingOrder(value);
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00002EAD File Offset: 0x000010AD
		// (set) Token: 0x06000054 RID: 84 RVA: 0x00002EB5 File Offset: 0x000010B5
		public override bool autoSizeTextContainer
		{
			get
			{
				return this.m_autoSizeTextContainer;
			}
			set
			{
				if (this.m_autoSizeTextContainer == value)
				{
					return;
				}
				this.m_autoSizeTextContainer = value;
				if (this.m_autoSizeTextContainer)
				{
					TMP_UpdateManager.RegisterTextElementForLayoutRebuild(this);
					this.SetLayoutDirty();
				}
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00002EDC File Offset: 0x000010DC
		[Obsolete("The TextContainer is now obsolete. Use the RectTransform instead.")]
		public TextContainer textContainer
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00002EDF File Offset: 0x000010DF
		public new Transform transform
		{
			get
			{
				if (this.m_transform == null)
				{
					this.m_transform = base.GetComponent<Transform>();
				}
				return this.m_transform;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00002F01 File Offset: 0x00001101
		public Renderer renderer
		{
			get
			{
				if (this.m_renderer == null)
				{
					this.m_renderer = base.GetComponent<Renderer>();
				}
				return this.m_renderer;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00002F23 File Offset: 0x00001123
		public override Mesh mesh
		{
			get
			{
				if (this.m_mesh == null)
				{
					this.m_mesh = new Mesh();
					this.m_mesh.hideFlags = HideFlags.HideAndDontSave;
				}
				return this.m_mesh;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00002F54 File Offset: 0x00001154
		public MeshFilter meshFilter
		{
			get
			{
				if (this.m_meshFilter == null)
				{
					this.m_meshFilter = base.GetComponent<MeshFilter>();
					if (this.m_meshFilter == null)
					{
						this.m_meshFilter = base.gameObject.AddComponent<MeshFilter>();
						this.m_meshFilter.hideFlags = (HideFlags.HideInHierarchy | HideFlags.HideInInspector | HideFlags.DontSaveInEditor | HideFlags.NotEditable | HideFlags.DontSaveInBuild | HideFlags.DontUnloadUnusedAsset);
					}
				}
				return this.m_meshFilter;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00002FAD File Offset: 0x000011AD
		// (set) Token: 0x0600005B RID: 91 RVA: 0x00002FB5 File Offset: 0x000011B5
		public MaskingTypes maskType
		{
			get
			{
				return this.m_maskType;
			}
			set
			{
				this.m_maskType = value;
				this.SetMask(this.m_maskType);
			}
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002FCA File Offset: 0x000011CA
		public void SetMask(MaskingTypes type, Vector4 maskCoords)
		{
			this.SetMask(type);
			this.SetMaskCoordinates(maskCoords);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002FDA File Offset: 0x000011DA
		public void SetMask(MaskingTypes type, Vector4 maskCoords, float softnessX, float softnessY)
		{
			this.SetMask(type);
			this.SetMaskCoordinates(maskCoords, softnessX, softnessY);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002FED File Offset: 0x000011ED
		public override void SetVerticesDirty()
		{
			if (this == null || !this.IsActive())
			{
				return;
			}
			TMP_UpdateManager.RegisterTextElementForGraphicRebuild(this);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00003007 File Offset: 0x00001207
		public override void SetLayoutDirty()
		{
			this.m_isPreferredWidthDirty = true;
			this.m_isPreferredHeightDirty = true;
			if (this == null || !this.IsActive())
			{
				return;
			}
			LayoutRebuilder.MarkLayoutForRebuild(base.rectTransform);
			this.m_isLayoutDirty = true;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x0000303B File Offset: 0x0000123B
		public override void SetMaterialDirty()
		{
			this.UpdateMaterial();
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00003043 File Offset: 0x00001243
		public override void SetAllDirty()
		{
			this.SetLayoutDirty();
			this.SetVerticesDirty();
			this.SetMaterialDirty();
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003058 File Offset: 0x00001258
		public override void Rebuild(CanvasUpdate update)
		{
			if (this == null)
			{
				return;
			}
			if (update == CanvasUpdate.Prelayout)
			{
				if (this.m_autoSizeTextContainer)
				{
					this.m_rectTransform.sizeDelta = base.GetPreferredValues(float.PositiveInfinity, float.PositiveInfinity);
					return;
				}
			}
			else if (update == CanvasUpdate.PreRender)
			{
				this.OnPreRenderObject();
				if (!this.m_isMaterialDirty)
				{
					return;
				}
				this.UpdateMaterial();
				this.m_isMaterialDirty = false;
			}
		}

		// Token: 0x06000063 RID: 99 RVA: 0x000030B8 File Offset: 0x000012B8
		protected override void UpdateMaterial()
		{
			if (this.renderer == null || this.m_sharedMaterial == null)
			{
				return;
			}
			if (this.m_renderer.sharedMaterial == null || this.m_renderer.sharedMaterial.GetInstanceID() != this.m_sharedMaterial.GetInstanceID())
			{
				this.m_renderer.sharedMaterial = this.m_sharedMaterial;
			}
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003124 File Offset: 0x00001324
		public override void UpdateMeshPadding()
		{
			this.m_padding = ShaderUtilities.GetPadding(this.m_sharedMaterial, this.m_enableExtraPadding, this.m_isUsingBold);
			this.m_isMaskingEnabled = ShaderUtilities.IsMaskingEnabled(this.m_sharedMaterial);
			this.m_havePropertiesChanged = true;
			this.checkPaddingRequired = false;
			if (this.m_textInfo == null)
			{
				return;
			}
			for (int i = 1; i < this.m_textInfo.materialCount; i++)
			{
				this.m_subTextObjects[i].UpdateMeshPadding(this.m_enableExtraPadding, this.m_isUsingBold);
			}
		}

		// Token: 0x06000065 RID: 101 RVA: 0x000031A5 File Offset: 0x000013A5
		public override void ForceMeshUpdate(bool ignoreActiveState = false, bool forceTextReparsing = false)
		{
			this.m_havePropertiesChanged = true;
			this.m_ignoreActiveState = ignoreActiveState;
			this.OnPreRenderObject();
		}

		// Token: 0x06000066 RID: 102 RVA: 0x000031BB File Offset: 0x000013BB
		public override TMP_TextInfo GetTextInfo(string text)
		{
			base.SetText(text, true);
			this.SetArraySizes(this.m_TextProcessingArray);
			this.m_renderMode = TextRenderFlags.DontRender;
			this.ComputeMarginSize();
			this.GenerateTextMesh();
			this.m_renderMode = TextRenderFlags.Render;
			return base.textInfo;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x000031F8 File Offset: 0x000013F8
		public override void ClearMesh(bool updateMesh)
		{
			if (this.m_textInfo.meshInfo[0].mesh == null)
			{
				this.m_textInfo.meshInfo[0].mesh = this.m_mesh;
			}
			this.m_textInfo.ClearMeshInfo(updateMesh);
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000068 RID: 104 RVA: 0x0000324C File Offset: 0x0000144C
		// (remove) Token: 0x06000069 RID: 105 RVA: 0x00003284 File Offset: 0x00001484
		public override event Action<TMP_TextInfo> OnPreRenderText
		{
			[CompilerGenerated]
			add
			{
				Action<TMP_TextInfo> action = this.OnPreRenderText;
				Action<TMP_TextInfo> action2;
				do
				{
					action2 = action;
					Action<TMP_TextInfo> value2 = (Action<TMP_TextInfo>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<TMP_TextInfo>>(ref this.OnPreRenderText, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<TMP_TextInfo> action = this.OnPreRenderText;
				Action<TMP_TextInfo> action2;
				do
				{
					action2 = action;
					Action<TMP_TextInfo> value2 = (Action<TMP_TextInfo>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<TMP_TextInfo>>(ref this.OnPreRenderText, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000032B9 File Offset: 0x000014B9
		public override void UpdateGeometry(Mesh mesh, int index)
		{
			mesh.RecalculateBounds();
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000032C4 File Offset: 0x000014C4
		public override void UpdateVertexData(TMP_VertexDataUpdateFlags flags)
		{
			int materialCount = this.m_textInfo.materialCount;
			for (int i = 0; i < materialCount; i++)
			{
				Mesh mesh;
				if (i == 0)
				{
					mesh = this.m_mesh;
				}
				else
				{
					mesh = this.m_subTextObjects[i].mesh;
				}
				if ((flags & TMP_VertexDataUpdateFlags.Vertices) == TMP_VertexDataUpdateFlags.Vertices)
				{
					mesh.vertices = this.m_textInfo.meshInfo[i].vertices;
				}
				if ((flags & TMP_VertexDataUpdateFlags.Uv0) == TMP_VertexDataUpdateFlags.Uv0)
				{
					mesh.uv = this.m_textInfo.meshInfo[i].uvs0;
				}
				if ((flags & TMP_VertexDataUpdateFlags.Uv2) == TMP_VertexDataUpdateFlags.Uv2)
				{
					mesh.uv2 = this.m_textInfo.meshInfo[i].uvs2;
				}
				if ((flags & TMP_VertexDataUpdateFlags.Colors32) == TMP_VertexDataUpdateFlags.Colors32)
				{
					mesh.colors32 = this.m_textInfo.meshInfo[i].colors32;
				}
				mesh.RecalculateBounds();
			}
		}

		// Token: 0x0600006C RID: 108 RVA: 0x0000339C File Offset: 0x0000159C
		public override void UpdateVertexData()
		{
			int materialCount = this.m_textInfo.materialCount;
			for (int i = 0; i < materialCount; i++)
			{
				Mesh mesh;
				if (i == 0)
				{
					mesh = this.m_mesh;
				}
				else
				{
					this.m_textInfo.meshInfo[i].ClearUnusedVertices();
					mesh = this.m_subTextObjects[i].mesh;
				}
				mesh.vertices = this.m_textInfo.meshInfo[i].vertices;
				mesh.uv = this.m_textInfo.meshInfo[i].uvs0;
				mesh.uv2 = this.m_textInfo.meshInfo[i].uvs2;
				mesh.colors32 = this.m_textInfo.meshInfo[i].colors32;
				mesh.RecalculateBounds();
			}
		}

		// Token: 0x0600006D RID: 109 RVA: 0x0000346D File Offset: 0x0000166D
		public void UpdateFontAsset()
		{
			this.LoadFontAsset();
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003475 File Offset: 0x00001675
		public void CalculateLayoutInputHorizontal()
		{
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003477 File Offset: 0x00001677
		public void CalculateLayoutInputVertical()
		{
		}

		// Token: 0x06000070 RID: 112 RVA: 0x0000347C File Offset: 0x0000167C
		protected override void Awake()
		{
			this.m_renderer = base.GetComponent<Renderer>();
			if (this.m_renderer == null)
			{
				this.m_renderer = base.gameObject.AddComponent<Renderer>();
			}
			this.m_rectTransform = base.rectTransform;
			this.m_transform = this.transform;
			this.m_meshFilter = base.GetComponent<MeshFilter>();
			if (this.m_meshFilter == null)
			{
				this.m_meshFilter = base.gameObject.AddComponent<MeshFilter>();
			}
			if (this.m_mesh == null)
			{
				this.m_mesh = new Mesh();
				this.m_mesh.hideFlags = HideFlags.HideAndDontSave;
				this.m_meshFilter.sharedMesh = this.m_mesh;
				this.m_textInfo = new TMP_TextInfo(this);
			}
			this.m_meshFilter.hideFlags = (HideFlags.HideInHierarchy | HideFlags.HideInInspector | HideFlags.DontSaveInEditor | HideFlags.NotEditable | HideFlags.DontSaveInBuild | HideFlags.DontUnloadUnusedAsset);
			base.LoadDefaultSettings();
			this.LoadFontAsset();
			if (this.m_TextProcessingArray == null)
			{
				this.m_TextProcessingArray = new TMP_Text.UnicodeChar[this.m_max_characters];
			}
			this.m_cached_TextElement = new TMP_Character();
			this.m_isFirstAllocation = true;
			TMP_SubMesh[] componentsInChildren = base.GetComponentsInChildren<TMP_SubMesh>();
			if (componentsInChildren.Length != 0)
			{
				int num = componentsInChildren.Length;
				if (num + 1 > this.m_subTextObjects.Length)
				{
					Array.Resize<TMP_SubMesh>(ref this.m_subTextObjects, num + 1);
				}
				for (int i = 0; i < num; i++)
				{
					this.m_subTextObjects[i + 1] = componentsInChildren[i];
				}
			}
			this.m_havePropertiesChanged = true;
			this.m_isAwake = true;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000035D0 File Offset: 0x000017D0
		protected override void OnEnable()
		{
			if (!this.m_isAwake)
			{
				return;
			}
			if (!this.m_isRegisteredForEvents)
			{
				this.m_isRegisteredForEvents = true;
			}
			if (!this.m_IsTextObjectScaleStatic)
			{
				TMP_UpdateManager.RegisterTextObjectForUpdate(this);
			}
			this.meshFilter.sharedMesh = this.mesh;
			this.SetActiveSubMeshes(true);
			this.ComputeMarginSize();
			this.SetAllDirty();
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003627 File Offset: 0x00001827
		protected override void OnDisable()
		{
			if (!this.m_isAwake)
			{
				return;
			}
			TMP_UpdateManager.UnRegisterTextElementForRebuild(this);
			TMP_UpdateManager.UnRegisterTextObjectForUpdate(this);
			this.meshFilter.sharedMesh = null;
			this.SetActiveSubMeshes(false);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003651 File Offset: 0x00001851
		protected override void OnDestroy()
		{
			if (this.m_mesh != null)
			{
				UnityEngine.Object.DestroyImmediate(this.m_mesh);
			}
			this.m_isRegisteredForEvents = false;
			TMP_UpdateManager.UnRegisterTextElementForRebuild(this);
			TMP_UpdateManager.UnRegisterTextObjectForUpdate(this);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003680 File Offset: 0x00001880
		protected override void LoadFontAsset()
		{
			ShaderUtilities.GetShaderPropertyIDs();
			if (this.m_fontAsset == null)
			{
				if (TMP_Settings.defaultFontAsset != null)
				{
					this.m_fontAsset = TMP_Settings.defaultFontAsset;
				}
				else
				{
					this.m_fontAsset = Resources.Load<TMP_FontAsset>("Fonts & Materials/LiberationSans SDF");
				}
				if (this.m_fontAsset == null)
				{
					Debug.LogWarning("The LiberationSans SDF Font Asset was not found. There is no Font Asset assigned to " + base.gameObject.name + ".", this);
					return;
				}
				if (this.m_fontAsset.characterLookupTable == null)
				{
					Debug.Log("Dictionary is Null!");
				}
				this.m_sharedMaterial = this.m_fontAsset.material;
				this.m_sharedMaterial.SetFloat("_CullMode", 0f);
				this.m_sharedMaterial.SetFloat(ShaderUtilities.ShaderTag_ZTestMode, 4f);
				this.m_renderer.receiveShadows = false;
				this.m_renderer.shadowCastingMode = ShadowCastingMode.Off;
			}
			else
			{
				if (this.m_fontAsset.characterLookupTable == null)
				{
					this.m_fontAsset.ReadFontAssetDefinition();
				}
				if (this.m_sharedMaterial == null || this.m_sharedMaterial.GetTexture(ShaderUtilities.ID_MainTex) == null || this.m_fontAsset.atlasTexture.GetInstanceID() != this.m_sharedMaterial.GetTexture(ShaderUtilities.ID_MainTex).GetInstanceID())
				{
					if (this.m_fontAsset.material == null)
					{
						Debug.LogWarning(string.Concat(new string[]
						{
							"The Font Atlas Texture of the Font Asset ",
							this.m_fontAsset.name,
							" assigned to ",
							base.gameObject.name,
							" is missing."
						}), this);
					}
					else
					{
						this.m_sharedMaterial = this.m_fontAsset.material;
					}
				}
				this.m_sharedMaterial.SetFloat(ShaderUtilities.ShaderTag_ZTestMode, 4f);
				if (this.m_sharedMaterial.passCount == 1)
				{
					this.m_renderer.receiveShadows = false;
					this.m_renderer.shadowCastingMode = ShadowCastingMode.Off;
				}
			}
			this.m_padding = this.GetPaddingForMaterial();
			this.m_isMaskingEnabled = ShaderUtilities.IsMaskingEnabled(this.m_sharedMaterial);
			base.GetSpecialCharacters(this.m_fontAsset);
			this.SetMaterialDirty();
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000038A8 File Offset: 0x00001AA8
		private void UpdateEnvMapMatrix()
		{
			if (!this.m_sharedMaterial.HasProperty(ShaderUtilities.ID_EnvMap) || this.m_sharedMaterial.GetTexture(ShaderUtilities.ID_EnvMap) == null)
			{
				return;
			}
			Vector3 euler = this.m_sharedMaterial.GetVector(ShaderUtilities.ID_EnvMatrixRotation);
			this.m_EnvMapMatrix = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(euler), Vector3.one);
			this.m_sharedMaterial.SetMatrix(ShaderUtilities.ID_EnvMatrix, this.m_EnvMapMatrix);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003928 File Offset: 0x00001B28
		private void SetMask(MaskingTypes maskType)
		{
			switch (maskType)
			{
			case MaskingTypes.MaskOff:
				this.m_sharedMaterial.DisableKeyword(ShaderUtilities.Keyword_MASK_SOFT);
				this.m_sharedMaterial.DisableKeyword(ShaderUtilities.Keyword_MASK_HARD);
				this.m_sharedMaterial.DisableKeyword(ShaderUtilities.Keyword_MASK_TEX);
				return;
			case MaskingTypes.MaskHard:
				this.m_sharedMaterial.EnableKeyword(ShaderUtilities.Keyword_MASK_HARD);
				this.m_sharedMaterial.DisableKeyword(ShaderUtilities.Keyword_MASK_SOFT);
				this.m_sharedMaterial.DisableKeyword(ShaderUtilities.Keyword_MASK_TEX);
				return;
			case MaskingTypes.MaskSoft:
				this.m_sharedMaterial.EnableKeyword(ShaderUtilities.Keyword_MASK_SOFT);
				this.m_sharedMaterial.DisableKeyword(ShaderUtilities.Keyword_MASK_HARD);
				this.m_sharedMaterial.DisableKeyword(ShaderUtilities.Keyword_MASK_TEX);
				return;
			default:
				return;
			}
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000039DA File Offset: 0x00001BDA
		private void SetMaskCoordinates(Vector4 coords)
		{
			this.m_sharedMaterial.SetVector(ShaderUtilities.ID_ClipRect, coords);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000039ED File Offset: 0x00001BED
		private void SetMaskCoordinates(Vector4 coords, float softX, float softY)
		{
			this.m_sharedMaterial.SetVector(ShaderUtilities.ID_ClipRect, coords);
			this.m_sharedMaterial.SetFloat(ShaderUtilities.ID_MaskSoftnessX, softX);
			this.m_sharedMaterial.SetFloat(ShaderUtilities.ID_MaskSoftnessY, softY);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003A24 File Offset: 0x00001C24
		private void EnableMasking()
		{
			if (this.m_sharedMaterial.HasProperty(ShaderUtilities.ID_ClipRect))
			{
				this.m_sharedMaterial.EnableKeyword(ShaderUtilities.Keyword_MASK_SOFT);
				this.m_sharedMaterial.DisableKeyword(ShaderUtilities.Keyword_MASK_HARD);
				this.m_sharedMaterial.DisableKeyword(ShaderUtilities.Keyword_MASK_TEX);
				this.m_isMaskingEnabled = true;
				this.UpdateMask();
			}
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003A80 File Offset: 0x00001C80
		private void DisableMasking()
		{
			if (this.m_sharedMaterial.HasProperty(ShaderUtilities.ID_ClipRect))
			{
				this.m_sharedMaterial.DisableKeyword(ShaderUtilities.Keyword_MASK_SOFT);
				this.m_sharedMaterial.DisableKeyword(ShaderUtilities.Keyword_MASK_HARD);
				this.m_sharedMaterial.DisableKeyword(ShaderUtilities.Keyword_MASK_TEX);
				this.m_isMaskingEnabled = false;
				this.UpdateMask();
			}
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003ADC File Offset: 0x00001CDC
		private void UpdateMask()
		{
			if (!this.m_isMaskingEnabled)
			{
				return;
			}
			if (this.m_isMaskingEnabled && this.m_fontMaterial == null)
			{
				this.CreateMaterialInstance();
			}
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00003B04 File Offset: 0x00001D04
		protected override Material GetMaterial(Material mat)
		{
			if (this.m_fontMaterial == null || this.m_fontMaterial.GetInstanceID() != mat.GetInstanceID())
			{
				this.m_fontMaterial = this.CreateMaterialInstance(mat);
			}
			this.m_sharedMaterial = this.m_fontMaterial;
			this.m_padding = this.GetPaddingForMaterial();
			this.SetVerticesDirty();
			this.SetMaterialDirty();
			return this.m_sharedMaterial;
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003B6C File Offset: 0x00001D6C
		protected override Material[] GetMaterials(Material[] mats)
		{
			int materialCount = this.m_textInfo.materialCount;
			if (this.m_fontMaterials == null)
			{
				this.m_fontMaterials = new Material[materialCount];
			}
			else if (this.m_fontMaterials.Length != materialCount)
			{
				TMP_TextInfo.Resize<Material>(ref this.m_fontMaterials, materialCount, false);
			}
			for (int i = 0; i < materialCount; i++)
			{
				if (i == 0)
				{
					this.m_fontMaterials[i] = base.fontMaterial;
				}
				else
				{
					this.m_fontMaterials[i] = this.m_subTextObjects[i].material;
				}
			}
			this.m_fontSharedMaterials = this.m_fontMaterials;
			return this.m_fontMaterials;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003BF9 File Offset: 0x00001DF9
		protected override void SetSharedMaterial(Material mat)
		{
			this.m_sharedMaterial = mat;
			this.m_padding = this.GetPaddingForMaterial();
			this.SetMaterialDirty();
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00003C14 File Offset: 0x00001E14
		protected override Material[] GetSharedMaterials()
		{
			int materialCount = this.m_textInfo.materialCount;
			if (this.m_fontSharedMaterials == null)
			{
				this.m_fontSharedMaterials = new Material[materialCount];
			}
			else if (this.m_fontSharedMaterials.Length != materialCount)
			{
				TMP_TextInfo.Resize<Material>(ref this.m_fontSharedMaterials, materialCount, false);
			}
			for (int i = 0; i < materialCount; i++)
			{
				if (i == 0)
				{
					this.m_fontSharedMaterials[i] = this.m_sharedMaterial;
				}
				else
				{
					this.m_fontSharedMaterials[i] = this.m_subTextObjects[i].sharedMaterial;
				}
			}
			return this.m_fontSharedMaterials;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003C98 File Offset: 0x00001E98
		protected override void SetSharedMaterials(Material[] materials)
		{
			int materialCount = this.m_textInfo.materialCount;
			if (this.m_fontSharedMaterials == null)
			{
				this.m_fontSharedMaterials = new Material[materialCount];
			}
			else if (this.m_fontSharedMaterials.Length != materialCount)
			{
				TMP_TextInfo.Resize<Material>(ref this.m_fontSharedMaterials, materialCount, false);
			}
			for (int i = 0; i < materialCount; i++)
			{
				Texture texture = materials[i].GetTexture(ShaderUtilities.ID_MainTex);
				if (i == 0)
				{
					if (!(texture == null) && texture.GetInstanceID() == this.m_sharedMaterial.GetTexture(ShaderUtilities.ID_MainTex).GetInstanceID())
					{
						this.m_sharedMaterial = (this.m_fontSharedMaterials[i] = materials[i]);
						this.m_padding = this.GetPaddingForMaterial(this.m_sharedMaterial);
					}
				}
				else if (!(texture == null) && texture.GetInstanceID() == this.m_subTextObjects[i].sharedMaterial.GetTexture(ShaderUtilities.ID_MainTex).GetInstanceID() && this.m_subTextObjects[i].isDefaultMaterial)
				{
					this.m_subTextObjects[i].sharedMaterial = (this.m_fontSharedMaterials[i] = materials[i]);
				}
			}
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003DAC File Offset: 0x00001FAC
		protected override void SetOutlineThickness(float thickness)
		{
			thickness = Mathf.Clamp01(thickness);
			this.m_renderer.material.SetFloat(ShaderUtilities.ID_OutlineWidth, thickness);
			if (this.m_fontMaterial == null)
			{
				this.m_fontMaterial = this.m_renderer.material;
			}
			this.m_fontMaterial = this.m_renderer.material;
			this.m_sharedMaterial = this.m_fontMaterial;
			this.m_padding = this.GetPaddingForMaterial();
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00003E20 File Offset: 0x00002020
		protected override void SetFaceColor(Color32 color)
		{
			this.m_renderer.material.SetColor(ShaderUtilities.ID_FaceColor, color);
			if (this.m_fontMaterial == null)
			{
				this.m_fontMaterial = this.m_renderer.material;
			}
			this.m_sharedMaterial = this.m_fontMaterial;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003E74 File Offset: 0x00002074
		protected override void SetOutlineColor(Color32 color)
		{
			this.m_renderer.material.SetColor(ShaderUtilities.ID_OutlineColor, color);
			if (this.m_fontMaterial == null)
			{
				this.m_fontMaterial = this.m_renderer.material;
			}
			this.m_sharedMaterial = this.m_fontMaterial;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003EC8 File Offset: 0x000020C8
		private void CreateMaterialInstance()
		{
			Material material = new Material(this.m_sharedMaterial);
			material.shaderKeywords = this.m_sharedMaterial.shaderKeywords;
			Material material2 = material;
			material2.name += " Instance";
			this.m_fontMaterial = material;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003F10 File Offset: 0x00002110
		protected override void SetShaderDepth()
		{
			if (this.m_isOverlay)
			{
				this.m_sharedMaterial.SetFloat(ShaderUtilities.ShaderTag_ZTestMode, 0f);
				this.m_renderer.material.renderQueue = 4000;
				this.m_sharedMaterial = this.m_renderer.material;
				return;
			}
			this.m_sharedMaterial.SetFloat(ShaderUtilities.ShaderTag_ZTestMode, 4f);
			this.m_renderer.material.renderQueue = -1;
			this.m_sharedMaterial = this.m_renderer.material;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003F98 File Offset: 0x00002198
		protected override void SetCulling()
		{
			if (this.m_isCullingEnabled)
			{
				this.m_renderer.material.SetFloat("_CullMode", 2f);
				for (int i = 1; i < this.m_subTextObjects.Length; i++)
				{
					if (!(this.m_subTextObjects[i] != null))
					{
						return;
					}
					Renderer renderer = this.m_subTextObjects[i].renderer;
					if (renderer != null)
					{
						renderer.material.SetFloat(ShaderUtilities.ShaderTag_CullMode, 2f);
					}
				}
			}
			else
			{
				this.m_renderer.material.SetFloat("_CullMode", 0f);
				int num = 1;
				while (num < this.m_subTextObjects.Length && this.m_subTextObjects[num] != null)
				{
					Renderer renderer2 = this.m_subTextObjects[num].renderer;
					if (renderer2 != null)
					{
						renderer2.material.SetFloat(ShaderUtilities.ShaderTag_CullMode, 0f);
					}
					num++;
				}
			}
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00004080 File Offset: 0x00002280
		private void SetPerspectiveCorrection()
		{
			if (this.m_isOrthographic)
			{
				this.m_sharedMaterial.SetFloat(ShaderUtilities.ID_PerspectiveFilter, 0f);
				return;
			}
			this.m_sharedMaterial.SetFloat(ShaderUtilities.ID_PerspectiveFilter, 0.875f);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x000040B8 File Offset: 0x000022B8
		internal override int SetArraySizes(TMP_Text.UnicodeChar[] unicodeChars)
		{
			int num = 0;
			this.m_totalCharacterCount = 0;
			this.m_isUsingBold = false;
			this.m_isParsingText = false;
			this.tag_NoParsing = false;
			this.m_FontStyleInternal = this.m_fontStyle;
			this.m_fontStyleStack.Clear();
			this.m_FontWeightInternal = (((this.m_FontStyleInternal & FontStyles.Bold) == FontStyles.Bold) ? FontWeight.Bold : this.m_fontWeight);
			this.m_FontWeightStack.SetDefault(this.m_FontWeightInternal);
			this.m_currentFontAsset = this.m_fontAsset;
			this.m_currentMaterial = this.m_sharedMaterial;
			this.m_currentMaterialIndex = 0;
			TMP_Text.m_materialReferenceStack.SetDefault(new MaterialReference(this.m_currentMaterialIndex, this.m_currentFontAsset, null, this.m_currentMaterial, this.m_padding));
			TMP_Text.m_materialReferenceIndexLookup.Clear();
			MaterialReference.AddMaterialReference(this.m_currentMaterial, this.m_currentFontAsset, ref TMP_Text.m_materialReferences, TMP_Text.m_materialReferenceIndexLookup);
			if (this.m_textInfo == null)
			{
				this.m_textInfo = new TMP_TextInfo(this.m_InternalTextProcessingArraySize);
			}
			else if (this.m_textInfo.characterInfo.Length < this.m_InternalTextProcessingArraySize)
			{
				TMP_TextInfo.Resize<TMP_CharacterInfo>(ref this.m_textInfo.characterInfo, this.m_InternalTextProcessingArraySize, false);
			}
			this.m_textElementType = TMP_TextElementType.Character;
			if (this.m_overflowMode == TextOverflowModes.Ellipsis)
			{
				base.GetEllipsisSpecialCharacter(this.m_currentFontAsset);
				if (this.m_Ellipsis.character != null)
				{
					if (this.m_Ellipsis.fontAsset.GetInstanceID() != this.m_currentFontAsset.GetInstanceID())
					{
						if (TMP_Settings.matchMaterialPreset && this.m_currentMaterial.GetInstanceID() != this.m_Ellipsis.fontAsset.material.GetInstanceID())
						{
							this.m_Ellipsis.material = TMP_MaterialManager.GetFallbackMaterial(this.m_currentMaterial, this.m_Ellipsis.fontAsset.material);
						}
						else
						{
							this.m_Ellipsis.material = this.m_Ellipsis.fontAsset.material;
						}
						this.m_Ellipsis.materialIndex = MaterialReference.AddMaterialReference(this.m_Ellipsis.material, this.m_Ellipsis.fontAsset, ref TMP_Text.m_materialReferences, TMP_Text.m_materialReferenceIndexLookup);
						TMP_Text.m_materialReferences[this.m_Ellipsis.materialIndex].referenceCount = 0;
					}
				}
				else
				{
					this.m_overflowMode = TextOverflowModes.Truncate;
					if (!TMP_Settings.warningsDisabled)
					{
						Debug.LogWarning("The character used for Ellipsis is not available in font asset [" + this.m_currentFontAsset.name + "] or any potential fallbacks. Switching Text Overflow mode to Truncate.", this);
					}
				}
			}
			if (this.m_overflowMode == TextOverflowModes.Linked && this.m_linkedTextComponent != null && !this.m_isCalculatingPreferredValues)
			{
				this.m_linkedTextComponent.text = string.Empty;
			}
			int num2 = 0;
			while (num2 < unicodeChars.Length && unicodeChars[num2].unicode != 0)
			{
				if (this.m_textInfo.characterInfo == null || this.m_totalCharacterCount >= this.m_textInfo.characterInfo.Length)
				{
					TMP_TextInfo.Resize<TMP_CharacterInfo>(ref this.m_textInfo.characterInfo, this.m_totalCharacterCount + 1, true);
				}
				int num3 = unicodeChars[num2].unicode;
				if (!this.m_isRichText || num3 != 60)
				{
					goto IL_4AE;
				}
				int currentMaterialIndex = this.m_currentMaterialIndex;
				int num4;
				if (!base.ValidateHtmlTag(unicodeChars, num2 + 1, out num4))
				{
					goto IL_4AE;
				}
				int stringIndex = unicodeChars[num2].stringIndex;
				num2 = num4;
				if ((this.m_FontStyleInternal & FontStyles.Bold) == FontStyles.Bold)
				{
					this.m_isUsingBold = true;
				}
				if (this.m_textElementType == TMP_TextElementType.Sprite)
				{
					MaterialReference[] materialReferences = TMP_Text.m_materialReferences;
					int currentMaterialIndex2 = this.m_currentMaterialIndex;
					materialReferences[currentMaterialIndex2].referenceCount = materialReferences[currentMaterialIndex2].referenceCount + 1;
					this.m_textInfo.characterInfo[this.m_totalCharacterCount].character = (char)(57344 + this.m_spriteIndex);
					this.m_textInfo.characterInfo[this.m_totalCharacterCount].spriteIndex = this.m_spriteIndex;
					this.m_textInfo.characterInfo[this.m_totalCharacterCount].fontAsset = this.m_currentFontAsset;
					this.m_textInfo.characterInfo[this.m_totalCharacterCount].spriteAsset = this.m_currentSpriteAsset;
					this.m_textInfo.characterInfo[this.m_totalCharacterCount].materialReferenceIndex = this.m_currentMaterialIndex;
					this.m_textInfo.characterInfo[this.m_totalCharacterCount].textElement = this.m_currentSpriteAsset.spriteCharacterTable[this.m_spriteIndex];
					this.m_textInfo.characterInfo[this.m_totalCharacterCount].elementType = this.m_textElementType;
					this.m_textInfo.characterInfo[this.m_totalCharacterCount].index = stringIndex;
					this.m_textInfo.characterInfo[this.m_totalCharacterCount].stringLength = unicodeChars[num2].stringIndex - stringIndex + 1;
					this.m_textElementType = TMP_TextElementType.Character;
					this.m_currentMaterialIndex = currentMaterialIndex;
					num++;
					this.m_totalCharacterCount++;
				}
				IL_B03:
				num2++;
				continue;
				IL_4AE:
				bool flag = false;
				TMP_FontAsset currentFontAsset = this.m_currentFontAsset;
				Material currentMaterial = this.m_currentMaterial;
				int currentMaterialIndex3 = this.m_currentMaterialIndex;
				if (this.m_textElementType == TMP_TextElementType.Character)
				{
					if ((this.m_FontStyleInternal & FontStyles.UpperCase) == FontStyles.UpperCase)
					{
						if (char.IsLower((char)num3))
						{
							num3 = (int)char.ToUpper((char)num3);
						}
					}
					else if ((this.m_FontStyleInternal & FontStyles.LowerCase) == FontStyles.LowerCase)
					{
						if (char.IsUpper((char)num3))
						{
							num3 = (int)char.ToLower((char)num3);
						}
					}
					else if ((this.m_FontStyleInternal & FontStyles.SmallCaps) == FontStyles.SmallCaps && char.IsLower((char)num3))
					{
						num3 = (int)char.ToUpper((char)num3);
					}
				}
				bool isUsingAlternateTypeface;
				TMP_TextElement tmp_TextElement = base.GetTextElement((uint)num3, this.m_currentFontAsset, this.m_FontStyleInternal, this.m_FontWeightInternal, out isUsingAlternateTypeface);
				if (tmp_TextElement == null)
				{
					int num5 = num3;
					num3 = (unicodeChars[num2].unicode = ((TMP_Settings.missingGlyphCharacter == 0) ? 9633 : TMP_Settings.missingGlyphCharacter));
					tmp_TextElement = TMP_FontAssetUtilities.GetCharacterFromFontAsset((uint)num3, this.m_currentFontAsset, true, this.m_FontStyleInternal, this.m_FontWeightInternal, out isUsingAlternateTypeface);
					if (tmp_TextElement == null && TMP_Settings.fallbackFontAssets != null && TMP_Settings.fallbackFontAssets.Count > 0)
					{
						tmp_TextElement = TMP_FontAssetUtilities.GetCharacterFromFontAssets((uint)num3, this.m_currentFontAsset, TMP_Settings.fallbackFontAssets, true, this.m_FontStyleInternal, this.m_FontWeightInternal, out isUsingAlternateTypeface);
					}
					if (tmp_TextElement == null && TMP_Settings.defaultFontAsset != null)
					{
						tmp_TextElement = TMP_FontAssetUtilities.GetCharacterFromFontAsset((uint)num3, TMP_Settings.defaultFontAsset, true, this.m_FontStyleInternal, this.m_FontWeightInternal, out isUsingAlternateTypeface);
					}
					if (tmp_TextElement == null)
					{
						num3 = (unicodeChars[num2].unicode = 32);
						tmp_TextElement = TMP_FontAssetUtilities.GetCharacterFromFontAsset((uint)num3, this.m_currentFontAsset, true, this.m_FontStyleInternal, this.m_FontWeightInternal, out isUsingAlternateTypeface);
					}
					if (tmp_TextElement == null)
					{
						num3 = (unicodeChars[num2].unicode = 3);
						tmp_TextElement = TMP_FontAssetUtilities.GetCharacterFromFontAsset((uint)num3, this.m_currentFontAsset, true, this.m_FontStyleInternal, this.m_FontWeightInternal, out isUsingAlternateTypeface);
					}
					if (!TMP_Settings.warningsDisabled)
					{
						Debug.LogWarning((num5 > 65535) ? string.Format("The character with Unicode value \\U{0:X8} was not found in the [{1}] font asset or any potential fallbacks. It was replaced by Unicode character \\u{2:X4} in text object [{3}].", new object[]
						{
							num5,
							this.m_fontAsset.name,
							tmp_TextElement.unicode,
							base.name
						}) : string.Format("The character with Unicode value \\u{0:X4} was not found in the [{1}] font asset or any potential fallbacks. It was replaced by Unicode character \\u{2:X4} in text object [{3}].", new object[]
						{
							num5,
							this.m_fontAsset.name,
							tmp_TextElement.unicode,
							base.name
						}), this);
					}
				}
				if (tmp_TextElement.elementType == TextElementType.Character && tmp_TextElement.textAsset.instanceID != this.m_currentFontAsset.instanceID)
				{
					flag = true;
					this.m_currentFontAsset = (tmp_TextElement.textAsset as TMP_FontAsset);
				}
				this.m_textInfo.characterInfo[this.m_totalCharacterCount].elementType = TMP_TextElementType.Character;
				this.m_textInfo.characterInfo[this.m_totalCharacterCount].textElement = tmp_TextElement;
				this.m_textInfo.characterInfo[this.m_totalCharacterCount].isUsingAlternateTypeface = isUsingAlternateTypeface;
				this.m_textInfo.characterInfo[this.m_totalCharacterCount].character = (char)num3;
				this.m_textInfo.characterInfo[this.m_totalCharacterCount].index = unicodeChars[num2].stringIndex;
				this.m_textInfo.characterInfo[this.m_totalCharacterCount].stringLength = unicodeChars[num2].length;
				this.m_textInfo.characterInfo[this.m_totalCharacterCount].fontAsset = this.m_currentFontAsset;
				if (tmp_TextElement.elementType == TextElementType.Sprite)
				{
					TMP_SpriteAsset tmp_SpriteAsset = tmp_TextElement.textAsset as TMP_SpriteAsset;
					this.m_currentMaterialIndex = MaterialReference.AddMaterialReference(tmp_SpriteAsset.material, tmp_SpriteAsset, ref TMP_Text.m_materialReferences, TMP_Text.m_materialReferenceIndexLookup);
					MaterialReference[] materialReferences2 = TMP_Text.m_materialReferences;
					int currentMaterialIndex4 = this.m_currentMaterialIndex;
					materialReferences2[currentMaterialIndex4].referenceCount = materialReferences2[currentMaterialIndex4].referenceCount + 1;
					this.m_textInfo.characterInfo[this.m_totalCharacterCount].elementType = TMP_TextElementType.Sprite;
					this.m_textInfo.characterInfo[this.m_totalCharacterCount].materialReferenceIndex = this.m_currentMaterialIndex;
					this.m_textInfo.characterInfo[this.m_totalCharacterCount].spriteAsset = tmp_SpriteAsset;
					this.m_textInfo.characterInfo[this.m_totalCharacterCount].spriteIndex = (int)tmp_TextElement.glyphIndex;
					this.m_textElementType = TMP_TextElementType.Character;
					this.m_currentMaterialIndex = currentMaterialIndex3;
					num++;
					this.m_totalCharacterCount++;
					goto IL_B03;
				}
				if (flag && this.m_currentFontAsset.instanceID != this.m_fontAsset.instanceID)
				{
					if (TMP_Settings.matchMaterialPreset)
					{
						this.m_currentMaterial = TMP_MaterialManager.GetFallbackMaterial(this.m_currentMaterial, this.m_currentFontAsset.material);
					}
					else
					{
						this.m_currentMaterial = this.m_currentFontAsset.material;
					}
					this.m_currentMaterialIndex = MaterialReference.AddMaterialReference(this.m_currentMaterial, this.m_currentFontAsset, ref TMP_Text.m_materialReferences, TMP_Text.m_materialReferenceIndexLookup);
				}
				if (tmp_TextElement != null && tmp_TextElement.glyph.atlasIndex > 0)
				{
					this.m_currentMaterial = TMP_MaterialManager.GetFallbackMaterial(this.m_currentFontAsset, this.m_currentMaterial, tmp_TextElement.glyph.atlasIndex);
					this.m_currentMaterialIndex = MaterialReference.AddMaterialReference(this.m_currentMaterial, this.m_currentFontAsset, ref TMP_Text.m_materialReferences, TMP_Text.m_materialReferenceIndexLookup);
					flag = true;
				}
				if (!char.IsWhiteSpace((char)num3) && num3 != 8203)
				{
					if (TMP_Text.m_materialReferences[this.m_currentMaterialIndex].referenceCount < 16383)
					{
						MaterialReference[] materialReferences3 = TMP_Text.m_materialReferences;
						int currentMaterialIndex5 = this.m_currentMaterialIndex;
						materialReferences3[currentMaterialIndex5].referenceCount = materialReferences3[currentMaterialIndex5].referenceCount + 1;
					}
					else
					{
						this.m_currentMaterialIndex = MaterialReference.AddMaterialReference(new Material(this.m_currentMaterial), this.m_currentFontAsset, ref TMP_Text.m_materialReferences, TMP_Text.m_materialReferenceIndexLookup);
						MaterialReference[] materialReferences4 = TMP_Text.m_materialReferences;
						int currentMaterialIndex6 = this.m_currentMaterialIndex;
						materialReferences4[currentMaterialIndex6].referenceCount = materialReferences4[currentMaterialIndex6].referenceCount + 1;
					}
				}
				this.m_textInfo.characterInfo[this.m_totalCharacterCount].material = this.m_currentMaterial;
				this.m_textInfo.characterInfo[this.m_totalCharacterCount].materialReferenceIndex = this.m_currentMaterialIndex;
				TMP_Text.m_materialReferences[this.m_currentMaterialIndex].isFallbackMaterial = flag;
				if (flag)
				{
					TMP_Text.m_materialReferences[this.m_currentMaterialIndex].fallbackMaterial = currentMaterial;
					this.m_currentFontAsset = currentFontAsset;
					this.m_currentMaterial = currentMaterial;
					this.m_currentMaterialIndex = currentMaterialIndex3;
				}
				this.m_totalCharacterCount++;
				goto IL_B03;
			}
			if (this.m_isCalculatingPreferredValues)
			{
				this.m_isCalculatingPreferredValues = false;
				return this.m_totalCharacterCount;
			}
			this.m_textInfo.spriteCount = num;
			int num6 = this.m_textInfo.materialCount = TMP_Text.m_materialReferenceIndexLookup.Count;
			if (num6 > this.m_textInfo.meshInfo.Length)
			{
				TMP_TextInfo.Resize<TMP_MeshInfo>(ref this.m_textInfo.meshInfo, num6, false);
			}
			if (num6 > this.m_subTextObjects.Length)
			{
				TMP_TextInfo.Resize<TMP_SubMesh>(ref this.m_subTextObjects, Mathf.NextPowerOfTwo(num6 + 1));
			}
			if (this.m_VertexBufferAutoSizeReduction && this.m_textInfo.characterInfo.Length - this.m_totalCharacterCount > 256)
			{
				TMP_TextInfo.Resize<TMP_CharacterInfo>(ref this.m_textInfo.characterInfo, Mathf.Max(this.m_totalCharacterCount + 1, 256), true);
			}
			for (int i = 0; i < num6; i++)
			{
				if (i > 0)
				{
					if (this.m_subTextObjects[i] == null)
					{
						this.m_subTextObjects[i] = TMP_SubMesh.AddSubTextObject(this, TMP_Text.m_materialReferences[i]);
						this.m_textInfo.meshInfo[i].vertices = null;
					}
					if (this.m_subTextObjects[i].sharedMaterial == null || this.m_subTextObjects[i].sharedMaterial.GetInstanceID() != TMP_Text.m_materialReferences[i].material.GetInstanceID())
					{
						this.m_subTextObjects[i].sharedMaterial = TMP_Text.m_materialReferences[i].material;
						this.m_subTextObjects[i].fontAsset = TMP_Text.m_materialReferences[i].fontAsset;
						this.m_subTextObjects[i].spriteAsset = TMP_Text.m_materialReferences[i].spriteAsset;
					}
					if (TMP_Text.m_materialReferences[i].isFallbackMaterial)
					{
						this.m_subTextObjects[i].fallbackMaterial = TMP_Text.m_materialReferences[i].material;
						this.m_subTextObjects[i].fallbackSourceMaterial = TMP_Text.m_materialReferences[i].fallbackMaterial;
					}
				}
				int referenceCount = TMP_Text.m_materialReferences[i].referenceCount;
				if (this.m_textInfo.meshInfo[i].vertices == null || this.m_textInfo.meshInfo[i].vertices.Length < referenceCount * 4)
				{
					if (this.m_textInfo.meshInfo[i].vertices == null)
					{
						if (i == 0)
						{
							this.m_textInfo.meshInfo[i] = new TMP_MeshInfo(this.m_mesh, referenceCount + 1);
						}
						else
						{
							this.m_textInfo.meshInfo[i] = new TMP_MeshInfo(this.m_subTextObjects[i].mesh, referenceCount + 1);
						}
					}
					else
					{
						this.m_textInfo.meshInfo[i].ResizeMeshInfo((referenceCount > 1024) ? (referenceCount + 256) : Mathf.NextPowerOfTwo(referenceCount + 1));
					}
				}
				else if (this.m_VertexBufferAutoSizeReduction && referenceCount > 0 && this.m_textInfo.meshInfo[i].vertices.Length / 4 - referenceCount > 256)
				{
					this.m_textInfo.meshInfo[i].ResizeMeshInfo((referenceCount > 1024) ? (referenceCount + 256) : Mathf.NextPowerOfTwo(referenceCount + 1));
				}
				this.m_textInfo.meshInfo[i].material = TMP_Text.m_materialReferences[i].material;
			}
			int num7 = num6;
			while (num7 < this.m_subTextObjects.Length && this.m_subTextObjects[num7] != null)
			{
				if (num7 < this.m_textInfo.meshInfo.Length)
				{
					this.m_textInfo.meshInfo[num7].ClearUnusedVertices(0, true);
				}
				num7++;
			}
			return this.m_totalCharacterCount;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00004FD0 File Offset: 0x000031D0
		public override void ComputeMarginSize()
		{
			if (base.rectTransform != null)
			{
				Rect rect = this.m_rectTransform.rect;
				this.m_marginWidth = rect.width - this.m_margin.x - this.m_margin.z;
				this.m_marginHeight = rect.height - this.m_margin.y - this.m_margin.w;
				this.m_PreviousRectTransformSize = rect.size;
				this.m_PreviousPivotPosition = this.m_rectTransform.pivot;
				this.m_RectTransformCorners = this.GetTextContainerLocalCorners();
			}
		}

		// Token: 0x0600008A RID: 138 RVA: 0x0000506E File Offset: 0x0000326E
		protected override void OnDidApplyAnimationProperties()
		{
			this.m_havePropertiesChanged = true;
			this.isMaskUpdateRequired = true;
			this.SetVerticesDirty();
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00005084 File Offset: 0x00003284
		protected override void OnTransformParentChanged()
		{
			this.SetVerticesDirty();
			this.SetLayoutDirty();
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00005094 File Offset: 0x00003294
		protected override void OnRectTransformDimensionsChange()
		{
			if (base.rectTransform != null && Mathf.Abs(this.m_rectTransform.rect.width - this.m_PreviousRectTransformSize.x) < 0.0001f && Mathf.Abs(this.m_rectTransform.rect.height - this.m_PreviousRectTransformSize.y) < 0.0001f && Mathf.Abs(this.m_rectTransform.pivot.x - this.m_PreviousPivotPosition.x) < 0.0001f && Mathf.Abs(this.m_rectTransform.pivot.y - this.m_PreviousPivotPosition.y) < 0.0001f)
			{
				return;
			}
			this.ComputeMarginSize();
			this.SetVerticesDirty();
			this.SetLayoutDirty();
		}

		// Token: 0x0600008D RID: 141 RVA: 0x0000516C File Offset: 0x0000336C
		internal override void InternalUpdate()
		{
			if (!this.m_havePropertiesChanged)
			{
				float y = this.m_rectTransform.lossyScale.y;
				if (Mathf.Abs(y - this.m_previousLossyScaleY) > 0.0001f && this.m_TextProcessingArray[0].unicode != 0)
				{
					float scaleDelta = y / this.m_previousLossyScaleY;
					this.UpdateSDFScale(scaleDelta);
					this.m_previousLossyScaleY = y;
				}
			}
			if (this.m_isUsingLegacyAnimationComponent)
			{
				this.m_havePropertiesChanged = true;
				this.OnPreRenderObject();
			}
		}

		// Token: 0x0600008E RID: 142 RVA: 0x000051E8 File Offset: 0x000033E8
		private void OnPreRenderObject()
		{
			if (!this.m_isAwake || (!this.IsActive() && !this.m_ignoreActiveState))
			{
				return;
			}
			if (this.m_fontAsset == null)
			{
				Debug.LogWarning("Please assign a Font Asset to this " + this.transform.name + " gameobject.", this);
				return;
			}
			if (this.m_havePropertiesChanged || this.m_isLayoutDirty)
			{
				if (this.isMaskUpdateRequired)
				{
					this.UpdateMask();
					this.isMaskUpdateRequired = false;
				}
				if (this.checkPaddingRequired)
				{
					this.UpdateMeshPadding();
				}
				base.ParseInputText();
				TMP_FontAsset.UpdateFontFeaturesForFontAssetsInQueue();
				if (this.m_enableAutoSizing)
				{
					this.m_fontSize = Mathf.Clamp(this.m_fontSizeBase, this.m_fontSizeMin, this.m_fontSizeMax);
				}
				this.m_maxFontSize = this.m_fontSizeMax;
				this.m_minFontSize = this.m_fontSizeMin;
				this.m_lineSpacingDelta = 0f;
				this.m_charWidthAdjDelta = 0f;
				this.m_isTextTruncated = false;
				this.m_havePropertiesChanged = false;
				this.m_isLayoutDirty = false;
				this.m_ignoreActiveState = false;
				this.m_IsAutoSizePointSizeSet = false;
				this.m_AutoSizeIterationCount = 0;
				this.SetActiveSubTextObjectRenderers(this.m_renderer.enabled);
				while (!this.m_IsAutoSizePointSizeSet)
				{
					this.GenerateTextMesh();
					this.m_AutoSizeIterationCount++;
				}
			}
		}

		// Token: 0x0600008F RID: 143 RVA: 0x0000532C File Offset: 0x0000352C
		protected virtual void GenerateTextMesh()
		{
			if (this.m_fontAsset == null || this.m_fontAsset.characterLookupTable == null)
			{
				Debug.LogWarning("Can't Generate Mesh! No Font Asset has been assigned to Object ID: " + base.GetInstanceID().ToString());
				this.m_IsAutoSizePointSizeSet = true;
				return;
			}
			if (this.m_textInfo != null)
			{
				this.m_textInfo.Clear();
			}
			if (this.m_TextProcessingArray == null || this.m_TextProcessingArray.Length == 0 || this.m_TextProcessingArray[0].unicode == 0)
			{
				this.ClearMesh(true);
				this.m_preferredWidth = 0f;
				this.m_preferredHeight = 0f;
				TMPro_EventManager.ON_TEXT_CHANGED(this);
				this.m_IsAutoSizePointSizeSet = true;
				return;
			}
			this.m_currentFontAsset = this.m_fontAsset;
			this.m_currentMaterial = this.m_sharedMaterial;
			this.m_currentMaterialIndex = 0;
			TMP_Text.m_materialReferenceStack.SetDefault(new MaterialReference(this.m_currentMaterialIndex, this.m_currentFontAsset, null, this.m_currentMaterial, this.m_padding));
			this.m_currentSpriteAsset = this.m_spriteAsset;
			if (this.m_spriteAnimator != null)
			{
				this.m_spriteAnimator.StopAllAnimations();
			}
			int totalCharacterCount = this.m_totalCharacterCount;
			float num = this.m_fontSize / (float)this.m_fontAsset.m_FaceInfo.pointSize * this.m_fontAsset.m_FaceInfo.scale * (this.m_isOrthographic ? 1f : 0.1f);
			float num2 = num;
			float num3 = this.m_fontSize * 0.01f * (this.m_isOrthographic ? 1f : 0.1f);
			this.m_fontScaleMultiplier = 1f;
			this.m_currentFontSize = this.m_fontSize;
			this.m_sizeStack.SetDefault(this.m_currentFontSize);
			int num4 = 0;
			this.m_FontStyleInternal = this.m_fontStyle;
			this.m_FontWeightInternal = (((this.m_FontStyleInternal & FontStyles.Bold) == FontStyles.Bold) ? FontWeight.Bold : this.m_fontWeight);
			this.m_FontWeightStack.SetDefault(this.m_FontWeightInternal);
			this.m_fontStyleStack.Clear();
			this.m_lineJustification = this.m_HorizontalAlignment;
			this.m_lineJustificationStack.SetDefault(this.m_lineJustification);
			float num5 = 0f;
			this.m_baselineOffset = 0f;
			this.m_baselineOffsetStack.Clear();
			bool flag = false;
			Vector3 zero = Vector3.zero;
			Vector3 zero2 = Vector3.zero;
			bool flag2 = false;
			Vector3 zero3 = Vector3.zero;
			Vector3 zero4 = Vector3.zero;
			bool flag3 = false;
			Vector3 vector = Vector3.zero;
			Vector3 vector2 = Vector3.zero;
			this.m_fontColor32 = this.m_fontColor;
			this.m_htmlColor = this.m_fontColor32;
			this.m_underlineColor = this.m_htmlColor;
			this.m_strikethroughColor = this.m_htmlColor;
			this.m_colorStack.SetDefault(this.m_htmlColor);
			this.m_underlineColorStack.SetDefault(this.m_htmlColor);
			this.m_strikethroughColorStack.SetDefault(this.m_htmlColor);
			this.m_HighlightStateStack.SetDefault(new HighlightState(this.m_htmlColor, TMP_Offset.zero));
			this.m_colorGradientPreset = null;
			this.m_colorGradientStack.SetDefault(null);
			this.m_ItalicAngle = (int)this.m_currentFontAsset.italicStyle;
			this.m_ItalicAngleStack.SetDefault(this.m_ItalicAngle);
			this.m_actionStack.Clear();
			this.m_isFXMatrixSet = false;
			this.m_lineOffset = 0f;
			this.m_lineHeight = -32767f;
			float num6 = this.m_currentFontAsset.m_FaceInfo.lineHeight - (this.m_currentFontAsset.m_FaceInfo.ascentLine - this.m_currentFontAsset.m_FaceInfo.descentLine);
			this.m_cSpacing = 0f;
			this.m_monoSpacing = 0f;
			this.m_xAdvance = 0f;
			this.tag_LineIndent = 0f;
			this.tag_Indent = 0f;
			this.m_indentStack.SetDefault(0f);
			this.tag_NoParsing = false;
			this.m_characterCount = 0;
			this.m_firstCharacterOfLine = this.m_firstVisibleCharacter;
			this.m_lastCharacterOfLine = 0;
			this.m_firstVisibleCharacterOfLine = 0;
			this.m_lastVisibleCharacterOfLine = 0;
			this.m_maxLineAscender = TMP_Text.k_LargeNegativeFloat;
			this.m_maxLineDescender = TMP_Text.k_LargePositiveFloat;
			this.m_lineNumber = 0;
			this.m_startOfLineAscender = 0f;
			this.m_startOfLineDescender = 0f;
			this.m_lineVisibleCharacterCount = 0;
			bool flag4 = true;
			this.m_IsDrivenLineSpacing = false;
			this.m_firstOverflowCharacterIndex = -1;
			this.m_pageNumber = 0;
			int num7 = Mathf.Clamp(this.m_pageToDisplay - 1, 0, this.m_textInfo.pageInfo.Length - 1);
			this.m_textInfo.ClearPageInfo();
			Vector4 margin = this.m_margin;
			float num8 = (this.m_marginWidth > 0f) ? this.m_marginWidth : 0f;
			float num9 = (this.m_marginHeight > 0f) ? this.m_marginHeight : 0f;
			this.m_marginLeft = 0f;
			this.m_marginRight = 0f;
			this.m_width = -1f;
			float num10 = num8 + 0.0001f - this.m_marginLeft - this.m_marginRight;
			this.m_meshExtents.min = TMP_Text.k_LargePositiveVector2;
			this.m_meshExtents.max = TMP_Text.k_LargeNegativeVector2;
			this.m_textInfo.ClearLineInfo();
			this.m_maxCapHeight = 0f;
			this.m_maxTextAscender = 0f;
			this.m_ElementDescender = 0f;
			this.m_PageAscender = 0f;
			float num11 = 0f;
			bool flag5 = false;
			this.m_isNewPage = false;
			bool flag6 = true;
			this.m_isNonBreakingSpace = false;
			bool flag7 = false;
			int num12 = 0;
			TMP_Text.CharacterSubstitution characterSubstitution = new TMP_Text.CharacterSubstitution(-1, 0U);
			bool flag8 = false;
			base.SaveWordWrappingState(ref TMP_Text.m_SavedWordWrapState, -1, -1);
			base.SaveWordWrappingState(ref TMP_Text.m_SavedLineState, -1, -1);
			base.SaveWordWrappingState(ref TMP_Text.m_SavedEllipsisState, -1, -1);
			base.SaveWordWrappingState(ref TMP_Text.m_SavedLastValidState, -1, -1);
			base.SaveWordWrappingState(ref TMP_Text.m_SavedSoftLineBreakState, -1, -1);
			TMP_Text.m_EllipsisInsertionCandidateStack.Clear();
			int num13 = 0;
			int num14 = 0;
			while (num14 < this.m_TextProcessingArray.Length && this.m_TextProcessingArray[num14].unicode != 0)
			{
				num4 = this.m_TextProcessingArray[num14].unicode;
				if (num13 > 5)
				{
					Debug.LogError("Line breaking recursion max threshold hit... Character [" + num4.ToString() + "] index: " + num14.ToString());
					characterSubstitution.index = this.m_characterCount;
					characterSubstitution.unicode = 3U;
				}
				if (!this.m_isRichText || num4 != 60)
				{
					this.m_textElementType = this.m_textInfo.characterInfo[this.m_characterCount].elementType;
					this.m_currentMaterialIndex = this.m_textInfo.characterInfo[this.m_characterCount].materialReferenceIndex;
					this.m_currentFontAsset = this.m_textInfo.characterInfo[this.m_characterCount].fontAsset;
					goto IL_6B9;
				}
				this.m_isParsingText = true;
				this.m_textElementType = TMP_TextElementType.Character;
				int num15;
				if (!base.ValidateHtmlTag(this.m_TextProcessingArray, num14 + 1, out num15))
				{
					goto IL_6B9;
				}
				num14 = num15;
				if (this.m_textElementType != TMP_TextElementType.Character)
				{
					goto IL_6B9;
				}
				IL_3BD5:
				num14++;
				continue;
				IL_6B9:
				int currentMaterialIndex = this.m_currentMaterialIndex;
				bool isUsingAlternateTypeface = this.m_textInfo.characterInfo[this.m_characterCount].isUsingAlternateTypeface;
				this.m_isParsingText = false;
				bool flag9 = false;
				if (characterSubstitution.index == this.m_characterCount)
				{
					num4 = (int)characterSubstitution.unicode;
					this.m_textElementType = TMP_TextElementType.Character;
					flag9 = true;
					if (num4 != 3)
					{
						if (num4 != 45)
						{
							if (num4 == 8230)
							{
								this.m_textInfo.characterInfo[this.m_characterCount].textElement = this.m_Ellipsis.character;
								this.m_textInfo.characterInfo[this.m_characterCount].elementType = TMP_TextElementType.Character;
								this.m_textInfo.characterInfo[this.m_characterCount].fontAsset = this.m_Ellipsis.fontAsset;
								this.m_textInfo.characterInfo[this.m_characterCount].material = this.m_Ellipsis.material;
								this.m_textInfo.characterInfo[this.m_characterCount].materialReferenceIndex = this.m_Ellipsis.materialIndex;
								this.m_isTextTruncated = true;
								characterSubstitution.index = this.m_characterCount + 1;
								characterSubstitution.unicode = 3U;
							}
						}
					}
					else
					{
						this.m_textInfo.characterInfo[this.m_characterCount].textElement = this.m_currentFontAsset.characterLookupTable[3U];
						this.m_isTextTruncated = true;
					}
				}
				if (this.m_characterCount < this.m_firstVisibleCharacter && num4 != 3)
				{
					this.m_textInfo.characterInfo[this.m_characterCount].isVisible = false;
					this.m_textInfo.characterInfo[this.m_characterCount].character = '​';
					this.m_textInfo.characterInfo[this.m_characterCount].lineNumber = 0;
					this.m_characterCount++;
					goto IL_3BD5;
				}
				float num16 = 1f;
				if (this.m_textElementType == TMP_TextElementType.Character)
				{
					if ((this.m_FontStyleInternal & FontStyles.UpperCase) == FontStyles.UpperCase)
					{
						if (char.IsLower((char)num4))
						{
							num4 = (int)char.ToUpper((char)num4);
						}
					}
					else if ((this.m_FontStyleInternal & FontStyles.LowerCase) == FontStyles.LowerCase)
					{
						if (char.IsUpper((char)num4))
						{
							num4 = (int)char.ToLower((char)num4);
						}
					}
					else if ((this.m_FontStyleInternal & FontStyles.SmallCaps) == FontStyles.SmallCaps && char.IsLower((char)num4))
					{
						num16 = 0.8f;
						num4 = (int)char.ToUpper((char)num4);
					}
				}
				float num17 = 0f;
				float num18 = 0f;
				float num19 = 0f;
				if (this.m_textElementType == TMP_TextElementType.Sprite)
				{
					this.m_currentSpriteAsset = this.m_textInfo.characterInfo[this.m_characterCount].spriteAsset;
					this.m_spriteIndex = this.m_textInfo.characterInfo[this.m_characterCount].spriteIndex;
					TMP_SpriteCharacter tmp_SpriteCharacter = this.m_currentSpriteAsset.spriteCharacterTable[this.m_spriteIndex];
					if (tmp_SpriteCharacter == null)
					{
						goto IL_3BD5;
					}
					if (num4 == 60)
					{
						num4 = 57344 + this.m_spriteIndex;
					}
					else
					{
						this.m_spriteColor = TMP_Text.s_colorWhite;
					}
					float num20 = this.m_currentFontSize / (float)this.m_currentFontAsset.faceInfo.pointSize * this.m_currentFontAsset.faceInfo.scale * (this.m_isOrthographic ? 1f : 0.1f);
					if (this.m_currentSpriteAsset.m_FaceInfo.pointSize > 0)
					{
						float num21 = this.m_currentFontSize / (float)this.m_currentSpriteAsset.m_FaceInfo.pointSize * this.m_currentSpriteAsset.m_FaceInfo.scale * (this.m_isOrthographic ? 1f : 0.1f);
						num2 = tmp_SpriteCharacter.m_Scale * tmp_SpriteCharacter.m_Glyph.scale * num21;
						num18 = this.m_currentSpriteAsset.m_FaceInfo.ascentLine;
						num17 = this.m_currentSpriteAsset.m_FaceInfo.baseline * num20 * this.m_fontScaleMultiplier * this.m_currentSpriteAsset.m_FaceInfo.scale;
						num19 = this.m_currentSpriteAsset.m_FaceInfo.descentLine;
					}
					else
					{
						float num22 = this.m_currentFontSize / (float)this.m_currentFontAsset.m_FaceInfo.pointSize * this.m_currentFontAsset.m_FaceInfo.scale * (this.m_isOrthographic ? 1f : 0.1f);
						num2 = this.m_currentFontAsset.m_FaceInfo.ascentLine / tmp_SpriteCharacter.m_Glyph.metrics.height * tmp_SpriteCharacter.m_Scale * tmp_SpriteCharacter.m_Glyph.scale * num22;
						float num23 = num22 / num2;
						num18 = this.m_currentFontAsset.m_FaceInfo.ascentLine * num23;
						num17 = this.m_currentFontAsset.m_FaceInfo.baseline * num20 * this.m_fontScaleMultiplier * this.m_currentFontAsset.m_FaceInfo.scale;
						num19 = this.m_currentFontAsset.m_FaceInfo.descentLine * num23;
					}
					this.m_cached_TextElement = tmp_SpriteCharacter;
					this.m_textInfo.characterInfo[this.m_characterCount].elementType = TMP_TextElementType.Sprite;
					this.m_textInfo.characterInfo[this.m_characterCount].scale = num2;
					this.m_textInfo.characterInfo[this.m_characterCount].spriteAsset = this.m_currentSpriteAsset;
					this.m_textInfo.characterInfo[this.m_characterCount].fontAsset = this.m_currentFontAsset;
					this.m_textInfo.characterInfo[this.m_characterCount].materialReferenceIndex = this.m_currentMaterialIndex;
					this.m_currentMaterialIndex = currentMaterialIndex;
					num5 = 0f;
				}
				else if (this.m_textElementType == TMP_TextElementType.Character)
				{
					this.m_cached_TextElement = this.m_textInfo.characterInfo[this.m_characterCount].textElement;
					if (this.m_cached_TextElement == null)
					{
						goto IL_3BD5;
					}
					this.m_currentFontAsset = this.m_textInfo.characterInfo[this.m_characterCount].fontAsset;
					this.m_currentMaterial = this.m_textInfo.characterInfo[this.m_characterCount].material;
					this.m_currentMaterialIndex = this.m_textInfo.characterInfo[this.m_characterCount].materialReferenceIndex;
					float num24;
					if (flag9 && this.m_TextProcessingArray[num14].unicode == 10 && this.m_characterCount != this.m_firstCharacterOfLine)
					{
						num24 = this.m_textInfo.characterInfo[this.m_characterCount - 1].pointSize * num16 / (float)this.m_currentFontAsset.m_FaceInfo.pointSize * this.m_currentFontAsset.m_FaceInfo.scale * (this.m_isOrthographic ? 1f : 0.1f);
					}
					else
					{
						num24 = this.m_currentFontSize * num16 / (float)this.m_currentFontAsset.m_FaceInfo.pointSize * this.m_currentFontAsset.m_FaceInfo.scale * (this.m_isOrthographic ? 1f : 0.1f);
					}
					if (flag9 && num4 == 8230)
					{
						num18 = 0f;
						num19 = 0f;
					}
					else
					{
						num18 = this.m_currentFontAsset.m_FaceInfo.ascentLine;
						num19 = this.m_currentFontAsset.m_FaceInfo.descentLine;
					}
					num2 = num24 * this.m_fontScaleMultiplier * this.m_cached_TextElement.m_Scale * this.m_cached_TextElement.m_Glyph.scale;
					num17 = this.m_currentFontAsset.m_FaceInfo.baseline * num24 * this.m_fontScaleMultiplier * this.m_currentFontAsset.m_FaceInfo.scale;
					this.m_textInfo.characterInfo[this.m_characterCount].elementType = TMP_TextElementType.Character;
					this.m_textInfo.characterInfo[this.m_characterCount].scale = num2;
					num5 = ((this.m_currentMaterialIndex == 0) ? this.m_padding : this.m_subTextObjects[this.m_currentMaterialIndex].padding);
				}
				float num25 = num2;
				if (num4 == 173 || num4 == 3)
				{
					num2 = 0f;
				}
				this.m_textInfo.characterInfo[this.m_characterCount].character = (char)num4;
				this.m_textInfo.characterInfo[this.m_characterCount].pointSize = this.m_currentFontSize;
				this.m_textInfo.characterInfo[this.m_characterCount].color = this.m_htmlColor;
				this.m_textInfo.characterInfo[this.m_characterCount].underlineColor = this.m_underlineColor;
				this.m_textInfo.characterInfo[this.m_characterCount].strikethroughColor = this.m_strikethroughColor;
				this.m_textInfo.characterInfo[this.m_characterCount].highlightState = this.m_HighlightStateStack.current;
				this.m_textInfo.characterInfo[this.m_characterCount].style = this.m_FontStyleInternal;
				GlyphMetrics metrics = this.m_cached_TextElement.m_Glyph.metrics;
				bool flag10 = num4 <= 65535 && char.IsWhiteSpace((char)num4);
				TMP_GlyphValueRecord tmp_GlyphValueRecord = default(TMP_GlyphValueRecord);
				float num26 = this.m_characterSpacing;
				this.m_GlyphHorizontalAdvanceAdjustment = 0f;
				if (this.m_enableKerning)
				{
					uint glyphIndex = this.m_cached_TextElement.m_GlyphIndex;
					if (this.m_characterCount < totalCharacterCount - 1)
					{
						uint key = this.m_textInfo.characterInfo[this.m_characterCount + 1].textElement.m_GlyphIndex << 16 | glyphIndex;
						TMP_GlyphPairAdjustmentRecord tmp_GlyphPairAdjustmentRecord;
						if (this.m_currentFontAsset.m_FontFeatureTable.m_GlyphPairAdjustmentRecordLookupDictionary.TryGetValue(key, out tmp_GlyphPairAdjustmentRecord))
						{
							tmp_GlyphValueRecord = tmp_GlyphPairAdjustmentRecord.m_FirstAdjustmentRecord.m_GlyphValueRecord;
							num26 = (((tmp_GlyphPairAdjustmentRecord.m_FeatureLookupFlags & FontFeatureLookupFlags.IgnoreSpacingAdjustments) == FontFeatureLookupFlags.IgnoreSpacingAdjustments) ? 0f : num26);
						}
					}
					if (this.m_characterCount >= 1)
					{
						uint glyphIndex2 = this.m_textInfo.characterInfo[this.m_characterCount - 1].textElement.m_GlyphIndex;
						uint key2 = glyphIndex << 16 | glyphIndex2;
						TMP_GlyphPairAdjustmentRecord tmp_GlyphPairAdjustmentRecord;
						if (this.m_currentFontAsset.m_FontFeatureTable.m_GlyphPairAdjustmentRecordLookupDictionary.TryGetValue(key2, out tmp_GlyphPairAdjustmentRecord))
						{
							tmp_GlyphValueRecord += tmp_GlyphPairAdjustmentRecord.m_SecondAdjustmentRecord.m_GlyphValueRecord;
							num26 = (((tmp_GlyphPairAdjustmentRecord.m_FeatureLookupFlags & FontFeatureLookupFlags.IgnoreSpacingAdjustments) == FontFeatureLookupFlags.IgnoreSpacingAdjustments) ? 0f : num26);
						}
					}
					this.m_GlyphHorizontalAdvanceAdjustment = tmp_GlyphValueRecord.xAdvance;
				}
				if (this.m_isRightToLeft)
				{
					this.m_xAdvance -= metrics.horizontalAdvance * (1f - this.m_charWidthAdjDelta) * num2;
					if (flag10 || num4 == 8203)
					{
						this.m_xAdvance -= this.m_wordSpacing * num3;
					}
				}
				float num27 = 0f;
				if (this.m_monoSpacing != 0f)
				{
					num27 = (this.m_monoSpacing / 2f - (metrics.width / 2f + metrics.horizontalBearingX) * num2) * (1f - this.m_charWidthAdjDelta);
					this.m_xAdvance += num27;
				}
				float num28;
				float num29;
				if (this.m_textElementType == TMP_TextElementType.Character && !isUsingAlternateTypeface && (this.m_FontStyleInternal & FontStyles.Bold) == FontStyles.Bold)
				{
					if (this.m_currentMaterial != null && this.m_currentMaterial.HasProperty(ShaderUtilities.ID_GradientScale))
					{
						float @float = this.m_currentMaterial.GetFloat(ShaderUtilities.ID_GradientScale);
						num28 = this.m_currentFontAsset.boldStyle / 4f * @float * this.m_currentMaterial.GetFloat(ShaderUtilities.ID_ScaleRatio_A);
						if (num28 + num5 > @float)
						{
							num5 = @float - num28;
						}
					}
					else
					{
						num28 = 0f;
					}
					num29 = this.m_currentFontAsset.boldSpacing;
				}
				else
				{
					if (this.m_currentMaterial != null && this.m_currentMaterial.HasProperty(ShaderUtilities.ID_GradientScale) && this.m_currentMaterial.HasProperty(ShaderUtilities.ID_ScaleRatio_A))
					{
						float float2 = this.m_currentMaterial.GetFloat(ShaderUtilities.ID_GradientScale);
						num28 = this.m_currentFontAsset.normalStyle / 4f * float2 * this.m_currentMaterial.GetFloat(ShaderUtilities.ID_ScaleRatio_A);
						if (num28 + num5 > float2)
						{
							num5 = float2 - num28;
						}
					}
					else
					{
						num28 = 0f;
					}
					num29 = 0f;
				}
				Vector3 vector3;
				vector3.x = this.m_xAdvance + (metrics.horizontalBearingX - num5 - num28 + tmp_GlyphValueRecord.m_XPlacement) * num2 * (1f - this.m_charWidthAdjDelta);
				vector3.y = num17 + (metrics.horizontalBearingY + num5 + tmp_GlyphValueRecord.m_YPlacement) * num2 - this.m_lineOffset + this.m_baselineOffset;
				vector3.z = 0f;
				Vector3 vector4;
				vector4.x = vector3.x;
				vector4.y = vector3.y - (metrics.height + num5 * 2f) * num2;
				vector4.z = 0f;
				Vector3 vector5;
				vector5.x = vector4.x + (metrics.width + num5 * 2f + num28 * 2f) * num2 * (1f - this.m_charWidthAdjDelta);
				vector5.y = vector3.y;
				vector5.z = 0f;
				Vector3 vector6;
				vector6.x = vector5.x;
				vector6.y = vector4.y;
				vector6.z = 0f;
				if (this.m_textElementType == TMP_TextElementType.Character && !isUsingAlternateTypeface && (this.m_FontStyleInternal & FontStyles.Italic) == FontStyles.Italic)
				{
					float num30 = (float)this.m_ItalicAngle * 0.01f;
					Vector3 vector7 = new Vector3(num30 * ((metrics.horizontalBearingY + num5 + num28) * num2), 0f, 0f);
					Vector3 vector8 = new Vector3(num30 * ((metrics.horizontalBearingY - metrics.height - num5 - num28) * num2), 0f, 0f);
					Vector3 b = new Vector3((vector7.x - vector8.x) / 2f, 0f, 0f);
					vector3 = vector3 + vector7 - b;
					vector4 = vector4 + vector8 - b;
					vector5 = vector5 + vector7 - b;
					vector6 = vector6 + vector8 - b;
				}
				if (this.m_isFXMatrixSet)
				{
					float x = this.m_FXMatrix.lossyScale.x;
					Vector3 b2 = (vector5 + vector4) / 2f;
					vector3 = this.m_FXMatrix.MultiplyPoint3x4(vector3 - b2) + b2;
					vector4 = this.m_FXMatrix.MultiplyPoint3x4(vector4 - b2) + b2;
					vector5 = this.m_FXMatrix.MultiplyPoint3x4(vector5 - b2) + b2;
					vector6 = this.m_FXMatrix.MultiplyPoint3x4(vector6 - b2) + b2;
				}
				this.m_textInfo.characterInfo[this.m_characterCount].bottomLeft = vector4;
				this.m_textInfo.characterInfo[this.m_characterCount].topLeft = vector3;
				this.m_textInfo.characterInfo[this.m_characterCount].topRight = vector5;
				this.m_textInfo.characterInfo[this.m_characterCount].bottomRight = vector6;
				this.m_textInfo.characterInfo[this.m_characterCount].origin = this.m_xAdvance;
				this.m_textInfo.characterInfo[this.m_characterCount].baseLine = num17 - this.m_lineOffset + this.m_baselineOffset;
				this.m_textInfo.characterInfo[this.m_characterCount].aspectRatio = (vector5.x - vector4.x) / (vector3.y - vector4.y);
				float num31 = (this.m_textElementType == TMP_TextElementType.Character) ? (num18 * num2 / num16 + this.m_baselineOffset) : (num18 * num2 + this.m_baselineOffset);
				float num32 = (this.m_textElementType == TMP_TextElementType.Character) ? (num19 * num2 / num16 + this.m_baselineOffset) : (num19 * num2 + this.m_baselineOffset);
				float num33 = num31;
				float num34 = num32;
				bool flag11 = this.m_characterCount == this.m_firstCharacterOfLine;
				if (flag11 || !flag10)
				{
					if (this.m_baselineOffset != 0f)
					{
						num33 = Mathf.Max((num31 - this.m_baselineOffset) / this.m_fontScaleMultiplier, num33);
						num34 = Mathf.Min((num32 - this.m_baselineOffset) / this.m_fontScaleMultiplier, num34);
					}
					this.m_maxLineAscender = Mathf.Max(num33, this.m_maxLineAscender);
					this.m_maxLineDescender = Mathf.Min(num34, this.m_maxLineDescender);
				}
				if (flag11 || !flag10)
				{
					this.m_textInfo.characterInfo[this.m_characterCount].adjustedAscender = num33;
					this.m_textInfo.characterInfo[this.m_characterCount].adjustedDescender = num34;
					this.m_ElementAscender = (this.m_textInfo.characterInfo[this.m_characterCount].ascender = num31 - this.m_lineOffset);
					this.m_ElementDescender = (this.m_textInfo.characterInfo[this.m_characterCount].descender = num32 - this.m_lineOffset);
				}
				else
				{
					this.m_textInfo.characterInfo[this.m_characterCount].adjustedAscender = this.m_maxLineAscender;
					this.m_textInfo.characterInfo[this.m_characterCount].adjustedDescender = this.m_maxLineDescender;
					this.m_ElementAscender = (this.m_textInfo.characterInfo[this.m_characterCount].ascender = this.m_maxLineAscender - this.m_lineOffset);
					this.m_ElementDescender = (this.m_textInfo.characterInfo[this.m_characterCount].descender = this.m_maxLineDescender - this.m_lineOffset);
				}
				if ((this.m_lineNumber == 0 || this.m_isNewPage) && (flag11 || !flag10))
				{
					this.m_maxTextAscender = this.m_maxLineAscender;
					this.m_maxCapHeight = Mathf.Max(this.m_maxCapHeight, this.m_currentFontAsset.m_FaceInfo.capLine * num2 / num16);
				}
				if (this.m_lineOffset == 0f && (flag11 || !flag10))
				{
					this.m_PageAscender = ((this.m_PageAscender > num31) ? this.m_PageAscender : num31);
				}
				this.m_textInfo.characterInfo[this.m_characterCount].isVisible = false;
				bool flag12 = (this.m_lineJustification & HorizontalAlignmentOptions.Flush) == HorizontalAlignmentOptions.Flush || (this.m_lineJustification & HorizontalAlignmentOptions.Justified) == HorizontalAlignmentOptions.Justified;
				if (num4 == 9 || (!flag10 && num4 != 8203 && num4 != 173 && num4 != 3) || (num4 == 173 && !flag8) || this.m_textElementType == TMP_TextElementType.Sprite)
				{
					this.m_textInfo.characterInfo[this.m_characterCount].isVisible = true;
					float marginLeft = this.m_marginLeft;
					float marginRight = this.m_marginRight;
					if (flag9)
					{
						marginLeft = this.m_textInfo.lineInfo[this.m_lineNumber].marginLeft;
						marginRight = this.m_textInfo.lineInfo[this.m_lineNumber].marginRight;
					}
					num10 = ((this.m_width != -1f) ? Mathf.Min(num8 + 0.0001f - marginLeft - marginRight, this.m_width) : (num8 + 0.0001f - marginLeft - marginRight));
					float num35 = Mathf.Abs(this.m_xAdvance) + ((!this.m_isRightToLeft) ? metrics.horizontalAdvance : 0f) * (1f - this.m_charWidthAdjDelta) * ((num4 == 173) ? num25 : num2);
					float num36 = this.m_maxTextAscender - (this.m_maxLineDescender - this.m_lineOffset) + ((this.m_lineOffset > 0f && !this.m_IsDrivenLineSpacing) ? (this.m_maxLineAscender - this.m_startOfLineAscender) : 0f);
					int characterCount = this.m_characterCount;
					if (num36 > num9 + 0.0001f)
					{
						if (this.m_firstOverflowCharacterIndex == -1)
						{
							this.m_firstOverflowCharacterIndex = this.m_characterCount;
						}
						if (this.m_enableAutoSizing)
						{
							if (this.m_lineSpacingDelta > this.m_lineSpacingMax && this.m_lineOffset > 0f && this.m_AutoSizeIterationCount < this.m_AutoSizeMaxIterationCount)
							{
								float num37 = (num9 - num36) / (float)this.m_lineNumber;
								this.m_lineSpacingDelta = Mathf.Max(this.m_lineSpacingDelta + num37 / num, this.m_lineSpacingMax);
								return;
							}
							if (this.m_fontSize > this.m_fontSizeMin && this.m_AutoSizeIterationCount < this.m_AutoSizeMaxIterationCount)
							{
								this.m_maxFontSize = this.m_fontSize;
								float num38 = Mathf.Max((this.m_fontSize - this.m_minFontSize) / 2f, 0.05f);
								this.m_fontSize -= num38;
								this.m_fontSize = Mathf.Max((float)((int)(this.m_fontSize * 20f + 0.5f)) / 20f, this.m_fontSizeMin);
								return;
							}
						}
						switch (this.m_overflowMode)
						{
						case TextOverflowModes.Ellipsis:
						{
							if (TMP_Text.m_EllipsisInsertionCandidateStack.Count == 0)
							{
								num14 = -1;
								this.m_characterCount = 0;
								characterSubstitution.index = 0;
								characterSubstitution.unicode = 3U;
								this.m_firstCharacterOfLine = 0;
								goto IL_3BD5;
							}
							WordWrapState wordWrapState = TMP_Text.m_EllipsisInsertionCandidateStack.Pop();
							num14 = base.RestoreWordWrappingState(ref wordWrapState);
							num14--;
							this.m_characterCount--;
							characterSubstitution.index = this.m_characterCount;
							characterSubstitution.unicode = 8230U;
							num13++;
							goto IL_3BD5;
						}
						case TextOverflowModes.Truncate:
							num14 = base.RestoreWordWrappingState(ref TMP_Text.m_SavedLastValidState);
							characterSubstitution.index = characterCount;
							characterSubstitution.unicode = 3U;
							goto IL_3BD5;
						case TextOverflowModes.Page:
							if (num14 < 0 || characterCount == 0)
							{
								num14 = -1;
								this.m_characterCount = 0;
								characterSubstitution.index = 0;
								characterSubstitution.unicode = 3U;
								goto IL_3BD5;
							}
							if (this.m_maxLineAscender - this.m_maxLineDescender > num9 + 0.0001f)
							{
								num14 = base.RestoreWordWrappingState(ref TMP_Text.m_SavedLineState);
								characterSubstitution.index = characterCount;
								characterSubstitution.unicode = 3U;
								goto IL_3BD5;
							}
							num14 = base.RestoreWordWrappingState(ref TMP_Text.m_SavedLineState);
							this.m_isNewPage = true;
							this.m_firstCharacterOfLine = this.m_characterCount;
							this.m_maxLineAscender = TMP_Text.k_LargeNegativeFloat;
							this.m_maxLineDescender = TMP_Text.k_LargePositiveFloat;
							this.m_startOfLineAscender = 0f;
							this.m_xAdvance = 0f + this.tag_Indent;
							this.m_lineOffset = 0f;
							this.m_maxTextAscender = 0f;
							this.m_PageAscender = 0f;
							this.m_lineNumber++;
							this.m_pageNumber++;
							goto IL_3BD5;
						case TextOverflowModes.Linked:
							num14 = base.RestoreWordWrappingState(ref TMP_Text.m_SavedLastValidState);
							if (this.m_linkedTextComponent != null)
							{
								this.m_linkedTextComponent.text = this.text;
								this.m_linkedTextComponent.m_inputSource = this.m_inputSource;
								this.m_linkedTextComponent.firstVisibleCharacter = this.m_characterCount;
								this.m_linkedTextComponent.ForceMeshUpdate(false, false);
								this.m_isTextTruncated = true;
							}
							characterSubstitution.index = characterCount;
							characterSubstitution.unicode = 3U;
							goto IL_3BD5;
						}
					}
					if (num35 > num10 * (flag12 ? 1.05f : 1f))
					{
						if (this.m_enableWordWrapping && this.m_characterCount != this.m_firstCharacterOfLine)
						{
							num14 = base.RestoreWordWrappingState(ref TMP_Text.m_SavedWordWrapState);
							float num39;
							if (this.m_lineHeight == -32767f)
							{
								float adjustedAscender = this.m_textInfo.characterInfo[this.m_characterCount].adjustedAscender;
								num39 = ((this.m_lineOffset > 0f && !this.m_IsDrivenLineSpacing) ? (this.m_maxLineAscender - this.m_startOfLineAscender) : 0f) - this.m_maxLineDescender + adjustedAscender + (num6 + this.m_lineSpacingDelta) * num + this.m_lineSpacing * num3;
							}
							else
							{
								num39 = this.m_lineHeight + this.m_lineSpacing * num3;
								this.m_IsDrivenLineSpacing = true;
							}
							float num40 = this.m_maxTextAscender + num39 + this.m_lineOffset - this.m_textInfo.characterInfo[this.m_characterCount].adjustedDescender;
							if (this.m_textInfo.characterInfo[this.m_characterCount - 1].character == '­' && !flag8 && (this.m_overflowMode == TextOverflowModes.Overflow || num40 < num9 + 0.0001f))
							{
								characterSubstitution.index = this.m_characterCount - 1;
								characterSubstitution.unicode = 45U;
								num14--;
								this.m_characterCount--;
								goto IL_3BD5;
							}
							flag8 = false;
							if (this.m_textInfo.characterInfo[this.m_characterCount].character == '­')
							{
								flag8 = true;
								goto IL_3BD5;
							}
							if (this.m_enableAutoSizing && flag6)
							{
								if (this.m_charWidthAdjDelta < this.m_charWidthMaxAdj / 100f && this.m_AutoSizeIterationCount < this.m_AutoSizeMaxIterationCount)
								{
									float num41 = num35;
									if (this.m_charWidthAdjDelta > 0f)
									{
										num41 /= 1f - this.m_charWidthAdjDelta;
									}
									float num42 = num35 - (num10 - 0.0001f) * (flag12 ? 1.05f : 1f);
									this.m_charWidthAdjDelta += num42 / num41;
									this.m_charWidthAdjDelta = Mathf.Min(this.m_charWidthAdjDelta, this.m_charWidthMaxAdj / 100f);
									return;
								}
								if (this.m_fontSize > this.m_fontSizeMin && this.m_AutoSizeIterationCount < this.m_AutoSizeMaxIterationCount)
								{
									this.m_maxFontSize = this.m_fontSize;
									float num43 = Mathf.Max((this.m_fontSize - this.m_minFontSize) / 2f, 0.05f);
									this.m_fontSize -= num43;
									this.m_fontSize = Mathf.Max((float)((int)(this.m_fontSize * 20f + 0.5f)) / 20f, this.m_fontSizeMin);
									return;
								}
							}
							int previous_WordBreak = TMP_Text.m_SavedSoftLineBreakState.previous_WordBreak;
							if (flag6 && previous_WordBreak != -1 && previous_WordBreak != num12)
							{
								num14 = base.RestoreWordWrappingState(ref TMP_Text.m_SavedSoftLineBreakState);
								num12 = previous_WordBreak;
								if (this.m_textInfo.characterInfo[this.m_characterCount - 1].character == '­')
								{
									characterSubstitution.index = this.m_characterCount - 1;
									characterSubstitution.unicode = 45U;
									num14--;
									this.m_characterCount--;
									goto IL_3BD5;
								}
							}
							if (num40 <= num9 + 0.0001f)
							{
								base.InsertNewLine(num14, num, num2, num3, this.m_GlyphHorizontalAdvanceAdjustment, num29, num26, num10, num6, ref flag5, ref num11);
								flag4 = true;
								flag6 = true;
								goto IL_3BD5;
							}
							if (this.m_firstOverflowCharacterIndex == -1)
							{
								this.m_firstOverflowCharacterIndex = this.m_characterCount;
							}
							if (this.m_enableAutoSizing)
							{
								if (this.m_lineSpacingDelta > this.m_lineSpacingMax && this.m_AutoSizeIterationCount < this.m_AutoSizeMaxIterationCount)
								{
									float num44 = (num9 - num40) / (float)(this.m_lineNumber + 1);
									this.m_lineSpacingDelta = Mathf.Max(this.m_lineSpacingDelta + num44 / num, this.m_lineSpacingMax);
									return;
								}
								if (this.m_charWidthAdjDelta < this.m_charWidthMaxAdj / 100f && this.m_AutoSizeIterationCount < this.m_AutoSizeMaxIterationCount)
								{
									float num45 = num35;
									if (this.m_charWidthAdjDelta > 0f)
									{
										num45 /= 1f - this.m_charWidthAdjDelta;
									}
									float num46 = num35 - (num10 - 0.0001f) * (flag12 ? 1.05f : 1f);
									this.m_charWidthAdjDelta += num46 / num45;
									this.m_charWidthAdjDelta = Mathf.Min(this.m_charWidthAdjDelta, this.m_charWidthMaxAdj / 100f);
									return;
								}
								if (this.m_fontSize > this.m_fontSizeMin && this.m_AutoSizeIterationCount < this.m_AutoSizeMaxIterationCount)
								{
									this.m_maxFontSize = this.m_fontSize;
									float num47 = Mathf.Max((this.m_fontSize - this.m_minFontSize) / 2f, 0.05f);
									this.m_fontSize -= num47;
									this.m_fontSize = Mathf.Max((float)((int)(this.m_fontSize * 20f + 0.5f)) / 20f, this.m_fontSizeMin);
									return;
								}
							}
							switch (this.m_overflowMode)
							{
							case TextOverflowModes.Overflow:
							case TextOverflowModes.Masking:
							case TextOverflowModes.ScrollRect:
								base.InsertNewLine(num14, num, num2, num3, this.m_GlyphHorizontalAdvanceAdjustment, num29, num26, num10, num6, ref flag5, ref num11);
								flag4 = true;
								flag6 = true;
								goto IL_3BD5;
							case TextOverflowModes.Ellipsis:
							{
								if (TMP_Text.m_EllipsisInsertionCandidateStack.Count == 0)
								{
									num14 = -1;
									this.m_characterCount = 0;
									characterSubstitution.index = 0;
									characterSubstitution.unicode = 3U;
									this.m_firstCharacterOfLine = 0;
									goto IL_3BD5;
								}
								WordWrapState wordWrapState2 = TMP_Text.m_EllipsisInsertionCandidateStack.Pop();
								num14 = base.RestoreWordWrappingState(ref wordWrapState2);
								num14--;
								this.m_characterCount--;
								characterSubstitution.index = this.m_characterCount;
								characterSubstitution.unicode = 8230U;
								num13++;
								goto IL_3BD5;
							}
							case TextOverflowModes.Truncate:
								num14 = base.RestoreWordWrappingState(ref TMP_Text.m_SavedLastValidState);
								characterSubstitution.index = characterCount;
								characterSubstitution.unicode = 3U;
								goto IL_3BD5;
							case TextOverflowModes.Page:
								this.m_isNewPage = true;
								base.InsertNewLine(num14, num, num2, num3, this.m_GlyphHorizontalAdvanceAdjustment, num29, num26, num10, num6, ref flag5, ref num11);
								this.m_startOfLineAscender = 0f;
								this.m_lineOffset = 0f;
								this.m_maxTextAscender = 0f;
								this.m_PageAscender = 0f;
								this.m_pageNumber++;
								flag4 = true;
								flag6 = true;
								goto IL_3BD5;
							case TextOverflowModes.Linked:
								if (this.m_linkedTextComponent != null)
								{
									this.m_linkedTextComponent.text = this.text;
									this.m_linkedTextComponent.m_inputSource = this.m_inputSource;
									this.m_linkedTextComponent.firstVisibleCharacter = this.m_characterCount;
									this.m_linkedTextComponent.ForceMeshUpdate(false, false);
									this.m_isTextTruncated = true;
								}
								characterSubstitution.index = this.m_characterCount;
								characterSubstitution.unicode = 3U;
								goto IL_3BD5;
							}
						}
						else
						{
							if (this.m_enableAutoSizing && this.m_AutoSizeIterationCount < this.m_AutoSizeMaxIterationCount)
							{
								if (this.m_charWidthAdjDelta < this.m_charWidthMaxAdj / 100f)
								{
									float num48 = num35;
									if (this.m_charWidthAdjDelta > 0f)
									{
										num48 /= 1f - this.m_charWidthAdjDelta;
									}
									float num49 = num35 - (num10 - 0.0001f) * (flag12 ? 1.05f : 1f);
									this.m_charWidthAdjDelta += num49 / num48;
									this.m_charWidthAdjDelta = Mathf.Min(this.m_charWidthAdjDelta, this.m_charWidthMaxAdj / 100f);
									return;
								}
								if (this.m_fontSize > this.m_fontSizeMin)
								{
									this.m_maxFontSize = this.m_fontSize;
									float num50 = Mathf.Max((this.m_fontSize - this.m_minFontSize) / 2f, 0.05f);
									this.m_fontSize -= num50;
									this.m_fontSize = Mathf.Max((float)((int)(this.m_fontSize * 20f + 0.5f)) / 20f, this.m_fontSizeMin);
									return;
								}
							}
							switch (this.m_overflowMode)
							{
							case TextOverflowModes.Ellipsis:
							{
								if (TMP_Text.m_EllipsisInsertionCandidateStack.Count == 0)
								{
									num14 = -1;
									this.m_characterCount = 0;
									characterSubstitution.index = 0;
									characterSubstitution.unicode = 3U;
									this.m_firstCharacterOfLine = 0;
									goto IL_3BD5;
								}
								WordWrapState wordWrapState3 = TMP_Text.m_EllipsisInsertionCandidateStack.Pop();
								num14 = base.RestoreWordWrappingState(ref wordWrapState3);
								num14--;
								this.m_characterCount--;
								characterSubstitution.index = this.m_characterCount;
								characterSubstitution.unicode = 8230U;
								num13++;
								goto IL_3BD5;
							}
							case TextOverflowModes.Truncate:
								num14 = base.RestoreWordWrappingState(ref TMP_Text.m_SavedWordWrapState);
								characterSubstitution.index = characterCount;
								characterSubstitution.unicode = 3U;
								goto IL_3BD5;
							case TextOverflowModes.Linked:
								num14 = base.RestoreWordWrappingState(ref TMP_Text.m_SavedWordWrapState);
								if (this.m_linkedTextComponent != null)
								{
									this.m_linkedTextComponent.text = this.text;
									this.m_linkedTextComponent.m_inputSource = this.m_inputSource;
									this.m_linkedTextComponent.firstVisibleCharacter = this.m_characterCount;
									this.m_linkedTextComponent.ForceMeshUpdate(false, false);
									this.m_isTextTruncated = true;
								}
								characterSubstitution.index = this.m_characterCount;
								characterSubstitution.unicode = 3U;
								goto IL_3BD5;
							}
						}
					}
					if (num4 == 9)
					{
						this.m_textInfo.characterInfo[this.m_characterCount].isVisible = false;
						this.m_lastVisibleCharacterOfLine = this.m_characterCount;
						TMP_LineInfo[] lineInfo = this.m_textInfo.lineInfo;
						int lineNumber = this.m_lineNumber;
						lineInfo[lineNumber].spaceCount = lineInfo[lineNumber].spaceCount + 1;
						this.m_textInfo.spaceCount++;
					}
					else if (num4 == 173)
					{
						this.m_textInfo.characterInfo[this.m_characterCount].isVisible = false;
					}
					else
					{
						Color32 vertexColor;
						if (this.m_overrideHtmlColors)
						{
							vertexColor = this.m_fontColor32;
						}
						else
						{
							vertexColor = this.m_htmlColor;
						}
						if (this.m_textElementType == TMP_TextElementType.Character)
						{
							this.SaveGlyphVertexInfo(num5, num28, vertexColor);
						}
						else if (this.m_textElementType == TMP_TextElementType.Sprite)
						{
							this.SaveSpriteVertexInfo(vertexColor);
						}
						if (flag4)
						{
							flag4 = false;
							this.m_firstVisibleCharacterOfLine = this.m_characterCount;
						}
						this.m_lineVisibleCharacterCount++;
						this.m_lastVisibleCharacterOfLine = this.m_characterCount;
						this.m_textInfo.lineInfo[this.m_lineNumber].marginLeft = marginLeft;
						this.m_textInfo.lineInfo[this.m_lineNumber].marginRight = marginRight;
					}
				}
				else
				{
					if (this.m_overflowMode == TextOverflowModes.Linked && (num4 == 10 || num4 == 11))
					{
						float num51 = this.m_maxTextAscender - (this.m_maxLineDescender - this.m_lineOffset) + ((this.m_lineOffset > 0f && !this.m_IsDrivenLineSpacing) ? (this.m_maxLineAscender - this.m_startOfLineAscender) : 0f);
						int characterCount2 = this.m_characterCount;
						if (num51 > num9 + 0.0001f)
						{
							if (this.m_firstOverflowCharacterIndex == -1)
							{
								this.m_firstOverflowCharacterIndex = this.m_characterCount;
							}
							num14 = base.RestoreWordWrappingState(ref TMP_Text.m_SavedLastValidState);
							if (this.m_linkedTextComponent != null)
							{
								this.m_linkedTextComponent.text = this.text;
								this.m_linkedTextComponent.m_inputSource = this.m_inputSource;
								this.m_linkedTextComponent.firstVisibleCharacter = this.m_characterCount;
								this.m_linkedTextComponent.ForceMeshUpdate(false, false);
								this.m_isTextTruncated = true;
							}
							characterSubstitution.index = characterCount2;
							characterSubstitution.unicode = 3U;
							goto IL_3BD5;
						}
					}
					if ((num4 == 10 || num4 == 11 || num4 == 160 || num4 == 8199 || num4 == 8232 || num4 == 8233 || char.IsSeparator((char)num4)) && num4 != 173 && num4 != 8203 && num4 != 8288)
					{
						TMP_LineInfo[] lineInfo2 = this.m_textInfo.lineInfo;
						int lineNumber2 = this.m_lineNumber;
						lineInfo2[lineNumber2].spaceCount = lineInfo2[lineNumber2].spaceCount + 1;
						this.m_textInfo.spaceCount++;
					}
					if (num4 == 160)
					{
						TMP_LineInfo[] lineInfo3 = this.m_textInfo.lineInfo;
						int lineNumber3 = this.m_lineNumber;
						lineInfo3[lineNumber3].controlCharacterCount = lineInfo3[lineNumber3].controlCharacterCount + 1;
					}
				}
				if (this.m_overflowMode == TextOverflowModes.Ellipsis && (!flag9 || num4 == 45))
				{
					float num52 = this.m_currentFontSize / (float)this.m_Ellipsis.fontAsset.m_FaceInfo.pointSize * this.m_Ellipsis.fontAsset.m_FaceInfo.scale * (this.m_isOrthographic ? 1f : 0.1f) * this.m_fontScaleMultiplier * this.m_Ellipsis.character.m_Scale * this.m_Ellipsis.character.m_Glyph.scale;
					float marginLeft2 = this.m_marginLeft;
					float marginRight2 = this.m_marginRight;
					if (num4 == 10 && this.m_characterCount != this.m_firstCharacterOfLine)
					{
						num52 = this.m_textInfo.characterInfo[this.m_characterCount - 1].pointSize / (float)this.m_Ellipsis.fontAsset.m_FaceInfo.pointSize * this.m_Ellipsis.fontAsset.m_FaceInfo.scale * (this.m_isOrthographic ? 1f : 0.1f) * this.m_fontScaleMultiplier * this.m_Ellipsis.character.m_Scale * this.m_Ellipsis.character.m_Glyph.scale;
						marginLeft2 = this.m_textInfo.lineInfo[this.m_lineNumber].marginLeft;
						marginRight2 = this.m_textInfo.lineInfo[this.m_lineNumber].marginRight;
					}
					float num53 = this.m_maxTextAscender - (this.m_maxLineDescender - this.m_lineOffset) + ((this.m_lineOffset > 0f && !this.m_IsDrivenLineSpacing) ? (this.m_maxLineAscender - this.m_startOfLineAscender) : 0f);
					float num54 = Mathf.Abs(this.m_xAdvance) + ((!this.m_isRightToLeft) ? this.m_Ellipsis.character.m_Glyph.metrics.horizontalAdvance : 0f) * (1f - this.m_charWidthAdjDelta) * num52;
					float num55 = (this.m_width != -1f) ? Mathf.Min(num8 + 0.0001f - marginLeft2 - marginRight2, this.m_width) : (num8 + 0.0001f - marginLeft2 - marginRight2);
					if (num54 < num55 * (flag12 ? 1.05f : 1f) && num53 < num9 + 0.0001f)
					{
						base.SaveWordWrappingState(ref TMP_Text.m_SavedEllipsisState, num14, this.m_characterCount);
						TMP_Text.m_EllipsisInsertionCandidateStack.Push(TMP_Text.m_SavedEllipsisState);
					}
				}
				this.m_textInfo.characterInfo[this.m_characterCount].lineNumber = this.m_lineNumber;
				this.m_textInfo.characterInfo[this.m_characterCount].pageNumber = this.m_pageNumber;
				if ((num4 != 10 && num4 != 11 && num4 != 13 && !flag9) || this.m_textInfo.lineInfo[this.m_lineNumber].characterCount == 1)
				{
					this.m_textInfo.lineInfo[this.m_lineNumber].alignment = this.m_lineJustification;
				}
				if (num4 == 9)
				{
					float num56 = this.m_currentFontAsset.m_FaceInfo.tabWidth * (float)this.m_currentFontAsset.tabSize * num2;
					float num57 = Mathf.Ceil(this.m_xAdvance / num56) * num56;
					this.m_xAdvance = ((num57 > this.m_xAdvance) ? num57 : (this.m_xAdvance + num56));
				}
				else if (this.m_monoSpacing != 0f)
				{
					this.m_xAdvance += (this.m_monoSpacing - num27 + (this.m_currentFontAsset.normalSpacingOffset + num26) * num3 + this.m_cSpacing) * (1f - this.m_charWidthAdjDelta);
					if (flag10 || num4 == 8203)
					{
						this.m_xAdvance += this.m_wordSpacing * num3;
					}
				}
				else if (this.m_isRightToLeft)
				{
					this.m_xAdvance -= (tmp_GlyphValueRecord.m_XAdvance * num2 + (this.m_currentFontAsset.normalSpacingOffset + num26 + num29) * num3 + this.m_cSpacing) * (1f - this.m_charWidthAdjDelta);
					if (flag10 || num4 == 8203)
					{
						this.m_xAdvance -= this.m_wordSpacing * num3;
					}
				}
				else
				{
					float num58 = 1f;
					if (this.m_isFXMatrixSet)
					{
						num58 = this.m_FXMatrix.lossyScale.x;
					}
					this.m_xAdvance += ((metrics.horizontalAdvance * num58 + tmp_GlyphValueRecord.m_XAdvance) * num2 + (this.m_currentFontAsset.normalSpacingOffset + num26 + num29) * num3 + this.m_cSpacing) * (1f - this.m_charWidthAdjDelta);
					if (flag10 || num4 == 8203)
					{
						this.m_xAdvance += this.m_wordSpacing * num3;
					}
				}
				this.m_textInfo.characterInfo[this.m_characterCount].xAdvance = this.m_xAdvance;
				if (num4 == 13)
				{
					this.m_xAdvance = 0f + this.tag_Indent;
				}
				if (num4 == 10 || num4 == 11 || num4 == 3 || num4 == 8232 || num4 == 8233 || (num4 == 45 && flag9) || this.m_characterCount == totalCharacterCount - 1)
				{
					float num59 = this.m_maxLineAscender - this.m_startOfLineAscender;
					if (this.m_lineOffset > 0f && Math.Abs(num59) > 0.01f && !this.m_IsDrivenLineSpacing && !this.m_isNewPage)
					{
						base.AdjustLineOffset(this.m_firstCharacterOfLine, this.m_characterCount, num59);
						this.m_ElementDescender -= num59;
						this.m_lineOffset += num59;
						if (TMP_Text.m_SavedEllipsisState.lineNumber == this.m_lineNumber)
						{
							TMP_Text.m_SavedEllipsisState = TMP_Text.m_EllipsisInsertionCandidateStack.Pop();
							TMP_Text.m_SavedEllipsisState.startOfLineAscender = TMP_Text.m_SavedEllipsisState.startOfLineAscender + num59;
							TMP_Text.m_SavedEllipsisState.lineOffset = TMP_Text.m_SavedEllipsisState.lineOffset + num59;
							TMP_Text.m_EllipsisInsertionCandidateStack.Push(TMP_Text.m_SavedEllipsisState);
						}
					}
					this.m_isNewPage = false;
					float num60 = this.m_maxLineAscender - this.m_lineOffset;
					float num61 = this.m_maxLineDescender - this.m_lineOffset;
					this.m_ElementDescender = ((this.m_ElementDescender < num61) ? this.m_ElementDescender : num61);
					if (!flag5)
					{
						num11 = this.m_ElementDescender;
					}
					if (this.m_useMaxVisibleDescender && (this.m_characterCount >= this.m_maxVisibleCharacters || this.m_lineNumber >= this.m_maxVisibleLines))
					{
						flag5 = true;
					}
					this.m_textInfo.lineInfo[this.m_lineNumber].firstCharacterIndex = this.m_firstCharacterOfLine;
					this.m_textInfo.lineInfo[this.m_lineNumber].firstVisibleCharacterIndex = (this.m_firstVisibleCharacterOfLine = ((this.m_firstCharacterOfLine > this.m_firstVisibleCharacterOfLine) ? this.m_firstCharacterOfLine : this.m_firstVisibleCharacterOfLine));
					this.m_textInfo.lineInfo[this.m_lineNumber].lastCharacterIndex = (this.m_lastCharacterOfLine = this.m_characterCount);
					this.m_textInfo.lineInfo[this.m_lineNumber].lastVisibleCharacterIndex = (this.m_lastVisibleCharacterOfLine = ((this.m_lastVisibleCharacterOfLine < this.m_firstVisibleCharacterOfLine) ? this.m_firstVisibleCharacterOfLine : this.m_lastVisibleCharacterOfLine));
					this.m_textInfo.lineInfo[this.m_lineNumber].characterCount = this.m_textInfo.lineInfo[this.m_lineNumber].lastCharacterIndex - this.m_textInfo.lineInfo[this.m_lineNumber].firstCharacterIndex + 1;
					this.m_textInfo.lineInfo[this.m_lineNumber].visibleCharacterCount = this.m_lineVisibleCharacterCount;
					this.m_textInfo.lineInfo[this.m_lineNumber].lineExtents.min = new Vector2(this.m_textInfo.characterInfo[this.m_firstVisibleCharacterOfLine].bottomLeft.x, num61);
					this.m_textInfo.lineInfo[this.m_lineNumber].lineExtents.max = new Vector2(this.m_textInfo.characterInfo[this.m_lastVisibleCharacterOfLine].topRight.x, num60);
					this.m_textInfo.lineInfo[this.m_lineNumber].length = this.m_textInfo.lineInfo[this.m_lineNumber].lineExtents.max.x - num5 * num2;
					this.m_textInfo.lineInfo[this.m_lineNumber].width = num10;
					if (this.m_textInfo.lineInfo[this.m_lineNumber].characterCount == 1)
					{
						this.m_textInfo.lineInfo[this.m_lineNumber].alignment = this.m_lineJustification;
					}
					float num62 = ((this.m_currentFontAsset.normalSpacingOffset + num26 + num29) * num3 - this.m_cSpacing) * (1f - this.m_charWidthAdjDelta);
					if (this.m_textInfo.characterInfo[this.m_lastVisibleCharacterOfLine].isVisible)
					{
						this.m_textInfo.lineInfo[this.m_lineNumber].maxAdvance = this.m_textInfo.characterInfo[this.m_lastVisibleCharacterOfLine].xAdvance + (this.m_isRightToLeft ? num62 : (-num62));
					}
					else
					{
						this.m_textInfo.lineInfo[this.m_lineNumber].maxAdvance = this.m_textInfo.characterInfo[this.m_lastCharacterOfLine].xAdvance + (this.m_isRightToLeft ? num62 : (-num62));
					}
					this.m_textInfo.lineInfo[this.m_lineNumber].baseline = 0f - this.m_lineOffset;
					this.m_textInfo.lineInfo[this.m_lineNumber].ascender = num60;
					this.m_textInfo.lineInfo[this.m_lineNumber].descender = num61;
					this.m_textInfo.lineInfo[this.m_lineNumber].lineHeight = num60 - num61 + num6 * num;
					if (num4 == 10 || num4 == 11 || num4 == 45 || num4 == 8232 || num4 == 8233)
					{
						base.SaveWordWrappingState(ref TMP_Text.m_SavedLineState, num14, this.m_characterCount);
						this.m_lineNumber++;
						flag4 = true;
						flag7 = false;
						flag6 = true;
						this.m_firstCharacterOfLine = this.m_characterCount + 1;
						this.m_lineVisibleCharacterCount = 0;
						if (this.m_lineNumber >= this.m_textInfo.lineInfo.Length)
						{
							base.ResizeLineExtents(this.m_lineNumber);
						}
						float adjustedAscender2 = this.m_textInfo.characterInfo[this.m_characterCount].adjustedAscender;
						if (this.m_lineHeight == -32767f)
						{
							float num63 = 0f - this.m_maxLineDescender + adjustedAscender2 + (num6 + this.m_lineSpacingDelta) * num + (this.m_lineSpacing + ((num4 == 10 || num4 == 8233) ? this.m_paragraphSpacing : 0f)) * num3;
							this.m_lineOffset += num63;
							this.m_IsDrivenLineSpacing = false;
						}
						else
						{
							this.m_lineOffset += this.m_lineHeight + (this.m_lineSpacing + ((num4 == 10 || num4 == 8233) ? this.m_paragraphSpacing : 0f)) * num3;
							this.m_IsDrivenLineSpacing = true;
						}
						this.m_maxLineAscender = TMP_Text.k_LargeNegativeFloat;
						this.m_maxLineDescender = TMP_Text.k_LargePositiveFloat;
						this.m_startOfLineAscender = adjustedAscender2;
						this.m_xAdvance = 0f + this.tag_LineIndent + this.tag_Indent;
						base.SaveWordWrappingState(ref TMP_Text.m_SavedWordWrapState, num14, this.m_characterCount);
						base.SaveWordWrappingState(ref TMP_Text.m_SavedLastValidState, num14, this.m_characterCount);
						this.m_characterCount++;
						goto IL_3BD5;
					}
					if (num4 == 3)
					{
						num14 = this.m_TextProcessingArray.Length;
					}
				}
				if (this.m_textInfo.characterInfo[this.m_characterCount].isVisible)
				{
					this.m_meshExtents.min.x = Mathf.Min(this.m_meshExtents.min.x, this.m_textInfo.characterInfo[this.m_characterCount].bottomLeft.x);
					this.m_meshExtents.min.y = Mathf.Min(this.m_meshExtents.min.y, this.m_textInfo.characterInfo[this.m_characterCount].bottomLeft.y);
					this.m_meshExtents.max.x = Mathf.Max(this.m_meshExtents.max.x, this.m_textInfo.characterInfo[this.m_characterCount].topRight.x);
					this.m_meshExtents.max.y = Mathf.Max(this.m_meshExtents.max.y, this.m_textInfo.characterInfo[this.m_characterCount].topRight.y);
				}
				if (this.m_overflowMode == TextOverflowModes.Page && num4 != 10 && num4 != 11 && num4 != 13 && num4 != 8232 && num4 != 8233)
				{
					if (this.m_pageNumber + 1 > this.m_textInfo.pageInfo.Length)
					{
						TMP_TextInfo.Resize<TMP_PageInfo>(ref this.m_textInfo.pageInfo, this.m_pageNumber + 1, true);
					}
					this.m_textInfo.pageInfo[this.m_pageNumber].ascender = this.m_PageAscender;
					this.m_textInfo.pageInfo[this.m_pageNumber].descender = ((this.m_ElementDescender < this.m_textInfo.pageInfo[this.m_pageNumber].descender) ? this.m_ElementDescender : this.m_textInfo.pageInfo[this.m_pageNumber].descender);
					if (this.m_pageNumber == 0 && this.m_characterCount == 0)
					{
						this.m_textInfo.pageInfo[this.m_pageNumber].firstCharacterIndex = this.m_characterCount;
					}
					else if (this.m_characterCount > 0 && this.m_pageNumber != this.m_textInfo.characterInfo[this.m_characterCount - 1].pageNumber)
					{
						this.m_textInfo.pageInfo[this.m_pageNumber - 1].lastCharacterIndex = this.m_characterCount - 1;
						this.m_textInfo.pageInfo[this.m_pageNumber].firstCharacterIndex = this.m_characterCount;
					}
					else if (this.m_characterCount == totalCharacterCount - 1)
					{
						this.m_textInfo.pageInfo[this.m_pageNumber].lastCharacterIndex = this.m_characterCount;
					}
				}
				if (this.m_enableWordWrapping || this.m_overflowMode == TextOverflowModes.Truncate || this.m_overflowMode == TextOverflowModes.Ellipsis || this.m_overflowMode == TextOverflowModes.Linked)
				{
					if ((flag10 || num4 == 8203 || num4 == 45 || num4 == 173) && (!this.m_isNonBreakingSpace || flag7) && num4 != 160 && num4 != 8199 && num4 != 8209 && num4 != 8239 && num4 != 8288)
					{
						base.SaveWordWrappingState(ref TMP_Text.m_SavedWordWrapState, num14, this.m_characterCount);
						flag6 = false;
						TMP_Text.m_SavedSoftLineBreakState.previous_WordBreak = -1;
					}
					else if (!this.m_isNonBreakingSpace && ((((num4 > 4352 && num4 < 4607) || (num4 > 43360 && num4 < 43391) || (num4 > 44032 && num4 < 55295)) && !TMP_Settings.useModernHangulLineBreakingRules) || (num4 > 11904 && num4 < 40959) || (num4 > 63744 && num4 < 64255) || (num4 > 65072 && num4 < 65103) || (num4 > 65280 && num4 < 65519)))
					{
						bool flag13 = TMP_Settings.linebreakingRules.leadingCharacters.ContainsKey(num4);
						bool flag14 = this.m_characterCount < totalCharacterCount - 1 && TMP_Settings.linebreakingRules.followingCharacters.ContainsKey((int)this.m_textInfo.characterInfo[this.m_characterCount + 1].character);
						if (!flag13)
						{
							if (!flag14)
							{
								base.SaveWordWrappingState(ref TMP_Text.m_SavedWordWrapState, num14, this.m_characterCount);
								flag6 = false;
							}
							if (flag6)
							{
								if (flag10)
								{
									base.SaveWordWrappingState(ref TMP_Text.m_SavedSoftLineBreakState, num14, this.m_characterCount);
								}
								base.SaveWordWrappingState(ref TMP_Text.m_SavedWordWrapState, num14, this.m_characterCount);
							}
						}
						else if (flag6 && flag11)
						{
							if (flag10)
							{
								base.SaveWordWrappingState(ref TMP_Text.m_SavedSoftLineBreakState, num14, this.m_characterCount);
							}
							base.SaveWordWrappingState(ref TMP_Text.m_SavedWordWrapState, num14, this.m_characterCount);
						}
					}
					else if (flag6)
					{
						if (flag10 || (num4 == 173 && !flag8))
						{
							base.SaveWordWrappingState(ref TMP_Text.m_SavedSoftLineBreakState, num14, this.m_characterCount);
						}
						base.SaveWordWrappingState(ref TMP_Text.m_SavedWordWrapState, num14, this.m_characterCount);
					}
				}
				base.SaveWordWrappingState(ref TMP_Text.m_SavedLastValidState, num14, this.m_characterCount);
				this.m_characterCount++;
				goto IL_3BD5;
			}
			float num64 = this.m_maxFontSize - this.m_minFontSize;
			if (this.m_enableAutoSizing && num64 > 0.051f && this.m_fontSize < this.m_fontSizeMax && this.m_AutoSizeIterationCount < this.m_AutoSizeMaxIterationCount)
			{
				if (this.m_charWidthAdjDelta < this.m_charWidthMaxAdj / 100f)
				{
					this.m_charWidthAdjDelta = 0f;
				}
				this.m_minFontSize = this.m_fontSize;
				float num65 = Mathf.Max((this.m_maxFontSize - this.m_fontSize) / 2f, 0.05f);
				this.m_fontSize += num65;
				this.m_fontSize = Mathf.Min((float)((int)(this.m_fontSize * 20f + 0.5f)) / 20f, this.m_fontSizeMax);
				return;
			}
			this.m_IsAutoSizePointSizeSet = true;
			if (this.m_AutoSizeIterationCount >= this.m_AutoSizeMaxIterationCount)
			{
				Debug.Log("Auto Size Iteration Count: " + this.m_AutoSizeIterationCount.ToString() + ". Final Point Size: " + this.m_fontSize.ToString());
			}
			if (this.m_characterCount == 0 || (this.m_characterCount == 1 && num4 == 3))
			{
				this.ClearMesh(true);
				TMPro_EventManager.ON_TEXT_CHANGED(this);
				return;
			}
			int num66 = TMP_Text.m_materialReferences[this.m_Underline.materialIndex].referenceCount * 4;
			this.m_textInfo.meshInfo[0].Clear(false);
			Vector3 a = Vector3.zero;
			Vector3[] rectTransformCorners = this.m_RectTransformCorners;
			VerticalAlignmentOptions verticalAlignment = this.m_VerticalAlignment;
			if (verticalAlignment <= VerticalAlignmentOptions.Bottom)
			{
				if (verticalAlignment != VerticalAlignmentOptions.Top)
				{
					if (verticalAlignment != VerticalAlignmentOptions.Middle)
					{
						if (verticalAlignment == VerticalAlignmentOptions.Bottom)
						{
							if (this.m_overflowMode != TextOverflowModes.Page)
							{
								a = rectTransformCorners[0] + new Vector3(0f + margin.x, 0f - num11 + margin.w, 0f);
							}
							else
							{
								a = rectTransformCorners[0] + new Vector3(0f + margin.x, 0f - this.m_textInfo.pageInfo[num7].descender + margin.w, 0f);
							}
						}
					}
					else if (this.m_overflowMode != TextOverflowModes.Page)
					{
						a = (rectTransformCorners[0] + rectTransformCorners[1]) / 2f + new Vector3(0f + margin.x, 0f - (this.m_maxTextAscender + margin.y + num11 - margin.w) / 2f, 0f);
					}
					else
					{
						a = (rectTransformCorners[0] + rectTransformCorners[1]) / 2f + new Vector3(0f + margin.x, 0f - (this.m_textInfo.pageInfo[num7].ascender + margin.y + this.m_textInfo.pageInfo[num7].descender - margin.w) / 2f, 0f);
					}
				}
				else if (this.m_overflowMode != TextOverflowModes.Page)
				{
					a = rectTransformCorners[1] + new Vector3(0f + margin.x, 0f - this.m_maxTextAscender - margin.y, 0f);
				}
				else
				{
					a = rectTransformCorners[1] + new Vector3(0f + margin.x, 0f - this.m_textInfo.pageInfo[num7].ascender - margin.y, 0f);
				}
			}
			else if (verticalAlignment != VerticalAlignmentOptions.Baseline)
			{
				if (verticalAlignment != VerticalAlignmentOptions.Geometry)
				{
					if (verticalAlignment == VerticalAlignmentOptions.Capline)
					{
						a = (rectTransformCorners[0] + rectTransformCorners[1]) / 2f + new Vector3(0f + margin.x, 0f - (this.m_maxCapHeight - margin.y - margin.w) / 2f, 0f);
					}
				}
				else
				{
					a = (rectTransformCorners[0] + rectTransformCorners[1]) / 2f + new Vector3(0f + margin.x, 0f - (this.m_meshExtents.max.y + margin.y + this.m_meshExtents.min.y - margin.w) / 2f, 0f);
				}
			}
			else
			{
				a = (rectTransformCorners[0] + rectTransformCorners[1]) / 2f + new Vector3(0f + margin.x, 0f, 0f);
			}
			Vector3 vector9 = Vector3.zero;
			Vector3 vector10 = Vector3.zero;
			int index_X = 0;
			int index_X2 = 0;
			int num67 = 0;
			int lineCount = 0;
			int num68 = 0;
			bool flag15 = false;
			bool flag16 = false;
			int num69 = 0;
			float f = this.m_previousLossyScaleY = this.transform.lossyScale.y;
			Color32 color = Color.white;
			Color32 underlineColor = Color.white;
			HighlightState highlightState = new HighlightState(new Color32(byte.MaxValue, byte.MaxValue, 0, 64), TMP_Offset.zero);
			float num70 = 0f;
			float num71 = 0f;
			float num72 = 0f;
			float num73 = 0f;
			float num74 = TMP_Text.k_LargePositiveFloat;
			int num75 = 0;
			float num76 = 0f;
			float num77 = 0f;
			float b3 = 0f;
			TMP_CharacterInfo[] characterInfo = this.m_textInfo.characterInfo;
			int i = 0;
			while (i < this.m_characterCount)
			{
				TMP_FontAsset fontAsset = characterInfo[i].fontAsset;
				char character = characterInfo[i].character;
				int lineNumber4 = characterInfo[i].lineNumber;
				TMP_LineInfo tmp_LineInfo = this.m_textInfo.lineInfo[lineNumber4];
				lineCount = lineNumber4 + 1;
				HorizontalAlignmentOptions alignment = tmp_LineInfo.alignment;
				if (alignment <= HorizontalAlignmentOptions.Justified)
				{
					switch (alignment)
					{
					case HorizontalAlignmentOptions.Left:
						if (!this.m_isRightToLeft)
						{
							vector9 = new Vector3(0f + tmp_LineInfo.marginLeft, 0f, 0f);
						}
						else
						{
							vector9 = new Vector3(0f - tmp_LineInfo.maxAdvance, 0f, 0f);
						}
						break;
					case HorizontalAlignmentOptions.Center:
						vector9 = new Vector3(tmp_LineInfo.marginLeft + tmp_LineInfo.width / 2f - tmp_LineInfo.maxAdvance / 2f, 0f, 0f);
						break;
					case (HorizontalAlignmentOptions)3:
						break;
					case HorizontalAlignmentOptions.Right:
						if (!this.m_isRightToLeft)
						{
							vector9 = new Vector3(tmp_LineInfo.marginLeft + tmp_LineInfo.width - tmp_LineInfo.maxAdvance, 0f, 0f);
						}
						else
						{
							vector9 = new Vector3(tmp_LineInfo.marginLeft + tmp_LineInfo.width, 0f, 0f);
						}
						break;
					default:
						if (alignment == HorizontalAlignmentOptions.Justified)
						{
							goto IL_43BB;
						}
						break;
					}
				}
				else
				{
					if (alignment == HorizontalAlignmentOptions.Flush)
					{
						goto IL_43BB;
					}
					if (alignment == HorizontalAlignmentOptions.Geometry)
					{
						vector9 = new Vector3(tmp_LineInfo.marginLeft + tmp_LineInfo.width / 2f - (tmp_LineInfo.lineExtents.min.x + tmp_LineInfo.lineExtents.max.x) / 2f, 0f, 0f);
					}
				}
				IL_464B:
				vector10 = a + vector9;
				bool isVisible = characterInfo[i].isVisible;
				if (isVisible)
				{
					TMP_TextElementType elementType = characterInfo[i].elementType;
					if (elementType != TMP_TextElementType.Character)
					{
						if (elementType != TMP_TextElementType.Sprite)
						{
						}
					}
					else
					{
						Extents lineExtents = tmp_LineInfo.lineExtents;
						float num78 = this.m_uvLineOffset * (float)lineNumber4 % 1f;
						switch (this.m_horizontalMapping)
						{
						case TextureMappingOptions.Character:
							characterInfo[i].vertex_BL.uv2.x = 0f;
							characterInfo[i].vertex_TL.uv2.x = 0f;
							characterInfo[i].vertex_TR.uv2.x = 1f;
							characterInfo[i].vertex_BR.uv2.x = 1f;
							break;
						case TextureMappingOptions.Line:
							if (this.m_textAlignment != TextAlignmentOptions.Justified)
							{
								characterInfo[i].vertex_BL.uv2.x = (characterInfo[i].vertex_BL.position.x - lineExtents.min.x) / (lineExtents.max.x - lineExtents.min.x) + num78;
								characterInfo[i].vertex_TL.uv2.x = (characterInfo[i].vertex_TL.position.x - lineExtents.min.x) / (lineExtents.max.x - lineExtents.min.x) + num78;
								characterInfo[i].vertex_TR.uv2.x = (characterInfo[i].vertex_TR.position.x - lineExtents.min.x) / (lineExtents.max.x - lineExtents.min.x) + num78;
								characterInfo[i].vertex_BR.uv2.x = (characterInfo[i].vertex_BR.position.x - lineExtents.min.x) / (lineExtents.max.x - lineExtents.min.x) + num78;
							}
							else
							{
								characterInfo[i].vertex_BL.uv2.x = (characterInfo[i].vertex_BL.position.x + vector9.x - this.m_meshExtents.min.x) / (this.m_meshExtents.max.x - this.m_meshExtents.min.x) + num78;
								characterInfo[i].vertex_TL.uv2.x = (characterInfo[i].vertex_TL.position.x + vector9.x - this.m_meshExtents.min.x) / (this.m_meshExtents.max.x - this.m_meshExtents.min.x) + num78;
								characterInfo[i].vertex_TR.uv2.x = (characterInfo[i].vertex_TR.position.x + vector9.x - this.m_meshExtents.min.x) / (this.m_meshExtents.max.x - this.m_meshExtents.min.x) + num78;
								characterInfo[i].vertex_BR.uv2.x = (characterInfo[i].vertex_BR.position.x + vector9.x - this.m_meshExtents.min.x) / (this.m_meshExtents.max.x - this.m_meshExtents.min.x) + num78;
							}
							break;
						case TextureMappingOptions.Paragraph:
							characterInfo[i].vertex_BL.uv2.x = (characterInfo[i].vertex_BL.position.x + vector9.x - this.m_meshExtents.min.x) / (this.m_meshExtents.max.x - this.m_meshExtents.min.x) + num78;
							characterInfo[i].vertex_TL.uv2.x = (characterInfo[i].vertex_TL.position.x + vector9.x - this.m_meshExtents.min.x) / (this.m_meshExtents.max.x - this.m_meshExtents.min.x) + num78;
							characterInfo[i].vertex_TR.uv2.x = (characterInfo[i].vertex_TR.position.x + vector9.x - this.m_meshExtents.min.x) / (this.m_meshExtents.max.x - this.m_meshExtents.min.x) + num78;
							characterInfo[i].vertex_BR.uv2.x = (characterInfo[i].vertex_BR.position.x + vector9.x - this.m_meshExtents.min.x) / (this.m_meshExtents.max.x - this.m_meshExtents.min.x) + num78;
							break;
						case TextureMappingOptions.MatchAspect:
						{
							switch (this.m_verticalMapping)
							{
							case TextureMappingOptions.Character:
								characterInfo[i].vertex_BL.uv2.y = 0f;
								characterInfo[i].vertex_TL.uv2.y = 1f;
								characterInfo[i].vertex_TR.uv2.y = 0f;
								characterInfo[i].vertex_BR.uv2.y = 1f;
								break;
							case TextureMappingOptions.Line:
								characterInfo[i].vertex_BL.uv2.y = (characterInfo[i].vertex_BL.position.y - lineExtents.min.y) / (lineExtents.max.y - lineExtents.min.y) + num78;
								characterInfo[i].vertex_TL.uv2.y = (characterInfo[i].vertex_TL.position.y - lineExtents.min.y) / (lineExtents.max.y - lineExtents.min.y) + num78;
								characterInfo[i].vertex_TR.uv2.y = characterInfo[i].vertex_BL.uv2.y;
								characterInfo[i].vertex_BR.uv2.y = characterInfo[i].vertex_TL.uv2.y;
								break;
							case TextureMappingOptions.Paragraph:
								characterInfo[i].vertex_BL.uv2.y = (characterInfo[i].vertex_BL.position.y - this.m_meshExtents.min.y) / (this.m_meshExtents.max.y - this.m_meshExtents.min.y) + num78;
								characterInfo[i].vertex_TL.uv2.y = (characterInfo[i].vertex_TL.position.y - this.m_meshExtents.min.y) / (this.m_meshExtents.max.y - this.m_meshExtents.min.y) + num78;
								characterInfo[i].vertex_TR.uv2.y = characterInfo[i].vertex_BL.uv2.y;
								characterInfo[i].vertex_BR.uv2.y = characterInfo[i].vertex_TL.uv2.y;
								break;
							case TextureMappingOptions.MatchAspect:
								Debug.Log("ERROR: Cannot Match both Vertical & Horizontal.");
								break;
							}
							float num79 = (1f - (characterInfo[i].vertex_BL.uv2.y + characterInfo[i].vertex_TL.uv2.y) * characterInfo[i].aspectRatio) / 2f;
							characterInfo[i].vertex_BL.uv2.x = characterInfo[i].vertex_BL.uv2.y * characterInfo[i].aspectRatio + num79 + num78;
							characterInfo[i].vertex_TL.uv2.x = characterInfo[i].vertex_BL.uv2.x;
							characterInfo[i].vertex_TR.uv2.x = characterInfo[i].vertex_TL.uv2.y * characterInfo[i].aspectRatio + num79 + num78;
							characterInfo[i].vertex_BR.uv2.x = characterInfo[i].vertex_TR.uv2.x;
							break;
						}
						}
						switch (this.m_verticalMapping)
						{
						case TextureMappingOptions.Character:
							characterInfo[i].vertex_BL.uv2.y = 0f;
							characterInfo[i].vertex_TL.uv2.y = 1f;
							characterInfo[i].vertex_TR.uv2.y = 1f;
							characterInfo[i].vertex_BR.uv2.y = 0f;
							break;
						case TextureMappingOptions.Line:
							characterInfo[i].vertex_BL.uv2.y = (characterInfo[i].vertex_BL.position.y - tmp_LineInfo.descender) / (tmp_LineInfo.ascender - tmp_LineInfo.descender);
							characterInfo[i].vertex_TL.uv2.y = (characterInfo[i].vertex_TL.position.y - tmp_LineInfo.descender) / (tmp_LineInfo.ascender - tmp_LineInfo.descender);
							characterInfo[i].vertex_TR.uv2.y = characterInfo[i].vertex_TL.uv2.y;
							characterInfo[i].vertex_BR.uv2.y = characterInfo[i].vertex_BL.uv2.y;
							break;
						case TextureMappingOptions.Paragraph:
							characterInfo[i].vertex_BL.uv2.y = (characterInfo[i].vertex_BL.position.y - this.m_meshExtents.min.y) / (this.m_meshExtents.max.y - this.m_meshExtents.min.y);
							characterInfo[i].vertex_TL.uv2.y = (characterInfo[i].vertex_TL.position.y - this.m_meshExtents.min.y) / (this.m_meshExtents.max.y - this.m_meshExtents.min.y);
							characterInfo[i].vertex_TR.uv2.y = characterInfo[i].vertex_TL.uv2.y;
							characterInfo[i].vertex_BR.uv2.y = characterInfo[i].vertex_BL.uv2.y;
							break;
						case TextureMappingOptions.MatchAspect:
						{
							float num80 = (1f - (characterInfo[i].vertex_BL.uv2.x + characterInfo[i].vertex_TR.uv2.x) / characterInfo[i].aspectRatio) / 2f;
							characterInfo[i].vertex_BL.uv2.y = num80 + characterInfo[i].vertex_BL.uv2.x / characterInfo[i].aspectRatio;
							characterInfo[i].vertex_TL.uv2.y = num80 + characterInfo[i].vertex_TR.uv2.x / characterInfo[i].aspectRatio;
							characterInfo[i].vertex_BR.uv2.y = characterInfo[i].vertex_BL.uv2.y;
							characterInfo[i].vertex_TR.uv2.y = characterInfo[i].vertex_TL.uv2.y;
							break;
						}
						}
						num70 = characterInfo[i].scale * Mathf.Abs(f) * (1f - this.m_charWidthAdjDelta);
						if (!characterInfo[i].isUsingAlternateTypeface && (characterInfo[i].style & FontStyles.Bold) == FontStyles.Bold)
						{
							num70 *= -1f;
						}
						float num81 = characterInfo[i].vertex_BL.uv2.x;
						float num82 = characterInfo[i].vertex_BL.uv2.y;
						float num83 = characterInfo[i].vertex_TR.uv2.x;
						float num84 = characterInfo[i].vertex_TR.uv2.y;
						float num85 = (float)((int)num81);
						float num86 = (float)((int)num82);
						num81 -= num85;
						num83 -= num85;
						num82 -= num86;
						num84 -= num86;
						characterInfo[i].vertex_BL.uv2.x = base.PackUV(num81, num82);
						characterInfo[i].vertex_BL.uv2.y = num70;
						characterInfo[i].vertex_TL.uv2.x = base.PackUV(num81, num84);
						characterInfo[i].vertex_TL.uv2.y = num70;
						characterInfo[i].vertex_TR.uv2.x = base.PackUV(num83, num84);
						characterInfo[i].vertex_TR.uv2.y = num70;
						characterInfo[i].vertex_BR.uv2.x = base.PackUV(num83, num82);
						characterInfo[i].vertex_BR.uv2.y = num70;
					}
					if (i < this.m_maxVisibleCharacters && num67 < this.m_maxVisibleWords && lineNumber4 < this.m_maxVisibleLines && this.m_overflowMode != TextOverflowModes.Page)
					{
						TMP_CharacterInfo[] array = characterInfo;
						int num87 = i;
						array[num87].vertex_BL.position = array[num87].vertex_BL.position + vector10;
						TMP_CharacterInfo[] array2 = characterInfo;
						int num88 = i;
						array2[num88].vertex_TL.position = array2[num88].vertex_TL.position + vector10;
						TMP_CharacterInfo[] array3 = characterInfo;
						int num89 = i;
						array3[num89].vertex_TR.position = array3[num89].vertex_TR.position + vector10;
						TMP_CharacterInfo[] array4 = characterInfo;
						int num90 = i;
						array4[num90].vertex_BR.position = array4[num90].vertex_BR.position + vector10;
					}
					else if (i < this.m_maxVisibleCharacters && num67 < this.m_maxVisibleWords && lineNumber4 < this.m_maxVisibleLines && this.m_overflowMode == TextOverflowModes.Page && characterInfo[i].pageNumber == num7)
					{
						TMP_CharacterInfo[] array5 = characterInfo;
						int num91 = i;
						array5[num91].vertex_BL.position = array5[num91].vertex_BL.position + vector10;
						TMP_CharacterInfo[] array6 = characterInfo;
						int num92 = i;
						array6[num92].vertex_TL.position = array6[num92].vertex_TL.position + vector10;
						TMP_CharacterInfo[] array7 = characterInfo;
						int num93 = i;
						array7[num93].vertex_TR.position = array7[num93].vertex_TR.position + vector10;
						TMP_CharacterInfo[] array8 = characterInfo;
						int num94 = i;
						array8[num94].vertex_BR.position = array8[num94].vertex_BR.position + vector10;
					}
					else
					{
						characterInfo[i].vertex_BL.position = Vector3.zero;
						characterInfo[i].vertex_TL.position = Vector3.zero;
						characterInfo[i].vertex_TR.position = Vector3.zero;
						characterInfo[i].vertex_BR.position = Vector3.zero;
						characterInfo[i].isVisible = false;
					}
					if (elementType == TMP_TextElementType.Character)
					{
						this.FillCharacterVertexBuffers(i, index_X);
					}
					else if (elementType == TMP_TextElementType.Sprite)
					{
						this.FillSpriteVertexBuffers(i, index_X2);
					}
				}
				TMP_CharacterInfo[] characterInfo2 = this.m_textInfo.characterInfo;
				int num95 = i;
				characterInfo2[num95].bottomLeft = characterInfo2[num95].bottomLeft + vector10;
				TMP_CharacterInfo[] characterInfo3 = this.m_textInfo.characterInfo;
				int num96 = i;
				characterInfo3[num96].topLeft = characterInfo3[num96].topLeft + vector10;
				TMP_CharacterInfo[] characterInfo4 = this.m_textInfo.characterInfo;
				int num97 = i;
				characterInfo4[num97].topRight = characterInfo4[num97].topRight + vector10;
				TMP_CharacterInfo[] characterInfo5 = this.m_textInfo.characterInfo;
				int num98 = i;
				characterInfo5[num98].bottomRight = characterInfo5[num98].bottomRight + vector10;
				TMP_CharacterInfo[] characterInfo6 = this.m_textInfo.characterInfo;
				int num99 = i;
				characterInfo6[num99].origin = characterInfo6[num99].origin + vector10.x;
				TMP_CharacterInfo[] characterInfo7 = this.m_textInfo.characterInfo;
				int num100 = i;
				characterInfo7[num100].xAdvance = characterInfo7[num100].xAdvance + vector10.x;
				TMP_CharacterInfo[] characterInfo8 = this.m_textInfo.characterInfo;
				int num101 = i;
				characterInfo8[num101].ascender = characterInfo8[num101].ascender + vector10.y;
				TMP_CharacterInfo[] characterInfo9 = this.m_textInfo.characterInfo;
				int num102 = i;
				characterInfo9[num102].descender = characterInfo9[num102].descender + vector10.y;
				TMP_CharacterInfo[] characterInfo10 = this.m_textInfo.characterInfo;
				int num103 = i;
				characterInfo10[num103].baseLine = characterInfo10[num103].baseLine + vector10.y;
				if (lineNumber4 != num68 || i == this.m_characterCount - 1)
				{
					if (lineNumber4 != num68)
					{
						TMP_LineInfo[] lineInfo4 = this.m_textInfo.lineInfo;
						int num104 = num68;
						lineInfo4[num104].baseline = lineInfo4[num104].baseline + vector10.y;
						TMP_LineInfo[] lineInfo5 = this.m_textInfo.lineInfo;
						int num105 = num68;
						lineInfo5[num105].ascender = lineInfo5[num105].ascender + vector10.y;
						TMP_LineInfo[] lineInfo6 = this.m_textInfo.lineInfo;
						int num106 = num68;
						lineInfo6[num106].descender = lineInfo6[num106].descender + vector10.y;
						TMP_LineInfo[] lineInfo7 = this.m_textInfo.lineInfo;
						int num107 = num68;
						lineInfo7[num107].maxAdvance = lineInfo7[num107].maxAdvance + vector10.x;
						this.m_textInfo.lineInfo[num68].lineExtents.min = new Vector2(this.m_textInfo.characterInfo[this.m_textInfo.lineInfo[num68].firstCharacterIndex].bottomLeft.x, this.m_textInfo.lineInfo[num68].descender);
						this.m_textInfo.lineInfo[num68].lineExtents.max = new Vector2(this.m_textInfo.characterInfo[this.m_textInfo.lineInfo[num68].lastVisibleCharacterIndex].topRight.x, this.m_textInfo.lineInfo[num68].ascender);
					}
					if (i == this.m_characterCount - 1)
					{
						TMP_LineInfo[] lineInfo8 = this.m_textInfo.lineInfo;
						int num108 = lineNumber4;
						lineInfo8[num108].baseline = lineInfo8[num108].baseline + vector10.y;
						TMP_LineInfo[] lineInfo9 = this.m_textInfo.lineInfo;
						int num109 = lineNumber4;
						lineInfo9[num109].ascender = lineInfo9[num109].ascender + vector10.y;
						TMP_LineInfo[] lineInfo10 = this.m_textInfo.lineInfo;
						int num110 = lineNumber4;
						lineInfo10[num110].descender = lineInfo10[num110].descender + vector10.y;
						TMP_LineInfo[] lineInfo11 = this.m_textInfo.lineInfo;
						int num111 = lineNumber4;
						lineInfo11[num111].maxAdvance = lineInfo11[num111].maxAdvance + vector10.x;
						this.m_textInfo.lineInfo[lineNumber4].lineExtents.min = new Vector2(this.m_textInfo.characterInfo[this.m_textInfo.lineInfo[lineNumber4].firstCharacterIndex].bottomLeft.x, this.m_textInfo.lineInfo[lineNumber4].descender);
						this.m_textInfo.lineInfo[lineNumber4].lineExtents.max = new Vector2(this.m_textInfo.characterInfo[this.m_textInfo.lineInfo[lineNumber4].lastVisibleCharacterIndex].topRight.x, this.m_textInfo.lineInfo[lineNumber4].ascender);
					}
				}
				if (char.IsLetterOrDigit(character) || character == '-' || character == '­' || character == '‐' || character == '‑')
				{
					if (!flag16)
					{
						flag16 = true;
						num69 = i;
					}
					if (flag16 && i == this.m_characterCount - 1)
					{
						int num112 = this.m_textInfo.wordInfo.Length;
						int wordCount = this.m_textInfo.wordCount;
						if (this.m_textInfo.wordCount + 1 > num112)
						{
							TMP_TextInfo.Resize<TMP_WordInfo>(ref this.m_textInfo.wordInfo, num112 + 1);
						}
						int num113 = i;
						this.m_textInfo.wordInfo[wordCount].firstCharacterIndex = num69;
						this.m_textInfo.wordInfo[wordCount].lastCharacterIndex = num113;
						this.m_textInfo.wordInfo[wordCount].characterCount = num113 - num69 + 1;
						this.m_textInfo.wordInfo[wordCount].textComponent = this;
						num67++;
						this.m_textInfo.wordCount++;
						TMP_LineInfo[] lineInfo12 = this.m_textInfo.lineInfo;
						int num114 = lineNumber4;
						lineInfo12[num114].wordCount = lineInfo12[num114].wordCount + 1;
					}
				}
				else if ((flag16 || (i == 0 && (!char.IsPunctuation(character) || char.IsWhiteSpace(character) || character == '​' || i == this.m_characterCount - 1))) && (i <= 0 || i >= characterInfo.Length - 1 || i >= this.m_characterCount || (character != '\'' && character != '’') || !char.IsLetterOrDigit(characterInfo[i - 1].character) || !char.IsLetterOrDigit(characterInfo[i + 1].character)))
				{
					int num113 = (i == this.m_characterCount - 1 && char.IsLetterOrDigit(character)) ? i : (i - 1);
					flag16 = false;
					int num115 = this.m_textInfo.wordInfo.Length;
					int wordCount2 = this.m_textInfo.wordCount;
					if (this.m_textInfo.wordCount + 1 > num115)
					{
						TMP_TextInfo.Resize<TMP_WordInfo>(ref this.m_textInfo.wordInfo, num115 + 1);
					}
					this.m_textInfo.wordInfo[wordCount2].firstCharacterIndex = num69;
					this.m_textInfo.wordInfo[wordCount2].lastCharacterIndex = num113;
					this.m_textInfo.wordInfo[wordCount2].characterCount = num113 - num69 + 1;
					this.m_textInfo.wordInfo[wordCount2].textComponent = this;
					num67++;
					this.m_textInfo.wordCount++;
					TMP_LineInfo[] lineInfo13 = this.m_textInfo.lineInfo;
					int num116 = lineNumber4;
					lineInfo13[num116].wordCount = lineInfo13[num116].wordCount + 1;
				}
				if ((this.m_textInfo.characterInfo[i].style & FontStyles.Underline) == FontStyles.Underline)
				{
					bool flag17 = true;
					int pageNumber = this.m_textInfo.characterInfo[i].pageNumber;
					this.m_textInfo.characterInfo[i].underlineVertexIndex = num66;
					if (i > this.m_maxVisibleCharacters || lineNumber4 > this.m_maxVisibleLines || (this.m_overflowMode == TextOverflowModes.Page && pageNumber + 1 != this.m_pageToDisplay))
					{
						flag17 = false;
					}
					if (!char.IsWhiteSpace(character) && character != '​')
					{
						num73 = Mathf.Max(num73, this.m_textInfo.characterInfo[i].scale);
						num71 = Mathf.Max(num71, Mathf.Abs(num70));
						num74 = Mathf.Min((pageNumber == num75) ? num74 : TMP_Text.k_LargePositiveFloat, this.m_textInfo.characterInfo[i].baseLine + base.font.m_FaceInfo.underlineOffset * num73);
						num75 = pageNumber;
					}
					if (!flag && flag17 && i <= tmp_LineInfo.lastVisibleCharacterIndex && character != '\n' && character != '\v' && character != '\r' && (i != tmp_LineInfo.lastVisibleCharacterIndex || !char.IsSeparator(character)))
					{
						flag = true;
						num72 = this.m_textInfo.characterInfo[i].scale;
						if (num73 == 0f)
						{
							num73 = num72;
							num71 = num70;
						}
						zero = new Vector3(this.m_textInfo.characterInfo[i].bottomLeft.x, num74, 0f);
						color = this.m_textInfo.characterInfo[i].underlineColor;
					}
					if (flag && this.m_characterCount == 1)
					{
						flag = false;
						zero2 = new Vector3(this.m_textInfo.characterInfo[i].topRight.x, num74, 0f);
						float scale = this.m_textInfo.characterInfo[i].scale;
						this.DrawUnderlineMesh(zero, zero2, ref num66, num72, scale, num73, num71, color);
						num73 = 0f;
						num71 = 0f;
						num74 = TMP_Text.k_LargePositiveFloat;
					}
					else if (flag && (i == tmp_LineInfo.lastCharacterIndex || i >= tmp_LineInfo.lastVisibleCharacterIndex))
					{
						float scale;
						if (char.IsWhiteSpace(character) || character == '​')
						{
							int lastVisibleCharacterIndex = tmp_LineInfo.lastVisibleCharacterIndex;
							zero2 = new Vector3(this.m_textInfo.characterInfo[lastVisibleCharacterIndex].topRight.x, num74, 0f);
							scale = this.m_textInfo.characterInfo[lastVisibleCharacterIndex].scale;
						}
						else
						{
							zero2 = new Vector3(this.m_textInfo.characterInfo[i].topRight.x, num74, 0f);
							scale = this.m_textInfo.characterInfo[i].scale;
						}
						flag = false;
						this.DrawUnderlineMesh(zero, zero2, ref num66, num72, scale, num73, num71, color);
						num73 = 0f;
						num71 = 0f;
						num74 = TMP_Text.k_LargePositiveFloat;
					}
					else if (flag && !flag17)
					{
						flag = false;
						zero2 = new Vector3(this.m_textInfo.characterInfo[i - 1].topRight.x, num74, 0f);
						float scale = this.m_textInfo.characterInfo[i - 1].scale;
						this.DrawUnderlineMesh(zero, zero2, ref num66, num72, scale, num73, num71, color);
						num73 = 0f;
						num71 = 0f;
						num74 = TMP_Text.k_LargePositiveFloat;
					}
					else if (flag && i < this.m_characterCount - 1 && !color.Compare(this.m_textInfo.characterInfo[i + 1].underlineColor))
					{
						flag = false;
						zero2 = new Vector3(this.m_textInfo.characterInfo[i].topRight.x, num74, 0f);
						float scale = this.m_textInfo.characterInfo[i].scale;
						this.DrawUnderlineMesh(zero, zero2, ref num66, num72, scale, num73, num71, color);
						num73 = 0f;
						num71 = 0f;
						num74 = TMP_Text.k_LargePositiveFloat;
					}
				}
				else if (flag)
				{
					flag = false;
					zero2 = new Vector3(this.m_textInfo.characterInfo[i - 1].topRight.x, num74, 0f);
					float scale = this.m_textInfo.characterInfo[i - 1].scale;
					this.DrawUnderlineMesh(zero, zero2, ref num66, num72, scale, num73, num71, color);
					num73 = 0f;
					num71 = 0f;
					num74 = TMP_Text.k_LargePositiveFloat;
				}
				bool flag18 = (this.m_textInfo.characterInfo[i].style & FontStyles.Strikethrough) == FontStyles.Strikethrough;
				float strikethroughOffset = fontAsset.m_FaceInfo.strikethroughOffset;
				if (flag18)
				{
					bool flag19 = true;
					this.m_textInfo.characterInfo[i].strikethroughVertexIndex = num66;
					if (i > this.m_maxVisibleCharacters || lineNumber4 > this.m_maxVisibleLines || (this.m_overflowMode == TextOverflowModes.Page && this.m_textInfo.characterInfo[i].pageNumber + 1 != this.m_pageToDisplay))
					{
						flag19 = false;
					}
					if (!flag2 && flag19 && i <= tmp_LineInfo.lastVisibleCharacterIndex && character != '\n' && character != '\v' && character != '\r' && (i != tmp_LineInfo.lastVisibleCharacterIndex || !char.IsSeparator(character)))
					{
						flag2 = true;
						num76 = this.m_textInfo.characterInfo[i].pointSize;
						num77 = this.m_textInfo.characterInfo[i].scale;
						zero3 = new Vector3(this.m_textInfo.characterInfo[i].bottomLeft.x, this.m_textInfo.characterInfo[i].baseLine + strikethroughOffset * num77, 0f);
						underlineColor = this.m_textInfo.characterInfo[i].strikethroughColor;
						b3 = this.m_textInfo.characterInfo[i].baseLine;
					}
					if (flag2 && this.m_characterCount == 1)
					{
						flag2 = false;
						zero4 = new Vector3(this.m_textInfo.characterInfo[i].topRight.x, this.m_textInfo.characterInfo[i].baseLine + strikethroughOffset * num77, 0f);
						this.DrawUnderlineMesh(zero3, zero4, ref num66, num77, num77, num77, num70, underlineColor);
					}
					else if (flag2 && i == tmp_LineInfo.lastCharacterIndex)
					{
						if (char.IsWhiteSpace(character) || character == '​')
						{
							int lastVisibleCharacterIndex2 = tmp_LineInfo.lastVisibleCharacterIndex;
							zero4 = new Vector3(this.m_textInfo.characterInfo[lastVisibleCharacterIndex2].topRight.x, this.m_textInfo.characterInfo[lastVisibleCharacterIndex2].baseLine + strikethroughOffset * num77, 0f);
						}
						else
						{
							zero4 = new Vector3(this.m_textInfo.characterInfo[i].topRight.x, this.m_textInfo.characterInfo[i].baseLine + strikethroughOffset * num77, 0f);
						}
						flag2 = false;
						this.DrawUnderlineMesh(zero3, zero4, ref num66, num77, num77, num77, num70, underlineColor);
					}
					else if (flag2 && i < this.m_characterCount && (this.m_textInfo.characterInfo[i + 1].pointSize != num76 || !TMP_Math.Approximately(this.m_textInfo.characterInfo[i + 1].baseLine + vector10.y, b3)))
					{
						flag2 = false;
						int lastVisibleCharacterIndex3 = tmp_LineInfo.lastVisibleCharacterIndex;
						if (i > lastVisibleCharacterIndex3)
						{
							zero4 = new Vector3(this.m_textInfo.characterInfo[lastVisibleCharacterIndex3].topRight.x, this.m_textInfo.characterInfo[lastVisibleCharacterIndex3].baseLine + strikethroughOffset * num77, 0f);
						}
						else
						{
							zero4 = new Vector3(this.m_textInfo.characterInfo[i].topRight.x, this.m_textInfo.characterInfo[i].baseLine + strikethroughOffset * num77, 0f);
						}
						this.DrawUnderlineMesh(zero3, zero4, ref num66, num77, num77, num77, num70, underlineColor);
					}
					else if (flag2 && i < this.m_characterCount && fontAsset.GetInstanceID() != characterInfo[i + 1].fontAsset.GetInstanceID())
					{
						flag2 = false;
						zero4 = new Vector3(this.m_textInfo.characterInfo[i].topRight.x, this.m_textInfo.characterInfo[i].baseLine + strikethroughOffset * num77, 0f);
						this.DrawUnderlineMesh(zero3, zero4, ref num66, num77, num77, num77, num70, underlineColor);
					}
					else if (flag2 && !flag19)
					{
						flag2 = false;
						zero4 = new Vector3(this.m_textInfo.characterInfo[i - 1].topRight.x, this.m_textInfo.characterInfo[i - 1].baseLine + strikethroughOffset * num77, 0f);
						this.DrawUnderlineMesh(zero3, zero4, ref num66, num77, num77, num77, num70, underlineColor);
					}
				}
				else if (flag2)
				{
					flag2 = false;
					zero4 = new Vector3(this.m_textInfo.characterInfo[i - 1].topRight.x, this.m_textInfo.characterInfo[i - 1].baseLine + strikethroughOffset * num77, 0f);
					this.DrawUnderlineMesh(zero3, zero4, ref num66, num77, num77, num77, num70, underlineColor);
				}
				if ((this.m_textInfo.characterInfo[i].style & FontStyles.Highlight) == FontStyles.Highlight)
				{
					bool flag20 = true;
					int pageNumber2 = this.m_textInfo.characterInfo[i].pageNumber;
					if (i > this.m_maxVisibleCharacters || lineNumber4 > this.m_maxVisibleLines || (this.m_overflowMode == TextOverflowModes.Page && pageNumber2 + 1 != this.m_pageToDisplay))
					{
						flag20 = false;
					}
					if (!flag3 && flag20 && i <= tmp_LineInfo.lastVisibleCharacterIndex && character != '\n' && character != '\v' && character != '\r' && (i != tmp_LineInfo.lastVisibleCharacterIndex || !char.IsSeparator(character)))
					{
						flag3 = true;
						vector = TMP_Text.k_LargePositiveVector2;
						vector2 = TMP_Text.k_LargeNegativeVector2;
						highlightState = this.m_textInfo.characterInfo[i].highlightState;
					}
					if (flag3)
					{
						TMP_CharacterInfo tmp_CharacterInfo = this.m_textInfo.characterInfo[i];
						HighlightState highlightState2 = tmp_CharacterInfo.highlightState;
						bool flag21 = false;
						if (highlightState != tmp_CharacterInfo.highlightState)
						{
							vector2.x = (vector2.x - highlightState.padding.right + tmp_CharacterInfo.bottomLeft.x) / 2f;
							vector.y = Mathf.Min(vector.y, tmp_CharacterInfo.descender);
							vector2.y = Mathf.Max(vector2.y, tmp_CharacterInfo.ascender);
							this.DrawTextHighlight(vector, vector2, ref num66, highlightState.color);
							flag3 = true;
							vector = new Vector2(vector2.x, tmp_CharacterInfo.descender - highlightState2.padding.bottom);
							vector2 = new Vector2(tmp_CharacterInfo.topRight.x + highlightState2.padding.right, tmp_CharacterInfo.ascender + highlightState2.padding.top);
							highlightState = tmp_CharacterInfo.highlightState;
							flag21 = true;
						}
						if (!flag21)
						{
							vector.x = Mathf.Min(vector.x, tmp_CharacterInfo.bottomLeft.x - highlightState.padding.left);
							vector.y = Mathf.Min(vector.y, tmp_CharacterInfo.descender - highlightState.padding.bottom);
							vector2.x = Mathf.Max(vector2.x, tmp_CharacterInfo.topRight.x + highlightState.padding.right);
							vector2.y = Mathf.Max(vector2.y, tmp_CharacterInfo.ascender + highlightState.padding.top);
						}
					}
					if (flag3 && this.m_characterCount == 1)
					{
						flag3 = false;
						this.DrawTextHighlight(vector, vector2, ref num66, highlightState.color);
					}
					else if (flag3 && (i == tmp_LineInfo.lastCharacterIndex || i >= tmp_LineInfo.lastVisibleCharacterIndex))
					{
						flag3 = false;
						this.DrawTextHighlight(vector, vector2, ref num66, highlightState.color);
					}
					else if (flag3 && !flag20)
					{
						flag3 = false;
						this.DrawTextHighlight(vector, vector2, ref num66, highlightState.color);
					}
				}
				else if (flag3)
				{
					flag3 = false;
					this.DrawTextHighlight(vector, vector2, ref num66, highlightState.color);
				}
				num68 = lineNumber4;
				i++;
				continue;
				IL_43BB:
				if (character == '\n' || character == '­' || character == '​' || character == '⁠' || character == '\u0003')
				{
					goto IL_464B;
				}
				char character2 = characterInfo[tmp_LineInfo.lastCharacterIndex].character;
				bool flag22 = (alignment & HorizontalAlignmentOptions.Flush) == HorizontalAlignmentOptions.Flush;
				if ((!char.IsControl(character2) && lineNumber4 < this.m_lineNumber) || flag22 || tmp_LineInfo.maxAdvance > tmp_LineInfo.width)
				{
					if (lineNumber4 != num68 || i == 0 || i == this.m_firstVisibleCharacter)
					{
						if (!this.m_isRightToLeft)
						{
							vector9 = new Vector3(tmp_LineInfo.marginLeft, 0f, 0f);
						}
						else
						{
							vector9 = new Vector3(tmp_LineInfo.marginLeft + tmp_LineInfo.width, 0f, 0f);
						}
						flag15 = char.IsSeparator(character);
						goto IL_464B;
					}
					float num117 = (!this.m_isRightToLeft) ? (tmp_LineInfo.width - tmp_LineInfo.maxAdvance) : (tmp_LineInfo.width + tmp_LineInfo.maxAdvance);
					int num118 = tmp_LineInfo.visibleCharacterCount - 1 + tmp_LineInfo.controlCharacterCount;
					int num119 = (characterInfo[tmp_LineInfo.lastCharacterIndex].isVisible ? tmp_LineInfo.spaceCount : (tmp_LineInfo.spaceCount - 1)) - tmp_LineInfo.controlCharacterCount;
					if (flag15)
					{
						num119--;
						num118++;
					}
					float num120 = (num119 > 0) ? this.m_wordWrappingRatios : 1f;
					if (num119 < 1)
					{
						num119 = 1;
					}
					if (character != '\u00a0' && (character == '\t' || char.IsSeparator(character)))
					{
						if (!this.m_isRightToLeft)
						{
							vector9 += new Vector3(num117 * (1f - num120) / (float)num119, 0f, 0f);
							goto IL_464B;
						}
						vector9 -= new Vector3(num117 * (1f - num120) / (float)num119, 0f, 0f);
						goto IL_464B;
					}
					else
					{
						if (!this.m_isRightToLeft)
						{
							vector9 += new Vector3(num117 * num120 / (float)num118, 0f, 0f);
							goto IL_464B;
						}
						vector9 -= new Vector3(num117 * num120 / (float)num118, 0f, 0f);
						goto IL_464B;
					}
				}
				else
				{
					if (!this.m_isRightToLeft)
					{
						vector9 = new Vector3(tmp_LineInfo.marginLeft, 0f, 0f);
						goto IL_464B;
					}
					vector9 = new Vector3(tmp_LineInfo.marginLeft + tmp_LineInfo.width, 0f, 0f);
					goto IL_464B;
				}
			}
			this.m_textInfo.characterCount = this.m_characterCount;
			this.m_textInfo.spriteCount = this.m_spriteCount;
			this.m_textInfo.lineCount = lineCount;
			this.m_textInfo.wordCount = ((num67 != 0 && this.m_characterCount > 0) ? num67 : 1);
			this.m_textInfo.pageCount = this.m_pageNumber + 1;
			if (this.m_renderMode == TextRenderFlags.Render && this.IsActive())
			{
				Action<TMP_TextInfo> onPreRenderText = this.OnPreRenderText;
				if (onPreRenderText != null)
				{
					onPreRenderText(this.m_textInfo);
				}
				if (this.m_geometrySortingOrder != VertexSortingOrder.Normal)
				{
					this.m_textInfo.meshInfo[0].SortGeometry(VertexSortingOrder.Reverse);
				}
				this.m_mesh.MarkDynamic();
				this.m_mesh.vertices = this.m_textInfo.meshInfo[0].vertices;
				this.m_mesh.uv = this.m_textInfo.meshInfo[0].uvs0;
				this.m_mesh.uv2 = this.m_textInfo.meshInfo[0].uvs2;
				this.m_mesh.colors32 = this.m_textInfo.meshInfo[0].colors32;
				this.m_mesh.RecalculateBounds();
				for (int j = 1; j < this.m_textInfo.materialCount; j++)
				{
					this.m_textInfo.meshInfo[j].ClearUnusedVertices();
					if (!(this.m_subTextObjects[j] == null))
					{
						if (this.m_geometrySortingOrder != VertexSortingOrder.Normal)
						{
							this.m_textInfo.meshInfo[j].SortGeometry(VertexSortingOrder.Reverse);
						}
						this.m_subTextObjects[j].mesh.vertices = this.m_textInfo.meshInfo[j].vertices;
						this.m_subTextObjects[j].mesh.uv = this.m_textInfo.meshInfo[j].uvs0;
						this.m_subTextObjects[j].mesh.uv2 = this.m_textInfo.meshInfo[j].uvs2;
						this.m_subTextObjects[j].mesh.colors32 = this.m_textInfo.meshInfo[j].colors32;
						this.m_subTextObjects[j].mesh.RecalculateBounds();
					}
				}
			}
			TMPro_EventManager.ON_TEXT_CHANGED(this);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x0000C28C File Offset: 0x0000A48C
		protected override Vector3[] GetTextContainerLocalCorners()
		{
			if (this.m_rectTransform == null)
			{
				this.m_rectTransform = base.rectTransform;
			}
			this.m_rectTransform.GetLocalCorners(this.m_RectTransformCorners);
			return this.m_RectTransformCorners;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x0000C2C0 File Offset: 0x0000A4C0
		private void SetMeshFilters(bool state)
		{
			if (this.m_meshFilter != null)
			{
				if (state)
				{
					this.m_meshFilter.sharedMesh = this.m_mesh;
				}
				else
				{
					this.m_meshFilter.sharedMesh = null;
				}
			}
			int num = 1;
			while (num < this.m_subTextObjects.Length && this.m_subTextObjects[num] != null)
			{
				if (this.m_subTextObjects[num].meshFilter != null)
				{
					if (state)
					{
						this.m_subTextObjects[num].meshFilter.sharedMesh = this.m_subTextObjects[num].mesh;
					}
					else
					{
						this.m_subTextObjects[num].meshFilter.sharedMesh = null;
					}
				}
				num++;
			}
		}

		// Token: 0x06000092 RID: 146 RVA: 0x0000C36C File Offset: 0x0000A56C
		protected override void SetActiveSubMeshes(bool state)
		{
			int num = 1;
			while (num < this.m_subTextObjects.Length && this.m_subTextObjects[num] != null)
			{
				if (this.m_subTextObjects[num].enabled != state)
				{
					this.m_subTextObjects[num].enabled = state;
				}
				num++;
			}
		}

		// Token: 0x06000093 RID: 147 RVA: 0x0000C3BC File Offset: 0x0000A5BC
		protected void SetActiveSubTextObjectRenderers(bool state)
		{
			int num = 1;
			while (num < this.m_subTextObjects.Length && this.m_subTextObjects[num] != null)
			{
				Renderer renderer = this.m_subTextObjects[num].renderer;
				if (renderer != null && renderer.enabled != state)
				{
					renderer.enabled = state;
				}
				num++;
			}
		}

		// Token: 0x06000094 RID: 148 RVA: 0x0000C414 File Offset: 0x0000A614
		protected override void DestroySubMeshObjects()
		{
			int num = 1;
			while (num < this.m_subTextObjects.Length && this.m_subTextObjects[num] != null)
			{
				UnityEngine.Object.DestroyImmediate(this.m_subTextObjects[num]);
				num++;
			}
		}

		// Token: 0x06000095 RID: 149 RVA: 0x0000C454 File Offset: 0x0000A654
		internal void UpdateSubMeshSortingLayerID(int id)
		{
			for (int i = 1; i < this.m_subTextObjects.Length; i++)
			{
				TMP_SubMesh tmp_SubMesh = this.m_subTextObjects[i];
				if (tmp_SubMesh != null && tmp_SubMesh.renderer != null)
				{
					tmp_SubMesh.renderer.sortingLayerID = id;
				}
			}
		}

		// Token: 0x06000096 RID: 150 RVA: 0x0000C4A0 File Offset: 0x0000A6A0
		internal void UpdateSubMeshSortingOrder(int order)
		{
			for (int i = 1; i < this.m_subTextObjects.Length; i++)
			{
				TMP_SubMesh tmp_SubMesh = this.m_subTextObjects[i];
				if (tmp_SubMesh != null && tmp_SubMesh.renderer != null)
				{
					tmp_SubMesh.renderer.sortingOrder = order;
				}
			}
		}

		// Token: 0x06000097 RID: 151 RVA: 0x0000C4EC File Offset: 0x0000A6EC
		protected override Bounds GetCompoundBounds()
		{
			Bounds bounds = this.m_mesh.bounds;
			Vector3 min = bounds.min;
			Vector3 max = bounds.max;
			int num = 1;
			while (num < this.m_subTextObjects.Length && this.m_subTextObjects[num] != null)
			{
				Bounds bounds2 = this.m_subTextObjects[num].mesh.bounds;
				min.x = ((min.x < bounds2.min.x) ? min.x : bounds2.min.x);
				min.y = ((min.y < bounds2.min.y) ? min.y : bounds2.min.y);
				max.x = ((max.x > bounds2.max.x) ? max.x : bounds2.max.x);
				max.y = ((max.y > bounds2.max.y) ? max.y : bounds2.max.y);
				num++;
			}
			Vector3 center = (min + max) / 2f;
			Vector2 v = max - min;
			return new Bounds(center, v);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x0000C640 File Offset: 0x0000A840
		private void UpdateSDFScale(float scaleDelta)
		{
			if (scaleDelta == 0f || scaleDelta == float.PositiveInfinity || scaleDelta == float.NegativeInfinity)
			{
				this.m_havePropertiesChanged = true;
				this.OnPreRenderObject();
				return;
			}
			for (int i = 0; i < this.m_textInfo.materialCount; i++)
			{
				TMP_MeshInfo tmp_MeshInfo = this.m_textInfo.meshInfo[i];
				for (int j = 0; j < tmp_MeshInfo.uvs2.Length; j++)
				{
					Vector2[] uvs = tmp_MeshInfo.uvs2;
					int num = j;
					uvs[num].y = uvs[num].y * Mathf.Abs(scaleDelta);
				}
			}
			for (int k = 0; k < this.m_textInfo.meshInfo.Length; k++)
			{
				if (k == 0)
				{
					this.m_mesh.uv2 = this.m_textInfo.meshInfo[0].uvs2;
				}
				else
				{
					this.m_subTextObjects[k].mesh.uv2 = this.m_textInfo.meshInfo[k].uvs2;
				}
			}
		}

		// Token: 0x06000099 RID: 153 RVA: 0x0000C72F File Offset: 0x0000A92F
		public TextMeshPro()
		{
		}

		// Token: 0x0600009A RID: 154 RVA: 0x0000C768 File Offset: 0x0000A968
		// Note: this type is marked as 'beforefieldinit'.
		static TextMeshPro()
		{
		}

		// Token: 0x04000031 RID: 49
		[SerializeField]
		internal int _SortingLayer;

		// Token: 0x04000032 RID: 50
		[SerializeField]
		internal int _SortingLayerID;

		// Token: 0x04000033 RID: 51
		[SerializeField]
		internal int _SortingOrder;

		// Token: 0x04000034 RID: 52
		[CompilerGenerated]
		private new Action<TMP_TextInfo> OnPreRenderText;

		// Token: 0x04000035 RID: 53
		private bool m_currentAutoSizeMode;

		// Token: 0x04000036 RID: 54
		[SerializeField]
		private bool m_hasFontAssetChanged;

		// Token: 0x04000037 RID: 55
		private float m_previousLossyScaleY = -1f;

		// Token: 0x04000038 RID: 56
		[SerializeField]
		private Renderer m_renderer;

		// Token: 0x04000039 RID: 57
		private MeshFilter m_meshFilter;

		// Token: 0x0400003A RID: 58
		private bool m_isFirstAllocation;

		// Token: 0x0400003B RID: 59
		private int m_max_characters = 8;

		// Token: 0x0400003C RID: 60
		private int m_max_numberOfLines = 4;

		// Token: 0x0400003D RID: 61
		private TMP_SubMesh[] m_subTextObjects = new TMP_SubMesh[8];

		// Token: 0x0400003E RID: 62
		[SerializeField]
		private MaskingTypes m_maskType;

		// Token: 0x0400003F RID: 63
		private Matrix4x4 m_EnvMapMatrix;

		// Token: 0x04000040 RID: 64
		private Vector3[] m_RectTransformCorners = new Vector3[4];

		// Token: 0x04000041 RID: 65
		[NonSerialized]
		private bool m_isRegisteredForEvents;

		// Token: 0x04000042 RID: 66
		private static ProfilerMarker k_GenerateTextMarker = new ProfilerMarker("TMP Layout Text");

		// Token: 0x04000043 RID: 67
		private static ProfilerMarker k_SetArraySizesMarker = new ProfilerMarker("TMP.SetArraySizes");

		// Token: 0x04000044 RID: 68
		private static ProfilerMarker k_GenerateTextPhaseIMarker = new ProfilerMarker("TMP GenerateText - Phase I");

		// Token: 0x04000045 RID: 69
		private static ProfilerMarker k_ParseMarkupTextMarker = new ProfilerMarker("TMP Parse Markup Text");

		// Token: 0x04000046 RID: 70
		private static ProfilerMarker k_CharacterLookupMarker = new ProfilerMarker("TMP Lookup Character & Glyph Data");

		// Token: 0x04000047 RID: 71
		private static ProfilerMarker k_HandleGPOSFeaturesMarker = new ProfilerMarker("TMP Handle GPOS Features");

		// Token: 0x04000048 RID: 72
		private static ProfilerMarker k_CalculateVerticesPositionMarker = new ProfilerMarker("TMP Calculate Vertices Position");

		// Token: 0x04000049 RID: 73
		private static ProfilerMarker k_ComputeTextMetricsMarker = new ProfilerMarker("TMP Compute Text Metrics");

		// Token: 0x0400004A RID: 74
		private static ProfilerMarker k_HandleVisibleCharacterMarker = new ProfilerMarker("TMP Handle Visible Character");

		// Token: 0x0400004B RID: 75
		private static ProfilerMarker k_HandleWhiteSpacesMarker = new ProfilerMarker("TMP Handle White Space & Control Character");

		// Token: 0x0400004C RID: 76
		private static ProfilerMarker k_HandleHorizontalLineBreakingMarker = new ProfilerMarker("TMP Handle Horizontal Line Breaking");

		// Token: 0x0400004D RID: 77
		private static ProfilerMarker k_HandleVerticalLineBreakingMarker = new ProfilerMarker("TMP Handle Vertical Line Breaking");

		// Token: 0x0400004E RID: 78
		private static ProfilerMarker k_SaveGlyphVertexDataMarker = new ProfilerMarker("TMP Save Glyph Vertex Data");

		// Token: 0x0400004F RID: 79
		private static ProfilerMarker k_ComputeCharacterAdvanceMarker = new ProfilerMarker("TMP Compute Character Advance");

		// Token: 0x04000050 RID: 80
		private static ProfilerMarker k_HandleCarriageReturnMarker = new ProfilerMarker("TMP Handle Carriage Return");

		// Token: 0x04000051 RID: 81
		private static ProfilerMarker k_HandleLineTerminationMarker = new ProfilerMarker("TMP Handle Line Termination");

		// Token: 0x04000052 RID: 82
		private static ProfilerMarker k_SavePageInfoMarker = new ProfilerMarker("TMP Save Text Extent & Page Info");

		// Token: 0x04000053 RID: 83
		private static ProfilerMarker k_SaveProcessingStatesMarker = new ProfilerMarker("TMP Save Processing States");

		// Token: 0x04000054 RID: 84
		private static ProfilerMarker k_GenerateTextPhaseIIMarker = new ProfilerMarker("TMP GenerateText - Phase II");

		// Token: 0x04000055 RID: 85
		private static ProfilerMarker k_GenerateTextPhaseIIIMarker = new ProfilerMarker("TMP GenerateText - Phase III");
	}
}
