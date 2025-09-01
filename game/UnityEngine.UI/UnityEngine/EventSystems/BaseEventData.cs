using System;

namespace UnityEngine.EventSystems
{
	// Token: 0x0200004E RID: 78
	public class BaseEventData : AbstractEventData
	{
		// Token: 0x0600052E RID: 1326 RVA: 0x00017D86 File Offset: 0x00015F86
		public BaseEventData(EventSystem eventSystem)
		{
			this.m_EventSystem = eventSystem;
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x0600052F RID: 1327 RVA: 0x00017D95 File Offset: 0x00015F95
		public BaseInputModule currentInputModule
		{
			get
			{
				return this.m_EventSystem.currentInputModule;
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000530 RID: 1328 RVA: 0x00017DA2 File Offset: 0x00015FA2
		// (set) Token: 0x06000531 RID: 1329 RVA: 0x00017DAF File Offset: 0x00015FAF
		public GameObject selectedObject
		{
			get
			{
				return this.m_EventSystem.currentSelectedGameObject;
			}
			set
			{
				this.m_EventSystem.SetSelectedGameObject(value, this);
			}
		}

		// Token: 0x040001AE RID: 430
		private readonly EventSystem m_EventSystem;
	}
}
