﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inversion.Extensions;

namespace Inversion.Process.Behaviour {
	/// <summary>
	/// A behaviour concerned with driving the processing of a
	/// sequence of messages.
	/// </summary>
	public class ParameterisedSequenceBehaviour : ApplicationBehaviour {
		/// <summary>
		/// Creates a new instance of the behaviour.
		/// </summary>
		/// <param name="message">The name of the behaviour.</param>
		/// <param name="namedLists">Named lists used to configure this behaviour.</param>
		/// <param name="namedMaps">Named maps used to configure this behaviour.</param>
		/// <param name="namedMappedLists">Named maps of lists used to configure this behaviour.</param>
		public ParameterisedSequenceBehaviour(string message, IDictionary<string, IEnumerable<string>> namedLists,
			IDictionary<string, IDictionary<string, string>> namedMaps,
			IDictionary<string, IDictionary<string, IEnumerable<string>>> namedMappedLists)
			: base(message, namedLists, namedMaps, namedMappedLists) {}

		/// <summary>
		/// The action to perform when the `Condition(IEvent)` is met.
		/// </summary>
		/// <param name="ev">The event to consult.</param>
		/// <param name="context">The context upon which to perform any action.</param>
		public override void Action(IEvent ev, ProcessContext context) {
			foreach (string seqRef in this.NamedLists["sequence"]) {
				IDictionary<string, string> map = this.NamedMaps[seqRef];
				string message = seqRef.TrimLeftBy(4);
				IEvent next = new Event(context, message, map);
				context.ControlState[next.Message] = next;
				next.Fire();
			}
		}
	}
}
