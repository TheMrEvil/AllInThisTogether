using System;
using System.Text;

namespace System.Data.SqlClient
{
	// Token: 0x020001E3 RID: 483
	internal class SqlConnectionTimeoutErrorInternal
	{
		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x0600177D RID: 6013 RVA: 0x0006A24F File Offset: 0x0006844F
		internal SqlConnectionTimeoutErrorPhase CurrentPhase
		{
			get
			{
				return this._currentPhase;
			}
		}

		// Token: 0x0600177E RID: 6014 RVA: 0x0006A258 File Offset: 0x00068458
		public SqlConnectionTimeoutErrorInternal()
		{
			this._phaseDurations = new SqlConnectionTimeoutPhaseDuration[9];
			for (int i = 0; i < this._phaseDurations.Length; i++)
			{
				this._phaseDurations[i] = null;
			}
		}

		// Token: 0x0600177F RID: 6015 RVA: 0x0006A294 File Offset: 0x00068494
		public void SetFailoverScenario(bool useFailoverServer)
		{
			this._isFailoverScenario = useFailoverServer;
		}

		// Token: 0x06001780 RID: 6016 RVA: 0x0006A29D File Offset: 0x0006849D
		public void SetInternalSourceType(SqlConnectionInternalSourceType sourceType)
		{
			this._currentSourceType = sourceType;
			if (this._currentSourceType == SqlConnectionInternalSourceType.RoutingDestination)
			{
				this._originalPhaseDurations = this._phaseDurations;
				this._phaseDurations = new SqlConnectionTimeoutPhaseDuration[9];
				this.SetAndBeginPhase(SqlConnectionTimeoutErrorPhase.PreLoginBegin);
			}
		}

		// Token: 0x06001781 RID: 6017 RVA: 0x0006A2D0 File Offset: 0x000684D0
		internal void ResetAndRestartPhase()
		{
			this._currentPhase = SqlConnectionTimeoutErrorPhase.PreLoginBegin;
			for (int i = 0; i < this._phaseDurations.Length; i++)
			{
				this._phaseDurations[i] = null;
			}
		}

		// Token: 0x06001782 RID: 6018 RVA: 0x0006A300 File Offset: 0x00068500
		internal void SetAndBeginPhase(SqlConnectionTimeoutErrorPhase timeoutErrorPhase)
		{
			this._currentPhase = timeoutErrorPhase;
			if (this._phaseDurations[(int)timeoutErrorPhase] == null)
			{
				this._phaseDurations[(int)timeoutErrorPhase] = new SqlConnectionTimeoutPhaseDuration();
			}
			this._phaseDurations[(int)timeoutErrorPhase].StartCapture();
		}

		// Token: 0x06001783 RID: 6019 RVA: 0x0006A32D File Offset: 0x0006852D
		internal void EndPhase(SqlConnectionTimeoutErrorPhase timeoutErrorPhase)
		{
			this._phaseDurations[(int)timeoutErrorPhase].StopCapture();
		}

		// Token: 0x06001784 RID: 6020 RVA: 0x0006A33C File Offset: 0x0006853C
		internal void SetAllCompleteMarker()
		{
			this._currentPhase = SqlConnectionTimeoutErrorPhase.Complete;
		}

