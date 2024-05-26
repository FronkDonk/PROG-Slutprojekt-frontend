using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG_Slutprojekt_frontend.Constants
{
    public class AvatarGradient
    {
        public string Color1 { get; set; }
        public string Color2 { get; set; }

        public static AvatarGradient[] Gradients = new[]
        {
            new AvatarGradient { Color1 = "#7FB2FF", Color2 = "#FF7F7F" }, //blue to red
            new AvatarGradient { Color1 = "#7FFF7F", Color2 = "#FFFF7F" }, //green to yellow
            new AvatarGradient { Color1 = "#FFC0CB", Color2 = "#ADD8E6" }, //pink to blue
            new AvatarGradient { Color1 = "#90EE90", Color2 = "#ADD8E6" }, //lightgreen to lightblue
            new AvatarGradient { Color1 = "#000080", Color2 = "#90EE90" }, //navy to green
            new AvatarGradient { Color1 = "#7FFFFF", Color2 = "#FF7FFF" }, //cyan to magenta
            new AvatarGradient { Color1 = "#FFD700", Color2 = "#FF7F50" }, //gold to coral
            new AvatarGradient { Color1 = "#FF6347", Color2 = "#FFD700" }, //tomato to gold
            new AvatarGradient { Color1 = "#FFA500", Color2 = "#FF4500" }, //orange to orangered
            new AvatarGradient { Color1 = "#800080", Color2 = "#FF00FF" }, //purple to fuchsia
            new AvatarGradient { Color1 = "#FF0000", Color2 = "#00FFFF" }, //red to aqua
            new AvatarGradient { Color1 = "#008000", Color2 = "#FF00FF" }, //green to fuchsia
            new AvatarGradient { Color1 = "#FFA500", Color2 = "#FFFF00" }, //orange to yellow
            new AvatarGradient { Color1 = "#800080", Color2 = "#FFA500" }, //purple to orange
            new AvatarGradient { Color1 = "#FF6347", Color2 = "#FFD700" }  //tomato to gold
        };

    }
}
