using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.AI
{
	// Token: 0x02000013 RID: 19
	public struct NavMeshLinkInstance
	{
		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000DF RID: 223 RVA: 0x00002852 File Offset: 0x00000A52
		public bool valid
		{
			get
			{
				return this.id != 0 && NavMesh.IsValidLinkHandle(this.id);
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x0000286A File Offset: 0x00000A6A
		// (set) Token: 0x060000E1 RID: 225 RVA: 0x00002872 File Offset: 0x00000A72
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

		// Token: 0x060000E2 RID: 226 RVA: 0x0000287B File Offset: 0x00000A7B
		public void Remove()
		{
			NavMesh.RemoveLinkInternal(this.id);
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x0000288C File Offset: 0x00000A8C
		// (set) Token: 0x060000E4 RID: 228 RVA: 0x000028AC File Offset: 0x00000AAC
		public Object owner
		{
			get
			{
				return NavMesh.InternalGetLinkOwner(this.id);
			}
			set
			{
				int ownerID = (value != null) ? value.GetInstanceID() : 0;
				bool flag = !NavMesh.InternalSetLinkOwner(this.id, ownerID);
				if (flag)
				{
					Debug.LogError("Cannot set 'owner' on an invalid NavMeshLinkInstance");
				}
			}
		}

		// Token: 0x0400002A RID: 42
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private int <id>k__BackingField;
	}
}
