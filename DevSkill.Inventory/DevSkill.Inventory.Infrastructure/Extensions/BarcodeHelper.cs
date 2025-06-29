using System.Drawing;
using System.Drawing.Imaging;
using ZXing;
using ZXing.Common;
using ZXing.Rendering;
namespace DevSkill.Inventory.Infrastructure.Extensions
{
    public class BarcodeHelper
    {
        public static string GenerateProductBarcode(string productName, string productCode, decimal price, string savePath)
        {
            // Barcode value (what's actually encoded)
            var barcodeContent = productCode;

            var writer = new BarcodeWriter<Bitmap>
            {
                Format = BarcodeFormat.CODE_128,
                Options = new EncodingOptions
                {
                    Width = 400,
                    Height = 100,
                    Margin = 10,
                    PureBarcode = true
                },
                Renderer = new BitmapRenderer()
            };

            // Generate barcode image
            using (Bitmap barcodeBitmap = writer.Write(productCode))
            {
                int finalWidth = 400;
                int finalHeight = 160;

                using (Bitmap finalImage = new Bitmap(finalWidth, finalHeight))
                using (Graphics graphics = Graphics.FromImage(finalImage))
                {
                    graphics.Clear(Color.White);

                    // Draw barcode
                    graphics.DrawImage(barcodeBitmap, new Point(0, 0));

                    // Draw text under the barcode
                    using (System.Drawing.Font font = new System.Drawing.Font("Arial", 12, FontStyle.Regular))
                    using (Brush brush = Brushes.Black)
                    {
                        graphics.DrawString(productName, font, brush, new PointF(10, 105));
                        graphics.DrawString(productCode, font, brush, new PointF(10, 125));
                        graphics.DrawString($"TK. {price:F2}", font, brush, new PointF(10, 145));
                    }

                    // Save final image
                    finalImage.Save(savePath, ImageFormat.Png);
                }
            }

            return savePath;
        }
    }
}
