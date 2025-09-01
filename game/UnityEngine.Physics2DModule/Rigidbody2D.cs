using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x0200001F RID: 31
	[RequireComponent(typeof(Transform))]
	[NativeHeader("Modules/Physics2D/Public/Rigidbody2D.h")]
	public sealed class Rigidbody2D : Component
	{
		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600027F RID: 639 RVA: 0x00007120 File Offset: 0x00005320
		// (set) Token: 0x06000280 RID: 640 RVA: 0x00007136 File Offset: 0x00005336
		public Vector2 position
		{
			get
			{
				Vector2 result;
				this.get_position_Injected(out result);
				return result;
			}
			set
			{
				this.set_position_Injected(ref value);
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000281 RID: 641
		// (set) Token: 0x06000282 RID: 642
		public extern float rotation { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06000283 RID: 643 RVA: 0x00007140 File Offset: 0x00005340
		public void SetRotation(float angle)
		{
			this.SetRotation_Angle(angle);
		}

		// Token: 0x06000284 RID: 644
		[NativeMethod("SetRotation")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetRotation_Angle(float angle);

		// Token: 0x06000285 RID: 645 RVA: 0x0000714B File Offset: 0x0000534B
		public void SetRotation(Quaternion rotation)
		{
			this.SetRotation_Quaternion(rotation);
		}

		// Token: 0x06000286 RID: 646 RVA: 0x00007156 File Offset: 0x00005356
		[NativeMethod("SetRotation")]
		private void SetRotation_Quaternion(Quaternion rotation)
		{
			this.SetRotation_Quaternion_Injected(ref rotation);
		}

		// Token: 0x06000287 RID: 647 RVA: 0x00007160 File Offset: 0x00005360
		public void MovePosition(Vector2 position)
		{
			this.MovePosition_Injected(ref position);
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000716A File Offset: 0x0000536A
		public void MoveRotation(float angle)
		{
			this.MoveRotation_Angle(angle);
		}

		// Token: 0x06000289 RID: 649
		[NativeMethod("MoveRotation")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void MoveRotation_Angle(float angle);

		// Token: 0x0600028A RID: 650 RVA: 0x00007175 File Offset: 0x00005375
		public void MoveRotation(Quaternion rotation)
		{
			this.MoveRotation_Quaternion(rotation);
		}

		// Token: 0x0600028B RID: 651 RVA: 0x00007180 File Offset: 0x00005380
		[NativeMethod("MoveRotation")]
		private void MoveRotation_Quaternion(Quaternion rotation)
		{
			this.MoveRotation_Quaternion_Injected(ref rotation);
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600028C RID: 652 RVA: 0x0000718C File Offset: 0x0000538C
		// (set) Token: 0x0600028D RID: 653 RVA: 0x000071A2 File Offset: 0x000053A2
		public Vector2 velocity
		{
			get
			{
				Vector2 result;
				this.get_velocity_Injected(out result);
				return result;
			}
			set
			{
				this.set_velocity_Injected(ref value);
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600028E RID: 654
		// (set) Token: 0x0600028F RID: 655
		public extern float angularVelocity { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000290 RID: 656
		// (set) Token: 0x06000291 RID: 657
		public extern bool useAutoMass { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000292 RID: 658
		// (set) Token: 0x06000293 RID: 659
		public extern float mass { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000294 RID: 660
		// (set) Token: 0x06000295 RID: 661
		[NativeMethod("Material")]
		public extern PhysicsMaterial2D sharedMaterial { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000296 RID: 662 RVA: 0x000071AC File Offset: 0x000053AC
		// (set) Token: 0x06000297 RID: 663 RVA: 0x000071C2 File Offset: 0x000053C2
		public Vector2 centerOfMass
		{
			get
			{
				Vector2 result;
				this.get_centerOfMass_Injected(out result);
				return result;
			}
			set
			{
				this.set_centerOfMass_Injected(ref value);
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000298 RID: 664 RVA: 0x000071CC File Offset: 0x000053CC
		public Vector2 worldCenterOfMass
		{
			get
			{
				Vector2 result;
				this.get_worldCenterOfMass_Injected(out result);
				return result;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000299 RID: 665
		// (set) Token: 0x0600029A RID: 666
		public extern float inertia { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600029B RID: 667
		// (set) Token: 0x0600029C RID: 668
		public extern float drag { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600029D RID: 669
		// (set) Token: 0x0600029E RID: 670
		public extern float angularDrag { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600029F RID: 671
		// (set) Token: 0x060002A0 RID: 672
		public extern float gravityScale { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060002A1 RID: 673
		// (set) Token: 0x060002A2 RID: 674
		public extern RigidbodyType2D bodyType { [MethodImpl(MethodImplOptions.InternalCall)] get; [NativeMethod("SetBodyType_Binding")] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x060002A3 RID: 675
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void SetDragBehaviour(bool dragged);

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060002A4 RID: 676
		// (set) Token: 0x060002A5 RID: 677
		public extern bool useFullKinematicContacts { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060002A6 RID: 678 RVA: 0x000071E4 File Offset: 0x000053E4
		// (set) Token: 0x060002A7 RID: 679 RVA: 0x000071FF File Offset: 0x000053FF
		public bool isKinematic
		{
			get
			{
				return this.bodyType == RigidbodyType2D.Kinematic;
			}
			set
			{
				this.bodyType = (value ? RigidbodyType2D.Kinematic : RigidbodyType2D.Dynamic);
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060002A8 RID: 680
		// (set) Token: 0x060002A9 RID: 681
		[Obsolete("'fixedAngle' is no longer supported. Use constraints instead.", false)]
		[NativeMethod("FreezeRotation")]
		public extern bool fixedAngle { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060002AA RID: 682
		// (set) Token: 0x060002AB RID: 683
		public extern bool freezeRotation { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060002AC RID: 684
		// (set) Token: 0x060002AD RID: 685
		public extern RigidbodyConstraints2D constraints { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x060002AE RID: 686
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool IsSleeping();

		// Token: 0x060002AF RID: 687
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool IsAwake();

		// Token: 0x060002B0 RID: 688
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Sleep();

		// Token: 0x060002B1 RID: 689
		[NativeMethod("Wake")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void WakeUp();

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060002B2 RID: 690
		// (set) Token: 0x060002B3 RID: 691
		public extern bool simulated { [MethodImpl(MethodImplOptions.InternalCall)] get; [NativeMethod("SetSimulated_Binding")] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060002B4 RID: 692
		// (set) Token: 0x060002B5 RID: 693
		public extern RigidbodyInterpolation2D interpolation { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060002B6 RID: 694
		// (set) Token: 0x060002B7 RID: 695
		public extern RigidbodySleepMode2D sleepMode { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060002B8 RID: 696
		// (set) Token: 0x060002B9 RID: 697
		public extern CollisionDetectionMode2D collisionDetectionMode { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060002BA RID: 698
		public extern int attachedColliderCount { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x060002BB RID: 699
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool IsTouching([Writable] [NotNull("ArgumentNullException")] Collider2D collider);

		// Token: 0x060002BC RID: 700 RVA: 0x00007210 File Offset: 0x00005410
		public bool IsTouching([Writable] Collider2D collider, ContactFilter2D contactFilter)
		{
			return this.IsTouching_OtherColliderWithFilter_Internal(collider, contactFilter);
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0000722A File Offset: 0x0000542A
		[NativeMethod("IsTouching")]
		private bool IsTouching_OtherColliderWithFilter_Internal([NotNull("ArgumentNullException")] [Writable] Collider2D collider, ContactFilter2D contactFilter)
		{
			return this.IsTouching_OtherColliderWithFilter_Internal_Injected(collider, ref contactFilter);
		}

		// Token: 0x060002BE RID: 702 RVA: 0x00007238 File Offset: 0x00005438
		public bool IsTouching(ContactFilter2D contactFilter)
		{
			return this.IsTouching_AnyColliderWithFilter_Internal(contactFilter);
		}

		// Token: 0x060002BF RID: 703 RVA: 0x00007251 File Offset: 0x00005451
		[NativeMethod("IsTouching")]
		private bool IsTouching_AnyColliderWithFilter_Internal(ContactFilter2D contactFilter)
		{
			return this.IsTouching_AnyColliderWithFilter_Internal_Injected(ref contactFilter);
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x0000725C File Offset: 0x0000545C
		[ExcludeFromDocs]
		public bool IsTouchingLayers()
		{
			return this.IsTouchingLayers(-1);
		}

		// Token: 0x060002C1 RID: 705
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool IsTouchingLayers([DefaultValue("Physics2D.AllLayers")] int layerMask);

		// Token: 0x060002C2 RID: 706 RVA: 0x00007275 File Offset: 0x00005475
		public bool OverlapPoint(Vector2 point)
		{
			return this.OverlapPoint_Injected(ref point);
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x00007280 File Offset: 0x00005480
		public ColliderDistance2D Distance([Writable] Collider2D collider)
		{
			bool flag = collider == null;
			if (flag)
			{
				throw new ArgumentNullException("Collider cannot be null.");
			}
			bool flag2 = collider.attachedRigidbody == this;
			if (flag2)
			{
				throw new ArgumentException("The collider cannot be attached to the Rigidbody2D being searched.");
			}
			return this.Distance_Internal(collider);
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x000072CC File Offset: 0x000054CC
		[NativeMethod("Distance")]
		private ColliderDistance2D Distance_Internal([NotNull("ArgumentNullException")] [Writable] Collider2D collider)
		{
			ColliderDistance2D result;
			this.Distance_Internal_Injected(collider, out result);
			return result;
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x000072E4 File Offset: 0x000054E4
		public Vector2 ClosestPoint(Vector2 position)
		{
			return Physics2D.ClosestPoint(position, this);
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x000072FD File Offset: 0x000054FD
		[ExcludeFromDocs]
		public void AddForce(Vector2 force)
		{
			this.AddForce(force, ForceMode2D.Force);
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x00007309 File Offset: 0x00005509
		public void AddForce(Vector2 force, [DefaultValue("ForceMode2D.Force")] ForceMode2D mode)
		{
			this.AddForce_Injected(ref force, mode);
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x00007314 File Offset: 0x00005514
		[ExcludeFromDocs]
		public void AddRelativeForce(Vector2 relativeForce)
		{
			this.AddRelativeForce(relativeForce, ForceMode2D.Force);
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x00007320 File Offset: 0x00005520
		public void AddRelativeForce(Vector2 relativeForce, [DefaultValue("ForceMode2D.Force")] ForceMode2D mode)
		{
			this.AddRelativeForce_Injected(ref relativeForce, mode);
		}

		// Token: 0x060002CA RID: 714 RVA: 0x0000732B File Offset: 0x0000552B
		[ExcludeFromDocs]
		public void AddForceAtPosition(Vector2 force, Vector2 position)
		{
			this.AddForceAtPosition(force, position, ForceMode2D.Force);
		}

		// Token: 0x060002CB RID: 715 RVA: 0x00007338 File Offset: 0x00005538
		public void AddForceAtPosition(Vector2 force, Vector2 position, [DefaultValue("ForceMode2D.Force")] ForceMode2D mode)
		{
			this.AddForceAtPosition_Injected(ref force, ref position, mode);
		}

		// Token: 0x060002CC RID: 716 RVA: 0x00007345 File Offset: 0x00005545
		[ExcludeFromDocs]
		public void AddTorque(float torque)
		{
			this.AddTorque(torque, ForceMode2D.Force);
		}

		// Token: 0x060002CD RID: 717
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void AddTorque(float torque, [DefaultValue("ForceMode2D.Force")] ForceMode2D mode);

		// Token: 0x060002CE RID: 718 RVA: 0x00007354 File Offset: 0x00005554
		public Vector2 GetPoint(Vector2 point)
		{
			Vector2 result;
			this.GetPoint_Injected(ref point, out result);
			return result;
		}

		// Token: 0x060002CF RID: 719 RVA: 0x0000736C File Offset: 0x0000556C
		public Vector2 GetRelativePoint(Vector2 relativePoint)
		{
			Vector2 result;
			this.GetRelativePoint_Injected(ref relativePoint, out result);
			return result;
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x00007384 File Offset: 0x00005584
		public Vector2 GetVector(Vector2 vector)
		{
			Vector2 result;
			this.GetVector_Injected(ref vector, out result);
			return result;
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0000739C File Offset: 0x0000559C
		public Vector2 GetRelativeVector(Vector2 relativeVector)
		{
			Vector2 result;
			this.GetRelativeVector_Injected(ref relativeVector, out result);
			return result;
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x000073B4 File Offset: 0x000055B4
		public Vector2 GetPointVelocity(Vector2 point)
		{
			Vector2 result;
			this.GetPointVelocity_Injected(ref point, out result);
			return result;
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x000073CC File Offset: 0x000055CC
		public Vector2 GetRelativePointVelocity(Vector2 relativePoint)
		{
			Vector2 result;
			this.GetRelativePointVelocity_Injected(ref relativePoint, out result);
			return result;
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x000073E4 File Offset: 0x000055E4
		public int OverlapCollider(ContactFilter2D contactFilter, [Out] Collider2D[] results)
		{
			return this.OverlapColliderArray_Internal(contactFilter, results);
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x000073FE File Offset: 0x000055FE
		[NativeMethod("OverlapColliderArray_Binding")]
		private int OverlapColliderArray_Internal(ContactFilter2D contactFilter, [Unmarshalled] [NotNull("ArgumentNullException")] Collider2D[] results)
		{
			return this.OverlapColliderArray_Internal_Injected(ref contactFilter, results);
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x0000740C File Offset: 0x0000560C
		public int OverlapCollider(ContactFilter2D contactFilter, List<Collider2D> results)
		{
			return this.OverlapColliderList_Internal(contactFilter, results);
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x00007426 File Offset: 0x00005626
		[NativeMethod("OverlapColliderList_Binding")]
		private int OverlapColliderList_Internal(ContactFilter2D contactFilter, [NotNull("ArgumentNullException")] List<Collider2D> results)
		{
			return this.OverlapColliderList_Internal_Injected(ref contactFilter, results);
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x00007434 File Offset: 0x00005634
		public int GetContacts(ContactPoint2D[] contacts)
		{
			return Physics2D.GetContacts(this, default(ContactFilter2D).NoFilter(), contacts);
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0000745C File Offset: 0x0000565C
		public int GetContacts(List<ContactPoint2D> contacts)
		{
			return Physics2D.GetContacts(this, default(ContactFilter2D).NoFilter(), contacts);
		}

		// Token: 0x060002DA RID: 730 RVA: 0x00007484 File Offset: 0x00005684
		public int GetContacts(ContactFilter2D contactFilter, ContactPoint2D[] contacts)
		{
			return Physics2D.GetContacts(this, contactFilter, contacts);
		}

		// Token: 0x060002DB RID: 731 RVA: 0x000074A0 File Offset: 0x000056A0
		public int GetContacts(ContactFilter2D contactFilter, List<ContactPoint2D> contacts)
		{
			return Physics2D.GetContacts(this, contactFilter, contacts);
		}

		// Token: 0x060002DC RID: 732 RVA: 0x000074BC File Offset: 0x000056BC
		public int GetContacts(Collider2D[] colliders)
		{
			return Physics2D.GetContacts(this, default(ContactFilter2D).NoFilter(), colliders);
		}

		// Token: 0x060002DD RID: 733 RVA: 0x000074E4 File Offset: 0x000056E4
		public int GetContacts(List<Collider2D> colliders)
		{
			return Physics2D.GetContacts(this, default(ContactFilter2D).NoFilter(), colliders);
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0000750C File Offset: 0x0000570C
		public int GetContacts(ContactFilter2D contactFilter, Collider2D[] colliders)
		{
			return Physics2D.GetContacts(this, contactFilter, colliders);
		}

		// Token: 0x060002DF RID: 735 RVA: 0x00007528 File Offset: 0x00005728
		public int GetContacts(ContactFilter2D contactFilter, List<Collider2D> colliders)
		{
			return Physics2D.GetContacts(this, contactFilter, colliders);
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x00007544 File Offset: 0x00005744
		public int GetAttachedColliders([Out] Collider2D[] results)
		{
			return this.GetAttachedCollidersArray_Internal(results);
		}

		// Token: 0x060002E1 RID: 737
		[NativeMethod("GetAttachedCollidersArray_Binding")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetAttachedCollidersArray_Internal([NotNull("ArgumentNullException")] [Unmarshalled] Collider2D[] results);

		// Token: 0x060002E2 RID: 738 RVA: 0x00007560 File Offset: 0x00005760
		public int GetAttachedColliders(List<Collider2D> results)
		{
			return this.GetAttachedCollidersList_Internal(results);
		}

		// Token: 0x060002E3 RID: 739
		[NativeMethod("GetAttachedCollidersList_Binding")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetAttachedCollidersList_Internal([NotNull("ArgumentNullException")] List<Collider2D> results);

		// Token: 0x060002E4 RID: 740 RVA: 0x0000757C File Offset: 0x0000577C
		[ExcludeFromDocs]
		public int Cast(Vector2 direction, RaycastHit2D[] results)
		{
			return this.CastArray_Internal(direction, float.PositiveInfinity, results);
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0000759C File Offset: 0x0000579C
		public int Cast(Vector2 direction, RaycastHit2D[] results, [DefaultValue("Mathf.Infinity")] float distance)
		{
			return this.CastArray_Internal(direction, distance, results);
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x000075B7 File Offset: 0x000057B7
		[NativeMethod("CastArray_Binding")]
		private int CastArray_Internal(Vector2 direction, float distance, [Unmarshalled] [NotNull("ArgumentNullException")] RaycastHit2D[] results)
		{
			return this.CastArray_Internal_Injected(ref direction, distance, results);
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x000075C4 File Offset: 0x000057C4
		public int Cast(Vector2 direction, List<RaycastHit2D> results, [DefaultValue("Mathf.Infinity")] float distance = float.PositiveInfinity)
		{
			return this.CastList_Internal(direction, distance, results);
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x000075DF File Offset: 0x000057DF
		[NativeMethod("CastList_Binding")]
		private int CastList_Internal(Vector2 direction, float distance, [NotNull("ArgumentNullException")] List<RaycastHit2D> results)
		{
			return this.CastList_Internal_Injected(ref direction, distance, results);
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x000075EC File Offset: 0x000057EC
		[ExcludeFromDocs]
		public int Cast(Vector2 direction, ContactFilter2D contactFilter, RaycastHit2D[] results)
		{
			return this.CastFilteredArray_Internal(direction, float.PositiveInfinity, contactFilter, results);
		}

		// Token: 0x060002EA RID: 746 RVA: 0x0000760C File Offset: 0x0000580C
		public int Cast(Vector2 direction, ContactFilter2D contactFilter, RaycastHit2D[] results, [DefaultValue("Mathf.Infinity")] float distance)
		{
			return this.CastFilteredArray_Internal(direction, distance, contactFilter, results);
		}

		// Token: 0x060002EB RID: 747 RVA: 0x00007629 File Offset: 0x00005829
		[NativeMethod("CastFilteredArray_Binding")]
		private int CastFilteredArray_Internal(Vector2 direction, float distance, ContactFilter2D contactFilter, [Unmarshalled] [NotNull("ArgumentNullException")] RaycastHit2D[] results)
		{
			return this.CastFilteredArray_Internal_Injected(ref direction, distance, ref contactFilter, results);
		}

		// Token: 0x060002EC RID: 748 RVA: 0x00007638 File Offset: 0x00005838
		public int Cast(Vector2 direction, ContactFilter2D contactFilter, List<RaycastHit2D> results, [DefaultValue("Mathf.Infinity")] float distance)
		{
			return this.CastFilteredList_Internal(direction, distance, contactFilter, results);
		}

		// Token: 0x060002ED RID: 749 RVA: 0x00007655 File Offset: 0x00005855
		[NativeMethod("CastFilteredList_Binding")]
		private int CastFilteredList_Internal(Vector2 direction, float distance, ContactFilter2D contactFilter, [NotNull("ArgumentNullException")] List<RaycastHit2D> results)
		{
			return this.CastFilteredList_Internal_Injected(ref direction, distance, ref contactFilter, results);
		}

		// Token: 0x060002EE RID: 750 RVA: 0x00007664 File Offset: 0x00005864
		public int GetShapes(PhysicsShapeGroup2D physicsShapeGroup)
		{
			return this.GetShapes_Internal(ref physicsShapeGroup.m_GroupState);
		}

		// Token: 0x060002EF RID: 751
		[NativeMethod("GetShapes_Binding")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetShapes_Internal(ref PhysicsShapeGroup2D.GroupState physicsShapeGroupState);

		// Token: 0x060002F0 RID: 752 RVA: 0x00007682 File Offset: 0x00005882
		public Rigidbody2D()
		{
		}

		// Token: 0x060002F1 RID: 753
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_position_Injected(out Vector2 ret);

		// Token: 0x060002F2 RID: 754
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_position_Injected(ref Vector2 value);

		// Token: 0x060002F3 RID: 755
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetRotation_Quaternion_Injected(ref Quaternion rotation);

		// Token: 0x060002F4 RID: 756
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void MovePosition_Injected(ref Vector2 position);

		// Token: 0x060002F5 RID: 757
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void MoveRotation_Quaternion_Injected(ref Quaternion rotation);

		// Token: 0x060002F6 RID: 758
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_velocity_Injected(out Vector2 ret);

		// Token: 0x060002F7 RID: 759
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_velocity_Injected(ref Vector2 value);

		// Token: 0x060002F8 RID: 760
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_centerOfMass_Injected(out Vector2 ret);

		// Token: 0x060002F9 RID: 761
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_centerOfMass_Injected(ref Vector2 value);

		// Token: 0x060002FA RID: 762
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_worldCenterOfMass_Injected(out Vector2 ret);

		// Token: 0x060002FB RID: 763
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool IsTouching_OtherColliderWithFilter_Internal_Injected([Writable] Collider2D collider, ref ContactFilter2D contactFilter);

		// Token: 0x060002FC RID: 764
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool IsTouching_AnyColliderWithFilter_Internal_Injected(ref ContactFilter2D contactFilter);

		// Token: 0x060002FD RID: 765
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool OverlapPoint_Injected(ref Vector2 point);

		// Token: 0x060002FE RID: 766
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Distance_Internal_Injected([Writable] Collider2D collider, out ColliderDistance2D ret);

		// Token: 0x060002FF RID: 767
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void AddForce_Injected(ref Vector2 force, [DefaultValue("ForceMode2D.Force")] ForceMode2D mode);

		// Token: 0x06000300 RID: 768
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void AddRelativeForce_Injected(ref Vector2 relativeForce, [DefaultValue("ForceMode2D.Force")] ForceMode2D mode);

		// Token: 0x06000301 RID: 769
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void AddForceAtPosition_Injected(ref Vector2 force, ref Vector2 position, [DefaultValue("ForceMode2D.Force")] ForceMode2D mode);

		// Token: 0x06000302 RID: 770
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetPoint_Injected(ref Vector2 point, out Vector2 ret);

		// Token: 0x06000303 RID: 771
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetRelativePoint_Injected(ref Vector2 relativePoint, out Vector2 ret);

		// Token: 0x06000304 RID: 772
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetVector_Injected(ref Vector2 vector, out Vector2 ret);

		// Token: 0x06000305 RID: 773
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetRelativeVector_Injected(ref Vector2 relativeVector, out Vector2 ret);

		// Token: 0x06000306 RID: 774
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetPointVelocity_Injected(ref Vector2 point, out Vector2 ret);

		// Token: 0x06000307 RID: 775
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetRelativePointVelocity_Injected(ref Vector2 relativePoint, out Vector2 ret);

		// Token: 0x06000308 RID: 776
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int OverlapColliderArray_Internal_Injected(ref ContactFilter2D contactFilter, Collider2D[] results);

		// Token: 0x06000309 RID: 777
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int OverlapColliderList_Internal_Injected(ref ContactFilter2D contactFilter, List<Collider2D> results);

		// Token: 0x0600030A RID: 778
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int CastArray_Internal_Injected(ref Vector2 direction, float distance, RaycastHit2D[] results);

		// Token: 0x0600030B RID: 779
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int CastList_Internal_Injected(ref Vector2 direction, float distance, List<RaycastHit2D> results);

		// Token: 0x0600030C RID: 780
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int CastFilteredArray_Internal_Injected(ref Vector2 direction, float distance, ref ContactFilter2D contactFilter, RaycastHit2D[] results);

		// Token: 0x0600030D RID: 781
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int CastFilteredList_Internal_Injected(ref Vector2 direction, float distance, ref ContactFilter2D contactFilter, List<RaycastHit2D> results);
	}
}
