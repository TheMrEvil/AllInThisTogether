using System;

namespace UnityEngine.NVIDIA
{
	// Token: 0x02000012 RID: 18
	public class DLSSContext
	{
		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00002844 File Offset: 0x00000A44
		public ref readonly DLSSCommandInitializationData initData
		{
			get
			{
				return ref this.m_InitData.Value;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00002864 File Offset: 0x00000A64
		public ref DLSSCommandExecutionData executeData
		{
			get
			{
				return ref this.m_ExecData.Value;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00002884 File Offset: 0x00000A84
		internal unsafe uint featureSlot
		{
			get
			{
				DLSSCommandInitializationData dlsscommandInitializationData = *this.initData;
				return dlsscommandInitializationData.featureSlot;
			}
		}

		// Token: 0x06000060 RID: 96 RVA: 0x000028A9 File Offset: 0x00000AA9
		internal DLSSContext(DLSSCommandInitializationData initSettings, uint featureSlot)
		{
			this.m_InitData.Value = initSettings;
			this.m_InitData.Value.featureSlot = featureSlot;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x000028E8 File Offset: 0x00000AE8
		internal IntPtr GetInitCmdPtr()
		{
			return this.m_InitData.Ptr;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002908 File Offset: 0x00000B08
		internal IntPtr GetExecuteCmdPtr()
		{
			this.m_ExecData.Value.featureSlot = this.featureSlot;
			return this.m_ExecData.Ptr;
		}

		// Token: 0x0400004B RID: 75
		private NativeData<DLSSCommandInitializationData> m_InitData = new NativeData<DLSSCommandInitializationData>();

		// Token: 0x0400004C RID: 76
		private NativeData<DLSSCommandExecutionData> m_ExecData = new NativeData<DLSSCommandExecutionData>();
	}
}
