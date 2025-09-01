using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace TMPro
{
	// Token: 0x02000059 RID: 89
	[ExecuteAlways]
	[RequireComponent(typeof(CanvasRenderer))]
	public class TMP_SubMeshUI : MaskableGraphic
	{
		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060003FE RID: 1022 RVA: 0x00027170 File Offset: 0x00025370
		// (set) Token: 0x060003FF RID: 1023 RVA: 0x00027178 File Offset: 0x00025378
		public TMP_FontAsset fontAsset
		{
			get
			{
				return this.m_fontAsset;
			}
			set
			{
				this.m_fontAsset = value;
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000400 RID: 1024 RVA: 0x00027181 File Offset: 0x00025381
		// (set) Token: 0x06000401 RID: 1025 RVA: 0x00027189 File Offset: 0x00025389
		public TMP_SpriteAsset spriteAsset
		{
			get
			{
				return this.m_spriteAsset;
			}
			set
			{
				this.m_spriteAsset = value;
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000402 RID: 1026 RVA: 0x00027192 File Offset: 0x00025392
		public override Texture mainTexture
		{
			get
			{
				if (this.sharedMaterial != null)
				{
					return this.sharedMaterial.GetTexture(ShaderUtilities.ID_MainTex);
				}
				return null;
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000403 RID: 1027 RVA: 0x000271B4 File Offset: 0x000253B4
		// (set) Token: 0x06000404 RID: 1028 RVA: 0x000271C4 File Offset: 0x000253C4
		public override Material material
		{
			get
			{
				return this.GetMaterial(this.m_sharedMaterial);
			}
			set
			{
				if (this.m_sharedMaterial != null && this.m_sharedMaterial.GetInstanceID() == value.GetInstanceID())
				{
					return;
				}
				this.m_material = value;
				this.m_sharedMaterial = value;
				this.m_padding = this.GetPaddingForMaterial();
				this.SetVerticesDirty();
				this.SetMaterialDirty();
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000405 RID: 1029 RVA: 0x0002721B File Offset: 0x0002541B
		// (set) Token: 0x06000406 RID: 1030 RVA: 0x00027223 File Offset: 0x00025423
		public Material sharedMaterial
		{
			get
			{
				return this.m_sharedMaterial;
			}
			set
			{
				this.SetSharedMaterial(value);
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000407 RID: 1031 RVA: 0x0002722C File Offset: 0x0002542C
		// (set) Token: 0x06000408 RID: 1032 RVA: 0x00027234 File Offset: 0x00025434
		public Material fallbackMaterial
		{
			get
			{
				return this.m_fallbackMaterial;
			}
			set
			{
				if (this.m_fallbackMaterial == value)
				{
					return;
				}
				if (this.m_fallbackMaterial != null && this.m_fallbackMaterial != value)
				{
					TMP_MaterialManager.ReleaseFallbackMaterial(this.m_fallbackMaterial);
				}
				this.m_fallbackMaterial = value;
				TMP_MaterialManager.AddFallbackMaterialReference(this.m_fallbackMaterial);
				this.SetSharedMaterial(this.m_fallbackMaterial);
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000409 RID: 1033 RVA: 0x00027295 File Offset: 0x00025495
		// (set) Token: 0x0600040A RID: 1034 RVA: 0x0002729D File Offset: 0x0002549D
		public Material fallbackSourceMaterial
		{
			get
			{
				return this.m_fallbackSourceMaterial;
			}
			set
			{
				this.m_fallbackSourceMaterial = value;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x0600040B RID: 1035 RVA: 0x000272A6 File Offset: 0x000254A6
		public override Material materialForRendering
		{
			get
			{
				return TMP_MaterialManager.GetMaterialForRendering(this, this.m_sharedMaterial);
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x0600040C RID: 1036 RVA: 0x000272B4 File Offset: 0x000254B4
		// (set) Token: 0x0600040D RID: 1037 RVA: 0x000272BC File Offset: 0x000254BC
		public bool isDefaultMaterial
		{
			get
			{
				return this.m_isDefaultMaterial;
			}
			set
			{
				this.m_isDefaultMaterial = value;
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x0600040E RID: 1038 RVA: 0x000272C5 File Offset: 0x000254C5
		// (set) Token: 0x0600040F RID: 1039 RVA: 0x000272CD File Offset: 0x000254CD
		public float padding
		{
			get
			{
				return this.m_padding;
			}
			set
			{
				this.m_padding = value;
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000410 RID: 1040 RVA: 0x000272D6 File Offset: 0x000254D6
		// (set) Token: 0x06000411 RID: 1041 RVA: 0x00027304 File Offset: 0x00025504
		public Mesh mesh
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
			set
			{
				this.m_mesh = value;
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000412 RID: 1042 RVA: 0x0002730D File Offset: 0x0002550D
		public TMP_Text textComponent
		{
			get
			{
				if (this.m_TextComponent == null)
				{
					this.m_TextComponent = base.GetComponentInParent<TextMeshProUGUI>();
				}
				return this.m_TextComponent;
			}
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x00027330 File Offset: 0x00025530
		public static TMP_SubMeshUI AddSubTextObject(TextMeshProUGUI textComponent, MaterialReference materialReference)
		{
			GameObject gameObject = new GameObject("TMP UI SubObject [" + materialReference.material.name + "]", new Type[]
			{
				typeof(RectTransform)
			});
			gameObject.hideFlags = HideFlags.DontSave;
			gameObject.transform.SetParent(textComponent.transform, false);
			gameObject.transform.SetAsFirstSibling();
			gameObject.layer = textComponent.gameObject.layer;
			RectTransform component = gameObject.GetComponent<RectTransform>();
			component.anchorMin = Vector2.zero;
			component.anchorMax = Vector2.one;
			component.sizeDelta = Vector2.zero;
			component.pivot = textComponent.rectTransform.pivot;
			gameObject.AddComponent<LayoutElement>().ignoreLayout = true;
			TMP_SubMeshUI tmp_SubMeshUI = gameObject.AddComponent<TMP_SubMeshUI>();
			tmp_SubMeshUI.m_TextComponent = textComponent;
			tmp_SubMeshUI.m_materialReferenceIndex = materialReference.index;
			tmp_SubMeshUI.m_fontAsset = materialReference.fontAsset;
			tmp_SubMeshUI.m_spriteAsset = materialReference.spriteAsset;
			tmp_SubMeshUI.m_isDefaultMaterial = materialReference.isDefaultMaterial;
			tmp_SubMeshUI.SetSharedMaterial(materialReference.material);
			return tmp_SubMeshUI;
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x00027430 File Offset: 0x00025630
		protected override void OnEnable()
		{
			if (!this.m_isRegisteredForEvents)
			{
				this.m_isRegisteredForEvents = true;
			}
			if (base.hideFlags != HideFlags.DontSave)
			{
				base.hideFlags = HideFlags.DontSave;
			}
			this.m_ShouldRecalculateStencil = true;
			this.RecalculateClipping();
			this.RecalculateMasking();
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x00027466 File Offset: 0x00025666
		protected override void OnDisable()
		{
			base.OnDisable();
			if (this.m_fallbackMaterial != null)
			{
				TMP_MaterialManager.ReleaseFallbackMaterial(this.m_fallbackMaterial);
				this.m_fallbackMaterial = null;
			}
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x00027490 File Offset: 0x00025690
		protected override void OnDestroy()
		{
			if (this.m_mesh != null)
			{
				UnityEngine.Object.DestroyImmediate(this.m_mesh);
			}
			if (this.m_MaskMaterial != null)
			{
				TMP_MaterialManager.ReleaseStencilMaterial(this.m_MaskMaterial);
			}
			if (this.m_fallbackMaterial != null)
			{
				TMP_MaterialManager.ReleaseFallbackMaterial(this.m_fallbackMaterial);
				this.m_fallbackMaterial = null;
			}
			this.m_isRegisteredForEvents = false;
			this.RecalculateClipping();
			if (this.m_TextComponent != null)
			{
				this.m_TextComponent.havePropertiesChanged = true;
				this.m_TextComponent.SetAllDirty();
			}
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x00027521 File Offset: 0x00025721
		protected override void OnTransformParentChanged()
		{
			if (!this.IsActive())
			{
				return;
			}
			this.m_ShouldRecalculateStencil = true;
			this.RecalculateClipping();
			this.RecalculateMasking();
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x00027540 File Offset: 0x00025740
		public override Material GetModifiedMaterial(Material baseMaterial)
		{
			Material material = baseMaterial;
			if (this.m_ShouldRecalculateStencil)
			{
				Transform stopAfter = MaskUtilities.FindRootSortOverrideCanvas(base.transform);
				this.m_StencilValue = (base.maskable ? MaskUtilities.GetStencilDepth(base.transform, stopAfter) : 0);
				this.m_ShouldRecalculateStencil = false;
			}
			if (this.m_StencilValue > 0)
			{
				Material maskMaterial = StencilMaterial.Add(material, (1 << this.m_StencilValue) - 1, StencilOp.Keep, CompareFunction.Equal, ColorWriteMask.All, (1 << this.m_StencilValue) - 1, 0);
				StencilMaterial.Remove(this.m_MaskMaterial);
				this.m_MaskMaterial = maskMaterial;
				material = this.m_MaskMaterial;
			}
			return material;
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x000275D0 File Offset: 0x000257D0
		public float GetPaddingForMaterial()
		{
			return ShaderUtilities.GetPadding(this.m_sharedMaterial, this.m_TextComponent.extraPadding, this.m_TextComponent.isUsingBold);
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x000275F3 File Offset: 0x000257F3
		public float GetPaddingForMaterial(Material mat)
		{
			return ShaderUtilities.GetPadding(mat, this.m_TextComponent.extraPadding, this.m_TextComponent.isUsingBold);
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x00027611 File Offset: 0x00025811
		public void UpdateMeshPadding(bool isExtraPadding, bool isUsingBold)
		{
			this.m_padding = ShaderUtilities.GetPadding(this.m_sharedMaterial, isExtraPadding, isUsingBold);
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x00027626 File Offset: 0x00025826
		public override void SetAllDirty()
		{
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x00027628 File Offset: 0x00025828
		public override void SetVerticesDirty()
		{
			if (!this.IsActive())
			{
				return;
			}
			if (this.m_TextComponent != null)
			{
				this.m_TextComponent.havePropertiesChanged = true;
				this.m_TextComponent.SetVerticesDirty();
			}
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x00027658 File Offset: 0x00025858
		public override void SetLayoutDirty()
		{
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x0002765A File Offset: 0x0002585A
		public override void SetMaterialDirty()
		{
			this.m_materialDirty = true;
			this.UpdateMaterial();
			if (this.m_OnDirtyMaterialCallback != null)
			{
				this.m_OnDirtyMaterialCallback();
			}
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x0002767C File Offset: 0x0002587C
		public void SetPivotDirty()
		{
			if (!this.IsActive())
			{
				return;
			}
			base.rectTransform.pivot = this.m_TextComponent.rectTransform.pivot;
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x000276A2 File Offset: 0x000258A2
		private Transform GetRootCanvasTransform()
		{
			if (this.m_RootCanvasTransform == null)
			{
				this.m_RootCanvasTransform = this.m_TextComponent.canvas.rootCanvas.transform;
			}
			return this.m_RootCanvasTransform;
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x000276D3 File Offset: 0x000258D3
		public override void Cull(Rect clipRect, bool validRect)
		{
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x000276D5 File Offset: 0x000258D5
		protected override void UpdateGeometry()
		{
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x000276D7 File Offset: 0x000258D7
		public override void Rebuild(CanvasUpdate update)
		{
			if (update == CanvasUpdate.PreRender)
			{
				if (!this.m_materialDirty)
				{
					return;
				}
				this.UpdateMaterial();
				this.m_materialDirty = false;
			}
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x000276F3 File Offset: 0x000258F3
		public void RefreshMaterial()
		{
			this.UpdateMaterial();
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x000276FC File Offset: 0x000258FC
		protected override void UpdateMaterial()
		{
			if (this.m_sharedMaterial == null)
			{
				return;
			}
			if (this.m_sharedMaterial.HasProperty(ShaderUtilities.ShaderTag_CullMode))
			{
				float @float = this.textComponent.fontSharedMaterial.GetFloat(ShaderUtilities.ShaderTag_CullMode);
				this.m_sharedMaterial.SetFloat(ShaderUtilities.ShaderTag_CullMode, @float);
			}
			base.canvasRenderer.materialCount = 1;
			base.canvasRenderer.SetMaterial(this.materialForRendering, 0);
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x0002776F File Offset: 0x0002596F
		public override void RecalculateClipping()
		{
			base.RecalculateClipping();
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x00027777 File Offset: 0x00025977
		private Material GetMaterial()
		{
			return this.m_sharedMaterial;
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x00027780 File Offset: 0x00025980
		private Material GetMaterial(Material mat)
		{
			if (this.m_material == null || this.m_material.GetInstanceID() != mat.GetInstanceID())
			{
				this.m_material = this.CreateMaterialInstance(mat);
			}
			this.m_sharedMaterial = this.m_material;
			this.m_padding = this.GetPaddingForMaterial();
			this.SetVerticesDirty();
			this.SetMaterialDirty();
			return this.m_sharedMaterial;
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x000277E5 File Offset: 0x000259E5
		private Material CreateMaterialInstance(Material source)
		{
			Material material = new Material(source);
			material.shaderKeywords = source.shaderKeywords;
			material.name += " (Instance)";
			return material;
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x0002780F File Offset: 0x00025A0F
		private Material GetSharedMaterial()
		{
			return base.canvasRenderer.GetMaterial();
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x0002781C File Offset: 0x00025A1C
		private void SetSharedMaterial(Material mat)
		{
			this.m_sharedMaterial = mat;
			this.m_Material = this.m_sharedMaterial;
			this.m_padding = this.GetPaddingForMaterial();
			this.SetMaterialDirty();
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x00027843 File Offset: 0x00025A43
		public TMP_SubMeshUI()
		{
		}

		// Token: 0x040003C2 RID: 962
		[SerializeField]
		private TMP_FontAsset m_fontAsset;

		// Token: 0x040003C3 RID: 963
		[SerializeField]
		private TMP_SpriteAsset m_spriteAsset;

		// Token: 0x040003C4 RID: 964
		[SerializeField]
		private Material m_material;

		// Token: 0x040003C5 RID: 965
		[SerializeField]
		private Material m_sharedMaterial;

		// Token: 0x040003C6 RID: 966
		private Material m_fallbackMaterial;

		// Token: 0x040003C7 RID: 967
		private Material m_fallbackSourceMaterial;

		// Token: 0x040003C8 RID: 968
		[SerializeField]
		private bool m_isDefaultMaterial;

		// Token: 0x040003C9 RID: 969
		[SerializeField]
		private float m_padding;

		// Token: 0x040003CA RID: 970
		private Mesh m_mesh;

		// Token: 0x040003CB RID: 971
		[SerializeField]
		private TextMeshProUGUI m_TextComponent;

		// Token: 0x040003CC RID: 972
		[NonSerialized]
		private bool m_isRegisteredForEvents;

		// Token: 0x040003CD RID: 973
		private bool m_materialDirty;

		// Token: 0x040003CE RID: 974
		[SerializeField]
		private int m_materialReferenceIndex;

		// Token: 0x040003CF RID: 975
		private Transform m_RootCanvasTransform;
	}
}
