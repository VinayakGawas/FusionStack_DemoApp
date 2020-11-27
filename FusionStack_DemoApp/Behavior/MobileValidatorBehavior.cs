using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace FusionStack_DemoApp.Behavior
{
    public class MobileValidatorBehavior : Behavior<Entry>
    {
        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(entry);
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= OnEntryTextChanged;
            base.OnDetachingFrom(entry);
        }

        void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {
            if (string.IsNullOrEmpty(((Entry)sender).Text))
                return;

            string reg = @"^[0-9]+$";
            bool IsNumber = false;
            bool IsValid = false;
            bool HasTen = false;
            if (args.NewTextValue.Length >= 10)
            {
                ((Entry)sender).Text = args.NewTextValue.Substring(0, 10);
                HasTen = true;
            }
            else if (args.NewTextValue.Length < 10)
                HasTen = false;

            IsNumber = (Regex.IsMatch(args.NewTextValue, reg, RegexOptions.IgnoreCase));


            if (HasTen && IsNumber)
                IsValid = true;
            else
                IsValid = false;

            ((Entry)sender).TextColor = IsValid ? Color.Default : Color.Red;
        }
    }
}
