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
    ///     Strongly-typed collection for the LDepartmentRoom class.
    /// </summary>
    [Serializable]
    public class LDepartmentRoomCollection : ActiveList<LDepartmentRoom, LDepartmentRoomCollection>
    {
        /// <summary>
        ///     Filters an existing collection based on the set criteria. This is an in-memory filter
        ///     Thanks to developingchris for this!
        /// </summary>
        /// <returns>LDepartmentRoomCollection</returns>
        public LDepartmentRoomCollection Filter()
        {
            for (int i = Count - 1; i > -1; i--)
            {
                LDepartmentRoom o = this[i];
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
    ///     This is an ActiveRecord class which wraps the L_Department_Room table.
    /// </summary>
    [Serializable]
    public class LDepartmentRoom : ActiveRecord<LDepartmentRoom>, IActiveRecord
    {
        #region .ctors and Default Settings

        public LDepartmentRoom()
        {
            SetSQLProps();
            InitSetDefaults();
            MarkNew();
        }

        public LDepartmentRoom(bool useDatabaseDefaults)
        {
            SetSQLProps();
            if (useDatabaseDefaults)
                ForceDefaults();
            MarkNew();
        }

        public LDepartmentRoom(object keyID)
        {
            SetSQLProps();
            InitSetDefaults();
            LoadByKey(keyID);
        }

        public LDepartmentRoom(string columnName, object columnValue)
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
                var schema = new TableSchema.Table("L_Department_Room", TableType.Table, DataService.GetInstance("ORM"));
                schema.Columns = new TableSchema.TableColumnCollection();
                schema.SchemaName = @"dbo";
                //columns

                var colvarDepartmentID = new TableSchema.TableColumn(schema);
                colvarDepartmentID.ColumnName = "DepartmentID";
                colvarDepartmentID.DataType = DbType.Int16;
                colvarDepartmentID.MaxLength = 0;
                colvarDepartmentID.AutoIncrement = false;
                colvarDepartmentID.IsNullable = false;
                colvarDepartmentID.IsPrimaryKey = true;
                colvarDepartmentID.IsForeignKey = false;
                colvarDepartmentID.IsReadOnly = false;
                colvarDepartmentID.DefaultSetting = @"";
                colvarDepartmentID.ForeignKeyTableName = "";
                schema.Columns.Add(colvarDepartmentID);

                var colvarRoomID = new TableSchema.TableColumn(schema);
                colvarRoomID.ColumnName = "RoomID";
                colvarRoomID.DataType = DbType.Int16;
                colvarRoomID.MaxLength = 0;
                colvarRoomID.AutoIncrement = false;
                colvarRoomID.IsNullable = false;
                colvarRoomID.IsPrimaryKey = true;
                colvarRoomID.IsForeignKey = false;
                colvarRoomID.IsReadOnly = false;
                colvarRoomID.DefaultSetting = @"";
                colvarRoomID.ForeignKeyTableName = "";
                schema.Columns.Add(colvarRoomID);

                BaseSchema = schema;
                //add this schema to the provider
                //so we can query it later
                DataService.Providers["ORM"].AddSchema("L_Department_Room", schema);
            }
        }

        #endregion

        #region Props

        [XmlAttribute("DepartmentID")]
        [Bindable(true)]
        public short DepartmentID
        {
            get { return GetColumnValue<short>(Columns.DepartmentID); }
            set { SetColumnValue(Columns.DepartmentID, value); }
        }

        [XmlAttribute("RoomID")]
        [Bindable(true)]
        public short RoomID
        {
            get { return GetColumnValue<short>(Columns.RoomID); }
            set { SetColumnValue(Columns.RoomID, value); }
        }

        #endregion

        #region ObjectDataSource support

        /// <summary>
        ///     Inserts a record, can be used with the Object Data Source
        /// </summary>
        public static void Insert(short varDepartmentID, short varRoomID)
        {
            var item = new LDepartmentRoom();

            item.DepartmentID = varDepartmentID;

            item.RoomID = varRoomID;


            if (HttpContext.Current != null)
                item.Save(HttpContext.Current.User.Identity.Name);
            else
                item.Save(Thread.CurrentPrincipal.Identity.Name);
        }

        /// <summary>
        ///     Updates a record, can be used with the Object Data Source
        /// </summary>
        public static void Update(short varDepartmentID, short varRoomID)
        {
            var item = new LDepartmentRoom();

            item.DepartmentID = varDepartmentID;

            item.RoomID = varRoomID;

            item.IsNew = false;
            if (HttpContext.Current != null)
                item.Save(HttpContext.Current.User.Identity.Name);
            else
                item.Save(Thread.CurrentPrincipal.Identity.Name);
        }

        #endregion

        #region Typed Columns

        public static TableSchema.TableColumn DepartmentIDColumn
        {
            get { return Schema.Columns[0]; }
        }


        public static TableSchema.TableColumn RoomIDColumn
        {
            get { return Schema.Columns[1]; }
        }

        #endregion

        #region Columns Struct

        public struct Columns
        {
            public static string DepartmentID = @"DepartmentID";
            public static string RoomID = @"RoomID";
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