using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.AI
{
	// Token: 0x0200001A RID: 26
	[UsedByNativeCode]
	[NativeHeader("Modules/AI/Public/NavMeshBindingTypes.h")]
	public struct NavMeshBuildSource
	{
		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000133 RID: 307 RVA: 0x00002CE0 File Offset: 0x00000EE0
		// (set) Token: 0x06000134 RID: 308 RVA: 0x00002CF8 File Offset: 0x00000EF8
		public Matrix4x4 transform
		{
			get
			{
				return this.m_Transform;
			}
			set
			{
				this.m_Transform = value;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000135 RID: 309 RVA: 0x00002D04 File Offset: 0x00000F04
		// (set) Token: 0x06000136 RID: 310 RVA: 0x00002D1C File Offset: 0x00000F1C
		public Vector3 size
		{
			get
			{
				return this.m_Size;
			}
			set
			{
				this.m_Size = value;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000137 RID: 311 RVA: 0x00002D28 File Offset: 0x00000F28
		// (set) Token: 0x06000138 RID: 312 RVA: 0x00002D40 File Offset: 0x00000F40
		public NavMeshBuildSourceShape shape
		{
			get
			{
				return this.m_Shape;
			}
			set
			{
				this.m_Shape = value;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000139 RID: 313 RVA: 0x00002D4C File Offset: 0x00000F4C
		// (set) Token: 0x0600013A RID: 314 RVA: 0x00002D64 File Offset: 0x00000F64
		public int area
		{
			get
			{
				return this.m_Area;
			}
			set
			{
				this.m_Area = value;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600013B RID: 315 RVA: 0x00002D70 File Offset: 0x00000F70
		// (set) Token: 0x0600013C RID: 316 RVA: 0x00002D8D File Offset: 0x00000F8D
		public Object sourceObject
		{
			get
			{
				return NavMeshBuildSource.InternalGetObject(this.m_InstanceID);
			}
			set
			{
				this.m_InstanceID = ((value != null) ? value.GetInstanceID() : 0);
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600013D RID: 317 RVA: 0x00002DA8 File Offset: 0x00000FA8
		// (set) Token: 0x0600013E RID: 318 RVA: 0x00002DC5 File Offset: 0x00000FC5
		public Component component
		{
			get
			{
				return NavMeshBuildSource.InternalGetComponent(this.m_ComponentID);
			}
			set
			{
				this.m_ComponentID = ((value != null) ? value.GetInstanceID() : 0);
			}
		}

		// Token: 0x0600013F RID: 319
		[StaticAccessor("NavMeshBuildSource", StaticAccessorType.DoubleColon)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Component InternalGetComponent(int instanceID);

		// Token: 0x06000140 RID: 320
		[StaticAccessor("NavMeshBuildSource", StaticAccessorType.DoubleColon)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Object InternalGetObject(int instanceID);

		// Token: 0x04000045 RID: 69
		private Matrix4x4 m_Transform;

		// Token: 0x04000046 RID: 70
		private Vector3 m_Size;

		// Token: 0x04000047 RID: 71
		private NavMeshBuildSourceShape m_Shape;

		// Token: 0x04000048 RID: 72
		private int m_Area;

		// Token: 0x04000049 RID: 73
		private int m_InstanceID;

		// Token: 0x0400004A RID: 74
		private int m_ComponentID;
	}
}
