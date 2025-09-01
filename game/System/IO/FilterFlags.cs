using System;

namespace System.IO
{
	// Token: 0x0200051C RID: 1308
	[Flags]
	internal enum FilterFlags : uint
	{
		// Token: 0x04001696 RID: 5782
		ReadPoll = 4096U,
		// Token: 0x04001697 RID: 5783
		ReadOutOfBand = 8192U,
		// Token: 0x04001698 RID: 5784
		ReadLowWaterMark = 1U,
		// Token: 0x04001699 RID: 5785
		WriteLowWaterMark = 1U,
		// Token: 0x0400169A RID: 5786
		NoteTrigger = 16777216U,
		// Token: 0x0400169B RID: 5787
		NoteFFNop = 0U,
		// Token: 0x0400169C RID: 5788
		NoteFFAnd = 1073741824U,
		// Token: 0x0400169D RID: 5789
		NoteFFOr = 2147483648U,
		// Token: 0x0400169E RID: 5790
		NoteFFCopy = 3221225472U,
		// Token: 0x0400169F RID: 5791
		NoteFFCtrlMask = 3221225472U,
		// Token: 0x040016A0 RID: 5792
		NoteFFlagsMask = 16777215U,
		// Token: 0x040016A1 RID: 5793
		VNodeDelete = 1U,
		// Token: 0x040016A2 RID: 5794
		VNodeWrite = 2U,
		// Token: 0x040016A3 RID: 5795
		VNodeExtend = 4U,
		// Token: 0x040016A4 RID: 5796
		VNodeAttrib = 8U,
		// Token: 0x040016A5 RID: 5797
		VNodeLink = 16U,
		// Token: 0x040016A6 RID: 5798
		VNodeRename = 32U,
		// Token: 0x040016A7 RID: 5799
		VNodeRevoke = 64U,
		// Token: 0x040016A8 RID: 5800
		VNodeNone = 128U,
		// Token: 0x040016A9 RID: 5801
		ProcExit = 2147483648U,
		// Token: 0x040016AA RID: 5802
		ProcFork = 1073741824U,
		// Token: 0x040016AB RID: 5803
		ProcExec = 536870912U,
		// Token: 0x040016AC RID: 5804
		ProcReap = 268435456U,
		// Token: 0x040016AD RID: 5805
		ProcSignal = 134217728U,
		// Token: 0x040016AE RID: 5806
		ProcExitStatus = 67108864U,
		// Token: 0x040016AF RID: 5807
		ProcResourceEnd = 33554432U,
		// Token: 0x040016B0 RID: 5808
		ProcAppactive = 8388608U,
		// Token: 0x040016B1 RID: 5809
		ProcAppBackground = 4194304U,
		// Token: 0x040016B2 RID: 5810
		ProcAppNonUI = 2097152U,
		// Token: 0x040016B3 RID: 5811
		ProcAppInactive = 1048576U,
		// Token: 0x040016B4 RID: 5812
		ProcAppAllStates = 15728640U,
		// Token: 0x040016B5 RID: 5813
		ProcPDataMask = 1048575U,
		// Token: 0x040016B6 RID: 5814
		ProcControlMask = 4293918720U,
		// Token: 0x040016B7 RID: 5815
		VMPressure = 2147483648U,
		// Token: 0x040016B8 RID: 5816
		VMPressureTerminate = 1073741824U,
		// Token: 0x040016B9 RID: 5817
		VMPressureSuddenTerminate = 536870912U,
		// Token: 0x040016BA RID: 5818
		VMError = 268435456U,
		// Token: 0x040016BB RID: 5819
		TimerSeconds = 1U,
		// Token: 0x040016BC RID: 5820
		TimerMicroSeconds = 2U,
		// Token: 0x040016BD RID: 5821
		TimerNanoSeconds = 4U,
		// Token: 0x040016BE RID: 5822
		TimerAbsolute = 8U
	}
}
