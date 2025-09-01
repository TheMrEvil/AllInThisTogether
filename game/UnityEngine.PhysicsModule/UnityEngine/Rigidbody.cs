using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x02000027 RID: 39
	[RequireComponent(typeof(Transform))]
	[NativeHeader("Modules/Physics/Rigidbody.h")]
	public class Rigidbody : Component
	{
		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000236 RID: 566 RVA: 0x00004D78 File Offset: 0x00002F78
		// (set) Token: 0x06000237 RID: 567 RVA: 0x00004D8E File Offset: 0x00002F8E
		public Vector3 velocity
		{
			get
			{
				Vector3 result;
				this.get_velocity_Injected(out result);
				return result;
			}
			set
			{
				this.set_velocity_Injected(ref value);
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000238 RID: 568 RVA: 0x00004D98 File Offset: 0x00002F98
		// (set) Token: 0x06000239 RID: 569 RVA: 0x00004DAE File Offset: 0x00002FAE
		public Vector3 angularVelocity
		{
			get
			{
				Vector3 result;
				this.get_angularVelocity_Injected(out result);
				return result;
			}
			set
			{
				this.set_angularVelocity_Injected(ref value);
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x0600023A RID: 570
		// (set) Token: 0x0600023B RID: 571
		public extern float drag { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x0600023C RID: 572
		// (set) Token: 0x0600023D RID: 573
		public extern float angularDrag { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x0600023E RID: 574
		// (set) Token: 0x0600023F RID: 575
		public extern float mass { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06000240 RID: 576
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetDensity(float density);

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000241 RID: 577
		// (set) Token: 0x06000242 RID: 578
		public extern bool useGravity { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000243 RID: 579
		// (set) Token: 0x06000244 RID: 580
		public extern float maxDepenetrationVelocity { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000245 RID: 581
		// (set) Token: 0x06000246 RID: 582
		public extern bool isKinematic { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000247 RID: 583
		// (set) Token: 0x06000248 RID: 584
		public extern bool freezeRotation { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000249 RID: 585
		// (set) Token: 0x0600024A RID: 586
		public extern RigidbodyConstraints constraints { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x0600024B RID: 587
		// (set) Token: 0x0600024C RID: 588
		public extern CollisionDetectionMode collisionDetectionMode { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x0600024D RID: 589 RVA: 0x00004DB8 File Offset: 0x00002FB8
		// (set) Token: 0x0600024E RID: 590 RVA: 0x00004DCE File Offset: 0x00002FCE
		public Vector3 centerOfMass
		{
			get
			{
				Vector3 result;
				this.get_centerOfMass_Injected(out result);
				return result;
			}
			set
			{
				this.set_centerOfMass_Injected(ref value);
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x0600024F RID: 591 RVA: 0x00004DD8 File Offset: 0x00002FD8
		public Vector3 worldCenterOfMass
		{
			get
			{
				Vector3 result;
				this.get_worldCenterOfMass_Injected(out result);
				return result;
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000250 RID: 592 RVA: 0x00004DF0 File Offset: 0x00002FF0
		// (set) Token: 0x06000251 RID: 593 RVA: 0x00004E06 File Offset: 0x00003006
		public Quaternion inertiaTensorRotation
		{
			get
			{
				Quaternion result;
				this.get_inertiaTensorRotation_Injected(out result);
				return result;
			}
			set
			{
				this.set_inertiaTensorRotation_Injected(ref value);
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000252 RID: 594 RVA: 0x00004E10 File Offset: 0x00003010
		// (set) Token: 0x06000253 RID: 595 RVA: 0x00004E26 File Offset: 0x00003026
		public Vector3 inertiaTensor
		{
			get
			{
				Vector3 result;
				this.get_inertiaTensor_Injected(out result);
				return result;
			}
			set
			{
				this.set_inertiaTensor_Injected(ref value);
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000254 RID: 596
		// (set) Token: 0x06000255 RID: 597
		public extern bool detectCollisions { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000256 RID: 598 RVA: 0x00004E30 File Offset: 0x00003030
		// (set) Token: 0x06000257 RID: 599 RVA: 0x00004E46 File Offset: 0x00003046
		public Vector3 position
		{
			get
			{
				Vector3 result;
				this.get_position_Injected(out result);
				return result;
			}
			set
			{
				this.set_position_Injected(ref value);
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000258 RID: 600 RVA: 0x00004E50 File Offset: 0x00003050
		// (set) Token: 0x06000259 RID: 601 RVA: 0x00004E66 File Offset: 0x00003066
		public Quaternion rotation
		{
			get
			{
				Quaternion result;
				this.get_rotation_Injected(out result);
				return result;
			}
			set
			{
				this.set_rotation_Injected(ref value);
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x0600025A RID: 602
		// (set) Token: 0x0600025B RID: 603
		public extern RigidbodyInterpolation interpolation { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x0600025C RID: 604
		// (set) Token: 0x0600025D RID: 605
		public extern int solverIterations { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x0600025E RID: 606
		// (set) Token: 0x0600025F RID: 607
		public extern float sleepThreshold { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000260 RID: 608
		// (set) Token: 0x06000261 RID: 609
		public extern float maxAngularVelocity { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06000262 RID: 610 RVA: 0x00004E70 File Offset: 0x00003070
		public void MovePosition(Vector3 position)
		{
			this.MovePosition_Injected(ref position);
		}

		// Token: 0x06000263 RID: 611 RVA: 0x00004E7A File Offset: 0x0000307A
		public void MoveRotation(Quaternion rot)
		{
			this.MoveRotation_Injected(ref rot);
		}

		// Token: 0x06000264 RID: 612
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Sleep();

		// Token: 0x06000265 RID: 613
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool IsSleeping();

		// Token: 0x06000266 RID: 614
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void WakeUp();

		// Token: 0x06000267 RID: 615
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void ResetCenterOfMass();

		// Token: 0x06000268 RID: 616
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void ResetInertiaTensor();

		// Token: 0x06000269 RID: 617 RVA: 0x00004E84 File Offset: 0x00003084
		public Vector3 GetRelativePointVelocity(Vector3 relativePoint)
		{
			Vector3 result;
			this.GetRelativePointVelocity_Injected(ref relativePoint, out result);
			return result;
		}

		// Token: 0x0600026A RID: 618 RVA: 0x00004E9C File Offset: 0x0000309C
		public Vector3 GetPointVelocity(Vector3 worldPoint)
		{
			Vector3 result;
			this.GetPointVelocity_Injected(ref worldPoint, out result);
			return result;
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x0600026B RID: 619
		// (set) Token: 0x0600026C RID: 620
		public extern int solverVelocityIterations { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x0600026D RID: 621 RVA: 0x00004EB4 File Offset: 0x000030B4
		public void AddForce(Vector3 force, [UnityEngine.Internal.DefaultValue("ForceMode.Force")] ForceMode mode)
		{
			this.AddForce_Injected(ref force, mode);
		}

		// Token: 0x0600026E RID: 622 RVA: 0x00004EBF File Offset: 0x000030BF
		[ExcludeFromDocs]
		public void AddForce(Vector3 force)
		{
			this.AddForce(force, ForceMode.Force);
		}

		// Token: 0x0600026F RID: 623 RVA: 0x00004ECB File Offset: 0x000030CB
		public void AddForce(float x, float y, float z, [UnityEngine.Internal.DefaultValue("ForceMode.Force")] ForceMode mode)
		{
			this.AddForce(new Vector3(x, y, z), mode);
		}

		// Token: 0x06000270 RID: 624 RVA: 0x00004EDF File Offset: 0x000030DF
		[ExcludeFromDocs]
		public void AddForce(float x, float y, float z)
		{
			this.AddForce(new Vector3(x, y, z), ForceMode.Force);
		}

		// Token: 0x06000271 RID: 625 RVA: 0x00004EF2 File Offset: 0x000030F2
		public void AddRelativeForce(Vector3 force, [UnityEngine.Internal.DefaultValue("ForceMode.Force")] ForceMode mode)
		{
			this.AddRelativeForce_Injected(ref force, mode);
		}

		// Token: 0x06000272 RID: 626 RVA: 0x00004EFD File Offset: 0x000030FD
		[ExcludeFromDocs]
		public void AddRelativeForce(Vector3 force)
		{
			this.AddRelativeForce(force, ForceMode.Force);
		}

		// Token: 0x06000273 RID: 627 RVA: 0x00004F09 File Offset: 0x00003109
		public void AddRelativeForce(float x, float y, float z, [UnityEngine.Internal.DefaultValue("ForceMode.Force")] ForceMode mode)
		{
			this.AddRelativeForce(new Vector3(x, y, z), mode);
		}

		// Token: 0x06000274 RID: 628 RVA: 0x00004F1D File Offset: 0x0000311D
		[ExcludeFromDocs]
		public void AddRelativeForce(float x, float y, float z)
		{
			this.AddRelativeForce(new Vector3(x, y, z), ForceMode.Force);
		}

		// Token: 0x06000275 RID: 629 RVA: 0x00004F30 File Offset: 0x00003130
		public void AddTorque(Vector3 torque, [UnityEngine.Internal.DefaultValue("ForceMode.Force")] ForceMode mode)
		{
			this.AddTorque_Injected(ref torque, mode);
		}

		// Token: 0x06000276 RID: 630 RVA: 0x00004F3B File Offset: 0x0000313B
		[ExcludeFromDocs]
		public void AddTorque(Vector3 torque)
		{
			this.AddTorque(torque, ForceMode.Force);
		}

		// Token: 0x06000277 RID: 631 RVA: 0x00004F47 File Offset: 0x00003147
		public void AddTorque(float x, float y, float z, [UnityEngine.Internal.DefaultValue("ForceMode.Force")] ForceMode mode)
		{
			this.AddTorque(new Vector3(x, y, z), mode);
		}

		// Token: 0x06000278 RID: 632 RVA: 0x00004F5B File Offset: 0x0000315B
		[ExcludeFromDocs]
		public void AddTorque(float x, float y, float z)
		{
			this.AddTorque(new Vector3(x, y, z), ForceMode.Force);
		}

		// Token: 0x06000279 RID: 633 RVA: 0x00004F6E File Offset: 0x0000316E
		public void AddRelativeTorque(Vector3 torque, [UnityEngine.Internal.DefaultValue("ForceMode.Force")] ForceMode mode)
		{
			this.AddRelativeTorque_Injected(ref torque, mode);
		}

		// Token: 0x0600027A RID: 634 RVA: 0x00004F79 File Offset: 0x00003179
		[ExcludeFromDocs]
		public void AddRelativeTorque(Vector3 torque)
		{
			this.AddRelativeTorque(torque, ForceMode.Force);
		}

		// Token: 0x0600027B RID: 635 RVA: 0x00004F85 File Offset: 0x00003185
		public void AddRelativeTorque(float x, float y, float z, [UnityEngine.Internal.DefaultValue("ForceMode.Force")] ForceMode mode)
		{
			this.AddRelativeTorque(new Vector3(x, y, z), mode);
		}

		// Token: 0x0600027C RID: 636 RVA: 0x00004F99 File Offset: 0x00003199
		[ExcludeFromDocs]
		public void AddRelativeTorque(float x, float y, float z)
		{
			this.AddRelativeTorque(x, y, z, ForceMode.Force);
		}

		// Token: 0x0600027D RID: 637 RVA: 0x00004FA7 File Offset: 0x000031A7
		public void AddForceAtPosition(Vector3 force, Vector3 position, [UnityEngine.Internal.DefaultValue("ForceMode.Force")] ForceMode mode)
		{
			this.AddForceAtPosition_Injected(ref force, ref position, mode);
		}

		// Token: 0x0600027E RID: 638 RVA: 0x00004FB4 File Offset: 0x000031B4
		[ExcludeFromDocs]
		public void AddForceAtPosition(Vector3 force, Vector3 position)
		{
			this.AddForceAtPosition(force, position, ForceMode.Force);
		}

		// Token: 0x0600027F RID: 639 RVA: 0x00004FC1 File Offset: 0x000031C1
		public void AddExplosionForce(float explosionForce, Vector3 explosionPosition, float explosionRadius, [UnityEngine.Internal.DefaultValue("0.0f")] float upwardsModifier, [UnityEngine.Internal.DefaultValue("ForceMode.Force)")] ForceMode mode)
		{
			this.AddExplosionForce_Injected(explosionForce, ref explosionPosition, explosionRadius, upwardsModifier, mode);
		}

		// Token: 0x06000280 RID: 640 RVA: 0x00004FD1 File Offset: 0x000031D1
		[ExcludeFromDocs]
		public void AddExplosionForce(float explosionForce, Vector3 explosionPosition, float explosionRadius, float upwardsModifier)
		{
			this.AddExplosionForce(explosionForce, explosionPosition, explosionRadius, upwardsModifier, ForceMode.Force);
		}

		// Token: 0x06000281 RID: 641 RVA: 0x00004FE1 File Offset: 0x000031E1
		[ExcludeFromDocs]
		public void AddExplosionForce(float explosionForce, Vector3 explosionPosition, float explosionRadius)
		{
			this.AddExplosionForce(explosionForce, explosionPosition, explosionRadius, 0f, ForceMode.Force);
		}

		// Token: 0x06000282 RID: 642 RVA: 0x00004FF4 File Offset: 0x000031F4
		[NativeName("ClosestPointOnBounds")]
		private void Internal_ClosestPointOnBounds(Vector3 point, ref Vector3 outPos, ref float distance)
		{
			this.Internal_ClosestPointOnBounds_Injected(ref point, ref outPos, ref distance);
		}

		// Token: 0x06000283 RID: 643 RVA: 0x00005000 File Offset: 0x00003200
		public Vector3 ClosestPointOnBounds(Vector3 position)
		{
			float num = 0f;
			Vector3 zero = Vector3.zero;
			this.Internal_ClosestPointOnBounds(position, ref zero, ref num);
			return zero;
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0000502C File Offset: 0x0000322C
		private RaycastHit SweepTest(Vector3 direction, float maxDistance, QueryTriggerInteraction queryTriggerInteraction, ref bool hasHit)
		{
			RaycastHit result;
			this.SweepTest_Injected(ref direction, maxDistance, queryTriggerInteraction, ref hasHit, out result);
			return result;
		}

		// Token: 0x06000285 RID: 645 RVA: 0x00005048 File Offset: 0x00003248
		public bool SweepTest(Vector3 direction, out RaycastHit hitInfo, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float maxDistance, [UnityEngine.Internal.DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			float magnitude = direction.magnitude;
			bool flag = magnitude > float.Epsilon;
			bool result;
			if (flag)
			{
				Vector3 direction2 = direction / magnitude;
				bool flag2 = false;
				hitInfo = this.SweepTest(direction2, maxDistance, queryTriggerInteraction, ref flag2);
				result = flag2;
			}
			else
			{
				hitInfo = default(RaycastHit);
				result = false;
			}
			return result;
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000509C File Offset: 0x0000329C
		[ExcludeFromDocs]
		public bool SweepTest(Vector3 direction, out RaycastHit hitInfo, float maxDistance)
		{
			return this.SweepTest(direction, out hitInfo, maxDistance, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000287 RID: 647 RVA: 0x000050B8 File Offset: 0x000032B8
		[ExcludeFromDocs]
		public bool SweepTest(Vector3 direction, out RaycastHit hitInfo)
		{
			return this.SweepTest(direction, out hitInfo, float.PositiveInfinity, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000288 RID: 648 RVA: 0x000050D8 File Offset: 0x000032D8
		[NativeName("SweepTestAll")]
		private RaycastHit[] Internal_SweepTestAll(Vector3 direction, float maxDistance, QueryTriggerInteraction queryTriggerInteraction)
		{
			return this.Internal_SweepTestAll_Injected(ref direction, maxDistance, queryTriggerInteraction);
		}

		// Token: 0x06000289 RID: 649 RVA: 0x000050E4 File Offset: 0x000032E4
		public RaycastHit[] SweepTestAll(Vector3 direction, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float maxDistance, [UnityEngine.Internal.DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			float magnitude = direction.magnitude;
			bool flag = magnitude > float.Epsilon;
			RaycastHit[] result;
			if (flag)
			{
				Vector3 direction2 = direction / magnitude;
				result = this.Internal_SweepTestAll(direction2, maxDistance, queryTriggerInteraction);
			}
			else
			{
				result = new RaycastHit[0];
			}
			return result;
		}

		// Token: 0x0600028A RID: 650 RVA: 0x00005128 File Offset: 0x00003328
		[ExcludeFromDocs]
		public RaycastHit[] SweepTestAll(Vector3 direction, float maxDistance)
		{
			return this.SweepTestAll(direction, maxDistance, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x0600028B RID: 651 RVA: 0x00005144 File Offset: 0x00003344
		[ExcludeFromDocs]
		public RaycastHit[] SweepTestAll(Vector3 direction)
		{
			return this.SweepTestAll(direction, float.PositiveInfinity, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x0600028C RID: 652 RVA: 0x00005164 File Offset: 0x00003364
		// (set) Token: 0x0600028D RID: 653 RVA: 0x00002193 File Offset: 0x00000393
		[Obsolete("The sleepVelocity is no longer supported. Use sleepThreshold. Note that sleepThreshold is energy but not velocity.", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float sleepVelocity
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x0600028E RID: 654 RVA: 0x0000517C File Offset: 0x0000337C
		// (set) Token: 0x0600028F RID: 655 RVA: 0x00002193 File Offset: 0x00000393
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("The sleepAngularVelocity is no longer supported. Use sleepThreshold to specify energy.", true)]
		public float sleepAngularVelocity
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		// Token: 0x06000290 RID: 656 RVA: 0x00005193 File Offset: 0x00003393
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use Rigidbody.maxAngularVelocity instead.")]
		public void SetMaxAngularVelocity(float a)
		{
			this.maxAngularVelocity = a;
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000291 RID: 657 RVA: 0x000051A0 File Offset: 0x000033A0
		// (set) Token: 0x06000292 RID: 658 RVA: 0x00002193 File Offset: 0x00000393
		[Obsolete("Cone friction is no longer supported.", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool useConeFriction
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000293 RID: 659 RVA: 0x000051B4 File Offset: 0x000033B4
		// (set) Token: 0x06000294 RID: 660 RVA: 0x000051CC File Offset: 0x000033CC
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Please use Rigidbody.solverIterations instead. (UnityUpgradable) -> solverIterations")]
		public int solverIterationCount
		{
			get
			{
				return this.solverIterations;
			}
			set
			{
				this.solverIterations = value;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000295 RID: 661 RVA: 0x000051D8 File Offset: 0x000033D8
		// (set) Token: 0x06000296 RID: 662 RVA: 0x000051F0 File Offset: 0x000033F0
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Please use Rigidbody.solverVelocityIterations instead. (UnityUpgradable) -> solverVelocityIterations")]
		public int solverVelocityIterationCount
		{
			get
			{
				return this.solverVelocityIterations;
			}
			set
			{
				this.solverVelocityIterations = value;
			}
		}

		// Token: 0x06000297 RID: 663 RVA: 0x000051FB File Offset: 0x000033FB
		public Rigidbody()
		{
		}

		// Token: 0x06000298 RID: 664
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_velocity_Injected(out Vector3 ret);

		// Token: 0x06000299 RID: 665
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_velocity_Injected(ref Vector3 value);

		// Token: 0x0600029A RID: 666
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_angularVelocity_Injected(out Vector3 ret);

		// Token: 0x0600029B RID: 667
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_angularVelocity_Injected(ref Vector3 value);

		// Token: 0x0600029C RID: 668
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_centerOfMass_Injected(out Vector3 ret);

		// Token: 0x0600029D RID: 669
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_centerOfMass_Injected(ref Vector3 value);

		// Token: 0x0600029E RID: 670
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_worldCenterOfMass_Injected(out Vector3 ret);

		// Token: 0x0600029F RID: 671
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_inertiaTensorRotation_Injected(out Quaternion ret);

		// Token: 0x060002A0 RID: 672
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_inertiaTensorRotation_Injected(ref Quaternion value);

		// Token: 0x060002A1 RID: 673
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_inertiaTensor_Injected(out Vector3 ret);

		// Token: 0x060002A2 RID: 674
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_inertiaTensor_Injected(ref Vector3 value);

		// Token: 0x060002A3 RID: 675
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_position_Injected(out Vector3 ret);

		// Token: 0x060002A4 RID: 676
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_position_Injected(ref Vector3 value);

		// Token: 0x060002A5 RID: 677
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_rotation_Injected(out Quaternion ret);

		// Token: 0x060002A6 RID: 678
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_rotation_Injected(ref Quaternion value);

		// Token: 0x060002A7 RID: 679
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void MovePosition_Injected(ref Vector3 position);

		// Token: 0x060002A8 RID: 680
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void MoveRotation_Injected(ref Quaternion rot);

		// Token: 0x060002A9 RID: 681
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetRelativePointVelocity_Injected(ref Vector3 relativePoint, out Vector3 ret);

		// Token: 0x060002AA RID: 682
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetPointVelocity_Injected(ref Vector3 worldPoint, out Vector3 ret);

		// Token: 0x060002AB RID: 683
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void AddForce_Injected(ref Vector3 force, [UnityEngine.Internal.DefaultValue("ForceMode.Force")] ForceMode mode);

		// Token: 0x060002AC RID: 684
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void AddRelativeForce_Injected(ref Vector3 force, [UnityEngine.Internal.DefaultValue("ForceMode.Force")] ForceMode mode);

		// Token: 0x060002AD RID: 685
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void AddTorque_Injected(ref Vector3 torque, [UnityEngine.Internal.DefaultValue("ForceMode.Force")] ForceMode mode);

		// Token: 0x060002AE RID: 686
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void AddRelativeTorque_Injected(ref Vector3 torque, [UnityEngine.Internal.DefaultValue("ForceMode.Force")] ForceMode mode);

		// Token: 0x060002AF RID: 687
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void AddForceAtPosition_Injected(ref Vector3 force, ref Vector3 position, [UnityEngine.Internal.DefaultValue("ForceMode.Force")] ForceMode mode);

		// Token: 0x060002B0 RID: 688
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void AddExplosionForce_Injected(float explosionForce, ref Vector3 explosionPosition, float explosionRadius, [UnityEngine.Internal.DefaultValue("0.0f")] float upwardsModifier, [UnityEngine.Internal.DefaultValue("ForceMode.Force)")] ForceMode mode);

		// Token: 0x060002B1 RID: 689
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_ClosestPointOnBounds_Injected(ref Vector3 point, ref Vector3 outPos, ref float distance);

		// Token: 0x060002B2 RID: 690
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SweepTest_Injected(ref Vector3 direction, float maxDistance, QueryTriggerInteraction queryTriggerInteraction, ref bool hasHit, out RaycastHit ret);

		// Token: 0x060002B3 RID: 691
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern RaycastHit[] Internal_SweepTestAll_Injected(ref Vector3 direction, float maxDistance, QueryTriggerInteraction queryTriggerInteraction);
	}
}
