using StorybrewCommon.Mapset;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;

using OpenTK;
using OpenTK.Graphics;

using StorybrewCommon.Util;
using System;
using System.Drawing;
using System.Linq;


namespace StorybrewScripts
{
    public class Click : StoryboardObjectGenerator
    {
        [Configurable]
        public int StartTime = 0;

        [Configurable]
        public int EndTime = 0;

        [Configurable]
        public int BeatDivisor = 8;

        [Configurable]
        public int FadeTime = 200;

        [Configurable]
        public string SpritePath = "sb/glow.png";

        [Configurable]
        public double SpriteScale = 1;

        public override void Generate()
        {
            var hitobjectLayer = GetLayer("");
            foreach (var hitobject in Beatmap.HitObjects)
            {
                if ((StartTime != 0 || EndTime != 0) && 
                    (hitobject.StartTime < StartTime - 5 || EndTime - 5 <= hitobject.StartTime))
                    continue;

                    for (int i = 0; i < 30; i++) {
                        var spawnAngle = Random(Math.PI * 2);
                        var moveAngle = MathHelper.DegreesToRadians(0 + Random(-360, 360) * 0.5f);

                        var moveDistance = 60 * 2500 * 0.001f;

                        var endPosition = hitobject.Position + new Vector2((float)Math.Cos(moveAngle), (float)Math.Sin(moveAngle)) * moveDistance;

                        

                        var star = hitobjectLayer.CreateSprite(SpritePath, OsbOrigin.Centre, hitobject.Position);
                        star.Scale(OsbEasing.In, hitobject.StartTime, hitobject.StartTime + FadeTime, SpriteScale, SpriteScale * 0.2);
                        star.Fade(OsbEasing.In, hitobject.StartTime, hitobject.StartTime + FadeTime, 1, 0);
                        star.Additive(hitobject.StartTime, hitobject.StartTime + FadeTime);
                        star.Move(OsbEasing.In, hitobject.StartTime, hitobject.StartTime + FadeTime, hitobject.Position, endPosition);
                    }
            }
        }
    }
}
