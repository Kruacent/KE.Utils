using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Features.Pools;
using LabApi.Features.Wrappers;
using UnityEngine;
using Color = System.Drawing.Color;
using Exiled.API.Features.Toys;
using Exiled.API.Features;

namespace KE.Utils.API.GifAnimator
{
    public class TextImage
    {


        private string rawString = string.Empty;

        public const string Base = "█";

        public TextImage(Image img,float size)
        {
            FrameDimension dimension = new FrameDimension(img.FrameDimensionsList[0]);
            img.SelectActiveFrame(dimension, 0);
            var b = new Bitmap(img);

            StringBuilder sb = StringBuilderPool.Pool.Get();

            sb.Append("<size=");
            sb.Append(size);
            sb.Append(">");

            for (int y = 0; y < b.Height; y++)
            {
                Color? lastColor = null;
                int runLength = 0;

                for (int x = 0; x < b.Width; x++)
                {
                    Color current = b.GetPixel(x, y);

                    if (lastColor == null || current != lastColor)
                    {
                        // Flush previous run
                        if (lastColor != null)
                            AppendRun(sb, lastColor.Value, runLength);

                        lastColor = current;
                        runLength = 1;
                    }
                    else
                    {
                        runLength++;
                    }
                }

                // Flush end-of-line run
                if (lastColor != null)
                    AppendRun(sb, lastColor.Value, runLength);

                sb.AppendLine();
            }

            sb.Append("</size>");
            rawString = sb.ToString();
            Log.Info("image length = " + rawString.Length);

            StringBuilderPool.Pool.Return(sb);
        }
        private static void AppendRun(StringBuilder sb, Color color, int count)
        {
            sb.Append("<color=");
            sb.Append(ToHexValue(color));
            sb.Append(">");

            sb.Append('█', count);

            sb.Append("</color>");
        }


        public TextToy Spawn(Vector3 position, Quaternion? rotation = null,Vector3? scale = null, Transform parent = null)
        {

            TextToy textToy = TextToy.Create(position, rotation ?? Quaternion.Euler(Vector3.zero), scale ?? Vector3.one , parent,false);

            textToy.TextFormat = rawString;
            


            return textToy;
        }


        public string RawString => rawString;



        private void Animated(Image img)
        {
            FrameDimension dimension = new FrameDimension(img.FrameDimensionsList[0]);
            int frameCount = img.GetFrameCount(dimension);
            StringBuilder sb = StringBuilderPool.Pool.Get();
            for (int i = 0; i < frameCount; i++)
            {
                img.SelectActiveFrame(dimension, i);
                var b = new Bitmap(img);

                for (int x = 0; x < b.Width; x++)
                {
                    for (int y = 0; y < b.Height; y++)
                    {
                        Color pixelColor = b.GetPixel(x, y);

                        //sb.Append(PixelToString(pixelColor));
                    }
                }

            }

            rawString = sb.ToString();

            StringBuilderPool.Pool.Return(sb);
        }





        private static string ToHexValue(Color color)
        {
            return "#" + color.R.ToString("X2") +
                         color.G.ToString("X2") +
                         color.B.ToString("X2") +
                         color.A.ToString("X2");
        }

    }
}
