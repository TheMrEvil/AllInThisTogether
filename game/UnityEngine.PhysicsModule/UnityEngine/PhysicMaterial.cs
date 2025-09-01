using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000025 RID: 37
	[NativeHeader("Modules/Physics/PhysicMaterial.h")]
	public class PhysicMaterial : Object
	{
		// Token: 0x0600020B RID: 523 RVA: 0x000049D9 File Offset: 0x00002BD9
		public PhysicMaterial()
		{
			PhysicMaterial.Internal_CreateDynamicsMaterial(this, "DynamicMaterial");
		}

		// Token: 0x0600020C RID: 524 RVA: 0x000049EF File Offset: 0x00002BEF
		public PhysicMaterial(string name)
		{
			PhysicMaterial.Internal_CreateDynamicsMaterial(this, name);
		}

		// Token: 0x0600020D RID: 525
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_CreateDynamicsMaterial([Writable] PhysicMaterial mat, string name);

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600020E RID: 526
		// (set) Token: 0x0600020F RID: 527
		public extern float bounciness { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000210 RID: 528
		// (set) Token: 0x06000211 RID: 529
		public extern float dynamicFriction { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000212 RID: 530
		// (set) Token: 0x06000213 RID: 531
		public extern float staticFriction { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000214 RID: 532
		// (set) Token: 0x06000215 RID: 533
		public extern PhysicMaterialCombine frictionCombine { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000216 RID: 534
		// (set) Token: 0x06000217 RID: 535
		public extern PhysicMaterialCombine bounceCombine { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000218 RID: 536 RVA: 0x00004A04 File Offset: 0x00002C04
		// (set) Token: 0x06000219 RID: 537 RVA: 0x00004A1C File Offset: 0x00002C1C
		[Obsolete("Use PhysicMaterial.bounciness instead (UnityUpgradable) -> bounciness")]
		public float bouncyness
		{
			get
			{
				return this.bounciness;
			}
			set
			{
				this.bounciness = value;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600021A RID: 538 RVA: 0x00004A28 File Offset: 0x00002C28
		// (set) Token: 0x0600021B RID: 539 RVA: 0x00002193 File Offset: 0x00000393
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Anisotropic friction is no longer supported since Unity 5.0.", true)]
		public Vector3 frictionDirection2
		{
			get
			{
				return Vector3.zero;
			}
			set
			{
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600021C RID: 540 RVA: 0x00004A40 File Offset: 0x00002C40
		// (set) Token: 0x0600021D RID: 541 RVA: 0x00002193 File Offset: 0x00000393
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Anisotropic friction is no longer supported since Unity 5.0.", true)]
		public float dynamicFriction2
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600021E RID: 542 RVA: 0x00004A58 File Offset: 0x00002C58
		// (set) Token: 0x0600021F RID: 543 RVA: 0x00002193 File Offset: 0x00000393
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Anisotropic friction is no longer supported since Unity 5.0.", true)]
		public float staticFriction2
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000220 RID: 544 RVA: 0x00004A70 File Offset: 0x00002C70
		// (set) Token: 0x06000221 RID: 545 RVA: 0x00002193 File Offset: 0x00000393
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Anisotropic friction is no longer supported since Unity 5.0.", true)]
		public Vector3 frictionDirection
		{
			get
			{
				return Vector3.zero;
			}
			set
			{
			}
		}
	}
}
