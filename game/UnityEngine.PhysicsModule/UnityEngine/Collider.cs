using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000028 RID: 40
	[RequiredByNativeCode]
	[RequireComponent(typeof(Transform))]
	[NativeHeader("Modules/Physics/Collider.h")]
	public class Collider : Component
	{
		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060002B4 RID: 692
		// (set) Token: 0x060002B5 RID: 693
		public extern bool enabled { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060002B6 RID: 694
		public extern Rigidbody attachedRigidbody { [NativeMethod("GetRigidbody")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060002B7 RID: 695
		public extern ArticulationBody attachedArticulationBody { [NativeMethod("GetArticulationBody")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060002B8 RID: 696
		// (set) Token: 0x060002B9 RID: 697
		public extern bool isTrigger { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060002BA RID: 698
		// (set) Token: 0x060002BB RID: 699
		public extern float contactOffset { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x060002BC RID: 700 RVA: 0x00005204 File Offset: 0x00003404
		public Vector3 ClosestPoint(Vector3 position)
		{
			Vector3 result;
			this.ClosestPoint_Injected(ref position, out result);
			return result;
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060002BD RID: 701 RVA: 0x0000521C File Offset: 0x0000341C
		public Bounds bounds
		{
			get
			{
				Bounds result;
				this.get_bounds_Injected(out result);
				return result;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060002BE RID: 702
		// (set) Token: 0x060002BF RID: 703
		public extern bool hasModifiableContacts { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060002C0 RID: 704
		// (set) Token: 0x060002C1 RID: 705
		[NativeMethod("Material")]
		public extern PhysicMaterial sharedMaterial { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060002C2 RID: 706
		// (set) Token: 0x060002C3 RID: 707
		public extern PhysicMaterial material { [NativeMethod("GetClonedMaterial")] [MethodImpl(MethodImplOptions.InternalCall)] get; [NativeMethod("SetMaterial")] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x060002C4 RID: 708 RVA: 0x00005234 File Offset: 0x00003434
		private RaycastHit Raycast(Ray ray, float maxDistance, ref bool hasHit)
		{
			RaycastHit result;
			this.Raycast_Injected(ref ray, maxDistance, ref hasHit, out result);
			return result;
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x00005250 File Offset: 0x00003450
		public bool Raycast(Ray ray, out RaycastHit hitInfo, float maxDistance)
		{
			bool result = false;
			hitInfo = this.Raycast(ray, maxDistance, ref result);
			return result;
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x00005275 File Offset: 0x00003475
		[NativeName("ClosestPointOnBounds")]
		private void Internal_ClosestPointOnBounds(Vector3 point, ref Vector3 outPos, ref float distance)
		{
			this.Internal_ClosestPointOnBounds_Injected(ref point, ref outPos, ref distance);
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x00005284 File Offset: 0x00003484
		public Vector3 ClosestPointOnBounds(Vector3 position)
		{
			float num = 0f;
			Vector3 zero = Vector3.zero;
			this.Internal_ClosestPointOnBounds(position, ref zero, ref num);
			return zero;
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x000051FB File Offset: 0x000033FB
		public Collider()
		{
		}

		// Token: 0x060002C9 RID: 713
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void ClosestPoint_Injected(ref Vector3 position, out Vector3 ret);

		// Token: 0x060002CA RID: 714
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_bounds_Injected(out Bounds ret);

		// Token: 0x060002CB RID: 715
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Raycast_Injected(ref Ray ray, float maxDistance, ref bool hasHit, out RaycastHit ret);

		// Token: 0x060002CC RID: 716
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_ClosestPointOnBounds_Injected(ref Vector3 point, ref Vector3 outPos, ref float distance);
	}
}