		// Token: 0x06001785 RID: 6021 RVA: 0x0006A348 File Offset: 0x00068548
		internal string GetErrorMessage()
		{
			StringBuilder stringBuilder;
			string text;
			switch (this._currentPhase)
			{
			case SqlConnectionTimeoutErrorPhase.PreLoginBegin:
				stringBuilder = new StringBuilder(SQLMessage.Timeout_PreLogin_Begin());
				text = SQLMessage.Duration_PreLogin_Begin(this._phaseDurations[1].GetMilliSecondDuration());
				break;
			case SqlConnectionTimeoutErrorPhase.InitializeConnection:
				stringBuilder = new StringBuilder(SQLMessage.Timeout_PreLogin_InitializeConnection());
				text = SQLMessage.Duration_PreLogin_Begin(this._phaseDurations[1].GetMilliSecondDuration() + this._phaseDurations[2].GetMilliSecondDuration());
				break;
			case SqlConnectionTimeoutErrorPhase.SendPreLoginHandshake:
				stringBuilder = new StringBuilder(SQLMessage.Timeout_PreLogin_SendHandshake());
				text = SQLMessage.Duration_PreLoginHandshake(this._phaseDurations[1].GetMilliSecondDuration() + this._phaseDurations[2].GetMilliSecondDuration(), this._phaseDurations[3].GetMilliSecondDuration());
				break;
			case SqlConnectionTimeoutErrorPhase.ConsumePreLoginHandshake:
				stringBuilder = new StringBuilder(SQLMessage.Timeout_PreLogin_ConsumeHandshake());
				text = SQLMessage.Duration_PreLoginHandshake(this._phaseDurations[1].GetMilliSecondDuration() + this._phaseDurations[2].GetMilliSecondDuration(), this._phaseDurations[3].GetMilliSecondDuration() + this._phaseDurations[4].GetMilliSecondDuration());
				break;
			case SqlConnectionTimeoutErrorPhase.LoginBegin:
				stringBuilder = new StringBuilder(SQLMessage.Timeout_Login_Begin());
				text = SQLMessage.Duration_Login_Begin(this._phaseDurations[1].GetMilliSecondDuration() + this._phaseDurations[2].GetMilliSecondDuration(), this._phaseDurations[3].GetMilliSecondDuration() + this._phaseDurations[4].GetMilliSecondDuration(), this._phaseDurations[5].GetMilliSecondDuration());
				break;
			case SqlConnectionTimeoutErrorPhase.ProcessConnectionAuth:
				stringBuilder = new StringBuilder(SQLMessage.Timeout_Login_ProcessConnectionAuth());
				text = SQLMessage.Duration_Login_ProcessConnectionAuth(this._phaseDurations[1].GetMilliSecondDuration() + this._phaseDurations[2].GetMilliSecondDuration(), this._phaseDurations[3].GetMilliSecondDuration() + this._phaseDurations[4].GetMilliSecondDuration(), this._phaseDurations[5].GetMilliSecondDuration(), this._phaseDurations[6].GetMilliSecondDuration());
				break;
			case SqlConnectionTimeoutErrorPhase.PostLogin:
				stringBuilder = new StringBuilder(SQLMessage.Timeout_PostLogin());
				text = SQLMessage.Duration_PostLogin(this._phaseDurations[1].GetMilliSecondDuration() + this._phaseDurations[2].GetMilliSecondDuration(), this._phaseDurations[3].GetMilliSecondDuration() + this._phaseDurations[4].GetMilliSecondDuration(), this._phaseDurations[5].GetMilliSecondDuration(), this._phaseDurations[6].GetMilliSecondDuration(), this._phaseDurations[7].GetMilliSecondDuration());
				break;
			default:
				stringBuilder = new StringBuilder(SQLMessage.Timeout());
				text = null;
				break;
			}
			if (this._currentPhase != SqlConnectionTimeoutErrorPhase.Undefined && this._currentPhase != SqlConnectionTimeoutErrorPhase.Complete)
			{
				if (this._isFailoverScenario)
				{
					stringBuilder.Append("  ");
					stringBuilder.AppendFormat(null, SQLMessage.Timeout_FailoverInfo(), this._currentSourceType);
				}
				else if (this._currentSourceType == SqlConnectionInternalSourceType.RoutingDestination)
				{
					stringBuilder.Append("  ");
					stringBuilder.AppendFormat(null, SQLMessage.Timeout_RoutingDestination(), new object[]
					{
						this._originalPhaseDurations[1].GetMilliSecondDuration() + this._originalPhaseDurations[2].GetMilliSecondDuration(),
						this._originalPhaseDurations[3].GetMilliSecondDuration() + this._originalPhaseDurations[4].GetMilliSecondDuration(),
						this._originalPhaseDurations[5].GetMilliSecondDuration(),
						this._originalPhaseDurations[6].GetMilliSecondDuration(),
						this._originalPhaseDurations[7].GetMilliSecondDuration()
					});
				}
			}
			if (text != null)
			{
				stringBuilder.Append("  ");
				stringBuilder.Append(text);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000F27 RID: 3879
		private SqlConnectionTimeoutPhaseDuration[] _phaseDurations;

		// Token: 0x04000F28 RID: 3880
		private SqlConnectionTimeoutPhaseDuration[] _originalPhaseDurations;

		// Token: 0x04000F29 RID: 3881
		private SqlConnectionTimeoutErrorPhase _currentPhase;

		// Token: 0x04000F2A RID: 3882
		private SqlConnectionInternalSourceType _currentSourceType;

		// Token: 0x04000F2B RID: 3883
		private bool _isFailoverScenario;
	}
}
