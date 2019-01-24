﻿using System;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DausterDriver.Utils
{
    public class Global
    {
        public bool IsValidEmail(string emailaddress)
        {
            Boolean bSuccess = true;
            try
            {
                MailAddress m = new MailAddress(emailaddress);
            }
            catch (FormatException)
            {
                bSuccess = false;
            }

            return bSuccess;
        }


        public bool IsPasswordValid(String sPassword)
        {
            Regex rgx = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#\$%\^&\*])(?=.{8,})");
            return rgx.IsMatch(sPassword);
        }

        public async Task ShowMessage(string message,
            string title,
            string buttonText,
            Action afterHideCallback)
        {
            await App.Current.MainPage.DisplayAlert(
                title,
                message,
                buttonText);

            afterHideCallback?.Invoke();
        }
    }
}
