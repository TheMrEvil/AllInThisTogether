using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000168 RID: 360
	public class RepeatButton : TextElement
	{
		// Token: 0x06000B6D RID: 2925 RVA: 0x0002EE10 File Offset: 0x0002D010
		public RepeatButton()
		{
			base.AddToClassList(RepeatButton.ussClassName);
		}

		// Token: 0x06000B6E RID: 2926 RVA: 0x0002EE26 File Offset: 0x0002D026
		public RepeatButton(Action clickEvent, long delay, long interval) : this()
		{
			this.SetAction(clickEvent, delay, interval);
		}

		// Token: 0x06000B6F RID: 2927 RVA: 0x0002EE3A File Offset: 0x0002D03A
		public void SetAction(Action clickEvent, long delay, long interval)
		{
			this.RemoveManipulator(this.m_Clickable);
			this.m_Clickable = new Clickable(clickEvent, delay, interval);
			this.AddManipulator(this.m_Clickable);
		}

		// Token: 0x06000B70 RID: 2928 RVA: 0x0002EE65 File Offset: 0x0002D065
		internal void AddAction(Action clickEvent)
		{
			this.m_Clickable.clicked += clickEvent;
		}

		// Token: 0x06000B71 RID: 2929 RVA: 0x0002EE75 File Offset: 0x0002D075
		// Note: this type is marked as 'beforefieldinit'.
		static RepeatButton()
		{
		}

		// Token: 0x0400053A RID: 1338
		private Clickable m_Clickable;

		// Token: 0x0400053B RID: 1339
		public new static readonly string ussClassName = "unity-repeat-button";

		// Token: 0x02000169 RID: 361
		public new class UxmlFactory : UxmlFactory<RepeatButton, RepeatButton.UxmlTraits>
		{
			// Token: 0x06000B72 RID: 2930 RVA: 0x0002EE81 File Offset: 0x0002D081
			public UxmlFactory()
			{
			}
		}

		// Token: 0x0200016A RID: 362
		public new class UxmlTraits : TextElement.UxmlTraits
		{
			// Token: 0x06000B73 RID: 2931 RVA: 0x0002EE8C File Offset: 0x0002D08C
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				RepeatButton repeatButton = (RepeatButton)ve;
				repeatButton.SetAction(null, this.m_Delay.GetValueFromBag(bag, cc), this.m_Interval.GetValueFromBag(bag, cc));
			}

			// Token: 0x06000B74 RID: 2932 RVA: 0x0002EECD File Offset: 0x0002D0CD
			public UxmlTraits()
			{
			}

			// Token: 0x0400053C RID: 1340
			private UxmlLongAttributeDescription m_Delay = new UxmlLongAttributeDescription
			{
				name = "delay"
			};

			// Token: 0x0400053D RID: 1341
			private UxmlLongAttributeDescription m_Interval = new UxmlLongAttributeDescription
			{
				name = "interval"
			};
		}
	}
}
