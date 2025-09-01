using System;
using System.Collections.Generic;
using UnityEngine;

namespace Photon.Pun
{
	// Token: 0x02000013 RID: 19
	public class PhotonStreamQueue
	{
		// Token: 0x060000DF RID: 223 RVA: 0x00007A44 File Offset: 0x00005C44
		public PhotonStreamQueue(int sampleRate)
		{
			this.m_SampleRate = sampleRate;
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00007A80 File Offset: 0x00005C80
		private void BeginWritePackage()
		{
			if (Time.realtimeSinceStartup < this.m_LastSampleTime + 1f / (float)this.m_SampleRate)
			{
				this.m_IsWriting = false;
				return;
			}
			if (this.m_SampleCount == 1)
			{
				this.m_ObjectsPerSample = this.m_Objects.Count;
			}
			else if (this.m_SampleCount > 1 && this.m_Objects.Count / this.m_SampleCount != this.m_ObjectsPerSample)
			{
				Debug.LogWarning("The number of objects sent via a PhotonStreamQueue has to be the same each frame");
				Debug.LogWarning(string.Concat(new string[]
				{
					"Objects in List: ",
					this.m_Objects.Count.ToString(),
					" / Sample Count: ",
					this.m_SampleCount.ToString(),
					" = ",
					(this.m_Objects.Count / this.m_SampleCount).ToString(),
					" != ",
					this.m_ObjectsPerSample.ToString()
				}));
			}
			this.m_IsWriting = true;
			this.m_SampleCount++;
			this.m_LastSampleTime = Time.realtimeSinceStartup;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00007BA1 File Offset: 0x00005DA1
		public void Reset()
		{
			this.m_SampleCount = 0;
			this.m_ObjectsPerSample = -1;
			this.m_LastSampleTime = float.NegativeInfinity;
			this.m_LastFrameCount = -1;
			this.m_Objects.Clear();
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00007BCE File Offset: 0x00005DCE
		public void SendNext(object obj)
		{
			if (Time.frameCount != this.m_LastFrameCount)
			{
				this.BeginWritePackage();
			}
			this.m_LastFrameCount = Time.frameCount;
			if (!this.m_IsWriting)
			{
				return;
			}
			this.m_Objects.Add(obj);
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00007C03 File Offset: 0x00005E03
		public bool HasQueuedObjects()
		{
			return this.m_NextObjectIndex != -1;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00007C14 File Offset: 0x00005E14
		public object ReceiveNext()
		{
			if (this.m_NextObjectIndex == -1)
			{
				return null;
			}
			if (this.m_NextObjectIndex >= this.m_Objects.Count)
			{
				this.m_NextObjectIndex -= this.m_ObjectsPerSample;
			}
			List<object> objects = this.m_Objects;
			int nextObjectIndex = this.m_NextObjectIndex;
			this.m_NextObjectIndex = nextObjectIndex + 1;
			return objects[nextObjectIndex];
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00007C70 File Offset: 0x00005E70
		public void Serialize(PhotonStream stream)
		{
			if (this.m_Objects.Count > 0 && this.m_ObjectsPerSample < 0)
			{
				this.m_ObjectsPerSample = this.m_Objects.Count;
			}
			stream.SendNext(this.m_SampleCount);
			stream.SendNext(this.m_ObjectsPerSample);
			for (int i = 0; i < this.m_Objects.Count; i++)
			{
				stream.SendNext(this.m_Objects[i]);
			}
			this.m_Objects.Clear();
			this.m_SampleCount = 0;
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00007D04 File Offset: 0x00005F04
		public void Deserialize(PhotonStream stream)
		{
			this.m_Objects.Clear();
			this.m_SampleCount = (int)stream.ReceiveNext();
			this.m_ObjectsPerSample = (int)stream.ReceiveNext();
			for (int i = 0; i < this.m_SampleCount * this.m_ObjectsPerSample; i++)
			{
				this.m_Objects.Add(stream.ReceiveNext());
			}
			if (this.m_Objects.Count > 0)
			{
				this.m_NextObjectIndex = 0;
				return;
			}
			this.m_NextObjectIndex = -1;
		}

		// Token: 0x04000080 RID: 128
		private int m_SampleRate;

		// Token: 0x04000081 RID: 129
		private int m_SampleCount;

		// Token: 0x04000082 RID: 130
		private int m_ObjectsPerSample = -1;

		// Token: 0x04000083 RID: 131
		private float m_LastSampleTime = float.NegativeInfinity;

		// Token: 0x04000084 RID: 132
		private int m_LastFrameCount = -1;

		// Token: 0x04000085 RID: 133
		private int m_NextObjectIndex = -1;

		// Token: 0x04000086 RID: 134
		private List<object> m_Objects = new List<object>();

		// Token: 0x04000087 RID: 135
		private bool m_IsWriting;
	}
}
