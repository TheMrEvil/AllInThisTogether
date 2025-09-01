using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.AI
{
	// Token: 0x0200000A RID: 10
	[NativeHeader("Modules/AI/Components/NavMeshObstacle.bindings.h")]
	[MovedFrom("UnityEngine")]
	public sealed class NavMeshObstacle : Behaviour
	{
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600007D RID: 125
		// (set) Token: 0x0600007E RID: 126
		public extern float height { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600007F RID: 127
		// (set) Token: 0x06000080 RID: 128
		public extern float radius { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000081 RID: 129 RVA: 0x000024B8 File Offset: 0x000006B8
		// (set) Token: 0x06000082 RID: 130 RVA: 0x000024CE File Offset: 0x000006CE
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

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000083 RID: 131
		// (set) Token: 0x06000084 RID: 132
		public extern bool carving { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000085 RID: 133
		// (set) Token: 0x06000086 RID: 134
		public extern bool carveOnlyStationary { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000087 RID: 135
		// (set) Token: 0x06000088 RID: 136
		[NativeProperty("MoveThreshold")]
		public extern float carvingMoveThreshold { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000089 RID: 137
		// (set) Token: 0x0600008A RID: 138
		[NativeProperty("TimeToStationary")]
		public extern float carvingTimeToStationary { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600008B RID: 139
		// (set) Token: 0x0600008C RID: 140
		public extern NavMeshObstacleShape shape { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600008D RID: 141 RVA: 0x000024D8 File Offset: 0x000006D8
		// (set) Token: 0x0600008E RID: 142 RVA: 0x000024EE File Offset: 0x000006EE
		public Vector3 center
		{
			get
			{
				Vector3 result;
				this.get_center_Injected(out result);
				return result;
			}
			set
			{
				this.set_center_Injected(ref value);
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600008F RID: 143 RVA: 0x000024F8 File Offset: 0x000006F8
		// (set) Token: 0x06000090 RID: 144 RVA: 0x0000250E File Offset: 0x0000070E
		public Vector3 size
		{
			[FreeFunction("NavMeshObstacleScriptBindings::GetSize", HasExplicitThis = true)]
			get
			{
				Vector3 result;
				this.get_size_Injected(out result);
				return result;
			}
			[FreeFunction("NavMeshObstacleScriptBindings::SetSize", HasExplicitThis = true)]
			set
			{
				this.set_size_Injected(ref value);
			}
		}

		// Token: 0x06000091 RID: 145
		[FreeFunction("NavMeshObstacleScriptBindings::FitExtents", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void FitExtents();

		// Token: 0x06000092 RID: 146 RVA: 0x000024AF File Offset: 0x000006AF
		public NavMeshObstacle()
		{
		}

		// Token: 0x06000093 RID: 147
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_velocity_Injected(out Vector3 ret);

		// Token: 0x06000094 RID: 148
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_velocity_Injected(ref Vector3 value);

		// Token: 0x06000095 RID: 149
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_center_Injected(out Vector3 ret);

		// Token: 0x06000096 RID: 150
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_center_Injected(ref Vector3 value);

		// Token: 0x06000097 RID: 151
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_size_Injected(out Vector3 ret);

		// Token: 0x06000098 RID: 152
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_size_Injected(ref Vector3 value);
	}
}
