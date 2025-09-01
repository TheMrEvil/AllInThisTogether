using System;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.AI.Navigation
{
	// Token: 0x02000004 RID: 4
	[ExecuteInEditMode]
	[AddComponentMenu("Navigation/NavMeshModifier", 32)]
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.ai.navigation@1.0/manual/NavMeshModifier.html")]
	public class NavMeshModifier : MonoBehaviour
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600001D RID: 29 RVA: 0x000023C7 File Offset: 0x000005C7
		// (set) Token: 0x0600001E RID: 30 RVA: 0x000023CF File Offset: 0x000005CF
		public bool overrideArea
		{
			get
			{
				return this.m_OverrideArea;
			}
			set
			{
				this.m_OverrideArea = value;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600001F RID: 31 RVA: 0x000023D8 File Offset: 0x000005D8
		// (set) Token: 0x06000020 RID: 32 RVA: 0x000023E0 File Offset: 0x000005E0
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

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000021 RID: 33 RVA: 0x000023E9 File Offset: 0x000005E9
		// (set) Token: 0x06000022 RID: 34 RVA: 0x000023F1 File Offset: 0x000005F1
		public bool ignoreFromBuild
		{
			get
			{
				return this.m_IgnoreFromBuild;
			}
			set
			{
				this.m_IgnoreFromBuild = value;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000023 RID: 35 RVA: 0x000023FA File Offset: 0x000005FA
		public static List<NavMeshModifier> activeModifiers
		{
			get
			{
				return NavMeshModifier.s_NavMeshModifiers;
			}
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002401 File Offset: 0x00000601
		private void OnEnable()
		{
			if (!NavMeshModifier.s_NavMeshModifiers.Contains(this))
			{
				NavMeshModifier.s_NavMeshModifiers.Add(this);
			}
		}

		// Token: 0x06000025 RID: 37 RVA: 0x0000241B File Offset: 0x0000061B
		private void OnDisable()
		{
			NavMeshModifier.s_NavMeshModifiers.Remove(this);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002429 File Offset: 0x00000629
		public bool AffectsAgentType(int agentTypeID)
		{
			return this.m_AffectedAgents.Count != 0 && (this.m_AffectedAgents[0] == -1 || this.m_AffectedAgents.IndexOf(agentTypeID) != -1);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x0000245D File Offset: 0x0000065D
		public NavMeshModifier()
		{
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000247A File Offset: 0x0000067A
		// Note: this type is marked as 'beforefieldinit'.
		static NavMeshModifier()
		{
		}

		// Token: 0x04000011 RID: 17
		[SerializeField]
		private bool m_OverrideArea;

		// Token: 0x04000012 RID: 18
		[SerializeField]
		private int m_Area;

		// Token: 0x04000013 RID: 19
		[SerializeField]
		private bool m_IgnoreFromBuild;

		// Token: 0x04000014 RID: 20
		[SerializeField]
		private List<int> m_AffectedAgents = new List<int>(new int[]
		{
			-1
		});

		// Token: 0x04000015 RID: 21
		private static readonly List<NavMeshModifier> s_NavMeshModifiers = new List<NavMeshModifier>();
	}
}
