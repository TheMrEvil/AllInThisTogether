using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200005B RID: 91
	[NativeHeader("Modules/ParticleSystem/ScriptBindings/ParticleSystemScriptBindings.h")]
	[RequireComponent(typeof(Transform))]
	[NativeHeader("Modules/ParticleSystem/ParticleSystemForceFieldManager.h")]
	[NativeHeader("Modules/ParticleSystem/ParticleSystemForceField.h")]
	[NativeHeader("ParticleSystemScriptingClasses.h")]
	[NativeHeader("Modules/ParticleSystem/ParticleSystem.h")]
	public class ParticleSystemForceField : Behaviour
	{
		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x060006FA RID: 1786
		// (set) Token: 0x060006FB RID: 1787
		[NativeName("ForceShape")]
		public extern ParticleSystemForceFieldShape shape { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x060006FC RID: 1788
		// (set) Token: 0x060006FD RID: 1789
		public extern float startRange { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x060006FE RID: 1790
		// (set) Token: 0x060006FF RID: 1791
		public extern float endRange { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000700 RID: 1792
		// (set) Token: 0x06000701 RID: 1793
		public extern float length { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000702 RID: 1794
		// (set) Token: 0x06000703 RID: 1795
		public extern float gravityFocus { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000704 RID: 1796 RVA: 0x00006440 File Offset: 0x00004640
		// (set) Token: 0x06000705 RID: 1797 RVA: 0x00006456 File Offset: 0x00004656
		public Vector2 rotationRandomness
		{
			get
			{
				Vector2 result;
				this.get_rotationRandomness_Injected(out result);
				return result;
			}
			set
			{
				this.set_rotationRandomness_Injected(ref value);
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000706 RID: 1798
		// (set) Token: 0x06000707 RID: 1799
		public extern bool multiplyDragByParticleSize { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06000708 RID: 1800
		// (set) Token: 0x06000709 RID: 1801
		public extern bool multiplyDragByParticleVelocity { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x0600070A RID: 1802
		// (set) Token: 0x0600070B RID: 1803
		public extern Texture3D vectorField { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x0600070C RID: 1804 RVA: 0x00006460 File Offset: 0x00004660
		// (set) Token: 0x0600070D RID: 1805 RVA: 0x00006476 File Offset: 0x00004676
		public ParticleSystem.MinMaxCurve directionX
		{
			get
			{
				ParticleSystem.MinMaxCurve result;
				this.get_directionX_Injected(out result);
				return result;
			}
			set
			{
				this.set_directionX_Injected(ref value);
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x0600070E RID: 1806 RVA: 0x00006480 File Offset: 0x00004680
		// (set) Token: 0x0600070F RID: 1807 RVA: 0x00006496 File Offset: 0x00004696
		public ParticleSystem.MinMaxCurve directionY
		{
			get
			{
				ParticleSystem.MinMaxCurve result;
				this.get_directionY_Injected(out result);
				return result;
			}
			set
			{
				this.set_directionY_Injected(ref value);
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000710 RID: 1808 RVA: 0x000064A0 File Offset: 0x000046A0
		// (set) Token: 0x06000711 RID: 1809 RVA: 0x000064B6 File Offset: 0x000046B6
		public ParticleSystem.MinMaxCurve directionZ
		{
			get
			{
				ParticleSystem.MinMaxCurve result;
				this.get_directionZ_Injected(out result);
				return result;
			}
			set
			{
				this.set_directionZ_Injected(ref value);
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000712 RID: 1810 RVA: 0x000064C0 File Offset: 0x000046C0
		// (set) Token: 0x06000713 RID: 1811 RVA: 0x000064D6 File Offset: 0x000046D6
		public ParticleSystem.MinMaxCurve gravity
		{
			get
			{
				ParticleSystem.MinMaxCurve result;
				this.get_gravity_Injected(out result);
				return result;
			}
			set
			{
				this.set_gravity_Injected(ref value);
			}
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000714 RID: 1812 RVA: 0x000064E0 File Offset: 0x000046E0
		// (set) Token: 0x06000715 RID: 1813 RVA: 0x000064F6 File Offset: 0x000046F6
		public ParticleSystem.MinMaxCurve rotationSpeed
		{
			get
			{
				ParticleSystem.MinMaxCurve result;
				this.get_rotationSpeed_Injected(out result);
				return result;
			}
			set
			{
				this.set_rotationSpeed_Injected(ref value);
			}
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000716 RID: 1814 RVA: 0x00006500 File Offset: 0x00004700
		// (set) Token: 0x06000717 RID: 1815 RVA: 0x00006516 File Offset: 0x00004716
		public ParticleSystem.MinMaxCurve rotationAttraction
		{
			get
			{
				ParticleSystem.MinMaxCurve result;
				this.get_rotationAttraction_Injected(out result);
				return result;
			}
			set
			{
				this.set_rotationAttraction_Injected(ref value);
			}
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000718 RID: 1816 RVA: 0x00006520 File Offset: 0x00004720
		// (set) Token: 0x06000719 RID: 1817 RVA: 0x00006536 File Offset: 0x00004736
		public ParticleSystem.MinMaxCurve drag
		{
			get
			{
				ParticleSystem.MinMaxCurve result;
				this.get_drag_Injected(out result);
				return result;
			}
			set
			{
				this.set_drag_Injected(ref value);
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x0600071A RID: 1818 RVA: 0x00006540 File Offset: 0x00004740
		// (set) Token: 0x0600071B RID: 1819 RVA: 0x00006556 File Offset: 0x00004756
		public ParticleSystem.MinMaxCurve vectorFieldSpeed
		{
			get
			{
				ParticleSystem.MinMaxCurve result;
				this.get_vectorFieldSpeed_Injected(out result);
				return result;
			}
			set
			{
				this.set_vectorFieldSpeed_Injected(ref value);
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x0600071C RID: 1820 RVA: 0x00006560 File Offset: 0x00004760
		// (set) Token: 0x0600071D RID: 1821 RVA: 0x00006576 File Offset: 0x00004776
		public ParticleSystem.MinMaxCurve vectorFieldAttraction
		{
			get
			{
				ParticleSystem.MinMaxCurve result;
				this.get_vectorFieldAttraction_Injected(out result);
				return result;
			}
			set
			{
				this.set_vectorFieldAttraction_Injected(ref value);
			}
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x00006580 File Offset: 0x00004780
		public ParticleSystemForceField()
		{
		}

		// Token: 0x0600071F RID: 1823
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_rotationRandomness_Injected(out Vector2 ret);

		// Token: 0x06000720 RID: 1824
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_rotationRandomness_Injected(ref Vector2 value);

		// Token: 0x06000721 RID: 1825
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_directionX_Injected(out ParticleSystem.MinMaxCurve ret);

		// Token: 0x06000722 RID: 1826
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_directionX_Injected(ref ParticleSystem.MinMaxCurve value);

		// Token: 0x06000723 RID: 1827
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_directionY_Injected(out ParticleSystem.MinMaxCurve ret);

		// Token: 0x06000724 RID: 1828
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_directionY_Injected(ref ParticleSystem.MinMaxCurve value);

		// Token: 0x06000725 RID: 1829
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_directionZ_Injected(out ParticleSystem.MinMaxCurve ret);

		// Token: 0x06000726 RID: 1830
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_directionZ_Injected(ref ParticleSystem.MinMaxCurve value);

		// Token: 0x06000727 RID: 1831
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_gravity_Injected(out ParticleSystem.MinMaxCurve ret);

		// Token: 0x06000728 RID: 1832
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_gravity_Injected(ref ParticleSystem.MinMaxCurve value);

		// Token: 0x06000729 RID: 1833
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_rotationSpeed_Injected(out ParticleSystem.MinMaxCurve ret);

		// Token: 0x0600072A RID: 1834
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_rotationSpeed_Injected(ref ParticleSystem.MinMaxCurve value);

		// Token: 0x0600072B RID: 1835
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_rotationAttraction_Injected(out ParticleSystem.MinMaxCurve ret);

		// Token: 0x0600072C RID: 1836
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_rotationAttraction_Injected(ref ParticleSystem.MinMaxCurve value);

		// Token: 0x0600072D RID: 1837
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_drag_Injected(out ParticleSystem.MinMaxCurve ret);

		// Token: 0x0600072E RID: 1838
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_drag_Injected(ref ParticleSystem.MinMaxCurve value);

		// Token: 0x0600072F RID: 1839
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_vectorFieldSpeed_Injected(out ParticleSystem.MinMaxCurve ret);

		// Token: 0x06000730 RID: 1840
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_vectorFieldSpeed_Injected(ref ParticleSystem.MinMaxCurve value);

		// Token: 0x06000731 RID: 1841
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_vectorFieldAttraction_Injected(out ParticleSystem.MinMaxCurve ret);

		// Token: 0x06000732 RID: 1842
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_vectorFieldAttraction_Injected(ref ParticleSystem.MinMaxCurve value);
	}
}
