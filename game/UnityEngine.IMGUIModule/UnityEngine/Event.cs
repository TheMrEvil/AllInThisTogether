using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000004 RID: 4
	[StaticAccessor("GUIEvent", StaticAccessorType.DoubleColon)]
	[NativeHeader("Modules/IMGUI/Event.bindings.h")]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class Event
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3
		[NativeProperty("type", false, TargetType.Field)]
		public extern EventType rawType { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000004 RID: 4 RVA: 0x0000205C File Offset: 0x0000025C
		// (set) Token: 0x06000005 RID: 5 RVA: 0x00002072 File Offset: 0x00000272
		[NativeProperty("mousePosition", false, TargetType.Field)]
		public Vector2 mousePosition
		{
			get
			{
				Vector2 result;
				this.get_mousePosition_Injected(out result);
				return result;
			}
			set
			{
				this.set_mousePosition_Injected(ref value);
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000006 RID: 6 RVA: 0x0000207C File Offset: 0x0000027C
		// (set) Token: 0x06000007 RID: 7 RVA: 0x00002092 File Offset: 0x00000292
		[NativeProperty("delta", false, TargetType.Field)]
		public Vector2 delta
		{
			get
			{
				Vector2 result;
				this.get_delta_Injected(out result);
				return result;
			}
			set
			{
				this.set_delta_Injected(ref value);
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000008 RID: 8
		// (set) Token: 0x06000009 RID: 9
		[NativeProperty("pointerType", false, TargetType.Field)]
		public extern PointerType pointerType { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000A RID: 10
		// (set) Token: 0x0600000B RID: 11
		[NativeProperty("button", false, TargetType.Field)]
		public extern int button { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000C RID: 12
		// (set) Token: 0x0600000D RID: 13
		[NativeProperty("modifiers", false, TargetType.Field)]
		public extern EventModifiers modifiers { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000E RID: 14
		// (set) Token: 0x0600000F RID: 15
		[NativeProperty("pressure", false, TargetType.Field)]
		public extern float pressure { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000010 RID: 16
		// (set) Token: 0x06000011 RID: 17
		[NativeProperty("clickCount", false, TargetType.Field)]
		public extern int clickCount { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000012 RID: 18
		// (set) Token: 0x06000013 RID: 19
		[NativeProperty("character", false, TargetType.Field)]
		public extern char character { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000014 RID: 20
		// (set) Token: 0x06000015 RID: 21
		[NativeProperty("keycode", false, TargetType.Field)]
		public extern KeyCode keyCode { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000016 RID: 22
		// (set) Token: 0x06000017 RID: 23
		[NativeProperty("displayIndex", false, TargetType.Field)]
		public extern int displayIndex { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000018 RID: 24
		// (set) Token: 0x06000019 RID: 25
		public extern EventType type { [FreeFunction("GUIEvent::GetType", HasExplicitThis = true)] [MethodImpl(MethodImplOptions.InternalCall)] get; [FreeFunction("GUIEvent::SetType", HasExplicitThis = true)] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600001A RID: 26
		// (set) Token: 0x0600001B RID: 27
		public extern string commandName { [FreeFunction("GUIEvent::GetCommandName", HasExplicitThis = true)] [MethodImpl(MethodImplOptions.InternalCall)] get; [FreeFunction("GUIEvent::SetCommandName", HasExplicitThis = true)] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x0600001C RID: 28
		[NativeMethod("Use")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_Use();

		// Token: 0x0600001D RID: 29
		[FreeFunction("GUIEvent::Internal_Create", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Internal_Create(int displayIndex);

		// Token: 0x0600001E RID: 30
		[FreeFunction("GUIEvent::Internal_Destroy", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Destroy(IntPtr ptr);

		// Token: 0x0600001F RID: 31
		[FreeFunction("GUIEvent::Internal_Copy", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Internal_Copy(IntPtr otherPtr);

		// Token: 0x06000020 RID: 32
		[FreeFunction("GUIEvent::GetTypeForControl", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern EventType GetTypeForControl(int controlID);

		// Token: 0x06000021 RID: 33
		[VisibleToOtherModules(new string[]
		{
			"UnityEngine.UIElementsModule"
		})]
		[FreeFunction("GUIEvent::CopyFromPtr", IsThreadSafe = true, HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void CopyFromPtr(IntPtr ptr);

		// Token: 0x06000022 RID: 34
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool PopEvent([NotNull("ArgumentNullException")] Event outEvent);

		// Token: 0x06000023 RID: 35
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetEventCount();

		// Token: 0x06000024 RID: 36
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetNativeEvent(IntPtr ptr);

		// Token: 0x06000025 RID: 37 RVA: 0x0000209C File Offset: 0x0000029C
		[RequiredByNativeCode]
		internal static void Internal_MakeMasterEventCurrent(int displayIndex)
		{
			bool flag = Event.s_MasterEvent == null;
			if (flag)
			{
				Event.s_MasterEvent = new Event(displayIndex);
			}
			Event.s_MasterEvent.displayIndex = displayIndex;
			Event.s_Current = Event.s_MasterEvent;
			Event.Internal_SetNativeEvent(Event.s_MasterEvent.m_Ptr);
		}

		// Token: 0x06000026 RID: 38
		[VisibleToOtherModules(new string[]
		{
			"UnityEngine.UIElementsModule"
		})]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetDoubleClickTime();

		// Token: 0x06000027 RID: 39 RVA: 0x000020E7 File Offset: 0x000002E7
		public Event()
		{
			this.m_Ptr = Event.Internal_Create(0);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000020FD File Offset: 0x000002FD
		public Event(int displayIndex)
		{
			this.m_Ptr = Event.Internal_Create(displayIndex);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002114 File Offset: 0x00000314
		public Event(Event other)
		{
			bool flag = other == null;
			if (flag)
			{
				throw new ArgumentException("Event to copy from is null.");
			}
			this.m_Ptr = Event.Internal_Copy(other.m_Ptr);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002150 File Offset: 0x00000350
		protected override void Finalize()
		{
			try
			{
				bool flag = this.m_Ptr != IntPtr.Zero;
				if (flag)
				{
					Event.Internal_Destroy(this.m_Ptr);
					this.m_Ptr = IntPtr.Zero;
				}
			}
			finally
			{
				base.Finalize();
			}
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000021A8 File Offset: 0x000003A8
		internal static void CleanupRoots()
		{
			Event.s_Current = null;
			Event.s_MasterEvent = null;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000021B8 File Offset: 0x000003B8
		internal void CopyFrom(Event e)
		{
			bool flag = e.m_Ptr != this.m_Ptr;
			if (flag)
			{
				this.CopyFromPtr(e.m_Ptr);
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600002D RID: 45 RVA: 0x000021EC File Offset: 0x000003EC
		// (set) Token: 0x0600002E RID: 46 RVA: 0x0000220D File Offset: 0x0000040D
		[Obsolete("Use HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public Ray mouseRay
		{
			get
			{
				return new Ray(Vector3.up, Vector3.up);
			}
			set
			{
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600002F RID: 47 RVA: 0x00002210 File Offset: 0x00000410
		// (set) Token: 0x06000030 RID: 48 RVA: 0x00002230 File Offset: 0x00000430
		public bool shift
		{
			get
			{
				return (this.modifiers & EventModifiers.Shift) > EventModifiers.None;
			}
			set
			{
				bool flag = !value;
				if (flag)
				{
					this.modifiers &= ~EventModifiers.Shift;
				}
				else
				{
					this.modifiers |= EventModifiers.Shift;
				}
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00002268 File Offset: 0x00000468
		// (set) Token: 0x06000032 RID: 50 RVA: 0x00002288 File Offset: 0x00000488
		public bool control
		{
			get
			{
				return (this.modifiers & EventModifiers.Control) > EventModifiers.None;
			}
			set
			{
				bool flag = !value;
				if (flag)
				{
					this.modifiers &= ~EventModifiers.Control;
				}
				else
				{
					this.modifiers |= EventModifiers.Control;
				}
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000033 RID: 51 RVA: 0x000022C0 File Offset: 0x000004C0
		// (set) Token: 0x06000034 RID: 52 RVA: 0x000022E0 File Offset: 0x000004E0
		public bool alt
		{
			get
			{
				return (this.modifiers & EventModifiers.Alt) > EventModifiers.None;
			}
			set
			{
				bool flag = !value;
				if (flag)
				{
					this.modifiers &= ~EventModifiers.Alt;
				}
				else
				{
					this.modifiers |= EventModifiers.Alt;
				}
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002318 File Offset: 0x00000518
		// (set) Token: 0x06000036 RID: 54 RVA: 0x00002338 File Offset: 0x00000538
		public bool command
		{
			get
			{
				return (this.modifiers & EventModifiers.Command) > EventModifiers.None;
			}
			set
			{
				bool flag = !value;
				if (flag)
				{
					this.modifiers &= ~EventModifiers.Command;
				}
				else
				{
					this.modifiers |= EventModifiers.Command;
				}
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00002370 File Offset: 0x00000570
		// (set) Token: 0x06000038 RID: 56 RVA: 0x00002390 File Offset: 0x00000590
		public bool capsLock
		{
			get
			{
				return (this.modifiers & EventModifiers.CapsLock) > EventModifiers.None;
			}
			set
			{
				bool flag = !value;
				if (flag)
				{
					this.modifiers &= ~EventModifiers.CapsLock;
				}
				else
				{
					this.modifiers |= EventModifiers.CapsLock;
				}
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000039 RID: 57 RVA: 0x000023C8 File Offset: 0x000005C8
		// (set) Token: 0x0600003A RID: 58 RVA: 0x000023E8 File Offset: 0x000005E8
		public bool numeric
		{
			get
			{
				return (this.modifiers & EventModifiers.Numeric) > EventModifiers.None;
			}
			set
			{
				bool flag = !value;
				if (flag)
				{
					this.modifiers &= ~EventModifiers.Numeric;
				}
				else
				{
					this.modifiers |= EventModifiers.Numeric;
				}
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00002420 File Offset: 0x00000620
		public bool functionKey
		{
			get
			{
				return (this.modifiers & EventModifiers.FunctionKey) > EventModifiers.None;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002430 File Offset: 0x00000630
		// (set) Token: 0x0600003D RID: 61 RVA: 0x00002447 File Offset: 0x00000647
		public static Event current
		{
			get
			{
				return Event.s_Current;
			}
			set
			{
				Event.s_Current = (value ?? Event.s_MasterEvent);
				Event.Internal_SetNativeEvent(Event.s_Current.m_Ptr);
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600003E RID: 62 RVA: 0x0000246C File Offset: 0x0000066C
		public bool isKey
		{
			get
			{
				EventType type = this.type;
				return type == EventType.KeyDown || type == EventType.KeyUp;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00002490 File Offset: 0x00000690
		public bool isMouse
		{
			get
			{
				EventType type = this.type;
				return type == EventType.MouseMove || type == EventType.MouseDown || type == EventType.MouseUp || type == EventType.MouseDrag || type == EventType.ContextClick || type == EventType.MouseEnterWindow || type == EventType.MouseLeaveWindow;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000040 RID: 64 RVA: 0x000024CC File Offset: 0x000006CC
		public bool isScrollWheel
		{
			get
			{
				EventType type = this.type;
				return type == EventType.ScrollWheel;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000041 RID: 65 RVA: 0x000024EC File Offset: 0x000006EC
		internal bool isDirectManipulationDevice
		{
			get
			{
				return this.pointerType == PointerType.Pen || this.pointerType == PointerType.Touch;
			}
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002514 File Offset: 0x00000714
		public static Event KeyboardEvent(string key)
		{
			Event @event = new Event(0)
			{
				type = EventType.KeyDown
			};
			bool flag = string.IsNullOrEmpty(key);
			Event result;
			if (flag)
			{
				result = @event;
			}
			else
			{
				int num = 0;
				for (;;)
				{
					bool flag2 = true;
					bool flag3 = num >= key.Length;
					if (flag3)
					{
						break;
					}
					char c = key[num];
					char c2 = c;
					switch (c2)
					{
					case '#':
						@event.modifiers |= EventModifiers.Shift;
						num++;
						break;
					case '$':
						goto IL_CA;
					case '%':
						@event.modifiers |= EventModifiers.Command;
						num++;
						break;
					case '&':
						@event.modifiers |= EventModifiers.Alt;
						num++;
						break;
					default:
						if (c2 != '^')
						{
							goto IL_CA;
						}
						@event.modifiers |= EventModifiers.Control;
						num++;
						break;
					}
					IL_CE:
					if (!flag2)
					{
						break;
					}
					continue;
					IL_CA:
					flag2 = false;
					goto IL_CE;
				}
				string text = key.Substring(num, key.Length - num).ToLowerInvariant();
				string text2 = text;
				string text3 = text2;
				uint num2 = <PrivateImplementationDetails>.ComputeStringHash(text3);
				if (num2 <= 2049299002U)
				{
					if (num2 <= 1035581717U)
					{
						if (num2 <= 388133425U)
						{
							if (num2 <= 306900080U)
							{
								if (num2 != 203579616U)
								{
									if (num2 != 220357235U)
									{
										if (num2 == 306900080U)
										{
											if (text3 == "left")
											{
												@event.keyCode = KeyCode.LeftArrow;
												@event.modifiers |= EventModifiers.FunctionKey;
												goto IL_EF8;
											}
										}
									}
									else if (text3 == "f8")
									{
										@event.keyCode = KeyCode.F8;
										@event.modifiers |= EventModifiers.FunctionKey;
										goto IL_EF8;
									}
								}
								else if (text3 == "f9")
								{
									@event.keyCode = KeyCode.F9;
									@event.modifiers |= EventModifiers.FunctionKey;
									goto IL_EF8;
								}
							}
							else if (num2 != 337800568U)
							{
								if (num2 != 371355806U)
								{
									if (num2 == 388133425U)
									{
										if (text3 == "f2")
										{
											@event.keyCode = KeyCode.F2;
											@event.modifiers |= EventModifiers.FunctionKey;
											goto IL_EF8;
										}
									}
								}
								else if (text3 == "f3")
								{
									@event.keyCode = KeyCode.F3;
									@event.modifiers |= EventModifiers.FunctionKey;
									goto IL_EF8;
								}
							}
							else if (text3 == "f1")
							{
								@event.keyCode = KeyCode.F1;
								@event.modifiers |= EventModifiers.FunctionKey;
								goto IL_EF8;
							}
						}
						else if (num2 <= 438466282U)
						{
							if (num2 != 404911044U)
							{
								if (num2 != 421688663U)
								{
									if (num2 == 438466282U)
									{
										if (text3 == "f7")
										{
											@event.keyCode = KeyCode.F7;
											@event.modifiers |= EventModifiers.FunctionKey;
											goto IL_EF8;
										}
									}
								}
								else if (text3 == "f4")
								{
									@event.keyCode = KeyCode.F4;
									@event.modifiers |= EventModifiers.FunctionKey;
									goto IL_EF8;
								}
							}
							else if (text3 == "f5")
							{
								@event.keyCode = KeyCode.F5;
								@event.modifiers |= EventModifiers.FunctionKey;
								goto IL_EF8;
							}
						}
						else if (num2 != 455243901U)
						{
							if (num2 != 894689925U)
							{
								if (num2 == 1035581717U)
								{
									if (text3 == "down")
									{
										@event.keyCode = KeyCode.DownArrow;
										@event.modifiers |= EventModifiers.FunctionKey;
										goto IL_EF8;
									}
								}
							}
							else if (text3 == "space")
							{
								@event.keyCode = KeyCode.Space;
								@event.character = ' ';
								@event.modifiers &= ~EventModifiers.FunctionKey;
								goto IL_EF8;
							}
						}
						else if (text3 == "f6")
						{
							@event.keyCode = KeyCode.F6;
							@event.modifiers |= EventModifiers.FunctionKey;
							goto IL_EF8;
						}
					}
					else if (num2 <= 1980614408U)
					{
						if (num2 <= 1193063839U)
						{
							if (num2 != 1113118030U)
							{
								if (num2 != 1128467232U)
								{
									if (num2 == 1193063839U)
									{
										if (text3 == "page up")
										{
											@event.keyCode = KeyCode.PageUp;
											@event.modifiers |= EventModifiers.FunctionKey;
											goto IL_EF8;
										}
									}
								}
								else if (text3 == "up")
								{
									@event.keyCode = KeyCode.UpArrow;
									@event.modifiers |= EventModifiers.FunctionKey;
									goto IL_EF8;
								}
							}
							else if (text3 == "[equals]")
							{
								@event.character = '=';
								@event.keyCode = KeyCode.KeypadEquals;
								goto IL_EF8;
							}
						}
						else if (num2 != 1740784714U)
						{
							if (num2 != 1787721130U)
							{
								if (num2 == 1980614408U)
								{
									if (text3 == "[=]")
									{
										@event.character = '=';
										@event.keyCode = KeyCode.KeypadEquals;
										goto IL_EF8;
									}
								}
							}
							else if (text3 == "end")
							{
								@event.keyCode = KeyCode.End;
								@event.modifiers |= EventModifiers.FunctionKey;
								goto IL_EF8;
							}
						}
						else if (text3 == "delete")
						{
							@event.keyCode = KeyCode.Delete;
							@event.modifiers |= EventModifiers.FunctionKey;
							goto IL_EF8;
						}
					}
					else if (num2 <= 1981894336U)
					{
						if (num2 != 1980761503U)
						{
							if (num2 != 1981202788U)
							{
								if (num2 == 1981894336U)
								{
									if (text3 == "[5]")
									{
										@event.character = '5';
										@event.keyCode = KeyCode.Keypad5;
										goto IL_EF8;
									}
								}
							}
							else if (text3 == "[1]")
							{
								@event.character = '1';
								@event.keyCode = KeyCode.Keypad1;
								goto IL_EF8;
							}
						}
						else if (text3 == "[2]")
						{
							@event.character = '2';
							@event.keyCode = KeyCode.Keypad2;
							goto IL_EF8;
						}
					}
					else if (num2 != 2028154341U)
					{
						if (num2 != 2048857717U)
						{
							if (num2 == 2049299002U)
							{
								if (text3 == "[+]")
								{
									@event.character = '+';
									@event.keyCode = KeyCode.KeypadPlus;
									goto IL_EF8;
								}
							}
						}
						else if (text3 == "[4]")
						{
							@event.character = '4';
							@event.keyCode = KeyCode.Keypad4;
							goto IL_EF8;
						}
					}
					else if (text3 == "right")
					{
						@event.keyCode = KeyCode.RightArrow;
						@event.modifiers |= EventModifiers.FunctionKey;
						goto IL_EF8;
					}
				}
				else if (num2 <= 3121933785U)
				{
					if (num2 <= 3053690476U)
					{
						if (num2 <= 2235328556U)
						{
							if (num2 != 2049990550U)
							{
								if (num2 != 2130866490U)
								{
									if (num2 == 2235328556U)
									{
										if (text3 == "backspace")
										{
											@event.keyCode = KeyCode.Backspace;
											@event.modifiers |= EventModifiers.FunctionKey;
											goto IL_EF8;
										}
									}
								}
								else if (text3 == "page down")
								{
									@event.keyCode = KeyCode.PageDown;
									@event.modifiers |= EventModifiers.FunctionKey;
									goto IL_EF8;
								}
							}
							else if (text3 == "[/]")
							{
								@event.character = '/';
								@event.keyCode = KeyCode.KeypadDivide;
								goto IL_EF8;
							}
						}
						else if (num2 != 2246981567U)
						{
							if (num2 != 2566336076U)
							{
								if (num2 == 3053690476U)
								{
									if (text3 == "[9]")
									{
										@event.character = '9';
										@event.keyCode = KeyCode.Keypad9;
										goto IL_EF8;
									}
								}
							}
							else if (text3 == "tab")
							{
								@event.keyCode = KeyCode.Tab;
								goto IL_EF8;
							}
						}
						else if (text3 == "return")
						{
							@event.character = '\n';
							@event.keyCode = KeyCode.Return;
							@event.modifiers &= ~EventModifiers.FunctionKey;
							goto IL_EF8;
						}
					}
					else if (num2 <= 3056941880U)
					{
						if (num2 != 3055117499U)
						{
							if (num2 != 3056397427U)
							{
								if (num2 == 3056941880U)
								{
									if (text3 == "[-]")
									{
										@event.character = '-';
										@event.keyCode = KeyCode.KeypadMinus;
										goto IL_EF8;
									}
								}
							}
							else if (text3 == "[.]")
							{
								@event.character = '.';
								@event.keyCode = KeyCode.KeypadPeriod;
								goto IL_EF8;
							}
						}
						else if (text3 == "[6]")
						{
							@event.character = '6';
							@event.keyCode = KeyCode.Keypad6;
							goto IL_EF8;
						}
					}
					else if (num2 != 3120653857U)
					{
						if (num2 != 3121786690U)
						{
							if (num2 == 3121933785U)
							{
								if (text3 == "[0]")
								{
									@event.character = '0';
									@event.keyCode = KeyCode.Keypad0;
									goto IL_EF8;
								}
							}
						}
						else if (text3 == "[3]")
						{
							@event.character = '3';
							@event.keyCode = KeyCode.Keypad3;
							goto IL_EF8;
						}
					}
					else if (text3 == "[8]")
					{
						@event.character = '8';
						@event.keyCode = KeyCode.Keypad8;
						goto IL_EF8;
					}
				}
				else if (num2 <= 4197582936U)
				{
					if (num2 <= 3536372366U)
					{
						if (num2 != 3122375070U)
						{
							if (num2 != 3332609576U)
							{
								if (num2 == 3536372366U)
								{
									if (text3 == "home")
									{
										@event.keyCode = KeyCode.Home;
										@event.modifiers |= EventModifiers.FunctionKey;
										goto IL_EF8;
									}
								}
							}
							else if (text3 == "insert")
							{
								@event.keyCode = KeyCode.Insert;
								@event.modifiers |= EventModifiers.FunctionKey;
								goto IL_EF8;
							}
						}
						else if (text3 == "[7]")
						{
							@event.character = '7';
							@event.keyCode = KeyCode.Keypad7;
							goto IL_EF8;
						}
					}
					else if (num2 != 3906143141U)
					{
						if (num2 != 3984432914U)
						{
							if (num2 == 4197582936U)
							{
								if (text3 == "f10")
								{
									@event.keyCode = KeyCode.F10;
									@event.modifiers |= EventModifiers.FunctionKey;
									goto IL_EF8;
								}
							}
						}
						else if (text3 == "[esc]")
						{
							@event.keyCode = KeyCode.Escape;
							goto IL_EF8;
						}
					}
					else if (text3 == "pgup")
					{
						@event.keyCode = KeyCode.PageDown;
						@event.modifiers |= EventModifiers.FunctionKey;
						goto IL_EF8;
					}
				}
				else if (num2 <= 4227375619U)
				{
					if (num2 != 4213014532U)
					{
						if (num2 != 4214360555U)
						{
							if (num2 == 4227375619U)
							{
								if (text3 == "[enter]")
								{
									@event.character = '\n';
									@event.keyCode = KeyCode.KeypadEnter;
									goto IL_EF8;
								}
							}
						}
						else if (text3 == "f11")
						{
							@event.keyCode = KeyCode.F11;
							@event.modifiers |= EventModifiers.FunctionKey;
							goto IL_EF8;
						}
					}
					else if (text3 == "pgdown")
					{
						@event.keyCode = KeyCode.PageUp;
						@event.modifiers |= EventModifiers.FunctionKey;
						goto IL_EF8;
					}
				}
				else if (num2 <= 4247915793U)
				{
					if (num2 != 4231138174U)
					{
						if (num2 == 4247915793U)
						{
							if (text3 == "f13")
							{
								@event.keyCode = KeyCode.F13;
								@event.modifiers |= EventModifiers.FunctionKey;
								goto IL_EF8;
							}
						}
					}
					else if (text3 == "f12")
					{
						@event.keyCode = KeyCode.F12;
						@event.modifiers |= EventModifiers.FunctionKey;
						goto IL_EF8;
					}
				}
				else if (num2 != 4264693412U)
				{
					if (num2 == 4281471031U)
					{
						if (text3 == "f15")
						{
							@event.keyCode = KeyCode.F15;
							@event.modifiers |= EventModifiers.FunctionKey;
							goto IL_EF8;
						}
					}
				}
				else if (text3 == "f14")
				{
					@event.keyCode = KeyCode.F14;
					@event.modifiers |= EventModifiers.FunctionKey;
					goto IL_EF8;
				}
				bool flag4 = text.Length != 1;
				if (flag4)
				{
					try
					{
						@event.keyCode = (KeyCode)Enum.Parse(typeof(KeyCode), text, true);
					}
					catch (ArgumentException)
					{
						Debug.LogError(UnityString.Format("Unable to find key name that matches '{0}'", new object[]
						{
							text
						}));
					}
				}
				else
				{
					@event.character = text.ToLower()[0];
					@event.keyCode = (KeyCode)@event.character;
					bool flag5 = @event.modifiers > EventModifiers.None;
					if (flag5)
					{
						@event.character = '\0';
					}
				}
				IL_EF8:
				result = @event;
			}
			return result;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00003430 File Offset: 0x00001630
		public override int GetHashCode()
		{
			int num = 1;
			bool isKey = this.isKey;
			if (isKey)
			{
				num = (int)((ushort)this.keyCode);
			}
			bool isMouse = this.isMouse;
			if (isMouse)
			{
				num = this.mousePosition.GetHashCode();
			}
			return num * 37 | (int)this.modifiers;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00003484 File Offset: 0x00001684
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = this == obj;
				if (flag2)
				{
					result = true;
				}
				else
				{
					bool flag3 = obj.GetType() != base.GetType();
					if (flag3)
					{
						result = false;
					}
					else
					{
						Event @event = (Event)obj;
						bool flag4 = this.type != @event.type || (this.modifiers & ~EventModifiers.CapsLock) != (@event.modifiers & ~EventModifiers.CapsLock);
						if (flag4)
						{
							result = false;
						}
						else
						{
							bool isKey = this.isKey;
							if (isKey)
							{
								result = (this.keyCode == @event.keyCode);
							}
							else
							{
								bool isMouse = this.isMouse;
								result = (isMouse && this.mousePosition == @event.mousePosition);
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00003548 File Offset: 0x00001748
		public override string ToString()
		{
			bool isKey = this.isKey;
			string result;
			if (isKey)
			{
				bool flag = this.character == '\0';
				if (flag)
				{
					result = UnityString.Format("Event:{0}   Character:\\0   Modifiers:{1}   KeyCode:{2}", new object[]
					{
						this.type,
						this.modifiers,
						this.keyCode
					});
				}
				else
				{
					result = string.Concat(new string[]
					{
						"Event:",
						this.type.ToString(),
						"   Character:",
						((int)this.character).ToString(),
						"   Modifiers:",
						this.modifiers.ToString(),
						"   KeyCode:",
						this.keyCode.ToString()
					});
				}
			}
			else
			{
				bool isMouse = this.isMouse;
				if (isMouse)
				{
					result = UnityString.Format("Event: {0}   Position: {1} Modifiers: {2}", new object[]
					{
						this.type,
						this.mousePosition,
						this.modifiers
					});
				}
				else
				{
					bool flag2 = this.type == EventType.ExecuteCommand || this.type == EventType.ValidateCommand;
					if (flag2)
					{
						result = UnityString.Format("Event: {0}  \"{1}\"", new object[]
						{
							this.type,
							this.commandName
						});
					}
					else
					{
						result = (this.type.ToString() ?? "");
					}
				}
			}
			return result;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000036EC File Offset: 0x000018EC
		public void Use()
		{
			bool flag = this.type == EventType.Repaint || this.type == EventType.Layout;
			if (flag)
			{
				Debug.LogWarning(UnityString.Format("Event.Use() should not be called for events of type {0}", new object[]
				{
					this.type
				}));
			}
			this.Internal_Use();
		}

		// Token: 0x06000047 RID: 71
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_mousePosition_Injected(out Vector2 ret);

		// Token: 0x06000048 RID: 72
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_mousePosition_Injected(ref Vector2 value);

		// Token: 0x06000049 RID: 73
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_delta_Injected(out Vector2 ret);

		// Token: 0x0600004A RID: 74
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_delta_Injected(ref Vector2 value);

		// Token: 0x04000001 RID: 1
		[NonSerialized]
		internal IntPtr m_Ptr;

		// Token: 0x04000002 RID: 2
		private static Event s_Current;

		// Token: 0x04000003 RID: 3
		private static Event s_MasterEvent;
	}
}
