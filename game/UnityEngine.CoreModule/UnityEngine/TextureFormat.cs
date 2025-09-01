using System;
using System.ComponentModel;

namespace UnityEngine
{
	// Token: 0x0200017C RID: 380
	public enum TextureFormat
	{
		// Token: 0x040004E4 RID: 1252
		Alpha8 = 1,
		// Token: 0x040004E5 RID: 1253
		ARGB4444,
		// Token: 0x040004E6 RID: 1254
		RGB24,
		// Token: 0x040004E7 RID: 1255
		RGBA32,
		// Token: 0x040004E8 RID: 1256
		ARGB32,
		// Token: 0x040004E9 RID: 1257
		RGB565 = 7,
		// Token: 0x040004EA RID: 1258
		R16 = 9,
		// Token: 0x040004EB RID: 1259
		DXT1,
		// Token: 0x040004EC RID: 1260
		DXT5 = 12,
		// Token: 0x040004ED RID: 1261
		RGBA4444,
		// Token: 0x040004EE RID: 1262
		BGRA32,
		// Token: 0x040004EF RID: 1263
		RHalf,
		// Token: 0x040004F0 RID: 1264
		RGHalf,
		// Token: 0x040004F1 RID: 1265
		RGBAHalf,
		// Token: 0x040004F2 RID: 1266
		RFloat,
		// Token: 0x040004F3 RID: 1267
		RGFloat,
		// Token: 0x040004F4 RID: 1268
		RGBAFloat,
		// Token: 0x040004F5 RID: 1269
		YUY2,
		// Token: 0x040004F6 RID: 1270
		RGB9e5Float,
		// Token: 0x040004F7 RID: 1271
		BC4 = 26,
		// Token: 0x040004F8 RID: 1272
		BC5,
		// Token: 0x040004F9 RID: 1273
		BC6H = 24,
		// Token: 0x040004FA RID: 1274
		BC7,
		// Token: 0x040004FB RID: 1275
		DXT1Crunched = 28,
		// Token: 0x040004FC RID: 1276
		DXT5Crunched,
		// Token: 0x040004FD RID: 1277
		PVRTC_RGB2,
		// Token: 0x040004FE RID: 1278
		PVRTC_RGBA2,
		// Token: 0x040004FF RID: 1279
		PVRTC_RGB4,
		// Token: 0x04000500 RID: 1280
		PVRTC_RGBA4,
		// Token: 0x04000501 RID: 1281
		ETC_RGB4,
		// Token: 0x04000502 RID: 1282
		EAC_R = 41,
		// Token: 0x04000503 RID: 1283
		EAC_R_SIGNED,
		// Token: 0x04000504 RID: 1284
		EAC_RG,
		// Token: 0x04000505 RID: 1285
		EAC_RG_SIGNED,
		// Token: 0x04000506 RID: 1286
		ETC2_RGB,
		// Token: 0x04000507 RID: 1287
		ETC2_RGBA1,
		// Token: 0x04000508 RID: 1288
		ETC2_RGBA8,
		// Token: 0x04000509 RID: 1289
		ASTC_4x4,
		// Token: 0x0400050A RID: 1290
		ASTC_5x5,
		// Token: 0x0400050B RID: 1291
		ASTC_6x6,
		// Token: 0x0400050C RID: 1292
		ASTC_8x8,
		// Token: 0x0400050D RID: 1293
		ASTC_10x10,
		// Token: 0x0400050E RID: 1294
		ASTC_12x12,
		// Token: 0x0400050F RID: 1295
		[Obsolete("Nintendo 3DS is no longer supported.")]
		ETC_RGB4_3DS = 60,
		// Token: 0x04000510 RID: 1296
		[Obsolete("Nintendo 3DS is no longer supported.")]
		ETC_RGBA8_3DS,
		// Token: 0x04000511 RID: 1297
		RG16,
		// Token: 0x04000512 RID: 1298
		R8,
		// Token: 0x04000513 RID: 1299
		ETC_RGB4Crunched,
		// Token: 0x04000514 RID: 1300
		ETC2_RGBA8Crunched,
		// Token: 0x04000515 RID: 1301
		ASTC_HDR_4x4,
		// Token: 0x04000516 RID: 1302
		ASTC_HDR_5x5,
		// Token: 0x04000517 RID: 1303
		ASTC_HDR_6x6,
		// Token: 0x04000518 RID: 1304
		ASTC_HDR_8x8,
		// Token: 0x04000519 RID: 1305
		ASTC_HDR_10x10,
		// Token: 0x0400051A RID: 1306
		ASTC_HDR_12x12,
		// Token: 0x0400051B RID: 1307
		RG32,
		// Token: 0x0400051C RID: 1308
		RGB48,
		// Token: 0x0400051D RID: 1309
		RGBA64,
		// Token: 0x0400051E RID: 1310
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Enum member TextureFormat.ASTC_RGB_4x4 has been deprecated. Use ASTC_4x4 instead (UnityUpgradable) -> ASTC_4x4")]
		ASTC_RGB_4x4 = 48,
		// Token: 0x0400051F RID: 1311
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Enum member TextureFormat.ASTC_RGB_5x5 has been deprecated. Use ASTC_5x5 instead (UnityUpgradable) -> ASTC_5x5")]
		ASTC_RGB_5x5,
		// Token: 0x04000520 RID: 1312
		[Obsolete("Enum member TextureFormat.ASTC_RGB_6x6 has been deprecated. Use ASTC_6x6 instead (UnityUpgradable) -> ASTC_6x6")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		ASTC_RGB_6x6,
		// Token: 0x04000521 RID: 1313
		[Obsolete("Enum member TextureFormat.ASTC_RGB_8x8 has been deprecated. Use ASTC_8x8 instead (UnityUpgradable) -> ASTC_8x8")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		ASTC_RGB_8x8,
		// Token: 0x04000522 RID: 1314
		[Obsolete("Enum member TextureFormat.ASTC_RGB_10x10 has been deprecated. Use ASTC_10x10 instead (UnityUpgradable) -> ASTC_10x10")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		ASTC_RGB_10x10,
		// Token: 0x04000523 RID: 1315
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Enum member TextureFormat.ASTC_RGB_12x12 has been deprecated. Use ASTC_12x12 instead (UnityUpgradable) -> ASTC_12x12")]
		ASTC_RGB_12x12,
		// Token: 0x04000524 RID: 1316
		[Obsolete("Enum member TextureFormat.ASTC_RGBA_4x4 has been deprecated. Use ASTC_4x4 instead (UnityUpgradable) -> ASTC_4x4")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		ASTC_RGBA_4x4,
		// Token: 0x04000525 RID: 1317
		[Obsolete("Enum member TextureFormat.ASTC_RGBA_5x5 has been deprecated. Use ASTC_5x5 instead (UnityUpgradable) -> ASTC_5x5")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		ASTC_RGBA_5x5,
		// Token: 0x04000526 RID: 1318
		[Obsolete("Enum member TextureFormat.ASTC_RGBA_6x6 has been deprecated. Use ASTC_6x6 instead (UnityUpgradable) -> ASTC_6x6")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		ASTC_RGBA_6x6,
		// Token: 0x04000527 RID: 1319
		[Obsolete("Enum member TextureFormat.ASTC_RGBA_8x8 has been deprecated. Use ASTC_8x8 instead (UnityUpgradable) -> ASTC_8x8")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		ASTC_RGBA_8x8,
		// Token: 0x04000528 RID: 1320
		[Obsolete("Enum member TextureFormat.ASTC_RGBA_10x10 has been deprecated. Use ASTC_10x10 instead (UnityUpgradable) -> ASTC_10x10")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		ASTC_RGBA_10x10,
		// Token: 0x04000529 RID: 1321
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Enum member TextureFormat.ASTC_RGBA_12x12 has been deprecated. Use ASTC_12x12 instead (UnityUpgradable) -> ASTC_12x12")]
		ASTC_RGBA_12x12
	}
}
