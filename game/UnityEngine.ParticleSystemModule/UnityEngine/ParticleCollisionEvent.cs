using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000059 RID: 89
	[RequiredByNativeCode(Optional = true)]
	public struct ParticleCollisionEvent
	{
		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x060006ED RID: 1773 RVA: 0x000063D0 File Offset: 0x000045D0
		public Vector3 intersection
		{
			get
			{
				return this.m_Intersection;
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x060006EE RID: 1774 RVA: 0x000063E8 File Offset: 0x000045E8
		public Vector3 normal
		{
			get
			{
				return this.m_Normal;
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x060006EF RID: 1775 RVA: 0x00006400 File Offset: 0x00004600
		public Vector3 velocity
		{
			get
			{
				return this.m_Velocity;
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x060006F0 RID: 1776 RVA: 0x00006418 File Offset: 0x00004618
		public Component colliderComponent
		{
			get
			{
				return ParticleCollisionEvent.InstanceIDToColliderComponent(this.m_ColliderInstanceID);
			}
		}

		// Token: 0x060006F1 RID: 1777
		[FreeFunction(Name = "ParticleSystemScriptBindings::InstanceIDToColliderComponent")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Component InstanceIDToColliderComponent(int instanceID);

		// Token: 0x0400016F RID: 367
		internal Vector3 m_Intersection;

		// Token: 0x04000170 RID: 368
		internal Vector3 m_Normal;

		// Token: 0x04000171 RID: 369
		internal Vector3 m_Velocity;

		// Token: 0x04000172 RID: 370
		internal int m_ColliderInstanceID;
	}
}
