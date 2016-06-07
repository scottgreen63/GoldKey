using GoldKeyLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text.RegularExpressions;


namespace GoldKeyLib.Entities
{
    public enum eObjectType
    {
        strType = 0,
        intType = 1,
        dblType = 2,
        bteType = 3
    }

    public class Validation
    {
        #region Properties

        /// <summary>
        /// Gets the number of validation errors
        /// </summary>
        /// <value></value>
        /// <remarks></remarks>
        public int Count
        {
            get
            {
                return ValidationList.Count;
            }
        }

        /// <summary>
        /// Gets the validation errors for the defined property.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public string this[string propertyName]
        {
            get
            {
                if (ValidationList.ContainsKey(propertyName))
                    return ValidationList[propertyName];
                else
                    return null;
            }
        }

        private Dictionary<String, String> mValidationList;

        /// <summary>
        /// Gets or sets the list of validation errors
        /// </summary>
        /// <value></value>
        /// <remarks></remarks>
        private Dictionary<String, String> ValidationList
        {
            get { return mValidationList; }
            set { mValidationList = value; }
        }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Constructor executed when an instance of this class is created
        /// </summary>
        /// <remarks></remarks>
        public Validation()
        {
            // Create the list to contain the validation errors
            ValidationList = new Dictionary<String, String>();
        }

        #endregion Constructors

        #region Methods

        //Validation.ValidateClear(sPropName);
        //Validation.ValidateRequired(sPropName, value);
        //Validation.ValidateZeroLength(sPropName, value);
        //Validation.ValidateMinLength(sPropName, value, 2);
        //Validation.ValidateMaxLength(sPropName, value, 30);

        #region ToString

        /// <summary>
        /// Converts the collection of validation errors
        /// into a single string
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public override string ToString()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            foreach (string s in ValidationList.Values)
                sb.AppendLine(s);

            return sb.ToString();
        }

        #endregion ToString

        #region ValidateClear

        /// <summary>
        /// Clears the validation for a property
        /// </summary>
        /// <param name="propertyName">Name of the Property</param>
        /// <remarks>Should be called before any other validation
        /// is called</remarks>
        public void ValidateClear(string propertyName)
        {
            // If the Property doesn't have any messages, this is done
            if (ValidationList.ContainsKey(propertyName))
                // Otherwise, remove the entry
                ValidationList.Remove(propertyName);
        }

        #endregion ValidateClear

        #region ValidateLength

        public Boolean ValidateMaxLength(string propertyName, string value, int maxLength)
        {
            String newMessage = String.Empty;
            newMessage = String.Format("{0} has a maximum size of {1}", propertyName, maxLength);

            if (!String.IsNullOrEmpty(value) && value.Length > maxLength)
            {
                // Add the message to the validation list
                AddValidationError(propertyName, newMessage);
                return false;
            }
            else
                return true;
        }

        public Boolean ValidateMinLength(string propertyName, string value, int minLength)
        {
            String newMessage = String.Empty;
            newMessage = String.Format("{0} has a minimum size of {1}", propertyName, minLength);

            if (!String.IsNullOrEmpty(value) && value.Length < minLength)
            {
                // Add the message to the validation list
                AddValidationError(propertyName, newMessage);
                return false;
            }
            else
                return true;
        }

        public Boolean ValidateZeroLength(string propertyName, string value)
        {
            String newMessage = String.Empty;
            newMessage = String.Format("{0} must not have a zero length", propertyName);

            if (String.IsNullOrEmpty(value) || value.Length == 0)
            {
                // Add the message to the validation list
                AddValidationError(propertyName, newMessage);
                return false;
            }
            else
                return true;
        }

        #endregion ValidateLength

        #region ValidateRequired

        public Boolean ValidateRequired(string propertyName, object value)
        {
            string newMessage = String.Empty;
            newMessage = String.Format("{0} is required", propertyName);

            if (String.IsNullOrEmpty(value.ToString()))
            {
                // Add the message to the validation list
                AddValidationError(propertyName, newMessage + " " + ", please enter a valid value");
                return false;
            }
            else
                return true;
        }

        #endregion ValidateRequired

        #region ValidateNull

        public Boolean ValidateNull(string propertyName, object value)
        {
            string newMessage = String.Empty;
            newMessage = String.Format("{0} is required", propertyName);

            if (DBNull.Value == value || value == null)
            {
                // Add the message to the validation list
                AddValidationError(propertyName, newMessage + " " + ", please enter a valid value");
                return false;
            }
            else
                return true;
        }

        #endregion ValidateNull

        #region ValidateString

        public Boolean ValidateString(string propertyName, int iMinLength, int iMaxLength, object value)
        {
            string newMessage = String.Empty;
            newMessage = String.Format("{0} must be a string with at least {1} characters and no more than {2}", propertyName, iMinLength, iMaxLength);

            string pattern = @"^[a-zA-Z]{" + iMinLength + "," + iMaxLength + "}$";
            bool result = Regex.IsMatch(value.ToString(), pattern, RegexOptions.IgnoreCase);
            if (result)
            {
                // Add the message to the validation list
                AddValidationError(propertyName, newMessage);
                return false;
            }
            else
                return true;
        }

