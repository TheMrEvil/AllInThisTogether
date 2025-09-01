using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.AI
{
	// Token: 0x0200001C RID: 28
	[NativeHeader("Modules/AI/Public/NavMeshBuildSettings.h")]
	public struct NavMeshBuildSettings
	{
		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600014A RID: 330 RVA: 0x00002E94 File Offset: 0x00001094
		// (set) Token: 0x0600014B RID: 331 RVA: 0x00002EAC File Offset: 0x000010AC
		public int agentTypeID
		{
			get
			{
				return this.m_AgentTypeID;
			}
			set
			{
				this.m_AgentTypeID = value;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600014C RID: 332 RVA: 0x00002EB8 File Offset: 0x000010B8
		// (set) Token: 0x0600014D RID: 333 RVA: 0x00002ED0 File Offset: 0x000010D0
		public float agentRadius
		{
			get
			{
				return this.m_AgentRadius;
			}
			set
			{
				this.m_AgentRadius = value;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x0600014E RID: 334 RVA: 0x00002EDC File Offset: 0x000010DC
		// (set) Token: 0x0600014F RID: 335 RVA: 0x00002EF4 File Offset: 0x000010F4
		public float agentHeight
		{
			get
			{
				return this.m_AgentHeight;
			}
			set
			{
				this.m_AgentHeight = value;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000150 RID: 336 RVA: 0x00002F00 File Offset: 0x00001100
		// (set) Token: 0x06000151 RID: 337 RVA: 0x00002F18 File Offset: 0x00001118
		public float agentSlope
		{
			get
			{
				return this.m_AgentSlope;
			}
			set
			{
				this.m_AgentSlope = value;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000152 RID: 338 RVA: 0x00002F24 File Offset: 0x00001124
		// (set) Token: 0x06000153 RID: 339 RVA: 0x00002F3C File Offset: 0x0000113C
		public float agentClimb
		{
			get
			{
				return this.m_AgentClimb;
			}
			set
			{
				this.m_AgentClimb = value;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000154 RID: 340 RVA: 0x00002F48 File Offset: 0x00001148
		// (set) Token: 0x06000155 RID: 341 RVA: 0x00002F60 File Offset: 0x00001160
		public float minRegionArea
		{
			get
			{
				return this.m_MinRegionArea;
			}
			set
			{
				this.m_MinRegionArea = value;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000156 RID: 342 RVA: 0x00002F6C File Offset: 0x0000116C
		// (set) Token: 0x06000157 RID: 343 RVA: 0x00002F87 File Offset: 0x00001187
		public bool overrideVoxelSize
		{
			get
			{
				return this.m_OverrideVoxelSize != 0;
			}
			set
			{
				this.m_OverrideVoxelSize = (value ? 1 : 0);
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000158 RID: 344 RVA: 0x00002F98 File Offset: 0x00001198
		// (set) Token: 0x06000159 RID: 345 RVA: 0x00002FB0 File Offset: 0x000011B0
		public float voxelSize
		{
			get
			{
				return this.m_VoxelSize;
			}
			set
			{
				this.m_VoxelSize = value;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600015A RID: 346 RVA: 0x00002FBC File Offset: 0x000011BC
		// (set) Token: 0x0600015B RID: 347 RVA: 0x00002FD7 File Offset: 0x000011D7
		public bool overrideTileSize
		{
			get
			{
				return this.m_OverrideTileSize != 0;
			}
			set
			{
				this.m_OverrideTileSize = (value ? 1 : 0);
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600015C RID: 348 RVA: 0x00002FE8 File Offset: 0x000011E8
		// (set) Token: 0x0600015D RID: 349 RVA: 0x00003000 File Offset: 0x00001200
		public int tileSize
		{
			get
			{
				return this.m_TileSize;
			}
			set
			{
				this.m_TileSize = value;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600015E RID: 350 RVA: 0x0000300C File Offset: 0x0000120C
		// (set) Token: 0x0600015F RID: 351 RVA: 0x00003024 File Offset: 0x00001224
		public uint maxJobWorkers
		{
			get
			{
				return this.m_MaxJobWorkers;
			}
			set
			{
				this.m_MaxJobWorkers = value;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000160 RID: 352 RVA: 0x00003030 File Offset: 0x00001230
		// (set) Token: 0x06000161 RID: 353 RVA: 0x0000304B File Offset: 0x0000124B
		public bool preserveTilesOutsideBounds
		{
			get
			{
				return this.m_PreserveTilesOutsideBounds != 0;
			}
			set
			{
				this.m_PreserveTilesOutsideBounds = (value ? 1 : 0);
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000162 RID: 354 RVA: 0x0000305C File Offset: 0x0000125C
		// (set) Token: 0x06000163 RID: 355 RVA: 0x00003074 File Offset: 0x00001274
		public NavMeshBuildDebugSettings debug
		{
			get
			{
				return this.m_Debug;
			}
			set
			{
				this.m_Debug = value;
			}
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00003080 File Offset: 0x00001280
		public string[] ValidationReport(Bounds buildBounds)
		{
			return NavMeshBuildSettings.InternalValidationReport(this, buildBounds);
		}

		// Token: 0x06000165 RID: 357 RVA: 0x0000309E File Offset: 0x0000129E
		[FreeFunction]
		[NativeHeader("Modules/AI/Public/NavMeshBuildSettings.h")]
		private static string[] InternalValidationReport(NavMeshBuildSettings buildSettings, Bounds buildBounds)
		{
			return NavMeshBuildSettings.InternalValidationReport_Injected(ref buildSettings, ref buildBounds);
		}

		// Token: 0x06000166 RID: 358
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string[] InternalValidationReport_Injected(ref NavMeshBuildSettings buildSettings, ref Bounds buildBounds);

		// Token: 0x0400004F RID: 79
		private int m_AgentTypeID;

		// Token: 0x04000050 RID: 80
		private float m_AgentRadius;

		// Token: 0x04000051 RID: 81
		private float m_AgentHeight;

		// Token: 0x04000052 RID: 82
		private float m_AgentSlope;

		// Token: 0x04000053 RID: 83
		private float m_AgentClimb;

		// Token: 0x04000054 RID: 84
		private float m_LedgeDropHeight;

		// Token: 0x04000055 RID: 85
		private float m_MaxJumpAcrossDistance;

		// Token: 0x04000056 RID: 86
		private float m_MinRegionArea;

		// Token: 0x04000057 RID: 87
		private int m_OverrideVoxelSize;

		// Token: 0x04000058 RID: 88
		private float m_VoxelSize;

		// Token: 0x04000059 RID: 89
		private int m_OverrideTileSize;

		// Token: 0x0400005A RID: 90
		private int m_TileSize;

		// Token: 0x0400005B RID: 91
		private int m_AccuratePlacement;

		// Token: 0x0400005C RID: 92
		private uint m_MaxJobWorkers;

		// Token: 0x0400005D RID: 93
		private int m_PreserveTilesOutsideBounds;

		// Token: 0x0400005E RID: 94
		private NavMeshBuildDebugSettings m_Debug;
	}
}
