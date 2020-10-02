using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using System.Linq;

namespace StorybrewScripts
{
    public class Transition : StoryboardObjectGenerator
    {
        [Configurable]
        public string RectanglePath = "";

        [Configurable]
        public int StartTime = 0;

        [Configurable]
        public int EndTime = 0;

        [Configurable]
        public string AddTime = "";

        [Configurable]
        public bool HORISONTAL = true;

        [Configurable]
        public bool HorisontalDisappear = false;

        [Configurable]
        public bool HorisontalToRight = true;

        [Configurable]
        public bool HorisontalToDown = true;

        [Configurable]
        public double SpriteWidth = 1;

        [Configurable]
        public int DivideByWidth = 8;

        [Configurable]
        public OsbEasing HorisontalEasing;

        [Configurable]
        public Color4 HorisontalColor = new Color4(1, 1, 1, 0.6f);

        [Configurable]
        public int ExtendToTimeHorisontal = 0;

        [Configurable]
        public double ExtendToSpriteWidth = 1;
        
        [Configurable]
        public OsbEasing ExtendToHorisontalEasing;

        [Configurable]
        public bool VERTICAL = false;

        [Configurable]
        public bool VerticalDisappear = false;

        [Configurable]
        public bool VerticalToRight = true;

        [Configurable]
        public bool VerticalToDown = true;

        [Configurable]
        public double SpriteHeight = 1;

        [Configurable]
        public int DivideByHeight = 8;
        
        [Configurable]
        public OsbEasing VerticalEasing;

        [Configurable]
        public Color4 VerticalColor = new Color4(1, 1, 1, 0.6f);

        [Configurable]
        public int ExtendToTimeVertical = 0;

        [Configurable]
        public double ExtendToSpriteHeight = 1;
        
        [Configurable]
        public OsbEasing ExtendToVerticalEasing;

