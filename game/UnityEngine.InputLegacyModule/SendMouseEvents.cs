using System;
using System.Collections.Generic;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000010 RID: 16
	internal class SendMouseEvents
	{
		// Token: 0x060000A6 RID: 166 RVA: 0x0000289C File Offset: 0x00000A9C
		private static void UpdateMouse()
		{
			bool flag = SendMouseEvents.s_GetMouseState != null;
			if (flag)
			{
				KeyValuePair<int, Vector2> keyValuePair = SendMouseEvents.s_GetMouseState();
				SendMouseEvents.s_MousePosition = keyValuePair.Value;
				SendMouseEvents.s_MouseButtonPressedThisFrame = (keyValuePair.Key == 2);
				SendMouseEvents.s_MouseButtonIsPressed = (keyValuePair.Key != 0);
			}
			else
			{
				bool flag2 = !Input.CheckDisabled();
				if (flag2)
				{
					SendMouseEvents.s_MousePosition = Input.mousePosition;
					SendMouseEvents.s_MouseButtonPressedThisFrame = Input.GetMouseButtonDown(0);
					SendMouseEvents.s_MouseButtonIsPressed = Input.GetMouseButton(0);
				}
				else
				{
					SendMouseEvents.s_MousePosition = default(Vector2);
					SendMouseEvents.s_MouseButtonPressedThisFrame = false;
					SendMouseEvents.s_MouseButtonIsPressed = false;
				}
			}
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x0000293D File Offset: 0x00000B3D
		[RequiredByNativeCode]
		private static void SetMouseMoved()
		{
			SendMouseEvents.s_MouseUsed = true;
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00002948 File Offset: 0x00000B48
		[RequiredByNativeCode]
		private static void DoSendMouseEvents(int skipRTCameras)
		{
			SendMouseEvents.UpdateMouse();
			Vector2 v = SendMouseEvents.s_MousePosition;
			int allCamerasCount = Camera.allCamerasCount;
			bool flag = SendMouseEvents.m_Cameras == null || SendMouseEvents.m_Cameras.Length != allCamerasCount;
			if (flag)
			{
				SendMouseEvents.m_Cameras = new Camera[allCamerasCount];
			}
			Camera.GetAllCameras(SendMouseEvents.m_Cameras);
			for (int i = 0; i < SendMouseEvents.m_CurrentHit.Length; i++)
			{
				SendMouseEvents.m_CurrentHit[i] = default(SendMouseEvents.HitInfo);
			}
			bool flag2 = !SendMouseEvents.s_MouseUsed;
			if (flag2)
			{
				foreach (Camera camera in SendMouseEvents.m_Cameras)
				{
					bool flag3 = camera == null || (skipRTCameras != 0 && camera.targetTexture != null);
					if (!flag3)
					{
						int targetDisplay = camera.targetDisplay;
						Vector3 vector = Display.RelativeMouseAt(v);
						bool flag4 = vector != Vector3.zero;
						if (flag4)
						{
							int num = (int)vector.z;
							bool flag5 = num != targetDisplay;
							if (flag5)
							{
								goto IL_368;
							}
							float num2 = (float)Screen.width;
							float num3 = (float)Screen.height;
							bool flag6 = targetDisplay > 0 && targetDisplay < Display.displays.Length;
							if (flag6)
							{
								num2 = (float)Display.displays[targetDisplay].systemWidth;
								num3 = (float)Display.displays[targetDisplay].systemHeight;
							}
							Vector2 vector2 = new Vector2(vector.x / num2, vector.y / num3);
							bool flag7 = vector2.x < 0f || vector2.x > 1f || vector2.y < 0f || vector2.y > 1f;
							if (flag7)
							{
								goto IL_368;
							}
						}
						else
						{
							vector = v;
						}
						bool flag8 = !camera.pixelRect.Contains(vector);
						if (!flag8)
						{
							bool flag9 = camera.eventMask == 0;
							if (!flag9)
							{
								Ray ray = camera.ScreenPointToRay(vector);
								float z = ray.direction.z;
								float distance = Mathf.Approximately(0f, z) ? float.PositiveInfinity : Mathf.Abs((camera.farClipPlane - camera.nearClipPlane) / z);
								GameObject gameObject = CameraRaycastHelper.RaycastTry(camera, ray, distance, camera.cullingMask & camera.eventMask);
								bool flag10 = gameObject != null;
								if (flag10)
								{
									SendMouseEvents.m_CurrentHit[1].target = gameObject;
									SendMouseEvents.m_CurrentHit[1].camera = camera;
								}
								else
								{
									bool flag11 = camera.clearFlags == CameraClearFlags.Skybox || camera.clearFlags == CameraClearFlags.Color;
									if (flag11)
									{
										SendMouseEvents.m_CurrentHit[1].target = null;
										SendMouseEvents.m_CurrentHit[1].camera = null;
									}
								}
								GameObject gameObject2 = CameraRaycastHelper.RaycastTry2D(camera, ray, distance, camera.cullingMask & camera.eventMask);
								bool flag12 = gameObject2 != null;
								if (flag12)
								{
									SendMouseEvents.m_CurrentHit[2].target = gameObject2;
									SendMouseEvents.m_CurrentHit[2].camera = camera;
								}
								else
								{
									bool flag13 = camera.clearFlags == CameraClearFlags.Skybox || camera.clearFlags == CameraClearFlags.Color;
									if (flag13)
									{
										SendMouseEvents.m_CurrentHit[2].target = null;
										SendMouseEvents.m_CurrentHit[2].camera = null;
									}
								}
							}
						}
					}
					IL_368:;
				}
			}
			for (int k = 0; k < SendMouseEvents.m_CurrentHit.Length; k++)
			{
				SendMouseEvents.SendEvents(k, SendMouseEvents.m_CurrentHit[k]);
			}
			SendMouseEvents.s_MouseUsed = false;
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00002D08 File Offset: 0x00000F08
		private static void SendEvents(int i, SendMouseEvents.HitInfo hit)
		{
			bool flag = SendMouseEvents.s_MouseButtonPressedThisFrame;
			bool flag2 = SendMouseEvents.s_MouseButtonIsPressed;
			bool flag3 = flag;
			if (flag3)
			{
				bool flag4 = hit;
				if (flag4)
				{
					SendMouseEvents.m_MouseDownHit[i] = hit;
					SendMouseEvents.m_MouseDownHit[i].SendMessage("OnMouseDown");
				}
			}
			else
			{
				bool flag5 = !flag2;
				if (flag5)
				{
					bool flag6 = SendMouseEvents.m_MouseDownHit[i];
					if (flag6)
					{
						bool flag7 = SendMouseEvents.HitInfo.Compare(hit, SendMouseEvents.m_MouseDownHit[i]);
						if (flag7)
						{
							SendMouseEvents.m_MouseDownHit[i].SendMessage("OnMouseUpAsButton");
						}
						SendMouseEvents.m_MouseDownHit[i].SendMessage("OnMouseUp");
						SendMouseEvents.m_MouseDownHit[i] = default(SendMouseEvents.HitInfo);
					}
				}
				else
				{
					bool flag8 = SendMouseEvents.m_MouseDownHit[i];
					if (flag8)
					{
						SendMouseEvents.m_MouseDownHit[i].SendMessage("OnMouseDrag");
					}
				}
			}
			bool flag9 = SendMouseEvents.HitInfo.Compare(hit, SendMouseEvents.m_LastHit[i]);
			if (flag9)
			{
				bool flag10 = hit;
				if (flag10)
				{
					hit.SendMessage("OnMouseOver");
				}
			}
			else
			{
				bool flag11 = SendMouseEvents.m_LastHit[i];
				if (flag11)
				{
					SendMouseEvents.m_LastHit[i].SendMessage("OnMouseExit");
				}
				bool flag12 = hit;
				if (flag12)
				{
					hit.SendMessage("OnMouseEnter");
					hit.SendMessage("OnMouseOver");
				}
			}
			SendMouseEvents.m_LastHit[i] = hit;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x0000257E File Offset: 0x0000077E
		public SendMouseEvents()
		{
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00002E9C File Offset: 0x0000109C
		// Note: this type is marked as 'beforefieldinit'.
		static SendMouseEvents()
		{
		}

		// Token: 0x0400003D RID: 61
		private const int m_HitIndexGUI = 0;

		// Token: 0x0400003E RID: 62
		private const int m_HitIndexPhysics3D = 1;

		// Token: 0x0400003F RID: 63
		private const int m_HitIndexPhysics2D = 2;

		// Token: 0x04000040 RID: 64
		private static bool s_MouseUsed = false;

		// Token: 0x04000041 RID: 65
		private static readonly SendMouseEvents.HitInfo[] m_LastHit = new SendMouseEvents.HitInfo[3];

		// Token: 0x04000042 RID: 66
		private static readonly SendMouseEvents.HitInfo[] m_MouseDownHit = new SendMouseEvents.HitInfo[3];

		// Token: 0x04000043 RID: 67
		private static readonly SendMouseEvents.HitInfo[] m_CurrentHit = new SendMouseEvents.HitInfo[3];

		// Token: 0x04000044 RID: 68
		private static Camera[] m_Cameras;

		// Token: 0x04000045 RID: 69
		public static Func<KeyValuePair<int, Vector2>> s_GetMouseState;

		// Token: 0x04000046 RID: 70
		private static Vector2 s_MousePosition;

		// Token: 0x04000047 RID: 71
		private static bool s_MouseButtonPressedThisFrame;

		// Token: 0x04000048 RID: 72
		private static bool s_MouseButtonIsPressed;

		// Token: 0x02000011 RID: 17
		private struct HitInfo
		{
			// Token: 0x060000AC RID: 172 RVA: 0x00002EC5 File Offset: 0x000010C5
			public void SendMessage(string name)
			{
				this.target.SendMessage(name, null, SendMessageOptions.DontRequireReceiver);
			}

			// Token: 0x060000AD RID: 173 RVA: 0x00002ED8 File Offset: 0x000010D8
			public static implicit operator bool(SendMouseEvents.HitInfo exists)
			{
				return exists.target != null && exists.camera != null;
			}

			// Token: 0x060000AE RID: 174 RVA: 0x00002F08 File Offset: 0x00001108
			public static bool Compare(SendMouseEvents.HitInfo lhs, SendMouseEvents.HitInfo rhs)
			{
				return lhs.target == rhs.target && lhs.camera == rhs.camera;
			}

			// Token: 0x04000049 RID: 73
			public GameObject target;

			// Token: 0x0400004A RID: 74
			public Camera camera;
		}

		// Token: 0x02000012 RID: 18
		public enum LeftMouseButtonState
		{
			// Token: 0x0400004C RID: 76
			NotPressed,
			// Token: 0x0400004D RID: 77
			Pressed,
			// Token: 0x0400004E RID: 78
			PressedThisFrame
		}
	}
}
