using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Mapset;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using StorybrewCommon.Storyboarding.Util;
using StorybrewCommon.Subtitles;
using StorybrewCommon.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StorybrewScripts
{
    public class Sb : StoryboardObjectGenerator
    {
        public override void Generate()
        {
		    var vig = GetLayer("vig").CreateSprite("sb/etc/vig.png", OsbOrigin.Centre);
            vig.Fade(-500, 137, 0, 1);
            vig.Scale(-500, 0.63);
            vig.Fade(195409, 0);

            var grey = GetLayer("background").CreateSprite("sb/bg/blur.jpg", OsbOrigin.Centre);
            grey.Fade(-500, 137, 0, 1);
            grey.Scale(-500, 8864, 0.45, 0.52);
            grey.Fade(8864, 0);

            var grey2 = GetLayer("background").CreateSprite("sb/bg/blur.jpg", OsbOrigin.Centre);
            grey2.Fade(83046, 83591, 0, 1);
            grey2.Scale(83046, 91773, 0.45, 0.52);
            grey2.Fade(91773, 0);

            var ice = GetLayer("ice").CreateSprite("sb/etc/ice.png", OsbOrigin.Centre);
            ice.Scale(128864, 0.88);
            ice.Additive(128864, 172500);
            ice.Fade(128864, 129955, 0, 1);
        }
    }
}
