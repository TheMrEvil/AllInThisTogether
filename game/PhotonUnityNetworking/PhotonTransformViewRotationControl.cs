using System;
using UnityEngine;

namespace Photon.Pun
{
	// Token: 0x02000028 RID: 40
	public class PhotonTransformViewRotationControl
	{
		// Token: 0x060001B0 RID: 432 RVA: 0x0000B1B2 File Offset: 0x000093B2
		public PhotonTransformViewRotationControl(PhotonTransformViewRotationModel model)
		{
			this.m_Model = model;
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x0000B1C1 File Offset: 0x000093C1
		public Quaternion GetNetworkRotation()
		{
			return this.m_NetworkRotation;
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x0000B1CC File Offset: 0x000093CC
		public Quaternion GetRotation(Quaternion currentRotation)
		{
			switch (this.m_Model.InterpolateOption)
			{
			default:
				return this.m_NetworkRotation;
			case PhotonTransformViewRotationModel.InterpolateOptions.RotateTowards:
				return Quaternion.RotateTowards(currentRotation, this.m_NetworkRotation, this.m_Model.InterpolateRotateTowardsSpeed * Time.deltaTime);
			case PhotonTransformViewRotationModel.InterpolateOptions.Lerp:
				return Quaternion.Lerp(currentRotation, this.m_NetworkRotation, this.m_Model.InterpolateLerpSpeed * Time.deltaTime);
			}
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x0000B239 File Offset: 0x00009439
		public void OnPhotonSerializeView(Quaternion currentRotation, PhotonStream stream, PhotonMessageInfo info)
		{
			if (!this.m_Model.SynchronizeEnabled)
			{
				return;
			}
			if (stream.IsWriting)
			{
				stream.SendNext(currentRotation);
				this.m_NetworkRotation = currentRotation;
				return;
			}
			this.m_NetworkRotation = (Quaternion)stream.ReceiveNext();
		}

		// Token: 0x0400010B RID: 267
		private PhotonTransformViewRotationModel m_Model;

		// Token: 0x0400010C RID: 268
		private Quaternion m_NetworkRotation;
	}
}
