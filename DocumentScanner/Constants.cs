using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentScanner
{
    public static class Constants
    {
        public const int handleRadius = 5;
        public const int defaultBrightness = 0;
        public const int defaultContrast = 100;
        public const string brightnessLabel = "Brightness";
        public const string contrastLabel = "Contrast";
        public const int maxBrightness = 100;
        public const int maxContrast = 200;
        public const int lowerThreshold = 255 / 3;
        public const int upperThreshold = 255;

    }
}
