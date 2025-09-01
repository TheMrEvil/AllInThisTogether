using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Photon.Pun
{
	// Token: 0x02000020 RID: 32
	[AddComponentMenu("Photon Networking/Photon Animator View")]
	public class PhotonAnimatorView : MonoBehaviourPun, IPunObservable
	{
		// Token: 0x0600017B RID: 379 RVA: 0x000097AC File Offset: 0x000079AC
		private void Awake()
		{
			this.m_Animator = base.GetComponent<Animator>();
		}

		// Token: 0x0600017C RID: 380 RVA: 0x000097BC File Offset: 0x000079BC
		private void Update()
		{
			if (this.m_Animator.applyRootMotion && !base.photonView.IsMine && PhotonNetwork.IsConnected)
			{
				this.m_Animator.applyRootMotion = false;
			}
			if (!PhotonNetwork.InRoom || PhotonNetwork.CurrentRoom.PlayerCount <= 1)
			{
				this.m_StreamQueue.Reset();
				return;
			}
			if (base.photonView.IsMine)
			{
				this.SerializeDataContinuously();
				this.CacheDiscreteTriggers();
				return;
			}
			this.DeserializeDataContinuously();
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00009838 File Offset: 0x00007A38
		public void CacheDiscreteTriggers()
		{
			for (int i = 0; i < this.m_SynchronizeParameters.Count; i++)
			{
				PhotonAnimatorView.SynchronizedParameter synchronizedParameter = this.m_SynchronizeParameters[i];
				if (synchronizedParameter.SynchronizeType == PhotonAnimatorView.SynchronizeType.Discrete && synchronizedParameter.Type == PhotonAnimatorView.ParameterType.Trigger && this.m_Animator.GetBool(synchronizedParameter.Name) && synchronizedParameter.Type == PhotonAnimatorView.ParameterType.Trigger)
				{
					this.m_raisedDiscreteTriggersCache.Add(synchronizedParameter.Name);
					return;
				}
			}
		}

		// Token: 0x0600017E RID: 382 RVA: 0x000098AC File Offset: 0x00007AAC
		public bool DoesLayerSynchronizeTypeExist(int layerIndex)
		{
			return this.m_SynchronizeLayers.FindIndex((PhotonAnimatorView.SynchronizedLayer item) => item.LayerIndex == layerIndex) != -1;
		}

		// Token: 0x0600017F RID: 383 RVA: 0x000098E4 File Offset: 0x00007AE4
		public bool DoesParameterSynchronizeTypeExist(string name)
		{
			return this.m_SynchronizeParameters.FindIndex((PhotonAnimatorView.SynchronizedParameter item) => item.Name == name) != -1;
		}

		// Token: 0x06000180 RID: 384 RVA: 0x0000991B File Offset: 0x00007B1B
		public List<PhotonAnimatorView.SynchronizedLayer> GetSynchronizedLayers()
		{
			return this.m_SynchronizeLayers;
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00009923 File Offset: 0x00007B23
		public List<PhotonAnimatorView.SynchronizedParameter> GetSynchronizedParameters()
		{
			return this.m_SynchronizeParameters;
		}

		// Token: 0x06000182 RID: 386 RVA: 0x0000992C File Offset: 0x00007B2C
		public PhotonAnimatorView.SynchronizeType GetLayerSynchronizeType(int layerIndex)
		{
			int num = this.m_SynchronizeLayers.FindIndex((PhotonAnimatorView.SynchronizedLayer item) => item.LayerIndex == layerIndex);
			if (num == -1)
			{
				return PhotonAnimatorView.SynchronizeType.Disabled;
			}
			return this.m_SynchronizeLayers[num].SynchronizeType;
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00009978 File Offset: 0x00007B78
		public PhotonAnimatorView.SynchronizeType GetParameterSynchronizeType(string name)
		{
			int num = this.m_SynchronizeParameters.FindIndex((PhotonAnimatorView.SynchronizedParameter item) => item.Name == name);
			if (num == -1)
			{
				return PhotonAnimatorView.SynchronizeType.Disabled;
			}
			return this.m_SynchronizeParameters[num].SynchronizeType;
		}

		// Token: 0x06000184 RID: 388 RVA: 0x000099C4 File Offset: 0x00007BC4
		public void SetLayerSynchronized(int layerIndex, PhotonAnimatorView.SynchronizeType synchronizeType)
		{
			if (Application.isPlaying)
			{
				this.m_WasSynchronizeTypeChanged = true;
			}
			int num = this.m_SynchronizeLayers.FindIndex((PhotonAnimatorView.SynchronizedLayer item) => item.LayerIndex == layerIndex);
			if (num == -1)
			{
				this.m_SynchronizeLayers.Add(new PhotonAnimatorView.SynchronizedLayer
				{
					LayerIndex = layerIndex,
					SynchronizeType = synchronizeType
				});
				return;
			}
			this.m_SynchronizeLayers[num].SynchronizeType = synchronizeType;
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00009A40 File Offset: 0x00007C40
		public void SetParameterSynchronized(string name, PhotonAnimatorView.ParameterType type, PhotonAnimatorView.SynchronizeType synchronizeType)
		{
			if (Application.isPlaying)
			{
				this.m_WasSynchronizeTypeChanged = true;
			}
			int num = this.m_SynchronizeParameters.FindIndex((PhotonAnimatorView.SynchronizedParameter item) => item.Name == name);
			if (num == -1)
			{
				this.m_SynchronizeParameters.Add(new PhotonAnimatorView.SynchronizedParameter
				{
					Name = name,
					Type = type,
					SynchronizeType = synchronizeType
				});
				return;
			}
			this.m_SynchronizeParameters[num].SynchronizeType = synchronizeType;
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00009AC4 File Offset: 0x00007CC4
		private void SerializeDataContinuously()
		{
			if (this.m_Animator == null)
			{
				return;
			}
			for (int i = 0; i < this.m_SynchronizeLayers.Count; i++)
			{
				if (this.m_SynchronizeLayers[i].SynchronizeType == PhotonAnimatorView.SynchronizeType.Continuous)
				{
					this.m_StreamQueue.SendNext(this.m_Animator.GetLayerWeight(this.m_SynchronizeLayers[i].LayerIndex));
				}
			}
			for (int j = 0; j < this.m_SynchronizeParameters.Count; j++)
			{
				PhotonAnimatorView.SynchronizedParameter synchronizedParameter = this.m_SynchronizeParameters[j];
				if (synchronizedParameter.SynchronizeType == PhotonAnimatorView.SynchronizeType.Continuous)
				{
					PhotonAnimatorView.ParameterType type = synchronizedParameter.Type;
					switch (type)
					{
					case PhotonAnimatorView.ParameterType.Float:
						this.m_StreamQueue.SendNext(this.m_Animator.GetFloat(synchronizedParameter.Name));
						break;
					case (PhotonAnimatorView.ParameterType)2:
						break;
					case PhotonAnimatorView.ParameterType.Int:
						this.m_StreamQueue.SendNext(this.m_Animator.GetInteger(synchronizedParameter.Name));
						break;
					case PhotonAnimatorView.ParameterType.Bool:
						this.m_StreamQueue.SendNext(this.m_Animator.GetBool(synchronizedParameter.Name));
						break;
					default:
						if (type == PhotonAnimatorView.ParameterType.Trigger)
						{
							if (!this.TriggerUsageWarningDone)
							{
								this.TriggerUsageWarningDone = true;
								Debug.Log("PhotonAnimatorView: When using triggers, make sure this component is last in the stack.\nIf you still experience issues, implement triggers as a regular RPC \nor in custom IPunObservable component instead", this);
							}
							this.m_StreamQueue.SendNext(this.m_Animator.GetBool(synchronizedParameter.Name));
						}
						break;
					}
				}
			}
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00009C3C File Offset: 0x00007E3C
		private void DeserializeDataContinuously()
		{
			if (!this.m_StreamQueue.HasQueuedObjects())
			{
				return;
			}
			for (int i = 0; i < this.m_SynchronizeLayers.Count; i++)
			{
				if (this.m_SynchronizeLayers[i].SynchronizeType == PhotonAnimatorView.SynchronizeType.Continuous)
				{
					this.m_Animator.SetLayerWeight(this.m_SynchronizeLayers[i].LayerIndex, (float)this.m_StreamQueue.ReceiveNext());
				}
			}
			for (int j = 0; j < this.m_SynchronizeParameters.Count; j++)
			{
				PhotonAnimatorView.SynchronizedParameter synchronizedParameter = this.m_SynchronizeParameters[j];
				if (synchronizedParameter.SynchronizeType == PhotonAnimatorView.SynchronizeType.Continuous)
				{
					PhotonAnimatorView.ParameterType type = synchronizedParameter.Type;
					switch (type)
					{
					case PhotonAnimatorView.ParameterType.Float:
						this.m_Animator.SetFloat(synchronizedParameter.Name, (float)this.m_StreamQueue.ReceiveNext());
						break;
					case (PhotonAnimatorView.ParameterType)2:
						break;
					case PhotonAnimatorView.ParameterType.Int:
						this.m_Animator.SetInteger(synchronizedParameter.Name, (int)this.m_StreamQueue.ReceiveNext());
						break;
					case PhotonAnimatorView.ParameterType.Bool:
						this.m_Animator.SetBool(synchronizedParameter.Name, (bool)this.m_StreamQueue.ReceiveNext());
						break;
					default:
						if (type == PhotonAnimatorView.ParameterType.Trigger)
						{
							this.m_Animator.SetBool(synchronizedParameter.Name, (bool)this.m_StreamQueue.ReceiveNext());
						}
						break;
					}
				}
			}
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00009D98 File Offset: 0x00007F98
		private void SerializeDataDiscretly(PhotonStream stream)
		{
			for (int i = 0; i < this.m_SynchronizeLayers.Count; i++)
			{
				if (this.m_SynchronizeLayers[i].SynchronizeType == PhotonAnimatorView.SynchronizeType.Discrete)
				{
					stream.SendNext(this.m_Animator.GetLayerWeight(this.m_SynchronizeLayers[i].LayerIndex));
				}
			}
			for (int j = 0; j < this.m_SynchronizeParameters.Count; j++)
			{
				PhotonAnimatorView.SynchronizedParameter synchronizedParameter = this.m_SynchronizeParameters[j];
				if (synchronizedParameter.SynchronizeType == PhotonAnimatorView.SynchronizeType.Discrete)
				{
					PhotonAnimatorView.ParameterType type = synchronizedParameter.Type;
					switch (type)
					{
					case PhotonAnimatorView.ParameterType.Float:
						stream.SendNext(this.m_Animator.GetFloat(synchronizedParameter.Name));
						break;
					case (PhotonAnimatorView.ParameterType)2:
						break;
					case PhotonAnimatorView.ParameterType.Int:
						stream.SendNext(this.m_Animator.GetInteger(synchronizedParameter.Name));
						break;
					case PhotonAnimatorView.ParameterType.Bool:
						stream.SendNext(this.m_Animator.GetBool(synchronizedParameter.Name));
						break;
					default:
						if (type == PhotonAnimatorView.ParameterType.Trigger)
						{
							if (!this.TriggerUsageWarningDone)
							{
								this.TriggerUsageWarningDone = true;
								Debug.Log("PhotonAnimatorView: When using triggers, make sure this component is last in the stack.\nIf you still experience issues, implement triggers as a regular RPC \nor in custom IPunObservable component instead", this);
							}
							stream.SendNext(this.m_raisedDiscreteTriggersCache.Contains(synchronizedParameter.Name));
						}
						break;
					}
				}
			}
			this.m_raisedDiscreteTriggersCache.Clear();
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00009EF0 File Offset: 0x000080F0
		private void DeserializeDataDiscretly(PhotonStream stream)
		{
			for (int i = 0; i < this.m_SynchronizeLayers.Count; i++)
			{
				if (this.m_SynchronizeLayers[i].SynchronizeType == PhotonAnimatorView.SynchronizeType.Discrete)
				{
					this.m_Animator.SetLayerWeight(this.m_SynchronizeLayers[i].LayerIndex, (float)stream.ReceiveNext());
				}
			}
			for (int j = 0; j < this.m_SynchronizeParameters.Count; j++)
			{
				PhotonAnimatorView.SynchronizedParameter synchronizedParameter = this.m_SynchronizeParameters[j];
				if (synchronizedParameter.SynchronizeType == PhotonAnimatorView.SynchronizeType.Discrete)
				{
					PhotonAnimatorView.ParameterType type = synchronizedParameter.Type;
					switch (type)
					{
					case PhotonAnimatorView.ParameterType.Float:
						if (!(stream.PeekNext() is float))
						{
							return;
						}
						this.m_Animator.SetFloat(synchronizedParameter.Name, (float)stream.ReceiveNext());
						break;
					case (PhotonAnimatorView.ParameterType)2:
						break;
					case PhotonAnimatorView.ParameterType.Int:
						if (!(stream.PeekNext() is int))
						{
							return;
						}
						this.m_Animator.SetInteger(synchronizedParameter.Name, (int)stream.ReceiveNext());
						break;
					case PhotonAnimatorView.ParameterType.Bool:
						if (!(stream.PeekNext() is bool))
						{
							return;
						}
						this.m_Animator.SetBool(synchronizedParameter.Name, (bool)stream.ReceiveNext());
						break;
					default:
						if (type == PhotonAnimatorView.ParameterType.Trigger)
						{
							if (!(stream.PeekNext() is bool))
							{
								return;
							}
							if ((bool)stream.ReceiveNext())
							{
								this.m_Animator.SetTrigger(synchronizedParameter.Name);
							}
						}
						break;
					}
				}
			}
		}

		// Token: 0x0600018A RID: 394 RVA: 0x0000A064 File Offset: 0x00008264
		private void SerializeSynchronizationTypeState(PhotonStream stream)
		{
			byte[] array = new byte[this.m_SynchronizeLayers.Count + this.m_SynchronizeParameters.Count];
			for (int i = 0; i < this.m_SynchronizeLayers.Count; i++)
			{
				array[i] = (byte)this.m_SynchronizeLayers[i].SynchronizeType;
			}
			for (int j = 0; j < this.m_SynchronizeParameters.Count; j++)
			{
				array[this.m_SynchronizeLayers.Count + j] = (byte)this.m_SynchronizeParameters[j].SynchronizeType;
			}
			stream.SendNext(array);
		}

		// Token: 0x0600018B RID: 395 RVA: 0x0000A0F8 File Offset: 0x000082F8
		private void DeserializeSynchronizationTypeState(PhotonStream stream)
		{
			byte[] array = (byte[])stream.ReceiveNext();
			for (int i = 0; i < this.m_SynchronizeLayers.Count; i++)
			{
				this.m_SynchronizeLayers[i].SynchronizeType = (PhotonAnimatorView.SynchronizeType)array[i];
			}
			for (int j = 0; j < this.m_SynchronizeParameters.Count; j++)
			{
				this.m_SynchronizeParameters[j].SynchronizeType = (PhotonAnimatorView.SynchronizeType)array[this.m_SynchronizeLayers.Count + j];
			}
		}

		// Token: 0x0600018C RID: 396 RVA: 0x0000A174 File Offset: 0x00008374
		public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
		{
			if (this.m_Animator == null)
			{
				return;
			}
			if (stream.IsWriting)
			{
				if (this.m_WasSynchronizeTypeChanged)
				{
					this.m_StreamQueue.Reset();
					this.SerializeSynchronizationTypeState(stream);
					this.m_WasSynchronizeTypeChanged = false;
				}
				this.m_StreamQueue.Serialize(stream);
				this.SerializeDataDiscretly(stream);
				return;
			}
			if (stream.PeekNext() is byte[])
			{
				this.DeserializeSynchronizationTypeState(stream);
			}
			this.m_StreamQueue.Deserialize(stream);
			this.DeserializeDataDiscretly(stream);
		}

		// Token: 0x0600018D RID: 397 RVA: 0x0000A1F4 File Offset: 0x000083F4
		public PhotonAnimatorView()
		{
		}

		// Token: 0x040000C4 RID: 196
		private bool TriggerUsageWarningDone;

		// Token: 0x040000C5 RID: 197
		private Animator m_Animator;

		// Token: 0x040000C6 RID: 198
		private PhotonStreamQueue m_StreamQueue = new PhotonStreamQueue(120);

		// Token: 0x040000C7 RID: 199
		[HideInInspector]
		[SerializeField]
		private bool ShowLayerWeightsInspector = true;

		// Token: 0x040000C8 RID: 200
		[HideInInspector]
		[SerializeField]
		private bool ShowParameterInspector = true;

		// Token: 0x040000C9 RID: 201
		[HideInInspector]
		[SerializeField]
		private List<PhotonAnimatorView.SynchronizedParameter> m_SynchronizeParameters = new List<PhotonAnimatorView.SynchronizedParameter>();

		// Token: 0x040000CA RID: 202
		[HideInInspector]
		[SerializeField]
		private List<PhotonAnimatorView.SynchronizedLayer> m_SynchronizeLayers = new List<PhotonAnimatorView.SynchronizedLayer>();

		// Token: 0x040000CB RID: 203
		private Vector3 m_ReceiverPosition;

		// Token: 0x040000CC RID: 204
		private float m_LastDeserializeTime;

		// Token: 0x040000CD RID: 205
		private bool m_WasSynchronizeTypeChanged = true;

		// Token: 0x040000CE RID: 206
		private List<string> m_raisedDiscreteTriggersCache = new List<string>();

		// Token: 0x02000031 RID: 49
		public enum ParameterType
		{
			// Token: 0x04000128 RID: 296
			Float = 1,
			// Token: 0x04000129 RID: 297
			Int = 3,
			// Token: 0x0400012A RID: 298
			Bool,
			// Token: 0x0400012B RID: 299
			Trigger = 9
		}

		// Token: 0x02000032 RID: 50
		public enum SynchronizeType
		{
			// Token: 0x0400012D RID: 301
			Disabled,
			// Token: 0x0400012E RID: 302
			Discrete,
			// Token: 0x0400012F RID: 303
			Continuous
		}

		// Token: 0x02000033 RID: 51
		[Serializable]
		public class SynchronizedParameter
		{
			// Token: 0x060001CC RID: 460 RVA: 0x0000B53C File Offset: 0x0000973C
			public SynchronizedParameter()
			{
			}

			// Token: 0x04000130 RID: 304
			public PhotonAnimatorView.ParameterType Type;

			// Token: 0x04000131 RID: 305
			public PhotonAnimatorView.SynchronizeType SynchronizeType;

			// Token: 0x04000132 RID: 306
			public string Name;
		}

		// Token: 0x02000034 RID: 52
		[Serializable]
		public class SynchronizedLayer
		{
			// Token: 0x060001CD RID: 461 RVA: 0x0000B544 File Offset: 0x00009744
			public SynchronizedLayer()
			{
			}

			// Token: 0x04000133 RID: 307
			public PhotonAnimatorView.SynchronizeType SynchronizeType;

			// Token: 0x04000134 RID: 308
			public int LayerIndex;
		}

		// Token: 0x02000035 RID: 53
		[CompilerGenerated]
		private sealed class <>c__DisplayClass18_0
		{
			// Token: 0x060001CE RID: 462 RVA: 0x0000B54C File Offset: 0x0000974C
			public <>c__DisplayClass18_0()
			{
			}

			// Token: 0x060001CF RID: 463 RVA: 0x0000B554 File Offset: 0x00009754
			internal bool <DoesLayerSynchronizeTypeExist>b__0(PhotonAnimatorView.SynchronizedLayer item)
			{
				return item.LayerIndex == this.layerIndex;
			}

			// Token: 0x04000135 RID: 309
			public int layerIndex;
		}

		// Token: 0x02000036 RID: 54
		[CompilerGenerated]
		private sealed class <>c__DisplayClass19_0
		{
			// Token: 0x060001D0 RID: 464 RVA: 0x0000B564 File Offset: 0x00009764
			public <>c__DisplayClass19_0()
			{
			}

			// Token: 0x060001D1 RID: 465 RVA: 0x0000B56C File Offset: 0x0000976C
			internal bool <DoesParameterSynchronizeTypeExist>b__0(PhotonAnimatorView.SynchronizedParameter item)
			{
				return item.Name == this.name;
			}

			// Token: 0x04000136 RID: 310
			public string name;
		}

		// Token: 0x02000037 RID: 55
		[CompilerGenerated]
		private sealed class <>c__DisplayClass22_0
		{
			// Token: 0x060001D2 RID: 466 RVA: 0x0000B57F File Offset: 0x0000977F
			public <>c__DisplayClass22_0()
			{
			}

			// Token: 0x060001D3 RID: 467 RVA: 0x0000B587 File Offset: 0x00009787
			internal bool <GetLayerSynchronizeType>b__0(PhotonAnimatorView.SynchronizedLayer item)
			{
				return item.LayerIndex == this.layerIndex;
			}

			// Token: 0x04000137 RID: 311
			public int layerIndex;
		}

		// Token: 0x02000038 RID: 56
		[CompilerGenerated]
		private sealed class <>c__DisplayClass23_0
		{
			// Token: 0x060001D4 RID: 468 RVA: 0x0000B597 File Offset: 0x00009797
			public <>c__DisplayClass23_0()
			{
			}

			// Token: 0x060001D5 RID: 469 RVA: 0x0000B59F File Offset: 0x0000979F
			internal bool <GetParameterSynchronizeType>b__0(PhotonAnimatorView.SynchronizedParameter item)
			{
				return item.Name == this.name;
			}

			// Token: 0x04000138 RID: 312
			public string name;
		}

		// Token: 0x02000039 RID: 57
		[CompilerGenerated]
		private sealed class <>c__DisplayClass24_0
		{
			// Token: 0x060001D6 RID: 470 RVA: 0x0000B5B2 File Offset: 0x000097B2
			public <>c__DisplayClass24_0()
			{
			}

			// Token: 0x060001D7 RID: 471 RVA: 0x0000B5BA File Offset: 0x000097BA
			internal bool <SetLayerSynchronized>b__0(PhotonAnimatorView.SynchronizedLayer item)
			{
				return item.LayerIndex == this.layerIndex;
			}

			// Token: 0x04000139 RID: 313
			public int layerIndex;
		}

		// Token: 0x0200003A RID: 58
		[CompilerGenerated]
		private sealed class <>c__DisplayClass25_0
		{
			// Token: 0x060001D8 RID: 472 RVA: 0x0000B5CA File Offset: 0x000097CA
			public <>c__DisplayClass25_0()
			{
			}

			// Token: 0x060001D9 RID: 473 RVA: 0x0000B5D2 File Offset: 0x000097D2
			internal bool <SetParameterSynchronized>b__0(PhotonAnimatorView.SynchronizedParameter item)
			{
				return item.Name == this.name;
			}

			// Token: 0x0400013A RID: 314
			public string name;
		}
	}
}
