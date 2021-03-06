using System;
using System.ComponentModel;
using System.Threading;
using System.Web;
using SubSonic;

// <auto-generated />

namespace Vietbait.Lablink.Model
{
    /// <summary>
    ///     Controller class for T_PARA_LIST
    /// </summary>
    [DataObject]
    public class TParaListController
    {
        // Preload our schema..
        private TParaList thisSchemaLoad = new TParaList();
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
        public TParaListCollection FetchAll()
        {
            var coll = new TParaListCollection();
            var qry = new Query(TParaList.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public TParaListCollection FetchByID(object ParaId)
        {
            TParaListCollection coll = new TParaListCollection().Where("Para_ID", ParaId).Load();
            return coll;
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public TParaListCollection FetchByQuery(Query qry)
        {
            var coll = new TParaListCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }

        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object ParaId)
        {
            return (TParaList.Delete(ParaId) == 1);
        }

        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object ParaId)
        {
            return (TParaList.Destroy(ParaId) == 1);
        }


        /// <summary>
        ///     Inserts a record, can be used with the Object Data Source
        /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public void Insert(string ParaId, string ParaName, string MeasureUnit, string Description)
        {
            var item = new TParaList();

            item.ParaId = ParaId;

            item.ParaName = ParaName;

            item.MeasureUnit = MeasureUnit;

            item.Description = Description;


            item.Save(UserName);
        }

        /// <summary>
        ///     Updates a record, can be used with the Object Data Source
        /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
        public void Update(string ParaId, string ParaName, string MeasureUnit, string Description)
        {
            var item = new TParaList();
            item.MarkOld();
            item.IsLoaded = true;

            item.ParaId = ParaId;

            item.ParaName = ParaName;

            item.MeasureUnit = MeasureUnit;

            item.Description = Description;

            item.Save(UserName);
        }
    }
}