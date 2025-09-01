using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace FIMSpace
{
	// Token: 0x0200003F RID: 63
	public class FImp_ColliderData_Box : FImp_ColliderData_Base
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000184 RID: 388 RVA: 0x0000C153 File Offset: 0x0000A353
		// (set) Token: 0x06000185 RID: 389 RVA: 0x0000C15B File Offset: 0x0000A35B
		public BoxCollider Box
		{
			[CompilerGenerated]
			get
			{
				return this.<Box>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Box>k__BackingField = value;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000186 RID: 390 RVA: 0x0000C164 File Offset: 0x0000A364
		// (set) Token: 0x06000187 RID: 391 RVA: 0x0000C16C File Offset: 0x0000A36C
		public BoxCollider2D Box2D
		{
			[CompilerGenerated]
			get
			{
				return this.<Box2D>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Box2D>k__BackingField = value;
			}
		}

		// Token: 0x06000188 RID: 392 RVA: 0x0000C178 File Offset: 0x0000A378
		public FImp_ColliderData_Box(BoxCollider collider)
		{
			this.Is2D = false;
			base.Collider = collider;
			base.Transform = collider.transform;
			this.Box = collider;
			base.ColliderType = FImp_ColliderData_Base.EFColliderType.Box;
			this.RefreshColliderData();
			this.previousPosition = base.Transform.position + Vector3.forward * Mathf.Epsilon;
		}

		// Token: 0x06000189 RID: 393 RVA: 0x0000C1E0 File Offset: 0x0000A3E0
		public FImp_ColliderData_Box(BoxCollider2D collider2D)
		{
			this.Is2D = true;
			base.Collider2D = collider2D;
			base.Transform = collider2D.transform;
			this.Box2D = collider2D;
			base.ColliderType = FImp_ColliderData_Base.EFColliderType.Box;
			this.RefreshColliderData();
			this.previousPosition = base.Transform.position + Vector3.forward * Mathf.Epsilon;
		}

		// Token: 0x0600018A RID: 394 RVA: 0x0000C248 File Offset: 0x0000A448
		public override void RefreshColliderData()
		{
			if (base.IsStatic)
			{
				return;
			}
			if (base.Collider2D == null)
			{
				bool flag = false;
				if (!base.Transform.position.VIsSame(this.previousPosition))
				{
					flag = true;
				}
				else if (!base.Transform.rotation.QIsSame(this.previousRotation))
				{
					flag = true;
				}
				if (flag)
				{
					this.right = this.Box.transform.TransformVector(Vector3.right / 2f * this.Box.size.x);
					this.up = this.Box.transform.TransformVector(Vector3.up / 2f * this.Box.size.y);
					this.forward = this.Box.transform.TransformVector(Vector3.forward / 2f * this.Box.size.z);
					this.rightN = this.right.normalized;
					this.upN = this.up.normalized;
					this.forwardN = this.forward.normalized;
					this.boxCenter = FImp_ColliderData_Box.GetBoxCenter(this.Box);
					this.scales = Vector3.Scale(this.Box.size, this.Box.transform.lossyScale);
					this.scales.Normalize();
				}
			}
			else
			{
				bool flag2 = false;
				if (Vector2.Distance(base.Transform.position, this.previousPosition) > Mathf.Epsilon)
				{
					flag2 = true;
				}
				else if (!base.Transform.rotation.QIsSame(this.previousRotation))
				{
					flag2 = true;
				}
				if (flag2)
				{
					this.right = this.Box2D.transform.TransformVector(Vector3.right / 2f * this.Box2D.size.x);
					this.up = this.Box2D.transform.TransformVector(Vector3.up / 2f * this.Box2D.size.y);
					this.rightN = this.right.normalized;
					this.upN = this.up.normalized;
					this.boxCenter = FImp_ColliderData_Box.GetBoxCenter(this.Box2D);
					this.boxCenter.z = 0f;
					Vector3 lossyScale = base.Transform.lossyScale;
					lossyScale.z = 1f;
					this.scales = Vector3.Scale(this.Box2D.size, lossyScale);
					this.scales.Normalize();
				}
			}
			base.RefreshColliderData();
			this.previousPosition = base.Transform.position;
			this.previousRotation = base.Transform.rotation;
		}

		// Token: 0x0600018B RID: 395 RVA: 0x0000C540 File Offset: 0x0000A740
		public override bool PushIfInside(ref Vector3 segmentPosition, float segmentRadius, Vector3 segmentOffset)
		{
			int num = 0;
			Vector3 vector = Vector3.zero;
			Vector3 vector2 = segmentPosition + segmentOffset;
			float planeDistance = FImp_ColliderData_Box.PlaneDistance(this.boxCenter + this.up, this.upN, vector2);
			if (FImp_ColliderData_Box.SphereInsidePlane(planeDistance, segmentRadius))
			{
				num++;
			}
			else if (FImp_ColliderData_Box.SphereIntersectsPlane(planeDistance, segmentRadius))
			{
				num++;
				vector = this.up;
			}
			planeDistance = FImp_ColliderData_Box.PlaneDistance(this.boxCenter - this.up, -this.upN, vector2);
			if (FImp_ColliderData_Box.SphereInsidePlane(planeDistance, segmentRadius))
			{
				num++;
			}
			else if (FImp_ColliderData_Box.SphereIntersectsPlane(planeDistance, segmentRadius))
			{
				num++;
				vector = -this.up;
			}
			planeDistance = FImp_ColliderData_Box.PlaneDistance(this.boxCenter - this.right, -this.rightN, vector2);
			if (FImp_ColliderData_Box.SphereInsidePlane(planeDistance, segmentRadius))
			{
				num++;
			}
			else if (FImp_ColliderData_Box.SphereIntersectsPlane(planeDistance, segmentRadius))
			{
				num++;
				vector = -this.right;
			}
			planeDistance = FImp_ColliderData_Box.PlaneDistance(this.boxCenter + this.right, this.rightN, vector2);
			if (FImp_ColliderData_Box.SphereInsidePlane(planeDistance, segmentRadius))
			{
				num++;
			}
			else if (FImp_ColliderData_Box.SphereIntersectsPlane(planeDistance, segmentRadius))
			{
				num++;
				vector = this.right;
			}
			bool flag = false;
			if (base.Collider2D == null)
			{
				planeDistance = FImp_ColliderData_Box.PlaneDistance(this.boxCenter + this.forward, this.forwardN, vector2);
				if (FImp_ColliderData_Box.SphereInsidePlane(planeDistance, segmentRadius))
				{
					num++;
				}
				else if (FImp_ColliderData_Box.SphereIntersectsPlane(planeDistance, segmentRadius))
				{
					num++;
					vector = this.forward;
				}
				planeDistance = FImp_ColliderData_Box.PlaneDistance(this.boxCenter - this.forward, -this.forwardN, vector2);
				if (FImp_ColliderData_Box.SphereInsidePlane(planeDistance, segmentRadius))
				{
					num++;
				}
				else if (FImp_ColliderData_Box.SphereIntersectsPlane(planeDistance, segmentRadius))
				{
					num++;
					vector = -this.forward;
				}
				if (num == 6)
				{
					flag = true;
				}
			}
			else if (num == 4)
			{
				flag = true;
			}
			if (flag)
			{
				bool flag2 = false;
				if (vector.sqrMagnitude == 0f)
				{
					flag2 = true;
				}
				else if (base.Collider2D == null)
				{
					if (FImp_ColliderData_Box.IsInsideBoxCollider(this.Box, vector2, false))
					{
						flag2 = true;
					}
				}
				else if (FImp_ColliderData_Box.IsInsideBoxCollider(this.Box2D, vector2))
				{
					flag2 = true;
				}
				Vector3 vector3 = this.GetNearestPoint(vector2) - vector2;
				if (flag2)
				{
					vector3 += vector3.normalized * segmentRadius;
				}
				else
				{
					vector3 -= vector3.normalized * segmentRadius;
				}
				if (flag2)
				{
					segmentPosition += vector3;
				}
				else if (vector3.sqrMagnitude > 0f)
				{
					segmentPosition += vector3;
				}
				return true;
			}
			return false;
		}

		// Token: 0x0600018C RID: 396 RVA: 0x0000C7F8 File Offset: 0x0000A9F8
		public static void PushOutFromBoxCollider(BoxCollider box, Collision collision, float segmentColliderRadius, ref Vector3 segmentPosition, bool is2D = false)
		{
			Vector3 vector = box.transform.TransformVector(Vector3.right / 2f * box.size.x + box.center.x * Vector3.right);
			Vector3 vector2 = box.transform.TransformVector(Vector3.up / 2f * box.size.y + box.center.y * Vector3.up);
			Vector3 vector3 = box.transform.TransformVector(Vector3.forward / 2f * box.size.z + box.center.z * Vector3.forward);
			Vector3 vector4 = Vector3.Scale(box.size, box.transform.lossyScale);
			vector4.Normalize();
			FImp_ColliderData_Box.PushOutFromBoxCollider(box, collision, segmentColliderRadius, ref segmentPosition, vector, vector2, vector3, vector4, is2D);
		}

		// Token: 0x0600018D RID: 397 RVA: 0x0000C904 File Offset: 0x0000AB04
		public static void PushOutFromBoxCollider(BoxCollider box, float segmentColliderRadius, ref Vector3 segmentPosition, bool is2D = false)
		{
			Vector3 vector = box.transform.TransformVector(Vector3.right / 2f * box.size.x + box.center.x * Vector3.right);
			Vector3 vector2 = box.transform.TransformVector(Vector3.up / 2f * box.size.y + box.center.y * Vector3.up);
			Vector3 vector3 = box.transform.TransformVector(Vector3.forward / 2f * box.size.z + box.center.z * Vector3.forward);
			Vector3.Scale(box.size, box.transform.lossyScale).Normalize();
			Vector3 a = FImp_ColliderData_Box.GetBoxCenter(box);
			Vector3 normalized = vector2.normalized;
			Vector3 normalized2 = vector.normalized;
			Vector3 normalized3 = vector3.normalized;
			int num = 0;
			Vector3 vector4 = Vector3.zero;
			float planeDistance = FImp_ColliderData_Box.PlaneDistance(a + vector2, normalized, segmentPosition);
			if (FImp_ColliderData_Box.SphereInsidePlane(planeDistance, segmentColliderRadius))
			{
				num++;
			}
			else if (FImp_ColliderData_Box.SphereIntersectsPlane(planeDistance, segmentColliderRadius))
			{
				num++;
				vector4 = vector2;
			}
			planeDistance = FImp_ColliderData_Box.PlaneDistance(a - vector2, -normalized, segmentPosition);
			if (FImp_ColliderData_Box.SphereInsidePlane(planeDistance, segmentColliderRadius))
			{
				num++;
			}
			else if (FImp_ColliderData_Box.SphereIntersectsPlane(planeDistance, segmentColliderRadius))
			{
				num++;
				vector4 = -vector2;
			}
			planeDistance = FImp_ColliderData_Box.PlaneDistance(a - vector, -normalized2, segmentPosition);
			if (FImp_ColliderData_Box.SphereInsidePlane(planeDistance, segmentColliderRadius))
			{
				num++;
			}
			else if (FImp_ColliderData_Box.SphereIntersectsPlane(planeDistance, segmentColliderRadius))
			{
				num++;
				vector4 = -vector;
			}
			planeDistance = FImp_ColliderData_Box.PlaneDistance(a + vector, normalized2, segmentPosition);
			if (FImp_ColliderData_Box.SphereInsidePlane(planeDistance, segmentColliderRadius))
			{
				num++;
			}
			else if (FImp_ColliderData_Box.SphereIntersectsPlane(planeDistance, segmentColliderRadius))
			{
				num++;
				vector4 = vector;
			}
			planeDistance = FImp_ColliderData_Box.PlaneDistance(a + vector3, normalized3, segmentPosition);
			if (FImp_ColliderData_Box.SphereInsidePlane(planeDistance, segmentColliderRadius))
			{
				num++;
			}
			else if (FImp_ColliderData_Box.SphereIntersectsPlane(planeDistance, segmentColliderRadius))
			{
				num++;
				vector4 = vector3;
			}
			planeDistance = FImp_ColliderData_Box.PlaneDistance(a - vector3, -normalized3, segmentPosition);
			if (FImp_ColliderData_Box.SphereInsidePlane(planeDistance, segmentColliderRadius))
			{
				num++;
			}
			else if (FImp_ColliderData_Box.SphereIntersectsPlane(planeDistance, segmentColliderRadius))
			{
				num++;
				vector4 = -vector3;
			}
			if (num == 6)
			{
				bool flag = false;
				if (vector4.sqrMagnitude == 0f)
				{
					flag = true;
				}
				else if (FImp_ColliderData_Box.IsInsideBoxCollider(box, segmentPosition, false))
				{
					flag = true;
				}
				Vector3 vector5 = FImp_ColliderData_Box.GetNearestPoint(segmentPosition, a, vector, vector2, vector3, is2D) - segmentPosition;
				if (flag)
				{
					vector5 += vector5.normalized * segmentColliderRadius * 1.01f;
				}
				else
				{
					vector5 -= vector5.normalized * segmentColliderRadius * 1.01f;
				}
				if (flag)
				{
					segmentPosition += vector5;
					return;
				}
				if (vector5.sqrMagnitude > 0f)
				{
					segmentPosition += vector5;
				}
			}
		}

		// Token: 0x0600018E RID: 398 RVA: 0x0000CC90 File Offset: 0x0000AE90
		public static void PushOutFromBoxCollider(BoxCollider box, Collision collision, float segmentColliderRadius, ref Vector3 pos, Vector3 right, Vector3 up, Vector3 forward, Vector3 scales, bool is2D = false)
		{
			Vector3 vector = collision.contacts[0].point;
			Vector3 a = pos - vector;
			Vector3 vector2 = FImp_ColliderData_Box.GetBoxCenter(box);
			if (a.sqrMagnitude == 0f)
			{
				a = pos - vector2;
			}
			float num = 1f;
			if (FImp_ColliderData_Box.IsInsideBoxCollider(box, pos, false))
			{
				float boxAverageScale = FImp_ColliderData_Box.GetBoxAverageScale(box);
				Vector3 targetPlaneNormal = FImp_ColliderData_Box.GetTargetPlaneNormal(box, pos, right, up, forward, scales, false);
				Vector3 normalized = targetPlaneNormal.normalized;
				RaycastHit raycastHit;
				if (box.Raycast(new Ray(pos - normalized * boxAverageScale * 3f, normalized), out raycastHit, boxAverageScale * 4f))
				{
					vector = raycastHit.point;
				}
				else
				{
					vector = FImp_ColliderData_Box.GetIntersectOnBoxFromInside(box, vector2, pos, targetPlaneNormal);
				}
				a = vector - pos;
				num = 100f;
			}
			Vector3 b = pos - (a / num + a.normalized * 1.15f) / 2f * segmentColliderRadius;
			b = vector - b;
			float sqrMagnitude = b.sqrMagnitude;
			if (sqrMagnitude > 0f && sqrMagnitude < segmentColliderRadius * segmentColliderRadius * num)
			{
				pos += b;
			}
		}

		// Token: 0x0600018F RID: 399 RVA: 0x0000CDF4 File Offset: 0x0000AFF4
		public static void PushOutFromBoxCollider(BoxCollider2D box2D, float segmentColliderRadius, ref Vector3 segmentPosition)
		{
			Vector2 vector = box2D.transform.TransformVector(Vector3.right / 2f * box2D.size.x + box2D.offset.x * Vector3.right);
			Vector2 vector2 = box2D.transform.TransformVector(Vector3.up / 2f * box2D.size.y + box2D.offset.y * Vector3.up);
			Vector3 lossyScale = box2D.transform.lossyScale;
			lossyScale.z = 1f;
			Vector3.Scale(box2D.size, lossyScale).Normalize();
			Vector2 a = FImp_ColliderData_Box.GetBoxCenter(box2D);
			Vector2 normalized = vector2.normalized;
			Vector2 normalized2 = vector.normalized;
			int num = 0;
			Vector3 vector3 = Vector3.zero;
			float planeDistance = FImp_ColliderData_Box.PlaneDistance(a + vector2, normalized, segmentPosition);
			if (FImp_ColliderData_Box.SphereInsidePlane(planeDistance, segmentColliderRadius))
			{
				num++;
			}
			else if (FImp_ColliderData_Box.SphereIntersectsPlane(planeDistance, segmentColliderRadius))
			{
				num++;
				vector3 = vector2;
			}
			planeDistance = FImp_ColliderData_Box.PlaneDistance(a - vector2, -normalized, segmentPosition);
			if (FImp_ColliderData_Box.SphereInsidePlane(planeDistance, segmentColliderRadius))
			{
				num++;
			}
			else if (FImp_ColliderData_Box.SphereIntersectsPlane(planeDistance, segmentColliderRadius))
			{
				num++;
				vector3 = -vector2;
			}
			planeDistance = FImp_ColliderData_Box.PlaneDistance(a - vector, -normalized2, segmentPosition);
			if (FImp_ColliderData_Box.SphereInsidePlane(planeDistance, segmentColliderRadius))
			{
				num++;
			}
			else if (FImp_ColliderData_Box.SphereIntersectsPlane(planeDistance, segmentColliderRadius))
			{
				num++;
				vector3 = -vector;
			}
			planeDistance = FImp_ColliderData_Box.PlaneDistance(a + vector, normalized2, segmentPosition);
			if (FImp_ColliderData_Box.SphereInsidePlane(planeDistance, segmentColliderRadius))
			{
				num++;
			}
			else if (FImp_ColliderData_Box.SphereIntersectsPlane(planeDistance, segmentColliderRadius))
			{
				num++;
				vector3 = vector;
			}
			if (num == 4)
			{
				bool flag = false;
				if (vector3.sqrMagnitude == 0f)
				{
					flag = true;
				}
				else if (FImp_ColliderData_Box.IsInsideBoxCollider(box2D, segmentPosition))
				{
					flag = true;
				}
				Vector3 vector4 = FImp_ColliderData_Box.GetNearestPoint2D(segmentPosition, a, vector, vector2) - segmentPosition;
				if (flag)
				{
					vector4 += vector4.normalized * segmentColliderRadius * 1.01f;
				}
				else
				{
					vector4 -= vector4.normalized * segmentColliderRadius * 1.01f;
				}
				if (flag)
				{
					segmentPosition += vector4;
					return;
				}
				if (vector4.sqrMagnitude > 0f)
				{
					segmentPosition += vector4;
				}
			}
		}

		// Token: 0x06000190 RID: 400 RVA: 0x0000D110 File Offset: 0x0000B310
		private Vector3 GetNearestPoint(Vector3 point)
		{
			Vector3 one = Vector3.one;
			one.x = FImp_ColliderData_Box.PlaneDistance(this.boxCenter + this.right, this.rightN, point);
			one.y = FImp_ColliderData_Box.PlaneDistance(this.boxCenter + this.up, this.upN, point);
			if (base.Collider2D == null)
			{
				one.z = FImp_ColliderData_Box.PlaneDistance(this.boxCenter + this.forward, this.forwardN, point);
			}
			Vector3 one2 = Vector3.one;
			one2.x = FImp_ColliderData_Box.PlaneDistance(this.boxCenter - this.right, -this.rightN, point);
			one2.y = FImp_ColliderData_Box.PlaneDistance(this.boxCenter - this.up, -this.upN, point);
			if (base.Collider2D == null)
			{
				one2.z = FImp_ColliderData_Box.PlaneDistance(this.boxCenter - this.forward, -this.forwardN, point);
			}
			float x;
			float d;
			if (one.x > one2.x)
			{
				x = one.x;
				d = -1f;
			}
			else
			{
				x = one2.x;
				d = 1f;
			}
			float y;
			float d2;
			if (one.y > one2.y)
			{
				y = one.y;
				d2 = -1f;
			}
			else
			{
				y = one2.y;
				d2 = 1f;
			}
			Vector3 result;
			if (base.Collider2D == null)
			{
				float z;
				float d3;
				if (one.z > one2.z)
				{
					z = one.z;
					d3 = -1f;
				}
				else
				{
					z = one2.z;
					d3 = 1f;
				}
				if (x > z)
				{
					if (x > y)
					{
						result = FImp_ColliderData_Box.ProjectPointOnPlane(this.right * d, point, x);
					}
					else
					{
						result = FImp_ColliderData_Box.ProjectPointOnPlane(this.up * d2, point, y);
					}
				}
				else if (z > y)
				{
					result = FImp_ColliderData_Box.ProjectPointOnPlane(this.forward * d3, point, z);
				}
				else
				{
					result = FImp_ColliderData_Box.ProjectPointOnPlane(this.up * d2, point, y);
				}
			}
			else if (x > y)
			{
				result = FImp_ColliderData_Box.ProjectPointOnPlane(this.right * d, point, x);
			}
			else
			{
				result = FImp_ColliderData_Box.ProjectPointOnPlane(this.up * d2, point, y);
			}
			return result;
		}

		// Token: 0x06000191 RID: 401 RVA: 0x0000D384 File Offset: 0x0000B584
		private static Vector3 GetNearestPoint(Vector3 point, Vector3 boxCenter, Vector3 right, Vector3 up, Vector3 forward, bool is2D = false)
		{
			Vector3 one = Vector3.one;
			one.x = FImp_ColliderData_Box.PlaneDistance(boxCenter + right, right.normalized, point);
			one.y = FImp_ColliderData_Box.PlaneDistance(boxCenter + up, up.normalized, point);
			if (!is2D)
			{
				one.z = FImp_ColliderData_Box.PlaneDistance(boxCenter + forward, forward.normalized, point);
			}
			Vector3 one2 = Vector3.one;
			one2.x = FImp_ColliderData_Box.PlaneDistance(boxCenter - right, -right.normalized, point);
			one2.y = FImp_ColliderData_Box.PlaneDistance(boxCenter - up, -up.normalized, point);
			if (!is2D)
			{
				one2.z = FImp_ColliderData_Box.PlaneDistance(boxCenter - forward, -forward.normalized, point);
			}
			float x;
			float d;
			if (one.x > one2.x)
			{
				x = one.x;
				d = -1f;
			}
			else
			{
				x = one2.x;
				d = 1f;
			}
			float y;
			float d2;
			if (one.y > one2.y)
			{
				y = one.y;
				d2 = -1f;
			}
			else
			{
				y = one2.y;
				d2 = 1f;
			}
			Vector3 result;
			if (!is2D)
			{
				float z;
				float d3;
				if (one.z > one2.z)
				{
					z = one.z;
					d3 = -1f;
				}
				else
				{
					z = one2.z;
					d3 = 1f;
				}
				if (x > z)
				{
					if (x > y)
					{
						result = FImp_ColliderData_Box.ProjectPointOnPlane(right * d, point, x);
					}
					else
					{
						result = FImp_ColliderData_Box.ProjectPointOnPlane(up * d2, point, y);
					}
				}
				else if (z > y)
				{
					result = FImp_ColliderData_Box.ProjectPointOnPlane(forward * d3, point, z);
				}
				else
				{
					result = FImp_ColliderData_Box.ProjectPointOnPlane(up * d2, point, y);
				}
			}
			else if (x > y)
			{
				result = FImp_ColliderData_Box.ProjectPointOnPlane(right * d, point, x);
			}
			else
			{
				result = FImp_ColliderData_Box.ProjectPointOnPlane(up * d2, point, y);
			}
			return result;
		}

		// Token: 0x06000192 RID: 402 RVA: 0x0000D584 File Offset: 0x0000B784
		private static Vector3 GetNearestPoint2D(Vector2 point, Vector2 boxCenter, Vector2 right, Vector2 up)
		{
			Vector3 result = point;
			Vector3 one = Vector3.one;
			one.x = FImp_ColliderData_Box.PlaneDistance(boxCenter + right, right.normalized, point);
			one.y = FImp_ColliderData_Box.PlaneDistance(boxCenter + up, up.normalized, point);
			Vector3 one2 = Vector3.one;
			one2.x = FImp_ColliderData_Box.PlaneDistance(boxCenter - right, -right.normalized, point);
			one2.y = FImp_ColliderData_Box.PlaneDistance(boxCenter - up, -up.normalized, point);
			float x;
			float d;
			if (one.x > one2.x)
			{
				x = one.x;
				d = -1f;
			}
			else
			{
				x = one2.x;
				d = 1f;
			}
			float y;
			float d2;
			if (one.y > one2.y)
			{
				y = one.y;
				d2 = -1f;
			}
			else
			{
				y = one2.y;
				d2 = 1f;
			}
			if (x > y)
			{
				result = FImp_ColliderData_Box.ProjectPointOnPlane(right * d, point, x);
			}
			else
			{
				result = FImp_ColliderData_Box.ProjectPointOnPlane(up * d2, point, y);
			}
			return result;
		}

		// Token: 0x06000193 RID: 403 RVA: 0x0000D6FC File Offset: 0x0000B8FC
		public static Vector3 GetNearestPointOnBox(BoxCollider boxCollider, Vector3 point, bool is2D = false)
		{
			Vector3 vector = boxCollider.transform.TransformVector(Vector3.right / 2f);
			Vector3 vector2 = boxCollider.transform.TransformVector(Vector3.up / 2f);
			Vector3 vector3 = Vector3.forward;
			if (!is2D)
			{
				vector3 = boxCollider.transform.TransformVector(Vector3.forward / 2f);
			}
			Vector3 a = FImp_ColliderData_Box.GetBoxCenter(boxCollider);
			Vector3 normalized = vector.normalized;
			Vector3 normalized2 = vector2.normalized;
			Vector3 normalized3 = vector3.normalized;
			Vector3 one = Vector3.one;
			one.x = FImp_ColliderData_Box.PlaneDistance(a + vector, normalized, point);
			one.y = FImp_ColliderData_Box.PlaneDistance(a + vector2, normalized2, point);
			if (!is2D)
			{
				one.z = FImp_ColliderData_Box.PlaneDistance(a + vector3, normalized3, point);
			}
			Vector3 one2 = Vector3.one;
			one2.x = FImp_ColliderData_Box.PlaneDistance(a - vector, -normalized, point);
			one2.y = FImp_ColliderData_Box.PlaneDistance(a - vector2, -normalized2, point);
			if (!is2D)
			{
				one2.z = FImp_ColliderData_Box.PlaneDistance(a - vector3, -normalized3, point);
			}
			float x;
			float d;
			if (one.x > one2.x)
			{
				x = one.x;
				d = -1f;
			}
			else
			{
				x = one2.x;
				d = 1f;
			}
			float y;
			float d2;
			if (one.y > one2.y)
			{
				y = one.y;
				d2 = -1f;
			}
			else
			{
				y = one2.y;
				d2 = 1f;
			}
			Vector3 result;
			if (!is2D)
			{
				float z;
				float d3;
				if (one.z > one2.z)
				{
					z = one.z;
					d3 = -1f;
				}
				else
				{
					z = one2.z;
					d3 = 1f;
				}
				if (x > z)
				{
					if (x > y)
					{
						result = FImp_ColliderData_Box.ProjectPointOnPlane(vector * d, point, x);
					}
					else
					{
						result = FImp_ColliderData_Box.ProjectPointOnPlane(vector2 * d2, point, y);
					}
				}
				else if (z > y)
				{
					result = FImp_ColliderData_Box.ProjectPointOnPlane(vector3 * d3, point, z);
				}
				else
				{
					result = FImp_ColliderData_Box.ProjectPointOnPlane(vector2 * d2, point, y);
				}
			}
			else if (x > y)
			{
				result = FImp_ColliderData_Box.ProjectPointOnPlane(vector * d, point, x);
			}
			else
			{
				result = FImp_ColliderData_Box.ProjectPointOnPlane(vector2 * d2, point, y);
			}
			return result;
		}

		// Token: 0x06000194 RID: 404 RVA: 0x0000D96F File Offset: 0x0000BB6F
		private static float PlaneDistance(Vector3 planeCenter, Vector3 planeNormal, Vector3 point)
		{
			return Vector3.Dot(point - planeCenter, planeNormal);
		}

		// Token: 0x06000195 RID: 405 RVA: 0x0000D980 File Offset: 0x0000BB80
		private static Vector3 ProjectPointOnPlane(Vector3 planeNormal, Vector3 point, float distance)
		{
			Vector3 b = planeNormal.normalized * distance;
			return point + b;
		}

		// Token: 0x06000196 RID: 406 RVA: 0x0000D9A2 File Offset: 0x0000BBA2
		private static bool SphereInsidePlane(float planeDistance, float pointRadius)
		{
			return -planeDistance > pointRadius;
		}

		// Token: 0x06000197 RID: 407 RVA: 0x0000D9A9 File Offset: 0x0000BBA9
		private static bool SphereOutsidePlane(float planeDistance, float pointRadius)
		{
			return planeDistance > pointRadius;
		}

		// Token: 0x06000198 RID: 408 RVA: 0x0000D9AF File Offset: 0x0000BBAF
		private static bool SphereIntersectsPlane(float planeDistance, float pointRadius)
		{
			return Mathf.Abs(planeDistance) <= pointRadius;
		}

		// Token: 0x06000199 RID: 409 RVA: 0x0000D9C0 File Offset: 0x0000BBC0
		public static bool IsInsideBoxCollider(BoxCollider collider, Vector3 point, bool is2D = false)
		{
			point = collider.transform.InverseTransformPoint(point) - collider.center;
			float num = collider.size.x * 0.5f;
			float num2 = collider.size.y * 0.5f;
			float num3 = collider.size.z * 0.5f;
			return point.x < num && point.x > -num && point.y < num2 && point.y > -num2 && point.z < num3 && point.z > -num3;
		}

		// Token: 0x0600019A RID: 410 RVA: 0x0000DA58 File Offset: 0x0000BC58
		public static bool IsInsideBoxCollider(BoxCollider2D collider, Vector3 point)
		{
			point = collider.transform.InverseTransformPoint(point) - collider.offset;
			float num = collider.size.x * 0.5f;
			float num2 = collider.size.y * 0.5f;
			return point.x < num && point.x > -num && point.y < num2 && point.y > -num2;
		}

		// Token: 0x0600019B RID: 411 RVA: 0x0000DAD4 File Offset: 0x0000BCD4
		protected static float GetBoxAverageScale(BoxCollider box)
		{
			Vector3 vector = box.transform.lossyScale;
			vector = Vector3.Scale(vector, box.size);
			return (vector.x + vector.y + vector.z) / 3f;
		}

		// Token: 0x0600019C RID: 412 RVA: 0x0000DB14 File Offset: 0x0000BD14
		protected static Vector3 GetBoxCenter(BoxCollider box)
		{
			return box.transform.position + box.transform.TransformVector(box.center);
		}

		// Token: 0x0600019D RID: 413 RVA: 0x0000DB37 File Offset: 0x0000BD37
		protected static Vector3 GetBoxCenter(BoxCollider2D box)
		{
			return box.transform.position + box.transform.TransformVector(box.offset);
		}

		// Token: 0x0600019E RID: 414 RVA: 0x0000DB60 File Offset: 0x0000BD60
		protected static Vector3 GetTargetPlaneNormal(BoxCollider boxCollider, Vector3 point, bool is2D = false)
		{
			Vector3 vector = boxCollider.transform.TransformVector(Vector3.right / 2f * boxCollider.size.x);
			Vector3 vector2 = boxCollider.transform.TransformVector(Vector3.up / 2f * boxCollider.size.y);
			Vector3 vector3 = Vector3.forward;
			if (!is2D)
			{
				vector3 = boxCollider.transform.TransformVector(Vector3.forward / 2f * boxCollider.size.z);
			}
			Vector3 vector4 = Vector3.Scale(boxCollider.size, boxCollider.transform.lossyScale);
			vector4.Normalize();
			return FImp_ColliderData_Box.GetTargetPlaneNormal(boxCollider, point, vector, vector2, vector3, vector4, is2D);
		}

		// Token: 0x0600019F RID: 415 RVA: 0x0000DC24 File Offset: 0x0000BE24
		protected static Vector3 GetTargetPlaneNormal(BoxCollider boxCollider, Vector3 point, Vector3 right, Vector3 up, Vector3 forward, Vector3 scales, bool is2D = false)
		{
			Vector3 normalized = (FImp_ColliderData_Box.GetBoxCenter(boxCollider) - point).normalized;
			Vector3 vector;
			vector.x = Vector3.Dot(normalized, right.normalized);
			vector.y = Vector3.Dot(normalized, up.normalized);
			vector.x = vector.x * scales.y * scales.z;
			vector.y = vector.y * scales.x * scales.z;
			if (!is2D)
			{
				vector.z = Vector3.Dot(normalized, forward.normalized);
				vector.z = vector.z * scales.y * scales.x;
			}
			else
			{
				vector.z = 0f;
			}
			vector.Normalize();
			Vector3 vector2 = vector;
			if (vector.x < 0f)
			{
				vector2.x = -vector.x;
			}
			if (vector.y < 0f)
			{
				vector2.y = -vector.y;
			}
			if (vector.z < 0f)
			{
				vector2.z = -vector.z;
			}
			Vector3 result;
			if (vector2.x > vector2.y)
			{
				if (vector2.x > vector2.z || is2D)
				{
					result = right * Mathf.Sign(vector.x);
				}
				else
				{
					result = forward * Mathf.Sign(vector.z);
				}
			}
			else if (vector2.y > vector2.z || is2D)
			{
				result = up * Mathf.Sign(vector.y);
			}
			else
			{
				result = forward * Mathf.Sign(vector.z);
			}
			return result;
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x0000DDCC File Offset: 0x0000BFCC
		protected static Vector3 GetTargetPlaneNormal(BoxCollider2D boxCollider, Vector2 point, Vector2 right, Vector2 up, Vector2 scales)
		{
			Vector2 normalized = (FImp_ColliderData_Box.GetBoxCenter(boxCollider) - point).normalized;
			Vector2 vector;
			vector.x = Vector3.Dot(normalized, right.normalized);
			vector.y = Vector3.Dot(normalized, up.normalized);
			vector.x *= scales.y;
			vector.y *= scales.x;
			vector.Normalize();
			Vector2 vector2 = vector;
			if (vector.x < 0f)
			{
				vector2.x = -vector.x;
			}
			if (vector.y < 0f)
			{
				vector2.y = -vector.y;
			}
			Vector3 result;
			if (vector2.x > vector2.y)
			{
				result = right * Mathf.Sign(vector.x);
			}
			else
			{
				result = up * Mathf.Sign(vector.y);
			}
			return result;
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x0000DED8 File Offset: 0x0000C0D8
		protected static Vector3 GetIntersectOnBoxFromInside(BoxCollider boxCollider, Vector3 from, Vector3 to, Vector3 planeNormal)
		{
			Vector3 direction = to - from;
			Plane plane = new Plane(-planeNormal, FImp_ColliderData_Box.GetBoxCenter(boxCollider) + planeNormal);
			Vector3 result = to;
			float distance = 0f;
			Ray ray = new Ray(from, direction);
			if (plane.Raycast(ray, out distance))
			{
				result = ray.GetPoint(distance);
			}
			return result;
		}

		// Token: 0x040001D2 RID: 466
		[CompilerGenerated]
		private BoxCollider <Box>k__BackingField;

		// Token: 0x040001D3 RID: 467
		[CompilerGenerated]
		private BoxCollider2D <Box2D>k__BackingField;

		// Token: 0x040001D4 RID: 468
		private Vector3 boxCenter;

		// Token: 0x040001D5 RID: 469
		private Vector3 right;

		// Token: 0x040001D6 RID: 470
		private Vector3 up;

		// Token: 0x040001D7 RID: 471
		private Vector3 forward;

		// Token: 0x040001D8 RID: 472
		private Vector3 rightN;

		// Token: 0x040001D9 RID: 473
		private Vector3 upN;

		// Token: 0x040001DA RID: 474
		private Vector3 forwardN;

		// Token: 0x040001DB RID: 475
		private Vector3 scales;
	}
}
