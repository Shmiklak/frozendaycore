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
    
    public class Cursor : StoryboardObjectGenerator
    {
        [Configurable]
        public int StartTime = 0;

        [Configurable]
        public int EndTime = 0;

        [Configurable]
        public int FadeTime = 200;
 
        [Configurable]
        public int FadeoutTime = 200;

        [Configurable]
        public string SpritePath = "sb/glow.png";
    
        [Configurable]

        public float intensity = 0.05f;
    
        [Configurable]

        public float Opacity = 0.7f;

        [Configurable]
        public double BeatDivisor = 8;

        public override void Generate()

        {
            var hitobjectLayer = GetLayer("");
            var bitmap = GetMapsetBitmap(SpritePath);
            var hSprite = GetLayer("").CreateSprite(SpritePath, OsbOrigin.Centre);
            var middle = new Vector2(320, 240);
            var realMiddleX = (float)middle.X - (float)middle.X * (float)intensity;
            var realMiddleY = (float)middle.Y - (float)middle.Y * (float)intensity;
            var Center = new Vector2(realMiddleX, realMiddleY);
            hSprite.Scale(StartTime, 1);
            hSprite.Fade(StartTime, Opacity);
            var StartOn = 0;
            var Amount = 0;
            foreach (var hitobject in Beatmap.HitObjects)
            {
                    if ((StartTime != 0 || EndTime != 0) && (hitobject.StartTime <= StartTime - 5))
                    {
                    StartOn++;
                    }
            }
            foreach (var hitobject in Beatmap.HitObjects)
            {
                    if ((StartTime != 0 || EndTime != 0) && 
                    (hitobject.StartTime >= StartTime - 5 && hitobject.StartTime <= EndTime - 5))
                    {
                    Amount++;
                    }
            }
            var pl = Amount + StartOn;
            for (int i = StartOn; i <= pl; i++)
            {
                var hitobject = Beatmap.HitObjects.ElementAt(i);
                var hitstart = hitobject.PositionAtTime(hitobject.StartTime);
                var hitend = hitobject.PositionAtTime(hitobject.EndTime);
                var movebyXhitstart = (float)hitstart.X * (float)intensity;
                var movebyYhitstart = (float)hitstart.Y * (float)intensity;
                var movebyXhitend = (float)hitend.X * (float)intensity;
                var movebyYhitend = (float)hitend.Y * (float)intensity;
                var movehitbyXYstart = new Vector2(movebyXhitstart, movebyYhitstart);
                var movehitbyXYend = new Vector2(movebyXhitend, movebyYhitend);
                if (i > StartOn && i < pl)
                {
                    var previous = Beatmap.HitObjects.ElementAt(i - 1);
                    var prevstart = previous.PositionAtTime(previous.StartTime);
                    var prevend = previous.PositionAtTime(previous.EndTime);
                    var movebyXprevstart = (float)prevstart.X * (float)intensity;
                    var movebyYprevstart = (float)prevstart.Y * (float)intensity;
                    var movebyXprevend = (float)prevend.X * (float)intensity;
                    var movebyYprevend = (float)prevend.Y * (float)intensity;
                    var moveprevbyXYstart = new Vector2(movebyXprevstart, movebyYprevstart);
                    var moveprevbyXYend = new Vector2(movebyXprevend, movebyYprevend);
                    if (previous is OsuCircle && hitobject is OsuCircle)
                    {
                        hSprite.Move(previous.StartTime, hitobject.StartTime, Center + moveprevbyXYstart, Center + movehitbyXYstart);
                    }
                    if (previous is OsuCircle && hitobject is OsuSlider)
                    {
                        hSprite.Move(previous.StartTime, hitobject.StartTime, Center + moveprevbyXYstart, Center + movehitbyXYstart);
                        var timestep = Beatmap.GetTimingPointAt((int)hitobject.StartTime).BeatDuration / BeatDivisor;
                        var startTime = hitobject.StartTime;
                        if (hitobject is OsuSlider)
                        {
                        while (true)
                        {
                        var endTime = startTime + timestep;

                        var complete = hitobject.EndTime - endTime < 5;
                        if (complete) endTime = hitobject.EndTime;
                        var startPosition = hitobject.PositionAtTime(startTime) * intensity;
                        var endPosition = hitobject.PositionAtTime(endTime) * intensity;
                        
                        hSprite.Move(startTime, endTime,Center +(Vector2)startPosition,Center +endPosition);

                        if (complete) break;
                        startTime += timestep;
                        }
                        }
                    }
                    if (previous is OsuSlider && hitobject is OsuCircle)
                    {
                        hSprite.Move(previous.EndTime, hitobject.StartTime, Center + moveprevbyXYend, Center + movehitbyXYstart);
                    }
                    if (previous is OsuSlider && hitobject is OsuSlider)
                    {
                        hSprite.Move(previous.EndTime, hitobject.StartTime, Center + moveprevbyXYend, Center + movehitbyXYstart);
                        var timestep = Beatmap.GetTimingPointAt((int)hitobject.StartTime).BeatDuration / BeatDivisor;
                        var startTime = hitobject.StartTime;
                        if (hitobject is OsuSlider)
                        {
                        while (true)
                        {
                        var endTime = startTime + timestep;

                        var complete = hitobject.EndTime - endTime < 5;
                        if (complete) endTime = hitobject.EndTime;

                        var startPosition = hitobject.PositionAtTime(startTime) * intensity;
                        var endPosition = hitobject.PositionAtTime(endTime) * intensity;
                        
                        hSprite.Move(startTime, endTime, Center + (Vector2)startPosition, Center + endPosition);

                        if (complete) break;
                        startTime += timestep;
                        }
                        }
                    }
                    if ((previous is OsuSlider || previous is OsuCircle) && hitobject is OsuSpinner)
                    {
                        hSprite.Move(previous.EndTime, hitobject.StartTime, Center + moveprevbyXYend, middle);
                    }
                    if ((hitobject is OsuSlider || hitobject is OsuCircle) && previous is OsuSpinner)
                    {
                        hSprite.Move(previous.EndTime, hitobject.StartTime, middle, Center + movehitbyXYstart);
                    }
                }
                else if (i == StartOn)
                {
                    hSprite.Move(hitobject.StartTime - FadeTime, hitobject.StartTime, middle, Center + movehitbyXYstart);
                    hSprite.Move(hitobject.StartTime, hitobject.EndTime, Center + movehitbyXYstart, Center + movehitbyXYend);

                }
                else if (i == pl)
                {
                    var previous = Beatmap.HitObjects.ElementAt(i - 1);
                    var prevend = previous.PositionAtTime(previous.EndTime);
                    var movebyXprevend = (float)prevend.X * (float)intensity;
                    var movebyYprevend = (float)prevend.Y * (float)intensity;
                    var moveprevbyXYend = new Vector2(movebyXprevend, movebyYprevend);
                    hSprite.Move(previous.EndTime, hitobject.StartTime, Center + moveprevbyXYend, Center + movehitbyXYstart);
                    hSprite.Move(hitobject.StartTime, hitobject.EndTime + FadeoutTime, Center + movehitbyXYstart, middle);
                    hSprite.Fade(hitobject.StartTime, hitobject.EndTime + FadeoutTime, Opacity, 0);
                }
            }
        }
    }
}
