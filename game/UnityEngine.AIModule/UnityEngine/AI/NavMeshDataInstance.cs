using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.AI
{
	// Token: 0x02000011 RID: 17
	public struct NavMeshDataInstance
	{
		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000CB RID: 203 RVA: 0x000026B8 File Offset: 0x000008B8
		public bool valid
		{
			get
			{
				return this.id != 0 && NavMesh.IsValidNavMeshDataHandle(this.id);
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000CC RID: 204 RVA: 0x000026D0 File Offset: 0x000008D0
		// (set) Token: 0x060000CD RID: 205 RVA: 0x000026D8 File Offset: 0x000008D8
		internal int id
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<id>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<id>k__BackingField = value;
			}
		}

		// Token: 0x060000CE RID: 206 RVA: 0x000026E1 File Offset: 0x000008E1
		public void Remove()
		{
			NavMesh.RemoveNavMeshDataInternal(this.id);
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000CF RID: 207 RVA: 0x000026F0 File Offset: 0x000008F0
		// (set) Token: 0x060000D0 RID: 208 RVA: 0x00002710 File Offset: 0x00000910
		public Object owner
		{
			get
			{
				return NavMesh.InternalGetOwner(this.id);
			}
			set
			{
				int ownerID = (value != null) ? value.GetInstanceID() : 0;
				bool flag = !NavMesh.InternalSetOwner(this.id, ownerID);
				if (flag)
				{
					Debug.LogError("Cannot set 'owner' on an invalid NavMeshDataInstance");
				}
			}
		}

		// Token: 0x04000022 RID: 34
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int <id>k__BackingField;
	}
}
