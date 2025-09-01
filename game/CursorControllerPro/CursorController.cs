using System;
using System.Runtime.CompilerServices;
using SlimUI.CursorControllerPro.InputSystem;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SlimUI.CursorControllerPro
{
	// Token: 0x02000006 RID: 6
	public class CursorController : PointerInputModule
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000A RID: 10 RVA: 0x00002208 File Offset: 0x00000408
		// (set) Token: 0x0600000B RID: 11 RVA: 0x0000220F File Offset: 0x0000040F
		public static CursorController Instance
		{
			[CompilerGenerated]
			get
			{
				return CursorController.<Instance>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				CursorController.<Instance>k__BackingField = value;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000C RID: 12 RVA: 0x00002217 File Offset: 0x00000417
		public static GeneralSettings InstanceGeneralSettings
		{
			get
			{
				return CursorController.Instance._generalSettings;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000D RID: 13 RVA: 0x00002223 File Offset: 0x00000423
		public bool IsUsingController
		{
			get
			{
				return this.m_State == CursorController.eInputState.Controller;
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x0000222E File Offset: 0x0000042E
		protected override void Awake()
		{
			if (CursorController.Instance == null)
			{
				CursorController.Instance = this;
				this.Start();
				return;
			}
			UnityEngine.Object.Destroy(base.gameObject);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002258 File Offset: 0x00000458
		protected override void Start()
		{
			this.inputProvider = base.transform.Find("InputSystemProvider").GetComponent<IInputProvider>();
			this.CursorObjectsList = Resources.LoadAll("SlimUI/Prefabs/Cursors", typeof(UnityEngine.Object));
			this._generalSettings = Resources.Load<GeneralSettings>("SlimUI/Settings/GeneralSettings");
			this.generalSettingsInstance = UnityEngine.Object.Instantiate<GeneralSettings>(this._generalSettings);
			this.visibilityState = CursorController.cursorVisible;
			if (this.startEnabled)
			{
				this.canMoveCursor = true;
			}
			else
			{
				this.canMoveCursor = false;
			}
			this.StartingCursor();
			this.currentPlayerActive = 0;
			this.currentXAxis = this.horizontalAxis;
			this.currentYAxis = this.verticalAxis;
			this.tooltipController = base.GetComponent<TooltipController>();
			this.selfCanvas = base.GetComponent<Canvas>();
			this.boundaries = base.transform.GetComponent<RectTransform>();
			this.mouseEventSystem = this.mouseInputModule.GetComponent<EventSystem>();
			this.xMin = this.boundaries.rect.width / 2f * -1f + this.leftOffset;
			this.xMax = this.boundaries.rect.width / 2f + this.rightOffset;
			this.yMin = this.boundaries.rect.height / 2f * -1f + this.bottomOffset;
			this.yMax = this.boundaries.rect.height / 2f + this.topOffset;
			base.Start();
			this.pointer = new PointerEventData(base.eventSystem);
			this.cursorObjectPlayer1 = this.currentActiveCursorPlayer1;
			this.fade = this.cursorObjectPlayer1.GetComponent<Animator>();
			this.ChangeCursor(this.startingCursor);
			this.generalSettingsInstance.tempHspeed = this.generalSettingsInstance.horizontalSpeed;
			this.generalSettingsInstance.tempVspeed = this.generalSettingsInstance.verticalSpeed;
			CursorController.usingDesktopCursorStaticValue = this.startInDesktopMode;
			this.usingDesktopCursor = this.startInDesktopMode;
			CursorController.currentlyUsingDesktop = CursorController.usingDesktopCursorStaticValue;
			if (CursorController.currentlyUsingDesktop)
			{
				this.SwitchToMouse();
			}
			else
			{
				this.SwitchToController();
			}
			CursorController.Initialized = true;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000248C File Offset: 0x0000068C
		private void StartingCursor()
		{
			for (int i = 0; i < this.CursorObjectsList.Length; i++)
			{
				if (this.CursorObjectsList[i].name == this.startingCursor)
				{
					UnityEngine.Object.Destroy(this.currentActiveCursorPlayer1);
					GameObject gameObject = UnityEngine.Object.Instantiate(this.CursorObjectsList[i]) as GameObject;
					gameObject.transform.SetParent(this.cursorRect);
					gameObject.SetActive(true);
					gameObject.transform.localScale = new Vector3(this.generalSettingsInstance.cursorScale, this.generalSettingsInstance.cursorScale, this.generalSettingsInstance.cursorScale);
					gameObject.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0f, 0f, this.generalSettingsInstance.cursorZOffset);
					gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(gameObject.GetComponent<RectTransform>().anchoredPosition.x, gameObject.GetComponent<RectTransform>().anchoredPosition.y, 0f);
					this.currentActiveCursorPlayer1 = gameObject;
				}
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000259C File Offset: 0x0000079C
		public void ChangeCursor(string cursorNum)
		{
			for (int i = 0; i < this.CursorObjectsList.Length; i++)
			{
				if (this.CursorObjectsList[i].name == cursorNum)
				{
					UnityEngine.Object.Destroy(this.currentActiveCursorPlayer1);
					GameObject gameObject = UnityEngine.Object.Instantiate(this.CursorObjectsList[i]) as GameObject;
					gameObject.transform.SetParent(this.cursorRect);
					gameObject.SetActive(true);
					gameObject.transform.localScale = new Vector3(this.generalSettingsInstance.cursorScale, this.generalSettingsInstance.cursorScale, this.generalSettingsInstance.cursorScale);
					gameObject.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0f, 0f, this.generalSettingsInstance.cursorZOffset);
					this.currentActiveCursorPlayer1 = gameObject;
					this.cursorObjectPlayer1 = this.currentActiveCursorPlayer1;
					if (this.overrideTint)
					{
						gameObject.GetComponent<CursorTint>().SetColor(this.tint);
					}
				}
			}
			this.fade = this.cursorObjectPlayer1.GetComponent<Animator>();
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000026A4 File Offset: 0x000008A4
		public void UpdateOffsets(float newLeftOffset, float newRightOffset, float newBottomOffset, float newTopOffset)
		{
			this.xMin = this.boundaries.rect.width / 2f * -1f + newLeftOffset;
			this.xMax = this.boundaries.rect.width / 2f + newRightOffset;
			this.yMin = this.boundaries.rect.height / 2f * -1f + newBottomOffset;
			this.yMax = this.boundaries.rect.height / 2f + newTopOffset;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002742 File Offset: 0x00000942
		public void SwitchToMouse()
		{
			CursorController.cursorVisible = true;
			this.hasSwitchedToVirtualMouse = true;
			this.hasSwitchedToController = false;
			CursorController.currentlyUsingDesktop = true;
			base.GetComponent<EventSystem>().enabled = false;
			this.mouseInputModule.SetActive(true);
			this.currentActiveCursorPlayer1.SetActive(false);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002784 File Offset: 0x00000984
		public void SwitchToController()
		{
			CursorController.cursorVisible = true;
			this.hasSwitchedToVirtualMouse = false;
			this.hasSwitchedToController = true;
			CursorController.currentlyUsingDesktop = false;
			this.mouseInputModule.SetActive(false);
			base.GetComponent<EventSystem>().enabled = true;
			this.currentActiveCursorPlayer1.SetActive(true);
			this.cursorObjectPlayer1 = this.currentActiveCursorPlayer1;
			this.fade = this.cursorObjectPlayer1.GetComponent<Animator>();
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000027EC File Offset: 0x000009EC
		public void SwitchingToConsoleInput()
		{
			base.GetComponent<EventSystem>().enabled = true;
			base.GetComponent<CanvasGroup>().alpha = 1f;
			if (this.mouseEventSystem != null)
			{
				this.mouseEventSystem.enabled = false;
			}
			CursorController.currentlyUsingDesktop = false;
			CursorController.usingDesktopCursorStaticValue = false;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000283C File Offset: 0x00000A3C
		public void SwitchingToMouseInput()
		{
			base.GetComponent<EventSystem>().enabled = false;
			base.GetComponent<CanvasGroup>().alpha = 0f;
			if (this.mouseEventSystem != null)
			{
				this.mouseEventSystem.enabled = true;
			}
			CursorController.currentlyUsingDesktop = true;
			CursorController.usingDesktopCursorStaticValue = true;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000288C File Offset: 0x00000A8C
		private void SelectedPlayerMovement()
		{
			Vector3 position;
			if (this.selfCanvas.renderMode == RenderMode.ScreenSpaceOverlay)
			{
				position = this.inputProvider.GetAbsolutePosition();
			}
			else
			{
				position = this.cameraMain.ScreenToWorldPoint(this.inputProvider.GetAbsolutePosition());
			}
			if (!this.usingDesktopCursor)
			{
				this.CursorBoundaries(this.cursorRect);
				return;
			}
			if (this.usingDesktopCursor)
			{
				this.cursorRect.position = position;
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002900 File Offset: 0x00000B00
		private void SetMovementToCurrentPlayerInput()
		{
			Vector2 vector = Vector2.zero;
			vector = this.inputProvider.GetRelativeMovement(GamepadPlayerNum.One);
			this.xMovement = Time.unscaledDeltaTime * (this.generalSettingsInstance.horizontalSpeed * 100f) * vector.x;
			this.yMovement = Time.unscaledDeltaTime * (this.generalSettingsInstance.verticalSpeed * 100f) * vector.y;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002968 File Offset: 0x00000B68
		public void ChangeActivePlayer(int activePlayerIndex)
		{
			this.currentPlayerActive = 0;
			this.cursorRect.gameObject.SetActive(true);
			this.cursorObjectPlayer1.SetActive(true);
			this.currentActiveCursor = this.currentActiveCursorPlayer1;
			this.currentXAxis = this.horizontalAxis;
			this.currentYAxis = this.verticalAxis;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000029C0 File Offset: 0x00000BC0
		private void CheckDeviceInput()
		{
			if (this.usingDesktopCursor && !this.hasSwitchedToVirtualMouse)
			{
				CursorController.usingDesktopCursorStaticValue = true;
				this.SwitchToMouse();
				return;
			}
			if (!this.usingDesktopCursor && !this.hasSwitchedToController)
			{
				CursorController.usingDesktopCursorStaticValue = false;
				if (!this.hasSwitchedToController)
				{
					this.SwitchToController();
				}
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002A10 File Offset: 0x00000C10
		private void CursorBoundaries(RectTransform rect)
		{
			rect.anchoredPosition += new Vector2(this.xMovement, this.yMovement);
			float x = rect.anchoredPosition.x;
			float y = rect.anchoredPosition.y;
			if (x < this.xMin)
			{
				x = this.xMin;
			}
			if (x > this.xMax)
			{
				x = this.xMax;
			}
			if (y < this.yMin)
			{
				y = this.yMin;
			}
			if (y > this.yMax)
			{
				y = this.yMax;
			}
			rect.anchoredPosition = new Vector3(x, y);
			rect.localPosition = new Vector3(rect.localPosition.x, rect.localPosition.y, 0f);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002AD0 File Offset: 0x00000CD0
		public override void Process()
		{
			Vector3 vector = this.cameraMain.WorldToScreenPoint(this.cursorObjectPlayer1.transform.position);
			if (this.currentPlayerActive == 0)
			{
				vector = this.cameraMain.WorldToScreenPoint(this.cursorObjectPlayer1.transform.position);
			}
			this.screenVec.x = vector.x;
			this.screenVec.y = vector.y;
			this.pointer.position = this.screenVec;
			base.eventSystem.RaycastAll(this.pointer, this.m_RaycastResultCache);
			RaycastResult raycastResult = BaseInputModule.FindFirstRaycast(this.m_RaycastResultCache);
			this.pointer.pointerCurrentRaycast = raycastResult;
			if (raycastResult.distance < this.generalSettingsInstance.maxDetectionDistance)
			{
				this.ProcessMove(this.pointer);
				this.ProcessDrag(this.pointer);
				if (this.inputProvider.GetSubmitWasPressed())
				{
					base.CancelInvoke("HoldingDown");
					if (this.allowHoldRepeat)
					{
						base.InvokeRepeating("HoldingDown", 0f, this.pressHoldRepeatTime);
					}
				}
				if (this.inputProvider.GetSubmitWasReleased())
				{
					base.CancelInvoke("HoldingDown");
				}
				if (this.canRepeatSubmit)
				{
					this.canRepeatSubmit = false;
					this.pointer.pressPosition = this.screenVec;
					this.pointer.clickTime = Time.unscaledTime;
					this.pointer.pointerPressRaycast = raycastResult;
					this.pointer.eligibleForClick = true;
					float clickTime = this.pointer.clickTime;
					float num = this.lastClickTime;
					this.lastClickTime = Time.unscaledTime;
					if (this.m_RaycastResultCache.Count > 0)
					{
						this.pointer.selectedObject = raycastResult.gameObject;
						this.pointer.pointerPress = ExecuteEvents.ExecuteHierarchy<ISubmitHandler>(raycastResult.gameObject, this.pointer, ExecuteEvents.submitHandler);
						this.pointer.rawPointerPress = raycastResult.gameObject;
						return;
					}
					this.pointer.rawPointerPress = null;
					return;
				}
				else
				{
					this.pointer.eligibleForClick = false;
					this.pointer.pointerPress = null;
					this.pointer.pointerDrag = null;
					this.pointer.rawPointerPress = null;
				}
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002CF6 File Offset: 0x00000EF6
		private void HoldingDown()
		{
			this.canRepeatSubmit = true;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002D00 File Offset: 0x00000F00
		private bool ShouldStartDrag(Vector2 pressPos, Vector2 currentPos, float threshold, bool useDragThreshold)
		{
			return !useDragThreshold || (pressPos - currentPos).sqrMagnitude >= threshold * threshold;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002D2C File Offset: 0x00000F2C
		protected override void ProcessDrag(PointerEventData pointerEvent)
		{
			if (pointerEvent.pointerDrag == null)
			{
				return;
			}
			if (!pointerEvent.dragging && this.ShouldStartDrag(pointerEvent.pressPosition, pointerEvent.position, (float)base.eventSystem.pixelDragThreshold, pointerEvent.useDragThreshold))
			{
				ExecuteEvents.Execute<IBeginDragHandler>(pointerEvent.pointerDrag, pointerEvent, ExecuteEvents.beginDragHandler);
				pointerEvent.dragging = true;
			}
			if (pointerEvent.dragging)
			{
				if (pointerEvent.pointerPress != pointerEvent.pointerDrag)
				{
					ExecuteEvents.Execute<IPointerUpHandler>(pointerEvent.pointerPress, pointerEvent, ExecuteEvents.pointerUpHandler);
					pointerEvent.eligibleForClick = false;
					pointerEvent.pointerPress = null;
					pointerEvent.rawPointerPress = null;
				}
				ExecuteEvents.Execute<IDragHandler>(pointerEvent.pointerDrag, pointerEvent, ExecuteEvents.dragHandler);
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002DE4 File Offset: 0x00000FE4
		private void OnGUI()
		{
			if (this.canMoveCursor)
			{
				CursorController.eInputState state = this.m_State;
				if (state != CursorController.eInputState.MouseKeyboard)
				{
					if (state != CursorController.eInputState.Controller)
					{
						return;
					}
					if (this.inputProvider.GetActiveInputType() == InputType.MouseAndKeyboard)
					{
						this.m_State = CursorController.eInputState.MouseKeyboard;
						this.usingDesktopCursor = true;
						if (this.debugLogging)
						{
							Debug.Log("Switching to MOUSE & KEYBOARD Input");
						}
					}
				}
				else if (this.inputProvider.GetActiveInputType() == InputType.Gamepad)
				{
					this.m_State = CursorController.eInputState.Controller;
					this.usingDesktopCursor = false;
					if (this.debugLogging)
					{
						Debug.Log("Switching to HANDHELD Input");
						return;
					}
				}
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002E65 File Offset: 0x00001065
		public CursorController.eInputState GetInputState()
		{
			return this.m_State;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002E6D File Offset: 0x0000106D
		public void EnableUserControl()
		{
			this.canMoveCursor = true;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002E76 File Offset: 0x00001076
		public void PauseGame()
		{
			this.canMoveCursor = true;
			CursorController.cursorVisible = true;
			if (this.hasSwitchedToVirtualMouse)
			{
				this.SwitchToMouse();
				return;
			}
			this.SwitchToController();
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002E9A File Offset: 0x0000109A
		public void ResumeGame()
		{
			this.canMoveCursor = false;
			CursorController.cursorVisible = false;
			this.tooltipController.tooltipRect.GetComponent<Animator>().SetBool("Show", false);
			this.currentActiveCursorPlayer1.SetActive(false);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002ED0 File Offset: 0x000010D0
		public void HoverSpeed()
		{
			this.generalSettingsInstance.horizontalSpeed = this.generalSettingsInstance.horizontalSpeed * this.generalSettingsInstance.hoverMultiplier;
			this.generalSettingsInstance.verticalSpeed = this.generalSettingsInstance.verticalSpeed * this.generalSettingsInstance.hoverMultiplier;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002F21 File Offset: 0x00001121
		public void NormalSpeed()
		{
			this.generalSettingsInstance.horizontalSpeed = this.generalSettingsInstance.tempHspeed;
			this.generalSettingsInstance.verticalSpeed = this.generalSettingsInstance.tempVspeed;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002F50 File Offset: 0x00001150
		public void FadeIn()
		{
			this.tooltipController.countTimer = true;
			if (this.fade.GetComponent<Animator>())
			{
				while (this.tooltipController.timer < this.tooltipController.popUpDelay && this.tooltipController.countTimer)
				{
					this.tooltipController.timer = this.tooltipController.timer + Time.deltaTime;
				}
				if (this.tooltipController.timer >= this.tooltipController.popUpDelay)
				{
					this.fade.SetBool("Fade", true);
					this.tooltipController.countTimer = false;
					this.tooltipController.timer = 0f;
				}
			}
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00003008 File Offset: 0x00001208
		public void FadeOut()
		{
			if (this.fade.GetComponent<Animator>())
			{
				this.fade.SetBool("Fade", false);
				this.tooltipController.countTimer = false;
				this.tooltipController.timer = 0f;
			}
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00003054 File Offset: 0x00001254
		private void Update()
		{
			this.SetMovementToCurrentPlayerInput();
			if (this.mouseInputModule)
			{
				this.CheckDeviceInput();
			}
			else if (!this.mouseInputModule)
			{
				Debug.LogWarning("There is no Mouse Input game object in the scene! Please add one.");
			}
			if (this.canMoveCursor)
			{
				this.SelectedPlayerMovement();
				this.tooltipController.ToolTipPositions();
				this.tooltipController.ToolTipBoundaries(this.cursorRect);
			}
			if (this.tooltipController.countTimer)
			{
				this.tooltipController.ToolTipPopUpDelay();
			}
			if (this.visibilityState != CursorController.cursorVisible)
			{
				this.visibilityState = CursorController.cursorVisible;
				this.CursorStateChange();
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000030F3 File Offset: 0x000012F3
		public void CursorStateChange()
		{
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000030F8 File Offset: 0x000012F8
		public CursorController()
		{
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00003172 File Offset: 0x00001372
		// Note: this type is marked as 'beforefieldinit'.
		static CursorController()
		{
		}

		// Token: 0x04000011 RID: 17
		[CompilerGenerated]
		private static CursorController <Instance>k__BackingField;

		// Token: 0x04000012 RID: 18
		private CursorController.eInputState m_State;

		// Token: 0x04000013 RID: 19
		private GeneralSettings _generalSettings;

		// Token: 0x04000014 RID: 20
		private GeneralSettings generalSettingsInstance;

		// Token: 0x04000015 RID: 21
		private IInputProvider inputProvider;

		// Token: 0x04000016 RID: 22
		private UnityEngine.Object[] CursorObjectsList;

		// Token: 0x04000017 RID: 23
		private GameObject currentActiveCursorPlayer1;

		// Token: 0x04000018 RID: 24
		private GameObject currentActiveCursor;

		// Token: 0x04000019 RID: 25
		[Header("PLAYER CURSORS")]
		public string startingCursor;

		// Token: 0x0400001A RID: 26
		[Tooltip("The axis name in the Input Manager")]
		public string horizontalAxis = "Horizontal";

		// Token: 0x0400001B RID: 27
		[Tooltip("The axis name in the Input Manager")]
		public string verticalAxis = "Vertical";

		// Token: 0x0400001C RID: 28
		public bool canMoveCursor;

		// Token: 0x0400001D RID: 29
		[Header("CURSOR CONTROLS")]
		[Tooltip("If enabled, the game will start with Mouse Input instead of a handheld controller.")]
		public bool startInDesktopMode = true;

		// Token: 0x0400001E RID: 30
		public bool startEnabled = true;

		// Token: 0x0400001F RID: 31
		private bool usingDesktopCursor;

		// Token: 0x04000020 RID: 32
		[Space]
		[HideInInspector]
		public int currentPlayerActive;

		// Token: 0x04000021 RID: 33
		[HideInInspector]
		public GameObject cursorObjectPlayer1;

		// Token: 0x04000022 RID: 34
		[Space]
		[Tooltip("The camera in your scene being used as the Main Camera")]
		public Camera cameraMain;

		// Token: 0x04000023 RID: 35
		private Canvas selfCanvas;

		// Token: 0x04000024 RID: 36
		private string currentXAxis;

		// Token: 0x04000025 RID: 37
		private string currentYAxis;

		// Token: 0x04000026 RID: 38
		[Tooltip("If you want to switch between Mouse Input and Hand-Held Controller Input, add a game object with a Standalone Input Module attached and call it through the function.")]
		[HideInInspector]
		public EventSystem mouseEventSystem;

		// Token: 0x04000027 RID: 39
		[Tooltip("The rect UI elements functioning as the CURSOR")]
		public RectTransform cursorRect;

		// Token: 0x04000028 RID: 40
		[Tooltip("The Rect Transform whose boundaries will be used to calculate edge of screen")]
		private RectTransform boundaries;

		// Token: 0x04000029 RID: 41
		private float lastClickTime;

		// Token: 0x0400002A RID: 42
		[Tooltip("The speed that the button repeat presses when holding down the Submit action.")]
		[Range(0.1f, 0.5f)]
		public float pressHoldRepeatTime = 0.25f;

		// Token: 0x0400002B RID: 43
		[Tooltip("Is the user allow to hold a button down to repeat presses.")]
		public bool allowHoldRepeat = true;

		// Token: 0x0400002C RID: 44
		[HideInInspector]
		public bool canRepeatSubmit = true;

		// Token: 0x0400002D RID: 45
		[HideInInspector]
		public Animator fade;

		// Token: 0x0400002E RID: 46
		public static bool cursorVisible = true;

		// Token: 0x0400002F RID: 47
		private bool visibilityState;

		// Token: 0x04000030 RID: 48
		public static bool usingDesktopCursorStaticValue = false;

		// Token: 0x04000031 RID: 49
		public static bool currentlyUsingDesktop = false;

		// Token: 0x04000032 RID: 50
		private bool hasSwitchedToVirtualMouse;

		// Token: 0x04000033 RID: 51
		private bool hasSwitchedToController;

		// Token: 0x04000034 RID: 52
		public GameObject mouseInputModule;

		// Token: 0x04000035 RID: 53
		[Header("EDGE BOUNDS")]
		[Tooltip("Right from the LEFT of the screen limiting cursor movement. To move the offset right, use positive values (Ex. 100)")]
		public float leftOffset;

		// Token: 0x04000036 RID: 54
		[Tooltip("Right from the RIGHT of the screen limiting cursor movement. To move the offset left, use negative values (Ex. -100)")]
		public float rightOffset;

		// Token: 0x04000037 RID: 55
		[Tooltip("Offset from BOTTOM of screen limiting cursor movement. To move the offset up, use positive values (Ex. 100)")]
		public float bottomOffset;

		// Token: 0x04000038 RID: 56
		[Tooltip("Offset from TOP of screen limiting cursor movement. To move the offset down, use negative values (Ex. -100)")]
		public float topOffset;

		// Token: 0x04000039 RID: 57
		[HideInInspector]
		public float xMin;

		// Token: 0x0400003A RID: 58
		[HideInInspector]
		public float xMax;

		// Token: 0x0400003B RID: 59
		[HideInInspector]
		public float yMin;

		// Token: 0x0400003C RID: 60
		[HideInInspector]
		public float yMax;

		// Token: 0x0400003D RID: 61
		[HideInInspector]
		public float xMovement;

		// Token: 0x0400003E RID: 62
		[HideInInspector]
		public float yMovement;

		// Token: 0x0400003F RID: 63
		private Vector2 screenVec;

		// Token: 0x04000040 RID: 64
		public PointerEventData pointer;

		// Token: 0x04000041 RID: 65
		[HideInInspector]
		public TooltipController tooltipController;

		// Token: 0x04000042 RID: 66
		[Header("COLORS")]
		[Tooltip("If true, the tint color on this CursorControl prefab will override the tint set on each cursor prefab, unless the image component has a 'TintBypass' component attached.")]
		public bool overrideTint;

		// Token: 0x04000043 RID: 67
		public Color tint = new Color(1f, 1f, 1f, 1f);

		// Token: 0x04000044 RID: 68
		[Header("PARALLAX")]
		[Tooltip("The speed at which RectTransform objects that have the ParallaxWindow' script component move with the cursor.")]
		[Range(0f, 0.25f)]
		public float parallaxStrength = 0.05f;

		// Token: 0x04000045 RID: 69
		[Header("DEMO STUFF")]
		[Tooltip("If enabled, certain actions with the controller will print Debug.Log() for.")]
		public bool debugLogging;

		// Token: 0x04000046 RID: 70
		private static bool Initialized;

		// Token: 0x02000015 RID: 21
		public enum eInputState
		{
			// Token: 0x0400008E RID: 142
			MouseKeyboard,
			// Token: 0x0400008F RID: 143
			Controller
		}
	}
}
