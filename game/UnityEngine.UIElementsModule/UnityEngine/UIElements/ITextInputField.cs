using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000185 RID: 389
	internal interface ITextInputField : IEventHandler, ITextElement
	{
		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06000C3F RID: 3135
		bool hasFocus { get; }

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000C40 RID: 3136
		bool doubleClickSelectsWord { get; }

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000C41 RID: 3137
		bool tripleClickSelectsLine { get; }

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000C42 RID: 3138
		bool isReadOnly { get; }

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000C43 RID: 3139
		bool isDelayed { get; }

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000C44 RID: 3140
		bool isPasswordField { get; }

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000C45 RID: 3141
		TextEditorEngine editorEngine { get; }

		// Token: 0x06000C46 RID: 3142
		void SyncTextEngine();

		// Token: 0x06000C47 RID: 3143
		bool AcceptCharacter(char c);

		// Token: 0x06000C48 RID: 3144
		string CullString(string s);

		// Token: 0x06000C49 RID: 3145
		void UpdateText(string value);

		// Token: 0x06000C4A RID: 3146
		void UpdateValueFromText();
	}
}
