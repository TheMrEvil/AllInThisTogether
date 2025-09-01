using System;
using UnityEngine;

namespace FidelityFX
{
	// Token: 0x02000004 RID: 4
	public static class Fsr3ShaderIDs
	{
		// Token: 0x0600000C RID: 12 RVA: 0x0000227C File Offset: 0x0000047C
		// Note: this type is marked as 'beforefieldinit'.
		static Fsr3ShaderIDs()
		{
		}

		// Token: 0x04000006 RID: 6
		public static readonly int SrvInputColor = Shader.PropertyToID("r_input_color_jittered");

		// Token: 0x04000007 RID: 7
		public static readonly int SrvOpaqueOnly = Shader.PropertyToID("r_input_opaque_only");

		// Token: 0x04000008 RID: 8
		public static readonly int SrvInputMotionVectors = Shader.PropertyToID("r_input_motion_vectors");

		// Token: 0x04000009 RID: 9
		public static readonly int SrvInputDepth = Shader.PropertyToID("r_input_depth");

		// Token: 0x0400000A RID: 10
		public static readonly int SrvInputExposure = Shader.PropertyToID("r_input_exposure");

		// Token: 0x0400000B RID: 11
		public static readonly int SrvFrameInfo = Shader.PropertyToID("r_frame_info");

		// Token: 0x0400000C RID: 12
		public static readonly int SrvReactiveMask = Shader.PropertyToID("r_reactive_mask");

		// Token: 0x0400000D RID: 13
		public static readonly int SrvTransparencyAndCompositionMask = Shader.PropertyToID("r_transparency_and_composition_mask");

		// Token: 0x0400000E RID: 14
		public static readonly int SrvReconstructedPrevNearestDepth = Shader.PropertyToID("r_reconstructed_previous_nearest_depth");

		// Token: 0x0400000F RID: 15
		public static readonly int SrvDilatedMotionVectors = Shader.PropertyToID("r_dilated_motion_vectors");

		// Token: 0x04000010 RID: 16
		public static readonly int SrvDilatedDepth = Shader.PropertyToID("r_dilated_depth");

		// Token: 0x04000011 RID: 17
		public static readonly int SrvInternalUpscaled = Shader.PropertyToID("r_internal_upscaled_color");

		// Token: 0x04000012 RID: 18
		public static readonly int SrvAccumulation = Shader.PropertyToID("r_accumulation");

		// Token: 0x04000013 RID: 19
		public static readonly int SrvLumaHistory = Shader.PropertyToID("r_luma_history");

		// Token: 0x04000014 RID: 20
		public static readonly int SrvRcasInput = Shader.PropertyToID("r_rcas_input");

		// Token: 0x04000015 RID: 21
		public static readonly int SrvLanczosLut = Shader.PropertyToID("r_lanczos_lut");

		// Token: 0x04000016 RID: 22
		public static readonly int SrvSpdMips = Shader.PropertyToID("r_spd_mips");

		// Token: 0x04000017 RID: 23
		public static readonly int SrvDilatedReactiveMasks = Shader.PropertyToID("r_dilated_reactive_masks");

		// Token: 0x04000018 RID: 24
		public static readonly int SrvNewLocks = Shader.PropertyToID("r_new_locks");

		// Token: 0x04000019 RID: 25
		public static readonly int SrvFarthestDepth = Shader.PropertyToID("r_farthest_depth");

		// Token: 0x0400001A RID: 26
		public static readonly int SrvFarthestDepthMip1 = Shader.PropertyToID("r_farthest_depth_mip1");

		// Token: 0x0400001B RID: 27
		public static readonly int SrvShadingChange = Shader.PropertyToID("r_shading_change");

		// Token: 0x0400001C RID: 28
		public static readonly int SrvCurrentLuma = Shader.PropertyToID("r_current_luma");

		// Token: 0x0400001D RID: 29
		public static readonly int SrvPreviousLuma = Shader.PropertyToID("r_previous_luma");

		// Token: 0x0400001E RID: 30
		public static readonly int SrvLumaInstability = Shader.PropertyToID("r_luma_instability");

		// Token: 0x0400001F RID: 31
		public static readonly int SrvPrevColorPreAlpha = Shader.PropertyToID("r_input_prev_color_pre_alpha");

		// Token: 0x04000020 RID: 32
		public static readonly int SrvPrevColorPostAlpha = Shader.PropertyToID("r_input_prev_color_post_alpha");

