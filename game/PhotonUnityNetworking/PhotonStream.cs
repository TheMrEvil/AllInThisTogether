using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Photon.Realtime;
using UnityEngine;

namespace Photon.Pun
{
	// Token: 0x0200001A RID: 26
	public class PhotonStream
	{
		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000141 RID: 321 RVA: 0x000088CB File Offset: 0x00006ACB
		// (set) Token: 0x06000142 RID: 322 RVA: 0x000088D3 File Offset: 0x00006AD3
		public bool IsWriting
		{
			[CompilerGenerated]
			get
			{
				return this.<IsWriting>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<IsWriting>k__BackingField = value;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000143 RID: 323 RVA: 0x000088DC File Offset: 0x00006ADC
		public bool IsReading
		{
			get
			{
				return !this.IsWriting;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000144 RID: 324 RVA: 0x000088E7 File Offset: 0x00006AE7
		public int Count
		{
			get
			{
				if (!this.IsWriting)
				{
					return this.readData.Length;
				}
				return this.writeData.Count;
			}
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00008905 File Offset: 0x00006B05
		public PhotonStream(bool write, object[] incomingData)
		{
			this.IsWriting = write;
			if (!write && incomingData != null)
			{
				this.readData = incomingData;
			}
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00008921 File Offset: 0x00006B21
		public void SetReadStream(object[] incomingData, int pos = 0)
		{
			this.readData = incomingData;
			this.currentItem = pos;
			this.IsWriting = false;
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00008938 File Offset: 0x00006B38
		internal void SetWriteStream(List<object> newWriteData, int pos = 0)
		{
			if (pos != newWriteData.Count)
			{
				throw new Exception("SetWriteStream failed, because count does not match position value. pos: " + pos.ToString() + " newWriteData.Count:" + newWriteData.Count.ToString());
			}
			this.writeData = newWriteData;
			this.currentItem = pos;
			this.IsWriting = true;
		}

		// Token: 0x06000148 RID: 328 RVA: 0x0000898D File Offset: 0x00006B8D
		internal List<object> GetWriteStream()
		{
			return this.writeData;
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00008995 File Offset: 0x00006B95
		[Obsolete("Either SET the writeData with an empty List or use Clear().")]
		internal void ResetWriteStream()
		{
			this.writeData.Clear();
		}

		// Token: 0x0600014A RID: 330 RVA: 0x000089A2 File Offset: 0x00006BA2
		public object ReceiveNext()
		{
			if (this.IsWriting)
			{
				Debug.LogError("Error: you cannot read this stream that you are writing!");
				return null;
			}
			object result = this.readData[this.currentItem];
			this.currentItem++;
			return result;
		}

		// Token: 0x0600014B RID: 331 RVA: 0x000089D3 File Offset: 0x00006BD3
		public object PeekNext()
		{
			if (this.IsWriting)
			{
				Debug.LogError("Error: you cannot read this stream that you are writing!");
				return null;
			}
			return this.readData[this.currentItem];
		}

		// Token: 0x0600014C RID: 332 RVA: 0x000089F6 File Offset: 0x00006BF6
		public void SendNext(object obj)
		{
			if (!this.IsWriting)
			{
				Debug.LogError("Error: you cannot write/send to this stream that you are reading!");
				return;
			}
			this.writeData.Add(obj);
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00008A17 File Offset: 0x00006C17
		[Obsolete("writeData is a list now. Use and re-use it directly.")]
		public bool CopyToListAndClear(List<object> target)
		{
			if (!this.IsWriting)
			{
				return false;
			}
			target.AddRange(this.writeData);
			this.writeData.Clear();
			return true;
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00008A3B File Offset: 0x00006C3B
		public object[] ToArray()
		{
			if (!this.IsWriting)
			{
				return this.readData;
			}
			return this.writeData.ToArray();
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00008A58 File Offset: 0x00006C58
		public void Serialize(ref bool myBool)
		{
			if (this.IsWriting)
			{
				this.writeData.Add(myBool);
				return;
			}
			if (this.readData.Length > this.currentItem)
			{
				myBool = (bool)this.readData[this.currentItem];
				this.currentItem++;
			}
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00008AB4 File Offset: 0x00006CB4
		public void Serialize(ref int myInt)
		{
			if (this.IsWriting)
			{
				this.writeData.Add(myInt);
				return;
			}
			if (this.readData.Length > this.currentItem)
			{
				myInt = (int)this.readData[this.currentItem];
				this.currentItem++;
			}
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00008B10 File Offset: 0x00006D10
		public void Serialize(ref string value)
		{
			if (this.IsWriting)
			{
				this.writeData.Add(value);
				return;
			}
			if (this.readData.Length > this.currentItem)
			{
				value = (string)this.readData[this.currentItem];
				this.currentItem++;
			}
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00008B68 File Offset: 0x00006D68
		public void Serialize(ref char value)
		{
			if (this.IsWriting)
			{
				this.writeData.Add((short)value);
				return;
			}
			if (this.readData.Length > this.currentItem)
			{
				value = (char)((short)this.readData[this.currentItem]);
				this.currentItem++;
			}
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00008BC4 File Offset: 0x00006DC4
		public void Serialize(ref byte value)
		{
			if (this.IsWriting)
			{
				this.writeData.Add(value);
				return;
			}
			if (this.readData.Length > this.currentItem)
			{
				value = (byte)this.readData[this.currentItem];
				this.currentItem++;
			}
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00008C20 File Offset: 0x00006E20
		public void Serialize(ref short value)
		{
			if (this.IsWriting)
			{
				this.writeData.Add(value);
				return;
			}
			if (this.readData.Length > this.currentItem)
			{
				value = (short)this.readData[this.currentItem];
				this.currentItem++;
			}
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00008C7C File Offset: 0x00006E7C
		public void Serialize(ref float obj)
		{
			if (this.IsWriting)
			{
				this.writeData.Add(obj);
				return;
			}
			if (this.readData.Length > this.currentItem)
			{
				obj = (float)this.readData[this.currentItem];
				this.currentItem++;
			}
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00008CD8 File Offset: 0x00006ED8
		public void Serialize(ref Player obj)
		{
			if (this.IsWriting)
			{
				this.writeData.Add(obj);
				return;
			}
			if (this.readData.Length > this.currentItem)
			{
				obj = (Player)this.readData[this.currentItem];
				this.currentItem++;
			}
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00008D30 File Offset: 0x00006F30
		public void Serialize(ref Vector3 obj)
		{
			if (this.IsWriting)
			{
				this.writeData.Add(obj);
				return;
			}
			if (this.readData.Length > this.currentItem)
			{
				obj = (Vector3)this.readData[this.currentItem];
				this.currentItem++;
			}
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00008D94 File Offset: 0x00006F94
		public void Serialize(ref Vector2 obj)
		{
			if (this.IsWriting)
			{
				this.writeData.Add(obj);
				return;
			}
			if (this.readData.Length > this.currentItem)
			{
				obj = (Vector2)this.readData[this.currentItem];
				this.currentItem++;
			}
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00008DF8 File Offset: 0x00006FF8
		public void Serialize(ref Quaternion obj)
		{
			if (this.IsWriting)
			{
				this.writeData.Add(obj);
				return;
			}
			if (this.readData.Length > this.currentItem)
			{
				obj = (Quaternion)this.readData[this.currentItem];
				this.currentItem++;
			}
		}

		// Token: 0x040000B4 RID: 180
		private List<object> writeData;

		// Token: 0x040000B5 RID: 181
		private object[] readData;

		// Token: 0x040000B6 RID: 182
		private int currentItem;

		// Token: 0x040000B7 RID: 183
		[CompilerGenerated]
		private bool <IsWriting>k__BackingField;
	}
}
