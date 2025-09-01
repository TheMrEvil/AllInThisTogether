using System;
using UnityEngine;

namespace SlimUI.CursorControllerPro.InputSystem
{
	// Token: 0x02000011 RID: 17
	public interface IInputProvider
	{
		// Token: 0x06000055 RID: 85
		InputType GetActiveInputType();

		// Token: 0x06000056 RID: 86
		Vector2 GetAbsolutePosition();

		// Token: 0x06000057 RID: 87
		Vector2 GetRelativeMovement(GamepadPlayerNum player = GamepadPlayerNum.One);

		// Token: 0x06000058 RID: 88
		bool GetSubmitWasPressed();

		// Token: 0x06000059 RID: 89
		bool GetSubmitWasReleased();
	}
}
