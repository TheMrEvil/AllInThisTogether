using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace FIMSpace
{
	// Token: 0x02000042 RID: 66
	public class FImp_ColliderData_Sphere : FImp_ColliderData_Base
	{
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060001BB RID: 443 RVA: 0x0000EF86 File Offset: 0x0000D186
		// (set) Token: 0x060001BC RID: 444 RVA: 0x0000EF8E File Offset: 0x0000D18E
		public SphereCollider Sphere
		{
			[CompilerGenerated]
			get
			{
				return this.<Sphere>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Sphere>k__BackingField = value;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060001BD RID: 445 RVA: 0x0000EF97 File Offset: 0x0000D197
		// (set) Token: 0x060001BE RID: 446 RVA: 0x0000EF9F File Offset: 0x0000D19F
		public CircleCollider2D Sphere2D
		{
			[CompilerGenerated]
			get
			{
				return this.<Sphere2D>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Sphere2D>k__BackingField = value;
			}
		}

		// Token: 0x060001BF RID: 447 RVA: 0x0000EFA8 File Offset: 0x0000D1A8
		public FImp_ColliderData_Sphere(SphereCollider collider)
		{
			this.Is2D = false;
			base.Transform = collider.transform;
			base.Collider = collider;
			this.Sphere = collider;
			base.ColliderType = FImp_ColliderData_Base.EFColliderType.Sphere;
			this.RefreshColliderData();
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x0000EFDE File Offset: 0x0000D1DE
		public FImp_ColliderData_Sphere(CircleCollider2D collider)
		{
			this.Is2D = true;
			base.Transform = collider.transform;
			base.Collider2D = collider;
			this.Sphere2D = collider;
			base.ColliderType = FImp_ColliderData_Base.EFColliderType.Sphere;
			this.RefreshColliderData();
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x0000F014 File Offset: 0x0000D214
		public override void RefreshColliderData()
		{
			if (base.IsStatic)
			{
				return;
			}
			if (this.Sphere2D == null)
			{
				this.SphereRadius = FImp_ColliderData_Sphere.CalculateTrueRadiusOfSphereCollider(this.Sphere.transform, this.Sphere.radius);
				base.RefreshColliderData();
				return;
			}
			this.SphereRadius = FImp_ColliderData_Sphere.CalculateTrueRadiusOfSphereCollider(this.Sphere2D.transform, this.Sphere2D.radius);
			base.RefreshColliderData();
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x0000F087 File Offset: 0x0000D287
		public override bool PushIfInside(ref Vector3 point, float pointRadius, Vector3 pointOffset)
		{
			if (!this.Is2D)
			{
				return FImp_ColliderData_Sphere.PushOutFromSphereCollider(this.Sphere, pointRadius, ref point, this.SphereRadius, pointOffset);
			}
			return FImp_ColliderData_Sphere.PushOutFromSphereCollider(this.Sphere2D, pointRadius, ref point, this.SphereRadius, pointOffset);
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x0000F0BA File Offset: 0x0000D2BA
		public static bool PushOutFromSphereCollider(SphereCollider sphere, float segmentColliderRadius, ref Vector3 segmentPos, Vector3 segmentOffset)
		{
			return FImp_ColliderData_Sphere.PushOutFromSphereCollider(sphere, segmentColliderRadius, ref segmentPos, FImp_ColliderData_Sphere.CalculateTrueRadiusOfSphereCollider(sphere), segmentOffset);
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x0000F0CC File Offset: 0x0000D2CC
		public static bool PushOutFromSphereCollider(SphereCollider sphere, float segmentColliderRadius, ref Vector3 segmentPos, float collidingSphereRadius, Vector3 segmentOffset)
		{
			Vector3 vector = sphere.transform.position + sphere.transform.TransformVector(sphere.center);
			float num = collidingSphereRadius + segmentColliderRadius;
			Vector3 a = segmentPos + segmentOffset - vector;
			float sqrMagnitude = a.sqrMagnitude;
			if (sqrMagnitude > 0f && sqrMagnitude < num * num)
			{
				segmentPos = vector - segmentOffset + a * (num / Mathf.Sqrt(sqrMagnitude));
				return true;
			}
			return false;
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x0000F150 File Offset: 0x0000D350
		public static bool PushOutFromSphereCollider(CircleCollider2D sphere, float segmentColliderRadius, ref Vector3 segmentPos, float collidingSphereRadius, Vector3 segmentOffset)
		{
			Vector3 vector = sphere.transform.position + sphere.transform.TransformVector(sphere.offset);
			vector.z = 0f;
			float num = collidingSphereRadius + segmentColliderRadius;
			Vector3 a = segmentPos;
			a.z = 0f;
			Vector3 a2 = a + segmentOffset - vector;
			float sqrMagnitude = a2.sqrMagnitude;
			if (sqrMagnitude > 0f && sqrMagnitude < num * num)
			{
				segmentPos = vector - segmentOffset + a2 * (num / Mathf.Sqrt(sqrMagnitude));
				return true;
			}
			return false;
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x0000F1F4 File Offset: 0x0000D3F4
		public static float CalculateTrueRadiusOfSphereCollider(SphereCollider sphere)
		{
			return FImp_ColliderData_Sphere.CalculateTrueRadiusOfSphereCollider(sphere.transform, sphere.radius);
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x0000F207 File Offset: 0x0000D407
		public static float CalculateTrueRadiusOfSphereCollider(CircleCollider2D sphere)
		{
			return FImp_ColliderData_Sphere.CalculateTrueRadiusOfSphereCollider(sphere.transform, sphere.radius);
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x0000F21C File Offset: 0x0000D41C
		public static float CalculateTrueRadiusOfSphereCollider(Transform transform, float componentRadius)
		{
			float result;
			if (transform.lossyScale.x > transform.lossyScale.y)
			{
				if (transform.lossyScale.x > transform.lossyScale.z)
				{
					result = componentRadius * transform.lossyScale.x;
				}
				else
				{
					result = componentRadius * transform.lossyScale.z;
				}
			}
			else if (transform.lossyScale.y > transform.lossyScale.z)
			{
				result = componentRadius * transform.lossyScale.y;
			}
			else
			{
				result = componentRadius * transform.lossyScale.z;
			}
			return result;
		}

		// Token: 0x040001E8 RID: 488
		[CompilerGenerated]
		private SphereCollider <Sphere>k__BackingField;

		// Token: 0x040001E9 RID: 489
		[CompilerGenerated]
		private CircleCollider2D <Sphere2D>k__BackingField;

		// Token: 0x040001EA RID: 490
		private float SphereRadius;
	}
}
