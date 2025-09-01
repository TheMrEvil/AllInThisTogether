using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200002A RID: 42
	public struct EventDispatcherGate : IDisposable, IEquatable<EventDispatcherGate>
	{
		// Token: 0x060000FC RID: 252 RVA: 0x0000550C File Offset: 0x0000370C
		public EventDispatcherGate(EventDispatcher d)
		{
			bool flag = d == null;
			if (flag)
			{
				throw new ArgumentNullException("d");
			}
			this.m_Dispatcher = d;
			this.m_Dispatcher.CloseGate();
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00005541 File Offset: 0x00003741
		public void Dispose()
		{
			this.m_Dispatcher.OpenGate();
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00005550 File Offset: 0x00003750
		public bool Equals(EventDispatcherGate other)
		{
			return object.Equals(this.m_Dispatcher, other.m_Dispatcher);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00005574 File Offset: 0x00003774
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is EventDispatcherGate && this.Equals((EventDispatcherGate)obj);
		}

		// Token: 0x06000100 RID: 256 RVA: 0x000055AC File Offset: 0x000037AC
		public override int GetHashCode()
		{
			return (this.m_Dispatcher != null) ? this.m_Dispatcher.GetHashCode() : 0;
		}

		// Token: 0x06000101 RID: 257 RVA: 0x000055D4 File Offset: 0x000037D4
		public static bool operator ==(EventDispatcherGate left, EventDispatcherGate right)
		{
			return left.Equals(right);
		}

		// Token: 0x06000102 RID: 258 RVA: 0x000055F0 File Offset: 0x000037F0
		public static bool operator !=(EventDispatcherGate left, EventDispatcherGate right)
		{
			return !left.Equals(right);
		}

		// Token: 0x04000070 RID: 112
		private readonly EventDispatcher m_Dispatcher;
	}
}
