using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x0200003A RID: 58
	internal class DefaultGroupManager : IGroupManager
	{
		// Token: 0x06000160 RID: 352 RVA: 0x00006AB4 File Offset: 0x00004CB4
		public IGroupBoxOption GetSelectedOption()
		{
			return this.m_SelectedOption;
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00006ACC File Offset: 0x00004CCC
		public void OnOptionSelectionChanged(IGroupBoxOption selectedOption)
		{
			bool flag = this.m_SelectedOption == selectedOption;
			if (!flag)
			{
				this.m_SelectedOption = selectedOption;
				foreach (IGroupBoxOption groupBoxOption in this.m_GroupOptions)
				{
					groupBoxOption.SetSelected(groupBoxOption == this.m_SelectedOption);
				}
			}
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00006B44 File Offset: 0x00004D44
		public void RegisterOption(IGroupBoxOption option)
		{
			bool flag = !this.m_GroupOptions.Contains(option);
			if (flag)
			{
				this.m_GroupOptions.Add(option);
			}
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00006B74 File Offset: 0x00004D74
		public void UnregisterOption(IGroupBoxOption option)
		{
			this.m_GroupOptions.Remove(option);
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00006B84 File Offset: 0x00004D84
		public DefaultGroupManager()
		{
		}

		// Token: 0x04000093 RID: 147
		private List<IGroupBoxOption> m_GroupOptions = new List<IGroupBoxOption>();

		// Token: 0x04000094 RID: 148
		private IGroupBoxOption m_SelectedOption;
	}
}
