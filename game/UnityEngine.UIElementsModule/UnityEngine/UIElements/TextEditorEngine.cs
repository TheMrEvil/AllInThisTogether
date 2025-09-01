using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200017E RID: 382
	internal class TextEditorEngine : TextEditor
	{
		// Token: 0x06000C15 RID: 3093 RVA: 0x00032B6E File Offset: 0x00030D6E
		public TextEditorEngine(TextEditorEngine.OnDetectFocusChangeFunction detectFocusChange, TextEditorEngine.OnIndexChangeFunction indexChangeFunction)
		{
			this.m_DetectFocusChangeFunction = detectFocusChange;
			this.m_IndexChangeFunction = indexChangeFunction;
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000C16 RID: 3094 RVA: 0x00032B88 File Offset: 0x00030D88
		internal override Rect localPosition
		{
			get
			{
				return new Rect(0f, 0f, base.position.width, base.position.height);
			}
		}

		// Token: 0x06000C17 RID: 3095 RVA: 0x00032BC5 File Offset: 0x00030DC5
		internal override void OnDetectFocusChange()
		{
			this.m_DetectFocusChangeFunction();
		}

		// Token: 0x06000C18 RID: 3096 RVA: 0x00032BD4 File Offset: 0x00030DD4
		internal override void OnCursorIndexChange()
		{
			this.m_IndexChangeFunction();
		}

		// Token: 0x06000C19 RID: 3097 RVA: 0x00032BD4 File Offset: 0x00030DD4
		internal override void OnSelectIndexChange()
		{
			this.m_IndexChangeFunction();
		}

		// Token: 0x040005C7 RID: 1479
		private TextEditorEngine.OnDetectFocusChangeFunction m_DetectFocusChangeFunction;

		// Token: 0x040005C8 RID: 1480
		private TextEditorEngine.OnIndexChangeFunction m_IndexChangeFunction;

		// Token: 0x0200017F RID: 383
		// (Invoke) Token: 0x06000C1B RID: 3099
		internal delegate void OnDetectFocusChangeFunction();

		// Token: 0x02000180 RID: 384
		// (Invoke) Token: 0x06000C1F RID: 3103
		internal delegate void OnIndexChangeFunction();
	}
}
