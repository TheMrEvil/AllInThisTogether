using System;
using System.Collections.Generic;
using UnityEngine.UI.Collections;

namespace UnityEngine.UI
{
	// Token: 0x02000013 RID: 19
	public class GraphicRegistry
	{
		// Token: 0x06000102 RID: 258 RVA: 0x0000666F File Offset: 0x0000486F
		protected GraphicRegistry()
		{
			GC.KeepAlive(new Dictionary<Graphic, int>());
			GC.KeepAlive(new Dictionary<ICanvasElement, int>());
			GC.KeepAlive(new Dictionary<IClipper, int>());
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000103 RID: 259 RVA: 0x000066AB File Offset: 0x000048AB
		public static GraphicRegistry instance
		{
			get
			{
				if (GraphicRegistry.s_Instance == null)
				{
					GraphicRegistry.s_Instance = new GraphicRegistry();
				}
				return GraphicRegistry.s_Instance;
			}
		}

		// Token: 0x06000104 RID: 260 RVA: 0x000066C4 File Offset: 0x000048C4
		public static void RegisterGraphicForCanvas(Canvas c, Graphic graphic)
		{
			if (c == null || graphic == null)
			{
				return;
			}
			IndexedSet<Graphic> indexedSet;
			GraphicRegistry.instance.m_Graphics.TryGetValue(c, out indexedSet);
			if (indexedSet != null)
			{
				indexedSet.AddUnique(graphic, true);
				GraphicRegistry.RegisterRaycastGraphicForCanvas(c, graphic);
				return;
			}
			indexedSet = new IndexedSet<Graphic>();
			indexedSet.Add(graphic);
			GraphicRegistry.instance.m_Graphics.Add(c, indexedSet);
			GraphicRegistry.RegisterRaycastGraphicForCanvas(c, graphic);
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00006730 File Offset: 0x00004930
		public static void RegisterRaycastGraphicForCanvas(Canvas c, Graphic graphic)
		{
			if (c == null || graphic == null || !graphic.raycastTarget)
			{
				return;
			}
			IndexedSet<Graphic> indexedSet;
			GraphicRegistry.instance.m_RaycastableGraphics.TryGetValue(c, out indexedSet);
			if (indexedSet != null)
			{
				indexedSet.AddUnique(graphic, true);
				return;
			}
			indexedSet = new IndexedSet<Graphic>();
			indexedSet.Add(graphic);
			GraphicRegistry.instance.m_RaycastableGraphics.Add(c, indexedSet);
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00006798 File Offset: 0x00004998
		public static void UnregisterGraphicForCanvas(Canvas c, Graphic graphic)
		{
			if (c == null || graphic == null)
			{
				return;
			}
			IndexedSet<Graphic> indexedSet;
			if (GraphicRegistry.instance.m_Graphics.TryGetValue(c, out indexedSet))
			{
				indexedSet.Remove(graphic);
				if (indexedSet.Capacity == 0)
				{
					GraphicRegistry.instance.m_Graphics.Remove(c);
				}
				GraphicRegistry.UnregisterRaycastGraphicForCanvas(c, graphic);
			}
		}

		// Token: 0x06000107 RID: 263 RVA: 0x000067F4 File Offset: 0x000049F4
		public static void UnregisterRaycastGraphicForCanvas(Canvas c, Graphic graphic)
		{
			if (c == null || graphic == null)
			{
				return;
			}
			IndexedSet<Graphic> indexedSet;
			if (GraphicRegistry.instance.m_RaycastableGraphics.TryGetValue(c, out indexedSet))
			{
				indexedSet.Remove(graphic);
				if (indexedSet.Count == 0)
				{
					GraphicRegistry.instance.m_RaycastableGraphics.Remove(c);
				}
			}
		}

		// Token: 0x06000108 RID: 264 RVA: 0x0000684C File Offset: 0x00004A4C
		public static void DisableGraphicForCanvas(Canvas c, Graphic graphic)
		{
			if (c == null)
			{
				return;
			}
			IndexedSet<Graphic> indexedSet;
			if (GraphicRegistry.instance.m_Graphics.TryGetValue(c, out indexedSet))
			{
				indexedSet.DisableItem(graphic);
				if (indexedSet.Capacity == 0)
				{
					GraphicRegistry.instance.m_Graphics.Remove(c);
				}
				GraphicRegistry.DisableRaycastGraphicForCanvas(c, graphic);
			}
		}

		// Token: 0x06000109 RID: 265 RVA: 0x000068A0 File Offset: 0x00004AA0
		public static void DisableRaycastGraphicForCanvas(Canvas c, Graphic graphic)
		{
			if (c == null || !graphic.raycastTarget)
			{
				return;
			}
			IndexedSet<Graphic> indexedSet;
			if (GraphicRegistry.instance.m_RaycastableGraphics.TryGetValue(c, out indexedSet))
			{
				indexedSet.DisableItem(graphic);
				if (indexedSet.Capacity == 0)
				{
					GraphicRegistry.instance.m_RaycastableGraphics.Remove(c);
				}
			}
		}

		// Token: 0x0600010A RID: 266 RVA: 0x000068F4 File Offset: 0x00004AF4
		public static IList<Graphic> GetGraphicsForCanvas(Canvas canvas)
		{
			IndexedSet<Graphic> result;
			if (GraphicRegistry.instance.m_Graphics.TryGetValue(canvas, out result))
			{
				return result;
			}
			return GraphicRegistry.s_EmptyList;
		}

		// Token: 0x0600010B RID: 267 RVA: 0x0000691C File Offset: 0x00004B1C
		public static IList<Graphic> GetRaycastableGraphicsForCanvas(Canvas canvas)
		{
			IndexedSet<Graphic> result;
			if (GraphicRegistry.instance.m_RaycastableGraphics.TryGetValue(canvas, out result))
			{
				return result;
			}
			return GraphicRegistry.s_EmptyList;
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00006944 File Offset: 0x00004B44
		// Note: this type is marked as 'beforefieldinit'.
		static GraphicRegistry()
		{
		}

		// Token: 0x0400006C RID: 108
		private static GraphicRegistry s_Instance;

		// Token: 0x0400006D RID: 109
		private readonly Dictionary<Canvas, IndexedSet<Graphic>> m_Graphics = new Dictionary<Canvas, IndexedSet<Graphic>>();

		// Token: 0x0400006E RID: 110
		private readonly Dictionary<Canvas, IndexedSet<Graphic>> m_RaycastableGraphics = new Dictionary<Canvas, IndexedSet<Graphic>>();

		// Token: 0x0400006F RID: 111
		private static readonly List<Graphic> s_EmptyList = new List<Graphic>();
	}
}
