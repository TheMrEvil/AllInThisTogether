using System;
using System.ComponentModel;

namespace UnityEngine.Experimental.Rendering
{
	// Token: 0x0200047B RID: 1147
	public enum GraphicsFormat
	{
		// Token: 0x04000EEE RID: 3822
		None,
		// Token: 0x04000EEF RID: 3823
		R8_SRGB,
		// Token: 0x04000EF0 RID: 3824
		R8G8_SRGB,
		// Token: 0x04000EF1 RID: 3825
		R8G8B8_SRGB,
		// Token: 0x04000EF2 RID: 3826
		R8G8B8A8_SRGB,
		// Token: 0x04000EF3 RID: 3827
		R8_UNorm,
		// Token: 0x04000EF4 RID: 3828
		R8G8_UNorm,
		// Token: 0x04000EF5 RID: 3829
		R8G8B8_UNorm,
		// Token: 0x04000EF6 RID: 3830
		R8G8B8A8_UNorm,
		// Token: 0x04000EF7 RID: 3831
		R8_SNorm,
		// Token: 0x04000EF8 RID: 3832
		R8G8_SNorm,
		// Token: 0x04000EF9 RID: 3833
		R8G8B8_SNorm,
		// Token: 0x04000EFA RID: 3834
		R8G8B8A8_SNorm,
		// Token: 0x04000EFB RID: 3835
		R8_UInt,
		// Token: 0x04000EFC RID: 3836
		R8G8_UInt,
		// Token: 0x04000EFD RID: 3837
		R8G8B8_UInt,
		// Token: 0x04000EFE RID: 3838
		R8G8B8A8_UInt,
		// Token: 0x04000EFF RID: 3839
		R8_SInt,
		// Token: 0x04000F00 RID: 3840
		R8G8_SInt,
		// Token: 0x04000F01 RID: 3841
		R8G8B8_SInt,
		// Token: 0x04000F02 RID: 3842
		R8G8B8A8_SInt,
		// Token: 0x04000F03 RID: 3843
		R16_UNorm,
		// Token: 0x04000F04 RID: 3844
		R16G16_UNorm,
		// Token: 0x04000F05 RID: 3845
		R16G16B16_UNorm,
		// Token: 0x04000F06 RID: 3846
		R16G16B16A16_UNorm,
		// Token: 0x04000F07 RID: 3847
		R16_SNorm,
		// Token: 0x04000F08 RID: 3848
		R16G16_SNorm,
		// Token: 0x04000F09 RID: 3849
		R16G16B16_SNorm,
		// Token: 0x04000F0A RID: 3850
		R16G16B16A16_SNorm,
		// Token: 0x04000F0B RID: 3851
		R16_UInt,
		// Token: 0x04000F0C RID: 3852
		R16G16_UInt,
		// Token: 0x04000F0D RID: 3853
		R16G16B16_UInt,
		// Token: 0x04000F0E RID: 3854
		R16G16B16A16_UInt,
		// Token: 0x04000F0F RID: 3855
		R16_SInt,
		// Token: 0x04000F10 RID: 3856
		R16G16_SInt,
		// Token: 0x04000F11 RID: 3857
		R16G16B16_SInt,
		// Token: 0x04000F12 RID: 3858
		R16G16B16A16_SInt,
		// Token: 0x04000F13 RID: 3859
		R32_UInt,
		// Token: 0x04000F14 RID: 3860
		R32G32_UInt,
		// Token: 0x04000F15 RID: 3861
		R32G32B32_UInt,
		// Token: 0x04000F16 RID: 3862
		R32G32B32A32_UInt,
		// Token: 0x04000F17 RID: 3863
		R32_SInt,
		// Token: 0x04000F18 RID: 3864
		R32G32_SInt,
		// Token: 0x04000F19 RID: 3865
		R32G32B32_SInt,
		// Token: 0x04000F1A RID: 3866
		R32G32B32A32_SInt,
		// Token: 0x04000F1B RID: 3867
		R16_SFloat,
		// Token: 0x04000F1C RID: 3868
		R16G16_SFloat,
		// Token: 0x04000F1D RID: 3869
		R16G16B16_SFloat,
		// Token: 0x04000F1E RID: 3870
		R16G16B16A16_SFloat,
		// Token: 0x04000F1F RID: 3871
		R32_SFloat,
		// Token: 0x04000F20 RID: 3872
		R32G32_SFloat,
		// Token: 0x04000F21 RID: 3873
		R32G32B32_SFloat,
		// Token: 0x04000F22 RID: 3874
		R32G32B32A32_SFloat,
		// Token: 0x04000F23 RID: 3875
		B8G8R8_SRGB = 56,
		// Token: 0x04000F24 RID: 3876
		B8G8R8A8_SRGB,
		// Token: 0x04000F25 RID: 3877
		B8G8R8_UNorm,
		// Token: 0x04000F26 RID: 3878
		B8G8R8A8_UNorm,
		// Token: 0x04000F27 RID: 3879
		B8G8R8_SNorm,
		// Token: 0x04000F28 RID: 3880
		B8G8R8A8_SNorm,
		// Token: 0x04000F29 RID: 3881
		B8G8R8_UInt,
		// Token: 0x04000F2A RID: 3882
		B8G8R8A8_UInt,
		// Token: 0x04000F2B RID: 3883
		B8G8R8_SInt,
		// Token: 0x04000F2C RID: 3884
		B8G8R8A8_SInt,
		// Token: 0x04000F2D RID: 3885
		R4G4B4A4_UNormPack16,
		// Token: 0x04000F2E RID: 3886
		B4G4R4A4_UNormPack16,
		// Token: 0x04000F2F RID: 3887
		R5G6B5_UNormPack16,
		// Token: 0x04000F30 RID: 3888
		B5G6R5_UNormPack16,
		// Token: 0x04000F31 RID: 3889
		R5G5B5A1_UNormPack16,
		// Token: 0x04000F32 RID: 3890
		B5G5R5A1_UNormPack16,
		// Token: 0x04000F33 RID: 3891
		A1R5G5B5_UNormPack16,
		// Token: 0x04000F34 RID: 3892
		E5B9G9R9_UFloatPack32,
		// Token: 0x04000F35 RID: 3893
		B10G11R11_UFloatPack32,
		// Token: 0x04000F36 RID: 3894
		A2B10G10R10_UNormPack32,
		// Token: 0x04000F37 RID: 3895
		A2B10G10R10_UIntPack32,
		// Token: 0x04000F38 RID: 3896
		A2B10G10R10_SIntPack32,
		// Token: 0x04000F39 RID: 3897
		A2R10G10B10_UNormPack32,
		// Token: 0x04000F3A RID: 3898
		A2R10G10B10_UIntPack32,
		// Token: 0x04000F3B RID: 3899
		A2R10G10B10_SIntPack32,
		// Token: 0x04000F3C RID: 3900
		A2R10G10B10_XRSRGBPack32,
		// Token: 0x04000F3D RID: 3901
		A2R10G10B10_XRUNormPack32,
		// Token: 0x04000F3E RID: 3902
		R10G10B10_XRSRGBPack32,
		// Token: 0x04000F3F RID: 3903
		R10G10B10_XRUNormPack32,
		// Token: 0x04000F40 RID: 3904
		A10R10G10B10_XRSRGBPack32,
		// Token: 0x04000F41 RID: 3905
		A10R10G10B10_XRUNormPack32,
		// Token: 0x04000F42 RID: 3906
		D16_UNorm = 90,
		// Token: 0x04000F43 RID: 3907
		D24_UNorm,
		// Token: 0x04000F44 RID: 3908
		D24_UNorm_S8_UInt,
		// Token: 0x04000F45 RID: 3909
		D32_SFloat,
		// Token: 0x04000F46 RID: 3910
		D32_SFloat_S8_UInt,
		// Token: 0x04000F47 RID: 3911
		S8_UInt,
		// Token: 0x04000F48 RID: 3912
		[Obsolete("Enum member GraphicsFormat.RGB_DXT1_SRGB has been deprecated. Use GraphicsFormat.RGBA_DXT1_SRGB instead (UnityUpgradable) -> RGBA_DXT1_SRGB", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		RGB_DXT1_SRGB,
		// Token: 0x04000F49 RID: 3913
		RGBA_DXT1_SRGB = 96,
		// Token: 0x04000F4A RID: 3914
		[Obsolete("Enum member GraphicsFormat.RGB_DXT1_UNorm has been deprecated. Use GraphicsFormat.RGBA_DXT1_UNorm instead (UnityUpgradable) -> RGBA_DXT1_UNorm", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		RGB_DXT1_UNorm,
		// Token: 0x04000F4B RID: 3915
		RGBA_DXT1_UNorm = 97,
		// Token: 0x04000F4C RID: 3916
		RGBA_DXT3_SRGB,
		// Token: 0x04000F4D RID: 3917
		RGBA_DXT3_UNorm,
		// Token: 0x04000F4E RID: 3918
		RGBA_DXT5_SRGB,
		// Token: 0x04000F4F RID: 3919
		RGBA_DXT5_UNorm,
		// Token: 0x04000F50 RID: 3920
		R_BC4_UNorm,
		// Token: 0x04000F51 RID: 3921
		R_BC4_SNorm,
		// Token: 0x04000F52 RID: 3922
		RG_BC5_UNorm,
		// Token: 0x04000F53 RID: 3923
		RG_BC5_SNorm,
		// Token: 0x04000F54 RID: 3924
		RGB_BC6H_UFloat,
		// Token: 0x04000F55 RID: 3925
		RGB_BC6H_SFloat,
		// Token: 0x04000F56 RID: 3926
		RGBA_BC7_SRGB,
		// Token: 0x04000F57 RID: 3927
		RGBA_BC7_UNorm,
		// Token: 0x04000F58 RID: 3928
		RGB_PVRTC_2Bpp_SRGB,
		// Token: 0x04000F59 RID: 3929
		RGB_PVRTC_2Bpp_UNorm,
		// Token: 0x04000F5A RID: 3930
		RGB_PVRTC_4Bpp_SRGB,
		// Token: 0x04000F5B RID: 3931
		RGB_PVRTC_4Bpp_UNorm,
		// Token: 0x04000F5C RID: 3932
		RGBA_PVRTC_2Bpp_SRGB,
		// Token: 0x04000F5D RID: 3933
		RGBA_PVRTC_2Bpp_UNorm,
		// Token: 0x04000F5E RID: 3934
		RGBA_PVRTC_4Bpp_SRGB,
		// Token: 0x04000F5F RID: 3935
		RGBA_PVRTC_4Bpp_UNorm,
		// Token: 0x04000F60 RID: 3936
		RGB_ETC_UNorm,
		// Token: 0x04000F61 RID: 3937
		RGB_ETC2_SRGB,
		// Token: 0x04000F62 RID: 3938
		RGB_ETC2_UNorm,
		// Token: 0x04000F63 RID: 3939
		RGB_A1_ETC2_SRGB,
		// Token: 0x04000F64 RID: 3940
		RGB_A1_ETC2_UNorm,
		// Token: 0x04000F65 RID: 3941
		RGBA_ETC2_SRGB,
		// Token: 0x04000F66 RID: 3942
		RGBA_ETC2_UNorm,
		// Token: 0x04000F67 RID: 3943
		R_EAC_UNorm,
		// Token: 0x04000F68 RID: 3944
		R_EAC_SNorm,
		// Token: 0x04000F69 RID: 3945
		RG_EAC_UNorm,
		// Token: 0x04000F6A RID: 3946
		RG_EAC_SNorm,
		// Token: 0x04000F6B RID: 3947
		RGBA_ASTC4X4_SRGB,
		// Token: 0x04000F6C RID: 3948
		RGBA_ASTC4X4_UNorm,
		// Token: 0x04000F6D RID: 3949
		RGBA_ASTC5X5_SRGB,
		// Token: 0x04000F6E RID: 3950
		RGBA_ASTC5X5_UNorm,
		// Token: 0x04000F6F RID: 3951
		RGBA_ASTC6X6_SRGB,
		// Token: 0x04000F70 RID: 3952
		RGBA_ASTC6X6_UNorm,
		// Token: 0x04000F71 RID: 3953
		RGBA_ASTC8X8_SRGB,
		// Token: 0x04000F72 RID: 3954
		RGBA_ASTC8X8_UNorm,
		// Token: 0x04000F73 RID: 3955
		RGBA_ASTC10X10_SRGB,
		// Token: 0x04000F74 RID: 3956
		RGBA_ASTC10X10_UNorm,
		// Token: 0x04000F75 RID: 3957
		RGBA_ASTC12X12_SRGB,
		// Token: 0x04000F76 RID: 3958
		RGBA_ASTC12X12_UNorm,
		// Token: 0x04000F77 RID: 3959
		YUV2,
		// Token: 0x04000F78 RID: 3960
		[Obsolete("Enum member GraphicsFormat.DepthAuto has been deprecated. Use GraphicsFormat.None as a color format to indicate depth only rendering and DefaultFormat to get the default depth buffer format.", false)]
		DepthAuto,
		// Token: 0x04000F79 RID: 3961
		[Obsolete("Enum member GraphicsFormat.ShadowAuto has been deprecated. Use GraphicsFormat.None as a color format to indicate depth only rendering and DefaultFormat to get the default shadow buffer format.", false)]
		ShadowAuto,
		// Token: 0x04000F7A RID: 3962
		[Obsolete("Enum member GraphicsFormat.VideoAuto has been deprecated. Use DefaultFormat instead.", false)]
		VideoAuto,
		// Token: 0x04000F7B RID: 3963
		RGBA_ASTC4X4_UFloat,
		// Token: 0x04000F7C RID: 3964
		RGBA_ASTC5X5_UFloat,
		// Token: 0x04000F7D RID: 3965
		RGBA_ASTC6X6_UFloat,
		// Token: 0x04000F7E RID: 3966
		RGBA_ASTC8X8_UFloat,
		// Token: 0x04000F7F RID: 3967
		RGBA_ASTC10X10_UFloat,
		// Token: 0x04000F80 RID: 3968
		RGBA_ASTC12X12_UFloat
	}
}
