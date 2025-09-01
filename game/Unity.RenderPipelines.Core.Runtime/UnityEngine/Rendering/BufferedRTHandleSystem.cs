using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering
{
	// Token: 0x0200008C RID: 140
	public class BufferedRTHandleSystem : IDisposable
	{
		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000415 RID: 1045 RVA: 0x00014706 File Offset: 0x00012906
		public int maxWidth
		{
			get
			{
				return this.m_RTHandleSystem.GetMaxWidth();
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000416 RID: 1046 RVA: 0x00014713 File Offset: 0x00012913
		public int maxHeight
		{
			get
			{
				return this.m_RTHandleSystem.GetMaxHeight();
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000417 RID: 1047 RVA: 0x00014720 File Offset: 0x00012920
		public RTHandleProperties rtHandleProperties
		{
			get
			{
				return this.m_RTHandleSystem.rtHandleProperties;
			}
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x0001472D File Offset: 0x0001292D
		public RTHandle GetFrameRT(int bufferId, int frameIndex)
		{
			if (!this.m_RTHandles.ContainsKey(bufferId))
			{
				return null;
			}
			return this.m_RTHandles[bufferId][frameIndex];
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x00014750 File Offset: 0x00012950
		public void AllocBuffer(int bufferId, Func<RTHandleSystem, int, RTHandle> allocator, int bufferCount)
		{
			RTHandle[] array = new RTHandle[bufferCount];
			this.m_RTHandles.Add(bufferId, array);
			array[0] = allocator(this.m_RTHandleSystem, 0);
			int i = 1;
			int num = array.Length;
			while (i < num)
			{
				array[i] = allocator(this.m_RTHandleSystem, i);
				this.m_RTHandleSystem.SwitchResizeMode(array[i], RTHandleSystem.ResizeMode.OnDemand);
				i++;
			}
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x000147B0 File Offset: 0x000129B0
		public void ReleaseBuffer(int bufferId)
		{
			RTHandle[] array;
			if (this.m_RTHandles.TryGetValue(bufferId, out array))
			{
				foreach (RTHandle rth in array)
				{
					this.m_RTHandleSystem.Release(rth);
				}
			}
			this.m_RTHandles.Remove(bufferId);
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x000147FA File Offset: 0x000129FA
		public void SwapAndSetReferenceSize(int width, int height)
		{
			this.Swap();
			this.m_RTHandleSystem.SetReferenceSize(width, height);
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x0001480F File Offset: 0x00012A0F
		public void ResetReferenceSize(int width, int height)
		{
			this.m_RTHandleSystem.ResetReferenceSize(width, height);
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x0001481E File Offset: 0x00012A1E
		public int GetNumFramesAllocated(int bufferId)
		{
			if (!this.m_RTHandles.ContainsKey(bufferId))
			{
				return 0;
			}
			return this.m_RTHandles[bufferId].Length;
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x00014840 File Offset: 0x00012A40
		public Vector2 CalculateRatioAgainstMaxSize(int width, int height)
		{
			RTHandleSystem rthandleSystem = this.m_RTHandleSystem;
			Vector2Int vector2Int = new Vector2Int(width, height);
			return rthandleSystem.CalculateRatioAgainstMaxSize(vector2Int);
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x00014864 File Offset: 0x00012A64
		private void Swap()
		{
			foreach (KeyValuePair<int, RTHandle[]> keyValuePair in this.m_RTHandles)
			{
				if (keyValuePair.Value.Length > 1)
				{
					RTHandle rthandle = keyValuePair.Value[keyValuePair.Value.Length - 1];
					int i = 0;
					int num = keyValuePair.Value.Length - 1;
					while (i < num)
					{
						keyValuePair.Value[i + 1] = keyValuePair.Value[i];
						i++;
					}
					keyValuePair.Value[0] = rthandle;
					this.m_RTHandleSystem.SwitchResizeMode(keyValuePair.Value[0], RTHandleSystem.ResizeMode.Auto);
					this.m_RTHandleSystem.SwitchResizeMode(keyValuePair.Value[1], RTHandleSystem.ResizeMode.OnDemand);
				}
				else
				{
					this.m_RTHandleSystem.SwitchResizeMode(keyValuePair.Value[0], RTHandleSystem.ResizeMode.Auto);
				}
			}
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x00014950 File Offset: 0x00012B50
		private void Dispose(bool disposing)
		{
			if (!this.m_DisposedValue)
			{
				if (disposing)
				{
					this.ReleaseAll();
					this.m_RTHandleSystem.Dispose();
					this.m_RTHandleSystem = null;
				}
				this.m_DisposedValue = true;
			}
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x0001497C File Offset: 0x00012B7C
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x00014988 File Offset: 0x00012B88
		public void ReleaseAll()
		{
			foreach (KeyValuePair<int, RTHandle[]> keyValuePair in this.m_RTHandles)
			{
				int i = 0;
				int num = keyValuePair.Value.Length;
				while (i < num)
				{
					this.m_RTHandleSystem.Release(keyValuePair.Value[i]);
					i++;
				}
			}
			this.m_RTHandles.Clear();
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x00014A0C File Offset: 0x00012C0C
		public BufferedRTHandleSystem()
		{
		}

		// Token: 0x040002E4 RID: 740
		private Dictionary<int, RTHandle[]> m_RTHandles = new Dictionary<int, RTHandle[]>();

		// Token: 0x040002E5 RID: 741
		private RTHandleSystem m_RTHandleSystem = new RTHandleSystem();

		// Token: 0x040002E6 RID: 742
		private bool m_DisposedValue;
	}
}
