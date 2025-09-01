using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000006 RID: 6
	[NativeHeader("Modules/TextRendering/TextGenerator.h")]
	[UsedByNativeCode]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class TextGenerator : IDisposable
	{
		// Token: 0x0600001E RID: 30 RVA: 0x000023BD File Offset: 0x000005BD
		public TextGenerator() : this(50)
		{
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000023C9 File Offset: 0x000005C9
		public TextGenerator(int initialCapacity)
		{
			this.m_Ptr = TextGenerator.Internal_Create();
			this.m_Verts = new List<UIVertex>((initialCapacity + 1) * 4);
			this.m_Characters = new List<UICharInfo>(initialCapacity + 1);
			this.m_Lines = new List<UILineInfo>(20);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000240C File Offset: 0x0000060C
		~TextGenerator()
		{
			((IDisposable)this).Dispose();
		}

		// Token: 0x06000021 RID: 33 RVA: 0x0000243C File Offset: 0x0000063C
		void IDisposable.Dispose()
		{
			bool flag = this.m_Ptr != IntPtr.Zero;
			if (flag)
			{
				TextGenerator.Internal_Destroy(this.m_Ptr);
				this.m_Ptr = IntPtr.Zero;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000022 RID: 34 RVA: 0x00002477 File Offset: 0x00000677
		public int characterCountVisible
		{
			get
			{
				return this.characterCount - 1;
			}
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002484 File Offset: 0x00000684
		private TextGenerationSettings ValidatedSettings(TextGenerationSettings settings)
		{
			bool flag = settings.font != null && settings.font.dynamic;
			TextGenerationSettings result;
			if (flag)
			{
				result = settings;
			}
			else
			{
				bool flag2 = settings.fontSize != 0 || settings.fontStyle > FontStyle.Normal;
				if (flag2)
				{
					bool flag3 = settings.font != null;
					if (flag3)
					{
						Debug.LogWarningFormat(settings.font, "Font size and style overrides are only supported for dynamic fonts. Font '{0}' is not dynamic.", new object[]
						{
							settings.font.name
						});
					}
					settings.fontSize = 0;
					settings.fontStyle = FontStyle.Normal;
				}
				bool resizeTextForBestFit = settings.resizeTextForBestFit;
				if (resizeTextForBestFit)
				{
					bool flag4 = settings.font != null;
					if (flag4)
					{
						Debug.LogWarningFormat(settings.font, "BestFit is only supported for dynamic fonts. Font '{0}' is not dynamic.", new object[]
						{
							settings.font.name
						});
					}
					settings.resizeTextForBestFit = false;
				}
				result = settings;
			}
			return result;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x0000256A File Offset: 0x0000076A
		public void Invalidate()
		{
			this.m_HasGenerated = false;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002574 File Offset: 0x00000774
		public void GetCharacters(List<UICharInfo> characters)
		{
			this.GetCharactersInternal(characters);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x0000257F File Offset: 0x0000077F
		public void GetLines(List<UILineInfo> lines)
		{
			this.GetLinesInternal(lines);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x0000258A File Offset: 0x0000078A
		public void GetVertices(List<UIVertex> vertices)
		{
			this.GetVerticesInternal(vertices);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002598 File Offset: 0x00000798
		public float GetPreferredWidth(string str, TextGenerationSettings settings)
		{
			settings.horizontalOverflow = HorizontalWrapMode.Overflow;
			settings.verticalOverflow = VerticalWrapMode.Overflow;
			settings.updateBounds = true;
			this.Populate(str, settings);
			return this.rectExtents.width;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000025DC File Offset: 0x000007DC
		public float GetPreferredHeight(string str, TextGenerationSettings settings)
		{
			settings.verticalOverflow = VerticalWrapMode.Overflow;
			settings.updateBounds = true;
			this.Populate(str, settings);
			return this.rectExtents.height;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002618 File Offset: 0x00000818
		public bool PopulateWithErrors(string str, TextGenerationSettings settings, GameObject context)
		{
			TextGenerationError textGenerationError = this.PopulateWithError(str, settings);
			bool flag = textGenerationError == TextGenerationError.None;
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				bool flag2 = (textGenerationError & TextGenerationError.CustomSizeOnNonDynamicFont) > TextGenerationError.None;
				if (flag2)
				{
					Debug.LogErrorFormat(context, "Font '{0}' is not dynamic, which is required to override its size", new object[]
					{
						settings.font
					});
				}
				bool flag3 = (textGenerationError & TextGenerationError.CustomStyleOnNonDynamicFont) > TextGenerationError.None;
				if (flag3)
				{
					Debug.LogErrorFormat(context, "Font '{0}' is not dynamic, which is required to override its style", new object[]
					{
						settings.font
					});
				}
				result = false;
			}
			return result;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x0000268C File Offset: 0x0000088C
		public bool Populate(string str, TextGenerationSettings settings)
		{
			TextGenerationError textGenerationError = this.PopulateWithError(str, settings);
			return textGenerationError == TextGenerationError.None;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000026AC File Offset: 0x000008AC
		private TextGenerationError PopulateWithError(string str, TextGenerationSettings settings)
		{
			bool flag = this.m_HasGenerated && str == this.m_LastString && settings.Equals(this.m_LastSettings);
			TextGenerationError lastValid;
			if (flag)
			{
				lastValid = this.m_LastValid;
			}
			else
			{
				this.m_LastValid = this.PopulateAlways(str, settings);
				lastValid = this.m_LastValid;
			}
			return lastValid;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002708 File Offset: 0x00000908
		private TextGenerationError PopulateAlways(string str, TextGenerationSettings settings)
		{
			this.m_LastString = str;
			this.m_HasGenerated = true;
			this.m_CachedVerts = false;
			this.m_CachedCharacters = false;
			this.m_CachedLines = false;
			this.m_LastSettings = settings;
			TextGenerationSettings textGenerationSettings = this.ValidatedSettings(settings);
			TextGenerationError textGenerationError;
			this.Populate_Internal(str, textGenerationSettings.font, textGenerationSettings.color, textGenerationSettings.fontSize, textGenerationSettings.scaleFactor, textGenerationSettings.lineSpacing, textGenerationSettings.fontStyle, textGenerationSettings.richText, textGenerationSettings.resizeTextForBestFit, textGenerationSettings.resizeTextMinSize, textGenerationSettings.resizeTextMaxSize, textGenerationSettings.verticalOverflow, textGenerationSettings.horizontalOverflow, textGenerationSettings.updateBounds, textGenerationSettings.textAnchor, textGenerationSettings.generationExtents, textGenerationSettings.pivot, textGenerationSettings.generateOutOfBounds, textGenerationSettings.alignByGeometry, out textGenerationError);
			this.m_LastValid = textGenerationError;
			return textGenerationError;
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600002E RID: 46 RVA: 0x000027CC File Offset: 0x000009CC
		public IList<UIVertex> verts
		{
			get
			{
				bool flag = !this.m_CachedVerts;
				if (flag)
				{
					this.GetVertices(this.m_Verts);
					this.m_CachedVerts = true;
				}
				return this.m_Verts;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600002F RID: 47 RVA: 0x00002808 File Offset: 0x00000A08
		public IList<UICharInfo> characters
		{
			get
			{
				bool flag = !this.m_CachedCharacters;
				if (flag)
				{
					this.GetCharacters(this.m_Characters);
					this.m_CachedCharacters = true;
				}
				return this.m_Characters;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00002844 File Offset: 0x00000A44
		public IList<UILineInfo> lines
		{
			get
			{
				bool flag = !this.m_CachedLines;
				if (flag)
				{
					this.GetLines(this.m_Lines);
					this.m_CachedLines = true;
				}
				return this.m_Lines;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00002880 File Offset: 0x00000A80
		public Rect rectExtents
		{
			get
			{
				Rect result;
				this.get_rectExtents_Injected(out result);
				return result;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000032 RID: 50
		public extern int vertexCount { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000033 RID: 51
		public extern int characterCount { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000034 RID: 52
		public extern int lineCount { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000035 RID: 53
		[NativeProperty("FontSizeFoundForBestFit", false, TargetType.Function)]
		public extern int fontSizeUsedForBestFit { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06000036 RID: 54
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Internal_Create();

		// Token: 0x06000037 RID: 55
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Destroy(IntPtr ptr);

		// Token: 0x06000038 RID: 56 RVA: 0x00002898 File Offset: 0x00000A98
		internal bool Populate_Internal(string str, Font font, Color color, int fontSize, float scaleFactor, float lineSpacing, FontStyle style, bool richText, bool resizeTextForBestFit, int resizeTextMinSize, int resizeTextMaxSize, int verticalOverFlow, int horizontalOverflow, bool updateBounds, TextAnchor anchor, float extentsX, float extentsY, float pivotX, float pivotY, bool generateOutOfBounds, bool alignByGeometry, out uint error)
		{
			return this.Populate_Internal_Injected(str, font, ref color, fontSize, scaleFactor, lineSpacing, style, richText, resizeTextForBestFit, resizeTextMinSize, resizeTextMaxSize, verticalOverFlow, horizontalOverflow, updateBounds, anchor, extentsX, extentsY, pivotX, pivotY, generateOutOfBounds, alignByGeometry, out error);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000028D8 File Offset: 0x00000AD8
		internal bool Populate_Internal(string str, Font font, Color color, int fontSize, float scaleFactor, float lineSpacing, FontStyle style, bool richText, bool resizeTextForBestFit, int resizeTextMinSize, int resizeTextMaxSize, VerticalWrapMode verticalOverFlow, HorizontalWrapMode horizontalOverflow, bool updateBounds, TextAnchor anchor, Vector2 extents, Vector2 pivot, bool generateOutOfBounds, bool alignByGeometry, out TextGenerationError error)
		{
			bool flag = font == null;
			bool result;
			if (flag)
			{
				error = TextGenerationError.NoFont;
				result = false;
			}
			else
			{
				uint num = 0U;
				bool flag2 = this.Populate_Internal(str, font, color, fontSize, scaleFactor, lineSpacing, style, richText, resizeTextForBestFit, resizeTextMinSize, resizeTextMaxSize, (int)verticalOverFlow, (int)horizontalOverflow, updateBounds, anchor, extents.x, extents.y, pivot.x, pivot.y, generateOutOfBounds, alignByGeometry, out num);
				error = (TextGenerationError)num;
				result = flag2;
			}
			return result;
		}

		// Token: 0x0600003A RID: 58
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern UIVertex[] GetVerticesArray();

		// Token: 0x0600003B RID: 59
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern UICharInfo[] GetCharactersArray();

		// Token: 0x0600003C RID: 60
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern UILineInfo[] GetLinesArray();

		// Token: 0x0600003D RID: 61
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetVerticesInternal(object vertices);

		// Token: 0x0600003E RID: 62
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetCharactersInternal(object characters);

		// Token: 0x0600003F RID: 63
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetLinesInternal(object lines);

		// Token: 0x06000040 RID: 64
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_rectExtents_Injected(out Rect ret);

		// Token: 0x06000041 RID: 65
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool Populate_Internal_Injected(string str, Font font, ref Color color, int fontSize, float scaleFactor, float lineSpacing, FontStyle style, bool richText, bool resizeTextForBestFit, int resizeTextMinSize, int resizeTextMaxSize, int verticalOverFlow, int horizontalOverflow, bool updateBounds, TextAnchor anchor, float extentsX, float extentsY, float pivotX, float pivotY, bool generateOutOfBounds, bool alignByGeometry, out uint error);

		// Token: 0x0400001D RID: 29
		internal IntPtr m_Ptr;

		// Token: 0x0400001E RID: 30
		private string m_LastString;

		// Token: 0x0400001F RID: 31
		private TextGenerationSettings m_LastSettings;

		// Token: 0x04000020 RID: 32
		private bool m_HasGenerated;

		// Token: 0x04000021 RID: 33
		private TextGenerationError m_LastValid;

		// Token: 0x04000022 RID: 34
		private readonly List<UIVertex> m_Verts;

		// Token: 0x04000023 RID: 35
		private readonly List<UICharInfo> m_Characters;

		// Token: 0x04000024 RID: 36
		private readonly List<UILineInfo> m_Lines;

		// Token: 0x04000025 RID: 37
		private bool m_CachedVerts;

		// Token: 0x04000026 RID: 38
		private bool m_CachedCharacters;

		// Token: 0x04000027 RID: 39
		private bool m_CachedLines;
	}
}
