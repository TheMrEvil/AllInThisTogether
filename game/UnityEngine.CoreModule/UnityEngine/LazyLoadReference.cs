using System;

namespace UnityEngine
{
	// Token: 0x0200020D RID: 525
	[Serializable]
	public struct LazyLoadReference<T> where T : Object
	{
		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x06001720 RID: 5920 RVA: 0x000252D3 File Offset: 0x000234D3
		public bool isSet
		{
			get
			{
				return this.m_InstanceID != 0;
			}
		}

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x06001721 RID: 5921 RVA: 0x000252DE File Offset: 0x000234DE
		public bool isBroken
		{
			get
			{
				return this.m_InstanceID != 0 && !Object.DoesObjectWithInstanceIDExist(this.m_InstanceID);
			}
		}

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x06001722 RID: 5922 RVA: 0x000252FC File Offset: 0x000234FC
		// (set) Token: 0x06001723 RID: 5923 RVA: 0x0002533C File Offset: 0x0002353C
		public T asset
		{
			get
			{
				bool flag = this.m_InstanceID == 0;
				T result;
				if (flag)
				{
					result = default(T);
				}
				else
				{
					result = (T)((object)Object.ForceLoadFromInstanceID(this.m_InstanceID));
				}
				return result;
			}
			set
			{
				bool flag = value == null;
				if (flag)
				{
					this.m_InstanceID = 0;
				}
				else
				{
					bool flag2 = !Object.IsPersistent(value);
					if (flag2)
					{
						throw new ArgumentException("Object that does not belong to a persisted asset cannot be set as the target of a LazyLoadReference.");
					}
					this.m_InstanceID = value.GetInstanceID();
				}
			}
		}

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x06001724 RID: 5924 RVA: 0x00025396 File Offset: 0x00023596
		// (set) Token: 0x06001725 RID: 5925 RVA: 0x0002539E File Offset: 0x0002359E
		public int instanceID
		{
			get
			{
				return this.m_InstanceID;
			}
			set
			{
				this.m_InstanceID = value;
			}
		}

		// Token: 0x06001726 RID: 5926 RVA: 0x000253A8 File Offset: 0x000235A8
		public LazyLoadReference(T asset)
		{
			bool flag = asset == null;
			if (flag)
			{
				this.m_InstanceID = 0;
			}
			else
			{
				bool flag2 = !Object.IsPersistent(asset);
				if (flag2)
				{
					throw new ArgumentException("Object that does not belong to a persisted asset cannot be set as the target of a LazyLoadReference.");
				}
				this.m_InstanceID = asset.GetInstanceID();
			}
		}

		// Token: 0x06001727 RID: 5927 RVA: 0x00025402 File Offset: 0x00023602
		public LazyLoadReference(int instanceID)
		{
			this.m_InstanceID = instanceID;
		}

		// Token: 0x06001728 RID: 5928 RVA: 0x0002540C File Offset: 0x0002360C
		public static implicit operator LazyLoadReference<T>(T asset)
		{
			return new LazyLoadReference<T>
			{
				asset = asset
			};
		}

		// Token: 0x06001729 RID: 5929 RVA: 0x00025430 File Offset: 0x00023630
		public static implicit operator LazyLoadReference<T>(int instanceID)
		{
			return new LazyLoadReference<T>
			{
				instanceID = instanceID
			};
		}

		// Token: 0x040007FA RID: 2042
		private const int kInstanceID_None = 0;

		// Token: 0x040007FB RID: 2043
		[SerializeField]
		private int m_InstanceID;
	}
}