		// Token: 0x04000021 RID: 33
		public static readonly int UavReconstructedPrevNearestDepth = Shader.PropertyToID("rw_reconstructed_previous_nearest_depth");

		// Token: 0x04000022 RID: 34
		public static readonly int UavDilatedMotionVectors = Shader.PropertyToID("rw_dilated_motion_vectors");

		// Token: 0x04000023 RID: 35
		public static readonly int UavDilatedDepth = Shader.PropertyToID("rw_dilated_depth");

		// Token: 0x04000024 RID: 36
		public static readonly int UavInternalUpscaled = Shader.PropertyToID("rw_internal_upscaled_color");

		// Token: 0x04000025 RID: 37
		public static readonly int UavAccumulation = Shader.PropertyToID("rw_accumulation");

		// Token: 0x04000026 RID: 38
		public static readonly int UavLumaHistory = Shader.PropertyToID("rw_luma_history");

		// Token: 0x04000027 RID: 39
		public static readonly int UavUpscaledOutput = Shader.PropertyToID("rw_upscaled_output");

		// Token: 0x04000028 RID: 40
		public static readonly int UavDilatedReactiveMasks = Shader.PropertyToID("rw_dilated_reactive_masks");

		// Token: 0x04000029 RID: 41
		public static readonly int UavFrameInfo = Shader.PropertyToID("rw_frame_info");

		// Token: 0x0400002A RID: 42
		public static readonly int UavSpdAtomicCount = Shader.PropertyToID("rw_spd_global_atomic");

		// Token: 0x0400002B RID: 43
		public static readonly int UavNewLocks = Shader.PropertyToID("rw_new_locks");

		// Token: 0x0400002C RID: 44
		public static readonly int UavAutoReactive = Shader.PropertyToID("rw_output_autoreactive");

		// Token: 0x0400002D RID: 45
		public static readonly int UavShadingChange = Shader.PropertyToID("rw_shading_change");

		// Token: 0x0400002E RID: 46
		public static readonly int UavFarthestDepth = Shader.PropertyToID("rw_farthest_depth");

		// Token: 0x0400002F RID: 47
		public static readonly int UavFarthestDepthMip1 = Shader.PropertyToID("rw_farthest_depth_mip1");

		// Token: 0x04000030 RID: 48
		public static readonly int UavCurrentLuma = Shader.PropertyToID("rw_current_luma");

		// Token: 0x04000031 RID: 49
		public static readonly int UavLumaInstability = Shader.PropertyToID("rw_luma_instability");

		// Token: 0x04000032 RID: 50
		public static readonly int UavIntermediate = Shader.PropertyToID("rw_intermediate_fp16x1");

		// Token: 0x04000033 RID: 51
		public static readonly int UavSpdMip0 = Shader.PropertyToID("rw_spd_mip0");

		// Token: 0x04000034 RID: 52
		public static readonly int UavSpdMip1 = Shader.PropertyToID("rw_spd_mip1");

		// Token: 0x04000035 RID: 53
		public static readonly int UavSpdMip2 = Shader.PropertyToID("rw_spd_mip2");

		// Token: 0x04000036 RID: 54
		public static readonly int UavSpdMip3 = Shader.PropertyToID("rw_spd_mip3");

		// Token: 0x04000037 RID: 55
		public static readonly int UavSpdMip4 = Shader.PropertyToID("rw_spd_mip4");

		// Token: 0x04000038 RID: 56
		public static readonly int UavSpdMip5 = Shader.PropertyToID("rw_spd_mip5");

		// Token: 0x04000039 RID: 57
		public static readonly int UavAutoComposition = Shader.PropertyToID("rw_output_autocomposition");

		// Token: 0x0400003A RID: 58
		public static readonly int UavPrevColorPreAlpha = Shader.PropertyToID("rw_output_prev_color_pre_alpha");

		// Token: 0x0400003B RID: 59
		public static readonly int UavPrevColorPostAlpha = Shader.PropertyToID("rw_output_prev_color_post_alpha");

		// Token: 0x0400003C RID: 60
		public static readonly int CbFsr3Upscaler = Shader.PropertyToID("cbFSR3Upscaler");

		// Token: 0x0400003D RID: 61
		public static readonly int CbSpd = Shader.PropertyToID("cbSPD");

		// Token: 0x0400003E RID: 62
		public static readonly int CbRcas = Shader.PropertyToID("cbRCAS");

		// Token: 0x0400003F RID: 63
		public static readonly int CbGenReactive = Shader.PropertyToID("cbGenerateReactive");
	}
}
