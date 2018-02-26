using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlexaRokuControl.Models.Config
{
    public class Vizio
    {
        public string TVUrl { get; set; }
        public string SoundBarUrl { get; set; }
        public string VizioAuth { get; set; }
    }

    public class APISettings
    {
        public string RokuUrl { get; set; }
        public Vizio Vizio { get; set; }
    }
}
