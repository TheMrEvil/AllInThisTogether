using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace QFSW.QC
{
	// Token: 0x0200002F RID: 47
	public class QuantumKeyConfig : ScriptableObject
	{
		// Token: 0x06000123 RID: 291 RVA: 0x00006ADC File Offset: 0x00004CDC
		public QuantumKeyConfig()
		{
		}

		// Token: 0x040000D7 RID: 215
		public KeyCode SubmitCommandKey = KeyCode.Return;

		// Token: 0x040000D8 RID: 216
		public ModifierKeyCombo ShowConsoleKey = KeyCode.None;

		// Token: 0x040000D9 RID: 217
		public ModifierKeyCombo HideConsoleKey = KeyCode.None;

		// Token: 0x040000DA RID: 218
		public ModifierKeyCombo ToggleConsoleVisibilityKey = KeyCode.Escape;

		// Token: 0x040000DB RID: 219
		public ModifierKeyCombo ZoomInKey = new ModifierKeyCombo
		{
			Key = KeyCode.Equals,
			Ctrl = true
		};

		// Token: 0x040000DC RID: 220
		public ModifierKeyCombo ZoomOutKey = new ModifierKeyCombo
		{
			Key = KeyCode.Minus,
			Ctrl = true
		};

		// Token: 0x040000DD RID: 221
		public ModifierKeyCombo DragConsoleKey = new ModifierKeyCombo
		{
			Key = KeyCode.Mouse0,
			Shift = true
		};

		// Token: 0x040000DE RID: 222
		[FormerlySerializedAs("SuggestNextCommandKey")]
		public ModifierKeyCombo SelectNextSuggestionKey = KeyCode.Tab;

		// Token: 0x040000DF RID: 223
		[FormerlySerializedAs("SuggestPreviousCommandKey")]
		public ModifierKeyCombo SelectPreviousSuggestionKey = new ModifierKeyCombo
		{
			Key = KeyCode.Tab,
			Shift = true
		};

		// Token: 0x040000E0 RID: 224
		public KeyCode NextCommandKey = KeyCode.UpArrow;

		// Token: 0x040000E1 RID: 225
		public KeyCode PreviousCommandKey = KeyCode.DownArrow;

		// Token: 0x040000E2 RID: 226
		public ModifierKeyCombo CancelActionsKey = new ModifierKeyCombo
		{
			Key = KeyCode.C,
			Ctrl = true
		};
	}
}
