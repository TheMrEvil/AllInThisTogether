using System;
using UnityEngine.Serialization;

namespace UnityEngine.ProBuilder
{
	// Token: 0x02000017 RID: 23
	[DisallowMultipleComponent]
	[AddComponentMenu("")]
	internal sealed class Entity : MonoBehaviour
	{
		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x0000F790 File Offset: 0x0000D990
		public EntityType entityType
		{
			get
			{
				return this.m_EntityType;
			}
		}

		// Token: 0x060000CA RID: 202 RVA: 0x0000F798 File Offset: 0x0000D998
		public void Awake()
		{
			MeshRenderer component = base.GetComponent<MeshRenderer>();
			if (!component)
			{
				return;
			}
			switch (this.entityType)
			{
			case EntityType.Detail:
			case EntityType.Occluder:
				break;
			case EntityType.Trigger:
				component.enabled = false;
				return;
			case EntityType.Collider:
				component.enabled = false;
				break;
			default:
				return;
			}
		}

		// Token: 0x060000CB RID: 203 RVA: 0x0000F7E2 File Offset: 0x0000D9E2
		public void SetEntity(EntityType t)
		{
			this.m_EntityType = t;
		}

		// Token: 0x060000CC RID: 204 RVA: 0x0000F7EB File Offset: 0x0000D9EB
		public Entity()
		{
		}

		// Token: 0x04000050 RID: 80
		[SerializeField]
		[HideInInspector]
		[FormerlySerializedAs("_entityType")]
		private EntityType m_EntityType;
	}
}
