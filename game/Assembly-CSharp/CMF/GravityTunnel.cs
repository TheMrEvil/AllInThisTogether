using System;
using System.Collections.Generic;
using UnityEngine;

namespace CMF
{
	// Token: 0x020003AC RID: 940
	public class GravityTunnel : MonoBehaviour
	{
		// Token: 0x06001F30 RID: 7984 RVA: 0x000BACF8 File Offset: 0x000B8EF8
		private void FixedUpdate()
		{
			for (int i = 0; i < this.rigidbodies.Count; i++)
			{
				Vector3 a = Vector3.Project(this.rigidbodies[i].transform.position - base.transform.position, base.transform.position + base.transform.forward - base.transform.position) + base.transform.position;
				this.RotateRigidbody(this.rigidbodies[i].transform, (a - this.rigidbodies[i].transform.position).normalized);
			}
		}

		// Token: 0x06001F31 RID: 7985 RVA: 0x000BADC4 File Offset: 0x000B8FC4
		private void OnTriggerEnter(Collider col)
		{
			Rigidbody component = col.GetComponent<Rigidbody>();
			if (!component)
			{
				return;
			}
			if (col.GetComponent<Mover>() == null)
			{
				return;
			}
			this.rigidbodies.Add(component);
		}

		// Token: 0x06001F32 RID: 7986 RVA: 0x000BADFC File Offset: 0x000B8FFC
		private void OnTriggerExit(Collider col)
		{
			Rigidbody component = col.GetComponent<Rigidbody>();
			if (!component)
			{
				return;
			}
			if (col.GetComponent<Mover>() == null)
			{
				return;
			}
			this.rigidbodies.Remove(component);
			this.RotateRigidbody(component.transform, Vector3.up);
			Vector3 eulerAngles = component.rotation.eulerAngles;
			eulerAngles.z = 0f;
			eulerAngles.x = 0f;
			component.MoveRotation(Quaternion.Euler(eulerAngles));
		}

		// Token: 0x06001F33 RID: 7987 RVA: 0x000BAE7C File Offset: 0x000B907C
		private void RotateRigidbody(Transform _transform, Vector3 _targetDirection)
		{
			Rigidbody component = _transform.GetComponent<Rigidbody>();
			_targetDirection.Normalize();
			Quaternion lhs = Quaternion.FromToRotation(_transform.up, _targetDirection);
			Quaternion rotation = _transform.rotation;
			Quaternion rot = lhs * _transform.rotation;
			component.MoveRotation(rot);
		}

		// Token: 0x06001F34 RID: 7988 RVA: 0x000BAEBC File Offset: 0x000B90BC
		private Quaternion GetCounterRotation(Quaternion _rotation)
		{
			float f;
			Vector3 axis;
			_rotation.ToAngleAxis(out f, out axis);
			Quaternion rotation = Quaternion.AngleAxis(Mathf.Sign(f) * 180f, axis);
			return _rotation * Quaternion.Inverse(rotation);
		}

		// Token: 0x06001F35 RID: 7989 RVA: 0x000BAEF3 File Offset: 0x000B90F3
		public GravityTunnel()
		{
		}

		// Token: 0x04001F81 RID: 8065
		private List<Rigidbody> rigidbodies = new List<Rigidbody>();
	}
}
