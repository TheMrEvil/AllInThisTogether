using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.XR
{
	// Token: 0x02000030 RID: 48
	[NativeHeader("Modules/XR/Subsystems/Meshing/XRMeshBindings.h")]
	[UsedByNativeCode]
	public readonly struct MeshTransform : IEquatable<MeshTransform>
	{
		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600015A RID: 346 RVA: 0x00004B1A File Offset: 0x00002D1A
		public MeshId MeshId
		{
			[CompilerGenerated]
			get
			{
				return this.<MeshId>k__BackingField;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600015B RID: 347 RVA: 0x00004B22 File Offset: 0x00002D22
		public ulong Timestamp
		{
			[CompilerGenerated]
			get
			{
				return this.<Timestamp>k__BackingField;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600015C RID: 348 RVA: 0x00004B2A File Offset: 0x00002D2A
		public Vector3 Position
		{
			[CompilerGenerated]
			get
			{
				return this.<Position>k__BackingField;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600015D RID: 349 RVA: 0x00004B32 File Offset: 0x00002D32
		public Quaternion Rotation
		{
			[CompilerGenerated]
			get
			{
				return this.<Rotation>k__BackingField;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600015E RID: 350 RVA: 0x00004B3A File Offset: 0x00002D3A
		public Vector3 Scale
		{
			[CompilerGenerated]
			get
			{
				return this.<Scale>k__BackingField;
			}
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00004B42 File Offset: 0x00002D42
		public MeshTransform(in MeshId meshId, ulong timestamp, in Vector3 position, in Quaternion rotation, in Vector3 scale)
		{
			this.MeshId = meshId;
			this.Timestamp = timestamp;
			this.Position = position;
			this.Rotation = rotation;
			this.Scale = scale;
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00004B80 File Offset: 0x00002D80
		public override bool Equals(object obj)
		{
			bool result;
			if (obj is MeshTransform)
			{
				MeshTransform other = (MeshTransform)obj;
				result = this.Equals(other);
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00004BA8 File Offset: 0x00002DA8
		public bool Equals(MeshTransform other)
		{
			return this.MeshId.Equals(other.MeshId) && this.Timestamp == other.Timestamp && this.Position.Equals(other.Position) && this.Rotation.Equals(other.Rotation) && this.Scale.Equals(other.Scale);
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00004C21 File Offset: 0x00002E21
		public static bool operator ==(MeshTransform lhs, MeshTransform rhs)
		{
			return lhs.Equals(rhs);
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00004C2B File Offset: 0x00002E2B
		public static bool operator !=(MeshTransform lhs, MeshTransform rhs)
		{
			return !lhs.Equals(rhs);
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00004C38 File Offset: 0x00002E38
		public override int GetHashCode()
		{
			return HashCodeHelper.Combine(this.MeshId.GetHashCode(), this.Timestamp.GetHashCode(), this.Position.GetHashCode(), this.Rotation.GetHashCode(), this.Scale.GetHashCode());
		}

		// Token: 0x0400010C RID: 268
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private readonly MeshId <MeshId>k__BackingField;

		// Token: 0x0400010D RID: 269
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private readonly ulong <Timestamp>k__BackingField;

		// Token: 0x0400010E RID: 270
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly Vector3 <Position>k__BackingField;

		// Token: 0x0400010F RID: 271
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly Quaternion <Rotation>k__BackingField;

		// Token: 0x04000110 RID: 272
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly Vector3 <Scale>k__BackingField;
	}
}
