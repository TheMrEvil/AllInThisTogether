using System;
using UnityEngine;

namespace QFSW.QC.Actions
{
	// Token: 0x0200007C RID: 124
	public class WaitRealtime : ICommandAction
	{
		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000285 RID: 645 RVA: 0x0000A9C8 File Offset: 0x00008BC8
		public bool IsFinished
		{
			get
			{
				return Time.realtimeSinceStartup >= this._startTime + this._duration;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000286 RID: 646 RVA: 0x0000A9E1 File Offset: 0x00008BE1
		public bool StartsIdle
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000A9E4 File Offset: 0x00008BE4
		public WaitRealtime(float seconds)
		{
			this._duration = seconds;
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000A9F3 File Offset: 0x00008BF3
		public void Start(ActionContext ctx)
		{
			this._startTime = Time.realtimeSinceStartup;
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0000AA00 File Offset: 0x00008C00
		public void Finalize(ActionContext ctx)
		{
		}

		// Token: 0x04000164 RID: 356
		private float _startTime;

		// Token: 0x04000165 RID: 357
		private readonly float _duration;
	}
}
