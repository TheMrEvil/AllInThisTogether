using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020003FB RID: 1019
	public struct DepthState : IEquatable<DepthState>
	{
		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x060022A4 RID: 8868 RVA: 0x0003A264 File Offset: 0x00038464
		public static DepthState defaultValue
		{
			get
			{
				return new DepthState(true, CompareFunction.Less);
			}
		}

		// Token: 0x060022A5 RID: 8869 RVA: 0x0003A27D File Offset: 0x0003847D
		public DepthState(bool writeEnabled = true, CompareFunction compareFunction = CompareFunction.Less)
		{
			this.m_WriteEnabled = Convert.ToByte(writeEnabled);
			this.m_CompareFunction = (sbyte)compareFunction;
		}

		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x060022A6 RID: 8870 RVA: 0x0003A294 File Offset: 0x00038494
		// (set) Token: 0x060022A7 RID: 8871 RVA: 0x0003A2B1 File Offset: 0x000384B1
		public bool writeEnabled
		{
			get
			{
				return Convert.ToBoolean(this.m_WriteEnabled);
			}
			set
			{
				this.m_WriteEnabled = Convert.ToByte(value);
			}
		}

		// Token: 0x1700065D RID: 1629
		// (get) Token: 0x060022A8 RID: 8872 RVA: 0x0003A2C0 File Offset: 0x000384C0
		// (set) Token: 0x060022A9 RID: 8873 RVA: 0x0003A2D8 File Offset: 0x000384D8
		public CompareFunction compareFunction
		{
			get
			{
				return (CompareFunction)this.m_CompareFunction;
			}
			set
			{
				this.m_CompareFunction = (sbyte)value;
			}
		}

		// Token: 0x060022AA RID: 8874 RVA: 0x0003A2E4 File Offset: 0x000384E4
		public bool Equals(DepthState other)
		{
			return this.m_WriteEnabled == other.m_WriteEnabled && this.m_CompareFunction == other.m_CompareFunction;
		}

		// Token: 0x060022AB RID: 8875 RVA: 0x0003A318 File Offset: 0x00038518
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is DepthState && this.Equals((DepthState)obj);
		}

		// Token: 0x060022AC RID: 8876 RVA: 0x0003A350 File Offset: 0x00038550
		public override int GetHashCode()
		{
			return this.m_WriteEnabled.GetHashCode() * 397 ^ this.m_CompareFunction.GetHashCode();
		}

		// Token: 0x060022AD RID: 8877 RVA: 0x0003A380 File Offset: 0x00038580
		public static bool operator ==(DepthState left, DepthState right)
		{
			return left.Equals(right);
		}

		// Token: 0x060022AE RID: 8878 RVA: 0x0003A39C File Offset: 0x0003859C
		public static bool operator !=(DepthState left, DepthState right)
		{
			return !left.Equals(right);
		}

		// Token: 0x04000CD2 RID: 3282
		private byte m_WriteEnabled;

		// Token: 0x04000CD3 RID: 3283
		private sbyte m_CompareFunction;
	}
}
