﻿using System.Collections.Generic;
using System.Linq;

namespace PSXPackager.Common.Cue
{
    public class CueFile
    {
        public List<CueFileEntry> FileEntries { get; }

        public CueFile()
        {
            FileEntries = new List<CueFileEntry>();
        }

        public CueFile(IEnumerable<CueFileEntry> cueFileEntry)
        {
            FileEntries = cueFileEntry.ToList();
        }
    }

}