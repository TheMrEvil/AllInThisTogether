using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace UnityEngine.UI
{
	// Token: 0x02000012 RID: 18
	[AddComponentMenu("Event/Graphic Raycaster")]
	[RequireComponent(typeof(Canvas))]
	public class GraphicRaycaster : BaseRaycaster
	{
		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x00005FC0 File Offset: 0x000041C0
		public override int sortOrderPriority
		{
			get
			{
				if (this.canvas.renderMode == RenderMode.ScreenSpaceOverlay)
				{
					return this.canvas.sortingOrder;
				}
				return base.sortOrderPriority;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x00005FE1 File Offset: 0x000041E1
		public override int renderOrderPriority
		{
			get
			{
				if (this.canvas.renderMode == RenderMode.ScreenSpaceOverlay)
				{
					return this.canvas.rootCanvas.renderOrder;
				}
				return base.renderOrderPriority;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x00006007 File Offset: 0x00004207
		// (set) Token: 0x060000F7 RID: 247 RVA: 0x0000600F File Offset: 0x0000420F
		public bool ignoreReversedGraphics
		{
			get
			{
				return this.m_IgnoreReversedGraphics;
			}
			set
			{
				this.m_IgnoreReversedGraphics = value;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x00006018 File Offset: 0x00004218
		// (set) Token: 0x060000F9 RID: 249 RVA: 0x00006020 File Offset: 0x00004220
		public GraphicRaycaster.BlockingObjects blockingObjects
		{
			get
			{
				return this.m_BlockingObjects;
			}
			set
			{
				this.m_BlockingObjects = value;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000FA RID: 250 RVA: 0x00006029 File Offset: 0x00004229
		// (set) Token: 0x060000FB RID: 251 RVA: 0x00006031 File Offset: 0x00004231
		public LayerMask blockingMask
		{
			get
			{
				return this.m_BlockingMask;
			}
			set
			{
				this.m_BlockingMask = value;
			}
		}

		// Token: 0x060000FC RID: 252 RVA: 0x0000603A File Offset: 0x0000423A
		protected GraphicRaycaster()
		{
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000FD RID: 253 RVA: 0x00006060 File Offset: 0x00004260
		private Canvas canvas
		{
			get
			{
				if (this.m_Canvas != null)
				{
					return this.m_Canvas;
				}
				this.m_Canvas = base.GetComponent<Canvas>();
				return this.m_Canvas;
			}
		}

		// Token: 0x060000FE RID: 254 RVA: 0x0000608C File Offset: 0x0000428C
		public override void Raycast(PointerEventData eventData, List<RaycastResult> resultAppendList)
		{
			if (this.canvas == null)
			{
				return;
			}
			IList<Graphic> raycastableGraphicsForCanvas = GraphicRegistry.GetRaycastableGraphicsForCanvas(this.canvas);
			if (raycastableGraphicsForCanvas == null || raycastableGraphicsForCanvas.Count == 0)
			{
				return;
			}
			Camera eventCamera = this.eventCamera;
			int targetDisplay;
			if (this.canvas.renderMode == RenderMode.ScreenSpaceOverlay || eventCamera == null)
			{
				targetDisplay = this.canvas.targetDisplay;
			}
			else
			{
				targetDisplay = eventCamera.targetDisplay;
			}
			Vector3 vector = MultipleDisplayUtilities.RelativeMouseAtScaled(eventData.position);
			if (vector != Vector3.zero)
			{
				if ((int)vector.z != targetDisplay)
				{
					return;
				}
			}
			else
			{
				vector = eventData.position;
			}
			Vector2 vector2;
			if (eventCamera == null)
			{
				float num = (float)Screen.width;
				float num2 = (float)Screen.height;
				if (targetDisplay > 0 && targetDisplay < Display.displays.Length)
				{
					num = (float)Display.displays[targetDisplay].systemWidth;
					num2 = (float)Display.displays[targetDisplay].systemHeight;
				}
				vector2 = new Vector2(vector.x / num, vector.y / num2);
			}
			else
			{
				vector2 = eventCamera.ScreenToViewportPoint(vector);
			}
			if (vector2.x < 0f || vector2.x > 1f || vector2.y < 0f || vector2.y > 1f)
			{
				return;
			}
			float num3 = float.MaxValue;
			Ray r = default(Ray);
			if (eventCamera != null)
			{
				r = eventCamera.ScreenPointToRay(vector);
			}
			if (this.canvas.renderMode != RenderMode.ScreenSpaceOverlay && this.blockingObjects != GraphicRaycaster.BlockingObjects.None)
			{
				float f = 100f;
				if (eventCamera != null)
				{
					float z = r.direction.z;
					f = (Mathf.Approximately(0f, z) ? float.PositiveInfinity : Mathf.Abs((eventCamera.farClipPlane - eventCamera.nearClipPlane) / z));
				}
				RaycastHit raycastHit;
				if ((this.blockingObjects == GraphicRaycaster.BlockingObjects.ThreeD || this.blockingObjects == GraphicRaycaster.BlockingObjects.All) && ReflectionMethodsCache.Singleton.raycast3D != null && ReflectionMethodsCache.Singleton.raycast3D(r, out raycastHit, f, this.m_BlockingMask))
				{
					num3 = raycastHit.distance;
				}
				if ((this.blockingObjects == GraphicRaycaster.BlockingObjects.TwoD || this.blockingObjects == GraphicRaycaster.BlockingObjects.All) && ReflectionMethodsCache.Singleton.raycast2D != null)
				{
					RaycastHit2D[] array = ReflectionMethodsCache.Singleton.getRayIntersectionAll(r, f, this.m_BlockingMask);
					if (array.Length != 0)
					{
						num3 = array[0].distance;
					}
				}
			}
			this.m_RaycastResults.Clear();
			GraphicRaycaster.Raycast(this.canvas, eventCamera, vector, raycastableGraphicsForCanvas, this.m_RaycastResults);
			int count = this.m_RaycastResults.Count;
			for (int i = 0; i < count; i++)
			{
				GameObject gameObject = this.m_RaycastResults[i].gameObject;
				bool flag = true;
				if (this.ignoreReversedGraphics)
				{
					if (eventCamera == null)
					{
						Vector3 rhs = gameObject.transform.rotation * Vector3.forward;
						flag = (Vector3.Dot(Vector3.forward, rhs) > 0f);
					}
					else
					{
						Vector3 b = eventCamera.transform.rotation * Vector3.forward * eventCamera.nearClipPlane;
						flag = (Vector3.Dot(gameObject.transform.position - eventCamera.transform.position - b, gameObject.transform.forward) >= 0f);
					}
				}
				if (flag)
				{
					Transform transform = gameObject.transform;
					Vector3 forward = transform.forward;
					float num4;
					if (eventCamera == null || this.canvas.renderMode == RenderMode.ScreenSpaceOverlay)
					{
						num4 = 0f;
					}
					else
					{
						num4 = Vector3.Dot(forward, transform.position - r.origin) / Vector3.Dot(forward, r.direction);
						if (num4 < 0f)
						{
							goto IL_482;
						}
					}
					if (num4 < num3)
					{
						RaycastResult item = new RaycastResult
						{
							gameObject = gameObject,
							module = this,
							distance = num4,
							screenPosition = vector,
							displayIndex = targetDisplay,
							index = (float)resultAppendList.Count,
							depth = this.m_RaycastResults[i].depth,
							sortingLayer = this.canvas.sortingLayerID,
							sortingOrder = this.canvas.sortingOrder,
							worldPosition = r.origin + r.direction * num4,
							worldNormal = -forward
						};
						resultAppendList.Add(item);
					}
				}
				IL_482:;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000FF RID: 255 RVA: 0x0000652C File Offset: 0x0000472C
		public override Camera eventCamera
		{
			get
			{
				Canvas canvas = this.canvas;
				RenderMode renderMode = canvas.renderMode;
				if (renderMode == RenderMode.ScreenSpaceOverlay || (renderMode == RenderMode.ScreenSpaceCamera && canvas.worldCamera == null))
				{
					return null;
				}
				return canvas.worldCamera ?? Camera.main;
			}
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00006570 File Offset: 0x00004770
		private static void Raycast(Canvas canvas, Camera eventCamera, Vector2 pointerPosition, IList<Graphic> foundGraphics, List<Graphic> results)
		{
			int count = foundGraphics.Count;
			for (int i = 0; i < count; i++)
			{
				Graphic graphic = foundGraphics[i];
				if (graphic.raycastTarget && !graphic.canvasRenderer.cull && graphic.depth != -1 && RectTransformUtility.RectangleContainsScreenPoint(graphic.rectTransform, pointerPosition, eventCamera, graphic.raycastPadding) && (!(eventCamera != null) || eventCamera.WorldToScreenPoint(graphic.rectTransform.position).z <= eventCamera.farClipPlane) && graphic.Raycast(pointerPosition, eventCamera))
				{
					GraphicRaycaster.s_SortedGraphics.Add(graphic);
				}
			}
			GraphicRaycaster.s_SortedGraphics.Sort((Graphic g1, Graphic g2) => g2.depth.CompareTo(g1.depth));
			count = GraphicRaycaster.s_SortedGraphics.Count;
			for (int j = 0; j < count; j++)
			{
				results.Add(GraphicRaycaster.s_SortedGraphics[j]);
			}
			GraphicRaycaster.s_SortedGraphics.Clear();
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00006663 File Offset: 0x00004863
		// Note: this type is marked as 'beforefieldinit'.
		static GraphicRaycaster()
		{
		}

		// Token: 0x04000065 RID: 101
		protected const int kNoEventMaskSet = -1;

		// Token: 0x04000066 RID: 102
		[FormerlySerializedAs("ignoreReversedGraphics")]
		[SerializeField]
		private bool m_IgnoreReversedGraphics = true;

		// Token: 0x04000067 RID: 103
		[FormerlySerializedAs("blockingObjects")]
		[SerializeField]
		private GraphicRaycaster.BlockingObjects m_BlockingObjects;

		// Token: 0x04000068 RID: 104
		[SerializeField]
		protected LayerMask m_BlockingMask = -1;

		// Token: 0x04000069 RID: 105
		private Canvas m_Canvas;

		// Token: 0x0400006A RID: 106
		[NonSerialized]
		private List<Graphic> m_RaycastResults = new List<Graphic>();

		// Token: 0x0400006B RID: 107
		[NonSerialized]
		private static readonly List<Graphic> s_SortedGraphics = new List<Graphic>();

		// Token: 0x02000081 RID: 129
		public enum BlockingObjects
		{
			// Token: 0x0400025A RID: 602
			None,
			// Token: 0x0400025B RID: 603
			TwoD,
			// Token: 0x0400025C RID: 604
			ThreeD,
			// Token: 0x0400025D RID: 605
			All
		}

		// Token: 0x02000082 RID: 130
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060006B5 RID: 1717 RVA: 0x0001BD0E File Offset: 0x00019F0E
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060006B6 RID: 1718 RVA: 0x0001BD1A File Offset: 0x00019F1A
			public <>c()
			{
			}

			// Token: 0x060006B7 RID: 1719 RVA: 0x0001BD24 File Offset: 0x00019F24
			internal int <Raycast>b__27_0(Graphic g1, Graphic g2)
			{
				return g2.depth.CompareTo(g1.depth);
			}

			// Token: 0x0400025E RID: 606
			public static readonly GraphicRaycaster.<>c <>9 = new GraphicRaycaster.<>c();

			// Token: 0x0400025F RID: 607
			public static Comparison<Graphic> <>9__27_0;
		}
	}
}
