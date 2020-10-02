using StorybrewCommon.Mapset;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;

namespace StorybrewScripts
{
    public class ClickRings : StoryboardObjectGenerator
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

                var hSprite = hitobjectLayer.CreateSprite(SpritePath, OsbOrigin.Centre, hitobject.Position);

                

                hSprite.Scale(OsbEasing.In, hitobject.StartTime, hitobject.StartTime + FadeTime, SpriteScale, SpriteScale * 0.2);
                hSprite.Fade(OsbEasing.In, hitobject.StartTime, hitobject.StartTime + FadeTime, 1, 0);
                hSprite.Additive(hitobject.StartTime, hitobject.StartTime + FadeTime);
                hSprite.Color(hitobject.StartTime, hitobject.Color);


            }
        }
    }
}
