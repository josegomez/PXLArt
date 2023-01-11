
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace PXLArt
{
    public partial class XLPixelArtGenerator : Form
    {
        public XLPixelArtGenerator()
        {
            InitializeComponent();
        }

        private void convertImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Bitmap Images|*.bmp;*.jpg;*.jpeg;*.png;*.gif;*.ico;*.tif;*.tiff|All Files (*.*)|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {

                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

                Image image = Image.FromFile(ofd.FileName);
                int xX = picPic.Width, yY = picPic.Height;
                int newWidth, newHeight;
                if (image.Width > xX || image.Height > yY)
                {
                    if (image.Width > image.Height)
                    {
                        newWidth = xX;
                        newHeight = (int)(image.Height * ((float)xX / image.Width));
                    }
                    else
                    {
                        newWidth = (int)(image.Width * ((float)yY / image.Height));
                        newHeight = yY;
                    }
                    Image newImage = new Bitmap(newWidth, newHeight);
                    using (var graphics = Graphics.FromImage(newImage))
                    {
                        graphics.DrawImage(image, 0, 0, newWidth, newHeight);
                    }
                    image.Dispose();
                    image = newImage;
                }
                picPic.Image = image;

                // Get the width and height of the image
                int width = image.Width;
                int height = image.Height;
                pbProgress.Maximum = (width * height) + width + height;
                pbProgress.Step = 1;
                Task.Run(() =>
                {
                    // create new Excel package
                    using (ExcelPackage excelPackage = new ExcelPackage())
                    {
                        // add a new worksheet to the empty workbook
                        ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("PixelArt");
                        Bitmap imageB = new Bitmap(image);
                        BitmapData imageData = imageB.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
                        // Get the address of the first pixel
                        IntPtr pointer = imageData.Scan0;
                        // Declare an array to hold the bytes of the image
                        int bytes = Math.Abs(imageData.Stride) * height;
                        byte[] data = new byte[bytes];
                        // Copy the pixels to the array
                        System.Runtime.InteropServices.Marshal.Copy(pointer, data, 0, bytes);
                        // Unlock the bits of the image
                        imageB.UnlockBits(imageData);

                        List<Tuple<int, int>> listOfCordinates = new List<Tuple<int, int>>();
                        // Loop through the rows and columns of the image
                        for (int row = 1; row <= height; row++)
                        {
                            for (int col = 1; col <= width; col++)
                            {

                                int pixelIndex = ((row - 1) * width + (col - 1)) * 4;
                                Color pixelColor = Color.FromArgb(data[pixelIndex + 3], data[pixelIndex + 2], data[pixelIndex + 1], data[pixelIndex]);
                                // Set the cell color to the pixel color
                                worksheet.Cells[row, col].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                                worksheet.Cells[row, col].Style.Fill.BackgroundColor.SetColor(pixelColor);
                                try
                                {
                                    this.Invoke((ThreadStart)delegate ()
                                    {
                                        pbProgress.PerformStep();
                                    });
                                }
                                catch { }
                            }
                        }




                        // set the column width for all columns to be square
                        for (int i = 1; i <= width; i++)
                        {
                            worksheet.Column(i).Width = 2.14;
                            try
                            {
                                this.Invoke((ThreadStart)delegate ()
                                {
                                    pbProgress.PerformStep();
                                });
                            }
                            catch { }
                        }
                        for (int i = 1; i <= height; i++)
                        {
                            worksheet.Row(i).Height = 15;
                            try
                            {
                                this.Invoke((ThreadStart)delegate ()
                                {
                                    pbProgress.PerformStep();
                                });
                            }
                            catch { }
                        }
                        worksheet.View.ZoomScale = 25;
                        // Save the workbook to a memory stream
                        MemoryStream stream = new MemoryStream();
                        excelPackage.SaveAs(stream);
                        // write memory stream to a file
                        //
                        this.Invoke((ThreadStart)delegate ()
                        {
                            SaveFileDialog sfd = new SaveFileDialog();
                            sfd.Filter = "Excel Files|*.xls;*.xlsx|All Files (*.*)|*.*";
                            sfd.DefaultExt = ".xlsx";
                            sfd.FileName = Path.GetFileNameWithoutExtension(ofd.FileName);
                            if (sfd.ShowDialog() == DialogResult.OK)
                            {
                                File.WriteAllBytes(sfd.FileName, stream.ToArray());
                                Process.Start(sfd.FileName);
                            }
                        });
                    }
                });
            }

        }

    }
}
