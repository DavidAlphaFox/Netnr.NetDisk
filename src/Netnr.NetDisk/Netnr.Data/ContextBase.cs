using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Netnr.Domain;
using System;

namespace Netnr.Data
{
    public class ContextBase : DbContext
    {
        /// <summary>
        /// 数据库
        /// </summary>
        public enum TypeDB
        {
            MySQL,
            SQLite,
            SQLServer,
            PostgreSQL
        }

        /// <summary>
        /// 数据库
        /// </summary>
        private TypeDB TDB;

        private static ILoggerFactory _loggerFactory = null;
        public static ILoggerFactory LoggerFactory
        {
            get
            {
                if (_loggerFactory == null)
                {
                    var sc = new ServiceCollection();
                    sc.AddLogging(builder => builder.AddConsole().AddFilter(level => level >= LogLevel.Warning));
                    _loggerFactory = sc.BuildServiceProvider().GetService<ILoggerFactory>();
                }
                return _loggerFactory;
            }
        }

        /// <summary>
        /// 配置连接字符串
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var conn = string.Empty;
            foreach (TypeDB item in Enum.GetValues(typeof(TypeDB)))
            {
                conn = GlobalTo.GetValue("conn:" + item.ToString().ToLower());
                if (!string.IsNullOrWhiteSpace(conn))
                {
                    conn = conn.Replace("~", GlobalTo.ContentRootPath);
                    TDB = item;
                    break;
                }
            }

            if (!string.IsNullOrWhiteSpace(conn))
            {
                switch (TDB)
                {
                    case TypeDB.MySQL:
                        optionsBuilder.UseMySql(conn);
                        break;
                    case TypeDB.SQLite:
                        optionsBuilder.UseSqlite(conn);
                        break;
                    case TypeDB.SQLServer:
                        optionsBuilder.UseSqlServer(conn, options =>
                        {
                            //启用 row_number 分页 （兼容2005、2008）
                            //options.UseRowNumberForPaging();
                        });
                        break;
                    case TypeDB.PostgreSQL:
                        optionsBuilder.UseNpgsql(conn);
                        break;
                }
            }

            //注册日志（修改日志等级为Information，可查看执行的SQL语句）
            optionsBuilder.UseLoggerFactory(LoggerFactory);
        }

        public virtual DbSet<FileRecord> FileRecord { get; set; }
        public virtual DbSet<FileSetting> FileSetting { get; set; }
        public virtual DbSet<UserInfo> UserInfo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FileRecord>(entity =>
            {
                entity.HasKey(e => e.FileId);
                entity.Property(e => e.FileId).HasMaxLength(50);
                entity.HasIndex(e => e.Uid);
                entity.Property(e => e.FileName).HasMaxLength(200);
                entity.Property(e => e.FilePath).HasMaxLength(200);
                entity.Property(e => e.FileCreateTime).HasColumnType("datetime");
                entity.Property(e => e.FileType).HasMaxLength(50);
                entity.Property(e => e.FileHash).HasMaxLength(50);
                entity.Property(e => e.FileTag).HasMaxLength(200);
                entity.Property(e => e.FileRmeark).HasMaxLength(200);
            });

            modelBuilder.Entity<FileSetting>(entity =>
            {
                entity.HasKey(e => e.FsId);
                entity.Property(e => e.FsId).HasMaxLength(50);
                entity.HasIndex(e => e.Uid);
                entity.Property(e => e.FsType).HasMaxLength(50);
                entity.Property(e => e.FsValue).HasMaxLength(50);
                entity.Property(e => e.FsPermission).HasMaxLength(50);
                entity.Property(e => e.FsCreateTime).HasColumnType("datetime");
                entity.Property(e => e.FsRmeark).HasMaxLength(200);
            });

            modelBuilder.Entity<UserToken>(entity =>
            {
                entity.HasKey(e => e.UtAppId);
                entity.Property(e => e.UtAppId).HasMaxLength(50);
                entity.Property(e => e.UtAppKey).HasMaxLength(50);
                entity.Property(e => e.UtToken).HasMaxLength(50);
                entity.Property(e => e.UtTokenTime).HasColumnType("datetime");
                entity.Property(e => e.UtPermission).HasMaxLength(500);
                entity.Property(e => e.UtCreateTime).HasColumnType("datetime");
                entity.Property(e => e.UtRemark).HasMaxLength(200);
            });

            modelBuilder.Entity<UserInfo>(entity =>
            {
                entity.HasKey(e => e.UserId);
                entity.HasIndex(e => e.UserName).IsUnique();
                entity.Property(e => e.UserName).HasMaxLength(50);
                entity.Property(e => e.UserPwd).HasMaxLength(50);
                entity.Property(e => e.Nickname).HasMaxLength(50);
                entity.Property(e => e.UserGroup).HasMaxLength(50);
                entity.Property(e => e.UserPhoto).HasMaxLength(200);
                entity.Property(e => e.UserBirthday).HasColumnType("datetime");
                entity.Property(e => e.UserPhone).HasMaxLength(20);
                entity.Property(e => e.UserMail).HasMaxLength(50);
                entity.Property(e => e.UserUrl).HasMaxLength(100);
                entity.Property(e => e.UserSay).HasMaxLength(200);
                entity.Property(e => e.UserAddTime).HasColumnType("datetime");
                entity.Property(e => e.UserSign).HasMaxLength(30);
                entity.Property(e => e.OpenId1).HasMaxLength(50);
                entity.Property(e => e.OpenId2).HasMaxLength(50);
                entity.Property(e => e.OpenId3).HasMaxLength(50);
                entity.Property(e => e.OpenId4).HasMaxLength(50);
                entity.Property(e => e.OpenId5).HasMaxLength(50);
                entity.Property(e => e.OpenId6).HasMaxLength(50);
                entity.Property(e => e.OpenId7).HasMaxLength(50);
                entity.Property(e => e.OpenId8).HasMaxLength(50);
                entity.Property(e => e.OpenId9).HasMaxLength(50);
            });
        }
    }
}