using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	// Token: 0x0200017D RID: 381
	internal class TextEditorEventHandler
	{
		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000C0E RID: 3086 RVA: 0x00032ABF File Offset: 0x00030CBF
		// (set) Token: 0x06000C0F RID: 3087 RVA: 0x00032AC7 File Offset: 0x00030CC7
		private protected TextEditorEngine editorEngine
		{
			[CompilerGenerated]
			protected get
			{
				return this.<editorEngine>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<editorEngine>k__BackingField = value;
			}
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000C10 RID: 3088 RVA: 0x00032AD0 File Offset: 0x00030CD0
		// (set) Token: 0x06000C11 RID: 3089 RVA: 0x00032AD8 File Offset: 0x00030CD8
		private protected ITextInputField textInputField
		{
			[CompilerGenerated]
			protected get
			{
				return this.<textInputField>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<textInputField>k__BackingField = value;
			}
		}

		// Token: 0x06000C12 RID: 3090 RVA: 0x00032AE1 File Offset: 0x00030CE1
		protected TextEditorEventHandler(TextEditorEngine editorEngine, ITextInputField textInputField)
		{
			this.editorEngine = editorEngine;
			this.textInputField = textInputField;
			this.textInputField.SyncTextEngine();
		}

		// Token: 0x06000C13 RID: 3091 RVA: 0x00002166 File Offset: 0x00000366
		public virtual void ExecuteDefaultActionAtTarget(EventBase evt)
		{
		}

		// Token: 0x06000C14 RID: 3092 RVA: 0x00032B08 File Offset: 0x00030D08
		public virtual void ExecuteDefaultAction(EventBase evt)
		{
			bool flag = evt.eventTypeId == EventBase<FocusEvent>.TypeId();
			if (flag)
			{
				this.editorEngine.OnFocus();
				this.editorEngine.SelectAll();
			}
			else
			{
				bool flag2 = evt.eventTypeId == EventBase<BlurEvent>.TypeId();
				if (flag2)
				{
					this.editorEngine.OnLostFocus();
					this.editorEngine.SelectNone();
				}
			}
		}

		// Token: 0x040005C5 RID: 1477
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private TextEditorEngine <editorEngine>k__BackingField;

		// Token: 0x040005C6 RID: 1478
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private ITextInputField <textInputField>k__BackingField;
	}
}
