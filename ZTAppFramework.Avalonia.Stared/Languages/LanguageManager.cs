﻿using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ZTAppFramework.Avalonia.Stared.Languages
{
    public class LanguageManager : BindableBase
    {
        private const string IndexerName = "Item";
        private const string IndexerArrayName = "Item[]";
        public static LanguageManager Instance { get; set; } = new LanguageManager();
        public LanguageManager()
        {
        }
        private ResourceDictionary _LanguageDictionary;
        public ResourceDictionary LanguageDictionary
        {
            get { return _LanguageDictionary; }
            set { SetProperty(ref _LanguageDictionary, value); }
        }


        public bool LoadLanguage(string uri)
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlReader reader = XmlReader.Create($"{Directory.GetCurrentDirectory()}/{uri}");
            xmlDoc.Load(reader);
            reader.Close();
            var languageXaml = AvaloniaRuntimeXamlLoader.Load(xmlDoc.InnerXml);
            if (languageXaml is ResourceDictionary xaml)
            {
                LanguageDictionary = xaml;
                Invalidate();
                return true;
            }
            return false;
        }
        public string this[string key]
        {
            get
            {
                if (LanguageDictionary == null) return key;
                return LanguageDictionary[key].ToString();
            }
        }
        public void Invalidate()
        {
            OnPropertyChanged(new PropertyChangedEventArgs(IndexerName));
            OnPropertyChanged(new PropertyChangedEventArgs(IndexerArrayName));
        }
    }
}
