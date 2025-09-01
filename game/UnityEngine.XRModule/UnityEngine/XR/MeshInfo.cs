using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.XR
{
	// Token: 0x0200002F RID: 47
	[NativeHeader("Modules/XR/Subsystems/Meshing/XRMeshBindings.h")]
	[UsedByNativeCode]
	public struct MeshInfo : IEquatable<MeshInfo>
	{
		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600014F RID: 335 RVA: 0x000049CF File Offset: 0x00002BCF
		// (set) Token: 0x06000150 RID: 336 RVA: 0x000049D7 File Offset: 0x00002BD7
		public MeshId MeshId
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<MeshId>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<MeshId>k__BackingField = value;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000151 RID: 337 RVA: 0x000049E0 File Offset: 0x00002BE0
		// (set) Token: 0x06000152 RID: 338 RVA: 0x000049E8 File Offset: 0x00002BE8
		public MeshChangeState ChangeState
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<ChangeState>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ChangeState>k__BackingField = value;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000153 RID: 339 RVA: 0x000049F1 File Offset: 0x00002BF1
		// (set) Token: 0x06000154 RID: 340 RVA: 0x000049F9 File Offset: 0x00002BF9
		public int PriorityHint
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<PriorityHint>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<PriorityHint>k__BackingField = value;
			}
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00004A04 File Offset: 0x00002C04
		public override bool Equals(object obj)
		{
			bool flag = !(obj is MeshInfo);
			return !flag && this.Equals((MeshInfo)obj);
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00004A38 File Offset: 0x00002C38
		public bool Equals(MeshInfo other)
		{
			return this.MeshId.Equals(other.MeshId) && this.ChangeState.Equals(other.ChangeState) && this.PriorityHint.Equals(other.PriorityHint);
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00004A9C File Offset: 0x00002C9C
		public static bool operator ==(MeshInfo lhs, MeshInfo rhs)
		{
			return lhs.Equals(rhs);
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00004AB8 File Offset: 0x00002CB8
		public static bool operator !=(MeshInfo lhs, MeshInfo rhs)
		{
			return !lhs.Equals(rhs);
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00004AD8 File Offset: 0x00002CD8
		public override int GetHashCode()
		{
			return HashCodeHelper.Combine(this.MeshId.GetHashCode(), ((int)this.ChangeState).GetHashCode(), this.PriorityHint.GetHashCode());
		}

		// Token: 0x04000109 RID: 265
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private MeshId <MeshId>k__BackingField;

		// Token: 0x0400010A RID: 266
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private MeshChangeState <ChangeState>k__BackingField;

		// Token: 0x0400010B RID: 267
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int <PriorityHint>k__BackingField;
	}
}
