using System;
using UnityEngine;

namespace Photon.Pun
{
	// Token: 0x0200002A RID: 42
	public class PhotonTransformViewScaleControl
	{
		// Token: 0x060001B5 RID: 437 RVA: 0x0000B289 File Offset: 0x00009489
		public PhotonTransformViewScaleControl(PhotonTransformViewScaleModel model)
		{
			this.m_Model = model;
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x0000B2A3 File Offset: 0x000094A3
		public Vector3 GetNetworkScale()
		{
			return this.m_NetworkScale;
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x0000B2AC File Offset: 0x000094AC
		public Vector3 GetScale(Vector3 currentScale)
		{
			switch (this.m_Model.InterpolateOption)
			{
			default:
				return this.m_NetworkScale;
			case PhotonTransformViewScaleModel.InterpolateOptions.MoveTowards:
				return Vector3.MoveTowards(currentScale, this.m_NetworkScale, this.m_Model.InterpolateMoveTowardsSpeed * Time.deltaTime);
			case PhotonTransformViewScaleModel.InterpolateOptions.Lerp:
				return Vector3.Lerp(currentScale, this.m_NetworkScale, this.m_Model.InterpolateLerpSpeed * Time.deltaTime);
			}
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x0000B319 File Offset: 0x00009519
		public void OnPhotonSerializeView(Vector3 currentScale, PhotonStream stream, PhotonMessageInfo info)
		{
			if (!this.m_Model.SynchronizeEnabled)
			{
				return;
			}
			if (stream.IsWriting)
			{
				stream.SendNext(currentScale);
				this.m_NetworkScale = currentScale;
				return;
			}
			this.m_NetworkScale = (Vector3)stream.ReceiveNext();
		}

		// Token: 0x04000111 RID: 273
		private PhotonTransformViewScaleModel m_Model;

		// Token: 0x04000112 RID: 274
		private Vector3 m_NetworkScale = Vector3.one;
	}
}
