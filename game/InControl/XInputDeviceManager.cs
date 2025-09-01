using System;
using System.Collections.Generic;
using System.Threading;
using InControl.Internal;
using InControl.UnityDeviceProfiles;
using UnityEngine;
using XInputDotNetPure;

namespace InControl
{
	// Token: 0x02000074 RID: 116
	public class XInputDeviceManager : InputDeviceManager
	{
		// Token: 0x0600059A RID: 1434 RVA: 0x00013AA4 File Offset: 0x00011CA4
		public XInputDeviceManager()
		{
			if (InputManager.XInputUpdateRate == 0U)
			{
				this.timeStep = Mathf.FloorToInt(Time.fixedDeltaTime * 1000f);
			}
			else
			{
				this.timeStep = Mathf.FloorToInt(1f / InputManager.XInputUpdateRate * 1000f);
			}
			this.bufferSize = (int)Math.Max(InputManager.XInputBufferSize, 1U);
			for (int i = 0; i < 4; i++)
			{
				this.gamePadState[i] = new RingBuffer<GamePadState>(this.bufferSize);
			}
			this.StartWorker();
			for (int j = 0; j < 4; j++)
			{
				this.devices.Add(new XInputDevice(j, this));
			}
			this.Update(0UL, 0f);
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x00013B6D File Offset: 0x00011D6D
		private void StartWorker()
		{
			if (this.thread == null)
			{
				this.thread = new Thread(new ThreadStart(this.Worker));
				this.thread.IsBackground = true;
				this.thread.Start();
			}
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x00013BA5 File Offset: 0x00011DA5
		private void StopWorker()
		{
			if (this.thread != null)
			{
				this.thread.Abort();
				this.thread.Join();
				this.thread = null;
			}
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x00013BCC File Offset: 0x00011DCC
		private void Worker()
		{
			for (;;)
			{
				for (int i = 0; i < 4; i++)
				{
					this.gamePadState[i].Enqueue(GamePad.GetState((PlayerIndex)i));
				}
				Thread.Sleep(this.timeStep);
			}
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x00013C04 File Offset: 0x00011E04
		internal GamePadState GetState(int deviceIndex)
		{
			return this.gamePadState[deviceIndex].Dequeue();
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x00013C14 File Offset: 0x00011E14
		public override void Update(ulong updateTick, float deltaTime)
		{
			for (int i = 0; i < 4; i++)
			{
				XInputDevice xinputDevice = this.devices[i] as XInputDevice;
				if (!xinputDevice.IsConnected)
				{
					xinputDevice.GetState();
				}
				if (xinputDevice.IsConnected != this.deviceConnected[i])
				{
					if (xinputDevice.IsConnected)
					{
						InputManager.AttachDevice(xinputDevice);
					}
					else
					{
						InputManager.DetachDevice(xinputDevice);
					}
					this.deviceConnected[i] = xinputDevice.IsConnected;
				}
			}
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x00013C81 File Offset: 0x00011E81
		public override void Destroy()
		{
			this.StopWorker();
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x00013C8C File Offset: 0x00011E8C
		public static bool CheckPlatformSupport(ICollection<string> errors)
		{
			if (Application.platform != RuntimePlatform.WindowsPlayer && Application.platform != RuntimePlatform.WindowsEditor)
			{
				return false;
			}
			try
			{
				GamePad.GetState(PlayerIndex.One);
			}
			catch (DllNotFoundException ex)
			{
				if (errors != null)
				{
					errors.Add(ex.Message + ".dll could not be found or is missing a dependency.");
				}
				return false;
			}
			return true;
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x00013CE8 File Offset: 0x00011EE8
		internal static void Enable()
		{
			List<string> list = new List<string>();
			if (XInputDeviceManager.CheckPlatformSupport(list))
			{
				InputManager.HideDevicesWithProfile(typeof(Xbox360WindowsUnityProfile));
				InputManager.HideDevicesWithProfile(typeof(XboxOneWindowsUnityProfile));
				InputManager.HideDevicesWithProfile(typeof(XboxOneWindows10UnityProfile));
				InputManager.HideDevicesWithProfile(typeof(XboxOneWindows10AEUnityProfile));
				InputManager.HideDevicesWithProfile(typeof(LogitechF310ModeXWindowsUnityProfile));
				InputManager.HideDevicesWithProfile(typeof(LogitechF510ModeXWindowsUnityProfile));
				InputManager.HideDevicesWithProfile(typeof(LogitechF710ModeXWindowsUnityProfile));
				InputManager.AddDeviceManager<XInputDeviceManager>();
				return;
			}
			foreach (string text in list)
			{
				Logger.LogError(text);
			}
		}

		// Token: 0x04000425 RID: 1061
		private readonly bool[] deviceConnected = new bool[4];

		// Token: 0x04000426 RID: 1062
		private const int maxDevices = 4;

		// Token: 0x04000427 RID: 1063
		private readonly RingBuffer<GamePadState>[] gamePadState = new RingBuffer<GamePadState>[4];

		// Token: 0x04000428 RID: 1064
		private Thread thread;

		// Token: 0x04000429 RID: 1065
		private readonly int timeStep;

		// Token: 0x0400042A RID: 1066
		private int bufferSize;
	}
}
