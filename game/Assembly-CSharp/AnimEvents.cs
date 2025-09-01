using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000076 RID: 118
public class AnimEvents : MonoBehaviour
{
	// Token: 0x06000480 RID: 1152 RVA: 0x00022334 File Offset: 0x00020534
	private void Awake()
	{
		this.entityAudio = base.GetComponentInParent<EntityAudio>();
	}

	// Token: 0x06000481 RID: 1153 RVA: 0x00022342 File Offset: 0x00020542
	public void Footstep()
	{
		EntityAudio entityAudio = this.entityAudio;
		if (entityAudio == null)
		{
			return;
		}
		entityAudio.PlayFootstep();
	}

	// Token: 0x06000482 RID: 1154 RVA: 0x00022354 File Offset: 0x00020554
	public void PlayExtra()
	{
		if (this.entityAudio != null)
		{
			AIAudio aiaudio = this.entityAudio as AIAudio;
			if (aiaudio != null)
			{
				aiaudio.PlayExtraEffect();
			}
		}
	}

	// Token: 0x06000483 RID: 1155 RVA: 0x00022384 File Offset: 0x00020584
	public void TriggerLocalEvent(string eventName)
	{
		foreach (AnimEvents.AnimEvent animEvent in this.Events)
		{
			if (!(animEvent.ID != eventName))
			{
				animEvent.Event.Invoke();
				break;
			}
		}
	}

	// Token: 0x06000484 RID: 1156 RVA: 0x000223EC File Offset: 0x000205EC
	public AnimEvents()
	{
	}

	// Token: 0x040003BE RID: 958
	private EntityAudio entityAudio;

	// Token: 0x040003BF RID: 959
	public List<AnimEvents.AnimEvent> Events = new List<AnimEvents.AnimEvent>();

	// Token: 0x02000494 RID: 1172
	[Serializable]
	public class AnimEvent
	{
		// Token: 0x060021F9 RID: 8697 RVA: 0x000C4A99 File Offset: 0x000C2C99
		public AnimEvent()
		{
		}

		// Token: 0x0400233E RID: 9022
		public string ID;

		// Token: 0x0400233F RID: 9023
		public UnityEvent Event;
	}
}
