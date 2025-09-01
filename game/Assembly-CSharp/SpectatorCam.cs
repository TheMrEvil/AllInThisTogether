using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x02000032 RID: 50
public class SpectatorCam : MonoBehaviour
{
	// Token: 0x1700000B RID: 11
	// (get) Token: 0x06000173 RID: 371 RVA: 0x0000E972 File Offset: 0x0000CB72
	private InputActions actions
	{
		get
		{
			return InputManager.Actions;
		}
	}

	// Token: 0x06000174 RID: 372 RVA: 0x0000E97C File Offset: 0x0000CB7C
	private void Awake()
	{
		this.control = base.GetComponentInParent<PlayerControl>();
		this.targetRot = base.transform.rotation;
		this.targetPos = base.transform.position;
		this.proxy = new GameObject("_CamFollow");
		this.proxy.hideFlags = HideFlags.HideInHierarchy;
		this.proxy.transform.SetParent(base.transform.parent);
		this.position = (this.proxy.transform.position = base.transform.position);
		this.rotation = (this.proxy.transform.rotation = base.transform.rotation);
	}

	// Token: 0x06000175 RID: 373 RVA: 0x0000EA36 File Offset: 0x0000CC36
	public void SpectatorChanged(bool isSpectator)
	{
		this.PlayerListener.enabled = !isSpectator;
		this.SpectatorListener.enabled = isSpectator;
	}

	// Token: 0x06000176 RID: 374 RVA: 0x0000EA54 File Offset: 0x0000CC54
	private void Update()
	{
		if (!this.control.IsMine || !this.control.IsSpectator)
		{
			return;
		}
		Transform transform = base.transform;
		ValueTuple<float, float, float> inputs = this.GetInputs();
		float item = inputs.Item1;
		float item2 = inputs.Item2;
		float item3 = inputs.Item3;
		if (this.isPlayback)
		{
			this.playbackIndex++;
			if (this.playbackIndex >= this.recordPoints.Count)
			{
				this.playbackIndex = 0;
			}
			transform.position = this.recordPoints[this.playbackIndex];
			transform.rotation = this.recordRots[this.playbackIndex];
			return;
		}
		transform.position = Vector3.Lerp(transform.position, this.targetPos, Time.deltaTime * this.PosLerpSpeed);
		transform.rotation = Quaternion.Lerp(transform.rotation, this.targetRot, Time.deltaTime * this.RotLerpSpeed);
		if (this.isRecording)
		{
			this.RecordStep();
		}
		this.proxy.transform.SetPositionAndRotation(this.position, this.rotation);
		this.position += this.proxy.transform.forward * this.move.y * Time.deltaTime * this.MoveSpeed * item3;
		if (!this.horizontalLocked)
		{
			this.position += this.proxy.transform.right * this.move.x * Time.deltaTime * this.MoveSpeed * item3;
		}
		this.position += item * Vector3.up * this.VerticalSpeed * Time.deltaTime;
		this.position -= item2 * Vector3.up * this.VerticalSpeed * Time.deltaTime;
		if (this.heightLocked)
		{
			this.position.y = this.lockHeightVal;
		}
		if (Scene_Settings.instance.SceneTerrain != null)
		{
			this.position.y = Mathf.Max(this.position.y, Scene_Settings.instance.SceneTerrain.SampleHeight(this.position) + 0.25f);
		}
		this.proxy.transform.Rotate(Vector3.up, this.rot.x * this.TurnSpeed * Time.deltaTime);
		if (!this.lockTilt)
		{
			this.proxy.transform.Rotate(Vector3.right, -this.rot.y * this.TurnSpeed * Time.deltaTime);
		}
		this.proxy.transform.rotation = Quaternion.Euler(this.proxy.transform.eulerAngles.x, this.proxy.transform.eulerAngles.y, 0f);
		this.rotation = this.proxy.transform.rotation;
	}

