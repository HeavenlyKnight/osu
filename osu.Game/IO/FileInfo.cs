﻿// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

namespace osu.Game.IO
{
    public class FileInfo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Hash { get; set; }

        public string StoragePath => Path.Combine(Hash.Remove(1), Hash.Remove(2), Hash);

        public int ReferenceCount { get; set; }
    }
}
