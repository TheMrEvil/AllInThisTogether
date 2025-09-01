using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Unity.AI.Navigation
{
	// Token: 0x02000003 RID: 3
	[ExecuteInEditMode]
	[DefaultExecutionOrder(-101)]
	[AddComponentMenu("Navigation/NavMeshLink", 33)]
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.ai.navigation@1.0/manual/NavMeshModifier.html")]
	public class NavMeshLink : MonoBehaviour
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		// (set) Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
		public int agentTypeID
		{
			get
			{
				return this.m_AgentTypeID;
			}
			set
			{
				this.m_AgentTypeID = value;
				this.UpdateLink();
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00002067 File Offset: 0x00000267
		// (set) Token: 0x06000004 RID: 4 RVA: 0x0000206F File Offset: 0x0000026F
		public Vector3 startPoint
		{
			get
			{
				return this.m_StartPoint;
			}
			set
			{
				this.m_StartPoint = value;
				this.UpdateLink();
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000005 RID: 5 RVA: 0x0000207E File Offset: 0x0000027E
		// (set) Token: 0x06000006 RID: 6 RVA: 0x00002086 File Offset: 0x00000286
		public Vector3 endPoint
		{
			get
			{
				return this.m_EndPoint;
			}
			set
			{
				this.m_EndPoint = value;
				this.UpdateLink();
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002095 File Offset: 0x00000295
		// (set) Token: 0x06000008 RID: 8 RVA: 0x0000209D File Offset: 0x0000029D
		public float width
		{
			get
			{
				return this.m_Width;
			}
			set
			{
				this.m_Width = value;
				this.UpdateLink();
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000009 RID: 9 RVA: 0x000020AC File Offset: 0x000002AC
		// (set) Token: 0x0600000A RID: 10 RVA: 0x000020B4 File Offset: 0x000002B4
		public int costModifier
		{
			get
			{
				return this.m_CostModifier;
			}
			set
			{
				this.m_CostModifier = value;
				this.UpdateLink();
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000020C3 File Offset: 0x000002C3
		// (set) Token: 0x0600000C RID: 12 RVA: 0x000020CB File Offset: 0x000002CB
		public bool bidirectional
		{
			get
			{
				return this.m_Bidirectional;
			}
			set
			{
				this.m_Bidirectional = value;
				this.UpdateLink();
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000D RID: 13 RVA: 0x000020DA File Offset: 0x000002DA
		// (set) Token: 0x0600000E RID: 14 RVA: 0x000020E2 File Offset: 0x000002E2
		public bool autoUpdate
		{
			get
			{
				return this.m_AutoUpdatePosition;
			}
			set
			{
				this.SetAutoUpdate(value);
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600000F RID: 15 RVA: 0x000020EB File Offset: 0x000002EB
		// (set) Token: 0x06000010 RID: 16 RVA: 0x000020F3 File Offset: 0x000002F3
		public int area
		{
			get
			{
				return this.m_Area;
			}
			set
			{
				this.m_Area = value;
				this.UpdateLink();
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002102 File Offset: 0x00000302
		private void OnEnable()
		{
			this.AddLink();
			if (this.m_AutoUpdatePosition && this.m_LinkInstance.valid)
			{
				NavMeshLink.AddTracking(this);
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002125 File Offset: 0x00000325
		private void OnDisable()
		{
			NavMeshLink.RemoveTracking(this);
			this.m_LinkInstance.Remove();
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002138 File Offset: 0x00000338
		public void UpdateLink()
		{
			this.m_LinkInstance.Remove();
			this.AddLink();
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000214B File Offset: 0x0000034B
		private static void AddTracking(NavMeshLink link)
		{
			if (NavMeshLink.s_Tracked.Count == 0)
			{
				NavMesh.onPreUpdate = (NavMesh.OnNavMeshPreUpdate)Delegate.Combine(NavMesh.onPreUpdate, new NavMesh.OnNavMeshPreUpdate(NavMeshLink.UpdateTrackedInstances));
			}
			NavMeshLink.s_Tracked.Add(link);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002184 File Offset: 0x00000384
		private static void RemoveTracking(NavMeshLink link)
		{
			NavMeshLink.s_Tracked.Remove(link);
			if (NavMeshLink.s_Tracked.Count == 0)
			{
				NavMesh.onPreUpdate = (NavMesh.OnNavMeshPreUpdate)Delegate.Remove(NavMesh.onPreUpdate, new NavMesh.OnNavMeshPreUpdate(NavMeshLink.UpdateTrackedInstances));
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000021BE File Offset: 0x000003BE
		private void SetAutoUpdate(bool value)
		{
			if (this.m_AutoUpdatePosition == value)
			{
				return;
			}
			this.m_AutoUpdatePosition = value;
			if (value)
			{
				NavMeshLink.AddTracking(this);
				return;
			}
			NavMeshLink.RemoveTracking(this);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000021E4 File Offset: 0x000003E4
		private void AddLink()
		{
			this.m_LinkInstance = NavMesh.AddLink(new NavMeshLinkData
			{
				startPosition = this.m_StartPoint,
				endPosition = this.m_EndPoint,
				width = this.m_Width,
				costModifier = (float)this.m_CostModifier,
				bidirectional = this.m_Bidirectional,
				area = this.m_Area,
				agentTypeID = this.m_AgentTypeID
			}, base.transform.position, base.transform.rotation);
			if (this.m_LinkInstance.valid)
			{
				this.m_LinkInstance.owner = this;
			}
			this.m_LastPosition = base.transform.position;
			this.m_LastRotation = base.transform.rotation;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000022B2 File Offset: 0x000004B2
		private bool HasTransformChanged()
		{
			return this.m_LastPosition != base.transform.position || this.m_LastRotation != base.transform.rotation;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000022E9 File Offset: 0x000004E9
		private void OnDidApplyAnimationProperties()
		{
			this.UpdateLink();
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000022F4 File Offset: 0x000004F4
		private static void UpdateTrackedInstances()
		{
			foreach (NavMeshLink navMeshLink in NavMeshLink.s_Tracked)
			{
				if (navMeshLink.HasTransformChanged())
				{
					navMeshLink.UpdateLink();
				}
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002350 File Offset: 0x00000550
		public NavMeshLink()
		{
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000023BB File Offset: 0x000005BB
		// Note: this type is marked as 'beforefieldinit'.
		static NavMeshLink()
		{
		}

		// Token: 0x04000005 RID: 5
		[SerializeField]
		private int m_AgentTypeID;

		// Token: 0x04000006 RID: 6
		[SerializeField]
		private Vector3 m_StartPoint = new Vector3(0f, 0f, -2.5f);

		// Token: 0x04000007 RID: 7
		[SerializeField]
		private Vector3 m_EndPoint = new Vector3(0f, 0f, 2.5f);

		// Token: 0x04000008 RID: 8
		[SerializeField]
		private float m_Width;

		// Token: 0x04000009 RID: 9
		[SerializeField]
		private int m_CostModifier = -1;

		// Token: 0x0400000A RID: 10
		[SerializeField]
		private bool m_Bidirectional = true;

		// Token: 0x0400000B RID: 11
		[SerializeField]
		private bool m_AutoUpdatePosition;

		// Token: 0x0400000C RID: 12
		[SerializeField]
		private int m_Area;

		// Token: 0x0400000D RID: 13
		private NavMeshLinkInstance m_LinkInstance;

		// Token: 0x0400000E RID: 14
		private Vector3 m_LastPosition = Vector3.zero;

		// Token: 0x0400000F RID: 15
		private Quaternion m_LastRotation = Quaternion.identity;

		// Token: 0x04000010 RID: 16
		private static readonly List<NavMeshLink> s_Tracked = new List<NavMeshLink>();
	}
}
