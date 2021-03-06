using System;
using System.ComponentModel;
using System.Data;
using System.Reflection;
using System.Threading;
using System.Web;
using System.Xml.Serialization;
using SubSonic;

// <auto-generated />

namespace Vietbait.Lablink.Model
{
    /// <summary>
    ///     Strongly-typed collection for the SysPrintColor class.
    /// </summary>
    [Serializable]
    public class SysPrintColorCollection : ActiveList<SysPrintColor, SysPrintColorCollection>
    {
        /// <summary>
        ///     Filters an existing collection based on the set criteria. This is an in-memory filter
        ///     Thanks to developingchris for this!
        /// </summary>
        /// <returns>SysPrintColorCollection</returns>
        public SysPrintColorCollection Filter()
        {
            for (int i = Count - 1; i > -1; i--)
            {
                SysPrintColor o = this[i];
                foreach (Where w in wheres)
                {
                    bool remove = false;
                    PropertyInfo pi = o.GetType().GetProperty(w.ColumnName);
                    if (pi.CanRead)
                    {
                        object val = pi.GetValue(o, null);
                        switch (w.Comparison)
                        {
                            case Comparison.Equals:
                                if (!val.Equals(w.ParameterValue))
                                {
                                    remove = true;
                                }
                                break;
                        }
                    }
                    if (remove)
                    {
                        Remove(o);
                        break;
                    }
                }
            }
            return this;
        }
    }

    /// <summary>
    ///     This is an ActiveRecord class which wraps the Sys_PrintColor table.
    /// </summary>
    [Serializable]
    public class SysPrintColor : ActiveRecord<SysPrintColor>, IActiveRecord
    {
        #region .ctors and Default Settings

        public SysPrintColor()
        {
            SetSQLProps();
            InitSetDefaults();
            MarkNew();
        }

        public SysPrintColor(bool useDatabaseDefaults)
        {
            SetSQLProps();
            if (useDatabaseDefaults)
                ForceDefaults();
            MarkNew();
        }

        public SysPrintColor(object keyID)
        {
            SetSQLProps();
            InitSetDefaults();
            LoadByKey(keyID);
        }

        public SysPrintColor(string columnName, object columnValue)
        {
            SetSQLProps();
            InitSetDefaults();
            LoadByParam(columnName, columnValue);
        }

        private void InitSetDefaults()
        {
            SetDefaults();
        }

        protected static void SetSQLProps()
        {
            GetTableSchema();
        }

        #endregion

        #region Schema and Query Accessor	

        public static TableSchema.Table Schema
        {
            get
            {
                if (BaseSchema == null)
                    SetSQLProps();
                return BaseSchema;
            }
        }

        public static Query CreateQuery()
        {
            return new Query(Schema);
        }

        private static void GetTableSchema()
        {
            if (!IsSchemaInitialized)
            {
                //Schema declaration
                var schema = new TableSchema.Table("Sys_PrintColor", TableType.Table, DataService.GetInstance("ORM"));
                schema.Columns = new TableSchema.TableColumnCollection();
                schema.SchemaName = @"dbo";
                //columns

                var colvarId = new TableSchema.TableColumn(schema);
                colvarId.ColumnName = "ID";
                colvarId.DataType = DbType.Int32;
                colvarId.MaxLength = 0;
                colvarId.AutoIncrement = true;
                colvarId.IsNullable = false;
                colvarId.IsPrimaryKey = true;
                colvarId.IsForeignKey = false;
                colvarId.IsReadOnly = false;
                colvarId.DefaultSetting = @"";
                colvarId.ForeignKeyTableName = "";
                schema.Columns.Add(colvarId);

                var colvarGroupName = new TableSchema.TableColumn(schema);
                colvarGroupName.ColumnName = "GroupName";
                colvarGroupName.DataType = DbType.String;
                colvarGroupName.MaxLength = 255;
                colvarGroupName.AutoIncrement = false;
                colvarGroupName.IsNullable = false;
                colvarGroupName.IsPrimaryKey = false;
                colvarGroupName.IsForeignKey = false;
                colvarGroupName.IsReadOnly = false;
                colvarGroupName.DefaultSetting = @"";
                colvarGroupName.ForeignKeyTableName = "";
                schema.Columns.Add(colvarGroupName);

                var colvarArgb = new TableSchema.TableColumn(schema);
                colvarArgb.ColumnName = "Argb";
                colvarArgb.DataType = DbType.Int32;
                colvarArgb.MaxLength = 0;
                colvarArgb.AutoIncrement = false;
                colvarArgb.IsNullable = false;
                colvarArgb.IsPrimaryKey = false;
                colvarArgb.IsForeignKey = false;
                colvarArgb.IsReadOnly = false;
                colvarArgb.DefaultSetting = @"";
                colvarArgb.ForeignKeyTableName = "";
                schema.Columns.Add(colvarArgb);

                BaseSchema = schema;
                //add this schema to the provider
                //so we can query it later
                DataService.Providers["ORM"].AddSchema("Sys_PrintColor", schema);
            }
        }

        #endregion

        #region Props

        [XmlAttribute("Id")]
        [Bindable(true)]
        public int Id
        {
            get { return GetColumnValue<int>(Columns.Id); }
            set { SetColumnValue(Columns.Id, value); }
        }

        [XmlAttribute("GroupName")]
        [Bindable(true)]
        public string GroupName
        {
            get { return GetColumnValue<string>(Columns.GroupName); }
            set { SetColumnValue(Columns.GroupName, value); }
        }

        [XmlAttribute("Argb")]
        [Bindable(true)]
        public int Argb
        {
            get { return GetColumnValue<int>(Columns.Argb); }
            set { SetColumnValue(Columns.Argb, value); }
        }

        #endregion

        #region ObjectDataSource support

        /// <summary>
        ///     Inserts a record, can be used with the Object Data Source
        /// </summary>
        public static void Insert(string varGroupName, int varArgb)
        {
            var item = new SysPrintColor();

            item.GroupName = varGroupName;

            item.Argb = varArgb;


            if (HttpContext.Current != null)
                item.Save(HttpContext.Current.User.Identity.Name);
            else
                item.Save(Thread.CurrentPrincipal.Identity.Name);
        }

        /// <summary>
        ///     Updates a record, can be used with the Object Data Source
        /// </summary>
        public static void Update(int varId, string varGroupName, int varArgb)
        {
            var item = new SysPrintColor();

            item.Id = varId;

            item.GroupName = varGroupName;

            item.Argb = varArgb;

            item.IsNew = false;
            if (HttpContext.Current != null)
                item.Save(HttpContext.Current.User.Identity.Name);
            else
                item.Save(Thread.CurrentPrincipal.Identity.Name);
        }

        #endregion

        #region Typed Columns

        public static TableSchema.TableColumn IdColumn
        {
            get { return Schema.Columns[0]; }
        }


        public static TableSchema.TableColumn GroupNameColumn
        {
            get { return Schema.Columns[1]; }
        }


        public static TableSchema.TableColumn ArgbColumn
        {
            get { return Schema.Columns[2]; }
        }

        #endregion

        #region Columns Struct

        public struct Columns
        {
            public static string Id = @"ID";
            public static string GroupName = @"GroupName";
            public static string Argb = @"Argb";
        }

        #endregion

        #region Update PK Collections

        #endregion

        #region Deep Save

        #endregion

        //no foreign key tables defined (0)


        //no ManyToMany tables defined (0)
    }
}