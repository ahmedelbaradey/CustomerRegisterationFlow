using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CustomerRegisterationFlow.Resources
{
    public static class SharedResourcesKey
    {
        public const string EmptyICNumberValidation = "EmptyICNumberValidation";
        public const string EmptyIdValidation = "EmptyIdValidation";
        public const string EmptyPINCodeValidation = "EmptyPINCodeValidation";
        public const string ExistICNumberValidation = "ExistICNumberValidation";
        public const string HasAcceptedTerms = "ExistICNumberValidation";
        public const string EmptyCustomerNameValidtion = "EmptyCustomerNameValidtion";
        public const string MaximumCharsCustomerNameValidtion = "MaximumCharsCustomerNameValidtion";
        public const string EmptyCustomerPhoneValidation = "EmptyCustomerNameValidtion";
        public const string ExistCustomerPhoneValidation = "EmptyCustomerNameValidtion";
        public const string EmptyCustomerEmailValidation = "EmptyCustomerEmailValidation";
        public const string ExistCustomerEmailValidation = "EmptyCustomerNameValidtion";
        public const string EmptyTOTPValidation = "EmptyTOTPValidation";
        public const string MaximumDigitsTOTPValidation = "MaximumDigitsTOTPValidation";
        public const string MinimumDigitsTOTPValidation = "MinimumDigitsTOTPValidation";
        public const string NotValidOrExpiredTOTPValidation = "NotValidOrExpiredTOTPValidation";
        public const string NotValidPINCodeValidation = "NotValidPINCodeValidation";
        public const string MaximumDigitsPINCodeValidation = "MaximumDigitsPINCodeValidation";
        public const string MinimumDigitsPINCodeValidation = "MinimumDigitsPINCodeValidation";
        public const string CustomerCreationFailed = "CustomerCreationFailed";
        public const string PhoneTOTPIs = "PhoneTOTPIs";
        public const string TOTPExpireAfter = "TOTPExpireAfter";
        public const string PINCodeUpdated = "PINCodeUpdated";
        public const string PINCodeCreationFailed = "PINCodeCreationFailed";
        public const string BiometricLoginEnabledSucessfully = "BiometricLoginEnabledSucessfully";
        public const string BiometricLoginEnabledFailed = "BiometricLoginEnabledFailed";
        public const string TermsAcceptedSucessfully = "TermsAcceptedSucessfully";
        public const string TermsAcceptedFailed = "TermsAcceptedFailed";
        public const string EmailVerirfiedSucessfully = "EmailVerirfiedSucessfully";
        public const string EmailVerificationFailed = "EmailVerificationFailed";
        public const string PhoneVerifiedAndEmailTOTPIs = "PhoneVerifiedAndEmailTOTPIs";
        public const string PhoneVerificationFailed = "PhoneVerificationFailed";
        public const string PINCodeVerirfiedSucessfully = "PINCodeVerirfiedSucessfully";
        public const string PINCodeVerificationFailed = "PINCodeVerificationFailed";
        public const string ValidEmailValidation = "ValidEmailValidation";
        public const string TheCustomerWithICNumber = "TheCustomerWithICNumber";
        public const string DoesntExist = "DoesntExist";

    }
}
