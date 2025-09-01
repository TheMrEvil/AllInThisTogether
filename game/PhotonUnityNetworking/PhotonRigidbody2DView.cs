using System;
using UnityEngine;

namespace Photon.Pun
{
	// Token: 0x02000021 RID: 33
	[RequireComponent(typeof(Rigidbody2D))]
	[AddComponentMenu("Photon Networking/Photon Rigidbody 2D View")]
	public class PhotonRigidbody2DView : MonoBehaviourPun, IPunObservable
	{
		// Token: 0x0600018E RID: 398 RVA: 0x0000A24A File Offset: 0x0000844A
		public void Awake()
		{
			this.m_Body = base.GetComponent<Rigidbody2D>();
			this.m_NetworkPosition = default(Vector2);
		}

		// Token: 0x0600018F RID: 399 RVA: 0x0000A264 File Offset: 0x00008464
		public void FixedUpdate()
		{
			if (!base.photonView.IsMine)
			{
				this.m_Body.position = Vector2.MoveTowards(this.m_Body.position, this.m_NetworkPosition, this.m_Distance * (1f / (float)PhotonNetwork.SerializationRate));
				this.m_Body.rotation = Mathf.MoveTowards(this.m_Body.rotation, this.m_NetworkRotation, this.m_Angle * (1f / (float)PhotonNetwork.SerializationRate));
			}
		}

		// Token: 0x06000190 RID: 400 RVA: 0x0000A2E8 File Offset: 0x000084E8
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
				this.m_NetworkPosition = (Vector2)stream.ReceiveNext();
				this.m_NetworkRotation = (float)stream.ReceiveNext();
				if (this.m_TeleportEnabled && Vector3.Distance(this.m_Body.position, this.m_NetworkPosition) > this.m_TeleportIfDistanceGreaterThan)
				{
					this.m_Body.position = this.m_NetworkPosition;
				}
				if (this.m_SynchronizeVelocity || this.m_SynchronizeAngularVelocity)
				{
					float num = Mathf.Abs((float)(PhotonNetwork.Time - info.SentServerTime));
					if (this.m_SynchronizeVelocity)
					{
						this.m_Body.velocity = (Vector2)stream.ReceiveNext();
						this.m_NetworkPosition += this.m_Body.velocity * num;
						this.m_Distance = Vector2.Distance(this.m_Body.position, this.m_NetworkPosition);
					}
					if (this.m_SynchronizeAngularVelocity)
					{
						this.m_Body.angularVelocity = (float)stream.ReceiveNext();
						this.m_NetworkRotation += this.m_Body.angularVelocity * num;
						this.m_Angle = Mathf.Abs(this.m_Body.rotation - this.m_NetworkRotation);
					}
				}
			}
		}

		// Token: 0x06000191 RID: 401 RVA: 0x0000A4A4 File Offset: 0x000086A4
		public PhotonRigidbody2DView()
		{
		}

		// Token: 0x040000CF RID: 207
		private float m_Distance;

		// Token: 0x040000D0 RID: 208
		private float m_Angle;

		// Token: 0x040000D1 RID: 209
		private Rigidbody2D m_Body;

		// Token: 0x040000D2 RID: 210
		private Vector2 m_NetworkPosition;

		// Token: 0x040000D3 RID: 211
		private float m_NetworkRotation;

		// Token: 0x040000D4 RID: 212
		[HideInInspector]
		public bool m_SynchronizeVelocity = true;

		// Token: 0x040000D5 RID: 213
		[HideInInspector]
		public bool m_SynchronizeAngularVelocity;

		// Token: 0x040000D6 RID: 214
		[HideInInspector]
		public bool m_TeleportEnabled;

		// Token: 0x040000D7 RID: 215
		[HideInInspector]
		public float m_TeleportIfDistanceGreaterThan = 3f;
	}
}
