using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using QFSW.QC;
using UnityEngine;

// Token: 0x02000090 RID: 144
public class PlayerInput : MonoBehaviour
{
	// Token: 0x1700008F RID: 143
	// (get) Token: 0x06000683 RID: 1667 RVA: 0x0003046E File Offset: 0x0002E66E
	public InputActions actions
	{
		get
		{
			return InputManager.Actions;
		}
	}

	// Token: 0x06000684 RID: 1668 RVA: 0x00030475 File Offset: 0x0002E675
	private void Awake()
	{
		this.Control = base.GetComponent<PlayerControl>();
	}

	// Token: 0x06000685 RID: 1669 RVA: 0x00030483 File Offset: 0x0002E683
	private void Start()
	{
		if (this.isLocalControl)
		{
			PlayerInput.myInstance = this;
		}
	}

	// Token: 0x06000686 RID: 1670 RVA: 0x00030494 File Offset: 0x0002E694
	private void Update()
	{
		if (this.isLocalControl)
		{
			this.CheckInputIgnore();
			this.rawInput = new Vector2(this.actions.Move.X, this.actions.Move.Y);
			this.movementAxis = Vector2.ClampMagnitude(this.rawInput, 1f) * (float)(this.ignoreInput ? 0 : 1);
			Vector2 vector = new Vector2(this.actions.Camera.X, this.actions.Camera.Y);
			this.cameraAxis = Vector2.ClampMagnitude(vector, 1f) * (float)(this.ignoreInput ? 0 : 1);
			if (this.CameraOverriden)
			{
				this.cameraAxis = Vector3.zero;
				if (this.OverrideMoveComplete)
				{
					PlayerControl.myInstance.Display.CameraHolder.SetPositionAndRotation(this.overridePos, this.overrideRot);
				}
			}
			this.cameraRight = Vector3.ProjectOnPlane(this.camDirRef.right, Vector3.up);
			this.cameraForward = Vector3.ProjectOnPlane(this.camDirRef.forward, Vector3.up);
			this.boostPressed = (this.actions.MovementAbility && !this.ignoreInput);
			this.jumpPressed = (this.actions.Jump && !this.ignoreInput);
			this.firePressed = (this.actions.PrimaryUse && !this.ignoreInput && !this.ignoreMouseInput);
			this.secondaryPressed = (this.actions.SecondaryUse && !this.ignoreInput && !this.ignoreMouseInput);
			this.utilityPressed = (this.actions.UtilityUse && !this.ignoreInput);
			this.interactPressed = (this.actions.Interact && !this.ignoreInput);
			this.interactDownPressed = (this.actions.Interact.WasPressed && !this.ignoreInput);
			this.pingPressed = (this.actions.Ping.IsPressed && !this.ignoreInput);
			this.pingReleased = (this.actions.Ping.WasReleased && !this.ignoreInput);
			this.emotePressed = (this.actions.Emote.IsPressed && !this.ignoreInput);
			this.emoteReleased = (this.actions.Emote.WasReleased && !this.ignoreInput);
			this.spectateMultiplier = this.actions.SpectateMultiplier.RawValue;
			this.spectateUp = this.actions.SpectateUp;
			this.spectateDown = this.actions.SpectateDown;
			this.WorldInputAxis = this.GetWorldMovementAxis();
			if (this.movementAxis.sqrMagnitude > 0f || this.jumpPressed || this.firePressed || this.secondaryPressed || this.utilityPressed || this.boostPressed || vector.sqrMagnitude > 0.2f)
			{
				StateManager.HasActed = true;
			}
		}
	}

	// Token: 0x06000687 RID: 1671 RVA: 0x000307FD File Offset: 0x0002E9FD
	private void LateUpdate()
	{
		if (!this.CameraOverriden || !this.needLateUpdateFix)
		{
			return;
		}
		this.needLateUpdateFix = false;
		this.Control.Display.CameraHolder.SetPositionAndRotation(this.latePos, this.lateRot);
	}

	// Token: 0x06000688 RID: 1672 RVA: 0x00030838 File Offset: 0x0002EA38
	public void CacheCameraLoc()
	{
		this.needLateUpdateFix = true;
		this.latePos = this.Control.Display.CameraHolder.position;
		this.lateRot = this.Control.Display.CameraHolder.rotation;
	}

