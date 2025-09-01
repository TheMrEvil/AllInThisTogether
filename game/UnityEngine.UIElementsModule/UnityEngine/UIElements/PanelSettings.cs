using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.UIElements.UIR;

namespace UnityEngine.UIElements
{
	// Token: 0x0200024B RID: 587
	[HelpURL("UIE-Runtime-Panel-Settings")]
	public class PanelSettings : ScriptableObject
	{
		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x060011AC RID: 4524 RVA: 0x00045D24 File Offset: 0x00043F24
		// (set) Token: 0x060011AD RID: 4525 RVA: 0x00045D3C File Offset: 0x00043F3C
		public ThemeStyleSheet themeStyleSheet
		{
			get
			{
				return this.themeUss;
			}
			set
			{
				this.themeUss = value;
				this.ApplyThemeStyleSheet(null);
			}
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x060011AE RID: 4526 RVA: 0x00045D4E File Offset: 0x00043F4E
		// (set) Token: 0x060011AF RID: 4527 RVA: 0x00045D56 File Offset: 0x00043F56
		public RenderTexture targetTexture
		{
			get
			{
				return this.m_TargetTexture;
			}
			set
			{
				this.m_TargetTexture = value;
				this.m_PanelAccess.SetTargetTexture();
			}
		}

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x060011B0 RID: 4528 RVA: 0x00045D6C File Offset: 0x00043F6C
		// (set) Token: 0x060011B1 RID: 4529 RVA: 0x00045D74 File Offset: 0x00043F74
		public PanelScaleMode scaleMode
		{
			get
			{
				return this.m_ScaleMode;
			}
			set
			{
				this.m_ScaleMode = value;
			}
		}

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x060011B2 RID: 4530 RVA: 0x00045D7D File Offset: 0x00043F7D
		// (set) Token: 0x060011B3 RID: 4531 RVA: 0x00045D85 File Offset: 0x00043F85
		public float scale
		{
			get
			{
				return this.m_Scale;
			}
			set
			{
				this.m_Scale = value;
			}
		}

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x060011B4 RID: 4532 RVA: 0x00045D8E File Offset: 0x00043F8E
		// (set) Token: 0x060011B5 RID: 4533 RVA: 0x00045D96 File Offset: 0x00043F96
		public float referenceDpi
		{
			get
			{
				return this.m_ReferenceDpi;
			}
			set
			{
				this.m_ReferenceDpi = ((value >= 1f) ? value : 96f);
			}
		}

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x060011B6 RID: 4534 RVA: 0x00045DAE File Offset: 0x00043FAE
		// (set) Token: 0x060011B7 RID: 4535 RVA: 0x00045DB6 File Offset: 0x00043FB6
		public float fallbackDpi
		{
			get
			{
				return this.m_FallbackDpi;
			}
			set
			{
				this.m_FallbackDpi = ((value >= 1f) ? value : 96f);
			}
		}

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x060011B8 RID: 4536 RVA: 0x00045DCE File Offset: 0x00043FCE
		// (set) Token: 0x060011B9 RID: 4537 RVA: 0x00045DD6 File Offset: 0x00043FD6
		public Vector2Int referenceResolution
		{
			get
			{
				return this.m_ReferenceResolution;
			}
			set
			{
				this.m_ReferenceResolution = value;
			}
		}

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x060011BA RID: 4538 RVA: 0x00045DDF File Offset: 0x00043FDF
		// (set) Token: 0x060011BB RID: 4539 RVA: 0x00045DE7 File Offset: 0x00043FE7
		public PanelScreenMatchMode screenMatchMode
		{
			get
			{
				return this.m_ScreenMatchMode;
			}
			set
			{
				this.m_ScreenMatchMode = value;
			}
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x060011BC RID: 4540 RVA: 0x00045DF0 File Offset: 0x00043FF0
		// (set) Token: 0x060011BD RID: 4541 RVA: 0x00045DF8 File Offset: 0x00043FF8
		public float match
		{
			get
			{
				return this.m_Match;
			}
			set
			{
				this.m_Match = value;
			}
		}

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x060011BE RID: 4542 RVA: 0x00045E01 File Offset: 0x00044001
		// (set) Token: 0x060011BF RID: 4543 RVA: 0x00045E09 File Offset: 0x00044009
		public float sortingOrder
		{
			get
			{
				return this.m_SortingOrder;
			}
			set
			{
				this.m_SortingOrder = value;
				this.ApplySortingOrder();
			}
		}

		// Token: 0x060011C0 RID: 4544 RVA: 0x00045E1A File Offset: 0x0004401A
		internal void ApplySortingOrder()
		{
			this.m_PanelAccess.SetSortingPriority();
		}

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x060011C1 RID: 4545 RVA: 0x00045E29 File Offset: 0x00044029
		// (set) Token: 0x060011C2 RID: 4546 RVA: 0x00045E31 File Offset: 0x00044031
		public int targetDisplay
		{
			get
			{
				return this.m_TargetDisplay;
			}
			set
			{
				this.m_TargetDisplay = value;
				this.m_PanelAccess.SetTargetDisplay();
			}
		}

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x060011C3 RID: 4547 RVA: 0x00045E47 File Offset: 0x00044047
		// (set) Token: 0x060011C4 RID: 4548 RVA: 0x00045E4F File Offset: 0x0004404F
		public bool clearDepthStencil
		{
			get
			{
				return this.m_ClearDepthStencil;
			}
			set
			{
				this.m_ClearDepthStencil = value;
			}
		}

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x060011C5 RID: 4549 RVA: 0x00045E58 File Offset: 0x00044058
		public float depthClearValue
		{
			get
			{
				return 0.99f;
			}
		}

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x060011C6 RID: 4550 RVA: 0x00045E5F File Offset: 0x0004405F
		// (set) Token: 0x060011C7 RID: 4551 RVA: 0x00045E67 File Offset: 0x00044067
		public bool clearColor
		{
			get
			{
				return this.m_ClearColor;
			}
			set
			{
				this.m_ClearColor = value;
			}
		}

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x060011C8 RID: 4552 RVA: 0x00045E70 File Offset: 0x00044070
		// (set) Token: 0x060011C9 RID: 4553 RVA: 0x00045E78 File Offset: 0x00044078
		public Color colorClearValue
		{
			get
			{
				return this.m_ColorClearValue;
			}
			set
			{
				this.m_ColorClearValue = value;
			}
		}

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x060011CA RID: 4554 RVA: 0x00045E81 File Offset: 0x00044081
		internal BaseRuntimePanel panel
		{
			get
			{
				return this.m_PanelAccess.panel;
			}
		}

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x060011CB RID: 4555 RVA: 0x00045E8E File Offset: 0x0004408E
		internal VisualElement visualTree
		{
			get
			{
				return this.m_PanelAccess.panel.visualTree;
			}
		}

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x060011CC RID: 4556 RVA: 0x00045EA0 File Offset: 0x000440A0
		// (set) Token: 0x060011CD RID: 4557 RVA: 0x00045EA8 File Offset: 0x000440A8
		public DynamicAtlasSettings dynamicAtlasSettings
		{
			get
			{
				return this.m_DynamicAtlasSettings;
			}
			set
			{
				this.m_DynamicAtlasSettings = value;
			}
		}

		// Token: 0x060011CE RID: 4558 RVA: 0x00045EB4 File Offset: 0x000440B4
		private PanelSettings()
		{
			this.m_PanelAccess = new PanelSettings.RuntimePanelAccess(this);
		}

		// Token: 0x060011CF RID: 4559 RVA: 0x00002166 File Offset: 0x00000366
		private void Reset()
		{
		}

		// Token: 0x060011D0 RID: 4560 RVA: 0x00045F5C File Offset: 0x0004415C
		private void OnEnable()
		{
			bool flag = this.themeUss == null;
			if (flag)
			{
				Debug.LogWarning("No Theme Style Sheet set to PanelSettings " + base.name + ", UI will not render properly", this);
			}
			this.UpdateScreenDPI();
			this.InitializeShaders();
		}

		// Token: 0x060011D1 RID: 4561 RVA: 0x00045FA8 File Offset: 0x000441A8
		private void OnDisable()
		{
			this.m_PanelAccess.DisposePanel();
		}

		// Token: 0x060011D2 RID: 4562 RVA: 0x00045FA8 File Offset: 0x000441A8
		internal void DisposePanel()
		{
			this.m_PanelAccess.DisposePanel();
		}

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x060011D3 RID: 4563 RVA: 0x00045FB7 File Offset: 0x000441B7
		// (set) Token: 0x060011D4 RID: 4564 RVA: 0x00045FBF File Offset: 0x000441BF
		private float ScreenDPI
		{
			[CompilerGenerated]
			get
			{
				return this.<ScreenDPI>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ScreenDPI>k__BackingField = value;
			}
		}

		// Token: 0x060011D5 RID: 4565 RVA: 0x00045FC8 File Offset: 0x000441C8
		internal void UpdateScreenDPI()
		{
			this.ScreenDPI = Screen.dpi;
		}

		// Token: 0x060011D6 RID: 4566 RVA: 0x00045FD8 File Offset: 0x000441D8
		private void ApplyThemeStyleSheet(VisualElement root = null)
		{
			bool flag = !this.m_PanelAccess.isInitialized;
			if (!flag)
			{
				bool flag2 = root == null;
				if (flag2)
				{
					root = this.visualTree;
				}
				bool flag3 = this.m_OldThemeUss != this.themeUss && this.m_OldThemeUss != null;
				if (flag3)
				{
					if (root != null)
					{
						root.styleSheets.Remove(this.m_OldThemeUss);
					}
				}
				bool flag4 = this.themeUss != null;
				if (flag4)
				{
					this.themeUss.isDefaultStyleSheet = true;
					if (root != null)
					{
						root.styleSheets.Add(this.themeUss);
					}
				}
				this.m_OldThemeUss = this.themeUss;
			}
		}

		// Token: 0x060011D7 RID: 4567 RVA: 0x0004609C File Offset: 0x0004429C
		private void InitializeShaders()
		{
			bool flag = this.m_AtlasBlitShader == null;
			if (flag)
			{
				this.m_AtlasBlitShader = Shader.Find(Shaders.k_AtlasBlit);
			}
			bool flag2 = this.m_RuntimeShader == null;
			if (flag2)
			{
				this.m_RuntimeShader = Shader.Find(Shaders.k_Runtime);
			}
			bool flag3 = this.m_RuntimeWorldShader == null;
			if (flag3)
			{
				this.m_RuntimeWorldShader = Shader.Find(Shaders.k_RuntimeWorld);
			}
			this.m_PanelAccess.SetTargetTexture();
		}

		// Token: 0x060011D8 RID: 4568 RVA: 0x0004611C File Offset: 0x0004431C
		internal void ApplyPanelSettings()
		{
			Rect targetRect = this.m_TargetRect;
			float resolvedScale = this.m_ResolvedScale;
			this.UpdateScreenDPI();
			this.m_TargetRect = this.GetDisplayRect();
			this.m_ResolvedScale = this.ResolveScale(this.m_TargetRect, this.ScreenDPI);
			bool flag = this.visualTree.style.width.value == 0f || this.m_ResolvedScale != resolvedScale || this.m_TargetRect.width != targetRect.width || this.m_TargetRect.height != targetRect.height;
			if (flag)
			{
				this.panel.scale = ((this.m_ResolvedScale == 0f) ? 0f : (1f / this.m_ResolvedScale));
				this.visualTree.style.left = 0f;
				this.visualTree.style.top = 0f;
				this.visualTree.style.width = this.m_TargetRect.width * this.m_ResolvedScale;
				this.visualTree.style.height = this.m_TargetRect.height * this.m_ResolvedScale;
			}
			this.panel.targetTexture = this.targetTexture;
			this.panel.targetDisplay = this.targetDisplay;
			this.panel.drawToCameras = false;
			this.panel.clearSettings = new PanelClearSettings
			{
				clearColor = this.m_ClearColor,
				clearDepthStencil = this.m_ClearDepthStencil,
				color = this.m_ColorClearValue
			};
			DynamicAtlas dynamicAtlas = this.panel.atlas as DynamicAtlas;
			bool flag2 = dynamicAtlas != null;
			if (flag2)
			{
				dynamicAtlas.minAtlasSize = this.dynamicAtlasSettings.minAtlasSize;
				dynamicAtlas.maxAtlasSize = this.dynamicAtlasSettings.maxAtlasSize;
				dynamicAtlas.maxSubTextureSize = this.dynamicAtlasSettings.maxSubTextureSize;
				dynamicAtlas.activeFilters = this.dynamicAtlasSettings.activeFilters;
				dynamicAtlas.customFilter = this.dynamicAtlasSettings.customFilter;
			}
		}

		// Token: 0x060011D9 RID: 4569 RVA: 0x00046363 File Offset: 0x00044563
		public void SetScreenToPanelSpaceFunction(Func<Vector2, Vector2> screentoPanelSpaceFunction)
		{
			this.m_AssignedScreenToPanel = screentoPanelSpaceFunction;
			this.panel.screenToPanelSpace = this.m_AssignedScreenToPanel;
		}

		// Token: 0x060011DA RID: 4570 RVA: 0x00046380 File Offset: 0x00044580
		internal float ResolveScale(Rect targetRect, float screenDpi)
		{
			float num = 1f;
			switch (this.scaleMode)
			{
			case PanelScaleMode.ConstantPhysicalSize:
			{
				float num2 = (screenDpi == 0f) ? this.fallbackDpi : screenDpi;
				bool flag = num2 != 0f;
				if (flag)
				{
					num = this.referenceDpi / num2;
				}
				break;
			}
			case PanelScaleMode.ScaleWithScreenSize:
			{
				bool flag2 = this.referenceResolution.x * this.referenceResolution.y != 0;
				if (flag2)
				{
					Vector2 vector = this.referenceResolution;
					Vector2 vector2 = new Vector2(targetRect.width / vector.x, targetRect.height / vector.y);
					PanelScreenMatchMode screenMatchMode = this.screenMatchMode;
					PanelScreenMatchMode panelScreenMatchMode = screenMatchMode;
					float num3;
					if (panelScreenMatchMode != PanelScreenMatchMode.Shrink)
					{
						if (panelScreenMatchMode != PanelScreenMatchMode.Expand)
						{
							float t = Mathf.Clamp01(this.match);
							num3 = Mathf.Lerp(vector2.x, vector2.y, t);
						}
						else
						{
							num3 = Mathf.Min(vector2.x, vector2.y);
						}
					}
					else
					{
						num3 = Mathf.Max(vector2.x, vector2.y);
					}
					bool flag3 = num3 != 0f;
					if (flag3)
					{
						num = 1f / num3;
					}
				}
				break;
			}
			}
			bool flag4 = this.scale > 0f;
			if (flag4)
			{
				num /= this.scale;
			}
			else
			{
				num = 0f;
			}
			return num;
		}

		// Token: 0x060011DB RID: 4571 RVA: 0x0004650C File Offset: 0x0004470C
		internal Rect GetDisplayRect()
		{
			bool flag = this.m_TargetTexture != null;
			Rect result;
			if (flag)
			{
				result = new Rect(0f, 0f, (float)this.m_TargetTexture.width, (float)this.m_TargetTexture.height);
			}
			else
			{
				bool flag2 = this.targetDisplay > 0 && this.targetDisplay < Display.displays.Length;
				if (flag2)
				{
					result = new Rect(0f, 0f, (float)Display.displays[this.targetDisplay].renderingWidth, (float)Display.displays[this.targetDisplay].renderingHeight);
				}
				else
				{
					result = new Rect(0f, 0f, (float)Screen.width, (float)Screen.height);
				}
			}
			return result;
		}

		// Token: 0x060011DC RID: 4572 RVA: 0x000465CC File Offset: 0x000447CC
		internal void AttachAndInsertUIDocumentToVisualTree(UIDocument uiDocument)
		{
			bool flag = this.m_AttachedUIDocumentsList == null;
			if (flag)
			{
				this.m_AttachedUIDocumentsList = new UIDocumentList();
			}
			else
			{
				this.m_AttachedUIDocumentsList.RemoveFromListAndFromVisualTree(uiDocument);
			}
			this.m_AttachedUIDocumentsList.AddToListAndToVisualTree(uiDocument, this.visualTree, 0);
		}

		// Token: 0x060011DD RID: 4573 RVA: 0x0004661C File Offset: 0x0004481C
		internal void DetachUIDocument(UIDocument uiDocument)
		{
			bool flag = this.m_AttachedUIDocumentsList == null;
			if (!flag)
			{
				this.m_AttachedUIDocumentsList.RemoveFromListAndFromVisualTree(uiDocument);
				bool flag2 = this.m_AttachedUIDocumentsList.m_AttachedUIDocuments.Count == 0;
				if (flag2)
				{
					this.m_PanelAccess.MarkPotentiallyEmpty();
				}
			}
		}

		// Token: 0x040007E9 RID: 2025
		private const int k_DefaultSortingOrder = 0;

		// Token: 0x040007EA RID: 2026
		private const float k_DefaultScaleValue = 1f;

		// Token: 0x040007EB RID: 2027
		internal const string k_DefaultStyleSheetPath = "Packages/com.unity.ui/PackageResources/StyleSheets/Generated/Default.tss.asset";

		// Token: 0x040007EC RID: 2028
		[SerializeField]
		private ThemeStyleSheet themeUss;

		// Token: 0x040007ED RID: 2029
		[SerializeField]
		private RenderTexture m_TargetTexture;

		// Token: 0x040007EE RID: 2030
		[SerializeField]
		private PanelScaleMode m_ScaleMode = PanelScaleMode.ConstantPhysicalSize;

		// Token: 0x040007EF RID: 2031
		[SerializeField]
		private float m_Scale = 1f;

		// Token: 0x040007F0 RID: 2032
		private const float DefaultDpi = 96f;

		// Token: 0x040007F1 RID: 2033
		[SerializeField]
		private float m_ReferenceDpi = 96f;

		// Token: 0x040007F2 RID: 2034
		[SerializeField]
		private float m_FallbackDpi = 96f;

		// Token: 0x040007F3 RID: 2035
		[SerializeField]
		private Vector2Int m_ReferenceResolution = new Vector2Int(1200, 800);

		// Token: 0x040007F4 RID: 2036
		[SerializeField]
		private PanelScreenMatchMode m_ScreenMatchMode = PanelScreenMatchMode.MatchWidthOrHeight;

		// Token: 0x040007F5 RID: 2037
		[SerializeField]
		[Range(0f, 1f)]
		private float m_Match = 0f;

		// Token: 0x040007F6 RID: 2038
		[SerializeField]
		private float m_SortingOrder = 0f;

		// Token: 0x040007F7 RID: 2039
		[SerializeField]
		private int m_TargetDisplay = 0;

		// Token: 0x040007F8 RID: 2040
		[SerializeField]
		private bool m_ClearDepthStencil = true;

		// Token: 0x040007F9 RID: 2041
		[SerializeField]
		private bool m_ClearColor;

		// Token: 0x040007FA RID: 2042
		[SerializeField]
		private Color m_ColorClearValue = Color.clear;

		// Token: 0x040007FB RID: 2043
		private PanelSettings.RuntimePanelAccess m_PanelAccess;

		// Token: 0x040007FC RID: 2044
		internal UIDocumentList m_AttachedUIDocumentsList;

		// Token: 0x040007FD RID: 2045
		[HideInInspector]
		[SerializeField]
		private DynamicAtlasSettings m_DynamicAtlasSettings = DynamicAtlasSettings.defaults;

		// Token: 0x040007FE RID: 2046
		[SerializeField]
		[HideInInspector]
		private Shader m_AtlasBlitShader;

		// Token: 0x040007FF RID: 2047
		[SerializeField]
		[HideInInspector]
		private Shader m_RuntimeShader;

		// Token: 0x04000800 RID: 2048
		[SerializeField]
		[HideInInspector]
		private Shader m_RuntimeWorldShader;

		// Token: 0x04000801 RID: 2049
		[SerializeField]
		public PanelTextSettings textSettings;

		// Token: 0x04000802 RID: 2050
		private Rect m_TargetRect;

		// Token: 0x04000803 RID: 2051
		private float m_ResolvedScale;

		// Token: 0x04000804 RID: 2052
		private StyleSheet m_OldThemeUss;

		// Token: 0x04000805 RID: 2053
		internal int m_EmptyPanelCounter = 0;

		// Token: 0x04000806 RID: 2054
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private float <ScreenDPI>k__BackingField;

		// Token: 0x04000807 RID: 2055
		private Func<Vector2, Vector2> m_AssignedScreenToPanel;

		// Token: 0x0200024C RID: 588
		private class RuntimePanelAccess
		{
			// Token: 0x060011DE RID: 4574 RVA: 0x0004666A File Offset: 0x0004486A
			internal RuntimePanelAccess(PanelSettings settings)
			{
				this.m_Settings = settings;
			}

			// Token: 0x17000402 RID: 1026
			// (get) Token: 0x060011DF RID: 4575 RVA: 0x0004667B File Offset: 0x0004487B
			internal bool isInitialized
			{
				get
				{
					return this.m_RuntimePanel != null;
				}
			}

			// Token: 0x17000403 RID: 1027
			// (get) Token: 0x060011E0 RID: 4576 RVA: 0x00046688 File Offset: 0x00044888
			internal BaseRuntimePanel panel
			{
				get
				{
					bool flag = this.m_RuntimePanel == null;
					if (flag)
					{
						this.m_RuntimePanel = this.CreateRelatedRuntimePanel();
						this.m_RuntimePanel.sortingPriority = this.m_Settings.m_SortingOrder;
						this.m_RuntimePanel.targetDisplay = this.m_Settings.m_TargetDisplay;
						VisualElement visualTree = this.m_RuntimePanel.visualTree;
						visualTree.name = this.m_Settings.name;
						this.m_Settings.ApplyThemeStyleSheet(visualTree);
						bool flag2 = this.m_Settings.m_TargetTexture != null;
						if (flag2)
						{
							this.m_RuntimePanel.targetTexture = this.m_Settings.m_TargetTexture;
						}
						bool flag3 = this.m_Settings.m_AssignedScreenToPanel != null;
						if (flag3)
						{
							this.m_Settings.SetScreenToPanelSpaceFunction(this.m_Settings.m_AssignedScreenToPanel);
						}
					}
					return this.m_RuntimePanel;
				}
			}

			// Token: 0x060011E1 RID: 4577 RVA: 0x00046774 File Offset: 0x00044974
			internal void DisposePanel()
			{
				bool flag = this.m_RuntimePanel != null;
				if (flag)
				{
					this.DisposeRelatedPanel();
					this.m_RuntimePanel = null;
				}
			}

			// Token: 0x060011E2 RID: 4578 RVA: 0x000467A0 File Offset: 0x000449A0
			internal void SetTargetTexture()
			{
				bool flag = this.m_RuntimePanel != null;
				if (flag)
				{
					this.m_RuntimePanel.targetTexture = this.m_Settings.targetTexture;
				}
			}

			// Token: 0x060011E3 RID: 4579 RVA: 0x000467D4 File Offset: 0x000449D4
			internal void SetSortingPriority()
			{
				bool flag = this.m_RuntimePanel != null;
				if (flag)
				{
					this.m_RuntimePanel.sortingPriority = this.m_Settings.m_SortingOrder;
				}
			}

			// Token: 0x060011E4 RID: 4580 RVA: 0x00046808 File Offset: 0x00044A08
			internal void SetTargetDisplay()
			{
				bool flag = this.m_RuntimePanel != null;
				if (flag)
				{
					this.m_RuntimePanel.targetDisplay = this.m_Settings.m_TargetDisplay;
				}
			}

			// Token: 0x060011E5 RID: 4581 RVA: 0x0004683C File Offset: 0x00044A3C
			private BaseRuntimePanel CreateRelatedRuntimePanel()
			{
				return (RuntimePanel)UIElementsRuntimeUtility.FindOrCreateRuntimePanel(this.m_Settings, new UIElementsRuntimeUtility.CreateRuntimePanelDelegate(RuntimePanel.Create));
			}

			// Token: 0x060011E6 RID: 4582 RVA: 0x0004686C File Offset: 0x00044A6C
			private void DisposeRelatedPanel()
			{
				UIElementsRuntimeUtility.DisposeRuntimePanel(this.m_Settings);
			}

			// Token: 0x060011E7 RID: 4583 RVA: 0x0004687B File Offset: 0x00044A7B
			internal void MarkPotentiallyEmpty()
			{
				UIElementsRuntimeUtility.MarkPotentiallyEmpty(this.m_Settings);
			}

			// Token: 0x04000808 RID: 2056
			private readonly PanelSettings m_Settings;

			// Token: 0x04000809 RID: 2057
			private BaseRuntimePanel m_RuntimePanel;
		}
	}
}
