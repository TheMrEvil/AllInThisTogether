using System;

namespace UnityEngine.TextCore.LowLevel
{
	// Token: 0x02000009 RID: 9
	public enum FontEngineError
	{
		// Token: 0x04000048 RID: 72
		Success,
		// Token: 0x04000049 RID: 73
		Invalid_File_Path,
		// Token: 0x0400004A RID: 74
		Invalid_File_Format,
		// Token: 0x0400004B RID: 75
		Invalid_File_Structure,
		// Token: 0x0400004C RID: 76
		Invalid_File,
		// Token: 0x0400004D RID: 77
		Invalid_Table = 8,
		// Token: 0x0400004E RID: 78
		Invalid_Glyph_Index = 16,
		// Token: 0x0400004F RID: 79
		Invalid_Character_Code,
		// Token: 0x04000050 RID: 80
		Invalid_Pixel_Size = 23,
		// Token: 0x04000051 RID: 81
		Invalid_Library = 33,
		// Token: 0x04000052 RID: 82
		Invalid_Face = 35,
		// Token: 0x04000053 RID: 83
		Invalid_Library_or_Face = 41,
		// Token: 0x04000054 RID: 84
		Atlas_Generation_Cancelled = 100,
		// Token: 0x04000055 RID: 85
		Invalid_SharedTextureData,
		// Token: 0x04000056 RID: 86
		OpenTypeLayoutLookup_Mismatch = 116
	}
}
