using System;
using System.ComponentModel;
using System.Threading;
using System.Web;
using SubSonic;

// <auto-generated />

namespace Vietbait.Lablink.Model
{
    /// <summary>
    ///     Controller class for T_Evolis_Result
    /// </summary>
    [DataObject]
    public class TEvolisResultController
    {
        // Preload our schema..
        private TEvolisResult thisSchemaLoad = new TEvolisResult();
        private string userName = String.Empty;

        protected string UserName
        {
            get
            {
                if (userName.Length == 0)
                {
                    if (HttpContext.Current != null)
                    {
                        userName = HttpContext.Current.User.Identity.Name;
                    }
                    else
                    {
                        userName = Thread.CurrentPrincipal.Identity.Name;
                    }
                }
                return userName;
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public TEvolisResultCollection FetchAll()
        {
            var coll = new TEvolisResultCollection();
            var qry = new Query(TEvolisResult.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public TEvolisResultCollection FetchByID(object Id)
        {
            TEvolisResultCollection coll = new TEvolisResultCollection().Where("ID", Id).Load();
            return coll;
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public TEvolisResultCollection FetchByQuery(Query qry)
        {
            var coll = new TEvolisResultCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }

        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object Id)
        {
            return (TEvolisResult.Delete(Id) == 1);
        }

        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object Id)
        {
            return (TEvolisResult.Destroy(Id) == 1);
        }


        /// <summary>
        ///     Inserts a record, can be used with the Object Data Source
        /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public void Insert(long DetailID, string PatientID, string Barcode, string TestName, string Well, string Flag,
            string ValueX, string Sco, string Result, int? TestNumber, int? ReasonId, string BarcodeRerun)
        {
            var item = new TEvolisResult();

            item.DetailID = DetailID;

            item.PatientID = PatientID;

            item.Barcode = Barcode;

            item.TestName = TestName;

            item.Well = Well;

            item.Flag = Flag;

            item.ValueX = ValueX;

            item.Sco = Sco;

            item.Result = Result;

            item.TestNumber = TestNumber;

            item.ReasonId = ReasonId;

            item.BarcodeRerun = BarcodeRerun;


            item.Save(UserName);
        }

        /// <summary>
        ///     Updates a record, can be used with the Object Data Source
        /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
        public void Update(long Id, long DetailID, string PatientID, string Barcode, string TestName, string Well,
            string Flag, string ValueX, string Sco, string Result, int? TestNumber, int? ReasonId,
            string BarcodeRerun)
        {
            var item = new TEvolisResult();
            item.MarkOld();
            item.IsLoaded = true;

            item.Id = Id;

            item.DetailID = DetailID;

            item.PatientID = PatientID;

            item.Barcode = Barcode;

            item.TestName = TestName;

            item.Well = Well;

            item.Flag = Flag;

            item.ValueX = ValueX;

            item.Sco = Sco;

            item.Result = Result;

            item.TestNumber = TestNumber;

            item.ReasonId = ReasonId;

            item.BarcodeRerun = BarcodeRerun;

            item.Save(UserName);
        }
    }
}