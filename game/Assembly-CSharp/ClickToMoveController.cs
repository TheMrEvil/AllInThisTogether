using System;
using CMF;
using UnityEngine;

// Token: 0x02000033 RID: 51
public class ClickToMoveController : Controller
{
	// Token: 0x0600017C RID: 380 RVA: 0x0000F024 File Offset: 0x0000D224
	private void Start()
	{
		this.mover = base.GetComponent<Mover>();
		this.tr = base.transform;
		if (this.playerCamera == null)
		{
			Debug.LogWarning("No camera has been assigned to this controller!", this);
		}
		this.lastPosition = this.tr.position;
		this.currentTargetPosition = base.transform.position;
		this.groundPlane = new Plane(this.tr.up, this.tr.position);
	}

	// Token: 0x0600017D RID: 381 RVA: 0x0000F0A5 File Offset: 0x0000D2A5
	private void Update()
	{
		this.HandleMouseInput();
	}

	// Token: 0x0600017E RID: 382 RVA: 0x0000F0B0 File Offset: 0x0000D2B0
	private void FixedUpdate()
	{
		this.mover.CheckForGround();
		this.isGrounded = this.mover.IsGrounded();
		this.HandleTimeOut();
		Vector3 vector = Vector3.zero;
		vector = this.CalculateMovementVelocity();
		this.lastMovementVelocity = vector;
		this.HandleGravity();
		vector += this.tr.up * this.currentVerticalSpeed;
		this.mover.SetExtendSensorRange(this.isGrounded);
		this.mover.SetVelocity(vector);
		this.lastVelocity = vector;
	}

	// Token: 0x0600017F RID: 383 RVA: 0x0000F13C File Offset: 0x0000D33C
	private Vector3 CalculateMovementVelocity()
	{
		if (!this.hasTarget)
		{
			return Vector3.zero;
		}
		Vector3 vector = this.currentTargetPosition - this.tr.position;
		vector = VectorMath.RemoveDotVector(vector, this.tr.up);
		float magnitude = vector.magnitude;
		if (magnitude <= this.reachTargetThreshold)
		{
			this.hasTarget = false;
			return Vector3.zero;
		}
		Vector3 result = vector.normalized * this.movementSpeed;
		if (this.movementSpeed * Time.fixedDeltaTime > magnitude)
		{
			result = vector.normalized * magnitude;
			this.hasTarget = false;
		}
		return result;
	}

	// Token: 0x06000180 RID: 384 RVA: 0x0000F1D8 File Offset: 0x0000D3D8
	private void HandleGravity()
	{
		if (!this.isGrounded)
		{
			this.currentVerticalSpeed -= this.gravity * Time.deltaTime;
			return;
		}
		if (this.currentVerticalSpeed < 0f && this.OnLand != null)
		{
			this.OnLand(this.tr.up * this.currentVerticalSpeed, this.mover.lastContactPoint, this.mover.surfaceNormal);
		}
		this.currentVerticalSpeed = 0f;
	}

	// Token: 0x06000181 RID: 385 RVA: 0x0000F260 File Offset: 0x0000D460
	private void HandleMouseInput()
	{
		if (this.playerCamera == null)
		{
			return;
		}
		if ((!this.holdMouseButtonToMove && this.WasMouseButtonJustPressed()) || (this.holdMouseButtonToMove && this.IsMouseButtonPressed()))
		{
			Ray ray = this.playerCamera.ScreenPointToRay(this.GetMousePosition());
			if (this.mouseDetectionType == ClickToMoveController.MouseDetectionType.AbstractPlane)
			{
				this.groundPlane.SetNormalAndPosition(this.tr.up, this.tr.position);
				float distance = 0f;
				if (this.groundPlane.Raycast(ray, out distance))
				{
					this.currentTargetPosition = ray.GetPoint(distance);
					this.hasTarget = true;
					return;
				}
				this.hasTarget = false;
				return;
			}
			else if (this.mouseDetectionType == ClickToMoveController.MouseDetectionType.Raycast)
			{
				RaycastHit raycastHit;
				if (Physics.Raycast(ray, out raycastHit, 100f, this.raycastLayerMask, QueryTriggerInteraction.Ignore))
				{
					this.currentTargetPosition = raycastHit.point;
					this.hasTarget = true;
					return;
				}
				this.hasTarget = false;
			}
		}
	}

