using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200012C RID: 300
	public class Button : TextElement
	{
		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000A18 RID: 2584 RVA: 0x0002862C File Offset: 0x0002682C
		// (set) Token: 0x06000A19 RID: 2585 RVA: 0x00028644 File Offset: 0x00026844
		public Clickable clickable
		{
			get
			{
				return this.m_Clickable;
			}
			set
			{
				bool flag = this.m_Clickable != null && this.m_Clickable.target == this;
				if (flag)
				{
					this.RemoveManipulator(this.m_Clickable);
				}
				this.m_Clickable = value;
				bool flag2 = this.m_Clickable != null;
				if (flag2)
				{
					this.AddManipulator(this.m_Clickable);
				}
			}
		}

		// Token: 0x14000021 RID: 33
		// (add) Token: 0x06000A1A RID: 2586 RVA: 0x000286A1 File Offset: 0x000268A1
		// (remove) Token: 0x06000A1B RID: 2587 RVA: 0x000286AC File Offset: 0x000268AC
		[Obsolete("onClick is obsolete. Use clicked instead (UnityUpgradable) -> clicked", true)]
		public event Action onClick
		{
			add
			{
				this.clicked += value;
			}
			remove
			{
				this.clicked -= value;
			}
		}

		// Token: 0x14000022 RID: 34
		// (add) Token: 0x06000A1C RID: 2588 RVA: 0x000286B8 File Offset: 0x000268B8
		// (remove) Token: 0x06000A1D RID: 2589 RVA: 0x000286F4 File Offset: 0x000268F4
		public event Action clicked
		{
			add
			{
				bool flag = this.m_Clickable == null;
				if (flag)
				{
					this.clickable = new Clickable(value);
				}
				else
				{
					this.m_Clickable.clicked += value;
				}
			}
			remove
			{
				bool flag = this.m_Clickable != null;
				if (flag)
				{
					this.m_Clickable.clicked -= value;
				}
			}
		}

		// Token: 0x06000A1E RID: 2590 RVA: 0x0002871E File Offset: 0x0002691E
		public Button() : this(null)
		{
		}

		// Token: 0x06000A1F RID: 2591 RVA: 0x0002872C File Offset: 0x0002692C
		public Button(Action clickEvent)
		{
			base.AddToClassList(Button.ussClassName);
			this.clickable = new Clickable(clickEvent);
			base.focusable = true;
			base.RegisterCallback<NavigationSubmitEvent>(new EventCallback<NavigationSubmitEvent>(this.OnNavigationSubmit), TrickleDown.NoTrickleDown);
			base.RegisterCallback<KeyDownEvent>(new EventCallback<KeyDownEvent>(this.OnKeyDown), TrickleDown.NoTrickleDown);
		}

		// Token: 0x06000A20 RID: 2592 RVA: 0x0002878A File Offset: 0x0002698A
		private void OnNavigationSubmit(NavigationSubmitEvent evt)
		{
			Clickable clickable = this.clickable;
			if (clickable != null)
			{
				clickable.SimulateSingleClick(evt, 100);
			}
			evt.StopPropagation();
		}

		// Token: 0x06000A21 RID: 2593 RVA: 0x000287AC File Offset: 0x000269AC
		private void OnKeyDown(KeyDownEvent evt)
		{
			IPanel panel = base.panel;
			bool flag = panel == null || panel.contextType != ContextType.Editor;
			if (!flag)
			{
				bool flag2 = evt.keyCode == KeyCode.KeypadEnter || evt.keyCode == KeyCode.Return || evt.keyCode == KeyCode.Space;
				if (flag2)
				{
					Clickable clickable = this.clickable;
					if (clickable != null)
					{
						clickable.SimulateSingleClick(evt, 100);
					}
					evt.StopPropagation();
				}
			}
		}

		// Token: 0x06000A22 RID: 2594 RVA: 0x00028820 File Offset: 0x00026A20
		protected internal override Vector2 DoMeasure(float desiredWidth, VisualElement.MeasureMode widthMode, float desiredHeight, VisualElement.MeasureMode heightMode)
		{
			string text = this.text;
			bool flag = string.IsNullOrEmpty(text);
			if (flag)
			{
				text = Button.NonEmptyString;
			}
			return base.MeasureTextSize(text, desiredWidth, widthMode, desiredHeight, heightMode);
		}

		// Token: 0x06000A23 RID: 2595 RVA: 0x00028857 File Offset: 0x00026A57
		// Note: this type is marked as 'beforefieldinit'.
		static Button()
		{
		}

		// Token: 0x0400044E RID: 1102
		public new static readonly string ussClassName = "unity-button";

		// Token: 0x0400044F RID: 1103
		private Clickable m_Clickable;

		// Token: 0x04000450 RID: 1104
		private static readonly string NonEmptyString = " ";

		// Token: 0x0200012D RID: 301
		public new class UxmlFactory : UxmlFactory<Button, Button.UxmlTraits>
		{
			// Token: 0x06000A24 RID: 2596 RVA: 0x0002886D File Offset: 0x00026A6D
			public UxmlFactory()
			{
			}
		}

		// Token: 0x0200012E RID: 302
		public new class UxmlTraits : TextElement.UxmlTraits
		{
			// Token: 0x06000A25 RID: 2597 RVA: 0x00028876 File Offset: 0x00026A76
			public UxmlTraits()
			{
				base.focusable.defaultValue = true;
			}
		}
	}
}
