using System;
using UnityEngine;

namespace InControl
{
	// Token: 0x02000055 RID: 85
	public abstract class TouchControl : MonoBehaviour
	{
		// Token: 0x0600040C RID: 1036
		public abstract void CreateControl();

		// Token: 0x0600040D RID: 1037
		public abstract void DestroyControl();

		// Token: 0x0600040E RID: 1038
		public abstract void ConfigureControl();

		// Token: 0x0600040F RID: 1039
		public abstract void SubmitControlState(ulong updateTick, float deltaTime);

		// Token: 0x06000410 RID: 1040
		public abstract void CommitControlState(ulong updateTick, float deltaTime);

		// Token: 0x06000411 RID: 1041
		public abstract void TouchBegan(Touch touch);

		// Token: 0x06000412 RID: 1042
		public abstract void TouchMoved(Touch touch);

		// Token: 0x06000413 RID: 1043
		public abstract void TouchEnded(Touch touch);

		// Token: 0x06000414 RID: 1044
		public abstract void DrawGizmos();

		// Token: 0x06000415 RID: 1045 RVA: 0x0000EA69 File Offset: 0x0000CC69
		private void OnEnable()
		{
			TouchManager.OnSetup += this.Setup;
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x0000EA7C File Offset: 0x0000CC7C
		private void OnDisable()
		{
			this.DestroyControl();
			Resources.UnloadUnusedAssets();
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x0000EA8A File Offset: 0x0000CC8A
		private void Setup()
		{
			if (!base.enabled)
			{
				return;
			}
			this.CreateControl();
			this.ConfigureControl();
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x0000EAA4 File Offset: 0x0000CCA4
		protected Vector3 OffsetToWorldPosition(TouchControlAnchor anchor, Vector2 offset, TouchUnitType offsetUnitType, bool lockAspectRatio)
		{
			Vector3 b;
			if (offsetUnitType == TouchUnitType.Pixels)
			{
				b = TouchUtility.RoundVector(offset) * TouchManager.PixelToWorld;
			}
			else if (lockAspectRatio)
			{
				b = offset * TouchManager.PercentToWorld;
			}
			else
			{
				b = Vector3.Scale(offset, TouchManager.ViewSize);
			}
			return TouchManager.ViewToWorldPoint(TouchUtility.AnchorToViewPoint(anchor)) + b;
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x0000EB08 File Offset: 0x0000CD08
		protected void SubmitButtonState(TouchControl.ButtonTarget target, bool state, ulong updateTick, float deltaTime)
		{
			if (TouchManager.Device == null || target == TouchControl.ButtonTarget.None)
			{
				return;
			}
			InputControl control = TouchManager.Device.GetControl((InputControlType)target);
			if (control != null && control != InputControl.Null)
			{
				control.UpdateWithState(state, updateTick, deltaTime);
			}
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x0000EB44 File Offset: 0x0000CD44
		protected void SubmitButtonValue(TouchControl.ButtonTarget target, float value, ulong updateTick, float deltaTime)
		{
			if (TouchManager.Device == null || target == TouchControl.ButtonTarget.None)
			{
				return;
			}
			InputControl control = TouchManager.Device.GetControl((InputControlType)target);
			if (control != null && control != InputControl.Null)
			{
				control.UpdateWithValue(value, updateTick, deltaTime);
			}
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x0000EB80 File Offset: 0x0000CD80
		protected void CommitButton(TouchControl.ButtonTarget target)
		{
			if (TouchManager.Device == null || target == TouchControl.ButtonTarget.None)
			{
				return;
			}
			InputControl control = TouchManager.Device.GetControl((InputControlType)target);
			if (control != null && control != InputControl.Null)
			{
				control.Commit();
			}
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x0000EBB8 File Offset: 0x0000CDB8
		protected void SubmitAnalogValue(TouchControl.AnalogTarget target, Vector2 value, float lowerDeadZone, float upperDeadZone, ulong updateTick, float deltaTime)
		{
			if (TouchManager.Device == null || target == TouchControl.AnalogTarget.None)
			{
				return;
			}
			Vector2 value2 = DeadZone.Circular(value.x, value.y, lowerDeadZone, upperDeadZone);
			if (target == TouchControl.AnalogTarget.LeftStick || target == TouchControl.AnalogTarget.Both)
			{
				TouchManager.Device.UpdateLeftStickWithValue(value2, updateTick, deltaTime);
			}
			if (target == TouchControl.AnalogTarget.RightStick || target == TouchControl.AnalogTarget.Both)
			{
				TouchManager.Device.UpdateRightStickWithValue(value2, updateTick, deltaTime);
			}
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x0000EC13 File Offset: 0x0000CE13
		protected void CommitAnalog(TouchControl.AnalogTarget target)
		{
			if (TouchManager.Device == null || target == TouchControl.AnalogTarget.None)
			{
				return;
			}
			if (target == TouchControl.AnalogTarget.LeftStick || target == TouchControl.AnalogTarget.Both)
			{
				TouchManager.Device.CommitLeftStick();
			}
			if (target == TouchControl.AnalogTarget.RightStick || target == TouchControl.AnalogTarget.Both)
			{
				TouchManager.Device.CommitRightStick();
			}
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x0000EC44 File Offset: 0x0000CE44
		protected void SubmitRawAnalogValue(TouchControl.AnalogTarget target, Vector2 rawValue, ulong updateTick, float deltaTime)
		{
			if (TouchManager.Device == null || target == TouchControl.AnalogTarget.None)
			{
				return;
			}
			if (target == TouchControl.AnalogTarget.LeftStick || target == TouchControl.AnalogTarget.Both)
			{
				TouchManager.Device.UpdateLeftStickWithRawValue(rawValue, updateTick, deltaTime);
			}
			if (target == TouchControl.AnalogTarget.RightStick || target == TouchControl.AnalogTarget.Both)
			{
				TouchManager.Device.UpdateRightStickWithRawValue(rawValue, updateTick, deltaTime);
			}
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x0000EC80 File Offset: 0x0000CE80
		protected static Vector3 SnapTo(Vector2 vector, TouchControl.SnapAngles snapAngles)
		{
			if (snapAngles == TouchControl.SnapAngles.None)
			{
				return vector;
			}
			float snapAngle = 360f / (float)snapAngles;
			return TouchControl.SnapTo(vector, snapAngle);
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x0000ECA8 File Offset: 0x0000CEA8
		protected static Vector3 SnapTo(Vector2 vector, float snapAngle)
		{
			float num = Vector2.Angle(vector, Vector2.up);
			if (num < snapAngle / 2f)
			{
				return Vector2.up * vector.magnitude;
			}
			if (num > 180f - snapAngle / 2f)
			{
				return -Vector2.up * vector.magnitude;
			}
			float angle = Mathf.Round(num / snapAngle) * snapAngle - num;
			Vector3 axis = Vector3.Cross(Vector2.up, vector);
			return Quaternion.AngleAxis(angle, axis) * vector;
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x0000ED41 File Offset: 0x0000CF41
		private void OnDrawGizmosSelected()
		{
			if (!base.enabled)
			{
				return;
			}
			if (TouchManager.ControlsShowGizmos != TouchManager.GizmoShowOption.WhenSelected)
			{
				return;
			}
			if (Utility.GameObjectIsCulledOnCurrentCamera(base.gameObject))
			{
				return;
			}
			if (!Application.isPlaying)
			{
				this.ConfigureControl();
			}
			this.DrawGizmos();
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x0000ED78 File Offset: 0x0000CF78
		private void OnDrawGizmos()
		{
			if (!base.enabled)
			{
				return;
			}
			if (TouchManager.ControlsShowGizmos == TouchManager.GizmoShowOption.UnlessPlaying)
			{
				if (Application.isPlaying)
				{
					return;
				}
			}
			else if (TouchManager.ControlsShowGizmos != TouchManager.GizmoShowOption.Always)
			{
				return;
			}
			if (Utility.GameObjectIsCulledOnCurrentCamera(base.gameObject))
			{
				return;
			}
			if (!Application.isPlaying)
			{
				this.ConfigureControl();
			}
			this.DrawGizmos();
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x0000EDC8 File Offset: 0x0000CFC8
		protected TouchControl()
		{
		}

		// Token: 0x02000214 RID: 532
		public enum ButtonTarget
		{
			// Token: 0x0400045E RID: 1118
			None,
			// Token: 0x0400045F RID: 1119
			DPadDown = 12,
			// Token: 0x04000460 RID: 1120
			DPadLeft,
			// Token: 0x04000461 RID: 1121
			DPadRight,
			// Token: 0x04000462 RID: 1122
			DPadUp = 11,
			// Token: 0x04000463 RID: 1123
			LeftTrigger = 15,
			// Token: 0x04000464 RID: 1124
			RightTrigger,
			// Token: 0x04000465 RID: 1125
			LeftBumper,
			// Token: 0x04000466 RID: 1126
			RightBumper,
			// Token: 0x04000467 RID: 1127
			Action1,
			// Token: 0x04000468 RID: 1128
			Action2,
			// Token: 0x04000469 RID: 1129
			Action3,
			// Token: 0x0400046A RID: 1130
			Action4,
			// Token: 0x0400046B RID: 1131
			Action5,
			// Token: 0x0400046C RID: 1132
			Action6,
			// Token: 0x0400046D RID: 1133
			Action7,
			// Token: 0x0400046E RID: 1134
			Action8,
			// Token: 0x0400046F RID: 1135
			Action9,
			// Token: 0x04000470 RID: 1136
			Action10,
			// Token: 0x04000471 RID: 1137
			Action11,
			// Token: 0x04000472 RID: 1138
			Action12,
			// Token: 0x04000473 RID: 1139
			Menu = 106,
			// Token: 0x04000474 RID: 1140
			Button0 = 500,
			// Token: 0x04000475 RID: 1141
			Button1,
			// Token: 0x04000476 RID: 1142
			Button2,
			// Token: 0x04000477 RID: 1143
			Button3,
			// Token: 0x04000478 RID: 1144
			Button4,
			// Token: 0x04000479 RID: 1145
			Button5,
			// Token: 0x0400047A RID: 1146
			Button6,
			// Token: 0x0400047B RID: 1147
			Button7,
			// Token: 0x0400047C RID: 1148
			Button8,
			// Token: 0x0400047D RID: 1149
			Button9,
			// Token: 0x0400047E RID: 1150
			Button10,
			// Token: 0x0400047F RID: 1151
			Button11,
			// Token: 0x04000480 RID: 1152
			Button12,
			// Token: 0x04000481 RID: 1153
			Button13,
			// Token: 0x04000482 RID: 1154
			Button14,
			// Token: 0x04000483 RID: 1155
			Button15,
			// Token: 0x04000484 RID: 1156
			Button16,
			// Token: 0x04000485 RID: 1157
			Button17,
			// Token: 0x04000486 RID: 1158
			Button18,
			// Token: 0x04000487 RID: 1159
			Button19
		}

		// Token: 0x02000215 RID: 533
		public enum AnalogTarget
		{
			// Token: 0x04000489 RID: 1161
			None,
			// Token: 0x0400048A RID: 1162
			LeftStick,
			// Token: 0x0400048B RID: 1163
			RightStick,
			// Token: 0x0400048C RID: 1164
			Both
		}

		// Token: 0x02000216 RID: 534
		public enum SnapAngles
		{
			// Token: 0x0400048E RID: 1166
			None,
			// Token: 0x0400048F RID: 1167
			Four = 4,
			// Token: 0x04000490 RID: 1168
			Eight = 8,
			// Token: 0x04000491 RID: 1169
			Sixteen = 16
		}
	}
}
