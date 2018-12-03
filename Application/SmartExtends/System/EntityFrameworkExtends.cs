using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using SmartExtends.System;

namespace System.Linq
{
    #region 对排序的扩展
    /// <summary>
    /// 对 IQueryable的排序扩展
    /// </summary>
    public static partial class EntityFrameworkExtendOrderBy
    {
        /// <summary>
        /// 排序
        /// </summary>
        /// <typeparam name="T">泛型类型</typeparam>
        /// <param name="source">要排序的数据源</param>
        /// <param name="sortExpression">排序字段</param>
        /// <param name="sortDirection">排序方式（ASC,DESC）</param>
        /// <returns>排序结果</returns>
        public static IQueryable<T> Sort<T>(this IQueryable<T> source, string sortExpression, string sortDirection)
        {
            string sortingDir = string.Empty;
            if (sortDirection.Trim().Compare("ASC", true) || sortDirection.IsNullOrEmptyOrBlank())
                sortingDir = "OrderBy";
            else if (sortDirection.Trim().Compare("DESC", true))
                sortingDir = "OrderByDescending";
            ParameterExpression param = Expression.Parameter(typeof(T), sortExpression);
            PropertyInfo pi = typeof(T).GetProperty(sortExpression);
            Type[] types = new Type[2];
            types[0] = typeof(T);
            types[1] = pi.PropertyType;
            Expression expr = Expression.Call(typeof(Queryable), sortingDir, types, source.Expression, Expression.Lambda(Expression.Property(param, sortExpression), param));
            IQueryable<T> query = source.Provider.CreateQuery<T>(expr);
            return query;
        }
    }
    #endregion

    //////    #region EntityFrameWork的扩展

    //////    /// <summary>
    //////    /// 对 DbContext 的扩展
    //////    /// </summary>
    //////    public static partial class DbContextExtend
    //////    {
    //////        static bool IsEfReleaseWriteLog = false;
    //////        static DbContextExtend()
    //////        {
    //////            var isEnableMiniProfilter = ConfigurationManager.AppSettings["IsEfReleaseWriteLog"];
    //////            if (isEnableMiniProfilter.IsNotNullOrEmptyOrBlank() && isEnableMiniProfilter.ToLower() == "true")
    //////                IsEfReleaseWriteLog = true;
    //////        }

    //////        /// <summary>
    //////        /// 打印 EntityFramework 执行的日志
    //////        /// </summary>
    //////        /// <param name="dbContext">要打印日志的DbContext</param>
    //////        /// <param name="releaseDebug">默认为false，如果设置为true，并且传入了自定义的action
    //////        /// 则会在发布之后打印到传入的方法</param>
    //////        /// <param name="action">默认打印到输出窗口，可以自己传入参数为string的方法自己处理</param>
    //////        public static void WriteScript(this DbContext dbContext, bool releaseDebug = false, Action<string> action = null)
    //////        {
    //////#if DEBUG
    //////            if (action == null)
    //////                dbContext.Database.Log = s => Debug.Print(s);
    //////            else
    //////                dbContext.Database.Log = action;
    //////#endif
    //////            if (releaseDebug)
    //////            {
    //////                if (action == null)
    //////                    dbContext.Database.Log = s => Debug.Print(s);
    //////                else
    //////                    dbContext.Database.Log = action;
    //////            }
    //////        }
    //////    }

    //////    #endregion

    #region 对 集合的扩展，可以将集合转换为 DataTable

    /// <summary>
    /// 转换成DataTable中的数据类型
    /// </summary>
    public static partial class EntityFrameworkExtendToTable
    {
        public static Type ToDbType(this Type fromType)
        {
            Type toType = typeof(string);
            if (fromType == typeof(System.DateTime) || fromType == typeof(Nullable<DateTime>) || fromType == typeof(DateTime?))
                toType = typeof(System.DateTime);
            else
                if (fromType == typeof(Boolean) || fromType == typeof(Nullable<Boolean>) || fromType == typeof(Boolean?))
                    toType = typeof(Boolean);
                else
                    if (fromType == typeof(Int32) || fromType == typeof(Nullable<Int32>) || fromType == typeof(Int32?))
                        toType = typeof(Int32);
                    else
                        if (fromType == typeof(Int64) || fromType == typeof(Nullable<Int64>) || fromType == typeof(Int64?))
                            toType = typeof(Int64);
                        else
                            if (fromType == typeof(Decimal) || fromType == typeof(Nullable<Decimal>) || fromType == typeof(Decimal?))
                                toType = typeof(Decimal);
                            else
                                if (fromType == typeof(Byte[]))
                                    toType = typeof(Byte[]);
            return toType;
        }
    }

