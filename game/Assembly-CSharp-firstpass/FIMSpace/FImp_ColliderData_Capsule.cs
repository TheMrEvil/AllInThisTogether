using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace FIMSpace
{
	// Token: 0x02000040 RID: 64
	public class FImp_ColliderData_Capsule : FImp_ColliderData_Base
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x0000DF2E File Offset: 0x0000C12E
		// (set) Token: 0x060001A3 RID: 419 RVA: 0x0000DF36 File Offset: 0x0000C136
		public CapsuleCollider Capsule
		{
			[CompilerGenerated]
			get
			{
				return this.<Capsule>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Capsule>k__BackingField = value;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x0000DF3F File Offset: 0x0000C13F
		// (set) Token: 0x060001A5 RID: 421 RVA: 0x0000DF47 File Offset: 0x0000C147
		public CapsuleCollider2D Capsule2D
		{
			[CompilerGenerated]
			get
			{
				return this.<Capsule2D>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Capsule2D>k__BackingField = value;
			}
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x0000DF50 File Offset: 0x0000C150
		public FImp_ColliderData_Capsule(CapsuleCollider collider)
		{
			this.Is2D = false;
			base.Transform = collider.transform;
			base.Collider = collider;
			base.Transform = collider.transform;
			this.Capsule = collider;
			base.ColliderType = FImp_ColliderData_Base.EFColliderType.Capsule;
			FImp_ColliderData_Capsule.CalculateCapsuleParameters(this.Capsule, ref this.Direction, ref this.radius, ref this.scaleFactor);
			this.RefreshColliderData();
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x0000DFBC File Offset: 0x0000C1BC
		public FImp_ColliderData_Capsule(CapsuleCollider2D collider)
		{
			this.Is2D = true;
			base.Transform = collider.transform;
			base.Collider2D = collider;
			base.Transform = collider.transform;
			this.Capsule2D = collider;
			base.ColliderType = FImp_ColliderData_Base.EFColliderType.Capsule;
			FImp_ColliderData_Capsule.CalculateCapsuleParameters(this.Capsule2D, ref this.Direction, ref this.radius, ref this.scaleFactor);
			this.RefreshColliderData();
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x0000E028 File Offset: 0x0000C228
		public override void RefreshColliderData()
		{
			if (base.IsStatic)
			{
				return;
			}
			bool flag = false;
			if (!this.previousPosition.VIsSame(base.Transform.position))
			{
				flag = true;
			}
			else if (!base.Transform.rotation.QIsSame(this.previousRotation))
			{
				flag = true;
			}
			else if (!this.Is2D)
			{
				if (this.preRadius != this.Capsule.radius || !this.previousScale.VIsSame(base.Transform.lossyScale))
				{
					FImp_ColliderData_Capsule.CalculateCapsuleParameters(this.Capsule, ref this.Direction, ref this.radius, ref this.scaleFactor);
				}
			}
			else if (this.preRadius != FImp_ColliderData_Capsule.GetCapsule2DRadius(this.Capsule2D) || !this.previousScale.VIsSame(base.Transform.lossyScale))
			{
				FImp_ColliderData_Capsule.CalculateCapsuleParameters(this.Capsule2D, ref this.Direction, ref this.radius, ref this.scaleFactor);
			}
			if (flag)
			{
				if (!this.Is2D)
				{
					FImp_ColliderData_Capsule.GetCapsuleHeadsPositions(this.Capsule, ref this.Top, ref this.Bottom, this.Direction, this.radius, this.scaleFactor);
				}
				else
				{
					FImp_ColliderData_Capsule.GetCapsuleHeadsPositions(this.Capsule2D, ref this.Top, ref this.Bottom, this.Direction, this.radius, this.scaleFactor);
				}
			}
			base.RefreshColliderData();
			this.previousPosition = base.Transform.position;
			this.previousRotation = base.Transform.rotation;
			this.previousScale = base.Transform.lossyScale;
			if (!this.Is2D)
			{
				this.preRadius = this.Capsule.radius;
				return;
			}
			this.preRadius = FImp_ColliderData_Capsule.GetCapsule2DRadius(this.Capsule2D);
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x0000E1DB File Offset: 0x0000C3DB
		public override bool PushIfInside(ref Vector3 point, float pointRadius, Vector3 pointOffset)
		{
			return FImp_ColliderData_Capsule.PushOutFromCapsuleCollider(pointRadius, ref point, this.Top, this.Bottom, this.radius, pointOffset, this.Is2D);
		}

		// Token: 0x060001AA RID: 426 RVA: 0x0000E200 File Offset: 0x0000C400
		public static bool PushOutFromCapsuleCollider(CapsuleCollider capsule, float segmentColliderRadius, ref Vector3 pos, Vector3 segmentOffset)
		{
			Vector3 zero = Vector3.zero;
			float capsuleRadius = capsule.radius;
			float scalerFactor = 1f;
			FImp_ColliderData_Capsule.CalculateCapsuleParameters(capsule, ref zero, ref capsuleRadius, ref scalerFactor);
			Vector3 zero2 = Vector3.zero;
			Vector3 zero3 = Vector3.zero;
			FImp_ColliderData_Capsule.GetCapsuleHeadsPositions(capsule, ref zero2, ref zero3, zero, capsuleRadius, scalerFactor);
			return FImp_ColliderData_Capsule.PushOutFromCapsuleCollider(segmentColliderRadius, ref pos, zero2, zero3, capsuleRadius, segmentOffset, false);
		}

		// Token: 0x060001AB RID: 427 RVA: 0x0000E254 File Offset: 0x0000C454
		public static bool PushOutFromCapsuleCollider(float segmentColliderRadius, ref Vector3 segmentPos, Vector3 capSphereCenter1, Vector3 capSphereCenter2, float capsuleRadius, Vector3 segmentOffset, bool is2D = false)
		{
			float num = capsuleRadius + segmentColliderRadius;
			Vector3 vector = capSphereCenter2 - capSphereCenter1;
			Vector3 vector2 = segmentPos + segmentOffset - capSphereCenter1;
			if (is2D)
			{
				vector.z = 0f;
				vector2.z = 0f;
			}
			float num2 = Vector3.Dot(vector2, vector);
			if (num2 <= 0f)
			{
				float sqrMagnitude = vector2.sqrMagnitude;
				if (sqrMagnitude > 0f && sqrMagnitude < num * num)
				{
					segmentPos = capSphereCenter1 - segmentOffset + vector2 * (num / Mathf.Sqrt(sqrMagnitude));
					return true;
				}
			}
			else
			{
				float sqrMagnitude2 = vector.sqrMagnitude;
				if (num2 >= sqrMagnitude2)
				{
					vector2 = segmentPos + segmentOffset - capSphereCenter2;
					float sqrMagnitude3 = vector2.sqrMagnitude;
					if (sqrMagnitude3 > 0f && sqrMagnitude3 < num * num)
					{
						segmentPos = capSphereCenter2 - segmentOffset + vector2 * (num / Mathf.Sqrt(sqrMagnitude3));
						return true;
					}
				}
				else if (sqrMagnitude2 > 0f)
				{
					vector2 -= vector * (num2 / sqrMagnitude2);
					float sqrMagnitude4 = vector2.sqrMagnitude;
					if (sqrMagnitude4 > 0f && sqrMagnitude4 < num * num)
					{
						float num3 = Mathf.Sqrt(sqrMagnitude4);
						segmentPos += vector2 * ((num - num3) / num3);
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060001AC RID: 428 RVA: 0x0000E3B4 File Offset: 0x0000C5B4
		protected static void CalculateCapsuleParameters(CapsuleCollider capsule, ref Vector3 direction, ref float trueRadius, ref float scalerFactor)
		{
			Transform transform = capsule.transform;
			float num;
			if (capsule.direction == 1)
			{
				direction = Vector3.up;
				scalerFactor = transform.lossyScale.y;
				num = ((transform.lossyScale.x > transform.lossyScale.z) ? transform.lossyScale.x : transform.lossyScale.z);
			}
			else if (capsule.direction == 0)
			{
				direction = Vector3.right;
				scalerFactor = transform.lossyScale.x;
				num = ((transform.lossyScale.y > transform.lossyScale.z) ? transform.lossyScale.y : transform.lossyScale.z);
			}
			else
			{
				direction = Vector3.forward;
				scalerFactor = transform.lossyScale.z;
				num = ((transform.lossyScale.y > transform.lossyScale.x) ? transform.lossyScale.y : transform.lossyScale.x);
			}
			trueRadius = capsule.radius * num;
		}

		// Token: 0x060001AD RID: 429 RVA: 0x0000E4C5 File Offset: 0x0000C6C5
		private static float GetCapsule2DRadius(CapsuleCollider2D capsule)
		{
			if (capsule.direction == CapsuleDirection2D.Vertical)
			{
				return capsule.size.x / 2f;
			}
			return capsule.size.y / 2f;
		}

		// Token: 0x060001AE RID: 430 RVA: 0x0000E4F2 File Offset: 0x0000C6F2
		private static float GetCapsule2DHeight(CapsuleCollider2D capsule)
		{
			if (capsule.direction == CapsuleDirection2D.Vertical)
			{
				return capsule.size.y / 2f;
			}
			return capsule.size.x / 2f;
		}

		// Token: 0x060001AF RID: 431 RVA: 0x0000E520 File Offset: 0x0000C720
		protected static void CalculateCapsuleParameters(CapsuleCollider2D capsule, ref Vector3 direction, ref float trueRadius, ref float scalerFactor)
		{
			Transform transform = capsule.transform;
			if (capsule.direction == CapsuleDirection2D.Vertical)
			{
				direction = Vector3.up;
				scalerFactor = transform.lossyScale.y;
				float num = (transform.lossyScale.x > transform.lossyScale.z) ? transform.lossyScale.x : transform.lossyScale.z;
				trueRadius = capsule.size.x / 2f * num;
				return;
			}
			if (capsule.direction == CapsuleDirection2D.Horizontal)
			{
				direction = Vector3.right;
				scalerFactor = transform.lossyScale.x;
				float num = (transform.lossyScale.y > transform.lossyScale.z) ? transform.lossyScale.y : transform.lossyScale.z;
				trueRadius = capsule.size.y / 2f * num;
			}
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x0000E604 File Offset: 0x0000C804
		protected static void GetCapsuleHeadsPositions(CapsuleCollider capsule, ref Vector3 upper, ref Vector3 bottom, Vector3 direction, float radius, float scalerFactor)
		{
			Vector3 direction2 = direction * (capsule.height / 2f * scalerFactor - radius);
			upper = capsule.transform.position + capsule.transform.TransformDirection(direction2) + capsule.transform.TransformVector(capsule.center);
			Vector3 direction3 = -direction * (capsule.height / 2f * scalerFactor - radius);
			bottom = capsule.transform.position + capsule.transform.TransformDirection(direction3) + capsule.transform.TransformVector(capsule.center);
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x0000E6B8 File Offset: 0x0000C8B8
		protected static void GetCapsuleHeadsPositions(CapsuleCollider2D capsule, ref Vector3 upper, ref Vector3 bottom, Vector3 direction, float radius, float scalerFactor)
		{
			Vector3 direction2 = direction * (FImp_ColliderData_Capsule.GetCapsule2DHeight(capsule) * scalerFactor - radius);
			upper = capsule.transform.position + capsule.transform.TransformDirection(direction2) + capsule.transform.TransformVector(capsule.offset);
			upper.z = 0f;
			Vector3 direction3 = -direction * (FImp_ColliderData_Capsule.GetCapsule2DHeight(capsule) * scalerFactor - radius);
			bottom = capsule.transform.position + capsule.transform.TransformDirection(direction3) + capsule.transform.TransformVector(capsule.offset);
			bottom.z = 0f;
		}

		// Token: 0x040001DC RID: 476
		[CompilerGenerated]
		private CapsuleCollider <Capsule>k__BackingField;

		// Token: 0x040001DD RID: 477
		[CompilerGenerated]
		private CapsuleCollider2D <Capsule2D>k__BackingField;

		// Token: 0x040001DE RID: 478
		private Vector3 Top;

		// Token: 0x040001DF RID: 479
		private Vector3 Bottom;

		// Token: 0x040001E0 RID: 480
		private Vector3 Direction;

		// Token: 0x040001E1 RID: 481
		private float radius;

		// Token: 0x040001E2 RID: 482
		private float scaleFactor;

		// Token: 0x040001E3 RID: 483
		private float preRadius;
	}
}
