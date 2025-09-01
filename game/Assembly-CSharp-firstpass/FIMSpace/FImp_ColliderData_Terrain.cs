using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace FIMSpace
{
	// Token: 0x02000043 RID: 67
	public class FImp_ColliderData_Terrain : FImp_ColliderData_Base
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x0000F2B2 File Offset: 0x0000D4B2
		// (set) Token: 0x060001CA RID: 458 RVA: 0x0000F2BA File Offset: 0x0000D4BA
		public TerrainCollider TerrCollider
		{
			[CompilerGenerated]
			get
			{
				return this.<TerrCollider>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<TerrCollider>k__BackingField = value;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060001CB RID: 459 RVA: 0x0000F2C3 File Offset: 0x0000D4C3
		// (set) Token: 0x060001CC RID: 460 RVA: 0x0000F2CB File Offset: 0x0000D4CB
		public Terrain TerrainComponent
		{
			[CompilerGenerated]
			get
			{
				return this.<TerrainComponent>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<TerrainComponent>k__BackingField = value;
			}
		}

		// Token: 0x060001CD RID: 461 RVA: 0x0000F2D4 File Offset: 0x0000D4D4
		public FImp_ColliderData_Terrain(TerrainCollider collider)
		{
			base.Collider = collider;
			base.Transform = collider.transform;
			this.TerrCollider = collider;
			base.ColliderType = FImp_ColliderData_Base.EFColliderType.Terrain;
			this.TerrainComponent = collider.GetComponent<Terrain>();
		}

		// Token: 0x060001CE RID: 462 RVA: 0x0000F30C File Offset: 0x0000D50C
		public override bool PushIfInside(ref Vector3 segmentPosition, float segmentRadius, Vector3 segmentOffset)
		{
			if (segmentPosition.x + segmentRadius < this.TerrainComponent.GetPosition().x - segmentRadius || segmentPosition.x > this.TerrainComponent.GetPosition().x + this.TerrainComponent.terrainData.size.x || segmentPosition.z + segmentRadius < this.TerrainComponent.GetPosition().z - segmentRadius || segmentPosition.z > this.TerrainComponent.GetPosition().z + this.TerrainComponent.terrainData.size.z)
			{
				return false;
			}
			Vector3 vector = segmentPosition + segmentOffset;
			Vector3 vector2 = vector;
			vector2.y = this.TerrCollider.transform.position.y + this.TerrainComponent.SampleHeight(vector);
			float magnitude = (vector - vector2).magnitude;
			float num = 1f;
			if (vector.y < vector2.y)
			{
				num = 4f;
			}
			else if (vector.y + segmentRadius * 2f < vector2.y)
			{
				num = 8f;
			}
			if (magnitude < segmentRadius * num)
			{
				Vector3 a = vector2 - vector;
				Vector3 b;
				if (num > 1f)
				{
					b = a + a.normalized * segmentRadius;
				}
				else
				{
					b = a - a.normalized * segmentRadius;
				}
				segmentPosition += b;
				return true;
			}
			return false;
		}

		// Token: 0x060001CF RID: 463 RVA: 0x0000F488 File Offset: 0x0000D688
		public static void PushOutFromTerrain(TerrainCollider terrainCollider, float segmentRadius, ref Vector3 point)
		{
			Terrain component = terrainCollider.GetComponent<Terrain>();
			Vector3 origin = point;
			origin.y = terrainCollider.transform.position.y + component.SampleHeight(point) + segmentRadius;
			Ray ray = new Ray(origin, Vector3.down);
			RaycastHit raycastHit;
			if (terrainCollider.Raycast(ray, out raycastHit, segmentRadius * 2f))
			{
				float magnitude = (point - raycastHit.point).magnitude;
				float num = 1f;
				if (raycastHit.point.y > point.y + segmentRadius * 0.9f)
				{
					num = 8f;
				}
				else if (raycastHit.point.y > point.y)
				{
					num = 4f;
				}
				if (magnitude < segmentRadius * num)
				{
					Vector3 a = raycastHit.point - point;
					Vector3 b;
					if (num > 1f)
					{
						b = a + a.normalized * segmentRadius;
					}
					else
					{
						b = a - a.normalized * segmentRadius;
					}
					point += b;
				}
			}
		}

		// Token: 0x040001EB RID: 491
		[CompilerGenerated]
		private TerrainCollider <TerrCollider>k__BackingField;

		// Token: 0x040001EC RID: 492
		[CompilerGenerated]
		private Terrain <TerrainComponent>k__BackingField;
	}
}
