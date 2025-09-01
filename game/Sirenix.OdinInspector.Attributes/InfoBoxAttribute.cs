using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Sirenix.OdinInspector
{
	// Token: 0x0200003C RID: 60
	[DontApplyToListElements]
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = true)]
	[Conditional("UNITY_EDITOR")]
	public sealed class InfoBoxAttribute : Attribute
	{
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600009A RID: 154 RVA: 0x000029DA File Offset: 0x00000BDA
		// (set) Token: 0x0600009B RID: 155 RVA: 0x000029E2 File Offset: 0x00000BE2
		public SdfIconType Icon
		{
			get
			{
				return this.icon;
			}
			set
			{
				this.icon = value;
				this.HasDefinedIcon = true;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600009C RID: 156 RVA: 0x000029F2 File Offset: 0x00000BF2
		// (set) Token: 0x0600009D RID: 157 RVA: 0x000029FA File Offset: 0x00000BFA
		public bool HasDefinedIcon
		{
			[CompilerGenerated]
			get
			{
				return this.<HasDefinedIcon>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<HasDefinedIcon>k__BackingField = value;
			}
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00002A03 File Offset: 0x00000C03
		public InfoBoxAttribute(string message, InfoMessageType infoMessageType = InfoMessageType.Info, string visibleIfMemberName = null)
		{
			this.Message = message;
			this.InfoMessageType = infoMessageType;
			this.VisibleIf = visibleIfMemberName;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00002A20 File Offset: 0x00000C20
		public InfoBoxAttribute(string message, string visibleIfMemberName)
		{
			this.Message = message;
			this.InfoMessageType = InfoMessageType.Info;
			this.VisibleIf = visibleIfMemberName;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00002A3D File Offset: 0x00000C3D
		public InfoBoxAttribute(string message, SdfIconType icon, string visibleIfMemberName = null)
		{
			this.Message = message;
			this.Icon = icon;
			this.VisibleIf = visibleIfMemberName;
			this.InfoMessageType = InfoMessageType.None;
		}

		// Token: 0x04000073 RID: 115
		public string Message;

		// Token: 0x04000074 RID: 116
		public InfoMessageType InfoMessageType;

		// Token: 0x04000075 RID: 117
		public string VisibleIf;

		// Token: 0x04000076 RID: 118
		public bool GUIAlwaysEnabled;

		// Token: 0x04000077 RID: 119
		public string IconColor;

		// Token: 0x04000078 RID: 120
		[CompilerGenerated]
		private bool <HasDefinedIcon>k__BackingField;

		// Token: 0x04000079 RID: 121
		private SdfIconType icon;
	}
}
