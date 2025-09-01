using System;

namespace Sirenix.OdinInspector
{
	// Token: 0x02000080 RID: 128
	public struct ValueDropdownItem<T> : IValueDropdownItem
	{
		// Token: 0x060001A9 RID: 425 RVA: 0x000040D7 File Offset: 0x000022D7
		public ValueDropdownItem(string text, T value)
		{
			this.Text = text;
			this.Value = value;
		}

		// Token: 0x060001AA RID: 426 RVA: 0x000040E7 File Offset: 0x000022E7
		string IValueDropdownItem.GetText()
		{
			return this.Text;
		}

		// Token: 0x060001AB RID: 427 RVA: 0x000040EF File Offset: 0x000022EF
		object IValueDropdownItem.GetValue()
		{
			return this.Value;
		}

		// Token: 0x060001AC RID: 428 RVA: 0x000040FC File Offset: 0x000022FC
		public override string ToString()
		{
			string result;
			if ((result = this.Text) == null)
			{
				T value = this.Value;
				result = (((value != null) ? value.ToString() : null) ?? "");
			}
			return result;
		}

		// Token: 0x0400025F RID: 607
		public string Text;

		// Token: 0x04000260 RID: 608
		public T Value;
	}
}
