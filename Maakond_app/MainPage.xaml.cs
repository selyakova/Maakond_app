﻿using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Xamarin.Forms;

namespace Maakond_app
{
    public partial class MainPage : ContentPage
    {
        protected internal ObservableCollection<Maakond> Maakonnads { get; set; }
        string filename;
        string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);//
        public MainPage()
        {
            InitializeComponent();
            Maakonnads = new ObservableCollection<Maakond>
            {
                new Maakond {Nimetus="Harjumaa", Pealinn="Tallinn", Inimeste_arv=52000},
                new Maakond {Nimetus="Tartumaa", Pealinn="Tartu", Inimeste_arv=30000},
                new Maakond {Nimetus="Ida-Virumaa", Pealinn="Kohtla-Järve", Inimeste_arv=10000}
            };
            list.BindingContext = Maakonnads;
        }
        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Maakonnad(null));
        }
        private async void list_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Maakond selected = e.SelectedItem as Maakond;
            if (selected != null)
            {
                list.SelectedItem = null;
                await Navigation.PushAsync(new Maakonnad(selected));
            }
        }
        protected internal void AddMaakond(Maakond maakond)
        {
            Maakonnads.Add(maakond);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        private void Loe_failist(object sender, EventArgs e)
        {
            filename = "Maakonnad.txt";
            if (String.IsNullOrEmpty(filename)) return;
            if (filename != null)
            {
                String[] Andmed = File.ReadAllLines(Path.Combine(folderPath, filename));
                for (int i = 0; i < Andmed.Length; i++)
                {
                    var columns = Andmed[i].Split(' ');
                    var maakond = new Maakond(columns[0], columns[1], int.Parse(columns[2]));
                    if (Maakonnads.Where(m => m.Nimetus == maakond.Nimetus).FirstOrDefault() == null)
                    {
                        Maakonnads.Add(maakond);
                    }
                };
                list.BindingContext = Maakonnads;
            }
        }
    }
}
