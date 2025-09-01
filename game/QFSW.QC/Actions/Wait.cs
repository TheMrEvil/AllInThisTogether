using System;
using UnityEngine;

namespace QFSW.QC.Actions
{
	// Token: 0x02000079 RID: 121
	public class Wait : ICommandAction
	{
		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600027E RID: 638 RVA: 0x0000A955 File Offset: 0x00008B55
		public bool IsFinished
		{
			get
			{
				return Time.time >= this._startTime + this._duration;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600027F RID: 639 RVA: 0x0000A96E File Offset: 0x00008B6E
		public bool StartsIdle
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000A971 File Offset: 0x00008B71
		public Wait(float seconds)
		{
			this._duration = seconds;
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000A980 File Offset: 0x00008B80
		public void Start(ActionContext ctx)
		{
			this._startTime = Time.time;
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000A98D File Offset: 0x00008B8D
		public void Finalize(ActionContext ctx)
		{
		}

		// Token: 0x04000162 RID: 354
		private float _startTime;

		// Token: 0x04000163 RID: 355
		private readonly float _duration;
	}
}
