using System;
using System.Diagnostics;

namespace MagicaCloth2
{
	// Token: 0x020000F6 RID: 246
	public struct ResultCode
	{
		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000478 RID: 1144 RVA: 0x000227C9 File Offset: 0x000209C9
		public Define.Result Result
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000479 RID: 1145 RVA: 0x000227D3 File Offset: 0x000209D3
		public static ResultCode None
		{
			get
			{
				return new ResultCode(Define.Result.None);
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600047A RID: 1146 RVA: 0x000227DB File Offset: 0x000209DB
		public static ResultCode Empty
		{
			get
			{
				return new ResultCode(Define.Result.Empty);
			}
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x000227E3 File Offset: 0x000209E3
		public ResultCode(Define.Result initResult)
		{
			this.result = initResult;
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x000227EE File Offset: 0x000209EE
		public void Clear()
		{
			this.result = Define.Result.None;
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x000227E3 File Offset: 0x000209E3
		public void SetResult(Define.Result code)
		{
			this.result = code;
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x000227F9 File Offset: 0x000209F9
		public void SetSuccess()
		{
			this.SetResult(Define.Result.Success);
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x00022802 File Offset: 0x00020A02
		public void SetCancel()
		{
			this.SetResult(Define.Result.Cancel);
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x000227E3 File Offset: 0x000209E3
		public void SetError(Define.Result code = Define.Result.Error)
		{
			this.result = code;
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x0002280B File Offset: 0x00020A0B
		public void Set(ResultCode src)
		{
			this.result = src.result;
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x0002281D File Offset: 0x00020A1D
		public void SetProcess()
		{
			this.SetResult(Define.Result.Process);
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x00022826 File Offset: 0x00020A26
		public bool IsResult(Define.Result code)
		{
			return this.result == code;
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x00022833 File Offset: 0x00020A33
		public bool IsNone()
		{
			return this.result == Define.Result.None;
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x00022840 File Offset: 0x00020A40
		public bool IsSuccess()
		{
			return this.result == Define.Result.Success;
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x0002284D File Offset: 0x00020A4D
		public bool IsFaild()
		{
			return !this.IsSuccess();
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x00022858 File Offset: 0x00020A58
		public bool IsCancel()
		{
			return this.result == Define.Result.Cancel;
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x00022865 File Offset: 0x00020A65
		public bool IsNormal()
		{
			return this.result < Define.Result.Warning;
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x00022876 File Offset: 0x00020A76
		public bool IsWarning()
		{
			return this.result >= Define.Result.Warning && this.result < Define.Result.Error;
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x00022898 File Offset: 0x00020A98
		public bool IsError()
		{
			return this.result >= Define.Result.Error;
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x000228AC File Offset: 0x00020AAC
		public bool IsProcess()
		{
			return this.result == Define.Result.Process;
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x000228B9 File Offset: 0x00020AB9
		public string GetResultString()
		{
			if (this.IsNormal())
			{
				return this.result.ToString();
			}
			return string.Format("({0}) {1}", (int)this.result, this.result);
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x000228FC File Offset: 0x00020AFC
		public string GetResultInformation()
		{
			Define.Result result = this.result;
			if (result == Define.Result.RenderSetup_Unreadable)
			{
				return "It is necessary to turn on [Read/Write] in the model import settings.";
			}
			if (result != Define.Result.RenderSetup_Over65535vertices)
			{
				return null;
			}
			return "Original mesh must have no more than 65,535 vertices";
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x00022931 File Offset: 0x00020B31
		[Conditional("MC2_DEBUG")]
		public void DebugLog(bool error = true, bool warning = true, bool normal = true)
		{
			Define.Result result = this.result;
			if ((this.IsError() && error) || (!this.IsWarning() || !warning))
			{
			}
		}

		// Token: 0x0400064A RID: 1610
		private volatile Define.Result result;
	}
}
