using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.AI
{
	// Token: 0x0200000C RID: 12
	[NativeHeader("Modules/AI/Components/OffMeshLink.bindings.h")]
	[MovedFrom("UnityEngine")]
	public struct OffMeshLinkData
	{
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000099 RID: 153 RVA: 0x00002518 File Offset: 0x00000718
		public bool valid
		{
			get
			{
				return this.m_Valid != 0;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600009A RID: 154 RVA: 0x00002523 File Offset: 0x00000723
		public bool activated
		{
			get
			{
				return this.m_Activated != 0;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600009B RID: 155 RVA: 0x0000252E File Offset: 0x0000072E
		public OffMeshLinkType linkType
		{
			get
			{
				return this.m_LinkType;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600009C RID: 156 RVA: 0x00002536 File Offset: 0x00000736
		public Vector3 startPos
		{
			get
			{
				return this.m_StartPos;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600009D RID: 157 RVA: 0x0000253E File Offset: 0x0000073E
		public Vector3 endPos
		{
			get
			{
				return this.m_EndPos;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600009E RID: 158 RVA: 0x00002546 File Offset: 0x00000746
		public OffMeshLink offMeshLink
		{
			get
			{
				return OffMeshLinkData.GetOffMeshLinkInternal(this.m_InstanceID);
			}
		}

		// Token: 0x0600009F RID: 159
		[FreeFunction("OffMeshLinkScriptBindings::GetOffMeshLinkInternal")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern OffMeshLink GetOffMeshLinkInternal(int instanceID);

		// Token: 0x04000014 RID: 20
		internal int m_Valid;

		// Token: 0x04000015 RID: 21
		internal int m_Activated;

		// Token: 0x04000016 RID: 22
		internal int m_InstanceID;

		// Token: 0x04000017 RID: 23
		internal OffMeshLinkType m_LinkType;

		// Token: 0x04000018 RID: 24
		internal Vector3 m_StartPos;

		// Token: 0x04000019 RID: 25
		internal Vector3 m_EndPos;
	}
}