	// Token: 0x06000177 RID: 375 RVA: 0x0000ED8C File Offset: 0x0000CF8C
	[return: TupleElementNames(new string[]
	{
		"up",
		"down",
		"mult"
	})]
	private ValueTuple<float, float, float> GetInputs()
	{
		this.move = PlayerInput.myInstance.movementAxis;
		this.rot = PlayerInput.myInstance.cameraAxis;
		float spectateUp = PlayerInput.myInstance.spectateUp;
		float spectateDown = PlayerInput.myInstance.spectateDown;
		float item = 1f - PlayerInput.myInstance.spectateMultiplier;
		if (this.actions.SpectateSpeedUp.WasPressed)
		{
			this.MoveSpeed = Mathf.Clamp(this.MoveSpeed + 3f, 1f, 125f);
		}
		else if (this.actions.SpectateSpeedDown.WasPressed)
		{
			this.MoveSpeed = Mathf.Clamp(this.MoveSpeed - 3f, 1f, 125f);
		}
		if (this.actions.SpectateXButton.WasPressed)
		{
			PostFXManager.instance.ReleaseSketch();
		}
		if (this.actions.SpectateYButton.WasPressed)
		{
			PostFXManager.instance.ActivateSketch(true, true);
		}
		if (this.actions.SpectateLockRot.WasPressed)
		{
			this.lockTilt = !this.lockTilt;
		}
		if (this.actions.SpectateLockHeight.WasPressed)
		{
			this.heightLocked = !this.heightLocked;
			this.lockHeightVal = this.position.y;
		}
		if (this.actions.SpectateToggleRecord.WasPressed)
		{
			if (!this.isRecording)
			{
				this.recordPoints.Clear();
				this.recordRots.Clear();
			}
			this.isRecording = !this.isRecording;
			this.isPlayback = false;
		}
		if (this.actions.SpectatePlayRecord.WasPressed)
		{
			this.isRecording = false;
			this.isPlayback = !this.isPlayback;
		}
		return new ValueTuple<float, float, float>(spectateUp, spectateDown, item);
	}

	// Token: 0x06000178 RID: 376 RVA: 0x0000EF49 File Offset: 0x0000D149
	public bool ToggleHorizontalLock()
	{
		this.horizontalLocked = !this.horizontalLocked;
		return this.horizontalLocked;
	}

	// Token: 0x06000179 RID: 377 RVA: 0x0000EF60 File Offset: 0x0000D160
	private void RecordStep()
	{
		this.recordPoints.Add(base.transform.position);
		this.recordRots.Add(base.transform.rotation);
	}

	// Token: 0x0600017A RID: 378 RVA: 0x0000EF90 File Offset: 0x0000D190
	private void LateUpdate()
	{
		Transform transform = this.proxy.transform;
		this.targetRot = transform.rotation;
		this.targetPos = transform.position;
	}

	// Token: 0x0600017B RID: 379 RVA: 0x0000EFC4 File Offset: 0x0000D1C4
	public SpectatorCam()
	{
	}

	// Token: 0x040001A7 RID: 423
	private PlayerControl control;

	// Token: 0x040001A8 RID: 424
	private Quaternion targetRot;

	// Token: 0x040001A9 RID: 425
	private Vector3 targetPos;

	// Token: 0x040001AA RID: 426
	[Header("Camera Easing")]
	public float PosLerpSpeed = 4f;

	// Token: 0x040001AB RID: 427
	public float RotLerpSpeed = 4f;

	// Token: 0x040001AC RID: 428
	[Header("Joystic Controls")]
	[Range(1f, 100f)]
	public float MoveSpeed = 50f;

	// Token: 0x040001AD RID: 429
	[Range(1f, 100f)]
	public float TurnSpeed = 30f;

	// Token: 0x040001AE RID: 430
	[Range(1f, 100f)]
	public float VerticalSpeed = 25f;

	// Token: 0x040001AF RID: 431
	public AudioListener PlayerListener;

	// Token: 0x040001B0 RID: 432
	public AudioListener SpectatorListener;

	// Token: 0x040001B1 RID: 433
	private Vector3 position;

	// Token: 0x040001B2 RID: 434
	private Quaternion rotation;

	// Token: 0x040001B3 RID: 435
	private Vector2 move;

	// Token: 0x040001B4 RID: 436
	private Vector2 rot;

	// Token: 0x040001B5 RID: 437
	private bool lockTilt;

	// Token: 0x040001B6 RID: 438
	private bool heightLocked;

	// Token: 0x040001B7 RID: 439
	private bool horizontalLocked;

	// Token: 0x040001B8 RID: 440
	private float lockHeightVal;

	// Token: 0x040001B9 RID: 441
	private GameObject proxy;

	// Token: 0x040001BA RID: 442
	private bool isRecording;

	// Token: 0x040001BB RID: 443
	private bool isPlayback;

	// Token: 0x040001BC RID: 444
	private List<Vector3> recordPoints = new List<Vector3>();

	// Token: 0x040001BD RID: 445
	private List<Quaternion> recordRots = new List<Quaternion>();

	// Token: 0x040001BE RID: 446
	private int playbackIndex;
}
