using System;
using Agoston_R.Aim_Assist_Pro.Scripts.AimAssistCode.AimAssists;
using Agoston_R.Aim_Assist_Pro.Scripts.AimAssistCode.Helper.Chaining;
using Agoston_R.Aim_Assist_Pro.Scripts.AimAssistCode.Model;
using UnityEngine;

namespace CMF
{
	// Token: 0x020003A3 RID: 931
	public class CameraController : MonoBehaviour
	{
		// Token: 0x06001EB5 RID: 7861 RVA: 0x000B7844 File Offset: 0x000B5A44
		private void Awake()
		{
			this.tr = base.transform;
			this.cam = base.GetComponent<Camera>();
			this.cameraInput = base.GetComponent<CameraInput>();
			if (this.cameraInput == null)
			{
				Debug.LogWarning("No camera input script has been attached to this gameobject", base.gameObject);
			}
			if (this.cam == null)
			{
				this.cam = base.GetComponentInChildren<Camera>();
			}
			this.currentXAngle = this.tr.localRotation.eulerAngles.x;
			this.currentYAngle = this.tr.localRotation.eulerAngles.y;
			this.RotateCamera(0f, 0f);
			this.Setup();
		}

		// Token: 0x06001EB6 RID: 7862 RVA: 0x000B78FF File Offset: 0x000B5AFF
		protected virtual void Setup()
		{
		}

		// Token: 0x06001EB7 RID: 7863 RVA: 0x000B7904 File Offset: 0x000B5B04
		private void Update()
		{
			Vector2 vector = this.HandleCameraRotation();
			if (InputManager.IsUsingController)
			{
				vector = this.assistChainer.WithLookInputDelta(vector).UsingAimEaseIn(this.AssistEasing).UsingPrecisionAim(this.AssistPrecision).UsingAutoAim(this.AssistAuto).GetModifiedLookInputDelta();
			}
			this.RotateCamera(vector.x, vector.y);
		}

		// Token: 0x06001EB8 RID: 7864 RVA: 0x000B7964 File Offset: 0x000B5B64
		protected Vector2 HandleCameraRotation()
		{
			if (this.cameraInput == null)
			{
				return Vector2.zero;
			}
			if (PanelManager.instance != null && PanelManager.CurPanel != PanelType.GameInvisible)
			{
				return Vector2.zero;
			}
			if (PingUISelector.IsActive || EmoteSelector.IsActive)
			{
				return Vector2.zero;
			}
			float horizontalCameraInput = this.cameraInput.GetHorizontalCameraInput();
			float verticalCameraInput = this.cameraInput.GetVerticalCameraInput();
			return new Vector2(horizontalCameraInput, verticalCameraInput);
		}

		// Token: 0x06001EB9 RID: 7865 RVA: 0x000B79D4 File Offset: 0x000B5BD4
		protected void RotateCamera(float _newHorizontalInput, float _newVerticalInput)
		{
			if (this.smoothCameraRotation)
			{
				this.oldHorizontalInput = Mathf.Lerp(this.oldHorizontalInput, _newHorizontalInput, Time.deltaTime * this.cameraSmoothingFactor);
				this.oldVerticalInput = Mathf.Lerp(this.oldVerticalInput, _newVerticalInput, Time.deltaTime * this.cameraSmoothingFactor);
			}
			else
			{
				this.oldHorizontalInput = _newHorizontalInput;
				this.oldVerticalInput = _newVerticalInput;
			}
			this.currentXAngle += Mathf.Clamp(this.oldVerticalInput * this.cameraSpeed * Mathf.Clamp(Time.deltaTime, 0f, 0.033f) * 0.8f, -30f, 30f);
			this.currentYAngle += Mathf.Clamp(this.oldHorizontalInput * this.cameraSpeed * Mathf.Clamp(Time.deltaTime, 0f, 0.033f), -30f, 30f);
			if (InputManager.IsUsingController && this.AssistLock.Initialized && this.AssistLock.aimAssistEnabled)
			{
				AimAssistResult aimAssistResult = this.AssistLock.AssistAim();
				this.currentYAngle += aimAssistResult.RotationAdditionInDegrees;
				this.currentXAngle += aimAssistResult.PitchAdditionInDegrees;
			}
			if (InputManager.IsUsingController && this.AssistMagnet.Initialized && this.AssistMagnet.aimAssistEnabled)
			{
				Vector2 movementAxis = PlayerControl.myInstance.Input.movementAxis;
				AimAssistResult aimAssistResult2 = this.AssistMagnet.AssistAim(movementAxis);
				this.currentYAngle += aimAssistResult2.RotationAdditionInDegrees;
				this.currentXAngle += aimAssistResult2.PitchAdditionInDegrees;
			}
			this.currentXAngle = Mathf.Clamp(this.currentXAngle, -this.upperVerticalLimit, this.lowerVerticalLimit);
			this.UpdateRotation();
		}

		// Token: 0x06001EBA RID: 7866 RVA: 0x000B7B98 File Offset: 0x000B5D98
		protected void UpdateRotation()
		{
			if (float.IsNaN(this.currentYAngle))
			{
				this.currentYAngle = 0f;
			}
			this.tr.localRotation = Quaternion.Euler(new Vector3(0f, this.currentYAngle, 0f));
			this.facingDirection = this.tr.forward;
			this.upwardsDirection = this.tr.up;
			if (float.IsNaN(this.currentXAngle))
			{
				this.currentXAngle = 0f;
			}
			this.tr.localRotation = Quaternion.Euler(new Vector3(this.currentXAngle, this.currentYAngle, 0f));
		}