        public Boolean ValidateZipcode(string propertyName, object value)
        {
            string newMessage = String.Empty;
            newMessage = String.Format("{0} must be 5 digit format ", propertyName);

            string pattern = @"(^\d{5}(-\d{4})?$)";
            bool result = Regex.IsMatch(value.ToString(), pattern, RegexOptions.IgnoreCase);

            //string strRegex = @"/^\d{5}([\-]\d{4})?$/";
            //Regex myRegex = new Regex(strRegex, RegexOptions.None);
            //string strTargetString = @"1234" + "\n";

            //foreach (Match myMatch in myRegex.Matches(strTargetString))
            //{
            //    if (myMatch.Success)
            //    {
            //        // Add your code here
            //    }
            //}


            if (!result)
            {
                // Add the message to the validation list
                AddValidationError(propertyName, newMessage);
                return false;
            }
            else
                return true;
        }



        public Boolean ValidatePhone(string propertyName, object value)
        {
            string newMessage = String.Empty;
            newMessage = String.Format("{0} must be in a (xxx)-xxx-xxxx format ", propertyName);

            string pattern = @"^((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}$";
            bool result = Regex.IsMatch(value.ToString(), pattern, RegexOptions.IgnoreCase);
            if (result)
            {
                // Add the message to the validation list
                AddValidationError(propertyName, newMessage);
                return false;
            }
            else
                return true;
        }

        public Boolean ValidateEmail(string propertyname, object value)
        {


            try
            {
                MailAddress m = new MailAddress(value.ToString());
                return true;
            }
            catch (FormatException)
            {
                return false;
            }


        }

        public Boolean ValidateUSPSStateAbbreviation(string propertyname, object value)
        {
            String states = "|AL|AK|AS|AZ|AR|CA|CO|CT|DE|DC|FM|FL|GA|GU|HI|ID|IL|IN|IA|KS|KY|LA|ME|MH|MD|MA|MI|MN|MS|MO|MT|NE|NV|NH|NJ|NM|NY|NC|ND|MP|OH|OK|OR|PW|PA|PR|RI|SC|SD|TN|TX|UT|VT|VI|VA|WA|WV|WI|WY|";
            return value.ToString().Length == 2 && states.IndexOf(value.ToString()) > 0;
        }

        public Boolean ValidateVehicleActivityType(string propertyName, object value)
        {

            string newMessage = String.Empty;
            newMessage = String.Format("{0} must be an accepted vehicle activity type ", propertyName);
            bool result = Enum.IsDefined(typeof(IVehicleActivityType), value);

            if (result)
            {
                // Add the message to the validation list
                AddValidationError(propertyName, newMessage);
                return false;
            }
            else
                return true;


        }


        public Boolean ValidateVehicleImages (string propertyName, object value)
        {
            string newMessage = String.Empty;

            newMessage = "This Customer Vehicle still has images stored ";
            bool result = Convert.ToBoolean(value);
            if (result)
            {
                // Add the message to the validation list
                AddValidationError(propertyName, newMessage);
                return false;
            }
            else
                return true;
        }

        public Boolean ValidateVehicleDropOff(string propertyName, object value)
        {
            string newMessage = String.Empty;

            newMessage = "This Customer Vehicle still has a Drop Off activity listed";
            bool result = Convert.ToBoolean(value);
            if (result)
            {
                // Add the message to the validation list
                AddValidationError(propertyName, newMessage);
                return false;
            }
            else
                return true;
        }

        public Boolean ValidateVehiclePickUp(string propertyName, object value)
        {
            string newMessage = String.Empty;

            newMessage = "This Customer Vehicle still has a Pick Up activity listed";
            bool result = Convert.ToBoolean(value);
            if (result)
            {
                // Add the message to the validation list
                AddValidationError(propertyName, newMessage);
                return false;
            }
            else
                return true;
        }

        #endregion ValidateString

        #region ValidateSpecialChars

        public Boolean ValidateDisallowSpecialChars(string propertyName, int iMinLength, int iMaxLength, object value)
        {
            string newMessage = String.Empty;
            newMessage = String.Format("{0} Numbers and special characters are not allowed ", propertyName);

            string pattern = @"^[a-zA-Z''-'\s]{" + iMinLength + "," + iMaxLength + "}$";
            bool result = Regex.IsMatch(value.ToString(), pattern, RegexOptions.IgnoreCase);
            if (result)
            {
                // Add the message to the validation list
                AddValidationError(propertyName, newMessage);
                return false;
            }
            else
                return true;
        }

        #endregion ValidateSpecialChars

        public int ValidateColorValue(string text)
        {
            int val = 0;
            int.TryParse(text, out val);
            if (255 < val)
            {
                val = 255;
            }
            return val;
        }

        #region AddValidationError

