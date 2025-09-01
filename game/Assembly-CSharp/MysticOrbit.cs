using System;
using UnityEngine;

// Token: 0x0200000F RID: 15
[AddComponentMenu("Camera-Control/Mouse drag Orbit with zoom")]
public class MysticOrbit : MonoBehaviour
{
	// Token: 0x06000042 RID: 66 RVA: 0x00004C80 File Offset: 0x00002E80
	private void Start()
	{
		Vector3 eulerAngles = base.transform.eulerAngles;
		this.rotationYAxis = eulerAngles.y;
		this.rotationXAxis = eulerAngles.x;
		if (base.GetComponent<Rigidbody>())
		{
			base.GetComponent<Rigidbody>().freezeRotation = true;
		}
	}

	// Token: 0x06000043 RID: 67 RVA: 0x00004CCC File Offset: 0x00002ECC
	private void LateUpdate()
	{
		if (this.target)
		{
			if (Input.GetMouseButton(1))
			{
				this.velocityX += this.xSpeed * Input.GetAxis("Mouse X") * this.distance * 0.02f;
				this.velocityY += this.ySpeed * Input.GetAxis("Mouse Y") * 0.02f;
			}
			this.rotationYAxis += this.velocityX;
			this.rotationXAxis -= this.velocityY;
			this.rotationXAxis = MysticOrbit.ClampAngle(this.rotationXAxis, this.yMinLimit, this.yMaxLimit);
			Quaternion rotation = Quaternion.Euler(this.rotationXAxis, this.rotationYAxis, 0f);
			this.distance = Mathf.Clamp(this.distance - Input.GetAxis("Mouse ScrollWheel") * 5f, this.distanceMin, this.distanceMax);
			RaycastHit raycastHit;
			if (Physics.Linecast(this.target.position, base.transform.position, out raycastHit))
			{
				this.distance -= raycastHit.distance;
			}
			Vector3 point = new Vector3(0f, 0f, -this.distance);
			Vector3 position = rotation * point + this.target.position;
			base.transform.rotation = rotation;
			base.transform.position = position;
			this.velocityX = Mathf.Lerp(this.velocityX, 0f, Time.deltaTime * this.smoothTime);
			this.velocityY = Mathf.Lerp(this.velocityY, 0f, Time.deltaTime * this.smoothTime);
		}
	}

	// Token: 0x06000044 RID: 68 RVA: 0x00004E87 File Offset: 0x00003087
	public static float ClampAngle(float angle, float min, float max)
	{
		if (angle < -360f)
		{
			angle += 360f;
		}
		if (angle > 360f)
		{
			angle -= 360f;
		}
		return Mathf.Clamp(angle, min, max);
	}

	// Token: 0x06000045 RID: 69 RVA: 0x00004EB4 File Offset: 0x000030B4
	public MysticOrbit()
	{
	}

	// Token: 0x0400002E RID: 46
	public Transform target;

	// Token: 0x0400002F RID: 47
	public float distance = 5f;

	// Token: 0x04000030 RID: 48
	public float xSpeed = 120f;

	// Token: 0x04000031 RID: 49
	public float ySpeed = 120f;

	// Token: 0x04000032 RID: 50
	public float yMinLimit = -20f;

	// Token: 0x04000033 RID: 51
	public float yMaxLimit = 80f;

	// Token: 0x04000034 RID: 52
	public float distanceMin = 0.5f;

	// Token: 0x04000035 RID: 53
	public float distanceMax = 15f;

	// Token: 0x04000036 RID: 54
	public float smoothTime = 2f;

	// Token: 0x04000037 RID: 55
	private float rotationYAxis;

	// Token: 0x04000038 RID: 56
	private float rotationXAxis;

	// Token: 0x04000039 RID: 57
	private float velocityX;

	// Token: 0x0400003A RID: 58
	private float velocityY;
}
