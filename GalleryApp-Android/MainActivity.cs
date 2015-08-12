using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
//using GalleryApp.Core;

namespace GalleryApp_Android
{
    [Activity(Label = "GalleryApp_Android", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private const string UploadUrl = "http://192.168.1.103/api/photo";

        //private PhotoUploader _uploader;
        //private PhotoListener _listener;
        //private ImageView _imageSection;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.myButton);

            button.Click += delegate {
                var imageIntent = new Intent();
                imageIntent.SetType("image/*");
                imageIntent.SetAction(Intent.ActionGetContent);
                StartActivityForResult(
                    Intent.CreateChooser(imageIntent, "Select photo"), 0);
            };
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if (resultCode == Result.Ok)
            {
                var imageView =
                    FindViewById<ImageView>(Resource.Id.myImageView);
                imageView.SetImageURI(data.Data);
            }
        }


        //public override async void ViewDidLoad()
        //{
        //    base.ViewDidLoad();

        //    NavigationItem.RightBarButtonItem = new UIBarButtonItem(UIBarButtonSystemItem.Add);
        //    NavigationItem.RightBarButtonItem.Clicked += delegate { UploadPicture(); };

        //    _uploader = new PhotoUploader();
        //    _listener = new PhotoListener();

        //    _listener.NewPhotosReceived += (sender, urls) =>
        //    {
        //        InvokeOnMainThread(() =>
        //        {
        //            foreach (var url in urls)
        //            {
        //                _imageSection.Add(
        //                    new ImageStringElement(DateTime.Now.ToString(),
        //                                           UIImage.LoadFromData(NSData.FromUrl(new NSUrl(url))))
        //                );
        //            }
        //        });
        //    };

        //    await _listener.StartListening();
        //}

        //private void UploadPicture()
        //{
        //    _picker = new UIImagePickerController();
        //    _picker.SourceType = UIImagePickerControllerSourceType.PhotoLibrary;
        //    _picker.Canceled += delegate { _picker.DismissViewController(true, null); };
        //    _picker.FinishedPickingMedia += async (s, e) =>
        //    {
        //        _picker.DismissViewController(true, null);
        //        var image = (UIImage)e.Info.ObjectForKey(new NSString("UIImagePickerControllerOriginalImage"));
        //        byte[] bytes;
        //        using (var imageData = image.AsJPEG())
        //        {
        //            bytes = new byte[imageData.Length];
        //            Marshal.Copy(imageData.Bytes, bytes, 0, Convert.ToInt32(imageData.Length));
        //        }

        //        await _uploader.UploadPhoto(bytes, "jpg");
        //    };

        //    PresentViewController(_picker, true, null);
        //}
    }
}

