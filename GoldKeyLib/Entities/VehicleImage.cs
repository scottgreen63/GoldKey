using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using GoldKeyLib.Interfaces;
using GoldKeyLib.Entities;
using GoldKeyLib.DA;

namespace GoldKeyLib.Entities
{
    [Serializable]
    public class VehicleImage : BaseEntity, IVehicleImage
    {

        #region Data

        //[tbld_VehicleImages]
	    //[fldc_Vehicle_ID]         [nchar](10) NULL,
	    //[fldd_DateTimeStamp]      [datetime] NULL,
	    //[fldc_ImagePath]          [nchar](500) NULL,
	    //[fldc_ImageCaption]       [nchar](30) NULL

        //'Database Field Names
        /// <summary>
        /// Const used to identify DA Field Name
        /// </summary>
        private const string cn_fVehicleID = "fldc_Vehicle_ID";

        /// <summary>
        /// Const used to identify DA Field Name
        /// </summary>
        private const string cn_fDateTimeStamp = "fldd_DateTimeStamp";

        ///// <summary>
        ///// Const used to identify DA Field Name
        ///// </summary>
        private const string cn_fImagePath = "fldc_ImagePath";

        /// <summary>
        /// Const used to identify DA Field Name
        /// </summary>
        private const string cn_fImageCaption = "fldc_ImageCaption";


        //'StoredProcedure Names
        /// <summary>
        /// Const used to identify DA Stored Proc Name
        /// </summary>
        private const string cn_spAdd = "pr_AddVehicleImage";

        /// <summary>
        /// Const used to identify DA Stored Proc Name
        /// </summary>
        private const string cn_spModify = "pr_ModifyVehicle";

        /// <summary>
        /// Const used to identify DA Stored Proc Name
        /// </summary>
        private const string cn_spDelete = "pr_DeleteVehicleImage";

        /// <summary>
        /// Const used to identify DA Stored Proc Name
        /// </summary>
        private const string cn_spGet = "pr_GetVehicleImage";

        //'Stored Proc Parameters'
        /// <summary>
        /// private var used for DA Stored Proc variable
        /// </summary>
        private const string cn_pmUserID = "@pUserId";

        //'Stored Proc Parameters'
        /// <summary>
        /// private var used for DA Stored Proc variable
        /// </summary>
        private const string cn_pmNewVehicleID = "@pNewVehicleImageID";

        /// <summary>
        /// private var used for DA Stored Proc variable
        /// </summary>
        private const string cn_pmVehicleID = "@pVehicleID";

        ///// <summary>
        ///// private var used for DA Stored Proc variable
        ///// </summary>
        private const string cn_pmDateTimeStamp = "@pDateTimeStamp";

        /// <summary>
        /// private var used for DA Stored Proc variable
        /// </summary>
        private const string cn_pmImagePath = "@pImagePath";

        /// <summary>
        /// private var used for DA Stored Proc variable
        /// </summary>
        private const string cn_pmImageCaption = "@pImageCaption";


        #endregion Data

        #region Private Attributes
        // private backing fields
        /// <summary>
        private int _userid = 0;
        /// private backing field
        /// </summary>
        private string _vehicleid;
        ///// <summary>
        ///// private backing field
        ///// </summary>
        private DateTime _datetimestamp;
        /// <summary>
        /// private backing field
        /// </summary>
        private string _imagepath;
        /// <summary>
        /// private backing field
        /// </summary>
        private string _imagecaption;
      

        //'Property Names'
        /// <summary>
        /// Const used to identify Entity Property Name
        /// </summary>
        private const string cn_pnVehicleID = "VehicleID";
        ///// <summary>
        ///// Const used to identify BO Property Name
        ///// </summary>
        private const string cn_pnDateTimeStamp = "DateTimeStamp";
        /// <summary>
        /// Const used to identify BO Property Name
        /// </summary>
        private const string cn_pnImagePath = "ImagePath";
        /// <summary>
        /// Const used to identify BO Property Name
        /// </summary>
        private const string cn_pnImageCaption = "ImageCaption";
   
       
        #endregion Private Attributes

        #region Properties

        //VehicleID
        /// <summary>
        /// BO Property
        /// </summary>
        [DisplayName(cn_pnVehicleID)]
        [Required()]
        public string VehicleID
        {
            get
            { return _vehicleid; }
            set
            {
                if (_vehicleid != value)
                {
                    this._vehicleid = value;
                    this.SetEntityState(EntityStateType.Modified, cn_pnVehicleID);
                }
            }
        }

        //DateTimeStamp
        /// <summary>
        /// BO Property
        /// </summary>
        [DisplayName(cn_pnDateTimeStamp)]
        [Required()]
        public DateTime DateTimeStamp
        {
            get
            { return _datetimestamp; }
            set
            {
                if (_datetimestamp != value)
                {
                    this._datetimestamp = value;
                    this.SetEntityState(EntityStateType.Modified, cn_pnDateTimeStamp);
                }
            }
        }

        //ImagePath
        /// <summary>
        /// BO Property
        /// </summary>
        [DisplayName(cn_pnImagePath)]
        [Required()]
        public string ImagePath
        {
            get
            { return _imagepath; }
            set
            {
                if (_imagepath != value)
                {
                    this._imagepath = value;
                    this.SetEntityState(EntityStateType.Modified, cn_pnImagePath);
                }
            }
        }

