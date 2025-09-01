using System;

namespace Sirenix.OdinInspector
{
	// Token: 0x0200007F RID: 127
	public struct ValueDropdownItem : IValueDropdownItem
	{
		// Token: 0x060001A5 RID: 421 RVA: 0x00004090 File Offset: 0x00002290
		public ValueDropdownItem(string text, object value)
		{
			this.Text = text;
			this.Value = value;
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x000040A0 File Offset: 0x000022A0
		public override string ToString()
		{
			string result;
			if ((result = this.Text) == null)
			{
				object value = this.Value;
				result = (((value != null) ? value.ToString() : null) ?? "");
			}
			return result;
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x000040C7 File Offset: 0x000022C7
		string IValueDropdownItem.GetText()
		{
			return this.Text;
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x000040CF File Offset: 0x000022CF
		object IValueDropdownItem.GetValue()
		{
			return this.Value;
		}

		// Token: 0x0400025D RID: 605
		public string Text;

		// Token: 0x0400025E RID: 606
		public object Value;
	}
}