	// Token: 0x06000689 RID: 1673 RVA: 0x00030878 File Offset: 0x0002EA78
	private void CheckInputIgnore()
	{
		this.ignoreInput = (PanelManager.instance != null && (PanelManager.curSelect.OverridesPlayerControl || InputManager.IsUsingController) && PanelManager.curSelect.gameplayInteractable);
		this.ignoreInput |= PausePanel.IsGamePaused;
		this.ignoreInput |= QuantumConsole.Instance.IsActive;
		this.ignoreInput |= SignatureInkUIControl.MyPlayerPrestiging;
		this.ignoreInput |= (GameplayUI.InputLockoutTimer > 0f);
		if (this.inputLockoutDur > 0f)
		{
			this.inputLockoutDur -= Time.deltaTime;
			this.ignoreInput = true;
		}
		this.ignoreMouseInput = !PanelManager.ShouldLockCursor();
		this.ignoreInput |= (!PanelManager.ShouldLockCursor() && InputManager.IsUsingController);
	}

	// Token: 0x0600068A RID: 1674 RVA: 0x0003095C File Offset: 0x0002EB5C
	public void OverrideCamera(Transform t, float speed, bool forceSmooth = false)
	{
		if (this.camRoutine != null)
		{
			base.StopCoroutine(this.camRoutine);
		}
		this.CameraOverriden = true;
		this.camRoutine = base.StartCoroutine(this.MoveCameraLoc(t, speed, forceSmooth));
	}

	// Token: 0x0600068B RID: 1675 RVA: 0x0003098E File Offset: 0x0002EB8E
	public void OverrideCameraInstant(Transform t)
	{
		PlayerControl.myInstance.Display.CamRays.enabled = false;
		PlayerControl.myInstance.Display.CameraHolder.SetPositionAndRotation(t.position, t.rotation);
	}

	// Token: 0x0600068C RID: 1676 RVA: 0x000309C8 File Offset: 0x0002EBC8
	public void TryLibraryIn()
	{
		if (!MapManager.InLobbyScene || LibraryManager.instance == null || TutorialManager.InTutorial)
		{
			return;
		}
		if (this.camRoutine != null)
		{
			base.StopCoroutine(this.camRoutine);
		}
		this.CameraOverriden = true;
		this.camRoutine = base.StartCoroutine(this.LibraryCamSpawn());
	}

	// Token: 0x0600068D RID: 1677 RVA: 0x00030A1E File Offset: 0x0002EC1E
	public void ReturnCamera(float speed = 6f, bool canWarp = true)
	{
		if (this.camRoutine != null)
		{
			base.StopCoroutine(this.camRoutine);
		}
		this.camRoutine = base.StartCoroutine(this.ReleaseCamera(speed, canWarp));
	}

	// Token: 0x0600068E RID: 1678 RVA: 0x00030A48 File Offset: 0x0002EC48
	private IEnumerator LibraryCamSpawn()
	{
		this.OverrideCameraInstant(LibraryManager.instance.CamLoc_Main);
		yield return true;
		yield return new WaitForSeconds(0.25f);
		this.ReturnCamera(4f, false);
		yield break;
	}

	// Token: 0x0600068F RID: 1679 RVA: 0x00030A57 File Offset: 0x0002EC57
	private IEnumerator MoveCameraLoc(Transform target, float moveSpeed, bool forceSmooth)
	{
		Transform cam = PlayerControl.myInstance.Display.CameraHolder;
		PlayerControl.myInstance.Display.CamRays.enabled = false;
		Vector3 pos = cam.position;
		Quaternion rot = cam.rotation;
		this.overridePos = target.position;
		this.overrideRot = target.rotation;
		this.OverrideMoveComplete = false;
		yield return new WaitForEndOfFrame();
		cam.SetPositionAndRotation(pos, rot);
		float d = Vector3.Distance(cam.position, target.position);
		if (d < 25f || forceSmooth)
		{
			while (d > 0.025f)
			{
				d = Vector3.Distance(pos, this.overridePos);
				UnityEngine.Debug.DrawLine(pos, this.overridePos);
				pos = Vector3.Lerp(pos, this.overridePos, Time.deltaTime * moveSpeed);
				rot = Quaternion.Lerp(rot, this.overrideRot, Time.deltaTime * moveSpeed);
				cam.SetPositionAndRotation(pos, rot);
				this.needLateUpdateFix = true;
				this.latePos = pos;
				this.lateRot = rot;
				yield return true;
				cam.SetPositionAndRotation(pos, rot);
				UnityEngine.Debug.DrawLine(cam.position, pos, Color.blue);
			}
		}
		else
		{
			PageFlip.instance.DoFlipInstant();
			yield return true;
			yield return true;
		}
		cam.SetPositionAndRotation(target.position, target.rotation);
		this.OverrideMoveComplete = true;
		yield break;
	}