        /// <summary>
        /// Adds a validation error to the collection
        /// </summary>
        /// <param name="propertyName">Property with the error</param>
        /// <param name="message">Message displayed to the user</param>
        /// <remarks></remarks>
        private void AddValidationError(string propertyName, string message)
        {
            // If the property already has a message, add this message
            if (ValidationList.ContainsKey(propertyName))
            {
                string existingMessage = ValidationList[propertyName];

                if (!existingMessage.Contains(message))
                    // Append the new message to the existing message
                    ValidationList[propertyName] += "; " + message;
            }
            else
                // Add the message to the validation list
                ValidationList.Add(propertyName, message);
        }

        #endregion AddValidationError

        public object CheckDBNull(object obj, eObjectType ObjectType = eObjectType.strType)
        {
            object oReturn = null;
            oReturn = obj;
            if (ObjectType == eObjectType.strType && DBNull.Value.Equals(obj))
            {
                oReturn = "";
            }
            else if (ObjectType == eObjectType.intType && DBNull.Value.Equals(obj))
            {
                oReturn = 0;
            }
            else if (ObjectType == eObjectType.dblType && DBNull.Value.Equals(obj))
            {
                oReturn = 0.0;
            }
            else if (ObjectType == eObjectType.bteType && DBNull.Value.Equals(obj))
            {
                oReturn = "";
            }
            return oReturn;
        }

        #endregion Methods
    }

    //public class ValidatableModel : INotifyDataErrorInfo, INotifyPropertyChanged
    //{
    //    private ConcurrentDictionary<string, List<string>> _errors =
    //        new ConcurrentDictionary<string, List<string>>();

    //    public event PropertyChangedEventHandler PropertyChanged;

    //    public void RaisePropertyChanged(string propertyName)
    //    {
    //        var handler = PropertyChanged;
    //        if (handler != null)
    //            handler(this, new PropertyChangedEventArgs(propertyName));
    //        ValidateAsync();
    //    }

    //    public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

    //    public void OnErrorsChanged(string propertyName)
    //    {
    //        var handler = ErrorsChanged;
    //        if (handler != null)
    //            handler(this, new DataErrorsChangedEventArgs(propertyName));
    //    }

    //    public IEnumerable GetErrors(string propertyName)
    //    {
    //        List<string> errorsForName;
    //        _errors.TryGetValue(propertyName, out errorsForName);
    //        return errorsForName;
    //    }

    //    public bool HasErrors
    //    {
    //        get { return _errors.Any(kv => kv.Value != null && kv.Value.Count > 0); }
    //    }

    //    public Task ValidateAsync()
    //    {
    //        return Task.Run(() => Validate());
    //    }

    //    private object _lock = new object();
    //    public void Validate()
    //    {
    //        lock (_lock)
    //        {
    //            var validationContext = new ValidationContext(this, null, null);
    //            var validationResults = new List<ValidationResult>();
    //            Validator.TryValidateObject(this, validationContext, validationResults, true);

    //            foreach (var kv in _errors.ToList())
    //            {
    //                if (validationResults.All(r => r.MemberNames.All(m => m != kv.Key)))
    //                {
    //                    List<string> outLi;
    //                    _errors.TryRemove(kv.Key, out outLi);
    //                    OnErrorsChanged(kv.Key);
    //                }
    //            }

    //            var q = from r in validationResults
    //                    from m in r.MemberNames
    //                    group r by m into g
    //                    select g;

    //            foreach (var prop in q)
    //            {
    //                var messages = prop.Select(r => r.ErrorMessage).ToList();

    //                if (_errors.ContainsKey(prop.Key))
    //                {
    //                    List<string> outLi;
    //                    _errors.TryRemove(prop.Key, out outLi);
    //                }
    //                _errors.TryAdd(prop.Key, messages);
    //                OnErrorsChanged(prop.Key);
    //            }
    //        }
    //    }
    //}

    //#region newValidator

    //////var validationResult = DataAnnotation.ValidateEntity<Keyword>(mKeyword);
    // ////               btnOk.Enabled = !validationResult.HasError;
    //public class DataAnnotation
    //{
    //    public static EntityValidationResult ValidateEntity<T>(T entity) where T : BOBase
    //    {
    //        return new EntityValidator<T>().Validate(entity);
    //    }
    //}
    //public class EntityValidator<T> where T : BOBase
    //{
    //    public EntityValidationResult Validate(T entity)
    //    {
    //        var validationResults = new List<ValidationResult>();
    //        var vc = new ValidationContext(entity, null, null);
    //        var isValid = Validator.TryValidateObject(entity, vc, validationResults, true);

    //        return new EntityValidationResult(validationResults);
    //    }
    //}

    //public class EntityValidationResult
    //{
    //    public IList<ValidationResult> ValidationErrors { get; private set; }
    //    public bool HasError
    //    {
    //        get { return ValidationErrors.Count > 0; }
    //    }

    //    public EntityValidationResult(IList<ValidationResult> violations = null)
    //    {
    //        ValidationErrors = violations ?? new List<ValidationResult>();
    //    }
    //}

    //#endregion
}