        //ImageCaption
        /// <summary>
        /// BO Property
        /// </summary>
        [DisplayName(cn_pnImageCaption)]
        [Required()]
        public string ImageCaption
        {
            get
            { return _imagecaption; }
            set
            {
                if (_imagecaption != value)
                {
                    this._imagecaption = value;
                    this.SetEntityState(EntityStateType.Modified, cn_pnImageCaption);
                }
            }
        }


        #endregion Properties

        #region Constructors

        /// <summary>
        /// Constructor ()
        /// </summary>
        public VehicleImage()
        {
            //this.Validator = new Validation();
            this.EntityState = EntityStateType.Added;
        }

        /// <summary>
        /// Constructor ()
        /// </summary>
        public VehicleImage(int UserId,string VehicleId)
        {
            //this.Validator = new Validation();
            _userid = UserId;
            _vehicleid = VehicleId;
            this.EntityState = EntityStateType.Added;
        }

        /// <summary>
        /// Constructor (DataRow)
        /// </summary>
        public VehicleImage(DataRow dr)
        {
            this.Populate(dr);
        }

        /// <summary>
        /// Private method used to populate new BO
        /// </summary>
        private void Populate(DataRow dr)
        {
            this._vehicleid = dr[cn_fVehicleID].ToString();
            this._datetimestamp = Convert.ToDateTime(dr[cn_fDateTimeStamp].ToString());
            this._imagepath = dr[cn_fImagePath].ToString();
            this._imagecaption = dr[cn_fImageCaption].ToString();
           
            // Reset the state to unchanged
            this.SetEntityState(EntityStateType.Unchanged);
        }

        #endregion Constructors

        #region Methods
        public void AddVehicleImage(int UserId)
        {
            _userid = UserId;

            if ((_vehicleid != "0") && (this.Validator.Count == 0))
            {
                DABase.Instance.ExecSP(cn_spAdd,
                DABase.Instance.Parameter(cn_pmUserID, _userid),
                DABase.Instance.Parameter(cn_pmVehicleID, _vehicleid),
                DABase.Instance.Parameter(cn_pmImagePath, _imagepath),
                DABase.Instance.Parameter(cn_pmImageCaption, _imagecaption),
                DABase.Instance.Parameter(cn_pmNewVehicleID, _vehicleid));

                this.SetEntityState(EntityStateType.Added);
                //Vehicles.ListVehicles(_userid);
            }
            else
            {
                throw new System.Exception(Validator.ToString());
                ///MessageBox.Show(ValidationInstance.ToString());
            }

        }

        //public void ModifyVehicle(int UserId)
        //{
        //    _userid = UserId;

        //    if ((_customerid != "0") && (Validator.Count == 0))
        //    {
        //        try
        //        {
        //            DABase.Instance.ExecSP(cn_spModify,
        //            DABase.Instance.Parameter(cn_pmUserID, _userid),
        //            DABase.Instance.Parameter(cn_pmVehicleID, _vehicleid),
        //            DABase.Instance.Parameter(cn_pmCustomerID, _customerid),
        //            DABase.Instance.Parameter(cn_pmYear, _year),
        //            DABase.Instance.Parameter(cn_pmMake, _make),
        //            DABase.Instance.Parameter(cn_pmModel, _model),
        //            DABase.Instance.Parameter(cn_pmColor, _color),
        //            DABase.Instance.Parameter(cn_pmTrim, _trim),
        //            DABase.Instance.Parameter(cn_pmCondition, _condition),
        //            DABase.Instance.Parameter(cn_pmMileage, _mileage),
        //            DABase.Instance.Parameter(cn_pmVINumber, _vinumber),
        //            DABase.Instance.Parameter(cn_pmNote, _note),
        //            DABase.Instance.Parameter(cn_pmNewVehicleID, _vehicleid));

        //            this.SetEntityState(EntityStateType.Modified);
        //            Vehicles.Listing.List(_userid);
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception(ex.Message, ex.InnerException);
        //        }
        //    }
        //    else
        //        throw new Exception(Validator.ToString());


        //}

        public void DeleteVehicleImage(int UserId)
        {
            if (_vehicleid != "0")
            {
                DABase.Instance.ExecSP(cn_spDelete,
                DABase.Instance.Parameter(cn_pmUserID, _userid),
                DABase.Instance.Parameter(cn_pmVehicleID, _vehicleid),
                DABase.Instance.Parameter(cn_pmImagePath, _imagepath),
                DABase.Instance.Parameter(cn_pmImageCaption, _imagecaption)
                );

                this.SetEntityState(EntityStateType.Deleted);
                Vehicles.ListVehicles(_userid);
            }
        }

        public VehicleImage GetVehicleImage(int UserID, string VehicleID, string ImagePath, string ImageCaption)
        {
            _userid = UserID;
            _vehicleid = VehicleID;
            _imagepath = ImagePath;
            _imagecaption = ImageCaption;
            DataTable dt;

            dt = DABase.Instance.ExecSP(cn_spGet,
                 DABase.Instance.Parameter(cn_pmUserID, _userid),
                 DABase.Instance.Parameter(cn_pmVehicleID, _vehicleid),
                 DABase.Instance.Parameter(cn_pmImagePath, _imagepath),
                 DABase.Instance.Parameter(cn_pmImageCaption, _imagecaption));

            VehicleImage vehicleimage = new VehicleImage(_userid, _vehicleid);

            foreach (DataRow dr in dt.Rows)
            {
                vehicleimage = new VehicleImage(dr);
            }
            return vehicleimage;
        }


        #endregion Methods
    }
}
