using System;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000016 RID: 22
	[RequiredByNativeCode(Optional = true, GenerateProxy = true)]
	[NativeHeader("Modules/IMGUI/GUIContent.h")]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public class GUIContent
	{
		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000184 RID: 388 RVA: 0x00007998 File Offset: 0x00005B98
		// (set) Token: 0x06000185 RID: 389 RVA: 0x000079B0 File Offset: 0x00005BB0
		public string text
		{
			get
			{
				return this.m_Text;
			}
			set
			{
				this.m_Text = value;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000186 RID: 390 RVA: 0x000079BC File Offset: 0x00005BBC
		// (set) Token: 0x06000187 RID: 391 RVA: 0x000079D4 File Offset: 0x00005BD4
		public Texture image
		{
			get
			{
				return this.m_Image;
			}
			set
			{
				this.m_Image = value;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000188 RID: 392 RVA: 0x000079E0 File Offset: 0x00005BE0
		// (set) Token: 0x06000189 RID: 393 RVA: 0x000079F8 File Offset: 0x00005BF8
		public string tooltip
		{
			get
			{
				return this.m_Tooltip;
			}
			set
			{
				this.m_Tooltip = value;
			}
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00007A02 File Offset: 0x00005C02
		public GUIContent()
		{
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00007A22 File Offset: 0x00005C22
		public GUIContent(string text) : this(text, null, string.Empty)
		{
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00007A33 File Offset: 0x00005C33
		public GUIContent(Texture image) : this(string.Empty, image, string.Empty)
		{
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00007A48 File Offset: 0x00005C48
		public GUIContent(string text, Texture image) : this(text, image, string.Empty)
		{
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00007A59 File Offset: 0x00005C59
		public GUIContent(string text, string tooltip) : this(text, null, tooltip)
		{
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00007A66 File Offset: 0x00005C66
		public GUIContent(Texture image, string tooltip) : this(string.Empty, image, tooltip)
		{
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00007A77 File Offset: 0x00005C77
		public GUIContent(string text, Texture image, string tooltip)
		{
			this.text = text;
			this.image = image;
			this.tooltip = tooltip;
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00007AB0 File Offset: 0x00005CB0
		public GUIContent(GUIContent src)
		{
			this.text = src.m_Text;
			this.image = src.m_Image;
			this.tooltip = src.m_Tooltip;
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000192 RID: 402 RVA: 0x00007B04 File Offset: 0x00005D04
		internal int hash
		{
			get
			{
				int result = 0;
				bool flag = !string.IsNullOrEmpty(this.m_Text);
				if (flag)
				{
					result = this.m_Text.GetHashCode() * 37;
				}
				return result;
			}
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00007B3C File Offset: 0x00005D3C
		internal static GUIContent Temp(string t)
		{
			GUIContent.s_Text.m_Text = t;
			GUIContent.s_Text.m_Tooltip = string.Empty;
			return GUIContent.s_Text;
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00007B70 File Offset: 0x00005D70
		internal static GUIContent Temp(string t, string tooltip)
		{
			GUIContent.s_Text.m_Text = t;
			GUIContent.s_Text.m_Tooltip = tooltip;
			return GUIContent.s_Text;
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00007BA0 File Offset: 0x00005DA0
		internal static GUIContent Temp(Texture i)
		{
			GUIContent.s_Image.m_Image = i;
			GUIContent.s_Image.m_Tooltip = string.Empty;
			return GUIContent.s_Image;
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00007BD4 File Offset: 0x00005DD4
		internal static GUIContent Temp(Texture i, string tooltip)
		{
			GUIContent.s_Image.m_Image = i;
			GUIContent.s_Image.m_Tooltip = tooltip;
			return GUIContent.s_Image;
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00007C04 File Offset: 0x00005E04
		internal static GUIContent Temp(string t, Texture i)
		{
			GUIContent.s_TextImage.m_Text = t;
			GUIContent.s_TextImage.m_Image = i;
			return GUIContent.s_TextImage;
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00007C34 File Offset: 0x00005E34
		internal static void ClearStaticCache()
		{
			GUIContent.s_Text.m_Text = null;
			GUIContent.s_Text.m_Tooltip = string.Empty;
			GUIContent.s_Image.m_Image = null;
			GUIContent.s_Image.m_Tooltip = string.Empty;
			GUIContent.s_TextImage.m_Text = null;
			GUIContent.s_TextImage.m_Image = null;
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00007C8C File Offset: 0x00005E8C
		internal static GUIContent[] Temp(string[] texts)
		{
			GUIContent[] array = new GUIContent[texts.Length];
			for (int i = 0; i < texts.Length; i++)
			{
				array[i] = new GUIContent(texts[i]);
			}
			return array;
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00007CC8 File Offset: 0x00005EC8
		internal static GUIContent[] Temp(Texture[] images)
		{
			GUIContent[] array = new GUIContent[images.Length];
			for (int i = 0; i < images.Length; i++)
			{
				array[i] = new GUIContent(images[i]);
			}
			return array;
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00007D04 File Offset: 0x00005F04
		public override string ToString()
		{
			string result;
			if ((result = this.text) == null)
			{
				result = (this.tooltip ?? base.ToString());
			}
			return result;
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00007D30 File Offset: 0x00005F30
		// Note: this type is marked as 'beforefieldinit'.
		static GUIContent()
		{
		}

		// Token: 0x0400006D RID: 109
		[SerializeField]
		private string m_Text = string.Empty;

		// Token: 0x0400006E RID: 110
		[SerializeField]
		private Texture m_Image;

		// Token: 0x0400006F RID: 111
		[SerializeField]
		private string m_Tooltip = string.Empty;

		// Token: 0x04000070 RID: 112
		private static readonly GUIContent s_Text = new GUIContent();

		// Token: 0x04000071 RID: 113
		private static readonly GUIContent s_Image = new GUIContent();

		// Token: 0x04000072 RID: 114
		private static readonly GUIContent s_TextImage = new GUIContent();

		// Token: 0x04000073 RID: 115
		public static GUIContent none = new GUIContent("");
	}
}
