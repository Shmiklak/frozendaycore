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
    public class Flash : StoryboardObjectGenerator
    {
        public override void Generate()
        {
            var beat = (74318 - 74046) * 2;

		    var EndTime = (int)(Beatmap.HitObjects.LastOrDefault()?.EndTime ?? AudioDuration);

            int[] timeStamps = {26318, 11046, 13227, 15409, 17591, 19773, 21955, 25227, 30682, 35046, 39409, 42682, 43773, 48137, 51409, 52500, 56864, 61227, 63409, 64364, 64500, 65591, 69955, 71046, 72137, 73227, 74318, 91773, 96137, 100500, 102682, 104864, 107046, 115773, 120137, 124500, 125591, 126682, 127773, 126000, 172500, 176864, 181227, 183409, 184364, 184500, 185591, 189955, 191046, 192137, 192682, 193227, 193637, 194046,  194455, };
            foreach (var stamp in timeStamps)
            {
                var flash = GetLayer("Flashes").CreateSprite("sb/etc/p.png", OsbOrigin.Centre);
                flash.Scale(stamp, 1000);
                flash.Additive(stamp, stamp+beat);
                flash.Fade(stamp, stamp+beat, 1, 0);
            }
        }
    }
}