	// Token: 0x06000690 RID: 1680 RVA: 0x00030A7B File Offset: 0x0002EC7B
	private IEnumerator ReleaseCamera(float speed = 6f, bool canWarp = false)
	{
		Transform cam = this.Control.Display.CameraHolder;
		Transform camHolderParent = cam.parent;
		this.OverrideMoveComplete = false;
		Vector3 pos = cam.position;
		Quaternion rot = cam.rotation;
		yield return new WaitForEndOfFrame();
		cam.SetPositionAndRotation(pos, rot);
		float d = Vector3.Distance(cam.position, camHolderParent.position);
		if (d < 25f || !canWarp)
		{
			cam.SetPositionAndRotation(pos, rot);
			while (d > 0.025f)
			{
				d = Vector3.Distance(cam.position, camHolderParent.position);
				if (d > 10f)
				{
					cam.SetPositionAndRotation(pos, rot);
				}
				pos = Vector3.Lerp(cam.position, camHolderParent.position, Time.deltaTime * speed);
				pos = Vector3.MoveTowards(pos, camHolderParent.position, Time.deltaTime * 0.5f);
				rot = Quaternion.Lerp(cam.rotation, camHolderParent.rotation, Time.deltaTime * speed);
				cam.SetPositionAndRotation(pos, rot);
				yield return true;
			}
		}
		else
		{
			PageFlip.instance.DoFlipInstant();
			yield return true;
			yield return true;
		}
		cam.localRotation = Quaternion.identity;
		cam.localPosition = Vector3.zero;
		this.CameraOverriden = false;
		PlayerControl.myInstance.Display.CamRays.enabled = true;
		yield break;
	}

	// Token: 0x06000691 RID: 1681 RVA: 0x00030A98 File Offset: 0x0002EC98
	public void LockoutInput(float duration)
	{
		this.inputLockoutDur = duration;
	}

	// Token: 0x06000692 RID: 1682 RVA: 0x00030AA4 File Offset: 0x0002ECA4
	public Vector3 GetWorldMovementAxis()
	{
		Vector3 vector = Vector2.zero;
		vector += Vector3.ProjectOnPlane(this.camDirRef.right, Vector3.up).normalized * this.movementAxis.x;
		vector += Vector3.ProjectOnPlane(this.camDirRef.forward, Vector3.up).normalized * this.movementAxis.y;
		if (vector.magnitude > 1f)
		{
			vector.Normalize();
		}
		return vector;
	}

	// Token: 0x06000693 RID: 1683 RVA: 0x00030B3C File Offset: 0x0002ED3C
	public bool IsInputActive(PlayerInput.AbilityInput input)
	{
		bool result;
		switch (input)
		{
		case PlayerInput.AbilityInput.Generator:
			result = this.firePressed;
			break;
		case PlayerInput.AbilityInput.Spender:
			result = this.secondaryPressed;
			break;
		case PlayerInput.AbilityInput.Movement:
			result = this.boostPressed;
			break;
		case PlayerInput.AbilityInput.Signature:
			result = this.utilityPressed;
			break;
		case PlayerInput.AbilityInput.Jump:
			result = this.jumpPressed;
			break;
		case PlayerInput.AbilityInput.Directional:
			result = (this.movementAxis.sqrMagnitude > 0f);
			break;
		default:
			result = false;
			break;
		}
		return result;
	}

