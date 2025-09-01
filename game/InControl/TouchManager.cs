using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;

namespace InControl
{
	// Token: 0x02000058 RID: 88
	[ExecuteInEditMode]
	public class TouchManager : SingletonMonoBehavior<TouchManager>
	{
		// Token: 0x1400000F RID: 15
		// (add) Token: 0x06000425 RID: 1061 RVA: 0x0000EDE8 File Offset: 0x0000CFE8
		// (remove) Token: 0x06000426 RID: 1062 RVA: 0x0000EE1C File Offset: 0x0000D01C
		public static event Action OnSetup
		{
			[CompilerGenerated]
			add
			{
				Action action = TouchManager.OnSetup;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref TouchManager.OnSetup, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = TouchManager.OnSetup;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref TouchManager.OnSetup, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x0000EE4F File Offset: 0x0000D04F
		protected TouchManager()
		{
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x0000EE78 File Offset: 0x0000D078
		private void OnEnable()
		{
			if (base.GetComponent<InControlManager>() == null)
			{
				Logger.LogError("Touch Manager component can only be added to the InControl Manager object.");
				UnityEngine.Object.DestroyImmediate(this);
				return;
			}
			if (base.EnforceSingleton)
			{
				return;
			}
			this.touchControls = base.GetComponentsInChildren<TouchControl>(true);
			if (Application.isPlaying)
			{
				InputManager.OnSetup += this.Setup;
				InputManager.OnUpdateDevices += this.UpdateDevice;
				InputManager.OnCommitDevices += this.CommitDevice;
			}
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x0000EEF4 File Offset: 0x0000D0F4
		private void OnDisable()
		{
			if (Application.isPlaying)
			{
				InputManager.OnSetup -= this.Setup;
				InputManager.OnUpdateDevices -= this.UpdateDevice;
				InputManager.OnCommitDevices -= this.CommitDevice;
			}
			this.Reset();
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x0000EF41 File Offset: 0x0000D141
		private void Setup()
		{
			this.UpdateScreenSize(this.GetCurrentScreenSize());
			this.CreateDevice();
			this.CreateTouches();
			if (TouchManager.OnSetup != null)
			{
				TouchManager.OnSetup();
				TouchManager.OnSetup = null;
			}
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x0000EF74 File Offset: 0x0000D174
		private void Reset()
		{
			this.device = null;
			for (int i = 0; i < 3; i++)
			{
				this.mouseTouches[i] = null;
			}
			this.cachedTouches = null;
			this.activeTouches = null;
			this.readOnlyActiveTouches = null;
			this.touchControls = null;
			TouchManager.OnSetup = null;
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x0000EFBF File Offset: 0x0000D1BF
		private IEnumerator UpdateScreenSizeAtEndOfFrame()
		{
			yield return new WaitForEndOfFrame();
			this.UpdateScreenSize(this.GetCurrentScreenSize());
			yield return null;
			yield break;
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x0000EFD0 File Offset: 0x0000D1D0
		private void Update()
		{
			Vector2 currentScreenSize = this.GetCurrentScreenSize();
			if (!this.isReady)
			{
				base.StartCoroutine(this.UpdateScreenSizeAtEndOfFrame());
				this.UpdateScreenSize(currentScreenSize);
				this.isReady = true;
				return;
			}
			if (this.screenSize != currentScreenSize)
			{
				this.UpdateScreenSize(currentScreenSize);
			}
			if (TouchManager.OnSetup != null)
			{
				TouchManager.OnSetup();
				TouchManager.OnSetup = null;
			}
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x0000F034 File Offset: 0x0000D234
		private void CreateDevice()
		{
			this.device = new TouchInputDevice();
			this.device.AddControl(InputControlType.LeftStickLeft, "LeftStickLeft");
			this.device.AddControl(InputControlType.LeftStickRight, "LeftStickRight");
			this.device.AddControl(InputControlType.LeftStickUp, "LeftStickUp");
			this.device.AddControl(InputControlType.LeftStickDown, "LeftStickDown");
			this.device.AddControl(InputControlType.RightStickLeft, "RightStickLeft");
			this.device.AddControl(InputControlType.RightStickRight, "RightStickRight");
			this.device.AddControl(InputControlType.RightStickUp, "RightStickUp");
			this.device.AddControl(InputControlType.RightStickDown, "RightStickDown");
			this.device.AddControl(InputControlType.DPadUp, "DPadUp");
			this.device.AddControl(InputControlType.DPadDown, "DPadDown");
			this.device.AddControl(InputControlType.DPadLeft, "DPadLeft");
			this.device.AddControl(InputControlType.DPadRight, "DPadRight");
			this.device.AddControl(InputControlType.LeftTrigger, "LeftTrigger");
			this.device.AddControl(InputControlType.RightTrigger, "RightTrigger");
			this.device.AddControl(InputControlType.LeftBumper, "LeftBumper");
			this.device.AddControl(InputControlType.RightBumper, "RightBumper");
			for (InputControlType inputControlType = InputControlType.Action1; inputControlType <= InputControlType.Action12; inputControlType++)
			{
				this.device.AddControl(inputControlType, inputControlType.ToString());
			}
			this.device.AddControl(InputControlType.Menu, "Menu");
			for (InputControlType inputControlType2 = InputControlType.Button0; inputControlType2 <= InputControlType.Button19; inputControlType2++)
			{
				this.device.AddControl(inputControlType2, inputControlType2.ToString());
			}
			InputManager.AttachDevice(this.device);
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x0000F1E9 File Offset: 0x0000D3E9
		private void UpdateDevice(ulong updateTick, float deltaTime)
		{
			this.UpdateTouches(updateTick, deltaTime);
			this.SubmitControlStates(updateTick, deltaTime);
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x0000F1FB File Offset: 0x0000D3FB
		private void CommitDevice(ulong updateTick, float deltaTime)
		{
			this.CommitControlStates(updateTick, deltaTime);
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x0000F208 File Offset: 0x0000D408
		private void SubmitControlStates(ulong updateTick, float deltaTime)
		{
			int num = this.touchControls.Length;
			for (int i = 0; i < num; i++)
			{
				TouchControl touchControl = this.touchControls[i];
				if (touchControl.enabled && touchControl.gameObject.activeInHierarchy)
				{
					touchControl.SubmitControlState(updateTick, deltaTime);
				}
			}
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x0000F250 File Offset: 0x0000D450
		private void CommitControlStates(ulong updateTick, float deltaTime)
		{
			int num = this.touchControls.Length;
			for (int i = 0; i < num; i++)
			{
				TouchControl touchControl = this.touchControls[i];
				if (touchControl.enabled && touchControl.gameObject.activeInHierarchy)
				{
					touchControl.CommitControlState(updateTick, deltaTime);
				}
			}
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x0000F298 File Offset: 0x0000D498
		private void UpdateScreenSize(Vector2 currentScreenSize)
		{
			this.touchCamera.rect = new Rect(0f, 0f, 0.99f, 1f);
			this.touchCamera.rect = new Rect(0f, 0f, 1f, 1f);
			this.screenSize = currentScreenSize;
			this.halfScreenSize = this.screenSize / 2f;
			this.viewSize = this.ConvertViewToWorldPoint(Vector2.one) * 0.02f;
			this.percentToWorld = Mathf.Min(this.viewSize.x, this.viewSize.y);
			this.halfPercentToWorld = this.percentToWorld / 2f;
			if (this.touchCamera != null)
			{
				this.halfPixelToWorld = this.touchCamera.orthographicSize / this.screenSize.y;
				this.pixelToWorld = this.halfPixelToWorld * 2f;
			}
			if (this.touchControls != null)
			{
				int num = this.touchControls.Length;
				for (int i = 0; i < num; i++)
				{
					this.touchControls[i].ConfigureControl();
				}
			}
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x0000F3C0 File Offset: 0x0000D5C0
		private void CreateTouches()
		{
			this.cachedTouches = new TouchPool();
			for (int i = 0; i < 3; i++)
			{
				this.mouseTouches[i] = new Touch();
				this.mouseTouches[i].fingerId = -2;
			}
			this.activeTouches = new List<Touch>(32);
			this.readOnlyActiveTouches = new ReadOnlyCollection<Touch>(this.activeTouches);
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x0000F420 File Offset: 0x0000D620
		private void UpdateTouches(ulong updateTick, float deltaTime)
		{
			this.activeTouches.Clear();
			this.cachedTouches.FreeEndedTouches();
			for (int i = 0; i < 3; i++)
			{
				if (this.mouseTouches[i].SetWithMouseData(i, updateTick, deltaTime))
				{
					this.activeTouches.Add(this.mouseTouches[i]);
				}
			}
			for (int j = 0; j < Input.touchCount; j++)
			{
				Touch touch = Input.GetTouch(j);
				Touch touch2 = this.cachedTouches.FindOrCreateTouch(touch.fingerId);
				touch2.SetWithTouchData(touch, updateTick, deltaTime);
				this.activeTouches.Add(touch2);
			}
			int count = this.cachedTouches.Touches.Count;
			for (int k = 0; k < count; k++)
			{
				Touch touch3 = this.cachedTouches.Touches[k];
				if (touch3.phase != TouchPhase.Ended && touch3.updateTick != updateTick)
				{
					touch3.phase = TouchPhase.Ended;
					this.activeTouches.Add(touch3);
				}
			}
			this.InvokeTouchEvents();
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x0000F51C File Offset: 0x0000D71C
		private void SendTouchBegan(Touch touch)
		{
			int num = this.touchControls.Length;
			for (int i = 0; i < num; i++)
			{
				TouchControl touchControl = this.touchControls[i];
				if (touchControl.enabled && touchControl.gameObject.activeInHierarchy)
				{
					touchControl.TouchBegan(touch);
				}
			}
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x0000F564 File Offset: 0x0000D764
		private void SendTouchMoved(Touch touch)
		{
			int num = this.touchControls.Length;
			for (int i = 0; i < num; i++)
			{
				TouchControl touchControl = this.touchControls[i];
				if (touchControl.enabled && touchControl.gameObject.activeInHierarchy)
				{
					touchControl.TouchMoved(touch);
				}
			}
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x0000F5AC File Offset: 0x0000D7AC
		private void SendTouchEnded(Touch touch)
		{
			int num = this.touchControls.Length;
			for (int i = 0; i < num; i++)
			{
				TouchControl touchControl = this.touchControls[i];
				if (touchControl.enabled && touchControl.gameObject.activeInHierarchy)
				{
					touchControl.TouchEnded(touch);
				}
			}
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x0000F5F4 File Offset: 0x0000D7F4
		private void InvokeTouchEvents()
		{
			int count = this.activeTouches.Count;
			if (this.enableControlsOnTouch && count > 0 && !this.controlsEnabled)
			{
				TouchManager.Device.RequestActivation();
				this.controlsEnabled = true;
			}
			for (int i = 0; i < count; i++)
			{
				Touch touch = this.activeTouches[i];
				switch (touch.phase)
				{
				case TouchPhase.Began:
					this.SendTouchBegan(touch);
					break;
				case TouchPhase.Moved:
					this.SendTouchMoved(touch);
					break;
				case TouchPhase.Stationary:
					break;
				case TouchPhase.Ended:
					this.SendTouchEnded(touch);
					break;
				case TouchPhase.Canceled:
					this.SendTouchEnded(touch);
					break;
				default:
					throw new ArgumentOutOfRangeException();
				}
			}
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x0000F698 File Offset: 0x0000D898
		private bool TouchCameraIsValid()
		{
			return !(this.touchCamera == null) && !Utility.IsZero(this.touchCamera.orthographicSize) && (!Utility.IsZero(this.touchCamera.rect.width) || !Utility.IsZero(this.touchCamera.rect.height)) && (!Utility.IsZero(this.touchCamera.pixelRect.width) || !Utility.IsZero(this.touchCamera.pixelRect.height));
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x0000F738 File Offset: 0x0000D938
		private Vector3 ConvertScreenToWorldPoint(Vector2 point)
		{
			if (this.TouchCameraIsValid())
			{
				return this.touchCamera.ScreenToWorldPoint(new Vector3(point.x, point.y, -this.touchCamera.transform.position.z));
			}
			return Vector3.zero;
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x0000F788 File Offset: 0x0000D988
		private Vector3 ConvertViewToWorldPoint(Vector2 point)
		{
			if (this.TouchCameraIsValid())
			{
				return this.touchCamera.ViewportToWorldPoint(new Vector3(point.x, point.y, -this.touchCamera.transform.position.z));
			}
			return Vector3.zero;
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x0000F7D8 File Offset: 0x0000D9D8
		private Vector3 ConvertScreenToViewPoint(Vector2 point)
		{
			if (this.TouchCameraIsValid())
			{
				return this.touchCamera.ScreenToViewportPoint(new Vector3(point.x, point.y, -this.touchCamera.transform.position.z));
			}
			return Vector3.zero;
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x0000F825 File Offset: 0x0000DA25
		private Vector2 GetCurrentScreenSize()
		{
			if (this.TouchCameraIsValid())
			{
				return new Vector2((float)this.touchCamera.pixelWidth, (float)this.touchCamera.pixelHeight);
			}
			return new Vector2((float)Screen.width, (float)Screen.height);
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x0600043F RID: 1087 RVA: 0x0000F85E File Offset: 0x0000DA5E
		// (set) Token: 0x06000440 RID: 1088 RVA: 0x0000F868 File Offset: 0x0000DA68
		public bool controlsEnabled
		{
			get
			{
				return this._controlsEnabled;
			}
			set
			{
				if (this._controlsEnabled != value)
				{
					int num = this.touchControls.Length;
					for (int i = 0; i < num; i++)
					{
						this.touchControls[i].enabled = value;
					}
					this._controlsEnabled = value;
				}
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000441 RID: 1089 RVA: 0x0000F8A8 File Offset: 0x0000DAA8
		public static ReadOnlyCollection<Touch> Touches
		{
			get
			{
				return SingletonMonoBehavior<TouchManager>.Instance.readOnlyActiveTouches;
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000442 RID: 1090 RVA: 0x0000F8B4 File Offset: 0x0000DAB4
		public static int TouchCount
		{
			get
			{
				return SingletonMonoBehavior<TouchManager>.Instance.activeTouches.Count;
			}
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x0000F8C5 File Offset: 0x0000DAC5
		public static Touch GetTouch(int touchIndex)
		{
			return SingletonMonoBehavior<TouchManager>.Instance.activeTouches[touchIndex];
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x0000F8D7 File Offset: 0x0000DAD7
		public static Touch GetTouchByFingerId(int fingerId)
		{
			return SingletonMonoBehavior<TouchManager>.Instance.cachedTouches.FindTouch(fingerId);
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x0000F8E9 File Offset: 0x0000DAE9
		public static Vector3 ScreenToWorldPoint(Vector2 point)
		{
			return SingletonMonoBehavior<TouchManager>.Instance.ConvertScreenToWorldPoint(point);
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x0000F8F6 File Offset: 0x0000DAF6
		public static Vector3 ViewToWorldPoint(Vector2 point)
		{
			return SingletonMonoBehavior<TouchManager>.Instance.ConvertViewToWorldPoint(point);
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x0000F903 File Offset: 0x0000DB03
		public static Vector3 ScreenToViewPoint(Vector2 point)
		{
			return SingletonMonoBehavior<TouchManager>.Instance.ConvertScreenToViewPoint(point);
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x0000F910 File Offset: 0x0000DB10
		public static float ConvertToWorld(float value, TouchUnitType unitType)
		{
			return value * ((unitType == TouchUnitType.Pixels) ? TouchManager.PixelToWorld : TouchManager.PercentToWorld);
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x0000F924 File Offset: 0x0000DB24
		public static Rect PercentToWorldRect(Rect rect)
		{
			return new Rect((rect.xMin - 50f) * TouchManager.ViewSize.x, (rect.yMin - 50f) * TouchManager.ViewSize.y, rect.width * TouchManager.ViewSize.x, rect.height * TouchManager.ViewSize.y);
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x0000F98C File Offset: 0x0000DB8C
		public static Rect PixelToWorldRect(Rect rect)
		{
			return new Rect(Mathf.Round(rect.xMin - TouchManager.HalfScreenSize.x) * TouchManager.PixelToWorld, Mathf.Round(rect.yMin - TouchManager.HalfScreenSize.y) * TouchManager.PixelToWorld, Mathf.Round(rect.width) * TouchManager.PixelToWorld, Mathf.Round(rect.height) * TouchManager.PixelToWorld);
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x0000F9FC File Offset: 0x0000DBFC
		public static Rect ConvertToWorld(Rect rect, TouchUnitType unitType)
		{
			if (unitType != TouchUnitType.Pixels)
			{
				return TouchManager.PercentToWorldRect(rect);
			}
			return TouchManager.PixelToWorldRect(rect);
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x0600044C RID: 1100 RVA: 0x0000FA0F File Offset: 0x0000DC0F
		public static Camera Camera
		{
			get
			{
				return SingletonMonoBehavior<TouchManager>.Instance.touchCamera;
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x0600044D RID: 1101 RVA: 0x0000FA1B File Offset: 0x0000DC1B
		public static InputDevice Device
		{
			get
			{
				return SingletonMonoBehavior<TouchManager>.Instance.device;
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x0600044E RID: 1102 RVA: 0x0000FA27 File Offset: 0x0000DC27
		public static Vector3 ViewSize
		{
			get
			{
				return SingletonMonoBehavior<TouchManager>.Instance.viewSize;
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x0600044F RID: 1103 RVA: 0x0000FA33 File Offset: 0x0000DC33
		public static float PercentToWorld
		{
			get
			{
				return SingletonMonoBehavior<TouchManager>.Instance.percentToWorld;
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000450 RID: 1104 RVA: 0x0000FA3F File Offset: 0x0000DC3F
		public static float HalfPercentToWorld
		{
			get
			{
				return SingletonMonoBehavior<TouchManager>.Instance.halfPercentToWorld;
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000451 RID: 1105 RVA: 0x0000FA4B File Offset: 0x0000DC4B
		public static float PixelToWorld
		{
			get
			{
				return SingletonMonoBehavior<TouchManager>.Instance.pixelToWorld;
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000452 RID: 1106 RVA: 0x0000FA57 File Offset: 0x0000DC57
		public static float HalfPixelToWorld
		{
			get
			{
				return SingletonMonoBehavior<TouchManager>.Instance.halfPixelToWorld;
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000453 RID: 1107 RVA: 0x0000FA63 File Offset: 0x0000DC63
		public static Vector2 ScreenSize
		{
			get
			{
				return SingletonMonoBehavior<TouchManager>.Instance.screenSize;
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000454 RID: 1108 RVA: 0x0000FA6F File Offset: 0x0000DC6F
		public static Vector2 HalfScreenSize
		{
			get
			{
				return SingletonMonoBehavior<TouchManager>.Instance.halfScreenSize;
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000455 RID: 1109 RVA: 0x0000FA7B File Offset: 0x0000DC7B
		public static TouchManager.GizmoShowOption ControlsShowGizmos
		{
			get
			{
				return SingletonMonoBehavior<TouchManager>.Instance.controlsShowGizmos;
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000456 RID: 1110 RVA: 0x0000FA87 File Offset: 0x0000DC87
		// (set) Token: 0x06000457 RID: 1111 RVA: 0x0000FA93 File Offset: 0x0000DC93
		public static bool ControlsEnabled
		{
			get
			{
				return SingletonMonoBehavior<TouchManager>.Instance.controlsEnabled;
			}
			set
			{
				SingletonMonoBehavior<TouchManager>.Instance.controlsEnabled = value;
			}
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x0000FAA0 File Offset: 0x0000DCA0
		public static implicit operator bool(TouchManager instance)
		{
			return instance != null;
		}

		// Token: 0x040003BE RID: 958
		[Space(10f)]
		public Camera touchCamera;

		// Token: 0x040003BF RID: 959
		public TouchManager.GizmoShowOption controlsShowGizmos = TouchManager.GizmoShowOption.Always;

		// Token: 0x040003C0 RID: 960
		[HideInInspector]
		public bool enableControlsOnTouch;

		// Token: 0x040003C1 RID: 961
		[SerializeField]
		[HideInInspector]
		private bool _controlsEnabled = true;

		// Token: 0x040003C2 RID: 962
		[HideInInspector]
		public int controlsLayer = 5;

		// Token: 0x040003C3 RID: 963
		[CompilerGenerated]
		private static Action OnSetup;

		// Token: 0x040003C4 RID: 964
		private InputDevice device;

		// Token: 0x040003C5 RID: 965
		private Vector3 viewSize;

		// Token: 0x040003C6 RID: 966
		private Vector2 screenSize;

		// Token: 0x040003C7 RID: 967
		private Vector2 halfScreenSize;

		// Token: 0x040003C8 RID: 968
		private float percentToWorld;

		// Token: 0x040003C9 RID: 969
		private float halfPercentToWorld;

		// Token: 0x040003CA RID: 970
		private float pixelToWorld;

		// Token: 0x040003CB RID: 971
		private float halfPixelToWorld;

		// Token: 0x040003CC RID: 972
		private TouchControl[] touchControls;

		// Token: 0x040003CD RID: 973
		private TouchPool cachedTouches;

		// Token: 0x040003CE RID: 974
		private List<Touch> activeTouches;

		// Token: 0x040003CF RID: 975
		private ReadOnlyCollection<Touch> readOnlyActiveTouches;

		// Token: 0x040003D0 RID: 976
		private bool isReady;

		// Token: 0x040003D1 RID: 977
		private readonly Touch[] mouseTouches = new Touch[3];

		// Token: 0x02000217 RID: 535
		public enum GizmoShowOption
		{
			// Token: 0x04000493 RID: 1171
			Never,
			// Token: 0x04000494 RID: 1172
			WhenSelected,
			// Token: 0x04000495 RID: 1173
			UnlessPlaying,
			// Token: 0x04000496 RID: 1174
			Always
		}

		// Token: 0x02000218 RID: 536
		[CompilerGenerated]
		private sealed class <UpdateScreenSizeAtEndOfFrame>d__28 : IEnumerator<object>, IEnumerator, IDisposable
		{
			// Token: 0x06000919 RID: 2329 RVA: 0x00052D78 File Offset: 0x00050F78
			[DebuggerHidden]
			public <UpdateScreenSizeAtEndOfFrame>d__28(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x0600091A RID: 2330 RVA: 0x00052D87 File Offset: 0x00050F87
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x0600091B RID: 2331 RVA: 0x00052D8C File Offset: 0x00050F8C
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				TouchManager touchManager = this;
				switch (num)
				{
				case 0:
					this.<>1__state = -1;
					this.<>2__current = new WaitForEndOfFrame();
					this.<>1__state = 1;
					return true;
				case 1:
					this.<>1__state = -1;
					touchManager.UpdateScreenSize(touchManager.GetCurrentScreenSize());
					this.<>2__current = null;
					this.<>1__state = 2;
					return true;
				case 2:
					this.<>1__state = -1;
					return false;
				default:
					return false;
				}
			}

			// Token: 0x1700018C RID: 396
			// (get) Token: 0x0600091C RID: 2332 RVA: 0x00052E01 File Offset: 0x00051001
			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0600091D RID: 2333 RVA: 0x00052E09 File Offset: 0x00051009
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x1700018D RID: 397
			// (get) Token: 0x0600091E RID: 2334 RVA: 0x00052E10 File Offset: 0x00051010
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x04000497 RID: 1175
			private int <>1__state;

			// Token: 0x04000498 RID: 1176
			private object <>2__current;

			// Token: 0x04000499 RID: 1177
			public TouchManager <>4__this;
		}
	}
}
