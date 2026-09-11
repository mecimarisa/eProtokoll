using System;
using System.IO;

namespace eProtokoll
{
    public static class ScannerHelper
    {
        
        private const string WiaFormatJpeg =
            "{B96B3CAE-0728-11D3-9D7B-0000F81EF32E}";

        public static string? SkanoDokumentin(Form formulari)
        {
            try
            {
                WIA.CommonDialog dialog =
                    new WIA.CommonDialog();

                WIA.ImageFile? imazhi =
                    dialog.ShowAcquireImage(
                        WIA.WiaDeviceType.ScannerDeviceType,
                        WIA.WiaImageIntent.ColorIntent,
                        WIA.WiaImageBias.MinimizeSize,
                        WiaFormatJpeg,
                        true,
                        true,
                        false);

                if (imazhi == null)
                {
                    return null;
                }

                string folderi =
                    Path.Combine(
                        Path.GetTempPath(),
                        "eProtokollSkanime");

                Directory.CreateDirectory(folderi);

                string path =
                    Path.Combine(
                        folderi,
                        $"Skanim_{DateTime.Now:yyyyMMdd_HHmmss}_" +
                        $"{Guid.NewGuid():N}.jpg");

                imazhi.SaveFile(path);

                return path;
            }
            catch (System.Runtime.InteropServices.COMException)
            {
                throw new Exception(
                    "Skanimi u anulua ose nuk u gjet " +
                    "asnjë skaner i lidhur.");
            }
            finally
            {
                if (!formulari.IsDisposed)
                {
                    formulari.WindowState = FormWindowState.Normal;
                    formulari.Show();
                    formulari.BringToFront();
                    formulari.Activate();
                    formulari.TopMost = true;
                    formulari.TopMost = false;
                }
            }
        }
    }
}