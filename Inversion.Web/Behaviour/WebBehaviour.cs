﻿using System.Collections.Generic;

using Inversion.Process;
using Inversion.Process.Behaviour;

namespace Inversion.Web.Behaviour {
	/// <summary>
	/// An abstract provision of basic web-centric features for process behaviours
	/// being used in a web application.
	/// </summary>
	public abstract class WebBehaviour : ProcessBehaviour, IWebBehaviour {
		/// <summary>
		/// Creates a new instance of the behaviour.
		/// </summary>
		/// <param name="respondsTo">The name of the behaviour.</param>
		protected WebBehaviour(string respondsTo) : base(respondsTo) {}

		/// <summary>
		/// Determines if this behaviours action should be executed in
		/// response to the provided event.
		/// </summary>
		/// <param name="ev">The event to consider.</param>
		/// <returns>Returns true if this behaviours action to execute in response to this event; otherwise returns  false.</returns>
		public override bool Condition(IEvent ev) {
			return this.Condition(ev, (IWebContext)ev.Context);
		}

		/// <summary>
		/// Determines if this behaviours action should be executed in
		/// response to the provided event and context.
		/// </summary>
		/// <param name="ev">The event to consider.</param>
		/// <param name="context">The context to consider.</param>
		/// <returns></returns>
		public bool Condition(IEvent ev, IWebContext context) {
			return base.Condition(ev, context);
		}

		/// <summary>
		/// The action to perform if this behaviours condition is met.
		/// </summary>
		/// <param name="ev">The event to consult.</param>
		public override void Action(IEvent ev) {
			this.Action(ev, (WebContext)ev.Context);
		}

		/// <summary>
		/// The action to perform if this behaviours condition is met.
		/// </summary>
		/// <param name="ev">The event to consult.</param>
		/// <param name="context">The context upon which to perform any action.</param>
		public override void Action(IEvent ev, IProcessContext context) {
			this.Action(ev, (WebContext)context);
		}

		/// <summary>
		/// Implementors should impliment this behaviour with the desired action
		/// for their behaviour.
		/// </summary>
		/// <param name="ev">The event to consult.</param>
		/// <param name="context">The context upon which to perform any action.</param>
		public abstract void Action(IEvent ev, IWebContext context);

	}
}
