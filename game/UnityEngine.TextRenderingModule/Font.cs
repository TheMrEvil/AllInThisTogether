using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000010 RID: 16
	[NativeClass("TextRendering::Font")]
	[NativeHeader("Modules/TextRendering/Public/Font.h")]
	[NativeHeader("Modules/TextRendering/Public/FontImpl.h")]
	[StaticAccessor("TextRenderingPrivate", StaticAccessorType.DoubleColon)]
	public sealed class Font : Object
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600007E RID: 126 RVA: 0x00002FE4 File Offset: 0x000011E4
		// (remove) Token: 0x0600007F RID: 127 RVA: 0x00003018 File Offset: 0x00001218
		public static event Action<Font> textureRebuilt
		{
			[CompilerGenerated]
			add
			{
				Action<Font> action = Font.textureRebuilt;
				Action<Font> action2;
				do
				{
					action2 = action;
					Action<Font> value2 = (Action<Font>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<Font>>(ref Font.textureRebuilt, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<Font> action = Font.textureRebuilt;
				Action<Font> action2;
				do
				{
					action2 = action;
					Action<Font> value2 = (Action<Font>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<Font>>(ref Font.textureRebuilt, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000080 RID: 128 RVA: 0x0000304C File Offset: 0x0000124C
		// (remove) Token: 0x06000081 RID: 129 RVA: 0x00003084 File Offset: 0x00001284
		private event Font.FontTextureRebuildCallback m_FontTextureRebuildCallback
		{
			[CompilerGenerated]
			add
			{
				Font.FontTextureRebuildCallback fontTextureRebuildCallback = this.m_FontTextureRebuildCallback;
				Font.FontTextureRebuildCallback fontTextureRebuildCallback2;
				do
				{
					fontTextureRebuildCallback2 = fontTextureRebuildCallback;
					Font.FontTextureRebuildCallback value2 = (Font.FontTextureRebuildCallback)Delegate.Combine(fontTextureRebuildCallback2, value);
					fontTextureRebuildCallback = Interlocked.CompareExchange<Font.FontTextureRebuildCallback>(ref this.m_FontTextureRebuildCallback, value2, fontTextureRebuildCallback2);
				}
				while (fontTextureRebuildCallback != fontTextureRebuildCallback2);
			}
			[CompilerGenerated]
			remove
			{
				Font.FontTextureRebuildCallback fontTextureRebuildCallback = this.m_FontTextureRebuildCallback;
				Font.FontTextureRebuildCallback fontTextureRebuildCallback2;
				do
				{
					fontTextureRebuildCallback2 = fontTextureRebuildCallback;
					Font.FontTextureRebuildCallback value2 = (Font.FontTextureRebuildCallback)Delegate.Remove(fontTextureRebuildCallback2, value);
					fontTextureRebuildCallback = Interlocked.CompareExchange<Font.FontTextureRebuildCallback>(ref this.m_FontTextureRebuildCallback, value2, fontTextureRebuildCallback2);
				}
				while (fontTextureRebuildCallback != fontTextureRebuildCallback2);
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000082 RID: 130
		// (set) Token: 0x06000083 RID: 131
		public extern Material material { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000084 RID: 132
		// (set) Token: 0x06000085 RID: 133
		public extern string[] fontNames { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] [param: Unmarshalled] set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000086 RID: 134
		public extern bool dynamic { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000087 RID: 135
		public extern int ascent { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000088 RID: 136
		public extern int fontSize { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000089 RID: 137
		// (set) Token: 0x0600008A RID: 138
		public extern CharacterInfo[] characterInfo { [FreeFunction("TextRenderingPrivate::GetFontCharacterInfo", HasExplicitThis = true)] [MethodImpl(MethodImplOptions.InternalCall)] get; [FreeFunction("TextRenderingPrivate::SetFontCharacterInfo", HasExplicitThis = true)] [MethodImpl(MethodImplOptions.InternalCall)] [param: Unmarshalled] set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600008B RID: 139
		[NativeProperty("LineSpacing", false, TargetType.Function)]
		public extern int lineHeight { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600008C RID: 140 RVA: 0x000030BC File Offset: 0x000012BC
		// (set) Token: 0x0600008D RID: 141 RVA: 0x000030D4 File Offset: 0x000012D4
		[Obsolete("Font.textureRebuildCallback has been deprecated. Use Font.textureRebuilt instead.")]
		public Font.FontTextureRebuildCallback textureRebuildCallback
		{
			get
			{
				return this.m_FontTextureRebuildCallback;
			}
			set
			{
				this.m_FontTextureRebuildCallback = value;
			}
		}

		// Token: 0x0600008E RID: 142 RVA: 0x000030DE File Offset: 0x000012DE
		public Font()
		{
			Font.Internal_CreateFont(this, null);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000030F0 File Offset: 0x000012F0
		public Font(string name)
		{
			bool flag = Path.GetDirectoryName(name) == string.Empty;
			bool flag2 = flag;
			if (flag2)
			{
				Font.Internal_CreateFont(this, name);
			}
			else
			{
				Font.Internal_CreateFontFromPath(this, name);
			}
		}

		// Token: 0x06000090 RID: 144 RVA: 0x0000312D File Offset: 0x0000132D
		private Font(string[] names, int size)
		{
			Font.Internal_CreateDynamicFont(this, names, size);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00003140 File Offset: 0x00001340
		public static Font CreateDynamicFontFromOSFont(string fontname, int size)
		{
			return new Font(new string[]
			{
				fontname
			}, size);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00003164 File Offset: 0x00001364
		public static Font CreateDynamicFontFromOSFont(string[] fontnames, int size)
		{
			return new Font(fontnames, size);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x0000317D File Offset: 0x0000137D
		[RequiredByNativeCode]
		internal static void InvokeTextureRebuilt_Internal(Font font)
		{
			Action<Font> action = Font.textureRebuilt;
			if (action != null)
			{
				action(font);
			}
			Font.FontTextureRebuildCallback fontTextureRebuildCallback = font.m_FontTextureRebuildCallback;
			if (fontTextureRebuildCallback != null)
			{
				fontTextureRebuildCallback();
			}
		}

		// Token: 0x06000094 RID: 148 RVA: 0x000031A4 File Offset: 0x000013A4
		public static int GetMaxVertsForString(string str)
		{
			return str.Length * 4 + 4;
		}

		// Token: 0x06000095 RID: 149
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern Font GetDefault();

		// Token: 0x06000096 RID: 150 RVA: 0x000031C0 File Offset: 0x000013C0
		public bool HasCharacter(char c)
		{
			return this.HasCharacter((int)c);
		}

		// Token: 0x06000097 RID: 151
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool HasCharacter(int c);

		// Token: 0x06000098 RID: 152
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string[] GetOSInstalledFontNames();

		// Token: 0x06000099 RID: 153
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string[] GetPathsToOSFonts();

		// Token: 0x0600009A RID: 154
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_CreateFont([Writable] Font self, string name);

		// Token: 0x0600009B RID: 155
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_CreateFontFromPath([Writable] Font self, string fontPath);

		// Token: 0x0600009C RID: 156
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_CreateDynamicFont([Writable] Font self, [Unmarshalled] string[] _names, int size);

		// Token: 0x0600009D RID: 157
		[FreeFunction("TextRenderingPrivate::GetCharacterInfo", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool GetCharacterInfo(char ch, out CharacterInfo info, [DefaultValue("0")] int size, [DefaultValue("FontStyle.Normal")] FontStyle style);

		// Token: 0x0600009E RID: 158 RVA: 0x000031DC File Offset: 0x000013DC
		[ExcludeFromDocs]
		public bool GetCharacterInfo(char ch, out CharacterInfo info, int size)
		{
			return this.GetCharacterInfo(ch, out info, size, FontStyle.Normal);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x000031F8 File Offset: 0x000013F8
		[ExcludeFromDocs]
		public bool GetCharacterInfo(char ch, out CharacterInfo info)
		{
			return this.GetCharacterInfo(ch, out info, 0, FontStyle.Normal);
		}

		// Token: 0x060000A0 RID: 160
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void RequestCharactersInTexture(string characters, [DefaultValue("0")] int size, [DefaultValue("FontStyle.Normal")] FontStyle style);

		// Token: 0x060000A1 RID: 161 RVA: 0x00003214 File Offset: 0x00001414
		[ExcludeFromDocs]
		public void RequestCharactersInTexture(string characters, int size)
		{
			this.RequestCharactersInTexture(characters, size, FontStyle.Normal);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00003221 File Offset: 0x00001421
		[ExcludeFromDocs]
		public void RequestCharactersInTexture(string characters)
		{
			this.RequestCharactersInTexture(characters, 0, FontStyle.Normal);
		}

		// Token: 0x04000054 RID: 84
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private static Action<Font> textureRebuilt;

		// Token: 0x04000055 RID: 85
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Font.FontTextureRebuildCallback m_FontTextureRebuildCallback;

		// Token: 0x02000011 RID: 17
		// (Invoke) Token: 0x060000A4 RID: 164
		public delegate void FontTextureRebuildCallback();
	}
}
