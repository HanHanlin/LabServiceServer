using System;
using System.ComponentModel;
using System.Threading;
using System.Web;
using SubSonic;

// <auto-generated />

namespace Vietbait.Lablink.Model
{
    /// <summary>
    ///     Controller class for tbl_Reason
    /// </summary>
    [DataObject]
    public class TblReasonController
    {
        // Preload our schema..
        private TblReason thisSchemaLoad = new TblReason();
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
        public TblReasonCollection FetchAll()
        {
            var coll = new TblReasonCollection();
            var qry = new Query(TblReason.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public TblReasonCollection FetchByID(object Id)
        {
            TblReasonCollection coll = new TblReasonCollection().Where("ID", Id).Load();
            return coll;
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public TblReasonCollection FetchByQuery(Query qry)
        {
            var coll = new TblReasonCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }

        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object Id)
        {
            return (TblReason.Delete(Id) == 1);
        }

        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object Id)
        {
            return (TblReason.Destroy(Id) == 1);
        }


        /// <summary>
        ///     Inserts a record, can be used with the Object Data Source
        /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public void Insert(string SName, string SDesc, short IntSTT)
        {
            var item = new TblReason();

            item.SName = SName;

            item.SDesc = SDesc;

            item.IntSTT = IntSTT;


            item.Save(UserName);
        }

        /// <summary>
        ///     Updates a record, can be used with the Object Data Source
        /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
        public void Update(short Id, string SName, string SDesc, short IntSTT)
        {
            var item = new TblReason();
            item.MarkOld();
            item.IsLoaded = true;

            item.Id = Id;

            item.SName = SName;

            item.SDesc = SDesc;

            item.IntSTT = IntSTT;

            item.Save(UserName);
        }
    }
}