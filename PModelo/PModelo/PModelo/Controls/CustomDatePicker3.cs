﻿using Xamarin.Forms;

namespace PModelo.Controls
{
    public class CustomDatePicker3 : DatePicker
    {
        public CustomDatePicker3()
        {
            TextColor = Color.FromHex("#282828");
        }
        public static readonly BindableProperty EnterTextProperty = BindableProperty.Create(propertyName: "EnterText", returnType: typeof(string), declaringType: typeof(CustomDatePicker), defaultValue: default(string));
        public string EnterText { get; set; }

        public static readonly BindableProperty CustomFontFamilyProperty = BindableProperty.Create(propertyName: "CustomFontFamily", returnType: typeof(string), declaringType: typeof(CustomDatePicker), defaultValue: default(string));
        public string CustomFontFamily { get; set; }

        public static readonly BindableProperty CustomFontSizeProperty = BindableProperty.Create(propertyName: "CustomFontSize", returnType: typeof(float), declaringType: typeof(CustomDatePicker), defaultValue: default(float));
        public float CustomFontSize { get; set; }

    }
}
