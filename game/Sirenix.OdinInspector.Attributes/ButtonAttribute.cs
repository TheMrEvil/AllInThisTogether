using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Sirenix.OdinInspector
{
	// Token: 0x02000006 RID: 6
	[AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = false)]
	[Conditional("UNITY_EDITOR")]
	public class ButtonAttribute : ShowInInspectorAttribute
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000009 RID: 9 RVA: 0x0000217D File Offset: 0x0000037D
		// (set) Token: 0x0600000A RID: 10 RVA: 0x00002185 File Offset: 0x00000385
		public int ButtonHeight
		{
			get
			{
				return this.buttonHeight;
			}
			set
			{
				this.buttonHeight = value;
				this.HasDefinedButtonHeight = true;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000B RID: 11 RVA: 0x00002195 File Offset: 0x00000395
		// (set) Token: 0x0600000C RID: 12 RVA: 0x0000219D File Offset: 0x0000039D
		public IconAlignment IconAlignment
		{
			get
			{
				return this.buttonIconAlignment;
			}
			set
			{
				this.buttonIconAlignment = value;
				this.HasDefinedButtonIconAlignment = true;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000D RID: 13 RVA: 0x000021AD File Offset: 0x000003AD
		// (set) Token: 0x0600000E RID: 14 RVA: 0x000021B5 File Offset: 0x000003B5
		public float ButtonAlignment
		{
			get
			{
				return this.buttonAlignment;
			}
			set
			{
				this.buttonAlignment = value;
				this.HasDefinedButtonAlignment = true;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000F RID: 15 RVA: 0x000021C5 File Offset: 0x000003C5
		// (set) Token: 0x06000010 RID: 16 RVA: 0x000021CD File Offset: 0x000003CD
		public bool Stretch
		{
			get
			{
				return this.stretch;
			}
			set
			{
				this.stretch = value;
				this.HasDefinedStretch = true;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000012 RID: 18 RVA: 0x000021ED File Offset: 0x000003ED
		// (set) Token: 0x06000011 RID: 17 RVA: 0x000021DD File Offset: 0x000003DD
		public bool DrawResult
		{
			get
			{
				return this.drawResult;
			}
			set
			{
				this.drawResult = value;
				this.drawResultIsSet = true;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000013 RID: 19 RVA: 0x000021F5 File Offset: 0x000003F5
		public bool DrawResultIsSet
		{
			get
			{
				return this.drawResultIsSet;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000014 RID: 20 RVA: 0x000021FD File Offset: 0x000003FD
		// (set) Token: 0x06000015 RID: 21 RVA: 0x00002205 File Offset: 0x00000405
		public bool HasDefinedButtonHeight
		{
			[CompilerGenerated]
			get
			{
				return this.<HasDefinedButtonHeight>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<HasDefinedButtonHeight>k__BackingField = value;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000016 RID: 22 RVA: 0x0000220E File Offset: 0x0000040E
		public bool HasDefinedIcon
		{
			get
			{
				return this.Icon > SdfIconType.None;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002219 File Offset: 0x00000419
		// (set) Token: 0x06000018 RID: 24 RVA: 0x00002221 File Offset: 0x00000421
		public bool HasDefinedButtonIconAlignment
		{
			[CompilerGenerated]
			get
			{
				return this.<HasDefinedButtonIconAlignment>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<HasDefinedButtonIconAlignment>k__BackingField = value;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000019 RID: 25 RVA: 0x0000222A File Offset: 0x0000042A
		// (set) Token: 0x0600001A RID: 26 RVA: 0x00002232 File Offset: 0x00000432
		public bool HasDefinedButtonAlignment
		{
			[CompilerGenerated]
			get
			{
				return this.<HasDefinedButtonAlignment>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<HasDefinedButtonAlignment>k__BackingField = value;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600001B RID: 27 RVA: 0x0000223B File Offset: 0x0000043B
		// (set) Token: 0x0600001C RID: 28 RVA: 0x00002243 File Offset: 0x00000443
		public bool HasDefinedStretch
		{
			[CompilerGenerated]
			get
			{
				return this.<HasDefinedStretch>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<HasDefinedStretch>k__BackingField = value;
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x0000224C File Offset: 0x0000044C
		public ButtonAttribute()
		{
			this.Name = null;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002269 File Offset: 0x00000469
		public ButtonAttribute(ButtonSizes size)
		{
			this.Name = null;
			this.ButtonHeight = (int)size;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x0000228D File Offset: 0x0000048D
		public ButtonAttribute(int buttonSize)
		{
			this.ButtonHeight = buttonSize;
			this.Name = null;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000022B1 File Offset: 0x000004B1
		public ButtonAttribute(string name)
		{
			this.Name = name;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000022CE File Offset: 0x000004CE
		public ButtonAttribute(string name, ButtonSizes buttonSize)
		{
			this.Name = name;
			this.ButtonHeight = (int)buttonSize;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000022CE File Offset: 0x000004CE
		public ButtonAttribute(string name, int buttonSize)
		{
			this.Name = name;
			this.ButtonHeight = buttonSize;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000022F2 File Offset: 0x000004F2
		public ButtonAttribute(ButtonStyle parameterBtnStyle)
		{
			this.Name = null;
			this.Style = parameterBtnStyle;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002316 File Offset: 0x00000516
		public ButtonAttribute(int buttonSize, ButtonStyle parameterBtnStyle)
		{
			this.ButtonHeight = buttonSize;
			this.Name = null;
			this.Style = parameterBtnStyle;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002316 File Offset: 0x00000516
		public ButtonAttribute(ButtonSizes size, ButtonStyle parameterBtnStyle)
		{
			this.ButtonHeight = (int)size;
			this.Name = null;
			this.Style = parameterBtnStyle;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002341 File Offset: 0x00000541
		public ButtonAttribute(string name, ButtonStyle parameterBtnStyle)
		{
			this.Name = name;
			this.Style = parameterBtnStyle;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002365 File Offset: 0x00000565
		public ButtonAttribute(string name, ButtonSizes buttonSize, ButtonStyle parameterBtnStyle)
		{
			this.Name = name;
			this.ButtonHeight = (int)buttonSize;
			this.Style = parameterBtnStyle;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002365 File Offset: 0x00000565
		public ButtonAttribute(string name, int buttonSize, ButtonStyle parameterBtnStyle)
		{
			this.Name = name;
			this.ButtonHeight = buttonSize;
			this.Style = parameterBtnStyle;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002390 File Offset: 0x00000590
		public ButtonAttribute(SdfIconType icon, IconAlignment iconAlignment)
		{
			this.Icon = icon;
			this.IconAlignment = iconAlignment;
			this.Name = null;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000023BB File Offset: 0x000005BB
		public ButtonAttribute(SdfIconType icon)
		{
			this.Icon = icon;
			this.Name = null;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000023DF File Offset: 0x000005DF
		public ButtonAttribute(SdfIconType icon, string name)
		{
			this.Name = name;
			this.Icon = icon;
		}

		// Token: 0x04000015 RID: 21
		public string Name;

		// Token: 0x04000016 RID: 22
		public ButtonStyle Style;

		// Token: 0x04000017 RID: 23
		public bool Expanded;

		// Token: 0x04000018 RID: 24
		public bool DisplayParameters = true;

		// Token: 0x04000019 RID: 25
		public bool DirtyOnClick = true;

		// Token: 0x0400001A RID: 26
		public SdfIconType Icon;

		// Token: 0x0400001B RID: 27
		[CompilerGenerated]
		private bool <HasDefinedButtonHeight>k__BackingField;

		// Token: 0x0400001C RID: 28
		[CompilerGenerated]
		private bool <HasDefinedButtonIconAlignment>k__BackingField;

		// Token: 0x0400001D RID: 29
		[CompilerGenerated]
		private bool <HasDefinedButtonAlignment>k__BackingField;

		// Token: 0x0400001E RID: 30
		[CompilerGenerated]
		private bool <HasDefinedStretch>k__BackingField;

		// Token: 0x0400001F RID: 31
		private int buttonHeight;

		// Token: 0x04000020 RID: 32
		private bool drawResult;

		// Token: 0x04000021 RID: 33
		private bool drawResultIsSet;

		// Token: 0x04000022 RID: 34
		private bool stretch;

		// Token: 0x04000023 RID: 35
		private IconAlignment buttonIconAlignment;

		// Token: 0x04000024 RID: 36
		private float buttonAlignment;
	}
}
