using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000020 RID: 32
	public struct ModifiableContactPair
	{
		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060001ED RID: 493 RVA: 0x0000468B File Offset: 0x0000288B
		public int colliderInstanceID
		{
			get
			{
				return ModifiableContactPair.ResolveColliderInstanceID(this.shape);
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060001EE RID: 494 RVA: 0x00004698 File Offset: 0x00002898
		public int otherColliderInstanceID
		{
			get
			{
				return ModifiableContactPair.ResolveColliderInstanceID(this.otherShape);
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060001EF RID: 495 RVA: 0x000046A5 File Offset: 0x000028A5
		public int bodyInstanceID
		{
			get
			{
				return ModifiableContactPair.ResolveBodyInstanceID(this.actor);
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x000046B2 File Offset: 0x000028B2
		public int otherBodyInstanceID
		{
			get
			{
				return ModifiableContactPair.ResolveBodyInstanceID(this.otherActor);
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x000046BF File Offset: 0x000028BF
		public int contactCount
		{
			get
			{
				return this.numContacts;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x000046C8 File Offset: 0x000028C8
		// (set) Token: 0x060001F3 RID: 499 RVA: 0x000046E8 File Offset: 0x000028E8
		public unsafe ModifiableMassProperties massProperties
		{
			get
			{
				return this.GetContactPatch()->massProperties;
			}
			set
			{
				ModifiableContactPatch* contactPatch = this.GetContactPatch();
				contactPatch->massProperties = value;
				ModifiableContactPatch* ptr = contactPatch;
				ptr->internalFlags = (ptr->internalFlags | 8);
			}
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00004710 File Offset: 0x00002910
		public unsafe Vector3 GetPoint(int i)
		{
			return this.GetContact(i)->contact;
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x0000472E File Offset: 0x0000292E
		public unsafe void SetPoint(int i, Vector3 v)
		{
			this.GetContact(i)->contact = v;
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00004740 File Offset: 0x00002940
		public unsafe Vector3 GetNormal(int i)
		{
			return this.GetContact(i)->normal;
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x0000475E File Offset: 0x0000295E
		public unsafe void SetNormal(int i, Vector3 normal)
		{
			this.GetContact(i)->normal = normal;
			ModifiableContactPatch* contactPatch = this.GetContactPatch();
			contactPatch->internalFlags = (contactPatch->internalFlags | 64);
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00004780 File Offset: 0x00002980
		public unsafe float GetSeparation(int i)
		{
			return this.GetContact(i)->separation;
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x0000479E File Offset: 0x0000299E
		public unsafe void SetSeparation(int i, float separation)
		{
			this.GetContact(i)->separation = separation;
		}

		// Token: 0x060001FA RID: 506 RVA: 0x000047B0 File Offset: 0x000029B0
		public unsafe Vector3 GetTargetVelocity(int i)
		{
			return this.GetContact(i)->targetVelocity;
		}

		// Token: 0x060001FB RID: 507 RVA: 0x000047CE File Offset: 0x000029CE
		public unsafe void SetTargetVelocity(int i, Vector3 velocity)
		{
			this.GetContact(i)->targetVelocity = velocity;
			ModifiableContactPatch* contactPatch = this.GetContactPatch();
			contactPatch->internalFlags = (contactPatch->internalFlags | 16);
		}

		// Token: 0x060001FC RID: 508 RVA: 0x000047F0 File Offset: 0x000029F0
		public unsafe float GetBounciness(int i)
		{
			return this.GetContact(i)->restitution;
		}

		// Token: 0x060001FD RID: 509 RVA: 0x0000480E File Offset: 0x00002A0E
		public unsafe void SetBounciness(int i, float bounciness)
		{
			this.GetContact(i)->restitution = bounciness;
			ModifiableContactPatch* contactPatch = this.GetContactPatch();
			contactPatch->internalFlags = (contactPatch->internalFlags | 64);
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00004830 File Offset: 0x00002A30
		public unsafe float GetStaticFriction(int i)
		{
			return this.GetContact(i)->staticFriction;
		}

		// Token: 0x060001FF RID: 511 RVA: 0x0000484E File Offset: 0x00002A4E
		public unsafe void SetStaticFriction(int i, float staticFriction)
		{
			this.GetContact(i)->staticFriction = staticFriction;
			ModifiableContactPatch* contactPatch = this.GetContactPatch();
			contactPatch->internalFlags = (contactPatch->internalFlags | 64);
		}

		// Token: 0x06000200 RID: 512 RVA: 0x00004870 File Offset: 0x00002A70
		public unsafe float GetDynamicFriction(int i)
		{
			return this.GetContact(i)->dynamicFriction;
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0000488E File Offset: 0x00002A8E
		public unsafe void SetDynamicFriction(int i, float dynamicFriction)
		{
			this.GetContact(i)->dynamicFriction = dynamicFriction;
			ModifiableContactPatch* contactPatch = this.GetContactPatch();
			contactPatch->internalFlags = (contactPatch->internalFlags | 64);
		}

		// Token: 0x06000202 RID: 514 RVA: 0x000048B0 File Offset: 0x00002AB0
		public unsafe float GetMaxImpulse(int i)
		{
			return this.GetContact(i)->maxImpulse;
		}

		// Token: 0x06000203 RID: 515 RVA: 0x000048CE File Offset: 0x00002ACE
		public unsafe void SetMaxImpulse(int i, float value)
		{
			this.GetContact(i)->maxImpulse = value;
			ModifiableContactPatch* contactPatch = this.GetContactPatch();
			contactPatch->internalFlags = (contactPatch->internalFlags | 32);
		}

		// Token: 0x06000204 RID: 516 RVA: 0x000048F0 File Offset: 0x00002AF0
		public void IgnoreContact(int i)
		{
			this.SetMaxImpulse(i, 0f);
		}

		// Token: 0x06000205 RID: 517 RVA: 0x00004900 File Offset: 0x00002B00
		public unsafe uint GetFaceIndex(int i)
		{
			bool flag = (this.GetContactPatch()->internalFlags & 1) > 0;
			uint result;
			if (flag)
			{
				IntPtr value = new IntPtr(this.contacts.ToInt64() + (long)(this.numContacts * sizeof(ModifiableContact)) + (long)((this.numContacts + i) * 4));
				uint rawIndex = *(uint*)((void*)value);
				result = ModifiableContactPair.TranslateTriangleIndex(this.otherShape, rawIndex);
			}
			else
			{
				result = uint.MaxValue;
			}
			return result;
		}

		// Token: 0x06000206 RID: 518 RVA: 0x0000496C File Offset: 0x00002B6C
		private unsafe ModifiableContact* GetContact(int index)
		{
			IntPtr value = new IntPtr(this.contacts.ToInt64() + (long)(index * sizeof(ModifiableContact)));
			return (ModifiableContact*)((void*)value);
		}

		// Token: 0x06000207 RID: 519 RVA: 0x000049A0 File Offset: 0x00002BA0
		private unsafe ModifiableContactPatch* GetContactPatch()
		{
			IntPtr value = new IntPtr(this.contacts.ToInt64() - (long)(this.numContacts * sizeof(ModifiableContactPatch)));
			return (ModifiableContactPatch*)((void*)value);
		}

		// Token: 0x06000208 RID: 520
		[ThreadSafe]
		[StaticAccessor("GetPhysicsManager()")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int ResolveColliderInstanceID(IntPtr shapePtr);

		// Token: 0x06000209 RID: 521
		[ThreadSafe]
		[StaticAccessor("GetPhysicsManager()")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int ResolveBodyInstanceID(IntPtr actorPtr);

		// Token: 0x0600020A RID: 522
		[ThreadSafe]
		[StaticAccessor("GetPhysicsManager()")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern uint TranslateTriangleIndex(IntPtr shapePtr, uint rawIndex);

		// Token: 0x04000084 RID: 132
		private IntPtr actor;

		// Token: 0x04000085 RID: 133
		private IntPtr otherActor;

		// Token: 0x04000086 RID: 134
		private IntPtr shape;

		// Token: 0x04000087 RID: 135
		private IntPtr otherShape;

		// Token: 0x04000088 RID: 136
		public Quaternion rotation;

		// Token: 0x04000089 RID: 137
		public Vector3 position;

		// Token: 0x0400008A RID: 138
		public Quaternion otherRotation;

		// Token: 0x0400008B RID: 139
		public Vector3 otherPosition;

		// Token: 0x0400008C RID: 140
		private int numContacts;

		// Token: 0x0400008D RID: 141
		private IntPtr contacts;
	}
}
