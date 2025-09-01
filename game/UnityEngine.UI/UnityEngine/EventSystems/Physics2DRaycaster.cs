using System;
using System.Collections.Generic;
using UnityEngine.Rendering;
using UnityEngine.Tilemaps;
using UnityEngine.U2D;
using UnityEngine.UI;

namespace UnityEngine.EventSystems
{
	// Token: 0x02000070 RID: 112
	[AddComponentMenu("Event/Physics 2D Raycaster")]
	[RequireComponent(typeof(Camera))]
	public class Physics2DRaycaster : PhysicsRaycaster
	{
		// Token: 0x0600066B RID: 1643 RVA: 0x0001B236 File Offset: 0x00019436
		protected Physics2DRaycaster()
		{
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x0001B240 File Offset: 0x00019440
		public override void Raycast(PointerEventData eventData, List<RaycastResult> resultAppendList)
		{
			Ray r = default(Ray);
			float f = 0f;
			int displayIndex = 0;
			if (!base.ComputeRayAndDistance(eventData, ref r, ref displayIndex, ref f))
			{
				return;
			}
			int num;
			if (base.maxRayIntersections == 0)
			{
				if (ReflectionMethodsCache.Singleton.getRayIntersectionAll == null)
				{
					return;
				}
				this.m_Hits = ReflectionMethodsCache.Singleton.getRayIntersectionAll(r, f, base.finalEventMask);
				num = this.m_Hits.Length;
			}
			else
			{
				if (ReflectionMethodsCache.Singleton.getRayIntersectionAllNonAlloc == null)
				{
					return;
				}
				if (this.m_LastMaxRayIntersections != this.m_MaxRayIntersections)
				{
					this.m_Hits = new RaycastHit2D[base.maxRayIntersections];
					this.m_LastMaxRayIntersections = this.m_MaxRayIntersections;
				}
				num = ReflectionMethodsCache.Singleton.getRayIntersectionAllNonAlloc(r, this.m_Hits, f, base.finalEventMask);
			}
			if (num != 0)
			{
				int i = 0;
				int num2 = num;
				while (i < num2)
				{
					Renderer renderer = null;
					Renderer component = this.m_Hits[i].collider.gameObject.GetComponent<Renderer>();
					if (component != null)
					{
						if (component is SpriteRenderer)
						{
							renderer = component;
						}
						if (component is TilemapRenderer)
						{
							renderer = component;
						}
						if (component is SpriteShapeRenderer)
						{
							renderer = component;
						}
					}
					RaycastResult raycastResult = new RaycastResult
					{
						gameObject = this.m_Hits[i].collider.gameObject,
						module = this,
						distance = Vector3.Distance(this.eventCamera.transform.position, this.m_Hits[i].point),
						worldPosition = this.m_Hits[i].point,
						worldNormal = this.m_Hits[i].normal,
						screenPosition = eventData.position,
						displayIndex = displayIndex,
						index = (float)resultAppendList.Count,
						sortingGroupID = ((renderer != null) ? renderer.sortingGroupID : SortingGroup.invalidSortingGroupID),
						sortingGroupOrder = ((renderer != null) ? renderer.sortingGroupOrder : 0),
						sortingLayer = ((renderer != null) ? renderer.sortingLayerID : 0),
						sortingOrder = ((renderer != null) ? renderer.sortingOrder : 0)
					};
					if (raycastResult.sortingGroupID != SortingGroup.invalidSortingGroupID)
					{
						SortingGroup sortingGroupByIndex = SortingGroup.GetSortingGroupByIndex(renderer.sortingGroupID);
						if (sortingGroupByIndex != null)
						{
							raycastResult.distance = Vector3.Dot(r.direction, sortingGroupByIndex.transform.position - r.origin);
							raycastResult.sortingLayer = sortingGroupByIndex.sortingLayerID;
							raycastResult.sortingOrder = sortingGroupByIndex.sortingOrder;
						}
					}
					resultAppendList.Add(raycastResult);
					i++;
				}
			}
		}

		// Token: 0x0400022A RID: 554
		private RaycastHit2D[] m_Hits;
	}
}
