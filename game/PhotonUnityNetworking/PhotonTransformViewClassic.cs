using System;
using UnityEngine;

namespace Photon.Pun
{
	// Token: 0x02000024 RID: 36
	[AddComponentMenu("Photon Networking/Photon Transform View Classic")]
	public class PhotonTransformViewClassic : MonoBehaviourPun, IPunObservable
	{
		// Token: 0x0600019C RID: 412 RVA: 0x0000AAFC File Offset: 0x00008CFC
		private void Awake()
		{
			this.m_PhotonView = base.GetComponent<PhotonView>();
			this.m_PositionControl = new PhotonTransformViewPositionControl(this.m_PositionModel);
			this.m_RotationControl = new PhotonTransformViewRotationControl(this.m_RotationModel);
			this.m_ScaleControl = new PhotonTransformViewScaleControl(this.m_ScaleModel);
		}

		// Token: 0x0600019D RID: 413 RVA: 0x0000AB48 File Offset: 0x00008D48
		private void OnEnable()
		{
			this.m_firstTake = true;
		}

		// Token: 0x0600019E RID: 414 RVA: 0x0000AB51 File Offset: 0x00008D51
		private void Update()
		{
			if (this.m_PhotonView == null || this.m_PhotonView.IsMine || !PhotonNetwork.IsConnectedAndReady)
			{
				return;
			}
			this.UpdatePosition();
			this.UpdateRotation();
			this.UpdateScale();
		}

		// Token: 0x0600019F RID: 415 RVA: 0x0000AB88 File Offset: 0x00008D88
		private void UpdatePosition()
		{
			if (!this.m_PositionModel.SynchronizeEnabled || !this.m_ReceivedNetworkUpdate)
			{
				return;
			}
			base.transform.localPosition = this.m_PositionControl.UpdatePosition(base.transform.localPosition);
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x0000ABC1 File Offset: 0x00008DC1
		private void UpdateRotation()
		{
			if (!this.m_RotationModel.SynchronizeEnabled || !this.m_ReceivedNetworkUpdate)
			{
				return;
			}
			base.transform.localRotation = this.m_RotationControl.GetRotation(base.transform.localRotation);
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x0000ABFA File Offset: 0x00008DFA
		private void UpdateScale()
		{
			if (!this.m_ScaleModel.SynchronizeEnabled || !this.m_ReceivedNetworkUpdate)
			{
				return;
			}
			base.transform.localScale = this.m_ScaleControl.GetScale(base.transform.localScale);
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x0000AC33 File Offset: 0x00008E33
		public void SetSynchronizedValues(Vector3 speed, float turnSpeed)
		{
			this.m_PositionControl.SetSynchronizedValues(speed, turnSpeed);
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x0000AC44 File Offset: 0x00008E44
		public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
		{
			this.m_PositionControl.OnPhotonSerializeView(base.transform.localPosition, stream, info);
			this.m_RotationControl.OnPhotonSerializeView(base.transform.localRotation, stream, info);
			this.m_ScaleControl.OnPhotonSerializeView(base.transform.localScale, stream, info);
			if (stream.IsReading)
			{
				this.m_ReceivedNetworkUpdate = true;
				if (this.m_firstTake)
				{
					this.m_firstTake = false;
					if (this.m_PositionModel.SynchronizeEnabled)
					{
						base.transform.localPosition = this.m_PositionControl.GetNetworkPosition();
					}
					if (this.m_RotationModel.SynchronizeEnabled)
					{
						base.transform.localRotation = this.m_RotationControl.GetNetworkRotation();
					}
					if (this.m_ScaleModel.SynchronizeEnabled)
					{
						base.transform.localScale = this.m_ScaleControl.GetNetworkScale();
					}
				}
			}
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x0000AD20 File Offset: 0x00008F20
		public PhotonTransformViewClassic()
		{
		}

		// Token: 0x040000EC RID: 236
		[HideInInspector]
		public PhotonTransformViewPositionModel m_PositionModel = new PhotonTransformViewPositionModel();

		// Token: 0x040000ED RID: 237
		[HideInInspector]
		public PhotonTransformViewRotationModel m_RotationModel = new PhotonTransformViewRotationModel();

		// Token: 0x040000EE RID: 238
		[HideInInspector]
		public PhotonTransformViewScaleModel m_ScaleModel = new PhotonTransformViewScaleModel();

		// Token: 0x040000EF RID: 239
		private PhotonTransformViewPositionControl m_PositionControl;

		// Token: 0x040000F0 RID: 240
		private PhotonTransformViewRotationControl m_RotationControl;

		// Token: 0x040000F1 RID: 241
		private PhotonTransformViewScaleControl m_ScaleControl;

		// Token: 0x040000F2 RID: 242
		private PhotonView m_PhotonView;

		// Token: 0x040000F3 RID: 243
		private bool m_ReceivedNetworkUpdate;

		// Token: 0x040000F4 RID: 244
		private bool m_firstTake;
	}
}
