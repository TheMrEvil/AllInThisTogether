using System;
using InControl;
using UnityEngine;

namespace BasicExample
{
	// Token: 0x0200000A RID: 10
	public class BasicExample : MonoBehaviour
	{
		// Token: 0x06000026 RID: 38 RVA: 0x00002FE0 File Offset: 0x000011E0
		private void Update()
		{
			InputDevice activeDevice = InputManager.ActiveDevice;
			base.transform.Rotate(Vector3.down, 500f * Time.deltaTime * activeDevice.LeftStickX, Space.World);
			base.transform.Rotate(Vector3.right, 500f * Time.deltaTime * activeDevice.LeftStickY, Space.World);
			Color a = activeDevice.Action1.IsPressed ? Color.red : Color.white;
			Color b = activeDevice.Action2.IsPressed ? Color.green : Color.white;
			base.GetComponent<Renderer>().material.color = Color.Lerp(a, b, 0.5f);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00003093 File Offset: 0x00001293
		public BasicExample()
		{
		}
	}
}
