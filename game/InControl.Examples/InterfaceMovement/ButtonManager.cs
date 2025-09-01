using System;
using InControl;
using UnityEngine;

namespace InterfaceMovement
{
	// Token: 0x02000007 RID: 7
	public class ButtonManager : MonoBehaviour
	{
		// Token: 0x06000017 RID: 23 RVA: 0x000027A8 File Offset: 0x000009A8
		private void Awake()
		{
			this.filteredDirection = new TwoAxisInputControl();
			this.filteredDirection.StateThreshold = 0.5f;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000027C8 File Offset: 0x000009C8
		private void Update()
		{
			InputDevice activeDevice = InputManager.ActiveDevice;
			this.filteredDirection.Filter(activeDevice.Direction, Time.deltaTime);
			if (this.filteredDirection.Left.WasRepeated)
			{
				Debug.Log("!!!");
			}
			if (this.filteredDirection.Up.WasPressed)
			{
				this.MoveFocusTo(this.focusedButton.up);
			}
			if (this.filteredDirection.Down.WasPressed)
			{
				this.MoveFocusTo(this.focusedButton.down);
			}
			if (this.filteredDirection.Left.WasPressed)
			{
				this.MoveFocusTo(this.focusedButton.left);
			}
			if (this.filteredDirection.Right.WasPressed)
			{
				this.MoveFocusTo(this.focusedButton.right);
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002899 File Offset: 0x00000A99
		private void MoveFocusTo(Button newFocusedButton)
		{
			if (newFocusedButton != null)
			{
				this.focusedButton = newFocusedButton;
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000028AB File Offset: 0x00000AAB
		public ButtonManager()
		{
		}

		// Token: 0x04000010 RID: 16
		public Button focusedButton;

		// Token: 0x04000011 RID: 17
		private TwoAxisInputControl filteredDirection;
	}
}
