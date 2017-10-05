// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace osu.Game.Rulesets
{
    public class RulesetInfo : IEquatable<RulesetInfo>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }

        public string Name { get; set; }

        public string InstantiationInfo { get; set; }

        public bool Available { get; set; }

        public virtual Ruleset CreateInstance() => (Ruleset)Activator.CreateInstance(Type.GetType(InstantiationInfo), this);

        public bool Equals(RulesetInfo other) => other != null && Id == other.Id && Available == other.Available && Name == other.Name && InstantiationInfo == other.InstantiationInfo;
    }
}
