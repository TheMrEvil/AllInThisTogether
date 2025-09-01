using System;
using System.Collections.Generic;
using UnityEngine;

namespace InControl
{
	// Token: 0x0200003A RID: 58
	[Preserve]
	[Serializable]
	public class InputDeviceProfile
	{
		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000287 RID: 647 RVA: 0x000086AA File Offset: 0x000068AA
		// (set) Token: 0x06000288 RID: 648 RVA: 0x000086B2 File Offset: 0x000068B2
		public InputDeviceProfileType ProfileType
		{
			get
			{
				return this.profileType;
			}
			protected set
			{
				this.profileType = value;
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000289 RID: 649 RVA: 0x000086BB File Offset: 0x000068BB
		// (set) Token: 0x0600028A RID: 650 RVA: 0x000086C3 File Offset: 0x000068C3
		public string DeviceName
		{
			get
			{
				return this.deviceName;
			}
			protected set
			{
				this.deviceName = value;
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x0600028B RID: 651 RVA: 0x000086CC File Offset: 0x000068CC
		// (set) Token: 0x0600028C RID: 652 RVA: 0x000086D4 File Offset: 0x000068D4
		public string DeviceNotes
		{
			get
			{
				return this.deviceNotes;
			}
			protected set
			{
				this.deviceNotes = value;
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x0600028D RID: 653 RVA: 0x000086DD File Offset: 0x000068DD
		// (set) Token: 0x0600028E RID: 654 RVA: 0x000086E5 File Offset: 0x000068E5
		public InputDeviceClass DeviceClass
		{
			get
			{
				return this.deviceClass;
			}
			protected set
			{
				this.deviceClass = value;
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x0600028F RID: 655 RVA: 0x000086EE File Offset: 0x000068EE
		// (set) Token: 0x06000290 RID: 656 RVA: 0x000086F6 File Offset: 0x000068F6
		public InputDeviceStyle DeviceStyle
		{
			get
			{
				return this.deviceStyle;
			}
			protected set
			{
				this.deviceStyle = value;
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000291 RID: 657 RVA: 0x000086FF File Offset: 0x000068FF
		// (set) Token: 0x06000292 RID: 658 RVA: 0x00008707 File Offset: 0x00006907
		public float Sensitivity
		{
			get
			{
				return this.sensitivity;
			}
			protected set
			{
				this.sensitivity = Mathf.Clamp01(value);
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000293 RID: 659 RVA: 0x00008715 File Offset: 0x00006915
		// (set) Token: 0x06000294 RID: 660 RVA: 0x0000871D File Offset: 0x0000691D
		public float LowerDeadZone
		{
			get
			{
				return this.lowerDeadZone;
			}
			protected set
			{
				this.lowerDeadZone = Mathf.Clamp01(value);
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000295 RID: 661 RVA: 0x0000872B File Offset: 0x0000692B
		// (set) Token: 0x06000296 RID: 662 RVA: 0x00008733 File Offset: 0x00006933
		public float UpperDeadZone
		{
			get
			{
				return this.upperDeadZone;
			}
			protected set
			{
				this.upperDeadZone = Mathf.Clamp01(value);
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000297 RID: 663 RVA: 0x00008741 File Offset: 0x00006941
		// (set) Token: 0x06000298 RID: 664 RVA: 0x00008749 File Offset: 0x00006949
		public InputControlMapping[] AnalogMappings
		{
			get
			{
				return this.analogMappings;
			}
			protected set
			{
				this.analogMappings = value;
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000299 RID: 665 RVA: 0x00008752 File Offset: 0x00006952
		// (set) Token: 0x0600029A RID: 666 RVA: 0x0000875A File Offset: 0x0000695A
		public InputControlMapping[] ButtonMappings
		{
			get
			{
				return this.buttonMappings;
			}
			protected set
			{
				this.buttonMappings = value;
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x0600029B RID: 667 RVA: 0x00008763 File Offset: 0x00006963
		// (set) Token: 0x0600029C RID: 668 RVA: 0x0000876B File Offset: 0x0000696B
		public string[] IncludePlatforms
		{
			get
			{
				return this.includePlatforms;
			}
			protected set
			{
				this.includePlatforms = value;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x0600029D RID: 669 RVA: 0x00008774 File Offset: 0x00006974
		// (set) Token: 0x0600029E RID: 670 RVA: 0x0000877C File Offset: 0x0000697C
		public string[] ExcludePlatforms
		{
			get
			{
				return this.excludePlatforms;
			}
			protected set
			{
				this.excludePlatforms = value;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x0600029F RID: 671 RVA: 0x00008785 File Offset: 0x00006985
		// (set) Token: 0x060002A0 RID: 672 RVA: 0x0000878D File Offset: 0x0000698D
		public int MinSystemBuildNumber
		{
			get
			{
				return this.minSystemBuildNumber;
			}
			protected set
			{
				this.minSystemBuildNumber = value;
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060002A1 RID: 673 RVA: 0x00008796 File Offset: 0x00006996
		// (set) Token: 0x060002A2 RID: 674 RVA: 0x0000879E File Offset: 0x0000699E
		public int MaxSystemBuildNumber
		{
			get
			{
				return this.maxSystemBuildNumber;
			}
			protected set
			{
				this.maxSystemBuildNumber = value;
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x000087A7 File Offset: 0x000069A7
		// (set) Token: 0x060002A4 RID: 676 RVA: 0x000087AF File Offset: 0x000069AF
		public VersionInfo MinUnityVersion
		{
			get
			{
				return this.minUnityVersion;
			}
			protected set
			{
				this.minUnityVersion = value;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x000087B8 File Offset: 0x000069B8
		// (set) Token: 0x060002A6 RID: 678 RVA: 0x000087C0 File Offset: 0x000069C0
		public VersionInfo MaxUnityVersion
		{
			get
			{
				return this.maxUnityVersion;
			}
			protected set
			{
				this.maxUnityVersion = value;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x000087C9 File Offset: 0x000069C9
		// (set) Token: 0x060002A8 RID: 680 RVA: 0x000087D1 File Offset: 0x000069D1
		public InputDeviceMatcher[] Matchers
		{
			get
			{
				return this.matchers;
			}
			protected set
			{
				this.matchers = value;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x000087DA File Offset: 0x000069DA
		// (set) Token: 0x060002AA RID: 682 RVA: 0x000087E2 File Offset: 0x000069E2
		public InputDeviceMatcher[] LastResortMatchers
		{
			get
			{
				return this.lastResortMatchers;
			}
			protected set
			{
				this.lastResortMatchers = value;
			}
		}

		// Token: 0x060002AB RID: 683 RVA: 0x000087EB File Offset: 0x000069EB
		public static InputDeviceProfile CreateInstanceOfType(Type type)
		{
			InputDeviceProfile inputDeviceProfile = (InputDeviceProfile)Activator.CreateInstance(type);
			inputDeviceProfile.Define();
			return inputDeviceProfile;
		}

		// Token: 0x060002AC RID: 684 RVA: 0x00008800 File Offset: 0x00006A00
		public static InputDeviceProfile CreateInstanceOfType(string typeName)
		{
			Type type = Type.GetType(typeName);
			if (type == null)
			{
				Logger.LogWarning("Cannot find type: " + typeName + "(is the IL2CPP stripping level too high?)");
				return null;
			}
			return InputDeviceProfile.CreateInstanceOfType(type);
		}

		// Token: 0x060002AD RID: 685 RVA: 0x0000883C File Offset: 0x00006A3C
		public virtual void Define()
		{
			this.profileType = ((base.GetType().GetCustomAttributes(typeof(NativeInputDeviceProfileAttribute), false).Length != 0) ? InputDeviceProfileType.Native : InputDeviceProfileType.Unity);
		}

		// Token: 0x060002AE RID: 686 RVA: 0x00008871 File Offset: 0x00006A71
		public bool Matches(InputDeviceInfo deviceInfo)
		{
			return this.Matches(deviceInfo, this.Matchers);
		}

		// Token: 0x060002AF RID: 687 RVA: 0x00008880 File Offset: 0x00006A80
		public bool LastResortMatches(InputDeviceInfo deviceInfo)
		{
			return this.Matches(deviceInfo, this.LastResortMatchers);
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x00008890 File Offset: 0x00006A90
		public bool Matches(InputDeviceInfo deviceInfo, InputDeviceMatcher[] matchers)
		{
			if (matchers != null)
			{
				int num = matchers.Length;
				for (int i = 0; i < num; i++)
				{
					if (matchers[i].Matches(deviceInfo))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060002B1 RID: 689 RVA: 0x000088C4 File Offset: 0x00006AC4
		public bool IsSupportedOnThisPlatform
		{
			get
			{
				VersionInfo a = VersionInfo.UnityVersion();
				if (a < this.MinUnityVersion || a > this.MaxUnityVersion)
				{
					return false;
				}
				int systemBuildNumber = Utility.GetSystemBuildNumber();
				if (this.MaxSystemBuildNumber > 0 && systemBuildNumber > this.MaxSystemBuildNumber)
				{
					return false;
				}
				if (this.MinSystemBuildNumber > 0 && systemBuildNumber < this.MinSystemBuildNumber)
				{
					return false;
				}
				if (this.ExcludePlatforms != null)
				{
					int num = this.ExcludePlatforms.Length;
					for (int i = 0; i < num; i++)
					{
						if (InputManager.Platform.Contains(this.ExcludePlatforms[i].ToUpper()))
						{
							return false;
						}
					}
				}
				if (this.IncludePlatforms == null || this.IncludePlatforms.Length == 0)
				{
					return true;
				}
				if (this.IncludePlatforms != null)
				{
					int num2 = this.IncludePlatforms.Length;
					for (int j = 0; j < num2; j++)
					{
						if (InputManager.Platform.Contains(this.IncludePlatforms[j].ToUpper()))
						{
							return true;
						}
					}
				}
				return false;
			}
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x000089AE File Offset: 0x00006BAE
		public static void Hide(Type type)
		{
			InputDeviceProfile.hiddenProfiles.Add(type);
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060002B3 RID: 691 RVA: 0x000089BC File Offset: 0x00006BBC
		public bool IsHidden
		{
			get
			{
				return InputDeviceProfile.hiddenProfiles.Contains(base.GetType());
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060002B4 RID: 692 RVA: 0x000089CE File Offset: 0x00006BCE
		public bool IsNotHidden
		{
			get
			{
				return !this.IsHidden;
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060002B5 RID: 693 RVA: 0x000089D9 File Offset: 0x00006BD9
		public int AnalogCount
		{
			get
			{
				return this.AnalogMappings.Length;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060002B6 RID: 694 RVA: 0x000089E3 File Offset: 0x00006BE3
		public int ButtonCount
		{
			get
			{
				return this.ButtonMappings.Length;
			}
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x000089ED File Offset: 0x00006BED
		protected static InputControlSource Button(int index)
		{
			return new InputControlSource(InputControlSourceType.Button, index);
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x000089F6 File Offset: 0x00006BF6
		protected static InputControlSource Analog(int index)
		{
			return new InputControlSource(InputControlSourceType.Analog, index);
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x000089FF File Offset: 0x00006BFF
		protected static InputControlMapping LeftStickLeftMapping(int analog)
		{
			return new InputControlMapping
			{
				Name = "Left Stick Left",
				Target = InputControlType.LeftStickLeft,
				Source = InputDeviceProfile.Analog(analog),
				SourceRange = InputRangeType.ZeroToMinusOne,
				TargetRange = InputRangeType.ZeroToOne
			};
		}

		// Token: 0x060002BA RID: 698 RVA: 0x00008A32 File Offset: 0x00006C32
		protected static InputControlMapping LeftStickRightMapping(int analog)
		{
			return new InputControlMapping
			{
				Name = "Left Stick Right",
				Target = InputControlType.LeftStickRight,
				Source = InputDeviceProfile.Analog(analog),
				SourceRange = InputRangeType.ZeroToOne,
				TargetRange = InputRangeType.ZeroToOne
			};
		}

		// Token: 0x060002BB RID: 699 RVA: 0x00008A65 File Offset: 0x00006C65
		protected static InputControlMapping LeftStickUpMapping(int analog)
		{
			return new InputControlMapping
			{
				Name = "Left Stick Up",
				Target = InputControlType.LeftStickUp,
				Source = InputDeviceProfile.Analog(analog),
				SourceRange = InputRangeType.ZeroToMinusOne,
				TargetRange = InputRangeType.ZeroToOne
			};
		}

		// Token: 0x060002BC RID: 700 RVA: 0x00008A98 File Offset: 0x00006C98
		protected static InputControlMapping LeftStickDownMapping(int analog)
		{
			return new InputControlMapping
			{
				Name = "Left Stick Down",
				Target = InputControlType.LeftStickDown,
				Source = InputDeviceProfile.Analog(analog),
				SourceRange = InputRangeType.ZeroToOne,
				TargetRange = InputRangeType.ZeroToOne
			};
		}

		// Token: 0x060002BD RID: 701 RVA: 0x00008ACB File Offset: 0x00006CCB
		protected static InputControlMapping LeftStickUpMapping2(int analog)
		{
			return new InputControlMapping
			{
				Name = "Left Stick Up",
				Target = InputControlType.LeftStickUp,
				Source = InputDeviceProfile.Analog(analog),
				SourceRange = InputRangeType.ZeroToOne,
				TargetRange = InputRangeType.ZeroToOne
			};
		}

		// Token: 0x060002BE RID: 702 RVA: 0x00008AFE File Offset: 0x00006CFE
		protected static InputControlMapping LeftStickDownMapping2(int analog)
		{
			return new InputControlMapping
			{
				Name = "Left Stick Down",
				Target = InputControlType.LeftStickDown,
				Source = InputDeviceProfile.Analog(analog),
				SourceRange = InputRangeType.ZeroToMinusOne,
				TargetRange = InputRangeType.ZeroToOne
			};
		}

		// Token: 0x060002BF RID: 703 RVA: 0x00008B31 File Offset: 0x00006D31
		protected static InputControlMapping RightStickLeftMapping(int analog)
		{
			return new InputControlMapping
			{
				Name = "Right Stick Left",
				Target = InputControlType.RightStickLeft,
				Source = InputDeviceProfile.Analog(analog),
				SourceRange = InputRangeType.ZeroToMinusOne,
				TargetRange = InputRangeType.ZeroToOne
			};
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x00008B64 File Offset: 0x00006D64
		protected static InputControlMapping RightStickRightMapping(int analog)
		{
			return new InputControlMapping
			{
				Name = "Right Stick Right",
				Target = InputControlType.RightStickRight,
				Source = InputDeviceProfile.Analog(analog),
				SourceRange = InputRangeType.ZeroToOne,
				TargetRange = InputRangeType.ZeroToOne
			};
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x00008B98 File Offset: 0x00006D98
		protected static InputControlMapping RightStickUpMapping(int analog)
		{
			return new InputControlMapping
			{
				Name = "Right Stick Up",
				Target = InputControlType.RightStickUp,
				Source = InputDeviceProfile.Analog(analog),
				SourceRange = InputRangeType.ZeroToMinusOne,
				TargetRange = InputRangeType.ZeroToOne
			};
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x00008BCB File Offset: 0x00006DCB
		protected static InputControlMapping RightStickDownMapping(int analog)
		{
			return new InputControlMapping
			{
				Name = "Right Stick Down",
				Target = InputControlType.RightStickDown,
				Source = InputDeviceProfile.Analog(analog),
				SourceRange = InputRangeType.ZeroToOne,
				TargetRange = InputRangeType.ZeroToOne
			};
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x00008BFE File Offset: 0x00006DFE
		protected static InputControlMapping RightStickUpMapping2(int analog)
		{
			return new InputControlMapping
			{
				Name = "Right Stick Up",
				Target = InputControlType.RightStickUp,
				Source = InputDeviceProfile.Analog(analog),
				SourceRange = InputRangeType.ZeroToOne,
				TargetRange = InputRangeType.ZeroToOne
			};
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x00008C31 File Offset: 0x00006E31
		protected static InputControlMapping RightStickDownMapping2(int analog)
		{
			return new InputControlMapping
			{
				Name = "Right Stick Down",
				Target = InputControlType.RightStickDown,
				Source = InputDeviceProfile.Analog(analog),
				SourceRange = InputRangeType.ZeroToMinusOne,
				TargetRange = InputRangeType.ZeroToOne
			};
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x00008C64 File Offset: 0x00006E64
		protected static InputControlMapping LeftTriggerMapping(int analog, string name = "Left Trigger")
		{
			return new InputControlMapping
			{
				Name = name,
				Target = InputControlType.LeftTrigger,
				Source = InputDeviceProfile.Analog(analog),
				SourceRange = InputRangeType.MinusOneToOne,
				TargetRange = InputRangeType.ZeroToOne,
				IgnoreInitialZeroValue = true
			};
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x00008C9B File Offset: 0x00006E9B
		protected static InputControlMapping RightTriggerMapping(int analog, string name = "Right Trigger")
		{
			return new InputControlMapping
			{
				Name = name,
				Target = InputControlType.RightTrigger,
				Source = InputDeviceProfile.Analog(analog),
				SourceRange = InputRangeType.MinusOneToOne,
				TargetRange = InputRangeType.ZeroToOne,
				IgnoreInitialZeroValue = true
			};
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x00008CD2 File Offset: 0x00006ED2
		protected static InputControlMapping DPadLeftMapping(int analog)
		{
			return new InputControlMapping
			{
				Name = "DPad Left",
				Target = InputControlType.DPadLeft,
				Source = InputDeviceProfile.Analog(analog),
				SourceRange = InputRangeType.ZeroToMinusOne,
				TargetRange = InputRangeType.ZeroToOne
			};
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x00008D06 File Offset: 0x00006F06
		protected static InputControlMapping DPadRightMapping(int analog)
		{
			return new InputControlMapping
			{
				Name = "DPad Right",
				Target = InputControlType.DPadRight,
				Source = InputDeviceProfile.Analog(analog),
				SourceRange = InputRangeType.ZeroToOne,
				TargetRange = InputRangeType.ZeroToOne
			};
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x00008D3A File Offset: 0x00006F3A
		protected static InputControlMapping DPadUpMapping(int analog)
		{
			return new InputControlMapping
			{
				Name = "DPad Up",
				Target = InputControlType.DPadUp,
				Source = InputDeviceProfile.Analog(analog),
				SourceRange = InputRangeType.ZeroToMinusOne,
				TargetRange = InputRangeType.ZeroToOne
			};
		}

		// Token: 0x060002CA RID: 714 RVA: 0x00008D6E File Offset: 0x00006F6E
		protected static InputControlMapping DPadDownMapping(int analog)
		{
			return new InputControlMapping
			{
				Name = "DPad Down",
				Target = InputControlType.DPadDown,
				Source = InputDeviceProfile.Analog(analog),
				SourceRange = InputRangeType.ZeroToOne,
				TargetRange = InputRangeType.ZeroToOne
			};
		}

		// Token: 0x060002CB RID: 715 RVA: 0x00008DA2 File Offset: 0x00006FA2
		protected static InputControlMapping DPadUpMapping2(int analog)
		{
			return new InputControlMapping
			{
				Name = "DPad Up",
				Target = InputControlType.DPadUp,
				Source = InputDeviceProfile.Analog(analog),
				SourceRange = InputRangeType.ZeroToOne,
				TargetRange = InputRangeType.ZeroToOne
			};
		}

		// Token: 0x060002CC RID: 716 RVA: 0x00008DD6 File Offset: 0x00006FD6
		protected static InputControlMapping DPadDownMapping2(int analog)
		{
			return new InputControlMapping
			{
				Name = "DPad Down",
				Target = InputControlType.DPadDown,
				Source = InputDeviceProfile.Analog(analog),
				SourceRange = InputRangeType.ZeroToMinusOne,
				TargetRange = InputRangeType.ZeroToOne
			};
		}

		// Token: 0x060002CD RID: 717 RVA: 0x00008E0C File Offset: 0x0000700C
		public InputDeviceProfile()
		{
		}

		// Token: 0x060002CE RID: 718 RVA: 0x00008EB4 File Offset: 0x000070B4
		// Note: this type is marked as 'beforefieldinit'.
		static InputDeviceProfile()
		{
		}

		// Token: 0x0400029A RID: 666
		private static readonly HashSet<Type> hiddenProfiles = new HashSet<Type>();

		// Token: 0x0400029B RID: 667
		[SerializeField]
		private InputDeviceProfileType profileType;

		// Token: 0x0400029C RID: 668
		[SerializeField]
		private string deviceName = "";

		// Token: 0x0400029D RID: 669
		[SerializeField]
		[TextArea]
		private string deviceNotes = "";

		// Token: 0x0400029E RID: 670
		[SerializeField]
		private InputDeviceClass deviceClass;

		// Token: 0x0400029F RID: 671
		[SerializeField]
		private InputDeviceStyle deviceStyle;

		// Token: 0x040002A0 RID: 672
		[SerializeField]
		private float sensitivity = 1f;

		// Token: 0x040002A1 RID: 673
		[SerializeField]
		private float lowerDeadZone = 0.2f;

		// Token: 0x040002A2 RID: 674
		[SerializeField]
		private float upperDeadZone = 0.9f;

		// Token: 0x040002A3 RID: 675
		[SerializeField]
		private string[] includePlatforms = new string[0];

		// Token: 0x040002A4 RID: 676
		[SerializeField]
		private string[] excludePlatforms = new string[0];

		// Token: 0x040002A5 RID: 677
		[SerializeField]
		private int minSystemBuildNumber;

		// Token: 0x040002A6 RID: 678
		[SerializeField]
		private int maxSystemBuildNumber;

		// Token: 0x040002A7 RID: 679
		[SerializeField]
		private VersionInfo minUnityVersion = VersionInfo.Min;

		// Token: 0x040002A8 RID: 680
		[SerializeField]
		private VersionInfo maxUnityVersion = VersionInfo.Max;

		// Token: 0x040002A9 RID: 681
		[SerializeField]
		private InputDeviceMatcher[] matchers = new InputDeviceMatcher[0];

		// Token: 0x040002AA RID: 682
		[SerializeField]
		private InputDeviceMatcher[] lastResortMatchers = new InputDeviceMatcher[0];

		// Token: 0x040002AB RID: 683
		[SerializeField]
		private InputControlMapping[] analogMappings = new InputControlMapping[0];

		// Token: 0x040002AC RID: 684
		[SerializeField]
		private InputControlMapping[] buttonMappings = new InputControlMapping[0];

		// Token: 0x040002AD RID: 685
		protected static readonly InputControlSource MenuKey = new InputControlSource(KeyCode.Menu);

		// Token: 0x040002AE RID: 686
		protected static readonly InputControlSource EscapeKey = new InputControlSource(KeyCode.Escape);
	}
}