	// Token: 0x06000694 RID: 1684 RVA: 0x00030BAE File Offset: 0x0002EDAE
	public PlayerInput()
	{
	}

	// Token: 0x04000571 RID: 1393
	public bool isLocalControl;

	// Token: 0x04000572 RID: 1394
	public Transform camDirRef;

	// Token: 0x04000573 RID: 1395
	public static PlayerInput myInstance;

	// Token: 0x04000574 RID: 1396
	[Header("Runtime Values")]
	private PlayerControl Control;

	// Token: 0x04000575 RID: 1397
	public Vector2 movementAxis;

	// Token: 0x04000576 RID: 1398
	public Vector2 cameraAxis;

	// Token: 0x04000577 RID: 1399
	public bool boostPressed;

	// Token: 0x04000578 RID: 1400
	public bool sprintPressed;

	// Token: 0x04000579 RID: 1401
	public bool jumpPressed;

	// Token: 0x0400057A RID: 1402
	public bool firePressed;

	// Token: 0x0400057B RID: 1403
	public bool secondaryPressed;

	// Token: 0x0400057C RID: 1404
	public bool utilityPressed;

	// Token: 0x0400057D RID: 1405
	public bool interactPressed;

	// Token: 0x0400057E RID: 1406
	public bool interactDownPressed;

	// Token: 0x0400057F RID: 1407
	public bool pingPressed;

	// Token: 0x04000580 RID: 1408
	public bool pingReleased;

	// Token: 0x04000581 RID: 1409
	public bool emotePressed;

	// Token: 0x04000582 RID: 1410
	public bool emoteReleased;

	// Token: 0x04000583 RID: 1411
	public Vector3 cameraForward;

	// Token: 0x04000584 RID: 1412
	public Vector3 cameraRight;

	// Token: 0x04000585 RID: 1413
	public float spectateUp;

	// Token: 0x04000586 RID: 1414
	public float spectateDown;

	// Token: 0x04000587 RID: 1415
	public float spectateMultiplier;

	// Token: 0x04000588 RID: 1416
	public Vector3 WorldInputAxis;

	// Token: 0x04000589 RID: 1417
	private bool canPing;

	// Token: 0x0400058A RID: 1418
	public Vector2 rawInput;

	// Token: 0x0400058B RID: 1419
	private Coroutine camRoutine;

	// Token: 0x0400058C RID: 1420
	public bool CameraOverriden;

	// Token: 0x0400058D RID: 1421
	private bool OverrideMoveComplete;

	// Token: 0x0400058E RID: 1422
	private Vector3 overridePos;

	// Token: 0x0400058F RID: 1423
	private Quaternion overrideRot;

	// Token: 0x04000590 RID: 1424
	private bool needLateUpdateFix;

	// Token: 0x04000591 RID: 1425
	private Vector3 latePos;

	// Token: 0x04000592 RID: 1426
	private Quaternion lateRot;

	// Token: 0x04000593 RID: 1427
	private bool ignoreInput;

	// Token: 0x04000594 RID: 1428
	private bool ignoreMouseInput;

	// Token: 0x04000595 RID: 1429
	private float inputLockoutDur;

	// Token: 0x020004A5 RID: 1189
	public enum AbilityInput
	{
		// Token: 0x040023D0 RID: 9168
		Generator,
		// Token: 0x040023D1 RID: 9169
		Spender,
		// Token: 0x040023D2 RID: 9170
		Movement,
		// Token: 0x040023D3 RID: 9171
		Signature,
		// Token: 0x040023D4 RID: 9172
		Jump,
		// Token: 0x040023D5 RID: 9173
		Directional
	}

