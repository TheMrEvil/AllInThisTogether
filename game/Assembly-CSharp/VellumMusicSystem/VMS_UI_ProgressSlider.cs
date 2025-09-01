using System;
using UnityEngine;
using UnityEngine.UI;

namespace VellumMusicSystem
{
	// Token: 0x020003C0 RID: 960
	public class VMS_UI_ProgressSlider : MonoBehaviour
	{
		// Token: 0x06001FA4 RID: 8100 RVA: 0x000BC5DC File Offset: 0x000BA7DC
		private void Awake()
		{
			this.ResetSliders();
		}

		// Token: 0x06001FA5 RID: 8101 RVA: 0x000BC5E4 File Offset: 0x000BA7E4
		private void Update()
		{
			double dspTime = AudioSettings.dspTime;
			for (int i = 0; i < this.sliders.Length; i++)
			{
				if (dspTime > this.timeEvents[i].startTime)
				{
					if (dspTime < this.timeEvents[i].endTime)
					{
						double num = (dspTime - this.timeEvents[i].startTime) / (this.timeEvents[i].endTime - this.timeEvents[i].startTime);
						this.sliders[i].value = (float)num;
					}
					else
					{
						this.sliders[i].value = 0f;
					}
				}
			}
		}

		// Token: 0x06001FA6 RID: 8102 RVA: 0x000BC693 File Offset: 0x000BA893
		private void ConnectSliderToTimeEvent(VMS_TimeCalculationEvent timeEvent)
		{
			this.timeEvents[this.toggle] = timeEvent;
			this.toggle = 1 - this.toggle;
		}

		// Token: 0x06001FA7 RID: 8103 RVA: 0x000BC6B8 File Offset: 0x000BA8B8
		private void ResetSliders()
		{
			this.timeEvents[0].startTime = 999999999999.0;
			this.timeEvents[1].startTime = 999999999999.0;
			this.sliders[0].value = 0f;
			this.sliders[1].value = 0f;
		}

		// Token: 0x06001FA8 RID: 8104 RVA: 0x000BC71D File Offset: 0x000BA91D
		private void OnEnable()
		{
			VMS_Player.OnMusicPlayerStopping += this.ResetSliders;
			VMS_Player.OnTimeCalculated += this.ConnectSliderToTimeEvent;
		}

		// Token: 0x06001FA9 RID: 8105 RVA: 0x000BC741 File Offset: 0x000BA941
		private void OnDisable()
		{
			VMS_Player.OnMusicPlayerStopping -= this.ResetSliders;
			VMS_Player.OnTimeCalculated -= this.ConnectSliderToTimeEvent;
		}

		// Token: 0x06001FAA RID: 8106 RVA: 0x000BC765 File Offset: 0x000BA965
		public VMS_UI_ProgressSlider()
		{
		}

		// Token: 0x04001FD4 RID: 8148
		[SerializeField]
		private Slider[] sliders;

		// Token: 0x04001FD5 RID: 8149
		private VMS_TimeCalculationEvent[] timeEvents = new VMS_TimeCalculationEvent[2];

		// Token: 0x04001FD6 RID: 8150
		private int toggle;
	}
}
