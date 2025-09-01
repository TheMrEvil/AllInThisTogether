using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Sirenix.OdinInspector
{
	// Token: 0x0200003E RID: 62
	[AttributeUsage(AttributeTargets.All)]
	[Conditional("UNITY_EDITOR")]
	public class InlineEditorAttribute : Attribute
	{
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x00002AA5 File Offset: 0x00000CA5
		// (set) Token: 0x060000A6 RID: 166 RVA: 0x00002AAD File Offset: 0x00000CAD
		public bool Expanded
		{
			get
			{
				return this.expanded;
			}
			set
			{
				this.expanded = value;
				this.ExpandedHasValue = true;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x00002ABD File Offset: 0x00000CBD
		// (set) Token: 0x060000A8 RID: 168 RVA: 0x00002AC5 File Offset: 0x00000CC5
		public bool ExpandedHasValue
		{
			[CompilerGenerated]
			get
			{
				return this.<ExpandedHasValue>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<ExpandedHasValue>k__BackingField = value;
			}
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00002AD0 File Offset: 0x00000CD0
		public InlineEditorAttribute(InlineEditorModes inlineEditorMode = InlineEditorModes.GUIOnly, InlineEditorObjectFieldModes objectFieldMode = InlineEditorObjectFieldModes.Boxed)
		{
			this.ObjectFieldMode = objectFieldMode;
			switch (inlineEditorMode)
			{
			case InlineEditorModes.GUIOnly:
				this.DrawGUI = true;
				return;
			case InlineEditorModes.GUIAndHeader:
				this.DrawGUI = true;
				this.DrawHeader = true;
				return;
			case InlineEditorModes.GUIAndPreview:
				this.DrawGUI = true;
				this.DrawPreview = true;
				return;
			case InlineEditorModes.SmallPreview:
				this.expanded = true;
				this.DrawPreview = true;
				return;
			case InlineEditorModes.LargePreview:
				this.expanded = true;
				this.DrawPreview = true;
				this.PreviewHeight = 170f;
				return;
			case InlineEditorModes.FullEditor:
				this.DrawGUI = true;
				this.DrawHeader = true;
				this.DrawPreview = true;
				return;
			default:
				throw new NotImplementedException();
			}
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00002B9F File Offset: 0x00000D9F
		public InlineEditorAttribute(InlineEditorObjectFieldModes objectFieldMode) : this(InlineEditorModes.GUIOnly, objectFieldMode)
		{
		}

		// Token: 0x04000081 RID: 129
		private bool expanded;

		// Token: 0x04000082 RID: 130
		public bool DrawHeader;

		// Token: 0x04000083 RID: 131
		public bool DrawGUI;

		// Token: 0x04000084 RID: 132
		public bool DrawPreview;

		// Token: 0x04000085 RID: 133
		public float MaxHeight;

		// Token: 0x04000086 RID: 134
		public float PreviewWidth = 100f;

		// Token: 0x04000087 RID: 135
		public float PreviewHeight = 35f;

		// Token: 0x04000088 RID: 136
		public bool IncrementInlineEditorDrawerDepth = true;

		// Token: 0x04000089 RID: 137
		public InlineEditorObjectFieldModes ObjectFieldMode;

		// Token: 0x0400008A RID: 138
		public bool DisableGUIForVCSLockedAssets = true;

		// Token: 0x0400008B RID: 139
		public PreviewAlignment PreviewAlignment = PreviewAlignment.Right;

		// Token: 0x0400008C RID: 140
		[CompilerGenerated]
		private bool <ExpandedHasValue>k__BackingField;
	}
}
