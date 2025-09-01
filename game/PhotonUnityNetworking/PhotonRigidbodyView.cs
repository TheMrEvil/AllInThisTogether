using System;
using UnityEngine;

namespace Photon.Pun
{
	// Token: 0x02000022 RID: 34
	[RequireComponent(typeof(Rigidbody))]
	[AddComponentMenu("Photon Networking/Photon Rigidbody View")]
	public class PhotonRigidbodyView : MonoBehaviourPun, IPunObservable
	{
		// Token: 0x06000192 RID: 402 RVA: 0x0000A4BE File Offset: 0x000086BE
		public void Awake()
		{
			this.m_Body = base.GetComponent<Rigidbody>();
			this.m_NetworkPosition = default(Vector3);
			this.m_NetworkRotation = default(Quaternion);
		}

		// Token: 0x06000193 RID: 403 RVA: 0x0000A4E4 File Offset: 0x000086E4
		public void FixedUpdate()
		{
			if (!base.photonView.IsMine)
			{
				this.m_Body.position = Vector3.MoveTowards(this.m_Body.position, this.m_NetworkPosition, this.m_Distance * (1f / (float)PhotonNetwork.SerializationRate));
				this.m_Body.rotation = Quaternion.RotateTowards(this.m_Body.rotation, this.m_NetworkRotation, this.m_Angle * (1f / (float)PhotonNetwork.SerializationRate));
			}
		}

		// Token: 0x06000194 RID: 404 RVA: 0x0000A568 File Offset: 0x00008768
		public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
		{
			if (stream.IsWriting)
			{
				stream.SendNext(this.m_Body.position);
				stream.SendNext(this.m_Body.rotation);
				if (this.m_SynchronizeVelocity)
				{
					stream.SendNext(this.m_Body.velocity);
				}
				if (this.m_SynchronizeAngularVelocity)
				{
					stream.SendNext(this.m_Body.angularVelocity);
					return;
				}
			}
			else
			{
				this.m_NetworkPosition = (Vector3)stream.ReceiveNext();
				this.m_NetworkRotation = (Quaternion)stream.ReceiveNext();
				if (this.m_TeleportEnabled && Vector3.Distance(this.m_Body.position, this.m_NetworkPosition) > this.m_TeleportIfDistanceGreaterThan)
				{
					this.m_Body.position = this.m_NetworkPosition;
				}
				if (this.m_SynchronizeVelocity || this.m_SynchronizeAngularVelocity)
				{
					float d = Mathf.Abs((float)(PhotonNetwork.Time - info.SentServerTime));
					if (this.m_SynchronizeVelocity)
					{
						this.m_Body.velocity = (Vector3)stream.ReceiveNext();
						this.m_NetworkPosition += this.m_Body.velocity * d;
						this.m_Distance = Vector3.Distance(this.m_Body.position, this.m_NetworkPosition);
					}
					if (this.m_SynchronizeAngularVelocity)
					{
						this.m_Body.angularVelocity = (Vector3)stream.ReceiveNext();
						this.m_NetworkRotation = Quaternion.Euler(this.m_Body.angularVelocity * d) * this.m_NetworkRotation;
						this.m_Angle = Quaternion.Angle(this.m_Body.rotation, this.m_NetworkRotation);
					}
				}
			}
		}

		// Token: 0x06000195 RID: 405 RVA: 0x0000A726 File Offset: 0x00008926
		public PhotonRigidbodyView()
		{
		}

		// Token: 0x040000D8 RID: 216
		private float m_Distance;

		// Token: 0x040000D9 RID: 217
		private float m_Angle;

		// Token: 0x040000DA RID: 218
		private Rigidbody m_Body;

		// Token: 0x040000DB RID: 219
		private Vector3 m_NetworkPosition;

		// Token: 0x040000DC RID: 220
		private Quaternion m_NetworkRotation;

		// Token: 0x040000DD RID: 221
		[HideInInspector]
		public bool m_SynchronizeVelocity = true;

		// Token: 0x040000DE RID: 222
		[HideInInspector]
		public bool m_SynchronizeAngularVelocity;

		// Token: 0x040000DF RID: 223
		[HideInInspector]
		public bool m_TeleportEnabled;

		// Token: 0x040000E0 RID: 224
		[HideInInspector]
		public float m_TeleportIfDistanceGreaterThan = 3f;
	}
}
