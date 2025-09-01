using System;
using System.Collections.Generic;
using UnityEngine;

namespace Photon.Pun
{
	// Token: 0x02000026 RID: 38
	public class PhotonTransformViewPositionControl
	{
		// Token: 0x060001A6 RID: 422 RVA: 0x0000ADA7 File Offset: 0x00008FA7
		public PhotonTransformViewPositionControl(PhotonTransformViewPositionModel model)
		{
			this.m_Model = model;
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x0000ADD4 File Offset: 0x00008FD4
		private Vector3 GetOldestStoredNetworkPosition()
		{
			Vector3 result = this.m_NetworkPosition;
			if (this.m_OldNetworkPositions.Count > 0)
			{
				result = this.m_OldNetworkPositions.Peek();
			}
			return result;
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x0000AE03 File Offset: 0x00009003
		public void SetSynchronizedValues(Vector3 speed, float turnSpeed)
		{
			this.m_SynchronizedSpeed = speed;
			this.m_SynchronizedTurnSpeed = turnSpeed;
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x0000AE14 File Offset: 0x00009014
		public Vector3 UpdatePosition(Vector3 currentPosition)
		{
			Vector3 vector = this.GetNetworkPosition() + this.GetExtrapolatedPositionOffset();
			switch (this.m_Model.InterpolateOption)
			{
			case PhotonTransformViewPositionModel.InterpolateOptions.Disabled:
				if (!this.m_UpdatedPositionAfterOnSerialize)
				{
					currentPosition = vector;
					this.m_UpdatedPositionAfterOnSerialize = true;
				}
				break;
			case PhotonTransformViewPositionModel.InterpolateOptions.FixedSpeed:
				currentPosition = Vector3.MoveTowards(currentPosition, vector, Time.deltaTime * this.m_Model.InterpolateMoveTowardsSpeed);
				break;
			case PhotonTransformViewPositionModel.InterpolateOptions.EstimatedSpeed:
				if (this.m_OldNetworkPositions.Count != 0)
				{
					float num = Vector3.Distance(this.m_NetworkPosition, this.GetOldestStoredNetworkPosition()) / (float)this.m_OldNetworkPositions.Count * (float)PhotonNetwork.SerializationRate;
					currentPosition = Vector3.MoveTowards(currentPosition, vector, Time.deltaTime * num);
				}
				break;
			case PhotonTransformViewPositionModel.InterpolateOptions.SynchronizeValues:
				if (this.m_SynchronizedSpeed.magnitude == 0f)
				{
					currentPosition = vector;
				}
				else
				{
					currentPosition = Vector3.MoveTowards(currentPosition, vector, Time.deltaTime * this.m_SynchronizedSpeed.magnitude);
				}
				break;
			case PhotonTransformViewPositionModel.InterpolateOptions.Lerp:
				currentPosition = Vector3.Lerp(currentPosition, vector, Time.deltaTime * this.m_Model.InterpolateLerpSpeed);
				break;
			}
			if (this.m_Model.TeleportEnabled && Vector3.Distance(currentPosition, this.GetNetworkPosition()) > this.m_Model.TeleportIfDistanceGreaterThan)
			{
				currentPosition = this.GetNetworkPosition();
			}
			return currentPosition;
		}

		// Token: 0x060001AA RID: 426 RVA: 0x0000AF5B File Offset: 0x0000915B
		public Vector3 GetNetworkPosition()
		{
			return this.m_NetworkPosition;
		}

		// Token: 0x060001AB RID: 427 RVA: 0x0000AF64 File Offset: 0x00009164
		public Vector3 GetExtrapolatedPositionOffset()
		{
			float num = (float)(PhotonNetwork.Time - this.m_LastSerializeTime);
			if (this.m_Model.ExtrapolateIncludingRoundTripTime)
			{
				num += (float)PhotonNetwork.GetPing() / 1000f;
			}
			Vector3 result = Vector3.zero;
			switch (this.m_Model.ExtrapolateOption)
			{
			case PhotonTransformViewPositionModel.ExtrapolateOptions.SynchronizeValues:
				result = Quaternion.Euler(0f, this.m_SynchronizedTurnSpeed * num, 0f) * (this.m_SynchronizedSpeed * num);
				break;
			case PhotonTransformViewPositionModel.ExtrapolateOptions.EstimateSpeedAndTurn:
				result = (this.m_NetworkPosition - this.GetOldestStoredNetworkPosition()) * (float)PhotonNetwork.SerializationRate * num;
				break;
			case PhotonTransformViewPositionModel.ExtrapolateOptions.FixedSpeed:
				result = (this.m_NetworkPosition - this.GetOldestStoredNetworkPosition()).normalized * this.m_Model.ExtrapolateSpeed * num;
				break;
			}
			return result;
		}

		// Token: 0x060001AC RID: 428 RVA: 0x0000B047 File Offset: 0x00009247
		public void OnPhotonSerializeView(Vector3 currentPosition, PhotonStream stream, PhotonMessageInfo info)
		{
			if (!this.m_Model.SynchronizeEnabled)
			{
				return;
			}
			if (stream.IsWriting)
			{
				this.SerializeData(currentPosition, stream, info);
			}
			else
			{
				this.DeserializeData(stream, info);
			}
			this.m_LastSerializeTime = PhotonNetwork.Time;
			this.m_UpdatedPositionAfterOnSerialize = false;
		}

		// Token: 0x060001AD RID: 429 RVA: 0x0000B084 File Offset: 0x00009284
		private void SerializeData(Vector3 currentPosition, PhotonStream stream, PhotonMessageInfo info)
		{
			stream.SendNext(currentPosition);
			this.m_NetworkPosition = currentPosition;
			if (this.m_Model.ExtrapolateOption == PhotonTransformViewPositionModel.ExtrapolateOptions.SynchronizeValues || this.m_Model.InterpolateOption == PhotonTransformViewPositionModel.InterpolateOptions.SynchronizeValues)
			{
				stream.SendNext(this.m_SynchronizedSpeed);
				stream.SendNext(this.m_SynchronizedTurnSpeed);
			}
		}

		// Token: 0x060001AE RID: 430 RVA: 0x0000B0E4 File Offset: 0x000092E4
		private void DeserializeData(PhotonStream stream, PhotonMessageInfo info)
		{
			Vector3 networkPosition = (Vector3)stream.ReceiveNext();
			if (this.m_Model.ExtrapolateOption == PhotonTransformViewPositionModel.ExtrapolateOptions.SynchronizeValues || this.m_Model.InterpolateOption == PhotonTransformViewPositionModel.InterpolateOptions.SynchronizeValues)
			{
				this.m_SynchronizedSpeed = (Vector3)stream.ReceiveNext();
				this.m_SynchronizedTurnSpeed = (float)stream.ReceiveNext();
			}
			if (this.m_OldNetworkPositions.Count == 0)
			{
				this.m_NetworkPosition = networkPosition;
			}
			this.m_OldNetworkPositions.Enqueue(this.m_NetworkPosition);
			this.m_NetworkPosition = networkPosition;
			while (this.m_OldNetworkPositions.Count > this.m_Model.ExtrapolateNumberOfStoredPositions)
			{
				this.m_OldNetworkPositions.Dequeue();
			}
		}

		// Token: 0x040000FF RID: 255
		private PhotonTransformViewPositionModel m_Model;

		// Token: 0x04000100 RID: 256
		private float m_CurrentSpeed;

		// Token: 0x04000101 RID: 257
		private double m_LastSerializeTime;

		// Token: 0x04000102 RID: 258
		private Vector3 m_SynchronizedSpeed = Vector3.zero;

		// Token: 0x04000103 RID: 259
		private float m_SynchronizedTurnSpeed;

		// Token: 0x04000104 RID: 260
		private Vector3 m_NetworkPosition;

		// Token: 0x04000105 RID: 261
		private Queue<Vector3> m_OldNetworkPositions = new Queue<Vector3>();

		// Token: 0x04000106 RID: 262
		private bool m_UpdatedPositionAfterOnSerialize = true;
	}
}
