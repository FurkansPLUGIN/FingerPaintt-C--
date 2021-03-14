
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using SignaturePad.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using PermissionStatus = Plugin.Permissions.Abstractions.PermissionStatus;

namespace fingerPaint
{
    public partial class MainPage : ContentPage
    {
        private Color editedColor;

        public Color EditedColor
        {
            get => editedColor;
            set { editedColor = value; OnPropertyChanged(); }
        }
        public MainPage()
        {
            InitializeComponent();
        }

        private async void save_Clicked(object sender, EventArgs e)
        {
            Stream sigimage = await Padview.GetImageStreamAsync(SignatureImageFormat.Jpg);
            if (sigimage == null)
                return;
            BinaryReader br = new BinaryReader(sigimage);
            br.BaseStream.Position = 0;
            Byte[] All = br.ReadBytes((int)sigimage.Length);
            byte[] image = (byte[])All;

            bool storegaePermissionResp = await IsAvailStoragePermission();
            if (storegaePermissionResp)
            {
                var path = DependencyService.Get<IStorage>().SaveImage(image);
                Padview.Clear();
               await DisplayAlert("Message", "Signature Saved", "Ok");
            }
            else
            {
               await DisplayAlert("Message", "User denied to storage permission", "Ok");
            }
        }

        private async Task<bool> IsAvailStoragePermission()
        {
            bool response = false;
            PermissionStatus permissionStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);
            if (permissionStatus != PermissionStatus.Granted)
            {
                var resp = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Storage);
                if (resp.ContainsKey(Permission.Storage))
                {
                    permissionStatus = resp[Permission.Storage];
                }


                if (permissionStatus == PermissionStatus.Granted)
                {
                    response = true;
                }
                else if (permissionStatus == PermissionStatus.Denied)
                {
                    response = false;
                }
            }
            else
            {
                response = true;
            }
            return response;
        }

        private void clear_Clicked(object sender, EventArgs e)
        {
            Padview.Clear();
        }

        private void color_Clicked(object sender, EventArgs e)
        {
            var color2 = colorx.ViewModel.Color;
            var colorhex = colorx.ViewModel.Hex;
            var color22 = colorx.ViewModel.HueColor;
            Padview.StrokeColor = Color.FromHex(colorhex);

        }

        private void backcolor_Clicked(object sender, EventArgs e)
        {
            var color2 = colorx.ViewModel.Color;
            var colorhex = colorx.ViewModel.Hex;
            var color22 = colorx.ViewModel.HueColor;
            Padview.BackgroundColor = Color.FromHex(colorhex);
        }
    }
}
