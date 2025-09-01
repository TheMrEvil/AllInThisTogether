using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace InControl
{
	// Token: 0x0200005B RID: 91
	[Serializable]
	public class TouchSprite
	{
		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000460 RID: 1120 RVA: 0x0000FC41 File Offset: 0x0000DE41
		// (set) Token: 0x06000461 RID: 1121 RVA: 0x0000FC49 File Offset: 0x0000DE49
		public bool Dirty
		{
			[CompilerGenerated]
			get
			{
				return this.<Dirty>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Dirty>k__BackingField = value;
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06000462 RID: 1122 RVA: 0x0000FC52 File Offset: 0x0000DE52
		// (set) Token: 0x06000463 RID: 1123 RVA: 0x0000FC5A File Offset: 0x0000DE5A
		public bool Ready
		{
			[CompilerGenerated]
			get
			{
				return this.<Ready>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Ready>k__BackingField = value;
			}
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x0000FC64 File Offset: 0x0000DE64
		public TouchSprite()
		{
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x0000FCD4 File Offset: 0x0000DED4
		public TouchSprite(float size)
		{
			this.size = Vector2.one * size;
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x0000FD54 File Offset: 0x0000DF54
		public void Create(string gameObjectName, Transform parentTransform, int sortingOrder)
		{
			this.spriteGameObject = this.CreateSpriteGameObject(gameObjectName, parentTransform);
			this.spriteRenderer = this.CreateSpriteRenderer(this.spriteGameObject, this.idleSprite, sortingOrder);
			this.spriteRenderer.color = this.idleColor;
			this.Ready = true;
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x0000FDA0 File Offset: 0x0000DFA0
		public void Delete()
		{
			this.Ready = false;
			UnityEngine.Object.Destroy(this.spriteGameObject);
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x0000FDB4 File Offset: 0x0000DFB4
		public void Update()
		{
			this.Update(false);
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x0000FDC0 File Offset: 0x0000DFC0
		public void Update(bool forceUpdate)
		{
			if (this.Dirty || forceUpdate)
			{
				if (this.spriteRenderer != null)
				{
					this.spriteRenderer.sprite = (this.State ? this.busySprite : this.idleSprite);
				}
				if (this.sizeUnitType == TouchUnitType.Pixels)
				{
					Vector2 a = TouchUtility.RoundVector(this.size);
					this.ScaleSpriteInPixels(this.spriteGameObject, this.spriteRenderer, a);
					this.worldSize = a * TouchManager.PixelToWorld;
				}
				else
				{
					this.ScaleSpriteInPercent(this.spriteGameObject, this.spriteRenderer, this.size);
					if (this.lockAspectRatio)
					{
						this.worldSize = this.size * TouchManager.PercentToWorld;
					}
					else
					{
						this.worldSize = Vector2.Scale(this.size, TouchManager.ViewSize);
					}
				}
				this.Dirty = false;
			}
			if (this.spriteRenderer != null)
			{
				Color color = this.State ? this.busyColor : this.idleColor;
				if (this.spriteRenderer.color != color)
				{
					this.spriteRenderer.color = Utility.MoveColorTowards(this.spriteRenderer.color, color, 5f * Time.unscaledDeltaTime);
				}
			}
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x0000FF00 File Offset: 0x0000E100
		private GameObject CreateSpriteGameObject(string name, Transform parentTransform)
		{
			return new GameObject(name)
			{
				transform = 
				{
					parent = parentTransform,
					localPosition = Vector3.zero,
					localScale = Vector3.one
				},
				layer = parentTransform.gameObject.layer
			};
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x0000FF50 File Offset: 0x0000E150
		private SpriteRenderer CreateSpriteRenderer(GameObject spriteGameObject, Sprite sprite, int sortingOrder)
		{
			if (!TouchSprite.spriteRendererMaterial)
			{
				TouchSprite.spriteRendererShader = Shader.Find("Sprites/Default");
				TouchSprite.spriteRendererMaterial = new Material(TouchSprite.spriteRendererShader);
				TouchSprite.spriteRendererPixelSnapId = Shader.PropertyToID("PixelSnap");
			}
			SpriteRenderer spriteRenderer = spriteGameObject.AddComponent<SpriteRenderer>();
			spriteRenderer.sprite = sprite;
			spriteRenderer.sortingOrder = sortingOrder;
			spriteRenderer.sharedMaterial = TouchSprite.spriteRendererMaterial;
			spriteRenderer.sharedMaterial.SetFloat(TouchSprite.spriteRendererPixelSnapId, 1f);
			return spriteRenderer;
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x0000FFCC File Offset: 0x0000E1CC
		private void ScaleSpriteInPixels(GameObject spriteGameObject, SpriteRenderer spriteRenderer, Vector2 size)
		{
			if (spriteGameObject == null || spriteRenderer == null || spriteRenderer.sprite == null)
			{
				return;
			}
			float num = spriteRenderer.sprite.rect.width / spriteRenderer.sprite.bounds.size.x;
			float num2 = TouchManager.PixelToWorld * num;
			float x = num2 * size.x / spriteRenderer.sprite.rect.width;
			float y = num2 * size.y / spriteRenderer.sprite.rect.height;
			spriteGameObject.transform.localScale = new Vector3(x, y);
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x0001007C File Offset: 0x0000E27C
		private void ScaleSpriteInPercent(GameObject spriteGameObject, SpriteRenderer spriteRenderer, Vector2 size)
		{
			if (spriteGameObject == null || spriteRenderer == null || spriteRenderer.sprite == null)
			{
				return;
			}
			if (this.lockAspectRatio)
			{
				float num = Mathf.Min(TouchManager.ViewSize.x, TouchManager.ViewSize.y);
				float x = num * size.x / spriteRenderer.sprite.bounds.size.x;
				float y = num * size.y / spriteRenderer.sprite.bounds.size.y;
				spriteGameObject.transform.localScale = new Vector3(x, y);
				return;
			}
			float x2 = TouchManager.ViewSize.x * size.x / spriteRenderer.sprite.bounds.size.x;
			float y2 = TouchManager.ViewSize.y * size.y / spriteRenderer.sprite.bounds.size.y;
			spriteGameObject.transform.localScale = new Vector3(x2, y2);
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x0001018C File Offset: 0x0000E38C
		public bool Contains(Vector2 testWorldPoint)
		{
			if (this.shape == TouchSpriteShape.Oval)
			{
				float num = (testWorldPoint.x - this.Position.x) / this.worldSize.x;
				float num2 = (testWorldPoint.y - this.Position.y) / this.worldSize.y;
				return num * num + num2 * num2 < 0.25f;
			}
			float num3 = Utility.Abs(testWorldPoint.x - this.Position.x) * 2f;
			float num4 = Utility.Abs(testWorldPoint.y - this.Position.y) * 2f;
			return num3 <= this.worldSize.x && num4 <= this.worldSize.y;
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x00010247 File Offset: 0x0000E447
		public bool Contains(Touch touch)
		{
			return this.Contains(TouchManager.ScreenToWorldPoint(touch.position));
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x0001025F File Offset: 0x0000E45F
		public void DrawGizmos(Vector3 position, Color color)
		{
			if (this.shape == TouchSpriteShape.Oval)
			{
				Utility.DrawOvalGizmo(position, this.WorldSize, color);
				return;
			}
			Utility.DrawRectGizmo(position, this.WorldSize, color);
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06000471 RID: 1137 RVA: 0x0001028E File Offset: 0x0000E48E
		// (set) Token: 0x06000472 RID: 1138 RVA: 0x00010296 File Offset: 0x0000E496
		public bool State
		{
			get
			{
				return this.state;
			}
			set
			{
				if (this.state != value)
				{
					this.state = value;
					this.Dirty = true;
				}
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000473 RID: 1139 RVA: 0x000102AF File Offset: 0x0000E4AF
		// (set) Token: 0x06000474 RID: 1140 RVA: 0x000102B7 File Offset: 0x0000E4B7
		public Sprite BusySprite
		{
			get
			{
				return this.busySprite;
			}
			set
			{
				if (this.busySprite != value)
				{
					this.busySprite = value;
					this.Dirty = true;
				}
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000475 RID: 1141 RVA: 0x000102D5 File Offset: 0x0000E4D5
		// (set) Token: 0x06000476 RID: 1142 RVA: 0x000102DD File Offset: 0x0000E4DD
		public Sprite IdleSprite
		{
			get
			{
				return this.idleSprite;
			}
			set
			{
				if (this.idleSprite != value)
				{
					this.idleSprite = value;
					this.Dirty = true;
				}
			}
		}

		// Token: 0x17000157 RID: 343
		// (set) Token: 0x06000477 RID: 1143 RVA: 0x000102FB File Offset: 0x0000E4FB
		public Sprite Sprite
		{
			set
			{
				if (this.idleSprite != value)
				{
					this.idleSprite = value;
					this.Dirty = true;
				}
				if (this.busySprite != value)
				{
					this.busySprite = value;
					this.Dirty = true;
				}
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000478 RID: 1144 RVA: 0x00010335 File Offset: 0x0000E535
		// (set) Token: 0x06000479 RID: 1145 RVA: 0x0001033D File Offset: 0x0000E53D
		public Color BusyColor
		{
			get
			{
				return this.busyColor;
			}
			set
			{
				if (this.busyColor != value)
				{
					this.busyColor = value;
					this.Dirty = true;
				}
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x0600047A RID: 1146 RVA: 0x0001035B File Offset: 0x0000E55B
		// (set) Token: 0x0600047B RID: 1147 RVA: 0x00010363 File Offset: 0x0000E563
		public Color IdleColor
		{
			get
			{
				return this.idleColor;
			}
			set
			{
				if (this.idleColor != value)
				{
					this.idleColor = value;
					this.Dirty = true;
				}
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x0600047C RID: 1148 RVA: 0x00010381 File Offset: 0x0000E581
		// (set) Token: 0x0600047D RID: 1149 RVA: 0x00010389 File Offset: 0x0000E589
		public TouchSpriteShape Shape
		{
			get
			{
				return this.shape;
			}
			set
			{
				if (this.shape != value)
				{
					this.shape = value;
					this.Dirty = true;
				}
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x0600047E RID: 1150 RVA: 0x000103A2 File Offset: 0x0000E5A2
		// (set) Token: 0x0600047F RID: 1151 RVA: 0x000103AA File Offset: 0x0000E5AA
		public TouchUnitType SizeUnitType
		{
			get
			{
				return this.sizeUnitType;
			}
			set
			{
				if (this.sizeUnitType != value)
				{
					this.sizeUnitType = value;
					this.Dirty = true;
				}
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06000480 RID: 1152 RVA: 0x000103C3 File Offset: 0x0000E5C3
		// (set) Token: 0x06000481 RID: 1153 RVA: 0x000103CB File Offset: 0x0000E5CB
		public Vector2 Size
		{
			get
			{
				return this.size;
			}
			set
			{
				if (this.size != value)
				{
					this.size = value;
					this.Dirty = true;
				}
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000482 RID: 1154 RVA: 0x000103E9 File Offset: 0x0000E5E9
		public Vector2 WorldSize
		{
			get
			{
				return this.worldSize;
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000483 RID: 1155 RVA: 0x000103F1 File Offset: 0x0000E5F1
		// (set) Token: 0x06000484 RID: 1156 RVA: 0x00010416 File Offset: 0x0000E616
		public Vector3 Position
		{
			get
			{
				if (!this.spriteGameObject)
				{
					return Vector3.zero;
				}
				return this.spriteGameObject.transform.position;
			}
			set
			{
				if (this.spriteGameObject)
				{
					this.spriteGameObject.transform.position = value;
				}
			}
		}

		// Token: 0x040003D8 RID: 984
		[SerializeField]
		private Sprite idleSprite;

		// Token: 0x040003D9 RID: 985
		[SerializeField]
		private Sprite busySprite;

		// Token: 0x040003DA RID: 986
		[SerializeField]
		private Color idleColor = new Color(1f, 1f, 1f, 0.5f);

		// Token: 0x040003DB RID: 987
		[SerializeField]
		private Color busyColor = new Color(1f, 1f, 1f, 1f);

		// Token: 0x040003DC RID: 988
		[SerializeField]
		private TouchSpriteShape shape;

		// Token: 0x040003DD RID: 989
		[SerializeField]
		private TouchUnitType sizeUnitType;

		// Token: 0x040003DE RID: 990
		[SerializeField]
		private Vector2 size = new Vector2(10f, 10f);

		// Token: 0x040003DF RID: 991
		[SerializeField]
		private bool lockAspectRatio = true;

		// Token: 0x040003E0 RID: 992
		[SerializeField]
		[HideInInspector]
		private Vector2 worldSize;

		// Token: 0x040003E1 RID: 993
		private GameObject spriteGameObject;

		// Token: 0x040003E2 RID: 994
		private SpriteRenderer spriteRenderer;

		// Token: 0x040003E3 RID: 995
		private bool state;

		// Token: 0x040003E4 RID: 996
		[CompilerGenerated]
		private bool <Dirty>k__BackingField;

		// Token: 0x040003E5 RID: 997
		[CompilerGenerated]
		private bool <Ready>k__BackingField;

		// Token: 0x040003E6 RID: 998
		private static Shader spriteRendererShader;

		// Token: 0x040003E7 RID: 999
		private static Material spriteRendererMaterial;

		// Token: 0x040003E8 RID: 1000
		private static int spriteRendererPixelSnapId;
	}
}
