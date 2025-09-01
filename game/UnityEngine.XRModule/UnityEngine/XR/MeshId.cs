using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.XR
{
	// Token: 0x02000028 RID: 40
	[NativeHeader("Modules/XR/Subsystems/Meshing/XRMeshBindings.h")]
	[UsedByNativeCode]
	public struct MeshId : IEquatable<MeshId>
	{
		// Token: 0x06000132 RID: 306 RVA: 0x000045E0 File Offset: 0x000027E0
		public override string ToString()
		{
			return string.Format("{0}-{1}", this.m_SubId1.ToString("X16"), this.m_SubId2.ToString("X16"));
		}

		// Token: 0x06000133 RID: 307 RVA: 0x0000461C File Offset: 0x0000281C
		public override int GetHashCode()
		{
			return this.m_SubId1.GetHashCode() ^ this.m_SubId2.GetHashCode();
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00004648 File Offset: 0x00002848
		public override bool Equals(object obj)
		{
			return obj is MeshId && this.Equals((MeshId)obj);
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00004674 File Offset: 0x00002874
		public bool Equals(MeshId other)
		{
			return this.m_SubId1 == other.m_SubId1 && this.m_SubId2 == other.m_SubId2;
		}

		// Token: 0x06000136 RID: 310 RVA: 0x000046A8 File Offset: 0x000028A8
		public static bool operator ==(MeshId id1, MeshId id2)
		{
			return id1.m_SubId1 == id2.m_SubId1 && id1.m_SubId2 == id2.m_SubId2;
		}

		// Token: 0x06000137 RID: 311 RVA: 0x000046DC File Offset: 0x000028DC
		public static bool operator !=(MeshId id1, MeshId id2)
		{
			return id1.m_SubId1 != id2.m_SubId1 || id1.m_SubId2 != id2.m_SubId2;
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000138 RID: 312 RVA: 0x00004710 File Offset: 0x00002910
		public static MeshId InvalidId
		{
			get
			{
				return MeshId.s_InvalidId;
			}
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00004727 File Offset: 0x00002927
		// Note: this type is marked as 'beforefieldinit'.
		static MeshId()
		{
		}

		// Token: 0x040000E8 RID: 232
		private static MeshId s_InvalidId = default(MeshId);

		// Token: 0x040000E9 RID: 233
		private ulong m_SubId1;

		// Token: 0x040000EA RID: 234
		private ulong m_SubId2;
	}
}
