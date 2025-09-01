using System;

namespace ExitGames.Client.Photon
{
	// Token: 0x02000026 RID: 38
	public class EventData
	{
		// Token: 0x060001BA RID: 442 RVA: 0x0000D141 File Offset: 0x0000B341
		public EventData()
		{
			this.Parameters = new ParameterDictionary();
		}

		// Token: 0x17000087 RID: 135
		public object this[byte key]
		{
			get
			{
				object result;
				this.Parameters.TryGetValue(key, out result);
				return result;
			}
			internal set
			{
				this.Parameters.Add(key, value);
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060001BD RID: 445 RVA: 0x0000D1A8 File Offset: 0x0000B3A8
		// (set) Token: 0x060001BE RID: 446 RVA: 0x0000D1F0 File Offset: 0x0000B3F0
		public int Sender
		{
			get
			{
				bool flag = this.sender == -1;
				if (flag)
				{
					int num;
					this.sender = (this.Parameters.TryGetValue<int>(this.SenderKey, out num) ? num : -1);
				}
				return this.sender;
			}
			internal set
			{
				this.sender = value;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060001BF RID: 447 RVA: 0x0000D1FC File Offset: 0x0000B3FC
		// (set) Token: 0x060001C0 RID: 448 RVA: 0x0000D23B File Offset: 0x0000B43B
		public object CustomData
		{
			get
			{
				bool flag = this.customData == null;
				if (flag)
				{
					this.Parameters.TryGetValue(this.CustomDataKey, out this.customData);
				}
				return this.customData;
			}
			internal set
			{
				this.customData = value;
			}
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x0000D245 File Offset: 0x0000B445
		internal void Reset()
		{
			this.Code = 0;
			this.Parameters.Clear();
			this.sender = -1;
			this.customData = null;
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x0000D26C File Offset: 0x0000B46C
		public override string ToString()
		{
			return string.Format("Event {0}.", this.Code.ToString());
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x0000D294 File Offset: 0x0000B494
		public string ToStringFull()
		{
			return string.Format("Event {0}: {1}", this.Code, SupportClass.DictionaryToString(this.Parameters, true));
		}

		// Token: 0x0400016C RID: 364
		public byte Code;

		// Token: 0x0400016D RID: 365
		public readonly ParameterDictionary Parameters;

		// Token: 0x0400016E RID: 366
		public byte SenderKey = 254;

		// Token: 0x0400016F RID: 367
		private int sender = -1;

		// Token: 0x04000170 RID: 368
		public byte CustomDataKey = 245;

		// Token: 0x04000171 RID: 369
		private object customData;
	}
}