    /// <summary>
    /// 将列表转换为 DataTable
    /// </summary>
    public static partial class EntityFrameworkExtendToTable
    {
        /// <summary>
        /// 转换为一个DataTable
        /// </summary>
        /// <typeparam name="T">泛型类型</typeparam>
        /// <param name="value">泛型对象</param>
        /// <returns>转换后的DataTable</returns>
        public static DataTable ToDataTable<T>(this IQueryable<T> value) where T : class, new()
        {
            return value.ToList().ToDataTable();
        }

        /// <summary>
        /// 转换为一个DataTable
        /// </summary>
        /// <typeparam name="T">泛型类型</typeparam>
        /// <param name="value">泛型列表</param>
        /// <returns>转换后的DataTable</returns>
        public static DataTable ToDataTable<T>(this IEnumerable<T> value) where T : class, new()
        {
            return value.ToList().ToDataTable();
        }

        /// <summary>
        /// 转换为一个DataTable
        /// </summary>
        /// <typeparam name="T">泛型类型</typeparam>
        /// <param name="value">泛型列表</param>
        /// <returns>转换后的DataTable</returns>
        public static DataTable ToDataTable<T>(this IList<T> value) where T : class, new()
        {
            ////创建属性的集合
            List<PropertyInfo> plist = new List<PropertyInfo>();
            ////获得反射的入口
            Type type = typeof(T);
            DataTable dt = new DataTable();
            if (dicType.ContainsKey(type))
            {
                dt = dicType[type].Table.Clone();
                plist = dicType[type].PList;
            }
            else
            {
                ////把所有的public属性加入到集合 并添加DataTable的列

                Array.ForEach<PropertyInfo>(
                    type.GetProperties(),
                    p =>
                    {
                        ////if (!p.PropertyType.IsArray && !p.PropertyType.IsEnum && (p.PropertyType == typeof(string) || !p.PropertyType.IsClass))
                        if (p.PropertyType.IsSealed)
                        {
                            plist.Add(p);
                            dt.Columns.Add(p.Name, p.PropertyType.ToDbType());
                        }
                    });
                dicType.Add(type, new ObjectToTable() { PList = plist, Table = dt.Clone() });
            }
            ////创建一个DataRow实例
            DataRow row = dt.NewRow();
            foreach (var item in value)
            {
                ////给row 赋值
                plist.ForEach(p => row[p.Name] = p.GetValue(item, null) ?? DBNull.Value);
                ////加入到DataTable
                dt.Rows.Add(row);
                row = dt.NewRow();
            }

            return dt;
        }

        private static Dictionary<Type, ObjectToTable> dicType = new Dictionary<Type, ObjectToTable>();
    }

    #endregion

    #region 对 DbSet 的扩展

    /// <summary>
    /// 对 EntityFrameworkDbSet的扩展
    /// </summary>
    public static partial class EntityFrameworkExtendDbSet
    {
        /// <summary>
        /// 按照条件从数据库删除
        /// </summary>
        /// <typeparam name="T">泛型类型</typeparam>
        /// <param name="soruces">源对象</param>
        /// <param name="direction">条件</param>
        public static void Remove<T>(this DbSet<T> soruces, Expression<Func<T, bool>> direction) where T : class, new()
        {
            var query = soruces.Where(direction);
            foreach (var item in query)
            {
                soruces.Remove(item);
            }
        }

        /// <summary>
        /// DbSet的添加方法
        /// </summary>
        /// <typeparam name="T">泛型类型</typeparam>
        /// <param name="soruces">数据源</param>
        /// <param name="input">添加的对象</param>
        public static void Add<T>(this DbSet<T> soruces, ICollection<T> input) where T : class, new()
        {
            foreach (var item in input.ToList())
            {
                soruces.Add(item);
            }
        }
        static EntityFrameworkExtendDbSet()
        {
            Authority a = new Authority();
        }
        /// <summary>
        /// 按照条件从数据库删除
        /// </summary>
        /// <typeparam name="T">泛型类型</typeparam>
        /// <param name="soruces">源</param>
        /// <param name="direction">条件</param>
        public static void Delete<T>(this DbSet<T> soruces, Expression<Func<T, bool>> direction) where T : class, new()
        {
            Remove<T>(soruces, direction);
        }
    }
    #endregion

