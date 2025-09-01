using System;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.AI.Navigation
{
	// Token: 0x02000005 RID: 5
	[ExecuteInEditMode]
	[AddComponentMenu("Navigation/NavMeshModifierVolume", 31)]
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.ai.navigation@1.0/manual/NavMeshModifierVolume.html")]
	public class NavMeshModifierVolume : MonoBehaviour
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00002486 File Offset: 0x00000686
		// (set) Token: 0x0600002A RID: 42 RVA: 0x0000248E File Offset: 0x0000068E
		public Vector3 size
		{
			get
			{
				return this.m_Size;
			}
			set
			{
				this.m_Size = value;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00002497 File Offset: 0x00000697
		// (set) Token: 0x0600002C RID: 44 RVA: 0x0000249F File Offset: 0x0000069F
		public Vector3 center
		{
			get
			{
				return this.m_Center;
			}
			set
			{
				this.m_Center = value;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600002D RID: 45 RVA: 0x000024A8 File Offset: 0x000006A8
		// (set) Token: 0x0600002E RID: 46 RVA: 0x000024B0 File Offset: 0x000006B0
		public int area
		{
			get
			{
				return this.m_Area;
			}
			set
			{
				this.m_Area = value;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600002F RID: 47 RVA: 0x000024B9 File Offset: 0x000006B9
		public static List<NavMeshModifierVolume> activeModifiers
		{
			get
			{
				return NavMeshModifierVolume.s_NavMeshModifiers;
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000024C0 File Offset: 0x000006C0
		private void OnEnable()
		{
			if (!NavMeshModifierVolume.s_NavMeshModifiers.Contains(this))
			{
				NavMeshModifierVolume.s_NavMeshModifiers.Add(this);
			}
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000024DA File Offset: 0x000006DA
		private void OnDisable()
		{
			NavMeshModifierVolume.s_NavMeshModifiers.Remove(this);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000024E8 File Offset: 0x000006E8
		public bool AffectsAgentType(int agentTypeID)
		{
			return this.m_AffectedAgents.Count != 0 && (this.m_AffectedAgents[0] == -1 || this.m_AffectedAgents.IndexOf(agentTypeID) != -1);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x0000251C File Offset: 0x0000071C
		public NavMeshModifierVolume()
		{
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002578 File Offset: 0x00000778
		// Note: this type is marked as 'beforefieldinit'.
		static NavMeshModifierVolume()
		{
		}

		// Token: 0x04000016 RID: 22
		[SerializeField]
		private Vector3 m_Size = new Vector3(4f, 3f, 4f);

		// Token: 0x04000017 RID: 23
		[SerializeField]
		private Vector3 m_Center = new Vector3(0f, 1f, 0f);

		// Token: 0x04000018 RID: 24
		[SerializeField]
		private int m_Area;

		// Token: 0x04000019 RID: 25
		[SerializeField]
		private List<int> m_AffectedAgents = new List<int>(new int[]
		{
			-1
		});

		// Token: 0x0400001A RID: 26
		private static readonly List<NavMeshModifierVolume> s_NavMeshModifiers = new List<NavMeshModifierVolume>();
	}
}
