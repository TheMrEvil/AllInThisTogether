using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000187 RID: 391
	[NativeHeader("Runtime/Camera/LightProbeProxyVolume.h")]
	public sealed class LightProbeProxyVolume : Behaviour
	{
		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06000EAC RID: 3756
		public static extern bool isFeatureSupported { [NativeName("IsFeatureSupported")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06000EAD RID: 3757 RVA: 0x00012CFC File Offset: 0x00010EFC
		[NativeName("GlobalAABB")]
		public Bounds boundsGlobal
		{
			get
			{
				Bounds result;
				this.get_boundsGlobal_Injected(out result);
				return result;
			}
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06000EAE RID: 3758 RVA: 0x00012D14 File Offset: 0x00010F14
		// (set) Token: 0x06000EAF RID: 3759 RVA: 0x00012D2A File Offset: 0x00010F2A
		[NativeName("BoundingBoxSizeCustom")]
		public Vector3 sizeCustom
		{
			get
			{
				Vector3 result;
				this.get_sizeCustom_Injected(out result);
				return result;
			}
			set
			{
				this.set_sizeCustom_Injected(ref value);
			}
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06000EB0 RID: 3760 RVA: 0x00012D34 File Offset: 0x00010F34
		// (set) Token: 0x06000EB1 RID: 3761 RVA: 0x00012D4A File Offset: 0x00010F4A
		[NativeName("BoundingBoxOriginCustom")]
		public Vector3 originCustom
		{
			get
			{
				Vector3 result;
				this.get_originCustom_Injected(out result);
				return result;
			}
			set
			{
				this.set_originCustom_Injected(ref value);
			}
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06000EB2 RID: 3762
		// (set) Token: 0x06000EB3 RID: 3763
		public extern float probeDensity { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06000EB4 RID: 3764
		// (set) Token: 0x06000EB5 RID: 3765
		public extern int gridResolutionX { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06000EB6 RID: 3766
		// (set) Token: 0x06000EB7 RID: 3767
		public extern int gridResolutionY { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06000EB8 RID: 3768
		// (set) Token: 0x06000EB9 RID: 3769
		public extern int gridResolutionZ { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06000EBA RID: 3770
		// (set) Token: 0x06000EBB RID: 3771
		public extern LightProbeProxyVolume.BoundingBoxMode boundingBoxMode { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06000EBC RID: 3772
		// (set) Token: 0x06000EBD RID: 3773
		public extern LightProbeProxyVolume.ResolutionMode resolutionMode { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06000EBE RID: 3774
		// (set) Token: 0x06000EBF RID: 3775
		public extern LightProbeProxyVolume.ProbePositionMode probePositionMode { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06000EC0 RID: 3776
		// (set) Token: 0x06000EC1 RID: 3777
		public extern LightProbeProxyVolume.RefreshMode refreshMode { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06000EC2 RID: 3778
		// (set) Token: 0x06000EC3 RID: 3779
		public extern LightProbeProxyVolume.QualityMode qualityMode { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06000EC4 RID: 3780
		// (set) Token: 0x06000EC5 RID: 3781
		public extern LightProbeProxyVolume.DataFormat dataFormat { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06000EC6 RID: 3782 RVA: 0x00012D54 File Offset: 0x00010F54
		public void Update()
		{
			this.SetDirtyFlag(true);
		}

		// Token: 0x06000EC7 RID: 3783
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetDirtyFlag(bool flag);

		// Token: 0x06000EC8 RID: 3784 RVA: 0x000084C0 File Offset: 0x000066C0
		public LightProbeProxyVolume()
		{
		}

		// Token: 0x06000EC9 RID: 3785
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_boundsGlobal_Injected(out Bounds ret);

		// Token: 0x06000ECA RID: 3786
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_sizeCustom_Injected(out Vector3 ret);

		// Token: 0x06000ECB RID: 3787
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_sizeCustom_Injected(ref Vector3 value);

		// Token: 0x06000ECC RID: 3788
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_originCustom_Injected(out Vector3 ret);

		// Token: 0x06000ECD RID: 3789
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_originCustom_Injected(ref Vector3 value);

		// Token: 0x02000188 RID: 392
		public enum ResolutionMode
		{
			// Token: 0x0400057A RID: 1402
			Automatic,
			// Token: 0x0400057B RID: 1403
			Custom
		}

		// Token: 0x02000189 RID: 393
		public enum BoundingBoxMode
		{
			// Token: 0x0400057D RID: 1405
			AutomaticLocal,
			// Token: 0x0400057E RID: 1406
			AutomaticWorld,
			// Token: 0x0400057F RID: 1407
			Custom
		}

		// Token: 0x0200018A RID: 394
		public enum ProbePositionMode
		{
			// Token: 0x04000581 RID: 1409
			CellCorner,
			// Token: 0x04000582 RID: 1410
			CellCenter
		}

		// Token: 0x0200018B RID: 395
		public enum RefreshMode
		{
			// Token: 0x04000584 RID: 1412
			Automatic,
			// Token: 0x04000585 RID: 1413
			EveryFrame,
			// Token: 0x04000586 RID: 1414
			ViaScripting
		}

		// Token: 0x0200018C RID: 396
		public enum QualityMode
		{
			// Token: 0x04000588 RID: 1416
			Low,
			// Token: 0x04000589 RID: 1417
			Normal
		}

		// Token: 0x0200018D RID: 397
		public enum DataFormat
		{
			// Token: 0x0400058B RID: 1419
			HalfFloat,
			// Token: 0x0400058C RID: 1420
			Float
		}
	}
}
