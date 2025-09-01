using System;
using InControl;
using UnityEngine;

namespace VirtualDeviceExample
{
	// Token: 0x02000002 RID: 2
	public class VirtualDevice : InputDevice
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public VirtualDevice() : base("Virtual Controller")
		{
			base.AddControl(InputControlType.LeftStickLeft, "Left Stick Left");
			base.AddControl(InputControlType.LeftStickRight, "Left Stick Right");
			base.AddControl(InputControlType.LeftStickUp, "Left Stick Up");
			base.AddControl(InputControlType.LeftStickDown, "Left Stick Down");
			base.AddControl(InputControlType.RightStickLeft, "Right Stick Left");
			base.AddControl(InputControlType.RightStickRight, "Right Stick Right");
			base.AddControl(InputControlType.RightStickUp, "Right Stick Up");
			base.AddControl(InputControlType.RightStickDown, "Right Stick Down");
			base.AddControl(InputControlType.Action1, "A");
			base.AddControl(InputControlType.Action2, "B");
			base.AddControl(InputControlType.Action3, "X");
			base.AddControl(InputControlType.Action4, "Y");
		}

		// Token: 0x06000002 RID: 2 RVA: 0x0000210C File Offset: 0x0000030C
		public override void Update(ulong updateTick, float deltaTime)
		{
			Vector2 vectorFromKeyboard = this.GetVectorFromKeyboard(deltaTime, true);
			base.UpdateLeftStickWithValue(vectorFromKeyboard, updateTick, deltaTime);
			Vector2 vectorFromMouse = this.GetVectorFromMouse(deltaTime, true);
			base.UpdateRightStickWithRawValue(vectorFromMouse, updateTick, deltaTime);
			base.UpdateWithState(InputControlType.Action1, Input.GetKey(KeyCode.Space), updateTick, deltaTime);
			base.UpdateWithState(InputControlType.Action2, Input.GetKey(KeyCode.S), updateTick, deltaTime);
			base.UpdateWithState(InputControlType.Action3, Input.GetKey(KeyCode.D), updateTick, deltaTime);
			base.UpdateWithState(InputControlType.Action4, Input.GetKey(KeyCode.F), updateTick, deltaTime);
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002184 File Offset: 0x00000384
		private Vector2 GetVectorFromKeyboard(float deltaTime, bool smoothed)
		{
			if (smoothed)
			{
				this.kx = VirtualDevice.ApplySmoothing(this.kx, VirtualDevice.GetXFromKeyboard(), deltaTime, 0.1f);
				this.ky = VirtualDevice.ApplySmoothing(this.ky, VirtualDevice.GetYFromKeyboard(), deltaTime, 0.1f);
			}
			else
			{
				this.kx = VirtualDevice.GetXFromKeyboard();
				this.ky = VirtualDevice.GetYFromKeyboard();
			}
			return new Vector2(this.kx, this.ky);
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000021F8 File Offset: 0x000003F8
		private static float GetXFromKeyboard()
		{
			float num = Input.GetKey(KeyCode.LeftArrow) ? -1f : 0f;
			float num2 = Input.GetKey(KeyCode.RightArrow) ? 1f : 0f;
			return num + num2;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002238 File Offset: 0x00000438
		private static float GetYFromKeyboard()
		{
			float num = Input.GetKey(KeyCode.UpArrow) ? 1f : 0f;
			float num2 = Input.GetKey(KeyCode.DownArrow) ? -1f : 0f;
			return num + num2;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002278 File Offset: 0x00000478
		private Vector2 GetVectorFromMouse(float deltaTime, bool smoothed)
		{
			if (smoothed)
			{
				this.mx = VirtualDevice.ApplySmoothing(this.mx, Input.GetAxisRaw("mouse x") * 0.05f, deltaTime, 0.1f);
				this.my = VirtualDevice.ApplySmoothing(this.my, Input.GetAxisRaw("mouse y") * 0.05f, deltaTime, 0.1f);
			}
			else
			{
				this.mx = Input.GetAxisRaw("mouse x") * 0.05f;
				this.my = Input.GetAxisRaw("mouse y") * 0.05f;
			}
			return new Vector2(this.mx, this.my);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002315 File Offset: 0x00000515
		private static float ApplySmoothing(float lastValue, float thisValue, float deltaTime, float sensitivity)
		{
			sensitivity = Mathf.Clamp(sensitivity, 0.001f, 1f);
			if (Mathf.Approximately(sensitivity, 1f))
			{
				return thisValue;
			}
			return Mathf.Lerp(lastValue, thisValue, deltaTime * sensitivity * 100f);
		}

		// Token: 0x04000001 RID: 1
		private const float Sensitivity = 0.1f;

		// Token: 0x04000002 RID: 2
		private const float MouseScale = 0.05f;

		// Token: 0x04000003 RID: 3
		private float kx;

		// Token: 0x04000004 RID: 4
		private float ky;

		// Token: 0x04000005 RID: 5
		private float mx;

		// Token: 0x04000006 RID: 6
		private float my;
	}
}
