using System;
using System.Runtime.CompilerServices;
using InControl;
using UnityEngine;

namespace VirtualDeviceExample
{
	// Token: 0x02000003 RID: 3
	public class VirtualDeviceExample : MonoBehaviour
	{
		// Token: 0x06000008 RID: 8 RVA: 0x00002348 File Offset: 0x00000548
		private void OnEnable()
		{
			this.virtualDevice = new VirtualDevice();
			InputManager.OnSetup += delegate()
			{
				InputManager.AttachDevice(this.virtualDevice);
			};
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002366 File Offset: 0x00000566
		private void OnDisable()
		{
			InputManager.DetachDevice(this.virtualDevice);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002374 File Offset: 0x00000574
		private void Update()
		{
			InputDevice activeDevice = InputManager.ActiveDevice;
			this.leftObject.transform.Rotate(Vector3.down, 500f * Time.deltaTime * activeDevice.LeftStickX, Space.World);
			this.leftObject.transform.Rotate(Vector3.right, 500f * Time.deltaTime * activeDevice.LeftStickY, Space.World);
			this.rightObject.transform.Rotate(Vector3.down, 500f * Time.deltaTime * activeDevice.RightStickX, Space.World);
			this.rightObject.transform.Rotate(Vector3.right, 500f * Time.deltaTime * activeDevice.RightStickY, Space.World);
			Color color = Color.white;
			if (activeDevice.Action1.IsPressed)
			{
				color = Color.green;
			}
			if (activeDevice.Action2.IsPressed)
			{
				color = Color.red;
			}
			if (activeDevice.Action3.IsPressed)
			{
				color = Color.blue;
			}
			if (activeDevice.Action4.IsPressed)
			{
				color = Color.yellow;
			}
			this.leftObject.GetComponent<Renderer>().material.color = color;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000024A3 File Offset: 0x000006A3
		public VirtualDeviceExample()
		{
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000024AB File Offset: 0x000006AB
		[CompilerGenerated]
		private void <OnEnable>b__3_0()
		{
			InputManager.AttachDevice(this.virtualDevice);
		}

		// Token: 0x04000007 RID: 7
		public GameObject leftObject;

		// Token: 0x04000008 RID: 8
		public GameObject rightObject;

		// Token: 0x04000009 RID: 9
		private VirtualDevice virtualDevice;
	}
}
