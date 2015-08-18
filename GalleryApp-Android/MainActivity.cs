using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using GalleryApp.Core;
using System.Threading;

namespace GalleryApp_Android
{
    [Activity(Label = "GalleryApp_Android", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private const string UploadUrl = "http://192.168.1.103/api/photo";

        private PhotoUploader _uploader;
        private PhotoListener _listener;
        private ImageView _imageSection;

        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.myButton);

            button.Click += delegate
            {
                var imageIntent = new Intent();
                imageIntent.SetType("image/*");
                imageIntent.SetAction(Intent.ActionGetContent);
                StartActivityForResult(
                    Intent.CreateChooser(imageIntent, "Select photo"), 0);
            };


            _uploader = new PhotoUploader();
            _listener = new PhotoListener();

            _listener.NewPhotosReceived += (sender, urls) =>
            {
                ThreadPool.QueueUserWorkItem(o =>
                {
                    foreach (var url in urls)
                    {
                        /*// Replace this iOS code
                        _imageSection.Add(
                            new ImageStringElement(DateTime.Now.ToString(),
                                                   UIImage.LoadFromData(NSData.FromUrl(new NSUrl(url))))
                        );

                        // With this, but tailored to load from URL and replace ImageView with http://android-er.blogspot.com/2010/06/listview-with-icon.html
                        _imageSection =
                            FindViewById<ImageView>(Resource.Id.myImageView);
                        _imageSection.SetImageURI(data.Data);
                        */

                    }
                });
            };

            await _listener.StartListening();
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if (resultCode == Result.Ok)
            {
                _imageSection =
                    FindViewById<ImageView>(Resource.Id.myImageView);
                _imageSection.SetImageURI(data.Data);

                
                byte[] bytes;
                using (var imageData = _imageSection.AsJPEG())
                {
                    bytes = new byte[imageData.Length];
                    Marshal.Copy(imageData.Bytes, bytes, 0, Convert.ToInt32(imageData.Length));
                }

                //        await _uploader.UploadPhoto(bytes, "jpg");

            }
        }

        // Note: merge this method into OnActivityResult
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

