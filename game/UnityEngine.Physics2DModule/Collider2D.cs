using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000020 RID: 32
	[RequiredByNativeCode(Optional = true)]
	[RequireComponent(typeof(Transform))]
	[NativeHeader("Modules/Physics2D/Public/Collider2D.h")]
	public class Collider2D : Behaviour
	{
		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600030E RID: 782
		// (set) Token: 0x0600030F RID: 783
		public extern float density { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000310 RID: 784
		// (set) Token: 0x06000311 RID: 785
		public extern bool isTrigger { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000312 RID: 786
		// (set) Token: 0x06000313 RID: 787
		public extern bool usedByEffector { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000314 RID: 788
		// (set) Token: 0x06000315 RID: 789
		public extern bool usedByComposite { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000316 RID: 790
		public extern CompositeCollider2D composite { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000317 RID: 791 RVA: 0x0000768C File Offset: 0x0000588C
		// (set) Token: 0x06000318 RID: 792 RVA: 0x000076A2 File Offset: 0x000058A2
		public Vector2 offset
		{
			get
			{
				Vector2 result;
				this.get_offset_Injected(out result);
				return result;
			}
			set
			{
				this.set_offset_Injected(ref value);
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000319 RID: 793
		public extern Rigidbody2D attachedRigidbody { [NativeMethod("GetAttachedRigidbody_Binding")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600031A RID: 794
		public extern int shapeCount { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x0600031B RID: 795
		[NativeMethod("CreateMesh_Binding")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Mesh CreateMesh(bool useBodyPosition, bool useBodyRotation);

		// Token: 0x0600031C RID: 796
		[NativeMethod("GetShapeHash_Binding")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern uint GetShapeHash();

		// Token: 0x0600031D RID: 797 RVA: 0x000076AC File Offset: 0x000058AC
		public int GetShapes(PhysicsShapeGroup2D physicsShapeGroup)
		{
			return this.GetShapes_Internal(ref physicsShapeGroup.m_GroupState, 0, this.shapeCount);
		}

		// Token: 0x0600031E RID: 798 RVA: 0x000076D4 File Offset: 0x000058D4
		public int GetShapes(PhysicsShapeGroup2D physicsShapeGroup, int shapeIndex, [DefaultValue("1")] int shapeCount = 1)
		{
			int shapeCount2 = this.shapeCount;
			bool flag = shapeIndex < 0 || shapeIndex >= shapeCount2 || shapeCount < 1 || shapeIndex + shapeCount > shapeCount2;
			if (flag)
			{
				throw new ArgumentOutOfRangeException(string.Format("Cannot get shape range from {0} to {1} as Collider2D only has {2} shape(s).", shapeIndex, shapeIndex + shapeCount - 1, shapeCount2));
			}
			return this.GetShapes_Internal(ref physicsShapeGroup.m_GroupState, shapeIndex, shapeCount);
		}

		// Token: 0x0600031F RID: 799
		[NativeMethod("GetShapes_Binding")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetShapes_Internal(ref PhysicsShapeGroup2D.GroupState physicsShapeGroupState, int shapeIndex, int shapeCount);

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000320 RID: 800 RVA: 0x0000773C File Offset: 0x0000593C
		public Bounds bounds
		{
			get
			{
				Bounds result;
				this.get_bounds_Injected(out result);
				return result;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000321 RID: 801
		public extern ColliderErrorState2D errorState { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000322 RID: 802
		internal extern bool compositeCapable { [NativeMethod("GetCompositeCapable_Binding")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000323 RID: 803
		// (set) Token: 0x06000324 RID: 804
		public extern PhysicsMaterial2D sharedMaterial { [NativeMethod("GetMaterial")] [MethodImpl(MethodImplOptions.InternalCall)] get; [NativeMethod("SetMaterial")] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000325 RID: 805
		public extern float friction { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000326 RID: 806
		public extern float bounciness { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06000327 RID: 807
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool IsTouching([Writable] [NotNull("ArgumentNullException")] Collider2D collider);

		// Token: 0x06000328 RID: 808 RVA: 0x00007754 File Offset: 0x00005954
		public bool IsTouching([Writable] Collider2D collider, ContactFilter2D contactFilter)
		{
			return this.IsTouching_OtherColliderWithFilter(collider, contactFilter);
		}

		// Token: 0x06000329 RID: 809 RVA: 0x0000776E File Offset: 0x0000596E
		[NativeMethod("IsTouching")]
		private bool IsTouching_OtherColliderWithFilter([NotNull("ArgumentNullException")] [Writable] Collider2D collider, ContactFilter2D contactFilter)
		{
			return this.IsTouching_OtherColliderWithFilter_Injected(collider, ref contactFilter);
		}

		// Token: 0x0600032A RID: 810 RVA: 0x0000777C File Offset: 0x0000597C
		public bool IsTouching(ContactFilter2D contactFilter)
		{
			return this.IsTouching_AnyColliderWithFilter(contactFilter);
		}

		// Token: 0x0600032B RID: 811 RVA: 0x00007795 File Offset: 0x00005995
		[NativeMethod("IsTouching")]
		private bool IsTouching_AnyColliderWithFilter(ContactFilter2D contactFilter)
		{
			return this.IsTouching_AnyColliderWithFilter_Injected(ref contactFilter);
		}

		// Token: 0x0600032C RID: 812 RVA: 0x000077A0 File Offset: 0x000059A0
		[ExcludeFromDocs]
		public bool IsTouchingLayers()
		{
			return this.IsTouchingLayers(-1);
		}

		// Token: 0x0600032D RID: 813
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool IsTouchingLayers([DefaultValue("Physics2D.AllLayers")] int layerMask);

		// Token: 0x0600032E RID: 814 RVA: 0x000077B9 File Offset: 0x000059B9
		public bool OverlapPoint(Vector2 point)
		{
			return this.OverlapPoint_Injected(ref point);
		}

		// Token: 0x0600032F RID: 815 RVA: 0x000077C4 File Offset: 0x000059C4
		public ColliderDistance2D Distance([Writable] Collider2D collider)
		{
			return Physics2D.Distance(this, collider);
		}

		// Token: 0x06000330 RID: 816 RVA: 0x000077E0 File Offset: 0x000059E0
		public int OverlapCollider(ContactFilter2D contactFilter, Collider2D[] results)
		{
			return PhysicsScene2D.OverlapCollider(this, contactFilter, results);
		}

		// Token: 0x06000331 RID: 817 RVA: 0x000077FC File Offset: 0x000059FC
		public int OverlapCollider(ContactFilter2D contactFilter, List<Collider2D> results)
		{
			return PhysicsScene2D.OverlapCollider(this, contactFilter, results);
		}

		// Token: 0x06000332 RID: 818 RVA: 0x00007818 File Offset: 0x00005A18
		public int GetContacts(ContactPoint2D[] contacts)
		{
			return Physics2D.GetContacts(this, default(ContactFilter2D).NoFilter(), contacts);
		}

		// Token: 0x06000333 RID: 819 RVA: 0x00007840 File Offset: 0x00005A40
		public int GetContacts(List<ContactPoint2D> contacts)
		{
			return Physics2D.GetContacts(this, default(ContactFilter2D).NoFilter(), contacts);
		}

		// Token: 0x06000334 RID: 820 RVA: 0x00007868 File Offset: 0x00005A68
		public int GetContacts(ContactFilter2D contactFilter, ContactPoint2D[] contacts)
		{
			return Physics2D.GetContacts(this, contactFilter, contacts);
		}

		// Token: 0x06000335 RID: 821 RVA: 0x00007884 File Offset: 0x00005A84
		public int GetContacts(ContactFilter2D contactFilter, List<ContactPoint2D> contacts)
		{
			return Physics2D.GetContacts(this, contactFilter, contacts);
		}

		// Token: 0x06000336 RID: 822 RVA: 0x000078A0 File Offset: 0x00005AA0
		public int GetContacts(Collider2D[] colliders)
		{
			return Physics2D.GetContacts(this, default(ContactFilter2D).NoFilter(), colliders);
		}

		// Token: 0x06000337 RID: 823 RVA: 0x000078C8 File Offset: 0x00005AC8
		public int GetContacts(List<Collider2D> colliders)
		{
			return Physics2D.GetContacts(this, default(ContactFilter2D).NoFilter(), colliders);
		}

		// Token: 0x06000338 RID: 824 RVA: 0x000078F0 File Offset: 0x00005AF0
		public int GetContacts(ContactFilter2D contactFilter, Collider2D[] colliders)
		{
			return Physics2D.GetContacts(this, contactFilter, colliders);
		}

		// Token: 0x06000339 RID: 825 RVA: 0x0000790C File Offset: 0x00005B0C
		public int GetContacts(ContactFilter2D contactFilter, List<Collider2D> colliders)
		{
			return Physics2D.GetContacts(this, contactFilter, colliders);
		}

		// Token: 0x0600033A RID: 826 RVA: 0x00007928 File Offset: 0x00005B28
		[ExcludeFromDocs]
		public int Cast(Vector2 direction, RaycastHit2D[] results)
		{
			ContactFilter2D contactFilter = default(ContactFilter2D);
			contactFilter.useTriggers = Physics2D.queriesHitTriggers;
			contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(base.gameObject.layer));
			return this.CastArray_Internal(direction, float.PositiveInfinity, contactFilter, true, results);
		}

		// Token: 0x0600033B RID: 827 RVA: 0x0000797C File Offset: 0x00005B7C
		[ExcludeFromDocs]
		public int Cast(Vector2 direction, RaycastHit2D[] results, float distance)
		{
			ContactFilter2D contactFilter = default(ContactFilter2D);
			contactFilter.useTriggers = Physics2D.queriesHitTriggers;
			contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(base.gameObject.layer));
			return this.CastArray_Internal(direction, distance, contactFilter, true, results);
		}

		// Token: 0x0600033C RID: 828 RVA: 0x000079CC File Offset: 0x00005BCC
		public int Cast(Vector2 direction, RaycastHit2D[] results, [DefaultValue("Mathf.Infinity")] float distance, [DefaultValue("true")] bool ignoreSiblingColliders)
		{
			ContactFilter2D contactFilter = default(ContactFilter2D);
			contactFilter.useTriggers = Physics2D.queriesHitTriggers;
			contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(base.gameObject.layer));
			return this.CastArray_Internal(direction, distance, contactFilter, ignoreSiblingColliders, results);
		}

		// Token: 0x0600033D RID: 829 RVA: 0x00007A1C File Offset: 0x00005C1C
		[ExcludeFromDocs]
		public int Cast(Vector2 direction, ContactFilter2D contactFilter, RaycastHit2D[] results)
		{
			return this.CastArray_Internal(direction, float.PositiveInfinity, contactFilter, true, results);
		}

		// Token: 0x0600033E RID: 830 RVA: 0x00007A40 File Offset: 0x00005C40
		[ExcludeFromDocs]
		public int Cast(Vector2 direction, ContactFilter2D contactFilter, RaycastHit2D[] results, float distance)
		{
			return this.CastArray_Internal(direction, distance, contactFilter, true, results);
		}

		// Token: 0x0600033F RID: 831 RVA: 0x00007A60 File Offset: 0x00005C60
		public int Cast(Vector2 direction, ContactFilter2D contactFilter, RaycastHit2D[] results, [DefaultValue("Mathf.Infinity")] float distance, [DefaultValue("true")] bool ignoreSiblingColliders)
		{
			return this.CastArray_Internal(direction, distance, contactFilter, ignoreSiblingColliders, results);
		}

		// Token: 0x06000340 RID: 832 RVA: 0x00007A7F File Offset: 0x00005C7F
		[NativeMethod("CastArray_Binding")]
		private int CastArray_Internal(Vector2 direction, float distance, ContactFilter2D contactFilter, bool ignoreSiblingColliders, [NotNull("ArgumentNullException")] [Unmarshalled] RaycastHit2D[] results)
		{
			return this.CastArray_Internal_Injected(ref direction, distance, ref contactFilter, ignoreSiblingColliders, results);
		}

		// Token: 0x06000341 RID: 833 RVA: 0x00007A90 File Offset: 0x00005C90
		public int Cast(Vector2 direction, ContactFilter2D contactFilter, List<RaycastHit2D> results, [DefaultValue("Mathf.Infinity")] float distance = float.PositiveInfinity, [DefaultValue("true")] bool ignoreSiblingColliders = true)
		{
			return this.CastList_Internal(direction, distance, contactFilter, ignoreSiblingColliders, results);
		}

		// Token: 0x06000342 RID: 834 RVA: 0x00007AAF File Offset: 0x00005CAF
		[NativeMethod("CastList_Binding")]
		private int CastList_Internal(Vector2 direction, float distance, ContactFilter2D contactFilter, bool ignoreSiblingColliders, [NotNull("ArgumentNullException")] List<RaycastHit2D> results)
		{
			return this.CastList_Internal_Injected(ref direction, distance, ref contactFilter, ignoreSiblingColliders, results);
		}

		// Token: 0x06000343 RID: 835 RVA: 0x00007AC0 File Offset: 0x00005CC0
		[ExcludeFromDocs]
		public int Raycast(Vector2 direction, RaycastHit2D[] results)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(-1, float.NegativeInfinity, float.PositiveInfinity);
			return this.RaycastArray_Internal(direction, float.PositiveInfinity, contactFilter, results);
		}

		// Token: 0x06000344 RID: 836 RVA: 0x00007AF4 File Offset: 0x00005CF4
		[ExcludeFromDocs]
		public int Raycast(Vector2 direction, RaycastHit2D[] results, float distance)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(-1, float.NegativeInfinity, float.PositiveInfinity);
			return this.RaycastArray_Internal(direction, distance, contactFilter, results);
		}

		// Token: 0x06000345 RID: 837 RVA: 0x00007B24 File Offset: 0x00005D24
		[ExcludeFromDocs]
		public int Raycast(Vector2 direction, RaycastHit2D[] results, float distance, int layerMask)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
			return this.RaycastArray_Internal(direction, distance, contactFilter, results);
		}

		// Token: 0x06000346 RID: 838 RVA: 0x00007B54 File Offset: 0x00005D54
		[ExcludeFromDocs]
		public int Raycast(Vector2 direction, RaycastHit2D[] results, float distance, int layerMask, float minDepth)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
			return this.RaycastArray_Internal(direction, distance, contactFilter, results);
		}

		// Token: 0x06000347 RID: 839 RVA: 0x00007B80 File Offset: 0x00005D80
		public int Raycast(Vector2 direction, RaycastHit2D[] results, [DefaultValue("Mathf.Infinity")] float distance, [DefaultValue("Physics2D.AllLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
			return this.RaycastArray_Internal(direction, distance, contactFilter, results);
		}

		// Token: 0x06000348 RID: 840 RVA: 0x00007BA8 File Offset: 0x00005DA8
		[ExcludeFromDocs]
		public int Raycast(Vector2 direction, ContactFilter2D contactFilter, RaycastHit2D[] results)
		{
			return this.RaycastArray_Internal(direction, float.PositiveInfinity, contactFilter, results);
		}

		// Token: 0x06000349 RID: 841 RVA: 0x00007BC8 File Offset: 0x00005DC8
		public int Raycast(Vector2 direction, ContactFilter2D contactFilter, RaycastHit2D[] results, [DefaultValue("Mathf.Infinity")] float distance)
		{
			return this.RaycastArray_Internal(direction, distance, contactFilter, results);
		}

		// Token: 0x0600034A RID: 842 RVA: 0x00007BE5 File Offset: 0x00005DE5
		[NativeMethod("RaycastArray_Binding")]
		private int RaycastArray_Internal(Vector2 direction, float distance, ContactFilter2D contactFilter, [NotNull("ArgumentNullException")] [Unmarshalled] RaycastHit2D[] results)
		{
			return this.RaycastArray_Internal_Injected(ref direction, distance, ref contactFilter, results);
		}

		// Token: 0x0600034B RID: 843 RVA: 0x00007BF4 File Offset: 0x00005DF4
		public int Raycast(Vector2 direction, ContactFilter2D contactFilter, List<RaycastHit2D> results, [DefaultValue("Mathf.Infinity")] float distance = float.PositiveInfinity)
		{
			return this.RaycastList_Internal(direction, distance, contactFilter, results);
		}

		// Token: 0x0600034C RID: 844 RVA: 0x00007C11 File Offset: 0x00005E11
		[NativeMethod("RaycastList_Binding")]
		private int RaycastList_Internal(Vector2 direction, float distance, ContactFilter2D contactFilter, [NotNull("ArgumentNullException")] List<RaycastHit2D> results)
		{
			return this.RaycastList_Internal_Injected(ref direction, distance, ref contactFilter, results);
		}

		// Token: 0x0600034D RID: 845 RVA: 0x00007C20 File Offset: 0x00005E20
		public Vector2 ClosestPoint(Vector2 position)
		{
			return Physics2D.ClosestPoint(position, this);
		}

		// Token: 0x0600034E RID: 846 RVA: 0x00007C39 File Offset: 0x00005E39
		public Collider2D()
		{
		}

		// Token: 0x0600034F RID: 847
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_offset_Injected(out Vector2 ret);

		// Token: 0x06000350 RID: 848
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_offset_Injected(ref Vector2 value);

		// Token: 0x06000351 RID: 849
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_bounds_Injected(out Bounds ret);

		// Token: 0x06000352 RID: 850
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool IsTouching_OtherColliderWithFilter_Injected([Writable] Collider2D collider, ref ContactFilter2D contactFilter);

		// Token: 0x06000353 RID: 851
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool IsTouching_AnyColliderWithFilter_Injected(ref ContactFilter2D contactFilter);

		// Token: 0x06000354 RID: 852
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool OverlapPoint_Injected(ref Vector2 point);

		// Token: 0x06000355 RID: 853
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int CastArray_Internal_Injected(ref Vector2 direction, float distance, ref ContactFilter2D contactFilter, bool ignoreSiblingColliders, RaycastHit2D[] results);

		// Token: 0x06000356 RID: 854
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int CastList_Internal_Injected(ref Vector2 direction, float distance, ref ContactFilter2D contactFilter, bool ignoreSiblingColliders, List<RaycastHit2D> results);

		// Token: 0x06000357 RID: 855
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int RaycastArray_Internal_Injected(ref Vector2 direction, float distance, ref ContactFilter2D contactFilter, RaycastHit2D[] results);

		// Token: 0x06000358 RID: 856
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int RaycastList_Internal_Injected(ref Vector2 direction, float distance, ref ContactFilter2D contactFilter, List<RaycastHit2D> results);
	}
}
