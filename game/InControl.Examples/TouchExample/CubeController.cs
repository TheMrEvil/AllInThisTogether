using System;
using InControl;
using UnityEngine;

namespace TouchExample
{
	// Token: 0x02000004 RID: 4
	public class CubeController : MonoBehaviour
	{
		// Token: 0x0600000D RID: 13 RVA: 0x000024B8 File Offset: 0x000006B8
		private void Start()
		{
			this.cachedRenderer = base.GetComponent<Renderer>();
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000024C8 File Offset: 0x000006C8
		private void Update()
		{
			InputDevice activeDevice = InputManager.ActiveDevice;
			if (activeDevice != InputDevice.Null && activeDevice != TouchManager.Device)
			{
				TouchManager.ControlsEnabled = false;
			}
			this.cachedRenderer.material.color = CubeController.GetColorFromActionButtons(activeDevice);
			base.transform.Rotate(Vector3.down, 500f * Time.deltaTime * activeDevice.Direction.X, Space.World);
			base.transform.Rotate(Vector3.right, 500f * Time.deltaTime * activeDevice.Direction.Y, Space.World);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002558 File Offset: 0x00000758
		private static Color GetColorFromActionButtons(InputDevice inputDevice)
		{
			if (inputDevice.Action1)
			{
				return Color.green;
			}
			if (inputDevice.Action2)
			{
				return Color.red;
			}
			if (inputDevice.Action3)
			{
				return Color.blue;
			}
			if (inputDevice.Action4)
			{
				return Color.yellow;
			}
			return Color.white;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000025B8 File Offset: 0x000007B8
		private void OnGUI()
		{
			float num = 10f;
			int touchCount = TouchManager.TouchCount;
			for (int i = 0; i < touchCount; i++)
			{
				InControl.Touch touch = TouchManager.GetTouch(i);
				string text = i.ToString() + ": fingerId = " + touch.fingerId.ToString();
				text = text + ", phase = " + touch.phase.ToString();
				string str = text;
				string str2 = ", startPosition = ";
				Vector2 vector = touch.startPosition;
				text = str + str2 + vector.ToString();
				string str3 = text;
				string str4 = ", position = ";
				vector = touch.position;
				text = str3 + str4 + vector.ToString();
				if (touch.IsMouse)
				{
					text = text + ", mouseButton = " + touch.mouseButton.ToString();
				}
				GUI.Label(new Rect(10f, num, (float)Screen.width, num + 15f), text);
				num += 20f;
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000026B5 File Offset: 0x000008B5
		public CubeController()
		{
		}

		// Token: 0x0400000A RID: 10
		private Renderer cachedRenderer;
	}
}
