using System;
using System.Collections.Generic;
using UnityEngine;

namespace PepijnWillekens.EasyWallColliderUnity
{
	// Token: 0x02000004 RID: 4
	[ExecuteAlways]
	public class EasyWallCollider : MonoBehaviour
	{
		// Token: 0x06000022 RID: 34 RVA: 0x0000241C File Offset: 0x0000061C
		public EasyWallCollider()
		{
		}

		// Token: 0x04000001 RID: 1
		[HideInInspector]
		public Transform colliderContainer;

		// Token: 0x04000002 RID: 2
		[HideInInspector]
		public List<Transform> corners = new List<Transform>();

		// Token: 0x04000003 RID: 3
		[Header("Settings")]
		public float radius = 0.5f;

		// Token: 0x04000004 RID: 4
		public float heigth = 2f;

		// Token: 0x04000005 RID: 5
		public float depth;

		// Token: 0x04000006 RID: 6
		public float extraWidth;

		// Token: 0x04000007 RID: 7
		public bool invert;

		// Token: 0x04000008 RID: 8
		public bool loop;

		// Token: 0x04000009 RID: 9
		public bool makeRenderers;

		// Token: 0x0400000A RID: 10
		public Material material;

		// Token: 0x0400000B RID: 11
		public LayerMask RenderLayer;

		// Token: 0x0400000C RID: 12
		public PhysicMaterial physicsMaterial;

		// Token: 0x0400000D RID: 13
		[Header("Gizmos")]
		public bool onlyWhenSelected;

		// Token: 0x0400000E RID: 14
		public float GizmoLineInterval = 0.5f;

		// Token: 0x0400000F RID: 15
		public Color GizmoColor = Color.green;

		// Token: 0x04000010 RID: 16
		[Tooltip("Enables disables hiding of the collider objects")]
		public bool DEBUG;

		// Token: 0x04000011 RID: 17
		private List<Vector2> cachedCornerList = new List<Vector2>();

		// Token: 0x04000012 RID: 18
		private List<Vector2> cachedCornerListToOutset = new List<Vector2>();

		// Token: 0x04000013 RID: 19
		[HideInInspector]
		public int lastHash;
	}
}
