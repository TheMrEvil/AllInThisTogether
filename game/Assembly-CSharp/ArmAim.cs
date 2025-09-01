using System;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x02000097 RID: 151
public class ArmAim : MonoBehaviour
{
	// Token: 0x0600073F RID: 1855 RVA: 0x0003439A File Offset: 0x0003259A
	private void Awake()
	{
		this.control = base.GetComponentInParent<PlayerControl>();
		this.localControl = this.control.IsMine;
	}

	// Token: 0x06000740 RID: 1856 RVA: 0x000343BC File Offset: 0x000325BC
	private void LateUpdate()
	{
		if (this.localControl && this.cameraRef != null)
		{
			Ray ray = this.cameraRef.ScreenPointToRay(new Vector3((float)Screen.width / 2f, (float)Screen.height / 2f, 0f));
			Vector3 vector = ray.origin + ray.direction * 1000f;
			Vector3 cameraAimPoint = vector;
			RaycastHit[] array = Physics.RaycastAll(ray, 250f, this.raycastMask);
			if (array.Length != 0)
			{
				Array.Sort<RaycastHit>(array, (RaycastHit x, RaycastHit y) => x.distance.CompareTo(y.distance));
				cameraAimPoint = array[0].point;
				foreach (RaycastHit raycastHit in array)
				{
					if (raycastHit.distance >= 16f || Scene_Settings.IsTerrainObject(raycastHit.collider.gameObject))
					{
						vector = raycastHit.point;
						break;
					}
				}
			}
			Debug.DrawLine(ray.origin, vector, Color.blue);
			this.control.CameraAimPoint = cameraAimPoint;
			Quaternion localRotation = base.transform.localRotation;
			base.transform.LookAt(vector);
			base.transform.localRotation = Quaternion.Lerp(localRotation, base.transform.localRotation, Time.deltaTime * 3f);
			this.fireLocation.LookAt(vector);
			array = Physics.RaycastAll(new Ray(this.fireLocation.position, this.fireLocation.forward), 250f, this.raycastMask);
			if (array.Length != 0)
			{
				this.control.AimPoint = vector;
				Array.Sort<RaycastHit>(array, (RaycastHit x, RaycastHit y) => x.distance.CompareTo(y.distance));
				foreach (RaycastHit raycastHit2 in array)
				{
					if (raycastHit2.distance >= 5f && Vector3.Distance(raycastHit2.point, vector) <= 5f)
					{
						this.control.AimPoint = raycastHit2.point;
						break;
					}
				}
			}
			else
			{
				this.control.AimPoint = ray.origin + ray.direction * 250f;
			}
			Debug.DrawLine(this.fireLocation.position, this.control.AimPoint, Color.green);
			Crosshair.SetTargetPoint(vector);
			return;
		}
		this.fireLocation.LookAt(this.control.AimPoint);
	}

	// Token: 0x06000741 RID: 1857 RVA: 0x0003465E File Offset: 0x0003285E
	public ArmAim()
	{
	}

	// Token: 0x040005CD RID: 1485
	public bool localControl;

	// Token: 0x040005CE RID: 1486
	public Camera cameraRef;

	// Token: 0x040005CF RID: 1487
	public LayerMask raycastMask;

	// Token: 0x040005D0 RID: 1488
	public Transform fireLocation;

	// Token: 0x040005D1 RID: 1489
	private PlayerControl control;

	// Token: 0x020004AC RID: 1196
	[CompilerGenerated]
	[Serializable]
	private sealed class <>c
	{
		// Token: 0x0600225C RID: 8796 RVA: 0x000C6A42 File Offset: 0x000C4C42
		// Note: this type is marked as 'beforefieldinit'.
		static <>c()
		{
		}

		// Token: 0x0600225D RID: 8797 RVA: 0x000C6A4E File Offset: 0x000C4C4E
		public <>c()
		{
		}

		// Token: 0x0600225E RID: 8798 RVA: 0x000C6A58 File Offset: 0x000C4C58
		internal int <LateUpdate>b__6_0(RaycastHit x, RaycastHit y)
		{
			return x.distance.CompareTo(y.distance);
		}

		// Token: 0x0600225F RID: 8799 RVA: 0x000C6A7C File Offset: 0x000C4C7C
		internal int <LateUpdate>b__6_1(RaycastHit x, RaycastHit y)
		{
			return x.distance.CompareTo(y.distance);
		}

		// Token: 0x040023F8 RID: 9208
		public static readonly ArmAim.<>c <>9 = new ArmAim.<>c();

		// Token: 0x040023F9 RID: 9209
		public static Comparison<RaycastHit> <>9__6_0;

		// Token: 0x040023FA RID: 9210
		public static Comparison<RaycastHit> <>9__6_1;
	}
}
