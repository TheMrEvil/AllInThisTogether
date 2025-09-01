using System;

namespace InControl
{
	// Token: 0x02000018 RID: 24
	public class MouseBindingSourceListener : BindingSourceListener
	{
		// Token: 0x0600009C RID: 156 RVA: 0x0000329A File Offset: 0x0000149A
		public void Reset()
		{
			this.detectFound = Mouse.None;
			this.detectPhase = 0;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x000032AC File Offset: 0x000014AC
		public BindingSource Listen(BindingListenOptions listenOptions, InputDevice device)
		{
			if (this.detectFound != Mouse.None && !this.IsPressed(this.detectFound) && this.detectPhase == 2)
			{
				BindingSource result = new MouseBindingSource(this.detectFound);
				this.Reset();
				return result;
			}
			Mouse mouse = this.ListenForControl(listenOptions);
			if (mouse != Mouse.None)
			{
				if (this.detectPhase == 1)
				{
					this.detectFound = mouse;
					this.detectPhase = 2;
				}
			}
			else if (this.detectPhase == 0)
			{
				this.detectPhase = 1;
			}
			return null;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x0000331E File Offset: 0x0000151E
		private bool IsPressed(Mouse control)
		{
			if (control == Mouse.PositiveScrollWheel)
			{
				return MouseBindingSource.PositiveScrollWheelIsActive(MouseBindingSourceListener.ScrollWheelThreshold);
			}
			if (control == Mouse.NegativeScrollWheel)
			{
				return MouseBindingSource.NegativeScrollWheelIsActive(MouseBindingSourceListener.ScrollWheelThreshold);
			}
			return MouseBindingSource.ButtonIsPressed(control);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00003348 File Offset: 0x00001548
		private Mouse ListenForControl(BindingListenOptions listenOptions)
		{
			if (listenOptions.IncludeMouseButtons)
			{
				for (Mouse mouse = Mouse.None; mouse <= Mouse.Button7; mouse++)
				{
					if (MouseBindingSource.ButtonIsPressed(mouse))
					{
						return mouse;
					}
				}
			}
			if (listenOptions.IncludeMouseScrollWheel)
			{
				if (MouseBindingSource.NegativeScrollWheelIsActive(MouseBindingSourceListener.ScrollWheelThreshold))
				{
					return Mouse.NegativeScrollWheel;
				}
				if (MouseBindingSource.PositiveScrollWheelIsActive(MouseBindingSourceListener.ScrollWheelThreshold))
				{
					return Mouse.PositiveScrollWheel;
				}
			}
			return Mouse.None;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x0000339A File Offset: 0x0000159A
		public MouseBindingSourceListener()
		{
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x000033A2 File Offset: 0x000015A2
		// Note: this type is marked as 'beforefieldinit'.
		static MouseBindingSourceListener()
		{
		}

		// Token: 0x040000E5 RID: 229
		public static float ScrollWheelThreshold = 0.001f;

		// Token: 0x040000E6 RID: 230
		private Mouse detectFound;

		// Token: 0x040000E7 RID: 231
		private int detectPhase;
	}
}
