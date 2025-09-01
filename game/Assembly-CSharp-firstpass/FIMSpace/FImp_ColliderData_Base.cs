using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace FIMSpace
{
	// Token: 0x0200003E RID: 62
	public abstract class FImp_ColliderData_Base
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000173 RID: 371 RVA: 0x0000BF6F File Offset: 0x0000A16F
		// (set) Token: 0x06000174 RID: 372 RVA: 0x0000BF77 File Offset: 0x0000A177
		public Transform Transform
		{
			[CompilerGenerated]
			get
			{
				return this.<Transform>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<Transform>k__BackingField = value;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000175 RID: 373 RVA: 0x0000BF80 File Offset: 0x0000A180
		// (set) Token: 0x06000176 RID: 374 RVA: 0x0000BF88 File Offset: 0x0000A188
		public Collider Collider
		{
			[CompilerGenerated]
			get
			{
				return this.<Collider>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<Collider>k__BackingField = value;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000177 RID: 375 RVA: 0x0000BF91 File Offset: 0x0000A191
		// (set) Token: 0x06000178 RID: 376 RVA: 0x0000BF99 File Offset: 0x0000A199
		public Collider2D Collider2D
		{
			[CompilerGenerated]
			get
			{
				return this.<Collider2D>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<Collider2D>k__BackingField = value;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000179 RID: 377 RVA: 0x0000BFA2 File Offset: 0x0000A1A2
		// (set) Token: 0x0600017A RID: 378 RVA: 0x0000BFAA File Offset: 0x0000A1AA
		public bool IsStatic
		{
			[CompilerGenerated]
			get
			{
				return this.<IsStatic>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<IsStatic>k__BackingField = value;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600017B RID: 379 RVA: 0x0000BFB3 File Offset: 0x0000A1B3
		// (set) Token: 0x0600017C RID: 380 RVA: 0x0000BFBB File Offset: 0x0000A1BB
		public FImp_ColliderData_Base.EFColliderType ColliderType
		{
			[CompilerGenerated]
			get
			{
				return this.<ColliderType>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<ColliderType>k__BackingField = value;
			}
		}

		// Token: 0x0600017D RID: 381 RVA: 0x0000BFC4 File Offset: 0x0000A1C4
		public static FImp_ColliderData_Base GetColliderDataFor(Collider collider)
		{
			SphereCollider sphereCollider = collider as SphereCollider;
			if (sphereCollider)
			{
				return new FImp_ColliderData_Sphere(sphereCollider);
			}
			CapsuleCollider capsuleCollider = collider as CapsuleCollider;
			if (capsuleCollider)
			{
				return new FImp_ColliderData_Capsule(capsuleCollider);
			}
			BoxCollider boxCollider = collider as BoxCollider;
			if (boxCollider)
			{
				return new FImp_ColliderData_Box(boxCollider);
			}
			MeshCollider meshCollider = collider as MeshCollider;
			if (meshCollider)
			{
				return new FImp_ColliderData_Mesh(meshCollider);
			}
			TerrainCollider terrainCollider = collider as TerrainCollider;
			if (terrainCollider)
			{
				return new FImp_ColliderData_Terrain(terrainCollider);
			}
			return null;
		}

		// Token: 0x0600017E RID: 382 RVA: 0x0000C044 File Offset: 0x0000A244
		public static FImp_ColliderData_Base GetColliderDataFor(Collider2D collider)
		{
			CircleCollider2D circleCollider2D = collider as CircleCollider2D;
			if (circleCollider2D)
			{
				return new FImp_ColliderData_Sphere(circleCollider2D);
			}
			CapsuleCollider2D capsuleCollider2D = collider as CapsuleCollider2D;
			if (capsuleCollider2D)
			{
				return new FImp_ColliderData_Capsule(capsuleCollider2D);
			}
			BoxCollider2D boxCollider2D = collider as BoxCollider2D;
			if (boxCollider2D)
			{
				return new FImp_ColliderData_Box(boxCollider2D);
			}
			PolygonCollider2D polygonCollider2D = collider as PolygonCollider2D;
			if (polygonCollider2D)
			{
				return new FImp_ColliderData_Mesh(polygonCollider2D);
			}
			return null;
		}

		// Token: 0x0600017F RID: 383 RVA: 0x0000C0AA File Offset: 0x0000A2AA
		public virtual void RefreshColliderData()
		{
			if (this.Transform.gameObject.isStatic)
			{
				this.IsStatic = true;
				return;
			}
			this.IsStatic = false;
		}

		// Token: 0x06000180 RID: 384 RVA: 0x0000C0CD File Offset: 0x0000A2CD
		public virtual bool PushIfInside(ref Vector3 point, float pointRadius, Vector3 pointOffset)
		{
			if (this.Collider as SphereCollider)
			{
				Debug.Log("It shouldn't appear");
			}
			return false;
		}

		// Token: 0x06000181 RID: 385 RVA: 0x0000C0EC File Offset: 0x0000A2EC
		public virtual bool PushIfInside2D(ref Vector3 point, float pointRadius, Vector3 pointOffset)
		{
			return this.PushIfInside(ref point, pointRadius, pointOffset);
		}

		// Token: 0x06000182 RID: 386 RVA: 0x0000C0F7 File Offset: 0x0000A2F7
		public static bool VIsSame(Vector3 vec1, Vector3 vec2)
		{
			return vec1.x == vec2.x && vec1.y == vec2.y && vec1.z == vec2.z;
		}

		// Token: 0x06000183 RID: 387 RVA: 0x0000C12A File Offset: 0x0000A32A
		protected FImp_ColliderData_Base()
		{
		}

		// Token: 0x040001C9 RID: 457
		[CompilerGenerated]
		private Transform <Transform>k__BackingField;

		// Token: 0x040001CA RID: 458
		[CompilerGenerated]
		private Collider <Collider>k__BackingField;

		// Token: 0x040001CB RID: 459
		[CompilerGenerated]
		private Collider2D <Collider2D>k__BackingField;

		// Token: 0x040001CC RID: 460
		public bool Is2D;

		// Token: 0x040001CD RID: 461
		[CompilerGenerated]
		private bool <IsStatic>k__BackingField;

		// Token: 0x040001CE RID: 462
		[CompilerGenerated]
		private FImp_ColliderData_Base.EFColliderType <ColliderType>k__BackingField;

		// Token: 0x040001CF RID: 463
		protected Vector3 previousPosition = Vector3.zero;

		// Token: 0x040001D0 RID: 464
		protected Quaternion previousRotation = Quaternion.identity;

		// Token: 0x040001D1 RID: 465
		protected Vector3 previousScale = Vector3.one;

		// Token: 0x0200019D RID: 413
		public enum EFColliderType
		{
			// Token: 0x04000CBF RID: 3263
			Box,
			// Token: 0x04000CC0 RID: 3264
			Sphere,
			// Token: 0x04000CC1 RID: 3265
			Capsule,
			// Token: 0x04000CC2 RID: 3266
			Mesh,
			// Token: 0x04000CC3 RID: 3267
			Terrain
		}
	}
}
