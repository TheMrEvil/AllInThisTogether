using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.AI
{
	// Token: 0x02000008 RID: 8
	[MovedFrom("UnityEngine")]
	[NativeHeader("Modules/AI/NavMesh/NavMesh.bindings.h")]
	[NativeHeader("Modules/AI/Components/NavMeshAgent.bindings.h")]
	public sealed class NavMeshAgent : Behaviour
	{
		// Token: 0x0600001A RID: 26 RVA: 0x000022F5 File Offset: 0x000004F5
		public bool SetDestination(Vector3 target)
		{
			return this.SetDestination_Injected(ref target);
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002300 File Offset: 0x00000500
		// (set) Token: 0x0600001C RID: 28 RVA: 0x00002316 File Offset: 0x00000516
		public Vector3 destination
		{
			get
			{
				Vector3 result;
				this.get_destination_Injected(out result);
				return result;
			}
			set
			{
				this.set_destination_Injected(ref value);
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600001D RID: 29
		// (set) Token: 0x0600001E RID: 30
		public extern float stoppingDistance { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00002320 File Offset: 0x00000520
		// (set) Token: 0x06000020 RID: 32 RVA: 0x00002336 File Offset: 0x00000536
		public Vector3 velocity
		{
			get
			{
				Vector3 result;
				this.get_velocity_Injected(out result);
				return result;
			}
			set
			{
				this.set_velocity_Injected(ref value);
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00002340 File Offset: 0x00000540
		// (set) Token: 0x06000022 RID: 34 RVA: 0x00002356 File Offset: 0x00000556
		[NativeProperty("Position")]
		public Vector3 nextPosition
		{
			get
			{
				Vector3 result;
				this.get_nextPosition_Injected(out result);
				return result;
			}
			set
			{
				this.set_nextPosition_Injected(ref value);
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00002360 File Offset: 0x00000560
		public Vector3 steeringTarget
		{
			get
			{
				Vector3 result;
				this.get_steeringTarget_Injected(out result);
				return result;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00002378 File Offset: 0x00000578
		public Vector3 desiredVelocity
		{
			get
			{
				Vector3 result;
				this.get_desiredVelocity_Injected(out result);
				return result;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000025 RID: 37
		public extern float remainingDistance { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000026 RID: 38
		// (set) Token: 0x06000027 RID: 39
		public extern float baseOffset { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000028 RID: 40
		public extern bool isOnOffMeshLink { [NativeName("IsOnOffMeshLink")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06000029 RID: 41
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void ActivateCurrentOffMeshLink(bool activated);

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600002A RID: 42 RVA: 0x0000238E File Offset: 0x0000058E
		public OffMeshLinkData currentOffMeshLinkData
		{
			get
			{
				return this.GetCurrentOffMeshLinkDataInternal();
			}
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002398 File Offset: 0x00000598
		[FreeFunction("NavMeshAgentScriptBindings::GetCurrentOffMeshLinkDataInternal", HasExplicitThis = true)]
		internal OffMeshLinkData GetCurrentOffMeshLinkDataInternal()
		{
			OffMeshLinkData result;
			this.GetCurrentOffMeshLinkDataInternal_Injected(out result);
			return result;
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600002C RID: 44 RVA: 0x000023AE File Offset: 0x000005AE
		public OffMeshLinkData nextOffMeshLinkData
		{
			get
			{
				return this.GetNextOffMeshLinkDataInternal();
			}
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000023B8 File Offset: 0x000005B8
		[FreeFunction("NavMeshAgentScriptBindings::GetNextOffMeshLinkDataInternal", HasExplicitThis = true)]
		internal OffMeshLinkData GetNextOffMeshLinkDataInternal()
		{
			OffMeshLinkData result;
			this.GetNextOffMeshLinkDataInternal_Injected(out result);
			return result;
		}

		// Token: 0x0600002E RID: 46
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void CompleteOffMeshLink();

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600002F RID: 47
		// (set) Token: 0x06000030 RID: 48
		public extern bool autoTraverseOffMeshLink { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000031 RID: 49
		// (set) Token: 0x06000032 RID: 50
		public extern bool autoBraking { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000033 RID: 51
		// (set) Token: 0x06000034 RID: 52
		public extern bool autoRepath { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000035 RID: 53
		public extern bool hasPath { [NativeName("HasPath")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000036 RID: 54
		public extern bool pathPending { [NativeName("PathPending")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000037 RID: 55
		public extern bool isPathStale { [NativeName("IsPathStale")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000038 RID: 56
		public extern NavMeshPathStatus pathStatus { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000039 RID: 57 RVA: 0x000023D0 File Offset: 0x000005D0
		[NativeProperty("EndPositionOfCurrentPath")]
		public Vector3 pathEndPosition
		{
			get
			{
				Vector3 result;
				this.get_pathEndPosition_Injected(out result);
				return result;
			}
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000023E6 File Offset: 0x000005E6
		public bool Warp(Vector3 newPosition)
		{
			return this.Warp_Injected(ref newPosition);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000023F0 File Offset: 0x000005F0
		public void Move(Vector3 offset)
		{
			this.Move_Injected(ref offset);
		}

		// Token: 0x0600003C RID: 60
		[Obsolete("Set isStopped to true instead.")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Stop();

		// Token: 0x0600003D RID: 61 RVA: 0x000023FA File Offset: 0x000005FA
		[Obsolete("Set isStopped to true instead.")]
		public void Stop(bool stopUpdates)
		{
			this.Stop();
		}

		// Token: 0x0600003E RID: 62
		[Obsolete("Set isStopped to false instead.")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Resume();

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600003F RID: 63
		// (set) Token: 0x06000040 RID: 64
		public extern bool isStopped { [FreeFunction("NavMeshAgentScriptBindings::GetIsStopped", HasExplicitThis = true)] [MethodImpl(MethodImplOptions.InternalCall)] get; [FreeFunction("NavMeshAgentScriptBindings::SetIsStopped", HasExplicitThis = true)] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06000041 RID: 65
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void ResetPath();

		// Token: 0x06000042 RID: 66
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool SetPath([NotNull("ArgumentNullException")] NavMeshPath path);

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00002404 File Offset: 0x00000604
		// (set) Token: 0x06000044 RID: 68 RVA: 0x00002428 File Offset: 0x00000628
		public NavMeshPath path
		{
			get
			{
				NavMeshPath navMeshPath = new NavMeshPath();
				this.CopyPathTo(navMeshPath);
				return navMeshPath;
			}
			set
			{
				bool flag = value == null;
				if (flag)
				{
					throw new NullReferenceException();
				}
				this.SetPath(value);
			}
		}

		// Token: 0x06000045 RID: 69
		[NativeMethod("CopyPath")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void CopyPathTo([NotNull("ArgumentNullException")] NavMeshPath path);

		// Token: 0x06000046 RID: 70
		[NativeName("DistanceToEdge")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool FindClosestEdge(out NavMeshHit hit);

		// Token: 0x06000047 RID: 71 RVA: 0x0000244C File Offset: 0x0000064C
		public bool Raycast(Vector3 targetPosition, out NavMeshHit hit)
		{
			return this.Raycast_Injected(ref targetPosition, out hit);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002458 File Offset: 0x00000658
		public bool CalculatePath(Vector3 targetPosition, NavMeshPath path)
		{
			path.ClearCorners();
			return this.CalculatePathInternal(targetPosition, path);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002479 File Offset: 0x00000679
		[FreeFunction("NavMeshAgentScriptBindings::CalculatePathInternal", HasExplicitThis = true)]
		private bool CalculatePathInternal(Vector3 targetPosition, [NotNull("ArgumentNullException")] NavMeshPath path)
		{
			return this.CalculatePathInternal_Injected(ref targetPosition, path);
		}

		// Token: 0x0600004A RID: 74
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool SamplePathPosition(int areaMask, float maxDistance, out NavMeshHit hit);

		// Token: 0x0600004B RID: 75
		[Obsolete("Use SetAreaCost instead.")]
		[NativeMethod("SetAreaCost")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetLayerCost(int layer, float cost);

		// Token: 0x0600004C RID: 76
		[Obsolete("Use GetAreaCost instead.")]
		[NativeMethod("GetAreaCost")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern float GetLayerCost(int layer);

		// Token: 0x0600004D RID: 77
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetAreaCost(int areaIndex, float areaCost);

		// Token: 0x0600004E RID: 78
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern float GetAreaCost(int areaIndex);

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00002484 File Offset: 0x00000684
		public Object navMeshOwner
		{
			get
			{
				return this.GetOwnerInternal();
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000050 RID: 80
		// (set) Token: 0x06000051 RID: 81
		public extern int agentTypeID { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06000052 RID: 82
		[NativeName("GetCurrentPolygonOwner")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Object GetOwnerInternal();

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000053 RID: 83 RVA: 0x0000248C File Offset: 0x0000068C
		// (set) Token: 0x06000054 RID: 84 RVA: 0x000024A4 File Offset: 0x000006A4
		[Obsolete("Use areaMask instead.")]
		public int walkableMask
		{
			get
			{
				return this.areaMask;
			}
			set
			{
				this.areaMask = value;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000055 RID: 85
		// (set) Token: 0x06000056 RID: 86
		public extern int areaMask { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000057 RID: 87
		// (set) Token: 0x06000058 RID: 88
		public extern float speed { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000059 RID: 89
		// (set) Token: 0x0600005A RID: 90
		public extern float angularSpeed { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600005B RID: 91
		// (set) Token: 0x0600005C RID: 92
		public extern float acceleration { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600005D RID: 93
		// (set) Token: 0x0600005E RID: 94
		public extern bool updatePosition { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600005F RID: 95
		// (set) Token: 0x06000060 RID: 96
		public extern bool updateRotation { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000061 RID: 97
		// (set) Token: 0x06000062 RID: 98
		public extern bool updateUpAxis { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000063 RID: 99
		// (set) Token: 0x06000064 RID: 100
		public extern float radius { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000065 RID: 101
		// (set) Token: 0x06000066 RID: 102
		public extern float height { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000067 RID: 103
		// (set) Token: 0x06000068 RID: 104
		public extern ObstacleAvoidanceType obstacleAvoidanceType { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000069 RID: 105
		// (set) Token: 0x0600006A RID: 106
		public extern int avoidancePriority { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600006B RID: 107
		public extern bool isOnNavMesh { [NativeName("InCrowdSystem")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x0600006C RID: 108 RVA: 0x000024AF File Offset: 0x000006AF
		public NavMeshAgent()
		{
		}

		// Token: 0x0600006D RID: 109
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool SetDestination_Injected(ref Vector3 target);

		// Token: 0x0600006E RID: 110
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_destination_Injected(out Vector3 ret);

		// Token: 0x0600006F RID: 111
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_destination_Injected(ref Vector3 value);

		// Token: 0x06000070 RID: 112
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_velocity_Injected(out Vector3 ret);

		// Token: 0x06000071 RID: 113
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_velocity_Injected(ref Vector3 value);

		// Token: 0x06000072 RID: 114
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_nextPosition_Injected(out Vector3 ret);

		// Token: 0x06000073 RID: 115
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_nextPosition_Injected(ref Vector3 value);

		// Token: 0x06000074 RID: 116
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_steeringTarget_Injected(out Vector3 ret);

		// Token: 0x06000075 RID: 117
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_desiredVelocity_Injected(out Vector3 ret);

		// Token: 0x06000076 RID: 118
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetCurrentOffMeshLinkDataInternal_Injected(out OffMeshLinkData ret);

		// Token: 0x06000077 RID: 119
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetNextOffMeshLinkDataInternal_Injected(out OffMeshLinkData ret);

		// Token: 0x06000078 RID: 120
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_pathEndPosition_Injected(out Vector3 ret);

		// Token: 0x06000079 RID: 121
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool Warp_Injected(ref Vector3 newPosition);

		// Token: 0x0600007A RID: 122
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Move_Injected(ref Vector3 offset);

		// Token: 0x0600007B RID: 123
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool Raycast_Injected(ref Vector3 targetPosition, out NavMeshHit hit);

		// Token: 0x0600007C RID: 124
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool CalculatePathInternal_Injected(ref Vector3 targetPosition, NavMeshPath path);
	}
}
