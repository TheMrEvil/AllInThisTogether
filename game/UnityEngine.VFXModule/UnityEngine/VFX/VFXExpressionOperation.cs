using System;

namespace UnityEngine.VFX
{
	// Token: 0x02000006 RID: 6
	internal enum VFXExpressionOperation
	{
		// Token: 0x04000007 RID: 7
		None,
		// Token: 0x04000008 RID: 8
		Value,
		// Token: 0x04000009 RID: 9
		Combine2f,
		// Token: 0x0400000A RID: 10
		Combine3f,
		// Token: 0x0400000B RID: 11
		Combine4f,
		// Token: 0x0400000C RID: 12
		ExtractComponent,
		// Token: 0x0400000D RID: 13
		DeltaTime,
		// Token: 0x0400000E RID: 14
		TotalTime,
		// Token: 0x0400000F RID: 15
		SystemSeed,
		// Token: 0x04000010 RID: 16
		LocalToWorld,
		// Token: 0x04000011 RID: 17
		WorldToLocal,
		// Token: 0x04000012 RID: 18
		FrameIndex,
		// Token: 0x04000013 RID: 19
		PlayRate,
		// Token: 0x04000014 RID: 20
		UnscaledDeltaTime,
		// Token: 0x04000015 RID: 21
		ManagerMaxDeltaTime,
		// Token: 0x04000016 RID: 22
		ManagerFixedTimeStep,
		// Token: 0x04000017 RID: 23
		GameDeltaTime,
		// Token: 0x04000018 RID: 24
		GameUnscaledDeltaTime,
		// Token: 0x04000019 RID: 25
		GameSmoothDeltaTime,
		// Token: 0x0400001A RID: 26
		GameTotalTime,
		// Token: 0x0400001B RID: 27
		GameUnscaledTotalTime,
		// Token: 0x0400001C RID: 28
		GameTotalTimeSinceSceneLoad,
		// Token: 0x0400001D RID: 29
		GameTimeScale,
		// Token: 0x0400001E RID: 30
		Sin,
		// Token: 0x0400001F RID: 31
		Cos,
		// Token: 0x04000020 RID: 32
		Tan,
		// Token: 0x04000021 RID: 33
		ASin,
		// Token: 0x04000022 RID: 34
		ACos,
		// Token: 0x04000023 RID: 35
		ATan,
		// Token: 0x04000024 RID: 36
		Abs,
		// Token: 0x04000025 RID: 37
		Sign,
		// Token: 0x04000026 RID: 38
		Saturate,
		// Token: 0x04000027 RID: 39
		Ceil,
		// Token: 0x04000028 RID: 40
		Round,
		// Token: 0x04000029 RID: 41
		Frac,
		// Token: 0x0400002A RID: 42
		Floor,
		// Token: 0x0400002B RID: 43
		Log2,
		// Token: 0x0400002C RID: 44
		Mul,
		// Token: 0x0400002D RID: 45
		Divide,
		// Token: 0x0400002E RID: 46
		Add,
		// Token: 0x0400002F RID: 47
		Subtract,
		// Token: 0x04000030 RID: 48
		Min,
		// Token: 0x04000031 RID: 49
		Max,
		// Token: 0x04000032 RID: 50
		Pow,
		// Token: 0x04000033 RID: 51
		ATan2,
		// Token: 0x04000034 RID: 52
		TRSToMatrix,
		// Token: 0x04000035 RID: 53
		InverseMatrix,
		// Token: 0x04000036 RID: 54
		InverseTRSMatrix,
		// Token: 0x04000037 RID: 55
		TransposeMatrix,
		// Token: 0x04000038 RID: 56
		ExtractPositionFromMatrix,
		// Token: 0x04000039 RID: 57
		ExtractAnglesFromMatrix,
		// Token: 0x0400003A RID: 58
		ExtractScaleFromMatrix,
		// Token: 0x0400003B RID: 59
		TransformMatrix,
		// Token: 0x0400003C RID: 60
		TransformPos,
		// Token: 0x0400003D RID: 61
		TransformVec,
		// Token: 0x0400003E RID: 62
		TransformDir,
		// Token: 0x0400003F RID: 63
		TransformVector4,
		// Token: 0x04000040 RID: 64
		Vector3sToMatrix,
		// Token: 0x04000041 RID: 65
		Vector4sToMatrix,
		// Token: 0x04000042 RID: 66
		MatrixToVector3s,
		// Token: 0x04000043 RID: 67
		MatrixToVector4s,
		// Token: 0x04000044 RID: 68
		SampleCurve,
		// Token: 0x04000045 RID: 69
		SampleGradient,
		// Token: 0x04000046 RID: 70
		SampleMeshVertexFloat,
		// Token: 0x04000047 RID: 71
		SampleMeshVertexFloat2,
		// Token: 0x04000048 RID: 72
		SampleMeshVertexFloat3,
		// Token: 0x04000049 RID: 73
		SampleMeshVertexFloat4,
		// Token: 0x0400004A RID: 74
		SampleMeshVertexColor,
		// Token: 0x0400004B RID: 75
		SampleMeshIndex,
		// Token: 0x0400004C RID: 76
		VertexBufferFromMesh,
		// Token: 0x0400004D RID: 77
		VertexBufferFromSkinnedMeshRenderer,
		// Token: 0x0400004E RID: 78
		IndexBufferFromMesh,
		// Token: 0x0400004F RID: 79
		MeshFromSkinnedMeshRenderer,
		// Token: 0x04000050 RID: 80
		BakeCurve,
		// Token: 0x04000051 RID: 81
		BakeGradient,
		// Token: 0x04000052 RID: 82
		BitwiseLeftShift,
		// Token: 0x04000053 RID: 83
		BitwiseRightShift,
		// Token: 0x04000054 RID: 84
		BitwiseOr,
		// Token: 0x04000055 RID: 85
		BitwiseAnd,
		// Token: 0x04000056 RID: 86
		BitwiseXor,
		// Token: 0x04000057 RID: 87
		BitwiseComplement,
		// Token: 0x04000058 RID: 88
		CastUintToFloat,
		// Token: 0x04000059 RID: 89
		CastIntToFloat,
		// Token: 0x0400005A RID: 90
		CastFloatToUint,
		// Token: 0x0400005B RID: 91
		CastIntToUint,
		// Token: 0x0400005C RID: 92
		CastFloatToInt,
		// Token: 0x0400005D RID: 93
		CastUintToInt,
		// Token: 0x0400005E RID: 94
		RGBtoHSV,
		// Token: 0x0400005F RID: 95
		HSVtoRGB,
		// Token: 0x04000060 RID: 96
		Condition,
		// Token: 0x04000061 RID: 97
		Branch,
		// Token: 0x04000062 RID: 98
		GenerateRandom,
		// Token: 0x04000063 RID: 99
		GenerateFixedRandom,
		// Token: 0x04000064 RID: 100
		ExtractMatrixFromMainCamera,
		// Token: 0x04000065 RID: 101
		ExtractFOVFromMainCamera,
		// Token: 0x04000066 RID: 102
		ExtractNearPlaneFromMainCamera,
		// Token: 0x04000067 RID: 103
		ExtractFarPlaneFromMainCamera,
		// Token: 0x04000068 RID: 104
		ExtractAspectRatioFromMainCamera,
		// Token: 0x04000069 RID: 105
		ExtractPixelDimensionsFromMainCamera,
		// Token: 0x0400006A RID: 106
		ExtractLensShiftFromMainCamera,
		// Token: 0x0400006B RID: 107
		GetBufferFromMainCamera,
		// Token: 0x0400006C RID: 108
		IsMainCameraOrthographic,
		// Token: 0x0400006D RID: 109
		GetOrthographicSizeFromMainCamera,
		// Token: 0x0400006E RID: 110
		LogicalAnd,
		// Token: 0x0400006F RID: 111
		LogicalOr,
		// Token: 0x04000070 RID: 112
		LogicalNot,
		// Token: 0x04000071 RID: 113
		ValueNoise1D,
		// Token: 0x04000072 RID: 114
		ValueNoise2D,
		// Token: 0x04000073 RID: 115
		ValueNoise3D,
		// Token: 0x04000074 RID: 116
		ValueCurlNoise2D,
		// Token: 0x04000075 RID: 117
		ValueCurlNoise3D,
		// Token: 0x04000076 RID: 118
		PerlinNoise1D,
		// Token: 0x04000077 RID: 119
		PerlinNoise2D,
		// Token: 0x04000078 RID: 120
		PerlinNoise3D,
		// Token: 0x04000079 RID: 121
		PerlinCurlNoise2D,
		// Token: 0x0400007A RID: 122
		PerlinCurlNoise3D,
		// Token: 0x0400007B RID: 123
		CellularNoise1D,
		// Token: 0x0400007C RID: 124
		CellularNoise2D,
		// Token: 0x0400007D RID: 125
		CellularNoise3D,
		// Token: 0x0400007E RID: 126
		CellularCurlNoise2D,
		// Token: 0x0400007F RID: 127
		CellularCurlNoise3D,
		// Token: 0x04000080 RID: 128
		VoroNoise2D,
		// Token: 0x04000081 RID: 129
		MeshVertexCount,
		// Token: 0x04000082 RID: 130
		MeshChannelOffset,
		// Token: 0x04000083 RID: 131
		MeshChannelInfos,
		// Token: 0x04000084 RID: 132
		MeshVertexStride,
		// Token: 0x04000085 RID: 133
		MeshIndexCount,
		// Token: 0x04000086 RID: 134
		MeshIndexFormat,
		// Token: 0x04000087 RID: 135
		BufferStride,
		// Token: 0x04000088 RID: 136
		BufferCount,
		// Token: 0x04000089 RID: 137
		TextureWidth,
		// Token: 0x0400008A RID: 138
		TextureHeight,
		// Token: 0x0400008B RID: 139
		TextureDepth,
		// Token: 0x0400008C RID: 140
		ReadEventAttribute,
		// Token: 0x0400008D RID: 141
		SpawnerStateNewLoop,
		// Token: 0x0400008E RID: 142
		SpawnerStateLoopState,
		// Token: 0x0400008F RID: 143
		SpawnerStateSpawnCount,
		// Token: 0x04000090 RID: 144
		SpawnerStateDeltaTime,
		// Token: 0x04000091 RID: 145
		SpawnerStateTotalTime,
		// Token: 0x04000092 RID: 146
		SpawnerStateDelayBeforeLoop,
		// Token: 0x04000093 RID: 147
		SpawnerStateLoopDuration,
		// Token: 0x04000094 RID: 148
		SpawnerStateDelayAfterLoop,
		// Token: 0x04000095 RID: 149
		SpawnerStateLoopIndex,
		// Token: 0x04000096 RID: 150
		SpawnerStateLoopCount
	}
}
