using System;
using UnityEngine;

namespace Photon.Pun
{
	// Token: 0x02000023 RID: 35
	[AddComponentMenu("Photon Networking/Photon Transform View")]
	[HelpURL("https://doc.photonengine.com/en-us/pun/v2/gameplay/synchronization-and-state")]
	public class PhotonTransformView : MonoBehaviourPun, IPunObservable
	{
		// Token: 0x06000196 RID: 406 RVA: 0x0000A740 File Offset: 0x00008940
		public void Awake()
		{
			this.m_StoredPosition = base.transform.localPosition;
			this.m_NetworkPosition = Vector3.zero;
			this.m_NetworkRotation = Quaternion.identity;
		}

		// Token: 0x06000197 RID: 407 RVA: 0x0000A769 File Offset: 0x00008969
		private void Reset()
		{
			this.m_UseLocal = true;
		}

		// Token: 0x06000198 RID: 408 RVA: 0x0000A772 File Offset: 0x00008972
		private void OnEnable()
		{
			this.m_firstTake = true;
		}

		// Token: 0x06000199 RID: 409 RVA: 0x0000A77C File Offset: 0x0000897C
		public void Update()
		{
			Transform transform = base.transform;
			if (!base.photonView.IsMine)
			{
				if (this.m_UseLocal)
				{
					transform.localPosition = Vector3.MoveTowards(transform.localPosition, this.m_NetworkPosition, this.m_Distance * Time.deltaTime * (float)PhotonNetwork.SerializationRate);
					transform.localRotation = Quaternion.RotateTowards(transform.localRotation, this.m_NetworkRotation, this.m_Angle * Time.deltaTime * (float)PhotonNetwork.SerializationRate);
					return;
				}
				transform.position = Vector3.MoveTowards(transform.position, this.m_NetworkPosition, this.m_Distance * Time.deltaTime * (float)PhotonNetwork.SerializationRate);
				transform.rotation = Quaternion.RotateTowards(transform.rotation, this.m_NetworkRotation, this.m_Angle * Time.deltaTime * (float)PhotonNetwork.SerializationRate);
			}
		}

		// Token: 0x0600019A RID: 410 RVA: 0x0000A854 File Offset: 0x00008A54
		public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
		{
			Transform transform = base.transform;
			if (stream.IsWriting)
			{
				if (this.m_SynchronizePosition)
				{
					if (this.m_UseLocal)
					{
						this.m_Direction = transform.localPosition - this.m_StoredPosition;
						this.m_StoredPosition = transform.localPosition;
						stream.SendNext(transform.localPosition);
						stream.SendNext(this.m_Direction);
					}
					else
					{
						this.m_Direction = transform.position - this.m_StoredPosition;
						this.m_StoredPosition = transform.position;
						stream.SendNext(transform.position);
						stream.SendNext(this.m_Direction);
					}
				}
				if (this.m_SynchronizeRotation)
				{
					if (this.m_UseLocal)
					{
						stream.SendNext(transform.localRotation);
					}
					else
					{
						stream.SendNext(transform.rotation);
					}
				}
				if (this.m_SynchronizeScale)
				{
					stream.SendNext(transform.localScale);
					return;
				}
			}
			else
			{
				if (this.m_SynchronizePosition)
				{
					this.m_NetworkPosition = (Vector3)stream.ReceiveNext();
					this.m_Direction = (Vector3)stream.ReceiveNext();
					if (this.m_firstTake)
					{
						if (this.m_UseLocal)
						{
							transform.localPosition = this.m_NetworkPosition;
						}
						else
						{
							transform.position = this.m_NetworkPosition;
						}
						this.m_Distance = 0f;
					}
					else
					{
						float d = Mathf.Abs((float)(PhotonNetwork.Time - info.SentServerTime));
						this.m_NetworkPosition += this.m_Direction * d;
						if (this.m_UseLocal)
						{
							this.m_Distance = Vector3.Distance(transform.localPosition, this.m_NetworkPosition);
						}
						else
						{
							this.m_Distance = Vector3.Distance(transform.position, this.m_NetworkPosition);
						}
					}
				}
				if (this.m_SynchronizeRotation)
				{
					this.m_NetworkRotation = (Quaternion)stream.ReceiveNext();
					if (this.m_firstTake)
					{
						this.m_Angle = 0f;
						if (this.m_UseLocal)
						{
							transform.localRotation = this.m_NetworkRotation;
						}
						else
						{
							transform.rotation = this.m_NetworkRotation;
						}
					}
					else if (this.m_UseLocal)
					{
						this.m_Angle = Quaternion.Angle(transform.localRotation, this.m_NetworkRotation);
					}
					else
					{
						this.m_Angle = Quaternion.Angle(transform.rotation, this.m_NetworkRotation);
					}
				}
				if (this.m_SynchronizeScale)
				{
					transform.localScale = (Vector3)stream.ReceiveNext();
				}
				if (this.m_firstTake)
				{
					this.m_firstTake = false;
				}
			}
		}

		// Token: 0x0600019B RID: 411 RVA: 0x0000AAE3 File Offset: 0x00008CE3
		public PhotonTransformView()
		{
		}

		// Token: 0x040000E1 RID: 225
		private float m_Distance;

		// Token: 0x040000E2 RID: 226
		private float m_Angle;

		// Token: 0x040000E3 RID: 227
		private Vector3 m_Direction;

		// Token: 0x040000E4 RID: 228
		private Vector3 m_NetworkPosition;

		// Token: 0x040000E5 RID: 229
		private Vector3 m_StoredPosition;

		// Token: 0x040000E6 RID: 230
		private Quaternion m_NetworkRotation;

		// Token: 0x040000E7 RID: 231
		public bool m_SynchronizePosition = true;

		// Token: 0x040000E8 RID: 232
		public bool m_SynchronizeRotation = true;

		// Token: 0x040000E9 RID: 233
		public bool m_SynchronizeScale;

		// Token: 0x040000EA RID: 234
		[Tooltip("Indicates if localPosition and localRotation should be used. Scale ignores this setting, and always uses localScale to avoid issues with lossyScale.")]
		public bool m_UseLocal;

		// Token: 0x040000EB RID: 235
		private bool m_firstTake;
	}
}