	// Token: 0x020004A6 RID: 1190
	[CompilerGenerated]
	private sealed class <LibraryCamSpawn>d__49 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x06002244 RID: 8772 RVA: 0x000C5F7E File Offset: 0x000C417E
		[DebuggerHidden]
		public <LibraryCamSpawn>d__49(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x06002245 RID: 8773 RVA: 0x000C5F8D File Offset: 0x000C418D
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06002246 RID: 8774 RVA: 0x000C5F90 File Offset: 0x000C4190
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			PlayerInput playerInput = this;
			switch (num)
			{
			case 0:
				this.<>1__state = -1;
				playerInput.OverrideCameraInstant(LibraryManager.instance.CamLoc_Main);
				this.<>2__current = true;
				this.<>1__state = 1;
				return true;
			case 1:
				this.<>1__state = -1;
				this.<>2__current = new WaitForSeconds(0.25f);
				this.<>1__state = 2;
				return true;
			case 2:
				this.<>1__state = -1;
				playerInput.ReturnCamera(4f, false);
				return false;
			default:
				return false;
			}
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06002247 RID: 8775 RVA: 0x000C601F File Offset: 0x000C421F
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x06002248 RID: 8776 RVA: 0x000C6027 File Offset: 0x000C4227
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06002249 RID: 8777 RVA: 0x000C602E File Offset: 0x000C422E
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x040023D6 RID: 9174
		private int <>1__state;

		// Token: 0x040023D7 RID: 9175
		private object <>2__current;

		// Token: 0x040023D8 RID: 9176
		public PlayerInput <>4__this;
	}

	// Token: 0x020004A7 RID: 1191
	[CompilerGenerated]
	private sealed class <MoveCameraLoc>d__50 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x0600224A RID: 8778 RVA: 0x000C6036 File Offset: 0x000C4236
		[DebuggerHidden]
		public <MoveCameraLoc>d__50(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x0600224B RID: 8779 RVA: 0x000C6045 File Offset: 0x000C4245
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x0600224C RID: 8780 RVA: 0x000C6048 File Offset: 0x000C4248
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			PlayerInput playerInput = this;
			switch (num)
			{
			case 0:
				this.<>1__state = -1;
				cam = PlayerControl.myInstance.Display.CameraHolder;
				PlayerControl.myInstance.Display.CamRays.enabled = false;
				pos = cam.position;
				rot = cam.rotation;
				playerInput.overridePos = target.position;
				playerInput.overrideRot = target.rotation;
				playerInput.OverrideMoveComplete = false;
				this.<>2__current = new WaitForEndOfFrame();
				this.<>1__state = 1;
				return true;
			case 1:
				this.<>1__state = -1;
				cam.SetPositionAndRotation(pos, rot);
				d = Vector3.Distance(cam.position, target.position);
				if (d >= 25f && !forceSmooth)
				{
					PageFlip.instance.DoFlipInstant();
					this.<>2__current = true;
					this.<>1__state = 3;
					return true;
				}
				break;
			case 2:
				this.<>1__state = -1;
				cam.SetPositionAndRotation(pos, rot);
				UnityEngine.Debug.DrawLine(cam.position, pos, Color.blue);
				break;
			case 3:
				this.<>1__state = -1;
				this.<>2__current = true;
				this.<>1__state = 4;
				return true;
			case 4:
				this.<>1__state = -1;
				goto IL_25D;
			default:
				return false;
			}
			if (d > 0.025f)
			{
				d = Vector3.Distance(pos, playerInput.overridePos);
				UnityEngine.Debug.DrawLine(pos, playerInput.overridePos);
				pos = Vector3.Lerp(pos, playerInput.overridePos, Time.deltaTime * moveSpeed);
				rot = Quaternion.Lerp(rot, playerInput.overrideRot, Time.deltaTime * moveSpeed);
				cam.SetPositionAndRotation(pos, rot);
				playerInput.needLateUpdateFix = true;
				playerInput.latePos = pos;
				playerInput.lateRot = rot;
				this.<>2__current = true;
				this.<>1__state = 2;
				return true;
			}
			IL_25D:
			cam.SetPositionAndRotation(target.position, target.rotation);
			playerInput.OverrideMoveComplete = true;
			return false;
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x0600224D RID: 8781 RVA: 0x000C62DB File Offset: 0x000C44DB
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x0600224E RID: 8782 RVA: 0x000C62E3 File Offset: 0x000C44E3
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x0600224F RID: 8783 RVA: 0x000C62EA File Offset: 0x000C44EA
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x040023D9 RID: 9177
		private int <>1__state;

		// Token: 0x040023DA RID: 9178
		private object <>2__current;

		// Token: 0x040023DB RID: 9179
		public PlayerInput <>4__this;

		// Token: 0x040023DC RID: 9180
		public Transform target;

		// Token: 0x040023DD RID: 9181
		public bool forceSmooth;

		// Token: 0x040023DE RID: 9182
		public float moveSpeed;

		// Token: 0x040023DF RID: 9183
		private Transform <cam>5__2;

		// Token: 0x040023E0 RID: 9184
		private Vector3 <pos>5__3;

		// Token: 0x040023E1 RID: 9185
		private Quaternion <rot>5__4;

		// Token: 0x040023E2 RID: 9186
		private float <d>5__5;
	}

