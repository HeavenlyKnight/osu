// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System;
using System.Collections.Generic;
using osu.Framework.Allocation;
using osu.Framework.Input.Bindings;
using osu.Game.Rulesets;

namespace osu.Game.Input.Bindings
{
    /// <summary>
    /// A KeyBindingInputManager with a database backing for custom overrides.
    /// </summary>
    /// <typeparam name="T">The type of the custom action.</typeparam>
    public abstract class DatabasedKeyBindingInputManager<T> : KeyBindingInputManager<T>
        where T : struct
    {
        private readonly RulesetInfo ruleset;

        private readonly int? variant;

        private KeyBindingStore store;

        public override IEnumerable<KeyBinding> DefaultKeyBindings => ruleset.CreateInstance().GetDefaultKeyBindings();

        /// <summary>
        /// Create a new instance.
        /// </summary>
        /// <param name="ruleset">A reference to identify the current <see cref="Ruleset"/>. Used to lookup mappings. Null for global mappings.</param>
        /// <param name="variant">An optional variant for the specified <see cref="Ruleset"/>. Used when a ruleset has more than one possible keyboard layouts.</param>
        /// <param name="simultaneousMode">Specify how to deal with multiple matches of <see cref="KeyCombination"/>s and <see cref="T"/>s.</param>
        protected DatabasedKeyBindingInputManager(RulesetInfo ruleset = null, int? variant = null, SimultaneousBindingMode simultaneousMode = SimultaneousBindingMode.None)
            : base(simultaneousMode)
        {
            this.ruleset = ruleset;
            this.variant = variant;

            if (ruleset != null && variant == null)
                throw new InvalidOperationException($"{nameof(variant)} can not be null when a non-null {nameof(ruleset)} is provided.");
        }

        [BackgroundDependencyLoader]
        private void load(KeyBindingStore keyBindings)
        {
            store = keyBindings;
        }

        protected override void ReloadMappings()
        {
            KeyBindings = store.Query(ruleset?.Id, variant);
        }
    }
}