	// Token: 0x06000182 RID: 386 RVA: 0x0000F358 File Offset: 0x0000D558
	private void HandleTimeOut()
	{
		if (!this.hasTarget)
		{
			this.currentTimeOutTime = 0f;
			return;
		}
		if (Vector3.Distance(this.tr.position, this.lastPosition) > this.timeOutDistanceThreshold)
		{
			this.currentTimeOutTime = 0f;
			this.lastPosition = this.tr.position;
			return;
		}
		this.currentTimeOutTime += Time.deltaTime;
		if (this.currentTimeOutTime >= this.timeOutTime)
		{
			this.hasTarget = false;
		}
	}

	// Token: 0x06000183 RID: 387 RVA: 0x0000F3DB File Offset: 0x0000D5DB
	protected Vector2 GetMousePosition()
	{
		return Input.mousePosition;
	}

	// Token: 0x06000184 RID: 388 RVA: 0x0000F3E7 File Offset: 0x0000D5E7
	protected bool IsMouseButtonPressed()
	{
		return Input.GetMouseButton(0);
	}

	// Token: 0x06000185 RID: 389 RVA: 0x0000F3EF File Offset: 0x0000D5EF
	protected bool WasMouseButtonJustPressed()
	{
		return Input.GetMouseButtonDown(0);
	}

	// Token: 0x06000186 RID: 390 RVA: 0x0000F3F7 File Offset: 0x0000D5F7
	public override bool IsGrounded()
	{
		return this.isGrounded;
	}

	// Token: 0x06000187 RID: 391 RVA: 0x0000F3FF File Offset: 0x0000D5FF
	public override Vector3 GetMovementVelocity()
	{
		return this.lastMovementVelocity;
	}

	// Token: 0x06000188 RID: 392 RVA: 0x0000F407 File Offset: 0x0000D607
	public override Vector3 GetVelocity()
	{
		return this.lastVelocity;
	}

	// Token: 0x06000189 RID: 393 RVA: 0x0000F410 File Offset: 0x0000D610
	public ClickToMoveController()
	{
	}

	// Token: 0x040001BF RID: 447
	public float movementSpeed = 10f;

	// Token: 0x040001C0 RID: 448
	public float gravity = 30f;

	// Token: 0x040001C1 RID: 449
	private float currentVerticalSpeed;

	// Token: 0x040001C2 RID: 450
	private bool isGrounded;

	// Token: 0x040001C3 RID: 451
	private Vector3 currentTargetPosition;

	// Token: 0x040001C4 RID: 452
	private float reachTargetThreshold = 0.001f;

	// Token: 0x040001C5 RID: 453
	public bool holdMouseButtonToMove;

	// Token: 0x040001C6 RID: 454
	public ClickToMoveController.MouseDetectionType mouseDetectionType;

	// Token: 0x040001C7 RID: 455
	public LayerMask raycastLayerMask = -1;

	// Token: 0x040001C8 RID: 456
	public float timeOutTime = 1f;

	// Token: 0x040001C9 RID: 457
	private float currentTimeOutTime = 1f;

	// Token: 0x040001CA RID: 458
	public float timeOutDistanceThreshold = 0.05f;

	// Token: 0x040001CB RID: 459
	private Vector3 lastPosition;

	// Token: 0x040001CC RID: 460
	public Camera playerCamera;

	// Token: 0x040001CD RID: 461
	private bool hasTarget;

	// Token: 0x040001CE RID: 462
	private Vector3 lastVelocity = Vector3.zero;

	// Token: 0x040001CF RID: 463
	private Vector3 lastMovementVelocity = Vector3.zero;

	// Token: 0x040001D0 RID: 464
	private Plane groundPlane;

	// Token: 0x040001D1 RID: 465
	private Mover mover;

	// Token: 0x040001D2 RID: 466
	private Transform tr;

	// Token: 0x020003FD RID: 1021
	public enum MouseDetectionType
	{
		// Token: 0x04002129 RID: 8489
		AbstractPlane,
		// Token: 0x0400212A RID: 8490
		Raycast
	}
}
