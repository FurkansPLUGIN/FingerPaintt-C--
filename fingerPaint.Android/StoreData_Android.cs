using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using fingerPaint.Droid;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xamarin.Forms;

[assembly: Dependency(typeof(StoreData_Android))]
namespace fingerPaint.Droid
{
    class StoreData_Android : IStorage
    {
        public string SaveImage(byte[] byteData)
        {
            string path = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, "Signature_" + DateTime.UtcNow.ToBinary().ToString() + ".Jpg");
            File.WriteAllBytes(path, byteData);
            return path;
        }
    }
}