		// Token: 0x06001EBB RID: 7867 RVA: 0x000B7C42 File Offset: 0x000B5E42
		public void SetFOV(float _fov)
		{
			if (this.cam)
			{
				this.cam.fieldOfView = _fov;
			}
		}

		// Token: 0x06001EBC RID: 7868 RVA: 0x000B7C5D File Offset: 0x000B5E5D
		public void SetRotationAngles(float _xAngle, float _yAngle)
		{
			this.currentXAngle = _xAngle;
			this.currentYAngle = _yAngle;
			this.UpdateRotation();
		}

		// Token: 0x06001EBD RID: 7869 RVA: 0x000B7C74 File Offset: 0x000B5E74
		public void RotateTowardPosition(Vector3 _position, float _lookSpeed)
		{
			Vector3 direction = _position - this.tr.position;
			this.RotateTowardDirection(direction, _lookSpeed);
		}

		// Token: 0x06001EBE RID: 7870 RVA: 0x000B7C9C File Offset: 0x000B5E9C
		public void RotateTowardDirection(Vector3 _direction, float _lookSpeed)
		{
			_direction.Normalize();
			_direction = this.tr.parent.InverseTransformDirection(_direction);
			Vector3 vector = this.GetAimingDirection();
			vector = this.tr.parent.InverseTransformDirection(vector);
			float angle = VectorMath.GetAngle(new Vector3(0f, vector.y, 1f), new Vector3(0f, _direction.y, 1f), Vector3.right);
			vector.y = 0f;
			_direction.y = 0f;
			float angle2 = VectorMath.GetAngle(vector, _direction, Vector3.up);
			Vector2 vector2 = new Vector2(this.currentXAngle, this.currentYAngle);
			Vector2 a = new Vector2(angle, angle2);
			float magnitude = a.magnitude;
			if (magnitude == 0f)
			{
				return;
			}
			Vector2 a2 = a / magnitude;
			if (_lookSpeed * Time.deltaTime > magnitude)
			{
				vector2 += a2 * magnitude;
			}
			else
			{
				vector2 += a2 * _lookSpeed * Time.deltaTime;
			}
			this.currentYAngle = vector2.y;
			this.currentXAngle = Mathf.Clamp(vector2.x, -this.upperVerticalLimit, this.lowerVerticalLimit);
			this.UpdateRotation();
		}

		// Token: 0x06001EBF RID: 7871 RVA: 0x000B7DD6 File Offset: 0x000B5FD6
		public float GetCurrentXAngle()
		{
			return this.currentXAngle;
		}

		// Token: 0x06001EC0 RID: 7872 RVA: 0x000B7DDE File Offset: 0x000B5FDE
		public float GetCurrentYAngle()
		{
			return this.currentYAngle;
		}

		// Token: 0x06001EC1 RID: 7873 RVA: 0x000B7DE6 File Offset: 0x000B5FE6
		public Vector3 GetFacingDirection()
		{
			return this.facingDirection;
		}

		// Token: 0x06001EC2 RID: 7874 RVA: 0x000B7DEE File Offset: 0x000B5FEE
		public Vector3 GetAimingDirection()
		{
			return this.tr.forward;
		}

		// Token: 0x06001EC3 RID: 7875 RVA: 0x000B7DFB File Offset: 0x000B5FFB
		public Vector3 GetStrafeDirection()
		{
			return this.tr.right;
		}

		// Token: 0x06001EC4 RID: 7876 RVA: 0x000B7E08 File Offset: 0x000B6008
		public Vector3 GetUpDirection()
		{
			return this.upwardsDirection;
		}

		// Token: 0x06001EC5 RID: 7877 RVA: 0x000B7E10 File Offset: 0x000B6010
		public CameraController()
		{
		}

		// Token: 0x04001EF6 RID: 7926
		private float currentXAngle;

		// Token: 0x04001EF7 RID: 7927
		private float currentYAngle;

		// Token: 0x04001EF8 RID: 7928
		[Range(0f, 90f)]
		public float upperVerticalLimit = 60f;

		// Token: 0x04001EF9 RID: 7929
		[Range(0f, 90f)]
		public float lowerVerticalLimit = 60f;

		// Token: 0x04001EFA RID: 7930
		private float oldHorizontalInput;

		// Token: 0x04001EFB RID: 7931
		private float oldVerticalInput;

		// Token: 0x04001EFC RID: 7932
		public float cameraSpeed = 250f;

		// Token: 0x04001EFD RID: 7933
		public bool smoothCameraRotation;

		// Token: 0x04001EFE RID: 7934
		[Range(1f, 50f)]
		public float cameraSmoothingFactor = 25f;

		// Token: 0x04001EFF RID: 7935
		private Vector3 facingDirection;

		// Token: 0x04001F00 RID: 7936
		private Vector3 upwardsDirection;

		// Token: 0x04001F01 RID: 7937
		protected Transform tr;

		// Token: 0x04001F02 RID: 7938
		protected Camera cam;

		// Token: 0x04001F03 RID: 7939
		protected CameraInput cameraInput;

		// Token: 0x04001F04 RID: 7940
		[Header("Aim Assist")]
		public Rigidbody charRB;

		// Token: 0x04001F05 RID: 7941
		public Magnetism AssistMagnet;

		// Token: 0x04001F06 RID: 7942
		public AimLock AssistLock;

		// Token: 0x04001F07 RID: 7943
		public AimEaseIn AssistEasing;

		// Token: 0x04001F08 RID: 7944
		public PrecisionAim AssistPrecision;

		// Token: 0x04001F09 RID: 7945
		public AutoAim AssistAuto;

		// Token: 0x04001F0A RID: 7946
		private readonly LookInputBasedAimAssistChainer assistChainer = new LookInputBasedAimAssistChainer();
	}
}
