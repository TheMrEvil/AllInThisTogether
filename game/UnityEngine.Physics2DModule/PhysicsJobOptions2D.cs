using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200001E RID: 30
	[NativeClass("PhysicsJobOptions2D", "struct PhysicsJobOptions2D;")]
	[RequiredByNativeCode(Optional = true, GenerateProxy = true)]
	[NativeHeader("Modules/Physics2D/Public/Physics2DSettings.h")]
	public struct PhysicsJobOptions2D
	{
		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600025D RID: 605 RVA: 0x00006EBC File Offset: 0x000050BC
		// (set) Token: 0x0600025E RID: 606 RVA: 0x00006ED4 File Offset: 0x000050D4
		public bool useMultithreading
		{
			get
			{
				return this.m_UseMultithreading;
			}
			set
			{
				this.m_UseMultithreading = value;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600025F RID: 607 RVA: 0x00006EE0 File Offset: 0x000050E0
		// (set) Token: 0x06000260 RID: 608 RVA: 0x00006EF8 File Offset: 0x000050F8
		public bool useConsistencySorting
		{
			get
			{
				return this.m_UseConsistencySorting;
			}
			set
			{
				this.m_UseConsistencySorting = value;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000261 RID: 609 RVA: 0x00006F04 File Offset: 0x00005104
		// (set) Token: 0x06000262 RID: 610 RVA: 0x00006F1C File Offset: 0x0000511C
		public int interpolationPosesPerJob
		{
			get
			{
				return this.m_InterpolationPosesPerJob;
			}
			set
			{
				this.m_InterpolationPosesPerJob = value;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000263 RID: 611 RVA: 0x00006F28 File Offset: 0x00005128
		// (set) Token: 0x06000264 RID: 612 RVA: 0x00006F40 File Offset: 0x00005140
		public int newContactsPerJob
		{
			get
			{
				return this.m_NewContactsPerJob;
			}
			set
			{
				this.m_NewContactsPerJob = value;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000265 RID: 613 RVA: 0x00006F4C File Offset: 0x0000514C
		// (set) Token: 0x06000266 RID: 614 RVA: 0x00006F64 File Offset: 0x00005164
		public int collideContactsPerJob
		{
			get
			{
				return this.m_CollideContactsPerJob;
			}
			set
			{
				this.m_CollideContactsPerJob = value;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000267 RID: 615 RVA: 0x00006F70 File Offset: 0x00005170
		// (set) Token: 0x06000268 RID: 616 RVA: 0x00006F88 File Offset: 0x00005188
		public int clearFlagsPerJob
		{
			get
			{
				return this.m_ClearFlagsPerJob;
			}
			set
			{
				this.m_ClearFlagsPerJob = value;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000269 RID: 617 RVA: 0x00006F94 File Offset: 0x00005194
		// (set) Token: 0x0600026A RID: 618 RVA: 0x00006FAC File Offset: 0x000051AC
		public int clearBodyForcesPerJob
		{
			get
			{
				return this.m_ClearBodyForcesPerJob;
			}
			set
			{
				this.m_ClearBodyForcesPerJob = value;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600026B RID: 619 RVA: 0x00006FB8 File Offset: 0x000051B8
		// (set) Token: 0x0600026C RID: 620 RVA: 0x00006FD0 File Offset: 0x000051D0
		public int syncDiscreteFixturesPerJob
		{
			get
			{
				return this.m_SyncDiscreteFixturesPerJob;
			}
			set
			{
				this.m_SyncDiscreteFixturesPerJob = value;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600026D RID: 621 RVA: 0x00006FDC File Offset: 0x000051DC
		// (set) Token: 0x0600026E RID: 622 RVA: 0x00006FF4 File Offset: 0x000051F4
		public int syncContinuousFixturesPerJob
		{
			get
			{
				return this.m_SyncContinuousFixturesPerJob;
			}
			set
			{
				this.m_SyncContinuousFixturesPerJob = value;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600026F RID: 623 RVA: 0x00007000 File Offset: 0x00005200
		// (set) Token: 0x06000270 RID: 624 RVA: 0x00007018 File Offset: 0x00005218
		public int findNearestContactsPerJob
		{
			get
			{
				return this.m_FindNearestContactsPerJob;
			}
			set
			{
				this.m_FindNearestContactsPerJob = value;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000271 RID: 625 RVA: 0x00007024 File Offset: 0x00005224
		// (set) Token: 0x06000272 RID: 626 RVA: 0x0000703C File Offset: 0x0000523C
		public int updateTriggerContactsPerJob
		{
			get
			{
				return this.m_UpdateTriggerContactsPerJob;
			}
			set
			{
				this.m_UpdateTriggerContactsPerJob = value;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000273 RID: 627 RVA: 0x00007048 File Offset: 0x00005248
		// (set) Token: 0x06000274 RID: 628 RVA: 0x00007060 File Offset: 0x00005260
		public int islandSolverCostThreshold
		{
			get
			{
				return this.m_IslandSolverCostThreshold;
			}
			set
			{
				this.m_IslandSolverCostThreshold = value;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000275 RID: 629 RVA: 0x0000706C File Offset: 0x0000526C
		// (set) Token: 0x06000276 RID: 630 RVA: 0x00007084 File Offset: 0x00005284
		public int islandSolverBodyCostScale
		{
			get
			{
				return this.m_IslandSolverBodyCostScale;
			}
			set
			{
				this.m_IslandSolverBodyCostScale = value;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000277 RID: 631 RVA: 0x00007090 File Offset: 0x00005290
		// (set) Token: 0x06000278 RID: 632 RVA: 0x000070A8 File Offset: 0x000052A8
		public int islandSolverContactCostScale
		{
			get
			{
				return this.m_IslandSolverContactCostScale;
			}
			set
			{
				this.m_IslandSolverContactCostScale = value;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000279 RID: 633 RVA: 0x000070B4 File Offset: 0x000052B4
		// (set) Token: 0x0600027A RID: 634 RVA: 0x000070CC File Offset: 0x000052CC
		public int islandSolverJointCostScale
		{
			get
			{
				return this.m_IslandSolverJointCostScale;
			}
			set
			{
				this.m_IslandSolverJointCostScale = value;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600027B RID: 635 RVA: 0x000070D8 File Offset: 0x000052D8
		// (set) Token: 0x0600027C RID: 636 RVA: 0x000070F0 File Offset: 0x000052F0
		public int islandSolverBodiesPerJob
		{
			get
			{
				return this.m_IslandSolverBodiesPerJob;
			}
			set
			{
				this.m_IslandSolverBodiesPerJob = value;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600027D RID: 637 RVA: 0x000070FC File Offset: 0x000052FC
		// (set) Token: 0x0600027E RID: 638 RVA: 0x00007114 File Offset: 0x00005314
		public int islandSolverContactsPerJob
		{
			get
			{
				return this.m_IslandSolverContactsPerJob;
			}
			set
			{
				this.m_IslandSolverContactsPerJob = value;
			}
		}

		// Token: 0x0400007E RID: 126
		private bool m_UseMultithreading;

		// Token: 0x0400007F RID: 127
		private bool m_UseConsistencySorting;

		// Token: 0x04000080 RID: 128
		private int m_InterpolationPosesPerJob;

		// Token: 0x04000081 RID: 129
		private int m_NewContactsPerJob;

		// Token: 0x04000082 RID: 130
		private int m_CollideContactsPerJob;

		// Token: 0x04000083 RID: 131
		private int m_ClearFlagsPerJob;

		// Token: 0x04000084 RID: 132
		private int m_ClearBodyForcesPerJob;

		// Token: 0x04000085 RID: 133
		private int m_SyncDiscreteFixturesPerJob;

		// Token: 0x04000086 RID: 134
		private int m_SyncContinuousFixturesPerJob;

		// Token: 0x04000087 RID: 135
		private int m_FindNearestContactsPerJob;

		// Token: 0x04000088 RID: 136
		private int m_UpdateTriggerContactsPerJob;

		// Token: 0x04000089 RID: 137
		private int m_IslandSolverCostThreshold;

		// Token: 0x0400008A RID: 138
		private int m_IslandSolverBodyCostScale;

		// Token: 0x0400008B RID: 139
		private int m_IslandSolverContactCostScale;

		// Token: 0x0400008C RID: 140
		private int m_IslandSolverJointCostScale;

		// Token: 0x0400008D RID: 141
		private int m_IslandSolverBodiesPerJob;

		// Token: 0x0400008E RID: 142
		private int m_IslandSolverContactsPerJob;
	}
}
