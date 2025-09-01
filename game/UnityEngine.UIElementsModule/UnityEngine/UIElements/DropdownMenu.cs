using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	// Token: 0x02000025 RID: 37
	public class DropdownMenu
	{
		// Token: 0x060000ED RID: 237 RVA: 0x00005194 File Offset: 0x00003394
		public List<DropdownMenuItem> MenuItems()
		{
			return this.m_MenuItems;
		}

		// Token: 0x060000EE RID: 238 RVA: 0x000051AC File Offset: 0x000033AC
		public void AppendAction(string actionName, Action<DropdownMenuAction> action, Func<DropdownMenuAction, DropdownMenuAction.Status> actionStatusCallback, object userData = null)
		{
			DropdownMenuAction item = new DropdownMenuAction(actionName, action, actionStatusCallback, userData);
			this.m_MenuItems.Add(item);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x000051D4 File Offset: 0x000033D4
		public void AppendAction(string actionName, Action<DropdownMenuAction> action, DropdownMenuAction.Status status = DropdownMenuAction.Status.Normal)
		{
			bool flag = status == DropdownMenuAction.Status.Normal;
			if (flag)
			{
				this.AppendAction(actionName, action, new Func<DropdownMenuAction, DropdownMenuAction.Status>(DropdownMenuAction.AlwaysEnabled), null);
			}
			else
			{
				bool flag2 = status == DropdownMenuAction.Status.Disabled;
				if (flag2)
				{
					this.AppendAction(actionName, action, new Func<DropdownMenuAction, DropdownMenuAction.Status>(DropdownMenuAction.AlwaysDisabled), null);
				}
				else
				{
					this.AppendAction(actionName, action, (DropdownMenuAction e) => status, null);
				}
			}
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00005258 File Offset: 0x00003458
		public void InsertAction(int atIndex, string actionName, Action<DropdownMenuAction> action, Func<DropdownMenuAction, DropdownMenuAction.Status> actionStatusCallback, object userData = null)
		{
			DropdownMenuAction item = new DropdownMenuAction(actionName, action, actionStatusCallback, userData);
			this.m_MenuItems.Insert(atIndex, item);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00005280 File Offset: 0x00003480
		public void InsertAction(int atIndex, string actionName, Action<DropdownMenuAction> action, DropdownMenuAction.Status status = DropdownMenuAction.Status.Normal)
		{
			bool flag = status == DropdownMenuAction.Status.Normal;
			if (flag)
			{
				this.InsertAction(atIndex, actionName, action, new Func<DropdownMenuAction, DropdownMenuAction.Status>(DropdownMenuAction.AlwaysEnabled), null);
			}
			else
			{
				bool flag2 = status == DropdownMenuAction.Status.Disabled;
				if (flag2)
				{
					this.InsertAction(atIndex, actionName, action, new Func<DropdownMenuAction, DropdownMenuAction.Status>(DropdownMenuAction.AlwaysDisabled), null);
				}
				else
				{
					this.InsertAction(atIndex, actionName, action, (DropdownMenuAction e) => status, null);
				}
			}
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00005308 File Offset: 0x00003508
		public void AppendSeparator(string subMenuPath = null)
		{
			bool flag = this.m_MenuItems.Count > 0 && !(this.m_MenuItems[this.m_MenuItems.Count - 1] is DropdownMenuSeparator);
			if (flag)
			{
				DropdownMenuSeparator item = new DropdownMenuSeparator(subMenuPath ?? string.Empty);
				this.m_MenuItems.Add(item);
			}
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00005370 File Offset: 0x00003570
		public void InsertSeparator(string subMenuPath, int atIndex)
		{
			bool flag = atIndex > 0 && atIndex <= this.m_MenuItems.Count && !(this.m_MenuItems[atIndex - 1] is DropdownMenuSeparator);
			if (flag)
			{
				DropdownMenuSeparator item = new DropdownMenuSeparator(subMenuPath ?? string.Empty);
				this.m_MenuItems.Insert(atIndex, item);
			}
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x000053D0 File Offset: 0x000035D0
		public void RemoveItemAt(int index)
		{
			this.m_MenuItems.RemoveAt(index);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x000053E0 File Offset: 0x000035E0
		public void PrepareForDisplay(EventBase e)
		{
			this.m_DropdownMenuEventInfo = ((e != null) ? new DropdownMenuEventInfo(e) : null);
			bool flag = this.m_MenuItems.Count == 0;
			if (!flag)
			{
				foreach (DropdownMenuItem dropdownMenuItem in this.m_MenuItems)
				{
					DropdownMenuAction dropdownMenuAction = dropdownMenuItem as DropdownMenuAction;
					bool flag2 = dropdownMenuAction != null;
					if (flag2)
					{
						dropdownMenuAction.UpdateActionStatus(this.m_DropdownMenuEventInfo);
					}
				}
				bool flag3 = this.m_MenuItems[this.m_MenuItems.Count - 1] is DropdownMenuSeparator;
				if (flag3)
				{
					this.m_MenuItems.RemoveAt(this.m_MenuItems.Count - 1);
				}
			}
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x000054BC File Offset: 0x000036BC
		public DropdownMenu()
		{
		}

		// Token: 0x04000067 RID: 103
		private List<DropdownMenuItem> m_MenuItems = new List<DropdownMenuItem>();

		// Token: 0x04000068 RID: 104
		private DropdownMenuEventInfo m_DropdownMenuEventInfo;

		// Token: 0x02000026 RID: 38
		[CompilerGenerated]
		private sealed class <>c__DisplayClass4_0
		{
			// Token: 0x060000F7 RID: 247 RVA: 0x000020C2 File Offset: 0x000002C2
			public <>c__DisplayClass4_0()
			{
			}

			// Token: 0x060000F8 RID: 248 RVA: 0x000054D0 File Offset: 0x000036D0
			internal DropdownMenuAction.Status <AppendAction>b__0(DropdownMenuAction e)
			{
				return this.status;
			}

			// Token: 0x04000069 RID: 105
			public DropdownMenuAction.Status status;
		}

		// Token: 0x02000027 RID: 39
		[CompilerGenerated]
		private sealed class <>c__DisplayClass6_0
		{
			// Token: 0x060000F9 RID: 249 RVA: 0x000020C2 File Offset: 0x000002C2
			public <>c__DisplayClass6_0()
			{
			}

			// Token: 0x060000FA RID: 250 RVA: 0x000054D8 File Offset: 0x000036D8
			internal DropdownMenuAction.Status <InsertAction>b__0(DropdownMenuAction e)
			{
				return this.status;
			}

			// Token: 0x0400006A RID: 106
			public DropdownMenuAction.Status status;
		}
	}
}