        public override void Generate()
        {     

            int tick = (EndTime-StartTime)/DivideByWidth;
            var bitmap = GetMapsetBitmap(RectanglePath);
            double addTime = 0;
            if(AddTime.All(char.IsDigit))
                addTime = double.Parse(AddTime);
            else if (AddTime.IndexOf("#") == 0)
            {
                AddTime = AddTime.Remove(0, 1);
                addTime = Beatmap.GetTimingPointAt(StartTime).BeatDuration / double.Parse(AddTime);
            }
            

            if(HORISONTAL == true)
            {
                double widthOnArea;
                if (HorisontalToRight == true)
                    widthOnArea = -107;
                else widthOnArea = 747;
                double spriteWidth = 854f/DivideByWidth;

                if(StartTime <= EndTime)
                    for(int i = 0; i < DivideByWidth; i += 1)
                    {
                        var rectangle = GetLayer("Horisontal").CreateSprite(RectanglePath, OsbOrigin.Centre);
                        
                        if(HorisontalToRight == true)        
                        {
                            if (i == 0)
                                widthOnArea += spriteWidth/2; else widthOnArea += spriteWidth;
                        }
                        else 
                        {
                            if (i == 0)
                                widthOnArea -= spriteWidth/2; else widthOnArea -= spriteWidth;
                        }

                        if(!HorisontalDisappear)
                        {
                            rectangle.ScaleVec(HorisontalEasing, StartTime + (i * tick), EndTime, spriteWidth/bitmap.Width * SpriteWidth, 0, spriteWidth/bitmap.Width * SpriteWidth, 2);
                            
                            if(HorisontalToDown == true)
                                rectangle.Move(HorisontalEasing, StartTime + (i * tick), EndTime, widthOnArea, 0, widthOnArea, 240);       
                            else rectangle.Move(HorisontalEasing, StartTime + (i * tick), EndTime, widthOnArea, 480, widthOnArea, 240);
                            rectangle.Color(StartTime, HorisontalColor);
                            
                            if(ExtendToTimeHorisontal != 0)
                                rectangle.ScaleVec(ExtendToHorisontalEasing, EndTime, ExtendToTimeHorisontal, spriteWidth/bitmap.Width * SpriteWidth, 2, spriteWidth/bitmap.Width * SpriteWidth * ExtendToSpriteWidth, 2);
                        }
                        else
                        {
                            rectangle.ScaleVec(HorisontalEasing, StartTime + (i * tick), EndTime, spriteWidth/bitmap.Width * SpriteWidth, 2, spriteWidth/bitmap.Width * SpriteWidth, 0);
                            
                            if(HorisontalToDown == true)
                                rectangle.Move(HorisontalEasing, StartTime + (i * tick), EndTime, widthOnArea, 240, widthOnArea, 0);       
                            else rectangle.Move(HorisontalEasing, StartTime + (i * tick), EndTime, widthOnArea, 240, widthOnArea, 480);
                            rectangle.Color(StartTime, HorisontalColor);
                            
                            if(ExtendToTimeHorisontal != 0)
                                rectangle.ScaleVec(ExtendToHorisontalEasing, EndTime, ExtendToTimeHorisontal, spriteWidth/bitmap.Width * SpriteWidth, 0, spriteWidth/bitmap.Width * SpriteWidth * ExtendToSpriteWidth, 0);
                        }
                    }
                else
                    for(int i = 0; i < DivideByWidth; i += 1)
                    {
                        var rectangle = GetLayer("Horisontal").CreateSprite(RectanglePath, OsbOrigin.Centre);
                        
                        if(HorisontalToRight == true)        
                        {
                            if (i == 0)
                                widthOnArea += spriteWidth/2; else widthOnArea += spriteWidth;
                        }
                        else 
                        {
                            if (i == 0)
                                widthOnArea -= spriteWidth/2; else widthOnArea -= spriteWidth;
                        }

                        if(!HorisontalDisappear)
                        {
                            rectangle.ScaleVec(HorisontalEasing, StartTime + (addTime * i), StartTime + (addTime * (i + 1)), spriteWidth/bitmap.Width * SpriteWidth, 0, spriteWidth/bitmap.Width * SpriteWidth, 2);
                            
                            if(HorisontalToDown == true)
                                rectangle.Move(HorisontalEasing, StartTime + (addTime * i), StartTime + (addTime * (i + 1)), widthOnArea, 0, widthOnArea, 240);
                            else rectangle.Move(HorisontalEasing, StartTime + (addTime * i), StartTime + (addTime * (i + 1)), widthOnArea, 480, widthOnArea, 240);
                            
                            if (i != DivideByWidth - 1)
                                rectangle.Move(StartTime + (addTime * (i + 1)), StartTime + (addTime * DivideByWidth), widthOnArea, 240, widthOnArea, 240);
                            rectangle.Color(StartTime + (addTime * i), HorisontalColor);
                            
                            if(ExtendToTimeHorisontal != 0)
                                rectangle.ScaleVec(ExtendToHorisontalEasing, StartTime + (addTime * DivideByWidth), ExtendToTimeHorisontal, spriteWidth/bitmap.Width * SpriteWidth, 2, spriteWidth/bitmap.Width * SpriteWidth * ExtendToSpriteWidth, 2);
                        }
                        else
                        {
                            rectangle.ScaleVec(HorisontalEasing, StartTime + (addTime * i), StartTime + (addTime * (i + 1)), spriteWidth/bitmap.Width * SpriteWidth, 2, spriteWidth/bitmap.Width * SpriteWidth, 0);
                            
                            if(HorisontalToDown == true)
                            {
                                rectangle.Move(StartTime, StartTime + (addTime * i), widthOnArea, 240, widthOnArea, 240);
                                rectangle.Move(HorisontalEasing, StartTime + (addTime * i), StartTime + (addTime * (i + 1)), widthOnArea, 240, widthOnArea, 0);
                            }
                            else
                            {
                                rectangle.Move(StartTime, StartTime + (addTime * i), widthOnArea, 240, widthOnArea, 240);
                                rectangle.Move(HorisontalEasing, StartTime + (addTime * i), StartTime + (addTime * (i + 1)), widthOnArea, 240, widthOnArea, 480);
                            }
                            rectangle.Color(StartTime + (addTime * i), HorisontalColor);
                            
                            if(ExtendToTimeHorisontal != 0)
                                rectangle.ScaleVec(ExtendToHorisontalEasing, StartTime + (addTime * DivideByWidth), ExtendToTimeHorisontal, spriteWidth/bitmap.Width * SpriteWidth, 0, spriteWidth/bitmap.Width * SpriteWidth * ExtendToSpriteWidth, 0);
                        }

                        rectangle.ScaleVec(HorisontalEasing, StartTime + (addTime * DivideByWidth), StartTime + (addTime * DivideByWidth) + 300, spriteWidth/bitmap.Width * SpriteWidth, 2, 0, 2);
                    }
                }
            if(VERTICAL == true)
            {
                double heightOnArea;
                if (VerticalToDown == true)
                    heightOnArea = 0;
                else heightOnArea = 480;
                double spriteHeight = 480f/DivideByHeight;

                if(StartTime <= EndTime)
                    for(int i = 0; i < DivideByHeight; i += 1)
                    {
                        var rectangle = GetLayer("Vertical").CreateSprite(RectanglePath, OsbOrigin.Centre);

                        if(VerticalToDown == true)        
                        {
                            if (i == 0)
                                heightOnArea += spriteHeight/2; else heightOnArea += spriteHeight;
                        }
                        else 
                        {
                            if (i == 0)
                                heightOnArea -= spriteHeight/2; else heightOnArea -= spriteHeight;
                        }

                        if(!VerticalDisappear)
                        {
                            rectangle.ScaleVec(VerticalEasing, StartTime + (i * tick), EndTime, 0, spriteHeight/bitmap.Height * SpriteHeight, 2, spriteHeight/bitmap.Height * SpriteHeight);
                            
                            if(VerticalToRight == true)
                                rectangle.Move(VerticalEasing, StartTime + (i * tick), EndTime, -107, heightOnArea, 320, heightOnArea);   
                            else rectangle.Move(VerticalEasing, StartTime + (i * tick), EndTime, 747, heightOnArea, 320, heightOnArea); 
                            rectangle.Color(StartTime, VerticalColor);  
                            
                            if(ExtendToTimeVertical != 0)
                                rectangle.ScaleVec(ExtendToVerticalEasing, EndTime, ExtendToTimeVertical, 2, spriteHeight/bitmap.Height * SpriteHeight, 2, spriteHeight/bitmap.Height * SpriteHeight * ExtendToSpriteHeight);
                        }
                        else
                        {
                            rectangle.ScaleVec(VerticalEasing, StartTime + (i * tick), EndTime, 2, spriteHeight/bitmap.Height * SpriteHeight, 0, spriteHeight/bitmap.Height * SpriteHeight);

                            if(VerticalToRight == true)
                                rectangle.Move(VerticalEasing, StartTime + (i * tick), EndTime, 320, heightOnArea, -107, heightOnArea);   
                            else rectangle.Move(VerticalEasing, StartTime + (i * tick), EndTime, 320, heightOnArea, 747, heightOnArea); 
                            rectangle.Color(StartTime, VerticalColor);  
                            
                            if(ExtendToTimeVertical != 0)
                                rectangle.ScaleVec(ExtendToVerticalEasing, EndTime, ExtendToTimeVertical, 0, spriteHeight/bitmap.Height * SpriteHeight, 0, spriteHeight/bitmap.Height * SpriteHeight * ExtendToSpriteHeight);
                        }
                    }
                else
                    for(int i = 0; i < DivideByHeight; i += 1)
                    {
                        var rectangle = GetLayer("Vertical").CreateSprite(RectanglePath, OsbOrigin.Centre);
                        
                        if(VerticalToDown == true)        
                        {
                            if (i == 0)
                                heightOnArea += spriteHeight/2; else heightOnArea += spriteHeight;
                        }
                        else 
                        {
                            if (i == 0)
                                heightOnArea -= spriteHeight/2; else heightOnArea -= spriteHeight;
                        }
                        if(!VerticalDisappear)
                        {
                            rectangle.ScaleVec(VerticalEasing, StartTime + (addTime * i), StartTime + (addTime * (i + 1)), 0, spriteHeight/bitmap.Height * SpriteHeight, 2, spriteHeight/bitmap.Height * SpriteHeight);
                            
                            if(VerticalToRight == true)
                                rectangle.Move(VerticalEasing, StartTime + (addTime * i), StartTime + (addTime * (i + 1)), -107, heightOnArea, 320, heightOnArea);
                            else rectangle.Move(VerticalEasing, StartTime + (addTime * i), StartTime + (addTime * (i + 1)), 747, heightOnArea, 320, heightOnArea);
                            
                            if (i != DivideByHeight - 1)
                                // rectangle.Move(StartTime + (addTime * (i + 1)), StartTime + (addTime * DivideByHeight), 320, heightOnArea, 320, heightOnArea);
                            // rectangle.Color(StartTime + (addTime * i), VerticalColor);

                            if(ExtendToTimeVertical != 0)
                                rectangle.ScaleVec(ExtendToVerticalEasing, StartTime + (addTime * DivideByHeight), ExtendToTimeVertical, 2, spriteHeight/bitmap.Height * SpriteHeight, 2, spriteHeight/bitmap.Height * SpriteHeight * ExtendToSpriteHeight);
                        }
                        else
                        {
                            rectangle.ScaleVec(VerticalEasing, StartTime + (addTime * i), StartTime + (addTime * (i + 1)), 2, spriteHeight/bitmap.Height * SpriteHeight, 0, spriteHeight/bitmap.Height * SpriteHeight);
                            
                            if(VerticalToRight == true)
                            {
                                // rectangle.Move(StartTime, StartTime + (addTime * i), 320, heightOnArea, 320, heightOnArea);
                                // rectangle.Move(VerticalEasing, StartTime + (addTime * i), StartTime + (addTime * (i + 1)), 320, heightOnArea, -107, heightOnArea);
                            }
                            else 
                            {
                                // rectangle.Move(StartTime, StartTime + (addTime * i), 320, heightOnArea, 320, heightOnArea);
                                // rectangle.Move(VerticalEasing, StartTime + (addTime * i), StartTime + (addTime * (i + 1)), 320, heightOnArea, 747, heightOnArea);
                            }
                            // rectangle.Color(StartTime + (addTime * i), VerticalColor);

                            if(ExtendToTimeVertical != 0)
                                rectangle.ScaleVec(ExtendToVerticalEasing, StartTime + (addTime * DivideByHeight), ExtendToTimeVertical, 0, spriteHeight/bitmap.Height * SpriteHeight, 0, spriteHeight/bitmap.Height * SpriteHeight * ExtendToSpriteHeight);
                        }
                    }
            }
        }
    }
}
