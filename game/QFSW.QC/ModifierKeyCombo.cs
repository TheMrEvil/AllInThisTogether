using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace QFSW.QC
{
	// Token: 0x0200001A RID: 26
	[Serializable]
	public struct ModifierKeyCombo
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600005B RID: 91 RVA: 0x000030F8 File Offset: 0x000012F8
		public bool ModifiersActive
		{
			get
			{
				bool flag = !this.Ctrl ^ (InputHelper.GetKey(KeyCode.LeftControl) || InputHelper.GetKey(KeyCode.RightControl) || InputHelper.GetKey(KeyCode.LeftMeta) || InputHelper.GetKey(KeyCode.RightMeta));
				bool flag2 = !this.Alt ^ (InputHelper.GetKey(KeyCode.LeftAlt) || InputHelper.GetKey(KeyCode.RightAlt));
				bool flag3 = !this.Shift ^ (InputHelper.GetKey(KeyCode.LeftShift) || InputHelper.GetKey(KeyCode.RightShift));
				return flag && flag2 && flag3;
			}
		}

		// Token: 0x0600005C RID: 92 RVA: 0x0000318C File Offset: 0x0000138C
		public bool IsHeld()
		{
			return this.ModifiersActive && InputHelper.GetKey(this.Key);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x000031A3 File Offset: 0x000013A3
		public bool IsPressed()
		{
			return this.ModifiersActive && InputHelper.GetKeyDown(this.Key);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x000031BC File Offset: 0x000013BC
		public static implicit operator ModifierKeyCombo(KeyCode key)
		{
			return new ModifierKeyCombo
			{
				Key = key
			};
		}

		// Token: 0x04000034 RID: 52
		[FormerlySerializedAs("key")]
		public KeyCode Key;

		// Token: 0x04000035 RID: 53
		[FormerlySerializedAs("ctrl")]
		public bool Ctrl;

		// Token: 0x04000036 RID: 54
		[FormerlySerializedAs("alt")]
		public bool Alt;

		// Token: 0x04000037 RID: 55
		[FormerlySerializedAs("shift")]
		public bool Shift;
	}
}
