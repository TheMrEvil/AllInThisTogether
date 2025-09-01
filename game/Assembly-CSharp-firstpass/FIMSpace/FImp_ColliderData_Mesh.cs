using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace FIMSpace
{
	// Token: 0x02000041 RID: 65
	public class FImp_ColliderData_Mesh : FImp_ColliderData_Base
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x0000E780 File Offset: 0x0000C980
		// (set) Token: 0x060001B3 RID: 435 RVA: 0x0000E788 File Offset: 0x0000C988
		public MeshCollider Mesh
		{
			[CompilerGenerated]
			get
			{
				return this.<Mesh>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Mesh>k__BackingField = value;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x0000E791 File Offset: 0x0000C991
		// (set) Token: 0x060001B5 RID: 437 RVA: 0x0000E799 File Offset: 0x0000C999
		public PolygonCollider2D Poly2D
		{
			[CompilerGenerated]
			get
			{
				return this.<Poly2D>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Poly2D>k__BackingField = value;
			}
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x0000E7A2 File Offset: 0x0000C9A2
		public FImp_ColliderData_Mesh(MeshCollider collider)
		{
			this.Is2D = false;
			base.Transform = collider.transform;
			base.Collider = collider;
			this.Mesh = collider;
			base.ColliderType = FImp_ColliderData_Base.EFColliderType.Mesh;
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x0000E7D4 File Offset: 0x0000C9D4
		public FImp_ColliderData_Mesh(PolygonCollider2D collider)
		{
			this.Is2D = true;
			base.Transform = collider.transform;
			this.Poly2D = collider;
			base.Collider2D = collider;
			base.ColliderType = FImp_ColliderData_Base.EFColliderType.Mesh;
			this.filter = default(ContactFilter2D);
			this.filter.useTriggers = false;
			this.filter.useDepth = false;
			this.r = new RaycastHit2D[1];
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x0000E840 File Offset: 0x0000CA40
		public override bool PushIfInside(ref Vector3 segmentPosition, float segmentRadius, Vector3 segmentOffset)
		{
			if (!this.Is2D)
			{
				if (!this.Mesh.convex)
				{
					Vector3 vector = segmentPosition + segmentOffset;
					Vector3 vector2 = this.Mesh.ClosestPointOnBounds(vector);
					float magnitude = (vector2 - this.Mesh.transform.position).magnitude;
					bool flag = false;
					float num = 1f;
					if (vector2 == vector)
					{
						flag = true;
						num = 7f;
						vector2 = this.Mesh.transform.position;
					}
					Vector3 vector3 = vector2 - vector;
					Vector3 normalized = vector3.normalized;
					Vector3 origin = vector - normalized * (segmentRadius * 2f + this.Mesh.bounds.extents.magnitude);
					float maxDistance = vector3.magnitude + segmentRadius * 2f + magnitude + this.Mesh.bounds.extents.magnitude;
					if ((vector - vector2).magnitude < segmentRadius * num)
					{
						Ray ray = new Ray(origin, normalized);
						RaycastHit raycastHit;
						if (this.Mesh.Raycast(ray, out raycastHit, maxDistance) && (vector - raycastHit.point).magnitude < segmentRadius * num)
						{
							Vector3 a = raycastHit.point - vector;
							Vector3 b;
							if (flag)
							{
								b = a + a.normalized * segmentRadius;
							}
							else
							{
								b = a - a.normalized * segmentRadius;
							}
							float num2 = Vector3.Dot((raycastHit.point - vector).normalized, normalized);
							if (flag && num2 > 0f)
							{
								b = a - a.normalized * segmentRadius;
							}
							segmentPosition += b;
							return true;
						}
					}
					return false;
				}
				Vector3 vector4 = segmentPosition + segmentOffset;
				float num3 = 1f;
				Vector3 a2 = Physics.ClosestPoint(vector4, this.Mesh, this.Mesh.transform.position, this.Mesh.transform.rotation);
				if (Vector3.Distance(a2, vector4) > segmentRadius * 1.01f)
				{
					return false;
				}
				Vector3 lhs = a2 - vector4;
				if (lhs == Vector3.zero)
				{
					return false;
				}
				RaycastHit raycastHit2;
				this.Mesh.Raycast(new Ray(vector4, lhs.normalized), out raycastHit2, segmentRadius * num3);
				if (raycastHit2.transform)
				{
					segmentPosition = raycastHit2.point + raycastHit2.normal * segmentRadius;
					return true;
				}
			}
			else
			{
				Vector2 vector5 = segmentPosition + segmentOffset;
				Vector2 a3;
				if (this.Poly2D.OverlapPoint(vector5))
				{
					Vector3 vector6 = this.Poly2D.bounds.center - vector5;
					vector6.z = 0f;
					Ray ray2 = new Ray(this.Poly2D.bounds.center - vector6 * this.Poly2D.bounds.max.magnitude, vector6);
					float num4 = 0f;
					this.Poly2D.bounds.IntersectRay(ray2, out num4);
					if (num4 > 0f)
					{
						a3 = this.Poly2D.ClosestPoint(ray2.GetPoint(num4));
					}
					else
					{
						a3 = this.Poly2D.ClosestPoint(vector5);
					}
				}
				else
				{
					a3 = this.Poly2D.ClosestPoint(vector5);
				}
				Vector2 normalized2 = (a3 - vector5).normalized;
				if (Physics2D.Raycast(vector5, normalized2, this.filter, this.r, segmentRadius) > 0 && this.r[0].transform == base.Transform)
				{
					segmentPosition = a3 + this.r[0].normal * segmentRadius;
					return true;
				}
			}
			return false;
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x0000EC84 File Offset: 0x0000CE84
		public static void PushOutFromMeshCollider(MeshCollider mesh, Collision collision, float segmentColliderRadius, ref Vector3 pos)
		{
			Vector3 point = collision.contacts[0].point;
			Vector3 a = collision.contacts[0].normal;
			RaycastHit raycastHit;
			if (mesh.Raycast(new Ray(pos + a * segmentColliderRadius * 2f, -a), out raycastHit, segmentColliderRadius * 5f))
			{
				a = raycastHit.point - pos;
				float sqrMagnitude = a.sqrMagnitude;
				if (sqrMagnitude > 0f && sqrMagnitude < segmentColliderRadius * segmentColliderRadius)
				{
					pos = raycastHit.point - a * (segmentColliderRadius / Mathf.Sqrt(sqrMagnitude)) * 0.9f;
					return;
				}
			}
			else
			{
				a = point - pos;
				float sqrMagnitude2 = a.sqrMagnitude;
				if (sqrMagnitude2 > 0f && sqrMagnitude2 < segmentColliderRadius * segmentColliderRadius)
				{
					pos = point - a * (segmentColliderRadius / Mathf.Sqrt(sqrMagnitude2)) * 0.9f;
				}
			}
		}

		// Token: 0x060001BA RID: 442 RVA: 0x0000ED8C File Offset: 0x0000CF8C
		public static void PushOutFromMesh(MeshCollider mesh, Collision collision, float pointRadius, ref Vector3 point)
		{
			Vector3 vector = mesh.ClosestPointOnBounds(point);
			float magnitude = (vector - mesh.transform.position).magnitude;
			bool flag = false;
			float num = 1f;
			if (vector == point)
			{
				flag = true;
				num = 7f;
				vector = mesh.transform.position;
			}
			Vector3 vector2 = vector - point;
			Vector3 normalized = vector2.normalized;
			Vector3 origin = point - normalized * (pointRadius * 2f + mesh.bounds.extents.magnitude);
			float maxDistance = vector2.magnitude + pointRadius * 2f + magnitude + mesh.bounds.extents.magnitude;
			if ((point - vector).magnitude < pointRadius * num)
			{
				Vector3 point2;
				if (!flag)
				{
					point2 = collision.contacts[0].point;
				}
				else
				{
					Ray ray = new Ray(origin, normalized);
					RaycastHit raycastHit;
					if (mesh.Raycast(ray, out raycastHit, maxDistance))
					{
						point2 = raycastHit.point;
					}
					else
					{
						point2 = collision.contacts[0].point;
					}
				}
				if ((point - point2).magnitude < pointRadius * num)
				{
					Vector3 a = point2 - point;
					Vector3 b;
					if (flag)
					{
						b = a + a.normalized * pointRadius;
					}
					else
					{
						b = a - a.normalized * pointRadius;
					}
					float num2 = Vector3.Dot((point2 - point).normalized, normalized);
					if (flag && num2 > 0f)
					{
						b = a - a.normalized * pointRadius;
					}
					point += b;
				}
			}
		}

		// Token: 0x040001E4 RID: 484
		[CompilerGenerated]
		private MeshCollider <Mesh>k__BackingField;

		// Token: 0x040001E5 RID: 485
		[CompilerGenerated]
		private PolygonCollider2D <Poly2D>k__BackingField;

		// Token: 0x040001E6 RID: 486
		private ContactFilter2D filter;

		// Token: 0x040001E7 RID: 487
		private RaycastHit2D[] r;
	}
}
