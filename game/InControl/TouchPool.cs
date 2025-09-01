using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace InControl
{
	// Token: 0x02000059 RID: 89
	public class TouchPool
	{
		// Token: 0x06000459 RID: 1113 RVA: 0x0000FAAC File Offset: 0x0000DCAC
		public TouchPool(int capacity)
		{
			this.freeTouches = new List<Touch>(capacity);
			for (int i = 0; i < capacity; i++)
			{
				this.freeTouches.Add(new Touch());
			}
			this.usedTouches = new List<Touch>(capacity);
			this.Touches = new ReadOnlyCollection<Touch>(this.usedTouches);
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x0000FB04 File Offset: 0x0000DD04
		public TouchPool() : this(16)
		{
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x0000FB10 File Offset: 0x0000DD10
		public Touch FindOrCreateTouch(int fingerId)
		{
			int count = this.usedTouches.Count;
			Touch touch;
			for (int i = 0; i < count; i++)
			{
				touch = this.usedTouches[i];
				if (touch.fingerId == fingerId)
				{
					return touch;
				}
			}
			touch = this.NewTouch();
			touch.fingerId = fingerId;
			this.usedTouches.Add(touch);
			return touch;
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x0000FB68 File Offset: 0x0000DD68
		public Touch FindTouch(int fingerId)
		{
			int count = this.usedTouches.Count;
			for (int i = 0; i < count; i++)
			{
				Touch touch = this.usedTouches[i];
				if (touch.fingerId == fingerId)
				{
					return touch;
				}
			}
			return null;
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x0000FBA8 File Offset: 0x0000DDA8
		private Touch NewTouch()
		{
			int count = this.freeTouches.Count;
			if (count > 0)
			{
				Touch result = this.freeTouches[count - 1];
				this.freeTouches.RemoveAt(count - 1);
				return result;
			}
			return new Touch();
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x0000FBE7 File Offset: 0x0000DDE7
		public void FreeTouch(Touch touch)
		{
			touch.Reset();
			this.freeTouches.Add(touch);
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x0000FBFC File Offset: 0x0000DDFC
		public void FreeEndedTouches()
		{
			for (int i = this.usedTouches.Count - 1; i >= 0; i--)
			{
				if (this.usedTouches[i].phase == TouchPhase.Ended)
				{
					this.usedTouches.RemoveAt(i);
				}
			}
		}

		// Token: 0x040003D2 RID: 978
		public readonly ReadOnlyCollection<Touch> Touches;

		// Token: 0x040003D3 RID: 979
		private List<Touch> usedTouches;

		// Token: 0x040003D4 RID: 980
		private List<Touch> freeTouches;
	}
}
