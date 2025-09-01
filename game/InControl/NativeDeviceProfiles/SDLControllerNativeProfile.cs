using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x020001D7 RID: 471
	[Preserve]
	[NativeInputDeviceProfile]
	public class SDLControllerNativeProfile : InputDeviceProfile
	{
		// Token: 0x06000879 RID: 2169 RVA: 0x00045184 File Offset: 0x00043384
		public override void Define()
		{
			base.Define();
			base.DeviceName = "{NAME}";
			base.DeviceNotes = "";
			base.DeviceClass = InputDeviceClass.Controller;
			base.IncludePlatforms = new string[]
			{
				"OS X",
				"Windows"
			};
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x000451D0 File Offset: 0x000433D0
		protected static InputControlMapping Action1Mapping(string name)
		{
			return new InputControlMapping
			{
				Name = name,
				Target = InputControlType.Action1,
				Source = InputDeviceProfile.Button(0)
			};
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x000451F2 File Offset: 0x000433F2
		protected static InputControlMapping Action2Mapping(string name)
		{
			return new InputControlMapping
			{
				Name = name,
				Target = InputControlType.Action2,
				Source = InputDeviceProfile.Button(1)
			};
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x00045214 File Offset: 0x00043414
		protected static InputControlMapping Action3Mapping(string name)
		{
			return new InputControlMapping
			{
				Name = name,
				Target = InputControlType.Action3,
				Source = InputDeviceProfile.Button(2)
			};
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x00045236 File Offset: 0x00043436
		protected static InputControlMapping Action4Mapping(string name)
		{
			return new InputControlMapping
			{
				Name = name,
				Target = InputControlType.Action4,
				Source = InputDeviceProfile.Button(3)
			};
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x00045258 File Offset: 0x00043458
		protected static InputControlMapping LeftCommandMapping(string name, InputControlType target)
		{
			return new InputControlMapping
			{
				Name = name,
				Target = target,
				Source = InputDeviceProfile.Button(4)
			};
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x00045279 File Offset: 0x00043479
		protected static InputControlMapping SystemMapping(string name, InputControlType target)
		{
			return new InputControlMapping
			{
				Name = name,
				Target = target,
				Source = InputDeviceProfile.Button(5)
			};
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x0004529A File Offset: 0x0004349A
		protected static InputControlMapping RightCommandMapping(string name, InputControlType target)
		{
			return new InputControlMapping
			{
				Name = name,
				Target = target,
				Source = InputDeviceProfile.Button(6)
			};
		}

		// Token: 0x06000881 RID: 2177 RVA: 0x000452BB File Offset: 0x000434BB
		protected static InputControlMapping LeftStickButtonMapping()
		{
			return new InputControlMapping
			{
				Name = "Left Stick Button",
				Target = InputControlType.LeftStickButton,
				Source = InputDeviceProfile.Button(7)
			};
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x000452E0 File Offset: 0x000434E0
		protected static InputControlMapping RightStickButtonMapping()
		{
			return new InputControlMapping
			{
				Name = "Right Stick Button",
				Target = InputControlType.RightStickButton,
				Source = InputDeviceProfile.Button(8)
			};
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x00045306 File Offset: 0x00043506
		protected static InputControlMapping LeftBumperMapping(string name = "Left Bumper")
		{
			return new InputControlMapping
			{
				Name = name,
				Target = InputControlType.LeftBumper,
				Source = InputDeviceProfile.Button(9)
			};
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x00045329 File Offset: 0x00043529
		protected static InputControlMapping RightBumperMapping(string name = "Right Bumper")
		{
			return new InputControlMapping
			{
				Name = name,
				Target = InputControlType.RightBumper,
				Source = InputDeviceProfile.Button(10)
			};
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x0004534C File Offset: 0x0004354C
		protected static InputControlMapping DPadUpMapping()
		{
			return new InputControlMapping
			{
				Name = "DPad Up",
				Target = InputControlType.DPadUp,
				Source = InputDeviceProfile.Button(11)
			};
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x00045373 File Offset: 0x00043573
		protected static InputControlMapping DPadDownMapping()
		{
			return new InputControlMapping
			{
				Name = "DPad Down",
				Target = InputControlType.DPadDown,
				Source = InputDeviceProfile.Button(12)
			};
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x0004539A File Offset: 0x0004359A
		protected static InputControlMapping DPadLeftMapping()
		{
			return new InputControlMapping
			{
				Name = "DPad Left",
				Target = InputControlType.DPadLeft,
				Source = InputDeviceProfile.Button(13)
			};
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x000453C1 File Offset: 0x000435C1
		protected static InputControlMapping DPadRightMapping()
		{
			return new InputControlMapping
			{
				Name = "DPad Right",
				Target = InputControlType.DPadRight,
				Source = InputDeviceProfile.Button(14)
			};
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x000453E8 File Offset: 0x000435E8
		protected static InputControlMapping Misc1Mapping(string name, InputControlType target)
		{
			return new InputControlMapping
			{
				Name = name,
				Target = target,
				Source = InputDeviceProfile.Button(15)
			};
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x0004540A File Offset: 0x0004360A
		protected static InputControlMapping Paddle1Mapping()
		{
			return new InputControlMapping
			{
				Name = "Paddle 1",
				Target = InputControlType.Paddle1,
				Source = InputDeviceProfile.Button(16)
			};
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x00045434 File Offset: 0x00043634
		protected static InputControlMapping Paddle2Mapping()
		{
			return new InputControlMapping
			{
				Name = "Paddle 2",
				Target = InputControlType.Paddle2,
				Source = InputDeviceProfile.Button(17)
			};
		}

		// Token: 0x0600088C RID: 2188 RVA: 0x0004545E File Offset: 0x0004365E
		protected static InputControlMapping Paddle3Mapping()
		{
			return new InputControlMapping
			{
				Name = "Paddle 3",
				Target = InputControlType.Paddle3,
				Source = InputDeviceProfile.Button(18)
			};
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x00045488 File Offset: 0x00043688
		protected static InputControlMapping Paddle4Mapping()
		{
			return new InputControlMapping
			{
				Name = "Paddle 4",
				Target = InputControlType.Paddle4,
				Source = InputDeviceProfile.Button(19)
			};
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x000454B2 File Offset: 0x000436B2
		protected static InputControlMapping TouchPadButtonMapping()
		{
			return new InputControlMapping
			{
				Name = "Touch Pad Button",
				Target = InputControlType.TouchPadButton,
				Source = InputDeviceProfile.Button(20)
			};
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x000454DC File Offset: 0x000436DC
		protected static InputControlMapping LeftStickLeftMapping()
		{
			return new InputControlMapping
			{
				Name = "Left Stick Left",
				Target = InputControlType.LeftStickLeft,
				Source = InputDeviceProfile.Analog(0),
				SourceRange = InputRangeType.ZeroToMinusOne,
				TargetRange = InputRangeType.ZeroToOne
			};
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x0004550F File Offset: 0x0004370F
		protected static InputControlMapping LeftStickRightMapping()
		{
			return new InputControlMapping
			{
				Name = "Left Stick Right",
				Target = InputControlType.LeftStickRight,
				Source = InputDeviceProfile.Analog(0),
				SourceRange = InputRangeType.ZeroToOne,
				TargetRange = InputRangeType.ZeroToOne
			};
		}

		// Token: 0x06000891 RID: 2193 RVA: 0x00045542 File Offset: 0x00043742
		protected static InputControlMapping LeftStickUpMapping()
		{
			return new InputControlMapping
			{
				Name = "Left Stick Up",
				Target = InputControlType.LeftStickUp,
				Source = InputDeviceProfile.Analog(1),
				SourceRange = InputRangeType.ZeroToMinusOne,
				TargetRange = InputRangeType.ZeroToOne
			};
		}

		// Token: 0x06000892 RID: 2194 RVA: 0x00045575 File Offset: 0x00043775
		protected static InputControlMapping LeftStickDownMapping()
		{
			return new InputControlMapping
			{
				Name = "Left Stick Down",
				Target = InputControlType.LeftStickDown,
				Source = InputDeviceProfile.Analog(1),
				SourceRange = InputRangeType.ZeroToOne,
				TargetRange = InputRangeType.ZeroToOne
			};
		}

		// Token: 0x06000893 RID: 2195 RVA: 0x000455A8 File Offset: 0x000437A8
		protected static InputControlMapping RightStickLeftMapping()
		{
			return new InputControlMapping
			{
				Name = "Right Stick Left",
				Target = InputControlType.RightStickLeft,
				Source = InputDeviceProfile.Analog(2),
				SourceRange = InputRangeType.ZeroToMinusOne,
				TargetRange = InputRangeType.ZeroToOne
			};
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x000455DB File Offset: 0x000437DB
		protected static InputControlMapping RightStickRightMapping()
		{
			return new InputControlMapping
			{
				Name = "Right Stick Right",
				Target = InputControlType.RightStickRight,
				Source = InputDeviceProfile.Analog(2),
				SourceRange = InputRangeType.ZeroToOne,
				TargetRange = InputRangeType.ZeroToOne
			};
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x0004560F File Offset: 0x0004380F
		protected static InputControlMapping RightStickUpMapping()
		{
			return new InputControlMapping
			{
				Name = "Right Stick Up",
				Target = InputControlType.RightStickUp,
				Source = InputDeviceProfile.Analog(3),
				SourceRange = InputRangeType.ZeroToMinusOne,
				TargetRange = InputRangeType.ZeroToOne
			};
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x00045642 File Offset: 0x00043842
		protected static InputControlMapping RightStickDownMapping()
		{
			return new InputControlMapping
			{
				Name = "Right Stick Down",
				Target = InputControlType.RightStickDown,
				Source = InputDeviceProfile.Analog(3),
				SourceRange = InputRangeType.ZeroToOne,
				TargetRange = InputRangeType.ZeroToOne
			};
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x00045675 File Offset: 0x00043875
		protected static InputControlMapping LeftTriggerMapping(string name = "Left Trigger")
		{
			return new InputControlMapping
			{
				Name = name,
				Target = InputControlType.LeftTrigger,
				Source = InputDeviceProfile.Analog(4),
				SourceRange = InputRangeType.ZeroToOne,
				TargetRange = InputRangeType.ZeroToOne
			};
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x000456A5 File Offset: 0x000438A5
		protected static InputControlMapping RightTriggerMapping(string name = "Right Trigger")
		{
			return new InputControlMapping
			{
				Name = name,
				Target = InputControlType.RightTrigger,
				Source = InputDeviceProfile.Analog(5),
				SourceRange = InputRangeType.ZeroToOne,
				TargetRange = InputRangeType.ZeroToOne
			};
		}

		// Token: 0x06000899 RID: 2201 RVA: 0x000456D5 File Offset: 0x000438D5
		protected static InputControlMapping AccelerometerXMapping()
		{
			return new InputControlMapping
			{
				Name = "Accelerometer X",
				Target = InputControlType.AccelerometerX,
				Source = InputDeviceProfile.Analog(6),
				SourceRange = InputRangeType.MinusOneToOne,
				TargetRange = InputRangeType.MinusOneToOne,
				Passive = true
			};
		}

		// Token: 0x0600089A RID: 2202 RVA: 0x00045713 File Offset: 0x00043913
		protected static InputControlMapping AccelerometerYMapping()
		{
			return new InputControlMapping
			{
				Name = "Accelerometer Y",
				Target = InputControlType.AccelerometerY,
				Source = InputDeviceProfile.Analog(7),
				SourceRange = InputRangeType.MinusOneToOne,
				TargetRange = InputRangeType.MinusOneToOne,
				Passive = true
			};
		}

		// Token: 0x0600089B RID: 2203 RVA: 0x00045751 File Offset: 0x00043951
		protected static InputControlMapping AccelerometerZMapping()
		{
			return new InputControlMapping
			{
				Name = "Accelerometer Z",
				Target = InputControlType.AccelerometerZ,
				Source = InputDeviceProfile.Analog(8),
				SourceRange = InputRangeType.MinusOneToOne,
				TargetRange = InputRangeType.MinusOneToOne,
				Passive = true
			};
		}

		// Token: 0x0600089C RID: 2204 RVA: 0x0004578F File Offset: 0x0004398F
		protected static InputControlMapping GyroscopeXMapping()
		{
			return new InputControlMapping
			{
				Name = "Gyroscope X",
				Target = InputControlType.TiltX,
				Source = InputDeviceProfile.Analog(9),
				SourceRange = InputRangeType.MinusOneToOne,
				TargetRange = InputRangeType.MinusOneToOne,
				Passive = true
			};
		}

		// Token: 0x0600089D RID: 2205 RVA: 0x000457CE File Offset: 0x000439CE
		protected static InputControlMapping GyroscopeYMapping()
		{
			return new InputControlMapping
			{
				Name = "Gyroscope Y",
				Target = InputControlType.TiltY,
				Source = InputDeviceProfile.Analog(10),
				SourceRange = InputRangeType.MinusOneToOne,
				TargetRange = InputRangeType.MinusOneToOne,
				Passive = true
			};
		}

		// Token: 0x0600089E RID: 2206 RVA: 0x0004580D File Offset: 0x00043A0D
		protected static InputControlMapping GyroscopeZMapping()
		{
			return new InputControlMapping
			{
				Name = "Gyroscope Z",
				Target = InputControlType.TiltZ,
				Source = InputDeviceProfile.Analog(11),
				SourceRange = InputRangeType.MinusOneToOne,
				TargetRange = InputRangeType.MinusOneToOne,
				Passive = true
			};
		}

		// Token: 0x0600089F RID: 2207 RVA: 0x0004584C File Offset: 0x00043A4C
		public SDLControllerNativeProfile()
		{
		}

		// Token: 0x0200021C RID: 540
		protected enum SDLButtonType
		{
			// Token: 0x040004A3 RID: 1187
			SDL_CONTROLLER_BUTTON_INVALID = -1,
			// Token: 0x040004A4 RID: 1188
			SDL_CONTROLLER_BUTTON_A,
			// Token: 0x040004A5 RID: 1189
			SDL_CONTROLLER_BUTTON_B,
			// Token: 0x040004A6 RID: 1190
			SDL_CONTROLLER_BUTTON_X,
			// Token: 0x040004A7 RID: 1191
			SDL_CONTROLLER_BUTTON_Y,
			// Token: 0x040004A8 RID: 1192
			SDL_CONTROLLER_BUTTON_BACK,
			// Token: 0x040004A9 RID: 1193
			SDL_CONTROLLER_BUTTON_GUIDE,
			// Token: 0x040004AA RID: 1194
			SDL_CONTROLLER_BUTTON_START,
			// Token: 0x040004AB RID: 1195
			SDL_CONTROLLER_BUTTON_LEFTSTICK,
			// Token: 0x040004AC RID: 1196
			SDL_CONTROLLER_BUTTON_RIGHTSTICK,
			// Token: 0x040004AD RID: 1197
			SDL_CONTROLLER_BUTTON_LEFTSHOULDER,
			// Token: 0x040004AE RID: 1198
			SDL_CONTROLLER_BUTTON_RIGHTSHOULDER,
			// Token: 0x040004AF RID: 1199
			SDL_CONTROLLER_BUTTON_DPAD_UP,
			// Token: 0x040004B0 RID: 1200
			SDL_CONTROLLER_BUTTON_DPAD_DOWN,
			// Token: 0x040004B1 RID: 1201
			SDL_CONTROLLER_BUTTON_DPAD_LEFT,
			// Token: 0x040004B2 RID: 1202
			SDL_CONTROLLER_BUTTON_DPAD_RIGHT,
			// Token: 0x040004B3 RID: 1203
			SDL_CONTROLLER_BUTTON_MISC1,
			// Token: 0x040004B4 RID: 1204
			SDL_CONTROLLER_BUTTON_PADDLE1,
			// Token: 0x040004B5 RID: 1205
			SDL_CONTROLLER_BUTTON_PADDLE2,
			// Token: 0x040004B6 RID: 1206
			SDL_CONTROLLER_BUTTON_PADDLE3,
			// Token: 0x040004B7 RID: 1207
			SDL_CONTROLLER_BUTTON_PADDLE4,
			// Token: 0x040004B8 RID: 1208
			SDL_CONTROLLER_BUTTON_TOUCHPAD,
			// Token: 0x040004B9 RID: 1209
			SDL_CONTROLLER_BUTTON_MAX
		}
	}
}
