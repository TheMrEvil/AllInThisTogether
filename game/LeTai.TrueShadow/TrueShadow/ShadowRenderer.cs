using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LeTai.TrueShadow
{
	// Token: 0x0200000E RID: 14
	[AddComponentMenu("")]
	[ExecuteAlways]
	public class ShadowRenderer : MonoBehaviour, ILayoutIgnorer, IMaterialModifier, IMeshModifier
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00003C68 File Offset: 0x00001E68
		public bool ignoreLayout
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003C6B File Offset: 0x00001E6B
		[Conditional("UNITY_EDITOR")]
		internal static void QueueRedraw()
		{
			ShadowRenderer.needRedraw = true;
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00003C73 File Offset: 0x00001E73
		// (set) Token: 0x0600006E RID: 110 RVA: 0x00003C7B File Offset: 0x00001E7B
		internal CanvasRenderer CanvasRenderer
		{
			[CompilerGenerated]
			get
			{
				return this.<CanvasRenderer>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<CanvasRenderer>k__BackingField = value;
			}
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003C84 File Offset: 0x00001E84
		public static void Initialize(TrueShadow shadow, ref ShadowRenderer renderer)
		{
			if (renderer && renderer.shadow == shadow)
			{
				renderer.gameObject.SetActive(true);
				return;
			}
			string name = shadow.gameObject.name + "'s Shadow";
			HideFlags hideFlags = HideFlags.HideAndDontSave;
			GameObject gameObject = new GameObject(name)
			{
				hideFlags = hideFlags
			};
			RectTransform rectTransform = gameObject.AddComponent<RectTransform>();
			gameObject.AddComponent<CanvasRenderer>();
			RawImage rawImage = gameObject.AddComponent<RawImage>();
			renderer = gameObject.AddComponent<ShadowRenderer>();
			shadow.SetHierachyDirty();
			rectTransform.anchorMin = Vector2.zero;
			rectTransform.anchorMax = Vector2.zero;
			rawImage.raycastTarget = false;
			rawImage.color = shadow.Color;
			renderer.shadow = shadow;
			renderer.rt = rectTransform;
			renderer.graphic = rawImage;
			renderer.UpdateMaterial();
			renderer.CanvasRenderer = gameObject.GetComponent<CanvasRenderer>();
			renderer.CanvasRenderer.SetColor(shadow.IgnoreCasterColor ? Color.white : shadow.CanvasRenderer.GetColor());
			renderer.CanvasRenderer.SetAlpha(shadow.CanvasRenderer.GetAlpha());
			renderer.ReLayout();
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003D98 File Offset: 0x00001F98
		public void UpdateMaterial()
		{
			if (!this.graphic)
			{
				return;
			}
			MaskableGraphic maskableGraphic = this.shadow.Graphic as MaskableGraphic;
			if (maskableGraphic != null)
			{
				this.graphic.maskable = maskableGraphic.maskable;
			}
			if (CanvasUpdateRegistry.IsRebuildingGraphics())
			{
				this.NextFrames(delegate
				{
					this.graphic.material = this.shadow.GetShadowRenderingMaterial();
				}, 1);
				return;
			}
			this.graphic.material = this.shadow.GetShadowRenderingMaterial();
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003E0C File Offset: 0x0000200C
		internal void ReLayout()
		{
			if (!base.isActiveAndEnabled)
			{
				return;
			}
			RectTransform rectTransform = this.shadow.RectTransform;
			if (!rectTransform)
			{
				this.CanvasRenderer.SetAlpha(0f);
				return;
			}
			if (!this.shadowTexture)
			{
				this.CanvasRenderer.SetAlpha(0f);
				return;
			}
			if (!this.shadow.SpriteMesh)
			{
				this.CanvasRenderer.SetAlpha(0f);
				return;
			}
			bool flag = !this.shadow.DisableFitCompensation && !(this.shadow.Graphic is Text);
			flag = (flag && !(this.shadow.Graphic is TextMeshProUGUI) && !(this.shadow.Graphic is TMP_SubMeshUI));
			ShadowContainer shadowContainer = this.shadow.ShadowContainer;
			float? num;
			if (shadowContainer == null)
			{
				num = null;
			}
			else
			{
				ShadowSettingSnapshot snapshot = shadowContainer.Snapshot;
				num = ((snapshot != null) ? new float?(snapshot.canvasScale) : null);
			}
			float num2 = num ?? this.graphic.canvas.scaleFactor;
			Bounds bounds = this.shadow.SpriteMesh.bounds;
			Vector2 vector = (shadowContainer == null) ? Vector2.one : (bounds.size * num2 / shadowContainer.ImprintSize);
			Vector2 vector2 = new Vector2((float)this.shadowTexture.width, (float)this.shadowTexture.height);
			vector2 *= vector;
			vector2 /= num2;
			if (flag)
			{
				if (this.shadow.Inset)
				{
					vector2 += Vector2.one / num2;
				}
				else
				{
					vector2 -= Vector2.one / num2;
				}
			}
			if (vector2.x < 0.001f || vector2.y < 0.001f)
			{
				this.CanvasRenderer.SetAlpha(0f);
				return;
			}
			this.rt.sizeDelta = vector2;
			float num3 = (float)((shadowContainer != null) ? shadowContainer.Padding : Mathf.CeilToInt(this.shadow.Size * num2));
			num3 /= num2;
			if (flag)
			{
				if (this.shadow.Inset)
				{
					num3 += 0.5f / num2;
				}
				else
				{
					num3 -= 0.5f / num2;
				}
			}
			Vector2 a = -bounds.min;
			this.rt.pivot = (a + num3 * vector) / vector2;
			Vector2? vector3;
			if (shadowContainer == null)
			{
				vector3 = null;
			}
			else
			{
				ShadowSettingSnapshot snapshot2 = shadowContainer.Snapshot;
				vector3 = ((snapshot2 != null) ? new Vector2?(snapshot2.canvasRelativeOffset) : null) / num2;
			}
			Vector2 self = vector3 ?? this.shadow.Offset;
			Vector3 vector4 = this.shadow.ShadowAsSibling ? this.shadow.Offset.WithZ(0f) : self.WithZ(0f);
			this.rt.localPosition = (this.shadow.ShadowAsSibling ? (rectTransform.localPosition + vector4) : vector4);
			this.rt.localRotation = (this.shadow.ShadowAsSibling ? rectTransform.localRotation : Quaternion.identity);
			this.rt.localScale = (this.shadow.ShadowAsSibling ? rectTransform.localScale : Vector3.one);
			Color color = this.shadow.Color;
			if (this.shadow.UseCasterAlpha)
			{
				color.a *= this.shadow.Graphic.color.a;
			}
			this.graphic.color = color;
			this.CanvasRenderer.SetColor(this.shadow.IgnoreCasterColor ? Color.white : this.shadow.CanvasRenderer.GetColor());
			this.CanvasRenderer.SetAlpha(this.shadow.CanvasRenderer.GetAlpha());
			this.graphic.Rebuild(CanvasUpdate.PreRender);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x0000426D File Offset: 0x0000246D
		public void SetTexture(Texture texture)
		{
			this.shadowTexture = texture;
			this.CanvasRenderer.SetTexture(texture);
			this.graphic.texture = texture;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x0000428E File Offset: 0x0000248E
		public void SetMaterialDirty()
		{
			this.graphic.SetMaterialDirty();
		}

		// Token: 0x06000074 RID: 116 RVA: 0x0000429B File Offset: 0x0000249B
		public void ModifyMesh(VertexHelper vertexHelper)
		{
			if (!this.shadow)
			{
				return;
			}
			this.shadow.ModifyShadowRendererMesh(vertexHelper);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000042B7 File Offset: 0x000024B7
		public void ModifyMesh(Mesh mesh)
		{
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000042B9 File Offset: 0x000024B9
		protected virtual void LateUpdate()
		{
			if (!this.shadow)
			{
				this.Dispose();
			}
			if (!this.willBeDestroyed)
			{
				base.gameObject;
			}
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000042E2 File Offset: 0x000024E2
		protected virtual void OnDestroy()
		{
			this.willBeDestroyed = true;
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000042EC File Offset: 0x000024EC
		public void Dispose()
		{
			if (this.willBeDestroyed)
			{
				return;
			}
			if (this.shadow && this.shadow.ShadowAsSibling)
			{
				base.gameObject.SetActive(false);
				base.transform.SetParent(null);
			}
			UnityEngine.Object.Destroy(base.gameObject);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00004340 File Offset: 0x00002540
		internal static void ClearMaskMaterialCache()
		{
			foreach (KeyValuePair<int, Material> keyValuePair in ShadowRenderer.MASK_MATERIALS_CACHE)
			{
				if (Application.isPlaying)
				{
					UnityEngine.Object.Destroy(keyValuePair.Value);
				}
				else
				{
					UnityEngine.Object.DestroyImmediate(keyValuePair.Value);
				}
			}
			ShadowRenderer.MASK_MATERIALS_CACHE.Clear();
		}

		// Token: 0x0600007A RID: 122 RVA: 0x000043B8 File Offset: 0x000025B8
		public Material GetModifiedMaterial(Material baseMaterial)
		{
			if (!this.shadow)
			{
				return baseMaterial;
			}
			this.shadow.ModifyShadowRendererMaterial(baseMaterial);
			if (!baseMaterial.HasProperty(ShaderId.STENCIL_ID))
			{
				return baseMaterial;
			}
			Mask component = this.shadow.GetComponent<Mask>();
			bool flag = component != null && component.isActiveAndEnabled;
			int key = HashUtils.CombineHashCodes(flag.GetHashCode(), baseMaterial.GetHashCode());
			Material material;
			ShadowRenderer.MASK_MATERIALS_CACHE.TryGetValue(key, out material);
			if (!material)
			{
				material = new Material(baseMaterial);
				if (this.shadow.ShadowAsSibling)
				{
					material.SetInt(ShaderId.COLOR_MASK, 15);
					material.SetInt(ShaderId.STENCIL_OP, 0);
				}
				else if (flag)
				{
					int num = material.GetInt(ShaderId.STENCIL_ID) + 1;
					int num2 = 0;
					while (num2 < 8 && (num >> num2 & 1) != 1)
					{
						num2++;
					}
					num2 = Mathf.Max(0, num2 - 1);
					int value = (1 << num2) - 1;
					material.SetInt(ShaderId.STENCIL_ID, value);
					material.SetInt(ShaderId.STENCIL_READ_MASK, value);
				}
				ShadowRenderer.MASK_MATERIALS_CACHE[key] = material;
			}
			else
			{
				int @int = material.GetInt(ShaderId.STENCIL_ID);
				int int2 = material.GetInt(ShaderId.STENCIL_OP);
				int int3 = material.GetInt(ShaderId.COLOR_MASK);
				int int4 = material.GetInt(ShaderId.STENCIL_READ_MASK);
				material.CopyPropertiesFromMaterial(baseMaterial);
				material.SetInt(ShaderId.STENCIL_ID, @int);
				material.SetInt(ShaderId.STENCIL_OP, int2);
				material.SetInt(ShaderId.COLOR_MASK, int3);
				material.SetInt(ShaderId.STENCIL_READ_MASK, int4);
			}
			return material;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00004547 File Offset: 0x00002747
		public ShadowRenderer()
		{
		}

		// Token: 0x0600007C RID: 124 RVA: 0x0000454F File Offset: 0x0000274F
		// Note: this type is marked as 'beforefieldinit'.
		static ShadowRenderer()
		{
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00004561 File Offset: 0x00002761
		[CompilerGenerated]
		private void <UpdateMaterial>b__13_0()
		{
			this.graphic.material = this.shadow.GetShadowRenderingMaterial();
		}

		// Token: 0x04000055 RID: 85
		private static bool needRedraw = false;

		// Token: 0x04000056 RID: 86
		[CompilerGenerated]
		private CanvasRenderer <CanvasRenderer>k__BackingField;

		// Token: 0x04000057 RID: 87
		private TrueShadow shadow;

		// Token: 0x04000058 RID: 88
		private RectTransform rt;

		// Token: 0x04000059 RID: 89
		private RawImage graphic;

		// Token: 0x0400005A RID: 90
		private Texture shadowTexture;

		// Token: 0x0400005B RID: 91
		private bool willBeDestroyed;

		// Token: 0x0400005C RID: 92
		private static readonly Dictionary<int, Material> MASK_MATERIALS_CACHE = new Dictionary<int, Material>();
	}
}