	// Token: 0x020004A8 RID: 1192
	[CompilerGenerated]
	private sealed class <ReleaseCamera>d__51 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x06002250 RID: 8784 RVA: 0x000C62F2 File Offset: 0x000C44F2
		[DebuggerHidden]
		public <ReleaseCamera>d__51(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x06002251 RID: 8785 RVA: 0x000C6301 File Offset: 0x000C4501
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06002252 RID: 8786 RVA: 0x000C6304 File Offset: 0x000C4504
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			PlayerInput playerInput = this;
			switch (num)
			{
			case 0:
				this.<>1__state = -1;
				cam = playerInput.Control.Display.CameraHolder;
				camHolderParent = cam.parent;
				playerInput.OverrideMoveComplete = false;
				pos = cam.position;
				rot = cam.rotation;
				this.<>2__current = new WaitForEndOfFrame();
				this.<>1__state = 1;
				return true;
			case 1:
				this.<>1__state = -1;
				cam.SetPositionAndRotation(pos, rot);
				d = Vector3.Distance(cam.position, camHolderParent.position);
				if (d >= 25f && canWarp)
				{
					PageFlip.instance.DoFlipInstant();
					this.<>2__current = true;
					this.<>1__state = 3;
					return true;
				}
				cam.SetPositionAndRotation(pos, rot);
				break;
			case 2:
				this.<>1__state = -1;
				break;
			case 3:
				this.<>1__state = -1;
				this.<>2__current = true;
				this.<>1__state = 4;
				return true;
			case 4:
				this.<>1__state = -1;
				goto IL_255;
			default:
				return false;
			}
			if (d > 0.025f)
			{
				d = Vector3.Distance(cam.position, camHolderParent.position);
				if (d > 10f)
				{
					cam.SetPositionAndRotation(pos, rot);
				}
				pos = Vector3.Lerp(cam.position, camHolderParent.position, Time.deltaTime * speed);
				pos = Vector3.MoveTowards(pos, camHolderParent.position, Time.deltaTime * 0.5f);
				rot = Quaternion.Lerp(cam.rotation, camHolderParent.rotation, Time.deltaTime * speed);
				cam.SetPositionAndRotation(pos, rot);
				this.<>2__current = true;
				this.<>1__state = 2;
				return true;
			}
			IL_255:
			cam.localRotation = Quaternion.identity;
			cam.localPosition = Vector3.zero;
			playerInput.CameraOverriden = false;
			PlayerControl.myInstance.Display.CamRays.enabled = true;
			return false;
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06002253 RID: 8787 RVA: 0x000C65A3 File Offset: 0x000C47A3
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x06002254 RID: 8788 RVA: 0x000C65AB File Offset: 0x000C47AB
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06002255 RID: 8789 RVA: 0x000C65B2 File Offset: 0x000C47B2
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x040023E3 RID: 9187
		private int <>1__state;

		// Token: 0x040023E4 RID: 9188
		private object <>2__current;

		// Token: 0x040023E5 RID: 9189
		public PlayerInput <>4__this;

		// Token: 0x040023E6 RID: 9190
		public bool canWarp;

		// Token: 0x040023E7 RID: 9191
		public float speed;

		// Token: 0x040023E8 RID: 9192
		private Transform <cam>5__2;

		// Token: 0x040023E9 RID: 9193
		private Transform <camHolderParent>5__3;

		// Token: 0x040023EA RID: 9194
		private Vector3 <pos>5__4;

		// Token: 0x040023EB RID: 9195
		private Quaternion <rot>5__5;

		// Token: 0x040023EC RID: 9196
		private float <d>5__6;
	}
}
