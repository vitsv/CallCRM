namespace Asterisk.NET.Manager.Event
{
	/// <summary>
	/// Abstract base class for events triggered in response to an OriginateAction.
	/// </summary>
	/// <seealso cref="Manager.Action.OriginateAction" />
	public abstract class OriginateEvent : ResponseEvent
	{
		private string context;
		private string exten;
		private int reason;

		/// <summary>
		/// Get/Set the name of the context of the extension to connect to.
		/// </summary>
		public string Context
		{
			get { return this.context; }
			set { this.context = value; }
		}
		/// <summary>
		/// Get/Set the the extension to connect to.
		/// </summary>
		public string Exten
		{
			get { return this.exten; }
			set { this.exten = value; }
		}

		public int Reason
		{
			get { return this.reason; }
			set { this.reason = value; }
		}

		public OriginateEvent(object source)
			: base(source)
		{
		}
	}
}