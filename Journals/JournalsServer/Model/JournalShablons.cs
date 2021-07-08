using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JournalsServer.Model
{
    public class JournalShablons
    {
        public ulong Id { get; set; }

        public string Name { get; set; }

        public virtual Department Department { get; set; }
    }
}
