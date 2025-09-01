using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Pool;
using UnityEngine.Serialization;
using UnityEngine.UI.CoroutineTween;

namespace UnityEngine.UI
{
	// Token: 0x02000011 RID: 17
	[DisallowMultipleComponent]
	[RequireComponent(typeof(RectTransform))]
	[ExecuteAlways]
	public abstract class Graphic : UIBehaviour, ICanvasElement
	{
		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x00005335 File Offset: 0x00003535
		public static Material defaultGraphicMaterial
		{
			get
			{
				if (Graphic.s_DefaultUI == null)
				{
					Graphic.s_DefaultUI = Canvas.GetDefaultCanvasMaterial();
				}
				return Graphic.s_DefaultUI;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x00005353 File Offset: 0x00003553
		// (set) Token: 0x060000B9 RID: 185 RVA: 0x0000535B File Offset: 0x0000355B
		public virtual Color color
		{
			get
			{
				return this.m_Color;
			}
			set
			{
				if (SetPropertyUtility.SetColor(ref this.m_Color, value))
				{
					this.SetVerticesDirty();
				}
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000BA RID: 186 RVA: 0x00005371 File Offset: 0x00003571
		// (set) Token: 0x060000BB RID: 187 RVA: 0x0000537C File Offset: 0x0000357C
		public virtual bool raycastTarget
		{
			get
			{
				return this.m_RaycastTarget;
			}
			set
			{
				if (value != this.m_RaycastTarget)
				{
					if (this.m_RaycastTarget)
					{
						GraphicRegistry.UnregisterRaycastGraphicForCanvas(this.canvas, this);
					}
					this.m_RaycastTarget = value;
					if (this.m_RaycastTarget && base.isActiveAndEnabled)
					{
						GraphicRegistry.RegisterRaycastGraphicForCanvas(this.canvas, this);
					}
				}
				this.m_RaycastTargetCache = value;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000BC RID: 188 RVA: 0x000053D0 File Offset: 0x000035D0
		// (set) Token: 0x060000BD RID: 189 RVA: 0x000053D8 File Offset: 0x000035D8
		public Vector4 raycastPadding
		{
			get
			{
				return this.m_RaycastPadding;
			}
			set
			{
				this.m_RaycastPadding = value;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000BE RID: 190 RVA: 0x000053E1 File Offset: 0x000035E1
		// (set) Token: 0x060000BF RID: 191 RVA: 0x000053E9 File Offset: 0x000035E9
		protected bool useLegacyMeshGeneration
		{
			[CompilerGenerated]
			get
			{
				return this.<useLegacyMeshGeneration>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<useLegacyMeshGeneration>k__BackingField = value;
			}
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x000053F4 File Offset: 0x000035F4
		protected Graphic()
		{
			if (this.m_ColorTweenRunner == null)
			{
				this.m_ColorTweenRunner = new TweenRunner<ColorTween>();
			}
			this.m_ColorTweenRunner.Init(this);
			this.useLegacyMeshGeneration = true;
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00005446 File Offset: 0x00003646
		public virtual void SetAllDirty()
		{
			if (this.m_SkipLayoutUpdate)
			{
				this.m_SkipLayoutUpdate = false;
			}
			else
			{
				this.SetLayoutDirty();
			}
			if (this.m_SkipMaterialUpdate)
			{
				this.m_SkipMaterialUpdate = false;
			}
			else
			{
				this.SetMaterialDirty();
			}
			this.SetVerticesDirty();
			this.SetRaycastDirty();
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00005482 File Offset: 0x00003682
		public virtual void SetLayoutDirty()
		{
			if (!this.IsActive())
			{
				return;
			}
			LayoutRebuilder.MarkLayoutForRebuild(this.rectTransform);
			if (this.m_OnDirtyLayoutCallback != null)
			{
				this.m_OnDirtyLayoutCallback();
			}
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x000054AB File Offset: 0x000036AB
		public virtual void SetVerticesDirty()
		{
			if (!this.IsActive())
			{
				return;
			}
			this.m_VertsDirty = true;
			CanvasUpdateRegistry.RegisterCanvasElementForGraphicRebuild(this);
			if (this.m_OnDirtyVertsCallback != null)
			{
				this.m_OnDirtyVertsCallback();
			}
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x000054D6 File Offset: 0x000036D6
		public virtual void SetMaterialDirty()
		{
			if (!this.IsActive())
			{
				return;
			}
			this.m_MaterialDirty = true;
			CanvasUpdateRegistry.RegisterCanvasElementForGraphicRebuild(this);
			if (this.m_OnDirtyMaterialCallback != null)
			{
				this.m_OnDirtyMaterialCallback();
			}
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00005504 File Offset: 0x00003704
		public void SetRaycastDirty()
		{
			if (this.m_RaycastTargetCache != this.m_RaycastTarget)
			{
				if (this.m_RaycastTarget && base.isActiveAndEnabled)
				{
					GraphicRegistry.RegisterRaycastGraphicForCanvas(this.canvas, this);
				}
				else if (!this.m_RaycastTarget)
				{
					GraphicRegistry.UnregisterRaycastGraphicForCanvas(this.canvas, this);
				}
			}
			this.m_RaycastTargetCache = this.m_RaycastTarget;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x0000555D File Offset: 0x0000375D
		protected override void OnRectTransformDimensionsChange()
		{
			if (base.gameObject.activeInHierarchy)
			{
				if (CanvasUpdateRegistry.IsRebuildingLayout())
				{
					this.SetVerticesDirty();
					return;
				}
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00005586 File Offset: 0x00003786
		protected override void OnBeforeTransformParentChanged()
		{
			GraphicRegistry.UnregisterGraphicForCanvas(this.canvas, this);
			LayoutRebuilder.MarkLayoutForRebuild(this.rectTransform);
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x0000559F File Offset: 0x0000379F
		protected override void OnTransformParentChanged()
		{
			base.OnTransformParentChanged();
			this.m_Canvas = null;
			if (!this.IsActive())
			{
				return;
			}
			this.CacheCanvas();
			GraphicRegistry.RegisterGraphicForCanvas(this.canvas, this);
			this.SetAllDirty();
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x000055CF File Offset: 0x000037CF
		public int depth
		{
			get
			{
				return this.canvasRenderer.absoluteDepth;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000CA RID: 202 RVA: 0x000055DC File Offset: 0x000037DC
		public RectTransform rectTransform
		{
			get
			{
				if (this.m_RectTransform == null)
				{
					this.m_RectTransform = base.GetComponent<RectTransform>();
				}
				return this.m_RectTransform;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000CB RID: 203 RVA: 0x000055F8 File Offset: 0x000037F8
		public Canvas canvas
		{
			get
			{
				if (this.m_Canvas == null)
				{
					this.CacheCanvas();
				}
				return this.m_Canvas;
			}
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00005614 File Offset: 0x00003814
		private void CacheCanvas()
		{
			List<Canvas> list = CollectionPool<List<Canvas>, Canvas>.Get();
			base.gameObject.GetComponentsInParent<Canvas>(false, list);
			if (list.Count > 0)
			{
				for (int i = 0; i < list.Count; i++)
				{
					if (list[i].isActiveAndEnabled)
					{
						this.m_Canvas = list[i];
						break;
					}
					if (i == list.Count - 1)
					{
						this.m_Canvas = null;
					}
				}
			}
			else
			{
				this.m_Canvas = null;
			}
			CollectionPool<List<Canvas>, Canvas>.Release(list);
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000CD RID: 205 RVA: 0x0000568C File Offset: 0x0000388C
		public CanvasRenderer canvasRenderer
		{
			get
			{
				if (this.m_CanvasRenderer == null)
				{
					this.m_CanvasRenderer = base.GetComponent<CanvasRenderer>();
					if (this.m_CanvasRenderer == null)
					{
						this.m_CanvasRenderer = base.gameObject.AddComponent<CanvasRenderer>();
					}
				}
				return this.m_CanvasRenderer;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000CE RID: 206 RVA: 0x000056C1 File Offset: 0x000038C1
		public virtual Material defaultMaterial
		{
			get
			{
				return Graphic.defaultGraphicMaterial;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000CF RID: 207 RVA: 0x000056C8 File Offset: 0x000038C8
		// (set) Token: 0x060000D0 RID: 208 RVA: 0x000056E5 File Offset: 0x000038E5
		public virtual Material material
		{
			get
			{
				if (!(this.m_Material != null))
				{
					return this.defaultMaterial;
				}
				return this.m_Material;
			}
			set
			{
				if (this.m_Material == value)
				{
					return;
				}
				this.m_Material = value;
				this.SetMaterialDirty();
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x00005704 File Offset: 0x00003904
		public virtual Material materialForRendering
		{
			get
			{
				List<Component> list = CollectionPool<List<Component>, Component>.Get();
				base.GetComponents(typeof(IMaterialModifier), list);
				Material material = this.material;
				for (int i = 0; i < list.Count; i++)
				{
					material = (list[i] as IMaterialModifier).GetModifiedMaterial(material);
				}
				CollectionPool<List<Component>, Component>.Release(list);
				return material;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x0000575A File Offset: 0x0000395A
		public virtual Texture mainTexture
		{
			get
			{
				return Graphic.s_WhiteTexture;
			}
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00005761 File Offset: 0x00003961
		protected override void OnEnable()
		{
			base.OnEnable();
			this.CacheCanvas();
			GraphicRegistry.RegisterGraphicForCanvas(this.canvas, this);
			if (Graphic.s_WhiteTexture == null)
			{
				Graphic.s_WhiteTexture = Texture2D.whiteTexture;
			}
			this.SetAllDirty();
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00005798 File Offset: 0x00003998
		protected override void OnDisable()
		{
			GraphicRegistry.DisableGraphicForCanvas(this.canvas, this);
			CanvasUpdateRegistry.DisableCanvasElementForRebuild(this);
			if (this.canvasRenderer != null)
			{
				this.canvasRenderer.Clear();
			}
			LayoutRebuilder.MarkLayoutForRebuild(this.rectTransform);
			base.OnDisable();
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x000057D6 File Offset: 0x000039D6
		protected override void OnDestroy()
		{
			GraphicRegistry.UnregisterGraphicForCanvas(this.canvas, this);
			CanvasUpdateRegistry.UnRegisterCanvasElementForRebuild(this);
			if (this.m_CachedMesh)
			{
				Object.Destroy(this.m_CachedMesh);
			}
			this.m_CachedMesh = null;
			base.OnDestroy();
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00005810 File Offset: 0x00003A10
		protected override void OnCanvasHierarchyChanged()
		{
			Canvas canvas = this.m_Canvas;
			this.m_Canvas = null;
			if (!this.IsActive())
			{
				GraphicRegistry.UnregisterGraphicForCanvas(canvas, this);
				return;
			}
			this.CacheCanvas();
			if (canvas != this.m_Canvas)
			{
				GraphicRegistry.UnregisterGraphicForCanvas(canvas, this);
				if (this.IsActive())
				{
					GraphicRegistry.RegisterGraphicForCanvas(this.canvas, this);
				}
			}
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x0000586A File Offset: 0x00003A6A
		public virtual void OnCullingChanged()
		{
			if (!this.canvasRenderer.cull && (this.m_VertsDirty || this.m_MaterialDirty))
			{
				CanvasUpdateRegistry.RegisterCanvasElementForGraphicRebuild(this);
			}
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00005890 File Offset: 0x00003A90
		public virtual void Rebuild(CanvasUpdate update)
		{
			if (this.canvasRenderer == null || this.canvasRenderer.cull)
			{
				return;
			}
			if (update == CanvasUpdate.PreRender)
			{
				if (this.m_VertsDirty)
				{
					this.UpdateGeometry();
					this.m_VertsDirty = false;
				}
				if (this.m_MaterialDirty)
				{
					this.UpdateMaterial();
					this.m_MaterialDirty = false;
				}
			}
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x000058E7 File Offset: 0x00003AE7
		public virtual void LayoutComplete()
		{
		}

		// Token: 0x060000DA RID: 218 RVA: 0x000058E9 File Offset: 0x00003AE9
		public virtual void GraphicUpdateComplete()
		{
		}

		// Token: 0x060000DB RID: 219 RVA: 0x000058EB File Offset: 0x00003AEB
		protected virtual void UpdateMaterial()
		{
			if (!this.IsActive())
			{
				return;
			}
			this.canvasRenderer.materialCount = 1;
			this.canvasRenderer.SetMaterial(this.materialForRendering, 0);
			this.canvasRenderer.SetTexture(this.mainTexture);
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00005925 File Offset: 0x00003B25
		protected virtual void UpdateGeometry()
		{
			if (this.useLegacyMeshGeneration)
			{
				this.DoLegacyMeshGeneration();
				return;
			}
			this.DoMeshGeneration();
		}

		// Token: 0x060000DD RID: 221 RVA: 0x0000593C File Offset: 0x00003B3C
		private void DoMeshGeneration()
		{
			if (this.rectTransform != null && this.rectTransform.rect.width >= 0f && this.rectTransform.rect.height >= 0f)
			{
				this.OnPopulateMesh(Graphic.s_VertexHelper);
			}
			else
			{
				Graphic.s_VertexHelper.Clear();
			}
			List<Component> list = CollectionPool<List<Component>, Component>.Get();
			base.GetComponents(typeof(IMeshModifier), list);
			for (int i = 0; i < list.Count; i++)
			{
				((IMeshModifier)list[i]).ModifyMesh(Graphic.s_VertexHelper);
			}
			CollectionPool<List<Component>, Component>.Release(list);
			Graphic.s_VertexHelper.FillMesh(Graphic.workerMesh);
			this.canvasRenderer.SetMesh(Graphic.workerMesh);
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00005A08 File Offset: 0x00003C08
		private void DoLegacyMeshGeneration()
		{
			if (this.rectTransform != null && this.rectTransform.rect.width >= 0f && this.rectTransform.rect.height >= 0f)
			{
				this.OnPopulateMesh(Graphic.workerMesh);
			}
			else
			{
				Graphic.workerMesh.Clear();
			}
			List<Component> list = CollectionPool<List<Component>, Component>.Get();
			base.GetComponents(typeof(IMeshModifier), list);
			for (int i = 0; i < list.Count; i++)
			{
				((IMeshModifier)list[i]).ModifyMesh(Graphic.workerMesh);
			}
			CollectionPool<List<Component>, Component>.Release(list);
			this.canvasRenderer.SetMesh(Graphic.workerMesh);
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000DF RID: 223 RVA: 0x00005AC2 File Offset: 0x00003CC2
		protected static Mesh workerMesh
		{
			get
			{
				if (Graphic.s_Mesh == null)
				{
					Graphic.s_Mesh = new Mesh();
					Graphic.s_Mesh.name = "Shared UI Mesh";
					Graphic.s_Mesh.hideFlags = HideFlags.HideAndDontSave;
				}
				return Graphic.s_Mesh;
			}
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00005AFB File Offset: 0x00003CFB
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use OnPopulateMesh instead.", true)]
		protected virtual void OnFillVBO(List<UIVertex> vbo)
		{
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00005AFD File Offset: 0x00003CFD
		[Obsolete("Use OnPopulateMesh(VertexHelper vh) instead.", false)]
		protected virtual void OnPopulateMesh(Mesh m)
		{
			this.OnPopulateMesh(Graphic.s_VertexHelper);
			Graphic.s_VertexHelper.FillMesh(m);
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00005B18 File Offset: 0x00003D18
		protected virtual void OnPopulateMesh(VertexHelper vh)
		{
			Rect pixelAdjustedRect = this.GetPixelAdjustedRect();
			Vector4 vector = new Vector4(pixelAdjustedRect.x, pixelAdjustedRect.y, pixelAdjustedRect.x + pixelAdjustedRect.width, pixelAdjustedRect.y + pixelAdjustedRect.height);
			Color32 color = this.color;
			vh.Clear();
			vh.AddVert(new Vector3(vector.x, vector.y), color, new Vector2(0f, 0f));
			vh.AddVert(new Vector3(vector.x, vector.w), color, new Vector2(0f, 1f));
			vh.AddVert(new Vector3(vector.z, vector.w), color, new Vector2(1f, 1f));
			vh.AddVert(new Vector3(vector.z, vector.y), color, new Vector2(1f, 0f));
			vh.AddTriangle(0, 1, 2);
			vh.AddTriangle(2, 3, 0);
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00005C33 File Offset: 0x00003E33
		protected override void OnDidApplyAnimationProperties()
		{
			this.SetAllDirty();
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00005C3B File Offset: 0x00003E3B
		public virtual void SetNativeSize()
		{
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00005C40 File Offset: 0x00003E40
		public virtual bool Raycast(Vector2 sp, Camera eventCamera)
		{
			if (!base.isActiveAndEnabled)
			{
				return false;
			}
			Transform transform = base.transform;
			List<Component> list = CollectionPool<List<Component>, Component>.Get();
			bool flag = false;
			bool flag2 = true;
			while (transform != null)
			{
				transform.GetComponents<Component>(list);
				for (int i = 0; i < list.Count; i++)
				{
					Canvas canvas = list[i] as Canvas;
					if (canvas != null && canvas.overrideSorting)
					{
						flag2 = false;
					}
					ICanvasRaycastFilter canvasRaycastFilter = list[i] as ICanvasRaycastFilter;
					if (canvasRaycastFilter != null)
					{
						bool flag3 = true;
						CanvasGroup canvasGroup = list[i] as CanvasGroup;
						if (canvasGroup != null)
						{
							if (!canvasGroup.enabled)
							{
								goto IL_CD;
							}
							if (!flag && canvasGroup.ignoreParentGroups)
							{
								flag = true;
								flag3 = canvasRaycastFilter.IsRaycastLocationValid(sp, eventCamera);
							}
							else if (!flag)
							{
								flag3 = canvasRaycastFilter.IsRaycastLocationValid(sp, eventCamera);
							}
						}
						else
						{
							flag3 = canvasRaycastFilter.IsRaycastLocationValid(sp, eventCamera);
						}
						if (!flag3)
						{
							CollectionPool<List<Component>, Component>.Release(list);
							return false;
						}
					}
					IL_CD:;
				}
				transform = (flag2 ? transform.parent : null);
			}
			CollectionPool<List<Component>, Component>.Release(list);
			return true;
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00005D50 File Offset: 0x00003F50
		public Vector2 PixelAdjustPoint(Vector2 point)
		{
			if (!this.canvas || this.canvas.renderMode == RenderMode.WorldSpace || this.canvas.scaleFactor == 0f || !this.canvas.pixelPerfect)
			{
				return point;
			}
			return RectTransformUtility.PixelAdjustPoint(point, base.transform, this.canvas);
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00005DAC File Offset: 0x00003FAC
		public Rect GetPixelAdjustedRect()
		{
			if (!this.canvas || this.canvas.renderMode == RenderMode.WorldSpace || this.canvas.scaleFactor == 0f || !this.canvas.pixelPerfect)
			{
				return this.rectTransform.rect;
			}
			return RectTransformUtility.PixelAdjustRect(this.rectTransform, this.canvas);
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00005E10 File Offset: 0x00004010
		public virtual void CrossFadeColor(Color targetColor, float duration, bool ignoreTimeScale, bool useAlpha)
		{
			this.CrossFadeColor(targetColor, duration, ignoreTimeScale, useAlpha, true);
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00005E20 File Offset: 0x00004020
		public virtual void CrossFadeColor(Color targetColor, float duration, bool ignoreTimeScale, bool useAlpha, bool useRGB)
		{
			if (this.canvasRenderer == null || (!useRGB && !useAlpha))
			{
				return;
			}
			if (this.canvasRenderer.GetColor().Equals(targetColor))
			{
				this.m_ColorTweenRunner.StopTween();
				return;
			}
			ColorTween.ColorTweenMode tweenMode = (useRGB && useAlpha) ? ColorTween.ColorTweenMode.All : (useRGB ? ColorTween.ColorTweenMode.RGB : ColorTween.ColorTweenMode.Alpha);
			ColorTween info = new ColorTween
			{
				duration = duration,
				startColor = this.canvasRenderer.GetColor(),
				targetColor = targetColor
			};
			info.AddOnChangedCallback(new UnityAction<Color>(this.canvasRenderer.SetColor));
			info.ignoreTimeScale = ignoreTimeScale;
			info.tweenMode = tweenMode;
			this.m_ColorTweenRunner.StartTween(info);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00005EDC File Offset: 0x000040DC
		private static Color CreateColorFromAlpha(float alpha)
		{
			Color black = Color.black;
			black.a = alpha;
			return black;
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00005EF8 File Offset: 0x000040F8
		public virtual void CrossFadeAlpha(float alpha, float duration, bool ignoreTimeScale)
		{
			this.CrossFadeColor(Graphic.CreateColorFromAlpha(alpha), duration, ignoreTimeScale, true, false);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00005F0A File Offset: 0x0000410A
		public void RegisterDirtyLayoutCallback(UnityAction action)
		{
			this.m_OnDirtyLayoutCallback = (UnityAction)Delegate.Combine(this.m_OnDirtyLayoutCallback, action);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00005F23 File Offset: 0x00004123
		public void UnregisterDirtyLayoutCallback(UnityAction action)
		{
			this.m_OnDirtyLayoutCallback = (UnityAction)Delegate.Remove(this.m_OnDirtyLayoutCallback, action);
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00005F3C File Offset: 0x0000413C
		public void RegisterDirtyVerticesCallback(UnityAction action)
		{
			this.m_OnDirtyVertsCallback = (UnityAction)Delegate.Combine(this.m_OnDirtyVertsCallback, action);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00005F55 File Offset: 0x00004155
		public void UnregisterDirtyVerticesCallback(UnityAction action)
		{
			this.m_OnDirtyVertsCallback = (UnityAction)Delegate.Remove(this.m_OnDirtyVertsCallback, action);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00005F6E File Offset: 0x0000416E
		public void RegisterDirtyMaterialCallback(UnityAction action)
		{
			this.m_OnDirtyMaterialCallback = (UnityAction)Delegate.Combine(this.m_OnDirtyMaterialCallback, action);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00005F87 File Offset: 0x00004187
		public void UnregisterDirtyMaterialCallback(UnityAction action)
		{
			this.m_OnDirtyMaterialCallback = (UnityAction)Delegate.Remove(this.m_OnDirtyMaterialCallback, action);
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00005FA0 File Offset: 0x000041A0
		// Note: this type is marked as 'beforefieldinit'.
		static Graphic()
		{
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00005FB8 File Offset: 0x000041B8
		Transform ICanvasElement.get_transform()
		{
			return base.transform;
		}

		// Token: 0x0400004E RID: 78
		protected static Material s_DefaultUI = null;

		// Token: 0x0400004F RID: 79
		protected static Texture2D s_WhiteTexture = null;

		// Token: 0x04000050 RID: 80
		[FormerlySerializedAs("m_Mat")]
		[SerializeField]
		protected Material m_Material;

		// Token: 0x04000051 RID: 81
		[SerializeField]
		private Color m_Color = Color.white;

		// Token: 0x04000052 RID: 82
		[NonSerialized]
		protected bool m_SkipLayoutUpdate;

		// Token: 0x04000053 RID: 83
		[NonSerialized]
		protected bool m_SkipMaterialUpdate;

		// Token: 0x04000054 RID: 84
		[SerializeField]
		private bool m_RaycastTarget = true;

		// Token: 0x04000055 RID: 85
		private bool m_RaycastTargetCache = true;

		// Token: 0x04000056 RID: 86
		[SerializeField]
		private Vector4 m_RaycastPadding;

		// Token: 0x04000057 RID: 87
		[NonSerialized]
		private RectTransform m_RectTransform;

		// Token: 0x04000058 RID: 88
		[NonSerialized]
		private CanvasRenderer m_CanvasRenderer;

		// Token: 0x04000059 RID: 89
		[NonSerialized]
		private Canvas m_Canvas;

		// Token: 0x0400005A RID: 90
		[NonSerialized]
		private bool m_VertsDirty;

		// Token: 0x0400005B RID: 91
		[NonSerialized]
		private bool m_MaterialDirty;

		// Token: 0x0400005C RID: 92
		[NonSerialized]
		protected UnityAction m_OnDirtyLayoutCallback;

		// Token: 0x0400005D RID: 93
		[NonSerialized]
		protected UnityAction m_OnDirtyVertsCallback;

		// Token: 0x0400005E RID: 94
		[NonSerialized]
		protected UnityAction m_OnDirtyMaterialCallback;

		// Token: 0x0400005F RID: 95
		[NonSerialized]
		protected static Mesh s_Mesh;

		// Token: 0x04000060 RID: 96
		[NonSerialized]
		private static readonly VertexHelper s_VertexHelper = new VertexHelper();

		// Token: 0x04000061 RID: 97
		[NonSerialized]
		protected Mesh m_CachedMesh;

		// Token: 0x04000062 RID: 98
		[NonSerialized]
		protected Vector2[] m_CachedUvs;

		// Token: 0x04000063 RID: 99
		[NonSerialized]
		private readonly TweenRunner<ColorTween> m_ColorTweenRunner;

		// Token: 0x04000064 RID: 100
		[CompilerGenerated]
		private bool <useLegacyMeshGeneration>k__BackingField;
	}
}
