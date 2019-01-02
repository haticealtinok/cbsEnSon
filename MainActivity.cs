using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Support.Design.Widget;
using Android.Views;
using System;
using Android.Animation;
using Ambulance.mCode.hospitalsData;
using Ambulance.mCode.HospitalListView;
using Android.Content;
using Ambulance.mCode.specialityDetail;

namespace Ambulance
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private static bool isAmbulanceOpen;
        private FloatingActionButton ambulanceNavigation;
        private FloatingActionButton ambulanceDoctor;
        private FloatingActionButton ambulanceMain;
        private View bgAmbulanceMenu;
        //private ListView lv;
        //private CustomAdapter adapter;
        //private JavaList<hospitals> hospital;
        private Intent i = null;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            ambulanceNavigation = FindViewById<FloatingActionButton>(Resource.Id.ambulance_navigation);
            ambulanceDoctor = FindViewById<FloatingActionButton>(Resource.Id.ambulance_doctor);
            ambulanceMain = FindViewById<FloatingActionButton>(Resource.Id.ambulance_main);
            bgAmbulanceMenu = FindViewById<View>(Resource.Id.ambulance_menu);
            //lv = FindViewById<ListView>(Resource.Id.lv);
            //hospital = hospitalsCollcetion.GetHospitals();
            //adapter = new CustomAdapter(this, hospital);

            //lv.Adapter = adapter;
           
            
            ambulanceMain.Click += (o, e) =>
              {
                  if (!isAmbulanceOpen)
                      ShowAmbulanceMenu();
                  else
                      CloseAmbulanceMenu();
              };

            ambulanceNavigation.Click += (o, e) =>
             {
                 CloseAmbulanceMenu();
                 Toast.MakeText(this, "Navigation", ToastLength.Short).Show();
             };
            ambulanceDoctor.Click += (o, e) =>
            {
                PopupMenu menu = new PopupMenu(this, ambulanceDoctor);
                menu.MenuInflater.Inflate(Resource.Layout.DetailLayout, menu.Menu);
                menu.MenuItemClick += (s, arg) =>
                  {
                     // Toast.MakeText(this, string.Format("Uzamlık {0} clicked", arg.Item.TitleFormatted), ToastLength.Short).Show();
                      this.OpenHospitalActivity();
                  };
                menu.Show();
               // CloseAmbulanceMenu();
                Toast.MakeText(this, "Doctors", ToastLength.Short).Show();
            };

            bgAmbulanceMenu.Click+=(o,e)=> CloseAmbulanceMenu();
        }

        //private void lv_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        //{
        //    Toast.MakeText(this, hospital[e.Position].Name, ToastLength.Long).Show();
        //}
        //private void lv_ContextMenuCreated(object sender, Button.CreateContextMenuEventArgs e)
        //{
        //    e.Menu.SetHeaderTitle("Uzmanlık Seçiniz: ");
        //    e.Menu.Add(0, 0, 0, "Göz");
        //    e.Menu.Add(0, 1, 0, "Cerrah");
        //    e.Menu.Add(0, 2, 0, "İç Hastalıklar");
        //    e.Menu.Add(0, 3, 0, "Diş Hastalıkları");
        //    e.Menu.Add(0, 4, 0, "Kadın Doğum");
        //    e.Menu.Add(0, 5, 0, "Ortopedi");
        //}
        //public override bool OnContextItemSelected(IMenuItem item)
        //{
        //    string title = item.TitleFormatted.ToString();
        //    this.OpenHospitalActivity(title);
        //    return base.OnContextItemSelected(item);
        //}

        private void OpenHospitalActivity()
        {
            i = new Intent(this, typeof(HospitalActivity));
            //add some data
           // i.PutExtra("NAME_KEY", action);
            this.StartActivity(i);
        }
        private void ShowAmbulanceMenu()
        {
            isAmbulanceOpen = true;
            ambulanceNavigation.Visibility = ViewStates.Visible;
            ambulanceDoctor.Visibility = ViewStates.Visible;
            bgAmbulanceMenu.Visibility = ViewStates.Visible;

            ambulanceMain.Animate().Rotation(135f);
            bgAmbulanceMenu.Animate().Alpha(1f);
            ambulanceNavigation.Animate().TranslationY(-Resources.GetDimension(Resource.Dimension.standard_100)).Rotation(0f);
            ambulanceDoctor.Animate().TranslationY(-Resources.GetDimension(Resource.Dimension.standard_55)).Rotation(0f);
        }

        private void CloseAmbulanceMenu()
        {
            isAmbulanceOpen = false;
            ambulanceMain.Animate().Rotation(0f);
            bgAmbulanceMenu.Animate().Alpha(0f);
            ambulanceNavigation.Animate().TranslationY(0f).Rotation(90f);
            ambulanceDoctor.Animate().TranslationY(0f).Rotation(90f).SetListener(new FabAnimatorListener(bgAmbulanceMenu,ambulanceDoctor,ambulanceNavigation));
        }

        private class FabAnimatorListener : Java.Lang.Object, Animator.IAnimatorListener
        {
            View[] viewsToHide;
            public FabAnimatorListener(params View[] viewsToHide)
            {
                this.viewsToHide = viewsToHide;
            }
            public void OnAnimationCancel(Animator animation)
            {
               
            }

            public void OnAnimationEnd(Animator animation)
            {
                if (!isAmbulanceOpen)
                    foreach (var views in viewsToHide)
                        views.Visibility = ViewStates.Gone;
            }

            public void OnAnimationRepeat(Animator animation)
            {
                
            }

            public void OnAnimationStart(Animator animation)
            {
            }
        }
    }
}