    #region 对 where 的扩展，增加了 EntityFramework 的 and 和 or 方法

    /// <summary>
    /// 对 Expression 的扩展
    /// </summary>
    public static partial class EntityFrameworkExtendAndOr
    {
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second) where T : class, new()
        {
            return first.Compose(second, Expression.AndAlso);
            ////var invokedExpression = Expression.Invoke(second, first.Parameters.Cast<Expression>());
            ////return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(first.Body, invokedExpression), second.Parameters);
        }
        static EntityFrameworkExtendAndOr()
        {
            Authority a = new Authority();
        }
        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second) where T : class, new()
        {
            return first.Compose(second, Expression.OrElse);
            ////var invokedExpression = Expression.Invoke(second, first.Parameters.Cast<Expression>());
            ////return Expression.Lambda<Func<T, bool>>(Expression.OrElse(first.Body, invokedExpression), second.Parameters);
        }

        internal static Expression<T> Compose<T>(this Expression<T> first, Expression<T> second, Func<Expression, Expression, Expression> merge)
        {
            //// build parameter map (from parameters of second to parameters of first)
            var map = first.Parameters.Select((f, i) => new { f, s = second.Parameters[i] }).ToDictionary(p => p.s, p => p.f);

            //// replace parameters in the second lambda expression with parameters from the first
            var secondBody = ParameterRebinder.ReplaceParameters(map, second.Body);

            //// apply composition of lambda expression bodies to parameters from the first expression 

            return Expression.Lambda<T>(merge(first.Body, secondBody), first.Parameters);
        }
    }

    public class ParameterRebinder : ExpressionVisitor
    {
        private readonly Dictionary<ParameterExpression, ParameterExpression> map;

        public ParameterRebinder(Dictionary<ParameterExpression, ParameterExpression> map)
        {
            this.map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();
        }

        public static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> map, Expression exp)
        {
            return new ParameterRebinder(map).Visit(exp);
        }

        protected override Expression VisitParameter(ParameterExpression p)
        {
            ParameterExpression replacement;
            if (this.map.TryGetValue(p, out replacement))
            {
                p = replacement;
            }

            return base.VisitParameter(p);
        }
    }
    #endregion

    #region 对排序的扩展

    public static partial class EntityFrameworkExtendOrderBy
    {
        /// <summary>
        /// 排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="sortProperty"></param>
        /// <param name="sortDirection"></param>
        /// <returns></returns>
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string sortProperty, string sortDirection)
        {
            string sortingDir = string.Empty;
            if (sortDirection.Trim().Compare("ASC", true) || sortDirection.IsNullOrEmptyOrBlank())
                sortingDir = "OrderBy";
            else if (sortDirection.Trim().Compare("DESC", true))
                sortingDir = "OrderByDescending";
            ParameterExpression param = Expression.Parameter(typeof(T), sortProperty);
            PropertyInfo pi = typeof(T).GetProperty(sortProperty);
            Type[] types = new Type[2];
            types[0] = typeof(T);
            types[1] = pi.PropertyType;
            Expression expr = Expression.Call(typeof(Queryable), sortingDir, types, source.Expression, Expression.Lambda(Expression.Property(param, sortProperty), param));
            IQueryable<T> query = source.Provider.CreateQuery<T>(expr);
            return query;
        }

        static EntityFrameworkExtendOrderBy()
        {
            Authority a = new Authority();
        }

        /// <summary>
        /// 多列排序
        /// </summary>
        /// <typeparam name="T">泛型类型</typeparam>
        /// <param name="source">iqueryable</param>
        /// <param name="orderByDic">排序字典，key值必须在 T 的属性中</param>
        /// <returns></returns>
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, Dictionary<string, string> orderByDic)
        {
            foreach (var item in orderByDic)
            {
                source = source.OrderBy(item.Key, item.Value);
            }
            return source;
        }
    }
    #endregion

    /// <summary>
    /// 缓存ObjectList转换为Table的实体
    /// </summary>
    internal class ObjectToTable
    {
        internal List<PropertyInfo> PList;
        internal DataTable Table;
    }
}
