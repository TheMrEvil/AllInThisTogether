using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Profiling;

namespace UnityEngine.TextCore.LowLevel
{
	// Token: 0x0200000D RID: 13
	[NativeHeader("Modules/TextCoreFontEngine/Native/FontEngine.h")]
	public sealed class FontEngine
	{
		// Token: 0x06000060 RID: 96 RVA: 0x00002C1D File Offset: 0x00000E1D
		internal FontEngine()
		{
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002C28 File Offset: 0x00000E28
		public static FontEngineError InitializeFontEngine()
		{
			return (FontEngineError)FontEngine.InitializeFontEngine_Internal();
		}

		// Token: 0x06000062 RID: 98
		[NativeMethod(Name = "TextCore::FontEngine::InitFontEngine", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int InitializeFontEngine_Internal();

		// Token: 0x06000063 RID: 99 RVA: 0x00002C40 File Offset: 0x00000E40
		public static FontEngineError DestroyFontEngine()
		{
			return (FontEngineError)FontEngine.DestroyFontEngine_Internal();
		}

		// Token: 0x06000064 RID: 100
		[NativeMethod(Name = "TextCore::FontEngine::DestroyFontEngine", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int DestroyFontEngine_Internal();

		// Token: 0x06000065 RID: 101 RVA: 0x00002C57 File Offset: 0x00000E57
		internal static void SendCancellationRequest()
		{
			FontEngine.SendCancellationRequest_Internal();
		}

		// Token: 0x06000066 RID: 102
		[NativeMethod(Name = "TextCore::FontEngine::SendCancellationRequest", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SendCancellationRequest_Internal();

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000067 RID: 103
		internal static extern bool isProcessingDone { [NativeMethod(Name = "TextCore::FontEngine::GetIsProcessingDone", IsFreeFunction = true)] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000068 RID: 104
		internal static extern float generationProgress { [NativeMethod(Name = "TextCore::FontEngine::GetGenerationProgress", IsFreeFunction = true)] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06000069 RID: 105 RVA: 0x00002C60 File Offset: 0x00000E60
		public static FontEngineError LoadFontFace(string filePath)
		{
			return (FontEngineError)FontEngine.LoadFontFace_Internal(filePath);
		}

		// Token: 0x0600006A RID: 106
		[NativeMethod(Name = "TextCore::FontEngine::LoadFontFace", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int LoadFontFace_Internal(string filePath);

		// Token: 0x0600006B RID: 107 RVA: 0x00002C78 File Offset: 0x00000E78
		public static FontEngineError LoadFontFace(string filePath, int pointSize)
		{
			return (FontEngineError)FontEngine.LoadFontFace_With_Size_Internal(filePath, pointSize);
		}

		// Token: 0x0600006C RID: 108
		[NativeMethod(Name = "TextCore::FontEngine::LoadFontFace", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int LoadFontFace_With_Size_Internal(string filePath, int pointSize);

		// Token: 0x0600006D RID: 109 RVA: 0x00002C94 File Offset: 0x00000E94
		public static FontEngineError LoadFontFace(string filePath, int pointSize, int faceIndex)
		{
			return (FontEngineError)FontEngine.LoadFontFace_With_Size_And_FaceIndex_Internal(filePath, pointSize, faceIndex);
		}

		// Token: 0x0600006E RID: 110
		[NativeMethod(Name = "TextCore::FontEngine::LoadFontFace", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int LoadFontFace_With_Size_And_FaceIndex_Internal(string filePath, int pointSize, int faceIndex);

		// Token: 0x0600006F RID: 111 RVA: 0x00002CB0 File Offset: 0x00000EB0
		public static FontEngineError LoadFontFace(byte[] sourceFontFile)
		{
			bool flag = sourceFontFile.Length == 0;
			FontEngineError result;
			if (flag)
			{
				result = FontEngineError.Invalid_File;
			}
			else
			{
				result = (FontEngineError)FontEngine.LoadFontFace_FromSourceFontFile_Internal(sourceFontFile);
			}
			return result;
		}

		// Token: 0x06000070 RID: 112
		[NativeMethod(Name = "TextCore::FontEngine::LoadFontFace", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int LoadFontFace_FromSourceFontFile_Internal(byte[] sourceFontFile);

		// Token: 0x06000071 RID: 113 RVA: 0x00002CD8 File Offset: 0x00000ED8
		public static FontEngineError LoadFontFace(byte[] sourceFontFile, int pointSize)
		{
			bool flag = sourceFontFile.Length == 0;
			FontEngineError result;
			if (flag)
			{
				result = FontEngineError.Invalid_File;
			}
			else
			{
				result = (FontEngineError)FontEngine.LoadFontFace_With_Size_FromSourceFontFile_Internal(sourceFontFile, pointSize);
			}
			return result;
		}

		// Token: 0x06000072 RID: 114
		[NativeMethod(Name = "TextCore::FontEngine::LoadFontFace", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int LoadFontFace_With_Size_FromSourceFontFile_Internal(byte[] sourceFontFile, int pointSize);

		// Token: 0x06000073 RID: 115 RVA: 0x00002D00 File Offset: 0x00000F00
		public static FontEngineError LoadFontFace(byte[] sourceFontFile, int pointSize, int faceIndex)
		{
			bool flag = sourceFontFile.Length == 0;
			FontEngineError result;
			if (flag)
			{
				result = FontEngineError.Invalid_File;
			}
			else
			{
				result = (FontEngineError)FontEngine.LoadFontFace_With_Size_And_FaceIndex_FromSourceFontFile_Internal(sourceFontFile, pointSize, faceIndex);
			}
			return result;
		}

		// Token: 0x06000074 RID: 116
		[NativeMethod(Name = "TextCore::FontEngine::LoadFontFace", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int LoadFontFace_With_Size_And_FaceIndex_FromSourceFontFile_Internal(byte[] sourceFontFile, int pointSize, int faceIndex);

		// Token: 0x06000075 RID: 117 RVA: 0x00002D28 File Offset: 0x00000F28
		public static FontEngineError LoadFontFace(Font font)
		{
			return (FontEngineError)FontEngine.LoadFontFace_FromFont_Internal(font);
		}

		// Token: 0x06000076 RID: 118
		[NativeMethod(Name = "TextCore::FontEngine::LoadFontFace", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int LoadFontFace_FromFont_Internal(Font font);

		// Token: 0x06000077 RID: 119 RVA: 0x00002D40 File Offset: 0x00000F40
		public static FontEngineError LoadFontFace(Font font, int pointSize)
		{
			return (FontEngineError)FontEngine.LoadFontFace_With_Size_FromFont_Internal(font, pointSize);
		}

		// Token: 0x06000078 RID: 120
		[NativeMethod(Name = "TextCore::FontEngine::LoadFontFace", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int LoadFontFace_With_Size_FromFont_Internal(Font font, int pointSize);

		// Token: 0x06000079 RID: 121 RVA: 0x00002D5C File Offset: 0x00000F5C
		public static FontEngineError LoadFontFace(Font font, int pointSize, int faceIndex)
		{
			return (FontEngineError)FontEngine.LoadFontFace_With_Size_and_FaceIndex_FromFont_Internal(font, pointSize, faceIndex);
		}

		// Token: 0x0600007A RID: 122
		[NativeMethod(Name = "TextCore::FontEngine::LoadFontFace", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int LoadFontFace_With_Size_and_FaceIndex_FromFont_Internal(Font font, int pointSize, int faceIndex);

		// Token: 0x0600007B RID: 123 RVA: 0x00002D78 File Offset: 0x00000F78
		public static FontEngineError LoadFontFace(string familyName, string styleName)
		{
			return (FontEngineError)FontEngine.LoadFontFace_by_FamilyName_and_StyleName_Internal(familyName, styleName);
		}

		// Token: 0x0600007C RID: 124
		[NativeMethod(Name = "TextCore::FontEngine::LoadFontFace", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int LoadFontFace_by_FamilyName_and_StyleName_Internal(string familyName, string styleName);

		// Token: 0x0600007D RID: 125 RVA: 0x00002D94 File Offset: 0x00000F94
		public static FontEngineError LoadFontFace(string familyName, string styleName, int pointSize)
		{
			return (FontEngineError)FontEngine.LoadFontFace_With_Size_by_FamilyName_and_StyleName_Internal(familyName, styleName, pointSize);
		}

		// Token: 0x0600007E RID: 126
		[NativeMethod(Name = "TextCore::FontEngine::LoadFontFace", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int LoadFontFace_With_Size_by_FamilyName_and_StyleName_Internal(string familyName, string styleName, int pointSize);

		// Token: 0x0600007F RID: 127 RVA: 0x00002DB0 File Offset: 0x00000FB0
		public static FontEngineError UnloadFontFace()
		{
			return (FontEngineError)FontEngine.UnloadFontFace_Internal();
		}

		// Token: 0x06000080 RID: 128
		[NativeMethod(Name = "TextCore::FontEngine::UnloadFontFace", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int UnloadFontFace_Internal();

		// Token: 0x06000081 RID: 129 RVA: 0x00002DC8 File Offset: 0x00000FC8
		public static FontEngineError UnloadAllFontFaces()
		{
			return (FontEngineError)FontEngine.UnloadAllFontFaces_Internal();
		}

		// Token: 0x06000082 RID: 130
		[NativeMethod(Name = "TextCore::FontEngine::UnloadAllFontFaces", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int UnloadAllFontFaces_Internal();

		// Token: 0x06000083 RID: 131 RVA: 0x00002DE0 File Offset: 0x00000FE0
		public static string[] GetSystemFontNames()
		{
			string[] systemFontNames_Internal = FontEngine.GetSystemFontNames_Internal();
			bool flag = systemFontNames_Internal != null && systemFontNames_Internal.Length == 0;
			string[] result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = systemFontNames_Internal;
			}
			return result;
		}

		// Token: 0x06000084 RID: 132
		[NativeMethod(Name = "TextCore::FontEngine::GetSystemFontNames", IsThreadSafe = true, IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string[] GetSystemFontNames_Internal();

		// Token: 0x06000085 RID: 133
		[NativeMethod(Name = "TextCore::FontEngine::GetSystemFontReferences", IsThreadSafe = true, IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern FontReference[] GetSystemFontReferences();

		// Token: 0x06000086 RID: 134 RVA: 0x00002E0C File Offset: 0x0000100C
		internal static bool TryGetSystemFontReference(string familyName, string styleName, out FontReference fontRef)
		{
			return FontEngine.TryGetSystemFontReference_Internal(familyName, styleName, out fontRef);
		}

		// Token: 0x06000087 RID: 135
		[NativeMethod(Name = "TextCore::FontEngine::TryGetSystemFontReference", IsThreadSafe = true, IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool TryGetSystemFontReference_Internal(string familyName, string styleName, out FontReference fontRef);

		// Token: 0x06000088 RID: 136 RVA: 0x00002E28 File Offset: 0x00001028
		public static FontEngineError SetFaceSize(int pointSize)
		{
			return (FontEngineError)FontEngine.SetFaceSize_Internal(pointSize);
		}

		// Token: 0x06000089 RID: 137
		[NativeMethod(Name = "TextCore::FontEngine::SetFaceSize", IsThreadSafe = true, IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int SetFaceSize_Internal(int pointSize);

		// Token: 0x0600008A RID: 138 RVA: 0x00002E40 File Offset: 0x00001040
		public static FaceInfo GetFaceInfo()
		{
			FaceInfo result = default(FaceInfo);
			FontEngine.GetFaceInfo_Internal(ref result);
			return result;
		}

		// Token: 0x0600008B RID: 139
		[NativeMethod(Name = "TextCore::FontEngine::GetFaceInfo", IsThreadSafe = true, IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetFaceInfo_Internal(ref FaceInfo faceInfo);

		// Token: 0x0600008C RID: 140
		[NativeMethod(Name = "TextCore::FontEngine::GetFaceCount", IsThreadSafe = true, IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetFaceCount();

		// Token: 0x0600008D RID: 141 RVA: 0x00002E64 File Offset: 0x00001064
		public static string[] GetFontFaces()
		{
			string[] fontFaces_Internal = FontEngine.GetFontFaces_Internal();
			bool flag = fontFaces_Internal != null && fontFaces_Internal.Length == 0;
			string[] result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = fontFaces_Internal;
			}
			return result;
		}

		// Token: 0x0600008E RID: 142
		[NativeMethod(Name = "TextCore::FontEngine::GetFontFaces", IsThreadSafe = true, IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string[] GetFontFaces_Internal();

		// Token: 0x0600008F RID: 143
		[NativeMethod(Name = "TextCore::FontEngine::GetVariantGlyphIndex", IsThreadSafe = true, IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern uint GetVariantGlyphIndex(uint unicode, uint variantSelectorUnicode);

		// Token: 0x06000090 RID: 144
		[NativeMethod(Name = "TextCore::FontEngine::GetGlyphIndex", IsThreadSafe = true, IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern uint GetGlyphIndex(uint unicode);

		// Token: 0x06000091 RID: 145
		[NativeMethod(Name = "TextCore::FontEngine::TryGetGlyphIndex", IsThreadSafe = true, IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool TryGetGlyphIndex(uint unicode, out uint glyphIndex);

		// Token: 0x06000092 RID: 146 RVA: 0x00002E90 File Offset: 0x00001090
		internal static FontEngineError LoadGlyph(uint unicode, GlyphLoadFlags flags)
		{
			return (FontEngineError)FontEngine.LoadGlyph_Internal(unicode, flags);
		}

		// Token: 0x06000093 RID: 147
		[NativeMethod(Name = "TextCore::FontEngine::LoadGlyph", IsThreadSafe = true, IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int LoadGlyph_Internal(uint unicode, GlyphLoadFlags loadFlags);

		// Token: 0x06000094 RID: 148 RVA: 0x00002EAC File Offset: 0x000010AC
		public static bool TryGetGlyphWithUnicodeValue(uint unicode, GlyphLoadFlags flags, out Glyph glyph)
		{
			GlyphMarshallingStruct glyphStruct = default(GlyphMarshallingStruct);
			bool flag = FontEngine.TryGetGlyphWithUnicodeValue_Internal(unicode, flags, ref glyphStruct);
			bool result;
			if (flag)
			{
				glyph = new Glyph(glyphStruct);
				result = true;
			}
			else
			{
				glyph = null;
				result = false;
			}
			return result;
		}

		// Token: 0x06000095 RID: 149
		[NativeMethod(Name = "TextCore::FontEngine::TryGetGlyphWithUnicodeValue", IsThreadSafe = true, IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool TryGetGlyphWithUnicodeValue_Internal(uint unicode, GlyphLoadFlags loadFlags, ref GlyphMarshallingStruct glyphStruct);

		// Token: 0x06000096 RID: 150 RVA: 0x00002EE4 File Offset: 0x000010E4
		public static bool TryGetGlyphWithIndexValue(uint glyphIndex, GlyphLoadFlags flags, out Glyph glyph)
		{
			GlyphMarshallingStruct glyphStruct = default(GlyphMarshallingStruct);
			bool flag = FontEngine.TryGetGlyphWithIndexValue_Internal(glyphIndex, flags, ref glyphStruct);
			bool result;
			if (flag)
			{
				glyph = new Glyph(glyphStruct);
				result = true;
			}
			else
			{
				glyph = null;
				result = false;
			}
			return result;
		}

		// Token: 0x06000097 RID: 151
		[NativeMethod(Name = "TextCore::FontEngine::TryGetGlyphWithIndexValue", IsThreadSafe = true, IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool TryGetGlyphWithIndexValue_Internal(uint glyphIndex, GlyphLoadFlags loadFlags, ref GlyphMarshallingStruct glyphStruct);

		// Token: 0x06000098 RID: 152 RVA: 0x00002F1C File Offset: 0x0000111C
		internal static bool TryPackGlyphInAtlas(Glyph glyph, int padding, GlyphPackingMode packingMode, GlyphRenderMode renderMode, int width, int height, List<GlyphRect> freeGlyphRects, List<GlyphRect> usedGlyphRects)
		{
			GlyphMarshallingStruct glyphMarshallingStruct = new GlyphMarshallingStruct(glyph);
			int count = freeGlyphRects.Count;
			int count2 = usedGlyphRects.Count;
			int num = count + count2;
			bool flag = FontEngine.s_FreeGlyphRects.Length < num || FontEngine.s_UsedGlyphRects.Length < num;
			if (flag)
			{
				int num2 = Mathf.NextPowerOfTwo(num + 1);
				FontEngine.s_FreeGlyphRects = new GlyphRect[num2];
				FontEngine.s_UsedGlyphRects = new GlyphRect[num2];
			}
			int num3 = Mathf.Max(count, count2);
			for (int i = 0; i < num3; i++)
			{
				bool flag2 = i < count;
				if (flag2)
				{
					FontEngine.s_FreeGlyphRects[i] = freeGlyphRects[i];
				}
				bool flag3 = i < count2;
				if (flag3)
				{
					FontEngine.s_UsedGlyphRects[i] = usedGlyphRects[i];
				}
			}
			bool flag4 = FontEngine.TryPackGlyphInAtlas_Internal(ref glyphMarshallingStruct, padding, packingMode, renderMode, width, height, FontEngine.s_FreeGlyphRects, ref count, FontEngine.s_UsedGlyphRects, ref count2);
			bool result;
			if (flag4)
			{
				glyph.glyphRect = glyphMarshallingStruct.glyphRect;
				freeGlyphRects.Clear();
				usedGlyphRects.Clear();
				num3 = Mathf.Max(count, count2);
				for (int j = 0; j < num3; j++)
				{
					bool flag5 = j < count;
					if (flag5)
					{
						freeGlyphRects.Add(FontEngine.s_FreeGlyphRects[j]);
					}
					bool flag6 = j < count2;
					if (flag6)
					{
						usedGlyphRects.Add(FontEngine.s_UsedGlyphRects[j]);
					}
				}
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06000099 RID: 153
		[NativeMethod(Name = "TextCore::FontEngine::TryPackGlyph", IsThreadSafe = true, IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool TryPackGlyphInAtlas_Internal(ref GlyphMarshallingStruct glyph, int padding, GlyphPackingMode packingMode, GlyphRenderMode renderMode, int width, int height, [Out] GlyphRect[] freeGlyphRects, ref int freeGlyphRectCount, [Out] GlyphRect[] usedGlyphRects, ref int usedGlyphRectCount);

		// Token: 0x0600009A RID: 154 RVA: 0x00003098 File Offset: 0x00001298
		internal static bool TryPackGlyphsInAtlas(List<Glyph> glyphsToAdd, List<Glyph> glyphsAdded, int padding, GlyphPackingMode packingMode, GlyphRenderMode renderMode, int width, int height, List<GlyphRect> freeGlyphRects, List<GlyphRect> usedGlyphRects)
		{
			int count = glyphsToAdd.Count;
			int count2 = glyphsAdded.Count;
			int count3 = freeGlyphRects.Count;
			int count4 = usedGlyphRects.Count;
			int num = count + count2 + count3 + count4;
			bool flag = FontEngine.s_GlyphMarshallingStruct_IN.Length < num || FontEngine.s_GlyphMarshallingStruct_OUT.Length < num || FontEngine.s_FreeGlyphRects.Length < num || FontEngine.s_UsedGlyphRects.Length < num;
			if (flag)
			{
				int num2 = Mathf.NextPowerOfTwo(num + 1);
				FontEngine.s_GlyphMarshallingStruct_IN = new GlyphMarshallingStruct[num2];
				FontEngine.s_GlyphMarshallingStruct_OUT = new GlyphMarshallingStruct[num2];
				FontEngine.s_FreeGlyphRects = new GlyphRect[num2];
				FontEngine.s_UsedGlyphRects = new GlyphRect[num2];
			}
			FontEngine.s_GlyphLookupDictionary.Clear();
			for (int i = 0; i < num; i++)
			{
				bool flag2 = i < count;
				if (flag2)
				{
					GlyphMarshallingStruct glyphMarshallingStruct = new GlyphMarshallingStruct(glyphsToAdd[i]);
					FontEngine.s_GlyphMarshallingStruct_IN[i] = glyphMarshallingStruct;
					bool flag3 = !FontEngine.s_GlyphLookupDictionary.ContainsKey(glyphMarshallingStruct.index);
					if (flag3)
					{
						FontEngine.s_GlyphLookupDictionary.Add(glyphMarshallingStruct.index, glyphsToAdd[i]);
					}
				}
				bool flag4 = i < count2;
				if (flag4)
				{
					GlyphMarshallingStruct glyphMarshallingStruct2 = new GlyphMarshallingStruct(glyphsAdded[i]);
					FontEngine.s_GlyphMarshallingStruct_OUT[i] = glyphMarshallingStruct2;
					bool flag5 = !FontEngine.s_GlyphLookupDictionary.ContainsKey(glyphMarshallingStruct2.index);
					if (flag5)
					{
						FontEngine.s_GlyphLookupDictionary.Add(glyphMarshallingStruct2.index, glyphsAdded[i]);
					}
				}
				bool flag6 = i < count3;
				if (flag6)
				{
					FontEngine.s_FreeGlyphRects[i] = freeGlyphRects[i];
				}
				bool flag7 = i < count4;
				if (flag7)
				{
					FontEngine.s_UsedGlyphRects[i] = usedGlyphRects[i];
				}
			}
			bool result = FontEngine.TryPackGlyphsInAtlas_Internal(FontEngine.s_GlyphMarshallingStruct_IN, ref count, FontEngine.s_GlyphMarshallingStruct_OUT, ref count2, padding, packingMode, renderMode, width, height, FontEngine.s_FreeGlyphRects, ref count3, FontEngine.s_UsedGlyphRects, ref count4);
			glyphsToAdd.Clear();
			glyphsAdded.Clear();
			freeGlyphRects.Clear();
			usedGlyphRects.Clear();
			for (int j = 0; j < num; j++)
			{
				bool flag8 = j < count;
				if (flag8)
				{
					GlyphMarshallingStruct glyphMarshallingStruct3 = FontEngine.s_GlyphMarshallingStruct_IN[j];
					Glyph glyph = FontEngine.s_GlyphLookupDictionary[glyphMarshallingStruct3.index];
					glyph.metrics = glyphMarshallingStruct3.metrics;
					glyph.glyphRect = glyphMarshallingStruct3.glyphRect;
					glyph.scale = glyphMarshallingStruct3.scale;
					glyph.atlasIndex = glyphMarshallingStruct3.atlasIndex;
					glyphsToAdd.Add(glyph);
				}
				bool flag9 = j < count2;
				if (flag9)
				{
					GlyphMarshallingStruct glyphMarshallingStruct4 = FontEngine.s_GlyphMarshallingStruct_OUT[j];
					Glyph glyph2 = FontEngine.s_GlyphLookupDictionary[glyphMarshallingStruct4.index];
					glyph2.metrics = glyphMarshallingStruct4.metrics;
					glyph2.glyphRect = glyphMarshallingStruct4.glyphRect;
					glyph2.scale = glyphMarshallingStruct4.scale;
					glyph2.atlasIndex = glyphMarshallingStruct4.atlasIndex;
					glyphsAdded.Add(glyph2);
				}
				bool flag10 = j < count3;
				if (flag10)
				{
					freeGlyphRects.Add(FontEngine.s_FreeGlyphRects[j]);
				}
				bool flag11 = j < count4;
				if (flag11)
				{
					usedGlyphRects.Add(FontEngine.s_UsedGlyphRects[j]);
				}
			}
			return result;
		}

		// Token: 0x0600009B RID: 155
		[NativeMethod(Name = "TextCore::FontEngine::TryPackGlyphs", IsThreadSafe = true, IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool TryPackGlyphsInAtlas_Internal([Out] GlyphMarshallingStruct[] glyphsToAdd, ref int glyphsToAddCount, [Out] GlyphMarshallingStruct[] glyphsAdded, ref int glyphsAddedCount, int padding, GlyphPackingMode packingMode, GlyphRenderMode renderMode, int width, int height, [Out] GlyphRect[] freeGlyphRects, ref int freeGlyphRectCount, [Out] GlyphRect[] usedGlyphRects, ref int usedGlyphRectCount);

		// Token: 0x0600009C RID: 156 RVA: 0x00003400 File Offset: 0x00001600
		internal static FontEngineError RenderGlyphToTexture(Glyph glyph, int padding, GlyphRenderMode renderMode, Texture2D texture)
		{
			GlyphMarshallingStruct glyphStruct = new GlyphMarshallingStruct(glyph);
			return (FontEngineError)FontEngine.RenderGlyphToTexture_Internal(glyphStruct, padding, renderMode, texture);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00003423 File Offset: 0x00001623
		[NativeMethod(Name = "TextCore::FontEngine::RenderGlyphToTexture", IsFreeFunction = true)]
		private static int RenderGlyphToTexture_Internal(GlyphMarshallingStruct glyphStruct, int padding, GlyphRenderMode renderMode, Texture2D texture)
		{
			return FontEngine.RenderGlyphToTexture_Internal_Injected(ref glyphStruct, padding, renderMode, texture);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00003430 File Offset: 0x00001630
		internal static FontEngineError RenderGlyphsToTexture(List<Glyph> glyphs, int padding, GlyphRenderMode renderMode, Texture2D texture)
		{
			int count = glyphs.Count;
			bool flag = FontEngine.s_GlyphMarshallingStruct_IN.Length < count;
			if (flag)
			{
				int num = Mathf.NextPowerOfTwo(count + 1);
				FontEngine.s_GlyphMarshallingStruct_IN = new GlyphMarshallingStruct[num];
			}
			for (int i = 0; i < count; i++)
			{
				FontEngine.s_GlyphMarshallingStruct_IN[i] = new GlyphMarshallingStruct(glyphs[i]);
			}
			return (FontEngineError)FontEngine.RenderGlyphsToTexture_Internal(FontEngine.s_GlyphMarshallingStruct_IN, count, padding, renderMode, texture);
		}

		// Token: 0x0600009F RID: 159
		[NativeMethod(Name = "TextCore::FontEngine::RenderGlyphsToTexture", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int RenderGlyphsToTexture_Internal(GlyphMarshallingStruct[] glyphs, int glyphCount, int padding, GlyphRenderMode renderMode, Texture2D texture);

		// Token: 0x060000A0 RID: 160 RVA: 0x000034B0 File Offset: 0x000016B0
		internal static FontEngineError RenderGlyphsToTexture(List<Glyph> glyphs, int padding, GlyphRenderMode renderMode, byte[] texBuffer, int texWidth, int texHeight)
		{
			int count = glyphs.Count;
			bool flag = FontEngine.s_GlyphMarshallingStruct_IN.Length < count;
			if (flag)
			{
				int num = Mathf.NextPowerOfTwo(count + 1);
				FontEngine.s_GlyphMarshallingStruct_IN = new GlyphMarshallingStruct[num];
			}
			for (int i = 0; i < count; i++)
			{
				FontEngine.s_GlyphMarshallingStruct_IN[i] = new GlyphMarshallingStruct(glyphs[i]);
			}
			return (FontEngineError)FontEngine.RenderGlyphsToTextureBuffer_Internal(FontEngine.s_GlyphMarshallingStruct_IN, count, padding, renderMode, texBuffer, texWidth, texHeight);
		}

		// Token: 0x060000A1 RID: 161
		[NativeMethod(Name = "TextCore::FontEngine::RenderGlyphsToTextureBuffer", IsThreadSafe = true, IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int RenderGlyphsToTextureBuffer_Internal(GlyphMarshallingStruct[] glyphs, int glyphCount, int padding, GlyphRenderMode renderMode, [Out] byte[] texBuffer, int texWidth, int texHeight);

		// Token: 0x060000A2 RID: 162 RVA: 0x00003534 File Offset: 0x00001734
		internal static FontEngineError RenderGlyphsToSharedTexture(List<Glyph> glyphs, int padding, GlyphRenderMode renderMode)
		{
			int count = glyphs.Count;
			bool flag = FontEngine.s_GlyphMarshallingStruct_IN.Length < count;
			if (flag)
			{
				int num = Mathf.NextPowerOfTwo(count + 1);
				FontEngine.s_GlyphMarshallingStruct_IN = new GlyphMarshallingStruct[num];
			}
			for (int i = 0; i < count; i++)
			{
				FontEngine.s_GlyphMarshallingStruct_IN[i] = new GlyphMarshallingStruct(glyphs[i]);
			}
			return (FontEngineError)FontEngine.RenderGlyphsToSharedTexture_Internal(FontEngine.s_GlyphMarshallingStruct_IN, count, padding, renderMode);
		}

		// Token: 0x060000A3 RID: 163
		[NativeMethod(Name = "TextCore::FontEngine::RenderGlyphsToSharedTexture", IsThreadSafe = true, IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int RenderGlyphsToSharedTexture_Internal(GlyphMarshallingStruct[] glyphs, int glyphCount, int padding, GlyphRenderMode renderMode);

		// Token: 0x060000A4 RID: 164
		[NativeMethod(Name = "TextCore::FontEngine::SetSharedTextureData", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void SetSharedTexture(Texture2D texture);

		// Token: 0x060000A5 RID: 165
		[NativeMethod(Name = "TextCore::FontEngine::ReleaseSharedTextureData", IsThreadSafe = true, IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ReleaseSharedTexture();

		// Token: 0x060000A6 RID: 166
		[NativeMethod(Name = "TextCore::FontEngine::SetTextureUploadMode", IsThreadSafe = true, IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void SetTextureUploadMode(bool shouldUploadImmediately);

		// Token: 0x060000A7 RID: 167 RVA: 0x000035B4 File Offset: 0x000017B4
		internal static bool TryAddGlyphToTexture(uint glyphIndex, int padding, GlyphPackingMode packingMode, List<GlyphRect> freeGlyphRects, List<GlyphRect> usedGlyphRects, GlyphRenderMode renderMode, Texture2D texture, out Glyph glyph)
		{
			int count = freeGlyphRects.Count;
			int count2 = usedGlyphRects.Count;
			int num = count + count2;
			bool flag = FontEngine.s_FreeGlyphRects.Length < num || FontEngine.s_UsedGlyphRects.Length < num;
			if (flag)
			{
				int num2 = Mathf.NextPowerOfTwo(num + 1);
				FontEngine.s_FreeGlyphRects = new GlyphRect[num2];
				FontEngine.s_UsedGlyphRects = new GlyphRect[num2];
			}
			int num3 = Mathf.Max(count, count2);
			for (int i = 0; i < num3; i++)
			{
				bool flag2 = i < count;
				if (flag2)
				{
					FontEngine.s_FreeGlyphRects[i] = freeGlyphRects[i];
				}
				bool flag3 = i < count2;
				if (flag3)
				{
					FontEngine.s_UsedGlyphRects[i] = usedGlyphRects[i];
				}
			}
			GlyphMarshallingStruct glyphStruct;
			bool flag4 = FontEngine.TryAddGlyphToTexture_Internal(glyphIndex, padding, packingMode, FontEngine.s_FreeGlyphRects, ref count, FontEngine.s_UsedGlyphRects, ref count2, renderMode, texture, out glyphStruct);
			bool result;
			if (flag4)
			{
				glyph = new Glyph(glyphStruct);
				freeGlyphRects.Clear();
				usedGlyphRects.Clear();
				num3 = Mathf.Max(count, count2);
				for (int j = 0; j < num3; j++)
				{
					bool flag5 = j < count;
					if (flag5)
					{
						freeGlyphRects.Add(FontEngine.s_FreeGlyphRects[j]);
					}
					bool flag6 = j < count2;
					if (flag6)
					{
						usedGlyphRects.Add(FontEngine.s_UsedGlyphRects[j]);
					}
				}
				result = true;
			}
			else
			{
				glyph = null;
				result = false;
			}
			return result;
		}

		// Token: 0x060000A8 RID: 168
		[NativeMethod(Name = "TextCore::FontEngine::TryAddGlyphToTexture", IsThreadSafe = true, IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool TryAddGlyphToTexture_Internal(uint glyphIndex, int padding, GlyphPackingMode packingMode, [Out] GlyphRect[] freeGlyphRects, ref int freeGlyphRectCount, [Out] GlyphRect[] usedGlyphRects, ref int usedGlyphRectCount, GlyphRenderMode renderMode, Texture2D texture, out GlyphMarshallingStruct glyph);

		// Token: 0x060000A9 RID: 169 RVA: 0x00003720 File Offset: 0x00001920
		internal static bool TryAddGlyphsToTexture(List<Glyph> glyphsToAdd, List<Glyph> glyphsAdded, int padding, GlyphPackingMode packingMode, List<GlyphRect> freeGlyphRects, List<GlyphRect> usedGlyphRects, GlyphRenderMode renderMode, Texture2D texture)
		{
			Profiler.BeginSample("FontEngine.TryAddGlyphsToTexture");
			int count = glyphsToAdd.Count;
			int num = 0;
			bool flag = FontEngine.s_GlyphMarshallingStruct_IN.Length < count || FontEngine.s_GlyphMarshallingStruct_OUT.Length < count;
			if (flag)
			{
				int newSize = Mathf.NextPowerOfTwo(count + 1);
				bool flag2 = FontEngine.s_GlyphMarshallingStruct_IN.Length < count;
				if (flag2)
				{
					Array.Resize<GlyphMarshallingStruct>(ref FontEngine.s_GlyphMarshallingStruct_IN, newSize);
				}
				bool flag3 = FontEngine.s_GlyphMarshallingStruct_OUT.Length < count;
				if (flag3)
				{
					Array.Resize<GlyphMarshallingStruct>(ref FontEngine.s_GlyphMarshallingStruct_OUT, newSize);
				}
			}
			int count2 = freeGlyphRects.Count;
			int count3 = usedGlyphRects.Count;
			int num2 = count2 + count3 + count;
			bool flag4 = FontEngine.s_FreeGlyphRects.Length < num2 || FontEngine.s_UsedGlyphRects.Length < num2;
			if (flag4)
			{
				int newSize2 = Mathf.NextPowerOfTwo(num2 + 1);
				bool flag5 = FontEngine.s_FreeGlyphRects.Length < num2;
				if (flag5)
				{
					Array.Resize<GlyphRect>(ref FontEngine.s_FreeGlyphRects, newSize2);
				}
				bool flag6 = FontEngine.s_UsedGlyphRects.Length < num2;
				if (flag6)
				{
					Array.Resize<GlyphRect>(ref FontEngine.s_UsedGlyphRects, newSize2);
				}
			}
			FontEngine.s_GlyphLookupDictionary.Clear();
			int num3 = 0;
			bool flag7 = true;
			while (flag7)
			{
				flag7 = false;
				bool flag8 = num3 < count;
				if (flag8)
				{
					Glyph glyph = glyphsToAdd[num3];
					FontEngine.s_GlyphMarshallingStruct_IN[num3] = new GlyphMarshallingStruct(glyph);
					FontEngine.s_GlyphLookupDictionary.Add(glyph.index, glyph);
					flag7 = true;
				}
				bool flag9 = num3 < count2;
				if (flag9)
				{
					FontEngine.s_FreeGlyphRects[num3] = freeGlyphRects[num3];
					flag7 = true;
				}
				bool flag10 = num3 < count3;
				if (flag10)
				{
					FontEngine.s_UsedGlyphRects[num3] = usedGlyphRects[num3];
					flag7 = true;
				}
				num3++;
			}
			bool result = FontEngine.TryAddGlyphsToTexture_Internal_MultiThread(FontEngine.s_GlyphMarshallingStruct_IN, ref count, FontEngine.s_GlyphMarshallingStruct_OUT, ref num, padding, packingMode, FontEngine.s_FreeGlyphRects, ref count2, FontEngine.s_UsedGlyphRects, ref count3, renderMode, texture);
			glyphsToAdd.Clear();
			glyphsAdded.Clear();
			freeGlyphRects.Clear();
			usedGlyphRects.Clear();
			num3 = 0;
			flag7 = true;
			while (flag7)
			{
				flag7 = false;
				bool flag11 = num3 < count;
				if (flag11)
				{
					uint index = FontEngine.s_GlyphMarshallingStruct_IN[num3].index;
					glyphsToAdd.Add(FontEngine.s_GlyphLookupDictionary[index]);
					flag7 = true;
				}
				bool flag12 = num3 < num;
				if (flag12)
				{
					uint index2 = FontEngine.s_GlyphMarshallingStruct_OUT[num3].index;
					Glyph glyph2 = FontEngine.s_GlyphLookupDictionary[index2];
					glyph2.atlasIndex = FontEngine.s_GlyphMarshallingStruct_OUT[num3].atlasIndex;
					glyph2.scale = FontEngine.s_GlyphMarshallingStruct_OUT[num3].scale;
					glyph2.glyphRect = FontEngine.s_GlyphMarshallingStruct_OUT[num3].glyphRect;
					glyph2.metrics = FontEngine.s_GlyphMarshallingStruct_OUT[num3].metrics;
					glyphsAdded.Add(glyph2);
					flag7 = true;
				}
				bool flag13 = num3 < count2;
				if (flag13)
				{
					freeGlyphRects.Add(FontEngine.s_FreeGlyphRects[num3]);
					flag7 = true;
				}
				bool flag14 = num3 < count3;
				if (flag14)
				{
					usedGlyphRects.Add(FontEngine.s_UsedGlyphRects[num3]);
					flag7 = true;
				}
				num3++;
			}
			Profiler.EndSample();
			return result;
		}

		// Token: 0x060000AA RID: 170
		[NativeMethod(Name = "TextCore::FontEngine::TryAddGlyphsToTexture", IsThreadSafe = true, IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool TryAddGlyphsToTexture_Internal_MultiThread([Out] GlyphMarshallingStruct[] glyphsToAdd, ref int glyphsToAddCount, [Out] GlyphMarshallingStruct[] glyphsAdded, ref int glyphsAddedCount, int padding, GlyphPackingMode packingMode, [Out] GlyphRect[] freeGlyphRects, ref int freeGlyphRectCount, [Out] GlyphRect[] usedGlyphRects, ref int usedGlyphRectCount, GlyphRenderMode renderMode, Texture2D texture);

		// Token: 0x060000AB RID: 171 RVA: 0x00003A54 File Offset: 0x00001C54
		internal static bool TryAddGlyphsToTexture(List<uint> glyphIndexes, int padding, GlyphPackingMode packingMode, List<GlyphRect> freeGlyphRects, List<GlyphRect> usedGlyphRects, GlyphRenderMode renderMode, Texture2D texture, out Glyph[] glyphs)
		{
			Profiler.BeginSample("FontEngine.TryAddGlyphsToTexture");
			glyphs = null;
			bool flag = glyphIndexes == null || glyphIndexes.Count == 0;
			bool result;
			if (flag)
			{
				Profiler.EndSample();
				result = false;
			}
			else
			{
				int count = glyphIndexes.Count;
				bool flag2 = FontEngine.s_GlyphIndexes_MarshallingArray_A == null || FontEngine.s_GlyphIndexes_MarshallingArray_A.Length < count;
				if (flag2)
				{
					FontEngine.s_GlyphIndexes_MarshallingArray_A = new uint[Mathf.NextPowerOfTwo(count + 1)];
				}
				int count2 = freeGlyphRects.Count;
				int count3 = usedGlyphRects.Count;
				int num = count2 + count3 + count;
				bool flag3 = FontEngine.s_FreeGlyphRects.Length < num || FontEngine.s_UsedGlyphRects.Length < num;
				if (flag3)
				{
					int num2 = Mathf.NextPowerOfTwo(num + 1);
					FontEngine.s_FreeGlyphRects = new GlyphRect[num2];
					FontEngine.s_UsedGlyphRects = new GlyphRect[num2];
				}
				bool flag4 = FontEngine.s_GlyphMarshallingStruct_OUT.Length < count;
				if (flag4)
				{
					int num3 = Mathf.NextPowerOfTwo(count + 1);
					FontEngine.s_GlyphMarshallingStruct_OUT = new GlyphMarshallingStruct[num3];
				}
				int num4 = FontEngineUtilities.MaxValue(count2, count3, count);
				for (int i = 0; i < num4; i++)
				{
					bool flag5 = i < count;
					if (flag5)
					{
						FontEngine.s_GlyphIndexes_MarshallingArray_A[i] = glyphIndexes[i];
					}
					bool flag6 = i < count2;
					if (flag6)
					{
						FontEngine.s_FreeGlyphRects[i] = freeGlyphRects[i];
					}
					bool flag7 = i < count3;
					if (flag7)
					{
						FontEngine.s_UsedGlyphRects[i] = usedGlyphRects[i];
					}
				}
				bool flag8 = FontEngine.TryAddGlyphsToTexture_Internal(FontEngine.s_GlyphIndexes_MarshallingArray_A, padding, packingMode, FontEngine.s_FreeGlyphRects, ref count2, FontEngine.s_UsedGlyphRects, ref count3, renderMode, texture, FontEngine.s_GlyphMarshallingStruct_OUT, ref count);
				bool flag9 = FontEngine.s_Glyphs == null || FontEngine.s_Glyphs.Length <= count;
				if (flag9)
				{
					FontEngine.s_Glyphs = new Glyph[Mathf.NextPowerOfTwo(count + 1)];
				}
				FontEngine.s_Glyphs[count] = null;
				freeGlyphRects.Clear();
				usedGlyphRects.Clear();
				num4 = FontEngineUtilities.MaxValue(count2, count3, count);
				for (int j = 0; j < num4; j++)
				{
					bool flag10 = j < count;
					if (flag10)
					{
						FontEngine.s_Glyphs[j] = new Glyph(FontEngine.s_GlyphMarshallingStruct_OUT[j]);
					}
					bool flag11 = j < count2;
					if (flag11)
					{
						freeGlyphRects.Add(FontEngine.s_FreeGlyphRects[j]);
					}
					bool flag12 = j < count3;
					if (flag12)
					{
						usedGlyphRects.Add(FontEngine.s_UsedGlyphRects[j]);
					}
				}
				glyphs = FontEngine.s_Glyphs;
				Profiler.EndSample();
				result = flag8;
			}
			return result;
		}

		// Token: 0x060000AC RID: 172
		[NativeMethod(Name = "TextCore::FontEngine::TryAddGlyphsToTexture", IsThreadSafe = true, IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool TryAddGlyphsToTexture_Internal(uint[] glyphIndex, int padding, GlyphPackingMode packingMode, [Out] GlyphRect[] freeGlyphRects, ref int freeGlyphRectCount, [Out] GlyphRect[] usedGlyphRects, ref int usedGlyphRectCount, GlyphRenderMode renderMode, Texture2D texture, [Out] GlyphMarshallingStruct[] glyphs, ref int glyphCount);

		// Token: 0x060000AD RID: 173 RVA: 0x00003CCC File Offset: 0x00001ECC
		[NativeMethod(Name = "TextCore::FontEngine::GetOpenTypeLayoutTable", IsFreeFunction = true)]
		internal static OTL_Table GetOpenTypeLayoutTable(OTL_TableType type)
		{
			OTL_Table result;
			FontEngine.GetOpenTypeLayoutTable_Injected(type, out result);
			return result;
		}

		// Token: 0x060000AE RID: 174
		[NativeMethod(Name = "TextCore::FontEngine::GetOpenTypeLayoutScripts", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern OTL_Script[] GetOpenTypeLayoutScripts();

		// Token: 0x060000AF RID: 175
		[NativeMethod(Name = "TextCore::FontEngine::GetOpenTypeLayoutFeatures", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern OTL_Feature[] GetOpenTypeLayoutFeatures();

		// Token: 0x060000B0 RID: 176
		[NativeMethod(Name = "TextCore::FontEngine::GetOpenTypeLayoutLookups", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern OTL_Lookup[] GetOpenTypeLayoutLookups();

		// Token: 0x060000B1 RID: 177 RVA: 0x00003CE2 File Offset: 0x00001EE2
		internal static OpenTypeFeature[] GetOpenTypeFontFeatureList()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000B2 RID: 178
		[NativeMethod(Name = "TextCore::FontEngine::GetAllSingleSubstitutionRecords", IsThreadSafe = true, IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern SingleSubstitutionRecord[] GetAllSingleSubstitutionRecords();

		// Token: 0x060000B3 RID: 179 RVA: 0x00003CEC File Offset: 0x00001EEC
		internal static SingleSubstitutionRecord[] GetSingleSubstitutionRecords(int lookupIndex, uint glyphIndex)
		{
			FontEngine.GlyphIndexToMarshallingArray(glyphIndex, ref FontEngine.s_GlyphIndexes_MarshallingArray_A);
			return FontEngine.GetSingleSubstitutionRecords(lookupIndex, FontEngine.s_GlyphIndexes_MarshallingArray_A);
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00003D18 File Offset: 0x00001F18
		internal static SingleSubstitutionRecord[] GetSingleSubstitutionRecords(int lookupIndex, List<uint> glyphIndexes)
		{
			FontEngine.GenericListToMarshallingArray<uint>(ref glyphIndexes, ref FontEngine.s_GlyphIndexes_MarshallingArray_A);
			return FontEngine.GetSingleSubstitutionRecords(lookupIndex, FontEngine.s_GlyphIndexes_MarshallingArray_A);
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00003D44 File Offset: 0x00001F44
		private static SingleSubstitutionRecord[] GetSingleSubstitutionRecords(int lookupIndex, uint[] glyphIndexes)
		{
			int num;
			FontEngine.PopulateSingleSubstitutionRecordMarshallingArray_from_GlyphIndexes(glyphIndexes, lookupIndex, out num);
			bool flag = num == 0;
			SingleSubstitutionRecord[] result;
			if (flag)
			{
				result = null;
			}
			else
			{
				FontEngine.SetMarshallingArraySize<SingleSubstitutionRecord>(ref FontEngine.s_SingleSubstitutionRecords_MarshallingArray, num);
				FontEngine.GetSingleSubstitutionRecordsFromMarshallingArray(FontEngine.s_SingleSubstitutionRecords_MarshallingArray);
				FontEngine.s_SingleSubstitutionRecords_MarshallingArray[num] = default(SingleSubstitutionRecord);
				result = FontEngine.s_SingleSubstitutionRecords_MarshallingArray;
			}
			return result;
		}

		// Token: 0x060000B6 RID: 182
		[NativeMethod(Name = "TextCore::FontEngine::PopulateSingleSubstitutionRecordMarshallingArray", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int PopulateSingleSubstitutionRecordMarshallingArray_from_GlyphIndexes(uint[] glyphIndexes, int lookupIndex, out int recordCount);

		// Token: 0x060000B7 RID: 183
		[NativeMethod(Name = "TextCore::FontEngine::GetSingleSubstitutionRecordsFromMarshallingArray", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetSingleSubstitutionRecordsFromMarshallingArray([Out] SingleSubstitutionRecord[] singleSubstitutionRecords);

		// Token: 0x060000B8 RID: 184
		[NativeMethod(Name = "TextCore::FontEngine::GetAllMultipleSubstitutionRecords", IsThreadSafe = true, IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern MultipleSubstitutionRecord[] GetAllMultipleSubstitutionRecords();

		// Token: 0x060000B9 RID: 185 RVA: 0x00003D9C File Offset: 0x00001F9C
		internal static MultipleSubstitutionRecord[] GetMultipleSubstitutionRecords(int lookupIndex, uint glyphIndex)
		{
			FontEngine.GlyphIndexToMarshallingArray(glyphIndex, ref FontEngine.s_GlyphIndexes_MarshallingArray_A);
			return FontEngine.GetMultipleSubstitutionRecords(lookupIndex, FontEngine.s_GlyphIndexes_MarshallingArray_A);
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00003DC8 File Offset: 0x00001FC8
		internal static MultipleSubstitutionRecord[] GetMultipleSubstitutionRecords(int lookupIndex, List<uint> glyphIndexes)
		{
			FontEngine.GenericListToMarshallingArray<uint>(ref glyphIndexes, ref FontEngine.s_GlyphIndexes_MarshallingArray_A);
			return FontEngine.GetMultipleSubstitutionRecords(lookupIndex, FontEngine.s_GlyphIndexes_MarshallingArray_A);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00003DF4 File Offset: 0x00001FF4
		private static MultipleSubstitutionRecord[] GetMultipleSubstitutionRecords(int lookupIndex, uint[] glyphIndexes)
		{
			int num;
			FontEngine.PopulateMultipleSubstitutionRecordMarshallingArray_from_GlyphIndexes(glyphIndexes, lookupIndex, out num);
			bool flag = num == 0;
			MultipleSubstitutionRecord[] result;
			if (flag)
			{
				result = null;
			}
			else
			{
				FontEngine.SetMarshallingArraySize<MultipleSubstitutionRecord>(ref FontEngine.s_MultipleSubstitutionRecords_MarshallingArray, num);
				FontEngine.GetMultipleSubstitutionRecordsFromMarshallingArray(FontEngine.s_MultipleSubstitutionRecords_MarshallingArray);
				FontEngine.s_MultipleSubstitutionRecords_MarshallingArray[num] = default(MultipleSubstitutionRecord);
				result = FontEngine.s_MultipleSubstitutionRecords_MarshallingArray;
			}
			return result;
		}

		// Token: 0x060000BC RID: 188
		[NativeMethod(Name = "TextCore::FontEngine::PopulateMultipleSubstitutionRecordMarshallingArray", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int PopulateMultipleSubstitutionRecordMarshallingArray_from_GlyphIndexes(uint[] glyphIndexes, int lookupIndex, out int recordCount);

		// Token: 0x060000BD RID: 189
		[NativeMethod(Name = "TextCore::FontEngine::GetMultipleSubstitutionRecordsFromMarshallingArray", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetMultipleSubstitutionRecordsFromMarshallingArray([Out] MultipleSubstitutionRecord[] substitutionRecords);

		// Token: 0x060000BE RID: 190
		[NativeMethod(Name = "TextCore::FontEngine::GetAllAlternateSubstitutionRecords", IsThreadSafe = true, IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern AlternateSubstitutionRecord[] GetAllAlternateSubstitutionRecords();

		// Token: 0x060000BF RID: 191 RVA: 0x00003E4C File Offset: 0x0000204C
		internal static AlternateSubstitutionRecord[] GetAlternateSubstitutionRecords(int lookupIndex, uint glyphIndex)
		{
			FontEngine.GlyphIndexToMarshallingArray(glyphIndex, ref FontEngine.s_GlyphIndexes_MarshallingArray_A);
			return FontEngine.GetAlternateSubstitutionRecords(lookupIndex, FontEngine.s_GlyphIndexes_MarshallingArray_A);
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00003E78 File Offset: 0x00002078
		internal static AlternateSubstitutionRecord[] GetAlternateSubstitutionRecords(int lookupIndex, List<uint> glyphIndexes)
		{
			FontEngine.GenericListToMarshallingArray<uint>(ref glyphIndexes, ref FontEngine.s_GlyphIndexes_MarshallingArray_A);
			return FontEngine.GetAlternateSubstitutionRecords(lookupIndex, FontEngine.s_GlyphIndexes_MarshallingArray_A);
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00003EA4 File Offset: 0x000020A4
		private static AlternateSubstitutionRecord[] GetAlternateSubstitutionRecords(int lookupIndex, uint[] glyphIndexes)
		{
			int num;
			FontEngine.PopulateAlternateSubstitutionRecordMarshallingArray_from_GlyphIndexes(glyphIndexes, lookupIndex, out num);
			bool flag = num == 0;
			AlternateSubstitutionRecord[] result;
			if (flag)
			{
				result = null;
			}
			else
			{
				FontEngine.SetMarshallingArraySize<AlternateSubstitutionRecord>(ref FontEngine.s_AlternateSubstitutionRecords_MarshallingArray, num);
				FontEngine.GetAlternateSubstitutionRecordsFromMarshallingArray(FontEngine.s_AlternateSubstitutionRecords_MarshallingArray);
				FontEngine.s_AlternateSubstitutionRecords_MarshallingArray[num] = default(AlternateSubstitutionRecord);
				result = FontEngine.s_AlternateSubstitutionRecords_MarshallingArray;
			}
			return result;
		}

		// Token: 0x060000C2 RID: 194
		[NativeMethod(Name = "TextCore::FontEngine::PopulateAlternateSubstitutionRecordMarshallingArray", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int PopulateAlternateSubstitutionRecordMarshallingArray_from_GlyphIndexes(uint[] glyphIndexes, int lookupIndex, out int recordCount);

		// Token: 0x060000C3 RID: 195
		[NativeMethod(Name = "TextCore::FontEngine::GetAlternateSubstitutionRecordsFromMarshallingArray", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetAlternateSubstitutionRecordsFromMarshallingArray([Out] AlternateSubstitutionRecord[] singleSubstitutionRecords);

		// Token: 0x060000C4 RID: 196
		[NativeMethod(Name = "TextCore::FontEngine::GetAllLigatureSubstitutionRecords", IsThreadSafe = true, IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern LigatureSubstitutionRecord[] GetAllLigatureSubstitutionRecords();

		// Token: 0x060000C5 RID: 197 RVA: 0x00003EFC File Offset: 0x000020FC
		internal static LigatureSubstitutionRecord[] GetLigatureSubstitutionRecords(uint glyphIndex)
		{
			FontEngine.GlyphIndexToMarshallingArray(glyphIndex, ref FontEngine.s_GlyphIndexes_MarshallingArray_A);
			return FontEngine.GetLigatureSubstitutionRecords(FontEngine.s_GlyphIndexes_MarshallingArray_A);
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00003F24 File Offset: 0x00002124
		internal static LigatureSubstitutionRecord[] GetLigatureSubstitutionRecords(List<uint> glyphIndexes)
		{
			FontEngine.GenericListToMarshallingArray<uint>(ref glyphIndexes, ref FontEngine.s_GlyphIndexes_MarshallingArray_A);
			return FontEngine.GetLigatureSubstitutionRecords(FontEngine.s_GlyphIndexes_MarshallingArray_A);
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00003F50 File Offset: 0x00002150
		internal static LigatureSubstitutionRecord[] GetLigatureSubstitutionRecords(int lookupIndex, uint glyphIndex)
		{
			FontEngine.GlyphIndexToMarshallingArray(glyphIndex, ref FontEngine.s_GlyphIndexes_MarshallingArray_A);
			return FontEngine.GetLigatureSubstitutionRecords(lookupIndex, FontEngine.s_GlyphIndexes_MarshallingArray_A);
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00003F7C File Offset: 0x0000217C
		internal static LigatureSubstitutionRecord[] GetLigatureSubstitutionRecords(int lookupIndex, List<uint> glyphIndexes)
		{
			FontEngine.GenericListToMarshallingArray<uint>(ref glyphIndexes, ref FontEngine.s_GlyphIndexes_MarshallingArray_A);
			return FontEngine.GetLigatureSubstitutionRecords(lookupIndex, FontEngine.s_GlyphIndexes_MarshallingArray_A);
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00003FA8 File Offset: 0x000021A8
		private static LigatureSubstitutionRecord[] GetLigatureSubstitutionRecords(uint[] glyphIndexes)
		{
			int num;
			FontEngine.PopulateLigatureSubstitutionRecordMarshallingArray(glyphIndexes, out num);
			bool flag = num == 0;
			LigatureSubstitutionRecord[] result;
			if (flag)
			{
				result = null;
			}
			else
			{
				FontEngine.SetMarshallingArraySize<LigatureSubstitutionRecord>(ref FontEngine.s_LigatureSubstitutionRecords_MarshallingArray, num);
				FontEngine.GetLigatureSubstitutionRecordsFromMarshallingArray(FontEngine.s_LigatureSubstitutionRecords_MarshallingArray);
				FontEngine.s_LigatureSubstitutionRecords_MarshallingArray[num] = default(LigatureSubstitutionRecord);
				result = FontEngine.s_LigatureSubstitutionRecords_MarshallingArray;
			}
			return result;
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00003FFC File Offset: 0x000021FC
		private static LigatureSubstitutionRecord[] GetLigatureSubstitutionRecords(int lookupIndex, uint[] glyphIndexes)
		{
			int num;
			FontEngine.PopulateLigatureSubstitutionRecordMarshallingArray_for_LookupIndex(glyphIndexes, lookupIndex, out num);
			bool flag = num == 0;
			LigatureSubstitutionRecord[] result;
			if (flag)
			{
				result = null;
			}
			else
			{
				FontEngine.SetMarshallingArraySize<LigatureSubstitutionRecord>(ref FontEngine.s_LigatureSubstitutionRecords_MarshallingArray, num);
				FontEngine.GetLigatureSubstitutionRecordsFromMarshallingArray(FontEngine.s_LigatureSubstitutionRecords_MarshallingArray);
				FontEngine.s_LigatureSubstitutionRecords_MarshallingArray[num] = default(LigatureSubstitutionRecord);
				result = FontEngine.s_LigatureSubstitutionRecords_MarshallingArray;
			}
			return result;
		}

		// Token: 0x060000CB RID: 203
		[NativeMethod(Name = "TextCore::FontEngine::PopulateLigatureSubstitutionRecordMarshallingArray", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int PopulateLigatureSubstitutionRecordMarshallingArray(uint[] glyphIndexes, out int recordCount);

		// Token: 0x060000CC RID: 204
		[NativeMethod(Name = "TextCore::FontEngine::PopulateLigatureSubstitutionRecordMarshallingArray", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int PopulateLigatureSubstitutionRecordMarshallingArray_for_LookupIndex(uint[] glyphIndexes, int lookupIndex, out int recordCount);

		// Token: 0x060000CD RID: 205
		[NativeMethod(Name = "TextCore::FontEngine::GetLigatureSubstitutionRecordsFromMarshallingArray", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetLigatureSubstitutionRecordsFromMarshallingArray([Out] LigatureSubstitutionRecord[] ligatureSubstitutionRecords);

		// Token: 0x060000CE RID: 206
		[NativeMethod(Name = "TextCore::FontEngine::GetAllContextualSubstitutionRecords", IsThreadSafe = true, IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern ContextualSubstitutionRecord[] GetAllContextualSubstitutionRecords();

		// Token: 0x060000CF RID: 207 RVA: 0x00004054 File Offset: 0x00002254
		internal static ContextualSubstitutionRecord[] GetContextualSubstitutionRecords(int lookupIndex, uint glyphIndex)
		{
			FontEngine.GlyphIndexToMarshallingArray(glyphIndex, ref FontEngine.s_GlyphIndexes_MarshallingArray_A);
			return FontEngine.GetContextualSubstitutionRecords(lookupIndex, FontEngine.s_GlyphIndexes_MarshallingArray_A);
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00004080 File Offset: 0x00002280
		internal static ContextualSubstitutionRecord[] GetContextualSubstitutionRecords(int lookupIndex, List<uint> glyphIndexes)
		{
			FontEngine.GenericListToMarshallingArray<uint>(ref glyphIndexes, ref FontEngine.s_GlyphIndexes_MarshallingArray_A);
			return FontEngine.GetContextualSubstitutionRecords(lookupIndex, FontEngine.s_GlyphIndexes_MarshallingArray_A);
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x000040AC File Offset: 0x000022AC
		private static ContextualSubstitutionRecord[] GetContextualSubstitutionRecords(int lookupIndex, uint[] glyphIndexes)
		{
			int num;
			FontEngine.PopulateContextualSubstitutionRecordMarshallingArray_from_GlyphIndexes(glyphIndexes, lookupIndex, out num);
			bool flag = num == 0;
			ContextualSubstitutionRecord[] result;
			if (flag)
			{
				result = null;
			}
			else
			{
				FontEngine.SetMarshallingArraySize<ContextualSubstitutionRecord>(ref FontEngine.s_ContextualSubstitutionRecords_MarshallingArray, num);
				FontEngine.GetContextualSubstitutionRecordsFromMarshallingArray(FontEngine.s_ContextualSubstitutionRecords_MarshallingArray);
				FontEngine.s_ContextualSubstitutionRecords_MarshallingArray[num] = default(ContextualSubstitutionRecord);
				result = FontEngine.s_ContextualSubstitutionRecords_MarshallingArray;
			}
			return result;
		}

		// Token: 0x060000D2 RID: 210
		[NativeMethod(Name = "TextCore::FontEngine::PopulateContextualSubstitutionRecordMarshallingArray", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int PopulateContextualSubstitutionRecordMarshallingArray_from_GlyphIndexes(uint[] glyphIndexes, int lookupIndex, out int recordCount);

		// Token: 0x060000D3 RID: 211
		[NativeMethod(Name = "TextCore::FontEngine::GetContextualSubstitutionRecordsFromMarshallingArray", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetContextualSubstitutionRecordsFromMarshallingArray([Out] ContextualSubstitutionRecord[] substitutionRecords);

		// Token: 0x060000D4 RID: 212
		[NativeMethod(Name = "TextCore::FontEngine::GetAllChainingContextualSubstitutionRecords", IsThreadSafe = true, IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern ChainingContextualSubstitutionRecord[] GetAllChainingContextualSubstitutionRecords();

		// Token: 0x060000D5 RID: 213 RVA: 0x00004104 File Offset: 0x00002304
		internal static ChainingContextualSubstitutionRecord[] GetChainingContextualSubstitutionRecords(int lookupIndex, uint glyphIndex)
		{
			FontEngine.GlyphIndexToMarshallingArray(glyphIndex, ref FontEngine.s_GlyphIndexes_MarshallingArray_A);
			return FontEngine.GetChainingContextualSubstitutionRecords(lookupIndex, FontEngine.s_GlyphIndexes_MarshallingArray_A);
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00004130 File Offset: 0x00002330
		internal static ChainingContextualSubstitutionRecord[] GetChainingContextualSubstitutionRecords(int lookupIndex, List<uint> glyphIndexes)
		{
			FontEngine.GenericListToMarshallingArray<uint>(ref glyphIndexes, ref FontEngine.s_GlyphIndexes_MarshallingArray_A);
			return FontEngine.GetChainingContextualSubstitutionRecords(lookupIndex, FontEngine.s_GlyphIndexes_MarshallingArray_A);
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x0000415C File Offset: 0x0000235C
		private static ChainingContextualSubstitutionRecord[] GetChainingContextualSubstitutionRecords(int lookupIndex, uint[] glyphIndexes)
		{
			int num;
			FontEngine.PopulateChainingContextualSubstitutionRecordMarshallingArray_from_GlyphIndexes(glyphIndexes, lookupIndex, out num);
			bool flag = num == 0;
			ChainingContextualSubstitutionRecord[] result;
			if (flag)
			{
				result = null;
			}
			else
			{
				FontEngine.SetMarshallingArraySize<ChainingContextualSubstitutionRecord>(ref FontEngine.s_ChainingContextualSubstitutionRecords_MarshallingArray, num);
				FontEngine.GetChainingContextualSubstitutionRecordsFromMarshallingArray(FontEngine.s_ChainingContextualSubstitutionRecords_MarshallingArray);
				FontEngine.s_ChainingContextualSubstitutionRecords_MarshallingArray[num] = default(ChainingContextualSubstitutionRecord);
				result = FontEngine.s_ChainingContextualSubstitutionRecords_MarshallingArray;
			}
			return result;
		}

		// Token: 0x060000D8 RID: 216
		[NativeMethod(Name = "TextCore::FontEngine::PopulateChainingContextualSubstitutionRecordMarshallingArray", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int PopulateChainingContextualSubstitutionRecordMarshallingArray_from_GlyphIndexes(uint[] glyphIndexes, int lookupIndex, out int recordCount);

		// Token: 0x060000D9 RID: 217
		[NativeMethod(Name = "TextCore::FontEngine::GetChainingContextualSubstitutionRecordsFromMarshallingArray", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetChainingContextualSubstitutionRecordsFromMarshallingArray([Out] ChainingContextualSubstitutionRecord[] substitutionRecords);

		// Token: 0x060000DA RID: 218 RVA: 0x000041B4 File Offset: 0x000023B4
		internal static GlyphPairAdjustmentRecord[] GetGlyphPairAdjustmentTable(uint[] glyphIndexes)
		{
			int num;
			FontEngine.PopulatePairAdjustmentRecordMarshallingArray_from_KernTable(glyphIndexes, out num);
			bool flag = num == 0;
			GlyphPairAdjustmentRecord[] result;
			if (flag)
			{
				result = null;
			}
			else
			{
				FontEngine.SetMarshallingArraySize<GlyphPairAdjustmentRecord>(ref FontEngine.s_PairAdjustmentRecords_MarshallingArray, num);
				FontEngine.GetPairAdjustmentRecordsFromMarshallingArray(FontEngine.s_PairAdjustmentRecords_MarshallingArray);
				FontEngine.s_PairAdjustmentRecords_MarshallingArray[num] = default(GlyphPairAdjustmentRecord);
				result = FontEngine.s_PairAdjustmentRecords_MarshallingArray;
			}
			return result;
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00004208 File Offset: 0x00002408
		internal static GlyphPairAdjustmentRecord[] GetGlyphPairAdjustmentRecords(List<uint> glyphIndexes, out int recordCount)
		{
			FontEngine.GenericListToMarshallingArray<uint>(ref glyphIndexes, ref FontEngine.s_GlyphIndexes_MarshallingArray_A);
			FontEngine.PopulatePairAdjustmentRecordMarshallingArray_from_KernTable(FontEngine.s_GlyphIndexes_MarshallingArray_A, out recordCount);
			bool flag = recordCount == 0;
			GlyphPairAdjustmentRecord[] result;
			if (flag)
			{
				result = null;
			}
			else
			{
				FontEngine.SetMarshallingArraySize<GlyphPairAdjustmentRecord>(ref FontEngine.s_PairAdjustmentRecords_MarshallingArray, recordCount);
				FontEngine.GetPairAdjustmentRecordsFromMarshallingArray(FontEngine.s_PairAdjustmentRecords_MarshallingArray);
				FontEngine.s_PairAdjustmentRecords_MarshallingArray[recordCount] = default(GlyphPairAdjustmentRecord);
				result = FontEngine.s_PairAdjustmentRecords_MarshallingArray;
			}
			return result;
		}

		// Token: 0x060000DC RID: 220
		[NativeMethod(Name = "TextCore::FontEngine::PopulatePairAdjustmentRecordMarshallingArrayFromKernTable", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int PopulatePairAdjustmentRecordMarshallingArray_from_KernTable(uint[] glyphIndexes, out int recordCount);

		// Token: 0x060000DD RID: 221 RVA: 0x00004270 File Offset: 0x00002470
		internal static GlyphPairAdjustmentRecord[] GetGlyphPairAdjustmentRecords(uint glyphIndex, out int recordCount)
		{
			FontEngine.PopulatePairAdjustmentRecordMarshallingArray_from_GlyphIndex(glyphIndex, out recordCount);
			bool flag = recordCount == 0;
			GlyphPairAdjustmentRecord[] result;
			if (flag)
			{
				result = null;
			}
			else
			{
				FontEngine.SetMarshallingArraySize<GlyphPairAdjustmentRecord>(ref FontEngine.s_PairAdjustmentRecords_MarshallingArray, recordCount);
				FontEngine.GetPairAdjustmentRecordsFromMarshallingArray(FontEngine.s_PairAdjustmentRecords_MarshallingArray);
				FontEngine.s_PairAdjustmentRecords_MarshallingArray[recordCount] = default(GlyphPairAdjustmentRecord);
				result = FontEngine.s_PairAdjustmentRecords_MarshallingArray;
			}
			return result;
		}

		// Token: 0x060000DE RID: 222
		[NativeMethod(Name = "TextCore::FontEngine::PopulatePairAdjustmentRecordMarshallingArrayFromKernTable", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int PopulatePairAdjustmentRecordMarshallingArray_from_GlyphIndex(uint glyphIndex, out int recordCount);

		// Token: 0x060000DF RID: 223 RVA: 0x000042C8 File Offset: 0x000024C8
		internal static GlyphPairAdjustmentRecord[] GetGlyphPairAdjustmentRecords(List<uint> newGlyphIndexes, List<uint> allGlyphIndexes)
		{
			FontEngine.GenericListToMarshallingArray<uint>(ref newGlyphIndexes, ref FontEngine.s_GlyphIndexes_MarshallingArray_A);
			FontEngine.GenericListToMarshallingArray<uint>(ref allGlyphIndexes, ref FontEngine.s_GlyphIndexes_MarshallingArray_B);
			int num;
			FontEngine.PopulatePairAdjustmentRecordMarshallingArray_for_NewlyAddedGlyphIndexes(FontEngine.s_GlyphIndexes_MarshallingArray_A, FontEngine.s_GlyphIndexes_MarshallingArray_B, out num);
			bool flag = num == 0;
			GlyphPairAdjustmentRecord[] result;
			if (flag)
			{
				result = null;
			}
			else
			{
				FontEngine.SetMarshallingArraySize<GlyphPairAdjustmentRecord>(ref FontEngine.s_PairAdjustmentRecords_MarshallingArray, num);
				FontEngine.GetPairAdjustmentRecordsFromMarshallingArray(FontEngine.s_PairAdjustmentRecords_MarshallingArray);
				FontEngine.s_PairAdjustmentRecords_MarshallingArray[num] = default(GlyphPairAdjustmentRecord);
				result = FontEngine.s_PairAdjustmentRecords_MarshallingArray;
			}
			return result;
		}

		// Token: 0x060000E0 RID: 224
		[NativeMethod(Name = "TextCore::FontEngine::PopulatePairAdjustmentRecordMarshallingArrayFromKernTable", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int PopulatePairAdjustmentRecordMarshallingArray_for_NewlyAddedGlyphIndexes(uint[] newGlyphIndexes, uint[] allGlyphIndexes, out int recordCount);

		// Token: 0x060000E1 RID: 225 RVA: 0x00004340 File Offset: 0x00002540
		[NativeMethod(Name = "TextCore::FontEngine::GetGlyphPairAdjustmentRecord", IsFreeFunction = true)]
		internal static GlyphPairAdjustmentRecord GetGlyphPairAdjustmentRecord(uint firstGlyphIndex, uint secondGlyphIndex)
		{
			GlyphPairAdjustmentRecord result;
			FontEngine.GetGlyphPairAdjustmentRecord_Injected(firstGlyphIndex, secondGlyphIndex, out result);
			return result;
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00004358 File Offset: 0x00002558
		internal static GlyphAdjustmentRecord[] GetSingleAdjustmentRecords(int lookupIndex, uint glyphIndex)
		{
			bool flag = FontEngine.s_GlyphIndexes_MarshallingArray_A == null;
			if (flag)
			{
				FontEngine.s_GlyphIndexes_MarshallingArray_A = new uint[8];
			}
			FontEngine.s_GlyphIndexes_MarshallingArray_A[0] = glyphIndex;
			FontEngine.s_GlyphIndexes_MarshallingArray_A[1] = 0U;
			return FontEngine.GetSingleAdjustmentRecords(lookupIndex, FontEngine.s_GlyphIndexes_MarshallingArray_A);
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x0000439C File Offset: 0x0000259C
		internal static GlyphAdjustmentRecord[] GetSingleAdjustmentRecords(int lookupIndex, List<uint> glyphIndexes)
		{
			FontEngine.GenericListToMarshallingArray<uint>(ref glyphIndexes, ref FontEngine.s_GlyphIndexes_MarshallingArray_A);
			return FontEngine.GetSingleAdjustmentRecords(lookupIndex, FontEngine.s_GlyphIndexes_MarshallingArray_A);
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x000043C8 File Offset: 0x000025C8
		private static GlyphAdjustmentRecord[] GetSingleAdjustmentRecords(int lookupIndex, uint[] glyphIndexes)
		{
			int num;
			FontEngine.PopulateSingleAdjustmentRecordMarshallingArray_from_GlyphIndexes(glyphIndexes, lookupIndex, out num);
			bool flag = num == 0;
			GlyphAdjustmentRecord[] result;
			if (flag)
			{
				result = null;
			}
			else
			{
				FontEngine.SetMarshallingArraySize<GlyphAdjustmentRecord>(ref FontEngine.s_SingleAdjustmentRecords_MarshallingArray, num);
				FontEngine.GetSingleAdjustmentRecordsFromMarshallingArray(FontEngine.s_SingleAdjustmentRecords_MarshallingArray);
				FontEngine.s_SingleAdjustmentRecords_MarshallingArray[num] = default(GlyphAdjustmentRecord);
				result = FontEngine.s_SingleAdjustmentRecords_MarshallingArray;
			}
			return result;
		}

		// Token: 0x060000E5 RID: 229
		[NativeMethod(Name = "TextCore::FontEngine::PopulateSingleAdjustmentRecordMarshallingArray", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int PopulateSingleAdjustmentRecordMarshallingArray_from_GlyphIndexes(uint[] glyphIndexes, int lookupIndex, out int recordCount);

		// Token: 0x060000E6 RID: 230
		[NativeMethod(Name = "TextCore::FontEngine::GetSingleAdjustmentRecordsFromMarshallingArray", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetSingleAdjustmentRecordsFromMarshallingArray([Out] GlyphAdjustmentRecord[] singleSubstitutionRecords);

		// Token: 0x060000E7 RID: 231
		[NativeMethod(Name = "TextCore::FontEngine::GetPairAdjustmentRecords", IsThreadSafe = true, IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern GlyphPairAdjustmentRecord[] GetPairAdjustmentRecords(uint glyphIndex);

		// Token: 0x060000E8 RID: 232 RVA: 0x00004420 File Offset: 0x00002620
		[NativeMethod(Name = "TextCore::FontEngine::GetPairAdjustmentRecord", IsThreadSafe = true, IsFreeFunction = true)]
		internal static GlyphPairAdjustmentRecord GetPairAdjustmentRecord(uint firstGlyphIndex, uint secondGlyphIndex)
		{
			GlyphPairAdjustmentRecord result;
			FontEngine.GetPairAdjustmentRecord_Injected(firstGlyphIndex, secondGlyphIndex, out result);
			return result;
		}

		// Token: 0x060000E9 RID: 233
		[NativeMethod(Name = "TextCore::FontEngine::GetAllPairAdjustmentRecords", IsThreadSafe = true, IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern GlyphPairAdjustmentRecord[] GetAllPairAdjustmentRecords();

		// Token: 0x060000EA RID: 234 RVA: 0x00004438 File Offset: 0x00002638
		internal static GlyphPairAdjustmentRecord[] GetPairAdjustmentRecords(List<uint> glyphIndexes)
		{
			FontEngine.GenericListToMarshallingArray<uint>(ref glyphIndexes, ref FontEngine.s_GlyphIndexes_MarshallingArray_A);
			return FontEngine.GetPairAdjustmentRecords(FontEngine.s_GlyphIndexes_MarshallingArray_A);
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00004464 File Offset: 0x00002664
		internal static GlyphPairAdjustmentRecord[] GetPairAdjustmentRecords(int lookupIndex, uint glyphIndex)
		{
			FontEngine.GlyphIndexToMarshallingArray(glyphIndex, ref FontEngine.s_GlyphIndexes_MarshallingArray_A);
			return FontEngine.GetPairAdjustmentRecords(lookupIndex, FontEngine.s_GlyphIndexes_MarshallingArray_A);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00004490 File Offset: 0x00002690
		internal static GlyphPairAdjustmentRecord[] GetPairAdjustmentRecords(int lookupIndex, List<uint> glyphIndexes)
		{
			FontEngine.GenericListToMarshallingArray<uint>(ref glyphIndexes, ref FontEngine.s_GlyphIndexes_MarshallingArray_A);
			return FontEngine.GetPairAdjustmentRecords(lookupIndex, FontEngine.s_GlyphIndexes_MarshallingArray_A);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x000044BC File Offset: 0x000026BC
		private static GlyphPairAdjustmentRecord[] GetPairAdjustmentRecords(uint[] glyphIndexes)
		{
			int num;
			FontEngine.PopulatePairAdjustmentRecordMarshallingArray(glyphIndexes, out num);
			bool flag = num == 0;
			GlyphPairAdjustmentRecord[] result;
			if (flag)
			{
				result = null;
			}
			else
			{
				FontEngine.SetMarshallingArraySize<GlyphPairAdjustmentRecord>(ref FontEngine.s_PairAdjustmentRecords_MarshallingArray, num);
				FontEngine.GetPairAdjustmentRecordsFromMarshallingArray(FontEngine.s_PairAdjustmentRecords_MarshallingArray);
				FontEngine.s_PairAdjustmentRecords_MarshallingArray[num] = default(GlyphPairAdjustmentRecord);
				result = FontEngine.s_PairAdjustmentRecords_MarshallingArray;
			}
			return result;
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00004510 File Offset: 0x00002710
		private static GlyphPairAdjustmentRecord[] GetPairAdjustmentRecords(int lookupIndex, uint[] glyphIndexes)
		{
			int num;
			FontEngine.PopulatePairAdjustmentRecordMarshallingArray_for_LookupIndex(glyphIndexes, lookupIndex, out num);
			bool flag = num == 0;
			GlyphPairAdjustmentRecord[] result;
			if (flag)
			{
				result = null;
			}
			else
			{
				FontEngine.SetMarshallingArraySize<GlyphPairAdjustmentRecord>(ref FontEngine.s_PairAdjustmentRecords_MarshallingArray, num);
				FontEngine.GetPairAdjustmentRecordsFromMarshallingArray(FontEngine.s_PairAdjustmentRecords_MarshallingArray);
				FontEngine.s_PairAdjustmentRecords_MarshallingArray[num] = default(GlyphPairAdjustmentRecord);
				result = FontEngine.s_PairAdjustmentRecords_MarshallingArray;
			}
			return result;
		}

		// Token: 0x060000EF RID: 239
		[NativeMethod(Name = "TextCore::FontEngine::PopulatePairAdjustmentRecordMarshallingArray", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int PopulatePairAdjustmentRecordMarshallingArray(uint[] glyphIndexes, out int recordCount);

		// Token: 0x060000F0 RID: 240
		[NativeMethod(Name = "TextCore::FontEngine::PopulatePairAdjustmentRecordMarshallingArray", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int PopulatePairAdjustmentRecordMarshallingArray_for_LookupIndex(uint[] glyphIndexes, int lookupIndex, out int recordCount);

		// Token: 0x060000F1 RID: 241
		[NativeMethod(Name = "TextCore::FontEngine::GetGlyphPairAdjustmentRecordsFromMarshallingArray", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetPairAdjustmentRecordsFromMarshallingArray([Out] GlyphPairAdjustmentRecord[] glyphPairAdjustmentRecords);

		// Token: 0x060000F2 RID: 242
		[NativeMethod(Name = "TextCore::FontEngine::GetAllMarkToBaseAdjustmentRecords", IsThreadSafe = true, IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern MarkToBaseAdjustmentRecord[] GetAllMarkToBaseAdjustmentRecords();

		// Token: 0x060000F3 RID: 243
		[NativeMethod(Name = "TextCore::FontEngine::GetMarkToBaseAdjustmentRecords", IsThreadSafe = true, IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern MarkToBaseAdjustmentRecord[] GetMarkToBaseAdjustmentRecords(uint baseGlyphIndex);

		// Token: 0x060000F4 RID: 244 RVA: 0x00004568 File Offset: 0x00002768
		[NativeMethod(Name = "TextCore::FontEngine::GetMarkToBaseAdjustmentRecord", IsFreeFunction = true)]
		internal static MarkToBaseAdjustmentRecord GetMarkToBaseAdjustmentRecord(uint baseGlyphIndex, uint markGlyphIndex)
		{
			MarkToBaseAdjustmentRecord result;
			FontEngine.GetMarkToBaseAdjustmentRecord_Injected(baseGlyphIndex, markGlyphIndex, out result);
			return result;
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00004580 File Offset: 0x00002780
		internal static MarkToBaseAdjustmentRecord[] GetMarkToBaseAdjustmentRecords(List<uint> glyphIndexes)
		{
			FontEngine.GenericListToMarshallingArray<uint>(ref glyphIndexes, ref FontEngine.s_GlyphIndexes_MarshallingArray_A);
			return FontEngine.GetMarkToBaseAdjustmentRecords(FontEngine.s_GlyphIndexes_MarshallingArray_A);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x000045AC File Offset: 0x000027AC
		internal static MarkToBaseAdjustmentRecord[] GetMarkToBaseAdjustmentRecords(int lookupIndex, List<uint> glyphIndexes)
		{
			FontEngine.GenericListToMarshallingArray<uint>(ref glyphIndexes, ref FontEngine.s_GlyphIndexes_MarshallingArray_A);
			return FontEngine.GetMarkToBaseAdjustmentRecords(lookupIndex, FontEngine.s_GlyphIndexes_MarshallingArray_A);
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x000045D8 File Offset: 0x000027D8
		private static MarkToBaseAdjustmentRecord[] GetMarkToBaseAdjustmentRecords(uint[] glyphIndexes)
		{
			int num;
			FontEngine.PopulateMarkToBaseAdjustmentRecordMarshallingArray(glyphIndexes, out num);
			bool flag = num == 0;
			MarkToBaseAdjustmentRecord[] result;
			if (flag)
			{
				result = null;
			}
			else
			{
				FontEngine.SetMarshallingArraySize<MarkToBaseAdjustmentRecord>(ref FontEngine.s_MarkToBaseAdjustmentRecords_MarshallingArray, num);
				FontEngine.GetMarkToBaseAdjustmentRecordsFromMarshallingArray(FontEngine.s_MarkToBaseAdjustmentRecords_MarshallingArray);
				FontEngine.s_MarkToBaseAdjustmentRecords_MarshallingArray[num] = default(MarkToBaseAdjustmentRecord);
				result = FontEngine.s_MarkToBaseAdjustmentRecords_MarshallingArray;
			}
			return result;
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x0000462C File Offset: 0x0000282C
		private static MarkToBaseAdjustmentRecord[] GetMarkToBaseAdjustmentRecords(int lookupIndex, uint[] glyphIndexes)
		{
			int num;
			FontEngine.PopulateMarkToBaseAdjustmentRecordMarshallingArray_for_LookupIndex(glyphIndexes, lookupIndex, out num);
			bool flag = num == 0;
			MarkToBaseAdjustmentRecord[] result;
			if (flag)
			{
				result = null;
			}
			else
			{
				FontEngine.SetMarshallingArraySize<MarkToBaseAdjustmentRecord>(ref FontEngine.s_MarkToBaseAdjustmentRecords_MarshallingArray, num);
				FontEngine.GetMarkToBaseAdjustmentRecordsFromMarshallingArray(FontEngine.s_MarkToBaseAdjustmentRecords_MarshallingArray);
				FontEngine.s_MarkToBaseAdjustmentRecords_MarshallingArray[num] = default(MarkToBaseAdjustmentRecord);
				result = FontEngine.s_MarkToBaseAdjustmentRecords_MarshallingArray;
			}
			return result;
		}

		// Token: 0x060000F9 RID: 249
		[NativeMethod(Name = "TextCore::FontEngine::PopulateMarkToBaseAdjustmentRecordMarshallingArray", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int PopulateMarkToBaseAdjustmentRecordMarshallingArray(uint[] glyphIndexes, out int recordCount);

		// Token: 0x060000FA RID: 250
		[NativeMethod(Name = "TextCore::FontEngine::PopulateMarkToBaseAdjustmentRecordMarshallingArray", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int PopulateMarkToBaseAdjustmentRecordMarshallingArray_for_LookupIndex(uint[] glyphIndexes, int lookupIndex, out int recordCount);

		// Token: 0x060000FB RID: 251
		[NativeMethod(Name = "TextCore::FontEngine::GetMarkToBaseAdjustmentRecordsFromMarshallingArray", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetMarkToBaseAdjustmentRecordsFromMarshallingArray([Out] MarkToBaseAdjustmentRecord[] adjustmentRecords);

		// Token: 0x060000FC RID: 252
		[NativeMethod(Name = "TextCore::FontEngine::GetAllMarkToMarkAdjustmentRecords", IsThreadSafe = true, IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern MarkToMarkAdjustmentRecord[] GetAllMarkToMarkAdjustmentRecords();

		// Token: 0x060000FD RID: 253
		[NativeMethod(Name = "TextCore::FontEngine::GetMarkToMarkAdjustmentRecords", IsThreadSafe = true, IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern MarkToMarkAdjustmentRecord[] GetMarkToMarkAdjustmentRecords(uint baseMarkGlyphIndex);

		// Token: 0x060000FE RID: 254 RVA: 0x00004684 File Offset: 0x00002884
		[NativeMethod(Name = "TextCore::FontEngine::GetMarkToMarkAdjustmentRecord", IsFreeFunction = true)]
		internal static MarkToMarkAdjustmentRecord GetMarkToMarkAdjustmentRecord(uint firstGlyphIndex, uint secondGlyphIndex)
		{
			MarkToMarkAdjustmentRecord result;
			FontEngine.GetMarkToMarkAdjustmentRecord_Injected(firstGlyphIndex, secondGlyphIndex, out result);
			return result;
		}

		// Token: 0x060000FF RID: 255 RVA: 0x0000469C File Offset: 0x0000289C
		internal static MarkToMarkAdjustmentRecord[] GetMarkToMarkAdjustmentRecords(List<uint> glyphIndexes)
		{
			FontEngine.GenericListToMarshallingArray<uint>(ref glyphIndexes, ref FontEngine.s_GlyphIndexes_MarshallingArray_A);
			return FontEngine.GetMarkToMarkAdjustmentRecords(FontEngine.s_GlyphIndexes_MarshallingArray_A);
		}

		// Token: 0x06000100 RID: 256 RVA: 0x000046C8 File Offset: 0x000028C8
		internal static MarkToMarkAdjustmentRecord[] GetMarkToMarkAdjustmentRecords(int lookupIndex, List<uint> glyphIndexes)
		{
			FontEngine.GenericListToMarshallingArray<uint>(ref glyphIndexes, ref FontEngine.s_GlyphIndexes_MarshallingArray_A);
			return FontEngine.GetMarkToMarkAdjustmentRecords(lookupIndex, FontEngine.s_GlyphIndexes_MarshallingArray_A);
		}

		// Token: 0x06000101 RID: 257 RVA: 0x000046F4 File Offset: 0x000028F4
		private static MarkToMarkAdjustmentRecord[] GetMarkToMarkAdjustmentRecords(uint[] glyphIndexes)
		{
			int num;
			FontEngine.PopulateMarkToMarkAdjustmentRecordMarshallingArray(FontEngine.s_GlyphIndexes_MarshallingArray_A, out num);
			bool flag = num == 0;
			MarkToMarkAdjustmentRecord[] result;
			if (flag)
			{
				result = null;
			}
			else
			{
				FontEngine.SetMarshallingArraySize<MarkToMarkAdjustmentRecord>(ref FontEngine.s_MarkToMarkAdjustmentRecords_MarshallingArray, num);
				FontEngine.GetMarkToMarkAdjustmentRecordsFromMarshallingArray(FontEngine.s_MarkToMarkAdjustmentRecords_MarshallingArray);
				FontEngine.s_MarkToMarkAdjustmentRecords_MarshallingArray[num] = default(MarkToMarkAdjustmentRecord);
				result = FontEngine.s_MarkToMarkAdjustmentRecords_MarshallingArray;
			}
			return result;
		}

		// Token: 0x06000102 RID: 258 RVA: 0x0000474C File Offset: 0x0000294C
		private static MarkToMarkAdjustmentRecord[] GetMarkToMarkAdjustmentRecords(int lookupIndex, uint[] glyphIndexes)
		{
			int num;
			FontEngine.PopulateMarkToMarkAdjustmentRecordMarshallingArray_for_LookupIndex(FontEngine.s_GlyphIndexes_MarshallingArray_A, lookupIndex, out num);
			bool flag = num == 0;
			MarkToMarkAdjustmentRecord[] result;
			if (flag)
			{
				result = null;
			}
			else
			{
				FontEngine.SetMarshallingArraySize<MarkToMarkAdjustmentRecord>(ref FontEngine.s_MarkToMarkAdjustmentRecords_MarshallingArray, num);
				FontEngine.GetMarkToMarkAdjustmentRecordsFromMarshallingArray(FontEngine.s_MarkToMarkAdjustmentRecords_MarshallingArray);
				FontEngine.s_MarkToMarkAdjustmentRecords_MarshallingArray[num] = default(MarkToMarkAdjustmentRecord);
				result = FontEngine.s_MarkToMarkAdjustmentRecords_MarshallingArray;
			}
			return result;
		}

		// Token: 0x06000103 RID: 259
		[NativeMethod(Name = "TextCore::FontEngine::PopulateMarkToMarkAdjustmentRecordMarshallingArray", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int PopulateMarkToMarkAdjustmentRecordMarshallingArray(uint[] glyphIndexes, out int recordCount);

		// Token: 0x06000104 RID: 260
		[NativeMethod(Name = "TextCore::FontEngine::PopulateMarkToMarkAdjustmentRecordMarshallingArray", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int PopulateMarkToMarkAdjustmentRecordMarshallingArray_for_LookupIndex(uint[] glyphIndexes, int lookupIndex, out int recordCount);

		// Token: 0x06000105 RID: 261
		[NativeMethod(Name = "TextCore::FontEngine::GetMarkToMarkAdjustmentRecordsFromMarshallingArray", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetMarkToMarkAdjustmentRecordsFromMarshallingArray([Out] MarkToMarkAdjustmentRecord[] adjustmentRecords);

		// Token: 0x06000106 RID: 262 RVA: 0x000047A8 File Offset: 0x000029A8
		private static void GlyphIndexToMarshallingArray(uint glyphIndex, ref uint[] dstArray)
		{
			bool flag = dstArray == null || dstArray.Length == 1;
			if (flag)
			{
				dstArray = new uint[8];
			}
			dstArray[0] = glyphIndex;
			dstArray[1] = 0U;
		}

		// Token: 0x06000107 RID: 263 RVA: 0x000047DC File Offset: 0x000029DC
		private static void GenericListToMarshallingArray<T>(ref List<T> srcList, ref T[] dstArray)
		{
			int count = srcList.Count;
			bool flag = dstArray == null || dstArray.Length <= count;
			if (flag)
			{
				int num = Mathf.NextPowerOfTwo(count + 1);
				bool flag2 = dstArray == null;
				if (flag2)
				{
					dstArray = new T[num];
				}
				else
				{
					Array.Resize<T>(ref dstArray, num);
				}
			}
			for (int i = 0; i < count; i++)
			{
				dstArray[i] = srcList[i];
			}
			dstArray[count] = default(T);
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00004868 File Offset: 0x00002A68
		private static void SetMarshallingArraySize<T>(ref T[] marshallingArray, int recordCount)
		{
			bool flag = marshallingArray == null || marshallingArray.Length <= recordCount;
			if (flag)
			{
				int num = Mathf.NextPowerOfTwo(recordCount + 1);
				bool flag2 = marshallingArray == null;
				if (flag2)
				{
					marshallingArray = new T[num];
				}
				else
				{
					Array.Resize<T>(ref marshallingArray, num);
				}
			}
		}

		// Token: 0x06000109 RID: 265
		[NativeMethod(Name = "TextCore::FontEngine::ResetAtlasTexture", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ResetAtlasTexture(Texture2D texture);

		// Token: 0x0600010A RID: 266
		[NativeMethod(Name = "TextCore::FontEngine::RenderToTexture", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void RenderBufferToTexture(Texture2D srcTexture, int padding, GlyphRenderMode renderMode, Texture2D dstTexture);

		// Token: 0x0600010B RID: 267 RVA: 0x000048B4 File Offset: 0x00002AB4
		// Note: this type is marked as 'beforefieldinit'.
		static FontEngine()
		{
		}

		// Token: 0x0600010C RID: 268
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int RenderGlyphToTexture_Internal_Injected(ref GlyphMarshallingStruct glyphStruct, int padding, GlyphRenderMode renderMode, Texture2D texture);

		// Token: 0x0600010D RID: 269
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetOpenTypeLayoutTable_Injected(OTL_TableType type, out OTL_Table ret);

		// Token: 0x0600010E RID: 270
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetGlyphPairAdjustmentRecord_Injected(uint firstGlyphIndex, uint secondGlyphIndex, out GlyphPairAdjustmentRecord ret);

		// Token: 0x0600010F RID: 271
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetPairAdjustmentRecord_Injected(uint firstGlyphIndex, uint secondGlyphIndex, out GlyphPairAdjustmentRecord ret);

		// Token: 0x06000110 RID: 272
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetMarkToBaseAdjustmentRecord_Injected(uint baseGlyphIndex, uint markGlyphIndex, out MarkToBaseAdjustmentRecord ret);

		// Token: 0x06000111 RID: 273
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetMarkToMarkAdjustmentRecord_Injected(uint firstGlyphIndex, uint secondGlyphIndex, out MarkToMarkAdjustmentRecord ret);

		// Token: 0x0400006E RID: 110
		private static Glyph[] s_Glyphs = new Glyph[16];

		// Token: 0x0400006F RID: 111
		private static uint[] s_GlyphIndexes_MarshallingArray_A;

		// Token: 0x04000070 RID: 112
		private static uint[] s_GlyphIndexes_MarshallingArray_B;

		// Token: 0x04000071 RID: 113
		private static GlyphMarshallingStruct[] s_GlyphMarshallingStruct_IN = new GlyphMarshallingStruct[16];

		// Token: 0x04000072 RID: 114
		private static GlyphMarshallingStruct[] s_GlyphMarshallingStruct_OUT = new GlyphMarshallingStruct[16];

		// Token: 0x04000073 RID: 115
		private static GlyphRect[] s_FreeGlyphRects = new GlyphRect[16];

		// Token: 0x04000074 RID: 116
		private static GlyphRect[] s_UsedGlyphRects = new GlyphRect[16];

		// Token: 0x04000075 RID: 117
		private static GlyphAdjustmentRecord[] s_SingleAdjustmentRecords_MarshallingArray;

		// Token: 0x04000076 RID: 118
		private static SingleSubstitutionRecord[] s_SingleSubstitutionRecords_MarshallingArray;

		// Token: 0x04000077 RID: 119
		private static MultipleSubstitutionRecord[] s_MultipleSubstitutionRecords_MarshallingArray;

		// Token: 0x04000078 RID: 120
		private static AlternateSubstitutionRecord[] s_AlternateSubstitutionRecords_MarshallingArray;

		// Token: 0x04000079 RID: 121
		private static LigatureSubstitutionRecord[] s_LigatureSubstitutionRecords_MarshallingArray;

		// Token: 0x0400007A RID: 122
		private static ContextualSubstitutionRecord[] s_ContextualSubstitutionRecords_MarshallingArray;

		// Token: 0x0400007B RID: 123
		private static ChainingContextualSubstitutionRecord[] s_ChainingContextualSubstitutionRecords_MarshallingArray;

		// Token: 0x0400007C RID: 124
		private static GlyphPairAdjustmentRecord[] s_PairAdjustmentRecords_MarshallingArray;

		// Token: 0x0400007D RID: 125
		private static MarkToBaseAdjustmentRecord[] s_MarkToBaseAdjustmentRecords_MarshallingArray;

		// Token: 0x0400007E RID: 126
		private static MarkToMarkAdjustmentRecord[] s_MarkToMarkAdjustmentRecords_MarshallingArray;

		// Token: 0x0400007F RID: 127
		private static Dictionary<uint, Glyph> s_GlyphLookupDictionary = new Dictionary<uint, Glyph>();
	}
}
