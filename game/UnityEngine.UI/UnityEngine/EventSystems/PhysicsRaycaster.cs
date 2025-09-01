using System;
using System.Collections.Generic;
using UnityEngine.UI;

namespace UnityEngine.EventSystems
{
	// Token: 0x02000071 RID: 113
	[AddComponentMenu("Event/Physics Raycaster")]
	[RequireComponent(typeof(Camera))]
	public class PhysicsRaycaster : BaseRaycaster
	{
		// Token: 0x0600066D RID: 1645 RVA: 0x0001B517 File Offset: 0x00019717
		protected PhysicsRaycaster()
		{
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x0600066E RID: 1646 RVA: 0x0001B52B File Offset: 0x0001972B
		public override Camera eventCamera
		{
			get
			{
				if (this.m_EventCamera == null)
				{
					this.m_EventCamera = base.GetComponent<Camera>();
				}
				return this.m_EventCamera ?? Camera.main;
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x0600066F RID: 1647 RVA: 0x0001B556 File Offset: 0x00019756
		public virtual int depth
		{
			get
			{
				if (!(this.eventCamera != null))
				{
					return 16777215;
				}
				return (int)this.eventCamera.depth;
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06000670 RID: 1648 RVA: 0x0001B578 File Offset: 0x00019778
		public int finalEventMask
		{
			get
			{
				if (!(this.eventCamera != null))
				{
					return -1;
				}
				return this.eventCamera.cullingMask & this.m_EventMask;
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06000671 RID: 1649 RVA: 0x0001B5A1 File Offset: 0x000197A1
		// (set) Token: 0x06000672 RID: 1650 RVA: 0x0001B5A9 File Offset: 0x000197A9
		public LayerMask eventMask
		{
			get
			{
				return this.m_EventMask;
			}
			set
			{
				this.m_EventMask = value;
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000673 RID: 1651 RVA: 0x0001B5B2 File Offset: 0x000197B2
		// (set) Token: 0x06000674 RID: 1652 RVA: 0x0001B5BA File Offset: 0x000197BA
		public int maxRayIntersections
		{
			get
			{
				return this.m_MaxRayIntersections;
			}
			set
			{
				this.m_MaxRayIntersections = value;
			}
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x0001B5C4 File Offset: 0x000197C4
		protected bool ComputeRayAndDistance(PointerEventData eventData, ref Ray ray, ref int eventDisplayIndex, ref float distanceToClipPlane)
		{
			if (this.eventCamera == null)
			{
				return false;
			}
			Vector3 vector = MultipleDisplayUtilities.RelativeMouseAtScaled(eventData.position);
			if (vector != Vector3.zero)
			{
				eventDisplayIndex = (int)vector.z;
				if (eventDisplayIndex != this.eventCamera.targetDisplay)
				{
					return false;
				}
			}
			else
			{
				vector = eventData.position;
			}
			if (!this.eventCamera.pixelRect.Contains(vector))
			{
				return false;
			}
			ray = this.eventCamera.ScreenPointToRay(vector);
			float z = ray.direction.z;
			distanceToClipPlane = (Mathf.Approximately(0f, z) ? float.PositiveInfinity : Mathf.Abs((this.eventCamera.farClipPlane - this.eventCamera.nearClipPlane) / z));
			return true;
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x0001B68C File Offset: 0x0001988C
		public override void Raycast(PointerEventData eventData, List<RaycastResult> resultAppendList)
		{
			Ray r = default(Ray);
			int displayIndex = 0;
			float f = 0f;
			if (!this.ComputeRayAndDistance(eventData, ref r, ref displayIndex, ref f))
			{
				return;
			}
			int num;
			if (this.m_MaxRayIntersections == 0)
			{
				if (ReflectionMethodsCache.Singleton.raycast3DAll == null)
				{
					return;
				}
				this.m_Hits = ReflectionMethodsCache.Singleton.raycast3DAll(r, f, this.finalEventMask);
				num = this.m_Hits.Length;
			}
			else
			{
				if (ReflectionMethodsCache.Singleton.getRaycastNonAlloc == null)
				{
					return;
				}
				if (this.m_LastMaxRayIntersections != this.m_MaxRayIntersections)
				{
					this.m_Hits = new RaycastHit[this.m_MaxRayIntersections];
					this.m_LastMaxRayIntersections = this.m_MaxRayIntersections;
				}
				num = ReflectionMethodsCache.Singleton.getRaycastNonAlloc(r, this.m_Hits, f, this.finalEventMask);
			}
			if (num != 0)
			{
				if (num > 1)
				{
					Array.Sort<RaycastHit>(this.m_Hits, 0, num, PhysicsRaycaster.RaycastHitComparer.instance);
				}
				int i = 0;
				int num2 = num;
				while (i < num2)
				{
					RaycastResult item = new RaycastResult
					{
						gameObject = this.m_Hits[i].collider.gameObject,
						module = this,
						distance = this.m_Hits[i].distance,
						worldPosition = this.m_Hits[i].point,
						worldNormal = this.m_Hits[i].normal,
						screenPosition = eventData.position,
						displayIndex = displayIndex,
						index = (float)resultAppendList.Count,
						sortingLayer = 0,
						sortingOrder = 0
					};
					resultAppendList.Add(item);
					i++;
				}
			}
		}

		// Token: 0x0400022B RID: 555
		protected const int kNoEventMaskSet = -1;

		// Token: 0x0400022C RID: 556
		protected Camera m_EventCamera;

		// Token: 0x0400022D RID: 557
		[SerializeField]
		protected LayerMask m_EventMask = -1;

		// Token: 0x0400022E RID: 558
		[SerializeField]
		protected int m_MaxRayIntersections;

		// Token: 0x0400022F RID: 559
		protected int m_LastMaxRayIntersections;

		// Token: 0x04000230 RID: 560
		private RaycastHit[] m_Hits;

		// Token: 0x020000CA RID: 202
		private class RaycastHitComparer : IComparer<RaycastHit>
		{
			// Token: 0x06000756 RID: 1878 RVA: 0x0001C97C File Offset: 0x0001AB7C
			public int Compare(RaycastHit x, RaycastHit y)
			{
				return x.distance.CompareTo(y.distance);
			}

			// Token: 0x06000757 RID: 1879 RVA: 0x0001C99F File Offset: 0x0001AB9F
			public RaycastHitComparer()
			{
			}

			// Token: 0x06000758 RID: 1880 RVA: 0x0001C9A7 File Offset: 0x0001ABA7
			// Note: this type is marked as 'beforefieldinit'.
			static RaycastHitComparer()
			{
			}

			// Token: 0x0400034F RID: 847
			public static PhysicsRaycaster.RaycastHitComparer instance = new PhysicsRaycaster.RaycastHitComparer();
		}
	}
}
